// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SetupNumberingSchemaForm
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса SpecificationNumberingSchemaForm </summary>
public class SetupNumberingSchemaForm : ExtForm
{
  private IContainer components;
  private ToolTipController EditModeToolTip;
  private SpecifNumberingControlFull specifNumberingControlFull;
  public Button btnCancel;
  public Button btnOK;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;
  private ImageList imageList1;
  private ToolTipController ReadModeToolTip;
  private SettingsStructure settingsStructure;
  protected long _SchemaObjectID = -1;
  protected long _TemplateObjectID = -1;
  private int _settingsHolderObjType = -1;
  private InheritanceSettingsLevel settingsLevel;
  protected SpecifNumberingFull _SpecifNumberingFull;
  private bool _Loaded;
  private Hashtable _ServiceTable = new Hashtable();
  private bool _NeedToAutoCheckIn;
  private bool inView;

  public SetupNumberingSchemaForm(SettingsStructure settingsStructure, long ObjectId)
  {
    this.InitializeComponent();
    this.settingsStructure = settingsStructure;
    this.specifNumberingControlFull.SpecificationTemplateObjectId = ObjectId;
    this.SchemaObjectID = ObjectId;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1522);
  }

  public SetupNumberingSchemaForm(
    SettingsStructure settingsStructure,
    long specificationID,
    long templateID)
  {
    this.InitializeComponent();
    this.settingsStructure = settingsStructure;
    this._settingsHolderObjType = AvsIDCache.ObjType_Specification;
    this.specifNumberingControlFull.SpecificationTemplateObjectId = templateID;
    this._TemplateObjectID = templateID;
    this.SchemaObjectID = specificationID;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1522);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SetupNumberingSchemaForm));
    this.EditModeToolTip = new ToolTipController(this.components);
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.ReadModeToolTip = new ToolTipController(this.components);
    this.specifNumberingControlFull = new SpecifNumberingControlFull();
    this.treeList1 = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.imageList1 = new ImageList(this.components);
    this.treeList1.BeginInit();
    this.SuspendLayout();
    this.EditModeToolTip.Active = false;
    this.EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.FlatStyle = FlatStyle.System;
    this.btnCancel.Location = new Point(586, 284);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 4;
    this.btnCancel.Text = "Отмена";
    this.EditModeToolTip.SetToolTip((Control) this.btnCancel, "Отменить правки, произведенные в настройках нумерации");
    this.ReadModeToolTip.SetToolTip((Control) this.btnCancel, "Закрыть диалог");
    this.btnCancel.Click += new EventHandler(this.btnOK_Click);
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Enabled = false;
    this.btnOK.FlatStyle = FlatStyle.System;
    this.btnOK.Location = new Point(459, 284);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 3;
    this.btnOK.Text = "ОК";
    this.EditModeToolTip.SetToolTip((Control) this.btnOK, "Сохранить изменения настроек нумерации позиций");
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this.specifNumberingControlFull.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.specifNumberingControlFull.AutoScroll = true;
    this.specifNumberingControlFull.Location = new Point(0, 64 /*0x40*/);
    this.specifNumberingControlFull.Name = "specifNumberingControlFull";
    this.specifNumberingControlFull.Size = new Size(716, 247);
    this.specifNumberingControlFull.SpecificationTemplateObjectId = -1L;
    this.specifNumberingControlFull.TabIndex = 1;
    this.specifNumberingControlFull.OnChangedEvent += new EventHandler(this.specifNumberingControlFull_OnChangedEvent);
    this.treeList1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.treeList1.BehaviorOptions = BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.ResizeNodes | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoNodeHeight | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this.treeList1.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeList1.Location = new Point(5, 4);
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
    this.treeList1.Size = new Size(705, 62);
    this.treeList1.StateImageList = this.imageList1;
    this.treeList1.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ControlDark, SystemColors.Window));
    this.treeList1.TabIndex = 0;
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
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "");
    this.imageList1.Images.SetKeyName(1, "");
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(716, 323);
    this.Controls.Add((Control) this.treeList1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.specifNumberingControlFull);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(732, 327);
    this.Name = nameof (SetupNumberingSchemaForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Параметры автоматической расстановки позиций";
    this.Closing += new CancelEventHandler(this.SetupNumberingSchemaForm_Closing);
    this.Closed += new EventHandler(this.SetupNumberingSchemaForm_Closed);
    this.Load += new EventHandler(this.SpecificationNumberingSchemaForm_Load);
    this.treeList1.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Идентификатор объекта-владельца настроек нумерации спецификации </summary>
  protected long SchemaObjectID
  {
    get => this._SchemaObjectID;
    set
    {
      this._SchemaObjectID = value;
      this.InitNumberingSchema();
    }
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      return this.specifNumberingControlFull.CancelButtonRightEdge > 0 ? this.specifNumberingControlFull.CancelButtonRightEdge + 1 : this.Size.Width - (this.btnCancel.Location.X + this.btnCancel.Size.Width);
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    this.btnCancel.Text = this.ReadOnly ? "Закрыть" : "Отмена";
    this.btnOK.Enabled = !this.ReadOnly;
    if (this.inView)
    {
      this.btnCancel.Text = "Отмена";
      this.btnOK.Text = "Применить";
      this.btnCancel.Enabled = !this.ReadOnly;
    }
    this.treeList1.BackColor = this.ReadOnly ? Color.WhiteSmoke : SystemColors.Window;
    if (this.EditModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this.EditModeToolTip.Active)
        {
          this.EditModeToolTip.Active = false;
          this.ReadModeToolTip.Active = true;
        }
      }
      else if (this.ReadModeToolTip.Active)
      {
        this.ReadModeToolTip.Active = false;
        this.EditModeToolTip.Active = true;
      }
    }
    if (!this.inView)
      return;
    this.EditModeToolTip?.SetToolTip((Control) this.btnOK, "Сохранить изменения");
    this.EditModeToolTip?.SetToolTip((Control) this.btnCancel, "Отменить изменения");
    this.ReadModeToolTip.Active = false;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected override bool GetIsReadOnly()
  {
    return this._SpecifNumberingFull == null || this._SpecifNumberingFull.ReadOnly;
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
    if (this._SpecifNumberingFull == null)
      return false;
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return false;
    SetupNumberingSchemaForm._LevelConnector levelConnector = (SetupNumberingSchemaForm._LevelConnector) this._ServiceTable[(object) this.specifNumberingControlFull.SpecifNumberingFull.Level];
    if (levelConnector == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.specifNumberingControlFull.SpecifNumberingFull.OwnerObjectID);
      if (dbObject1 == null)
        return false;
      if (dbObject1.GetAttributeByID(AvsIDCache.Attr_NumberingSchema) == null)
        return !this.specifNumberingControlFull.SpecifNumberingFull.ReadOnly;
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
          this.specifNumberingControlFull.SpecifNumberingFull._OwnerObjectID = dbObject2.ObjectID;
          wasUpdated = true;
          this.InitSelectedLevel();
          this._NeedToAutoCheckIn = true;
          levelConnector.NeedToAutoCheckIn = true;
          return true;
        case ObjectModifyModes.CantModify:
          int num1 = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', в атрибутах которого хранится схема нумерации позиций недоступен для редактирования", "Редактирование схемы нумерации позиций", MessageBoxButtons.OK);
          return false;
        default:
          return false;
      }
    }
  }

  /// <summary> Инициализация данных </summary>
  public void InitNumberingSchema()
  {
    this.LockControls();
    try
    {
      this._SpecifNumberingFull = this.LoadNumberingSchema();
      this.specifNumberingControlFull.SpecifNumberingFull = this._SpecifNumberingFull;
      this.Changed = false;
      this.RefreshReadOnly();
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Переинифиализация выбраного уровня настроек </summary>
  public void InitSelectedLevel()
  {
    this.LockControls();
    try
    {
      this.specifNumberingControlFull.SpecifNumberingFull.LoadParams();
      this.specifNumberingControlFull.Changed = false;
      this.specifNumberingControlFull.RefreshReadOnly();
      this.specifNumberingControlFull.UpdateControls(true);
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      if (focusedNode == null || !this.specifNumberingControlFull.ReadOnly)
        return;
      focusedNode.StateImageIndex = 1;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>
  /// Создание объекта "Схема нумерации позиций в спецификации" применимую к объекту, для которого открыта данная панель
  /// </summary>
  /// <returns>Объект "Схема нумерации позиций в спецификации"</returns>
  public SpecifNumberingFull LoadNumberingSchema()
  {
    Guid guid = Guid.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._SchemaObjectID);
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
      return (SpecifNumberingFull) this.settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this._SchemaObjectID, this._settingsHolderObjType, this._TemplateObjectID, AvsIDCache.Attr_NumberingSchema, typeof (SpecifNumberingFull));
  }

  /// <summary> Сохранение изменений </summary>
  public void SaveChanges()
  {
    if (this._SpecifNumberingFull == null)
      return;
    this.LockControls();
    try
    {
      foreach (SetupNumberingSchemaForm._LevelConnector levelConnector in (IEnumerable) this._ServiceTable.Values)
      {
        if (levelConnector.Changed)
          this._SpecifNumberingFull.GetSchemaByLevel(levelConnector.Level).SaveParams();
      }
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
    if (!this._NeedToAutoCheckIn)
      return;
    foreach (SetupNumberingSchemaForm._LevelConnector levelConnector in (IEnumerable) this._ServiceTable.Values)
    {
      if (levelConnector.NeedToAutoCheckIn)
      {
        SpecifNumberingFull schemaByLevel = this._SpecifNumberingFull.GetSchemaByLevel(levelConnector.Level);
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

  /// <summary>Был выбран другой уровень настроек нумерации позиций</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    if (e.Node == null || e.Node.Tag == null || !this._Loaded)
      return;
    SpecifNumberingFull schemaByLevel = this._SpecifNumberingFull.GetSchemaByLevel((SettingsLevel) e.Node.Tag);
    if (schemaByLevel == null)
      return;
    this.LockControls();
    try
    {
      if (this.specifNumberingControlFull.SpecifNumberingFull != null)
      {
        if (!schemaByLevel.NonNumneringRazdelsChanged)
          schemaByLevel._NonNumneringRazdels = schemaByLevel.LoadDefaultNonNumneringRazdels();
        if (!schemaByLevel.SpecifRazdelNumbering.Changed)
          schemaByLevel.SpecifRazdelNumbering.LoadDefaultSchema();
        if (!schemaByLevel.CompareDesignationSchema.Changed)
          schemaByLevel.CompareDesignationSchema.LoadDefaultSchema();
      }
      this.specifNumberingControlFull.SpecifNumberingFull = schemaByLevel;
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SpecificationNumberingSchemaForm_Load(object sender, EventArgs e)
  {
    TreeListNode node1 = (TreeListNode) null;
    this.treeList1.BeginUnboundLoad();
    int num1 = 0;
    try
    {
      this.treeList1.Nodes.Clear();
      int parentNodeId = -1;
      TreeListNode treeListNode1 = (TreeListNode) null;
      this._ServiceTable.Clear();
      foreach (SettingsLevel allLevel in this.settingsStructure.AllLevels)
      {
        if (allLevel.InheritanceLevel >= this.settingsLevel)
        {
          SpecifNumberingFull schemaByLevel = this._SpecifNumberingFull != null ? this._SpecifNumberingFull.GetSchemaByLevel(allLevel) : (SpecifNumberingFull) null;
          if (schemaByLevel != null)
          {
            int stateImageIndex = schemaByLevel.ReadOnly ? 1 : -1;
            string str = allLevel.LevelName;
            if (allLevel.InheritanceLevel == InheritanceSettingsLevel.Template)
              str = $"{str} \"{DBHelper.GetObjCaption(schemaByLevel.OwnerObjectID)}\"";
            TreeListNode treeListNode2 = this.treeList1.AppendNode((object) new object[1]
            {
              (object) str
            }, parentNodeId, -1, -1, stateImageIndex);
            this._ServiceTable.Add((object) allLevel, (object) new SetupNumberingSchemaForm._LevelConnector(allLevel));
            ++parentNodeId;
            ++num1;
            if (treeListNode1 != null)
            {
              treeListNode1.Expanded = true;
              Application.DoEvents();
            }
            treeListNode1 = treeListNode2;
            node1 = treeListNode2;
            treeListNode2.Tag = (object) allLevel;
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
        Size clientSize = this.ClientSize;
        int width = clientSize.Width;
        clientSize = this.ClientSize;
        int height = clientSize.Height - num3;
        this.ClientSize = new Size(width, height);
        this.specifNumberingControlFull.Location = new Point(this.specifNumberingControlFull.Location.X, this.specifNumberingControlFull.Location.Y - num3);
        this.specifNumberingControlFull.Height += num3;
      }
      foreach (TreeListNode node2 in this.treeList1.Nodes)
        node2.Expanded = true;
      Application.DoEvents();
    }
    else if (this.treeList1.Visible)
    {
      this.treeList1.Visible = false;
      int num4 = this.treeList1.Height + this.treeList1.Location.Y;
      this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height - num4);
      this.specifNumberingControlFull.Location = new Point(this.specifNumberingControlFull.Location.X, this.specifNumberingControlFull.Location.Y - num4);
      this.specifNumberingControlFull.Height += num4;
    }
    if (node1 != null)
      this.treeList1.SetFocusedNode(node1);
    this._Loaded = true;
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SetupNumberingSchemaForm_Closed(object sender, EventArgs e)
  {
  }

  /// <summary>Вызывается при изменении данных</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void specifNumberingControlFull_OnChangedEvent(object sender, EventArgs e)
  {
    if (!this.specifNumberingControlFull.Changed)
      return;
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return;
    SetupNumberingSchemaForm._LevelConnector levelConnector = (SetupNumberingSchemaForm._LevelConnector) this._ServiceTable[(object) (SettingsLevel) focusedNode.Tag];
    if (levelConnector == null)
      return;
    levelConnector.Changed = true;
    focusedNode.StateImageIndex = 0;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (!(sender is Button button))
      return;
    this.DialogResult = button.DialogResult;
    this.Close();
  }

  /// <summary>Установить форму в вьюшку</summary>
  public void SetInView()
  {
    this.AcceptButton = (IButtonControl) null;
    this.CancelButton = (IButtonControl) null;
    this.inView = true;
  }

  private void SetupNumberingSchemaForm_Closing(object sender, CancelEventArgs e)
  {
    if (!this.ReadOnly && this.DialogResult == DialogResult.OK)
      this.SaveChanges();
    this.AutoCheckInAll();
    if (!this.inView)
      return;
    e.Cancel = true;
    this.InitNumberingSchema();
    this.SpecificationNumberingSchemaForm_Load((object) null, EventArgs.Empty);
  }

  private class _LevelConnector
  {
    public bool NeedToAutoCheckIn;
    public bool Changed;
    public SettingsLevel Level;

    public _LevelConnector(SettingsLevel level) => this.Level = level;
  }
}
