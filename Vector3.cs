// Decompiled with JetBrains decompiler
// Type: PmxLib.Vector3
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;

namespace PmxLib
{
  public struct Vector3
  {
    public float x;
    public float y;
    public float z;

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

    public static Vector3 zero => new Vector3(0.0f, 0.0f, 0.0f);

    public static Vector3 Zero => new Vector3(0.0f, 0.0f, 0.0f);

    public Vector3(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public Vector3(float x, float y)
    {
      this.x = x;
      this.y = y;
      this.z = 0.0f;
    }

    public Vector3(Vector3 v)
    {
      this.x = v.x;
      this.y = v.y;
      this.z = v.z;
    }

    public static float Dot(Vector3 lhs, Vector3 rhs) => (float) ((double) lhs.x * (double) rhs.x + (double) lhs.y * (double) rhs.y + (double) lhs.z * (double) rhs.z);

    public static Vector3 Cross(Vector3 lhs, Vector3 rhs) => new Vector3((float) ((double) lhs.y * (double) rhs.z - (double) lhs.z * (double) rhs.y), (float) ((double) lhs.z * (double) rhs.x - (double) lhs.x * (double) rhs.z), (float) ((double) lhs.x * (double) rhs.y - (double) lhs.y * (double) rhs.x));

    public static float SqrMagnitude(Vector3 a) => (float) ((double) a.x * (double) a.x + (double) a.y * (double) a.y + (double) a.z * (double) a.z);

    public float Length()
    {
      double y = (double) this.Y;
      double x = (double) this.X;
      double z = (double) this.Z;
      double num1 = x;
      double num2 = num1 * num1;
      double num3 = y;
      double num4 = num2 + num3 * num3;
      double num5 = z;
      return (float) Math.Sqrt(num4 + num5 * num5);
    }

    public void Normalize()
    {
      float num1 = this.Length();
      if ((double) num1 == 0.0)
        return;
      float num2 = 1f / num1;
      this.X *= num2;
      this.Y *= num2;
      this.Z *= num2;
    }

    public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);

    public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);

    public static Vector3 operator -(Vector3 a) => new Vector3(-a.x, -a.y, -a.z);

    public static Vector3 operator *(Vector3 a, float d) => new Vector3(a.x * d, a.y * d, a.z * d);

    public static Vector3 operator *(float d, Vector3 a) => new Vector3(a.x * d, a.y * d, a.z * d);

    public static Vector3 operator /(Vector3 a, float d) => new Vector3(a.x / d, a.y / d, a.z / d);

    public static bool operator ==(Vector3 lhs, Vector3 rhs) => (double) Vector3.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;

    public static bool operator !=(Vector3 lhs, Vector3 rhs) => (double) Vector3.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;

    public override string ToString()
    {
      object[] objArray = new object[3];
      float x = this.X;
      objArray[0] = (object) x.ToString();
      float y = this.Y;
      objArray[1] = (object) y.ToString();
      float z = this.Z;
      objArray[2] = (object) z.ToString();
      return string.Format("X:{0} Y:{1} Z:{2}", objArray);
    }

    public override bool Equals(object other) => other is Vector3 vector3 && this.x.Equals(vector3.x) && this.y.Equals(vector3.y) && this.z.Equals(vector3.z);

    public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
  }
}
