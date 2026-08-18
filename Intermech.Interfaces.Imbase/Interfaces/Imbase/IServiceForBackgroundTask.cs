// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IServiceForBackgroundTask
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
public interface IServiceForBackgroundTask
{
  /// <summary>Запуск задачи на выполнение.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="taskName">Наименование задачи</param>
  /// <param name="inputData">Входные данные</param>
  void StartTask(Guid sessionGuid, Guid taskGuid, string taskName, object inputData);

  /// <summary>Остановка выполнения задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  void StopTask(Guid taskGuid);

  /// <summary>Приостановка выполнения задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  void PauseTask(Guid taskGuid);

  /// <summary>Возобновление выполнения задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  void ResumeTask(Guid taskGuid);

  /// <summary>Получение процента выполнения задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="state">Состояние</param>
  /// <param name="text">Наименование</param>
  /// <remarks>На тот случай, если задача составная и наименование меняется в время выполнения основной задачи</remarks>
  /// <returns>Процент завершенности задачи</returns>
  int GetCompleted(Guid taskGuid, out int state, out string text);

  /// <summary>Получение информации о выполнении задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <returns>Информация о выполненной задаче</returns>
  BackgroundTaskResult GetResult(Guid taskGuid);
}
