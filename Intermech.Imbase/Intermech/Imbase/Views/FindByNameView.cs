// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.FindByNameView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Docking;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class FindByNameView : DockControl, IImbaseView
{
  protected long _targetId;
  protected NavigatorTreeNode _parentINode;
  protected TreeNode _parentTNode;
  protected LocateNodeEventHandler _locateHandler;
  protected Icon _ico;
  private bool _loading;
  private static AutoCompleteStringCollection _autoCompleteSource = new AutoCompleteStringCollection();
  private IContainer components;
  private TreeView _treeView;
  private SplitContainer splitContainer1;
  private Button button1;
  private ComboBox cbFindData;
  private Label label2;
  private Label label1;
  private ListView lbResult;
  private CheckBox cbRegister;
  private Button btFind;
  private ComboBox cbFindMode;
  private Label label3;
  private Button btnFindInMainWin;
  private Panel panel1;
  private ToolTip toolTip1;
  private TextBox textBox1;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;

  public virtual Icon Icon
  {
    get
    {
      if (this._ico != null)
        return this._ico;
      this._ico = Intermech.Imbase.ResourceHelper.GetResourceData<Icon>(this.GetType().Assembly, "Intermech.Imbase.Resources.FindByName.ico");
      return this._ico;
    }
  }

  public virtual TreeView CatalogTree
  {
    [DebuggerStepThrough] get => this._treeView;
  }

  public virtual ListView ResultList
  {
    [DebuggerStepThrough] get => this.lbResult;
  }

  public FindByNameView()
  {
    this.InitializeComponent();
    this.InitializeCustomData();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, this.HelpID);
  }

  internal void LoadData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        return;
      this.BuildTree(customService.GetAllSubfolders(session.SessionGUID, this._targetId, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS));
    }
  }

  private void BuildTree(DataTable dt)
  {
    try
    {
      this._loading = true;
      this._treeView.BeginUpdate();
      this._treeView.Nodes.Clear();
      if (dt != null)
      {
        if (dt.Rows.Count > 0)
        {
          DataView dataView = new DataView(dt);
          dataView.Sort = "F_PATH ASC";
          int count = dataView.Count;
          Hashtable hashtable = new Hashtable(count);
          int columnIndex1 = dt.Columns.IndexOf("F_OBJECT_ID");
          int columnIndex2 = dt.Columns.IndexOf("CAPTION");
          int columnIndex3 = dt.Columns.IndexOf("F_OBJECT_TYPE");
          int columnIndex4 = dt.Columns.IndexOf("F_PATH");
          int columnIndex5 = dt.Columns.IndexOf(sc_7955.ssp_imbase_7956());
          for (int recordIndex = 0; recordIndex < count; ++recordIndex)
          {
            DataRow row = dataView[recordIndex].Row;
            NodeInfo nodeInfo = new NodeInfo(Convert.ToInt64(row[columnIndex1]), Convert.ToInt32(row[columnIndex3]));
            TreeNode node = new TreeNode(Convert.ToString(row[columnIndex2]), 0, 1);
            if (!DBNull.Value.Equals(row[columnIndex5]))
              nodeInfo.Order = Convert.ToInt32(row[columnIndex5]);
            node.Tag = (object) nodeInfo;
            node.SelectedImageIndex = node.ImageIndex = TreeBuilder.GetIconIndex(Convert.ToInt32(row[columnIndex3]));
            string key1 = Convert.ToString(row[columnIndex4]);
            if (!hashtable.ContainsKey((object) key1))
            {
              int length = key1.Length - 2;
              string key2 = key1.Substring(0, length);
              if (hashtable[(object) key2] is TreeNode treeNode)
                treeNode.Nodes.Add(node);
              else
                this._treeView.Nodes.Add(node);
              hashtable.Add((object) key1, (object) node);
            }
          }
          this._treeView.Sort();
          foreach (TreeNode node in this._treeView.Nodes)
            node.Collapse(false);
        }
      }
    }
    finally
    {
      this._treeView.EndUpdate();
      this._loading = false;
    }
    TreeNodeCollection nodes = this._treeView.Nodes;
    if (nodes.Count != 1)
      return;
    IntPtr handle = this._treeView.Handle;
    nodes[0].Expand();
  }

  protected virtual void InitializeCustomData()
  {
    this.cbFindData.AutoCompleteCustomSource = FindByNameView._autoCompleteSource;
    int count = FindByNameView._autoCompleteSource.Count;
    for (int index = 0; index < count; ++index)
      this.cbFindData.Items.Add((object) FindByNameView._autoCompleteSource[index]);
    this.cbFindMode.SelectedIndex = 1;
    this._treeView.ImageList = TreeBuilder.ImageList;
    this.lbResult.SmallImageList = TreeBuilder.ImageList;
  }

  protected virtual void AddResultNode(TreeNode node)
  {
    ListViewItem listItem = new ListViewItem(node.Text);
    this.FillResultItem(listItem, node);
    this.lbResult.Items.Add(listItem);
  }

  protected virtual void FillResultItem(ListViewItem listItem, TreeNode node)
  {
    listItem.Tag = (object) node;
    listItem.ImageIndex = node.ImageIndex;
    listItem.SubItems.Add(node.FullPath);
  }

  private void View_BeforeFirstShown(object sender, EventArgs e) => this.LoadData();

  private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1)
      return;
    ListBox listBox = sender as ListBox;
    TreeNode treeNode = listBox.Items[e.Index] as TreeNode;
    Rectangle rectangle = new Rectangle(e.Bounds.Left + 4, e.Bounds.Top, 16 /*0x10*/, 16 /*0x10*/);
    this._treeView.ImageList.Draw(e.Graphics, rectangle.X, rectangle.Y, treeNode.ImageIndex);
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = SystemBrushes.HighlightText;
    e.Graphics.DrawString(treeNode.Text, listBox.Font, brush, (float) (rectangle.Left + 20), (float) (rectangle.Top + 2));
  }

  private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    ListViewItem focusedItem = this.lbResult.FocusedItem;
    if (focusedItem == null || !(focusedItem.Tag is TreeNode tag))
      return;
    this._treeView.SelectedNode = tag;
    tag.EnsureVisible();
  }

  private void ListBox_DoubleClick(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (selectedNode == null)
      return;
    NodeInfo tag = selectedNode.Tag as NodeInfo;
    if (this._locateHandler != null)
    {
      long objectId = tag.ObjectId;
      this._locateHandler((object) this, new LocateNodeEventArgs(objectId, FindHelper.GetDataTable(objectId)));
    }
    else if (this._parentINode != null && FindHelper.IsValidNode(this._parentINode))
    {
      this.LocateNavigatorNode(this._parentINode, tag.ObjectId);
    }
    else
    {
      if (this._parentTNode == null)
        return;
      this.LocateNavigatorNode(this._parentTNode, tag.ObjectId);
    }
  }

  private void LocateNavigatorNode(NavigatorTreeNode parentNode, long objectId)
  {
    NavigatorTreeNode navigatorTreeNode = FindHelper.SearchNodeByNodeID(parentNode, objectId);
    if (navigatorTreeNode == null || navigatorTreeNode.Equals((object) parentNode))
      return;
    navigatorTreeNode.Focus();
  }

  private void LocateNavigatorNode(TreeNode parentNode, long objectId)
  {
    TreeNode treeNode = FindHelper.SearchNodeByNodeID(parentNode, objectId);
    if (treeNode == null || treeNode.Equals((object) parentNode))
      return;
    treeNode.EnsureVisible();
    treeNode.TreeView.SelectedNode = treeNode;
  }

  private void FindData_TextChanged(object sender, EventArgs e)
  {
    this.btFind.Enabled = !string.IsNullOrEmpty(this.cbFindData.Text);
  }

  private void BtFind_Click(object sender, EventArgs e)
  {
    string pattern = this.cbFindData.Text;
    if (string.IsNullOrEmpty(pattern))
      return;
    try
    {
      this._loading = true;
      if (!FindByNameView._autoCompleteSource.Contains(pattern))
        FindByNameView._autoCompleteSource.Add(pattern);
      if (!this.cbFindData.Items.Contains((object) pattern))
        this.cbFindData.Items.Add((object) pattern);
      switch (this.cbFindMode.SelectedIndex)
      {
        case 0:
          pattern += "*";
          goto case 3;
        case 1:
          pattern = $"*{pattern}*";
          goto case 3;
        case 2:
          pattern = "*" + pattern;
          goto case 3;
        case 3:
          this.ScanNodes(this.cbRegister.Checked ? new Wildcard(pattern) : new Wildcard(pattern, RegexOptions.IgnoreCase));
          break;
      }
    }
    finally
    {
      this._loading = false;
    }
  }

  private void ScanNodes(Wildcard wc)
  {
    try
    {
      this.lbResult.BeginUpdate();
      this.lbResult.Items.Clear();
      TreeNodeCollection nodes = this._treeView.Nodes;
      this.ScanNodes(wc, nodes);
    }
    finally
    {
      this.lbResult.Sort();
      this.lbResult.Columns[0].Width = -1;
      this.lbResult.Columns[1].Width = -1;
      this.lbResult.EndUpdate();
    }
  }

  private void ScanNodes(Wildcard wc, TreeNodeCollection nodes)
  {
    if (nodes == null)
      return;
    int count = nodes.Count;
    for (int index = 0; index < count; ++index)
    {
      TreeNode node = nodes[index];
      wc.Matches(node.Text);
      if (wc.IsMatch(node.Text))
        this.AddResultNode(node);
      this.ScanNodes(wc, node.Nodes);
    }
  }

  private void ClearHistory_Click(object sender, EventArgs e)
  {
    FindByNameView._autoCompleteSource.Clear();
    this.cbFindData.Items.Clear();
  }

  public static void Show(object parentNode, bool modal, LocateNodeEventHandler locateHandler)
  {
    FindByNameView view = new FindByNameView();
    view.SetData(parentNode, locateHandler);
    if (modal)
    {
      ImbaseViewForm.FindOrCreateViewForm(ImbaseViewForm.FormType.FindByName, (IImbaseView) view, view.Icon).Show();
    }
    else
    {
      if (!(ServicesManager.GetService(typeof (DockManager)) is DockManager service))
        return;
      view.Manager = service;
      view.Float();
      if (view.Parent == null || !(view.Parent.Parent is Form))
        return;
      view.Parent.Parent.MinimumSize = new Size(view.MinimumSize.Width + 20, view.MinimumSize.Height + 40);
    }
  }

  public void SetData(object parentNode, LocateNodeEventHandler locateHandler)
  {
    this._locateHandler = locateHandler;
    switch (parentNode)
    {
      case NavigatorTreeNode navigatorTreeNode:
        this._targetId = (navigatorTreeNode.NodeID as NodeID).ObjectID;
        this._parentINode = navigatorTreeNode;
        break;
      case TreeNode treeNode:
        if (!(treeNode.Tag is NodeInfo tag))
          break;
        this._targetId = tag.ObjectId;
        this._parentTNode = treeNode;
        break;
    }
  }

  public void FirstShown(object sender, EventArgs e) => this.View_BeforeFirstShown(sender, e);

  public void ViewClosing(object sender, CancelEventArgs e)
  {
  }

  private void cbFindData_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r' || !this.btFind.Enabled)
      return;
    this.btFind.PerformClick();
  }

  private void button2_Click(object sender, EventArgs e) => this.Close();

  private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    string str = string.Empty;
    if (selectedNode != null)
      str = selectedNode.FullPath;
    this.textBox1.Text = str;
  }

  public override string HelpID => "1753";

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._ico?.Dispose();
      this._ico = (Icon) null;
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FindByNameView));
    this.splitContainer1 = new SplitContainer();
    this.btnFindInMainWin = new Button();
    this._treeView = new TreeView();
    this.panel1 = new Panel();
    this.cbRegister = new CheckBox();
    this.label3 = new Label();
    this.label2 = new Label();
    this.cbFindData = new ComboBox();
    this.cbFindMode = new ComboBox();
    this.button1 = new Button();
    this.btFind = new Button();
    this.label1 = new Label();
    this.lbResult = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.toolTip1 = new ToolTip(this.components);
    this.textBox1 = new TextBox();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.btnFindInMainWin);
    this.splitContainer1.Panel1.Controls.Add((Control) this._treeView);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel1, "splitContainer1.Panel1");
    this.splitContainer1.Panel2.Controls.Add((Control) this.panel1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.label1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lbResult);
    componentResourceManager.ApplyResources((object) this.btnFindInMainWin, "btnFindInMainWin");
    this.btnFindInMainWin.Name = "btnFindInMainWin";
    this.btnFindInMainWin.UseVisualStyleBackColor = true;
    this.btnFindInMainWin.Click += new EventHandler(this.ListBox_DoubleClick);
    componentResourceManager.ApplyResources((object) this._treeView, "_treeView");
    this._treeView.HideSelection = false;
    this._treeView.ItemHeight = 18;
    this._treeView.Name = "_treeView";
    this._treeView.AfterSelect += new TreeViewEventHandler(this.TreeView_AfterSelect);
    this._treeView.DoubleClick += new EventHandler(this.ListBox_DoubleClick);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.cbRegister);
    this.panel1.Controls.Add((Control) this.label3);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.cbFindData);
    this.panel1.Controls.Add((Control) this.cbFindMode);
    this.panel1.Controls.Add((Control) this.button1);
    this.panel1.Controls.Add((Control) this.btFind);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.cbRegister, "cbRegister");
    this.cbRegister.Name = "cbRegister";
    this.toolTip1.SetToolTip((Control) this.cbRegister, componentResourceManager.GetString("cbRegister.ToolTip"));
    this.cbRegister.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.cbFindData, "cbFindData");
    this.cbFindData.AutoCompleteMode = AutoCompleteMode.Suggest;
    this.cbFindData.AutoCompleteSource = AutoCompleteSource.CustomSource;
    this.cbFindData.FormattingEnabled = true;
    this.cbFindData.Name = "cbFindData";
    this.cbFindData.TextChanged += new EventHandler(this.FindData_TextChanged);
    this.cbFindData.KeyPress += new KeyPressEventHandler(this.cbFindData_KeyPress);
    componentResourceManager.ApplyResources((object) this.cbFindMode, "cbFindMode");
    this.cbFindMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbFindMode.FormattingEnabled = true;
    this.cbFindMode.Items.AddRange(new object[4]
    {
      (object) componentResourceManager.GetString("cbFindMode.Items"),
      (object) componentResourceManager.GetString("cbFindMode.Items1"),
      (object) componentResourceManager.GetString("cbFindMode.Items2"),
      (object) componentResourceManager.GetString("cbFindMode.Items3")
    });
    this.cbFindMode.Name = "cbFindMode";
    this.toolTip1.SetToolTip((Control) this.cbFindMode, componentResourceManager.GetString("cbFindMode.ToolTip"));
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.toolTip1.SetToolTip((Control) this.button1, componentResourceManager.GetString("button1.ToolTip"));
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.ClearHistory_Click);
    componentResourceManager.ApplyResources((object) this.btFind, "btFind");
    this.btFind.Name = "btFind";
    this.btFind.UseVisualStyleBackColor = true;
    this.btFind.Click += new EventHandler(this.BtFind_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.lbResult, "lbResult");
    this.lbResult.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this.lbResult.FullRowSelect = true;
    this.lbResult.Name = "lbResult";
    this.lbResult.UseCompatibleStateImageBehavior = false;
    this.lbResult.View = View.Details;
    this.lbResult.SelectedIndexChanged += new EventHandler(this.ListBox_SelectedIndexChanged);
    this.lbResult.DoubleClick += new EventHandler(this.ListBox_DoubleClick);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.Name = "textBox1";
    this.textBox1.ReadOnly = true;
    this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this.splitContainer1);
    this.DoubleBuffered = true;
    this.FloatingSize = new Size(766, 496);
    this.MinimumSize = new Size(548, 250);
    this.Name = nameof (FindByNameView);
    this.PersistState = false;
    this.ShowHint = DockState.Float;
    this.BeforeFirstShown += new EventHandler(this.View_BeforeFirstShown);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
