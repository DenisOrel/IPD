// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ObjChangedList
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Список измененных атрибутов объекта, а также всех исходящих из него связей
/// (изменение которых может приводить к изменению содержимого объекта)
/// Полный список изменений в задаче ЭС - это List из ObjChangedList
/// </summary>
[Serializable]
public class ObjChangedList : List<AttrChange>
{
  private long _objVerId;
  private long _checkoutBy;
  private ObjectModifyModes _modifyMode;
  private List<RelChangedList> _changedRels;

  /// <summary>
  /// Идентификатор версии объекта (может быть и отрицательным)
  /// </summary>
  public long ObjVerId
  {
    get => this._objVerId;
    set => this._objVerId = value;
  }

  /// <summary>
  /// Идентификатор пользователя, взявшего эту версию объекта на редактирование
  /// </summary>
  public long CheckoutBy
  {
    get => this._checkoutBy;
    set => this._checkoutBy = value;
  }

  /// <summary>Способ модификации этой версии объекта</summary>
  public ObjectModifyModes ModifyMode
  {
    get => this._modifyMode;
    set => this._modifyMode = value;
  }

  /// <summary>
  /// Список измененных атрибутов для связей, исходящих из этой версии. Только для чтения!
  /// </summary>
  public List<RelChangedList> ChangedRels => this._changedRels;

  /// <summary>Общий конструктор, если все параметры уже известны</summary>
  /// <param name="objVerId">ИД версии объекта</param>
  /// <param name="checkoutBy">ИД пользователя, взявшего объект на изменение</param>
  /// <param name="modifyMode">Способ модификации объекта</param>
  /// <param name="changedRels">Список измененных атрибутов связей (опциональный)</param>
  public ObjChangedList(
    long objVerId,
    long checkoutBy,
    ObjectModifyModes modifyMode,
    List<RelChangedList> changedRels = null)
  {
    this._objVerId = objVerId;
    this._checkoutBy = checkoutBy;
    this._modifyMode = modifyMode;
    if (changedRels == null || changedRels.Count <= 0)
      return;
    this._changedRels = changedRels;
  }

  /// <summary>Конструктор из пользовательской сессии</summary>
  /// <param name="objVerId">ИД версии объекта</param>
  /// <param name="ius">пользовательская сессия</param>
  /// <param name="changedRels">Список измененных атрибутов связей (опциональный)</param>
  public ObjChangedList(long objVerId, IUserSession ius, List<RelChangedList> changedRels = null)
  {
    this._objVerId = objVerId;
    IDBObject dbObject = ius.GetObject(objVerId, true);
    this._checkoutBy = dbObject.CheckoutBy;
    this._modifyMode = dbObject.ObjectModifyMode;
    if (changedRels == null || changedRels.Count <= 0)
      return;
    this._changedRels = changedRels;
  }

  public void InitChangedRels()
  {
    if (this._changedRels != null)
      return;
    this._changedRels = new List<RelChangedList>();
  }
}
