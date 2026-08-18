// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Methods.CompositionTrackingBaseMethod
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Params;
using Intermech.Interfaces;
using Intermech.Interfaces.CompositionTracking;
using System;

#nullable disable
namespace Intermech.CompositionTracking.Server.Methods;

internal abstract class CompositionTrackingBaseMethod
{
  public abstract CompositionTrackingCommands Command { get; }

  internal virtual bool Validate(CompositionTrackingParams trackingParams)
  {
    return trackingParams != null && trackingParams.DbObject != null;
  }

  internal virtual IDBObject GetTargetObject(CompositionTrackingParams trackingParams)
  {
    return trackingParams != null ? trackingParams.DbObject : throw new ArgumentNullException(nameof (trackingParams));
  }

  internal abstract bool Execute(
    CompositionTrackingParams trackingParams,
    IDBObject sourceDbObject,
    ref IDBObject targetDbObject);
}
