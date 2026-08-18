// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ModParmSort
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Sort and group modifier parms</summary>
public class ModParmSort : ModParm
{
  public List<string> sortAttrs;
  public List<string> groupAttrs;
  public List<string> sortAttrTexts;
  public List<string> groupAttrTexts;
  public List<bool> sortAttrChecks;
  public List<bool> groupAttrChecks;
  public bool useInbuiltSort;

  public ModParmSort()
  {
  }

  public ModParmSort(ref ModParmData modData)
    : base(ref modData)
  {
    this.SetData(ref modData);
  }

  public override void SetData(ref ModParmData modData)
  {
    if (modData.sortGUIDs.Count > 0)
    {
      if (this.sortAttrs == null)
      {
        this.sortAttrs = new List<string>();
        this.sortAttrTexts = new List<string>();
        this.sortAttrChecks = new List<bool>();
      }
      else
      {
        this.sortAttrs.Clear();
        this.sortAttrTexts.Clear();
        this.sortAttrChecks.Clear();
      }
      for (int index = 0; index < modData.sortGUIDs.Count; ++index)
      {
        this.sortAttrs.Add(modData.sortGUIDs[index]);
        this.sortAttrTexts.Add(modData.sortTexts[index]);
        this.sortAttrChecks.Add(modData.sortChecks[index]);
      }
    }
    else
      this.sortAttrs = (List<string>) null;
    if (modData.groupGUIDs.Count > 0)
    {
      if (this.groupAttrs == null)
      {
        this.groupAttrs = new List<string>();
        this.groupAttrTexts = new List<string>();
        this.groupAttrChecks = new List<bool>();
      }
      else
      {
        this.groupAttrs.Clear();
        this.groupAttrTexts.Clear();
        this.groupAttrChecks.Clear();
      }
      for (int index = 0; index < modData.groupGUIDs.Count; ++index)
      {
        this.groupAttrs.Add(modData.groupGUIDs[index]);
        this.groupAttrTexts.Add(modData.groupTexts[index]);
        this.groupAttrChecks.Add(modData.groupChecks[index]);
      }
    }
    else
      this.groupAttrs = (List<string>) null;
    this.useInbuiltSort = modData.ForLoop;
  }

  public override void Clear()
  {
  }

  public override void FillModParmData(ref ModParmData modData)
  {
    modData.Clear();
    if (this.sortAttrs != null)
    {
      for (int index = 0; index < this.sortAttrs.Count; ++index)
      {
        modData.sortGUIDs.Add(this.sortAttrs[index]);
        modData.sortTexts.Add(this.sortAttrTexts[index]);
        modData.sortChecks.Add(this.sortAttrChecks[index]);
      }
    }
    else
    {
      modData.sortGUIDs.Clear();
      modData.sortTexts.Clear();
      modData.sortChecks.Clear();
    }
    if (this.groupAttrs != null)
    {
      for (int index = 0; index < this.groupAttrs.Count; ++index)
      {
        modData.groupGUIDs.Add(this.groupAttrs[index]);
        modData.groupTexts.Add(this.groupAttrTexts[index]);
        modData.groupChecks.Add(this.groupAttrChecks[index]);
      }
    }
    else
    {
      modData.groupGUIDs.Clear();
      modData.groupTexts.Clear();
      modData.groupChecks.Clear();
    }
    modData.ForLoop = this.useInbuiltSort;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.sortAttrs != null && this.sortAttrs.Count > 0)
    {
      writer.WriteStartElement("sort-attrs");
      writer.WriteAttributeString("num", Convert.ToString(this.sortAttrs.Count));
      for (int index = 0; index < this.sortAttrs.Count; ++index)
      {
        writer.WriteStartElement("Attr-Link");
        writer.WriteElementString("GUID", this.sortAttrs[index]);
        writer.WriteElementString("Name", this.sortAttrTexts[index]);
        writer.WriteElementString("Link", this.sortAttrChecks[index] ? "Y" : "N");
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }
    if (this.groupAttrs != null && this.groupAttrs.Count > 0)
    {
      writer.WriteStartElement("group-attrs");
      writer.WriteAttributeString("num", Convert.ToString(this.groupAttrs.Count));
      for (int index = 0; index < this.groupAttrs.Count; ++index)
      {
        writer.WriteStartElement("Attr-Link");
        writer.WriteElementString("GUID", this.groupAttrs[index]);
        writer.WriteElementString("Name", this.groupAttrTexts[index]);
        writer.WriteElementString("Link", this.groupAttrChecks[index] ? "Y" : "N");
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }
    writer.WriteStartElement("InbuiltSort");
    writer.WriteElementString("Sort", this.useInbuiltSort ? "Y" : "N");
    writer.WriteEndElement();
  }

  public override void LoadFromXML(XmlNode node, int modTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode1 in node.ChildNodes)
    {
      if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "sort-attrs" && childNode1.HasChildNodes)
      {
        if (this.sortAttrs == null)
        {
          this.sortAttrs = new List<string>();
          this.sortAttrTexts = new List<string>();
          this.sortAttrChecks = new List<bool>();
        }
        else
        {
          this.sortAttrs.Clear();
          this.sortAttrTexts.Clear();
          this.sortAttrChecks.Clear();
        }
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          if (childNode2.Name == "Attr-Link")
          {
            this.sortAttrs.Add(childNode2.ChildNodes[0].InnerText);
            this.sortAttrTexts.Add(childNode2.ChildNodes[1].InnerText);
            if (childNode2.ChildNodes.Count >= 3 && childNode2.ChildNodes[2].InnerText == "Y")
              this.sortAttrChecks.Add(true);
            else
              this.sortAttrChecks.Add(false);
          }
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "group-attrs" && childNode1.HasChildNodes)
      {
        if (this.groupAttrs == null)
        {
          this.groupAttrs = new List<string>();
          this.groupAttrTexts = new List<string>();
          this.groupAttrChecks = new List<bool>();
        }
        else
        {
          this.groupAttrs.Clear();
          this.groupAttrTexts.Clear();
          this.groupAttrChecks.Clear();
        }
        foreach (XmlNode childNode3 in childNode1.ChildNodes)
        {
          if (childNode3.Name == "Attr-Link")
          {
            this.groupAttrs.Add(childNode3.ChildNodes[0].InnerText);
            this.groupAttrTexts.Add(childNode3.ChildNodes[1].InnerText);
            if (childNode3.ChildNodes.Count >= 3 && childNode3.ChildNodes[2].InnerText == "Y")
              this.groupAttrChecks.Add(true);
            else
              this.groupAttrChecks.Add(false);
          }
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "InbuiltSort" && childNode1.HasChildNodes)
        this.useInbuiltSort = childNode1.ChildNodes[0].InnerText == "Y";
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool FixIdentsComplete(IUserSession ius) => false;

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = true;
    if (this.sortAttrs != null)
    {
      foreach (string sortAttr in this.sortAttrs)
      {
        Guid empty = Guid.Empty;
        ref Guid local = ref empty;
        if (Guid.TryParse(sortAttr, out local))
          flag = flag && eoi.AddAttrType(empty);
      }
    }
    if (this.groupAttrs != null)
    {
      foreach (string groupAttr in this.groupAttrs)
      {
        Guid empty = Guid.Empty;
        ref Guid local = ref empty;
        if (Guid.TryParse(groupAttr, out local))
          flag = flag && eoi.AddAttrType(empty);
      }
    }
    return flag;
  }

  /// <summary>
  /// Обработать событие слияния атрибутов - заменить один атрибут на другой.
  /// </summary>
  /// <param name="fromAttribute">Заменяемый атрибут</param>
  /// <param name="toAttribute">Заменяющий атрибут</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns>true, если что-то изменилось при переводе</returns>
  public override bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    bool flag = false;
    string str = fromAttribute.GUID.ToString();
    if (this.sortAttrs != null)
    {
      for (int index = 0; index < this.sortAttrs.Count; ++index)
      {
        if (this.sortAttrs[index] == str)
        {
          this.sortAttrs[index] = toAttribute.GUID.ToString();
          this.sortAttrTexts[index] = toAttribute.Name;
          flag = true;
        }
      }
      for (int index = 0; index < this.groupAttrs.Count; ++index)
      {
        if (this.groupAttrs[index] == str)
        {
          this.groupAttrs[index] = toAttribute.GUID.ToString();
          this.groupAttrTexts[index] = toAttribute.Name;
          flag = true;
        }
      }
    }
    return flag;
  }
}
