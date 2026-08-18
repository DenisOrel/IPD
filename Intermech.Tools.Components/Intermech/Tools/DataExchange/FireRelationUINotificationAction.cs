// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.FireRelationUINotificationAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Data;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.DataExchange;

internal abstract class FireRelationUINotificationAction : FireUINotificationAction
{
  private IDBRelationRef relationRef;

  protected FireRelationUINotificationAction(
    IDBRelationRef relationRef,
    UINotificationsBuilder uiNotifications)
    : base(uiNotifications)
  {
    this.relationRef = relationRef != null ? relationRef : throw new ArgumentNullException(nameof (relationRef));
  }

  protected IDBRelationRef RelationRef
  {
    [DebuggerStepThrough] get => this.relationRef;
  }
}
