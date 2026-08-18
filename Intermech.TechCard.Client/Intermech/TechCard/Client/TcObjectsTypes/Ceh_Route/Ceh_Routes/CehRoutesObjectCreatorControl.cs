// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes.CehRoutesObjectCreatorControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.ObjectCreator;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Ceh_Route;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;

/// <summary>
/// 
/// </summary>
internal class CehRoutesObjectCreatorControl : TechObjectCreatorProcRouteSupportControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox grbArticle;
  private Button btnArticle;
  private TextBox tbxArtName;
  private Label label3;
  private TextBox tbxMoCaption;
  private Button btnMoSelect;
  private Label label4;
  private GroupBox grbRoute;
  private Button btnPrototype;
  private TextBox tbxPrototype;
  private Label lblPrototype;
  private Label lblRouteDateFinish;
  private Label lblRouteDateStart;
  private CheckBox cbxRouteDataFinish;
  private CheckBox cbxRouteDateStart;
  private DateTimePicker dtpRouteDateFinish;
  private DateTimePicker dtpRouteDateStart;
  private Label lblRouteKind;
  private Label lblRoutePurpose;
  private ComboBox cbRouteKind;
  private ComboBox cbRoutePurpose;
  private Label lblRouteType;
  private ComboBox cbRouteType;
  private TextBox tbxObjDesign;
  private Label lblRouteDesign;
  private Label lblRouteName;
  private TextBox tbxObjName;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControlData()
  {
    this._objNameControl = (Control) this.tbxObjName;
    this._objDesignationControl = (Control) this.tbxObjDesign;
    this._artNameControl = (Control) this.tbxArtName;
    this._moNameControl = (Control) this.tbxMoCaption;
    this._helpTopicId = 1425;
    this.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.LoadContextObjectData();
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

  /// <summary>
  /// 
  /// </summary>
  public CehRoutesObjectCreatorControl() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="creatorExtraParams"></param>
  public CehRoutesObjectCreatorControl(
    CreatedObjectItem createdObject,
    IObjectCreatorParams creatorExtraParams)
    : base(createdObject, creatorExtraParams)
  {
    this.InitializeComponent();
    this.InitializeControlData();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoLoadObjectData(IDBObject dbObject)
  {
    if (this.FirstTimeDataLoading)
    {
      TechObjectCreatorBaseControl.FillComboBoxList(dbObject.Session, this.cbRouteType, TechCardConsts.AttributeTypes.RouteTypeAttrGUID);
      TechObjectCreatorBaseControl.FillComboBoxList(dbObject.Session, this.cbRoutePurpose, TechCardConsts.AttributeTypes.RoutePurposeAttrGUID);
      TechObjectCreatorBaseControl.FillComboBoxList(dbObject.Session, this.cbRouteKind, TechCardConsts.AttributeTypes.RouteKindAttrGUID);
    }
    IDBAttribute byGuid1 = dbObject.Attributes.FindByGUID(TechCardConsts.AttributeTypes.RouteTypeAttrGUID);
    if (byGuid1 != null)
      this.cbRouteType.SelectedIndex = this.cbRouteType.Items.IndexOf((object) byGuid1.AsString);
    IDBAttribute byGuid2 = dbObject.Attributes.FindByGUID(TechCardConsts.AttributeTypes.RoutePurposeAttrGUID);
    if (byGuid2 != null)
      this.cbRoutePurpose.SelectedIndex = this.cbRoutePurpose.Items.IndexOf((object) byGuid2.AsString);
    IDBAttribute byGuid3 = dbObject.Attributes.FindByGUID(TechCardConsts.AttributeTypes.RouteKindAttrGUID);
    if (byGuid3 != null)
      this.cbRouteKind.SelectedIndex = this.cbRouteKind.Items.IndexOf((object) byGuid3.AsString);
    IDBAttribute byGuid4 = dbObject.Attributes.FindByGUID(TechCardConsts.AttributeTypes.DateStartAttrGUID);
    if (byGuid4 != null && byGuid4.AsDateTime != DateTime.MinValue)
    {
      this.cbxRouteDateStart.Checked = true;
      this.dtpRouteDateStart.Value = byGuid4.AsDateTime;
    }
    IDBAttribute byGuid5 = dbObject.Attributes.FindByGUID(TechCardConsts.AttributeTypes.DateFinishAttrGUID);
    if (byGuid5 != null && byGuid5.AsDateTime != DateTime.MinValue)
    {
      this.cbxRouteDataFinish.Checked = true;
      this.dtpRouteDateFinish.Value = byGuid5.AsDateTime;
    }
    base.DoLoadObjectData(dbObject);
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoSaveObjectData(IDBObject dbObject)
  {
    this.CreateObject_CopyPrototypeAttributes(dbObject);
    List<AttributeValues> attributeValuesList1 = new List<AttributeValues>(7);
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.RouteTypeAttrGUID);
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.RoutePurposeAttrGUID);
    int attributeTypeId3 = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.RouteKindAttrGUID);
    int attributeTypeId4 = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.DateStartAttrGUID);
    int attributeTypeId5 = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.DateFinishAttrGUID);
    attributeValuesList1.Add(new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) this.tbxObjName.Text));
    attributeValuesList1.Add(new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) this.tbxObjDesign.Text));
    attributeValuesList1.Add(new AttributeValues(attributeTypeId1, (object) this.cbRouteType.SelectedItem.ToString()));
    attributeValuesList1.Add(new AttributeValues(attributeTypeId2, (object) this.cbRoutePurpose.SelectedItem.ToString()));
    attributeValuesList1.Add(new AttributeValues(attributeTypeId3, (object) this.cbRouteKind.SelectedItem.ToString()));
    List<AttributeValues> attributeValuesList2 = attributeValuesList1;
    DateTime dateTime;
    AttributeValues attributeValues1;
    if (!this.cbxRouteDateStart.Checked)
    {
      attributeValues1 = new AttributeValues(attributeTypeId4, (object) null);
    }
    else
    {
      int attributeID = attributeTypeId4;
      dateTime = this.dtpRouteDateStart.Value;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime> date = (System.ValueType) dateTime.Date;
      attributeValues1 = new AttributeValues(attributeID, (object) date);
    }
    attributeValuesList2.Add(attributeValues1);
    List<AttributeValues> attributeValuesList3 = attributeValuesList1;
    AttributeValues attributeValues2;
    if (!this.cbxRouteDataFinish.Checked)
    {
      attributeValues2 = new AttributeValues(attributeTypeId5, (object) null);
    }
    else
    {
      int attributeID = attributeTypeId5;
      dateTime = this.dtpRouteDateFinish.Value;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime> date = (System.ValueType) dateTime.Date;
      attributeValues2 = new AttributeValues(attributeID, (object) date);
    }
    attributeValuesList3.Add(attributeValues2);
    dbObject.SetAttributesValues(attributeValuesList1.ToArray());
    if (this._articleId == 0L)
      return;
    TechCardBaseObjectUtils.Attributes.CopyImbaseAttributes(this._articleId, dbObject.ObjectID, false, dbObject.Session);
    TechCardBaseObjectUtils.Attributes.CopyLinkAttributes(this._articleId, dbObject.ObjectID, false, dbObject.Session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  /// <returns></returns>
  protected override bool SelectProcRouteForObjectType(out long objectId)
  {
    objectId = this._moObjectId;
    return ProcMoCehRouteListViewDlg.ShowDialog(this._articleId, 0L, ref objectId) && objectId != this._moObjectId;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void ValidateObjData()
  {
    base.ValidateObjData();
    this.SetControlErrorMsg((Control) this.dtpRouteDateStart, string.Empty);
    if (!this.cbxRouteDateStart.Checked || !this.cbxRouteDataFinish.Checked || !(this.dtpRouteDateStart.Value.Date > this.dtpRouteDateFinish.Value.Date))
      return;
    this.SetControlErrorMsg((Control) this.dtpRouteDateStart, LocalizationHolder.rm.GetString(sc_19470.ssp_techcard_19471()));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void ValidateMoData(IUserSession session)
  {
    if (this.CreatedObject.ObjectTypeID != -1 && this._moObjectId != 0L)
    {
      base.ValidateMoData(session);
      if (!TechCardParamsHelper.TechParams.ProcessRoute.UniqueCehRoute)
        return;
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.CehRouteGUID), LogicalOperators.NONE, 0, false)
      };
      List<TechCardUtils.SostavSortedTreeItem> childSostavTree = TechCardUtils.GetChildSostavTree(this._moObjectId, session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, conditions);
      if (childSostavTree == null || childSostavTree.Count == 0)
        return;
      IDBObject dbObject = session.GetObject(this._moObjectId, false);
      this.SetControlErrorMsg((Control) this.tbxMoCaption, dbObject != null ? string.Format(LocalizationHolder.rm.GetString(sc_19470.ssp_techcard_19473()), (object) dbObject.Caption, (object) dbObject.ObjectID) : string.Format(LocalizationHolder.rm.GetString(sc_19470.ssp_techcard_19472()), (object) this._moObjectId));
    }
    else
      base.ValidateMoData(session);
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
    ICompositionsAutomaticSortingService service1 = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) session, false);
    service1?.CreateSession((object) session.SessionGUID);
    long objectId1 = newObject.ObjectID;
    try
    {
      this.CreateObject_CopyPrototypeComposition(session);
      if ((session.GetRelation(objectId1, this._moObjectId, TechCardConsts.RelTypes.TechRelationID, true) ?? session.GetRelation(this._moObjectId, objectId1, TechCardConsts.RelTypes.TechRelationID, true)) == null)
      {
        TechcardClientUtils.StartCreateRelations(this._moObjectId, session);
        try
        {
          List<IDBRelation> relations = TechcardClientUtils.CreateRelations(session, objectId1, new int[1]
          {
            TechCardConsts.RelTypes.TechRelationID
          }, new long[1]{ this._moObjectId }, DateTime.Now, TechCreateRelMode.tcrmEnterIn);
          if (relations.Count > 0)
            this._notificationEvents.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relations.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) relations.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) relations.Select<IDBRelation, int>((System.Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
        }
        finally
        {
          TechcardClientUtils.StopCreateRelations(session);
        }
      }
      IEnumerable<RelObjInfoItem> relObjInfoItems;
      IEnumerable<ObjInfoItem> objInfoItems = !(this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams) || !TechcardClientControlsUtils.GetItemsApplicabilityInfo(creatorExtraParams.Items, (System.IServiceProvider) ApplicationServices.Container, out relObjInfoItems) ? (IEnumerable<ObjInfoItem>) null : relObjInfoItems.Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo));
      if ((this._prototypeObjId == 0L || this._prototypeObjId == -1L) && (this.CreatedObject.ObjectID == 0L || this.CreatedObject.PrototypeID == -1L))
      {
        ICehRouteStringService customService = (ICehRouteStringService) session.GetCustomService(typeof (ICehRouteStringService));
        if (customService != null)
        {
          ICehRouteStringItem cehRouteStringItem;
          customService.LoadSettings(session.SessionGUID, out cehRouteStringItem);
          ITechCardClassifyObjectService service2 = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
          TechcardClientUtils.StartCreateRelations(this.CreatedObject.ObjectID, session);
          try
          {
            foreach (ICehRouteStringTemplItem routeStringTemplItem in (IEnumerable<ICehRouteStringTemplItem>) cehRouteStringItem.Items)
            {
              if (routeStringTemplItem != null && !MetaDataHelper.IsObjectTypeChildOf(routeStringTemplItem.ObjTypeID, TechCardConsts.ObjectTypes.CehRouteID))
              {
                IMSApplicability applicability = MetaDataHelper.GetApplicability(this.CreatedObject.ObjectTypeID, routeStringTemplItem.ObjTypeID, TechCardConsts.RelTypes.TechRelationID);
                if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled)
                {
                  IDBObjectCollection objectCollection = session.GetObjectCollection(routeStringTemplItem.ObjTypeID);
                  if (objectCollection != null)
                  {
                    IDBObject dbObject1 = objectCollection.Create();
                    long objectId2 = dbObject1.ObjectID;
                    AttributeValues[] valuesList = new AttributeValues[2];
                    string initValue = "";
                    string objectTypeName;
                    if (this._articleId != 0L)
                    {
                      ObjInfoItem classifyObjectItem = new ObjInfoItem(dbObject1);
                      ObjInfoItem contextObjectItem = new ObjInfoItem(this._articleId);
                      ITechCardClassifyObjectService classifyObjectService1 = service2;
                      IUserSession session1 = session;
                      TechCardClassifyObjectAttributeParams classifyParams1 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem);
                      classifyParams1.ExtraContextObjInfoItems = objInfoItems;
                      TechCardClassifyObjectDesignationStrategy classifyStrategy1 = new TechCardClassifyObjectDesignationStrategy();
                      ref string local1 = ref objectTypeName;
                      classifyObjectService1.ClassifyObjectAttribute(session1, classifyParams1, (ITechCardClassifyObjectStrategy) classifyStrategy1, out local1);
                      ITechCardClassifyObjectService classifyObjectService2 = service2;
                      IUserSession session2 = session;
                      TechCardClassifyObjectAttributeParams classifyParams2 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, contextObjectItem);
                      classifyParams2.ExtraContextObjInfoItems = objInfoItems;
                      TechCardClassifyObjectNameStrategy classifyStrategy2 = new TechCardClassifyObjectNameStrategy();
                      ref string local2 = ref initValue;
                      classifyObjectService2.ClassifyObjectAttribute(session2, classifyParams2, (ITechCardClassifyObjectStrategy) classifyStrategy2, out local2);
                    }
                    else
                      objectTypeName = session.GetObjectType(routeStringTemplItem.ObjTypeID).ObjectTypeName;
                    valuesList[0] = new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) initValue);
                    valuesList[1] = new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) objectTypeName);
                    dbObject1.SetAttributesValues(valuesList);
                    List<IDBRelation> source = new List<IDBRelation>()
                    {
                      TechcardClientUtils.CreateRelation(TechCardConsts.RelTypes.TechRelationID, session, newObject, dbObject1)
                    };
                    if (source.Count != 0)
                      this._notificationEvents.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source.Select<IDBRelation, int>((System.Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
                    long relationId = 0;
                    if (source.Count != 0 && source[0] != null)
                      relationId = source[0].RelationID;
                    this._relObjInfo4AutoSelect.Add(new RelObjInfoItem(relationId)
                    {
                      PartInfo = new ObjInfoItem(dbObject1)
                    });
                    IDBObject dbObject2 = session.GetObject(objectId2);
                    if (dbObject2 != null && dbObject2.IsCreationMode)
                      dbObject2.CommitCreation(true);
                  }
                }
              }
            }
          }
          finally
          {
            TechcardClientUtils.StopCreateRelations(session);
          }
        }
      }
      else if (this._articleId != 0L && !this.CreatedObject.IsVersion)
      {
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(1)
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TemplRouteBaseID).ToArray(), LogicalOperators.NONE, 0, false)
        };
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(1)
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
        };
        IDBRelationCollection relationCollection = session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
        DataTable dataTable;
        if (newObject.IsCreationMode)
        {
          long[] collection = relationCollection.ConsistFromBlanks(newObject.ObjectID);
          dataTable = collection == null || collection.Length == 0 ? (DataTable) null : DataHelper.GetObjectData(TechCardConsts.ObjectTypes.TemplRouteBaseID, session, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<long>) new List<long>((IEnumerable<long>) collection));
        }
        else
        {
          relationCollection.LocalTypesMode = true;
          DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray());
          dataTable = relationCollection.ConsistFrom(paramSet, newObject.ObjectID, false);
        }
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          ITechCardClassifyObjectService service3 = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row[0] != DBNull.Value)
            {
              IDBObject dbObject = session.GetObject(Convert.ToInt64(row[0]));
              if (dbObject != null && !dbObject.ReadOnly)
              {
                ObjInfoItem classifyObjectItem = new ObjInfoItem(dbObject);
                ObjInfoItem contextObjectItem = new ObjInfoItem(this._articleId);
                ITechCardClassifyObjectService classifyObjectService3 = service3;
                IUserSession session3 = session;
                TechCardClassifyObjectAttributeParams classifyParams3 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem);
                classifyParams3.ExtraContextObjInfoItems = objInfoItems;
                TechCardClassifyObjectDesignationStrategy classifyStrategy3 = new TechCardClassifyObjectDesignationStrategy();
                string initValue1;
                ref string local3 = ref initValue1;
                int num1 = classifyObjectService3.ClassifyObjectAttribute(session3, classifyParams3, (ITechCardClassifyObjectStrategy) classifyStrategy3, out local3) ? 1 : 0;
                ITechCardClassifyObjectService classifyObjectService4 = service3;
                IUserSession session4 = session;
                TechCardClassifyObjectAttributeParams classifyParams4 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, contextObjectItem);
                classifyParams4.ExtraContextObjInfoItems = objInfoItems;
                TechCardClassifyObjectNameStrategy classifyStrategy4 = new TechCardClassifyObjectNameStrategy();
                string initValue2;
                ref string local4 = ref initValue2;
                int num2 = classifyObjectService4.ClassifyObjectAttribute(session4, classifyParams4, (ITechCardClassifyObjectStrategy) classifyStrategy4, out local4) ? 1 : 0;
                if ((num1 | num2) != 0)
                {
                  AttributeValues[] valuesList = new AttributeValues[2]
                  {
                    new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) initValue2),
                    new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) initValue1)
                  };
                  dbObject.SetAttributesValues(valuesList);
                }
              }
            }
          }
        }
      }
      this.RemoveStaledLinks();
      this.AutoCheckInMo(session, this._notificationEvents);
    }
    finally
    {
      service1?.DisposeSession((object) session.SessionGUID);
    }
    return true;
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
    if (!base.CreatedObject_DoAfterCommitCreation(session, newObjectId, nea))
      return false;
    if (!this.CreatedObject.IsVersion)
      ServiceUtils.GetService<ICehRouteStringService>((object) session, false)?.CreateCehRouteString(this.CreatedObject.ObjectID, session.SessionGUID);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxObjDesign_TextChanged(object sender, EventArgs e) => this.ValidateObjData();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbRouteType_SelectionChangeCommitted(object sender, EventArgs e)
  {
    this.ValidateObjData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void dtpRouteDateStart_ValueChanged(object sender, EventArgs e) => this.ValidateObjData();

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
  private void btnPrototype_Click(object sender, EventArgs e) => this.SelectPrototype();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbxRouteDateStart_CheckedChanged(object sender, EventArgs e)
  {
    this.dtpRouteDateStart.Enabled = this.cbxRouteDateStart.Checked;
    this.ValidateObjData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbxRouteDataFinish_CheckedChanged(object sender, EventArgs e)
  {
    this.dtpRouteDateFinish.Enabled = this.cbxRouteDataFinish.Checked;
    this.ValidateObjData();
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
  private void tbxArtName_KeyDown(object sender, KeyEventArgs e)
  {
    if ((e.KeyCode & Keys.Delete) == Keys.None && (e.KeyCode & Keys.Back) == Keys.None)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateArticleData(0L, true, true, sessionKeeper.Session);
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
    this.btnArticle = new Button();
    this.tbxArtName = new TextBox();
    this.label3 = new Label();
    this.tbxMoCaption = new TextBox();
    this.btnMoSelect = new Button();
    this.label4 = new Label();
    this.grbRoute = new GroupBox();
    this.btnPrototype = new Button();
    this.tbxPrototype = new TextBox();
    this.lblPrototype = new Label();
    this.lblRouteDateFinish = new Label();
    this.lblRouteDateStart = new Label();
    this.cbxRouteDataFinish = new CheckBox();
    this.cbxRouteDateStart = new CheckBox();
    this.dtpRouteDateFinish = new DateTimePicker();
    this.dtpRouteDateStart = new DateTimePicker();
    this.lblRouteKind = new Label();
    this.lblRoutePurpose = new Label();
    this.cbRouteKind = new ComboBox();
    this.cbRoutePurpose = new ComboBox();
    this.lblRouteType = new Label();
    this.cbRouteType = new ComboBox();
    this.tbxObjDesign = new TextBox();
    this.lblRouteDesign = new Label();
    this.lblRouteName = new Label();
    this.tbxObjName = new TextBox();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.grbArticle.SuspendLayout();
    this.grbRoute.SuspendLayout();
    this.SuspendLayout();
    this.grbArticle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grbArticle.Controls.Add((Control) this.btnArticle);
    this.grbArticle.Controls.Add((Control) this.tbxArtName);
    this.grbArticle.Controls.Add((Control) this.label3);
    this.grbArticle.Controls.Add((Control) this.tbxMoCaption);
    this.grbArticle.Controls.Add((Control) this.btnMoSelect);
    this.grbArticle.Controls.Add((Control) this.label4);
    this.grbArticle.Location = new Point(11, 260);
    this.grbArticle.Name = "grbArticle";
    this.grbArticle.Size = new Size(418, 77);
    this.grbArticle.TabIndex = 4;
    this.grbArticle.TabStop = false;
    this.grbArticle.Text = "Привязка к изделию";
    this.btnArticle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnArticle.FlatStyle = FlatStyle.System;
    this.btnArticle.ImeMode = ImeMode.NoControl;
    this.btnArticle.Location = new Point(383, 16 /*0x10*/);
    this.btnArticle.Name = "btnArticle";
    this.btnArticle.Size = new Size(24, 23);
    this.btnArticle.TabIndex = 2;
    this.btnArticle.Text = "...";
    this.btnArticle.Click += new EventHandler(this.btnArticle_Click);
    this.tbxArtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxArtName.BackColor = SystemColors.Window;
    this.tbxArtName.ForeColor = SystemColors.WindowText;
    this.tbxArtName.Location = new Point(124, 19);
    this.tbxArtName.Name = "tbxArtName";
    this.tbxArtName.ReadOnly = true;
    this.tbxArtName.Size = new Size(256 /*0x0100*/, 20);
    this.tbxArtName.TabIndex = 1;
    this.tbxArtName.KeyDown += new KeyEventHandler(this.tbxArtName_KeyDown);
    this.label3.ImeMode = ImeMode.NoControl;
    this.label3.Location = new Point(6, 22);
    this.label3.Name = "label3";
    this.label3.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.label3.TabIndex = 13;
    this.label3.Text = "Изделие";
    this.tbxMoCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxMoCaption.BackColor = SystemColors.Window;
    this.tbxMoCaption.Location = new Point(124, 45);
    this.tbxMoCaption.Name = "tbxMoCaption";
    this.tbxMoCaption.ReadOnly = true;
    this.tbxMoCaption.Size = new Size(256 /*0x0100*/, 20);
    this.tbxMoCaption.TabIndex = 3;
    this.tbxMoCaption.KeyDown += new KeyEventHandler(this.tbxMoCaption_KeyDown);
    this.btnMoSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnMoSelect.FlatStyle = FlatStyle.System;
    this.btnMoSelect.ImeMode = ImeMode.NoControl;
    this.btnMoSelect.Location = new Point(383, 43);
    this.btnMoSelect.Name = "btnMoSelect";
    this.btnMoSelect.Size = new Size(24, 23);
    this.btnMoSelect.TabIndex = 4;
    this.btnMoSelect.Text = "...";
    this.btnMoSelect.Click += new EventHandler(this.btnMoSelect_Click);
    this.label4.ImeMode = ImeMode.NoControl;
    this.label4.Location = new Point(6, 48 /*0x30*/);
    this.label4.Name = "label4";
    this.label4.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.label4.TabIndex = 7;
    this.label4.Text = "Маршрут обработки";
    this.grbRoute.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grbRoute.Controls.Add((Control) this.btnPrototype);
    this.grbRoute.Controls.Add((Control) this.tbxPrototype);
    this.grbRoute.Controls.Add((Control) this.lblPrototype);
    this.grbRoute.Controls.Add((Control) this.lblRouteDateFinish);
    this.grbRoute.Controls.Add((Control) this.lblRouteDateStart);
    this.grbRoute.Controls.Add((Control) this.cbxRouteDataFinish);
    this.grbRoute.Controls.Add((Control) this.cbxRouteDateStart);
    this.grbRoute.Controls.Add((Control) this.dtpRouteDateFinish);
    this.grbRoute.Controls.Add((Control) this.dtpRouteDateStart);
    this.grbRoute.Controls.Add((Control) this.lblRouteKind);
    this.grbRoute.Controls.Add((Control) this.lblRoutePurpose);
    this.grbRoute.Controls.Add((Control) this.cbRouteKind);
    this.grbRoute.Controls.Add((Control) this.cbRoutePurpose);
    this.grbRoute.Controls.Add((Control) this.lblRouteType);
    this.grbRoute.Controls.Add((Control) this.cbRouteType);
    this.grbRoute.Controls.Add((Control) this.tbxObjDesign);
    this.grbRoute.Controls.Add((Control) this.lblRouteDesign);
    this.grbRoute.Controls.Add((Control) this.lblRouteName);
    this.grbRoute.Controls.Add((Control) this.tbxObjName);
    this.grbRoute.Location = new Point(11, 12);
    this.grbRoute.Name = "grbRoute";
    this.grbRoute.Size = new Size(418, 242);
    this.grbRoute.TabIndex = 3;
    this.grbRoute.TabStop = false;
    this.grbRoute.Text = "Параметры маршрута";
    this.btnPrototype.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnPrototype.FlatStyle = FlatStyle.System;
    this.btnPrototype.ImeMode = ImeMode.NoControl;
    this.btnPrototype.Location = new Point(382, 154);
    this.btnPrototype.Name = "btnPrototype";
    this.btnPrototype.Size = new Size(24, 23);
    this.btnPrototype.TabIndex = 7;
    this.btnPrototype.Text = "...";
    this.btnPrototype.Click += new EventHandler(this.btnPrototype_Click);
    this.tbxPrototype.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxPrototype.BackColor = SystemColors.Window;
    this.tbxPrototype.Location = new Point(124, 156);
    this.tbxPrototype.Name = "tbxPrototype";
    this.tbxPrototype.ReadOnly = true;
    this.tbxPrototype.Size = new Size(256 /*0x0100*/, 20);
    this.tbxPrototype.TabIndex = 6;
    this.tbxPrototype.KeyDown += new KeyEventHandler(this.tbxPrototype_KeyDown);
    this.lblPrototype.ImeMode = ImeMode.NoControl;
    this.lblPrototype.Location = new Point(6, 158);
    this.lblPrototype.Name = "lblPrototype";
    this.lblPrototype.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblPrototype.TabIndex = 41;
    this.lblPrototype.Text = "Прототип";
    this.lblRouteDateFinish.AutoSize = true;
    this.lblRouteDateFinish.ImeMode = ImeMode.NoControl;
    this.lblRouteDateFinish.Location = new Point(6, 214);
    this.lblRouteDateFinish.Name = "lblRouteDateFinish";
    this.lblRouteDateFinish.Size = new Size(166, 13);
    this.lblRouteDateFinish.TabIndex = 40;
    this.lblRouteDateFinish.Text = "Дата аннулирования маршрута";
    this.lblRouteDateStart.AutoSize = true;
    this.lblRouteDateStart.ImeMode = ImeMode.NoControl;
    this.lblRouteDateStart.Location = new Point(6, 188);
    this.lblRouteDateStart.Name = "lblRouteDateStart";
    this.lblRouteDateStart.Size = new Size(119, 13);
    this.lblRouteDateStart.TabIndex = 39;
    this.lblRouteDateStart.Text = "Дата ввода маршрута";
    this.cbxRouteDataFinish.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.cbxRouteDataFinish.AutoSize = true;
    this.cbxRouteDataFinish.ImeMode = ImeMode.NoControl;
    this.cbxRouteDataFinish.Location = new Point(388, 212);
    this.cbxRouteDataFinish.Name = "cbxRouteDataFinish";
    this.cbxRouteDataFinish.Size = new Size(15, 14);
    this.cbxRouteDataFinish.TabIndex = 11;
    this.cbxRouteDataFinish.UseVisualStyleBackColor = true;
    this.cbxRouteDataFinish.CheckedChanged += new EventHandler(this.cbxRouteDataFinish_CheckedChanged);
    this.cbxRouteDateStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.cbxRouteDateStart.AutoSize = true;
    this.cbxRouteDateStart.ImeMode = ImeMode.NoControl;
    this.cbxRouteDateStart.Location = new Point(388, 186);
    this.cbxRouteDateStart.Name = "cbxRouteDateStart";
    this.cbxRouteDateStart.Size = new Size(15, 14);
    this.cbxRouteDateStart.TabIndex = 9;
    this.cbxRouteDateStart.UseVisualStyleBackColor = true;
    this.cbxRouteDateStart.CheckedChanged += new EventHandler(this.cbxRouteDateStart_CheckedChanged);
    this.dtpRouteDateFinish.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.dtpRouteDateFinish.Enabled = false;
    this.dtpRouteDateFinish.Location = new Point(205, 208 /*0xD0*/);
    this.dtpRouteDateFinish.Name = "dtpRouteDateFinish";
    this.dtpRouteDateFinish.Size = new Size(177, 20);
    this.dtpRouteDateFinish.TabIndex = 10;
    this.dtpRouteDateFinish.ValueChanged += new EventHandler(this.dtpRouteDateStart_ValueChanged);
    this.dtpRouteDateStart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.dtpRouteDateStart.Checked = false;
    this.dtpRouteDateStart.Enabled = false;
    this.dtpRouteDateStart.Location = new Point(205, 182);
    this.dtpRouteDateStart.Name = "dtpRouteDateStart";
    this.dtpRouteDateStart.Size = new Size(177, 20);
    this.dtpRouteDateStart.TabIndex = 8;
    this.dtpRouteDateStart.ValueChanged += new EventHandler(this.dtpRouteDateStart_ValueChanged);
    this.lblRouteKind.AutoSize = true;
    this.lblRouteKind.ImeMode = ImeMode.NoControl;
    this.lblRouteKind.Location = new Point(6, 131);
    this.lblRouteKind.Name = "lblRouteKind";
    this.lblRouteKind.Size = new Size(79, 13);
    this.lblRouteKind.TabIndex = 34;
    this.lblRouteKind.Text = "Вид маршрута";
    this.lblRoutePurpose.AutoSize = true;
    this.lblRoutePurpose.ImeMode = ImeMode.NoControl;
    this.lblRoutePurpose.Location = new Point(6, 104);
    this.lblRoutePurpose.Name = "lblRoutePurpose";
    this.lblRoutePurpose.Size = new Size(68, 13);
    this.lblRoutePurpose.TabIndex = 33;
    this.lblRoutePurpose.Text = "Назначение";
    this.cbRouteKind.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbRouteKind.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbRouteKind.FormattingEnabled = true;
    this.cbRouteKind.Location = new Point(124, 128 /*0x80*/);
    this.cbRouteKind.Name = "cbRouteKind";
    this.cbRouteKind.Size = new Size(282, 21);
    this.cbRouteKind.TabIndex = 5;
    this.cbRouteKind.SelectionChangeCommitted += new EventHandler(this.cbRouteType_SelectionChangeCommitted);
    this.cbRoutePurpose.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbRoutePurpose.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbRoutePurpose.FormattingEnabled = true;
    this.cbRoutePurpose.Location = new Point(124, 100);
    this.cbRoutePurpose.Name = "cbRoutePurpose";
    this.cbRoutePurpose.Size = new Size(282, 21);
    this.cbRoutePurpose.TabIndex = 4;
    this.lblRouteType.AutoSize = true;
    this.lblRouteType.ImeMode = ImeMode.NoControl;
    this.lblRouteType.Location = new Point(6, 77);
    this.lblRouteType.Name = "lblRouteType";
    this.lblRouteType.Size = new Size(79, 13);
    this.lblRouteType.TabIndex = 30;
    this.lblRouteType.Text = "Тип маршрута";
    this.cbRouteType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbRouteType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbRouteType.FormattingEnabled = true;
    this.cbRouteType.Location = new Point(124, 72);
    this.cbRouteType.Name = "cbRouteType";
    this.cbRouteType.Size = new Size(282, 21);
    this.cbRouteType.TabIndex = 3;
    this.tbxObjDesign.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxObjDesign.Location = new Point(124, 19);
    this.tbxObjDesign.Name = "tbxObjDesign";
    this.tbxObjDesign.Size = new Size(282, 20);
    this.tbxObjDesign.TabIndex = 1;
    this.tbxObjDesign.TextChanged += new EventHandler(this.tbxObjDesign_TextChanged);
    this.lblRouteDesign.ImeMode = ImeMode.NoControl;
    this.lblRouteDesign.Location = new Point(6, 22);
    this.lblRouteDesign.Name = "lblRouteDesign";
    this.lblRouteDesign.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblRouteDesign.TabIndex = 28;
    this.lblRouteDesign.Text = "Обозначение";
    this.lblRouteName.ImeMode = ImeMode.NoControl;
    this.lblRouteName.Location = new Point(6, 49);
    this.lblRouteName.Name = "lblRouteName";
    this.lblRouteName.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblRouteName.TabIndex = 26;
    this.lblRouteName.Text = "Наименование";
    this.tbxObjName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxObjName.Location = new Point(124, 45);
    this.tbxObjName.Name = "tbxObjName";
    this.tbxObjName.Size = new Size(282, 20);
    this.tbxObjName.TabIndex = 2;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.grbArticle);
    this.Controls.Add((Control) this.grbRoute);
    this.MinimumSize = new Size(441, 352);
    this.Name = nameof (CehRoutesObjectCreatorControl);
    this.Size = new Size(441, 352);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.grbArticle.ResumeLayout(false);
    this.grbArticle.PerformLayout();
    this.grbRoute.ResumeLayout(false);
    this.grbRoute.PerformLayout();
    this.ResumeLayout(false);
  }
}
