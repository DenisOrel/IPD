// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CustomTechClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Базовый класс для технологических объектов</summary>
public abstract class CustomTechClass
{
  /// <summary>Идентификатор версии объекта</summary>
  protected long _objID;
  /// <summary>Ид. версии связи с родительским объектом</summary>
  private long _linkId;
  /// <summary>Идентификатор сортировки</summary>
  protected long _orderID;
  /// <summary>Признак изменения параметров объекта</summary>
  private bool _modified;

  /// <summary>Initialize class data</summary>
  private void InitData()
  {
  }

  /// <summary>Get object id</summary>
  /// <returns></returns>
  private long GetObjectId() => this._objID;

  /// <summary>Set object id</summary>
  /// <param name="objId"></param>
  private void SetObjectId(long objId)
  {
    if (this._objID == objId)
      return;
    this._objID = objId;
    this.Modified = true;
  }

  /// <summary>Get link id</summary>
  /// <returns></returns>
  private long GetLinkId() => this._linkId;

  /// <summary>Set link id</summary>
  /// <param name="linkId"></param>
  private void SetLinkId(long linkId)
  {
    if (linkId == this._linkId)
      return;
    this._linkId = linkId;
    this.Modified = true;
  }

  /// <summary>Get order ID</summary>
  /// <returns></returns>
  private long GetOrderId() => this._orderID;

  /// <summary>Set order ID</summary>
  /// <param name="orderId"></param>
  private void SetOrderId(long orderId)
  {
    if (this._orderID == orderId)
      return;
    this._orderID = orderId;
    this.Modified = true;
  }

  /// <summary>Конструктор</summary>
  protected CustomTechClass()
    : this(0L)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Ид. версии объекта</param>
  /// <param name="linkId">Ид. версии связи с родительским объектом</param>
  protected CustomTechClass(long objectId, long linkId = 0)
  {
    this.ObjectId = objectId;
    this.LinkID = linkId;
    this.InitData();
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectId
  {
    get => this.GetObjectId();
    set => this.SetObjectId(value);
  }

  [Obsolete("Use ObjectId instead")]
  public long ObjID
  {
    get => this.ObjectId;
    set => this.ObjectId = value;
  }

  /// <summary>Идентификатор связи с родительским объектом</summary>
  public long LinkID
  {
    get => this.GetLinkId();
    set => this.SetLinkId(value);
  }

  /// <summary>Значение атрибута сортировки</summary>
  public virtual long OrderID
  {
    get => this.GetOrderId();
    set => this.SetOrderId(value);
  }

  /// <summary>Признак изменения параметров объекта</summary>
  public bool Modified
  {
    get => this._modified;
    set => this._modified = value;
  }

  /// <summary>Очистить объект</summary>
  public virtual void Clear()
  {
    this.OrderID = 0L;
    this.Modified = false;
  }

  /// <summary>Загрузить данные из базы</summary>
  public virtual void LoadData(IUserSession session)
  {
  }

  /// <summary>Сохранить данные в базу</summary>
  public virtual void SaveData(IUserSession session)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return (!(obj is CustomTechClass customTechClass) || (this._objID != customTechClass._objID || this._linkId != customTechClass._linkId || this._orderID != customTechClass._orderID ? 0 : (this._modified == customTechClass._modified ? 1 : 0)) != 0) && this == obj;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    long num = this.ObjectId;
    int hashCode1 = num.GetHashCode();
    num = this.LinkID;
    int hashCode2 = num.GetHashCode();
    return hashCode1 ^ hashCode2;
  }
}
