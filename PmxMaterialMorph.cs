// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxMaterialMorph
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  public class PmxMaterialMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    public int Index;
    public static PmxMaterial RefAllMaterial = new PmxMaterial();
    public PmxMaterialMorph.OpType Op;
    public PmxMaterialMorph.MorphData Data;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.MaterialMorph;

    public override int BaseIndex
    {
      get => this.Index;
      set => this.Index = value;
    }

    public PmxMaterial RefMaterial { get; set; }

    public void ClearData() => this.Data.Clear(this.Op);

    public PmxMaterialMorph()
    {
      this.Op = PmxMaterialMorph.OpType.Mul;
      this.ClearData();
    }

    public PmxMaterialMorph(int index, PmxMaterialMorph.MorphData d)
      : this()
    {
      this.Index = index;
      this.Data = d;
    }

    public PmxMaterialMorph(PmxMaterialMorph sv)
      : this()
    {
      this.FromPmxMaterialMorph(sv);
    }

    public void FromPmxMaterialMorph(PmxMaterialMorph sv)
    {
      this.Index = sv.Index;
      this.Op = sv.Op;
      this.Data = sv.Data;
    }

    public override void FromStreamEx(Stream s, PmxElementFormat size)
    {
      this.Index = PmxStreamHelper.ReadElement_Int32(s, size.MaterialSize, true);
      this.Op = (PmxMaterialMorph.OpType) s.ReadByte();
      this.Data.Diffuse = V4_BytesConvert.FromStream(s);
      this.Data.Specular = V4_BytesConvert.FromStream(s);
      this.Data.Ambient = V3_BytesConvert.FromStream(s);
      this.Data.EdgeColor = V4_BytesConvert.FromStream(s);
      this.Data.EdgeSize = PmxStreamHelper.ReadElement_Float(s);
      this.Data.Tex = V4_BytesConvert.FromStream(s);
      this.Data.Sphere = V4_BytesConvert.FromStream(s);
      this.Data.Toon = V4_BytesConvert.FromStream(s);
    }

    public override void ToStreamEx(Stream s, PmxElementFormat size)
    {
      PmxStreamHelper.WriteElement_Int32(s, this.Index, size.MaterialSize, true);
      s.WriteByte((byte) this.Op);
      V4_BytesConvert.ToStream(s, this.Data.Diffuse);
      V4_BytesConvert.ToStream(s, this.Data.Specular);
      V3_BytesConvert.ToStream(s, this.Data.Ambient);
      V4_BytesConvert.ToStream(s, this.Data.EdgeColor);
      PmxStreamHelper.WriteElement_Float(s, this.Data.EdgeSize);
      V4_BytesConvert.ToStream(s, this.Data.Tex);
      V4_BytesConvert.ToStream(s, this.Data.Sphere);
      V4_BytesConvert.ToStream(s, this.Data.Toon);
    }

    object ICloneable.Clone() => (object) new PmxMaterialMorph(this);

    public override PmxBaseMorph Clone() => (PmxBaseMorph) new PmxMaterialMorph(this);

    public enum OpType
    {
      Mul,
      Add,
    }

    public struct MorphData
    {
      public Vector4 Diffuse;
      public Vector4 Specular;
      public Vector3 Ambient;
      public float EdgeSize;
      public Vector4 EdgeColor;
      public Vector4 Tex;
      public Vector4 Sphere;
      public Vector4 Toon;

      public void Set(float v)
      {
        this.Diffuse = new Vector4(v, v, v, v);
        this.Specular = new Vector4(v, v, v, v);
        this.Ambient = new Vector3(v, v, v);
        this.EdgeSize = v;
        this.EdgeColor = new Vector4(v, v, v, v);
        this.Tex = new Vector4(v, v, v, v);
        this.Sphere = new Vector4(v, v, v, v);
        this.Toon = new Vector4(v, v, v, v);
      }

      public void Clear(PmxMaterialMorph.OpType op)
      {
        switch (op)
        {
          case PmxMaterialMorph.OpType.Mul:
            this.Set(1f);
            this.Diffuse = new Vector4(1f, 1f, 1f, 1f);
            this.Specular = new Vector4(1f, 1f, 1f, 1f);
            this.Ambient = new Vector3(1f, 1f, 1f);
            this.EdgeSize = 1f;
            this.EdgeColor = new Vector4(1f, 1f, 1f, 1f);
            this.Tex = new Vector4(1f, 1f, 1f, 1f);
            this.Sphere = new Vector4(1f, 1f, 1f, 1f);
            this.Toon = new Vector4(1f, 1f, 1f, 1f);
            break;
          case PmxMaterialMorph.OpType.Add:
            this.Diffuse = Vector4.zero;
            this.Specular = Vector4.zero;
            this.Ambient = Vector3.zero;
            this.EdgeSize = 0.0f;
            this.EdgeColor = Vector4.zero;
            this.Tex = Vector4.zero;
            this.Sphere = Vector4.zero;
            this.Toon = Vector4.zero;
            break;
        }
      }

      private Vector4 mul_v4(Vector4 v0, Vector4 v1) => new Vector4(v0.x * v1.x, v0.y * v1.y, v0.z * v1.z, v0.w * v1.w);

      private Vector3 mul_v3(Vector3 v0, Vector3 v1) => new Vector3(v0.x * v1.x, v0.y * v1.y, v0.z * v1.z);

      public void Mul(PmxMaterialMorph.MorphData d)
      {
        this.Diffuse = this.mul_v4(this.Diffuse, d.Diffuse);
        this.Specular = this.mul_v4(this.Specular, d.Specular);
        this.Ambient = this.mul_v3(this.Ambient, d.Ambient);
        this.EdgeSize *= d.EdgeSize;
        this.EdgeColor = this.mul_v4(this.EdgeColor, d.EdgeColor);
        this.Tex = this.mul_v4(this.Tex, d.Tex);
        this.Sphere = this.mul_v4(this.Sphere, d.Sphere);
        this.Toon = this.mul_v4(this.Toon, d.Toon);
      }

      public void Mul(float v)
      {
        this.Diffuse *= v;
        this.Specular *= v;
        this.Ambient *= v;
        this.EdgeSize *= v;
        this.EdgeColor *= v;
        this.Tex *= v;
        this.Sphere *= v;
        this.Toon *= v;
      }

      public void Add(PmxMaterialMorph.MorphData d)
      {
        this.Diffuse += d.Diffuse;
        this.Specular += d.Specular;
        this.Ambient += d.Ambient;
        this.EdgeSize += d.EdgeSize;
        this.EdgeColor += d.EdgeColor;
        this.Tex += d.Tex;
        this.Sphere += d.Sphere;
        this.Toon += d.Toon;
      }

      public static PmxMaterialMorph.MorphData Inter(
        PmxMaterialMorph.MorphData a,
        PmxMaterialMorph.MorphData b,
        float val)
      {
        PmxMaterialMorph.MorphData morphData1;
        if ((double) val == 0.0)
          morphData1 = a;
        else if ((double) val == 1.0)
        {
          morphData1 = b;
        }
        else
        {
          PmxMaterialMorph.MorphData morphData2 = a;
          morphData2.Mul(1f - val);
          PmxMaterialMorph.MorphData d = b;
          d.Mul(val);
          morphData2.Add(d);
          morphData1 = morphData2;
        }
        return morphData1;
      }
    }
  }
}
