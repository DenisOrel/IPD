// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IUINotificationService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса UI уведомлений - показывает BaloonTooltip в системном трее и запоминает
/// их в специальном окне, если юзер на них не отреагировал в течении таймаута.
/// Реализация должна быть thread safe.
/// </summary>
public interface IUINotificationService
{
  /// <summary>
  /// Отображает уведомление в интерфейсе пользователя в специальном окне и во всплывающей подсказке.
  /// </summary>
  /// <param name="notification">Уведомление</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="notification" /> содержит null</exception>
  void ShowNotification(UINotification notification);

  /// <summary>
  /// Событие обработки действия, связанного с уведомлением.
  /// </summary>
  event EventHandler<UINotificationActionEventArgs> NotificationAction;
}
