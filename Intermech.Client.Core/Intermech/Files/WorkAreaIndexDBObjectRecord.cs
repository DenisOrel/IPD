
// Type: Intermech.Files.WorkAreaIndexDBObjectRecord
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Files;

/// <summary>
/// Запись в файле индекса рабочей области об объекте IPS, опубликованном в рабочей области.
/// Реализация является immutable и thread safe.
/// </summary>
internal sealed class WorkAreaIndexDBObjectRecord
{
  /// <summary>Создает объект.</summary>
  /// <param name="objectState">Состояние объекта IPS</param>
  /// <param name="lastUsedTime">Дата и время последнего обращения к объекту IPS в UTC</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="objectState" /> не должен быть равен null</exception>
  public WorkAreaIndexDBObjectRecord(DBObjectState objectState, DateTime lastUsedTime)
  {
    this.ObjectState = objectState;
    this.LastUsedTime = lastUsedTime;
  }

  /// <summary>Возвращает состояние объекта IPS.</summary>
  public DBObjectState ObjectState { get; private set; }

  /// <summary>
  /// Возвращает дату и время последнего обращения к объекту IPS в UTC.
  /// </summary>
  public DateTime LastUsedTime { get; private set; }
}
