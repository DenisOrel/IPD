// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPTasksQueue
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.MRP.Server;

internal sealed class MRPTasksQueue : IDisposable, IMRPTasksQueue, IMRPContext, IMRPProgress
{
  private object syncRoot = new object();
  private object syncRootFields = new object();
  private volatile bool advancedTaskExecuted;
  private volatile IMRPCompositionTask advancedTask;
  private volatile bool autoComplete = true;
  private volatile int inProcess;
  private volatile int processedTasks;
  private volatile int cancelledTasks;
  private volatile int skippedTasks;
  private volatile int queuedTasks;
  private volatile int totalTasks;
  private volatile int nestedTasks;
  private volatile bool isBreaked;
  private volatile bool isDisposed;
  private volatile bool inExecute;
  private Thread[] threads;
  private Guid queueGuid = Guid.NewGuid();
  private Guid sessionGuid = Guid.Empty;
  private volatile string taskOperation = string.Empty;
  private volatile int minProgress;
  private volatile int maxProgress = 100;
  private volatile int progress;
  private volatile MRPNavigatorEventsRef navigatorEvents;
  private volatile Exception exception;
  private AdvancedServiceContainer services = new AdvancedServiceContainer();
  private Queue<IMRPCompositionTask> tasks = new Queue<IMRPCompositionTask>();
  private Dictionary<Guid, MRPIntermediateTaskResult> results = new Dictionary<Guid, MRPIntermediateTaskResult>();
  private List<Guid> tasksInExecutor = new List<Guid>(16 /*0x10*/);
  private CurrentEditingContext editingContext = CurrentEditingContext.Dummy;

  public MRPTasksQueue(
    Guid sessionGuid,
    IServiceProvider services,
    CurrentEditingContext editingContext)
    : this(sessionGuid, services, editingContext, -1, true, (IMRPCompositionTask) null)
  {
  }

  public MRPTasksQueue(
    Guid sessionGuid,
    IServiceProvider services,
    CurrentEditingContext editingContext,
    int threadsCount)
    : this(sessionGuid, services, editingContext, threadsCount, true, (IMRPCompositionTask) null)
  {
  }

  public MRPTasksQueue(
    Guid sessionGuid,
    IServiceProvider services,
    CurrentEditingContext editingContext,
    bool autoComplete)
    : this(sessionGuid, services, editingContext, -1, autoComplete, (IMRPCompositionTask) null)
  {
  }

  public MRPTasksQueue(
    Guid sessionGuid,
    IServiceProvider services,
    CurrentEditingContext editingContext,
    int threadsCount,
    bool autoComplete,
    IMRPCompositionTask advancedTask)
  {
    this.taskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.1");
    this.advancedTask = advancedTask;
    this.autoComplete = autoComplete;
    this.services.AddService(typeof (IMRPTasksQueue), (object) this);
    this.services.AddService(typeof (IMRPProgress), (object) this);
    this.services.AdvancedProvider = services;
    if (!(MRPContextHelper.GetContextSession(sessionGuid, (IMRPContext) this) is UserSession contextSession))
      throw new KernelExceptionID(210, (object) nameof (MRPTasksQueue));
    if (editingContext != null)
      this.editingContext = editingContext;
    this.taskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.2");
    UserSession userSession = contextSession.Clone(true, nameof (MRPTasksQueue)) as UserSession;
    this.sessionGuid = userSession.SessionGUID;
    this.services.AddService(typeof (MRPSessionGuidHolder), (object) new MRPSessionGuidHolder(userSession.SessionGUID));
    threadsCount = threadsCount > 0 ? threadsCount : userSession.MaxTaskThreadsCount;
    this.threads = new Thread[threadsCount];
    string str = $"{DateTime.Now.Hour:D2}:{DateTime.Now.Minute:D2}:{DateTime.Now.Second:D2}.";
    for (int index = 0; index < threadsCount; ++index)
    {
      this.threads[index] = new Thread(this.editingContext.SendToThread(new ThreadStart(this.Executor)));
      this.threads[index].Priority = ThreadPriority.Lowest;
      this.threads[index].Name = $"MRPTasksQueue.{str}{this.QueueGuid.ToString()}.{index.ToString()}";
      this.threads[index].IsBackground = true;
      this.threads[index].Start();
    }
  }

  public void ReleaseSession()
  {
    this.isDisposed = true;
    MRPSessionGuidHolder service = this.Services.GetService(typeof (MRPSessionGuidHolder)) as MRPSessionGuidHolder;
    IUserSession contextSession = MRPContextHelper.GetContextSession(this.sessionGuid, (IMRPContext) this);
    if (contextSession != null)
      (contextSession as UserSession).Logout(nameof (MRPTasksQueue));
    if (service != null)
      service.Enabled = false;
    lock (this.syncRoot)
    {
      this.tasks = (Queue<IMRPCompositionTask>) null;
      this.threads = (Thread[]) null;
      this.advancedTask = (IMRPCompositionTask) null;
      this.minProgress = 0;
      this.maxProgress = 100;
      this.progress = 100;
      this.IsBreaked = true;
    }
  }

  public void Dispose()
  {
    if (this.IsDisposed)
      return;
    try
    {
      foreach (Thread thread in this.threads)
        this.EnqueueTask((IMRPCompositionTask) null);
      this.IsBreaked = true;
      foreach (Thread thread in this.threads)
        thread.Join();
    }
    finally
    {
      this.ReleaseSession();
    }
  }

  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this.services;
    set => this.services.AdvancedProvider = value;
  }

  public MRPTasksQueueState State
  {
    get
    {
      lock (this.syncRoot)
      {
        lock (this.syncRootFields)
          return new MRPTasksQueueState(this.IsDisposed, this.AutoComplete, this.InQueue, this.InProcess, this.ProcessedTasks, this.CancelledTasks, this.SkippedTasks, this.TotalTasks, this.NestedTasks, this.IsBreaked, this.QueueGuid, this.SessionGuid, this.TaskOperation, this.MinProgress, this.MaxProgress, this.Progress, this.Exception, this.NavigatorEvents);
      }
    }
  }

  public bool IsDisposed
  {
    [DebuggerStepThrough] get => this.isDisposed;
  }

  public bool AutoComplete
  {
    [DebuggerStepThrough] get => this.autoComplete;
    set
    {
      this.autoComplete = value;
      if (!this.autoComplete || this.InQueue != 0)
        return;
      this.EnqueueTask((IMRPCompositionTask) null);
    }
  }

  public int InQueue => this.queuedTasks;

  public int InProcess
  {
    [DebuggerStepThrough] get => this.inProcess;
  }

  public int ProcessedTasks
  {
    [DebuggerStepThrough] get => this.processedTasks;
  }

  public int CancelledTasks
  {
    [DebuggerStepThrough] get => this.cancelledTasks;
  }

  public int SkippedTasks
  {
    [DebuggerStepThrough] get => this.skippedTasks;
  }

  public int TotalTasks
  {
    [DebuggerStepThrough] get => this.totalTasks;
  }

  public int NestedTasks
  {
    [DebuggerStepThrough] get => this.nestedTasks;
  }

  public bool IsBreaked
  {
    [DebuggerStepThrough] get => this.isBreaked;
    set => this.isBreaked = value;
  }

  public Guid QueueGuid
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRootFields)
        return this.queueGuid;
    }
  }

  public Guid SessionGuid
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRootFields)
        return this.sessionGuid;
    }
  }

  public string TaskOperation
  {
    [DebuggerStepThrough] get => this.taskOperation;
    set => this.taskOperation = value;
  }

  public int MinProgress
  {
    [DebuggerStepThrough] get => this.minProgress;
    set => this.minProgress = value;
  }

  public int MaxProgress
  {
    [DebuggerStepThrough] get => this.maxProgress;
    set => this.maxProgress = value;
  }

  public int Progress
  {
    [DebuggerStepThrough] get => this.progress;
    set => this.progress = value;
  }

  public Exception Exception
  {
    [DebuggerStepThrough] get => this.exception;
    set => this.exception = value;
  }

  public MRPNavigatorEventsRef NavigatorEvents
  {
    [DebuggerStepThrough] get => this.navigatorEvents;
    set => this.navigatorEvents = value;
  }

  public void EnqueueTask(IMRPCompositionTask task)
  {
    if (this.IsBreaked || this.IsDisposed)
      return;
    lock (this.syncRoot)
    {
      if (task != null)
      {
        ++this.totalTasks;
        if (task.MasterTask != null)
          ++this.nestedTasks;
      }
      this.tasks.Enqueue(task);
      this.queuedTasks = this.tasks.Count;
      Monitor.PulseAll(this.syncRoot);
    }
  }

  private void InternalExecute()
  {
    while (this.InProcess > 0 || this.InQueue > 0)
    {
      Thread.Sleep(50);
      if (this.IsBreaked || this.IsDisposed)
        break;
    }
  }

  public void Execute()
  {
    this.inExecute = true;
    try
    {
      this.InternalExecute();
    }
    finally
    {
      if (this.autoComplete)
        this.EnqueueTask((IMRPCompositionTask) null);
      this.inExecute = false;
      if (this.autoComplete)
        this.ReleaseSession();
    }
  }

  public bool HasException(Guid actionsID)
  {
    lock (this.results)
    {
      MRPIntermediateTaskResult result = this.results.ContainsKey(actionsID) ? this.results[actionsID] : (MRPIntermediateTaskResult) null;
      return result != null && result.Exception != null;
    }
  }

  private bool ExecutorsAreEmpty
  {
    get
    {
      lock (this.tasksInExecutor)
        return this.tasksInExecutor.Count == 0;
    }
  }

  private bool TaskInExecutor(Guid taskID)
  {
    lock (this.tasksInExecutor)
      return this.tasksInExecutor.IndexOf(taskID) >= 0;
  }

  private bool EmptyQueue(Guid taskID)
  {
    lock (this.tasksInExecutor)
      return (this.tasksInExecutor.Count == 0 || this.tasksInExecutor.Count == 1 && this.tasksInExecutor[0] == taskID) && this.InQueue == 0;
  }

  private bool EmptyQueue(params IMRPCompositionTask[] tasks)
  {
    List<Guid> taskIDs = new List<Guid>(tasks != null ? tasks.Length : 1);
    if (tasks != null)
    {
      for (int index = 0; index < tasks.Length; ++index)
      {
        if (tasks[index] != null && !(tasks[index].TaskID == Guid.Empty) && taskIDs.IndexOf(tasks[index].TaskID) < 0)
          taskIDs.Add(tasks[index].TaskID);
      }
    }
    return this.EmptyQueue(taskIDs);
  }

  private bool EmptyQueue(List<Guid> taskIDs)
  {
    lock (this.tasksInExecutor)
    {
      if (this.InQueue == 0 && this.tasksInExecutor.Count == 0)
        return true;
      return (this.tasksInExecutor.Count <= 0 || taskIDs != null && taskIDs.Count != 0) && !this.tasksInExecutor.Exists((Predicate<Guid>) (taskID => taskIDs.IndexOf(taskID) < 0));
    }
  }

  private void OnMRPTaskCompleteEventHandler(
    IMRPCompositionTask task,
    LinkedList<IMRPAction> result,
    LinkedList<IMRPCompositionTask> advancedTasks)
  {
    try
    {
      if (task == null || result == null || result.Count == 0)
        return;
      lock (this.results)
      {
        MRPIntermediateTaskResult intermediateTaskResult = this.results.ContainsKey(task.ActionsID) ? this.results[task.ActionsID] : new MRPIntermediateTaskResult(task.ActionsID);
        intermediateTaskResult.MergeWith(result);
        this.results[task.ActionsID] = intermediateTaskResult;
      }
    }
    finally
    {
      if (task != null)
        ++this.processedTasks;
      if (this.autoComplete && this.InQueue == 0 && !this.inExecute && !this.advancedTaskExecuted && this.advancedTask != null)
      {
        if (this.EmptyQueue(task, this.advancedTask))
        {
          this.advancedTaskExecuted = true;
          this.EnqueueTask(this.advancedTask);
          this.advancedTask = (IMRPCompositionTask) null;
        }
      }
      if (this.advancedTaskExecuted && advancedTasks != null && advancedTasks.Count > 0)
      {
        foreach (IMRPCompositionTask advancedTask in advancedTasks)
          this.EnqueueTask(advancedTask);
      }
      if (this.autoComplete && this.InQueue == 0 && !this.inExecute)
      {
        if (this.EmptyQueue(task, this.advancedTask))
          this.IsBreaked = true;
      }
    }
  }

  private void OnMRPTaskCancelEventHandler(
    IMRPCompositionTask task,
    LinkedList<IMRPAction> result,
    LinkedList<IMRPCompositionTask> advancedTasks)
  {
    try
    {
      if (task == null)
        return;
      ++this.cancelledTasks;
      if (task.Exception == null)
        return;
      if (this.exception == null)
        this.exception = task.Exception;
      lock (this.results)
      {
        MRPIntermediateTaskResult intermediateTaskResult = this.results.ContainsKey(task.ActionsID) ? this.results[task.ActionsID] : new MRPIntermediateTaskResult(task.ActionsID, task.Exception);
        intermediateTaskResult.Exception = task.Exception;
        this.results[task.ActionsID] = intermediateTaskResult;
      }
    }
    finally
    {
      if (this.autoComplete && this.InQueue == 0 && !this.inExecute && !this.advancedTaskExecuted && this.advancedTask != null)
      {
        if (this.EmptyQueue(task, this.advancedTask))
        {
          this.advancedTaskExecuted = true;
          this.EnqueueTask(this.advancedTask);
          this.advancedTask = (IMRPCompositionTask) null;
        }
      }
      if (this.advancedTaskExecuted && advancedTasks != null && advancedTasks.Count > 0)
      {
        foreach (IMRPCompositionTask advancedTask in advancedTasks)
          this.EnqueueTask(advancedTask);
      }
      if (this.autoComplete && this.InQueue == 0 && !this.inExecute)
      {
        if (this.EmptyQueue(task, this.advancedTask))
          this.IsBreaked = true;
      }
    }
  }

  private void Executor()
  {
    IUserSession userSession = MRPContextHelper.GetContextSession(this.SessionGuid, (IMRPContext) this);
    if (userSession == null)
      return;
    try
    {
      userSession = (userSession as IServerSession).Clone(true, "MRPServer.Executor");
      while (!this.IsBreaked && !this.IsDisposed)
      {
        IMRPCompositionTask task = (IMRPCompositionTask) null;
        MRPTaskCompleteEventHandler completeHandler = new MRPTaskCompleteEventHandler(this.OnMRPTaskCompleteEventHandler);
        MRPTaskCancelEventHandler cancelHandler = new MRPTaskCancelEventHandler(this.OnMRPTaskCancelEventHandler);
        do
        {
          lock (this.syncRoot)
          {
            while (!this.IsDisposed)
            {
              if (!this.isBreaked)
              {
                if (this.tasks.Count == 0)
                  Monitor.Wait(this.syncRoot, 50);
                else
                  break;
              }
              else
                break;
            }
          }
          if (!this.IsBreaked && !this.IsDisposed)
          {
            if (this.queuedTasks > 0)
            {
              lock (this.syncRoot)
              {
                if (this.tasks.Count > 0)
                {
                  task = this.tasks.Dequeue();
                  this.queuedTasks = this.tasks.Count;
                  if (task != null)
                  {
                    lock (this.tasksInExecutor)
                      this.tasksInExecutor.Add(task.TaskID);
                  }
                }
              }
            }
            if (task != null)
            {
              if (this.IsBreaked || this.IsDisposed)
              {
                lock (this.tasksInExecutor)
                {
                  this.tasksInExecutor.Remove(task.TaskID);
                  return;
                }
              }
            }
            else
              goto label_11;
          }
          else
            goto label_25;
        }
        while (task == null);
        goto label_37;
label_25:
        break;
label_11:
        break;
label_37:
        if (this.HasException(task.ActionsID) && !task.GetType().IsAssignableFrom(typeof (MRPDestroyQueueTask)))
        {
          ++this.skippedTasks;
          lock (this.tasksInExecutor)
            this.tasksInExecutor.Remove(task.TaskID);
        }
        else
        {
          try
          {
            ++this.inProcess;
            try
            {
              task.Execute(userSession.SessionGUID, this.Services, completeHandler, cancelHandler);
            }
            catch (Exception ex)
            {
              lock (this.tasksInExecutor)
                this.tasksInExecutor.Remove(task.TaskID);
              task.State = MRPCompositionTaskState.Error;
              task.Exception = ex;
              cancelHandler(task, (LinkedList<IMRPAction>) null, (LinkedList<IMRPCompositionTask>) null);
            }
          }
          finally
          {
            lock (this.tasksInExecutor)
              this.tasksInExecutor.Remove(task.TaskID);
            --this.inProcess;
          }
        }
      }
    }
    finally
    {
      userSession?.Logout("MRPServer.Executor");
    }
  }

  public Dictionary<Guid, MRPIntermediateTaskResult> GetResults()
  {
    lock (this.results)
      return CloneHelper.Clone((object) this.results) as Dictionary<Guid, MRPIntermediateTaskResult>;
  }
}
