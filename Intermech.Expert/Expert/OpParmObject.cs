// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmObject
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Object-searching parms</summary>
public class OpParmObject : OpParm, IDataAttrs
{
  public long excerptID;
  public string excerptName = "";
  public int compId;
  public string objTypeForGlobalGUID = "";
  public string compName = "";
  public bool Dups;
  public bool InbuiltSort;
  public IspMode ispWork;
  public bool useCurrentIsps;
  public GlobalData useGlobal;
  public GlobalSave saveGlobal;
  public bool AddThis;
  public bool NoSearch = true;
  public bool saveRels = true;
  public List<string> dataAttrGUIDs;
  public List<string> dataAttrTexts;
  public List<string> dataAttrChecks;
  public List<int> linkTypeIDs;
  public ArrayList linkTypeTexts;
  public List<int> objTypeIDs;
  public ArrayList objTypeTexts;
  public TempFormula cond;
  public TempFormula filter;
  public TempFormula afterFilter;
  public List<string> linkTypeGUIDs;
  public List<string> objTypeGUIDs;
  public bool useConfiguratorOptions;
  public HiddenContentsMode hiddenMode;

  public OpParmObject()
  {
  }

  public OpParmObject(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.cond = opData.tf.Count != 0 ? (TempFormula) opData.tf.Clone() : (TempFormula) null;
    this.filter = opData.tf2 != null ? (TempFormula) opData.tf2.Clone() : (TempFormula) null;
    this.afterFilter = opData.tf3 != null ? (TempFormula) opData.tf3.Clone() : (TempFormula) null;
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
    if (opData.linkTexts.Count == 0)
    {
      this.linkTypeTexts = (ArrayList) null;
    }
    else
    {
      if (this.linkTypeTexts == null)
        this.linkTypeTexts = new ArrayList();
      else
        this.linkTypeTexts.Clear();
      for (int index = 0; index < opData.linkTexts.Count; ++index)
        this.linkTypeTexts.Add((object) opData.linkTexts[index]);
    }
    if (opData.objTexts.Count == 0)
    {
      this.objTypeTexts = (ArrayList) null;
    }
    else
    {
      if (this.objTypeTexts == null)
        this.objTypeTexts = new ArrayList();
      else
        this.objTypeTexts.Clear();
      for (int index = 0; index < opData.objTexts.Count; ++index)
        this.objTypeTexts.Add((object) opData.objTexts[index]);
    }
    this.Dups = opData.b1;
    this.useGlobal = (GlobalData) Convert.ToInt32(opData.s2);
    this.saveGlobal = (GlobalSave) Convert.ToInt32(opData.s4);
    this.AddThis = opData.b4;
    this.NoSearch = opData.b3;
    this.excerptID = opData.exID;
    this.excerptName = opData.st1;
    this.compId = opData.settingMod;
    this.objTypeForGlobalGUID = opData.s5;
    this.compName = opData.s1;
    this.saveRels = opData.b2;
    this.InbuiltSort = opData.st2 == "Y";
    this.useCurrentIsps = opData.st3 == "Y";
    this.ispWork = (IspMode) Convert.ToInt32(opData.s3);
    this.useConfiguratorOptions = opData.b5;
    this.hiddenMode = (HiddenContentsMode) Convert.ToInt32(opData.st4);
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    if (this.cond != null)
      opData.tf = (TempFormula) this.cond.Clone();
    if (this.filter != null)
      opData.tf2 = (TempFormula) this.filter.Clone();
    if (this.afterFilter != null)
      opData.tf3 = (TempFormula) this.afterFilter.Clone();
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
        opData.linkTexts.Add((string) this.linkTypeTexts[index]);
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
          opData.objTexts.Add((string) this.objTypeTexts[index]);
      }
    }
    opData.b1 = this.Dups;
    opData.s2 = Convert.ToString((int) this.useGlobal);
    opData.s4 = Convert.ToString((int) this.saveGlobal);
    opData.b4 = this.AddThis;
    opData.b3 = this.NoSearch;
    opData.exID = this.excerptID;
    opData.st1 = this.excerptName;
    opData.s1 = this.compName;
    opData.settingMod = this.compId;
    opData.b2 = this.saveRels;
    opData.st2 = this.InbuiltSort ? "Y" : "N";
    opData.st3 = this.useCurrentIsps ? "Y" : "N";
    opData.s5 = this.objTypeForGlobalGUID;
    opData.s3 = Convert.ToString((int) this.ispWork);
    opData.b5 = this.useConfiguratorOptions;
    opData.st4 = Convert.ToString((int) this.hiddenMode);
  }

  public bool GetAttrCheck(int index)
  {
    if (index >= this.dataAttrChecks.Count)
      return false;
    string dataAttrCheck = this.dataAttrChecks[index];
    return dataAttrCheck != "" && dataAttrCheck[0] == 'Y';
  }

  public char GetAttrSort(int index)
  {
    if (index < this.dataAttrChecks.Count)
    {
      string dataAttrCheck = this.dataAttrChecks[index];
      if (dataAttrCheck.Contains("a"))
        return 'a';
      if (dataAttrCheck.Contains("d"))
        return 'd';
    }
    return ' ';
  }

  public GlobalData UseGlobal => this.NoSearch ? GlobalData.globalNone : this.useGlobal;

  public bool UseWholeTable => this.NoSearch && this.useGlobal == GlobalData.globalAdd;

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.excerptID != 0L)
    {
      writer.WriteStartElement("Excerpt");
      writer.WriteElementString("ID", Convert.ToString(this.excerptID));
      writer.WriteElementString("Name", this.excerptName);
      writer.WriteEndElement();
    }
    if (this.compId != 0)
    {
      writer.WriteStartElement("Comparison");
      writer.WriteElementString("ID", Convert.ToString(this.compId));
      writer.WriteElementString("Name", this.compName);
      writer.WriteEndElement();
    }
    writer.WriteElementString("Dups", this.Dups ? "Y" : "N");
    writer.WriteElementString("InbuiltSort", this.InbuiltSort ? "Y" : "N");
    writer.WriteElementString("useCurrentIsps", this.useCurrentIsps ? "Y" : "N");
    writer.WriteElementString("useGlobal", Convert.ToString((int) this.useGlobal));
    writer.WriteElementString("saveGlobal", Convert.ToString((int) this.saveGlobal));
    writer.WriteElementString("AddThis", this.AddThis ? "Y" : "N");
    writer.WriteElementString("NoSearch", this.NoSearch ? "Y" : "N");
    writer.WriteElementString("ConfigOptions", this.useConfiguratorOptions ? "Y" : "N");
    writer.WriteElementString("ShowHidden", Convert.ToString((int) this.hiddenMode));
    if (this.cond != null)
    {
      this.cond.FillObjectLinks();
      this.cond.WriteToXML(ref writer);
    }
    if (this.filter != null)
    {
      this.filter.FillObjectLinks();
      this.filter.WriteToXML(ref writer, "FormGlobFilter");
    }
    if (this.afterFilter != null)
    {
      this.afterFilter.FillObjectLinks();
      this.afterFilter.WriteToXML(ref writer, "FormAfterFilter");
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
        writer.WriteElementString("GUID", Convert.ToString(this.linkTypeIDs[index]));
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
        writer.WriteElementString("GUID", Convert.ToString(this.objTypeIDs[index]));
      if (this.objTypeGUIDs != null)
      {
        for (int index = 0; index < this.objTypeGUIDs.Count; ++index)
          writer.WriteElementString("_GUID", this.objTypeGUIDs[index]);
      }
      writer.WriteEndElement();
    }
    writer.WriteElementString("ispWork", Convert.ToString((int) this.ispWork));
    writer.WriteElementString("objTypeForGlobal", this.objTypeForGlobalGUID);
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
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Comparison")
      {
        if (childNode1.HasChildNodes)
        {
          this.compId = Convert.ToInt32(childNode1.ChildNodes[0].InnerText);
          this.compName = childNode1.ChildNodes[1].InnerText;
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Dups")
        this.Dups = childNode1.InnerText == "Y";
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "InbuiltSort")
        this.InbuiltSort = childNode1.InnerText == "Y";
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "useCurrentIsps")
        this.useCurrentIsps = childNode1.InnerText == "Y";
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "NoSearch")
        this.NoSearch = childNode1.InnerText == "Y";
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "useGlobal")
        this.useGlobal = (GlobalData) Convert.ToInt32(childNode1.InnerText);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "saveGlobal")
        this.saveGlobal = (GlobalSave) Convert.ToInt32(childNode1.InnerText);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "AddThis")
        this.AddThis = childNode1.InnerText == "Y";
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ispWork")
        this.ispWork = (IspMode) Convert.ToInt32(childNode1.InnerText);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "objTypeForGlobal")
        this.objTypeForGlobalGUID = childNode1.InnerText;
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ConfigOptions")
        this.useConfiguratorOptions = childNode1.InnerText == "Y";
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ShowHidden")
        this.hiddenMode = (HiddenContentsMode) Convert.ToInt32(childNode1.InnerText);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Formula")
        this.cond = new TempFormula(childNode1);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "FormGlobFilter")
        this.filter = new TempFormula(childNode1);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "FormAfterFilter")
        this.afterFilter = new TempFormula(childNode1);
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
            if (childNode3.Name == "GUID" && childNode3.HasChildNodes)
              this.linkTypeIDs.Add(Convert.ToInt32(childNode3.ChildNodes[0].InnerText));
            if (childNode3.Name == "_GUID" && childNode3.HasChildNodes)
            {
              if (this.linkTypeGUIDs == null)
                this.linkTypeGUIDs = new List<string>();
              this.linkTypeGUIDs.Add(childNode3.ChildNodes[0].InnerText);
            }
          }
        }
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "object-types" && childNode1.HasChildNodes)
        {
          if (this.objTypeIDs == null)
            this.objTypeIDs = new List<int>();
          foreach (XmlNode childNode4 in childNode1.ChildNodes)
          {
            if (childNode4.Name == "GUID" && childNode4.HasChildNodes)
              this.objTypeIDs.Add(Convert.ToInt32(childNode4.ChildNodes[0].InnerText));
            if (childNode4.Name == "_GUID" && childNode4.HasChildNodes)
            {
              if (this.objTypeGUIDs == null)
                this.objTypeGUIDs = new List<string>();
              this.objTypeGUIDs.Add(childNode4.ChildNodes[0].InnerText);
            }
          }
        }
      }
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.cond != null)
      flag = this.cond.FixIDs(attrs, objs);
    if (this.filter != null)
      flag = flag || this.filter.FixIDs(attrs, objs);
    return flag;
  }

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.cond != null)
      flag = this.cond.CollectGUIDs(attrs, objs);
    if (this.filter != null)
      flag = flag || this.filter.CollectGUIDs(attrs, objs);
    return flag;
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    bool flag = false;
    if (this.cond != null)
      flag = this.cond.FixIdentsComplete(ius);
    if (this.filter != null)
      flag = flag || this.filter.FixIdentsComplete(ius);
    return flag;
  }

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = true;
    if (this.cond != null)
      flag = this.cond.CollectExpObjInfo(eoi, ius);
    if (this.filter != null)
      flag = flag && this.filter.CollectExpObjInfo(eoi, ius);
    if (this.afterFilter != null)
      flag = flag && this.afterFilter.CollectExpObjInfo(eoi, ius);
    if (this.excerptID != 0L)
      eoi.AddObjLink(this.excerptID, ius);
    if (this.objTypeForGlobalGUID != "")
      flag = flag && eoi.AddObjType(this.objTypeForGlobalGUID);
    if (this.dataAttrGUIDs != null)
    {
      foreach (string dataAttrGuiD in this.dataAttrGUIDs)
        flag = flag && eoi.AddAttrType(dataAttrGuiD);
    }
    if (this.linkTypeIDs != null)
    {
      foreach (int linkTypeId in this.linkTypeIDs)
        flag = flag && eoi.AddRelType(linkTypeId);
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
    if (this.cond != null)
      flag = this.cond.PerformAttrChange(fromAttribute, toAttribute);
    if (this.filter != null)
      flag = this.filter.PerformAttrChange(fromAttribute, toAttribute) | flag;
    if (this.afterFilter != null)
      flag = this.afterFilter.PerformAttrChange(fromAttribute, toAttribute) | flag;
    string lower = fromAttribute.GUID.ToString().ToLower();
    if (this.dataAttrGUIDs != null)
    {
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
    }
    return flag;
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
}
