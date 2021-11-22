// Decompiled with JetBrains decompiler
// Type: PmxLib.Vector4
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

namespace PmxLib
{
  public struct Vector4
  {
    public float x;
    public float y;
    public float z;
    public float w;

    public float X
    {
      get => this.x;
      set => this.x = value;
    }

    public float Y
    {
      get => this.y;
      set => this.y = value;
    }

    public float Z
    {
      get => this.z;
      set => this.z = value;
    }

    public float W
    {
      get => this.w;
      set => this.w = value;
    }

    public static Vector4 zero => new Vector4(0.0f, 0.0f, 0.0f, 0.0f);

    public Vector4(float x, float y, float z, float w)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = w;
    }

    public Vector4(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = 0.0f;
    }

    public Vector4(float x, float y)
    {
      this.x = x;
      this.y = y;
      this.z = 0.0f;
      this.w = 0.0f;
    }

    public static float Dot(Vector4 a, Vector4 b) => (float) ((double) a.x * (double) b.x + (double) a.y * (double) b.y + (double) a.z * (double) b.z + (double) a.w * (double) b.w);

    public static float SqrMagnitude(Vector4 a) => Vector4.Dot(a, a);

    public static Vector4 operator +(Vector4 a, Vector4 b) => new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);

    public static Vector4 operator -(Vector4 a, Vector4 b) => new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);

    public static Vector4 operator -(Vector4 a) => new Vector4(-a.x, -a.y, -a.z, -a.w);

    public static Vector4 operator *(Vector4 a, float d) => new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);

    public static Vector4 operator *(float d, Vector4 a) => new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);

    public static Vector4 operator /(Vector4 a, float d) => new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);

    public static bool operator ==(Vector4 lhs, Vector4 rhs) => (double) Vector4.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;

    public static bool operator !=(Vector4 lhs, Vector4 rhs) => (double) Vector4.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;

    public static implicit operator Vector4(Vector3 v) => new Vector4(v.x, v.y, v.z, 0.0f);

    public static implicit operator Vector3(Vector4 v) => new Vector3(v.x, v.y, v.z);

    public static implicit operator Vector4(Vector2 v) => new Vector4(v.x, v.y, 0.0f, 0.0f);

    public static implicit operator Vector2(Vector4 v) => new Vector2(v.x, v.y);

    public override bool Equals(object other) => other is Vector4 vector4 && this.x.Equals(vector4.x) && this.y.Equals(vector4.y) && this.z.Equals(vector4.z) && this.w.Equals(vector4.w);

    public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
  }
}
