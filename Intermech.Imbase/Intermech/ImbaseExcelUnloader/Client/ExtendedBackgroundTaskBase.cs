// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.ExtendedBackgroundTaskBase
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Client;
using System;
using System.Threading;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

public class ExtendedBackgroundTaskBase : IExtendedBackgroundTask, IBackgroundTask
{
  protected object _LockObject = new object();
  protected IAsyncResult _AsyncResult;
  protected bool _Terminated;
  protected bool _Stopped;
  protected bool _Paused;
  protected int _Value;
  protected string _Name;
  protected int _MaximumValue;

  public string Name
  {
    get => this._Name;
    set
    {
      lock (this._LockObject)
        this._Name = value;
      this.OnChanged(BackgroundTaskChangedType.Text);
    }
  }

  public bool IsProcessStoped
  {
    get
    {
      while (this._Paused)
      {
        if (this._Stopped)
          return true;
        Thread.Sleep(1000);
      }
      return this._Stopped;
    }
  }

  public void IncProgress() => this.Value = (object) (Convert.ToInt32(this.Value) + 1);

  public event BackgroundTaskChangedEventHandler Changed;

  public virtual int ImageIndex => -1;

  public int MaximumValue
  {
    get => this._MaximumValue;
    set
    {
      lock (this._LockObject)
        this._MaximumValue = value;
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
    get => (object) this._Value;
    set
    {
      lock (this._LockObject)
        this._Value = Convert.ToInt32(value);
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
      switch (this._Terminated ? -2 : (this._AsyncResult == null ? 0 : (this._Paused ? -1 : 1)))
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
    if (this._AsyncResult != null)
    {
      lock (this._LockObject)
        this._Stopped = true;
    }
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  public void Pause()
  {
    lock (this._LockObject)
      this._Paused = true;
    this.OnChanged(BackgroundTaskChangedType.State);
  }

  public virtual void Resume()
  {
  }

  public void Terminate() => this.OnChanged(BackgroundTaskChangedType.Dispose);

  protected void OnChanged(BackgroundTaskChangedType type)
  {
    BackgroundTaskChangedEventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, type);
  }
}
