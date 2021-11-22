// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxVertex
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  public class PmxVertex : IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    public const int MaxUVACount = 4;
    public const int MaxWeightBoneCount = 4;
    public Vector3 Position;
    public Vector3 Normal;
    public Vector2 UV;
    public Vector4[] UVA;
    public PmxVertex.BoneWeight[] Weight;
    public PmxVertex.DeformType Deform;
    public float EdgeScale = 1f;
    public int VertexMorphIndex = -1;
    public int UVMorphIndex = -1;
    public int[] UVAMorphIndex;
    public int SDEFIndex = -1;
    public int QDEFIndex = -1;
    public int SoftBodyPosIndex = -1;
    public int SoftBodyNormalIndex = -1;
    public bool SDEF;
    public Vector3 C0;
    public Vector3 R0;
    public Vector3 R1;
    public Vector3 RW0;
    public Vector3 RW1;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Vertex;

    public int NWeight
    {
      get => (int) (((double) this.Weight[0].Value + 0.00499999988824129) * 100.0);
      set
      {
        this.ClearWeightValue();
        this.Weight[0].Value = (float) value * 0.01f;
        this.Weight[1].Value = 1f - this.Weight[0].Value;
        this.UpdateDeformType();
        if (this.Deform != PmxVertex.DeformType.BDEF4 && this.Deform != PmxVertex.DeformType.QDEF)
          return;
        this.Deform = PmxVertex.DeformType.BDEF2;
      }
    }

    public PmxVertex()
    {
      this.UVAMorphIndex = new int[4];
      this.Weight = new PmxVertex.BoneWeight[4];
      this.UVA = new Vector4[4];
      this.VertexMorphIndex = -1;
      this.UVMorphIndex = -1;
      for (int index = 0; index < this.UVAMorphIndex.Length; ++index)
        this.UVAMorphIndex[index] = -1;
      this.SDEFIndex = -1;
      this.QDEFIndex = -1;
      this.SoftBodyPosIndex = -1;
      this.SoftBodyNormalIndex = -1;
      this.ClearWeight();
    }

    public void ClearWeight()
    {
      this.ClearWeightBone();
      this.ClearWeightValue();
      this.Deform = PmxVertex.DeformType.BDEF1;
      this.SDEF = false;
    }

    public void ClearWeightBone()
    {
      this.Weight[0].Bone = 0;
      for (int index = 1; index < 4; ++index)
        this.Weight[index].Bone = -1;
    }

    public void ClearWeightValue()
    {
      this.Weight[0].Value = 1f;
      for (int index = 1; index < 4; ++index)
        this.Weight[index].Value = 0.0f;
    }

    public void NormalizeWeight(bool bdef4Sum)
    {
      for (int index = 0; index < 4; ++index)
      {
        if (this.Weight[index].Bone < 0)
          this.Weight[index].Value = 0.0f;
      }
      if (this.Deform == PmxVertex.DeformType.SDEF)
        this.NormalizeWeightOrder_SDEF();
      else
        this.NormalizeWeightOrder();
      this.UpdateDeformType();
      if (this.Deform == PmxVertex.DeformType.SDEF)
      {
        this.Weight[2].Bone = -1;
        this.Weight[2].Value = 0.0f;
        this.Weight[3].Bone = -1;
        this.Weight[3].Value = 0.0f;
      }
      this.NormalizeWeightSum(bdef4Sum);
      int num = 1;
      switch (this.Deform)
      {
        case PmxVertex.DeformType.BDEF2:
        case PmxVertex.DeformType.SDEF:
          num = 2;
          break;
        case PmxVertex.DeformType.BDEF4:
        case PmxVertex.DeformType.QDEF:
          num = 4;
          break;
      }
      for (int index = 0; index < num; ++index)
      {
        if (this.Weight[index].Bone < 0)
        {
          this.Weight[index].Bone = 0;
          this.Weight[index].Value = 0.0f;
        }
      }
      this.UpdateDeformType();
    }

    public void NormalizeWeightOrder()
    {
      PmxVertex.BoneWeight[] boneWeightArray = PmxVertex.BoneWeight.Sort(this.Weight);
      for (int index = 0; index < boneWeightArray.Length; ++index)
        this.Weight[index] = boneWeightArray[index];
    }

    public void NormalizeWeightOrder_BDEF2()
    {
      if ((double) this.Weight[0].Value >= (double) this.Weight[1].Value)
        return;
      CP.Swap<PmxVertex.BoneWeight>(ref this.Weight[0], ref this.Weight[1]);
    }

    public void NormalizeWeightOrder_SDEF()
    {
      if (this.Weight[0].Bone <= this.Weight[1].Bone)
        return;
      CP.Swap<PmxVertex.BoneWeight>(ref this.Weight[0], ref this.Weight[1]);
      CP.Swap<Vector3>(ref this.R0, ref this.R1);
      CP.Swap<Vector3>(ref this.RW0, ref this.RW0);
    }

    public void NormalizeWeightSum(bool bdef4)
    {
      if (!bdef4 && (this.Deform == PmxVertex.DeformType.BDEF4 || this.Deform == PmxVertex.DeformType.QDEF))
        return;
      float num1 = 0.0f;
      for (int index = 0; index < 4; ++index)
        num1 += this.Weight[index].Value;
      if ((double) num1 != 0.0 && (double) num1 != 1.0)
      {
        float num2 = 1f / num1;
        for (int index = 0; index < 4; ++index)
          this.Weight[index].Value *= num2;
      }
    }

    public void NormalizeWeightSum_BDEF2()
    {
      float num1 = this.Weight[0].Value + this.Weight[1].Value;
      if ((double) num1 == 1.0)
        return;
      if ((double) num1 == 0.0)
      {
        this.Weight[0].Value = 1f;
        this.Weight[1].Value = 0.0f;
      }
      else
      {
        float num2 = 1f / num1;
        this.Weight[0].Value *= num2;
        this.Weight[1].Value *= num2;
      }
    }

    public PmxVertex.DeformType GetDeformType()
    {
      int num = 0;
      for (int index = 0; index < 4; ++index)
      {
        if ((double) this.Weight[index].Value != 0.0)
          ++num;
      }
      PmxVertex.DeformType deformType1;
      if (this.SDEF && num != 1)
        deformType1 = PmxVertex.DeformType.SDEF;
      else if (this.Deform == PmxVertex.DeformType.QDEF && num != 1)
      {
        deformType1 = PmxVertex.DeformType.QDEF;
      }
      else
      {
        PmxVertex.DeformType deformType2 = PmxVertex.DeformType.BDEF1;
        switch (num)
        {
          case 0:
          case 1:
            deformType2 = PmxVertex.DeformType.BDEF1;
            break;
          case 2:
            deformType2 = PmxVertex.DeformType.BDEF2;
            break;
          case 3:
          case 4:
            deformType2 = PmxVertex.DeformType.BDEF4;
            break;
        }
        deformType1 = deformType2;
      }
      return deformType1;
    }

    public void UpdateDeformType() => this.Deform = this.GetDeformType();

    public void SetSDEF_RV(Vector3 r0, Vector3 r1)
    {
      this.R0 = r0;
      this.R1 = r1;
      this.CalcSDEF_RW();
    }

    public void CalcSDEF_RW()
    {
      Vector3 vector3 = this.Weight[0].Value * this.R0 + this.Weight[1].Value * this.R1;
      this.RW0 = this.R0 - vector3;
      this.RW1 = this.R1 - vector3;
    }

    public bool NormalizeSDEF_C0(List<PmxBone> boneList)
    {
      bool flag;
      if (this.Deform != PmxVertex.DeformType.SDEF)
      {
        flag = true;
      }
      else
      {
        int bone1 = this.Weight[0].Bone;
        int bone2 = this.Weight[1].Bone;
        if (!CP.InRange<PmxBone>(boneList, bone1))
        {
          flag = false;
        }
        else
        {
          PmxBone bone3 = boneList[bone1];
          if (CP.InRange<PmxBone>(boneList, bone2))
          {
            PmxBone bone4 = boneList[bone2];
            Vector3 position = bone3.Position;
            Vector3 lhs = bone4.Position - position;
            lhs.Normalize();
            Vector3 rhs = this.Position - position;
            float num = Vector3.Dot(lhs, rhs);
            this.C0 = lhs * num + position;
            flag = true;
          }
          else
            flag = false;
        }
      }
      return flag;
    }

    public bool IsSDEF_EnableBone(List<PmxBone> boneList)
    {
      int bone1 = this.Weight[0].Bone;
      int bone2 = this.Weight[1].Bone;
      bool flag;
      if (!CP.InRange<PmxBone>(boneList, bone1))
      {
        flag = false;
      }
      else
      {
        PmxBone bone3 = boneList[bone1];
        if (CP.InRange<PmxBone>(boneList, bone2))
        {
          PmxBone bone4 = boneList[bone2];
          flag = bone3.Parent == bone2 || bone4.Parent == bone1;
        }
        else
          flag = false;
      }
      return flag;
    }

    public PmxVertex(PmxVertex vertex)
      : this()
    {
      this.FromPmxVertex(vertex);
    }

    public void FromPmxVertex(PmxVertex vertex)
    {
      this.Position = vertex.Position;
      this.Normal = vertex.Normal;
      this.UV = vertex.UV;
      for (int index = 0; index < 4; ++index)
        this.UVA[index] = vertex.UVA[index];
      for (int index = 0; index < 4; ++index)
      {
        this.Weight[index] = vertex.Weight[index];
        this.Weight[index].RefBone = (PmxBone) null;
      }
      this.EdgeScale = vertex.EdgeScale;
      this.Deform = vertex.Deform;
      this.SDEF = vertex.SDEF;
      this.C0 = vertex.C0;
      this.R0 = vertex.R0;
      this.R1 = vertex.R1;
      this.RW0 = vertex.RW0;
      this.RW1 = vertex.RW1;
      this.VertexMorphIndex = vertex.VertexMorphIndex;
      this.UVMorphIndex = vertex.UVMorphIndex;
      for (int index = 0; index < this.UVAMorphIndex.Length; ++index)
        this.UVAMorphIndex[index] = vertex.UVAMorphIndex[index];
      this.SDEFIndex = vertex.SDEFIndex;
      this.QDEFIndex = vertex.QDEFIndex;
      this.SoftBodyPosIndex = vertex.SoftBodyPosIndex;
      this.SoftBodyNormalIndex = vertex.SoftBodyNormalIndex;
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.Position = V3_BytesConvert.FromStream(s);
      this.Normal = V3_BytesConvert.FromStream(s);
      this.UV = V2_BytesConvert.FromStream(s);
      for (int index = 0; index < f.UVACount; ++index)
      {
        Vector4 vector4 = V4_BytesConvert.FromStream(s);
        if (0 <= index && index < this.UVA.Length)
          this.UVA[index] = vector4;
      }
      this.Deform = (PmxVertex.DeformType) s.ReadByte();
      this.SDEF = false;
      switch (this.Deform)
      {
        case PmxVertex.DeformType.BDEF1:
          this.Weight[0].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[0].Value = 1f;
          break;
        case PmxVertex.DeformType.BDEF2:
          this.Weight[0].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[1].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[0].Value = PmxStreamHelper.ReadElement_Float(s);
          this.Weight[1].Value = 1f - this.Weight[0].Value;
          break;
        case PmxVertex.DeformType.BDEF4:
        case PmxVertex.DeformType.QDEF:
          this.Weight[0].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[1].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[2].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[3].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[0].Value = PmxStreamHelper.ReadElement_Float(s);
          this.Weight[1].Value = PmxStreamHelper.ReadElement_Float(s);
          this.Weight[2].Value = PmxStreamHelper.ReadElement_Float(s);
          this.Weight[3].Value = PmxStreamHelper.ReadElement_Float(s);
          break;
        case PmxVertex.DeformType.SDEF:
          this.Weight[0].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[1].Bone = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
          this.Weight[0].Value = PmxStreamHelper.ReadElement_Float(s);
          this.Weight[1].Value = 1f - this.Weight[0].Value;
          this.C0 = V3_BytesConvert.FromStream(s);
          this.R0 = V3_BytesConvert.FromStream(s);
          this.R1 = V3_BytesConvert.FromStream(s);
          this.CalcSDEF_RW();
          this.SDEF = true;
          break;
      }
      this.EdgeScale = PmxStreamHelper.ReadElement_Float(s);
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      V3_BytesConvert.ToStream(s, this.Position);
      V3_BytesConvert.ToStream(s, this.Normal);
      V2_BytesConvert.ToStream(s, this.UV);
      for (int index = 0; index < f.UVACount; ++index)
        V4_BytesConvert.ToStream(s, this.UVA[index]);
      if (this.Deform == PmxVertex.DeformType.QDEF && (double) f.Ver < 2.09999990463257)
        s.WriteByte((byte) 2);
      else
        s.WriteByte((byte) this.Deform);
      switch (this.Deform)
      {
        case PmxVertex.DeformType.BDEF1:
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[0].Bone, f.BoneSize, true);
          break;
        case PmxVertex.DeformType.BDEF2:
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[0].Bone, f.BoneSize, true);
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[1].Bone, f.BoneSize, true);
          PmxStreamHelper.WriteElement_Float(s, this.Weight[0].Value);
          break;
        case PmxVertex.DeformType.BDEF4:
        case PmxVertex.DeformType.QDEF:
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[0].Bone, f.BoneSize, true);
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[1].Bone, f.BoneSize, true);
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[2].Bone, f.BoneSize, true);
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[3].Bone, f.BoneSize, true);
          PmxStreamHelper.WriteElement_Float(s, this.Weight[0].Value);
          PmxStreamHelper.WriteElement_Float(s, this.Weight[1].Value);
          PmxStreamHelper.WriteElement_Float(s, this.Weight[2].Value);
          PmxStreamHelper.WriteElement_Float(s, this.Weight[3].Value);
          break;
        case PmxVertex.DeformType.SDEF:
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[0].Bone, f.BoneSize, true);
          PmxStreamHelper.WriteElement_Int32(s, this.Weight[1].Bone, f.BoneSize, true);
          PmxStreamHelper.WriteElement_Float(s, this.Weight[0].Value);
          V3_BytesConvert.ToStream(s, this.C0);
          V3_BytesConvert.ToStream(s, this.R0);
          V3_BytesConvert.ToStream(s, this.R1);
          break;
      }
      PmxStreamHelper.WriteElement_Float(s, this.EdgeScale);
    }

    object ICloneable.Clone() => (object) new PmxVertex(this);

    public PmxVertex Clone() => new PmxVertex(this);

    public struct BoneWeight
    {
      public int Bone;
      public float Value;

      public PmxBone RefBone { get; set; }

      public static PmxVertex.BoneWeight[] Sort(PmxVertex.BoneWeight[] w)
      {
        List<PmxVertex.BoneWeight> list = new List<PmxVertex.BoneWeight>((IEnumerable<PmxVertex.BoneWeight>) w);
        PmxVertex.BoneWeight.SortList(list);
        return list.ToArray();
      }

      public static void SortList(List<PmxVertex.BoneWeight> list) => CP.SSort<PmxVertex.BoneWeight>(list, (Comparison<PmxVertex.BoneWeight>) ((l, r) =>
      {
        float num = Math.Abs(r.Value) - Math.Abs(l.Value);
        return (double) num >= 0.0 ? ((double) num > 0.0 ? 1 : 0) : -1;
      }));
    }

    public enum DeformType
    {
      BDEF1,
      BDEF2,
      BDEF4,
      SDEF,
      QDEF,
    }
  }
}
