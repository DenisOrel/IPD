// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBObjectsFiltrationEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы события, возникающего при изменении настроек фильтрации списков объектов
/// </summary>
[Serializable]
public class DBObjectsFiltrationEventArgs : NotificationEventArgs
{
  /// <summary>
  /// Идентификатор выборки, по которой выполняется фильтрация списков объектов
  /// </summary>
  private long _selectionID = -1;
  /// <summary>
  /// Guid выборки, по которой выполняется фильтрация списков объектов
  /// </summary>
  private Guid _selectionGuid = Guid.Empty;

  /// <summary>Создать аргументы события</summary>
  /// <param name="eventName">Название события</param>
  /// <param name="selectionID">Идентификатор выборки, по которой выполняется фильтрация списков объектов</param>
  /// <param name="selectionGuid">Guid выборки, по которой выполняется фильтрация списков объектов</param>
  public DBObjectsFiltrationEventArgs(string eventName, long selectionID, Guid selectionGuid)
    : base(eventName)
  {
    this._selectionID = selectionID;
    this._selectionGuid = selectionGuid;
  }

  /// <summary>
  /// Идентификатор выборки, по которой выполняется фильтрация списков объектов
  /// </summary>
  public long SelectionID => this._selectionID;

  /// <summary>
  /// Guid выборки, по которой выполняется фильтрация списков объектов
  /// </summary>
  public Guid SelectionGuid => this._selectionGuid;
}
