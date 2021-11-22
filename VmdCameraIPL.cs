// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdCameraIPL
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  public class VmdCameraIPL : IBytesConvert, ICloneable
  {
    public VmdIplData MoveX = new VmdIplData();
    public VmdIplData MoveY = new VmdIplData();
    public VmdIplData MoveZ = new VmdIplData();
    public VmdIplData Rotate = new VmdIplData();
    public VmdIplData Distance = new VmdIplData();
    public VmdIplData Angle = new VmdIplData();

    public int ByteCount => 24;

    public VmdCameraIPL()
    {
    }

    public VmdCameraIPL(VmdCameraIPL ipl)
    {
      this.MoveX = (VmdIplData) ipl.MoveX.Clone();
      this.MoveY = (VmdIplData) ipl.MoveY.Clone();
      this.MoveZ = (VmdIplData) ipl.MoveZ.Clone();
      this.Rotate = (VmdIplData) ipl.Rotate.Clone();
      this.Distance = (VmdIplData) ipl.Distance.Clone();
      this.Angle = (VmdIplData) ipl.Angle.Clone();
    }

    public byte[] ToBytes() => new List<byte>()
    {
      (byte) this.MoveX.P1.X,
      (byte) this.MoveX.P2.X,
      (byte) this.MoveX.P1.Y,
      (byte) this.MoveX.P2.Y,
      (byte) this.MoveY.P1.X,
      (byte) this.MoveY.P2.X,
      (byte) this.MoveY.P1.Y,
      (byte) this.MoveY.P2.Y,
      (byte) this.MoveZ.P1.X,
      (byte) this.MoveZ.P2.X,
      (byte) this.MoveZ.P1.Y,
      (byte) this.MoveZ.P2.Y,
      (byte) this.Rotate.P1.X,
      (byte) this.Rotate.P2.X,
      (byte) this.Rotate.P1.Y,
      (byte) this.Rotate.P2.Y,
      (byte) this.Distance.P1.X,
      (byte) this.Distance.P2.X,
      (byte) this.Distance.P1.Y,
      (byte) this.Distance.P2.Y,
      (byte) this.Angle.P1.X,
      (byte) this.Angle.P2.X,
      (byte) this.Angle.P1.Y,
      (byte) this.Angle.P2.Y
    }.ToArray();

    public void FromBytes(byte[] bytes, int startIndex)
    {
      this.MoveX.P1.X = (int) bytes[startIndex];
      int index1 = startIndex + 1;
      this.MoveX.P2.X = (int) bytes[index1];
      int index2 = index1 + 1;
      this.MoveX.P1.Y = (int) bytes[index2];
      int index3 = index2 + 1;
      this.MoveX.P2.Y = (int) bytes[index3];
      int index4 = index3 + 1;
      this.MoveY.P1.X = (int) bytes[index4];
      int index5 = index4 + 1;
      this.MoveY.P2.X = (int) bytes[index5];
      int index6 = index5 + 1;
      this.MoveY.P1.Y = (int) bytes[index6];
      int index7 = index6 + 1;
      this.MoveY.P2.Y = (int) bytes[index7];
      int index8 = index7 + 1;
      this.MoveZ.P1.X = (int) bytes[index8];
      int index9 = index8 + 1;
      this.MoveZ.P2.X = (int) bytes[index9];
      int index10 = index9 + 1;
      this.MoveZ.P1.Y = (int) bytes[index10];
      int index11 = index10 + 1;
      this.MoveZ.P2.Y = (int) bytes[index11];
      int index12 = index11 + 1;
      this.Rotate.P1.X = (int) bytes[index12];
      int index13 = index12 + 1;
      this.Rotate.P2.X = (int) bytes[index13];
      int index14 = index13 + 1;
      this.Rotate.P1.Y = (int) bytes[index14];
      int index15 = index14 + 1;
      this.Rotate.P2.Y = (int) bytes[index15];
      int index16 = index15 + 1;
      this.Distance.P1.X = (int) bytes[index16];
      int index17 = index16 + 1;
      this.Distance.P2.X = (int) bytes[index17];
      int index18 = index17 + 1;
      this.Distance.P1.Y = (int) bytes[index18];
      int index19 = index18 + 1;
      this.Distance.P2.Y = (int) bytes[index19];
      int index20 = index19 + 1;
      this.Angle.P1.X = (int) bytes[index20];
      int index21 = index20 + 1;
      this.Angle.P2.X = (int) bytes[index21];
      int index22 = index21 + 1;
      this.Angle.P1.Y = (int) bytes[index22];
      int index23 = index22 + 1;
      this.Angle.P2.Y = (int) bytes[index23];
      int num = index23 + 1;
    }

    public object Clone() => (object) new VmdCameraIPL(this);
  }
}
