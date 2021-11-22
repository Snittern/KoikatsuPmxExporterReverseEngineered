// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxFace
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;

namespace PmxLib
{
  public class PmxFace : IPmxObjectKey, ICloneable
  {
    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Face;

    public PmxVertex V0 { get; set; }

    public PmxVertex V1 { get; set; }

    public PmxVertex V2 { get; set; }

    public PmxFace()
    {
    }

    public PmxFace(PmxFace f) => this.FromPmxFace(f);

    public void FromPmxFace(PmxFace f)
    {
      this.V0 = f.V0;
      this.V1 = f.V1;
      this.V2 = f.V2;
    }

    object ICloneable.Clone() => (object) new PmxFace(this);

    public PmxFace Clone() => new PmxFace(this);
  }
}
