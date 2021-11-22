// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxReadmeTag
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  internal static class PmxReadmeTag
  {
    public const string TAG_README = "readme";

    public static string[] GetReadme(Pmx pmx, string root)
    {
      if (root == null)
        root = "";
      string[] tag = PmxTag.GetTag("readme", pmx.ModelInfo.CommentE);
      if (tag != null && !string.IsNullOrEmpty(root))
      {
        for (int index = 0; index < tag.Length; ++index)
        {
          if (!Path.IsPathRooted(tag[index]))
            tag[index] = root + "\\" + tag[index];
        }
      }
      return tag;
    }

    public static void SetReadme(Pmx pmx, string path)
    {
      pmx.ModelInfo.CommentE += Environment.NewLine;
      PmxTag.SetTag("readme", pmx.ModelInfo.CommentE);
    }

    public static bool ExistReadme(Pmx pmx) => PmxTag.ExistTag("readme", pmx.ModelInfo.CommentE);

    public static void RemoveReadme(Pmx pmx)
    {
      pmx.ModelInfo.CommentE = PmxTag.RemoveTag("readme", pmx.ModelInfo.CommentE);
      pmx.ModelInfo.CommentE = pmx.ModelInfo.CommentE.Trim();
    }
  }
}
