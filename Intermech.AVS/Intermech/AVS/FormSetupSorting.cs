// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.FormSetupSorting
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса FormSetupSorting </summary>
public class FormSetupSorting : ExtForm
{
  private ToolTipController _toolTipEditMode;
  private Button _btnOK;
  private Button _btnCancel;
  private ToolTipController _toolTipReadMode;
  private ImageList imageList1;
  private FormAVSCommonPropertiesTreeList treeList1;
  private TreeListColumn treeListColumn1;
  private UserControlSortingSetup _userControlSortingSetup;
  private IContainer components;
  private long _specificationObjectId = -1;
  private long _settingsHolderObjID;
  private int _settingsHolderObjType = -1;
  private InheritanceSettingsLevel settingsLevel;
  protected long _templateObjectID = -1;
  public SortSchema _sortSchema;
  private bool _loaded;
  private HybridDictionary _settingLevelToTreeNode = new HybridDictionary();
  private bool _needToAutoCheckIn;
  private SettingsStructure _settingsStructure;
  private List<Triple> _tripleList;
  public List<int> _objTypes;
  public List<int> _relTypes;
  private bool inView;

  public FormSetupSorting()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1518);
  }

  public FormSetupSorting(
    long settingsHolderObjID,
    SettingsStructure settingsStructure,
    List<int> relTypes)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1518);
    this.SettingsStructure = settingsStructure;
    this._userControlSortingSetup.SpecificationTemplateObjectId = settingsHolderObjID;
    this.RelTypes = relTypes;
    this.SettingsHolderObjID = settingsHolderObjID;
  }

  public void AddCustomAttributes(List<AVSColumnScheme> customColumnSchemes)
  {
    this._userControlSortingSetup.AddCustomAttributes(customColumnSchemes);
  }

  public FormSetupSorting(
    long settingsHolderObjID,
    VedomostiSettingsStructure settingsStructure,
    List<Triple> tripleList,
    List<int> objTypes,
    List<int> relTypes)
  {
    this.InitializeComponent();
    this.SettingsStructure = settingsStructure == null ? (SettingsStructure) VedomostiSettingsStructure.Instance : (SettingsStructure) settingsStructure;
    this.TripleList = tripleList;
    this.ObjTypes = objTypes;
    this.RelTypes = relTypes;
    this.SettingsHolderObjID = settingsHolderObjID;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1518);
  }

  public FormSetupSorting(
    long settingsHolderObjID,
    int settingsHolderObjType,
    long templateID,
    SettingsStructure settingsStructure,
    List<int> relTypes)
  {
    this.InitializeComponent();
    this.SettingsStructure = settingsStructure;
    this.RelTypes = relTypes;
    this._templateObjectID = templateID;
    this._userControlSortingSetup.SpecificationTemplateObjectId = templateID;
    this._settingsHolderObjType = settingsHolderObjType;
    this.SettingsHolderObjID = settingsHolderObjID;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1518);
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormSetupSorting));
    this._toolTipEditMode = new ToolTipController(this.components);
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this._toolTipReadMode = new ToolTipController(this.components);
    this.imageList1 = new ImageList(this.components);
    this.treeList1 = new FormAVSCommonPropertiesTreeList();
    this.treeListColumn1 = new TreeListColumn();
    this._userControlSortingSetup = new UserControlSortingSetup();
    this.treeList1.BeginInit();
    this.SuspendLayout();
    this._toolTipEditMode.Active = false;
    this._toolTipEditMode.Style = new ViewStyle("ToolTip style");
    this._btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Enabled = false;
    this._btnOK.FlatStyle = FlatStyle.System;
    this._btnOK.Location = new Point(452, 437);
    this._btnOK.Name = "_btnOK";
    this._btnOK.Size = new Size(121, 27);
    this._btnOK.TabIndex = 3;
    this._btnOK.Text = "ОК";
    this._toolTipEditMode.SetToolTip((Control) this._btnOK, "Сохранить изменения и закрыть диалог");
    this._btnOK.Click += new EventHandler(this._btnOK_Click);
    this._btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.FlatStyle = FlatStyle.System;
    this._btnCancel.Location = new Point(579, 437);
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Size = new Size(121, 27);
    this._btnCancel.TabIndex = 4;
    this._btnCancel.Text = "Отмена";
    this._toolTipEditMode.SetToolTip((Control) this._btnCancel, "Отменить изменения и закрыть диалог");
    this._toolTipReadMode.SetToolTip((Control) this._btnCancel, "Закрыть диалог");
    this._btnCancel.Click += new EventHandler(this._btnOK_Click);
    this._toolTipReadMode.Style = new ViewStyle("ToolTip style");
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "");
    this.imageList1.Images.SetKeyName(1, "");
    this.treeList1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.treeList1.BehaviorOptions = BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.ResizeNodes | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoNodeHeight | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this.treeList1.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeList1.Location = new Point(30, 8);
    this.treeList1.Name = "treeList1";
    this.treeList1.BeginUnboundLoad();
    this.treeList1.AppendNode((object) new object[1]
    {
      (object) "Общие настройки"
    }, -1, -1, -1, 0);
    this.treeList1.AppendNode((object) new object[1]
    {
      (object) "Настройки шаблона конструкторского документа"
    }, 0);
    this.treeList1.AppendNode((object) new object[1]
    {
      (object) "Настройки конструкторского документа"
    }, 1);
    this.treeList1.EndUnboundLoad();
    this.treeList1.PreviewLineCount = 3;
    this.treeList1.RowHeight = 19;
    this.treeList1.SelectImageList = this.imageList1;
    this.treeList1.Size = new Size(645, 62);
    this.treeList1.StateImageList = this.imageList1;
    this.treeList1.Styles.AddReplace("TreeLine", (object) new ViewStyle("TreeLine", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.ControlDark));
    this.treeList1.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ControlDark, SystemColors.Window));
    this.treeList1.TabIndex = 5;
    this.treeList1.TreeLineStyle = LineStyle.None;
    this.treeList1.UncheckedStateIndex = 4610;
    this.treeList1.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowFocusedFrame;
    this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    this.treeListColumn1.Caption = "treeListColumn1";
    this.treeListColumn1.FieldName = "treeListColumn1";
    this.treeListColumn1.Name = "treeListColumn1";
    this.treeListColumn1.Options = ColumnOptions.CanResized | ColumnOptions.CanFocused;
    this.treeListColumn1.VisibleIndex = 0;
    this.treeListColumn1.Width = 500;
    this._userControlSortingSetup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._userControlSortingSetup.Location = new Point(0, 76);
    this._userControlSortingSetup.MinimumSize = new Size(655, 320);
    this._userControlSortingSetup.Name = "_userControlSortingSetup";
    this._userControlSortingSetup.ObjTypes = (List<int>) null;
    this._userControlSortingSetup.RelTypes = (List<int>) null;
    this._userControlSortingSetup.SettingsStructure = (SettingsStructure) null;
    this._userControlSortingSetup.Size = new Size(704, 388);
    this._userControlSortingSetup.SortSchema = (SortSchema) null;
    this._userControlSortingSetup.SpecificationObjectId = -1L;
    this._userControlSortingSetup.SpecificationTemplateObjectId = -1L;
    this._userControlSortingSetup.TabIndex = 6;
    this._userControlSortingSetup.TripleList = (List<Triple>) null;
    this._userControlSortingSetup.OnChangedEvent += new EventHandler(this._userControlSortingSetup_OnChangedEvent);
    this.AcceptButton = (IButtonControl) this._btnOK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.ClientSize = new Size(709, 476);
    this.Controls.Add((Control) this.treeList1);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.Controls.Add((Control) this._userControlSortingSetup);
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(675, 443);
    this.Name = nameof (FormSetupSorting);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Text = "Настройка сортировки записей";
    this.Closing += new CancelEventHandler(this.FormSetupSorting_Closing);
    this.Closed += new EventHandler(this.FormSetupSorting_Closed);
    this.Load += new EventHandler(this.FormSetupSorting_Load);
    this.treeList1.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Идентификатор объекта, в атрибуте которого храняться настройки </summary>
  public long SettingsHolderObjID
  {
    get => this._settingsHolderObjID;
    set
    {
      this._settingsHolderObjID = value;
      this.InitSortSchema();
    }
  }

  /// <summary> Идентификатор объекта для которого открыта спецификация </summary>
  public long SpecificationObjectId
  {
    get => this._specificationObjectId;
    set
    {
      this._specificationObjectId = value;
      this._userControlSortingSetup.SpecificationObjectId = value;
    }
  }

  private SettingsStructure SettingsStructure
  {
    get => this._settingsStructure;
    set
    {
      this._settingsStructure = value;
      this._userControlSortingSetup.SettingsStructure = this._settingsStructure;
    }
  }

  /// <summary> Список возможных заголовков (разделов) ведомостей </summary>
  private List<Triple> TripleList
  {
    get => this._tripleList;
    set
    {
      this._tripleList = value;
      this._userControlSortingSetup.TripleList = this._tripleList;
    }
  }

  /// <summary> Список типов объектов, которые могут присутствовать в ведомости </summary>
  public List<int> ObjTypes
  {
    get => this._objTypes;
    set
    {
      this._objTypes = value;
      this._userControlSortingSetup.ObjTypes = this._objTypes;
    }
  }

  /// <summary> Список типов связей, которые могут присутствовать в ведомости </summary>
  public List<int> RelTypes
  {
    get => this._relTypes;
    set
    {
      this._relTypes = value;
      this._userControlSortingSetup.RelTypes = this._relTypes;
    }
  }

  /// <summary> Инициализировать схему сортировки </summary>
  public void InitSortSchema()
  {
    this.LockControls();
    try
    {
      this._userControlSortingSetup.SettingsStructure = this._settingsStructure;
      this._userControlSortingSetup.TripleList = this._tripleList;
      this._userControlSortingSetup.ObjTypes = this._objTypes;
      this._userControlSortingSetup.RelTypes = this._relTypes;
      this._userControlSortingSetup.SpecificationTemplateObjectId = this._templateObjectID;
      this._userControlSortingSetup.SettingsHolderObjectId = this._settingsHolderObjID;
      this._sortSchema = this.LoadSortingSchema();
      this._userControlSortingSetup.SortSchema = this._sortSchema;
      this.Changed = false;
      this.RefreshReadOnly();
      this.UpdateControls(true);
      this.RaiseOnInitDataEvent((object) this._sortSchema);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>
  /// Создание объекта "Схема сортировки спецификации" применимую к объекту, для которого открыта данный диалог
  /// </summary>
  /// <returns>Объект "Схема сортировки спецификации"</returns>
  public SortSchema LoadSortingSchema()
  {
    Guid guid = Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this.SettingsHolderObjID);
      this._settingsHolderObjType = objectInfo.ObjectTypeID;
      guid = objectInfo.VersionGuid;
    }
    this.settingsLevel = AVSDocumentsSettings.GetSettingsLevel(guid, this._settingsHolderObjType);
    if (this._settingsStructure == null)
    {
      AVSDocumentTypeSettings settingsForTemplate = AVSDocumentsSettings.Instance.GetDocumentTypeSettingsForTemplate(guid, out InheritanceSettingsLevel _);
      if (settingsForTemplate != null)
      {
        this._settingsStructure = settingsForTemplate.SettingsInheritanceStructure;
      }
      else
      {
        AVSDocumentTypeSettings typeForDbObjectType = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(this._settingsHolderObjType, AVSDocumentType.Specification);
        this._settingsStructure = typeForDbObjectType == null ? (SettingsStructure) new UserAVSDocumentSettingsStructure() : typeForDbObjectType.SettingsInheritanceStructure;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (SortSchema) this._settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this._settingsHolderObjID, this._settingsHolderObjType, this._templateObjectID, AvsIDCache.Attr_SortSchema, typeof (SortSchema), this._tripleList);
  }

  public void AddSectionToScheme(Guid sectionGuid, string sectionName, int positionIndex)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._sortSchema?.AddSectionScheme(sessionKeeper.Session, sectionGuid, sectionName, positionIndex);
  }

  public void RemoveSectionFromScheme(Guid sectionGuid)
  {
    this._sortSchema?.RemoveSectionScheme(sectionGuid);
  }

  /// <summary> Переинифиализация выбраного уровня настроек </summary>
  public void InitSelectedLevel()
  {
    this.LockControls();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._userControlSortingSetup.SortSchema.LoadParams(sessionKeeper.Session);
      this._userControlSortingSetup.ReloadSchemaTree();
      this._userControlSortingSetup.Changed = false;
      this._userControlSortingSetup.RefreshReadOnly();
      this._userControlSortingSetup.UpdateControls(true);
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      if (focusedNode == null || !this._userControlSortingSetup.ReadOnly)
        return;
      focusedNode.StateImageIndex = 1;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>
  /// Возвращение в архив всех объектов, которые были взяты на редатирование
  /// </summary>
  public void AutoCheckInAll()
  {
    if (!this._needToAutoCheckIn)
      return;
    foreach (FormSetupSorting.SettingsLevelNodeConnector key in (IEnumerable) this._settingLevelToTreeNode.Keys)
    {
      if (key.NeedToAutoCheckIn)
      {
        SortSchema schemaByLevel = this._sortSchema.GetSchemaByLevel(key.Level);
        if (schemaByLevel != null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject objectActual = sessionKeeper.Session.GetObjectActual(schemaByLevel.OwnerObjectID, false);
            if (objectActual != null)
            {
              if (objectActual.CheckoutBy == sessionKeeper.Session.UserID)
                objectActual.CheckIn();
            }
          }
        }
      }
    }
  }

  /// <summary> Сохранение изменений </summary>
  public void SaveChanges()
  {
    if (this._sortSchema == null)
      return;
    this.LockControls();
    try
    {
      if (this._settingLevelToTreeNode.Keys.Count > 0)
      {
        foreach (FormSetupSorting.SettingsLevelNodeConnector key in (IEnumerable) this._settingLevelToTreeNode.Keys)
        {
          if (key.Changed)
          {
            SortSchema schemaByLevel = this._sortSchema.GetSchemaByLevel(key.Level);
            if (schemaByLevel != null)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                schemaByLevel.SaveParams();
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(schemaByLevel.OwnerObjectID);
                AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(objectInfo.ObjectID, objectInfo.ObjectTypeID, new AttributeValues(AvsIDCache.Attr_SortSchema, (object) null), new AttributeValues(AvsIDCache.Attr_SortSchema, (object) null)));
              }
            }
          }
        }
      }
      else
        this._sortSchema?.SaveParams();
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    this._btnCancel.Text = this.ReadOnly ? "Закрыть" : "Отмена";
    this._btnOK.Enabled = !this.ReadOnly;
    if (this.inView)
    {
      this._btnCancel.Text = "Отмена";
      this._btnOK.Text = "Применить";
      this._btnCancel.Enabled = !this.ReadOnly;
    }
    if (this._toolTipEditMode != null)
    {
      if (this.ReadOnly)
      {
        if (this._toolTipEditMode.Active)
        {
          this._toolTipEditMode.Active = false;
          this._toolTipReadMode.Active = true;
        }
      }
      else if (this._toolTipReadMode.Active)
      {
        this._toolTipReadMode.Active = false;
        this._toolTipEditMode.Active = true;
      }
    }
    if (!this.inView)
      return;
    this._toolTipEditMode?.SetToolTip((Control) this._btnOK, "Сохранить изменения");
    this._toolTipEditMode?.SetToolTip((Control) this._btnCancel, "Отменить изменения");
    this._toolTipReadMode.Active = false;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected override bool GetIsReadOnly() => this._sortSchema == null || this._sortSchema.ReadOnly;

  /// <summary>
  /// Вызывается, когда требуется проверка перед попыткой модификации данных
  /// Например, когда у пользователя необходимо запросить разрешение на взятие
  /// на редактирование некоторого объекта
  /// </summary>
  /// <returns> true если редактирование разрешено </returns>
  protected override bool BeforeObjectEditBegin(ref bool wasUpdated)
  {
    wasUpdated = false;
    if (this._sortSchema == null)
      return false;
    SettingsLevel level = this._userControlSortingSetup.SortSchema.Level;
    FormSetupSorting.SettingsLevelNodeConnector levelNodeConnector = (FormSetupSorting.SettingsLevelNodeConnector) null;
    if (this.treeList1.Nodes.Count > 0)
    {
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      if (focusedNode == null || focusedNode.Tag == null)
        return false;
      levelNodeConnector = (FormSetupSorting.SettingsLevelNodeConnector) focusedNode.GetValue((object) 0);
      if (levelNodeConnector == null)
        return false;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._userControlSortingSetup.SortSchema.OwnerObjectID);
      if (dbObject1 == null || (dbObject1.GetAttributeByID(AvsIDCache.Attr_SortSchema) ?? dbObject1.Attributes.AddAttribute(AvsIDCache.Attr_SortSchema, false)) == null)
        return false;
      if (dbObject1.ObjectID < 0L)
      {
        if (dbObject1.CheckoutBy == sessionKeeper.Session.UserID)
          return true;
        int num = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', взят на редактирование пользователем '{sessionKeeper.Session.GetObject(dbObject1.CheckoutBy).Caption}', редактирование недоступно", "Редактирование схемы нумерации позиций", MessageBoxButtons.OK);
        wasUpdated = true;
        this.InitSelectedLevel();
        return false;
      }
      switch (dbObject1.ObjectModifyMode)
      {
        case ObjectModifyModes.InBase:
        case ObjectModifyModes.CreateVersion:
          return true;
        case ObjectModifyModes.Checkout:
          if (MessageBox.Show($"Взять на редактирование объект '{dbObject1.Caption}'? (После завершения редактирования объект будет возвращен в архив)", "Редактирование схемы нумерации позиций", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return false;
          IDBObject dbObject2 = dbObject1.CheckOut();
          if (dbObject2 == null || dbObject2.CheckoutBy != sessionKeeper.Session.UserID)
            return false;
          this._userControlSortingSetup.SortSchema.SetOwnerObjectID(sessionKeeper.Session, dbObject2.ObjectID);
          wasUpdated = true;
          this.InitSelectedLevel();
          this._needToAutoCheckIn = true;
          if (levelNodeConnector != null)
            levelNodeConnector.NeedToAutoCheckIn = true;
          return true;
        case ObjectModifyModes.CantModify:
          int num1 = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', в атрибутах которого хранится схема сортировки недоступен для редактирования", "Редактирование схемы нумерации позиций", MessageBoxButtons.OK);
          return false;
        default:
          return false;
      }
    }
  }

  /// <summary>Установить форму в вьюшку</summary>
  public void SetInView()
  {
    this.AcceptButton = (IButtonControl) null;
    this.CancelButton = (IButtonControl) null;
    this.inView = true;
  }

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormSetupSorting_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    TreeListNode node1 = (TreeListNode) null;
    int num1 = 0;
    this.treeList1.BeginUnboundLoad();
    try
    {
      this.treeList1.Nodes.Clear();
      int parentNodeId = -1;
      TreeListNode treeListNode = (TreeListNode) null;
      this._settingLevelToTreeNode.Clear();
      foreach (SettingsLevel allLevel in this._settingsStructure.AllLevels)
      {
        if (allLevel.InheritanceLevel >= this.settingsLevel)
        {
          SortSchema schemaByLevel = this._sortSchema?.GetSchemaByLevel(allLevel);
          if (schemaByLevel != null)
          {
            int stateImageIndex = schemaByLevel.ReadOnly ? 1 : -1;
            TreeListNode node2 = this.treeList1.AppendNode((object) null, parentNodeId, -1, -1, stateImageIndex);
            FormSetupSorting.SettingsLevelNodeConnector levelNodeConnector = new FormSetupSorting.SettingsLevelNodeConnector(allLevel, node2, this._settingsHolderObjType);
            if (allLevel.InheritanceLevel == InheritanceSettingsLevel.Template)
              levelNodeConnector.Caption = $"{levelNodeConnector.Caption} \"{DBHelper.GetObjCaption(schemaByLevel.OwnerObjectID)}\"";
            node2.SetValue((object) 0, (object) levelNodeConnector);
            this._settingLevelToTreeNode.Add((object) levelNodeConnector, (object) node2);
            ++parentNodeId;
            ++num1;
            if (treeListNode != null)
              treeListNode.Expanded = true;
            treeListNode = node2;
            node1 = node2;
            node2.Tag = (object) allLevel;
          }
        }
      }
    }
    finally
    {
      this.treeList1.EndUnboundLoad();
    }
    if (num1 >= 2)
    {
      int num2 = num1 * 20 + 2;
      if (num2 != this.treeList1.Height)
      {
        int num3 = this.treeList1.Height - num2;
        this.treeList1.Height = num2;
        this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - num3);
        UserControlSortingSetup controlSortingSetup = this._userControlSortingSetup;
        Point location = this._userControlSortingSetup.Location;
        int x = location.X;
        location = this._userControlSortingSetup.Location;
        int y = location.Y - num3;
        Point point = new Point(x, y);
        controlSortingSetup.Location = point;
      }
      foreach (TreeListNode node3 in this.treeList1.Nodes)
        node3.Expanded = true;
      Application.DoEvents();
    }
    else if (this.treeList1.Visible)
    {
      UserControlSortingSetup controlSortingSetup1 = this._userControlSortingSetup;
      Point location = this._userControlSortingSetup.Location;
      int x = location.X;
      location = this._userControlSortingSetup.Location;
      int num4 = location.Y - this.treeList1.Height;
      location = this.treeList1.Location;
      int y1 = location.Y;
      int y2 = num4 - y1;
      Point point = new Point(x, y2);
      controlSortingSetup1.Location = point;
      UserControlSortingSetup controlSortingSetup2 = this._userControlSortingSetup;
      int width1 = this._userControlSortingSetup.Size.Width;
      int num5 = this._userControlSortingSetup.Size.Height + this.treeList1.Height;
      location = this.treeList1.Location;
      int y3 = location.Y;
      int height1 = num5 + y3;
      Size size = new Size(width1, height1);
      controlSortingSetup2.Size = size;
      Size clientSize = this.ClientSize;
      int width2 = clientSize.Width;
      clientSize = this.ClientSize;
      int num6 = clientSize.Height - this.treeList1.Height;
      location = this.treeList1.Location;
      int y4 = location.Y;
      int height2 = num6 - y4;
      this.ClientSize = new Size(width2, height2);
      this.treeList1.Visible = false;
    }
    if (node1 != null)
      this.treeList1.SetFocusedNode(node1);
    this._loaded = true;
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormSetupSorting_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary> Был выбран другой уровень настроек сортировки записей </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    if (e.Node == null || !this._loaded || this._sortSchema == null)
      return;
    object obj = e.Node.GetValue((object) 0);
    if (obj == null || !(obj is FormSetupSorting.SettingsLevelNodeConnector))
      return;
    SortSchema schemaByLevel = this._sortSchema.GetSchemaByLevel(((FormSetupSorting.SettingsLevelNodeConnector) obj).Level);
    if (schemaByLevel == null)
      return;
    this.LockControls();
    try
    {
      if (!schemaByLevel.Changed && schemaByLevel.ParentLevel != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          schemaByLevel.LoadDefaultSchema(sessionKeeper.Session);
      }
      this._userControlSortingSetup.SortSchema = schemaByLevel;
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void _userControlSortingSetup_OnChangedEvent(object sender, EventArgs e)
  {
    if (this._sortSchema == null)
      return;
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return;
    FormSetupSorting.SettingsLevelNodeConnector levelNodeConnector = (FormSetupSorting.SettingsLevelNodeConnector) focusedNode.GetValue((object) 0);
    if (levelNodeConnector == null)
      return;
    levelNodeConnector.Changed = true;
    focusedNode.StateImageIndex = 0;
  }

  private void FormSetupSorting_Closing(object sender, CancelEventArgs e)
  {
    if (!this.ReadOnly && this.DialogResult == DialogResult.OK)
      this.SaveChanges();
    this.AutoCheckInAll();
    if (!this.inView)
      return;
    e.Cancel = true;
    this.InitSortSchema();
    this.FormSetupSorting_Load((object) null, EventArgs.Empty);
  }

  private void _btnOK_Click(object sender, EventArgs e)
  {
    if (!(sender is Button button))
      return;
    this.DialogResult = button.DialogResult;
    this.Close();
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      return this._userControlSortingSetup.CancelButtonRightEdge > 0 ? this._userControlSortingSetup.CancelButtonRightEdge + 1 : this.Size.Width - (this._btnCancel.Location.X + this._btnCancel.Size.Width);
    }
  }

  private class SettingsLevelNodeConnector
  {
    private SettingsLevel _level;
    private TreeListNode _node;
    private string _caption = string.Empty;
    private bool _needToAutoCheckIn;
    private bool _changed;

    public SettingsLevelNodeConnector(SettingsLevel level, TreeListNode node, int objType)
    {
      this._level = level;
      this._node = node;
      this.LoadCaption(objType);
    }

    /// <summary> Ссылка на уровнень настроек </summary>
    public SettingsLevel Level
    {
      get => this._level;
      set => this._level = value;
    }

    /// <summary> Ссылка на ветку дерева уровней настроек </summary>
    public TreeListNode Node
    {
      get => this._node;
      set => this._node = value;
    }

    /// <summary> Признак того, что объект-хранитель уровня настроек необходимо вернуть в архив после завершения работы с ним </summary>
    public bool NeedToAutoCheckIn
    {
      get => this._needToAutoCheckIn;
      set => this._needToAutoCheckIn = value;
    }

    public bool Changed
    {
      get => this._changed;
      set => this._changed = value;
    }

    public string Caption
    {
      get => this._caption;
      set => this._caption = value;
    }

    private void LoadCaption(int objType)
    {
      this._caption = this.Level == null ? string.Empty : this.Level.LevelName;
    }

    /// <summary> Преобразование в строку </summary>
    /// <returns> Описание уровня настроек </returns>
    public override string ToString() => this._caption;
  }
}
