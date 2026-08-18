// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ScheduledImportTasks
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


namespace Intermech.Kernel.Services.PortalServices;

internal class ScheduledImportTasks(
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
      IPortalConnector customService = (IPortalConnector) this.Session.GetCustomService(typeof (IPortalConnector));
      Guid connectGuid = customService.Login(this.Session.SessionGUID);
      try
      {
        if (!TasksHelper.ReloadSitesInfo((IUserSession) this.Session, connectGuid, customService))
          return false;
        this.GetUpdates(connectGuid, customService);
      }
      finally
      {
        if (connectGuid != Guid.Empty && customService != null)
          customService.Logout(connectGuid);
      }
      try
      {
        IDBObjectCollection objectCollection = this.Session.GetObjectCollection(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545"));
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
        {
          new ConditionStructure(PortalConsts.attributeTaskStatus, RelationalOperators.Equal, (object) 4, LogicalOperators.AND, 0),
          new ConditionStructure(PortalConsts.attributeTaskType, RelationalOperators.Equal, (object) 0, LogicalOperators.AND, 0),
          new ConditionStructure(PortalConsts.attributeServerName, RelationalOperators.Empty, (object) null, LogicalOperators.OR, 1),
          new ConditionStructure(PortalConsts.attributeServerName, RelationalOperators.Equal, (object) EnvironmentConsts.MachineName.ToUpper(), LogicalOperators.AND, -1)
        };
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
        TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1063"), (object) Helper.FormingLogError(ex)));
        return false;
      }
    }
  }

  private void GetUpdates(Guid connectGuid, IPortalConnector connector)
  {
    try
    {
      string[] updates = connector.GetUpdates(connectGuid, this.Session.SessionGUID);
      IDBObjectCollection objectCollection = this.Session.GetObjectCollection(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545"));
      if (updates == null || updates.Length == 0)
        return;
      for (int index = 0; index < updates.Length; ++index)
      {
        try
        {
          this.FormingImportTaskFromUpdate((IUserSession) this.Session, connector, objectCollection, connectGuid, updates[index]);
        }
        catch (Exception ex)
        {
          TasksHelper.AddMessageToLog($"Ошибка при получении обновления {updates[index]}: {Helper.FormingLogError(ex)}");
        }
      }
    }
    catch (Exception ex)
    {
      TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1078"), (object) Helper.FormingLogError(ex)));
    }
  }

  private long FormingImportTaskFromUpdate(
    IUserSession session,
    IPortalConnector connector,
    IDBObjectCollection objColl,
    Guid connectGuid,
    string updateGuid)
  {
    TransferedObject[] updateUnit = connector.GetUpdateUnit(connectGuid, updateGuid);
    if (updateUnit == null || updateUnit.Length == 0)
    {
      connector.EndUpdateUnit(connectGuid, updateGuid);
      return 0;
    }
    connector.StartUpdateUnit(connectGuid, updateGuid);
    (session as UserSession).StartTransaction();
    try
    {
      ImportUpdatesTask importUpdatesTask = new ImportUpdatesTask(session.UserID, (session as UserSession).UserGUID, string.Format(LocalizationHolder.rm.GetString("Kernel_1077"), (object) updateGuid), TaskPriority.Normal, updateGuid, updateUnit, new ObjectImportedEventHandler(this.tasksQueue.FireObjectImported), new RelationImportedEventHandler(this.tasksQueue.FireRelationImported), new ImportTaskCompletedEventHandler(this.tasksQueue.FireImportTaskCompleted));
      bool flag = false;
      ConditionStructure conditionStructure1 = new ConditionStructure(PortalConsts.attributeUpdateGuid, RelationalOperators.Equal, (object) updateGuid, LogicalOperators.AND, 0);
      ConditionStructure conditionStructure2 = new ConditionStructure(PortalConsts.attributeTaskType, RelationalOperators.Equal, (object) 2, LogicalOperators.AND, 0);
      DataTable dataTable = objColl.Select(new DBRecordSetParams(new ConditionStructure[2]
      {
        conditionStructure1,
        conditionStructure2
      }, new object[2]{ (object) -2, (object) -8 }));
      IDBObject dbTask;
      if (dataTable.Rows.Count == 1)
      {
        importUpdatesTask.TaskID = Convert.ToInt64(dataTable.Rows[0][0]);
        dbTask = session.GetObject(importUpdatesTask.TaskID);
        importUpdatesTask.UserID = Convert.ToInt64(dataTable.Rows[0][1]);
        ImportTask importTask = new ImportTask(importUpdatesTask.TaskID);
        (dbTask as DBTask).LoadTaskData((ITask) importTask);
        importUpdatesTask.Priority = importTask.Priority;
        flag = true;
      }
      else
      {
        dbTask = objColl.Create();
        importUpdatesTask.TaskID = Math.Abs(dbTask.ObjectID);
        importUpdatesTask.Enabled = true;
      }
      this.tasksQueue.Storage.UpdateTask(session, dbTask, (ITask) importUpdatesTask);
      if (!flag)
        dbTask.CommitCreation(true);
      (session as UserSession).Commit();
      return dbTask.ObjectID;
    }
    catch
    {
      (session as UserSession).Rollback();
      throw;
    }
  }
}
