// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPCheckInObjectsTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPCheckInObjectsTask(
  string taskName,
  IServiceProvider services,
  IMRPCompositionTask masterTask) : MRPCompositionBaseTask(taskName, services, masterTask)
{
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
        if (this.State != MRPCompositionTaskState.NotStarted)
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
        this.Exception = (Exception) new KernelExceptionID(210, (object) "MRPCheckInObjectsTask.Execute");
        this.State = MRPCompositionTaskState.Error;
        service.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
      }
      else
      {
        dbTransactions = (IDBTransactions) null;
        try
        {
          service.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.5");
          if (!session.SessionGUID.Equals(this.SessionGuid))
            session = session.Clone(true, "MRPCheckin.Execute") as IServerSession;
          this.AddSession((IUserSession) session);
          if (session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions dbTransactions)
            dbTransactions.StartTransaction();
          try
          {
            session.DBObjectsCacheStart();
            new MRPCheckInObjectsAction(this.Services, this.Services.GetService(typeof (IMRPCheckInObjectsRef)) as IMRPCheckInObjectsRef, true).Execute();
          }
          finally
          {
            session.DBObjectsCacheStop();
          }
          if (dbTransactions != null && dbTransactions.InTransaction)
            dbTransactions.Commit();
          advancedTasks.AddLast((IMRPCompositionTask) new MRPFillEditingContextTask("MRPFillEditingContextTask", this.Services, (IMRPCompositionTask) this));
        }
        catch (Exception ex)
        {
          lock (this.syncRoot)
          {
            this.Exception = ex;
            this.State = MRPCompositionTaskState.Error;
            service.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
          }
          dbTransactions?.Rollback();
          advancedTasks.AddLast((IMRPCompositionTask) new MRPDestroyQueueTask("MRPDestroyQueueTask", this.Services, (IMRPCompositionTask) this));
        }
      }
    }
    finally
    {
      this.RemoveSession();
      if (!session.SessionGUID.Equals(this.SessionGuid))
        session.Logout("MRPCheckin.Execute");
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
