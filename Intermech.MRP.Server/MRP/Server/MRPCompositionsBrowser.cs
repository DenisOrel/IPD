// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPCompositionsBrowser
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.MRP;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPCompositionsBrowser : 
  LongLifeObject,
  IMRPCompositionsServerBrowser,
  IMRPCompositionsBrowser
{
  private object syncRoot = new object();
  private Dictionary<Guid, IMRPTasksQueue> tasks = new Dictionary<Guid, IMRPTasksQueue>();

  private IUserSession GetUserSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession));
      default:
        return (IUserSession) null;
    }
  }

  private UserSession GetServerSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as UserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID) as UserSession;
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession)) as UserSession;
      default:
        return (UserSession) null;
    }
  }

  public Guid StartActionsCreateJob(
    Guid sessionGuid,
    ManufactureOrderHolder holder,
    int threadsCount,
    bool autoComplete)
  {
    UserSession serverSession = this.GetServerSession((object) sessionGuid);
    if (serverSession == null)
      throw new KernelExceptionID(210, (object) "MRPCompositionsBrowser.StartActionsCreateJob");
    if (holder == null)
      throw new ArgumentNullException(nameof (holder), LocalizationHolder.rm.GetString("MRP.Server_4"));
    CurrentEditingContext editingContext = CurrentEditingContext.Dummy;
    if (holder != null && holder.FiltrationSettings != null && holder.FiltrationSettings.EditingContext != null)
      editingContext = holder.FiltrationSettings.EditingContext;
    AdvancedServiceContainer services = new AdvancedServiceContainer();
    services.AddService(typeof (ManufactureOrderHolder), (object) holder);
    MRPServerSessionHelper serviceInstance1 = new MRPServerSessionHelper();
    services.AddService(typeof (IMRPUserSessionHelper), (object) serviceInstance1);
    MRPEditingContextRef serviceInstance2 = new MRPEditingContextRef((IServiceProvider) services);
    services.AddService(typeof (IMRPEditingContextRef), (object) serviceInstance2);
    MRPCheckInObjectsRef serviceInstance3 = new MRPCheckInObjectsRef((IServiceProvider) services);
    services.AddService(typeof (IMRPCheckInObjectsRef), (object) serviceInstance3);
    MRPParsedLinks serviceInstance4 = new MRPParsedLinks();
    services.AddService(typeof (MRPParsedLinks), (object) serviceInstance4);
    MRPNavigatorEventsRef serviceInstance5 = new MRPNavigatorEventsRef((IServiceProvider) services);
    services.AddService(typeof (MRPNavigatorEventsRef), (object) serviceInstance5);
    services.AddService(typeof (IMRPNavigatorEventsRef), (object) serviceInstance5);
    services.AddService(typeof (MRPContextOptionsHolder), (object) new MRPContextOptionsHolder(MRPContextOptions.None));
    IMRPTasksQueue tasksQueue = this.CreateTasksQueue(serverSession.SessionGUID, (IServiceProvider) services, editingContext, threadsCount, autoComplete);
    tasksQueue.EnqueueTask((IMRPCompositionTask) new MRPCompositionsBrowseTask("", (IServiceProvider) null, (IMRPCompositionTask) null, (holder.FiltrationSettings == null || holder.FiltrationSettings.Tags == null ? (RelationPair) null : holder.FiltrationSettings.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] as RelationPair) ?? new RelationPair(serverSession.ClientConnectionID, holder.ObjectID, holder.ObjectType, 0L, serverSession.UserID, holder.ObjectID, -1, holder.ObjectType), new RelationPath(), holder.ObjectID, holder));
    return tasksQueue.QueueGuid;
  }

  public Guid StartTechRouteChangeJob(
    Guid sessionGuid,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    long projObj,
    ManufactureOrderHolder holder,
    int threadsCount,
    bool autoComplete)
  {
    UserSession serverSession = this.GetServerSession((object) sessionGuid);
    if (serverSession == null)
      throw new KernelExceptionID(210, (object) "MRPCompositionsBrowser.StartTechRouteChangeJob");
    if (holder == null)
      throw new ArgumentNullException(nameof (holder), LocalizationHolder.rm.GetString("MRP.Server_4"));
    if (rootObject == null)
      throw new ArgumentNullException(nameof (rootObject), LocalizationHolder.rm.GetString("MRP.Server_4"));
    if (rootObjectPath == null)
      throw new ArgumentNullException(nameof (rootObjectPath), LocalizationHolder.rm.GetString("MRP.Server_4"));
    CurrentEditingContext editingContext = CurrentEditingContext.Dummy;
    if (holder != null && holder.FiltrationSettings != null && holder.FiltrationSettings.EditingContext != null)
      editingContext = holder.FiltrationSettings.EditingContext;
    AdvancedServiceContainer services = new AdvancedServiceContainer();
    services.AddService(typeof (ManufactureOrderHolder), (object) holder);
    MRPServerSessionHelper serviceInstance1 = new MRPServerSessionHelper();
    services.AddService(typeof (IMRPUserSessionHelper), (object) serviceInstance1);
    MRPEditingContextRef serviceInstance2 = new MRPEditingContextRef((IServiceProvider) services);
    services.AddService(typeof (IMRPEditingContextRef), (object) serviceInstance2);
    MRPCheckInObjectsRef serviceInstance3 = new MRPCheckInObjectsRef((IServiceProvider) services);
    services.AddService(typeof (IMRPCheckInObjectsRef), (object) serviceInstance3);
    MRPParsedLinks serviceInstance4 = new MRPParsedLinks();
    services.AddService(typeof (MRPParsedLinks), (object) serviceInstance4);
    MRPNavigatorEventsRef serviceInstance5 = new MRPNavigatorEventsRef((IServiceProvider) services);
    services.AddService(typeof (MRPNavigatorEventsRef), (object) serviceInstance5);
    services.AddService(typeof (IMRPNavigatorEventsRef), (object) serviceInstance5);
    services.AddService(typeof (MRPContextOptionsHolder), (object) new MRPContextOptionsHolder(MRPContextOptions.None));
    IMRPTasksQueue tasksQueue = this.CreateTasksQueue(serverSession.SessionGUID, (IServiceProvider) services, editingContext, threadsCount, autoComplete);
    tasksQueue.EnqueueTask((IMRPCompositionTask) new MRPTechRoutesChangeTask("", (IServiceProvider) null, (IMRPCompositionTask) null, rootObject, rootObjectPath, projObj, holder));
    return tasksQueue.QueueGuid;
  }

  public MRPTasksQueueState GetJobState(Guid jobID) => this.GetTasksQueue(jobID)?.State;

  public void CancelJob(Guid jobID) => this.RemoveTasksQueue(jobID);

  public LinkedList<IMRPAction> GetActions(Guid actionsID) => throw new NotImplementedException();

  public IMRPTasksQueue CreateTasksQueue(
    Guid sessionGuid,
    IServiceProvider services,
    CurrentEditingContext editingContext,
    int threadsCount,
    bool autoComplete)
  {
    IMRPTasksQueue tasksQueue = (IMRPTasksQueue) new MRPTasksQueue(sessionGuid, services, editingContext, threadsCount, autoComplete, (IMRPCompositionTask) new MRPActionsOfMasterTask("MRPActionsOfMasterTask", services, (IMRPCompositionTask) null));
    lock (this.syncRoot)
      this.tasks[tasksQueue.QueueGuid] = tasksQueue;
    return tasksQueue;
  }

  public IMRPTasksQueue GetTasksQueue(Guid queueGuid)
  {
    lock (this.syncRoot)
    {
      if (this.tasks.ContainsKey(queueGuid))
        return this.tasks[queueGuid];
    }
    return (IMRPTasksQueue) null;
  }

  public bool RemoveTasksQueue(Guid queueGuid)
  {
    IMRPTasksQueue mrpTasksQueue = (IMRPTasksQueue) null;
    lock (this.syncRoot)
    {
      if (this.tasks.ContainsKey(queueGuid))
      {
        mrpTasksQueue = this.tasks[queueGuid];
        this.tasks.Remove(queueGuid);
      }
    }
    if (mrpTasksQueue == null)
      return false;
    mrpTasksQueue.IsBreaked = true;
    mrpTasksQueue.EnqueueTask((IMRPCompositionTask) null);
    return true;
  }

  public bool RemoveTasksQueue(IMRPTasksQueue queue)
  {
    if (queue == null)
      return false;
    lock (this.syncRoot)
    {
      if (!this.tasks.ContainsKey(queue.QueueGuid))
        return false;
      this.tasks.Remove(queue.QueueGuid);
    }
    queue.IsBreaked = true;
    queue.EnqueueTask((IMRPCompositionTask) null);
    return true;
  }
}
