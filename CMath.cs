// Decompiled with JetBrains decompiler
// Type: PmxLib.CMath
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;

namespace PmxLib
{
  internal static class CMath
  {
    public static float CrossVector2(Vector2 p, Vector2 q) => (float) ((double) p.X * (double) q.Y - (double) p.Y * (double) q.X);

    public static Vector2 NormalizeValue(Vector2 val)
    {
      if (float.IsNaN(val.X))
        val.X = 0.0f;
      if (float.IsNaN(val.Y))
        val.Y = 0.0f;
      return val;
    }

    public static Vector3 NormalizeValue(Vector3 val)
    {
      if (float.IsNaN(val.X))
        val.X = 0.0f;
      if (float.IsNaN(val.Y))
        val.Y = 0.0f;
      if (float.IsNaN(val.Z))
        val.Z = 0.0f;
      return val;
    }

    public static Vector4 NormalizeValue(Vector4 val)
    {
      if (float.IsNaN(val.X))
        val.X = 0.0f;
      if (float.IsNaN(val.Y))
        val.Y = 0.0f;
      if (float.IsNaN(val.Z))
        val.Z = 0.0f;
      if (float.IsNaN(val.W))
        val.W = 0.0f;
      return val;
    }

    public static bool GetIntersectPoint(
      Vector2 p0,
      Vector2 p1,
      Vector2 q0,
      Vector2 q1,
      ref Vector2 rate,
      ref Vector2 pos)
    {
      Vector2 vector2 = p1 - p0;
      Vector2 q = q1 - q0;
      float num1 = CMath.CrossVector2(vector2, q);
      bool flag;
      if ((double) num1 == 0.0)
      {
        flag = false;
      }
      else
      {
        Vector2 p = q0 - p0;
        float num2 = CMath.CrossVector2(p, vector2);
        float num3 = CMath.CrossVector2(p, q);
        float y = num2 / num1;
        float x = num3 / num1;
        if ((double) x + 9.99999974737875E-06 < 0.0 || (double) x - 9.99999974737875E-06 > 1.0 || (double) y + 9.99999974737875E-06 < 0.0 || (double) y - 9.99999974737875E-06 > 1.0)
        {
          flag = false;
        }
        else
        {
          rate = new Vector2(x, y);
          pos = p0 + vector2 * x;
          flag = true;
        }
      }
      return flag;
    }

    public static Vector3 GetFaceNormal(Vector3 p0, Vector3 p1, Vector3 p2)
    {
      Vector3 vector3 = Vector3.Cross(p1 - p0, p2 - p0);
      vector3.Normalize();
      return vector3;
    }

    public static Vector3 GetFaceOrigin(Vector3 p0, Vector3 p1, Vector3 p2) => (p0 + p1 + p2) / 3f;

    public static Vector3 GetTriangleIntersect(
      Vector3 org,
      Vector3 dir,
      Vector3 v0,
      Vector3 v1,
      Vector3 v2)
    {
      Vector3 vector3_1 = new Vector3(-1f, 0.0f, 0.0f);
      Vector3 vector3_2 = v1 - v0;
      Vector3 vector3_3 = v2 - v0;
      Vector3 rhs1 = Vector3.Cross(dir, vector3_3);
      float num1 = Vector3.Dot(vector3_2, rhs1);
      float num2;
      Vector3 rhs2;
      float num3;
      if ((double) num1 > 1.0 / 1000.0)
      {
        Vector3 lhs = org - v0;
        num2 = Vector3.Dot(lhs, rhs1);
        if ((double) num2 < 0.0 || (double) num2 > (double) num1)
          return vector3_1;
        rhs2 = Vector3.Cross(lhs, vector3_2);
        num3 = Vector3.Dot(dir, rhs2);
        if ((double) num3 < 0.0 || (double) num2 + (double) num3 > (double) num1)
          return vector3_1;
      }
      else
      {
        if ((double) num1 >= -1.0 / 1000.0)
          return vector3_1;
        Vector3 lhs = org - v0;
        num2 = Vector3.Dot(lhs, rhs1);
        if ((double) num2 > 0.0 || (double) num2 < (double) num1)
          return vector3_1;
        rhs2 = Vector3.Cross(lhs, vector3_2);
        num3 = Vector3.Dot(dir, rhs2);
        if ((double) num3 > 0.0 || (double) num2 + (double) num3 < (double) num1)
          return vector3_1;
      }
      float num4 = 1f / num1;
      float num5 = Vector3.Dot(vector3_3, rhs2) * num4;
      float num6 = num2 * num4;
      float num7 = num3 * num4;
      vector3_1.X = num5;
      vector3_1.Y = num6;
      vector3_1.Z = num7;
      return vector3_1;
    }

    public static Vector3 GetLineCrossPoint(
      Vector3 p,
      Vector3 from,
      Vector3 dir,
      out float d)
    {
      Vector3 rhs = p - from;
      d = Vector3.Dot(dir, rhs);
      return dir * d + from;
    }

    public static Vector3 GetLineCrossPoint(Vector3 p, Vector3 from, Vector3 dir) => CMath.GetLineCrossPoint(p, from, dir, out float _);

    public static Matrix CreateViewportMatrix(int width, int height)
    {
      Matrix identity = Matrix.Identity;
      float num1 = (float) width * 0.5f;
      float num2 = (float) height * 0.5f;
      identity.M11 = num1;
      identity.M22 = -num2;
      identity.M41 = num1;
      identity.M42 = num2;
      return identity;
    }

    public static bool InArcPosition(Vector3 pos, float cx, float cy, float r2, out float d2)
    {
      float num1 = pos.X - cx;
      float num2 = pos.Y - cy;
      d2 = (float) ((double) num1 * (double) num1 + (double) num2 * (double) num2);
      return (double) d2 <= (double) r2;
    }

    public static bool InArcPosition(Vector3 pos, float cx, float cy, float r2) => CMath.InArcPosition(pos, cx, cy, r2, out float _);

    public static void NormalizeRotateXYZ(ref Vector3 v)
    {
      if ((double) v.X < -3.14159274101257)
        v.X += 6.283185f;
      else if ((double) v.X > 3.14159274101257)
        v.X -= 6.283185f;
      if ((double) v.Y < -3.14159274101257)
        v.Y += 6.283185f;
      else if ((double) v.Y > 3.14159274101257)
        v.Y -= 6.283185f;
      if ((double) v.Z < -3.14159274101257)
        v.Z += 6.283185f;
      else if ((double) v.Z > 3.14159274101257)
        v.Z -= 6.283185f;
    }

    public static Vector3 MatrixToEuler_ZXY0(ref Matrix m)
    {
      Vector3 zero = Vector3.Zero;
      if ((double) m.M32 == 1.0)
      {
        zero.X = 1.570796f;
        zero.Z = (float) Math.Atan2((double) m.M21, (double) m.M11);
      }
      else if ((double) m.M32 == -1.0)
      {
        zero.X = -1.570796f;
        zero.Z = (float) Math.Atan2((double) m.M21, (double) m.M11);
      }
      else
      {
        zero.X = -(float) Math.Asin((double) m.M32);
        zero.Y = -(float) Math.Atan2(-(double) m.M31, (double) m.M33);
        zero.Z = -(float) Math.Atan2(-(double) m.M12, (double) m.M22);
      }
      return zero;
    }

    public static Vector3 MatrixToEuler_ZXY(ref Matrix m)
    {
      Vector3 zero = Vector3.Zero;
      zero.X = -(float) Math.Asin((double) m.M32);
      if ((double) zero.X == 1.57079637050629 || (double) zero.X == -1.57079637050629)
      {
        zero.Y = (float) Math.Atan2(-(double) m.M13, (double) m.M11);
      }
      else
      {
        zero.Y = (float) Math.Atan2((double) m.M31, (double) m.M33);
        zero.Z = (float) Math.Asin((double) m.M12 / Math.Cos((double) zero.X));
        if ((double) m.M22 < 0.0)
          zero.Z = 3.141593f - zero.Z;
      }
      return zero;
    }

    public static Vector3 MatrixToEuler_XYZ(ref Matrix m)
    {
      Vector3 zero = Vector3.Zero;
      zero.Y = -(float) Math.Asin((double) m.M13);
      if ((double) zero.Y == 1.57079637050629 || (double) zero.Y == -1.57079637050629)
      {
        zero.Z = (float) Math.Atan2(-(double) m.M21, (double) m.M22);
      }
      else
      {
        zero.Z = (float) Math.Atan2((double) m.M12, (double) m.M11);
        zero.X = (float) Math.Asin((double) m.M23 / Math.Cos((double) zero.Y));
        if ((double) m.M33 < 0.0)
          zero.X = 3.141593f - zero.X;
      }
      return zero;
    }

    public static Vector3 MatrixToEuler_YZX(ref Matrix m)
    {
      Vector3 zero = Vector3.Zero;
      zero.Z = -(float) Math.Asin((double) m.M21);
      if ((double) zero.Z == 1.57079637050629 || (double) zero.Z == -1.57079637050629)
      {
        zero.X = (float) Math.Atan2(-(double) m.M32, (double) m.M33);
      }
      else
      {
        zero.X = (float) Math.Atan2((double) m.M23, (double) m.M22);
        zero.Y = (float) Math.Asin((double) m.M31 / Math.Cos((double) zero.Z));
        if ((double) m.M11 < 0.0)
          zero.Y = 3.141593f - zero.Y;
      }
      return zero;
    }

    public static Vector3 MatrixToEuler_ZXY_Lim2(ref Matrix m)
    {
      Vector3 zero = Vector3.Zero;
      zero.X = -(float) Math.Asin((double) m.M32);
      if ((double) zero.X == 1.57079637050629 || (double) zero.X == -1.57079637050629)
      {
        zero.Y = (float) Math.Atan2(-(double) m.M13, (double) m.M11);
      }
      else
      {
        if (1.53588974475861 < (double) zero.X)
          zero.X = 22f * (float) Math.PI / 45f;
        else if ((double) zero.X < -1.53588974475861)
          zero.X = -22f * (float) Math.PI / 45f;
        zero.Y = (float) Math.Atan2((double) m.M31, (double) m.M33);
        zero.Z = (float) Math.Asin((double) m.M12 / Math.Cos((double) zero.X));
        if ((double) m.M22 < 0.0)
          zero.Z = 3.141593f - zero.Z;
      }
      return zero;
    }

    public static Vector3 MatrixToEuler_XYZ_Lim2(ref Matrix m)
    {
      Vector3 zero = Vector3.Zero;
      zero.Y = -(float) Math.Asin((double) m.M13);
      if ((double) zero.Y == 1.57079637050629 || (double) zero.Y == -1.57079637050629)
      {
        zero.Z = (float) Math.Atan2(-(double) m.M21, (double) m.M22);
      }
      else
      {
        if (1.53588974475861 < (double) zero.Y)
          zero.Y = 22f * (float) Math.PI / 45f;
        else if ((double) zero.Y < -1.53588974475861)
          zero.Y = -22f * (float) Math.PI / 45f;
        zero.Z = (float) Math.Atan2((double) m.M12, (double) m.M11);
        zero.X = (float) Math.Asin((double) m.M23 / Math.Cos((double) zero.Y));
        if ((double) m.M33 < 0.0)
          zero.X = 3.141593f - zero.X;
      }
      return zero;
    }

    public static Vector3 MatrixToEuler_YZX_Lim2(ref Matrix m)
    {
      Vector3 zero = Vector3.Zero;
      zero.Z = -(float) Math.Asin((double) m.M21);
      if ((double) zero.Z == 1.57079637050629 || (double) zero.Z == -1.57079637050629)
      {
        zero.X = (float) Math.Atan2(-(double) m.M32, (double) m.M33);
      }
      else
      {
        if (1.53588974475861 < (double) zero.Z)
          zero.Z = 22f * (float) Math.PI / 45f;
        else if ((double) zero.Z < -1.53588974475861)
          zero.Z = -22f * (float) Math.PI / 45f;
        zero.X = (float) Math.Atan2((double) m.M23, (double) m.M22);
        zero.Y = (float) Math.Asin((double) m.M31 / Math.Cos((double) zero.Z));
        if ((double) m.M11 < 0.0)
          zero.Y = 3.141593f - zero.Y;
      }
      return zero;
    }
  }
}
