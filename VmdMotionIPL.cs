// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdMotionIPL
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  public class VmdMotionIPL : IBytesConvert, ICloneable
  {
    public VmdIplData MoveX = new VmdIplData();
    public VmdIplData MoveY = new VmdIplData();
    public VmdIplData MoveZ = new VmdIplData();
    public VmdIplData Rotate = new VmdIplData();

    public int ByteCount => 16;

    public VmdMotionIPL()
    {
    }

    public VmdMotionIPL(VmdMotionIPL ipl)
    {
      this.MoveX = (VmdIplData) ipl.MoveX.Clone();
      this.MoveY = (VmdIplData) ipl.MoveY.Clone();
      this.MoveZ = (VmdIplData) ipl.MoveZ.Clone();
      this.Rotate = (VmdIplData) ipl.Rotate.Clone();
    }

    public byte[] ToBytes() => new List<byte>()
    {
      (byte) this.MoveX.P1.X,
      (byte) this.MoveX.P1.Y,
      (byte) this.MoveX.P2.X,
      (byte) this.MoveX.P2.Y,
      (byte) this.MoveY.P1.X,
      (byte) this.MoveY.P1.Y,
      (byte) this.MoveY.P2.X,
      (byte) this.MoveY.P2.Y,
      (byte) this.MoveZ.P1.X,
      (byte) this.MoveZ.P1.Y,
      (byte) this.MoveZ.P2.X,
      (byte) this.MoveZ.P2.Y,
      (byte) this.Rotate.P1.X,
      (byte) this.Rotate.P1.Y,
      (byte) this.Rotate.P2.X,
      (byte) this.Rotate.P2.Y
    }.ToArray();

    public void FromBytes(byte[] bytes, int startIndex)
    {
      this.MoveX.P1.X = (int) bytes[startIndex];
      int index1 = startIndex + 1;
      this.MoveX.P1.Y = (int) bytes[index1];
      int index2 = index1 + 1;
      this.MoveX.P2.X = (int) bytes[index2];
      int index3 = index2 + 1;
      this.MoveX.P2.Y = (int) bytes[index3];
      int index4 = index3 + 1;
      this.MoveY.P1.X = (int) bytes[index4];
      int index5 = index4 + 1;
      this.MoveY.P1.Y = (int) bytes[index5];
      int index6 = index5 + 1;
      this.MoveY.P2.X = (int) bytes[index6];
      int index7 = index6 + 1;
      this.MoveY.P2.Y = (int) bytes[index7];
      int index8 = index7 + 1;
      this.MoveZ.P1.X = (int) bytes[index8];
      int index9 = index8 + 1;
      this.MoveZ.P1.Y = (int) bytes[index9];
      int index10 = index9 + 1;
      this.MoveZ.P2.X = (int) bytes[index10];
      int index11 = index10 + 1;
      this.MoveZ.P2.Y = (int) bytes[index11];
      int index12 = index11 + 1;
      this.Rotate.P1.X = (int) bytes[index12];
      int index13 = index12 + 1;
      this.Rotate.P1.Y = (int) bytes[index13];
      int index14 = index13 + 1;
      this.Rotate.P2.X = (int) bytes[index14];
      int index15 = index14 + 1;
      this.Rotate.P2.Y = (int) bytes[index15];
      int num = index15 + 1;
    }

    public object Clone() => (object) new VmdMotionIPL(this);
  }
}
