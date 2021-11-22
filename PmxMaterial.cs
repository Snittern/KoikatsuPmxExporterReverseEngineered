// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxMaterial
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PmxLib
{
  public class PmxMaterial : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
  {
    public Color Diffuse;
    public Color Specular;
    public float Power;
    public Color Ambient;
    public PmxMaterial.ExDrawMode ExDraw;
    public PmxMaterial.MaterialFlags Flags;
    public Color EdgeColor;
    public float EdgeSize;
    public int FaceCount;
    public PmxMaterial.SphereModeType SphereMode;
    public PmxMaterialMorph.MorphData OffsetMul;
    public PmxMaterialMorph.MorphData OffsetAdd;
    public List<PmxFace> FaceList;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Material;

    public string Name { get; set; }

    public string NameE { get; set; }

    public string Tex { get; set; }

    public string Sphere { get; set; }

    public string Toon { get; set; }

    public string Memo { get; set; }

    public PmxMaterialAttribute Attribute { get; private set; }

    public string NXName
    {
      get => this.Name;
      set => this.Name = value;
    }

    public void ClearFlags() => this.Flags = PmxMaterial.MaterialFlags.None;

    public bool GetFlag(PmxMaterial.MaterialFlags f) => (f & this.Flags) == f;

    public void SetFlag(PmxMaterial.MaterialFlags f, bool val)
    {
      if (val)
        this.Flags |= f;
      else
        this.Flags &= ~f;
    }

    public void ClearOffset()
    {
      this.OffsetMul.Clear(PmxMaterialMorph.OpType.Mul);
      this.OffsetAdd.Clear(PmxMaterialMorph.OpType.Add);
    }

    public void UpdateAttributeFromMemo() => this.Attribute.SetFromText(this.Memo);

    public PmxMaterial()
    {
      this.Name = "";
      this.NameE = "";
      this.Diffuse = new Color(1f, 1f, 1f);
      this.Specular = new Color(0.0f, 0.0f, 0.0f);
      this.Power = 0.0f;
      this.Ambient = new Color(1f, 1f, 1f);
      this.ClearFlags();
      this.EdgeColor = new Color(0.0f, 0.0f, 0.0f);
      this.EdgeSize = 1f;
      this.Tex = "";
      this.Sphere = "";
      this.SphereMode = PmxMaterial.SphereModeType.Mul;
      this.Toon = "";
      this.Memo = "";
      this.OffsetMul = new PmxMaterialMorph.MorphData();
      this.OffsetAdd = new PmxMaterialMorph.MorphData();
      this.ClearOffset();
      this.ExDraw = PmxMaterial.ExDrawMode.F3;
      this.Attribute = new PmxMaterialAttribute();
    }

    public PmxMaterial(PmxMaterial m, bool nonStr)
      : this()
    {
      this.FromPmxMaterial(m, nonStr);
    }

    public void FromPmxMaterial(PmxMaterial m, bool nonStr)
    {
      this.Diffuse = m.Diffuse;
      this.Specular = m.Specular;
      this.Power = m.Power;
      this.Ambient = m.Ambient;
      this.Flags = m.Flags;
      this.EdgeColor = m.EdgeColor;
      this.EdgeSize = m.EdgeSize;
      this.SphereMode = m.SphereMode;
      this.FaceCount = m.FaceCount;
      this.ExDraw = m.ExDraw;
      if (!nonStr)
      {
        this.Name = m.Name;
        this.NameE = m.NameE;
        this.Tex = m.Tex;
        this.Sphere = m.Sphere;
        this.Toon = m.Toon;
        this.Memo = m.Memo;
      }
      this.Attribute = m.Attribute;
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.Name = PmxStreamHelper.ReadString(s, f);
      this.NameE = PmxStreamHelper.ReadString(s, f);
      this.Diffuse = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
      this.Specular = V3_BytesConvert.Vector3ToColor(V3_BytesConvert.FromStream(s));
      this.Power = PmxStreamHelper.ReadElement_Float(s);
      this.Ambient = V3_BytesConvert.Vector3ToColor(V3_BytesConvert.FromStream(s));
      this.Flags = (PmxMaterial.MaterialFlags) s.ReadByte();
      this.EdgeColor = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
      this.EdgeSize = PmxStreamHelper.ReadElement_Float(s);
      this.Tex = PmxStreamHelper.ReadString(s, f);
      this.Sphere = PmxStreamHelper.ReadString(s, f);
      this.SphereMode = (PmxMaterial.SphereModeType) s.ReadByte();
      this.Toon = PmxStreamHelper.ReadString(s, f);
      this.Memo = PmxStreamHelper.ReadString(s, f);
      this.FaceCount = PmxStreamHelper.ReadElement_Int32(s, 4, true);
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      PmxStreamHelper.WriteString(s, this.Name, f);
      PmxStreamHelper.WriteString(s, this.NameE, f);
      V4_BytesConvert.ToStream(s, V4_BytesConvert.ColorToVector4(this.Diffuse));
      V3_BytesConvert.ToStream(s, V3_BytesConvert.ColorToVector3(this.Specular));
      PmxStreamHelper.WriteElement_Float(s, this.Power);
      V3_BytesConvert.ToStream(s, V3_BytesConvert.ColorToVector3(this.Ambient));
      s.WriteByte((byte) this.Flags);
      V4_BytesConvert.ToStream(s, V4_BytesConvert.ColorToVector4(this.EdgeColor));
      PmxStreamHelper.WriteElement_Float(s, this.EdgeSize);
      PmxStreamHelper.WriteString(s, this.Tex, f);
      PmxStreamHelper.WriteString(s, this.Sphere, f);
      s.WriteByte((byte) this.SphereMode);
      PmxStreamHelper.WriteString(s, this.Toon, f);
      PmxStreamHelper.WriteString(s, this.Memo, f);
      PmxStreamHelper.WriteElement_Int32(s, this.FaceCount, 4, true);
    }

    public void FromStreamEx_TexTable(Stream s, PmxTextureTable tx, PmxElementFormat f)
    {
      this.Name = PmxStreamHelper.ReadString(s, f);
      this.NameE = PmxStreamHelper.ReadString(s, f);
      this.Diffuse = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
      this.Specular = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
      this.Power = PmxStreamHelper.ReadElement_Float(s);
      this.Ambient = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
      this.Flags = (PmxMaterial.MaterialFlags) s.ReadByte();
      this.EdgeColor = V4_BytesConvert.Vector4ToColor(V4_BytesConvert.FromStream(s));
      this.EdgeSize = PmxStreamHelper.ReadElement_Float(s);
      this.Tex = tx.GetName(PmxStreamHelper.ReadElement_Int32(s, f.TexSize, true));
      this.Sphere = tx.GetName(PmxStreamHelper.ReadElement_Int32(s, f.TexSize, true));
      this.SphereMode = (PmxMaterial.SphereModeType) s.ReadByte();
      this.Toon = s.ReadByte() != 0 ? SystemToon.GetToonName(s.ReadByte()) : tx.GetName(PmxStreamHelper.ReadElement_Int32(s, f.TexSize, true));
      this.Memo = PmxStreamHelper.ReadString(s, f);
      this.UpdateAttributeFromMemo();
      this.FaceCount = PmxStreamHelper.ReadElement_Int32(s, 4, true);
    }

    public void ToStreamEx_TexTable(Stream s, PmxTextureTable tx, PmxElementFormat f)
    {
      PmxStreamHelper.WriteString(s, this.Name, f);
      PmxStreamHelper.WriteString(s, this.NameE, f);
      V4_BytesConvert.ToStream(s, V4_BytesConvert.ColorToVector4(this.Diffuse));
      V3_BytesConvert.ToStream(s, V3_BytesConvert.ColorToVector3(this.Specular));
      PmxStreamHelper.WriteElement_Float(s, this.Power);
      V3_BytesConvert.ToStream(s, V3_BytesConvert.ColorToVector3(this.Ambient));
      s.WriteByte((byte) this.Flags);
      V4_BytesConvert.ToStream(s, V4_BytesConvert.ColorToVector4(this.EdgeColor));
      PmxStreamHelper.WriteElement_Float(s, this.EdgeSize);
      PmxStreamHelper.WriteElement_Int32(s, tx.GetIndex(this.Tex), f.TexSize, true);
      PmxStreamHelper.WriteElement_Int32(s, tx.GetIndex(this.Sphere), f.TexSize, true);
      s.WriteByte((byte) this.SphereMode);
      int toonIndex = SystemToon.GetToonIndex(this.Toon);
      if (toonIndex < 0)
      {
        s.WriteByte((byte) 0);
        PmxStreamHelper.WriteElement_Int32(s, tx.GetIndex(this.Toon), f.TexSize, true);
      }
      else
      {
        s.WriteByte((byte) 1);
        s.WriteByte((byte) toonIndex);
      }
      PmxStreamHelper.WriteString(s, this.Memo, f);
      PmxStreamHelper.WriteElement_Int32(s, this.FaceCount, 4, true);
    }

    object ICloneable.Clone() => (object) new PmxMaterial(this, false);

    public PmxMaterial Clone() => new PmxMaterial(this, false);

    [System.Flags]
    public enum MaterialFlags
    {
      None = 0,
      DrawBoth = 1,
      Shadow = 2,
      SelfShadowMap = 4,
      SelfShadow = 8,
      Edge = 16, // 0x00000010
      VertexColor = 32, // 0x00000020
      PointDraw = 64, // 0x00000040
      LineDraw = 128, // 0x00000080
    }

    public enum ExDrawMode
    {
      F1,
      F2,
      F3,
    }

    public enum SphereModeType
    {
      None,
      Mul,
      Add,
      SubTex,
    }
  }
}
