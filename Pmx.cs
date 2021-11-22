// Decompiled with JetBrains decompiler
// Type: PmxLib.Pmx
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  public class Pmx : IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    public const string RootNodeName = "Root";
    public const string ExpNodeName = "表情";

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Pmx;

    public PmxHeader Header { get; set; }

    public PmxModelInfo ModelInfo { get; set; }

    public List<PmxVertex> VertexList { get; set; }

    public List<int> FaceList { get; set; }

    public List<PmxMaterial> MaterialList { get; set; }

    public List<PmxBone> BoneList { get; set; }

    public List<PmxMorph> MorphList { get; set; }

    public List<PmxNode> NodeList { get; set; }

    public List<PmxBody> BodyList { get; set; }

    public List<PmxJoint> JointList { get; set; }

    public List<PmxSoftBody> SoftBodyList { get; set; }

    public PmxNode RootNode { get; set; }

    public PmxNode ExpNode { get; set; }

    public string FilePath { get; set; }

    public static PmxSaveVersion SaveVersion { get; set; }

    public static bool AutoSelect_UVACount { get; set; }

    public Pmx()
    {
      if (PmxLibClass.IsLocked())
        return;
      this.Header = new PmxHeader(2.1f);
      this.ModelInfo = new PmxModelInfo();
      this.VertexList = new List<PmxVertex>();
      this.FaceList = new List<int>();
      this.MaterialList = new List<PmxMaterial>();
      this.BoneList = new List<PmxBone>();
      this.MorphList = new List<PmxMorph>();
      this.NodeList = new List<PmxNode>();
      this.BodyList = new List<PmxBody>();
      this.JointList = new List<PmxJoint>();
      this.SoftBodyList = new List<PmxSoftBody>();
      this.RootNode = new PmxNode();
      this.ExpNode = new PmxNode();
      this.InitializeSystemNode();
      this.FilePath = "";
    }

    static Pmx()
    {
      Pmx.SaveVersion = PmxSaveVersion.AutoSelect;
      Pmx.AutoSelect_UVACount = true;
    }

    public Pmx(Pmx pmx)
      : this()
    {
      this.FromPmx(pmx);
    }

    public Pmx(string path)
      : this()
    {
      this.FromFile(path);
    }

    public virtual void Clear()
    {
      this.Header.ElementFormat.Ver = 2.1f;
      this.Header.ElementFormat.UVACount = 0;
      this.ModelInfo.Clear();
      this.VertexList.Clear();
      this.FaceList.Clear();
      this.MaterialList.Clear();
      this.BoneList.Clear();
      this.MorphList.Clear();
      this.BodyList.Clear();
      this.JointList.Clear();
      this.SoftBodyList.Clear();
      this.InitializeSystemNode();
      this.FilePath = "";
    }

    public void Initialize()
    {
      this.Clear();
      this.InitializeBone();
    }

    public void InitializeBone()
    {
      this.BoneList.Clear();
      PmxBone pmxBone = new PmxBone();
      pmxBone.Name = "センター";
      pmxBone.NameE = "center";
      pmxBone.Parent = -1;
      pmxBone.SetFlag(PmxBone.BoneFlags.Translation, true);
      this.BoneList.Add(pmxBone);
    }

    public void InitializeSystemNode()
    {
      this.RootNode.Name = "Root";
      this.RootNode.NameE = "Root";
      this.RootNode.SystemNode = true;
      this.RootNode.ElementList.Clear();
      this.RootNode.ElementList.Add(new PmxNode.NodeElement()
      {
        ElementType = PmxNode.ElementType.Bone,
        Index = 0
      });
      this.ExpNode.Name = "表情";
      this.ExpNode.NameE = "Exp";
      this.ExpNode.SystemNode = true;
      this.ExpNode.ElementList.Clear();
      this.NodeList.Clear();
      this.NodeList.Add(this.RootNode);
      this.NodeList.Add(this.ExpNode);
    }

    public void UpdateSystemNode()
    {
      for (int index = 0; index < this.NodeList.Count; ++index)
      {
        if (this.NodeList[index].SystemNode)
        {
          if (this.NodeList[index].Name == "Root")
            this.RootNode = this.NodeList[index];
          else if (this.NodeList[index].Name == "表情")
            this.ExpNode = this.NodeList[index];
        }
      }
    }

    public bool FromFile(string path)
    {
      bool flag = false;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        try
        {
          this.FromStreamEx((Stream) fileStream, (PmxElementFormat) null);
          flag = true;
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
      }
      this.FilePath = path;
      return flag;
    }

    public bool ToFile(string path)
    {
      bool flag = false;
      using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
      {
        try
        {
          this.NormalizeVersion();
          if (!Pmx.AutoSelect_UVACount)
            ;
          this.ToStreamEx((Stream) fileStream, (PmxElementFormat) null);
          flag = true;
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
          throw new Exception("保存中にエラーが発生しました." + (object) ex);
        }
      }
      this.FilePath = path;
      return flag;
    }

    public void FromPmx(Pmx pmx)
    {
      this.Clear();
      this.FilePath = pmx.FilePath;
      this.Header = pmx.Header.Clone();
      this.ModelInfo = pmx.ModelInfo.Clone();
      int count1 = pmx.VertexList.Count;
      this.VertexList.Capacity = count1;
      for (int index = 0; index < count1; ++index)
        this.VertexList.Add(pmx.VertexList[index].Clone());
      int count2 = pmx.FaceList.Count;
      this.FaceList.Capacity = count2;
      for (int index = 0; index < count2; ++index)
        this.FaceList.Add(pmx.FaceList[index]);
      int count3 = pmx.MaterialList.Count;
      this.MaterialList.Capacity = count3;
      for (int index = 0; index < count3; ++index)
        this.MaterialList.Add(pmx.MaterialList[index].Clone());
      int count4 = pmx.BoneList.Count;
      this.BoneList.Capacity = count4;
      for (int index = 0; index < count4; ++index)
        this.BoneList.Add(pmx.BoneList[index].Clone());
      int count5 = pmx.MorphList.Count;
      this.MorphList.Capacity = count5;
      for (int index = 0; index < count5; ++index)
        this.MorphList.Add(pmx.MorphList[index].Clone());
      int count6 = pmx.NodeList.Count;
      this.NodeList.Clear();
      this.NodeList.Capacity = count6;
      for (int index = 0; index < count6; ++index)
      {
        this.NodeList.Add(pmx.NodeList[index].Clone());
        if (this.NodeList[index].SystemNode)
        {
          if (this.NodeList[index].Name == "Root")
            this.RootNode = this.NodeList[index];
          else if (this.NodeList[index].Name == "表情")
            this.ExpNode = this.NodeList[index];
        }
      }
      int count7 = pmx.BodyList.Count;
      this.BodyList.Capacity = count7;
      for (int index = 0; index < count7; ++index)
        this.BodyList.Add(pmx.BodyList[index].Clone());
      int count8 = pmx.JointList.Count;
      this.JointList.Capacity = count8;
      for (int index = 0; index < count8; ++index)
        this.JointList.Add(pmx.JointList[index].Clone());
      int count9 = pmx.SoftBodyList.Count;
      this.SoftBodyList.Capacity = count9;
      for (int index = 0; index < count9; ++index)
        this.SoftBodyList.Add(pmx.SoftBodyList[index].Clone());
    }

    public virtual void FromStreamEx(Stream s, PmxElementFormat f)
    {
      PmxHeader h = new PmxHeader(2.1f);
      h.FromStreamEx(s, (PmxElementFormat) null);
      this.Header.FromHeader(h);
      this.ModelInfo.FromStreamEx(s, h.ElementFormat);
      int num1 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.VertexList.Clear();
      this.VertexList.Capacity = num1;
      for (int index = 0; index < num1; ++index)
      {
        PmxVertex pmxVertex = new PmxVertex();
        pmxVertex.FromStreamEx(s, h.ElementFormat);
        this.VertexList.Add(pmxVertex);
      }
      int num2 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.FaceList.Clear();
      this.FaceList.Capacity = num2;
      for (int index = 0; index < num2; ++index)
        this.FaceList.Add(PmxStreamHelper.ReadElement_Int32(s, h.ElementFormat.VertexSize, false));
      PmxTextureTable tx = new PmxTextureTable();
      tx.FromStreamEx(s, h.ElementFormat);
      int num3 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.MaterialList.Clear();
      this.MaterialList.Capacity = num3;
      for (int index = 0; index < num3; ++index)
      {
        PmxMaterial pmxMaterial = new PmxMaterial();
        pmxMaterial.FromStreamEx_TexTable(s, tx, h.ElementFormat);
        this.MaterialList.Add(pmxMaterial);
      }
      int num4 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.BoneList.Clear();
      this.BoneList.Capacity = num4;
      for (int index = 0; index < num4; ++index)
      {
        PmxBone pmxBone = new PmxBone();
        pmxBone.FromStreamEx(s, h.ElementFormat);
        this.BoneList.Add(pmxBone);
      }
      int num5 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.MorphList.Clear();
      this.MorphList.Capacity = num5;
      for (int index = 0; index < num5; ++index)
      {
        PmxMorph pmxMorph = new PmxMorph();
        pmxMorph.FromStreamEx(s, h.ElementFormat);
        this.MorphList.Add(pmxMorph);
      }
      int num6 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.NodeList.Clear();
      this.NodeList.Capacity = num6;
      for (int index = 0; index < num6; ++index)
      {
        PmxNode pmxNode = new PmxNode();
        pmxNode.FromStreamEx(s, h.ElementFormat);
        this.NodeList.Add(pmxNode);
        if (this.NodeList[index].SystemNode)
        {
          if (this.NodeList[index].Name == "Root")
            this.RootNode = this.NodeList[index];
          else if (this.NodeList[index].Name == "表情")
            this.ExpNode = this.NodeList[index];
        }
      }
      int num7 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.BodyList.Clear();
      this.BodyList.Capacity = num7;
      for (int index = 0; index < num7; ++index)
      {
        PmxBody pmxBody = new PmxBody();
        pmxBody.FromStreamEx(s, h.ElementFormat);
        this.BodyList.Add(pmxBody);
      }
      int num8 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.JointList.Clear();
      this.JointList.Capacity = num8;
      for (int index = 0; index < num8; ++index)
      {
        PmxJoint pmxJoint = new PmxJoint();
        pmxJoint.FromStreamEx(s, h.ElementFormat);
        this.JointList.Add(pmxJoint);
      }
      if ((double) h.Ver < 2.09999990463257)
        return;
      int num9 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.SoftBodyList.Clear();
      this.SoftBodyList.Capacity = num9;
      for (int index = 0; index < num9; ++index)
      {
        PmxSoftBody pmxSoftBody = new PmxSoftBody();
        pmxSoftBody.FromStreamEx(s, h.ElementFormat);
        this.SoftBodyList.Add(pmxSoftBody);
      }
    }

    public void UpdateElementFormatSize(PmxElementFormat f, PmxTextureTable tx)
    {
      if (f == null)
        f = this.Header.ElementFormat;
      f.VertexSize = PmxElementFormat.GetUnsignedBufSize(this.VertexList.Count);
      f.MaterialSize = PmxElementFormat.GetSignedBufSize(this.MaterialList.Count);
      f.BoneSize = PmxElementFormat.GetSignedBufSize(this.BoneList.Count);
      f.MorphSize = PmxElementFormat.GetSignedBufSize(this.MorphList.Count);
      f.BodySize = PmxElementFormat.GetSignedBufSize(this.BodyList.Count);
      if (tx == null)
        tx = new PmxTextureTable(this.MaterialList);
      f.TexSize = PmxElementFormat.GetSignedBufSize(tx.Count);
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      PmxHeader header = this.Header;
      PmxTextureTable tx = new PmxTextureTable(this.MaterialList);
      this.UpdateElementFormatSize(header.ElementFormat, tx);
      header.ToStreamEx(s, (PmxElementFormat) null);
      this.ModelInfo.ToStreamEx(s, header.ElementFormat);
      PmxStreamHelper.WriteElement_Int32(s, this.VertexList.Count, 4, true);
      for (int index = 0; index < this.VertexList.Count; ++index)
        this.VertexList[index].ToStreamEx(s, header.ElementFormat);
      PmxStreamHelper.WriteElement_Int32(s, this.FaceList.Count, 4, true);
      for (int index = 0; index < this.FaceList.Count; ++index)
        PmxStreamHelper.WriteElement_Int32(s, this.FaceList[index], header.ElementFormat.VertexSize, false);
      tx.ToStreamEx(s, header.ElementFormat);
      PmxStreamHelper.WriteElement_Int32(s, this.MaterialList.Count, 4, true);
      for (int index = 0; index < this.MaterialList.Count; ++index)
        this.MaterialList[index].ToStreamEx_TexTable(s, tx, header.ElementFormat);
      PmxStreamHelper.WriteElement_Int32(s, this.BoneList.Count, 4, true);
      for (int index = 0; index < this.BoneList.Count; ++index)
        this.BoneList[index].ToStreamEx(s, header.ElementFormat);
      PmxStreamHelper.WriteElement_Int32(s, this.MorphList.Count, 4, true);
      for (int index = 0; index < this.MorphList.Count; ++index)
        this.MorphList[index].ToStreamEx(s, header.ElementFormat);
      PmxStreamHelper.WriteElement_Int32(s, this.NodeList.Count, 4, true);
      for (int index = 0; index < this.NodeList.Count; ++index)
        this.NodeList[index].ToStreamEx(s, header.ElementFormat);
      PmxStreamHelper.WriteElement_Int32(s, this.BodyList.Count, 4, true);
      for (int index = 0; index < this.BodyList.Count; ++index)
        this.BodyList[index].ToStreamEx(s, header.ElementFormat);
      PmxStreamHelper.WriteElement_Int32(s, this.JointList.Count, 4, true);
      for (int index = 0; index < this.JointList.Count; ++index)
        this.JointList[index].ToStreamEx(s, header.ElementFormat);
      if ((double) header.Ver < 2.09999990463257)
        return;
      PmxStreamHelper.WriteElement_Int32(s, this.SoftBodyList.Count, 4, true);
      for (int index = 0; index < this.SoftBodyList.Count; ++index)
        this.SoftBodyList[index].ToStreamEx(s, header.ElementFormat);
    }

    public void ClearMaterialNames()
    {
      for (int index = 0; index < this.MaterialList.Count; ++index)
        this.MaterialList[index].Name = "材質" + (index + 1).ToString();
    }

    public static void UpdateBoneIKKind(List<PmxBone> boneList)
    {
      for (int index = 0; index < boneList.Count; ++index)
        boneList[index].IKKind = PmxBone.IKKindType.None;
      for (int index1 = 0; index1 < boneList.Count; ++index1)
      {
        PmxBone bone1 = boneList[index1];
        if (bone1.GetFlag(PmxBone.BoneFlags.IK))
        {
          bone1.IKKind = PmxBone.IKKindType.IK;
          int target = bone1.IK.Target;
          if (CP.InRange<PmxBone>(boneList, target))
            boneList[target].IKKind = PmxBone.IKKindType.Target;
          for (int index2 = 0; index2 < bone1.IK.LinkList.Count; ++index2)
          {
            int bone2 = bone1.IK.LinkList[index2].Bone;
            if (CP.InRange<PmxBone>(boneList, bone2))
              boneList[bone2].IKKind = PmxBone.IKKindType.Link;
          }
        }
      }
    }

    public void UpdateBoneIKKind() => Pmx.UpdateBoneIKKind(this.BoneList);

    public void NormalizeVertex_SDEF_C0()
    {
      for (int index = 0; index < this.VertexList.Count; ++index)
        this.VertexList[index].NormalizeSDEF_C0(this.BoneList);
    }

    public float RequireVersion(
      out bool isQDEF,
      out bool isExMorph,
      out bool isExJoint,
      out bool isSoftBody)
    {
      Func<bool> func1 = (Func<bool>) (() =>
      {
        bool flag = false;
        for (int index = 0; index < this.VertexList.Count; ++index)
        {
          if (this.VertexList[index].Deform == PmxVertex.DeformType.QDEF)
          {
            flag = true;
            break;
          }
        }
        return flag;
      });
      Func<bool> func2 = (Func<bool>) (() =>
      {
        bool flag = false;
        for (int index = 0; index < this.MorphList.Count; ++index)
        {
          PmxMorph morph = this.MorphList[index];
          if (morph.IsFlip || morph.IsImpulse)
          {
            flag = true;
            break;
          }
        }
        return flag;
      });
      Func<bool> func3 = (Func<bool>) (() =>
      {
        bool flag = false;
        for (int index = 0; index < this.JointList.Count; ++index)
        {
          if ((uint) this.JointList[index].Kind > 0U)
          {
            flag = true;
            break;
          }
        }
        return flag;
      });
      Func<bool> func4 = (Func<bool>) (() => this.SoftBodyList.Count > 0);
      isQDEF = func1();
      isExMorph = func2();
      isExJoint = func3();
      isSoftBody = func4();
      float num = 2f;
      if (isQDEF | isExMorph | isExJoint | isSoftBody)
        num = 2.1f;
      return num;
    }

    private void NormalizeVersion()
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      float num = 2f;
      switch (Pmx.SaveVersion)
      {
        case PmxSaveVersion.AutoSelect:
          this.Header.Ver = num;
          break;
        case PmxSaveVersion.PMX2_0:
          string str1 = "";
          if (flag1)
            str1 = str1 + "頂点ウェイト : QDEF -> BDEF4" + Environment.NewLine;
          if (flag2)
            str1 = str1 + "モーフ : インパルス->削除／フリップ->グループ" + Environment.NewLine;
          if (flag3)
            str1 = str1 + "Joint : 拡張Joint -> 基本Joint(ﾊﾞﾈ付6DOF)" + Environment.NewLine;
          if (flag4)
            str1 = str1 + "SoftBody : 削除" + Environment.NewLine;
          this.Header.Ver = 2f;
          if (str1.Length <= 0)
            break;
          string str2 = "PMX2.0での保存では 以下の項目が書き換えられますが よろしいですか?" + Environment.NewLine + Environment.NewLine + str1;
          break;
        case PmxSaveVersion.PMX2_1:
          this.Header.Ver = 2.1f;
          break;
      }
    }

    public void NormalizeUVACount()
    {
      if (this.VertexList.Count <= 0)
      {
        this.Header.ElementFormat.UVACount = 0;
      }
      else
      {
        Func<Vector4, bool> func = (Func<Vector4, bool>) (v => (double) Math.Abs(v.x) > 9.99999996004197E-13 || (double) Math.Abs(v.y) > 9.99999996004197E-13 || (double) Math.Abs(v.z) > 9.99999996004197E-13 || (double) Math.Abs(v.w) > 9.99999996004197E-13);
        int num1 = 0;
        foreach (PmxVertex vertex in this.VertexList)
        {
          for (int index = 0; index < vertex.UVA.Length; ++index)
          {
            if (func(vertex.UVA[index]))
            {
              int num2 = index + 1;
              if (num1 < num2)
                num1 = num2;
            }
          }
        }
        this.Header.ElementFormat.UVACount = num1;
      }
    }

    object ICloneable.Clone() => (object) new Pmx(this);

    public virtual Pmx Clone() => new Pmx(this);
  }
}
