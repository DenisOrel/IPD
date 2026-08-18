// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.SelectFolder2
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core.Thumbnail;
using Intermech.Controls.Thumbnail;
using Intermech.Imbase.Controls;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.API;

public class SelectFolder2 : Form
{
  private long _folderId;
  private long _catalogId;
  protected List<ThumbnailItem> _items = new List<ThumbnailItem>();
  protected ThumbnailRenderer _renderer;
  private IPicturesCache _cache;
  private IContainer components;
  private TreeView _treeView;
  private Button btOK;
  private Button btCancel;
  private TreeBuilder treeBuilder;
  private SplitContainer splitContainer1;
  private ThumbnailGrid _thumbnails;

  public static long Select(long catalogId, string prompt, out long parentId)
  {
    long num = 0;
    parentId = num;
    using (SelectFolder2 selectFolder2 = new SelectFolder2())
    {
      selectFolder2.SetData(catalogId, prompt);
      if (selectFolder2.ShowDialog() == DialogResult.OK)
        num = selectFolder2.GetData(out parentId);
    }
    return num;
  }

  public SelectFolder2()
  {
    this.InitializeComponent();
    this._folderId = -1L;
    if (this.DesignMode)
      return;
    this._cache = ServicesManager.GetService(typeof (IPicturesCache)) as IPicturesCache;
    if (this._cache != null)
      this._cache.CacheChanged += new CacheChangedEventHandler(this.Cache_Changed);
    this._renderer = new ThumbnailRenderer(this.Font, new GetImageHandler(this.Renderer_OnGetImage));
    this._thumbnails.Renderer = (IThumbnailRenderer) this._renderer;
  }

  private void SetData(long catalogId, string prompt)
  {
    this._catalogId = catalogId;
    this.Text = prompt;
  }

  private void Cache_Changed(object sender, long objectId)
  {
    if (this._items == null)
      return;
    bool flag = false;
    int count = this._items.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this._items[index].PictureObjectId == objectId)
      {
        this._items[index].CleanCache();
        flag = true;
      }
    }
    if (!flag)
      return;
    this._thumbnails.Invalidate();
  }

  private object Renderer_OnGetImage(int imageIndex)
  {
    ThumbnailItem thumbnailItem = this._items[imageIndex];
    object image = thumbnailItem.Image;
    if (image != null || this._cache == null)
      return image;
    long newObjectId;
    object picture = this._cache.GetPicture(thumbnailItem.TypeId, thumbnailItem.PictureObjectId, out newObjectId);
    if (thumbnailItem.PictureObjectId != newObjectId)
      thumbnailItem.PictureObjectId = newObjectId;
    thumbnailItem.Image = picture;
    return picture;
  }

  private long GetData(out long parentId)
  {
    TreeNode selectedNode = this._treeView.SelectedNode;
    TreeNode parent = selectedNode.Parent;
    parentId = (parent.Tag as NodeInfo).ObjectId;
    return (selectedNode.Tag as NodeInfo).ObjectId;
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

  private void SelectFolder_Shown(object sender, EventArgs e)
  {
    this.Activate();
    TableViewForm.SetForeWindow(this.Handle);
    this.treeBuilder.Catalogs = new long[1]
    {
      this._catalogId
    };
  }

  private void TreeNode_AfterSelect(object sender, TreeViewEventArgs e)
  {
    bool flag = false;
    TreeNode node1 = e.Node;
    if (node1 == null)
      return;
    if (this.treeBuilder.UnexploredNode(node1))
      this.treeBuilder.ExploreNode(node1);
    if (node1.Tag is NodeInfo tag1 && tag1.IsFolder)
    {
      this._folderId = tag1.ObjectId;
      flag = true;
    }
    this._items.Clear();
    foreach (TreeNode node2 in node1.Nodes)
    {
      if (node1.Tag is NodeInfo tag2)
        this._items.Add(new ThumbnailItem((INodeID) null, node2.Text, tag2._objectId, tag2._typeId));
    }
    this._renderer.Items = this._items;
    this._thumbnails.Count = this._items.Count;
    this.btOK.Enabled = flag;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._cache != null)
        this._cache.CacheChanged -= new CacheChangedEventHandler(this.Cache_Changed);
      this._renderer?.Dispose();
      this._renderer = (ThumbnailRenderer) null;
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectFolder2));
    this.btOK = new Button();
    this.btCancel = new Button();
    this._treeView = new TreeView();
    this.treeBuilder = new TreeBuilder(this.components);
    this.splitContainer1 = new SplitContainer();
    this._thumbnails = new ThumbnailGrid();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
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
    this.treeBuilder.ShowTableReferences = false;
    this.treeBuilder.TreeView = this._treeView;
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this._treeView);
    this.splitContainer1.Panel2.Controls.Add((Control) this._thumbnails);
    componentResourceManager.ApplyResources((object) this._thumbnails, "_thumbnails");
    this._thumbnails.ItemIndex = 0;
    this._thumbnails.Name = "_thumbnails";
    this._thumbnails.PanelSize = new Size(150, 120);
    this._thumbnails.Renderer = (IThumbnailRenderer) null;
    this.AcceptButton = (IButtonControl) this.btOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOK);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (SelectFolder2);
    this.ShowIcon = false;
    this.Shown += new EventHandler(this.SelectFolder_Shown);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
