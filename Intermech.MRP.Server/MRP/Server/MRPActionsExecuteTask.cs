// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPActionsExecuteTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPActionsExecuteTask : MRPCompositionBaseTask
{
  private LinkedList<IMRPAction> actionsList;

  public MRPActionsExecuteTask(
    string taskName,
    IServiceProvider services,
    IMRPCompositionTask masterTask,
    IMRPAction action)
    : base(taskName, services, masterTask)
  {
    if (action == null)
      return;
    this.actionsList = new LinkedList<IMRPAction>();
    this.actionsList.AddLast(action);
  }

  public MRPActionsExecuteTask(
    string taskName,
    IServiceProvider services,
    IMRPCompositionTask masterTask,
    LinkedList<IMRPAction> actions)
    : base(taskName, services, masterTask)
  {
    this.actionsList = actions;
  }

  public override void Execute(
    Guid sessionGuid,
    IServiceProvider services,
    MRPTaskCompleteEventHandler completeHandler,
    MRPTaskCancelEventHandler cancelHandler)
  {
    session = (IServerSession) null;
    LinkedList<IMRPCompositionTask> advancedTasks = new LinkedList<IMRPCompositionTask>();
    try
    {
      this.SessionGuid = sessionGuid;
      this.Services = services;
      IMRPTasksQueue service = this.Services.GetService(typeof (IMRPTasksQueue)) as IMRPTasksQueue;
      lock (this.syncRoot)
      {
        if (this.State != MRPCompositionTaskState.NotStarted || this.actionsList == null || this.actionsList.Count == 0)
          return;
        this.State = MRPCompositionTaskState.Working;
        if (service == null)
        {
          this.Exception = (Exception) new ArgumentNullException("IMRPTasksQueue");
          this.State = MRPCompositionTaskState.Error;
          return;
        }
      }
      if (!(MRPContextHelper.GetContextSession(this.SessionGuid, (IMRPContext) this) is IServerSession session))
      {
        this.Exception = (Exception) new KernelExceptionID(210, (object) "MRPActionsExecuteTask.Execute");
        this.State = MRPCompositionTaskState.Error;
      }
      else
      {
        try
        {
          if (!session.SessionGUID.Equals(this.SessionGuid))
            session = session.Clone(true, "MRPActions.Execute") as IServerSession;
          this.AddSession((IUserSession) session);
          IDBTransactions customService = session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
          try
          {
            session.DBObjectsCacheStart();
            foreach (IMRPAction actions in this.actionsList)
            {
              customService.StartTransaction();
              try
              {
                actions.Execute(this.Services);
                customService.Commit();
              }
              catch
              {
                customService.Rollback();
                throw;
              }
              lock (this.syncRoot)
              {
                if (service.IsBreaked)
                {
                  this.State = MRPCompositionTaskState.Cancelled;
                  break;
                }
              }
            }
          }
          finally
          {
            session.DBObjectsCacheStop();
          }
        }
        catch (Exception ex)
        {
          lock (this.syncRoot)
          {
            this.Exception = ex;
            this.State = MRPCompositionTaskState.Error;
          }
          advancedTasks.AddLast((IMRPCompositionTask) new MRPDestroyQueueTask("MRPDestroyQueueTask", this.Services, (IMRPCompositionTask) this));
        }
      }
    }
    finally
    {
      this.RemoveSession();
      if (!session.SessionGUID.Equals(this.SessionGuid))
        session.Logout("MRPActions.Execute");
      lock (this.syncRoot)
      {
        if (this.Exception == null && this.State != MRPCompositionTaskState.Cancelled)
        {
          if (completeHandler != null)
            completeHandler((IMRPCompositionTask) this, (LinkedList<IMRPAction>) null, advancedTasks);
        }
        else
        {
          if (this.State != MRPCompositionTaskState.Error)
            this.State = MRPCompositionTaskState.Cancelled;
          if (cancelHandler != null)
            cancelHandler((IMRPCompositionTask) this, (LinkedList<IMRPAction>) null, advancedTasks);
        }
      }
    }
  }
}
