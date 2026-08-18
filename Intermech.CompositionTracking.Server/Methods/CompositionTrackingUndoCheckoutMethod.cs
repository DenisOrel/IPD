// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Methods.CompositionTrackingUndoCheckoutMethod
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Params;
using Intermech.Interfaces;
using Intermech.Interfaces.CompositionTracking;
using Intermech.Kernel;

#nullable disable
namespace Intermech.CompositionTracking.Server.Methods;

internal class CompositionTrackingUndoCheckoutMethod : CompositionTrackingBaseMethod
{
  public override CompositionTrackingCommands Command
  {
    get => CompositionTrackingCommands.ctcUndoCheckOut;
  }

  internal override bool Validate(CompositionTrackingParams trackingParams)
  {
    return base.Validate(trackingParams) && trackingParams.DbObject is DBObject;
  }

  internal override bool Execute(
    CompositionTrackingParams trackingParams,
    IDBObject sourceDbObject,
    ref IDBObject targetDbObject)
  {
    if (targetDbObject == null || targetDbObject.CheckoutBy != targetDbObject.Session.UserID)
      return false;
    targetDbObject.CancelChanges();
    return true;
  }
}
