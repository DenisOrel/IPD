// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportAttr
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Interfaces.XmlExchange.Settings.Export.Common;
using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Класс для хранения настроек типа атрибута</summary>
[XmlRoot("attribute")]
[Serializable]
public class XmlExchangeExportAttr : XmlExchangeExportTypedItem
{
  /// <summary>Режим выгрузки</summary>
  private XmlExportAttributeMode _mode;
  /// <summary>Пользовательская ед. изменения</summary>
  protected object _userMeasureCode;
  /// <summary>Пользовательский тип данных</summary>
  protected object _userFldType;

  /// <summary>Конструктор</summary>
  public XmlExchangeExportAttr()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId"></param>
  public XmlExchangeExportAttr(int typeId)
    : base(typeId)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(typeId);
    if (attributeType == null)
      return;
    this.TypeGuid = attributeType.AttributeGuid;
    this.TypeName = attributeType.Name;
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId"></param>
  /// <param name="typeGuid"></param>
  /// <param name="typeName"></param>
  public XmlExchangeExportAttr(int typeId, Guid typeGuid, string typeName)
    : base(typeId, typeGuid, typeName)
  {
  }

  /// <summary>Загрузка данных</summary>
  /// <param name="xmlNode"></param>
  /// <param name="validateMode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode))
      return false;
    XmlAttribute attribute1 = xmlNode.Attributes["user_type"];
    if (attribute1 != null)
      this._userFldType = (object) attribute1.Value;
    XmlAttribute attribute2 = xmlNode.Attributes["user_mc"];
    if (attribute2 != null)
      this._userMeasureCode = (object) attribute2.Value;
    XmlAttribute attribute3 = xmlNode.Attributes["mode"];
    int result;
    if (attribute3 != null && int.TryParse(attribute3.Value, out result))
      this._mode = (XmlExportAttributeMode) result;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  public override bool ValidateData(bool fixMode = true)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.TypeGuid);
    if (fixMode)
    {
      this.TypeID = attributeType != null ? attributeType.AttributeID : 0;
      return base.ValidateData();
    }
    return base.ValidateData(false) && attributeType != null && attributeType.AttributeID == this.TypeID;
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
    if (this._userMeasureCode != null)
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("user_mc");
      attribute.Value = this._userMeasureCode.ToString();
      xmlNode.Attributes.Append(attribute);
    }
    if (this._mode != XmlExportAttributeMode.None)
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("mode");
      int mode = (int) this.Mode;
      attribute.Value = mode.ToString();
      xmlNode.Attributes.Append(attribute);
    }
    return xmlNode;
  }

  /// <summary>Параметры экспорта атрибута</summary>
  public virtual XmlExportAttributeMode Mode
  {
    [DebuggerStepThrough] get => this._mode;
    [DebuggerStepThrough] set => this._mode = value;
  }

  /// <summary>Пользовательская ед. изменения</summary>
  public object UserMeasureCode
  {
    [DebuggerStepThrough] get => this._userMeasureCode;
    [DebuggerStepThrough] set => this._userMeasureCode = value;
  }

  /// <summary>Пользовательский тип данных</summary>
  public object UserFldType
  {
    [DebuggerStepThrough] get => this._userFldType;
    [DebuggerStepThrough] set => this._userFldType = value;
  }
}
