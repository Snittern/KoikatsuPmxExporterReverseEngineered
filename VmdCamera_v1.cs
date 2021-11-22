// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdCamera_v1
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  internal class VmdCamera_v1 : VmdFrameBase, IBytesConvert, ICloneable
  {
    public float Distance;
    public Vector3 Position;
    public Vector3 Rotate;
    public VmdIplData CameraIpl = new VmdIplData();

    public int ByteCount => 36;

    public VmdCamera_v1()
    {
    }

    public VmdCamera_v1(VmdCamera_v1 camera)
    {
      this.FrameIndex = camera.FrameIndex;
      this.Distance = camera.Distance;
      this.Position = camera.Position;
      this.Rotate = camera.Rotate;
      this.CameraIpl = camera.CameraIpl;
    }

    public VmdCamera ToVmdCamera()
    {
      VmdCamera vmdCamera = new VmdCamera();
      vmdCamera.FrameIndex = this.FrameIndex;
      vmdCamera.Distance = this.Distance;
      vmdCamera.Position = this.Position;
      vmdCamera.Rotate = this.Rotate;
      vmdCamera.IPL.MoveX = new VmdIplData(this.CameraIpl);
      vmdCamera.IPL.MoveY = new VmdIplData(this.CameraIpl);
      vmdCamera.IPL.MoveZ = new VmdIplData(this.CameraIpl);
      vmdCamera.IPL.Rotate = new VmdIplData(this.CameraIpl);
      vmdCamera.IPL.Distance = new VmdIplData(this.CameraIpl);
      vmdCamera.IPL.Angle = new VmdIplData(this.CameraIpl);
      vmdCamera.Angle = 45f;
      vmdCamera.Pers = (byte) 0;
      return vmdCamera;
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
      byteList.Add((byte) this.CameraIpl.P1.X);
      byteList.Add((byte) this.CameraIpl.P2.X);
      byteList.Add((byte) this.CameraIpl.P1.Y);
      byteList.Add((byte) this.CameraIpl.P2.Y);
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
      int index1 = startIndex7 + 4;
      this.CameraIpl.P1.X = (int) bytes[index1];
      int index2 = index1 + 1;
      this.CameraIpl.P1.Y = (int) bytes[index2];
      int index3 = index2 + 1;
      this.CameraIpl.P2.X = (int) bytes[index3];
      int index4 = index3 + 1;
      this.CameraIpl.P2.Y = (int) bytes[index4];
      int num = index4 + 1;
    }

    public object Clone() => (object) new VmdCamera_v1(this);
  }
}
