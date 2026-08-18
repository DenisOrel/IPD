// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ModParmLoop
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Loop parms</summary>
public class ModParmLoop : ModParm
{
  public TempFormula tf;
  public bool whileLoop = true;
  public string attrGUID = "";
  public string attrText = "";
  public int startWith;

  public string RefGuid { get; set; }

  public string RefText { get; set; }

  public ModParmLoop()
  {
    this.RefGuid = "";
    this.RefText = "";
  }

  public ModParmLoop(ref ModParmData modData)
    : base(ref modData)
  {
    this.SetData(ref modData);
  }

  public override void SetData(ref ModParmData modData)
  {
    this.whileLoop = !modData.ForLoop;
    if (this.whileLoop)
    {
      int count = modData.tf.Count;
    }
    if (!this.whileLoop)
    {
      if (modData.ForAttrGUID == "")
        throw new AbortException(LocalizationHolder.rm.GetString("Expert_128"));
      if (modData.tf.Count == 0)
        throw new AbortException(LocalizationHolder.rm.GetString("Expert_129"));
    }
    this.tf = modData.tf.Count != 0 ? (TempFormula) modData.tf.Clone() : (TempFormula) null;
    this.attrGUID = modData.ForAttrGUID;
    this.attrText = modData.ForAttrText;
    if (modData.sortGUIDs.Count > 0)
      this.RefGuid = modData.sortGUIDs[0];
    if (modData.sortTexts.Count > 0)
      this.RefText = modData.sortTexts[0];
    this.startWith = modData.startValue;
  }

  public override void Clear()
  {
  }

  public override void FillModParmData(ref ModParmData modData)
  {
    modData.Clear();
    if (this.tf != null)
      modData.tf = (TempFormula) this.tf.Clone();
    modData.ForLoop = !this.whileLoop;
    modData.ForAttrGUID = this.attrGUID;
    modData.ForAttrText = this.attrText;
    modData.startValue = this.startWith;
    modData.sortGUIDs.Clear();
    modData.sortGUIDs.Add(this.RefGuid);
    modData.sortTexts.Clear();
    modData.sortTexts.Add(this.RefText);
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    if (this.tf != null)
      this.tf.WriteToXML(ref writer);
    writer.WriteElementString("while-loop", this.whileLoop ? "Y" : "N");
    if (this.attrGUID != "")
    {
      writer.WriteStartElement("Attr-Link");
      writer.WriteElementString("GUID", this.attrGUID);
      writer.WriteElementString("Name", this.attrText);
      writer.WriteEndElement();
    }
    if (this.RefGuid != "")
    {
      writer.WriteStartElement("Ref-Attr");
      writer.WriteElementString("GUID", this.RefGuid);
      writer.WriteElementString("Name", this.RefText);
      writer.WriteEndElement();
    }
    writer.WriteElementString("startWith", Convert.ToString(this.startWith));
  }

  public override void LoadFromXML(XmlNode node, int modTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Formula")
        this.tf = new TempFormula(childNode);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "while-loop")
        this.whileLoop = childNode.InnerText == "Y";
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Attr-Link" && childNode.HasChildNodes)
      {
        this.attrGUID = childNode.ChildNodes[0].InnerText;
        this.attrText = childNode.ChildNodes[1].InnerText;
      }
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Ref-Attr" && childNode.HasChildNodes)
      {
        this.RefGuid = childNode.ChildNodes[0].InnerText;
        this.RefText = childNode.ChildNodes[1].InnerText;
      }
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "startWith")
        this.startWith = Convert.ToInt32(childNode.InnerText);
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
