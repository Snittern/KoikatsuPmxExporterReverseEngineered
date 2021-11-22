// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdVisibleIK
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  public class VmdVisibleIK : VmdFrameBase, IBytesConvert, ICloneable
  {
    private const int NameBytes = 20;
    public bool Visible;

    public List<VmdVisibleIK.IK> IKList { get; set; }

    public int ByteCount => 9 + 21 * this.IKList.Count;

    public VmdVisibleIK()
    {
      this.Visible = true;
      this.IKList = new List<VmdVisibleIK.IK>();
    }

    public VmdVisibleIK(VmdVisibleIK vik)
    {
      this.Visible = vik.Visible;
      this.IKList = CP.CloneList<VmdVisibleIK.IK>(vik.IKList);
    }

    public byte[] ToBytes()
    {
      List<byte> byteList = new List<byte>();
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.FrameIndex));
      byteList.Add(this.Visible ? (byte) 1 : (byte) 0);
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.IKList.Count));
      for (int index = 0; index < this.IKList.Count; ++index)
      {
        byte[] buf = new byte[20];
        BytesStringProc.SetString(buf, this.IKList[index].IKName, (byte) 0, (byte) 253);
        byteList.AddRange((IEnumerable<byte>) buf);
        byteList.Add(this.IKList[index].Enable ? (byte) 1 : (byte) 0);
      }
      return byteList.ToArray();
    }

    public void FromBytes(byte[] bytes, int startIndex)
    {
      this.FrameIndex = BitConverter.ToInt32(bytes, startIndex);
      int num1 = startIndex + 4;
      byte[] numArray1 = bytes;
      int index1 = num1;
      int startIndex1 = index1 + 1;
      this.Visible = numArray1[index1] > (byte) 0;
      int int32 = BitConverter.ToInt32(bytes, startIndex1);
      int sourceIndex = startIndex1 + 4;
      byte[] buf = new byte[20];
      for (int index2 = 0; index2 < int32; ++index2)
      {
        VmdVisibleIK.IK ik1 = new VmdVisibleIK.IK();
        Array.Copy((Array) bytes, sourceIndex, (Array) buf, 0, 20);
        ik1.IKName = BytesStringProc.GetString(buf, (byte) 0);
        int num2 = sourceIndex + 20;
        VmdVisibleIK.IK ik2 = ik1;
        byte[] numArray2 = bytes;
        int index3 = num2;
        sourceIndex = index3 + 1;
        int num3 = numArray2[index3] > (byte) 0 ? 1 : 0;
        ik2.Enable = num3 != 0;
        this.IKList.Add(ik1);
      }
    }

    public object Clone() => (object) new VmdVisibleIK(this);

    public class IK : ICloneable
    {
      public bool Enable;

      public string IKName { get; set; }

      public IK()
      {
        this.IKName = "";
        this.Enable = true;
      }

      public IK(VmdVisibleIK.IK ik)
      {
        this.IKName = ik.IKName;
        this.Enable = ik.Enable;
      }

      object ICloneable.Clone() => (object) new VmdVisibleIK.IK(this);
    }
  }
}
