// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.TablesIndexerService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

internal class TablesIndexerService : 
  BackgroundTaskService,
  ITablesIndexerService,
  IServiceForBackgroundTask
{
  protected override void StartProcess(Guid taskGuid, object inputData)
  {
    BaseTaskForBackgroundTaskService task = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((System.Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (task == null)
      return;
    task.Running = true;
    UserSession session = (UserSession) null;
    try
    {
      session = this.GetSystemSession();
      List<long> tablesIds = this.GetTablesIds((IUserSession) session);
      int count = tablesIds.Count;
      task.CountElements = count;
      for (int index = 0; index < count; ++index)
      {
        if (this.IsProcessStoped(task))
          throw new TablesIndexerService.StopTaskException();
        TablesIndexer.IndexTable(tablesIds[index], session);
        task.Next();
      }
      int version = 1;
      this.UpdateVersion(session.DataManager, session.EventLogHelper, version);
    }
    catch (TablesIndexerService.StopTaskException ex)
    {
      task.Result.Messages.Add(new BackgroundTaskMessage(LocalizationHolder.rm.GetString("Imbase_Task_Stop")));
    }
    catch (Exception ex)
    {
      task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
    }
    finally
    {
      session?.Logout("Imbase.TableIndexer.Service");
      task.Stopped = true;
    }
  }

  private UserSession GetSystemSession()
  {
    return (ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, true).GetSystemSessionTemporaryClone("Imbase.TableIndexer.Service") ?? throw new Exception(LocalizationHolder.rm.GetString("Imbase_NullSession"))) as UserSession;
  }

  private List<long> GetTablesIds(IUserSession session)
  {
    DataRowCollection rows = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID)
    })
    {
      ColumnNames = new ColumnNameMapping[1]
      {
        ColumnNameMapping.ID
      },
      TableName = "f",
      FailIfNotFound = false
    }).Rows;
    int count = rows.Count;
    List<long> tablesIds = new List<long>(count);
    for (int index = 0; index < count; ++index)
      tablesIds.Add(Convert.ToInt64(rows[index][0]));
    return tablesIds;
  }

  private bool UpdateVersion(IDbManager dbManager, IEventLogHelper eventLogHelper, int version)
  {
    try
    {
      dbManager.ExecuteNonQuery($"UPDATE IMS_DBVERSION SET F_VERSION_ID = {version} WHERE F_MODULE_NAME = 'IMBASE'");
      return true;
    }
    catch (Exception ex)
    {
      eventLogHelper?.AddToTrace("Ошибка при обновлении версии ядра Imbase: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
    }
    return false;
  }

  private bool IsProcessStoped(BaseTaskForBackgroundTaskService task)
  {
    while (task.Paused && !task.Stopped)
      Thread.Sleep(1000);
    return task.Stopped;
  }

  private class StopTaskException : ApplicationException
  {
  }
}
