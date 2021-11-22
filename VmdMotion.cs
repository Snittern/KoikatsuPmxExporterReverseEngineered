// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdMotion
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  public class VmdMotion : VmdFrameBase, IBytesConvert, ICloneable
  {
    private const int NameBytes = 15;
    private const ushort PhysicsOffNum = 3939;
    public string Name = "";
    public Vector3 Position;
    public Quaternion Rotate = Quaternion.identity;
    public VmdMotionIPL IPL = new VmdMotionIPL();
    protected int NoDataCount = 48;

    public bool PhysicsOff { get; set; }

    public int ByteCount => 47 + this.IPL.ByteCount + this.NoDataCount;

    public VmdMotion() => this.PhysicsOff = false;

    public VmdMotion(VmdMotion motion)
      : this()
    {
      this.Name = motion.Name;
      this.FrameIndex = motion.FrameIndex;
      this.Position = motion.Position;
      this.Rotate = motion.Rotate;
      this.IPL = (VmdMotionIPL) motion.IPL.Clone();
      this.PhysicsOff = motion.PhysicsOff;
    }

    public byte[] ToBytes()
    {
      List<byte> byteList = new List<byte>();
      byte[] buf = new byte[15];
      BytesStringProc.SetString(buf, this.Name, (byte) 0, (byte) 253);
      byteList.AddRange((IEnumerable<byte>) buf);
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.FrameIndex));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Position.x));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Position.y));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Position.z));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Rotate.x));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Rotate.y));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Rotate.z));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Rotate.w));
      byte[] numArray = new byte[this.IPL.ByteCount * 4];
      byte[] bytes1 = this.IPL.ToBytes();
      int length = bytes1.Length;
      for (int index = 0; index < length; ++index)
        numArray[index * 4] = bytes1[index];
      if (this.PhysicsOff)
      {
        byte[] bytes2 = BitConverter.GetBytes(3939);
        numArray[2] = bytes2[0];
        numArray[3] = bytes2[1];
      }
      byteList.AddRange((IEnumerable<byte>) numArray);
      return byteList.ToArray();
    }

    public void FromBytes(byte[] bytes, int startIndex)
    {
      byte[] buf = new byte[15];
      Array.Copy((Array) bytes, startIndex, (Array) buf, 0, 15);
      this.Name = BytesStringProc.GetString(buf, (byte) 0);
      int startIndex1 = startIndex + 15;
      this.FrameIndex = BitConverter.ToInt32(bytes, startIndex1);
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
      this.Rotate.w = BitConverter.ToSingle(bytes, startIndex8);
      int sourceIndex = startIndex8 + 4;
      int byteCount = this.IPL.ByteCount;
      byte[] numArray = new byte[byteCount * 4];
      byte[] bytes1 = new byte[this.IPL.ByteCount];
      Array.Copy((Array) bytes, sourceIndex, (Array) numArray, 0, numArray.Length);
      this.PhysicsOff = BitConverter.ToUInt16(numArray, 2) == (ushort) 3939;
      for (int index = 0; index < byteCount; ++index)
        bytes1[index] = numArray[index * 4];
      this.IPL.FromBytes(bytes1, 0);
    }

    public object Clone() => (object) new VmdMotion(this);
  }
}
