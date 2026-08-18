// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmFillFld
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

/// <summary>Fill doc fld operator parms</summary>
public class OpParmFillFld : OpParm
{
  public string FldID = "";
  public string FldName = "";
  public string AddAttrGUID = "";
  public string AddAttrText = "";
  public bool ActiveLink;
  public bool LinkThisDoc;
  public string attrGUID = "";
  public string objTypeGUID = "";
  public string attrText = "";
  public string objTypeText = "";
  public TempFormula tf;
  public TempFormula _leftInd;
  public string FontName = "";
  public long FontSize;
  public bool Bold;
  public bool Italic;
  public bool Underline;
  public int Color;
  public bool AuthFile;
  public string Tag = "";

  public OpParmFillFld()
  {
  }

  public OpParmFillFld(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.tf = opData.tf.Count != 0 ? (TempFormula) opData.tf.Clone() : (TempFormula) null;
    this._leftInd = opData.tf2 == null || opData.tf2.Count == 0 ? (TempFormula) null : (TempFormula) opData.tf2.Clone();
    this.FontName = opData.s5;
    this.FontSize = opData.exID;
    this.Bold = opData.b2;
    this.Italic = opData.b3;
    this.Underline = opData.b4;
    this.LinkThisDoc = opData.b5;
    this.AuthFile = opData.b6;
    this.FldID = opData.s3;
    this.AddAttrGUID = opData.s4;
    this.attrGUID = opData.s1;
    this.objTypeGUID = opData.s2;
    this.AddAttrText = opData.st3;
    this.attrText = opData.st1;
    this.objTypeText = opData.st2;
    this.FldName = opData.st4;
    this.ActiveLink = opData.b1;
    this.Tag = opData.s6;
    this.Color = opData.settingMod;
  }

  public bool fillAttr() => this.attrGUID != "";

  public bool fillFormula() => this.tf != null;

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    if (this.tf != null)
      opData.tf = (TempFormula) this.tf.Clone();
    if (this._leftInd != null)
      opData.tf2 = (TempFormula) this._leftInd.Clone();
    opData.s5 = this.FontName;
    opData.exID = this.FontSize;
    opData.b2 = this.Bold;
    opData.b3 = this.Italic;
    opData.b4 = this.Underline;
    opData.b5 = this.LinkThisDoc;
    opData.b6 = this.AuthFile;
    opData.s3 = this.FldID;
    opData.s4 = this.AddAttrGUID;
    opData.s1 = this.attrGUID;
    opData.s2 = this.objTypeGUID;
    opData.st3 = this.AddAttrText;
    opData.st1 = this.attrText;
    opData.st2 = this.objTypeText;
    opData.st4 = this.FldName;
    opData.b1 = this.ActiveLink;
    opData.s6 = this.Tag;
    opData.settingMod = this.Color;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteElementString("ID", this.FldID);
    writer.WriteElementString("Name", this.FldName);
    if (this.FontSize > 0L)
    {
      writer.WriteElementString("Font", this.FontName);
      writer.WriteElementString("FontSize", Convert.ToString(this.FontSize));
    }
    writer.WriteElementString("Bold", this.Bold ? "Y" : "N");
    writer.WriteElementString("Italic", this.Italic ? "Y" : "N");
    writer.WriteElementString("Underline", this.Underline ? "Y" : "N");
    writer.WriteElementString("ActiveLink", this.ActiveLink ? "Y" : "N");
    writer.WriteElementString("LinkThisDoc", this.LinkThisDoc ? "Y" : "N");
    writer.WriteElementString("AuthFile", this.AuthFile ? "Y" : "N");
    writer.WriteElementString("Tag", this.Tag);
    if (this.AddAttrGUID != "")
    {
      writer.WriteStartElement("Attr-Link");
      writer.WriteElementString("GUID", this.AddAttrGUID);
      writer.WriteElementString("Name", this.AddAttrText);
      writer.WriteEndElement();
    }
    if (this.attrGUID != "")
    {
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
    }
    if (this.tf != null)
    {
      this.tf.FillObjectLinks();
      this.tf.WriteToXML(ref writer);
    }
    if (this._leftInd != null)
    {
      this._leftInd.FillObjectLinks();
      this._leftInd.WriteToXML(ref writer, "FormLeftIndent");
    }
    writer.WriteElementString("Color", Convert.ToString(this.Color));
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode1 in node.ChildNodes)
    {
      if (childNode1.Name == "ID")
        this.FldID = childNode1.InnerText;
      else if (childNode1.Name == "Name")
        this.FldName = childNode1.InnerText;
      else if (childNode1.Name == "Font")
        this.FontName = childNode1.InnerText;
      else if (childNode1.Name == "FontSize")
        this.FontSize = Convert.ToInt64(childNode1.InnerText);
      else if (childNode1.Name == "ActiveLink")
        this.ActiveLink = childNode1.InnerText == "Y";
      else if (childNode1.Name == "Bold")
        this.Bold = childNode1.InnerText == "Y";
      else if (childNode1.Name == "Italic")
        this.Italic = childNode1.InnerText == "Y";
      else if (childNode1.Name == "Underline")
        this.Underline = childNode1.InnerText == "Y";
      else if (childNode1.Name == "LinkThisDoc")
        this.LinkThisDoc = childNode1.InnerText == "Y";
      else if (childNode1.Name == "AuthFile")
        this.AuthFile = childNode1.InnerText == "Y";
      else if (childNode1.Name == "Attr-Link")
      {
        if (childNode1.HasChildNodes && childNode1.ChildNodes.Count > 1)
        {
          this.AddAttrGUID = childNode1.ChildNodes[0].InnerText;
          this.AddAttrText = childNode1.ChildNodes[1].InnerText;
        }
      }
      else if (childNode1.Name == "Attr-ObjType" && childNode1.HasChildNodes)
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
      else if (childNode1.Name == "Formula")
        this.tf = new TempFormula(childNode1);
      else if (childNode1.Name == "FormLeftIndent")
        this._leftInd = new TempFormula(childNode1);
      else if (childNode1.Name == "Tag")
        this.Tag = childNode1.InnerText;
      else if (childNode1.Name == "Color")
        this.Color = Convert.ToInt32(childNode1.InnerText);
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs)
  {
    return this.tf != null && this.tf.FixIDs(attrs, objs);
  }

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    return this.tf != null && this.tf.CollectGUIDs(attrs, objs);
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    return this.tf != null && this.tf.FixIdentsComplete(ius);
  }

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = eoi.AddAttrType(this.attrGUID) && eoi.AddObjType(this.objTypeGUID) && eoi.AddAttrType(this.AddAttrGUID);
    if (this.tf != null)
      flag = flag && this.tf.CollectExpObjInfo(eoi, ius);
    if (this._leftInd != null)
      flag = flag && this._leftInd.CollectExpObjInfo(eoi, ius);
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
    if (this.AddAttrGUID == fromAttribute.GUID.ToString())
    {
      this.AddAttrGUID = toAttribute.GUID.ToString();
      this.AddAttrText = toAttribute.Name;
      flag = true;
    }
    if (this.attrGUID == fromAttribute.GUID.ToString())
    {
      this.attrGUID = toAttribute.GUID.ToString();
      this.attrText = toAttribute.Name;
      flag = true;
    }
    if (this.tf != null)
      flag = this.tf.PerformAttrChange(fromAttribute, toAttribute) | flag;
    return flag;
  }
}
