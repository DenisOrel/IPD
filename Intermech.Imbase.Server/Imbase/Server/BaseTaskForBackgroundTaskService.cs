// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.BaseTaskForBackgroundTaskService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Imbase.Server;

public class BaseTaskForBackgroundTaskService
{
  private bool _running;
  private bool _paused = true;
  private bool _stopped;
  private int _current;

  public IUserSession Session { get; private set; }

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
      if (this.Session == null)
        return;
      this.Session.Logout(nameof (BaseTaskForBackgroundTaskService));
    }
  }

  public BackgroundTaskResult Result { get; private set; }

  public BaseTaskForBackgroundTaskService(Guid sessionGuid, Guid taskGuid, string taskName)
  {
    this.TaskGuid = taskGuid;
    this.Name = taskName;
    this.GetUserSession(sessionGuid);
    this.CountElements = 0;
    this.CompletedValue = 0;
    this.Result = new BackgroundTaskResult();
  }

  private void GetUserSession(Guid sessionGuid)
  {
    this.Session = (ImbaseServer.GetSession(sessionGuid) ?? throw new Exception(LocalizationHolder.rm.GetString("Imbase.Server.ImbaseindexingService.NullSession"))).Clone(nameof (BaseTaskForBackgroundTaskService));
  }

  public void Next()
  {
    ++this._current;
    this.CompletedValue = this.CountElements == 0 || this.Stopped || this._current > this.CountElements ? 100 : (int) ((double) this._current / (double) this.CountElements * 100.0);
  }
}
