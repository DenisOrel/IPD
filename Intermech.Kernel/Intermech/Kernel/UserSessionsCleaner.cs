// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionsCleaner
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class UserSessionsCleaner : IUserSessionsCleaner
{
  private IEventLogHelper _EventLogHelper;
  private UserSessionCollection _SessionCollection;
  private Timer _Timer;
  private TimeSpan _InactivityInterval;
  private const string SessionsErrorTraceFile = "SessionsError.log";

  public UserSessionsCleaner(IEventLogHelper eventLogHelper)
  {
    this._EventLogHelper = eventLogHelper != null ? eventLogHelper : throw new ArgumentNullException(nameof (eventLogHelper));
    this._SessionCollection = (UserSessionCollection) UserSession.Sessions;
    this._InactivityInterval = ServerConsts.OldSessionsInactivityInterval;
    this._Timer = new Timer(new TimerCallback(this.ClearOldSessionsEventHandler), (object) null, ServerConsts.OldSessionsInactivityInterval, ServerConsts.OldSessionsCheckInterval);
  }

  private void ClearOldSessionsEventHandler(object state)
  {
    try
    {
      this.ClearOldSessions();
    }
    catch (Exception ex)
    {
      this.ReportUnhandledException(ex);
    }
  }

  private void ClearOldSessions()
  {
    List<Guid> sessionGuids = this.CollectOldSessions();
    if (sessionGuids.Count == 0)
      return;
    this.LogoutOldSessions((ICollection<Guid>) sessionGuids);
  }

  private List<Guid> CollectOldSessions()
  {
    IList<KeyValuePair<Guid, UserSession>> guidsAndSessions = this._SessionCollection.GetGuidsAndSessions();
    List<Guid> guidList = new List<Guid>(guidsAndSessions.Count);
    foreach (KeyValuePair<Guid, UserSession> keyValuePair in (IEnumerable<KeyValuePair<Guid, UserSession>>) guidsAndSessions)
    {
      Guid key = keyValuePair.Key;
      ThreadedAccessWrapper threadedAccessWrapper = keyValuePair.Value.GetThreadedAccessWrapper();
      if (this.OldSessionsClosingPolicy(threadedAccessWrapper) && threadedAccessWrapper.TrySetClosingState())
      {
        this._EventLogHelper.AddToTrace($"Устаревшая сессия пользователя '{threadedAccessWrapper.UserName}' помечена для удаления (SessionGuid={threadedAccessWrapper.SessionGUID}).", "SessionsError.log");
        if (threadedAccessWrapper.ParentSession != null)
          guidList.Insert(0, key);
        else
          guidList.Add(key);
      }
    }
    return guidList;
  }

  private void LogoutOldSessions(ICollection<Guid> sessionGuids)
  {
    foreach (Guid sessionGuid in (IEnumerable<Guid>) sessionGuids)
    {
      IUserSession session = this._SessionCollection.GetSession(sessionGuid);
      if (session != null)
      {
        try
        {
          session.Logout("MustCloseByKernelName");
        }
        catch (Exception ex)
        {
          this.ReportUnhandledException(ex);
        }
      }
    }
  }

  private bool OldSessionsClosingPolicy(ThreadedAccessWrapper uSessionWrapper)
  {
    if (uSessionWrapper.IsClosingOrDisposed)
      return true;
    if (uSessionWrapper.IsPermanent || DateTime.UtcNow < uSessionWrapper.LastCallTime + this._InactivityInterval)
      return false;
    if (this.IsLocalSession(uSessionWrapper.Session))
      return true;
    UserSession parentSession = uSessionWrapper.ParentSession;
    return parentSession == null || parentSession != null && parentSession.GetThreadedAccessWrapper().IsClosingOrDisposed;
  }

  private bool IsLocalSession(UserSession uSession)
  {
    return RemotingServices.GetObjectUri((MarshalByRefObject) uSession) == null;
  }

  private void ReportUnhandledException(Exception x)
  {
    this._EventLogHelper.AddToTrace($"Ошибка удаления устаревших сессий: {x.Message}", "SessionsError.log");
    this._EventLogHelper.AddToTrace(Environment.StackTrace, "SessionsError.log");
  }
}
