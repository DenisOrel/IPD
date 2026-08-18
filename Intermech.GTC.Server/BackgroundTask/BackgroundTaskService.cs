// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.BackgroundTask.BackgroundTaskService
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Server.Processors;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.GTC.Server.BackgroundTask;

public class BackgroundTaskService : LongLifeObject, IServiceForBackgroundTask
{
  private List<BaseTaskForBackgroundTaskService> Tasks { get; set; }

  protected BaseTaskForBackgroundTaskService GetTask(Guid taskGuid)
  {
    return this.Tasks.FirstOrDefault<BaseTaskForBackgroundTaskService>((Func<BaseTaskForBackgroundTaskService, bool>) (x => x.TaskGuid == taskGuid));
  }

  public BackgroundTaskService() => this.Tasks = new List<BaseTaskForBackgroundTaskService>();

  public void StartTask(Guid sessionGuid, Guid taskGuid, string taskName, object inputData)
  {
    this.Tasks.Add(new BaseTaskForBackgroundTaskService(taskGuid, taskName));
    new BackgroundTaskService.TaskHandler(this.StartProcess).BeginInvoke(taskGuid, sessionGuid, inputData, (AsyncCallback) null, (object) null);
  }

  public void StoppingTask(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService task = this.GetTask(taskGuid);
    if (task == null)
      return;
    task.Stopping = true;
  }

  public bool StoppedTask(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService task = this.GetTask(taskGuid);
    return task == null || task.Stopped;
  }

  public void PauseTask(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService task = this.GetTask(taskGuid);
    if (task == null)
      return;
    task.Paused = true;
  }

  public void ResumeTask(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService task = this.GetTask(taskGuid);
    if (task == null)
      return;
    task.Running = true;
  }

  public int GetCompleted(Guid taskGuid, out int state, out string text)
  {
    int completed = 0;
    state = 0;
    text = string.Empty;
    BaseTaskForBackgroundTaskService task = this.GetTask(taskGuid);
    if (task != null)
    {
      state = task.Running ? 1 : (task.Paused ? 0 : -1);
      text = task.Name;
      completed = task.CompletedValue;
    }
    return completed;
  }

  public BackgroundTaskResult GetResult(Guid taskGuid)
  {
    BaseTaskForBackgroundTaskService task = this.GetTask(taskGuid);
    if (task != null)
      this.Tasks.Remove(task);
    return task?.Result;
  }

  protected virtual void StartProcess(Guid taskGuid, Guid sessionGuid, object inputData)
  {
    BaseTaskForBackgroundTaskService task = this.GetTask(taskGuid);
    if (task == null)
      throw new Exception("Task not found");
    if (!(inputData is IImportConfig importConfig))
      throw new Exception("Incorrect task input data!");
    GtcProcessorFactory.GetProcessor(sessionGuid, task, importConfig).Import();
  }

  private delegate void TaskHandler(Guid taskGuid, Guid sessionGuid, object inputData);
}
