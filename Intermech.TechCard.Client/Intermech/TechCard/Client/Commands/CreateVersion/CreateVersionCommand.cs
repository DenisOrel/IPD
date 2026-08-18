// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CreateVersion.CreateVersionCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Docking;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Services.CreateVersion;
using Intermech.TechCard.Client.Services.CreateVersion.Analyzer;
using Intermech.TechCard.Client.Services.DataProviders;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using Intermech.TechCard.Client.Services.DataProviders.Versions;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.CreateVersion;

/// <summary>
/// Реализация команды "Создать версию" контекстного меню навигатора для технологических объектов
/// </summary>
internal class CreateVersionCommand : TechCardSelectedItemsCommand
{
  /// <summary>
  /// 
  /// </summary>
  private readonly object _syncRoot = new object();
  /// <summary>
  /// Признак вызова обработчика по-умолчанию для созданий версий объектов
  /// </summary>
  private bool _defaultCreateVersionHandler;
  /// <summary>Параметры анализатора</summary>
  private TechCardCreateVersionAnalyzerStepData _analyzerStepData;
  /// <summary>Параметры создания версий</summary>
  private TechCardCreateVersionParams _createVersionParams;
  /// <summary>
  /// 
  /// </summary>
  private List<CategoryValue> _modificationLog;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool LoadCommandInfo_ObjData()
  {
    this._analyzerStepData = (TechCardCreateVersionAnalyzerStepData) null;
    this._createVersionParams = (TechCardCreateVersionParams) null;
    return true;
  }

  /// <summary>Анализ данных</summary>
  /// <returns></returns>
  private bool AnalyzeCommandData()
  {
    Func<RelObjInfoItem, RelObjInfoItem, bool> compareMethod = (Func<RelObjInfoItem, RelObjInfoItem, bool>) ((first, second) => (TypedInfoItem) first.PartInfo == (TypedInfoItem) second.PartInfo && (TypedInfoItem) first.ProjInfo == (TypedInfoItem) second.ProjInfo && first.RelTypeID == second.RelTypeID);
    HashSet<int> possibleRelationTypeIds = TechCardConsts.RelTypes.TechAllRelationTypes.Append<int>(TechCardConsts.RelTypes.TechDraftRelationID).Append<int>(TechCardConsts.RelTypes.SortedRelationID).ToHashSet<int>();
    TechRelObjInfoItemsFromSelectedItemContextProvider sourceProvider1 = new TechRelObjInfoItemsFromSelectedItemContextProvider(this.Items, this.ContextServices);
    sourceProvider1.RelationItemFilter = (System.Func<RelObjInfoItem, bool>) (relationItem => (TypedInfoItem) relationItem != (TypedInfoItem) null && possibleRelationTypeIds.Contains(relationItem.RelTypeID));
    sourceProvider1.RelationItemComparer = (IEqualityComparer<RelObjInfoItem>) new SimpleEqualityComparer<RelObjInfoItem>(compareMethod);
    HashSet<RelObjInfoItem> contextRelationItems = new TechRelObjInfoItemsFromSelectedItemContextProvider(this.Items, this.ContextServices).Execute().ToHashSet<RelObjInfoItem>();
    TechRelObjInfoItemsFromSelectedItemApplicabilityProvider applicabilityProvider = new TechRelObjInfoItemsFromSelectedItemApplicabilityProvider(this.Items, this.ContextServices);
    applicabilityProvider.RelationItemFilter = (System.Func<RelObjInfoItem, bool>) (relObjInfoItem =>
    {
      if (contextRelationItems.Contains(relObjInfoItem))
        return true;
      return (TypedInfoItem) relObjInfoItem.PartInfo != (TypedInfoItem) null && TechCardConsts.Utils.IsTechcardObjectType((object) relObjInfoItem.PartInfo.ObjTypeID);
    });
    applicabilityProvider.RelationItemComparer = (IEqualityComparer<RelObjInfoItem>) new SimpleEqualityComparer<RelObjInfoItem>(compareMethod);
    ITechCardDataEnumerableProvider<RelObjInfoItem> sourceProvider2 = (ITechCardDataEnumerableProvider<RelObjInfoItem>) applicabilityProvider;
    Action<ICollection<RelObjInfoItem>> action = (Action<ICollection<RelObjInfoItem>>) (relObjInfoItems =>
    {
      RelObjInfoItem[] array1 = relObjInfoItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (relationItem => MetaDataHelper.IsObjectTypeChildOf(relationItem.PartInfo.ObjTypeID, TechCardConsts.ObjectTypes.TechBaseDocID) || MetaDataHelper.IsObjectTypeChildOf(relationItem.PartInfo.ObjTypeID, TechCardConsts.ObjectTypes.ComlectTechDocBaseID))).ToArray<RelObjInfoItem>();
      if (!((IEnumerable<RelObjInfoItem>) array1).Any<RelObjInfoItem>())
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
        ColumnDescriptor[] array2 = RelObjInfoDbScheme<ObjInfoItem>.GetSourceTableColumns().ToArray<ColumnDescriptor>();
        CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) ((IEnumerable<RelObjInfoItem>) array1).Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (relationItem => relationItem.PartInfo)).ToArray<ObjInfoItem>(), (IEnumerable<int>) null, (IEnumerable<int>) null, (IEnumerable<int>) new int[2]
        {
          TechCardConsts.RelTypes.TechRelationID,
          TechCardConsts.RelTypes.SortedRelationID
        }, (IEnumerable<ColumnDescriptor>) array2, (IEnumerable<ConditionStructure>) null, false, false, 1, (VersionsRule) null, "cad005aa-306c-11d8-b4e9-00304f19f545");
        DataTable dataTable = service.LoadComplexCompositions((object) sessionKeeper.Session, loadingParams);
        if (dataTable == null)
          return;
        RelObjInfoDbScheme<ObjInfoIDItem> relObjInfoDbScheme = new RelObjInfoDbScheme<ObjInfoIDItem>(false);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          RelObjInfoItem relationObjInfoItem = relObjInfoDbScheme.ParseItem(row);
          if (!((IEnumerable<RelObjInfoItem>) array1).Any<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => item.RelationID == relationObjInfoItem.RelationID)))
          {
            ((ObjInfoIDItem) relationObjInfoItem.ProjInfo).ID = DataSetProcessor.GetInt64Value(row, "F_ID", 0L);
            relationObjInfoItem.PartInfo = ((IEnumerable<RelObjInfoItem>) array1).Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (relationItem => relationItem.PartInfo.Equals(relationObjInfoItem.PartInfo))).Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (relationItem => relationItem.PartInfo)).FirstOrDefault<ObjInfoItem>();
            if (!relObjInfoItems.Any<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.PartInfo == (TypedInfoItem) relationObjInfoItem.PartInfo && ((TypedInfoItem) item.ProjInfo == (TypedInfoItem) relationObjInfoItem.ProjInfo || ((ObjInfoIDItem) item.ProjInfo).ID == ((ObjInfoIDItem) relationObjInfoItem.ProjInfo).ID) && item.RelTypeID == relationObjInfoItem.RelTypeID)))
              relObjInfoItems.Add(relationObjInfoItem);
          }
        }
      }
    });
    TechCardDataEnumerableWithActionProvider<RelObjInfoItem> relObjInfoProvider = new TechCardDataEnumerableWithActionProvider<RelObjInfoItem>((ITechCardDataEnumerableProvider<RelObjInfoItem>) sourceProvider1, action);
    ITechCardDataEnumerableProvider<RelObjInfoItem> enumerableProvider = (ITechCardDataEnumerableProvider<RelObjInfoItem>) new TechCardDataEnumerableWithActionProvider<RelObjInfoItem>(sourceProvider2, action);
    if (!new TechCardCreateVersionAnalyzer(new TechCardObjectCreateVersionAnalyzerParam((ITechCardDataEnumerableProvider<RelObjInfoItem>) relObjInfoProvider)
    {
      CompositionProvider = enumerableProvider,
      AnalyzerSteps = (IEnumerable<TechCardCreateVersionAnalyzerStep>) new TechCardCreateVersionAnalyzerStep[3]
      {
        (TechCardCreateVersionAnalyzerStep) new TechCardCreateVersionAnalyzerCopyModeStep(),
        (TechCardCreateVersionAnalyzerStep) new TechCardCreateVersionAnalyzerSignApplicabilityStep(),
        (TechCardCreateVersionAnalyzerStep) new TechCardCreateVersionAnalyzerSignObjectStep(this.SingleSignedObjectLimit)
      }
    }, this.ContextServices).Execute(out this._analyzerStepData))
    {
      this._defaultCreateVersionHandler = this._analyzerStepData.DefaultCreateVersionHandler;
      return false;
    }
    return this._analyzerStepData.RelObjInfoItems.Any<RelObjInfoItem>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool BeforeExecuteCommand() => this.SelectEcoObj();

  /// <summary>
  /// 
  /// </summary>
  private void AfterExecuteCommand()
  {
    if (!ObjInfoItem.IsEmpty((ITypedInfoItem) this._createVersionParams.EcoObjectInfo))
    {
      ICurrentUserAndRole service = ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, false);
      if (service != null)
        service.EditingContextID = this._createVersionParams.EcoObjectInfo.ObjectID;
    }
    ObjInfoIDItem signedObjVerInfo = this._createVersionParams.SignedObjInfoItems.FirstOrDefault<ObjInfoIDItem>();
    if (!ObjInfoItem.IsEmpty((ITypedInfoItem) signedObjVerInfo) && this._analyzerStepData.CompositionItems.All<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.ProjInfo != (TypedInfoItem) signedObjVerInfo)))
    {
      TechCardClientConst.OpenObjectInNewWindow(signedObjVerInfo.ObjectID);
    }
    else
    {
      NavigatorTreeView service1 = this.ContextServices.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView;
      DockManager service2 = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
      if (service2 == null)
        return;
      foreach (DockControl dockControl in service2.GetDockControls())
      {
        if (dockControl is NavWindow navWindow && navWindow.TreeView == service1)
        {
          navWindow.Activate();
          break;
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool ExecuteCommandInternal()
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._createVersionParams.EcoObjectInfo))
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.StartLogHistory();
      try
      {
        IEnumerable<RelObjInfoItem> createdRelInfoItems;
        int num = ServiceUtils.GetService<ITechCardCreateVersionService>((object) ApplicationServices.Container, true).Execute(sessionKeeper.Session, this._createVersionParams, out createdRelInfoItems) ? 1 : 0;
        if (num != 0)
          this._createdRelInfoList.AddRange(createdRelInfoItems);
        this._modificationLog = sessionKeeper.Session.GetModificationsHistoryList();
        return num != 0;
      }
      finally
      {
        sessionKeeper.Session.StopLogHistory();
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool SelectEcoObj()
  {
    ICollection<ObjInfoItem> values = this._analyzerStepData.RelObjInfo2SignedObjCache != null ? this._analyzerStepData.RelObjInfo2SignedObjCache.Values : (ICollection<ObjInfoItem>) null;
    if (values == null)
      return false;
    ObjInfoItem ecoObjectInfo = (ObjInfoItem) null;
    ObjInfoItem objInfoItem1 = (ObjInfoItem) null;
    List<long> list1 = values.Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (a => a.ObjectID)).Distinct<long>().ToList<long>();
    long editingContextId;
    if (list1.Count == 1 && !TechCardParamsHelper.TechParams.Common.DisplayEcoVersionDialog && this.GetEditingContextForObject(list1[0], out editingContextId))
    {
      ecoObjectInfo = new ObjInfoItem(editingContextId);
      objInfoItem1 = values.FirstOrDefault<ObjInfoItem>();
    }
    if ((TypedInfoItem) ecoObjectInfo == (TypedInfoItem) null || (TypedInfoItem) objInfoItem1 == (TypedInfoItem) null)
    {
      IEnumerable<ObjInfoIDItem> source = (values.Count != 1 ? (ITechCardDataProvider<IEnumerable<ObjInfoIDItem>>) new TechObjInfoItemsVersionsProvider((IEnumerable<ObjInfoItem>) values) : (ITechCardDataProvider<IEnumerable<ObjInfoIDItem>>) new TechObjInfoItemVersionsProvider(values.FirstOrDefault<ObjInfoItem>())).Execute();
      List<ObjInfoItem> list2 = source != null ? source.Select<ObjInfoIDItem, ObjInfoItem>((System.Func<ObjInfoIDItem, ObjInfoItem>) (item => (ObjInfoItem) item)).ToList<ObjInfoItem>() : (List<ObjInfoItem>) null;
      if (list2 == null || !list2.Any<ObjInfoItem>())
      {
        this._defaultCreateVersionHandler = true;
        return false;
      }
      CreateVersionCommandDialog versionCommandDialog = new CreateVersionCommandDialog();
      versionCommandDialog.LoadData((IEnumerable<ObjInfoItem>) list2, new TechRelObjInfoItemsEcoProvider((IEnumerable<ObjInfoItem>) list2).Execute());
      if (versionCommandDialog.ShowDialog() != DialogResult.OK || ObjInfoItem.IsEmpty((ITypedInfoItem) versionCommandDialog.EcoObjInfo) || ObjInfoItem.IsEmpty((ITypedInfoItem) versionCommandDialog.TechVerObjInfo))
        return false;
      ecoObjectInfo = versionCommandDialog.EcoObjInfo;
      objInfoItem1 = versionCommandDialog.TechVerObjInfo;
    }
    List<ObjInfoIDItem> objInfoIdItemList = new List<ObjInfoIDItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      objInfoIdItemList.Add(new ObjInfoIDItem((TypedInfoItem) objInfoItem1));
      int relationTypeId = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
      IDBRelation relation = sessionKeeper.Session.GetRelation(ecoObjectInfo.ObjectID, objInfoItem1.ObjectID, relationTypeId, true);
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeId);
      foreach (ObjInfoItem objInfoItem2 in (IEnumerable<ObjInfoItem>) values)
      {
        if (((ObjInfoIDItem) objInfoItem2).ID != ((ObjInfoIDItem) objInfoItem1).ID)
        {
          CreateVersionResult versionInternal = ((IClientDBObjectCollection) sessionKeeper.Session.GetObjectCollection(objInfoItem2.ObjTypeID)).CreateVersionInternal(objInfoItem2.ObjectID);
          try
          {
            NewRelationProperties properties = new NewRelationProperties(-1L, ecoObjectInfo.ObjectID, versionInternal.NewObjectVersion.ID)
            {
              PartObjectID = versionInternal.NewObjectVersion.ObjectID,
              PrototypeRelationID = relation != null ? relation.RelationID : 0L,
              ValuesList = new AttributeValues[1]
              {
                new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.EcoAuxObjAttrGuid), (object) DBNull.Value)
              }
            };
            relationCollection.Create(properties);
            versionInternal.NewObjectVersion.CommitCreation(true, true);
            versionInternal.Commit(sessionKeeper.Session);
          }
          catch
          {
            versionInternal.Rollback(sessionKeeper.Session);
            throw;
          }
          objInfoIdItemList.Add(new ObjInfoIDItem(versionInternal.NewObjectVersion));
        }
      }
    }
    this._createVersionParams = new TechCardCreateVersionParams(ecoObjectInfo, (IEnumerable<RelObjInfoItem>) this._analyzerStepData.RelObjInfoItems)
    {
      SignedObjInfoItems = (IEnumerable<ObjInfoIDItem>) objInfoIdItemList,
      CompositionRelInfoItems = (IEnumerable<RelObjInfoItem>) this._analyzerStepData.CompositionItems
    };
    return true;
  }

  /// <summary>
  /// Получить контекст редактирования для объекта, требующего подпись
  /// </summary>
  /// <param name="signedObjId"></param>
  /// <param name="editingContextId"></param>
  /// <returns></returns>
  private bool GetEditingContextForObject(long signedObjId, out long editingContextId)
  {
    editingContextId = 0L;
    if (signedObjId == 0L || !(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || service.EditingContextID == 0L)
      return false;
    long editingContextId1 = service.EditingContextID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(signedObjId, false);
      if (objectActualCopy == null || objectActualCopy.ObjectModifyMode == ObjectModifyModes.CantModify || objectActualCopy.ObjectModifyMode == ObjectModifyModes.CreateVersion || objectActualCopy.CheckoutBy != 0L && objectActualCopy.CheckoutBy != service.UserID || !(sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService))
        return false;
      editingContextId = customService.ExistsInContext((object) sessionKeeper.Session, editingContextId1, signedObjId) ? editingContextId1 : 0L;
      return editingContextId != 0L;
    }
  }

  /// <summary>Конструктор</summary>
  public CreateVersionCommand()
    : base("CreateVersion")
  {
  }

  /// <summary>Ограничение на число подписываемых объектов = 1</summary>
  public bool SingleSignedObjectLimit { get; set; }

  /// <summary>Проверка входных параметров</summary>
  /// <returns></returns>
  protected override bool ValidateCommandArgs()
  {
    return base.ValidateCommandArgs() && this.Items.Count > 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool LoadCommandInfo()
  {
    return base.LoadCommandInfo() && this.LoadCommandInfo_ObjData();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    lock (this._syncRoot)
    {
      if (!this.ValidateCommandArgs())
        return;
      this._defaultCreateVersionHandler = false;
      try
      {
        if (!this.LoadCommandInfo() || !this.AnalyzeCommandData() || !this.ExecuteCommand())
          return;
        this.UpdateNotificationQueue();
      }
      finally
      {
        if (this._defaultCreateVersionHandler)
          ObjectCommands.CreateVersionCommand(this.Items, this.ContextServices, this.AdditionalInfo);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool ExecuteCommand()
  {
    if (!this.BeforeExecuteCommand() || !this.ExecuteCommandInternal())
      return false;
    this.AfterExecuteCommand();
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override void UpdateNotificationQueue()
  {
    if (ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false) == null || this._modificationLog == null || !this._modificationLog.Any<CategoryValue>())
      return;
    List<RelObjInfoItem> source = new List<RelObjInfoItem>();
    foreach (RelObjInfoItem createdRelInfo in this._createdRelInfoList)
    {
      CreatedVersionRelationItem versionRelationItem = createdRelInfo as CreatedVersionRelationItem;
      if (!((TypedInfoItem) versionRelationItem == (TypedInfoItem) null) && !((TypedInfoItem) versionRelationItem.PrototypeRelationItem == (TypedInfoItem) null))
        source.Add(versionRelationItem.PrototypeRelationItem);
    }
    if (source.Count != 0)
      this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) source.Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (item => item.RelationID)).ToArray<long>(), (IList<long>) source.Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToArray<long>(), (IList<int>) source.Select<RelObjInfoItem, int>((System.Func<RelObjInfoItem, int>) (item => item.ProjInfo.ObjTypeID)).ToArray<int>(), (IList<int>) source.Select<RelObjInfoItem, int>((System.Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToArray<int>()));
    this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) this._createdRelInfoList.Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (item => item.RelationID)).ToArray<long>(), (IList<long>) this._createdRelInfoList.Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToArray<long>(), (IList<int>) this._createdRelInfoList.Select<RelObjInfoItem, int>((System.Func<RelObjInfoItem, int>) (item => item.ProjInfo.ObjTypeID)).ToArray<int>(), (IList<int>) this._createdRelInfoList.Select<RelObjInfoItem, int>((System.Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToArray<int>()));
    foreach (NotificationEventArgs notificationEvent in TechcardClientControlsUtils.GetNotificationEvents((IList<CategoryValue>) this._modificationLog))
    {
      if (!(notificationEvent.EventName == "ObjectsChanged") && !(notificationEvent.EventName == "RelationsCreated"))
        this.Notifications.QueueEvent(notificationEvent);
    }
  }

  protected override void FlushNotificationQuery()
  {
    NavigatorTreeView service = ServiceUtils.GetService<NavigatorTreeView>((object) this.ContextServices, false);
    NodeIDPath focusedPath = service?.FocusedPath;
    base.FlushNotificationQuery();
    if (focusedPath == null)
      return;
    Dictionary<long, RelObjInfoItem> dictionary = new Dictionary<long, RelObjInfoItem>();
    foreach (RelObjInfoItem createdRelInfo in this._createdRelInfoList)
    {
      CreatedVersionRelationItem versionRelationItem = createdRelInfo as CreatedVersionRelationItem;
      if (!((TypedInfoItem) versionRelationItem == (TypedInfoItem) null) && !((TypedInfoItem) versionRelationItem.PrototypeRelationItem == (TypedInfoItem) null))
        dictionary[versionRelationItem.PrototypeRelationItem.RelationID] = (RelObjInfoItem) versionRelationItem;
    }
    NodeIDPath nodeIDPath = new NodeIDPath(focusedPath);
    nodeIDPath.Clear();
    foreach (object obj in focusedPath)
    {
      if (obj is NodeID NodeID1)
      {
        RelObjInfoItem relObjInfoItem;
        if (!dictionary.TryGetValue(NodeID1.PrjLinkID, out relObjInfoItem))
        {
          nodeIDPath.Add((INodeID) NodeID1);
        }
        else
        {
          NodeID NodeID = new NodeID(relObjInfoItem.PartInfo.ObjTypeID, relObjInfoItem.PartInfo.ObjectID, NodeID1.ID, NodeID1.CheckedOutBy, relObjInfoItem.RelationID, NodeID1.LCStepID, NodeID1.Caption, relObjInfoItem.RelTypeID, NodeID1.Owner, NodeID1.Sorting, NodeID1.State, NodeID1.Version, NodeID1.BaseVersion, NodeID1.SiteID, relObjInfoItem.ProjInfo.ObjectID, NodeID1.RelGuid, NodeID1.ModificationID);
          nodeIDPath.Add((INodeID) NodeID);
        }
      }
    }
    if (nodeIDPath.Length == 0)
      return;
    service.TryBrowse(nodeIDPath);
  }
}
