// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.TableView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions;
using GridViewExtensions.GridFilterFactories;
using GridViewExtensions.GridFilters;
using GridViewExtensions.GridFilters.EnumerationSources;
using Intermech.Client.Core.Thumbnail;
using Intermech.Controls;
using Intermech.Controls.Thumbnail;
using Intermech.Extensions;
using Intermech.Imbase.API;
using Intermech.Imbase.Editors;
using Intermech.Imbase.PDF;
using Intermech.Imbase.Printing;
using Intermech.Imbase.Selection;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.Search.UI;
using PdfiumViewer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Imbase.Controls;

public class TableView : UserControl, IFilterTarget
{
  private static bool _extenderVisible;
  private static bool _extenderAtTop = true;
  private static Dictionary<long, int> _filterPanelWidth = new Dictionary<long, int>();
  private static Guid MULTI_GUID_SORTING = new Guid("CC038C7E-BDFD-47FF-B9F3-5902F3D4FFAA");
  private static IPicturesCache _picturesCache;
  private object _picture;
  private StringFormat _sf;
  private static int SplitterDistance = 150;
  private const string F_GUID = "-12";
  private const string F_KEY = "-2";
  private System.IServiceProvider _services;
  private long _objectId = -1;
  private int _objectType;
  private DataTable _createdObjects;
  private List<long> _usedKeys = new List<long>();
  private INamedImageList _nil;
  private int _imgObjectIndex = -1;
  private int _imgEnabledUserFilterIndex = -1;
  private List<long> _selectedRows;
  private List<DataGridViewColumn> _recordRefColumns = new List<DataGridViewColumn>();
  private Dictionary<string, DataGridViewColumn> _objectRefColumns = new Dictionary<string, DataGridViewColumn>();
  private List<DataGridViewColumn> _imageColumns = new List<DataGridViewColumn>();
  private List<DataGridViewColumn> _noteColumns = new List<DataGridViewColumn>();
  private string _rtf;
  private Dictionary<string, string> _recordRefMap = new Dictionary<string, string>();
  private Dictionary<string, string> _objectRefMap = new Dictionary<string, string>();
  protected DataTable _dataTable;
  protected DataView _dataView;
  private string _internalFilter = string.Empty;
  private string _rowFilter = string.Empty;
  private List<IComboBoxFiller> _comboBoxFillers = new List<IComboBoxFiller>();
  private string _userFilter = string.Empty;
  private string _usingFilter = string.Empty;
  private string _filter = string.Empty;
  private Guid _objectGuid = Guid.Empty;
  private DataGridView.HitTestInfo _popupHitTest;
  protected List<AttributeValues> _tableAttributes = new List<AttributeValues>();
  protected List<AttributeValues> _linkAttributes = new List<AttributeValues>();
  protected AttributeTypeProperties[] _rowsAttProps;
  private DataGridViewPrinting _dgvPrinter;
  private string _tableName = string.Empty;
  private static bool _hideUnusedRecords;
  private Image _imgFilterEmpty;
  private Image _imgFilterFull;
  private bool _isSubscribeToViewChanged;
  private ThumbnailRenderer _renderer;
  private IPicturesCache _cache;
  private List<Intermech.Client.Core.Thumbnail.ThumbnailItem> _items;
  private bool _layoutNeed;
  private SplitterPanel _imagesPanel;
  private int _isRestoreSelectionMode;
  private DataGridViewCheckBoxColumn _checkColumn;
  private bool _lockDisplayIndexChanged;
  private bool _lockFormatting;
  private Dictionary<long, Color> _colorizedRows;
  private DataGridViewColumn _userFilterCheckColumn;
  private Image _imgDisabledUserFilter;
  private Image _imgEnabledUserFilter;
  private UserFilter _userSetting;
  private FullTextSearchGridFilterFactory _fullTextFactory;
  private IGridFilterFactory _defaultGridFilterFactory;
  private List<ColumnViewInfo> _viewInfo;
  private XmlNode _gSettings;
  private XmlNode _uSettings;
  private XmlNode _rSettings;
  private XmlNode _currNode;
  private DisplayMode _currDisplayType;
  private bool _fromSettingDlg;
  private bool _isAdminRole;
  private ImbaseUserParams _userParams;
  private INotificationService _notificationService;
  private bool _cancelEdit;
  private bool _ignoreSaveFilter;
  private const int ScanSize = 256 /*0x0100*/;
  private int _needDistance;
  private long[] _checkedRecIds;
  private IContainer components;
  private bool _disposed;
  private DoubleBufferedDataGridView _grid;
  private ContextMenuStrip _contextMenu;
  private ToolStripMenuItem _miAutoResize;
  private ToolStripMenuItem _miDataSize;
  private ToolStripMenuItem _miViewSetting;
  private ToolStripMenuItem mnCreateObject;
  private ToolStripSeparator toolStripMenuItem2;
  private PrintDocument _printDoc;
  private ToolTip _tt;
  private SplitContainer _splitContainer;
  private ImageList imageList1;
  private ToolStripMenuItem mnObjectProps;
  private ToolStripMenuItem mnCopyCurrentCell;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem mnOpenInNewWindow;
  private ToolStripMenuItem mnSynch;
  private DataGridFilterExtender _extender;
  private ToolStrip toolStrip1;
  private ToolStripButton _tbPrint;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripDropDownButton tbFilter;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripButton tbSaveDisplaySettings;
  private ToolStripLabel toolStripLabel1;
  private ToolStripComboBox _cbDisplayMode;
  private ToolStripLabel toolStripLabel2;
  private ToolStripComboBox _cmbRole;
  private ToolStripMenuItem mnUsedRecords;
  private ToolStripMenuItem mnDataFilter;
  private ToolStripSeparator toolStripSeparator4;
  private ToolStripMenuItem mnFilterOptions;
  private ToolStripSeparator toolStripSeparator5;
  private ToolStripMenuItem mnCleanFilter;
  private ToolStripLabel lbRecords;
  private SplitContainer splitContainer1;
  private LayoutedGridFilterFactoryControl leftFilterFactory;
  private ToolStripTextBox toolStripTextBox1;
  private ToolStripMenuItem mnFilterTop;
  private ToolStripMenuItem mnFilterLeft;
  private ToolStripDropDownButton tbUserFilter;
  private ToolStripMenuItem miApplyUserFilter;
  private ToolStripSeparator toolStripSeparator6;
  private ToolStripMenuItem miDeleteUserFilter;
  private ToolStripMenuItem miEditUresFilter;
  private ToolStripMenuItem mnShow;
  private ToolStripSeparator mnShowtoolStripSeparator;
  private ToolStripDropDownButton tbNormaCS;
  private ToolStripMenuItem tbLaunchNormaCS;
  private ToolStripMenuItem tbFindByNumberNCS;
  private ToolStripMenuItem tbFindByNameNCS;
  private ToolStripMenuItem tbFindByTextNCS;
  private ToolStripMenuItem mnNormaCS;
  private ToolStripMenuItem mnLaunchNormaCS;
  private ToolStripMenuItem mnFindByNumberNCS;
  private ToolStripMenuItem mnFindByNameNCS;
  private ToolStripMenuItem mnFindByTextNCS;
  private SplitContainer _noteImageContainer;
  private RichTextBox _richTextBox;
  private ToolStripMenuItem mnCopy;
  private ToolStripMenuItem mnCopyRecordCode;
  private ToolStripMenuItem mnCopyRecordKey;
  private ToolStripMenuItem mnCopyRecordGuid;

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int LockWindowUpdate(IntPtr hWnd);

  private static IPicturesCache PicturesCache
  {
    get
    {
      return TableView._picturesCache ?? (TableView._picturesCache = ServicesManager.GetService(typeof (IPicturesCache)) as IPicturesCache);
    }
  }

  [DefaultValue(-1)]
  public long ObjectId
  {
    get => this._objectId;
    set
    {
      if (this._objectId == value || this.DesignMode)
        return;
      this.Attach(value);
    }
  }

  [Browsable(false)]
  [DefaultValue(false)]
  public ImFollowSelectMode FollowSelectMode { get; set; } = ImFollowSelectMode.imfsmFirstRow;

  [Browsable(false)]
  internal long TableId { get; private set; } = -1;

  internal string TableName
  {
    get
    {
      int count = this._tableAttributes.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this._tableAttributes[index].AttributeID == Intermech.Imbase.Consts.ImbaseInternalTableNameAttID)
          return this._tableAttributes[index].Values[0].ToString();
      }
      return string.Empty;
    }
  }

  [Browsable(false)]
  internal long LinkId { get; private set; } = -1;

  [Browsable(false)]
  public DataTable Table => this._dataTable;

  public DataView DataView => this._dataView;

  [Browsable(false)]
  public bool CheckedRecord(int rowIndex)
  {
    if (this._checkColumn == null)
      return false;
    object obj = this._grid.Rows[rowIndex].Cells[this._checkColumn.Index].Value;
    return obj != null && Convert.ToBoolean(obj);
  }

  [Browsable(false)]
  public void CheckRecord(int rowIndex, bool checkState)
  {
    if (this._checkColumn == null)
      return;
    this._grid.Rows[rowIndex].Cells[this._checkColumn.Index].Value = (object) checkState;
  }

  [Browsable(false)]
  public long[] CheckedRecords
  {
    get
    {
      List<long> longList = new List<long>();
      if (this._checkColumn != null)
      {
        int count = this._grid.Rows.Count;
        for (int index = 0; index < count; ++index)
        {
          if (this.CheckedRecord(index))
          {
            long int64 = Convert.ToInt64(this._grid.Rows[index].Cells["-2"].Value);
            longList.Add(int64);
          }
        }
      }
      longList.Sort();
      return longList.ToArray();
    }
  }

  [Browsable(false)]
  public AttributeTypeProperties[] RowAttProps => this._rowsAttProps;

  public string Filter
  {
    get => this.CalculateFilterString();
    set
    {
      this._filter = this.RenameFields(value, true);
      this.ApplyFilter();
    }
  }

  public long RecordId
  {
    get
    {
      DataGridViewRow dataGridViewRow = (DataGridViewRow) null;
      if (this._dataTable == null || this._dataView.Table.Rows.Count == 0)
        return -1;
      if (this._grid.SelectedRows.Count > 0)
        dataGridViewRow = this._grid.SelectedRows[0];
      return dataGridViewRow == null ? -1L : Convert.ToInt64(dataGridViewRow.Cells["-2"].Value);
    }
    set
    {
      foreach (DataGridViewRow row in (IEnumerable) this._grid.Rows)
      {
        if ((long) Convert.ToInt32(row.Cells["-2"].Value) == value)
        {
          this._grid.CurrentCell = row.Cells[this.FirstVisibleColumnIndex()];
          break;
        }
      }
    }
  }

  public Guid RecordGuid
  {
    get
    {
      DataGridViewRow dataGridViewRow = (DataGridViewRow) null;
      if (this._dataTable == null || this._dataView.Table.Rows.Count == 0)
        return Guid.Empty;
      if (this._grid.SelectedRows.Count > 0)
        dataGridViewRow = this._grid.SelectedRows[0];
      return dataGridViewRow == null ? Guid.Empty : new Guid(dataGridViewRow.Cells["-12"].Value.ToString());
    }
    set
    {
      string str = value.ToString();
      foreach (DataGridViewRow row in (IEnumerable) this._grid.Rows)
      {
        if (str.Equals(row.Cells["-12"].Value.ToString()))
        {
          this._grid.CurrentCell = row.Cells[this.FirstVisibleColumnIndex()];
          break;
        }
      }
    }
  }

  internal int[] ColumnsOrder { get; private set; }

  public DataGridView Grid => (DataGridView) this._grid;

  public ContextMenuStrip ImContextMenu => this._contextMenu;

  internal bool ActivateDisplaySettings { get; set; }

  public TableView()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    IntPtr handle = this._grid.Handle;
    this._defaultGridFilterFactory = this._extender.FilterFactory;
    this._imagesPanel = this._noteImageContainer.Panel2;
    this._items = new List<Intermech.Client.Core.Thumbnail.ThumbnailItem>();
    this._cache = ServicesManager.GetService(typeof (IPicturesCache)) as IPicturesCache;
    this._renderer = new ThumbnailRenderer(this.Font, new Intermech.Client.Core.Thumbnail.GetImageHandler(this.OnGetImage))
    {
      Items = this._items
    };
    this._renderer.RedrawRequired += new RedrawEventHandler(this.Renderer_RedrawRequired);
    this._sf = new StringFormat();
    this._splitContainer.SplitterDistance = TableView.SplitterDistance;
    this._selectedRows = new List<long>(32 /*0x20*/);
    this._nil = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    if (this._nil == null)
      return;
    int index1 = this._nil.ImageIndex("imgRecFilter");
    this._imgObjectIndex = this._nil.ImageIndex("imgObject");
    this._imgEnabledUserFilterIndex = this._nil.ImageIndex("imgEnabledUserFilter");
    this.tbFilter.Image = this._imgFilterEmpty = index1 != -1 ? this._nil.ImageList.Images[index1] : (Image) null;
    int index2 = this._nil.ImageIndex("imgRecFilterAdd");
    this._imgFilterFull = index2 != -1 ? this._nil.ImageList.Images[index2] : (Image) null;
    int index3 = this._nil.ImageIndex("imgDisabledUserFilter");
    this._imgDisabledUserFilter = index3 != -1 ? this._nil.ImageList.Images[index3] : (Image) null;
    int index4 = this._nil.ImageIndex("imgEnabledUserFilter");
    this._imgEnabledUserFilter = index4 != -1 ? this._nil.ImageList.Images[index4] : (Image) null;
    int index5 = this._nil.ImageIndex("imgSave");
    this.tbSaveDisplaySettings.Image = index5 != -1 ? this._nil.ImageList.Images[index5] : (Image) null;
    this.tbNormaCS.Visible = this.toolStripSeparator2.Visible = ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, false) != null;
    int index6 = this._nil.ImageIndex("imgNormaCS");
    this.tbNormaCS.Image = index6 != -1 ? this._nil.ImageList.Images[index6] : (Image) null;
    this.tbLaunchNormaCS.Image = index6 != -1 ? this._nil.ImageList.Images[index6] : (Image) null;
    int index7 = this._nil.ImageIndex("imgPrint");
    this._tbPrint.Image = index7 != -1 ? this._nil.ImageList.Images[index7] : (Image) null;
    this.mnDataFilter.Checked = TableView._extenderVisible;
    this.mnFilterTop.Checked = TableView._extenderAtTop;
    this.mnFilterLeft.Checked = !this.mnFilterTop.Checked;
    this.tbUserFilter.Image = this._imgDisabledUserFilter;
    this.RegisterGlobalDelegates();
    this.SubscribeEvents();
    this._fullTextFactory = new FullTextSearchGridFilterFactory(this.toolStripTextBox1.TextBox);
    this._grid.SetFilterTarget((IFilterTarget) this);
    this.ShowGridExtender();
    this.LoadDisplayModeDataSource();
    this.LoadRolesDataSource();
    this.ApplyVisualSettings();
  }

  public event EventHandler FocusedChanged;

  public event EventHandler ItemDoubleClick;

  public event EventHandler ItemEnterPress;

  public event CheckEventHandler ItemChecked;

  public event CreateObjectEventHandler CreateObject;

  private void On_miViewSetting_Click(object sender, EventArgs e)
  {
    if (this._cbDisplayMode.ComboBox?.SelectedValue == null)
      return;
    this.ActivateDisplaySettings = true;
    this.StoreDisplaySettings();
    if (this._currDisplayType == DisplayMode.RoleMode)
    {
      using (ImbaseTableViewEditorForm tableViewEditorForm = new ImbaseTableViewEditorForm(this._rSettings, this._cmbRole.ComboBox?.DataSource, this._cmbRole.ComboBox?.SelectedValue))
      {
        if (tableViewEditorForm.ShowDialog() != DialogResult.Yes)
          return;
        this._rSettings.InnerXml = tableViewEditorForm.Settings.InnerXml;
        this._fromSettingDlg = true;
        if (this._cmbRole.ComboBox != null)
          this._cmbRole.ComboBox.SelectedValue = tableViewEditorForm.SelectedRole;
        this._fromSettingDlg = false;
        this.SetCurrentNode();
      }
    }
    else
    {
      using (ImbaseTableViewEditorForm tableViewEditorForm = new ImbaseTableViewEditorForm(this._currNode))
      {
        if (tableViewEditorForm.ShowDialog() != DialogResult.Yes)
          return;
        this._currNode.InnerXml = tableViewEditorForm.Settings.InnerXml;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.MapColumns(sessionKeeper.Session);
    this.ActiveControl = (Control) this._grid;
    this.On_btnSaveDisplaySettings_Enabled((object) this._grid, (DataGridViewColumnEventArgs) null);
  }

  private void OnSync_Click(object sender, EventArgs e)
  {
    Dictionary<int, List<long>> fromSelectedRows = this.GetCreatedObjectsFromSelectedRows();
    if (fromSelectedRows == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IImbaseSynchObjectsService)) is IImbaseSynchObjectsService)
      {
        using (SynchObjectsBaseForm synchObjectsBaseForm = new SynchObjectsBaseForm(fromSelectedRows))
        {
          int num = (int) synchObjectsBaseForm.ShowDialog();
        }
      }
      else
      {
        int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_SynchService_Null"), LocalizationHolder.rm.GetString("Imbase_SynchObjects_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
  }

  private Dictionary<int, List<long>> GetCreatedObjectsFromSelectedRows()
  {
    Dictionary<int, List<long>> fromSelectedRows = (Dictionary<int, List<long>>) null;
    if (this._createdObjects != null && this._grid.SelectedRows.Count > 0)
    {
      int recNumColumnIndex = this._createdObjects.Columns.IndexOf(Convert.ToString(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID));
      if (recNumColumnIndex > -1)
      {
        fromSelectedRows = new Dictionary<int, List<long>>(1);
        int columnIndex1 = this._createdObjects.Columns.IndexOf(Convert.ToString(-2));
        int columnIndex2 = this._createdObjects.Columns.IndexOf(Convert.ToString(-7));
        foreach (DataGridViewRow selectedRow in (BaseCollection) this._grid.SelectedRows)
        {
          long recId = Convert.ToInt64(selectedRow.Cells["-2"].Value);
          EnumerableRowCollection<DataRow> source = this._createdObjects.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => !DBNull.Value.Equals(x[recNumColumnIndex]) && Convert.ToInt64(x[recNumColumnIndex]) == recId));
          if (source != null)
          {
            int int32 = Convert.ToInt32(source.ElementAt<DataRow>(0)[columnIndex2]);
            long objectID = Convert.ToInt64(source.ElementAt<DataRow>(0)[columnIndex1]);
            if (source.Count<DataRow>() > 1)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
                if (!objectInfo.Empty)
                {
                  IDBObject objectByVersionsRule = sessionKeeper.Session.GetObjectByVersionsRule(objectInfo.ID, "cad005aa-306c-11d8-b4e9-00304f19f545", false);
                  if (objectByVersionsRule != null)
                    objectID = objectByVersionsRule.ObjectID;
                }
              }
            }
            if (fromSelectedRows.ContainsKey(int32))
              fromSelectedRows[int32].Add(objectID);
            else
              fromSelectedRows.Add(int32, new List<long>()
              {
                objectID
              });
          }
        }
      }
    }
    return fromSelectedRows;
  }

  private bool CanSynch
  {
    get
    {
      bool canSynch = false;
      if (this._createdObjects != null && this._grid.SelectedRows.Count > 0)
      {
        int index = this._createdObjects.Columns.IndexOf(Convert.ToString(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID));
        if (index > -1)
        {
          foreach (DataGridViewRow selectedRow in (BaseCollection) this._grid.SelectedRows)
          {
            long recId = Convert.ToInt64(selectedRow.Cells["-2"].Value);
            if (this._createdObjects.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => x[index] != null && x[index] != DBNull.Value && Convert.ToInt64(x[index]) == recId)) != null)
            {
              canSynch = true;
              break;
            }
          }
        }
      }
      return canSynch;
    }
  }

  public string RowFilter
  {
    get => this._rowFilter;
    set
    {
      this._rowFilter = value;
      foreach (ComboBoxFiller comboBoxFiller in this._comboBoxFillers)
        comboBoxFiller.Refill = true;
      this.ApplyFilter();
    }
  }

  public bool CanSetFilter => true;

  private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
  {
    if (e.RowIndex == -1)
      return;
    this.OnItemDoubleClick(e);
  }

  private void OnGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
  {
    DataGridView dataGridView = (DataGridView) sender;
    if (e.ColumnIndex > -1 && e.RowIndex > -1)
      dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = e.Exception.Message;
    e.ThrowException = false;
  }

  private void Grid_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return)
      return;
    this.OnEnterPress();
    e.Handled = true;
  }

  private void Grid_SelectionChanged(object sender, EventArgs e)
  {
    if (this._lockFormatting)
      return;
    if (this._items != null && this._items.Count > 0 && this._grid.SelectedRows.Count > 0)
    {
      DataGridViewRow selectedRow = this._grid.SelectedRows[0];
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (Intermech.Client.Core.Thumbnail.ThumbnailItem thumbnailItem in this._items)
        {
          if (thumbnailItem.Tag is DataGridViewColumn tag)
          {
            thumbnailItem.Clear();
            object obj = selectedRow.Cells[tag.Index].Value;
            if (obj != null)
            {
              string str = obj.ToString();
              if (obj is ValuesArray valuesArray && str.Length > 0)
                str = valuesArray.GetValue(0) as string;
              if (str != null && GuidHelper.IsGuid(str))
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(new Guid(str));
                if (!objectInfo.Empty)
                  thumbnailItem.SetValues(thumbnailItem.Name, Math.Abs(objectInfo.ObjectID), objectInfo.ObjectTypeID);
              }
            }
          }
        }
      }
      this._imagesPanel.Invalidate(true);
      Application.DoEvents();
    }
    if (!this._richTextBox.IsDisposed)
    {
      string str1 = this._rtf;
      if (this._noteColumns.Count > 0 && this._grid.SelectedRows.Count > 0)
      {
        DataGridViewRow selectedRow = this._grid.SelectedRows[0];
        DataGridViewColumn noteColumn = this._noteColumns[0];
        if (noteColumn != null)
        {
          object obj = selectedRow.Cells[noteColumn.Index].Value;
          if (obj != null)
          {
            string str2 = obj.ToString();
            if (obj is ValuesArray valuesArray && str2.Length > 0)
              str2 = valuesArray.GetValue(0) as string;
            if (GuidHelper.IsGuid(str2))
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                if (str2 != null)
                {
                  IDBAttribute attributeById = sessionKeeper.Session.GetObject(new Guid(str2), false)?.GetAttributeByID(Intermech.Imbase.Consts.ImbaseNoteAttID);
                  if (attributeById != null)
                    str1 = Convert.ToString(attributeById.Value);
                }
              }
            }
          }
        }
      }
      if (str1 == null)
        str1 = string.Empty;
      if (str1.StartsWith("{\\rtf1"))
        this._richTextBox.Rtf = str1;
      else
        this._richTextBox.Text = str1;
    }
    EventHandler focusedChanged = this.FocusedChanged;
    if (focusedChanged == null)
      return;
    focusedChanged((object) this, (EventArgs) new TableView.SelEventArgs(this._isRestoreSelectionMode > 0));
  }

  private void OnShowUsedRecords_Click(object sender, EventArgs e)
  {
    TableView._hideUnusedRecords = this.mnUsedRecords.Checked;
    this.SetDisabledRecordFilter();
  }

  private void ActivateUnusedRecordsMode()
  {
    if (this.mnUsedRecords.Checked != TableView._hideUnusedRecords)
      this.mnUsedRecords.Checked = TableView._hideUnusedRecords;
    else
      this.OnShowUsedRecords_Click((object) null, EventArgs.Empty);
  }

  private void OnPrint_Click(object sender, EventArgs e)
  {
    this._printDoc.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
    this._dgvPrinter = new DataGridViewPrinting((DataGridView) this._grid, this._printDoc, this._tableName);
    using (PrintPreviewDlg printPreviewDlg = new PrintPreviewDlg())
    {
      printPreviewDlg.Document = this._printDoc;
      printPreviewDlg.WindowState = FormWindowState.Maximized;
      printPreviewDlg.PageOrientationVisible = true;
      int num = (int) printPreviewDlg.ShowDialog();
    }
  }

  private void On_grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
  {
    if (!this._userParams.SaveColumnsState)
      return;
    if (this._cbDisplayMode.ComboBox?.SelectedValue.ToString() == DisplayMode.GeneralMode.ToString())
    {
      if (!this._isAdminRole)
        return;
      this.tbSaveDisplaySettings.Enabled = true;
    }
    else
      this.tbSaveDisplaySettings.Enabled = true;
  }

  private void On_miAutoResize_Click(object sender, EventArgs e)
  {
    this.ActivateDisplaySettings = true;
    this._grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
  }

  private void On_miDataSize_Click(object sender, EventArgs e)
  {
    this.ActivateDisplaySettings = true;
    this._grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
  }

  private void On_printDoc_BeginPrint(object sender, PrintEventArgs e)
  {
    this._dgvPrinter.BeginPrint();
  }

  private void On_printDoc_EndPrint(object sender, PrintEventArgs e) => this._dgvPrinter.EndPrint();

  private void On_printDoc_PrintPage(object sender, PrintPageEventArgs e)
  {
    if (this._printDoc.PrintController == null)
      return;
    e.HasMorePages = this._dgvPrinter.DrawDataGridView(e.Graphics);
  }

  private void On_CreateObject()
  {
    if (this.CreateObject != null)
    {
      CreateObjectEventHandler createObject = this.CreateObject;
      if (createObject == null)
        return;
      createObject(this.LinkId, this.RecordId, this._services);
    }
    else
      CreateObjectForm.ShowCreateObjectDialog(this.LinkId, this.RecordId, this._services);
  }

  private void OnOpenInNewWindow_Click(object sender, EventArgs e)
  {
    List<long> objectIds = this.GetObjectIds(this.RecordId);
    if (objectIds.Count <= 0)
      return;
    IDescriptor rootDescriptor;
    if (objectIds.Count == 1)
    {
      rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectIds[0]);
    }
    else
    {
      int num = -1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectIds[0]);
        if (!objectInfo.Empty)
          num = objectInfo.ObjectTypeID;
      }
      Dictionary<int, List<long>> objectIDs = new Dictionary<int, List<long>>()
      {
        {
          num,
          objectIds
        }
      };
      rootDescriptor = (IDescriptor) new DictDescriptor(Intermech.Navigator.Consts.CategoryAllObjectTypes, 0, "Созданные объекты", objectIDs);
    }
    Utils.OpenNewWindow(rootDescriptor, (System.IServiceProvider) null);
  }

  private void TableView_Resize(object sender, EventArgs e)
  {
    Rectangle clientRectangle = this.ClientRectangle;
    int height = this.toolStrip1.Height;
    clientRectangle.Y = height + 1;
    clientRectangle.Height = clientRectangle.Height - height - 2;
    this._splitContainer.Bounds = clientRectangle;
  }

  private void OnEditUresFilter_Click(object sender, EventArgs e)
  {
    this._userFilterCheckColumn.Visible = this.miEditUresFilter.Checked;
  }

  private void OnDeleteUserFilter_Click(object sender, EventArgs e)
  {
    string caption = LocalizationHolder.rm.GetString("IMB_WARN");
    switch (MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_Remove_User_Filter"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
    {
      case DialogResult.Yes:
        if (this._userFilterCheckColumn == null)
          break;
        this._dataTable.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToBoolean(row["F_USERFILTER"]))).ToList<DataRow>().ForEach((Action<DataRow>) (row => row["F_USERFILTER"] = (object) false));
        this._dataTable.AcceptChanges();
        if (!this._userParams.SaveUserFilterState)
          break;
        this.tbSaveDisplaySettings.Enabled = true;
        break;
    }
  }

  private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
  {
    this.OnImagesPanel_Resize((object) null, EventArgs.Empty);
    this._splitContainer.Panel1.Invalidate(true);
  }

  private void OnGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
  {
    if (this._checkColumn.Visible && e.ColumnIndex == this._checkColumn.Index)
    {
      if (this._grid.CurrentRow == null)
        return;
      object obj = this._grid.CurrentRow.Cells[e.ColumnIndex].Value;
      bool currentValue = false;
      if (obj != null)
        currentValue = (bool) obj;
      if (this.ItemChecked != null)
      {
        TableView.CheckEventArgs ce = new TableView.CheckEventArgs(currentValue);
        CheckEventHandler itemChecked = this.ItemChecked;
        if (itemChecked != null)
          itemChecked((object) this, ce);
        if (ce.Cancel)
        {
          this._cancelEdit = true;
          return;
        }
      }
      this.CheckRecord(this._grid.CurrentRow.Index, !currentValue);
    }
    else
    {
      if (!this._userFilterCheckColumn.Visible || e.ColumnIndex != this._userFilterCheckColumn.Index || this._grid.CurrentRow == null || e.RowIndex == -1 || !this._userParams.SaveUserFilterState)
        return;
      this.tbSaveDisplaySettings.Enabled = true;
    }
  }

  private void _contextMenu_Opening(object sender, CancelEventArgs e)
  {
    this.mnCreateObject.Enabled = !this.DisabledRecord();
    this.mnObjectProps.Enabled = this.mnOpenInNewWindow.Enabled = this._usedKeys.Contains(this.RecordId);
    Point client = this._grid.PointToClient(Control.MousePosition);
    this._popupHitTest = this._grid.HitTest(client.X, client.Y);
    if (this._popupHitTest.Type == DataGridViewHitTestType.RowHeader && this._popupHitTest.RowIndex != -1)
      this._grid.CurrentCell = this._grid.Rows[this._popupHitTest.RowIndex].Cells[this.FirstVisibleColumnIndex()];
    this.mnCopyCurrentCell.Enabled = this._popupHitTest.Type == DataGridViewHitTestType.Cell;
    this.mnSynch.Visible = this.CanSynch;
    this.mnShow.Visible = this.mnShowtoolStripSeparator.Visible = this.CanShow;
    this.mnNormaCS.Visible = ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, false) != null;
    int index = this._nil.ImageIndex("imgNormaCS");
    this.mnNormaCS.Image = this.mnLaunchNormaCS.Image = index != -1 ? this._nil.ImageList.Images[index] : (Image) null;
    if (this.RecordId == -1L)
    {
      this.mnCopy.Visible = false;
    }
    else
    {
      this.mnCopy.Visible = true;
      this.mnCopyRecordCode.Text = $"Ключ записи: {ImbaseHelper.MakeInternalImbaseKey(this.LinkId, this.RecordId)}";
      this.mnCopyRecordKey.Text = $"Код записи: {this.RecordId}";
      this.mnCopyRecordGuid.Text = $"GUID записи: {this.RecordGuid}";
    }
  }

  private bool CanShow
  {
    get
    {
      int columnIndex = this._popupHitTest.ColumnIndex;
      int rowIndex = this._popupHitTest.RowIndex;
      int attId;
      if (rowIndex != -1 && columnIndex != -1 && TableView.GetAttributeIdFromColumn(this._grid.Columns[columnIndex], out attId))
      {
        IMSAttributeType attributeType = MetaDataHelperService.Instance.GetAttributeType(attId);
        if ((attributeType != null ? (attributeType.FieldType == FieldTypes.ftObjectLink ? 1 : 0) : 0) != 0 && this._grid.Rows[rowIndex].Cells[columnIndex].Value != DBNull.Value)
          return true;
      }
      return false;
    }
  }

  private void On_mnShow_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int columnIndex = this._popupHitTest.ColumnIndex;
      int rowIndex = this._popupHitTest.RowIndex;
      int attId;
      if (rowIndex == -1 || columnIndex == -1 || !TableView.GetAttributeIdFromColumn(this._grid.Columns[columnIndex], out attId))
        return;
      IMSAttributeType attributeType = MetaDataHelperService.Instance.GetAttributeType(attId);
      if ((attributeType != null ? (attributeType.FieldType != FieldTypes.ftObjectLink ? 1 : 0) : 1) != 0)
        return;
      object obj = this._grid.Rows[rowIndex].Cells[columnIndex].Value;
      if (obj == DBNull.Value)
        return;
      string str = Convert.ToString(obj);
      if (!GuidHelper.IsGuid(str))
        return;
      Guid guid = new Guid(str);
      if (sessionKeeper.Session.GetObject(guid, false) == null)
        return;
      Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(guid), (System.IServiceProvider) null);
    }
  }

  private void OnGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
  {
    if (this._grid.RowHeadersVisible && e.ColumnIndex == -1 && e.RowIndex != -1 && e.RowIndex < this._dataTable.Rows.Count)
    {
      long int64 = Convert.ToInt64(this._grid.Rows[e.RowIndex].Cells["-2"].Value);
      e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
      Rectangle cellBounds = e.CellBounds;
      if (this._usedKeys.BinarySearch(int64) >= 0 && this._nil != null && this._imgObjectIndex != -1)
      {
        int x = cellBounds.Right - 20;
        int y = cellBounds.Top + (cellBounds.Height - 16 /*0x10*/) / 2;
        if (x > 12)
          this._nil.ImageList.Draw(e.Graphics, new Point(x, y), this._imgObjectIndex);
      }
      e.Handled = true;
    }
    if (this._userFilterCheckColumn == null || e.ColumnIndex != this._userFilterCheckColumn.Index || e.RowIndex != -1 || this._nil == null)
      return;
    e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
    Rectangle cellBounds1 = e.CellBounds;
    int x1 = cellBounds1.Left + (cellBounds1.Width - 16 /*0x10*/) / 2;
    int y1 = cellBounds1.Top + (cellBounds1.Height - 16 /*0x10*/) / 2;
    this._nil.ImageList.Draw(e.Graphics, new Point(x1, y1), this._imgEnabledUserFilterIndex);
    e.Handled = true;
  }

  private void OnObjectProps_Click(object sender, EventArgs e)
  {
    List<long> objectIds = this.GetObjectIds(this.RecordId);
    if (objectIds.Count <= 0)
      return;
    long objectId = objectIds[0];
    if (objectIds.Count > 1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectId);
        if (!objectInfo.Empty)
        {
          IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(objectInfo.ID, false);
          if (objectBaseVersionById != null)
            objectId = objectBaseVersionById.ObjectID;
        }
      }
    }
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, objectId);
  }

  private void Grid_CellToolTipTextNeeded(
    object sender,
    DataGridViewCellToolTipTextNeededEventArgs e)
  {
    if (e.ColumnIndex == -1 && e.RowIndex != -1)
    {
      long int64 = Convert.ToInt64(this._grid.Rows[e.RowIndex].Cells["-2"].Value);
      if (this._usedKeys.BinarySearch(int64) < 0)
        return;
      DataRow[] dataRowArray = this._createdObjects.Select($"[{Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID}]={int64}");
      if (dataRowArray.Length == 0)
        return;
      string str1 = string.Empty;
      string str2 = string.Empty;
      foreach (DataRow dataRow in dataRowArray)
      {
        str2 = str2 + str1 + $"{dataRow[2]}. Идентификатор объекта :{dataRow[0]}";
        str1 = Environment.NewLine;
      }
      e.ToolTipText = str2;
    }
    else
    {
      if (e.ColumnIndex <= -1 || e.RowIndex == -1)
        return;
      if (this._objectRefColumns.ContainsKey(this._grid.Columns[e.ColumnIndex].Name))
      {
        string str = this._grid[e.ColumnIndex, e.RowIndex].FormattedValue?.ToString();
        if (str == null || str.IndexOf("; ") == -1)
          return;
        e.ToolTipText = str.Replace("; ", Environment.NewLine);
      }
      else
      {
        DataGridViewRow row = this._grid.Rows[e.RowIndex];
        e.ToolTipText = string.Format(LocalizationHolder.rm.GetString("Imbase_RecordCode"), (object) ImbaseHelper.MakeInternalImbaseKey(this.LinkId, Convert.ToInt64(row.Cells["-2"].Value)));
      }
    }
  }

  private void CopyCurrentCell_Click(object sender, EventArgs e)
  {
    if (this._popupHitTest.Type != DataGridViewHitTestType.Cell || this._popupHitTest.RowIndex == -1 || this._popupHitTest.ColumnIndex == -1)
      return;
    DataGridViewCell cell = this._grid.Rows[this._popupHitTest.RowIndex].Cells[this._popupHitTest.ColumnIndex];
    if (cell == null)
      return;
    Clipboard.SetText(cell.Value.ToString(), TextDataFormat.UnicodeText);
  }

  private void TableView_VisibleChanged(object sender, EventArgs e)
  {
    this.CheckedRecords_ActiveChanged();
  }

  private void CheckedRecords_ContextChanged()
  {
    this.LoadCheckedRows(false);
    this._grid.Invalidate();
  }

  private void CheckedRecords_ActiveChanged()
  {
    if (this._checkColumn == null)
      return;
    this._checkColumn.Visible = Intermech.Imbase.Views.CheckedRecords.Active;
    this._grid.Invalidate();
  }

  private void Renderer_RedrawRequired(object sender, BoundsEventArgs e)
  {
    if (this._items.Count <= 0)
      return;
    this._imagesPanel.Invalidate(true);
  }

  public bool DisabledRecord()
  {
    DataGridViewRow row = (DataGridViewRow) null;
    if (this._dataTable == null || this._dataView.Table.Rows.Count == 0)
      return true;
    if (this._grid.SelectedRows.Count > 0)
      row = this._grid.SelectedRows[0];
    return row != null && this.DisabledRecord(row);
  }

  public bool DisabledRecord(DataGridViewRow row)
  {
    object obj = row.Cells["F_APPLICABILITY"].Value;
    return obj != null && !Convert.ToBoolean(obj);
  }

  internal void ApplyShowFields(string showFields, string sortOrder)
  {
    DataGridViewColumnCollection columns = this._grid.Columns;
    string[] strArray = (string[]) null;
    string[] collection = (string[]) null;
    if (showFields.Length > 0)
    {
      bool flag;
      if (showFields[0] == '#')
      {
        flag = false;
        int result;
        if (int.TryParse(showFields.Substring(1), out result) && result != 0)
        {
          List<string> stringList = new List<string>();
          for (DataGridViewColumn dataGridViewColumn = columns.GetFirstColumn(DataGridViewElementStates.Visible); dataGridViewColumn != null; dataGridViewColumn = columns.GetNextColumn(dataGridViewColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None))
          {
            int attId;
            if (TableView.GetAttributeIdFromColumn(dataGridViewColumn, out attId))
            {
              int index = TableEditor.IndexOfAttProp(attId, this._rowsAttProps);
              if (index != -1 && (this._rowsAttProps[index].Options & (AttributeOptions) result) != AttributeOptions.None)
                stringList.Add(attId.ToString());
            }
          }
          collection = stringList.ToArray();
        }
      }
      else
      {
        collection = this.RenameFields(showFields, false).Split(';');
        flag = true;
        if (!string.IsNullOrEmpty(sortOrder))
          strArray = this.RenameFields(sortOrder, false).Split(';');
      }
      this._dataView.Sort = string.Empty;
      if (collection != null && collection.Length != 0)
      {
        List<string> stringList = new List<string>((IEnumerable<string>) collection);
        foreach (DataGridViewColumn dataGridViewColumn in (BaseCollection) columns)
        {
          if (dataGridViewColumn.Name.Length > 0)
          {
            if (dataGridViewColumn.Name[0] == '-')
              dataGridViewColumn.Visible = false;
            else if (dataGridViewColumn.Tag != null)
            {
              int num = stringList.IndexOf(dataGridViewColumn.Tag.ToString());
              if (num == -1)
              {
                dataGridViewColumn.Visible = false;
              }
              else
              {
                dataGridViewColumn.Visible = true;
                if (flag)
                  dataGridViewColumn.DisplayIndex = num + 1;
              }
            }
          }
        }
      }
    }
    if (strArray == null)
      strArray = collection;
    if (strArray == null)
      return;
    string empty = string.Empty;
    int length = strArray.Length;
    for (int index = 0; index < length; ++index)
    {
      string str = strArray[index];
      if (str.Length != 0)
      {
        if (empty.Length > 0)
          empty += ",";
        empty += str;
      }
    }
    if (empty.Length == 0)
      return;
    this._dataView.Sort = empty;
    this._checkColumn.Visible = false;
  }

  public void Detach()
  {
    if (this._isSubscribeToViewChanged)
    {
      this._grid.ColumnDisplayIndexChanged -= new DataGridViewColumnEventHandler(this.On_btnSaveDisplaySettings_Enabled);
      this._grid.ColumnWidthChanged -= new DataGridViewColumnEventHandler(this.On_btnSaveDisplaySettings_Enabled);
      this._isSubscribeToViewChanged = false;
    }
    if (!this.tbSaveDisplaySettings.Enabled)
      return;
    this.SaveAllDispalyParams();
    this.tbSaveDisplaySettings.Enabled = false;
  }

  internal void LocateByFilter(string filterStr)
  {
    try
    {
      DataRow[] dataRowArray = this._dataTable.Select(this.RenameFields(filterStr, true));
      if (dataRowArray.Length == 0)
        return;
      this.LocateRowByGuid(dataRowArray[0]["-12"].ToString());
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
  }

  internal void LocateRowByGuid(string rowGuid)
  {
    if (string.IsNullOrEmpty(rowGuid))
      return;
    foreach (DataGridViewRow row in (IEnumerable) this._grid.Rows)
    {
      if (rowGuid.Equals(row.Cells["-12"].Value.ToString()))
      {
        this._grid.CurrentCell = row.Cells[this.FirstVisibleColumnIndex()];
        break;
      }
    }
  }

  internal void SetServices(System.IServiceProvider services) => this._services = services;

  internal void DisableViewSettingMenuItem() => this._miViewSetting.Enabled = false;

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    if (this._isSubscribeToViewChanged)
      return;
    this._grid.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(this.On_btnSaveDisplaySettings_Enabled);
    this._grid.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.On_btnSaveDisplaySettings_Enabled);
    this._isSubscribeToViewChanged = true;
  }

  private void AttachExtender() => this._extender.DataGridView = (DataGridView) this._grid;

  private void LoadExtenderFilters()
  {
    if (!TableView._extenderVisible)
      return;
    XmlAttribute attribute = this._currNode?.SelectSingleNode("Condition")?.Attributes?["Value"];
    if (attribute == null || string.IsNullOrEmpty(attribute.Value))
      return;
    using (StringReader stringReader = new StringReader(attribute.Value))
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (ConditionItem[]));
      try
      {
        if (!(xmlSerializer.Deserialize((TextReader) stringReader) is ConditionItem[] conditionItemArray) || !((IEnumerable<ConditionItem>) conditionItemArray).Any<ConditionItem>())
          return;
        this._ignoreSaveFilter = true;
        this._extender.SetFilters(conditionItemArray);
      }
      catch
      {
      }
      finally
      {
        this._ignoreSaveFilter = false;
      }
    }
  }

  private void ShowGridExtender()
  {
    this.DetachExtender();
    if (this.mnDataFilter.Checked)
    {
      if (this.mnFilterLeft.Checked)
      {
        this._defaultGridFilterFactory.GridFilterCreated -= new GridFilterEventHandler(this.FilterFactory_GridFilterCreated);
        this._extender.FilterFactory = (IGridFilterFactory) this.leftFilterFactory;
        this.leftFilterFactory.GridFilterCreated += new GridFilterEventHandler(this.FilterFactory_GridFilterCreated);
        this.splitContainer1.Panel1Collapsed = false;
        this._grid.Dock = DockStyle.Fill;
      }
      else
      {
        this._extender.FilterFactory = this._defaultGridFilterFactory;
        this._defaultGridFilterFactory.GridFilterCreated += new GridFilterEventHandler(this.FilterFactory_GridFilterCreated);
        this.leftFilterFactory.GridFilterCreated -= new GridFilterEventHandler(this.FilterFactory_GridFilterCreated);
        this.splitContainer1.Panel1Collapsed = true;
        this._grid.Dock = DockStyle.None;
        this._grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      }
      this._extender.Operator = GridViewExtensions.LogicalOperators.And;
      this._grid.Top = this._extender.FilterBoxPosition == FilterPosition.Off ? 0 : Math.Abs(this._extender.ControlBounds.Height);
      this._grid.Height = this._grid.Parent.Height - this._grid.Top - 1;
      this._grid.Width = this._grid.Parent.ClientRectangle.Width;
      this.AttachExtender();
    }
    else
    {
      this.splitContainer1.Panel1Collapsed = true;
      this.leftFilterFactory.GridFilterCreated -= new GridFilterEventHandler(this.FilterFactory_GridFilterCreated);
      this._extender.FilterFactory = (IGridFilterFactory) this._fullTextFactory;
      this._extender.Operator = GridViewExtensions.LogicalOperators.Or;
      this._grid.Dock = DockStyle.Fill;
      this._grid.Top = 0;
      this.AttachExtender();
    }
  }

  private void mnDataFilter_Click(object sender, EventArgs e)
  {
    TableView._extenderVisible = this.mnDataFilter.Checked;
    this.ShowGridExtender();
    this.LoadExtenderFilters();
  }

  private void OnFilterTop_Click(object sender, EventArgs e)
  {
    if (sender == this.mnFilterLeft)
    {
      if (this.mnFilterLeft.Checked)
        return;
      this.mnFilterLeft.Checked = true;
      TableView._extenderAtTop = false;
      this.mnFilterTop.Checked = false;
    }
    else
    {
      if (sender != this.mnFilterTop || this.mnFilterTop.Checked)
        return;
      TableView._extenderAtTop = true;
      this.mnFilterLeft.Checked = false;
      this.mnFilterTop.Checked = true;
    }
    this.ShowGridExtender();
  }

  private void FilterFactory_GridFilterCreated(object sender, GridFilterEventArgs args)
  {
    DataGridViewColumn column1 = args.Column;
    int attId;
    if (string.IsNullOrEmpty(column1.DataPropertyName) || !TableView.GetAttributeIdFromColumn(column1, out attId) || attId < 0)
      return;
    IGridFilter gridFilter = args.GridFilter;
    if (gridFilter == null)
      return;
    string dataPropertyName = column1.DataPropertyName;
    if (this._dataTable.Columns.IndexOf(dataPropertyName) == -1)
      return;
    DataColumn column2 = this._dataTable.Columns[dataPropertyName];
    if (column2.ExtendedProperties.ContainsKey((object) "F_VISIBLE") && Convert.ToBoolean(column2.ExtendedProperties[(object) "F_VISIBLE"]) && this.mnFilterLeft.Checked && typeof (EmptyGridFilter) != args.GridFilter.GetType())
    {
      args.GridFilter = (IGridFilter) new EmptyGridFilter();
    }
    else
    {
      System.Type o = column2.DataType;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = column2.ExtendedProperties.ContainsKey((object) "F_LIST");
      if (o.Equals(typeof (ValuesArray)))
      {
        o = column2.ExtendedProperties[(object) "dataType"] as System.Type;
        flag2 = true;
      }
      if (typeof (string).Equals(o) || typeof (Guid).Equals(o))
        flag1 = true;
      if (!this._objectRefColumns.ContainsKey(column1.Name) && !this._recordRefColumns.Contains(column1) && !flag3 && !flag2)
      {
        if (args.GridFilter == null || args.GridFilter.ComboBox == null)
          return;
        ComboBoxFiller comboBoxFiller = new ComboBoxFiller(args.GridFilter, column2, (Dictionary<string, string>) null);
        comboBoxFiller.Action = new Action(comboBoxFiller.SimpleComboAction);
        this._comboBoxFillers.Add((IComboBoxFiller) comboBoxFiller);
      }
      else
      {
        Dictionary<string, string> dict = (Dictionary<string, string>) null;
        ObjectStringMapEnumerationSource enumerationSource = new ObjectStringMapEnumerationSource();
        if (this._objectRefColumns.ContainsKey(column1.Name))
          dict = this._objectRefMap;
        else if (this._recordRefColumns.Contains(column1))
          dict = this._recordRefMap;
        else if (flag3 && column2.ExtendedProperties[(object) "F_LIST"] is IMSAttributeType extendedProperty && extendedProperty.PossibleValues != null)
        {
          int count = extendedProperty.PossibleValues.Count;
          dict = new Dictionary<string, string>();
          for (int index = 0; index < count; ++index)
          {
            object possibleValue = extendedProperty.PossibleValues[index];
            string str = Convert.ToString(extendedProperty.PossibleValuesDescriptions[index]);
            if (string.IsNullOrEmpty(str))
              str = possibleValue.ToString();
            dict.Add(possibleValue.ToString(), str);
          }
        }
        EnumerationGridFilter filter = new EnumerationGridFilter((IEnumerationSource) enumerationSource);
        filter.UseQuotes = flag1;
        if (flag2)
          filter.UseLike = true;
        filter.UseCustomFilterPlacement = gridFilter.UseCustomFilterPlacement;
        args.GridFilter = (IGridFilter) filter;
        ComboBoxFiller comboBoxFiller = new ComboBoxFiller((IGridFilter) filter, column2, dict);
        comboBoxFiller.Action = new Action(comboBoxFiller.DictionaryAction);
        this._comboBoxFillers.Add((IComboBoxFiller) comboBoxFiller);
      }
    }
  }

  private void DetachExtender() => this._extender.DataGridView = (DataGridView) null;

  private void OnCleanFilter_Click(object sender, EventArgs e) => this._extender.ClearFilters();

  private void Extender_BeforeFiltersChanging(object sender, EventArgs e)
  {
    this._lockDisplayIndexChanged = true;
  }

  private void Extender_AfterFiltersChanged(object sender, EventArgs e)
  {
    this._lockDisplayIndexChanged = false;
    if (this._ignoreSaveFilter)
      return;
    DisplayMode result;
    Enum.TryParse<DisplayMode>(this._cbDisplayMode.ComboBox?.SelectedValue.ToString(), out result);
    if (!this._userParams.SaveFilterState)
      return;
    this.tbSaveDisplaySettings.Enabled = result != DisplayMode.GeneralMode || this._isAdminRole;
  }

  private void MainRecordsThreadProc(object stateInfo)
  {
    if (!(stateInfo is List<string> state1))
      return;
    int count = state1.Count;
    if (count < 256 /*0x0100*/)
    {
      ThreadPool.QueueUserWorkItem(new WaitCallback(this.RecordThreadProc), (object) state1);
    }
    else
    {
      List<string> state2 = new List<string>(256 /*0x0100*/);
      for (int index = 0; index < count; ++index)
      {
        state2.Add(state1[index]);
        if (state2.Count > (int) byte.MaxValue)
        {
          ThreadPool.QueueUserWorkItem(new WaitCallback(this.RecordThreadProc), (object) state2);
          state2 = new List<string>(256 /*0x0100*/);
        }
      }
      if (state2.Count <= 0)
        return;
      ThreadPool.QueueUserWorkItem(new WaitCallback(this.RecordThreadProc), (object) state2);
    }
  }

  private void RecordThreadProc(object stateInfo)
  {
    if (this._grid == null || this._grid.IsDisposed)
      return;
    List<string> keyValues = stateInfo as List<string>;
    try
    {
      if (keyValues != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          Dictionary<string, string> dictionary = CadmechHelper.GetServer(session).NameRecordReferences(session.SessionGUID, keyValues);
          lock (this._recordRefMap)
          {
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
              if (!this._recordRefMap.ContainsKey(keyValuePair.Key))
                this._recordRefMap.Add(keyValuePair.Key, keyValuePair.Value);
            }
          }
        }
      }
      if (this._grid.IsDisposed)
        return;
      this._grid.Invoke((Delegate) new EventHandler(this.RepaintGrid));
    }
    catch (Exception ex)
    {
    }
  }

  private void MainObjectsThreadProc(object stateInfo)
  {
    if (!(stateInfo is List<string> state1))
      return;
    int count = state1.Count;
    if (count < 256 /*0x0100*/)
    {
      ThreadPool.QueueUserWorkItem(new WaitCallback(this.ObjectThreadProc), (object) state1);
    }
    else
    {
      List<string> state2 = new List<string>(256 /*0x0100*/);
      for (int index = 0; index < count; ++index)
      {
        state2.Add(state1[index]);
        if (state2.Count > (int) byte.MaxValue)
        {
          ThreadPool.QueueUserWorkItem(new WaitCallback(this.ObjectThreadProc), (object) state2);
          state2 = new List<string>(256 /*0x0100*/);
        }
      }
      if (state2.Count <= 0)
        return;
      ThreadPool.QueueUserWorkItem(new WaitCallback(this.ObjectThreadProc), (object) state2);
    }
  }

  private void ObjectThreadProc(object stateInfo)
  {
    List<string> keyValues = stateInfo as List<string>;
    try
    {
      if (keyValues != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          Dictionary<string, string> dictionary = CadmechHelper.GetServer(session).NameObjectReferences(session.SessionGUID, keyValues);
          lock (this._objectRefMap)
          {
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
              if (!this._objectRefMap.ContainsKey(keyValuePair.Key))
                this._objectRefMap.Add(keyValuePair.Key, keyValuePair.Value);
            }
          }
        }
      }
      this._grid.Invoke((Delegate) new EventHandler(this.RepaintGrid));
    }
    catch (Exception ex)
    {
    }
  }

  private void RepaintGrid(object sender, EventArgs e)
  {
    if (this._disposed)
      return;
    this._grid.Invalidate();
  }

  private void LoadSelectedRows()
  {
    this._selectedRows.Clear();
    try
    {
      if (this.LinkId == -1L)
        return;
      long[] collection = SelectedRecords.Select(this.LinkId);
      if (collection == null || collection.Length == 0)
        return;
      this._selectedRows.AddRange((IEnumerable<long>) collection);
    }
    finally
    {
      this.ViewSelectRows();
    }
  }

  private void LoadCheckedRows(bool loading)
  {
    try
    {
      if (this.LinkId == -1L)
        return;
      long[] collection = Intermech.Imbase.Views.CheckedRecords.Select(this.LinkId);
      List<long> longList = new List<long>();
      if (collection != null)
        longList.AddRange((IEnumerable<long>) collection);
      longList.Sort();
      if (this._checkColumn == null || longList.Count == 0 & loading)
        return;
      int count = this._grid.Rows.Count;
      for (int index = 0; index < count; ++index)
      {
        long int64 = Convert.ToInt64(this._grid.Rows[index].Cells["-2"].Value);
        this.CheckRecord(index, longList.BinarySearch(int64) >= 0);
      }
    }
    finally
    {
      this.ViewSelectRows();
    }
  }

  private void ViewSelectRows()
  {
    switch (this.FollowSelectMode)
    {
      case ImFollowSelectMode.ifsmClear:
        ++this._isRestoreSelectionMode;
        try
        {
          this._grid.ClearSelection();
          break;
        }
        finally
        {
          --this._isRestoreSelectionMode;
        }
      case ImFollowSelectMode.imfsmFirstRow:
        this.ViewSelectFirstRow();
        break;
      case ImFollowSelectMode.imfsmAllRows:
        this.ViewSelectAllRows();
        break;
    }
  }

  private void ViewSelectAllRows()
  {
    if (!this._grid.MultiSelect)
    {
      this.ViewSelectFirstRow();
    }
    else
    {
      ++this._isRestoreSelectionMode;
      try
      {
        if (this._selectedRows.Count == 0)
        {
          this._grid.ClearSelection();
        }
        else
        {
          DataGridViewRowCollection rows = this._grid.Rows;
          int count = rows.Count;
          this._grid.SelectAll();
          for (int index = 0; index < count; ++index)
          {
            if (!this._selectedRows.Contains((long) Convert.ToInt32(rows[index].Cells["-2"].Value)))
              rows[index].Selected = false;
          }
        }
      }
      catch
      {
      }
      finally
      {
        --this._isRestoreSelectionMode;
      }
    }
  }

  private void ViewSelectFirstRow()
  {
    ++this._isRestoreSelectionMode;
    try
    {
      DataGridViewRowCollection rows = this._grid.Rows;
      int count = rows.Count;
      if (this._selectedRows.Count <= 0)
        return;
      for (int index = 0; index < count; ++index)
      {
        if (this._selectedRows.Contains((long) Convert.ToInt32(rows[index].Cells["-2"].Value)))
        {
          this._grid.CurrentCell = rows[index].Cells[this.FirstVisibleColumnIndex()];
          this._grid.FirstDisplayedScrollingRowIndex = index;
          break;
        }
      }
    }
    catch
    {
    }
    finally
    {
      --this._isRestoreSelectionMode;
    }
  }

  private void SelectedRecords_ContextChanged()
  {
    this.LoadSelectedRows();
    this._grid.Invalidate();
  }

  private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
  {
    if (this._lockFormatting)
      return;
    try
    {
      if (this._dataTable == null)
        return;
      if (this._recordRefColumns.Contains(this._grid.Columns[e.ColumnIndex]))
      {
        object empty = e.Value;
        if (empty is ValuesArray valuesArray)
          empty = valuesArray.GetValue(0);
        if (empty == null)
          empty = (object) string.Empty;
        string str;
        if (this._recordRefMap.TryGetValue(empty.ToString(), out str))
        {
          e.Value = (object) str;
          e.FormattingApplied = true;
          e.CellStyle.ForeColor = Color.DarkBlue;
        }
      }
      else if (this._objectRefColumns.ContainsKey(this._grid.Columns[e.ColumnIndex].Name))
      {
        string str = (string) null;
        object empty = e.Value;
        bool flag = false;
        if (empty is ValuesArray valuesArray)
        {
          object[] array = valuesArray.GetArray();
          StringBuilder stringBuilder = new StringBuilder(128 /*0x80*/);
          int length = array.Length;
          for (int index = 0; index < length; ++index)
          {
            string key = array[index]?.ToString();
            if (!string.IsNullOrWhiteSpace(key) && this._objectRefMap.TryGetValue(key, out key))
            {
              flag = true;
              if (stringBuilder.Length > 0)
                stringBuilder.Append("; ");
              stringBuilder.Append(key);
            }
          }
          str = stringBuilder.ToString();
        }
        if (empty == null)
          empty = (object) string.Empty;
        if (flag || this._objectRefMap.TryGetValue(empty.ToString(), out str))
        {
          e.Value = (object) str;
          e.FormattingApplied = true;
          e.CellStyle.ForeColor = Color.DarkBlue;
        }
      }
      DataGridViewColumn column1 = this._grid.Columns[e.ColumnIndex];
      if (column1.IsDataBound)
      {
        DataColumn column2 = this._dataTable.Columns[column1.DataPropertyName];
        if (column2 != null)
        {
          if (column2.ExtendedProperties.ContainsKey((object) "F_DISPLAY"))
          {
            object extendedProperty = column2.ExtendedProperties[(object) "F_DISPLAY"];
            if (extendedProperty != null)
            {
              e.Value = extendedProperty;
              e.FormattingApplied = true;
            }
          }
          else if (column2.ExtendedProperties.ContainsKey((object) "F_LIST"))
          {
            if (column2.ExtendedProperties[(object) "F_LIST"] is IMSAttributeType extendedProperty1 && extendedProperty1.PossibleValues != null)
            {
              int index = extendedProperty1.PossibleValues.IndexOf(e.Value);
              if (index != -1)
              {
                string str = Convert.ToString(extendedProperty1.PossibleValuesDescriptions[index]);
                if (!string.IsNullOrEmpty(str))
                {
                  e.Value = (object) str;
                  e.FormattingApplied = true;
                }
              }
            }
          }
          else if (column2.ExtendedProperties.Contains((object) "F_ONLY_DATE") && column2.DataType == typeof (DateTime) && e.Value != null && !DBNull.Value.Equals(e.Value))
          {
            DateTime dateTime = (DateTime) e.Value;
            e.Value = (object) dateTime.ToShortDateString();
            e.FormattingApplied = true;
          }
        }
      }
      if (e.RowIndex != -1 && e.RowIndex < this._dataTable.Rows.Count && this._grid.Columns.Contains("-2"))
      {
        long int64 = Convert.ToInt64(this._grid.Rows[e.RowIndex].Cells["-2"].Value);
        if (this._selectedRows.Count > 0 && this._selectedRows.Contains(int64))
        {
          if (this._grid.Focused)
            e.CellStyle.SelectionForeColor = Color.Yellow;
          e.CellStyle.BackColor = Color.FromArgb(214, 231, (int) byte.MaxValue);
        }
        if (DynamicSelectionHelper.IsSelected(this.LinkId, int64))
        {
          e.CellStyle.BackColor = Color.Wheat;
          e.CellStyle.SelectionForeColor = this._grid.Focused ? Color.Wheat : Color.Brown;
        }
        if (this._colorizedRows != null && this._colorizedRows.ContainsKey(int64))
        {
          if (this._grid.Focused)
          {
            e.CellStyle.BackColor = this._colorizedRows[int64];
            e.CellStyle.SelectionForeColor = e.CellStyle.BackColor;
          }
          else
          {
            e.CellStyle.BackColor = this._colorizedRows[int64];
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
          }
        }
      }
      if (!this.DisabledRecord(this._grid.Rows[e.RowIndex]))
        return;
      e.CellStyle.SelectionForeColor = Color.LightGray;
      e.CellStyle.ForeColor = Color.DarkGray;
    }
    catch
    {
    }
  }

  private void SubscribeComboBoxes()
  {
    this._cbDisplayMode.SelectedIndexChanged += new EventHandler(this.OnDisplayMode_SelectedIndexChanged);
    this._cmbRole.SelectedIndexChanged += new EventHandler(this.OnRole_SelectedIndexChanged);
  }

  private void UnSubscribeComboBoxes()
  {
    this._cbDisplayMode.SelectedIndexChanged -= new EventHandler(this.OnDisplayMode_SelectedIndexChanged);
    this._cmbRole.SelectedIndexChanged -= new EventHandler(this.OnRole_SelectedIndexChanged);
  }

  private void LoadDisplayModeDataSource()
  {
    DataTable dataTable = new DataTable();
    dataTable.Columns.Add(new DataColumn("Type"));
    dataTable.Columns.Add(new DataColumn("Caption"));
    dataTable.Rows.Add((object) DisplayMode.GeneralMode, (object) LocalizationHolder.rm.GetString("Imbase_DisplaySettings_GeneralMode"));
    dataTable.Rows.Add((object) DisplayMode.PersonalMode, (object) LocalizationHolder.rm.GetString("Imbase_DisplaySettings_PersonalMode"));
    dataTable.Rows.Add((object) DisplayMode.RoleMode, (object) LocalizationHolder.rm.GetString("Imbase_DisplaySettings_RoleMode"));
    if (this._cbDisplayMode.ComboBox == null)
      return;
    this._cbDisplayMode.ComboBox.ValueMember = "Type";
    this._cbDisplayMode.ComboBox.DisplayMember = "Caption";
    this._cbDisplayMode.ComboBox.DataSource = (object) dataTable;
  }

  private void OnDisplayMode_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._gSettings == null)
      return;
    this.StoreDisplaySettings();
    if (this._cbDisplayMode.ComboBox?.SelectedValue == null)
      return;
    Enum.TryParse<DisplayMode>(this._cbDisplayMode.ComboBox.SelectedValue.ToString(), out this._currDisplayType);
    if (this._currDisplayType != DisplayMode.GeneralMode)
      this._miViewSetting.Enabled = true;
    this._cmbRole.Enabled = this._currDisplayType == DisplayMode.RoleMode;
    this.SetCurrentNode();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.MapColumns(sessionKeeper.Session);
  }

  private void LoadRolesDataSource()
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      DataTable source = sk.Session.GetObjectCollection(new Guid("cad00007-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) ((IEnumerable<RoleProperties>) sk.Session.GetRolesList(sk.Session.UserID)).Select<RoleProperties, long>((System.Func<RoleProperties, long>) (x => x.RoleID)).ToArray<long>(), Intermech.Kernel.Search.LogicalOperators.NONE, 0, false)
      }, new object[3]
      {
        (object) -12,
        (object) -2,
        (object) -50
      }));
      if (source == null)
        return;
      source.Columns[0].ColumnName = "Guid";
      source.Columns[1].ColumnName = "ID";
      source.Columns[2].ColumnName = "Caption";
      if (this._cmbRole.ComboBox == null)
        return;
      this._cmbRole.ComboBox.DisplayMember = "Caption";
      this._cmbRole.ComboBox.ValueMember = "Guid";
      this._cmbRole.ComboBox.DataSource = (object) source;
      DataRow dataRow = source.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["ID"]) == sk.Session.RoleID));
      if (dataRow == null)
        return;
      this._cmbRole.ComboBox.SelectedValue = dataRow["Guid"];
    }
  }

  private void OnRole_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._gSettings == null || this._fromSettingDlg)
      return;
    this.StoreDisplaySettings();
    if (this._cmbRole.ComboBox?.SelectedValue == null)
      return;
    this.SetCurrentNode();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.MapColumns(sessionKeeper.Session);
  }

  private void LoadDisplaySettings(
    IUserSession session,
    Guid objGuid,
    out Guid sortGuid,
    out string sortMode)
  {
    sortGuid = TableView.MULTI_GUID_SORTING;
    sortMode = string.Empty;
    ITablesDisplayService customService = session.GetCustomService(typeof (ITablesDisplayService)) as ITablesDisplayService;
    if (objGuid == Guid.Empty || customService == null || this._cmbRole.ComboBox?.SelectedValue == null)
    {
      this._cmbRole.Enabled = this._cbDisplayMode.Enabled = this._miViewSetting.Enabled = false;
    }
    else
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(session.UserID);
      this._currDisplayType = customService.GetDisplayModeForUser(objectInfo.VersionGuid);
      this._cbDisplayMode.Enabled = true;
      if (this._cbDisplayMode.ComboBox != null)
      {
        this._cbDisplayMode.ComboBox.SelectedValue = (object) this._currDisplayType;
        this._cmbRole.Enabled = this._currDisplayType == DisplayMode.RoleMode;
        this._miViewSetting.Enabled = this._currDisplayType != DisplayMode.GeneralMode || session.RoleID == session.IdentHelper.AdminRoleID;
        XmlDocument xmlDocument1 = new XmlDocument();
        xmlDocument1.InnerXml = customService.GetGeneralSettingsForObject(objGuid);
        XmlDocument xmlDocument2 = xmlDocument1;
        this._gSettings = (XmlNode) xmlDocument2.CreateElement(xmlDocument2.FirstChild.Name);
        this._gSettings.InnerXml = xmlDocument2.FirstChild.InnerXml;
        xmlDocument2.InnerXml = customService.GetObjectSettingsForUser(objGuid, objectInfo.VersionGuid);
        XmlElement element = xmlDocument2.CreateElement(xmlDocument2.FirstChild.Name);
        element.SetAttribute("Guid", objectInfo.VersionGuid.ToString());
        this._uSettings = (XmlNode) element;
        this._uSettings.InnerXml = xmlDocument2.FirstChild.InnerXml;
        List<Guid> roleGuids;
        if (this._cmbRole.ComboBox.DataSource is DataTable dataSource)
        {
          if (!dataSource.Columns.Contains("Guid"))
            return;
          roleGuids = new List<Guid>(dataSource.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) dataSource.Rows)
          {
            string str = Convert.ToString(row["Guid"]);
            if (GuidHelper.IsGuid(str))
              roleGuids.Add(new Guid(str));
          }
        }
        else
        {
          roleGuids = new List<Guid>(1);
          objectInfo = session.GetObjectInfo(session.RoleID);
          roleGuids.Add(!objectInfo.Empty ? objectInfo.VersionGuid : Guid.Empty);
        }
        xmlDocument2.InnerXml = customService.GetObjectSettingsForRoles(objGuid, roleGuids);
        this._rSettings = (XmlNode) xmlDocument2.CreateElement(xmlDocument2.FirstChild.Name);
        this._rSettings.InnerXml = xmlDocument2.FirstChild.InnerXml;
      }
      sortGuid = customService.GetSortedColumnGuid(objGuid, objectInfo.VersionGuid, out sortMode);
      this.SetCurrentNode();
    }
  }

  private void StoreDisplaySettings()
  {
    if (this.TableId == 0L || this._currNode == null)
      return;
    this._currNode.InnerXml = string.Empty;
    XmlDocument ownerDocument = this._currNode.OwnerDocument;
    if (ownerDocument == null)
      return;
    if (this._userParams.SaveColumnsState)
    {
      for (int index = 0; index < this._grid.Columns.Count; ++index)
      {
        Guid empty = Guid.Empty;
        DataGridViewColumn column = this._grid.Columns[index];
        if (this.GetColumnGuid(Convert.ToInt32(column.Tag), this._rowsAttProps, ref empty))
        {
          XmlElement element = ownerDocument.CreateElement("Column");
          element.SetAttribute("Guid", empty.ToString());
          XmlElement xmlElement1 = element;
          int num = column.DisplayIndex;
          string str1 = num.ToString();
          xmlElement1.SetAttribute("Index", str1);
          XmlElement xmlElement2 = element;
          num = column.Width;
          string str2 = num.ToString();
          xmlElement2.SetAttribute("Width", str2);
          element.SetAttribute("Visible", column.Visible.ToString());
          this._currNode.AppendChild((XmlNode) element);
        }
      }
    }
    if (!this._userParams.SaveFilterState)
      return;
    ConditionItem[] filters = this._extender.GetFilters();
    if (filters.Length == 0)
      return;
    using (StringWriter stringWriter = new StringWriter())
    {
      new XmlSerializer(typeof (ConditionItem[])).Serialize((TextWriter) stringWriter, (object) filters);
      string str = stringWriter.ToString();
      XmlElement element = ownerDocument.CreateElement("Condition");
      element.SetAttribute("Value", str);
      this._currNode.AppendChild((XmlNode) element);
    }
  }

  private void SaveDisplaySettings()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo1 = sessionKeeper.Session.GetObjectInfo(this.ObjectId);
      if (objectInfo1.Empty || !(objectInfo1.VersionGuid != Guid.Empty))
        return;
      QuickObjectInfo objectInfo2 = sessionKeeper.Session.GetObjectInfo(sessionKeeper.Session.UserID);
      string mode = string.Empty;
      Guid multiGuidSorting = TableView.MULTI_GUID_SORTING;
      if (this._userParams.SaveColumnsState)
        mode = this._grid.GetSortString();
      string gSettings = this._isAdminRole ? this._gSettings.OuterXml : string.Empty;
      ServiceUtils.GetService<ITablesDisplayService>((object) sessionKeeper.Session, true).SaveSettingsForObject(objectInfo1.VersionGuid, objectInfo2.VersionGuid, multiGuidSorting, mode, this._currDisplayType, gSettings, this._uSettings.OuterXml, this._rSettings.OuterXml);
    }
  }

  private void On_btnSaveDisplaySettings_Enabled(object sender, DataGridViewColumnEventArgs e)
  {
    if (this.tbSaveDisplaySettings.Enabled || e != null && (e.Column == this._checkColumn || e.Column == this._userFilterCheckColumn) || this._lockDisplayIndexChanged || !this._userParams.SaveColumnsState || !(sender is DoubleBufferedDataGridView) || !this.ActivateDisplaySettings)
      return;
    DisplayMode result;
    Enum.TryParse<DisplayMode>(this._cbDisplayMode.ComboBox?.SelectedValue.ToString(), out result);
    this.tbSaveDisplaySettings.Enabled = result != DisplayMode.GeneralMode || this._isAdminRole;
  }

  private void On_btnSaveDisplaySettings_Click(object sender, EventArgs e)
  {
    this.SaveAllDispalyParams();
    this.tbSaveDisplaySettings.Enabled = false;
  }

  private void SaveAllDispalyParams()
  {
    this.StoreDisplaySettings();
    this.SaveDisplaySettings();
    this.SaveUserFilter();
  }

  private void LoadViewInfo()
  {
    if (this._currNode != null)
    {
      this._viewInfo = new List<ColumnViewInfo>(this._currNode.ChildNodes.Count);
      foreach (XmlNode childNode in this._currNode.ChildNodes)
      {
        XmlAttribute attribute1 = childNode.Attributes?["Guid"];
        if (attribute1 != null && GuidHelper.IsGuid(attribute1.Value))
        {
          int index = TableEditor.IndexOfAttProp(new Guid(attribute1.Value), this._rowsAttProps);
          if (index != -1)
          {
            ColumnViewInfo columnViewInfo = new ColumnViewInfo()
            {
              attId = this._rowsAttProps[index].AttributeID
            };
            XmlAttribute attribute2 = childNode.Attributes["Index"];
            int result1;
            if (attribute2 != null && int.TryParse(attribute2.Value, out result1))
              columnViewInfo.order = result1;
            XmlAttribute attribute3 = childNode.Attributes["Width"];
            int result2;
            if (attribute3 != null && int.TryParse(attribute3.Value, out result2))
              columnViewInfo.width = result2;
            XmlAttribute attribute4 = childNode.Attributes["Visible"];
            bool result3;
            if (attribute4 != null && bool.TryParse(attribute4.Value, out result3))
              columnViewInfo.visible = result3;
            this._viewInfo.Add(columnViewInfo);
          }
        }
      }
      this._viewInfo.Sort((Comparison<ColumnViewInfo>) ((x, y) => x.order - y.order));
      for (int index = 0; index < this._viewInfo.Count; ++index)
        this._viewInfo[index].order = index + 1;
    }
    if (this._viewInfo != null && this._viewInfo.Count != 0)
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
    DataGridViewColumn column = this._grid.Columns[attributeTypeId.ToString()];
    if (column == null)
      return;
    int index1 = TableEditor.IndexOfAttProp(attributeTypeId, this._rowsAttProps);
    if (index1 != -1)
    {
      AttributeTypeProperties rowsAttProp = this._rowsAttProps[index1];
      column.HeaderText = rowsAttProp.Name;
    }
    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
    int width = column.Width;
    this._viewInfo = new List<ColumnViewInfo>()
    {
      new ColumnViewInfo()
      {
        attId = attributeTypeId,
        order = 1,
        visible = true,
        width = column.Width
      }
    };
    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
    column.Width = width;
  }

  private void SetSortedColumn(Guid columnGuid, string mode)
  {
    if (!(columnGuid != Guid.Empty) || this._grid.Columns.Count <= 0)
      return;
    if (columnGuid.Equals(TableView.MULTI_GUID_SORTING))
    {
      try
      {
        this._grid.SetSortString(mode);
      }
      catch
      {
      }
    }
    else
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(columnGuid);
      if (attributeType == null)
        return;
      string str = attributeType.AttributeID.ToString();
      foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
      {
        if (!(column.Name != str))
        {
          this._grid.Sort(column, mode == "DESC" ? ListSortDirection.Descending : ListSortDirection.Ascending);
          break;
        }
      }
    }
  }

  private void SetCurrentNode()
  {
    if (this._currDisplayType == DisplayMode.GeneralMode)
      this._currNode = this._gSettings;
    else if (this._currDisplayType == DisplayMode.PersonalMode)
    {
      this._currNode = this._uSettings;
    }
    else
    {
      XmlNode newChild = this._rSettings.SelectSingleNode($"//Role[@Guid='{this._cmbRole.ComboBox?.SelectedValue}']");
      if (newChild == null)
      {
        XmlElement element = this._rSettings.OwnerDocument?.CreateElement("Role");
        element?.SetAttribute("Guid", this._cmbRole.ComboBox?.SelectedValue.ToString());
        newChild = (XmlNode) element;
        if (newChild != null)
          this._rSettings.AppendChild(newChild);
      }
      this._currNode = newChild;
    }
  }

  private bool CalcImageData(IUserSession iUserSession)
  {
    bool flag1 = false;
    bool flag2 = false;
    this.ClearImageData();
    IPicturesCache picturesCache = TableView.PicturesCache;
    if (picturesCache != null)
      this._picture = picturesCache.GetPicture(this._objectType, this.ObjectId, out long _);
    if (this._picture is DBNull)
      this._picture = (object) null;
    Control imagesPanel = (Control) this._imagesPanel;
    try
    {
      this._noteImageContainer.Panel1.SuspendLayout();
      imagesPanel.SuspendLayout();
      if (this._picture != null)
      {
        this._layoutNeed = true;
        QuickObjectInfo objectInfo = iUserSession.GetObjectInfo(this._objectId);
        Intermech.Client.Core.Thumbnail.ThumbnailItem thItem = new Intermech.Client.Core.Thumbnail.ThumbnailItem((INodeID) null, objectInfo.Caption, Math.Abs(this._objectId), objectInfo.ObjectTypeID);
        this._items.Add(thItem);
        DoubleBufferedPanel doubleBufferedPanel = new DoubleBufferedPanel();
        doubleBufferedPanel.Tag = (object) thItem;
        doubleBufferedPanel.ContextMenuStrip = this.GetContextMenuStrip(thItem);
        Panel panel = (Panel) doubleBufferedPanel;
        imagesPanel.Controls.Add((Control) panel);
        panel.Paint += new PaintEventHandler(this.ImagePanelPaint);
      }
      if (this._imageColumns.Count > 0)
      {
        this._layoutNeed = true;
        foreach (DataGridViewColumn imageColumn in this._imageColumns)
        {
          Intermech.Client.Core.Thumbnail.ThumbnailItem thItem = new Intermech.Client.Core.Thumbnail.ThumbnailItem((INodeID) null, imageColumn.HeaderText, -1L, -1)
          {
            Tag = (object) imageColumn
          };
          this._items.Add(thItem);
          DoubleBufferedPanel doubleBufferedPanel = new DoubleBufferedPanel();
          doubleBufferedPanel.Tag = (object) thItem;
          doubleBufferedPanel.ContextMenuStrip = this.GetContextMenuStrip(thItem);
          Panel panel = (Panel) doubleBufferedPanel;
          imagesPanel.Controls.Add((Control) panel);
          panel.Paint += new PaintEventHandler(this.ImagePanelPaint);
        }
      }
      flag1 = this._items.Count > 0;
      if (this._noteColumns.Count > 0 || this._rtf != null)
        flag2 = true;
      if (this._rtf != null)
        flag2 = true;
      this._noteImageContainer.Panel1Collapsed = !flag2;
      this._noteImageContainer.Panel2Collapsed = !flag1;
      this._splitContainer.Panel1Collapsed = !flag1 && !flag2;
    }
    finally
    {
      this._noteImageContainer.Panel1.ResumeLayout();
      imagesPanel.ResumeLayout();
    }
    return flag1 | flag2;
  }

  private ContextMenuStrip GetContextMenuStrip(Intermech.Client.Core.Thumbnail.ThumbnailItem thItem)
  {
    ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
    Image image = (Image) null;
    if (this._nil != null)
    {
      int index = this._nil.ImageIndex("imgView");
      if (index != -1)
        image = this._nil.ImageList.Images[index];
    }
    contextMenuStrip.Items.Add(LocalizationHolder.rm.GetString("Imbase_View"), image).Click += (EventHandler) ((sender, e) => this.ShowImageEvent(sender, e, thItem));
    return contextMenuStrip;
  }

  private void ShowImageEvent(object sender, EventArgs e, Intermech.Client.Core.Thumbnail.ThumbnailItem thItem)
  {
    if (thItem.Image == null)
      return;
    if (thItem.TypeId == Intermech.Imbase.Consts.PDFBookTypeID)
      FullPDFView.ShowPdf(thItem.ObjectId);
    else
      FullImageView.ShowImage(thItem.Image);
  }

  private void ImagePanelPaint(object sender, PaintEventArgs e)
  {
    if (!(sender is Panel panel) || !(panel.Tag is Intermech.Client.Core.Thumbnail.ThumbnailItem tag))
      return;
    this._renderer.DrawPanel(this._items.IndexOf(tag), e.Graphics, panel.ClientRectangle, false, true);
  }

  private void _splitContainer_Panel1_Paint(object sender, PaintEventArgs e)
  {
    Control panel1 = (Control) this._splitContainer.Panel1;
    if (this._layoutNeed)
      this.OnImagesPanel_Resize((object) panel1, EventArgs.Empty);
    if (panel1.Controls.Count != 0)
      return;
    e.Graphics.DrawString(LocalizationHolder.rm.GetString("NoData"), panel1.Font, SystemBrushes.ControlText, (RectangleF) panel1.DisplayRectangle, this._sf);
  }

  private void ClearImageData()
  {
    this._picture = (object) null;
    List<Control> controlList = new List<Control>();
    foreach (Control control in (ArrangedElementCollection) this._imagesPanel.Controls)
      controlList.Add(control);
    this._imagesPanel.Controls.Clear();
    foreach (Component component in controlList)
      component.Dispose();
    this._items?.Clear();
  }

  private void SetUserRecordsFilter()
  {
    this._userFilter = string.Empty;
    if (this.miApplyUserFilter.Checked)
      this._userFilter = $"{"F_USERFILTER"}={(System.ValueType) true}";
    this._userFilterCheckColumn.Visible = true;
    this.ApplyFilter();
    this._userFilterCheckColumn.Visible = this.miEditUresFilter.Checked;
    this.tbUserFilter.Image = this.miApplyUserFilter.Checked ? this._imgEnabledUserFilter : this._imgDisabledUserFilter;
  }

  private void SaveUserFilter()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!this._userParams.SaveUserFilterState)
        return;
      ICustomUsersTableFilterService service = ServiceUtils.GetService<ICustomUsersTableFilterService>((object) sessionKeeper.Session, true);
      List<Guid> list = this._dataTable.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToBoolean(row["F_USERFILTER"]))).Select<DataRow, Guid>((System.Func<DataRow, Guid>) (row => new Guid(Convert.ToString(row["-12"])))).ToList<Guid>();
      this._userSetting.Enabled = this.miApplyUserFilter.Checked;
      this._userSetting.RecordGuids = list;
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      Guid objectGuid = this._objectGuid;
      UserFilter userSetting = this._userSetting;
      service.SetUserFilter(sessionGuid, objectGuid, userSetting);
    }
  }

  private void SetDisabledRecordFilter()
  {
    this._usingFilter = string.Empty;
    if (this.mnUsedRecords.Checked)
    {
      this._usingFilter = $"{"F_APPLICABILITY"}={(System.ValueType) true}";
      this.tbFilter.Image = this._imgFilterFull;
    }
    else
      this.tbFilter.Image = this._imgFilterEmpty;
    this.ApplyFilter();
  }

  private void tbLaunchNormaCS_Click(object sender, EventArgs e)
  {
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.Start();
  }

  private void tbFindByNumberNCS_Click(object sender, EventArgs e)
  {
    if (this._grid.CurrentCell == null || this._grid.CurrentCell.RowIndex == -1 || this._grid.CurrentCell.ColumnIndex == -1)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByNumber(Convert.ToString(this._grid.CurrentCell.Value));
  }

  private void tbFindByNameNCS_Click(object sender, EventArgs e)
  {
    if (this._grid.CurrentCell == null || this._grid.CurrentCell.RowIndex == -1 || this._grid.CurrentCell.ColumnIndex == -1)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByName(Convert.ToString(this._grid.CurrentCell.Value));
  }

  private void TbFindByTextNCS_Click(object sender, EventArgs e)
  {
    if (this._grid.CurrentCell == null || this._grid.CurrentCell.RowIndex == -1 || this._grid.CurrentCell.ColumnIndex == -1)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByText(Convert.ToString(this._grid.CurrentCell.Value));
  }

  private void MnFindByNumberNCS_Click(object sender, EventArgs e)
  {
    if (this._popupHitTest.Type != DataGridViewHitTestType.Cell || this._popupHitTest.RowIndex == -1 || this._popupHitTest.ColumnIndex == -1)
      return;
    DataGridViewCell cell = this._grid.Rows[this._popupHitTest.RowIndex].Cells[this._popupHitTest.ColumnIndex];
    if (cell == null || cell.Value == null)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByNumber(Convert.ToString(cell.Value));
  }

  private void mnFindByNameNCS_Click(object sender, EventArgs e)
  {
    if (this._popupHitTest.Type != DataGridViewHitTestType.Cell || this._popupHitTest.RowIndex == -1 || this._popupHitTest.ColumnIndex == -1)
      return;
    DataGridViewCell cell = this._grid.Rows[this._popupHitTest.RowIndex].Cells[this._popupHitTest.ColumnIndex];
    if (cell == null || cell.Value == null)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByName(Convert.ToString(cell.Value));
  }

  private void mnFindByTextNCS_Click(object sender, EventArgs e)
  {
    if (this._popupHitTest.Type != DataGridViewHitTestType.Cell || this._popupHitTest.RowIndex == -1 || this._popupHitTest.ColumnIndex == -1)
      return;
    DataGridViewCell cell = this._grid.Rows[this._popupHitTest.RowIndex].Cells[this._popupHitTest.ColumnIndex];
    if (cell == null || cell.Value == null)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByText(Convert.ToString(cell.Value));
  }

  private void SubscribeEvents()
  {
    this._notificationService = ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true);
    this._notificationService?.Subscribe("ProjectChanged", new NotificationEventHandler(this.ProjectChanged));
  }

  private void UnsubscribeEvents()
  {
    this._notificationService?.Unsubscribe("ProjectChanged", new NotificationEventHandler(this.ProjectChanged));
  }

  private void ProjectChanged(object sender, NotificationEventArgs e) => this.ApplyVisualSettings();

  private void RegisterGlobalDelegates()
  {
    SelectedRecords.ContextChanged += new SelectedRecords.ContextChangedEventHandler(this.SelectedRecords_ContextChanged);
    Intermech.Imbase.Views.CheckedRecords.ActiveChanged += new Intermech.Imbase.Views.CheckedRecords.ContextChangedEventHandler(this.CheckedRecords_ActiveChanged);
    Intermech.Imbase.Views.CheckedRecords.ContextChanged += new Intermech.Imbase.Views.CheckedRecords.ContextChangedEventHandler(this.CheckedRecords_ContextChanged);
  }

  private void UnregiterGlobalDeletegates()
  {
    SelectedRecords.ContextChanged -= new SelectedRecords.ContextChangedEventHandler(this.SelectedRecords_ContextChanged);
    Intermech.Imbase.Views.CheckedRecords.ActiveChanged -= new Intermech.Imbase.Views.CheckedRecords.ContextChangedEventHandler(this.CheckedRecords_ActiveChanged);
    Intermech.Imbase.Views.CheckedRecords.ContextChanged -= new Intermech.Imbase.Views.CheckedRecords.ContextChangedEventHandler(this.CheckedRecords_ContextChanged);
  }

  private void OnImagesPanel_Resize(object sender, EventArgs e)
  {
    SplitterPanel imagesPanel = this._imagesPanel;
    int count = imagesPanel.Controls.Count;
    Rectangle clientRectangle = imagesPanel.ClientRectangle;
    int num1 = clientRectangle.Height - 4;
    if (!imagesPanel.HorizontalScroll.Visible)
      num1 -= SystemInformation.HorizontalScrollBarHeight;
    int num2 = (int) ((double) num1 * 1.33);
    int num3 = count * num2 + 2 * (count - 1);
    int num4 = clientRectangle.Width - num3;
    Point point = new Point(!this._noteImageContainer.Panel1Collapsed ? 2 : (num4 <= 0 ? 2 : num4 / 2), 2);
    for (int index = 0; index < count; ++index)
    {
      if (imagesPanel.Controls[index] is Panel control)
      {
        control.Height = num1;
        control.Width = num2;
        control.Location = point;
      }
      point.X += num2 + 2;
    }
    this._layoutNeed = false;
  }

  private string RenameFields(string filter, bool aplyBraces)
  {
    StringBuilder sb = new StringBuilder(filter.Length);
    List<char> nameChars = new List<char>();
    int length = filter.Length;
    int num = 0;
    bool flag = false;
    while (num < length)
    {
      char c = filter[num++];
      if (flag)
      {
        if (char.IsLetterOrDigit(c) || c == '_')
        {
          nameChars.Add(c);
        }
        else
        {
          this.AppendName(sb, nameChars, aplyBraces);
          if (c != ']')
            sb.Append(c);
          flag = false;
        }
      }
      else if (c == '$')
        flag = true;
      else
        sb.Append(c);
    }
    if (nameChars.Count > 0)
      this.AppendName(sb, nameChars, aplyBraces);
    return sb.ToString().Replace("F_KEY", "[-2]").Replace("F_GUID", "[-12]");
  }

  private void AppendName(StringBuilder sb, List<char> nameChars, bool applyBraces)
  {
    string columnName = this.NameToColumnName(new string(nameChars.ToArray()));
    if (applyBraces)
      sb.Append('[');
    sb.Append(columnName);
    if (applyBraces)
      sb.Append(']');
    nameChars.Clear();
  }

  private string NameToColumnName(string name)
  {
    if (name.Equals("F_KEY"))
      return "-2";
    int length = this._rowsAttProps.Length;
    for (int index = 0; index < length; ++index)
    {
      AttributeTypeProperties rowsAttProp = this._rowsAttProps[index];
      if (name.Equals(rowsAttProp.ShortName))
        return rowsAttProp.AttributeID.ToString();
    }
    for (int index = 0; index < length; ++index)
    {
      AttributeTypeProperties rowsAttProp = this._rowsAttProps[index];
      if (string.Compare(name, rowsAttProp.ShortName, true) == 0)
        return rowsAttProp.AttributeID.ToString();
    }
    return "?" + name;
  }

  private void Attach(long objectId)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      this._isAdminRole = session.IsAdmin;
      this._userParams = ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).GetUserParams(sessionKeeper.Session.SessionGUID);
      this.splitContainer1.SuspendDrawing();
      this.Detach();
      this.DetachExtender();
      try
      {
        this.Clear();
        this._lockFormatting = true;
        if (objectId != -1L)
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(objectId);
          this._objectType = objectInfo.ObjectTypeID;
          if (this._objectType != Intermech.Imbase.Consts.ImbaseTableTypeID && this._objectType != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
            throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_29"), nameof (objectId));
          this._tableName = objectInfo.Caption;
          this._objectId = objectId;
          this._objectGuid = objectInfo.VersionGuid;
          this._userSetting = (UserFilter) null;
          if (session.GetCustomService(typeof (ICustomUsersTableFilterService)) is ICustomUsersTableFilterService customService)
            this._userSetting = customService.GetUserFilter(session.SessionGUID, this._objectGuid);
          if (this._userSetting == null)
            this._userSetting = new UserFilter();
          this.LoadDataTable(objectId, session);
          this._dataTable.BeginLoadData();
          this.GetTableAttributes(session);
          Guid sortGuid;
          string sortMode;
          this.LoadDisplaySettings(session, objectInfo.VersionGuid, out sortGuid, out sortMode);
          this.MapColumns(sessionKeeper.Session);
          this.CheckReferencesColumns(session);
          this.CalcRecordRefColumnsData();
          this.CalcObjectRefColumnsData();
          this.AttachExtender();
          this.LoadExtenderFilters();
          this.LoadSelectedRows();
          this.LoadCheckedRows(true);
          this.ActivateUnusedRecordsMode();
          flag = this.CalcImageData(sessionKeeper.Session);
          this.LoadRowColors(session);
          bool enabled = this._userSetting.Enabled;
          this.miApplyUserFilter.Checked = false;
          this._userSetting.Enabled = enabled;
          this.miApplyUserFilter.Checked = this._userParams.SaveUserFilterState && this._userSetting.Enabled;
          string rowGuid = this._grid.CurrentRow != null ? Convert.ToString(this._grid.CurrentRow.Cells["-12"].Value) : string.Empty;
          this.ApplySort(sortGuid, sortMode);
          this.LocateRowByGuid(rowGuid);
        }
        else
        {
          this._grid.AutoGenerateColumns = true;
          this._grid.DataSource = (object) null;
          this._objectId = objectId;
        }
      }
      finally
      {
        this._dataTable?.EndLoadData();
        this.LoadFilters();
        this.splitContainer1.ResumeDrawing();
        this._lockFormatting = false;
        this.tbSaveDisplaySettings.Enabled = false;
        if (flag)
        {
          Application.DoEvents();
          this.SplitContainer_SplitterMoved((object) null, (SplitterEventArgs) null);
          this.Grid_SelectionChanged((object) this._grid, EventArgs.Empty);
        }
        Application.DoEvents();
      }
      this.SubscribeComboBoxes();
      this._grid.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(this.On_btnSaveDisplaySettings_Enabled);
      this._grid.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.On_btnSaveDisplaySettings_Enabled);
      this._isSubscribeToViewChanged = true;
      this.CheckedRecords_ActiveChanged();
      this.lbRecords.Text = $"записей: {this._grid.Rows.Count}";
      this._grid.Invalidate();
      this.PerformLayout();
      Application.DoEvents();
      if (!TableView._filterPanelWidth.ContainsKey(this._objectId))
        return;
      if (this.splitContainer1.Width != 0)
      {
        if (this.splitContainer1.SplitterDistance == TableView._filterPanelWidth[this._objectId])
          return;
        this.splitContainer1.SplitterDistance = TableView._filterPanelWidth[this._objectId];
      }
      else
      {
        this._needDistance = TableView._filterPanelWidth[this._objectId];
        this.splitContainer1.SizeChanged += new EventHandler(this.SplitContainer1_SizeChangedOnce);
      }
    }
  }

  private void SplitContainer1_SizeChangedOnce(object sender, EventArgs e)
  {
    this.splitContainer1.SizeChanged -= new EventHandler(this.SplitContainer1_SizeChangedOnce);
    if (this._needDistance == 0)
      return;
    this.splitContainer1.SplitterDistance = this._needDistance;
    this._needDistance = 0;
  }

  private void LoadFilters()
  {
    if (!this.mnDataFilter.Checked)
      return;
    List<ConditionItem> conditions = SelectedRecords.Conditions;
    if (conditions == null || conditions.Count <= 0)
      return;
    foreach (ConditionItem conditionItem in conditions)
    {
      string name = conditionItem.AttId.ToString();
      if (this._dataTable.Columns.Contains(name))
      {
        DataColumn column = this._dataTable.Columns[name];
        foreach (ComboBoxFiller comboBoxFiller in this._comboBoxFillers)
        {
          if (comboBoxFiller._column == column)
          {
            comboBoxFiller.ReadData();
            break;
          }
        }
      }
    }
    this._ignoreSaveFilter = true;
    this._extender.SetFilters(conditions.ToArray());
    this._ignoreSaveFilter = false;
  }

  private List<long> GetObjectIds(long recordId)
  {
    List<long> objectIds = new List<long>();
    if (this._usedKeys.BinarySearch(recordId) >= 0)
    {
      DataRow[] dataRowArray = this._createdObjects.Select($"[{Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID}]={recordId}");
      if (dataRowArray.Length != 0)
      {
        foreach (DataRow dataRow in dataRowArray)
          objectIds.Add(Convert.ToInt64(dataRow[0]));
      }
    }
    return objectIds;
  }

  private void ApplySort(Guid sortGuid, string sortMode)
  {
    if (!(sortGuid != Guid.Empty))
      return;
    this.SetSortedColumn(sortGuid, sortMode);
  }

  private void LoadRowColors(IUserSession session)
  {
    if (this._colorizedRows != null)
    {
      this._colorizedRows.Clear();
      this._colorizedRows = (Dictionary<long, Color>) null;
    }
    this._colorizedRows = TableColorizer.Instance.GetColorsForRows(session, this._rowsAttProps, this._dataTable);
  }

  private void CalcRecordRefColumnsData()
  {
    if (this._recordRefColumns.Count == 0)
      return;
    this._recordRefMap.Clear();
    List<string> state = new List<string>(this._dataTable.Rows.Count * this._recordRefColumns.Count);
    foreach (DataGridViewColumn recordRefColumn in this._recordRefColumns)
    {
      int columnIndex = this._dataTable.Columns.IndexOf(recordRefColumn.DataPropertyName);
      if (columnIndex != -1)
      {
        foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
        {
          string str = Convert.ToString(row[columnIndex]);
          if (str.Length > 2 && str[0] == 'I' && str[1] == 'K')
          {
            int num = state.BinarySearch(str);
            if (num < 0)
              state.Insert(~num, str);
          }
        }
      }
    }
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.MainRecordsThreadProc), (object) state);
  }

  private void CalcObjectRefColumnsData()
  {
    if (this._objectRefColumns.Count == 0)
      return;
    this._objectRefMap.Clear();
    List<string> state = new List<string>(this._dataTable.Rows.Count * this._objectRefColumns.Count);
    foreach (KeyValuePair<string, DataGridViewColumn> objectRefColumn in this._objectRefColumns)
    {
      int columnIndex = this._dataTable.Columns.IndexOf(objectRefColumn.Value.DataPropertyName);
      if (columnIndex != -1)
      {
        foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
        {
          if (row[columnIndex] is ValuesArray valuesArray)
          {
            foreach (object obj in valuesArray.GetArray())
            {
              string str = obj?.ToString();
              if (!string.IsNullOrWhiteSpace(str) && !state.Contains(str))
                state.Add(str);
            }
          }
          else
          {
            string str = Convert.ToString(row[columnIndex]);
            if (str.Length > 0 && !state.Contains(str))
              state.Add(str);
          }
        }
      }
    }
    state.Sort();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.MainObjectsThreadProc), (object) state);
  }

  private void CheckReferencesColumns(IUserSession session)
  {
    this._recordRefColumns.Clear();
    this._objectRefColumns.Clear();
    this._imageColumns.Clear();
    this._noteColumns.Clear();
    foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
    {
      int attId;
      if (TableView.GetAttributeIdFromColumn(column, out attId) && attId > 0)
      {
        int index1 = TableEditor.IndexOfAttProp(attId, this._rowsAttProps);
        if (index1 != -1)
        {
          IDBAttributeType attributeType = session.GetAttributeType(attId, false);
          if ((this._rowsAttProps[index1].Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef)
            this._recordRefColumns.Add(column);
          else if (attributeType != null && (attributeType.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef)
            this._recordRefColumns.Add(column);
          if (attributeType != null && attributeType.AttributeType == FieldTypes.ftObjectLink)
          {
            this._objectRefColumns.Add(column.Name, column);
            if (attributeType.AttributeID == Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID)
              this._imageColumns.Add(column);
            else if (attributeType is IDBObjectLinkAttributeType linkAttributeType)
            {
              int[] validObjectTypes = linkAttributeType.GetValidObjectTypes();
              int length = validObjectTypes.Length;
              for (int index2 = 0; index2 < length; ++index2)
              {
                int ObjectTypeID = validObjectTypes[index2];
                if (ObjectTypeID == Intermech.Client.Core.Thumbnail.Consts.ImageLibraryItemTypeID || ObjectTypeID == Intermech.Imbase.Consts.PDFBookTypeID)
                {
                  this._imageColumns.Add(column);
                  break;
                }
                if (ObjectTypeID == Intermech.Imbase.Consts.ImbaseBLOBTypeID)
                  this._noteColumns.Add(column);
                List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(ObjectTypeID);
                if (attribute4ObjectTypeList != null)
                {
                  foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
                  {
                    if (attribute4ObjectType.ObjectTypeID == Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID || attribute4ObjectType.ObjectTypeID == Intermech.Imbase.Consts.PDFBookTypeID)
                    {
                      this._imageColumns.Add(column);
                      break;
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  private void ApplyFilters(IUserSession session, IDBObject linkObject)
  {
    int index1 = this.IndexOfAttValues(Intermech.Imbase.Consts.ImbaseTemplateAttID);
    IDBAttribute attributeById = linkObject.GetAttributeByID(Intermech.Imbase.Consts.ManualTableFilterId);
    if (index1 == -1 && attributeById == null)
      return;
    int columnIndex1 = this._dataTable.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseTemplateAttID.ToString());
    if (columnIndex1 == -1 && attributeById == null)
      return;
    List<long> longList = (List<long>) null;
    string empty = string.Empty;
    int columnIndex2 = -1;
    if (index1 != -1)
      empty = Convert.ToString(this._tableAttributes[index1].Values[0]);
    if (attributeById is IBlobReader blobReader)
    {
      BlobInformation blobInformation = blobReader.OpenBlob(0);
      try
      {
        try
        {
          if (blobInformation.RealFileSize > 0L)
          {
            columnIndex2 = this._dataTable.Columns.IndexOf("-2");
            byte[] buffer = blobReader.ReadDataBlock(0);
            if (buffer != null)
            {
              using (MemoryStream inStream = new MemoryStream(buffer))
              {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                  ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
                  memoryStream.Position = 0L;
                  using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream))
                  {
                    int capacity = (int) memoryStream.Length / 8;
                    longList = new List<long>(capacity);
                    for (int index2 = 0; index2 < capacity; ++index2)
                      longList.Add(binaryReader.ReadInt64());
                  }
                }
              }
            }
          }
        }
        finally
        {
          blobReader.CloseBlob();
        }
      }
      catch
      {
      }
    }
    DataRowCollection rows = this._dataTable.Rows;
    int count1 = rows.Count;
    List<DataRow> dataRowList = new List<DataRow>(32 /*0x20*/);
    for (int index3 = 0; index3 < count1; ++index3)
    {
      DataRow dataRow = rows[index3];
      if (longList != null && !longList.Contains(Convert.ToInt64(dataRow[columnIndex2])))
        dataRowList.Add(dataRow);
      else if (index1 != -1 && columnIndex1 != -1 && !this.AcceptMask(empty, dataRow[columnIndex1].ToString()))
        dataRowList.Add(dataRow);
    }
    int count2 = dataRowList.Count;
    for (int index4 = 0; index4 < count2; ++index4)
      dataRowList[index4].Delete();
    this._dataTable.AcceptChanges();
  }

  private bool AcceptMask(string maskData, string value)
  {
    if (string.IsNullOrEmpty(maskData))
      return true;
    if (string.IsNullOrEmpty(value))
      return false;
    char[] charArray = maskData.ToCharArray();
    int length = charArray.Length;
    for (int index = 0; index < length; ++index)
    {
      if (value.IndexOf(charArray[index]) == -1)
        return false;
    }
    return true;
  }

  private int FirstVisibleColumnIndex()
  {
    return this._grid.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index;
  }

  private bool GetColumnGuid(int attId, AttributeTypeProperties[] atps, ref Guid colGuid)
  {
    int length = atps.Length;
    for (int index = 0; index < length; ++index)
    {
      if (atps[index].AttributeID == attId)
      {
        colGuid = atps[index].AttributeGuid;
        return true;
      }
    }
    return false;
  }

  private void GetTableAttributes(IUserSession session)
  {
    AttributeValues[] attributesValues1 = session.GetObject(this.TableId).GetAttributesValues(TableEditor.ImbaseAttValuesModes);
    this._tableAttributes.Clear();
    if (attributesValues1 != null)
      this._tableAttributes.AddRange((IEnumerable<AttributeValues>) attributesValues1);
    if (this.LinkId == -1L)
      return;
    IDBObject linkObject = session.GetObject(this.LinkId);
    AttributeValues[] attributesValues2 = linkObject.GetAttributesValues(TableEditor.ImbaseAttValuesModes);
    int length = attributesValues2.Length;
    for (int index1 = 0; index1 < length; ++index1)
    {
      AttributeValues attributeValues = attributesValues2[index1];
      int index2 = this.IndexOfAttValues(attributeValues.AttributeID);
      if (index2 == -1)
        this._tableAttributes.Add(attributeValues);
      else if (attributeValues.AttributeID > 0 && !DBNull.Value.Equals(attributeValues.Values[0]))
        this._tableAttributes[index2] = attributeValues;
    }
    this._rtf = (string) null;
    AttributeValues attributeValues1 = this._tableAttributes.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == Intermech.Imbase.Consts.ImbaseNoteAttID));
    if (attributeValues1 != null)
    {
      this._rtf = Convert.ToString(attributeValues1.Values[0]).Trim();
      if (string.IsNullOrEmpty(this._rtf))
        this._rtf = (string) null;
    }
    this.ApplyFilters(session, linkObject);
  }

  private int IndexOfAttValues(int attId)
  {
    int count = this._tableAttributes.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this._tableAttributes[index].AttributeID == attId)
        return index;
    }
    return -1;
  }

  private void LoadDataTable(long objectId, IUserSession session)
  {
    IImbaseServer server = CadmechHelper.GetServer(session);
    server.LoadRecords(session.SessionGUID, objectId, string.Empty, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out this._dataTable, out this._rowsAttProps, out ImbaseKeyInfo _);
    this.TableId = Convert.ToInt64(this._dataTable.ExtendedProperties[(object) -2]);
    IDBObject objectActualCopy = session.GetObjectActualCopy(this.TableId, false);
    if (objectActualCopy != null)
      this.TableId = objectActualCopy.ObjectID;
    this.LinkId = Convert.ToInt64(this._dataTable.ExtendedProperties[(object) Intermech.Imbase.Consts.ImbaseLinkRefAttID]);
    this._dataView = this._dataTable.DefaultView;
    this.GetCreatedObjects(session, server);
    this._grid.Columns.Clear();
    this._grid.DataSource = (object) null;
    this._grid.AutoGenerateColumns = true;
    this._grid.DataSource = (object) this._dataView;
  }

  public void GetCreatedObjects(IUserSession session, IImbaseServer ims)
  {
    this._createdObjects = ims.GetCreatedObjects(session.SessionGUID, this.LinkId);
    this._usedKeys.Clear();
    if (this._createdObjects != null)
    {
      if (this.LinkId != -1L)
      {
        this._createdObjects.DefaultView.RowFilter = $"[{Intermech.Imbase.Consts.ImbaseObjectRefAttID}]={this.LinkId}";
        this._createdObjects = this._createdObjects.DefaultView.ToTable();
      }
      int columnIndex1 = this._createdObjects.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID.ToString());
      int columnIndex2 = this._createdObjects.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseObjectRefAttID.ToString());
      if (columnIndex1 != -1)
      {
        DataRowCollection rows = this._createdObjects.Rows;
        int count = rows.Count;
        for (int index = 0; index < count; ++index)
        {
          if (columnIndex2 != -1)
          {
            object obj = rows[index][columnIndex2];
            if (DBNull.Value.Equals(obj) || obj == null || Convert.ToInt64(obj) != this.LinkId)
              continue;
          }
          object obj1 = rows[index][columnIndex1];
          if (!DBNull.Value.Equals(obj1) && obj1 != null)
          {
            long int64 = Convert.ToInt64(obj1);
            if (!this._usedKeys.Contains(int64))
              this._usedKeys.Add(int64);
          }
        }
        this._usedKeys.Sort();
      }
      this._grid.RowHeadersVisible = this._usedKeys.Count > 0;
    }
    else
      this._grid.RowHeadersVisible = false;
  }

  protected void MapColumns(IUserSession session)
  {
    this._lockDisplayIndexChanged = true;
    bool hideEmptyColumns = ServiceUtils.GetService<IImbaseParamsService>((object) session, true).GetUserParams(session.SessionGUID).HideEmptyColumns;
    this._grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    try
    {
      this._grid.AutoGenerateColumns = false;
      if (this._grid.Columns.Cast<DataGridViewColumn>().All<DataGridViewColumn>((System.Func<DataGridViewColumn, bool>) (x => x.Name != "F_CHECKCOLUMN")))
      {
        DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
        viewCheckBoxColumn.Name = "F_CHECKCOLUMN";
        viewCheckBoxColumn.ReadOnly = false;
        viewCheckBoxColumn.Width = 30;
        viewCheckBoxColumn.HeaderText = string.Empty;
        viewCheckBoxColumn.Visible = false;
        this._checkColumn = viewCheckBoxColumn;
        this._grid.Columns.Add((DataGridViewColumn) this._checkColumn);
      }
      this._checkColumn.Frozen = false;
      DataGridViewColumn[] dataGridViewColumnArray = new DataGridViewColumn[this._grid.Columns.Count];
      dataGridViewColumnArray[0] = (DataGridViewColumn) this._checkColumn;
      this.LoadViewInfo();
      DataGridViewColumnCollection columns = this._grid.Columns;
      int num1 = 1;
      this.ColumnsOrder = (int[]) null;
      foreach (DataGridViewColumn column1 in (BaseCollection) columns)
      {
        int attId;
        if (int.TryParse(column1.Name, out attId))
          column1.Tag = (object) attId;
        if (column1 != this._checkColumn)
        {
          column1.ReadOnly = true;
          if (column1.ValueType == typeof (double) && column1.DefaultCellStyle.Format != "#################0.#################")
            column1.DefaultCellStyle.Format = "#################0.#################";
          if (TableView.GetAttributeIdFromColumn(column1, out attId))
          {
            if (attId == -12 || attId == -2 || attId == Intermech.Imbase.Consts.ImbaseTableRefAttID)
            {
              column1.Visible = false;
              dataGridViewColumnArray[columns.Count - num1++] = column1;
            }
            else
            {
              int index = TableEditor.IndexOfAttProp(attId, this._rowsAttProps);
              if (index != -1)
              {
                ColumnViewInfo columnViewInfo = this._viewInfo.FirstOrDefault<ColumnViewInfo>((System.Func<ColumnViewInfo, bool>) (x => x.attId == attId));
                if (columnViewInfo != null)
                {
                  column1.Width = columnViewInfo.width;
                  column1.Visible = columnViewInfo.visible;
                  dataGridViewColumnArray[columnViewInfo.order] = column1;
                }
                else
                {
                  column1.Visible = true;
                  dataGridViewColumnArray[index + 1] = column1;
                }
                AttributeTypeProperties rowsAttProp = this._rowsAttProps[index];
                DataColumn column2 = this._dataTable.Columns[column1.Name];
                if (column2 == null)
                {
                  string columnCaption = TableEditor.GetColumnCaption(rowsAttProp, (string) null);
                  if (column1.HeaderText != columnCaption)
                    column1.HeaderText = columnCaption;
                }
                else
                {
                  if (hideEmptyColumns && column2.ExtendedProperties.ContainsKey((object) "F_VISIBLE"))
                  {
                    bool boolean = Convert.ToBoolean(column2.ExtendedProperties[(object) "F_VISIBLE"]);
                    if (column1.Visible != boolean)
                      column1.Visible = boolean;
                  }
                  string columnCaption = TableEditor.GetColumnCaption(rowsAttProp, column2.ExtendedProperties[(object) "F_MEASURE_U"] as string);
                  if (column1.HeaderText != columnCaption)
                    column1.HeaderText = columnCaption;
                  if (column2.ExtendedProperties.ContainsKey((object) "F_VIRTUAL"))
                    column1.DefaultCellStyle.ForeColor = Color.Blue;
                  if (rowsAttProp.MultiValueMode == MultiValueModes.SingleValueFromList)
                  {
                    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(rowsAttProp.AttributeID);
                    if (attributeType?.PossibleValues != null && !column2.ExtendedProperties.ContainsKey((object) "F_LIST"))
                      column2.ExtendedProperties.Add((object) "F_LIST", (object) attributeType);
                  }
                  if (column2.DataType.IsValueType)
                  {
                    if (column1.DefaultCellStyle.Alignment != DataGridViewContentAlignment.MiddleRight)
                      column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (rowsAttProp.Mask == Intermech.Consts.OnlyDateFunction && !column2.ExtendedProperties.ContainsKey((object) "F_ONLY_DATE"))
                      column2.ExtendedProperties.Add((object) "F_ONLY_DATE", (object) true);
                  }
                }
              }
            }
          }
          else
          {
            if (column1.Name == "F_APPLICABILITY")
              column1.Visible = false;
            if (column1.Name == "F_USERFILTER")
            {
              this._userFilterCheckColumn = column1;
              this._userFilterCheckColumn.ReadOnly = false;
              this._userFilterCheckColumn.HeaderText = string.Empty;
              this._userFilterCheckColumn.Width = 30;
              this._userFilterCheckColumn.Visible = false;
              this._userFilterCheckColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridViewColumnArray[columns.Count - num1++] = column1;
          }
        }
      }
      int num2 = 1;
      int num3 = dataGridViewColumnArray.Length - 1;
      dataGridViewColumnArray[0].DisplayIndex = 0;
      for (int index = 1; index < dataGridViewColumnArray.Length; ++index)
      {
        if (dataGridViewColumnArray[index] != null)
          dataGridViewColumnArray[index].DisplayIndex = dataGridViewColumnArray[index].Visible ? num2++ : num3--;
      }
      this._userFilterCheckColumn.DisplayIndex = 0;
    }
    finally
    {
      this._grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this._lockDisplayIndexChanged = false;
    }
    this._checkColumn.Frozen = true;
    this._userFilterCheckColumn.Frozen = true;
    this.FreezeFirstColumn();
  }

  private void FreezeFirstColumn()
  {
    if (!this._userParams.FreezeFirstColumn)
      return;
    int num = 2;
    foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
    {
      if (column.DisplayIndex == num)
      {
        column.Frozen = true;
        break;
      }
    }
  }

  private void Clear()
  {
    this._objectId = -1L;
    this.TableId = -1L;
    this.LinkId = -1L;
    this._selectedRows?.Clear();
    if (this._dataTable != null)
    {
      this._dataTable.Dispose();
      this._dataTable = (DataTable) null;
    }
    this._rowsAttProps = (AttributeTypeProperties[]) null;
    this._viewInfo = (List<ColumnViewInfo>) null;
    this._grid.ClearSort();
    this._comboBoxFillers.Clear();
    this._rowFilter = string.Empty;
    this.ClearDisplaySettings();
  }

  private void ClearDisplaySettings()
  {
    this.tbSaveDisplaySettings.Enabled = false;
    this._viewInfo = (List<ColumnViewInfo>) null;
    this._gSettings = this._uSettings = this._rSettings = this._currNode = (XmlNode) null;
    this._currDisplayType = DisplayMode.GeneralMode;
    this.UnSubscribeComboBoxes();
  }

  private void OnItemDoubleClick(DataGridViewCellEventArgs e)
  {
    EventHandler itemDoubleClick = this.ItemDoubleClick;
    if (itemDoubleClick == null)
      return;
    itemDoubleClick((object) this, (EventArgs) e);
  }

  private void OnEnterPress()
  {
    EventHandler itemEnterPress = this.ItemEnterPress;
    if (itemEnterPress == null)
      return;
    itemEnterPress((object) this, EventArgs.Empty);
  }

  private void OnCreateObject_Click(object sender, EventArgs e)
  {
    if (this.LinkId < 0L || this.TableId < 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string text = "Перед созданием объекта необходимо завершить редактирование ярлыка и/или таблицы.";
        QuickObjectInfo objectInfo;
        if (this.TableId < 0L)
        {
          objectInfo = sessionKeeper.Session.GetObjectInfo(this.TableId);
          if (!objectInfo.Empty)
            text = $"Перед созданием объекта необходимо завершить редактирование таблицы '{objectInfo.Caption}' .";
        }
        if (this.LinkId < 0L)
        {
          objectInfo = sessionKeeper.Session.GetObjectInfo(this.LinkId);
          if (!objectInfo.Empty)
            text = $"Перед созданием объекта необходимо завершить редактирование ярлыка '{objectInfo.Caption}' .";
        }
        int num = (int) MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
    else
      this.On_CreateObject();
  }

  private object OnGetImage(int imageIndex)
  {
    Intermech.Client.Core.Thumbnail.ThumbnailItem thumbnailItem = this._items[imageIndex];
    object image = thumbnailItem.Image;
    if (image == null)
    {
      if (thumbnailItem.TypeId == Intermech.Imbase.Consts.PDFBookTypeID)
      {
        IPdfDocument orLoadPdfDocument = PDFCache.GetOrLoadPdfDocument(thumbnailItem.ObjectId);
        if (orLoadPdfDocument != null && orLoadPdfDocument.PageCount > 0)
        {
          using (Graphics graphics = this.CreateGraphics())
            return thumbnailItem.Image = (object) orLoadPdfDocument.Render(0, graphics.DpiX, graphics.DpiY, false);
        }
      }
      else if (this._cache != null)
      {
        long newObjectId;
        object picture = this._cache.GetPicture(thumbnailItem.TypeId, thumbnailItem.PictureObjectId, out newObjectId);
        if (thumbnailItem.PictureObjectId != newObjectId)
          thumbnailItem.PictureObjectId = newObjectId;
        thumbnailItem.Image = picture;
        return picture;
      }
    }
    return image;
  }

  private string CalculateFilterString()
  {
    string str1 = this._internalFilter.Length > 0 ? this._internalFilter : string.Empty;
    string str2;
    if (this._usingFilter.Length <= 0)
      str2 = str1;
    else if (str1.Length <= 0)
      str2 = this._usingFilter;
    else
      str2 = $"({str1}) AND ({this._usingFilter})";
    string str3 = str2;
    string str4;
    if (this._userFilter.Length <= 0)
      str4 = str3;
    else if (str3.Length <= 0)
      str4 = this._userFilter;
    else
      str4 = $"({str3}) AND ({this._userFilter})";
    string str5 = str4;
    string str6;
    if (this._filter.Length <= 0)
      str6 = str5;
    else if (str5.Length <= 0)
      str6 = this._filter;
    else
      str6 = $"({str5}) AND ({this._filter})";
    string str7 = str6;
    string filterString;
    if (this._rowFilter.Length <= 0)
      filterString = str7;
    else if (str7.Length <= 0)
      filterString = this._rowFilter;
    else
      filterString = $"({str7}) AND ({this._rowFilter})";
    return filterString;
  }

  private void ApplyFilter()
  {
    if (this._dataView == null || this._dataTable == null)
      return;
    if (this.HasChanges(this._dataTable))
      this._dataTable.AcceptChanges();
    this._dataView.RowFilter = this.CalculateFilterString();
    this.lbRecords.Text = this._grid.Rows.Count == this._dataTable.Rows.Count ? $"записей: {this._grid.Rows.Count}" : $"записей: {this._grid.Rows.Count} из {this._dataTable.Rows.Count}";
  }

  private bool HasChanges(DataTable dataTable)
  {
    DataRowState dataRowState = DataRowState.Added | DataRowState.Deleted | DataRowState.Modified;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if ((row.RowState & dataRowState) != (DataRowState) 0)
        return true;
    }
    return false;
  }

  private void miApplyUserFilter_CheckedChanged(object sender, EventArgs e)
  {
    this.ActivateDisplaySettings = false;
    if (this.miApplyUserFilter.Checked)
    {
      this._checkedRecIds = this.CheckedRecords;
    }
    else
    {
      List<long> exisitingItems = ((IEnumerable<long>) this._checkedRecIds).ToList<long>();
      IEnumerable<long> collection = ((IEnumerable<long>) this.CheckedRecords).ToList<long>().Where<long>((System.Func<long, bool>) (x => !exisitingItems.Contains(x)));
      exisitingItems.AddRange(collection);
      this._checkedRecIds = exisitingItems.ToArray();
    }
    this.SetUserRecordsFilter();
    Intermech.Imbase.Views.CheckedRecords.Add(this._objectId, this._checkedRecIds);
    if (this._userParams.SaveUserFilterState)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (ICustomUsersTableFilterService)) is ICustomUsersTableFilterService customService)
        {
          this._userSetting.Enabled = this.miApplyUserFilter.Checked;
          customService.SetUserFilter(sessionKeeper.Session.SessionGUID, this._objectGuid, this._userSetting);
        }
      }
    }
    this.ActivateDisplaySettings = true;
  }

  private void _grid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
  {
    if (!this._grid.IsCurrentCellInEditMode)
      return;
    if (this._cancelEdit)
      this._grid.CancelEdit();
    else
      this._grid.EndEdit();
    this._cancelEdit = false;
  }

  private void ApplyVisualSettings()
  {
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) is Font font))
      return;
    this._grid.Font = font;
    this._grid.RowTemplate.Height = FontHelper.MeasureStringFast(this._grid.Font, "Ay").Height + 6 + 3;
  }

  private static bool GetAttributeIdFromColumn(DataGridViewColumn column, out int attId)
  {
    attId = 0;
    if (column.Tag == null)
      return false;
    attId = Convert.ToInt32(column.Tag);
    return true;
  }

  private void On_CopyRecordCode_Click(object sender, EventArgs e)
  {
    Clipboard.SetText(ImbaseHelper.MakeInternalImbaseKey(this.LinkId, this.RecordId));
  }

  private void On_CopyRecordKey_Click(object sender, EventArgs e)
  {
    Clipboard.SetText(this.RecordId.ToString());
  }

  private void On_CopyRecordGuid_Click(object sender, EventArgs e)
  {
    Clipboard.SetText(this.RecordGuid.ToString());
  }

  private void OnsplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
  {
    TableView._filterPanelWidth[this._objectId] = this.splitContainer1.SplitterDistance;
  }

  protected override void Dispose(bool disposing)
  {
    this._sf?.Dispose();
    this._sf = (StringFormat) null;
    TableView.SplitterDistance = this._splitContainer.SplitterDistance;
    if (this._renderer != null)
    {
      this._renderer.RedrawRequired -= new RedrawEventHandler(this.Renderer_RedrawRequired);
      this._renderer.Dispose();
    }
    if (this._imgFilterEmpty != null)
      this._imgFilterEmpty.Dispose();
    if (this._imgFilterFull != null)
      this._imgFilterFull.Dispose();
    if (this._imgDisabledUserFilter != null)
      this._imgDisabledUserFilter.Dispose();
    if (this._imgEnabledUserFilter != null)
      this._imgEnabledUserFilter.Dispose();
    this.ClearImageData();
    if (disposing)
    {
      this.UnregiterGlobalDeletegates();
      this.UnsubscribeEvents();
      if (this.components != null)
        this.components.Dispose();
      this._disposed = true;
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableView));
    DefaultGridFilterFactory gridFilterFactory1 = new DefaultGridFilterFactory();
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DefaultGridFilterFactory gridFilterFactory2 = new DefaultGridFilterFactory();
    this._splitContainer = new SplitContainer();
    this._noteImageContainer = new SplitContainer();
    this._richTextBox = new RichTextBox();
    this.splitContainer1 = new SplitContainer();
    this.leftFilterFactory = new LayoutedGridFilterFactoryControl();
    this._grid = new DoubleBufferedDataGridView();
    this._contextMenu = new ContextMenuStrip(this.components);
    this.mnShow = new ToolStripMenuItem();
    this.mnShowtoolStripSeparator = new ToolStripSeparator();
    this.mnCreateObject = new ToolStripMenuItem();
    this.mnObjectProps = new ToolStripMenuItem();
    this.mnOpenInNewWindow = new ToolStripMenuItem();
    this.mnSynch = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripSeparator();
    this.mnNormaCS = new ToolStripMenuItem();
    this.mnLaunchNormaCS = new ToolStripMenuItem();
    this.mnFindByNumberNCS = new ToolStripMenuItem();
    this.mnFindByNameNCS = new ToolStripMenuItem();
    this.mnFindByTextNCS = new ToolStripMenuItem();
    this.mnCopy = new ToolStripMenuItem();
    this.mnCopyRecordCode = new ToolStripMenuItem();
    this.mnCopyRecordKey = new ToolStripMenuItem();
    this.mnCopyRecordGuid = new ToolStripMenuItem();
    this.mnCopyCurrentCell = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._miAutoResize = new ToolStripMenuItem();
    this._miDataSize = new ToolStripMenuItem();
    this._miViewSetting = new ToolStripMenuItem();
    this.imageList1 = new ImageList(this.components);
    this._printDoc = new PrintDocument();
    this._tt = new ToolTip(this.components);
    this.toolStrip1 = new ToolStrip();
    this._tbPrint = new ToolStripButton();
    this.toolStripSeparator5 = new ToolStripSeparator();
    this.tbNormaCS = new ToolStripDropDownButton();
    this.tbLaunchNormaCS = new ToolStripMenuItem();
    this.tbFindByNumberNCS = new ToolStripMenuItem();
    this.tbFindByNameNCS = new ToolStripMenuItem();
    this.tbFindByTextNCS = new ToolStripMenuItem();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.tbFilter = new ToolStripDropDownButton();
    this.mnUsedRecords = new ToolStripMenuItem();
    this.toolStripSeparator4 = new ToolStripSeparator();
    this.mnDataFilter = new ToolStripMenuItem();
    this.mnCleanFilter = new ToolStripMenuItem();
    this.mnFilterOptions = new ToolStripMenuItem();
    this.mnFilterTop = new ToolStripMenuItem();
    this.mnFilterLeft = new ToolStripMenuItem();
    this.tbUserFilter = new ToolStripDropDownButton();
    this.miApplyUserFilter = new ToolStripMenuItem();
    this.toolStripSeparator6 = new ToolStripSeparator();
    this.miEditUresFilter = new ToolStripMenuItem();
    this.miDeleteUserFilter = new ToolStripMenuItem();
    this.toolStripTextBox1 = new ToolStripTextBox();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this.tbSaveDisplaySettings = new ToolStripButton();
    this.toolStripLabel1 = new ToolStripLabel();
    this._cbDisplayMode = new ToolStripComboBox();
    this.toolStripLabel2 = new ToolStripLabel();
    this._cmbRole = new ToolStripComboBox();
    this.lbRecords = new ToolStripLabel();
    this._extender = new DataGridFilterExtender(this.components);
    this._splitContainer.BeginInit();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    this._noteImageContainer.BeginInit();
    this._noteImageContainer.Panel1.SuspendLayout();
    this._noteImageContainer.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    ((ISupportInitialize) this._grid).BeginInit();
    this._contextMenu.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this._extender.BeginInit();
    this.SuspendLayout();
    this._splitContainer.FixedPanel = FixedPanel.Panel1;
    componentResourceManager.ApplyResources((object) this._splitContainer, "_splitContainer");
    this._splitContainer.Name = "_splitContainer";
    componentResourceManager.ApplyResources((object) this._splitContainer.Panel1, "_splitContainer.Panel1");
    this._splitContainer.Panel1.Controls.Add((Control) this._noteImageContainer);
    this._splitContainer.Panel1.Paint += new PaintEventHandler(this._splitContainer_Panel1_Paint);
    this._splitContainer.Panel2.Controls.Add((Control) this.splitContainer1);
    this._splitContainer.SplitterMoved += new SplitterEventHandler(this.SplitContainer_SplitterMoved);
    componentResourceManager.ApplyResources((object) this._noteImageContainer, "_noteImageContainer");
    this._noteImageContainer.Name = "_noteImageContainer";
    this._noteImageContainer.Panel1.Controls.Add((Control) this._richTextBox);
    this._richTextBox.BackColor = Color.WhiteSmoke;
    this._richTextBox.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this._richTextBox, "_richTextBox");
    this._richTextBox.Name = "_richTextBox";
    this._richTextBox.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.FixedPanel = FixedPanel.Panel1;
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.leftFilterFactory);
    this.splitContainer1.Panel1Collapsed = true;
    this.splitContainer1.Panel2.Controls.Add((Control) this._grid);
    this.splitContainer1.SplitterMoved += new SplitterEventHandler(this.OnsplitContainer1_SplitterMoved);
    componentResourceManager.ApplyResources((object) this.leftFilterFactory, "leftFilterFactory");
    gridFilterFactory1.CreateDistinctGridFilters = false;
    gridFilterFactory1.DefaultGridFilterType = typeof (TextGridFilterCombo);
    gridFilterFactory1.DefaultShowDateInBetweenOperator = false;
    gridFilterFactory1.DefaultShowNumericInBetweenOperator = true;
    gridFilterFactory1.HandleEnumerationTypes = true;
    gridFilterFactory1.MaximumDistinctValues = 20;
    this.leftFilterFactory.InnerGridFilterFactory = (IGridFilterFactory) gridFilterFactory1;
    this.leftFilterFactory.Name = "leftFilterFactory";
    this._grid.AllowUserToAddRows = false;
    this._grid.AllowUserToDeleteRows = false;
    this._grid.AllowUserToOrderColumns = true;
    this._grid.AllowUserToResizeRows = false;
    this._grid.BackgroundColor = SystemColors.Control;
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle1.BackColor = SystemColors.Control;
    gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle1.ForeColor = SystemColors.WindowText;
    gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
    this._grid.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
    this._grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._grid.ContextMenuStrip = this._contextMenu;
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.BackColor = SystemColors.Window;
    gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle2.ForeColor = SystemColors.ControlText;
    gridViewCellStyle2.SelectionBackColor = SystemColors.ControlLight;
    gridViewCellStyle2.SelectionForeColor = SystemColors.WindowText;
    gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
    this._grid.DefaultCellStyle = gridViewCellStyle2;
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    this._grid.EditMode = DataGridViewEditMode.EditOnEnter;
    this._grid.MultiSelect = false;
    this._grid.Name = "_grid";
    this._grid.RowFilter = "";
    this._grid.RowHeadersVisible = false;
    this._grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this._grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._grid.ShowEditingIcon = false;
    this._grid.SortChanged = false;
    this._grid.StandardTab = true;
    this._grid.CellContentClick += new DataGridViewCellEventHandler(this.OnGrid_CellContentClick);
    this._grid.CellDoubleClick += new DataGridViewCellEventHandler(this.Grid_CellDoubleClick);
    this._grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
    this._grid.CellMouseUp += new DataGridViewCellMouseEventHandler(this._grid_CellMouseUp);
    this._grid.CellPainting += new DataGridViewCellPaintingEventHandler(this.OnGrid_CellPainting);
    this._grid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(this.Grid_CellToolTipTextNeeded);
    this._grid.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.On_grid_ColumnHeaderMouseClick);
    this._grid.DataError += new DataGridViewDataErrorEventHandler(this.OnGrid_DataError);
    this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
    this._grid.KeyDown += new KeyEventHandler(this.Grid_KeyDown);
    this._contextMenu.Items.AddRange(new ToolStripItem[14]
    {
      (ToolStripItem) this.mnShow,
      (ToolStripItem) this.mnShowtoolStripSeparator,
      (ToolStripItem) this.mnCreateObject,
      (ToolStripItem) this.mnObjectProps,
      (ToolStripItem) this.mnOpenInNewWindow,
      (ToolStripItem) this.mnSynch,
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.mnNormaCS,
      (ToolStripItem) this.mnCopy,
      (ToolStripItem) this.mnCopyCurrentCell,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._miAutoResize,
      (ToolStripItem) this._miDataSize,
      (ToolStripItem) this._miViewSetting
    });
    this._contextMenu.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this._contextMenu, "_contextMenu");
    this._contextMenu.Opening += new CancelEventHandler(this._contextMenu_Opening);
    this.mnShow.Name = "mnShow";
    componentResourceManager.ApplyResources((object) this.mnShow, "mnShow");
    this.mnShow.Click += new EventHandler(this.On_mnShow_Click);
    this.mnShowtoolStripSeparator.Name = "mnShowtoolStripSeparator";
    componentResourceManager.ApplyResources((object) this.mnShowtoolStripSeparator, "mnShowtoolStripSeparator");
    this.mnCreateObject.Name = "mnCreateObject";
    componentResourceManager.ApplyResources((object) this.mnCreateObject, "mnCreateObject");
    this.mnCreateObject.Click += new EventHandler(this.OnCreateObject_Click);
    this.mnObjectProps.Name = "mnObjectProps";
    componentResourceManager.ApplyResources((object) this.mnObjectProps, "mnObjectProps");
    this.mnObjectProps.Click += new EventHandler(this.OnObjectProps_Click);
    this.mnOpenInNewWindow.Name = "mnOpenInNewWindow";
    componentResourceManager.ApplyResources((object) this.mnOpenInNewWindow, "mnOpenInNewWindow");
    this.mnOpenInNewWindow.Click += new EventHandler(this.OnOpenInNewWindow_Click);
    this.mnSynch.Name = "mnSynch";
    componentResourceManager.ApplyResources((object) this.mnSynch, "mnSynch");
    this.mnSynch.Click += new EventHandler(this.OnSync_Click);
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem2, "toolStripMenuItem2");
    this.mnNormaCS.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.mnLaunchNormaCS,
      (ToolStripItem) this.mnFindByNumberNCS,
      (ToolStripItem) this.mnFindByNameNCS,
      (ToolStripItem) this.mnFindByTextNCS
    });
    this.mnNormaCS.Name = "mnNormaCS";
    componentResourceManager.ApplyResources((object) this.mnNormaCS, "mnNormaCS");
    this.mnLaunchNormaCS.Name = "mnLaunchNormaCS";
    componentResourceManager.ApplyResources((object) this.mnLaunchNormaCS, "mnLaunchNormaCS");
    this.mnLaunchNormaCS.Click += new EventHandler(this.tbLaunchNormaCS_Click);
    this.mnFindByNumberNCS.Name = "mnFindByNumberNCS";
    componentResourceManager.ApplyResources((object) this.mnFindByNumberNCS, "mnFindByNumberNCS");
    this.mnFindByNumberNCS.Click += new EventHandler(this.MnFindByNumberNCS_Click);
    this.mnFindByNameNCS.Name = "mnFindByNameNCS";
    componentResourceManager.ApplyResources((object) this.mnFindByNameNCS, "mnFindByNameNCS");
    this.mnFindByNameNCS.Click += new EventHandler(this.mnFindByNameNCS_Click);
    this.mnFindByTextNCS.Name = "mnFindByTextNCS";
    componentResourceManager.ApplyResources((object) this.mnFindByTextNCS, "mnFindByTextNCS");
    this.mnFindByTextNCS.Click += new EventHandler(this.mnFindByTextNCS_Click);
    this.mnCopy.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.mnCopyRecordCode,
      (ToolStripItem) this.mnCopyRecordKey,
      (ToolStripItem) this.mnCopyRecordGuid
    });
    this.mnCopy.Name = "mnCopy";
    componentResourceManager.ApplyResources((object) this.mnCopy, "mnCopy");
    this.mnCopyRecordCode.Name = "mnCopyRecordCode";
    componentResourceManager.ApplyResources((object) this.mnCopyRecordCode, "mnCopyRecordCode");
    this.mnCopyRecordCode.Click += new EventHandler(this.On_CopyRecordCode_Click);
    this.mnCopyRecordKey.Name = "mnCopyRecordKey";
    componentResourceManager.ApplyResources((object) this.mnCopyRecordKey, "mnCopyRecordKey");
    this.mnCopyRecordKey.Click += new EventHandler(this.On_CopyRecordKey_Click);
    this.mnCopyRecordGuid.Name = "mnCopyRecordGuid";
    componentResourceManager.ApplyResources((object) this.mnCopyRecordGuid, "mnCopyRecordGuid");
    this.mnCopyRecordGuid.Click += new EventHandler(this.On_CopyRecordGuid_Click);
    this.mnCopyCurrentCell.Name = "mnCopyCurrentCell";
    componentResourceManager.ApplyResources((object) this.mnCopyCurrentCell, "mnCopyCurrentCell");
    this.mnCopyCurrentCell.Click += new EventHandler(this.CopyCurrentCell_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this._miAutoResize.Name = "_miAutoResize";
    componentResourceManager.ApplyResources((object) this._miAutoResize, "_miAutoResize");
    this._miAutoResize.Click += new EventHandler(this.On_miAutoResize_Click);
    this._miDataSize.Name = "_miDataSize";
    componentResourceManager.ApplyResources((object) this._miDataSize, "_miDataSize");
    this._miDataSize.Click += new EventHandler(this.On_miDataSize_Click);
    componentResourceManager.ApplyResources((object) this._miViewSetting, "_miViewSetting");
    this._miViewSetting.Name = "_miViewSetting";
    this._miViewSetting.Click += new EventHandler(this.On_miViewSetting_Click);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.FromArgb(163, 73, 164);
    this.imageList1.Images.SetKeyName(0, "normacs.bmp");
    this._printDoc.BeginPrint += new PrintEventHandler(this.On_printDoc_BeginPrint);
    this._printDoc.EndPrint += new PrintEventHandler(this.On_printDoc_EndPrint);
    this._printDoc.PrintPage += new PrintPageEventHandler(this.On_printDoc_PrintPage);
    componentResourceManager.ApplyResources((object) this.toolStrip1, "toolStrip1");
    this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
    this.toolStrip1.Items.AddRange(new ToolStripItem[14]
    {
      (ToolStripItem) this._tbPrint,
      (ToolStripItem) this.toolStripSeparator5,
      (ToolStripItem) this.tbNormaCS,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.tbFilter,
      (ToolStripItem) this.tbUserFilter,
      (ToolStripItem) this.toolStripTextBox1,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this.tbSaveDisplaySettings,
      (ToolStripItem) this.toolStripLabel1,
      (ToolStripItem) this._cbDisplayMode,
      (ToolStripItem) this.toolStripLabel2,
      (ToolStripItem) this._cmbRole,
      (ToolStripItem) this.lbRecords
    });
    this.toolStrip1.Name = "toolStrip1";
    this._tbPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._tbPrint, "_tbPrint");
    this._tbPrint.Name = "_tbPrint";
    this._tbPrint.Click += new EventHandler(this.OnPrint_Click);
    this.toolStripSeparator5.Name = "toolStripSeparator5";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator5, "toolStripSeparator5");
    this.tbNormaCS.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbNormaCS.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tbLaunchNormaCS,
      (ToolStripItem) this.tbFindByNumberNCS,
      (ToolStripItem) this.tbFindByNameNCS,
      (ToolStripItem) this.tbFindByTextNCS
    });
    componentResourceManager.ApplyResources((object) this.tbNormaCS, "tbNormaCS");
    this.tbNormaCS.Name = "tbNormaCS";
    this.tbLaunchNormaCS.Name = "tbLaunchNormaCS";
    componentResourceManager.ApplyResources((object) this.tbLaunchNormaCS, "tbLaunchNormaCS");
    this.tbLaunchNormaCS.Click += new EventHandler(this.tbLaunchNormaCS_Click);
    this.tbFindByNumberNCS.Name = "tbFindByNumberNCS";
    componentResourceManager.ApplyResources((object) this.tbFindByNumberNCS, "tbFindByNumberNCS");
    this.tbFindByNumberNCS.Click += new EventHandler(this.tbFindByNumberNCS_Click);
    this.tbFindByNameNCS.Name = "tbFindByNameNCS";
    componentResourceManager.ApplyResources((object) this.tbFindByNameNCS, "tbFindByNameNCS");
    this.tbFindByNameNCS.Click += new EventHandler(this.tbFindByNameNCS_Click);
    this.tbFindByTextNCS.Name = "tbFindByTextNCS";
    componentResourceManager.ApplyResources((object) this.tbFindByTextNCS, "tbFindByTextNCS");
    this.tbFindByTextNCS.Click += new EventHandler(this.TbFindByTextNCS_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator2, "toolStripSeparator2");
    this.tbFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbFilter.DropDownItems.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.mnUsedRecords,
      (ToolStripItem) this.toolStripSeparator4,
      (ToolStripItem) this.mnDataFilter,
      (ToolStripItem) this.mnCleanFilter,
      (ToolStripItem) this.mnFilterOptions
    });
    componentResourceManager.ApplyResources((object) this.tbFilter, "tbFilter");
    this.tbFilter.Name = "tbFilter";
    this.mnUsedRecords.CheckOnClick = true;
    this.mnUsedRecords.Name = "mnUsedRecords";
    componentResourceManager.ApplyResources((object) this.mnUsedRecords, "mnUsedRecords");
    this.mnUsedRecords.CheckedChanged += new EventHandler(this.OnShowUsedRecords_Click);
    this.toolStripSeparator4.Name = "toolStripSeparator4";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator4, "toolStripSeparator4");
    this.mnDataFilter.CheckOnClick = true;
    this.mnDataFilter.Name = "mnDataFilter";
    componentResourceManager.ApplyResources((object) this.mnDataFilter, "mnDataFilter");
    this.mnDataFilter.Click += new EventHandler(this.mnDataFilter_Click);
    this.mnCleanFilter.Name = "mnCleanFilter";
    componentResourceManager.ApplyResources((object) this.mnCleanFilter, "mnCleanFilter");
    this.mnCleanFilter.Click += new EventHandler(this.OnCleanFilter_Click);
    this.mnFilterOptions.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.mnFilterTop,
      (ToolStripItem) this.mnFilterLeft
    });
    this.mnFilterOptions.Name = "mnFilterOptions";
    componentResourceManager.ApplyResources((object) this.mnFilterOptions, "mnFilterOptions");
    this.mnFilterTop.Checked = true;
    this.mnFilterTop.CheckState = CheckState.Checked;
    this.mnFilterTop.Name = "mnFilterTop";
    componentResourceManager.ApplyResources((object) this.mnFilterTop, "mnFilterTop");
    this.mnFilterTop.Click += new EventHandler(this.OnFilterTop_Click);
    this.mnFilterLeft.Name = "mnFilterLeft";
    componentResourceManager.ApplyResources((object) this.mnFilterLeft, "mnFilterLeft");
    this.mnFilterLeft.Click += new EventHandler(this.OnFilterTop_Click);
    this.tbUserFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbUserFilter.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.miApplyUserFilter,
      (ToolStripItem) this.toolStripSeparator6,
      (ToolStripItem) this.miEditUresFilter,
      (ToolStripItem) this.miDeleteUserFilter
    });
    componentResourceManager.ApplyResources((object) this.tbUserFilter, "tbUserFilter");
    this.tbUserFilter.Name = "tbUserFilter";
    this.miApplyUserFilter.CheckOnClick = true;
    this.miApplyUserFilter.Name = "miApplyUserFilter";
    componentResourceManager.ApplyResources((object) this.miApplyUserFilter, "miApplyUserFilter");
    this.miApplyUserFilter.CheckedChanged += new EventHandler(this.miApplyUserFilter_CheckedChanged);
    this.toolStripSeparator6.Name = "toolStripSeparator6";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator6, "toolStripSeparator6");
    this.miEditUresFilter.CheckOnClick = true;
    this.miEditUresFilter.Name = "miEditUresFilter";
    componentResourceManager.ApplyResources((object) this.miEditUresFilter, "miEditUresFilter");
    this.miEditUresFilter.Click += new EventHandler(this.OnEditUresFilter_Click);
    this.miDeleteUserFilter.Name = "miDeleteUserFilter";
    componentResourceManager.ApplyResources((object) this.miDeleteUserFilter, "miDeleteUserFilter");
    this.miDeleteUserFilter.Click += new EventHandler(this.OnDeleteUserFilter_Click);
    this.toolStripTextBox1.Name = "toolStripTextBox1";
    componentResourceManager.ApplyResources((object) this.toolStripTextBox1, "toolStripTextBox1");
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator3, "toolStripSeparator3");
    this.tbSaveDisplaySettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tbSaveDisplaySettings, "tbSaveDisplaySettings");
    this.tbSaveDisplaySettings.Name = "tbSaveDisplaySettings";
    this.tbSaveDisplaySettings.Click += new EventHandler(this.On_btnSaveDisplaySettings_Click);
    this.toolStripLabel1.Name = "toolStripLabel1";
    componentResourceManager.ApplyResources((object) this.toolStripLabel1, "toolStripLabel1");
    this._cbDisplayMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cbDisplayMode.DropDownWidth = 136;
    componentResourceManager.ApplyResources((object) this._cbDisplayMode, "_cbDisplayMode");
    this._cbDisplayMode.Name = "_cbDisplayMode";
    this.toolStripLabel2.Name = "toolStripLabel2";
    componentResourceManager.ApplyResources((object) this.toolStripLabel2, "toolStripLabel2");
    this._cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this._cmbRole, "_cmbRole");
    this._cmbRole.Name = "_cmbRole";
    this.lbRecords.Name = "lbRecords";
    componentResourceManager.ApplyResources((object) this.lbRecords, "lbRecords");
    this._extender.DataGridView = (DataGridView) null;
    gridFilterFactory2.CreateDistinctGridFilters = false;
    gridFilterFactory2.DefaultGridFilterType = typeof (TextGridFilterCombo);
    gridFilterFactory2.DefaultShowDateInBetweenOperator = false;
    gridFilterFactory2.DefaultShowNumericInBetweenOperator = true;
    gridFilterFactory2.HandleEnumerationTypes = true;
    gridFilterFactory2.MaximumDistinctValues = 20;
    this._extender.FilterFactory = (IGridFilterFactory) gridFilterFactory2;
    this._extender.FilterText = "";
    this._extender.AfterFiltersChanged += new EventHandler(this.Extender_AfterFiltersChanged);
    this._extender.BeforeFiltersChanging += new EventHandler(this.Extender_BeforeFiltersChanging);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.toolStrip1);
    this.Controls.Add((Control) this._splitContainer);
    this.Name = nameof (TableView);
    this.VisibleChanged += new EventHandler(this.TableView_VisibleChanged);
    this.Resize += new EventHandler(this.TableView_Resize);
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.EndInit();
    this._splitContainer.ResumeLayout(false);
    this._noteImageContainer.Panel1.ResumeLayout(false);
    this._noteImageContainer.EndInit();
    this._noteImageContainer.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    ((ISupportInitialize) this._grid).EndInit();
    this._contextMenu.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this._extender.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public delegate Dictionary<long, Color> ColorizeRowsEventHandler(
    IUserSession session,
    AttributeTypeProperties[] properties,
    DataTable dataTable);

  public delegate bool RowSelecting(AttributeTypeProperties[] properties, DataRow row);

  public class SelEventArgs : EventArgs
  {
    public bool RestoreMode;

    public SelEventArgs(bool restoreMode) => this.RestoreMode = restoreMode;
  }

  public class CheckEventArgs : CancelEventArgs
  {
    public CheckEventArgs(bool currentValue) => this.Checked = currentValue;

    public bool Checked { get; }
  }
}
