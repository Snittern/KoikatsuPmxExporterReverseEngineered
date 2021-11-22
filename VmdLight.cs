// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdLight
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace PmxLib
{
  public class VmdLight : VmdFrameBase, IBytesConvert, ICloneable
  {
    public Color Color;
    public Vector3 Direction;

    public int ByteCount => 28;

    public VmdLight()
    {
    }

    public VmdLight(VmdLight light)
      : this()
    {
      this.FrameIndex = light.FrameIndex;
      this.Color = light.Color;
      this.Direction = light.Direction;
    }

    public byte[] ToBytes()
    {
      List<byte> byteList = new List<byte>();
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.FrameIndex));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes((float) this.Color.r));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes((float) this.Color.g));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes((float) this.Color.b));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Direction.x));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Direction.y));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Direction.z));
      return byteList.ToArray();
    }

    public void FromBytes(byte[] bytes, int startIndex)
    {
      this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
      int startIndex1 = startIndex + 4;
      this.Color.r = BitConverter.ToSingle(bytes, startIndex1);
      int startIndex2 = startIndex1 + 4;
      this.Color.g = BitConverter.ToSingle(bytes, startIndex2);
      int startIndex3 = startIndex2 + 4;
      this.Color.b = BitConverter.ToSingle(bytes, startIndex3);
      int startIndex4 = startIndex3 + 4;
      this.Direction.x = BitConverter.ToSingle(bytes, startIndex4);
      int startIndex5 = startIndex4 + 4;
      this.Direction.y = BitConverter.ToSingle(bytes, startIndex5);
      int startIndex6 = startIndex5 + 4;
      this.Direction.z = BitConverter.ToSingle(bytes, startIndex6);
    }

    public object Clone() => (object) new VmdLight(this);
  }
}
