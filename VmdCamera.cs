// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdCamera
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  public class VmdCamera : VmdFrameBase, IBytesConvert, ICloneable
  {
    public float Distance;
    public Vector3 Position;
    public Vector3 Rotate;
    public VmdCameraIPL IPL = new VmdCameraIPL();
    public float Angle;
    public byte Pers;

    public int ByteCount => 32 + this.IPL.ByteCount + 1 + 4;

    public VmdCamera()
    {
    }

    public VmdCamera(VmdCamera camera)
    {
      this.FrameIndex = camera.FrameIndex;
      this.Distance = camera.Distance;
      this.Position = camera.Position;
      this.Rotate = camera.Rotate;
      this.IPL = (VmdCameraIPL) camera.IPL.Clone();
      this.Angle = camera.Angle;
      this.Pers = camera.Pers;
    }

    public byte[] ToBytes()
    {
      List<byte> byteList = new List<byte>();
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.FrameIndex));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Distance));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Position.x));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Position.y));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Position.z));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Rotate.x));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Rotate.y));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Rotate.z));
      byteList.AddRange((IEnumerable<byte>) this.IPL.ToBytes());
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes((int) this.Angle));
      byteList.Add(this.Pers);
      return byteList.ToArray();
    }

    public void FromBytes(byte[] bytes, int startIndex)
    {
      this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
      int startIndex1 = startIndex + 4;
      this.Distance = BitConverter.ToSingle(bytes, startIndex1);
      int startIndex2 = startIndex1 + 4;
      this.Position.x = BitConverter.ToSingle(bytes, startIndex2);
      int startIndex3 = startIndex2 + 4;
      this.Position.y = BitConverter.ToSingle(bytes, startIndex3);
      int startIndex4 = startIndex3 + 4;
      this.Position.z = BitConverter.ToSingle(bytes, startIndex4);
      int startIndex5 = startIndex4 + 4;
      this.Rotate.x = BitConverter.ToSingle(bytes, startIndex5);
      int startIndex6 = startIndex5 + 4;
      this.Rotate.y = BitConverter.ToSingle(bytes, startIndex6);
      int startIndex7 = startIndex6 + 4;
      this.Rotate.z = BitConverter.ToSingle(bytes, startIndex7);
      int startIndex8 = startIndex7 + 4;
      this.IPL.FromBytes(bytes, startIndex8);
      int startIndex9 = startIndex8 + this.IPL.ByteCount;
      this.Angle = (float) BitConverter.ToInt32(bytes, startIndex9);
      int index = startIndex9 + 4;
      this.Pers = bytes[index];
    }

    public object Clone() => (object) new VmdCamera(this);
  }
}
