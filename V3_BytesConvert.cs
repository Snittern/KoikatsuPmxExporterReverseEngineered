// Decompiled with JetBrains decompiler
// Type: PmxLib.V3_BytesConvert
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PmxLib
{
  internal static class V3_BytesConvert
  {
    public static readonly int UnitBytes = 12;

    public static int ByteCount => V3_BytesConvert.UnitBytes;

    public static byte[] ToBytes(Vector3 v3)
    {
      List<byte> byteList = new List<byte>();
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v3.x));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v3.y));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v3.z));
      return byteList.ToArray();
    }

    public static Vector3 FromBytes(byte[] bytes, int startIndex)
    {
      int num = 4;
      return new Vector3(BitConverter.ToSingle(bytes, startIndex), BitConverter.ToSingle(bytes, startIndex + num), BitConverter.ToSingle(bytes, startIndex + num * 2));
    }

    public static Vector3 FromStream(Stream s)
    {
      Vector3 zero = Vector3.zero;
      byte[] buffer = new byte[12];
      s.Read(buffer, 0, 12);
      int startIndex1 = 0;
      zero.x = BitConverter.ToSingle(buffer, startIndex1);
      int startIndex2 = startIndex1 + 4;
      zero.y = BitConverter.ToSingle(buffer, startIndex2);
      int startIndex3 = startIndex2 + 4;
      zero.z = BitConverter.ToSingle(buffer, startIndex3);
      return zero;
    }

    public static void ToStream(Stream s, Vector3 v)
    {
      s.Write(BitConverter.GetBytes(v.x), 0, 4);
      s.Write(BitConverter.GetBytes(v.y), 0, 4);
      s.Write(BitConverter.GetBytes(v.z), 0, 4);
    }

    public static Color Vector3ToColor(Vector3 v) => new Color()
    {
      r = (float) v.x,
      g = (float) v.y,
      b = (float) v.z
    };

    public static Vector3 ColorToVector3(Color color) => new Vector3()
    {
      x = (float) color.r,
      y = (float) color.g,
      z = (float) color.b
    };
  }
}
