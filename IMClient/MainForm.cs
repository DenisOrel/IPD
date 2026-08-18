
// Type: IMClient.MainForm




using IMClient.AboutBox;
using IMClient.PropertyPages;
using IMClient.Services;
using IMClient.Splash;
using IMClient.ToolbarControls;
using IMClient.UI.Winforms;
using IMClient.Views;
using Intermech;
using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Controls;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;
using Intermech.Client.Core.Navigator.Controls.Views;
using Intermech.Client.Core.Services;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Projects;
using Intermech.NavBars;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Protection;
using Intermech.Search;
using Intermech.Search.ButtonBars;
using Intermech.Search.Configuration;
using Intermech.Search.EditingContexts;
using Intermech.Search.NotificationSelections;
using Intermech.Search.UI;
using Intermech.Search.UI.Toolbars;
using Intermech.Search.VoiceAssistant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using WaitCursor;


namespace IMClient
{
    internal sealed class MainForm : 
      Form,
      IContentProvider,
      IToolbarProvider,
      IAddressService,
      IStatusBar,
      ICommandTarget,
      IEditingContextToolbar,
      IIODestination,
      IMainFormUpdate,
      IViewsManagerService,
      IContextFiltrationPanel
    {
      private IConfigurationManager _configurationManager;
      private System.IServiceProvider _serviceProvider;
      private PluginManager _pluginManager;
      private FormWindowState _oldState;
      private CommandManager _commandManager;
      private NavigateManager _navigateManager;
      private PropertyPagesService _propertyPages;
      private RecentObjectsSettingsViewPage _recentObjects;
      private SplashScreenService _splashScreen;
      private bool _showSplash;
      private AutoCompleteStringCollection _acsc;
      private bool StatusBar_ShowPanels;
      internal static string ProxyPrefix = "@ ";
      private ICommandState _gotoAddressState;
      private ArrayList _documentMenuItems;
      private MainFormInitParams _initParams;
      private AssemblyInitializerModule _clientCoreInitializer;
      private AssemblyLateInitializerModule _clientCoreLateInitializer;
      private FiltrationPanel _filtration;
      private IHotKeysManager _hotKeysManager;
      private IIODispatcher _IODispatcher = (IIODispatcher) new IODispatcher();
      private AdjustableMenuCommands _adjustableContextMenus;
      private AdjustableViews _adjustableViews;
      private ProjectsDropDownControl _projectsFiltration;
      private string _lastProjectGuid = "{35D4B53A-6C18-4184-9016-B3B63791741F}";
      private string _lastProjectModeGuid = "{51A27A73-24F1-46FB-903A-5F07006417DE}";
      private ContextFiltrationPanelControl _editingContextsFiltration;
      private string _lastEditingContextGuid = "{76E7E5DF-C8F6-43D2-9985-E8F2A9BC84CE}";
      private string _lastEditingContextModeGuid = "{7EEAD34F-B2FF-4C22-B0DB-0395BB1BBC17}";
      private string _lastEditingContextsHistryGuid = "{79985D8D-B2EE-4F63-9FFA-1D9B9995D87C}";
      private CreateObjTypesMenuMRU _createObjTypesMenuMRU;
      private ICreateObjByTypeMRU _createObjByTypeMRU;
      private CompositionByObjectTypesFilters _otNodeFilters;
      private ArrayList _hidedToolbars;
      private IDefaultCommands4ObjTypes _defaultCommands4ObjTypes;
      private SizeF _scaleFactor = SizeF.Empty;
      private Point _desktopLocation;
      private IContainer components;
      private DockControl _pluginView;
      private DockControl _clipboardView;
      private DockControl _namedImagesView;
      private DockControl _propertyView;
      private DockControl _backgroundView;
      private DockControl _outputView;
      private DockControl _errorsView;
      private DockControl _serverObjects;
      private DockControl _serverOutputView;
      private DockControl _appPanel;
      private DockControl _documentLayout;
      private DockControl _overview;
      private BarManager _barManager;
      private MenuBarItem _fileMenuBarItem;
      private ToolBarContainer bottomBarDock;
      private ToolBarContainer leftBarDock;
      private ToolBarContainer rightBarDock;
      private ToolBarContainer topBarDock;
      private ImageList _mainImageList;
      private MenuButtonItem _pluginsMenuButtonItem;
      private MenuButtonItem _loadPluginMenuButtonItem;
      private MenuButtonItem _pluginsListMenuButtonItem;
      private MenuButtonItem _exitMenuButtonItem;
      private MenuBarItem _compositionMenuBarItem;
      internal MenuButtonItem _createMenuButtonItem;
      private MenuButtonItem _closeMenuButtonItem;
      internal DropDownMenuItem btNewItem;
      private ButtonItem btSave;
      private ButtonItem btPrint;
      private ButtonItem btPrintPreview;
      private ButtonItem btCut;
      private ButtonItem btCopy;
      private ButtonItem btPaste;
      private DropDownMenuItem btUndo;
      private DropDownMenuItem btRedo;
      private DropDownMenuItem btDocumentBack;
      private DropDownMenuItem btDocumentForward;
      private MenuBarItem _editMenuBarItem;
      private MenuButtonItem _undoMenuButtonItem;
      private MenuButtonItem _redoMenuButtonItem;
      private MenuButtonItem _cutMenuButtonItem;
      private MenuButtonItem _copyMenuButtonItem;
      private MenuButtonItem _pasteMenuButtonItem;
      private MenuButtonItem _printMenuButtonItem;
      private MenuButtonItem _printPreviewMenuButtonItem;
      private MenuBar _mainMenu;
      private Intermech.Bars.ToolBar _mainToolbar;
      private MenuButtonItem _taskBarMenuButtonItem;
      private MenuButtonItem _settingsMenuButtonItem;
      private MenuButtonItem _namedImageListMenuButtonItem;
      private ContextMenuBarItem docTabContextMenu;
      private MenuButtonItem mnCloseDocument;
      private MenuButtonItem mnNewHorizontalGroup;
      private MenuButtonItem mnContextSaveDocument;
      private MenuButtonItem mnNewVerticalGroup;
      private MenuButtonItem _clipboardMenuButtonItem;
      private MenuButtonItem _backgroundTasksMenuButtonItem;
      private MenuButtonItem _outputMenuButtonItem;
      private MenuButtonItem _serverObjectsMenuButtonItem;
      private DockManager _dockManager;
      private DockContainer leftDock;
      private DockContainer rightDock;
      private DockContainer bottomDock;
      private DockContainer topDock;
      private DocumentContainer _docContainer;
      private MenuBarItem _windowsMenuBarItem;
      private MenuButtonItem _closeAllWindowsMenuButtonItem;
      private MenuButtonItem mnCloseAllBut;
      private MenuButtonItem mnOpenNewInstanceAsObject;
      private Intermech.Bars.ToolBar _addressToolbar;
      private ComboBoxItem cbAddress;
      private ButtonItem btGotoAddress;
      private System.Windows.Forms.Timer _disableActiveTargetTimer;
      private StatusBar _statusBar;
      private MenuButtonItem _serverOutputMenuButtonItem;
      private MenuButtonItem _saveMenuButtonItem;
      private MenuButtonItem _saveAsMenuButtonItem;
      private MenuButtonItem _fullScreenMenuButtonItem;
      private StatusBarPanel statusBarPanel1;
      private StatusBarPanel sbpUserName;
      private StatusBarPanel sbpRole;
      private Intermech.Bars.ToolBar _filterToolbar;
      private ComboBoxItem cbFiltrationRule;
      private ButtonItem btRuleBrowser;
      private ButtonItem btRuleVariant;
      private ButtonItem btRuleHint;
      private MenuButtonItem _overviewMenuButtonItem;
      private MenuButtonItem _documentLayoutMenuButtonItem;
      internal MenuButtonItem _createNewMenuButtonItem;
      private MenuButtonItem _userInterfaceMenuButtonItem;
      private Intermech.Bars.ToolBar _navigateToolbar;
      private MenuBarItem _tuningMenuBarItem;
      internal StatusBarPanel sbpProject;
      private Intermech.Bars.ToolBar toolBarProjects;
      internal ButtonItem _buttonProjectRefresh;
      internal DropDownMenuItem _buttonProjectFilterMode;
      private DropDownMenuItem btNavigateBack;
      private DropDownMenuItem btNavigateForward;
      private ButtonItem btCheckOut;
      private ButtonItem btSaveChanges;
      private ButtonItem btCheckIn;
      private ButtonItem btCancelChanges;
      private ButtonItem btAdminCancelChanges;
      private ButtonItem btParametersCard;
      private ButtonItem btDelete;
      private ButtonItem btExclude;
      private ButtonItem btRefresh;
      private MenuButtonItem _previousWindowMenuButtonItem;
      private MenuButtonItem _nextWindowMenuButtonItem;
      private MenuBarItem _exportImportMenuBarItem;
      private MenuButtonItem _checkOutMenuButtonItem;
      private MenuButtonItem _checkInMenuButtonItem;
      private MenuButtonItem _saveChangesMenuButtonItem;
      private MenuButtonItem _cancelChangesMenuButtonItem;
      private MenuButtonItem _adminCancelChangesMenuButtonItem;
      private MenuButtonItem _deleteMenuButtonItem;
      private MenuButtonItem _excludeMenuButtonItem;
      private MenuButtonItem _cardMenuButtonItem;
      internal StatusBarPanel spbLevel;
      private MenuBarItem _helpMenuBarItem;
      private MenuButtonItem _aboutMenuButtonItem;
      private MenuBarItem _applicationsMenuBarItem;
      private Intermech.Bars.ToolBar toolBarEditingContexts;
      internal ButtonItem _buttonEditingContextsRefresh;
      internal DropDownMenuItem _buttonEditingContextMode;
      internal ButtonItem _buttonEditingContextsBrowse;
      internal ButtonItem _buttonEditingContextsCreate;
      internal ButtonItem _buttonEditingContextsEdit;
      private MenuButtonItem _helpMenuButtonItem;
      private MenuButtonItem _indexMenuButtonItem;
      private MenuButtonItem _searchMenuButtonItem;
      private MenuButtonItem _dynamicHelpMenuButtonItem;
      private ButtonItem btClearHistory;
      private MenuButtonItem _findMenuButtonItem;
      private StatusBarPanel sbpIOUserName;
      private DropDownMenuItem contextsList;
      private LabelItem labelContext;
      private LabelItem labelItem1;
      private DropDownMenuItem projectsList;
      private MenuButtonItem _fulfillmentOfDutiesMenuButtonItem;
      private MenuButtonItem _supportMenuButtonItem;
      private ButtonItem btnFullRefresh;
      private Intermech.Bars.ToolBar _configurationToolbar;
      private ButtonItem _openConfigurationDialogButtonItem;
      private MenuBarItem _viewMenuBarItem;
      private MenuButtonItem _administratorUtilitiesMenuButtonItem;
      private ButtonItem _useStoredExplicitPartVersionIDButtonItem;
      private MenuButtonItem _uinotifmenuButtonItem;
      private StatusBarPanel _voiceAssistantStatusBarPanel;
      private MenuButtonItem menuButtonItem1;
      private ButtonItem btnLineStyleSetup;

      public MainForm()
      {
        this.InitializeComponent();
        this.StatusBar_ShowPanels = this._statusBar.ShowPanels;
        this._mainMenu.ShortcutListener.SecondaryShortcutAction += new SecondaryShortcutEventHandler(this.ShortcutListener_SecondaryShortcutAction);
        this._documentMenuItems = new ArrayList();
        this._hidedToolbars = new ArrayList();
        this._filtration = new FiltrationPanel(this._filterToolbar, this.cbFiltrationRule, this.btRuleBrowser, this.btRuleVariant, this.btRuleHint, this._useStoredExplicitPartVersionIDButtonItem, true);
      }

      public void Initialize(MainFormInitParams initParams)
      {
        if (initParams == null)
          throw new ArgumentNullException(nameof (initParams));
        this.ValidateParams(initParams);
        this._initParams = initParams;
        try
        {
          this.ShowLoggedUserInfo();
          this.InitializeServices();
          Application.Idle += new EventHandler(this.Application_Idle);
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
        this._filtration.Initialize();
        this._barManager.AddToolbar(this._configurationToolbar);
        ToolbarHelper.InitializeConfigurationToolbar(this._configurationToolbar, this.GetOutputViewService());
      }

      private void ValidateParams(MainFormInitParams initParams)
      {
        if (initParams.SharedLibraryInitializer == null)
          throw new InvalidOperationException("Property 'SharedLibraryInitializer' is not set.");
        if (initParams.IMServerService == null)
          throw new InvalidOperationException("Property 'IMServerService' is not set.");
        if (initParams.CreateSessionPluginLoader == null)
          throw new InvalidOperationException("Property 'CreateSessionPluginLoader' is not set.");
        if (initParams.CreatePersonalPluginLoader == null)
          throw new InvalidOperationException("Property 'CreatePersonalPluginLoader' is not set.");
      }

      private void AddStandardIcons(INamedImageList namedImageList, ICommandManager commandManager)
      {
        Stream manifestResourceStream1 = this.GetType().Assembly.GetManifestResourceStream("IMClient.Resources.StandardIcons.bmp");
        if (manifestResourceStream1 != null)
        {
          using (Bitmap images = new Bitmap(manifestResourceStream1))
          {
            images.MakeTransparent();
            namedImageList.AddStrip((Image) images, new string[128 /*0x80*/]
            {
              "imgEmpty",
              "imgSave",
              "imgSaveAll",
              "imgCut",
              "imgCopy",
              "imgPaste",
              "imgUndo",
              "imgRedo",
              "imgDocBack",
              "imgDocForward",
              "imgProp",
              "imgDelete",
              "imgNewItem",
              "imgOpenItem",
              "imgPrint",
              "imgNavigateBack",
              "imgNavigateForward",
              "imgHome",
              "imgRefresh",
              "imgStop",
              "imgTreeUp",
              "imgBack",
              "imgForward",
              "imgFind",
              "imgReplace",
              "imgFavorites",
              "imgAddFavorites",
              "imgPrintPreview",
              "imgPlugin",
              "imgPluginLoad",
              "imgPluginList",
              "imgAdminPane",
              "imgTuning",
              "imgFolder",
              "imgFolderOpened",
              "imgAppClose",
              "imgUsers",
              "imgUser",
              "imgKeys",
              "imgDocument",
              "imgNewFolder",
              "imgPropPage",
              "imgLCStepDocument",
              "imgHorGroup",
              "imgVerGroup",
              "imgOk",
              "imgApply",
              "imgClose",
              "imgAppPane2",
              "imgAppPane",
              "imgTrashFull",
              "imgTrash",
              "imgAccess",
              "imgFolderAccess",
              "imgCard",
              "imgNavigator",
              "imgClose2",
              "imgBackground",
              "imgOutput",
              "imgPause",
              "imgStart",
              "imgStop2",
              "imgPluginWarn",
              "imgSessionPlugin",
              "imgPluginFolder",
              "imgPackage",
              "imgOutputServer",
              "imgCloseAll",
              "imgFetchData2",
              "imgBooks",
              "imgNewWindow",
              "imgFetchData",
              "imgBook",
              "imgFilesList",
              "imgThumbnails",
              "imgContains",
              "imgEntersTo",
              "imgGotoAddress",
              "imgImageLib",
              "imgImageLib2",
              "imgListView",
              "imgTreeView",
              "imgChecked",
              "imgUnchecked",
              "imgGrayed",
              "imgFullScreen",
              "imgView",
              "imgServerObjects",
              "imgZoomIn",
              "imgZoomOut",
              "imgZoomAll",
              "imgSign",
              "imgSign2",
              "imgDocumentLayout",
              "imgOverview",
              "imgClearAll",
              "imgWordWrap",
              "imgZoomPrevious",
              "imgTableEdit",
              "imgRolesSettings",
              "imgPlugins",
              "imgInsertItem",
              "imgPerformance",
              "imgCoincidences",
              "imgCompCompare",
              "imgDistinctions",
              "imgAdditionalViews",
              "imgFilter",
              "imgDefaultColumns",
              "imgNewScheme",
              "imgEditScheme",
              "imgMail",
              "imgCopies",
              "imgNewVersion",
              "imgCheckOut",
              "imgUndoCheckOut",
              "imgDeleteObject",
              "imgRedPencil",
              "imgRedLine",
              "imgRedEllipse",
              "imgRedNote",
              "imgPointer",
              "imgSystemVariables",
              "imgRectSelect",
              "imgLinecolor",
              "imgSort",
              "imInProducts",
              "imgObject"
            });
          }
          manifestResourceStream1.Close();
          Stream manifestResourceStream2 = this.GetType().Assembly.GetManifestResourceStream("IMClient.Resources.StandardIcons2.bmp");
          if (manifestResourceStream2 != null)
          {
            using (Bitmap images = new Bitmap(manifestResourceStream2))
            {
              images.MakeTransparent();
              namedImageList.AddStrip((Image) images, new string[74]
              {
                "imgBriefcase",
                "imgImportBriefcase",
                "imgCheckBriefcase",
                "imgExportBriefcase",
                "imgExportDB",
                "imgBookmark",
                "imgSelectionReplace",
                "imgSearch",
                "imgSearchTree",
                "imgReport",
                "imgTableReport",
                "imgTableReportEdit",
                "imgPageFirst",
                "imgPagePrev",
                "imgPageNext",
                "imgPageLast",
                "imgExpandComposition",
                "imgExclude",
                "imgZoom1to1",
                "imgMainPageIcon",
                "imgViewSettings",
                "imgProject",
                "imgProjectFilter",
                "imgProjectTeam",
                "imgGroup",
                "imgCalendarGoTo",
                "imgUserRoles",
                "imgFindInTables",
                "imgFindByImages",
                "imgTableRestructure",
                "imgRecFilter",
                "imgRecFilterShow",
                "imgRecFilterAdd",
                "imgRecFilterRemove",
                "imgRecFilterClear",
                "imgAutoupgradeScripts",
                "imgClean",
                "imgVersionsList",
                "imgVersionsTree",
                "imgTableView",
                "imgSpecRow",
                "imgPortal",
                "imgDistance",
                "imgLink",
                "imgHelp",
                "imgFindText",
                "imgFindTextNext",
                "imgDocCopies",
                "imgRegDocument",
                "imgRedEllipseFill",
                "imgRedCircle",
                "imgRedCircleFill",
                "imgRedRectangle",
                "imgRedRectangleFill",
                "imgFillColor",
                "imgFontColor",
                "imgUnregisterDoc",
                "imgCopyListFromDoc",
                "imgLock",
                "imgFilterAgreed",
                "imgFilterRejected",
                "imgFilterInconsistent",
                "imgFilterCorrected",
                "imgAddSubscrByRoute",
                "imgCreateCopiesByDeliveryList",
                "imgNormaCS",
                "imgNewRedDoc",
                "imgArchAutoPlace",
                "imgRedViewOnly",
                "imgRedEdit",
                "imgExtRedliningEditor",
                "imgAddToFavoritesNavigator",
                "imgRemoveFromFavoritesNavigator",
                "imgCryptoSignUp"
              });
            }
            manifestResourceStream2.Close();
          }
          this.btNewItem.ImageIndex = namedImageList.ImageIndex("imgNewItem");
          this._createMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgNewItem");
          this._createNewMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgNewItem");
          commandManager.Add((ButtonItemBase) this._createNewMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgNewItem");
          commandManager.Add((ButtonItemBase) this.btSave, (ButtonItemBase) this._saveMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgSave");
          commandManager.Add((ButtonItemBase) this._saveAsMenuButtonItem);
          commandManager.Add((ButtonItemBase) this.btPrint, (ButtonItemBase) this._printMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgPrint");
          commandManager.Add((ButtonItemBase) this.btPrintPreview, (ButtonItemBase) this._printPreviewMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgPrintPreview");
          commandManager.Add((ButtonItemBase) this.btnLineStyleSetup);
          commandManager.Add((ButtonItemBase) this.btCut, (ButtonItemBase) this._cutMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgCut");
          commandManager.Add((ButtonItemBase) this.btCopy, (ButtonItemBase) this._copyMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgCopy");
          commandManager.Add((ButtonItemBase) this.btPaste, (ButtonItemBase) this._pasteMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgPaste");
          commandManager.Add((ButtonItemBase) this.btDelete, (ButtonItemBase) this._deleteMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgDelete");
          commandManager.Add((ButtonItemBase) this.btExclude, (ButtonItemBase) this._excludeMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgExclude");
          commandManager.Add((ButtonItemBase) this.btParametersCard, (ButtonItemBase) this._cardMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgCard");
          commandManager.Add((ButtonItemBase) this.btUndo, (ButtonItemBase) this._undoMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgUndo");
          commandManager.Add((ButtonItemBase) this.btRedo, (ButtonItemBase) this._redoMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgRedo");
          this._dynamicHelpMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgHelp");
          this.btClearHistory.ImageIndex = namedImageList.ImageIndex("imgClearAll");
          this._previousWindowMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgDocBack");
          this._nextWindowMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgDocForward");
          this.btDocumentBack.ImageIndex = namedImageList.ImageIndex("imgDocBack");
          this.btDocumentForward.ImageIndex = namedImageList.ImageIndex("imgDocForward");
          this._fullScreenMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgFullScreen");
          this._fullScreenMenuButtonItem.Shortcut = (Shortcut) 327693 /*0x05000D*/;
          commandManager.Add((ButtonItemBase) this.btNavigateBack).ImageIndex = namedImageList.ImageIndex("imgNavigateBack");
          commandManager.Add((ButtonItemBase) this.btNavigateForward).ImageIndex = namedImageList.ImageIndex("imgNavigateForward");
          commandManager.Add((ButtonItemBase) this.btRefresh).ImageIndex = namedImageList.ImageIndex("imgRefresh");
          this._documentLayoutMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgDocumentLayout");
          this._overviewMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgOverview");
          commandManager.Add((ButtonItemBase) this._overviewMenuButtonItem);
          commandManager.Add((ButtonItemBase) this._documentLayoutMenuButtonItem);
          this._gotoAddressState = commandManager.Add((ButtonItemBase) this.btGotoAddress);
          this._gotoAddressState.ImageIndex = namedImageList.ImageIndex("imgGotoAddress");
          this.cbAddress.ComboBox.KeyPress += new KeyPressEventHandler(this.AddressComboBox_KeyPress);
          this._pluginsMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgPlugin");
          this._loadPluginMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgPluginLoad");
          this._pluginsListMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgPluginList");
          this._taskBarMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgAppPane");
          this._settingsMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgTuning");
          this._exitMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgAppClose");
          this._clipboardMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgPaste");
          this._backgroundTasksMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgBackground");
          this._outputMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgOutput");
          this._serverObjectsMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgServerObjects");
          this._serverOutputMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgOutputServer");
          this.mnNewHorizontalGroup.ImageIndex = namedImageList.ImageIndex("imgHorGroup");
          this.mnNewVerticalGroup.ImageIndex = namedImageList.ImageIndex("imgVerGroup");
          this.mnContextSaveDocument.ImageIndex = namedImageList.ImageIndex("imgSave");
          this.mnCloseDocument.ImageIndex = namedImageList.ImageIndex("imgClose");
          this._closeMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgClose");
          this._closeAllWindowsMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgCloseAll");
          commandManager.Add((ButtonItemBase) this._findMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgFind");
        }
        Stream manifestResourceStream3 = this.GetType().Assembly.GetManifestResourceStream("IMClient.Resources.FiltrationIcons.bmp");
        if (manifestResourceStream3 != null)
        {
          using (Bitmap images = new Bitmap(manifestResourceStream3))
          {
            images.MakeTransparent();
            namedImageList.AddStrip((Image) images, new string[38]
            {
              "imgVersionRule",
              "imgVersionRuleEditor",
              "imgVersionRuleImport",
              "imgVersionRuleExport",
              "imgRedBall",
              "imgYellowBall",
              "imgGreenBall",
              "imgApplyBall",
              "imgRuleCriterion",
              "imgInvalidRule",
              "imgCorruptedRule",
              "imgVersionsID",
              "imgRolesContextMenus",
              "imgCheckOut",
              "imgSaveChanges",
              "imgCheckIn",
              "imgCancelChanges",
              "imgAdminCancelChanges",
              "imgObjectsFilter",
              "imgRolesPlugins",
              "imgRolesViews",
              "imgFunnel",
              "imgFunnelActive",
              "imgFunnelAdd",
              "imgFunnelSetup",
              "imgFunnelDisabled",
              "imgManualSorting",
              "imgManualSortingSetup",
              "imgObjectVisibility",
              "imgEditingContextsEdit",
              "imgEditingContextsCreate",
              "imgEditingContextsBrowse",
              "imgEditingContextsMode",
              "imgEditingContextsAddComposition",
              "imgEditingContextsAdd",
              "imgEditingContextsModeAuto",
              "imgFilterByCurrentProject",
              "imgFilterByUserProjects"
            });
          }
          manifestResourceStream3.Close();
          commandManager.Add((ButtonItemBase) this.btCheckOut, (ButtonItemBase) this._checkOutMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgCheckOut");
          commandManager.Add((ButtonItemBase) this.btCheckIn, (ButtonItemBase) this._checkInMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgCheckIn");
          commandManager.Add((ButtonItemBase) this.btSaveChanges, (ButtonItemBase) this._saveChangesMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgSaveChanges");
          commandManager.Add((ButtonItemBase) this.btCancelChanges, (ButtonItemBase) this._cancelChangesMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgCancelChanges");
          commandManager.Add((ButtonItemBase) this.btAdminCancelChanges, (ButtonItemBase) this._adminCancelChangesMenuButtonItem).ImageIndex = namedImageList.ImageIndex("imgAdminCancelChanges");
        }
        Stream manifestResourceStream4 = this.GetType().Assembly.GetManifestResourceStream("IMClient.Resources.EventLogCreateFilter.ico");
        if (manifestResourceStream4 == null)
          return;
        Icon icon = new Icon(manifestResourceStream4);
        manifestResourceStream4.Close();
        namedImageList.Add(icon, "imgEventLogCreateFilterIcon");
        icon.Dispose();
      }

      private void AddBigImages(IBigImageList bigImageList)
      {
        Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("IMClient.Resources.BigImages.bmp");
        if (manifestResourceStream == null)
          return;
        Bitmap images = new Bitmap(manifestResourceStream);
        images.MakeTransparent();
        bigImageList.AddStrip((Image) images, new string[8]
        {
          "imgEmpty",
          "imgWarning",
          "imgBriefcase",
          "imgError",
          "imgInfo",
          "imgWorking",
          "imgHelp",
          "imgLocked"
        });
      }

      private void InitializeServices()
      {
        this._serviceProvider = (System.IServiceProvider) ServicesManager.ServiceContainer;
        ServiceLocator.Initialize((System.IServiceProvider) ServicesManager.ServiceContainer);
        MainMenuService service1 = new MainMenuService(this._mainMenu);
        ServiceLocator.Register<IMainMenuService>((IMainMenuService) service1);
        service1.SuppressRebuildMainMenu();
        service1.RegisterMenuItems(MainMenuItemSite.TuningTop, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._userInterfaceMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.TuningTop, MainMenuItemPosition.Last, new MenuButtonItem[1]
        {
          this._pluginsMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.TuningMiddle, MainMenuItemPosition.First, new MenuButtonItem[1]
        {
          this._administratorUtilitiesMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.TuningBottom, MainMenuItemPosition.Last, new MenuButtonItem[1]
        {
          this._settingsMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewTop, MainMenuItemPosition.First, new MenuButtonItem[1]
        {
          this._taskBarMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewTop, MainMenuItemPosition.Second, new MenuButtonItem[1]
        {
          this._fullScreenMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewTop, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._overviewMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._clipboardMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._namedImageListMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._serverObjectsMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._backgroundTasksMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._outputMenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._uinotifmenuButtonItem
        });
        service1.RegisterMenuItems(MainMenuItemSite.ViewBottom, MainMenuItemPosition.Default, new MenuButtonItem[1]
        {
          this._serverOutputMenuButtonItem
        });
        this._configurationManager = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, true);
        this._configurationManager.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(this.SaveFormLocationAndSize);
        this._commandManager = new CommandManager();
        this._commandManager.AddTarget((ICommandTarget) this);
        BigImageList serviceInstance1 = new BigImageList();
        this.AddBigImages((IBigImageList) serviceInstance1);
        ServicesManager.AddService(typeof (IBigImageList), (object) serviceInstance1);
        NamedImageList serviceInstance2 = new NamedImageList(this._mainImageList);
        ServicesManager.AddService(typeof (INamedImageList), (object) serviceInstance2);
        this.AddStandardIcons((INamedImageList) serviceInstance2, (ICommandManager) this._commandManager);
        ServicesManager.AddService(typeof (ICategoryTypeIconService), (object) new CategoryTypeIconService((Control) this));
        ServicesManager.AddService(typeof (ICategoryTypeStateImageService), (object) new CategoryTypeStateImageService((Control) this));
        new Thread(new ThreadStart(Intermech.Navigator.Services.LoadIconsFromResources))
        {
          IsBackground = true
        }.Start();
        WellKnownWindowsOpenService.Register();
        UISettings.FirstTimeRunning = this._configurationManager.Configurations.Count == 0;
        this.RestoreFormLocationAndSize(this._configurationManager);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this._administratorUtilitiesMenuButtonItem.Visible = sessionKeeper.Session.IsAdmin;
          ServicesManager.AddService(typeof (IViewsManagerService), (object) this);
          ServicesManager.AddService(typeof (ICommandManager), (object) this._commandManager);
          ServicesManager.AddService(typeof (IAddressService), (object) this);
          ServicesManager.AddService(typeof (IStatusBar), (object) this);
          this.InitializeAddressAutocomplete();
          this._createObjByTypeMRU = (ICreateObjByTypeMRU) new CreateObjByTypeMRU();
          ServicesManager.AddService(typeof (ICreateObjByTypeMRU), (object) this._createObjByTypeMRU);
          this._createObjTypesMenuMRU = new CreateObjTypesMenuMRU(this);
          ServicesManager.AddService(typeof (ICreateObjectButton), (object) this._createObjTypesMenuMRU);
          this._propertyPages = (PropertyPagesService) ServicesManager.GetService(typeof (PropertyPagesService));
          UISettingsPage uiSettingsPage = new UISettingsPage((System.IServiceProvider) ServicesManager.ServiceContainer);
          this._showSplash = UISettings.ShowSplash;
          ServicesManager.AddService(typeof (IOutputView), (object) this.GetOutputViewService());
          if (this._showSplash)
            this.StartSplash();
          ServicesManager.AddService(typeof (IFiltrationService), (object) this._filtration);
          ServicesManager.AddService(typeof (IEditingContextToolbar), (object) this);
          Module.Initialize();
          this._hotKeysManager = (IHotKeysManager) new HotKeysManager();
          ServicesManager.AddService(typeof (IHotKeysManager), (object) this._hotKeysManager);
          this._IODispatcher.RegisterDestination((IIODestination) this);
          ServicesManager.AddService(typeof (IIODispatcher), (object) this._IODispatcher);
          this._navigateManager = new NavigateManager((ICommandManager) this._commandManager, this.btNavigateBack, this.btNavigateForward);
          this._commandManager.AddTarget((ICommandTarget) this._navigateManager);
          ServicesManager.AddService(typeof (INavigateManager), (object) this._navigateManager);
          this._adjustableContextMenus = new AdjustableMenuCommands((AdjustableMenuCommands) null);
          ServicesManager.AddService(typeof (AdjustableMenuCommands), (object) this._adjustableContextMenus);
          this._adjustableViews = new AdjustableViews();
          ServicesManager.AddService(typeof (AdjustableViews), (object) this._adjustableViews);
          this._otNodeFilters = new CompositionByObjectTypesFilters();
          ServicesManager.AddService(typeof (ICompositionByObjectTypesFilters), (object) this._otNodeFilters);
          this._pluginManager = new PluginManager((System.IServiceProvider) ApplicationServices.Container, (IConfigurationManager) ServiceUtils.GetService<ILocalConfigurationManager>((object) ApplicationServices.Container, true), this.GetOutputViewService(), ServiceUtils.GetService<IAlertMessageService>((object) ApplicationServices.Container, true));
          if (this._initParams.PluginManagerConfigureAction != null)
            this._initParams.PluginManagerConfigureAction(this._pluginManager);
          ServicesManager.AddService(typeof (IPluginManager), (object) this._pluginManager);
          ServicesManager.AddService(typeof (IPopupMenuHost), (object) this._mainMenu);
          ServicesManager.AddService(typeof (IContentProvider), (object) this);
          ServicesManager.AddService(typeof (IToolbarProvider), (object) this);
          ServicesManager.AddService(typeof (IMainFormUpdate), (object) this);
          this._appPanel = (DockControl) new AppPanel();
          ServicesManager.AddService(typeof (INavigationBar), (object) ((AppPanel) this._appPanel)._navigationBar);
          this.GetPluginView();
          ServicesManager.AddService(typeof (IPropertyGridView), (object) this.GetPropertyView());
          ServicesManager.AddService(typeof (IClipboard), (object) this.GetClipboardView());
          ServicesManager.AddService(typeof (IBackgroundTaskView), (object) this.GetBackgroundView());
          ServicesManager.AddService(typeof (IUINotificationService), (object) this.GetErrorsView());
          IExceptionHandlerService service2 = ServicesManager.GetService(typeof (IExceptionHandlerService)) as IExceptionHandlerService;
          service2.HandleException += new ExceptionHandler(AccessDeniedExceptionForm.OnExceptionHandler);
          service2.HandleException += new ExceptionHandler(SOAPExceptionForm.OnExceptionHandler);
          service2.HandleException += new ExceptionHandler(this.ExceptionHandlerService_HandleException);
          this._clientCoreInitializer = this._initParams.SharedLibraryInitializer.InitializerModuleFactory.Create<AssemblyInitializerModule>();
          this._clientCoreInitializer.Initialize();
          (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).LoadUserSettings((object) sessionKeeper.Session.SessionGUID);
          if (this._splashScreen != null)
            this._splashScreen.StepName = LocalizationHolder.rm.GetString("IMClient_41");
          Engine.Start();
          UserPropertiesPage userPropertiesPage = new UserPropertiesPage((System.IServiceProvider) ServicesManager.ServiceContainer);
          BarsDockingProperty barsDockingProperty = new BarsDockingProperty((System.IServiceProvider) ServicesManager.ServiceContainer);
          ElementStatusesView elementStatusesView = new ElementStatusesView((System.IServiceProvider) ServicesManager.ServiceContainer);
          OptimizationSettingsPage optimizationSettingsPage = new OptimizationSettingsPage((System.IServiceProvider) ServicesManager.ServiceContainer);
          new CoreClientModule().Initialize();
          Intermech.Search.VoiceAssistant.VoiceAssistant serviceInstance3 = new Intermech.Search.VoiceAssistant.VoiceAssistant();
          serviceInstance3.StateChanged += new EventHandler(this.VoiceAssistant_StateChanged);
          ServicesManager.AddService(typeof (IVoiceAssistant), (object) serviceInstance3);
          this.UpdateVoiceAssistantStatusBarPanel();
          ServicesManager.AddService(typeof (IMainFormClientService), (object) new MainFormClientService(this));
          this._clientCoreLateInitializer = this._initParams.SharedLibraryInitializer.InitializerModuleFactory.Create<AssemblyLateInitializerModule>();
          this._clientCoreLateInitializer.Initialize();
        }
        ServicesManager.AddService(typeof (IProtectionMessageService), (object) new ClientProtectionServiceProxy());
        if (!(ServicesManager.GetService(typeof (IProtectionKey)) is IProtectionKey service3))
          return;
        service3.PostLoad();
      }

      private void StartSplash()
      {
        this._splashScreen = new SplashScreenService();
        this._splashScreen.SetBanner(this._initParams.IMServerService.ServerObject.UsersBanner);
        ServicesManager.AddService(typeof (ISplashService), (object) this._splashScreen);
        this.AddOwnedForm((Form) this._splashScreen);
        this._splashScreen.Show();
        Application.DoEvents();
        this._splashScreen.StepName = LocalizationHolder.rm.GetString("IMClient_39");
      }

      private void InitializeAddressAutocomplete()
      {
        ComboBox comboBox = this.cbAddress.ComboBox;
        this._acsc = new AutoCompleteStringCollection();
        comboBox.AutoCompleteCustomSource = this._acsc;
        comboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
      }

      protected override void WndProc(ref Message m)
      {
        if (m.LParam == new IntPtr(536) && m.WParam == new IntPtr(0))
        {
          if (ServicesManager.GetService(typeof (IProtectionKey)) is IProtectionKey service)
            m.Result = service.CheckHibernate();
        }
        else if (m.Msg == 28 && m.WParam == new IntPtr(1))
        {
          foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
          {
            Form f = openForm;
            if (f.Modal)
            {
              bool success = false;
              f.Invoke((Delegate) (() =>
              {
                try
                {
                  f.Activate();
                  success = true;
                }
                catch
                {
                }
              }));
              if (success)
                break;
            }
          }
        }
        base.WndProc(ref m);
      }

      private void SaveFormLocationAndSize(IConfigurationManager cfgManager)
      {
        IConfiguration configuration = this._configurationManager.Create("ApplicationWindow");
        Point desktopLocation = this._desktopLocation;
        configuration.SetProperty("X", desktopLocation.X.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        configuration.SetProperty("Y", desktopLocation.Y.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        Size size = this.Size;
        configuration.SetProperty("Width", size.Width.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        configuration.SetProperty("Height", size.Height.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (this.WindowState == FormWindowState.Maximized)
          configuration.SetProperty("Maximized", bool.TrueString);
        if (this.WindowState != FormWindowState.Minimized)
          return;
        configuration.SetProperty("Minimized", bool.TrueString);
      }

      private void RestoreFormLocationAndSize(IConfigurationManager cfgManager)
      {
        IConfiguration configuration = this._configurationManager.Open("ApplicationWindow");
        if (configuration != null)
        {
          Point lLocation = Point.Empty;
          Size size = Size.Empty;
          if (configuration.HasProperty("X") && configuration.HasProperty("Y"))
            lLocation = new Point(int.Parse(configuration.GetProperty("X"), (IFormatProvider) CultureInfo.InvariantCulture), int.Parse(configuration.GetProperty("Y"), (IFormatProvider) CultureInfo.InvariantCulture));
          if (configuration.HasProperty("Width") && configuration.HasProperty("Height"))
            size = new Size(int.Parse(configuration.GetProperty("Width"), (IFormatProvider) CultureInfo.InvariantCulture), int.Parse(configuration.GetProperty("Height"), (IFormatProvider) CultureInfo.InvariantCulture));
          if (!Point.Empty.Equals((object) lLocation) && !Size.Empty.Equals((object) size))
          {
            Point point = FormStorage.ValidateLocation(lLocation);
            this.StartPosition = FormStartPosition.Manual;
            this.DesktopLocation = point;
            if (!configuration.HasProperty("Maximized") && !configuration.HasProperty("Minimized"))
              this.Size = size;
          }
          if (configuration.HasProperty("Maximized") || configuration.HasProperty("Minimized"))
            this.WindowState = FormWindowState.Maximized;
        }
        this.RefreshDesktopLocation();
      }

      public event GetContentCallback ContentCallback;

      private DockControl RestoreDockControl(Guid guid, string data)
      {
        if (this.ContentCallback != null)
        {
          IOutputView outputView = this._outputView as IOutputView;
          Delegate[] invocationList = this.ContentCallback.GetInvocationList();
          int length = invocationList.Length;
          for (int index = 0; index < length; ++index)
          {
            try
            {
              DockControl dockControl = ((GetContentCallback) invocationList[index])(guid, data);
              if (dockControl != null)
                return dockControl;
            }
            catch (Exception ex)
            {
              outputView.WriteString("Восстановление", $"Ошибка : {ex.Message}");
              ExceptionHelper.ExceptionService.ShowException(ex);
            }
          }
        }
        return (DockControl) null;
      }

      private DockControl GetDockControl(Guid guid, string persistString, string text)
      {
        if (guid == ViewGuids.PluginView_Guid)
          return this.GetPluginView();
        if (guid == ViewGuids.AppPanel_Guid)
          return this._appPanel;
        if (guid == ViewGuids.ClipboardView_Guid)
          return this.GetClipboardView();
        if (guid == ViewGuids.BackgroundView_Guid)
          return this.GetBackgroundView();
        if (guid == ViewGuids.OutputView_Guid)
          return this.GetOutputView();
        if (guid == ViewGuids.ServerOutputView_Guid)
          return this.GetServerOutputView();
        if (guid == ViewGuids.ServerObjectsView_Guid)
          return this.GetServerObjectsView();
        if (guid == ViewGuids.NamedImagesView_Guid)
          return this.GetNamedImagesView();
        if (guid == ViewGuids.PropertyGridView_Guid)
          return this.GetPropertyView();
        if (guid == ViewGuids.DocumentOverview_Guid)
          return this.GetOverview();
        if (guid == ViewGuids.DocumentLayoutView_Guid)
          return this.GetDocumentLayout();
        if (guid == ViewGuids.UINotificationView_Guid)
          return this.GetErrorsView();
        switch (UISettings.RestoreDocumentWindows)
        {
          case DocumentRestoreMode.None:
            return (DockControl) null;
          case DocumentRestoreMode.CreateProxy:
            DockControl dockControl = (DockControl) new DockControlProxy(guid, text, persistString);
            dockControl.BeforeFirstShown += new EventHandler(this.Proxy_BeforeFirstShown);
            return dockControl;
          case DocumentRestoreMode.Restore:
            return this.RestoreDockControl(guid, persistString);
          default:
            return (DockControl) null;
        }
      }

      public event GetToolbarCallback ToolbarCallback;

      private Intermech.Bars.ToolBar GetToolbarByGuid(Guid guid)
      {
        if (this.ToolbarCallback != null)
        {
          foreach (GetToolbarCallback invocation in this.ToolbarCallback.GetInvocationList())
          {
            Intermech.Bars.ToolBar toolbar = invocation(guid);
            if (toolbar != null)
            {
              this._barManager.AddToolbar(toolbar);
              return toolbar;
            }
          }
        }
        return (Intermech.Bars.ToolBar) null;
      }

      private void MainForm_Load(object sender, EventArgs e)
      {
        string str1 = System.Configuration.ConfigurationManager.AppSettings["Caption"];
        if (str1 == null || str1.StartsWith("Intermech Professional Solutions"))
          str1 = "";
        if (str1 == "")
          str1 = "IPS 2022";
        this.Text = $"{str1} ({AssemblyAttributes.IPSVersion})";
        this.InitializeWaitCursor();
        this._mainMenu.Enabled = false;
        try
        {
          ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
          this._serverObjectsMenuButtonItem.Visible = service1.IsAdmin;
          this._serverOutputMenuButtonItem.Visible = service1.IsAdmin;
          SplashScreenService splashScreen = this._splashScreen;
          using (DockBarsSettings dockBarsSettings = new DockBarsSettings(this._serviceProvider))
            dockBarsSettings.ApplyChanges();
          this._pluginManager.LoadComplete += new EventHandler(this.PluginsLoadComplete);
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            sessionKeeper.Session.DBObjectsCacheStart();
            if (ServicesManager.GetService(typeof (ICreateObjByTypeMRU)) is ICreateObjByTypeMRU service2)
              service2.LoadMRU(sessionKeeper.Session.UserID);
            try
            {
              SessionPluginsLoader sessionPluginsLoader = this._initParams.CreateSessionPluginLoader();
              sessionPluginsLoader.DiagnosticReporter = (IMessageReporter) new MultilineMessageReporter((IMessageReporter) new OutputViewMessageReporter(this.GetOutputViewService(), LocalizationHolder.rm.GetString("IMClient_75")));
              sessionPluginsLoader.SplashService = (ISplashService) this._splashScreen;
              sessionPluginsLoader.LoadPlugins((IPluginManager) this._pluginManager);
              PersonalPluginsLoader personalPluginsLoader = this._initParams.CreatePersonalPluginLoader();
              personalPluginsLoader.DiagnosticReporter = (IMessageReporter) new MultilineMessageReporter((IMessageReporter) new OutputViewMessageReporter(this.GetOutputViewService(), LocalizationHolder.rm.GetString("IMClient_75")));
              personalPluginsLoader.LoadPlugins((IPluginManager) this._pluginManager);
              this._pluginManager.LoadConfiguration();
              this._pluginManager.FinishAutoLoad();
            }
            finally
            {
              sessionKeeper.Session.DBObjectsCacheStop();
            }
            this._projectsFiltration = new ProjectsDropDownControl(this, this.projectsList, (Image) null, 0L);
            ServicesManager.AddService(typeof (ProjectsDropDownControl), (object) this._projectsFiltration);
            this._editingContextsFiltration = new ContextFiltrationPanelControl((IContextFiltrationPanel) this, this.contextsList, (Image) null, (IList<long>) null, 0L);
            ServicesManager.AddService(typeof (ContextFiltrationPanelControl), (object) this._editingContextsFiltration);
            if (!this.ContainsCmd("-norestore"))
            {
              this._barManager.LoadConfiguration((BarManagerConfigurationStorage) new BarManagerConfigurationAdapter(this._configurationManager), new GetToolbarCallback(this.GetToolbarByGuid));
              this._barManager.MenuBar.Visible = true;
            }
            IFactory service3 = ServicesManager.GetService(typeof (IFactory)) as IFactory;
            CoreConsts.FilterRecords = service1 == null || !service1.IsAdmin;
            IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
            this._adjustableContextMenus.Assign(AdjustableMenusHelper.BuildFromMenuTemplate(service3.ContextMenuTemplate));
            this._adjustableContextMenus.SyncWithRoleSettings(sessionKeeper.Session.RoleID);
            if (!service1.BlockedMenus)
              this._adjustableContextMenus.SyncWithUserSettings(sessionKeeper.Session.UserID);
            service3.ConfiguredContextMenuTemplate = AdjustableMenusHelper.BuildMenuTemplate(this._adjustableContextMenus);
            AdjustableViewsService.RegisterNavigatorViews();
            this._adjustableViews.SyncWithRoleSettings(sessionKeeper.Session.RoleID);
            if (!service1.BlockedViews)
              this._adjustableViews.SyncWithUserSettings(sessionKeeper.Session.UserID);
            AdjustableViewsHelper.ProcessViews((List<AdjustableView>) this._adjustableViews);
            this._otNodeFilters.Load(sessionKeeper.Session.UserID);
            this._recentObjects = new RecentObjectsSettingsViewPage((System.IServiceProvider) ServicesManager.ServiceContainer);
            RedliningViewPage redliningViewPage = new RedliningViewPage((System.IServiceProvider) ServicesManager.ServiceContainer);
            long int64Value1 = DataSetProcessor.GetInt64Value(customService[sessionKeeper.Session.UserID, (object) this._lastProjectGuid], 0L);
            ProjectFiltrationModes int32Value = (ProjectFiltrationModes) DataSetProcessor.GetInt32Value(customService[sessionKeeper.Session.UserID, (object) this._lastProjectModeGuid], 0);
            if (int64Value1 != 0L)
              service1.SetCurrentProject(int64Value1, int32Value, true);
            this._editingContextsFiltration.Load((IList<long>) (customService[sessionKeeper.Session.UserID, (object) this._lastEditingContextsHistryGuid] as List<long>), 0L);
            if (service1.EditingContextSource == EditingContextSource.SessionContext)
            {
              long int64Value2 = DataSetProcessor.GetInt64Value(customService[sessionKeeper.Session.UserID, (object) this._lastEditingContextGuid], 0L);
              EditingContextMode editingContextMode = (EditingContextMode) Convert.ToInt32(customService[sessionKeeper.Session.UserID, (object) this._lastEditingContextModeGuid]);
              string str2 = string.Empty;
              if (int64Value2 != 0L)
              {
                if (!service1.IsContextToolbarVisible)
                  editingContextMode = EditingContextMode.Default;
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64Value2);
                if (!objectInfo.Empty)
                {
                  str2 = objectInfo.Caption;
                  if (editingContextMode != EditingContextMode.AutoUpdate || !service1.CanLeaveContextAutoUpdateMode(int64Value2))
                    ;
                  service1.EditingContextID = int64Value2;
                  service1.ReplaceEditingContext(new CurrentEditingContext(service1.CachedEditingContextID, service1.CachedEditingContextModificationID, EditingContextMode.Default));
                }
              }
              else
                service1.ReplaceEditingContext(CurrentEditingContext.Empty);
              if (ServiceLocator.Get<IConfigurationOptionRepository>().Find(EditingContextsConfigurationOptionKyes.ShowEditingContextAutoRefillDialogOnClientEnter) is bool flag && flag && service1.CachedContextMode != EditingContextMode.AutoUpdate && service1.CachedEditingContextID != 0L && IMMessageBox.Show("Вопрос", "Включить режим автоматического пополнения текущего контекста редактирования\n" + string.Format("[{1}] \"{0}\" ?", (object) str2, (object) service1.CachedEditingContextID), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
              {
                service1.SilentMode = false;
                service1.EditingContextMode = EditingContextMode.AutoUpdate;
              }
            }
            if (ServicesManager.GetService(typeof (INavGraphicsCache)) is INavGraphicsCache service4)
              service4.LoadUserColorsScheme(sessionKeeper.Session.UserID);
          }
          this._dockManager.IntegralClose = true;
          ServicesManager.AddService(typeof (IExtensionsService), (object) new ExtensionsService());
          if (!this.ContainsCmd("-norestore"))
            this._dockManager.LoadConfiguration((DockManagerConfigurationStorage) new DockManagerConfigurationAdapter(this._configurationManager), new DockManager.GetDockControlCallback(this.GetDockControl));
          this.LoadAddressAutocomplete(this._configurationManager);
          DocumentNavigator documentNavigator = new DocumentNavigator(this.btDocumentBack, this._previousWindowMenuButtonItem, this.btDocumentForward, this._nextWindowMenuButtonItem, this._docContainer);
          ServicesManager.AddService(typeof (IUndoService), (object) new UndoService(this.btUndo, this.btRedo, this._docContainer));
          this._filtration.FiltrationUpdate(false);
          new ButtonBarsManager().ResetButtonBars();
          ServicesManager.AddService(typeof (AfterCreateRoleActions), (object) new AfterCreateRoleActions());
          ServicesManager.AddService(typeof (AfterCreateContextActions), (object) new AfterCreateContextActions());
          this.CheckToolbarsBlocking();
          FileExtensionsViewPage extensionsViewPage = new FileExtensionsViewPage();
          BlackWidthViewPage blackWidthViewPage = new BlackWidthViewPage((System.IServiceProvider) ServicesManager.ServiceContainer);
          RedPropertyViewPage propertyViewPage = new RedPropertyViewPage((System.IServiceProvider) ServicesManager.ServiceContainer);
          AuthFilesPropertyPage filesPropertyPage = new AuthFilesPropertyPage((System.IServiceProvider) ServicesManager.ServiceContainer);
          FilesComparisonSettingsViewPage settingsViewPage = new FilesComparisonSettingsViewPage();
          if (this._dockManager.DocumentContainer != null)
          {
            foreach (DockControl document in this._dockManager.DocumentContainer.Documents)
            {
              if (document.IsOpen)
              {
                document.Activate();
                break;
              }
            }
          }
        }
        finally
        {
          this._mainMenu.Enabled = true;
          (ServicesManager.GetService(typeof (IMainMenuService)) as IMainMenuService).ResumeRebuildMainMenu();
          if (ServicesManager.GetService(typeof (ISplashService)) is ISplashService service)
          {
            service.CloseSplash();
            ServicesManager.RemoveService(typeof (ISplashService));
            Application.DoEvents();
            Thread.Sleep(500);
            Application.DoEvents();
            if (this._splashScreen != null)
            {
              this._splashScreen.Close();
              this.RemoveOwnedForm((Form) this._splashScreen);
              this._splashScreen.Dispose();
            }
            this._splashScreen = (SplashScreenService) null;
          }
        }
        if (UISettings.FirstTimeRunning && ServicesManager.GetService(typeof (IWellKnownWindowsOpenService)) is IWellKnownWindowsOpenService service5)
        {
          service5.OpenWellKnownWindow("desktopNavigator");
          service5.OpenWellKnownWindow("mainNavigator");
        }
        NotificationSelectionsClientService serviceInstance = new NotificationSelectionsClientService();
        ServicesManager.AddService(typeof (INotificationSelectionsClientService), (object) serviceInstance);
        serviceInstance.StartNotificationSelections();
        if (!(ServicesManager.GetService(typeof (IUserSessionPool)) is IUserSessionPool service6))
          return;
        service6.MainSessionCreated += new EventHandler<UserSessionCreatedEventArgs>(this.SynchronizeVersionRulesCache);
      }

      private void PluginsLoadComplete(object sender, EventArgs e)
      {
        if (ServicesManager.GetService(typeof (StatusesInfoService)) is StatusesInfoService)
          return;
        ServicesManager.AddService(typeof (StatusesInfoService), (object) new StatusesInfoService());
      }

      private bool ContainsCmd(string value)
      {
        if (string.IsNullOrEmpty(value))
          return false;
        string[] commandLineArgs = Environment.GetCommandLineArgs();
        if (commandLineArgs == null)
          return false;
        int length = commandLineArgs.Length;
        for (int index = 0; index < length; ++index)
        {
          if (value.Equals(commandLineArgs[index], StringComparison.InvariantCultureIgnoreCase))
            return true;
        }
        return false;
      }

      private void InitializeWaitCursor()
      {
        AutoWaitCursor.Cursor = Cursors.WaitCursor;
        AutoWaitCursor.Delay = new TimeSpan(0, 0, 0, 0, 250);
        AutoWaitCursor.MainWindowHandle = this.Handle;
        AutoWaitCursor.Start();
      }

      private void MainForm_Closed(object sender, EventArgs e)
      {
        this._dockManager.SaveConfiguration((DockManagerConfigurationStorage) new DockManagerConfigurationAdapter(this._configurationManager));
        this._barManager.SaveConfiguration((BarManagerConfigurationStorage) new BarManagerConfigurationAdapter(this._configurationManager));
        this.SaveAddressAutocomplete(this._configurationManager);
        if (this._pluginManager != null)
          this._pluginManager.SaveConfiguration();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          bool etalonBase = sessionKeeper.Session.EtalonBase;
          IFactory service1 = ServicesManager.GetService(typeof (IFactory)) as IFactory;
          ICurrentUserAndRole service2 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
          IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
          if (etalonBase)
          {
            this._adjustableContextMenus.Assign(AdjustableMenusHelper.BuildFromMenuTemplate(service1.ContextMenuTemplate));
            this._adjustableContextMenus.SaveToUserSettings(sessionKeeper.Session.UserID);
            this._adjustableContextMenus.SaveToRoleSettings(sessionKeeper.Session.RoleID);
          }
          if (!etalonBase)
          {
            if (customService != null)
            {
              this._adjustableContextMenus.Assign(AdjustableMenusHelper.BuildFromMenuTemplate(service1.ConfiguredContextMenuTemplate));
              customService.SaveFiltrationTuning((object) sessionKeeper.Session.SessionGUID);
              customService.SaveRuleVars((object) sessionKeeper.Session.SessionGUID);
              customService.ResetDateTime((object) sessionKeeper.Session.SessionGUID);
              if (!service2.BlockedMenus)
                this._adjustableContextMenus.SaveToUserSettings(sessionKeeper.Session.UserID);
              if (!service2.BlockedViews)
                this._adjustableViews.SaveToUserSettings(sessionKeeper.Session.UserID);
              if (ServicesManager.GetService(typeof (ICreateObjByTypeMRU)) is ICreateObjByTypeMRU service3)
                service3.SaveMRU(sessionKeeper.Session.UserID);
              this._otNodeFilters.Save(sessionKeeper.Session.UserID);
              customService[sessionKeeper.Session.UserID, (object) this._lastProjectGuid] = (object) service2.ProjectID;
              customService[sessionKeeper.Session.UserID, (object) this._lastProjectModeGuid] = (object) service2.ProjectFiltrationMode;
              if (this._editingContextsFiltration.History.Count > this._editingContextsFiltration.HistoryLimit)
                this._editingContextsFiltration.History.RemoveRange(this._editingContextsFiltration.HistoryLimit, this._editingContextsFiltration.History.Count - this._editingContextsFiltration.HistoryLimit);
              if (this._editingContextsFiltration.History.Count > 0)
                customService[sessionKeeper.Session.UserID, (object) this._lastEditingContextsHistryGuid] = (object) this._editingContextsFiltration.History;
              customService[sessionKeeper.Session.UserID, (object) this._lastEditingContextGuid] = (object) service2.EditingContextID;
              customService[sessionKeeper.Session.UserID, (object) this._lastEditingContextModeGuid] = (object) service2.EditingContextMode;
              customService.SaveUserSettings((object) sessionKeeper.Session.SessionGUID);
              if (ServicesManager.GetService(typeof (INavigatorColumnsService)) is INavigatorColumnsService service4)
                service4.SaveToUserConfig();
            }
          }
        }
        this.RaiseApplicationClosed();
      }

      private void RaiseApplicationClosed()
      {
        NotificationEventArgs e = new NotificationEventArgs("ApplicationClosed", false);
        ((INotificationService) ApplicationServices.Container.GetService(typeof (INotificationService))).FireEvent((object) this, e);
      }

      private void TaskBarMenuButtonItem_Click(object sender, EventArgs e)
      {
        this._appPanel.Show(this._dockManager);
      }

      private void DisableActiveTargerTimer_Tick(object sender, EventArgs e)
      {
        this._disableActiveTargetTimer.Enabled = false;
        this._commandManager.ActiveTarget = (ICommandTarget) null;
      }

      private void DockManager_DockControlDeactivated(object sender, DockControlEventArgs e)
      {
        this._disableActiveTargetTimer.Enabled = true;
      }

      private void DockManager_DockControlActivated(object sender, DockControlEventArgs e)
      {
        this._disableActiveTargetTimer.Enabled = false;
        DockControl dockControl = e.DockControl;
        if (dockControl is ISkipTargetActivate)
          return;
        this._commandManager.ActiveTarget = dockControl as ICommandTarget;
        INavigate navigate = dockControl as INavigate;
        System.IServiceProvider serviceProvider = dockControl as System.IServiceProvider;
        if (navigate == null && serviceProvider != null)
          navigate = (INavigate) serviceProvider.GetService(typeof (INavigate));
        this._navigateManager.Attach(navigate);
      }

      private void LoadPluginMenuButtonItem_Click(object sender, EventArgs e)
      {
        using (SelectPluginsForm selectPluginsForm = new SelectPluginsForm())
        {
          selectPluginsForm.InitForm();
          if (selectPluginsForm.ShowDialog() != DialogResult.OK)
            return;
          foreach (string selectedDll in selectPluginsForm.SelectedDlls)
          {
            try
            {
              if (File.Exists(selectedDll))
                this._pluginManager.Load(selectedDll, true);
            }
            catch (Exception ex)
            {
              ExceptionHelper.ExceptionService.ShowException(ex);
            }
          }
          foreach (string selectedLoadModule in selectPluginsForm.SelectedLoadModules)
          {
            try
            {
              string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, selectedLoadModule);
              if (File.Exists(str))
                this._pluginManager.Load(str, true);
            }
            catch (Exception ex)
            {
              ExceptionHelper.ExceptionService.ShowException(ex);
            }
          }
        }
      }

      private DockControl GetPluginView()
      {
        if (this._pluginView == null)
          this._pluginView = (DockControl) new PluginView();
        return this._pluginView;
      }

      private DockControl GetBackgroundView()
      {
        if (this._backgroundView == null)
          this._backgroundView = (DockControl) new BackgroundView(this._serviceProvider);
        return this._backgroundView;
      }

      private DockControl GetOutputView()
      {
        if (this._outputView == null)
        {
          OutputView outputView = new OutputView();
          outputView.Initialize();
          this._outputView = (DockControl) outputView;
        }
        return this._outputView;
      }

      internal IOutputView GetOutputViewService() => (IOutputView) this.GetOutputView();

      private DockControl GetErrorsView()
      {
        if (this._errorsView == null)
          this._errorsView = (DockControl) new UINotificationView();
        return this._errorsView;
      }

      private DockControl GetServerObjectsView()
      {
        if (this._serverObjects == null)
          this._serverObjects = (DockControl) new ServerObjectsView(this._serviceProvider);
        return this._serverObjects;
      }

      private DockControl GetServerOutputView()
      {
        if (this._serverOutputView == null)
          this._serverOutputView = (DockControl) new ServerOutputView(this._serviceProvider);
        return this._serverOutputView;
      }

      private DockControl GetOverview()
      {
        if (this._overview == null)
          this._overview = (DockControl) new DocumentOverview(this._serviceProvider);
        return this._overview;
      }

      private DockControl GetDocumentLayout()
      {
        if (this._documentLayout == null)
          this._documentLayout = (DockControl) new DocumentLayoutView(this._serviceProvider);
        return this._documentLayout;
      }

      private DockControl GetClipboardView()
      {
        if (this._clipboardView == null)
          this._clipboardView = (DockControl) new ClipboardView(this._serviceProvider);
        return this._clipboardView;
      }

      private DockControl GetNamedImagesView()
      {
        if (this._namedImagesView == null)
          this._namedImagesView = (DockControl) new NamedImagesView(this._serviceProvider);
        return this._namedImagesView;
      }

      private DockControl GetPropertyView()
      {
        if (this._propertyView == null)
          this._propertyView = (DockControl) new PropertyGridView();
        return this._propertyView;
      }

      internal BarManager GetBarManagerService() => this._barManager;

      internal DockManager GetDockManagerService() => this._dockManager;

      private void PluginsListMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetPluginView().Show(this._dockManager);
      }

      internal static bool RunningOnXP()
      {
        bool flag = false;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
          flag = Environment.OSVersion.Version >= new Version(5, 1, 0, 0);
        return flag;
      }

      private void MainForm_Activated(object sender, EventArgs e)
      {
      }

      private void MainForm_Deactivate(object sender, EventArgs e)
      {
      }

      private void ExitMenuButtonItem_Click(object sender, EventArgs e) => this.Close();

      private void Application_Idle(object sender, EventArgs e)
      {
        Application.Idle -= new EventHandler(this.Application_Idle);
        this._commandManager.QueryStatus();
      }

      private void SettingsMenuButtonItem_Click(object sender, EventArgs e)
      {
        this._propertyPages.ShowDialog();
      }

      private void NamedImageListMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetNamedImagesView().Show(this._dockManager);
      }

      private void mnPropertyView_Click(object sender, EventArgs e)
      {
        this.GetPropertyView().Show(this._dockManager);
      }

      private void ClipboardMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetClipboardView().Show(this._dockManager);
      }

      private void BackgroundTasksMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetBackgroundView().Show(this._dockManager);
      }

      private void OutputMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetOutputView().Show(this._dockManager);
      }

      private void _errormenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetErrorsView().Show(this._dockManager);
      }

      private void ServerObjectsMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetServerObjectsView().Show(this._dockManager);
      }

      private void MainForm_Closing(object sender, FormClosingEventArgs e)
      {
        if (!this._mainMenu.Enabled)
        {
          e.Cancel = true;
        }
        else
        {
          Statics.IsApplicationClosing = true;
          try
          {
            INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
            ApplicationClosingEventArgs e1 = new ApplicationClosingEventArgs("ApplicationClosing", false);
            service?.FireEvent((object) this, (NotificationEventArgs) e1);
            if (e1.Cancel)
            {
              e.Cancel = true;
            }
            else
            {
              if (UISettings.AskOnExit && e.CloseReason != CloseReason.WindowsShutDown)
              {
                if (MessageBox.Show(LocalizationHolder.rm.GetString("IMClient_49"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                  e.Cancel = false;
                }
                else
                {
                  e.Cancel = true;
                  return;
                }
              }
              if ((IBackgroundTaskView) this._backgroundView != null && !((IBackgroundTaskView) this._backgroundView).CheckClosing())
                e.Cancel = true;
              if (!e.Cancel && !this._docContainer.CheckCloseDocuments())
                e.Cancel = true;
              if (e.Cancel || this.leftDock.Visible)
                return;
              this.FullScreenMenuButtonItem_Click((object) null, (EventArgs) null);
            }
          }
          catch
          {
            Statics.IsApplicationClosing = false;
            throw;
          }
          finally
          {
            if (e.Cancel)
              Statics.IsApplicationClosing = false;
          }
        }
      }

      private void Proxy_BeforeFirstShown(object sender, EventArgs e)
      {
        if (!(sender is DockControl dockControl))
          return;
        DockControl target = (DockControl) null;
        try
        {
          target = this.RestoreDockControl(dockControl.Guid, dockControl.PersistString);
        }
        catch
        {
        }
        if (dockControl.LayoutSystem == null)
          return;
        if (target != null)
        {
          dockControl.ReplaceTo(target);
          target.Invalidate();
        }
        else
        {
          if (this._outputView is IOutputView outputView)
          {
            string text = string.Format(LocalizationHolder.rm.GetString("IMClient_50"), (object) Environment.NewLine, (object) dockControl.Guid, (object) dockControl.PersistString);
            outputView.WriteString(LocalizationHolder.rm.GetString("IMClient_51"), text);
          }
          dockControl.Close();
        }
      }

      private void MainForm_VisibleChanged(object sender, EventArgs e)
      {
      }

      private void Documents_ShowContextMenu(object sender, ShowControlContextMenuEventArgs e)
      {
        DockControl dockControl = e.DockControl;
        Point position = e.Position;
        if (dockControl == null || dockControl.DockLocation != DockLocation.Document)
          return;
        this.mnContextSaveDocument.Enabled = false;
        this.mnContextSaveDocument.Visible = false;
        this.mnCloseDocument.Enabled = dockControl.Closable;
        bool flag = dockControl.LayoutSystem.Controls.Count > 1;
        this.mnNewHorizontalGroup.Enabled = flag;
        this.mnNewVerticalGroup.Enabled = flag;
        this.mnCloseAllBut.Enabled = flag;
        this.mnOpenNewInstanceAsObject.Visible = false;
        if (dockControl is IOpenAsObjectSupport openAsObjectSupport1)
        {
          this.mnOpenNewInstanceAsObject.Visible = true;
          this.mnOpenNewInstanceAsObject.Enabled = openAsObjectSupport1.CanBeOpenedInNewWindowsAsObject;
        }
        MenuButtonItem menuButtonItem = this.docTabContextMenu.Show((Control) dockControl, e.Position);
        if (menuButtonItem == null)
          return;
        switch (menuButtonItem.CommandName)
        {
          case "Close":
            dockControl.Close();
            break;
          case "CloseAllBut":
            if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("IMClient_99")), string.Format(LocalizationHolder.rm.GetString("IMClient_100")), MessageBoxButtons.OKCancel) == DialogResult.Cancel)
              break;
            foreach (DockControl document in this._docContainer.Documents)
            {
              if (dockControl != document && document.Closable)
                document.Close();
            }
            break;
          case "OpenNewInstanceAsObject":
            if (!(dockControl is IOpenAsObjectSupport openAsObjectSupport2))
              break;
            openAsObjectSupport2.OpenNewInstanceAsObject();
            break;
          case "Float":
            dockControl.Float();
            break;
        }
      }

      private void documentContainer1_DocumentListClick(object sender, EventArgs e)
      {
        if (!(sender is DocumentContainer))
          return;
        Point mousePosition = Control.MousePosition;
      }

      private void ClearDocumentList()
      {
        foreach (MenuButtonItem documentMenuItem in this._documentMenuItems)
        {
          documentMenuItem.Click -= new EventHandler(this.DocumentItem_Click);
          documentMenuItem.Dispose();
        }
        this._documentMenuItems.Clear();
      }

      private void mnWindows_BeforePopup(object sender, MenuPopupEventArgs e)
      {
        this.ClearDocumentList();
        DockControl[] documents = this._docContainer.Documents;
        this._closeAllWindowsMenuButtonItem.Enabled = documents.Length != 0;
        DocumentComparer documentComparer = new DocumentComparer();
        Array.Sort((Array) documents, (IComparer) documentComparer);
        DockControl activeDocument = this._docContainer.ActiveDocument;
        int num = 0;
        MenuBarItem menuBarItem = sender as MenuBarItem;
        foreach (DockControl dockControl in documents)
        {
          MenuButtonItem menuButtonItem = new MenuButtonItem(documentComparer.RemoveSign(dockControl.Text));
          menuButtonItem.Click += new EventHandler(this.DocumentItem_Click);
          if (activeDocument == dockControl)
            menuButtonItem.Checked = true;
          if (dockControl.TabImage != null)
            menuButtonItem.Image = (Image) dockControl.TabImage.Clone();
          else if (dockControl.TabImageIndex != -1)
            menuButtonItem.ImageIndex = dockControl.TabImageIndex;
          menuButtonItem.Tag = (object) dockControl;
          if (num == 0)
            menuButtonItem.BeginGroup = true;
          if (num++ > 10 && !menuButtonItem.Checked)
            menuButtonItem.Importance = ToolBarItemImportance.Low;
          menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem);
          this._documentMenuItems.Add((object) menuButtonItem);
        }
      }

      private void DocumentItem_Click(object sender, EventArgs e)
      {
        MenuButtonItem menuButtonItem = sender as MenuButtonItem;
        if (sender == null || menuButtonItem == null || menuButtonItem.Checked)
          return;
        DockControl tag = (DockControl) menuButtonItem.Tag;
        if (tag == null)
          return;
        this._docContainer.ActiveDocument = tag;
      }

      private void DocContainer_DocumentListClick(object sender, DocumentListEventArgs e)
      {
        DockControl[] documents = e.Documents;
        DocumentComparer documentComparer = new DocumentComparer();
        Array.Sort((Array) documents, (IComparer) documentComparer);
        DockControl activeDocument = this._docContainer.ActiveDocument;
        int num1 = 0;
        MenuBarItem menuBarItem = new MenuBarItem();
        foreach (DockControl dockControl in documents)
        {
          MenuButtonItem menuButtonItem = new MenuButtonItem(documentComparer.RemoveSign(dockControl.Text));
          if (activeDocument == dockControl)
            menuButtonItem.Checked = true;
          if (dockControl.TabImage != null)
            menuButtonItem.Image = (Image) dockControl.TabImage.Clone();
          if (dockControl.TabImageIndex != -1)
            menuButtonItem.ImageIndex = dockControl.TabImageIndex;
          menuButtonItem.Tag = (object) dockControl;
          if (num1++ > 10)
          {
            int num2 = menuButtonItem.Checked ? 1 : 0;
          }
          menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem);
        }
        if (menuBarItem.HasChildren)
        {
          Point client = this._docContainer.PointToClient(Control.MousePosition);
          bool fullMenus = this._mainMenu.FullMenus;
          MenuButtonItem menuButtonItem = (MenuButtonItem) null;
          try
          {
            menuButtonItem = menuBarItem.Show((IPopupMenuHost) this._mainMenu, (Control) this._docContainer, client);
          }
          finally
          {
            this._mainMenu.FullMenus = fullMenus;
          }
          if (menuButtonItem != null && !menuButtonItem.Checked)
          {
            DockControl tag = (DockControl) menuButtonItem.Tag;
            if (tag != null)
              this._docContainer.ActiveDocument = tag;
          }
        }
        menuBarItem.Dispose();
      }

      private void CloseAllWindowsMenuButtonItem_Click(object sender, EventArgs e)
      {
        foreach (DockControl document in this._docContainer.Documents)
        {
          if (document.Closable)
            document.Close();
        }
      }

      private void AddressComboBox_KeyPress(object sender, KeyPressEventArgs e)
      {
        if (e.KeyChar != '\r' || !this._gotoAddressState.Enabled)
          return;
        this._commandManager.Execute(this._gotoAddressState);
      }

      private void MainMenu_ButtonClick(object sender, ToolBarItemEventArgs e)
      {
        if (!(e.Item is MenuButtonItem menuButtonItem))
          return;
        menuButtonItem.IncreaseImportance();
      }

      private void _dockManager_DockingStarted(object sender, EventArgs e)
      {
        this.StatusBar_ShowPanels = this._statusBar.ShowPanels;
        this._statusBar.ShowPanels = false;
        this._statusBar.Text = "Hold down CTRL to prevent docking. Point to title bar of destination window to tab link.";
      }

      private void _docContainer_DockingStarted(object sender, EventArgs e)
      {
        this.StatusBar_ShowPanels = this._statusBar.ShowPanels;
        this._statusBar.ShowPanels = false;
        this._statusBar.Text = "Drag document to sides of existing document to split window.";
      }

      private void _dockManager_DockingFinished(object sender, EventArgs e)
      {
        this._statusBar.ShowPanels = this.StatusBar_ShowPanels;
        this._statusBar.Text = LocalizationHolder.rm.GetString("IMClient_52");
      }

      private void _docContainer_DockingFinished(object sender, EventArgs e)
      {
        this._statusBar.ShowPanels = this.StatusBar_ShowPanels;
        this._statusBar.Text = LocalizationHolder.rm.GetString("IMClient_53");
      }

      private void ShortcutListener_SecondaryShortcutAction(object sender, SecondaryShortcutEventArgs e)
      {
        if (e.SecondaryShortcut == Keys.None)
        {
          this.StatusBar_ShowPanels = this._statusBar.ShowPanels;
          this._statusBar.ShowPanels = false;
          this._statusBar.Text = "First key in shortcut chord pressed. Waiting for second...";
        }
        else if (e.Item == null)
          this._statusBar.Text = "Unrecognised key combination activated.";
        else
          this._statusBar.Text = LocalizationHolder.rm.GetString("IMClient_54");
      }

      private void ServerOutputMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetServerOutputView().Show(this._dockManager);
      }

      private void SaveAddressAutocomplete(IConfigurationManager configurationManager)
      {
        IConfiguration configuration = configurationManager.Create("AddressHistory");
        int count = this._acsc.Count;
        int maximumAddresses = MainForm.MainFormConsts.MaximumAddresses;
        for (int index = count - 1; index >= 0; --index)
        {
          configuration.SetProperty("L" + index.ToString(), this._acsc[index]);
          if (--maximumAddresses == 0)
            break;
        }
      }

      private void LoadAddressAutocomplete(IConfigurationManager configurationManager)
      {
        this.cbAddress.Items.Clear();
        this._acsc.Clear();
        IConfiguration configuration = configurationManager.Open("AddressHistory");
        if (configuration != null)
        {
          int maximumAddresses = MainForm.MainFormConsts.MaximumAddresses;
          foreach (IConfigurationProperty property in configuration.Properties)
          {
            this.AddNewAddress(configuration.GetProperty(property.Name));
            if (--maximumAddresses == 0)
              break;
          }
        }
        this.cbAddress.ComboBox.Sorted = true;
      }

      private void AddNewAddress(string value)
      {
        if (!this.cbAddress.Items.Contains((object) value))
          this.cbAddress.Items.Add((object) value);
        if (this._acsc.Contains(value))
          return;
        this._acsc.Add(value);
      }

      string IAddressService.Text
      {
        get => this.cbAddress.ComboBox.Text;
        set
        {
          if (this.cbAddress.Enabled)
            this.AddNewAddress(value);
          this.cbAddress.ComboBox.Text = value;
        }
      }

      string[] IAddressService.History
      {
        get
        {
          int count = this.cbAddress.Items.Count;
          string[] history = new string[count];
          for (int index = 0; index < count; ++index)
            history[index] = Convert.ToString(this.cbAddress.Items[index]);
          return history;
        }
        set
        {
          this.cbAddress.Items.Clear();
          if (value == null)
            return;
          this.cbAddress.Items.AddRange((object[]) value);
        }
      }

      bool IAddressService.Enabled
      {
        get => this.cbAddress.Enabled;
        set
        {
          this.cbAddress.Enabled = value;
          this.btGotoAddress.Enabled = value;
        }
      }

      StatusBar IStatusBar.StatusBar => this._statusBar;

      private void ShowLoggedUserInfo()
      {
        IUserSessionLoginInfo loginInfo = ((IUserSessionLoginService) ServicesManager.GetService(typeof (IUserSessionLoginService))).GetLoginInfo();
        this.sbpRole.Text = " " + loginInfo.RoleName;
        this.sbpUserName.Text = " " + loginInfo.UserName;
        this.sbpIOUserName.Text = loginInfo.ActingUserName;
        this.sbpIOUserName.ToolTipText = string.IsNullOrEmpty(loginInfo.ActingUserName) ? string.Empty : LocalizationHolder.rm.GetString("IOUserName");
      }

      private void FullScreenMenuButtonItem_Click(object sender, EventArgs e)
      {
        try
        {
          this.SuspendLayout();
          bool flag = !this.leftDock.Visible;
          if (!flag)
          {
            this._hidedToolbars.Clear();
            foreach (Intermech.Bars.ToolBar control in (ArrangedElementCollection) this.topBarDock.Controls)
            {
              if (control != this._mainMenu && control.Visible)
                this._hidedToolbars.Add((object) control);
            }
          }
          this.leftDock.Visible = flag;
          this.rightDock.Visible = flag;
          this.topDock.Visible = flag;
          this.bottomDock.Visible = flag;
          this.leftBarDock.Visible = flag;
          this.rightBarDock.Visible = flag;
          this.bottomBarDock.Visible = flag;
          this._statusBar.Visible = flag;
          if (flag)
          {
            foreach (Control hidedToolbar in this._hidedToolbars)
              hidedToolbar.Visible = true;
            this._hidedToolbars.Clear();
            this.WindowState = this._oldState;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            UISettings.FullScreenMode = false;
          }
          else
          {
            foreach (Intermech.Bars.ToolBar hidedToolbar in this._hidedToolbars)
            {
              if (hidedToolbar.Visible)
                hidedToolbar.Visible = false;
            }
            this._oldState = this.WindowState;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            UISettings.FullScreenMode = true;
          }
        }
        finally
        {
          this.ResumeLayout();
        }
      }

      private void OverviewMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetOverview().Show(this._dockManager);
      }

      private void DocumentLayoutMenuButtonItem_Click(object sender, EventArgs e)
      {
        this.GetDocumentLayout().Show(this._dockManager);
      }

      private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
      {
      }

      private void MainForm_Shown(object sender, EventArgs e)
      {
        if (this._splashScreen != null && this._splashScreen.Visible)
          this._splashScreen.BringToFront();
        StartupService startupService = this.GetStartupService();
        startupService.RaiseMainFormShown();
        try
        {
          DockControl activeDocument = this._dockManager.ActiveDocument;
          foreach (DockControl document in this._docContainer.Documents)
          {
            if (document.IsOpen && document is DockControlProxy)
              document.Activate();
          }
          activeDocument?.Activate();
        }
        catch
        {
        }
        startupService.RaiseStartupComplete();
      }

      private StartupService GetStartupService()
      {
        return (StartupService) ApplicationServices.Container.GetService(typeof (StartupService));
      }

      public bool Execute(ICommandState commandState)
      {
        switch (commandState.CommandName)
        {
          case "Overview":
            this.OverviewMenuButtonItem_Click((object) null, EventArgs.Empty);
            return true;
          case "DocLayout":
            this.DocumentLayoutMenuButtonItem_Click((object) null, EventArgs.Empty);
            return true;
          default:
            return false;
        }
      }

      public bool QueryStatus(ICommandState commandState)
      {
        if (!(commandState.CommandName == "Overview") && !(commandState.CommandName == "DocLayout"))
          return false;
        commandState.Enabled = true;
        return true;
      }

      internal bool ExecuteMenuCommand(List<IHotKeysCommand> commands, IIOEvent ioEvent)
      {
        if (commands == null || commands.Count == 0 || ioEvent == null || ioEvent.Source.SelectedItems == null)
          return false;
        CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(ioEvent.Source.SelectedItems, ioEvent.Source.Services, false);
        string commandName = string.Empty;
        for (int index = 0; index < commands.Count; ++index)
        {
          if (commandsTable.Contains(commands[index].Command))
          {
            commandName = commands[index].Command;
            break;
          }
        }
        if (commandName == string.Empty)
          return false;
        Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, ioEvent.Source.Services);
        return true;
      }

      internal bool ExecuteMenuCommand(string command, IIOEvent ioEvent)
      {
        if (command == string.Empty || ioEvent == null || ioEvent.Source.SelectedItems == null)
          return false;
        CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(ioEvent.Source.SelectedItems, ioEvent.Source.Services, false);
        if (!commandsTable.Contains(command))
          return false;
        Intermech.Navigator.ContextMenu.Services.InvokeCommand(command, commandsTable, ioEvent.Source.Services);
        return true;
      }

      internal bool ExecuteMenuCommand(IDefaultCommand command, IIOEvent ioEvent)
      {
        return command != null && ioEvent != null && ioEvent.Source.SelectedItems != null && this.ExecuteMenuCommand(command.DefaultCommandName, ioEvent);
      }

      public IOEventTypes SupportedEvents
      {
        get => IOEventTypes.evKeyDown | IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
        set
        {
        }
      }

      private IDefaultCommands4ObjTypes DefaultCommands4ObjTypes
      {
        get
        {
          if (this._defaultCommands4ObjTypes == null)
            this._defaultCommands4ObjTypes = ServicesManager.GetService(typeof (IDefaultCommands4ObjTypes)) as IDefaultCommands4ObjTypes;
          return this._defaultCommands4ObjTypes;
        }
      }

      bool IIODestination.ProcessEvent(IIOEvent Event)
      {
        if (Event == null)
          return false;
        if ((Event.EventType == IOEventType.evKeyUp || Event.EventType == IOEventType.evKeyDown) && this._hotKeysManager != null)
        {
          List<IHotKeysCommand> commands = this._hotKeysManager[((KeyEventArgs) Event.EventData).KeyCode | ((KeyEventArgs) Event.EventData).Modifiers];
          if (commands != null && commands.Count > 0)
          {
            ((KeyEventArgs) Event.EventData).Handled = true;
            return this.ExecuteMenuCommand(commands, Event);
          }
        }
        if (Event.EventType != IOEventType.evMouseDoubleClick && (Event.EventType != IOEventType.evKeyUp || ((KeyEventArgs) Event.EventData).KeyCode != Keys.Return) || Event.Source.SelectedItems == null || Event.Source.SelectedItems.Count <= 0 || !(Event.Source.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
          return false;
        IDefaultCommand commands4ObjType = this.DefaultCommands4ObjTypes[itemData.ObjectType, true];
        return commands4ObjType != null && commands4ObjType.CommandHandler == DefaultCommandHandler.ContectMenu && this.ExecuteMenuCommand(commands4ObjType, Event);
      }

      public bool ApplicationHasOpenedModalForms
      {
        get
        {
          foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
          {
            if (openForm.Modal)
              return true;
          }
          return false;
        }
      }

      public bool AllWindowsRefreshButtonTextVisible
      {
        [DebuggerStepThrough] get => this.btnFullRefresh.ShowText;
        set => this.btnFullRefresh.ShowText = value;
      }

      public void ReloadAllWindows(object sender)
      {
        this.btnFullRefresh.ShowText = false;
        this.btnLineStyleSetup.ShowText = false;
        if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
          return;
        object sender1 = sender ?? (object) this;
        NotificationEventArgs e = new NotificationEventArgs("ProjectChanged");
        service.FireEvent(sender1, e);
      }

      public void UpdateWindow()
      {
        this.Invalidate();
        this.Update();
      }

      public void CheckToolbarsBlocking()
      {
        ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
        bool blockedToolbars = service.BlockedToolbars;
        this._filterToolbar.Closable = !blockedToolbars;
        this.toolBarEditingContexts.Closable = !blockedToolbars;
        if (blockedToolbars)
        {
          this._filterToolbar.Hidden = false;
          this.toolBarEditingContexts.Hidden = false;
        }
        this.toolBarEditingContexts.Enabled = !service.LockEditingContextID;
      }

      public void CollectCurrentContextsHistory()
      {
        this._editingContextsFiltration.CollectCurrentContextsHistory();
      }

      public void RefreshEditingContextToolbar()
      {
        if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || this._editingContextsFiltration == null)
          return;
        this._editingContextsFiltration.AlterObject(service.CachedEditingContextID, true, true, true);
      }

      public List<long> EditingContextHistory
      {
        get => this._editingContextsFiltration.History;
        set
        {
          this._editingContextsFiltration.History.Clear();
          this._editingContextsFiltration.AddToHistory(value);
        }
      }

      Form IMainFormUpdate.MainForm => (Form) this;

      Screen IMainFormUpdate.MainFormScreen
      {
        get
        {
          Screen result = Screen.PrimaryScreen;
          if (!this.InvokeRequired)
            return Screen.FromControl((Control) this);
          this.Invoke((Delegate) (() => result = Screen.FromControl((Control) this)));
          return result;
        }
      }

      Rectangle IMainFormUpdate.PrimaryWorkingArea
      {
        get
        {
          Rectangle result = Rectangle.Empty;
          if (!this.InvokeRequired)
            return Screen.FromControl((Control) this).WorkingArea;
          this.Invoke((Delegate) (() => result = Screen.FromControl((Control) this).WorkingArea));
          return result;
        }
      }

      public event ActivateViewEventHandler OnActivateView;

      public void FireActivateViewEvent(object sender, ActivateViewEventArgs e)
      {
        if (this.OnActivateView == null)
          return;
        Delegate[] invocationList = this.OnActivateView.GetInvocationList();
        for (int index = 0; index < invocationList.Length; ++index)
        {
          bool flag = true;
          if (invocationList[index].Target is Control)
          {
            Control target = (Control) invocationList[index].Target;
            if (target.InvokeRequired)
            {
              flag = false;
              target.BeginInvoke(invocationList[index], sender, (object) e);
            }
          }
          if (flag)
            ((ActivateViewEventHandler) invocationList[index])(sender, e);
        }
      }

      private void UserInterfaceMenuButtonItem_Click(object sender, EventArgs e)
      {
        CustomizationForm.Execute();
      }

      private void DoBeforeMainMenuPopup(object sender, MenuPopupEventArgs e)
      {
        this._createObjTypesMenuMRU.PrepareControls();
        DockControl activeDocument = this._docContainer.ActiveDocument;
        this._closeMenuButtonItem.Enabled = activeDocument != null && activeDocument.Closable;
      }

      private void btNewItem_BeforePopup(object sender, MenuPopupEventArgs e)
      {
        this.btNewItem.DisposeChildren();
        this._createObjTypesMenuMRU.PrepareControls();
        foreach (MenuButtonItem menuButtonItem1 in (CollectionBase) this._createMenuButtonItem.Items)
        {
          MenuButtonItem menuButtonItem2 = (MenuButtonItem) menuButtonItem1.CloneItem();
          this.btNewItem.Items.Add((ToolbarItemBase) menuButtonItem2);
          menuButtonItem2.Tag = menuButtonItem1.Tag is IMRUItem ? menuButtonItem1.Tag : (object) menuButtonItem1;
        }
      }

      private void btNewItem_Click(object sender, EventArgs e)
      {
        this.btNewItem_BeforePopup(sender, (MenuPopupEventArgs) null);
        MenuButtonItem tag1 = this.btNewItem.Tag as MenuButtonItem;
        IMRUItem tag2 = this.btNewItem.Tag as IMRUItem;
        if (tag1 != null)
        {
          tag1.PerformClick();
        }
        else
        {
          if (this._createMenuButtonItem.Items.Count <= 0)
            return;
          MenuButtonItem menuButtonItem = this._createMenuButtonItem.Items[0];
          this.btNewItem.ToolTipText = LocalizationHolder.rm.GetString("IMClient_56");
          if (tag2 != null)
          {
            for (int index = 0; index < this._createMenuButtonItem.Items.Count; ++index)
            {
              if (this._createMenuButtonItem.Items[index].Tag != null && this._createMenuButtonItem.Items[index].Tag.Equals((object) tag2))
              {
                menuButtonItem = this._createMenuButtonItem.Items[index];
                this.btNewItem.ToolTipText = string.Format(LocalizationHolder.rm.GetString("IMClient_57"), (object) tag2.Caption);
                break;
              }
            }
          }
          this.btNewItem.Tag = (object) menuButtonItem;
          menuButtonItem.PerformClick();
        }
      }

      private void CloseMenuButtonItem_Click(object sender, EventArgs e)
      {
        DockControl activeDocument = this._docContainer.ActiveDocument;
        if (activeDocument == null || !activeDocument.Closable)
          return;
        activeDocument.Close();
      }

      private void AboutMenuButtonItem_Click(object sender, EventArgs e) => AboutIPS.ShowAboutBox();

      public SizeF ScaleFactor => this._scaleFactor;

      public ButtonItem ButtonEditingContextsEdit => this._buttonEditingContextsEdit;

      public DropDownMenuItem MenuEditingContextMode => this._buttonEditingContextMode;

      public ButtonItem ButtonEditingContextsRefresh => this._buttonEditingContextsRefresh;

      public ButtonItem ButtonEditingContextsCreate => this._buttonEditingContextsCreate;

      public ButtonItem ButtonEditingContextsBrowse => this._buttonEditingContextsBrowse;

      public DropDownMenuItem ButtonProjectFilterMode => this._buttonProjectFilterMode;

      protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
      {
        base.ScaleControl(factor, specified);
        if (!(this._scaleFactor == SizeF.Empty))
          return;
        this._scaleFactor = factor;
      }

      private void HelpMenuButtonItem_Click(object sender, EventArgs e)
      {
        HelpProvidersClass.ShowHelp();
      }

      private void IndexMenuButtonItem_Click(object sender, EventArgs e)
      {
        HelpProvidersClass.ShowIndexes();
      }

      private void SearchMenuButtonItem_Click(object sender, EventArgs e)
      {
        HelpProvidersClass.ShowSearch();
      }

      private void DynamicHelpMenuButtonItem_Click(object sender, EventArgs e)
      {
        DockManager service = (DockManager) ServicesManager.GetService(typeof (DockManager));
        if (service != null && service.ActiveDockControl != null)
        {
          Control control = service.ActiveDockControl.ActiveControl;
          string topicID;
          for (topicID = (string) null; string.IsNullOrEmpty(topicID) && control != null; control = control.Parent)
            topicID = HelpProvidersClass.GetHelpKeyword(control.GetType());
          if (!string.IsNullOrEmpty(topicID))
            HelpProvidersClass.ShowHelpTopic(topicID);
          else
            HelpProvidersClass.ShowHelpTopic(service.ActiveDockControl.HelpID);
        }
        else
          HelpProvidersClass.ShowHelpTopic(649);
      }

      private void btClearHistory_Click(object sender, EventArgs e)
      {
        this._acsc.Clear();
        this.cbAddress.Items.Clear();
      }

      private void toolBarProjects_ButtonClick(object sender, ToolBarItemEventArgs e)
      {
      }

      private void FulfillmentOfDutiesMenuButtonItem_Click(object sender, EventArgs e)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          List<ActingUserLoginSettings> userLoginSettings = sessionKeeper.Session.GetActingUserLoginSettings(sessionKeeper.Session.UserID);
          if (userLoginSettings != null)
          {
            if (userLoginSettings.Count != 0)
              goto label_7;
          }
          int num = (int) MessageBox.Show("В настоящий момент не назначены пользователи, обязанности которых вы можете исполнять.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
    label_7:
        this.StartNewClientInstance();
      }

      private void StartNewClientInstance()
      {
        ActingUserInfo actingUserInfo;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          actingUserInfo = new ActingUserInfo(sessionKeeper.Session.UserID, sessionKeeper.Session.ComputerName, sessionKeeper.Session.TimeZoneOffset, sessionKeeper.Session.SecurityLevel);
        Process.Start(new ProcessStartInfo(Application.ExecutablePath)
        {
          Arguments = $"ActingUser:{this.EncodeActingUserInfo(actingUserInfo)} "
        });
      }

      private string EncodeActingUserInfo(ActingUserInfo actingUserInfo)
      {
        byte[] array;
        using (MemoryStream output = new MemoryStream())
        {
          using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.UTF8, true))
          {
            binaryWriter.Write(actingUserInfo.UserID);
            binaryWriter.Write(actingUserInfo.ComputerName);
            binaryWriter.Write(actingUserInfo.TimeZoneOffset.Ticks);
            binaryWriter.Write(actingUserInfo.SecurityLevel);
          }
          output.Flush();
          array = output.ToArray();
        }
        return Convert.ToBase64String(array);
      }

      private void SupportMenuButtonItem_Click(object sender, EventArgs e)
      {
        using (RequestForm requestForm = new RequestForm())
        {
          if (requestForm.ShowForm() != DialogResult.OK)
            return;
          new Intermech.Interfaces.Client.InformationRequest.InformationRequest().SendRequest(requestForm.RequestInformation, requestForm.Attach);
        }
      }

      private void DoAllWindowsRefresh(object sender, EventArgs e) => this.ReloadAllWindows(sender);

      private void ShowLineStyleSetupDialog(object sender, EventArgs e)
      {
        this._propertyPages.ShowDialog(BlackWidthViewPage.GetPath());
      }

      private void OpenConfigurationDialogButtonItem_Click(object sender, EventArgs e)
      {
        this._propertyPages.ShowDialog();
      }

      private void VoiceAssistant_StateChanged(object sender, EventArgs e)
      {
        this.UpdateVoiceAssistantStatusBarPanel();
      }

      private void StatusBar_PanelClick(object sender, StatusBarPanelClickEventArgs e)
      {
        if (e.StatusBarPanel != this._voiceAssistantStatusBarPanel)
          return;
        IVoiceAssistant service = ServicesManager.GetService(typeof (IVoiceAssistant)) as IVoiceAssistant;
        if (service.State == VoiceAssistantState.Running)
          service.Stop();
        else
          service.Start();
      }

      private void ExceptionHandlerService_HandleException(object sender, ExceptionEventArgs e)
      {
        Exception exception = (Exception) null;
        if (e.Exception is ObjectsFoundException || e.Exception is RelationsFoundException)
          exception = e.Exception;
        else if (e.Exception != null && (e.Exception.InnerException is ObjectsFoundException || e.Exception.InnerException is RelationsFoundException))
          exception = e.Exception.InnerException;
        switch (exception)
        {
          case ObjectsFoundException _:
            e.Handled = true;
            using (ObjectsFoundExceptionDialog foundExceptionDialog = new ObjectsFoundExceptionDialog())
            {
              foundExceptionDialog.Exception = (ObjectsFoundException) exception;
              int num = (int) foundExceptionDialog.ShowDialog();
              break;
            }
          case RelationsFoundException _:
            e.Handled = true;
            using (RelationsFoundExceptionDialog foundExceptionDialog = new RelationsFoundExceptionDialog())
            {
              foundExceptionDialog.Exception = (RelationsFoundException) exception;
              int num = (int) foundExceptionDialog.ShowDialog();
              break;
            }
        }
      }

      private void SynchronizeVersionRulesCache(object sender, UserSessionCreatedEventArgs e)
      {
        if (this._filtration.FiltrationsCache == null || !(e.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
          return;
        foreach (KeyValuePair<string, FiltrationSettings> keyValuePair in this._filtration.FiltrationsCache)
          customService.SetFiltrationSettings((object) e.Session.SessionGUID, keyValuePair.Key, keyValuePair.Value);
      }

      private void UpdateVoiceAssistantStatusBarPanel()
      {
        IVoiceAssistant service = ServicesManager.GetService(typeof (IVoiceAssistant)) as IVoiceAssistant;
        ResourceManager resourceManager = new ResourceManager("IMClient.Resources.IMClientResources", this.GetType().Assembly);
        if (service.State == VoiceAssistantState.Running)
        {
          this._voiceAssistantStatusBarPanel.Icon = (Icon) resourceManager.GetObject("microphone");
          this._voiceAssistantStatusBarPanel.ToolTipText = "Голосовой помощник запущен";
        }
        else if (service.State == VoiceAssistantState.Stopped)
        {
          this._voiceAssistantStatusBarPanel.Icon = (Icon) resourceManager.GetObject("microphone_minus");
          this._voiceAssistantStatusBarPanel.ToolTipText = "Голосовой помощник остановлен";
        }
        else
        {
          if (service.State != VoiceAssistantState.StoppedOnError)
            return;
          this._voiceAssistantStatusBarPanel.Icon = (Icon) resourceManager.GetObject("microphone_exclamation");
          this._voiceAssistantStatusBarPanel.ToolTipText = "Голосовой помощник остановлен. Возникла ошибка";
        }
      }

      private void MainForm_Move(object sender, EventArgs e) => this.RefreshDesktopLocation();

      private void RefreshDesktopLocation()
      {
        if (this.WindowState == FormWindowState.Minimized)
          return;
        this._desktopLocation = this.DesktopLocation;
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
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainForm));
        this._mainImageList = new ImageList(this.components);
        this._disableActiveTargetTimer = new System.Windows.Forms.Timer(this.components);
        this._statusBar = new StatusBar();
        this.statusBarPanel1 = new StatusBarPanel();
        this.sbpIOUserName = new StatusBarPanel();
        this.sbpUserName = new StatusBarPanel();
        this.sbpRole = new StatusBarPanel();
        this.sbpProject = new StatusBarPanel();
        this.spbLevel = new StatusBarPanel();
        this._voiceAssistantStatusBarPanel = new StatusBarPanel();
        this._docContainer = new DocumentContainer();
        this.leftDock = new DockContainer();
        this._dockManager = new DockManager();
        this.rightDock = new DockContainer();
        this.bottomDock = new DockContainer();
        this.topDock = new DockContainer();
        this.leftBarDock = new ToolBarContainer();
        this._barManager = new BarManager();
        this.rightBarDock = new ToolBarContainer();
        this.bottomBarDock = new ToolBarContainer();
        this.topBarDock = new ToolBarContainer();
        this._navigateToolbar = new Intermech.Bars.ToolBar();
        this.btParametersCard = new ButtonItem();
        this.btCheckOut = new ButtonItem();
        this.btSaveChanges = new ButtonItem();
        this.btCheckIn = new ButtonItem();
        this.btCancelChanges = new ButtonItem();
        this.btAdminCancelChanges = new ButtonItem();
        this.btDelete = new ButtonItem();
        this.btExclude = new ButtonItem();
        this._configurationToolbar = new Intermech.Bars.ToolBar();
        this._openConfigurationDialogButtonItem = new ButtonItem();
        this._filterToolbar = new Intermech.Bars.ToolBar();
        this.cbFiltrationRule = new ComboBoxItem();
        this.btRuleVariant = new ButtonItem();
        this.btRuleBrowser = new ButtonItem();
        this.btRuleHint = new ButtonItem();
        this._useStoredExplicitPartVersionIDButtonItem = new ButtonItem();
        this.toolBarEditingContexts = new Intermech.Bars.ToolBar();
        this.labelContext = new LabelItem();
        this.contextsList = new DropDownMenuItem();
        this._buttonEditingContextsEdit = new ButtonItem();
        this._buttonEditingContextMode = new DropDownMenuItem();
        this._buttonEditingContextsRefresh = new ButtonItem();
        this._buttonEditingContextsBrowse = new ButtonItem();
        this._buttonEditingContextsCreate = new ButtonItem();
        this.toolBarProjects = new Intermech.Bars.ToolBar();
        this.labelItem1 = new LabelItem();
        this.projectsList = new DropDownMenuItem();
        this.menuButtonItem1 = new MenuButtonItem();
        this._buttonProjectRefresh = new ButtonItem();
        this._buttonProjectFilterMode = new DropDownMenuItem();
        this._mainMenu = new MenuBar();
        this._fileMenuBarItem = new MenuBarItem();
        this._createMenuButtonItem = new MenuButtonItem();
        this._createNewMenuButtonItem = new MenuButtonItem();
        this._closeMenuButtonItem = new MenuButtonItem();
        this._saveMenuButtonItem = new MenuButtonItem();
        this._saveAsMenuButtonItem = new MenuButtonItem();
        this._cardMenuButtonItem = new MenuButtonItem();
        this._checkOutMenuButtonItem = new MenuButtonItem();
        this._saveChangesMenuButtonItem = new MenuButtonItem();
        this._checkInMenuButtonItem = new MenuButtonItem();
        this._cancelChangesMenuButtonItem = new MenuButtonItem();
        this._adminCancelChangesMenuButtonItem = new MenuButtonItem();
        this._deleteMenuButtonItem = new MenuButtonItem();
        this._printPreviewMenuButtonItem = new MenuButtonItem();
        this._printMenuButtonItem = new MenuButtonItem();
        this._fulfillmentOfDutiesMenuButtonItem = new MenuButtonItem();
        this._exitMenuButtonItem = new MenuButtonItem();
        this._editMenuBarItem = new MenuBarItem();
        this._findMenuButtonItem = new MenuButtonItem();
        this._copyMenuButtonItem = new MenuButtonItem();
        this._cutMenuButtonItem = new MenuButtonItem();
        this._excludeMenuButtonItem = new MenuButtonItem();
        this._pasteMenuButtonItem = new MenuButtonItem();
        this._applicationsMenuBarItem = new MenuBarItem();
        this._compositionMenuBarItem = new MenuBarItem();
        this._tuningMenuBarItem = new MenuBarItem();
        this._userInterfaceMenuButtonItem = new MenuButtonItem();
        this._pluginsMenuButtonItem = new MenuButtonItem();
        this._loadPluginMenuButtonItem = new MenuButtonItem();
        this._pluginsListMenuButtonItem = new MenuButtonItem();
        this._administratorUtilitiesMenuButtonItem = new MenuButtonItem();
        this._settingsMenuButtonItem = new MenuButtonItem();
        this._exportImportMenuBarItem = new MenuBarItem();
        this._viewMenuBarItem = new MenuBarItem();
        this._taskBarMenuButtonItem = new MenuButtonItem();
        this._fullScreenMenuButtonItem = new MenuButtonItem();
        this._overviewMenuButtonItem = new MenuButtonItem();
        this._clipboardMenuButtonItem = new MenuButtonItem();
        this._namedImageListMenuButtonItem = new MenuButtonItem();
        this._serverObjectsMenuButtonItem = new MenuButtonItem();
        this._backgroundTasksMenuButtonItem = new MenuButtonItem();
        this._outputMenuButtonItem = new MenuButtonItem();
        this._serverOutputMenuButtonItem = new MenuButtonItem();
        this._uinotifmenuButtonItem = new MenuButtonItem();
        this._windowsMenuBarItem = new MenuBarItem();
        this._previousWindowMenuButtonItem = new MenuButtonItem();
        this._nextWindowMenuButtonItem = new MenuButtonItem();
        this._closeAllWindowsMenuButtonItem = new MenuButtonItem();
        this._helpMenuBarItem = new MenuBarItem();
        this._helpMenuButtonItem = new MenuButtonItem();
        this._indexMenuButtonItem = new MenuButtonItem();
        this._searchMenuButtonItem = new MenuButtonItem();
        this._supportMenuButtonItem = new MenuButtonItem();
        this._aboutMenuButtonItem = new MenuButtonItem();
        this.docTabContextMenu = new ContextMenuBarItem();
        this.mnContextSaveDocument = new MenuButtonItem();
        this.mnCloseDocument = new MenuButtonItem();
        this.mnCloseAllBut = new MenuButtonItem();
        this.mnOpenNewInstanceAsObject = new MenuButtonItem();
        this.mnNewHorizontalGroup = new MenuButtonItem();
        this.mnNewVerticalGroup = new MenuButtonItem();
        this._mainToolbar = new Intermech.Bars.ToolBar();
        this.btNewItem = new DropDownMenuItem();
        this.btSave = new ButtonItem();
        this.btPrint = new ButtonItem();
        this.btPrintPreview = new ButtonItem();
        this.btnLineStyleSetup = new ButtonItem();
        this.btCut = new ButtonItem();
        this.btCopy = new ButtonItem();
        this.btPaste = new ButtonItem();
        this.btUndo = new DropDownMenuItem();
        this.btRedo = new DropDownMenuItem();
        this.btDocumentBack = new DropDownMenuItem();
        this.btDocumentForward = new DropDownMenuItem();
        this.btRefresh = new ButtonItem();
        this.btnFullRefresh = new ButtonItem();
        this._addressToolbar = new Intermech.Bars.ToolBar();
        this.cbAddress = new ComboBoxItem();
        this.btGotoAddress = new ButtonItem();
        this.btClearHistory = new ButtonItem();
        this.btNavigateBack = new DropDownMenuItem();
        this.btNavigateForward = new DropDownMenuItem();
        this._dynamicHelpMenuButtonItem = new MenuButtonItem();
        this._documentLayoutMenuButtonItem = new MenuButtonItem();
        this._undoMenuButtonItem = new MenuButtonItem();
        this._redoMenuButtonItem = new MenuButtonItem();
        this.statusBarPanel1.BeginInit();
        this.sbpIOUserName.BeginInit();
        this.sbpUserName.BeginInit();
        this.sbpRole.BeginInit();
        this.sbpProject.BeginInit();
        this.spbLevel.BeginInit();
        this._voiceAssistantStatusBarPanel.BeginInit();
        this.topBarDock.SuspendLayout();
        this.SuspendLayout();
        this._mainImageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_mainImageList.ImageStream");
        this._mainImageList.TransparentColor = Color.Transparent;
        this._mainImageList.Images.SetKeyName(0, "перечитать_все_окна.png");
        this._mainImageList.Images.SetKeyName(1, "настройки.png");
        this._mainImageList.Images.SetKeyName(2, "h.ico");
        this._mainImageList.Images.SetKeyName(3, "обновить.png");
        this._mainImageList.Images.SetKeyName(4, "обновить_фильтр.png");
        this._mainImageList.Images.SetKeyName(5, "просмотр1.png");
        this._mainImageList.Images.SetKeyName(6, "настройка_поиска.png");
        this._mainImageList.Images.SetKeyName(7, "правило_подбора_версий.png");
        this._mainImageList.Images.SetKeyName(8, "редактировать.png");
        this._mainImageList.Images.SetKeyName(9, "ручная_сортировка.png");
        this._mainImageList.Images.SetKeyName(10, "настройка_ручной_сортировки.png");
        this._mainImageList.Images.SetKeyName(11, "вид.png");
        this._mainImageList.Images.SetKeyName(12, "группировка.png");
        this._mainImageList.Images.SetKeyName(13, "свернуть.png");
        this._mainImageList.Images.SetKeyName(14, "развернуть.png");
        this._mainImageList.Images.SetKeyName(15, "свернуть_все_кроме_активной.png");
        this._mainImageList.Images.SetKeyName(16 /*0x10*/, "настройка_линий.png");
        this._disableActiveTargetTimer.Interval = 200;
        this._disableActiveTargetTimer.Tick += new EventHandler(this.DisableActiveTargerTimer_Tick);
        componentResourceManager.ApplyResources((object) this._statusBar, "_statusBar");
        this._statusBar.Name = "_statusBar";
        this._statusBar.Panels.AddRange(new StatusBarPanel[7]
        {
          this.statusBarPanel1,
          this.sbpIOUserName,
          this.sbpUserName,
          this.sbpRole,
          this.sbpProject,
          this.spbLevel,
          this._voiceAssistantStatusBarPanel
        });
        this._statusBar.ShowPanels = true;
        this._statusBar.SizingGrip = false;
        this._statusBar.PanelClick += new StatusBarPanelClickEventHandler(this.StatusBar_PanelClick);
        this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Spring;
        componentResourceManager.ApplyResources((object) this.statusBarPanel1, "statusBarPanel1");
        this.sbpIOUserName.AutoSize = StatusBarPanelAutoSize.Contents;
        componentResourceManager.ApplyResources((object) this.sbpIOUserName, "sbpIOUserName");
        this.sbpUserName.AutoSize = StatusBarPanelAutoSize.Contents;
        componentResourceManager.ApplyResources((object) this.sbpUserName, "sbpUserName");
        this.sbpRole.AutoSize = StatusBarPanelAutoSize.Contents;
        componentResourceManager.ApplyResources((object) this.sbpRole, "sbpRole");
        componentResourceManager.ApplyResources((object) this.sbpProject, "sbpProject");
        componentResourceManager.ApplyResources((object) this.spbLevel, "spbLevel");
        componentResourceManager.ApplyResources((object) this._voiceAssistantStatusBarPanel, "_voiceAssistantStatusBarPanel");
        this._docContainer.DockingManager = DockingManager.Whidbey;
        this._docContainer.Guid = new Guid("c8ee4cdb-7a87-476c-b1c9-63047bdec5b2");
        this._docContainer.LayoutSystem = new SplitLayoutSystem(250, 400);
        componentResourceManager.ApplyResources((object) this._docContainer, "_docContainer");
        this._docContainer.Manager = (DockManager) null;
        this._docContainer.Name = "_docContainer";
        this._docContainer.Renderer = (Intermech.Docking.Rendering.RendererBase) null;
        this._docContainer.DocumentListClick += new DocumentContainer.DocumentListClickEventHandler(this.DocContainer_DocumentListClick);
        this._docContainer.DockingFinished += new EventHandler(this._docContainer_DockingFinished);
        this._docContainer.DockingStarted += new EventHandler(this._docContainer_DockingStarted);
        this._docContainer.ShowControlContextMenu += new ShowControlContextMenuEventHandler(this.Documents_ShowContextMenu);
        componentResourceManager.ApplyResources((object) this.leftDock, "leftDock");
        this.leftDock.Guid = new Guid("73cb47b5-2d92-4e89-9f3e-56996d70df40");
        this.leftDock.LayoutSystem = new SplitLayoutSystem(250, 400);
        this.leftDock.Manager = this._dockManager;
        this.leftDock.Name = "leftDock";
        this.leftDock.Renderer = (Intermech.Docking.Rendering.RendererBase) null;
        this._dockManager.DockingManager = DockingManager.Whidbey;
        this._dockManager.DocumentContainer = this._docContainer;
        this._dockManager.ImageList = this._mainImageList;
        this._dockManager.OwnerForm = (Form) this;
        this._dockManager.DockControlActivated += new DockControlEventHandler(this.DockManager_DockControlActivated);
        this._dockManager.DockControlDeactivated += new DockControlEventHandler(this.DockManager_DockControlDeactivated);
        this._dockManager.DockingFinished += new EventHandler(this._dockManager_DockingFinished);
        this._dockManager.DockingStarted += new EventHandler(this._dockManager_DockingStarted);
        componentResourceManager.ApplyResources((object) this.rightDock, "rightDock");
        this.rightDock.Guid = new Guid("68ddd40b-9774-4386-b56d-08beb339919b");
        this.rightDock.LayoutSystem = new SplitLayoutSystem(250, 400);
        this.rightDock.Manager = this._dockManager;
        this.rightDock.Name = "rightDock";
        this.rightDock.Renderer = (Intermech.Docking.Rendering.RendererBase) null;
        componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
        this.bottomDock.Guid = new Guid("a0f52ea3-5c46-4c00-8a25-ecbb3e3538c9");
        this.bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400);
        this.bottomDock.Manager = this._dockManager;
        this.bottomDock.Name = "bottomDock";
        this.bottomDock.Renderer = (Intermech.Docking.Rendering.RendererBase) null;
        componentResourceManager.ApplyResources((object) this.topDock, "topDock");
        this.topDock.Guid = new Guid("66303559-7fa2-48d0-9605-0c6ac3b6e307");
        this.topDock.LayoutSystem = new SplitLayoutSystem(250, 400);
        this.topDock.Manager = this._dockManager;
        this.topDock.Name = "topDock";
        this.topDock.Renderer = (Intermech.Docking.Rendering.RendererBase) null;
        componentResourceManager.ApplyResources((object) this.leftBarDock, "leftBarDock");
        this.leftBarDock.Guid = new Guid("b06b482f-6e39-4754-88e4-f5da219ecf19");
        this.leftBarDock.Manager = this._barManager;
        this.leftBarDock.Name = "leftBarDock";
        this._barManager.OwnerForm = (Form) this;
        componentResourceManager.ApplyResources((object) this.rightBarDock, "rightBarDock");
        this.rightBarDock.Guid = new Guid("f0774739-e6b0-4f10-9dee-95c84363571d");
        this.rightBarDock.Manager = this._barManager;
        this.rightBarDock.Name = "rightBarDock";
        componentResourceManager.ApplyResources((object) this.bottomBarDock, "bottomBarDock");
        this.bottomBarDock.Guid = new Guid("cc4d10c7-0bb0-406f-9c5a-b44ebda39fe5");
        this.bottomBarDock.Manager = this._barManager;
        this.bottomBarDock.Name = "bottomBarDock";
        this.topBarDock.Controls.Add((Control) this._navigateToolbar);
        this.topBarDock.Controls.Add((Control) this._configurationToolbar);
        this.topBarDock.Controls.Add((Control) this._filterToolbar);
        this.topBarDock.Controls.Add((Control) this.toolBarEditingContexts);
        this.topBarDock.Controls.Add((Control) this.toolBarProjects);
        this.topBarDock.Controls.Add((Control) this._mainMenu);
        this.topBarDock.Controls.Add((Control) this._mainToolbar);
        this.topBarDock.Controls.Add((Control) this._addressToolbar);
        componentResourceManager.ApplyResources((object) this.topBarDock, "topBarDock");
        this.topBarDock.Guid = new Guid("cb75cd35-fc72-4059-bd7c-b96574973c90");
        this.topBarDock.Manager = this._barManager;
        this.topBarDock.Name = "topBarDock";
        this._navigateToolbar.DockLine = 1;
        this._navigateToolbar.DockOffset = 2;
        this._navigateToolbar.FullMenus = true;
        this._navigateToolbar.Guid = new Guid("f34da14a-091a-4f96-934f-3e5ba2a5dc08");
        this._navigateToolbar.Hidden = false;
        this._navigateToolbar.ImageList = this._mainImageList;
        this._navigateToolbar.Items.AddRange(new ToolbarItemBase[8]
        {
          (ToolbarItemBase) this.btParametersCard,
          (ToolbarItemBase) this.btCheckOut,
          (ToolbarItemBase) this.btSaveChanges,
          (ToolbarItemBase) this.btCheckIn,
          (ToolbarItemBase) this.btCancelChanges,
          (ToolbarItemBase) this.btAdminCancelChanges,
          (ToolbarItemBase) this.btDelete,
          (ToolbarItemBase) this.btExclude
        });
        componentResourceManager.ApplyResources((object) this._navigateToolbar, "_navigateToolbar");
        this._navigateToolbar.Name = "_navigateToolbar";
        this.btParametersCard.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btParametersCard, "btParametersCard");
        this.btCheckOut.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btCheckOut, "btCheckOut");
        componentResourceManager.ApplyResources((object) this.btSaveChanges, "btSaveChanges");
        componentResourceManager.ApplyResources((object) this.btCheckIn, "btCheckIn");
        this.btCancelChanges.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btCancelChanges, "btCancelChanges");
        componentResourceManager.ApplyResources((object) this.btAdminCancelChanges, "btAdminCancelChanges");
        this.btDelete.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btDelete, "btDelete");
        componentResourceManager.ApplyResources((object) this.btExclude, "btExclude");
        this._configurationToolbar.DockLine = 1;
        this._configurationToolbar.DockOffset = 3;
        this._configurationToolbar.FullMenus = true;
        this._configurationToolbar.Guid = new Guid("4c09ddfe-d14f-4e4f-9761-6613ef851a66");
        this._configurationToolbar.Hidden = false;
        this._configurationToolbar.ImageList = this._mainImageList;
        this._configurationToolbar.Items.AddRange(new ToolbarItemBase[1]
        {
          (ToolbarItemBase) this._openConfigurationDialogButtonItem
        });
        componentResourceManager.ApplyResources((object) this._configurationToolbar, "_configurationToolbar");
        this._configurationToolbar.Name = "_configurationToolbar";
        this._openConfigurationDialogButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._openConfigurationDialogButtonItem, "_openConfigurationDialogButtonItem");
        this._openConfigurationDialogButtonItem.ImageIndex = 1;
        this._openConfigurationDialogButtonItem.Click += new EventHandler(this.OpenConfigurationDialogButtonItem_Click);
        this._filterToolbar.AllowVerticalDock = false;
        this._filterToolbar.DockLine = 2;
        this._filterToolbar.FullMenus = true;
        this._filterToolbar.Guid = new Guid("7b9a8adc-5be9-42fb-a7e2-91052e0fcbd9");
        this._filterToolbar.Hidden = false;
        this._filterToolbar.ImageList = this._mainImageList;
        this._filterToolbar.Items.AddRange(new ToolbarItemBase[5]
        {
          (ToolbarItemBase) this.cbFiltrationRule,
          (ToolbarItemBase) this.btRuleVariant,
          (ToolbarItemBase) this.btRuleBrowser,
          (ToolbarItemBase) this.btRuleHint,
          (ToolbarItemBase) this._useStoredExplicitPartVersionIDButtonItem
        });
        componentResourceManager.ApplyResources((object) this._filterToolbar, "_filterToolbar");
        this._filterToolbar.MinimumFloatingSize = new Size(250, 30);
        this._filterToolbar.Name = "_filterToolbar";
        this._filterToolbar.StretchItem = (ToolbarItemBase) this.cbFiltrationRule;
        componentResourceManager.ApplyResources((object) this.cbFiltrationRule, "cbFiltrationRule");
        this.cbFiltrationRule.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cbFiltrationRule.Locked = true;
        this.cbFiltrationRule.MinimumControlWidth = 100;
        this.cbFiltrationRule.MinimumSize = 356;
        this.cbFiltrationRule.Padding.Bottom = 0;
        this.cbFiltrationRule.Padding.Left = 1;
        this.cbFiltrationRule.Padding.Right = 1;
        this.cbFiltrationRule.Padding.Top = 0;
        this.cbFiltrationRule.Stretch = true;
        componentResourceManager.ApplyResources((object) this.btRuleVariant, "btRuleVariant");
        componentResourceManager.ApplyResources((object) this.btRuleBrowser, "btRuleBrowser");
        this.btRuleHint.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btRuleHint, "btRuleHint");
        this.btRuleHint.Locked = true;
        this.btRuleHint.ShowText = true;
        this._useStoredExplicitPartVersionIDButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._useStoredExplicitPartVersionIDButtonItem, "_useStoredExplicitPartVersionIDButtonItem");
        this._useStoredExplicitPartVersionIDButtonItem.ImageIndex = 2;
        this.toolBarEditingContexts.AllowVerticalDock = false;
        componentResourceManager.ApplyResources((object) this.toolBarEditingContexts, "toolBarEditingContexts");
        this.toolBarEditingContexts.DockLine = 2;
        this.toolBarEditingContexts.DockOffset = 1;
        this.toolBarEditingContexts.FullMenus = true;
        this.toolBarEditingContexts.Guid = new Guid("7e41d6d7-f8e4-4809-b69a-09b9706dffef");
        this.toolBarEditingContexts.Hidden = false;
        this.toolBarEditingContexts.ImageList = this._mainImageList;
        this.toolBarEditingContexts.Items.AddRange(new ToolbarItemBase[7]
        {
          (ToolbarItemBase) this.labelContext,
          (ToolbarItemBase) this.contextsList,
          (ToolbarItemBase) this._buttonEditingContextsEdit,
          (ToolbarItemBase) this._buttonEditingContextMode,
          (ToolbarItemBase) this._buttonEditingContextsRefresh,
          (ToolbarItemBase) this._buttonEditingContextsBrowse,
          (ToolbarItemBase) this._buttonEditingContextsCreate
        });
        this.toolBarEditingContexts.Name = "toolBarEditingContexts";
        componentResourceManager.ApplyResources((object) this.labelContext, "labelContext");
        this.labelContext.Importance = ToolBarItemImportance.Highest;
        componentResourceManager.ApplyResources((object) this.contextsList, "contextsList");
        this.contextsList.Importance = ToolBarItemImportance.Highest;
        this.contextsList.MinimumSize = 64 /*0x40*/;
        this.contextsList.ShowText = true;
        componentResourceManager.ApplyResources((object) this._buttonEditingContextsEdit, "_buttonEditingContextsEdit");
        this._buttonEditingContextsEdit.Importance = ToolBarItemImportance.Highest;
        componentResourceManager.ApplyResources((object) this._buttonEditingContextMode, "_buttonEditingContextMode");
        this._buttonEditingContextMode.Importance = ToolBarItemImportance.Highest;
        this._buttonEditingContextMode.ShowText = true;
        this._buttonEditingContextsRefresh.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._buttonEditingContextsRefresh, "_buttonEditingContextsRefresh");
        componentResourceManager.ApplyResources((object) this._buttonEditingContextsBrowse, "_buttonEditingContextsBrowse");
        componentResourceManager.ApplyResources((object) this._buttonEditingContextsCreate, "_buttonEditingContextsCreate");
        this.toolBarProjects.AllowVerticalDock = false;
        this.toolBarProjects.DockLine = 2;
        this.toolBarProjects.DockOffset = 2;
        this.toolBarProjects.FullMenus = true;
        this.toolBarProjects.Guid = new Guid("e11bca70-2265-4870-bdeb-38acc74f65eb");
        this.toolBarProjects.Hidden = false;
        this.toolBarProjects.ImageList = this._mainImageList;
        this.toolBarProjects.Items.AddRange(new ToolbarItemBase[4]
        {
          (ToolbarItemBase) this.labelItem1,
          (ToolbarItemBase) this.projectsList,
          (ToolbarItemBase) this._buttonProjectRefresh,
          (ToolbarItemBase) this._buttonProjectFilterMode
        });
        componentResourceManager.ApplyResources((object) this.toolBarProjects, "toolBarProjects");
        this.toolBarProjects.Name = "toolBarProjects";
        this.toolBarProjects.ButtonClick += new Intermech.Bars.ToolBar.ButtonClickEventHandler(this.toolBarProjects_ButtonClick);
        componentResourceManager.ApplyResources((object) this.labelItem1, "labelItem1");
        this.labelItem1.Importance = ToolBarItemImportance.Highest;
        componentResourceManager.ApplyResources((object) this.projectsList, "projectsList");
        this.projectsList.Importance = ToolBarItemImportance.Highest;
        this.projectsList.Items.AddRange(new ToolbarItemBase[1]
        {
          (ToolbarItemBase) this.menuButtonItem1
        });
        this.projectsList.MinimumSize = 64 /*0x40*/;
        this.projectsList.ShowText = true;
        componentResourceManager.ApplyResources((object) this.menuButtonItem1, "menuButtonItem1");
        this.menuButtonItem1.ShowText = true;
        componentResourceManager.ApplyResources((object) this._buttonProjectRefresh, "_buttonProjectRefresh");
        this._buttonProjectFilterMode.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._buttonProjectFilterMode, "_buttonProjectFilterMode");
        this._buttonProjectFilterMode.ShowText = true;
        this._mainMenu.FullMenus = true;
        this._mainMenu.Guid = new Guid("b8259ee3-e05c-42f6-97ab-1550ac4fcfbe");
        this._mainMenu.Hidden = false;
        this._mainMenu.ImageList = this._mainImageList;
        this._mainMenu.Items.AddRange(new ToolbarItemBase[10]
        {
          (ToolbarItemBase) this._fileMenuBarItem,
          (ToolbarItemBase) this._editMenuBarItem,
          (ToolbarItemBase) this._applicationsMenuBarItem,
          (ToolbarItemBase) this._compositionMenuBarItem,
          (ToolbarItemBase) this._tuningMenuBarItem,
          (ToolbarItemBase) this._exportImportMenuBarItem,
          (ToolbarItemBase) this._viewMenuBarItem,
          (ToolbarItemBase) this._windowsMenuBarItem,
          (ToolbarItemBase) this._helpMenuBarItem,
          (ToolbarItemBase) this.docTabContextMenu
        });
        componentResourceManager.ApplyResources((object) this._mainMenu, "_mainMenu");
        this._mainMenu.Name = "_mainMenu";
        this._mainMenu.OwnerForm = (Form) this;
        this._mainMenu.ButtonClick += new Intermech.Bars.ToolBar.ButtonClickEventHandler(this.MainMenu_ButtonClick);
        componentResourceManager.ApplyResources((object) this._fileMenuBarItem, "_fileMenuBarItem");
        this._fileMenuBarItem.Items.AddRange(new ToolbarItemBase[15]
        {
          (ToolbarItemBase) this._createMenuButtonItem,
          (ToolbarItemBase) this._closeMenuButtonItem,
          (ToolbarItemBase) this._saveMenuButtonItem,
          (ToolbarItemBase) this._saveAsMenuButtonItem,
          (ToolbarItemBase) this._cardMenuButtonItem,
          (ToolbarItemBase) this._checkOutMenuButtonItem,
          (ToolbarItemBase) this._saveChangesMenuButtonItem,
          (ToolbarItemBase) this._checkInMenuButtonItem,
          (ToolbarItemBase) this._cancelChangesMenuButtonItem,
          (ToolbarItemBase) this._adminCancelChangesMenuButtonItem,
          (ToolbarItemBase) this._deleteMenuButtonItem,
          (ToolbarItemBase) this._printPreviewMenuButtonItem,
          (ToolbarItemBase) this._printMenuButtonItem,
          (ToolbarItemBase) this._fulfillmentOfDutiesMenuButtonItem,
          (ToolbarItemBase) this._exitMenuButtonItem
        });
        this._fileMenuBarItem.ShowText = true;
        this._fileMenuBarItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.DoBeforeMainMenuPopup);
        componentResourceManager.ApplyResources((object) this._createMenuButtonItem, "_createMenuButtonItem");
        this._createMenuButtonItem.Items.AddRange(new ToolbarItemBase[1]
        {
          (ToolbarItemBase) this._createNewMenuButtonItem
        });
        this._createMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._createNewMenuButtonItem, "_createNewMenuButtonItem");
        this._createNewMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._closeMenuButtonItem, "_closeMenuButtonItem");
        this._closeMenuButtonItem.ShowText = true;
        this._closeMenuButtonItem.Click += new EventHandler(this.CloseMenuButtonItem_Click);
        this._saveMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._saveMenuButtonItem, "_saveMenuButtonItem");
        this._saveMenuButtonItem.Shortcut = Shortcut.CtrlS;
        this._saveMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._saveAsMenuButtonItem, "_saveAsMenuButtonItem");
        this._saveAsMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._cardMenuButtonItem, "_cardMenuButtonItem");
        this._cardMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._checkOutMenuButtonItem, "_checkOutMenuButtonItem");
        this._checkOutMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._saveChangesMenuButtonItem, "_saveChangesMenuButtonItem");
        this._saveChangesMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._checkInMenuButtonItem, "_checkInMenuButtonItem");
        this._checkInMenuButtonItem.ShowText = true;
        this._cancelChangesMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._cancelChangesMenuButtonItem, "_cancelChangesMenuButtonItem");
        this._cancelChangesMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._adminCancelChangesMenuButtonItem, "_adminCancelChangesMenuButtonItem");
        this._adminCancelChangesMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._deleteMenuButtonItem, "_deleteMenuButtonItem");
        this._deleteMenuButtonItem.Shortcut = Shortcut.CtrlDel;
        this._deleteMenuButtonItem.ShowText = true;
        this._printPreviewMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._printPreviewMenuButtonItem, "_printPreviewMenuButtonItem");
        this._printPreviewMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._printMenuButtonItem, "_printMenuButtonItem");
        this._printMenuButtonItem.Shortcut = Shortcut.CtrlP;
        this._printMenuButtonItem.ShowText = true;
        this._fulfillmentOfDutiesMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._fulfillmentOfDutiesMenuButtonItem, "_fulfillmentOfDutiesMenuButtonItem");
        this._fulfillmentOfDutiesMenuButtonItem.ShowText = true;
        this._fulfillmentOfDutiesMenuButtonItem.Click += new EventHandler(this.FulfillmentOfDutiesMenuButtonItem_Click);
        this._exitMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._exitMenuButtonItem, "_exitMenuButtonItem");
        this._exitMenuButtonItem.ShowText = true;
        this._exitMenuButtonItem.Click += new EventHandler(this.ExitMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._editMenuBarItem, "_editMenuBarItem");
        this._editMenuBarItem.Items.AddRange(new ToolbarItemBase[5]
        {
          (ToolbarItemBase) this._findMenuButtonItem,
          (ToolbarItemBase) this._copyMenuButtonItem,
          (ToolbarItemBase) this._cutMenuButtonItem,
          (ToolbarItemBase) this._excludeMenuButtonItem,
          (ToolbarItemBase) this._pasteMenuButtonItem
        });
        this._editMenuBarItem.ShowText = true;
        this._findMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._findMenuButtonItem, "_findMenuButtonItem");
        this._findMenuButtonItem.Shortcut = Shortcut.CtrlF;
        this._findMenuButtonItem.ShowText = true;
        this._copyMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._copyMenuButtonItem, "_copyMenuButtonItem");
        this._copyMenuButtonItem.Shortcut = Shortcut.CtrlC;
        this._copyMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._cutMenuButtonItem, "_cutMenuButtonItem");
        this._cutMenuButtonItem.Shortcut = Shortcut.CtrlX;
        this._cutMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._excludeMenuButtonItem, "_excludeMenuButtonItem");
        this._excludeMenuButtonItem.Shortcut = Shortcut.Del;
        this._excludeMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._pasteMenuButtonItem, "_pasteMenuButtonItem");
        this._pasteMenuButtonItem.Shortcut = Shortcut.CtrlV;
        this._pasteMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._applicationsMenuBarItem, "_applicationsMenuBarItem");
        this._applicationsMenuBarItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._compositionMenuBarItem, "_compositionMenuBarItem");
        this._compositionMenuBarItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._tuningMenuBarItem, "_tuningMenuBarItem");
        this._tuningMenuBarItem.Items.AddRange(new ToolbarItemBase[4]
        {
          (ToolbarItemBase) this._userInterfaceMenuButtonItem,
          (ToolbarItemBase) this._pluginsMenuButtonItem,
          (ToolbarItemBase) this._administratorUtilitiesMenuButtonItem,
          (ToolbarItemBase) this._settingsMenuButtonItem
        });
        this._tuningMenuBarItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._userInterfaceMenuButtonItem, "_userInterfaceMenuButtonItem");
        this._userInterfaceMenuButtonItem.ShowText = true;
        this._userInterfaceMenuButtonItem.Click += new EventHandler(this.UserInterfaceMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._pluginsMenuButtonItem, "_pluginsMenuButtonItem");
        this._pluginsMenuButtonItem.Items.AddRange(new ToolbarItemBase[2]
        {
          (ToolbarItemBase) this._loadPluginMenuButtonItem,
          (ToolbarItemBase) this._pluginsListMenuButtonItem
        });
        this._pluginsMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._loadPluginMenuButtonItem, "_loadPluginMenuButtonItem");
        this._loadPluginMenuButtonItem.ShowText = true;
        this._loadPluginMenuButtonItem.Click += new EventHandler(this.LoadPluginMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._pluginsListMenuButtonItem, "_pluginsListMenuButtonItem");
        this._pluginsListMenuButtonItem.ShowText = true;
        this._pluginsListMenuButtonItem.Click += new EventHandler(this.PluginsListMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._administratorUtilitiesMenuButtonItem, "_administratorUtilitiesMenuButtonItem");
        this._administratorUtilitiesMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._settingsMenuButtonItem, "_settingsMenuButtonItem");
        this._settingsMenuButtonItem.ShowText = true;
        this._settingsMenuButtonItem.Click += new EventHandler(this.SettingsMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._exportImportMenuBarItem, "_exportImportMenuBarItem");
        this._exportImportMenuBarItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._viewMenuBarItem, "_viewMenuBarItem");
        this._viewMenuBarItem.Items.AddRange(new ToolbarItemBase[10]
        {
          (ToolbarItemBase) this._taskBarMenuButtonItem,
          (ToolbarItemBase) this._fullScreenMenuButtonItem,
          (ToolbarItemBase) this._overviewMenuButtonItem,
          (ToolbarItemBase) this._clipboardMenuButtonItem,
          (ToolbarItemBase) this._namedImageListMenuButtonItem,
          (ToolbarItemBase) this._serverObjectsMenuButtonItem,
          (ToolbarItemBase) this._backgroundTasksMenuButtonItem,
          (ToolbarItemBase) this._outputMenuButtonItem,
          (ToolbarItemBase) this._serverOutputMenuButtonItem,
          (ToolbarItemBase) this._uinotifmenuButtonItem
        });
        this._viewMenuBarItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._taskBarMenuButtonItem, "_taskBarMenuButtonItem");
        this._taskBarMenuButtonItem.ShowText = true;
        this._taskBarMenuButtonItem.Click += new EventHandler(this.TaskBarMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._fullScreenMenuButtonItem, "_fullScreenMenuButtonItem");
        this._fullScreenMenuButtonItem.ShowText = true;
        this._fullScreenMenuButtonItem.Click += new EventHandler(this.FullScreenMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._overviewMenuButtonItem, "_overviewMenuButtonItem");
        this._overviewMenuButtonItem.ShowText = true;
        this._overviewMenuButtonItem.Click += new EventHandler(this.OverviewMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._clipboardMenuButtonItem, "_clipboardMenuButtonItem");
        this._clipboardMenuButtonItem.ShowText = true;
        this._clipboardMenuButtonItem.Click += new EventHandler(this.ClipboardMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._namedImageListMenuButtonItem, "_namedImageListMenuButtonItem");
        this._namedImageListMenuButtonItem.ShowText = true;
        this._namedImageListMenuButtonItem.Visible = false;
        this._namedImageListMenuButtonItem.Click += new EventHandler(this.NamedImageListMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._serverObjectsMenuButtonItem, "_serverObjectsMenuButtonItem");
        this._serverObjectsMenuButtonItem.ShowText = true;
        this._serverObjectsMenuButtonItem.Click += new EventHandler(this.ServerObjectsMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._backgroundTasksMenuButtonItem, "_backgroundTasksMenuButtonItem");
        this._backgroundTasksMenuButtonItem.ShowText = true;
        this._backgroundTasksMenuButtonItem.Click += new EventHandler(this.BackgroundTasksMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._outputMenuButtonItem, "_outputMenuButtonItem");
        this._outputMenuButtonItem.ShowText = true;
        this._outputMenuButtonItem.Click += new EventHandler(this.OutputMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._serverOutputMenuButtonItem, "_serverOutputMenuButtonItem");
        this._serverOutputMenuButtonItem.ShowText = true;
        this._serverOutputMenuButtonItem.Click += new EventHandler(this.ServerOutputMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._uinotifmenuButtonItem, "_uinotifmenuButtonItem");
        this._uinotifmenuButtonItem.ShowText = true;
        this._uinotifmenuButtonItem.Click += new EventHandler(this._errormenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._windowsMenuBarItem, "_windowsMenuBarItem");
        this._windowsMenuBarItem.Items.AddRange(new ToolbarItemBase[3]
        {
          (ToolbarItemBase) this._previousWindowMenuButtonItem,
          (ToolbarItemBase) this._nextWindowMenuButtonItem,
          (ToolbarItemBase) this._closeAllWindowsMenuButtonItem
        });
        this._windowsMenuBarItem.ShowText = true;
        this._windowsMenuBarItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.mnWindows_BeforePopup);
        this._previousWindowMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._previousWindowMenuButtonItem, "_previousWindowMenuButtonItem");
        this._previousWindowMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._nextWindowMenuButtonItem, "_nextWindowMenuButtonItem");
        this._nextWindowMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._closeAllWindowsMenuButtonItem, "_closeAllWindowsMenuButtonItem");
        this._closeAllWindowsMenuButtonItem.ShowText = true;
        this._closeAllWindowsMenuButtonItem.Click += new EventHandler(this.CloseAllWindowsMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._helpMenuBarItem, "_helpMenuBarItem");
        this._helpMenuBarItem.Items.AddRange(new ToolbarItemBase[5]
        {
          (ToolbarItemBase) this._helpMenuButtonItem,
          (ToolbarItemBase) this._indexMenuButtonItem,
          (ToolbarItemBase) this._searchMenuButtonItem,
          (ToolbarItemBase) this._supportMenuButtonItem,
          (ToolbarItemBase) this._aboutMenuButtonItem
        });
        this._helpMenuBarItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._helpMenuButtonItem, "_helpMenuButtonItem");
        this._helpMenuButtonItem.ShowText = true;
        this._helpMenuButtonItem.Click += new EventHandler(this.HelpMenuButtonItem_Click);
        this._indexMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._indexMenuButtonItem, "_indexMenuButtonItem");
        this._indexMenuButtonItem.ShowText = true;
        this._indexMenuButtonItem.Click += new EventHandler(this.IndexMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._searchMenuButtonItem, "_searchMenuButtonItem");
        this._searchMenuButtonItem.ShowText = true;
        this._searchMenuButtonItem.Click += new EventHandler(this.SearchMenuButtonItem_Click);
        this._supportMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._supportMenuButtonItem, "_supportMenuButtonItem");
        this._supportMenuButtonItem.ShowText = true;
        this._supportMenuButtonItem.Click += new EventHandler(this.SupportMenuButtonItem_Click);
        this._aboutMenuButtonItem.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this._aboutMenuButtonItem, "_aboutMenuButtonItem");
        this._aboutMenuButtonItem.ShowText = true;
        this._aboutMenuButtonItem.Click += new EventHandler(this.AboutMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this.docTabContextMenu, "docTabContextMenu");
        this.docTabContextMenu.Items.AddRange(new ToolbarItemBase[6]
        {
          (ToolbarItemBase) this.mnContextSaveDocument,
          (ToolbarItemBase) this.mnCloseDocument,
          (ToolbarItemBase) this.mnCloseAllBut,
          (ToolbarItemBase) this.mnOpenNewInstanceAsObject,
          (ToolbarItemBase) this.mnNewHorizontalGroup,
          (ToolbarItemBase) this.mnNewVerticalGroup
        });
        this.docTabContextMenu.ShowText = true;
        componentResourceManager.ApplyResources((object) this.mnContextSaveDocument, "mnContextSaveDocument");
        this.mnContextSaveDocument.ShowText = true;
        this.mnCloseDocument.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.mnCloseDocument, "mnCloseDocument");
        this.mnCloseDocument.ShowText = true;
        componentResourceManager.ApplyResources((object) this.mnCloseAllBut, "mnCloseAllBut");
        this.mnCloseAllBut.ShowText = true;
        componentResourceManager.ApplyResources((object) this.mnOpenNewInstanceAsObject, "mnOpenNewInstanceAsObject");
        this.mnOpenNewInstanceAsObject.ShowText = true;
        this.mnNewHorizontalGroup.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.mnNewHorizontalGroup, "mnNewHorizontalGroup");
        this.mnNewHorizontalGroup.ShowText = true;
        this.mnNewHorizontalGroup.Visible = false;
        componentResourceManager.ApplyResources((object) this.mnNewVerticalGroup, "mnNewVerticalGroup");
        this.mnNewVerticalGroup.ShowText = true;
        this.mnNewVerticalGroup.Visible = false;
        this._mainToolbar.DockLine = 1;
        this._mainToolbar.FullMenus = true;
        this._mainToolbar.Guid = new Guid("8677e272-35ff-41eb-9d50-4e160a2c65a2");
        this._mainToolbar.Hidden = false;
        this._mainToolbar.ImageList = this._mainImageList;
        this._mainToolbar.Items.AddRange(new ToolbarItemBase[14]
        {
          (ToolbarItemBase) this.btNewItem,
          (ToolbarItemBase) this.btSave,
          (ToolbarItemBase) this.btPrint,
          (ToolbarItemBase) this.btPrintPreview,
          (ToolbarItemBase) this.btnLineStyleSetup,
          (ToolbarItemBase) this.btCut,
          (ToolbarItemBase) this.btCopy,
          (ToolbarItemBase) this.btPaste,
          (ToolbarItemBase) this.btUndo,
          (ToolbarItemBase) this.btRedo,
          (ToolbarItemBase) this.btDocumentBack,
          (ToolbarItemBase) this.btDocumentForward,
          (ToolbarItemBase) this.btRefresh,
          (ToolbarItemBase) this.btnFullRefresh
        });
        componentResourceManager.ApplyResources((object) this._mainToolbar, "_mainToolbar");
        this._mainToolbar.Name = "_mainToolbar";
        componentResourceManager.ApplyResources((object) this.btNewItem, "btNewItem");
        this.btNewItem.ShowText = true;
        this.btNewItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.btNewItem_BeforePopup);
        this.btNewItem.Click += new EventHandler(this.btNewItem_Click);
        this.btSave.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btSave, "btSave");
        this.btPrint.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btPrint, "btPrint");
        componentResourceManager.ApplyResources((object) this.btPrintPreview, "btPrintPreview");
        componentResourceManager.ApplyResources((object) this.btnLineStyleSetup, "btnLineStyleSetup");
        this.btnLineStyleSetup.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
        this.btnLineStyleSetup.ImageIndex = 16 /*0x10*/;
        this.btnLineStyleSetup.Click += new EventHandler(this.ShowLineStyleSetupDialog);
        this.btCut.BeginGroup = true;
        this.btCut.BuddyMenu = this._cutMenuButtonItem;
        componentResourceManager.ApplyResources((object) this.btCut, "btCut");
        this.btCopy.BuddyMenu = this._copyMenuButtonItem;
        componentResourceManager.ApplyResources((object) this.btCopy, "btCopy");
        this.btPaste.BuddyMenu = this._pasteMenuButtonItem;
        componentResourceManager.ApplyResources((object) this.btPaste, "btPaste");
        this.btUndo.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btUndo, "btUndo");
        this.btUndo.ShowText = true;
        componentResourceManager.ApplyResources((object) this.btRedo, "btRedo");
        this.btRedo.ShowText = true;
        this.btDocumentBack.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btDocumentBack, "btDocumentBack");
        this.btDocumentBack.Enabled = false;
        this.btDocumentBack.ShowText = true;
        componentResourceManager.ApplyResources((object) this.btDocumentForward, "btDocumentForward");
        this.btDocumentForward.Enabled = false;
        this.btDocumentForward.ShowText = true;
        this.btRefresh.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btRefresh, "btRefresh");
        this.btnFullRefresh.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btnFullRefresh, "btnFullRefresh");
        this.btnFullRefresh.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
        this.btnFullRefresh.ImageIndex = 0;
        this.btnFullRefresh.Click += new EventHandler(this.DoAllWindowsRefresh);
        this._addressToolbar.AllowVerticalDock = false;
        this._addressToolbar.DockLine = 5;
        this._addressToolbar.FullMenus = true;
        this._addressToolbar.Guid = new Guid("5e2a34ba-ce70-44ea-969b-a5f0791ecb9c");
        this._addressToolbar.Hidden = false;
        this._addressToolbar.ImageList = this._mainImageList;
        this._addressToolbar.Items.AddRange(new ToolbarItemBase[5]
        {
          (ToolbarItemBase) this.cbAddress,
          (ToolbarItemBase) this.btGotoAddress,
          (ToolbarItemBase) this.btClearHistory,
          (ToolbarItemBase) this.btNavigateBack,
          (ToolbarItemBase) this.btNavigateForward
        });
        componentResourceManager.ApplyResources((object) this._addressToolbar, "_addressToolbar");
        this._addressToolbar.MinimumFloatingSize = new Size(260, 30);
        this._addressToolbar.Name = "_addressToolbar";
        this._addressToolbar.Stretch = true;
        this._addressToolbar.StretchItem = (ToolbarItemBase) this.cbAddress;
        componentResourceManager.ApplyResources((object) this.cbAddress, "cbAddress");
        this.cbAddress.Locked = true;
        this.cbAddress.MinimumControlWidth = 250;
        this.cbAddress.Padding.Bottom = 0;
        this.cbAddress.Padding.Left = 1;
        this.cbAddress.Padding.Right = 1;
        this.cbAddress.Padding.Top = 0;
        this.cbAddress.Stretch = true;
        componentResourceManager.ApplyResources((object) this.btGotoAddress, "btGotoAddress");
        this.btGotoAddress.Locked = true;
        this.btGotoAddress.ShowText = true;
        componentResourceManager.ApplyResources((object) this.btClearHistory, "btClearHistory");
        this.btClearHistory.Click += new EventHandler(this.btClearHistory_Click);
        this.btNavigateBack.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btNavigateBack, "btNavigateBack");
        this.btNavigateBack.ShowText = true;
        componentResourceManager.ApplyResources((object) this.btNavigateForward, "btNavigateForward");
        this.btNavigateForward.ShowText = true;
        componentResourceManager.ApplyResources((object) this._dynamicHelpMenuButtonItem, "_dynamicHelpMenuButtonItem");
        this._dynamicHelpMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._documentLayoutMenuButtonItem, "_documentLayoutMenuButtonItem");
        this._documentLayoutMenuButtonItem.ShowText = true;
        this._documentLayoutMenuButtonItem.Visible = false;
        this._documentLayoutMenuButtonItem.Click += new EventHandler(this.DocumentLayoutMenuButtonItem_Click);
        componentResourceManager.ApplyResources((object) this._undoMenuButtonItem, "_undoMenuButtonItem");
        this._undoMenuButtonItem.Shortcut = Shortcut.CtrlZ;
        this._undoMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this._redoMenuButtonItem, "_redoMenuButtonItem");
        this._redoMenuButtonItem.Shortcut = Shortcut.CtrlY;
        this._redoMenuButtonItem.ShowText = true;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add((Control) this._docContainer);
        this.Controls.Add((Control) this.leftDock);
        this.Controls.Add((Control) this.rightDock);
        this.Controls.Add((Control) this.bottomDock);
        this.Controls.Add((Control) this.topDock);
        this.Controls.Add((Control) this.leftBarDock);
        this.Controls.Add((Control) this.rightBarDock);
        this.Controls.Add((Control) this.bottomBarDock);
        this.Controls.Add((Control) this.topBarDock);
        this.Controls.Add((Control) this._statusBar);
        this.IsMdiContainer = true;
        this.KeyPreview = true;
        this.Name = nameof (MainForm);
        this.Activated += new EventHandler(this.MainForm_Activated);
        this.Closed += new EventHandler(this.MainForm_Closed);
        this.Deactivate += new EventHandler(this.MainForm_Deactivate);
        this.FormClosing += new FormClosingEventHandler(this.MainForm_Closing);
        this.Load += new EventHandler(this.MainForm_Load);
        this.Shown += new EventHandler(this.MainForm_Shown);
        this.VisibleChanged += new EventHandler(this.MainForm_VisibleChanged);
        this.Move += new EventHandler(this.MainForm_Move);
        this.statusBarPanel1.EndInit();
        this.sbpIOUserName.EndInit();
        this.sbpUserName.EndInit();
        this.sbpRole.EndInit();
        this.sbpProject.EndInit();
        this.spbLevel.EndInit();
        this._voiceAssistantStatusBarPanel.EndInit();
        this.topBarDock.ResumeLayout(false);
        this.ResumeLayout(false);
      }

      internal static class MainFormConsts
      {
        internal static readonly int MaximumAddresses = 100;
      }
    }
}
