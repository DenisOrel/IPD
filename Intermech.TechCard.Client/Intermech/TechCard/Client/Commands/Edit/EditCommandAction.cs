// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Edit.EditCommandAction
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Commands.Action;
using Intermech.TechCard.Client.Common.Forms;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Services.CreateVersion;
using Intermech.TechCard.Client.Services.CreateVersion.Analyzer;
using Intermech.TechCard.Client.Services.DataProviders;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Edit;

/// <summary>
/// 
/// </summary>
/// <param name="actionParam"></param>
internal class EditCommandAction([NotNull] EditCommandActionParam actionParam) : CommandAction((CommandActionParam) actionParam)
{
  /// <summary>
  /// 
  /// </summary>
  protected RelObjInfoItem _targetRelObjInfo;
  /// <summary>Атрибуты объекта</summary>
  protected readonly AttributeStorage _objectAttributeStorage = new AttributeStorage();
  /// <summary>Атрибуты связи</summary>
  protected readonly AttributeStorage _relationAttributeStorage = new AttributeStorage();
  /// <summary>Список связей ЕТП с объектами требующие изменения</summary>
  protected readonly IList<RelObjInfoItem> _etpRelObjInfoList = (IList<RelObjInfoItem>) new List<RelObjInfoItem>();
  /// <summary>
  /// 
  /// </summary>
  protected readonly IList<CategoryValue> _modificationsList = (IList<CategoryValue>) new List<CategoryValue>();
  /// <summary>
  /// Список объектов для текущего контекста вида ID объекта -&gt; Описание объекта в контексте
  /// </summary>
  private readonly IDictionary<long, EditingContextsObjectVersion> _contextObjectCache = (IDictionary<long, EditingContextsObjectVersion>) new Dictionary<long, EditingContextsObjectVersion>();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool LoadRelationInfo()
  {
    if (!(this._actionParam.SelectedItems.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData1) || !(this._actionParam.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2))
      return false;
    IDBTypedObjectID parentData = this._actionParam.SelectedItems.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    this._targetRelObjInfo = new RelObjInfoItem(itemData1.Value, itemData1.RelationType)
    {
      PartInfo = (ObjInfoItem) new ObjInfoIDItem(itemData2.ObjectID, itemData2.ObjectType, itemData2.ID),
      ProjInfo = parentData != null ? (ObjInfoItem) new ObjInfoIDItem(parentData.ObjectID, parentData.ObjectType, parentData.ID) : (ObjInfoItem) null
    };
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._targetRelObjInfo.ProjInfo) && itemData1.ProjID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(itemData1.ProjID);
        this._targetRelObjInfo.ProjInfo = (ObjInfoItem) new ObjInfoIDItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.ID);
      }
    }
    return !RelInfoItem.IsEmpty((RelInfoItem) this._targetRelObjInfo) && !ObjInfoItem.IsEmpty((ITypedInfoItem) this._targetRelObjInfo.ProjInfo) && !ObjInfoItem.IsEmpty((ITypedInfoItem) this._targetRelObjInfo.PartInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool LoadAttributes()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(this._targetRelObjInfo.PartInfo.ObjectID, false);
      if (dbObject == null)
      {
        string caption = LocalizationHolder.rm.GetString(sc_19291.ssp_techcard_19292());
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_394"), (object) this._targetRelObjInfo.PartInfo.ObjectID), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      this.DoLoadObjectAttributes(dbObject);
      this.DoLoadRelationAttributes(session.GetRelation(this._targetRelObjInfo.RelationID, true));
      return true;
    }
  }

  private void LoadContextObjects()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session.EditingContextID == 0L)
        return;
      foreach (EditingContextsObjectVersion contextsObjectVersion in ServiceUtils.GetService<IDBEditingContextsService>((object) session, true).GetEditingContextsObject((object) session.SessionGUID, session.EditingContextID, false, false).Objects)
        this._contextObjectCache[contextsObjectVersion.F_ID] = contextsObjectVersion;
    }
  }

  /// <summary>Загрузка атрибутов объекта</summary>
  /// <returns></returns>
  protected virtual void DoLoadObjectAttributes(IDBObject dbObject)
  {
    AttributeStorage.LoadAttributes((IDBAttributable) dbObject, this._objectAttributeStorage.Values);
  }

  /// <summary>Загрузка атрибутов связей</summary>
  /// <param name="dbRelation"></param>
  protected void DoLoadRelationAttributes(IDBRelation dbRelation)
  {
    AttributeStorage.LoadAttributes((IDBAttributable) dbRelation, this._relationAttributeStorage.Values);
  }

  /// <summary>
  /// Получение информации о ЕТП объектах, требующих изменения
  /// </summary>
  private void SelectLinkedEtpObjects()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Dictionary<RelInfoItem, ObjInfoItem> etpRel2ObjList;
      if (!TechProcGroupUtils.GetEtpRelIDListByDialog((RelInfoItem) this._targetRelObjInfo, LocalizationHolder.rm.GetString("TechCard.Client_254"), LocalizationHolder.rm.GetString("TechCard.Client_420"), sessionKeeper.Session, out etpRel2ObjList))
        return;
      foreach (KeyValuePair<RelInfoItem, ObjInfoItem> keyValuePair in etpRel2ObjList)
      {
        RelObjInfoItem key = (RelObjInfoItem) keyValuePair.Key;
        RelObjInfoItem relObjInfoItem1 = key;
        if (!(key.PartInfo is ObjInfoIDItem objInfoIdItem1))
          objInfoIdItem1 = new ObjInfoIDItem((TypedInfoItem) key.PartInfo);
        relObjInfoItem1.PartInfo = (ObjInfoItem) objInfoIdItem1;
        RelObjInfoItem relObjInfoItem2 = key;
        if (!(key.ProjInfo is ObjInfoIDItem objInfoIdItem2))
          objInfoIdItem2 = new ObjInfoIDItem((TypedInfoItem) key.ProjInfo);
        relObjInfoItem2.ProjInfo = (ObjInfoItem) objInfoIdItem2;
      }
      foreach (RelObjInfoItem relObjInfoItem in new TechCardRelObjInfoItemsTypeUpdater<RelObjInfoItem>((ITechCardDataEnumerableProvider<RelObjInfoItem>) new TechCardDataEnumerableSimpleProvider<RelObjInfoItem>(etpRel2ObjList.Select<KeyValuePair<RelInfoItem, ObjInfoItem>, RelObjInfoItem>((Func<KeyValuePair<RelInfoItem, ObjInfoItem>, RelObjInfoItem>) (item => (RelObjInfoItem) item.Key)))).Execute())
      {
        ObjInfoIDItem partInfo = relObjInfoItem.PartInfo as ObjInfoIDItem;
        EditingContextsObjectVersion contextsObjectVersion;
        if (!((TypedInfoItem) partInfo == (TypedInfoItem) null) && (!this._contextObjectCache.TryGetValue(partInfo.ID, out contextsObjectVersion) || Math.Abs(partInfo.ObjectID) == contextsObjectVersion.F_OBJECT_ID))
        {
          ObjInfoIDItem projInfo = relObjInfoItem.ProjInfo as ObjInfoIDItem;
          if (!((TypedInfoItem) projInfo == (TypedInfoItem) null) && (!this._contextObjectCache.TryGetValue(projInfo.ID, out contextsObjectVersion) || Math.Abs(projInfo.ObjectID) == contextsObjectVersion.F_OBJECT_ID))
            this._etpRelObjInfoList.Add(relObjInfoItem);
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected virtual bool EditObject()
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._targetRelObjInfo.PartInfo))
      return false;
    DescriptorCollection descriptors = new DescriptorCollection()
    {
      (IDescriptor) new RelObjInfoDescriptor(this._targetRelObjInfo)
    };
    ISelectedItems items = Intermech.Navigator.ContextMenu.ObjectExtensions.GetItems((IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(Intermech.Navigator.Consts.CategoryCustomNode, 0, string.Empty, descriptors), (System.IServiceProvider) null);
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.StartLogHistory();
      try
      {
        ObjectCommands.ParametersCardCommand(items, (System.IServiceProvider) viewServices, (object) null);
        this._modificationsList.AddRange<CategoryValue>((IEnumerable<CategoryValue>) sessionKeeper.Session.GetModificationsHistoryList());
      }
      finally
      {
        sessionKeeper.Session.StopLogHistory();
      }
    }
    return this.CheckModifications();
  }

  /// <summary>Анализ изменений</summary>
  /// <returns></returns>
  protected bool CheckModifications()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._targetRelObjInfo.PartInfo.ObjectID, false);
      if (dbObject == null)
        return false;
      AttributeStorage.LoadAttributes((IDBAttributable) dbObject, this._objectAttributeStorage.NewValues);
      AttributeStorage.LoadAttributes((IDBAttributable) sessionKeeper.Session.GetRelation(this._targetRelObjInfo.RelationID, true), this._relationAttributeStorage.NewValues);
    }
    this._objectAttributeStorage.ExtractDeltaValues();
    this._relationAttributeStorage.ExtractDeltaValues();
    return this._objectAttributeStorage.DeltaValues.Any<AttributeValues>() || this._relationAttributeStorage.DeltaValues.Any<AttributeValues>();
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void AcceptChanges()
  {
    if (this._etpRelObjInfoList == null || this._etpRelObjInfoList.Count == 0)
      return;
    this.CheckObjectsModifications();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.StartLogHistory();
      IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      try
      {
        customService?.StartTransaction();
        foreach (RelObjInfoItem etpRelObjInfo in (IEnumerable<RelObjInfoItem>) this._etpRelObjInfoList)
        {
          if (this._objectAttributeStorage.DeltaValues.Any<AttributeValues>())
            sessionKeeper.Session.GetObject(etpRelObjInfo.PartInfo.ObjectID, false)?.SetAttributesValues(this._objectAttributeStorage.DeltaValues.ToArray<AttributeValues>());
          if (this._relationAttributeStorage.DeltaValues.Any<AttributeValues>())
            sessionKeeper.Session.GetRelation(etpRelObjInfo.RelationID, false)?.SetAttributesValues(this._relationAttributeStorage.DeltaValues.ToArray<AttributeValues>());
        }
        customService?.Commit();
        this._modificationsList.AddRange<CategoryValue>((IEnumerable<CategoryValue>) sessionKeeper.Session.GetModificationsHistoryList());
      }
      catch (Exception ex)
      {
        customService?.Rollback();
        throw;
      }
      finally
      {
        sessionKeeper.Session.StopLogHistory();
      }
    }
  }

  protected bool CheckObjectsModifications()
  {
    List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      foreach (RelObjInfoItem etpRelObjInfo in (IEnumerable<RelObjInfoItem>) this._etpRelObjInfoList)
      {
        if (this._objectAttributeStorage.DeltaValues.Any<AttributeValues>())
        {
          IDBObject dbObject1 = session.GetObject(etpRelObjInfo.PartInfo.ObjectID, true);
          switch (dbObject1.ObjectModifyMode)
          {
            case ObjectModifyModes.Checkout:
              if (dbObject1.CheckoutBy != session.UserID)
              {
                IDBObject dbObject2 = dbObject1.CheckOut();
                etpRelObjInfo.PartInfo = (ObjInfoItem) new ObjInfoIDItem(dbObject2);
                break;
              }
              break;
            case ObjectModifyModes.CreateVersion:
              relObjInfoItemList.Add(etpRelObjInfo);
              break;
          }
        }
        if (this._relationAttributeStorage.DeltaValues.Any<AttributeValues>())
        {
          IDBObject dbObject3 = session.GetObject(etpRelObjInfo.ProjInfo.ObjectID, true);
          switch (dbObject3.ObjectModifyMode)
          {
            case ObjectModifyModes.Checkout:
              if (dbObject3.CheckoutBy != session.UserID)
              {
                IDBObject dbObject4 = dbObject3.CheckOut();
                etpRelObjInfo.ProjInfo = (ObjInfoItem) new ObjInfoIDItem(dbObject4);
                continue;
              }
              continue;
            case ObjectModifyModes.CreateVersion:
              if (!relObjInfoItemList.Contains(etpRelObjInfo))
              {
                relObjInfoItemList.Add(etpRelObjInfo);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
    return !relObjInfoItemList.Any<RelObjInfoItem>() || this.CreateObjectVersions((IList<RelObjInfoItem>) relObjInfoItemList);
  }

  private bool CreateObjectVersions(IList<RelObjInfoItem> relObjInfoItems)
  {
    long editingContextId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      editingContextId = sessionKeeper.Session.EditingContextID;
    if (editingContextId == 0L)
      throw new Exception("Извещение не задано. Создание версий невозможно");
    TechCardDataEnumerableSimpleProvider<RelObjInfoItem> relObjInfoProvider = new TechCardDataEnumerableSimpleProvider<RelObjInfoItem>((IEnumerable<RelObjInfoItem>) relObjInfoItems);
    TechCardCreateVersionAnalyzerStepData stepData;
    if (!new TechCardCreateVersionAnalyzer(new TechCardObjectCreateVersionAnalyzerParam((ITechCardDataEnumerableProvider<RelObjInfoItem>) relObjInfoProvider)
    {
      CompositionProvider = (ITechCardDataEnumerableProvider<RelObjInfoItem>) relObjInfoProvider,
      AnalyzerSteps = (IEnumerable<TechCardCreateVersionAnalyzerStep>) new TechCardCreateVersionAnalyzerStep[3]
      {
        (TechCardCreateVersionAnalyzerStep) new TechCardCreateVersionAnalyzerCopyModeStep(),
        (TechCardCreateVersionAnalyzerStep) new TechCardCreateVersionAnalyzerSignApplicabilityStep(),
        (TechCardCreateVersionAnalyzerStep) new TechCardCreateVersionAnalyzerSignObjectStep(false)
      }
    }, (System.IServiceProvider) null).Execute(out stepData) || !stepData.RelObjInfoItems.Any<RelObjInfoItem>())
      return false;
    List<ObjInfoIDItem> objInfoIdItemList = new List<ObjInfoIDItem>();
    List<ObjInfoIDItem> objInfoList = new List<ObjInfoIDItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      foreach (KeyValuePair<RelObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<RelObjInfoItem, ObjInfoItem>>) stepData.RelObjInfo2SignedObjCache)
      {
        QuickObjectInfo objectInfo = session.GetObjectInfo(keyValuePair.Value.ObjectID);
        EditingContextsObjectVersion contextsObjectVersion;
        if (this._contextObjectCache.TryGetValue(objectInfo.ID, out contextsObjectVersion))
          objInfoIdItemList.Add(new ObjInfoIDItem(contextsObjectVersion.F_OBJECT_ID, objectInfo.ObjectTypeID, objectInfo.ID));
        else
          objInfoList.Add(new ObjInfoIDItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.ID));
      }
    }
    if (objInfoList.Count > 0)
    {
      string caption = sc_19291.ssp_techcard_19293();
      TechDictDescriptor techDictDescriptor = new TechDictDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, 0, caption, ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) objInfoList));
      techDictDescriptor.ExpandNodes = false;
      TechcardErrorObjForm techcardErrorObjForm = new TechcardErrorObjForm();
      string errorMsg = "Требуется выпуск версий следующих объектов. Продолжить?";
      techcardErrorObjForm.ShowBtn_OK = true;
      techcardErrorObjForm.LoadData(errorMsg, (IDescriptor) techDictDescriptor);
      if (techcardErrorObjForm.ShowDialog() != DialogResult.OK)
        return false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        List<ObjInfoItem> list = this._contextObjectCache.Values.Select<EditingContextsObjectVersion, ObjInfoItem>((Func<EditingContextsObjectVersion, ObjInfoItem>) (item => new ObjInfoItem(item.F_OBJECT_ID))).ToList<ObjInfoItem>();
        ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) list, sessionKeeper.Session);
        ObjInfoItem objInfoItem = list.FirstOrDefault<ObjInfoItem>((Func<ObjInfoItem, bool>) (item => MetaDataHelper.IsObjectTypeChildOf(item.ObjTypeID, TechCardConsts.ObjectTypes.TechProcGroupID) || MetaDataHelper.IsObjectTypeChildOf(item.ObjTypeID, TechCardConsts.ObjectTypes.TechProcTipovID)));
        if ((TypedInfoItem) objInfoItem == (TypedInfoItem) null)
          return false;
        int relationTypeId = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
        IDBRelation relation = sessionKeeper.Session.GetRelation(editingContextId, objInfoItem.ObjectID, relationTypeId, true);
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeId);
        foreach (ObjInfoIDItem objInfoIdItem in objInfoList)
        {
          CreateVersionResult versionInternal = ((IClientDBObjectCollection) sessionKeeper.Session.GetObjectCollection(objInfoIdItem.ObjTypeID)).CreateVersionInternal(objInfoIdItem.ObjectID);
          try
          {
            NewRelationProperties properties = new NewRelationProperties(-1L, editingContextId, versionInternal.NewObjectVersion.ID)
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
            versionInternal.Commit(session);
          }
          catch
          {
            versionInternal.Rollback(session);
            throw;
          }
          objInfoIdItemList.Add(new ObjInfoIDItem(versionInternal.NewObjectVersion));
        }
      }
    }
    TechCardCreateVersionParams createVersionParams = new TechCardCreateVersionParams(new ObjInfoItem(editingContextId), (IEnumerable<RelObjInfoItem>) stepData.RelObjInfoItems)
    {
      SignedObjInfoItems = (IEnumerable<ObjInfoIDItem>) objInfoIdItemList,
      CompositionRelInfoItems = (IEnumerable<RelObjInfoItem>) stepData.CompositionItems
    };
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) createVersionParams.EcoObjectInfo))
      return false;
    IEnumerable<RelObjInfoItem> createdRelInfoItems;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.StartLogHistory();
      try
      {
        if (!ServiceUtils.GetService<ITechCardCreateVersionService>((object) ApplicationServices.Container, true).Execute(sessionKeeper.Session, createVersionParams, out createdRelInfoItems))
          return false;
        this._modificationsList.AddRange<CategoryValue>((IEnumerable<CategoryValue>) sessionKeeper.Session.GetModificationsHistoryList());
      }
      finally
      {
        sessionKeeper.Session.StopLogHistory();
      }
    }
    foreach (RelObjInfoItem relObjInfoItem1 in (IEnumerable<RelObjInfoItem>) relObjInfoItems)
    {
      long partId = ((ObjInfoIDItem) relObjInfoItem1.PartInfo).ID;
      long projId = ((ObjInfoIDItem) relObjInfoItem1.ProjInfo).ID;
      RelObjInfoItem relObjInfoItem2 = createdRelInfoItems.FirstOrDefault<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item => ((ObjInfoIDItem) item.PartInfo).ID == partId && ((ObjInfoIDItem) item.ProjInfo).ID == projId));
      if (!((TypedInfoItem) relObjInfoItem2 == (TypedInfoItem) null))
        relObjInfoItem1.CopyFrom((TypedInfoItem) relObjInfoItem2);
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override bool Execute(out IList<CategoryValue> modificationsList)
  {
    modificationsList = this._modificationsList;
    if (!this.LoadRelationInfo() || !this.LoadAttributes() || !this.EditObject())
      return false;
    this.LoadContextObjects();
    this.SelectLinkedEtpObjects();
    this.AcceptChanges();
    return true;
  }
}
