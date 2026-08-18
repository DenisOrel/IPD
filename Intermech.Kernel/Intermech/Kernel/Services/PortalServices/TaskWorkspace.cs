// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.TaskWorkspace
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

internal class TaskWorkspace
{
  private readonly IUserSession _session;
  private readonly PortalTasksQueue _tasksQueue;
  private readonly ITask _task;
  private IDBObject _dbTask;

  public TaskWorkspace(
    IUserSession session,
    PortalTasksQueue tasksQueue,
    ITask task,
    IDBObject dbTask)
  {
    this._session = session;
    this._tasksQueue = tasksQueue;
    this._task = task;
    this._dbTask = dbTask;
  }

  public void BeginTask()
  {
    this._task.TaskStatusChangedEvent += new TaskStatusChangedEventHandler(this.Task_TaskStatusChangedEvent);
    this._task.TaskStartEvent += new TaskStartEventHandler(this.Task_TaskStartEvent);
    this._task.TaskStepCompletedEvent += new TaskStepCompletedEventHandler(this.Task_TaskStepCompletedEvent);
    this._task.TaskSaveDataEvent += new TaskSaveDataEventHandler(this.Task_TaskSaveDataEvent);
    try
    {
      if (this._dbTask == null)
      {
        if (!this._tasksQueue.StartedTasksObjects.TryGetValue(this._task.TaskID, out this._dbTask))
        {
          this._dbTask = this._session.GetObject(this._task.TaskID, true);
          IDBAttributeCollection attributes = this._dbTask.Attributes;
          this._tasksQueue.StartedTasksObjects.Add(this._task.TaskID, this._dbTask);
        }
      }
      else if (!this._tasksQueue.StartedTasksObjects.ContainsKey(this._task.TaskID))
        this._tasksQueue.StartedTasksObjects.Add(this._task.TaskID, this._dbTask);
      if (!this._task.Enabled)
        throw new Exception($"Задача {this._task.Name} запрещена к выполнению!");
      this._task.BeginTask(this._session, ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper);
    }
    finally
    {
      this._task.TaskStatusChangedEvent -= new TaskStatusChangedEventHandler(this.Task_TaskStatusChangedEvent);
      this._task.TaskStartEvent -= new TaskStartEventHandler(this.Task_TaskStartEvent);
      this._task.TaskStepCompletedEvent -= new TaskStepCompletedEventHandler(this.Task_TaskStepCompletedEvent);
      this._task.TaskSaveDataEvent -= new TaskSaveDataEventHandler(this.Task_TaskSaveDataEvent);
    }
  }

  private void Task_TaskSaveDataEvent(object sender, TaskSaveDataEventArgs e)
  {
    (this._dbTask as DBTask).SaveTaskData((ITask) sender);
  }

  private void Task_TaskStartEvent(object sender, TaskStartEventArgs e)
  {
    (this._dbTask as DBTask).SetStatus(TaskStatus.Transmitting);
    (this._dbTask as DBTask).SetError((Exception) null);
  }

  private void Task_TaskStepCompletedEvent(object sender, TaskStepCompletedEventArgs e)
  {
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"Task_TaskStepCompletedEvent sessionGUID={e.Session.SessionGUID} unit={e.Unit.GUID} percent={e.Percent} unitIndex={e.UnitIndex}");
    (this._dbTask as DBTask).SetUnitCompleted(e);
  }

  private void Task_TaskStatusChangedEvent(object sender, TaskStatusChangedEventArgs e)
  {
    this._tasksQueue.StartedTasksObjects.Remove(this._task.TaskID);
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"Task_TaskCompletedEvent sessionGUID={e.Session.SessionGUID} e.Result={e.NewStatus} taskID={this._task.TaskID}");
    (this._dbTask as DBTask).SetStatus(e.NewStatus);
    if (e.NewStatus == TaskStatus.Successfully)
    {
      (this._dbTask as DBTask).SetPercent(99.0);
      this._tasksQueue.Storage.RemoveTask(e.Session, (sender as Task).TaskID);
    }
    else if (e.NewStatus == TaskStatus.Erroneous)
    {
      if ((sender as Task).Error != null)
        (this._dbTask as DBTask).SetError((sender as Task).Error);
      this.GenerateNotification((sender as Task).Type, (sender as Task).Error);
    }
    else
    {
      if (!e.PercentChanged)
        return;
      (this._dbTask as DBTask).SetPercent(e.Percent);
    }
  }

  private void GenerateNotification(TaskType type, Exception exception)
  {
    IEmailService customService = (IEmailService) this._session.GetCustomService(typeof (IEmailService));
    TaskNotifications notifications = TaskNotifications.GetNotifications(this._session, type);
    if (notifications.Notifications.Count <= 0)
      return;
    string subject = string.Format(LocalizationHolder.rm.GetString("Kernel_1081"), (object) this._dbTask.NameInMessages);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("Kernel_1157"), (object) this._dbTask.ObjectID, (object) EnumDescConverter.GetEnumDescription((Enum) type)));
    stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("Kernel_1082"), (object) DateTime.UtcNow));
    stringBuilder.AppendLine(LocalizationHolder.rm.GetString("Kernel_1083"));
    stringBuilder.AppendLine(exception.Message);
    stringBuilder.AppendLine("StackTrace:");
    stringBuilder.AppendLine(exception.StackTrace);
    try
    {
      Guid accauntSender = TaskNotifications.GetAccauntSender(this._session);
      if (accauntSender == Guid.Empty)
        throw new Exception(LocalizationHolder.rm.GetString("Kernel_1084"));
      for (int index = 0; index < notifications.Notifications.Count; ++index)
      {
        TaskNotification notification = notifications.Notifications[index];
        if (notification.Enable)
          customService.SendMessage(this._session.SessionGUID, accauntSender, notification.Email, subject, stringBuilder.ToString());
      }
    }
    catch (Exception ex)
    {
      TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1085"), (object) ex.Message));
      TasksHelper.AddMessageToLog($"StackTrace: {ex.StackTrace}");
    }
  }
}
