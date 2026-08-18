// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DynamicGroupHeaderSettingsForm
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

public class DynamicGroupHeaderSettingsForm : ExtForm
{
  private const int HelpTopic = 0;
  private IContainer components;
  private Button _BtnOK;
  private Button _BtnCancel;
  private ToolTipController _EditModeToolTip;
  private UserControlDynamicGroupHeaderSettings _userControlDynamicGroupHeaderSettings;
  private ImageList imageList1;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;
  private ToolTipController _ReadModeToolTip;
  private SettingsStructure settingsStructure;
  private long _settingsHolderObjID;
  private int _settingsHolderObjType = -1;
  private InheritanceSettingsLevel settingsLevel;
  protected long _templateObjectID = -1;
  public DynamicGroupHeaderSettings _dynamicGroupHeaderSettings;
  private bool _loaded;
  private HybridDictionary _settingLevelToTreeNode = new HybridDictionary();
  private bool _needToAutoCheckIn;
  private bool inView;

  public DynamicGroupHeaderSettingsForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 0);
  }

  public DynamicGroupHeaderSettingsForm(
    SettingsStructure settingsStructure,
    long settingsHolderObjID)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 0);
    this.settingsStructure = settingsStructure;
    this.SettingsHolderObjID = settingsHolderObjID;
  }

  public DynamicGroupHeaderSettingsForm(
    SettingsStructure settingsStructure,
    long settingsHolderObjID,
    int settingsHolderObjType,
    long templateID)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 0);
    this.settingsStructure = settingsStructure;
    this._templateObjectID = templateID;
    this._settingsHolderObjType = settingsHolderObjType;
    this.SettingsHolderObjID = settingsHolderObjID;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DynamicGroupHeaderSettingsForm));
    this._EditModeToolTip = new ToolTipController(this.components);
    this._BtnOK = new Button();
    this._BtnCancel = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this._userControlDynamicGroupHeaderSettings = new UserControlDynamicGroupHeaderSettings();
    this.imageList1 = new ImageList(this.components);
    this.treeList1 = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeList1.BeginInit();
    this.SuspendLayout();
    this._EditModeToolTip.Active = false;
    this._EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(365, 365);
    this._BtnOK.MaximumSize = new Size(121, 27);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 10;
    this._BtnOK.Text = "ОК";
    this._EditModeToolTip.SetToolTip((Control) this._BtnOK, "Сохранить изменения и закрыть диалог");
    this._BtnOK.MouseClick += new MouseEventHandler(this._BtnOK_MouseClick);
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(492, 365);
    this._BtnCancel.MaximumSize = new Size(121, 27);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 11;
    this._BtnCancel.Text = "Отмена";
    this._EditModeToolTip.SetToolTip((Control) this._BtnCancel, "Отменить изменения и закрыть диалог");
    this._ReadModeToolTip.SetToolTip((Control) this._BtnCancel, "Закрыть диалог");
    this._BtnCancel.MouseClick += new MouseEventHandler(this._BtnOK_MouseClick);
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this._userControlDynamicGroupHeaderSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._userControlDynamicGroupHeaderSettings.DynamicGroupHeaderSettings = (DynamicGroupHeaderSettings) null;
    this._userControlDynamicGroupHeaderSettings.Location = new Point(-1, 68);
    this._userControlDynamicGroupHeaderSettings.MinimumSize = new Size(500, 328);
    this._userControlDynamicGroupHeaderSettings.Name = "_userControlDynamicGroupHeaderSettings";
    this._userControlDynamicGroupHeaderSettings.RootDynamicGroupHeaderSettings = (DynamicGroupHeaderSettings) null;
    this._userControlDynamicGroupHeaderSettings.Size = new Size(627, 328);
    this._userControlDynamicGroupHeaderSettings.TabIndex = 0;
    this._userControlDynamicGroupHeaderSettings.OnChangedEvent += new EventHandler(this._userControlDynamicGroupHeaderSettings_OnChangedEvent);
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
    this.treeList1.Location = new Point(12, 12);
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
    this.treeList1.Size = new Size(601, 62);
    this.treeList1.StateImageList = this.imageList1;
    this.treeList1.Styles.AddReplace("TreeLine", (object) new ViewStyle("TreeLine", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.ControlDark));
    this.treeList1.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.ControlDark, SystemColors.Window));
    this.treeList1.TabIndex = 20;
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
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(626, 406);
    this.Controls.Add((Control) this.treeList1);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.Controls.Add((Control) this._userControlDynamicGroupHeaderSettings);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(642, 445);
    this.Name = nameof (DynamicGroupHeaderSettingsForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Настройка группировки записей под общим заголовком";
    this.Closed += new EventHandler(this.FormDynamicGroupHeaderSettings_Closed);
    this.FormClosing += new FormClosingEventHandler(this.FormDynamicGroupHeaderSettings_FormClosing);
    this.Load += new EventHandler(this.FormDynamicGroupHeaderSettings_Load);
    this.treeList1.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Идентификатор объекта, в атрибуте которого хранятся настройки </summary>
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
  protected override bool GetIsReadOnly()
  {
    return this._userControlDynamicGroupHeaderSettings.GetIsReadOnly();
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
    if (this._dynamicGroupHeaderSettings == null)
      return false;
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return false;
    SettingsLevel level = this._userControlDynamicGroupHeaderSettings.DynamicGroupHeaderSettings.Level;
    DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector levelNodeConnector = (DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector) focusedNode.GetValue((object) 0);
    if (levelNodeConnector == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._userControlDynamicGroupHeaderSettings.DynamicGroupHeaderSettings.OwnerObjectID);
      if (dbObject1 == null)
        return false;
      if (dbObject1.ObjectID < 0L)
      {
        if (dbObject1.CheckoutBy == sessionKeeper.Session.UserID)
          return true;
        int num = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', взят на редактирование пользователем '{sessionKeeper.Session.GetObject(dbObject1.CheckoutBy).Caption}', редактирование настроек недоступно", "Настройка группировки записей под общим заголовком", MessageBoxButtons.OK);
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
          if (MessageBox.Show($"Взять на редактирование объект '{dbObject1.Caption}'? (После завершения редактирования объект будет возвращен в архив)", "Настройка группировки записей под общим заголовком", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return false;
          IDBObject dbObject2 = dbObject1.CheckOut();
          if (dbObject2 == null || dbObject2.CheckoutBy != sessionKeeper.Session.UserID)
            return false;
          this._userControlDynamicGroupHeaderSettings.DynamicGroupHeaderSettings.OwnerObjectID = dbObject2.ObjectID;
          wasUpdated = true;
          this.InitSelectedLevel();
          this._needToAutoCheckIn = true;
          levelNodeConnector.NeedToAutoCheckIn = true;
          return true;
        case ObjectModifyModes.CantModify:
          int num1 = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', в атрибутах которого хранятся настройки, недоступен для редактирования", "Настройка группировки записей под общим заголовком", MessageBoxButtons.OK);
          return false;
        default:
          return false;
      }
    }
  }

  /// <summary>Форма была загружена</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormDynamicGroupHeaderSettings_Load(object sender, EventArgs e)
  {
    TreeListNode node1 = (TreeListNode) null;
    this.treeList1.BeginUnboundLoad();
    int num1 = 0;
    try
    {
      this.treeList1.Nodes.Clear();
      int parentNodeId = -1;
      TreeListNode treeListNode = (TreeListNode) null;
      this._settingLevelToTreeNode.Clear();
      foreach (SettingsLevel allLevel in this.settingsStructure.AllLevels)
      {
        if (allLevel.InheritanceLevel >= this.settingsLevel)
        {
          DynamicGroupHeaderSettings schemaByLevel = this._dynamicGroupHeaderSettings != null ? this._dynamicGroupHeaderSettings.GetSchemaByLevel(allLevel) : (DynamicGroupHeaderSettings) null;
          int stateImageIndex = schemaByLevel == null ? -1 : (schemaByLevel.ReadOnly ? 1 : -1);
          TreeListNode node2 = this.treeList1.AppendNode((object) null, parentNodeId, -1, -1, stateImageIndex);
          DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector levelNodeConnector = new DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector(allLevel, node2, this._settingsHolderObjType);
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
        UserControlDynamicGroupHeaderSettings groupHeaderSettings = this._userControlDynamicGroupHeaderSettings;
        Point location = this._userControlDynamicGroupHeaderSettings.Location;
        int x = location.X;
        location = this._userControlDynamicGroupHeaderSettings.Location;
        int y = location.Y - num3;
        Point point = new Point(x, y);
        groupHeaderSettings.Location = point;
      }
      foreach (TreeListNode node3 in this.treeList1.Nodes)
        node3.Expanded = true;
      Application.DoEvents();
    }
    else if (this.treeList1.Visible)
    {
      this.treeList1.Visible = false;
      int num4 = this.treeList1.Height + this.treeList1.Location.Y;
      this._userControlDynamicGroupHeaderSettings.Location = new Point(this._userControlDynamicGroupHeaderSettings.Location.X, this._userControlDynamicGroupHeaderSettings.Location.Y - num4);
      this._userControlDynamicGroupHeaderSettings.Height += num4;
      Size size = this.Size;
      int width = size.Width;
      size = this.Size;
      int height = size.Height - num4;
      this.Size = new Size(width, height);
    }
    if (node1 != null)
      this.treeList1.SetFocusedNode(node1);
    this._loaded = true;
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormDynamicGroupHeaderSettings_Closed(object sender, EventArgs e)
  {
  }

  /// <summary>Установить форму в вьюшку</summary>
  public void SetInView()
  {
    this.AcceptButton = (IButtonControl) null;
    this.CancelButton = (IButtonControl) null;
    this.inView = true;
  }

  /// <summary> Инициализировать настройки</summary>
  public void InitSettings()
  {
    this.LockControls();
    try
    {
      this._dynamicGroupHeaderSettings = this.LoadSettings();
      this._userControlDynamicGroupHeaderSettings.RootDynamicGroupHeaderSettings = this._dynamicGroupHeaderSettings;
      this._userControlDynamicGroupHeaderSettings.DynamicGroupHeaderSettings = this._dynamicGroupHeaderSettings;
      this.Changed = false;
      this.RefreshReadOnly();
      this.UpdateControls(true);
      this.RaiseOnInitDataEvent((object) this._dynamicGroupHeaderSettings);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>
  /// Загрузка настроек применимых к объекту, для которого открыт данный диалог
  /// </summary>
  public DynamicGroupHeaderSettings LoadSettings()
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
      return (DynamicGroupHeaderSettings) this.settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, this._settingsHolderObjID, this._settingsHolderObjType, this._templateObjectID, AvsIDCache.Attr_DynamicGroupHeaderSettings, typeof (DynamicGroupHeaderSettings));
  }

  /// <summary> Переинициализация выбранного уровня настроек </summary>
  public void InitSelectedLevel()
  {
    this.LockControls();
    try
    {
      this._userControlDynamicGroupHeaderSettings.DynamicGroupHeaderSettings.LoadParams();
      this._userControlDynamicGroupHeaderSettings.Changed = false;
      this._userControlDynamicGroupHeaderSettings.RefreshReadOnly();
      this._userControlDynamicGroupHeaderSettings.UpdateControls(true);
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      if (focusedNode == null || !this._userControlDynamicGroupHeaderSettings.ReadOnly)
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
    foreach (DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector key in (IEnumerable) this._settingLevelToTreeNode.Keys)
    {
      if (key.NeedToAutoCheckIn)
      {
        DynamicGroupHeaderSettings schemaByLevel = this._dynamicGroupHeaderSettings.GetSchemaByLevel(key.Level);
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
    if (this._dynamicGroupHeaderSettings == null)
      return;
    this.LockControls();
    try
    {
      foreach (DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector key in (IEnumerable) this._settingLevelToTreeNode.Keys)
      {
        if (key.Changed)
        {
          DynamicGroupHeaderSettings schemaByLevel = this._dynamicGroupHeaderSettings.GetSchemaByLevel(key.Level);
          if (schemaByLevel != null)
          {
            schemaByLevel.SaveParams();
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(schemaByLevel.OwnerObjectID);
              if (AVSPlugin.NotificationService != null)
                AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(dbObject.ObjectID, dbObject.ObjectType, new AttributeValues(AvsIDCache.Attr_DynamicGroupHeaderSettings, (object) null), new AttributeValues(AvsIDCache.Attr_DynamicGroupHeaderSettings, (object) null)));
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
    if (e.Node == null || !this._loaded || this._dynamicGroupHeaderSettings == null)
      return;
    object obj = e.Node.GetValue((object) 0);
    if (obj == null || !(obj is DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector))
      return;
    DynamicGroupHeaderSettings schemaByLevel = this._dynamicGroupHeaderSettings.GetSchemaByLevel(((DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector) obj).Level);
    if (schemaByLevel == null)
      return;
    this.LockControls();
    try
    {
      this._userControlDynamicGroupHeaderSettings.DynamicGroupHeaderSettings = schemaByLevel;
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void _userControlDynamicGroupHeaderSettings_OnChangedEvent(object sender, EventArgs e)
  {
    if (this._dynamicGroupHeaderSettings == null)
      return;
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag == null)
      return;
    DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector levelNodeConnector = (DynamicGroupHeaderSettingsForm.SettingsLevelNodeConnector) focusedNode.GetValue((object) 0);
    if (levelNodeConnector == null)
      return;
    levelNodeConnector.Changed = true;
    focusedNode.StateImageIndex = 0;
  }

  private void FormDynamicGroupHeaderSettings_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.ReadOnly && this.DialogResult == DialogResult.OK)
      this.SaveChanges();
    this.AutoCheckInAll();
    if (!this.inView)
      return;
    e.Cancel = true;
    this.InitSettings();
    this.FormDynamicGroupHeaderSettings_Load((object) null, EventArgs.Empty);
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
      return this._userControlDynamicGroupHeaderSettings.CancelButtonRightEdge > 0 ? this._userControlDynamicGroupHeaderSettings.CancelButtonRightEdge + 1 : this.Size.Width - (this._BtnCancel.Location.X + this._BtnCancel.Size.Width);
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

    /// <summary> Ссылка на уровень настроек </summary>
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
