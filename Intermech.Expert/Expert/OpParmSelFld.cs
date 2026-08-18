// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmSelFld
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Select doc fld operator parms</summary>
public class OpParmSelFld : OpParm
{
  public TempFormula tf;
  public string FldId = "";
  public string FldName = "";
  public bool selWholeDoc;
  public bool selAncestor;
  public bool byDefault;

  public OpParmSelFld()
  {
  }

  public OpParmSelFld(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.tf = opData.tf.Count != 0 ? (TempFormula) opData.tf.Clone() : (TempFormula) null;
    this.FldId = opData.s1;
    this.FldName = opData.st3;
    this.selWholeDoc = opData.b1;
    this.selAncestor = opData.b2;
    this.byDefault = opData.b3;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    if (this.tf != null)
      opData.tf = (TempFormula) this.tf.Clone();
    opData.s1 = this.FldId;
    opData.st3 = this.FldName;
    opData.b1 = this.selWholeDoc;
    opData.b2 = this.selAncestor;
    opData.b3 = this.byDefault;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.tf != null)
    {
      this.tf.FillObjectLinks();
      this.tf.WriteToXML(ref writer);
    }
    if (this.FldId != "")
      writer.WriteElementString("ID", this.FldId);
    if (this.FldName != "")
      writer.WriteElementString("Name", this.FldName);
    if (this.selWholeDoc)
      writer.WriteElementString("selDoc", "yes");
    if (this.selAncestor)
      writer.WriteElementString("selAncestor", "yes");
    if (!this.byDefault)
      return;
    writer.WriteElementString("byDefault", "yes");
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name == "ID")
        this.FldId = childNode.InnerText;
      else if (childNode.Name == "Name")
        this.FldName = childNode.InnerText;
      else if (childNode.Name == "selDoc")
        this.selWholeDoc = true;
      else if (childNode.Name == "selAncestor")
        this.selAncestor = true;
      else if (childNode.Name == "byDefault")
        this.byDefault = true;
      else if (childNode.Name == "Formula")
        this.tf = new TempFormula(childNode);
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
    bool flag = true;
    if (this.tf != null)
      flag = this.tf.CollectExpObjInfo(eoi, ius);
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
    if (this.tf != null)
      flag = this.tf.PerformAttrChange(fromAttribute, toAttribute);
    return flag;
  }
}
