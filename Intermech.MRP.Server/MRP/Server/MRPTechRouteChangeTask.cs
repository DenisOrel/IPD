// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPTechRouteChangeTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPTechRouteChangeTask : MRPCompositionBaseTask
{
  private RelationPair rootObject;
  private RelationPath rootObjectPath;
  private MRPCompositionObject projObject;
  private ManufactureOrderHolder holder;

  public MRPTechRouteChangeTask(
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
    this.projObject = new MRPCompositionObject(projObj, 0L);
    this.holder = holder;
  }

  public MRPTechRouteChangeTask(
    string taskName,
    IServiceProvider services,
    IMRPCompositionTask masterTask,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    MRPCompositionObject projObject,
    ManufactureOrderHolder holder)
    : base(taskName, services, masterTask)
  {
    this.rootObject = rootObject;
    this.rootObjectPath = rootObjectPath;
    this.projObject = projObject;
    this.holder = holder;
  }

  public override void Execute(
    Guid sessionGuid,
    IServiceProvider services,
    MRPTaskCompleteEventHandler completeHandler,
    MRPTaskCancelEventHandler cancelHandler)
  {
    serverSession = (IServerSession) null;
    Guid g = Guid.Empty;
    try
    {
      this.SessionGuid = sessionGuid;
      if (this.services.AdvancedProvider == null)
        this.Services = services;
      IMRPTasksQueue service1 = this.Services.GetService(typeof (IMRPTasksQueue)) as IMRPTasksQueue;
      this.Services.GetService(typeof (MRPParsedLinks));
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
      if (!(MRPContextHelper.GetContextSession(this.SessionGuid, (IMRPContext) this) is IServerSession serverSession))
      {
        this.Exception = (Exception) new KernelExceptionID(210, (object) "MRPTechRouteChangeTask.Execute");
        this.State = MRPCompositionTaskState.Error;
        service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
      }
      else
      {
        try
        {
          service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.10");
          if (!serverSession.SessionGUID.Equals(this.SessionGuid))
          {
            serverSession = serverSession.Clone(true, "MRPTechRoute.Execute") as IServerSession;
            g = serverSession.SessionGUID;
          }
          this.AddSession((IUserSession) serverSession);
          lock (this.syncRoot)
          {
            if (service1.IsBreaked)
            {
              this.State = MRPCompositionTaskState.Cancelled;
              return;
            }
          }
          bool flag1 = MetaDataHelper.IsObjectTypeChildOf(this.projObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00583-306c-11d8-b4e9-00304f19f545"));
          IDBObject dbObject = serverSession.GetObject(this.projObject.F_OBJECT_ID);
          if (dbObject.IsCreationMode)
            dbObject.CommitCreation(false, true);
          HybridDictionary hybridDictionary = new HybridDictionary();
          hybridDictionary[(object) RelationPath.RelationPathGuid] = (object) this.rootObjectPath;
          hybridDictionary[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) this.rootObject;
          MetaDataHelper.GetLCLevelID("cad00011-306c-11d8-b4e9-00304f19f545");
          if (!(ServerServices.GetService(typeof (ICompositionsAutomaticSortingService)) is ICompositionsAutomaticSortingService service2))
            return;
          CompositionsAutosortRule autosortRule = service2.GetAutosortRule((object) serverSession, false);
          if (autosortRule == null)
            return;
          autosortRule.UseEvents = true;
          IMRPSettings service3 = ServerServices.GetService(typeof (IMRPSettings)) as IMRPSettings;
          List<int> visibleRelations = autosortRule.GetObjectTypeVisibleRelations(this.projObject.F_OBJECT_TYPE, true);
          List<IMRPCompositionTask> mrpCompositionTaskList = new List<IMRPCompositionTask>();
          IMRPEditingContextRef service4 = this.Services.GetService(typeof (IMRPEditingContextRef)) as IMRPEditingContextRef;
          ICompositionLoadService customService = serverSession.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
          for (int index1 = 0; index1 < visibleRelations.Count; ++index1)
          {
            if (!flag1 || visibleRelations[index1] == MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"))
            {
              IDBRelationCollection relationCollection = serverSession.GetRelationCollection(visibleRelations[index1], this.holder.FiltrationSettings.OwnerID);
              if (relationCollection != null)
              {
                List<int> compositionTypes = customService.GetPresentCompositionTypes((object) serverSession, (IEnumerable<long>) new long[1]
                {
                  this.projObject.F_OBJECT_ID
                }, visibleRelations[index1], true);
                if (compositionTypes != null && compositionTypes.Count != 0)
                {
                  List<int> intList = MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) compositionTypes);
                  if (intList.Count == 1)
                    relationCollection.ObjectTypeID = intList[0];
                  else
                    relationCollection.ChildObjectTypes = (IList<int>) intList;
                  DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
                  {
                    new ConditionStructure(-21, RelationalOperators.Equal, (object) this.projObject.F_OBJECT_ID, LogicalOperators.NONE, 0, true)
                  }, this.projObject.GetColumnDescriptors(visibleRelations[index1]).ToArray());
                  paramSet.Tags = hybridDictionary;
                  DataTable dataTable;
                  try
                  {
                    paramSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true;
                    paramSet.Tags[(object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}"] = (object) true;
                    paramSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
                    if (this.holder.GetRelationSetting(this.projObject.F_PRJLINK_ID, typeof (SubstitutesItemSettings)) is SubstitutesItemSettings relationSetting)
                      paramSet.Tags[(object) "{7C2D15CB-FD98-4A41-A036-6D3E5AF3FD1B}"] = (object) relationSetting.ActualSubstitutes;
                    paramSet.Tags[(object) "{82E381A1-8952-416A-B303-F81BA2945F8F}"] = (object) true;
                    paramSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) false;
                    paramSet.Tags[(object) "ShowNotOwnedWorkCopies"] = (object) false;
                    dataTable = relationCollection.Select(paramSet);
                  }
                  catch
                  {
                    dataTable = (DataTable) null;
                  }
                  if (dataTable != null && dataTable.Rows.Count != 0)
                  {
                    for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                    {
                      MRPCompositionObject projObject = new MRPCompositionObject(dataTable.Rows[index2]);
                      SimpleRelationPair simpleRelationPair = new SimpleRelationPair(projObject.F_PRJLINK_ID, projObject.F_RELATION_TYPE, projObject.F_OBJECT_ID, projObject.F_OBJECT_TYPE, false);
                      RelationPath rootObjectPath = new RelationPath((object) this.rootObjectPath);
                      rootObjectPath.Items.Add(simpleRelationPair);
                      RelationPair rootObject = this.rootObject != null ? new RelationPair(this.rootObject.Handle, this.rootObject.TOP_OBJECT_ID, this.rootObject.TOP_OBJECT_TYPE, projObject.F_PRJLINK_ID, serverSession.UserID, projObject.F_OBJECT_ID, projObject.F_RELATION_TYPE, projObject.F_OBJECT_TYPE, false) : (RelationPair) null;
                      bool flag2 = true;
                      for (int index3 = rootObjectPath.Items.Count - 2; index3 >= 0; --index3)
                      {
                        flag2 = Math.Abs(projObject.F_OBJECT_ID) != Math.Abs(rootObjectPath.Items[index3].F_PART_ID);
                        if (!flag2)
                          break;
                      }
                      if ((service3 != null && service3.UseDocumentation && (projObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad0057c-306c-11d8-b4e9-00304f19f545") || projObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545")) || MetaDataHelper.IsObjectTypeChildOf(projObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00163-306c-11d8-b4e9-00304f19f545"))) && !MetaDataHelper.IsObjectTypeChildOf(projObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00583-306c-11d8-b4e9-00304f19f545")))
                        service4.Add(projObject.F_OBJECT_ID, projObject.F_ID, projObject.F_OBJECT_TYPE);
                      if (flag2)
                      {
                        MRPTechRouteChangeTask task = new MRPTechRouteChangeTask(this.Name, this.Services, (IMRPCompositionTask) this, rootObject, rootObjectPath, projObject, this.holder);
                        service1.EnqueueTask((IMRPCompositionTask) task);
                      }
                    }
                    dataTable.Dispose();
                  }
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          lock (this.syncRoot)
          {
            this.Exception = ex;
            this.State = MRPCompositionTaskState.Error;
            service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
          }
        }
      }
    }
    finally
    {
      this.RemoveSession();
      if (!serverSession.SessionGUID.Equals(this.SessionGuid) && serverSession.SessionGUID.Equals(g))
        serverSession.Logout("MRPTechRoute.Execute");
      lock (this.syncRoot)
      {
        if (this.Exception == null && this.State != MRPCompositionTaskState.Cancelled)
        {
          this.State = MRPCompositionTaskState.Completed;
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
