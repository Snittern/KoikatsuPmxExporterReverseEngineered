// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdFrameBase
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System.Collections.Generic;

namespace PmxLib
{
  public abstract class VmdFrameBase : IComparer<VmdFrameBase>
  {
    public int FrameIndex;

    public static int Compare(VmdFrameBase x, VmdFrameBase y) => x.FrameIndex - y.FrameIndex;

    int IComparer<VmdFrameBase>.Compare(VmdFrameBase x, VmdFrameBase y) => VmdFrameBase.Compare(x, y);
  }
}
