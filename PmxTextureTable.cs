// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxTextureTable
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  public class PmxTextureTable : IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    PmxObject IPmxObjectKey.ObjectKey => PmxObject.TexTable;

    public Dictionary<string, int> NameToIndex { get; private set; }

    public Dictionary<int, string> IndexToName { get; private set; }

    public int Count => this.NameToIndex.Count;

    public int GetIndex(string name)
    {
      int num = -1;
      if (this.NameToIndex.ContainsKey(name))
        num = this.NameToIndex[name];
      return num;
    }

    public string GetName(int ix)
    {
      string str = "";
      if (this.IndexToName.ContainsKey(ix))
        str = this.IndexToName[ix];
      return str;
    }

    public PmxTextureTable()
    {
      this.NameToIndex = new Dictionary<string, int>();
      this.IndexToName = new Dictionary<int, string>();
    }

    public PmxTextureTable(PmxTextureTable tx) => this.FromPmxTextureTable(tx);

    public void FromPmxTextureTable(PmxTextureTable tx)
    {
      string[] strArray = new string[tx.Count];
      tx.NameToIndex.Keys.CopyTo(strArray, 0);
      this.CreateTable(strArray);
    }

    public PmxTextureTable(List<PmxMaterial> ml)
      : this()
    {
      this.CreateTable(ml);
    }

    public void CreateTable(List<PmxMaterial> ml)
    {
      this.NameToIndex.Clear();
      this.IndexToName.Clear();
      int key = 0;
      for (int index = 0; index < ml.Count; ++index)
      {
        PmxMaterial pmxMaterial = ml[index];
        if (!string.IsNullOrEmpty(pmxMaterial.Tex) && !this.NameToIndex.ContainsKey(pmxMaterial.Tex))
        {
          this.NameToIndex.Add(pmxMaterial.Tex, key);
          this.IndexToName.Add(key, pmxMaterial.Tex);
          ++key;
        }
        if (!string.IsNullOrEmpty(pmxMaterial.Sphere) && !this.NameToIndex.ContainsKey(pmxMaterial.Sphere))
        {
          this.NameToIndex.Add(pmxMaterial.Sphere, key);
          this.IndexToName.Add(key, pmxMaterial.Sphere);
          ++key;
        }
        if (!string.IsNullOrEmpty(pmxMaterial.Toon) && !this.NameToIndex.ContainsKey(pmxMaterial.Toon) && !SystemToon.IsSystemToon(pmxMaterial.Toon))
        {
          this.NameToIndex.Add(pmxMaterial.Toon, key);
          this.IndexToName.Add(key, pmxMaterial.Toon);
          ++key;
        }
      }
    }

    public void CreateTable(string[] names)
    {
      this.NameToIndex.Clear();
      this.IndexToName.Clear();
      for (int key = 0; key < names.Length; ++key)
      {
        this.NameToIndex.Add(names[key], key);
        this.IndexToName.Add(key, names[key]);
      }
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      int length = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      string[] names = new string[length];
      for (int index = 0; index < length; ++index)
        names[index] = PmxStreamHelper.ReadString(s, f);
      this.CreateTable(names);
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      int count = this.Count;
      PmxStreamHelper.WriteElement_Int32(s, count, 4, true);
      for (int key = 0; key < count; ++key)
        PmxStreamHelper.WriteString(s, this.IndexToName[key], f);
    }

    object ICloneable.Clone() => (object) new PmxTextureTable(this);

    public PmxTextureTable Clone() => new PmxTextureTable(this);
  }
}
