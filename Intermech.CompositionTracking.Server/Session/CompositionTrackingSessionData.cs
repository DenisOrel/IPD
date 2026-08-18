// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Session.CompositionTrackingSessionData
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CompositionTracking.Server.Session;

internal class CompositionTrackingSessionData : IUserSessionLocalData
{
  public IDictionary<long, IMSLifeCycleStep> BeforeLifeCycleSteps { get; } = (IDictionary<long, IMSLifeCycleStep>) new ConcurrentDictionary<long, IMSLifeCycleStep>();
}
