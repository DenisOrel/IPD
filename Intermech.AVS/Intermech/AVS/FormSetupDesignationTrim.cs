// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.FormSetupDesignationTrim
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса FormSetupSkipPositions </summary>
public class FormSetupDesignationTrim : ExtForm
{
  private IContainer components;
  private Button _BtnOK;
  private Button _BtnCancel;
  private ToolTipController _EditModeToolTip;
  private ImageList imageList1;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;
  private UserControlDesignationTrim userControlDesignationTrim;
  public Button _btnReset;
  private ToolTipController _ReadModeToolTip;
  private SettingsStructure settingsStructure;
  private long _settingsHolderObjID;
  private int _settingsHolderObjType = -1;
  private InheritanceSettingsLevel settingsLevel;
  protected long _templateObjectID = -1;
  public DesignationTrimSchema DesignationTrimSchema;
  private bool _loaded;
  private HybridDictionary _settingLevelToTreeNode = new HybridDictionary();
  private bool _needToAutoCheckIn;

  public FormSetupDesignationTrim()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1524);
    this.userControlDesignationTrim._btnReset.Visible = false;
    this._btnReset.Enabled = this.userControlDesignationTrim._btnReset.Enabled;
    this.userControlDesignationTrim._btnReset.EnabledChanged += new EventHandler(this._btnReset_EnabledChanged);
  }

  public FormSetupDesignationTrim(SettingsStructure settingsStructure, long settingsHolderObjID)
    : this(settingsStructure, settingsHolderObjID, -1L)
  {
  }

  public FormSetupDesignationTrim(
    SettingsStructure settingsStructure,
    long settingsHolderObjID,
    long templateID)
    : this()
  {
    this.settingsStructure = settingsStructure;
    this._templateObjectID = templateID;
    this.SettingsHolderObjID = settingsHolderObjID;
    this._settingsHolderObjType = AvsIDCache.ObjType_Specification;
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.components?.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormSetupDesignationTrim));
    this._EditModeToolTip = new ToolTipController(this.components);
    this._BtnOK = new Button();
    this._BtnCancel = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this.imageList1 = new ImageList(this.components);
    this.treeList1 = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.userControlDesignationTrim = new UserControlDesignationTrim();
    this._btnReset = new Button();
    this.treeList1.BeginInit();
    this.SuspendLayout();
    this._EditModeToolTip.Active = false;
    this._EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(323, 471);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 1;
    this._BtnOK.Text = "ОК";
    this._EditModeToolTip.SetToolTip((Control) this._BtnOK, "Сохранить изменения и закрыть диалог");
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(450, 471);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 2;
    this._BtnCancel.Text = "Отмена";
    this._EditModeToolTip.SetToolTip((Control) this._BtnCancel, "Отменить изменения и закрыть диалог");
    this._ReadModeToolTip.SetToolTip((Control) this._BtnCancel, "Закрыть диалог");
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
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
    this.treeList1.Location = new Point(14, 14);
    this.treeList1.Name = "treeList1";
    this.treeList1.PreviewLineCount = 3;
    this.treeList1.RowHeight = 19;
    this.treeList1.Size = new Size(554, 62);
    this.treeList1.StateImageList = this.imageList1;
    this.treeList1.Styles.AddReplace("TreeLine", (object) new ViewStyle("TreeLine", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.ControlDark));
    this.treeList1.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ControlDark, SystemColors.Window));
    this.treeList1.TabIndex = 6;
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
    this.userControlDesignationTrim.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.userControlDesignationTrim.DesignationTrimSchema = (DesignationTrimSchema) null;
    this.userControlDesignationTrim.Location = new Point(1, 78);
    this.userControlDesignationTrim.Name = "userControlDesignationTrim";
    this.userControlDesignationTrim.Size = new Size(580, 420);
    this.userControlDesignationTrim.TabIndex = 7;
    this.userControlDesignationTrim.OnChangedEvent += new EventHandler(this.userControl_OnChangedEvent);
    this._btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._btnReset.Enabled = false;
    this._btnReset.FlatStyle = FlatStyle.System;
    this._btnReset.Location = new Point(12, 471);
    this._btnReset.Name = "_btnReset";
    this._btnReset.Size = new Size(121, 27);
    this._btnReset.TabIndex = 19;
    this._btnReset.Text = "По умолчанию";
    this._btnReset.Click += new EventHandler(this._btnReset_Click);
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(583, 510);
    this.Controls.Add((Control) this._btnReset);
    this.Controls.Add((Control) this.treeList1);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.Controls.Add((Control) this.userControlDesignationTrim);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormSetupDesignationTrim);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Text = "Настройка обозначений исполнений";
    this.Closed += new EventHandler(this.Form_Closed);
    this.FormClosing += new FormClosingEventHandler(this.Form_FormClosing);
    this.Load += new EventHandler(this.Form_Load);
    this.treeList1.EndInit();
    this.ResumeLayout(false);
  }

  private void _btnReset_EnabledChanged(object sender, EventArgs e)
  {
    this._btnReset.Enabled = this.userControlDesignationTrim._btnReset.Enabled;
  }

  /// <summary> Идентификатор объекта, в атрибуте которого храняться настройки </summary>
  public long SettingsHolderObjID
  {
    get => this._settingsHolderObjID;
    set
    {
      this._settingsHolderObjID = value;
      this.InitSettings();
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    this._BtnCancel.Text = this.ReadOnly ? "Закрыть" : "Отмена";
    this._BtnOK.Enabled = !this.ReadOnly;
    if (this._EditModeToolTip == null)
      return;
    if (this.ReadOnly)
    {
      if (!this._EditModeToolTip.Active)
        return;
      this._EditModeToolTip.Active = false;
      this._ReadModeToolTip.Active = true;
    }
    else
    {
      if (!this._ReadModeToolTip.Active)
        return;
      this._ReadModeToolTip.Active = false;
      this._EditModeToolTip.Active = true;
    }
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected override bool GetIsReadOnly()
  {
    return this.DesignationTrimSchema == null || this.DesignationTrimSchema.ReadOnly;
  }

  /// <summary>
  /// Вызывается, когда требуется проверка перед попыткой модификации данных
  /// Например, когда у пользователя необходимо запросить разрешение на взятие
  /// на редактирование некоторого объекта
  /// </summary>
  /// <returns> true если редактирование разрешено </returns>
  protected override bool BeforeObjectEditBegin(ref bool wasUpdated)
  {
    wasUpdated = false;
    if (this.DesignationTrimSchema == null)
      return false;
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return false;
    SettingsLevel level = this.userControlDesignationTrim.DesignationTrimSchema.Level;
    FormSetupDesignationTrim.SettingsLevelNodeConnector levelNodeConnector = (FormSetupDesignationTrim.SettingsLevelNodeConnector) focusedNode.GetValue((object) 0);
    if (levelNodeConnector == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.userControlDesignationTrim.DesignationTrimSchema.OwnerObjectID);
      if (dbObject1 == null)
        return false;
      dbObject1.GetAttributeByID(AvsIDCache.Attr_DesignationTrimSchema);
      if (dbObject1.ObjectID < 0L)
      {
        if (dbObject1.CheckoutBy == sessionKeeper.Session.UserID)
          return true;
        int num = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', взят на редактирование пользователем '{sessionKeeper.Session.GetObject(dbObject1.CheckoutBy).Caption}', редактирование недоступно", "Редактирование схемы пропуска строк", MessageBoxButtons.OK);
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
          if (MessageBox.Show($"Взять на редактирование объект '{dbObject1.Caption}'? (После завершения редактирования объект будет возвращен в архив)", "Редактирование схемы пропуска строк", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return false;
          IDBObject dbObject2 = dbObject1.CheckOut();
          if (dbObject2 == null || dbObject2.CheckoutBy != sessionKeeper.Session.UserID)
            return false;
          this.userControlDesignationTrim.DesignationTrimSchema.OwnerObjectID = dbObject2.ObjectID;
          wasUpdated = true;
          this.InitSelectedLevel();
          this._needToAutoCheckIn = true;
          levelNodeConnector.NeedToAutoCheckIn = true;
          return true;
        case ObjectModifyModes.CantModify:
          int num1 = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', в атрибутах которого хранится схема пропуска строк недоступен для редактирования", "Редактирование схемы пропуска строк", MessageBoxButtons.OK);
          return false;
        default:
          return false;
      }
    }
  }

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Form_Load(object sender, EventArgs e)
  {
    TreeListNode node1 = (TreeListNode) null;
    this.treeList1.BeginUnboundLoad();
    int num1 = 0;
    try
    {
      this.treeList1.Nodes.Clear();
      int parentNodeId = -1;
      TreeListNode treeListNode = (TreeListNode) null;
      foreach (SettingsLevel allLevel in this.settingsStructure.AllLevels)
      {
        if (allLevel.InheritanceLevel >= this.settingsLevel)
        {
          DesignationTrimSchema schemaByLevel = this.DesignationTrimSchema != null ? this.DesignationTrimSchema.GetSchemaByLevel(allLevel) : (DesignationTrimSchema) null;
          int stateImageIndex = schemaByLevel == null ? -1 : (schemaByLevel.ReadOnly ? 1 : -1);
          TreeListNode node2 = this.treeList1.AppendNode((object) null, parentNodeId, -1, -1, stateImageIndex);
          FormSetupDesignationTrim.SettingsLevelNodeConnector levelNodeConnector = new FormSetupDesignationTrim.SettingsLevelNodeConnector(allLevel, node2);
          if (allLevel.InheritanceLevel == InheritanceSettingsLevel.Template)
            levelNodeConnector.Caption = $"{levelNodeConnector.Caption} \"{DBHelper.GetObjCaption(schemaByLevel.OwnerObjectID)}\"";
          node2.SetValue((object) 0, (object) levelNodeConnector);
          this._settingLevelToTreeNode.Add((object) levelNodeConnector, (object) node2);
          ++parentNodeId;
          ++num1;
          if (treeListNode != null)
          {
            treeListNode.Expanded = true;
            Application.DoEvents();
          }
          treeListNode = node2;
          node1 = node2;
          node2.Tag = (object) allLevel;
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
        Size clientSize = this.ClientSize;
        int width = clientSize.Width;
        clientSize = this.ClientSize;
        int height = clientSize.Height - num3;
        this.ClientSize = new Size(width, height);
        this.userControlDesignationTrim.Location = new Point(this.userControlDesignationTrim.Location.X, this.userControlDesignationTrim.Location.Y - num3);
        this.userControlDesignationTrim.Height += num3;
      }
      foreach (TreeListNode node3 in this.treeList1.Nodes)
        node3.Expanded = true;
      Application.DoEvents();
    }
    else if (this.treeList1.Visible)
    {
      this.treeList1.Visible = false;
      int num4 = this.treeList1.Height + this.treeList1.Location.Y;
      this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - num4);
      this.userControlDesignationTrim.Location = new Point(this.userControlDesignationTrim.Location.X, this.userControlDesignationTrim.Location.Y - num4);
      this.userControlDesignationTrim.Height += num4;
    }
    if (node1 != null)
      this.treeList1.SetFocusedNode(node1);
    this._loaded = true;
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Form_Closed(object sender, EventArgs e)
  {
  }

  /// <summary> Инициализировать схему сортировки </summary>
  public void InitSettings()
  {
    this.LockControls();
    try
    {
      this.DesignationTrimSchema = this.LoadSettings();
      this.userControlDesignationTrim.DesignationTrimSchema = this.DesignationTrimSchema;
      this.Changed = false;
      this.RefreshReadOnly();
      this.UpdateControls(true);
      this.RaiseOnInitDataEvent((object) this.DesignationTrimSchema);
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
  public DesignationTrimSchema LoadSettings()
  {
    Guid guid = Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this.SettingsHolderObjID);
      this._settingsHolderObjType = objectInfo.ObjectTypeID;
      guid = objectInfo.VersionGuid;
    }
    this.settingsLevel = AVSDocumentsSettings.GetSettingsLevel(guid, this._settingsHolderObjType);
    if (this.settingsStructure == null)
    {
      AVSDocumentTypeSettings settingsForTemplate = AVSDocumentsSettings.Instance.GetDocumentTypeSettingsForTemplate(guid, out InheritanceSettingsLevel _);
      if (settingsForTemplate != null)
      {
        this.settingsStructure = settingsForTemplate.SettingsInheritanceStructure;
      }
      else
      {
        AVSDocumentTypeSettings typeForDbObjectType = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(this._settingsHolderObjType, AVSDocumentType.Specification);
        this.settingsStructure = typeForDbObjectType == null ? (SettingsStructure) new UserAVSDocumentSettingsStructure() : typeForDbObjectType.SettingsInheritanceStructure;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (DesignationTrimSchema) this.settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this._settingsHolderObjID, this._settingsHolderObjType, this._templateObjectID, AvsIDCache.Attr_DesignationTrimSchema, typeof (DesignationTrimSchema));
  }

  /// <summary> Переинициализация выбранного уровня настроек </summary>
  public void InitSelectedLevel()
  {
    this.LockControls();
    try
    {
      this.userControlDesignationTrim.DesignationTrimSchema.LoadParams();
      this.userControlDesignationTrim.Changed = false;
      this.userControlDesignationTrim.RefreshReadOnly();
      this.userControlDesignationTrim.UpdateControls(true);
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      if (focusedNode == null || !this.userControlDesignationTrim.ReadOnly)
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
    foreach (FormSetupDesignationTrim.SettingsLevelNodeConnector key in (IEnumerable) this._settingLevelToTreeNode.Keys)
    {
      if (key.NeedToAutoCheckIn)
      {
        DesignationTrimSchema schemaByLevel = this.DesignationTrimSchema.GetSchemaByLevel(key.Level);
        if (schemaByLevel != null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(schemaByLevel.OwnerObjectID);
            if (dbObject != null)
            {
              if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
                dbObject.CheckIn();
            }
          }
        }
      }
    }
  }

  /// <summary> Сохранение изменений </summary>
  public void SaveChanges()
  {
    if (this.DesignationTrimSchema == null)
      return;
    this.LockControls();
    try
    {
      foreach (FormSetupDesignationTrim.SettingsLevelNodeConnector key in (IEnumerable) this._settingLevelToTreeNode.Keys)
      {
        if (key.Changed)
        {
          DesignationTrimSchema schemaByLevel = this.DesignationTrimSchema.GetSchemaByLevel(key.Level);
          if (schemaByLevel != null)
          {
            schemaByLevel.SaveParams();
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(schemaByLevel.OwnerObjectID);
              if (AVSPlugin.NotificationService != null)
                AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(dbObject.ObjectID, dbObject.ObjectType, new AttributeValues(AvsIDCache.Attr_DesignationTrimSchema, (object) null), new AttributeValues(AvsIDCache.Attr_DesignationTrimSchema, (object) null)));
            }
          }
        }
      }
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    if (e.Node == null || !this._loaded || this.DesignationTrimSchema == null)
      return;
    object obj = e.Node.GetValue((object) 0);
    if (obj == null || !(obj is FormSetupDesignationTrim.SettingsLevelNodeConnector))
      return;
    DesignationTrimSchema schemaByLevel = this.DesignationTrimSchema.GetSchemaByLevel(((FormSetupDesignationTrim.SettingsLevelNodeConnector) obj).Level);
    if (schemaByLevel == null)
      return;
    this.LockControls();
    try
    {
      this.userControlDesignationTrim.DesignationTrimSchema = schemaByLevel;
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void Form_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.ReadOnly && this.DialogResult == DialogResult.OK)
      this.SaveChanges();
    this.AutoCheckInAll();
  }

  private void userControl_OnChangedEvent(object sender, EventArgs e)
  {
    if (this.DesignationTrimSchema == null)
      return;
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return;
    FormSetupDesignationTrim.SettingsLevelNodeConnector levelNodeConnector = (FormSetupDesignationTrim.SettingsLevelNodeConnector) focusedNode.GetValue((object) 0);
    if (levelNodeConnector == null)
      return;
    levelNodeConnector.Changed = true;
    focusedNode.StateImageIndex = 0;
  }

  private void _btnReset_Click(object sender, EventArgs e)
  {
    if (!this.userControlDesignationTrim._btnReset.Enabled)
      return;
    this.userControlDesignationTrim._btnReset_Click(sender, e);
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      return this.userControlDesignationTrim.CancelButtonRightEdge > 0 ? this.userControlDesignationTrim.CancelButtonRightEdge + 1 : this.Size.Width - (this._BtnCancel.Location.X + this._BtnCancel.Size.Width);
    }
  }

  private class SettingsLevelNodeConnector
  {
    private SettingsLevel _level;
    private TreeListNode _node;
    private string _caption = string.Empty;
    private bool _needToAutoCheckIn;
    private bool _changed;

    public SettingsLevelNodeConnector(SettingsLevel level, TreeListNode node)
    {
      this._level = level;
      this._node = node;
      this.LoadCaption();
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

    private void LoadCaption()
    {
      this._caption = this.Level == null ? string.Empty : this.Level.LevelName;
    }

    /// <summary> Преобразование в строку </summary>
    /// <returns> Описание уровня настроек </returns>
    public override string ToString() => this._caption;
  }
}
