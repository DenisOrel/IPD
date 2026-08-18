// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.ObjectCommandEvents
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Commands;

public static class ObjectCommandEvents
{
  private static readonly ObjectCommandEventSite checkoutSite = new ObjectCommandEventSite();
  private static readonly ObjectCommandEventSite saveChangesSite = new ObjectCommandEventSite();
  private static readonly ObjectCommandEventSite checkinSite = new ObjectCommandEventSite();
  private static readonly ObjectCommandEventSite cancelChangesSite = new ObjectCommandEventSite();

  public static ObjectCommandEventSite Checkout
  {
    [DebuggerStepThrough] get => ObjectCommandEvents.checkoutSite;
  }

  public static ObjectCommandEventSite SaveChanges
  {
    [DebuggerStepThrough] get => ObjectCommandEvents.saveChangesSite;
  }

  public static ObjectCommandEventSite Checkin
  {
    [DebuggerStepThrough] get => ObjectCommandEvents.checkinSite;
  }

  public static ObjectCommandEventSite CancelChanges
  {
    [DebuggerStepThrough] get => ObjectCommandEvents.cancelChangesSite;
  }
}
