// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBCustomManualScheduledService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Runtime;
using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Kernel;

public abstract class DBCustomManualScheduledService : 
  DBTimedService,
  IDBManualScheduledService,
  IDBTimedService
{
  private UserSession _session;
  private string _sessionName;
  private int _eventInProcess;
  private const int PROCESS_NOT_STARTED = 0;
  private const int PROCESS_STARTED = 1;

  public DBCustomManualScheduledService()
  {
  }

  internal DBCustomManualScheduledService(UserSession session, string sessionName)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (!session.IsPermanent)
      throw new ArgumentException("Пользовательская сессия должна быть долгоживущей (IsPermanent = true).", nameof (session));
    if (sessionName == null)
      throw new ArgumentNullException(nameof (sessionName));
    this.SetSession(session, sessionName);
  }

  protected internal UserSession Session
  {
    [DebuggerStepThrough] get => this._session;
  }

  protected virtual void SaveLog(string logFileName, string[] log)
  {
    if (log == null)
      return;
    for (int index = 0; index < log.Length; ++index)
      this.Session.EventLogHelper.AddToTrace(log[index], Consts.traceAlways, logFileName);
  }

  protected virtual void Initialize()
  {
  }

  protected virtual void ReleaseResources()
  {
  }

  internal void InitializeInternal()
  {
    if (this._session == null)
    {
      string fullName = this.GetType().FullName;
      this.SetSession((UserSession) this.TimedEventService.GetSystemSessionPermanentClone(fullName), fullName);
    }
    this.Initialize();
  }

  internal void ReleaseInternal()
  {
    this.ReleaseResources();
    this.ReleaseSession();
  }

  private void SetSession(UserSession session, string sessionName)
  {
    this._session = session;
    this._sessionName = sessionName;
  }

  private void ReleaseSession()
  {
    if (this._session == null)
      return;
    try
    {
      this._session.Logout(this._sessionName);
    }
    catch (Exception ex)
    {
      string currentMethodName = this.GetCurrentMethodName(nameof (ReleaseSession));
      SuppressedExceptions.TraceException(ex, currentMethodName);
    }
    finally
    {
      this._session = (UserSession) null;
      this._sessionName = (string) null;
    }
  }

  public virtual bool IsMultiThread => true;

  public void ProcessEventInThread(object obj)
  {
    try
    {
      if (Interlocked.CompareExchange(ref this._eventInProcess, 1, 0) != 0)
        return;
      int key = 0;
      try
      {
        TimedEventProperties properties = (TimedEventProperties) obj;
        try
        {
          key = properties.KeyID;
          (this._TimedEventService as DBTimedEvents).inProgressDict.TryAdd(key, true);
          this.ProcessEvent(properties);
          if (properties.EventKind != TimedEventKinds.Once)
            return;
          lock (this._session)
            this._TimedEventService.DeleteEventID(properties.KeyID, this._session.DataManager);
        }
        catch (Exception ex)
        {
          this._TimedEventService.AddToTrace($"Ошибка при обработке события службой {this.ServiceName}: {ex.Message}", true);
          this._TimedEventService.AddToTrace(ex.StackTrace, true);
          lock (this._session)
          {
            string str = ex.Message;
            if (str.Length > Consts.DefaultStringDbFieldLength)
              str = str.Substring(0, Consts.DefaultStringDbFieldLength);
            if (properties.EventKind == TimedEventKinds.Once)
            {
              if (--properties.RetryCount < 0)
                this._TimedEventService.DeleteEventID(properties.KeyID, this.Session.DataManager);
              else
                this.Session.DataManager.ExecuteNonQuery("UPDATE IMS_TIMED_EVENTS SET F_ERROR_MSG = :errorMsg, F_TRY_COUNT = :tryCount WHERE F_KEY = :keyID", this.Session.DataManager.Parameter("errorMsg", (object) str), this.Session.DataManager.Parameter("tryCount", (object) properties.RetryCount), this.Session.DataManager.Parameter("keyID", (object) properties.KeyID));
            }
            else
              this.Session.DataManager.ExecuteNonQuery("UPDATE IMS_TIMED_EVENTS SET F_ERROR_MSG = :errorMsg WHERE F_KEY = :keyID", this.Session.DataManager.Parameter("errorMsg", (object) str), this.Session.DataManager.Parameter("keyID", (object) properties.KeyID));
          }
        }
      }
      finally
      {
        (this._TimedEventService as DBTimedEvents).inProgressDict.TryRemove(key, out bool _);
        Interlocked.Exchange(ref this._eventInProcess, 0);
      }
    }
    catch
    {
    }
  }

  public virtual TimedEventProperties BeforeAddEvent(
    IUserSession session,
    TimedEventProperties properties)
  {
    return properties;
  }

  public virtual TimedEventProperties BeforeEditEvent(
    IUserSession session,
    TimedEventProperties properties)
  {
    return properties;
  }

  public virtual bool Visible => true;
}
