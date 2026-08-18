// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.FormulaForAttribute
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Класс хранит данные для использования формулы на атрибуты
/// </summary>
[Serializable]
public class FormulaForAttribute
{
  public string Formula { get; set; }

  /// <summary>
  /// Распространить ли формулу на все объекты, собранные для генерации уведомления.
  /// </summary>
  public bool SpreadFormulaForObject { get; set; }

  /// <summary>
  /// Использовать новые значения атрибутов (false) или старые (true).
  /// </summary>
  public bool UseOldAttrValues { get; set; }

  public FormulaForAttribute()
  {
    this.Formula = string.Empty;
    this.SpreadFormulaForObject = false;
    this.UseOldAttrValues = false;
  }

  public FormulaForAttribute(string formula, bool spreadFormula, bool useOldAttrValues)
  {
    this.Formula = formula;
    this.SpreadFormulaForObject = spreadFormula;
    this.UseOldAttrValues = useOldAttrValues;
  }

  /// <summary>Создать xml-узел для формулы на атрибуты.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел для формулы на атрибуты</returns>
  public XmlNode WriteXmlNode(XmlDocument xmlDoc)
  {
    XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "FormulaForAttr", string.Empty);
    XmlNode node2 = xmlDoc.CreateNode(XmlNodeType.Element, "FormulaStr", string.Empty);
    node2.InnerText = this.Formula;
    node1.AppendChild(node2);
    XmlAttribute attribute1 = xmlDoc.CreateAttribute("SpreadFormula");
    attribute1.Value = this.SpreadFormulaForObject.ToString();
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("UseOldAttrValues");
    attribute2.Value = this.UseOldAttrValues.ToString();
    if (node1.Attributes != null)
    {
      node1.Attributes.Append(attribute1);
      node1.Attributes.Append(attribute2);
    }
    return node1;
  }

  /// <summary>Прочитать данные из хмл.</summary>
  /// <param name="node">The node.</param>
  public void ReadFromXml(XmlNode node)
  {
    this.SpreadFormulaForObject = Convert.ToBoolean(node.Attributes["SpreadFormula"].Value);
    this.UseOldAttrValues = Convert.ToBoolean(node.Attributes["UseOldAttrValues"].Value);
    for (int i = 0; i < node.ChildNodes.Count; ++i)
    {
      XmlNode childNode = node.ChildNodes[i];
      if (childNode.Name == "FormulaStr")
        this.Formula = childNode.InnerText;
    }
  }
}
