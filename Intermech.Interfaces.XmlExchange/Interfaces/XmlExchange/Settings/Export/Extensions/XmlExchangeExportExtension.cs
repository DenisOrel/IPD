// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.Settings.Export.Extensions.XmlExchangeExportExtension
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange.Settings.Export.Extensions;

/// <summary>Класс для хранения настроек расширений экспорта</summary>
[XmlRoot("extention")]
[Serializable]
public class XmlExchangeExportExtension : XmlExchangeExportItem
{
  /// <summary>Конструктор</summary>
  public XmlExchangeExportExtension()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeGuid"></param>
  /// <param name="typeName"></param>
  public XmlExchangeExportExtension(Guid typeGuid, string typeName)
  {
    this.Guid = typeGuid;
    this.Name = typeName;
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode))
      return false;
    XmlAttribute attribute1 = xmlNode.Attributes?["guid"];
    if (attribute1 != null && GuidHelper.IsGuid(attribute1.Value))
      this.Guid = new Guid(attribute1.Value);
    XmlAttribute attribute2 = xmlNode.Attributes?["name"];
    if (attribute2 != null)
      this.Name = attribute2.Value;
    return true;
  }

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public override XmlNode SaveData(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return (XmlNode) null;
    XmlNode xmlNode = base.SaveData(xmlDoc);
    if (xmlNode?.Attributes == null)
      return (XmlNode) null;
    XmlAttribute attribute1 = xmlNode.Attributes.Count > 0 ? xmlNode.Attributes[0] : (XmlAttribute) null;
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("guid");
    attribute2.Value = this.Guid.ToString();
    xmlNode.Attributes.InsertBefore(attribute2, attribute1);
    XmlAttribute attribute3 = xmlDoc.CreateAttribute("name");
    attribute3.Value = this.Name;
    xmlNode.Attributes.InsertBefore(attribute3, attribute1);
    return xmlNode;
  }

  /// <summary>Гл. ид. расширения</summary>
  public Guid Guid { get; set; }

  /// <summary>Наименование расширения</summary>
  public string Name { get; set; }
}
