﻿// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxJoint
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  public class PmxJoint : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
  {
    public PmxJoint.JointKind Kind;
    public int BodyA;
    public int BodyB;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Limit_MoveLow;
    public Vector3 Limit_MoveHigh;
    public Vector3 Limit_AngleLow;
    public Vector3 Limit_AngleHigh;
    public Vector3 SpConst_Move;
    public Vector3 SpConst_Rotate;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Joint;

    public string Name { get; set; }

    public string NameE { get; set; }

    public PmxBody RefBodyA { get; set; }

    public PmxBody RefBodyB { get; set; }

    public string NXName
    {
      get => this.Name;
      set => this.Name = value;
    }

    public PmxJoint()
    {
      this.Name = "";
      this.NameE = "";
      this.BodyA = -1;
      this.BodyB = -1;
    }

    public PmxJoint(PmxJoint joint, bool nonStr) => this.FromPmxJoint(joint, nonStr);

    public void FromPmxJoint(PmxJoint joint, bool nonStr)
    {
      if (!nonStr)
      {
        this.Name = joint.Name;
        this.NameE = joint.NameE;
      }
      this.Kind = joint.Kind;
      this.BodyA = joint.BodyA;
      this.BodyB = joint.BodyB;
      this.Position = joint.Position;
      this.Rotation = joint.Rotation;
      this.Limit_MoveLow = joint.Limit_MoveLow;
      this.Limit_MoveHigh = joint.Limit_MoveHigh;
      this.Limit_AngleLow = joint.Limit_AngleLow;
      this.Limit_AngleHigh = joint.Limit_AngleHigh;
      this.SpConst_Move = joint.SpConst_Move;
      this.SpConst_Rotate = joint.SpConst_Rotate;
    }

    public void ClearLimit()
    {
      this.Limit_AngleLow = Vector3.zero;
      this.Limit_AngleHigh = Vector3.zero;
      if (this.Kind == PmxJoint.JointKind.ConeTwist)
      {
        this.Limit_MoveLow.y = 0.0f;
        this.Limit_MoveHigh.y = 0.0f;
      }
      else
      {
        this.Limit_MoveLow = Vector3.zero;
        this.Limit_MoveHigh = Vector3.zero;
      }
    }

    public void ClearParameter()
    {
      switch (this.Kind)
      {
        case PmxJoint.JointKind.Sp6DOF:
        case PmxJoint.JointKind.G6DOF:
        case PmxJoint.JointKind.P2P:
        case PmxJoint.JointKind.Slider:
          this.SpConst_Move = Vector3.zero;
          this.SpConst_Rotate = Vector3.zero;
          break;
        case PmxJoint.JointKind.ConeTwist:
          this.Limit_MoveLow.x = 0.0f;
          this.Limit_MoveHigh.x = 1f;
          this.Limit_MoveLow.z = 0.0f;
          this.Limit_MoveHigh.z = 0.0f;
          this.SpConst_Move = new Vector3(1f, 0.3f, 1f);
          this.SpConst_Rotate = Vector3.zero;
          break;
        case PmxJoint.JointKind.Hinge:
          this.SpConst_Move = new Vector3(0.9f, 0.3f, 1f);
          this.SpConst_Rotate = Vector3.zero;
          break;
      }
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.Name = PmxStreamHelper.ReadString(s, f);
      this.NameE = PmxStreamHelper.ReadString(s, f);
      this.Kind = (PmxJoint.JointKind) s.ReadByte();
      this.BodyA = PmxStreamHelper.ReadElement_Int32(s, f.BodySize, true);
      this.BodyB = PmxStreamHelper.ReadElement_Int32(s, f.BodySize, true);
      this.Position = V3_BytesConvert.FromStream(s);
      this.Rotation = V3_BytesConvert.FromStream(s);
      this.Limit_MoveLow = V3_BytesConvert.FromStream(s);
      this.Limit_MoveHigh = V3_BytesConvert.FromStream(s);
      this.Limit_AngleLow = V3_BytesConvert.FromStream(s);
      this.Limit_AngleHigh = V3_BytesConvert.FromStream(s);
      this.SpConst_Move = V3_BytesConvert.FromStream(s);
      this.SpConst_Rotate = V3_BytesConvert.FromStream(s);
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      PmxStreamHelper.WriteString(s, this.Name, f);
      PmxStreamHelper.WriteString(s, this.NameE, f);
      if (this.Kind != PmxJoint.JointKind.Sp6DOF && (double) f.Ver < 2.09999990463257)
        s.WriteByte((byte) 0);
      else
        s.WriteByte((byte) this.Kind);
      PmxStreamHelper.WriteElement_Int32(s, this.BodyA, f.BodySize, true);
      PmxStreamHelper.WriteElement_Int32(s, this.BodyB, f.BodySize, true);
      V3_BytesConvert.ToStream(s, this.Position);
      V3_BytesConvert.ToStream(s, this.Rotation);
      V3_BytesConvert.ToStream(s, this.Limit_MoveLow);
      V3_BytesConvert.ToStream(s, this.Limit_MoveHigh);
      V3_BytesConvert.ToStream(s, this.Limit_AngleLow);
      V3_BytesConvert.ToStream(s, this.Limit_AngleHigh);
      V3_BytesConvert.ToStream(s, this.SpConst_Move);
      V3_BytesConvert.ToStream(s, this.SpConst_Rotate);
    }

    object ICloneable.Clone() => (object) new PmxJoint(this, false);

    public PmxJoint Clone() => new PmxJoint(this, false);

    public enum JointKind
    {
      Sp6DOF,
      G6DOF,
      P2P,
      ConeTwist,
      Slider,
      Hinge,
    }
  }
}
