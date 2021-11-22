// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdSelfShadow
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  public class VmdSelfShadow : VmdFrameBase, IBytesConvert, ICloneable
  {
    public int Mode;
    public float Distance;

    public int ByteCount => 9;

    public VmdSelfShadow()
    {
      this.Mode = 0;
      this.Distance = 11f / 1000f;
    }

    public VmdSelfShadow(VmdSelfShadow shadow)
    {
      this.FrameIndex = shadow.FrameIndex;
      this.Mode = shadow.Mode;
      this.Distance = shadow.Distance;
    }

    public byte[] ToBytes()
    {
      List<byte> byteList = new List<byte>();
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.FrameIndex));
      byteList.Add((byte) this.Mode);
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Distance));
      return byteList.ToArray();
    }

    public void FromBytes(byte[] bytes, int startIndex)
    {
      this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
      int num1 = startIndex + 4;
      byte[] numArray = bytes;
      int index = num1;
      int startIndex1 = index + 1;
      this.Mode = (int) numArray[index];
      this.Distance = BitConverter.ToSingle(bytes, startIndex1);
      int num2 = startIndex1 + 4;
    }

    public object Clone() => (object) new VmdSelfShadow(this);
  }
}
