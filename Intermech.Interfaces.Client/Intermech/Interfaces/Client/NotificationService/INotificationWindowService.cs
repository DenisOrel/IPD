// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NotificationService.INotificationWindowService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client.NotificationService;

/// <summary>
/// INotificationWindowService - расширение окна навигатора для получения доступа
/// к ТОЛЬКО его службе уведомлений. (Используется для отправки событий в конкретное окно навигатора)
/// Для получения доступа к расширению - приводим окно навигатора к данному интерфейсу.
/// </summary>
public interface INotificationWindowService
{
  /// <summary>
  /// Извещает подписчика/подписчиков о произошедшем событии.
  /// </summary>
  /// <param name="sender">Объект, рассылающий событие обновления.</param>
  /// <param name="e">Данные для события обновления.</param>
  /// <returns></returns>
  bool FireEvent(object sender, NotificationEventArgs e);
}
