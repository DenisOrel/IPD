
// Type: Intermech.Tools.Data.Sync.AttributeSyncTaskArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Базовый класс для аргументов событий задачи по переносу атрибута из одной системы в другую.
/// </summary>
public class AttributeSyncTaskArgs : EventArgs
{
  private readonly AttributeSyncTaskData taskData;

  /// <summary>Создает объект.</summary>
  /// <param name="parameters">Параметры переноса атрибутов из одной системы в другую</param>
  public AttributeSyncTaskArgs(AttributeSyncTaskData taskData)
  {
    this.taskData = taskData != null ? taskData : throw new ArgumentNullException(nameof (taskData));
  }

  /// <summary>
  /// Возвращает параметры переноса атрибутов из одной системы в другую
  /// </summary>
  public AttributeSyncTaskData TaskData => this.taskData;
}
