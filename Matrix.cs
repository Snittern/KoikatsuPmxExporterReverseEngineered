// Decompiled with JetBrains decompiler
// Type: PmxLib.Matrix
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

namespace PmxLib
{
  public struct Matrix
  {
    public float M11;
    public float M12;
    public float M13;
    public float M14;
    public float M21;
    public float M22;
    public float M23;
    public float M24;
    public float M31;
    public float M32;
    public float M33;
    public float M34;
    public float M41;
    public float M42;
    public float M43;
    public float M44;

    public static Matrix Identity => new Matrix()
    {
      M11 = 1f,
      M22 = 1f,
      M33 = 1f,
      M44 = 1f
    };

    public float[] ToArray() => new float[16]
    {
      this.M11,
      this.M12,
      this.M13,
      this.M14,
      this.M21,
      this.M22,
      this.M23,
      this.M24,
      this.M31,
      this.M32,
      this.M33,
      this.M34,
      this.M41,
      this.M42,
      this.M43,
      this.M44
    };
  }
}
