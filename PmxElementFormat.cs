// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxElementFormat
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  public class PmxElementFormat : IPmxStreamIO, ICloneable
  {
    private const int SizeBufLength = 8;
    public const int MaxUVACount = 4;

    public float Ver { get; set; }

    public PmxElementFormat.StringEncType StringEnc { get; set; }

    public int UVACount { get; set; }

    public int VertexSize { get; set; }

    public int TexSize { get; set; }

    public int MaterialSize { get; set; }

    public int BoneSize { get; set; }

    public int MorphSize { get; set; }

    public int BodySize { get; set; }

    public PmxElementFormat(float ver)
    {
      if ((double) ver == 0.0)
        ver = 2.1f;
      this.Ver = ver;
      this.StringEnc = PmxElementFormat.StringEncType.UTF16;
      this.UVACount = 0;
      this.VertexSize = 2;
      this.TexSize = 1;
      this.MaterialSize = 1;
      this.BoneSize = 2;
      this.MorphSize = 2;
      this.BodySize = 4;
    }

    public PmxElementFormat(PmxElementFormat f) => this.FromElementFormat(f);

    public void FromElementFormat(PmxElementFormat f)
    {
      this.Ver = f.Ver;
      this.StringEnc = f.StringEnc;
      this.UVACount = f.UVACount;
      this.VertexSize = f.VertexSize;
      this.TexSize = f.TexSize;
      this.MaterialSize = f.MaterialSize;
      this.BoneSize = f.BoneSize;
      this.MorphSize = f.MorphSize;
      this.BodySize = f.BodySize;
    }

    public static int GetUnsignedBufSize(int count) => count >= 256 ? (count >= 65536 ? 4 : 2) : 1;

    public static int GetSignedBufSize(int count) => count >= 128 ? (count >= 32768 ? 4 : 2) : 1;

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      byte[] buffer = new byte[PmxStreamHelper.ReadElement_Int32(s, 1, true)];
      s.Read(buffer, 0, buffer.Length);
      int num1 = 0;
      int num2;
      if ((double) this.Ver <= 1.0)
      {
        byte[] numArray1 = buffer;
        int index1 = num1;
        int num3 = index1 + 1;
        this.VertexSize = (int) numArray1[index1];
        byte[] numArray2 = buffer;
        int index2 = num3;
        int num4 = index2 + 1;
        this.BoneSize = (int) numArray2[index2];
        byte[] numArray3 = buffer;
        int index3 = num4;
        int num5 = index3 + 1;
        this.MorphSize = (int) numArray3[index3];
        byte[] numArray4 = buffer;
        int index4 = num5;
        int num6 = index4 + 1;
        this.MaterialSize = (int) numArray4[index4];
        byte[] numArray5 = buffer;
        int index5 = num6;
        num2 = index5 + 1;
        this.BodySize = (int) numArray5[index5];
      }
      else
      {
        byte[] numArray6 = buffer;
        int index6 = num1;
        int num7 = index6 + 1;
        this.StringEnc = (PmxElementFormat.StringEncType) numArray6[index6];
        byte[] numArray7 = buffer;
        int index7 = num7;
        int num8 = index7 + 1;
        this.UVACount = (int) numArray7[index7];
        byte[] numArray8 = buffer;
        int index8 = num8;
        int num9 = index8 + 1;
        this.VertexSize = (int) numArray8[index8];
        byte[] numArray9 = buffer;
        int index9 = num9;
        int num10 = index9 + 1;
        this.TexSize = (int) numArray9[index9];
        byte[] numArray10 = buffer;
        int index10 = num10;
        int num11 = index10 + 1;
        this.MaterialSize = (int) numArray10[index10];
        byte[] numArray11 = buffer;
        int index11 = num11;
        int num12 = index11 + 1;
        this.BoneSize = (int) numArray11[index11];
        byte[] numArray12 = buffer;
        int index12 = num12;
        int num13 = index12 + 1;
        this.MorphSize = (int) numArray12[index12];
        byte[] numArray13 = buffer;
        int index13 = num13;
        num2 = index13 + 1;
        this.BodySize = (int) numArray13[index13];
      }
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      byte[] buffer = new byte[8];
      for (int index = 0; index < buffer.Length; ++index)
        buffer[index] = (byte) 0;
      int num1 = 0;
      int num2;
      if ((double) this.Ver <= 1.0)
      {
        byte[] numArray1 = buffer;
        int index1 = num1;
        int num3 = index1 + 1;
        int vertexSize = (int) (byte) this.VertexSize;
        numArray1[index1] = (byte) vertexSize;
        byte[] numArray2 = buffer;
        int index2 = num3;
        int num4 = index2 + 1;
        int boneSize = (int) (byte) this.BoneSize;
        numArray2[index2] = (byte) boneSize;
        byte[] numArray3 = buffer;
        int index3 = num4;
        int num5 = index3 + 1;
        int morphSize = (int) (byte) this.MorphSize;
        numArray3[index3] = (byte) morphSize;
        byte[] numArray4 = buffer;
        int index4 = num5;
        int num6 = index4 + 1;
        int materialSize = (int) (byte) this.MaterialSize;
        numArray4[index4] = (byte) materialSize;
        byte[] numArray5 = buffer;
        int index5 = num6;
        num2 = index5 + 1;
        int bodySize = (int) (byte) this.BodySize;
        numArray5[index5] = (byte) bodySize;
      }
      else
      {
        byte[] numArray6 = buffer;
        int index6 = num1;
        int num7 = index6 + 1;
        int stringEnc = (int) (byte) this.StringEnc;
        numArray6[index6] = (byte) stringEnc;
        byte[] numArray7 = buffer;
        int index7 = num7;
        int num8 = index7 + 1;
        int uvaCount = (int) (byte) this.UVACount;
        numArray7[index7] = (byte) uvaCount;
        byte[] numArray8 = buffer;
        int index8 = num8;
        int num9 = index8 + 1;
        int vertexSize = (int) (byte) this.VertexSize;
        numArray8[index8] = (byte) vertexSize;
        byte[] numArray9 = buffer;
        int index9 = num9;
        int num10 = index9 + 1;
        int texSize = (int) (byte) this.TexSize;
        numArray9[index9] = (byte) texSize;
        byte[] numArray10 = buffer;
        int index10 = num10;
        int num11 = index10 + 1;
        int materialSize = (int) (byte) this.MaterialSize;
        numArray10[index10] = (byte) materialSize;
        byte[] numArray11 = buffer;
        int index11 = num11;
        int num12 = index11 + 1;
        int boneSize = (int) (byte) this.BoneSize;
        numArray11[index11] = (byte) boneSize;
        byte[] numArray12 = buffer;
        int index12 = num12;
        int num13 = index12 + 1;
        int morphSize = (int) (byte) this.MorphSize;
        numArray12[index12] = (byte) morphSize;
        byte[] numArray13 = buffer;
        int index13 = num13;
        num2 = index13 + 1;
        int bodySize = (int) (byte) this.BodySize;
        numArray13[index13] = (byte) bodySize;
      }
      PmxStreamHelper.WriteElement_Int32(s, buffer.Length, 1, true);
      s.Write(buffer, 0, buffer.Length);
    }

    object ICloneable.Clone() => (object) new PmxElementFormat(this);

    public PmxElementFormat Clone() => new PmxElementFormat(this);

    public enum StringEncType
    {
      UTF16,
      UTF8,
    }
  }
}
