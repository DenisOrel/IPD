// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.DocumentEditorMainForm
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Bars;
using Intermech.Controls;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Document.Editor.PropertyPages;
using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Document.Model.Undo;
using Intermech.Document.UI;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Editor;

public class DocumentEditorMainForm : Form, IImDocumentManager, ICommandTarget, IStandaloneEditor
{
  private MenuButtonItem miAbout;
  private MenuButtonItem miExit;
  private MenuBarItem miWindows;
  private MenuButtonItem miPrevWindow;
  private MenuButtonItem miNextWindow;
  private MenuButtonItem miCloseAllWindows;
  private MenuButtonItem miNewTestDocByTemplate;
  public DocumentMenuHelper menuHelper;
  private MenuBarItem miTable;
  private MenuButtonItem miProperties;
  private MenuButtonItem miExportToWMF;
  private MenuBarItem miPage;
  private MenuButtonItem miCreateFromTemplate;
  private MenuButtonItem menuButtonItem1;
  private MenuButtonItem miConfig;
  private MenuButtonItem miGridSize;
  private MenuButtonItem miPageCoorSystem;
  private MenuButtonItem miSelectAll;
  private MenuButtonItem miUndo;
  private MenuButtonItem miRedo;
  private DocumentControl lastActiveDocumentControl;
  private static string[] args;
  internal static bool FirstInstance;
  public const int WM_COPYDATA = 74;
  private DockManager dockManager;
  private DockContainer leftDock;
  private DockContainer rightDock;
  private DockContainer bottomDock;
  private DockContainer topDock;
  private DocumentContainer documentContainer;
  private BarManager barManager;
  private ToolBarContainer leftBarDock;
  private ToolBarContainer rightBarDock;
  private ToolBarContainer bottomBarDock;
  private ToolBarContainer topBarDock;
  private MenuBar menuBar;
  private MenuBarItem menuBarItem1;
  private MenuBarItem menuBarItem2;
  private MenuBarItem menuBarItem3;
  private MenuBarItem menuBarItem4;
  private MenuBarItem menuBarItem5;
  private MenuBarItem miFile;
  private MenuButtonItem miNewFile;
  private MenuButtonItem miSaveFile;
  private MenuButtonItem miSaveAsFile;
  private MenuButtonItem miTemplate;
  private MenuButtonItem miSaveTemplateAs;
  private MenuButtonItem miSetTemplate;
  private MenuButtonItem miOpenFile;
  private MenuButtonItem miPrintPreview;
  private MenuButtonItem miPrint;
  private MenuBarItem miEdit;
  private MenuButtonItem miRemove;
  private MenuButtonItem miCut;
  private MenuButtonItem miCopy;
  private MenuButtonItem miPaste;
  private MenuBarItem miPageElements;
  private MenuButtonItem miSelect;
  private MenuBarItem miView;
  private MenuButtonItem miShowTemplate;
  private MenuButtonItem miShowDocument;
  private MenuButtonItem miDocumentTreeView;
  private MenuButtonItem miZoom;
  private Intermech.Bars.ToolBar pageElementsToolBar;
  private ImageList imageList;
  private ButtonItem selectElementButton;
  private IContainer components;
  private OpenFileDialog OpenDlg;
  private StatusBar statusBar;
  private StatusBarPanel sbPanelPage;
  private StatusBarPanel sbPanelPageCursorPosition;
  private StatusBarPanel sbMessagePanel;
  private Intermech.Bars.CommandManager commandManager = new Intermech.Bars.CommandManager();
  private ICommandState selectElementCommand;
  protected SaveFileDialog SaveDlg;
  private DockControlLayoutSettings propertyGridSettings = new DockControlLayoutSettings();
  private DockControlLayoutSettings docTreeViewSettings = new DockControlLayoutSettings();
  private DockControlLayoutSettings insertSymbolViewSettings = new DockControlLayoutSettings();
  private ArrayList openedDocuments = new ArrayList();
  private string baseTitle;
  private PropertyGridForm propertyGridDlg;
  private DocumentTreeViewDlg documentTreeViewDlg;
  private InsertSymbolDockControl insertSymbolDlg;
  private int docNameGeneratorCount = 1;
  private bool isElementSelecting = true;
  private bool isElementCreating;
  private ArrayList elementCreators = new ArrayList();
  private ArrayList elementCreatorCommands = new ArrayList();
  private PageElementCreator selectedElementCreator;
  private DocPropertyPagesService docPropertyPagesService;
  private SaveFileDialog saveToFileDialog;
  private string recentlySaveAsPath;

  private void DocumentEditorForm_Load(object sender, EventArgs e)
  {
    this.SaveDlg = ImDocumentEditorFormBase.CreateSaveFileDialog();
    DocumentPlugin.InitDocumentPlugin();
    ImDocumentEditorConfig.Instance.IsClientPluginConfig = false;
    ImDocumentData.ShowExceptionDialog = new ShowExceptionDialogDelegate(this.ShowExceptionDialog);
    this.docPropertyPagesService = new DocPropertyPagesService();
    this.docPropertyPagesService.AddPage(LocalizationHolder.rm.GetString("Document.Editor_5"), (IPropertyPage) ImDocumentEditorConfig.Instance);
    DocumentMenuHelper.DockManager = this.dockManager;
    this.commandManager.AddTarget((ICommandTarget) this);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miExportToWMF
    });
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miNextWindow
    });
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miPrevWindow
    });
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miCloseAllWindows
    });
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miNewTestDocByTemplate
    });
    this.selectElementCommand = this.commandManager.Add(new ButtonItemBase[2]
    {
      (ButtonItemBase) this.miSelect,
      (ButtonItemBase) this.selectElementButton
    });
    Stream manifestResourceStream = typeof (ImDocument).Assembly.GetManifestResourceStream("Intermech.Document.Model.Resources.SelectArrow.bmp");
    if (manifestResourceStream != null)
    {
      Bitmap bitmap = new Bitmap(manifestResourceStream);
      bitmap.MakeTransparent();
      this.imageList.Images.Add((Image) bitmap);
      this.selectElementCommand.ImageIndex = this.imageList.Images.Count - 1;
      this.selectElementButton.ShowText = false;
    }
    else
      this.selectElementButton.ShowText = true;
    this.selectElementCommand.Enabled = true;
    this.selectElementCommand.Checked = true;
    this.LoadPageElementCreators(typeof (ImDocument).Assembly);
    this.MenuHelper = new DocumentMenuHelper((ICommandManager) this.commandManager);
    DocumentMenuHelper.Instance = this.MenuHelper;
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miPrint
    });
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miPrintPreview
    });
    this.AddNodeCommandsToMenu();
    Intermech.Bars.ToolBar tableToolBar = this.MenuHelper.CreateTableToolBar(this.imageList, (ICommandManager) this.commandManager);
    this.barManager.RemoveToolbar(this.pageElementsToolBar);
    this.barManager.AddToolbar(tableToolBar, DockStyle.Top);
    this.barManager.AddToolbar(this.pageElementsToolBar, DockStyle.Top);
    tableToolBar.DockLine = 1;
    this.pageElementsToolBar.DockLine = 1;
    this.barManager.AddToolbar(this.MenuHelper.CreateFormatToolBar(this.imageList, (ICommandManager) this.commandManager), DockStyle.Top);
    this.barManager.AddToolbar(this.MenuHelper.CreateNavigatorToolBar(this.imageList, (ICommandManager) this.commandManager), DockStyle.Top);
    this.LoadConfiguration();
    if (this.dockManager.DocumentContainer.Documents.Length == 0)
      this.NewDocument().FocusDocument();
    if (this.dockManager.DocumentContainer.Documents.Length != 0)
      this.dockManager.DocumentContainer.Documents[0].Select();
    if (this.ActiveImDocumentEditorForm != null)
    {
      this.ActiveImDocumentEditorForm.FocusDocument();
      this.documentContainer.ActiveDocument = (DockControl) this.ActiveImDocumentEditorForm;
    }
    this.commandManager.QueryStatus();
  }

  public DocumentMenuHelper MenuHelper
  {
    get => this.menuHelper;
    set => this.menuHelper = value;
  }

  public DocumentEditorMainForm()
  {
    this.InitializeComponent();
    this.baseTitle = this.Text;
    this.AllowDrop = true;
    this.dockManager.DockControlActivated += new DockControlEventHandler(this.DockManager_DockControlActivated);
    this.dockManager.DockControlDeactivated += new DockControlEventHandler(this.DockManager_DockControlDeactivated);
  }

  private void DockManager_DockControlDeactivated(object sender, DockControlEventArgs e)
  {
    if (((IEnumerable<DockControl>) this.dockManager.DocumentContainer.Documents).Any<DockControl>((Func<DockControl, bool>) (i => i is ImDocumentEditorForm)))
      return;
    this.lastActiveDocumentControl = (DocumentControl) null;
  }

  private void DockManager_DockControlActivated(object sender, DockControlEventArgs e)
  {
    if (this.ActiveDocumentControl != null)
      this.lastActiveDocumentControl = this.ActiveDocumentControl;
    if (this.documentTreeViewDlg == null)
      return;
    this.documentTreeViewDlg.TreeRoot = (DocumentTreeNode) this.ActiveDocumentControl?.Document;
    this.documentTreeViewDlg.DocumentControl = this.ActiveDocumentControl;
    this.documentTreeViewDlg.UpdateSelection();
  }

  [DllImport("user32.dll")]
  private static extern bool SetProcessDPIAware();

  [STAThread]
  private static void Main(string[] args)
  {
    if (Environment.OSVersion.Version.Major >= 6)
      DocumentEditorMainForm.SetProcessDPIAware();
    Mutex mutex = new Mutex(false, "IMMutexUniqueName", out DocumentEditorMainForm.FirstInstance);
    if (DocumentEditorMainForm.FirstInstance || args == null || args.Length == 0)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      IPSFinder.PathToIPSConfig();
      Application.ApplicationExit += new EventHandler(DocumentEditorMainForm.Application_ApplicationExit);
      try
      {
        DocumentEditorMainForm.args = args;
        if (args != null)
        {
          for (int index = 0; index < args.Length; ++index)
          {
            if (args[index] == "/debug" || args[index] == "/d")
              ImDocumentData.ShowDebugInfo = true;
          }
        }
        Application.Run((Form) new DocumentEditorMainForm());
      }
      catch (Exception ex)
      {
        int num = (int) ExceptionForm.ShowExceptionDialog(ex);
      }
    }
    else
    {
      foreach (Process process in Process.GetProcesses())
      {
        if (process.ProcessName.Contains("IMDocumentEditor") && Process.GetCurrentProcess().Id != process.Id)
        {
          DocumentEditorMainForm.COPYDATASTRUCT lParam;
          lParam.dwData = (IntPtr) 100;
          string s = args[0];
          lParam.lpData = s;
          byte[] bytes = Encoding.Default.GetBytes(s);
          lParam.cbData = bytes.Length + 1;
          DocumentEditorMainForm.SendMessage(process.MainWindowHandle, 74, 0, ref lParam);
          break;
        }
      }
    }
  }

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 74)
    {
      System.Type type = new DocumentEditorMainForm.COPYDATASTRUCT().GetType();
      this.OpenDocument(((DocumentEditorMainForm.COPYDATASTRUCT) m.GetLParam(type)).lpData);
    }
    base.WndProc(ref m);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SendMessage(
    IntPtr hWnd,
    int msg,
    int wParam,
    ref DocumentEditorMainForm.COPYDATASTRUCT lParam);

  private static void Application_ApplicationExit(object sender, EventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentEditorMainForm));
    this.OpenDlg = new OpenFileDialog();
    this.statusBar = new StatusBar();
    this.sbMessagePanel = new StatusBarPanel();
    this.sbPanelPageCursorPosition = new StatusBarPanel();
    this.sbPanelPage = new StatusBarPanel();
    this.dockManager = new DockManager();
    this.leftDock = new DockContainer();
    this.rightDock = new DockContainer();
    this.bottomDock = new DockContainer();
    this.topDock = new DockContainer();
    this.documentContainer = new DocumentContainer();
    this.barManager = new BarManager();
    this.leftBarDock = new ToolBarContainer();
    this.rightBarDock = new ToolBarContainer();
    this.bottomBarDock = new ToolBarContainer();
    this.topBarDock = new ToolBarContainer();
    this.menuBar = new MenuBar();
    this.imageList = new ImageList(this.components);
    this.miFile = new MenuBarItem();
    this.miNewFile = new MenuButtonItem();
    this.miCreateFromTemplate = new MenuButtonItem();
    this.miOpenFile = new MenuButtonItem();
    this.miSaveFile = new MenuButtonItem();
    this.miSaveAsFile = new MenuButtonItem();
    this.miTemplate = new MenuButtonItem();
    this.miSaveTemplateAs = new MenuButtonItem();
    this.miSetTemplate = new MenuButtonItem();
    this.miExportToWMF = new MenuButtonItem();
    this.miPrintPreview = new MenuButtonItem();
    this.miPrint = new MenuButtonItem();
    this.miExit = new MenuButtonItem();
    this.miEdit = new MenuBarItem();
    this.miUndo = new MenuButtonItem();
    this.miRedo = new MenuButtonItem();
    this.miCut = new MenuButtonItem();
    this.miCopy = new MenuButtonItem();
    this.miPaste = new MenuButtonItem();
    this.miRemove = new MenuButtonItem();
    this.miSelectAll = new MenuButtonItem();
    this.menuButtonItem1 = new MenuButtonItem();
    this.miPageElements = new MenuBarItem();
    this.miSelect = new MenuButtonItem();
    this.miTable = new MenuBarItem();
    this.miPage = new MenuBarItem();
    this.miWindows = new MenuBarItem();
    this.miPrevWindow = new MenuButtonItem();
    this.miNextWindow = new MenuButtonItem();
    this.miCloseAllWindows = new MenuButtonItem();
    this.miView = new MenuBarItem();
    this.miShowTemplate = new MenuButtonItem();
    this.miShowDocument = new MenuButtonItem();
    this.miProperties = new MenuButtonItem();
    this.miDocumentTreeView = new MenuButtonItem();
    this.miZoom = new MenuButtonItem();
    this.miGridSize = new MenuButtonItem();
    this.miPageCoorSystem = new MenuButtonItem();
    this.miConfig = new MenuButtonItem();
    this.miAbout = new MenuButtonItem();
    this.pageElementsToolBar = new Intermech.Bars.ToolBar();
    this.selectElementButton = new ButtonItem();
    this.menuBarItem1 = new MenuBarItem();
    this.menuBarItem2 = new MenuBarItem();
    this.menuBarItem3 = new MenuBarItem();
    this.menuBarItem4 = new MenuBarItem();
    this.menuBarItem5 = new MenuBarItem();
    this.miNewTestDocByTemplate = new MenuButtonItem();
    this.sbMessagePanel.BeginInit();
    this.sbPanelPageCursorPosition.BeginInit();
    this.sbPanelPage.BeginInit();
    this.topBarDock.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.OpenDlg, "OpenDlg");
    this.OpenDlg.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.statusBar, "statusBar");
    this.statusBar.Name = "statusBar";
    this.statusBar.Panels.AddRange(new StatusBarPanel[3]
    {
      this.sbMessagePanel,
      this.sbPanelPageCursorPosition,
      this.sbPanelPage
    });
    this.statusBar.ShowPanels = true;
    this.sbMessagePanel.AutoSize = StatusBarPanelAutoSize.Spring;
    componentResourceManager.ApplyResources((object) this.sbMessagePanel, "sbMessagePanel");
    componentResourceManager.ApplyResources((object) this.sbPanelPageCursorPosition, "sbPanelPageCursorPosition");
    componentResourceManager.ApplyResources((object) this.sbPanelPage, "sbPanelPage");
    this.dockManager.OwnerForm = (Form) this;
    this.dockManager.DocumentContainer = this.documentContainer;
    componentResourceManager.ApplyResources((object) this.leftDock, "leftDock");
    this.leftDock.Guid = new Guid("340caf08-86c4-4ca6-8558-710eb27e2618");
    this.leftDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.leftDock.Manager = this.dockManager;
    this.leftDock.Name = "leftDock";
    this.leftDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.rightDock, "rightDock");
    this.rightDock.Guid = new Guid("2d34e820-8f62-48dd-8d51-46510e6400cc");
    this.rightDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.rightDock.Manager = this.dockManager;
    this.rightDock.Name = "rightDock";
    this.rightDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
    this.bottomDock.Guid = new Guid("ab1c6c4f-6bc8-4a4d-8047-a6e7cf7c1fff");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.bottomDock.Manager = this.dockManager;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.topDock, "topDock");
    this.topDock.Guid = new Guid("cd0d28f4-d660-4982-bb05-81ceb1a4a961");
    this.topDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.topDock.Manager = this.dockManager;
    this.topDock.Name = "topDock";
    this.topDock.Renderer = (RendererBase) null;
    this.documentContainer.AllowDrop = false;
    this.documentContainer.DockingManager = DockingManager.Whidbey;
    this.documentContainer.Guid = new Guid("777ddc74-5f6b-46df-b383-da5989f92030");
    this.documentContainer.LayoutSystem = new SplitLayoutSystem(250, 400);
    componentResourceManager.ApplyResources((object) this.documentContainer, "documentContainer");
    this.documentContainer.Manager = (DockManager) null;
    this.documentContainer.Name = "documentContainer";
    this.documentContainer.Renderer = (RendererBase) null;
    this.documentContainer.ActiveDocumentChanged += new ActiveDocumentEventHandler(this.documentContainer_ActiveDocumentChanged);
    this.barManager.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.leftBarDock, "leftBarDock");
    this.leftBarDock.Guid = new Guid("c20414d5-5fcb-4834-8c5d-ac5505638bcc");
    this.leftBarDock.Manager = this.barManager;
    this.leftBarDock.Name = "leftBarDock";
    componentResourceManager.ApplyResources((object) this.rightBarDock, "rightBarDock");
    this.rightBarDock.Guid = new Guid("c4121ef5-a40d-4ad9-aec1-239f7aa91014");
    this.rightBarDock.Manager = this.barManager;
    this.rightBarDock.Name = "rightBarDock";
    componentResourceManager.ApplyResources((object) this.bottomBarDock, "bottomBarDock");
    this.bottomBarDock.Guid = new Guid("53b5b590-67ad-4a4d-93e5-27bd3a3869c0");
    this.bottomBarDock.Manager = this.barManager;
    this.bottomBarDock.Name = "bottomBarDock";
    this.topBarDock.Controls.Add((Control) this.menuBar);
    this.topBarDock.Controls.Add((Control) this.pageElementsToolBar);
    componentResourceManager.ApplyResources((object) this.topBarDock, "topBarDock");
    this.topBarDock.Guid = new Guid("9e6c8871-749a-4dc8-a073-51a878b32ca0");
    this.topBarDock.Manager = this.barManager;
    this.topBarDock.Name = "topBarDock";
    this.menuBar.Guid = new Guid("d411a299-5276-4794-8954-50a00b8d6b80");
    this.menuBar.Hidden = false;
    this.menuBar.ImageList = this.imageList;
    this.menuBar.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this.miFile,
      (ToolbarItemBase) this.miEdit,
      (ToolbarItemBase) this.miPageElements,
      (ToolbarItemBase) this.miTable,
      (ToolbarItemBase) this.miPage,
      (ToolbarItemBase) this.miWindows,
      (ToolbarItemBase) this.miView
    });
    componentResourceManager.ApplyResources((object) this.menuBar, "menuBar");
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) this;
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "Symbol-Insert.png");
    componentResourceManager.ApplyResources((object) this.miFile, "miFile");
    this.miFile.Items.AddRange(new ToolbarItemBase[11]
    {
      (ToolbarItemBase) this.miNewFile,
      (ToolbarItemBase) this.miCreateFromTemplate,
      (ToolbarItemBase) this.miOpenFile,
      (ToolbarItemBase) this.miSaveFile,
      (ToolbarItemBase) this.miSaveAsFile,
      (ToolbarItemBase) this.miTemplate,
      (ToolbarItemBase) this.miExportToWMF,
      (ToolbarItemBase) this.miPrintPreview,
      (ToolbarItemBase) this.miPrint,
      (ToolbarItemBase) this.miExit,
      (ToolbarItemBase) this.miNewTestDocByTemplate
    });
    this.miFile.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miNewFile, "miNewFile");
    this.miNewFile.Shortcut = Shortcut.CtrlN;
    this.miNewFile.ShowText = true;
    this.miNewFile.Click += new EventHandler(this.miNewFile_Click);
    componentResourceManager.ApplyResources((object) this.miCreateFromTemplate, "miCreateFromTemplate");
    this.miCreateFromTemplate.ShowText = true;
    this.miCreateFromTemplate.Click += new EventHandler(this.miCreateFromTemplate_Click);
    componentResourceManager.ApplyResources((object) this.miOpenFile, "miOpenFile");
    this.miOpenFile.Shortcut = Shortcut.CtrlO;
    this.miOpenFile.ShowText = true;
    this.miOpenFile.Click += new EventHandler(this.miOpenFile_Click);
    this.miSaveFile.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miSaveFile, "miSaveFile");
    this.miSaveFile.Shortcut = Shortcut.CtrlS;
    this.miSaveFile.ShowText = true;
    this.miSaveFile.Click += new EventHandler(this.miSaveFile_Click);
    componentResourceManager.ApplyResources((object) this.miSaveAsFile, "miSaveAsFile");
    this.miSaveAsFile.ShowText = true;
    this.miSaveAsFile.Click += new EventHandler(this.miSaveAsFile_Click);
    this.miTemplate.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miTemplate, "miTemplate");
    this.miTemplate.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.miSaveTemplateAs,
      (ToolbarItemBase) this.miSetTemplate
    });
    this.miTemplate.ShowText = true;
    this.miTemplate.Click += new EventHandler(this.miTemplate_Click);
    componentResourceManager.ApplyResources((object) this.miSaveTemplateAs, "miSaveTemplateAs");
    this.miSaveTemplateAs.ShowText = true;
    this.miSaveTemplateAs.Click += new EventHandler(this.miSaveTemplateAs_Click);
    componentResourceManager.ApplyResources((object) this.miSetTemplate, "miSetTemplate");
    this.miSetTemplate.ShowText = true;
    this.miSetTemplate.Click += new EventHandler(this.miSetTemplate_Click);
    componentResourceManager.ApplyResources((object) this.miExportToWMF, "miExportToWMF");
    this.miExportToWMF.ShowText = true;
    this.miPrintPreview.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miPrintPreview, "miPrintPreview");
    this.miPrintPreview.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miPrint, "miPrint");
    this.miPrint.Shortcut = Shortcut.CtrlP;
    this.miPrint.ShowText = true;
    this.miExit.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miExit, "miExit");
    this.miExit.ShowText = true;
    this.miExit.Click += new EventHandler(this.miExit_Click);
    componentResourceManager.ApplyResources((object) this.miEdit, "miEdit");
    this.miEdit.Items.AddRange(new ToolbarItemBase[8]
    {
      (ToolbarItemBase) this.miUndo,
      (ToolbarItemBase) this.miRedo,
      (ToolbarItemBase) this.miCut,
      (ToolbarItemBase) this.miCopy,
      (ToolbarItemBase) this.miPaste,
      (ToolbarItemBase) this.miRemove,
      (ToolbarItemBase) this.miSelectAll,
      (ToolbarItemBase) this.menuButtonItem1
    });
    this.miEdit.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miUndo, "miUndo");
    this.miUndo.Shortcut = Shortcut.CtrlZ;
    this.miUndo.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miRedo, "miRedo");
    this.miRedo.Shortcut = Shortcut.CtrlY;
    this.miRedo.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miCut, "miCut");
    this.miCut.Shortcut = Shortcut.CtrlX;
    this.miCut.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miCopy, "miCopy");
    this.miCopy.Shortcut = Shortcut.CtrlC;
    this.miCopy.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miPaste, "miPaste");
    this.miPaste.Shortcut = Shortcut.CtrlV;
    this.miPaste.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miRemove, "miRemove");
    this.miRemove.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miSelectAll, "miSelectAll");
    this.miSelectAll.Shortcut = Shortcut.CtrlA;
    this.miSelectAll.ShowText = true;
    this.miSelectAll.Stretch = true;
    this.menuButtonItem1.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.menuButtonItem1, "menuButtonItem1");
    this.menuButtonItem1.Icon = (Icon) componentResourceManager.GetObject("menuButtonItem1.Icon");
    this.menuButtonItem1.ImageIndex = 0;
    this.menuButtonItem1.ShowText = true;
    this.menuButtonItem1.Click += new EventHandler(this.menuButtonItem1_Click);
    componentResourceManager.ApplyResources((object) this.miPageElements, "miPageElements");
    this.miPageElements.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.miSelect
    });
    this.miPageElements.ShowText = true;
    this.miSelect.Checked = true;
    componentResourceManager.ApplyResources((object) this.miSelect, "miSelect");
    this.miSelect.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miTable, "miTable");
    this.miTable.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miPage, "miPage");
    this.miPage.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miWindows, "miWindows");
    this.miWindows.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.miPrevWindow,
      (ToolbarItemBase) this.miNextWindow,
      (ToolbarItemBase) this.miCloseAllWindows
    });
    this.miWindows.ShowText = true;
    this.miWindows.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.miWindows_BeforePopup);
    componentResourceManager.ApplyResources((object) this.miPrevWindow, "miPrevWindow");
    this.miPrevWindow.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miNextWindow, "miNextWindow");
    this.miNextWindow.ShowText = true;
    this.miCloseAllWindows.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miCloseAllWindows, "miCloseAllWindows");
    this.miCloseAllWindows.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miView, "miView");
    this.miView.Items.AddRange(new ToolbarItemBase[9]
    {
      (ToolbarItemBase) this.miShowTemplate,
      (ToolbarItemBase) this.miShowDocument,
      (ToolbarItemBase) this.miProperties,
      (ToolbarItemBase) this.miDocumentTreeView,
      (ToolbarItemBase) this.miZoom,
      (ToolbarItemBase) this.miGridSize,
      (ToolbarItemBase) this.miPageCoorSystem,
      (ToolbarItemBase) this.miConfig,
      (ToolbarItemBase) this.miAbout
    });
    this.miView.ShowText = true;
    this.miView.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.miView_BeforePopup);
    componentResourceManager.ApplyResources((object) this.miShowTemplate, "miShowTemplate");
    this.miShowTemplate.ShowText = true;
    this.miShowTemplate.Click += new EventHandler(this.miShowTemplate_Click);
    componentResourceManager.ApplyResources((object) this.miShowDocument, "miShowDocument");
    this.miShowDocument.ShowText = true;
    this.miShowDocument.Click += new EventHandler(this.miShowDocument_Click);
    this.miProperties.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miProperties, "miProperties");
    this.miProperties.ShowText = true;
    this.miProperties.Click += new EventHandler(this.miProperties__Click);
    componentResourceManager.ApplyResources((object) this.miDocumentTreeView, "miDocumentTreeView");
    this.miDocumentTreeView.ShowText = true;
    this.miDocumentTreeView.Click += new EventHandler(this.miDocumentTreeView_Click);
    this.miZoom.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miZoom, "miZoom");
    this.miZoom.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miGridSize, "miGridSize");
    this.miGridSize.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miPageCoorSystem, "miPageCoorSystem");
    this.miPageCoorSystem.ShowText = true;
    this.miConfig.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miConfig, "miConfig");
    this.miConfig.ShowText = true;
    this.miConfig.Click += new EventHandler(this.miConfig_Click);
    componentResourceManager.ApplyResources((object) this.miAbout, "miAbout");
    this.miAbout.ShowText = true;
    this.miAbout.Click += new EventHandler(this.miAbout_Click);
    this.pageElementsToolBar.DockLine = 1;
    this.pageElementsToolBar.FullMenus = true;
    this.pageElementsToolBar.Guid = new Guid("6cb8f8f2-0dd1-4f8a-b642-ece847e92228");
    this.pageElementsToolBar.Hidden = false;
    this.pageElementsToolBar.ImageList = this.imageList;
    this.pageElementsToolBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.selectElementButton
    });
    componentResourceManager.ApplyResources((object) this.pageElementsToolBar, "pageElementsToolBar");
    this.pageElementsToolBar.Name = "pageElementsToolBar";
    componentResourceManager.ApplyResources((object) this.selectElementButton, "selectElementButton");
    componentResourceManager.ApplyResources((object) this.menuBarItem1, "menuBarItem1");
    this.menuBarItem1.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuBarItem2, "menuBarItem2");
    this.menuBarItem2.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuBarItem3, "menuBarItem3");
    this.menuBarItem3.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuBarItem4, "menuBarItem4");
    this.menuBarItem4.MdiWindowList = true;
    this.menuBarItem4.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuBarItem5, "menuBarItem5");
    this.menuBarItem5.ShowText = true;
    this.miNewTestDocByTemplate.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miNewTestDocByTemplate, "miNewTestDocByTemplate");
    this.miNewTestDocByTemplate.ShowText = true;
    this.miNewTestDocByTemplate.Visible = false;
    this.miNewTestDocByTemplate.Click += new EventHandler(this.mbNewTestDocByTemplate_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.documentContainer);
    this.Controls.Add((Control) this.leftDock);
    this.Controls.Add((Control) this.rightDock);
    this.Controls.Add((Control) this.bottomDock);
    this.Controls.Add((Control) this.topDock);
    this.Controls.Add((Control) this.statusBar);
    this.Controls.Add((Control) this.leftBarDock);
    this.Controls.Add((Control) this.rightBarDock);
    this.Controls.Add((Control) this.bottomBarDock);
    this.Controls.Add((Control) this.topBarDock);
    this.KeyPreview = true;
    this.Name = nameof (DocumentEditorMainForm);
    this.Tag = (object) " ";
    this.Closing += new CancelEventHandler(this.DocumentEditorForm_Closing);
    this.Closed += new EventHandler(this.DocumentEditorForm_Closed);
    this.Load += new EventHandler(this.DocumentEditorForm_Load);
    this.KeyDown += new KeyEventHandler(this.DocumentEditorForm_KeyDown);
    this.sbMessagePanel.EndInit();
    this.sbPanelPageCursorPosition.EndInit();
    this.sbPanelPage.EndInit();
    this.topBarDock.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public void SetMessageText(string text) => this.sbMessagePanel.Text = text;

  public void UpdateSBPanelPage()
  {
    if (this.InvokeRequired)
      this.Invoke((Delegate) new MethodInvoker(this.UpdateSBPanelPage));
    else if (this.ActiveDocumentControl != null && this.ActiveDocumentControl.Document != null)
    {
      int num1 = -1;
      int num2 = 0;
      ImDocument document = this.ActiveDocumentControl.Document;
      if (document.NodesCount > 0)
      {
        num1 = document.Nodes.IndexOf((DocumentTreeNode) this.ActiveDocumentControl.ActivePage);
        num2 = document.Nodes.Count;
      }
      if (num1 < 0)
        this.sbPanelPage.Text = string.Format(LocalizationHolder.rm.GetString("Document.Editor_6"), (object) "...", (object) num2);
      else
        this.sbPanelPage.Text = string.Format(LocalizationHolder.rm.GetString("Document.Editor_7"), (object) (num1 + 1), (object) num2);
    }
    else
      this.sbPanelPage.Text = LocalizationHolder.rm.GetString("Document.Editor_8");
  }

  public void PageAdded_EventHandler(object sender, EventArgs e) => this.UpdateSBPanelPage();

  public void PageRemoved_EventHandler(object sender, EventArgs e) => this.UpdateSBPanelPage();

  public void documentControl_ActivePageChanged(object sender, EventArgs e)
  {
    this.UpdateSBPanelPage();
  }

  private void DocumentEditorForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (this.ActiveDocumentControl == null)
      return;
    if (e.KeyValue == 33 && e.Control)
    {
      this.ActiveDocumentControl.GotoPrevPage();
    }
    else
    {
      if (e.KeyValue != 34 || !e.Control)
        return;
      this.ActiveDocumentControl.GotoNextPage();
    }
  }

  public ICommandManager CommandManager => (ICommandManager) this.commandManager;

  public MenuBarItem PageElementsMenu => this.miPageElements;

  private void AddDocCommandToMenu(string commandName, MenuItemBase menu)
  {
    MenuButtonItem menuItem = DocumentMenuHelper.GetMenuItem(commandName);
    if (menuItem == null)
      return;
    menu.Items.Add((ToolbarItemBase) menuItem);
  }

  private void AddNodeCommandsToMenu()
  {
    DocumentMenuHelper.CreateMenuCommands((ICommandManager) this.commandManager);
    DocumentMenuHelper.CreateMenuItem("DocElementProperty", LocalizationHolder.rm.GetString("Document.Editor_9"), "", false, true, (ICommandManager) this.commandManager);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miProperties
    });
    MenuButtonItem contextMenuItem1 = NodeContextMenu.GetContextMenuItem("DocElementProperty");
    if (contextMenuItem1 != null)
      contextMenuItem1.BeginGroup = true;
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miCopy
    });
    MenuButtonItem contextMenuItem2 = NodeContextMenu.GetContextMenuItem("Copy");
    if (contextMenuItem2 != null)
      this.miCopy.Image = contextMenuItem2.Image;
    DocumentMenuHelper.SetMenuItem(this.miCopy.CommandName, this.miCopy);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miCut
    });
    MenuButtonItem contextMenuItem3 = NodeContextMenu.GetContextMenuItem("Cut");
    if (contextMenuItem3 != null)
      this.miCut.Image = contextMenuItem3.Image;
    DocumentMenuHelper.SetMenuItem(this.miCut.CommandName, this.miCut);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miPaste
    });
    MenuButtonItem contextMenuItem4 = NodeContextMenu.GetContextMenuItem("Paste");
    if (contextMenuItem4 != null)
      this.miPaste.Image = contextMenuItem4.Image;
    DocumentMenuHelper.SetMenuItem(this.miPaste.CommandName, this.miPaste);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miRemove
    });
    MenuButtonItem contextMenuItem5 = NodeContextMenu.GetContextMenuItem("Delete");
    if (contextMenuItem5 != null)
      this.miRemove.Image = contextMenuItem5.Image;
    DocumentMenuHelper.SetMenuItem(this.miRemove.CommandName, this.miRemove);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miRemove
    });
    DocumentMenuHelper.SetMenuItem(this.miSelectAll.CommandName, this.miSelectAll);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miSelectAll
    });
    MenuButtonItem contextMenuItem6 = NodeContextMenu.GetContextMenuItem("Undo");
    if (contextMenuItem6 != null)
      this.miUndo.Image = contextMenuItem6.Image;
    DocumentMenuHelper.SetMenuItem(this.miUndo.CommandName, this.miUndo);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miUndo
    });
    MenuButtonItem contextMenuItem7 = NodeContextMenu.GetContextMenuItem("Redo");
    if (contextMenuItem7 != null)
      this.miRedo.Image = contextMenuItem7.Image;
    DocumentMenuHelper.SetMenuItem(this.miRedo.CommandName, this.miRedo);
    this.commandManager.Add(new ButtonItemBase[1]
    {
      (ButtonItemBase) this.miRedo
    });
    this.AddDocCommandToMenu("ClearFormat", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("CopySelectedToImage", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("CopySelectedToExcel", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("LoadOleFile", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("CreateOleObject", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("CallEditor", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("CopySelectedToImage", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("CopySelectedToImage", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("CopySelectedToImage", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("Find", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("FindNext", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("Replace", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("FindId", (MenuItemBase) this.miEdit);
    this.AddDocCommandToMenu("RemoveRow", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("RemoveColumn", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("RemoveCell", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("AddTableRowAbove", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("AddTableRowBelow", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("AddRowFromTemplateAbove", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("AddRowFromTemplateBelow", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("AddTableColumnLeft", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("AddTableColumnRight", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("AddTableCell", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("SplitCell", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("MergeCells", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("ConvertToHeader", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("ApplyPreviousTable", (MenuItemBase) this.miTable);
    this.AddDocCommandToMenu("NewPageBefore", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("NewPageAfter", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("CreateNextPageTemplate", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("RemovePage", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("PrevPage", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("NextPage", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("DocEditor.InsertAdditionalPages", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("DocEditor.RemoveAdditionalPages", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("DocEditor.ChangePageNumberingStyle", (MenuItemBase) this.miPage);
    this.AddDocCommandToMenu("Zoom200", (MenuItemBase) this.miZoom);
    this.AddDocCommandToMenu("Zoom100", (MenuItemBase) this.miZoom);
    this.AddDocCommandToMenu("Zoom75", (MenuItemBase) this.miZoom);
    this.AddDocCommandToMenu("Zoom50", (MenuItemBase) this.miZoom);
    this.AddDocCommandToMenu("ZoomFitWidth", (MenuItemBase) this.miZoom);
    this.AddDocCommandToMenu("ZoomFitPage", (MenuItemBase) this.miZoom);
    this.AddDocCommandToMenu("Doc.GridSize_1", (MenuItemBase) this.miGridSize);
    this.AddDocCommandToMenu("Doc.GridSize_0.5", (MenuItemBase) this.miGridSize);
    this.AddDocCommandToMenu("Doc.GridSize_0.1", (MenuItemBase) this.miGridSize);
    this.AddDocCommandToMenu("Doc.GridSize_0.05", (MenuItemBase) this.miGridSize);
    this.AddDocCommandToMenu("Doc.CoorSystem_BottomLeft", (MenuItemBase) this.miPageCoorSystem);
    this.AddDocCommandToMenu("Doc.CoorSystem_TopLeft", (MenuItemBase) this.miPageCoorSystem);
    this.AddDocCommandToMenu("Doc.CoorSystem_TopRight", (MenuItemBase) this.miPageCoorSystem);
    this.AddDocCommandToMenu("Doc.CoorSystem_BottomRight", (MenuItemBase) this.miPageCoorSystem);
    this.AddDocCommandToMenu("Doc.CoorSystem_Custom", (MenuItemBase) this.miPageCoorSystem);
  }

  private void miSaveAsFile_Click(object sender, EventArgs e)
  {
    try
    {
      if (this.ActiveImDocumentEditorForm == null)
        return;
      this.FileSaveAs(this.ActiveImDocumentEditorForm);
    }
    catch (Exception ex)
    {
      int num = (int) ExceptionForm.ShowExceptionDialog(ex);
    }
  }

  private void FileSaveAs(ImDocumentEditorForm docContent)
  {
    this.SaveDlg.FileName = docContent != null ? this.GetDocumentFileName(docContent) : throw new ArgumentNullException(nameof (docContent));
    this.SaveDlg.FileName = Path.GetFileNameWithoutExtension(this.SaveDlg.FileName);
    if (docContent.DocumentsComplect != null)
      this.SaveDlg.Filter = ImDocumentEditorFormBase.ImDocumentsComplectFilter;
    else
      this.SaveDlg.Filter = ImDocumentEditorFormBase.ImDocumentFilter;
    this.SaveDlg.Filter += "|PDF документ (*.pdf)|*.pdf";
    this.SaveDlg.Filter += "|Документ Excel (*.xlsx)|*.xlsx";
    if (this.SaveDlg.ShowDialog() != DialogResult.OK)
      return;
    bool packFile = ImDocumentEditorFormBase.GetSelectedFileFilter(this.SaveDlg.Filter, this.SaveDlg.FilterIndex).IndexOf(".zimd") != -1;
    string fileName = this.SaveDlg.FileName;
    string str1 = new FileInfo(fileName).Extension;
    string selectedFileFilter = ImDocumentEditorFormBase.GetSelectedFileFilter(this.SaveDlg.Filter, this.SaveDlg.FilterIndex);
    string empty = string.Empty;
    if (str1 == empty)
    {
      string str2 = selectedFileFilter.TrimStart('*');
      fileName += str2;
    }
    this.SaveDocument(docContent, fileName, packFile, selectedFileFilter);
  }

  private void SaveTemplateAs(ImDocumentEditorForm docContent)
  {
    if (docContent == null)
      throw new ArgumentNullException(nameof (docContent));
    this.SaveDlg.Filter = ImDocumentEditorFormBase.ImDocumentFilter;
    this.SaveDlg.FileName = this.GetDocumentFileName(docContent);
    if (this.SaveDlg.ShowDialog() != DialogResult.OK)
      return;
    bool packFile = ImDocumentEditorFormBase.GetSelectedFileFilter(this.SaveDlg.Filter, this.SaveDlg.FilterIndex).IndexOf(".zimd") != -1;
    this.SaveTemplate(docContent, this.SaveDlg.FileName, packFile);
  }

  private void miSaveFile_Click(object sender, EventArgs e)
  {
    try
    {
      ImDocumentEditorForm documentEditorForm = this.ActiveImDocumentEditorForm;
      if (documentEditorForm == null)
        return;
      if (documentEditorForm.FileName == null)
        this.FileSaveAs(documentEditorForm);
      else
        this.SaveDocument(documentEditorForm, documentEditorForm.FileName, documentEditorForm.PackedFile, string.Empty);
    }
    catch (Exception ex)
    {
      int num = (int) ExceptionForm.ShowExceptionDialog(ex);
    }
  }

  private void miOpenFile_Click(object sender, EventArgs e)
  {
    try
    {
      if (this.OpenDlg.ShowDialog() != DialogResult.OK)
        return;
      this.OpenDocument(this.OpenDlg.FileName);
    }
    catch (Exception ex)
    {
      int num = (int) ExceptionForm.ShowExceptionDialog(ex);
    }
  }

  private void miProperties__Click(object sender, EventArgs e) => this.ShowPropertyGrid(true);

  private void miNewFile_Click(object sender, EventArgs e) => this.NewDocument();

  private void miShowTemplate_Click(object sender, EventArgs e)
  {
    try
    {
      ImDocumentEditorForm documentEditorForm = this.ActiveImDocumentEditorForm;
      if (documentEditorForm == null || documentEditorForm.Document.IsTemplate)
        return;
      ImDocument documentTemplate = documentEditorForm.Document.DocumentTemplate as ImDocument;
      documentTemplate.SetNeedUIRecursive(true, true);
      if (documentTemplate.DocumentControl == null)
        documentTemplate.CreateUI();
      if (!(documentEditorForm.InternalDocumentTemplateWindow is ImDocumentEditorForm docContent))
      {
        docContent = new ImDocumentEditorForm((IImDocumentManager) this, documentTemplate.DocumentControl, false);
        this.SetupNewDocumentContent((ImDocumentEditorFormBase) docContent);
        docContent.FileName = documentEditorForm.FileName;
        docContent.DefaultFileName = documentEditorForm.DefaultFileName;
      }
      docContent.Show(this.dockManager, DockState.Document);
      this.UpdateDocumentCaption(docContent, false, (string) null, (string) null);
      docContent.Select();
      docContent.FocusDocument();
    }
    catch (Exception ex)
    {
      int num = (int) ExceptionForm.ShowExceptionDialog(ex);
    }
  }

  private void miShowDocument_Click(object sender, EventArgs e)
  {
    ImDocumentEditorForm documentEditorForm = this.ActiveImDocumentEditorForm;
    if (documentEditorForm == null || !(documentEditorForm.Document.TemplateOwner is ImDocument templateOwner))
      return;
    if (templateOwner.DocumentControl == null)
      templateOwner.SetNeedUIRecursive(true, true);
    if (!(templateOwner.DocumentControl.Parent is ImDocumentEditorForm docContent))
    {
      docContent = new ImDocumentEditorForm((IImDocumentManager) this, templateOwner.DocumentControl, false);
      docContent.FileName = documentEditorForm.FileName;
      docContent.DefaultFileName = documentEditorForm.DefaultFileName;
      this.SetupNewDocumentContent((ImDocumentEditorFormBase) docContent);
    }
    docContent.Show(this.dockManager, DockState.Document);
    this.UpdateDocumentCaption(docContent, false, (string) null, (string) null);
    docContent.Select();
    docContent.FocusDocument();
  }

  private void miDocumentTreeView_Click(object sender, EventArgs e)
  {
    this.ShowDocumentTreeView(true);
  }

  private void miSaveTemplateAs_Click(object sender, EventArgs e)
  {
    if (this.ActiveImDocumentEditorForm == null)
      return;
    this.SaveTemplateAs(this.ActiveImDocumentEditorForm);
  }

  private void miSetTemplate_Click(object sender, EventArgs e)
  {
    if (this.OpenDlg.ShowDialog() != DialogResult.OK)
      return;
    try
    {
      this.LoadDocumentTemplate(this.ActiveImDocumentEditorForm, this.OpenDlg.FileName);
    }
    catch (Exception ex)
    {
      int num = (int) ExceptionForm.ShowExceptionDialog(ex);
    }
  }

  private void miView_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (this.ActiveDocumentControl != null)
    {
      this.miShowTemplate.Visible = true;
      this.miShowDocument.Visible = true;
    }
    else
    {
      this.miShowTemplate.Visible = false;
      this.miShowDocument.Visible = false;
    }
  }

  private void miTemplate_Click(object sender, EventArgs e)
  {
    this.miSetTemplate.Enabled = this.ActiveImDocumentEditorForm != null && (!this.ActiveImDocumentEditorForm.Document.IsTemplate || this.ActiveImDocumentEditorForm.Document.TemplateOwner != null);
  }

  public void ShowPropertyGrid(bool show)
  {
    PropertyGridForm propertyGridDlg = this.propertyGridDlg;
    if (this.propertyGridDlg == null || this.propertyGridDlg.IsDisposed)
    {
      this.propertyGridDlg = new PropertyGridForm();
      this.propertyGridDlg.Closed += new EventHandler(this.propertyGridDlg_Closed);
    }
    this.SelectionChanged();
    if (!(this.propertyGridDlg != null & show))
      return;
    this.propertyGridSettings.Open((DockControl) this.propertyGridDlg, this.dockManager);
  }

  private void propertyGridDlg_Closed(object sender, EventArgs e)
  {
    this.UpdatePropertiesCommandState();
  }

  private void UpdatePropertiesCommandState()
  {
    ICommandState command = this.commandManager.FindCommand("DocElementProperty");
    if (command == null)
      return;
    this.QueryStatus(command);
  }

  protected DocumentTreeViewDlg DocumentTreeViewDlg
  {
    get
    {
      return this.documentTreeViewDlg != null && !this.documentTreeViewDlg.IsDisposed ? this.documentTreeViewDlg : (DocumentTreeViewDlg) null;
    }
  }

  protected InsertSymbolDockControl InsertSymbolDockControl
  {
    get
    {
      return this.insertSymbolDlg != null && !this.insertSymbolDlg.IsDisposed ? this.insertSymbolDlg : (InsertSymbolDockControl) null;
    }
  }

  public void ShowDocumentTreeView(bool show)
  {
    if (this.DocumentTreeViewDlg == null)
    {
      this.documentTreeViewDlg = new DocumentTreeViewDlg();
      this.documentTreeViewDlg.Text = LocalizationHolder.rm.GetString("Document.Editor_11");
    }
    if (this.ActiveDocumentControl != null)
    {
      this.documentTreeViewDlg.TreeRoot = (DocumentTreeNode) this.ActiveDocumentControl.Document;
      this.documentTreeViewDlg.DocumentControl = this.ActiveDocumentControl;
      this.documentTreeViewDlg.UpdateSelection();
    }
    if (!show)
      return;
    this.docTreeViewSettings.Open((DockControl) this.documentTreeViewDlg, this.dockManager);
  }

  public void ShowInsertSymbolDlg(bool show)
  {
    InsertSymbolDockControl insertSymbolDlg = this.insertSymbolDlg;
    if (this.insertSymbolDlg == null)
    {
      this.insertSymbolDlg = new InsertSymbolDockControl();
      this.insertSymbolDlg.Text = LocalizationHolder.rm.GetString("Document.Editor_12");
    }
    this.insertSymbolDlg.DocumentControl = this.ActiveDocumentControl;
    if (!show)
      return;
    this.insertSymbolViewSettings.Open((DockControl) this.insertSymbolDlg, this.dockManager);
  }

  private void characterMap1_OnCharSelected(object source, CharacterMap.CharacterMapEventArgs e)
  {
    if (e == null || this.ActiveDocumentControl == null)
      return;
    this.ActiveDocumentControl.GetActiveEditorControl();
  }

  public ImDocumentEditorForm NewDocument()
  {
    ImDocumentEditorForm doc = new ImDocumentEditorForm((IImDocumentManager) this, true, true);
    this.OpenDocument(doc);
    return doc;
  }

  public void OpenDocument(ImDocumentEditorForm doc)
  {
    doc.DefaultFileName = doc.DocumentCaption + (object) this.docNameGeneratorCount++ + doc.DefaultFileExtension;
    this.SetupNewDocumentContent((ImDocumentEditorFormBase) doc);
    doc.Show(this.dockManager, DockState.Document);
    this.UpdateDocumentCaption(doc, false, (string) null, (string) null);
    doc.Select();
    this.UpdateSBPanelPage();
  }

  private bool DocReadOnly(string fileName)
  {
    FileInfo fileInfo = new FileInfo(fileName);
    return fileInfo.Exists && fileInfo.IsReadOnly;
  }

  public void OpenDocument(string fileName)
  {
    ImDocumentEditorForm docContent = new ImDocumentEditorForm((IImDocumentManager) this, true, false);
    DocumentFileType docType;
    DocumentTreeNode documentTreeNode = ImDocument.LoadFromFile(fileName, out docType, true);
    docContent.ReadOnly = this.DocReadOnly(fileName);
    switch (documentTreeNode)
    {
      case ImDocument imDocument:
        docContent.DocumentControl.Document = imDocument;
        break;
      case DocumentsComplect documentsComplect:
        docContent.DocumentControl.DocumentsComplect = documentsComplect;
        break;
    }
    docContent.DocFileType = docType;
    if (docType == DocumentFileType.OldBlank || docType == DocumentFileType.OldUEditDocument || docType == DocumentFileType.OldPrimitiveLib)
      docContent.DefaultFileName = Path.GetFileNameWithoutExtension(fileName);
    else
      docContent.FileName = fileName;
    this.SetupNewDocumentContent((ImDocumentEditorFormBase) docContent);
    docContent.Show(this.dockManager, DockState.Document);
    this.UpdateDocumentCaption(docContent, false, (string) null, (string) null);
    this.UpdateSBPanelPage();
    docContent.Select();
  }

  public void LoadDocumentTemplate(ImDocumentEditorForm docContent, string fileName)
  {
    ImDocument imDocument1 = this.RootDocument(docContent.Document);
    ImDocument imDocument2 = ImDocument.LoadFromXml(fileName, true, false);
    if (imDocument2 != null && !imDocument2.IsTemplate)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Document.Editor_13"), (object) fileName));
    ImDocument documentTemplate = imDocument1.DocumentTemplate as ImDocument;
    imDocument1.AssignDocumentTemplate((ImDocumentData) imDocument2, true, true, true);
    for (int index = 0; index < this.dockManager.DocumentContainer.Documents.Length; ++index)
    {
      if (this.dockManager.DocumentContainer.Documents[index] is ImDocumentEditorForm document && document.Document == documentTemplate)
        document.DocumentControl.Document = imDocument2;
    }
    if (this.ActiveImDocumentEditorForm == null || this.ActiveImDocumentEditorForm.Document != imDocument2)
      return;
    if (this.DocumentTreeViewDlg != null)
    {
      this.documentTreeViewDlg.TreeRoot = (DocumentTreeNode) this.ActiveDocumentControl.Document;
      this.documentTreeViewDlg.DocumentControl = this.ActiveDocumentControl;
    }
    if (this.insertSymbolDlg != null)
      this.insertSymbolDlg.DocumentControl = this.ActiveDocumentControl;
    this.ActiveImDocumentEditorForm.DocumentControl.SetActiveElement((DocumentTreeNode) null, false, Point.Empty);
  }

  protected override void OnDragOver(DragEventArgs drgevent)
  {
    if (drgevent.Data.GetDataPresent("FileDrop"))
      drgevent.Effect = DragDropEffects.Copy;
    else
      base.OnDragOver(drgevent);
  }

  protected override void OnDragDrop(DragEventArgs drgevent)
  {
    try
    {
      foreach (string fileName in (string[]) drgevent.Data.GetData("FileDrop"))
        this.OpenDocument(fileName);
    }
    catch (Exception ex)
    {
      if (ImDocumentData.ShowExceptionDialog != null)
      {
        ImDocumentData.ShowExceptionDialog(ex);
      }
      else
      {
        int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, LocalizationHolder.rm.GetString("Document.Model_617"));
      }
    }
  }

  private void SaveDocument(
    ImDocumentEditorForm docContent,
    string fileName,
    bool packFile,
    string filter)
  {
    if (docContent == null)
      throw new ArgumentNullException(nameof (docContent));
    docContent.Document?.DocumentControl?.EditorValidating();
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    if (filter.ToLower().Contains("pdf"))
      docContent.SaveAsPdf(fileName);
    else if (filter.ToLower().Contains("xlsx"))
    {
      docContent.SaveAsXLS(fileName);
    }
    else
    {
      if (docContent.DocumentsComplect != null)
      {
        docContent.FileName = fileName;
        docContent.DocFileType = packFile ? DocumentFileType.ImDocumentsComplect_IsPacked : DocumentFileType.ImDocumentsComplect;
        docContent.DocumentsComplect.SaveToXml(fileName, packFile);
      }
      else if (docContent.Document != null)
      {
        if (docContent.Document.IsTemplate && docContent.Document.TemplateOwner != null)
          docContent.Document.TemplateOwner.SaveToXml(fileName, packFile);
        else
          docContent.Document.SaveToXml(fileName, packFile);
        docContent.FileName = fileName;
        docContent.DocFileType = packFile ? DocumentFileType.ImDocument_IsPacked : DocumentFileType.ImDocument;
      }
      this.UpdateDocumentCaption(docContent, true, fileName, (string) null);
    }
  }

  private void SaveTemplate(ImDocumentEditorForm docContent, string fileName, bool packFile)
  {
    if (docContent == null)
      throw new ArgumentNullException(nameof (docContent));
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    if (docContent.Document.IsTemplate)
    {
      docContent.Document.SaveToXml(fileName, packFile);
      if (docContent.Document.TemplateOwner != null)
        return;
      this.UpdateDocumentCaption(docContent, true, docContent.FileName, docContent.DefaultFileName);
    }
    else
    {
      if (docContent.Document.Template == null)
        return;
      docContent.Document.DocumentTemplate.SaveToXml(fileName, packFile);
    }
  }

  private void SetupNewDocumentContent(ImDocumentEditorFormBase docContent)
  {
    if (docContent == null || docContent.DocumentControl == null)
      return;
    docContent.DocumentControl.DocumentManager = (IImDocumentManager) this;
    docContent.DocumentControl.PageAdded += new Intermech.Document.UI.PageAdded_EventHandler(this.PageAdded_EventHandler);
    docContent.DocumentControl.PageRemoved += new Intermech.Document.UI.PageRemoved_EventHandler(this.PageRemoved_EventHandler);
    docContent.DocumentControl.ActivePageChanged += new ActivePageChanged_EventHandler(this.documentControl_ActivePageChanged);
    docContent.DocumentControl.PageCursorPositionChanged += new PageCursorPositionChanged_EventHandler(this.DocumentControl_PageCursorPositionChanged);
    docContent.DocumentControl.GetCustomElementContextMenu += new GetCustomElementContextMenu_EventHandler(this.DocumentControl_GetCustomElementContextMenu);
    docContent.DocumentControl.DocumentModifiedChanged += new ModifiedChanged_EventHandler(this.DocumentModifiedChanged_Handler);
    docContent.DocumentControl.GotoFirstPage();
    docContent.Closing += new CancelEventHandler(this.DocumentContent_Closing);
    docContent.Closed += new EventHandler(this.DocumentContent_Closed);
  }

  private void DocumentContent_Closed(object sender, EventArgs e)
  {
    if (!(sender is ImDocumentEditorForm documentEditorForm) || documentEditorForm.DocumentControl == null)
      return;
    documentEditorForm.DocumentControl.DocumentManager = (IImDocumentManager) null;
    documentEditorForm.DocumentControl.PageAdded -= new Intermech.Document.UI.PageAdded_EventHandler(this.PageAdded_EventHandler);
    documentEditorForm.DocumentControl.PageRemoved -= new Intermech.Document.UI.PageRemoved_EventHandler(this.PageRemoved_EventHandler);
    documentEditorForm.DocumentControl.ActivePageChanged -= new ActivePageChanged_EventHandler(this.documentControl_ActivePageChanged);
    documentEditorForm.DocumentControl.PageCursorPositionChanged -= new PageCursorPositionChanged_EventHandler(this.DocumentControl_PageCursorPositionChanged);
    documentEditorForm.DocumentControl.GetCustomElementContextMenu -= new GetCustomElementContextMenu_EventHandler(this.DocumentControl_GetCustomElementContextMenu);
    documentEditorForm.DocumentControl.DocumentModifiedChanged -= new ModifiedChanged_EventHandler(this.DocumentModifiedChanged_Handler);
    documentEditorForm.DocumentControl.ActivePage = (Page) null;
    documentEditorForm.DocumentControl.SetSelection(new List<DocumentTreeNode>(0), false, false);
    documentEditorForm.Closing -= new CancelEventHandler(this.DocumentContent_Closing);
    documentEditorForm.Closed -= new EventHandler(this.DocumentContent_Closed);
    if (documentEditorForm.InternalDocumentTemplateWindow == null)
      return;
    documentEditorForm.InternalDocumentTemplateWindow.Close();
  }

  private void DocumentControl_GetCustomElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
    MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem("DocElementProperty");
    if (contextMenuItem == null)
      return;
    this.UpdatePropertiesCommandState();
    e.ContextMenuItems.Add(contextMenuItem);
  }

  private void cmiShowProperties_Click(object sender, EventArgs e)
  {
    this.ShowPropertyGrid(true);
    if (NodeContextMenu.ContextForContextMenu == null || NodeContextMenu.ContextForContextMenu.Length == 0)
      return;
    object[] objArray = new object[NodeContextMenu.ContextForContextMenu.Length];
    NodeContextMenu.ContextForContextMenu.CopyTo((Array) objArray, 0);
    this.propertyGridDlg.SelectedObjects = objArray;
  }

  private void DocumentContent_Closing(object sender, CancelEventArgs e)
  {
    ImDocumentEditorForm docWin = sender as ImDocumentEditorForm;
    ImDocument document = docWin?.Document;
    if (document == null || !document.Modified || docWin.ReadOnly || document.TemplateOwner != null || this.dockManager.DocumentContainer.Documents.OfType<ImDocumentEditorForm>().Any<ImDocumentEditorForm>((Func<ImDocumentEditorForm, bool>) (w => w != docWin && w.Document == document)))
      return;
    string documentFileName = this.GetDocumentFileName(docWin);
    switch (MessageBox.Show((IWin32Window) this, $"{LocalizationHolder.rm.GetString("Document.Editor_15")}{documentFileName}\"", this.baseTitle, MessageBoxButtons.YesNoCancel))
    {
      case DialogResult.Cancel:
        e.Cancel = true;
        break;
      case DialogResult.Yes:
        if (docWin.FileName == null)
        {
          this.FileSaveAs(docWin);
          break;
        }
        this.SaveDocument(docWin, docWin.FileName, docWin.PackedFile, string.Empty);
        break;
    }
  }

  private void documentContainer_ActiveDocumentChanged(object sender, ActiveDocumentEventArgs e)
  {
    if (this.ActiveDocumentControl != null)
    {
      DocumentTreeNode activeElement = this.ActiveDocumentControl.ActiveElement;
      if (this.DocumentTreeViewDlg != null)
      {
        this.documentTreeViewDlg.TreeRoot = (DocumentTreeNode) this.ActiveDocumentControl.Document;
        this.documentTreeViewDlg.DocumentControl = this.ActiveDocumentControl;
        this.documentTreeViewDlg.UpdateSelection();
      }
      if (this.insertSymbolDlg != null)
        this.insertSymbolDlg.DocumentControl = this.ActiveDocumentControl;
      if (this.propertyGridDlg != null)
        this.propertyGridDlg.SelectedObject = (object) activeElement;
    }
    else
    {
      if (this.DocumentTreeViewDlg != null)
      {
        this.documentTreeViewDlg.TreeRoot = (DocumentTreeNode) null;
        this.documentTreeViewDlg.DocumentControl = (DocumentControl) null;
        this.documentTreeViewDlg.UpdateSelection();
      }
      if (this.insertSymbolDlg != null)
        this.insertSymbolDlg.DocumentControl = (DocumentControl) null;
      if (this.propertyGridDlg != null)
        this.propertyGridDlg.SelectedObject = (object) null;
    }
    this.PartTitle = this.dockManager == null || this.dockManager.ActiveDocument == null ? "" : this.dockManager.ActiveDocument.Text;
    this.UpdateSBPanelPage();
    this.commandManager.ActiveTarget = e.NewActiveDocument as ICommandTarget;
  }

  private void DocumentEditorForm_Closing(object sender, CancelEventArgs e)
  {
    foreach (ImDocumentEditorForm docContent in this.dockManager.DocumentContainer.Documents.OfType<ImDocumentEditorForm>())
    {
      ImDocument document = docContent?.Document;
      if (document != null && document.Modified && !docContent.ReadOnly && document.TemplateOwner == null && this.dockManager.DocumentContainer.Documents.OfType<ImDocumentEditorForm>().FirstOrDefault<ImDocumentEditorForm>((Func<ImDocumentEditorForm, bool>) (w => w.Document == document)) == docContent)
      {
        string documentFileName = this.GetDocumentFileName(docContent);
        switch (MessageBox.Show((IWin32Window) this, $"{LocalizationHolder.rm.GetString("Document.Editor_16")}{documentFileName}\"", this.baseTitle, MessageBoxButtons.YesNoCancel))
        {
          case DialogResult.Cancel:
            e.Cancel = true;
            return;
          case DialogResult.Yes:
            if (docContent.FileName == null)
            {
              this.FileSaveAs(docContent);
              continue;
            }
            this.SaveDocument(docContent, docContent.FileName, docContent.PackedFile, string.Empty);
            continue;
          default:
            continue;
        }
      }
    }
  }

  private DockControl FindContent(string text)
  {
    foreach (DockControl document in this.dockManager.DocumentContainer.Documents)
    {
      if (document.Text == text)
        return document;
    }
    return (DockControl) null;
  }

  private DocumentControl ActiveDocumentControl
  {
    get
    {
      if (this.documentContainer.ActiveDocument != null && this.documentContainer.ActiveDocument is ImDocumentEditorForm)
        return (this.documentContainer.ActiveDocument as ImDocumentEditorForm).DocumentControl;
      if (this.dockManager.ActiveDocument != null && this.dockManager.ActiveDocument is ImDocumentEditorForm)
        return (this.dockManager.ActiveDocument as ImDocumentEditorForm).DocumentControl;
      return ((IEnumerable<DockControl>) this.dockManager.DocumentContainer.Documents).Any<DockControl>((Func<DockControl, bool>) (d => d is ImDocumentEditorForm)) ? this.lastActiveDocumentControl : (DocumentControl) null;
    }
  }

  private ImDocumentEditorForm ActiveImDocumentEditorForm
  {
    get
    {
      return this.documentContainer.ActiveDocument != null && this.documentContainer.ActiveDocument is ImDocumentEditorForm ? this.documentContainer.ActiveDocument as ImDocumentEditorForm : this.dockManager.ActiveDocument as ImDocumentEditorForm;
    }
  }

  private ImDocument RootDocument(ImDocument document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    return document.IsTemplate && document.TemplateOwner != null ? document.TemplateOwner as ImDocument : document;
  }

  private string GetDocumentFileName(ImDocumentEditorForm docContent)
  {
    string fileName = docContent.FileName;
    return fileName == null || !(fileName != "") ? docContent.DefaultFileName : new FileInfo(fileName).Name;
  }

  private string ModifiedSign(bool modified) => modified ? "*" : "";

  private void UpdateDocumentCaption(
    ImDocumentEditorForm docContent,
    bool setFileName,
    string fileName,
    string defaultFileName)
  {
    this.UpdateDocumentCaption(docContent.Document, setFileName, fileName, defaultFileName);
  }

  private void UpdateDocumentCaption(
    ImDocument document,
    bool setFileName,
    string fileName,
    string defaultFileName)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (document.DocumentControl != null && document.DocumentControl.DocumentEditorForm != null && document.DocumentControl.DocumentEditorForm.UndoManager == null)
      document.DocumentControl.DocumentEditorForm.UndoManager = (IUndoManager) new UndoManager(document.DocumentControl.DocumentEditorForm);
    if (document.IsTemplate && document.TemplateOwner != null)
      document = document.TemplateOwner as ImDocument;
    ImDocument imDocument = document.DocumentTemplate as ImDocument;
    if (document.IsTemplate)
    {
      imDocument = document;
      document = imDocument.TemplateOwner as ImDocument;
    }
    for (int index = 0; index < this.dockManager.DocumentContainer.Documents.Length; ++index)
    {
      if (this.dockManager.DocumentContainer.Documents[index] is ImDocumentEditorForm document1)
      {
        if (document1.Document == imDocument)
        {
          if (setFileName)
          {
            document1.FileName = fileName;
            document1.DefaultFileName = defaultFileName;
          }
          if (document != null)
            document1.Text = document1.DocumentCaption + LocalizationHolder.rm.GetString("Document.Editor_17") + this.ModifiedSign(document.Modified && !document1.ReadOnly);
          else
            document1.Text = document1.DocumentCaption + LocalizationHolder.rm.GetString("Document.Editor_18") + this.ModifiedSign(imDocument.Modified && !document1.ReadOnly);
        }
        else if (document1.Document == document)
        {
          if (setFileName)
          {
            document1.FileName = fileName;
            document1.DefaultFileName = defaultFileName;
          }
          document1.Text = document1.DocumentCaption + this.ModifiedSign(document.Modified && !document1.ReadOnly);
        }
      }
    }
    this.PartTitle = this.dockManager.ActiveDocument?.Text ?? "";
  }

  private void DocumentModifiedChanged_Handler(object sender, ModifiedChanged_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new ModifiedChanged_EventHandler(this.DocumentModifiedChanged_Handler), sender, (object) e);
    }
    else
    {
      if (!(sender is ImDocument document))
        return;
      this.UpdateDocumentCaption(document, false, (string) null, (string) null);
    }
  }

  protected string PartTitle
  {
    set
    {
      if (value != null && value != "")
        this.Text = $"{this.baseTitle} - {value}";
      else
        this.Text = this.baseTitle;
    }
  }

  public void UpdateSelectedElementInfo()
  {
    if (this.propertyGridDlg == null)
      return;
    this.propertyGridDlg.SelectedObject = this.propertyGridDlg.SelectedObject;
    this.propertyGridDlg.Refresh();
  }

  public void UpdatePagesInfo() => this.UpdateSBPanelPage();

  private ImDocumentEditorForm OpenNewDocumentByTemplate()
  {
    if (this.ActiveImDocumentEditorForm == null)
      return (ImDocumentEditorForm) null;
    ImDocument baseDocTemplate = this.ActiveImDocumentEditorForm.Document.IsTemplate ? this.ActiveImDocumentEditorForm.Document : this.ActiveImDocumentEditorForm.Document.DocumentTemplate as ImDocument;
    if (baseDocTemplate == null)
      return (ImDocumentEditorForm) null;
    ImDocument documentByTemplate = this.GenerateImDocumentByTemplate(baseDocTemplate, "WorkSpace", "Row");
    ImDocumentEditorForm doc = new ImDocumentEditorForm((IImDocumentManager) this, documentByTemplate, false);
    documentByTemplate.UpdateLayout(true);
    this.OpenDocument(doc);
    return doc;
  }

  private ImDocument GenerateImDocumentByTemplate(
    ImDocument baseDocTemplate,
    string mainTableID,
    string mainTableRowID,
    string internalRowID = null)
  {
    ImDocument documentByTemplate = new ImDocument(baseDocTemplate, true, true);
    ImDocument documentTemplate = documentByTemplate.DocumentTemplate as ImDocument;
    TableData node1 = documentByTemplate.FindNode(mainTableID) as TableData;
    TableData node2 = documentTemplate.FindNode(mainTableRowID) as TableData;
    TableData tableData1 = (TableData) null;
    TableData nodeTemplate = (TableData) null;
    if (!string.IsNullOrWhiteSpace(internalRowID))
    {
      tableData1 = documentTemplate.FindNode(internalRowID) as TableData;
      nodeTemplate = tableData1.ParentCell;
    }
    for (int index1 = 1; index1 < 40; ++index1)
    {
      TableData child = node2.CloneFromTemplate() as TableData;
      if (tableData1 != null)
      {
        TableData templateRecursive = child.FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) nodeTemplate) as TableData;
        for (int index2 = 0; index2 < 2; ++index2)
        {
          TableData tableData2 = tableData1.CloneFromTemplate() as TableData;
          foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(tableData2))
            textData.AssignText($"{"in"}: {index1}.{index2}", false, false, false);
          templateRecursive?.AddChildNode((DocumentTreeNode) tableData2, false, false);
        }
      }
      for (int index3 = 0; index3 < child.Nodes.Count; ++index3)
      {
        if (index3 != 0 || !(mainTableID == "Таблица Ведомость покупных"))
        {
          if (child.Nodes[index3] is TextData node5)
          {
            string str = $"{index3}: {index1}";
            node5.AssignText(str, false, false, false);
          }
          else if (child.Nodes[index3] is TableData node4)
          {
            for (int index4 = 0; index4 < node4.Nodes.Count; ++index4)
            {
              if (node4.Nodes[index4] is TableData node3)
              {
                foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(node3))
                  textData.AssignText(textData.Text + "*", false, false, false);
              }
            }
          }
        }
      }
      TextData node6 = child.Nodes[0] as TextData;
      node6.AssignBackColor(Color.Aqua, false);
      node6.IsOverridden(OverrideFlags.BackColor);
      node1.AddChildNode((DocumentTreeNode) child, false, false);
    }
    return documentByTemplate;
  }

  public bool IsElementSelecting
  {
    get => this.isElementSelecting;
    set
    {
      if (this.isElementSelecting == value)
        return;
      this.isElementSelecting = value;
      this.selectElementCommand.Checked = value;
      this.IsElementCreating = !value;
    }
  }

  public bool IsElementCreating
  {
    get => this.isElementCreating;
    set
    {
      if (this.isElementCreating == value)
        return;
      if (this.ActiveDocumentControl != null && this.ActiveDocumentControl.Document != null && this.ActiveDocumentControl.Document.UndoManager != null)
      {
        if (value)
          this.ActiveDocumentControl.Document.UndoManager.LockUndo();
        else
          this.ActiveDocumentControl.Document.UndoManager.UnlockUndo();
      }
      this.isElementCreating = value;
      this.IsElementSelecting = !value;
      if (this.isElementCreating)
        return;
      this.SelectedElementCreator = (PageElementCreator) null;
    }
  }

  public void AddPageElementCreator(PageElementCreator elementCreator)
  {
    ButtonItem buttonItem = new ButtonItem();
    buttonItem.CommandName = elementCreator.Name;
    if (elementCreator.Image == null)
      buttonItem.ShowText = true;
    this.pageElementsToolBar.Items.Add((ToolbarItemBase) buttonItem);
    MenuButtonItem menuButtonItem = new MenuButtonItem();
    menuButtonItem.CommandName = elementCreator.Name;
    this.PageElementsMenu.Items.Add((ToolbarItemBase) menuButtonItem);
    ICommandState commandState = this.commandManager.Add(new ButtonItemBase[2]
    {
      (ButtonItemBase) menuButtonItem,
      (ButtonItemBase) buttonItem
    });
    commandState.Text = elementCreator.Name;
    commandState.Enabled = true;
    if (elementCreator.Image != null)
    {
      this.imageList.Images.Add(elementCreator.Image);
      commandState.ImageIndex = this.imageList.Images.Count - 1;
    }
    commandState.ToolTipText = elementCreator.Name;
    this.elementCreators.Add((object) elementCreator);
    this.elementCreatorCommands.Add((object) commandState);
  }

  public void RemovePageElementCreator(PageElementCreator elementCreator)
  {
    int index = this.elementCreators.IndexOf((object) elementCreator);
    this.elementCreators.RemoveAt(index);
    ICommandState elementCreatorCommand = this.elementCreatorCommands[index] as ICommandState;
    elementCreatorCommand.Visible = false;
    elementCreatorCommand.Enabled = false;
    this.elementCreatorCommands.RemoveAt(index);
  }

  public PageElementCreator SelectedElementCreator
  {
    [DebuggerStepThrough] get => this.selectedElementCreator;
    set
    {
      if (this.selectedElementCreator == value)
        return;
      if (this.selectedElementCreator != null)
        this.selectedElementCreator.Reset();
      this.selectedElementCreator = value;
      this.IsElementCreating = this.selectedElementCreator != null;
      for (int index = 0; index < this.elementCreatorCommands.Count; ++index)
        (this.elementCreatorCommands[index] as ICommandState).Checked = this.selectedElementCreator != null && this.elementCreators[index] == this.selectedElementCreator;
    }
  }

  public void LoadPageElementCreators(Assembly assembly)
  {
    System.Type[] types = assembly.GetTypes();
    System.Type c = typeof (PageElementCreator);
    List<System.Type> typeList1 = new List<System.Type>();
    foreach (System.Type type in types)
    {
      if (type.IsSubclassOf(c) && !type.IsAbstract)
        typeList1.Add(type);
    }
    List<System.Type> typeList2 = new List<System.Type>();
    if (typeList1.Contains(typeof (TextBoxCreator)))
      typeList2.Add(typeof (TextBoxCreator));
    if (typeList1.Contains(typeof (LabelCreator)))
      typeList2.Add(typeof (LabelCreator));
    if (typeList1.Contains(typeof (TableCreator)))
      typeList2.Add(typeof (TableCreator));
    if (typeList1.Contains(typeof (PolylineCreator)))
      typeList2.Add(typeof (PolylineCreator));
    if (typeList1.Contains(typeof (ContainerCreator)))
      typeList2.Add(typeof (ContainerCreator));
    foreach (System.Type type in typeList1)
    {
      if (type.Name != "TextBoxCreator" && type.Name != "LabelCreator" && type.Name != "TableCreator" && type.Name != "PolylineCreator" && type.Name != "ContainerCreator")
        typeList2.Add(type);
    }
    foreach (System.Type type in typeList2)
      this.AddPageElementCreator((PageElementCreator) Activator.CreateInstance(type));
  }

  private void DocumentControl_PageCursorPositionChanged(
    object sender,
    PageCursorPositionChanged_EventArgs e)
  {
    StatusBarPanel pageCursorPosition = this.sbPanelPageCursorPosition;
    float num = e.Position.X;
    string str1 = num.ToString("F2");
    num = e.Position.Y;
    string str2 = num.ToString("F2");
    string str3 = $"{str1}; {str2}";
    pageCursorPosition.Text = str3;
  }

  public void SelectionChanged()
  {
    if (this.propertyGridDlg != null)
    {
      DocumentControl documentControl = this.ActiveDocumentControl ?? this.DocumentTreeViewDlg.DocumentControl;
      if (documentControl?.SelectedNodes != null)
        this.propertyGridDlg.SelectedObjects = (object[]) documentControl.SelectedNodes.ToArray();
    }
    this.commandManager.QueryStatus();
  }

  private string ConfigFileNameOld => Application.StartupPath + "\\IMDocumentEditor.cfg";

  private string ConfigFileName
  {
    get
    {
      string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\Intermech";
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      return path + "\\IMDocumentEditor.cfg";
    }
  }

  public virtual void SaveConfiguration()
  {
    try
    {
      ConfigurationManager configurationManager = new ConfigurationManager("IntermechDocumentEditor");
      IConfiguration configuration1 = configurationManager.Create("MainWindow");
      if (this.WindowState != FormWindowState.Minimized)
        configuration1.SetProperty("WindowState", this.WindowState.ToString());
      if (this.WindowState != FormWindowState.Maximized && this.WindowState != FormWindowState.Minimized)
      {
        configuration1.SetProperty("Location", TypeDescriptor.GetConverter(typeof (Point)).ConvertToString((object) this.Location));
        configuration1.SetProperty("Size", TypeDescriptor.GetConverter(typeof (Size)).ConvertToString((object) this.Size));
      }
      ImDocumentEditorConfig.Instance.SaveConfiguration((IConfigurationManager) configurationManager);
      this.barManager.SaveConfiguration((BarManagerConfigurationStorage) new BarManagerConfigurationAdapter((IConfigurationManager) configurationManager));
      this.SaveControlsConfig(configurationManager);
      DockControl[] documents = this.dockManager.DocumentContainer.Documents;
      if (documents.Length != 0)
      {
        IConfiguration configuration2 = configurationManager.Create("Documents");
        for (int index = 0; index < documents.Length; ++index)
        {
          if (documents[index] is ImDocumentEditorForm documentEditorForm && !string.IsNullOrEmpty(documentEditorForm.FileName))
          {
            ImDocument document = documentEditorForm.Document;
            if ((document != null ? (document.IsTemplate ? 1 : 0) : 0) == 0 || documentEditorForm.Document?.TemplateOwner == null)
            {
              IConfiguration configuration3 = configuration2.Add("Window" + index.ToString());
              if (documentEditorForm.Document != null)
                configuration3.SetProperty("FileName", documentEditorForm.FileName);
            }
          }
        }
      }
      FileStream fileStream = new FileStream(this.ConfigFileName, FileMode.Create);
      try
      {
        configurationManager.Save((Stream) fileStream);
      }
      finally
      {
        fileStream.Close();
      }
    }
    catch (Exception ex)
    {
      int num = (int) ExceptionForm.ShowExceptionDialog(ex);
    }
  }

  private void SaveControlsConfig(ConfigurationManager configurationManager)
  {
    try
    {
      new ImDocumentDockManagerStorage((ImDocumentEditorFormBase) null, this.dockManager, (IConfigurationManager) configurationManager).SaveConfiguration();
      IConfigurationManager configurationManager1 = (IConfigurationManager) configurationManager;
      if (configurationManager1 == null)
        return;
      IConfiguration config = configurationManager1.Create("DockControls");
      this.propertyGridSettings = DockControlLayoutSettings.GetSettings((DockControl) this.propertyGridDlg, "PropertyGrid");
      this.docTreeViewSettings = DockControlLayoutSettings.GetSettings((DockControl) this.documentTreeViewDlg, "DocTreeView");
      this.insertSymbolViewSettings = DockControlLayoutSettings.GetSettings((DockControl) this.insertSymbolDlg, "InsertSymbolView");
      this.propertyGridSettings.SetSettings(config, "PropertyGrid");
      this.docTreeViewSettings.SetSettings(config, "DocTreeView");
      this.insertSymbolViewSettings.SetSettings(config, "InsertSymbolView");
    }
    catch (Exception ex)
    {
      if (ExceptionForm.ShowExceptionDialog(ex) != DialogResult.Abort)
        return;
      Application.Exit();
    }
  }

  private Intermech.Bars.ToolBar GetToolbarByGuid(Guid guid) => (Intermech.Bars.ToolBar) null;

  public virtual void LoadConfiguration()
  {
    string path = this.ConfigFileName;
    if (!File.Exists(this.ConfigFileName))
      path = this.ConfigFileNameOld;
    if (!File.Exists(path))
      return;
    try
    {
      ConfigurationManager configurationManager = new ConfigurationManager("IntermechDocumentEditor");
      FileStream fileStream = new FileStream(path, FileMode.Open);
      try
      {
        configurationManager.Load((Stream) fileStream);
      }
      finally
      {
        fileStream.Close();
      }
      IConfiguration configuration1 = configurationManager.Open("MainWindow");
      string property1 = configuration1.GetProperty("Location");
      if (property1 != "")
        this.Location = (Point) TypeDescriptor.GetConverter(typeof (Point)).ConvertFrom((object) property1);
      string property2 = configuration1.GetProperty("Size");
      if (property2 != "")
        this.Size = (Size) TypeDescriptor.GetConverter(typeof (Size)).ConvertFrom((object) property2);
      string property3 = configuration1.GetProperty("WindowState");
      if (property3 != "")
        this.WindowState = (FormWindowState) Enum.Parse(typeof (FormWindowState), property3);
      ImDocumentEditorConfig.Instance.LoadConfiguration((IConfigurationManager) configurationManager);
      this.barManager.LoadConfiguration((BarManagerConfigurationStorage) new BarManagerConfigurationAdapter((IConfigurationManager) configurationManager), new GetToolbarCallback(this.GetToolbarByGuid));
      this.LoadControlsConfiguration(configurationManager);
      IConfiguration configuration2 = configurationManager.Open("Documents");
      for (int index = 0; index < DocumentEditorMainForm.args.Length; ++index)
      {
        ImDocumentEditorForm docContent = new ImDocumentEditorForm((IImDocumentManager) this, true, true);
        string str = DocumentEditorMainForm.args[index];
        if (str != "" && File.Exists(str))
        {
          DocumentFileType docType;
          DocumentTreeNode documentTreeNode = ImDocument.LoadFromFile(str, out docType, true);
          docContent.ReadOnly = this.DocReadOnly(str);
          switch (documentTreeNode)
          {
            case ImDocument imDocument:
              docContent.DocumentControl.Document = imDocument;
              break;
            case DocumentsComplect documentsComplect:
              docContent.DocumentControl.DocumentsComplect = documentsComplect;
              break;
          }
          docContent.FileName = str;
          docContent.DocFileType = docType;
          configuration2 = (IConfiguration) null;
          this.SetupNewDocumentContent((ImDocumentEditorFormBase) docContent);
          docContent.Show(this.dockManager, DockState.Document);
          this.UpdateDocumentCaption(docContent, false, (string) null, (string) null);
        }
      }
      if (configuration2 == null)
        return;
      for (int index = 0; index < configuration2.Count; ++index)
      {
        IConfiguration configuration3 = configuration2.Configurations[index];
        ImDocumentEditorForm docContent = new ImDocumentEditorForm((IImDocumentManager) this, true, true);
        string property4 = configuration3.GetProperty("FileName");
        if (property4 != "" && File.Exists(property4))
        {
          DocumentFileType docType;
          DocumentTreeNode documentTreeNode = ImDocument.LoadFromFile(property4, out docType, true);
          docContent.ReadOnly = this.DocReadOnly(property4);
          switch (documentTreeNode)
          {
            case ImDocument imDocument:
              docContent.DocumentControl.Document = imDocument;
              break;
            case DocumentsComplect documentsComplect:
              docContent.DocumentControl.DocumentsComplect = documentsComplect;
              break;
          }
          docContent.FileName = property4;
          docContent.DocFileType = docType;
        }
        this.SetupNewDocumentContent((ImDocumentEditorFormBase) docContent);
        docContent.Show(this.dockManager, DockState.Document);
        this.UpdateDocumentCaption(docContent, false, (string) null, (string) null);
      }
    }
    catch (Exception ex)
    {
      int num = (int) ExceptionForm.ShowExceptionDialog(ex);
    }
  }

  public virtual void LoadControlsConfiguration(ConfigurationManager configurationManager)
  {
    try
    {
      IConfigurationManager configManager = (IConfigurationManager) configurationManager;
      ImDocumentDockManagerStorage dockManagerStorage = new ImDocumentDockManagerStorage((ImDocumentEditorFormBase) null, this.dockManager, configManager);
      dockManagerStorage.GetDockControlEvent = new DockManager.GetDockControlCallback(this.GetDockControl);
      if (configManager != null)
      {
        IConfiguration config = configManager.Open("DockControls");
        if (config != null)
        {
          this.propertyGridSettings = DockControlLayoutSettings.GetSettings(config, "PropertyGrid");
          this.docTreeViewSettings = DockControlLayoutSettings.GetSettings(config, "DocTreeView");
          this.insertSymbolViewSettings = DockControlLayoutSettings.GetSettings(config, "InsertSymbolView");
        }
      }
      if (!dockManagerStorage.LoadConfiguration())
      {
        if (this.docTreeViewSettings.Opened)
          this.ShowDocumentTreeView(true);
        if (this.propertyGridSettings.Opened)
          this.ShowPropertyGrid(true);
        if (this.insertSymbolViewSettings.Opened)
          this.ShowInsertSymbolDlg(true);
      }
      if (this.propertyGridDlg != null && this.propertyGridDlg.Size.Width <= 0 && this.propertyGridSettings.Opened)
        this.ShowPropertyGrid(true);
      if (this.documentTreeViewDlg != null && this.documentTreeViewDlg.Size.Width <= 0 && this.docTreeViewSettings.Opened)
        this.ShowDocumentTreeView(true);
      if (this.insertSymbolDlg != null && this.insertSymbolDlg.Size.Width <= 0 && this.insertSymbolViewSettings.Opened)
        this.ShowInsertSymbolDlg(true);
      if (this.propertyGridDlg != null && this.propertyGridDlg.LayoutSystem != null && this.propertyGridSettings.Visible)
        this.propertyGridDlg.LayoutSystem.SelectedControl = (DockControl) this.propertyGridDlg;
      if (this.documentTreeViewDlg != null && this.documentTreeViewDlg.LayoutSystem != null && this.docTreeViewSettings.Visible)
        this.documentTreeViewDlg.LayoutSystem.SelectedControl = (DockControl) this.documentTreeViewDlg;
      if (this.insertSymbolDlg == null || this.insertSymbolDlg.LayoutSystem == null || !this.insertSymbolViewSettings.Visible)
        return;
      this.insertSymbolDlg.LayoutSystem.SelectedControl = (DockControl) this.insertSymbolDlg;
    }
    catch (Exception ex)
    {
      if (ExceptionForm.ShowExceptionDialog(ex) != DialogResult.Abort)
        return;
      Application.Exit();
    }
  }

  protected virtual DockControl GetDockControl(Guid guid, string persistString, string text)
  {
    if (guid == DocumentTreeViewDlg.DockGuid)
    {
      this.ShowDocumentTreeView(false);
      return (DockControl) this.documentTreeViewDlg;
    }
    if (guid == PropertyGridForm.DockGuid)
    {
      this.ShowPropertyGrid(false);
      return (DockControl) this.propertyGridDlg;
    }
    if (!(guid == InsertSymbolDockControl.DockGuid))
      return (DockControl) null;
    this.ShowInsertSymbolDlg(false);
    return (DockControl) this.insertSymbolDlg;
  }

  public bool Execute(ICommandState commandState)
  {
    if (commandState == null)
      return false;
    try
    {
      bool flag = this.ActiveImDocumentEditorForm == null;
      if (flag && this.DocumentTreeViewDlg.DocumentControl == null)
        return false;
      if (commandState.CommandName == "ExportToWMF")
      {
        if (flag)
          return false;
        string fileName1 = this.ActiveImDocumentEditorForm.FileName;
        string fileName2 = $"{Path.GetDirectoryName(fileName1)}\\{Path.GetFileNameWithoutExtension(fileName1)}.wmf";
        int[] pages;
        if (ExportToImagesDlg.Execute(this.ActiveImDocumentEditorForm.Document.Nodes.Count, out pages, ref fileName2) == DialogResult.OK)
        {
          if (Path.GetExtension(fileName2).ToLower() == ".wmf")
            fileName2 = $"{Path.GetDirectoryName(fileName2)}\\{Path.GetFileNameWithoutExtension(fileName2)}";
          this.ActiveImDocumentEditorForm.Document.GeneratePageMetafiles(pages, fileName2);
        }
        return true;
      }
      if (commandState == this.selectElementCommand)
      {
        this.IsElementSelecting = true;
        return true;
      }
      int index = this.elementCreatorCommands.IndexOf((object) commandState);
      if (index >= 0)
      {
        this.SelectedElementCreator = this.elementCreators[index] as PageElementCreator;
        return true;
      }
      switch (commandState.CommandName)
      {
        case "DocElementProperty":
          this.ShowPropertyGrid(true);
          return true;
        case "PrevWindow":
          if (flag)
            return false;
          DocumentLayoutSystem documentLayoutSystem1 = (DocumentLayoutSystem) null;
          if (this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems.Count > 0)
            documentLayoutSystem1 = this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems[0] as DocumentLayoutSystem;
          if (documentLayoutSystem1 != null)
          {
            int num = documentLayoutSystem1.Controls.IndexOf(this.dockManager.ActiveDocument);
            if (num != -1 && num > 0)
              this.dockManager.DocumentContainer.ActiveDocument = documentLayoutSystem1.Controls[num - 1];
          }
          return true;
        case "NextWindow":
          if (flag)
            return false;
          DocumentLayoutSystem documentLayoutSystem2 = (DocumentLayoutSystem) null;
          if (this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems.Count > 0)
            documentLayoutSystem2 = this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems[0] as DocumentLayoutSystem;
          if (documentLayoutSystem2 != null)
          {
            int num = documentLayoutSystem2.Controls.IndexOf(this.dockManager.ActiveDocument);
            if (num != -1 && num < documentLayoutSystem2.Controls.Count - 1)
              this.dockManager.DocumentContainer.ActiveDocument = documentLayoutSystem2.Controls[num + 1];
          }
          return true;
        case "CloseAllWindows":
          foreach (DockControl document in this.dockManager.DocumentContainer.Documents)
          {
            if (document is ImDocumentEditorForm)
              document.Close();
          }
          return true;
        case "Tree.Update":
          if (this.documentTreeViewDlg != null)
            this.documentTreeViewDlg.UpdateTree();
          return true;
      }
    }
    catch (Exception ex)
    {
      if (ExceptionForm.ShowExceptionDialog(ex) == DialogResult.Abort)
        Application.Exit();
      return true;
    }
    return false;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    bool flag1 = this.ActiveImDocumentEditorForm == null;
    if (flag1 && this.DocumentTreeViewDlg?.DocumentControl == null)
      return false;
    if (commandState.CommandName == "ExportToWMF")
    {
      commandState.Enabled = true;
      return true;
    }
    if (commandState == this.selectElementCommand)
    {
      this.selectElementCommand.Enabled = true;
      return true;
    }
    if (this.elementCreatorCommands.IndexOf((object) commandState) >= 0)
    {
      commandState.Enabled = true;
      return true;
    }
    switch (commandState.CommandName)
    {
      case "Tree.Update":
        commandState.Enabled = ImDocumentData.ShowDebugInfo && this.documentTreeViewDlg != null && this.documentTreeViewDlg.GetFocusedControl() != null;
        return true;
      case "DocElementProperty":
        DocumentTreeNode[] selectedNodes = this.ActiveDocumentControl.GetSelectedNodes();
        commandState.Enabled = selectedNodes != null && selectedNodes.Length != 0;
        bool flag2 = this.propertyGridDlg == null || this.propertyGridDlg.Parent == null && this.propertyGridDlg.LayoutSystem == null;
        commandState.Visible = flag2;
        return true;
      case "PrevWindow":
        if (flag1)
          return false;
        DocumentLayoutSystem documentLayoutSystem1 = (DocumentLayoutSystem) null;
        if (this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems.Count > 0)
          documentLayoutSystem1 = this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems[0] as DocumentLayoutSystem;
        int num1 = -1;
        if (documentLayoutSystem1 != null)
          num1 = documentLayoutSystem1.Controls.IndexOf(this.dockManager.ActiveDocument);
        commandState.Enabled = num1 != -1 && num1 > 0;
        return true;
      case "NextWindow":
        if (flag1)
          return false;
        DocumentLayoutSystem documentLayoutSystem2 = (DocumentLayoutSystem) null;
        if (this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems.Count > 0)
          documentLayoutSystem2 = this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems[0] as DocumentLayoutSystem;
        bool flag3 = false;
        if (documentLayoutSystem2 != null)
        {
          int num2 = documentLayoutSystem2.Controls.IndexOf(this.dockManager.ActiveDocument);
          flag3 = num2 != -1 && num2 < documentLayoutSystem2.Controls.Count - 1;
        }
        commandState.Enabled = flag3;
        return true;
      case "CloseAllWindows":
        commandState.Enabled = true;
        return true;
      case "NewTestDocByTemplate":
        commandState.Enabled = true;
        return true;
      default:
        return false;
    }
  }

  private void DocumentEditorForm_Closed(object sender, EventArgs e) => this.SaveConfiguration();

  [Browsable(false)]
  public SaveFileDialog SaveToFileDialog
  {
    [DebuggerStepThrough] get
    {
      if (this.saveToFileDialog == null)
        this.saveToFileDialog = ImDocumentEditorFormBase.CreateSaveFileDialog();
      return this.saveToFileDialog;
    }
  }

  [Browsable(false)]
  public string RecentlySaveAsPath
  {
    [DebuggerStepThrough] get => this.recentlySaveAsPath;
    set => this.recentlySaveAsPath = value;
  }

  public void UpdateFormatCommands()
  {
    if (this.ActiveImDocumentEditorForm == null)
      return;
    this.ActiveImDocumentEditorForm.UpdateFormatCommands();
  }

  public void ShowExceptionDialog(Exception e)
  {
    if (ExceptionForm.ShowExceptionDialog(e) != DialogResult.Abort)
      return;
    Application.Exit();
  }

  private void miCreateFromTemplate_Click(object sender, EventArgs e)
  {
    if (this.OpenDlg.ShowDialog() != DialogResult.OK)
      return;
    ImDocument template = ImDocument.LoadFromXml(this.OpenDlg.FileName, true, false);
    if (!template.IsTemplate)
    {
      if (template.Template != null)
      {
        template = MessageBox.Show(LocalizationHolder.rm.GetString("Document.Editor_23"), LocalizationHolder.rm.GetString("Document.Editor_24"), MessageBoxButtons.YesNo) != DialogResult.Yes ? (ImDocument) null : (ImDocument) template.TemplateOwner;
      }
      else
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Document.Editor_25"), LocalizationHolder.rm.GetString("Document.Editor_26"), MessageBoxButtons.OK);
        template = (ImDocument) null;
      }
    }
    if (template == null)
      return;
    ImDocumentEditorForm docContent = new ImDocumentEditorForm((IImDocumentManager) this, new ImDocument(template, true, true), false);
    docContent.DefaultFileName = docContent.DocumentCaption + (object) this.docNameGeneratorCount++ + docContent.DefaultFileExtension;
    this.SetupNewDocumentContent((ImDocumentEditorFormBase) docContent);
    docContent.Show(this.dockManager, DockState.Document);
    this.UpdateDocumentCaption(docContent, false, (string) null, (string) null);
    docContent.Select();
  }

  private void menuButtonItem1_Click(object sender, EventArgs e) => this.ShowInsertSymbolDlg(true);

  private void miConfig_Click(object sender, EventArgs e)
  {
    if (this.docPropertyPagesService == null)
      return;
    this.docPropertyPagesService.ShowDialog();
  }

  private void miAbout_Click(object sender, EventArgs e)
  {
    int num = (int) new AboutDialog().ShowDialog();
  }

  private void miExit_Click(object sender, EventArgs e) => this.Close();

  private void miWindows_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    for (int index = this.miWindows.Items.Count - 1; index > 2; --index)
    {
      MenuButtonItem menuButtonItem = this.miWindows.Items[index];
      this.miWindows.Items.RemoveAt(index);
    }
    DocumentLayoutSystem documentLayoutSystem = (DocumentLayoutSystem) null;
    if (this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems.Count > 0)
      documentLayoutSystem = this.dockManager.DocumentContainer.LayoutSystem.LayoutSystems[0] as DocumentLayoutSystem;
    if (documentLayoutSystem == null)
      return;
    int num1 = 0;
    foreach (DockControl control in (CollectionBase) documentLayoutSystem.Controls)
    {
      string text = control.Text;
      int num2 = 25;
      int num3 = 7;
      if (text.Length > num2)
      {
        string str1 = text.Substring(0, num2 - num3);
        string str2 = text.Substring(text.Length - num3);
        text = str1.PadRight(str1.Length + 3, '.') + str2;
      }
      MenuButtonItem menuButtonItem = new MenuButtonItem(text);
      menuButtonItem.Click += new EventHandler(this.item_Click);
      if (control == this.dockManager.ActiveDocument)
        menuButtonItem.Checked = true;
      menuButtonItem.Tag = (object) control;
      if (num1 == 0)
        menuButtonItem.BeginGroup = true;
      this.miWindows.Items.Add((ToolbarItemBase) menuButtonItem);
      ++num1;
    }
  }

  private void item_Click(object sender, EventArgs e)
  {
    if (!((sender as MenuButtonItem).Tag is DockControl))
      return;
    this.dockManager.DocumentContainer.ActiveDocument = (sender as MenuButtonItem).Tag as DockControl;
  }

  void IStandaloneEditor.DragOver(DragEventArgs drgevent) => this.OnDragOver(drgevent);

  void IStandaloneEditor.DragDrop(DragEventArgs drgevent) => this.OnDragDrop(drgevent);

  private void mbNewTestDocByTemplate_Click(object sender, EventArgs e)
  {
    this.OpenNewDocumentByTemplate();
  }

  public struct COPYDATASTRUCT
  {
    public IntPtr dwData;
    public int cbData;
    [MarshalAs(UnmanagedType.LPStr)]
    public string lpData;
  }
}
