// Decompiled with JetBrains decompiler
// Type: PmxLib.PmxNode
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  public class PmxNode : IPmxObjectKey, IPmxStreamIO, ICloneable, INXName
  {
    public bool SystemNode;
    public List<PmxNode.NodeElement> ElementList;

    PmxObject IPmxObjectKey.ObjectKey => PmxObject.Node;

    public string Name { get; set; }

    public string NameE { get; set; }

    public string NXName
    {
      get => this.Name;
      set => this.Name = value;
    }

    public PmxNode()
    {
      this.Name = "";
      this.NameE = "";
      this.SystemNode = false;
      this.ElementList = new List<PmxNode.NodeElement>();
    }

    public PmxNode(PmxNode node, bool nonStr)
      : this()
    {
      this.FromPmxNode(node, nonStr);
    }

    public void FromPmxNode(PmxNode node, bool nonStr)
    {
      if (!nonStr)
      {
        this.Name = node.Name;
        this.NameE = node.NameE;
      }
      this.SystemNode = node.SystemNode;
      int count = node.ElementList.Count;
      this.ElementList.Clear();
      this.ElementList.Capacity = count;
      for (int index = 0; index < count; ++index)
        this.ElementList.Add(node.ElementList[index].Clone());
    }

    public void FromStreamEx(Stream s, PmxElementFormat f)
    {
      this.Name = PmxStreamHelper.ReadString(s, f);
      this.NameE = PmxStreamHelper.ReadString(s, f);
      this.SystemNode = (uint) s.ReadByte() > 0U;
      int num = PmxStreamHelper.ReadElement_Int32(s, 4, true);
      this.ElementList.Clear();
      this.ElementList.Capacity = num;
      for (int index = 0; index < num; ++index)
      {
        PmxNode.NodeElement nodeElement = new PmxNode.NodeElement();
        nodeElement.FromStreamEx(s, f);
        this.ElementList.Add(nodeElement);
      }
    }

    public void ToStreamEx(Stream s, PmxElementFormat f)
    {
      PmxStreamHelper.WriteString(s, this.Name, f);
      PmxStreamHelper.WriteString(s, this.NameE, f);
      s.WriteByte(this.SystemNode ? (byte) 1 : (byte) 0);
      PmxStreamHelper.WriteElement_Int32(s, this.ElementList.Count, 4, true);
      for (int index = 0; index < this.ElementList.Count; ++index)
        this.ElementList[index].ToStreamEx(s, f);
    }

    object ICloneable.Clone() => (object) new PmxNode(this, false);

    public PmxNode Clone() => new PmxNode(this, false);

    public enum ElementType
    {
      Bone,
      Morph,
    }

    public class NodeElement : IPmxObjectKey, IPmxStreamIO, ICloneable
    {
      public PmxNode.ElementType ElementType;
      public int Index;

      PmxObject IPmxObjectKey.ObjectKey => PmxObject.NodeElement;

      public PmxBone RefBone { get; set; }

      public PmxMorph RefMorph { get; set; }

      public NodeElement()
      {
      }

      public NodeElement(PmxNode.NodeElement e) => this.FromNodeElement(e);

      public void FromNodeElement(PmxNode.NodeElement e)
      {
        this.ElementType = e.ElementType;
        this.Index = e.Index;
      }

      public void FromStreamEx(Stream s, PmxElementFormat f)
      {
        this.ElementType = (PmxNode.ElementType) s.ReadByte();
        switch (this.ElementType)
        {
          case PmxNode.ElementType.Bone:
            this.Index = PmxStreamHelper.ReadElement_Int32(s, f.BoneSize, true);
            break;
          case PmxNode.ElementType.Morph:
            this.Index = PmxStreamHelper.ReadElement_Int32(s, f.MorphSize, true);
            break;
        }
      }

      public void ToStreamEx(Stream s, PmxElementFormat f)
      {
        s.WriteByte((byte) this.ElementType);
        switch (this.ElementType)
        {
          case PmxNode.ElementType.Bone:
            PmxStreamHelper.WriteElement_Int32(s, this.Index, f.BoneSize, true);
            break;
          case PmxNode.ElementType.Morph:
            PmxStreamHelper.WriteElement_Int32(s, this.Index, f.MorphSize, true);
            break;
        }
      }

      public static PmxNode.NodeElement BoneElement(int index) => new PmxNode.NodeElement()
      {
        ElementType = PmxNode.ElementType.Bone,
        Index = index
      };

      public static PmxNode.NodeElement MorphElement(int index) => new PmxNode.NodeElement()
      {
        ElementType = PmxNode.ElementType.Morph,
        Index = index
      };

      object ICloneable.Clone() => (object) new PmxNode.NodeElement(this);

      public PmxNode.NodeElement Clone() => new PmxNode.NodeElement(this);
    }
  }
}
