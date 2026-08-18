// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpCreateDoc
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

/// <summary>Create complects of documents</summary>
public class OpCreateDoc : OpParm
{
  public string objTypeGUID = "";
  public string objTypeText = "";
  public string scriptGUID = "";
  public string scriptText = "";
  public string prefix = "";
  public bool noEmpty;
  public bool secondPass;
  public string docType = "";
  public TempFormula cond;
  public DocGroupMode groupMode;
  public bool useCoWorkerDoc;
  public bool dontNumber;
  public bool dontCount;
  public TempFormula createCond;

  public OpCreateDoc()
  {
  }

  public OpCreateDoc(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.objTypeGUID = opData.s1;
    this.objTypeText = opData.s2;
    this.scriptGUID = opData.s3;
    this.scriptText = opData.s4;
    this.prefix = opData.st1;
    this.noEmpty = opData.b1;
    this.secondPass = opData.b2;
    this.docType = opData.st2;
    this.groupMode = (DocGroupMode) Convert.ToInt32(opData.s5);
    this.useCoWorkerDoc = opData.b4;
    this.dontNumber = opData.b5;
    this.dontCount = opData.b6;
    this.cond = opData.tf.Count != 0 ? (TempFormula) opData.tf.Clone() : (TempFormula) null;
    if (opData.tf2.Count == 0)
      this.createCond = (TempFormula) null;
    else
      this.createCond = (TempFormula) opData.tf2.Clone();
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    opData.s1 = this.objTypeGUID;
    opData.s2 = this.objTypeText;
    opData.s3 = this.scriptGUID;
    opData.s4 = this.scriptText;
    opData.b1 = this.noEmpty;
    opData.b2 = this.secondPass;
    opData.st2 = this.docType;
    opData.s5 = Convert.ToString((int) this.groupMode);
    opData.b4 = this.useCoWorkerDoc;
    opData.b5 = this.dontNumber;
    opData.b6 = this.dontCount;
    opData.st1 = this.prefix;
    if (this.cond != null)
      opData.tf = (TempFormula) this.cond.Clone();
    if (this.createCond == null)
      return;
    opData.tf2 = (TempFormula) this.createCond.Clone();
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteElementString("GUID", this.objTypeGUID);
    writer.WriteElementString("Text", this.objTypeText);
    writer.WriteElementString("scr-GUID", this.scriptGUID);
    writer.WriteElementString("scr-Text", this.scriptText);
    writer.WriteElementString("noEmpty", this.noEmpty ? "Y" : "N");
    writer.WriteElementString("secondPass", this.secondPass ? "Y" : "N");
    writer.WriteElementString("scenario", this.docType);
    writer.WriteElementString("prefix", this.prefix);
    writer.WriteElementString("groupMode", Convert.ToString((int) this.groupMode));
    writer.WriteElementString("coWorkerDoc", this.useCoWorkerDoc ? "Y" : "N");
    writer.WriteElementString("dontNumber", this.dontNumber ? "Y" : "N");
    writer.WriteElementString("dontCount", this.dontCount ? "Y" : "N");
    if (this.cond != null)
    {
      this.cond.FillObjectLinks();
      this.cond.WriteToXML(ref writer);
    }
    if (this.createCond == null)
      return;
    this.createCond.FillObjectLinks();
    this.createCond.WriteToXML(ref writer, "formCreateCond");
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name == "GUID")
        this.objTypeGUID = childNode.InnerText;
      else if (childNode.Name == "Text")
        this.objTypeText = childNode.InnerText;
      else if (childNode.Name == "scr-GUID")
        this.scriptGUID = childNode.InnerText;
      else if (childNode.Name == "scr-Text")
        this.scriptText = childNode.InnerText;
      else if (childNode.Name == "prefix")
        this.prefix = childNode.InnerText;
      else if (childNode.Name == "noEmpty")
        this.noEmpty = childNode.InnerText == "Y";
      else if (childNode.Name == "secondPass")
        this.secondPass = childNode.InnerText == "Y";
      else if (childNode.Name == "scenario")
        this.docType = childNode.InnerText;
      else if (childNode.Name == "dontNumber")
        this.dontNumber = childNode.InnerText == "Y";
      else if (childNode.Name == "dontCount")
        this.dontCount = childNode.InnerText == "Y";
      else if (childNode.Name == "groupMode")
        this.groupMode = (DocGroupMode) Convert.ToInt32(childNode.InnerText);
      else if (childNode.Name == "coWorkerDoc")
        this.useCoWorkerDoc = childNode.InnerText == "Y";
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Formula")
        this.cond = new TempFormula(childNode);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "formCreateCond")
        this.createCond = new TempFormula(childNode);
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs)
  {
    if (this.cond != null)
      return this.cond.FixIDs(attrs, objs);
    return this.createCond != null && this.createCond.FixIDs(attrs, objs);
  }

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    if (this.cond != null)
      return this.cond.CollectGUIDs(attrs, objs);
    return this.createCond != null && this.createCond.CollectGUIDs(attrs, objs);
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    if (this.cond != null)
      return this.cond.FixIdentsComplete(ius);
    return this.createCond != null && this.createCond.FixIdentsComplete(ius);
  }

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = eoi.AddObjType(this.objTypeGUID);
    Guid result = Guid.Empty;
    if (Guid.TryParse(this.scriptGUID, out result))
      flag = flag && eoi.AddObjLink(result, ius);
    if (this.cond != null)
      flag = flag && this.cond.CollectExpObjInfo(eoi, ius);
    if (this.createCond != null)
      flag = flag && this.createCond.CollectExpObjInfo(eoi, ius);
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
    if (this.createCond != null)
      flag = this.createCond.PerformAttrChange(fromAttribute, toAttribute);
    return flag;
  }
}
