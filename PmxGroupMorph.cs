// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxGroupMorph
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  internal class PmxGroupMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    public int Index;
    public float Ratio;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.GroupMorph;

    public override int BaseIndex
    {
      get => this.Index;
      set => this.Index = value;
    }

    public PmxMorph RefMorph { get; set; }

    public PmxGroupMorph() => this.Ratio = 1f;

    public PmxGroupMorph(int index, float r)
      : this()
    {
      this.Index = index;
      this.Ratio = r;
    }

    public PmxGroupMorph(PmxGroupMorph sv)
      : this()
    {
      this.FromPmxGroupMorph(sv);
    }

    public void FromPmxGroupMorph(PmxGroupMorph sv)
    {
      this.Index = sv.Index;
      this.Ratio = sv.Ratio;
    }

    public override void FromStreamEx(Stream s, PmxElementFormat size)
    {
      this.Index = PmxStreamHelper.ReadElement_Int32(s, size.MorphSize, true);
      this.Ratio = PmxStreamHelper.ReadElement_Float(s);
    }

    public override void ToStreamEx(Stream s, PmxElementFormat size)
    {
      PmxStreamHelper.WriteElement_Int32(s, this.Index, size.MorphSize, true);
      PmxStreamHelper.WriteElement_Float(s, this.Ratio);
    }

    object ICloneable.Clone() => (object) new PmxGroupMorph(this);

    public override PmxBaseMorph Clone() => (PmxBaseMorph) new PmxGroupMorph(this);
  }
}
