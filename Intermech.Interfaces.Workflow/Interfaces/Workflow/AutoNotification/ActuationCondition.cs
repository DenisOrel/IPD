// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.ActuationCondition
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Условия срабатывания автоуведомления</summary>
[Serializable]
public class ActuationCondition
{
  /// <summary>Формула на атрибуты и сопутствующая инфа.</summary>
  public FormulaForAttribute FormulaForAttribute { get; set; }

  /// <summary>ИД скрипта</summary>
  public long ScriptID { get; set; }

  public ActuationCondition()
  {
    this.FormulaForAttribute = new FormulaForAttribute();
    this.ScriptID = 0L;
  }

  public ActuationCondition(FormulaForAttribute formula, long scriptID)
  {
    this.FormulaForAttribute = formula;
    this.ScriptID = scriptID;
  }

  /// <summary>Создать xml-узел Условий срабатывания уведомлений.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Условий срабатывания уведомлений</returns>
  public virtual XmlNode CreateXmlNode(XmlDocument xmlDoc)
  {
    XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, nameof (ActuationCondition), string.Empty);
    XmlNode newChild = this.FormulaForAttribute.WriteXmlNode(xmlDoc);
    node.AppendChild(newChild);
    XmlAttribute attribute = xmlDoc.CreateAttribute("ScriptIdAttr");
    attribute.Value = this.ScriptID.ToString();
    if (node.Attributes != null)
      node.Attributes.Append(attribute);
    return node;
  }

  /// <summary>Прочитать из XML.</summary>
  /// <param name="xmlNode">The XML node.</param>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public void ReadFromXml(XmlNode xmlNode)
  {
    this.ScriptID = Convert.ToInt64(xmlNode.Attributes["ScriptIdAttr"].Value);
    for (int i = 0; i < xmlNode.ChildNodes.Count; ++i)
    {
      XmlNode childNode = xmlNode.ChildNodes[i];
      if (childNode.Name == "FormulaForAttr")
        this.FormulaForAttribute.ReadFromXml(childNode);
    }
  }

  /// <summary>Указана ли формула для атрибутов.</summary>
  /// <returns>True - если указана</returns>
  public bool HasFormula() => this.FormulaForAttribute.Formula != string.Empty;

  /// <summary>Checks the script.</summary>
  /// <returns></returns>
  private bool CheckScript() => true;
}
