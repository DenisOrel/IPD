// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechObjectCreatorProcRouteSupportControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.ObjectCreator;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Navigator.VirtualNodes;
using Intermech.Search;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.TcObjectsTypes;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>
/// Базовый control для создателей технологических объектов, входящих в состав МО
/// </summary>
internal class TechObjectCreatorProcRouteSupportControl : TechObjectCreatorBaseControl
{
  /// <summary>идентификатор изделия</summary>
  protected long _articleId;
  /// <summary>Краткая информация об изделии</summary>
  protected QuickObjectInfo _articleObjInfo;
  /// <summary>идентификатор маршрута обработки</summary>
  protected long _moObjectId;
  /// <summary>Список всех МО-Изделий для текущего объекта</summary>
  private List<Tuple<ObjInfoItem, ObjInfoItem>> _mo2ArticleList;
  /// <summary>
  /// Коллекция идентификаторов МО, которые были созданы в процессе привязки к Изделию/Мо
  /// </summary>
  private List<long> _createdMoList = new List<long>();
  /// <summary>
  /// 
  /// </summary>
  protected Control _objNameControl;
  /// <summary>
  /// 
  /// </summary>
  protected Control _objDesignationControl;
  /// <summary>
  /// 
  /// </summary>
  protected Control _artNameControl;
  /// <summary>
  /// 
  /// </summary>
  protected Control _moNameControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControlData()
  {
  }

  /// <summary>
  /// Загрузка информации о контексте объекта (Изделие / МО)
  /// </summary>
  protected virtual void LoadContextObjectData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      List<long> longList = new List<long>();
      if (this.CreatedObject.ObjectRelationArray != null)
        longList.AddRange(this.CreatedObject.ObjectRelationArray.Select<ObjectRelationLink, long>((System.Func<ObjectRelationLink, long>) (item => item.ObjectID)));
      if ((this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams ? creatorExtraParams.Items : (ISelectedItems) null) != null)
      {
        IDBObjectID itemData = creatorExtraParams.Items.GetItemData<IDBObjectID>(0, false);
        if (itemData != null)
          longList.Add(itemData.Value);
        IDBObjectID parentData = creatorExtraParams.Items.GetParentData<IDBObjectID>(0, false);
        if (parentData != null)
          longList.Add(parentData.Value);
      }
      if (longList.Count == 0 && this.CreatedObject.PrototypeID != 0L && this.CreatedObject.PrototypeID != -1L)
      {
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
        {
          new ConditionStructure(-7, RelationalOperators.Equal, (object) TechCardConsts.ObjectTypes.ProcRoutingID, LogicalOperators.NONE, 0, false)
        };
        List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(this.CreatedObject.PrototypeID, session, new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, false, conditionStructureList.ToArray());
        if (parentSostavTree != null && parentSostavTree.Count != 0)
          longList.Add(parentSostavTree[0].ProjID);
      }
      long articleObjId;
      long moObjId;
      if (TechObjectCreatorProcRouteSupportControl.GeRefArtMoData(longList.ToArray(), out articleObjId, out moObjId, session))
      {
        if (moObjId == 0L && articleObjId != 0L)
        {
          IList<long> createdObjects;
          moObjId = ProcRouteHelper.GetDefaultProcRouteForArticle(articleObjId, session, true, out createdObjects);
          this._createdMoList.AddRange((IEnumerable<long>) createdObjects);
        }
      }
      else if (TechObjectCreatorProcRouteSupportControl.GeRefArtMoData(longList.ToArray(), out this._mo2ArticleList, session) && this._mo2ArticleList.Count == 1)
      {
        moObjId = this._mo2ArticleList[0].Item1.ObjectID;
        articleObjId = this._mo2ArticleList[0].Item2.ObjectID;
      }
      this.UpdateArticleData(articleObjId, moObjId == 0L, false, session);
      if (moObjId == 0L)
        return;
      this.UpdateMoData(moObjId, session);
    }
  }

  /// <summary>Получить контекст производственной копии</summary>
  /// <returns></returns>
  protected virtual bool GetProductionContext(out long productionItem)
  {
    productionItem = 0L;
    List<int> articleCopyObjTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ArticleCopyBaseID);
    if (this._articleObjInfo.ObjectID != 0L)
    {
      if (!articleCopyObjTypes.Contains(this._articleObjInfo.ObjectTypeID))
        return false;
      productionItem = this._articleObjInfo.ObjectID;
      return true;
    }
    IEnumerable<RelObjInfoItem> relObjInfoItems;
    if ((this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams ? creatorExtraParams.Items : (ISelectedItems) null) == null || !TechcardClientControlsUtils.GetItemsApplicabilityInfo(creatorExtraParams.Items, (System.IServiceProvider) ApplicationServices.Container, out relObjInfoItems))
      return false;
    RelObjInfoItem relObjInfoItem = relObjInfoItems.FirstOrDefault<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (a => articleCopyObjTypes.Contains(a.PartInfo.ObjTypeID)));
    if ((TypedInfoItem) relObjInfoItem != (TypedInfoItem) null)
    {
      productionItem = relObjInfoItem.PartInfo.ObjectID;
      return true;
    }
    ObjInfoItem partInfo = relObjInfoItems.Last<RelObjInfoItem>()?.PartInfo;
    if ((TypedInfoItem) partInfo == (TypedInfoItem) null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<Tuple<ObjInfoItem, ObjInfoItem>> mo2ArticleList;
      if (!TechObjectCreatorProcRouteSupportControl.GeRefArtMoData(new long[1]
      {
        partInfo.ObjectID
      }, out mo2ArticleList, sessionKeeper.Session))
        return false;
      Tuple<ObjInfoItem, ObjInfoItem> tuple = mo2ArticleList.FirstOrDefault<Tuple<ObjInfoItem, ObjInfoItem>>((System.Func<Tuple<ObjInfoItem, ObjInfoItem>, bool>) (a => articleCopyObjTypes.Contains(a.Item2.ObjTypeID)));
      if (tuple != null)
      {
        productionItem = tuple.Item2.ObjectID;
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  protected virtual void ClassifyObjName(IUserSession session)
  {
    if (this._articleId == 0L)
      return;
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(this.CreatedObject.ObjectID, this.CreatedObject.ObjectTypeID);
    ObjInfoItem contextObjectItem = new ObjInfoItem(this._articleObjInfo.ObjectID, this._articleObjInfo.ObjectTypeID);
    IEnumerable<RelObjInfoItem> relObjInfoItems;
    IEnumerable<ObjInfoItem> objInfoItems = !(this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams) || !TechcardClientControlsUtils.GetItemsApplicabilityInfo(creatorExtraParams.Items, (System.IServiceProvider) ApplicationServices.Container, out relObjInfoItems) ? (IEnumerable<ObjInfoItem>) null : relObjInfoItems.Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo));
    ITechCardClassifyObjectService classifyObjectService1 = service;
    IUserSession session1 = session;
    TechCardClassifyObjectAttributeParams classifyParams1 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem);
    classifyParams1.ExtraContextObjInfoItems = objInfoItems;
    TechCardClassifyObjectDesignationStrategy classifyStrategy1 = new TechCardClassifyObjectDesignationStrategy();
    string str1;
    ref string local1 = ref str1;
    int num1 = classifyObjectService1.ClassifyObjectAttribute(session1, classifyParams1, (ITechCardClassifyObjectStrategy) classifyStrategy1, out local1) ? 1 : 0;
    ITechCardClassifyObjectService classifyObjectService2 = service;
    IUserSession session2 = session;
    TechCardClassifyObjectAttributeParams classifyParams2 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, contextObjectItem);
    classifyParams2.ExtraContextObjInfoItems = objInfoItems;
    TechCardClassifyObjectNameStrategy classifyStrategy2 = new TechCardClassifyObjectNameStrategy();
    string str2;
    ref string local2 = ref str2;
    int num2 = classifyObjectService2.ClassifyObjectAttribute(session2, classifyParams2, (ITechCardClassifyObjectStrategy) classifyStrategy2, out local2) ? 1 : 0;
    if ((num1 | num2) == 0)
      return;
    this.ObjectName = str2;
    this.ObjectDesignation = str1;
  }

  /// <summary>Обновление параметров изделия</summary>
  /// <param name="artObjId"></param>
  /// <param name="updateMoData"></param>
  /// <param name="needObjClassify"></param>
  /// <param name="session"></param>
  protected void UpdateArticleData(
    long artObjId,
    bool needProcRouteUpdate,
    bool needObjClassify,
    IUserSession session)
  {
    try
    {
      if (this._articleId == artObjId && !needProcRouteUpdate)
        return;
      this._articleId = artObjId;
      this._articleObjInfo = session.GetObjectInfo(this._articleId);
      this.ArtObjectName = this._articleObjInfo.Caption;
      if (needProcRouteUpdate)
      {
        IList<long> createdObjects;
        this.UpdateMoData(ProcRouteHelper.GetDefaultProcRouteForArticle(this._articleId, session, true, out createdObjects), session);
        this._createdMoList.AddRange((IEnumerable<long>) createdObjects);
      }
      if (!needObjClassify)
        return;
      this.ClassifyObjName(session);
    }
    finally
    {
      this.ValidateArtData(session);
    }
  }

  /// <summary>Обновление значения маршрута обработки</summary>
  /// <param name="moObjId"></param>
  /// <param name="session"></param>
  protected void UpdateMoData(long moObjId, IUserSession session)
  {
    try
    {
      if (this._moObjectId == moObjId)
        return;
      this._moObjectId = moObjId;
      this.MoObjectName = TechCardConsts.Utils.GetObjectString(this._moObjectId, session);
    }
    finally
    {
      this.ValidateMoData(session);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void SelectPrototype()
  {
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(this.CreatedObject.ObjectTypeID);
    if (objectTypeGuid.Equals(Guid.Empty))
      return;
    List<IDescriptor> descriptorList = new List<IDescriptor>();
    long productionItem;
    if (!this.GetProductionContext(out productionItem))
    {
      IList<ObjInfoItem> articleObjInfoList;
      IList<ObjInfoItem> prototypeObjInfoList;
      IList<ObjInfoItem> groupObjInfoList;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        articleObjInfoList = TechObjectCreatorProcRouteSupportControl.GetArticleObjInfoList((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
        {
          new ObjInfoItem(this._articleObjInfo.ObjectID, this._articleObjInfo.ObjectTypeID)
        }, this.CreatedObject.ObjectTypeID, sessionKeeper.Session);
        prototypeObjInfoList = (IList<ObjInfoItem>) TechObjectCreatorProcRouteSupportControl.GetPrototypeObjInfoList(this._articleId, this.CreatedObject.ObjectTypeID, sessionKeeper.Session);
        groupObjInfoList = (IList<ObjInfoItem>) TechObjectCreatorProcRouteSupportControl.GetArticleGroupObjInfoList(this._articleId, this.CreatedObject.ObjectTypeID, sessionKeeper.Session);
      }
      DictDescriptor dictDescriptor1 = new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, this.CreatedObject.ObjectTypeID, string.Format(LocalizationHolder.rm.GetString("TechCard.Client_542"), (object) MetaDataHelper.GetObjectTypeName(this.CreatedObject.ObjectTypeID)), SomeTypedInfoHelper<ObjInfoItem>.GetItemTypeCache((IEnumerable<ObjInfoItem>) articleObjInfoList))
      {
        ExpandNodes = false
      };
      descriptorList.Add((IDescriptor) dictDescriptor1);
      DictDescriptor dictDescriptor2 = new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, this.CreatedObject.ObjectTypeID, string.Format(LocalizationHolder.rm.GetString("TechCard.Client_506"), (object) MetaDataHelper.GetObjectTypeName(this.CreatedObject.ObjectTypeID)), SomeTypedInfoHelper<ObjInfoItem>.GetItemTypeCache((IEnumerable<ObjInfoItem>) prototypeObjInfoList))
      {
        ExpandNodes = false
      };
      descriptorList.Add((IDescriptor) dictDescriptor2);
      DictDescriptor dictDescriptor3 = new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, this.CreatedObject.ObjectTypeID, string.Format(LocalizationHolder.rm.GetString("TechCard.Client_508"), (object) MetaDataHelper.GetObjectTypeName(this.CreatedObject.ObjectTypeID)), SomeTypedInfoHelper<ObjInfoItem>.GetItemTypeCache((IEnumerable<ObjInfoItem>) groupObjInfoList))
      {
        ExpandNodes = false
      };
      descriptorList.Add((IDescriptor) dictDescriptor3);
    }
    else
    {
      DescriptorCollection descriptors = new DescriptorCollection();
      descriptors.AddRange((IEnumerable<IDescriptor>) new HiveDescriptor[3]
      {
        new HiveDescriptor(SelectObjectFromArticleWizardControl.RootCategoryNodeId, this.CreatedObject.ObjectTypeID, "Для изделия"),
        new HiveDescriptor(SelectObjectFromProductionReportWizardControl.RootCategoryNodeId, this.CreatedObject.ObjectTypeID, "Из состава производственной ведомости"),
        (HiveDescriptor) new TypedHiveDescriptor<long>(SelectObjectFromProductionCopyWizardControl.RootCategoryNodeId, this.CreatedObject.ObjectTypeID, "Для производственной копии ДСЕ", productionItem)
      });
      Intermech.Navigator.CustomNode.Descriptor descriptor = new Intermech.Navigator.CustomNode.Descriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, this.CreatedObject.ObjectTypeID, "Выбор из мастера", descriptors);
      descriptorList.Add((IDescriptor) descriptor);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(productionItem, new Guid("cadd9a8c-306c-11d8-b4e9-00304f19f545"));
        long num = 0;
        if (objectAttributeByGuid != null)
          num = objectAttributeByGuid.AsInteger;
        if (num != 0L)
        {
          IList<ObjInfoItem> articleObjInfoList = TechObjectCreatorProcRouteSupportControl.GetArticleObjInfoList((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
          {
            new ObjInfoItem(num)
          }, this.CreatedObject.ObjectTypeID, sessionKeeper.Session);
          DictDescriptor dictDescriptor4 = new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, this.CreatedObject.ObjectTypeID, string.Format(LocalizationHolder.rm.GetString("TechCard.Client_544"), (object) MetaDataHelper.GetObjectTypeName(this.CreatedObject.ObjectTypeID)), SomeTypedInfoHelper<ObjInfoItem>.GetItemTypeCache((IEnumerable<ObjInfoItem>) articleObjInfoList))
          {
            ExpandNodes = false
          };
          descriptorList.Add((IDescriptor) dictDescriptor4);
          IList<ObjInfoItem> groupObjInfoList = (IList<ObjInfoItem>) TechObjectCreatorProcRouteSupportControl.GetArticleGroupObjInfoList(num, this.CreatedObject.ObjectTypeID, sessionKeeper.Session);
          DictDescriptor dictDescriptor5 = new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, this.CreatedObject.ObjectTypeID, string.Format(LocalizationHolder.rm.GetString("TechCard.Client_545"), (object) MetaDataHelper.GetObjectTypeName(this.CreatedObject.ObjectTypeID)), SomeTypedInfoHelper<ObjInfoItem>.GetItemTypeCache((IEnumerable<ObjInfoItem>) groupObjInfoList))
          {
            ExpandNodes = false
          };
          descriptorList.Add((IDescriptor) dictDescriptor5);
        }
      }
    }
    AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer();
    serviceContainer.AddService(typeof (IViewsProvider), (object) new SelectObjectFromArticleWizardProvider());
    long templateObjId = TechCardClientConst.SelectObjectDlg(objectTypeGuid, LocalizationHolder.rm.GetString("TechCard.Client_507"), descriptorList.ToArray(), LocalizationHolder.rm.GetString("TechCard.Client_505"), (System.IServiceProvider) serviceContainer);
    if (templateObjId == 0L)
      return;
    this._prototypeNeedCopyAttrs = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateTemplateData(templateObjId, sessionKeeper.Session, false);
  }

  /// <summary>Выбор изделия</summary>
  protected void SelectArticle()
  {
    IEnumerable<long> source = this._mo2ArticleList == null ? (IEnumerable<long>) TechCardClientConst.SelectObjectsDlg((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes, LocalizationHolder.rm.GetString(sc_19625.ssp_techcard_19626())) : (IEnumerable<long>) TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.ArticleBaseID, (IList<ObjInfoItem>) this._mo2ArticleList.Select<Tuple<ObjInfoItem, ObjInfoItem>, ObjInfoItem>((System.Func<Tuple<ObjInfoItem, ObjInfoItem>, ObjInfoItem>) (item => item.Item2)).ToList<ObjInfoItem>(), LocalizationHolder.rm.GetString("TechCard.Client_503"), LocalizationHolder.rm.GetString("TechCard.Client_276"));
    if (!source.Any<long>())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateArticleData(source.First<long>(), true, true, sessionKeeper.Session);
  }

  /// <summary>Выбор МО</summary>
  protected virtual void SelectProcRoute()
  {
    if (this._articleId != 0L)
    {
      long objectId;
      if (!this.SelectProcRouteForObjectType(out objectId) || objectId == this._moObjectId)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.UpdateMoData(objectId, sessionKeeper.Session);
    }
    else
    {
      long num;
      if (this._mo2ArticleList != null)
      {
        List<long> longList = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.ProcRoutingID, (IList<ObjInfoItem>) this._mo2ArticleList.Select<Tuple<ObjInfoItem, ObjInfoItem>, ObjInfoItem>((System.Func<Tuple<ObjInfoItem, ObjInfoItem>, ObjInfoItem>) (item => item.Item1)).ToList<ObjInfoItem>(), LocalizationHolder.rm.GetString("TechCard.Client_504"), LocalizationHolder.rm.GetString("TechCard.Client_277"));
        num = longList == null || longList.Count == 0 ? 0L : longList[0];
      }
      else
        num = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.ProcRoutingGUID, LocalizationHolder.rm.GetString("TechCard.Client_277"));
      if (num == 0L || num == this._moObjectId)
        return;
      List<long> articlesForProcRoute;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        articlesForProcRoute = ProcRouteHelper.GetArticlesForProcRoute(num, sessionKeeper.Session);
      long artObjId = this._articleId;
      if (articlesForProcRoute.Count > 0)
      {
        if (!articlesForProcRoute.Contains(artObjId))
          artObjId = articlesForProcRoute[0];
      }
      else
        artObjId = 0L;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.UpdateArticleData(artObjId, false, true, sessionKeeper.Session);
        this.UpdateMoData(num, sessionKeeper.Session);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  protected virtual bool SelectProcRouteForObjectType(out long objectId)
  {
    objectId = 0L;
    return false;
  }

  /// <summary>Обновление значения прототипа объекта</summary>
  /// <param name="templateObjId"></param>
  /// <param name="session"></param>
  /// <param name="forceMode">Режим принудительного обновления</param>
  protected virtual bool UpdateTemplateData(
    long templateObjId,
    IUserSession session,
    bool forceMode)
  {
    if (this._prototypeObjId == templateObjId && !forceMode)
      return false;
    this._prototypeObjId = templateObjId;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void ValidateObjData()
  {
    if (this._objDesignationControl != null)
    {
      this.SetControlErrorMsg(this._objDesignationControl, string.Empty);
      if (this.ObjectDesignation == string.Empty && (MetaDataHelper.GetAttribute4ObjectType(this.CreatedObject.ObjectTypeID, MetaDataHelper.GetAttributeTypeID(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"))).Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
        this.SetControlErrorMsg(this._objDesignationControl, LocalizationHolder.rm.GetString(sc_19625.ssp_techcard_19627()));
    }
    Control objNameControl = this._objNameControl;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected virtual void ValidateArtData(IUserSession session)
  {
    if (this._artNameControl == null)
      return;
    this.SetControlErrorMsg(this._artNameControl, string.Empty);
    if (this._articleId != 0L)
      return;
    this.SetControlErrorMsg(this._artNameControl, string.Format(LocalizationHolder.rm.GetString(sc_19625.ssp_techcard_19628()), (object) LocalizationHolder.rm.GetString(sc_19625.ssp_techcard_19629())));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected virtual void ValidateMoData(IUserSession session)
  {
    if (this._moNameControl == null)
      return;
    this.SetControlErrorMsg(this._moNameControl, string.Empty);
    if (this._moObjectId == 0L)
    {
      this.SetControlErrorMsg(this._moNameControl, string.Format(LocalizationHolder.rm.GetString(sc_19625.ssp_techcard_19630()), (object) LocalizationHolder.rm.GetString(sc_19625.ssp_techcard_19631())));
    }
    else
    {
      if (this.CreatedObject.ObjectTypeID == -1)
        return;
      IMSApplicability applicability = MetaDataHelper.GetApplicability(TechCardConsts.ObjectTypes.ProcRoutingID, this.CreatedObject.ObjectTypeID, TechCardConsts.RelTypes.TechRelationID);
      if ((applicability == null ? 0 : (applicability.IsContent ? 1 : 0)) == 0)
        return;
      IDBObject dbObject = session.GetObject(this._moObjectId, false);
      if (dbObject == null || dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.CheckoutBy != 0L)
        return;
      this.SetControlErrorMsg(this._moNameControl, string.Format(LocalizationHolder.rm.GetString(sc_19625.ssp_techcard_19632()), (object) dbObject.Caption, (object) dbObject.ObjectID));
    }
  }

  /// <summary>Автоматическое завершение редактирования МО</summary>
  /// <param name="session"></param>
  /// <param name="nea"></param>
  protected void AutoCheckInMo(IUserSession session, List<NotificationEventArgs> nea = null)
  {
    if (!TechCardParamsHelper.TechParams.ProcessRoute.AutoCheckIn)
      return;
    IDBObject objectActualCopy = session.GetObjectActualCopy(this._moObjectId, false);
    if (objectActualCopy == null || objectActualCopy.CheckoutBy != session.UserID)
      return;
    objectActualCopy.CheckIn();
    this._moObjectId = objectActualCopy.ObjectID;
    nea?.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", -this._moObjectId));
  }

  /// <summary>Удаление устаревших связей</summary>
  protected void RemoveStaledLinks()
  {
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
    foreach (ObjectRelationLink objectRelation in this.CreatedObject.ObjectRelationArray)
    {
      if (objectRelation != null && objectRelation.LinkID != 0L)
        objInfoList.Add(new ObjInfoItem(objectRelation.ObjectID));
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objInfoList, sessionKeeper.Session);
      Dictionary<long, int> objectCache = ObjInfoHelper.GetObjectCache((IEnumerable<ObjInfoItem>) objInfoList);
      for (int index = this.CreatedObject.ObjectRelationArray.Count - 1; index >= 0; --index)
      {
        ObjectRelationLink objectRelation = this.CreatedObject.ObjectRelationArray[index];
        int childType;
        if (objectRelation != null && objectRelation.LinkID != 0L && objectRelation.RelationTypeID == TechCardConsts.RelTypes.TechRelationID && objectCache.TryGetValue(objectRelation.ObjectID, out childType) && MetaDataHelper.IsObjectTypeChildOf(childType, TechCardConsts.ObjectTypes.ProcRoutingID) && objectRelation.ObjectID != this._moObjectId)
        {
          sessionKeeper.Session.GetRelation(objectRelation.LinkID, false)?.Delete(0L);
          this.CreatedObject.ObjectRelationArray.RemoveAt(index);
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public TechObjectCreatorProcRouteSupportControl()
  {
    this.InitializeComponent();
    this.InitializeControlData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="creatorExtraParams"></param>
  public TechObjectCreatorProcRouteSupportControl(
    CreatedObjectItem createdObject,
    IObjectCreatorParams creatorExtraParams)
    : base(createdObject, creatorExtraParams)
  {
    this.InitializeComponent();
    this.InitializeControlData();
  }

  /// <summary>Наименование объекта</summary>
  protected string ObjectName
  {
    get => !(this._objNameControl is TextBox objNameControl) ? string.Empty : objNameControl.Text;
    set
    {
      if (!(this._objNameControl is TextBox objNameControl))
        return;
      objNameControl.Text = value;
    }
  }

  /// <summary>Обозначение объекта</summary>
  protected string ObjectDesignation
  {
    get
    {
      return !(this._objDesignationControl is TextBox designationControl) ? string.Empty : designationControl.Text;
    }
    set
    {
      if (!(this._objDesignationControl is TextBox designationControl))
        return;
      designationControl.Text = value;
    }
  }

  /// <summary>Наименование изделия</summary>
  protected string ArtObjectName
  {
    get => !(this._artNameControl is TextBox artNameControl) ? string.Empty : artNameControl.Text;
    set
    {
      if (!(this._artNameControl is TextBox artNameControl))
        return;
      artNameControl.Text = value;
    }
  }

  /// <summary>Наименование маршрута обработки</summary>
  protected string MoObjectName
  {
    get => !(this._moNameControl is TextBox moNameControl) ? string.Empty : moNameControl.Text;
    set
    {
      if (!(this._moNameControl is TextBox moNameControl))
        return;
      moNameControl.Text = value;
    }
  }

  /// <summary>Загрузка параметров объекта</summary>
  /// <param name="dbObject"></param>
  protected override void DoLoadObjectData(IDBObject dbObject)
  {
    IDBAttribute byGuid1 = dbObject.Attributes.FindByGUID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    if (byGuid1 != null)
      this.ObjectName = Convert.ToString(byGuid1.Value);
    IDBAttribute byGuid2 = dbObject.Attributes.FindByGUID(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
    if (byGuid2 != null)
      this.ObjectDesignation = Convert.ToString(byGuid2.Value);
    if (!this.FirstTimeDataLoading)
      return;
    this.UpdateTemplateData(this._prototypeObjId, dbObject.Session, true);
    if (!(this.ObjectDesignation == string.Empty))
      return;
    this.ClassifyObjName(dbObject.Session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObjectId"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  protected override bool CreatedObject_DoCancelCreation(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    this._notificationEvents.Clear();
    if (this._createdMoList.Count > 0)
    {
      foreach (long createdMo in this._createdMoList)
      {
        IDBObject dbObject = session.GetObject(Math.Abs(createdMo), false);
        if (dbObject != null)
        {
          try
          {
            dbObject.Delete(0L);
            nea.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", createdMo));
          }
          catch
          {
          }
        }
      }
      this._createdMoList.Clear();
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.AutoCheckInMo(sessionKeeper.Session, this._notificationEvents);
    return base.CreatedObject_DoCancelCreation(session, newObjectId, nea);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObjectId"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  protected override bool CreatedObject_DoAfterCommitCreation(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    return base.CreatedObject_DoAfterCommitCreation(session, newObjectId, nea);
  }

  protected override bool CreatedObject_DoBeforeCommitCreation(
    IUserSession session,
    IDBObject newObject)
  {
    int num = base.CreatedObject_DoBeforeCommitCreation(session, newObject) ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (!this._createdMoList.Contains(this._moObjectId))
      return num != 0;
    ISelectedItems navigatorSelection = SelectedItemsHelper.GetNavigatorSelection();
    if (navigatorSelection == null)
      return num != 0;
    ProcRouteEntryHelper.CreateProcRouteEntry(session, new ObjInfoItem(this._moObjectId), false, navigatorSelection);
    return num != 0;
  }

  /// <summary>Поиск изделия / МО по списку объектов</summary>
  /// <param name="refObjIDs"></param>
  /// <param name="articleObjId"></param>
  /// <param name="moObjId"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static bool GeRefArtMoData(
    long[] refObjIDs,
    out long articleObjId,
    out long moObjId,
    IUserSession session)
  {
    articleObjId = 0L;
    moObjId = 0L;
    if (refObjIDs == null || refObjIDs.Length == 0)
      return false;
    List<ObjInfoItem> itemInfoList = SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList((IEnumerable<long>) refObjIDs, false);
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) itemInfoList, session);
    List<long> longList = new List<long>();
    ObjInfoItem partObj = itemInfoList.FirstOrDefault<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => MetaDataHelper.IsObjectTypeChildOf(item.ObjTypeID, TechCardConsts.ObjectTypes.ProcRoutingID)));
    if ((TypedInfoItem) partObj != (TypedInfoItem) null)
    {
      moObjId = partObj.ObjectID;
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes).ToArray(), LogicalOperators.NONE, 0, false)
      };
      DataTable parentSostavData = DataHelper.GetParentSostavData(partObj, session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, (IEnumerable<ConditionStructure>) conditions);
      if (parentSostavData != null)
      {
        int idxFldArtObjId = parentSostavData.Columns.IndexOf("F_OBJECT_ID");
        if (idxFldArtObjId != -1)
          longList.AddRange((IEnumerable<long>) parentSostavData.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[idxFldArtObjId]))));
      }
      longList.Sort();
    }
    foreach (ObjInfoItem objInfoItem in itemInfoList)
    {
      IMSApplicability applicability = MetaDataHelper.GetApplicability(objInfoItem.ObjTypeID, TechCardConsts.ObjectTypes.ProcRoutingID, TechCardConsts.RelTypes.TechRelationID);
      if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled && (moObjId == 0L || longList.BinarySearch(objInfoItem.ObjectID) >= 0))
      {
        articleObjId = objInfoItem.ObjectID;
        break;
      }
    }
    if (articleObjId == 0L && longList.Count > 0)
      articleObjId = longList[0];
    return moObjId != 0L || articleObjId != 0L;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="refObjIDs"></param>
  /// <param name="mo2ArticleList"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static bool GeRefArtMoData(
    long[] refObjIDs,
    out List<Tuple<ObjInfoItem, ObjInfoItem>> mo2ArticleList,
    IUserSession session)
  {
    mo2ArticleList = (List<Tuple<ObjInfoItem, ObjInfoItem>>) null;
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (refObjIDs == null || refObjIDs.Length == 0)
      return false;
    List<ObjInfoItem> itemInfoList = SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList((IEnumerable<long>) refObjIDs);
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) itemInfoList, session);
    List<ObjInfoItem> list1 = itemInfoList.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => TechCardConsts.Utils.IsTechcardObjectType((object) item.ObjTypeID))).ToList<ObjInfoItem>();
    if (list1.Count == 0)
      return false;
    DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) list1, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, true, new DBRecordSetParams());
    if (parentSostavData == null || parentSostavData.Rows.Count == 0)
      return false;
    int idxFldObjectId = parentSostavData.Columns.IndexOf("F_OBJECT_ID");
    int idxFldObjTypeId = parentSostavData.Columns.IndexOf("F_OBJECT_TYPE");
    int columnIndex = parentSostavData.Columns.IndexOf(DataHelper.Consts.cnt_fld_PartObjID);
    List<ObjInfoItem> list2 = parentSostavData.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(row[idxFldObjTypeId]), TechCardConsts.ObjectTypes.ProcRoutingID))).Select<DataRow, ObjInfoItem>((System.Func<DataRow, ObjInfoItem>) (row => new ObjInfoItem(Convert.ToInt64(row[idxFldObjectId]), Convert.ToInt32(row[idxFldObjTypeId])))).ToList<ObjInfoItem>();
    SomeTypedInfoHelper<ObjInfoItem>.RemoveDuplicateEmpty(list2);
    if (list2.Count == 0)
      return false;
    mo2ArticleList = new List<Tuple<ObjInfoItem, ObjInfoItem>>();
    ObjInfoItem objInfoItem = new ObjInfoItem();
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
    foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
    {
      objInfoItem.ObjectID = Convert.ToInt64(row[columnIndex]);
      int index = list2.BinarySearch(objInfoItem);
      if (index >= 0)
      {
        int int32 = Convert.ToInt32(row[idxFldObjTypeId]);
        if (childrenIdRecursive.Contains(int32))
          mo2ArticleList.Add(new Tuple<ObjInfoItem, ObjInfoItem>(list2[index], new ObjInfoItem(Convert.ToInt64(row[idxFldObjectId]), int32)));
      }
    }
    if (mo2ArticleList.Count == 0)
      mo2ArticleList = (List<Tuple<ObjInfoItem, ObjInfoItem>>) null;
    return mo2ArticleList != null;
  }

  public static IList<ObjInfoItem> GetArticleObjInfoList(
    IEnumerable<ObjInfoItem> articleObjInfoItems,
    int objTypeId,
    IUserSession session)
  {
    List<ObjInfoItem> articleObjInfoList = new List<ObjInfoItem>();
    if (articleObjInfoItems == null)
      return (IList<ObjInfoItem>) articleObjInfoList;
    DataTable childSostavData = DataHelper.GetChildSostavData(articleObjInfoItems, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, 2, new DBRecordSetParams(), (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objTypeId));
    if (childSostavData == null)
      return (IList<ObjInfoItem>) articleObjInfoList;
    int idxFldObjectId = childSostavData.Columns.IndexOf("F_OBJECT_ID");
    int idxFldObjTypeId = childSostavData.Columns.IndexOf("F_OBJECT_TYPE");
    articleObjInfoList.AddRange((IEnumerable<ObjInfoItem>) childSostavData.AsEnumerable().Select<DataRow, ObjInfoItem>((System.Func<DataRow, ObjInfoItem>) (row => new ObjInfoItem(Convert.ToInt64(row[idxFldObjectId]), Convert.ToInt32(row[idxFldObjTypeId])))).ToArray<ObjInfoItem>());
    return (IList<ObjInfoItem>) articleObjInfoList;
  }

  /// <summary>
  /// Получение объектов указанного типа для изделия-прототипа
  /// </summary>
  /// <param name="articleObjId">Ид. исходного изделия</param>
  /// <param name="objTypeId">Ид. типа искомых объектов</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static List<ObjInfoItem> GetPrototypeObjInfoList(
    long articleObjId,
    int objTypeId,
    IUserSession session)
  {
    List<ObjInfoItem> prototypeObjInfoList = new List<ObjInfoItem>();
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (articleObjId == 0L || articleObjId == -1L || objTypeId == -1)
      return prototypeObjInfoList;
    IDBObject objectActualCopy = session.GetObjectActualCopy(articleObjId, false);
    if (objectActualCopy == null)
      return prototypeObjInfoList;
    IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cadd9668-306c-11d8-b4e9-00304f19f545"), false);
    long asInteger = attributeByGuid == null || attributeByGuid.Value == DBNull.Value ? 0L : attributeByGuid.AsInteger;
    if (asInteger == 0L)
      return prototypeObjInfoList;
    prototypeObjInfoList.AddRange((IEnumerable<ObjInfoItem>) TechObjectCreatorProcRouteSupportControl.GetArticleObjInfoList((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      new ObjInfoItem(asInteger)
    }, objTypeId, session));
    return prototypeObjInfoList;
  }

  /// <summary>
  /// Получение объектов указанного типа для всех исполнений изделий
  /// </summary>
  /// <param name="articleObjId">Ид. исходного изделия</param>
  /// <param name="objTypeId">Ид. типа искомых объектов</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static List<ObjInfoItem> GetArticleGroupObjInfoList(
    long articleObjId,
    int objTypeId,
    IUserSession session)
  {
    List<ObjInfoItem> groupObjInfoList = new List<ObjInfoItem>();
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (articleObjId == 0L || articleObjId == -1L || objTypeId == -1)
      return groupObjInfoList;
    IDBObject objectActualCopy = session.GetObjectActualCopy(articleObjId, false);
    if (objectActualCopy == null)
      return groupObjInfoList;
    IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), false);
    string conditionValue = attributeByGuid != null ? Convert.ToString(attributeByGuid.Value) : string.Empty;
    if (conditionValue == string.Empty)
      return groupObjInfoList;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    };
    DBRecordSetParams dbRsp = new DBRecordSetParams(new List<ConditionStructure>()
    {
      new ConditionStructure(MetaDataHelper.GetAttributeID((object) "cad001f9-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue, (object) null, LogicalOperators.NONE, 0, false)
    }.ToArray(), columnDescriptorList.ToArray());
    DataTable objectData = DataHelper.GetObjectData((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes), session, dbRsp, (IEnumerable<long>) null);
    if (objectData == null || objectData.Rows.Count == 0)
      return groupObjInfoList;
    groupObjInfoList.AddRange((IEnumerable<ObjInfoItem>) TechObjectCreatorProcRouteSupportControl.GetArticleObjInfoList((IEnumerable<ObjInfoItem>) objectData.AsEnumerable().Select<DataRow, ObjInfoIDItem>((System.Func<DataRow, ObjInfoIDItem>) (row => new ObjInfoIDItem(Convert.ToInt64(row[0]), Convert.ToInt32(row[1])))).ToList<ObjInfoIDItem>(), objTypeId, session));
    return groupObjInfoList;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}
