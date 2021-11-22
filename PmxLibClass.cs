// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxLibClass
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;

namespace PmxLib
{
  internal static class PmxLibClass
  {
    private static bool m_lock = false;

    public static void Unlock(string key)
    {
      PmxLibClass.m_lock = true;
      if (!(key == PmxLibClass.RString(-167698971, "UnlockPmxLibClass")))
        return;
      PmxLibClass.m_lock = false;
    }

    public static bool IsLocked() => PmxLibClass.m_lock;

    public static string RString(int s, string str)
    {
      Random random = new Random(s);
      char[] charArray = str.ToCharArray();
      int length = charArray.Length;
      while (length > 1)
      {
        --length;
        int index = random.Next(length + 1);
        char ch = charArray[index];
        charArray[index] = charArray[length];
        charArray[length] = ch;
      }
      return new string(charArray);
    }
  }
}
