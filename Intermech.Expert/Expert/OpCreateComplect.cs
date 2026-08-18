// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpCreateComplect
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Create complects of documents</summary>
public class OpCreateComplect : OpParm
{
  public string objTypeGUID = "";
  public string objTypeText = "";
  public string compObjTypeGUID = "";
  public string compObjTypeText = "";
  public bool needComplect;
  public string postfix = "";
  public TempFormula cond;
  public bool useCoWorkerComp;
  public bool additional;

  public OpCreateComplect()
  {
  }

  public OpCreateComplect(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.objTypeGUID = opData.s1;
    this.objTypeText = opData.s2;
    this.compObjTypeGUID = opData.s3;
    this.compObjTypeText = opData.s4;
    this.cond = opData.tf.Count != 0 ? (TempFormula) opData.tf.Clone() : (TempFormula) null;
    this.needComplect = opData.b1;
    this.postfix = opData.st1;
    this.useCoWorkerComp = opData.b2;
    this.additional = opData.b3;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    opData.s1 = this.objTypeGUID;
    opData.s2 = this.objTypeText;
    opData.s3 = this.compObjTypeGUID;
    opData.s4 = this.compObjTypeText;
    if (this.cond != null)
      opData.tf = (TempFormula) this.cond.Clone();
    opData.b1 = this.needComplect;
    opData.st1 = this.postfix;
    opData.b2 = this.useCoWorkerComp;
    opData.b3 = this.additional;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteElementString("GUID", this.objTypeGUID);
    writer.WriteElementString("Text", this.objTypeText);
    writer.WriteElementString("compGUID", this.compObjTypeGUID);
    writer.WriteElementString("compText", this.compObjTypeText);
    writer.WriteElementString("postfix", this.postfix);
    writer.WriteElementString("needComplect", this.needComplect ? "Y" : "N");
    writer.WriteElementString("coWorkerComp", this.useCoWorkerComp ? "Y" : "N");
    writer.WriteElementString("additional", this.additional ? "Y" : "N");
    if (this.cond == null)
      return;
    this.cond.FillObjectLinks();
    this.cond.WriteToXML(ref writer);
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
      else if (childNode.Name == "compGUID")
        this.compObjTypeGUID = childNode.InnerText;
      else if (childNode.Name == "compText")
        this.compObjTypeText = childNode.InnerText;
      else if (childNode.Name == "needComplect")
        this.needComplect = childNode.InnerText == "Y";
      else if (childNode.Name == "coWorkerComp")
        this.useCoWorkerComp = childNode.InnerText == "Y";
      else if (childNode.Name == "additional")
        this.additional = childNode.InnerText == "Y";
      else if (childNode.Name == "postfix")
        this.postfix = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Formula")
        this.cond = new TempFormula(childNode);
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs)
  {
    return this.cond != null && this.cond.FixIDs(attrs, objs);
  }

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    return this.cond != null && this.cond.CollectGUIDs(attrs, objs);
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    return this.cond != null && this.cond.FixIdentsComplete(ius);
  }

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = eoi.AddObjType(this.objTypeGUID) && eoi.AddObjType(this.compObjTypeGUID);
    if (this.cond != null)
      flag = flag && this.cond.CollectExpObjInfo(eoi, ius);
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
    return flag;
  }
}
