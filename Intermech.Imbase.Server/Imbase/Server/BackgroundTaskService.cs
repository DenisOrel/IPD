// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.BackgroundTaskService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server;

public class BackgroundTaskService : LongLifeObject, IServiceForBackgroundTask
{
  protected List<BaseTaskForBackgroundTaskService> Tasks { get; set; }

  public BackgroundTaskService() => this.Tasks = new List<BaseTaskForBackgroundTaskService>();

  public void StartTask(Guid sessionGuid, Guid taskGuid, string taskName, object inputData)
  {
    this.Tasks.Add(new BaseTaskForBackgroundTaskService(sessionGuid, taskGuid, taskName));
    new BackgroundTaskService.TaskHandler(this.StartProcess).BeginInvoke(taskGuid, inputData, (AsyncCallback) null, (object) null);
  }

  public void StopTask(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService backgroundTaskService = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (backgroundTaskService == null)
      return;
    backgroundTaskService.Stopped = true;
  }

  public void PauseTask(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService backgroundTaskService = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (backgroundTaskService == null)
      return;
    backgroundTaskService.Paused = true;
  }

  public void ResumeTask(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService backgroundTaskService = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (backgroundTaskService == null)
      return;
    backgroundTaskService.Running = true;
  }

  public int GetCompleted(Guid taskGuid, out int state, out string text)
  {
    int completed = 0;
    state = 0;
    text = string.Empty;
    BaseTaskForBackgroundTaskService backgroundTaskService = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (backgroundTaskService != null)
    {
      state = backgroundTaskService.Running ? 1 : (backgroundTaskService.Paused ? 0 : -1);
      text = backgroundTaskService.Name;
      completed = backgroundTaskService.CompletedValue;
    }
    return completed;
  }

  public BackgroundTaskResult GetResult(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService backgroundTaskService = this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
    if (backgroundTaskService != null)
      this.Tasks.Remove(backgroundTaskService);
    return backgroundTaskService?.Result;
  }

  protected virtual void StartProcess(Guid taskGuid, object inputData)
  {
  }

  private delegate void TaskHandler(Guid taskGuid, object inputData);
}
