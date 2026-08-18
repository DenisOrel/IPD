// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.FindByIndexView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Imbase.Comparers;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class FindByIndexView : DockControl, IImbaseView
{
  private long _catalogID;
  private long _targetID;
  private string _category = string.Empty;
  private Lookup<long, int> _indexes;
  private NavigatorTreeNode _parentINode;
  private TreeNode _parentTNode;
  private LocateNodeEventHandler _locateHandler;
  private SearchesAccuracy _searchesAccuracy = SearchesAccuracy.Exact;
  private string _strCount = LocalizationHolder.rm.GetString("Imbase_IndexingView_RowsCount");
  private List<long> _catalogIDs;
  private IContainer components;
  private TableLayoutPanel _tlpBottom;
  private Button _btnSearch;
  private Panel _pnlSecondPage;
  private Panel _pnlBottom;
  private Button _btnInTable;
  private Panel _pnlFirstPage;
  private SplitContainer _scConditions;
  private System.Windows.Forms.TabControl _tcIndexes;
  private System.Windows.Forms.TabPage _tpTree;
  private TreeView _trvIndexes;
  private System.Windows.Forms.TabPage _tpList;
  private ListView _lvIndexes;
  private ColumnHeader colName;
  private GroupBox _gbConditions;
  private Panel _pnlConditions;
  private GroupBox _gbRate;
  private RadioButton _rbTemplate;
  private RadioButton _rbEnd;
  private RadioButton _rbContain;
  private RadioButton _rbStart;
  private RadioButton _rbExact;
  private Panel _pnlControl;
  private Label _lbText;
  private Panel _pnlFindInResult;
  private Label _lbFindInResult;
  private TextBox _txtFindInResult;
  private SplitContainer _scResult;
  private TreeView _trvGroup;
  private DataGridView _dgvResult;
  private BindingSource _bsFilter;
  private CheckBox _chbShowAll;
  private Button _btnHelp;
  private ImageList _imgList;
  private Panel _pnlHelp;
  private Button _btnHelpClose;
  private TextBox _txtHelp;
  private ComboBox _cbText;
  private Label _lbCount;
  private Button _btnPrev;

  public TreeViewsBridge Bridge { get; set; }

  public IViewsManager ViewsMngr { get; set; }

  public override string HelpID => "1756";

  public FindByIndexView()
  {
    this.InitializeComponent();
    this.Text = LocalizationHolder.rm.GetString("Imbase_FindByIndexView_DialogCaption");
    if (Statics.IconSrv != null)
      this._trvIndexes.ImageList = this._lvIndexes.SmallImageList = Statics.IconSrv.ImageList;
    NodeSorter nodeSorter = new NodeSorter();
    this._trvIndexes.TreeViewNodeSorter = (IComparer) nodeSorter;
    this._trvIndexes.Sort();
    this._trvGroup.TreeViewNodeSorter = (IComparer) nodeSorter;
    this._trvGroup.Sort();
    this._pnlSecondPage.Dock = DockStyle.Fill;
    this._pnlSecondPage.Visible = false;
    this._pnlFirstPage.Dock = DockStyle.Fill;
    this._rbExact.Tag = (object) SearchesAccuracy.Exact;
    this._rbStart.Tag = (object) SearchesAccuracy.Start;
    this._rbContain.Tag = (object) SearchesAccuracy.Сontain;
    this._rbEnd.Tag = (object) SearchesAccuracy.End;
    this._rbTemplate.Tag = (object) SearchesAccuracy.Template;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, this.HelpID);
  }

  public static DockState DockStateToLocation(DockLocation location)
  {
    switch (location)
    {
      case DockLocation.Unknown:
      case DockLocation.Left:
        return DockState.DockLeft;
      case DockLocation.Right:
      case DockLocation.Center:
        return DockState.DockRight;
      case DockLocation.Top:
        return DockState.DockTop;
      case DockLocation.Bottom:
        return DockState.DockBottom;
      case DockLocation.Float:
        return DockState.Float;
      case DockLocation.Document:
        return DockState.Document;
      default:
        return DockState.DockLeft;
    }
  }

  public static void Show(
    object parentNode,
    bool modal,
    LocateNodeEventHandler locateHandler,
    IViewsManager mngr,
    TreeViewsBridge bridge)
  {
    FindByIndexView view = new FindByIndexView();
    view.SetData(parentNode, locateHandler);
    view.ViewsMngr = mngr;
    view.Bridge = bridge;
    if (modal)
    {
      ImbaseViewForm.FindOrCreateViewForm(ImbaseViewForm.FormType.FindByIndex, (IImbaseView) view, (Icon) null).Show();
    }
    else
    {
      if (!(ServicesManager.GetService(typeof (DockManager)) is DockManager service))
        return;
      HybridDictionary hybridDictionary = new HybridDictionary(0, true);
      FormStorage.LoadLayout((Control) view, (IDictionary) hybridDictionary, true, out Point _, out Size _);
      DockLocation location = DockLocation.Right;
      Size size = Size.Empty;
      Point point = Point.Empty;
      if (hybridDictionary.Count > 0)
      {
        size = hybridDictionary.Contains((object) "FloatingSize") ? (Size) hybridDictionary[(object) "FloatingSize"] : Size.Empty;
        point = hybridDictionary.Contains((object) "FloatingLocation") ? (Point) hybridDictionary[(object) "FloatingLocation"] : Point.Empty;
        location = hybridDictionary.Contains((object) "DockLocation") ? (DockLocation) hybridDictionary[(object) "DockLocation"] : DockLocation.Right;
        if (location == DockLocation.Float && (size == Size.Empty || point == Point.Empty))
          location = DockLocation.Right;
      }
      view.ShowHint = FindByIndexView.DockStateToLocation(location);
      if (view.ShowHint == DockState.Float)
      {
        view.FloatingSize = size;
        view.FloatingLocation = point;
        view.Manager = service;
        view.Float();
      }
      else
        view.Show(service);
      view.Activate();
    }
  }

  private void On_btnInTable_Click(object sender, EventArgs e)
  {
    SelectedRecords.Clear();
    if (this._dgvResult.SelectedRows.Count <= 0)
      return;
    DataGridViewRow selectedRow = this._dgvResult.SelectedRows[0];
    long int64_1 = Convert.ToInt64(selectedRow.Cells[IndexesField.F_LINK_ID].Value);
    long int64_2 = Convert.ToInt64(selectedRow.Cells[IndexesField.F_TABKEY].Value);
    SelectedRecords.Add(int64_1, new long[1]{ int64_2 });
    if (this._locateHandler != null)
      this._locateHandler((object) this, new LocateNodeEventArgs(int64_1, FindHelper.GetDataTable(int64_1)));
    else if (this._parentINode != null)
    {
      FindHelper.SearchNodeByNodeID(this._parentINode, int64_1);
      if (this.ViewsMngr != null)
      {
        try
        {
          if (this.Bridge != null)
            this.Bridge.BridgeEnabled = false;
          this.ViewsMngr.UpdateViews(Intermech.Navigator.ContextMenu.Services.GetItems(int64_1));
          string str = int64_2 == -1L ? "ObjectProperties" : "ImbaseTableView";
          for (int index = 0; index < this.ViewsMngr.ViewPages.Count; ++index)
          {
            if (!(this.ViewsMngr.ViewPages[index].Name != str))
            {
              this.ViewsMngr.ActiveViewPage = this.ViewsMngr.ViewPages[index];
              break;
            }
          }
        }
        finally
        {
          if (this.Bridge != null)
            this.Bridge.BridgeEnabled = true;
        }
      }
    }
    if (this._parentTNode == null)
      return;
    TreeNode treeNode = FindHelper.SearchNodeByNodeID(this._parentTNode, int64_1);
    if (treeNode == null)
      return;
    treeNode.EnsureVisible();
    treeNode.TreeView.SelectedNode = treeNode;
  }

  private void GoNext(object sender, EventArgs e)
  {
    this._pnlFirstPage.Visible = false;
    this._pnlSecondPage.Visible = true;
    this._btnSearch.Enabled = false;
    this._btnPrev.Enabled = true;
  }

  private void On_btnPrev_Click(object sender, EventArgs e)
  {
    this._pnlFirstPage.Visible = true;
    this._pnlSecondPage.Visible = false;
    this._btnSearch.Enabled = true;
    this._btnPrev.Enabled = false;
  }

  private void On_btnSearch_Click(object sender, EventArgs e)
  {
    try
    {
      List<long> catalogIDs = (List<long>) null;
      int int32;
      if (this._tcIndexes.SelectedIndex == 0)
      {
        int32 = Convert.ToInt32(this._trvIndexes.SelectedNode.Name);
        catalogIDs = new List<long>()
        {
          Convert.ToInt64(this._trvIndexes.SelectedNode.Parent.Name)
        };
      }
      else
        int32 = Convert.ToInt32(this._lvIndexes.SelectedItems[0].Name);
      string[] colsNames = new string[4]
      {
        IndexesField.F_TEXT,
        IndexesField.F_LINK_ID,
        IndexesField.F_TABKEY,
        IndexesField.F_TABLE_ID
      };
      DataTable dt;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
          throw new Exception(LocalizationHolder.rm.GetString("Imbase_FindByIndex_GetIndexes_Error"));
        dt = this._catalogID == this._targetID || catalogIDs == null ? customService.Search(sessionKeeper.Session.SessionGUID, catalogIDs, int32, colsNames, this._cbText.Text, this._searchesAccuracy) : customService.Search(sessionKeeper.Session.SessionGUID, catalogIDs[0], int32, this._targetID, colsNames, this._cbText.Text, this._searchesAccuracy);
        if (dt != null)
        {
          this.CheckVisible(sessionKeeper.Session, dt);
          dt.Columns.Add(IndexesField.F_GROUP);
        }
      }
      this.FillResultTable(dt);
      this._txtFindInResult.Enabled = this._dgvResult.Rows.Count > 0;
      this._bsFilter.DataSource = (object) dt;
      this._chbShowAll.Checked = true;
      this._txtFindInResult.Text = string.Empty;
      this._bsFilter.Filter = string.Empty;
      this.GoNext((object) this._btnSearch, new EventArgs());
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void On_chbShowAll_CheckedChanged(object sender, EventArgs e)
  {
    this._txtFindInResult.Text = string.Empty;
    if ((sender as CheckBox).Checked)
    {
      this._trvGroup.Enabled = false;
      this._bsFilter.Filter = string.Empty;
    }
    else
    {
      this._trvGroup.Enabled = true;
      if (this._trvGroup.SelectedNode != null)
        this._bsFilter.Filter = $"{SQLStringHelper.QuoteLikeString($"{IndexesField.F_GROUP} LIKE '{this._trvGroup.SelectedNode.Name}")}{"*"}'";
    }
    this._lbCount.Text = $"{this._strCount} {this._dgvResult.Rows.Count.ToString()}";
  }

  private void On_dgvResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
  {
    if (!(this._dgvResult.Columns[e.ColumnIndex].Name == IndexesField.F_CATALOG_ID) || e.Value == null || this._dgvResult.Tag == null || !(this._dgvResult.Tag is Dictionary<long, string> tag))
      return;
    long result = 0;
    if (!long.TryParse(Convert.ToString(e.Value), out result))
      return;
    if (!tag.ContainsKey(result))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(result);
        if (objectInfo.Empty)
          return;
        tag.Add(result, objectInfo.Caption);
      }
    }
    e.Value = (object) tag[result];
  }

  private void On_dgvResult_DoubleClick(object sender, EventArgs e)
  {
    this.On_btnInTable_Click((object) null, e);
  }

  private void On_lvIndexes_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnSearch.Enabled = (sender as ListView).SelectedItems.Count > 0;
  }

  private void On_tcIndexes_SelectedIndexChanged(object sender, EventArgs e)
  {
    switch ((sender as System.Windows.Forms.TabControl).SelectedIndex)
    {
      case 0:
        this._btnSearch.Enabled = this._trvIndexes.SelectedNode != null && this._trvIndexes.SelectedNode.Parent != null;
        break;
      case 1:
        this._btnSearch.Enabled = this._lvIndexes.SelectedItems.Count > 0;
        break;
    }
  }

  private void On_trvGroup_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this._txtFindInResult.Text = string.Empty;
    TreeNode selectedNode = (sender as TreeView).SelectedNode;
    if (selectedNode == null)
    {
      this._bsFilter.Filter = string.Empty;
    }
    else
    {
      string str = selectedNode.Name.Trim().Replace("'", "''");
      this._bsFilter.Filter = $"{IndexesField.F_GROUP}='{str}'";
      if (selectedNode.Nodes.Count > 0)
        this._bsFilter.Filter += $"{SQLStringHelper.QuoteLikeString($" OR {IndexesField.F_GROUP} LIKE '{str}{IndexesConsts.GROUP_DELIMITER}")}{"*"}'";
    }
    this._lbCount.Text = $"{this._strCount} {this._dgvResult.Rows.Count.ToString()}";
  }

  private void On_trvGroup_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    if (Convert.ToInt16(e.Node.Tag) == (short) 1)
      return;
    this._trvGroup.BeginUpdate();
    try
    {
      e.Node.Nodes.Clear();
      e.Node.Tag = (object) 1;
      DataTable dataSource = this._dgvResult.DataSource as DataTable;
      string str1 = e.Node.Name.Replace("'", "''");
      string filterExpression = $"{SQLStringHelper.QuoteLikeString($"{IndexesField.F_GROUP} LIKE '{str1}{IndexesConsts.GROUP_DELIMITER}")}{"*"}'";
      DataRow[] dataRowArray = dataSource.Select(filterExpression);
      if (dataRowArray.Length == 0)
        return;
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        string str2 = Convert.ToString(dataRowArray[index][IndexesField.F_GROUP]);
        int length = str2.IndexOf(IndexesConsts.GROUP_DELIMITER, str1.Length + 1);
        string strB = string.Empty;
        string key;
        string text;
        if (length > -1)
        {
          key = str2.Substring(0, length);
          text = str2.Substring(str1.Length + 1, length - str1.Length - 1);
          strB = str2.Substring(key.Length + 1);
        }
        else
        {
          key = str2;
          text = str2.Substring(str1.Length + 1);
        }
        if (dictionary1.ContainsKey(key))
        {
          if (dictionary1[key] == 1 && string.Compare(dictionary2[key], strB) != 0)
          {
            dictionary1[key]++;
            dictionary2.Remove(key);
            e.Node.Nodes[key].Nodes.Add(new TreeNode());
          }
        }
        else
        {
          TreeNode node = new TreeNode(text)
          {
            Name = key,
            Tag = (object) 0
          };
          e.Node.Nodes.Add(node);
          dictionary1.Add(key, 1);
          dictionary2.Add(key, strB);
        }
      }
      string empty5 = string.Empty;
      foreach (string key in dictionary2.Keys)
      {
        string str3 = dictionary2[key].Replace(IndexesConsts.GROUP_DELIMITER, ' ');
        e.Node.Nodes[key].Text = $"{e.Node.Nodes[key].Text} {str3}";
        if (!string.IsNullOrEmpty(str3))
          e.Node.Nodes[key].Name = $"{e.Node.Nodes[key].Name}{IndexesConsts.GROUP_DELIMITER}{dictionary2[key]}";
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this._trvGroup.EndUpdate();
    }
  }

  private void On_trvIndexes_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeView treeView = sender as TreeView;
    treeView.SelectedNode = treeView.SelectedNode ?? this._trvIndexes.TopNode;
    this._btnSearch.Enabled = treeView.SelectedNode.Parent != null;
  }

  private void On_txtFindInResult_TextChanged(object sender, EventArgs e)
  {
    string empty = string.Empty;
    foreach (DataGridViewRow row in (IEnumerable) this._dgvResult.Rows)
    {
      if (Convert.ToString(row.Cells[IndexesField.F_TEXT].Value).StartsWith(this._txtFindInResult.Text, StringComparison.InvariantCultureIgnoreCase))
      {
        this._dgvResult.Rows[row.Index].Selected = true;
        this._dgvResult.FirstDisplayedScrollingRowIndex = row.Index;
        break;
      }
    }
  }

  private void OnBeforeFirstShown(object sender, EventArgs e)
  {
    try
    {
      DataTable dt = (DataTable) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
          throw new Exception(LocalizationHolder.rm.GetString("Imbase_FindByIndex_GetIndexes_Error"));
        string[] colsNames1 = new string[2]
        {
          IndexesField.F_ATTRIBUTE_ID,
          IndexesField.F_CATALOG_ID
        };
        if (this._catalogID == 0L)
        {
          if (string.IsNullOrEmpty(this._category))
          {
            dt = customService.GetIndexes(sessionKeeper.Session.SessionGUID, this._catalogIDs, colsNames1);
          }
          else
          {
            Dictionary<long, string> catalogInfo = this.GetCatalogInfo();
            if (catalogInfo == null)
              return;
            dt = customService.GetIndexes(sessionKeeper.Session.SessionGUID, catalogInfo.Keys.ToList<long>(), colsNames1);
          }
        }
        else
        {
          IImbaseIndexingService imbaseIndexingService = customService;
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          List<long> catalogIDs = new List<long>();
          catalogIDs.Add(this._catalogID);
          string[] colsNames2 = colsNames1;
          dt = imbaseIndexingService.GetIndexes(sessionGuid, catalogIDs, colsNames2);
        }
      }
      this.BuildIndexesTree(dt);
      if (this._trvIndexes.Nodes.Count > 0)
      {
        TreeNode node = this._trvIndexes.Nodes[0];
        if (node.Nodes.Count > 0)
          this._trvIndexes.SelectedNode = node.Nodes[0];
        if (this._lvIndexes.Items.Count > 0)
          this._lvIndexes.Items[0].Selected = true;
      }
      this.LoadSettings();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void OnRadioButton_Click(object sender, EventArgs e)
  {
    this._searchesAccuracy = (SearchesAccuracy) (sender as RadioButton).Tag;
    this._btnHelp.Visible = this._searchesAccuracy == SearchesAccuracy.Template;
  }

  private void On_btnHelp_Click(object sender, EventArgs e) => this._pnlHelp.Visible = true;

  private void On_btnHelp_MouseDown(object sender, MouseEventArgs e)
  {
    this._btnHelp.BackgroundImage = this._imgList.Images["Help_Clicked"];
  }

  private void On_btnHelp_MouseEnter(object sender, EventArgs e)
  {
    this._btnHelp.BackgroundImage = this._imgList.Images["Help_Focused"];
  }

  private void On_btnHelp_MouseLeave(object sender, EventArgs e)
  {
    this._btnHelp.BackgroundImage = this._imgList.Images["Help"];
  }

  private void On_btnHelp_MouseUp(object sender, MouseEventArgs e)
  {
    this._btnHelp.BackgroundImage = this._imgList.Images["Help"];
  }

  private void On_btnHelp_VisibleChanged(object sender, EventArgs e)
  {
    this._btnHelp.BackgroundImage = this._imgList.Images["Help"];
    this._pnlHelp.Visible = false;
  }

  private void On_btnHelpClose_Click(object sender, EventArgs e) => this._pnlHelp.Visible = false;

  public void FirstShown(object sender, EventArgs e) => this.OnBeforeFirstShown(sender, e);

  public void ViewClosing(object sender, CancelEventArgs e) => this.OnClosing(e);

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    this.SaveSettings();
  }

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

  private void AddIndexToIndexesList(IDBAttributeType attrType, int imgIndex)
  {
    string key = Convert.ToString(attrType.AttributeID);
    if (this._lvIndexes.Items.ContainsKey(key))
      return;
    this._lvIndexes.Items.Add(new ListViewItem(attrType.Name, imgIndex)
    {
      Name = key
    });
  }

  private void BuildFirstLevelNodesForGroupTree(DataTable dt)
  {
    if (dt == null)
      return;
    Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      string group = this.GetGroup(Convert.ToString(row[IndexesField.F_TEXT]));
      row[IndexesField.F_GROUP] = (object) group;
      int length = group.IndexOf(IndexesConsts.GROUP_DELIMITER, 0);
      string str = length > -1 ? group.Substring(0, length) : (string.IsNullOrEmpty(group) ? " " : group);
      if (dictionary1.ContainsKey(str))
      {
        if (dictionary1[str] == 1 && string.Compare(dictionary2[str], group) != 0)
        {
          dictionary1[str]++;
          dictionary2.Remove(str);
          this._trvGroup.Nodes[str].Nodes.Add(new TreeNode());
        }
      }
      else
      {
        this._trvGroup.Nodes.Add(new TreeNode(str)
        {
          Name = str,
          Tag = (object) 0
        });
        dictionary1.Add(str, 1);
        dictionary2.Add(str, group);
      }
    }
    foreach (string key in dictionary2.Keys)
    {
      this._trvGroup.Nodes[key].Text = dictionary2[key].Replace(IndexesConsts.GROUP_DELIMITER, ' ');
      this._trvGroup.Nodes[key].Name = dictionary2[key];
    }
    this._trvGroup.CollapseAll();
    this._trvGroup.Sort();
  }

  private void BuildIndexesTree(DataTable dt)
  {
    this._trvIndexes.BeginUpdate();
    this._trvIndexes.Nodes.Clear();
    try
    {
      if (dt == null || dt.Rows.Count <= 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        int num1 = Statics.IconSrv.IndexOf(4, Intermech.Imbase.Consts.ImbaseCatalogTypeID, (object) null);
        this._indexes = (Lookup<long, int>) dt.AsEnumerable().ToLookup<DataRow, long, int>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_CATALOG_ID])), (System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID])));
        foreach (IGrouping<long, int> index in this._indexes)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(index.Key);
          if (!objectInfo.Empty)
          {
            TreeNode node1 = new TreeNode(objectInfo.Caption, num1, num1)
            {
              Name = Convert.ToString(objectInfo.ObjectID)
            };
            this._trvIndexes.Nodes.Add(node1);
            foreach (int anAttributeType in (IEnumerable<int>) index)
            {
              IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeType);
              if (attributeType != null)
              {
                int num2 = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.AttributeType);
                TreeNode node2 = new TreeNode(attributeType.Name, num2, num2)
                {
                  Name = Convert.ToString(anAttributeType)
                };
                node1.Nodes.Add(node2);
                this.AddIndexToIndexesList(attributeType, num2);
              }
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this._trvIndexes.EndUpdate();
    }
  }

  private DataTable CheckVisible(IUserSession session, DataTable dt)
  {
    IImbaseServer server = TreeBuilder.GetServer(session);
    if (server != null)
    {
      foreach (IGrouping<long, DataRow> grouping in (Lookup<long, DataRow>) dt.AsEnumerable().ToLookup<DataRow, long, DataRow>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_CATALOG_ID])), (System.Func<DataRow, DataRow>) (x => x)))
      {
        DataTable allSubfolders = server.GetAllSubfolders(session.SessionGUID, grouping.Key, new int[1]
        {
          Intermech.Imbase.Consts.ImbaseTableRefTypeID
        });
        if (allSubfolders != null)
        {
          List<long> list = allSubfolders.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["F_OBJECT_ID"]))).ToList<long>();
          list.Sort();
          foreach (DataRow row in (IEnumerable<DataRow>) grouping)
          {
            if (list.BinarySearch(Convert.ToInt64(row[IndexesField.F_LINK_ID])) <= -1)
            {
              dt.BeginLoadData();
              dt.Rows.Remove(row);
              dt.EndLoadData();
            }
          }
        }
      }
    }
    return dt;
  }

  private void FillResultTable(DataTable dt)
  {
    this._dgvResult.Tag = (object) null;
    this._dgvResult.DataSource = (object) dt;
    if (dt == null)
      return;
    for (int index = 0; index < this._dgvResult.Columns.Count; ++index)
      this._dgvResult.Columns[index].Visible = false;
    DataGridViewColumn column1 = this._dgvResult.Columns[IndexesField.F_CATALOG_ID];
    column1.DisplayIndex = 0;
    column1.HeaderText = LocalizationHolder.rm.GetString("Imbase_FindByIndex_ResultTable_Column_Catalog");
    column1.ReadOnly = true;
    column1.Visible = true;
    DataGridViewColumn column2 = this._dgvResult.Columns[IndexesField.F_TEXT];
    column2.DisplayIndex = 1;
    column2.HeaderText = LocalizationHolder.rm.GetString("Imbase_FindByIndex_ResultTable_Column_Text");
    column2.ReadOnly = true;
    column2.Visible = true;
    column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this._trvGroup.BeginUpdate();
    try
    {
      this._trvGroup.Nodes.Clear();
      if (dt.Rows.Count > 0)
      {
        this._dgvResult.Tag = (object) this.GetCatalogInfo();
        this.BuildFirstLevelNodesForGroupTree(dt);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this._trvGroup.EndUpdate();
    }
    this._lbCount.Text = $"{this._strCount} {this._dgvResult.Rows.Count.ToString()}";
  }

  private Dictionary<long, string> GetCatalogInfo()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable source = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        this._catalogID != 0L ? new ConditionStructure(-2, RelationalOperators.Equal, (object) this._catalogID, LogicalOperators.NONE, 0, true) : (!string.IsNullOrEmpty(this._category) ? new ConditionStructure(Intermech.Imbase.Consts.CatalogTypeAttID, RelationalOperators.Equal, (object) this._category, LogicalOperators.NONE, 0, true) : new ConditionStructure(-2, RelationalOperators.NotEqual, (object) 0L, LogicalOperators.NONE, 0, true))
      }, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION
      })
      {
        Contents = new ColumnContents[2]
        {
          ColumnContents.ID,
          ColumnContents.String
        }
      });
      return source != null ? source.AsEnumerable().ToDictionary<DataRow, long, string>((System.Func<DataRow, long>) (k => Convert.ToInt64(k[0])), (System.Func<DataRow, string>) (v => Convert.ToString(v[1]))) : (Dictionary<long, string>) null;
    }
  }

  private string GetGroup(string text)
  {
    string[] strArray = text.Split(' ', '#', '&', ';', ':', '-', '/', '\\', '*');
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < strArray.Length - 1; ++index)
    {
      if (!string.IsNullOrEmpty(strArray[index]))
      {
        stringBuilder.Append(strArray[index]);
        stringBuilder.Append(IndexesConsts.GROUP_DELIMITER);
      }
    }
    if (!string.IsNullOrEmpty(strArray[strArray.Length - 1]))
      stringBuilder.Append(strArray[strArray.Length - 1]);
    else if (stringBuilder.Length > 0)
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
    return stringBuilder.ToString();
  }

  private void SetData(object parentNode, LocateNodeEventHandler locateHandler)
  {
    this._locateHandler = locateHandler;
    switch (parentNode)
    {
      case NavigatorTreeNode navigatorTreeNode2:
        NavigatorTreeNode navigatorTreeNode1 = navigatorTreeNode2;
        if (navigatorTreeNode1.NodeID is CatalogsNodeID nodeId2)
        {
          this._catalogID = 0L;
          this._targetID = 0L;
          this._category = nodeId2.CatalogName;
        }
        else if (navigatorTreeNode1.NodeID is NodeID nodeId1)
        {
          this._targetID = nodeId1.ObjectID;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            this._catalogID = sessionKeeper.Session.GetObjectInfo(this._targetID).ObjectTypeID != Intermech.Imbase.Consts.ImbaseCatalogTypeID ? TableLoadHelper.GetCatalogIDByObjectID(sessionKeeper.Session, this._targetID) : this._targetID;
            this._catalogIDs = new List<long>()
            {
              this._catalogID
            };
          }
        }
        else if (navigatorTreeNode1.NodeID.CategoryID == Intermech.Imbase.Consts.RootNodeCategoryID)
        {
          this._catalogIDs = new List<long>(navigatorTreeNode1.Children.Count);
          foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) navigatorTreeNode1.Children)
          {
            if (child.NodeID is NodeID nodeId)
              this._catalogIDs.Add(nodeId.ObjectID);
          }
        }
        List<long> longList = this._catalogIDs;
        if (longList == null)
          longList = new List<long>() { -1L };
        this._catalogIDs = longList;
        this._parentINode = navigatorTreeNode2;
        break;
      case TreeNode treeNode:
        if (!(treeNode.Tag is NodeInfo tag))
          break;
        this._targetID = tag.ObjectId;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this._catalogID = tag.IsCatalog ? this._targetID : TableLoadHelper.GetCatalogIDByObjectID(sessionKeeper.Session, this._targetID);
        this._catalogIDs = new List<long>()
        {
          this._catalogID
        };
        this._parentTNode = treeNode;
        break;
    }
  }

  private void LoadSettings()
  {
    HybridDictionary hybridDictionary = new HybridDictionary(0, true);
    FormStorage.LoadLayout((Control) this, (IDictionary) hybridDictionary, true, out Point _, out Size _);
    int result1 = -1;
    if (hybridDictionary.Contains((object) "TabIndex") && int.TryParse(Convert.ToString(hybridDictionary[(object) "TabIndex"]), out result1))
    {
      string str1 = hybridDictionary.Contains((object) "CatalogID") ? Convert.ToString(hybridDictionary[(object) "CatalogID"]) : string.Empty;
      string str2 = hybridDictionary.Contains((object) "AttributeID") ? Convert.ToString(hybridDictionary[(object) "AttributeID"]) : string.Empty;
      if (result1 == 0)
      {
        if (!string.IsNullOrEmpty(str1))
        {
          foreach (TreeNode node in this._trvIndexes.Nodes)
          {
            if (!(node.Name != str1))
            {
              if (!string.IsNullOrEmpty(str2))
              {
                IEnumerator enumerator = node.Nodes.GetEnumerator();
                try
                {
                  while (enumerator.MoveNext())
                  {
                    TreeNode current = (TreeNode) enumerator.Current;
                    if (!(current.Name != str2))
                    {
                      this._trvIndexes.SelectedNode = current;
                      break;
                    }
                  }
                  break;
                }
                finally
                {
                  if (enumerator is IDisposable disposable)
                    disposable.Dispose();
                }
              }
              else
              {
                this._trvIndexes.SelectedNode = node;
                break;
              }
            }
          }
        }
        this._tcIndexes.SelectedIndex = 0;
      }
      else
      {
        if (!string.IsNullOrEmpty(str2))
        {
          foreach (ListViewItem listViewItem in this._lvIndexes.Items)
          {
            if (!(listViewItem.Name != str2))
            {
              listViewItem.Selected = true;
              this.On_tcIndexes_SelectedIndexChanged((object) this._tcIndexes, EventArgs.Empty);
              break;
            }
          }
        }
        this._tcIndexes.SelectedIndex = 1;
      }
    }
    if (hybridDictionary.Contains((object) "TextSearch"))
      this._cbText.Text = Convert.ToString(hybridDictionary[(object) "TextSearch"]);
    if (!hybridDictionary.Contains((object) "SearchType"))
      return;
    int result2 = -1;
    if (!int.TryParse(Convert.ToString(hybridDictionary[(object) "SearchType"]), out result2))
      return;
    RadioButton sender = (RadioButton) null;
    switch (result2)
    {
      case 0:
        sender = this._rbExact;
        break;
      case 1:
        sender = this._rbStart;
        break;
      case 2:
        sender = this._rbContain;
        break;
      case 3:
        sender = this._rbEnd;
        break;
      case 4:
        sender = this._rbTemplate;
        break;
    }
    if (sender == null)
      return;
    sender.Checked = true;
    this.OnRadioButton_Click((object) sender, EventArgs.Empty);
  }

  private void SaveSettings()
  {
    HybridDictionary hybridDictionary = new HybridDictionary(0, true);
    hybridDictionary.Add((object) "DockLocation", (object) this.DockLocation);
    if (this.DockLocation == DockLocation.Float)
    {
      hybridDictionary.Add((object) "FloatingLocation", (object) this.FloatingLocation);
      hybridDictionary.Add((object) "FloatingSize", (object) this.FloatingSize);
    }
    hybridDictionary.Add((object) "TabIndex", (object) this._tcIndexes.SelectedIndex);
    if (this._tcIndexes.SelectedIndex == 0)
    {
      TreeNode selectedNode = this._trvIndexes.SelectedNode;
      if (selectedNode != null)
      {
        if (selectedNode.Parent == null)
        {
          hybridDictionary.Add((object) "CatalogID", (object) selectedNode.Name);
        }
        else
        {
          hybridDictionary.Add((object) "CatalogID", (object) selectedNode.Parent.Name);
          hybridDictionary.Add((object) "AttributeID", (object) Convert.ToInt32(selectedNode.Name));
        }
      }
    }
    else if (this._lvIndexes.SelectedItems.Count > 0)
      hybridDictionary.Add((object) "AttributeID", (object) Convert.ToInt32(this._lvIndexes.SelectedItems[0].Name));
    hybridDictionary.Add((object) "TextSearch", (object) this._cbText.Text);
    int num = this._rbExact.Checked ? 0 : (this._rbStart.Checked ? 1 : (this._rbContain.Checked ? 2 : (this._rbEnd.Checked ? 3 : (this._rbTemplate.Checked ? 4 : -1))));
    if (num != -1)
      hybridDictionary.Add((object) "SearchType", (object) num);
    FormStorage.SaveLayout((Control) this, (IDictionary) hybridDictionary);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FindByIndexView));
    this._scConditions = new SplitContainer();
    this._tcIndexes = new System.Windows.Forms.TabControl();
    this._tpTree = new System.Windows.Forms.TabPage();
    this._trvIndexes = new TreeView();
    this._tpList = new System.Windows.Forms.TabPage();
    this._lvIndexes = new ListView();
    this.colName = new ColumnHeader();
    this._gbConditions = new GroupBox();
    this._pnlConditions = new Panel();
    this._gbRate = new GroupBox();
    this._pnlHelp = new Panel();
    this._btnHelpClose = new Button();
    this._imgList = new ImageList(this.components);
    this._txtHelp = new TextBox();
    this._rbTemplate = new RadioButton();
    this._rbEnd = new RadioButton();
    this._rbContain = new RadioButton();
    this._rbStart = new RadioButton();
    this._rbExact = new RadioButton();
    this._pnlControl = new Panel();
    this._cbText = new ComboBox();
    this._btnHelp = new Button();
    this._lbText = new Label();
    this._scResult = new SplitContainer();
    this._trvGroup = new TreeView();
    this._dgvResult = new DataGridView();
    this._tlpBottom = new TableLayoutPanel();
    this._btnPrev = new Button();
    this._btnSearch = new Button();
    this._pnlSecondPage = new Panel();
    this._pnlFindInResult = new Panel();
    this._lbFindInResult = new Label();
    this._txtFindInResult = new TextBox();
    this._pnlBottom = new Panel();
    this._lbCount = new Label();
    this._chbShowAll = new CheckBox();
    this._btnInTable = new Button();
    this._pnlFirstPage = new Panel();
    this._bsFilter = new BindingSource(this.components);
    this._scConditions.BeginInit();
    this._scConditions.Panel1.SuspendLayout();
    this._scConditions.Panel2.SuspendLayout();
    this._scConditions.SuspendLayout();
    this._tcIndexes.SuspendLayout();
    this._tpTree.SuspendLayout();
    this._tpList.SuspendLayout();
    this._gbConditions.SuspendLayout();
    this._pnlConditions.SuspendLayout();
    this._gbRate.SuspendLayout();
    this._pnlHelp.SuspendLayout();
    this._pnlControl.SuspendLayout();
    this._scResult.BeginInit();
    this._scResult.Panel1.SuspendLayout();
    this._scResult.Panel2.SuspendLayout();
    this._scResult.SuspendLayout();
    ((ISupportInitialize) this._dgvResult).BeginInit();
    this._tlpBottom.SuspendLayout();
    this._pnlSecondPage.SuspendLayout();
    this._pnlFindInResult.SuspendLayout();
    this._pnlBottom.SuspendLayout();
    this._pnlFirstPage.SuspendLayout();
    ((ISupportInitialize) this._bsFilter).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._scConditions, "_scConditions");
    this._scConditions.Name = "_scConditions";
    this._scConditions.Panel1.Controls.Add((Control) this._tcIndexes);
    componentResourceManager.ApplyResources((object) this._scConditions.Panel1, "_scConditions.Panel1");
    this._scConditions.Panel2.Controls.Add((Control) this._gbConditions);
    componentResourceManager.ApplyResources((object) this._scConditions.Panel2, "_scConditions.Panel2");
    this._tcIndexes.Controls.Add((Control) this._tpTree);
    this._tcIndexes.Controls.Add((Control) this._tpList);
    componentResourceManager.ApplyResources((object) this._tcIndexes, "_tcIndexes");
    this._tcIndexes.Multiline = true;
    this._tcIndexes.Name = "_tcIndexes";
    this._tcIndexes.SelectedIndex = 0;
    this._tcIndexes.SelectedIndexChanged += new EventHandler(this.On_tcIndexes_SelectedIndexChanged);
    this._tpTree.Controls.Add((Control) this._trvIndexes);
    componentResourceManager.ApplyResources((object) this._tpTree, "_tpTree");
    this._tpTree.Name = "_tpTree";
    this._tpTree.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._trvIndexes, "_trvIndexes");
    this._trvIndexes.FullRowSelect = true;
    this._trvIndexes.HideSelection = false;
    this._trvIndexes.Name = "_trvIndexes";
    this._trvIndexes.AfterSelect += new TreeViewEventHandler(this.On_trvIndexes_AfterSelect);
    this._tpList.Controls.Add((Control) this._lvIndexes);
    componentResourceManager.ApplyResources((object) this._tpList, "_tpList");
    this._tpList.Name = "_tpList";
    this._tpList.UseVisualStyleBackColor = true;
    this._lvIndexes.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName
    });
    componentResourceManager.ApplyResources((object) this._lvIndexes, "_lvIndexes");
    this._lvIndexes.FullRowSelect = true;
    this._lvIndexes.HeaderStyle = ColumnHeaderStyle.None;
    this._lvIndexes.HideSelection = false;
    this._lvIndexes.MultiSelect = false;
    this._lvIndexes.Name = "_lvIndexes";
    this._lvIndexes.UseCompatibleStateImageBehavior = false;
    this._lvIndexes.View = View.Details;
    this._lvIndexes.SelectedIndexChanged += new EventHandler(this.On_lvIndexes_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.colName, "colName");
    this._gbConditions.Controls.Add((Control) this._pnlConditions);
    componentResourceManager.ApplyResources((object) this._gbConditions, "_gbConditions");
    this._gbConditions.ForeColor = SystemColors.ControlText;
    this._gbConditions.Name = "_gbConditions";
    this._gbConditions.TabStop = false;
    this._pnlConditions.Controls.Add((Control) this._gbRate);
    this._pnlConditions.Controls.Add((Control) this._pnlControl);
    this._pnlConditions.Controls.Add((Control) this._lbText);
    componentResourceManager.ApplyResources((object) this._pnlConditions, "_pnlConditions");
    this._pnlConditions.Name = "_pnlConditions";
    this._gbRate.Controls.Add((Control) this._pnlHelp);
    this._gbRate.Controls.Add((Control) this._rbTemplate);
    this._gbRate.Controls.Add((Control) this._rbEnd);
    this._gbRate.Controls.Add((Control) this._rbContain);
    this._gbRate.Controls.Add((Control) this._rbStart);
    this._gbRate.Controls.Add((Control) this._rbExact);
    componentResourceManager.ApplyResources((object) this._gbRate, "_gbRate");
    this._gbRate.ForeColor = SystemColors.ControlText;
    this._gbRate.Name = "_gbRate";
    this._gbRate.TabStop = false;
    componentResourceManager.ApplyResources((object) this._pnlHelp, "_pnlHelp");
    this._pnlHelp.BackColor = SystemColors.Info;
    this._pnlHelp.Controls.Add((Control) this._btnHelpClose);
    this._pnlHelp.Controls.Add((Control) this._txtHelp);
    this._pnlHelp.Name = "_pnlHelp";
    componentResourceManager.ApplyResources((object) this._btnHelpClose, "_btnHelpClose");
    this._btnHelpClose.ImageList = this._imgList;
    this._btnHelpClose.Name = "_btnHelpClose";
    this._btnHelpClose.UseVisualStyleBackColor = true;
    this._btnHelpClose.Click += new EventHandler(this.On_btnHelpClose_Click);
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Magenta;
    this._imgList.Images.SetKeyName(0, "Close");
    this._imgList.Images.SetKeyName(1, "Help_Focused");
    this._imgList.Images.SetKeyName(2, "Help");
    this._imgList.Images.SetKeyName(3, "Help_Clicked");
    this._txtHelp.BackColor = SystemColors.Info;
    this._txtHelp.BorderStyle = BorderStyle.None;
    componentResourceManager.ApplyResources((object) this._txtHelp, "_txtHelp");
    this._txtHelp.Name = "_txtHelp";
    componentResourceManager.ApplyResources((object) this._rbTemplate, "_rbTemplate");
    this._rbTemplate.Name = "_rbTemplate";
    this._rbTemplate.TabStop = true;
    this._rbTemplate.Tag = (object) "";
    this._rbTemplate.UseVisualStyleBackColor = true;
    this._rbTemplate.Click += new EventHandler(this.OnRadioButton_Click);
    componentResourceManager.ApplyResources((object) this._rbEnd, "_rbEnd");
    this._rbEnd.Name = "_rbEnd";
    this._rbEnd.TabStop = true;
    this._rbEnd.Tag = (object) "";
    this._rbEnd.UseVisualStyleBackColor = true;
    this._rbEnd.Click += new EventHandler(this.OnRadioButton_Click);
    componentResourceManager.ApplyResources((object) this._rbContain, "_rbContain");
    this._rbContain.Name = "_rbContain";
    this._rbContain.TabStop = true;
    this._rbContain.Tag = (object) "";
    this._rbContain.UseVisualStyleBackColor = true;
    this._rbContain.Click += new EventHandler(this.OnRadioButton_Click);
    componentResourceManager.ApplyResources((object) this._rbStart, "_rbStart");
    this._rbStart.Name = "_rbStart";
    this._rbStart.TabStop = true;
    this._rbStart.Tag = (object) "";
    this._rbStart.UseVisualStyleBackColor = true;
    this._rbStart.Click += new EventHandler(this.OnRadioButton_Click);
    componentResourceManager.ApplyResources((object) this._rbExact, "_rbExact");
    this._rbExact.Checked = true;
    this._rbExact.Name = "_rbExact";
    this._rbExact.TabStop = true;
    this._rbExact.Tag = (object) "";
    this._rbExact.UseVisualStyleBackColor = true;
    this._rbExact.Click += new EventHandler(this.OnRadioButton_Click);
    this._pnlControl.Controls.Add((Control) this._cbText);
    this._pnlControl.Controls.Add((Control) this._btnHelp);
    componentResourceManager.ApplyResources((object) this._pnlControl, "_pnlControl");
    this._pnlControl.Name = "_pnlControl";
    componentResourceManager.ApplyResources((object) this._cbText, "_cbText");
    this._cbText.DropDownStyle = ComboBoxStyle.Simple;
    this._cbText.FormattingEnabled = true;
    this._cbText.Name = "_cbText";
    componentResourceManager.ApplyResources((object) this._btnHelp, "_btnHelp");
    this._btnHelp.FlatAppearance.BorderSize = 0;
    this._btnHelp.ImageList = this._imgList;
    this._btnHelp.Name = "_btnHelp";
    this._btnHelp.UseVisualStyleBackColor = false;
    this._btnHelp.VisibleChanged += new EventHandler(this.On_btnHelp_VisibleChanged);
    this._btnHelp.Click += new EventHandler(this.On_btnHelp_Click);
    this._btnHelp.MouseDown += new MouseEventHandler(this.On_btnHelp_MouseDown);
    this._btnHelp.MouseEnter += new EventHandler(this.On_btnHelp_MouseEnter);
    this._btnHelp.MouseLeave += new EventHandler(this.On_btnHelp_MouseLeave);
    this._btnHelp.MouseUp += new MouseEventHandler(this.On_btnHelp_MouseUp);
    componentResourceManager.ApplyResources((object) this._lbText, "_lbText");
    this._lbText.Name = "_lbText";
    componentResourceManager.ApplyResources((object) this._scResult, "_scResult");
    this._scResult.Name = "_scResult";
    this._scResult.Panel1.Controls.Add((Control) this._trvGroup);
    componentResourceManager.ApplyResources((object) this._scResult.Panel1, "_scResult.Panel1");
    this._scResult.Panel2.Controls.Add((Control) this._dgvResult);
    componentResourceManager.ApplyResources((object) this._scResult.Panel2, "_scResult.Panel2");
    componentResourceManager.ApplyResources((object) this._trvGroup, "_trvGroup");
    this._trvGroup.HideSelection = false;
    this._trvGroup.Name = "_trvGroup";
    this._trvGroup.BeforeExpand += new TreeViewCancelEventHandler(this.On_trvGroup_BeforeExpand);
    this._trvGroup.AfterSelect += new TreeViewEventHandler(this.On_trvGroup_AfterSelect);
    this._dgvResult.AllowUserToAddRows = false;
    this._dgvResult.AllowUserToDeleteRows = false;
    this._dgvResult.AllowUserToResizeRows = false;
    this._dgvResult.BackgroundColor = SystemColors.Window;
    this._dgvResult.BorderStyle = BorderStyle.Fixed3D;
    this._dgvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    componentResourceManager.ApplyResources((object) this._dgvResult, "_dgvResult");
    this._dgvResult.MultiSelect = false;
    this._dgvResult.Name = "_dgvResult";
    this._dgvResult.RowHeadersVisible = false;
    this._dgvResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._dgvResult.CellFormatting += new DataGridViewCellFormattingEventHandler(this.On_dgvResult_CellFormatting);
    this._dgvResult.DoubleClick += new EventHandler(this.On_dgvResult_DoubleClick);
    componentResourceManager.ApplyResources((object) this._tlpBottom, "_tlpBottom");
    this._tlpBottom.Controls.Add((Control) this._btnPrev, 1, 0);
    this._tlpBottom.Controls.Add((Control) this._btnSearch, 2, 0);
    this._tlpBottom.Name = "_tlpBottom";
    componentResourceManager.ApplyResources((object) this._btnPrev, "_btnPrev");
    this._btnPrev.Name = "_btnPrev";
    this._btnPrev.UseVisualStyleBackColor = true;
    this._btnPrev.Click += new EventHandler(this.On_btnPrev_Click);
    componentResourceManager.ApplyResources((object) this._btnSearch, "_btnSearch");
    this._btnSearch.Name = "_btnSearch";
    this._btnSearch.UseVisualStyleBackColor = true;
    this._btnSearch.Click += new EventHandler(this.On_btnSearch_Click);
    this._pnlSecondPage.Controls.Add((Control) this._scResult);
    this._pnlSecondPage.Controls.Add((Control) this._pnlFindInResult);
    this._pnlSecondPage.Controls.Add((Control) this._pnlBottom);
    componentResourceManager.ApplyResources((object) this._pnlSecondPage, "_pnlSecondPage");
    this._pnlSecondPage.Name = "_pnlSecondPage";
    this._pnlFindInResult.Controls.Add((Control) this._lbFindInResult);
    this._pnlFindInResult.Controls.Add((Control) this._txtFindInResult);
    componentResourceManager.ApplyResources((object) this._pnlFindInResult, "_pnlFindInResult");
    this._pnlFindInResult.Name = "_pnlFindInResult";
    componentResourceManager.ApplyResources((object) this._lbFindInResult, "_lbFindInResult");
    this._lbFindInResult.Name = "_lbFindInResult";
    componentResourceManager.ApplyResources((object) this._txtFindInResult, "_txtFindInResult");
    this._txtFindInResult.Name = "_txtFindInResult";
    this._txtFindInResult.TextChanged += new EventHandler(this.On_txtFindInResult_TextChanged);
    this._pnlBottom.Controls.Add((Control) this._lbCount);
    this._pnlBottom.Controls.Add((Control) this._chbShowAll);
    this._pnlBottom.Controls.Add((Control) this._btnInTable);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._lbCount, "_lbCount");
    this._lbCount.Name = "_lbCount";
    componentResourceManager.ApplyResources((object) this._chbShowAll, "_chbShowAll");
    this._chbShowAll.Checked = true;
    this._chbShowAll.CheckState = CheckState.Checked;
    this._chbShowAll.Name = "_chbShowAll";
    this._chbShowAll.UseVisualStyleBackColor = true;
    this._chbShowAll.CheckedChanged += new EventHandler(this.On_chbShowAll_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._btnInTable, "_btnInTable");
    this._btnInTable.Name = "_btnInTable";
    this._btnInTable.UseVisualStyleBackColor = true;
    this._btnInTable.Click += new EventHandler(this.On_btnInTable_Click);
    componentResourceManager.ApplyResources((object) this._pnlFirstPage, "_pnlFirstPage");
    this._pnlFirstPage.Controls.Add((Control) this._scConditions);
    this._pnlFirstPage.Name = "_pnlFirstPage";
    this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._pnlFirstPage);
    this.Controls.Add((Control) this._pnlSecondPage);
    this.Controls.Add((Control) this._tlpBottom);
    this.DoubleBuffered = true;
    this.FloatingSize = new Size(766, 496);
    this.Name = nameof (FindByIndexView);
    this.PersistState = false;
    this.ShowImageInDocumentTab = true;
    this.BeforeFirstShown += new EventHandler(this.OnBeforeFirstShown);
    this._scConditions.Panel1.ResumeLayout(false);
    this._scConditions.Panel2.ResumeLayout(false);
    this._scConditions.EndInit();
    this._scConditions.ResumeLayout(false);
    this._tcIndexes.ResumeLayout(false);
    this._tpTree.ResumeLayout(false);
    this._tpList.ResumeLayout(false);
    this._gbConditions.ResumeLayout(false);
    this._pnlConditions.ResumeLayout(false);
    this._gbRate.ResumeLayout(false);
    this._gbRate.PerformLayout();
    this._pnlHelp.ResumeLayout(false);
    this._pnlHelp.PerformLayout();
    this._pnlControl.ResumeLayout(false);
    this._scResult.Panel1.ResumeLayout(false);
    this._scResult.Panel2.ResumeLayout(false);
    this._scResult.EndInit();
    this._scResult.ResumeLayout(false);
    ((ISupportInitialize) this._dgvResult).EndInit();
    this._tlpBottom.ResumeLayout(false);
    this._pnlSecondPage.ResumeLayout(false);
    this._pnlFindInResult.ResumeLayout(false);
    this._pnlFindInResult.PerformLayout();
    this._pnlBottom.ResumeLayout(false);
    this._pnlBottom.PerformLayout();
    this._pnlFirstPage.ResumeLayout(false);
    ((ISupportInitialize) this._bsFilter).EndInit();
    this.ResumeLayout(false);
  }
}
