// Decompiled with JetBrains decompiler
// Type: PmxLib.Vmd
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  public class Vmd : IBytesConvert, ICloneable
  {
    private const int HeaderBytes = 30;
    private const string HeaderString_V1 = "Vocaloid Motion Data file";
    private const string HeaderString_V2 = "Vocaloid Motion Data 0002";
    public const string CameraHeaderName = "カメラ・照明";
    private const int ModelNameBytes_V1 = 10;
    private const int ModelNameBytes_V2 = 20;
    public string VMDHeader = "Vocaloid Motion Data 0002";
    public string ModelName = "";
    public int ModelNameBytes = 20;
    private Vmd.VmdVersion m_ver;
    public List<VmdMotion> MotionList = new List<VmdMotion>();
    public List<VmdMorph> MorphList = new List<VmdMorph>();
    public List<VmdCamera> CameraList = new List<VmdCamera>();
    public List<VmdLight> LightList = new List<VmdLight>();
    public List<VmdSelfShadow> SelfShadowList = new List<VmdSelfShadow>();
    public List<VmdVisibleIK> VisibleIKList = new List<VmdVisibleIK>();

    public Vmd.VmdVersion Version => this.m_ver;

    public int ByteCount => 30 + this.ModelNameBytes + Vmd.GetListBytes<VmdMotion>(this.MotionList) + Vmd.GetListBytes<VmdMorph>(this.MorphList) + Vmd.GetListBytes<VmdVisibleIK>(this.VisibleIKList) + Vmd.GetListBytes<VmdCamera>(this.CameraList) + Vmd.GetListBytes<VmdLight>(this.LightList) + Vmd.GetListBytes<VmdSelfShadow>(this.SelfShadowList);

    public Vmd() => PmxLibClass.IsLocked();

    public Vmd(Vmd vmd)
      : this()
    {
      this.VMDHeader = vmd.VMDHeader;
      this.ModelName = vmd.ModelName;
      this.MotionList = CP.CloneList<VmdMotion>(vmd.MotionList);
      this.MorphList = CP.CloneList<VmdMorph>(vmd.MorphList);
      this.CameraList = CP.CloneList<VmdCamera>(vmd.CameraList);
      this.LightList = CP.CloneList<VmdLight>(vmd.LightList);
      this.SelfShadowList = CP.CloneList<VmdSelfShadow>(vmd.SelfShadowList);
      this.VisibleIKList = CP.CloneList<VmdVisibleIK>(vmd.VisibleIKList);
    }

    public Vmd(string path)
      : this()
    {
      this.FromFile(path);
    }

    public void FromFile(string path)
    {
      try
      {
        this.FromBytes(File.ReadAllBytes(path), 0);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void NormalizeList(Vmd.NormalizeDataType type)
    {
      switch (type)
      {
        case Vmd.NormalizeDataType.All:
          this.MotionList.Sort(new Comparison<VmdMotion>(VmdFrameBase.Compare));
          this.MorphList.Sort(new Comparison<VmdMorph>(VmdFrameBase.Compare));
          this.CameraList.Sort(new Comparison<VmdCamera>(VmdFrameBase.Compare));
          this.LightList.Sort(new Comparison<VmdLight>(VmdFrameBase.Compare));
          this.SelfShadowList.Sort(new Comparison<VmdSelfShadow>(VmdFrameBase.Compare));
          this.VisibleIKList.Sort(new Comparison<VmdVisibleIK>(VmdFrameBase.Compare));
          break;
        case Vmd.NormalizeDataType.Motion:
          this.MotionList.Sort(new Comparison<VmdMotion>(VmdFrameBase.Compare));
          break;
        case Vmd.NormalizeDataType.Skin:
          this.MorphList.Sort(new Comparison<VmdMorph>(VmdFrameBase.Compare));
          break;
        case Vmd.NormalizeDataType.Camera:
          this.CameraList.Sort(new Comparison<VmdCamera>(VmdFrameBase.Compare));
          break;
        case Vmd.NormalizeDataType.Light:
          this.LightList.Sort(new Comparison<VmdLight>(VmdFrameBase.Compare));
          break;
        case Vmd.NormalizeDataType.SelfShadow:
          this.SelfShadowList.Sort(new Comparison<VmdSelfShadow>(VmdFrameBase.Compare));
          break;
        case Vmd.NormalizeDataType.VisibleIK:
          this.VisibleIKList.Sort(new Comparison<VmdVisibleIK>(VmdFrameBase.Compare));
          break;
      }
    }

    public byte[] ToBytes()
    {
      this.NormalizeList(Vmd.NormalizeDataType.All);
      List<byte> byteList = new List<byte>();
      byte[] buf1 = new byte[30];
      BytesStringProc.SetString(buf1, this.VMDHeader, (byte) 0, (byte) 0);
      byteList.AddRange((IEnumerable<byte>) buf1);
      byte[] buf2 = new byte[this.ModelNameBytes];
      BytesStringProc.SetString(buf2, this.ModelName, (byte) 0, (byte) 253);
      byteList.AddRange((IEnumerable<byte>) buf2);
      int count1 = this.MotionList.Count;
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(count1));
      for (int index = 0; index < count1; ++index)
        byteList.AddRange((IEnumerable<byte>) this.MotionList[index].ToBytes());
      int count2 = this.MorphList.Count;
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(count2));
      for (int index = 0; index < count2; ++index)
        byteList.AddRange((IEnumerable<byte>) this.MorphList[index].ToBytes());
      int count3 = this.VisibleIKList.Count;
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(count3));
      for (int index = 0; index < count3; ++index)
        byteList.AddRange((IEnumerable<byte>) this.VisibleIKList[index].ToBytes());
      int count4 = this.CameraList.Count;
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(count4));
      for (int index = 0; index < count4; ++index)
        byteList.AddRange((IEnumerable<byte>) this.CameraList[index].ToBytes());
      int count5 = this.LightList.Count;
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(count5));
      for (int index = 0; index < count5; ++index)
        byteList.AddRange((IEnumerable<byte>) this.LightList[index].ToBytes());
      int count6 = this.SelfShadowList.Count;
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(count6));
      for (int index = 0; index < count6; ++index)
        byteList.AddRange((IEnumerable<byte>) this.SelfShadowList[index].ToBytes());
      return byteList.ToArray();
    }

    public void FromBytes(byte[] bytes, int startIndex)
    {
      byte[] buf = new byte[30];
      Array.Copy((Array) bytes, startIndex, (Array) buf, 0, 30);
      this.VMDHeader = BytesStringProc.GetString(buf, (byte) 0);
      int num;
      if (string.Compare(this.VMDHeader, "Vocaloid Motion Data file", true) == 0)
      {
        this.ModelNameBytes = 10;
        num = 1;
        this.m_ver = Vmd.VmdVersion.v1;
      }
      else
      {
        if ((uint) string.Compare(this.VMDHeader, "Vocaloid Motion Data 0002", true) > 0U)
          throw new Exception("対応したVMDファイルではありません");
        this.ModelNameBytes = 20;
        num = 2;
        this.m_ver = Vmd.VmdVersion.v2;
      }
      int sourceIndex = startIndex + 30;
      Array.Copy((Array) bytes, sourceIndex, (Array) buf, 0, this.ModelNameBytes);
      this.ModelName = BytesStringProc.GetString(buf, (byte) 0);
      int startIndex1 = sourceIndex + this.ModelNameBytes;
      int int32_1 = BitConverter.ToInt32(bytes, startIndex1);
      int startIndex2 = startIndex1 + 4;
      this.MotionList.Clear();
      this.MotionList.Capacity = int32_1;
      for (int index = 0; index < int32_1; ++index)
      {
        VmdMotion vmdMotion = new VmdMotion();
        vmdMotion.FromBytes(bytes, startIndex2);
        startIndex2 += vmdMotion.ByteCount;
        this.MotionList.Add(vmdMotion);
      }
      if (bytes.Length <= startIndex2)
        return;
      int int32_2 = BitConverter.ToInt32(bytes, startIndex2);
      int startIndex3 = startIndex2 + 4;
      this.MorphList.Clear();
      this.MorphList.Capacity = int32_2;
      for (int index = 0; index < int32_2; ++index)
      {
        VmdMorph vmdMorph = new VmdMorph();
        vmdMorph.FromBytes(bytes, startIndex3);
        startIndex3 += vmdMorph.ByteCount;
        this.MorphList.Add(vmdMorph);
      }
      if (bytes.Length > startIndex3)
      {
        int int32_3 = BitConverter.ToInt32(bytes, startIndex3);
        int startIndex4 = startIndex3 + 4;
        this.CameraList.Clear();
        this.CameraList.Capacity = int32_3;
        if (num == 1)
        {
          for (int index = 0; index < int32_3; ++index)
          {
            VmdCamera_v1 vmdCameraV1 = new VmdCamera_v1();
            vmdCameraV1.FromBytes(bytes, startIndex4);
            startIndex4 += vmdCameraV1.ByteCount;
            this.CameraList.Add(vmdCameraV1.ToVmdCamera());
          }
        }
        else if (num == 2)
        {
          for (int index = 0; index < int32_3; ++index)
          {
            VmdCamera vmdCamera = new VmdCamera();
            vmdCamera.FromBytes(bytes, startIndex4);
            startIndex4 += vmdCamera.ByteCount;
            this.CameraList.Add(vmdCamera);
          }
        }
        if (bytes.Length > startIndex4)
        {
          int int32_4 = BitConverter.ToInt32(bytes, startIndex4);
          int startIndex5 = startIndex4 + 4;
          this.LightList.Clear();
          this.LightList.Capacity = int32_4;
          for (int index = 0; index < int32_4; ++index)
          {
            VmdLight vmdLight = new VmdLight();
            vmdLight.FromBytes(bytes, startIndex5);
            startIndex5 += vmdLight.ByteCount;
            this.LightList.Add(vmdLight);
          }
          if (bytes.Length > startIndex5)
          {
            int int32_5 = BitConverter.ToInt32(bytes, startIndex5);
            int startIndex6 = startIndex5 + 4;
            this.SelfShadowList.Clear();
            this.SelfShadowList.Capacity = int32_5;
            for (int index = 0; index < int32_5; ++index)
            {
              VmdSelfShadow vmdSelfShadow = new VmdSelfShadow();
              vmdSelfShadow.FromBytes(bytes, startIndex6);
              startIndex6 += vmdSelfShadow.ByteCount;
              this.SelfShadowList.Add(vmdSelfShadow);
            }
            if (bytes.Length > startIndex6)
            {
              int int32_6 = BitConverter.ToInt32(bytes, startIndex6);
              int startIndex7 = startIndex6 + 4;
              this.VisibleIKList.Clear();
              this.VisibleIKList.Capacity = int32_6;
              for (int index = 0; index < int32_6; ++index)
              {
                VmdVisibleIK vmdVisibleIk = new VmdVisibleIK();
                vmdVisibleIk.FromBytes(bytes, startIndex7);
                startIndex7 += vmdVisibleIk.ByteCount;
                this.VisibleIKList.Add(vmdVisibleIk);
              }
            }
          }
        }
      }
    }

    public object Clone() => (object) new Vmd(this);

    public static int GetListBytes<T>(List<T> list) where T : IBytesConvert
    {
      int count = list.Count;
      int num = 0;
      for (int index = 0; index < count; ++index)
        num += list[index].ByteCount;
      return num;
    }

    public enum VmdVersion
    {
      v2,
      v1,
    }

    public enum NormalizeDataType
    {
      All,
      Motion,
      Skin,
      Camera,
      Light,
      SelfShadow,
      VisibleIK,
    }
  }
}
