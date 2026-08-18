// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Route_Template.RouteTemplateObjectCreatorControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.ObjectCreator;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Route_Template;

/// <summary>
/// 
/// </summary>
public class RouteTemplateObjectCreatorControl : TechObjectCreatorBaseControl
{
  /// <summary>идентификатор объекта "Маршрут расцеховки"</summary>
  private long _cehRouteObjId;
  /// <summary>идентификатор объекта "Изделие"</summary>
  private long _articleId;
  /// <summary>Режим связи шаблона с изделием</summary>
  private int _artLinkMode;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox chbArticleLink;
  private GroupBox grbRouteTemplate;
  private Button btnPrototype;
  private TextBox tbxProt;
  private Label lblProt;
  private ComboBox cbType;
  private Label lblType;
  private TextBox tbxName;
  private Label lblName;
  private TextBox tbxDesign;
  private Label lblDesign;
  private GroupBox grbArticle;
  private Button btnArticle;
  private TextBox tbxArtName;
  private Label label1;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControlData()
  {
    this._helpTopicId = 1425;
    this.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.LoadTemplateTypes();
    this.LoadContextObjectData();
    this.UpdateCustomControls();
  }

  /// <summary>Fill templates child list</summary>
  private void LoadTemplateTypes()
  {
    IntBaseInfo[] array = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TemplRouteBaseID).Select<int, IMSObjectType>(new Func<int, IMSObjectType>(MetaDataHelper.GetObjectType)).Where<IMSObjectType>((Func<IMSObjectType, bool>) (imsType => imsType != null && imsType.VersionsMode != 0)).Select<IMSObjectType, IntBaseInfo>((Func<IMSObjectType, IntBaseInfo>) (imsType => new IntBaseInfo((long) imsType.ObjectTypeID, imsType.ObjectName))).ToArray<IntBaseInfo>();
    TechObjectCreatorBaseControl.FillComboBoxList(this.cbType, (IEnumerable<object>) ((IEnumerable<IntBaseInfo>) array).Select<IntBaseInfo, object>((Func<IntBaseInfo, object>) (item => (object) item)).ToArray<object>(), (object) ((IEnumerable<IntBaseInfo>) array).FirstOrDefault<IntBaseInfo>((Func<IntBaseInfo, bool>) (imsObjType => imsObjType.Value == (long) this.CreatedObject.ObjectTypeID)));
  }

  /// <summary>
  /// Загрузка информации о контексте объекта (Изделие / РМ)
  /// </summary>
  private void LoadContextObjectData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> longList = new List<long>();
      if (this.CreatedObject.ObjectRelationArray != null)
        longList.AddRange(this.CreatedObject.ObjectRelationArray.Select<ObjectRelationLink, long>((Func<ObjectRelationLink, long>) (item => item.ObjectID)));
      if ((this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams ? creatorExtraParams.Items : (ISelectedItems) null) != null)
      {
        IDBObjectID itemData = creatorExtraParams.Items.GetItemData<IDBObjectID>(0, false);
        if (itemData != null)
          longList.Add(itemData.Value);
        IDBObjectID parentData = creatorExtraParams.Items.GetParentData<IDBObjectID>(0, false);
        if (parentData != null)
          longList.Add(parentData.Value);
      }
      long articleId = 0;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
      foreach (long objectID in longList)
      {
        if (this._cehRouteObjId != 0L)
        {
          if (articleId != 0L)
            break;
        }
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
        if (childrenIdRecursive.Contains(objectInfo.ObjectTypeID))
          articleId = objectInfo.ObjectID;
        else if (MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, TechCardConsts.ObjectTypes.CehRouteID))
          this._cehRouteObjId = objectInfo.ObjectID;
      }
      if (articleId == 0L)
      {
        List<long> partIdList = longList;
        IUserSession session = sessionKeeper.Session;
        int[] relations = new int[2]
        {
          TechCardConsts.RelTypes.TechRelationID,
          TechCardConsts.RelTypes.SimpleRelationID
        };
        foreach (TechCardUtils.SostavTreeItem sostavTreeItem in TechCardUtils.GetParentSostavTree(partIdList, session, relations, true, (ConditionStructure[]) null, (Dictionary<string, ColumnDescriptor>) null))
        {
          if (sostavTreeItem != null && childrenIdRecursive.Contains(sostavTreeItem.ObjectTypeID))
          {
            articleId = sostavTreeItem.ProjID;
            break;
          }
        }
      }
      this.UpdateArticleData(articleId, sessionKeeper.Session);
    }
  }

  /// <summary>Select article for route</summary>
  private void SelectArticle()
  {
    List<long> source = TechCardClientConst.SelectObjectsDlg((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes, LocalizationHolder.rm.GetString(sc_19486.ssp_techcard_19487()));
    if (!source.Any<long>())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.UpdateArticleData(source.First<long>(), sessionKeeper.Session);
      this.UpdateCustomControls();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void SelectPrototypeObj()
  {
    if (!this.ValidateObjType())
      return;
    long prototypeObjId = TechCardClientConst.SelectObjectDlg(MetaDataHelper.GetObjectTypeGuid(this.CreatedObject.ObjectTypeID), LocalizationHolder.rm.GetString("TechCard.Client_146"));
    if (prototypeObjId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdatePrototypeData(prototypeObjId, sessionKeeper.Session);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  private void ClassifyObjName(IUserSession session)
  {
    if (this._articleId == 0L)
      return;
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(this.CreatedObject.ObjectID, this.CreatedObject.ObjectTypeID);
    ObjInfoItem contextObjectItem = new ObjInfoItem(this._articleId);
    IEnumerable<RelObjInfoItem> relObjInfoItems;
    IEnumerable<ObjInfoItem> objInfoItems = !(this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams) || !TechcardClientControlsUtils.GetItemsApplicabilityInfo(creatorExtraParams.Items, (System.IServiceProvider) ApplicationServices.Container, out relObjInfoItems) ? (IEnumerable<ObjInfoItem>) null : relObjInfoItems.Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo));
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
    this.tbxName.Text = str2;
    this.tbxDesign.Text = str1;
  }

  /// <summary>Обновление параметров изделия</summary>
  /// <param name="articleId"></param>
  /// <param name="session"></param>
  private void UpdateArticleData(long articleId, IUserSession session)
  {
    try
    {
      if (this._articleId == articleId)
        return;
      this._articleId = articleId;
      this.tbxArtName.Text = TechCardConsts.Utils.GetObjectString(this._articleId, session);
      this.ClassifyObjName(session);
    }
    finally
    {
      this.ValidateArticleData();
      this.UpdateCustomControls();
    }
  }

  /// <summary>Обновление значения маршрута обработки</summary>
  /// <param name="prototypeObjId"></param>
  /// <param name="session"></param>
  private void UpdatePrototypeData(long prototypeObjId, IUserSession session)
  {
    try
    {
      if (this._prototypeObjId == prototypeObjId)
        return;
      this._prototypeObjId = prototypeObjId;
      this.tbxProt.Text = TechCardConsts.Utils.GetObjectString(this._prototypeObjId, session);
    }
    finally
    {
      this.ValidatePrototypeData();
    }
  }

  /// <summary>Обновление параметров изделия</summary>
  /// <param name="linkMode"></param>
  private void UpdateArtLinkModeData(int linkMode)
  {
    if (this._artLinkMode == linkMode)
      return;
    this._artLinkMode = linkMode;
    this.ValidateArticleData();
    this.UpdateCustomControls();
  }

  /// <summary>Обновление состояний контролов</summary>
  private void UpdateCustomControls()
  {
    this.grbArticle.Enabled = this._artLinkMode != 0;
    this.btnArticle.Enabled = this._artLinkMode != 0 && this._cehRouteObjId == 0L;
  }

  /// <summary>
  /// 
  /// </summary>
  private void ValidateTemplateData()
  {
    this.SetControlErrorMsg((Control) this.tbxDesign, string.Empty);
    if (!(this.tbxDesign.Text == string.Empty))
      return;
    this.SetControlErrorMsg((Control) this.tbxDesign, LocalizationHolder.rm.GetString(sc_19486.ssp_techcard_19488()));
  }

  /// <summary>
  /// 
  /// </summary>
  private void ValidateArticleData()
  {
    if (this.CreatedObject == null)
      return;
    this.SetControlErrorMsg((Control) this.tbxArtName, string.Empty);
    if (this._articleId != 0L || this._artLinkMode != 1)
      return;
    this.SetControlErrorMsg((Control) this.tbxArtName, string.Format(LocalizationHolder.rm.GetString(sc_19486.ssp_techcard_19489()), (object) LocalizationHolder.rm.GetString(sc_19486.ssp_techcard_19490())));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool ValidateObjType()
  {
    this.SetControlErrorMsg((Control) this.cbType, string.Empty);
    if (this.CreatedObject != null && this.CreatedObject.ObjectTypeID != -1)
      return true;
    this.SetControlErrorMsg((Control) this.cbType, LocalizationHolder.rm.GetString(sc_19486.ssp_techcard_19491()));
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private void ValidatePrototypeData()
  {
  }

  /// <summary>Загрузка данных объекта</summary>
  /// <param name="dbObject"></param>
  protected override void DoLoadObjectData(IDBObject dbObject)
  {
    IDBAttribute byId1 = dbObject.Attributes.FindByID(TechCardConsts.AttributeTypes.NameAttrTypeID);
    if (byId1 != null)
      this.tbxName.Text = Convert.ToString(byId1.Value);
    IDBAttribute byId2 = dbObject.Attributes.FindByID(TechCardConsts.AttributeTypes.DesignationAttrTypeID);
    if (byId2 != null)
      this.tbxDesign.Text = Convert.ToString(byId2.Value);
    IDBAttribute byGuid = dbObject.Attributes.FindByGUID(TechCardConsts.AttributeTypes.ArticleAttrGuid);
    if (byGuid != null)
    {
      string asString = byGuid.AsString;
      if (GuidHelper.IsGuid(asString))
        this.chbArticleLink.Enabled = dbObject.Session.GetObjectByID(new Guid(asString), false) != null;
    }
    if (!this.FirstTimeDataLoading)
      return;
    this.UpdatePrototypeData(this._prototypeObjId, dbObject.Session);
    if (!(this.tbxDesign.Text == string.Empty))
      return;
    this.ClassifyObjName(dbObject.Session);
  }

  /// <summary>Сохранение данных объекта</summary>
  protected override void DoSaveObjectData(IDBObject dbObject)
  {
    this.CreateObject_CopyPrototypeAttributes(dbObject);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ArticleAttrGuid);
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) this.tbxName.Text));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) this.tbxDesign.Text));
    if (this.chbArticleLink.Checked)
    {
      IDBObject dbObject1 = dbObject.Session.GetObject(this._articleId, false);
      if (dbObject1 != null)
        attributeValuesList.Add(new AttributeValues(attributeTypeId, (object) dbObject1.GUID));
    }
    else
      attributeValuesList.Add(new AttributeValues(attributeTypeId, (object) DBNull.Value));
    dbObject.SetAttributesValues(attributeValuesList.ToArray());
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
    ICompositionsAutomaticSortingService service = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) session, false);
    service?.CreateSession((object) session.SessionGUID);
    long objectId = newObject.ObjectID;
    try
    {
      this.CreateObject_CopyPrototypeComposition(session);
      if (this._cehRouteObjId != 0L)
      {
        if ((session.GetRelation(objectId, this._cehRouteObjId, TechCardConsts.RelTypes.TechRelationID, true) ?? session.GetRelation(this._cehRouteObjId, objectId, TechCardConsts.RelTypes.TechRelationID, true)) == null)
        {
          TechcardClientUtils.StartCreateRelations(this._cehRouteObjId, session);
          try
          {
            TechcardClientUtils.CreateRelations(session, objectId, new int[1]
            {
              TechCardConsts.RelTypes.TechRelationID
            }, new long[1]{ this._cehRouteObjId }, DateTime.Now, TechCreateRelMode.tcrmEnterIn);
          }
          finally
          {
            TechcardClientUtils.StopCreateRelations(session);
          }
        }
      }
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
  public RouteTemplateObjectCreatorControl() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="creatorExtraParams"></param>
  public RouteTemplateObjectCreatorControl(
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
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chbArticleLink_CheckedChanged(object sender, EventArgs e)
  {
    this.UpdateArtLinkModeData(this.chbArticleLink.Checked ? 1 : 0);
  }

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
  private void btnPrototype_Click(object sender, EventArgs e) => this.SelectPrototypeObj();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxDesign_TextChanged(object sender, EventArgs e)
  {
    this.ValidateTemplateData();
    this.UpdateCustomControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbType_SelectedIndexChanged(object sender, EventArgs e)
  {
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
    this.chbArticleLink = new CheckBox();
    this.grbRouteTemplate = new GroupBox();
    this.btnPrototype = new Button();
    this.tbxProt = new TextBox();
    this.lblProt = new Label();
    this.cbType = new ComboBox();
    this.lblType = new Label();
    this.tbxName = new TextBox();
    this.lblName = new Label();
    this.tbxDesign = new TextBox();
    this.lblDesign = new Label();
    this.grbArticle = new GroupBox();
    this.btnArticle = new Button();
    this.tbxArtName = new TextBox();
    this.label1 = new Label();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.grbRouteTemplate.SuspendLayout();
    this.grbArticle.SuspendLayout();
    this.SuspendLayout();
    this.chbArticleLink.AutoSize = true;
    this.chbArticleLink.ImeMode = ImeMode.NoControl;
    this.chbArticleLink.Location = new Point(18, 150);
    this.chbArticleLink.Name = "chbArticleLink";
    this.chbArticleLink.Size = new Size(125, 17);
    this.chbArticleLink.TabIndex = 5;
    this.chbArticleLink.Text = "Шаблон на изделие";
    this.chbArticleLink.UseVisualStyleBackColor = true;
    this.chbArticleLink.CheckedChanged += new EventHandler(this.chbArticleLink_CheckedChanged);
    this.grbRouteTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grbRouteTemplate.Controls.Add((Control) this.btnPrototype);
    this.grbRouteTemplate.Controls.Add((Control) this.tbxProt);
    this.grbRouteTemplate.Controls.Add((Control) this.lblProt);
    this.grbRouteTemplate.Controls.Add((Control) this.cbType);
    this.grbRouteTemplate.Controls.Add((Control) this.lblType);
    this.grbRouteTemplate.Controls.Add((Control) this.tbxName);
    this.grbRouteTemplate.Controls.Add((Control) this.lblName);
    this.grbRouteTemplate.Controls.Add((Control) this.tbxDesign);
    this.grbRouteTemplate.Controls.Add((Control) this.lblDesign);
    this.grbRouteTemplate.Location = new Point(18, 14);
    this.grbRouteTemplate.Name = "grbRouteTemplate";
    this.grbRouteTemplate.Size = new Size(412, 130);
    this.grbRouteTemplate.TabIndex = 4;
    this.grbRouteTemplate.TabStop = false;
    this.grbRouteTemplate.Text = "Параметры шаблона";
    this.btnPrototype.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnPrototype.FlatStyle = FlatStyle.System;
    this.btnPrototype.ImeMode = ImeMode.NoControl;
    this.btnPrototype.Location = new Point(376, 96 /*0x60*/);
    this.btnPrototype.Name = "btnPrototype";
    this.btnPrototype.Size = new Size(24, 23);
    this.btnPrototype.TabIndex = 5;
    this.btnPrototype.Text = "...";
    this.btnPrototype.Click += new EventHandler(this.btnPrototype_Click);
    this.tbxProt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxProt.BackColor = SystemColors.Window;
    this.tbxProt.Location = new Point(124, 98);
    this.tbxProt.Name = "tbxProt";
    this.tbxProt.ReadOnly = true;
    this.tbxProt.Size = new Size(250, 20);
    this.tbxProt.TabIndex = 4;
    this.lblProt.ImeMode = ImeMode.NoControl;
    this.lblProt.Location = new Point(6, 101);
    this.lblProt.Name = "lblProt";
    this.lblProt.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblProt.TabIndex = 15;
    this.lblProt.Text = "Прототип";
    this.cbType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbType.Enabled = false;
    this.cbType.Location = new Point(124, 71);
    this.cbType.Name = "cbType";
    this.cbType.Size = new Size(276, 21);
    this.cbType.TabIndex = 3;
    this.cbType.SelectedIndexChanged += new EventHandler(this.cbType_SelectedIndexChanged);
    this.lblType.ImeMode = ImeMode.NoControl;
    this.lblType.Location = new Point(6, 76);
    this.lblType.Name = "lblType";
    this.lblType.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblType.TabIndex = 13;
    this.lblType.Text = "Тип шаблона";
    this.tbxName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxName.Location = new Point(124, 45);
    this.tbxName.Name = "tbxName";
    this.tbxName.Size = new Size(276, 20);
    this.tbxName.TabIndex = 2;
    this.tbxName.TextChanged += new EventHandler(this.tbxDesign_TextChanged);
    this.lblName.ImeMode = ImeMode.NoControl;
    this.lblName.Location = new Point(6, 49);
    this.lblName.Name = "lblName";
    this.lblName.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblName.TabIndex = 9;
    this.lblName.Text = "Наименование";
    this.tbxDesign.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxDesign.Location = new Point(124, 19);
    this.tbxDesign.Name = "tbxDesign";
    this.tbxDesign.Size = new Size(276, 20);
    this.tbxDesign.TabIndex = 1;
    this.tbxDesign.TextChanged += new EventHandler(this.tbxDesign_TextChanged);
    this.lblDesign.ImeMode = ImeMode.NoControl;
    this.lblDesign.Location = new Point(6, 23);
    this.lblDesign.Name = "lblDesign";
    this.lblDesign.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblDesign.TabIndex = 7;
    this.lblDesign.Text = "Обозначение";
    this.grbArticle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grbArticle.Controls.Add((Control) this.btnArticle);
    this.grbArticle.Controls.Add((Control) this.tbxArtName);
    this.grbArticle.Controls.Add((Control) this.label1);
    this.grbArticle.Enabled = false;
    this.grbArticle.Location = new Point(18, 173);
    this.grbArticle.Name = "grbArticle";
    this.grbArticle.Size = new Size(412, 53);
    this.grbArticle.TabIndex = 6;
    this.grbArticle.TabStop = false;
    this.grbArticle.Text = "Привязка к изделию";
    this.btnArticle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnArticle.FlatStyle = FlatStyle.System;
    this.btnArticle.ImeMode = ImeMode.NoControl;
    this.btnArticle.Location = new Point(376, 17);
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
    this.tbxArtName.Size = new Size(250, 20);
    this.tbxArtName.TabIndex = 1;
    this.label1.ImeMode = ImeMode.NoControl;
    this.label1.Location = new Point(6, 23);
    this.label1.Name = "label1";
    this.label1.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.label1.TabIndex = 13;
    this.label1.Text = "Изделие";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.chbArticleLink);
    this.Controls.Add((Control) this.grbRouteTemplate);
    this.Controls.Add((Control) this.grbArticle);
    this.Name = nameof (RouteTemplateObjectCreatorControl);
    this.Size = new Size(449, 246);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.grbRouteTemplate.ResumeLayout(false);
    this.grbRouteTemplate.PerformLayout();
    this.grbArticle.ResumeLayout(false);
    this.grbArticle.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
