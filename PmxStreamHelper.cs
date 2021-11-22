// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxStreamHelper
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;
using System.Text;

namespace PmxLib
{
  internal static class PmxStreamHelper
  {
    public static void WriteString(Stream s, string str, int f)
    {
      switch (f)
      {
        case 0:
          PmxStreamHelper.WriteString_v2(s, str, Encoding.Unicode);
          break;
        case 1:
          PmxStreamHelper.WriteString_v2(s, str, Encoding.UTF8);
          break;
      }
    }

    public static string ReadString(Stream s, int f)
    {
      string str = "";
      switch (f)
      {
        case 0:
          str = PmxStreamHelper.ReadString_v2(s, Encoding.Unicode);
          break;
        case 1:
          str = PmxStreamHelper.ReadString_v2(s, Encoding.UTF8);
          break;
      }
      return str;
    }

    public static void WriteString(Stream s, string str, PmxElementFormat f)
    {
      if (f == null)
        f = new PmxElementFormat(2.1f);
      if ((double) f.Ver <= 1.0)
        PmxStreamHelper.WriteString_v1(s, str);
      else if ((double) f.Ver <= 2.09999990463257)
      {
        if (f.StringEnc == PmxElementFormat.StringEncType.UTF8)
          PmxStreamHelper.WriteString_v2(s, str, Encoding.UTF8);
        else
          PmxStreamHelper.WriteString_v2(s, str, Encoding.Unicode);
      }
    }

    public static string ReadString(Stream s, PmxElementFormat f)
    {
      if (f == null)
        f = new PmxElementFormat(2.1f);
      string str = "";
      if ((double) f.Ver <= 1.0)
        str = PmxStreamHelper.ReadString_v1(s);
      else if ((double) f.Ver <= 2.09999990463257)
        str = f.StringEnc != PmxElementFormat.StringEncType.UTF8 ? PmxStreamHelper.ReadString_v2(s, Encoding.Unicode) : PmxStreamHelper.ReadString_v2(s, Encoding.UTF8);
      return str;
    }

    public static void WriteString_v1(Stream s, string str)
    {
      byte[] bufSjis = BytesStringProc.StringToBuf_SJIS(str);
      byte[] bytes = BitConverter.GetBytes(bufSjis.Length);
      s.Write(bytes, 0, bytes.Length);
      if ((uint) bufSjis.Length <= 0U)
        return;
      s.Write(bufSjis, 0, bufSjis.Length);
    }

    public static string ReadString_v1(Stream s)
    {
      string str = "";
      byte[] buffer = new byte[4];
      s.Read(buffer, 0, 4);
      int int32 = BitConverter.ToInt32(buffer, 0);
      if (int32 > 0)
      {
        byte[] numArray = new byte[int32];
        s.Read(numArray, 0, int32);
        str = BytesStringProc.BufToString_SJIS(numArray);
      }
      return str;
    }

    public static void WriteString_v2(Stream s, string str, Encoding ec)
    {
      if (ec == null)
        ec = Encoding.Unicode;
      byte[] bytes1 = ec.GetBytes(str);
      byte[] bytes2 = BitConverter.GetBytes(bytes1.Length);
      s.Write(bytes2, 0, bytes2.Length);
      if ((uint) bytes1.Length <= 0U)
        return;
      s.Write(bytes1, 0, bytes1.Length);
    }

    public static string ReadString_v2(Stream s, Encoding ec)
    {
      if (ec == null)
        ec = Encoding.Unicode;
      string str = "";
      byte[] buffer = new byte[4];
      s.Read(buffer, 0, 4);
      int int32 = BitConverter.ToInt32(buffer, 0);
      if (int32 > 0)
      {
        byte[] numArray = new byte[int32];
        if (s.Read(numArray, 0, int32) > 0)
          str = ec.GetString(numArray, 0, numArray.Length);
      }
      return str;
    }

    public static void WriteElement_Bool(Stream s, bool data) => PmxStreamHelper.WriteElement_Int32(s, data ? 1 : 0, 1, false);

    public static bool ReadElement_Bool(Stream s) => (uint) PmxStreamHelper.ReadElement_Int32(s, 1, false) > 0U;

    public static void WriteElement_Int32(Stream s, int data, int bufSize, bool signed)
    {
      byte[] buffer = (byte[]) null;
      switch (bufSize)
      {
        case 1:
          if (signed)
          {
            buffer = new byte[1]{ (byte) data };
            break;
          }
          buffer = new byte[1]{ (byte) data };
          break;
        case 2:
          buffer = !signed ? BitConverter.GetBytes((ushort) data) : BitConverter.GetBytes((short) data);
          break;
        case 4:
          buffer = BitConverter.GetBytes(data);
          break;
      }
      s.Write(buffer, 0, buffer.Length);
    }

    public static int ReadElement_Int32(Stream s, int bufSize, bool signed)
    {
      int num = 0;
      byte[] buffer = new byte[bufSize];
      s.Read(buffer, 0, bufSize);
      switch (bufSize)
      {
        case 1:
          num = !signed ? (int) buffer[0] : (int) (sbyte) buffer[0];
          break;
        case 2:
          num = !signed ? (int) BitConverter.ToUInt16(buffer, 0) : (int) BitConverter.ToInt16(buffer, 0);
          break;
        case 4:
          num = BitConverter.ToInt32(buffer, 0);
          break;
      }
      return num;
    }

    public static void WriteElement_UInt(Stream s, uint data)
    {
      byte[] bytes = BitConverter.GetBytes(data);
      s.Write(bytes, 0, bytes.Length);
    }

    public static uint ReadElement_UInt(Stream s)
    {
      byte[] buffer = new byte[4];
      s.Read(buffer, 0, 4);
      return BitConverter.ToUInt32(buffer, 0);
    }

    public static void WriteElement_Float(Stream s, float data)
    {
      byte[] bytes = BitConverter.GetBytes(data);
      s.Write(bytes, 0, bytes.Length);
    }

    public static float ReadElement_Float(Stream s)
    {
      byte[] buffer = new byte[4];
      s.Read(buffer, 0, 4);
      return BitConverter.ToSingle(buffer, 0);
    }

    public static void WriteElement_Vector2(Stream s, Vector2 data)
    {
      PmxStreamHelper.WriteElement_Float(s, data.X);
      PmxStreamHelper.WriteElement_Float(s, data.Y);
    }

    public static Vector2 ReadElement_Vector2(Stream s) => new Vector2()
    {
      X = PmxStreamHelper.ReadElement_Float(s),
      Y = PmxStreamHelper.ReadElement_Float(s)
    };

    public static void WriteElement_Vector3(Stream s, Vector3 data)
    {
      PmxStreamHelper.WriteElement_Float(s, data.X);
      PmxStreamHelper.WriteElement_Float(s, data.Y);
      PmxStreamHelper.WriteElement_Float(s, data.Z);
    }

    public static Vector3 ReadElement_Vector3(Stream s) => new Vector3()
    {
      X = PmxStreamHelper.ReadElement_Float(s),
      Y = PmxStreamHelper.ReadElement_Float(s),
      Z = PmxStreamHelper.ReadElement_Float(s)
    };

    public static void WriteElement_Vector4(Stream s, Vector4 data)
    {
      PmxStreamHelper.WriteElement_Float(s, data.X);
      PmxStreamHelper.WriteElement_Float(s, data.Y);
      PmxStreamHelper.WriteElement_Float(s, data.Z);
      PmxStreamHelper.WriteElement_Float(s, data.W);
    }

    public static Vector4 ReadElement_Vector4(Stream s) => new Vector4()
    {
      X = PmxStreamHelper.ReadElement_Float(s),
      Y = PmxStreamHelper.ReadElement_Float(s),
      Z = PmxStreamHelper.ReadElement_Float(s),
      W = PmxStreamHelper.ReadElement_Float(s)
    };

    public static void WriteElement_Quaternion(Stream s, Quaternion data)
    {
      PmxStreamHelper.WriteElement_Float(s, data.X);
      PmxStreamHelper.WriteElement_Float(s, data.Y);
      PmxStreamHelper.WriteElement_Float(s, data.Z);
      PmxStreamHelper.WriteElement_Float(s, data.W);
    }

    public static Quaternion ReadElement_Quaternion(Stream s) => new Quaternion()
    {
      X = PmxStreamHelper.ReadElement_Float(s),
      Y = PmxStreamHelper.ReadElement_Float(s),
      Z = PmxStreamHelper.ReadElement_Float(s),
      W = PmxStreamHelper.ReadElement_Float(s)
    };

    public static void WriteElement_Matrix(Stream s, Matrix m)
    {
      foreach (float data in m.ToArray())
        PmxStreamHelper.WriteElement_Float(s, data);
    }

    public static Matrix ReadElement_Matrix(Stream s)
    {
      Matrix matrix;
      matrix.M11 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M12 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M13 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M14 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M21 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M22 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M23 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M24 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M31 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M32 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M33 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M34 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M41 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M42 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M43 = PmxStreamHelper.ReadElement_Float(s);
      matrix.M44 = PmxStreamHelper.ReadElement_Float(s);
      return matrix;
    }
  }
}
