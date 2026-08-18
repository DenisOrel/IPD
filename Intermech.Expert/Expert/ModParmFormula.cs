// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ModParmFormula
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Formulae and condition parms</summary>
public class ModParmFormula : ModParm
{
  public TempFormula tf;
  public bool saveContext;
  public bool forAllIsps;

  public ModParmFormula()
  {
  }

  public ModParmFormula(ref ModParmData modData)
    : base(ref modData)
  {
    this.SetData(ref modData);
  }

  public override void SetData(ref ModParmData modData)
  {
    this.tf = modData.tf.Count != 0 ? (TempFormula) modData.tf.Clone() : (TempFormula) null;
    this.saveContext = modData.startValue != 0;
    this.forAllIsps = modData.ForLoop;
  }

  public override void Clear()
  {
    if (this.tf != null)
      this.tf = (TempFormula) null;
    this.saveContext = false;
    this.forAllIsps = false;
  }

  public override void FillModParmData(ref ModParmData modData)
  {
    modData.Clear();
    if (this.tf != null)
      modData.tf = (TempFormula) this.tf.Clone();
    modData.startValue = this.saveContext ? 1 : 0;
    modData.ForLoop = this.forAllIsps;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.tf != null)
      this.tf.WriteToXML(ref writer);
    writer.WriteElementString("saveContext", this.saveContext ? "Y" : "N");
    writer.WriteElementString("forAllIsps", this.forAllIsps ? "Y" : "N");
  }

  public override void LoadFromXML(XmlNode node, int modTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Formula")
        this.tf = new TempFormula(childNode);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "saveContext")
        this.saveContext = childNode.InnerText == "Y";
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "forAllIsps")
        this.forAllIsps = childNode.InnerText == "Y";
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
    return this.tf == null || this.tf.CollectExpObjInfo(eoi, ius);
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
    return this.tf != null && this.tf.PerformAttrChange(fromAttribute, toAttribute);
  }
}
