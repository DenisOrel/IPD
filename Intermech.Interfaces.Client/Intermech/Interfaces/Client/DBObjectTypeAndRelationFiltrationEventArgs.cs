// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBObjectTypeAndRelationFiltrationEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы события, возникающего при изменении настроек фильтрации составов по типам объектов и связей
/// </summary>
[Serializable]
public class DBObjectTypeAndRelationFiltrationEventArgs : NotificationEventArgs
{
  /// <summary>
  /// Идентификатор активного фильтра. null - фильтрация отключена
  /// </summary>
  private Guid _activeFilterGuid = Guid.Empty;

  /// <summary>Создать аргументы события</summary>
  /// <param name="eventName">Название события</param>
  /// <param name="activeFilterGuid">Идентификатор активного фильтра. null - фильтрация отключена</param>
  public DBObjectTypeAndRelationFiltrationEventArgs(string eventName, Guid activeFilterGuid)
    : base(eventName)
  {
    this._activeFilterGuid = activeFilterGuid;
  }

  /// <summary>
  /// Идентификатор активного фильтра. null - фильтрация отключена
  /// </summary>
  public Guid ActiveFilterGuid => this._activeFilterGuid;
}
