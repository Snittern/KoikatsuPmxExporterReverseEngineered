// Decompiled with JetBrains decompiler
// Type: PmxLib.Quaternion
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

namespace PmxLib
{
  public struct Quaternion
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

    public static Quaternion identity => new Quaternion()
    {
      X = 0.0f,
      Y = 0.0f,
      Z = 0.0f,
      W = 1f
    };

    public static Quaternion Identity => new Quaternion()
    {
      X = 0.0f,
      Y = 0.0f,
      Z = 0.0f,
      W = 1f
    };

    public Quaternion(Vector3 value, float w)
    {
      this.x = value.X;
      this.y = value.Y;
      this.z = value.Z;
      this.w = w;
    }

    public Quaternion(float x, float y, float z, float w)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = w;
    }

    public static Quaternion operator *(float scale, Quaternion quaternion) => new Quaternion()
    {
      X = quaternion.X * scale,
      Y = quaternion.Y * scale,
      Z = quaternion.Z * scale,
      W = quaternion.W * scale
    };

    public static Quaternion operator *(Quaternion quaternion, float scale) => new Quaternion()
    {
      X = quaternion.X * scale,
      Y = quaternion.Y * scale,
      Z = quaternion.Z * scale,
      W = quaternion.W * scale
    };

    public static Quaternion operator *(Quaternion left, Quaternion right)
    {
      Quaternion quaternion = new Quaternion();
      float x1 = left.X;
      float y1 = left.Y;
      float z1 = left.Z;
      float w1 = left.W;
      float x2 = right.X;
      float y2 = right.Y;
      float z2 = right.Z;
      float w2 = right.W;
      quaternion.X = (float) ((double) x2 * (double) w1 + (double) w2 * (double) x1 + (double) y2 * (double) z1 - (double) z2 * (double) y1);
      quaternion.Y = (float) ((double) y2 * (double) w1 + (double) w2 * (double) y1 + (double) z2 * (double) x1 - (double) x2 * (double) z1);
      quaternion.Z = (float) ((double) z2 * (double) w1 + (double) w2 * (double) z1 + (double) x2 * (double) y1 - (double) y2 * (double) x1);
      quaternion.W = (float) ((double) w2 * (double) w1 - ((double) y2 * (double) y1 + (double) x2 * (double) x1 + (double) z2 * (double) z1));
      return quaternion;
    }

    public static Quaternion operator /(Quaternion left, float right) => new Quaternion()
    {
      X = left.X / right,
      Y = left.Y / right,
      Z = left.Z / right,
      W = left.W / right
    };

    public static Quaternion operator +(Quaternion left, Quaternion right) => new Quaternion()
    {
      X = left.X + right.X,
      Y = left.Y + right.Y,
      Z = left.Z + right.Z,
      W = left.W + right.W
    };

    public static Quaternion operator -(Quaternion quaternion) => new Quaternion()
    {
      X = -quaternion.X,
      Y = -quaternion.Y,
      Z = -quaternion.Z,
      W = -quaternion.W
    };

    public static Quaternion operator -(Quaternion left, Quaternion right) => new Quaternion()
    {
      X = left.X - right.X,
      Y = left.Y - right.Y,
      Z = left.Z - right.Z,
      W = left.W - right.W
    };

    public static bool operator ==(Quaternion left, Quaternion right) => Quaternion.Equals(ref left, ref right);

    public static bool operator !=(Quaternion left, Quaternion right) => !Quaternion.Equals(ref left, ref right) || 0U > 0U;

    public override string ToString()
    {
      object[] objArray = new object[4];
      float x = this.X;
      objArray[0] = (object) x.ToString();
      float y = this.Y;
      objArray[1] = (object) y.ToString();
      float z = this.Z;
      objArray[2] = (object) z.ToString();
      float w = this.W;
      objArray[3] = (object) w.ToString();
      return string.Format("X:{0} Y:{1} Z:{2} W:{3}", objArray);
    }

    public override int GetHashCode()
    {
      float x = this.X;
      float y = this.Y;
      float z = this.Z;
      float w = this.W;
      int num = z.GetHashCode() + w.GetHashCode() + y.GetHashCode();
      return x.GetHashCode() + num;
    }

    public static bool Equals(ref Quaternion value1, ref Quaternion value2) => (double) value1.X == (double) value2.X && (double) value1.Y == (double) value2.Y && (double) value1.Z == (double) value2.Z && (double) value1.W == (double) value2.W && 1U > 0U;

    public bool Equals(Quaternion other) => (double) this.X == (double) other.X && (double) this.Y == (double) other.Y && (double) this.Z == (double) other.Z && (double) this.W == (double) other.W && 1U > 0U;

    public override bool Equals(object obj) => obj != null && obj.GetType() == this.GetType() && this.Equals((Quaternion) obj);
  }
}
