// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPFillEditingContextTask
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

internal class MRPFillEditingContextTask(
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
      IMRPTasksQueue service1 = this.Services.GetService(typeof (IMRPTasksQueue)) as IMRPTasksQueue;
      lock (this.syncRoot)
      {
        if (this.State != MRPCompositionTaskState.NotStarted)
          return;
        this.State = MRPCompositionTaskState.Working;
        if (service1 == null)
        {
          this.Exception = (Exception) new ArgumentNullException("IMRPTasksQueue");
          this.State = MRPCompositionTaskState.Error;
          return;
        }
      }
      if (!(MRPContextHelper.GetContextSession(this.SessionGuid, (IMRPContext) this) is IServerSession session))
      {
        this.Exception = (Exception) new KernelExceptionID(210, (object) "MRPFillEditingContextTask.Execute");
        this.State = MRPCompositionTaskState.Error;
        service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
      }
      else
      {
        dbTransactions = (IDBTransactions) null;
        try
        {
          service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.6");
          if (!session.SessionGUID.Equals(this.SessionGuid))
            session = session.Clone(true, "MRPFillEC.Execute") as IServerSession;
          this.AddSession((IUserSession) session);
          if (session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions dbTransactions)
            dbTransactions.StartTransaction();
          IMRPEditingContextRef service2 = this.Services.GetService(typeof (IMRPEditingContextRef)) as IMRPEditingContextRef;
          new MRPAddToContextActionAdv(this.Services, (this.Services.GetService(typeof (ManufactureOrderHolder)) as ManufactureOrderHolder).ObjectID, (IList<long>) service2.Items, (IList<long>) service2.ItemsF_ID, (IList<int>) service2.ItemTypes).Execute();
          if (dbTransactions != null && dbTransactions.InTransaction)
            dbTransactions.Commit();
          service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.7");
        }
        catch (Exception ex)
        {
          lock (this.syncRoot)
          {
            this.Exception = ex;
            this.State = MRPCompositionTaskState.Error;
            service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
          }
          dbTransactions?.Rollback();
        }
      }
    }
    finally
    {
      this.RemoveSession();
      advancedTasks.AddLast((IMRPCompositionTask) new MRPDestroyQueueTask("MRPDestroyQueueTask", this.Services, (IMRPCompositionTask) this));
      if (!session.SessionGUID.Equals(this.SessionGuid))
        session.Logout("MRPFillEC.Execute");
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
