// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.FireUINotificationAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.DataExchange;

internal abstract class FireUINotificationAction : IAction
{
  private UINotificationsBuilder uiNotifications;

  protected FireUINotificationAction(UINotificationsBuilder uiNotifications)
  {
    this.uiNotifications = uiNotifications != null ? uiNotifications : throw new ArgumentNullException(nameof (uiNotifications));
  }

  protected UINotificationsBuilder UINotifications
  {
    [DebuggerStepThrough] get => this.uiNotifications;
  }

  public abstract void Perform();
}
