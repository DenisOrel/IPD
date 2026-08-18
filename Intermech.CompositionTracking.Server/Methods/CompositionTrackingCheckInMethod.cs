// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Methods.CompositionTrackingCheckInMethod
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Params;
using Intermech.Interfaces;
using Intermech.Interfaces.CompositionTracking;

#nullable disable
namespace Intermech.CompositionTracking.Server.Methods;

internal class CompositionTrackingCheckInMethod : CompositionTrackingBaseMethod
{
  public override CompositionTrackingCommands Command => CompositionTrackingCommands.ctcCheckin;

  internal override bool Execute(
    CompositionTrackingParams trackingParams,
    IDBObject sourceDbObject,
    ref IDBObject targetDbObject)
  {
    if (trackingParams == null || targetDbObject == null || targetDbObject.ObjectID >= 0L || targetDbObject.CheckoutBy != targetDbObject.Session.UserID || targetDbObject.ObjectModifyMode != ObjectModifyModes.Checkout)
      return false;
    targetDbObject.CheckIn();
    return true;
  }
}
