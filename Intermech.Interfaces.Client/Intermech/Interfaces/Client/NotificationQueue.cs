// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NotificationQueue
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует очередь событий обновления, которая позволяет сначала накапливать события, а затем запустить
/// их на выполнение.
/// </summary>
public class NotificationQueue : INotificationQueue
{
  private readonly Dictionary<string, LinkedList<NotificationEventArgs>> queue;

  /// <summary>Создает объект.</summary>
  public NotificationQueue()
  {
    this.queue = new Dictionary<string, LinkedList<NotificationEventArgs>>();
  }

  /// <summary>Ставит событие в очередь на выполнение.</summary>
  /// <param name="args">Аргументы события</param>
  public void QueueEvent(NotificationEventArgs args)
  {
    LinkedList<NotificationEventArgs> linkedList1;
    if (this.queue.TryGetValue(args.EventName, out linkedList1))
    {
      for (LinkedListNode<NotificationEventArgs> linkedListNode = linkedList1.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        if (linkedListNode.Value is IDataMergingSupport dataMergingSupport && dataMergingSupport.MergeWith((object) args))
          return;
      }
      linkedList1.AddFirst(args);
    }
    else
    {
      LinkedList<NotificationEventArgs> linkedList2 = new LinkedList<NotificationEventArgs>();
      linkedList2.AddFirst(args);
      this.queue.Add(args.EventName, linkedList2);
    }
  }

  /// <summary>
  /// Запускает все события на выполнение и очищает очередь событий.
  /// </summary>
  public void FlushQueue()
  {
    try
    {
      INotificationService notificationService = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
      if (notificationService == null)
        return;
      IInvokeService service = ServiceUtils.GetService<IInvokeService>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        service.InvokeAction(-1, (Action) (() =>
        {
          foreach (NotificationEventArgs e in this.ToEnumerable())
            notificationService.FireEvent((object) null, e);
        }));
      }
      else
      {
        foreach (NotificationEventArgs e in this.ToEnumerable())
          notificationService.FireEvent((object) null, e);
      }
    }
    finally
    {
      this.queue.Clear();
    }
  }

  /// <summary>
  /// Извлекает содержимое очереди событий и возвращает его в виде перечисления. Сама очередь событий при этом очищается.
  /// </summary>
  /// <returns>Перечисление событий</returns>
  public IEnumerable<NotificationEventArgs> ToEnumerable()
  {
    List<NotificationEventArgs> enumerable = new List<NotificationEventArgs>(this.queue.Count);
    foreach (KeyValuePair<string, LinkedList<NotificationEventArgs>> keyValuePair in this.queue)
      enumerable.AddRange((IEnumerable<NotificationEventArgs>) keyValuePair.Value);
    return (IEnumerable<NotificationEventArgs>) enumerable;
  }
}
