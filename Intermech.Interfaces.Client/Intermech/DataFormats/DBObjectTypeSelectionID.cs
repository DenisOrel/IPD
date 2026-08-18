// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBObjectTypeSelectionID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи сведений о типах объектов, связанных с выборками.
/// Доступ к передаваемой информации осуществляется через интерфейс IDBObjectTypeSelectionID.
/// </summary>
public class DBObjectTypeSelectionID : DBSelectionID, IDBObjectTypeSelectionID
{
  /// <summary>Идентификатор типа объекта, с которым связана выборка</summary>
  private int _bindedObjectTypeID = -1;

  /// <summary>Создать экземпляр объекта</summary>
  /// <param name="objectID">Идентификатор версии выборки</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="handSelection">Является ли выборка ручной</param>
  /// <param name="selectionType">Принадлежность выборки</param>
  public DBObjectTypeSelectionID(
    long objectID,
    long id,
    bool handSelection,
    SelectionType selectionType)
    : base(objectID, id, handSelection, selectionType)
  {
  }

  /// <summary>Создать экземпляр объекта</summary>
  /// <param name="objectID">Идентификатор версии выборки</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="handSelection">Является ли выборка ручной</param>
  /// <param name="selectionType">Принадлежность выборки</param>
  /// <param name="bindedObjectTypeID">Идентификатор типа объекта, с которым связана выборка</param>
  public DBObjectTypeSelectionID(
    long objectID,
    long id,
    bool handSelection,
    SelectionType selectionType,
    int bindedObjectTypeID)
    : base(objectID, id, handSelection, selectionType)
  {
    this._bindedObjectTypeID = bindedObjectTypeID;
  }

  /// <summary>Идентификатор типа объекта, с которым связана выборка</summary>
  public int BindedObjectTypeID
  {
    [DebuggerStepThrough] get => this._bindedObjectTypeID;
  }
}
