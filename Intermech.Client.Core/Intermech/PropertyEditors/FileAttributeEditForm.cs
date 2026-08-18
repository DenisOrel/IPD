
// Type: Intermech.PropertyEditors.FileAttributeEditForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Checksums;
using Intermech.Client.Core;
using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.ControlFlow;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.CustomServices;
using Intermech.IO;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Redline;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for FileAttributeEditForm.</summary>
public class FileAttributeEditForm : Form
{
  /// <summary>Required designer variable.</summary>
  private static readonly string WithoutName = LocalizationHolder.rm.GetString("Client.Core_849");
  private IContainer components;
  /// <summary>
  /// флаг необходимости выполнения глобального оповещения об изменении объекта/связи
  /// </summary>
  private bool wasChangedEvendNeeded;
  /// <summary>
  /// флаг, нужно ли подмахивать наименование поля Комментарии.
  /// для объектов, наследованных от типа "Прототипы для файловых объектов"
  /// для редактируемых файлов вместо поля "Комментарий" пишем "Шаблон получения имени файла"
  /// </summary>
  private bool replaceFileComment;
  /// <summary>
  /// автоматическое переименование файлов при конфликте имен
  /// </summary>
  private bool autoRename;
  private MenuItemExt mbiAddAttr;
  private MenuItemExt mbiDelAttr;
  private MenuItemExt mbiClearAttr;
  private MenuItemExt mbiFileToolsSeparator0;
  private MenuItemExt mbiAddVal;
  private MenuItemExt mbiDelVal;
  private MenuItemExt mbiUploadVal;
  private MenuItemExt mbiDownloadVal;
  private MenuItemExt mbiUpdate;
  private MenuItemExt mbiFileToolsSeparator;
  private MenuItemExt mbiViewValue;
  private MenuItemExt mbiEditValue;
  public static readonly Guid mnuAddAttribute = Guid.NewGuid();
  public static readonly Guid mnuDeleteAttribute = Guid.NewGuid();
  public static readonly Guid mnuClearAttribute = Guid.NewGuid();
  public static readonly Guid mnuAddValue = Guid.NewGuid();
  public static readonly Guid mnuDeleteValue = Guid.NewGuid();
  public static readonly Guid mnuUpValue = Guid.NewGuid();
  public static readonly Guid mnuDownValue = Guid.NewGuid();
  public static readonly Guid mnuUpdateValue = Guid.NewGuid();
  public static readonly Guid mnuViewValue = Guid.NewGuid();
  public static readonly Guid mnuEditValue = Guid.NewGuid();
  private bool blockValueChanged;
  private bool biAssigned;
  private BlobInformation biSafe;
  private Hashtable downloads = Hashtable.Synchronized(new Hashtable());
  private BoxedAttributeTypeEditForm batef;
  private long id;
  private int elementType;
  private AttributableElements attributableElement;
  private bool attributableReadOnly;
  private DataTable possibleAttributes;
  private bool loaded;
  private NotificationEventHandler objectWasCheckedOutHandler;
  private NotificationEventHandler objectWasCheckedInHandler;
  private NotificationEventHandler objectChangesWasCanceledHandler;
  private NotificationEventHandler fileAttributeWasChangedHandler;
  private AttributeValueClassList attributeValuesList;
  private TreeView treeView;
  private SaveFileDialog saveFileDialog;
  private OpenFileDialog openFileDialog;
  private ContextMenu contextMenu1;
  private FileAttributeSelectorForm attrSelForm;
  private HeaderControl headerControl1;
  private Panel panel1;
  private Splitter splitter1;
  private Panel panel2;
  private PropertyGrid propertyGrid;
  private HeaderControl headerControl2;
  private bool xFile;
  private bool xBlob;
  private TabControl propertiesControl;
  private TabPage tabPage1;
  private TabPage tabPage2;
  private FilesHistoryView historyView;
  private HeaderControl headerControl3;
  private bool xShortBlob;
  protected Intermech.Bars.ToolBar toolBar1;
  private ButtonItem updateToolBtn;
  private ButtonItem addAttrToolBtn;
  private ButtonItem delAttrToolBtn;
  private ButtonItem addValToolBtn;
  private ButtonItem delValToolBtn;
  private ButtonItem upValToolBtn;
  private ButtonItem downValToolBtn;
  private ButtonItem viewValToolBtn;
  private ButtonItem editValToolBtn;
  private ImageList imageList;
  protected ButtonItem buttonHeightSet;
  /// <summary>
  /// грузить ли историю файлов для указаного объекта
  /// (если  у объекта есть хотя бы один файл,
  /// помещённый в шкаф типа ips.dvs)
  /// </summary>
  private bool isObjectHistoryLoad;
  private ButtonItem bCrc;
  private ButtonItem bCrcAll;
  private ComboBoxItem cbCrcAlgorithm;
  /// <summary>
  /// кэш флагов "Возможна модификация без взятия на изменение" для всех файловых/блобовых атрибутов объекта-связи.
  /// </summary>
  private AttributeOptionsStatusHelper modifyInBaseStatus;
  private static bool _cfgInited = false;
  private static ChecksumAlgorithm _checksumAlgorithm;
  private static bool _enableChecksumAlternatives;
  private static bool _DVSExists = false;
  /// <summary>
  /// Список файловых шкафов с сервера. Должен быть закэширован, т.к. меняется крайне редко!!!
  /// </summary>
  private static BlobStorageInfo[] blobStorages = (BlobStorageInfo[]) null;
  private FileAttributeProgressForm fapfGlobal;
  /// <summary>блокировка команд при операциях закачки</summary>
  private bool lockCommands;
  private bool cancelProgressFlag;
  private bool fileReplace;

  public long Id => this.id;

  public int ElementType => this.elementType;

  public AttributableElements AttributableElement => this.attributableElement;

  public bool AttributableReadOnly => this.attributableReadOnly;

  public bool Loaded => this.loaded;

  public FileAttributeEditForm()
  {
    this.InitializeComponent();
    if (!this.DesignMode && ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
      this.editValToolBtn.Image = service.ImageList.Images[service.ImageIndex("imgCheckOut")];
    this.Initialize();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.UnsubscribeThreadEvents();
      this.UnsubscribeEvents();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  public void Initialize()
  {
    this.SubscribeEvents();
    this.LoadCrcConfigs();
  }

  private void SubscribeEvents()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    this.objectWasCheckedOutHandler = new NotificationEventHandler(this.ObjectWasCheckedOut);
    this.objectWasCheckedInHandler = new NotificationEventHandler(this.ObjectWasCheckedIn);
    this.objectChangesWasCanceledHandler = new NotificationEventHandler(this.ObjectChangesWasCanceled);
    this.fileAttributeWasChangedHandler = new NotificationEventHandler(this.FileAttributeWasChanged);
    service.Subscribe("ObjectsCheckedOut", this.objectWasCheckedOutHandler);
    service.Subscribe("ObjectsCheckedIn", this.objectWasCheckedInHandler);
    service.Subscribe("ObjectsChangesCancelled", this.objectChangesWasCanceledHandler);
    service.Subscribe(ClientConsts.NotificationFileAttribute4ObjectChanged, this.fileAttributeWasChangedHandler);
  }

  private void UnsubscribeEvents()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Unsubscribe("ObjectsCheckedOut", this.objectWasCheckedOutHandler);
    service.Unsubscribe("ObjectsCheckedIn", this.objectWasCheckedInHandler);
    service.Unsubscribe("ObjectsChangesCancelled", this.objectChangesWasCanceledHandler);
    service.Unsubscribe(ClientConsts.NotificationFileAttribute4ObjectChanged, this.fileAttributeWasChangedHandler);
  }

  /// <summary> Обработчик события "объект-владелец был взят на изменение" </summary>
  public void ObjectWasCheckedOut(object sender, NotificationEventArgs e)
  {
    this.ProcessObjectEvent(e as DBObjectsEventArgs);
  }

  /// <summary> Обработчик события "объект-владелец был возвращён в архив" </summary>
  public void ObjectWasCheckedIn(object sender, NotificationEventArgs e)
  {
    this.ProcessObjectEvent(e as DBObjectsEventArgs);
  }

  /// <summary> Обработчик события "Правки объекта-владелеца были отменены" </summary>
  public void ObjectChangesWasCanceled(object sender, NotificationEventArgs e)
  {
    this.ProcessObjectEvent(e as DBObjectsEventArgs);
  }

  private void ProcessObjectEvent(DBObjectsEventArgs e)
  {
    if (!this.loaded || e == null || !e.ObjectIDs.Any<long>((System.Func<long, bool>) (objectId => Math.Abs(objectId) == Math.Abs(this.id))))
      return;
    this.LoadElement(-this.id, this.AttributableElement, this.xFile, this.xBlob, this.xShortBlob);
  }

  public void FileAttributeWasChanged(object sender, NotificationEventArgs e)
  {
    if (e.EventName != ClientConsts.NotificationFileAttribute4ObjectChanged || !(e is FileAttribute4ObjectChangedEventArgs))
      return;
    TreeNode attributeNode = this.GetAttributeNode(((Attribute4ObjectEventArgs) e).AttributeID);
    if (attributeNode == null)
      return;
    this.UpdateAttributeNode(attributeNode);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileAttributeEditForm));
    this.treeView = new TreeView();
    this.contextMenu1 = new ContextMenu();
    this.saveFileDialog = new SaveFileDialog();
    this.openFileDialog = new OpenFileDialog();
    this.panel1 = new Panel();
    this.headerControl1 = new HeaderControl();
    this.splitter1 = new Splitter();
    this.panel2 = new Panel();
    this.propertiesControl = new TabControl();
    this.tabPage1 = new TabPage();
    this.propertyGrid = new PropertyGrid();
    this.headerControl2 = new HeaderControl();
    this.tabPage2 = new TabPage();
    this.historyView = new FilesHistoryView();
    this.headerControl3 = new HeaderControl();
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.imageList = new ImageList();
    this.updateToolBtn = new ButtonItem();
    this.addAttrToolBtn = new ButtonItem();
    this.delAttrToolBtn = new ButtonItem();
    this.addValToolBtn = new ButtonItem();
    this.delValToolBtn = new ButtonItem();
    this.upValToolBtn = new ButtonItem();
    this.downValToolBtn = new ButtonItem();
    this.editValToolBtn = new ButtonItem();
    this.viewValToolBtn = new ButtonItem();
    this.cbCrcAlgorithm = new ComboBoxItem();
    this.bCrc = new ButtonItem();
    this.bCrcAll = new ButtonItem();
    this.buttonHeightSet = new ButtonItem();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.propertiesControl.SuspendLayout();
    this.tabPage1.SuspendLayout();
    this.tabPage2.SuspendLayout();
    this.SuspendLayout();
    this.treeView.AllowDrop = true;
    this.treeView.BorderStyle = BorderStyle.None;
    this.treeView.ContextMenu = this.contextMenu1;
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.HideSelection = false;
    this.treeView.Name = "treeView";
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    this.treeView.DragDrop += new DragEventHandler(this.treeView_DragDrop);
    this.treeView.DragOver += new DragEventHandler(this.treeView_DragOver);
    this.contextMenu1.Popup += new EventHandler(this.contextMenu1_Popup);
    this.saveFileDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.saveFileDialog, "saveFileDialog");
    this.openFileDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.openFileDialog, "openFileDialog");
    this.panel1.Controls.Add((Control) this.treeView);
    this.panel1.Controls.Add((Control) this.headerControl1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.headerControl1, "headerControl1");
    this.headerControl1.HeaderFont = new Font("Tahoma", 12f, FontStyle.Bold);
    this.headerControl1.Name = "headerControl1";
    this.splitter1.BackColor = SystemColors.ControlDark;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.panel2.Controls.Add((Control) this.propertiesControl);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.propertiesControl.Controls.Add((Control) this.tabPage1);
    this.propertiesControl.Controls.Add((Control) this.tabPage2);
    componentResourceManager.ApplyResources((object) this.propertiesControl, "propertiesControl");
    this.propertiesControl.Name = "propertiesControl";
    this.propertiesControl.SelectedIndex = 0;
    this.tabPage1.Controls.Add((Control) this.propertyGrid);
    this.tabPage1.Controls.Add((Control) this.headerControl2);
    componentResourceManager.ApplyResources((object) this.tabPage1, "tabPage1");
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.UseVisualStyleBackColor = true;
    this.propertyGrid.CategoryForeColor = SystemColors.InactiveCaptionText;
    this.propertyGrid.Cursor = Cursors.HSplit;
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.PropertySort = PropertySort.Alphabetical;
    this.propertyGrid.ToolbarVisible = false;
    this.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
    this.propertyGrid.SelectedObjectsChanged += new EventHandler(this.propertyGrid_SelectedObjectsChanged);
    componentResourceManager.ApplyResources((object) this.headerControl2, "headerControl2");
    this.headerControl2.HeaderFont = new Font("Tahoma", 12f, FontStyle.Bold);
    this.headerControl2.Name = "headerControl2";
    this.tabPage2.Controls.Add((Control) this.historyView);
    this.tabPage2.Controls.Add((Control) this.headerControl3);
    componentResourceManager.ApplyResources((object) this.tabPage2, "tabPage2");
    this.tabPage2.Name = "tabPage2";
    this.tabPage2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.historyView, "historyView");
    this.historyView.Name = "historyView";
    componentResourceManager.ApplyResources((object) this.headerControl3, "headerControl3");
    this.headerControl3.HeaderFont = new Font("Tahoma", 12f, FontStyle.Bold);
    this.headerControl3.Name = "headerControl3";
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("4f87bc23-173c-4e8d-973f-192a08b0dc34");
    this.toolBar1.Hidden = false;
    this.toolBar1.ImageList = this.imageList;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[13]
    {
      (ToolbarItemBase) this.updateToolBtn,
      (ToolbarItemBase) this.addAttrToolBtn,
      (ToolbarItemBase) this.delAttrToolBtn,
      (ToolbarItemBase) this.addValToolBtn,
      (ToolbarItemBase) this.delValToolBtn,
      (ToolbarItemBase) this.upValToolBtn,
      (ToolbarItemBase) this.downValToolBtn,
      (ToolbarItemBase) this.editValToolBtn,
      (ToolbarItemBase) this.viewValToolBtn,
      (ToolbarItemBase) this.cbCrcAlgorithm,
      (ToolbarItemBase) this.bCrc,
      (ToolbarItemBase) this.bCrcAll,
      (ToolbarItemBase) this.buttonHeightSet
    });
    componentResourceManager.ApplyResources((object) this.toolBar1, "toolBar1");
    this.toolBar1.Name = "toolBar1";
    this.toolBar1.Overflow = ToolBarOverflow.Wrap;
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Fuchsia;
    this.imageList.Images.SetKeyName(0, "обновить.png");
    this.imageList.Images.SetKeyName(1, "Add.bmp");
    this.imageList.Images.SetKeyName(2, "удалить.png");
    this.imageList.Images.SetKeyName(3, "AddFile.bmp");
    this.imageList.Images.SetKeyName(4, "исключить_из_состава.png");
    this.imageList.Images.SetKeyName(5, "Upload.bmp");
    this.imageList.Images.SetKeyName(6, "сохранить.png");
    this.imageList.Images.SetKeyName(7, "View.png");
    this.imageList.Images.SetKeyName(8, "Sum.png");
    this.imageList.Images.SetKeyName(9, "SumAll.png");
    componentResourceManager.ApplyResources((object) this.updateToolBtn, "updateToolBtn");
    this.updateToolBtn.ImageIndex = 0;
    this.updateToolBtn.Click += new EventHandler(this.OnUpdateMenuItem);
    this.addAttrToolBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.addAttrToolBtn, "addAttrToolBtn");
    this.addAttrToolBtn.ImageIndex = 1;
    this.addAttrToolBtn.Click += new EventHandler(this.OnAddAttributeMenuItem);
    componentResourceManager.ApplyResources((object) this.delAttrToolBtn, "delAttrToolBtn");
    this.delAttrToolBtn.ImageIndex = 2;
    this.delAttrToolBtn.Click += new EventHandler(this.OnDeleteAttributeMenuItem);
    this.addValToolBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.addValToolBtn, "addValToolBtn");
    this.addValToolBtn.ImageIndex = 3;
    this.addValToolBtn.Click += new EventHandler(this.OnAddValueMenuItem);
    componentResourceManager.ApplyResources((object) this.delValToolBtn, "delValToolBtn");
    this.delValToolBtn.ImageIndex = 4;
    this.delValToolBtn.Click += new EventHandler(this.OnDeleteValueMenuItem);
    componentResourceManager.ApplyResources((object) this.upValToolBtn, "upValToolBtn");
    this.upValToolBtn.ImageIndex = 5;
    this.upValToolBtn.Click += new EventHandler(this.OnUploadValueMenuItem);
    componentResourceManager.ApplyResources((object) this.downValToolBtn, "downValToolBtn");
    this.downValToolBtn.ImageIndex = 6;
    this.downValToolBtn.Click += new EventHandler(this.OnDownloadValueMenuItem);
    this.editValToolBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.editValToolBtn, "editValToolBtn");
    this.editValToolBtn.Click += new EventHandler(this.OnEditValueMenuItem);
    componentResourceManager.ApplyResources((object) this.viewValToolBtn, "viewValToolBtn");
    this.viewValToolBtn.ImageIndex = 7;
    this.viewValToolBtn.Click += new EventHandler(this.OnViewValueMenuItem);
    this.cbCrcAlgorithm.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.cbCrcAlgorithm, "cbCrcAlgorithm");
    this.cbCrcAlgorithm.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbCrcAlgorithm.MinimumControlWidth = 170;
    this.cbCrcAlgorithm.Padding.Bottom = 0;
    this.cbCrcAlgorithm.Padding.Left = 1;
    this.cbCrcAlgorithm.Padding.Right = 1;
    this.cbCrcAlgorithm.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this.bCrc, "bCrc");
    this.bCrc.ImageIndex = 8;
    this.bCrc.Click += new EventHandler(this.bCrc_Click);
    componentResourceManager.ApplyResources((object) this.bCrcAll, "bCrcAll");
    this.bCrcAll.ImageIndex = 9;
    this.bCrcAll.Click += new EventHandler(this.bCrcAll_Click);
    componentResourceManager.ApplyResources((object) this.buttonHeightSet, "buttonHeightSet");
    this.buttonHeightSet.Enabled = false;
    this.buttonHeightSet.IconSize = new Size(1, 37);
    this.buttonHeightSet.Image = (Image) Intermech.Client.Core.Properties.Resources.pixel;
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.toolBar1);
    this.Name = nameof (FileAttributeEditForm);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.propertiesControl.ResumeLayout(false);
    this.tabPage1.ResumeLayout(false);
    this.tabPage2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void CreateContextMenu()
  {
    this.mbiAddAttr = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_466"), new EventHandler(this.OnAddAttributeMenuItem), (object) FileAttributeEditForm.mnuAddAttribute);
    this.mbiDelAttr = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_1212"), new EventHandler(this.OnDeleteAttributeMenuItem), (object) FileAttributeEditForm.mnuDeleteAttribute);
    this.mbiClearAttr = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_1212c"), new EventHandler(this.OnClearAttributeMenuItem), (object) FileAttributeEditForm.mnuClearAttribute);
    this.mbiFileToolsSeparator0 = new MenuItemExt("-");
    this.mbiAddVal = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_932"), new EventHandler(this.OnAddValueMenuItem), (object) FileAttributeEditForm.mnuAddValue);
    this.mbiDelVal = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_933"), new EventHandler(this.OnDeleteValueMenuItem), (object) FileAttributeEditForm.mnuDeleteValue);
    this.mbiUploadVal = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_934"), new EventHandler(this.OnUploadValueMenuItem), (object) FileAttributeEditForm.mnuUpValue);
    this.mbiDownloadVal = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_935"), new EventHandler(this.OnDownloadValueMenuItem), (object) FileAttributeEditForm.mnuDownValue);
    this.mbiUpdate = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_97"), new EventHandler(this.OnUpdateMenuItem), (object) FileAttributeEditForm.mnuUpdateValue);
    this.mbiFileToolsSeparator = new MenuItemExt("-");
    this.mbiViewValue = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_View"), new EventHandler(this.OnViewValueMenuItem), (object) FileAttributeEditForm.mnuViewValue);
    this.mbiEditValue = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_Edit"), new EventHandler(this.OnEditValueMenuItem), (object) FileAttributeEditForm.mnuEditValue);
    this.contextMenu1.MenuItems.Clear();
    this.contextMenu1.MenuItems.AddRange((MenuItem[]) new MenuItemExt[3]
    {
      this.mbiAddAttr,
      this.mbiDelAttr,
      this.mbiClearAttr
    });
    this.contextMenu1.MenuItems.AddRange((MenuItem[]) new MenuItemExt[6]
    {
      this.mbiFileToolsSeparator0,
      this.mbiAddVal,
      this.mbiDelVal,
      this.mbiUploadVal,
      this.mbiDownloadVal,
      this.mbiUpdate
    });
    this.contextMenu1.MenuItems.AddRange((MenuItem[]) new MenuItemExt[3]
    {
      this.mbiFileToolsSeparator,
      this.mbiEditValue,
      this.mbiViewValue
    });
  }

  private void LoadCrcConfigs()
  {
    if (!FileAttributeEditForm._cfgInited)
    {
      IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
      FileAttributeEditForm._checksumAlgorithm = (ChecksumAlgorithm) service.ReadInteger("CLIENT", "AUTHFILES", "ALGORITHM", 0L, DBConfigMode.GlobalOnly);
      FileAttributeEditForm._enableChecksumAlternatives = service.ReadBool("CLIENT", "AUTHFILES", "ENABLEALTERNATIVES", true, DBConfigMode.GlobalOnly);
      FileAttributeEditForm._cfgInited = true;
    }
    this.cbCrcAlgorithm.Enabled = FileAttributeEditForm._enableChecksumAlternatives;
    this.LoadCrcAlgorithms();
    this.SetAlgorithmType(FileAttributeEditForm._checksumAlgorithm);
  }

  private void LoadCrcAlgorithms()
  {
    this.cbCrcAlgorithm.Items.Clear();
    foreach (ChecksumAlgorithm checksumAlgorithm in Enum.GetValues(typeof (ChecksumAlgorithm)))
      this.cbCrcAlgorithm.Items.Add((object) new ChecksumAlgorithmPropertyClass(checksumAlgorithm));
  }

  private void SetAlgorithmType(ChecksumAlgorithm alg)
  {
    for (int index = 0; index < this.cbCrcAlgorithm.Items.Count; ++index)
    {
      if (((ChecksumAlgorithmPropertyClass) this.cbCrcAlgorithm.Items[index]).ChecksumAlgorithm == alg)
      {
        this.cbCrcAlgorithm.ComboBox.SelectedItem = this.cbCrcAlgorithm.Items[index];
        break;
      }
    }
  }

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  public bool LoadElement(long aId, AttributableElements aAttributableElement)
  {
    return this.LoadElement(aId, aAttributableElement, true, false, false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aId"></param>
  /// <param name="aAttributableElement"></param>
  /// <param name="xFileAttrsEdit">редактировать атрибуты ftFile</param>
  /// <param name="xBlobAttrsEdit">редактировать атрибуты ftBlob</param>
  /// <param name="xShortBlobAttrsEdit">редактировать атрибуты ftShortBlob</param>
  /// <returns></returns>
  public bool LoadElement(
    long aId,
    AttributableElements aAttributableElement,
    bool xFileAttrsEdit,
    bool xBlobAttrsEdit,
    bool xShortBlobAttrsEdit)
  {
    FileAttributeStatics.InitImageList();
    this.treeView.ImageList = FileAttributeStatics.imageList;
    this.xFile = xFileAttrsEdit;
    this.xBlob = xBlobAttrsEdit;
    this.xShortBlob = xShortBlobAttrsEdit;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable attributable = ClientCommons.GetAttributable(aId, aAttributableElement, out this.elementType, sessionKeeper.Session);
      if (attributable == null)
        return false;
      this.id = aId;
      this.attributableElement = aAttributableElement;
      this.attributableReadOnly = attributable.ReadOnly;
      this.modifyInBaseStatus = new AttributeOptionsStatusHelper(aAttributableElement, this.elementType);
      this.replaceFileComment = false;
      if (this.xFile && aAttributableElement == AttributableElements.Object)
        this.replaceFileComment = this.CheckForReplaceFileComment(this.elementType);
      this.attributeValuesList = new AttributeValueClassList(this.id, this.attributableElement);
      this.ReadPossibleAttributesList();
      this.ReadActualAttributesList(attributable, (ArrayList) this.attributeValuesList);
    }
    this.FillTreeView();
    this.propertyGrid.SelectedObject = (object) null;
    this.UnsubscribeThreadEvents();
    this.downloads.Clear();
    this.CreateContextMenu();
    this.ProcessControlsStates();
    this.FilePlaceInFileStorage();
    if (!this.isObjectHistoryLoad)
      this.propertiesControl.TabPages.Remove(this.tabPage2);
    this.loaded = true;
    return true;
  }

  private bool CheckForReplaceFileComment(int objectType)
  {
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00342-306c-11d8-b4e9-00304f19f545"));
    return childrenIdRecursive != null && childrenIdRecursive.IndexOf(objectType) != -1;
  }

  private void ReadPossibleAttributesList()
  {
    this.possibleAttributes = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBCollection dbCollection = (IDBCollection) null;
      bool flag = false;
      if (this.attributableElement == AttributableElements.Object)
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.elementType);
        dbCollection = (IDBCollection) objectType.Attributes;
        flag = objectType.AnyAttributes;
      }
      if (this.attributableElement == AttributableElements.Relation)
      {
        IDBRelationType relationType = sessionKeeper.Session.GetRelationType(this.elementType);
        dbCollection = (IDBCollection) relationType.Attributes;
        flag = relationType.AnyAttributes;
      }
      if (flag)
        dbCollection = (IDBCollection) sessionKeeper.Session.GetAttributesGroup(-1).Attributes;
      FieldTypes fieldTypes = FieldTypes.ftUnknown;
      if (this.xFile && !this.xBlob && !this.xShortBlob)
        fieldTypes = FieldTypes.ftFile;
      if (!this.xFile && this.xBlob && !this.xShortBlob)
        fieldTypes = FieldTypes.ftBlob;
      if (!this.xFile && !this.xBlob && this.xShortBlob)
        fieldTypes = FieldTypes.ftShortBlob;
      if (fieldTypes != FieldTypes.ftUnknown)
      {
        this.possibleAttributes = dbCollection.Select(string.Empty, (object) fieldTypes, (object) "ALL_FIELDS");
      }
      else
      {
        this.possibleAttributes = dbCollection.Select(string.Empty, (object) "ALL_FIELDS");
        int index = 0;
        while (index < this.possibleAttributes.Rows.Count)
        {
          int int32 = Convert.ToInt32(this.possibleAttributes.Rows[index]["F_ATTRIBUTE_ID"]);
          IDBAttributeType attributeType1 = sessionKeeper.Session.GetAttributeType(int32);
          if (attributeType1 != null)
          {
            FieldTypes attributeType2 = attributeType1.AttributeType;
            if (this.xFile && attributeType2 == FieldTypes.ftFile || this.xBlob && attributeType2 == FieldTypes.ftBlob || this.xShortBlob && attributeType2 == FieldTypes.ftShortBlob)
              ++index;
            else
              this.possibleAttributes.Rows.RemoveAt(index);
          }
        }
      }
    }
  }

  private void ReadActualAttributesList(
    IDBAttributable iDBAttributable,
    ArrayList list,
    params int[] selectedId)
  {
    list.Clear();
    if (this.xFile)
      this.ReadActualAttributesList4FieldType(iDBAttributable, list, FieldTypes.ftFile, selectedId);
    if (this.xShortBlob)
      this.ReadActualAttributesList4FieldType(iDBAttributable, list, FieldTypes.ftShortBlob, selectedId);
    if (!this.xBlob)
      return;
    this.ReadActualAttributesList4FieldType(iDBAttributable, list, FieldTypes.ftBlob, selectedId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="iDBAttributable"></param>
  /// <param name="list">только на добавление, очистка должна быть вне процедуры</param>
  /// <param name="ft"></param>
  /// <param name="selectedId"></param>
  private void ReadActualAttributesList4FieldType(
    IDBAttributable iDBAttributable,
    ArrayList list,
    FieldTypes ft,
    params int[] selectedId)
  {
    IDBAttribute[] attributesByType = iDBAttributable.Attributes.GetAttributesByType(ft);
    IDBAttribute attributeByGuid = iDBAttributable.GetAttributeByGuid(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545"));
    foreach (IDBAttribute iDBAttribute in attributesByType)
    {
      if (selectedId.Length == 0 || selectedId.Length != 0 && Array.IndexOf<int>(selectedId, iDBAttribute.AttributeID) != -1)
      {
        AttributeValueClass attributeValueClass = new AttributeValueClass(iDBAttribute, attributeByGuid);
        list.Add((object) attributeValueClass);
      }
    }
  }

  private AttributeValueClass ReadActualAttributeValueClass(
    IDBAttributable iDBAttributable,
    int attributeId)
  {
    return new AttributeValueClass(iDBAttributable.GetAttributeByID(attributeId), iDBAttributable.GetAttributeByGuid(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545")));
  }

  private TreeNode ConstructAttributeNode(int id, string name, FieldTypes ft)
  {
    TreeNode treeNode = new TreeNode(name)
    {
      Tag = (object) new object[2]
      {
        (object) id,
        (object) ft
      },
      ImageIndex = FileAttributeStatics.FieldTypeToImageIndex(ft)
    };
    treeNode.SelectedImageIndex = treeNode.ImageIndex;
    return treeNode;
  }

  private TreeNode ConstructValueNode(
    FieldTypes attrType,
    BlobInformation bi,
    Color ColorText,
    bool isReadOnly,
    params object[] pars)
  {
    TreeNode tn = new TreeNode(attrType == FieldTypes.ftFile ? (bi.FileName == string.Empty ? FileAttributeEditForm.WithoutName : bi.FileName) : bi.BlobID.ToString());
    object obj = (object) null;
    switch (attrType)
    {
      case FieldTypes.ftShortBlob:
        obj = (object) new ShortBlobAttributeValueObject(bi, isReadOnly);
        break;
      case FieldTypes.ftFile:
        long par1 = (long) pars[0];
        obj = (object) new FileAttributeValueObject(bi, par1, isReadOnly);
        ((FileAttributeValueObject) obj).ReplaceFileComment = this.replaceFileComment;
        tn.ForeColor = ColorText;
        break;
      case FieldTypes.ftBlob:
        long par2 = (long) pars[0];
        obj = (object) new BlobAttributeValueObject(bi, par2, isReadOnly);
        break;
    }
    tn.Tag = obj;
    this.ChangeValueNodeTextAndImage(tn, tn.Text);
    return tn;
  }

  private void FillTreeView()
  {
    this.treeView.Nodes.Clear();
    FileAttributeStatics.InitImageList();
    for (int index = 0; index < this.attributeValuesList.Count; ++index)
    {
      AttributeValueClass attributeValues = (AttributeValueClass) this.attributeValuesList[index];
      TreeNode treeNode = this.ConstructAttributeNode(attributeValues.attributeID, attributeValues.attributeName, attributeValues.attributeType);
      this.treeView.Nodes.Add(treeNode);
      this.AddValuesToNode(treeNode, attributeValues);
      treeNode.Expand();
    }
  }

  /// <summary>Да, если есть шкаф типа DVS</summary>
  private bool DVSExists
  {
    get
    {
      if (FileAttributeEditForm.blobStorages == null)
      {
        FileAttributeEditForm.blobStorages = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IBlobStoragesService)) as IBlobStoragesService).GetStorages();
        foreach (BlobStorageInfo blobStorage in FileAttributeEditForm.blobStorages)
        {
          if (blobStorage.StorageType == "Intermech Document Server")
          {
            FileAttributeEditForm._DVSExists = true;
            break;
          }
        }
      }
      return FileAttributeEditForm._DVSExists;
    }
  }

  /// <summary>
  /// Загружать или нет историю изменения файлов
  /// 26.04.2010 В соот-ии с распоряжением Жукова:
  /// Историю показывfть только в том случае, если у объекта есть хотя бы один файл,
  /// расположенный в файловом шкафу ips.dvs.
  /// Если у объекта раньше были файлы, располженные в файловых шкафах ips.dvs,
  /// но в последствии файлы были перенесены или удалены - историю не показывать.
  /// </summary>
  private void FilePlaceInFileStorage()
  {
    if (!this.DVSExists)
      return;
    for (int index1 = 0; index1 < this.attributeValuesList.Count; ++index1)
    {
      AttributeValueClass attributeValues = (AttributeValueClass) this.attributeValuesList[index1];
      if (attributeValues.attributeType == FieldTypes.ftFile || attributeValues.attributeType == FieldTypes.ftBlob)
      {
        for (int index2 = 0; index2 < attributeValues.items.Count; ++index2)
        {
          long boxId = attributeValues.items[index2].boxId;
          bool flag = false;
          foreach (BlobStorageInfo blobStorage in FileAttributeEditForm.blobStorages)
          {
            if (blobStorage.StorageID == boxId)
            {
              flag = blobStorage.StorageType == "Intermech Document Server";
              break;
            }
          }
          if (flag)
          {
            this.isObjectHistoryLoad = true;
            this.LoadFileHistory();
            return;
          }
        }
      }
    }
  }

  private void AddValuesToNode(TreeNode tn, AttributeValueClass avc)
  {
    this.AddValuesToNode(tn, avc, true);
  }

  private void AddValuesToNode(TreeNode tn, AttributeValueClass avc, bool doClear)
  {
    if (doClear)
      tn.Nodes.Clear();
    for (int index = 0; index < avc.items.Count; ++index)
    {
      List<object> objectList = new List<object>();
      if (avc.attributeType == FieldTypes.ftFile || avc.attributeType == FieldTypes.ftBlob)
        objectList.Add((object) avc.items[index].boxId);
      tn.Nodes.Add(this.ConstructValueNode(avc.attributeType, avc.items[index].bi, avc.items[index].ColorText, avc.attributeReadOnly || avc.attributeDisableManualEdit, objectList.ToArray()));
    }
  }

  private Hashtable CollectActiveAttributes()
  {
    Hashtable hashtable = new Hashtable(this.treeView.Nodes.Count);
    for (int index = 0; index < this.treeView.Nodes.Count; ++index)
      hashtable.Add((object) (int) ((object[]) this.treeView.Nodes[index].Tag)[0], (object) null);
    return hashtable;
  }

  private ArrayList GetArray4Select(DataTable possible, Hashtable activeIDs)
  {
    ArrayList array4Select = new ArrayList();
    foreach (DataRow row in (InternalDataCollectionBase) possible.Rows)
    {
      int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      FieldTypes int32_2 = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
      string lname = row["F_NAME"].ToString();
      if (activeIDs.Count == 0 || !activeIDs.ContainsKey((object) int32_1))
        array4Select.Add((object) new AttrSelObject(int32_1, int32_2, lname));
    }
    return array4Select;
  }

  private bool IsAttributeNode(TreeNode tn) => tn != null && tn.Parent == null;

  private bool IsFileTypeAttributeNode(TreeNode tn)
  {
    return this.IsAttributeNode(tn) && (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType((int) ((object[]) tn.Tag)[0]).AttributeType == FieldTypes.ftFile;
  }

  private bool IsFileAttributeNode(TreeNode tn)
  {
    if (this.IsAttributeNode(tn))
    {
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      if (object.Equals(((object[]) tn.Tag)[0], (object) service.FileAttributeID))
        return true;
    }
    return false;
  }

  private bool IsValueNode(TreeNode tn) => tn != null && tn.Tag is CustomAttributeValueObject;

  private void OnAddAttributeMenuItem(object sender, EventArgs e)
  {
    if (this.attrSelForm == null)
    {
      this.attrSelForm = new FileAttributeSelectorForm();
      this.attrSelForm.BeforeClosing += new FileAttributeSelectorForm.BeforeClosingEventHandler(this.attrSelForm_BeforeClosing);
    }
    ArrayList array4Select = this.GetArray4Select(this.possibleAttributes, this.CollectActiveAttributes());
    if (array4Select.Count == 0)
    {
      int num1 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_132"), LocalizationHolder.rm.GetString("No_Attributes_For_Select"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
    }
    else
    {
      ArrayList attrsSelected = (ArrayList) null;
      if (this.attrSelForm.SelectDialog(array4Select, out attrsSelected) == DialogResult.OK && attrsSelected != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttributable attributable1 = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
          if (attributable1 == null)
            return;
          this.attributableReadOnly = attributable1.ReadOnly;
          bool flag = true;
          if (this.attributableReadOnly)
            this.modifyInBaseStatus.Clear();
          for (int index1 = 0; index1 < attrsSelected.Count; ++index1)
          {
            if (this.attributableReadOnly && !this.modifyInBaseStatus.CheckOptionStatus(((AttrSelObject) attrsSelected[index1]).id, AttributeOptions.ModifyInBase))
            {
              int num2 = (int) IMMessageBox.Show(MessageDialogs.msgError, string.Format(LocalizationHolder.rm.GetString("Client.Core.ImpossibleToAddAttribute"), (object) ((AttrSelObject) attrsSelected[index1]).name), MessageBoxButtons.OK, IMMessageBoxImage.Error);
              this.attributableReadOnly = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session).ReadOnly;
            }
            else
            {
              FileTypes fileTypes = FileTypes.ftNormal;
              if (((AttrSelObject) attrsSelected[index1]).type == FieldTypes.ftFile)
              {
                if (this.batef == null)
                  this.batef = new BoxedAttributeTypeEditForm();
                if (this.batef.ShowDialog(((AttrSelObject) attrsSelected[index1]).name, false) != DialogResult.OK)
                  return;
                fileTypes = this.batef.SelectedFileType;
              }
              this.openFileDialog.Title = $"{LocalizationHolder.rm.GetString("Client.Core_936")}{((AttrSelObject) attrsSelected[index1]).name}\"";
              this.openFileDialog.Multiselect = true;
              if (this.openFileDialog.ShowDialog() == DialogResult.OK)
              {
                string[] fileNames = this.openFileDialog.FileNames;
                if (fileNames != null)
                {
                  IDBAttributable attributable2 = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
                  this.attributableReadOnly = attributable2.ReadOnly;
                  IDBAttribute iDBAttribute;
                  try
                  {
                    iDBAttribute = attributable2.Attributes.AddAttribute(((AttrSelObject) attrsSelected[index1]).id, true);
                  }
                  catch (Exception ex)
                  {
                    ExceptionHelper.ExceptionService.ShowException(ex);
                    continue;
                  }
                  TreeNode treeNode = this.ConstructAttributeNode(((AttrSelObject) attrsSelected[index1]).id, ((AttrSelObject) attrsSelected[index1]).name, ((AttrSelObject) attrsSelected[index1]).type);
                  this.treeView.Nodes.Add(treeNode);
                  IDBAttribute attributeByGuid = attributable2.GetAttributeByGuid(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545"));
                  AttributeValueClass attributeValueClass = new AttributeValueClass(iDBAttribute, attributeByGuid);
                  this.attributeValuesList.Add((object) attributeValueClass);
                  for (int index2 = 0; index2 < attributeValueClass.items.Count; ++index2)
                  {
                    List<object> objectList = new List<object>();
                    if (attributeValueClass.attributeType == FieldTypes.ftFile || attributeValueClass.attributeType == FieldTypes.ftBlob)
                      objectList.Add((object) attributeValueClass.items[index2].boxId);
                    treeNode.Nodes.Add(this.ConstructValueNode(attributeValueClass.attributeType, attributeValueClass.items[index2].bi, attributeValueClass.items[index2].ColorText, attributeValueClass.attributeReadOnly || attributeValueClass.attributeDisableManualEdit, objectList.ToArray()));
                  }
                  using (FileAttributeProgressForm fapf = new FileAttributeProgressForm())
                  {
                    this.cancelProgressFlag = false;
                    fapf.Break += new BreakEvent(this.fapf_Break);
                    fapf.ShowProgress(LocalizationHolder.rm.GetString("FileAttributeUpload"), fileNames.Length);
                    this.LockCommands = true;
                    try
                    {
                      for (int index3 = 0; index3 < fileNames.Length; ++index3)
                      {
                        try
                        {
                          if (index3 < treeNode.Nodes.Count)
                          {
                            this.UploadValueForeground(treeNode.Nodes[index3], fileNames[index3], fileTypes, fapf);
                          }
                          else
                          {
                            TreeNode vtn = this.AddValue(treeNode, fileTypes);
                            if (vtn != null)
                              this.UploadValueForeground(vtn, fileNames[index3], fileTypes, fapf);
                          }
                        }
                        catch (Exception ex)
                        {
                          ExceptionHelper.ExceptionService.ShowException(ex);
                        }
                        if (this.cancelProgressFlag)
                          break;
                      }
                    }
                    finally
                    {
                      fapf.HideProgress();
                      this.LockCommands = false;
                    }
                  }
                  flag = flag && fileNames.Length == 0;
                  treeNode.Expand();
                }
              }
            }
          }
          if (attrsSelected.Count > 0 & flag)
            this.FireElementWasChangedSemafor();
        }
      }
      this.FireElementWasChangedEvent();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// <param name="selected">list of AttrSelObject</param>
  private void attrSelForm_BeforeClosing(object sender, CancelEventArgs e, ArrayList selected)
  {
    if (selected == null)
      return;
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < selected.Count; ++index)
    {
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(this.elementType, ((AttrSelObject) selected[index]).id);
      if (attribute4ObjectType != null)
      {
        if ((attribute4ObjectType.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit)
          arrayList.Add(selected[index]);
      }
      else
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(((AttrSelObject) selected[index]).id);
        if (attributeType != null && (attributeType.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit)
          arrayList.Add(selected[index]);
      }
    }
    if (arrayList.Count <= 0)
      return;
    string str = string.Empty;
    for (int index = 0; index < arrayList.Count; ++index)
      str = $"{str}\n\"{(object) (AttrSelObject) arrayList[index]}\"";
    int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_132"), LocalizationHolder.rm.GetString("SelectImpossible4DisableManualEdit") + str, MessageBoxButtons.OK, IMMessageBoxImage.Warning);
    e.Cancel = true;
  }

  private void OnDeleteAttributeMenuItem(object sender, EventArgs e)
  {
    if (this.treeView.SelectedNode == null || !this.IsAttributeNode(this.treeView.SelectedNode) || IMMessageBox.Show(MessageDialogs.msgConfirmDelete, MessageDialogs.msgReallyDeleteAttribute, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    this.DeleteAttribute((int) ((object[]) this.treeView.SelectedNode.Tag)[0]);
    this.treeView.SelectedNode.Remove();
    this.FireElementWasChangedEvent();
  }

  private void OnClearAttributeMenuItem(object sender, EventArgs e)
  {
    if (this.treeView.SelectedNode == null || !this.IsAttributeNode(this.treeView.SelectedNode) || IMMessageBox.Show(MessageDialogs.msgConfirmDelete, MessageDialogs.msgReallyClearAttribute, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    int num = (int) ((object[]) this.treeView.SelectedNode.Tag)[0];
    AttributeValueClass avc = this.ClearAttribute(num);
    int index = this.attributeValuesList.IndexOfbyAttributeID(num);
    if (index != -1)
    {
      this.attributeValuesList[index] = (object) avc;
      TreeNode selectedNode = this.treeView.SelectedNode;
      this.AddValuesToNode(selectedNode, avc);
      selectedNode.Expand();
    }
    this.FireElementWasChangedEvent();
  }

  private void DeleteAttribute(int aAttributeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
      if (attributable == null)
        return;
      IDBAttribute attributeById = attributable.GetAttributeByID(aAttributeId);
      if (attributeById == null)
        return;
      int index = this.attributeValuesList.IndexOfbyAttributeID(attributeById.AttributeID);
      attributeById.Delete(0L);
      if (index != -1)
        this.attributeValuesList.RemoveAt(index);
      this.FireElementWasChangedSemafor();
    }
  }

  /// <summary>
  /// Очистка значений атрибута и перечитка пустого значения атрибута
  /// </summary>
  /// <param name="aAttributeId"></param>
  /// <returns></returns>
  private AttributeValueClass ClearAttribute(int aAttributeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
      if (attributable != null)
      {
        IDBAttribute attributeById = attributable.GetAttributeByID(aAttributeId);
        if (attributeById != null)
        {
          attributeById.ClearValues();
          this.FireElementWasChangedSemafor();
          return this.ReadActualAttributeValueClass(attributable, aAttributeId);
        }
      }
    }
    return (AttributeValueClass) null;
  }

  private TreeNode AddValue(TreeNode attributeTreeNode, FileTypes defaultFileType)
  {
    TreeNode node = (TreeNode) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
      if (attributable == null)
        return (TreeNode) null;
      IDBAttribute attributeById = attributable.GetAttributeByID((int) ((object[]) attributeTreeNode.Tag)[0]);
      if (attributeById != null)
      {
        int num1 = attributeById.AttributeType.AttributeType == FieldTypes.ftFile || attributeById.AttributeType.AttributeType == FieldTypes.ftBlob ? attributeById.AddValue((object) defaultFileType) : attributeById.AddValue((object) null);
        attributeById.Index = num1;
        if (!(attributeById is IBlobReader blobReader))
          return (TreeNode) null;
        IDBAttribute attributeByGuid = attributable.GetAttributeByGuid(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545"));
        DateTime contentModifyDate = attributeByGuid == null || attributeByGuid.IsNull ? DateTime.MinValue : attributeByGuid.AsDateTime;
        List<object> objectList = new List<object>();
        FieldTypes attributeType = attributeById.AttributeType.AttributeType;
        BlobInformation bi = blobReader.OpenBlob(-1);
        long num2 = -1;
        if (attributeType == FieldTypes.ftFile || attributeType == FieldTypes.ftBlob)
        {
          num2 = Convert.ToInt64(attributeById.AsDouble);
          objectList.Add((object) num2);
        }
        long boxId = num2;
        AttributeSingleValueClass singleValueClass = new AttributeSingleValueClass(bi, boxId);
        if (attributeType == FieldTypes.ftFile)
          singleValueClass.InitializeColorText(defaultFileType, contentModifyDate);
        bool flag = (attributeById.AttributeType.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit;
        node = this.ConstructValueNode(attributeType, singleValueClass.bi, singleValueClass.ColorText, attributeById.ReadOnly | flag, objectList.ToArray());
        attributeTreeNode.Nodes.Add(node);
      }
    }
    return node;
  }

  private void SetGlobalProgressForm(FileAttributeProgressForm fapf) => this.fapfGlobal = fapf;

  private bool LockCommands
  {
    get => this.lockCommands;
    set
    {
      this.lockCommands = value;
      this.ProcessControlsStates();
    }
  }

  private void fapf_Break(object sender) => this.cancelProgressFlag = true;

  /// <summary>Добавление значения в атрибут</summary>
  /// <param name="tn">нод атрибута</param>
  /// <param name="files">список добавляемых файлов, если добавляем автоматически (напр, через DragDrop)</param>
  /// <returns></returns>
  private bool AddValueCustom(TreeNode tn, string[] files = null)
  {
    FileTypes fileTypes = FileTypes.ftNormal;
    if ((FieldTypes) ((object[]) tn.Tag)[1] == FieldTypes.ftFile)
    {
      bool isReadOnly = true;
      AttributeValueClass attributeValueClass = this.attributeValuesList.AttributeValueClassByAttributeID((int) ((object[]) tn.Tag)[0]);
      if (attributeValueClass != null)
        isReadOnly = attributeValueClass.attributeReadOnly;
      if (this.batef == null)
        this.batef = new BoxedAttributeTypeEditForm();
      this.batef.TopMost = true;
      try
      {
        if (this.batef.ShowDialog(tn.Name, isReadOnly) != DialogResult.OK)
          return false;
      }
      finally
      {
        this.batef.TopMost = false;
      }
      fileTypes = this.batef.SelectedFileType;
    }
    if (files == null)
    {
      this.openFileDialog.Title = LocalizationHolder.rm.GetString("Client.Core_937");
      this.openFileDialog.Multiselect = true;
      this.openFileDialog.FileName = string.Empty;
      if (this.openFileDialog.ShowDialog() != DialogResult.OK)
        return false;
      files = this.openFileDialog.FileNames;
    }
    if (files == null || files.Length == 0)
      return false;
    bool flag = true;
    using (FileAttributeProgressForm fapf = new FileAttributeProgressForm())
    {
      this.cancelProgressFlag = false;
      fapf.Break += new BreakEvent(this.fapf_Break);
      fapf.ShowProgress(LocalizationHolder.rm.GetString("FileAttributeUpload"), files.Length);
      this.LockCommands = true;
      try
      {
        for (int index = 0; index < files.Length; ++index)
        {
          TreeNode vtn = tn.Nodes.Count <= 0 || !(tn.Nodes[0].Text == FileAttributeEditForm.WithoutName) ? this.AddValue(tn, fileTypes) : tn.Nodes[0];
          try
          {
            this.UploadValueForeground(vtn, files[index], fileTypes, fapf);
            flag = false;
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
          if (this.cancelProgressFlag)
            break;
        }
      }
      finally
      {
        fapf.HideProgress();
        this.LockCommands = false;
      }
    }
    this.UpdateAttributeNode(tn);
    if (!flag)
      this.FireElementWasChangedSemafor();
    this.FireElementWasChangedEvent();
    return true;
  }

  private void OnAddValueMenuItem(object sender, EventArgs e)
  {
    if (this.treeView.SelectedNode == null)
      return;
    this.AddValueCustom(this.IsAttributeNode(this.treeView.SelectedNode) ? this.treeView.SelectedNode : this.treeView.SelectedNode.Parent);
  }

  private void OnDeleteValueMenuItem(object sender, EventArgs e)
  {
    if (this.treeView.SelectedNode == null || !this.IsValueNode(this.treeView.SelectedNode) || IMMessageBox.Show(MessageDialogs.msgConfirmDelete, LocalizationHolder.rm.GetString("FilePropertyEditor_ReallyDeleteAttributeValue"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
      if (attributable == null)
        return;
      IDBAttribute attributeById1 = attributable.GetAttributeByID((int) ((object[]) this.treeView.SelectedNode.Parent.Tag)[0]);
      if (attributeById1 != null)
      {
        string reallyDeleteFile = MessageDialogs.msgReallyDeleteFile;
        string msgConfirmDelete = MessageDialogs.msgConfirmDelete;
        if (attributeById1.ValuesCount == 1)
        {
          IDBAttributeTypeInfo4 attributeById2 = ClientCommons.GetAttributableType(this.elementType, this.attributableElement).Attributes.GetAttributeByID((int) ((object[]) this.treeView.SelectedNode.Parent.Tag)[0]);
          if (attributeById2 != null && attributeById2.Required == RequiredModes.AutoRequired)
          {
            int num = (int) IMMessageBox.Show(MessageDialogs.msgWarning, LocalizationHolder.rm.GetString("FilePropertyEditor_CantDeleteLastValueFromRequiredAttribute"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
            return;
          }
          if (IMMessageBox.Show(MessageDialogs.msgConfirmDelete, LocalizationHolder.rm.GetString("Client.Core_938"), MessageBoxButtons.OKCancel, IMMessageBoxImage.Warning) != DialogResult.OK)
            return;
          this.DeleteAttribute((int) ((object[]) this.treeView.SelectedNode.Parent.Tag)[0]);
          this.treeView.SelectedNode.Parent.Remove();
          this.propertyGrid.SelectedObject = (object) null;
          this.FireElementWasChangedEvent();
          return;
        }
        attributeById1.Index = this.treeView.SelectedNode.Index;
        if (!(attributeById1 is IBlobReader blobReader))
          return;
        if (blobReader.OpenBlob(-1).BlobID == ((CustomAttributeValueObject) this.treeView.SelectedNode.Tag).BlobInformation.BlobID)
        {
          attributeById1.DeleteValue();
          this.treeView.SelectedNode.Remove();
          this.FireElementWasChangedSemafor();
        }
        else
        {
          int num = (int) IMMessageBox.Show(MessageDialogs.msgInformation, LocalizationHolder.rm.GetString("Client.Core_939"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
          this.UpdateAttributeNode(this.treeView.SelectedNode.Parent);
        }
      }
    }
    this.FireElementWasChangedEvent();
  }

  private void UploadValueForeground(
    TreeNode vtn,
    string _filename,
    FileTypes filetype,
    FileAttributeProgressForm fapf)
  {
    bool flag1 = false;
    bool flag2 = _filename == null;
    if (flag2)
    {
      this.openFileDialog.Title = LocalizationHolder.rm.GetString("Client.Core_941");
      this.openFileDialog.Multiselect = false;
      this.openFileDialog.FileName = string.Empty;
      if (this.openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      _filename = this.openFileDialog.FileName;
    }
    this.SetGlobalProgressForm(fapf);
    int num = (int) ((object[]) vtn.Parent.Tag)[0];
    string filePathToImport = _filename;
    if (fapf != null)
    {
      if (!fapf.Visible)
        fapf.ShowProgress("", 1);
      fapf.NewFileProgress(_filename);
    }
    string oldFilename = ((CustomAttributeValueObject) vtn.Tag).BlobInformation.FileName;
    try
    {
      bool flag3 = true;
      while (flag3)
      {
        using (FileStream aSourceStream = new FileStream(_filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
          BlobInformation aBlobInformation = vtn.Tag is CustomAttributeBoxedValueObject ? new BlobInformation(0L, 0L, vtn.Tag is FileAttributeValueObject ? File.GetLastWriteTime(_filename) : DateTime.Now, vtn.Tag is FileAttributeValueObject ? this.GetFileNameFoBlobInformation(num, vtn.Index, filePathToImport) : string.Empty, ArcMethods.ZLibPacked, ((CustomAttributeValueObject) vtn.Tag).BlobInformation.Note, flag2 ? ((CustomAttributeValueObject) vtn.Tag).BlobInformation.FileType : filetype, ((CustomAttributeValueObject) vtn.Tag).BlobInformation.Author) : new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, vtn.Tag is CustomAttributeValueObject ? ((CustomAttributeValueObject) vtn.Tag).BlobInformation.Note : string.Empty);
          BlobProcWriter blobProcWriter = new BlobProcWriter(this.id, this.attributableElement, num, vtn.Index, 0, aBlobInformation, (Stream) aSourceStream, new BlobProcCustomClass.ProgressEventHandler(this.OnProgress), (BlobProcCustomClass.ThreadFinishEventHandler) null);
          this.ChecksumClear4File(vtn.Tag);
          try
          {
            blobProcWriter.WriteData();
          }
          catch (Exception ex)
          {
            if (ex is KernelExceptionID && (((KernelExceptionID) ex).ErrorID == 336 || ((KernelExceptionID) ex).ErrorID == 324))
            {
              if (this.autoRename)
              {
                filePathToImport = Path.Combine(Path.GetDirectoryName(_filename), FileAttributeEditForm.AutoRename(_filename));
                continue;
              }
              FileAttributeRenameForm attributeRenameForm = new FileAttributeRenameForm();
              string empty = string.Empty;
              string conflictFullName = _filename;
              ref string local = ref empty;
              switch (attributeRenameForm.ShowDialog(conflictFullName, out local))
              {
                case DialogResult.OK:
                  filePathToImport = Path.Combine(Path.GetDirectoryName(_filename), empty);
                  continue;
                case DialogResult.Cancel:
                  flag1 = true;
                  break;
                case DialogResult.Yes:
                  this.autoRename = true;
                  filePathToImport = Path.Combine(Path.GetDirectoryName(_filename), FileAttributeEditForm.AutoRename(_filename));
                  continue;
              }
            }
            throw;
          }
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            aBlobInformation = (BlobProcCustomClass.GetAttributeInterface(this.id, this.attributableElement, num, vtn.Index, sessionKeeper.Session) as IBlobReader).OpenBlob(-1);
          string newFilename = aBlobInformation.FileName;
          FileTypes fileType = aBlobInformation.FileType;
          if (vtn.TreeView != null)
          {
            ((CustomAttributeValueObject) vtn.Tag).BlobInformation = aBlobInformation;
            this.ChecksumClear4File(vtn.Tag);
            this.ChangeValueNodeTextAndImage(vtn, aBlobInformation.FileName);
            if (this.treeView.SelectedNode == vtn)
              this.propertyGrid.SelectedObject = this.treeView.SelectedNode.Tag;
          }
          this.FireElementWasChangedEvent();
          if (this.fileReplace && ApplicationServices.Container.GetService(typeof (INotificationService)) is INotificationService service)
            service.FireEvent((object) this, (NotificationEventArgs) new FileReplacedEventArgs("FileReplaced", this.AttributableElement, this.Id, this.ElementType, num, vtn.Index, filetype));
          if (!Path.GetFileName(oldFilename).Equals(Path.GetFileName(newFilename), StringComparison.InvariantCultureIgnoreCase))
          {
            if (fileType == FileTypes.ftNormal)
              this.Invoke((Delegate) (() => this.CheckAndRenameRedlineFilenames(this.IsValueNode(vtn) ? vtn.Parent : vtn, oldFilename, newFilename)));
          }
        }
        flag3 = false;
      }
    }
    catch
    {
      if (vtn != null && vtn.TreeView != null && vtn.Text == FileAttributeEditForm.WithoutName)
      {
        if (vtn.Tag is FileAttributeValueObject tag)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
            if (attributable != null)
            {
              IDBAttribute attributeById = attributable.GetAttributeByID(num);
              if (attributeById != null)
              {
                if (attributeById.ValuesCount > 1)
                {
                  for (int index = 0; index < attributeById.ValuesCount; ++index)
                  {
                    attributeById.Index = index;
                    if (attributeById is IBlobReader blobReader && blobReader.OpenBlob(-1).BlobID == tag.BlobInformation.BlobID)
                    {
                      attributeById.DeleteValue();
                      break;
                    }
                  }
                }
              }
            }
          }
        }
        vtn.Remove();
      }
      if (flag1)
        return;
      throw;
    }
    finally
    {
      this.SetGlobalProgressForm((FileAttributeProgressForm) null);
    }
  }

  public static string AutoRename(string filename)
  {
    return $"{Path.GetFileNameWithoutExtension(filename)}_{(new Random().Next(22767) + 10000).ToString()}{Path.GetExtension(filename)}";
  }

  /// <summary>
  /// Вычисляет имя импортируемого файла в файловом атрибуте.
  /// </summary>
  /// <param name="fileAttributeId">Идентификатор файлового атрибута</param>
  /// <param name="fileAttributeIndex">Индекс файла внутри файлового атрибута</param>
  /// <param name="filePathToImport">Путь к импортируемому файлу</param>
  /// <returns>Имя файла в файловом атрибуте</returns>
  private string GetFileNameFoBlobInformation(
    int fileAttributeId,
    int fileAttributeIndex,
    string filePathToImport)
  {
    if (this.attributableElement == AttributableElements.Object)
    {
      string areaPath = ClientContext.FileVault.WorkArea.AreaPath;
      if (PathUtils.IsPlacedIn(filePathToImport, areaPath))
        return PathUtils.GetRelativePath(filePathToImport, areaPath, RelativePathOptions.ThrowIfNotPossible);
      if (fileAttributeIndex != 0)
      {
        string masterFileName = ClientContext.FileVault.DBFilesInfo.GetMasterFileName(this.id, false);
        if (!string.IsNullOrEmpty(masterFileName))
          return Path.Combine(Path.GetDirectoryName(masterFileName), Path.GetFileName(filePathToImport));
      }
    }
    return Path.GetFileName(filePathToImport);
  }

  private void OnProgress(BlobProcCustomClass sender, BlobProcessorMode mode, int progress)
  {
    if (this.fapfGlobal != null)
      this.fapfGlobal.FileProgress = progress;
    Application.DoEvents();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="tn"></param>
  /// <param name="filename">при null берется имеющийся filetype</param>
  /// <param name="fileTypes"></param>
  /// <returns></returns>
  private bool UploadValueCustom(TreeNode tn, string filename = null, FileTypes fileTypes = FileTypes.ftNormal)
  {
    using (FileAttributeProgressForm fapf = new FileAttributeProgressForm())
    {
      this.cancelProgressFlag = false;
      fapf.Break += new BreakEvent(this.fapf_Break);
      this.LockCommands = true;
      this.fileReplace = true;
      try
      {
        this.UploadValueForeground(tn, filename, fileTypes, fapf);
        return true;
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
        return false;
      }
      finally
      {
        this.fileReplace = false;
        this.LockCommands = false;
        fapf.HideProgress();
      }
    }
  }

  private void OnUploadValueMenuItem(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null || !this.IsValueNode(selectedNode))
      return;
    this.UploadValueCustom(selectedNode);
  }

  private void OnDownloadValueMenuItem(object sender, EventArgs e)
  {
    if (this.treeView.SelectedNode == null || !this.IsValueNode(this.treeView.SelectedNode))
      return;
    BlobInformation blobInformation = ((CustomAttributeValueObject) this.treeView.SelectedNode.Tag).BlobInformation;
    this.saveFileDialog.FileName = blobInformation.FileName != string.Empty ? blobInformation.FileName : blobInformation.BlobID.ToString() + ".blb";
    this.saveFileDialog.DefaultExt = Path.GetExtension(this.saveFileDialog.FileName);
    if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    if (this.downloads.ContainsValue((object) this.saveFileDialog.FileName))
    {
      int num1 = (int) IMMessageBox.Show(MessageDialogs.msgInformation, LocalizationHolder.rm.GetString("Client.Core_944"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
    }
    else
    {
      FileStream aDestStream;
      try
      {
        aDestStream = new FileStream(this.saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
      }
      catch
      {
        int num2 = (int) IMMessageBox.Show(MessageDialogs.msgError, LocalizationHolder.rm.GetString("Client.Core_945") + this.saveFileDialog.FileName, MessageBoxButtons.OK, IMMessageBoxImage.Error);
        return;
      }
      BlobProcReader blobProcReader = new BlobProcReader(this.id, this.attributableElement, (int) ((object[]) this.treeView.SelectedNode.Parent.Tag)[0], this.treeView.SelectedNode.Index, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, new BlobProcCustomClass.ThreadFinishEventHandler(this.DownloadFinished));
      ((IBackgroundTaskView) ServicesManager.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) new BlobProcessorTask(string.Format(LocalizationHolder.rm.GetString("Client.Core_946"), (object) this.saveFileDialog.FileName), (BlobProcCustomClass) blobProcReader));
      lock (this.downloads)
      {
        this.downloads.Add((object) blobProcReader, (object) this.saveFileDialog.FileName);
        blobProcReader.ReadDataThread(true);
      }
    }
  }

  private void OnUpdateMenuItem(object sender, EventArgs e)
  {
    this.LoadElement(this.id, this.AttributableElement, this.xFile, this.xBlob, this.xShortBlob);
  }

  private void UpdateAttributeNode(TreeNode tn)
  {
    if (tn == null || !this.IsAttributeNode(tn))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
      if (attributable == null)
        return;
      this.attributableReadOnly = attributable.ReadOnly;
      IDBAttribute attributeById = attributable.GetAttributeByID((int) ((object[]) tn.Tag)[0]);
      if (attributeById != null)
      {
        tn.Text = attributeById.Name;
        IDBAttribute attributeByGuid = attributable.GetAttributeByGuid(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545"));
        AttributeValueClass avc = new AttributeValueClass(attributeById, attributeByGuid);
        int count = tn.Nodes.Count;
        this.AddValuesToNode(tn, avc, false);
        for (int index = 0; index < count; ++index)
          tn.Nodes.RemoveAt(0);
      }
      else
        tn.Remove();
    }
    this.ProcessControlsStates();
  }

  private void OnEditValueMenuItem(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null || !this.IsValueNode(selectedNode) || this.FilePreprocessIsHandled(selectedNode, LaunchType.Edit))
      return;
    if (this.IsFileAttributeNode(selectedNode.Parent))
    {
      this.LaunchFile(selectedNode, LaunchType.Edit);
    }
    else
    {
      using (new DynamicScope())
        this.ShellExecuteLaunch(selectedNode, LaunchType.Edit);
    }
  }

  private void OnViewValueMenuItem(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null || !this.IsValueNode(selectedNode) || this.FilePreprocessIsHandled(selectedNode, LaunchType.View))
      return;
    if (this.IsFileAttributeNode(selectedNode.Parent))
    {
      this.LaunchFile(selectedNode, LaunchType.View);
    }
    else
    {
      using (new DynamicScope())
        this.ShellExecuteLaunch(selectedNode, LaunchType.View);
    }
  }

  private void ShellExecuteLaunch(TreeNode fileNode, LaunchType launchType)
  {
    BlobInformation blobInformation = ((CustomAttributeValueObject) fileNode.Tag).BlobInformation;
    ClientContext.LaunchActions.LaunchByShell(new LaunchParams(launchType, this.id, this.elementType, VersionsRuleSources.GetCurrentWindowRule())
    {
      ObjectFileName = blobInformation.FileName
    });
  }

  private bool FilePreprocessIsHandled(TreeNode fileNode, LaunchType launchType)
  {
    IProcessFileService service = ServicesManager.GetService<IProcessFileService>(false);
    if (service == null)
      return false;
    BlobInformation blobInformation = ((CustomAttributeValueObject) fileNode.Tag).BlobInformation;
    FileProcessEventArgs eventArgs = new FileProcessEventArgs(this.id, this.attributableElement, this.elementType, (int) ((object[]) fileNode.Parent.Tag)[0], fileNode.Index, blobInformation, launchType);
    service.FireFileProcessEvent(eventArgs);
    return eventArgs.IsHandled;
  }

  private void LaunchFile(TreeNode fileNode, LaunchType launchType)
  {
    BlobInformation blobInformation = ((CustomAttributeValueObject) fileNode.Tag).BlobInformation;
    bool flag = fileNode.Index == 0;
    using (new DynamicScope())
    {
      LaunchActionServiceVars.RootObjectMode.Declare(!flag);
      if (this.attributableElement == AttributableElements.Object && !flag && blobInformation.FileType == FileTypes.ftRedlining && PathUtils.IsSamePath(Path.GetExtension(blobInformation.FileName), ".rxml") && ServiceUtils.GetService<IClientRxmlService>((object) ApplicationServices.Container, true).TryOpenRxmlViewer(this.id))
        return;
      this.ShellExecuteLaunch(fileNode, launchType);
    }
  }

  private void ReplaceHashtableValue(Hashtable ht, object o1, object o2)
  {
    foreach (DictionaryEntry dictionaryEntry in ht)
    {
      if (dictionaryEntry.Value.Equals(o1))
      {
        ht[dictionaryEntry.Key] = o2;
        break;
      }
    }
  }

  private void RemoveHashtableValue(Hashtable ht, object o1)
  {
    foreach (DictionaryEntry dictionaryEntry in ht)
    {
      if (dictionaryEntry.Value.Equals(o1))
      {
        ht.Remove(dictionaryEntry.Key);
        break;
      }
    }
  }

  private MultiValueModes GetMultiValueMode(int attrId)
  {
    MultiValueModes multiValueMode = MultiValueModes.SingleValue;
    if (this.possibleAttributes != null)
    {
      DataRow[] dataRowArray = this.possibleAttributes.Select("F_ATTRIBUTE_ID=" + attrId.ToString());
      if (dataRowArray.Length != 0)
        multiValueMode = (MultiValueModes) Convert.ToInt16(dataRowArray[0]["F_MULTIPLE_VALUED"]);
    }
    return multiValueMode;
  }

  private void contextMenu1_Popup(object sender, EventArgs e)
  {
    this.ProcessControlsStates(this.treeView.GetNodeAt(this.treeView.PointToClient(Control.MousePosition)));
  }

  private void ProcessControlsStates() => this.ProcessControlsStates((TreeNode) null);

  private void ProcessControlsStates(TreeNode tn)
  {
    bool flag1 = false;
    if (tn == null)
      tn = this.treeView.SelectedNode;
    if (tn == null)
      flag1 = true;
    if (tn != this.treeView.SelectedNode)
      this.treeView.SelectedNode = tn;
    int num1 = this.treeView.SelectedNode != null ? 1 : 0;
    bool flag2 = num1 != 0 && this.IsAttributeNode(this.treeView.SelectedNode);
    bool flag3 = num1 != 0 && this.IsValueNode(this.treeView.SelectedNode);
    int num2 = -1;
    AttributeValueClass attributeValueClass = (AttributeValueClass) null;
    if (num1 != 0)
    {
      num2 = flag2 ? (int) ((object[]) this.treeView.SelectedNode.Tag)[0] : (int) ((object[]) this.treeView.SelectedNode.Parent.Tag)[0];
      attributeValueClass = this.attributeValuesList.AttributeValueClassByAttributeID(num2);
    }
    bool flag4 = num1 != 0 && this.GetMultiValueMode(num2) == MultiValueModes.MultiValues;
    this.bCrc.Enabled = flag3 && this.treeView.SelectedNode.Tag is FileAttributeValueObject && !this.lockCommands;
    this.bCrcAll.Enabled = !this.lockCommands;
    bool flag5 = attributeValueClass != null && !attributeValueClass.attributeReadOnly;
    this.mbiAddAttr.Visible = true;
    this.mbiAddAttr.Enabled = this.mbiAddAttr.Visible && !this.lockCommands;
    this.addAttrToolBtn.Enabled = this.mbiAddAttr.Enabled;
    this.mbiDelAttr.Visible = !flag1 & flag2;
    this.mbiDelAttr.Enabled = this.mbiDelAttr.Visible & flag5 && !this.lockCommands;
    this.delAttrToolBtn.Enabled = this.mbiDelAttr.Enabled;
    this.mbiClearAttr.Visible = !flag1 & flag2;
    this.mbiClearAttr.Enabled = this.mbiClearAttr.Visible & flag5 && !this.lockCommands;
    this.mbiAddVal.Visible = ((flag1 ? 0 : (flag2 | flag3 ? 1 : 0)) & (flag4 ? 1 : 0)) != 0;
    this.mbiAddVal.Enabled = this.mbiAddVal.Visible && !this.lockCommands;
    this.addValToolBtn.Enabled = this.mbiAddVal.Enabled;
    this.mbiDelVal.Visible = !flag1 & flag3;
    this.mbiDelVal.Enabled = this.mbiDelVal.Visible && !this.lockCommands;
    this.delValToolBtn.Enabled = this.mbiDelVal.Enabled;
    this.mbiUploadVal.Visible = !flag1 & flag3;
    bool flag6 = true;
    if (flag3)
    {
      if (this.treeView.SelectedNode.Tag is CustomAttributeBoxedValueObject)
      {
        FileTypePropertyClass typePropertyClass = (FileTypePropertyClass) ((CustomAttributeBoxedValueObject) this.treeView.SelectedNode.Tag).FiletypePropDescriptor.GetValue((object) this);
        if (typePropertyClass == null || typePropertyClass != null && typePropertyClass.FileType == FileTypes.ftNormal)
          flag6 = flag5;
      }
      else
        flag6 = flag5;
    }
    this.mbiUploadVal.Enabled = this.mbiUploadVal.Visible & flag6 && !this.lockCommands;
    this.upValToolBtn.Enabled = this.mbiUploadVal.Enabled;
    this.mbiDownloadVal.Visible = !flag1 & flag3 && !this.lockCommands;
    this.downValToolBtn.Enabled = this.mbiDownloadVal.Visible;
    this.mbiUpdate.Visible = !this.lockCommands;
    this.updateToolBtn.Enabled = this.mbiUpdate.Visible;
    bool flag7 = !flag1 & flag3 && this.IsFileTypeAttributeNode(tn.Parent);
    this.mbiFileToolsSeparator.Visible = flag7;
    this.mbiEditValue.Visible = flag7;
    this.editValToolBtn.Enabled = flag7;
    this.mbiViewValue.Visible = flag7;
    this.viewValToolBtn.Enabled = flag7;
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    try
    {
      this.propertyGrid.SelectedObject = this.treeView.SelectedNode == null || !(this.treeView.SelectedNode.Tag is CustomAttributeValueObject) ? (object) null : this.treeView.SelectedNode.Tag;
      this.ProcessControlsStates();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (this.blockValueChanged)
      return;
    bool flag = true;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.attributableElement, sessionKeeper.Session);
        if (attributable == null)
          return;
        IDBAttribute attributeById = attributable.GetAttributeByID((int) ((object[]) this.treeView.SelectedNode.Parent.Tag)[0]);
        if (attributeById == null)
          return;
        attributeById.Index = this.treeView.SelectedNode.Index;
        if (!(attributeById is IBlobReader blobReader))
          return;
        BlobInformation blobInformation = blobReader.OpenBlob(-1);
        if (blobInformation.BlobID != ((CustomAttributeValueObject) this.treeView.SelectedNode.Tag).BlobInformation.BlobID)
        {
          this.LoadElement(this.id, this.AttributableElement);
        }
        else
        {
          string oldFilename = blobInformation.FileName;
          string newFilename = ((CustomAttributeValueObject) this.treeView.SelectedNode.Tag).BlobInformation.FileName;
          try
          {
            (attributeById as IBlobWriter).OpenBlob(((CustomAttributeValueObject) this.treeView.SelectedNode.Tag).BlobInformation, true);
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
            throw;
          }
          flag = false;
          this.ChangeValueNodeTextAndImage(this.treeView.SelectedNode, ((CustomAttributeValueObject) this.treeView.SelectedNode.Tag).BlobInformation.FileName);
          if (Path.GetFileName(oldFilename).Equals(Path.GetFileName(newFilename), StringComparison.InvariantCultureIgnoreCase) || blobInformation.FileType != FileTypes.ftNormal)
            return;
          this.Invoke((Delegate) (() => this.CheckAndRenameRedlineFilenames(this.IsValueNode(this.treeView.SelectedNode) ? this.treeView.SelectedNode.Parent : this.treeView.SelectedNode, oldFilename, newFilename)));
        }
      }
    }
    finally
    {
      if (flag)
      {
        this.blockValueChanged = true;
        try
        {
          if (this.biAssigned)
          {
            BlobInformation blobInformation = ((CustomAttributeValueObject) this.propertyGrid.SelectedObject).BlobInformation with
            {
              FileName = this.biSafe.FileName,
              Note = this.biSafe.Note,
              FileType = this.biSafe.FileType
            };
            CustomAttributeValueObject selectedObject = (CustomAttributeValueObject) this.propertyGrid.SelectedObject;
            selectedObject.BlobInformation = blobInformation;
            this.propertyGrid.SelectedObject = (object) null;
            this.propertyGrid.SelectedObject = (object) selectedObject;
          }
        }
        finally
        {
          this.blockValueChanged = false;
        }
      }
    }
  }

  /// <summary>
  /// При изменении имени основного файла проверить, не надо ли менять имена файлов комментариев.
  /// </summary>
  /// <param name="attributeNode">нод атрибута</param>
  /// <param name="oldNormalFilename">старое имя основного файла</param>
  /// <param name="newNormalFilename">новое имя основного файла</param>
  private void CheckAndRenameRedlineFilenames(
    TreeNode attributeNode,
    string oldNormalFilename,
    string newNormalFilename)
  {
    for (int index = 0; index < attributeNode.Nodes.Count; ++index)
    {
      if (attributeNode.Nodes[index].Tag is CustomAttributeValueObject tag && tag.BlobInformation.FileType == FileTypes.ftRedlining && Path.GetFileNameWithoutExtension(tag.BlobInformation.FileName).Equals(Path.GetFileName(oldNormalFilename), StringComparison.InvariantCultureIgnoreCase))
      {
        if (IMMessageBox.Show("Переименование", $"Переименовать также файл замечаний {tag.BlobInformation.FileName} ?", MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
        {
          try
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBAttribute attributeInterface = BlobProcCustomClass.GetAttributeInterface(this.id, this.attributableElement, (int) ((object[]) attributeNode.Tag)[0], index, sessionKeeper.Session);
              BlobInformation blobInfo = (attributeInterface as IBlobReader).OpenBlob(-1);
              blobInfo.FileName = Path.Combine(Path.GetDirectoryName(blobInfo.FileName), Path.GetFileName(newNormalFilename) + Path.GetExtension(blobInfo.FileName));
              (attributeInterface as IBlobWriter).OpenBlob(blobInfo, true);
              ((CustomAttributeValueObject) attributeNode.Nodes[index].Tag).BlobInformation = blobInfo;
              this.ChangeValueNodeTextAndImage(attributeNode.Nodes[index], blobInfo.FileName);
            }
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
        }
      }
    }
  }

  private void propertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
  {
    if (this.propertyGrid.SelectedObject == null)
    {
      this.biAssigned = false;
    }
    else
    {
      this.biAssigned = true;
      this.biSafe = ((CustomAttributeValueObject) this.propertyGrid.SelectedObject).BlobInformation;
    }
  }

  private void ChecksumClear4File(object tag)
  {
    if (!(tag is FileAttributeValueObject))
      return;
    ((FileAttributeValueObject) tag).ClearChecksum();
  }

  public void DownloadFinished(
    BlobProcCustomClass sender,
    bool result,
    object message,
    Exception exception,
    BlobInformation bi)
  {
    string download = (string) this.downloads[(object) sender];
    if (download != null)
    {
      if (result)
        ClientEventLog.AddEvent4Attributable(this.id, this.attributableElement, bi.FileName, ActionType.SaveToDisk, EventlogRecordType.AccessGranted);
      try
      {
        File.SetLastWriteTime(download, bi.ModifyDate);
      }
      catch
      {
        int num = (int) IMMessageBox.Show(MessageDialogs.msgError, LocalizationHolder.rm.GetString("Client.Core_947") + bi.ModifyDate.ToString((IFormatProvider) CultureInfo.InvariantCulture) + LocalizationHolder.rm.GetString("Client.Core_948") + download, MessageBoxButtons.OK, IMMessageBoxImage.Error);
      }
    }
    lock (this.downloads)
      this.downloads.Remove((object) sender);
    if (result)
      return;
    ClientEventLog.AddEvent4Attributable(this.id, this.attributableElement, bi.FileName, ActionType.SaveToDisk, EventlogRecordType.AccessDenied);
    if (exception == null)
      return;
    ExceptionHelper.ExceptionService.ShowException(exception);
  }

  private void UnsubscribeThreadEvents()
  {
    lock (this.downloads)
    {
      foreach (DictionaryEntry download in this.downloads)
      {
        if (download.Key is BlobProcReader)
          ((BlobProcCustomClass) download.Key).ThreadFinish -= new BlobProcCustomClass.ThreadFinishEventHandler(this.DownloadFinished);
      }
    }
  }

  private void ChangeValueNodeTextAndImage(TreeNode tn, string newName)
  {
    bool flag = false;
    if (tn.Tag is BlobAttributeValueObject || tn.Tag is ShortBlobAttributeValueObject)
      flag = true;
    tn.Text = flag ? ((CustomAttributeValueObject) tn.Tag).BlobInformation.BlobID.ToString() : newName;
    tn.ImageIndex = 0;
    if (!flag && tn.Text != FileAttributeEditForm.WithoutName)
    {
      string lower = Path.GetExtension(newName).ToLower();
      tn.ImageIndex = FileAttributeStatics.GetExtImageIndex(lower);
    }
    tn.SelectedImageIndex = tn.ImageIndex;
  }

  /// <summary>взводит флаг необходимости известить мир об изменении</summary>
  private void FireElementWasChangedSemafor() => this.wasChangedEvendNeeded = true;

  /// <summary>
  /// если флаг взведен по FireElementWasChangedSemafor выполнить рассылку сообщения
  /// </summary>
  private void FireElementWasChangedEvent()
  {
    this.LoadFileHistory();
    if (!this.wasChangedEvendNeeded)
      return;
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
    {
      switch (this.attributableElement)
      {
        case AttributableElements.Object:
          service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.id));
          break;
        case AttributableElements.Relation:
          service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", this.id));
          break;
      }
    }
    this.wasChangedEvendNeeded = false;
  }

  /// <summary>история выводится для всех файлов объекта</summary>
  private void LoadFileHistory()
  {
    if (!this.isObjectHistoryLoad)
      return;
    this.historyView.LoadObjectHistory(this.id);
  }

  private void bCrc_Click(object sender, EventArgs e) => this.ProcessCrcCalc(sender);

  private void bCrcAll_Click(object sender, EventArgs e) => this.ProcessCrcCalc(sender);

  /// <summary>
  /// подготовка и проверка значения на возможность расчета контрольной суммы
  /// </summary>
  /// <param name="tag"></param>
  /// <returns></returns>
  private bool PrepareTag4ChecksumCalc(object tag)
  {
    if (tag is FileAttributeValueObject)
    {
      using (PropertyGrid component = new PropertyGrid())
      {
        try
        {
          FileAttributeValueObject attributeValueObject = tag as FileAttributeValueObject;
          component.SelectedObject = (object) attributeValueObject;
          if (attributeValueObject.FiletypePropDescriptor.GetValue((object) component) is FileTypePropertyClass)
            return true;
        }
        finally
        {
          component.SelectedObject = (object) null;
        }
      }
    }
    return false;
  }

  private Guid AddTaskToList(
    IChecksumsService _iChecksumsService,
    List<FileAttributeValueObject> _calcList,
    List<Guid> _taskList,
    TreeNode _tn,
    Guid _sessionGUID,
    long _attributableID,
    AttributableElements _attributableElement,
    int _attrID,
    int _attrIndex,
    ChecksumAlgorithm _checksumAlgorithm)
  {
    Guid list = Guid.Empty;
    if (_tn.Tag is FileAttributeValueObject && this.PrepareTag4ChecksumCalc(_tn.Tag))
    {
      FileAttributeValueObject tag = (FileAttributeValueObject) _tn.Tag;
      list = _iChecksumsService.CalcChecksum(_sessionGUID, _attributableID, _attributableElement, _attrID, _attrIndex, _checksumAlgorithm);
      if (list != Guid.Empty)
      {
        tag.ChecksumTaskGuid = list;
        _calcList.Add(tag);
        _taskList.Add(list);
      }
    }
    return list;
  }

  private void ProcessCrcCalc(object sender)
  {
    List<FileAttributeValueObject> _calcList = new List<FileAttributeValueObject>();
    List<Guid> _taskList = new List<Guid>();
    bool flag1 = sender.Equals((object) this.bCrcAll);
    int num1 = this.treeView.SelectedNode != null ? 1 : 0;
    bool flag2 = num1 != 0 && this.IsAttributeNode(this.treeView.SelectedNode);
    bool flag3 = num1 != 0 && this.IsValueNode(this.treeView.SelectedNode);
    int num2 = -1;
    if (num1 != 0)
    {
      num2 = flag2 ? (int) ((object[]) this.treeView.SelectedNode.Tag)[0] : (int) ((object[]) this.treeView.SelectedNode.Parent.Tag)[0];
      this.attributeValuesList.AttributeValueClassByAttributeID(num2);
    }
    if (num1 != 0)
    {
      int multiValueMode = (int) this.GetMultiValueMode(num2);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IChecksumsService)) is IChecksumsService customService))
        return;
      ChecksumAlgorithm checksumAlgorithm = ((ChecksumAlgorithmPropertyClass) this.cbCrcAlgorithm.ComboBox.SelectedItem).ChecksumAlgorithm;
      if (!flag1)
      {
        if (flag3)
        {
          TreeNode selectedNode = this.treeView.SelectedNode;
          this.AddTaskToList(customService, _calcList, _taskList, selectedNode, sessionKeeper.Session.SessionGUID, this.id, this.attributableElement, num2, selectedNode.Index, checksumAlgorithm);
        }
        else if (flag2)
        {
          for (int index = 0; index < this.treeView.SelectedNode.Nodes.Count; ++index)
          {
            TreeNode node = this.treeView.SelectedNode.Nodes[index];
            this.AddTaskToList(customService, _calcList, _taskList, node, sessionKeeper.Session.SessionGUID, this.id, this.attributableElement, num2, node.Index, checksumAlgorithm);
          }
        }
      }
      else
      {
        for (int index1 = 0; index1 < this.treeView.Nodes.Count; ++index1)
        {
          if (this.IsAttributeNode(this.treeView.Nodes[index1]))
          {
            int _attrID = (int) ((object[]) this.treeView.Nodes[index1].Tag)[0];
            for (int index2 = 0; index2 < this.treeView.Nodes[index1].Nodes.Count; ++index2)
            {
              TreeNode node = this.treeView.Nodes[index1].Nodes[index2];
              this.AddTaskToList(customService, _calcList, _taskList, node, sessionKeeper.Session.SessionGUID, this.id, this.attributableElement, _attrID, node.Index, checksumAlgorithm);
            }
          }
        }
      }
      int num3 = 0;
      while (_taskList.Count > 0)
      {
        int index = 0;
        while (index < _taskList.Count)
        {
          ChecksumTaskProgress checksumTaskProgress = customService.GetChecksumTaskProgress(_taskList[index]);
          if (checksumTaskProgress == null || checksumTaskProgress.Operation == ChecksumOperationType.Error || checksumTaskProgress.Operation == ChecksumOperationType.Finished)
          {
            _taskList.RemoveAt(index);
            this.propertyGrid.Refresh();
          }
          else
            ++index;
        }
        ++num3;
        if (num3 != 1200)
          Thread.Sleep(250);
        else
          break;
      }
      for (int index = 0; index < _calcList.Count; ++index)
      {
        if (_calcList[index].ChecksumPropDescriptor.GetValue((object) this) is ChecksumPgPropertyClass checksumPgPropertyClass)
          checksumPgPropertyClass.RereadService();
      }
      this.propertyGrid.Refresh();
    }
  }

  private TreeNode GetAttributeNode(int attributeId)
  {
    TreeNode attributeNode = (TreeNode) null;
    for (int index = 0; index < this.treeView.Nodes.Count; ++index)
    {
      if (this.treeView.Nodes[index].Tag is object[] && (int) (this.treeView.Nodes[index].Tag as object[])[0] == attributeId)
      {
        attributeNode = this.treeView.Nodes[index];
        break;
      }
    }
    return attributeNode;
  }

  private void treeView_DragOver(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (this.lockCommands)
      return;
    TreeNode nodeAt = this.treeView.GetNodeAt(this.treeView.PointToClient(Control.MousePosition));
    if (nodeAt == null || !e.Data.GetDataPresent(DataFormats.FileDrop))
      return;
    bool flag = this.IsAttributeNode(nodeAt);
    int num = flag ? (int) ((object[]) nodeAt.Tag)[0] : (int) ((object[]) nodeAt.Parent.Tag)[0];
    if (this.attributeValuesList.AttributeValueClassByAttributeID(num).attributeReadOnly)
      return;
    if (!flag)
    {
      if (((IEnumerable<string>) (string[]) e.Data.GetData(DataFormats.FileDrop)).Count<string>() != 1)
        return;
      e.Effect = DragDropEffects.Copy;
    }
    else
    {
      if (this.GetMultiValueMode(num) != MultiValueModes.MultiValues)
        return;
      e.Effect = DragDropEffects.Copy;
    }
  }

  private void treeView_DragDrop(object sender, DragEventArgs e)
  {
    TreeNode tn = this.treeView.GetNodeAt(this.treeView.PointToClient(Control.MousePosition));
    if (tn == null || !e.Data.GetDataPresent(DataFormats.FileDrop))
      return;
    string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
    files = this.SelectFilesOnly(files);
    if (((IEnumerable<string>) files).Count<string>() == 0)
      return;
    int num1 = this.IsAttributeNode(tn) ? 1 : 0;
    if (num1 == 0)
    {
      int num2 = (int) ((object[]) tn.Parent.Tag)[0];
    }
    else
    {
      int num3 = (int) ((object[]) tn.Tag)[0];
    }
    if (num1 != 0)
    {
      this.BeginInvoke((Delegate) (() => this.AddValueCustom(tn, files)));
    }
    else
    {
      FileTypes fileType = FileTypes.ftNormal;
      if (tn.Tag is CustomAttributeBoxedValueObject)
        fileType = ((CustomAttributeValueObject) tn.Tag).BlobInformation.FileType;
      this.BeginInvoke((Delegate) (() =>
      {
        if (!IMMessageBox.Show("Подтвердите действие", "Выполнить перезапись значения атрибута?", new IMMessageBoxButton[2]
        {
          new IMMessageBoxButton("Да", DialogResult.Yes),
          new IMMessageBoxButton("Нет", DialogResult.No)
        }, IMMessageBoxImage.Question, (Form) this).Equals((object) DialogResult.Yes))
          return;
        this.UploadValueCustom(tn, files[0], fileType);
      }));
    }
  }

  private string[] SelectFilesOnly(string[] files)
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < files.Length; ++index)
    {
      if (!Directory.Exists(files[index]))
        stringList.Add(files[index]);
    }
    return stringList.ToArray();
  }
}
