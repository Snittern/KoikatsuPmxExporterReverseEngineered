// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxImpulseMorph
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;

namespace PmxLib
{
  internal class PmxImpulseMorph : PmxBaseMorph, IPmxObjectKey, IPmxStreamIO, ICloneable
  {
    public int Index;
    public bool Local;
    public Vector3 Velocity;
    public Vector3 Torque;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.ImpulseMorph;

    public override int BaseIndex
    {
      get => this.Index;
      set => this.Index = value;
    }

    public PmxBody RefBody { get; set; }

    public bool ZeroFlag { get; set; }

    public PmxImpulseMorph()
    {
      this.Local = false;
      this.Velocity = Vector3.zero;
      this.Torque = Vector3.zero;
    }

    public PmxImpulseMorph(int index, bool local, Vector3 t, Vector3 r)
    {
      this.Index = index;
      this.Local = local;
      this.Velocity = t;
      this.Torque = r;
    }

    public PmxImpulseMorph(PmxImpulseMorph sv)
      : this()
    {
      this.FromPmxImpulseMorph(sv);
    }

    public void FromPmxImpulseMorph(PmxImpulseMorph sv)
    {
      this.Index = sv.Index;
      this.Local = sv.Local;
      this.Velocity = sv.Velocity;
      this.Torque = sv.Torque;
      this.ZeroFlag = sv.ZeroFlag;
    }

    public bool UpdateZeroFlag()
    {
      this.ZeroFlag = this.Velocity == Vector3.zero && this.Torque == Vector3.zero;
      return this.ZeroFlag;
    }

    public void Clear()
    {
      this.Velocity = Vector3.zero;
      this.Torque = Vector3.zero;
    }

    public override void FromStreamEx(Stream s, PmxElementFormat size)
    {
      this.Index = PmxStreamHelper.ReadElement_Int32(s, size.BodySize, true);
      this.Local = (uint) s.ReadByte() > 0U;
      this.Velocity = V3_BytesConvert.FromStream(s);
      this.Torque = V3_BytesConvert.FromStream(s);
    }

    public override void ToStreamEx(Stream s, PmxElementFormat size)
    {
      PmxStreamHelper.WriteElement_Int32(s, this.Index, size.BodySize, true);
      s.WriteByte(this.Local ? (byte) 1 : (byte) 0);
      V3_BytesConvert.ToStream(s, this.Velocity);
      V3_BytesConvert.ToStream(s, this.Torque);
    }

    object ICloneable.Clone() => (object) new PmxImpulseMorph(this);

    public override PmxBaseMorph Clone() => (PmxBaseMorph) new PmxImpulseMorph(this);
  }
}
