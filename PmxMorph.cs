// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxMorph
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  public class PmxMorph : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
  {
    public int Panel;
    public PmxMorph.OffsetKind Kind;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Morph;

    public string Name { get; set; }

    public string NameE { get; set; }

    public List<PmxBaseMorph> OffsetList { get; private set; }

    public bool IsUV => this.Kind == PmxMorph.OffsetKind.UV || this.Kind == PmxMorph.OffsetKind.UVA1 || this.Kind == PmxMorph.OffsetKind.UVA2 || this.Kind == PmxMorph.OffsetKind.UVA3 || this.Kind == PmxMorph.OffsetKind.UVA4;

    public bool IsVertex => this.Kind == PmxMorph.OffsetKind.Vertex;

    public bool IsBone => this.Kind == PmxMorph.OffsetKind.Bone;

    public bool IsMaterial => this.Kind == PmxMorph.OffsetKind.Material;

    public bool IsFlip => this.Kind == PmxMorph.OffsetKind.Flip;

    public bool IsImpulse => this.Kind == PmxMorph.OffsetKind.Impulse;

    public bool IsGroup => this.Kind == PmxMorph.OffsetKind.Group;

    public string NXName
    {
      get => this.Name;
      set => this.Name = value;
    }

    public static string KindText(PmxMorph.OffsetKind kind)
    {
      string str = "-";
      switch (kind)
      {
        case PmxMorph.OffsetKind.Group:
          str = "グループ";
          break;
        case PmxMorph.OffsetKind.Vertex:
          str = "頂点";
          break;
        case PmxMorph.OffsetKind.Bone:
          str = "ボーン";
          break;
        case PmxMorph.OffsetKind.UV:
          str = "UV";
          break;
        case PmxMorph.OffsetKind.UVA1:
          str = "追加UV1";
          break;
        case PmxMorph.OffsetKind.UVA2:
          str = "追加UV2";
          break;
        case PmxMorph.OffsetKind.UVA3:
          str = "追加UV3";
          break;
        case PmxMorph.OffsetKind.UVA4:
          str = "追加UV4";
          break;
        case PmxMorph.OffsetKind.Material:
          str = "材質";
          break;
        case PmxMorph.OffsetKind.Flip:
          str = "フリップ";
          break;
        case PmxMorph.OffsetKind.Impulse:
          str = "インパルス";
          break;
      }
      return str;
    }

    public PmxMorph()
    {
      this.Name = "";
      this.NameE = "";
      this.Panel = 4;
      this.Kind = PmxMorph.OffsetKind.Vertex;
      this.OffsetList = new List<PmxBaseMorph>();
    }

    public PmxMorph(PmxMorph m, bool nonStr) => this.FromPmxMorph(m, nonStr);

    public void FromPmxMorph(PmxMorph m, bool nonStr)
    {
      if (!nonStr)
      {
        this.Name = m.Name;
        this.NameE = m.NameE;
      }
      this.Panel = m.Panel;
      this.Kind = m.Kind;
      int count = m.OffsetList.Count;
      this.OffsetList = new List<PmxBaseMorph>(count);
      for (int index = 0; index < count; ++index)
        this.OffsetList.Add(m.OffsetList[index].Clone());
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.Name = PmxStreamHelper.ReadString(s, f);
      this.NameE = PmxStreamHelper.ReadString(s, f);
      this.Panel = PmxStreamHelper.ReadElement_Int32(s, 1, true);
      this.Kind = (PmxMorph.OffsetKind) PmxStreamHelper.ReadElement_Int32(s, 1, true);
      int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.OffsetList.Clear();
      this.OffsetList.Capacity = num;
      for (int index = 0; index < num; ++index)
      {
        switch (this.Kind)
        {
          case PmxMorph.OffsetKind.Group:
          case PmxMorph.OffsetKind.Flip:
            PmxGroupMorph pmxGroupMorph = new PmxGroupMorph();
            pmxGroupMorph.FromStreamEx(s, f);
            this.OffsetList.Add((PmxBaseMorph) pmxGroupMorph);
            break;
          case PmxMorph.OffsetKind.Vertex:
            PmxVertexMorph pmxVertexMorph = new PmxVertexMorph();
            pmxVertexMorph.FromStreamEx(s, f);
            this.OffsetList.Add((PmxBaseMorph) pmxVertexMorph);
            break;
          case PmxMorph.OffsetKind.Bone:
            PmxBoneMorph pmxBoneMorph = new PmxBoneMorph();
            pmxBoneMorph.FromStreamEx(s, f);
            this.OffsetList.Add((PmxBaseMorph) pmxBoneMorph);
            break;
          case PmxMorph.OffsetKind.UV:
          case PmxMorph.OffsetKind.UVA1:
          case PmxMorph.OffsetKind.UVA2:
          case PmxMorph.OffsetKind.UVA3:
          case PmxMorph.OffsetKind.UVA4:
            PmxUVMorph pmxUvMorph = new PmxUVMorph();
            pmxUvMorph.FromStreamEx(s, f);
            this.OffsetList.Add((PmxBaseMorph) pmxUvMorph);
            break;
          case PmxMorph.OffsetKind.Material:
            PmxMaterialMorph pmxMaterialMorph = new PmxMaterialMorph();
            pmxMaterialMorph.FromStreamEx(s, f);
            this.OffsetList.Add((PmxBaseMorph) pmxMaterialMorph);
            break;
          case PmxMorph.OffsetKind.Impulse:
            PmxImpulseMorph pmxImpulseMorph = new PmxImpulseMorph();
            pmxImpulseMorph.FromStreamEx(s, f);
            this.OffsetList.Add((PmxBaseMorph) pmxImpulseMorph);
            break;
        }
      }
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      if (this.IsImpulse && (double) f.Ver < 2.09999990463257)
        return;
      PmxStreamHelper.WriteString(s, this.Name, f);
      PmxStreamHelper.WriteString(s, this.NameE, f);
      PmxStreamHelper.WriteElement_Int32(s, this.Panel, 1, true);
      if (this.IsFlip && (double) f.Ver < 2.09999990463257)
        PmxStreamHelper.WriteElement_Int32(s, 0, 1, true);
      else
        PmxStreamHelper.WriteElement_Int32(s, (int) this.Kind, 1, true);
      PmxStreamHelper.WriteElement_Int32(s, this.OffsetList.Count, 4, true);
      for (int index = 0; index < this.OffsetList.Count; ++index)
      {
        switch (this.Kind)
        {
          case PmxMorph.OffsetKind.Group:
          case PmxMorph.OffsetKind.Flip:
            (this.OffsetList[index] as PmxGroupMorph).ToStreamEx(s, f);
            break;
          case PmxMorph.OffsetKind.Vertex:
            (this.OffsetList[index] as PmxVertexMorph).ToStreamEx(s, f);
            break;
          case PmxMorph.OffsetKind.Bone:
            (this.OffsetList[index] as PmxBoneMorph).ToStreamEx(s, f);
            break;
          case PmxMorph.OffsetKind.UV:
          case PmxMorph.OffsetKind.UVA1:
          case PmxMorph.OffsetKind.UVA2:
          case PmxMorph.OffsetKind.UVA3:
          case PmxMorph.OffsetKind.UVA4:
            (this.OffsetList[index] as PmxUVMorph).ToStreamEx(s, f);
            break;
          case PmxMorph.OffsetKind.Material:
            (this.OffsetList[index] as PmxMaterialMorph).ToStreamEx(s, f);
            break;
          case PmxMorph.OffsetKind.Impulse:
            (this.OffsetList[index] as PmxImpulseMorph).ToStreamEx(s, f);
            break;
        }
      }
    }

    object ICloneable.Clone() => (object) new PmxMorph(this, false);

    public PmxMorph Clone() => new PmxMorph(this, false);

    public enum OffsetKind
    {
      Group,
      Vertex,
      Bone,
      UV,
      UVA1,
      UVA2,
      UVA3,
      UVA4,
      Material,
      Flip,
      Impulse,
    }
  }
}
