// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportObjList
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Настройки выгрузки типов объектов</summary>
[XmlRoot("object_types")]
[Serializable]
public class XmlExchangeExportObjList : XmlExchangeExportTypedItemList<XmlExchangeExportObj>
{
  /// <summary>Поиск элемента по ид-ру</summary>
  /// <param name="typeId"></param>
  /// <param name="checkParent"></param>
  /// <returns></returns>
  public XmlExchangeExportObj GetItemByID(int typeId, bool checkParent)
  {
    XmlExchangeExportObj itemById = base.GetItemByID(typeId);
    if (!checkParent || itemById != null)
      return itemById;
    foreach (int itemId in MetaDataHelper.GetObjectTypeParentsID(typeId))
    {
      itemById = base.GetItemByID(itemId);
      if (itemById != null)
        break;
    }
    return itemById;
  }

  /// <summary>Поиск элемента по Guid</summary>
  /// <param name="typeGuid"></param>
  /// <param name="checkParent"></param>
  /// <returns></returns>
  public XmlExchangeExportObj GetItemByGuid(Guid typeGuid, bool checkParent)
  {
    XmlExchangeExportObj itemByGuid = base.GetItemByGuid(typeGuid);
    if (!checkParent || itemByGuid != null)
      return itemByGuid;
    foreach (Guid typeGuid1 in MetaDataHelper.GetObjectTypeParentsGuid(typeGuid))
    {
      itemByGuid = base.GetItemByGuid(typeGuid1);
      if (itemByGuid != null)
        break;
    }
    return itemByGuid;
  }

  /// <summary>Поиск элемента по ид-ру</summary>
  /// <param name="typeId"></param>
  /// <returns></returns>
  public override XmlExchangeExportObj GetItemByID(int typeId) => this.GetItemByID(typeId, true);

  /// <summary>Поиск элемента по Guid</summary>
  /// <param name="typeGuid"></param>
  /// <returns></returns>
  public override XmlExchangeExportObj GetItemByGuid(Guid typeGuid)
  {
    return this.GetItemByGuid(typeGuid, true);
  }
}
