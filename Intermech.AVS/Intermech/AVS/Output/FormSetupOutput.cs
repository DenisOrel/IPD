// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.FormSetupOutput
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Output;

/// <summary> Описание класса FormSetupOutput </summary>
public class FormSetupOutput : ExtForm
{
  private IContainer components;
  private Button _BtnOK;
  private Button _BtnCancel;
  private ToolTipController _EditModeToolTip;
  private UserControlSetupOutput _userControlSetupOutput;
  private ImageList imageList1;
  private TreeList tlLevelsTreeList;
  private TreeListColumn treeListColumn;
  private ToolTipController _ReadModeToolTip;
  private SettingsStructure settingsStructure;
  private long _settingsHolderObjID;
  protected long _templateObjectID = -1;
  public OutputAttributeMappingScheme _outputAttributeMappingScheme;
  private bool _loaded;
  private HybridDictionary _settingLevelToTreeNode = new HybridDictionary();
  private bool _needToAutoCheckIn;
  public static bool ShowDocumentOutline = true;
  private bool inView;

  public FormSetupOutput()
  {
    this.InitializeComponent();
    this._userControlSetupOutput.OwnerForm = (Form) this;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2910);
  }

  public FormSetupOutput(long settingsHolderObjID)
  {
    this.InitializeComponent();
    this._userControlSetupOutput.OwnerForm = (Form) this;
    this._userControlSetupOutput.OnDocumentOutlineVisibleChanged += (EventHandler) ((s, e) => FormSetupOutput.ShowDocumentOutline = this._userControlSetupOutput.ShowDocumentOutline);
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2910);
    this.SettingsHolderObjID = settingsHolderObjID;
    this._templateObjectID = settingsHolderObjID;
  }

  public FormSetupOutput(SettingsStructure settingsStructure, long settingsHolderObjID)
  {
    this.InitializeComponent();
    this._userControlSetupOutput.OwnerForm = (Form) this;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2910);
    this.settingsStructure = settingsStructure;
    this.SettingsHolderObjID = settingsHolderObjID;
  }

  public FormSetupOutput(
    SettingsStructure settingsStructure,
    long settingsHolderObjID,
    int settingsHolderObjType,
    long templateID)
  {
    this._userControlSetupOutput.OwnerForm = (Form) this;
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2910);
    this.settingsStructure = settingsStructure;
    this._templateObjectID = templateID;
    this.SettingsHolderObjID = settingsHolderObjID;
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модифицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormSetupOutput));
    this._EditModeToolTip = new ToolTipController(this.components);
    this._BtnOK = new Button();
    this._BtnCancel = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this._userControlSetupOutput = new UserControlSetupOutput();
    this.imageList1 = new ImageList(this.components);
    this.tlLevelsTreeList = new TreeList();
    this.treeListColumn = new TreeListColumn();
    this.tlLevelsTreeList.BeginInit();
    this.SuspendLayout();
    this._EditModeToolTip.Active = false;
    this._EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(522, 377);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 1;
    this._BtnOK.Text = "ОК";
    this._EditModeToolTip.SetToolTip((Control) this._BtnOK, "Сохранить изменения и закрыть диалог");
    this._BtnOK.MouseClick += new MouseEventHandler(this._BtnOK_MouseClick);
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(649, 377);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 2;
    this._BtnCancel.Text = "Отмена";
    this._EditModeToolTip.SetToolTip((Control) this._BtnCancel, "Отменить изменения и закрыть диалог");
    this._ReadModeToolTip.SetToolTip((Control) this._BtnCancel, "Закрыть диалог");
    this._BtnCancel.MouseClick += new MouseEventHandler(this._BtnOK_MouseClick);
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this._userControlSetupOutput.Dock = DockStyle.Fill;
    this._userControlSetupOutput.Location = new Point(0, 0);
    this._userControlSetupOutput.MinimumSize = new Size(615, 365);
    this._userControlSetupOutput.Name = "_userControlSetupOutput";
    this._userControlSetupOutput.ShowActionButtons = true;
    this._userControlSetupOutput.Size = new Size(779, 416);
    this._userControlSetupOutput.TabIndex = 0;
    this._userControlSetupOutput.OnActionButtonClicked += new MouseEventHandler(this._BtnOK_MouseClick);
    this._userControlSetupOutput.OnChangedEvent += new EventHandler(this.userControlSetupOutput_OnChangedEvent);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "");
    this.imageList1.Images.SetKeyName(1, "");
    this.tlLevelsTreeList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tlLevelsTreeList.BehaviorOptions = BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.ResizeNodes | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoNodeHeight | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this.tlLevelsTreeList.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn
    });
    this.tlLevelsTreeList.Location = new Point(12, 12);
    this.tlLevelsTreeList.Name = "tlLevelsTreeList";
    this.tlLevelsTreeList.BeginUnboundLoad();
    this.tlLevelsTreeList.AppendNode((object) new object[1]
    {
      (object) "Общие настройки"
    }, -1, -1, -1, 0);
    this.tlLevelsTreeList.AppendNode((object) new object[1]
    {
      (object) "Настройки шаблона конструкторского документа"
    }, 0);
    this.tlLevelsTreeList.AppendNode((object) new object[1]
    {
      (object) "Настройки конструкторского документа"
    }, 1);
    this.tlLevelsTreeList.EndUnboundLoad();
    this.tlLevelsTreeList.PreviewLineCount = 3;
    this.tlLevelsTreeList.RowHeight = 19;
    this.tlLevelsTreeList.Size = new Size(601, 62);
    this.tlLevelsTreeList.StateImageList = this.imageList1;
    this.tlLevelsTreeList.Styles.AddReplace("TreeLine", (object) new ViewStyle("TreeLine", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.ControlDark));
    this.tlLevelsTreeList.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ControlDark, SystemColors.Window));
    this.tlLevelsTreeList.TabIndex = 6;
    this.tlLevelsTreeList.TreeLineStyle = LineStyle.None;
    this.tlLevelsTreeList.UncheckedStateIndex = 4610;
    this.tlLevelsTreeList.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowFocusedFrame;
    this.tlLevelsTreeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    this.tlLevelsTreeList.Visible = false;
    this.treeListColumn.Caption = "treeListColumn1";
    this.treeListColumn.FieldName = "treeListColumn1";
    this.treeListColumn.Name = "treeListColumn";
    this.treeListColumn.Options = ColumnOptions.CanResized | ColumnOptions.CanFocused;
    this.treeListColumn.VisibleIndex = 0;
    this.treeListColumn.Width = 500;
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(779, 416);
    this.Controls.Add((Control) this.tlLevelsTreeList);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.Controls.Add((Control) this._userControlSetupOutput);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(642, 445);
    this.Name = nameof (FormSetupOutput);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Text = "Настройка вывода атрибутов";
    this.Closed += new EventHandler(this.FormSetupOutput_Closed);
    this.FormClosing += new FormClosingEventHandler(this.FormSetupOutput_FormClosing);
    this.Load += new EventHandler(this.FormSetupOutput_Load);
    this.tlLevelsTreeList.EndInit();
    this.ResumeLayout(false);
  }

  public InheritanceSettingsLevel SettingsLevel { get; set; }

  /// <summary> Идентификатор объекта, в атрибуте которого хранятся настройки </summary>
  public long SettingsHolderObjID
  {
    get => this._settingsHolderObjID;
    set => this._settingsHolderObjID = value;
  }

  internal void LoadControlData()
  {
    this._userControlSetupOutput.BuildTrees();
    this._userControlSetupOutput.ShowDocumentOutline = FormSetupOutput.ShowDocumentOutline;
    this.Changed = false;
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    this._BtnCancel.Text = this.ReadOnly ? "Закрыть" : "Отмена";
    this._BtnOK.Enabled = !this.ReadOnly;
    if (this.inView)
    {
      this._BtnCancel.Text = "Отмена";
      this._BtnOK.Text = "Применить";
      this._BtnCancel.Enabled = !this.ReadOnly;
    }
    if (this._EditModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this._EditModeToolTip.Active)
        {
          this._EditModeToolTip.Active = false;
          this._ReadModeToolTip.Active = true;
        }
      }
      else if (this._ReadModeToolTip.Active)
      {
        this._ReadModeToolTip.Active = false;
        this._EditModeToolTip.Active = true;
      }
    }
    if (!this.inView)
      return;
    this._EditModeToolTip?.SetToolTip((Control) this._BtnOK, "Сохранить изменения");
    this._EditModeToolTip?.SetToolTip((Control) this._BtnCancel, "Отменить изменения");
    this._ReadModeToolTip.Active = false;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected override bool GetIsReadOnly() => this._userControlSetupOutput.GetIsReadOnly();

  /// <summary>
  /// Вызывается, когда требуется проверка перед попыткой модификации данных
  /// Например, когда у пользователя необходимо запросить разрешение на взятие
  /// на редактирование некоторого объекта
  /// </summary>
  /// <returns> true если редактирование разрешено </returns>
  protected override bool BeforeObjectEditBegin(ref bool wasUpdated)
  {
    wasUpdated = false;
    if (this._outputAttributeMappingScheme == null)
      return false;
    TreeListNode focusedNode = this.tlLevelsTreeList.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return false;
    Intermech.Interfaces.AVS.SettingsLevel level = this._userControlSetupOutput.OutputAttributeMappingScheme.Level;
    FormSetupOutput.SettingsLevelNodeConnector levelNodeConnector = (FormSetupOutput.SettingsLevelNodeConnector) focusedNode.GetValue((object) 0);
    if (levelNodeConnector == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectActual(this._userControlSetupOutput.OutputAttributeMappingScheme.OwnerObjectID, true);
      if (dbObject.ObjectID < 0L && dbObject.CheckoutBy != sessionKeeper.Session.UserID)
      {
        int num = (int) MessageBox.Show($"Объект '{dbObject.Caption}', взят на редактирование пользователем '{sessionKeeper.Session.GetObject(dbObject.CheckoutBy).Caption}', редактирование недоступно", "Редактирование схемы вывода атрибутов", MessageBoxButtons.OK);
        wasUpdated = true;
        this.InitSelectedLevel();
        return false;
      }
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_OutputMappingSchema);
      bool flag = false;
      switch (dbObject.ObjectModifyMode)
      {
        case ObjectModifyModes.InBase:
        case ObjectModifyModes.CreateVersion:
          flag = true;
          break;
        case ObjectModifyModes.Checkout:
          if (dbObject != null && dbObject.CheckoutBy == 0L)
          {
            dbObject = dbObject.CheckOut();
            this._needToAutoCheckIn = true;
            flag = true;
          }
          if (dbObject != null && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
          {
            flag = true;
            break;
          }
          break;
        case ObjectModifyModes.CantModify:
          int num1 = (int) MessageBox.Show($"Объект '{dbObject.Caption}', в атрибутах которого хранится схема вывода атрибутов, недоступен для редактирования", "Редактирование схемы вывода атрибутов", MessageBoxButtons.OK);
          break;
      }
      if (flag)
      {
        if (attributeById == null)
          dbObject.Attributes.AddAttribute(AvsIDCache.Attr_OutputMappingSchema, true);
        this._userControlSetupOutput.OutputAttributeMappingScheme.OwnerObjectID = dbObject.ObjectID;
        wasUpdated = true;
        this.InitSelectedLevel();
        levelNodeConnector.NeedToAutoCheckIn = this._needToAutoCheckIn;
      }
      return flag;
    }
  }

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormSetupOutput_Load(object sender, EventArgs e)
  {
    TreeListNode node1 = (TreeListNode) null;
    this.tlLevelsTreeList.BeginUnboundLoad();
    int num1 = 0;
    try
    {
      this.tlLevelsTreeList.Nodes.Clear();
      int parentNodeId = -1;
      TreeListNode treeListNode = (TreeListNode) null;
      this._settingLevelToTreeNode.Clear();
      foreach (Intermech.Interfaces.AVS.SettingsLevel level in ((IEnumerable<Intermech.Interfaces.AVS.SettingsLevel>) this.settingsStructure.AllLevels).Where<Intermech.Interfaces.AVS.SettingsLevel>((Func<Intermech.Interfaces.AVS.SettingsLevel, bool>) (lv => this.SettingsLevel == (InheritanceSettingsLevel) 0 || lv.InheritanceLevel == this.SettingsLevel)))
      {
        OutputAttributeMappingScheme schemaByLevel = this._outputAttributeMappingScheme?.GetSchemaByLevel(level);
        int stateImageIndex = schemaByLevel == null ? -1 : (schemaByLevel.ReadOnly ? 1 : -1);
        TreeListNode node2 = this.tlLevelsTreeList.AppendNode((object) null, parentNodeId, -1, -1, stateImageIndex);
        FormSetupOutput.SettingsLevelNodeConnector levelNodeConnector = new FormSetupOutput.SettingsLevelNodeConnector(level, node2);
        if (level.InheritanceLevel == InheritanceSettingsLevel.Template)
          levelNodeConnector.Caption = $"{levelNodeConnector.Caption} \"{DBHelper.GetObjCaption(schemaByLevel != null ? schemaByLevel.OwnerObjectID : -1L)}\"";
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
        node2.Tag = (object) level;
      }
    }
    finally
    {
      this.tlLevelsTreeList.EndUnboundLoad();
    }
    if (num1 >= 2)
    {
      int num2 = num1 * 20 + 2;
      if (num2 != this.tlLevelsTreeList.Height)
      {
        int num3 = this.tlLevelsTreeList.Height - num2;
        this.tlLevelsTreeList.Height = num2;
        Size clientSize = this.ClientSize;
        int width = clientSize.Width;
        clientSize = this.ClientSize;
        int height = clientSize.Height - num3;
        this.ClientSize = new Size(width, height);
        this._userControlSetupOutput.Location = new Point(this._userControlSetupOutput.Location.X, this._userControlSetupOutput.Location.Y - num3);
        this._userControlSetupOutput.Height += num3;
      }
      foreach (TreeListNode node3 in this.tlLevelsTreeList.Nodes)
        node3.Expanded = true;
      Application.DoEvents();
    }
    else if (this.tlLevelsTreeList.Visible)
    {
      this.tlLevelsTreeList.Visible = false;
      int num4 = this.tlLevelsTreeList.Height + this.tlLevelsTreeList.Location.Y;
      this._userControlSetupOutput.Location = new Point(this._userControlSetupOutput.Location.X, this._userControlSetupOutput.Location.Y - num4);
      this._userControlSetupOutput.Height += num4;
      this.Size = new Size(this.Size.Width, this.Size.Height - num4);
    }
    if (node1 != null)
      this.tlLevelsTreeList.SetFocusedNode(node1);
    this._loaded = true;
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormSetupOutput_Closed(object sender, EventArgs e)
  {
  }

  /// <summary>Установить форму в вьюшку</summary>
  public void SetInView()
  {
    this.AcceptButton = (IButtonControl) null;
    this.CancelButton = (IButtonControl) null;
    this._BtnOK.Visible = this._BtnCancel.Visible = false;
    this.inView = true;
  }

  /// <summary> Инициализировать схему вывода атрибутов </summary>
  public void InitOutputMapping()
  {
    this.LockControls();
    try
    {
      this._outputAttributeMappingScheme = this.LoadOutputMapping();
      this._userControlSetupOutput.TemplateObjectId = this._settingsHolderObjID;
      this._userControlSetupOutput.OutputAttributeMappingScheme = this._outputAttributeMappingScheme;
      this.Changed = false;
      this.RefreshReadOnly();
      this.UpdateControls(true);
      this.RaiseOnInitDataEvent((object) this._outputAttributeMappingScheme);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>
  /// Создание объекта "Схема вывода атрибутов" применимую к объекту, для которого открыта данный диалог
  /// </summary>
  /// <returns>Объект "Схема вывода атрибутов"</returns>
  public OutputAttributeMappingScheme LoadOutputMapping()
  {
    return OutputAttributeMappingScheme.CreateOrLoad(this.SettingsHolderObjID, ref this.settingsStructure);
  }

  /// <summary> Переинициализация выбранного уровня настроек </summary>
  public void InitSelectedLevel()
  {
    this.LockControls();
    try
    {
      this._userControlSetupOutput.OutputAttributeMappingScheme.LoadParams();
      this._userControlSetupOutput.Changed = false;
      this._userControlSetupOutput.RefreshReadOnly();
      this._userControlSetupOutput.UpdateControls(true);
      TreeListNode focusedNode = this.tlLevelsTreeList.FocusedNode;
      if (focusedNode == null || !this._userControlSetupOutput.ReadOnly)
        return;
      focusedNode.StateImageIndex = 1;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>
  /// Возвращение в архив всех объектов, которые были взяты на редактирование
  /// </summary>
  public void AutoCheckInAll()
  {
    if (!this._needToAutoCheckIn)
      return;
    foreach (FormSetupOutput.SettingsLevelNodeConnector key in (IEnumerable) this._settingLevelToTreeNode.Keys)
    {
      if (key.NeedToAutoCheckIn)
      {
        OutputAttributeMappingScheme schemaByLevel = this._outputAttributeMappingScheme.GetSchemaByLevel(key.Level);
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
    if (this._outputAttributeMappingScheme == null)
      return;
    this.LockControls();
    try
    {
      this._userControlSetupOutput.UpdateScheme();
      foreach (FormSetupOutput.SettingsLevelNodeConnector key in (IEnumerable) this._settingLevelToTreeNode.Keys)
      {
        if (key.Changed)
        {
          OutputAttributeMappingScheme schemaByLevel = this._outputAttributeMappingScheme.GetSchemaByLevel(key.Level);
          if (schemaByLevel != null)
          {
            schemaByLevel.SaveParams();
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject objectActual = sessionKeeper.Session.GetObjectActual(schemaByLevel.OwnerObjectID, true);
              if (AVSPlugin.NotificationService != null)
                AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(objectActual.ObjectID, objectActual.ObjectType, new AttributeValues(AvsIDCache.Attr_OutputMappingSchema, (object) null), new AttributeValues(AvsIDCache.Attr_OutputMappingSchema, (object) null)));
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
    if (e.Node == null || !this._loaded || this._outputAttributeMappingScheme == null)
      return;
    object obj = e.Node.GetValue((object) 0);
    if (obj == null || !(obj is FormSetupOutput.SettingsLevelNodeConnector))
      return;
    OutputAttributeMappingScheme schemaByLevel = this._outputAttributeMappingScheme.GetSchemaByLevel(((FormSetupOutput.SettingsLevelNodeConnector) obj).Level);
    if (schemaByLevel == null)
      return;
    this.LockControls();
    try
    {
      this._userControlSetupOutput.OutputAttributeMappingScheme = schemaByLevel;
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void userControlSetupOutput_OnChangedEvent(object sender, EventArgs e)
  {
    if (this._outputAttributeMappingScheme == null)
      return;
    TreeListNode focusedNode = this.tlLevelsTreeList.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return;
    FormSetupOutput.SettingsLevelNodeConnector levelNodeConnector = (FormSetupOutput.SettingsLevelNodeConnector) focusedNode.GetValue((object) 0);
    if (levelNodeConnector == null)
      return;
    levelNodeConnector.Changed = true;
    focusedNode.StateImageIndex = 0;
  }

  private void FormSetupOutput_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.ReadOnly && this.DialogResult == DialogResult.OK)
      this.SaveChanges();
    this.AutoCheckInAll();
    this.Changed = false;
    if (!this.inView)
      return;
    e.Cancel = true;
    this.FormSetupOutput_Load((object) null, EventArgs.Empty);
  }

  private void _BtnOK_MouseClick(object sender, MouseEventArgs e)
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
      return this._userControlSetupOutput.CancelButtonRightEdge > 0 ? this._userControlSetupOutput.CancelButtonRightEdge + 1 : this.Size.Width - (this._BtnCancel.Location.X + this._BtnCancel.Size.Width);
    }
  }

  private class SettingsLevelNodeConnector
  {
    private Intermech.Interfaces.AVS.SettingsLevel _level;
    private TreeListNode _node;
    private string _caption = string.Empty;
    private bool _needToAutoCheckIn;
    private bool _changed;

    public SettingsLevelNodeConnector(Intermech.Interfaces.AVS.SettingsLevel level, TreeListNode node)
    {
      this._level = level;
      this._node = node;
      this.LoadCaption();
    }

    /// <summary> Ссылка на уровень настроек </summary>
    public Intermech.Interfaces.AVS.SettingsLevel Level
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
