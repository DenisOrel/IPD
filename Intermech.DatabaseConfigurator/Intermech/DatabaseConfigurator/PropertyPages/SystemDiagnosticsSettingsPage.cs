// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.SystemDiagnosticsSettingsPage
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Kernel;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

internal class SystemDiagnosticsSettingsPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IServiceProvider _serviceProvider;
  private ClassWrapperForPropertyGrid _wrapper;
  private SystemDiagnosticsSettingsPage.CurrentSystemDiagnosticsSettings _diagnosticsSettings;

  public SystemDiagnosticsSettingsPage(IServiceProvider serviceProvider)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    this._serviceProvider = serviceProvider;
    ((IPropertyPagesService) this._serviceProvider.GetService(typeof (IPropertyPagesService)))?.AddPage("Система\\Диагностика системы", (IPropertyPage) this);
    this._diagnosticsSettings = new SystemDiagnosticsSettingsPage.CurrentSystemDiagnosticsSettings();
    this._wrapper = new ClassWrapperForPropertyGrid((object) this._diagnosticsSettings);
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control => (object) this._wrapper;

  public string PageName => "Система\\Диагностика системы";

  public void Apply()
  {
    if (this._diagnosticsSettings == null)
      return;
    this._diagnosticsSettings.ApplyUpdates();
    this._wrapper.ResetOldValues();
  }

  public void Cancel()
  {
    if (this._diagnosticsSettings == null)
      return;
    this._diagnosticsSettings._inited = false;
  }

  public string HelpTopicID => string.Empty;

  public string HeaderText => "Настройки диагностики системы";

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  internal class CurrentSystemDiagnosticsSettings
  {
    internal bool _inited;
    private bool _modified;
    private int _StorageSizeNotification;
    private int _ServerDiskFreeSizeNotification;
    private int _ServerPeakMemoryUsageNotification;
    private int _MaxLogFileSize;
    private int _MaxLogFileCopies;
    private string _ServerLogPath;

    public CurrentSystemDiagnosticsSettings()
    {
      this._inited = false;
      this.LoadCurrentValues();
    }

    private void CheckInited()
    {
      if (this._inited)
        return;
      this.LoadCurrentValues();
      this._inited = true;
    }

    private void LoadCurrentValues()
    {
      SDSettings settings = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISystemDiagnosticsSettings)) as ISystemDiagnosticsSettings).Settings;
      this._StorageSizeNotification = settings.StorageSizeNotification;
      this._ServerDiskFreeSizeNotification = settings.ServerDiskFreeSizeNotification;
      this._ServerPeakMemoryUsageNotification = settings.ServerPeakMemoryUsageNotification;
      this._MaxLogFileSize = settings.MaxLogFileSize;
      this._MaxLogFileCopies = settings.MaxLogFileCopies;
      this._ServerLogPath = settings.ServerLogPath;
      this._modified = false;
    }

    public void ApplyUpdates()
    {
      if (!this._modified)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        SDSettings settings = new SDSettings(this._StorageSizeNotification, this._ServerDiskFreeSizeNotification, this._ServerPeakMemoryUsageNotification, this._MaxLogFileSize, this._ServerLogPath, this._MaxLogFileCopies);
        (sessionKeeper.Session.GetCustomService(typeof (ISystemDiagnosticsSettings)) as ISystemDiagnosticsSettings).SetSettings(sessionKeeper.Session.SessionGUID, settings);
        this._modified = false;
      }
    }

    [CustomDisplayName("MaxLogFileSize")]
    [CustomDescription("MaxLogFileSizeNote")]
    [TypeConverter(typeof (IntZeroEmptyStringConverter))]
    public int MaxLogFileSize
    {
      get
      {
        this.CheckInited();
        return this._MaxLogFileSize;
      }
      set
      {
        this._MaxLogFileSize = value;
        this._modified = true;
      }
    }

    [CustomDisplayName("MaxLogFileCopies")]
    [CustomDescription("MaxLogFileCopiesNote")]
    [TypeConverter(typeof (IntZeroEmptyStringConverter))]
    public int MaxLogFileCopies
    {
      get
      {
        this.CheckInited();
        return this._MaxLogFileCopies;
      }
      set
      {
        this._MaxLogFileCopies = value;
        this._modified = true;
      }
    }

    [CustomDisplayName("ServerLogPath")]
    [CustomDescription("ServerLogPathNote")]
    public string ServerLogPath
    {
      get
      {
        this.CheckInited();
        return this._ServerLogPath;
      }
    }

    [CustomDisplayName("StorageSizeNotification")]
    [CustomDescription("StorageSizeNotificationNote")]
    [TypeConverter(typeof (IntZeroEmptyStringConverter))]
    public int StorageSizeNotification
    {
      get
      {
        this.CheckInited();
        return this._StorageSizeNotification;
      }
      set
      {
        this._StorageSizeNotification = value;
        this._modified = true;
      }
    }

    [CustomDisplayName("ServerDiskFreeSizeNotification")]
    [CustomDescription("ServerDiskFreeSizeNotificationNote")]
    [TypeConverter(typeof (IntZeroEmptyStringConverter))]
    public int ServerDiskFreeSizeNotification
    {
      get
      {
        this.CheckInited();
        return this._ServerDiskFreeSizeNotification;
      }
      set
      {
        this._ServerDiskFreeSizeNotification = value;
        this._modified = true;
      }
    }

    [CustomDisplayName("ServerPeakMemoryUsageNotification")]
    [CustomDescription("ServerPeakMemoryUsageNotificationNote")]
    [TypeConverter(typeof (IntZeroEmptyStringConverter))]
    public int ServerPeakMemoryUsageNotification
    {
      get
      {
        this.CheckInited();
        return this._ServerPeakMemoryUsageNotification;
      }
      set
      {
        this._ServerPeakMemoryUsageNotification = value;
        this._modified = true;
      }
    }
  }
}
