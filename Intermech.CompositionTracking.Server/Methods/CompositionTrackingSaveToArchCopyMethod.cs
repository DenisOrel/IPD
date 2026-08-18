// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Methods.CompositionTrackingSaveToArchCopyMethod
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Params;
using Intermech.Interfaces;
using Intermech.Interfaces.CompositionTracking;
using System;

#nullable disable
namespace Intermech.CompositionTracking.Server.Methods;

internal class CompositionTrackingSaveToArchCopyMethod : CompositionTrackingBaseMethod
{
  public override CompositionTrackingCommands Command => CompositionTrackingCommands.ctcCheckin;

  internal override bool Validate(CompositionTrackingParams trackingParams)
  {
    return base.Validate(trackingParams);
  }

  internal override IDBObject GetTargetObject(CompositionTrackingParams trackingParams)
  {
    if (trackingParams == null)
      throw new ArgumentNullException(nameof (trackingParams));
    if (trackingParams.Session == null)
      throw new NullReferenceException("trackingParams.Session is Null");
    return trackingParams.Session.GetObjectActualCopy(trackingParams.DbObject.ObjectID, false);
  }

  internal override bool Execute(
    CompositionTrackingParams trackingParams,
    IDBObject sourceDbObject,
    ref IDBObject targetDbObject)
  {
    if (targetDbObject == null || targetDbObject.CheckoutBy != targetDbObject.Session.UserID || targetDbObject.ObjectModifyMode != ObjectModifyModes.Checkout)
      return false;
    targetDbObject.SaveToArcCopy();
    return true;
  }
}
