// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.BaseSyncTaskService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.DataBase;
using Intermech.Imbase.Server.Sync.Records;
using Intermech.Imbase.Server.Sync.Services;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Imbase.Params.CommonParams;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class BaseSyncTaskService : BackgroundTaskService
{
  protected CodeHandlersFactory Handlers { get; private set; }

  protected ImbaseSyncParams SyncParams { get; private set; }

  protected IDataBase SourceDb { get; private set; }

  protected List<EventRecord> EventRecords { get; private set; }

  protected IImbaseParamsService ImbaseParams { get; private set; }

  protected ImbaseCommonParams CommonParams { get; private set; }

  protected override void StartProcess(Guid taskGuid, object inputData)
  {
    BaseTaskForBackgroundTaskService task = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (task == null)
      return;
    IEventLoggerService service1 = ApplicationServices.Container.GetService<IEventLoggerService>();
    service1.HandlerEvent += new HandlerEventDelegate(this.EventLoggerService_HandlerEvent);
    service1.HandlerException += new HandlerExceptionDelegate(this.EventLoggerService_HandlerException);
    IUserSession session = (IUserSession) null;
    this.ImbaseParams = ServiceUtils.GetService<IImbaseParamsService>((object) ServerServices.ServiceContainer, true);
    this.CommonParams = this.ImbaseParams.CommonParams;
    this.SyncParams = this.CommonParams.ImbaseSyncParams;
    this.Handlers = this.RegisterHandlers(taskGuid);
    try
    {
      service1.AddMessage(taskGuid, EventType.Text, "Старт процесса синхронизации");
      task.Running = true;
      session = this.GetSystemSession();
      this.SourceDb = this.GetSourceDb(this.SyncParams);
      this.BeforeTaskExecute(session, this.SourceDb);
      this.EventRecords = this.GetEventRecs();
      task.CountElements = this.GetTaskCount(session);
      List<EventRecord> eventRecords = this.EventRecords;
      // ISSUE: explicit non-virtual call
      if ((eventRecords != null ? (__nonvirtual (eventRecords.Count) > 0 ? 1 : 0) : 0) != 0)
      {
        service1.AddMessage(taskGuid, EventType.Text, $"Найдено: {this.EventRecords.Count} записей подлежащих обработке, начиная с {this.SyncParams.TimePoint}");
        this.BeforeRecsProcess();
        foreach (EventRecord eventRecord in this.EventRecords)
        {
          if (this.IsProcessStoped(task))
            throw new BaseSyncTaskService.StopTaskException();
          try
          {
            this.BeforeRecProcess(eventRecord);
            this.Handlers.GetHandler(eventRecord.Code)?.Handle(eventRecord, this.SourceDb, session);
            this.AfterRecProcess(eventRecord);
            task.Next();
          }
          catch (Exception ex)
          {
            if (this.SyncParams.TerminateOnError)
              throw;
            service1.AddException(taskGuid, ex);
          }
        }
        IDelayedEvents service2 = ServiceUtils.GetService<IDelayedEvents>((object) ApplicationServices.Container, true);
        EventRecord[] delayedEvents = service2.GetDelayedEvents();
        if (delayedEvents.Length != 0)
        {
          service1.AddMessage(taskGuid, EventType.Text, "Обработка отложенных событий");
          foreach (EventRecord eventRecord in delayedEvents)
          {
            if (this.IsProcessStoped(task))
              throw new BaseSyncTaskService.StopTaskException();
            try
            {
              this.BeforeRecProcess(eventRecord);
              this.Handlers.GetHandler(eventRecord.Code)?.Handle(eventRecord, this.SourceDb, session);
              this.AfterRecProcess(eventRecord);
            }
            catch (Exception ex)
            {
              if (this.SyncParams.TerminateOnError)
                throw;
              service1.AddException(taskGuid, ex);
            }
          }
          service2.ClearDelayedEvents();
        }
      }
      else
        service1.AddMessage(taskGuid, EventType.Text, "В базе-источнике изменений не обнаружено");
      this.AfterTaskExecute(session, task);
      service1.AddMessage(taskGuid, EventType.Text, "Процесс синхронизации завершен");
    }
    catch (BaseSyncTaskService.StopTaskException ex)
    {
      service1.AddMessage(taskGuid, EventType.Text, LocalizationHolder.rm.GetString("Imbase_Task_Stop"));
    }
    catch (Exception ex)
    {
      service1.AddException(taskGuid, ex);
    }
    finally
    {
      service1.HandlerEvent -= new HandlerEventDelegate(this.EventLoggerService_HandlerEvent);
      service1.HandlerException -= new HandlerExceptionDelegate(this.EventLoggerService_HandlerException);
      if (session != null)
      {
        this.OnFinallyTask(session);
        session.Logout("imbase.sync");
      }
      this.SourceDb.Connection.Close();
      task.Stopped = true;
    }
  }

  private void EventLoggerService_HandlerException(Guid taskGuid, Exception e)
  {
    this.WriteMessage(taskGuid, e);
  }

  private void EventLoggerService_HandlerEvent(Guid taskGuid, EventType type, string eventText)
  {
    this.WriteMessage(taskGuid, type, eventText);
  }

  protected virtual void BeforeTaskExecute(IUserSession session, IDataBase sourceDB)
  {
  }

  protected virtual void AfterTaskExecute(
    IUserSession session,
    BaseTaskForBackgroundTaskService task)
  {
  }

  protected virtual int GetTaskCount(IUserSession session)
  {
    List<EventRecord> eventRecords = this.EventRecords;
    return eventRecords == null ? 0 : eventRecords.Count<EventRecord>();
  }

  protected virtual void OnFinallyTask(IUserSession session)
  {
  }

  protected virtual void BeforeRecProcess(EventRecord rec)
  {
  }

  protected virtual void AfterRecProcess(EventRecord rec)
  {
  }

  protected virtual void BeforeRecsProcess()
  {
  }

  protected virtual List<EventRecord> GetEventRecs() => new List<EventRecord>();

  private CodeHandlersFactory RegisterHandlers(Guid taskGuid)
  {
    CodeHandlersFactory codeHandlersFactory = new CodeHandlersFactory();
    codeHandlersFactory.Register((CodeHandler) new CatalogCodeHandler(taskGuid), 100, 102, 105, 106);
    codeHandlersFactory.Register((CodeHandler) new RecordCodeHandler(taskGuid), 121, (int) sbyte.MaxValue, 120, 122, 126);
    codeHandlersFactory.Register((CodeHandler) new FolderCodeHandler(taskGuid), 141, 143, 147, 140, 142, 146, 145, 148, 149);
    codeHandlersFactory.Register((CodeHandler) new TableCodeHandler(taskGuid, this.SyncParams.TerminateOnError), 200, 203, 210, 207, 202, 205, 208 /*0xD0*/, 216, 217);
    return codeHandlersFactory;
  }

  private IUserSession GetSystemSession()
  {
    return ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, true).GetSystemSessionTemporaryClone("imbase.sync") ?? throw new Exception(LocalizationHolder.rm.GetString("Imbase_NullSession"));
  }

  private IDataBase GetSourceDb(ImbaseSyncParams syncParams)
  {
    IDataBase dataBase = DatabaseHelper.GetDataBase(syncParams.SourceDBParams.BaseType, syncParams.SourceDBParams.ServerName, syncParams.SourceDBParams.DataBaseName, syncParams.SourceDBParams.UserName, syncParams.SourceDBParams.Password);
    return dataBase?.Connection != null ? dataBase : throw new Exception("Указан недопустимый тип БД-источника");
  }

  protected void WriteMessage(
    BaseTaskForBackgroundTaskService task,
    EventType type,
    string eventText)
  {
    task.Result.Messages.Add(new BackgroundTaskMessage($"{DateTime.Now}: {EnumTypeHelper.GetCaption((Enum) type)}: {eventText}"));
  }

  protected void WriteMessage(BaseTaskForBackgroundTaskService task, Exception ex)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (ex.InnerException != null)
    {
      stringBuilder.AppendLine("InnerTrace:");
      stringBuilder.AppendLine(ex.InnerException.StackTrace);
    }
    stringBuilder.AppendLine("StackTrace:");
    stringBuilder.AppendLine(ex.StackTrace);
    this.WriteMessage(task, EventType.Error, $"{ex.Message} {Environment.NewLine} {stringBuilder}");
  }

  protected void WriteMessage(Guid taskGuid, Exception ex)
  {
    BaseTaskForBackgroundTaskService task = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (task == null)
      return;
    this.WriteMessage(task, ex);
  }

  protected void WriteMessage(Guid taskGuid, EventType type, string eventText)
  {
    BaseTaskForBackgroundTaskService task = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (task == null)
      return;
    this.WriteMessage(task, type, eventText);
  }

  internal bool IsProcessStoped(BaseTaskForBackgroundTaskService task)
  {
    while (task.Paused)
    {
      Thread.Sleep(1000);
      if (task.Stopped)
        break;
    }
    return task.Stopped;
  }

  internal class StopTaskException : ApplicationException
  {
  }
}
