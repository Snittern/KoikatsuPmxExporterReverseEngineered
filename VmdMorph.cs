// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdMorph
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;

namespace PmxLib
{
  public class VmdMorph : VmdFrameBase, IBytesConvert, ICloneable
  {
    private const int NameBytes = 15;
    public string Name = "";
    public float Value;

    public int ByteCount => 23;

    public VmdMorph()
    {
    }

    public VmdMorph(VmdMorph skin)
    {
      this.Name = skin.Name;
      this.FrameIndex = skin.FrameIndex;
      this.Value = skin.Value;
    }

    public byte[] ToBytes()
    {
      List<byte> byteList = new List<byte>();
      byte[] buf = new byte[15];
      BytesStringProc.SetString(buf, this.Name, (byte) 0, (byte) 253);
      byteList.AddRange((IEnumerable<byte>) buf);
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.FrameIndex));
      byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(this.Value));
      return byteList.ToArray();
    }

    public void FromBytes(byte[] bytes, int startIndex)
    {
      byte[] buf = new byte[15];
      Array.Copy((Array) bytes, startIndex, (Array) buf, 0, 15);
      this.Name = BytesStringProc.GetString(buf, (byte) 0);
      int startIndex1 = startIndex + 15;
      this.FrameIndex = BitConverter.ToInt32(bytes, startIndex1);
      int startIndex2 = startIndex1 + 4;
      this.Value = BitConverter.ToSingle(bytes, startIndex2);
    }

    public object Clone() => (object) new VmdMorph(this);
  }
}
