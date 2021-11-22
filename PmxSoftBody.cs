// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxSoftBody
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  public class PmxSoftBody : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
  {
    public PmxSoftBody.ShapeKind Shape;
    public int Material;
    public int Group;
    public PmxBodyPassGroup PassGroup;
    public PmxSoftBody.SoftBodyFlags Flags;
    public int BendingLinkDistance;
    public int ClusterCount;
    public float TotalMass;
    public float Margin;
    public PmxSoftBody.SoftBodyConfig Config;
    public PmxSoftBody.SoftBodyMaterialConfig MaterialConfig;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.SoftBody;

    public string Name { get; set; }

    public string NameE { get; set; }

    public PmxMaterial RefMaterial { get; set; }

    public bool IsGenerateBendingLinks
    {
      get => (this.Flags & PmxSoftBody.SoftBodyFlags.GenerateBendingLinks) > (PmxSoftBody.SoftBodyFlags) 0;
      set
      {
        if (value)
          this.Flags |= PmxSoftBody.SoftBodyFlags.GenerateBendingLinks;
        else
          this.Flags &= ~PmxSoftBody.SoftBodyFlags.GenerateBendingLinks;
      }
    }

    public bool IsGenerateClusters
    {
      get => (this.Flags & PmxSoftBody.SoftBodyFlags.GenerateClusters) > (PmxSoftBody.SoftBodyFlags) 0;
      set
      {
        if (value)
          this.Flags |= PmxSoftBody.SoftBodyFlags.GenerateClusters;
        else
          this.Flags &= ~PmxSoftBody.SoftBodyFlags.GenerateClusters;
      }
    }

    public bool IsRandomizeConstraints
    {
      get => (this.Flags & PmxSoftBody.SoftBodyFlags.RandomizeConstraints) > (PmxSoftBody.SoftBodyFlags) 0;
      set
      {
        if (value)
          this.Flags |= PmxSoftBody.SoftBodyFlags.RandomizeConstraints;
        else
          this.Flags &= ~PmxSoftBody.SoftBodyFlags.RandomizeConstraints;
      }
    }

    public List<PmxSoftBody.BodyAnchor> BodyAnchorList { get; private set; }

    public List<PmxSoftBody.VertexPin> VertexPinList { get; private set; }

    public int[] VertexIndices { get; set; }

    public string NXName
    {
      get => this.Name;
      set => this.Name = value;
    }

    public void NormalizeBodyAnchorList()
    {
      if (this.BodyAnchorList.Count <= 0)
        return;
      List<int> intList = new List<int>(this.BodyAnchorList.Count);
      Dictionary<string, int> dictionary = new Dictionary<string, int>(this.BodyAnchorList.Count);
      for (int index = 0; index < this.BodyAnchorList.Count; ++index)
      {
        PmxSoftBody.BodyAnchor bodyAnchor = this.BodyAnchorList[index];
        string key = bodyAnchor.Body.ToString() + "_" + bodyAnchor.Vertex.ToString();
        if (!dictionary.ContainsKey(key))
          dictionary.Add(key, index);
        else
          intList.Add(index);
      }
      if (intList.Count > 0)
      {
        foreach (int index in CP.SortIndexForRemove(intList.ToArray()))
          this.BodyAnchorList.RemoveAt(index);
      }
    }

    public void SetVertexPinFromText(string text)
    {
      this.VertexPinList.Clear();
      string[] strArray = text.Split(',');
      if (strArray == null)
        return;
      this.VertexPinList.Capacity = strArray.Length;
      for (int index = 0; index < strArray.Length; ++index)
      {
        int result;
        if (!string.IsNullOrEmpty(strArray[index]) && int.TryParse(strArray[index].Trim(), out result))
          this.VertexPinList.Add(new PmxSoftBody.VertexPin()
          {
            Vertex = result
          });
      }
    }

    public void SortVertexPinList()
    {
      if (this.VertexPinList.Count <= 0)
        return;
      List<int> intList = new List<int>(this.VertexPinList.Count);
      for (int index = 0; index < this.VertexPinList.Count; ++index)
        intList.Add(this.VertexPinList[index].Vertex);
      intList.Sort();
      for (int index = 0; index < this.VertexPinList.Count; ++index)
      {
        PmxSoftBody.VertexPin vertexPin = this.VertexPinList[index];
        vertexPin.Vertex = intList[index];
        vertexPin.NodeIndex = -1;
        vertexPin.RefVertex = (PmxVertex) null;
      }
    }

    public void NormalizeVertexPinList()
    {
      if (this.VertexPinList.Count <= 0)
        return;
      this.SortVertexPinList();
      bool[] flagArray = new bool[this.VertexPinList.Count];
      flagArray[0] = false;
      for (int index = 1; index < this.VertexPinList.Count; ++index)
      {
        if (this.VertexPinList[index - 1].Vertex == this.VertexPinList[index].Vertex)
          flagArray[index] = true;
      }
      Dictionary<int, int> dictionary = new Dictionary<int, int>();
      foreach (PmxSoftBody.BodyAnchor bodyAnchor in this.BodyAnchorList)
        dictionary.Add(bodyAnchor.Vertex, 0);
      for (int index = 0; index < this.VertexPinList.Count; ++index)
      {
        int vertex = this.VertexPinList[index].Vertex;
        if (dictionary.ContainsKey(vertex))
          flagArray[index] = true;
      }
      for (int index = flagArray.Length - 1; index > 0; --index)
      {
        if (flagArray[index])
          this.VertexPinList.RemoveAt(index);
      }
    }

    public PmxSoftBody()
    {
      this.Name = "";
      this.NameE = "";
      this.Shape = PmxSoftBody.ShapeKind.TriMesh;
      this.Material = -1;
      this.Group = 0;
      this.PassGroup = new PmxBodyPassGroup();
      this.InitializeParameter();
      this.BodyAnchorList = new List<PmxSoftBody.BodyAnchor>();
      this.VertexPinList = new List<PmxSoftBody.VertexPin>();
      this.VertexIndices = new int[0];
    }

    public PmxSoftBody(PmxSoftBody sbody, bool nonStr) => this.FromPmxSoftBody(sbody, nonStr);

    public void InitializeParameter()
    {
      this.ClearGenerate();
      this.TotalMass = 1f;
      this.Margin = 0.05f;
      this.Config.Clear();
      this.MaterialConfig.Clear();
    }

    public void ClearGenerate()
    {
      this.IsGenerateBendingLinks = true;
      this.IsGenerateClusters = false;
      this.IsRandomizeConstraints = true;
      this.BendingLinkDistance = 2;
      this.ClusterCount = 0;
    }

    public void FromPmxSoftBody(PmxSoftBody sbody, bool nonStr)
    {
      if (!nonStr)
      {
        this.Name = sbody.Name;
        this.NameE = sbody.NameE;
      }
      this.Shape = sbody.Shape;
      this.Material = sbody.Material;
      this.Group = sbody.Group;
      this.PassGroup = sbody.PassGroup.Clone();
      this.IsGenerateBendingLinks = sbody.IsGenerateBendingLinks;
      this.IsGenerateClusters = sbody.IsGenerateClusters;
      this.IsRandomizeConstraints = sbody.IsRandomizeConstraints;
      this.BendingLinkDistance = sbody.BendingLinkDistance;
      this.ClusterCount = sbody.ClusterCount;
      this.TotalMass = sbody.TotalMass;
      this.Margin = sbody.Margin;
      this.Config = sbody.Config;
      this.MaterialConfig = sbody.MaterialConfig;
      this.BodyAnchorList = CP.CloneList<PmxSoftBody.BodyAnchor>(sbody.BodyAnchorList);
      this.VertexPinList = CP.CloneList<PmxSoftBody.VertexPin>(sbody.VertexPinList);
      this.VertexIndices = CP.CloneArray_ValueType<int>(sbody.VertexIndices);
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.Name = PmxStreamHelper.ReadString(s, f);
      this.NameE = PmxStreamHelper.ReadString(s, f);
      this.Shape = (PmxSoftBody.ShapeKind) PmxStreamHelper.ReadElement_Int32(s, 1, true);
      this.Material = PmxStreamHelper.ReadElement_Int32(s, f.MaterialSize, true);
      this.Group = PmxStreamHelper.ReadElement_Int32(s, 1, true);
      this.PassGroup.FromFlagBits((ushort) PmxStreamHelper.ReadElement_Int32(s, 2, false));
      this.Flags = (PmxSoftBody.SoftBodyFlags) PmxStreamHelper.ReadElement_Int32(s, 1, true);
      this.BendingLinkDistance = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.ClusterCount = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.TotalMass = PmxStreamHelper.ReadElement_Float(s);
      this.Margin = PmxStreamHelper.ReadElement_Float(s);
      this.Config.AeroModel = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.Config.VCF = PmxStreamHelper.ReadElement_Float(s);
      this.Config.DP = PmxStreamHelper.ReadElement_Float(s);
      this.Config.DG = PmxStreamHelper.ReadElement_Float(s);
      this.Config.LF = PmxStreamHelper.ReadElement_Float(s);
      this.Config.PR = PmxStreamHelper.ReadElement_Float(s);
      this.Config.VC = PmxStreamHelper.ReadElement_Float(s);
      this.Config.DF = PmxStreamHelper.ReadElement_Float(s);
      this.Config.MT = PmxStreamHelper.ReadElement_Float(s);
      this.Config.CHR = PmxStreamHelper.ReadElement_Float(s);
      this.Config.KHR = PmxStreamHelper.ReadElement_Float(s);
      this.Config.SHR = PmxStreamHelper.ReadElement_Float(s);
      this.Config.AHR = PmxStreamHelper.ReadElement_Float(s);
      this.Config.SRHR_CL = PmxStreamHelper.ReadElement_Float(s);
      this.Config.SKHR_CL = PmxStreamHelper.ReadElement_Float(s);
      this.Config.SSHR_CL = PmxStreamHelper.ReadElement_Float(s);
      this.Config.SR_SPLT_CL = PmxStreamHelper.ReadElement_Float(s);
      this.Config.SK_SPLT_CL = PmxStreamHelper.ReadElement_Float(s);
      this.Config.SS_SPLT_CL = PmxStreamHelper.ReadElement_Float(s);
      this.Config.V_IT = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.Config.P_IT = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.Config.D_IT = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.Config.C_IT = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.MaterialConfig.LST = PmxStreamHelper.ReadElement_Float(s);
      this.MaterialConfig.AST = PmxStreamHelper.ReadElement_Float(s);
      this.MaterialConfig.VST = PmxStreamHelper.ReadElement_Float(s);
      int num1 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.BodyAnchorList.Clear();
      this.BodyAnchorList.Capacity = num1;
      for (int index = 0; index < num1; ++index)
      {
        int num2 = PmxStreamHelper.ReadElement_Int32(s, f.BodySize, true);
        int num3 = PmxStreamHelper.ReadElement_Int32(s, f.VertexSize, true);
        int num4 = PmxStreamHelper.ReadElement_Int32(s, 1, true);
        this.BodyAnchorList.Add(new PmxSoftBody.BodyAnchor()
        {
          Body = num2,
          Vertex = num3,
          IsNear = (uint) num4 > 0U
        });
      }
      int num5 = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.VertexPinList.Clear();
      this.VertexPinList.Capacity = num5;
      for (int index = 0; index < num5; ++index)
      {
        int num6 = PmxStreamHelper.ReadElement_Int32(s, f.VertexSize, true);
        this.VertexPinList.Add(new PmxSoftBody.VertexPin()
        {
          Vertex = num6
        });
      }
      this.NormalizeBodyAnchorList();
      this.NormalizeVertexPinList();
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      PmxStreamHelper.WriteString(s, this.Name, f);
      PmxStreamHelper.WriteString(s, this.NameE, f);
      PmxStreamHelper.WriteElement_Int32(s, (int) this.Shape, 1, true);
      PmxStreamHelper.WriteElement_Int32(s, this.Material, f.MaterialSize, true);
      PmxStreamHelper.WriteElement_Int32(s, this.Group, 1, true);
      PmxStreamHelper.WriteElement_Int32(s, (int) this.PassGroup.ToFlagBits(), 2, false);
      PmxStreamHelper.WriteElement_Int32(s, (int) this.Flags, 1, false);
      PmxStreamHelper.WriteElement_Int32(s, this.BendingLinkDistance, 4, true);
      PmxStreamHelper.WriteElement_Int32(s, this.ClusterCount, 4, true);
      PmxStreamHelper.WriteElement_Float(s, this.TotalMass);
      PmxStreamHelper.WriteElement_Float(s, this.Margin);
      PmxStreamHelper.WriteElement_Int32(s, this.Config.AeroModel, 4, true);
      PmxStreamHelper.WriteElement_Float(s, this.Config.VCF);
      PmxStreamHelper.WriteElement_Float(s, this.Config.DP);
      PmxStreamHelper.WriteElement_Float(s, this.Config.DG);
      PmxStreamHelper.WriteElement_Float(s, this.Config.LF);
      PmxStreamHelper.WriteElement_Float(s, this.Config.PR);
      PmxStreamHelper.WriteElement_Float(s, this.Config.VC);
      PmxStreamHelper.WriteElement_Float(s, this.Config.DF);
      PmxStreamHelper.WriteElement_Float(s, this.Config.MT);
      PmxStreamHelper.WriteElement_Float(s, this.Config.CHR);
      PmxStreamHelper.WriteElement_Float(s, this.Config.KHR);
      PmxStreamHelper.WriteElement_Float(s, this.Config.SHR);
      PmxStreamHelper.WriteElement_Float(s, this.Config.AHR);
      PmxStreamHelper.WriteElement_Float(s, this.Config.SRHR_CL);
      PmxStreamHelper.WriteElement_Float(s, this.Config.SKHR_CL);
      PmxStreamHelper.WriteElement_Float(s, this.Config.SSHR_CL);
      PmxStreamHelper.WriteElement_Float(s, this.Config.SR_SPLT_CL);
      PmxStreamHelper.WriteElement_Float(s, this.Config.SK_SPLT_CL);
      PmxStreamHelper.WriteElement_Float(s, this.Config.SS_SPLT_CL);
      PmxStreamHelper.WriteElement_Int32(s, this.Config.V_IT, 4, true);
      PmxStreamHelper.WriteElement_Int32(s, this.Config.P_IT, 4, true);
      PmxStreamHelper.WriteElement_Int32(s, this.Config.D_IT, 4, true);
      PmxStreamHelper.WriteElement_Int32(s, this.Config.C_IT, 4, true);
      PmxStreamHelper.WriteElement_Float(s, this.MaterialConfig.LST);
      PmxStreamHelper.WriteElement_Float(s, this.MaterialConfig.AST);
      PmxStreamHelper.WriteElement_Float(s, this.MaterialConfig.VST);
      PmxStreamHelper.WriteElement_Int32(s, this.BodyAnchorList.Count, 4, true);
      for (int index = 0; index < this.BodyAnchorList.Count; ++index)
      {
        PmxStreamHelper.WriteElement_Int32(s, this.BodyAnchorList[index].Body, f.BodySize, true);
        PmxStreamHelper.WriteElement_Int32(s, this.BodyAnchorList[index].Vertex, f.VertexSize, false);
        PmxStreamHelper.WriteElement_Int32(s, this.BodyAnchorList[index].IsNear ? 1 : 0, 1, true);
      }
      PmxStreamHelper.WriteElement_Int32(s, this.VertexPinList.Count, 4, true);
      for (int index = 0; index < this.VertexPinList.Count; ++index)
        PmxStreamHelper.WriteElement_Int32(s, this.VertexPinList[index].Vertex, f.VertexSize, false);
    }

    object ICloneable.Clone() => (object) new PmxSoftBody(this, false);

    public PmxSoftBody Clone() => new PmxSoftBody(this, false);

    public enum ShapeKind
    {
      TriMesh,
      Rope,
    }

    [System.Flags]
    public enum SoftBodyFlags
    {
      GenerateBendingLinks = 1,
      GenerateClusters = 2,
      RandomizeConstraints = 4,
    }

    public struct SoftBodyConfig
    {
      public int AeroModel;
      public float VCF;
      public float DP;
      public float DG;
      public float LF;
      public float PR;
      public float VC;
      public float DF;
      public float MT;
      public float CHR;
      public float KHR;
      public float SHR;
      public float AHR;
      public float SRHR_CL;
      public float SKHR_CL;
      public float SSHR_CL;
      public float SR_SPLT_CL;
      public float SK_SPLT_CL;
      public float SS_SPLT_CL;
      public int V_IT;
      public int P_IT;
      public int D_IT;
      public int C_IT;

      public void Clear()
      {
        this.AeroModel = 0;
        this.VCF = 1f;
        this.DP = 0.0f;
        this.DG = 0.0f;
        this.LF = 0.0f;
        this.PR = 0.0f;
        this.VC = 0.0f;
        this.DF = 0.2f;
        this.MT = 0.0f;
        this.CHR = 1f;
        this.KHR = 0.1f;
        this.SHR = 1f;
        this.AHR = 0.7f;
        this.SRHR_CL = 0.1f;
        this.SKHR_CL = 1f;
        this.SSHR_CL = 0.5f;
        this.SR_SPLT_CL = 0.5f;
        this.SK_SPLT_CL = 0.5f;
        this.SS_SPLT_CL = 0.5f;
        this.V_IT = 0;
        this.P_IT = 1;
        this.D_IT = 0;
        this.C_IT = 4;
      }
    }

    public struct SoftBodyMaterialConfig
    {
      public float LST;
      public float AST;
      public float VST;

      public void Clear()
      {
        this.LST = 1f;
        this.AST = 1f;
        this.VST = 1f;
      }
    }

    public class BodyAnchor : IPmxObjectKey, ICloneable
    {
      public int Body;
      public int Vertex;
      public int NodeIndex;
      public bool IsNear;

      public PmxBody RefBody { get; set; }

      public PmxVertex RefVertex { get; set; }

      public PmxObject ObjectKey => PmxObject.SoftBodyAnchor;

      public BodyAnchor()
      {
        this.NodeIndex = -1;
        this.IsNear = false;
      }

      public BodyAnchor(PmxSoftBody.BodyAnchor ac)
      {
        this.Body = ac.Body;
        this.Vertex = ac.Vertex;
        this.NodeIndex = ac.NodeIndex;
        this.IsNear = ac.IsNear;
      }

      public object Clone() => (object) new PmxSoftBody.BodyAnchor(this);
    }

    public class VertexPin : IPmxObjectKey, ICloneable
    {
      public int Vertex;
      public int NodeIndex;

      public PmxVertex RefVertex { get; set; }

      public PmxObject ObjectKey => PmxObject.SoftBodyPinVertex;

      public VertexPin() => this.NodeIndex = -1;

      public VertexPin(PmxSoftBody.VertexPin pin)
      {
        this.Vertex = pin.Vertex;
        this.NodeIndex = pin.NodeIndex;
      }

      public object Clone() => (object) new PmxSoftBody.VertexPin(this);
    }
  }
}
