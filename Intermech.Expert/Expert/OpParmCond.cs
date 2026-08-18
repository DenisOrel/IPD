// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmCond
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Condition parms</summary>
public class OpParmCond : OpParm
{
  public TempFormula cond;
  public string refAttrGuid = "";
  public string refAttrName = "";

  public OpParmCond()
  {
  }

  public OpParmCond(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.cond = opData.tf.Count != 0 ? (TempFormula) opData.tf.Clone() : (TempFormula) null;
    this.refAttrGuid = opData.s3;
    this.refAttrName = opData.st3;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    if (this.cond != null)
      opData.tf = (TempFormula) this.cond.Clone();
    opData.s3 = this.refAttrGuid;
    opData.st3 = this.refAttrName;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.cond != null)
    {
      this.cond.FillObjectLinks();
      this.cond.WriteToXML(ref writer);
    }
    if (!(this.refAttrGuid != ""))
      return;
    writer.WriteStartElement("RefAttr");
    writer.WriteElementString("Guid", this.refAttrGuid);
    writer.WriteElementString("Name", this.refAttrName);
    writer.WriteEndElement();
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Formula")
        this.cond = new TempFormula(childNode);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "RefAttr" && childNode.HasChildNodes)
      {
        this.refAttrGuid = childNode.ChildNodes[0].InnerText;
        if (!GuidHelper.IsGuid(this.refAttrGuid))
          this.refAttrGuid = "";
        this.refAttrName = childNode.ChildNodes[1].InnerText;
      }
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
    bool flag = eoi.AddAttrType(this.refAttrGuid);
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
    if (this.refAttrGuid == fromAttribute.GUID.ToString())
    {
      this.refAttrGuid = toAttribute.GUID.ToString();
      this.refAttrName = toAttribute.Name;
      flag = true;
    }
    if (this.cond != null)
      flag = this.cond.PerformAttrChange(fromAttribute, toAttribute) | flag;
    return flag;
  }
}
