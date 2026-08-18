// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.RebuildViewsTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Services.ScheduledTasks;

internal class RebuildViewsTask : DBCustomManualScheduledService
{
  private AdminUtilsService _AdminUtils;

  public RebuildViewsTask(AdminUtilsService admUtils) => this._AdminUtils = admUtils;

  public override Guid GUID => new Guid("cadd95b3-306c-11d8-b4e9-00304f19f545");

  public override string ServiceName => LocalizationHolder.rm.GetString(nameof (RebuildViewsTask));

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    this.Session.EventLogHelper.AddToTrace(LocalizationHolder.rm.GetString("RebuildViewsTaskStarted"), Consts.traceAlways, string.Empty);
    IDatabaseLocker service = ServerServices.GetService(typeof (IDatabaseLocker)) as IDatabaseLocker;
    DatabaseLockInfo databaseLockInfo = service.Lock((IUserSession) this.Session, "RebuildViews", TimeSpan.FromDays(2.0));
    if (databaseLockInfo.Success)
    {
      try
      {
        this._AdminUtils.RebuildObjectsView(this.Session.SessionGUID);
        DataTable dataTable1 = this.Session.GetObjectTypeCollection(-2).Select(string.Empty);
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
        {
          try
          {
            this.Session.GetObjectType(Convert.ToInt32(dataTable1.Rows[index]["F_OBJECT_TYPE"])).RebuildView();
          }
          catch (Exception ex)
          {
            this.Session.EventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("RebuildObjectViewError"), dataTable1.Rows[index]["F_OBJECT_TYPE"], (object) ex.Message), Consts.traceAlways, string.Empty);
            break;
          }
        }
        DataTable dataTable2 = this.Session.GetRelationTypeCollection().Select(string.Empty);
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          try
          {
            this.Session.GetRelationType(Convert.ToInt32(dataTable2.Rows[index]["F_RELATION_TYPE"])).RebuildView();
          }
          catch (Exception ex)
          {
            this.Session.EventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("RebuildRelationViewError"), dataTable2.Rows[index]["F_RELATION_TYPE"], (object) ex.Message), Consts.traceAlways, string.Empty);
            break;
          }
        }
        this.Session.EventLogHelper.AddToTrace(LocalizationHolder.rm.GetString("RebuildViewsTaskFinished"), Consts.traceAlways, string.Empty);
      }
      catch (Exception ex)
      {
        this.Session.EventLogHelper.AddToTrace($"Фоновая задача перегенерации представлений данных прервана с ошибкой: {ex.Message}{Environment.NewLine}{ex.StackTrace}", Consts.traceAlways, string.Empty);
      }
      finally
      {
        service.UnLock((IUserSession) this.Session, "RebuildViews");
      }
    }
    else
      this.Session.EventLogHelper.AddToTrace(databaseLockInfo.GetErrorMessage("Перегенерация представлений данных"), Consts.traceAlways, string.Empty);
    return true;
  }
}
