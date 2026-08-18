// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmTiLink
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

public class OpParmTiLink : OpParm
{
  public List<string> dataAttrGUIDs;
  public List<string> dataAttrTexts;
  public List<string> dataAttrChecks;
  public string TiDocTypeGuid = "";
  public string TiDocTypeName = "";
  public string NewDocTypeGuid = "";
  public string NewDocTypeName = "";

  public OpParmTiLink()
  {
  }

  public OpParmTiLink(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    if (opData.dA_GUIDs.Count == 0)
    {
      this.dataAttrGUIDs = (List<string>) null;
    }
    else
    {
      if (this.dataAttrGUIDs == null)
        this.dataAttrGUIDs = new List<string>();
      else
        this.dataAttrGUIDs.Clear();
      for (int index = 0; index < opData.dA_GUIDs.Count; ++index)
        this.dataAttrGUIDs.Add(opData.dA_GUIDs[index]);
    }
    if (opData.dA_Texts.Count == 0)
    {
      this.dataAttrTexts = (List<string>) null;
    }
    else
    {
      if (this.dataAttrTexts == null)
        this.dataAttrTexts = new List<string>();
      else
        this.dataAttrTexts.Clear();
      for (int index = 0; index < opData.dA_Texts.Count; ++index)
        this.dataAttrTexts.Add(opData.dA_Texts[index]);
    }
    if (opData.dA_Checks.Count == 0)
    {
      this.dataAttrChecks = (List<string>) null;
    }
    else
    {
      if (this.dataAttrChecks == null)
        this.dataAttrChecks = new List<string>();
      else
        this.dataAttrChecks.Clear();
      for (int index = 0; index < opData.dA_Checks.Count; ++index)
        this.dataAttrChecks.Add(opData.dA_Checks[index]);
    }
    this.TiDocTypeGuid = opData.s1;
    this.TiDocTypeName = opData.s2;
    this.NewDocTypeGuid = opData.st1;
    this.NewDocTypeName = opData.st2;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    if (this.dataAttrTexts != null)
    {
      for (int index = 0; index < this.dataAttrTexts.Count; ++index)
      {
        opData.dA_GUIDs.Add(this.dataAttrGUIDs[index]);
        opData.dA_Texts.Add(this.dataAttrTexts[index]);
        opData.dA_Checks.Add(this.dataAttrChecks[index]);
      }
    }
    opData.s1 = this.TiDocTypeGuid;
    opData.s2 = this.TiDocTypeName;
    opData.st1 = this.NewDocTypeGuid;
    opData.st2 = this.NewDocTypeName;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.dataAttrGUIDs != null && this.dataAttrGUIDs.Count > 0)
    {
      writer.WriteStartElement("data-attrs");
      writer.WriteAttributeString("num", Convert.ToString(this.dataAttrGUIDs.Count));
      for (int index = 0; index < this.dataAttrGUIDs.Count; ++index)
      {
        writer.WriteStartElement("Attr-Link");
        writer.WriteElementString("GUID", this.dataAttrGUIDs[index]);
        writer.WriteElementString("Name", this.dataAttrTexts[index]);
        writer.WriteElementString("Link", this.dataAttrChecks[index]);
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }
    writer.WriteElementString("TiGuid", this.TiDocTypeGuid);
    writer.WriteElementString("TiName", this.TiDocTypeName);
    writer.WriteElementString("NewGuid", this.NewDocTypeGuid);
    writer.WriteElementString("NewName", this.NewDocTypeName);
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode1 in node.ChildNodes)
    {
      if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "data-attrs" && childNode1.HasChildNodes)
      {
        if (this.dataAttrGUIDs == null)
          this.dataAttrGUIDs = new List<string>();
        if (this.dataAttrTexts == null)
          this.dataAttrTexts = new List<string>();
        if (this.dataAttrChecks == null)
          this.dataAttrChecks = new List<string>();
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          if (childNode2.Name == "Attr-Link" && childNode2.HasChildNodes)
          {
            this.dataAttrGUIDs.Add(childNode2.ChildNodes[0].InnerText);
            this.dataAttrTexts.Add(childNode2.ChildNodes[1].InnerText);
            this.dataAttrChecks.Add(childNode2.ChildNodes[2].InnerText);
          }
        }
      }
      else
      {
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "TiGuid")
          this.TiDocTypeGuid = childNode1.InnerText;
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "TiName")
          this.TiDocTypeName = childNode1.InnerText;
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "NewGuid")
          this.NewDocTypeGuid = childNode1.InnerText;
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "NewName")
          this.NewDocTypeName = childNode1.InnerText;
      }
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool FixIdentsComplete(IUserSession ius) => false;

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = true;
    if (this.dataAttrGUIDs != null)
    {
      foreach (string dataAttrGuiD in this.dataAttrGUIDs)
        flag = flag && eoi.AddAttrType(dataAttrGuiD);
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
    string lower = fromAttribute.GUID.ToString().ToLower();
    for (int index = 0; index < this.dataAttrGUIDs.Count; ++index)
    {
      if (this.dataAttrGUIDs[index] == lower)
      {
        flag = true;
        this.dataAttrGUIDs[index] = toAttribute.GUID.ToString();
        this.dataAttrTexts[index] = toAttribute.Name;
        break;
      }
    }
    return flag;
  }
}
