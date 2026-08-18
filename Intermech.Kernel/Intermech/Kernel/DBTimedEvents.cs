// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBTimedEvents
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;


namespace Intermech.Kernel;

public class DBTimedEvents : LongLifeObject, IDBTimedEvents, IDisposable, ITimedEventsSheduler
{
  private Hashtable _Subscribers = Hashtable.Synchronized(new Hashtable());
  private List<ScheduledEventHandlerInfo> _SubscribersInfo = new List<ScheduledEventHandlerInfo>();
  private UserSession _SystemSession;
  private IDbManager dbManager;
  private IEventLogHelper _eventLogHelper;
  private DataTable _EventsList;
  private Timer _Timer;
  private TimerCallback _TimerDelegate;
  private string _LockProcessName = string.Empty;
  private bool _CanProcess;
  private bool _IsPrimaryServer = true;
  private string _ServerName = EnvironmentConsts.MachineName.ToUpper();
  internal ConcurrentDictionary<int, bool> inProgressDict = new ConcurrentDictionary<int, bool>();
  public const string TimedEventsTraceFileName = "TimedEvents.log";
  private bool ProcessTraceMode;
  private bool _FirstStart = true;

  public DBTimedEvents(
    IDbManagerService idbmanager,
    IEventLogHelper eventlogHelper,
    IApplicationStateEventsService applicationStateEvents)
  {
    if (idbmanager == null)
      throw new ArgumentNullException(nameof (idbmanager));
    if (eventlogHelper == null)
      throw new ArgumentNullException(nameof (eventlogHelper));
    if (applicationStateEvents == null)
      throw new ArgumentNullException(nameof (applicationStateEvents));
    this.dbManager = idbmanager.CreateDbManager();
    this._eventLogHelper = eventlogHelper;
    if (ConfigurationManager.AppSettings.Get("TimedEventsTrace") == "1")
      this.ProcessTraceMode = true;
    object obj = this.dbManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "PrimaryServer"));
    if (obj != null && obj != DBNull.Value)
    {
      string upper = obj.ToString().Trim().ToUpper();
      this._IsPrimaryServer = upper == this._ServerName || upper == string.Empty;
    }
    if (this.ProcessTraceMode)
      this._eventLogHelper.AddToTrace(this._IsPrimaryServer ? "Стартована служба обработки событий в режиме приоритетного сервера" : "Стартована служба обработки событий", Consts.traceAlways, "TimedEvents.log");
    this._TimerDelegate = new TimerCallback(this.DoTimedEvent);
    this._Timer = new Timer(this._TimerDelegate, (object) null, 60000, 60000);
    applicationStateEvents.Exit += new EventHandler(this.OnBeforeApplicationExit);
    applicationStateEvents.EmergencyExit += new EventHandler(this.OnBeforeApplicationEmergencyExit);
  }

  private void DoTimedEvent(object state) => this.ProcessEvents();

  private void OnBeforeApplicationExit(object sender, EventArgs e) => this.UnlockEventsQueue();

  private void OnBeforeApplicationEmergencyExit(object sender, EventArgs e)
  {
    this.UnlockEventsQueue();
  }

  public void UnlockEventsQueue()
  {
    if (this._SystemSession == null)
      return;
    UserSession userSession = this._SystemSession.Clone(nameof (UnlockEventsQueue)) as UserSession;
    try
    {
      IDbManager dataManager = userSession.DataManager;
      object obj = dataManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dataManager.Parameter("moduleName", (object) "KERNEL"), dataManager.Parameter("usrID", (object) 0L), dataManager.Parameter("sectID", (object) "TIMED_EVENTS"), dataManager.Parameter("parName", (object) "CurrentServer"));
      if (obj == null || !(obj.ToString() == this._ServerName))
        return;
      dataManager.ExecuteScalar("DELETE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dataManager.Parameter("moduleName", (object) "KERNEL"), dataManager.Parameter("usrID", (object) 0L), dataManager.Parameter("sectID", (object) "TIMED_EVENTS"), dataManager.Parameter("parName", (object) "CurrentServer"));
      dataManager.ExecuteScalar("DELETE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dataManager.Parameter("moduleName", (object) "KERNEL"), dataManager.Parameter("usrID", (object) 0L), dataManager.Parameter("sectID", (object) "TIMED_EVENTS"), dataManager.Parameter("parName", (object) "LastTime"));
    }
    finally
    {
      userSession.Logout(nameof (UnlockEventsQueue));
    }
  }

  private void ProcessCurrentEvent(TimedEventProperties props, IUserSession uSession)
  {
    if (!(this._Subscribers[(object) props.ServiceGuid] is IDBTimedService subscriber))
    {
      this.AddToTrace(LocalizationHolder.rm.GetString("Kernel_837") + props.ServiceGuid.ToString(), true);
      this._eventLogHelper.AddEvent(props.ObjectID, 0L, 14, 0L, LocalizationHolder.rm.GetString("Kernel_882"), LocalizationHolder.rm.GetString("Kernel_837") + props.ServiceGuid.ToString(), ActionType.Execute, EventlogRecordType.Error, props.UserID, EnvironmentConsts.MachineName, (IUserSession) null);
      if (props.EventKind != TimedEventKinds.Once)
        return;
      lock (this.dbManager)
        this.DeleteEvent(props.KeyID, this.dbManager);
    }
    else
    {
      this.AddToTrace("Выполняется событие " + this.GetTraceEventInfo(props), false);
      bool flag1 = false;
      bool flag2 = false;
      string str = string.Empty;
      try
      {
        if (subscriber is IDBManualScheduledService scheduledService)
          flag1 = scheduledService.IsMultiThread;
        if (props.EventKind != TimedEventKinds.Once)
        {
          DateTime nextUtcDate = props.GetNextUtcDate(uSession.TimeZoneOffset);
          lock (this.dbManager)
          {
            if (this._FirstStart && !props.ImmediateRun)
            {
              this.dbManager.ExecuteNonQuery(string.Format("UPDATE IMS_TIMED_EVENTS SET F_DATE = :nextDate WHERE F_KEY = :keyID"), this.dbManager.Parameter("nextDate", (object) nextUtcDate), this.dbManager.Parameter("keyID", (object) props.KeyID));
              return;
            }
            this.dbManager.ExecuteNonQuery($"UPDATE IMS_TIMED_EVENTS SET F_DATE = :nextDate, F_PREV_DATE = {this.dbManager.DataProvider.Now}, F_ERROR_MSG = NULL WHERE F_KEY = :keyID", this.dbManager.Parameter("nextDate", (object) nextUtcDate), this.dbManager.Parameter("keyID", (object) props.KeyID));
          }
        }
        if (flag1)
        {
          if (props.EventKind == TimedEventKinds.Once)
          {
            lock (this.dbManager)
              this.dbManager.ExecuteNonQuery($"UPDATE IMS_TIMED_EVENTS SET F_PREV_DATE = {this.dbManager.DataProvider.Now}, F_ERROR_MSG = NULL WHERE F_KEY = :keyID", this.dbManager.Parameter("keyID", (object) props.KeyID));
          }
          new Thread(new ParameterizedThreadStart(scheduledService.ProcessEventInThread))
          {
            IsBackground = true
          }.Start((object) props);
          this.AddToTrace($"Событие начало выполняться службой '{scheduledService.ServiceName}' в отдельном потоке.", false);
          return;
        }
        try
        {
          this.inProgressDict.TryAdd(props.KeyID, true);
          if (subscriber.ProcessEvent(props))
          {
            this.AddToTrace($"Событие выполнено службой '{subscriber.ServiceName}' в основном потоке.", false);
            if (props.EventKind != TimedEventKinds.Once)
              return;
            lock (this.dbManager)
              this.DeleteEvent(props.KeyID, this.dbManager);
            flag2 = true;
          }
          else
            this.AddToTrace($"Событие выполнено службой '{subscriber.ServiceName}' в основном потоке. Функция ProcessEvent вернула false.", false);
        }
        finally
        {
          this.inProgressDict.TryRemove(props.KeyID, out bool _);
        }
      }
      catch (Exception ex)
      {
        this.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_838"), (object) subscriber.ServiceName, (object) ex.Message), true);
        this.AddToTrace(ex.StackTrace, true);
        this._eventLogHelper.AddEvent(props.ObjectID, 0L, 14, 0L, LocalizationHolder.rm.GetString("Kernel_883"), string.Format(LocalizationHolder.rm.GetString("Kernel_838"), (object) subscriber.ServiceName, (object) ex.Message), ActionType.Execute, EventlogRecordType.Warning, props.UserID, EnvironmentConsts.MachineName, (IUserSession) null);
        str = ex.Message;
      }
      if (flag2)
        return;
      if (--props.RetryCount < 0)
      {
        lock (this.dbManager)
          this.DeleteEvent(props.KeyID, this.dbManager);
      }
      else
      {
        lock (this.dbManager)
        {
          if (str.Length > Consts.MaxNoteLength)
            str = str.Substring(0, Consts.MaxNoteLength);
          this.dbManager.ExecuteNonQuery("UPDATE IMS_TIMED_EVENTS SET F_TRY_COUNT = :tcount, F_ERROR_MSG = :errMsg WHERE F_KEY = :keyID", this.dbManager.Parameter("tcount", (object) props.RetryCount), this.dbManager.Parameter("errMsg", (object) str), this.dbManager.Parameter("keyID", (object) props.KeyID));
        }
      }
    }
  }

  public void RegisterService(object timedService)
  {
    Guid guid = timedService is IDBGuid ? (timedService as IDBGuid).GUID : throw new KernelExceptionID(sc_12332.ssp_appserver_12335(1404944024));
    if (!(timedService is IDBTimedService))
      throw new KernelExceptionID(sc_12332.ssp_appserver_12334(1330167554));
    if (this._Subscribers.Contains((object) guid))
      throw new KernelExceptionID(sc_12332.ssp_appserver_12333(440963575), (object) (timedService as IDBTimedService).ServiceName);
    this.InitializeTimedServiceResources((IDBTimedService) timedService);
    this._Subscribers.Add((object) guid, timedService);
    this.AddToTrace($"Зарегистрирована служба обработки событий '{(timedService as IDBTimedService).ServiceName}' с идентификатором {guid.ToString()}.", false);
    if (!(timedService is IDBManualScheduledService) || !(timedService as IDBManualScheduledService).Visible)
      return;
    this._SubscribersInfo.Add(new ScheduledEventHandlerInfo(guid, (timedService as IDBManualScheduledService).ServiceName));
  }

  public void UnregisterService(object timedService)
  {
    if (timedService == null)
      throw new ArgumentNullException(nameof (timedService));
    if (!(timedService is IDBGuid))
      throw new KernelExceptionID(sc_12332.ssp_appserver_12336(2123787361));
    IDBGuid serviceGuid = timedService is IDBTimedService ? timedService as IDBGuid : throw new KernelExceptionID(sc_12332.ssp_appserver_12337(1622618422));
    int num = 0;
    lock (this.dbManager)
    {
      DataTable eventsTable = this.GetEventsTable(this.dbManager);
      if (eventsTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) eventsTable.Rows)
        {
          if (new TimedEventProperties(row).ServiceGuid == serviceGuid.GUID)
            ++num;
        }
      }
    }
    if (num != 0)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1166"), (object) (timedService as IDBTimedService).ServiceName, (object) serviceGuid.GUID, (object) num));
    lock (this._Subscribers)
    {
      this.ReleaseTimedServiceResources((IDBTimedService) timedService);
      this._Subscribers.Remove((object) serviceGuid.GUID);
      if (!(timedService is IDBManualScheduledService))
        return;
      lock (this._SubscribersInfo)
        this._SubscribersInfo.RemoveAll((Predicate<ScheduledEventHandlerInfo>) (item => item.ServiceGuid == serviceGuid.GUID));
    }
  }

  private void InitializeTimedServiceResources(IDBTimedService timedService)
  {
    timedService.TimedEventService = (IDBTimedEvents) this;
    if (!(timedService is DBCustomManualScheduledService scheduledService))
      return;
    scheduledService.InitializeInternal();
  }

  private void ReleaseTimedServiceResources(IDBTimedService timedService)
  {
    if (timedService is DBCustomManualScheduledService scheduledService)
      scheduledService.ReleaseInternal();
    timedService.TimedEventService = (IDBTimedEvents) null;
  }

  public void Start() => this._CanProcess = true;

  private void ProcessEvents()
  {
    try
    {
      if (!this._CanProcess)
        return;
      this._CanProcess = false;
      try
      {
        lock (this.dbManager)
        {
          bool flag = false;
          if (this._IsPrimaryServer)
          {
            object obj1 = this.dbManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "CurrentServer"));
            if (obj1 != null && obj1 != DBNull.Value)
            {
              if (obj1.ToString() == this._ServerName)
              {
                this.dbManager.ExecuteNonQuery("UPDATE IMS_CONFIGS SET F_VALUE = :currTime WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", this.dbManager.Parameter("currTime", (object) DateTime.UtcNow.ToString((IFormatProvider) CultureInfo.InvariantCulture)), this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "LastTime"));
                flag = true;
              }
              else
              {
                object obj2 = this.dbManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "LastTime"));
                if (obj2 != null && obj2 != DBNull.Value)
                {
                  if (DateTime.UtcNow - Convert.ToDateTime(obj2, (IFormatProvider) CultureInfo.InvariantCulture) > TimeSpan.FromMinutes(10.0))
                  {
                    this.dbManager.BeginTransaction();
                    try
                    {
                      this.dbManager.ExecuteScalar(sc_12332.ssp_appserver_12338(), this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "CurrentServer"), this.dbManager.Parameter("servName", (object) this._ServerName));
                      this.dbManager.ExecuteScalar("UPDATE IMS_CONFIGS SET F_VALUE = :currTime WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", this.dbManager.Parameter("currTime", (object) DateTime.UtcNow.ToString((IFormatProvider) CultureInfo.InvariantCulture)), this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "LastTime"));
                      this.dbManager.Commit();
                      flag = true;
                      this.AddToTrace("Произведён перехват очереди событий у приоритетного сервера " + obj1.ToString(), false);
                    }
                    catch
                    {
                      this.dbManager.Rollback();
                      throw;
                    }
                  }
                }
                else
                {
                  this.dbManager.BeginTransaction();
                  try
                  {
                    this.dbManager.ExecuteScalar(sc_12332.ssp_appserver_12339(), this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "CurrentServer"), this.dbManager.Parameter("servName", (object) this._ServerName));
                    this.dbManager.ExecuteScalar("INSERT INTO IMS_CONFIGS (F_MODULE_NAME, F_USER_ID, F_SECTION_ID, F_PARAM_NAME, F_VALUE) VALUES (:moduleName, :usrID, :sectID, :parName, :currTime)", this.dbManager.Parameter("currTime", (object) DateTime.UtcNow.ToString((IFormatProvider) CultureInfo.InvariantCulture)), this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "LastTime"));
                    this.dbManager.Commit();
                    flag = true;
                    this.AddToTrace($"Произведён перехват очереди событий у приоритетного сервера {obj1.ToString()}, т.к. время последнего обращения к очереди не определено.", false);
                  }
                  catch
                  {
                    this.dbManager.Rollback();
                    throw;
                  }
                }
              }
            }
            else
            {
              this.dbManager.BeginTransaction();
              try
              {
                this.dbManager.ExecuteScalar("INSERT INTO IMS_CONFIGS (F_MODULE_NAME, F_USER_ID, F_SECTION_ID, F_PARAM_NAME, F_VALUE) VALUES (:moduleName, :usrID, :sectID, :parName, :servName)", this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "CurrentServer"), this.dbManager.Parameter("servName", (object) this._ServerName));
                this.dbManager.ExecuteScalar("DELETE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "LastTime"));
                this.dbManager.ExecuteScalar("INSERT INTO IMS_CONFIGS (F_MODULE_NAME, F_USER_ID, F_SECTION_ID, F_PARAM_NAME, F_VALUE) VALUES (:moduleName, :usrID, :sectID, :parName, :currTime)", this.dbManager.Parameter("currTime", (object) DateTime.UtcNow.ToString((IFormatProvider) CultureInfo.InvariantCulture)), this.dbManager.Parameter("moduleName", (object) "KERNEL"), this.dbManager.Parameter("usrID", (object) 0L), this.dbManager.Parameter("sectID", (object) "TIMED_EVENTS"), this.dbManager.Parameter("parName", (object) "LastTime"));
                this.dbManager.Commit();
                flag = true;
                this.AddToTrace("Начата обработка общей очереди событий.", false);
              }
              catch
              {
                this.dbManager.Rollback();
                throw;
              }
            }
          }
          if (flag)
          {
            this.dbManager.ExecuteNonQuery("DELETE FROM IMS_TIMED_EVENTS WHERE F_DEADLOCK_DATE < " + this.dbManager.DataProvider.Now);
            this._EventsList = this.dbManager.ExecuteDataTable($"SELECT * FROM IMS_TIMED_EVENTS WHERE F_DATE < {this.dbManager.DataProvider.Now} AND (F_COMPUTER_NAME = :compName OR F_COMPUTER_NAME = :compNameEmpty OR F_COMPUTER_NAME IS NULL) ORDER BY F_DATE ASC", this.dbManager.Parameter("compName", (object) this._ServerName), this.dbManager.Parameter("compNameEmpty", (object) ""));
          }
          else
            this._EventsList = this.dbManager.ExecuteDataTable($"SELECT * FROM IMS_TIMED_EVENTS WHERE F_DATE < {this.dbManager.DataProvider.Now} AND (F_COMPUTER_NAME = :compName) ORDER BY F_DATE ASC", this.dbManager.Parameter("compName", (object) this._ServerName));
        }
        IUserSession uSession = this.SystemSession.Clone("DBTimedEvents.ProcessEvents");
        try
        {
          for (int index = 0; index < this._EventsList.Rows.Count; ++index)
            this.ProcessCurrentEvent(new TimedEventProperties(this._EventsList.Rows[index]), uSession);
        }
        finally
        {
          this._FirstStart = false;
          uSession.Logout("DBTimedEvents.ProcessEvents");
        }
      }
      finally
      {
        this._CanProcess = true;
      }
    }
    catch (Exception ex)
    {
      this.AddToTrace("Ошибка выполнения метода ProcessEvents: " + ex.Message, true);
      this.AddToTrace(ex.StackTrace, true);
    }
  }

  public void DeleteEventID(int eventID, IDbManager db) => this.DeleteEvent(eventID, db);

  private void DeleteEvent(int eventID, IDbManager db)
  {
    this.AddToTrace("Удаляется событие номер N" + eventID.ToString(), false);
    db.ExecuteNonQuery("DELETE FROM IMS_TIMED_EVENTS WHERE F_KEY = :eventID", db.Parameter(nameof (eventID), (object) eventID));
    this.AddToTrace($"Cобытие номер N{eventID} удалено.", false);
  }

  public int FindEvent(Guid serviceGuid, int intInfo, long objectID, IDbManager db)
  {
    object obj = db.ExecuteScalar("SELECT F_KEY FROM IMS_TIMED_EVENTS WHERE F_GUID_TYPE = :guidType AND F_INT_INFO = :info AND F_OBJECT_ID = :objID", db.Parameter("guidType", (object) serviceGuid.ToString()), db.Parameter("info", (object) intInfo), db.Parameter("objID", (object) objectID));
    return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
  }

  private UserSession SystemSession
  {
    get
    {
      if (this._SystemSession == null)
      {
        this._SystemSession = new UserSession();
        this._SystemSession.SetLoginCapabilities(true, true);
        this._SystemSession.Login("SYSTEM", new PswPackage(), EnvironmentConsts.MachineName, TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now), 0L, nameof (DBTimedEvents));
      }
      return this._SystemSession;
    }
  }

  public IUserSession GetSystemSessionPermanentClone(string sessionName)
  {
    return this.SystemSession.Clone(true, sessionName);
  }

  public IUserSession GetSystemSessionTemporaryClone(string sessionName)
  {
    return this.SystemSession.Clone(sessionName);
  }

  internal string GetTraceEventInfo(TimedEventProperties props)
  {
    return $"'{props.Name}' для службы {props.ServiceGuid}. Время срабатывания {props.StartDate + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now)}. Периодичность {props.EventKind}. Объект N{props.ObjectID}.";
  }

  public int AddEvent(TimedEventProperties properties, IDbManager db)
  {
    object obj = properties.DeadlockDate == DateTime.MinValue || properties.DeadlockDate == DateTime.MaxValue ? (object) DBNull.Value : (object) properties.DeadlockDate;
    this.AddToTrace("Регистрируется событие " + this.GetTraceEventInfo(properties), false);
    db.BeginTransaction();
    try
    {
      db.ExecuteSpNonQuery("IMS_ADD_TIMED_EVENT", db.Parameter("inGUID_TYPE", (object) properties.ServiceGuid.ToString()), db.Parameter("inSTRING_INFO", (object) properties.StringInfo), db.Parameter("inDATE", (object) properties.StartDate), db.Parameter("inINT_INFO", (object) properties.IntInfo), db.Parameter("inUSER_ID", (object) properties.UserID), db.Parameter("inOBJECT_ID", (object) properties.ObjectID), db.Parameter("inDEADLOCK_DATE", obj), db.Parameter("inTRY_COUNT", (object) properties.RetryCount), db.OutputParameter("outKEY", (object) properties.KeyID));
      properties.KeyID = Convert.ToInt32(db.GetOutputParameterValue("outKEY"));
      if (properties.EventKind != TimedEventKinds.Once)
        db.ExecuteNonQuery("UPDATE IMS_TIMED_EVENTS SET F_EVENT_KIND = :eventKind, F_SCHEDULE = :sched WHERE F_KEY = :keyID", db.Parameter("eventKind", (object) (int) properties.EventKind), db.Parameter("sched", (object) properties.Schedule), db.Parameter("keyID", (object) properties.KeyID));
      if (properties.Name != string.Empty)
        db.ExecuteNonQuery("UPDATE IMS_TIMED_EVENTS SET F_NAME = :eventName WHERE F_KEY = :keyID", db.Parameter("eventName", (object) properties.Name), db.Parameter("keyID", (object) properties.KeyID));
      if (properties.ServerName != string.Empty)
        db.ExecuteNonQuery("UPDATE IMS_TIMED_EVENTS SET F_COMPUTER_NAME = :serverName WHERE F_KEY = :keyID", db.Parameter("serverName", (object) properties.ServerName.ToUpper()), db.Parameter("keyID", (object) properties.KeyID));
      if (properties.ImmediateRun)
        db.ExecuteNonQuery("UPDATE IMS_TIMED_EVENTS SET F_IMMEDIATE_RUN = :immRun WHERE F_KEY = :keyID", db.Parameter("immRun", (object) 1), db.Parameter("keyID", (object) properties.KeyID));
      db.Commit();
    }
    catch (Exception ex)
    {
      db.Rollback();
      this.AddToTrace($"Ошибка регистрации события {this.GetTraceEventInfo(properties)}: {ex.Message}", true);
      this.AddToTrace(ex.StackTrace, true);
      throw;
    }
    return properties.KeyID;
  }

  public void AddToTrace(string message, bool always)
  {
    if (!always && !this.ProcessTraceMode)
      return;
    this._eventLogHelper.AddToTrace(message, Consts.traceAlways, "TimedEvents.log");
  }

  public void Dispose()
  {
    this._Timer.Change(-1, -1);
    if (this._SystemSession != null)
      this._SystemSession.Logout(nameof (DBTimedEvents));
    this.dbManager.Dispose();
    this._Subscribers.Clear();
  }

  private UserSession GetSession(Guid sessionGuid)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    return sessionById.IsAdmin ? sessionById : throw new KernelExceptionID(sc_12332.ssp_appserver_12340(79592808));
  }

  private TimedEventProperties GetEventProperties(int eventID, IDbManager db)
  {
    DataTable dataTable = db.ExecuteDataTable("SELECT * FROM IMS_TIMED_EVENTS WHERE F_KEY = :eventID", db.Parameter(nameof (eventID), (object) eventID));
    if (dataTable.Rows.Count > 0)
      return new TimedEventProperties(dataTable.Rows[0]);
    throw new KernelException(string.Format(sc_12332.ssp_appserver_12341(), (object) eventID));
  }

  public DataTable GetEventsTable(IDbManager db)
  {
    return db.ExecuteDataTable(sc_12332.ssp_appserver_12342());
  }

  private void AddStatusColumn(DataTable tbl)
  {
    tbl.Columns.Add("F_STATUS", typeof (string));
    for (int index = 0; index < tbl.Rows.Count; ++index)
    {
      if (this.inProgressDict.TryGetValue(Convert.ToInt32(tbl.Rows[index]["F_KEY"]), out bool _))
        tbl.Rows[index]["F_STATUS"] = (object) "Выполняется";
      else if (tbl.Rows[index]["F_ERROR_MSG"].ToString() != string.Empty)
        tbl.Rows[index]["F_STATUS"] = (object) "Завершилась с ошибкой";
    }
  }

  public DataTable GetEventsTable(Guid sessionGuid)
  {
    UserSession session = this.GetSession(sessionGuid);
    DataTable tbl = session.IsAdmin ? this.GetEventsTable(session.DataManager) : throw new KernelExceptionID(sc_12332.ssp_appserver_12343(883928009));
    this.AddStatusColumn(tbl);
    return tbl;
  }

  private void Trace2Eventlog_DeleteEvents(
    EventlogRecordType recType,
    UserSession session,
    int[] eventIDs)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (int eventId in eventIDs)
    {
      TimedEventProperties eventProperties = this.GetEventProperties(eventId, session.DataManager);
      stringBuilder.AppendLine(eventProperties.ToString());
    }
    string str = stringBuilder.ToString();
    string Note = str.Length > Consts.MaxStringSize ? str.Substring(0, Consts.MaxStringSize) : str;
    session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Задачи планировщика", Note, ActionType.Delete, recType, session.UserID, session.ComputerName, (IUserSession) session);
  }

  public void DeleteEvents(Guid sessionGuid, int[] eventIDs)
  {
    UserSession session = this.GetSession(sessionGuid);
    if (!session.IsAdmin)
      throw new KernelExceptionID(sc_12332.ssp_appserver_12344(1489478957));
    try
    {
      session.GetSystemSecurity().CheckAccess(ActionType.AdminTaskManager);
      this.Trace2Eventlog_DeleteEvents(EventlogRecordType.AccessGranted, session, eventIDs);
    }
    catch (AccessDeniedException ex)
    {
      this.Trace2Eventlog_DeleteEvents(EventlogRecordType.AccessDenied, session, eventIDs);
      throw;
    }
    for (int index = 0; index < eventIDs.Length; ++index)
      this.DeleteEvent(eventIDs[index], session.DataManager);
  }

  public TimedEventProperties AddEvent(Guid sessionGuid, TimedEventProperties properties)
  {
    UserSession session = this.GetSession(sessionGuid);
    SqlHelper.ValidateEmptyValue(properties.Name, "Name");
    if (properties.EventKind != TimedEventKinds.Once)
      properties.StartDate = properties.GetNextUtcDate(session.TimeZoneOffset);
    string str = properties.ToString();
    try
    {
      session.GetSystemSecurity().CheckAccess(ActionType.AdminTaskManager);
      session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Задача планировщика: " + str, string.Empty, ActionType.Create, EventlogRecordType.AccessGranted, session.UserID, session.ComputerName, (IUserSession) session);
    }
    catch (AccessDeniedException ex)
    {
      session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Задача планировщика: " + str, string.Empty, ActionType.Create, EventlogRecordType.AccessDenied, session.UserID, session.ComputerName, (IUserSession) session);
      throw;
    }
    session.StartTransaction();
    DataTable dataTable;
    try
    {
      if (!(this._Subscribers[(object) properties.ServiceGuid] is IDBManualScheduledService subscriber))
        throw new KernelException(LocalizationHolder.rm.GetString("Kernel_837") + properties.ServiceGuid.ToString());
      properties = subscriber.BeforeAddEvent((IUserSession) session, properties);
      int num = this.AddEvent(properties, session.DataManager);
      dataTable = session.DataManager.ExecuteDataTable("SELECT * FROM IMS_TIMED_EVENTS WHERE F_KEY = :eventID", session.DataManager.Parameter("eventID", (object) num));
      session.Commit();
    }
    catch
    {
      session.Rollback();
      throw;
    }
    return new TimedEventProperties(dataTable.Rows[0]);
  }

  public TimedEventProperties EditEvent(Guid sessionGuid, TimedEventProperties properties)
  {
    UserSession session = this.GetSession(sessionGuid);
    SqlHelper.ValidateEmptyValue(properties.Name, "Name");
    if (properties.EventKind != TimedEventKinds.Once)
      properties.StartDate = properties.GetNextUtcDate(session.TimeZoneOffset);
    string str = properties.ToString();
    try
    {
      session.GetSystemSecurity().CheckAccess(ActionType.AdminTaskManager);
      session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Задача планировщика: " + str, string.Empty, ActionType.Edit, EventlogRecordType.AccessGranted, session.UserID, session.ComputerName, (IUserSession) session);
    }
    catch (AccessDeniedException ex)
    {
      session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Задача планировщика: " + str, string.Empty, ActionType.Edit, EventlogRecordType.AccessDenied, session.UserID, session.ComputerName, (IUserSession) session);
      throw;
    }
    session.StartTransaction();
    DataTable dataTable;
    try
    {
      if (!(this._Subscribers[(object) properties.ServiceGuid] is IDBManualScheduledService subscriber))
        throw new KernelException(LocalizationHolder.rm.GetString("Kernel_837") + properties.ServiceGuid.ToString());
      properties = subscriber.BeforeEditEvent((IUserSession) session, properties);
      session.DataManager.ExecuteNonQuery(sc_12332.ssp_appserver_12345(), session.DataManager.Parameter("immRun", (object) (properties.ImmediateRun ? 1 : 0)), session.DataManager.Parameter("eventName", (object) properties.Name), session.DataManager.Parameter("sched", (object) properties.Schedule), session.DataManager.Parameter("compName", (object) properties.ServerName.ToUpper()), session.DataManager.Parameter("eventKind", (object) (int) properties.EventKind), session.DataManager.Parameter("tryCount", (object) properties.RetryCount), session.DataManager.Parameter("strInfo", (object) properties.StringInfo), session.DataManager.Parameter("nextDate", (object) properties.StartDate), session.DataManager.Parameter("intInfo", (object) properties.IntInfo), session.DataManager.Parameter("usrID", (object) properties.UserID), session.DataManager.Parameter("eventID", (object) properties.KeyID));
      dataTable = session.DataManager.ExecuteDataTable("SELECT * FROM IMS_TIMED_EVENTS WHERE F_KEY = :eventID", session.DataManager.Parameter("eventID", (object) properties.KeyID));
      session.Commit();
    }
    catch
    {
      session.Rollback();
      throw;
    }
    return new TimedEventProperties(dataTable.Rows[0]);
  }

  public void RunEvent(Guid sessionGuid, int eventID)
  {
    UserSession session = this.GetSession(sessionGuid);
    string str = this.GetEventProperties(eventID, session.DataManager).ToString();
    try
    {
      session.GetSystemSecurity().CheckAccess(ActionType.AdminTaskManager);
      session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Задача планировщика: " + str, string.Empty, ActionType.Execute, EventlogRecordType.AccessGranted, session.UserID, session.ComputerName, (IUserSession) session);
    }
    catch (AccessDeniedException ex)
    {
      session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Задача планировщика: " + str, string.Empty, ActionType.Execute, EventlogRecordType.AccessDenied, session.UserID, session.ComputerName, (IUserSession) session);
      throw;
    }
    this.ProcessCurrentEvent(this.GetEventProperties(eventID, session.DataManager), (IUserSession) session);
  }

  public void SetPrimaryServer(Guid sessionGuid, string serverName)
  {
    UserSession session = this.GetSession(sessionGuid);
    try
    {
      session.GetSystemSecurity().CheckAccess(ActionType.AdminTaskManager);
      session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Изменение приоритетного сервера обработки очереди задач: " + serverName, string.Empty, ActionType.Edit, EventlogRecordType.AccessGranted, session.UserID, session.ComputerName, (IUserSession) session);
    }
    catch (AccessDeniedException ex)
    {
      session.EventLogHelper.AddEvent(0L, 0L, 14, 0L, "Изменение приоритетного сервера обработки очереди задач: " + serverName, string.Empty, ActionType.Edit, EventlogRecordType.AccessDenied, session.UserID, session.ComputerName, (IUserSession) session);
      throw;
    }
    if (session.DataManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :usrID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", session.DataManager.Parameter("moduleName", (object) "KERNEL"), session.DataManager.Parameter("usrID", (object) 0L), session.DataManager.Parameter("sectID", (object) "TIMED_EVENTS"), session.DataManager.Parameter("parName", (object) "PrimaryServer")) != null)
      session.DataManager.ExecuteNonQuery(sc_12332.ssp_appserver_12346(), session.DataManager.Parameter(nameof (serverName), (object) serverName), session.DataManager.Parameter("moduleName", (object) "KERNEL"), session.DataManager.Parameter("usrID", (object) 0L), session.DataManager.Parameter("sectID", (object) "TIMED_EVENTS"), session.DataManager.Parameter("parName", (object) "PrimaryServer"));
    else
      session.DataManager.ExecuteNonQuery(sc_12332.ssp_appserver_12347(), session.DataManager.Parameter(nameof (serverName), (object) serverName), session.DataManager.Parameter("moduleName", (object) "KERNEL"), session.DataManager.Parameter("usrID", (object) 0L), session.DataManager.Parameter("sectID", (object) "TIMED_EVENTS"), session.DataManager.Parameter("parName", (object) "PrimaryServer"));
  }

  public string GetPrimaryServer(Guid sessionGuid)
  {
    UserSession session = this.GetSession(sessionGuid);
    object obj = session.DataManager.ExecuteScalar(sc_12332.ssp_appserver_12348(), session.DataManager.Parameter("moduleName", (object) "KERNEL"), session.DataManager.Parameter("usrID", (object) 0L), session.DataManager.Parameter("sectID", (object) "TIMED_EVENTS"), session.DataManager.Parameter("parName", (object) "PrimaryServer"));
    return obj != null && obj != DBNull.Value ? obj.ToString() : string.Empty;
  }

  public ScheduledEventHandlerInfo[] GetScheduledEventHandlers() => this._SubscribersInfo.ToArray();
}
