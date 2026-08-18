// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.FindByImagesView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Bars;
using Intermech.Client.Core.Thumbnail;
using Intermech.Controls.Thumbnail;
using Intermech.Docking;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class FindByImagesView : DockControl, IImbaseView
{
  private const string GUID = "19A8B05B-FCB7-48e4-8410-000000000000";
  private long _targetId;
  private NavigatorTreeNode _parentINode;
  private TreeNode _parentTNode;
  private LocateNodeEventHandler _locateHandler;
  private DataTable _dtFolders = new DataTable();
  private DataTable _dtLinks = new DataTable();
  private IPicturesCache _cache;
  private Icon _ico;
  private ThumbnailsDictionary _thFoldersDict = new ThumbnailsDictionary();
  private ThumbnailsDictionary _thLinksDict = new ThumbnailsDictionary();
  private Dictionary<long, ArrayList> _items = new Dictionary<long, ArrayList>();
  private long _selectedImage = -1;
  private IPopupMenuHost _host;
  private INamedImageList _nil;
  private ThumbnailItem _selectedItem;
  private ContextMenuBarItem _contextMenu;
  private IContainer components;
  private ThumbnailGrid _view;
  private Tab tab3;
  private Tab tab2;
  private Tab tab1;
  private Intermech.Docking.TabControl _pager;
  private SplitContainer splContainer;
  private ImageList imgList;
  private DataGridView dgvItems;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn colObjID;
  private DataGridViewImageColumn Image;
  private DataGridViewTextBoxColumn AttrName;
  private ContextMenuStrip cmView;

  public Icon Icon
  {
    get
    {
      if (this._ico != null)
        return this._ico;
      this._ico = Intermech.Imbase.ResourceHelper.GetResourceData<Icon>(this.GetType().Assembly, "Intermech.Imbase.Resources.FindByImage.ico");
      return this._ico;
    }
  }

  public override string HelpID => "901";

  public FindByImagesView()
  {
    this.InitializeComponent();
    this.Guid = FindByImagesView.CreateGuid();
    this.Text = LocalizationHolder.rm.GetString("Imbase.FindByImagesView.DialogCaption");
    this._cache = ServicesManager.GetService(typeof (IPicturesCache)) as IPicturesCache;
    this._nil = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    this.TabImageIndex = this._nil != null ? this._nil.ImageIndex("imgFindByImages") : -1;
    this._pager.SelectedTab = this._pager.Tabs[0];
    HelpProvidersClass.SetHelpOptionForControl((Control) this, this.HelpID);
    this._host = ServicesManager.GetService(typeof (IPopupMenuHost)) as IPopupMenuHost;
    this._contextMenu = this.GetContextMenu();
  }

  internal static Guid CreateGuid() => Guid.NewGuid();

  public static void Show(object parentNode, bool modal, LocateNodeEventHandler locateHandler)
  {
    FindByImagesView view = new FindByImagesView();
    view.SetData(parentNode, locateHandler);
    if (modal)
    {
      ImbaseViewForm.FindOrCreateViewForm(ImbaseViewForm.FormType.FindByImage, (IImbaseView) view, view.Icon).Show();
    }
    else
    {
      if (!(ServicesManager.GetService(typeof (DockManager)) is DockManager service))
        return;
      view.Show(service);
      view.Activate();
    }
  }

  private DataTable GetFoldersFromFavorites(IUserSession session)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFavoritesTypeID);
    ConditionStructure conditionStructure1 = new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) this._targetId, LogicalOperators.NONE, 0, true);
    ConditionStructure conditionStructure2 = new ConditionStructure(Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID, RelationalOperators.NotEmpty, (object) 0, LogicalOperators.NONE, 0, true);
    DataTable foldersFromFavorites = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure1
    }, new object[5]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) ObligatoryObjectAttributes.CAPTION,
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
      (object) Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID
    })
    {
      Contents = new ColumnContents[5]
      {
        ColumnContents.ID,
        ColumnContents.ID,
        ColumnContents.Text,
        ColumnContents.Text,
        ColumnContents.ID
      }
    });
    foldersFromFavorites.Columns[4].ColumnName = "ImageID";
    DataRow[] dataRowArray = foldersFromFavorites.Select("ImageID is Null");
    if (dataRowArray.Length != 0)
    {
      for (int index = 0; index < dataRowArray.Length; ++index)
        foldersFromFavorites.Rows.Remove(dataRowArray[index]);
    }
    return foldersFromFavorites;
  }

  private DataTable GetFolders(IUserSession session, string strKey)
  {
    DataTable folders = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID).Select(new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) strKey, LogicalOperators.AND, 0, true),
      new ConditionStructure(Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID, RelationalOperators.NotEmpty, (object) 0, LogicalOperators.NONE, 0, true)
    }, new object[5]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) ObligatoryObjectAttributes.CAPTION,
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
      (object) Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID
    })
    {
      Contents = new ColumnContents[5]
      {
        ColumnContents.ID,
        ColumnContents.ID,
        ColumnContents.Text,
        ColumnContents.Text,
        ColumnContents.ID
      }
    });
    folders.Columns[4].ColumnName = "ImageID";
    DataRow[] dataRowArray = folders.Select("ImageID is Null");
    if (dataRowArray.Length != 0)
    {
      for (int index = 0; index < dataRowArray.Length; ++index)
        folders.Rows.Remove(dataRowArray[index]);
    }
    return folders;
  }

  private DataTable GetLinks(IUserSession session, string strKey)
  {
    IDBObjectCollection objectCollection1 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    ConditionStructure conditionStructure = new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) strKey, LogicalOperators.NONE, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[6]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) ObligatoryObjectAttributes.CAPTION,
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
      (object) Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID,
      (object) Intermech.Imbase.Consts.ImbaseTableRefAttID
    });
    paramSet.Contents = new ColumnContents[6]
    {
      ColumnContents.ID,
      ColumnContents.ID,
      ColumnContents.Text,
      ColumnContents.Text,
      ColumnContents.ID,
      ColumnContents.ID
    };
    DataTable links = objectCollection1.Select(paramSet);
    if (links.Rows.Count == 0)
      return links;
    links.Columns[4].ColumnName = "ImageID";
    DataRow[] dataRowArray = links.Select("ImageID is Null");
    if (dataRowArray.Length == 0)
      return links;
    IDBObjectCollection objectCollection2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableTypeID);
    conditionStructure = new ConditionStructure(Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, true);
    paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID
    });
    paramSet.Contents = new ColumnContents[2]
    {
      ColumnContents.ID,
      ColumnContents.ID
    };
    DataTable dataTable = objectCollection2.Select(paramSet);
    dataTable.Columns[0].ColumnName = "TableID";
    dataTable.PrimaryKey = new DataColumn[1]
    {
      dataTable.Columns[0]
    };
    for (int index = 0; index < dataRowArray.Length; ++index)
    {
      DataRow dataRow = dataTable.Rows.Find(dataRowArray[index][5]);
      if (dataRow == null)
        links.Rows.Remove(dataRowArray[index]);
      else
        dataRowArray[index][4] = dataRow[1];
    }
    return links;
  }

  private ThumbnailsDictionary MergeDictionaries(ThumbnailsDictionary[] dicts)
  {
    ThumbnailsDictionary thumbnailsDictionary = new ThumbnailsDictionary();
    for (int index = 0; index < dicts.Length; ++index)
      thumbnailsDictionary.Add(dicts[index]);
    return thumbnailsDictionary;
  }

  private void On_pager_SelectedTabChanged(object sender, EventArgs e)
  {
    ThumbnailsDictionary thumbnailsDictionary = (ThumbnailsDictionary) null;
    switch ((sender as Intermech.Docking.TabControl).SelectedTab.Index)
    {
      case 0:
        thumbnailsDictionary = this.MergeDictionaries(new ThumbnailsDictionary[2]
        {
          this._thFoldersDict,
          this._thLinksDict
        });
        break;
      case 1:
        thumbnailsDictionary = this._thFoldersDict;
        break;
      case 2:
        thumbnailsDictionary = this._thLinksDict;
        break;
    }
    if (thumbnailsDictionary != null)
    {
      this._view.Count = thumbnailsDictionary.Count;
      if (this._view.Renderer != null)
        (this._view.Renderer as IMBASERender).ThDictionary = thumbnailsDictionary;
    }
    this.UpdateListItems(0);
    this.Invalidate();
  }

  private void On_view_SelectionChanged(object sender, int oldIndex, int newIndex)
  {
    this.UpdateListItems(newIndex);
  }

  private void OnBeforeFirstShown(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._targetId);
      if (dbObject == null)
        return;
      IUserSession session = sessionKeeper.Session;
      if (dbObject.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
        this._dtFolders = this.GetFoldersFromFavorites(session);
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
      if (attributeById == null || attributeById.Value == null)
        return;
      string strKey = attributeById.Value.ToString();
      this._dtFolders = this.GetFolders(session, strKey);
      foreach (DataRow row in (InternalDataCollectionBase) this._dtFolders.Rows)
      {
        this._thFoldersDict.Add(Convert.ToInt64(row[4]), Convert.ToInt32(row[1]));
        if (this._items.ContainsKey(Convert.ToInt64(row[4])))
          this._items[Convert.ToInt64(row[4])].Add((object) new Elements(row[2].ToString(), Convert.ToInt64(row[0]), Convert.ToInt32(row[1])));
        else
          this._items.Add(Convert.ToInt64(row[4]), new ArrayList()
          {
            (object) new Elements(row[2].ToString(), Convert.ToInt64(row[0]), Convert.ToInt32(row[1]))
          });
      }
      this._dtLinks = this.GetLinks(session, strKey);
      foreach (DataRow row in (InternalDataCollectionBase) this._dtLinks.Rows)
      {
        this._thLinksDict.Add(Convert.ToInt64(row[4]), Convert.ToInt32(row[1]));
        if (this._items.ContainsKey(Convert.ToInt64(row[4])))
          this._items[Convert.ToInt64(row[4])].Add((object) new Elements(row[2].ToString(), Convert.ToInt64(row[0]), Convert.ToInt32(row[1])));
        else
          this._items.Add(Convert.ToInt64(row[4]), new ArrayList()
          {
            (object) new Elements(row[2].ToString(), Convert.ToInt64(row[0]), Convert.ToInt32(row[1]))
          });
      }
      ThumbnailsDictionary thDict = this.MergeDictionaries(new ThumbnailsDictionary[2]
      {
        this._thFoldersDict,
        this._thLinksDict
      });
      this._view.Count = thDict.Count;
      this._view.Renderer = (IThumbnailRenderer) new IMBASERender(this.Font, thDict, new GetImageHandler(this.OnGetImage));
      this.UpdateListItems(0);
    }
  }

  private void OndgvItems_SelectionChanged(object sender, EventArgs e)
  {
    if ((sender as DataGridView).SelectedRows.Count == 0)
      return;
    long int64 = Convert.ToInt64((sender as DataGridView).SelectedRows[0].Cells["colObjID"].Value);
    if (this._locateHandler != null)
      this._locateHandler((object) this, new LocateNodeEventArgs(int64, FindHelper.GetDataTable(int64)));
    else if (this._parentINode != null)
    {
      NavigatorTreeNode node = FindHelper.SearchNodeByNodeID(this._parentINode, int64);
      if (FindHelper.IsValidNode(node))
        node.Focus();
    }
    if (this._parentTNode == null)
      return;
    TreeNode treeNode = FindHelper.SearchNodeByNodeID(this._parentTNode, int64);
    if (treeNode == null)
      return;
    treeNode.EnsureVisible();
    treeNode.TreeView.SelectedNode = treeNode;
  }

  private object OnGetImage(int imageIndex)
  {
    ThumbnailItem thumbnailItem = new ThumbnailItem();
    switch (this._pager.SelectedTab.Index)
    {
      case 0:
        ThumbnailsDictionary thumbnailsDictionary = this.MergeDictionaries(new ThumbnailsDictionary[2]
        {
          this._thFoldersDict,
          this._thLinksDict
        });
        if (thumbnailsDictionary.Count > imageIndex)
        {
          thumbnailItem = thumbnailsDictionary[imageIndex];
          break;
        }
        break;
      case 1:
        if (this._thFoldersDict.Count > imageIndex)
        {
          thumbnailItem = this._thFoldersDict[imageIndex];
          break;
        }
        break;
      case 2:
        if (this._thLinksDict.Count > imageIndex)
        {
          thumbnailItem = this._thLinksDict[imageIndex];
          break;
        }
        break;
      default:
        thumbnailItem = new ThumbnailItem();
        break;
    }
    if (thumbnailItem.Image == null && this._cache != null)
      thumbnailItem.Image = this._cache.GetPicture(Intermech.Client.Core.Thumbnail.Consts.ImageLibraryItemTypeID, thumbnailItem.ImageID, out long _);
    return thumbnailItem.Image;
  }

  private void SetData(object parentNode, LocateNodeEventHandler locateHandler)
  {
    this._locateHandler = locateHandler;
    switch (parentNode)
    {
      case NavigatorTreeNode navigatorTreeNode:
        this._targetId = (navigatorTreeNode.NodeID as NodeID).ObjectID;
        this._parentINode = navigatorTreeNode;
        break;
      case TreeNode treeNode:
        if (!(treeNode.Tag is NodeInfo tag))
          break;
        this._targetId = tag.ObjectId;
        this._parentTNode = treeNode;
        break;
    }
  }

  private void UpdateListItems(int index)
  {
    int num = 0;
    int index1 = -1;
    this.dgvItems.Rows.Clear();
    switch (this._pager.SelectedTab.Index)
    {
      case 0:
        ThumbnailsDictionary thumbnailsDictionary = this.MergeDictionaries(new ThumbnailsDictionary[2]
        {
          this._thFoldersDict,
          this._thLinksDict
        });
        if (thumbnailsDictionary.Count <= index)
          return;
        this._selectedImage = thumbnailsDictionary[index].ImageID;
        break;
      case 1:
        if (this._thFoldersDict.Count <= index)
          return;
        this._selectedImage = this._thFoldersDict[index].ImageID;
        num = Intermech.Imbase.Consts.ImbaseFolderTypeID;
        index1 = 0;
        break;
      case 2:
        if (this._thLinksDict.Count <= index)
          return;
        this._selectedImage = this._thLinksDict[index].ImageID;
        num = Intermech.Imbase.Consts.ImbaseTableRefTypeID;
        index1 = 1;
        break;
    }
    if (this._items.ContainsKey(this._selectedImage))
    {
      ArrayList arrayList = this._items[this._selectedImage];
      arrayList.Sort();
      if (num == 0)
      {
        for (int index2 = 0; index2 < arrayList.Count; ++index2)
        {
          Elements elements = (Elements) arrayList[index2];
          if (elements.TypeID != Intermech.Imbase.Consts.ImbaseFolderTypeID)
          {
            elements = (Elements) arrayList[index2];
            if (elements.TypeID != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
              continue;
          }
          DataGridViewRowCollection rows = this.dgvItems.Rows;
          object[] objArray1 = new object[3];
          object[] objArray2 = objArray1;
          elements = (Elements) arrayList[index2];
          // ISSUE: variable of a boxed type
          __Boxed<long> objId = (System.ValueType) elements.ObjID;
          objArray2[0] = (object) objId;
          elements = (Elements) arrayList[index2];
          objArray1[1] = elements.TypeID != Intermech.Imbase.Consts.ImbaseFolderTypeID ? (object) this.imgList.Images[1] : (object) this.imgList.Images[0];
          object[] objArray3 = objArray1;
          elements = (Elements) arrayList[index2];
          string caption = elements.Caption;
          objArray3[2] = (object) caption;
          object[] objArray4 = objArray1;
          rows.Add(objArray4);
        }
      }
      else
      {
        for (int index3 = 0; index3 < arrayList.Count; ++index3)
        {
          Elements elements = (Elements) arrayList[index3];
          if (elements.TypeID == num)
          {
            DataGridViewRowCollection rows = this.dgvItems.Rows;
            object[] objArray = new object[3];
            elements = (Elements) arrayList[index3];
            objArray[0] = (object) elements.ObjID;
            objArray[1] = (object) this.imgList.Images[index1];
            elements = (Elements) arrayList[index3];
            objArray[2] = (object) elements.Caption;
            rows.Add(objArray);
          }
        }
      }
    }
    this.dgvItems.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
  }

  private void _view_ShowContextMenu(object sender, ThumbnailEventArgs e)
  {
    int itemIndex = e.ItemIndex;
    switch (this._pager.SelectedTab.Index)
    {
      case 0:
        ThumbnailsDictionary thumbnailsDictionary = this.MergeDictionaries(new ThumbnailsDictionary[2]
        {
          this._thFoldersDict,
          this._thLinksDict
        });
        if (thumbnailsDictionary.Count > 0)
        {
          this._selectedItem = thumbnailsDictionary[itemIndex];
          break;
        }
        break;
      case 1:
        if (this._thFoldersDict.Count > 0)
        {
          this._selectedItem = this._thFoldersDict[itemIndex];
          break;
        }
        break;
      case 2:
        if (this._thLinksDict.Count > 0)
        {
          this._selectedItem = this._thLinksDict[itemIndex];
          break;
        }
        break;
      default:
        this._selectedItem = new ThumbnailItem();
        break;
    }
    if (this._selectedItem.ImageID == 0L)
      return;
    this._contextMenu.Show(this._host, (Control) this, e.Pos);
  }

  private ContextMenuBarItem GetContextMenu()
  {
    ContextMenuBarItem contextMenu = new ContextMenuBarItem();
    int imageIndex = this._nil.ImageIndex("imgView");
    MenuButtonItem menuButtonItem = new MenuButtonItem(LocalizationHolder.rm.GetString("Imbase_View"), imageIndex);
    menuButtonItem.Click += (EventHandler) ((sender, e) => this.ShowImageEvent(sender, e, this._selectedItem));
    contextMenu.Items.Add((ToolbarItemBase) menuButtonItem);
    return contextMenu;
  }

  private void ShowImageEvent(object sender, EventArgs eventArgs, ThumbnailItem item)
  {
    FullImageView.ShowImage((object) item.ImageID);
  }

  public void FirstShown(object sender, EventArgs e) => this.OnBeforeFirstShown(sender, e);

  public void ViewClosing(object sender, CancelEventArgs e)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._ico?.Dispose();
      this._ico = (Icon) null;
      this._contextMenu?.Dispose();
      this._contextMenu = (ContextMenuBarItem) null;
      this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FindByImagesView));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
    this.splContainer = new SplitContainer();
    this._view = new ThumbnailGrid();
    this._pager = new Intermech.Docking.TabControl();
    this.tab1 = new Tab();
    this.tab2 = new Tab();
    this.tab3 = new Tab();
    this.dgvItems = new DataGridView();
    this.colObjID = new DataGridViewTextBoxColumn();
    this.Image = new DataGridViewImageColumn();
    this.AttrName = new DataGridViewTextBoxColumn();
    this.imgList = new ImageList();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.cmView = new ContextMenuStrip();
    this.splContainer.BeginInit();
    this.splContainer.Panel1.SuspendLayout();
    this.splContainer.Panel2.SuspendLayout();
    this.splContainer.SuspendLayout();
    ((ISupportInitialize) this.dgvItems).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splContainer, "splContainer");
    this.splContainer.Name = "splContainer";
    this.splContainer.Panel1.Controls.Add((Control) this._view);
    this.splContainer.Panel1.Controls.Add((Control) this._pager);
    this.splContainer.Panel2.Controls.Add((Control) this.dgvItems);
    this._view.Count = 2;
    componentResourceManager.ApplyResources((object) this._view, "_view");
    this._view.ItemIndex = 0;
    this._view.Name = "_view";
    this._view.PanelSize = new Size(150, 120);
    this._view.Renderer = (IThumbnailRenderer) null;
    this._view.ShowContextMenu += new ThumbnailEventHandler(this._view_ShowContextMenu);
    this._view.SelectionChanged += new SelectionChangedEventHandler(this.On_view_SelectionChanged);
    this._pager.BorderStyle = Intermech.Docking.Rendering.BorderStyle.None;
    componentResourceManager.ApplyResources((object) this._pager, "_pager");
    this._pager.Name = "_pager";
    this._pager.TabAlignment = Intermech.Docking.TabAlignment.Bottom;
    this._pager.TabLayout = TabLayout.SingleLineFixed;
    this._pager.Tabs.AddRange(new Tab[3]
    {
      this.tab1,
      this.tab2,
      this.tab3
    });
    this._pager.SelectedTabChanged += new EventHandler(this.On_pager_SelectedTabChanged);
    this.tab1.Index = 0;
    componentResourceManager.ApplyResources((object) this.tab1, "tab1");
    this.tab2.Index = 1;
    componentResourceManager.ApplyResources((object) this.tab2, "tab2");
    this.tab3.Index = 2;
    componentResourceManager.ApplyResources((object) this.tab3, "tab3");
    this.dgvItems.AllowUserToAddRows = false;
    this.dgvItems.AllowUserToDeleteRows = false;
    this.dgvItems.AllowUserToResizeColumns = false;
    this.dgvItems.AllowUserToResizeRows = false;
    this.dgvItems.BackgroundColor = SystemColors.Window;
    this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.dgvItems.CellBorderStyle = DataGridViewCellBorderStyle.None;
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle1.BackColor = SystemColors.Control;
    gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle1.ForeColor = SystemColors.WindowText;
    gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
    this.dgvItems.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
    this.dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgvItems.Columns.AddRange((DataGridViewColumn) this.colObjID, (DataGridViewColumn) this.Image, (DataGridViewColumn) this.AttrName);
    componentResourceManager.ApplyResources((object) this.dgvItems, "dgvItems");
    this.dgvItems.MultiSelect = false;
    this.dgvItems.Name = "dgvItems";
    this.dgvItems.RowHeadersVisible = false;
    this.dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgvItems.SelectionChanged += new EventHandler(this.OndgvItems_SelectionChanged);
    componentResourceManager.ApplyResources((object) this.colObjID, "colObjID");
    this.colObjID.Name = "colObjID";
    this.colObjID.Resizable = DataGridViewTriState.False;
    this.colObjID.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Image.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle2.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle2.NullValue");
    gridViewCellStyle2.Padding = new Padding(3, 0, 3, 0);
    this.Image.DefaultCellStyle = gridViewCellStyle2;
    componentResourceManager.ApplyResources((object) this.Image, "Image");
    this.Image.Name = "Image";
    this.Image.ReadOnly = true;
    this.Image.Resizable = DataGridViewTriState.False;
    this.AttrName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    gridViewCellStyle3.WrapMode = DataGridViewTriState.False;
    this.AttrName.DefaultCellStyle = gridViewCellStyle3;
    componentResourceManager.ApplyResources((object) this.AttrName, "AttrName");
    this.AttrName.Name = "AttrName";
    this.AttrName.ReadOnly = true;
    this.imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imgList.ImageStream");
    this.imgList.TransparentColor = Color.Transparent;
    this.imgList.Images.SetKeyName(0, "Folder.ico");
    this.imgList.Images.SetKeyName(1, "ImbaseTable.ico");
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    gridViewCellStyle4.WrapMode = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn2.DefaultCellStyle = gridViewCellStyle4;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.ReadOnly = true;
    this.cmView.Name = "cmView";
    componentResourceManager.ApplyResources((object) this.cmView, "cmView");
    this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.Controls.Add((Control) this.splContainer);
    this.FloatingSize = new Size(766, 496);
    this.Name = nameof (FindByImagesView);
    this.PersistState = false;
    this.ShowImageInDocumentTab = true;
    this.BeforeFirstShown += new EventHandler(this.OnBeforeFirstShown);
    this.splContainer.Panel1.ResumeLayout(false);
    this.splContainer.Panel2.ResumeLayout(false);
    this.splContainer.EndInit();
    this.splContainer.ResumeLayout(false);
    ((ISupportInitialize) this.dgvItems).EndInit();
    this.ResumeLayout(false);
  }
}
