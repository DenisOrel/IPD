// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportDefAttrValue
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Класс для хранение значений атрибутов по умолчанию</summary>
[XmlRoot("attribute_value")]
[Serializable]
public class XmlExchangeExportDefAttrValue : XmlExchangeExportUserItem
{
  /// <summary>Пользовательский тип данных</summary>
  protected object _userFldType;
  /// <summary>Порядковый номер значения атрибута (начинается с 0)</summary>
  protected int _inList;
  /// <summary>Значение атрибута</summary>
  protected object _value;
  /// <summary>Строковая составляющая значения</summary>
  protected object _stringValue;
  /// <summary>Дата/время в формате нейтральной языковой культуры</summary>
  protected object _dateValue;
  /// <summary>Целочисленная составляющая</summary>
  protected object _integerValue;
  /// <summary>Вещественная составляющая</summary>
  protected object _doubleValue;
  /// <summary>Значение атрибута</summary>
  protected object _guid;

  /// <summary>Конструктор</summary>
  public XmlExchangeExportDefAttrValue()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="userId"></param>
  public XmlExchangeExportDefAttrValue(int userId)
    : base(userId)
  {
  }

  /// <summary>Загрузка данных</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode))
      return false;
    XmlAttribute attribute1 = xmlNode.Attributes["user_type"];
    if (attribute1 != null)
      this._userFldType = (object) attribute1.Value;
    XmlAttribute attribute2 = xmlNode.Attributes[Consts.xmlAttrFInList];
    if (attribute2 != null && !int.TryParse(attribute2.Value, out this._inList))
      this._inList = 0;
    XmlAttribute attribute3 = xmlNode.Attributes[Consts.xmlAttrFValue];
    if (attribute3 != null)
      this._value = (object) attribute3.Value;
    XmlAttribute attribute4 = xmlNode.Attributes[Consts.xmlAttrFStringValue];
    if (attribute4 != null)
      this._stringValue = (object) attribute4.Value;
    XmlAttribute attribute5 = xmlNode.Attributes[Consts.xmlAttrFDateValue];
    if (attribute5 != null)
      this._dateValue = (object) attribute5.Value;
    XmlAttribute attribute6 = xmlNode.Attributes[Consts.xmlAttrFIntegerValue];
    if (attribute6 != null)
      this._integerValue = (object) attribute6.Value;
    XmlAttribute attribute7 = xmlNode.Attributes[Consts.xmlAttrFDoubleValue];
    if (attribute7 != null)
      this._doubleValue = (object) attribute7.Value;
    XmlAttribute attribute8 = xmlNode.Attributes[Consts.xmlAttrFGuid];
    if (attribute8 != null)
      this._guid = (object) attribute8.Value;
    return true;
  }

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public override XmlNode SaveData(XmlDocument xmlDoc)
  {
    XmlNode xmlNode = base.SaveData(xmlDoc);
    if (xmlNode == null)
      return (XmlNode) null;
    if (this._userFldType != null)
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("user_type");
      attribute.Value = this._userFldType.ToString();
      xmlNode.Attributes.Append(attribute);
    }
    XmlAttribute attribute1 = xmlDoc.CreateAttribute(Consts.xmlAttrFInList);
    attribute1.Value = this._inList.ToString();
    xmlNode.Attributes.Append(attribute1);
    if (this._value != null)
    {
      XmlAttribute attribute2 = xmlDoc.CreateAttribute(Consts.xmlAttrFValue);
      attribute2.Value = this._value.ToString();
      xmlNode.Attributes.Append(attribute2);
    }
    if (this._stringValue != null)
    {
      XmlAttribute attribute3 = xmlDoc.CreateAttribute(Consts.xmlAttrFStringValue);
      attribute3.Value = this._stringValue.ToString();
      xmlNode.Attributes.Append(attribute3);
    }
    if (this._dateValue != null)
    {
      XmlAttribute attribute4 = xmlDoc.CreateAttribute(Consts.xmlAttrFDateValue);
      attribute4.Value = this._dateValue.ToString();
      xmlNode.Attributes.Append(attribute4);
    }
    if (this._integerValue != null)
    {
      XmlAttribute attribute5 = xmlDoc.CreateAttribute(Consts.xmlAttrFIntegerValue);
      attribute5.Value = this._integerValue.ToString();
      xmlNode.Attributes.Append(attribute5);
    }
    if (this._doubleValue != null)
    {
      XmlAttribute attribute6 = xmlDoc.CreateAttribute(Consts.xmlAttrFDoubleValue);
      attribute6.Value = this._doubleValue.ToString();
      xmlNode.Attributes.Append(attribute6);
    }
    if (this._guid != null)
    {
      XmlAttribute attribute7 = xmlDoc.CreateAttribute(Consts.xmlAttrFGuid);
      attribute7.Value = this._guid.ToString();
      xmlNode.Attributes.Append(attribute7);
    }
    return xmlNode;
  }

  public int ID => this.UserID2Int;

  /// <summary>Пользовательский тип данных</summary>
  public object UserFldType
  {
    [DebuggerStepThrough] get => this._userFldType;
    [DebuggerStepThrough] set => this._userFldType = value;
  }

  /// <summary>Порядковый номер значения атрибута (начинается с 0)</summary>
  public int InList
  {
    [DebuggerStepThrough] get => this._inList;
    [DebuggerStepThrough] set => this._inList = value;
  }

  /// <summary>Значение атрибута</summary>
  public object Value
  {
    [DebuggerStepThrough] get => this._value;
    [DebuggerStepThrough] set => this._value = value;
  }

  /// <summary>Строковая составляющая значения</summary>
  public object StringValue
  {
    [DebuggerStepThrough] get => this._stringValue;
    [DebuggerStepThrough] set => this._stringValue = value;
  }

  /// <summary>Дата/время в формате нейтральной языковой культуры</summary>
  public object DateValue
  {
    [DebuggerStepThrough] get => this._dateValue;
    [DebuggerStepThrough] set => this._dateValue = value;
  }

  /// <summary>Целочисленная составляющая</summary>
  public object IntegerValue
  {
    [DebuggerStepThrough] get => this._integerValue;
    [DebuggerStepThrough] set => this._integerValue = value;
  }

  /// <summary>Вещественная составляющая</summary>
  public object DoubleValue
  {
    [DebuggerStepThrough] get => this._doubleValue;
    [DebuggerStepThrough] set => this._doubleValue = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public object Guid
  {
    [DebuggerStepThrough] get => this._guid;
    [DebuggerStepThrough] set => this._guid = value;
  }
}
