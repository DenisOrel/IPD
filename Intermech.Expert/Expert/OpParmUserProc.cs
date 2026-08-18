// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmUserProc
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

/// <summary>Call the extrrnal proc written by user</summary>
public class OpParmUserProc : OpParm
{
  public ExpertCalling type = ExpertCalling.callUserProc;
  public string procName = "";
  public int scriptType;
  public TempFormula parm1;
  public TempFormula parm2;

  public OpParmUserProc()
  {
  }

  public OpParmUserProc(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.parm1 = opData.tf == null || opData.tf.Count == 0 ? (TempFormula) null : (TempFormula) opData.tf.Clone();
    this.parm2 = opData.tf2 == null || opData.tf2.Count == 0 ? (TempFormula) null : (TempFormula) opData.tf2.Clone();
    this.procName = opData.s1;
    this.type = (ExpertCalling) Convert.ToInt32(opData.s2);
    this.scriptType = (int) opData.exID;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    if (this.parm1 != null)
      opData.tf = (TempFormula) this.parm1.Clone();
    if (this.parm2 != null)
      opData.tf2 = (TempFormula) this.parm2.Clone();
    opData.s1 = this.procName;
    opData.s2 = Convert.ToString((int) this.type);
    opData.exID = (long) this.scriptType;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.procName != "")
      writer.WriteElementString("Name", this.procName);
    writer.WriteElementString("Type", Convert.ToString(Convert.ToInt32((object) this.type)));
    writer.WriteElementString("scriptType", Convert.ToString(this.scriptType));
    if (this.parm1 != null)
    {
      this.parm1.FillObjectLinks();
      this.parm1.WriteToXML(ref writer, "FormParm1");
    }
    if (this.parm2 == null)
      return;
    this.parm2.FillObjectLinks();
    this.parm2.WriteToXML(ref writer, "FormParm2");
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Name")
        this.procName = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Type")
        this.type = (ExpertCalling) Convert.ToInt32(childNode.InnerText);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "scriptType")
        this.scriptType = Convert.ToInt32(childNode.InnerText);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "FormParm1")
        this.parm1 = new TempFormula(childNode);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "FormParm2")
        this.parm2 = new TempFormula(childNode);
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool FixIdentsComplete(IUserSession ius) => false;

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = true;
    if (this.parm1 != null)
      flag = this.parm1.CollectExpObjInfo(eoi, ius);
    if (this.parm2 != null)
      flag = flag && this.parm2.CollectExpObjInfo(eoi, ius);
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
    if (this.parm1 != null)
      flag = this.parm1.PerformAttrChange(fromAttribute, toAttribute);
    if (this.parm2 != null)
      flag = this.parm2.PerformAttrChange(fromAttribute, toAttribute) | flag;
    return flag;
  }
}
