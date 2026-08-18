
// Type: Intermech.Client.Core.SwitchedNotificationService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System.Diagnostics;


namespace Intermech.Client.Core;

/// <summary>
/// Служба уведомлений для окна "Навигатора". Позволяет управлять рассылкой
/// событий от окна остальным окнам и подписчикам "Навигатора"
/// </summary>
public class SwitchedNotificationService : NotificationService
{
  /// <summary>
  /// Разрешена ли отправка событий другим подписчикам "Навигатора"
  /// </summary>
  protected bool enabled = true;

  /// <summary>
  /// Разрешена ли отправка событий другим подписчикам "Навигатора"
  /// </summary>
  public virtual bool Enabled
  {
    [DebuggerStepThrough] get => this.enabled;
    set => this.enabled = value;
  }

  /// <summary>Пришло новое событие</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void InternalFireEvent(object sender, NotificationEventArgs e)
  {
    ICriticalEventArgs criticalEventArgs = e as ICriticalEventArgs;
    if (!this.Enabled && !NotificationEventNames.CriticalEventNames.Contains(e.EventName) && (criticalEventArgs == null || !criticalEventArgs.IsCritical))
      return;
    base.InternalFireEvent(sender, e);
  }
}
