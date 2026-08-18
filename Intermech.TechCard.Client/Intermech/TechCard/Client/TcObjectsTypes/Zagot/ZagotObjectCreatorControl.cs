// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Zagot.ZagotObjectCreatorControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.ObjectCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Zagot;

/// <summary>
/// 
/// </summary>
internal class ZagotObjectCreatorControl : TechObjectCreatorProcRouteSupportControl
{
  /// <summary>Вид детали для заготовки</summary>
  private long _artTypeId;
  /// <summary>Вид заготовки</summary>
  private long _zagotTypeId;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox grbZagot;
  private Button btnPrototype;
  private TextBox tbxPrototype;
  private Label lblPrototype;
  private Button btnArtTypeSelect;
  private TextBox tbxArtType;
  private Label lblArtType;
  private TextBox tbxZagotType;
  private Button btnZagotTypeSelect;
  private Label lblZagotType;
  private TextBox tbxDesign;
  private Label lblDesign;
  private Label lblName;
  private TextBox tbxName;
  private GroupBox grbArticle;
  private Button btnArtSelect;
  private TextBox tbxArtName;
  private Label lblArtName;
  private TextBox tbxMoName;
  private Button btnMoSelect;
  private Label lblMoName;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControlData()
  {
    this._objNameControl = (Control) this.tbxName;
    this._objDesignationControl = (Control) this.tbxDesign;
    this._artNameControl = (Control) this.tbxArtName;
    this._moNameControl = (Control) this.tbxMoName;
    if (this.DesignMode)
      return;
    this.LoadContextObjectData();
  }

  /// <summary>
  /// 
  /// </summary>
  public ZagotObjectCreatorControl() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="creatorExtraParams"></param>
  public ZagotObjectCreatorControl(
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
  private void btnMoSelect_Click(object sender, EventArgs e) => this.SelectProcRoute();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnArtSelect_Click(object sender, EventArgs e) => this.SelectArticle();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxDesign_TextChanged(object sender, EventArgs e) => this.ValidateObjData();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnArtTypeSelect_Click(object sender, EventArgs e) => this.SelectArtType();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnZagotTypeSelect_Click(object sender, EventArgs e) => this.SelectZagotType();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnPrototype_Click(object sender, EventArgs e) => this.SelectPrototype();

  /// <summary>Выбор вида изделия для заготовки</summary>
  private void SelectArtType()
  {
    long artTypeId = this._artTypeId;
    if (!TechcardClientUtils.Attributes.EditObjLinkValue(this.CreatedObject.ObjectTypeID, MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ArtTypeAttrGuid), ref artTypeId))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateArtTypeData(artTypeId, sessionKeeper.Session);
  }

  /// <summary>Выбор вида заготовки</summary>
  private void SelectZagotType()
  {
    long zagotTypeId = this._zagotTypeId;
    if (!TechcardClientUtils.Attributes.EditObjLinkValue(this.CreatedObject.ObjectTypeID, MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ZagTypeAttrGuid), ref zagotTypeId))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateZagotTypeData(zagotTypeId, sessionKeeper.Session);
  }

  /// <summary>Обновление значения маршрута обработки</summary>
  /// <param name="artTypeId"></param>
  /// <param name="session"></param>
  private void UpdateArtTypeData(long artTypeId, IUserSession session)
  {
    if (this._artTypeId == artTypeId)
      return;
    this._artTypeId = artTypeId;
    this.tbxArtType.Text = TechCardConsts.Utils.GetObjectString(this._artTypeId, session);
  }

  /// <summary>Обновление значения маршрута обработки</summary>
  /// <param name="zagotTypeId"></param>
  /// <param name="session"></param>
  private void UpdateZagotTypeData(long zagotTypeId, IUserSession session)
  {
    if (this._zagotTypeId == zagotTypeId)
      return;
    this._zagotTypeId = zagotTypeId;
    this.tbxZagotType.Text = TechCardConsts.Utils.GetObjectString(this._zagotTypeId, session);
  }

  /// <summary>
  /// Загрузка информации о контексте объекта (Изделие / МО)
  /// </summary>
  protected override void LoadContextObjectData()
  {
    base.LoadContextObjectData();
    if (this._articleId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._articleId, false);
      if (dbObject == null)
        return;
      IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.ArtTypeAttrGuid);
      long result;
      if (attributeByGuid1 != null && attributeByGuid1.Value != null && attributeByGuid1.Value != DBNull.Value && long.TryParse(attributeByGuid1.Value.ToString(), out result))
        this.UpdateArtTypeData(result, sessionKeeper.Session);
      IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.ZagTypeAttrGuid);
      if (attributeByGuid2 == null || attributeByGuid2.Value == null || attributeByGuid2.Value == DBNull.Value || !long.TryParse(attributeByGuid2.Value.ToString(), out result))
        return;
      this.UpdateZagotTypeData(result, sessionKeeper.Session);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoLoadObjectData(IDBObject dbObject)
  {
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.ArtTypeAttrGuid);
    if (attributeByGuid1 != null)
    {
      long asInteger = attributeByGuid1.AsInteger;
      if (asInteger != 0L)
        this.UpdateArtTypeData(asInteger, dbObject.Session);
    }
    IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(TechCardConsts.AttributeTypes.ZagTypeAttrGuid);
    if (attributeByGuid2 != null)
    {
      long asInteger = attributeByGuid2.AsInteger;
      if (asInteger != 0L)
        this.UpdateZagotTypeData(asInteger, dbObject.Session);
    }
    base.DoLoadObjectData(dbObject);
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoSaveObjectData(IDBObject dbObject)
  {
    this.CreateObject_CopyPrototypeAttributes(dbObject);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>(4);
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) this.tbxName.Text));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) this.tbxDesign.Text));
    if (this._artTypeId != 0L)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ArtTypeAttrGuid), (object) this._artTypeId));
    if (this._zagotTypeId != 0L)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.ZagTypeAttrGuid), (object) this._zagotTypeId));
    dbObject.SetAttributesValues(attributeValuesList.ToArray());
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
    return ProcRouteZagListViewDlg.ShowDialog(this._articleId, 0L, ref objectId) && objectId != this._moObjectId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void ValidateMoData(IUserSession session)
  {
    if (this.CreatedObject.ObjectTypeID != -1 && this._moObjectId != 0L && TechCardParamsHelper.TechParams.ProcessRoute.UniqueBillet)
    {
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ZagotGUID), LogicalOperators.NONE, 0, false)
      };
      List<TechCardUtils.SostavSortedTreeItem> childSostavTree = TechCardUtils.GetChildSostavTree(this._moObjectId, session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, conditions);
      if (childSostavTree != null && childSostavTree.Count != 0)
      {
        IDBObject dbObject = session.GetObject(this._moObjectId, false);
        this.SetControlErrorMsg((Control) this.tbxMoName, dbObject != null ? string.Format(LocalizationHolder.rm.GetString(sc_19707.ssp_techcard_19709()), (object) dbObject.Caption, (object) dbObject.ObjectID) : string.Format(LocalizationHolder.rm.GetString(sc_19707.ssp_techcard_19708()), (object) this._moObjectId));
        return;
      }
    }
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
      this.RemoveStaledLinks();
      int num = this._prototypeObjId == 0L ? 0 : (this._prototypeObjId != -1L ? 1 : 0);
      this.AutoCheckInMo(session, this._notificationEvents);
    }
    finally
    {
      service?.DisposeSession((object) session.SessionGUID);
    }
    return true;
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
  /// Включить/Выключить отображение группы контролов привязки а Изделию/МО
  /// </summary>
  protected bool GroupArticleVisible
  {
    set => this.grbArticle.Visible = value;
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
    this.grbZagot = new GroupBox();
    this.btnPrototype = new Button();
    this.tbxPrototype = new TextBox();
    this.lblPrototype = new Label();
    this.btnArtTypeSelect = new Button();
    this.tbxArtType = new TextBox();
    this.lblArtType = new Label();
    this.tbxZagotType = new TextBox();
    this.btnZagotTypeSelect = new Button();
    this.lblZagotType = new Label();
    this.tbxDesign = new TextBox();
    this.lblDesign = new Label();
    this.lblName = new Label();
    this.tbxName = new TextBox();
    this.grbArticle = new GroupBox();
    this.btnArtSelect = new Button();
    this.tbxArtName = new TextBox();
    this.lblArtName = new Label();
    this.tbxMoName = new TextBox();
    this.btnMoSelect = new Button();
    this.lblMoName = new Label();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.grbZagot.SuspendLayout();
    this.grbArticle.SuspendLayout();
    this.SuspendLayout();
    this.grbZagot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grbZagot.Controls.Add((Control) this.btnPrototype);
    this.grbZagot.Controls.Add((Control) this.tbxPrototype);
    this.grbZagot.Controls.Add((Control) this.lblPrototype);
    this.grbZagot.Controls.Add((Control) this.btnArtTypeSelect);
    this.grbZagot.Controls.Add((Control) this.tbxArtType);
    this.grbZagot.Controls.Add((Control) this.lblArtType);
    this.grbZagot.Controls.Add((Control) this.tbxZagotType);
    this.grbZagot.Controls.Add((Control) this.btnZagotTypeSelect);
    this.grbZagot.Controls.Add((Control) this.lblZagotType);
    this.grbZagot.Controls.Add((Control) this.tbxDesign);
    this.grbZagot.Controls.Add((Control) this.lblDesign);
    this.grbZagot.Controls.Add((Control) this.lblName);
    this.grbZagot.Controls.Add((Control) this.tbxName);
    this.grbZagot.Location = new Point(9, 3);
    this.grbZagot.Name = "grbZagot";
    this.grbZagot.Size = new Size(400, 154);
    this.grbZagot.TabIndex = 2;
    this.grbZagot.TabStop = false;
    this.btnPrototype.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnPrototype.FlatStyle = FlatStyle.System;
    this.btnPrototype.ImeMode = ImeMode.NoControl;
    this.btnPrototype.Location = new Point(364, 121);
    this.btnPrototype.Name = "btnPrototype";
    this.btnPrototype.Size = new Size(24, 23);
    this.btnPrototype.TabIndex = 36;
    this.btnPrototype.Text = "...";
    this.btnPrototype.Click += new EventHandler(this.btnPrototype_Click);
    this.tbxPrototype.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxPrototype.BackColor = SystemColors.Window;
    this.tbxPrototype.Location = new Point(124, 123);
    this.tbxPrototype.Name = "tbxPrototype";
    this.tbxPrototype.ReadOnly = true;
    this.tbxPrototype.Size = new Size(238, 20);
    this.tbxPrototype.TabIndex = 35;
    this.lblPrototype.ImeMode = ImeMode.NoControl;
    this.lblPrototype.Location = new Point(6, 126);
    this.lblPrototype.Name = "lblPrototype";
    this.lblPrototype.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblPrototype.TabIndex = 34;
    this.lblPrototype.Text = "Прототип";
    this.btnArtTypeSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnArtTypeSelect.FlatStyle = FlatStyle.System;
    this.btnArtTypeSelect.ImeMode = ImeMode.NoControl;
    this.btnArtTypeSelect.Location = new Point(364, 69);
    this.btnArtTypeSelect.Name = "btnArtTypeSelect";
    this.btnArtTypeSelect.Size = new Size(24, 23);
    this.btnArtTypeSelect.TabIndex = 4;
    this.btnArtTypeSelect.Text = "...";
    this.btnArtTypeSelect.Click += new EventHandler(this.btnArtTypeSelect_Click);
    this.tbxArtType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxArtType.BackColor = SystemColors.Window;
    this.tbxArtType.ForeColor = SystemColors.WindowText;
    this.tbxArtType.Location = new Point(124, 71);
    this.tbxArtType.Name = "tbxArtType";
    this.tbxArtType.ReadOnly = true;
    this.tbxArtType.Size = new Size(238, 20);
    this.tbxArtType.TabIndex = 3;
    this.lblArtType.ImeMode = ImeMode.NoControl;
    this.lblArtType.Location = new Point(6, 74);
    this.lblArtType.Name = "lblArtType";
    this.lblArtType.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblArtType.TabIndex = 33;
    this.lblArtType.Text = "Вид детали";
    this.tbxZagotType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxZagotType.BackColor = SystemColors.Window;
    this.tbxZagotType.Location = new Point(124, 97);
    this.tbxZagotType.Name = "tbxZagotType";
    this.tbxZagotType.ReadOnly = true;
    this.tbxZagotType.Size = new Size(238, 20);
    this.tbxZagotType.TabIndex = 5;
    this.btnZagotTypeSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnZagotTypeSelect.FlatStyle = FlatStyle.System;
    this.btnZagotTypeSelect.ImeMode = ImeMode.NoControl;
    this.btnZagotTypeSelect.Location = new Point(364, 95);
    this.btnZagotTypeSelect.Name = "btnZagotTypeSelect";
    this.btnZagotTypeSelect.Size = new Size(24, 23);
    this.btnZagotTypeSelect.TabIndex = 6;
    this.btnZagotTypeSelect.Text = "...";
    this.btnZagotTypeSelect.Click += new EventHandler(this.btnZagotTypeSelect_Click);
    this.lblZagotType.ImeMode = ImeMode.NoControl;
    this.lblZagotType.Location = new Point(6, 100);
    this.lblZagotType.Name = "lblZagotType";
    this.lblZagotType.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblZagotType.TabIndex = 31 /*0x1F*/;
    this.lblZagotType.Text = "Вид заготовки";
    this.tbxDesign.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxDesign.Location = new Point(124, 19);
    this.tbxDesign.Name = "tbxDesign";
    this.tbxDesign.Size = new Size(264, 20);
    this.tbxDesign.TabIndex = 1;
    this.tbxDesign.TextChanged += new EventHandler(this.tbxDesign_TextChanged);
    this.lblDesign.ImeMode = ImeMode.NoControl;
    this.lblDesign.Location = new Point(6, 22);
    this.lblDesign.Name = "lblDesign";
    this.lblDesign.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblDesign.TabIndex = 28;
    this.lblDesign.Text = "Обозначение";
    this.lblName.ImeMode = ImeMode.NoControl;
    this.lblName.Location = new Point(6, 48 /*0x30*/);
    this.lblName.Name = "lblName";
    this.lblName.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblName.TabIndex = 26;
    this.lblName.Text = "Наименование";
    this.tbxName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxName.Location = new Point(124, 45);
    this.tbxName.Name = "tbxName";
    this.tbxName.Size = new Size(264, 20);
    this.tbxName.TabIndex = 2;
    this.grbArticle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grbArticle.Controls.Add((Control) this.btnArtSelect);
    this.grbArticle.Controls.Add((Control) this.tbxArtName);
    this.grbArticle.Controls.Add((Control) this.lblArtName);
    this.grbArticle.Controls.Add((Control) this.tbxMoName);
    this.grbArticle.Controls.Add((Control) this.btnMoSelect);
    this.grbArticle.Controls.Add((Control) this.lblMoName);
    this.grbArticle.Location = new Point(9, 163);
    this.grbArticle.Name = "grbArticle";
    this.grbArticle.Size = new Size(400, 76);
    this.grbArticle.TabIndex = 3;
    this.grbArticle.TabStop = false;
    this.grbArticle.Text = "Привязка к изделию";
    this.btnArtSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnArtSelect.FlatStyle = FlatStyle.System;
    this.btnArtSelect.ImeMode = ImeMode.NoControl;
    this.btnArtSelect.Location = new Point(364, 17);
    this.btnArtSelect.Name = "btnArtSelect";
    this.btnArtSelect.Size = new Size(24, 23);
    this.btnArtSelect.TabIndex = 18;
    this.btnArtSelect.Text = "...";
    this.btnArtSelect.Click += new EventHandler(this.btnArtSelect_Click);
    this.tbxArtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxArtName.BackColor = SystemColors.Window;
    this.tbxArtName.ForeColor = SystemColors.WindowText;
    this.tbxArtName.Location = new Point(124, 19);
    this.tbxArtName.Name = "tbxArtName";
    this.tbxArtName.ReadOnly = true;
    this.tbxArtName.Size = new Size(238, 20);
    this.tbxArtName.TabIndex = 12;
    this.lblArtName.ImeMode = ImeMode.NoControl;
    this.lblArtName.Location = new Point(6, 22);
    this.lblArtName.Name = "lblArtName";
    this.lblArtName.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblArtName.TabIndex = 13;
    this.lblArtName.Text = "Изделие";
    this.tbxMoName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxMoName.BackColor = SystemColors.Window;
    this.tbxMoName.Location = new Point(124, 45);
    this.tbxMoName.Name = "tbxMoName";
    this.tbxMoName.ReadOnly = true;
    this.tbxMoName.Size = new Size(238, 20);
    this.tbxMoName.TabIndex = 6;
    this.btnMoSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnMoSelect.FlatStyle = FlatStyle.System;
    this.btnMoSelect.ImeMode = ImeMode.NoControl;
    this.btnMoSelect.Location = new Point(364, 43);
    this.btnMoSelect.Name = "btnMoSelect";
    this.btnMoSelect.Size = new Size(24, 23);
    this.btnMoSelect.TabIndex = 5;
    this.btnMoSelect.Text = "...";
    this.btnMoSelect.Click += new EventHandler(this.btnMoSelect_Click);
    this.lblMoName.ImeMode = ImeMode.NoControl;
    this.lblMoName.Location = new Point(6, 48 /*0x30*/);
    this.lblMoName.Name = "lblMoName";
    this.lblMoName.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.lblMoName.TabIndex = 7;
    this.lblMoName.Text = "Маршрут обработки";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.grbZagot);
    this.Controls.Add((Control) this.grbArticle);
    this.MinimumSize = new Size(419, 247);
    this.Name = nameof (ZagotObjectCreatorControl);
    this.Size = new Size(419, 247);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.grbZagot.ResumeLayout(false);
    this.grbZagot.PerformLayout();
    this.grbArticle.ResumeLayout(false);
    this.grbArticle.PerformLayout();
    this.ResumeLayout(false);
  }
}
