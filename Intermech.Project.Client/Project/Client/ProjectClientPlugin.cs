// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProjectClientPlugin
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Docking;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Plugins;
using Intermech.Metadata;
using Intermech.NavBars;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Views;
using Intermech.Project.Controls;
using Intermech.Protection;
using Intermech.Search;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

public class ProjectClientPlugin : IPackage, IConfigurable, ICommandTarget
{
  [NotNull]
  internal const string PluginName = "Редактор проектов";
  [CanBeNull]
  private MenuBarItem _projectMi;
  [CanBeNull]
  private MenuButtonItem _specialCommandsMi;
  [NotNull]
  internal static readonly CommandList CommandList = new CommandList();
  private ProjectEditorForm _activeEditor;

  [NotNull]
  internal static Assembly MyAssembly => typeof (ProjectClientPlugin).Assembly;

  public void Unload()
  {
    SpecialCommands.OnChanged -= new EventHandler(ProjectClientPlugin.SpecialCommands_OnChanged);
  }

  [NotNull]
  public string Name => "Редактор проектов";

  public void Load([NotNull] System.IServiceProvider serviceProvider)
  {
    (serviceProvider.GetService<ILicenser>(false) ?? throw new ProtectionException("ILicenser not found")).AllocateLicense(344);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      object obj = (object) null;
      try
      {
        obj = (object) sessionKeeper.Session.GetCustomService<IProjectServer>(false);
      }
      catch
      {
      }
      if (obj == null)
        throw new Exception($"Невозможно загрузить плагин \"{this.Name}\": серверная часть ImProject не загружена!");
      Intermech.Extensions.Client.Library.Init(serviceProvider, sessionKeeper.Session);
      Intermech.Workflow.Design.Holder.Init((IPackage) this, serviceProvider);
      Intermech.Project.Controls.Library.Init((IPackage) this, serviceProvider, sessionKeeper.Session);
      Images.Init(serviceProvider, sessionKeeper.Session);
    }
    Intermech.Client.Services.NotificationService.Subscribe("CalendarChanged", new NotificationEventHandler(ProjectClientPlugin.Notification_OnCalendarChanged));
    Intermech.Client.Services.CommandManager.AddTarget((ICommandTarget) this);
    IMainMenuService service1 = ApplicationServices.Container.GetService<IMainMenuService>(false);
    if (service1 != null)
    {
      MenuButtonItem menuButtonItem1 = new MenuButtonItem(this.Name);
      menuButtonItem1.CommandName = "ProjectEditor";
      menuButtonItem1.Click += new EventHandler(ProjectClientPlugin.ProjectEditor);
      menuButtonItem1.ImageIndex = Intermech.Project.Controls.Images.ProjectImageIndex;
      service1.RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Default, menuButtonItem1);
      MenuButtonItem menuButtonItem2 = new MenuButtonItem("Загрузка ресурсов...");
      menuButtonItem2.CommandName = "ResourceAssignments";
      menuButtonItem2.Click += new EventHandler(ProjectClientPlugin.ShowResourceAssignments);
      menuButtonItem2.ImageIndex = Intermech.Project.Controls.Images.ResourcesImageIndex;
      service1.RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Default, menuButtonItem2);
    }
    MenuBar menuBar1 = serviceProvider.GetService<BarManager>().MenuBar;
    ProjectClientPlugin.CommandList.AddCommand("InsertNew", Intermech.Project.Localization.GetString("CmdInsertTask")).Shortcut = Shortcut.Ins;
    ProjectClientPlugin.CommandList.AddCommand("InsertProject", Intermech.Project.Localization.GetString("CmdInsertProject"));
    ProjectClientPlugin.CommandList.AddCommand("Copy", Shortcut.CtrlC);
    ProjectClientPlugin.CommandList.AddCommand("Cut", Shortcut.CtrlX).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("Paste", Shortcut.CtrlV);
    CommandList.CommandInfo commandInfo = ProjectClientPlugin.CommandList.AddCommand("Import", Intermech.Project.Localization.GetString("CmdImport"));
    commandInfo.BeginGroup = true;
    commandInfo.Shortcut = Shortcut.CtrlShiftI;
    ProjectClientPlugin.CommandList.AddCommand("Export", Intermech.Project.Localization.GetString("CmdExport")).Shortcut = Shortcut.CtrlShiftE;
    ProjectClientPlugin.CommandList.AddCommand("CreateReport", Intermech.Project.Localization.GetString("CmdCreateReport")).BeginGroup = true;
    ProjectClientPlugin.CmdImportObject = ProjectClientPlugin.CommandList.AddCommand("ImportObject", Intermech.Project.Localization.GetString("CmdImportObject"));
    ProjectClientPlugin.CmdImportObject.BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("DecreaseIndent", Intermech.Project.Localization.GetString("CmdDecreaseIndent"), Intermech.Client.Services.NamedList.ImageIndex("imgBack")).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("IncreaseIndent", Intermech.Project.Localization.GetString("CmdIncreaseIndent"), Intermech.Client.Services.NamedList.ImageIndex("imgForward"));
    ProjectClientPlugin.CommandList.AddCommand("Delete", Shortcut.Del).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("Properties", Intermech.Project.Localization.GetString("CmdProperties"), Intermech.Client.Services.NamedList.ImageIndex("imgProp"), true).Shortcut = Shortcut.F4;
    ProjectClientPlugin.CommandList.AddCommand("ProjectProperties", Intermech.Project.Localization.GetString("CmdProjectProperties")).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("ValidateProject", Intermech.Project.Localization.GetString("CmdValidateProject")).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("Sync", Intermech.Project.Localization.GetString("CmdSync"), Intermech.Project.Controls.Images.SyncImageIndex).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("SpecialCommands", SpecialCommands.Caption).BeginGroup = false;
    ProjectClientPlugin.CommandList.AddCommand("WorkshopRouteProcessing", SpecialCommands.WorkshopRouteProcessingCommandCaption).BeginGroup = false;
    ProjectClientPlugin.CommandList.AddCommand("CheckOut").BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("CheckIn").BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("ConvertToProject", Intermech.Project.Localization.GetString("CmdConvertToProject")).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("ConvertToTask", Intermech.Project.Localization.GetString("CmdConvertToTask")).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("SyncWithImportedObjectComposition", Intermech.Project.Localization.GetString("CmdSyncWithImportedObjectComposition")).BeginGroup = false;
    ProjectClientPlugin.CommandList.AddCommand("ViewProject", Intermech.Project.Localization.GetString("CmdViewProject"), Intermech.Client.Services.NamedList.ImageIndex("imgView"), true);
    ProjectClientPlugin.CommandList.AddCommand("EditProject", Intermech.Project.Localization.GetString("CmdEditProject"));
    ProjectClientPlugin.CommandList.AddCommand("Filters", Intermech.Project.Localization.GetString("CmdFilters")).BeginGroup = true;
    ProjectClientPlugin.CommandList.AddCommand("Save");
    ProjectClientPlugin.CommandList.AddCommand("Find");
    ProjectClientPlugin.CommandList.AddCommand("Print");
    ProjectClientPlugin.CommandList.AddCommand("PrintPreview");
    ProjectClientPlugin.CommandList.AddCommand("LineStyleSetup");
    ProjectClientPlugin.CommandList.AddCommand("PrintDocument");
    DockManager service2 = ApplicationServices.Container.GetService<DockManager>();
    service2.DocumentContainer.ActiveDocumentChanged += new ActiveDocumentEventHandler(this.DocumentContainer_ActiveDocumentChanged);
    service2.DockControlActivating += new DockManager.DockControlActivatingHandler(ProjectClientPlugin.dockManager_DockControlActivating);
    MenuBarItem menuBar2 = menuBar1.FindMenuBar("View");
    if (menuBar2 != null)
    {
      this._projectMi = new MenuBarItem(Intermech.Project.Localization.GetString("MenuProject"));
      string[] items = new string[17]
      {
        "InsertNew",
        "InsertProject",
        "DecreaseIndent",
        "IncreaseIndent",
        "ConvertToProject",
        "ConvertToTask",
        "ValidateProject",
        "Sync",
        "SpecialCommands",
        "CreateReport",
        "Import",
        "Export",
        "ImportObject",
        "SyncWithImportedObjectComposition",
        "Filters",
        "Properties",
        "ProjectProperties"
      };
      ProjectClientPlugin.CommandList.AddToMenu((MenuItemBase) this._projectMi, (IReadOnlyCollection<string>) items);
      menuBar1.Items.Insert(menuBar2.Index, (ToolbarItemBase) this._projectMi);
      MenuBarItem menuBarItem = new MenuBarItem(Intermech.Project.Localization.GetString("MenuProject"));
      this._specialCommandsMi = this._projectMi.Items.Cast<MenuButtonItem>().FirstOrDefault<MenuButtonItem>((Func<MenuButtonItem, bool>) (menuItem => menuItem.CommandName == "SpecialCommands"));
      SpecialCommands.OnChanged += new EventHandler(ProjectClientPlugin.SpecialCommands_OnChanged);
      if (this._specialCommandsMi != null)
        ProjectClientPlugin.CommandList.AddToMenu((MenuItemBase) this._specialCommandsMi, (IReadOnlyCollection<string>) Enumeration.Create<string>("WorkshopRouteProcessing").ToList<string>());
    }
    INavigationBar service3 = serviceProvider.GetService<INavigationBar>(false);
    if (service3 != null && service3.FindPane("appPane") is IAppPane pane)
      pane.Add(this.Name, new EventHandler(ProjectClientPlugin.ProjectEditor), Intermech.Project.Controls.Images.ProjectImageIndex);
    BarManager service4 = ApplicationServices.Container.GetService<BarManager>(false);
    if (service4 != null)
      service4.RendererChanged += new EventHandler(ProjectClientPlugin.BarManager_RendererChanged);
    MenuTemplate contextMenuTemplate = Intermech.Client.Services.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode node = new MenuTemplateNode("ImProject", Intermech.Project.Localization.GetString("MenuProject"), Intermech.Project.Controls.Images.ProjectImageIndex, 2, 3);
      contextMenuTemplate.Nodes.Add(node);
      node.Nodes.Add(new MenuTemplateNode("ViewProject", Intermech.Project.Localization.GetString("CmdView"), Intermech.Client.Services.NamedList.ImageIndex("imgView"), 0, 1, Keys.F3));
      node.Nodes.Add(new MenuTemplateNode("EditProject", Intermech.Project.Localization.GetString("CmdEdit"), -1, 0, 2));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("StartProject", Intermech.Project.Localization.GetString("CmdStartProject"), -1, 0, 1));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("AbortProject", Intermech.Project.Localization.GetString("CmdAbortProject"), -1, 0, 2));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("VerifyResults", Intermech.Project.Localization.GetString("CmdVerifyResults"), Intermech.Workflow.Images.LaunchProcessImageIndex, 0, 2));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    Intermech.Client.Services.Factory.AddCommandsProvider(1, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task, (ICommandsProvider) new TaskCommands(this));
    Intermech.Client.Services.Factory.AddCommandsProvider(1, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Dependency, (ICommandsProvider) new TaskCommands(this));
    Intermech.Client.Services.Factory.AddCommandsProvider(1, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project, (ICommandsProvider) new ProjectCommands(this));
    serviceProvider.GetService<IDefaultCommands4ObjTypes>(false)?.AddDefaultCommand((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project, "EditDocument", DefaultCommandHandler.ContectMenu);
    IObjectCreatorService service5 = ApplicationServices.Container.GetService<IObjectCreatorService>();
    service5.RegisterCreatorCustomService((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project, typeof (ProjectCreator));
    service5.RegisterCreatorCustomService((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task, typeof (ProjectCreator));
    Intermech.Client.Services.Factory.AddCommandsProvider(1, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.ProjectMessage, (ICommandsProvider) new NotificationCommandProvider());
    MenuBarItem menuBar3 = menuBar1.FindMenuBar("File");
    if (menuBar3 != null)
    {
      MenuItemBase menuItemBase = menuBar3.FindItem("New");
      if (menuItemBase != null)
      {
        MenuButtonItem menuButtonItem = new MenuButtonItem(Intermech.Project.Localization.GetString("ProjectFull"));
        menuButtonItem.CommandName = "New.ImProject";
        menuButtonItem.Click += new EventHandler(ProjectClientPlugin.ProjectEditor);
        menuButtonItem.ImageIndex = Intermech.Project.Controls.Images.ProjectImageIndex;
        menuItemBase.Items.Add((ToolbarItemBase) menuButtonItem);
      }
    }
    serviceProvider.GetService<IContentProvider>().ContentCallback += new GetContentCallback(ProjectClientPlugin.contProvider_ContentCallback);
    UserTaskView.ViewProject += new UserTaskView.ViewProjectDelegate(ProjectClientPlugin.UserTaskView_ViewProject);
    Intermech.Client.Services.Factory.AddViewsProvider(1, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.ProjectObjects, (IViewsProvider) new ProjectViewsProvider());
    IPropertyPagesService service6 = ApplicationServices.Container.GetService<IPropertyPagesService>(false);
    if (service6 != null)
    {
      service6.AddPage(Intermech.Project.Localization.GetString("SpecialCommandsSettingsPath"), (IPropertyPage) SpecialCommands.Instance);
      if (Portal.Enabled)
        service6.AddPage($"{Intermech.Project.Localization.GetString("RemoteSettingsRoot")}\\{Intermech.Project.Localization.GetString("RemoteSettingsPageName")}", (IPropertyPage) new RemoteSettingsPropertyPage());
    }
    ProjectsRootNode.Register();
  }

  private static void SpecialCommands_OnChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    Intermech.Client.Services.CommandManager.QueryStatus();
  }

  private static void dockManager_DockControlActivating([NotNull] DockControl control, [NotNull] CancelEventArgs args)
  {
    DockManager service = ApplicationServices.Container.GetService<DockManager>(false);
    if (service == null || !(service.ActiveDockControl?.Tag is ProjectEditorForm tag) || tag.ProjectView.Validate())
      return;
    args.Cancel = true;
  }

  private static void UserTaskView_ViewProject(long projectID) => IMProject.ViewProject(projectID);

  private static void Notification_OnCalendarChanged([CanBeNull] object sender, [NotNull] NotificationEventArgs e)
  {
    if (!(sender is ICalendar calendar))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ScheduleList.ReloadSchedule(calendar.CalendarID, sessionKeeper.Session);
  }

  private static void BarManager_RendererChanged([NotNull] object sender, [NotNull] EventArgs e)
  {
    foreach (EditorInfo editorInfo in (List<EditorInfo>) Editors.List)
    {
      if (editorInfo.Form is ProjectEditorForm form)
        form.Renderer = ((BarManager) sender).Renderer;
    }
  }

  [CanBeNull]
  private static DockControl contProvider_ContentCallback(Guid guid, [NotNull] string persistString)
  {
    if (guid == ProjectEditorForm.DockGuid)
    {
      ProjectEditorMode mode = ProjectEditorMode.Project;
      bool editingMode = false;
      List<long> ids = new List<long>();
      if (ProjectEditorForm.ParsePersistString(persistString, ref mode, ref editingMode, ref ids))
      {
        ProjectEditorForm projectEditorForm = (ProjectEditorForm) null;
        if (mode == ProjectEditorMode.Project)
        {
          if (ids.Count > 0)
            projectEditorForm = IMProject.OpenProject(ids[0], editingMode);
        }
        else
          projectEditorForm = IMProject.ShowResourceAssignments(ids);
        if (projectEditorForm != null)
          return projectEditorForm.DockControl;
      }
    }
    return (DockControl) null;
  }

  private static void ProjectEditor([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    IMProject.EditProject(0L);
  }

  private static void ShowResourceAssignments([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    IMProject.ShowResourceAssignments();
  }

  protected void DocumentContainer_ActiveDocumentChanged([CanBeNull] object sender, [NotNull] ActiveDocumentEventArgs e)
  {
    this._activeEditor = !(e.NewActiveDocument?.Tag is ProjectEditorForm tag) ? (ProjectEditorForm) null : tag;
    if (this._projectMi != null)
      this._projectMi.Visible = this._activeEditor != null && this._activeEditor.Mode == ProjectEditorMode.Project;
    Intermech.Client.Services.CommandManager.QueryStatus();
    ProjectEditorForm.CurrentProject = this._activeEditor?.Project;
  }

  public void LoadConfiguration([NotNull] IConfigurationManager configurationManager)
  {
  }

  public void SaveConfiguration([NotNull] IConfigurationManager configurationManager)
  {
  }

  public bool Execute(ICommandState commandState)
  {
    ProjectEditorForm activeEditor = this._activeEditor;
    // ISSUE: explicit non-virtual call
    return activeEditor != null && __nonvirtual (activeEditor.Execute(commandState));
  }

  public bool QueryStatus(ICommandState commandState)
  {
    ProjectEditorForm activeEditor = this._activeEditor;
    // ISSUE: explicit non-virtual call
    return activeEditor != null && __nonvirtual (activeEditor.QueryStatus(commandState));
  }

  [NotNull]
  internal static CommandList.CommandInfo CmdImportObject { get; private set; }
}
