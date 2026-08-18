// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionSession
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.AutoSelectionNodeSupport;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionService;

public class AutoSelectionSession : MarshalByRefObject, IDisposable
{
  internal static readonly Guid SortingSession = Guid.NewGuid();
  private readonly Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService _service;
  private readonly AutoSelectionParams _params;
  private bool _testMode;
  private ObjInfoItem _targetObjInfo;
  private ObjInfoItem _targetProjInfo;
  private SessionContextInfo _contextInfo;
  private readonly Dictionary<long, ImbaseObjCreateInfo> _imbaseObj2CreationType = new Dictionary<long, ImbaseObjCreateInfo>();
  private List<RelObjInfoItem> _relObjInfoList = new List<RelObjInfoItem>();
  private List<AutoSelectionObject> _createdObjectList = new List<AutoSelectionObject>();
  private Intermech.AutoSelection.Client.AutoSelectionLog.AutoSelectionLog _selectionLog = new Intermech.AutoSelection.Client.AutoSelectionLog.AutoSelectionLog();

  private bool LoadTargetDataInfo(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    QuickObjectInfo objectInfo = session.GetObjectInfo(this.Params.ObjectID);
    if (objectInfo.Empty)
    {
      this.SelectionLog.AddRec(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_21"), (object) this.Params.ObjectID));
      return false;
    }
    this._targetObjInfo = new ObjInfoItem(objectInfo.ObjectID, objectInfo.ObjectTypeID);
    if (this.Params.ProjectObjectIDs != null && this.Params.ProjectObjectIDs.Length != 0)
    {
      objectInfo = session.GetObjectInfo(this.Params.ProjectObjectIDs[0]);
      if (!objectInfo.Empty)
        this._targetProjInfo = new ObjInfoItem(objectInfo.ObjectID, objectInfo.ObjectTypeID);
    }
    else if (this.Params.ProjectRelationIDs != null && this.Params.ProjectRelationIDs.Length != 0)
    {
      IDBRelation relation = session.GetRelation(this.Params.ProjectRelationIDs[0], false);
      if (relation != null)
      {
        objectInfo = session.GetObjectInfo(relation.ProjID);
        if (!objectInfo.Empty)
          this._targetProjInfo = new ObjInfoItem(objectInfo.ObjectID, objectInfo.ObjectTypeID);
      }
    }
    return true;
  }

  private bool LoadRulesData(IUserSession session, out List<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule> ruleDataList)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    ruleDataList = new List<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule>();
    List<long> rulesByObject = AutoSelectionUtils.ServiceKeeper.GetAutosServerService().GetRulesByObject(this.Params.ObjectID, session.SessionGUID);
    if (rulesByObject == null || rulesByObject.Count == 0)
    {
      this.SelectionLog.AddRec(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_22"), (object) this.Params.ObjectID));
      return false;
    }
    foreach (long objectID in rulesByObject)
    {
      IDBObject dbObject = session.GetObject(objectID, false);
      if (dbObject == null)
      {
        this.SelectionLog.AddRec(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_23"), (object) objectID));
      }
      else
      {
        Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule autoSelectionRule = Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule.Load(dbObject);
        if (autoSelectionRule != null && (this.Params.Mode == AutoSelectionMode.All || autoSelectionRule.Mode.Equals((object) this.Params.Mode)))
          ruleDataList.Add(autoSelectionRule);
      }
    }
    if (ruleDataList.Count != 0)
      return true;
    this.SelectionLog.AddRec(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_24"), (object) this.Params.ObjectID));
    return false;
  }

  private bool ExecuteRules(List<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule> ruleDataList)
  {
    if (ruleDataList == null)
      throw new ArgumentNullException(nameof (ruleDataList));
    try
    {
      if (!this.ExecuteRulesInternal(ruleDataList))
        return false;
      if (this.TestMode)
        return true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) sessionKeeper.Session, true)?.CreateSession((object) AutoSelectionSession.SortingSession);
      try
      {
        List<AutoSelectionObject> createdObjList;
        return this.ExecuteRules_CreateData(out createdObjList) && this.ExecuteRules_AutoSelect4CreatedData(createdObjList) && this.ExecuteRule_CommitCreation(createdObjList);
      }
      finally
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) sessionKeeper.Session, true)?.DisposeSession((object) AutoSelectionSession.SortingSession);
      }
    }
    finally
    {
      this.ExecuteRules_GarbageClear();
    }
  }

  private bool ExecuteRulesInternal(List<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule> ruleDataList)
  {
    AutoSelectionLogRec autoSelectionLogRec = this.SelectionLog.AddRec(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_25"));
    foreach (AutoSelectionNodeBase ruleData in ruleDataList)
    {
      if (ruleData.Execute(this, autoSelectionLogRec) == AutoSelExecuteStatus.AbortAll)
      {
        this.SelectionLog.AddRec(autoSelectionLogRec, (AutoSelectionNodeBase) null, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_13"));
        break;
      }
    }
    return this.CreatedObjectList.Count != 0;
  }

  private void ExecuteRules_GarbageClear()
  {
    if (!this.TestMode)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (AutoSelectionObject createdObject in this.CreatedObjectList)
        createdObject.Node.DeleteSelectionObject(this, createdObject, sessionKeeper.Session);
    }
  }

  private bool ExecuteRules_CreateData(out List<AutoSelectionObject> createdObjList)
  {
    createdObjList = new List<AutoSelectionObject>(this.CreatedObjectList.Count);
    if (!this.ExecuteRules_CreateObjects(createdObjList))
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.ExecuteRules_CreateRelations(sessionKeeper.Session, createdObjList);
  }

  private bool ExecuteRules_CreateObjects(List<AutoSelectionObject> createdObjList)
  {
    if (createdObjList == null)
      throw new ArgumentNullException(nameof (createdObjList));
    AutoSelectionLogRec logRec = Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService.SelectionLog.AddRec(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_26"));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateImbaseObjectCreationInfo(this.CreatedObjectList, sessionKeeper.Session);
    foreach (AutoSelectionObject createdObject in this.CreatedObjectList)
    {
      AutoSelectionNodeCommon node = createdObject.Node;
      if (node != null)
      {
        if (!ObjInfoItem.IsEmpty((ITypedInfoItem) createdObject.CreatedObjInfo))
        {
          createdObjList.Add(createdObject);
        }
        else
        {
          ObjInfoItem projectObjInfo = node.GetProjectObjInfo(this);
          if (node.AnalyzeObject(this, logRec, projectObjInfo))
          {
            IList<AutoSelectionObject> autoSelectionObjectList = node.CreateObject(this, createdObject);
            if (autoSelectionObjectList != null)
            {
              foreach (AutoSelectionObject autoSelectionObject in (IEnumerable<AutoSelectionObject>) autoSelectionObjectList)
              {
                if (autoSelectionObject != null && !ObjInfoItem.IsEmpty((ITypedInfoItem) autoSelectionObject.CreatedObjInfo))
                {
                  if ((TypedInfoItem) autoSelectionObject.CreatedRelnfo == (TypedInfoItem) null)
                    autoSelectionObject.CreatedRelnfo = new RelObjInfoItem((RelInfoItem) null, projectObjInfo, autoSelectionObject.CreatedObjInfo);
                  createdObjList.Add(autoSelectionObject);
                }
              }
            }
          }
        }
      }
    }
    return createdObjList.Count > 0;
  }

  private bool ExecuteRules_CreateRelations(
    IUserSession session,
    List<AutoSelectionObject> createdObjList)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (createdObjList == null)
      throw new ArgumentNullException(nameof (createdObjList));
    List<CompositionSortingProjInfo> compositionSortingProjInfoList1 = new List<CompositionSortingProjInfo>();
    List<CompositionSortingProjInfo> compositionSortingProjInfoList2 = new List<CompositionSortingProjInfo>();
    foreach (AutoSelectionObject createdObj in createdObjList)
    {
      AutoSelectionObject autoSelectionObject = createdObj;
      if (autoSelectionObject != null)
      {
        RelObjInfoItem createdRelnfo = createdObj.CreatedRelnfo;
        if (!((TypedInfoItem) createdRelnfo == (TypedInfoItem) null))
        {
          if (RelInfoItem.IsEmpty((RelInfoItem) createdRelnfo))
          {
            this.Service.DoBeforeCreateRelation((object) this, new RelationEventArgs(createdRelnfo));
            IDBObject objectActualCopy = session.GetObjectActualCopy(createdRelnfo.ProjInfo.ObjectID, true);
            createdRelnfo.ProjInfo.ObjectID = objectActualCopy.ObjectID;
            IDBRelation relation = autoSelectionObject.Node.CreateRelation(this, objectActualCopy, createdRelnfo.PartInfo);
            if (relation != null)
            {
              createdRelnfo.RelationID = relation.RelationID;
              createdRelnfo.RelTypeID = relation.RelationType;
              this.Service.DoAfterCreateRelation((object) this, new RelationEventArgs(createdRelnfo));
              CompositionSortingProjInfo compositionSortingProjInfo = new CompositionSortingProjInfo(createdRelnfo.RelationID, createdRelnfo.RelTypeID, createdRelnfo.ProjInfo.ObjectID, createdRelnfo.ProjInfo.ObjTypeID, createdRelnfo.PartInfo.ObjTypeID);
              if ((TypedInfoItem) this._targetProjInfo == (TypedInfoItem) createdRelnfo.ProjInfo)
                compositionSortingProjInfoList2.Add(compositionSortingProjInfo);
              else
                compositionSortingProjInfoList1.Add(compositionSortingProjInfo);
            }
            else
              continue;
          }
          this._relObjInfoList.Add(createdRelnfo);
        }
      }
    }
    if (compositionSortingProjInfoList1.Count != 0 || compositionSortingProjInfoList2.Count != 0)
    {
      ICompositionsAutomaticSortingSession automaticSortingSession = (ICompositionsAutomaticSortingSession) null;
      ICompositionsAutomaticSortingService service = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) session, true);
      if (service != null)
        automaticSortingSession = service.CreateSession((object) AutoSelectionSession.SortingSession);
      if (automaticSortingSession != null)
      {
        try
        {
          if (compositionSortingProjInfoList2.Count != 0)
          {
            automaticSortingSession.PrefetchObjectComposition((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
            {
              this.TargetObjInfo
            }, (object) session.SessionGUID);
            long projectRelationId = this.Params.ProjectRelationIDs == null || this.Params.ProjectRelationIDs.Length == 0 ? 0L : this.Params.ProjectRelationIDs[0];
            if (projectRelationId != 0L)
              automaticSortingSession.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList2.ToArray(), CompositionTargetMode.InsertAfter, projectRelationId, (object) session.SessionGUID);
            else
              automaticSortingSession.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList2.ToArray(), (object) session.SessionGUID);
          }
          if (compositionSortingProjInfoList1.Count != 0)
          {
            automaticSortingSession.PrefetchObjectComposition((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
            {
              this.TargetObjInfo
            }, (object) session.SessionGUID);
            automaticSortingSession.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList1.ToArray(), (object) session.SessionGUID);
          }
        }
        finally
        {
          service?.DisposeSession((object) AutoSelectionSession.SortingSession);
        }
      }
    }
    return true;
  }

  private bool ExecuteRule_CommitCreation(List<AutoSelectionObject> createdObjList)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (AutoSelectionObject createdObj in createdObjList)
      {
        AutoSelectionObject selectionObject = createdObj;
        if (selectionObject != null)
        {
          RelObjInfoItem createdRelnfo = createdObj.CreatedRelnfo;
          if (!((TypedInfoItem) createdRelnfo == (TypedInfoItem) null))
          {
            if (createdRelnfo.RelationID == 0L)
            {
              selectionObject.Node.DeleteSelectionObject(this, selectionObject, sessionKeeper.Session);
            }
            else
            {
              IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(createdObj.CreatedObjInfo.ObjectID, true);
              ObjInfoItem aObject = new ObjInfoItem(objectActualCopy);
              ObjInfoItem newObject = (ObjInfoItem) null;
              if (objectActualCopy.IsCreationMode)
              {
                this.Service.DoBeforeCommitCreation((object) this, new ObjectEventArgs(aObject));
                objectActualCopy.CommitCreation(true, true);
                createdObj.CreatedObjInfo.ObjectID = createdRelnfo.PartInfo.ObjectID = objectActualCopy.ObjectID;
                newObject = new ObjInfoItem(objectActualCopy);
              }
              this.Service.DoAfterCommitCreation((object) this, (ObjectEventArgs) new ObjectCommitEventArgs(aObject, newObject));
            }
          }
        }
      }
    }
    return true;
  }

  internal bool ExecuteRules_AutoSelect4CreatedData(List<AutoSelectionObject> createdObjList)
  {
    foreach (AutoSelectionObject createdObj in createdObjList)
    {
      AutoSelectionObject autoSelectionObject = createdObj;
      if (autoSelectionObject != null)
      {
        RelObjInfoItem createdRelnfo = createdObj.CreatedRelnfo;
        if (!((TypedInfoItem) createdRelnfo == (TypedInfoItem) null) && createdRelnfo.RelationID != 0L && autoSelectionObject.NeedAutoSelection)
        {
          List<long> longList1 = new List<long>();
          List<long> longList2 = new List<long>();
          longList1.Add(createdRelnfo.ProjInfo.ObjectID);
          longList2.Add(createdRelnfo.RelationID);
          if (this.Params.ProjectObjectIDs != null)
            longList1.AddRange((IEnumerable<long>) this.Params.ProjectObjectIDs);
          if (this.Params.ProjectRelationIDs != null)
            longList2.AddRange((IEnumerable<long>) this.Params.ProjectRelationIDs);
          using (AutoSelectionSession selectionSession = new AutoSelectionSession(this._service, new AutoSelectionParams(createdRelnfo.PartInfo.ObjectID, longList2.ToArray(), longList1.ToArray(), this.Params.Mode)))
            this._relObjInfoList.AddRange((IEnumerable<RelObjInfoItem>) selectionSession.Execute(this.TestMode));
          autoSelectionObject.NeedAutoSelection = false;
        }
      }
    }
    return true;
  }

  private void LoadImbaseObjectCreationInfo(List<long> imbaseObjList, IUserSession session)
  {
    if (imbaseObjList == null)
      throw new ArgumentNullException(nameof (imbaseObjList));
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (imbaseObjList.Count == 0)
      return;
    IImbaseObjInfoService service = ServiceUtils.GetService<IImbaseObjInfoService>((object) session, true);
    Dictionary<long, ImbaseObjCreateInfo> objCreateInfo;
    if (service == null || !service.GetCreationMode((IList<long>) imbaseObjList, session.SessionGUID, out objCreateInfo) || objCreateInfo == null)
      return;
    foreach (KeyValuePair<long, ImbaseObjCreateInfo> keyValuePair in objCreateInfo)
      this._imbaseObj2CreationType[keyValuePair.Key] = keyValuePair.Value;
  }

  private void UpdateImbaseObjectCreationInfo(
    List<AutoSelectionObject> selObjList,
    IUserSession session)
  {
    if (selObjList == null)
      throw new ArgumentNullException(nameof (selObjList));
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (selObjList.Count == 0)
      return;
    List<long> imbaseObjList = new List<long>(selObjList.Count);
    foreach (AutoSelectionObject selObj in selObjList)
    {
      AutoSelectionNodeCommon node = selObj.Node;
      if (ObjInfoItem.IsEmpty((ITypedInfoItem) selObj.CreatedObjInfo) && node != null && node is AutoSelectionNodeItemImbase selectionNodeItemImbase && !this._imbaseObj2CreationType.ContainsKey(selectionNodeItemImbase.ImbaseObjectID.Value))
        imbaseObjList.Add(selectionNodeItemImbase.ImbaseObjectID.Value);
    }
    this.LoadImbaseObjectCreationInfo(imbaseObjList, session);
  }

  public AutoSelectionSession(Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService service, AutoSelectionParams param)
  {
    this._service = service ?? throw new ArgumentNullException(nameof (service));
    this._params = param ?? throw new ArgumentNullException(nameof (param));
  }

  public List<RelObjInfoItem> Execute(bool testMode)
  {
    this._testMode = testMode;
    List<Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule> ruleDataList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!this.LoadTargetDataInfo(sessionKeeper.Session))
        return this._relObjInfoList;
      if (!this.LoadRulesData(sessionKeeper.Session, out ruleDataList))
        return this._relObjInfoList;
    }
    this.ExecuteRules(ruleDataList);
    return this._relObjInfoList;
  }

  public ImbaseObjCreateInfo GetImbaseObjectCreationInfo(long imbaseObjectId, IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (imbaseObjectId == 0L)
      return new ImbaseObjCreateInfo(-1, ImbaseObjCreateMode.iocmUnknown);
    ImbaseObjCreateInfo objectCreationInfo;
    if (!this._imbaseObj2CreationType.TryGetValue(imbaseObjectId, out objectCreationInfo))
    {
      this.LoadImbaseObjectCreationInfo(new List<long>()
      {
        imbaseObjectId
      }, session);
      objectCreationInfo = this._imbaseObj2CreationType[imbaseObjectId];
    }
    return objectCreationInfo;
  }

  public AutoSelectionParams Params
  {
    [DebuggerStepThrough] get => this._params;
  }

  public bool TestMode
  {
    [DebuggerStepThrough] get => this._testMode;
  }

  public Intermech.AutoSelection.Client.AutoSelectionLog.AutoSelectionLog SelectionLog
  {
    [DebuggerStepThrough] get => this._selectionLog;
  }

  internal SessionContextInfo ContextInfo
  {
    get
    {
      if (this._contextInfo != null)
        return this._contextInfo;
      this._contextInfo = new SessionContextInfo();
      this._contextInfo.ObjectIds.Add(this.Params.ObjectID);
      if (this.Params.ProjectObjectIDs != null)
        this._contextInfo.ObjectIds.AddRange<long>((IEnumerable<long>) this.Params.ProjectObjectIDs);
      if (this.Params.ProjectRelationIDs != null && this.Params.ProjectRelationIDs.Length != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (long projectRelationId in this.Params.ProjectRelationIDs)
          {
            IDBRelation relation = sessionKeeper.Session.GetRelation(projectRelationId, false);
            if (relation != null)
            {
              this._contextInfo.RelationIds.Add(projectRelationId);
              if (!this._contextInfo.ObjectIds.Contains(relation.ProjID))
                this._contextInfo.ObjectIds.Add(relation.ProjID);
            }
          }
        }
      }
      return this._contextInfo;
    }
  }

  internal List<AutoSelectionObject> CreatedObjectList
  {
    [DebuggerStepThrough] get => this._createdObjectList;
  }

  internal Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService Service
  {
    [DebuggerStepThrough] get => this._service;
  }

  internal ObjInfoItem TargetObjInfo => this._targetObjInfo;

  internal ObjInfoItem TargetProjInfo => this._targetProjInfo;

  void IDisposable.Dispose()
  {
    this._selectionLog.Clear();
    this._selectionLog = (Intermech.AutoSelection.Client.AutoSelectionLog.AutoSelectionLog) null;
    this._createdObjectList.Clear();
    this._createdObjectList = (List<AutoSelectionObject>) null;
    this._relObjInfoList = (List<RelObjInfoItem>) null;
  }
}
