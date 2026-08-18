// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ScheduledPublishTasks
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;


namespace Intermech.Kernel.Services.PortalServices;

internal class ScheduledPublishTasks(
  UserSession session,
  string sessionName,
  PortalTasksQueue tasksQueue,
  TaskPriority? taskPriority,
  string name,
  Guid guid) : CustomPortalScheduledTasks(session, sessionName, tasksQueue, taskPriority, name, guid)
{
  private readonly object _syncRoot = new object();

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    lock (this._syncRoot)
    {
      this.AutoUpdate();
      try
      {
        IDBObjectCollection objectCollection = this.Session.GetObjectCollection(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545"));
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
        {
          new ConditionStructure(PortalConsts.attributeTaskStatus, RelationalOperators.Equal, (object) 4, LogicalOperators.AND, 0),
          new ConditionStructure(PortalConsts.attributeTaskTransferEnabled, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.OR, 1),
          new ConditionStructure(PortalConsts.attributeTaskTransferEnabled, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, -1)
        };
        Array values = Enum.GetValues(typeof (TaskType));
        List<int> intList = new List<int>();
        foreach (TaskType taskType in values)
        {
          object[] customAttributes = taskType.GetType().GetField(taskType.ToString()).GetCustomAttributes(typeof (PublishTaskType), false);
          if (customAttributes.Length != 0 && ((PublishTaskType) customAttributes[0]).IsPublish)
            intList.Add((int) taskType);
        }
        if (intList.Count > 0)
          conditionStructureList.Add(new ConditionStructure(PortalConsts.attributeTaskType, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.AND, 0));
        conditionStructureList.Add(new ConditionStructure(PortalConsts.attributeServerName, RelationalOperators.Empty, (object) null, LogicalOperators.OR, 1));
        conditionStructureList.Add(new ConditionStructure(PortalConsts.attributeServerName, RelationalOperators.Equal, (object) EnvironmentConsts.MachineName.ToUpper(), LogicalOperators.AND, -1));
        if (this.priority.HasValue)
          conditionStructureList.Add(new ConditionStructure(PortalConsts.attributePriority, RelationalOperators.Equal, (object) (int) this.priority.Value, LogicalOperators.AND, 0));
        ColumnDescriptor[] columns = new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -2, SortOrders.NONE, -1),
          new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeTaskNo), SortOrders.ASC, 1)
        };
        DataTable dataTable = objectCollection.Select(new DBRecordSetParams(conditionStructureList.ToArray(), columns));
        if (dataTable.Rows.Count == 0)
          return true;
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
          try
          {
            IDBObject taskObject;
            ITask taskById = TasksHelper.GetTaskByID((IUserSession) this.Session, this.tasksQueue, int64, out taskObject);
            IDBAttribute attributeByGuid = taskObject.GetAttributeByGuid(PortalConsts.attributeServerName);
            if (attributeByGuid.AsString != string.Empty)
            {
              if (attributeByGuid.AsString != EnvironmentConsts.MachineName.ToUpper())
                continue;
            }
            attributeByGuid.AsString = EnvironmentConsts.MachineName.ToUpper();
            new TaskWorkspace((IUserSession) this.Session, this.tasksQueue, taskById, taskObject).BeginTask();
          }
          catch (Exception ex)
          {
            CustomPortalScheduledTasks.WriteErrorToTask((IUserSession) this.Session, int64, ex);
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1068"), (object) Helper.FormingLogError(ex)));
        return false;
      }
    }
  }

  private void AutoUpdate()
  {
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    IDBObject dbObject1 = this.Session.GetObject(PortalConsts.selectionAutoPublish);
    IDbManager dataManager = this.Session.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT A.F_OBJECT_ID, B.F_OBJECT_TYPE FROM IMS_SELECTIONS A, IMS_OBJECTS B WHERE A.F_OBJECT_ID = B.F_OBJECT_ID AND A.F_FOLDER_ID = :folder", dataManager.Parameter("folder", (object) dbObject1.ObjectID));
    if (dataTable.Rows.Count <= 0)
      return;
    ICustomPublisherService service = ServerServices.GetService(typeof (ICustomPublisherService)) as ICustomPublisherService;
    IPublishCompositionService customService = (IPublishCompositionService) this.Session.GetCustomService(typeof (IPublishCompositionService));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
      IPublisher publisher;
      string taskName;
      TaskPriority taskPriority;
      if (this.tasksQueue.OnObjectAutoPublishEvent((IUserSession) this.Session, int64, Convert.ToInt32(dataTable.Rows[index][1]), out publisher, out taskName, out taskPriority))
      {
        service.CustomPublish(this.Session.SessionGUID, publisher, string.IsNullOrEmpty(taskName) ? $"Export_task_{Guid.NewGuid()}" : taskName, taskPriority);
      }
      else
      {
        PublishComposition composition = (PublishComposition) null;
        try
        {
          IDBObject dbObject2 = this.Session.GetObject(int64);
          ExtendedPublishOptions options = PublishOptionsHelper.Deserialize(dbObject2);
          if (options != null)
          {
            Guid selectGUID = Guid.NewGuid();
            customService.Select(this.Session.SessionGUID, selectGUID, new List<long>((IEnumerable<long>) new long[1]
            {
              int64
            }), options, PublishType.Autoreplication, true);
            CompositionInfo info;
            for (info = customService.GetInfo(selectGUID); info != null && !info.ErrorPresent && info.Percent < 100; info = customService.GetInfo(selectGUID))
              Thread.Sleep(25);
            if (info.ErrorPresent)
              throw info.ErrorException;
            if (info.Result != null)
              composition = info.Result as PublishComposition;
            if (composition != null)
            {
              if (composition.Objects != null)
              {
                if (composition.Objects.Count != 0)
                  service.CustomPublish(this.Session.SessionGUID, (IPublisher) new ObjectsCompositionPublisher(composition, options, PublishType.Autoreplication), $"Авторепликация изменений {dbObject2.NameInMessages} от {DateTime.Now} (для {string.Format(options.EnableSites.Length > 1 ? "узлов {0}" : "узла {0}", (object) options.EnableSites)})", options.TaskPriority);
              }
            }
          }
        }
        catch (Exception ex)
        {
          TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1079"), (object) Helper.FormingLogError(ex)));
        }
        finally
        {
          if (composition != null)
          {
            composition.Objects.Clear();
            composition.Relations.Clear();
          }
        }
      }
    }
  }
}
