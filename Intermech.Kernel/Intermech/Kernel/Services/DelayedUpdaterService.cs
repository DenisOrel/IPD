// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.DelayedUpdaterService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.DelayedNotifications;
using Intermech.Interfaces.Server.GlobalIndex;
using Intermech.Interfaces.Snapshots;
using Intermech.Kernel.GlobalIndex;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading;


namespace Intermech.Kernel.Services;

public sealed class DelayedUpdaterService : IDelayedUpdaterService
{
  private UserSession _Session;
  private UserSession _BigSession;
  private ConcurrentQueue<AttrHistoryProperties> AttrHistoryQueue = new ConcurrentQueue<AttrHistoryProperties>();
  private ConcurrentQueue<EventlogProperties> EventlogQueue = new ConcurrentQueue<EventlogProperties>();
  private long _СurrentEventID;
  private ConcurrentDictionary<long, long> eventIDConverter = new ConcurrentDictionary<long, long>();
  private ConcurrentDictionary<long, EventlogProperties> closeEventsList = new ConcurrentDictionary<long, EventlogProperties>();
  private ConcurrentQueue<IndexQueueProperties> IndexQueue = new ConcurrentQueue<IndexQueueProperties>();
  private ConcurrentQueue<long> autoSnapshotsQueue = new ConcurrentQueue<long>();
  private GlobalIndexService _IndexService;
  private ConcurrentQueue<DelayedNotification> DelayedNotificationsQueue = new ConcurrentQueue<DelayedNotification>();
  private ConcurrentQueue<SearchQueryProperties> SearchHistoryQueue = new ConcurrentQueue<SearchQueryProperties>();
  private volatile bool _needUpdateRoles;

  public DelayedUpdaterService()
  {
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    this._Session = service.GetSystemSessionPermanentClone("DelayedUpdaterService.Session") as UserSession;
    this._BigSession = service.GetSystemSessionPermanentClone("DelayedUpdaterService.BigSession") as UserSession;
    this.Start();
  }

  private void Start()
  {
    new Thread(new ThreadStart(this.ProcessCriticalData))
    {
      Name = "DelayedUpdaterService critical thread",
      IsBackground = true
    }.Start();
    new Thread(new ThreadStart(this.ProcessBigData))
    {
      Name = "DelayedUpdaterService big data thread",
      IsBackground = true
    }.Start();
    new Thread(new ThreadStart(this.ProcessDelayedNotifications))
    {
      Name = "DelayedUpdaterService notifications thread",
      IsBackground = true
    }.Start();
  }

  internal void AddToAutoSnapshotsQueue(long objectID) => this.autoSnapshotsQueue.Enqueue(objectID);

  internal void AddToAutoSnapshotsQueue(List<long> objectsList)
  {
    for (int index = 0; index < objectsList.Count; ++index)
      this.AddToAutoSnapshotsQueue(objectsList[index]);
  }

  internal void ProcessAutoSnapshotsQueue()
  {
    if (this.autoSnapshotsQueue.Count <= 0)
      return;
    IDBSnapshotCollection snapshotCollection = this._BigSession.GetSnapshotCollection();
    List<long> longList = new List<long>(this.autoSnapshotsQueue.Count);
    long result;
    while (this.autoSnapshotsQueue.TryDequeue(out result))
    {
      if (longList.IndexOf(result) < 0)
      {
        IDBObject dbObject = this._BigSession.GetObject(result, false);
        if (dbObject == null && result < 0L)
          dbObject = this._BigSession.GetObject(-result, false);
        if (dbObject != null)
        {
          if (!dbObject.IsCreationMode)
          {
            try
            {
              snapshotCollection.Create(dbObject.ObjectID, "Сохранение изменений от " + dbObject.GetAttributeByID(this._BigSession.IdentHelper.ModifyContentDateID).AsString, "cad005aa-306c-11d8-b4e9-00304f19f545");
            }
            catch (Exception ex)
            {
              this._BigSession.EventLogHelper.AddToTrace($"Ошибка создания итерации для объекта '{dbObject.NameInMessages}': {ex.Message}", Consts.traceAlways, "DelayedService.log");
            }
          }
        }
        longList.Add(result);
      }
    }
  }

  internal void AddAttrToIndexQueue(IndexQueueProperties attrProps)
  {
    this.IndexQueue.Enqueue(attrProps);
  }

  internal void AddAttrToIndexQueue(List<IndexQueueProperties> attrPropsList)
  {
    for (int index = 0; index < attrPropsList.Count; ++index)
      this.AddAttrToIndexQueue(attrPropsList[index]);
  }

  private void ProcessAttrIndexQueue()
  {
    if (this._IndexService == null)
      this._IndexService = ServerServices.GetService(typeof (IGlobalIndexService)) as GlobalIndexService;
    if (this._IndexService == null)
      return;
    IndexQueueProperties result;
    while (this.IndexQueue.TryDequeue(out result))
    {
      if (result.Action == ActionType.CheckOut)
        this._IndexService.CheckOutIndex(result.ObjectID, this._BigSession);
      else if (result.Action == ActionType.CheckIn)
        this._IndexService.CheckInIndex(result.ObjectID, this._BigSession);
      else
        this._IndexService.IndexText(result, this._BigSession);
    }
  }

  internal void AddAttrHistory(AttrHistoryProperties attrProps)
  {
    this.AttrHistoryQueue.Enqueue(attrProps);
  }

  internal void AddAttrHistory(List<AttrHistoryProperties> attrPropsList)
  {
    for (int index = 0; index < attrPropsList.Count; ++index)
      this.AddAttrHistory(attrPropsList[index]);
  }

  private void ProcessAttrHistoryQueue()
  {
    IDbManager dataManager = this._Session.DataManager;
    string str1;
    string str2;
    if (dataManager.DataProvider.Name == "Sql")
    {
      str2 = str1 = string.Empty;
    }
    else
    {
      str2 = "F_KEY, ";
      str1 = dataManager.DataProvider.InsertGeneratorValueString("IMS_ATTR_HISTORY_GEN") + ",";
    }
    bool flag = false;
    AttrHistoryProperties result;
    while (this.AttrHistoryQueue.TryDequeue(out result))
    {
      dataManager.AddBatchSQL($"INSERT INTO IMS_ATTR_HISTORY ({str2}F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_RELATION_TYPE, F_USER_ID, F_SET_DATE, F_ID, F_STATUS, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) VALUES ({str1}:aID2, :oType2, :rType2, :uID2, :setDate2, :id12, :status, :intVal2, :strVal2, :dblVal2, :datVal2)", new DbCommandParam[11]
      {
        dataManager.BatchParameter("aID2", DbType.Int32, (object) result.AttributeID),
        dataManager.BatchParameter("oType2", DbType.Int32, (object) result.ObjectType),
        dataManager.BatchParameter("rType2", DbType.Int32, (object) result.RelationType),
        dataManager.BatchParameter("uID2", DbType.Int64, (object) result.UserID),
        dataManager.BatchParameter("setDate2", DbType.Date, (object) result.SetDate),
        dataManager.BatchParameter("id12", DbType.Int64, (object) result.ID),
        dataManager.BatchParameter("status", DbType.Int32, (object) 0),
        dataManager.BatchParameter("intVal2", DbType.Int64, result.IntValue),
        dataManager.BatchParameter("strVal2", DbType.String, result.StrValue),
        dataManager.BatchParameter("dblVal2", DbType.Double, result.DoubleValue),
        dataManager.BatchParameter("datVal2", DbType.Date, result.DateValue)
      });
      flag = true;
    }
    if (!flag)
      return;
    dataManager.BeginTransaction();
    try
    {
      dataManager.ExecuteBatchSQL();
      dataManager.Commit();
    }
    catch
    {
      dataManager.Rollback();
      throw;
    }
  }

  internal long NextEventID => Interlocked.Increment(ref this._СurrentEventID);

  internal long AddEvent(EventlogProperties eventProps)
  {
    eventProps.EventID = this.NextEventID;
    this.EventlogQueue.Enqueue(eventProps);
    return eventProps.EventID;
  }

  internal void AddEvents(List<EventlogProperties> eventPropsList)
  {
    for (int index = 0; index < eventPropsList.Count; ++index)
    {
      if (eventPropsList[index].EventKind == EventPropertiesType.CloseEventExt || eventPropsList[index].EventKind == EventPropertiesType.CloseEventSimple)
        this.CloseEvent(eventPropsList[index]);
      else
        this.EventlogQueue.Enqueue(eventPropsList[index]);
    }
  }

  private void ProcessEventlogQueue()
  {
    IDbManager dataManager = this._Session.DataManager;
    EventlogProperties result;
    while (this.EventlogQueue.TryDequeue(out result))
    {
      if (result.EventInBase)
      {
        if (result.EventKind == EventPropertiesType.CloseEventExt)
        {
          if (result.RelationID != 0L)
            dataManager.ExecuteNonQuery("UPDATE IMS_EVENTLOG SET F_END_DATE = :endDate, F_OBJECT_ID = :objID, F_RELATION_ID = :relID, F_CATEGORY_ID = :catID, F_OBJECT_NAME = :objName, F_NOTE = :note, F_AUDIT_TYPE = :auType, F_USER_ID = :usrID WHERE F_EVENT_ID = :evID", dataManager.Parameter("endDate", (object) result.EndDate), dataManager.Parameter("objID", (object) result.ObjectID), dataManager.Parameter("relID", (object) result.RelationID), dataManager.Parameter("catID", (object) result.CategoryID), dataManager.Parameter("objName", (object) result.ObjectName), dataManager.Parameter("note", (object) result.Note), dataManager.Parameter("auType", (object) (int) result.AuditType), dataManager.Parameter("usrID", (object) result.UserID), dataManager.Parameter("evID", (object) result.EventID));
          else
            dataManager.ExecuteNonQuery("UPDATE IMS_EVENTLOG SET F_END_DATE = :endDate, F_OBJECT_ID = :objID, F_CATEGORY_ID = :catID, F_OBJECT_NAME = :objName, F_NOTE = :note, F_AUDIT_TYPE = :auType, F_USER_ID = :usrID WHERE F_EVENT_ID = :evID", dataManager.Parameter("endDate", (object) result.EndDate), dataManager.Parameter("objID", (object) result.ObjectID), dataManager.Parameter("catID", (object) result.CategoryID), dataManager.Parameter("objName", (object) result.ObjectName), dataManager.Parameter("note", (object) result.Note), dataManager.Parameter("auType", (object) (int) result.AuditType), dataManager.Parameter("usrID", (object) result.UserID), dataManager.Parameter("evID", (object) result.EventID));
        }
        else if (result.Note != "$NO$")
          dataManager.ExecuteNonQuery("UPDATE IMS_EVENTLOG SET F_END_DATE = :endDate, F_NOTE = :note, F_AUDIT_TYPE = :auType WHERE F_EVENT_ID = :evID", dataManager.Parameter("endDate", (object) result.EndDate), dataManager.Parameter("note", (object) result.Note), dataManager.Parameter("auType", (object) (int) result.AuditType), dataManager.Parameter("evID", (object) result.EventID));
        else
          dataManager.ExecuteNonQuery("UPDATE IMS_EVENTLOG SET F_END_DATE = :endDate, F_AUDIT_TYPE = :auType WHERE F_EVENT_ID = :evID", dataManager.Parameter("endDate", (object) result.EndDate), dataManager.Parameter("auType", (object) (int) result.AuditType), dataManager.Parameter("evID", (object) result.EventID));
      }
      else
      {
        long num = 0;
        EventlogProperties props;
        if (result.EventKind == EventPropertiesType.AddEvent && this.closeEventsList.TryRemove(result.EventID, out props))
          result.CloseEvent(props);
        dataManager.ExecuteSpNonQuery("IMS_ADD_EVENTLOG_EX", dataManager.Parameter("inCATEGORY_TYPE", (object) result.CategoryType), dataManager.Parameter("inCATEGORY_ID", (object) result.CategoryID), dataManager.Parameter("inOBJECT_ID", (object) result.ObjectID), dataManager.Parameter("inRELATION_ID", (object) result.RelationID), dataManager.Parameter("inOBJECT_NAME", (object) result.ObjectName), dataManager.Parameter("inUSER_ID", (object) result.UserID), dataManager.Parameter("inCOMPUTER_NAME", (object) result.ComputerName), dataManager.Parameter("inNOTE", (object) result.Note), dataManager.Parameter("inEVENT_TYPE", (object) (int) result.EventType), dataManager.Parameter("inAUDIT_TYPE", (object) (int) result.AuditType), dataManager.Parameter("inBEGIN_DATE", (object) result.BeginDate), dataManager.Parameter("inEND_DATE", (object) result.EndDate), dataManager.OutputParameter("outEVENT_ID", (object) num));
        if (result.EventKind == EventPropertiesType.AddEvent)
          this.eventIDConverter.TryAdd(result.EventID, Convert.ToInt64(dataManager.GetOutputParameterValue("outEVENT_ID")));
      }
    }
  }

  internal long CloseEvent(EventlogProperties eventProps)
  {
    long eventId;
    if (this.eventIDConverter.TryRemove(eventProps.EventID, out eventId))
    {
      eventProps.EventID = eventId;
      eventProps.EventInBase = true;
      this.EventlogQueue.Enqueue(eventProps);
    }
    else
    {
      if (!this.closeEventsList.TryAdd(eventProps.EventID, eventProps) && this.closeEventsList.TryRemove(eventProps.EventID, out EventlogProperties _))
        this.closeEventsList.TryAdd(eventProps.EventID, eventProps);
      eventId = eventProps.EventID;
    }
    return eventId;
  }

  internal void AddDelayedNotification(DelayedNotification notify)
  {
    this.DelayedNotificationsQueue.Enqueue(notify);
  }

  internal void AddDelayedNotifications(DelayedNotification[] notifies)
  {
    for (int index = 0; index < notifies.Length; ++index)
      this.AddDelayedNotification(notifies[index]);
  }

  private void ProcessDelayedNotifications()
  {
    while (true)
    {
      try
      {
        DelayedNotification result;
        while (this.DelayedNotificationsQueue.TryDequeue(out result))
        {
          if (this.DelayedNotificationEvent != null)
            this.DelayedNotificationEvent(result);
        }
        Thread.Sleep(1000);
      }
      catch (Exception ex)
      {
        IEventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
        service.AddToTrace("Ошибка потока обработки отложенных событий: " + ex.Message, Consts.traceAlways, "DelayedService.log");
        service.AddToTrace(ex.StackTrace, Consts.traceAlways, "DelayedService.log");
        Thread.Sleep(1000);
      }
    }
  }

  public event DelayedNotificationHandler DelayedNotificationEvent;

  internal void AddSearchQuery(SearchQueryProperties qProps)
  {
    this.SearchHistoryQueue.Enqueue(qProps);
  }

  private void ProcessSearchQueriesHistory()
  {
    if (this._IndexService == null)
      return;
    IDbManager dataManager = this._BigSession.DataManager;
    SearchQueryProperties result;
    while (this.SearchHistoryQueue.TryDequeue(out result))
      this._IndexService.SaveSearchQuery(result, dataManager);
  }

  public void ReloadRolesCache() => this._needUpdateRoles = true;

  internal void ClearCache()
  {
    this.closeEventsList.Clear();
    this.eventIDConverter.Clear();
  }

  private void ProcessCriticalData()
  {
    while (true)
    {
      try
      {
        this.ProcessEventlogQueue();
        this.ProcessAttrHistoryQueue();
        Thread.Sleep(1000);
      }
      catch (Exception ex)
      {
        this._Session.EventLogHelper.AddToTrace("Ошибка потока обработки критических данных: " + ex.Message, Consts.traceAlways, "DelayedService.log");
        this._Session.EventLogHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, "DelayedService.log");
        Thread.Sleep(1000);
      }
    }
  }

  private void ProcessBigData()
  {
    while (true)
    {
      try
      {
        this.ProcessAttrIndexQueue();
        this.ProcessAutoSnapshotsQueue();
        this.ProcessSearchQueriesHistory();
        if (this._needUpdateRoles)
        {
          this._needUpdateRoles = false;
          if (ServerServices.GetService(typeof (IRolesCache)) is IRolesCache service)
            service.ReloadRoles((IUserSession) this._BigSession, true);
        }
        Thread.Sleep(1000);
      }
      catch (Exception ex)
      {
        this._BigSession.EventLogHelper.AddToTrace("Ошибка потока обработки больших данных: " + ex.Message, Consts.traceAlways, "DelayedService.log");
        this._BigSession.EventLogHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, "DelayedService.log");
        Thread.Sleep(1000);
      }
    }
  }
}
