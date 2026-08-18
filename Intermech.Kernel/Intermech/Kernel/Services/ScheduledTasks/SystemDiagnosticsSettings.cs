// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.SystemDiagnosticsSettings
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Kernel;
using Intermech.Interfaces.Server;
using System;
using System.Configuration;


namespace Intermech.Kernel.Services.ScheduledTasks;

public class SystemDiagnosticsSettings : LongLifeObject, ISystemDiagnosticsSettings
{
  private DBConfigurationService _ConfigService;
  private string _ServerLogPath;

  private DBConfigurationService ConfigService
  {
    get
    {
      if (this._ConfigService == null)
        this._ConfigService = ServerServices.GetService(typeof (IDBConfigurationService)) as DBConfigurationService;
      return this._ConfigService;
    }
  }

  internal int StorageSizeNotification
  {
    get
    {
      return Convert.ToInt32(this.ConfigService.GetValue("KERNEL", "DIAGNOSTICS", "StorageSizeNotify", (object) 0));
    }
  }

  internal int ServerDiskFreeSizeNotification
  {
    get
    {
      return Convert.ToInt32(this.ConfigService.GetValue("KERNEL", "DIAGNOSTICS", "ServerDiskFreeSizeNotify", (object) 10));
    }
  }

  internal int ServerPeakMemoryUsageNotification
  {
    get
    {
      return Convert.ToInt32(this.ConfigService.GetValue("KERNEL", "DIAGNOSTICS", "ServerPeakMemoryUsageNotify", (object) 0));
    }
  }

  public int MaxLogFileSize
  {
    get
    {
      return Convert.ToInt32(this.ConfigService.GetValue("KERNEL", "DIAGNOSTICS", nameof (MaxLogFileSize), (object) 0));
    }
  }

  public int MaxLogFileSizeInBytes => this.MaxLogFileSize * 1048576 /*0x100000*/;

  public int MaxLogFileCopies
  {
    get
    {
      return Convert.ToInt32(this.ConfigService.GetValue("KERNEL", "DIAGNOSTICS", nameof (MaxLogFileCopies), (object) 0));
    }
  }

  public string ServerLogPath
  {
    get
    {
      if (this._ServerLogPath == null)
      {
        this._ServerLogPath = ConfigurationManager.AppSettings.Get("LogPath");
        if (this._ServerLogPath == null)
          this._ServerLogPath = string.Empty;
      }
      return this._ServerLogPath;
    }
  }

  public SDSettings Settings
  {
    get
    {
      return new SDSettings(this.StorageSizeNotification, this.ServerDiskFreeSizeNotification, this.ServerPeakMemoryUsageNotification, this.MaxLogFileSize, this.ServerLogPath, this.MaxLogFileCopies);
    }
  }

  public void SetSettings(Guid sessionGuid, SDSettings settings)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_14191.ssp_appserver_14192(113983777));
    sessionById.Configurations.WriteInteger("KERNEL", "DIAGNOSTICS", "StorageSizeNotify", (long) settings.StorageSizeNotification, 0L);
    sessionById.Configurations.WriteInteger("KERNEL", "DIAGNOSTICS", "ServerDiskFreeSizeNotify", (long) settings.ServerDiskFreeSizeNotification, 0L);
    sessionById.Configurations.WriteInteger("KERNEL", "DIAGNOSTICS", "ServerPeakMemoryUsageNotify", (long) settings.ServerPeakMemoryUsageNotification, 0L);
    sessionById.Configurations.WriteInteger("KERNEL", "DIAGNOSTICS", "MaxLogFileSize", (long) settings.MaxLogFileSize, 0L);
    sessionById.Configurations.WriteInteger("KERNEL", "DIAGNOSTICS", "MaxLogFileCopies", (long) settings.MaxLogFileCopies, 0L);
  }
}
