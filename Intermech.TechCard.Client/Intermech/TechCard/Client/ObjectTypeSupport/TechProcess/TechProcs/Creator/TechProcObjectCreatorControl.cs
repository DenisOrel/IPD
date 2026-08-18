// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcs.Creator.TechProcObjectCreatorControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.ObjectCreator;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Localization;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.TcObjectsTypes;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;
using Intermech.TechCard.Client.TcObjectsTypes.Process_Route;
using Intermech.TechCard.Client.UI.Controls;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcs.Creator;

/// <summary>
/// 
/// </summary>
internal class TechProcObjectCreatorControl : TechObjectCreatorProcRouteSupportControl
{
  /// <summary>идентификатор расцеховочного маршрута</summary>
  private long _routeObjectId;
  /// <summary>идентификаторы расцеховочных элементов</summary>
  private RouteElemClassList _routeElemNodes;
  /// <summary>
  /// Список объектов, требующих завершения создания при завершении создания ТП
  /// </summary>
  private readonly List<long> _dbObjectId2CommitList = new List<long>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox grbArticle;
  private TextBox tbxRouteCaption;
  private Button btnRouteSelect;
  private Label lblRoute;
  private Button btnArticle;
  private TextBox tbxArtName;
  private Label lblArticle;
  private TextBox tbxMoCaption;
  private Button btnMoSelect;
  private Label lblMO;
  private GroupBox grbTechProc;
  private Button btnPrototype;
  private TextBox tbxPrototype;
  private Label lblTpProt;
  private ComboBox cbProduction;
  private Label lblTpProd;
  private TextBox tbxTpName;
  private Label lblTpName;
  private TextBox tbxTpDesignation;
  private Label lblTpDesign;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControlData()
  {
    this._objNameControl = (Control) this.tbxTpName;
    this._objDesignationControl = (Control) this.tbxTpDesignation;
    this._artNameControl = (Control) this.tbxArtName;
    this._moNameControl = (Control) this.tbxMoCaption;
    if (this.CreatedObject != null && this.CreatedObject.ObjectTypeID == TechCardConsts.ObjectTypes.TechProcEdinID)
      this._helpTopicId = 1425;
    this.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.LoadContextObjectData();
    this.UpdateCustomControls();
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateCustomControls()
  {
    int num = this.CreatedObject != null ? this.CreatedObject.ObjectTypeID : -1;
    bool flag = num == TechCardConsts.ObjectTypes.TechProcEdinID;
    this.grbArticle.Enabled = num == TechCardConsts.ObjectTypes.TechProcEdinID;
    this.tbxPrototype.Enabled = this.btnPrototype.Enabled = true;
    this.tbxRouteCaption.Enabled = this.btnRouteSelect.Enabled = flag;
    this.btnRouteSelect.Enabled = this._moObjectId != 0L | flag;
  }

  /// <summary>Выбор прототипа для ТП</summary>
  protected override void SelectPrototype()
  {
    if (this.CreatedObject == null || this.CreatedObject.ObjectTypeID == -1)
    {
      string caption = LocalizationHolder.rm.GetString("TechCard.Client_213");
      int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_19672.ssp_techcard_19673()), (object) LocalizationHolder.rm.GetString("TechCard.Client_402")), caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
      base.SelectPrototype();
  }

  /// <summary>Выбор РМ</summary>
  private void SelectRoute()
  {
    if (this._moObjectId == 0L)
      return;
    long moObjectId = this._moObjectId;
    long routeObjectId = this._routeObjectId;
    RouteElemClassList routeElemNodes = new RouteElemClassList();
    if (this._routeElemNodes != null)
    {
      foreach (RouteElemClass routeElemNode in (List<RouteElemClass>) this._routeElemNodes)
        routeElemNodes.Add(routeElemNode);
    }
    if (!CehRoutesElemsListDlg.ShowDialog(this._articleId, this.ProductionId, ref moObjectId, ref routeObjectId, ref routeElemNodes))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.UpdateMoData(moObjectId, sessionKeeper.Session);
      this.UpdateRouteData(routeObjectId, sessionKeeper.Session);
      this._routeElemNodes = routeElemNodes;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void ValidateProdData(IUserSession session)
  {
    this.SetControlErrorMsg((Control) this.cbProduction, string.Empty);
    if (this.ProductionId != 0L)
      return;
    this.SetControlErrorMsg((Control) this.cbProduction, string.Format(LocalizationHolder.rm.GetString(sc_19672.ssp_techcard_19674()), (object) LocalizationHolder.rm.GetString(sc_19672.ssp_techcard_19675())));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void ValidateRouteData(IUserSession session)
  {
  }

  /// <summary>Обновление значения прототипа объекта</summary>
  /// <param name="templateObjId"></param>
  /// <param name="session"></param>
  /// <param name="forceMode">Режим принудительного обновления</param>
  protected override bool UpdateTemplateData(
    long templateObjId,
    IUserSession session,
    bool forceMode)
  {
    if (!base.UpdateTemplateData(templateObjId, session, forceMode))
      return false;
    this.tbxPrototype.Text = TechCardConsts.Utils.GetObjectString(this._prototypeObjId, session);
    return true;
  }

  /// <summary>Обновление значения РМ</summary>
  /// <param name="routeObjId"></param>
  /// <param name="session"></param>
  private void UpdateRouteData(long routeObjId, IUserSession session)
  {
    try
    {
      if (this._routeObjectId == routeObjId)
        return;
      this._routeObjectId = routeObjId;
      this.tbxRouteCaption.Text = TechCardConsts.Utils.GetObjectString(this._routeObjectId, session);
    }
    finally
    {
      this.ValidateRouteData(session);
    }
  }

  /// <summary>Загрузка настроек</summary>
  /// <param name="productId"></param>
  private void LoadSettings(ref long productId)
  {
    HybridDictionary config = new HybridDictionary(2);
    TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.LocationOnly, (IDictionary) config);
    if (productId != 0L || !config.Contains((object) "prodID"))
      return;
    productId = (long) config[(object) "prodID"];
  }

  /// <summary>Сохранение настроек</summary>
  private void SaveSettings()
  {
    TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.LocationOnly, (IDictionary) new HybridDictionary(1)
    {
      {
        (object) "prodID",
        (object) this.ProductionId
      }
    });
  }

  /// <summary>
  /// 
  /// </summary>
  public TechProcObjectCreatorControl() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="creatorExtraParams"></param>
  public TechProcObjectCreatorControl(
    CreatedObjectItem createdObject,
    IObjectCreatorParams creatorExtraParams)
    : base(createdObject, creatorExtraParams)
  {
    this.InitializeComponent();
    this.InitializeControlData();
  }

  /// <summary>Выбор МО</summary>
  protected override void SelectProcRoute()
  {
    if (this._articleId != 0L)
    {
      long moObjectId = this._moObjectId;
      if (!ProcRouteListViewDlg.ShowDialog(this._articleId, 0L, ref moObjectId) || moObjectId == this._moObjectId)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.UpdateMoData(moObjectId, sessionKeeper.Session);
    }
    else
    {
      long num = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.ProcRoutingGUID, LocalizationHolder.rm.GetString("TechCard.Client_277"));
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
  /// <param name="session"></param>
  /// <returns></returns>
  protected override void ClassifyObjName(IUserSession session)
  {
    if (this._articleId == 0L)
      this.ValidateObjData();
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(this.CreatedObject.ObjectID, this.CreatedObject.ObjectTypeID);
    ObjInfoItem contextObjectItem = new ObjInfoItem(this._articleId);
    IEnumerable<RelObjInfoItem> relObjInfoItems;
    IEnumerable<ObjInfoItem> objInfoItems = !(this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams) || !TechcardClientControlsUtils.GetItemsApplicabilityInfo(creatorExtraParams.Items, (System.IServiceProvider) ApplicationServices.Container, out relObjInfoItems) ? (IEnumerable<ObjInfoItem>) null : relObjInfoItems.Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo));
    ITechCardClassifyObjectService classifyObjectService1 = service;
    IUserSession session1 = session;
    TechCardClassifyObjectAttributeParams objectAttributeParams = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem);
    objectAttributeParams.AttributeValues = (IEnumerable<AttributeValues>) new AttributeValues[1]
    {
      new AttributeValues(TechCardConsts.AttributeTypes.ProductionAttrID, (object) this.ProductionId)
    };
    objectAttributeParams.ExtraContextObjInfoItems = objInfoItems;
    TechCardClassifyObjectAttributeParams classifyParams1 = objectAttributeParams;
    TechCardClassifyTechProcessDesignationStrategy classifyStrategy1 = new TechCardClassifyTechProcessDesignationStrategy();
    string str1;
    ref string local1 = ref str1;
    if (classifyObjectService1.ClassifyObjectAttribute(session1, classifyParams1, (ITechCardClassifyObjectStrategy) classifyStrategy1, out local1))
      this.ObjectDesignation = str1;
    ITechCardClassifyObjectService classifyObjectService2 = service;
    IUserSession session2 = session;
    TechCardClassifyObjectAttributeParams classifyParams2 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, contextObjectItem);
    classifyParams2.ExtraContextObjInfoItems = objInfoItems;
    TechCardClassifyObjectNameStrategy classifyStrategy2 = new TechCardClassifyObjectNameStrategy();
    string str2;
    ref string local2 = ref str2;
    if (!classifyObjectService2.ClassifyObjectAttribute(session2, classifyParams2, (ITechCardClassifyObjectStrategy) classifyStrategy2, out local2))
      return;
    this.ObjectName = str2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void ValidateArtData(IUserSession session)
  {
    if (this.CreatedObject == null || (MetaDataHelper.IsObjectTypeChildOf(this.CreatedObject.ObjectTypeID, TechCardConsts.ObjectTypes.TechProcGroupID) ? 1 : (MetaDataHelper.IsObjectTypeChildOf(this.CreatedObject.ObjectTypeID, TechCardConsts.ObjectTypes.TechProcTipovID) ? 1 : 0)) != 0)
      return;
    base.ValidateArtData(session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void ValidateMoData(IUserSession session)
  {
    if (this.CreatedObject == null)
      return;
    this.UpdateCustomControls();
    bool flag = this.CreatedObject.ObjectTypeID == TechCardConsts.ObjectTypes.TechProcEdinID;
    if (this._moObjectId == 0L && !flag)
      return;
    base.ValidateMoData(session);
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoLoadObjectData(IDBObject dbObject)
  {
    if (this.FirstTimeDataLoading)
    {
      long defaultProductionId = 0;
      this.LoadSettings(ref defaultProductionId);
      List<TechProduction> techProductions = TechCardClientConst.GetTechProductions(dbObject.Session, true);
      TechObjectCreatorBaseControl.FillComboBoxList(this.cbProduction, (IEnumerable<object>) techProductions, (object) techProductions.FirstOrDefault<TechProduction>((Func<TechProduction, bool>) (item => item.ID == defaultProductionId)));
    }
    IDBAttribute byGuid = dbObject.Attributes.FindByGUID(TechCardConsts.AttributeTypes.ProductionAttrGUID);
    if (byGuid != null)
    {
      long currentProductionId = byGuid.AsInteger;
      if (currentProductionId != 0L)
        this.cbProduction.SelectedIndex = this.cbProduction.Items.IndexOfFirst((Predicate<object>) (item => ((TechProduction) item).ID == currentProductionId));
    }
    base.DoLoadObjectData(dbObject);
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoSaveObjectData(IDBObject dbObject)
  {
    this.CreateObject_CopyPrototypeAttributes(dbObject);
    AttributeValues[] valuesList = new AttributeValues[3]
    {
      new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) this.tbxTpName.Text),
      new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) this.tbxTpDesignation.Text),
      new AttributeValues(TechCardConsts.AttributeTypes.ProductionAttrID, this.cbProduction.SelectedItem is TechProduction selectedItem ? (object) selectedItem.ID : (object) DBNull.Value)
    };
    dbObject.SetAttributesValues(valuesList);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObject"></param>
  /// <returns></returns>
  protected override bool CreatedObject_DoBeforeCommitCreation(
    IUserSession session,
    IDBObject newObject)
  {
    if (!base.CreatedObject_DoBeforeCommitCreation(session, newObject))
      return false;
    ICompositionsAutomaticSortingService service = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) session, true);
    service?.CreateSession((object) session.SessionGUID);
    long objectId = newObject.ObjectID;
    try
    {
      this.CreateObject_CopyPrototypeComposition(session);
      if ((session.GetRelation(objectId, this._moObjectId, TechCardConsts.RelTypes.TechRelationID, true) ?? session.GetRelation(this._moObjectId, objectId, TechCardConsts.RelTypes.TechRelationID, true)) == null)
      {
        TechcardClientUtils.StartCreateRelations(this._moObjectId, session);
        try
        {
          List<IDBRelation> relations = TechcardClientUtils.CreateRelations(session, objectId, new int[1]
          {
            TechCardConsts.RelTypes.TechRelationID
          }, new long[1]{ this._moObjectId }, DateTime.Now, TechCreateRelMode.tcrmEnterIn);
          if (relations.Count > 0)
            this._notificationEvents.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relations.Select<IDBRelation, long>((Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) relations.Select<IDBRelation, long>((Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) relations.Select<IDBRelation, int>((Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
        }
        finally
        {
          TechcardClientUtils.StopCreateRelations(session);
        }
      }
      this.CreateRouteObjectLinkData(session, newObject);
      this.AutoCheckInMo(session, this._notificationEvents);
    }
    finally
    {
      service?.DisposeSession((object) session.SessionGUID);
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObject"></param>
  private void CreateRouteObjectLinkData(IUserSession session, IDBObject newObject)
  {
    if (newObject == null)
      throw new ArgumentNullException(nameof (newObject));
    bool flag = newObject.ObjectType == TechCardConsts.ObjectTypes.TechProcEdinID && (this._prototypeObjId == 0L || this._prototypeObjId == -1L);
    if (this._routeObjectId == -1L || !flag)
      return;
    List<IDBRelation> source = new List<IDBRelation>();
    TechcardClientUtils.StartCreateRelations(this._routeObjectId, session);
    try
    {
      source.AddRange((IEnumerable<IDBRelation>) TechcardClientUtils.CreateRelations(session, newObject.ObjectID, new int[1]
      {
        TechCardConsts.RelTypes.TechRouteRelationID
      }, new long[1]{ this._routeObjectId }, DateTime.Now, TechCreateRelMode.tcrmBothEnterInFirst));
    }
    finally
    {
      TechcardClientUtils.StopCreateRelations(session);
    }
    if (this._routeElemNodes != null && this._routeElemNodes.Count > 0)
    {
      List<int> intList1 = new List<int>();
      IDBAttributesGroup attributesGroup = session.GetAttributesGroup(TechCardConsts.AttributeTypes.TechcardAttrGroupGuid);
      foreach (IMSAttribute4ObjectType attribute4ObjectType in MetaDataHelper.GetAttribute4ObjectTypeList(TechCardConsts.ObjectTypes.CehZahodObjectGUID))
      {
        if (attribute4ObjectType != null && MetaDataHelper.GetAttribute4ObjectType(TechCardConsts.ObjectTypes.ElemRouteID, attribute4ObjectType.AttributeID) != null)
        {
          if ((attribute4ObjectType.AttributeID == TechCardConsts.AttributeTypes.NameAttrTypeID ? 0 : (attribute4ObjectType.AttributeID != TechCardConsts.AttributeTypes.DesignationAttrTypeID ? 1 : 0)) != 0)
          {
            IDBAttributeType attributeType = session.GetAttributeType(attribute4ObjectType.AttributeID);
            if (attributeType != null)
            {
              List<int> intList2 = attributeType.GetGroupsList() != null ? new List<int>((IEnumerable<int>) attributeType.GetGroupsList()) : (List<int>) null;
              if (intList2 == null || !intList2.Contains(attributesGroup.GroupID))
                continue;
            }
            else
              continue;
          }
          intList1.Add(attribute4ObjectType.AttributeID);
        }
      }
      TechcardClientUtils.StartCreateRelations(newObject.ObjectID, session);
      try
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection(TechCardConsts.ObjectTypes.CehZahodObjectGUID);
        foreach (RouteElemClass routeElemNode in (List<RouteElemClass>) this._routeElemNodes)
        {
          IDBObject projDbObject = session.GetObject(routeElemNode.ObjID);
          if (projDbObject != null)
          {
            AttributeValues[] attributesValues = projDbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes);
            IDBObject dbObject = objectCollection.Create();
            List<AttributeValues> attributeValuesList = new List<AttributeValues>(intList1.Count);
            foreach (AttributeValues attributeValues in attributesValues)
            {
              int attributeId = attributeValues.AttributeID;
              if (intList1.Contains(attributeId))
                attributeValuesList.Add(attributeValues);
            }
            dbObject.SetAttributesValues(attributeValuesList.ToArray());
            IDBRelation relation1 = TechcardClientUtils.CreateRelation(TechCardConsts.RelTypes.TechRelationID, session, newObject, dbObject);
            if (relation1 != null)
              source.Add(relation1);
            List<IDBRelation> dbRelationList = new List<IDBRelation>();
            IDBRelation relation2 = TechcardClientUtils.CreateRelation(TechCardConsts.RelTypes.TechRouteRelationID, session, projDbObject, dbObject);
            if (relation2 != null)
              dbRelationList.Add(relation2);
            if (dbRelationList.Count > 0)
            {
              AttributeValues[] valuesList = new AttributeValues[1];
              IDBRelation relation3 = session.GetRelation(routeElemNode.LinkID);
              int attributeTypeId = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ElemRouteLinkAttrGuid);
              valuesList[0] = new AttributeValues(attributeTypeId, (object) ((IDBGuid) relation3).GUID);
              IDBRelation dbRelation = dbRelationList[0];
              if (dbRelation != null)
              {
                dbRelation.SetAttributesValues(valuesList);
                source.Add(dbRelation);
              }
            }
            this._dbObjectId2CommitList.Add(dbObject.ObjectID);
            long relationId = 0;
            if (relation1 != null)
              relationId = relation1.RelationID;
            this._relObjInfo4AutoSelect.Add(new RelObjInfoItem(relationId)
            {
              PartInfo = new ObjInfoItem(dbObject)
            });
          }
        }
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(session);
      }
    }
    if (source.Count <= 0)
      return;
    this._notificationEvents.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<IDBRelation, long>((Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source.Select<IDBRelation, long>((Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source.Select<IDBRelation, int>((Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
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
    this.SaveSettings();
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
    if (this._dbObjectId2CommitList.Count != 0)
    {
      foreach (long dbObjectId2Commit in this._dbObjectId2CommitList)
      {
        IDBObject dbObject = session.GetObject(dbObjectId2Commit, false);
        if (dbObject != null && dbObject.IsCreationMode)
          dbObject.CommitCreation(true, true);
      }
    }
    this.SaveSettings();
    return base.CreatedObject_DoAfterCommitCreation(session, newObjectId, nea);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnPrototype_Click(object sender, EventArgs e) => this.SelectPrototype();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnArticle_Click(object sender, EventArgs e) => this.SelectArticle();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnMoSelect_Click(object sender, EventArgs e) => this.SelectProcRoute();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnRouteSelect_Click(object sender, EventArgs e) => this.SelectRoute();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxTpDesignation_TextChanged(object sender, EventArgs e) => this.ValidateObjData();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbProd_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._articleId == 0L || this.FirstTimeDataLoading)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.ClassifyObjName(sessionKeeper.Session);
      this.ValidateProdData(sessionKeeper.Session);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxPrototype_KeyDown(object sender, KeyEventArgs e)
  {
    if ((e.KeyCode & Keys.Delete) == Keys.None && (e.KeyCode & Keys.Back) == Keys.None)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateTemplateData(0L, sessionKeeper.Session, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxRouteCaption_KeyDown(object sender, KeyEventArgs e)
  {
    if ((e.KeyCode & Keys.Delete) == Keys.None && (e.KeyCode & Keys.Back) == Keys.None)
      return;
    this._routeElemNodes = (RouteElemClassList) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateRouteData(0L, sessionKeeper.Session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxMoCaption_KeyDown(object sender, KeyEventArgs e)
  {
    if ((e.KeyCode & Keys.Delete) == Keys.None && (e.KeyCode & Keys.Back) == Keys.None)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateMoData(0L, sessionKeeper.Session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxArtName_KeyDown(object sender, KeyEventArgs e)
  {
    if ((e.KeyCode & Keys.Delete) == Keys.None && (e.KeyCode & Keys.Back) == Keys.None)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateArticleData(0L, true, true, sessionKeeper.Session);
  }

  /// <summary>Ид. вида производства</summary>
  private long ProductionId
  {
    get
    {
      return this.cbProduction.SelectedItem == null ? 0L : ((TechProduction) this.cbProduction.SelectedItem).ID;
    }
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
    this.grbArticle = new GroupBox();
    this.tbxRouteCaption = new TextBox();
    this.btnRouteSelect = new Button();
    this.lblRoute = new Label();
    this.btnArticle = new Button();
    this.tbxArtName = new TextBox();
    this.lblArticle = new Label();
    this.tbxMoCaption = new TextBox();
    this.btnMoSelect = new Button();
    this.lblMO = new Label();
    this.grbTechProc = new GroupBox();
    this.btnPrototype = new Button();
    this.tbxPrototype = new TextBox();
    this.lblTpProt = new Label();
    this.cbProduction = new ComboBox();
    this.lblTpProd = new Label();
    this.tbxTpName = new TextBox();
    this.lblTpName = new Label();
    this.tbxTpDesignation = new TextBox();
    this.lblTpDesign = new Label();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.grbArticle.SuspendLayout();
    this.grbTechProc.SuspendLayout();
    this.SuspendLayout();
    this.grbArticle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grbArticle.Controls.Add((Control) this.tbxRouteCaption);
    this.grbArticle.Controls.Add((Control) this.btnRouteSelect);
    this.grbArticle.Controls.Add((Control) this.lblRoute);
    this.grbArticle.Controls.Add((Control) this.btnArticle);
    this.grbArticle.Controls.Add((Control) this.tbxArtName);
    this.grbArticle.Controls.Add((Control) this.lblArticle);
    this.grbArticle.Controls.Add((Control) this.tbxMoCaption);
    this.grbArticle.Controls.Add((Control) this.btnMoSelect);
    this.grbArticle.Controls.Add((Control) this.lblMO);
    this.grbArticle.Location = new Point(9, 145);
    this.grbArticle.Name = "grbArticle";
    this.grbArticle.Size = new Size(463, 101);
    this.grbArticle.TabIndex = 4;
    this.grbArticle.TabStop = false;
    this.grbArticle.Text = "Привязка к изделию";
    this.tbxRouteCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxRouteCaption.BackColor = SystemColors.Window;
    this.tbxRouteCaption.Location = new Point((int) sbyte.MaxValue, 71);
    this.tbxRouteCaption.Name = "tbxRouteCaption";
    this.tbxRouteCaption.ReadOnly = true;
    this.tbxRouteCaption.Size = new Size(299, 20);
    this.tbxRouteCaption.TabIndex = 5;
    this.tbxRouteCaption.KeyDown += new KeyEventHandler(this.tbxRouteCaption_KeyDown);
    this.btnRouteSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnRouteSelect.FlatStyle = FlatStyle.System;
    this.btnRouteSelect.ImeMode = ImeMode.NoControl;
    this.btnRouteSelect.Location = new Point(428, 69);
    this.btnRouteSelect.Name = "btnRouteSelect";
    this.btnRouteSelect.Size = new Size(24, 23);
    this.btnRouteSelect.TabIndex = 6;
    this.btnRouteSelect.Text = "...";
    this.btnRouteSelect.Click += new EventHandler(this.btnRouteSelect_Click);
    this.lblRoute.ImeMode = ImeMode.NoControl;
    this.lblRoute.Location = new Point(6, 74);
    this.lblRoute.Name = "lblRoute";
    this.lblRoute.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblRoute.TabIndex = 21;
    this.lblRoute.Text = "Расцеховка";
    this.btnArticle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnArticle.FlatStyle = FlatStyle.System;
    this.btnArticle.ImeMode = ImeMode.NoControl;
    this.btnArticle.Location = new Point(428, 17);
    this.btnArticle.Name = "btnArticle";
    this.btnArticle.Size = new Size(24, 23);
    this.btnArticle.TabIndex = 2;
    this.btnArticle.Text = "...";
    this.btnArticle.Click += new EventHandler(this.btnArticle_Click);
    this.tbxArtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxArtName.BackColor = SystemColors.Window;
    this.tbxArtName.ForeColor = SystemColors.WindowText;
    this.tbxArtName.Location = new Point((int) sbyte.MaxValue, 19);
    this.tbxArtName.Name = "tbxArtName";
    this.tbxArtName.ReadOnly = true;
    this.tbxArtName.Size = new Size(299, 20);
    this.tbxArtName.TabIndex = 1;
    this.tbxArtName.KeyDown += new KeyEventHandler(this.tbxArtName_KeyDown);
    this.lblArticle.ImeMode = ImeMode.NoControl;
    this.lblArticle.Location = new Point(6, 22);
    this.lblArticle.Name = "lblArticle";
    this.lblArticle.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblArticle.TabIndex = 13;
    this.lblArticle.Text = "Изделие";
    this.tbxMoCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxMoCaption.BackColor = SystemColors.Window;
    this.tbxMoCaption.Location = new Point((int) sbyte.MaxValue, 45);
    this.tbxMoCaption.Name = "tbxMoCaption";
    this.tbxMoCaption.ReadOnly = true;
    this.tbxMoCaption.Size = new Size(299, 20);
    this.tbxMoCaption.TabIndex = 3;
    this.tbxMoCaption.KeyDown += new KeyEventHandler(this.tbxMoCaption_KeyDown);
    this.btnMoSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnMoSelect.FlatStyle = FlatStyle.System;
    this.btnMoSelect.ImeMode = ImeMode.NoControl;
    this.btnMoSelect.Location = new Point(428, 43);
    this.btnMoSelect.Name = "btnMoSelect";
    this.btnMoSelect.Size = new Size(24, 23);
    this.btnMoSelect.TabIndex = 4;
    this.btnMoSelect.Text = "...";
    this.btnMoSelect.Click += new EventHandler(this.btnMoSelect_Click);
    this.lblMO.ImeMode = ImeMode.NoControl;
    this.lblMO.Location = new Point(6, 48 /*0x30*/);
    this.lblMO.Name = "lblMO";
    this.lblMO.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblMO.TabIndex = 7;
    this.lblMO.Text = "Маршрут обработки";
    this.grbTechProc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grbTechProc.Controls.Add((Control) this.btnPrototype);
    this.grbTechProc.Controls.Add((Control) this.tbxPrototype);
    this.grbTechProc.Controls.Add((Control) this.lblTpProt);
    this.grbTechProc.Controls.Add((Control) this.cbProduction);
    this.grbTechProc.Controls.Add((Control) this.lblTpProd);
    this.grbTechProc.Controls.Add((Control) this.tbxTpName);
    this.grbTechProc.Controls.Add((Control) this.lblTpName);
    this.grbTechProc.Controls.Add((Control) this.tbxTpDesignation);
    this.grbTechProc.Controls.Add((Control) this.lblTpDesign);
    this.grbTechProc.Location = new Point(8, 10);
    this.grbTechProc.Name = "grbTechProc";
    this.grbTechProc.Size = new Size(464, 129);
    this.grbTechProc.TabIndex = 3;
    this.grbTechProc.TabStop = false;
    this.grbTechProc.Text = "Техпроцесс";
    this.btnPrototype.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnPrototype.FlatStyle = FlatStyle.System;
    this.btnPrototype.ImeMode = ImeMode.NoControl;
    this.btnPrototype.Location = new Point(430, 96 /*0x60*/);
    this.btnPrototype.Name = "btnPrototype";
    this.btnPrototype.Size = new Size(24, 23);
    this.btnPrototype.TabIndex = 17;
    this.btnPrototype.Text = "...";
    this.btnPrototype.Click += new EventHandler(this.btnPrototype_Click);
    this.tbxPrototype.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxPrototype.BackColor = SystemColors.Window;
    this.tbxPrototype.Location = new Point(128 /*0x80*/, 98);
    this.tbxPrototype.Name = "tbxPrototype";
    this.tbxPrototype.ReadOnly = true;
    this.tbxPrototype.Size = new Size(300, 20);
    this.tbxPrototype.TabIndex = 16 /*0x10*/;
    this.tbxPrototype.KeyDown += new KeyEventHandler(this.tbxPrototype_KeyDown);
    this.lblTpProt.ImeMode = ImeMode.NoControl;
    this.lblTpProt.Location = new Point(7, 101);
    this.lblTpProt.Name = "lblTpProt";
    this.lblTpProt.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblTpProt.TabIndex = 15;
    this.lblTpProt.Text = "Прототип";
    this.cbProduction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbProduction.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.cbProduction.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.cbProduction.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbProduction.Location = new Point((int) sbyte.MaxValue, 71);
    this.cbProduction.Name = "cbProduction";
    this.cbProduction.Size = new Size(326, 21);
    this.cbProduction.Sorted = true;
    this.cbProduction.TabIndex = 12;
    this.cbProduction.SelectedIndexChanged += new EventHandler(this.cbProd_SelectedIndexChanged);
    this.lblTpProd.ImeMode = ImeMode.NoControl;
    this.lblTpProd.Location = new Point(6, 76);
    this.lblTpProd.Name = "lblTpProd";
    this.lblTpProd.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblTpProd.TabIndex = 11;
    this.lblTpProd.Text = "Вид производства";
    this.tbxTpName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxTpName.Location = new Point((int) sbyte.MaxValue, 45);
    this.tbxTpName.Name = "tbxTpName";
    this.tbxTpName.Size = new Size(326, 20);
    this.tbxTpName.TabIndex = 8;
    this.lblTpName.ImeMode = ImeMode.NoControl;
    this.lblTpName.Location = new Point(6, 49);
    this.lblTpName.Name = "lblTpName";
    this.lblTpName.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblTpName.TabIndex = 9;
    this.lblTpName.Text = "Наименование";
    this.tbxTpDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxTpDesignation.Location = new Point((int) sbyte.MaxValue, 19);
    this.tbxTpDesignation.Name = "tbxTpDesignation";
    this.tbxTpDesignation.Size = new Size(326, 20);
    this.tbxTpDesignation.TabIndex = 6;
    this.tbxTpDesignation.TextChanged += new EventHandler(this.tbxTpDesignation_TextChanged);
    this.lblTpDesign.ImeMode = ImeMode.NoControl;
    this.lblTpDesign.Location = new Point(6, 23);
    this.lblTpDesign.Name = "lblTpDesign";
    this.lblTpDesign.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblTpDesign.TabIndex = 7;
    this.lblTpDesign.Text = "Обозначение";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.grbArticle);
    this.Controls.Add((Control) this.grbTechProc);
    this.MinimumSize = new Size(478, 258);
    this.Name = nameof (TechProcObjectCreatorControl);
    this.Size = new Size(478, 258);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.grbArticle.ResumeLayout(false);
    this.grbArticle.PerformLayout();
    this.grbTechProc.ResumeLayout(false);
    this.grbTechProc.PerformLayout();
    this.ResumeLayout(false);
  }
}
