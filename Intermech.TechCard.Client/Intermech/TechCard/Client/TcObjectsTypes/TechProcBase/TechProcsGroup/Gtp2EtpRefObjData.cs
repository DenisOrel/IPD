// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.Gtp2EtpRefObjData
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>
/// 
/// </summary>
public class Gtp2EtpRefObjData : Gtp2EtpRefData
{
  /// <summary>Composition item</summary>
  protected TechCardUtils.SostavTreeItem _sostavItem;

  /// <summary>Constructor</summary>
  /// <param name="itemInfo">Описание версии объекта / связи</param>
  /// <param name="itemType">Тип данных</param>
  /// <param name="objRefIDs">Список связей с объектами ЕТП / ГТП в зависимости от типа данных</param>
  /// <param name="sostavItem">Информация о элементе состава</param>
  public Gtp2EtpRefObjData(
    TypedInfoItem itemInfo,
    GtpRefDataType itemType,
    Dictionary<TypedInfoItem, TypedInfoItem> objRefIDs,
    TechCardUtils.SostavTreeItem sostavItem)
    : base(itemInfo, itemType, objRefIDs)
  {
    this._sostavItem = sostavItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="refData"></param>
  /// <param name="sostavItem"></param>
  public Gtp2EtpRefObjData(Gtp2EtpRefData refData, TechCardUtils.SostavTreeItem sostavItem)
    : base(refData)
  {
    this._sostavItem = sostavItem;
  }

  /// <summary>Composition item</summary>
  public TechCardUtils.SostavTreeItem SostavItem
  {
    get => this._sostavItem;
    set => this._sostavItem = value;
  }
}
