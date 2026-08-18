// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.BackgroundTask.BaseTaskForBackgroundTaskService
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using System;

#nullable disable
namespace Intermech.GTC.Server.BackgroundTask;

public class BaseTaskForBackgroundTaskService
{
  private bool _running;
  private bool _paused = true;
  private bool _stopped;
  private int _current;

  public Guid TaskGuid { get; private set; }

  public string Name { get; set; }

  public int CompletedValue { get; set; }

  public int CountElements { get; set; }

  public bool Running
  {
    get => this._running;
    set
    {
      if (!value)
        return;
      this._paused = this._stopped = false;
      this._running = true;
    }
  }

  public bool Paused
  {
    get => this._paused;
    set
    {
      if (!value)
        return;
      this._running = this._stopped = false;
      this._paused = true;
    }
  }

  public bool Stopped
  {
    get => this._stopped;
    set
    {
      if (!value)
        return;
      this._running = this._paused = false;
      this._stopped = true;
      this.CompletedValue = 100;
    }
  }

  public bool Stopping { get; set; }

  public BackgroundTaskResult Result { get; private set; }

  public void ResetCounter()
  {
    this._current = 0;
    this.CompletedValue = 0;
  }

  public BaseTaskForBackgroundTaskService(Guid taskGuid, string taskName)
  {
    this.TaskGuid = taskGuid;
    this.Name = taskName;
    this.CountElements = 0;
    this.CompletedValue = 0;
    this.Result = new BackgroundTaskResult();
  }

  public void Next()
  {
    ++this._current;
    this.CompletedValue = this.CountElements == 0 || this.Stopped || this._current > this.CountElements ? 100 : (int) ((double) this._current / (double) this.CountElements * 100.0);
  }
}
