// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxBody
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  public class PmxBody : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
  {
    public int Bone;
    public static string NullBoneName = "-";
    public int Group;
    public PmxBodyPassGroup PassGroup;
    public Vector3 BoxSize;
    public Vector3 Position;
    public Vector3 Rotation;
    public float Mass;
    public float PositionDamping;
    public float RotationDamping;
    public float Restitution;
    public float Friction;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Body;

    public string Name { get; set; }

    public string NameE { get; set; }

    public PmxBone RefBone { get; set; }

    public PmxBody.BoxKind BoxType { get; set; }

    public PmxBody.ModeType Mode { get; set; }

    public string NXName
    {
      get => this.Name;
      set => this.Name = value;
    }

    public PmxBody()
    {
      this.Name = "";
      this.NameE = "";
      this.PassGroup = new PmxBodyPassGroup();
      this.InitializeParameter();
      this.BoxSize = new Vector3(2f, 2f, 2f);
    }

    public PmxBody(PmxBody body, bool nonStr) => this.FromPmxBody(body, nonStr);

    public void FromPmxBody(PmxBody body, bool nonStr)
    {
      if (!nonStr)
      {
        this.Name = body.Name;
        this.NameE = body.NameE;
      }
      this.Bone = body.Bone;
      this.Group = body.Group;
      this.PassGroup = body.PassGroup.Clone();
      this.BoxType = body.BoxType;
      this.BoxSize = body.BoxSize;
      this.Position = body.Position;
      this.Rotation = body.Rotation;
      this.Mass = body.Mass;
      this.PositionDamping = body.PositionDamping;
      this.RotationDamping = body.RotationDamping;
      this.Restitution = body.Restitution;
      this.Friction = body.Friction;
      this.Mode = body.Mode;
    }

    public void InitializeParameter()
    {
      this.Mass = 1f;
      this.PositionDamping = 0.5f;
      this.RotationDamping = 0.5f;
      this.Restitution = 0.0f;
      this.Friction = 0.5f;
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.Name = PmxStreamHelper.ReadString(s, f);
      this.NameE = PmxStreamHelper.ReadString(s, f);
      this.Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
      this.Group = PmxStreamHelper.ReadElement_Int32(s, 1, true);
      this.PassGroup.FromFlagBits((ushort) PmxStreamHelper.ReadElement_Int32(s, 2, false));
      this.BoxType = (PmxBody.BoxKind) s.ReadByte();
      this.BoxSize = V3_BytesConvert.FromStream(s);
      this.Position = V3_BytesConvert.FromStream(s);
      this.Rotation = V3_BytesConvert.FromStream(s);
      this.Mass = PmxStreamHelper.ReadElement_Float(s);
      Vector4 vector4 = V4_BytesConvert.FromStream(s);
      this.PositionDamping = vector4.x;
      this.RotationDamping = vector4.y;
      this.Restitution = vector4.z;
      this.Friction = vector4.w;
      this.Mode = (PmxBody.ModeType) s.ReadByte();
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      PmxStreamHelper.WriteString(s, this.Name, f);
      PmxStreamHelper.WriteString(s, this.NameE, f);
      PmxStreamHelper.WriteElement_Int32(s, this.Bone, f.BoneSize, true);
      PmxStreamHelper.WriteElement_Int32(s, this.Group, 1, true);
      PmxStreamHelper.WriteElement_Int32(s, (int) this.PassGroup.ToFlagBits(), 2, false);
      s.WriteByte((byte) this.BoxType);
      V3_BytesConvert.ToStream(s, this.BoxSize);
      V3_BytesConvert.ToStream(s, this.Position);
      V3_BytesConvert.ToStream(s, this.Rotation);
      PmxStreamHelper.WriteElement_Float(s, this.Mass);
      V4_BytesConvert.ToStream(s, new Vector4(this.PositionDamping, this.RotationDamping, this.Restitution, this.Friction));
      s.WriteByte((byte) this.Mode);
    }

    object ICloneable.Clone() => (object) new PmxBody(this, false);

    public PmxBody Clone() => new PmxBody(this, false);

    public enum BoxKind
    {
      Sphere,
      Box,
      Capsule,
    }

    public enum ModeType
    {
      Static,
      Dynamic,
      DynamicWithBone,
    }
  }
}
