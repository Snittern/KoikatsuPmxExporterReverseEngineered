﻿// Decompiled with JetBrains decompiler
// Type: PmxLib.IBytesConvert
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

namespace PmxLib
{
  public interface IBytesConvert
  {
    int ByteCount { get; }

    byte[] ToBytes();

    void FromBytes(byte[] bytes, int startIndex);
  }
}
