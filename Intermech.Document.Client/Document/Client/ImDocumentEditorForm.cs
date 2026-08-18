// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ImDocumentEditorForm
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Redline;
using Intermech.Client.Core.Redline.Controls;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Document.Client.UI;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Redline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Окно клиента документа</summary>
public class ImDocumentEditorForm : ImDocumentEditorFormBase, IOpenAsObjectSupport
{
  protected BarManager barManager;
  protected ToolBarContainer leftBarDock;
  protected ToolBarContainer rightBarDock;
  protected ToolBarContainer bottomBarDock;
  protected ToolBarContainer topBarDock;
  /// <summary>Панель "Формат"</summary>
  protected Intermech.Bars.ToolBar formatToolBar;
  /// <summary>Панель "Таблицы"</summary>
  protected Intermech.Bars.ToolBar tableToolBar;
  /// <summary>Панель "Элементы"</summary>
  protected Intermech.Bars.ToolBar pageElementsToolBar;
  private DockManager dockManager;
  protected TransparentRedlineView redlineView;
  protected ImDocumentRedlineController redlineController;
  protected RedlineToolBarPresenter redlineToolBarPresenter;
  private ImDocumentRedlineNotesDlg redlineEditingDlg;
  private DockContainer leftDock;
  private DockContainer rightDock;
  private DockContainer bottomDock;
  private DockContainer topDock;
  private InsertSymbolDockControl insertSymbolDlg;
  private DocumentTreeViewDlg docTreeViewDlg;
  protected PropertyGridForm propertyGrid;
  private DockControlLayoutSettings propertyGridSettings = new DockControlLayoutSettings();
  private DockControlLayoutSettings docTreeViewSettings = new DockControlLayoutSettings();
  private DockControlLayoutSettings insertSymbolViewSettings = new DockControlLayoutSettings();
  private DockControlLayoutSettings redlineEditingSettings = new DockControlLayoutSettings();
  /// <summary>Панель "Навигация"</summary>
  protected Intermech.Bars.ToolBar navigateToolbar;
  /// <summary>Панель "Красный карандаш"</summary>
  protected Intermech.Bars.ToolBar redlineOnOffToolbar;
  private bool _activated;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private bool baseEditCommandsEnabled;
  private bool tableEditCommandsEnabled;
  private long documentID = -1;
  private Guid documentGuid = Guid.Empty;
  private int documentType = -1;
  private string documentName;
  private string documentDesignation;
  private bool askForSaveBeforeClose = true;
  protected bool updateReferenceByNotificationService = true;
  protected bool barManagerInitializing;

  public override DockManager DockManager => this.dockManager;

  public InsertSymbolDockControl InsertSymbolDlg => this.insertSymbolDlg;

  public DocumentTreeViewDlg DocTreeViewDlg => this.docTreeViewDlg;

  public PropertyGridForm PropertyGridDlg => this.propertyGrid;

  /// <summary>Выполнить инициализацию формы</summary>
  protected override void Init()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.Guid = DocumentEditorPlugin.ImDocumentEditorFormGuid;
    this.SetBaseEditCommandsEnabled(true, false);
    if (this.ExternalEditor == null)
      this.ExternalEditor = (IExternalEditor) new DocumentExternalEditor();
    if (this.DocumentControl != null)
      this.DocumentControl.GetCustomElementContextMenu += new GetCustomElementContextMenu_EventHandler(this.DocumentControl_GetCustomElementContextMenu);
    this.InitBarManager();
    IBlackWidthService service = (IBlackWidthService) ServicesManager.GetService(typeof (IBlackWidthService));
    if (service != null)
      service.Changed += new EventHandler(this.BlackWidthService_Changed);
    DocumentContainer documentContainer = new DocumentContainer();
    documentContainer.Guid = new Guid("{18AF91FE-0D49-4EF0-8A42-FB0A6A1F5E5B}");
    documentContainer.Manager = this.DockManager;
    this.DockManager.DocumentContainer = documentContainer;
    if (DocumentMenuHelper.DockManager != null)
    {
      this.DockManager.Renderer = DocumentMenuHelper.DockManager.Renderer;
      this.DockManager.DockingManager = DocumentMenuHelper.DockManager.DockingManager;
      this.DockManager.OwnerForm = DocumentMenuHelper.DockManager.OwnerForm;
    }
    this.suspendSaveDocControlsSettings = true;
    try
    {
      this.DockManagerStorage = new ImDocumentDockManagerStorage((ImDocumentEditorFormBase) this, this.dockManager, DocumentEditorPlugin.Instance.ConfigurationManager);
      this.DockManagerStorage.ConfigName = this.GetConfigName();
      this.DockManagerStorage.GetDockControlEvent = new DockManager.GetDockControlCallback(this.GetDockControl);
      this.LoadControlsConfiguration();
    }
    finally
    {
      this.suspendSaveDocControlsSettings = false;
    }
  }

  private void BlackWidthService_Changed(object sender, EventArgs e)
  {
    DocumentTreeNode documentTreeNode1 = this.DocumentsComplect == null ? (DocumentTreeNode) this.Document : (DocumentTreeNode) this.DocumentsComplect;
    foreach (DocumentTreeNode documentTreeNode2 in documentTreeNode1.NodesRecursive)
    {
      if (documentTreeNode2 is ContainerElement containerElement && containerElement.DataSourceType == DataSourceType.ShowNET)
        containerElement.FirstDrawImage = true;
    }
    documentTreeNode1.UpdateLayout(true);
  }

  /// <summary>Загрузка параметров  отображаемых окон</summary>
  /// <param name="configurationManager"></param>
  protected virtual void LoadControlsConfiguration()
  {
    try
    {
      if (DocumentEditorPlugin.Instance == null)
        return;
      IConfigurationManager configurationManager = DocumentEditorPlugin.Instance.ConfigurationManager;
      ImDocumentDockManagerStorage dockManagerStorage = this.DockManagerStorage;
      ImDocumentDockManagerStorage.AddException(ImDocumentRedlineNotesDlg.DockGuid.ToString());
      DockManager dockManager1 = this.DockManager;
      DockManager dockManager2 = DocumentEditorPlugin.DockManager;
      if (!dockManagerStorage.LoadConfiguration())
        this.LoadControlsConfigurationOld();
      Size size;
      if (this.propertyGrid != null)
      {
        size = this.propertyGrid.Size;
        if (size.Width <= 0 && this.propertyGridSettings.Opened)
          this.ShowPropertyGrid(true);
      }
      if (this.docTreeViewDlg != null)
      {
        size = this.docTreeViewDlg.Size;
        if (size.Width <= 0 && this.docTreeViewSettings.Opened)
          this.ShowDocumentTreeView(true);
      }
      if (this.insertSymbolDlg != null)
      {
        size = this.insertSymbolDlg.Size;
        if (size.Width <= 0 && this.insertSymbolViewSettings.Opened)
          this.ShowInsertSymbolView(true);
      }
      if (this.redlineEditingDlg != null)
      {
        size = this.redlineEditingDlg.Size;
        if (size.Width <= 0 && this.redlineEditingSettings.Opened)
          this.ShowRedlineNotesEditingPanel(true);
      }
      if (this.propertyGrid != null && this.propertyGrid.LayoutSystem != null && this.propertyGridSettings.Visible)
        this.propertyGrid.LayoutSystem.SelectedControl = (DockControl) this.propertyGrid;
      if (this.docTreeViewDlg != null && this.docTreeViewDlg.LayoutSystem != null && this.docTreeViewSettings.Visible)
        this.docTreeViewDlg.LayoutSystem.SelectedControl = (DockControl) this.docTreeViewDlg;
      if (this.insertSymbolDlg != null && this.insertSymbolDlg.LayoutSystem != null && this.insertSymbolViewSettings.Visible)
        this.insertSymbolDlg.LayoutSystem.SelectedControl = (DockControl) this.insertSymbolDlg;
      if (this.redlineEditingDlg == null || this.redlineEditingDlg.LayoutSystem == null || !this.redlineEditingSettings.Visible)
        return;
      this.redlineEditingDlg.LayoutSystem.SelectedControl = (DockControl) this.redlineEditingDlg;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  protected virtual void LoadControlsConfigurationOld()
  {
    IConfigurationManager configurationManager = DocumentEditorPlugin.Instance.ConfigurationManager;
    if (configurationManager != null)
    {
      IConfiguration config = configurationManager.Open("ImDocEditor");
      if (config != null)
      {
        this.propertyGridSettings = DockControlLayoutSettings.GetSettings(config, "PropertyGrid");
        this.docTreeViewSettings = DockControlLayoutSettings.GetSettings(config, "DocTreeView");
        this.insertSymbolViewSettings = DockControlLayoutSettings.GetSettings(config, "InsertSymbolView");
        this.redlineEditingSettings = DockControlLayoutSettings.GetSettings(config, "RedlineEditingDlg");
      }
    }
    if (this.docTreeViewSettings.Opened && (this.docTreeViewSettings.Visible || this.docTreeViewSettings.DockLocation != DockLocation.Float))
      this.ShowDocumentTreeView(true);
    if (this.propertyGridSettings.Opened && (this.propertyGridSettings.Visible || this.propertyGridSettings.DockLocation != DockLocation.Float))
      this.ShowPropertyGrid(true);
    if (this.insertSymbolViewSettings.Opened && (this.insertSymbolViewSettings.Visible || this.insertSymbolViewSettings.DockLocation != DockLocation.Float))
      this.ShowInsertSymbolView(true);
    if (this.redlineEditingSettings.Opened && (this.redlineEditingSettings.Visible || this.redlineEditingSettings.DockLocation != DockLocation.Float))
      this.ShowRedlineNotesEditingPanel(true);
    if (this.propertyGrid != null && this.propertyGrid.LayoutSystem != null && this.propertyGridSettings.Visible)
      this.propertyGrid.LayoutSystem.SelectedControl = (DockControl) this.propertyGrid;
    if (this.docTreeViewDlg != null && this.docTreeViewDlg.LayoutSystem != null && this.docTreeViewSettings.Visible)
      this.docTreeViewDlg.LayoutSystem.SelectedControl = (DockControl) this.docTreeViewDlg;
    if (this.insertSymbolDlg != null && this.insertSymbolDlg.LayoutSystem != null && this.insertSymbolViewSettings.Visible)
      this.insertSymbolDlg.LayoutSystem.SelectedControl = (DockControl) this.insertSymbolDlg;
    if (this.redlineEditingDlg == null || this.redlineEditingDlg.LayoutSystem == null || !this.redlineEditingSettings.Visible)
      return;
    this.redlineEditingDlg.LayoutSystem.SelectedControl = (DockControl) this.redlineEditingDlg;
  }

  protected virtual DockControl GetDockControl(Guid guid, string persistString, string text)
  {
    if (guid == DocumentTreeViewDlg.DockGuid)
    {
      this.ShowDocumentTreeView(false);
      return (DockControl) this.docTreeViewDlg;
    }
    if (guid == PropertyGridForm.DockGuid)
    {
      this.ShowPropertyGrid(false);
      return (DockControl) this.propertyGrid;
    }
    if (guid == InsertSymbolDockControl.DockGuid)
    {
      this.ShowInsertSymbolView(false);
      return (DockControl) this.insertSymbolDlg;
    }
    if (!(guid == ImDocumentRedlineNotesDlg.DockGuid))
      return (DockControl) null;
    this.ShowRedlineNotesEditingPanel(false);
    return (DockControl) this.redlineEditingDlg;
  }

  /// <summary>Сохранение параметров  отображаемых окон</summary>
  /// <param name="configurationManager"></param>
  protected override void SaveControlsConfig()
  {
    if (!this.NeedSaveControlsConfig)
      return;
    if (this.suspendSaveDocControlsSettings)
      return;
    try
    {
      ImDocumentDockManagerStorage dockManagerStorage = this.DockManagerStorage;
      dockManagerStorage.ConfigName = this.GetConfigName();
      dockManagerStorage.SaveConfiguration();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void DocumentControl_GetCustomElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
    try
    {
      ICommandManager commandManager = this.DocumentControl.DocumentManager.CommandManager;
      this.AddEnabledContextMenu("DocElementProperty", e.ContextMenuItems, commandManager);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public override DocumentMenuHelper MenuHelper
  {
    get => base.MenuHelper;
    set => base.MenuHelper = value;
  }

  /// <summary>Выполнить инициализацию менеджера панелей</summary>
  protected virtual void InitBarManager()
  {
    if (this.DesignMode)
      return;
    try
    {
      this.barManagerInitializing = true;
      if (this.DocumentControl != null)
        this.DocumentControl.BarManager = this.barManager;
      if (this.barManager.OwnerForm == null)
        this.barManager.OwnerForm = Form.ActiveForm;
      if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
      {
        this.barManager.OwnerForm = service.OwnerForm;
        this.barManager.Renderer.Dispose();
        this.barManager.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
        service.CollectToolbars += new CollectToolbarsHandler(this.barMgr_CollectToolbars);
        this.ToolbarRendererChanged((object) service, EventArgs.Empty);
      }
      this.MenuHelper = this.CreateDocumentMenuHelper();
      if (DocumentEditorPlugin.Instance.CommandManager != null)
      {
        this.navigateToolbar = this.MenuHelper.CreateNavigatorToolBar(DocumentEditorPlugin.imageList, DocumentEditorPlugin.Instance.CommandManager);
        this.navigateToolbar.VisibleChanged += new EventHandler(this.toolBar_HiddenChanged);
        this.navigateToolbar.LocationChanged += new EventHandler(this.toolBar_HiddenChanged);
        this.navigateToolbar.ExitMenuLoop += new EventHandler(this.toolBar_HiddenChanged);
        this.AddToolbar(this.barManager, this.navigateToolbar, DockStyle.Top);
        this.formatToolBar = this.MenuHelper.CreateFormatToolBar(DocumentEditorPlugin.imageList, DocumentEditorPlugin.Instance.CommandManager);
        this.formatToolBar.VisibleChanged += new EventHandler(this.toolBar_HiddenChanged);
        this.formatToolBar.LocationChanged += new EventHandler(this.toolBar_HiddenChanged);
        this.formatToolBar.ExitMenuLoop += new EventHandler(this.toolBar_HiddenChanged);
        this.AddToolbar(this.barManager, this.formatToolBar, DockStyle.Top);
        this.tableToolBar = this.MenuHelper.CreateTableToolBar(DocumentEditorPlugin.imageList, DocumentEditorPlugin.Instance.CommandManager);
        this.tableToolBar.VisibleChanged += new EventHandler(this.toolBar_HiddenChanged);
        this.tableToolBar.LocationChanged += new EventHandler(this.toolBar_HiddenChanged);
        this.tableToolBar.ExitMenuLoop += new EventHandler(this.toolBar_HiddenChanged);
        this.AddToolbar(this.barManager, this.tableToolBar, DockStyle.Top);
        this.InitRedlineOnOffToolbar(DocumentEditorPlugin.Instance.CommandManager, new EventHandler(this.toolBar_HiddenChanged), new EventHandler(this.toolBar_HiddenChanged), new EventHandler(this.toolBar_HiddenChanged));
        this.InitRedlineNotesEditingToolbar(DocumentEditorPlugin.Instance.CommandManager, new EventHandler(this.toolBar_HiddenChanged), new EventHandler(this.toolBar_HiddenChanged), new EventHandler(this.toolBar_HiddenChanged));
      }
      if (this.DocumentManager is DocumentEditorPlugin)
      {
        this.pageElementsToolBar = (this.DocumentManager as DocumentEditorPlugin).CreatePageElementsToolBar();
        if (this.pageElementsToolBar != null)
        {
          this.pageElementsToolBar.VisibleChanged += new EventHandler(this.toolBar_HiddenChanged);
          this.pageElementsToolBar.LocationChanged += new EventHandler(this.toolBar_HiddenChanged);
          this.pageElementsToolBar.ExitMenuLoop += new EventHandler(this.toolBar_HiddenChanged);
          this.pageElementsToolBar.Visible = this.IsDocumentTemplate;
          this.AddToolbar(this.barManager, this.pageElementsToolBar, DockStyle.Top);
        }
      }
      if (DocumentEditorPlugin.Instance == null)
        return;
      IConfigurationManager configurationManager = DocumentEditorPlugin.Instance.ConfigurationManager;
      if (configurationManager == null)
        return;
      IConfiguration configuration = configurationManager.Open(this.GetConfigName());
      if (configuration == null)
        return;
      string property = configuration.GetProperty(this.GetToolbarConfigName());
      switch (property)
      {
        case null:
          break;
        case "":
          break;
        default:
          this.barManager.SetLayout(property);
          break;
      }
    }
    finally
    {
      this.barManagerInitializing = false;
    }
  }

  /// <summary>Настраивает панель Редактирование Замечаний</summary>
  protected void InitRedlineNotesEditingToolbar(
    ICommandManager cmdManager,
    EventHandler visibleChanged,
    EventHandler locationChanged,
    EventHandler exitManuLoop)
  {
    this.redlineToolBarPresenter = this.redlineToolBarPresenter ?? new RedlineToolBarPresenter(cmdManager);
    Intermech.Bars.ToolBar redlineEditingToolbar = this.redlineToolBarPresenter.RedlineEditingToolbar;
    redlineEditingToolbar.Visible = false;
    redlineEditingToolbar.Hidden = true;
    this.AddToolbar(this.barManager, redlineEditingToolbar, DockStyle.Top);
    redlineEditingToolbar.DockLine = 1;
    redlineEditingToolbar.VisibleChanged += visibleChanged;
    redlineEditingToolbar.LocationChanged += locationChanged;
    redlineEditingToolbar.ExitMenuLoop += exitManuLoop;
  }

  /// <summary>Настраивает панель Красный Карандаш</summary>
  protected void InitRedlineOnOffToolbar(
    ICommandManager cmdManager,
    EventHandler visibleChanged,
    EventHandler locationChanged,
    EventHandler exitManuLoop)
  {
    this.redlineOnOffToolbar = this.MenuHelper.CreateRedlineOnOffToolBar(DocumentEditorPlugin.imageList, cmdManager);
    this.redlineOnOffToolbar.Visible = false;
    this.AddToolbar(this.barManager, this.redlineOnOffToolbar, DockStyle.Top);
    this.redlineOnOffToolbar.DockLine = 1;
    this.redlineOnOffToolbar.VisibleChanged += visibleChanged;
    this.redlineOnOffToolbar.LocationChanged += locationChanged;
    this.redlineOnOffToolbar.ExitMenuLoop += exitManuLoop;
  }

  public override DocumentMenuHelper CreateDocumentMenuHelper()
  {
    return new DocumentMenuHelper(DocumentEditorPlugin.Instance.CommandManager)
    {
      Form = (ImDocumentEditorFormBase) this
    };
  }

  /// <summary>
  /// Метод получения имени конфига, требуется чтобы в разных плагинах в разные конфиги сохранять значения тулбаров, сделано для Ильи
  /// </summary>
  /// <returns></returns>
  protected virtual string GetConfigName() => "ImDocEditor";

  /// <summary>
  /// Метод получения имени конфига, требуется чтобы в разных плагинах в разные конфиги сохранять значения тулбаров, сделано для Ильи
  /// </summary>
  /// <returns></returns>
  protected virtual string GetToolbarConfigName()
  {
    return this.IsDocumentTemplate ? "ToolBar.Template" : "ToolBar";
  }

  private void toolBar_HiddenChanged(object sender, EventArgs e)
  {
    if (this.barManagerInitializing)
      return;
    try
    {
      IConfigurationManager configurationManager = DocumentEditorPlugin.Instance.ConfigurationManager;
      if (configurationManager == null)
        return;
      (configurationManager.Open(this.GetConfigName()) ?? configurationManager.Create(this.GetConfigName()))?.SetProperty(this.GetToolbarConfigName(), this.barManager.GetLayout(true));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void barMgr_CollectToolbars(object sender, CollectToolbarsEventArgs e)
  {
    try
    {
      if (DocumentEditorPlugin.ActiveImDocumentEditorForm != this)
        return;
      e.Toolbars.AddRange((ICollection) this.barManager.GetToolbarsList());
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private Intermech.Bars.ToolBar GetToolbarByGuid(Guid guid) => (Intermech.Bars.ToolBar) null;

  protected ImDocumentEditorForm() => this.Init();

  /// <summary>Конструктор. Создает окно с пустым документом</summary>
  /// <param name="documentManager">Менеджер документов</param>
  /// <param name="createDocument">Создать документ</param>
  /// <param name="readOnly">Только для чтения</param>
  protected ImDocumentEditorForm(
    IImDocumentManager documentManager,
    bool createDocument,
    bool readOnly)
    : base(documentManager, createDocument, false)
  {
    this.DocumentControl.ReadOnly = readOnly;
    this.Init();
  }

  /// <summary>Метод для создания формы через делегат</summary>
  /// <param name="documentManager">Менеджер документов</param>
  /// <param name="document">Документ</param>
  /// <param name="readOnly">Только для чтения</param>
  /// <returns>Окно редактора документов</returns>
  public static ImDocumentEditorForm DocumentWindowCreator(
    IImDocumentManager documentManager,
    ImDocument document,
    bool readOnly)
  {
    return new ImDocumentEditorForm(documentManager, document, readOnly);
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentManager">Менеджер документов</param>
  /// <param name="document">Документ</param>
  /// <param name="readOnly">Только для чтения</param>
  public ImDocumentEditorForm(
    IImDocumentManager documentManager,
    ImDocument document,
    bool readOnly)
    : base(documentManager, document, readOnly)
  {
    if (document != null)
      this.IsDocumentTemplate = document.IsTemplate;
    this.Init();
    if (document == null)
      return;
    ImDocument mainImDocument = this.MainImDocument;
    bool isLoading = document.IsLoading;
    document.IsLoading = true;
    this.SetDocumentParams(mainImDocument.DBObjectID, mainImDocument.DBObjectGuid, mainImDocument.DBObjectType, mainImDocument.Name, mainImDocument.Designation, mainImDocument.DBObjectCaption);
    document.IsLoading = isLoading;
  }

  protected bool CheckRedlinerAvailability(int dbObjectTypeId)
  {
    return dbObjectTypeId != -1 && AttributeCacheHelper.IsEnabledObjectTypeAttribute(Redliner.RedlineAttId, dbObjectTypeId);
  }

  /// <summary>Конструктор</summary>
  /// <param name="documentManager">Менеджер документов</param>
  /// <param name="documentsComplect">Документ</param>
  /// <param name="readOnly">Только для чтения</param>
  public ImDocumentEditorForm(
    IImDocumentManager documentManager,
    DocumentsComplect documentsComplect,
    bool readOnly)
    : base(documentManager, documentsComplect, readOnly)
  {
    this.Init();
  }

  private InsertSymbolDockControl CreateInsertSymbolView()
  {
    if (this.insertSymbolDlg != null)
    {
      this.insertSymbolDlg.Close();
      this.insertSymbolDlg.Dispose();
      this.insertSymbolDlg = (InsertSymbolDockControl) null;
    }
    DockControl dockControl = (DockControl) null;
    if (this.dockManager != null)
      dockControl = this.dockManager.FindDockControl(InsertSymbolDockControl.DockGuid);
    if (dockControl == null || !(dockControl is InsertSymbolDockControl))
    {
      this.insertSymbolDlg = new InsertSymbolDockControl();
      this.insertSymbolDlg.Text = LocalizationHolder.rm.GetString("Document.Client_116");
      this.DockManagerStorage.SetControl((DockControl) this.insertSymbolDlg);
      this.insertSymbolDlg.DocumentControl = this.DocumentControl;
    }
    return this.insertSymbolDlg;
  }

  private void ShowInsertSymbolView(bool show)
  {
    if (this.DocumentControl == null)
      return;
    if (this.insertSymbolDlg == null)
      this.CreateInsertSymbolView();
    else
      this.insertSymbolDlg.DocumentControl = this.DocumentControl;
    if (!(this.insertSymbolDlg != null & show))
      return;
    this.insertSymbolViewSettings = this.DockManagerStorage.GetSettings((DockControl) this.insertSymbolDlg);
    this.insertSymbolViewSettings.Open((DockControl) this.insertSymbolDlg, this.DockManager);
  }

  private void SaveInsertSymbolDlgConfig()
  {
    if (this.suspendSaveDocControlsSettings)
      return;
    try
    {
      this.NeedSaveControlsConfig = true;
      this.SaveControlsConfig();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private DocumentTreeViewDlg CreateDocumentTreeView()
  {
    if (this.docTreeViewDlg != null)
    {
      this.docTreeViewDlg.Close();
      this.docTreeViewDlg.Dispose();
      this.docTreeViewDlg = (DocumentTreeViewDlg) null;
    }
    DockControl dockControl = (DockControl) null;
    if (this.dockManager != null)
      dockControl = this.dockManager.FindDockControl(DocumentTreeViewDlg.DockGuid);
    if (dockControl == null || !(dockControl is DocumentTreeViewDlg))
    {
      this.docTreeViewDlg = new DocumentTreeViewDlg();
      this.docTreeViewDlg.PersistState = this.BaseEditCommandsEnabled;
      this.docTreeViewDlg.Text = LocalizationHolder.rm.GetString("Document.Client_70");
      this.DockManagerStorage.SetControl((DockControl) this.docTreeViewDlg);
      if (this.DocumentControl != null)
        this.docTreeViewDlg.TreeRoot = (DocumentTreeNode) this.DocumentControl.Document;
      this.docTreeViewDlg.DocumentControl = this.DocumentControl;
      this.docTreeViewDlg.UpdateSelection();
    }
    return this.docTreeViewDlg;
  }

  private void ShowDocumentTreeView(bool show)
  {
    if (!this.CanShowStructureTree() || this.DocumentControl == null)
      return;
    if (this.docTreeViewDlg == null)
    {
      this.CreateDocumentTreeView();
    }
    else
    {
      this.docTreeViewDlg.TreeRoot = (DocumentTreeNode) this.DocumentControl.Document;
      this.docTreeViewDlg.DocumentControl = this.DocumentControl;
      this.docTreeViewDlg.UpdateSelection();
    }
    if (!(this.docTreeViewDlg != null & show))
      return;
    this.docTreeViewSettings = this.DockManagerStorage.GetSettings((DockControl) this.docTreeViewDlg);
    this.docTreeViewSettings.Open((DockControl) this.docTreeViewDlg, this.DockManager);
  }

  private void SaveTreeConfig()
  {
    if (this.suspendSaveDocControlsSettings)
      return;
    if (!this.CanShowStructureTree())
      return;
    try
    {
      this.NeedSaveControlsConfig = true;
      this.SaveControlsConfig();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private ImDocumentRedlineNotesDlg CreateRedlineEditingDlg()
  {
    if (this.redlineEditingDlg != null)
    {
      this.redlineEditingDlg.Close();
      this.redlineEditingDlg.Dispose();
      this.redlineEditingDlg = (ImDocumentRedlineNotesDlg) null;
    }
    DockControl dockControl = (DockControl) null;
    if (this.dockManager != null)
      dockControl = this.dockManager.FindDockControl(ImDocumentRedlineNotesDlg.DockGuid);
    if (dockControl == null || !(dockControl is ImDocumentRedlineNotesDlg))
    {
      this.redlineEditingDlg = new ImDocumentRedlineNotesDlg(this.redlineToolBarPresenter);
      this.redlineEditingDlg.Text = LocalizationHolder.rm.GetString("Document.Client_172");
      this.DockManagerStorage.SetControl((DockControl) this.redlineEditingDlg);
      if (this.DocumentControl != null)
        this.redlineEditingDlg.DocumentControl = this.DocumentControl;
      this.redlineEditingDlg.UpdateSelection();
    }
    return this.redlineEditingDlg;
  }

  private void ShowRedlineNotesEditingPanel(bool show)
  {
    if (!this.CanShowRedline() || this.DocumentControl == null)
      return;
    if (this.redlineEditingDlg == null)
    {
      this.redlineController.NotesDlg = this.CreateRedlineEditingDlg();
    }
    else
    {
      this.redlineEditingDlg.DocumentControl = this.DocumentControl;
      this.redlineEditingDlg.UpdateSelection();
    }
    if (!(this.redlineEditingDlg != null & show))
      return;
    this.redlineEditingSettings = this.DockManagerStorage.GetSettings((DockControl) this.redlineEditingDlg);
    this.redlineEditingSettings.Open((DockControl) this.redlineEditingDlg, this.DockManager);
  }

  private void SaveRedlineConfig()
  {
    if (this.suspendSaveDocControlsSettings)
      return;
    if (!this.CanShowRedline())
      return;
    try
    {
      this.NeedSaveControlsConfig = true;
      this.SaveControlsConfig();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Создать панель свойств объекта</summary>
  /// <returns>Панель свойств</returns>
  private PropertyGridForm CreatePropertyGrid()
  {
    if (this.propertyGrid != null)
    {
      this.propertyGrid.Close();
      this.propertyGrid.Dispose();
      this.propertyGrid = (PropertyGridForm) null;
    }
    DockControl dockControl = (DockControl) null;
    if (this.dockManager != null)
      dockControl = this.dockManager.FindDockControl(PropertyGridForm.DockGuid);
    if (dockControl == null || !(dockControl is PropertyGridForm))
    {
      this.propertyGrid = new PropertyGridForm();
      this.propertyGrid.PersistState = this.BaseEditCommandsEnabled;
      this.propertyGrid.Closed += new EventHandler(this.propertyGridDlg_Closed);
      this.DockManagerStorage.SetControl((DockControl) this.propertyGrid);
      DocumentEditorPlugin.Instance.SelectionChanged();
    }
    return this.propertyGrid;
  }

  private void SavePropertyGridConfig()
  {
    if (this.suspendSaveDocControlsSettings)
      return;
    if (!this.CanShowPropertyGrid())
      return;
    try
    {
      this.NeedSaveControlsConfig = true;
      this.SaveControlsConfig();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  protected virtual IConfigurationManager ConfigurationManager
  {
    get => DocumentEditorPlugin.Instance.ConfigurationManager;
  }

  /// <summary>Открыть панель свойств</summary>
  public void ShowPropertyGrid(bool show)
  {
    if (!this.CanShowPropertyGrid() || this.DocumentControl == null)
      return;
    if (this.propertyGrid == null)
      this.CreatePropertyGrid();
    else
      DocumentEditorPlugin.Instance.SelectionChanged();
    if (!(this.propertyGrid != null & show))
      return;
    this.propertyGridSettings = this.DockManagerStorage.GetSettings((DockControl) this.propertyGrid);
    this.propertyGridSettings.Open((DockControl) this.propertyGrid, this.DockManager);
  }

  private void propertyGridDlg_Closed(object sender, EventArgs e)
  {
    try
    {
      if (DocumentEditorPlugin.Instance.CommandManager == null)
        return;
      ICommandState command = DocumentEditorPlugin.Instance.CommandManager.FindCommand("DocElementProperty");
      if (command == null)
        return;
      this.QueryStatus(command);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Документ связан с объектом системы</summary>
  public bool ObjectAssigned => this.documentID != -1L;

  /// <summary>Установить параметры документа</summary>
  /// <param name="documentID">Идентификатор объекта документа</param>
  /// <param name="documentGuid">Guid объекта документа</param>
  /// <param name="documentType">Тип объекта документа</param>
  /// <param name="documentName">Наименование документа</param>
  /// <param name="documentDesignation">Обозначение документа</param>
  /// <param name="documentCaption">Заголовок документа</param>
  public void SetDocumentParams(
    long documentID,
    Guid documentGuid,
    int documentType,
    string documentName,
    string documentDesignation,
    string documentCaption)
  {
    this.DocumentID = documentID;
    this.DocumentGuid = documentGuid;
    this.DocumentType = documentType;
    this.DocumentName = documentName;
    this.DocumentDesignation = documentDesignation;
    this.DocumentCaption = documentCaption;
  }

  /// <summary>Установить параметры документа</summary>
  /// <param name="documentObject">Объект документа</param>
  public void SetDocumentParams(IDBObject documentObject)
  {
    this.DocumentID = documentObject != null ? documentObject.ObjectID : throw new ArgumentNullException(nameof (documentObject));
    this.DocumentGuid = documentObject.ObjectGUID;
    this.DocumentCaption = documentObject.Caption;
    this.DocumentName = documentObject.GetAttributeByID(DocIDCache.Attr_Name) == null ? this.DocumentCaption : documentObject.GetAttributeByID(DocIDCache.Attr_Name).Description;
    this.DocumentDesignation = documentObject.GetAttributeByID(DocIDCache.Attr_Designation).Description;
  }

  public override string DocumentCaption
  {
    get => base.DocumentCaption;
    set
    {
      if (!(this.DocumentCaption != value))
        return;
      base.DocumentCaption = value;
      DocumentEditorPlugin.UpdateDocumentCaption(DocumentEditorPlugin.DockManager, this.Document);
    }
  }

  /// <summary>Идентификатор объекта документа</summary>
  public long DocumentID
  {
    get => this.documentID;
    set => this.documentID = value;
  }

  /// <summary>Guid объекта документа</summary>
  public Guid DocumentGuid
  {
    get => this.documentGuid;
    set => this.documentGuid = value;
  }

  /// <summary>Тип объекта документа</summary>
  public int DocumentType
  {
    get => this.documentType;
    set
    {
      if (this.documentType == value)
        return;
      this.documentType = value;
      this.SetRedlinerToolBarsVisible(this.CheckRedlinerAvailability(this.documentType));
    }
  }

  /// <summary>
  /// Показывать/скрывать панели Красный карандаш и Редактирование замечаний
  /// </summary>
  protected void SetRedlinerToolBarsVisible(bool showBars)
  {
    this.redlineToolBarPresenter.RedlineEditingToolbar.Visible = false;
    this.barManager.RemoveToolbar(this.redlineOnOffToolbar);
    this.barManager.RemoveToolbar(this.redlineToolBarPresenter.RedlineEditingToolbar);
    if (showBars)
    {
      this.AddToolbar(this.barManager, this.redlineOnOffToolbar, DockStyle.Top);
      this.redlineOnOffToolbar.DockLine = 1;
      this.AddToolbar(this.barManager, this.redlineToolBarPresenter.RedlineEditingToolbar, DockStyle.Top);
      this.redlineToolBarPresenter.RedlineEditingToolbar.DockLine = 1;
    }
    else
      this.redlineOnOffToolbar.Visible = false;
  }

  /// <summary>Наименование документа</summary>
  public string DocumentName
  {
    get => this.documentName;
    set
    {
      if (this.Document != null)
        this.Document.SetAttributeValue(DocumentTreeNode.AttributeName_DocName, value, false, false, false);
      this.documentName = value;
    }
  }

  /// <summary>Обозначение документа</summary>
  public string DocumentDesignation
  {
    get => this.documentDesignation;
    set
    {
      if (!(this.documentDesignation != value))
        return;
      if (this.Document != null)
        this.Document.SetAttributeValue(DocumentTreeNode.AttributeName_Designation, value, false, false, false);
      this.documentDesignation = value;
    }
  }

  public override bool ReadOnly
  {
    get => base.ReadOnly;
    set
    {
      base.ReadOnly = value;
      if (this.Document != null)
        DocumentEditorPlugin.UpdateDocumentCaption(DocumentEditorPlugin.DockManager, this.Document);
      else
        this.UpdateDocumentWindowCaption();
    }
  }

  /// <summary>Получить данные необходимые для восстановления окна при загрузке IMClient</summary>
  /// <returns></returns>
  protected override string GetPersistString()
  {
    try
    {
      HybridDictionary graph = new HybridDictionary();
      graph[(object) "DocumentGuid"] = (object) this.DocumentGuid;
      if (this.ReadOnly)
        graph[(object) "ReadOnly"] = (object) this.ReadOnly;
      string persistString = string.Empty;
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) graph);
        persistString = Convert.ToBase64String(serializationStream.ToArray());
      }
      return persistString;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return (string) null;
    }
  }

  /// <summary>Спросить у пользователя сохранять ли изменения в документе перед закрытием окна</summary>
  public virtual bool AskForSaveBeforeClose
  {
    get => this.askForSaveBeforeClose;
    set => this.askForSaveBeforeClose = value;
  }

  /// <summary>Обновлять ссылки в документе по событиям NotificationService</summary>
  public bool UpdateReferenceByNotificationService
  {
    [DebuggerStepThrough] get => this.updateReferenceByNotificationService;
  }

  public override string DefaultFileName
  {
    get
    {
      return ImDocumentData.ReplaceForbiddenSymbols(string.IsNullOrWhiteSpace(this.defaultFileName) ? DocumentEditorPlugin.GenerateDefaultFileNameForDB((ImDocumentData) this.Document) + this.DefaultFileExtension : this.defaultFileName);
    }
    set => base.DefaultFileName = value;
  }

  /// <summary>Сохранить документ в файловый атрибут объекта</summary>
  public virtual bool SaveDocument()
  {
    if (!this.DocumentControl.EditorValidating() || this.Document == null)
      return false;
    if (this.DocumentID.IsUndefinedId())
    {
      if (!DocumentEditorPlugin.CreateNewDBObjectForDocument(this.Document, this.DefaultDocumentDbObjectType, this.CallDialogWithObjectParamsBeforeSave))
        return false;
      this.SetDocumentParams(this.Document.DBObjectID, this.Document.DBObjectGuid, this.Document.DBObjectType, this.Document.Name, this.Document.Designation, this.Document.DBObjectCaption);
    }
    this.SaveDocumentToDBObjectFile();
    this.Document.Modified = false;
    return true;
  }

  /// <summary>Непосредственное сохранение в БД. Может отличаться в разных окнах</summary>
  protected virtual void SaveDocumentToDBObjectFile()
  {
    if (this.Document.IsTemplate && this.Document.TemplateOwner != null)
    {
      DocumentEditorPlugin.SaveImDocumentObjectFile(this.DocumentID, (ImDocument) this.Document.TemplateOwner, this.DefaultFileName, -1, false);
      this.Document.TemplateOwner.Modified = false;
    }
    else
      DocumentEditorPlugin.SaveImDocumentObjectFile(this.DocumentID, this.Document, this.DefaultFileName, -1, false);
  }

  /// <summary>Заменить шаблон документа</summary>
  /// <param name="rootTemplateType">Тип объектов базы для выбора шаблона</param>
  /// <returns>Возвращает false, если пользователь в диалоге выбрал отмену, или нет документа с шаблоном</returns>
  protected IDBTypedObjectID ReplaceTemplate(int rootTemplateType)
  {
    ImDocument doc = !this.Document.IsTemplate || this.Document.TemplateOwner == null ? this.Document : this.Document.TemplateOwner as ImDocument;
    if (doc == null)
      return (IDBTypedObjectID) null;
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(rootTemplateType));
    IDescriptor rootDescriptor = descriptors.Count != 1 ? (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Document.Client_100"), descriptors) : descriptors[0];
    object[] objArray = Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Document.Client_101"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default);
    if (objArray == null || objArray.Length == 0)
      return (IDBTypedObjectID) null;
    IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) objArray[0];
    this.DoReplaceTemplate(dbTypedObjectId.ObjectID, doc);
    return dbTypedObjectId;
  }

  /// <summary>Заменить шаблон текущего документа на новый</summary>
  /// <param name="newTemplateId">ИД шаблона нового документа</param>
  /// <param name="doc">Документ, у которого меняем шаблон</param>
  protected void DoReplaceTemplate(long newTemplateId, ImDocument doc)
  {
    ImDocument newTemplate = this.LoadTemplateFromDB(newTemplateId);
    if (newTemplate != null && !newTemplate.IsTemplate)
      throw new Exception(LocalizationHolder.rm.GetString("Document.Client_102"));
    ImDocument documentTemplate = doc.DocumentTemplate as ImDocument;
    string resultDescription;
    if (!this.CompareTemplates(documentTemplate, newTemplate, out resultDescription))
    {
      string str1 = "Ниже следует детализированный список несовместимых элементов шаблонов.";
      string str2 = "Применяя новый шаблон, вы действуете на свой страх и риск. Продолжить?";
      if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Document.Client_174"), $"{str1}\r\n{str2}", MessageBoxButtons.YesNo, (IList<string>) resultDescription.Split(new string[1]
      {
        "\r\n"
      }, StringSplitOptions.RemoveEmptyEntries)) != DialogResult.Yes)
        return;
    }
    doc.AssignDocumentTemplate((ImDocumentData) newTemplate, true, true, true);
    for (int index = 0; index < DocumentEditorPlugin.DockManager.DocumentContainer.Documents.Length; ++index)
    {
      if (DocumentEditorPlugin.DockManager.DocumentContainer.Documents[index] is ImDocumentEditorForm document && document.Document == documentTemplate)
        document.DocumentControl.Document = newTemplate;
    }
    if (DocumentEditorPlugin.ActiveImDocumentEditorForm == null || DocumentEditorPlugin.ActiveImDocumentEditorForm.Document != newTemplate)
      return;
    if (DocumentEditorPlugin.ActiveImDocumentEditorForm.DocTreeViewDlg != null)
      DocumentEditorPlugin.ActiveImDocumentEditorForm.DocTreeViewDlg.TreeRoot = (DocumentTreeNode) DocumentEditorPlugin.ActiveImDocumentControl.Document;
    DocumentEditorPlugin.ActiveImDocumentEditorForm.DocumentControl.SetActiveElement((DocumentTreeNode) null, false, Point.Empty);
  }

  protected virtual bool CompareTemplates(
    ImDocument oldTemplate,
    ImDocument newTemplate,
    out string resultDescription)
  {
    return ImDocument.AreCompatibleTemplates(oldTemplate, newTemplate, out resultDescription);
  }

  protected virtual ImDocument LoadTemplateFromDB(long newTemplateId)
  {
    return DocumentEditorPlugin.LoadDocumentFromDBObject(newTemplateId);
  }

  public void InsertFormula(TextBoxElement context)
  {
    if (context == null || this.DocumentControl == null || this.DocumentControl.QueryCache_HasLockedNodes || !context.InPlaceEditorActive)
      return;
    EditSymbolForm editSymbolForm = new EditSymbolForm();
    FormList formula = new FormList();
    ImDocument document = this.Document;
    FormList formList = formula;
    if (!editSymbolForm.Execute(document, formList) || formula.IsEmptyPages() || !(context.InPlaceEditorControl is ImRtfEditor placeEditorControl))
      return;
    for (int index = 0; index < formula.Count; ++index)
    {
      if (this.Document.FormulaList.FindNode(formula[index].page.Id) == null)
        this.Document.FormulaList.AddChildNode(formula[index].page.Clone(), false, false);
    }
    List<int> avsMaterialPos = (List<int>) null;
    context.TextBox.InsertTextByFormulaImage(placeEditorControl, this.Document, formula, formula.ToString(), placeEditorControl.CurLine, placeEditorControl.CurCol, ref avsMaterialPos, true);
  }

  public bool EditFormula(TextBoxElement context)
  {
    bool flag = true;
    if (context != null && this.DocumentControl != null && !this.DocumentControl.QueryCache_HasLockedNodes && context.InPlaceEditorActive)
    {
      EditSymbolForm editSymbolForm = new EditSymbolForm();
      if (context.InPlaceEditorControl is ImRtfEditor placeEditorControl)
      {
        FormList formList = context.TextBox.DecodeFormulaFromEditor();
        if (formList != null && !formList.IsEmptyPages())
        {
          flag = false;
          if (editSymbolForm.Execute(this.Document, formList))
          {
            for (int index = 0; index < formList.Count; ++index)
            {
              if (this.Document.FormulaList.FindNode(formList[index].page.Id) == null)
                this.Document.FormulaList.AddChildNode(formList[index].page.Clone(), false, false);
            }
            placeEditorControl.SelectTerText(placeEditorControl.CurLine, placeEditorControl.CurCol, placeEditorControl.CurLine, placeEditorControl.CurCol + 1, false);
            placeEditorControl.TerDeleteBlock(false);
            if (context.TextBox != null)
            {
              List<int> avsMaterialPos = (List<int>) null;
              context.TextBox.InsertTextByFormulaImage(placeEditorControl, this.Document, formList, formList.ToString(), placeEditorControl.CurLine, placeEditorControl.CurCol, ref avsMaterialPos, true);
            }
          }
        }
      }
    }
    if (((context == null ? 0 : (context.CanCallEditor ? 1 : 0)) & (flag ? 1 : 0)) != 0)
    {
      context.CallEditor();
      flag = false;
    }
    return flag;
  }

  public void UpdateFormulas()
  {
    TemplateHolderBase.Instance?.ReloadTemplates();
    this.ClearCachedFormulas(this.Document);
    this.UpdateFormulasInText(this.Document);
  }

  private void UpdateFormulasInText(ImDocument doc)
  {
    if (doc.DocumentTemplate is ImDocument documentTemplate)
      this.UpdateFormulasInText(documentTemplate);
    foreach (TextBoxElement textBoxElement in doc.ChildNodesByCondition<TextBoxElement>((Func<TextBoxElement, bool>) (t => t.IsFirstInFlow && t.Rtf != null && t.Text.Contains("<<"))))
      textBoxElement.SetRtfText((string) null, false, false);
    doc.UpdateLayout(true);
  }

  private void ClearCachedFormulas(ImDocument doc)
  {
    if (doc.DocumentTemplate is ImDocument documentTemplate)
      this.ClearCachedFormulas(documentTemplate);
    foreach (PageData allPage in doc.FormulaList.GetAllPages())
    {
      if (doc.FindFormulaInLib(allPage.Id) != null)
        allPage.Remove(false, false);
    }
  }

  protected virtual string TypeName => this.GetType().ToString();

  protected override void OnParentChanged(EventArgs e) => base.OnParentChanged(e);

  /// <summary>Вызывается при назначении нового документа</summary>
  public override void OnDocumentChanged()
  {
    base.OnDocumentChanged();
    if (this.docTreeViewDlg == null)
      return;
    if (this.DocumentControl != null)
      this.docTreeViewDlg.TreeRoot = (DocumentTreeNode) this.DocumentControl.Document;
    this.docTreeViewDlg.DocumentControl = this.DocumentControl;
    this.docTreeViewDlg.UpdateSelection();
  }

  public override void Activated()
  {
    try
    {
      base.Activated();
      if (this._activated)
        return;
      try
      {
        this.FiltrationInitToolbar();
      }
      finally
      {
        this._activated = true;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Деактивация формы</summary>
  public override void Deactivated()
  {
    try
    {
      base.Deactivated();
      if (!this._activated)
        return;
      try
      {
        this.FiltrationClearToolbar();
        if (!this.CanShowRedline())
          return;
        this.redlineController?.SaveRedline();
      }
      finally
      {
        this._activated = false;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Идентификатор правила подбора версий</summary>
  public virtual string FiltrationOwnerID
  {
    get
    {
      VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
      return editorRule != null && editorRule.OwnerId != null ? editorRule.OwnerId : "";
    }
  }

  /// <summary>Заполнить тулбар нашими настройками фильтрации состава</summary>
  protected virtual void FiltrationInitToolbar()
  {
    if (DocumentEditorPlugin.IFiltrationService == null)
      return;
    DocumentEditorPlugin.IFiltrationService.FiltrationServiceOwnerID = this.FiltrationOwnerID;
    DocumentEditorPlugin.IFiltrationService.Enabled = true;
    if (!DocumentEditorPlugin.IFiltrationService.FiltrationToolbarHidden)
      DocumentEditorPlugin.IFiltrationService.FiltrationToolbarVisible = true;
    DocumentEditorPlugin.IFiltrationService.FiltrationApplyUpdates(true);
  }

  /// <summary>Убрать из тулбара наши настройки фильтрации состава</summary>
  protected virtual void FiltrationClearToolbar()
  {
    if (DocumentEditorPlugin.IFiltrationService == null)
      return;
    DocumentEditorPlugin.IFiltrationService.FiltrationServiceOwnerID = string.Empty;
  }

  public override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    this.SaveControlsConfig();
  }

  /// <summary>Обработчик закрытия окна</summary>
  /// <param name="e"></param>
  protected override void OnClosing(CancelEventArgs e)
  {
    try
    {
      base.OnClosing(e);
      this.barManagerInitializing = true;
      try
      {
        if (!e.Cancel)
        {
          if (this.AskForSaveBeforeClose)
          {
            if (!this.IsInternalDocumentTemplate)
            {
              if (!this.ReadOnly)
              {
                if (this.Document != null)
                {
                  if (this.Document.Modified)
                  {
                    switch (MessageBox.Show($"{LocalizationHolder.rm.GetString("Document.Client_103")}{this.DocumentCaption}\"", LocalizationHolder.rm.GetString("Document.Client_104"), MessageBoxButtons.YesNoCancel))
                    {
                      case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                      case DialogResult.Yes:
                        if (!this.SaveDocument())
                        {
                          e.Cancel = true;
                          break;
                        }
                        break;
                    }
                  }
                }
              }
            }
          }
        }
      }
      finally
      {
        if (e.Cancel)
          this.barManagerInitializing = false;
      }
      if (e.Cancel)
        return;
      this.RedlineOff();
      this.suspendSaveDocControlsSettings = true;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Назначить DocumentControl</summary>
  /// <param name="value">Значение DocumentControl</param>
  public override void AssignDocumentControl(DocumentControl value)
  {
    if (this.DocumentControl != null)
    {
      this.DocumentControl.GetCustomElementContextMenu -= new GetCustomElementContextMenu_EventHandler(this.GetCustomElementContextMenu);
      this.DocumentControl.ActivePageChanged -= new ActivePageChanged_EventHandler(this.DocumentControl_ActivePageChanged);
      this.DocumentControl.BarManager = (BarManager) null;
    }
    base.AssignDocumentControl(value);
    if (value == null)
      return;
    value.GetCustomElementContextMenu += new GetCustomElementContextMenu_EventHandler(this.GetCustomElementContextMenu);
    this.DocumentControl.ActivePageChanged += new ActivePageChanged_EventHandler(this.DocumentControl_ActivePageChanged);
    this.DocumentControl.BarManager = this.barManager;
  }

  private void DocumentControl_ActivePageChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Добавить команду контекстного меню, если он Enabled</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="contextMenuItems">Список пунктов меню</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns></returns>
  protected MenuButtonItem AddEnabledContextMenu(
    string commandName,
    List<MenuButtonItem> contextMenuItems,
    ICommandManager commandManager)
  {
    return this.InsertEnabledContextMenu(commandName, contextMenuItems, -1, commandManager);
  }

  /// <summary>Добавить команду контекстного меню, если он Enabled</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="contextMenuItems">Список пунктов меню</param>
  /// <param name="index">Индекс для вставки</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns></returns>
  protected MenuButtonItem InsertEnabledContextMenu(
    string commandName,
    List<MenuButtonItem> contextMenuItems,
    int index,
    ICommandManager commandManager)
  {
    if (index > contextMenuItems.Count)
      index = contextMenuItems.Count;
    if (index == -1)
      index = contextMenuItems.Count;
    MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem(commandName);
    if (contextMenuItem != null)
    {
      ICommandState commandState = commandManager.Add(commandName, (ButtonItemBase) contextMenuItem);
      if (commandState != null)
      {
        this.QueryStatus(commandState);
        contextMenuItem.Enabled = commandState.Enabled;
        contextMenuItem.Visible = commandState.Visible;
      }
      if (contextMenuItem.Enabled)
        contextMenuItems.Insert(index, contextMenuItem);
    }
    return contextMenuItem;
  }

  private void GetCustomElementContextMenu(object sender, GetCustomElementContextMenu_EventArgs e)
  {
    ICommandManager commandManager = this.DocumentControl.DocumentManager.CommandManager;
    if (commandManager == null)
      return;
    this.AddEnabledContextMenu("DocEditor.InsertFormula", e.ContextMenuItems, commandManager);
    this.AddEnabledContextMenu("ParametersCard1", e.ContextMenuItems, commandManager);
    this.AddEnabledContextMenu("DocEditor.OpenInNewWindow", e.ContextMenuItems, commandManager);
  }

  /// <summary>Сохранить как файл в файловую систему</summary>
  /// <param name="fileName">Возвращает имя сохранённого файла</param>
  /// <returns>Возвращает true, если файл успешно сохранён</returns>
  public override bool SaveAsExecute(ref string fileName)
  {
    SaveAsEventHandlerArgs eventArgs = new SaveAsEventHandlerArgs(this.DocumentID, fileName, this.Document);
    DocumentEditorPlugin.OnBeforeSaveAs((object) this, eventArgs);
    fileName = eventArgs.FileName;
    int num = base.SaveAsExecute(ref fileName) ? 1 : 0;
    if (num == 0)
      return num != 0;
    eventArgs.FileName = fileName;
    DocumentEditorPlugin.OnAfterFileSaveAs((object) this, eventArgs);
    return num != 0;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      BarManager service1 = ServicesManager.GetService(typeof (BarManager)) as BarManager;
      if (this.DocumentControl != null)
        this.DocumentControl.GetCustomElementContextMenu -= new GetCustomElementContextMenu_EventHandler(this.DocumentControl_GetCustomElementContextMenu);
      if (service1 != null)
      {
        service1.CollectToolbars -= new CollectToolbarsHandler(this.barMgr_CollectToolbars);
        service1.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
      }
      IBlackWidthService service2 = (IBlackWidthService) ServicesManager.GetService(typeof (IBlackWidthService));
      if (service2 != null)
        service2.Changed -= new EventHandler(this.BlackWidthService_Changed);
      if (this.barManager != null)
      {
        this.barManager.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        this.barManager.OwnerForm = (Form) null;
      }
      if (this.navigateToolbar != null)
      {
        if (this.barManager != null)
          this.barManager.RemoveToolbar(this.navigateToolbar);
        this.navigateToolbar.VisibleChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.navigateToolbar.LocationChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.navigateToolbar.ExitMenuLoop -= new EventHandler(this.toolBar_HiddenChanged);
        this.navigateToolbar = (Intermech.Bars.ToolBar) null;
      }
      if (this.formatToolBar != null)
      {
        if (this.barManager != null)
          this.barManager.RemoveToolbar(this.formatToolBar);
        this.formatToolBar.VisibleChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.formatToolBar.LocationChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.formatToolBar.ExitMenuLoop -= new EventHandler(this.toolBar_HiddenChanged);
        this.formatToolBar = (Intermech.Bars.ToolBar) null;
      }
      if (this.tableToolBar != null)
      {
        if (this.barManager != null)
          this.barManager.RemoveToolbar(this.tableToolBar);
        this.tableToolBar.VisibleChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.tableToolBar.LocationChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.tableToolBar.ExitMenuLoop -= new EventHandler(this.toolBar_HiddenChanged);
        this.tableToolBar = (Intermech.Bars.ToolBar) null;
      }
      if (this.pageElementsToolBar != null)
      {
        if (this.barManager != null)
          this.barManager.RemoveToolbar(this.pageElementsToolBar);
        this.pageElementsToolBar.VisibleChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.pageElementsToolBar.LocationChanged -= new EventHandler(this.toolBar_HiddenChanged);
        this.pageElementsToolBar.ExitMenuLoop -= new EventHandler(this.toolBar_HiddenChanged);
        this.pageElementsToolBar = (Intermech.Bars.ToolBar) null;
      }
      if (this.insertSymbolDlg != null)
      {
        this.DockManagerStorage.DisposeControl((DockControl) this.insertSymbolDlg);
        this.insertSymbolDlg.DocumentControl = (DocumentControl) null;
        this.insertSymbolDlg = (InsertSymbolDockControl) null;
      }
      if (this.docTreeViewDlg != null)
      {
        this.DockManagerStorage.DisposeControl((DockControl) this.docTreeViewDlg);
        this.docTreeViewDlg = (DocumentTreeViewDlg) null;
      }
      if (this.propertyGrid != null)
      {
        this.propertyGrid.Closed -= new EventHandler(this.propertyGridDlg_Closed);
        this.DockManagerStorage.DisposeControl((DockControl) this.propertyGrid);
      }
      if (this.dockManager != null)
        this.dockManager.Dispose();
      if (this.components != null)
        this.components.Dispose();
      if (this.DockManager != null)
        this.DockManager.OwnerForm = (Form) null;
    }
    base.Dispose(disposing);
  }

  /// <summary>Required method for Designer support - do not modify
  /// the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImDocumentEditorForm));
    this.barManager = new BarManager();
    this.leftBarDock = new ToolBarContainer();
    this.rightBarDock = new ToolBarContainer();
    this.bottomBarDock = new ToolBarContainer();
    this.topBarDock = new ToolBarContainer();
    this.dockManager = new DockManager();
    this.leftDock = new DockContainer();
    this.rightDock = new DockContainer();
    this.bottomDock = new DockContainer();
    this.topDock = new DockContainer();
    this.SuspendLayout();
    this.barManager.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.leftBarDock, "leftBarDock");
    this.leftBarDock.Guid = new Guid("119d81fe-e4be-4e80-b187-ac7018d05047");
    this.leftBarDock.Manager = this.barManager;
    this.leftBarDock.Name = "leftBarDock";
    componentResourceManager.ApplyResources((object) this.rightBarDock, "rightBarDock");
    this.rightBarDock.Guid = new Guid("a811c68b-52b6-403d-b9ac-248230a9db2b");
    this.rightBarDock.Manager = this.barManager;
    this.rightBarDock.Name = "rightBarDock";
    componentResourceManager.ApplyResources((object) this.bottomBarDock, "bottomBarDock");
    this.bottomBarDock.Guid = new Guid("ca2e84f5-fbed-4eb2-8480-555288d9d311");
    this.bottomBarDock.Manager = this.barManager;
    this.bottomBarDock.Name = "bottomBarDock";
    componentResourceManager.ApplyResources((object) this.topBarDock, "topBarDock");
    this.topBarDock.Guid = new Guid("2421522d-7ab0-4852-b9ed-26a71bfd76b1");
    this.topBarDock.Manager = this.barManager;
    this.topBarDock.Name = "topBarDock";
    this.dockManager.DockingManager = DockingManager.Whidbey;
    this.dockManager.OwnerForm = this.ParentForm;
    componentResourceManager.ApplyResources((object) this.leftDock, "leftDock");
    this.leftDock.Guid = new Guid("78a40fd2-ee15-4907-b019-b8ffb674c87c");
    this.leftDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.leftDock.Manager = this.dockManager;
    this.leftDock.Name = "leftDock";
    this.leftDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.rightDock, "rightDock");
    this.rightDock.Guid = new Guid("6e0e68ee-48ab-443c-8a71-a7e0142c6a67");
    this.rightDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.rightDock.Manager = this.dockManager;
    this.rightDock.Name = "rightDock";
    this.rightDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
    this.bottomDock.Guid = new Guid("63319053-c17e-42d4-9120-ebe2c47e318f");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.bottomDock.Manager = this.dockManager;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.topDock, "topDock");
    this.topDock.Guid = new Guid("70287f75-cdbc-4a81-b58a-3869f872b94d");
    this.topDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.topDock.Manager = this.dockManager;
    this.topDock.Name = "topDock";
    this.topDock.Renderer = (RendererBase) null;
    this.Controls.Add((Control) this.leftDock);
    this.Controls.Add((Control) this.rightDock);
    this.Controls.Add((Control) this.bottomDock);
    this.Controls.Add((Control) this.topDock);
    this.Controls.Add((Control) this.leftBarDock);
    this.Controls.Add((Control) this.rightBarDock);
    this.Controls.Add((Control) this.bottomBarDock);
    this.Controls.Add((Control) this.topBarDock);
    this.Name = nameof (ImDocumentEditorForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ResumeLayout(false);
  }

  /// <summary>Включить базовые команды редактора документов</summary>
  public override bool BaseEditCommandsEnabled => this.baseEditCommandsEnabled;

  /// <summary>Установить BaseDocumentCommandsEnabled</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateCommandsStatus">Вызвать QueryStaus()</param>
  public virtual void SetBaseEditCommandsEnabled(bool value, bool updateCommandsStatus)
  {
    if (this.baseEditCommandsEnabled == value)
      return;
    this.baseEditCommandsEnabled = value;
    if (!updateCommandsStatus || this.CommandManager == null)
      return;
    this.CommandManager.QueryStatus();
  }

  public override bool TableEditCommandsEnabled => this.tableEditCommandsEnabled;

  /// <summary>Установить ToolbarEditCommandsEnabled</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateCommandsStatus">Вызвать QueryStaus()</param>
  public virtual void SetTableEditCommandsEnabled(bool value, bool updateCommandsStatus)
  {
    if (this.tableEditCommandsEnabled == value)
      return;
    this.tableEditCommandsEnabled = value;
    if (!updateCommandsStatus || this.CommandManager == null)
      return;
    this.CommandManager.QueryStatus();
  }

  /// <summary>Выполнить команду</summary>
  /// <param name="commandState">Данные команды</param>
  /// <returns>true, если команда найдена и обработана</returns>
  public override bool Execute(ICommandState commandState)
  {
    if (commandState == null)
      return false;
    try
    {
      switch (commandState.CommandName)
      {
        case "DocEditor.OpenInNewWindow":
          long selectedReferenceId = this.SelectedReferenceId;
          if (selectedReferenceId.IsDefinedId())
            ImDocumentEditorForm.DoOpenInNewWindowCommand(new long[1]
            {
              selectedReferenceId
            });
          return true;
        case "DocEditor.ReplaceTemplate":
          this.ReplaceTemplate(DocIDCache.ObjType_ImDocTemplate);
          return true;
        case "DocEditor.UpdateDocumentLinks":
          if (this.Document != null)
            DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) this.Document, true, true, true, true, true);
          return true;
        case "DocElementProperty":
          this.ShowPropertyGrid(true);
          return true;
        case "ParametersCard":
          int num1 = (int) PropertiesWindow.Execute(string.Empty, string.Empty, this.DocumentID);
          return true;
        case "ParametersCard1":
          long ObjectID = this.SelectedReferenceId;
          if (ObjectID == -1L)
            ObjectID = this.DocumentID;
          int num2 = (int) PropertiesWindow.Execute(string.Empty, string.Empty, ObjectID);
          return true;
        case "Print":
          List<ImDocumentData> imDocumentDataList = new List<ImDocumentData>();
          if (this.DocumentsComplect != null)
            imDocumentDataList.AddRange((IEnumerable<ImDocumentData>) this.DocumentsComplect.GetAllDocuments());
          else if (this.Document != null)
            imDocumentDataList.Add((ImDocumentData) this.Document);
          using (List<ImDocumentData>.Enumerator enumerator = imDocumentDataList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ImDocumentData current = enumerator.Current;
              if (current.DBObjectGuid != Guid.Empty)
              {
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                  sessionKeeper.Session.GetObject(current.DBObjectGuid, false)?.Print();
              }
            }
            break;
          }
        case "Redline.CompleteEdit":
          this.RedlineOff();
          this.redlineOnOffToolbar.Items[1].Index = 0;
          this.redlineOnOffToolbar.Refresh();
          return true;
        case "Redline.Edit":
          this.RedlineOn();
          this.redlineOnOffToolbar.Items[1].Index = 0;
          this.redlineOnOffToolbar.Refresh();
          return true;
        case "Save":
          if (this.Document != null)
            this.SaveDocument();
          return true;
        case "SaveAs":
          if (this.Document != null)
          {
            bool modified = this.Document.Modified;
            if (!base.Execute(commandState))
              return false;
            this.Document.Modified = modified;
          }
          return true;
        case "ShowDocumentTreeView":
          this.ShowDocumentTreeView(true);
          return true;
        case "ShowInsertSymbolView":
          this.ShowInsertSymbolView(true);
          return true;
        case "Tree.Update":
          if (this.docTreeViewDlg != null)
            this.docTreeViewDlg.UpdateTree();
          return true;
        case "UpdateFormulas":
          this.UpdateFormulas();
          return true;
      }
      DocumentTreeNode[] context1 = NodeContextMenu.ContextForContextMenu;
      if (context1 == null || !NodeContextMenu.ContextMenuCommand)
        context1 = this.DocumentControl.GetSelectedNodes();
      if (commandState.CommandName == "DocEditor.EditFormula" || commandState.CommandName == "CallEditor")
      {
        if (context1 != null && context1.Length == 1 && context1[0] is TextBoxElement context2)
        {
          this.EditFormula(context2);
          return true;
        }
      }
      else
      {
        if (commandState.CommandName == "DocEditor.InsertFormula")
        {
          if (context1 != null && context1.Length == 1 && context1[0] is TextBoxElement context3)
            this.InsertFormula(context3);
          return true;
        }
        if (commandState.CommandName == "AVS.ChangePageNumberingStyle")
          DocumentMenuHelper.ChangeAdditionalPageNumberingStyle(this.DocumentControl);
      }
      if (this.MenuHelper != null && this.MenuHelper.Execute(commandState, context1, this.DocumentControl))
      {
        NodeContextMenu.ContextForContextMenu = (DocumentTreeNode[]) null;
        NodeContextMenu.ContextMenuCommand = false;
        return true;
      }
      if (this.redlineController != null && this.redlineController.Execute(commandState))
        return true;
      if (base.Execute(commandState))
        return true;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      commandState.Enabled = false;
      return true;
    }
    return false;
  }

  private static void DoOpenInNewWindowCommand(long[] objIDs)
  {
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(objIDs);
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2), (System.IServiceProvider) viewServices1);
  }

  /// <summary>Выключить режим Красного карандаша</summary>
  private void RedlineOff()
  {
    if (this.redlineController == null || !this.redlineController.IsRedlineEnabled)
      return;
    this.redlineToolBarPresenter.UnSubscribeCbBoxRoleEvents(new EventHandler(this.redlineController.RoleChanged), new EventHandler(this.redlineController.RoleDropDownOpened), new EventHandler(this.redlineController.RoleDropDownClosed));
    this.redlineController.DocControl = (DocumentControl) null;
    this.redlineController.IsRedlineEnabled = false;
    this.redlineView.BackControl = (Control) null;
    this.DocumentControl.PageControl.Controls.Remove((Control) this.redlineView);
    this.redlineView.Dispose();
    this.redlineView = (TransparentRedlineView) null;
    this.redlineToolBarPresenter.RedlineEditingToolbar.Visible = false;
    this.redlineEditingDlg.Close();
  }

  /// <summary>Включить режим Красного карандаша</summary>
  private void RedlineOn()
  {
    this.redlineController = this.redlineController ?? new ImDocumentRedlineController(this.CommandManager);
    this.redlineController.IsRedlineEnabled = true;
    this.redlineView = new TransparentRedlineView()
    {
      BackControl = (Control) this.DocumentControl.PageControl
    };
    this.redlineController.View = this.redlineView;
    this.Controls.Add((Control) this.redlineView);
    this.redlineView.Dock = DockStyle.Fill;
    this.redlineView.BringToFront();
    this.redlineToolBarPresenter.RedlineEditingToolbar.Visible = true;
    this.redlineController.RedToolbar = this.redlineToolBarPresenter.RedlineEditingToolbar;
    this.redlineToolBarPresenter.SubscribeRoleComboEvents(new EventHandler(this.redlineController.RoleChanged), new EventHandler(this.redlineController.RoleDropDownOpened), new EventHandler(this.redlineController.RoleDropDownClosed));
    if (this.redlineController.NotesDlg == null)
      this.redlineController.NotesDlg = this.CreateRedlineEditingDlg();
    this.redlineController.DocControl = this.DocumentControl;
    bool controlsSettings = this.suspendSaveDocControlsSettings;
    this.suspendSaveDocControlsSettings = true;
    try
    {
      this.ShowRedlineNotesEditingPanel(true);
    }
    finally
    {
      this.suspendSaveDocControlsSettings = controlsSettings;
    }
  }

  private static long GetReferencedObjectId(INodeWithReference node)
  {
    long referencedObjectId = -1;
    ReferenceToDBObjectBase reference1 = node.Reference as ReferenceToDBObjectBase;
    if (node.Reference is ReferenceToDBObjectAttributeCore reference2)
    {
      if (!reference2.IsConnectedObjectRef)
        reference2.UpdateDBObjectInfo();
      referencedObjectId = reference2.LinkAttributeObjectID;
    }
    else if (reference1 != null)
    {
      if (!reference1.IsConnectedObjectRef)
        reference1.UpdateDBObjectInfo();
      DBHelper.GetObjIDByGuid(reference1.DBObjectGuid);
      if (reference1.DBObjectID != -1L)
        referencedObjectId = reference1.DBObjectID;
    }
    return referencedObjectId;
  }

  /// <summary>
  /// Идентификатор объекта по ссылке в выделенном узле документа
  /// </summary>
  public long SelectedReferenceId
  {
    get
    {
      long selectedReferenceId = -1;
      DocumentTreeNode[] selectedNodes = this.DocumentControl?.GetSelectedNodes();
      if (selectedNodes != null && selectedNodes.Length != 0 && selectedNodes[0] is INodeWithReference node)
        selectedReferenceId = ImDocumentEditorForm.GetReferencedObjectId(node);
      return selectedReferenceId;
    }
  }

  /// <summary>Разрешено показывать PropertyGrid</summary>
  /// <returns></returns>
  private bool CanShowPropertyGrid()
  {
    bool flag = true;
    if (this.GetType().Name != nameof (ImDocumentEditorForm))
    {
      flag = false;
      if (ImDocumentData.ShowDebugInfo)
        flag = true;
    }
    return flag;
  }

  /// <summary>Разрешено показывать дерево документа</summary>
  /// <returns></returns>
  protected virtual bool CanShowStructureTree()
  {
    bool flag = true;
    if (this.GetType().Name != nameof (ImDocumentEditorForm))
      flag = ImDocumentData.ShowDebugInfo;
    return flag;
  }

  /// <summary>Разрешено показывать панель замечаний к документу</summary>
  /// <returns></returns>
  public virtual bool CanShowRedline()
  {
    ImDocumentRedlineController redlineController = this.redlineController;
    return redlineController != null && redlineController.IsRedlineEnabled;
  }

  public override void BeginQuery()
  {
    base.BeginQuery();
    bool flag = this.BaseEditCommandsEnabled || this.TableEditCommandsEnabled;
    if (this.tableToolBar == null || this.tableToolBar.Visible == flag)
      return;
    this.tableToolBar.Visible = flag;
  }

  /// <summary>Проверить статус комманды</summary>
  /// <param name="commandState">Состояние комманды</param>
  /// <returns>true, если команда найдена</returns>
  public override bool QueryStatus(ICommandState commandState)
  {
    if (commandState == null || this.DocumentControl == null)
      return false;
    bool flag1 = this.DocumentType == DocIDCache.ObjType_FormulaLib;
    bool flag2;
    try
    {
      switch (commandState.CommandName)
      {
        case "AVS.ChangePageNumberingStyle":
          DocumentTreeNode[] selectedNodes1 = this.DocumentControl.GetSelectedNodes();
          bool flag3 = selectedNodes1 != null && ((IEnumerable<DocumentTreeNode>) selectedNodes1).Any<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (n =>
          {
            if (!(n is RectangleElement rectangleElement2))
              return false;
            PageData page = rectangleElement2.Page;
            return page != null && page.IsAdditionalPage;
          }));
          commandState.Visible = commandState.Enabled = !this.ReadOnly & flag3;
          return true;
        case "DocEditor.OpenInNewWindow":
          commandState.Enabled = commandState.Visible = this.SelectedReferenceId.IsDefinedId();
          return true;
        case "DocEditor.PageElementsMenu":
          commandState.Visible = this.BaseEditCommandsEnabled;
          commandState.Enabled = true;
          return true;
        case "DocEditor.ReplaceTemplate":
          commandState.Visible = this.BaseEditCommandsEnabled;
          ImDocument imDocument = (ImDocument) null;
          if (!this.ReadOnly)
            imDocument = this.Document?.TemplateOwner == null || !this.Document.IsTemplate ? this.Document : this.Document.TemplateOwner as ImDocument;
          commandState.Enabled = imDocument != null;
          return true;
        case "DocElementProperty":
          DocumentTreeNode[] selectedNodes2 = this.DocumentControl.GetSelectedNodes();
          commandState.Enabled = selectedNodes2 != null && selectedNodes2.Length != 0;
          bool flag4 = this.CanShowPropertyGrid();
          commandState.Visible = flag4;
          return true;
        case "ParametersCard":
          commandState.Enabled = true;
          commandState.Visible = true;
          return true;
        case "ParametersCard1":
          INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
          MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("ParametersCard1");
          if (contextMenuItem.Image == null)
          {
            MenuItemBase menuItem = ((BarManager) ServicesManager.GetService(typeof (BarManager))).MenuBar.FindMenuItem("mnObjects.ParametersCard");
            if (menuItem != null && service.ImageList.Images.Count > menuItem.ImageIndex)
              contextMenuItem.Image = service.ImageList.Images[menuItem.ImageIndex];
          }
          DocumentTreeNode[] selectedNodes3 = this.DocumentControl.GetSelectedNodes();
          if (selectedNodes3 == null || selectedNodes3.Length == 0)
            commandState.Enabled = true;
          else if (selectedNodes3[0] is ImDocumentData || selectedNodes3[0] is PageData)
          {
            commandState.Enabled = false;
          }
          else
          {
            INodeWithReference nodeWithReference = selectedNodes3[0] as INodeWithReference;
            commandState.Enabled = nodeWithReference?.Reference is ReferenceToDBObjectBase _;
          }
          commandState.Visible = true;
          return true;
        case "Redline.CompleteEdit":
          commandState.Enabled = commandState.Visible = this.CanShowRedline();
          return true;
        case "Redline.Edit":
          commandState.Enabled = commandState.Visible = !this.CanShowRedline();
          return true;
        case "Save":
          commandState.Enabled = this.Document != null && this.Document.LoadFromStreamThread == null && (!this.ReadOnly && this.Document.Modified || this.DocumentID == -1L);
          return true;
        case "ShowDocumentTreeView":
          commandState.Enabled = true;
          commandState.Visible = this.CanShowStructureTree();
          return true;
        case "ShowInsertSymbolView":
          commandState.Enabled = true;
          return true;
        case "Tree.Update":
          commandState.Enabled = ImDocumentData.ShowDebugInfo && this.docTreeViewDlg != null && this.docTreeViewDlg.GetFocusedControl() != null;
          return true;
        case "UpdateFormulas":
          commandState.Enabled = !this.ReadOnly && !this.Document.IsFormulaLib;
          commandState.Visible = ImDocumentData.ShowDebugInfo && !this.Document.IsFormulaLib;
          return true;
        default:
          if (this.CanShowRedline() && this.redlineController.QueryStatus(commandState))
            return true;
          if (this.BaseEditCommandsEnabled)
          {
            switch (commandState.CommandName)
            {
              case "CallEditor":
                commandState.Visible = true;
                break;
              case "BlockGeometryChanging":
                commandState.Visible = true;
                break;
              case "UnblockGeometryChanging":
                commandState.Visible = true;
                break;
              case "CreateDataField":
                commandState.Visible = true;
                break;
              case "DocEditor.UpdateDocumentLinks":
                commandState.Visible = true;
                break;
            }
          }
          DocumentTreeNode[] context = NodeContextMenu.ContextForContextMenu;
          if (context == null || !NodeContextMenu.ContextMenuCommand)
            context = this.DocumentControl.GetSelectedNodes();
          if (commandState.CommandName == "DocEditor.InsertFormula")
            commandState.Visible = true;
          flag2 = base.QueryStatus(commandState);
          if (!flag2 && this.MenuHelper != null)
            flag2 = this.MenuHelper.QueryStatus(commandState, context, this.DocumentControl);
          string commandName = commandState.CommandName;
          // ISSUE: reference to a compiler-generated method
          switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
          {
            case 148473325:
              if (commandName == "NewPageBefore")
              {
                if (flag1)
                {
                  commandState.Text = LocalizationHolder.rm.GetString("Document.Client_147");
                  break;
                }
                break;
              }
              break;
            case 500134568:
              if (commandName == "Navigation.FirstPage")
              {
                if (flag1)
                {
                  commandState.Text = LocalizationHolder.rm.GetString("Document.Client_143");
                  commandState.ToolTipText = LocalizationHolder.rm.GetString("Document.Client_143");
                }
                base.QueryStatus(commandState);
                break;
              }
              break;
            case 705469979:
              if (commandName == "PrevPage")
              {
                commandState.Text = !flag1 ? LocalizationHolder.rm.GetString("Document.Client_159") : LocalizationHolder.rm.GetString("Document.Client_150");
                break;
              }
              break;
            case 787014169:
              if (commandName == "DocEditor.Page")
              {
                commandState.Text = !flag1 ? LocalizationHolder.rm.GetString("Document.Client_99") : LocalizationHolder.rm.GetString("Document.Client_152");
                break;
              }
              break;
            case 1947474600:
              if (commandName == "NewPageAfter")
              {
                if (flag1)
                {
                  commandState.Text = LocalizationHolder.rm.GetString("Document.Client_148");
                  break;
                }
                break;
              }
              break;
            case 2069430092:
              if (commandName == "Navigation.LastPage")
              {
                if (flag1)
                {
                  commandState.Text = LocalizationHolder.rm.GetString("Document.Client_146");
                  commandState.ToolTipText = LocalizationHolder.rm.GetString("Document.Client_146");
                  break;
                }
                break;
              }
              break;
            case 2149055389:
              if (commandName == "Navigation.NextPage")
              {
                if (flag1)
                {
                  commandState.Text = LocalizationHolder.rm.GetString("Document.Client_145");
                  commandState.ToolTipText = LocalizationHolder.rm.GetString("Document.Client_145");
                  break;
                }
                break;
              }
              break;
            case 2177272782:
              if (commandName == "RemovePage")
              {
                commandState.Text = !flag1 ? LocalizationHolder.rm.GetString("Document.Client_158") : LocalizationHolder.rm.GetString("Document.Client_149");
                break;
              }
              break;
            case 2525280601:
              if (commandName == "Navigation.PrevPage")
              {
                if (flag1)
                {
                  commandState.Text = LocalizationHolder.rm.GetString("Document.Client_144");
                  commandState.ToolTipText = LocalizationHolder.rm.GetString("Document.Client_144");
                }
                base.QueryStatus(commandState);
                break;
              }
              break;
            case 2651393395:
              if (commandName == "NextPage")
              {
                commandState.Text = !flag1 ? LocalizationHolder.rm.GetString("Document.Client_160") : LocalizationHolder.rm.GetString("Document.Client_151");
                break;
              }
              break;
            case 3565823065:
              if (commandName == "CreateNextPageTemplate")
              {
                if (flag1)
                {
                  commandState.Visible = false;
                  break;
                }
                break;
              }
              break;
          }
          break;
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      commandState.Enabled = false;
      return true;
    }
    return flag2;
  }

  /// <summary>Пришло событие "Изменился рендерер панелей инструментов"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    try
    {
      this.barManager.Renderer = (sender as BarManager).Renderer;
      if (this.DocumentControl == null || this.DocumentControl.PageControl == null || this.DocumentControl.PageControl.MenuBar == null)
        return;
      this.DocumentControl.PageControl.MenuBar.Renderer = this.barManager.Renderer;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>вернуть раздел справки для контрола</summary>
  public override string HelpID => "1183";

  /// <summary>Вызывать диалог настройки параметров при первом сохранении документа в объект БД</summary>
  public bool CallDialogWithObjectParamsBeforeSave { get; set; } = true;

  /// <summary>Тип документа в БД по умолчанию.
  /// Если тип не задан, то при первом сохранении документа в БД, будет показан диалог выбора типа</summary>
  public int DefaultDocumentDbObjectType { get; set; } = -1;

  public bool CanBeOpenedInNewWindowsAsObject => this.documentID.IsDefinedId();

  public void OpenNewInstanceAsObject()
  {
    ImDocumentEditorForm.DoOpenInNewWindowCommand(new long[1]
    {
      this.documentID
    });
  }
}
