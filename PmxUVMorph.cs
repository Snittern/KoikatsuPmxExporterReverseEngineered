// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxUVMorph
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  internal class PmxUVMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    public int Index;
    public Vector4 Offset;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.UVMorph;

    public override int BaseIndex
    {
      get => this.Index;
      set => this.Index = value;
    }

    public PmxVertex RefVertex { get; set; }

    public PmxUVMorph() => this.Index = -1;

    public PmxUVMorph(int index, Vector4 offset)
      : this()
    {
      this.Index = index;
      this.Offset = offset;
    }

    public PmxUVMorph(PmxUVMorph sv)
      : this()
    {
      this.FromPmxUVMorph(sv);
    }

    public void FromPmxUVMorph(PmxUVMorph sv)
    {
      this.Index = sv.Index;
      this.Offset = sv.Offset;
    }

    public override void FromStreamEx(Stream s, PmxElementFormat size)
    {
      this.Index = PmxStreamHelper.ReadElement_Int32(s, size.VertexSize, false);
      this.Offset = V4_BytesConvert.FromStream(s);
    }

    public override void ToStreamEx(Stream s, PmxElementFormat size)
    {
      PmxStreamHelper.WriteElement_Int32(s, this.Index, size.VertexSize, false);
      V4_BytesConvert.ToStream(s, this.Offset);
    }

    object ICloneable.Clone() => (object) new PmxUVMorph(this);

    public override PmxBaseMorph Clone() => (PmxBaseMorph) new PmxUVMorph(this);
  }
}
