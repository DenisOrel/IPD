// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportScript
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Класс для хранения скриптов экспорта объектов</summary>
[XmlRoot("script")]
[Serializable]
public class XmlExchangeExportScript : XmlExchangeExportItem
{
  /// <summary>Конструктор</summary>
  public XmlExchangeExportScript()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="scriptName"></param>
  /// <param name="scriptCode"></param>
  public XmlExchangeExportScript(string scriptName, string scriptCode)
  {
    this.ScriptName = scriptName;
    this.ScriptCode = scriptCode;
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode))
      return false;
    XmlAttribute attribute = xmlNode.Attributes["name"];
    if (attribute != null)
      this.ScriptName = attribute.Value;
    this.ScriptCode = xmlNode.InnerText;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public override XmlNode SaveData(XmlDocument xmlDoc)
  {
    XmlNode xmlNode = base.SaveData(xmlDoc);
    if (xmlNode == null)
      return (XmlNode) null;
    XmlCDataSection cdataSection = xmlDoc.CreateCDataSection(this.ScriptCode);
    xmlNode.InnerXml = cdataSection.OuterXml;
    XmlAttribute attribute = xmlDoc.CreateAttribute("name");
    attribute.Value = this.ScriptName;
    xmlNode.Attributes.Append(attribute);
    return xmlNode;
  }

  [XmlAttribute("name")]
  public string ScriptName { get; set; }

  /// <summary>Код сценария (скрипта)</summary>
  public string ScriptCode { get; set; }
}
