// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseObjFilterTune
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Expert;
using Intermech.Imbase.Comparers;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseObjFilterTune : UserControl
{
  private ImbaseObjFilterInfo _imFilterInfo;
  private ImbaseObjFilterData _imFilterData;
  private List<ImbaseObjFilterTune.ImCatalogInfo> _imCataLogList;
  private string _ownerGuid = string.Empty;
  private bool _filterDirty;
  private bool _readOnly;
  private bool _restoreFilterSelection;
  private bool _createImTreeMode;
  private long _imCatalogID = -1;
  protected Dictionary<Guid, TreeNode> _imGuid2Node = new Dictionary<Guid, TreeNode>();
  private static string FilterObjDataSql = "F_GUID = '{0}'";
  private IContainer components;
  private ImageList ilStateImages;
  private ListBox lbFilterList;
  private Panel pnlCheckListInfo;
  private Label lblCheckedLlistInfo;
  private Panel pnlRightTop;
  private ToolStripMenuItem tsmiImFilterDataClear;
  private ToolStripSeparator tsmiImFilterDataSep1;
  private ToolStripMenuItem tsmiImFilterDataPaste;
  private ToolStripMenuItem tsmiImFilterDataCopy;
  private ContextMenuStrip cmsImFilterData;
  private TreeView tvImCatalog;
  private ImageList imageList;
  private SplitContainer splitContainer2;
  private Panel pnlLeftSplit;
  private Panel pnlLeftTop;
  private SplitContainer splitContainer1;
  private ComboBox cbImCatalogs;
  private ListView lvFilters;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;
  private ListView lvFilterItems;
  private ColumnHeader columnHeader1;
  private ContextMenuStrip cmsFilters;
  private ToolStripMenuItem tsmiFilterCopy;
  private ToolStripMenuItem tsmiFilterPaste;
  private ToolStripSeparator tsmiFilterSep1;
  private ToolStripMenuItem tsmiFilterDelete;
  private ContextMenuStrip cmsFilterItems;
  private ToolStripMenuItem tsmiFilterItemCopy;
  private ToolStripMenuItem tsmiFilterItemPaste;
  private ToolStripSeparator tsmiFilterItemSep1;
  private ToolStripMenuItem tsmiFilterItemDelete;
  private ToolStripMenuItem tsmiFilterNew;
  private ToolStripMenuItem tsmiFilterEdit;
  private ToolStripSeparator tsmiFilterSep2;
  private ToolStripMenuItem tsmiFilterItemAdd;
  private ToolStripMenuItem tsmiFilterItemEdit;
  private ToolStripSeparator tsmiFilterItemSep2;
  private ColumnHeader columnHeader4;
  private ToolStripMenuItem tsmiFilterView;
  private ToolStripSeparator tsmiFIlterSep3;
  private ToolStripMenuItem tsmiFilterRefresh;
  private ToolStripMenuItem tsmiFilterItemAllCopy;

  private bool _DesignMode
  {
    get
    {
      return this.DesignMode || this.GetService(typeof (IDesignerHost)) != null || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }
  }

  protected virtual void InitializeData()
  {
    this._imCataLogList = new List<ImbaseObjFilterTune.ImCatalogInfo>();
  }

  protected virtual void InitializeCustomComponents()
  {
    this.tvImCatalog.TreeViewNodeSorter = (IComparer) new NodeComparer();
    FolderFilterTune.InitializeStateImages(this.ilStateImages);
    this.tvImCatalog.StateImageList = this.ilStateImages;
    this.tvImCatalog.CheckBoxes = false;
    this.cbImCatalogs.Sorted = false;
    using (Graphics graphics = this.lbFilterList.CreateGraphics())
    {
      this.lbFilterList.ItemHeight = (int) Math.Max(graphics.MeasureString("Wq", this.lbFilterList.Font).Height + 2f, 17f);
      this.lbFilterList.DrawMode = DrawMode.OwnerDrawFixed;
    }
    this.UpdateControlsState();
  }

  protected virtual void UpdateControlsState()
  {
    this.UpdateImCatalogTreeStateIndex(this.tvImCatalog.Nodes);
  }

  protected virtual void UpdateImCatalogTreeStateIndex(TreeNodeCollection nodes)
  {
    foreach (TreeNode node in nodes)
    {
      this.UpdateImCatalogNodeStateIndex(node, false);
      this.UpdateImCatalogTreeStateIndex(node.Nodes);
    }
  }

  protected virtual void UpdateImCatalogNodeStateIndex(TreeNode node, bool fullUpdateMode)
  {
    if (node == null || !(node.Tag is FolderFilterTune.FilterNodeInfo tag1))
      return;
    int num = this.ReadOnly ? 4 : 0;
    DataTable selectedFilterItemTable = this.GetSelectedFilterItemTable();
    if (selectedFilterItemTable == null)
    {
      node.StateImageIndex = num;
    }
    else
    {
      int stateImageIndex1 = node.StateImageIndex;
      bool flag = false;
      string filterExpression1 = string.Format(ImbaseObjFilterTune.FilterObjDataSql, (object) tag1.ObjGuid);
      DataRow[] dataRowArray1 = selectedFilterItemTable.Select(filterExpression1);
      if (dataRowArray1 != null && dataRowArray1.Length != 0)
      {
        node.StateImageIndex = num + 1;
        flag = true;
      }
      else
      {
        TreeNode treeNode = (TreeNode) null;
        int columnIndex = selectedFilterItemTable.Columns.IndexOf("F_GUID");
        foreach (DataRow row in (InternalDataCollectionBase) selectedFilterItemTable.Rows)
        {
          if (GuidHelper.IsGuid(row[columnIndex].ToString()))
          {
            Guid key = new Guid(row[columnIndex].ToString());
            if (!(key == Guid.Empty) && this._imGuid2Node.TryGetValue(key, out treeNode) && treeNode.Tag is FolderFilterTune.FilterNodeInfo tag2 && tag2.NodePath.IndexOf(tag1.NodePath) != -1)
            {
              node.StateImageIndex = num + 2;
              flag = true;
              break;
            }
          }
        }
        if (!flag)
        {
          for (TreeNode parent = node.Parent; parent != null; parent = parent.Parent)
          {
            if (parent.Tag is FolderFilterTune.FilterNodeInfo tag3)
            {
              string filterExpression2 = string.Format(ImbaseObjFilterTune.FilterObjDataSql, (object) tag3.ObjGuid);
              DataRow[] dataRowArray2 = selectedFilterItemTable.Select(filterExpression2);
              if (dataRowArray2 != null && dataRowArray2.Length != 0)
              {
                node.StateImageIndex = num + 3;
                flag = true;
                break;
              }
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
          this.UpdateImCatalogNodeStateIndex(node.Parent, true);
          IEnumerator enumerator1 = node.Nodes.GetEnumerator();
          try
          {
            while (enumerator1.MoveNext())
              this.UpdateImCatalogNodeStateIndex((TreeNode) enumerator1.Current, true);
            break;
          }
          finally
          {
            if (enumerator1 is IDisposable disposable)
              disposable.Dispose();
          }
        case 2:
          this.UpdateImCatalogNodeStateIndex(node.Parent, true);
          break;
        case 3:
          if (stateImageIndex1 == 1)
            this.UpdateImCatalogNodeStateIndex(node.Parent, true);
          IEnumerator enumerator2 = node.Nodes.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
              this.UpdateImCatalogNodeStateIndex((TreeNode) enumerator2.Current, true);
            break;
          }
          finally
          {
            if (enumerator2 is IDisposable disposable)
              disposable.Dispose();
          }
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

  protected virtual void DoFilterChanged(TreeNode treeNode)
  {
    EventHandler filterChanged = this.FilterChanged;
    if (filterChanged == null)
      return;
    filterChanged((object) this, EventArgs.Empty);
  }

  private void LoadImCataLogList()
  {
    this._imCataLogList.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable objectData = DataHelper.GetObjectData(Intermech.Imbase.Consts.ImbaseCatalogTypeID, sessionKeeper.Session, (IEnumerable<ConditionStructure>) new ConditionStructure[0], (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.ASC, 0),
        new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1)
      }.ToArray(), (IEnumerable<long>) null);
      if (objectData == null || objectData.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
      {
        long result = 0;
        if (long.TryParse(row["F_OBJECT_ID"].ToString(), out result) && result != 0L)
          this._imCataLogList.Add(new ImbaseObjFilterTune.ImCatalogInfo(result, row["CAPTION"].ToString(), row[2].ToString()));
      }
    }
  }

  private void UpdateImCatalogList(long catalogID = 0)
  {
    ComboBox cbImCatalogs = this.cbImCatalogs;
    if (cbImCatalogs == null)
      return;
    cbImCatalogs.BeginUpdate();
    try
    {
      long num1 = catalogID;
      ImbaseObjFilterTune.ImCatalogInfo imCatalogInfo1 = num1 == 0L ? cbImCatalogs.SelectedItem as ImbaseObjFilterTune.ImCatalogInfo : (ImbaseObjFilterTune.ImCatalogInfo) null;
      List<string> list = new List<string>();
      DataTable selectedFilterItemTable = this.GetSelectedFilterItemTable();
      if (selectedFilterItemTable != null)
      {
        int columnIndex = selectedFilterItemTable.Columns.IndexOf("F_PATH");
        foreach (DataRow row in (InternalDataCollectionBase) selectedFilterItemTable.Rows)
        {
          if (row != null)
          {
            string str = row[columnIndex].ToString();
            if (str != null && str.Length >= 2)
              list.Add(str.Substring(0, 2));
          }
        }
        GenericListHelper.MakeUnique<string>(list);
      }
      cbImCatalogs.Items.Clear();
      List<ImbaseObjFilterTune.ImCatalogInfo> imCatalogInfoList = new List<ImbaseObjFilterTune.ImCatalogInfo>(this._imCataLogList.Count);
      foreach (ImbaseObjFilterTune.ImCatalogInfo imCataLog in this._imCataLogList)
      {
        if (imCataLog != null)
        {
          ImbaseObjFilterTune.ImCatalogInfo imCatalogInfo2 = imCataLog.Clone() as ImbaseObjFilterTune.ImCatalogInfo;
          imCatalogInfo2.HasFilterData = list.BinarySearch(imCatalogInfo2.ObjPath) >= 0;
          if (imCatalogInfo2.HasFilterData)
            cbImCatalogs.Items.Add((object) imCatalogInfo2);
          else
            imCatalogInfoList.Add(imCatalogInfo2);
          if (imCatalogInfo1 == null && num1 == imCatalogInfo2.ObjID)
            imCatalogInfo1 = imCatalogInfo2;
        }
      }
      cbImCatalogs.Items.AddRange((object[]) imCatalogInfoList.ToArray());
      int num2 = imCatalogInfo1 != null ? cbImCatalogs.Items.IndexOf((object) imCatalogInfo1) : -1;
      if (cbImCatalogs.SelectedIndex == num2)
        return;
      cbImCatalogs.SelectedIndex = num2;
    }
    finally
    {
      cbImCatalogs.EndUpdate();
    }
  }

  protected DataTable GetCatalogTable(long catalogId, bool checkBlobs)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      return session.GetCustomService(typeof (IObjectFilterService)) is IObjectFilterService customService ? customService.LoadCatalogTable(session.SessionGUID, catalogId) : (DataTable) null;
    }
  }

  protected void LoadImCatalog()
  {
    DataTable dt = (DataTable) null;
    if (this._imCatalogID != 0L && this._imCatalogID != -1L)
      dt = this.GetCatalogTable(this._imCatalogID, false);
    this._createImTreeMode = true;
    try
    {
      this.BuildTree(this.tvImCatalog, this._imGuid2Node, dt);
      this.UpdateImCatalogTreeStateIndex(this.tvImCatalog.Nodes);
    }
    finally
    {
      this._createImTreeMode = false;
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
      IntPtr handle = treeView.Handle;
      foreach (TreeNode node in treeView.Nodes)
        node.Collapse(false);
    }
    finally
    {
      treeView.Scrollable = scrollable;
      treeView.EndUpdate();
    }
  }

  private int GetIconIndex(int objectType) => -1;

  protected void UncheckAllNodes(TreeNodeCollection nodes)
  {
  }

  private void LoadFilters()
  {
    try
    {
      List<ImbaseObjFilterInfo> imbaseObjFilterInfoList = (List<ImbaseObjFilterInfo>) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IObjectFilterService)) is IObjectFilterService customService))
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_152"), (object) typeof (IObjectFilterService)));
        imbaseObjFilterInfoList = customService.GetFilterList(sessionKeeper.Session.SessionGUID, -2);
        if (imbaseObjFilterInfoList != null)
        {
          for (int index = imbaseObjFilterInfoList.Count - 1; index >= 0; --index)
          {
            ImbaseObjFilterInfo imbaseObjFilterInfo = imbaseObjFilterInfoList[index];
            if (imbaseObjFilterInfo == null || imbaseObjFilterInfo.Owner != this.OwnerGuid)
              imbaseObjFilterInfoList.RemoveAt(index);
          }
        }
      }
      ImbaseObjFilterInfo selectedFilterInfo = this.GetSelectedFilterInfo();
      this.lvFilters.Items.Clear();
      if (imbaseObjFilterInfoList == null || imbaseObjFilterInfoList.Count == 0)
        return;
      foreach (ImbaseObjFilterInfo imbaseObjFilterInfo in imbaseObjFilterInfoList)
      {
        if (imbaseObjFilterInfo != null)
        {
          long objectId = imbaseObjFilterInfo.ObjectID;
          string caption = imbaseObjFilterInfo.Caption;
          string objectTypeName = MetaDataHelper.GetObjectTypeName(imbaseObjFilterInfo.RefObjTypeID);
          ListViewItem listViewItem = this.lvFilters.Items.Add(objectId.ToString());
          listViewItem.SubItems.Add(caption);
          listViewItem.SubItems.Add(objectTypeName);
          listViewItem.Tag = (object) imbaseObjFilterInfo;
          if (selectedFilterInfo != null && selectedFilterInfo.ObjectID == objectId)
            listViewItem.Selected = true;
        }
      }
    }
    finally
    {
      this.FilterUpdate();
    }
  }

  private void LoadFilterData()
  {
    this.Dirty = false;
    this._imFilterData = (ImbaseObjFilterData) null;
    ImbaseObjFilterInfo selectedFilterInfo = this.GetSelectedFilterInfo();
    if (selectedFilterInfo == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IObjectFilterService)) is IObjectFilterService customService))
        return;
      customService.GetFilterData(sessionKeeper.Session.SessionGUID, selectedFilterInfo.ObjectID, out this._imFilterData);
    }
  }

  private void SaveFilterData()
  {
    ImbaseObjFilterInfo imFilterInfo = this._imFilterInfo;
    if (imFilterInfo == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IObjectFilterService)) is IObjectFilterService customService))
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_152"), (object) typeof (IObjectFilterService)));
      if (!customService.SetFilterData(sessionKeeper.Session.SessionGUID, imFilterInfo.ObjectID, this._imFilterData))
        return;
      this.Dirty = false;
    }
  }

  private void FilterCreate()
  {
    if (!this.FilterCheckModifyMode())
      return;
    long num = ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service ? service.CreateObjectByTypeDialog(Intermech.Imbase.Consts.ImbaseObjFilterTypeGuid) : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_152"), (object) typeof (IObjectCreatorService)));
    switch (num)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        if (this.OwnerGuid != string.Empty)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(num, false);
            if (dbObject == null)
              break;
            dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseFilterOwnerAttrID, false, new object[1]
            {
              (object) this.OwnerGuid
            });
          }
        }
        this.LoadFilters();
        this.SetSelectedFilter(num);
        break;
    }
  }

  private void FilterEdit()
  {
    ImbaseObjFilterInfo selectedFilterInfo = this.GetSelectedFilterInfo();
    if (selectedFilterInfo == null || !this.FilterCheckModifyMode() || !this.FilterShowParameterCard(selectedFilterInfo))
      return;
    this.LoadFilters();
  }

  private void FilterProperty() => this.FilterShowParameterCard(this.GetSelectedFilterInfo(), true);

  private bool FilterShowParameterCard(ImbaseObjFilterInfo filterInfo, bool readOnly = false)
  {
    if (filterInfo == null)
      return false;
    ISelectedItems items = ObjectExtensions.GetItems(filterInfo.ObjectID);
    ServiceContainer viewServices1 = new ServiceContainer();
    if (readOnly)
      viewServices1.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.ReadOnly));
    ServiceContainer viewServices2 = viewServices1;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2);
    if (commandsTable == null)
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("ParametersCard", commandsTable, (System.IServiceProvider) viewServices1);
    return true;
  }

  private void FilterCopy()
  {
    ImbaseObjFilterInfo selectedFilterInfo = this.GetSelectedFilterInfo();
    if (selectedFilterInfo == null || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    service.SetDataObject((object) new DataObject((object) selectedFilterInfo));
  }

  private void FilterPaste()
  {
    if (this.ReadOnly || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    DataObject dataObject = service.GetDataObject() as DataObject;
    if (!dataObject.GetDataPresent(typeof (ImbaseObjFilterInfo)) || !(dataObject.GetData(typeof (ImbaseObjFilterInfo)) is ImbaseObjFilterInfo data))
      return;
    long filterID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseObjFilterTypeGuid).Create(data.ObjectID);
      if (dbObject == null)
        return;
      dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseFilterOwnerAttrID, false, new object[1]
      {
        (object) this.OwnerGuid
      });
      if (dbObject.IsCreationMode)
        dbObject.CommitCreation(true);
      filterID = dbObject.ObjectID;
    }
    if (filterID == 0L || filterID == -1L)
      return;
    this.LoadFilters();
    this.SetSelectedFilter(filterID);
  }

  private void FilterDelete()
  {
    ImbaseObjFilterInfo selectedFilterInfo = this.GetSelectedFilterInfo();
    if (selectedFilterInfo == null || this.ReadOnly)
      return;
    ObjectCommands.DeleteCommand(ObjectExtensions.GetItems(selectedFilterInfo.ObjectID), (System.IServiceProvider) new ServiceContainer(), (object) null);
    this.Dirty = false;
    this.LoadFilters();
  }

  private void FilterUpdate()
  {
    this._imFilterInfo = this.GetSelectedFilterInfo();
    this.LoadFilterData();
    this.FilterItemsUpdate();
  }

  private bool FilterCheckModifyMode()
  {
    if (this.ReadOnly)
      return false;
    if (this._imFilterInfo == null || this._imFilterData == null || !this.Dirty)
      return true;
    switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase_FilterSettings_DataChanged"), (object) this._imFilterInfo.Caption), LocalizationHolder.rm.GetString("Imbase.Client_1133"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))
    {
      case DialogResult.Cancel:
        return false;
      case DialogResult.Yes:
        this.SaveFilterData();
        return true;
      case DialogResult.No:
        return true;
      default:
        return false;
    }
  }

  private ImbaseObjFilterInfo GetSelectedFilterInfo()
  {
    return this.lvFilters.SelectedItems.Count <= 0 ? (ImbaseObjFilterInfo) null : (ImbaseObjFilterInfo) this.lvFilters.SelectedItems[0].Tag;
  }

  private void SetSelectedFilter(long filterID)
  {
    if (filterID == 0L || filterID == -1L)
      return;
    foreach (ListViewItem listViewItem in this.lvFilters.Items)
    {
      if (listViewItem != null && listViewItem.Tag is ImbaseObjFilterInfo tag && tag.ObjectID == filterID)
      {
        listViewItem.Selected = true;
        break;
      }
    }
  }

  private ImbaseObjFilterItem GetSelectedFilterItem()
  {
    return this.lvFilterItems.SelectedItems.Count <= 0 ? (ImbaseObjFilterItem) null : this.lvFilterItems.SelectedItems[0].Tag as ImbaseObjFilterItem;
  }

  private DataTable GetSelectedFilterItemTable() => this.GetSelectedFilterItem()?.FilterData;

  private void FilterItemsUpdate()
  {
    this.lvFilterItems.BeginUpdate();
    try
    {
      this.lvFilterItems.Items.Clear();
      if (this._imFilterData == null)
        return;
      foreach (ImbaseObjFilterItem imbaseObjFilterItem in (List<ImbaseObjFilterItem>) this._imFilterData.Items)
      {
        if (imbaseObjFilterItem != null)
          this.lvFilterItems.Items.Add(imbaseObjFilterItem.Condition != null ? imbaseObjFilterItem.Condition.TrueText() : string.Empty).Tag = (object) imbaseObjFilterItem;
      }
    }
    finally
    {
      this.lvFilterItems.EndUpdate();
      if (this.lvFilterItems.Items.Count > 0)
        this.lvFilterItems.Items[0].Selected = true;
      else
        this.FilterItemUpdate();
    }
  }

  private void FilterItemCreate()
  {
    if (this.ReadOnly || this._imFilterData == null)
      return;
    ImbaseObjFilterItem filterItem = new ImbaseObjFilterItem(new TempFormula(), ImbaseObjFilterItem.CreateFilterTable("filter_data"));
    if (!this.FilterItemEdit(filterItem))
      return;
    filterItem.Order = (long) this._imFilterData.Items.Count;
    this._imFilterData.Items.Add(filterItem);
    this.Dirty = true;
    this.FilterItemsUpdate();
  }

  private void FilterItemEdit()
  {
    if (this.ReadOnly || this._imFilterData == null)
      return;
    ImbaseObjFilterItem selectedFilterItem = this.GetSelectedFilterItem();
    if (selectedFilterItem == null || !this.FilterItemEdit(selectedFilterItem))
      return;
    this.Dirty = true;
    this.FilterItemsUpdate();
  }

  private bool FilterItemEdit(ImbaseObjFilterItem filterItem)
  {
    if (filterItem == null || this.ReadOnly)
      return false;
    if (filterItem.Condition == null)
      filterItem.Condition = new TempFormula();
    IExpertEditor service = ServiceUtils.GetService<IExpertEditor>((object) ServicesManager.ServiceContainer, true);
    object obj = filterItem.Condition.Clone();
    string str = LocalizationHolder.rm.GetString(sc_7906.ssp_expert_7907());
    ref object local = ref obj;
    string title = str;
    if (!service.EditCondition(ref local, title))
      return false;
    filterItem.Condition = obj as TempFormula;
    return true;
  }

  private void FilterItemCopy()
  {
    ImbaseObjFilterItem selectedFilterItem = this.GetSelectedFilterItem();
    if (selectedFilterItem == null || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    ImbaseObjFilterItem data = selectedFilterItem.Clone() as ImbaseObjFilterItem;
    data.FilterData.Clear();
    service.SetDataObject((object) new DataObject((object) data));
  }

  private void FilterItemAllCopy()
  {
    ImbaseObjFilterItem selectedFilterItem = this.GetSelectedFilterItem();
    if (selectedFilterItem == null || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    service.SetDataObject((object) new DataObject(selectedFilterItem.Clone()));
  }

  private void FilterItemPaste()
  {
    if (this.ReadOnly || this._imFilterData == null || (!((ServicesManager.GetService(typeof (IClipboard)) as IClipboard).GetDataObject() is DataObject dataObject) ? 0 : (dataObject.GetDataPresent(typeof (ImbaseObjFilterItem)) ? 1 : 0)) == 0 || !(dataObject.GetData(typeof (ImbaseObjFilterItem)) is ImbaseObjFilterItem data))
      return;
    ImbaseObjFilterItem imbaseObjFilterItem = data.Clone() as ImbaseObjFilterItem;
    imbaseObjFilterItem.Order = (long) this._imFilterData.Items.Count;
    this._imFilterData.Items.Add(imbaseObjFilterItem);
    this.Dirty = true;
    this.FilterItemsUpdate();
  }

  private void FilterItemDelete()
  {
    if (this.ReadOnly || this._imFilterData == null)
      return;
    ImbaseObjFilterItem selectedFilterItem = this.GetSelectedFilterItem();
    if (selectedFilterItem == null || MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_FilterSettings_DeleteCurrentRecord"), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    int index = this._imFilterData.Items.IndexOf(selectedFilterItem);
    if (index == -1)
      return;
    this._imFilterData.Items.RemoveAt(index);
    foreach (ImbaseObjFilterItem imbaseObjFilterItem in (List<ImbaseObjFilterItem>) this._imFilterData.Items)
    {
      if (imbaseObjFilterItem != null && imbaseObjFilterItem.Order >= (long) index)
        --imbaseObjFilterItem.Order;
    }
    this.Dirty = true;
    this.FilterItemsUpdate();
  }

  private void FilterItemUpdate()
  {
    this.UpdateImCatalogList();
    this.ImFilterDataUpdate();
  }

  private void ImFilterDataUpdate()
  {
    try
    {
      this.UncheckAllNodes(this.tvImCatalog.Nodes);
      this.lbFilterList.Items.Clear();
      DataTable selectedFilterItemTable = this.GetSelectedFilterItemTable();
      if (selectedFilterItemTable == null)
        return;
      List<string> stringList = new List<string>(selectedFilterItemTable.Rows.Count);
      int columnIndex = selectedFilterItemTable.Columns.IndexOf("F_GUID");
      foreach (DataRow row in (InternalDataCollectionBase) selectedFilterItemTable.Rows)
      {
        if (row != null)
        {
          string text = row[columnIndex].ToString();
          if (text != null && !(text == string.Empty) && GuidHelper.IsGuid(text) && !stringList.Contains(text))
            stringList.Add(text);
        }
      }
      foreach (TreeNode treeNode in this._imGuid2Node.Values)
      {
        string str = ((FolderFilterTune.FilterNodeInfo) treeNode.Tag).ObjGuid.ToString();
        if (stringList.Contains(str))
          this.lbFilterList.Items.Add((object) treeNode);
      }
    }
    finally
    {
      this.UpdateImCatalogTreeStateIndex(this.tvImCatalog.Nodes);
    }
  }

  private void ImFilterDataCopy()
  {
    DataTable selectedFilterItemTable = this.GetSelectedFilterItemTable();
    if (selectedFilterItemTable == null || selectedFilterItemTable.Rows.Count == 0 || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    ImbaseObjFilterTune.FilterData4Clipboard data = new ImbaseObjFilterTune.FilterData4Clipboard(this.lbFilterList.Items.Count);
    foreach (object obj in this.lbFilterList.Items)
    {
      if (obj != null)
      {
        FolderFilterTune.FilterNodeInfo tag = obj is TreeNode treeNode ? treeNode.Tag as FolderFilterTune.FilterNodeInfo : (FolderFilterTune.FilterNodeInfo) null;
        if (tag != null)
          data.Add(tag);
      }
    }
    if (data.Count == 0)
      return;
    service.SetDataObject((object) new DataObject((object) data));
  }

  private void ImFilterDataPaste()
  {
    if (this.ReadOnly)
      return;
    DataTable selectedFilterItemTable = this.GetSelectedFilterItemTable();
    if (selectedFilterItemTable == null || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    DataObject dataObject = service.GetDataObject() as DataObject;
    if (!dataObject.GetDataPresent(typeof (ImbaseObjFilterTune.FilterData4Clipboard)) || !(dataObject.GetData(typeof (ImbaseObjFilterTune.FilterData4Clipboard)) is ImbaseObjFilterTune.FilterData4Clipboard data) || data.Count == 0)
      return;
    List<Guid> list = new List<Guid>(this.lbFilterList.Items.Count);
    foreach (object obj in this.lbFilterList.Items)
    {
      if (obj != null)
      {
        FolderFilterTune.FilterNodeInfo tag = obj is TreeNode treeNode ? treeNode.Tag as FolderFilterTune.FilterNodeInfo : (FolderFilterTune.FilterNodeInfo) null;
        if (tag != null)
          list.Add(tag.ObjGuid);
      }
    }
    GenericListHelper.MakeUnique<Guid>(list);
    bool flag = false;
    foreach (FolderFilterTune.FilterNodeInfo filterNodeInfo in (List<FolderFilterTune.FilterNodeInfo>) data)
    {
      if (filterNodeInfo != null && list.BinarySearch(filterNodeInfo.ObjGuid) < 0)
      {
        selectedFilterItemTable.Rows.Add((object) filterNodeInfo.ObjGuid, (object) string.Empty, (object) filterNodeInfo.NodePath);
        flag = true;
      }
    }
    if (!flag)
      return;
    this.Dirty = true;
    this.FilterItemUpdate();
  }

  private void ImFilterDataClear()
  {
    if (this.ReadOnly)
      return;
    DataTable selectedFilterItemTable = this.GetSelectedFilterItemTable();
    if (selectedFilterItemTable == null || selectedFilterItemTable.Rows.Count == 0)
      return;
    List<string> list = new List<string>(this.lbFilterList.Items.Count);
    foreach (object obj in this.lbFilterList.Items)
    {
      if (obj != null)
      {
        FolderFilterTune.FilterNodeInfo tag = obj is TreeNode treeNode ? treeNode.Tag as FolderFilterTune.FilterNodeInfo : (FolderFilterTune.FilterNodeInfo) null;
        if (tag != null)
          list.Add(tag.ObjGuid.ToString());
      }
    }
    if (list.Count == 0)
      return;
    GenericListHelper.MakeUnique<string>(list);
    int columnIndex = selectedFilterItemTable.Columns.IndexOf("F_GUID");
    for (int index = selectedFilterItemTable.Rows.Count - 1; index >= 0; --index)
    {
      DataRow row = selectedFilterItemTable.Rows[index];
      if (row != null && list.BinarySearch(row[columnIndex].ToString()) >= 0)
        row.Delete();
    }
    this.Dirty = true;
    this.FilterItemUpdate();
  }

  public ImbaseObjFilterTune()
  {
    this.InitializeComponent();
    if (this._DesignMode)
      return;
    this.InitializeData();
    this.InitializeCustomComponents();
  }

  private static DataTable CreateFilterTable()
  {
    return ImbaseObjFilterTune.CreateFilterTable("filter_data");
  }

  private static DataTable CreateFilterTable(string tableName)
  {
    return new DataTable(tableName)
    {
      Columns = {
        {
          "F_GUID",
          typeof (string)
        },
        {
          "F_OWNER",
          typeof (string)
        }
      },
      RemotingFormat = SerializationFormat.Binary
    };
  }

  public long ImCatalogID
  {
    get => this._imCatalogID;
    set
    {
      if (this._imCatalogID == value)
        return;
      this._imCatalogID = value;
      this.LoadImCatalog();
      this.UpdateImCatalogList(this._imCatalogID);
      this.FilterItemUpdate();
    }
  }

  public string OwnerGuid
  {
    [DebuggerStepThrough] get => this._ownerGuid;
    set
    {
      if (string.Equals(this._ownerGuid, value) || this.Dirty && !this.FilterCheckModifyMode())
        return;
      this._ownerGuid = value != null ? value : string.Empty;
      this.LoadFilters();
    }
  }

  public bool ReadOnly
  {
    [DebuggerStepThrough] get => this._readOnly;
    set
    {
      if (this._readOnly == value)
        return;
      this._readOnly = value;
      this.UpdateControlsState();
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

  public event EventHandler FilterChanged;

  public event EventHandler DirtyChanged;

  private void lvFilters_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._restoreFilterSelection)
      return;
    this._restoreFilterSelection = true;
    try
    {
      if (this._imFilterInfo != null)
      {
        if (this.Dirty)
        {
          if (!this.FilterCheckModifyMode())
          {
            this.SetSelectedFilter(this._imFilterInfo.ObjectID);
            return;
          }
        }
      }
    }
    finally
    {
      this._restoreFilterSelection = false;
    }
    this.FilterUpdate();
  }

  private void lvFilterItems_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.FilterItemUpdate();
  }

  private void lvFilterItems_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.FilterItemEdit();
  }

  private void cbImCatalogs_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.ImCatalogID = this.cbImCatalogs.SelectedItem is ImbaseObjFilterTune.ImCatalogInfo selectedItem ? selectedItem.ObjID : 0L;
  }

  private void cbImCatalogs_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (e == null)
      return;
    e.DrawBackground();
    if (e.Index == -1 || !(sender is ComboBox comboBox) || !(comboBox.Items[e.Index] is ImbaseObjFilterTune.ImCatalogInfo imCatalogInfo))
      return;
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
    if (imCatalogInfo.HasFilterData)
    {
      using (Font font = new Font(comboBox.Font, FontStyle.Bold))
        e.Graphics.DrawString(imCatalogInfo.ObjCaption, font, brush, (float) (rectangle.Left + 20), (float) (rectangle.Top + 2));
    }
    else
      e.Graphics.DrawString(imCatalogInfo.ObjCaption, comboBox.Font, brush, (float) (rectangle.Left + 20), (float) (rectangle.Top + 2));
  }

  private void cbImCatalogs_MeasureItem(object sender, MeasureItemEventArgs e)
  {
    this.cbImCatalogs.ItemHeight = (int) Math.Max(e.Graphics.MeasureString("Wq", this.cbImCatalogs.Font).Height + 2f, 17f);
    this.cbImCatalogs.DrawMode = DrawMode.OwnerDrawFixed;
  }

  private void tvImCatalog_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if (this.ReadOnly)
      return;
    DataTable selectedFilterItemTable = this.GetSelectedFilterItemTable();
    if (selectedFilterItemTable == null || (sender as TreeView).HitTest(e.X, e.Y).Location != TreeViewHitTestLocations.StateImage)
      return;
    TreeNode node = e.Node;
    FolderFilterTune.FilterNodeInfo tag = (FolderFilterTune.FilterNodeInfo) node.Tag;
    if (tag == null)
      return;
    string filterExpression = string.Format(ImbaseObjFilterTune.FilterObjDataSql, (object) tag.ObjGuid);
    DataRow[] dataRowArray = selectedFilterItemTable.Select(filterExpression);
    switch (node.StateImageIndex)
    {
      case 0:
      case 2:
        if (dataRowArray == null || dataRowArray.Length == 0)
        {
          this.Dirty = true;
          selectedFilterItemTable.Rows.Add((object) tag.ObjGuid, (object) string.Empty, (object) tag.NodePath);
        }
        this.lbFilterList.Items.Add((object) node);
        break;
      case 1:
        if (dataRowArray != null)
        {
          this.Dirty = true;
          foreach (DataRow dataRow in dataRowArray)
            dataRow.Delete();
        }
        this.lbFilterList.Items.Remove((object) node);
        break;
      case 3:
        return;
    }
    this.UpdateImCatalogNodeStateIndex(node, true);
    this.UpdateImCatalogList();
  }

  private void tvImCatalog_AfterExpand(object sender, TreeViewEventArgs e)
  {
    TreeNode node1 = e.Node;
    if (node1 == null || this._createImTreeMode)
      return;
    foreach (TreeNode node2 in node1.Nodes)
    {
      if (node2.StateImageIndex == -1)
        this.UpdateImCatalogNodeStateIndex(node2, false);
    }
  }

  private void tvImCatalog_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    TreeNode nodeAt = this.tvImCatalog.GetNodeAt(e.X, e.Y);
    if (nodeAt == null)
      return;
    this.tvImCatalog.SelectedNode = nodeAt;
  }

  private void cmsFilters_Opening(object sender, CancelEventArgs e)
  {
    ImbaseObjFilterInfo selectedFilterInfo = this.GetSelectedFilterInfo();
    this.tsmiFilterNew.Enabled = !this.ReadOnly;
    this.tsmiFilterEdit.Enabled = this.tsmiFilterDelete.Enabled = selectedFilterInfo != null && !this.ReadOnly;
    this.tsmiFilterView.Enabled = selectedFilterInfo != null;
    IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
    this.tsmiFilterCopy.Enabled = service != null && selectedFilterInfo != null;
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
          if (dataObject.GetData(format) is ImbaseObjFilterInfo)
          {
            flag = true;
            break;
          }
        }
      }
    }
    this.tsmiFilterPaste.Enabled = flag;
  }

  private void tsmiFilterNew_Click(object sender, EventArgs e) => this.FilterCreate();

  private void tsmiFilterEdit_Click(object sender, EventArgs e) => this.FilterEdit();

  private void tsmiFilterCopy_Click(object sender, EventArgs e) => this.FilterCopy();

  private void tsmiFilterPaste_Click(object sender, EventArgs e) => this.FilterPaste();

  private void tsmiFilterDelete_Click(object sender, EventArgs e) => this.FilterDelete();

  private void tsmiFilterView_Click(object sender, EventArgs e) => this.FilterProperty();

  private void tsmiFilterRefresh_Click(object sender, EventArgs e) => this.LoadData();

  private void cmsFilterItems_Opening(object sender, CancelEventArgs e)
  {
    this.tsmiFilterItemAdd.Enabled = this.GetSelectedFilterInfo() != null && !this.ReadOnly;
    ImbaseObjFilterItem selectedFilterItem = this.GetSelectedFilterItem();
    this.tsmiFilterItemDelete.Enabled = this.tsmiFilterItemEdit.Enabled = selectedFilterItem != null && !this.ReadOnly;
    IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
    this.tsmiFilterItemCopy.Enabled = this.tsmiFilterItemAllCopy.Enabled = service != null && selectedFilterItem != null;
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
          if (dataObject.GetData(format) is ImbaseObjFilterItem)
          {
            flag = true;
            break;
          }
        }
      }
    }
    this.tsmiFilterItemPaste.Enabled = flag;
  }

  private void tsmiFilterItemAdd_Click(object sender, EventArgs e) => this.FilterItemCreate();

  private void tsmiFilterItemEdit_Click(object sender, EventArgs e) => this.FilterItemEdit();

  private void tsmiFilterItemCopy_Click(object sender, EventArgs e) => this.FilterItemCopy();

  private void tsmiFilterItemAllCopy_Click(object sender, EventArgs e) => this.FilterItemAllCopy();

  private void tsmiFilterItemPaste_Click(object sender, EventArgs e) => this.FilterItemPaste();

  private void tsmiFilterItemDelete_Click(object sender, EventArgs e) => this.FilterItemDelete();

  private void cmsImFilterData_Opening(object sender, CancelEventArgs e)
  {
    ImbaseObjFilterItem selectedFilterItem = this.GetSelectedFilterItem();
    IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
    this.tsmiImFilterDataCopy.Enabled = selectedFilterItem != null && service != null;
    this.tsmiImFilterDataClear.Enabled = selectedFilterItem != null && !this.ReadOnly;
    bool flag = false;
    if (selectedFilterItem == null || this.ReadOnly)
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
          if (dataObject.GetData(format) is ImbaseObjFilterItem)
          {
            flag = true;
            break;
          }
        }
      }
    }
    this.tsmiImFilterDataPaste.Enabled = flag;
  }

  private void tsmiImFilterDataCopy_Click(object sender, EventArgs e) => this.ImFilterDataCopy();

  private void tsmiImFilterDataPaste_Click(object sender, EventArgs e) => this.ImFilterDataPaste();

  private void tsmiImFilterDataClear_Click(object sender, EventArgs e) => this.ImFilterDataClear();

  private void lbFilterList_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1 || !(sender is ListBox listBox) || !(listBox.Items[e.Index] is TreeNode treeNode))
      return;
    Rectangle rectangle = new Rectangle(e.Bounds.Left + 4, e.Bounds.Top, 16 /*0x10*/, 16 /*0x10*/);
    this.imageList.Draw(e.Graphics, rectangle.X, rectangle.Y, 0);
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = SystemBrushes.HighlightText;
    e.Graphics.DrawString(treeNode.Text, listBox.Font, brush, (float) (rectangle.Left + 20), (float) (rectangle.Top + 2));
  }

  private void lbFilterList_MeasureItem(object sender, MeasureItemEventArgs e)
  {
    if (e == null)
      return;
    this.lbFilterList.ItemHeight = (int) Math.Max(e.Graphics.MeasureString("Wq", this.lbFilterList.Font).Height + 2f, 17f);
  }

  private void lbFilterList_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    int index = this.lbFilterList.IndexFromPoint(e.X, e.Y);
    if (index == -1 || !(this.lbFilterList.Items[index] is TreeNode treeNode))
      return;
    this.tvImCatalog.SelectedNode = treeNode;
  }

  public virtual void LoadData()
  {
    this.LoadImCataLogList();
    this.LoadFilters();
  }

  public virtual void SaveData() => this.SaveFilterData();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseObjFilterTune));
    this.ilStateImages = new ImageList(this.components);
    this.lbFilterList = new ListBox();
    this.pnlCheckListInfo = new Panel();
    this.lblCheckedLlistInfo = new Label();
    this.pnlRightTop = new Panel();
    this.tsmiImFilterDataClear = new ToolStripMenuItem();
    this.tsmiImFilterDataSep1 = new ToolStripSeparator();
    this.tsmiImFilterDataPaste = new ToolStripMenuItem();
    this.tsmiImFilterDataCopy = new ToolStripMenuItem();
    this.cmsImFilterData = new ContextMenuStrip(this.components);
    this.tvImCatalog = new TreeView();
    this.imageList = new ImageList(this.components);
    this.splitContainer2 = new SplitContainer();
    this.cbImCatalogs = new ComboBox();
    this.pnlLeftSplit = new Panel();
    this.lvFilters = new ListView();
    this.columnHeader4 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.cmsFilters = new ContextMenuStrip(this.components);
    this.tsmiFilterNew = new ToolStripMenuItem();
    this.tsmiFilterEdit = new ToolStripMenuItem();
    this.tsmiFilterSep1 = new ToolStripSeparator();
    this.tsmiFilterCopy = new ToolStripMenuItem();
    this.tsmiFilterPaste = new ToolStripMenuItem();
    this.tsmiFilterSep2 = new ToolStripSeparator();
    this.tsmiFilterDelete = new ToolStripMenuItem();
    this.tsmiFIlterSep3 = new ToolStripSeparator();
    this.tsmiFilterView = new ToolStripMenuItem();
    this.tsmiFilterRefresh = new ToolStripMenuItem();
    this.pnlLeftTop = new Panel();
    this.splitContainer1 = new SplitContainer();
    this.lvFilterItems = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.cmsFilterItems = new ContextMenuStrip(this.components);
    this.tsmiFilterItemAdd = new ToolStripMenuItem();
    this.tsmiFilterItemEdit = new ToolStripMenuItem();
    this.tsmiFilterItemSep2 = new ToolStripSeparator();
    this.tsmiFilterItemCopy = new ToolStripMenuItem();
    this.tsmiFilterItemPaste = new ToolStripMenuItem();
    this.tsmiFilterItemSep1 = new ToolStripSeparator();
    this.tsmiFilterItemDelete = new ToolStripMenuItem();
    this.tsmiFilterItemAllCopy = new ToolStripMenuItem();
    this.pnlCheckListInfo.SuspendLayout();
    this.cmsImFilterData.SuspendLayout();
    this.splitContainer2.BeginInit();
    this.splitContainer2.Panel1.SuspendLayout();
    this.splitContainer2.Panel2.SuspendLayout();
    this.splitContainer2.SuspendLayout();
    this.pnlLeftSplit.SuspendLayout();
    this.cmsFilters.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.cmsFilterItems.SuspendLayout();
    this.SuspendLayout();
    this.ilStateImages.ColorDepth = ColorDepth.Depth24Bit;
    this.ilStateImages.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.ilStateImages.TransparentColor = Color.Magenta;
    this.lbFilterList.Dock = DockStyle.Fill;
    this.lbFilterList.DrawMode = DrawMode.OwnerDrawVariable;
    this.lbFilterList.FormattingEnabled = true;
    this.lbFilterList.IntegralHeight = false;
    this.lbFilterList.Location = new Point(0, 30);
    this.lbFilterList.Name = "lbFilterList";
    this.lbFilterList.Size = new Size(378, 129);
    this.lbFilterList.Sorted = true;
    this.lbFilterList.TabIndex = 0;
    this.lbFilterList.DrawItem += new DrawItemEventHandler(this.lbFilterList_DrawItem);
    this.lbFilterList.MeasureItem += new MeasureItemEventHandler(this.lbFilterList_MeasureItem);
    this.lbFilterList.MouseDoubleClick += new MouseEventHandler(this.lbFilterList_MouseDoubleClick);
    this.pnlCheckListInfo.Controls.Add((Control) this.lblCheckedLlistInfo);
    this.pnlCheckListInfo.Dock = DockStyle.Top;
    this.pnlCheckListInfo.Location = new Point(0, 0);
    this.pnlCheckListInfo.Name = "pnlCheckListInfo";
    this.pnlCheckListInfo.Size = new Size(378, 30);
    this.pnlCheckListInfo.TabIndex = 1;
    this.lblCheckedLlistInfo.AutoSize = true;
    this.lblCheckedLlistInfo.ImeMode = ImeMode.NoControl;
    this.lblCheckedLlistInfo.Location = new Point(-3, 10);
    this.lblCheckedLlistInfo.Name = "lblCheckedLlistInfo";
    this.lblCheckedLlistInfo.Size = new Size(172, 13);
    this.lblCheckedLlistInfo.TabIndex = 0;
    this.lblCheckedLlistInfo.Text = "Выбранные элементы фильтра :";
    this.pnlRightTop.AutoSize = true;
    this.pnlRightTop.Dock = DockStyle.Top;
    this.pnlRightTop.Location = new Point(0, 21);
    this.pnlRightTop.Name = "pnlRightTop";
    this.pnlRightTop.Size = new Size(378, 0);
    this.pnlRightTop.TabIndex = 1;
    this.tsmiImFilterDataClear.Name = "tsmiImFilterDataClear";
    this.tsmiImFilterDataClear.Size = new Size(183, 22);
    this.tsmiImFilterDataClear.Text = "Удалить фильтр";
    this.tsmiImFilterDataClear.Click += new EventHandler(this.tsmiImFilterDataClear_Click);
    this.tsmiImFilterDataSep1.Name = "tsmiImFilterDataSep1";
    this.tsmiImFilterDataSep1.Size = new Size(180, 6);
    this.tsmiImFilterDataPaste.Name = "tsmiImFilterDataPaste";
    this.tsmiImFilterDataPaste.Size = new Size(183, 22);
    this.tsmiImFilterDataPaste.Text = "Вставить фильтр";
    this.tsmiImFilterDataPaste.Click += new EventHandler(this.tsmiImFilterDataPaste_Click);
    this.tsmiImFilterDataCopy.Name = "tsmiImFilterDataCopy";
    this.tsmiImFilterDataCopy.Size = new Size(183, 22);
    this.tsmiImFilterDataCopy.Text = "Копировать фильтр";
    this.tsmiImFilterDataCopy.Click += new EventHandler(this.tsmiImFilterDataCopy_Click);
    this.cmsImFilterData.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiImFilterDataCopy,
      (ToolStripItem) this.tsmiImFilterDataPaste,
      (ToolStripItem) this.tsmiImFilterDataSep1,
      (ToolStripItem) this.tsmiImFilterDataClear
    });
    this.cmsImFilterData.Name = "cmsFilter";
    this.cmsImFilterData.Size = new Size(184, 76);
    this.cmsImFilterData.Opening += new CancelEventHandler(this.cmsImFilterData_Opening);
    this.tvImCatalog.CheckBoxes = true;
    this.tvImCatalog.ContextMenuStrip = this.cmsImFilterData;
    this.tvImCatalog.Dock = DockStyle.Fill;
    this.tvImCatalog.HideSelection = false;
    this.tvImCatalog.ImageIndex = 0;
    this.tvImCatalog.ImageList = this.imageList;
    this.tvImCatalog.Location = new Point(0, 21);
    this.tvImCatalog.Name = "tvImCatalog";
    this.tvImCatalog.SelectedImageIndex = 0;
    this.tvImCatalog.Size = new Size(378, 306);
    this.tvImCatalog.TabIndex = 0;
    this.tvImCatalog.AfterExpand += new TreeViewEventHandler(this.tvImCatalog_AfterExpand);
    this.tvImCatalog.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.tvImCatalog_NodeMouseClick);
    this.tvImCatalog.MouseDown += new MouseEventHandler(this.tvImCatalog_MouseDown);
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Magenta;
    this.imageList.Images.SetKeyName(0, "folderop.bmp");
    this.imageList.Images.SetKeyName(1, "folder.bmp");
    this.imageList.Images.SetKeyName(2, "папка_с_фильтром.bmp");
    this.imageList.Images.SetKeyName(3, "папка_с_фильтром1.bmp");
    this.imageList.Images.SetKeyName(4, "папка_наследующая_фильтр.bmp");
    this.imageList.Images.SetKeyName(5, "папка_наследующая_фильтр1.bmp");
    this.splitContainer2.Dock = DockStyle.Fill;
    this.splitContainer2.Location = new Point(0, 0);
    this.splitContainer2.Name = "splitContainer2";
    this.splitContainer2.Orientation = Orientation.Horizontal;
    this.splitContainer2.Panel1.Controls.Add((Control) this.tvImCatalog);
    this.splitContainer2.Panel1.Controls.Add((Control) this.pnlRightTop);
    this.splitContainer2.Panel1.Controls.Add((Control) this.cbImCatalogs);
    this.splitContainer2.Panel2.Controls.Add((Control) this.lbFilterList);
    this.splitContainer2.Panel2.Controls.Add((Control) this.pnlCheckListInfo);
    this.splitContainer2.Size = new Size(378, 494);
    this.splitContainer2.SplitterDistance = 327;
    this.splitContainer2.SplitterWidth = 8;
    this.splitContainer2.TabIndex = 1;
    this.cbImCatalogs.Dock = DockStyle.Top;
    this.cbImCatalogs.DrawMode = DrawMode.OwnerDrawFixed;
    this.cbImCatalogs.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbImCatalogs.FormattingEnabled = true;
    this.cbImCatalogs.Location = new Point(0, 0);
    this.cbImCatalogs.Name = "cbImCatalogs";
    this.cbImCatalogs.Size = new Size(378, 21);
    this.cbImCatalogs.TabIndex = 2;
    this.cbImCatalogs.DrawItem += new DrawItemEventHandler(this.cbImCatalogs_DrawItem);
    this.cbImCatalogs.MeasureItem += new MeasureItemEventHandler(this.cbImCatalogs_MeasureItem);
    this.cbImCatalogs.SelectedIndexChanged += new EventHandler(this.cbImCatalogs_SelectedIndexChanged);
    this.pnlLeftSplit.Controls.Add((Control) this.lvFilters);
    this.pnlLeftSplit.Dock = DockStyle.Top;
    this.pnlLeftSplit.Location = new Point(0, 0);
    this.pnlLeftSplit.Name = "pnlLeftSplit";
    this.pnlLeftSplit.Size = new Size(323, 212);
    this.pnlLeftSplit.TabIndex = 3;
    this.lvFilters.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader4,
      this.columnHeader2,
      this.columnHeader3
    });
    this.lvFilters.ContextMenuStrip = this.cmsFilters;
    this.lvFilters.Dock = DockStyle.Fill;
    this.lvFilters.FullRowSelect = true;
    this.lvFilters.GridLines = true;
    this.lvFilters.HideSelection = false;
    this.lvFilters.Location = new Point(0, 0);
    this.lvFilters.MultiSelect = false;
    this.lvFilters.Name = "lvFilters";
    this.lvFilters.Size = new Size(323, 212);
    this.lvFilters.TabIndex = 3;
    this.lvFilters.UseCompatibleStateImageBehavior = false;
    this.lvFilters.View = View.Details;
    this.lvFilters.SelectedIndexChanged += new EventHandler(this.lvFilters_SelectedIndexChanged);
    this.columnHeader4.Text = "ID фильтра";
    this.columnHeader4.Width = 79;
    this.columnHeader2.Text = "Наименование фильтра";
    this.columnHeader2.Width = 142;
    this.columnHeader3.Text = "Тип объекта";
    this.columnHeader3.Width = 91;
    this.cmsFilters.Items.AddRange(new ToolStripItem[10]
    {
      (ToolStripItem) this.tsmiFilterNew,
      (ToolStripItem) this.tsmiFilterEdit,
      (ToolStripItem) this.tsmiFilterSep1,
      (ToolStripItem) this.tsmiFilterCopy,
      (ToolStripItem) this.tsmiFilterPaste,
      (ToolStripItem) this.tsmiFilterSep2,
      (ToolStripItem) this.tsmiFilterDelete,
      (ToolStripItem) this.tsmiFIlterSep3,
      (ToolStripItem) this.tsmiFilterView,
      (ToolStripItem) this.tsmiFilterRefresh
    });
    this.cmsFilters.Name = "cmsFilter";
    this.cmsFilters.Size = new Size(162, 176 /*0xB0*/);
    this.cmsFilters.Opening += new CancelEventHandler(this.cmsFilters_Opening);
    this.tsmiFilterNew.Name = "tsmiFilterNew";
    this.tsmiFilterNew.Size = new Size(161, 22);
    this.tsmiFilterNew.Text = "Создать фильтр";
    this.tsmiFilterNew.Click += new EventHandler(this.tsmiFilterNew_Click);
    this.tsmiFilterEdit.Name = "tsmiFilterEdit";
    this.tsmiFilterEdit.Size = new Size(161, 22);
    this.tsmiFilterEdit.Text = "Изменить";
    this.tsmiFilterEdit.Click += new EventHandler(this.tsmiFilterEdit_Click);
    this.tsmiFilterSep1.Name = "tsmiFilterSep1";
    this.tsmiFilterSep1.Size = new Size(158, 6);
    this.tsmiFilterCopy.Name = "tsmiFilterCopy";
    this.tsmiFilterCopy.Size = new Size(161, 22);
    this.tsmiFilterCopy.Text = "Копировать ";
    this.tsmiFilterCopy.Click += new EventHandler(this.tsmiFilterCopy_Click);
    this.tsmiFilterPaste.Name = "tsmiFilterPaste";
    this.tsmiFilterPaste.Size = new Size(161, 22);
    this.tsmiFilterPaste.Text = "Вставить ";
    this.tsmiFilterPaste.Click += new EventHandler(this.tsmiFilterPaste_Click);
    this.tsmiFilterSep2.Name = "tsmiFilterSep2";
    this.tsmiFilterSep2.Size = new Size(158, 6);
    this.tsmiFilterDelete.Name = "tsmiFilterDelete";
    this.tsmiFilterDelete.Size = new Size(161, 22);
    this.tsmiFilterDelete.Text = "Удалить ";
    this.tsmiFilterDelete.Click += new EventHandler(this.tsmiFilterDelete_Click);
    this.tsmiFIlterSep3.Name = "tsmiFIlterSep3";
    this.tsmiFIlterSep3.Size = new Size(158, 6);
    this.tsmiFilterView.Name = "tsmiFilterView";
    this.tsmiFilterView.Size = new Size(161, 22);
    this.tsmiFilterView.Text = "Свойства";
    this.tsmiFilterView.Visible = false;
    this.tsmiFilterRefresh.Name = "tsmiFilterRefresh";
    this.tsmiFilterRefresh.Size = new Size(161, 22);
    this.tsmiFilterRefresh.Text = "Обновить";
    this.tsmiFilterRefresh.Click += new EventHandler(this.tsmiFilterRefresh_Click);
    this.pnlLeftTop.AutoSize = true;
    this.pnlLeftTop.Dock = DockStyle.Top;
    this.pnlLeftTop.Location = new Point(0, 0);
    this.pnlLeftTop.Name = "pnlLeftTop";
    this.pnlLeftTop.Size = new Size(323, 0);
    this.pnlLeftTop.TabIndex = 2;
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.lvFilterItems);
    this.splitContainer1.Panel1.Controls.Add((Control) this.pnlLeftSplit);
    this.splitContainer1.Panel1.Controls.Add((Control) this.pnlLeftTop);
    this.splitContainer1.Panel2.Controls.Add((Control) this.splitContainer2);
    this.splitContainer1.Size = new Size(709, 494);
    this.splitContainer1.SplitterDistance = 323;
    this.splitContainer1.SplitterWidth = 8;
    this.splitContainer1.TabIndex = 1;
    this.lvFilterItems.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.lvFilterItems.ContextMenuStrip = this.cmsFilterItems;
    this.lvFilterItems.Dock = DockStyle.Fill;
    this.lvFilterItems.FullRowSelect = true;
    this.lvFilterItems.GridLines = true;
    this.lvFilterItems.HideSelection = false;
    this.lvFilterItems.Location = new Point(0, 212);
    this.lvFilterItems.MultiSelect = false;
    this.lvFilterItems.Name = "lvFilterItems";
    this.lvFilterItems.Size = new Size(323, 282);
    this.lvFilterItems.TabIndex = 5;
    this.lvFilterItems.UseCompatibleStateImageBehavior = false;
    this.lvFilterItems.View = View.Details;
    this.lvFilterItems.SelectedIndexChanged += new EventHandler(this.lvFilterItems_SelectedIndexChanged);
    this.lvFilterItems.MouseDoubleClick += new MouseEventHandler(this.lvFilterItems_MouseDoubleClick);
    this.columnHeader1.Text = "Условия фильтра";
    this.columnHeader1.Width = 300;
    this.cmsFilterItems.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this.tsmiFilterItemAdd,
      (ToolStripItem) this.tsmiFilterItemEdit,
      (ToolStripItem) this.tsmiFilterItemSep2,
      (ToolStripItem) this.tsmiFilterItemCopy,
      (ToolStripItem) this.tsmiFilterItemAllCopy,
      (ToolStripItem) this.tsmiFilterItemPaste,
      (ToolStripItem) this.tsmiFilterItemSep1,
      (ToolStripItem) this.tsmiFilterItemDelete
    });
    this.cmsFilterItems.Name = "cmsFilter";
    this.cmsFilterItems.Size = new Size(253, 170);
    this.cmsFilterItems.Opening += new CancelEventHandler(this.cmsFilterItems_Opening);
    this.tsmiFilterItemAdd.Name = "tsmiFilterItemAdd";
    this.tsmiFilterItemAdd.Size = new Size(252, 22);
    this.tsmiFilterItemAdd.Text = "Добавить условие";
    this.tsmiFilterItemAdd.Click += new EventHandler(this.tsmiFilterItemAdd_Click);
    this.tsmiFilterItemEdit.Name = "tsmiFilterItemEdit";
    this.tsmiFilterItemEdit.Size = new Size(252, 22);
    this.tsmiFilterItemEdit.Text = "Изменить";
    this.tsmiFilterItemEdit.Click += new EventHandler(this.tsmiFilterItemEdit_Click);
    this.tsmiFilterItemSep2.Name = "tsmiFilterItemSep2";
    this.tsmiFilterItemSep2.Size = new Size(249, 6);
    this.tsmiFilterItemCopy.Name = "tsmiFilterItemCopy";
    this.tsmiFilterItemCopy.Size = new Size(252, 22);
    this.tsmiFilterItemCopy.Text = "Копировать ";
    this.tsmiFilterItemCopy.Click += new EventHandler(this.tsmiFilterItemCopy_Click);
    this.tsmiFilterItemPaste.Name = "tsmiFilterItemPaste";
    this.tsmiFilterItemPaste.Size = new Size(252, 22);
    this.tsmiFilterItemPaste.Text = "Вставить ";
    this.tsmiFilterItemPaste.Click += new EventHandler(this.tsmiFilterItemPaste_Click);
    this.tsmiFilterItemSep1.Name = "tsmiFilterItemSep1";
    this.tsmiFilterItemSep1.Size = new Size(249, 6);
    this.tsmiFilterItemDelete.Name = "tsmiFilterItemDelete";
    this.tsmiFilterItemDelete.Size = new Size(252, 22);
    this.tsmiFilterItemDelete.Text = "Удалить ";
    this.tsmiFilterItemDelete.Click += new EventHandler(this.tsmiFilterItemDelete_Click);
    this.tsmiFilterItemAllCopy.Name = "tsmiFilterItemAllCopy";
    this.tsmiFilterItemAllCopy.Size = new Size(252, 22);
    this.tsmiFilterItemAllCopy.Text = "Копировать с данными фильтра";
    this.tsmiFilterItemAllCopy.Click += new EventHandler(this.tsmiFilterItemAllCopy_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (ImbaseObjFilterTune);
    this.Size = new Size(709, 494);
    this.pnlCheckListInfo.ResumeLayout(false);
    this.pnlCheckListInfo.PerformLayout();
    this.cmsImFilterData.ResumeLayout(false);
    this.splitContainer2.Panel1.ResumeLayout(false);
    this.splitContainer2.Panel1.PerformLayout();
    this.splitContainer2.Panel2.ResumeLayout(false);
    this.splitContainer2.EndInit();
    this.splitContainer2.ResumeLayout(false);
    this.pnlLeftSplit.ResumeLayout(false);
    this.cmsFilters.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.cmsFilterItems.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  internal static class Consts
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

  internal class FilterData4Clipboard : List<FolderFilterTune.FilterNodeInfo>
  {
    public FilterData4Clipboard()
    {
    }

    public FilterData4Clipboard(
      IEnumerable<FolderFilterTune.FilterNodeInfo> collection)
      : base(collection)
    {
    }

    public FilterData4Clipboard(int capacity)
      : base(capacity)
    {
    }
  }

  internal class ImCatalogInfo : 
    ShortObjectDecription,
    ICloneable,
    IComparable<ImbaseObjFilterTune.ImCatalogInfo>
  {
    public bool HasFilterData;
    public string ObjPath = string.Empty;

    public ImCatalogInfo(long objectID, string objCaption, string objPath)
      : base(objectID, objCaption)
    {
      this.ObjPath = objPath;
    }

    public object Clone()
    {
      return (object) new ImbaseObjFilterTune.ImCatalogInfo(this.ObjID, this.ObjCaption, this.ObjPath)
      {
        HasFilterData = this.HasFilterData
      };
    }

    public int CompareTo(ImbaseObjFilterTune.ImCatalogInfo other)
    {
      return this.ObjID.CompareTo(other.ObjID);
    }
  }
}
