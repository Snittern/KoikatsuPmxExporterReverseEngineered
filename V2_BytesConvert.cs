// Decompiled with JetBrains decompiler
// Type: PmxLib.V2_BytesConvert
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  internal static class V2_BytesConvert
  {
    public static readonly int UnitBytes = 8;

    public static int ByteCount => V2_BytesConvert.UnitBytes;

    public static byte[] ToBytes(Vector2 v2)
    {
      List<byte> byteList = new List<byte>();
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v2.x));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(v2.y));
      return byteList.ToArray();
    }

    public static Vector2 FromBytes(byte[] bytes, int startIndex)
    {
      int num = 4;
      return new Vector2(BitConverter.ToSingle(bytes, startIndex), BitConverter.ToSingle(bytes, startIndex + num));
    }

    public static Vector2 FromStream(Stream s)
    {
      Vector2 zero = Vector2.zero;
      byte[] buffer = new byte[8];
      s.Read(buffer, 0, 8);
      int startIndex1 = 0;
      zero.x = BitConverter.ToSingle(buffer, startIndex1);
      int startIndex2 = startIndex1 + 4;
      zero.y = BitConverter.ToSingle(buffer, startIndex2);
      return zero;
    }

    public static void ToStream(Stream s, Vector2 v)
    {
      s.Write(BitConverter.GetBytes(v.x), 0, 4);
      s.Write(BitConverter.GetBytes(v.y), 0, 4);
    }
  }
}
