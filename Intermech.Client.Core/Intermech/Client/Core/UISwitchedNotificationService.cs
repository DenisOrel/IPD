
// Type: Intermech.Client.Core.UISwitchedNotificationService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;


namespace Intermech.Client.Core;

/// <summary>
/// Служба уведомлений, "пропускающая" события в зависимости от активности флажка UISettings.AutoupdateNonActiveWindows
/// </summary>
public class UISwitchedNotificationService : NotificationService
{
  /// <summary>
  /// При установке данного значения в True сервис будет пропускать все уведомления независимо от настроек системы
  /// </summary>
  public bool Forced;

  /// <summary>Пришло новое событие</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void InternalFireEvent(object sender, NotificationEventArgs e)
  {
    ICriticalEventArgs criticalEventArgs = e as ICriticalEventArgs;
    if (!this.Forced && !UISettings.AutoupdateNonActiveWindows && !NotificationEventNames.CriticalEventNames.Contains(e.EventName) && (criticalEventArgs == null || !criticalEventArgs.IsCritical))
      return;
    base.InternalFireEvent(sender, e);
  }
}
