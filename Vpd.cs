// Decompiled with JetBrains decompiler
// Type: PmxLib.Vpd
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PmxLib
{
  public class Vpd
  {
    public static string VpdHeader = "Vocaloid Pose Data file";
    private static string HeadGetReg = "^[^V]*Vocaloid Pose Data file";
    private static string InfoGetReg = "^[^V]*Vocaloid Pose Data file[^\\n]*\\n[\\n\\s]*(?<name>[^;]+);[\\s]*[^\\n]*\\n+(?<num>[^;]+);";
    private static string BoneGetReg = "\\n+\\s*Bone(?<no>\\d+)\\s*\\{\\s*(?<name>[^\\r\\n]+)[\\r\\n]+\\s*(?<trans_x>[^,]+),(?<trans_y>[^,]+),(?<trans_z>[^;]+);[^\\n]*\\n+(?<rot_x>[^,]+),(?<rot_y>[^,]+),(?<rot_z>[^,]+),(?<rot_w>[^;]+);[^\\n]*\\n+\\s*\\}";
    private static string MorphGetReg = "\\n+\\s*Morph(?<no>\\d+)\\s*\\{\\s*(?<name>[^\\r\\n]+)[\\r\\n]+\\s*(?<val>[^;]+);[^\\n]*\\n+\\s*\\}";
    private static string NameExt = ".osm";

    public string ModelName { get; set; }

    public List<Vpd.PoseData> PoseList { get; private set; }

    public List<Vpd.MorphData> MorphList { get; private set; }

    public bool Extend { get; set; }

    public Vpd()
    {
      this.PoseList = new List<Vpd.PoseData>();
      this.MorphList = new List<Vpd.MorphData>();
      this.Extend = true;
    }

    public static bool IsVpdText(string text) => new Regex(Vpd.HeadGetReg, RegexOptions.IgnoreCase).IsMatch(text);

    public bool FromText(string text)
    {
      bool flag = false;
      try
      {
        if (!Vpd.IsVpdText(text))
          return flag;
        Match match1 = new Regex(Vpd.InfoGetReg, RegexOptions.IgnoreCase).Match(text);
        if (match1.Success)
        {
          string str = match1.Groups["name"].Value;
          if (str.ToLower().Contains(Vpd.NameExt))
            str = str.Replace(Vpd.NameExt, "");
          this.ModelName = str;
        }
        this.PoseList.Clear();
        for (Match match2 = new Regex(Vpd.BoneGetReg, RegexOptions.IgnoreCase).Match(text); match2.Success; match2 = match2.NextMatch())
        {
          Vector3 t = new Vector3(0.0f, 0.0f, 0.0f);
          Quaternion identity = Quaternion.Identity;
          string name = match2.Groups["name"].Value;
          float result1;
          float.TryParse(match2.Groups["trans_x"].Value, out result1);
          float result2;
          float.TryParse(match2.Groups["trans_y"].Value, out result2);
          float result3;
          float.TryParse(match2.Groups["trans_z"].Value, out result3);
          t.x = result1;
          t.y = result2;
          t.z = result3;
          float.TryParse(match2.Groups["rot_x"].Value, out result1);
          float.TryParse(match2.Groups["rot_y"].Value, out result2);
          float.TryParse(match2.Groups["rot_z"].Value, out result3);
          float result4;
          float.TryParse(match2.Groups["rot_w"].Value, out result4);
          identity.x = result1;
          identity.y = result2;
          identity.z = result3;
          identity.w = result4;
          this.PoseList.Add(new Vpd.PoseData(name, identity, t));
        }
        this.MorphList.Clear();
        for (Match match3 = new Regex(Vpd.MorphGetReg, RegexOptions.IgnoreCase).Match(text); match3.Success; match3 = match3.NextMatch())
        {
          float result = 0.0f;
          string name = match3.Groups["name"].Value;
          float.TryParse(match3.Groups["val"].Value, out result);
          this.MorphList.Add(new Vpd.MorphData(name, result));
        }
        flag = true;
      }
      catch (Exception ex)
      {
      }
      return flag;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(Vpd.VpdHeader);
      stringBuilder.AppendLine();
      stringBuilder.AppendLine(this.ModelName + Vpd.NameExt + ";");
      stringBuilder.AppendLine(this.PoseList.Count.ToString() + ";");
      stringBuilder.AppendLine();
      for (int index = 0; index < this.PoseList.Count; ++index)
        stringBuilder.AppendLine("Bone" + index.ToString() + this.PoseList[index].ToString());
      if (this.Extend)
      {
        for (int index = 0; index < this.MorphList.Count; ++index)
          stringBuilder.AppendLine("Morph" + index.ToString() + this.MorphList[index].ToString());
      }
      return stringBuilder.ToString();
    }

    public class PoseData
    {
      public Quaternion Rotation;
      public Vector3 Translation;

      public string BoneName { get; set; }

      public PoseData()
      {
      }

      public PoseData(string name, Quaternion r, Vector3 t)
      {
        this.BoneName = name;
        this.Rotation = r;
        this.Translation = t;
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("{" + this.BoneName);
        string format = "0.000000";
        stringBuilder.AppendLine("  " + this.Translation.X.ToString(format) + "," + this.Translation.Y.ToString(format) + "," + this.Translation.Z.ToString(format) + ";");
        stringBuilder.AppendLine("  " + this.Rotation.X.ToString(format) + "," + this.Rotation.Y.ToString(format) + "," + this.Rotation.Z.ToString(format) + "," + this.Rotation.W.ToString(format) + ";");
        stringBuilder.AppendLine("}");
        return stringBuilder.ToString();
      }
    }

    public class MorphData
    {
      public float Value;

      public string MorphName { get; set; }

      public MorphData()
      {
      }

      public MorphData(string name, float val)
      {
        this.MorphName = name;
        this.Value = val;
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("{" + this.MorphName);
        stringBuilder.AppendLine("  " + this.Value.ToString() + ";");
        stringBuilder.AppendLine("}");
        return stringBuilder.ToString();
      }
    }
  }
}
