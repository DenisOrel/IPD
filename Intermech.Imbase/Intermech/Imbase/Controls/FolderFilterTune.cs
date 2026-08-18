// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.FolderFilterTune
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Comparers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class FolderFilterTune : UserControl
{
  protected long _masterCatalog = -1;
  protected long _slaveCatalog = -1;
  protected string _ownerGuid = string.Empty;
  protected Dictionary<Guid, TreeNode> _masterGuid2Node = new Dictionary<Guid, TreeNode>();
  protected Dictionary<Guid, TreeNode> _slaveGuid2Node = new Dictionary<Guid, TreeNode>();
  protected bool _readOnly;
  protected bool _loading;
  protected bool _createSlaveTreeMode;
  protected bool _filterDirty;
  protected DataTable _filterData;
  protected List<Guid> _filterCurrData = new List<Guid>(32 /*0x20*/);
  protected List<Guid> _filterOrgData = new List<Guid>(32 /*0x20*/);
  private IContainer components;
  private SplitContainer splitContainer1;
  private TreeView _masterTree;
  private TreeView _slaveTree;
  private SplitContainer splitContainer2;
  private ListBox _checkedList;
  private ImageList imageList;
  private ComboBox _masterCombo;
  private Panel pnlCheckListInfo;
  private Label lblCheckedLlistInfo;
  private Panel pnlRightTop;
  private Panel pnlLeftTop;
  private Panel pnlLeftSplit;
  private ImageList ilStateImages;
  private ContextMenuStrip cmsFilter;
  private ToolStripMenuItem tsmiFilterCopy;
  private ToolStripMenuItem tsmiFilterPaste;
  private ToolStripMenuItem tsmiFilterClear;
  private ToolStripSeparator tsmiFilterSep1;

  protected virtual void InitializeCustomComponents()
  {
    this._masterTree.TreeViewNodeSorter = (IComparer) new NodeComparer();
    this._slaveTree.TreeViewNodeSorter = (IComparer) new NodeComparer();
    FolderFilterTune.InitializeStateImages(this.ilStateImages);
    this._slaveTree.StateImageList = this.ilStateImages;
    this._slaveTree.CheckBoxes = false;
    this._slaveTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(this._slaveTree_MouseClick);
    this._slaveTree.AfterExpand += new TreeViewEventHandler(this._slaveTree_AfterExpand);
    using (Graphics graphics = this._checkedList.CreateGraphics())
    {
      this._checkedList.ItemHeight = (int) Math.Max(graphics.MeasureString("Wq", this._checkedList.Font).Height + 2f, 17f);
      this._checkedList.DrawMode = DrawMode.OwnerDrawFixed;
    }
    this.UpdateControlsState();
  }

  protected virtual void UpdateControlsState()
  {
    this.UpdateSlaveTreeStateIndex(this._slaveTree.Nodes);
  }

  protected virtual void UpdateSlaveTreeStateIndex(TreeNodeCollection nodes)
  {
    foreach (TreeNode node in nodes)
    {
      this.UpdateSlaveNodeStateIndex(node, false);
      this.UpdateSlaveTreeStateIndex(node.Nodes);
    }
  }

  protected virtual void UpdateSlaveNodeStateIndex(TreeNode node, bool fullUpdateMode)
  {
    if (node == null || !(node.Tag is FolderFilterTune.FilterNodeInfo tag1))
      return;
    int stateImageIndex1 = node.StateImageIndex;
    bool flag = false;
    int num = this.ReadOnly ? 4 : 0;
    if (this._filterCurrData.Contains(tag1.ObjGuid))
    {
      node.StateImageIndex = num + 1;
      flag = true;
    }
    else
    {
      foreach (Guid key in this._filterCurrData)
      {
        TreeNode treeNode;
        if (!(key == Guid.Empty) && this._slaveGuid2Node.TryGetValue(key, out treeNode) && treeNode.Tag is FolderFilterTune.FilterNodeInfo tag2 && tag2.NodePath.IndexOf(tag1.NodePath) != -1)
        {
          node.StateImageIndex = num + 2;
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        for (TreeNode parent = node.Parent; parent != null; parent = parent.Parent)
        {
          if (parent.Tag is FolderFilterTune.FilterNodeInfo tag3 && this._filterCurrData.Contains(tag3.ObjGuid))
          {
            node.StateImageIndex = num + 3;
            flag = true;
            break;
          }
        }
      }
    }
    if (!flag)
      node.StateImageIndex = num;
    if (!fullUpdateMode || stateImageIndex1 == node.StateImageIndex)
      return;
    int stateImageIndex2 = node.StateImageIndex;
    if (stateImageIndex2 >= 4)
      stateImageIndex2 -= 4;
    switch (stateImageIndex2)
    {
      case 0:
      case 1:
        this.UpdateSlaveNodeStateIndex(node.Parent, true);
        IEnumerator enumerator1 = node.Nodes.GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
            this.UpdateSlaveNodeStateIndex((TreeNode) enumerator1.Current, true);
          break;
        }
        finally
        {
          if (enumerator1 is IDisposable disposable)
            disposable.Dispose();
        }
      case 2:
        this.UpdateSlaveNodeStateIndex(node.Parent, true);
        break;
      case 3:
        if (stateImageIndex1 == 1)
          this.UpdateSlaveNodeStateIndex(node.Parent, true);
        IEnumerator enumerator2 = node.Nodes.GetEnumerator();
        try
        {
          while (enumerator2.MoveNext())
            this.UpdateSlaveNodeStateIndex((TreeNode) enumerator2.Current, true);
          break;
        }
        finally
        {
          if (enumerator2 is IDisposable disposable)
            disposable.Dispose();
        }
    }
  }

  protected virtual void DoDirtyChanged()
  {
    EventHandler dirtyChanged = this.DirtyChanged;
    if (dirtyChanged == null)
      return;
    dirtyChanged((object) this, EventArgs.Empty);
  }

  protected virtual void DoMasterNodeChanged(TreeNode treeNode)
  {
    EventHandler masterNodeChanged = this.MasterNodeChanged;
    if (masterNodeChanged == null)
      return;
    masterNodeChanged((object) treeNode, EventArgs.Empty);
  }

  protected DataTable GetCatalogTable(long catalogId, bool checkBlobs)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      return session.GetCustomService(typeof (IFolderFilterService)) is IFolderFilterService customService ? customService.LoadCatalogTable(session.SessionGUID, catalogId, checkBlobs) : (DataTable) null;
    }
  }

  protected void LoadMasterCatalog()
  {
    DataTable dt = (DataTable) null;
    if (this._masterCatalog != 0L && this._masterCatalog != -1L)
      dt = this.GetCatalogTable(this._masterCatalog, false);
    this.FilterDataLoad();
    this.BuildTree(this._masterTree, this._masterGuid2Node, dt);
  }

  protected void LoadSlaveCatalog()
  {
    DataTable dt = (DataTable) null;
    if (this._slaveCatalog != 0L && this._slaveCatalog != -1L)
      dt = this.GetCatalogTable(this._slaveCatalog, false);
    this._createSlaveTreeMode = true;
    try
    {
      this.BuildTree(this._slaveTree, this._slaveGuid2Node, dt);
      this._filterCurrData.Clear();
      this._filterOrgData.Clear();
      this.UpdateSlaveTreeStateIndex(this._slaveTree.Nodes);
    }
    finally
    {
      this._createSlaveTreeMode = false;
    }
  }

  protected void BuildTree(
    TreeView treeView,
    Dictionary<Guid, TreeNode> guid2NodeCache,
    DataTable dt)
  {
    bool scrollable = treeView.Scrollable;
    treeView.BeginUpdate();
    try
    {
      treeView.Scrollable = false;
      this._loading = true;
      guid2NodeCache?.Clear();
      treeView.Nodes.Clear();
      if (dt == null || dt.Rows.Count <= 0)
        return;
      DataView dataView = new DataView(dt);
      dataView.Sort = "F_PATH ASC";
      int count = dataView.Count;
      Hashtable hashtable = new Hashtable(count);
      int columnIndex1 = dt.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex2 = dt.Columns.IndexOf("CAPTION");
      int columnIndex3 = dt.Columns.IndexOf("F_OBJECT_TYPE");
      int columnIndex4 = dt.Columns.IndexOf("F_PATH");
      int columnIndex5 = dt.Columns.IndexOf("F_GUID");
      int columnIndex6 = dt.Columns.IndexOf("F_SORT");
      for (int recordIndex = 0; recordIndex < count; ++recordIndex)
      {
        DataRow row = dataView[recordIndex].Row;
        string g = Convert.ToString(row[columnIndex5]);
        string str = Convert.ToString(row[columnIndex4]);
        FolderFilterTune.FilterNodeInfo filterNodeInfo = new FolderFilterTune.FilterNodeInfo(Convert.ToInt64(row[columnIndex1]), Convert.ToInt32(row[columnIndex3]), new Guid(g), str);
        TreeNode node = new TreeNode(Convert.ToString(row[columnIndex2]), 0, 1);
        if (!DBNull.Value.Equals(row[columnIndex6]))
          filterNodeInfo.Order = Convert.ToInt32(row[columnIndex6]);
        node.Tag = (object) filterNodeInfo;
        guid2NodeCache?.Add(new Guid(g), node);
        int length = str.Length - 2;
        string key = str.Substring(0, length);
        if (hashtable[(object) key] is TreeNode treeNode)
        {
          treeNode.Nodes.Add(node);
          treeNode.Expand();
        }
        else
          treeView.Nodes.Add(node);
        hashtable.Add((object) str, (object) node);
      }
      treeView.Sort();
      foreach (TreeNode node in treeView.Nodes)
        node.Collapse(false);
    }
    finally
    {
      treeView.Scrollable = scrollable;
      treeView.EndUpdate();
      this._loading = false;
    }
  }

  private int GetIconIndex(int objectType) => -1;

  protected void UncheckAllNodes(TreeNodeCollection nodes)
  {
    if (nodes == null)
      return;
    bool loading = this._loading;
    try
    {
      this._loading = true;
      foreach (TreeNode node in nodes)
      {
        node.StateImageIndex = 0;
        this.UncheckAllNodes(node.Nodes);
      }
    }
    finally
    {
      this._loading = loading;
    }
  }

  protected void InternalSaveFilter()
  {
    TreeNode selectedNode = this._masterTree.SelectedNode;
    if (selectedNode == null || this._filterOrgData.Count == 0 && this._filterCurrData.Count == 0)
      return;
    List<string> stringList1 = new List<string>(32 /*0x20*/);
    List<string> stringList2 = new List<string>(32 /*0x20*/);
    int count1 = this._filterCurrData.Count;
    for (int index = 0; index < count1; ++index)
    {
      Guid guid = this._filterCurrData[index];
      if (!this._filterOrgData.Contains(guid))
        stringList1.Add(guid.ToString());
    }
    int count2 = this._filterOrgData.Count;
    for (int index = 0; index < count2; ++index)
    {
      if (!this._filterCurrData.Contains(this._filterOrgData[index]))
        stringList2.Add(this._filterOrgData[index].ToString());
    }
    if (stringList2.Count == 0 && stringList1.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session.GetCustomService(typeof (IFolderFilterService)) is IFolderFilterService customService)
      {
        Guid objGuid = ((FolderFilterTune.FilterNodeInfo) selectedNode.Tag).ObjGuid;
        int num = customService.SetFilter(session.SessionGUID, objGuid, this._ownerGuid, stringList1.ToArray(), stringList2.ToArray()) ? 1 : 0;
        bool flag = this._masterCombo.Items.Contains((object) selectedNode);
        if (num != 0)
        {
          if (!flag)
            this._masterCombo.Items.Add((object) selectedNode);
        }
        else if (flag)
          this._masterCombo.Items.Remove((object) selectedNode);
      }
    }
    this.Dirty = false;
  }

  protected void FilterDataCopy()
  {
    if (this._filterCurrData.Count == 0 || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    service.SetDataObject((object) new DataObject((object) new FolderFilterTune.FilterData4Clipboard((IEnumerable<Guid>) this._filterCurrData)));
  }

  protected void FilterDataPaste()
  {
    if (this.ReadOnly || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service) || (!(service.GetDataObject() is DataObject dataObject) ? 0 : (dataObject.GetDataPresent(typeof (FolderFilterTune.FilterData4Clipboard)) ? 1 : 0)) == 0 || !(dataObject.GetData(typeof (FolderFilterTune.FilterData4Clipboard)) is FolderFilterTune.FilterData4Clipboard data) || data.Count == 0)
      return;
    int count = this._filterCurrData.Count;
    foreach (Guid guid in (List<Guid>) data)
    {
      if (!this._filterCurrData.Contains(guid))
        this._filterCurrData.Add(guid);
    }
    if (count == this._filterCurrData.Count)
      return;
    this.Dirty = true;
    this.UpdateSlaveTreeStateIndex(this._slaveTree.Nodes);
  }

  protected void FilterDataClear()
  {
    if (this.ReadOnly || this._filterCurrData.Count == 0)
      return;
    this._filterCurrData.Clear();
    this.Dirty = true;
    this.UncheckAllNodes(this._slaveTree.Nodes);
  }

  protected void FilterDataLoad()
  {
    this._filterData = (DataTable) null;
    if (this._masterCatalog == 0L || this._masterCatalog == -1L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(session.GetCustomService(typeof (IFolderFilterService)) is IFolderFilterService customService))
        return;
      IDBObject dbObject = session.GetObject(this._masterCatalog);
      if (dbObject == null)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
      if (attributeById == null)
        return;
      string data = attributeById.Value.ToString();
      if (data == string.Empty)
        return;
      string str = $"(F_PATH LIKE '{SQLStringHelper.QuoteLikeString(data)}%')";
      string filterCond = !string.IsNullOrEmpty(this._ownerGuid) ? str + $" AND (F_OWNER='{this._ownerGuid}') " : str + $" AND (F_OWNER='{this._ownerGuid}' OR F_OWNER IS NULL)";
      this._filterData = customService.GetFilter(session.SessionGUID, filterCond);
    }
  }

  protected void FilterListUpdate()
  {
    this._masterTree.BeginUpdate();
    this._masterCombo.BeginUpdate();
    try
    {
      this._masterCombo.Items.Clear();
      if (this._masterTree.Nodes.Count == 0)
        return;
      Dictionary<long, List<Guid>> filterCache = new Dictionary<long, List<Guid>>();
      if (this._filterData == null || this._filterData.Rows.Count == 0)
      {
        this.FilterListNodesUpdate(this._masterTree.Nodes, this._masterCombo, filterCache);
      }
      else
      {
        int columnIndex1 = this._filterData.Columns.IndexOf("F_OBJECT_ID");
        int columnIndex2 = this._filterData.Columns.IndexOf("F_GUID");
        foreach (DataRow row in (InternalDataCollectionBase) this._filterData.Rows)
        {
          if (row != null)
          {
            long int64 = Convert.ToInt64(row[columnIndex1]);
            string str = row[columnIndex2].ToString();
            if (int64 != 0L && !(str == string.Empty) && GuidHelper.IsGuid(str))
            {
              List<Guid> guidList;
              if (!filterCache.TryGetValue(int64, out guidList))
              {
                guidList = new List<Guid>();
                filterCache.Add(int64, guidList);
              }
              Guid guid = new Guid(str);
              if (!guidList.Contains(guid))
                guidList.Add(guid);
            }
          }
        }
        this.FilterListNodesUpdate(this._masterTree.Nodes, this._masterCombo, filterCache);
      }
    }
    finally
    {
      this._masterTree.EndUpdate();
      this._masterCombo.EndUpdate();
    }
  }

  protected void FilterListNodesUpdate(
    TreeNodeCollection nodes,
    ComboBox comboBox,
    Dictionary<long, List<Guid>> filterCache)
  {
    if (nodes == null || nodes.Count == 0)
      return;
    foreach (TreeNode node in nodes)
    {
      if (node != null)
      {
        if (!(node.Tag is FolderFilterTune.FilterNodeInfo tag))
        {
          node.ImageIndex = 0;
          node.SelectedImageIndex = 1;
        }
        else
        {
          List<Guid> guidList;
          bool flag = filterCache.TryGetValue(tag.ObjectId, out guidList);
          if (flag)
          {
            flag = false;
            foreach (Guid key in guidList)
            {
              if (this._slaveGuid2Node.ContainsKey(key))
              {
                flag = true;
                break;
              }
            }
          }
          if (flag)
          {
            node.ImageIndex = 2;
            node.SelectedImageIndex = 3;
            comboBox.Items.Add((object) node);
          }
          else if (node.Parent != null && node.Parent.ImageIndex != 0)
          {
            node.ImageIndex = 4;
            node.SelectedImageIndex = 5;
          }
          else
          {
            node.ImageIndex = 0;
            node.SelectedImageIndex = 1;
          }
          this.FilterListNodesUpdate(node.Nodes, comboBox, filterCache);
        }
      }
    }
  }

  public FolderFilterTune()
  {
    this.InitializeComponent();
    this.InitializeCustomComponents();
  }

  public virtual bool SaveFilter()
  {
    if (this._masterCatalog == 0L || this._masterCatalog == -1L || this._loading || !this.Dirty || this.ReadOnly)
      return true;
    TreeNode selectedNode = this._masterTree.SelectedNode;
    if (selectedNode == null)
      return true;
    switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1132"), (object) selectedNode.Text), LocalizationHolder.rm.GetString("Imbase.Client_1133"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
    {
      case DialogResult.Cancel:
        return false;
      case DialogResult.Yes:
        this.InternalSaveFilter();
        this.FilterDataLoad();
        this.FilterListUpdate();
        break;
    }
    return true;
  }

  public virtual void LoadFilter(bool forceReloadCache = false)
  {
    if (forceReloadCache)
      this.FilterDataLoad();
    this._loading = true;
    try
    {
      this.UncheckAllNodes(this._slaveTree.Nodes);
      this._checkedList.Items.Clear();
      TreeNode selectedNode = this._masterTree.SelectedNode;
      if (selectedNode == null || this._filterData == null)
      {
        this._filterOrgData.Clear();
        this._filterCurrData.Clear();
      }
      else
      {
        using (new SessionKeeper())
        {
          DataRow[] dataRowArray = this._filterData.Select("F_OBJECT_ID = " + ((NodeInfo) selectedNode.Tag).ObjectId.ToString());
          int length = dataRowArray.Length;
          List<string> stringList = new List<string>(dataRowArray.Length);
          int columnIndex = this._filterData.Columns.IndexOf("F_GUID");
          foreach (DataRow dataRow in dataRowArray)
          {
            string text = dataRow[columnIndex].ToString();
            if (!(text == string.Empty) && GuidHelper.IsGuid(text) && !stringList.Contains(text))
              stringList.Add(text);
          }
          this._filterCurrData.Clear();
          foreach (TreeNode treeNode in this._slaveGuid2Node.Values)
          {
            Guid objGuid = ((FolderFilterTune.FilterNodeInfo) treeNode.Tag).ObjGuid;
            string str = objGuid.ToString();
            if (stringList.Contains(str))
            {
              this._filterCurrData.Add(objGuid);
              this._checkedList.Items.Add((object) treeNode);
            }
          }
        }
      }
    }
    finally
    {
      this._filterOrgData.Clear();
      this._filterOrgData.AddRange((IEnumerable<Guid>) this._filterCurrData);
      this.UpdateSlaveTreeStateIndex(this._slaveTree.Nodes);
      this._loading = false;
      this.Dirty = false;
    }
  }

  public static void InitializeStateImages(ImageList stateImages)
  {
    if (stateImages == null)
      return;
    stateImages.Images.Clear();
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
    {
      stateImages.Images.Add(service.ImageList.Images[service.ImageIndex("imgUnchecked")]);
      stateImages.Images.Add(service.ImageList.Images[service.ImageIndex("imgChecked")]);
      stateImages.Images.Add(service.ImageList.Images[service.ImageIndex("imgGrayed")]);
    }
    Stream manifestResourceStream = typeof (FolderFilterTune).Assembly.GetManifestResourceStream("Intermech.Imbase.Resources.GrayEmpty.bmp");
    if (manifestResourceStream != null)
    {
      Bitmap bitmap = new Bitmap(manifestResourceStream);
      bitmap.MakeTransparent();
      stateImages.Images.AddStrip((Image) bitmap);
    }
    if (service == null)
      return;
    int count = stateImages.Images.Count;
    for (int index = 0; index < count; ++index)
    {
      Image disabledImage = ToolStripRenderer.CreateDisabledImage(stateImages.Images[index]);
      if (disabledImage != null)
        stateImages.Images.AddStrip(disabledImage);
    }
  }

  public long MasterCatalog
  {
    get => this._masterCatalog;
    set
    {
      if (this._masterCatalog == value || this.Dirty && !this.SaveFilter())
        return;
      this._masterCatalog = value;
      this.LoadMasterCatalog();
    }
  }

  public long SlaveCatalog
  {
    get => this._slaveCatalog;
    set
    {
      if (this._slaveCatalog == value)
        return;
      if (this.Dirty && !this.SaveFilter())
        throw new AbortException();
      this._slaveCatalog = value;
      this.LoadSlaveCatalog();
      this.LoadFilter();
      this.FilterListUpdate();
    }
  }

  public bool Dirty
  {
    get => !this.ReadOnly && this._filterDirty;
    set
    {
      if (this._filterDirty == value || value && this.ReadOnly)
        return;
      this._filterDirty = value;
      this.DoDirtyChanged();
    }
  }

  public string OwnerGuid
  {
    get => this._ownerGuid;
    set
    {
      if (string.Equals(this._ownerGuid, value) || this.Dirty && !this.SaveFilter())
        return;
      this._ownerGuid = value;
      this.FilterDataLoad();
      this.LoadFilter();
      this.FilterListUpdate();
    }
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      if (this._readOnly == value)
        return;
      this._readOnly = value;
      this.UpdateControlsState();
    }
  }

  public event EventHandler DirtyChanged;

  public event EventHandler MasterNodeChanged;

  private void MasterTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
  {
    e.Cancel = !this.SaveFilter();
  }

  private void MasterTree_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this._masterCatalog == 0L || this._masterCatalog == -1L || this._loading)
      return;
    this.LoadFilter();
    this.DoMasterNodeChanged(this._masterTree.SelectedNode);
  }

  private void MasterTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
  {
    if (e == null || e.Node == null)
      return;
    TreeNode node = e.Node;
    Font font1 = e.Node.TreeView.Font;
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & TreeNodeStates.Selected) != (TreeNodeStates) 0)
      brush = SystemBrushes.HighlightText;
    if (this._masterCombo.Items.Contains((object) node))
    {
      using (Font font2 = new Font(font1, FontStyle.Bold))
        e.Graphics.DrawString(e.Node.Text, font2, brush, (RectangleF) Rectangle.Inflate(e.Bounds, 2, 0));
    }
    else
      e.Graphics.DrawString(e.Node.Text, font1, brush, (RectangleF) Rectangle.Inflate(e.Bounds, 2, 0));
  }

  private void MasterCombo_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1 || !(sender is ComboBox comboBox))
      return;
    TreeNode treeNode = comboBox.Items[e.Index] as TreeNode;
    Rectangle rectangle;
    ref Rectangle local = ref rectangle;
    Rectangle bounds = e.Bounds;
    int left = bounds.Left;
    bounds = e.Bounds;
    int top = bounds.Top;
    local = new Rectangle(left, top, 16 /*0x10*/, 16 /*0x10*/);
    this.imageList.Draw(e.Graphics, rectangle.X, rectangle.Y, 0);
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = SystemBrushes.HighlightText;
    if (treeNode == null)
      return;
    e.Graphics.DrawString(treeNode.Text, comboBox.Font, brush, (float) (rectangle.Left + 18), (float) (rectangle.Top + 2));
  }

  private void MasterCombo_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!(this._masterCombo.SelectedItem is TreeNode selectedItem))
      return;
    this._masterTree.SelectedNode = selectedItem;
  }

  private void SlaveTree_AfterCheck(object sender, TreeViewEventArgs e)
  {
    if (this._loading)
      return;
    TreeNode node = e.Node;
    bool flag = this._checkedList.Items.Contains((object) node);
    if (node.Checked)
    {
      if (flag)
        return;
      this.Dirty = true;
      this._checkedList.Items.Add((object) node);
    }
    else
    {
      if (!flag)
        return;
      this.Dirty = true;
      this._checkedList.Items.Remove((object) node);
    }
  }

  private void _slaveTree_MouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if (this.ReadOnly || !(sender is TreeView treeView) || treeView.HitTest(e.X, e.Y).Location != TreeViewHitTestLocations.StateImage)
      return;
    TreeNode node = e.Node;
    FolderFilterTune.FilterNodeInfo tag = (FolderFilterTune.FilterNodeInfo) node.Tag;
    if (tag == null)
      return;
    switch (node.StateImageIndex)
    {
      case 0:
      case 2:
        this.Dirty = true;
        this._filterCurrData.Add(tag.ObjGuid);
        this._checkedList.Items.Add((object) node);
        this.UpdateSlaveNodeStateIndex(node, true);
        break;
      case 1:
        this.Dirty = true;
        this._filterCurrData.Remove(tag.ObjGuid);
        this._checkedList.Items.Remove((object) node);
        this.UpdateSlaveNodeStateIndex(node, true);
        break;
    }
  }

  private void _slaveTree_AfterExpand(object sender, TreeViewEventArgs e)
  {
    TreeNode node1 = e.Node;
    if (node1 == null || this._createSlaveTreeMode)
      return;
    foreach (TreeNode node2 in node1.Nodes)
    {
      if (node2.StateImageIndex == -1)
        this.UpdateSlaveNodeStateIndex(node2, false);
    }
  }

  private void FilterNodes_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1 || !(sender is ListBox listBox))
      return;
    TreeNode treeNode = listBox.Items[e.Index] as TreeNode;
    Rectangle rectangle;
    ref Rectangle local = ref rectangle;
    Rectangle bounds = e.Bounds;
    int x = bounds.Left + 4;
    bounds = e.Bounds;
    int top = bounds.Top;
    local = new Rectangle(x, top, 16 /*0x10*/, 16 /*0x10*/);
    this.imageList.Draw(e.Graphics, rectangle.X, rectangle.Y, 0);
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = SystemBrushes.HighlightText;
    if (treeNode == null)
      return;
    e.Graphics.DrawString(treeNode.Text, listBox.Font, brush, (float) (rectangle.Left + 20), (float) (rectangle.Top + 2));
  }

  private void FilterNodes_MeasureItem(object sender, MeasureItemEventArgs e)
  {
    this._checkedList.ItemHeight = (int) Math.Max(e.Graphics.MeasureString("Wq", this._checkedList.Font).Height + 2f, 17f);
    this._checkedList.DrawMode = DrawMode.OwnerDrawFixed;
  }

  private void FilterNodes_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    int index = this._checkedList.IndexFromPoint(e.X, e.Y);
    if (index == -1 || !(this._checkedList.Items[index] is TreeNode treeNode))
      return;
    this._slaveTree.SelectedNode = treeNode;
  }

  private void cmsFilter_Opening(object sender, CancelEventArgs e)
  {
    IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
    this.tsmiFilterClear.Enabled = !this.ReadOnly && this._filterCurrData.Count > 0;
    this.tsmiFilterCopy.Enabled = service != null && this._filterCurrData.Count > 0;
    bool flag = false;
    if (this.ReadOnly)
      flag = false;
    else if (service != null)
    {
      DataObject dataObject = service.GetDataObject() as DataObject;
      flag = dataObject != null;
      if (flag)
      {
        flag = false;
        foreach (string format in dataObject.GetFormats())
        {
          if (dataObject.GetData(format) is FolderFilterTune.FilterData4Clipboard)
          {
            flag = true;
            break;
          }
        }
      }
    }
    this.tsmiFilterPaste.Enabled = flag;
  }

  private void tsmiFilterCopy_Click(object sender, EventArgs e) => this.FilterDataCopy();

  private void tsmiFilterPaste_Click(object sender, EventArgs e) => this.FilterDataPaste();

  private void tsmiFilterClear_Click(object sender, EventArgs e) => this.FilterDataClear();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FolderFilterTune));
    this.splitContainer1 = new SplitContainer();
    this._masterTree = new TreeView();
    this.imageList = new ImageList(this.components);
    this.pnlLeftSplit = new Panel();
    this._masterCombo = new ComboBox();
    this.pnlLeftTop = new Panel();
    this.splitContainer2 = new SplitContainer();
    this._slaveTree = new TreeView();
    this.cmsFilter = new ContextMenuStrip(this.components);
    this.tsmiFilterCopy = new ToolStripMenuItem();
    this.tsmiFilterPaste = new ToolStripMenuItem();
    this.tsmiFilterSep1 = new ToolStripSeparator();
    this.tsmiFilterClear = new ToolStripMenuItem();
    this.pnlRightTop = new Panel();
    this._checkedList = new ListBox();
    this.pnlCheckListInfo = new Panel();
    this.lblCheckedLlistInfo = new Label();
    this.ilStateImages = new ImageList(this.components);
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.splitContainer2.BeginInit();
    this.splitContainer2.Panel1.SuspendLayout();
    this.splitContainer2.Panel2.SuspendLayout();
    this.splitContainer2.SuspendLayout();
    this.cmsFilter.SuspendLayout();
    this.pnlCheckListInfo.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel1, "splitContainer1.Panel1");
    this.splitContainer1.Panel1.Controls.Add((Control) this._masterTree);
    this.splitContainer1.Panel1.Controls.Add((Control) this.pnlLeftSplit);
    this.splitContainer1.Panel1.Controls.Add((Control) this._masterCombo);
    this.splitContainer1.Panel1.Controls.Add((Control) this.pnlLeftTop);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel2, "splitContainer1.Panel2");
    this.splitContainer1.Panel2.Controls.Add((Control) this.splitContainer2);
    componentResourceManager.ApplyResources((object) this._masterTree, "_masterTree");
    this._masterTree.HideSelection = false;
    this._masterTree.ImageList = this.imageList;
    this._masterTree.Name = "_masterTree";
    this._masterTree.DrawNode += new DrawTreeNodeEventHandler(this.MasterTree_DrawNode);
    this._masterTree.BeforeSelect += new TreeViewCancelEventHandler(this.MasterTree_BeforeSelect);
    this._masterTree.AfterSelect += new TreeViewEventHandler(this.MasterTree_AfterSelect);
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Magenta;
    this.imageList.Images.SetKeyName(0, "folderop.bmp");
    this.imageList.Images.SetKeyName(1, "folder.bmp");
    this.imageList.Images.SetKeyName(2, "папка_с_фильтром.bmp");
    this.imageList.Images.SetKeyName(3, "папка_с_фильтром1.bmp");
    this.imageList.Images.SetKeyName(4, "папка_наследующая_фильтр.bmp");
    this.imageList.Images.SetKeyName(5, "папка_наследующая_фильтр1.bmp");
    componentResourceManager.ApplyResources((object) this.pnlLeftSplit, "pnlLeftSplit");
    this.pnlLeftSplit.Name = "pnlLeftSplit";
    componentResourceManager.ApplyResources((object) this._masterCombo, "_masterCombo");
    this._masterCombo.DrawMode = DrawMode.OwnerDrawFixed;
    this._masterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this._masterCombo.FormattingEnabled = true;
    this._masterCombo.Name = "_masterCombo";
    this._masterCombo.Sorted = true;
    this._masterCombo.DrawItem += new DrawItemEventHandler(this.MasterCombo_DrawItem);
    this._masterCombo.SelectedIndexChanged += new EventHandler(this.MasterCombo_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.pnlLeftTop, "pnlLeftTop");
    this.pnlLeftTop.Name = "pnlLeftTop";
    componentResourceManager.ApplyResources((object) this.splitContainer2, "splitContainer2");
    this.splitContainer2.Name = "splitContainer2";
    componentResourceManager.ApplyResources((object) this.splitContainer2.Panel1, "splitContainer2.Panel1");
    this.splitContainer2.Panel1.Controls.Add((Control) this._slaveTree);
    this.splitContainer2.Panel1.Controls.Add((Control) this.pnlRightTop);
    componentResourceManager.ApplyResources((object) this.splitContainer2.Panel2, "splitContainer2.Panel2");
    this.splitContainer2.Panel2.Controls.Add((Control) this._checkedList);
    this.splitContainer2.Panel2.Controls.Add((Control) this.pnlCheckListInfo);
    componentResourceManager.ApplyResources((object) this._slaveTree, "_slaveTree");
    this._slaveTree.CheckBoxes = true;
    this._slaveTree.ContextMenuStrip = this.cmsFilter;
    this._slaveTree.HideSelection = false;
    this._slaveTree.ImageList = this.imageList;
    this._slaveTree.Name = "_slaveTree";
    this._slaveTree.AfterCheck += new TreeViewEventHandler(this.SlaveTree_AfterCheck);
    componentResourceManager.ApplyResources((object) this.cmsFilter, "cmsFilter");
    this.cmsFilter.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiFilterCopy,
      (ToolStripItem) this.tsmiFilterPaste,
      (ToolStripItem) this.tsmiFilterSep1,
      (ToolStripItem) this.tsmiFilterClear
    });
    this.cmsFilter.Name = "cmsFilter";
    this.cmsFilter.Opening += new CancelEventHandler(this.cmsFilter_Opening);
    componentResourceManager.ApplyResources((object) this.tsmiFilterCopy, "tsmiFilterCopy");
    this.tsmiFilterCopy.Name = "tsmiFilterCopy";
    this.tsmiFilterCopy.Click += new EventHandler(this.tsmiFilterCopy_Click);
    componentResourceManager.ApplyResources((object) this.tsmiFilterPaste, "tsmiFilterPaste");
    this.tsmiFilterPaste.Name = "tsmiFilterPaste";
    this.tsmiFilterPaste.Click += new EventHandler(this.tsmiFilterPaste_Click);
    componentResourceManager.ApplyResources((object) this.tsmiFilterSep1, "tsmiFilterSep1");
    this.tsmiFilterSep1.Name = "tsmiFilterSep1";
    componentResourceManager.ApplyResources((object) this.tsmiFilterClear, "tsmiFilterClear");
    this.tsmiFilterClear.Name = "tsmiFilterClear";
    this.tsmiFilterClear.Click += new EventHandler(this.tsmiFilterClear_Click);
    componentResourceManager.ApplyResources((object) this.pnlRightTop, "pnlRightTop");
    this.pnlRightTop.Name = "pnlRightTop";
    componentResourceManager.ApplyResources((object) this._checkedList, "_checkedList");
    this._checkedList.DrawMode = DrawMode.OwnerDrawVariable;
    this._checkedList.FormattingEnabled = true;
    this._checkedList.Name = "_checkedList";
    this._checkedList.Sorted = true;
    this._checkedList.DrawItem += new DrawItemEventHandler(this.FilterNodes_DrawItem);
    this._checkedList.MeasureItem += new MeasureItemEventHandler(this.FilterNodes_MeasureItem);
    this._checkedList.MouseDoubleClick += new MouseEventHandler(this.FilterNodes_MouseDoubleClick);
    componentResourceManager.ApplyResources((object) this.pnlCheckListInfo, "pnlCheckListInfo");
    this.pnlCheckListInfo.Controls.Add((Control) this.lblCheckedLlistInfo);
    this.pnlCheckListInfo.Name = "pnlCheckListInfo";
    componentResourceManager.ApplyResources((object) this.lblCheckedLlistInfo, "lblCheckedLlistInfo");
    this.lblCheckedLlistInfo.Name = "lblCheckedLlistInfo";
    this.ilStateImages.ColorDepth = ColorDepth.Depth24Bit;
    componentResourceManager.ApplyResources((object) this.ilStateImages, "ilStateImages");
    this.ilStateImages.TransparentColor = Color.Magenta;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (FolderFilterTune);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.splitContainer2.Panel1.ResumeLayout(false);
    this.splitContainer2.Panel1.PerformLayout();
    this.splitContainer2.Panel2.ResumeLayout(false);
    this.splitContainer2.EndInit();
    this.splitContainer2.ResumeLayout(false);
    this.cmsFilter.ResumeLayout(false);
    this.pnlCheckListInfo.ResumeLayout(false);
    this.pnlCheckListInfo.PerformLayout();
    this.ResumeLayout(false);
  }

  protected static class Consts
  {
    public const int cnt_Folder_Idx = 0;
    public const int cnt_FolderSelected_Idx = 1;
    public const int cnt_Folder_Filter_Idx = 2;
    public const int cnt_FolderSelected_Filter_Idx = 3;
    public const int cnt_Folder_Inherited_Idx = 4;
    public const int cnt_FolderSelected_Inherited_Idx = 5;
    public const int cnt_FilterUnchecked_Idx = 0;
    public const int cnt_FilterChecked_Idx = 1;
    public const int cnt_FilterCheckedGray_Idx = 2;
    public const int cnt_FilterUncheckedGray_Idx = 3;
    public const int cnt_FilterDisabled_Offset = 4;
  }

  public class FilterNodeInfo : NodeInfo
  {
    protected Guid _objGuid;
    protected string _nodePath;

    public FilterNodeInfo(long objectId, int typeId)
      : this(objectId, typeId, Guid.Empty, string.Empty)
    {
    }

    public FilterNodeInfo(long objectId, int typeId, Guid objGuid, string nodePath)
      : base(objectId, typeId)
    {
      this._objGuid = objGuid;
      this._nodePath = nodePath;
    }

    public Guid ObjGuid
    {
      get => this._objGuid;
      set => this._objGuid = value;
    }

    public string NodePath
    {
      get => this._nodePath;
      set => this._nodePath = value;
    }
  }

  internal class FilterData4Clipboard : List<Guid>
  {
    public FilterData4Clipboard()
    {
    }

    public FilterData4Clipboard(IEnumerable<Guid> collection)
      : base(collection)
    {
    }

    public FilterData4Clipboard(int capacity)
      : base(capacity)
    {
    }
  }
}
