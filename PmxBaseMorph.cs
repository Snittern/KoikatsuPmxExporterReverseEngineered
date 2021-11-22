// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxBaseMorph
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  public abstract class PmxBaseMorph : IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    PmxObject IPmxObjectKey.ObjectKey => PmxObject.BaseMorph;

    public virtual int BaseIndex { get; set; }

    public void FromPmxBaseMorph(PmxBaseMorph sv) => this.BaseIndex = sv.BaseIndex;

    public static PmxBaseMorph CreateOffsetObject(PmxMorph.OffsetKind kind)
    {
      PmxBaseMorph pmxBaseMorph = (PmxBaseMorph) null;
      switch (kind)
      {
        case PmxMorph.OffsetKind.Group:
        case PmxMorph.OffsetKind.Flip:
          pmxBaseMorph = (PmxBaseMorph) new PmxGroupMorph();
          break;
        case PmxMorph.OffsetKind.Vertex:
          pmxBaseMorph = (PmxBaseMorph) new PmxVertexMorph();
          break;
        case PmxMorph.OffsetKind.Bone:
          pmxBaseMorph = (PmxBaseMorph) new PmxBoneMorph();
          break;
        case PmxMorph.OffsetKind.UV:
        case PmxMorph.OffsetKind.UVA1:
        case PmxMorph.OffsetKind.UVA2:
        case PmxMorph.OffsetKind.UVA3:
        case PmxMorph.OffsetKind.UVA4:
          pmxBaseMorph = (PmxBaseMorph) new PmxUVMorph();
          break;
        case PmxMorph.OffsetKind.Material:
          pmxBaseMorph = (PmxBaseMorph) new PmxMaterialMorph();
          break;
        case PmxMorph.OffsetKind.Impulse:
          pmxBaseMorph = (PmxBaseMorph) new PmxImpulseMorph();
          break;
      }
      return pmxBaseMorph;
    }

    object ICloneable.Clone() => (object) this.Clone();

    public virtual PmxBaseMorph Clone() => (PmxBaseMorph) null;

    public virtual void FromStreamEx(Stream s, PmxElementFormat f)
    {
    }

    public virtual void ToStreamEx(Stream s, PmxElementFormat f)
    {
    }
  }
}
