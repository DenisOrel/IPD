// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.BackgroundTask.ImbaseRestructuringTablesBackgroundTask
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

#nullable disable
namespace Intermech.Imbase.BackgroundTask;

internal class ImbaseRestructuringTablesBackgroundTask : IBackgroundTask
{
  private IImbaseRestructuringTablesService _restructuringSrv;
  private Timer _timer = new Timer(1000.0);
  private int _imgIndex = -1;
  private long _sourceID;
  private List<RestructuringTablesAttrSettings> _settings;

  public ImbaseRestructuringTablesBackgroundTask(
    IImbaseRestructuringTablesService srv,
    long sourceID,
    List<RestructuringTablesAttrSettings> settings)
  {
    this._restructuringSrv = srv;
    this._sourceID = sourceID;
    this._settings = settings;
    this._timer.Elapsed += new ElapsedEventHandler(this.On_timer_Elapsed);
    this._imgIndex = ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service ? service.ImageIndex("imgRefresh") : -1;
  }

  public event BackgroundTaskChangedEventHandler Changed;

  public int ImageIndex => this._imgIndex;

  public string Name => LocalizationHolder.rm.GetString("Imbase_RestructurinTables_Caption");

  public int MaximumValue
  {
    get => 100;
    set
    {
    }
  }

  public int MinimumValue
  {
    get => 0;
    set
    {
    }
  }

  public object Value
  {
    get => (object) this._restructuringSrv.Value;
    set
    {
    }
  }

  public object Result
  {
    get => (object) 1;
    set
    {
    }
  }

  public BackgroundTaskState State
  {
    get
    {
      switch (this._restructuringSrv.State)
      {
        case -2:
          return BackgroundTaskState.Terminated;
        case -1:
          return BackgroundTaskState.Paused;
        case 0:
          return BackgroundTaskState.Stopped;
        default:
          return BackgroundTaskState.Running;
      }
    }
    set
    {
    }
  }

  public BackgroundTaskShowMode ShowMode => BackgroundTaskShowMode.Progress;

  public bool Active => this._restructuringSrv.State > 0;

  public void SetMaxMin(int max, int min)
  {
  }

  public bool CanStop()
  {
    return this.State == BackgroundTaskState.Running || this.State == BackgroundTaskState.Paused;
  }

  public bool CanPause() => this.State == BackgroundTaskState.Running;

  public bool CanResume() => true;

  public bool CanTerminate() => true;

  public void Stop()
  {
    this._timer.Enabled = false;
    this._restructuringSrv.Stop();
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  public void Pause()
  {
    this._timer.Enabled = false;
    this._restructuringSrv.Pause();
    this.OnChanged(BackgroundTaskChangedType.State);
  }

  public void Resume()
  {
    this._timer.Enabled = true;
    long userID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      userID = sessionKeeper.Session.UserID;
    if (this._sourceID != 0L && this._settings != null && this._settings.Count > 0)
      this._restructuringSrv.Start(userID, this._sourceID, this._settings);
    this.OnChanged(BackgroundTaskChangedType.State);
  }

  public void Terminate()
  {
    this._timer.Enabled = false;
    this._restructuringSrv?.Stop();
    this.OnChanged(BackgroundTaskChangedType.Dispose);
    if (this._restructuringSrv == null || this._restructuringSrv.ExceptionInfo == null || this._restructuringSrv.ExceptionInfo.Count <= 0 || !(ServicesManager.GetService(typeof (IOutputView)) is IOutputView service))
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (RestructuringTablesExteption restructuringTablesExteption in this._restructuringSrv.ExceptionInfo)
    {
      if (restructuringTablesExteption.ID == 0L)
      {
        if (string.IsNullOrEmpty(restructuringTablesExteption.Caption))
          stringBuilder.AppendLine(restructuringTablesExteption.Message);
        else
          stringBuilder.AppendLine($"{restructuringTablesExteption.Caption}  -  {restructuringTablesExteption.Message}");
      }
      else if (string.IsNullOrEmpty(restructuringTablesExteption.Caption))
        stringBuilder.AppendLine($"({restructuringTablesExteption.ID})  -  {restructuringTablesExteption.Message}");
      else
        stringBuilder.AppendLine($"({restructuringTablesExteption.ID}) {restructuringTablesExteption.Caption}  -  {restructuringTablesExteption.Message}");
    }
    string category = LocalizationHolder.rm.GetString("Imbase_RestructurinTables_Caption");
    service.ClearText(category);
    service.WriteString(category, stringBuilder.ToString());
    service.Activate(category);
    service.ShowView();
  }

  private void On_timer_Elapsed(object sender, ElapsedEventArgs e)
  {
    try
    {
      this.OnChanged(BackgroundTaskChangedType.State);
      this.OnChanged(BackgroundTaskChangedType.Value);
      if (this.State != BackgroundTaskState.Terminated)
        return;
      this.Terminate();
    }
    catch (Exception ex)
    {
      if (!(ServicesManager.GetService(typeof (IOutputView)) is IOutputView service))
        return;
      string category = LocalizationHolder.rm.GetString("Imbase_RestructurinTables_Caption");
      service.WriteString(category, ex.Message);
      service.Activate(category);
    }
  }

  protected void OnChanged(BackgroundTaskChangedType type)
  {
    BackgroundTaskChangedEventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, type);
  }
}
