// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.PropertyPages.DocPropertyPagesForm
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Bars;
using Intermech.Interfaces.Client;
using Intermech.NavBars;
using Intermech.UI;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Editor.PropertyPages;

internal class DocPropertyPagesForm : Form
{
  private DocPropertyPagesService _propertyPagesService;
  private Control _pageControl;
  private Button btOK;
  private Button btApply;
  private Button btCancel;
  private IContainer components;
  private int _folderOpenedIndex;
  private int _folderIndex;
  private int _pageIndex;
  private SplitContainer splitContainer1;
  private TreeView treeView;
  private Bevel bevel;
  private int _selectedIndex;
  private Panel panelControls;
  private PropertyGrid propertyGrid;
  private HeaderControl headerControl;
  private MenuBar menuBar;
  private ContextMenuBarItem contextMenuBarItem;
  private MenuButtonItem mnpAddCriterion;
  private MenuButtonItem mnpDeleteCriterion;
  private MenuButtonItem mnpAddValue;
  private MenuButtonItem mnpDelValue;
  private MenuButtonItem mnpMoveUp;
  private MenuButtonItem mnpMoveDown;
  private HybridDictionary FControlsSettings = new HybridDictionary(0, true);

  public DocPropertyPagesForm(DocPropertyPagesService pagesService)
  {
    this.InitializeComponent();
    this._propertyPagesService = pagesService;
    this._propertyPagesService.Changed += new EventHandler(this._propertyPagesService_Changed);
  }

  private TreeNode FindNode(string name, TreeNode parent)
  {
    if (parent == null)
    {
      foreach (TreeNode node in this.treeView.Nodes)
      {
        if (node.Parent == null && node.Text == name)
          return node;
      }
    }
    else
    {
      foreach (TreeNode node in parent.Nodes)
      {
        if (node.Text == name)
          return node;
      }
    }
    TreeNode node1 = new TreeNode(name);
    if (parent != null)
      parent.Nodes.Add(node1);
    else
      this.treeView.Nodes.Add(node1);
    return node1;
  }

  private TreeNode AddNode(string path)
  {
    TreeNode parent1 = (TreeNode) null;
    string str = path;
    char[] chArray = new char[1]{ '\\' };
    foreach (string name in str.Split(chArray))
      parent1 = this.FindNode(name, parent1);
    if (parent1 != null)
    {
      for (TreeNode parent2 = parent1.Parent; parent2 != null; parent2 = parent2.Parent)
      {
        parent2.ImageIndex = this._folderIndex;
        parent2.SelectedImageIndex = this._folderOpenedIndex;
      }
    }
    return parent1;
  }

  internal void AddPage(string path, IPropertyPage page)
  {
    TreeNode treeNode = this.AddNode(path);
    if (treeNode == null)
      return;
    treeNode.Tag = (object) page;
    treeNode.ImageIndex = this._pageIndex;
    treeNode.SelectedImageIndex = this._selectedIndex;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocPropertyPagesForm));
    this.splitContainer1 = new SplitContainer();
    this.treeView = new TreeView();
    this.panelControls = new Panel();
    this.propertyGrid = new PropertyGrid();
    this.bevel = new Bevel();
    this.btOK = new Button();
    this.btApply = new Button();
    this.btCancel = new Button();
    this.headerControl = new HeaderControl();
    this.menuBar = new MenuBar();
    this.contextMenuBarItem = new ContextMenuBarItem();
    this.mnpAddCriterion = new MenuButtonItem();
    this.mnpDeleteCriterion = new MenuButtonItem();
    this.mnpAddValue = new MenuButtonItem();
    this.mnpDelValue = new MenuButtonItem();
    this.mnpMoveUp = new MenuButtonItem();
    this.mnpMoveDown = new MenuButtonItem();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.panelControls.SuspendLayout();
    this.headerControl.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.treeView);
    this.splitContainer1.Panel2.Controls.Add((Control) this.panelControls);
    this.splitContainer1.Panel2.Controls.Add((Control) this.headerControl);
    this.splitContainer1.Panel2.Controls.Add((Control) this.bevel);
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.FullRowSelect = true;
    this.treeView.HideSelection = false;
    this.treeView.Name = "treeView";
    this.treeView.Sorted = true;
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    this.panelControls.Controls.Add((Control) this.propertyGrid);
    componentResourceManager.ApplyResources((object) this.panelControls, "panelControls");
    this.panelControls.Name = "panelControls";
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.LineColor = SystemColors.Control;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.PropertySort = PropertySort.Alphabetical;
    this.propertyGrid.ToolbarVisible = false;
    componentResourceManager.ApplyResources((object) this.bevel, "bevel");
    this.bevel.Name = "bevel";
    componentResourceManager.ApplyResources((object) this.btOK, "btOK");
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Name = "btOK";
    this.btOK.Click += new EventHandler(this.btOK_Click);
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    this.btApply.Name = "btApply";
    this.btApply.Click += new EventHandler(this.btApply_Click);
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.Click += new EventHandler(this.btCancel_Click);
    this.headerControl.BackColor = SystemColors.Control;
    this.headerControl.Controls.Add((Control) this.menuBar);
    componentResourceManager.ApplyResources((object) this.headerControl, "headerControl");
    this.headerControl.ForeColor = SystemColors.ControlText;
    this.headerControl.HeaderFont = new Font("Tahoma", 12f, FontStyle.Bold);
    this.headerControl.Name = "headerControl";
    componentResourceManager.ApplyResources((object) this.menuBar, "menuBar");
    this.menuBar.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuBar.Hidden = false;
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem
    });
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.contextMenuBarItem, "contextMenuBarItem");
    this.contextMenuBarItem.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpAddCriterion,
      (ToolbarItemBase) this.mnpDeleteCriterion,
      (ToolbarItemBase) this.mnpAddValue,
      (ToolbarItemBase) this.mnpDelValue,
      (ToolbarItemBase) this.mnpMoveUp,
      (ToolbarItemBase) this.mnpMoveDown
    });
    this.contextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpAddCriterion, "mnpAddCriterion");
    this.mnpAddCriterion.ImageIndex = 0;
    this.mnpAddCriterion.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpDeleteCriterion, "mnpDeleteCriterion");
    this.mnpDeleteCriterion.ImageIndex = 1;
    this.mnpDeleteCriterion.ShowText = true;
    this.mnpAddValue.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpAddValue, "mnpAddValue");
    this.mnpAddValue.ImageIndex = 2;
    this.mnpAddValue.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpDelValue, "mnpDelValue");
    this.mnpDelValue.ImageIndex = 3;
    this.mnpDelValue.ShowText = true;
    this.mnpMoveUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveUp, "mnpMoveUp");
    this.mnpMoveUp.ImageIndex = 4;
    this.mnpMoveUp.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveDown, "mnpMoveDown");
    this.mnpMoveDown.ImageIndex = 5;
    this.mnpMoveDown.ShowText = true;
    this.AcceptButton = (IButtonControl) this.btOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btApply);
    this.Controls.Add((Control) this.btOK);
    this.MinimizeBox = false;
    this.Name = nameof (DocPropertyPagesForm);
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Tag = (object) " ";
    this.Load += new EventHandler(this.PropertyPagesForm_Load);
    this.FormClosed += new FormClosedEventHandler(this.PropertyPagesForm_FormClosed);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.ResumeLayout(false);
    this.panelControls.ResumeLayout(false);
    this.headerControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this._propertyPagesService.OnChanged();
  }

  private void _propertyPagesService_Changed(object sender, EventArgs e)
  {
    this.btApply.Enabled = true;
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    for (TreeNode node = e.Node; node != null; node = node.Nodes[0])
    {
      object tag = node.Tag;
      node.EnsureVisible();
      if (tag != null)
      {
        node.TreeView.SelectedNode = node;
        IPropertyPage propertyPage = tag as IPropertyPage;
        this.headerControl.Visible = !string.IsNullOrEmpty(propertyPage?.HeaderText);
        if (this.headerControl.Visible && propertyPage != null)
          this.headerControl.Text = propertyPage.HeaderText;
        if (propertyPage == null)
          break;
        object control = propertyPage.Control;
        if (control == null)
          break;
        if (this._pageControl != null)
        {
          this._pageControl.Visible = false;
          this._pageControl.Parent = (Control) null;
        }
        if (control is Control)
        {
          this._pageControl = control as Control;
          this.propertyGrid.SelectedObject = (object) null;
          this.propertyGrid.Visible = false;
          this._pageControl.Parent = (Control) this.panelControls;
          this._pageControl.Dock = DockStyle.Fill;
          this._pageControl.Visible = true;
          this.panelControls.BorderStyle = BorderStyle.Fixed3D;
          break;
        }
        this.panelControls.BorderStyle = BorderStyle.None;
        this._pageControl = (Control) null;
        this.propertyGrid.SelectedObject = control;
        this.propertyGrid.Visible = true;
        break;
      }
      if (node.Nodes.Count <= 0)
        break;
    }
  }

  private void btOK_Click(object sender, EventArgs e)
  {
    this._propertyPagesService.Apply();
    this.btApply.Enabled = false;
  }

  private void btApply_Click(object sender, EventArgs e)
  {
    this._propertyPagesService.Apply();
    this.btApply.Enabled = false;
    if (!this.propertyGrid.Visible)
      return;
    this.propertyGrid.SelectedObject = this.propertyGrid.SelectedObject;
  }

  private void btCancel_Click(object sender, EventArgs e)
  {
    this._propertyPagesService.Cancel();
    this.btApply.Enabled = false;
  }

  private void PropertyPagesForm_Load(object sender, EventArgs e)
  {
    if (this.FControlsSettings == null)
      this.FControlsSettings = new HybridDictionary(0, true);
    this.SetControlsState(this.FControlsSettings);
  }

  private void PropertyPagesForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.GetControlsState(this.FControlsSettings);
  }

  private object GetDicValue(HybridDictionary collection, object key, object defaultValue)
  {
    return collection == null || key == null ? defaultValue : collection[key] ?? defaultValue;
  }

  private void GetControlsState(HybridDictionary controlsState)
  {
    if (controlsState == null)
      return;
    controlsState[(object) "Splitter"] = (object) this.splitContainer1.SplitterDistance;
  }

  private void SetControlsState(HybridDictionary controlsState)
  {
    if (controlsState == null)
      return;
    this.splitContainer1.SplitterDistance = (int) this.GetDicValue(controlsState, (object) "Splitter", (object) (int) byte.MaxValue);
  }
}
