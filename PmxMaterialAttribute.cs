// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxMaterialAttribute
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;

namespace PmxLib
{
  public class PmxMaterialAttribute : ICloneable
  {
    public const string TAG_BumpMap = "BumpMap";
    public const string TAG_NormalMap = "NormalMap";
    public const string TAG_CubeMap = "CubeMap";
    public const string TAG_BumpMapUV = "BumpMapUV";
    public const string TAG_NormalMapUV = "NormalMapUV";
    public const string TAG_CubeMapUV = "CubeMapUV";
    public const string UVTarget_UV = "uv";
    public const string UVTarget_UVA1xy = "uva1xy";
    public const string UVTarget_UVA1zw = "uva1zw";
    public const string UVTarget_UVA2xy = "uva2xy";
    public const string UVTarget_UVA2zw = "uva2zw";
    public const string UVTarget_UVA3xy = "uva3xy";
    public const string UVTarget_UVA3zw = "uva3zw";
    public const string UVTarget_UVA4xy = "uva4xy";
    public const string UVTarget_UVA4zw = "uva4zw";

    public string BumpMapTexture { get; private set; }

    public PmxMaterialAttribute.UVTarget BumpMapUV { get; private set; }

    public string NormalMapTexture { get; private set; }

    public PmxMaterialAttribute.UVTarget NormalMapUV { get; private set; }

    public string CubeMapTexture { get; private set; }

    public PmxMaterialAttribute.UVTarget CubeMapUV { get; private set; }

    public PmxMaterialAttribute() => this.Clear();

    public PmxMaterialAttribute(string text)
      : this()
    {
      this.SetFromText(text);
    }

    public PmxMaterialAttribute(PmxMaterialAttribute att)
    {
      this.BumpMapTexture = att.BumpMapTexture;
      this.NormalMapTexture = att.NormalMapTexture;
      this.CubeMapTexture = att.CubeMapTexture;
      this.BumpMapUV = att.BumpMapUV;
      this.NormalMapUV = att.NormalMapUV;
      this.CubeMapUV = att.CubeMapUV;
    }

    public void Clear()
    {
      this.BumpMapTexture = (string) null;
      this.NormalMapTexture = (string) null;
      this.CubeMapTexture = (string) null;
      this.BumpMapUV = PmxMaterialAttribute.UVTarget.UV;
      this.NormalMapUV = PmxMaterialAttribute.UVTarget.UV;
      this.CubeMapUV = PmxMaterialAttribute.UVTarget.UV;
    }

    private static PmxMaterialAttribute.UVTarget TextToUVTarget(string text)
    {
      PmxMaterialAttribute.UVTarget uvTarget = PmxMaterialAttribute.UVTarget.UV;
      switch (text.ToLower())
      {
        case "uva1xy":
          uvTarget = PmxMaterialAttribute.UVTarget.UVA1xy;
          break;
        case "uva1zw":
          uvTarget = PmxMaterialAttribute.UVTarget.UVA1zw;
          break;
        case "uva2xy":
          uvTarget = PmxMaterialAttribute.UVTarget.UVA2xy;
          break;
        case "uva2zw":
          uvTarget = PmxMaterialAttribute.UVTarget.UVA2zw;
          break;
        case "uva3xy":
          uvTarget = PmxMaterialAttribute.UVTarget.UVA3xy;
          break;
        case "uva3zw":
          uvTarget = PmxMaterialAttribute.UVTarget.UVA3zw;
          break;
        case "uva4xy":
          uvTarget = PmxMaterialAttribute.UVTarget.UVA4xy;
          break;
        case "uva4zw":
          uvTarget = PmxMaterialAttribute.UVTarget.UVA4zw;
          break;
      }
      return uvTarget;
    }

    public void SetFromText(string text)
    {
      this.Clear();
      if (string.IsNullOrEmpty(text))
        return;
      string[] tag1 = PmxTag.GetTag("BumpMap", text);
      if (tag1 != null && (uint) tag1.Length > 0U)
        this.BumpMapTexture = tag1[0];
      string[] tag2 = PmxTag.GetTag("BumpMapUV", text);
      if (tag2 != null && (uint) tag2.Length > 0U)
        this.BumpMapUV = PmxMaterialAttribute.TextToUVTarget(tag2[0]);
      string[] tag3 = PmxTag.GetTag("NormalMap", text);
      if (tag3 != null && (uint) tag3.Length > 0U)
        this.NormalMapTexture = tag3[0];
      string[] tag4 = PmxTag.GetTag("NormalMapUV", text);
      if (tag4 != null && (uint) tag4.Length > 0U)
        this.NormalMapUV = PmxMaterialAttribute.TextToUVTarget(tag4[0]);
      string[] tag5 = PmxTag.GetTag("CubeMap", text);
      if (tag5 != null && (uint) tag5.Length > 0U)
        this.CubeMapTexture = tag5[0];
      string[] tag6 = PmxTag.GetTag("CubeMapUV", text);
      if (tag6 != null && (uint) tag6.Length > 0U)
        this.CubeMapUV = PmxMaterialAttribute.TextToUVTarget(tag6[0]);
    }

    object ICloneable.Clone() => (object) new PmxMaterialAttribute(this);

    public PmxMaterialAttribute Clone() => new PmxMaterialAttribute(this);

    public enum UVTarget
    {
      UV,
      UVA1xy,
      UVA1zw,
      UVA2xy,
      UVA2zw,
      UVA3xy,
      UVA3zw,
      UVA4xy,
      UVA4zw,
    }
  }
}
