// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPCompositionBaseTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MRP;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.MRP.Server;

internal abstract class MRPCompositionBaseTask : IMRPCompositionTask, IMRPContext
{
  protected volatile MRPCompositionTaskState state;
  protected volatile Exception exception;
  public volatile string Name = string.Empty;
  protected Guid taskID = Guid.NewGuid();
  protected volatile IMRPCompositionTask masterTask;
  protected Guid actionsID = Guid.Empty;
  protected LinkedList<IMRPAction> actions = new LinkedList<IMRPAction>();
  protected object syncRoot = new object();
  protected Guid SessionGuid;
  protected AdvancedServiceContainer services = new AdvancedServiceContainer();

  public MRPCompositionBaseTask(
    string taskName,
    IServiceProvider services,
    IMRPCompositionTask masterTask)
  {
    this.Name = taskName;
    this.services.AdvancedProvider = services;
    this.MasterTask = masterTask;
    if (this.MasterTask != null)
      return;
    this.ActionsID = Guid.NewGuid();
  }

  public override bool Equals(object obj)
  {
    return !(obj is MRPCompositionBaseTask compositionBaseTask) ? base.Equals(obj) : this.TaskID.Equals(compositionBaseTask.TaskID);
  }

  public override int GetHashCode() => this.TaskID.GetHashCode();

  protected virtual void AddSession(IUserSession session)
  {
    IServiceProvider advancedProvider = this.services.AdvancedProvider;
    this.services.AdvancedProvider = (IServiceProvider) null;
    this.services.AddService(typeof (IUserSession), (object) session);
    this.services.AdvancedProvider = advancedProvider;
  }

  protected virtual void RemoveSession()
  {
    IServiceProvider advancedProvider = this.services.AdvancedProvider;
    this.services.AdvancedProvider = (IServiceProvider) null;
    if (this.services.GetService(typeof (IUserSession)) != null)
      this.services.RemoveService(typeof (IUserSession));
    this.services.AdvancedProvider = advancedProvider;
  }

  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this.services;
    set
    {
      lock (this.syncRoot)
        this.services.AdvancedProvider = value;
    }
  }

  public virtual MRPCompositionTaskState State
  {
    [DebuggerStepThrough] get => this.state;
    set => this.state = value;
  }

  public virtual Exception Exception
  {
    [DebuggerStepThrough] get => this.exception;
    set => this.exception = value;
  }

  public virtual Guid TaskID
  {
    [DebuggerStepThrough] get => this.taskID;
  }

  public virtual Guid ActionsID
  {
    get
    {
      if (this.MasterTask != null)
        return this.MasterTask.ActionsID;
      lock (this.syncRoot)
        return this.actionsID;
    }
    set
    {
      lock (this.syncRoot)
        this.actionsID = value;
    }
  }

  public virtual LinkedList<IMRPAction> Actions
  {
    get
    {
      lock (this.syncRoot)
        return new LinkedList<IMRPAction>((IEnumerable<IMRPAction>) this.actions);
    }
  }

  public virtual IMRPCompositionTask MasterTask
  {
    [DebuggerStepThrough] get => this.masterTask;
    set => this.masterTask = value;
  }

  public abstract void Execute(
    Guid sessionGuid,
    IServiceProvider services,
    MRPTaskCompleteEventHandler completeHandler,
    MRPTaskCancelEventHandler cancelHandler);
}
