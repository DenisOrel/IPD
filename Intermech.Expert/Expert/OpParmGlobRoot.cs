// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmGlobRoot
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Общие параметры подбора глобальной таблицы</summary>
public class OpParmGlobRoot : OpParm, IDataAttrs, ILinkObjTypes
{
  public long excerptID;
  public string excerptName = "";
  public IspMode ispWork;
  public List<string> dataAttrGUIDs;
  public List<string> dataAttrTexts;
  public List<string> dataAttrChecks;
  public List<int> linkTypeIDs;
  public List<string> linkTypeTexts;
  public List<int> objTypeIDs;
  public List<string> objTypeTexts;
  public List<int> linkTypesForObjTypes;
  public TempFormula globalFilter;
  public TempFormula afterFilter;
  public string ReplaceContextGUID;
  public string ReplaceContextTypeName;
  public List<string> linkTypeGUIDs;
  public List<string> objTypeGUIDs;
  public bool useConfiguratorOptions;
  public HiddenContentsMode hiddenMode;

  public bool UseConfigOptions => this.useConfiguratorOptions;

  public HiddenContentsMode hcMode => this.hiddenMode;

  public OpParmGlobRoot()
  {
  }

  public OpParmGlobRoot(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.afterFilter = opData.tf.Count != 0 ? (TempFormula) opData.tf.Clone() : (TempFormula) null;
    this.globalFilter = opData.tf2 == null || opData.tf2.Count == 0 ? (TempFormula) null : (TempFormula) opData.tf2.Clone();
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
    if (opData.linkIDs.Count == 0)
    {
      this.linkTypeIDs = (List<int>) null;
    }
    else
    {
      if (this.linkTypeIDs == null)
        this.linkTypeIDs = new List<int>();
      else
        this.linkTypeIDs.Clear();
      for (int index = 0; index < opData.linkIDs.Count; ++index)
        this.linkTypeIDs.Add(opData.linkIDs[index]);
    }
    if (opData.linkTexts.Count == 0)
    {
      this.linkTypeTexts = (List<string>) null;
    }
    else
    {
      if (this.linkTypeTexts == null)
        this.linkTypeTexts = new List<string>();
      else
        this.linkTypeTexts.Clear();
      for (int index = 0; index < opData.linkTexts.Count; ++index)
        this.linkTypeTexts.Add(opData.linkTexts[index]);
    }
    if (opData.objGUIDs.Count == 0)
    {
      this.objTypeIDs = (List<int>) null;
    }
    else
    {
      if (this.objTypeIDs == null)
        this.objTypeIDs = new List<int>();
      else
        this.objTypeIDs.Clear();
      for (int index = 0; index < opData.objGUIDs.Count; ++index)
        this.objTypeIDs.Add(Convert.ToInt32(opData.objGUIDs[index]));
    }
    if (opData.objTexts.Count == 0)
    {
      this.objTypeTexts = (List<string>) null;
    }
    else
    {
      if (this.objTypeTexts == null)
        this.objTypeTexts = new List<string>();
      else
        this.objTypeTexts.Clear();
      for (int index = 0; index < opData.objTexts.Count; ++index)
        this.objTypeTexts.Add(opData.objTexts[index]);
    }
    if (opData.ltForOT.Count == 0)
    {
      this.linkTypesForObjTypes = (List<int>) null;
    }
    else
    {
      if (this.linkTypesForObjTypes == null)
        this.linkTypesForObjTypes = new List<int>();
      else
        this.linkTypesForObjTypes.Clear();
      for (int index = 0; index < opData.ltForOT.Count; ++index)
        this.linkTypesForObjTypes.Add(opData.ltForOT[index]);
    }
    this.excerptID = opData.exID;
    this.excerptName = opData.st1;
    this.ispWork = (IspMode) Convert.ToInt32(opData.s3);
    this.ReplaceContextGUID = opData.s1;
    this.ReplaceContextTypeName = opData.s2;
    this.useConfiguratorOptions = opData.b5;
    this.hiddenMode = (HiddenContentsMode) Convert.ToInt32(opData.st4);
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    if (this.afterFilter != null)
      opData.tf = (TempFormula) this.afterFilter.Clone();
    if (this.globalFilter != null)
      opData.tf2 = (TempFormula) this.globalFilter.Clone();
    if (this.dataAttrTexts != null)
    {
      for (int index = 0; index < this.dataAttrTexts.Count; ++index)
      {
        opData.dA_GUIDs.Add(this.dataAttrGUIDs[index]);
        opData.dA_Texts.Add(this.dataAttrTexts[index]);
        opData.dA_Checks.Add(this.dataAttrChecks[index]);
      }
    }
    if (this.linkTypeIDs != null)
    {
      for (int index = 0; index < this.linkTypeIDs.Count; ++index)
      {
        opData.linkIDs.Add(this.linkTypeIDs[index]);
        opData.linkTexts.Add(this.linkTypeTexts[index]);
      }
    }
    if (this.objTypeIDs != null)
    {
      for (int index = 0; index < this.objTypeIDs.Count; ++index)
      {
        opData.objGUIDs.Add(Convert.ToString(this.objTypeIDs[index]));
        if (this.objTypeTexts.Count <= index)
          opData.objTexts.Add("???");
        else
          opData.objTexts.Add(this.objTypeTexts[index]);
        opData.ltForOT.Add(this.linkTypesForObjTypes[index]);
      }
    }
    opData.exID = this.excerptID;
    opData.st1 = this.excerptName;
    opData.s1 = this.ReplaceContextGUID;
    opData.s2 = this.ReplaceContextTypeName;
    opData.s3 = Convert.ToString((int) this.ispWork);
    opData.b5 = this.useConfiguratorOptions;
    opData.st4 = Convert.ToString((int) this.hiddenMode);
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.excerptID != 0L)
    {
      writer.WriteStartElement("Excerpt");
      writer.WriteElementString("ID", Convert.ToString(this.excerptID));
      writer.WriteElementString("Name", this.excerptName);
      writer.WriteEndElement();
    }
    if (this.afterFilter != null)
    {
      this.afterFilter.FillObjectLinks();
      this.afterFilter.WriteToXML(ref writer, "FormAfterFilter");
    }
    if (this.globalFilter != null)
    {
      this.globalFilter.FillObjectLinks();
      this.globalFilter.WriteToXML(ref writer, "FormGlobFilter");
    }
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
    if (this.linkTypeIDs != null && this.linkTypeIDs.Count > 0)
    {
      writer.WriteStartElement("link-types");
      writer.WriteAttributeString("num", Convert.ToString(this.linkTypeIDs.Count));
      for (int index = 0; index < this.linkTypeIDs.Count; ++index)
        writer.WriteElementString("ID", Convert.ToString(this.linkTypeIDs[index]));
      if (this.linkTypeGUIDs != null)
      {
        for (int index = 0; index < this.linkTypeGUIDs.Count; ++index)
          writer.WriteElementString("_GUID", this.linkTypeGUIDs[index]);
      }
      writer.WriteEndElement();
    }
    if (this.objTypeIDs != null && this.objTypeIDs.Count > 0)
    {
      writer.WriteStartElement("object-types");
      writer.WriteAttributeString("num", Convert.ToString(this.objTypeIDs.Count));
      for (int index = 0; index < this.objTypeIDs.Count; ++index)
        writer.WriteElementString("ID", Convert.ToString(this.objTypeIDs[index]));
      if (this.objTypeGUIDs != null)
      {
        for (int index = 0; index < this.objTypeGUIDs.Count; ++index)
          writer.WriteElementString("_GUID", this.objTypeGUIDs[index]);
      }
      for (int index = 0; index < this.linkTypesForObjTypes.Count; ++index)
        writer.WriteElementString("linkTI", Convert.ToString(this.linkTypesForObjTypes[index]));
      writer.WriteEndElement();
    }
    writer.WriteElementString("ispWork", Convert.ToString((int) this.ispWork));
    writer.WriteElementString("replaceGUID", this.ReplaceContextGUID);
    writer.WriteElementString("replaceName", this.ReplaceContextTypeName);
    writer.WriteElementString("ConfigOptions", this.useConfiguratorOptions ? "Y" : "N");
    writer.WriteElementString("ShowHidden", Convert.ToString((int) this.hiddenMode));
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode1 in node.ChildNodes)
    {
      if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Excerpt")
      {
        if (childNode1.HasChildNodes)
        {
          this.excerptID = Convert.ToInt64(childNode1.ChildNodes[0].InnerText);
          this.excerptName = childNode1.ChildNodes[1].InnerText;
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ispWork")
        this.ispWork = (IspMode) Convert.ToInt32(childNode1.InnerText);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "replaceGUID")
        this.ReplaceContextGUID = childNode1.InnerText;
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "replaceName")
        this.ReplaceContextTypeName = childNode1.InnerText;
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "FormAfterFilter")
        this.afterFilter = new TempFormula(childNode1);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "FormGlobFilter")
        this.globalFilter = new TempFormula(childNode1);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ConfigOptions")
        this.useConfiguratorOptions = childNode1.InnerText == "Y";
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ShowHidden")
        this.hiddenMode = (HiddenContentsMode) Convert.ToInt32(childNode1.InnerText);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "data-attrs" && childNode1.HasChildNodes)
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
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "link-types" && childNode1.HasChildNodes)
        {
          if (this.linkTypeIDs == null)
            this.linkTypeIDs = new List<int>();
          foreach (XmlNode childNode3 in childNode1.ChildNodes)
          {
            if (childNode3.Name == "ID" && childNode3.HasChildNodes)
              this.linkTypeIDs.Add(Convert.ToInt32(childNode3.ChildNodes[0].InnerText));
            if (childNode3.Name == "_GUID" && childNode3.HasChildNodes)
            {
              if (this.linkTypeGUIDs == null)
                this.linkTypeGUIDs = new List<string>();
              this.linkTypeGUIDs.Add(childNode3.ChildNodes[0].InnerText);
            }
          }
          if (this.linkTypeTexts == null)
          {
            this.linkTypeTexts = new List<string>();
            if (this.linkTypeIDs.Count > 0)
            {
              foreach (int linkTypeId in this.linkTypeIDs)
              {
                int relTypeID = linkTypeId;
                if (relTypeID > 90000)
                  relTypeID -= 100000;
                if (relTypeID == 99)
                  this.linkTypeTexts.Add(LocalizationHolder.rm.GetString("Expert_225"));
                else
                  this.linkTypeTexts.Add(MetaDataHelper.GetRelationTypeName(relTypeID));
              }
            }
          }
        }
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "object-types" && childNode1.HasChildNodes)
        {
          if (this.objTypeIDs == null)
            this.objTypeIDs = new List<int>();
          foreach (XmlNode childNode4 in childNode1.ChildNodes)
          {
            if (childNode4.Name == "ID" && childNode4.HasChildNodes)
              this.objTypeIDs.Add(Convert.ToInt32(childNode4.ChildNodes[0].InnerText));
            if (childNode4.Name == "_GUID" && childNode4.HasChildNodes)
            {
              if (this.objTypeGUIDs == null)
                this.objTypeGUIDs = new List<string>();
              this.objTypeGUIDs.Add(childNode4.ChildNodes[0].InnerText);
            }
            if (childNode4.Name == "linkTI" && childNode4.HasChildNodes)
            {
              if (this.linkTypesForObjTypes == null)
                this.linkTypesForObjTypes = new List<int>();
              this.linkTypesForObjTypes.Add(Convert.ToInt32(childNode4.ChildNodes[0].InnerText));
            }
          }
          if (this.objTypeTexts == null)
          {
            this.objTypeTexts = new List<string>();
            if (this.objTypeIDs.Count > 0)
            {
              foreach (int objTypeId in this.objTypeIDs)
                this.objTypeTexts.Add(MetaDataHelper.GetObjectTypeName(objTypeId));
            }
          }
        }
      }
    }
  }

  public List<string> DataAttrGuids => this.dataAttrGUIDs;

  public List<string> DataAttrTexts => this.dataAttrTexts;

  public bool this[int index]
  {
    get
    {
      if (index < 0 || index >= this.dataAttrChecks.Count)
        return false;
      string dataAttrCheck = this.dataAttrChecks[index];
      return dataAttrCheck != "" && dataAttrCheck[0] == 'Y';
    }
  }

  public List<int> LinkTypeIDs => this.linkTypeIDs;

  public List<int> ObjTypeIDs => this.objTypeIDs;

  public List<int> LinkTypesForObjTypes => this.linkTypesForObjTypes;

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.afterFilter != null)
      flag = this.afterFilter.FixIDs(attrs, objs);
    return flag;
  }

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.afterFilter != null)
      flag = this.afterFilter.CollectGUIDs(attrs, objs);
    return flag;
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    bool flag = false;
    if (this.afterFilter != null)
      flag = this.afterFilter.FixIdentsComplete(ius);
    return flag;
  }

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = true;
    if (this.excerptID != 0L)
      eoi.AddObjLink(this.excerptID, ius);
    if (this.ReplaceContextGUID != "")
      flag = eoi.AddObjType(this.ReplaceContextGUID);
    if (this.globalFilter != null)
      flag = flag && this.globalFilter.CollectExpObjInfo(eoi, ius);
    if (this.afterFilter != null)
      flag = flag && this.afterFilter.CollectExpObjInfo(eoi, ius);
    if (this.dataAttrGUIDs != null)
    {
      foreach (string dataAttrGuiD in this.dataAttrGUIDs)
        flag = flag && eoi.AddAttrType(dataAttrGuiD);
    }
    if (this.linkTypeIDs != null)
    {
      foreach (int linkTypeId in this.linkTypeIDs)
        flag = flag && eoi.AddRelType(Math.Abs(linkTypeId));
    }
    if (this.objTypeIDs != null)
    {
      foreach (int objTypeId in this.objTypeIDs)
        flag = flag && eoi.AddObjType(objTypeId);
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
    if (this.globalFilter != null)
      flag = this.globalFilter.PerformAttrChange(fromAttribute, toAttribute);
    if (this.afterFilter != null)
      flag = this.afterFilter.PerformAttrChange(fromAttribute, toAttribute) | flag;
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
