// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ServersSynchTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services.ScheduledTasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;


namespace Intermech.Kernel.Services;

internal class ServersSynchTask : IServerSynchronizersManager
{
  private IAppServers _Servers;
  private ConcurrentDictionary<Guid, IServerSynchronizer> _Synchronizers = new ConcurrentDictionary<Guid, IServerSynchronizer>();
  private const string syncTraceFileName = "ServerSynchronizers.log";
  private UserSession _session;
  private Timer _Timer;
  private TimerCallback _TimerDelegate;
  private bool _SyncEnabled;
  private int _SynchPeriod = 5;
  private bool inProcess;
  private ISystemDiagnosticsTask _DiagnosticsTask;

  public ServersSynchTask(UserSession session, IAppServers servers)
  {
    this._session = session;
    this._Servers = servers;
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    ServerSynchActivator timedService = new ServerSynchActivator(this);
    service.RegisterService((object) timedService);
    if (servers.ServersCount <= 0)
      return;
    this.StartSynchIfNeed();
    string[] aliveServers = servers.GetAliveServers();
    TimedEventProperties properties = new TimedEventProperties();
    properties.DeadlockDate = DateTime.UtcNow + TimeSpan.FromDays(1.0);
    properties.ErrorMessage = string.Empty;
    properties.EventKind = TimedEventKinds.Once;
    properties.KeyID = 0;
    properties.Name = string.Empty;
    properties.ObjectID = 0L;
    properties.RetryCount = 10;
    properties.ServiceGuid = timedService.GUID;
    properties.StartDate = DateTime.UtcNow + TimeSpan.FromMinutes(1.0);
    properties.UserID = 0L;
    properties.StringInfo = string.Empty;
    for (int index = 0; index < aliveServers.Length; ++index)
    {
      if (aliveServers[index].IndexOf(':') > -1)
      {
        properties.IntInfo = Convert.ToInt32(aliveServers[index].Substring(aliveServers[index].IndexOf(':') + 1));
        properties.ServerName = aliveServers[index].Substring(0, aliveServers[index].IndexOf(':'));
        service.AddEvent(properties, session.DataManager);
      }
    }
  }

  internal void StartSynchIfNeed()
  {
    if (this._SyncEnabled)
      return;
    this.ClearDeleteOnStartEvents();
    string s = ConfigurationManager.AppSettings.Get("ServersSyncPeriod");
    if (s != null && s != string.Empty && !int.TryParse(s, out this._SynchPeriod))
      this._SynchPeriod = 5;
    if (this._SynchPeriod <= 0)
      return;
    this._TimerDelegate = new TimerCallback(this.FindMessages4Server);
    this._Timer = new Timer(this._TimerDelegate, (object) null, TimeSpan.FromMinutes((double) this._SynchPeriod), TimeSpan.FromMinutes((double) this._SynchPeriod));
    this._session.EventLogHelper.AddToTrace($"Запущена служба синхронизации серверов с интервалом {this._SynchPeriod} мин.", Consts.traceAlways, "ServerSynchronizers.log");
    this._SyncEnabled = true;
  }

  private void FindMessages4Server(object state)
  {
    try
    {
      this.ProcessEvents();
      this.ProcessDiagnostics();
    }
    catch (Exception ex)
    {
      this._session.EventLogHelper.AddToTrace($"Ошибка выполнения метода синхронизации серверов приложений: {ex.Message}", Consts.traceAlways, "ServerSynchronizers.log");
      this._session.EventLogHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, "ServerSynchronizers.log");
    }
  }

  private void ProcessDiagnostics()
  {
    ISystemDiagnosticsTask diagnosticsTask = this.DiagnosticsTask;
    if (diagnosticsTask == null)
      return;
    if (diagnosticsTask.NeedCheckServersDiskSpace)
    {
      string stringInfo = diagnosticsTask.CheckIsoStorageFreeSpace();
      if (stringInfo != string.Empty)
      {
        SynchonizerEventProperties synchonizerEventProperties = new SynchonizerEventProperties("SRV_FREE_SPACE", SystemDiagnosticsTask.DiagnosticsGuid, stringInfo, false);
        if (!this.MessageExists(synchonizerEventProperties))
          this.AddSynchronizerEvent(synchonizerEventProperties, this._session.DataManager);
      }
    }
    if (!diagnosticsTask.NeedCheckServersMemoryUsage)
      return;
    string stringInfo1 = diagnosticsTask.CheckPeakMemoryUsage();
    if (!(stringInfo1 != string.Empty))
      return;
    SynchonizerEventProperties synchonizerEventProperties1 = new SynchonizerEventProperties("SRV_MEMORY_USAGE", SystemDiagnosticsTask.DiagnosticsGuid, stringInfo1, false);
    if (this.MessageExists(synchonizerEventProperties1))
      return;
    this.AddSynchronizerEvent(synchonizerEventProperties1, this._session.DataManager);
  }

  private bool MessageExists(SynchonizerEventProperties syncProps)
  {
    bool flag = false;
    DataTable dataTable = this._session.DataManager.ExecuteDataTable("SELECT F_SERVER_SRC, F_GUID FROM IMS_ISB WHERE F_SERVER_DST = :dstServer", this._session.DataManager.Parameter("dstServer", (object) syncProps.DestinationServer));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (dataTable.Rows[index][0].ToString() == this._Servers.ServerName && new Guid(dataTable.Rows[index][1].ToString()).Equals(SystemDiagnosticsTask.DiagnosticsGuid))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  private ISystemDiagnosticsTask DiagnosticsTask
  {
    get
    {
      if (this._DiagnosticsTask == null)
        this._DiagnosticsTask = ServiceUtils.GetService<ISystemDiagnosticsTask>((object) ApplicationServices.Container, false);
      return this._DiagnosticsTask;
    }
  }

  private void ClearDeleteOnStartEvents()
  {
    DataTable dataTable = this._session.DataManager.ExecuteDataTable("SELECT * FROM IMS_ISB WHERE F_SERVER_DST = :srvDst", this._session.DataManager.Parameter("srvDst", (object) this._Servers.ServerName));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      DataRow row = dataTable.Rows[index];
      if (Convert.ToInt16(row["F_DELETE_ON_START"]) != (short) 0)
        this.DeleteEvent(row);
    }
  }

  private bool IsThisServerEvent(DataRow row)
  {
    return row["F_SERVER_DST"].ToString() == this._Servers.ServerName;
  }

  private void DeleteEvent(DataRow row)
  {
    this._session.DataManager.ExecuteNonQuery("DELETE FROM IMS_ISB WHERE F_KEY = :keyID", this._session.DataManager.Parameter("keyID", row["F_KEY"]));
  }

  public void ProcessEvents()
  {
    if (this.inProcess)
      return;
    try
    {
      this.inProcess = true;
      DataTable dataTable = this._session.DataManager.ExecuteDataTable("SELECT * FROM IMS_ISB WHERE F_SERVER_DST = :srvDst", this._session.DataManager.Parameter("srvDst", (object) this._Servers.ServerName));
      if (dataTable.Rows.Count <= 0)
        return;
      List<string> stringList = new List<string>(dataTable.Rows.Count);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dataTable.Rows[index];
        try
        {
          SynchonizerEventProperties eventProps = new SynchonizerEventProperties(row);
          Guid guid = eventProps.Guid;
          string str = guid.ToString() + eventProps.StringInfo;
          if (stringList.IndexOf(str) < 0)
          {
            IServerSynchronizer serverSynchronizer;
            if (this._Synchronizers.TryGetValue(eventProps.Guid, out serverSynchronizer))
            {
              stringList.Add(str);
              try
              {
                serverSynchronizer.ExecuteEvent(eventProps, (IUserSession) this._session);
              }
              catch (Exception ex)
              {
                this._session.EventLogHelper.AddToTrace(string.Format("Задача '{2}' (параметры {3}) прервана с ошибкой: {0}{1}", (object) ex.Message, (object) (Environment.NewLine + ex.StackTrace), (object) serverSynchronizer.ServiceName, (object) eventProps.StringInfo), Consts.traceAlways, "ServerSynchronizers.log");
              }
            }
            else
            {
              IEventLogHelper eventLogHelper = this._session.EventLogHelper;
              guid = eventProps.Guid;
              string EventStr = "Не найдена служба синхронизации серверов с идентификатором " + guid.ToString();
              int traceAlways = Consts.traceAlways;
              eventLogHelper.AddToTrace(EventStr, traceAlways, "ServerSynchronizers.log");
            }
          }
        }
        finally
        {
          this.DeleteEvent(row);
        }
      }
    }
    catch (Exception ex)
    {
      this._session.EventLogHelper.AddToTrace($"Фоновая задача синхронизации серверов приложений прервана с ошибкой: {ex.Message}{Environment.NewLine}{ex.StackTrace}", Consts.traceAlways, "ServerSynchronizers.log");
    }
    finally
    {
      this.inProcess = false;
    }
  }

  public void RegisterSynchronizer(IServerSynchronizer synchronizer)
  {
    if (synchronizer == null)
      throw new ArgumentNullException(nameof (synchronizer));
    if (synchronizer.Manager != null)
      throw new KernelException($"Попытка повторно зарегистрировать службу синхронизации серверов (ServiceGuid={synchronizer.ServiceGUID}', ServiceName='{synchronizer.ServiceName}')");
    if (!this._Synchronizers.TryAdd(synchronizer.ServiceGUID, synchronizer))
      throw new KernelException($"Попытка зарегистрировать службу синхронизации серверов с неуникальным идентификатором (ServiceGuid={synchronizer.ServiceGUID}', ServiceName='{synchronizer.ServiceName}')");
    synchronizer.Manager = (IServerSynchronizersManager) this;
  }

  public void UnregisterSynchronizer(IServerSynchronizer synchronizer)
  {
    if (synchronizer == null)
      throw new ArgumentNullException(nameof (synchronizer));
    IServerSynchronizer serverSynchronizer;
    if (!this._Synchronizers.TryGetValue(synchronizer.ServiceGUID, out serverSynchronizer) || serverSynchronizer != synchronizer || !this._Synchronizers.TryRemove(synchronizer.ServiceGUID, out serverSynchronizer) || serverSynchronizer != synchronizer)
      return;
    synchronizer.Manager = (IServerSynchronizersManager) null;
  }

  public void AddSynchronizerEvent(SynchonizerEventProperties eventProps, IDbManager db)
  {
    if (!this._SyncEnabled)
      return;
    string[] strArray;
    if (eventProps.DestinationServer == string.Empty)
      strArray = this._Servers.GetAliveServers();
    else
      strArray = new string[1]
      {
        eventProps.DestinationServer
      };
    IDbDataParameter dbDataParameter1 = db.Parameter("srcServer", (object) this._Servers.ServerName);
    IDbDataParameter dbDataParameter2 = db.Parameter("guidPar", (object) eventProps.Guid);
    IDbDataParameter dbDataParameter3 = db.Parameter("strInfo", (object) eventProps.StringInfo);
    IDbDataParameter dbDataParameter4 = db.Parameter("delOnStart", (object) (eventProps.DeleteOnStart ? 1 : 0));
    for (int index = 0; index < strArray.Length; ++index)
    {
      IDbDataParameter dbDataParameter5 = db.Parameter("dstServer", (object) strArray[index]);
      if (db.DataProvider.Name == "Sql")
      {
        db.ExecuteNonQuery($"INSERT INTO IMS_ISB (F_SERVER_SRC, F_SERVER_DST, F_GUID, F_STRING_INFO, F_DATE, F_DELETE_ON_START) VALUES (:srcServer, :dstServer, :guidPar, :strInfo, {db.DataProvider.Now}, :delOnStart)", dbDataParameter1, dbDataParameter5, dbDataParameter2, dbDataParameter3, dbDataParameter4);
      }
      else
      {
        long num = db.DataProvider.NextGeneratorValue("IMS_ISB_GEN", db);
        db.ExecuteNonQuery($"INSERT INTO IMS_ISB (F_KEY, F_SERVER_SRC, F_SERVER_DST, F_GUID, F_STRING_INFO, F_DATE, F_DELETE_ON_START) VALUES (:keyID, :srcServer, :dstServer, :guidPar, :strInfo, {db.DataProvider.Now}, :delOnStart)", db.Parameter("keyID", (object) num), dbDataParameter1, dbDataParameter5, dbDataParameter2, dbDataParameter3, dbDataParameter4);
      }
    }
  }
}
