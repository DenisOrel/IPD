// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBSelectionID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи сведений об выборках.
/// Доступ к передаваемой информации осуществляется через интерфейс IDBSelectionID.
/// </summary>
public class DBSelectionID : IDBSelectionID
{
  /// <summary>Идентификатор объекта</summary>
  private long _id;
  /// <summary>Идентификатор версии выборки</summary>
  private long _objectID;
  /// <summary>Является ли выборка ручной</summary>
  private bool _handSelection;
  /// <summary>Принадлежность выборки</summary>
  private SelectionType _selectionType;

  /// <summary>Создать экземпляр объекта</summary>
  /// <param name="objectID">Идентификатор версии выборки</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="handSelection">Является ли выборка ручной</param>
  /// <param name="selectionType">Принадлежность выборки</param>
  public DBSelectionID(long objectID, long id, bool handSelection, SelectionType selectionType)
  {
    this._objectID = objectID;
    this._id = id;
    this._handSelection = handSelection;
    this._selectionType = selectionType;
  }

  /// <summary>Идентификатор версии объекта (выборки)</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this._objectID;
  }

  /// <summary>Идентификатор объекта</summary>
  public long ID
  {
    [DebuggerStepThrough] get => this._id;
  }

  /// <summary>Является ли выборка ручной</summary>
  public bool HandSelection
  {
    [DebuggerStepThrough] get => this._handSelection;
  }

  /// <summary>Принадлежность выборки</summary>
  public SelectionType Type
  {
    [DebuggerStepThrough] get => this._selectionType;
  }

  /// <summary>Сравнить два объекта</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is DBSelectionID && this._handSelection == (obj as DBSelectionID)._handSelection;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  public override int GetHashCode() => this._handSelection.GetHashCode() << 1 | 1;
}
