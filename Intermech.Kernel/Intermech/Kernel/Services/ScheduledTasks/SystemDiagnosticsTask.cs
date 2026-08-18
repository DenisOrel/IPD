// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.SystemDiagnosticsTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;


namespace Intermech.Kernel.Services.ScheduledTasks;

public sealed class SystemDiagnosticsTask : DBCustomManualScheduledService, ISystemDiagnosticsTask
{
  public const string TraceFileName = "SystemDiagnostics.log";
  public const string ServerFreeSpaceDST_NAME = "SRV_FREE_SPACE";
  public const string ServerMemoryUsageDST_NAME = "SRV_MEMORY_USAGE";
  public static Guid DiagnosticsGuid = new Guid("2c04aa03-b1ca-4555-8c16-20f7c260fdfe");
  public const long GBKoef = 1000000000;
  public const long MBKoef = 1000000;
  private SystemDiagnosticsSettings _Settings;
  private List<long> _AdminsList;
  private DriveInfo _IsolatedStoreDrive;

  public SystemDiagnosticsTask(SystemDiagnosticsSettings settings) => this._Settings = settings;

  private long GetFreeDiskSpace()
  {
    if (this._IsolatedStoreDrive == null)
      this._IsolatedStoreDrive = new DriveInfo(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)));
    return this._IsolatedStoreDrive.TotalFreeSpace;
  }

  public string CheckIsoStorageFreeSpace()
  {
    int sizeNotification = this._Settings.ServerDiskFreeSizeNotification;
    if (sizeNotification <= 0)
      return string.Empty;
    long freeDiskSpace = this.GetFreeDiskSpace();
    return freeDiskSpace / 1000000000L < (long) sizeNotification ? $"Свободное пространство на системном диске сервера приложений {(ServerServices.GetService(typeof (IAppServers)) as IAppServers).ServerName} составляет {freeDiskSpace} байт, что ниже допустимого предела в {sizeNotification} Gb. Это может негативно сказаться на работоспособности сервера приложений IPS." : string.Empty;
  }

  private long GetPeakMemoryUsage() => Process.GetCurrentProcess().PeakWorkingSet64;

  public string CheckPeakMemoryUsage()
  {
    int num = this._Settings.ServerPeakMemoryUsageNotification;
    if (num > 0)
    {
      if (ServerConsts.PeakMemoryUsageNotify > 0)
        num = ServerConsts.PeakMemoryUsageNotify;
      long peakMemoryUsage = this.GetPeakMemoryUsage();
      if (peakMemoryUsage / 1000000L > (long) num)
        return $"Максимальный объем физической памяти, выделенный сервером приложений {(ServerServices.GetService(typeof (IAppServers)) as IAppServers).ServerName}, составил {peakMemoryUsage} байт, что выше допустимого предела в {num} МБ. Это может негативно сказаться на работоспособности сервера приложений IPS.";
    }
    return string.Empty;
  }

  public bool NeedCheckServersMemoryUsage => this._Settings.ServerPeakMemoryUsageNotification > 0;

  public bool NeedCheckServersDiskSpace => this._Settings.ServerDiskFreeSizeNotification > 0;

  private List<long> AdminsList
  {
    get
    {
      if (this._AdminsList == null)
      {
        DataTable dataTable = this.Session.GetObjectCollection(this.Session.IdentHelper.UsersTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(0, RelationalOperators.EntersIn, (object) this.Session.IdentHelper.AdminRoleID, LogicalOperators.NONE, 0, false)
        }, new object[1]{ (object) -2 }));
        this._AdminsList = new List<long>(dataTable.Rows.Count);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
          if (int64 != this.Session.IdentHelper.SystemID)
            this._AdminsList.Add(int64);
        }
      }
      return this._AdminsList;
    }
  }

  public void SendLetterToAdmins(string subject, string letter_body)
  {
    if (!(this.Session.GetCustomService(typeof (IRouterService)) is IRouterService customService))
      return;
    customService.CreateMessage(this.Session.SessionGUID, this.AdminsList.ToArray(), subject, letter_body, this.Session.IdentHelper.SystemID);
    this.Session.EventLogHelper.AddToTrace(letter_body, Consts.traceAlways, "SystemDiagnostics.log");
  }

  public override Guid GUID => new Guid("b0982a7e-95ad-4f19-9827-bbb84978a2e9");

  public override string ServiceName => "Диагностика системы";

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    try
    {
      int num = this._Settings.StorageSizeNotification;
      if (num > 0)
      {
        IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
        DataTable dataTable = this.Session.GetObjectCollection(new Guid("cad00014-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) -2,
          (object) -50
        }));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable.Rows[index][0]), (IUserSession) this.Session);
          if (storage.MaxStorageSize > 0)
            num = storage.MaxStorageSize;
          try
          {
            long int64 = Convert.ToInt64(storage.DataManager.ExecuteScalar($"SELECT SUM(F_ZIPSIZE) FROM {storage.StorageName}"));
            if (int64 / 1000000000L > (long) num)
              this.SendLetterToAdmins($"Уведомление о превышении допустимого размера файлового шкафа {storage.StorageName}", $"Суммарный объем файлов в файловом шкафу {storage.StorageName} достиг {int64}Gb, что выше значения {num}Gb, установленного для срабатывания данного уведомления. Выполните процедуру удаления устаревших данных или переместите файлы в другой файловый шкаф.");
          }
          finally
          {
            service.ReleaseStorage(storage);
          }
        }
      }
      if (this._Settings.ServerDiskFreeSizeNotification > 0)
      {
        string letter_body = this.CheckIsoStorageFreeSpace();
        IAppServers service = ServerServices.GetService(typeof (IAppServers)) as IAppServers;
        if (letter_body != string.Empty)
          this.SendLetterToAdmins($"Уведомление о критическом снижении свободного места на диске сервера приложений {service.ServerName}", letter_body);
        DataTable dataTable = this.Session.DataManager.ExecuteDataTable("SELECT * FROM IMS_ISB WHERE F_SERVER_DST = :dstServer", this.Session.DataManager.Parameter("dstServer", (object) "SRV_FREE_SPACE"));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          if (service.ServerName != dataTable.Rows[index]["F_SERVER_SRC"].ToString())
            this.SendLetterToAdmins($"Уведомление о критическом снижении свободного места на диске сервера приложений {dataTable.Rows[index]["F_SERVER_SRC"]}", dataTable.Rows[index]["F_STRING_INFO"].ToString());
          this.Session.DataManager.ExecuteNonQuery("DELETE FROM IMS_ISB WHERE F_KEY = :keyID", this.Session.DataManager.Parameter("keyID", (object) Convert.ToInt64(dataTable.Rows[index]["F_KEY"])));
        }
      }
      if (this._Settings.ServerPeakMemoryUsageNotification > 0)
      {
        string letter_body = this.CheckPeakMemoryUsage();
        IAppServers service = ServerServices.GetService(typeof (IAppServers)) as IAppServers;
        if (letter_body != string.Empty)
          this.SendLetterToAdmins($"Уведомление о чрезмерном расходе физической памяти сервером приложений {service.ServerName}", letter_body);
        DataTable dataTable = this.Session.DataManager.ExecuteDataTable("SELECT * FROM IMS_ISB WHERE F_SERVER_DST = :dstServer", this.Session.DataManager.Parameter("dstServer", (object) "SRV_MEMORY_USAGE"));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          if (service.ServerName != dataTable.Rows[index]["F_SERVER_SRC"].ToString())
            this.SendLetterToAdmins($"Уведомление о чрезмерном расходе физической памяти сервером приложений {dataTable.Rows[index]["F_SERVER_SRC"]}", dataTable.Rows[index]["F_STRING_INFO"].ToString());
          this.Session.DataManager.ExecuteNonQuery("DELETE FROM IMS_ISB WHERE F_KEY = :keyID", this.Session.DataManager.Parameter("keyID", (object) Convert.ToInt64(dataTable.Rows[index]["F_KEY"])));
        }
      }
    }
    catch (Exception ex)
    {
      this.Session.EventLogHelper.AddToTrace($"Ошибка в процессе выполнения диагностики ядра системы. Задача прервана с ошибкой {ex.Message}{Environment.NewLine}{ex.StackTrace}", Consts.traceAlways, "SystemDiagnostics.log");
    }
    return true;
  }
}
