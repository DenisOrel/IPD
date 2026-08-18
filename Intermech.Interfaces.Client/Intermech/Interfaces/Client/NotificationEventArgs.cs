// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NotificationEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Данные для событий службы уведомлений</summary>
[DebuggerDisplay("{_eventName}")]
[Serializable]
public class NotificationEventArgs : EventArgs, IEventArgsItemsCount, IEventArgsOptimizationMode
{
  /// <summary>Имя события обновления</summary>
  private string _eventName;
  /// <summary>
  /// "Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий
  /// </summary>
  private bool _firePrePostEvents;

  /// <summary>Создать событие с указанным именем</summary>
  /// <param name="eventName">Имя события</param>
  public NotificationEventArgs(string eventName) => this._eventName = eventName;

  /// <summary>Создать событие с указанным именем</summary>
  /// <param name="eventName">Имя события</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  public NotificationEventArgs(string eventName, bool firePrePostEvents)
  {
    this._eventName = eventName;
    this._firePrePostEvents = firePrePostEvents;
  }

  /// <summary>Имя события обновления</summary>
  public string EventName
  {
    [DebuggerStepThrough] get => this._eventName;
  }

  /// <summary>
  /// "Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий
  /// </summary>
  public bool FirePrePostEvents
  {
    [DebuggerStepThrough] get => this._firePrePostEvents;
    set => this._firePrePostEvents = value;
  }

  /// <summary>Количество заданий в аргументах</summary>
  public virtual int ItemsCount
  {
    [DebuggerStepThrough] get => 1;
  }

  /// <summary>
  /// Проверить, поддерживается ли указанный режим оптимизации аргументами события и,
  /// в случае необходимости, вернуть максимальный уровень поддерживаемой оптимизации
  /// </summary>
  /// <param name="mode">Запрашиваемый режим оптимизации</param>
  /// <returns>Допустимый режим оптимизации</returns>
  public virtual NotificationServiceMode GetSupportedOptimization(NotificationServiceMode mode)
  {
    return NotificationServiceMode.Default;
  }
}
