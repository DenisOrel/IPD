// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackingSession
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Methods;
using Intermech.CompositionTracking.Server.Params;
using Intermech.Interfaces.CompositionTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CompositionTracking.Server;

internal class CompositionTrackingSession : 
  MarshalByRefObject,
  ICompositionTrackingSession,
  IDisposable
{
  private readonly Guid _sessionGuid;
  private ConcurrentDictionary<CompositionTrackingBaseMethod, CompositionTrackingSessionTask> _taskCache;

  private void DoExecute(
    CompositionTrackingBaseMethod method,
    CompositionTrackingParams trackingParams)
  {
    CompositionTrackingSessionTask trackingSessionTask;
    if (!this._taskCache.TryGetValue(method, out trackingSessionTask))
    {
      trackingSessionTask = new CompositionTrackingSessionTask(this, method)
      {
        Params = trackingParams
      };
      this._taskCache.TryAdd(method, trackingSessionTask);
    }
    CompositionTrackingParams compositionTrackingParams = trackingSessionTask.Params;
    try
    {
      trackingSessionTask.Params = trackingParams;
      trackingSessionTask.Execute();
    }
    finally
    {
      trackingSessionTask.Params = compositionTrackingParams;
    }
  }

  public CompositionTrackingSession(Guid sessionGuid)
  {
    this._sessionGuid = sessionGuid;
    this._taskCache = new ConcurrentDictionary<CompositionTrackingBaseMethod, CompositionTrackingSessionTask>();
  }

  public Guid SessionGuid => this._sessionGuid;

  public Dictionary<CompositionTrackingCommands, List<long>> GetSessionLog()
  {
    Dictionary<CompositionTrackingCommands, List<long>> sessionLog = new Dictionary<CompositionTrackingCommands, List<long>>(this._taskCache.Count);
    foreach (CompositionTrackingSessionTask trackingSessionTask in (IEnumerable<CompositionTrackingSessionTask>) this._taskCache.Values)
      sessionLog[trackingSessionTask.Method.Command] = trackingSessionTask.GetCommandLog();
    return sessionLog;
  }

  public void Dispose()
  {
    foreach (CompositionTrackingSessionTask trackingSessionTask in (IEnumerable<CompositionTrackingSessionTask>) this._taskCache.Values)
      trackingSessionTask.Dispose();
    this._taskCache.Clear();
    this._taskCache = (ConcurrentDictionary<CompositionTrackingBaseMethod, CompositionTrackingSessionTask>) null;
  }

  internal void Execute(
    CompositionTrackingBaseMethod method,
    CompositionTrackingParams trackingParams)
  {
    if (method == null)
      throw new ArgumentNullException(nameof (method));
    bool objectsCacheStarted = ((trackingParams != null ? trackingParams.Session : throw new ArgumentNullException(nameof (trackingParams))) ?? throw new NullReferenceException("trackingParams.Session")).DBObjectsCacheStarted;
    try
    {
      int num = objectsCacheStarted ? 1 : 0;
      this.DoExecute(method, trackingParams);
    }
    finally
    {
      int num = objectsCacheStarted ? 1 : 0;
    }
  }
}
