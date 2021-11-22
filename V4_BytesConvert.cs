// Decompiled with JetBrains decompiler
// Type: PmxLib.V4_BytesConvert
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PmxLib
{
  internal static class V4_BytesConvert
  {
    public static readonly int UnitBytes = 16;

    public static int ByteCount => V4_BytesConvert.UnitBytes;

    public static byte[] ToBytes(Vector4 v4)
    {
      List<byte> byteList = new List<byte>();
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v4.x));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v4.y));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v4.z));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v4.w));
      return byteList.ToArray();
    }

    public static Vector4 FromBytes(byte[] bytes, int startIndex)
    {
      int num = 4;
      return new Vector4(BitConverter.ToSingle(bytes, startIndex), BitConverter.ToSingle(bytes, startIndex + num), BitConverter.ToSingle(bytes, startIndex + num * 2), BitConverter.ToSingle(bytes, startIndex + num * 3));
    }

    public static Vector4 FromStream(Stream s)
    {
      Vector4 zero = Vector4.zero;
      byte[] buffer = new byte[16];
      s.Read(buffer, 0, 16);
      int startIndex1 = 0;
      zero.x = BitConverter.ToSingle(buffer, startIndex1);
      int startIndex2 = startIndex1 + 4;
      zero.y = BitConverter.ToSingle(buffer, startIndex2);
      int startIndex3 = startIndex2 + 4;
      zero.z = BitConverter.ToSingle(buffer, startIndex3);
      int startIndex4 = startIndex3 + 4;
      zero.w = BitConverter.ToSingle(buffer, startIndex4);
      return zero;
    }

    public static void ToStream(Stream s, Vector4 v)
    {
      s.Write(BitConverter.GetBytes(v.x), 0, 4);
      s.Write(BitConverter.GetBytes(v.y), 0, 4);
      s.Write(BitConverter.GetBytes(v.z), 0, 4);
      s.Write(BitConverter.GetBytes(v.w), 0, 4);
    }

    public static Color Vector4ToColor(Vector4 v) => new Color()
    {
      a = (__Null) (double) v.w,
      r = (__Null) (double) v.x,
      g = (__Null) (double) v.y,
      b = (__Null) (double) v.z
    };

    public static Vector4 ColorToVector4(Color color) => new Vector4()
    {
      w = (float) color.a,
      x = (float) color.r,
      y = (float) color.g,
      z = (float) color.b
    };
  }
}
