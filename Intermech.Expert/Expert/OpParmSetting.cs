// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmSetting
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

/// <summary>Setting operator parms</summary>
public class OpParmSetting : OpParm
{
  public string setMode = "O";
  public string attrGUID = "";
  public string objTypeGUID = "";
  public string attrText = "";
  public string objTypeText = "";
  public FieldTypes attrType = FieldTypes.ftString;
  public TempFormula tf;
  public ExpertSettingKind setKind;
  public string listDivider = "";
  public int Count;
  public double Val = 1.2345;
  public long I_Val = 936532;
  public long measureID;
  public List<Triple> listTable;
  public bool hasArray;
  public TempFormula formX;
  public TempFormula formY;
  public bool storeInGlobal;

  public OpParmSetting()
  {
  }

  public OpParmSetting(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.tf = opData.tf.Count == 0 || opData.settingMod == 4 ? (TempFormula) null : (TempFormula) opData.tf.Clone();
    this.attrGUID = opData.s1;
    this.objTypeGUID = opData.s2;
    this.attrText = opData.st1;
    this.objTypeText = opData.st2;
    this.listDivider = opData.s3;
    this.setKind = (ExpertSettingKind) opData.settingMod;
    this.attrType = opData.attrType;
    this.listTable = opData.listTable.Count == 0 || opData.settingMod != 1 ? (List<Triple>) null : new List<Triple>((IEnumerable<Triple>) opData.listTable);
    this.hasArray = opData.b2;
    this.formX = opData.tf2 != null ? (TempFormula) opData.tf2.Clone() : (TempFormula) null;
    this.formY = opData.tf3 != null ? (TempFormula) opData.tf3.Clone() : (TempFormula) null;
    this.storeInGlobal = opData.b3;
    this.setMode = opData.s4;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    if (this.tf != null)
      opData.tf = (TempFormula) this.tf.Clone();
    opData.s1 = this.attrGUID;
    opData.s2 = this.objTypeGUID;
    opData.st1 = this.attrText;
    opData.st2 = this.objTypeText;
    opData.s3 = this.listDivider;
    opData.settingMod = (int) this.setKind;
    opData.attrType = this.attrType;
    if (this.listTable != null)
      opData.listTable = new List<Triple>((IEnumerable<Triple>) this.listTable);
    opData.b2 = this.hasArray;
    opData.tf2 = this.formX == null ? (TempFormula) null : (TempFormula) this.formX.Clone();
    opData.tf3 = this.formY == null ? (TempFormula) null : (TempFormula) this.formY.Clone();
    opData.b3 = this.storeInGlobal;
    opData.s4 = this.setMode;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.setMode == "F")
    {
      this.objTypeGUID = "";
      this.objTypeText = "";
    }
    writer.WriteElementString("set-kind", Convert.ToString((int) this.setKind));
    if (this.setKind == ExpertSettingKind.setKindList)
      writer.WriteElementString("list-divider", $"'{this.listDivider}'");
    writer.WriteStartElement("Attr-ObjType");
    writer.WriteStartElement("Attr-Link");
    writer.WriteElementString("GUID", this.attrGUID);
    writer.WriteElementString("Name", this.attrText);
    writer.WriteEndElement();
    if (this.objTypeGUID != "")
    {
      writer.WriteStartElement("Attr-Link");
      writer.WriteElementString("GUID", this.objTypeGUID);
      writer.WriteElementString("Name", this.objTypeText);
      writer.WriteEndElement();
    }
    writer.WriteEndElement();
    writer.WriteElementString("setMode", this.setMode);
    if (this.tf != null)
    {
      this.tf.FillObjectLinks();
      this.tf.WriteToXML(ref writer);
    }
    if (this.listTable != null)
    {
      foreach (Triple triple in this.listTable)
      {
        writer.WriteStartElement("Triple");
        writer.WriteElementString("From", triple.From);
        writer.WriteElementString("To", triple.To);
        writer.WriteElementString("Result", triple.Result);
        writer.WriteEndElement();
      }
    }
    if (this.hasArray)
      writer.WriteElementString("hasArray", "yes");
    if (this.storeInGlobal)
      writer.WriteElementString("StoreInGlobal", "yes");
    if (this.formX != null)
    {
      this.formX.FillObjectLinks();
      this.formX.WriteToXML(ref writer, "formX");
    }
    if (this.formY == null)
      return;
    this.formY.FillObjectLinks();
    this.formY.WriteToXML(ref writer, "formY");
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    this.setMode = "O";
    if (node.HasChildNodes)
    {
      foreach (XmlNode childNode1 in node.ChildNodes)
      {
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Attr-ObjType" && childNode1.HasChildNodes)
        {
          XmlNode childNode2 = childNode1.ChildNodes[0];
          if (childNode2.HasChildNodes && childNode2.ChildNodes.Count > 1 && childNode2.Name == "Attr-Link")
          {
            this.attrGUID = childNode2.ChildNodes[0].InnerText;
            this.attrText = childNode2.ChildNodes[1].InnerText;
          }
          if (childNode1.ChildNodes.Count > 1 && childNode1.ChildNodes[1].HasChildNodes && childNode1.ChildNodes[1].Name == "Attr-Link")
          {
            this.objTypeGUID = childNode1.ChildNodes[1].ChildNodes[0].InnerText;
            this.objTypeText = childNode1.ChildNodes[1].ChildNodes[1].InnerText;
          }
        }
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Formula")
          this.tf = new TempFormula(childNode1);
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "set-kind")
          this.setKind = (ExpertSettingKind) Convert.ToInt32(childNode1.InnerText);
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "list-divider")
        {
          this.listDivider = childNode1.InnerText;
          if (this.listDivider.StartsWith("'"))
            this.listDivider = this.listDivider.Substring(1, this.listDivider.Length - 2);
        }
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "doc-attr")
          this.setMode = "F";
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "setMode")
          this.setMode = childNode1.InnerText;
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Triple")
        {
          Triple triple = new Triple(childNode1);
          if (this.listTable == null)
            this.listTable = new List<Triple>();
          this.listTable.Add(triple);
        }
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "hasArray")
          this.hasArray = true;
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "StoreInGlobal")
          this.storeInGlobal = true;
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "formX")
          this.formX = new TempFormula(childNode1);
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "formY")
          this.formY = new TempFormula(childNode1);
      }
    }
    if (!(this.setMode == "F"))
      return;
    this.objTypeGUID = "";
    this.objTypeText = "";
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.tf != null)
      flag = this.tf.FixIDs(attrs, objs);
    if (this.formX != null)
      flag = this.formX.FixIDs(attrs, objs) | flag;
    if (this.formY != null)
      flag = this.formY.FixIDs(attrs, objs) | flag;
    return flag;
  }

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.tf != null)
      flag = this.tf.CollectGUIDs(attrs, objs);
    if (this.formX != null)
      flag = this.formX.CollectGUIDs(attrs, objs) | flag;
    if (this.formY != null)
      flag = this.formY.CollectGUIDs(attrs, objs) | flag;
    return flag;
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    bool flag = false;
    if (this.tf != null)
      flag = this.tf.FixIdentsComplete(ius);
    if (this.formX != null)
      flag = this.formX.FixIdentsComplete(ius) | flag;
    if (this.formY != null)
      flag = this.formY.FixIdentsComplete(ius) | flag;
    return flag;
  }

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = eoi.AddAttrType(this.attrGUID) && eoi.AddObjType(this.objTypeGUID);
    if (this.tf != null)
      flag = flag && this.tf.CollectExpObjInfo(eoi, ius);
    if (this.formX != null)
      flag = flag && this.formX.CollectExpObjInfo(eoi, ius);
    if (this.formY != null)
      flag = flag && this.formY.CollectExpObjInfo(eoi, ius);
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
    if (this.attrGUID == fromAttribute.GUID.ToString())
    {
      this.attrGUID = toAttribute.GUID.ToString();
      this.attrText = toAttribute.Name;
      flag = true;
    }
    if (this.tf != null)
      flag = this.tf.PerformAttrChange(fromAttribute, toAttribute) | flag;
    if (this.formX != null)
      flag = this.formX.PerformAttrChange(fromAttribute, toAttribute) | flag;
    if (this.formY != null)
      flag = this.formY.PerformAttrChange(fromAttribute, toAttribute) | flag;
    return flag;
  }
}
