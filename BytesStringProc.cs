// Decompiled with JetBrains decompiler
// Type: PmxLib.BytesStringProc
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace PmxLib
{
  internal static class BytesStringProc
  {
    private static Encoding m_sjis = Encoding.GetEncoding("shift_jis");

    public static void SetString(byte[] buf, string s, byte fix, byte fill)
    {
      if (fill == (byte) 0)
        fill = (byte) 253;
      s = s.Replace("\r\n", "\n");
      List<byte> byteList = new List<byte>((IEnumerable<byte>) BytesStringProc.m_sjis.GetBytes(s));
      byteList.Add(fix);
      if (byteList.Count > buf.Length)
        byteList[buf.Length - 1] = fix;
      for (int index = 0; index < buf.Length; ++index)
        buf[index] = fill;
      byte[] array = byteList.ToArray();
      int length = Math.Min(buf.Length, array.Length);
      Array.Copy((Array) array, (Array) buf, length);
    }

    public static string GetString(byte[] buf, byte fix)
    {
      int length = buf.Length;
      int count = buf.Length;
      for (int index = 0; index < length; ++index)
      {
        if ((int) buf[index] == (int) fix)
        {
          count = index;
          break;
        }
      }
      string str = BytesStringProc.m_sjis.GetString(buf, 0, count);
      return str != null ? str.Replace("\n", "\r\n") : "";
    }

    public static byte[] StringToBuf_SJIS(string s) => s.Length > 0 ? BytesStringProc.m_sjis.GetBytes(s) : new byte[0];

    public static string BufToString_SJIS(byte[] buf) => buf.Length != 0 ? BytesStringProc.m_sjis.GetString(buf, 0, buf.Length) : "";
  }
}
