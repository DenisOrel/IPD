// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBTypedObjectID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи сведений об идентификаторах и типах
/// объектов базы данных между различными частями системы. Доступ
/// к передаваемой информации осуществляется через интерфейс
/// IDBTypedObjectID.
/// </summary>
public class DBTypedObjectID : 
  DBObjectID,
  IDBTypedObjectID,
  IDBObjectID,
  IComparable<IDBTypedObjectID>
{
  /// <summary>Тип объекта</summary>
  protected int _objTypeID;
  /// <summary>Номер версии</summary>
  protected long _version;
  /// <summary>
  /// Признак базовой версии объекта (1). В дальнейшем
  /// может содержать дополнительные признаки (битовые флажки)
  /// </summary>
  protected long _baseVersion;
  /// <summary>Узлы информационной системы</summary>
  protected string _siteID;
  /// <summary>
  /// Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)
  /// </summary>
  protected long _modificationID;

  /// <summary>Конструктор</summary>
  /// <param name="objTypeID">Тип объекта</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="owner">Владелец объекта</param>
  /// <param name="version">Номер версии</param>
  /// <param name="baseVersion">Признак базовой версии</param>
  /// <param name="siteID">Узлы информационной системы</param>
  /// <param name="modificationID">Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)</param>
  public DBTypedObjectID(
    int objTypeID,
    long objID,
    long id,
    string caption,
    long owner,
    long version,
    long baseVersion,
    string siteID,
    long modificationID)
    : base(objID, id, caption, owner)
  {
    this._objTypeID = objTypeID;
    this._version = version;
    this._baseVersion = baseVersion;
    this._siteID = siteID;
    this._modificationID = modificationID;
  }

  /// <summary>Конструктор</summary>
  /// <param name="source">Прототип</param>
  public DBTypedObjectID(IDBTypedObjectID source)
    : base((IDBObjectID) source)
  {
    if (source == null)
      return;
    this._objTypeID = source.ObjectType;
    this._version = source.Version;
    this._baseVersion = source.BaseVersion;
    this._siteID = source.SiteID;
    this._modificationID = source.ModificationID;
  }

  /// <summary>Тип объекта</summary>
  public int ObjectType
  {
    [DebuggerStepThrough] get => this._objTypeID;
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this._objID;
  }

  /// <summary>Номер версии</summary>
  public long Version
  {
    [DebuggerStepThrough] get => this._version;
  }

  /// <summary>
  /// Признак базовой версии объекта (1). В дальнейшем
  /// может содержать дополнительные признаки (битовые флажки)
  /// </summary>
  public long BaseVersion
  {
    [DebuggerStepThrough] get => this._baseVersion;
  }

  /// <summary>Узлы информационной системы</summary>
  public string SiteID
  {
    [DebuggerStepThrough] get => this._siteID;
  }

  /// <summary>
  /// Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)
  /// </summary>
  public long ModificationID
  {
    [DebuggerStepThrough] get => this._modificationID;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(IDBTypedObjectID other)
  {
    return other == null ? -1 : this._objID.CompareTo(other.ObjectID);
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Сравнить с указанным объектом</param>
  /// <returns>-1, 0, 1</returns>
  public override int CompareTo(object obj)
  {
    return obj is DBTypedObjectID other ? this.CompareTo((IDBTypedObjectID) other) : base.CompareTo(obj);
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj) == 0;

  /// <summary>Получить 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  [DebuggerStepThrough]
  public override int GetHashCode() => this._objTypeID ^ this._objID.GetHashCode();

  public override string ToString() => this.Caption;
}
