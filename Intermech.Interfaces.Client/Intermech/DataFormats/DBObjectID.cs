// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBObjectID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи сведений об идентификаторах объектов
/// базы данных между различными частями системы. Доступ к передаваемой
/// информации осуществляется через интерфейс IDBObjectID.
/// </summary>
public class DBObjectID : IDBObjectID, IComparable, IComparable<IDBObjectID>
{
  /// <summary>Идентификатор версии объекта (F_OBJECT_ID)</summary>
  protected long _objID;
  /// <summary>Идентификатор объекта (F_ID)</summary>
  protected long _id;
  /// <summary>Заголовок объекта</summary>
  protected string _caption;
  /// <summary>Владелец объекта</summary>
  protected long _owner;

  /// <summary>Конструктор</summary>
  /// <param name="objID">Идентификатор версии объекта (F_OBJECT_ID)</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="caption">Заголовок объекта</param>
  /// <param name="owner">Владелец объекта</param>
  public DBObjectID(long objID, long id, string caption, long owner)
  {
    this._objID = objID;
    this._id = id;
    this._caption = caption;
    this._owner = owner;
  }

  /// <summary>Конструктор</summary>
  /// <param name="source"></param>
  public DBObjectID(IDBObjectID source)
  {
    if (source == null)
      return;
    this._objID = source.Value;
    this._id = source.ID;
    this._caption = source.Caption;
    this._owner = source.Owner;
  }

  /// <summary>Идентификатор версии объекта (F_OBJECT_ID)</summary>
  public long Value
  {
    [DebuggerStepThrough] get => this._objID;
  }

  /// <summary>Идентификатор объекта (F_ID)</summary>
  public long ID
  {
    [DebuggerStepThrough] get => this._id;
  }

  /// <summary>Заголовок объекта</summary>
  public string Caption
  {
    [DebuggerStepThrough] get => this._caption;
  }

  /// <summary>Владелец объекта</summary>
  public long Owner
  {
    [DebuggerStepThrough] get => this._owner;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Сравнить с указанным объектом</param>
  /// <returns>-1, 0, 1</returns>
  public virtual int CompareTo(object obj) => this.CompareTo(obj as IDBObjectID);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(IDBObjectID other)
  {
    return other == null ? -1 : this._objID.CompareTo(other.Value);
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj) == 0;

  /// <summary>Получить 32-битный хэш-код объекта</summary>
  /// <returns>32-битный хэш-код объекта</returns>
  [DebuggerStepThrough]
  public override int GetHashCode() => this._objID.GetHashCode();
}
