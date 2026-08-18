// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPCompositionBrowseTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Search.Pdm.Analogs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPCompositionBrowseTask : MRPCompositionBaseTask
{
  private RelationPair rootObject;
  private RelationPath rootObjectPath;
  private MRPCompositionObject projObject;
  private IMRPTypedObjectRef projInstanceObject;
  private ManufactureOrderHolder holder;

  public MRPCompositionBrowseTask(
    string taskName,
    IServiceProvider services,
    IMRPCompositionTask masterTask,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    long projObject,
    ManufactureOrderHolder holder)
    : base(taskName, services, masterTask)
  {
    this.rootObject = rootObject;
    this.rootObjectPath = rootObjectPath;
    this.projObject = new MRPCompositionObject(projObject, 0L);
    this.holder = holder;
  }

  public MRPCompositionBrowseTask(
    string taskName,
    IServiceProvider services,
    IMRPCompositionTask masterTask,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    MRPCompositionObject projObject,
    IMRPTypedObjectRef projInstanceObject,
    ManufactureOrderHolder holder)
    : base(taskName, services, masterTask)
  {
    this.rootObject = rootObject;
    this.rootObjectPath = rootObjectPath;
    this.projObject = projObject;
    this.projInstanceObject = projInstanceObject;
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
        this.Exception = (Exception) new KernelExceptionID(210, (object) "MRPCompositionBrowseTask.Execute");
        this.State = MRPCompositionTaskState.Error;
        service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.9");
      }
      else
      {
        try
        {
          service1.TaskOperation = LocalizationHolder.rm.GetString("MRP.Server.TasksQueue.3");
          if (!serverSession.SessionGUID.Equals(this.SessionGuid))
          {
            serverSession = serverSession.Clone(true, "MRPComposition.Execute") as IServerSession;
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
          bool flag1 = MetaDataHelper.IsObjectTypeChildOf(this.projObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cadd92e9-306c-11d8-b4e9-00304f19f545"));
          bool flag2 = MetaDataHelper.IsObjectTypeChildOf(this.projObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545"));
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
          ICompositionLoadService customService = serverSession.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
          for (int index1 = 0; index1 < visibleRelations.Count; ++index1)
          {
            if (!flag1 || visibleRelations[index1] == MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545"))
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
                  DataTable table;
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
                    paramSet.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) this.holder.CompositionContexts;
                    if (this.holder.SeriesDateSettingsHolder != null)
                      paramSet.Tags[(object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}"] = (object) this.holder.SeriesDateSettingsHolder;
                    else
                      paramSet.Tags.Remove((object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}");
                    AnalogsHelper.SetAnalogSelectionModeToRecordSetParamsTags(paramSet.Tags, this.holder.AnalogSelectionMode);
                    table = relationCollection.Select(paramSet);
                  }
                  catch
                  {
                    table = (DataTable) null;
                  }
                  if (table != null && table.Rows.Count != 0)
                  {
                    MRPFindTechRouteAction findTechRouteAction = new MRPFindTechRouteAction(this.Services, table, this.rootObjectPath, this.holder);
                    findTechRouteAction.Execute(this.Services);
                    for (int index2 = 0; index2 < table.Rows.Count; ++index2)
                    {
                      MRPCompositionObject compositionObject = new MRPCompositionObject(table.Rows[index2]);
                      bool flag3 = MetaDataHelper.IsObjectTypeChildOf(compositionObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad015b1-306c-11d8-b4e9-00304f19f545"));
                      bool flag4 = MetaDataHelper.IsObjectTypeChildOf(compositionObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545"));
                      bool flag5 = MetaDataHelper.IsObjectTypeChildOf(compositionObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545"));
                      if (!(flag2 & flag5) && (compositionObject.F_RELATION_TYPE != MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545") || compositionObject.IsBoughtArticle != 4L))
                      {
                        SimpleRelationPair simpleRelationPair = new SimpleRelationPair(compositionObject.F_PRJLINK_ID, compositionObject.F_RELATION_TYPE, compositionObject.F_OBJECT_ID, compositionObject.F_OBJECT_TYPE, false);
                        RelationPath rootObjectPath = new RelationPath((object) this.rootObjectPath);
                        rootObjectPath.Items.Add(simpleRelationPair);
                        RelationPair rootObject = this.rootObject != null ? new RelationPair(this.rootObject.Handle, this.rootObject.TOP_OBJECT_ID, this.rootObject.TOP_OBJECT_TYPE, compositionObject.F_PRJLINK_ID, serverSession.UserID, compositionObject.F_OBJECT_ID, compositionObject.F_RELATION_TYPE, compositionObject.F_OBJECT_TYPE, false) : (RelationPair) null;
                        bool flag6 = true;
                        for (int index3 = rootObjectPath.Items.Count - 2; index3 >= 0; --index3)
                        {
                          flag6 = Math.Abs(compositionObject.F_OBJECT_ID) != Math.Abs(rootObjectPath.Items[index3].F_PART_ID);
                          if (!flag6)
                            break;
                        }
                        if (this.Services.GetService(typeof (MRPContextOptionsHolder)) is MRPContextOptionsHolder service4 && (service4.Options & MRPContextOptions.FixToEditingContext) == MRPContextOptions.FixToEditingContext)
                          (this.Services.GetService(typeof (IMRPEditingContextRef)) as IMRPEditingContextRef).Add(compositionObject.F_OBJECT_ID, compositionObject.F_ID, compositionObject.F_OBJECT_TYPE);
                        AdvancedServiceContainer svc = (AdvancedServiceContainer) null;
                        if (service3 != null && service3.UseDocumentation && (compositionObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad0057c-306c-11d8-b4e9-00304f19f545") || compositionObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545")) || findTechRouteAction.ObjectID == compositionObject.F_OBJECT_ID)
                        {
                          svc = new AdvancedServiceContainer();
                          svc.AdvancedProvider = this.Services;
                          this.HackOptions(svc, MRPContextOptions.FixToEditingContext);
                        }
                        if (flag6)
                        {
                          bool flag7 = true;
                          bool flag8 = false;
                          IMRPTypedObjectRef projInstanceObject1 = (IMRPTypedObjectRef) null;
                          IMRPTypedObjectRef projInstanceObject2 = (IMRPTypedObjectRef) null;
                          BoughtArticleItemSettings articleItemSettings = (BoughtArticleItemSettings) null;
                          if (compositionObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545"))
                          {
                            MRPArticleToInstanceAction toInstanceAction1 = (MRPArticleToInstanceAction) null;
                            MRPArticleToInstanceAction toInstanceAction2 = (MRPArticleToInstanceAction) null;
                            articleItemSettings = this.holder != null ? this.holder.GetRelationSetting(compositionObject.F_PRJLINK_ID, typeof (BoughtArticleItemSettings)) as BoughtArticleItemSettings : (BoughtArticleItemSettings) null;
                            if (articleItemSettings == null)
                            {
                              articleItemSettings = new BoughtArticleItemSettings();
                              articleItemSettings.IsBoughtArticle = compositionObject.IsBoughtArticle;
                              articleItemSettings.SourceQuantity = compositionObject.Quantity;
                              articleItemSettings.BoughtQuantity = compositionObject.Quantity;
                              articleItemSettings.CheckSettings();
                            }
                            MovingItemSettings relationSetting = this.holder != null ? this.holder.GetRelationSetting(compositionObject.F_PRJLINK_ID, typeof (MovingItemSettings)) as MovingItemSettings : (MovingItemSettings) null;
                            if (!flag3 && !flag4)
                            {
                              if (articleItemSettings.IsBoughtArticle != 2L || articleItemSettings.BoughtQuantity == null || articleItemSettings.BoughtQuantity.Value == 0.0)
                              {
                                toInstanceAction1 = new MRPArticleToInstanceAction(this.Services, this.projInstanceObject ?? (IMRPTypedObjectRef) this.projObject, compositionObject, rootObjectPath, articleItemSettings, relationSetting);
                              }
                              else
                              {
                                if (articleItemSettings.BoughtQuantity != null && articleItemSettings.BoughtQuantity.Value != 0.0)
                                  toInstanceAction2 = new MRPArticleToInstanceAction(this.Services, this.projInstanceObject ?? (IMRPTypedObjectRef) this.projObject, compositionObject, rootObjectPath, articleItemSettings, relationSetting);
                                if (articleItemSettings.RestQuantity != null && articleItemSettings.RestQuantity.Value != 0.0)
                                {
                                  BoughtArticleItemSettings settings = new BoughtArticleItemSettings((object) articleItemSettings);
                                  settings.MakeOwn();
                                  toInstanceAction1 = new MRPArticleToInstanceAction(this.Services, this.projInstanceObject ?? (IMRPTypedObjectRef) this.projObject, compositionObject, rootObjectPath, settings, relationSetting);
                                }
                                else
                                  flag7 = false;
                              }
                            }
                            if (toInstanceAction2 != null)
                              this.actions.AddLast((IMRPAction) toInstanceAction2);
                            if (toInstanceAction1 != null)
                              this.actions.AddLast((IMRPAction) toInstanceAction1);
                            projInstanceObject1 = !(flag3 | flag4) ? (IMRPTypedObjectRef) toInstanceAction1 : this.projInstanceObject ?? (IMRPTypedObjectRef) this.projObject;
                            projInstanceObject2 = (IMRPTypedObjectRef) toInstanceAction2;
                          }
                          if (compositionObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545") && this.projInstanceObject != null)
                          {
                            if (service3 != null && service3.UseDocumentation)
                            {
                              MRPCreateRelationIfNeedAction relation = new MRPCreateRelationIfNeedAction(this.Services, this.projInstanceObject, (IMRPTypedObjectRef) compositionObject, compositionObject.F_RELATION_TYPE, Guid.Empty);
                              this.actions.AddLast((IMRPAction) relation);
                              this.actions.AddLast((IMRPAction) new MRPFixRelationPartAction(this.Services, (IMRPRelationRef) relation, (IMRPObjectRef) compositionObject));
                            }
                            else
                              flag7 = false;
                          }
                          if (compositionObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545") && (compositionObject.IsBoughtArticle == 2L || articleItemSettings != null && articleItemSettings.IsBoughtArticle == 2L) && projInstanceObject2 != null)
                            flag8 = service3.UseBoughtArticles;
                          if (flag7)
                          {
                            MRPCompositionBrowseTask task = new MRPCompositionBrowseTask(this.Name, (IServiceProvider) svc ?? this.Services, (IMRPCompositionTask) this, rootObject, rootObjectPath, compositionObject, projInstanceObject1, this.holder);
                            service1.EnqueueTask((IMRPCompositionTask) task);
                          }
                          if (flag8)
                          {
                            MRPCompositionBrowseTask task = new MRPCompositionBrowseTask(this.Name, (IServiceProvider) svc ?? this.Services, (IMRPCompositionTask) this, rootObject, rootObjectPath, compositionObject, projInstanceObject2, this.holder);
                            service1.EnqueueTask((IMRPCompositionTask) task);
                          }
                        }
                        if (flag1 && compositionObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545"))
                          this.actions.AddLast((IMRPAction) new MRPDeleteRelationAction(this.Services, (IMRPObjectRef) new MRPObjectRef(this.Services, compositionObject.F_PROJ_ID, compositionObject.LINK_GUID), compositionObject.LINK_GUID, compositionObject.F_RELATION_TYPE));
                      }
                    }
                    if (findTechRouteAction != null && this.projInstanceObject != null && findTechRouteAction.ObjectID != 0L)
                    {
                      MRPAttachTechRouteAction relation = new MRPAttachTechRouteAction(this.Services, this.projInstanceObject, (IMRPTypedObjectRef) findTechRouteAction);
                      this.actions.AddLast((IMRPAction) relation);
                      this.actions.AddLast((IMRPAction) new MRPFixRelationPartAction(this.Services, (IMRPRelationRef) relation, (IMRPObjectRef) findTechRouteAction));
                    }
                    table.Dispose();
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
        serverSession.Logout("MRPComposition.Execute");
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

  private void HackOptions(AdvancedServiceContainer svc, MRPContextOptions orOptions)
  {
    MRPContextOptionsHolder serviceInstance = new MRPContextOptionsHolder(svc.GetService(typeof (MRPContextOptionsHolder)) is MRPContextOptionsHolder service1 ? service1.Options | orOptions : orOptions);
    IServiceProvider advancedProvider = svc.AdvancedProvider;
    try
    {
      svc.AdvancedProvider = (IServiceProvider) null;
      if (svc.GetService(typeof (MRPContextOptionsHolder)) is MRPContextOptionsHolder service2)
        service2.Options |= orOptions;
      else
        svc.AddService(typeof (MRPContextOptionsHolder), (object) serviceInstance);
    }
    finally
    {
      svc.AdvancedProvider = advancedProvider;
    }
  }
}
