// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ConfigEditorMoveMenu
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class ConfigEditorMoveMenu
{
  private readonly TreeView _treeView;
  private ToolStripMenuItem _moveMenu;
  private ToolStripMenuItem _moveInStartMenu;
  private ToolStripMenuItem _moveUpMenu;
  private ToolStripMenuItem _moveDownMenu;
  private ToolStripMenuItem _moveInEndMenu;

  public ConfigEditorMoveMenu(TreeView treeView) => this._treeView = treeView;

  private void InitializeComponent()
  {
    this._moveMenu = new ToolStripMenuItem();
    this._moveInStartMenu = new ToolStripMenuItem();
    this._moveUpMenu = new ToolStripMenuItem();
    this._moveDownMenu = new ToolStripMenuItem();
    this._moveInEndMenu = new ToolStripMenuItem();
    this._moveMenu.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._moveInStartMenu,
      (ToolStripItem) this._moveUpMenu,
      (ToolStripItem) this._moveDownMenu,
      (ToolStripItem) this._moveInEndMenu
    });
    this._moveMenu.Name = "_moveMenu";
    this._moveMenu.Size = new Size(181, 22);
    this._moveMenu.Text = "Переместить";
    this._moveInStartMenu.Name = "_moveInStartMenu";
    this._moveInStartMenu.Size = new Size(200, 22);
    this._moveInStartMenu.Text = "В начало";
    this._moveInStartMenu.Click += new EventHandler(this.moveInStartMenu_Click);
    this._moveUpMenu.Name = "_moveUpMenu";
    this._moveUpMenu.Size = new Size(200, 22);
    this._moveUpMenu.Text = "На один уровень вверх";
    this._moveUpMenu.Click += new EventHandler(this.moveUpMenu_Click);
    this._moveDownMenu.Name = "_moveDownMenu";
    this._moveDownMenu.Size = new Size(200, 22);
    this._moveDownMenu.Text = "На один уровень вниз";
    this._moveDownMenu.Click += new EventHandler(this.moveDownMenu_Click);
    this._moveInEndMenu.Name = "_moveInEndMenu";
    this._moveInEndMenu.Size = new Size(200, 22);
    this._moveInEndMenu.Text = "В конец";
    this._moveInEndMenu.Click += new EventHandler(this.moveInEndMenu_Click);
  }

  private void ViewMenu()
  {
    foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) this._moveMenu.DropDownItems)
      dropDownItem.Enabled = false;
    TreeNode selectedNode = this._treeView.SelectedNode;
    TreeNode parent = selectedNode?.Parent;
    if (parent == null)
      return;
    object tag1 = selectedNode.Tag;
    if (!(parent.Tag is IList tag2) || !tag2.Contains(tag1))
      return;
    int num = tag2.IndexOf(tag1);
    if (num > 0)
    {
      this._moveInStartMenu.Enabled = true;
      this._moveUpMenu.Enabled = true;
    }
    if (num >= tag2.Count - 1)
      return;
    this._moveDownMenu.Enabled = true;
    this._moveInEndMenu.Enabled = true;
  }

  public ToolStripMenuItem MoveMenuItem
  {
    get
    {
      if (this._moveMenu == null)
        this.InitializeComponent();
      this.ViewMenu();
      return this._moveMenu;
    }
  }

  private void moveInStartMenu_Click(object sender, EventArgs e) => this.SetNewIndexNode(0);

  private void SetNewIndexNode(int newIndex)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    TreeNode parent = selectedNode?.Parent;
    if (parent == null)
      return;
    object tag1 = selectedNode.Tag;
    if (!(parent.Tag is IList tag2) || !tag2.Contains(tag1))
      return;
    int num = tag2.IndexOf(tag1);
    int count = tag2.Count;
    tag2.Remove(tag1);
    parent.Nodes.Remove(selectedNode);
    int index;
    switch (newIndex)
    {
      case -1:
        index = num - 1;
        break;
      case 0:
        index = 0;
        break;
      case 1:
        index = num + 1;
        break;
      default:
        index = count - 1;
        break;
    }
    tag2.Insert(index, tag1);
    parent.Nodes.Insert(index, selectedNode);
    this._treeView.SelectedNode = selectedNode;
  }

  private void moveUpMenu_Click(object sender, EventArgs e) => this.SetNewIndexNode(-1);

  private void moveDownMenu_Click(object sender, EventArgs e) => this.SetNewIndexNode(1);

  private void moveInEndMenu_Click(object sender, EventArgs e)
  {
    this.SetNewIndexNode(int.MaxValue);
  }
}
