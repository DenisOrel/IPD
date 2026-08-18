// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.Gtp2EtpRefData
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Compositions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>Gtp to etp link structure</summary>
public class Gtp2EtpRefData
{
  /// <summary>
  /// Item's Info (could be as object's info as relation's info)
  /// </summary>
  protected TypedInfoItem _itemInfo;
  /// <summary>Item's type</summary>
  protected GtpRefDataType _itemType;
  /// <summary>Object's reference data</summary>
  protected Dictionary<TypedInfoItem, TypedInfoItem> _objRefIDs;

  /// <summary>Constructor</summary>
  /// <param name="itemInfo">Описание версии объекта / связи </param>
  /// <param name="itemType">Тип данных</param>
  /// <param name="objRefIDs">Список связей с объектами ЕТП / ГТП в зависимости от типа данных</param>
  public Gtp2EtpRefData(
    TypedInfoItem itemInfo,
    GtpRefDataType itemType,
    Dictionary<TypedInfoItem, TypedInfoItem> objRefIDs)
  {
    this._itemInfo = itemInfo;
    this._itemType = itemType;
    if (objRefIDs != null)
      this._objRefIDs = new Dictionary<TypedInfoItem, TypedInfoItem>((IDictionary<TypedInfoItem, TypedInfoItem>) objRefIDs);
    else
      this._objRefIDs = new Dictionary<TypedInfoItem, TypedInfoItem>();
  }

  /// <summary>Constructor</summary>
  /// <param name="refData"></param>
  public Gtp2EtpRefData(Gtp2EtpRefData refData)
  {
    if (refData != null)
    {
      this._itemInfo = refData.ItemInfo;
      this._itemType = refData.ItemType;
    }
    if (refData != null && refData.ObjRefIDs != null)
      this._objRefIDs = new Dictionary<TypedInfoItem, TypedInfoItem>((IDictionary<TypedInfoItem, TypedInfoItem>) refData.ObjRefIDs);
    else
      this._objRefIDs = new Dictionary<TypedInfoItem, TypedInfoItem>();
  }

  /// <summary>Описание версии объекта / связи</summary>
  public TypedInfoItem ItemInfo
  {
    get => this._itemInfo;
    set => this._itemInfo = value;
  }

  /// <summary>Тип данных</summary>
  public GtpRefDataType ItemType
  {
    get => this._itemType;
    set => this._itemType = value;
  }

  /// <summary>
  /// Список связей с объектами ЕТП / ГТП в зависимости от типа данных
  /// </summary>
  /// <remarks>Описание объекта может быть не задано</remarks>
  public Dictionary<TypedInfoItem, TypedInfoItem> ObjRefIDs
  {
    get => this._objRefIDs;
    set
    {
      this._objRefIDs.Clear();
      if (value == null)
        return;
      this._objRefIDs = new Dictionary<TypedInfoItem, TypedInfoItem>((IDictionary<TypedInfoItem, TypedInfoItem>) value);
    }
  }
}
