// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdIplData
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;

namespace PmxLib
{
  public class VmdIplData : ICloneable
  {
    public VmdIplPoint P1;
    public VmdIplPoint P2;

    public VmdIplData()
    {
      this.P1 = new VmdIplPoint(20, 20);
      this.P2 = new VmdIplPoint(107, 107);
    }

    public VmdIplData(VmdIplData ip)
    {
      this.P1 = (VmdIplPoint) ip.P1.Clone();
      this.P2 = (VmdIplPoint) ip.P2.Clone();
    }

    public object Clone() => (object) new VmdIplData(this);
  }
}
