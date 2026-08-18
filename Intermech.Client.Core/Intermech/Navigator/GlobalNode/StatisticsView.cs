
// Type: Intermech.Navigator.GlobalNode.StatisticsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.GlobalNode;

/// <summary>Summary description for StatisticsView.</summary>
internal class StatisticsView : UserControl, IView
{
  private Panel panel1;
  private Button btInvoke;
  private Button btChildMenu;
  private Button btClearChildren;
  private Button btGetData;
  private Button btToggleNodes;
  private Button btClearChecks;
  private Button btEnable;
  private SplitContainer splitContainer;
  private NavigatorTreeView treeView;
  private ListBox listBox1;
  private IContainer components;

  public StatisticsView() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StatisticsView));
    this.splitContainer = new SplitContainer();
    this.treeView = new NavigatorTreeView();
    this.listBox1 = new ListBox();
    this.panel1 = new Panel();
    this.btInvoke = new Button();
    this.btChildMenu = new Button();
    this.btClearChildren = new Button();
    this.btGetData = new Button();
    this.btToggleNodes = new Button();
    this.btClearChecks = new Button();
    this.btEnable = new Button();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.treeView.BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer, "splitContainer");
    this.splitContainer.FixedPanel = FixedPanel.Panel2;
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.Controls.Add((Control) this.treeView);
    this.splitContainer.Panel2.Controls.Add((Control) this.listBox1);
    this.treeView.AllowDrop = true;
    this.treeView.AllowUserPinnedColumns = false;
    this.treeView.BackgroundImageMode = ImageDrawMode.Tile;
    this.treeView.BorderStyle = BorderStyle.Fixed3D;
    this.treeView.DisableCheckedOutColumn = true;
    this.treeView.DisableKeyDownEvents = true;
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("treeView.HeaderStyle.HorzAlignment");
    this.treeView.LineStyle = LineStyle.Dot;
    this.treeView.MultiSelect = true;
    this.treeView.Name = "treeView";
    this.treeView.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("treeView.RowEvenStyle.WordWrap");
    this.treeView.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("treeView.RowOddStyle.WordWrap");
    this.treeView.RowSelectedStyle.BackColor = SystemColors.Highlight;
    this.treeView.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("treeView.RowSelectedStyle.WordWrap");
    this.treeView.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.treeView.RowStyle.BorderColor = SystemColors.Control;
    this.treeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.treeView.RowStyle.BorderWidth = 1;
    this.treeView.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("treeView.RowStyle.WordWrap");
    this.treeView.SelectBeforeEdit = true;
    this.treeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.treeView.ShowRootRow = false;
    this.treeView.SuppressErrorMessages = true;
    this.treeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.treeView_AfterFocusNode);
    componentResourceManager.ApplyResources((object) this.listBox1, "listBox1");
    this.listBox1.Name = "listBox1";
    this.panel1.Controls.Add((Control) this.btInvoke);
    this.panel1.Controls.Add((Control) this.btChildMenu);
    this.panel1.Controls.Add((Control) this.btClearChildren);
    this.panel1.Controls.Add((Control) this.btGetData);
    this.panel1.Controls.Add((Control) this.btToggleNodes);
    this.panel1.Controls.Add((Control) this.btClearChecks);
    this.panel1.Controls.Add((Control) this.btEnable);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btInvoke, "btInvoke");
    this.btInvoke.Name = "btInvoke";
    this.btInvoke.Click += new EventHandler(this.btInvoke_Click);
    componentResourceManager.ApplyResources((object) this.btChildMenu, "btChildMenu");
    this.btChildMenu.Name = "btChildMenu";
    this.btChildMenu.Click += new EventHandler(this.btChildMenu_Click);
    componentResourceManager.ApplyResources((object) this.btClearChildren, "btClearChildren");
    this.btClearChildren.Name = "btClearChildren";
    this.btClearChildren.Click += new EventHandler(this.btClearChildren_Click);
    componentResourceManager.ApplyResources((object) this.btGetData, "btGetData");
    this.btGetData.Name = "btGetData";
    this.btGetData.Click += new EventHandler(this.btGetData_Click);
    componentResourceManager.ApplyResources((object) this.btToggleNodes, "btToggleNodes");
    this.btToggleNodes.Name = "btToggleNodes";
    this.btToggleNodes.Click += new EventHandler(this.btToggleNodes_Click);
    componentResourceManager.ApplyResources((object) this.btClearChecks, "btClearChecks");
    this.btClearChecks.Name = "btClearChecks";
    this.btClearChecks.Click += new EventHandler(this.btClearChecks_Click);
    componentResourceManager.ApplyResources((object) this.btEnable, "btEnable");
    this.btEnable.Name = "btEnable";
    this.btEnable.Click += new EventHandler(this.btEnable_Click);
    this.Controls.Add((Control) this.splitContainer);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (StatisticsView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.ResumeLayout(false);
    this.treeView.EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this.treeView.Services = services;
  }

  public void Activate(IView previousView)
  {
    if (this.treeView.FocusedNode != null)
      return;
    this.treeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    this.treeView.Build((IDescriptor) new Descriptor());
    if (this.treeView.RootNode == null)
      return;
    this.treeView.RootNode.Expanded = true;
  }

  public void Deactivate(IView nextView)
  {
  }

  private void btEnable_Click(object sender, EventArgs e)
  {
    this.treeView.CheckBoxStyle = this.treeView.CheckBoxStyle == NavigatorTreeViewCheckBoxStyle.None ? NavigatorTreeViewCheckBoxStyle.ThreeState : NavigatorTreeViewCheckBoxStyle.None;
    this.btEnable.Text = this.treeView.CheckBoxStyle == NavigatorTreeViewCheckBoxStyle.None ? "Enable checks" : "Disable checks";
  }

  private void btClearChecks_Click(object sender, EventArgs e) => this.treeView.CheckedNodesClear();

  private void btToggleNodes_Click(object sender, EventArgs e)
  {
    foreach (NavigatorTreeNode selectedNode in this.treeView.SelectedNodes)
      selectedNode.CheckState = selectedNode.CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
  }

  private void btGetData_Click(object sender, EventArgs e)
  {
    this.listBox1.Items.Clear();
    foreach (NavigatorTreeNode checkedNode in this.treeView.CheckedNodes)
    {
      NavigatorTreeNode navigatorTreeNode = checkedNode;
      if (navigatorTreeNode.NodeID.CategoryID == 1)
      {
        string empty = string.Empty;
        NavigatorTreeNode parent = checkedNode.Parent;
        if (parent != null)
        {
          IDBObjectID data = (IDBObjectID) parent.Handler.GetData(navigatorTreeNode.NodeID, typeof (IDBObjectID));
          if (data != null)
            empty = data.Value.ToString();
        }
        string str = "Database object " + checkedNode.GetDisplayText(0);
        if (empty != string.Empty)
          str = $"{str}, OBJECT_ID = {empty}";
        this.listBox1.Items.Add((object) str);
      }
      else
        this.listBox1.Items.Add((object) ("Non database object " + checkedNode.GetDisplayText(0)));
    }
  }

  private void btClearChildren_Click(object sender, EventArgs e)
  {
    this.treeView.FocusedNode.Children.Clear();
  }

  private void btChildMenu_Click(object sender, EventArgs e)
  {
    if (this.treeView.FocusedNode == null)
      return;
    MenuBarItem menu = Intermech.Navigator.ContextMenu.Services.GetMenu(this.treeView.CheckedItems, this.treeView.Services);
    Point client = this.treeView.PointToClient(Control.MousePosition);
    client.Offset(0, 10);
    NavigatorTreeView treeView = this.treeView;
    Point position = client;
    menu.Show((Control) treeView, position);
  }

  private void btInvoke_Click(object sender, EventArgs e)
  {
    ArrayList arrayList = new ArrayList();
    foreach (NavigatorTreeNode checkedNode in this.treeView.CheckedNodes)
    {
      NavigatorTreeNode navigatorTreeNode = checkedNode;
      if (navigatorTreeNode.NodeID.CategoryID == 1)
      {
        INode nodeHandler = this.treeView.GetNodeHandler(checkedNode);
        if (nodeHandler != null && nodeHandler.GetData(navigatorTreeNode.NodeID, typeof (IDBObjectID)) is IDBObjectID data)
          arrayList.Add((object) data.Value);
      }
    }
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems((long[]) arrayList.ToArray(typeof (long)));
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2, false), (System.IServiceProvider) viewServices1);
  }

  private void treeView_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    NavigatorTreeNode node = e.Node;
    NavigatorTreeNode navigatorTreeNode = node;
    if (navigatorTreeNode.NodeID.CategoryID == 1)
    {
      INode nodeHandler = this.treeView.GetNodeHandler(node);
      if (nodeHandler == null || !(nodeHandler.GetData(navigatorTreeNode.NodeID, typeof (IDBObjectID)) is IDBObjectID data))
        return;
      ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(data.Value);
      ServiceContainer serviceContainer = new ServiceContainer();
      serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService());
      ServiceContainer viewServices = serviceContainer;
      this.btInvoke.Enabled = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices, false).Contains("OpenInNewWindow");
    }
    else
      this.btInvoke.Enabled = false;
  }

  public string Caption => LocalizationHolder.rm.GetString("Client.Core_613");

  public int OrderID => 1000;

  public int ImageIndex => -1;
}
