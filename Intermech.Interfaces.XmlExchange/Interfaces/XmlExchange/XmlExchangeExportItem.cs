// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportItem
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Базовый класс для хранения настроек экспорта</summary>
[Serializable]
public abstract class XmlExchangeExportItem
{
  /// <summary>Получение наименование секции элемента в XML</summary>
  /// <returns></returns>
  private string GetXmlTypeName()
  {
    return this.GetType().GetCustomAttributes<XmlRootAttribute>().First<XmlRootAttribute>()?.ElementName ?? throw new Exception("XmlRootAttribute not found");
  }

  /// <summary>
  /// 
  /// </summary>
  protected internal XmlExchangeExportItem()
  {
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public virtual bool LoadData(XmlNode xmlNode)
  {
    if (xmlNode == null || string.Compare(xmlNode.Name, this.GetXmlTypeName(), StringComparison.OrdinalIgnoreCase) != 0)
      return false;
    XmlAttribute attribute1 = xmlNode.Attributes?["comment"];
    if (attribute1 != null)
      this.Comments = attribute1.Value;
    XmlAttribute attribute2 = xmlNode.Attributes?["enabled"];
    int result;
    if (attribute2 != null && int.TryParse(attribute2.Value, out result))
      this.Enabled = result != 0;
    return true;
  }

  /// <summary>Анализ / корректировка данных настойки</summary>
  /// <returns></returns>
  public virtual bool ValidateData(bool fixMode = true) => true;

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public virtual XmlNode SaveData(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return (XmlNode) null;
    XmlNode element = (XmlNode) xmlDoc.CreateElement(this.GetXmlTypeName());
    if (!string.IsNullOrEmpty(this.Comments))
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("comment");
      attribute.Value = this.Comments;
      element.Attributes?.Append(attribute);
    }
    if (!this.Enabled)
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("enabled");
      attribute.Value = this.Enabled ? "1" : "0";
      element.Attributes?.Append(attribute);
    }
    return element;
  }

  /// <summary>Комментарий</summary>
  [XmlAttribute("comment")]
  public string Comments { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [XmlAttribute("enabled")]
  [DefaultValue(true)]
  public bool Enabled { get; set; } = true;
}
