// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.FireObjectUINotificationAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Data;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.DataExchange;

internal abstract class FireObjectUINotificationAction : FireUINotificationAction
{
  private IDBObjectRef objectRef;

  protected FireObjectUINotificationAction(
    IDBObjectRef objectRef,
    UINotificationsBuilder uiNotifications)
    : base(uiNotifications)
  {
    this.objectRef = objectRef != null ? objectRef : throw new ArgumentNullException(nameof (objectRef));
  }

  protected IDBObjectRef ObjectRef
  {
    [DebuggerStepThrough] get => this.objectRef;
  }
}
