// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportAttributable
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Diagnostics;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Класс для хранение настроек типов со списком атрибутов
/// </summary>
[Serializable]
public abstract class XmlExchangeExportAttributable : XmlExchangeExportTypedItem
{
  /// <summary>Список атрибутов</summary>
  protected XmlExchangeExportAttrList _attrList = new XmlExchangeExportAttrList();
  /// <summary>Список значений атрибутов по умолчанию</summary>
  protected XmlExchangeExportDefAttrValueList _defAttrList = new XmlExchangeExportDefAttrValueList();

  /// <summary>Конструктор</summary>
  protected XmlExchangeExportAttributable()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId"></param>
  protected XmlExchangeExportAttributable(int typeId)
    : base(typeId)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId"></param>
  /// <param name="typeGuid"></param>
  /// <param name="typeName"></param>
  protected XmlExchangeExportAttributable(int typeId, Guid typeGuid, string typeName)
    : base(typeId, typeGuid, typeName)
  {
  }

  /// <summary>Режим выгрузки (обработки атрибутов)</summary>
  public virtual XmlExportAttrsMode AttrMode { get; set; }

  /// <summary>Список атрибутов</summary>
  public virtual XmlExchangeExportAttrList AttrList
  {
    [DebuggerStepThrough] get => this._attrList;
    [DebuggerStepThrough] set => this._attrList = value;
  }

  /// <summary>Список значений атрибутов по умолчанию</summary>
  public virtual XmlExchangeExportDefAttrValueList DefAttrList
  {
    [DebuggerStepThrough] get => this._defAttrList;
    [DebuggerStepThrough] set => this._defAttrList = value;
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode))
    {
      this.AttrList.Clear();
      this.DefAttrList.Clear();
      return false;
    }
    XmlAttribute attribute = xmlNode.Attributes?["attrmode"];
    int result;
    if (attribute != null && int.TryParse(attribute.Value, out result))
      this.AttrMode = (XmlExportAttrsMode) result;
    foreach (XmlNode childNode in xmlNode.ChildNodes)
    {
      if (childNode != null)
      {
        if (childNode.Name.ToLower() == "default_attributes")
          this.DefAttrList.LoadData(childNode);
        else
          this.AttrList.LoadData(childNode);
      }
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fixMode"></param>
  /// <returns></returns>
  public override bool ValidateData(bool fixMode = true)
  {
    return base.ValidateData(fixMode) && this.DefAttrList.TrueForAll((Predicate<XmlExchangeExportDefAttrValue>) (item => item.ValidateData(fixMode))) && this.AttrList.TrueForAll((Predicate<XmlExchangeExportAttr>) (item => item.ValidateData(fixMode)));
  }

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public override XmlNode SaveData(XmlDocument xmlDoc)
  {
    XmlNode xmlNode = base.SaveData(xmlDoc);
    if (xmlNode != null)
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("attrmode");
      int attrMode = (int) this.AttrMode;
      attribute.Value = attrMode.ToString();
      xmlNode.Attributes?.Append(attribute);
      xmlNode.AppendChild(this.AttrList.SaveData(xmlDoc));
      XmlNode newChild = this.DefAttrList.SaveData(xmlDoc);
      if (newChild != null && newChild.HasChildNodes)
        xmlNode.AppendChild(newChild);
    }
    return xmlNode;
  }
}
