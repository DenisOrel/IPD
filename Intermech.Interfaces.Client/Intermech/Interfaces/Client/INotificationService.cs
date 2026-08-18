// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INotificationService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// INotificationService – это ядро службы уведомлений. Данная служба позволяет рассылать уведомления и получать их.
/// Ссылка на неё доступна в глобальном контейнере сервисов ServicesManager.
/// </summary>
public interface INotificationService
{
  /// <summary>Событие генерируется перед вызовом указанного события</summary>
  event NotificationEventHandler OnBeforeEvent;

  /// <summary>Событие генерируется после вызова указанного события</summary>
  event NotificationEventHandler OnAfterEvent;

  /// <summary>Осуществяет подписку на обработку события обновления.</summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="eventHandler">Делегат обработчика события обновления.</param>
  void Subscribe(string eventName, NotificationEventHandler eventHandler);

  /// <summary>
  /// Осуществляет подписку на обработку любых событий обновления.
  /// </summary>
  /// <param name="eventHandler">Делегат обработчика события обновления.</param>
  void Subscribe(NotificationEventHandler eventHandler);

  /// <summary>Осуществляет отписку от обработки события обновления.</summary>
  /// <param name="eventName">Имя события обновления.</param>
  /// <param name="eventHandler">Делегат обработчика события обновления.</param>
  void Unsubscribe(string eventName, NotificationEventHandler eventHandler);

  /// <summary>
  /// Осуществляет отписку от обработки любых событий обновления.
  /// </summary>
  /// <param name="eventHandler">Делегат обработчика события обновления.</param>
  void Unsubscribe(NotificationEventHandler eventHandler);

  /// <summary>Извещает всех подписчиков о произошедшем событии.</summary>
  /// <param name="sender">Объект, рассылающий событие обновления.</param>
  /// <param name="e">Данные для события обновления.</param>
  void FireEvent(object sender, NotificationEventArgs e);

  /// <summary>
  /// Позволяет узнать, есть ли подписчики на указанное событие.
  /// </summary>
  /// <param name="eventName">Имя события</param>
  /// <returns>true - если подписчики есть</returns>
  bool HasSubscribers(string eventName);
}
