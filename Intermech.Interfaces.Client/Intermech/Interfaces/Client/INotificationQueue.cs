// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INotificationQueue
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Позволяет реализовать очередь событий обновления, которая сначала накапливает события, а затем запускает
/// их на выполнение.
/// </summary>
public interface INotificationQueue
{
  /// <summary>Ставит событие в очередь на выполнение.</summary>
  /// <param name="args">Аргументы события</param>
  void QueueEvent(NotificationEventArgs args);

  /// <summary>
  /// Запускает все события на выполнение и очищает очередь событий.
  /// </summary>
  void FlushQueue();

  /// <summary>
  /// Извлекает содержимое очереди событий и возвращает его в виде перечисления. Сама очередь событий при этом очищается.
  /// </summary>
  /// <returns>Перечисление событий</returns>
  IEnumerable<NotificationEventArgs> ToEnumerable();
}
