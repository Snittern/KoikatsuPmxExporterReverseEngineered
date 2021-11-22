// Decompiled with JetBrains decompiler
// Type: PmxLib.VmdIplPoint
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;

namespace PmxLib
{
  public class VmdIplPoint : ICloneable
  {
    protected int m_x;
    protected int m_y;

    public int X
    {
      get => this.m_x;
      set => this.m_x = this.RangeValue(value);
    }

    public int Y
    {
      get => this.m_y;
      set => this.m_y = this.RangeValue(value);
    }

    public VmdIplPoint()
    {
    }

    public VmdIplPoint(VmdIplPoint ip)
    {
      this.X = ip.X;
      this.Y = ip.Y;
    }

    public VmdIplPoint(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    public VmdIplPoint(Point p) => this.Set(p);

    public void Set(Point p)
    {
      this.X = p.X;
      this.Y = p.Y;
    }

    protected int RangeValue(int v)
    {
      v = v < 0 ? 0 : v;
      v = v > (int) sbyte.MaxValue ? (int) sbyte.MaxValue : v;
      return v;
    }

    public static implicit operator Point(VmdIplPoint p) => new Point(p.X, p.Y);

    public static implicit operator VmdIplPoint(Point p) => new VmdIplPoint(p);

    public object Clone() => (object) new VmdIplPoint(this);
  }
}
