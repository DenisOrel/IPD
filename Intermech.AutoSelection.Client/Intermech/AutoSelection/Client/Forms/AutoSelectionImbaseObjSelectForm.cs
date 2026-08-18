// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionImbaseObjSelectForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Imbase.Controls;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public class AutoSelectionImbaseObjSelectForm : Form
{
  private long _objectId;
  private readonly int _objTypeId;
  private long _imCatalogId;
  private readonly long _imbaseObjectId;
  private TreeBuilder _treeBuilder;
  private IContainer components;
  private Button btnCancel;
  private Panel pnlBottom;
  private Button btnOK;
  private TreeView tvImbase;
  private ToolStrip tstripMain;
  private ToolStripDropDownButton tsddbtnSearch;
  private ToolStripMenuItem tsmiSearchByName;
  private ToolStripMenuItem tsmiSearchByImage;
  private ToolStripMenuItem tsmiSearchByTable;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripLabel tslblSpace;
  private ContextMenuStrip cmsImbaseTree;
  private ToolStripMenuItem tsmiImbaseFind;
  private ToolStripMenuItem tsmiImbaseFindName;
  private ToolStripMenuItem tsmiImbaseFindImage;
  private ToolStripMenuItem tsmiImbaseFindTable;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem tsmiImbaseCollapse;
  private ToolStripMenuItem tsmiImbaseUpdate;
  private ImageList imageList;
  private ToolStripButton tsbtnFilterSettings;

  private void InitData()
  {
    if (this.DesignMode)
      return;
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.components == null)
      this.components = (IContainer) new System.ComponentModel.Container();
    this.tsddbtnSearch.Image = this.imageList.Images[0];
    this.tvImbase.CheckBoxes = false;
    this._treeBuilder = new TreeBuilder(this.components)
    {
      TreeView = this.tvImbase
    };
    this._treeBuilder.Selected += new SelectEventHandler(this.TreeBuilder_Selected);
  }

  private void LoadData()
  {
    if (this.DesignMode)
      return;
    this._treeBuilder.ShowTreeForType(this._objTypeId);
    if (this._imbaseObjectId != 0L)
    {
      this._treeBuilder.LoadFullTree(new List<long>((IEnumerable<long>) new long[1]
      {
        this._imbaseObjectId
      }));
      this.tvImbase.SelectedNode = this.GetImbaseNode(this.tvImbase.Nodes, this._imbaseObjectId) ?? throw new ArgumentNullException("GetImbaseNode(this.tvImbase.Nodes, this._imbaseObjectId)");
    }
    else
      this.tvImbase.SelectedNode = this.tvImbase.Nodes.Count > 0 ? this.tvImbase.Nodes[0] : (TreeNode) null;
  }

  private void FindByName()
  {
    FindByNameView.Show((object) this.tvImbase.SelectedNode, true, (LocateNodeEventHandler) null);
  }

  private void FindByImage()
  {
    FindByImagesView.Show((object) this.tvImbase.SelectedNode, true, (LocateNodeEventHandler) null);
  }

  private void FindInTable()
  {
    FindInTablesView.Show((object) this.tvImbase.SelectedNode, true, (LocateNodeEventHandler) null);
  }

  private TreeNode GetImbaseNode(TreeNodeCollection nodes, long imbaseObjId)
  {
    TreeNode imbaseNode = (TreeNode) null;
    if (nodes == null || imbaseObjId == 0L)
      return (TreeNode) null;
    foreach (TreeNode node in nodes)
    {
      if (node != null)
      {
        if (node.Tag is NodeInfo tag && tag.ObjectId == imbaseObjId)
          return node;
        imbaseNode = this.GetImbaseNode(node.Nodes, imbaseObjId);
        if (imbaseNode != null)
          break;
      }
    }
    return imbaseNode;
  }

  public AutoSelectionImbaseObjSelectForm(int objTypeId, long imbaseObjectId)
  {
    this._objTypeId = objTypeId;
    this._imbaseObjectId = imbaseObjectId;
    this.InitializeComponent();
    this.InitData();
    this.LoadData();
  }

  public long ImbaseCatalogID => this._imCatalogId;

  public long ImbaseObjID => this._objectId;

  private void TreeBuilder_Selected(object sender, TreeViewSelectEventArgs e)
  {
    if (e == null)
      return;
    if (e.NodeInfo == null)
    {
      this._imCatalogId = 0L;
      this._objectId = 0L;
    }
    else
    {
      this._objectId = e.NodeInfo.ObjectId;
      TreeNode treeNode = this.tvImbase.SelectedNode;
      while (treeNode.Parent != null)
        treeNode = treeNode.Parent;
      this._imCatalogId = !(treeNode.Tag is NodeInfo tag) || !tag.IsCatalog ? 0L : tag.ObjectId;
    }
    this.tsddbtnSearch.Enabled = this.tvImbase.SelectedNode != null;
  }

  private void tsmiSearchByName_Click(object sender, EventArgs e) => this.FindByName();

  private void tsmiSearchByImage_Click(object sender, EventArgs e) => this.FindByImage();

  private void tsmiSearchByTable_Click(object sender, EventArgs e) => this.FindInTable();

  private void tsmiImbaseFindName_Click(object sender, EventArgs e) => this.FindByName();

  private void tsmiImbaseFindImage_Click(object sender, EventArgs e) => this.FindByImage();

  private void tsmiImbaseFindTable_Click(object sender, EventArgs e) => this.FindInTable();

  private void tsmiImbaseCollapse_Click(object sender, EventArgs e) => this.tvImbase.CollapseAll();

  private void tsmiImbaseUpdate_Click(object sender, EventArgs e) => this.LoadData();

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.components?.Dispose();
      this._treeBuilder.Dispose();
      this._treeBuilder = (TreeBuilder) null;
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionImbaseObjSelectForm));
    this.btnCancel = new Button();
    this.pnlBottom = new Panel();
    this.btnOK = new Button();
    this.tvImbase = new TreeView();
    this.cmsImbaseTree = new ContextMenuStrip(this.components);
    this.tsmiImbaseFind = new ToolStripMenuItem();
    this.tsmiImbaseFindName = new ToolStripMenuItem();
    this.tsmiImbaseFindImage = new ToolStripMenuItem();
    this.tsmiImbaseFindTable = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.tsmiImbaseCollapse = new ToolStripMenuItem();
    this.tsmiImbaseUpdate = new ToolStripMenuItem();
    this.tstripMain = new ToolStrip();
    this.tsddbtnSearch = new ToolStripDropDownButton();
    this.tsmiSearchByName = new ToolStripMenuItem();
    this.tsmiSearchByImage = new ToolStripMenuItem();
    this.tsmiSearchByTable = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.tslblSpace = new ToolStripLabel();
    this.imageList = new ImageList(this.components);
    this.tsbtnFilterSettings = new ToolStripButton();
    this.pnlBottom.SuspendLayout();
    this.cmsImbaseTree.SuspendLayout();
    this.tstripMain.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.pnlBottom.Controls.Add((Control) this.btnCancel);
    this.pnlBottom.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.tvImbase.ContextMenuStrip = this.cmsImbaseTree;
    componentResourceManager.ApplyResources((object) this.tvImbase, "tvImbase");
    this.tvImbase.HideSelection = false;
    this.tvImbase.Name = "tvImbase";
    this.cmsImbaseTree.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiImbaseFind,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.tsmiImbaseCollapse,
      (ToolStripItem) this.tsmiImbaseUpdate
    });
    this.cmsImbaseTree.Name = "cmsImbaseTree";
    componentResourceManager.ApplyResources((object) this.cmsImbaseTree, "cmsImbaseTree");
    this.tsmiImbaseFind.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsmiImbaseFindName,
      (ToolStripItem) this.tsmiImbaseFindImage,
      (ToolStripItem) this.tsmiImbaseFindTable
    });
    this.tsmiImbaseFind.Name = "tsmiImbaseFind";
    componentResourceManager.ApplyResources((object) this.tsmiImbaseFind, "tsmiImbaseFind");
    this.tsmiImbaseFindName.Name = "tsmiImbaseFindName";
    componentResourceManager.ApplyResources((object) this.tsmiImbaseFindName, "tsmiImbaseFindName");
    this.tsmiImbaseFindName.Click += new EventHandler(this.tsmiImbaseFindName_Click);
    this.tsmiImbaseFindImage.Name = "tsmiImbaseFindImage";
    componentResourceManager.ApplyResources((object) this.tsmiImbaseFindImage, "tsmiImbaseFindImage");
    this.tsmiImbaseFindImage.Click += new EventHandler(this.tsmiImbaseFindImage_Click);
    this.tsmiImbaseFindTable.Name = "tsmiImbaseFindTable";
    componentResourceManager.ApplyResources((object) this.tsmiImbaseFindTable, "tsmiImbaseFindTable");
    this.tsmiImbaseFindTable.Click += new EventHandler(this.tsmiImbaseFindTable_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    this.tsmiImbaseCollapse.Name = "tsmiImbaseCollapse";
    componentResourceManager.ApplyResources((object) this.tsmiImbaseCollapse, "tsmiImbaseCollapse");
    this.tsmiImbaseCollapse.Click += new EventHandler(this.tsmiImbaseCollapse_Click);
    this.tsmiImbaseUpdate.Name = "tsmiImbaseUpdate";
    componentResourceManager.ApplyResources((object) this.tsmiImbaseUpdate, "tsmiImbaseUpdate");
    this.tsmiImbaseUpdate.Click += new EventHandler(this.tsmiImbaseUpdate_Click);
    this.tstripMain.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsddbtnSearch,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.tsbtnFilterSettings,
      (ToolStripItem) this.tslblSpace
    });
    componentResourceManager.ApplyResources((object) this.tstripMain, "tstripMain");
    this.tstripMain.Name = "tstripMain";
    this.tsddbtnSearch.Alignment = ToolStripItemAlignment.Right;
    this.tsddbtnSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsddbtnSearch.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsmiSearchByName,
      (ToolStripItem) this.tsmiSearchByImage,
      (ToolStripItem) this.tsmiSearchByTable
    });
    componentResourceManager.ApplyResources((object) this.tsddbtnSearch, "tsddbtnSearch");
    this.tsddbtnSearch.Name = "tsddbtnSearch";
    this.tsmiSearchByName.Name = "tsmiSearchByName";
    componentResourceManager.ApplyResources((object) this.tsmiSearchByName, "tsmiSearchByName");
    this.tsmiSearchByName.Click += new EventHandler(this.tsmiSearchByName_Click);
    this.tsmiSearchByImage.Name = "tsmiSearchByImage";
    componentResourceManager.ApplyResources((object) this.tsmiSearchByImage, "tsmiSearchByImage");
    this.tsmiSearchByImage.Click += new EventHandler(this.tsmiSearchByImage_Click);
    this.tsmiSearchByTable.Name = "tsmiSearchByTable";
    componentResourceManager.ApplyResources((object) this.tsmiSearchByTable, "tsmiSearchByTable");
    this.tsmiSearchByTable.Click += new EventHandler(this.tsmiSearchByTable_Click);
    this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this.tslblSpace.Alignment = ToolStripItemAlignment.Right;
    this.tslblSpace.Name = "tslblSpace";
    componentResourceManager.ApplyResources((object) this.tslblSpace, "tslblSpace");
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "look_comp.ico");
    this.tsbtnFilterSettings.Alignment = ToolStripItemAlignment.Right;
    this.tsbtnFilterSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsbtnFilterSettings, "tsbtnFilterSettings");
    this.tsbtnFilterSettings.Name = "tsbtnFilterSettings";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tvImbase);
    this.Controls.Add((Control) this.pnlBottom);
    this.Controls.Add((Control) this.tstripMain);
    this.Name = nameof (AutoSelectionImbaseObjSelectForm);
    this.ShowInTaskbar = false;
    this.pnlBottom.ResumeLayout(false);
    this.cmsImbaseTree.ResumeLayout(false);
    this.tstripMain.ResumeLayout(false);
    this.tstripMain.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
