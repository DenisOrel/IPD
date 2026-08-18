// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Client.TrackingClientSession
// Assembly: Intermech.CompositionTracking.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23902E52-823F-45F4-A7C9-769D98EE1E49
// Assembly location: D:\IPS\Client\Intermech.CompositionTracking.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.CompositionTracking;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CompositionTracking.Client;

public class TrackingClientSession : IDisposable
{
  private ICompositionTrackingSession _trackingSession;
  private RemoteLock _lock;

  private void RegisterTrackingSession(IUserSession session)
  {
    if (session == null)
      return;
    ICompositionTrackingService service = ServiceUtils.GetService<ICompositionTrackingService>((object) session, false);
    if (service == null)
      return;
    this._trackingSession = service.CreateTrackingSession(session.SessionGUID);
    this._lock.Add((object) this._trackingSession);
  }

  private void UnregisterTrackingSession()
  {
    if (this._trackingSession == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<ICompositionTrackingService>((object) sessionKeeper.Session, false)?.DisposeTrackingSession(this._trackingSession.SessionGuid);
    if (this._lock != null)
      this._lock.Remove((object) this._trackingSession);
    this._trackingSession = (ICompositionTrackingSession) null;
  }

  public TrackingClientSession(IUserSession session)
  {
    this._lock = new RemoteLock();
    this.RegisterTrackingSession(session);
  }

  public void Dispose()
  {
    this.UnregisterTrackingSession();
    if (this._lock == null)
      return;
    this._lock.Dispose();
    this._lock = (RemoteLock) null;
  }

  public List<long> GetTrackingLog(CompositionTrackingCommands command)
  {
    List<long> trackingLog = new List<long>();
    if (this._trackingSession == null)
      return trackingLog;
    this._trackingSession.GetSessionLog()?.TryGetValue(command, out trackingLog);
    return trackingLog;
  }
}
