// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.TableFolders
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.API;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Selection;

public class TableFolders : Form
{
  private Dictionary<long, TreeNode> _nodes;
  private IContainer components;
  private Button _okButton;
  private Button _cancelButton;
  private TreeView _treeView;
  private TreeBuilder _treeBuilder;

  public TableFolders()
  {
    this.InitializeComponent();
    this._nodes = new Dictionary<long, TreeNode>(32 /*0x20*/);
  }

  internal static long Select(
    string objectDef,
    string catalogDef,
    ref string tableName,
    ref long tableKey)
  {
    long objectID = -1;
    tableKey = -1L;
    tableName = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      List<long> ids;
      DataTable tree;
      CadmechHelper.GetServer(session).ResolveObjectDef(session.SessionGUID, objectDef, catalogDef, out ids, out tree);
      if (ids.Count == 1)
      {
        objectID = ids[0];
      }
      else
      {
        using (TableFolders tableFolders = new TableFolders())
        {
          tableFolders.SetData(tree);
          if (tableFolders.ShowDialog() == DialogResult.OK)
            objectID = tableFolders.TableId;
        }
      }
      if (objectID != -1L)
      {
        IDBAttribute attributeById1 = session.GetObject(objectID).GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
        if (attributeById1 != null)
        {
          long asInteger = attributeById1.AsInteger;
          IDBAttribute attributeById2 = session.GetObject(asInteger).GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID);
          if (attributeById2 != null)
            tableName = attributeById2.AsString;
        }
      }
    }
    return objectID;
  }

  internal static long Select(DataTable tree, long defaultId)
  {
    long num = -1;
    using (TableFolders tableFolders = new TableFolders())
    {
      tableFolders.SetData(tree);
      tableFolders.TableId = defaultId;
      if (tableFolders.ShowDialog() == DialogResult.OK)
        num = tableFolders.TableId;
    }
    return num;
  }

  private long TableId
  {
    get
    {
      TreeNode selectedNode = this._treeView.SelectedNode;
      return selectedNode != null && selectedNode.Tag is NodeInfo tag ? tag._objectId : -1L;
    }
    set
    {
      if (value == -1L)
      {
        TableFolders.SelectFirstRefNode(this._treeView);
      }
      else
      {
        TreeNode node = this._nodes[value];
        if (node == null)
          return;
        this._treeView.SelectedNode = node;
      }
    }
  }

  internal static void SelectFirstRefNode(TreeView tree)
  {
    TreeNodeCollection nodes = tree.Nodes;
    TableFolders.SelectFirstRefNode(tree, nodes);
  }

  private static void SelectFirstRefNode(TreeView tree, TreeNodeCollection nodes)
  {
    if (nodes == null || nodes.Count <= 0)
      return;
    TreeNode node = nodes[0];
    if (node.Tag is NodeInfo tag && tag.IsTableReference)
      tree.SelectedNode = node;
    else
      TableFolders.SelectFirstRefNode(tree, node.Nodes);
  }

  private void SetData(DataTable tree)
  {
    this._treeBuilder.CreateTree(tree, (IDictionary<long, TreeNode>) this._nodes);
  }

  private void TreeNode_Selected(object sender, TreeViewSelectEventArgs e)
  {
    this._okButton.Enabled = e.NodeInfo.IsTableReference;
  }

  private void OwnFormSet(Form owner, Form value, bool add)
  {
    if (add)
      owner.AddOwnedForm(value);
    else
      owner.RemoveOwnedForm(value);
  }

  private void _treeView_DoubleClick(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    if (selectedNode == null || !(selectedNode.Tag is NodeInfo tag) || !tag.IsTableReference)
      return;
    this._okButton.PerformClick();
  }

  private void TableFolders_Shown(object sender, EventArgs e)
  {
    TableViewForm.SetForeWindow(this.Handle);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableFolders));
    this._okButton = new Button();
    this._cancelButton = new Button();
    this._treeView = new TreeView();
    this._treeBuilder = new TreeBuilder(this.components);
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Name = "_okButton";
    this._okButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._treeView, "_treeView");
    this._treeView.HideSelection = false;
    this._treeView.Name = "_treeView";
    this._treeView.DoubleClick += new EventHandler(this._treeView_DoubleClick);
    this._treeBuilder.Catalogs = new long[0];
    this._treeBuilder.Checked = new long[0];
    this._treeBuilder.TreeView = this._treeView;
    this._treeBuilder.Selected += new SelectEventHandler(this.TreeNode_Selected);
    this.AcceptButton = (IButtonControl) this._okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.Controls.Add((Control) this._cancelButton);
    this.Controls.Add((Control) this._okButton);
    this.Controls.Add((Control) this._treeView);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TableFolders);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.TopMost = true;
    this.Shown += new EventHandler(this.TableFolders_Shown);
    this.ResumeLayout(false);
  }

  private delegate void OwnFormSetDelegate(Form owner, Form value, bool add);
}
