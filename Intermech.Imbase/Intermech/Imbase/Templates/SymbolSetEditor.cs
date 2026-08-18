// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.SymbolSetEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Templates;

public class SymbolSetEditor : Form
{
  private const int GROUP_INDEX = 3;
  private const int ITEM_INDEX = 4;
  private const string ITEM_FORMAT = "[{0}] {1}";
  private List<char> _availChars;
  private bool _updating;
  private IContainer components;
  private Button _btOK;
  private Button _btCancel;
  private TreeView _tree;
  private Button _btNewGroup;
  private Button _btNewItem;
  private Button _btDelete;
  private Button _btRefresh;
  private TextBox _edItemName;
  private Label _lbItemName;
  private Label _lbSymbol;
  private ComboBox _lbChars;
  private ImageList imageList1;
  private Button _btMoveUp;
  private Button _btMoveDown;
  private ToolTip toolTip1;

  public SymbolSetEditor()
  {
    this.InitializeComponent();
    this._availChars = new List<char>(sc_7937.ssp_imbase_7938(1757498501));
    this.InitializeChars();
    foreach (int availChar in this._availChars)
      this._lbChars.Items.Add((object) (char) availChar);
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1774);
  }

  public string Data
  {
    get
    {
      StringBuilder sb = new StringBuilder(1024 /*0x0400*/);
      TreeNodeCollection nodes = this._tree.Nodes;
      int count = nodes.Count;
      for (int index = 0; index < count; ++index)
      {
        TreeNode root = nodes[index];
        sb.AppendLine(root.Text);
        this.AppendSubnodes(root, sb);
      }
      return sb.ToString();
    }
    set
    {
      try
      {
        this._updating = true;
        this._tree.BeginUpdate();
        this._tree.Nodes.Clear();
        this.InitializeChars();
        string[] strArray = value.Replace(Environment.NewLine, "\n").Split('\n');
        int length = strArray.Length;
        TreeNode root = (TreeNode) null;
        for (int index = 0; index < length; ++index)
        {
          string groupName = strArray[index];
          if (groupName.Length != 0)
          {
            if (groupName[0] != '\t')
            {
              root?.Expand();
              root = this.AddNewGroup(groupName);
            }
            else
              this.CreateItemNode(groupName.Substring(5), groupName[2], root);
          }
        }
        root?.Expand();
      }
      finally
      {
        this._tree.EndUpdate();
        this._updating = false;
      }
      if (this._tree.Nodes.Count <= 0)
        return;
      this._tree.SelectedNode = this._tree.Nodes[0];
    }
  }

  private char NextChar
  {
    get
    {
      int nextChar = this._availChars.Count > 0 ? (int) this._availChars[0] : throw new Exception(LocalizationHolder.rm.GetString("Imbase_Templates_Exception_NotEnoughSymbols"));
      this._availChars.RemoveAt(0);
      return (char) nextChar;
    }
  }

  private void OnChars_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    char ch = (char) this._lbChars.Items[e.Index];
    if (this._availChars.Contains(ch))
    {
      e.Graphics.DrawString(ch.ToString(), e.Font, (e.State & DrawItemState.Selected) != DrawItemState.None ? SystemBrushes.ActiveCaptionText : SystemBrushes.ControlText, (RectangleF) e.Bounds);
    }
    else
    {
      using (Font font = new Font(e.Font, FontStyle.Strikeout))
        e.Graphics.DrawString(ch.ToString(), font, (e.State & DrawItemState.Selected) != DrawItemState.None ? SystemBrushes.ActiveCaptionText : SystemBrushes.ControlText, (RectangleF) e.Bounds);
    }
  }

  private void OnDelete_Click(object sender, EventArgs e)
  {
    this.DeleteNode(this._tree.SelectedNode);
  }

  private void OnItemName_TextChanged(object sender, EventArgs e)
  {
    if (this._updating || !(this._edItemName.Tag is TreeNode tag))
      return;
    string text = this._edItemName.Text;
    if (this._lbChars.Enabled)
      tag.Text = $"[{tag.Tag}] {text}";
    else
      tag.Text = text;
  }

  private void OnlbChars_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._updating)
      return;
    TreeNode selectedNode = this._tree.SelectedNode;
    if (selectedNode == null || selectedNode.Tag == null)
      return;
    char tag = (char) selectedNode.Tag;
    char selectedItem = (char) this._lbChars.SelectedItem;
    if ((int) tag == (int) selectedItem || !this._availChars.Contains(selectedItem))
      return;
    this.FreeChar(tag);
    selectedNode.Text = new StringBuilder(selectedNode.Text)
    {
      [1] = selectedItem
    }.ToString();
    selectedNode.Tag = (object) selectedItem;
    this.UseChar(selectedItem);
  }

  private void OnMoveDown_Click(object sender, EventArgs e)
  {
    try
    {
      this._updating = true;
      this._tree.BeginUpdate();
      TreeNode selectedNode = this._tree.SelectedNode;
      if (selectedNode == null)
        return;
      int index = selectedNode.Index;
      if (selectedNode.ImageIndex == 4)
      {
        int num = selectedNode.Parent.Nodes.Count - 1;
        if (index < num)
        {
          TreeNodeCollection nodes = selectedNode.Parent.Nodes;
          nodes.Remove(selectedNode);
          nodes.Insert(index + 1, selectedNode);
          this._tree.SelectedNode = selectedNode;
          selectedNode.EnsureVisible();
        }
        else
        {
          TreeNode nextNode = selectedNode.Parent.NextNode;
          if (nextNode == null)
            return;
          selectedNode.Parent.Nodes.Remove(selectedNode);
          nextNode.Nodes.Insert(0, selectedNode);
          this._tree.SelectedNode = selectedNode;
          selectedNode.EnsureVisible();
        }
      }
      else
      {
        if (selectedNode.ImageIndex != 3)
          return;
        int num = this._tree.Nodes.Count - 1;
        if (index >= num)
          return;
        this._tree.Nodes.Remove(selectedNode);
        this._tree.Nodes.Insert(index + 1, selectedNode);
        this._tree.SelectedNode = selectedNode;
        selectedNode.EnsureVisible();
      }
    }
    finally
    {
      this._tree.EndUpdate();
      this._updating = false;
    }
  }

  private void OnMoveUp_Click(object sender, EventArgs e)
  {
    try
    {
      this._updating = true;
      this._tree.BeginUpdate();
      TreeNode selectedNode = this._tree.SelectedNode;
      if (selectedNode == null)
        return;
      int index = selectedNode.Index;
      TreeNode treeNode = selectedNode.PrevNode;
      if (treeNode == null && selectedNode.ImageIndex == 4)
        treeNode = selectedNode.Parent;
      if (treeNode == null)
        return;
      if (selectedNode.ImageIndex == 4)
      {
        if (index > 0)
        {
          TreeNodeCollection nodes = selectedNode.Parent.Nodes;
          nodes.Remove(selectedNode);
          nodes.Insert(index - 1, selectedNode);
          this._tree.SelectedNode = selectedNode;
          selectedNode.EnsureVisible();
        }
        else
        {
          TreeNode prevNode = treeNode.PrevNode;
          if (prevNode == null)
            return;
          selectedNode.Parent.Nodes.Remove(selectedNode);
          prevNode.Nodes.Add(selectedNode);
          this._tree.SelectedNode = selectedNode;
          selectedNode.EnsureVisible();
        }
      }
      else
      {
        if (selectedNode.ImageIndex != 3 || index <= 0)
          return;
        this._tree.Nodes.Remove(selectedNode);
        this._tree.Nodes.Insert(index - 1, selectedNode);
        this._tree.SelectedNode = selectedNode;
      }
    }
    finally
    {
      this._tree.EndUpdate();
      this._updating = false;
    }
  }

  private void OnNewGroup_Click(object sender, EventArgs e)
  {
    this.AddNewGroup(this.CreateNewName(LocalizationHolder.rm.GetString(sc_7937.ssp_imbase_7939())));
    this.ActiveControl = (Control) this._edItemName;
  }

  private void OnNewItem_Click(object sender, EventArgs e)
  {
    this.AddNewElement(this.CreateNewName(LocalizationHolder.rm.GetString(sc_7937.ssp_imbase_7940())));
  }

  private void OnbtRefresh_Click(object sender, EventArgs e)
  {
    this.InitializeChars();
    int count = this._tree.Nodes.Count;
    for (int index = 0; index < count; ++index)
      this.UpdateNodeCodes(this._tree.Nodes[index]);
  }

  private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
  {
    bool updating = this._updating;
    this._updating = true;
    try
    {
      TreeNode node = e.Node;
      if (node.Tag == null)
      {
        this._lbChars.Enabled = false;
      }
      else
      {
        this._lbChars.Enabled = true;
        this._lbChars.SelectedIndex = this._lbChars.Items.IndexOf(node.Tag);
      }
      this._edItemName.Tag = (object) 0;
      if (this._lbChars.Enabled)
        this._edItemName.Text = node.Text.Substring(4);
      else
        this._edItemName.Text = node.Text;
      this._edItemName.Tag = (object) node;
    }
    finally
    {
      this._updating = updating;
    }
  }

  private void AddChars(char first, char last)
  {
    char ch = first;
    while ((int) ch <= (int) last)
      this._availChars.Add(ch++);
  }

  private void AddNewElement(string name)
  {
    this._updating = true;
    try
    {
      char nextChar = this.NextChar;
      if (this._tree.SelectedNode == null)
        this.OnNewGroup_Click((object) this._btNewGroup, EventArgs.Empty);
      TreeNode root = this._tree.SelectedNode;
      if (root == null)
        return;
      if (root.Parent != null)
        root = root.Parent;
      TreeNode itemNode = this.CreateItemNode(name, nextChar, root);
      this._tree.SelectedNode = itemNode;
      itemNode.EnsureVisible();
      this.ActiveControl = (Control) this._edItemName;
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
    this._updating = false;
  }

  private TreeNode AddNewGroup(string groupName)
  {
    TreeNode treeNode = this._tree.Nodes.Add(groupName);
    treeNode.Tag = (object) null;
    treeNode.ImageIndex = 3;
    treeNode.SelectedImageIndex = 3;
    this._tree.SelectedNode = treeNode;
    return treeNode;
  }

  private void AppendSubnodes(TreeNode root, StringBuilder sb)
  {
    TreeNodeCollection nodes = root.Nodes;
    int count = nodes.Count;
    for (int index = 0; index < count; ++index)
    {
      TreeNode treeNode = nodes[index];
      sb.AppendLine($"\t{treeNode.Text}");
    }
  }

  private string CreateNewName(string template)
  {
    int num = sc_7937.ssp_imbase_7941(716153119);
    int count = this._tree.Nodes.Count;
    bool flag;
    string name;
    do
    {
      flag = false;
      name = $"{template} {num++}";
      for (int index = 0; index < count; ++index)
      {
        flag = this.FindNode(this._tree.Nodes[index], name);
        if (flag)
          break;
      }
    }
    while (flag);
    return name;
  }

  private TreeNode CreateItemNode(string name, char ch, TreeNode root)
  {
    string text = $"[{ch}] {name}";
    TreeNode itemNode = root.Nodes.Add(text);
    itemNode.ImageIndex = 4;
    itemNode.SelectedImageIndex = 4;
    itemNode.Tag = (object) ch;
    this.UseChar(ch);
    return itemNode;
  }

  private void DeleteNode(TreeNode node)
  {
    if (node == null)
      return;
    if (node.Text[0] == '[' && node.Text[2] == ']')
    {
      this.FreeChar(node.Text[1]);
      node.Remove();
    }
    else
    {
      int count = node.Nodes.Count;
      for (int index = 0; index < count; ++index)
        this.DeleteNode(node.Nodes[0]);
      node.Remove();
    }
  }

  private bool FindNode(TreeNode node, string name)
  {
    if (this.GetNodeText(node) == name)
      return true;
    int count = node.Nodes.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.FindNode(node.Nodes[index], name))
        return true;
    }
    return false;
  }

  private void FreeChar(char ch)
  {
    if (this._availChars.IndexOf(ch) != -1)
      return;
    this._availChars.Add(ch);
    this._availChars.Sort();
  }

  private void UseChar(char ch)
  {
    if (this._availChars.IndexOf(ch) == -1)
      return;
    this._availChars.Remove(ch);
  }

  private string GetNodeText(TreeNode node)
  {
    string nodeText = node.Text;
    if (nodeText[0] == '[')
      nodeText = nodeText.Substring(4);
    return nodeText;
  }

  private void InitializeChars()
  {
    this._availChars.Clear();
    this.AddChars('a', 'z');
    this.AddChars('A', 'Z');
    this.AddChars('0', '9');
    this.AddChars('А', 'Я');
    this.AddChars('а', 'я');
    this._availChars.Sort();
  }

  private void UpdateNodeCodes(TreeNode root)
  {
    int count = root.Nodes.Count;
    for (int index = 0; index < count; ++index)
    {
      TreeNode node = root.Nodes[index];
      char nextChar = this.NextChar;
      node.Text = new StringBuilder(node.Text)
      {
        [1] = nextChar
      }.ToString();
      node.Tag = (object) nextChar;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SymbolSetEditor));
    this._btOK = new Button();
    this._btCancel = new Button();
    this._tree = new TreeView();
    this.imageList1 = new ImageList(this.components);
    this._btNewGroup = new Button();
    this._btNewItem = new Button();
    this._btDelete = new Button();
    this._btRefresh = new Button();
    this._edItemName = new TextBox();
    this._lbItemName = new Label();
    this._lbSymbol = new Label();
    this._lbChars = new ComboBox();
    this._btMoveUp = new Button();
    this._btMoveDown = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btOK, "_btOK");
    this._btOK.DialogResult = DialogResult.OK;
    this._btOK.Name = "_btOK";
    this._btOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btCancel, "_btCancel");
    this._btCancel.DialogResult = DialogResult.Cancel;
    this._btCancel.Name = "_btCancel";
    this._btCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._tree, "_tree");
    this._tree.HideSelection = false;
    this._tree.HotTracking = true;
    this._tree.ImageList = this.imageList1;
    this._tree.ItemHeight = 17;
    this._tree.Name = "_tree";
    this._tree.AfterSelect += new TreeViewEventHandler(this.Tree_AfterSelect);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Fuchsia;
    this.imageList1.Images.SetKeyName(0, "вверх.png");
    this.imageList1.Images.SetKeyName(1, "вниз.png");
    this.imageList1.Images.SetKeyName(2, "NewGroupBox");
    this.imageList1.Images.SetKeyName(3, "GroupBox");
    this.imageList1.Images.SetKeyName(4, "RadioButton");
    this.imageList1.Images.SetKeyName(5, "NewRadioButton");
    this.imageList1.Images.SetKeyName(6, "удалить.png");
    this.imageList1.Images.SetKeyName(7, "обновить.png");
    componentResourceManager.ApplyResources((object) this._btNewGroup, "_btNewGroup");
    this._btNewGroup.ImageList = this.imageList1;
    this._btNewGroup.Name = "_btNewGroup";
    this._btNewGroup.UseVisualStyleBackColor = true;
    this._btNewGroup.Click += new EventHandler(this.OnNewGroup_Click);
    componentResourceManager.ApplyResources((object) this._btNewItem, "_btNewItem");
    this._btNewItem.ImageList = this.imageList1;
    this._btNewItem.Name = "_btNewItem";
    this._btNewItem.UseVisualStyleBackColor = true;
    this._btNewItem.Click += new EventHandler(this.OnNewItem_Click);
    componentResourceManager.ApplyResources((object) this._btDelete, "_btDelete");
    this._btDelete.ImageList = this.imageList1;
    this._btDelete.Name = "_btDelete";
    this._btDelete.UseVisualStyleBackColor = true;
    this._btDelete.Click += new EventHandler(this.OnDelete_Click);
    componentResourceManager.ApplyResources((object) this._btRefresh, "_btRefresh");
    this._btRefresh.ImageList = this.imageList1;
    this._btRefresh.Name = "_btRefresh";
    this._btRefresh.UseVisualStyleBackColor = true;
    this._btRefresh.Click += new EventHandler(this.OnbtRefresh_Click);
    componentResourceManager.ApplyResources((object) this._edItemName, "_edItemName");
    this._edItemName.Name = "_edItemName";
    this._edItemName.TextChanged += new EventHandler(this.OnItemName_TextChanged);
    componentResourceManager.ApplyResources((object) this._lbItemName, "_lbItemName");
    this._lbItemName.Name = "_lbItemName";
    componentResourceManager.ApplyResources((object) this._lbSymbol, "_lbSymbol");
    this._lbSymbol.Name = "_lbSymbol";
    componentResourceManager.ApplyResources((object) this._lbChars, "_lbChars");
    this._lbChars.DrawMode = DrawMode.OwnerDrawFixed;
    this._lbChars.FormattingEnabled = true;
    this._lbChars.Name = "_lbChars";
    this._lbChars.DrawItem += new DrawItemEventHandler(this.OnChars_DrawItem);
    this._lbChars.SelectedIndexChanged += new EventHandler(this.OnlbChars_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._btMoveUp, "_btMoveUp");
    this._btMoveUp.ImageList = this.imageList1;
    this._btMoveUp.Name = "_btMoveUp";
    this.toolTip1.SetToolTip((Control) this._btMoveUp, componentResourceManager.GetString("_btMoveUp.ToolTip"));
    this._btMoveUp.UseVisualStyleBackColor = true;
    this._btMoveUp.Click += new EventHandler(this.OnMoveUp_Click);
    componentResourceManager.ApplyResources((object) this._btMoveDown, "_btMoveDown");
    this._btMoveDown.ImageList = this.imageList1;
    this._btMoveDown.Name = "_btMoveDown";
    this.toolTip1.SetToolTip((Control) this._btMoveDown, componentResourceManager.GetString("_btMoveDown.ToolTip"));
    this._btMoveDown.UseVisualStyleBackColor = true;
    this._btMoveDown.Click += new EventHandler(this.OnMoveDown_Click);
    this.AcceptButton = (IButtonControl) this._btOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btCancel;
    this.Controls.Add((Control) this._btMoveDown);
    this.Controls.Add((Control) this._btMoveUp);
    this.Controls.Add((Control) this._lbChars);
    this.Controls.Add((Control) this._lbSymbol);
    this.Controls.Add((Control) this._lbItemName);
    this.Controls.Add((Control) this._edItemName);
    this.Controls.Add((Control) this._btRefresh);
    this.Controls.Add((Control) this._btDelete);
    this.Controls.Add((Control) this._btNewItem);
    this.Controls.Add((Control) this._btNewGroup);
    this.Controls.Add((Control) this._tree);
    this.Controls.Add((Control) this._btCancel);
    this.Controls.Add((Control) this._btOK);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SymbolSetEditor);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
