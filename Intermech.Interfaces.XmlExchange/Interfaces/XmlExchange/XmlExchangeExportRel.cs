// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportRel
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Класс для хранения настроек типа связи</summary>
[XmlRoot("relation_type")]
[Serializable]
public class XmlExchangeExportRel : XmlExchangeExportAttributable
{
  /// <summary>Конструктор</summary>
  public XmlExchangeExportRel()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId"></param>
  public XmlExchangeExportRel(int typeId)
    : base(typeId)
  {
    IMSRelationType relationType = MetaDataHelper.GetRelationType(typeId);
    if (relationType == null)
      return;
    this.TypeGuid = relationType.Guid;
    this.TypeName = relationType.TypeName;
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId"></param>
  /// <param name="typeGuid"></param>
  /// <param name="typeName"></param>
  public XmlExchangeExportRel(int typeId, Guid typeGuid, string typeName)
    : base(typeId, typeGuid, typeName)
  {
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode) => base.LoadData(xmlNode);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fixMode"></param>
  /// <returns></returns>
  public override bool ValidateData(bool fixMode = true)
  {
    IMSRelationType relationType = MetaDataHelper.GetRelationType(this.TypeGuid);
    if (fixMode)
    {
      this.TypeID = relationType != null ? relationType.RelationTypeID : -1;
      return base.ValidateData(true);
    }
    return base.ValidateData(false) && relationType != null && relationType.RelationTypeID == this.TypeID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public override XmlNode SaveData(XmlDocument xmlDoc) => base.SaveData(xmlDoc) ?? (XmlNode) null;
}
