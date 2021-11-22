// Decompiled with JetBrains decompiler
// Type: PmxLib.SystemToon
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System.Collections.Generic;

namespace PmxLib
{
  internal class SystemToon
  {
    public const int EnableToonCount = 10;
    private static Dictionary<string, int> m_nametable;

    public static string GetToonName(int n) => n >= 0 ? "toon" + (n + 1).ToString("00") + ".bmp" : "toon0.bmp";

    public static string[] GetToonNames()
    {
      string[] strArray = new string[10];
      for (int n = 0; n < strArray.Length; ++n)
        strArray[n] = SystemToon.GetToonName(n);
      return strArray;
    }

    private static void CreateNameTable()
    {
      int num = 10;
      SystemToon.m_nametable = new Dictionary<string, int>(num + 1);
      for (int n = -1; n < num; ++n)
        SystemToon.m_nametable.Add(SystemToon.GetToonName(n), n);
    }

    public static bool IsSystemToon(string name)
    {
      if (SystemToon.m_nametable == null)
        SystemToon.CreateNameTable();
      return !string.IsNullOrEmpty(name) && SystemToon.m_nametable.ContainsKey(name);
    }

    public static int GetToonIndex(string name)
    {
      if (SystemToon.m_nametable == null)
        SystemToon.CreateNameTable();
      return string.IsNullOrEmpty(name) || !SystemToon.m_nametable.ContainsKey(name) ? -2 : SystemToon.m_nametable[name];
    }

    public SystemToon.ToonInfo GetToonInfo(List<PmxMaterial> list)
    {
      int count = list.Count;
      SystemToon.ToonInfo toonInfo1;
      if (count < 0)
      {
        toonInfo1 = (SystemToon.ToonInfo) null;
      }
      else
      {
        SystemToon.ToonInfo toonInfo2 = new SystemToon.ToonInfo(count);
        bool[] flagArray = new bool[10];
        List<string> stringList = new List<string>(count);
        Dictionary<string, int> dictionary1 = new Dictionary<string, int>(count);
        for (int index = 0; index < count; ++index)
        {
          PmxMaterial pmxMaterial = list[index];
          if (string.IsNullOrEmpty(pmxMaterial.Toon))
          {
            toonInfo2.MaterialToon[index] = -1;
          }
          else
          {
            int toonIndex = SystemToon.GetToonIndex(pmxMaterial.Toon);
            toonInfo2.MaterialToon[index] = toonIndex;
            if (-1 <= toonIndex && toonIndex < 10)
            {
              if (toonIndex >= 0)
                flagArray[toonIndex] = true;
            }
            else if (!dictionary1.ContainsKey(pmxMaterial.Toon))
            {
              stringList.Add(pmxMaterial.Toon);
              dictionary1.Add(pmxMaterial.Toon, 0);
            }
          }
        }
        if (stringList.Count > 0)
        {
          Dictionary<string, int> dictionary2 = new Dictionary<string, int>(stringList.Count);
          int num = 0;
          for (int index1 = 0; index1 < stringList.Count; ++index1)
          {
            for (int index2 = num; index2 < flagArray.Length; ++index2)
            {
              if (!flagArray[index2])
              {
                toonInfo2.ToonNames[index2] = stringList[index1];
                dictionary2.Add(stringList[index1], index2);
                flagArray[index2] = true;
                num = index2 + 1;
                break;
              }
            }
          }
          for (int index = 0; index < count; ++index)
          {
            if (toonInfo2.MaterialToon[index] < -1)
            {
              PmxMaterial pmxMaterial = list[index];
              if (dictionary2.ContainsKey(pmxMaterial.Toon))
              {
                toonInfo2.MaterialToon[index] = dictionary2[pmxMaterial.Toon];
              }
              else
              {
                toonInfo2.MaterialToon[index] = -1;
                toonInfo2.IsRejection = true;
              }
            }
          }
        }
        toonInfo1 = toonInfo2;
      }
      return toonInfo1;
    }

    public class ToonInfo
    {
      public string[] ToonNames { get; set; }

      public int[] MaterialToon { get; set; }

      public bool IsRejection { get; set; }

      public ToonInfo(int count)
      {
        this.ToonNames = SystemToon.GetToonNames();
        this.MaterialToon = new int[count];
        this.IsRejection = false;
      }
    }
  }
}
