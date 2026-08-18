// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UINotificationActionEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы для события обработки действия, связанного с уведомлением.
/// </summary>
public class UINotificationActionEventArgs : EventArgs
{
  /// <summary>Создает объект</summary>
  /// <param name="notification">уведомление</param>
  /// <param name="action">выбранное пользователем действие, связанное с уведомлением</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="notification" /> содержит null; параметр <paramref name="action" /> содержит null</exception>
  public UINotificationActionEventArgs(UINotification notification, UINotificationAction action)
  {
    if (notification == null)
      throw new ArgumentNullException(nameof (notification));
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    this.Notification = notification;
    this.Action = action;
  }

  /// <summary>Уведомление</summary>
  public UINotification Notification { get; }

  /// <summary>
  /// Выбранное пользователем действие, связанное с уведомлением
  /// </summary>
  public UINotificationAction Action { get; }

  /// <summary>Признак, что событие было обработано</summary>
  public bool Handled { get; set; }
}
