// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportObj
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Настройки выгрузки типа объекта</summary>
[XmlRoot("object_type")]
[Serializable]
public class XmlExchangeExportObj : XmlExchangeExportAttributable
{
  /// <summary>Конструктор</summary>
  public XmlExchangeExportObj()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId">Ид. типа объекта</param>
  public XmlExchangeExportObj(int typeId)
    : base(typeId)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(typeId);
    if (objectType == null)
      return;
    this.TypeGuid = objectType.Guid;
    this.TypeName = objectType.ObjectTypeName;
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId">Ид. типа объекта</param>
  /// <param name="typeGuid"></param>
  /// <param name="typeName"></param>
  public XmlExchangeExportObj(int typeId, Guid typeGuid, string typeName)
    : base(typeId, typeGuid, typeName)
  {
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode))
      return false;
    XmlAttribute attribute = xmlNode.Attributes["objmodes"];
    int result;
    if (attribute != null && int.TryParse(attribute.Value, out result))
      this.ObjModes = (XmlExportObjModes) result;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fixMode"></param>
  /// <returns></returns>
  public override bool ValidateData(bool fixMode = true)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(this.TypeGuid);
    if (fixMode)
    {
      if (objectType != null)
        this.TypeID = objectType.ObjectTypeID;
      else if (this.TypeGuid != Guid.Empty)
        this.TypeID = -1;
      return base.ValidateData(true);
    }
    if (!base.ValidateData(false))
      return false;
    if (this.TypeGuid == Guid.Empty)
      return true;
    return objectType != null && objectType.ObjectTypeID == this.TypeID;
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
    XmlAttribute attribute = xmlDoc.CreateAttribute("objmodes");
    int objModes = (int) this.ObjModes;
    attribute.Value = objModes.ToString();
    xmlNode.Attributes?.Append(attribute);
    return xmlNode;
  }

  /// <summary>Параметры экспорта объектов</summary>
  public virtual XmlExportObjModes ObjModes { get; set; }
}
