
// Type: Intermech.Search.Configuration.OptionsDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.Configuration;

public class OptionsDialog : Form
{
  private IPropertyPagesService _propertyPagesService;
  private int _folderOpenedIndex;
  private int _folderIndex;
  private int _pageIndex;
  private int _selectedIndex;
  private Button _okButton;
  private Button _cancelButton;
  private SplitContainer _splitContainer;
  private TreeView _tree;
  private Panel _panel;
  private PropertyGrid _propertyGrid;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public OptionsDialog()
  {
    this.InitializeComponent();
    this._propertyPagesService = ServiceLocator.Get<IPropertyPagesService>();
    this._propertyPagesService.Changed += new EventHandler(this.PropertyPagesService_Changed);
    INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
    this._tree.ImageList = namedImageList.ImageList;
    this._folderIndex = namedImageList.ImageIndex("imgFolder");
    this._folderOpenedIndex = namedImageList.ImageIndex("imgFolderOpened");
    this._pageIndex = namedImageList.ImageIndex("imgPropPage");
  }

  private void OptionsDialog_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    this.ShowHelp();
  }

  private void PropertyPagesForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    e.Cancel = true;
    this.ShowHelp();
  }

  private void PropertyPagesService_Changed(object sender, EventArgs e)
  {
    this._okButton.Enabled = true;
  }

  private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeNode node = e.Node;
    this._panel.Controls.Clear();
    if (node.Tag == null)
    {
      this._panel.Visible = false;
      this._propertyGrid.Visible = true;
      this._propertyGrid.SelectedObject = (object) null;
    }
    else
    {
      if (!(node.Tag is IPropertyPage))
        return;
      IPropertyPage tag = (IPropertyPage) node.Tag;
      if (tag == null)
        return;
      if (tag is Control)
      {
        this._propertyGrid.SelectedObject = (object) null;
        Control control = tag as Control;
        control.Parent = (Control) this._panel;
        control.Dock = DockStyle.Fill;
        control.Visible = true;
        this._propertyGrid.Visible = false;
        this._panel.Visible = true;
      }
      else
      {
        this._propertyGrid.Dock = DockStyle.Fill;
        this._propertyGrid.SelectedObject = tag.Control;
        this._panel.Visible = false;
        this._propertyGrid.Visible = true;
      }
    }
  }

  private void ShowHelp()
  {
    if (!(this._tree.SelectedNode.Tag is IPropertyPage tag))
      return;
    HelpProvidersClass.ShowHelpTopic(tag.HelpTopicID);
  }

  private TreeNode FindNode(string name, TreeNode parent)
  {
    if (parent == null)
    {
      foreach (TreeNode node in this._tree.Nodes)
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
      this._tree.Nodes.Add(node1);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OptionsDialog));
    this._splitContainer = new SplitContainer();
    this._tree = new TreeView();
    this._propertyGrid = new PropertyGrid();
    this._panel = new Panel();
    this._okButton = new Button();
    this._cancelButton = new Button();
    this._splitContainer.BeginInit();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._splitContainer, "_splitContainer");
    this._splitContainer.Name = "_splitContainer";
    this._splitContainer.Panel1.Controls.Add((Control) this._tree);
    this._splitContainer.Panel2.Controls.Add((Control) this._propertyGrid);
    this._splitContainer.Panel2.Controls.Add((Control) this._panel);
    componentResourceManager.ApplyResources((object) this._tree, "_tree");
    this._tree.FullRowSelect = true;
    this._tree.HideSelection = false;
    this._tree.Name = "_tree";
    this._tree.Sorted = true;
    this._tree.AfterSelect += new TreeViewEventHandler(this.Tree_AfterSelect);
    componentResourceManager.ApplyResources((object) this._propertyGrid, "_propertyGrid");
    this._propertyGrid.LineColor = SystemColors.Control;
    this._propertyGrid.Name = "_propertyGrid";
    this._propertyGrid.PropertySort = PropertySort.Alphabetical;
    this._panel.BackColor = SystemColors.Window;
    this._panel.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this._panel, "_panel");
    this._panel.Name = "_panel";
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Name = "_okButton";
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Name = "_cancelButton";
    this.AcceptButton = (IButtonControl) this._okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.Controls.Add((Control) this._splitContainer);
    this.Controls.Add((Control) this._cancelButton);
    this.Controls.Add((Control) this._okButton);
    this.DoubleBuffered = true;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (OptionsDialog);
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Tag = (object) " ";
    this.HelpButtonClicked += new CancelEventHandler(this.PropertyPagesForm_HelpButtonClicked);
    this.HelpRequested += new HelpEventHandler(this.OptionsDialog_HelpRequested);
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.EndInit();
    this._splitContainer.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
