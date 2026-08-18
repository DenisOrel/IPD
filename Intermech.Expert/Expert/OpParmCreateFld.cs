// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmCreateFld
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

public class OpParmCreateFld : OpParm
{
  public string FldID = "";
  public string FldName = "";
  public string AddAttrGUID = "";
  public string SaveIDAttrGUID = "";
  public string AddAttrText = "";
  public string SaveIDAttrText = "";
  public bool makeNewCurrent = true;
  public bool curForever;
  public bool fillChildren;
  public bool byDefault;
  public bool avoidDup;
  public string Tag = "";

  public OpParmCreateFld()
  {
  }

  public OpParmCreateFld(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.FldID = opData.s1;
    this.AddAttrGUID = opData.s4;
    this.SaveIDAttrGUID = opData.s3;
    this.AddAttrText = opData.st3;
    this.SaveIDAttrText = opData.st2;
    this.makeNewCurrent = opData.b1;
    this.fillChildren = opData.b2;
    this.byDefault = opData.b3;
    this.avoidDup = opData.b4;
    this.FldName = opData.st4;
    this.curForever = opData.b5;
    this.Tag = opData.s5;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.s1 = this.FldID;
    opData.s4 = this.AddAttrGUID;
    opData.s3 = this.SaveIDAttrGUID;
    opData.st3 = this.AddAttrText;
    opData.st2 = this.SaveIDAttrText;
    opData.b1 = this.makeNewCurrent;
    opData.b2 = this.fillChildren;
    opData.b3 = this.byDefault;
    opData.b4 = this.avoidDup;
    opData.st4 = this.FldName;
    opData.b5 = this.curForever;
    opData.s5 = this.Tag;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteElementString("ID", this.FldID);
    writer.WriteElementString("Name", this.FldName);
    if (this.AddAttrGUID != "")
    {
      writer.WriteStartElement("Attr-Link");
      writer.WriteElementString("GUID", this.AddAttrGUID);
      writer.WriteElementString("Name", this.AddAttrText);
      writer.WriteEndElement();
    }
    if (this.SaveIDAttrGUID != "")
    {
      writer.WriteStartElement("save-attr");
      writer.WriteElementString("GUID", this.SaveIDAttrGUID);
      writer.WriteElementString("Name", this.SaveIDAttrText);
      writer.WriteEndElement();
    }
    writer.WriteElementString("make-current", this.makeNewCurrent ? "Y" : "N");
    writer.WriteElementString("fill-children", this.fillChildren ? "Y" : "N");
    writer.WriteElementString("byDefault", this.byDefault ? "Y" : "N");
    writer.WriteElementString("avoidDup", this.avoidDup ? "Y" : "N");
    writer.WriteElementString("curForever", this.curForever ? "Y" : "N");
    writer.WriteElementString("Tag", this.Tag);
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name == "ID")
        this.FldID = childNode.InnerText;
      else if (childNode.Name == "Name")
        this.FldName = childNode.InnerText;
      else if (childNode.Name == "Attr-Link" && childNode.HasChildNodes && childNode.ChildNodes.Count > 1)
      {
        this.AddAttrGUID = childNode.ChildNodes[0].InnerText;
        this.AddAttrText = childNode.ChildNodes[1].InnerText;
      }
      else if (childNode.Name == "save-attr")
      {
        this.SaveIDAttrGUID = childNode.ChildNodes[0].InnerText;
        this.SaveIDAttrText = childNode.ChildNodes[1].InnerText;
      }
      else if (childNode.Name == "make-current")
        this.makeNewCurrent = childNode.InnerText == "Y";
      else if (childNode.Name == "fill-children")
        this.fillChildren = childNode.InnerText == "Y";
      else if (childNode.Name == "byDefault")
        this.byDefault = childNode.InnerText == "Y";
      else if (childNode.Name == "avoidDup")
        this.avoidDup = childNode.InnerText == "Y";
      else if (childNode.Name == "curForever")
        this.curForever = childNode.InnerText == "Y";
      else if (childNode.Name == "Tag")
        this.Tag = childNode.InnerText;
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool FixIdentsComplete(IUserSession ius) => false;

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    eoi.AddAttrType(this.AddAttrGUID);
    eoi.AddAttrType(this.SaveIDAttrGUID);
    return true;
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
    if (this.SaveIDAttrGUID == fromAttribute.GUID.ToString())
    {
      this.SaveIDAttrGUID = toAttribute.GUID.ToString();
      this.SaveIDAttrText = toAttribute.Name;
      flag = true;
    }
    return flag;
  }
}
