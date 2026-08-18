// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.CustomPortalScheduledTasks
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Configuration;
using System.IO;


namespace Intermech.Kernel.Services.PortalServices;

internal class CustomPortalScheduledTasks : DBCustomManualScheduledService
{
  protected PortalTasksQueue tasksQueue;
  protected TaskPriority? priority;
  private readonly string _name;
  private Guid _guid;

  public CustomPortalScheduledTasks(
    UserSession session,
    string sessionName,
    PortalTasksQueue tasksQueue,
    TaskPriority? taskPriority,
    string name,
    Guid guid)
    : base(session, sessionName)
  {
    this.tasksQueue = tasksQueue;
    this.priority = taskPriority;
    this._name = name;
    this._guid = guid;
  }

  public override Guid GUID => this._guid;

  public override string ServiceName => this._name;

  protected static void WriteErrorToTask(IUserSession session, long taskID, Exception ex)
  {
    TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1069"), (object) taskID, (object) ex.Message));
    string empty = string.Empty;
    string currentDirectory;
    try
    {
      currentDirectory = ConfigurationManager.AppSettings.Get("LogPath");
      if (currentDirectory != null)
      {
        if (!(currentDirectory == string.Empty))
          goto label_5;
      }
      currentDirectory = Environment.CurrentDirectory;
    }
    catch
    {
      currentDirectory = Environment.CurrentDirectory;
    }
label_5:
    string path = Path.Combine(currentDirectory, $"taskError{taskID}_{Guid.NewGuid()}.log");
    using (FileStream fileStream = File.Create(path))
    {
      try
      {
        if (ExceptionHelper.ExceptionToXML(ex, (IPluginManager) null).Save((Stream) fileStream))
          TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1071"), (object) path));
        else
          TasksHelper.AddMessageToLog(LocalizationHolder.rm.GetString("Kernel_1072"));
      }
      catch (Exception ex1)
      {
        TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1073"), (object) Helper.FormingLogError(ex1)));
      }
    }
  }

  public static void StartCustomTask(
    IUserSession session,
    PortalTasksQueue tasksQueue,
    long taskID)
  {
    string sessionName = $"CustomPortalScheduledTasks.Start_{Guid.NewGuid()}";
    IUserSession cloneSession = PortalServicesSessionHelper.GetCloneSession(session, sessionName, "CustomPortalScheduledTasks.StartCustomTask", true);
    try
    {
      IDBObject taskObject;
      ITask taskById = TasksHelper.GetTaskByID(cloneSession, tasksQueue, taskID, out taskObject);
      new TaskWorkspace(cloneSession, tasksQueue, taskById, taskObject).BeginTask();
    }
    catch (Exception ex)
    {
      CustomPortalScheduledTasks.WriteErrorToTask(cloneSession, taskID, ex);
    }
    finally
    {
      PortalServicesSessionHelper.LogoutSession(cloneSession, sessionName, "CustomPortalScheduledTasks.StartCustomTask");
    }
  }
}
