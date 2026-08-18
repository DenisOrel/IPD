// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DatabaseConfiguratorControl
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Search;
using Intermech.Search.Configuration;
using NJFLib.Controls;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class DatabaseConfiguratorControl : 
  DockControl,
  ICommandTarget,
  System.IServiceProvider,
  IGuid,
  IDatabaseConfiguratorControl
{
  private Panel panelMain;
  private IContainer components;
  private Panel buttonPanel;
  private Button buttonCancel;
  private Button buttonApply;
  private Panel Panel;
  private Panel placePanel;
  private TreeView treeView;
  private bool _eventAdded;
  private bool rereadAfterDelete;
  private ServiceContainer services;
  internal static Guid _databaseConfiguratorControlGuid = new Guid("{CA6135C4-40F3-4fad-83B7-CCE417ECB318}");
  private readonly Guid instGuid = Guid.NewGuid();
  private SearchOptions searchOptions = new SearchOptions();
  private ConfiguratorAction _peConfiguratorAction;
  private int _peCategory;
  private object[] _peArgs;
  private readonly Keys[] engKeys = new Keys[3]
  {
    Keys.C,
    Keys.L,
    Keys.R
  };
  private bool[] engPressed = new bool[3];
  internal CustomFolder RootAttributesFolder;
  internal CustomFolder RootObjectTypesFolder;
  internal CustomFolder RootRelationTypesFolder;
  private const string TreeViewWidth = "treeView.Width";
  private DatabaseConfiguratorFindForm dcFindForm;
  private TreeNode popupNode;
  private Button btnActionOk;
  private Button btnActionCancel;
  private MenuBar menuBar;
  private ContextMenuBarItem contextMenuExt;
  protected EventsDispatcher eventsDispatcher = new EventsDispatcher();
  protected IAddressService addressService;
  private CollapsibleSplitter splitter;
  protected TreeViewNavigator navigator;
  [Obsolete("Необходимость в флаге отпала после svn 90199-90200 / bb N1585960")]
  private bool needExpandAll = true;

  public Guid InstGuid => this.instGuid;

  internal TreeView DatabaseConfiguratorTreeView => this.treeView;

  public ConfiguratorAction GetConfiguratorAction() => this._peConfiguratorAction;

  public event ApplyEventHandler ExternalApply;

  public event EventHandler ExternalCancel;

  private DatabaseConfiguratorFindForm DCFormForm
  {
    get
    {
      if (this.dcFindForm == null)
      {
        this.dcFindForm = new DatabaseConfiguratorFindForm((Control) this);
        this.dcFindForm.Disposed += new EventHandler(this.FindFormDisposed);
      }
      return this.dcFindForm;
    }
  }

  private void FindFormDisposed(object sender, EventArgs e)
  {
    this.dcFindForm = (DatabaseConfiguratorFindForm) null;
  }

  public DatabaseConfiguratorControl()
  {
    this.InitializeComponent();
    this.Guid = DatabaseConfiguratorControl._databaseConfiguratorControlGuid;
    this.services = new ServiceContainer();
    this.addressService = ServicesManager.GetService(typeof (IAddressService)) as IAddressService;
    this.navigator = new TreeViewNavigator(this.treeView);
    this.navigator.AddressService = this.addressService;
    this.services.AddService(typeof (INavigate), (object) this.navigator);
    PropertyFormsHolder.RegisterPropertyForms(this.instGuid);
    TabPagesHolder.RegisterTabPages(this.instGuid);
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.WasChangedEventHandler(this.WasChanged));
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.ApplyEventHandler(this.ApplyIt));
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.CancelEventHandler(this.CancelIt));
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.TabControlPageOpeningEventHandler(this.TabControlPageOpening));
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.FolderDClickEventHandler(this.FolderDClick));
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.JumpToAttribute4CustomTypeEventHandler(this.JumpToAttribute4CustomType));
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.JumpToConfiguratorTreeNodeEventHandler(this.JumpToConfiguratorTreeNode));
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.ReloadConfiguratorTreeEventHandler(this.ReloadConfiguratorTree));
    this._eventAdded = true;
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiAdd, new EventHandler(this.add_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiAddGroup, new EventHandler(this.add_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiExclude, new EventHandler(this.exclude_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiDelete, new EventHandler(this.delete_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiUpdate, new EventHandler(this.update_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiCopy, new EventHandler(this.copy_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiCut, new EventHandler(this.cut_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiPaste, new EventHandler(this.paste_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiExportImage, new EventHandler(this.exportImage_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiOpenInNewWindow, new EventHandler(this.openInNewWindow_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiClone, new EventHandler(this.clone_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiFind, new EventHandler(this.find_mi_Click));
    if (ClientConsts.InDeveloperMode)
      this.eventsDispatcher.RegisterAction(ContextMenuID.cmiLocalizationConfig, new EventHandler(this.localizationConfig_mi_Click));
    this.eventsDispatcher.RegisterAction(ContextMenuID.cmiSetSystemGuid, new EventHandler(this.setSystemGuid_mi_Click));
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
    {
      this.TabImageIndex = service.ImageIndex("imgDatabaseConfigurator");
      this.ShowImageInDocumentTab = true;
      this.menuBar.ImageList = service.ImageList;
    }
    this.treeView.TreeViewNodeSorter = (IComparer) new DatabaseConfiguratorNodeSorter();
    this.ApplyVisualSettings();
  }

  private void ApplyVisualSettings()
  {
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_TreeFont) is Font font))
      return;
    this.treeView.Font = font;
  }

  public override void Activated()
  {
    base.Activated();
    if (this.addressService != null)
      this.addressService.Enabled = true;
    this.navigator.UpdateAddress(this.treeView.SelectedNode != null ? this.treeView.SelectedNode.FullPath : string.Empty);
  }

  public override void Deactivated()
  {
    base.Deactivated();
    if (this.addressService == null)
      return;
    this.addressService.Enabled = false;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this._eventAdded)
      {
        EventsHolder.UnregisterEvent(this.instGuid, typeof (EventsHolder.WasChangedEventHandler));
        EventsHolder.UnregisterEvent(this.instGuid, typeof (EventsHolder.ApplyEventHandler));
        EventsHolder.UnregisterEvent(this.instGuid, typeof (System.ComponentModel.CancelEventHandler));
        EventsHolder.UnregisterEvent(this.instGuid, typeof (EventsHolder.TabControlPageOpeningEventHandler));
        EventsHolder.UnregisterEvent(this.instGuid, typeof (EventsHolder.FolderDClickEventHandler));
        EventsHolder.UnregisterEvent(this.instGuid, typeof (EventsHolder.JumpToAttribute4CustomTypeEventHandler));
        EventsHolder.UnregisterEvent(this.instGuid, typeof (EventsHolder.JumpToConfiguratorTreeNodeEventHandler));
        EventsHolder.UnregisterEvent(this.instGuid, typeof (EventsHolder.ReloadConfiguratorTreeEventHandler));
        TabPagesHolder.UnregisterTabPages(this.instGuid);
        PropertyFormsHolder.UnregisterPropertyForms(this.instGuid);
        this._eventAdded = false;
      }
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DatabaseConfiguratorControl));
    this.panelMain = new Panel();
    this.Panel = new Panel();
    this.placePanel = new Panel();
    this.menuBar = new MenuBar();
    this.contextMenuExt = new ContextMenuBarItem();
    this.treeView = new TreeView();
    this.buttonPanel = new Panel();
    this.buttonCancel = new Button();
    this.buttonApply = new Button();
    this.btnActionCancel = new Button();
    this.btnActionOk = new Button();
    this.splitter = new CollapsibleSplitter();
    this.panelMain.SuspendLayout();
    this.Panel.SuspendLayout();
    this.placePanel.SuspendLayout();
    this.buttonPanel.SuspendLayout();
    this.SuspendLayout();
    this.panelMain.Controls.Add((Control) this.Panel);
    this.panelMain.Controls.Add((Control) this.splitter);
    this.panelMain.Controls.Add((Control) this.treeView);
    componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
    this.panelMain.Name = "panelMain";
    this.Panel.Controls.Add((Control) this.placePanel);
    this.Panel.Controls.Add((Control) this.buttonPanel);
    componentResourceManager.ApplyResources((object) this.Panel, "Panel");
    this.Panel.Name = "Panel";
    this.placePanel.Controls.Add((Control) this.menuBar);
    componentResourceManager.ApplyResources((object) this.placePanel, "placePanel");
    this.placePanel.Name = "placePanel";
    this.menuBar.Guid = new Guid("7fe2eca7-b33e-49e0-bd73-9a9c3aad1ccd");
    this.menuBar.Hidden = false;
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuExt
    });
    componentResourceManager.ApplyResources((object) this.menuBar, "menuBar");
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.contextMenuExt, "contextMenuExt");
    this.contextMenuExt.ShowText = true;
    this.contextMenuExt.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuExt_BeforePopup);
    this.treeView.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.HideSelection = false;
    this.treeView.ItemHeight = 18;
    this.treeView.Name = "treeView";
    this.menuBar.SetPopupMenu((Control) this.treeView, (MenuBarItem) this.contextMenuExt);
    this.treeView.BeforeExpand += new TreeViewCancelEventHandler(this.treeView_BeforeExpand);
    this.treeView.BeforeSelect += new TreeViewCancelEventHandler(this.treeView_BeforeSelect);
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    this.treeView.KeyDown += new KeyEventHandler(this.treeView_KeyDown);
    this.treeView.KeyUp += new KeyEventHandler(this.treeView_KeyUp);
    this.buttonPanel.Controls.Add((Control) this.buttonCancel);
    this.buttonPanel.Controls.Add((Control) this.buttonApply);
    this.buttonPanel.Controls.Add((Control) this.btnActionCancel);
    this.buttonPanel.Controls.Add((Control) this.btnActionOk);
    componentResourceManager.ApplyResources((object) this.buttonPanel, "buttonPanel");
    this.buttonPanel.Name = "buttonPanel";
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
    componentResourceManager.ApplyResources((object) this.buttonApply, "buttonApply");
    this.buttonApply.Name = "buttonApply";
    this.buttonApply.Click += new EventHandler(this.buttonApply_Click);
    componentResourceManager.ApplyResources((object) this.btnActionCancel, "btnActionCancel");
    this.btnActionCancel.Name = "btnActionCancel";
    this.btnActionCancel.Click += new EventHandler(this.buttonCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnActionOk, "btnActionOk");
    this.btnActionOk.Name = "btnActionOk";
    this.btnActionOk.Click += new EventHandler(this.buttonApply_Click);
    this.splitter.AnimationDelay = 1;
    this.splitter.AnimationStep = 2000;
    this.splitter.BorderStyle3D = Border3DStyle.Flat;
    this.splitter.ControlToHide = (Control) this.treeView;
    this.splitter.ExpandParentForm = false;
    componentResourceManager.ApplyResources((object) this.splitter, "splitter");
    this.splitter.Name = "splitter";
    this.splitter.TabStop = false;
    this.splitter.UseAnimations = true;
    this.splitter.VisualStyle = VisualStyles.Mozilla;
    this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Document;
    this.Controls.Add((Control) this.panelMain);
    this.HideOnClose = true;
    this.Name = nameof (DatabaseConfiguratorControl);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "   ";
    this.Closing += new System.ComponentModel.CancelEventHandler(this.DatabaseConfiguratorControl_Closing);
    this.Load += new EventHandler(this.DatabaseConfiguratorControl_Load);
    this.panelMain.ResumeLayout(false);
    this.Panel.ResumeLayout(false);
    this.placePanel.ResumeLayout(false);
    this.buttonPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private TreeNode FindFolderById(TreeNode ownerNode, object id, bool recursive)
  {
    TreeNode folderById = (TreeNode) null;
    for (int index = 0; index < ownerNode.Nodes.Count; ++index)
    {
      if (ownerNode.Nodes[index].Tag is IFolder && (ownerNode.Nodes[index].Tag as IFolder).Id.Equals(id))
      {
        folderById = ownerNode.Nodes[index];
        break;
      }
      if (recursive)
      {
        folderById = this.FindFolderById(ownerNode.Nodes[index], id, recursive);
        if (folderById != null)
          break;
      }
    }
    return folderById;
  }

  private void DatabaseConfiguratorControl_Load(object sender, EventArgs e)
  {
    this.splitter.AnimationStep = Screen.GetBounds((Control) this).Width;
    this.FillTreeView();
    bool flag = false;
    switch (this._peConfiguratorAction)
    {
      case ConfiguratorAction.None:
        flag = true;
        break;
      case ConfiguratorAction.Add:
        if (this._peCategory == 3)
        {
          (this.treeView.Nodes[0].Tag as IFolder).Populate(false);
          TreeNode folderById = this.FindFolderById(this.treeView.Nodes[0], (object) -1, false);
          if (folderById == null)
          {
            flag = true;
            break;
          }
          this.treeView.SelectedNode = folderById;
          this.popupNode = folderById;
          this.add_mi_Click((object) (folderById.Tag as CustomFolder).MIAdd, new EventArgs());
          break;
        }
        flag = true;
        break;
      case ConfiguratorAction.Edit:
        if (this._peCategory == 3)
        {
          (this.treeView.Nodes[0].Tag as IFolder).Populate(false);
          TreeNode folderById1 = this.FindFolderById(this.treeView.Nodes[0], (object) -1, false);
          if (folderById1 == null)
          {
            flag = true;
            break;
          }
          (folderById1.Tag as IFolder).Populate(false);
          TreeNode folderById2 = this.FindFolderById(folderById1, this._peArgs[0], false);
          if (folderById2 == null)
          {
            flag = true;
            break;
          }
          this.treeView.SelectedNode = folderById2;
          break;
        }
        flag = true;
        break;
    }
    if (flag)
      this.treeView.SelectedNode = this.treeView.Nodes[0];
    HybridDictionary hybridDictionary = new HybridDictionary();
    FormStorage.LoadLayout((Control) this, (IDictionary) hybridDictionary, true, out Point _, out Size _);
    object obj = hybridDictionary[(object) "treeView.Width"];
    if (obj == null)
      return;
    this.treeView.Width = Convert.ToInt32(obj);
  }

  private void FillTreeView()
  {
    bool flag = this._peConfiguratorAction == ConfiguratorAction.None;
    this.treeView.Visible = flag;
    this.treeView.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    if (flag || this._peCategory == 4)
      this.RootObjectTypesFolder = (CustomFolder) new ObjectTypesFolder(this.instGuid, LocalizationHolder.rm.GetString("DatabaseConfigurator_149"), (object) this.treeView);
    if (flag || this._peCategory == 6)
      this.RootRelationTypesFolder = (CustomFolder) new RelationTypesFolder(this.instGuid, LocalizationHolder.rm.GetString("DatabaseConfigurator_150"), (object) this.treeView);
    if (flag || this._peCategory == 3)
      this.RootAttributesFolder = (CustomFolder) new AttributesFolder(this.instGuid, LocalizationHolder.rm.GetString("DatabaseConfigurator_151"), (object) this.treeView);
    if (flag || this._peCategory == 8)
    {
      LevelsFolder levelsFolder = new LevelsFolder(this.instGuid, LocalizationHolder.rm.GetString("DatabaseConfigurator_152"), (object) this.treeView);
    }
    if (flag || this._peCategory == 16 /*0x10*/)
    {
      LCSchemasFolder lcSchemasFolder = new LCSchemasFolder(this.instGuid, LocalizationHolder.rm.GetString("DatabaseConfigurator_153"), (object) this.treeView);
    }
    if (flag || this._peCategory == 11)
    {
      AreasFolder areasFolder = new AreasFolder(this.instGuid, LocalizationHolder.rm.GetString("DatabaseConfigurator_154"), (object) this.treeView);
    }
    if (flag || this._peCategory == 9)
    {
      LanguagesFolder languagesFolder = new LanguagesFolder(this.instGuid, LocalizationHolder.rm.GetString("DatabaseConfigurator_155"), (object) this.treeView);
    }
    if (!flag && this._peCategory != 14)
      return;
    SystemFolder systemFolder = new SystemFolder(this.instGuid, LocalizationHolder.rm.GetString("DatabaseConfigurator_156"), (object) this.treeView);
  }

  internal TreeNode GetFocusedNode()
  {
    return this.treeView.GetNodeAt(this.treeView.PointToClient(Control.MousePosition)) ?? this.treeView.SelectedNode;
  }

  private void contextMenu_Popup(object sender, EventArgs e)
  {
  }

  private void add_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      if (ClientConsts.IsFakeNode(tag.Node))
        tag.Populate(false);
      this.treeView.SelectedNode = tag.AddChild((MenuButtonItem) sender).Node;
      this.UpdateNodeInfo(tag.Node);
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void DeleteObjectTypeProcessing(IFolder ifolder)
  {
    if (ifolder == null || ifolder.Node.Tag.GetType() != typeof (ObjectTypeFolder))
      return;
    int id = (int) ((DBPropDescriptorHolder) ifolder).Id;
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    DBObjectTypesEventArgs e = new DBObjectTypesEventArgs("ObjectTypesRemoved", id);
    if (service == null || e == null)
      return;
    service.FireEvent((object) null, (NotificationEventArgs) e);
  }

  private void RemoveNodeFromGroupByID(IFolder groupNode, int aId)
  {
    if (groupNode == null || ClientConsts.IsFakeNode(groupNode.Node))
      return;
    for (int index = 0; index < groupNode.Node.Nodes.Count; ++index)
    {
      if ((int) (groupNode.Node.Nodes[index].Tag as IFolder).Id == aId)
      {
        groupNode.Node.Nodes[index].Remove();
        break;
      }
    }
  }

  private void RemoveNodeFromGroupByIDRecursive(
    IFolder ifolder,
    int attributeId,
    int attributeGroupId)
  {
    TreeNodeCollection nodes = ifolder.Node.Nodes;
    for (int index = 0; index < nodes.Count; ++index)
    {
      if (nodes[index].Tag is AttributeGroupFolder && !ClientConsts.IsFakeNode(nodes[index]))
        this.RemoveNodeFromGroupByIDRecursive(nodes[index].Tag as IFolder, attributeId, attributeGroupId);
      if (nodes[index].Tag is AttributeFolder && (int) (nodes[index].Tag as IFolder).Id == attributeId && (int) (nodes[index].Parent.Tag as IFolder).Id != attributeGroupId)
      {
        int id = (int) (nodes[index].Parent.Tag as IFolder).Id;
        nodes[index].Remove();
        DataHolders.AttributesHolder.ClearInfo((object) id);
        break;
      }
    }
  }

  private void DeleteAttributeCustomProcessing(IFolder ifolder)
  {
    if (ifolder.Node.Tag.GetType() != typeof (AttributeFolder))
      return;
    int id1 = (int) ((DBPropDescriptorHolder) ifolder).Id;
    int id2 = (int) ((DBPropDescriptorHolder) ifolder.NodeParent.Tag).Id;
    if (id2 != -1)
    {
      this.RemoveNodeFromGroupByID((IFolder) this.GetAllAttributesGroup(), id1);
      this.RemoveNodeFromGroupByID((IFolder) this.GetTypeAssignedAttributesGroup(), id1);
      DataHolders.AttributesHolder.ClearInfo((object) -1);
    }
    else
      this.RemoveNodeFromGroupByID((IFolder) this.GetTypeAssignedAttributesGroup(), id1);
    for (int index = 0; index < this.RootAttributesFolder.Node.Nodes.Count; ++index)
    {
      switch ((int) (this.RootAttributesFolder.Node.Nodes[index].Tag as IFolder).Id)
      {
        case -10:
        case -1:
          continue;
        default:
          if (!ClientConsts.IsFakeNode(this.RootAttributesFolder.Node.Nodes[index]))
          {
            this.RemoveNodeFromGroupByIDRecursive(this.RootAttributesFolder.Node.Nodes[index].Tag as IFolder, id1, id2);
            continue;
          }
          continue;
      }
    }
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    DBAttributesEventArgs e = new DBAttributesEventArgs("AttributeRemoved", id1);
    if (service == null || e == null)
      return;
    service.FireEvent((object) null, (NotificationEventArgs) e);
  }

  private void exclude_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      this.rereadAfterDelete = true;
      tag.Exclude();
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void delete_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      this.rereadAfterDelete = true;
      TreeNode nodeParent = tag.NodeParent;
      if (tag.Delete(new EventHandler(this.OnDeleteIFolder)) == ActionResult.Cancel)
        return;
      this.UpdateNodeInfo(nodeParent);
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void OnDeleteIFolder(object sender, EventArgs args)
  {
    if (!(sender is IFolder))
      return;
    this.DeleteAttributeCustomProcessing(sender as IFolder);
    this.DeleteObjectTypeProcessing(sender as IFolder);
  }

  private void update_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      if (popupNode == this.treeView.SelectedNode)
        tag.Update();
      else
        tag.Populate(true);
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void copy_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      tag.Copy();
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void cut_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      tag.Cut();
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void exportImage_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      tag.ExportImage();
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void openInNewWindow_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    IFolder tag = popupNode.Tag as IFolder;
    Intermech.Navigator.Utils.OpenNewWindow(tag is AllObjectTypesFolder ? (IDescriptor) new ObjectTypesNodeDescriptor() : (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor((int) tag.Id), (System.IServiceProvider) null);
  }

  private void localizationConfig_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      tag.LocalizationConfig();
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void clone_mi_Click(object sender, EventArgs e)
  {
    IFolder folder = (IFolder) null;
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      folder = tag.Clone();
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
    if (folder == null)
      return;
    this.treeView.SelectedNode = folder.Node;
  }

  private void paste_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag))
        return;
      tag.Paste();
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void setSystemGuid_mi_Click(object sender, EventArgs e)
  {
    TreeNode popupNode = this.popupNode;
    if (popupNode == null)
      return;
    try
    {
      if (!(popupNode.Tag is IFolder tag) || MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_215"), LocalizationHolder.rm.GetString("DatabaseConfigurator_158"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      tag.SetSystemGuid();
    }
    finally
    {
      this.popupNode = (TreeNode) null;
    }
  }

  private void rights_mi_Click(object sender, EventArgs e)
  {
  }

  private void find_mi_Click(object sender, EventArgs e)
  {
    this.DCFormForm.Show(this.searchOptions);
  }

  public IFolder GetAllAttributesGroupFolder() => (IFolder) this.GetAllAttributesGroup();

  private AttributeGroupFolder GetAllAttributesGroup()
  {
    AttributeGroupFolder allAttributesGroup = (AttributeGroupFolder) null;
    if (ClientConsts.IsFakeNode(this.RootAttributesFolder.Node))
      this.RootAttributesFolder.Populate(false, false);
    for (int index = 0; index < this.RootAttributesFolder.Node.Nodes.Count; ++index)
    {
      if ((int) (this.RootAttributesFolder.Node.Nodes[index].Tag as AttributeGroupFolder).Id == -1)
      {
        allAttributesGroup = (AttributeGroupFolder) this.RootAttributesFolder.Node.Nodes[index].Tag;
        break;
      }
    }
    return allAttributesGroup;
  }

  private AttributeGroupFolder GetTypeAssignedAttributesGroup()
  {
    AttributeGroupFolder assignedAttributesGroup = (AttributeGroupFolder) null;
    for (int index = 0; index < this.RootAttributesFolder.Node.Nodes.Count; ++index)
    {
      if ((int) (this.RootAttributesFolder.Node.Nodes[index].Tag as AttributeGroupFolder).Id == -10)
      {
        assignedAttributesGroup = (AttributeGroupFolder) this.RootAttributesFolder.Node.Nodes[index].Tag;
        break;
      }
    }
    return assignedAttributesGroup;
  }

  private void ApplyAttributeCustomProcessing(IFolder ifolder)
  {
    if (ifolder.Node.Tag.GetType() != typeof (AttributeFolder))
      return;
    if ((int) ((DBPropDescriptorHolder) ifolder.NodeParent.Tag).Id != -1)
    {
      IFolder allAttributesGroup = (IFolder) this.GetAllAttributesGroup();
      if (allAttributesGroup != null && !ClientConsts.IsFakeNode(allAttributesGroup.Node))
      {
        if (!ifolder.IsVirtualFolder)
        {
          for (int index = 0; index < allAttributesGroup.Node.Nodes.Count; ++index)
          {
            if ((int) (allAttributesGroup.Node.Nodes[index].Tag as IFolder).Id == (int) ifolder.Id)
            {
              allAttributesGroup.Node.Nodes[index].Remove();
              break;
            }
          }
        }
        allAttributesGroup.AddChildDubbed(ifolder.Node.Tag as IFolder);
        DataHolders.AttributesHolder.ClearInfo((object) -1);
      }
    }
    else
    {
      for (int index1 = 0; index1 < this.RootAttributesFolder.Node.Nodes.Count; ++index1)
      {
        int id = (int) (this.RootAttributesFolder.Node.Nodes[index1].Tag as IFolder).Id;
        if (id != -1 && !ClientConsts.IsFakeNode(this.RootAttributesFolder.Node.Nodes[index1]))
        {
          TreeNodeCollection nodes = this.RootAttributesFolder.Node.Nodes[index1].Nodes;
          for (int index2 = 0; index2 < nodes.Count; ++index2)
          {
            if (nodes[index2].Tag is AttributeFolder && (int) (nodes[index2].Tag as IFolder).Id == (int) ifolder.Id)
            {
              nodes[index2].Remove();
              (this.RootAttributesFolder.Node.Nodes[index1].Tag as IFolder).AddChildDubbed(ifolder.Node.Tag as IFolder);
              DataHolders.AttributesHolder.ClearInfo((object) id);
              break;
            }
          }
        }
      }
    }
    this.AddAttributeCustomExternalProcessing(ifolder);
  }

  private void AddAttributeCustomExternalProcessing(IFolder ifolder)
  {
    if (ifolder.Node.Tag.GetType() != typeof (AttributeFolder) || this._peCategory != 3 || this._peConfiguratorAction != ConfiguratorAction.Add || this._peArgs == null || this._peArgs.Length < 2 || this._peArgs[1] == null)
      return;
    IntList peArg = (IntList) this._peArgs[1];
    if (peArg == null || peArg.Count == 1 && peArg[0] == -1)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < peArg.Count; ++index)
      {
        IDBAttributesGroup attributesGroup = sessionKeeper.Session.GetAttributesGroup(peArg[index]);
        if (attributesGroup != null)
        {
          attributesGroup.IncludeAttribute((int) ifolder.Id);
          DataHolders.AttributesHolder.ClearInfo((object) attributesGroup.GroupID);
        }
      }
    }
  }

  private void ApplyObjTypeCustomProcessing(IFolder ifolder)
  {
    if (ifolder.Node.Tag.GetType() != typeof (ObjectTypeFolder))
      return;
    StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage, false);
    if (PropertyFormsHolder.PropertyForms(this.instGuid).PropertyTabPageForm.LastTabPage != TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage)
      return;
    PropertyFormsHolder.PropertyForms(this.instGuid).PropertyTabPageForm.OpenTabPage((System.Windows.Forms.TabPage) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage);
  }

  private bool ApplyModifications(bool withUpdate)
  {
    if (this.treeView.SelectedNode == null || !(this.treeView.SelectedNode.Tag is IFolder tag))
      return false;
    int num1 = tag.IsVirtualFolder ? 1 : 0;
    int num2 = tag.ApplyData() ? 1 : 0;
    this.ButtonsRefresh(tag);
    if (withUpdate)
      this.UpdateNodeInfo(tag.NodeParent);
    this.ApplyAttributeCustomProcessing(tag);
    this.ApplyObjTypeCustomProcessing(tag);
    return num2 != 0;
  }

  private void CancelModifications()
  {
    if (this.treeView.SelectedNode == null || !(this.treeView.SelectedNode.Tag is IFolder tag1))
      return;
    tag1.Cancel();
    this.UpdateNodeInfo(tag1.NodeParent);
    if (this.treeView.SelectedNode == null || !(this.treeView.SelectedNode.Tag is IFolder tag2))
      return;
    this.ButtonsRefresh(tag2);
  }

  public void Apply(object sender)
  {
    EventsHolder.FireApply(sender, this.instGuid, new EventsHolder.BoolArgs(true));
    object data = (object) null;
    if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag is IFolder tag)
      data = tag.Id;
    if (this.ExternalApply == null)
      return;
    this.ExternalApply((object) this, new ApplyEventArgs(data));
  }

  public void Cancel(object sender)
  {
    EventsHolder.FireCancel(sender, this.instGuid, (EventArgs) null);
    try
    {
    }
    finally
    {
      if (this.ExternalCancel != null)
        this.ExternalCancel((object) this, new EventArgs());
    }
  }

  private void buttonApply_Click(object sender, EventArgs e)
  {
    try
    {
      this.Apply(sender);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void UpdateNodeInfo(TreeNode node)
  {
    if (node == null || !(node.Tag is IFolder tag))
      return;
    tag.UpdateData();
  }

  private void buttonCancel_Click(object sender, EventArgs e) => this.Cancel(sender);

  [Obsolete("Необходимость в флаге отпала после svn 90199-90200 / bb N1585960")]
  public bool NeedExpandAll
  {
    get => this.needExpandAll;
    set => this.needExpandAll = value;
  }

  private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    if (e.Action != TreeViewAction.Expand)
      return;
    TreeNode node = e.Node;
    if (node == null)
      return;
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    service?.BeginUpdate();
    try
    {
      IFolder tag1 = (IFolder) node.Tag;
      if (ClientConsts.IsFakeNode(node))
      {
        tag1.Populate(false, !(tag1 is AttributeGroupFolder) && !(tag1 is AttributesFolder));
      }
      else
      {
        for (int index = 0; index < node.Nodes.Count; ++index)
        {
          if (ClientConsts.IsFakeNode(node.Nodes[index]))
          {
            IFolder tag2 = (IFolder) node.Nodes[index].Tag;
            if (!(tag2 is AttributeGroupFolder))
              tag2.Populate(false, false);
          }
        }
      }
    }
    finally
    {
      service?.EndUpdate();
    }
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    IFolder tag = (IFolder) e.Node.Tag;
    if (tag == null)
      return;
    tag.LoadData(this.placePanel, this.rereadAfterDelete);
    this.rereadAfterDelete = false;
    if (tag.InChange && tag.PropertiesForm is IConfigPage propertiesForm && propertiesForm.TabControl != null && propertiesForm.TabControl.TabPages.IndexOf((System.Windows.Forms.TabPage) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage) != -1)
    {
      TabControlProcessor.BlockTabPageChangedEvent = true;
      try
      {
        propertiesForm.TabControl.SelectedTab = (System.Windows.Forms.TabPage) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage;
      }
      finally
      {
        TabControlProcessor.BlockTabPageChangedEvent = false;
      }
      propertiesForm.OpenTabPage((System.Windows.Forms.TabPage) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage);
    }
    this.treeView.Select();
    this.ButtonsRefresh(tag);
  }

  private void ButtonsRefresh(IFolder ifolder)
  {
    this.buttonApply.Visible = this._peConfiguratorAction == ConfiguratorAction.None;
    this.buttonApply.Enabled = ifolder != null && ifolder.InChange;
    this.buttonCancel.Visible = this._peConfiguratorAction == ConfiguratorAction.None;
    this.buttonCancel.Enabled = ifolder != null && ifolder.InChange;
    this.btnActionOk.Visible = this._peConfiguratorAction != 0;
    this.btnActionCancel.Visible = this._peConfiguratorAction != 0;
  }

  private bool ApplyIt(object s, EventsHolder.BoolArgs e) => this.ApplyModifications(e.Boolean);

  private void CancelIt(object s, EventArgs e) => this.CancelModifications();

  private void WasChanged(object s, EventArgs e)
  {
    this.buttonApply.Enabled = true;
    this.buttonCancel.Enabled = true;
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null)
      return;
    IFolder tag = (IFolder) selectedNode.Tag;
    if (tag == null)
      return;
    tag.InChange = true;
    if (!tag.InChange)
      return;
    tag.ChangeEventProcessing(s, e);
  }

  private void TabControlPageOpening(object s, EventsHolder.TabControlPageOpeningArgs e)
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null)
      return;
    IFolder tag = (IFolder) selectedNode.Tag;
    if (tag == null || !tag.NeedPageSave)
      return;
    e.Cancel = !this.CheckNeedSaveChanges();
    this.ButtonsRefresh(tag);
  }

  private void FolderDClick(object s, EventsHolder.FolderArgs e)
  {
    TreeNode node1 = e.IFolder.Node;
    if (node1 == null)
      return;
    IFolder tag = (IFolder) node1.Tag;
    if (tag == null)
      return;
    if (!node1.IsExpanded)
      tag.Populate(false);
    foreach (TreeNode node2 in node1.Nodes)
    {
      if (node2.Tag is IFolder && e.Id != null && ((IFolder) node2.Tag).Id.ToString() == e.Id.ToString())
      {
        this.treeView.SelectedNode = node2;
        break;
      }
    }
  }

  private void JumpToAttribute4CustomType(object s, EventsHolder.JumpToAttribute4CustomTypeArgs e)
  {
    BaseTabPage page = (BaseTabPage) null;
    TreeNode treeNode = (TreeNode) null;
    if (e.Category == 4)
    {
      page = e.AttributeId != 0 ? (BaseTabPage) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage : (BaseTabPage) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage;
      DataTable hierarchy = DataHolders.ObjectTypesHolder.GetHierarchy(false, CoreConsts.FilterRecords);
      ArrayList allParents = ObjectTypesHolder.GetAllParents(e.TypeId, hierarchy);
      TreeNode ownerNode = this.RootObjectTypesFolder.Node;
      if (!ownerNode.IsExpanded)
        (ownerNode.Tag as CustomFolder).Populate(false);
      for (int index = allParents.Count - 1; index >= 0; --index)
      {
        ownerNode = this.FindFolderById(ownerNode, (object) (int) allParents[index], false);
        if (ownerNode == null)
          return;
        if (!ownerNode.IsExpanded)
          (ownerNode.Tag as CustomFolder).Populate(false);
      }
      treeNode = this.FindFolderById(ownerNode, (object) e.TypeId, false);
    }
    if (e.Category == 6)
    {
      page = (BaseTabPage) TabPagesHolder.TabPages(this.instGuid).Attr4RelTypeTabPage;
      this.RootRelationTypesFolder.Populate(false);
      treeNode = this.FindFolderById(this.RootRelationTypesFolder.Node, (object) e.TypeId, false);
    }
    if (treeNode == null)
      return;
    this.treeView.SelectedNode = treeNode;
    if (!(treeNode.Tag is IFolder tag) || !(tag.PropertiesForm is IConfigPage propertiesForm) || propertiesForm.TabControl == null || propertiesForm.TabControl.TabPages.IndexOf((System.Windows.Forms.TabPage) page) == -1)
      return;
    TabControlProcessor.BlockTabPageChangedEvent = true;
    try
    {
      propertiesForm.TabControl.SelectedTab = (System.Windows.Forms.TabPage) page;
    }
    finally
    {
      TabControlProcessor.BlockTabPageChangedEvent = false;
    }
    propertiesForm.OpenTabPage((System.Windows.Forms.TabPage) page);
    if (e.AttributeId == 0 || !(page.TabPageProcessingForm is IPositionAssigner pageProcessingForm))
      return;
    pageProcessingForm.SetPositionAt(3, (object) e.AttributeId);
  }

  private void JumpToConfiguratorTreeNode(object s, EventsHolder.JumpToConfiguratorTreeNodeArgs e)
  {
    if (e == null || e.Category != 3)
      return;
    int id = (int) e.Id;
    IFolder attributesGroupFolder = this.GetAllAttributesGroupFolder();
    attributesGroupFolder.Populate(false);
    TreeNode folderById = this.FindFolderById(attributesGroupFolder.Node, (object) id, false);
    if (folderById == null)
      return;
    this.needExpandAll = false;
    this.treeView.SelectedNode = folderById;
  }

  private void ReloadConfiguratorTree(object s, EventsHolder.ReloadConfiguratorTreeArgs e)
  {
    if (this.CheckNeedSaveChanges())
    {
      this.treeView.Nodes.Clear();
      this.FillTreeView();
      this.treeView.SelectedNode = this.treeView.Nodes[0];
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_NeedManualUpdate"), LocalizationHolder.rm.GetString("DatabaseConfigurator_Attention"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  private void LocalizationConfig(object s, EventsHolder.FolderArgs e)
  {
  }

  private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
  {
    e.Cancel = !this.CheckNeedSaveChanges();
    if (e.Cancel)
      return;
    this.FolderLostFocus();
  }

  private void DatabaseConfiguratorControl_Closing(object sender, CancelEventArgs e)
  {
    e.Cancel = !this.CheckNeedSaveChanges();
    if (!e.Cancel)
      this.FolderLostFocus();
    FormStorage.SaveLayout((Control) this, (IDictionary) new HybridDictionary()
    {
      {
        (object) "treeView.Width",
        (object) this.treeView.Width
      }
    });
    if (e.Cancel || this.dcFindForm == null || !this.dcFindForm.Visible)
      return;
    this.dcFindForm.Close();
  }

  private void FolderLostFocus() => ((IFolder) this.treeView.SelectedNode?.Tag)?.FormLostFocus();

  private bool CheckNeedSaveChanges()
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null)
      return true;
    IFolder tag = (IFolder) selectedNode.Tag;
    if (tag == null || !tag.InChange)
      return true;
    MessageBoxButtons buttons = this._peConfiguratorAction == ConfiguratorAction.None ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.OKCancel;
    DialogResult dialogResult = MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_157"), LocalizationHolder.rm.GetString("DatabaseConfigurator_158"), buttons, MessageBoxIcon.Question);
    if (dialogResult == DialogResult.OK)
      dialogResult = DialogResult.Yes;
    if (dialogResult == DialogResult.Yes)
    {
      if (!tag.ApplyData())
        return false;
      this.UpdateNodeInfo(tag.NodeParent);
    }
    else
    {
      if (dialogResult != DialogResult.No)
        return false;
      tag.Cancel();
      this.UpdateNodeInfo(tag.NodeParent);
    }
    return true;
  }

  public bool Execute(ICommandState commandState)
  {
    if (!(commandState.CommandName == "GotoAddress"))
      return false;
    this.navigator.BrowseAddress();
    return true;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (!(commandState.CommandName == "GotoAddress"))
      return false;
    commandState.Enabled = this.navigator.AddressService != null;
    return true;
  }

  object System.IServiceProvider.GetService(System.Type serviceType)
  {
    return this.services.GetService(serviceType);
  }

  public Guid GUID => this.instGuid;

  public void PrepareModalExecute(ConfiguratorAction action, int category, params object[] args)
  {
    this._peConfiguratorAction = action;
    this._peCategory = category;
    this._peArgs = (object[]) args.Clone();
  }

  private void ClearKeyStates()
  {
    this.engPressed[0] = false;
    this.engPressed[1] = false;
    this.engPressed[2] = false;
  }

  private void treeView_KeyDown(object sender, KeyEventArgs e)
  {
    int keyCode = (int) e.KeyCode;
    if ((Keys) keyCode == this.engKeys[0])
      this.engPressed[0] = true;
    if ((Keys) keyCode == this.engKeys[1])
      this.engPressed[1] = true;
    if ((Keys) keyCode == this.engKeys[2])
      this.engPressed[2] = true;
    if (keyCode == 112 /*0x70*/)
      HelpProvidersClass.ShowHelpTopic(this.ShowHelpForConfigarator());
    if (!this.engPressed[0] || !this.engPressed[1] || !this.engPressed[2])
      return;
    ConfigCache.Empty();
    this.ClearKeyStates();
  }

  private int ShowHelpForConfigarator()
  {
    TreeNode focusedNode = this.GetFocusedNode();
    if (focusedNode == null)
      return 1003;
    switch (focusedNode.Tag as IFolder)
    {
      case AttributeGroupFolder _:
        return 1014;
      case AttributesFolder _:
      case AttributeFolder _:
        return 1006;
      case ObjectTypesFolder _:
      case ObjectTypeFolder _:
      case AllObjectTypesFolder _:
        return 1019;
      case RelationTypeFolder _:
      case RelationTypesFolder _:
        return 1030;
      case LCSchemasFolder _:
      case LCSchemaFolder _:
        return 1042;
      case AreasFolder _:
      case AreaFolder _:
        return 1048;
      case LanguagesFolder _:
      case LanguageFolder _:
        return 1053;
      case SystemFolder _:
        return 1058;
      case LevelFolder _:
      case LevelsFolder _:
        return 1037;
      default:
        return 1003;
    }
  }

  private void treeView_KeyUp(object sender, KeyEventArgs e)
  {
    int keyCode = (int) e.KeyCode;
    if (keyCode == 67)
      this.engPressed[0] = false;
    if (keyCode == 76)
      this.engPressed[1] = false;
    if (keyCode != 82)
      return;
    this.engPressed[2] = false;
  }

  private void contextMenuExt_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.popupNode = (TreeNode) null;
    TreeNode focusedNode = this.GetFocusedNode();
    if (focusedNode == null)
      AbortException.Abort();
    if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null && ((CustomFolder) this.treeView.SelectedNode.Tag).InChange)
      AbortException.Abort();
    IFolder tag = (IFolder) focusedNode.Tag;
    if (tag == null)
      AbortException.Abort();
    tag.GetContextMenu(this.contextMenuExt, (IEventsDispatcher) this.eventsDispatcher);
    tag.SetContextMenuItemStatus(this.contextMenuExt);
    this.popupNode = focusedNode;
  }

  public override string HelpID
  {
    get
    {
      return this.treeView.Focused ? this.ShowHelpForConfigarator().ToString() : PropertyFormsHolder.PropertyForms(this.instGuid).PropertyTabPageForm.helpTopicID;
    }
  }
}
