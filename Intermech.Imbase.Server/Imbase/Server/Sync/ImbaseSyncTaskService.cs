// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.ImbaseSyncTaskService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.DataBase;
using Intermech.Imbase.Server.Sync.Helper;
using Intermech.Imbase.Server.Sync.Records;
using Intermech.Imbase.Server.Sync.Services;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Sync;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class ImbaseSyncTaskService : 
  BaseSyncTaskService,
  IImbaseSyncService,
  IServiceForBackgroundTask
{
  private DateTime _timePoint;
  private DataTable _folders;

  protected override void BeforeTaskExecute(IUserSession session, IDataBase sourceDB)
  {
    this.AddIndexesForEventLogTable(sourceDB);
    this._timePoint = this.SyncParams.TimePoint;
    VisibleAttHelper.Init(session);
    PumpSettings.Init(this.SyncParams.PumpSettingsPath, sourceDB);
    this._folders = (DataTable) null;
    ApplicationServices.Container.GetService<IChangedTableIndexer>().Clear();
  }

  private void AddIndexesForEventLogTable(IDataBase sourceDb)
  {
    try
    {
      sourceDb.ExecuteNonQuery("CREATE INDEX IM_EVENTS_DCI ON IM_EVENTS(F_DATE, F_CODE)");
    }
    catch
    {
    }
  }

  protected override List<EventRecord> GetEventRecs()
  {
    List<EventRecord> list = this.GetEventsDt(this._timePoint, this.SourceDb).AsEnumerable().Select<DataRow, EventRecord>((System.Func<DataRow, EventRecord>) (x => new EventRecord(x))).ToList<EventRecord>();
    int count = list.Count;
    for (int index1 = 0; index1 < count; ++index1)
    {
      if (list[index1].Code == 120)
      {
        for (int index2 = index1 + 1; index2 < count; ++index2)
        {
          if (list[index2].Code == 200 && list[index1].Table == list[index2].Table)
          {
            EventRecord eventRecord = list[index2];
            list.RemoveAt(index2);
            list.Insert(index1, eventRecord);
            ++index1;
            break;
          }
        }
      }
    }
    return list;
  }

  protected override int GetTaskCount(IUserSession session)
  {
    if (!this.SyncParams.DeleteDuplicates)
      return base.GetTaskCount(session);
    this._folders = this.GetFoldersDataTable(session);
    return this.EventRecords.Count + this._folders.Rows.Count;
  }

  protected override void AfterTaskExecute(
    IUserSession session,
    BaseTaskForBackgroundTaskService task)
  {
    this.UpdateTableIndexes(session, task);
    if (!this.SyncParams.DeleteDuplicates)
      return;
    this.DeleteDublicates(session, task, this._folders);
  }

  private void UpdateTableIndexes(IUserSession session, BaseTaskForBackgroundTaskService task)
  {
    IChangedTableIndexer service1 = ApplicationServices.Container.GetService<IChangedTableIndexer>();
    long[] changedTableIds = service1.GetChangedTableIds();
    if (changedTableIds == null || changedTableIds.Length == 0)
      return;
    IEventLoggerService service2 = ApplicationServices.Container.GetService<IEventLoggerService>();
    IImbaseIndexingService service3 = ApplicationServices.Container.GetService<IImbaseIndexingService>();
    service2.AddMessage(task.TaskGuid, EventType.Text, "Обновление индексов для таблиц Imbase");
    foreach (long num in changedTableIds)
    {
      if (this.IsProcessStoped(task))
        throw new BaseSyncTaskService.StopTaskException();
      if (session.GetObjectInfo(num).Empty && num < 0L)
        num = -num;
      try
      {
        service3.UpdateAfterTableCheckIn(session.SessionGUID, num);
      }
      catch (Exception ex)
      {
        string eventText = $"В время обновления индексов таблицы ID = {num} произошла ошибка:{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}";
        service2.AddMessage(task.TaskGuid, EventType.Warning, eventText);
      }
    }
    service1.Clear();
  }

  protected override void OnFinallyTask(IUserSession session)
  {
    if (!(this.SyncParams.TimePoint != this._timePoint))
      return;
    this.SyncParams.TimePoint = this._timePoint;
    this.ImbaseParams.SetCommonParams(session.SessionGUID, this.CommonParams);
  }

  protected override void AfterRecProcess(EventRecord rec)
  {
    if (!(rec.Date > this._timePoint))
      return;
    this._timePoint = rec.Date;
  }

  private DataTable GetEventsDt(DateTime timePoint, IDataBase sourceDB)
  {
    string sql = string.Format("SELECT * FROM {0} A WHERE {1} > :timePoint AND {2} IN ({3}) ORDER BY {1} ASC", (object) "IM_EVENTS", (object) "F_DATE", (object) "F_CODE", (object) this.GetCodeHandlersStr());
    return sourceDB.ExecuteDataTable(sql, sourceDB.CreateParameter(nameof (timePoint), (object) timePoint));
  }

  private string GetCodeHandlersStr()
  {
    int[] handledCodes = this.Handlers.GetHandledCodes();
    return handledCodes != null && handledCodes.Length != 0 ? string.Join<int>(",", (IEnumerable<int>) handledCodes) : throw new Exception("Не зарегистрирован ни один обработчик событий!");
  }

  private DataTable GetFoldersDataTable(IUserSession session)
  {
    return session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID)).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(0, RelationalOperators.ConsistFromType, (object) MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID), LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 }));
  }

  private void DeleteDublicates(
    IUserSession session,
    BaseTaskForBackgroundTaskService task,
    DataTable dtFolders)
  {
    if (dtFolders == null || dtFolders.Rows.Count <= 0)
      return;
    IEventLoggerService eventLoggerService = ApplicationServices.Container.GetService<IEventLoggerService>();
    IDBObjectCollection objectCollection = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseTableRefTypeGUID));
    eventLoggerService.AddMessage(task.TaskGuid, EventType.Text, "Удаление дубликатов ярлыков");
    for (int index = 0; index < dtFolders.Rows.Count; ++index)
    {
      if (this.IsProcessStoped(task))
        throw new BaseSyncTaskService.StopTaskException();
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(0, RelationalOperators.EntersIn, (object) Convert.ToInt64(dtFolders.Rows[index][0]), LogicalOperators.AND, 0, false)
      };
      object[] columns = new object[3]
      {
        (object) -2,
        (object) -13,
        (object) session.IdentHelper.NameID
      };
      foreach (KeyValuePair<string, List<Tuple<long, DateTime>>> keyValuePair in objectCollection.Select(new DBRecordSetParams(conditions, columns)).AsEnumerable().GroupBy<DataRow, string, Tuple<long, DateTime>>((System.Func<DataRow, string>) (x => Convert.ToString(x[2]).ToUpper()), (System.Func<DataRow, Tuple<long, DateTime>>) (y => new Tuple<long, DateTime>(Convert.ToInt64(y[0]), Convert.ToDateTime(y[1])))).ToDictionary<IGrouping<string, Tuple<long, DateTime>>, string, List<Tuple<long, DateTime>>>((System.Func<IGrouping<string, Tuple<long, DateTime>>, string>) (x => x.Key), (System.Func<IGrouping<string, Tuple<long, DateTime>>, List<Tuple<long, DateTime>>>) (y => y.ToList<Tuple<long, DateTime>>())))
      {
        if (keyValuePair.Value.Count > 1)
        {
          DateTime maxDate = keyValuePair.Value.Max<Tuple<long, DateTime>, DateTime>((System.Func<Tuple<long, DateTime>, DateTime>) (x => x.Item2));
          keyValuePair.Value.Where<Tuple<long, DateTime>>((System.Func<Tuple<long, DateTime>, bool>) (x => x.Item2 != maxDate)).Select<Tuple<long, DateTime>, long>((System.Func<Tuple<long, DateTime>, long>) (x => x.Item1)).ToList<long>().ForEach((Action<long>) (x =>
          {
            try
            {
              IDBObject dbObject = session.GetObject(x);
              eventLoggerService.AddMessage(task.TaskGuid, EventType.Text, $"Удаление дубликата ссылки на таблицу Imbase {dbObject.NameInMessages}[{dbObject.ObjectID}] от {dbObject.CreateDate}");
              dbObject.Delete(0L);
            }
            catch (Exception ex)
            {
              eventLoggerService.AddException(task.TaskGuid, ex);
            }
          }));
        }
      }
      task.Next();
    }
  }
}
