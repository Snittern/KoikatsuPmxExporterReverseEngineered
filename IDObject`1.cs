// Decompiled with JetBrains decompiler
// Type: PmxLib.IDObject`1
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System.Collections.Generic;

namespace PmxLib
{
  internal class IDObject<T>
  {
    private Dictionary<uint, T> m_table;
    private readonly uint m_limit;
    private uint m_lastID;

    public int Count => this.m_table.Keys.Count;

    public T this[uint i] => this.Get(i);

    public IEnumerable<uint> IDs
    {
      get
      {
        foreach (uint current in this.m_table.Keys)
        {
          if (current > 0U)
            yield return current;
        }
      }
    }

    public bool IsIDOverflow { get; private set; }

    public IDObject(uint limit)
    {
      if (limit == 0U)
        limit = uint.MaxValue;
      this.m_limit = limit;
      this.Clear();
    }

    public void Clear()
    {
      if (this.m_table != null)
      {
        this.m_table.Clear();
        this.m_table = (Dictionary<uint, T>) null;
      }
      this.m_table = new Dictionary<uint, T>();
      this.m_table.Add(0U, default (T));
      this.m_lastID = 0U;
      this.IsIDOverflow = false;
    }

    public uint NewObject(T obj)
    {
      uint key;
      if (this.IsIDOverflow)
      {
        key = this.SearchNextID(this.m_lastID + 1U);
        if (key == 0U)
        {
          key = this.SearchNextID(1U);
          if (key == 0U)
          {
            this.m_lastID = 0U;
            throw new IDOverflowException();
          }
        }
      }
      else
      {
        key = ++this.m_lastID;
        if (key >= this.m_limit)
        {
          this.IsIDOverflow = true;
          this.m_lastID = 0U;
          return this.NewObject(obj);
        }
      }
      if (key > 0U)
      {
        this.m_table.Add(key, obj);
        this.m_lastID = key;
      }
      return key;
    }

    private uint SearchNextID(uint st)
    {
      for (uint key = st; key < this.m_limit; ++key)
      {
        if (!this.m_table.ContainsKey(key))
          return key;
      }
      return 0;
    }

    public bool ContainsID(uint id) => id != 0U && this.m_table.ContainsKey(id);

    public T Get(uint id) => id != 0U ? (!this.m_table.ContainsKey(id) ? default (T) : this.m_table[id]) : default (T);

    public void Remove(uint id)
    {
      if (id <= 0U || !this.m_table.ContainsKey(id))
        return;
      this.m_table.Remove(id);
    }

    public IEnumerator<T> GetEnumerator()
    {
      foreach (uint current in this.IDs)
        yield return this.m_table[current];
    }
  }
}
