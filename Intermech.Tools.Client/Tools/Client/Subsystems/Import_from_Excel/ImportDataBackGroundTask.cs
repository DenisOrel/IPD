// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.ImportDataBackGroundTask
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Data;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

internal class ImportDataBackGroundTask : IBackgroundTask
{
  private object _lockObject = new object();
  private IAsyncResult _asyncResult;
  private bool _terminated;
  private bool _stopped;
  private bool _paused;
  private int _value;
  private string _name;
  private int _maximumValue;
  private DataTable _data;
  private ImportDataBackGroundTask.ImportDelegate _handler;

  public ImportDataBackGroundTask(DataTable data) => this._data = data;

  public event BackgroundTaskChangedEventHandler Changed;

  public int ImageIndex { get; } = -1;

  public string Name
  {
    get => this._name;
    set
    {
      lock (this._lockObject)
        this._name = value;
      this.OnChanged(BackgroundTaskChangedType.Text);
    }
  }

  public int MaximumValue
  {
    get => this._maximumValue;
    set
    {
      lock (this._lockObject)
        this._maximumValue = value;
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
    get => (object) this._value;
    set
    {
      lock (this._lockObject)
        this._value = (int) value;
      this.OnChanged(BackgroundTaskChangedType.Value);
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
      switch (this._terminated ? -2 : (this._asyncResult == null ? 0 : (this._paused ? -1 : 1)))
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

  public bool Active => this.State > BackgroundTaskState.Running;

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
    if (this._asyncResult != null)
    {
      lock (this._lockObject)
        this._stopped = true;
    }
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  public void Pause()
  {
    lock (this._lockObject)
      this._paused = true;
    this.OnChanged(BackgroundTaskChangedType.State);
  }

  public void Resume()
  {
    lock (this._lockObject)
    {
      if (this._asyncResult == null)
      {
        this._terminated = this._stopped = this._paused = false;
        this._handler = new ImportDataBackGroundTask.ImportDelegate(this.Import);
        this._asyncResult = this._handler.BeginInvoke(this._data, new System.AsyncCallback(this.AsyncCallback), (object) null);
      }
      else
        this._paused = false;
    }
    this.OnChanged(BackgroundTaskChangedType.State);
  }

  public void Terminate() => this.OnChanged(BackgroundTaskChangedType.Dispose);

  protected void OnChanged(BackgroundTaskChangedType type)
  {
    BackgroundTaskChangedEventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, type);
  }

  private StringBuilder Import(DataTable data) => new DataImporter(data, this).ImportData();

  private void AsyncCallback(IAsyncResult ar)
  {
    StringBuilder stringBuilder = this._handler.EndInvoke(ar);
    this._asyncResult = (IAsyncResult) null;
    this._terminated = true;
    this.OnChanged(BackgroundTaskChangedType.Dispose);
    if (stringBuilder.Length <= 0)
      return;
    IOutputView service = ServiceUtils.GetService<IOutputView>((object) ServicesManager.ServiceContainer, false);
    if (service == null)
      return;
    service.ClearText(this._name);
    service.WriteString(this._name, stringBuilder.ToString());
    service.Activate(this._name);
  }

  public bool IsProcessStoped
  {
    get
    {
      while (this._paused)
      {
        if (this._stopped)
          return true;
        Thread.Sleep(1000);
      }
      return this._stopped;
    }
  }

  private delegate StringBuilder ImportDelegate(DataTable data);
}
