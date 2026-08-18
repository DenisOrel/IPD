// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IMCadSettings
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>Класс настроек интеграции с CAD-системой</summary>
internal class IMCadSettings : IIMCadSettings
{
  /// <summary>
  ///  Настройка соответствий атрибутов IPS - параметров CAD-системы
  /// </summary>
  private readonly IIMCadAttrTypeSettings _attrTypeSettings;

  /// <summary>Конструктор</summary>
  public IMCadSettings()
  {
    this._attrTypeSettings = (IIMCadAttrTypeSettings) new IMCadAttrTypeSettings();
  }

  /// <summary>
  ///  Настройка соответствий атрибутов IPS - параметров CAD-системы
  /// </summary>
  public IIMCadAttrTypeSettings AttrTypeSettings => this._attrTypeSettings;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  internal XmlDocument SaveToXml()
  {
    XmlDocument xmlDoc = new XmlDocument();
    XmlNode element = (XmlNode) xmlDoc.CreateElement(nameof (IMCadSettings));
    element.AppendChild((this._attrTypeSettings as IMCadAttrTypeSettings).SaveToXml(xmlDoc));
    xmlDoc.AppendChild(element);
    return xmlDoc;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlDoc"></param>
  internal void LoadFromXml(XmlDocument xmlDoc)
  {
    XmlNode xmlNode = xmlDoc != null ? xmlDoc.FirstChild : throw new ArgumentNullException(nameof (xmlDoc));
    if (xmlNode == null || !xmlNode.Name.Equals(nameof (IMCadSettings)) || !xmlNode.ChildNodes.Count.Equals(1))
      return;
    (this._attrTypeSettings as IMCadAttrTypeSettings).LoadFromXml(xmlNode.ChildNodes[0]);
  }

  /// <summary>
  /// 
  /// </summary>
  internal void LoadDefaultSettings()
  {
    (this._attrTypeSettings as IMCadAttrTypeSettings).LoadDefaultSettings();
  }
}
