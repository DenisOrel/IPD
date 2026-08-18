// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPTechRoutesChangeTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPTechRoutesChangeTask : MRPCompositionBaseTask
{
  private RelationPair rootObject;
  private RelationPath rootObjectPath;
  private long projObj;
  private ManufactureOrderHolder holder;

  public MRPTechRoutesChangeTask(
    string taskName,
    IServiceProvider services,
    IMRPCompositionTask masterTask,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    long projObj,
    ManufactureOrderHolder holder)
    : base(taskName, services, masterTask)
  {
    this.rootObject = rootObject;
    this.rootObjectPath = rootObjectPath;
    this.projObj = projObj;
    this.holder = holder;
  }

  public override void Execute(
    Guid sessionGuid,
    IServiceProvider services,
    MRPTaskCompleteEventHandler completeHandler,
    MRPTaskCancelEventHandler cancelHandler)
  {
    serverSession = (IServerSession) null;
    try
    {
      this.SessionGuid = sessionGuid;
      this.Services = services;
      IPdmConfiguratorService service1 = ServerServices.GetService(typeof (IPdmConfiguratorService)) as IPdmConfiguratorService;
      IMRPTasksQueue service2 = this.Services.GetService(typeof (IMRPTasksQueue)) as IMRPTasksQueue;
      lock (this.syncRoot)
      {
        if (this.State != MRPCompositionTaskState.NotStarted)
          return;
        this.State = MRPCompositionTaskState.Working;
        if (service2 == null)
        {
          this.Exception = (Exception) new ArgumentNullException("IMRPTasksQueue");
          this.State = MRPCompositionTaskState.Error;
          return;
        }
        if (service1 == null)
        {
          this.Exception = (Exception) new ArgumentNullException("IPdmConfiguratorService");
          this.State = MRPCompositionTaskState.Error;
          service2.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
          return;
        }
      }
      if (!(MRPContextHelper.GetContextSession(this.SessionGuid, (IMRPContext) this) is IServerSession serverSession))
      {
        this.Exception = (Exception) new KernelExceptionID(210, (object) "MRPTechRoutesChangeTask.Execute");
        this.State = MRPCompositionTaskState.Error;
        service2.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
      }
      else
      {
        try
        {
          service2.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.10");
          if (!serverSession.SessionGUID.Equals(this.SessionGuid))
            serverSession = serverSession.Clone(true, "MRPTechRoutes.Execute") as IServerSession;
          this.AddSession((IUserSession) serverSession);
          lock (this.syncRoot)
          {
            if (service2.IsBreaked)
            {
              this.State = MRPCompositionTaskState.Cancelled;
              return;
            }
          }
          if (this.holder == null || this.projObj == 0L || this.rootObject == null || this.rootObject.TOP_OBJECT_ID == 0L || this.rootObject.TOP_OBJECT_TYPE == -1)
          {
            this.State = MRPCompositionTaskState.Completed;
          }
          else
          {
            RelationPair relationPair = (RelationPair) null;
            if (this.rootObjectPath != null && !this.rootObjectPath.Empty)
            {
              for (int index = 0; index < this.rootObjectPath.Items.Count; ++index)
              {
                SimpleRelationPair simpleRelationPair = this.rootObjectPath.Items[index];
                if (!simpleRelationPair.Empty)
                {
                  RelationPair key = Helper.CreateKey(this.rootObject.Handle != 0L ? this.rootObject.Handle : serverSession.ClientConnectionID, this.rootObject.TOP_OBJECT_ID, this.rootObject.TOP_OBJECT_TYPE, this.rootObject.USER_ID != 0L ? this.rootObject.USER_ID : serverSession.UserID, simpleRelationPair.F_PRJLINK_ID, simpleRelationPair.F_RELATION_TYPE, simpleRelationPair.F_PART_ID, simpleRelationPair.F_OBJECT_TYPE);
                  PdmConfiguratorContext configuratorContext;
                  if (simpleRelationPair.F_PRJLINK_ID == 0L && simpleRelationPair.F_PART_ID != 0L && MetaDataHelper.IsPdmRootObjectType(simpleRelationPair.F_OBJECT_TYPE))
                  {
                    IDBObject source = serverSession.GetObject(simpleRelationPair.F_PART_ID, false);
                    if (source != null)
                    {
                      configuratorContext = new PdmConfiguratorContext((object) string.Empty);
                      configuratorContext.Key = key;
                      configuratorContext.ParentKey = relationPair;
                      configuratorContext.Assign((object) source);
                    }
                    else
                      continue;
                  }
                  else
                  {
                    IDBRelation relation = serverSession.GetRelation(simpleRelationPair.F_PRJLINK_ID, false);
                    if (relation != null)
                      configuratorContext = new PdmConfiguratorContext((object) relation);
                    else
                      continue;
                  }
                  if (configuratorContext == null)
                    configuratorContext = new PdmConfiguratorContext((object) string.Empty);
                  configuratorContext.Key = key;
                  configuratorContext.ParentKey = relationPair;
                  service1[(object) (serverSession as UserSession), key] = configuratorContext;
                  relationPair = key;
                }
              }
            }
            this.rootObject = relationPair ?? this.rootObject;
            IDBObject source1 = serverSession.GetObject(this.projObj, false);
            if (source1 == null)
              return;
            MRPCompositionObject projObject = new MRPCompositionObject((object) source1);
            RelationPath source2 = new RelationPath(false);
            RelationPair key1 = Helper.CreateKey(this.rootObject.Handle != 0L ? this.rootObject.Handle : serverSession.ClientConnectionID, this.rootObject.TOP_OBJECT_ID, this.rootObject.TOP_OBJECT_TYPE, this.rootObject.USER_ID != 0L ? this.rootObject.USER_ID : serverSession.UserID, projObject.F_PRJLINK_ID, projObject.F_RELATION_TYPE, projObject.F_OBJECT_ID, projObject.F_OBJECT_TYPE);
            if (key1 != null && key1.TOP_OBJECT_ID != 0L && key1.TOP_OBJECT_TYPE != -1)
              source2.Items.Add(new SimpleRelationPair(0L, -1, key1.TOP_OBJECT_ID, key1.TOP_OBJECT_TYPE));
            SimpleRelationPair simpleRelationPair1 = new SimpleRelationPair(projObject.F_PRJLINK_ID, projObject.F_PRJLINK_ID != 0L ? projObject.F_RELATION_TYPE : -1, projObject.F_OBJECT_ID, projObject.F_OBJECT_TYPE, false);
            RelationPath rootObjectPath = new RelationPath((object) source2);
            if (rootObjectPath.Items.IndexOf(simpleRelationPair1) < 0)
              rootObjectPath.Items.Add(simpleRelationPair1);
            MRPTechRouteChangeTask task = new MRPTechRouteChangeTask(this.Name, this.Services, (IMRPCompositionTask) this, this.rootObject, rootObjectPath, projObject, this.holder);
            service2.EnqueueTask((IMRPCompositionTask) task);
          }
        }
        catch (Exception ex)
        {
          lock (this.syncRoot)
          {
            this.Exception = ex;
            this.State = MRPCompositionTaskState.Error;
            service2.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
          }
        }
      }
    }
    finally
    {
      this.RemoveSession();
      if (!serverSession.SessionGUID.Equals(this.SessionGuid))
        serverSession.Logout("MRPTechRoutes.Execute");
      lock (this.syncRoot)
      {
        if (this.Exception == null && this.State != MRPCompositionTaskState.Cancelled)
        {
          if (completeHandler != null)
            completeHandler((IMRPCompositionTask) this, this.actions, (LinkedList<IMRPCompositionTask>) null);
        }
        else
        {
          if (this.State != MRPCompositionTaskState.Error)
            this.State = MRPCompositionTaskState.Cancelled;
          if (cancelHandler != null)
            cancelHandler((IMRPCompositionTask) this, this.actions, (LinkedList<IMRPCompositionTask>) null);
        }
      }
    }
  }
}
