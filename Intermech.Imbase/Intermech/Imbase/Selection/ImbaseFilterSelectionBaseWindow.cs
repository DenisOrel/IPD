// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFilterSelectionBaseWindow
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Favorites;
using Intermech.Imbase.QuickSearch;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseFilterSelectionBaseWindow : Form, IIODestination
{
  private const ViewStateFlags FavouritesViewStateFlags = ViewStateFlags.NoContainsInView | ViewStateFlags.NoEventsView | ViewStateFlags.NoPluginsViews | ViewStateFlags.NoGroupingObjectsViews;
  private const ViewStateFlags CommonViewStateflags = ViewStateFlags.ReadOnly | ViewStateFlags.NoContainsInView | ViewStateFlags.NoEventsView | ViewStateFlags.NoPluginsViews | ViewStateFlags.NoGroupingObjectsViews;
  private ViewStateService _viewStateService;
  private ToolStripMenuItem _checkedFolderFilterItem;
  private ToolStripMenuItem _checkedObjFilterItem;
  private bool _filterChanged;
  private DateTime _serverTime = DateTime.MinValue;
  private bool _textChanged;
  private bool _isSearchMode;
  private readonly ImbaseQuickSearchHelper _quickSearchHelper;
  private long _newRelationId = -1;
  protected readonly IServiceContainer _services = (IServiceContainer) new ServiceContainer();
  protected TreeBuilder _treeBuilder;
  protected long _ownerObjectId;
  protected int _ownerObjTypeID = -1;
  protected long _imbaseObjectId;
  protected long _imbaseRecID = -1;
  protected long _prevSelectedObjID;
  protected IList<long> _catalogIDs;
  protected string _areaFilterGuid = string.Empty;
  protected string _userFilterGuid = string.Empty;
  protected string _roleFilterGuid = string.Empty;
  private DataTable _dtTree;
  protected DataTable _dtFilter;
  protected List<ImbaseObjFilterInfo> _objFilterList;
  protected Dictionary<long, IDescriptor> _descrs = new Dictionary<long, IDescriptor>(0);
  protected string _folderOwnerGuid = string.Empty;
  protected long _objFilterID;
  private IContainer components;
  private Panel _pnlBottom;
  public PictureBox _pbObject;
  public Label _lbDescription;
  private ToolStrip _tsFilters;
  private ToolStripMenuItem _tsmiSearchByImg;
  private ToolStripMenuItem _tsmiSearchByName;
  private ToolStripMenuItem _tsmiSearchInTbl;
  private ToolStripMenuItem _tsmiSearchByIndex;
  private ToolStripSeparator _tsSeparator;
  private ToolStripDropDownButton _tsBtnFilterSettings;
  private ToolStripMenuItem _tsmiFolderFilterSetup;
  private ToolStripMenuItem _tsmiObjFilterSetup;
  private ToolStripSeparator tsmiObjFilterSep1;
  private ToolStripSeparator tsmiFilterSep1;
  public PageViewsManager _viewsMngr;
  private StatusStrip _statusStrip;
  private ToolStripStatusLabel _lbWarning;
  private ContextMenuStrip _cms;
  private ToolStripMenuItem _cmmiSearchByName;
  private ToolStripMenuItem _cmmiSearchByImg;
  private ToolStripMenuItem _cmmiSearchInTbl;
  private ToolStripMenuItem _cmmiSearchByIndex;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem _cmmiCollapse;
  private ToolStripMenuItem _cmmiUpdate;
  protected TreeView _trv;
  protected ToolStripDropDownButton _tsBtnFolderFilter;
  protected ImageList _imgList;
  protected ToolStripDropDownButton _tsBtnObjFilter;
  protected SplitContainer _spltContainer;
  protected ToolStripMenuItem _tsmiFolderFilterNone;
  protected ToolStripMenuItem _tsmiObjFilterNone;
  protected ToolStripMenuItem _tsmiObjFilterCommon;
  protected ToolStripMenuItem _tsmiObjFilterUser;
  protected ToolStripMenuItem _tsmiObjFilterArea;
  protected ToolStripMenuItem _tsmiObjFilterRole;
  protected ToolStripMenuItem _tsmiFolderFilterCommon;
  protected ToolStripMenuItem _tsmiFolderFilterUser;
  protected ToolStripMenuItem _tsmiFolderFilterArea;
  protected ToolStripMenuItem _tsmiFolderFilterRole;
  protected ToolStripDropDownButton _tsBtnSearch;
  protected ToolStripMenuItem _cmmiSearch;
  protected Panel _pnlTop;
  private ListView _lvSearchResult;
  private ColumnHeader _colText;
  private Timer _timer;
  protected TextBox _txtSearch;
  public Label _lblTreePath;
  private Timer _serverTimer;
  protected internal SplitContainer _splitContainerLeft;
  protected TableLayoutPanel _tlp;
  public Button _btnCancel;
  public Button _btnApply;
  private ToolStripMenuItem miFavorites;
  private ToolStripMenuItem miAddToFavorites;
  private ToolStripMenuItem miCreateFavorites;
  private ToolStripMenuItem miFindInTree;
  private ToolStripMenuItem miRemoveFromFavorites;
  private ToolStripMenuItem miRemoveFavorites;

  private void InitImages()
  {
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
    {
      this._cmmiSearch.Image = this._tsBtnSearch.Image = service.ImageList.Images[service.ImageIndex("imgFind")];
      this._cmmiSearchByImg.Image = this._tsmiSearchByImg.Image = service.ImageList.Images[service.ImageIndex("imgFindByImages")];
      this._cmmiSearchByName.Image = this._tsmiSearchByName.Image = service.ImageList.Images[service.ImageIndex("imgSearchTree")];
      if (ServicesManager.GetService(typeof (IImbaseSelector)) != null)
      {
        this._cmmiSearchInTbl.Image = this._tsmiSearchInTbl.Image = service.ImageList.Images[service.ImageIndex("imgFindInTables")];
        this._cmmiSearchByIndex.Image = this._tsmiSearchByIndex.Image = service.ImageList.Images[service.ImageIndex("imgFindByIndex")];
      }
      this.miAddToFavorites.Image = service.ImageList.Images[service.ImageIndex("addFavorites")];
      this.miRemoveFromFavorites.Image = service.ImageList.Images[service.ImageIndex("delFavorites")];
      this.miFindInTree.Image = service.ImageList.Images[service.ImageIndex("show")];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(Intermech.Imbase.Consts.ImbaseFavoritesTypeID);
        if (objectType?.Icon != null)
        {
          if (objectType.Icon.Length != 0)
          {
            using (MemoryStream memoryStream = new MemoryStream(objectType.Icon))
              this.miCreateFavorites.Image = Image.FromStream((Stream) memoryStream);
          }
        }
      }
      this.miRemoveFavorites.Image = service.ImageList.Images[service.ImageIndex("imgDelete")];
    }
    this._tsBtnFolderFilter.Image = this._imgList.Images[0];
    this._tsBtnObjFilter.Image = this._imgList.Images[2];
    this._tsBtnFilterSettings.Image = this._imgList.Images[4];
    this._lvSearchResult.SmallImageList = TreeBuilder.ImageList;
  }

  private void GetImbaseData(IUserSession session, long ownerObjId)
  {
    IDBObject objectActualCopy = ownerObjId != 0L ? session.GetObjectActualCopy(ownerObjId, false) : (IDBObject) null;
    if (objectActualCopy == null)
      return;
    this._ownerObjTypeID = objectActualCopy.TypeID;
    IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(Intermech.Imbase.Consts.ImbaseObjectRefAttGUID);
    if (attributeByGuid == null)
      return;
    this._imbaseObjectId = attributeByGuid.AsInteger;
    IDBAttribute attributeById = objectActualCopy.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
    this._imbaseRecID = attributeById != null ? attributeById.AsInteger : -1L;
  }

  private void LoadFolderFiltersInfo(IUserSession session)
  {
    this._tsmiFolderFilterCommon.Tag = (object) string.Empty;
    this._tsmiFolderFilterUser.Tag = (object) (this._userFilterGuid = Convert.ToString((object) session.GetObject(session.UserID).ObjectGUID));
    this._tsmiFolderFilterRole.Tag = (object) (this._roleFilterGuid = Convert.ToString((object) session.GetObject(session.RoleID).ObjectGUID));
    if (session.AreaID.Length > 0)
    {
      DataTable source = session.GetSubjectAreaCollection().Select(string.Empty);
      if (source != null)
      {
        DataRow dataRow = source.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => (int) Convert.ToChar(x["F_AREA_ID"]) == (int) session.AreaID[0]));
        this._tsmiFolderFilterArea.Tag = (object) (this._areaFilterGuid = dataRow != null ? Convert.ToString(dataRow["F_GUID"]) : string.Empty);
      }
    }
    if (string.IsNullOrEmpty(this._areaFilterGuid))
      this._tsmiFolderFilterArea.Tag = (object) (this._areaFilterGuid = Convert.ToString((object) Guid.NewGuid()));
    this._tsBtnFolderFilter.Enabled = this._tsmiFolderFilterSetup.Enabled = this._imbaseObjectId != 0L;
  }

  protected void LoadObjectFiltersInfo(IUserSession session)
  {
    if (this._ownerObjTypeID != -1)
      this._objFilterList = session.GetCustomService(typeof (IObjectFilterService)) is IObjectFilterService customService ? customService.GetFilterList(session.SessionGUID, this._ownerObjTypeID) : (List<ImbaseObjFilterInfo>) null;
    if (this._objFilterList == null || this._objFilterList.Count == 0)
    {
      this._tsBtnObjFilter.Enabled = false;
    }
    else
    {
      this.FillObjFilterMenu(this._tsmiObjFilterCommon, string.Empty);
      this.FillObjFilterMenu(this._tsmiObjFilterUser, this._userFilterGuid);
      this.FillObjFilterMenu(this._tsmiObjFilterRole, this._roleFilterGuid);
      this.FillObjFilterMenu(this._tsmiObjFilterArea, this._areaFilterGuid);
      this._tsBtnObjFilter.Enabled = this._tsmiObjFilterCommon.Enabled || this._tsmiObjFilterUser.Enabled || this._tsmiObjFilterRole.Enabled || this._tsmiObjFilterArea.Enabled;
    }
    this._tsmiObjFilterSetup.Enabled = this._ownerObjTypeID != -1;
  }

  private void FillObjFilterMenu(ToolStripMenuItem menuItem, string filterOwnMode)
  {
    foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) menuItem.DropDownItems)
      dropDownItem.Click -= new EventHandler(this.On_tsmiObjFilter_Click);
    menuItem.DropDownItems.Clear();
    foreach (ImbaseObjFilterInfo objFilter in this._objFilterList)
    {
      if (objFilter != null && !(objFilter.Owner != filterOwnMode))
      {
        ToolStripItemCollection dropDownItems = menuItem.DropDownItems;
        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(objFilter.Caption, (Image) null, new EventHandler(this.On_tsmiObjFilter_Click));
        toolStripMenuItem.Tag = (object) objFilter;
        dropDownItems.Add((ToolStripItem) toolStripMenuItem);
      }
    }
    menuItem.Enabled = menuItem.DropDownItems.Count > 0;
  }

  private void LoadDataForTree(IUserSession session)
  {
    IImbaseServer service = ServiceUtils.GetService<IImbaseServer>((object) session, false);
    if (service == null)
      return;
    long[] array = this._catalogIDs == null || this._catalogIDs.Count <= 0 ? (long[]) null : this._catalogIDs.ToArray<long>();
    IEnumerable<int> objectTypeIds = this.SelectionParams.ObjectTypeIds;
    if ((objectTypeIds != null ? (objectTypeIds.Any<int>() ? 1 : 0) : 0) != 0)
    {
      this._dtTree = service.GetFoldersForCreateType(session.SessionGUID, (object) this.SelectionParams.ObjectTypeIds.ToArray<int>(), array, true);
    }
    else
    {
      this._dtTree = (DataTable) null;
      if (array != null)
      {
        foreach (long parentId in array)
        {
          DataTable allSubfolders = service.GetAllSubfolders(session.SessionGUID, parentId, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
          if (this._dtTree == null)
            this._dtTree = allSubfolders;
          else
            DataSetProcessor.AddTable(this._dtTree, allSubfolders, false);
        }
        if (this._dtTree != null)
          this._dtTree.AcceptChanges();
      }
    }
    if (this._dtTree == null)
      return;
    this._catalogIDs = (IList<long>) this._dtTree.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_PATH"]).Length == 2)).Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["F_OBJECT_ID"]))).ToList<long>();
  }

  private void InitializeServices()
  {
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
    {
      this._services.AddService(typeof (INotificationService), (object) service);
      service.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectChangedEvent));
    }
    this._viewStateService = new ViewStateService(ViewStateFlags.ReadOnly | ViewStateFlags.NoContainsInView | ViewStateFlags.NoEventsView | ViewStateFlags.NoPluginsViews | ViewStateFlags.NoGroupingObjectsViews);
    this._services.AddService(typeof (IViewState), (object) this._viewStateService);
    IIODispatcher serviceInstance = (IIODispatcher) new IODispatcher();
    serviceInstance.RegisterDestination((IIODestination) this);
    this._services.AddService(typeof (IIODispatcher), (object) serviceInstance);
    this._viewsMngr.Services = (System.IServiceProvider) this._services;
  }

  private void ObjectChangedEvent(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || this._trv.SelectedNode == null || !(this._trv.SelectedNode.Tag is NodeInfo tag) || !objectsEventArgs.ObjectIDs.Contains(tag.ObjectId))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(tag.ObjectId, false);
      if (dbObject == null)
        return;
      this._trv.SelectedNode.Text = dbObject.Caption;
    }
  }

  private void ServicesFinalization()
  {
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
      service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectChangedEvent));
    this._services.RemoveService(typeof (IIODispatcher));
    this._services.RemoveService(typeof (INotificationService));
    this._services.RemoveService(typeof (IViewState));
  }

  private void SetSearchResultVisible(bool value)
  {
    this._lvSearchResult.Visible = value;
    if (!value)
      return;
    this._lvSearchResult.BringToFront();
    Point client = this._txtSearch.FindForm().PointToClient(this._txtSearch.Parent.PointToScreen(this._txtSearch.Location));
    this._lvSearchResult.Location = new Point(client.X, client.Y + this._txtSearch.Bottom);
    this._lvSearchResult.Height = this._lvSearchResult.Items.Count > 0 ? this._lvSearchResult.Items[0].GetBounds(ItemBoundsPortion.ItemOnly).Height * this._lvSearchResult.Items.Count + 5 : 50;
    int num = 0;
    foreach (ListViewItem listViewItem in this._lvSearchResult.Items)
    {
      Size size = TextRenderer.MeasureText(listViewItem.Text, this._lvSearchResult.Font);
      if (num <= size.Width)
        num = size.Width;
    }
    int width = this._lvSearchResult.SmallImageList.ImageSize.Width;
    if (num > 0)
    {
      this._lvSearchResult.Width = num + this._lvSearchResult.Items[0].Position.X + width + 2;
      this._lvSearchResult.Columns[0].Width = num + width + 1;
    }
    else
    {
      this._lvSearchResult.Width = this._txtSearch.Width;
      this._lvSearchResult.Columns[0].Width = width + 1;
    }
  }

  private bool SearchFolders(string text)
  {
    bool flag = false;
    this._lvSearchResult.BeginUpdate();
    this._lvSearchResult.Items.Clear();
    try
    {
      List<ImbaseQuickSearchItem> imbaseQuickSearchItemList = this._quickSearchHelper.SearchFolders(text, 20);
      if (imbaseQuickSearchItemList != null)
      {
        imbaseQuickSearchItemList.ForEach((Action<ImbaseQuickSearchItem>) (x => this._lvSearchResult.Items.Add(new ListViewItem(x.Caption, TreeBuilder.GetIconIndex(Intermech.Imbase.Consts.ImbaseFolderTypeID))
        {
          Tag = (object) x
        })));
        flag = true;
      }
    }
    catch (ApplicationException ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, ex.Message, LocalizationHolder.rm.GetString("Imbase.Client_45"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    finally
    {
      this._lvSearchResult.EndUpdate();
    }
    return flag;
  }

  private async void Start(int elementCount)
  {
    await Task.Run((Action) (() => this.StartSearch(elementCount)));
  }

  private void StartSearch(int elementCount)
  {
    string text = this._txtSearch.Text;
    List<ImbaseQuickSearchItem> items = this._quickSearchHelper.SearchRecords(text, elementCount);
    if (items == null || !(text == this._txtSearch.Text))
      return;
    this.QuickSearchCallback(items);
  }

  private void QuickSearchCallback(List<ImbaseQuickSearchItem> items)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new Action<List<ImbaseQuickSearchItem>>(this.QuickSearchCallback), (object) items);
    }
    else
    {
      if (!this._txtSearch.Focused && !this._lvSearchResult.Focused)
        return;
      this._lvSearchResult.BeginUpdate();
      try
      {
        items.ForEach((Action<ImbaseQuickSearchItem>) (x => this._lvSearchResult.Items.Add(new ListViewItem(x.Caption, TreeBuilder.GetIconIndex(Intermech.Imbase.Consts.ImbaseTableRefTypeID))
        {
          Tag = (object) x
        })));
      }
      catch (ApplicationException ex)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, ex.Message, LocalizationHolder.rm.GetString("Imbase.Client_45"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      finally
      {
        this._lvSearchResult.EndUpdate();
        this.SetSearchResultVisible(true);
      }
    }
  }

  protected string GetFilterOwnerGuid()
  {
    if (this._tsmiFolderFilterCommon.Checked)
      return string.Empty;
    return !this.DesignMode ? this._folderOwnerGuid : "unknown";
  }

  private void UpdateFilterImages()
  {
    ImbaseFilterSelectionBaseWindow.ImFilterMode filterMode = this.FilterMode;
    this._tsBtnFolderFilter.Image = filterMode == ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder ? this._imgList.Images[1] : this._imgList.Images[0];
    this._tsBtnObjFilter.Image = filterMode == ImbaseFilterSelectionBaseWindow.ImFilterMode.Object ? this._imgList.Images[3] : this._imgList.Images[2];
  }

  private TreeNode FindInTreeByObjectId(
    long objectId,
    bool findInFavorites,
    TreeNodeCollection nodes)
  {
    foreach (TreeNode node in nodes)
    {
      if (node.Tag is NodeInfo tag1)
      {
        if (node.Parent != null && node.Parent.Tag is NodeInfo tag && tag1.ObjectId == objectId && (tag.IsFavoritesFolder == findInFavorites || tag.IsCatalog))
          return node;
        if (tag1.IsCatalog || tag1.IsFavoritesFolder == findInFavorites)
        {
          TreeNode inTreeByObjectId = this.FindInTreeByObjectId(objectId, findInFavorites, node.Nodes);
          if (inTreeByObjectId != null)
            return inTreeByObjectId;
        }
      }
    }
    return (TreeNode) null;
  }

  private bool CheckForLoop(TreeNode draggedNode, TreeNode targetNode)
  {
    bool flag = false;
    for (TreeNode treeNode = targetNode; !flag && treeNode != null; treeNode = treeNode.Parent)
      flag = draggedNode == treeNode;
    return flag;
  }

  private bool NodeIsFromFavoritesBranch(TreeNode node)
  {
    bool flag = false;
    TreeNode parent = node.Parent;
    while (!flag && parent != null)
    {
      if (parent.Tag is NodeInfo tag)
      {
        flag = tag.IsFavoritesFolder;
        parent = parent.Parent;
      }
    }
    return flag;
  }

  protected virtual void LoadSettings()
  {
  }

  protected virtual void SaveSettings()
  {
  }

  protected virtual void InitializeData()
  {
    this.InitializeServices();
    this.InitImages();
    this._treeBuilder = new TreeBuilder(this.components)
    {
      TreeView = this._trv,
      AllowFavourites = true
    };
    this._checkedFolderFilterItem = this._tsmiFolderFilterNone;
    this._checkedObjFilterItem = this._tsmiObjFilterNone;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.LoadContextInfo(sessionKeeper.Session, this._ownerObjectId);
      this.LoadDataForTree(sessionKeeper.Session);
    }
    this._splitContainerLeft.Panel2Collapsed = true;
  }

  protected void LoadContextInfo(IUserSession session, long ownerObjectId)
  {
    this.GetImbaseData(session, ownerObjectId);
    this.LoadFolderFiltersInfo(session);
    this.LoadObjectFiltersInfo(session);
    this._tsBtnFilterSettings.Enabled = this._tsmiFolderFilterSetup.Enabled || this._tsmiObjFilterSetup.Enabled;
  }

  protected virtual bool FilterUpdate()
  {
    bool flag = false;
    if (this._dtTree != null)
    {
      this._dtFilter = (DataTable) null;
      DataTable dataTable1 = this._dtTree.Copy();
      this._trv.BeginUpdate();
      try
      {
        ImbaseFilterSelectionBaseWindow.ImFilterMode filterMode = this.FilterMode;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          switch (filterMode)
          {
            case ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder:
              if (this._imbaseObjectId != 0L && !this._tsmiFolderFilterNone.Checked && sessionKeeper.Session.GetCustomService(typeof (IFolderFilterService)) is IFolderFilterService customService1)
              {
                this._dtFilter = customService1.ApplyFilter(sessionKeeper.Session.SessionGUID, this._imbaseObjectId, this.GetFilterOwnerGuid(), dataTable1);
                break;
              }
              break;
            case ImbaseFilterSelectionBaseWindow.ImFilterMode.Object:
              if (sessionKeeper.Session.GetCustomService(typeof (IObjectFilterService)) is IObjectFilterService customService2)
              {
                HybridDictionary extArgs = new HybridDictionary()
                {
                  {
                    (object) ObjectFilterConsts.args_ObjectID,
                    (object) this._ownerObjectId
                  }
                };
                if (this.ExtraAttrValues != null)
                  extArgs.Add((object) ObjectFilterConsts.args_ExtraAttrs, (object) this.ExtraAttrValues);
                this._dtFilter = customService2.ApplyFilter(sessionKeeper.Session.SessionGUID, this._objFilterID, (string) null, dataTable1, extArgs);
                break;
              }
              break;
          }
          DataTable dataTable2 = this._dtFilter ?? dataTable1;
          if (dataTable2 != null)
          {
            int idxFilter = dataTable2.Columns.IndexOf("#FLT");
            int idxObjId = dataTable2.Columns.IndexOf("F_OBJECT_ID");
            List<long> list = idxFilter != -1 ? dataTable2.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => row[idxFilter] != DBNull.Value && Convert.ToBoolean(row[idxFilter]))).Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[idxObjId]))).ToList<long>() : (List<long>) null;
            this._treeBuilder.UpdateUnExploreRows(dataTable2, sessionKeeper.Session);
            Dictionary<long, TreeNode> nodeIds = new Dictionary<long, TreeNode>(dataTable2.Rows.Count);
            this._treeBuilder.CreateTree(dataTable2, (IDictionary<long, TreeNode>) nodeIds);
            if (filterMode != ImbaseFilterSelectionBaseWindow.ImFilterMode.None)
            {
              if (list != null)
              {
                foreach (long key in list)
                {
                  TreeNode parent;
                  if (nodeIds.TryGetValue(key, out parent))
                  {
                    for (; parent != null; parent = parent.Parent)
                    {
                      if (!this._treeBuilder.UnexploredNode(parent))
                        parent.Expand();
                    }
                  }
                }
              }
            }
          }
        }
      }
      catch (ObjectsFoundException ex)
      {
        if (this.TopMost)
          this.TopMost = false;
        throw;
      }
      finally
      {
        if (flag = this._trv.Nodes.Count > 0)
        {
          foreach (TreeNode node in this._trv.Nodes)
            node.Expand();
          this._trv.SelectedNode = this._trv.Nodes[0];
        }
        this._trv.EndUpdate();
        this._filterChanged = true;
        this._quickSearchHelper.Filter = this._dtFilter;
      }
    }
    return flag;
  }

  protected virtual void SelectPreviousItemInTree()
  {
  }

  protected void UpdateTreeNodePath()
  {
    TreeNode treeNode = this._trv.SelectedNode;
    List<string> values = new List<string>(treeNode != null ? treeNode.Level : 0);
    for (; treeNode != null; treeNode = treeNode.Parent)
      values.Insert(0, treeNode.Text);
    this._lblTreePath.Text = string.Join("\\", (IEnumerable<string>) values);
  }

  public ImbaseFilterSelectionBaseWindow() => this.InitializeComponent();

  public ImbaseFilterSelectionBaseWindow(
    long ownerObjectId,
    [CanBeNull] IEnumerable<long> imbaseCatalogIds,
    [CanBeNull] IEnumerable<int> objectTypeIds)
    : this(new ImbaseSelectionParam(ownerObjectId, objectTypeIds, imbaseCatalogIds))
  {
  }

  public ImbaseFilterSelectionBaseWindow([NotNull] ImbaseSelectionParam imbaseSelectionParams)
  {
    this.InitializeComponent();
    this.SelectionParams = imbaseSelectionParams;
    this._ownerObjectId = imbaseSelectionParams.OwnerObjectId;
    IEnumerable<long> imbaseCatalogIds = imbaseSelectionParams.ImbaseCatalogIds;
    this._catalogIDs = (IList<long>) ((imbaseCatalogIds != null ? imbaseCatalogIds.ToList<long>() : (List<long>) null) ?? new List<long>());
    this.InitializeData();
    this._quickSearchHelper = new ImbaseQuickSearchHelper()
    {
      CatalogIDs = this._catalogIDs
    };
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.LoadSettings();
    this.FilterUpdate();
    this.SelectPreviousItemInTree();
    this._cmmiSearch.Enabled = this._cmmiCollapse.Enabled = this._tsBtnSearch.Enabled = true;
    this.UpdateFilterImages();
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    this.ServicesFinalization();
    this.SaveSettings();
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    bool flag = true;
    if (keyData == Keys.Return && this._lvSearchResult.Focused)
      this.On_lvSearchResult_DoubleClick((object) null, (EventArgs) null);
    else if (keyData != Keys.Return || !this._txtSearch.Focused)
      flag = base.ProcessCmdKey(ref msg, keyData);
    return flag;
  }

  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    this._txtSearch.Focus();
  }

  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  public bool ProcessEvent(IIOEvent Event)
  {
    bool flag = false;
    if (Event != null && Event.Source != null && Event.Source.SelectedItems != null && Event.Source.SelectedItems.Count > 0)
    {
      flag = Event.EventType != IOEventType.evMouseDoubleClick;
      if (!flag && Event.Source.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && this._treeBuilder.SetSelectedNode(itemData.ObjectID))
      {
        this._trv.SelectedNode.Expand();
        flag = true;
      }
    }
    return flag;
  }

  public ImbaseSelectionParam SelectionParams { get; }

  protected ImbaseFilterSelectionBaseWindow.ImFilterMode FilterMode
  {
    get
    {
      ImbaseFilterSelectionBaseWindow.ImFilterMode filterMode = ImbaseFilterSelectionBaseWindow.ImFilterMode.None;
      if (!this._tsmiFolderFilterNone.Checked && this._imbaseObjectId != 0L)
        filterMode = ImbaseFilterSelectionBaseWindow.ImFilterMode.Folder;
      else if (!this._tsmiObjFilterNone.Checked)
        filterMode = ImbaseFilterSelectionBaseWindow.ImFilterMode.Object;
      return filterMode;
    }
  }

  public bool ObjectVersionMode { get; set; } = true;

  protected ToolStripMenuItem FolderFilterItemChecked
  {
    get => this._checkedFolderFilterItem;
    set
    {
      this._folderOwnerGuid = Convert.ToString(value.Tag);
      this._objFilterID = 0L;
      this._checkedFolderFilterItem.Checked = false;
      this._checkedFolderFilterItem = value;
      this._checkedFolderFilterItem.Checked = true;
      this._checkedObjFilterItem.Checked = false;
      this._checkedObjFilterItem = this._tsmiObjFilterNone;
      this._checkedObjFilterItem.Checked = true;
      this.UpdateFilterImages();
    }
  }

  protected ToolStripMenuItem ObjectFilterItemChecked
  {
    get => this._checkedObjFilterItem;
    set
    {
      this._folderOwnerGuid = string.Empty;
      this._checkedFolderFilterItem.Checked = false;
      this._checkedFolderFilterItem = this._tsmiFolderFilterNone;
      this._checkedFolderFilterItem.Checked = true;
      this._checkedObjFilterItem.Checked = false;
      this._checkedObjFilterItem = value;
      this._checkedObjFilterItem.Checked = true;
      this.UpdateFilterImages();
    }
  }

  protected int SplitterDistance => this._spltContainer.SplitterDistance;

  public Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> ExtraAttrValues { get; set; }

  private void On_SearchByImg_Click(object sender, EventArgs e)
  {
    bool topMost = this.TopMost;
    try
    {
      this.TopMost = false;
      if (this._trv.SelectedNode == null)
        return;
      FindByImagesView.Show((object) this._trv.SelectedNode, true, (LocateNodeEventHandler) null);
    }
    finally
    {
      this.TopMost = topMost;
    }
  }

  private void On_SearchInTbl_Click(object sender, EventArgs e)
  {
    bool topMost = this.TopMost;
    try
    {
      this.TopMost = false;
      if (this._trv.SelectedNode == null)
        return;
      FindInTablesView.Show((object) this._trv.SelectedNode, true, (LocateNodeEventHandler) null);
    }
    finally
    {
      this.TopMost = topMost;
    }
  }

  private void On_SearchByIndex_Click(object sender, EventArgs e)
  {
    bool topMost = this.TopMost;
    try
    {
      this.TopMost = false;
      TreeNode parentNode = this._trv.SelectedNode;
      if (parentNode != null)
      {
        while (parentNode.Parent != null)
          parentNode = parentNode.Parent;
      }
      FindByIndexView.Show((object) parentNode, true, (LocateNodeEventHandler) null, (IViewsManager) null, (TreeViewsBridge) null);
    }
    finally
    {
      this.TopMost = topMost;
    }
  }

  private void On_SearchByName_Click(object sender, EventArgs e)
  {
    bool topMost = this.TopMost;
    try
    {
      this.TopMost = false;
      this.SearchByName(sender, e);
    }
    finally
    {
      this.TopMost = topMost;
    }
  }

  protected virtual void SearchByName(object sender, EventArgs e)
  {
  }

  private void On_cmmiCollapse_Click(object sender, EventArgs e)
  {
    this._trv.CollapseAll();
    if (this._trv.Nodes.Count <= 0)
      return;
    this._trv.SelectedNode = this._trv.Nodes[0];
  }

  private void On_cmmiUpdate_Click(object sender, EventArgs e)
  {
    this.FilterUpdate();
    if (this._trv.Nodes.Count <= 0)
      return;
    this._trv.SelectedNode = this._trv.Nodes[0];
  }

  private void On_tsmiFolderFilterSetup_Click(object sender, EventArgs e)
  {
    bool topMost = this.TopMost;
    try
    {
      this.TopMost = false;
      this.OnFolderFilterSetup_Click(sender, e);
    }
    finally
    {
      this.TopMost = topMost;
    }
  }

  protected virtual void OnFolderFilterSetup_Click(object sender, EventArgs e)
  {
  }

  private void On_tsmiObjFilterSetup_Click(object sender, EventArgs e)
  {
    bool topMost = this.TopMost;
    try
    {
      this.TopMost = false;
      this.OnObjFilterSetup_Click(sender, e);
    }
    finally
    {
      this.TopMost = topMost;
    }
  }

  protected virtual void OnObjFilterSetup_Click(object sender, EventArgs e)
  {
  }

  private void On_tsmiFolderFilter_Click(object sender, EventArgs e)
  {
    this.FolderFilterItemChecked = sender as ToolStripMenuItem;
    this.On_cmmiUpdate_Click((object) this._cmmiUpdate, e);
  }

  private void On_tsmiObjFilter_Click(object sender, EventArgs e)
  {
    this.ObjectFilterItemChecked = sender as ToolStripMenuItem;
    if (this.ObjectFilterItemChecked == null)
      return;
    this._objFilterID = this.ObjectFilterItemChecked.Tag is ImbaseObjFilterInfo tag ? tag.ObjectID : 0L;
    this.On_cmmiUpdate_Click((object) this._cmmiUpdate, e);
  }

  private void On_tsBtn_Click(object sender, EventArgs e) => this._tsFilters.Focus();

  private void On_txtSearch_TextChanged(object sender, EventArgs e)
  {
    this._serverTime = DateTime.Now;
    if (this._textChanged = this._txtSearch.Text.Length > 2)
      this.SetSearchResultVisible(this.SearchFolders(this._txtSearch.Text));
    else
      this.SetSearchResultVisible(false);
  }

  private void On_txtSearch_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Down || this._lvSearchResult.Items.Count <= 0)
      return;
    this._lvSearchResult.FocusedItem = this._lvSearchResult.Items[0];
    this._lvSearchResult.FocusedItem.Selected = true;
    this._lvSearchResult.Focus();
  }

  private void On_txtSearch_Enter(object sender, EventArgs e)
  {
    this._isSearchMode = true;
    this._timer.Start();
    if (this._quickSearchHelper.NeedTimerForServerRequest)
    {
      this._serverTime = DateTime.Now;
      this._serverTimer.Start();
    }
    if (this._filterChanged)
    {
      this._textChanged = true;
      this.On_txtSearch_TextChanged((object) this._txtSearch, e);
      this._filterChanged = false;
    }
    else
      this.SetSearchResultVisible(this._txtSearch.Text.Length > 2);
  }

  private void On_SearchControls_Leave(object sender, EventArgs e) => this._isSearchMode = false;

  private void On_lvSearchResult_KeyDown(object sender, KeyEventArgs e)
  {
    if (!this._lvSearchResult.Items[0].Selected || e.KeyCode != Keys.Up)
      return;
    this._txtSearch.Focus();
  }

  private void On_lvSearchResult_DoubleClick(object sender, EventArgs e)
  {
    this._trv.Focus();
    if (this._lvSearchResult.SelectedItems.Count > 0)
    {
      if (!(this._lvSearchResult.SelectedItems[0].Tag is ImbaseQuickSearchItem tag))
        return;
      this._treeBuilder.SetSelectedNode(tag.ObjectId);
      if (tag.ObjectTypeId == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        SelectedRecords.Add(tag.ObjectId, new long[1]
        {
          tag.RecordId
        });
    }
    this.SetSearchResultVisible(false);
  }

  private void On_lvSearchResult_Enter(object sender, EventArgs e) => this._isSearchMode = true;

  private void On_timer_Tick(object sender, EventArgs e)
  {
    if (this._isSearchMode)
      return;
    this.SetSearchResultVisible(false);
    this._timer.Stop();
  }

  private void On_serverTimer_Tick(object sender, EventArgs e)
  {
    if (this._isSearchMode)
    {
      if (!this._textChanged || (DateTime.Now - this._serverTime).TotalMilliseconds < 500.0)
        return;
      if (this._lvSearchResult.Items.Count < 20)
      {
        this.Start(20 - this._lvSearchResult.Items.Count);
        this._serverTime = DateTime.Now;
      }
      this._textChanged = false;
    }
    else
    {
      this._serverTimer.Stop();
      this._serverTime = DateTime.MinValue;
      this._textChanged = false;
    }
  }

  private void On_trv_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.SetViewStateFlags();
    this.UpdateTreeNodePath();
  }

  private void SetViewStateFlags()
  {
    if (this._viewStateService == null || this._trv.SelectedNode == null || !(this._trv.SelectedNode.Tag is NodeInfo tag))
      return;
    this._viewStateService.SetViewStateFlags(tag.IsFavoritesFolder ? ViewStateFlags.NoContainsInView | ViewStateFlags.NoEventsView | ViewStateFlags.NoPluginsViews | ViewStateFlags.NoGroupingObjectsViews : ViewStateFlags.ReadOnly | ViewStateFlags.NoContainsInView | ViewStateFlags.NoEventsView | ViewStateFlags.NoPluginsViews | ViewStateFlags.NoGroupingObjectsViews);
  }

  private void _cms_Opening(object sender, CancelEventArgs e)
  {
    if (this._trv.SelectedNode == null || !(this._trv.SelectedNode.Tag is NodeInfo tag))
    {
      this.miFavorites.Enabled = false;
    }
    else
    {
      this.miAddToFavorites.Enabled = !tag.IsFavoritesFolder && !tag.IsCatalog && !this.NodeIsFromFavoritesBranch(this._trv.SelectedNode);
      this.miRemoveFromFavorites.Enabled = this.miFindInTree.Enabled = !tag.IsFavoritesFolder && this.NodeIsFromFavoritesBranch(this._trv.SelectedNode);
      this.miCreateFavorites.Enabled = tag.IsFavoritesFolder || tag.IsCatalog;
      this.miRemoveFavorites.Enabled = tag.IsFavoritesFolder;
      this.miFavorites.Enabled = this.miAddToFavorites.Enabled || this.miRemoveFromFavorites.Enabled || this.miCreateFavorites.Enabled || this.miFindInTree.Enabled || this.miRemoveFavorites.Enabled;
    }
  }

  private void miAddToFavorites_Click(object sender, EventArgs e)
  {
    if (this._trv.SelectedNode == null || !(this._trv.SelectedNode.Tag is NodeInfo tag) || tag.IsFavoritesFolder)
      return;
    this.TopMost = false;
    ImbaseFavoritesCommands.AddToFavoritesCommand(new long[1]
    {
      tag.ObjectId
    });
    this.TopMost = true;
    this.FilterUpdate();
    TreeNode inTreeByObjectId = this.FindInTreeByObjectId(tag.ObjectId, false, this._trv.Nodes);
    if (inTreeByObjectId == null)
      return;
    this._trv.SelectedNode = inTreeByObjectId;
  }

  private void miRemoveFromFavorites_Click(object sender, EventArgs e)
  {
    if (this._trv.SelectedNode == null || !(this._trv.SelectedNode.Tag is NodeInfo tag1) || tag1.IsFavoritesFolder || this._trv.SelectedNode.Parent == null || !(this._trv.SelectedNode.Parent.Tag is NodeInfo tag2) || !tag2.IsFavoritesFolder)
      return;
    ImbaseFavoritesCommands.RemoveFromFavoritesCommand(tag1.ObjectId, tag2.ObjectId);
    this.FilterUpdate();
    TreeNode inTreeByObjectId = this.FindInTreeByObjectId(tag2.ObjectId, true, this._trv.Nodes);
    if (inTreeByObjectId == null)
      return;
    this._trv.SelectedNode = inTreeByObjectId;
    inTreeByObjectId.Expand();
  }

  private void miFindInTree_Click(object sender, EventArgs e)
  {
    if (this._trv.SelectedNode == null || !(this._trv.SelectedNode.Tag is NodeInfo tag))
      return;
    TreeNode inTreeByObjectId = this.FindInTreeByObjectId(tag.ObjectId, false, this._trv.Nodes);
    if (inTreeByObjectId == null)
      return;
    this._trv.SelectedNode = inTreeByObjectId;
  }

  private void miCreateFavorites_Click(object sender, EventArgs e)
  {
    IObjectCreatorService service = ServicesManager.GetService<IObjectCreatorService>();
    if (service == null)
      return;
    this._newRelationId = -1L;
    service.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.OnObjectCreatorDraftCreatedFavoritesEvent);
    long objectByTypeDialog;
    try
    {
      this.TopMost = false;
      objectByTypeDialog = service.CreateObjectByTypeDialog(Intermech.Imbase.Consts.ImbaseFavoritesTypeID);
    }
    finally
    {
      service.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.OnObjectCreatorDraftCreatedFavoritesEvent);
      this.TopMost = true;
    }
    this.FilterUpdate();
    if (objectByTypeDialog == -1L || this._newRelationId == -1L)
      return;
    TreeNode inTreeByObjectId = this.FindInTreeByObjectId(objectByTypeDialog, true, this._trv.Nodes);
    if (inTreeByObjectId == null)
      return;
    this._trv.SelectedNode = inTreeByObjectId;
  }

  private void miRemoveFavorites_Click(object sender, EventArgs e)
  {
    long objectId = 0;
    if (this._trv.SelectedNode == null || !(this._trv.SelectedNode.Tag is NodeInfo tag1) || !tag1.IsFavoritesFolder)
      return;
    if (this._trv.SelectedNode.Parent != null && this._trv.SelectedNode.Parent.Tag is NodeInfo tag2)
      objectId = tag2.ObjectId;
    try
    {
      this.TopMost = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.GetObject(tag1.ObjectId).Delete(0L);
    }
    finally
    {
      this.TopMost = true;
    }
    this.FilterUpdate();
    if (objectId == 0L)
      return;
    TreeNode inTreeByObjectId = this.FindInTreeByObjectId(objectId, true, this._trv.Nodes);
    if (inTreeByObjectId == null)
      return;
    this._trv.SelectedNode = inTreeByObjectId;
  }

  private void OnObjectCreatorDraftCreatedFavoritesEvent(
    object sender,
    AfterDraftCreatedEventArgs e)
  {
    if (this._trv.SelectedNode == null || !(this._trv.SelectedNode.Tag is NodeInfo tag) || e.ObjectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(e.ObjectID, false);
      if (!MetaDataHelper.HasApplicability(tag.TypeId, objectActualCopy.ObjectType, Intermech.Imbase.Consts.ImbaseFavoritesRelationID))
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_7889.ssp_imbase_7890()), (object) objectActualCopy.Caption, (object) this._trv.SelectedNode.Text), LocalizationHolder.rm.GetString("Imbase_CreateRelation_ErrorCaption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
        this._newRelationId = sessionKeeper.Session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseFavoritesRelationID).Create(tag.ObjectId, objectActualCopy.ObjectID).RelationID;
    }
  }

  private void _trv_DragDrop(object sender, DragEventArgs e)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        TreeNode nodeAt = this._trv.GetNodeAt(this._trv.PointToClient(new Point(e.X, e.Y)));
        TreeNode data = (TreeNode) e.Data.GetData(typeof (TreeNode));
        if (nodeAt == null || data == null)
          return;
        TreeNode parent = data.Parent;
        if (data == nodeAt || parent == null || !(nodeAt.Tag is NodeInfo tag1) || !(data.Tag is NodeInfo tag2) || !(parent.Tag is NodeInfo tag3))
          return;
        if (tag2.IsFavoritesFolder && (tag1.IsFavoritesFolder || tag1.IsCatalog))
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(tag3.ObjectId, tag2.ObjectId, true);
          if (relation != null)
          {
            relation.ProjID = tag1.ObjectId;
            data.Remove();
            nodeAt.Nodes.Add(data);
            nodeAt.Expand();
          }
          this._trv.SelectedNode = data;
        }
        else
        {
          if (tag2.IsFavoritesFolder || tag2.IsCatalog || !tag1.IsFavoritesFolder)
            return;
          this.TopMost = false;
          long favorites = ImbaseFavoritesCommands.AddToFavorites(sessionKeeper.Session, tag1.ObjectId, tag2.ObjectId);
          this.FilterUpdate();
          if (favorites != 0L)
          {
            TreeNode inTreeByObjectId = this.FindInTreeByObjectId(favorites, true, this._trv.Nodes);
            if (inTreeByObjectId != null)
              this._trv.SelectedNode = inTreeByObjectId;
          }
          this.TopMost = true;
        }
      }
    }
    catch (Exception ex)
    {
      if (this.TopMost)
        this.TopMost = false;
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void _trv_ItemDrag(object sender, ItemDragEventArgs e)
  {
    int num = (int) this.DoDragDrop(e.Item, DragDropEffects.Move | DragDropEffects.Link);
  }

  private void _trv_DragOver(object sender, DragEventArgs e)
  {
    TreeNode nodeAt = this._trv.GetNodeAt(this._trv.PointToClient(new Point(e.X, e.Y)));
    TreeNode data = (TreeNode) e.Data.GetData(typeof (TreeNode));
    if (nodeAt == null || data == null || !(nodeAt.Tag is NodeInfo tag1) || !(data.Tag is NodeInfo tag2))
      return;
    if (nodeAt == data.Parent || this.CheckForLoop(data, nodeAt))
      e.Effect = DragDropEffects.None;
    else if (tag2.IsFavoritesFolder && (tag1.IsFavoritesFolder || tag1.IsCatalog))
      e.Effect = DragDropEffects.Move;
    else if (!tag2.IsFavoritesFolder && !tag2.IsCatalog && !this.NodeIsFromFavoritesBranch(data) && tag1.IsFavoritesFolder)
      e.Effect = DragDropEffects.Link;
    else
      e.Effect = DragDropEffects.None;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._treeBuilder = (TreeBuilder) null;
      this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseFilterSelectionBaseWindow));
    this._spltContainer = new SplitContainer();
    this._tlp = new TableLayoutPanel();
    this._splitContainerLeft = new SplitContainer();
    this._trv = new TreeView();
    this._cms = new ContextMenuStrip(this.components);
    this._cmmiSearch = new ToolStripMenuItem();
    this._cmmiSearchByName = new ToolStripMenuItem();
    this._cmmiSearchByImg = new ToolStripMenuItem();
    this._cmmiSearchInTbl = new ToolStripMenuItem();
    this._cmmiSearchByIndex = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this._cmmiCollapse = new ToolStripMenuItem();
    this._cmmiUpdate = new ToolStripMenuItem();
    this.miFavorites = new ToolStripMenuItem();
    this.miAddToFavorites = new ToolStripMenuItem();
    this.miRemoveFromFavorites = new ToolStripMenuItem();
    this.miFindInTree = new ToolStripMenuItem();
    this.miCreateFavorites = new ToolStripMenuItem();
    this._tsFilters = new ToolStrip();
    this._tsBtnSearch = new ToolStripDropDownButton();
    this._tsmiSearchByImg = new ToolStripMenuItem();
    this._tsmiSearchByName = new ToolStripMenuItem();
    this._tsmiSearchInTbl = new ToolStripMenuItem();
    this._tsmiSearchByIndex = new ToolStripMenuItem();
    this._tsSeparator = new ToolStripSeparator();
    this._tsBtnFilterSettings = new ToolStripDropDownButton();
    this._tsmiFolderFilterSetup = new ToolStripMenuItem();
    this._tsmiObjFilterSetup = new ToolStripMenuItem();
    this._tsBtnObjFilter = new ToolStripDropDownButton();
    this._tsmiObjFilterNone = new ToolStripMenuItem();
    this.tsmiObjFilterSep1 = new ToolStripSeparator();
    this._tsmiObjFilterCommon = new ToolStripMenuItem();
    this._tsmiObjFilterUser = new ToolStripMenuItem();
    this._tsmiObjFilterArea = new ToolStripMenuItem();
    this._tsmiObjFilterRole = new ToolStripMenuItem();
    this._tsBtnFolderFilter = new ToolStripDropDownButton();
    this._tsmiFolderFilterNone = new ToolStripMenuItem();
    this.tsmiFilterSep1 = new ToolStripSeparator();
    this._tsmiFolderFilterCommon = new ToolStripMenuItem();
    this._tsmiFolderFilterUser = new ToolStripMenuItem();
    this._tsmiFolderFilterArea = new ToolStripMenuItem();
    this._tsmiFolderFilterRole = new ToolStripMenuItem();
    this._txtSearch = new TextBox();
    this._viewsMngr = new PageViewsManager();
    this._lvSearchResult = new ListView();
    this._colText = new ColumnHeader();
    this._imgList = new ImageList(this.components);
    this._pnlBottom = new Panel();
    this._btnCancel = new Button();
    this._btnApply = new Button();
    this._pnlTop = new Panel();
    this._pbObject = new PictureBox();
    this._lblTreePath = new Label();
    this._lbDescription = new Label();
    this._statusStrip = new StatusStrip();
    this._lbWarning = new ToolStripStatusLabel();
    this._timer = new Timer(this.components);
    this._serverTimer = new Timer(this.components);
    this.miRemoveFavorites = new ToolStripMenuItem();
    this._spltContainer.BeginInit();
    this._spltContainer.Panel1.SuspendLayout();
    this._spltContainer.Panel2.SuspendLayout();
    this._spltContainer.SuspendLayout();
    this._tlp.SuspendLayout();
    this._splitContainerLeft.BeginInit();
    this._splitContainerLeft.Panel1.SuspendLayout();
    this._splitContainerLeft.SuspendLayout();
    this._cms.SuspendLayout();
    this._tsFilters.SuspendLayout();
    this._pnlBottom.SuspendLayout();
    this._pnlTop.SuspendLayout();
    ((ISupportInitialize) this._pbObject).BeginInit();
    this._statusStrip.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._spltContainer, "_spltContainer");
    this._spltContainer.Name = "_spltContainer";
    this._spltContainer.Panel1.Controls.Add((Control) this._tlp);
    componentResourceManager.ApplyResources((object) this._spltContainer.Panel1, "_spltContainer.Panel1");
    this._spltContainer.Panel2.Controls.Add((Control) this._viewsMngr);
    componentResourceManager.ApplyResources((object) this._spltContainer.Panel2, "_spltContainer.Panel2");
    componentResourceManager.ApplyResources((object) this._tlp, "_tlp");
    this._tlp.Controls.Add((Control) this._splitContainerLeft, 0, 1);
    this._tlp.Controls.Add((Control) this._tsFilters, 1, 0);
    this._tlp.Controls.Add((Control) this._txtSearch, 0, 0);
    this._tlp.Name = "_tlp";
    this._tlp.TabStop = true;
    this._tlp.SetColumnSpan((Control) this._splitContainerLeft, 2);
    componentResourceManager.ApplyResources((object) this._splitContainerLeft, "_splitContainerLeft");
    this._splitContainerLeft.Name = "_splitContainerLeft";
    this._splitContainerLeft.Panel1.Controls.Add((Control) this._trv);
    this._trv.AllowDrop = true;
    this._trv.ContextMenuStrip = this._cms;
    componentResourceManager.ApplyResources((object) this._trv, "_trv");
    this._trv.HideSelection = false;
    this._trv.ItemHeight = 19;
    this._trv.Name = "_trv";
    this._trv.ItemDrag += new ItemDragEventHandler(this._trv_ItemDrag);
    this._trv.AfterSelect += new TreeViewEventHandler(this.On_trv_AfterSelect);
    this._trv.DragDrop += new DragEventHandler(this._trv_DragDrop);
    this._trv.DragOver += new DragEventHandler(this._trv_DragOver);
    this._cms.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this._cmmiSearch,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this._cmmiCollapse,
      (ToolStripItem) this._cmmiUpdate,
      (ToolStripItem) this.miFavorites
    });
    this._cms.Name = "cmsImbaseTree";
    componentResourceManager.ApplyResources((object) this._cms, "_cms");
    this._cms.Opening += new CancelEventHandler(this._cms_Opening);
    this._cmmiSearch.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._cmmiSearchByName,
      (ToolStripItem) this._cmmiSearchByImg,
      (ToolStripItem) this._cmmiSearchInTbl,
      (ToolStripItem) this._cmmiSearchByIndex
    });
    componentResourceManager.ApplyResources((object) this._cmmiSearch, "_cmmiSearch");
    this._cmmiSearch.Name = "_cmmiSearch";
    this._cmmiSearchByName.Name = "_cmmiSearchByName";
    componentResourceManager.ApplyResources((object) this._cmmiSearchByName, "_cmmiSearchByName");
    this._cmmiSearchByName.Click += new EventHandler(this.On_SearchByName_Click);
    this._cmmiSearchByImg.Name = "_cmmiSearchByImg";
    componentResourceManager.ApplyResources((object) this._cmmiSearchByImg, "_cmmiSearchByImg");
    this._cmmiSearchByImg.Click += new EventHandler(this.On_SearchByImg_Click);
    this._cmmiSearchInTbl.Name = "_cmmiSearchInTbl";
    componentResourceManager.ApplyResources((object) this._cmmiSearchInTbl, "_cmmiSearchInTbl");
    this._cmmiSearchInTbl.Click += new EventHandler(this.On_SearchInTbl_Click);
    this._cmmiSearchByIndex.Name = "_cmmiSearchByIndex";
    componentResourceManager.ApplyResources((object) this._cmmiSearchByIndex, "_cmmiSearchByIndex");
    this._cmmiSearchByIndex.Click += new EventHandler(this.On_SearchByIndex_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    componentResourceManager.ApplyResources((object) this._cmmiCollapse, "_cmmiCollapse");
    this._cmmiCollapse.Name = "_cmmiCollapse";
    this._cmmiCollapse.Click += new EventHandler(this.On_cmmiCollapse_Click);
    componentResourceManager.ApplyResources((object) this._cmmiUpdate, "_cmmiUpdate");
    this._cmmiUpdate.Name = "_cmmiUpdate";
    this._cmmiUpdate.Click += new EventHandler(this.On_cmmiUpdate_Click);
    this.miFavorites.DropDownItems.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.miAddToFavorites,
      (ToolStripItem) this.miRemoveFromFavorites,
      (ToolStripItem) this.miFindInTree,
      (ToolStripItem) this.miCreateFavorites,
      (ToolStripItem) this.miRemoveFavorites
    });
    this.miFavorites.Name = "miFavorites";
    componentResourceManager.ApplyResources((object) this.miFavorites, "miFavorites");
    this.miAddToFavorites.Name = "miAddToFavorites";
    componentResourceManager.ApplyResources((object) this.miAddToFavorites, "miAddToFavorites");
    this.miAddToFavorites.Click += new EventHandler(this.miAddToFavorites_Click);
    this.miRemoveFromFavorites.Name = "miRemoveFromFavorites";
    componentResourceManager.ApplyResources((object) this.miRemoveFromFavorites, "miRemoveFromFavorites");
    this.miRemoveFromFavorites.Click += new EventHandler(this.miRemoveFromFavorites_Click);
    this.miFindInTree.Name = "miFindInTree";
    componentResourceManager.ApplyResources((object) this.miFindInTree, "miFindInTree");
    this.miFindInTree.Click += new EventHandler(this.miFindInTree_Click);
    this.miCreateFavorites.Name = "miCreateFavorites";
    componentResourceManager.ApplyResources((object) this.miCreateFavorites, "miCreateFavorites");
    this.miCreateFavorites.Click += new EventHandler(this.miCreateFavorites_Click);
    componentResourceManager.ApplyResources((object) this._tsFilters, "_tsFilters");
    this._tsFilters.GripStyle = ToolStripGripStyle.Hidden;
    this._tsFilters.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this._tsBtnSearch,
      (ToolStripItem) this._tsSeparator,
      (ToolStripItem) this._tsBtnFilterSettings,
      (ToolStripItem) this._tsBtnObjFilter,
      (ToolStripItem) this._tsBtnFolderFilter
    });
    this._tsFilters.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
    this._tsFilters.Name = "_tsFilters";
    this._tsFilters.TabStop = true;
    this._tsBtnSearch.Alignment = ToolStripItemAlignment.Right;
    this._tsBtnSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._tsBtnSearch.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._tsmiSearchByImg,
      (ToolStripItem) this._tsmiSearchByName,
      (ToolStripItem) this._tsmiSearchInTbl,
      (ToolStripItem) this._tsmiSearchByIndex
    });
    componentResourceManager.ApplyResources((object) this._tsBtnSearch, "_tsBtnSearch");
    this._tsBtnSearch.Name = "_tsBtnSearch";
    this._tsBtnSearch.Click += new EventHandler(this.On_tsBtn_Click);
    this._tsmiSearchByImg.Name = "_tsmiSearchByImg";
    componentResourceManager.ApplyResources((object) this._tsmiSearchByImg, "_tsmiSearchByImg");
    this._tsmiSearchByImg.Click += new EventHandler(this.On_SearchByImg_Click);
    this._tsmiSearchByName.Name = "_tsmiSearchByName";
    componentResourceManager.ApplyResources((object) this._tsmiSearchByName, "_tsmiSearchByName");
    this._tsmiSearchByName.Click += new EventHandler(this.On_SearchByName_Click);
    this._tsmiSearchInTbl.Name = "_tsmiSearchInTbl";
    componentResourceManager.ApplyResources((object) this._tsmiSearchInTbl, "_tsmiSearchInTbl");
    this._tsmiSearchInTbl.Click += new EventHandler(this.On_SearchInTbl_Click);
    this._tsmiSearchByIndex.Name = "_tsmiSearchByIndex";
    componentResourceManager.ApplyResources((object) this._tsmiSearchByIndex, "_tsmiSearchByIndex");
    this._tsmiSearchByIndex.Click += new EventHandler(this.On_SearchByIndex_Click);
    this._tsSeparator.Alignment = ToolStripItemAlignment.Right;
    this._tsSeparator.Name = "_tsSeparator";
    componentResourceManager.ApplyResources((object) this._tsSeparator, "_tsSeparator");
    this._tsBtnFilterSettings.Alignment = ToolStripItemAlignment.Right;
    this._tsBtnFilterSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._tsBtnFilterSettings.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._tsmiFolderFilterSetup,
      (ToolStripItem) this._tsmiObjFilterSetup
    });
    componentResourceManager.ApplyResources((object) this._tsBtnFilterSettings, "_tsBtnFilterSettings");
    this._tsBtnFilterSettings.Name = "_tsBtnFilterSettings";
    this._tsBtnFilterSettings.Click += new EventHandler(this.On_tsBtn_Click);
    this._tsmiFolderFilterSetup.Name = "_tsmiFolderFilterSetup";
    componentResourceManager.ApplyResources((object) this._tsmiFolderFilterSetup, "_tsmiFolderFilterSetup");
    this._tsmiFolderFilterSetup.Click += new EventHandler(this.On_tsmiFolderFilterSetup_Click);
    this._tsmiObjFilterSetup.Name = "_tsmiObjFilterSetup";
    componentResourceManager.ApplyResources((object) this._tsmiObjFilterSetup, "_tsmiObjFilterSetup");
    this._tsmiObjFilterSetup.Click += new EventHandler(this.On_tsmiObjFilterSetup_Click);
    this._tsBtnObjFilter.Alignment = ToolStripItemAlignment.Right;
    this._tsBtnObjFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._tsBtnObjFilter.DropDownItems.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this._tsmiObjFilterNone,
      (ToolStripItem) this.tsmiObjFilterSep1,
      (ToolStripItem) this._tsmiObjFilterCommon,
      (ToolStripItem) this._tsmiObjFilterUser,
      (ToolStripItem) this._tsmiObjFilterArea,
      (ToolStripItem) this._tsmiObjFilterRole
    });
    componentResourceManager.ApplyResources((object) this._tsBtnObjFilter, "_tsBtnObjFilter");
    this._tsBtnObjFilter.Name = "_tsBtnObjFilter";
    this._tsBtnObjFilter.Click += new EventHandler(this.On_tsBtn_Click);
    this._tsmiObjFilterNone.Checked = true;
    this._tsmiObjFilterNone.CheckState = CheckState.Checked;
    this._tsmiObjFilterNone.Name = "_tsmiObjFilterNone";
    componentResourceManager.ApplyResources((object) this._tsmiObjFilterNone, "_tsmiObjFilterNone");
    this._tsmiObjFilterNone.Click += new EventHandler(this.On_tsmiObjFilter_Click);
    this.tsmiObjFilterSep1.Name = "tsmiObjFilterSep1";
    componentResourceManager.ApplyResources((object) this.tsmiObjFilterSep1, "tsmiObjFilterSep1");
    this._tsmiObjFilterCommon.Name = "_tsmiObjFilterCommon";
    componentResourceManager.ApplyResources((object) this._tsmiObjFilterCommon, "_tsmiObjFilterCommon");
    this._tsmiObjFilterUser.Name = "_tsmiObjFilterUser";
    componentResourceManager.ApplyResources((object) this._tsmiObjFilterUser, "_tsmiObjFilterUser");
    this._tsmiObjFilterArea.Name = "_tsmiObjFilterArea";
    componentResourceManager.ApplyResources((object) this._tsmiObjFilterArea, "_tsmiObjFilterArea");
    this._tsmiObjFilterRole.Name = "_tsmiObjFilterRole";
    componentResourceManager.ApplyResources((object) this._tsmiObjFilterRole, "_tsmiObjFilterRole");
    this._tsBtnFolderFilter.Alignment = ToolStripItemAlignment.Right;
    this._tsBtnFolderFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._tsBtnFolderFilter.DropDownItems.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this._tsmiFolderFilterNone,
      (ToolStripItem) this.tsmiFilterSep1,
      (ToolStripItem) this._tsmiFolderFilterCommon,
      (ToolStripItem) this._tsmiFolderFilterUser,
      (ToolStripItem) this._tsmiFolderFilterArea,
      (ToolStripItem) this._tsmiFolderFilterRole
    });
    componentResourceManager.ApplyResources((object) this._tsBtnFolderFilter, "_tsBtnFolderFilter");
    this._tsBtnFolderFilter.Name = "_tsBtnFolderFilter";
    this._tsBtnFolderFilter.Click += new EventHandler(this.On_tsBtn_Click);
    this._tsmiFolderFilterNone.Checked = true;
    this._tsmiFolderFilterNone.CheckOnClick = true;
    this._tsmiFolderFilterNone.CheckState = CheckState.Checked;
    this._tsmiFolderFilterNone.Name = "_tsmiFolderFilterNone";
    componentResourceManager.ApplyResources((object) this._tsmiFolderFilterNone, "_tsmiFolderFilterNone");
    this._tsmiFolderFilterNone.Click += new EventHandler(this.On_tsmiFolderFilter_Click);
    this.tsmiFilterSep1.Name = "tsmiFilterSep1";
    componentResourceManager.ApplyResources((object) this.tsmiFilterSep1, "tsmiFilterSep1");
    this._tsmiFolderFilterCommon.CheckOnClick = true;
    this._tsmiFolderFilterCommon.Name = "_tsmiFolderFilterCommon";
    componentResourceManager.ApplyResources((object) this._tsmiFolderFilterCommon, "_tsmiFolderFilterCommon");
    this._tsmiFolderFilterCommon.Click += new EventHandler(this.On_tsmiFolderFilter_Click);
    this._tsmiFolderFilterUser.CheckOnClick = true;
    this._tsmiFolderFilterUser.Name = "_tsmiFolderFilterUser";
    componentResourceManager.ApplyResources((object) this._tsmiFolderFilterUser, "_tsmiFolderFilterUser");
    this._tsmiFolderFilterUser.Click += new EventHandler(this.On_tsmiFolderFilter_Click);
    this._tsmiFolderFilterArea.CheckOnClick = true;
    this._tsmiFolderFilterArea.Name = "_tsmiFolderFilterArea";
    componentResourceManager.ApplyResources((object) this._tsmiFolderFilterArea, "_tsmiFolderFilterArea");
    this._tsmiFolderFilterArea.Click += new EventHandler(this.On_tsmiFolderFilter_Click);
    this._tsmiFolderFilterRole.CheckOnClick = true;
    this._tsmiFolderFilterRole.Name = "_tsmiFolderFilterRole";
    componentResourceManager.ApplyResources((object) this._tsmiFolderFilterRole, "_tsmiFolderFilterRole");
    this._tsmiFolderFilterRole.Click += new EventHandler(this.On_tsmiFolderFilter_Click);
    componentResourceManager.ApplyResources((object) this._txtSearch, "_txtSearch");
    this._txtSearch.Name = "_txtSearch";
    this._txtSearch.TextChanged += new EventHandler(this.On_txtSearch_TextChanged);
    this._txtSearch.Enter += new EventHandler(this.On_txtSearch_Enter);
    this._txtSearch.KeyDown += new KeyEventHandler(this.On_txtSearch_KeyDown);
    this._txtSearch.Leave += new EventHandler(this.On_SearchControls_Leave);
    this._viewsMngr.ActiveViewPage = (IViewPage) null;
    this._viewsMngr.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this._viewsMngr, "_viewsMngr");
    this._viewsMngr.Name = "_viewsMngr";
    this._lvSearchResult.Columns.AddRange(new ColumnHeader[1]
    {
      this._colText
    });
    this._lvSearchResult.FullRowSelect = true;
    this._lvSearchResult.HeaderStyle = ColumnHeaderStyle.None;
    this._lvSearchResult.HideSelection = false;
    componentResourceManager.ApplyResources((object) this._lvSearchResult, "_lvSearchResult");
    this._lvSearchResult.MultiSelect = false;
    this._lvSearchResult.Name = "_lvSearchResult";
    this._lvSearchResult.Sorting = SortOrder.Ascending;
    this._lvSearchResult.TabStop = false;
    this._lvSearchResult.UseCompatibleStateImageBehavior = false;
    this._lvSearchResult.View = View.Details;
    this._lvSearchResult.DoubleClick += new EventHandler(this.On_lvSearchResult_DoubleClick);
    this._lvSearchResult.Enter += new EventHandler(this.On_lvSearchResult_Enter);
    this._lvSearchResult.KeyDown += new KeyEventHandler(this.On_lvSearchResult_KeyDown);
    this._lvSearchResult.Leave += new EventHandler(this.On_SearchControls_Leave);
    componentResourceManager.ApplyResources((object) this._colText, "_colText");
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "FolderFilterFree");
    this._imgList.Images.SetKeyName(1, "FolderFilterSet");
    this._imgList.Images.SetKeyName(2, "ObjectFilterFree.png");
    this._imgList.Images.SetKeyName(3, "ObjectFilterSet");
    this._imgList.Images.SetKeyName(4, "FilterSettings");
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Controls.Add((Control) this._btnApply);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    componentResourceManager.ApplyResources((object) this._btnApply, "_btnApply");
    this._btnApply.DialogResult = DialogResult.OK;
    this._btnApply.Name = "_btnApply";
    this._pnlTop.Controls.Add((Control) this._pbObject);
    this._pnlTop.Controls.Add((Control) this._lblTreePath);
    this._pnlTop.Controls.Add((Control) this._lbDescription);
    componentResourceManager.ApplyResources((object) this._pnlTop, "_pnlTop");
    this._pnlTop.Name = "_pnlTop";
    componentResourceManager.ApplyResources((object) this._pbObject, "_pbObject");
    this._pbObject.Name = "_pbObject";
    this._pbObject.TabStop = false;
    componentResourceManager.ApplyResources((object) this._lblTreePath, "_lblTreePath");
    this._lblTreePath.AutoEllipsis = true;
    this._lblTreePath.Name = "_lblTreePath";
    componentResourceManager.ApplyResources((object) this._lbDescription, "_lbDescription");
    this._lbDescription.Name = "_lbDescription";
    this._statusStrip.GripStyle = ToolStripGripStyle.Visible;
    this._statusStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this._lbWarning
    });
    componentResourceManager.ApplyResources((object) this._statusStrip, "_statusStrip");
    this._statusStrip.Name = "_statusStrip";
    componentResourceManager.ApplyResources((object) this._lbWarning, "_lbWarning");
    this._lbWarning.Name = "_lbWarning";
    this._timer.Tick += new EventHandler(this.On_timer_Tick);
    this._serverTimer.Tick += new EventHandler(this.On_serverTimer_Tick);
    this.miRemoveFavorites.Name = "miRemoveFavorites";
    componentResourceManager.ApplyResources((object) this.miRemoveFavorites, "miRemoveFavorites");
    this.miRemoveFavorites.Click += new EventHandler(this.miRemoveFavorites_Click);
    this.AcceptButton = (IButtonControl) this._btnApply;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._lvSearchResult);
    this.Controls.Add((Control) this._spltContainer);
    this.Controls.Add((Control) this._pnlTop);
    this.Controls.Add((Control) this._pnlBottom);
    this.Controls.Add((Control) this._statusStrip);
    this.DoubleBuffered = true;
    this.Name = nameof (ImbaseFilterSelectionBaseWindow);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._spltContainer.Panel1.ResumeLayout(false);
    this._spltContainer.Panel2.ResumeLayout(false);
    this._spltContainer.EndInit();
    this._spltContainer.ResumeLayout(false);
    this._tlp.ResumeLayout(false);
    this._tlp.PerformLayout();
    this._splitContainerLeft.Panel1.ResumeLayout(false);
    this._splitContainerLeft.EndInit();
    this._splitContainerLeft.ResumeLayout(false);
    this._cms.ResumeLayout(false);
    this._tsFilters.ResumeLayout(false);
    this._tsFilters.PerformLayout();
    this._pnlBottom.ResumeLayout(false);
    this._pnlTop.ResumeLayout(false);
    ((ISupportInitialize) this._pbObject).EndInit();
    this._statusStrip.ResumeLayout(false);
    this._statusStrip.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  protected enum ImFilterMode
  {
    None,
    Folder,
    Object,
  }
}
