// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPDestroyQueueTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces.MRP;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPDestroyQueueTask(
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
      try
      {
        if (!service.AutoComplete)
          return;
        while (service.InProcess > 1)
          Thread.Sleep(50);
        (service as MRPTasksQueue).NavigatorEvents = this.Services.GetService(typeof (MRPNavigatorEventsRef)) as MRPNavigatorEventsRef;
        (service as MRPTasksQueue).ReleaseSession();
      }
      catch (Exception ex)
      {
        if (service != null)
          (service as MRPTasksQueue).NavigatorEvents = (MRPNavigatorEventsRef) null;
        lock (this.syncRoot)
        {
          this.Exception = ex;
          this.State = MRPCompositionTaskState.Error;
        }
      }
    }
    finally
    {
      lock (this.syncRoot)
      {
        if (this.Exception == null && this.State != MRPCompositionTaskState.Cancelled)
        {
          if (completeHandler != null)
            completeHandler((IMRPCompositionTask) this, (LinkedList<IMRPAction>) null, (LinkedList<IMRPCompositionTask>) null);
        }
        else
        {
          if (this.State != MRPCompositionTaskState.Error)
            this.State = MRPCompositionTaskState.Cancelled;
          if (cancelHandler != null)
            cancelHandler((IMRPCompositionTask) this, (LinkedList<IMRPAction>) null, (LinkedList<IMRPCompositionTask>) null);
        }
      }
    }
  }
}
