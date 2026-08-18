// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.UpdateObjectsFromImbaseService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

public class UpdateObjectsFromImbaseService : 
  BackgroundTaskService,
  IUpdateObjectsFromImbaseService,
  IServiceForBackgroundTask
{
  private string objIDColName = Convert.ToString(-2);
  private string imbaseObjRefColName = Convert.ToString(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
  private string recIDColName = Convert.ToString(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);

  private bool IsProcessStoped(BaseTaskForBackgroundTaskService task)
  {
    while (task.Paused && !task.Stopped)
      Thread.Sleep(1000);
    return task.Stopped;
  }

  protected override void StartProcess(Guid taskGuid, object inputData)
  {
    BaseTaskForBackgroundTaskService task = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((System.Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (task == null)
      return;
    task.Running = true;
    try
    {
      if (!(inputData is List<long> longList))
        return;
      if (!(task.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        throw new Exception(LocalizationHolder.rm.GetString("Imbase.Server.ImbaseindexingService.NullImbaseServer"));
      if (longList.Count > 1)
      {
        task.CountElements = longList.Count;
        foreach (long tableRefID in longList)
        {
          if (this.IsProcessStoped(task))
            throw new UpdateObjectsFromImbaseService.StopTaskException();
          this.ProcessTableRefWithNext(task, tableRefID, customService);
        }
      }
      else
        this.ProcessTableRef(task, longList[0], customService, true);
    }
    catch (UpdateObjectsFromImbaseService.StopTaskException ex)
    {
      task.Result.Messages.Add(new BackgroundTaskMessage(LocalizationHolder.rm.GetString("Imbase_Task_Stop")));
    }
    catch (Exception ex)
    {
      task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
    }
    finally
    {
      task.Stopped = true;
    }
  }

  private void ProcessTableRef(
    BaseTaskForBackgroundTaskService task,
    long tableRefID,
    IImbaseServer imbaseServer,
    bool bCountObjects)
  {
    try
    {
      DataTable createdObjects = imbaseServer.GetCreatedObjects(task.Session.SessionGUID, tableRefID);
      if (createdObjects == null || createdObjects.Rows.Count <= 0)
        return;
      ISynchronizationObjService service = ApplicationServices.Container.GetService<ISynchronizationObjService>();
      if (bCountObjects)
      {
        task.CountElements = createdObjects.Rows.Count;
        foreach (DataRow row in (InternalDataCollectionBase) createdObjects.Rows)
        {
          if (this.IsProcessStoped(task))
            throw new UpdateObjectsFromImbaseService.StopTaskException();
          long int64_1 = Convert.ToInt64(row[this.objIDColName]);
          long int64_2 = Convert.ToInt64(row[this.imbaseObjRefColName]);
          long int64_3 = Convert.ToInt64(row[this.recIDColName]);
          this.ProcessObjectWithNext(task, int64_1, int64_2, int64_3, service);
        }
      }
      else
      {
        foreach (DataRow row in (InternalDataCollectionBase) createdObjects.Rows)
        {
          if (this.IsProcessStoped(task))
            throw new UpdateObjectsFromImbaseService.StopTaskException();
          long int64_4 = Convert.ToInt64(row[this.objIDColName]);
          long int64_5 = Convert.ToInt64(row[this.imbaseObjRefColName]);
          long int64_6 = Convert.ToInt64(row[this.recIDColName]);
          this.ProcessObject(task, int64_4, int64_5, int64_6, service);
        }
      }
    }
    catch (Exception ex)
    {
      QuickObjectInfo objectInfo = task.Session.GetObjectInfo(tableRefID);
      string str = LocalizationHolder.rm.GetString("Imbase_UpdateObjectsFromImbase_TableRefID_Stop");
      task.Result.Messages.Add(new BackgroundTaskMessage($"{objectInfo.Caption} (ID = '{tableRefID}'). {str}")
      {
        Exception = ex
      });
    }
  }

  private void ProcessTableRefWithNext(
    BaseTaskForBackgroundTaskService task,
    long tableRefID,
    IImbaseServer imbaseServer)
  {
    this.ProcessTableRef(task, tableRefID, imbaseServer, false);
    task.Next();
  }

  private void ProcessObject(
    BaseTaskForBackgroundTaskService task,
    long objID,
    long tableRefID,
    long recID,
    ISynchronizationObjService synchronizationObjService)
  {
    try
    {
      string message;
      int num = (int) synchronizationObjService.Synchronize(task.Session, objID, tableRefID, recID, false, out message);
      task.Result.ChangedObjects.Add(objID);
      task.Result.Messages.Add(new BackgroundTaskMessage(message));
    }
    catch (Exception ex)
    {
      string str = LocalizationHolder.rm.GetString("Imbase_ObjectUpdate_Error");
      IDBObject dbObject = task.Session.GetObject(objID, false);
      task.Result.Messages.Add(new BackgroundTaskMessage($"{dbObject?.NameInMessages} (ID = '{objID}'). {str}")
      {
        Exception = ex
      });
    }
  }

  private void ProcessObjectWithNext(
    BaseTaskForBackgroundTaskService task,
    long objID,
    long tableRefID,
    long recID,
    ISynchronizationObjService synchronizationObjService)
  {
    this.ProcessObject(task, objID, tableRefID, recID, synchronizationObjService);
    task.Next();
  }

  private class StopTaskException : ApplicationException
  {
  }
}
