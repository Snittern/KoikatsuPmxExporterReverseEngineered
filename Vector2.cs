// Decompiled with JetBrains decompiler
// Type: PmxLib.Vector2
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

namespace PmxLib
{
  public struct Vector2
  {
    public float x;
    public float y;

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

    public static Vector2 zero => new Vector2(0.0f, 0.0f);

    public Vector2(float x, float y)
    {
      this.x = x;
      this.y = y;
    }

    public Vector2(Vector2 v)
    {
      this.x = v.x;
      this.y = v.y;
    }

    public override bool Equals(object other) => other is Vector2 vector2 && this.x.Equals(vector2.x) && this.y.Equals(vector2.y);

    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);

    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);

    public static Vector2 operator -(Vector2 a) => new Vector2(-a.x, -a.y);

    public static Vector2 operator *(Vector2 a, float d) => new Vector2(a.x * d, a.y * d);

    public static Vector2 operator *(float d, Vector2 a) => new Vector2(a.x * d, a.y * d);

    public static Vector2 operator /(Vector2 a, float d) => new Vector2(a.x / d, a.y / d);

    public static bool operator ==(Vector2 lhs, Vector2 rhs) => (double) Vector2.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;

    public static bool operator !=(Vector2 lhs, Vector2 rhs) => (double) Vector2.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;

    public static implicit operator Vector2(Vector3 v) => new Vector2(v.x, v.y);

    public static implicit operator Vector3(Vector2 v) => new Vector3(v.x, v.y, 0.0f);

    public static float SqrMagnitude(Vector2 a) => (float) ((double) a.x * (double) a.x + (double) a.y * (double) a.y);

    public override string ToString()
    {
      object[] objArray = new object[2];
      float x = this.X;
      objArray[0] = (object) x.ToString();
      float y = this.Y;
      objArray[1] = (object) y.ToString();
      return string.Format("X:{0} Y:{1}", objArray);
    }

    public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
  }
}
