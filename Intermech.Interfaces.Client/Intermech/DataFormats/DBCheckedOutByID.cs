// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBCheckedOutByID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи сведений об идентификаторах связей
/// между различными частями системы. Доступ к передаваемой
/// информации осуществляется через интерфейс IDBCheckedOutID.
/// </summary>
[DebuggerDisplay("ObjectID = {_objectID}; CheckedOutBy = {_checkedOutBy}")]
public class DBCheckedOutByID : IDBCheckedOutByID
{
  /// <summary>Идентификатор версии объекта</summary>
  private long _objectID;
  /// <summary>
  /// Идентификатор пользователя, взявшего объект на изменение
  /// </summary>
  private long _checkedOutBy;
  /// <summary>Владелец объекта</summary>
  private long _owner;

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this._objectID;
  }

  /// <summary>
  /// Идентификатор пользователя, взявшего объект на изменение
  /// </summary>
  public long CheckedOutBy
  {
    [DebuggerStepThrough] get => this._checkedOutBy;
  }

  /// <summary>Владелец объекта</summary>
  public long Owner
  {
    [DebuggerStepThrough] get => this._owner;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="checkedOutBy">Идентификатор пользователя, взявшего объект на изменение</param>
  /// <param name="owner">Владелец объекта</param>
  public DBCheckedOutByID(long objectID, long checkedOutBy, long owner)
  {
    this._objectID = objectID;
    this._checkedOutBy = checkedOutBy;
    this._owner = owner;
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is DBCheckedOutByID dbCheckedOutById))
      return base.Equals(obj);
    return this._objectID == dbCheckedOutById._objectID && this._checkedOutBy == dbCheckedOutById._checkedOutBy;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  [DebuggerStepThrough]
  public override int GetHashCode()
  {
    return this._checkedOutBy.GetHashCode() << 24 ^ this._objectID.GetHashCode();
  }
}
