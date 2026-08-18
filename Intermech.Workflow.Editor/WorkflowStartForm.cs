// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Editor.WorkflowStartForm
// Assembly: Intermech.Workflow.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 48E18BC1-AABA-4AA1-97DA-4BBD788BE326
// Assembly location: D:\IPS\Client\Intermech.Workflow.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Editor.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Editor;

public class WorkflowStartForm : DockControl, ICommandTarget
{
  private ISelectedItemsHost _selhost;
  private long _schemeID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeViewsBridge treeViewsBridge;
  private ImageList imageList1;
  private Panel panel7;
  private PictureBox NewBox;
  private LinkLabel NewSchemeLabel;
  private Panel panel6;
  private Button OpenButton;
  private SchemesTreeView schemesView;
  private PageViewsManager pageViewsManager;
  private Panel SchemesPanel;
  private Splitter schemesSplitter;
  private DockManager dockManager;
  private DockContainer leftDock;
  private DockContainer rightDock;
  private DockControl RecentSchemesDock;
  private DockControl RecentLaunchedDock;
  private DockContainer bottomDock;
  private DockContainer topDock;
  private Label NoRecentLabel;
  private ListView RecentSchemesView;
  private ColumnHeader columnHeader1;
  private Label NoLaunchedLabel;
  private ListView RecentLaunchedView;
  private ColumnHeader columnHeader2;
  private LinkLabel showBaseVersion;

  public WorkflowStartForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1283);
  }

  private void WorkflowStartForm_Load(object sender, EventArgs e)
  {
    int index = BaseHolder.NamedList.ImageIndex("imgNewItem");
    if (index != -1)
      this.NewBox.Image = BaseHolder.NamedList.ImageList.Images[index];
    Intermech.Workflow.Design.Holder.ShowOnlyBaseVersion = false;
    Intermech.Workflow.Design.Holder.СanShowAllVersions = false;
    IDescriptor rootDescriptor = (IDescriptor) new TopObjectsDescriptor(Intermech.Workflow.Design.Holder.CategorySchemesID, 0, LocalizationHolder.rm.GetString("Workflow.Editor_18"), wfConsts.SchemeCategoriesID);
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService());
    serviceContainer.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    serviceContainer.AddService(typeof (VersionsRule), (object) Intermech.Workflow.Design.Holder.AllVersionsRule);
    serviceContainer.AddService(typeof (ICommandManager), (object) BaseHolder.CommandManager);
    this.schemesView.Services = (System.IServiceProvider) serviceContainer;
    this.pageViewsManager.Services = (System.IServiceProvider) serviceContainer;
    this.schemesView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    if (!wfFunx.RestoreTreePath((NavigatorTreeView) this.schemesView))
      this.schemesView.Build(rootDescriptor);
    if (this.pageViewsManager.ActiveViewPage != null)
    {
      this._selhost = this.pageViewsManager.ActiveViewPage.View as ISelectedItemsHost;
      if (this._selhost != null)
        this._selhost.SelectedItemsChanged += new EventHandler(this.SchemesSelectedItemsChanged);
    }
    this.SchemesSelectedItemsChanged((object) null, (EventArgs) null);
    this.FillRecentLaunched((object) null, (NotificationEventArgs) null);
    this.FillRecentSchemes((object) null, (NotificationEventArgs) null);
    BaseHolder.NotificationService.Subscribe("RecentLaunchedChanged", new NotificationEventHandler(this.FillRecentLaunched));
    BaseHolder.NotificationService.Subscribe("RecentSchemesChanged", new NotificationEventHandler(this.FillRecentSchemes));
    if (!(ApplicationServices.Container.GetService(typeof (DockManager)) is DockManager service))
      return;
    service.RendererChanged += new EventHandler(this.dm_RendererChanged);
    this.dm_RendererChanged((object) service, EventArgs.Empty);
  }

  private void dm_RendererChanged(object sender, EventArgs e)
  {
    this.dockManager.Renderer = (sender as DockManager).Renderer;
  }

  internal void OnClosed()
  {
    BaseHolder.NotificationService.Unsubscribe("RecentLaunchedChanged", new NotificationEventHandler(this.FillRecentLaunched));
    BaseHolder.NotificationService.Unsubscribe("RecentSchemesChanged", new NotificationEventHandler(this.FillRecentSchemes));
    Intermech.Workflow.Design.Holder.ShowOnlyBaseVersion = false;
  }

  private void FillRecentLaunched(object sender, NotificationEventArgs e)
  {
    this.RecentLaunchedView.Items.Clear();
    int schemeImageIndex = Intermech.Workflow.Design.Holder.SchemeImageIndex;
    this.RecentLaunchedView.SmallImageList = BaseHolder.IconService.ImageList;
    RecentList recentLaunched = Intermech.Workflow.Design.Holder.RecentLaunched;
    if (recentLaunched != null)
    {
      for (int index = 0; index < recentLaunched.Count; ++index)
        this.RecentLaunchedView.Items.Add(recentLaunched.Captions[index], schemeImageIndex).Tag = (object) recentLaunched.IDs[index];
    }
    this.NoLaunchedLabel.Visible = this.RecentLaunchedView.Items.Count == 0;
    this.RecentLaunchedView.Visible = !this.NoLaunchedLabel.Visible;
  }

  private void FillRecentSchemes(object sender, NotificationEventArgs e)
  {
    this.RecentSchemesView.Items.Clear();
    int schemeImageIndex = Intermech.Workflow.Design.Holder.SchemeImageIndex;
    this.RecentSchemesView.SmallImageList = BaseHolder.IconService.ImageList;
    RecentList recentSchemes = Intermech.Workflow.Design.Holder.RecentSchemes;
    if (recentSchemes != null)
    {
      for (int index = 0; index < recentSchemes.Count; ++index)
        this.RecentSchemesView.Items.Add(recentSchemes.Captions[index], schemeImageIndex).Tag = (object) recentSchemes.IDs[index];
    }
    this.NoRecentLabel.Visible = this.RecentSchemesView.Items.Count == 0;
    this.RecentSchemesView.Visible = !this.NoRecentLabel.Visible;
  }

  public void LoadState()
  {
    this.SuspendLayout();
    try
    {
      IDBConfigurations service1 = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
      this.schemesView.Width = (int) service1.ReadInteger("CLIENT", "Workflow.Editor", "SchemesViewW", (long) this.schemesView.Width, DBConfigMode.UserOnly);
      service1.ReadInteger("CLIENT", "Workflow.Editor", "LeftPanelTM", -1L, DBConfigMode.UserOnly);
      if (ControlFuncs.IsKeyPressed(Keys.ShiftKey))
        return;
      byte[] config_file;
      service1.LoadConfigData("Workflow.Editor.Layout", out BlobInformation _, out config_file);
      if (config_file.Length == 0)
        return;
      using (MemoryStream ms = new MemoryStream(config_file))
      {
        try
        {
          this.dockManager.SetLayout(StreamHelper.StreamToString((Stream) ms));
        }
        catch (Exception ex)
        {
          if (!(ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service2))
            return;
          service2.WriteString("Ошибки", $"В процессе загрузки формы проишла ошибка: {ex.Message}.");
        }
      }
    }
    finally
    {
      this.ResumeLayout();
    }
  }

  public void SaveState()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      configurations.WriteInteger("CLIENT", "Workflow.Editor", "SchemesViewW", (long) this.schemesView.Width);
      using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(this.dockManager.GetLayout().Replace("Visible=\"False\"", "Visible=\"True\""))))
      {
        BlobInformation config_info = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, "Workflow.Editor.Layout", ArcMethods.NotPacked, "");
        configurations.WriteConfigData(config_info, memoryStream.ToArray());
      }
    }
  }

  private void SchemesSelectedItemsChanged(object sender, EventArgs e)
  {
    if (this._selhost == null)
      return;
    ISelectedItems selectedItems = this._selhost.SelectedItems;
    if (selectedItems.Count > 0 && selectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
      this.SchemeID = itemData.Value;
    else
      this.SchemeID = 0L;
  }

  private void NewSchemeLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
  {
    wfFunx.EditProcess(0L);
  }

  private long SchemeID
  {
    get => this._schemeID;
    set
    {
      if (this._schemeID == value)
        return;
      this._schemeID = value;
      this.OpenButton.Enabled = value != 0L;
    }
  }

  private void OpenButton_Click(object sender, EventArgs e) => wfFunx.EditProcess(this.SchemeID);

  private void RecentSchemesView_ItemActivate(object sender, EventArgs e)
  {
    if (!(sender is ListView))
      return;
    ListViewItem focusedItem = (sender as ListView).FocusedItem;
    if (focusedItem == null)
      return;
    long int64 = Convert.ToInt64(focusedItem.Tag);
    if (sender == this.RecentSchemesView)
      wfFunx.EditProcess(int64);
    else
      wfFunx.CreateProcess(int64);
  }

  private void RecentSchemesDock_Resize(object sender, EventArgs e)
  {
    this.RecentSchemesView.Columns[0].Width = this.RecentSchemesView.ClientSize.Width;
    this.NoRecentLabel.Left = this.RecentSchemesView.Width / 2 - this.NoRecentLabel.Width / 2;
  }

  private void RecentLaunchedDock_Resize(object sender, EventArgs e)
  {
    this.RecentLaunchedView.Columns[0].Width = this.RecentLaunchedView.ClientSize.Width;
    this.NoLaunchedLabel.Left = this.RecentLaunchedView.Width / 2 - this.NoLaunchedLabel.Width / 2;
  }

  public void UpdateFloatingDocks(bool allow)
  {
    if (this.RecentSchemesDock.IsFloating && this.RecentSchemesDock.DockContainer != null && this.RecentSchemesDock.DockContainer.Parent != null)
      this.RecentSchemesDock.DockContainer.Parent.Visible = allow;
    if (!this.RecentLaunchedDock.IsFloating || this.RecentLaunchedDock.DockContainer == null || this.RecentLaunchedDock.DockContainer.Parent == null)
      return;
    this.RecentLaunchedDock.DockContainer.Parent.Visible = allow;
  }

  public bool Execute(ICommandState commandState) => this.pageViewsManager.Execute(commandState);

  public bool QueryStatus(ICommandState commandState)
  {
    return this.pageViewsManager.QueryStatus(commandState);
  }

  private void showBaseVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
  {
    ChildrenView control = this.pageViewsManager.ActiveViewPage.Control as ChildrenView;
    if (Intermech.Workflow.Design.Holder.ShowOnlyBaseVersion)
    {
      Intermech.Workflow.Design.Holder.ShowOnlyBaseVersion = false;
      this.showBaseVersion.Text = "Показать только базовые версии...";
    }
    else
    {
      Intermech.Workflow.Design.Holder.ShowOnlyBaseVersion = true;
      this.showBaseVersion.Text = "Показать все версии...";
    }
    int? count = new int?();
    control.ReloadItems(count);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WorkflowStartForm));
    this.imageList1 = new ImageList(this.components);
    this.treeViewsBridge = new TreeViewsBridge(this.components);
    this.schemesView = new SchemesTreeView();
    this.pageViewsManager = new PageViewsManager();
    this.panel7 = new Panel();
    this.NewBox = new PictureBox();
    this.NewSchemeLabel = new LinkLabel();
    this.panel6 = new Panel();
    this.OpenButton = new Button();
    this.SchemesPanel = new Panel();
    this.schemesSplitter = new Splitter();
    this.dockManager = new DockManager();
    this.leftDock = new DockContainer();
    this.rightDock = new DockContainer();
    this.RecentSchemesDock = new DockControl();
    this.NoRecentLabel = new Label();
    this.RecentSchemesView = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.RecentLaunchedDock = new DockControl();
    this.NoLaunchedLabel = new Label();
    this.RecentLaunchedView = new ListView();
    this.columnHeader2 = new ColumnHeader();
    this.bottomDock = new DockContainer();
    this.topDock = new DockContainer();
    this.showBaseVersion = new LinkLabel();
    this.schemesView.BeginInit();
    this.panel7.SuspendLayout();
    ((ISupportInitialize) this.NewBox).BeginInit();
    this.panel6.SuspendLayout();
    this.SchemesPanel.SuspendLayout();
    this.rightDock.SuspendLayout();
    this.RecentSchemesDock.SuspendLayout();
    this.RecentLaunchedDock.SuspendLayout();
    this.SuspendLayout();
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "abort.bmp");
    this.treeViewsBridge.NavTreeView = (NavigatorTreeView) this.schemesView;
    this.treeViewsBridge.UseDelay = false;
    this.treeViewsBridge.ViewsManager = (IViewsManager) this.pageViewsManager;
    this.schemesView.AllowDrop = true;
    this.schemesView.AllowMultiSelect = false;
    this.schemesView.AllowUserPinnedColumns = false;
    this.schemesView.DisableCheckedOutColumn = true;
    this.schemesView.DisableKeyDownEvents = true;
    componentResourceManager.ApplyResources((object) this.schemesView, "schemesView");
    this.schemesView.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("schemesView.HeaderStyle.HorzAlignment");
    this.schemesView.ImageList = (ImageList) null;
    this.schemesView.LineStyle = LineStyle.Dot;
    this.schemesView.Name = "schemesView";
    this.schemesView.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowEvenStyle.WordWrap");
    this.schemesView.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowOddStyle.WordWrap");
    this.schemesView.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowSelectedStyle.WordWrap");
    this.schemesView.RowStyle.BorderColor = SystemColors.Control;
    this.schemesView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.schemesView.RowStyle.BorderWidth = 1;
    this.schemesView.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowStyle.WordWrap");
    this.schemesView.SelectBeforeEdit = true;
    this.schemesView.ShowRootRow = false;
    this.schemesView.SuppressErrorMessages = true;
    this.schemesView.UseThemedHeaders = false;
    this.pageViewsManager.ActiveViewPage = (IViewPage) null;
    this.pageViewsManager.AllowedViews = new string[1]
    {
      "ChildrenView"
    };
    this.pageViewsManager.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this.pageViewsManager, "pageViewsManager");
    this.pageViewsManager.Name = "pageViewsManager";
    this.panel7.BackColor = SystemColors.Control;
    this.panel7.Controls.Add((Control) this.NewBox);
    this.panel7.Controls.Add((Control) this.showBaseVersion);
    this.panel7.Controls.Add((Control) this.NewSchemeLabel);
    componentResourceManager.ApplyResources((object) this.panel7, "panel7");
    this.panel7.Name = "panel7";
    componentResourceManager.ApplyResources((object) this.NewBox, "NewBox");
    this.NewBox.Name = "NewBox";
    this.NewBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.NewSchemeLabel, "NewSchemeLabel");
    this.NewSchemeLabel.Name = "NewSchemeLabel";
    this.NewSchemeLabel.TabStop = true;
    this.NewSchemeLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.NewSchemeLabel_LinkClicked);
    this.panel6.BackColor = SystemColors.Control;
    this.panel6.Controls.Add((Control) this.OpenButton);
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.Name = "panel6";
    componentResourceManager.ApplyResources((object) this.OpenButton, "OpenButton");
    this.OpenButton.BackColor = Color.Transparent;
    this.OpenButton.Name = "OpenButton";
    this.OpenButton.UseVisualStyleBackColor = false;
    this.OpenButton.Click += new EventHandler(this.OpenButton_Click);
    this.SchemesPanel.BackColor = SystemColors.Control;
    this.SchemesPanel.Controls.Add((Control) this.pageViewsManager);
    this.SchemesPanel.Controls.Add((Control) this.schemesSplitter);
    this.SchemesPanel.Controls.Add((Control) this.schemesView);
    componentResourceManager.ApplyResources((object) this.SchemesPanel, "SchemesPanel");
    this.SchemesPanel.Name = "SchemesPanel";
    componentResourceManager.ApplyResources((object) this.schemesSplitter, "schemesSplitter");
    this.schemesSplitter.Name = "schemesSplitter";
    this.schemesSplitter.TabStop = false;
    this.dockManager.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.leftDock, "leftDock");
    this.leftDock.Guid = new Guid("45212dd5-f77c-4cab-b6b7-48abb73abe9e");
    this.leftDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.leftDock.Manager = this.dockManager;
    this.leftDock.Name = "leftDock";
    this.leftDock.Renderer = (RendererBase) null;
    this.rightDock.Controls.Add((Control) this.RecentSchemesDock);
    this.rightDock.Controls.Add((Control) this.RecentLaunchedDock);
    componentResourceManager.ApplyResources((object) this.rightDock, "rightDock");
    this.rightDock.Guid = new Guid("cb5a6c57-069e-462d-80ba-62f97eddf97f");
    this.rightDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[2]
    {
      (LayoutSystemBase) new ControlLayoutSystem(250, 279, new DockControl[1]
      {
        this.RecentSchemesDock
      }, this.RecentSchemesDock),
      (LayoutSystemBase) new ControlLayoutSystem(250, 296, new DockControl[1]
      {
        this.RecentLaunchedDock
      }, this.RecentLaunchedDock)
    });
    this.rightDock.Manager = this.dockManager;
    this.rightDock.Name = "rightDock";
    this.rightDock.Renderer = (RendererBase) null;
    this.RecentSchemesDock.BackColor = SystemColors.Window;
    this.RecentSchemesDock.Closable = false;
    this.RecentSchemesDock.Controls.Add((Control) this.NoRecentLabel);
    this.RecentSchemesDock.Controls.Add((Control) this.RecentSchemesView);
    componentResourceManager.ApplyResources((object) this.RecentSchemesDock, "RecentSchemesDock");
    this.RecentSchemesDock.FloatingLocation = new Point(835, 324);
    this.RecentSchemesDock.Guid = new Guid("78f38b1a-b842-4b1f-8baa-620dc3429841");
    this.RecentSchemesDock.Name = "RecentSchemesDock";
    this.RecentSchemesDock.Resize += new EventHandler(this.RecentSchemesDock_Resize);
    componentResourceManager.ApplyResources((object) this.NoRecentLabel, "NoRecentLabel");
    this.NoRecentLabel.BackColor = SystemColors.Window;
    this.NoRecentLabel.Name = "NoRecentLabel";
    this.RecentSchemesView.Activation = ItemActivation.OneClick;
    this.RecentSchemesView.BorderStyle = System.Windows.Forms.BorderStyle.None;
    this.RecentSchemesView.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    componentResourceManager.ApplyResources((object) this.RecentSchemesView, "RecentSchemesView");
    this.RecentSchemesView.HeaderStyle = ColumnHeaderStyle.None;
    this.RecentSchemesView.HotTracking = true;
    this.RecentSchemesView.HoverSelection = true;
    this.RecentSchemesView.Name = "RecentSchemesView";
    this.RecentSchemesView.SmallImageList = this.imageList1;
    this.RecentSchemesView.UseCompatibleStateImageBehavior = false;
    this.RecentSchemesView.View = View.Details;
    this.RecentSchemesView.ItemActivate += new EventHandler(this.RecentSchemesView_ItemActivate);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    this.RecentLaunchedDock.BackColor = SystemColors.Window;
    this.RecentLaunchedDock.Closable = false;
    this.RecentLaunchedDock.Controls.Add((Control) this.NoLaunchedLabel);
    this.RecentLaunchedDock.Controls.Add((Control) this.RecentLaunchedView);
    componentResourceManager.ApplyResources((object) this.RecentLaunchedDock, "RecentLaunchedDock");
    this.RecentLaunchedDock.FloatingLocation = new Point(835, 324);
    this.RecentLaunchedDock.Guid = new Guid("44ae4519-fb9a-4d1d-a4a4-9388b6f373fc");
    this.RecentLaunchedDock.Name = "RecentLaunchedDock";
    this.RecentLaunchedDock.Resize += new EventHandler(this.RecentLaunchedDock_Resize);
    componentResourceManager.ApplyResources((object) this.NoLaunchedLabel, "NoLaunchedLabel");
    this.NoLaunchedLabel.Name = "NoLaunchedLabel";
    this.RecentLaunchedView.Activation = ItemActivation.OneClick;
    this.RecentLaunchedView.BorderStyle = System.Windows.Forms.BorderStyle.None;
    this.RecentLaunchedView.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader2
    });
    componentResourceManager.ApplyResources((object) this.RecentLaunchedView, "RecentLaunchedView");
    this.RecentLaunchedView.HeaderStyle = ColumnHeaderStyle.None;
    this.RecentLaunchedView.HotTracking = true;
    this.RecentLaunchedView.HoverSelection = true;
    this.RecentLaunchedView.Name = "RecentLaunchedView";
    this.RecentLaunchedView.UseCompatibleStateImageBehavior = false;
    this.RecentLaunchedView.View = View.Details;
    this.RecentLaunchedView.ItemActivate += new EventHandler(this.RecentSchemesView_ItemActivate);
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
    this.bottomDock.Guid = new Guid("9155813b-2eab-45cd-bc2c-3860e5850279");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.bottomDock.Manager = this.dockManager;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.topDock, "topDock");
    this.topDock.Guid = new Guid("da08ac07-e4cc-4a5d-988a-398aa22fd63c");
    this.topDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.topDock.Manager = this.dockManager;
    this.topDock.Name = "topDock";
    this.topDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.showBaseVersion, "showBaseVersion");
    this.showBaseVersion.Name = "showBaseVersion";
    this.showBaseVersion.TabStop = true;
    this.showBaseVersion.LinkClicked += new LinkLabelLinkClickedEventHandler(this.showBaseVersion_LinkClicked);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.SchemesPanel);
    this.Controls.Add((Control) this.panel7);
    this.Controls.Add((Control) this.panel6);
    this.Controls.Add((Control) this.leftDock);
    this.Controls.Add((Control) this.rightDock);
    this.Controls.Add((Control) this.bottomDock);
    this.Controls.Add((Control) this.topDock);
    this.Name = nameof (WorkflowStartForm);
    this.Load += new EventHandler(this.WorkflowStartForm_Load);
    this.schemesView.EndInit();
    this.panel7.ResumeLayout(false);
    this.panel7.PerformLayout();
    ((ISupportInitialize) this.NewBox).EndInit();
    this.panel6.ResumeLayout(false);
    this.SchemesPanel.ResumeLayout(false);
    this.rightDock.ResumeLayout(false);
    this.RecentSchemesDock.ResumeLayout(false);
    this.RecentSchemesDock.PerformLayout();
    this.RecentLaunchedDock.ResumeLayout(false);
    this.RecentLaunchedDock.PerformLayout();
    this.ResumeLayout(false);
  }
}
