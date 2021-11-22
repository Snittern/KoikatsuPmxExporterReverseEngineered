// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxBodyPassGroup
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Text;

namespace PmxLib
{
  public class PmxBodyPassGroup : ICloneable
  {
    private const int PassGroupCount = 16;

    public bool[] Flags { get; private set; }

    public PmxBodyPassGroup() => this.Flags = new bool[16];

    public PmxBodyPassGroup(PmxBodyPassGroup pg)
      : this()
    {
      for (int index = 0; index < 16; ++index)
        this.Flags[index] = pg.Flags[index];
    }

    public ushort ToFlagBits()
    {
      int num1 = 1;
      int num2 = 0;
      for (int index = 0; index < this.Flags.Length; ++index)
      {
        if (!this.Flags[index])
          num2 |= num1 << index;
      }
      return (ushort) num2;
    }

    public void FromFlagBits(ushort bits)
    {
      ushort num = 1;
      for (int index = 0; index < this.Flags.Length; ++index)
        this.Flags[index] = ((int) bits & (int) num << index) <= 0;
    }

    public void FromFlagBits(bool[] flags)
    {
      int num = Math.Min(this.Flags.Length, flags.Length);
      for (int index = 0; index < num; ++index)
        this.Flags[index] = flags[index];
    }

    public string ToText()
    {
      StringBuilder stringBuilder = new StringBuilder();
      int length = this.Flags.Length;
      for (int index = 0; index < length; ++index)
      {
        if (this.Flags[index])
          stringBuilder.Append((index + 1).ToString() + " ");
      }
      return stringBuilder.ToString();
    }

    public void FromText(string text)
    {
      try
      {
        string[] strArray = text.Split(new char[1]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int length1 = this.Flags.Length;
        for (int index = 0; index < length1; ++index)
          this.Flags[index] = false;
        int length2 = strArray.Length;
        for (int index = 0; index < length2; ++index)
        {
          int result;
          if (int.TryParse(strArray[index], out result))
          {
            --result;
            if (0 <= result && result < length1)
              this.Flags[result] = true;
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    object ICloneable.Clone() => (object) new PmxBodyPassGroup(this);

    public PmxBodyPassGroup Clone() => new PmxBodyPassGroup(this);
  }
}
