// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NullNotificationQueue
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

public class NullNotificationQueue : INotificationQueue
{
  public void QueueEvent(NotificationEventArgs args)
  {
  }

  public void FlushQueue()
  {
  }

  public IEnumerable<NotificationEventArgs> ToEnumerable()
  {
    return (IEnumerable<NotificationEventArgs>) new List<NotificationEventArgs>(0);
  }
}
