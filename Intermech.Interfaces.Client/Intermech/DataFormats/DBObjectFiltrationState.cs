// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBObjectFiltrationState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System.Diagnostics;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>Класс для получения статуса подбора версии объекта</summary>
public class DBObjectFiltrationState : IDBObjectFiltrationState
{
  /// <summary>
  /// Статус фильтрации текущего объекта по правилу подбора версий
  /// </summary>
  private ObjectFiltrationState _state;

  /// <summary>Создать экземпляр объекта</summary>
  /// <param name="state">Статус фильтрации текущего объекта по правилу подбора версий</param>
  public DBObjectFiltrationState(ObjectFiltrationState state) => this._state = state;

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is DBObjectFiltrationState && this._state == ((DBObjectFiltrationState) obj)._state;
  }

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  [DebuggerStepThrough]
  public override int GetHashCode() => this._state.GetHashCode();

  /// <summary>
  /// Статус фильтрации текущего объекта по правилу подбора версий
  /// </summary>
  public ObjectFiltrationState State
  {
    [DebuggerStepThrough] get => this._state;
  }
}
