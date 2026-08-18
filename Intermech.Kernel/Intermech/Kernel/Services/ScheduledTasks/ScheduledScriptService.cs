// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.ScheduledScriptService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.CustomServices;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading;


namespace Intermech.Kernel.Services.ScheduledTasks;

internal class ScheduledScriptService : IScheduledScriptService
{
  private const int SynchTimerPeriod = 30;
  internal const string ScheduledScriptLog = "scheduled_scripts.log";
  private const string ScheduledTaskSessionName = "ScheduledScriptTask";
  private Timer _synchTimer;
  private readonly IDictionary<ScheduledScriptInfo, ScheduledScriptTask> _taskList = (IDictionary<ScheduledScriptInfo, ScheduledScriptTask>) new ConcurrentDictionary<ScheduledScriptInfo, ScheduledScriptTask>();

  private void InitializeData()
  {
    this._synchTimer = new Timer(new TimerCallback(this.DoSynchTimedEvent), (object) null, 10000, 1800000);
    IEventLogHelper service = ServiceUtils.GetService<IEventLogHelper>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    service.AfterCacheReload += new Intermech.Interfaces.Server.CacheReloadHandler(this.CacheReloadHandler);
  }

  private void SynchScriptData()
  {
    lock (this)
    {
      IUserSession session = (IUserSession) null;
      try
      {
        session = ScheduledScriptService.GetSession(nameof (SynchScriptData));
        List<ScheduledScriptInfo> dbScriptList;
        if (!ScheduledScriptService.LoadScriptFromDataBase(session, out dbScriptList))
          return;
        foreach (ScheduledScriptInfo scriptInfo in dbScriptList)
          this.UpdateScript(session, scriptInfo);
        List<ScheduledScriptInfo> resultData1;
        if (GenericListHelper.GetDifference<ScheduledScriptInfo>((IList<ScheduledScriptInfo>) dbScriptList, (IList<ScheduledScriptInfo>) new List<ScheduledScriptInfo>((IEnumerable<ScheduledScriptInfo>) this._taskList.Keys), GenericListHelper.SearchMode.smNotExistInB, out resultData1))
        {
          foreach (ScheduledScriptInfo scriptInfo in resultData1)
            this.RegisterScript(session, scriptInfo);
        }
        List<ScheduledScriptInfo> resultData2;
        if (!GenericListHelper.GetDifference<ScheduledScriptInfo>((IList<ScheduledScriptInfo>) dbScriptList, (IList<ScheduledScriptInfo>) new List<ScheduledScriptInfo>((IEnumerable<ScheduledScriptInfo>) this._taskList.Keys), GenericListHelper.SearchMode.smNotExistInA, out resultData2))
          return;
        foreach (ScheduledScriptInfo scriptInfo in resultData2)
          this.RemoveScript(session, scriptInfo, false);
      }
      catch (Exception ex)
      {
        ServiceUtils.GetService<IEventLogHelper>((object) ApplicationServices.Container, false)?.TraceExeption(LocalizationHolder.rm.GetString("Kernel_1170"), ex, string.Empty);
      }
      finally
      {
        session?.Logout(nameof (SynchScriptData));
      }
    }
  }

  private static bool LoadScriptFromDataBase(
    IUserSession session,
    out List<ScheduledScriptInfo> dbScriptList)
  {
    dbScriptList = new List<ScheduledScriptInfo>();
    Guid guid = new Guid("cadd94cd-306c-11d8-b4e9-00304f19f545");
    if (MetaDataHelper.GetObjectType(guid) == null)
      return false;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -12, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -50, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    });
    DataTable dataTable = session.GetObjectCollection(guid).Select(paramSet);
    if (dataTable != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        dbScriptList.Add(new ScheduledScriptInfo(new Guid(Convert.ToString(row[1])), Convert.ToString(row[2])));
    }
    return dbScriptList.Count != 0;
  }

  private void RegisterScript(IUserSession session, ScheduledScriptInfo scriptInfo)
  {
    if (scriptInfo == null || this._taskList.ContainsKey(scriptInfo))
      return;
    ScheduledScriptTask timedService = new ScheduledScriptTask(scriptInfo);
    this._taskList[scriptInfo] = timedService;
    ServiceUtils.GetService<IDBTimedEvents>((object) ApplicationServices.Container, false)?.RegisterService((object) timedService);
  }

  private void RemoveScript(
    IUserSession session,
    ScheduledScriptInfo scriptInfo,
    bool exceptionOnError = true)
  {
    ScheduledScriptTask timedService;
    if (scriptInfo == null || !this._taskList.TryGetValue(scriptInfo, out timedService))
      return;
    int num = 0;
    DataTable eventsTable = ServiceUtils.GetService<IDBTimedEvents>((object) ApplicationServices.Container, false) is ITimedEventsSheduler service1 ? service1.GetEventsTable(session.SessionGUID) : (DataTable) null;
    if (eventsTable != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) eventsTable.Rows)
      {
        if (new TimedEventProperties(row).ServiceGuid == scriptInfo.ScriptGuid)
        {
          ++num;
          break;
        }
      }
    }
    if (num != 0)
    {
      if (exceptionOnError)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1167"), (object) scriptInfo.ScriptName, (object) scriptInfo.ScriptGuid, (object) num));
    }
    else
    {
      if (ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, false) is DBTimedEvents service2)
        service2.UnregisterService((object) timedService);
      this._taskList.Remove(scriptInfo);
    }
  }

  private void UpdateScript(IUserSession session, ScheduledScriptInfo scriptInfo)
  {
    ScheduledScriptTask scheduledScriptTask;
    if (scriptInfo == null || !this._taskList.TryGetValue(scriptInfo, out scheduledScriptTask) || !(scheduledScriptTask.ScriptInfo.ScriptName != scriptInfo.ScriptName))
      return;
    scheduledScriptTask.ScriptInfo.ScriptName = scriptInfo.ScriptName;
  }

  private ScheduledScriptService() => this.InitializeData();

  void IScheduledScriptService.RegisterScript(Guid sessionGuid, ScheduledScriptInfo scriptInfo)
  {
    this.RegisterScript(UserSession.GetSessionByID(sessionGuid), scriptInfo);
  }

  void IScheduledScriptService.RemoveScript(
    Guid sessionGuid,
    ScheduledScriptInfo scriptInfo,
    bool exceptionOnError)
  {
    this.RemoveScript(UserSession.GetSessionByID(sessionGuid), scriptInfo, exceptionOnError);
  }

  void IScheduledScriptService.UpdateScript(Guid sessionGuid, ScheduledScriptInfo scriptInfo)
  {
    this.UpdateScript(UserSession.GetSessionByID(sessionGuid), scriptInfo);
  }

  void IScheduledScriptService.ExecuteScript(Guid sessionGuid, ScheduledScriptInfo scriptInfo)
  {
    if (scriptInfo == null)
      throw new ArgumentNullException(nameof (scriptInfo));
    new ScheduledScriptExecutor(scriptInfo).Execute(UserSession.GetSessionByID(sessionGuid));
  }

  private void DoSynchTimedEvent(object state) => this.SynchScriptData();

  private void CacheReloadHandler(IDbManager dbManager) => this.SynchScriptData();

  public static void RegisterService()
  {
    ScheduledScriptService serviceInstance = new ScheduledScriptService();
    if (ServerServices.GetService(typeof (IScheduledScriptService)) == null)
      ServerServices.AddService(typeof (IScheduledScriptService), (object) serviceInstance);
    ICustomServices service = ServiceUtils.GetService<ICustomServices>((object) ApplicationServices.Container, false);
    if (service == null || service.GetService(typeof (IScheduledScriptService)) != null)
      return;
    service.AddService(typeof (IScheduledScriptService), (object) serviceInstance);
  }

  private static IUserSession GetSession(string sessionName)
  {
    return ServiceUtils.GetService<IDBTimedEvents>((object) ApplicationServices.Container, false)?.GetSystemSessionPermanentClone(sessionName);
  }
}
