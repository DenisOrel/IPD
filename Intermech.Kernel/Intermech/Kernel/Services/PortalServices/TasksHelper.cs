// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.TasksHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal class TasksHelper
{
  public static readonly string LogFile = "portal_connector.log";

  public static bool ReloadSitesInfo(
    IUserSession session,
    Guid connectGuid,
    IPortalConnector connector)
  {
    try
    {
      SiteInfo[] sitesInfo = connector.GetSitesInfo(connectGuid);
      if (sitesInfo != null)
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection(PortalConsts.objtypeSites);
        ColumnDescriptor[] columns = new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -12),
          new ColumnDescriptor((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"))
        };
        (session as UserSession).StartTransaction();
        try
        {
          for (int index = 0; index < sitesInfo.Length; ++index)
          {
            DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(PortalConsts.attributeSiteCode, RelationalOperators.Equal, (object) Convert.ToString(sitesInfo[index].Code), LogicalOperators.AND, 0)
            }, columns));
            if (dataTable.Rows.Count > 0)
            {
              Guid objectGUID = new Guid(Convert.ToString(dataTable.Rows[0][0]));
              if (objectGUID != sitesInfo[index].GUID)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1074"), (object) sitesInfo[index].Code));
              if (sitesInfo[index].Caption != Convert.ToString(dataTable.Rows[0][1]))
                session.GetObject(objectGUID).GetAttributeByID(session.IdentHelper.NameID).AsString = sitesInfo[index].Caption;
            }
            else
            {
              DBSiteObject.autoCreate = true;
              try
              {
                IDBObject dbObject = objectCollection.Create(sitesInfo[index].GUID);
                dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeSiteCode), false, new object[1]
                {
                  (object) sitesInfo[index].Code
                });
                dbObject.Attributes.AddAttribute(session.IdentHelper.NameID, false, new object[1]
                {
                  (object) sitesInfo[index].Caption
                });
                dbObject.CommitCreation(true);
              }
              finally
              {
                DBSiteObject.autoCreate = false;
              }
            }
          }
          (session as UserSession).Commit();
          return true;
        }
        catch
        {
          (session as UserSession).Rollback();
          throw;
        }
      }
    }
    catch (Exception ex)
    {
      TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1075"), (object) Helper.FormingLogError(ex)));
      return false;
    }
    finally
    {
      ((ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService))).Reload((object) session);
    }
    return true;
  }

  public static void AddMessageToLog(string message)
  {
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(message, Consts.traceAlways, TasksHelper.LogFile);
  }

  public static ITask GetTaskByID(
    IUserSession session,
    PortalTasksQueue tasksQueue,
    long taskID,
    out IDBObject taskObject)
  {
    taskObject = session.GetObject(taskID);
    return TasksHelper.GetTaskByID(session, tasksQueue, taskObject);
  }

  public static ITask GetTaskByID(
    IUserSession session,
    PortalTasksQueue tasksQueue,
    IDBObject taskObject)
  {
    IDBAttribute byGuid = taskObject.Attributes.FindByGUID(PortalConsts.attributeTaskType);
    ITask task;
    if (!tasksQueue.OnGetTaskByTypeEvent(taskObject, (TaskType) byGuid.AsInteger, out task))
    {
      switch ((int) byGuid.AsInteger)
      {
        case 0:
          task = (ITask) new ImportUpdatesTask(ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper, new ObjectImportedEventHandler(tasksQueue.FireObjectImported), new RelationImportedEventHandler(tasksQueue.FireRelationImported), new ImportTaskCompletedEventHandler(tasksQueue.FireImportTaskCompleted), new ImportTaskErrorEventHandler(tasksQueue.FireImportTaskError));
          break;
        case 1:
          task = (ITask) new PublishTask(taskObject.GetAttributeByGuid(PortalConsts.attributeTaskFiles));
          break;
        case 3:
        case 4:
          task = (ITask) new AutoTransferPublishTask(taskObject.GetAttributeByGuid(PortalConsts.attributeTaskFiles));
          break;
        default:
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1065"), (object) taskObject.NameInMessages));
      }
    }
    else if (task == null)
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1066"));
    task.TaskID = taskObject.ObjectID;
    (taskObject as DBTask).LoadTaskFromBase(task);
    return task;
  }
}
