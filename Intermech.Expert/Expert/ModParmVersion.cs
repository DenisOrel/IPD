// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ModParmVersion
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

public class ModParmVersion : ModParm
{
  public TempFormula cond;
  public VerSort sortMode;
  public bool descending;
  public bool forAllVersions;

  public ModParmVersion()
  {
  }

  public ModParmVersion(ref ModParmData modData)
    : base(ref modData)
  {
    this.SetData(ref modData);
  }

  public override void SetData(ref ModParmData modData)
  {
    this.cond = modData.tf.Count != 0 ? (TempFormula) modData.tf.Clone() : (TempFormula) null;
    this.sortMode = (VerSort) modData.startValue;
    this.forAllVersions = modData.ForLoop;
    this.descending = modData.Bool1;
  }

  public override void Clear()
  {
    if (this.cond != null)
      this.cond = (TempFormula) null;
    this.sortMode = VerSort.VerId;
    this.forAllVersions = false;
  }

  public override void FillModParmData(ref ModParmData modData)
  {
    modData.Clear();
    if (this.cond != null)
      modData.tf = (TempFormula) this.cond.Clone();
    modData.startValue = (int) this.sortMode;
    modData.ForLoop = this.forAllVersions;
    modData.Bool1 = this.descending;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.cond != null)
      this.cond.WriteToXML(ref writer);
    writer.WriteElementString("saveContext", ((int) this.sortMode).ToString());
    writer.WriteElementString("forAllVers", this.forAllVersions ? "Y" : "N");
    writer.WriteElementString("descending", this.descending ? "Y" : "N");
  }

  public override void LoadFromXML(XmlNode node, int modTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Formula")
        this.cond = new TempFormula(childNode);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "saveContext")
        this.sortMode = (VerSort) Convert.ToInt32(childNode.InnerText);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "forAllVers")
        this.forAllVersions = childNode.InnerText == "Y";
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "descending")
        this.descending = childNode.InnerText == "Y";
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
    return this.cond == null || this.cond.CollectExpObjInfo(eoi, ius);
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
    return this.cond != null && this.cond.PerformAttrChange(fromAttribute, toAttribute);
  }
}
