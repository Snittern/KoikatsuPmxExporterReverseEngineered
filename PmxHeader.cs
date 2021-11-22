// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxHeader
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;
using System.Text;

namespace PmxLib
{
  public class PmxHeader : IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    public const float LastVer = 2.1f;
    public static string PmxKey_v1 = "Pmx ";
    public static string PmxKey = "PMX ";

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Header;

    public float Ver
    {
      get => this.ElementFormat.Ver;
      set => this.ElementFormat.Ver = value;
    }

    public PmxElementFormat ElementFormat { get; private set; }

    public PmxHeader(float ver)
    {
      if ((double) ver == 0.0)
        ver = 2.1f;
      this.ElementFormat = new PmxElementFormat(ver);
    }

    public PmxHeader(PmxHeader h) => this.FromHeader(h);

    public void FromHeader(PmxHeader h) => this.ElementFormat = h.ElementFormat.Clone();

    public void FromElementFormat(PmxElementFormat f) => this.ElementFormat = f;

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      byte[] numArray = new byte[4];
      s.Read(numArray, 0, numArray.Length);
      string str = Encoding.ASCII.GetString(numArray);
      if (str.Equals(PmxHeader.PmxKey_v1))
      {
        this.Ver = 1f;
        byte[] buffer = new byte[4];
        s.Read(buffer, 0, buffer.Length);
      }
      else
      {
        if (!str.Equals(PmxHeader.PmxKey))
          throw new Exception("ファイル形式が異なります.");
        byte[] buffer = new byte[4];
        s.Read(buffer, 0, buffer.Length);
        this.Ver = BitConverter.ToSingle(buffer, 0);
      }
      this.ElementFormat = (double) this.Ver <= 2.09999990463257 ? new PmxElementFormat(this.Ver) : throw new Exception("未対応のverです.");
      this.ElementFormat.FromStreamEx(s, (PmxElementFormat) null);
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      if (f == null)
        f = this.ElementFormat;
      byte[] numArray = new byte[4];
      byte[] buffer = (double) f.Ver > 1.0 ? Encoding.ASCII.GetBytes(PmxHeader.PmxKey) : Encoding.ASCII.GetBytes(PmxHeader.PmxKey_v1);
      s.Write(buffer, 0, buffer.Length);
      byte[] bytes = BitConverter.GetBytes(this.Ver);
      s.Write(bytes, 0, bytes.Length);
      f.ToStreamEx(s, (PmxElementFormat) null);
    }

    object ICloneable.Clone() => (object) new PmxHeader(this);

    public PmxHeader Clone() => new PmxHeader(this);
  }
}
