// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxModelInfo
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  public class PmxModelInfo : IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    PmxObject IPmxObjectKey.ObjectKey => PmxObject.ModelInfo;

    public string ModelName { get; set; }

    public string ModelNameE { get; set; }

    public string Comment { get; set; }

    public string CommentE { get; set; }

    public PmxModelInfo() => this.Clear();

    public PmxModelInfo(PmxModelInfo info) => this.FromModelInfo(info);

    public void FromModelInfo(PmxModelInfo info)
    {
      this.ModelName = info.ModelName;
      this.Comment = info.Comment;
      this.ModelNameE = info.ModelNameE;
      this.CommentE = info.CommentE;
    }

    public void Clear()
    {
      this.ModelName = "";
      this.Comment = "";
      this.ModelNameE = "";
      this.CommentE = "";
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.ModelName = PmxStreamHelper.ReadString(s, f);
      this.ModelNameE = PmxStreamHelper.ReadString(s, f);
      this.Comment = PmxStreamHelper.ReadString(s, f);
      this.CommentE = PmxStreamHelper.ReadString(s, f);
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      PmxStreamHelper.WriteString(s, this.ModelName, f);
      PmxStreamHelper.WriteString(s, this.ModelNameE, f);
      PmxStreamHelper.WriteString(s, this.Comment, f);
      PmxStreamHelper.WriteString(s, this.CommentE, f);
    }

    object ICloneable.Clone() => (object) new PmxModelInfo(this);

    public PmxModelInfo Clone() => new PmxModelInfo(this);
  }
}
