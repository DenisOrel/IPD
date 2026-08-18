
// Type: Intermech.Search.TreeViewWithButtonsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search;

public class TreeViewWithButtonsForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeView _treeView;
  private Button _cancelButton;
  private Button _okButton;

  public TreeViewWithButtonsForm() => this.InitializeComponent();

  public TreeNodeCollection Nodes => this._treeView.Nodes;

  public ImageList ImageList
  {
    get => this._treeView.ImageList;
    set => this._treeView.ImageList = value;
  }

  public TreeView TreeView => this._treeView;

  public Button OKButton => this._okButton;

  public List<TreeNode> CheckedNodes
  {
    get
    {
      List<TreeNode> checkedNodes = new List<TreeNode>();
      this.GetCheckedNodes(this._treeView.Nodes, ref checkedNodes);
      return checkedNodes;
    }
  }

  public List<object> CheckedTags
  {
    get
    {
      return this.CheckedNodes.Select<TreeNode, object>((Func<TreeNode, object>) (o => o.Tag)).ToList<object>();
    }
    set => this.CheckNodes(this._treeView.Nodes, value ?? new List<object>(0));
  }

  public bool ShowCheckBoxes
  {
    get => this._treeView.CheckBoxes;
    set => this._treeView.CheckBoxes = value;
  }

  public object SelectedTag
  {
    get => this._treeView.SelectedNode == null ? (object) null : this._treeView.SelectedNode.Tag;
    set
    {
      this._treeView.SelectedNode = (TreeNode) null;
      this.SelectNode(this._treeView.Nodes, value);
    }
  }

  public bool DisableGroupCheckedNodes { get; set; }

  public bool DisableCheckParentNodes { get; set; }

  public void ShowCheckedNodes()
  {
    List<TreeNode[]> treeNodeArrayList = new List<TreeNode[]>();
    foreach (TreeNode checkedNode in this.CheckedNodes)
    {
      List<TreeNode> source = new List<TreeNode>();
      TreeNode treeNode = checkedNode;
      while (treeNode != null)
      {
        treeNode = treeNode.Parent;
        if (treeNode != null)
          source.Add(treeNode);
      }
      treeNodeArrayList.Add(source.Reverse<TreeNode>().ToArray<TreeNode>());
    }
    foreach (TreeNode[] treeNodeArray in treeNodeArrayList)
    {
      foreach (TreeNode treeNode in treeNodeArray)
      {
        if (!treeNode.Checked)
          treeNode.Expand();
        else
          break;
      }
    }
  }

  public void ShowSelectedNode()
  {
  }

  private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
  {
    if (this.DisableGroupCheckedNodes)
      return;
    this._treeView.AfterCheck -= new TreeViewEventHandler(this.TreeView_AfterCheck);
    try
    {
      this.CheckChildrenNodes(e.Node.Nodes, e.Node.Checked);
      if (this.DisableCheckParentNodes)
        return;
      this.CheckParentNodes(e.Node, e.Node.Checked);
    }
    finally
    {
      this._treeView.AfterCheck += new TreeViewEventHandler(this.TreeView_AfterCheck);
    }
  }

  private void GetCheckedNodes(TreeNodeCollection nodes, ref List<TreeNode> checkedNodes)
  {
    foreach (TreeNode node in nodes)
    {
      if (node.Checked)
      {
        checkedNodes.Add(node);
        this.GetCheckedNodes(node.Nodes, ref checkedNodes);
      }
      else
        this.GetCheckedNodes(node.Nodes, ref checkedNodes);
    }
  }

  private void CheckNodes(TreeNodeCollection nodes, List<object> checkedTags)
  {
    foreach (TreeNode node in nodes)
    {
      if (checkedTags.Contains(node.Tag))
      {
        node.Checked = true;
        if (!this.DisableGroupCheckedNodes)
          this.CheckChildrenNodes(node.Nodes, true);
        else
          this.CheckNodes(node.Nodes, checkedTags);
      }
      else
        this.CheckNodes(node.Nodes, checkedTags);
    }
  }

  private void CheckChildrenNodes(TreeNodeCollection nodes, bool isChecked)
  {
    foreach (TreeNode node in nodes)
    {
      node.Checked = isChecked;
      this.CheckChildrenNodes(node.Nodes, isChecked);
    }
  }

  private void CheckParentNodes(TreeNode node, bool isChecked)
  {
    if (node.Parent == null)
      return;
    if (!isChecked)
    {
      node.Parent.Checked = false;
      this.CheckParentNodes(node.Parent, false);
    }
    else
    {
      if (node.Parent.Nodes.Cast<TreeNode>().Where<TreeNode>((Func<TreeNode, bool>) (o => !o.Checked)).Count<TreeNode>() != 0)
        return;
      node.Parent.Checked = true;
      this.CheckParentNodes(node.Parent, true);
    }
  }

  private void SelectNode(TreeNodeCollection nodes, object selectedTag)
  {
    foreach (TreeNode node in nodes)
    {
      if (object.Equals(node.Tag, selectedTag))
        this._treeView.SelectedNode = node;
      this.SelectNode(node.Nodes, selectedTag);
    }
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
    this._treeView = new TreeView();
    this._cancelButton = new Button();
    this._okButton = new Button();
    this.SuspendLayout();
    this._treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._treeView.CheckBoxes = true;
    this._treeView.FullRowSelect = true;
    this._treeView.Location = new Point(12, 12);
    this._treeView.Name = "_treeView";
    this._treeView.Size = new Size(460, 359);
    this._treeView.TabIndex = 0;
    this._treeView.AfterCheck += new TreeViewEventHandler(this.TreeView_AfterCheck);
    this._cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Location = new Point(397, 377);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 1;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Location = new Point(316, 377);
    this._okButton.Name = "_okButton";
    this._okButton.Size = new Size(75, 23);
    this._okButton.TabIndex = 1;
    this._okButton.Text = "ОК";
    this._okButton.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._okButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(484, 412);
    this.Controls.Add((Control) this._okButton);
    this.Controls.Add((Control) this._cancelButton);
    this.Controls.Add((Control) this._treeView);
    this.Name = "TreeSelectDialog";
    this.ShowIcon = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Выберите объекты";
    this.ResumeLayout(false);
  }
}
