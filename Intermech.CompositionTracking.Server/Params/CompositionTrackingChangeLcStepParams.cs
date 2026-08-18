// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Params.CompositionTrackingChangeLcStepParams
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.CompositionTracking.Server.Params;

internal class CompositionTrackingChangeLcStepParams : CompositionTrackingParams
{
  public CompositionTrackingChangeLcStepParams(IDBObject dbObject, IDBLifecycleStep nextStep)
    : base(dbObject)
  {
    this.NextStep = nextStep;
  }

  public IDBLifecycleStep NextStep { get; }
}
