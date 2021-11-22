// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxIK
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  public class PmxIK : IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    public int Target;
    public int LoopCount;
    public float Angle;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.IK;

    public PmxBone RefTarget { get; set; }

    public List<PmxIK.IKLink> LinkList { get; private set; }

    public PmxIK()
    {
      this.Target = -1;
      this.LoopCount = 0;
      this.Angle = 1f;
      this.LinkList = new List<PmxIK.IKLink>();
    }

    public PmxIK(PmxIK ik) => this.FromPmxIK(ik);

    public void FromPmxIK(PmxIK ik)
    {
      this.Target = ik.Target;
      this.LoopCount = ik.LoopCount;
      this.Angle = ik.Angle;
      this.LinkList = new List<PmxIK.IKLink>();
      for (int index = 0; index < ik.LinkList.Count; ++index)
        this.LinkList.Add(ik.LinkList[index].Clone());
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.Target = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
      this.LoopCount = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.Angle = PmxStreamHelper.ReadElement_Float(s);
      int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.LinkList.Clear();
      this.LinkList.Capacity = num;
      for (int index = 0; index < num; ++index)
      {
        PmxIK.IKLink ikLink = new PmxIK.IKLink();
        ikLink.FromStreamEx(s, f);
        this.LinkList.Add(ikLink);
      }
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      PmxStreamHelper.WriteElement_Int32(s, this.Target, f.BoneSize, true);
      PmxStreamHelper.WriteElement_Int32(s, this.LoopCount, 4, true);
      PmxStreamHelper.WriteElement_Float(s, this.Angle);
      PmxStreamHelper.WriteElement_Int32(s, this.LinkList.Count, 4, true);
      for (int index = 0; index < this.LinkList.Count; ++index)
        this.LinkList[index].ToStreamEx(s, f);
    }

    object ICloneable.Clone() => (object) new PmxIK(this);

    public PmxIK Clone() => new PmxIK(this);

    public class IKLink : IPmxObjectKey, IPmxStreamIO, ICloneable
    {
      public int Bone;
      public bool IsLimit;
      public Vector3 Low;
      public Vector3 High;
      public PmxIK.IKLink.EulerType Euler;
      public PmxIK.IKLink.FixAxisType FixAxis;

      PmxObject IPmxObjectKey.ObjectKey => PmxObject.IKLink;

      public PmxBone RefBone { get; set; }

      public IKLink()
      {
        this.Bone = -1;
        this.IsLimit = false;
      }

      public IKLink(PmxIK.IKLink link) => this.FromIKLink(link);

      public void FromIKLink(PmxIK.IKLink link)
      {
        this.Bone = link.Bone;
        this.IsLimit = link.IsLimit;
        this.Low = link.Low;
        this.High = link.High;
        this.Euler = link.Euler;
      }

      public void NormalizeAngle()
      {
        Vector3 vector3_1 = new Vector3();
        this.Low.x = Math.Min(this.Low.x, this.High.x);
        Vector3 vector3_2 = new Vector3();
        this.High.x = Math.Max(this.Low.x, this.High.x);
        this.Low.y = Math.Min(this.Low.y, this.High.y);
        this.High.y = Math.Max(this.Low.y, this.High.y);
        this.Low.z = Math.Min(this.Low.z, this.High.z);
        this.High.z = Math.Max(this.Low.z, this.High.z);
        this.Low = vector3_1;
        this.High = vector3_2;
      }

      public void NormalizeEulerAxis()
      {
        this.Euler = -1.57079637050629 >= (double) this.Low.x || (double) this.High.x >= 1.57079637050629 ? (-1.57079637050629 >= (double) this.Low.y || (double) this.High.y >= 1.57079637050629 ? PmxIK.IKLink.EulerType.YZX : PmxIK.IKLink.EulerType.XYZ) : PmxIK.IKLink.EulerType.ZXY;
        this.FixAxis = PmxIK.IKLink.FixAxisType.None;
        if ((double) this.Low.x == 0.0 && (double) this.High.x == 0.0 && (double) this.Low.y == 0.0 && (double) this.High.y == 0.0 && (double) this.Low.z == 0.0 && (double) this.High.z == 0.0)
          this.FixAxis = PmxIK.IKLink.FixAxisType.Fix;
        else if ((double) this.Low.y == 0.0 && (double) this.High.y == 0.0 && (double) this.Low.z == 0.0 && (double) this.High.z == 0.0)
          this.FixAxis = PmxIK.IKLink.FixAxisType.X;
        else if ((double) this.Low.x == 0.0 && (double) this.High.x == 0.0 && (double) this.Low.z == 0.0 && (double) this.High.z == 0.0)
          this.FixAxis = PmxIK.IKLink.FixAxisType.Y;
        else if ((double) this.Low.x == 0.0 && (double) this.High.x == 0.0 && (double) this.Low.y == 0.0 && (double) this.High.y == 0.0)
          this.FixAxis = PmxIK.IKLink.FixAxisType.Z;
      }

      public void FromStreamEx(Stream s, PmxElementFormat f)
      {
        this.Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
        this.IsLimit = (uint) s.ReadByte() > 0U;
        if (!this.IsLimit)
          return;
        this.Low = V3_BytesConvert.FromStream(s);
        this.High = V3_BytesConvert.FromStream(s);
      }

      public void ToStreamEx(Stream s, PmxElementFormat f)
      {
        PmxStreamHelper.WriteElement_Int32(s, this.Bone, f.BoneSize, true);
        s.WriteByte(this.IsLimit ? (byte) 1 : (byte) 0);
        if (!this.IsLimit)
          return;
        V3_BytesConvert.ToStream(s, this.Low);
        V3_BytesConvert.ToStream(s, this.High);
      }

      object ICloneable.Clone() => (object) new PmxIK.IKLink(this);

      public PmxIK.IKLink Clone() => new PmxIK.IKLink(this);

      public enum EulerType
      {
        ZXY,
        XYZ,
        YZX,
      }

      public enum FixAxisType
      {
        None,
        Fix,
        X,
        Y,
        Z,
      }
    }
  }
}
