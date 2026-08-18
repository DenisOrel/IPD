// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackingServerHolder
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces.Server;
using System;

#nullable disable
namespace Intermech.CompositionTracking.Server;

public static class CompositionTrackingServerHolder
{
  internal static IServiceProvider serviceProvider;
  internal static IDBTimedEvents dbTimedEvents;
  internal static IEventLogHelper eventLogHelper;
  internal static CompositionTrackingService trackingService;

  public static IServiceProvider ServiceProvider => CompositionTrackingServerHolder.serviceProvider;

  public static IDBTimedEvents DbTimedEvents => CompositionTrackingServerHolder.dbTimedEvents;

  public static IEventLogHelper EventLogHelper => CompositionTrackingServerHolder.eventLogHelper;

  internal static CompositionTrackingService TrackingService
  {
    get => CompositionTrackingServerHolder.trackingService;
  }
}
