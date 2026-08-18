// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.FireRelationModifiedAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Data;

#nullable disable
namespace Intermech.Tools.DataExchange;

internal sealed class FireRelationModifiedAction(
  IDBRelationRef relationRef,
  UINotificationsBuilder uiNotifications) : FireRelationUINotificationAction(relationRef, uiNotifications)
{
  public override void Perform() => this.UINotifications.AddModifiedRelation(this.RelationRef);
}
