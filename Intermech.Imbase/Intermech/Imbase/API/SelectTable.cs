// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.SelectTable
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.API;

public class SelectTable : Form
{
  private long _linkId;
  private long _catalogId;
  private IContainer components;
  private TreeView _treeView;
  private Label label1;
  private Button btOK;
  private Button btCancel;
  private TreeBuilder treeBuilder;

  public static long Select(long catalogId, string prompt, ref long tableId, ref string fullList)
  {
    long num = -1;
    using (SelectTable selectTable = new SelectTable())
    {
      selectTable.SetData(catalogId, prompt);
      if (selectTable.ShowDialog() == DialogResult.OK)
        num = selectTable.GetData(ref tableId, ref fullList);
    }
    return num;
  }

  public SelectTable()
  {
    this.InitializeComponent();
    this._linkId = -1L;
  }

  private void SetData(long catalogId, string prompt)
  {
    this._catalogId = catalogId;
    this.label1.Text = prompt;
  }

  private long GetData(ref long tableId, ref string fullList)
  {
    long linkId = this._linkId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBAttribute attributeById1 = session.GetObject(this._linkId).GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
      if (attributeById1 != null)
      {
        if (attributeById1.Value != null)
        {
          IDBObject dbObject = session.GetObject(Convert.ToInt64(attributeById1.Value));
          string empty = string.Empty;
          if (dbObject != null)
          {
            IDBAttribute attributeById2 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID);
            if (attributeById2 != null)
              empty = attributeById2.Value.ToString();
          }
          TreeNode folderNode = this.GetFolderNode();
          TreeNode catalogNode = this.GetCatalogNode();
          TreeNode selectedNode = this._treeView.SelectedNode;
          StringBuilder stringBuilder = new StringBuilder(64 /*0x40*/);
          string str = string.Empty;
          long objectId = (catalogNode.Tag as NodeInfo).ObjectId;
          IDBAttribute attributeById3 = session.GetObject(objectId).GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID);
          if (attributeById3 != null)
            str = attributeById3.AsString;
          stringBuilder.AppendFormat("CtlName={0}{1}CtlId={2}{1}", (object) str, (object) Environment.NewLine, (object) objectId);
          stringBuilder.AppendFormat("TblName={0}{1}TblId={2}{1}", (object) empty, (object) Environment.NewLine, (object) dbObject.ObjectID);
          stringBuilder.AppendFormat("FldKey={0}{1}CtlPath=\\\\{2}{1}", (object) (folderNode.Tag as NodeInfo).ObjectId, (object) Environment.NewLine, (object) folderNode.FullPath);
          stringBuilder.AppendFormat("CtlRec={0}", (object) (selectedNode.Tag as NodeInfo).ObjectId);
          fullList = stringBuilder.ToString();
        }
      }
    }
    return linkId;
  }

  private TreeNode GetCatalogNode()
  {
    for (TreeNode catalogNode = this._treeView.SelectedNode; catalogNode != null; catalogNode = catalogNode.Parent)
    {
      if (catalogNode.Tag is NodeInfo tag && tag.IsCatalog)
        return catalogNode;
    }
    return (TreeNode) null;
  }

  private TreeNode GetFolderNode()
  {
    for (TreeNode folderNode = this._treeView.SelectedNode; folderNode != null; folderNode = folderNode.Parent)
    {
      if (folderNode.Tag is NodeInfo tag && tag.IsFolder)
        return folderNode;
    }
    return (TreeNode) null;
  }

  private void SelectTable_Shown(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        return;
      this.treeBuilder.Catalogs = customService.GetCatalogsList(session.SessionGUID);
      if (this._catalogId == -1L)
        return;
      TreeNodeCollection nodes = this._treeView.Nodes;
      int count = nodes.Count;
      for (int index = 0; index < count; ++index)
      {
        TreeNode treeNode = nodes[index];
        if (treeNode.Tag is NodeInfo tag && tag.IsCatalog && tag.ObjectId == this._catalogId)
        {
          this._treeView.SelectedNode = treeNode;
          treeNode.EnsureVisible();
          break;
        }
      }
    }
  }

  private void TreeNode_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.btOK.Enabled = false;
    TreeNode node = e.Node;
    if (node == null || !(node.Tag is NodeInfo tag) || !tag.IsTableReference)
      return;
    this._linkId = tag.ObjectId;
    this.btOK.Enabled = true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectTable));
    this.label1 = new Label();
    this.btOK = new Button();
    this.btCancel = new Button();
    this._treeView = new TreeView();
    this.treeBuilder = new TreeBuilder(this.components);
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.btOK, "btOK");
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Name = "btOK";
    this.btOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._treeView, "_treeView");
    this._treeView.HideSelection = false;
    this._treeView.Name = "_treeView";
    this._treeView.Sorted = true;
    this._treeView.AfterSelect += new TreeViewEventHandler(this.TreeNode_AfterSelect);
    this.treeBuilder.Catalogs = new long[0];
    this.treeBuilder.Checked = new long[0];
    this.treeBuilder.ShowCatalogRecords = false;
    this.treeBuilder.TreeView = this._treeView;
    this.AcceptButton = (IButtonControl) this.btOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOK);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._treeView);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (SelectTable);
    this.ShowIcon = false;
    this.Shown += new EventHandler(this.SelectTable_Shown);
    this.ResumeLayout(false);
  }
}
