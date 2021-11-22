// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxTag
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System.Text.RegularExpressions;

namespace PmxLib
{
  internal static class PmxTag
  {
    public const string GROUPNAME = "gp";

    public static MatchCollection MatchsTag(
      string tag,
      string text,
      string groupName)
    {
      if (groupName == null)
        groupName = "gp";
      string str1 = "<" + tag + ">";
      string str2 = "</" + tag + ">";
      return new Regex(str1 + "(?<" + groupName + ">.*?)" + str2, RegexOptions.IgnoreCase).Matches(text);
    }

    public static string[] GetTag(string tag, string text)
    {
      MatchCollection matchCollection = PmxTag.MatchsTag(tag, text, "gp");
      string[] strArray1;
      if (matchCollection.Count <= 0)
      {
        strArray1 = (string[]) null;
      }
      else
      {
        string[] strArray2 = new string[matchCollection.Count];
        for (int i = 0; i < matchCollection.Count; ++i)
        {
          string str = matchCollection[i].Groups["gp"].Value;
          strArray2[i] = string.IsNullOrEmpty(str) ? "" : str;
        }
        strArray1 = strArray2;
      }
      return strArray1;
    }

    public static string SetTag(string tag, string text)
    {
      string str = tag.Trim();
      return "<" + str + ">" + text + "</" + str + ">";
    }

    public static bool ExistTag(string tag, string text) => PmxTag.GetTag(tag, text) != null;

    public static string RemoveTag(string tag, string text)
    {
      MatchCollection matchCollection = PmxTag.MatchsTag(tag, text, "gp");
      for (int i = 0; i < matchCollection.Count; ++i)
        text = text.Replace(matchCollection[i].Value, "");
      return text;
    }
  }
}
