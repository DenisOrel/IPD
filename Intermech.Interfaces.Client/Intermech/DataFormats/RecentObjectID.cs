// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.RecentObjectID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Класс, позволяющий получить информацию о недавнем объекте
/// </summary>
public class RecentObjectID : IRecentObjectID, IComparable, IComparable<RecentObjectID>
{
  /// <summary>ID версии объекта</summary>
  protected long _objectID;
  /// <summary>Действие, выполненное над объектом</summary>
  protected ObjectAction _action;
  /// <summary>Дата и время (UTC) выполнения этого действия</summary>
  protected DateTime _date;

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID => this._objectID;

  /// <summary>Действие, выполненное над объектом</summary>
  public ObjectAction Action => this._action;

  /// <summary>Дата и время (UTC) выполнения этого действия</summary>
  public DateTime Date => this._date;

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0 или 1</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as RecentObjectID);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0 или 1</returns>
  public int CompareTo(RecentObjectID other)
  {
    return other == null ? 1 : this.ObjectID.CompareTo(other.ObjectID);
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="action">Действие, выполненное над объектом</param>
  /// <param name="date">Дата и время (UTC) выполнения этого действия</param>
  public RecentObjectID(long objectID, ObjectAction action, DateTime date)
  {
    this._objectID = objectID;
    this._action = action;
    this._date = date;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj) == 0;

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode() => this._objectID.GetHashCode();
}
