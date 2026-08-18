// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.TableEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions;
using GridViewExtensions.GridFilterFactories;
using GridViewExtensions.GridFilters;
using GridViewExtensions.GridFilters.EnumerationSources;
using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Expressions;
using Intermech.Imbase.API;
using Intermech.Imbase.BackgroundTask;
using Intermech.Imbase.Clipboard;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Indexes;
using Intermech.Imbase.Selection;
using Intermech.Imbase.Templates;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expressions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.Search.UI;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class TableEditor : DockControl, ICommandTarget, IFindTarget, ISecurityCallback
{
  internal const string TableEditorGuid = "3c867640-5326-4b43-9479-d82a8a02f876";
  private long _userId = -1;
  private string _userGuid;
  private List<ColumnViewInfo> _viewInfo;
  private XmlNode _userSettings;
  internal long _tableId = -1;
  internal DateTime _tableDate = DateTime.MinValue;
  private long _linkId = -1;
  private int _attributesViewId = -1;
  private int _relationTypeID = -1;
  private long _parentID = -1;
  private DataSet _dataSet;
  internal DataTable _dataTable;
  private DataTable _attTable;
  private CheckOutMode _checkOutMode;
  private bool _checkoutNeed;
  private bool _isAdmin;
  private IImbaseParamsService _imbaseParamsService;
  internal DataTable _proxyTable;
  internal DataSet _proxyDataSet;
  internal DataTableTransactionLog _undoLog;
  private List<DeletedRecord> _deletedRows = new List<DeletedRecord>();
  private List<CalculatedColumn> _calcColumns;
  private IMSAttributeType[] _namedValuesData;
  private NamedValue[] _namedValues;
  private Hashtable _proxyToDataColumns;
  private DataTable _createdObjects;
  private List<long> _usedKeys = new List<long>();
  private List<DataGridViewColumn> _protectedColumns = new List<DataGridViewColumn>();
  private List<DataGridViewColumn> _readOnlyColumns = new List<DataGridViewColumn>();
  private bool _isPortalReadOnly;
  private Dictionary<long, long> _needUpdateObjects = new Dictionary<long, long>();
  private INamedImageList _nil;
  private int _imgObjectIndex = -1;
  private int _lockedIndex = -1;
  private List<AttributeValues> _linkAttributes = new List<AttributeValues>();
  private AttributeTypeProperties[] _rowsAttProps;
  private int _guidColumnIndex = -1;
  private int _keyColumnIndex = -1;
  private bool _calculating;
  private bool _groupChanging;
  private bool _loading;
  private List<long> _bookmarks = new List<long>(32 /*0x20*/);
  private List<long> _filter = new List<long>(32 /*0x20*/);
  private bool _filterChanged;
  private IClipboard _clipboard;
  private QuickObjectInfo _tableInfo;
  private QuickObjectInfo _linkInfo;
  private int _findPos = -1;
  private bool _isColsOrderChanged;
  private bool _ignoreEvents;
  private Dictionary<int, int> _colsOrderDict = new Dictionary<int, int>();
  private List<DataGridViewColumn> _recordRefColumns = new List<DataGridViewColumn>();
  private List<DataGridViewColumn> _objectRefColumns = new List<DataGridViewColumn>();
  internal Dictionary<string, string> _recordRefMap = new Dictionary<string, string>();
  internal Dictionary<string, string> _objectRefMap = new Dictionary<string, string>();
  internal CalcContext _calcContext = new CalcContext(0L);
  internal DataGridViewColumn _recOwnerColumn;
  internal DataGridViewColumn _recModDateColumn;
  internal bool _canChangeRecOwner;
  internal Dictionary<DataGridViewColumn, MasterColDef> _depMappingColumns = new Dictionary<DataGridViewColumn, MasterColDef>();
  internal int _recOwnerColumnIndex = -1;
  internal int _recModDateColumnIndex = -1;
  internal long _lastCheckedRowId = -1;
  private SymbolSelectRB_Ctrl _templatesTree;
  private object _templateBody;
  private Dictionary<int, int> _colsWidthDict = new Dictionary<int, int>();
  private Dictionary<string, IAttributePropertyDescriber> _describers = new Dictionary<string, IAttributePropertyDescriber>();
  private SecurityEditorForm _securityEditorForm;
  private ICommandManager _commandManager;
  private ICommandState _undoCommandState;
  private ICommandState _redoCommandState;
  private int _undoPosition;
  private bool _redoEnabled;
  private bool _undoEnabled;
  private const int SCAN_SIZE = 256 /*0x0100*/;
  private DataView _depView;
  private string _emptyFilter;
  private int _securityCategory = 25;
  private INotificationService _notificationService;
  private IContainer components;
  private bool _disposed;
  private Intermech.Bars.ToolBar toolBar1;
  private MenuBar menuBar1;
  private ContextMenuBarItem contextMenuBarItem1;
  private MenuButtonItem mnSaveChanges;
  private MenuButtonItem mnCancelChanges;
  private MenuButtonItem mnSelection;
  private MenuButtonItem mnFilter;
  private DoubleBufferedDataGridView _grid;
  private MenuButtonItem mnCheckOut;
  private MenuButtonItem mnCapitalize;
  private MenuButtonItem mnNewRecord;
  private MenuButtonItem mnCopyRecord;
  private MenuButtonItem mnDeleteRecord;
  private MenuButtonItem mnFind;
  private MenuButtonItem mnReplace;
  private MenuButtonItem mnRepeadFind;
  private MenuButtonItem mnSelectByCondition;
  private MenuButtonItem mnSelectSameRecords;
  private MenuButtonItem mnSelectRecord;
  private MenuButtonItem mnClearSelection;
  private MenuButtonItem mnInvertSelection;
  private MenuButtonItem mnDeleteSelected;
  private MenuButtonItem mnCreateTable;
  private MenuButtonItem mnFilterShow;
  private MenuButtonItem mnFilterAdd;
  private MenuButtonItem mnFilterRemove;
  private MenuButtonItem mnFilterClear;
  private MenuButtonItem mnCheckIn;
  private ComboBoxItem cbQuickSearch;
  private ButtonItem btFind;
  private ButtonItem btReplace;
  private ButtonItem btCheckOut;
  private ButtonItem btCheckIn;
  private ButtonItem btCancelChanges;
  private ButtonItem btCut;
  private ButtonItem btCopy;
  private DropDownMenuItem btPaste;
  private MenuButtonItem mnClearClipboard;
  private ButtonItem btEditStructure;
  private DropDownMenuItem btManualFilter;
  private MenuButtonItem mnFilterShow2;
  private MenuButtonItem mnFilterAdd2;
  private MenuButtonItem mnFilterRemove2;
  private MenuButtonItem mnFilterClear2;
  private LabelItem labelItem1;
  private ButtonItem btNewRecord;
  private ButtonItem btSaveChanges;
  private SplitContainer _spltContainer;
  private Panel _pnlBottom;
  private Button _btnSetFilter;
  private Button _btnSelect;
  private CheckBox _chbAutoSelect;
  private Splitter _splitter;
  private Panel _pnlFilter;
  private TreeView _trv;
  private ButtonItem _btnTree;
  private TreeBuilder _treeBuilder;
  private ButtonItem btProperties;
  private ButtonItem _btnCheckDataSet;
  private ButtonItem btCancelCheckOut;
  private ImageList imageList1;
  private StatusStrip statusStrip1;
  private ToolStripStatusLabel sbRecNumPanel;
  private MenuButtonItem mnObjectProps;
  private MenuButtonItem _securityEditSelectedRows;
  private MenuButtonItem _securityCurrentRow;
  private MenuButtonItem menuButtonItem2;
  private ToolStripStatusLabel sbAttImage;
  private ToolStripStatusLabel sbShortName;
  private ToolStripStatusLabel sbLongName;
  private SplitContainer splitContainer1;
  private DataGridFilterExtender _extender;
  private LayoutedGridFilterFactoryControl leftFilterFactory;
  private DropDownMenuItem mnExpFilter;
  private MenuButtonItem mnFilterOn;
  private MenuButtonItem mnOnlyData;
  private MenuButtonItem mnFilterClean;
  private ToolStripStatusLabel sbRecKey;
  private MenuButtonItem miNormaCS;
  private MenuButtonItem miLaunchNormaCS;
  private MenuButtonItem miFindByNumberNCS;
  private MenuButtonItem miFindByNameNCS;
  private MenuButtonItem miFindByTextNCS;
  private DropDownMenuItem tbNormaCS;
  private MenuButtonItem tbLaunchNormaCS;
  private MenuButtonItem tbFindByNumberNCS;
  private MenuButtonItem tbFindByNameNCS;
  private MenuButtonItem FindByTextNCS;
  private ButtonItem btAddRecOwner;
  private ToolStripStatusLabel sbRecGuid;
  private MenuButtonItem menuButtonItem17;
  private MenuButtonItem menuButtonItem18;
  private MenuButtonItem menuButtonItem19;
  private MenuButtonItem menuButtonItem20;
  private ToolStripSplitButton sbFindId;
  private ButtonItem btAddRecDate;

  internal static event ImbaseTableChangedHandler TableChanged;

  private SecurityEditorForm SecurityEditorForm
  {
    get
    {
      if (this._securityEditorForm == null)
        this._securityEditorForm = new SecurityEditorForm();
      return this._securityEditorForm;
    }
  }

  public event EventHandler InvalidateGrid;

  public TableEditor()
  {
    this.InitializeComponent();
    this._grid.DataError += new DataGridViewDataErrorEventHandler(this.Grid_DataError);
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service != null)
    {
      this._nil = service;
      this._imgObjectIndex = service.ImageIndex("imgObject");
      this._lockedIndex = service.ImageIndex("imgLock");
      this.TabImageIndex = service.ImageIndex(sc_7788.ssp_imbase_7789());
      this.AssignToolbarImages(service);
    }
    this.Subscribe();
    this.UpdateButtons();
    this.cbQuickSearch.ComboBox.KeyPress += new KeyPressEventHandler(this.OnQuickSearchComboBox_KeyPress);
    this.cbQuickSearch.ComboBox.MouseDoubleClick += new MouseEventHandler(this.OnQuickSearch_MouseDoubleClick);
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 891);
    this._commandManager = ServicesManager.GetService<ICommandManager>();
    this._undoCommandState = this._commandManager.FindCommand("Undo");
    this._redoCommandState = this._commandManager.FindCommand("Redo");
    this.ApplyVisualSettings();
  }

  public TableEditor(long parentID, int relationTypeID)
    : this()
  {
    this._parentID = parentID;
    this._relationTypeID = relationTypeID;
  }

  private void AssignToolbarImages(INamedImageList nil)
  {
    this.toolBar1.ImageList = nil.ImageList;
    this.menuBar1.ImageList = nil.ImageList;
    this._btnTree.ImageIndex = nil.ImageIndex("imgTreeView");
    this.btNewRecord.ImageIndex = nil.ImageIndex("imgNewItem");
    this.btCheckOut.ImageIndex = this.mnCheckOut.ImageIndex = nil.ImageIndex("imgCheckOut");
    this.btCheckIn.ImageIndex = this.mnCheckIn.ImageIndex = nil.ImageIndex("imgCheckIn");
    this.btSaveChanges.ImageIndex = this.mnSaveChanges.ImageIndex = nil.ImageIndex("imgSaveChanges");
    this.btCancelChanges.ImageIndex = this.mnCancelChanges.ImageIndex = nil.ImageIndex("imgCancelChanges");
    this.btFind.ImageIndex = this.mnFind.ImageIndex = nil.ImageIndex("imgFind");
    this.btReplace.ImageIndex = this.mnReplace.ImageIndex = nil.ImageIndex("imgReplace");
    this.btCopy.ImageIndex = nil.ImageIndex("imgCopy");
    this.btPaste.ImageIndex = nil.ImageIndex("imgPaste");
    this.btCut.ImageIndex = nil.ImageIndex("imgCut");
    this.btEditStructure.ImageIndex = nil.ImageIndex("imgTableRestructure");
    this.btAddRecOwner.ImageIndex = nil.ImageIndex("imgUser");
    this.btAddRecDate.ImageIndex = nil.ImageIndex("imgOrganizerCalendar");
    this.btManualFilter.ImageIndex = nil.ImageIndex("imgRecFilter");
    this.mnExpFilter.ImageIndex = nil.ImageIndex("imgRecFilter");
    this.btProperties.ImageIndex = nil.ImageIndex("imgProp");
    this._btnCheckDataSet.ImageIndex = nil.ImageIndex("imgCheckBriefcase");
    this.mnClearClipboard.ImageIndex = nil.ImageIndex("imgDelete");
    this.mnFilterShow.ImageIndex = this.mnFilterShow2.ImageIndex = nil.ImageIndex("imgRecFilterShow");
    this.mnFilterAdd.ImageIndex = this.mnFilterAdd2.ImageIndex = nil.ImageIndex("imgRecFilterAdd");
    this.mnFilterRemove.ImageIndex = this.mnFilterRemove2.ImageIndex = nil.ImageIndex("imgRecFilterRemove");
    this.mnFilterClear.ImageIndex = this.mnFilterClear2.ImageIndex = nil.ImageIndex("imgRecFilterClear");
    this.tbNormaCS.ImageIndex = this.miNormaCS.ImageIndex = this.tbLaunchNormaCS.ImageIndex = this.miLaunchNormaCS.ImageIndex = nil.ImageIndex("imgNormaCS");
    int imageIndex = this.btFind.ImageIndex;
    this.sbFindId.Image = nil.ImageList.Images[imageIndex];
  }

  private void Subscribe()
  {
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service1)
    {
      service1.RendererChanged += new EventHandler(this.Toolbar_RendererChanged);
      this.Toolbar_RendererChanged((object) service1, EventArgs.Empty);
    }
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2)
    {
      this._notificationService = service2;
      service2.Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectAttributeChanged));
      service2.Subscribe("ProjectChanged", new NotificationEventHandler(this.ProjectChanged));
      service2.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.OnObjectCheckedIn));
      service2.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.OnObjectCheckedOut));
      service2.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.OnObjectChangesCancelled));
      this._ignoreEvents = false;
    }
    this._clipboard = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
    if (this._clipboard != null)
      this._clipboard.ContextChanged += new EventHandler(this.OnClipboardContextChanged);
    FindService.Closed += new EventHandler(this.FindService_Closed);
  }

  private void UnSubscribe()
  {
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
      service.RendererChanged -= new EventHandler(this.Toolbar_RendererChanged);
    INotificationService notificationService = this._notificationService;
    if (notificationService != null)
    {
      notificationService.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectAttributeChanged));
      notificationService.Unsubscribe("ProjectChanged", new NotificationEventHandler(this.ProjectChanged));
      notificationService.Unsubscribe("ObjectsCheckedIn", new NotificationEventHandler(this.OnObjectCheckedIn));
      notificationService.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.OnObjectCheckedOut));
      notificationService.Unsubscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.OnObjectChangesCancelled));
    }
    if (this._clipboard != null)
      this._clipboard.ContextChanged -= new EventHandler(this.OnClipboardContextChanged);
    EditorHelper._editors.Remove(this);
    FindService.Closed -= new EventHandler(this.FindService_Closed);
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

  private void StoreDisplaySettings()
  {
    if (this.TableId == 0L || this._userSettings == null)
      return;
    this._userSettings.InnerXml = string.Empty;
    XmlDocument ownerDocument = this._userSettings.OwnerDocument;
    if (ownerDocument == null)
      return;
    for (int index = 0; index < this._grid.Columns.Count; ++index)
    {
      Guid empty = Guid.Empty;
      DataGridViewColumn column = this._grid.Columns[index];
      if (this.GetColumnGuid(Convert.ToInt32(column.Name), this._rowsAttProps, ref empty))
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
        this._userSettings.AppendChild((XmlNode) element);
      }
    }
  }

  private void LoadDisplaySettings(IUserSession session)
  {
    IDBObject dbObject = session.GetObject(Math.Abs(this._tableId), false);
    if (dbObject == null)
      return;
    ITablesDisplayService customService = session.GetCustomService(typeof (ITablesDisplayService)) as ITablesDisplayService;
    QuickObjectInfo objectInfo = session.GetObjectInfo(session.UserID);
    XmlDocument xmlDocument1 = new XmlDocument();
    xmlDocument1.InnerXml = customService.GetObjectSettingsForUser(dbObject.GUID, objectInfo.VersionGuid);
    XmlDocument xmlDocument2 = xmlDocument1;
    XmlElement element = xmlDocument2.CreateElement(xmlDocument2.FirstChild.Name);
    element.SetAttribute("Guid", objectInfo.VersionGuid.ToString());
    this._userSettings = (XmlNode) element;
    this._userSettings.InnerXml = xmlDocument2.FirstChild.InnerXml;
  }

  private void LoadViewInfo()
  {
    if (this._userSettings == null)
      return;
    this._viewInfo = new List<ColumnViewInfo>(this._userSettings.ChildNodes.Count);
    foreach (XmlNode childNode in this._userSettings.ChildNodes)
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
  }

  private void SaveDisplaySettings()
  {
    this.StoreDisplaySettings();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(Math.Abs(this._tableId), false);
      if (dbObject == null)
        return;
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(sessionKeeper.Session.UserID);
      ServiceUtils.GetService<ITablesDisplayService>((object) sessionKeeper.Session, true).SaveSettingsForObject(dbObject.GUID, objectInfo.VersionGuid, Guid.Empty, string.Empty, Intermech.Interfaces.Imbase.DisplayMode.PersonalMode, string.Empty, this._userSettings.OuterXml, string.Empty);
    }
  }

  private bool IsOwnedTable(NotificationEventArgs e)
  {
    return e is DBObjectsEventArgs objectsEventArgs && objectsEventArgs.ObjectIDs.Contains(this._tableId);
  }

  private void OnObjectChangesCancelled(object sender, NotificationEventArgs e)
  {
    if (this._ignoreEvents)
      return;
    try
    {
      this._ignoreEvents = true;
      if (!this.IsOwnedTable(e))
        return;
      this._tableId = Math.Abs(this._tableId);
      this.OnCancelChanges((object) this, EventArgs.Empty);
    }
    finally
    {
      this._ignoreEvents = false;
    }
  }

  private void OnObjectCheckedOut(object sender, NotificationEventArgs e)
  {
    if (this._ignoreEvents)
      return;
    try
    {
      this._ignoreEvents = true;
      if (!this.IsOwnedTable(e))
        return;
      this.OnCheckOut((object) this, EventArgs.Empty);
    }
    finally
    {
      this._ignoreEvents = false;
    }
  }

  private void OnObjectCheckedIn(object sender, NotificationEventArgs e)
  {
    if (this._ignoreEvents)
      return;
    try
    {
      this._ignoreEvents = true;
      if (!this.IsOwnedTable(e))
        return;
      this._tableId = Math.Abs(this._tableId);
      this.OnCancelChanges((object) this, EventArgs.Empty);
    }
    finally
    {
      this._ignoreEvents = false;
    }
  }

  private void OnObjectAttributeChanged(object sender, NotificationEventArgs e)
  {
    bool flag1 = false;
    bool flag2 = false;
    if (!(e is DBObjectsExtendedEventArgs extendedEventArgs))
      return;
    if (ApplicationServices.Container.GetService(typeof (IObjectsInfoCache)) is IObjectsInfoCache service && service is IClientObjectsInfoCache objectsInfoCache)
    {
      foreach (long objectId in (IEnumerable<long>) extendedEventArgs.ObjectIDs)
      {
        if (objectsInfoCache.ResetInfo(objectId))
        {
          QuickObjectInfo objectInfo = service.GetObjectInfo(objectId);
          if (!objectInfo.Empty)
          {
            string key = objectInfo.VersionGuid.ToString();
            if (this._objectRefMap.ContainsKey(key))
            {
              this._objectRefMap[key] = objectInfo.Caption;
              flag1 = true;
            }
          }
        }
      }
    }
    if (extendedEventArgs.ObjectIDs.Contains(this._tableId) || extendedEventArgs.ObjectIDs.Contains(this._linkId))
    {
      int length = extendedEventArgs.AttributeValuesArray.Length;
      for (int index1 = 0; index1 < length; ++index1)
      {
        DataColumn column = this._proxyTable.Columns[extendedEventArgs.AttributeValuesArray[index1].AttributeID.ToString()];
        if (column != null)
        {
          if (column.ExtendedProperties.ContainsKey((object) "F_OBJECTID") && Convert.ToInt64(column.ExtendedProperties[(object) "F_OBJECTID"]) == extendedEventArgs.ObjectIDs[0])
          {
            AttributeValues attributeValues = extendedEventArgs.AttributeValuesArray[index1];
            object obj = attributeValues.Values[0];
            if (attributeValues.AttributeType == FieldTypes.ftMeasured)
            {
              MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(obj.ToString());
              long num = Intermech.Imbase.Consts.mmUnitID;
              if (column.ExtendedProperties.ContainsKey((object) "F_MEASURE"))
                num = Convert.ToInt64(column.ExtendedProperties[(object) "F_MEASURE"]);
              if (MeasureHelper.GetBaseMeasureID(measuredValue.MeasureID) == MeasureHelper.GetBaseMeasureID(num))
                measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, num);
              obj = (object) measuredValue.Value;
            }
            if (attributeValues.MultipleValued == MultiValueModes.SingleValueFromList)
            {
              IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeValues.AttributeID);
              if (attributeType.PossibleValues != null)
              {
                int index2 = attributeType.PossibleValues.IndexOf(obj);
                if (index2 != -1)
                {
                  string str = Convert.ToString(attributeType.PossibleValuesDescriptions[index2]);
                  if (!string.IsNullOrEmpty(str))
                    obj = (object) str;
                }
              }
            }
            if (attributeValues.AttributeType == FieldTypes.ftObjectLink)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(Convert.ToInt64(obj));
                if (!objectInfo.Empty)
                  obj = (object) objectInfo.VersionGuid;
                string key = Convert.ToString(obj);
                if (!this._objectRefMap.Keys.Contains<string>(key))
                  this._objectRefMap.Add(key, objectInfo.Caption);
              }
            }
            column.Expression = TableLoadHelper.QuoteString(Convert.ToString(obj));
            flag1 = true;
          }
          else if (string.IsNullOrEmpty(column.Expression))
            flag2 = true;
        }
      }
    }
    if (flag1)
      this.RecalcTable();
    if (!flag2)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.LoadTables(sessionKeeper.Session);
      this.UpdateButtons();
      this.Text = base.Text;
      this.MapColumns(sessionKeeper.Session);
    }
  }

  private void FireNotification(string notificationName, long objectId)
  {
    if (this._ignoreEvents)
      return;
    this._notificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs(notificationName, objectId));
  }

  private void ProjectChanged(object sender, NotificationEventArgs e) => this.ApplyVisualSettings();

  private void ApplyVisualSettings()
  {
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) is Font font))
      return;
    this._grid.Font = font;
    this._grid.RowTemplate.Height = FontHelper.MeasureStringFast(this._grid.Font, "Ay").Height + 6 + 3;
  }

  private static void OnTableChanged(TableEditor editor, ImbaseTableChangedEventArgs e)
  {
    ImbaseTableChangedHandler tableChanged = TableEditor.TableChanged;
    if (tableChanged == null)
      return;
    tableChanged((object) editor, e);
  }

  protected override string GetPersistString()
  {
    return this._linkId != -1L ? $"{this._linkId},{this._parentID}" : $"{this._tableId},{this._parentID}";
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    this.SaveDisplaySettings();
    if (!this.IsDirty())
      return;
    if (this._grid.IsCurrentCellInEditMode)
    {
      int num = (int) MessageBox.Show($"Текущая запись таблицы находится в режиме редактирования.{Environment.NewLine}Отмените изменения или завершите редактирование перед закрытием окна.", "Таблица редактируется", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      e.Cancel = true;
    }
    else
    {
      switch (MessageBox.Show(LocalizationHolder.rm.GetString("IMB_TABLECHANGED"), LocalizationHolder.rm.GetString("IMB_WARN"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
      {
        case DialogResult.Cancel:
          e.Cancel = true;
          break;
        case DialogResult.Yes:
          e.Cancel = !this.SaveChanges(true);
          break;
      }
    }
  }

  private void SetRenderer(IToolBarRenderer renderer)
  {
    this.menuBar1.Renderer = renderer;
    this.toolBar1.Renderer = renderer;
  }

  private void Toolbar_RendererChanged(object sender, EventArgs e)
  {
    this.SetRenderer((sender as BarManager).Renderer);
  }

  internal void Initialize(long tableId, long parentId)
  {
    this._spltContainer.Panel1Collapsed = true;
    this._linkId = parentId;
    this._tableId = tableId;
    this.LoadData();
    foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
    {
      int result;
      if (int.TryParse(column.Name, out result))
        this._colsOrderDict.Add(result, column.DisplayIndex);
    }
  }

  private void LoadData()
  {
    this.DetachExtender();
    this._loading = true;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        this._imbaseParamsService = ServiceUtils.GetService<IImbaseParamsService>((object) session, true);
        this._isAdmin = session.IsAdmin;
        this._userId = session.UserID;
        this._tableInfo = session.GetObjectInfo(this._tableId);
        if (this._linkId != -1L)
          this._linkInfo = session.GetObjectInfo(this._linkId);
        base.Text = this._tableInfo.Caption;
        this.CheckAccessRights(session, this._tableId);
        if (!this._grid.ReadOnly)
          this.CheckPortalRights(session, this._tableId);
        this.GetTableAttributes(session);
        this.GetLinkAttributes(session);
        this.LoadTableDataSet(session);
        this.LoadDisplaySettings(session);
        this.MapColumns(session);
        this.LoadViewInfo(session);
        if (this._filter.Count > 0)
          this._bookmarks.AddRange((IEnumerable<long>) this._filter);
        this.UpdateButtons();
      }
    }
    finally
    {
      this._loading = false;
    }
    this._needUpdateObjects.Clear();
  }

  private void GetLinkAttributes(IUserSession session)
  {
    this._linkAttributes.Clear();
    this._filter.Clear();
    if (this._linkId < 0L && this._linkId != -1L && session.GetObjectInfo(this._linkId).Empty)
      this._linkId = Math.Abs(this._linkId);
    if (this._linkId == -1L)
      return;
    IDBObject dbObject = session.GetObject(this._linkId);
    AttributeValues[] attributesValues = dbObject.GetAttributesValues(TableEditor.ImbaseAttValuesModes);
    int length = attributesValues.Length;
    for (int index = 0; index < length; ++index)
      this._linkAttributes.Add(attributesValues[index]);
    IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ManualTableFilterId);
    if (attributeById == null)
      return;
    this.LoadManualFilter(attributeById);
  }

  private void MapColumns(IUserSession session)
  {
    this._recOwnerColumn = (DataGridViewColumn) null;
    this._recModDateColumn = (DataGridViewColumn) null;
    this._recordRefColumns.Clear();
    this._objectRefColumns.Clear();
    this._depMappingColumns.Clear();
    this._readOnlyColumns.Clear();
    this._protectedColumns.Clear();
    this._describers.Clear();
    IntPtr handle = this.Handle;
    DataGridViewColumnCollection columns = this._grid.Columns;
    int count = columns.Count;
    int num1 = 1;
    IImbaseServer server = EditorHelper.GetServer(session);
    DataGridViewColumn[] dataGridViewColumnArray = new DataGridViewColumn[count];
    QuickObjectInfo objectInfo1 = session.GetObjectInfo(session.UserID);
    this._userGuid = objectInfo1.VersionGuid.ToString();
    this.LoadViewInfo();
    using (Graphics graphics = this._grid.CreateGraphics())
    {
      IAttributePropertyDescriberService service = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;
      for (int index1 = 0; index1 < count; ++index1)
      {
        DataGridViewColumn dataGridViewColumn = columns[index1];
        int attId;
        if (int.TryParse(dataGridViewColumn.Name, out attId))
        {
          if (attId == -12 || attId == -2)
          {
            dataGridViewColumn.Visible = false;
            int index2 = count - num1++;
            if (dataGridViewColumnArray.Length > index2 && index2 >= 0)
              dataGridViewColumnArray[index2] = dataGridViewColumn;
          }
          else
          {
            DataColumn column = this._proxyTable.Columns[dataGridViewColumn.Name];
            int num2 = !this._proxyToDataColumns.ContainsKey((object) column) ? 1 : 0;
            int index3 = TableEditor.IndexOfAttProp(attId, this._rowsAttProps);
            dataGridViewColumn.HeaderText = TableEditor.GetColumnCaption(this._rowsAttProps[index3], column.ExtendedProperties[(object) "F_MEASURE_U"] as string);
            if (this._colsWidthDict.ContainsKey(attId))
            {
              int num3 = this._colsWidthDict[attId];
              if (num3 > 0)
                dataGridViewColumn.Width = num3;
            }
            else
            {
              ColumnViewInfo columnViewInfo = this._viewInfo.FirstOrDefault<ColumnViewInfo>((System.Func<ColumnViewInfo, bool>) (x => x.attId == attId));
              if (columnViewInfo != null)
                dataGridViewColumn.Width = columnViewInfo.width;
            }
            int int32 = Convert.ToInt32(dataGridViewColumn.Name);
            IDBAttributeType attributeType1 = session.GetAttributeType(int32);
            if (num2 != 0)
            {
              dataGridViewColumn.ReadOnly = true;
              dataGridViewColumn.DefaultCellStyle.SelectionForeColor = Color.Yellow;
              dataGridViewColumn.DefaultCellStyle.ForeColor = Color.Blue;
              dataGridViewColumn.Tag = (object) "expression";
              if (attributeType1.AttributeType == FieldTypes.ftObjectLink)
                this._objectRefColumns.Add(dataGridViewColumn);
            }
            else
            {
              bool flag = false;
              IAttributePropertyDescriber describer = service?.GetDescriber(int32);
              if (describer != null)
                this._describers.Add(dataGridViewColumn.Name, describer);
              if (TableLoadHelper.IsArray(attributeType1))
              {
                TextWithButtonColumn withButtonColumn = new TextWithButtonColumn();
                withButtonColumn.DataPropertyName = withButtonColumn.Name = dataGridViewColumn.Name;
                withButtonColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                withButtonColumn.HeaderText = dataGridViewColumn.HeaderText;
                withButtonColumn.TextReadOnly = true;
                withButtonColumn.Width = dataGridViewColumn.Width;
                withButtonColumn.ButtonClick += new EventHandler(this.ArrayColumn_ButtonClick);
                columns.Remove(dataGridViewColumn);
                dataGridViewColumn = (DataGridViewColumn) withButtonColumn;
                if (attributeType1.AttributeType == FieldTypes.ftObjectLink)
                  this._objectRefColumns.Add((DataGridViewColumn) withButtonColumn);
                flag = true;
              }
              else if (attributeType1.MultipleValued == MultiValueModes.SingleValueFromList && !(dataGridViewColumn is DataGridViewComboBoxColumn))
              {
                DataTable possibleValues = attributeType1.GetPossibleValues();
                possibleValues.CaseSensitive = true;
                if ((attributeType1.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
                {
                  DataRow row = possibleValues.NewRow();
                  row[0] = row[1] = (object) DBNull.Value;
                  row[2] = (object) " ";
                  possibleValues.Rows.InsertAt(row, 0);
                }
                foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
                {
                  if (row[2] == DBNull.Value || string.IsNullOrEmpty(row[2].ToString()))
                    row[2] = row[1];
                }
                if (possibleValues.Columns.IndexOf(attributeType1.TextFieldName) != -1 && possibleValues.Columns[attributeType1.TextFieldName].DataType == typeof (Decimal))
                {
                  possibleValues.Columns[attributeType1.TextFieldName].ColumnName = "F_DECIMAL_VALUE";
                  if (attributeType1.TextFieldName == "F_INTEGER_VALUE")
                    possibleValues.Columns.Add(attributeType1.TextFieldName, typeof (long)).Expression = "F_DECIMAL_VALUE";
                  else if (attributeType1.TextFieldName == "F_DOUBLE_VALUE")
                    possibleValues.Columns.Add(attributeType1.TextFieldName, typeof (double)).Expression = "F_DECIMAL_VALUE";
                }
                string str = attributeType1.TextFieldName;
                string fieldName = attributeType1.PossibleValueFieldName;
                if (attributeType1.AttributeType == FieldTypes.ftMeasured && column.ExtendedProperties.Contains((object) "F_MEASURE"))
                {
                  str = "F_DOUBLE";
                  possibleValues.Columns.Add(str, typeof (double));
                  long int64 = Convert.ToInt64(column.ExtendedProperties[(object) "F_MEASURE"]);
                  MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(int64);
                  foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
                  {
                    string mValue = Convert.ToString(row[attributeType1.TextFieldName]);
                    if (!string.IsNullOrEmpty(mValue))
                    {
                      MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, descriptor, true);
                      if (measuredValue.MeasureID != int64)
                        measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, int64);
                      row[str] = (object) measuredValue.Value;
                    }
                    else
                      row[str] = (object) DBNull.Value;
                  }
                }
                if (attributeType1.AttributeType == FieldTypes.ftObjectLink)
                {
                  str = "F_GUID";
                  fieldName = str;
                  possibleValues.Columns.Add(str, typeof (string));
                  foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
                  {
                    if (!DBNull.Value.Equals(row[attributeType1.ValueFieldName]))
                    {
                      long int64 = Convert.ToInt64(row[attributeType1.ValueFieldName]);
                      if (DBNull.Value.Equals(row[str]))
                      {
                        QuickObjectInfo objectInfo2 = session.GetObjectInfo(int64);
                        if (objectInfo2.Empty)
                        {
                          row[str] = (object) int64.ToString();
                        }
                        else
                        {
                          row[str] = (object) objectInfo2.VersionGuid.ToString();
                          row[2] = (object) objectInfo2.Caption;
                        }
                      }
                    }
                  }
                }
                object extendedProperty1 = column.ExtendedProperties[(object) "F_FILTERED_POSSIBLE_VALUES"];
                object extendedProperty2 = column.ExtendedProperties[(object) "F_DEPEND_POSSIBLE_VALUES"];
                DataGridViewComboBoxColumn dgvColumn = new DataGridViewComboBoxColumn();
                dgvColumn.DataPropertyName = dgvColumn.Name = dataGridViewColumn.Name;
                dgvColumn.HeaderText = dataGridViewColumn.HeaderText;
                dgvColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                dgvColumn.DisplayStyleForCurrentCellOnly = true;
                dgvColumn.DataSource = this.GetFilteredPossibleValues(possibleValues, this._dataTable, extendedProperty1, extendedProperty2, fieldName, str, possibleValues.Columns[2].ColumnName, (DataGridViewColumn) dgvColumn);
                dgvColumn.DisplayMember = possibleValues.Columns[2].ColumnName;
                dgvColumn.ValueMember = str;
                dgvColumn.MaxDropDownItems = 10;
                columns.Remove(dataGridViewColumn);
                dataGridViewColumn = (DataGridViewColumn) dgvColumn;
                flag = true;
              }
              else if ((this._rowsAttProps[index3].Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef || (attributeType1.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef)
              {
                TextWithButtonColumn withButtonColumn = new TextWithButtonColumn();
                withButtonColumn.DataPropertyName = withButtonColumn.Name = dataGridViewColumn.Name;
                withButtonColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                withButtonColumn.HeaderText = dataGridViewColumn.HeaderText;
                withButtonColumn.TextReadOnly = true;
                withButtonColumn.Width = dataGridViewColumn.Width;
                withButtonColumn.ButtonClick += new EventHandler(this.RecordReference_ButtonClick);
                columns.Remove(dataGridViewColumn);
                dataGridViewColumn = (DataGridViewColumn) withButtonColumn;
                this._recordRefColumns.Add((DataGridViewColumn) withButtonColumn);
                flag = true;
              }
              else if (attributeType1.AttributeType == FieldTypes.ftObjectLink)
              {
                TextWithButtonColumn withButtonColumn = new TextWithButtonColumn();
                withButtonColumn.DataPropertyName = withButtonColumn.Name = dataGridViewColumn.Name;
                withButtonColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                withButtonColumn.HeaderText = dataGridViewColumn.HeaderText;
                withButtonColumn.TextReadOnly = true;
                withButtonColumn.Width = dataGridViewColumn.Width;
                withButtonColumn.Tag = (object) attributeType1.SizeType;
                this._canChangeRecOwner = false;
                if (attId == Intermech.Imbase.Consts.ImbaseTableRecordOwnerAttID)
                {
                  this._recOwnerColumn = (DataGridViewColumn) withButtonColumn;
                  if (session.GetAttributeType(Intermech.Imbase.Consts.ImbaseTableRecordOwnerAttID) is IDBSecurity attributeType2)
                  {
                    if (!attributeType2.CheckAccess(ActionType.Write, false, false))
                    {
                      this._canChangeRecOwner = false;
                      withButtonColumn.DefaultCellStyle.BackColor = Color.Silver;
                    }
                    else
                      this._canChangeRecOwner = true;
                  }
                }
                withButtonColumn.ButtonClick += new EventHandler(this.ObjectReference_ButtonClick);
                columns.Remove(dataGridViewColumn);
                dataGridViewColumn = (DataGridViewColumn) withButtonColumn;
                this._objectRefColumns.Add((DataGridViewColumn) withButtonColumn);
                flag = true;
              }
              else if (attributeType1.AttributeType == FieldTypes.ftMemo)
              {
                TextWithButtonColumn withButtonColumn = new TextWithButtonColumn();
                string name;
                string str = name = dataGridViewColumn.Name;
                withButtonColumn.Name = name;
                withButtonColumn.DataPropertyName = str;
                withButtonColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                withButtonColumn.HeaderText = dataGridViewColumn.HeaderText;
                withButtonColumn.TextReadOnly = false;
                withButtonColumn.Width = dataGridViewColumn.Width;
                withButtonColumn.Tag = (object) attributeType1.SizeType;
                withButtonColumn.ButtonClick += new EventHandler(this.MemoColumn_ButtonClick);
                columns.Remove(dataGridViewColumn);
                dataGridViewColumn = (DataGridViewColumn) withButtonColumn;
                flag = true;
              }
              else if (attributeType1.AttributeType == FieldTypes.ftDateTime)
              {
                if (attId == Intermech.Imbase.Consts.ImbaseTableRecordModDateAttID)
                {
                  this._recModDateColumn = dataGridViewColumn;
                  this._recModDateColumn.ReadOnly = true;
                }
                else
                {
                  DataGridViewCalendarColumn viewCalendarColumn = new DataGridViewCalendarColumn();
                  string name;
                  string str = name = dataGridViewColumn.Name;
                  viewCalendarColumn.Name = name;
                  viewCalendarColumn.DataPropertyName = str;
                  viewCalendarColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                  viewCalendarColumn.HeaderText = dataGridViewColumn.HeaderText;
                  viewCalendarColumn.Width = dataGridViewColumn.Width;
                  viewCalendarColumn.Tag = (object) attributeType1.SizeType;
                  columns.Remove(dataGridViewColumn);
                  dataGridViewColumn = (DataGridViewColumn) viewCalendarColumn;
                  flag = true;
                }
              }
              else if (attributeType1.AttributeID == Intermech.Imbase.Consts.ImbaseTemplateAttID)
              {
                LabelWithButtonColumn withButtonColumn = new LabelWithButtonColumn();
                string name;
                string str = name = dataGridViewColumn.Name;
                withButtonColumn.Name = name;
                withButtonColumn.DataPropertyName = str;
                withButtonColumn.HeaderText = dataGridViewColumn.HeaderText;
                withButtonColumn.ButtonClick += new EventHandler(this.LabelWithButtonEditingControl_ButtonClick);
                columns.Remove(dataGridViewColumn);
                dataGridViewColumn = (DataGridViewColumn) withButtonColumn;
                flag = true;
                this._btnTree.Enabled = true;
              }
              if (flag)
              {
                columns.Add(dataGridViewColumn);
                --count;
                if (index1 > 0)
                  --index1;
              }
              dataGridViewColumn.HeaderCell.Style.Font = new Font(this._grid.DefaultCellStyle.Font, FontStyle.Bold);
              if (dataGridViewColumn is DataGridViewComboBoxColumn cbc)
                this.CalculateDropDownWidth(graphics, cbc);
              if (index3 != -1)
              {
                object defValue = this._rowsAttProps[index3].DefaultValue;
                if (column.DataType == typeof (MeasuredValue) && defValue != null && defValue != DBNull.Value)
                  defValue = (object) MeasureHelper.ConvertToMeasuredValue(defValue.ToString());
                if (column.DataType.Equals(typeof (DateTime)) && Intermech.Consts.CurrentDateFunction.Equals(defValue))
                  defValue = (object) DateTime.Now;
                if (TableLoadHelper.IsNull(defValue) && (this._rowsAttProps[index3].Options & AttributeOptions.Imbase_DontUseDefaultsWithNull) != AttributeOptions.Imbase_DontUseDefaultsWithNull)
                {
                  defValue = this.GetDefalultValue(attId);
                  if (this._rowsAttProps[index3].FieldType == FieldTypes.ftObjectLink && !TableLoadHelper.IsNull(defValue))
                  {
                    long int64 = Convert.ToInt64(defValue);
                    QuickObjectInfo objectInfo3 = session.GetObjectInfo(int64);
                    if (!objectInfo3.Empty)
                      defValue = (object) objectInfo3.VersionGuid.ToString();
                  }
                }
                if (column.DataType.Equals(typeof (ValuesArray)) && defValue != null && defValue != DBNull.Value)
                  defValue = (object) TableLoadHelper.CreateArray(column.ExtendedProperties[(object) "dataType"] as System.Type, defValue);
                if (attId == Intermech.Imbase.Consts.ImbaseTableRecordOwnerAttID)
                  defValue = (object) objectInfo1.VersionGuid.ToString();
                if (!TableLoadHelper.IsNull(defValue))
                {
                  try
                  {
                    if (this._rowsAttProps[index3].FieldType == FieldTypes.ftMeasured)
                    {
                      string mValue = Convert.ToString(defValue);
                      if (!string.IsNullOrEmpty(mValue))
                      {
                        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue);
                        if (column.ExtendedProperties.Contains((object) "F_MEASURE"))
                        {
                          long int64 = Convert.ToInt64(column.ExtendedProperties[(object) "F_MEASURE"]);
                          if (int64 != measuredValue.MeasureID && MeasureHelper.GetBaseMeasureID_ByMeasureID(measuredValue.MeasureID) == MeasureHelper.GetBaseMeasureID_ByMeasureID(int64))
                            measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, int64);
                          defValue = (object) measuredValue.Value;
                        }
                      }
                    }
                    column.DefaultValue = defValue;
                  }
                  catch (Exception ex)
                  {
                    int num4 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1135"), defValue, (object) dataGridViewColumn.HeaderText, (object) Environment.NewLine, (object) ex.Message), LocalizationHolder.rm.GetString("Imbase.Client_1136"));
                  }
                }
                IDBSecurity securityForAtt = server.GetSecurityForAtt(session.SessionGUID, this._tableId, attId);
                if (!securityForAtt.CheckAccess(ActionType.Edit, true, false))
                {
                  dataGridViewColumn.ReadOnly = true;
                  dataGridViewColumn.DefaultCellStyle.BackColor = Color.Silver;
                  this._readOnlyColumns.Add(dataGridViewColumn);
                }
                if (!securityForAtt.CheckAccess(ActionType.View, true, false))
                {
                  dataGridViewColumn.ReadOnly = true;
                  dataGridViewColumn.DefaultCellStyle.BackColor = Color.Silver;
                  this._protectedColumns.Add(dataGridViewColumn);
                }
                if (this._isPortalReadOnly && (this._rowsAttProps[index3].Options & AttributeOptions.EditableLocalImbaseAttribute) != AttributeOptions.EditableLocalImbaseAttribute)
                {
                  dataGridViewColumn.ReadOnly = true;
                  dataGridViewColumn.DefaultCellStyle.BackColor = Color.Silver;
                  if (!this._readOnlyColumns.Contains(dataGridViewColumn))
                    this._readOnlyColumns.Add(dataGridViewColumn);
                }
              }
            }
            if (index3 > -1 && index3 < dataGridViewColumnArray.Length)
              dataGridViewColumnArray[index3] = dataGridViewColumn;
            if (column.DataType.IsValueType)
            {
              if (column.DataType == typeof (bool))
                dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              else
                dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
          }
        }
      }
    }
    int num5 = 0;
    for (int index = 0; index < dataGridViewColumnArray.Length; ++index)
    {
      if (dataGridViewColumnArray[index] != null)
        dataGridViewColumnArray[index].DisplayIndex = num5++;
    }
    if (this._depMappingColumns.Count > 0)
    {
      foreach (MasterColDef masterColDef in this._depMappingColumns.Values)
        masterColDef.ColumnIndex = this._grid.Columns[masterColDef.AttId.ToString()].Index;
    }
    this.CheckValidForDepColumns();
    this.CalcRecordRefColumnsData();
    this.CalcObjectRefColumnsData(objectInfo1);
    this.AttachExtender();
  }

  private void CheckValidForDepColumns()
  {
    Parallel.ForEach<KeyValuePair<DataGridViewColumn, MasterColDef>>((IEnumerable<KeyValuePair<DataGridViewColumn, MasterColDef>>) this._depMappingColumns, (Action<KeyValuePair<DataGridViewColumn, MasterColDef>>) (item => this.CheckValidColumn(item.Value)));
  }

  private void CheckValidColumn(MasterColDef value)
  {
  }

  private object GetFilteredPossibleValues(
    DataTable dt,
    DataTable dataTable,
    object filter,
    object dep,
    string fieldName,
    string valueName,
    string displayNameName,
    DataGridViewColumn dgvColumn)
  {
    object filteredPossibleValues = (object) dt;
    if (filter != null && filter != DBNull.Value && filter is object[] array)
    {
      string[] collection = Array.ConvertAll<object, string>(array, new Converter<object, string>(Convert.ToString));
      if (collection != null && collection.Length != 0)
      {
        List<string> stringList = new List<string>((IEnumerable<string>) collection);
        int index = 0;
        while (index < dt.Rows.Count)
        {
          object obj = dt.Rows[index][fieldName];
          if (obj != null && obj != DBNull.Value)
          {
            string str = obj.ToString();
            if (!string.IsNullOrEmpty(str) && !stringList.Contains(str))
            {
              dt.Rows.RemoveAt(index);
              continue;
            }
          }
          ++index;
        }
        dt.AcceptChanges();
      }
    }
    if (dep != null && dep is Tuple<string, List<Tuple<object, object>>> tuple1)
    {
      string str = tuple1.Item1;
      DataColumn column1 = dataTable.Columns[str];
      if (column1 != null)
      {
        DataTable dataTable1 = new DataTable();
        dataTable1.Columns.Add("F_MAIN", column1.DataType);
        DataColumn column2 = dt.Columns[valueName];
        DataColumn column3 = dt.Columns[displayNameName];
        dataTable1.Columns.Add(column2.ColumnName, column2.DataType);
        dataTable1.Columns.Add(column3.ColumnName, column3.DataType);
        Dictionary<object, object> dictionary = new Dictionary<object, object>();
        foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
        {
          dataTable1.Rows.Add(new object[3]
          {
            null,
            row[column2],
            row[column3]
          });
          dictionary.Add(row[column2], row[column3]);
        }
        foreach (Tuple<object, object> tuple in tuple1.Item2)
        {
          object key = tuple.Item2;
          dataTable1.Rows.Add(tuple.Item1, key, dictionary[key]);
        }
        int index = TableEditor.IndexOfAttProp(new Guid(str), this._rowsAttProps);
        if (index >= 0)
        {
          this._depMappingColumns.Add(dgvColumn, new MasterColDef(this._rowsAttProps[index].AttributeID));
          filteredPossibleValues = (object) dataTable1.DefaultView;
        }
      }
    }
    return filteredPossibleValues;
  }

  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    if (this._grid == null)
      return;
    using (Graphics graphics = this._grid.CreateGraphics())
    {
      foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
      {
        if (column is DataGridViewComboBoxColumn cbc)
          this.CalculateDropDownWidth(graphics, cbc);
      }
    }
  }

  private void CalculateDropDownWidth(Graphics g, DataGridViewComboBoxColumn cbc)
  {
    if (cbc.DropDownWidth > 1 || !(cbc.DataSource is DataTable dataSource))
      return;
    string displayMember = cbc.DisplayMember;
    int columnIndex = dataSource.Columns.IndexOf(displayMember);
    if (columnIndex == -1)
      return;
    int count = dataSource.Rows.Count;
    Font font1 = this._grid.Font;
    if (font1 == null)
      return;
    using (Font font2 = new Font(font1, FontStyle.Bold))
    {
      int num1 = 1;
      for (int index = 0; index < count; ++index)
      {
        string text = dataSource.Rows[index][columnIndex].ToString();
        if (!string.IsNullOrEmpty(text))
        {
          int num2 = (int) g.MeasureString(text, font2).Width + 8;
          if (num2 < 512 /*0x0200*/ && num2 > num1)
            num1 = num2;
        }
      }
      cbc.DropDownWidth = num1;
    }
  }

  private Color InterpolateColors(Color color1, Color color2, float Percentage)
  {
    int r1 = (int) color1.R;
    int g1 = (int) color1.G;
    int b1 = (int) color1.B;
    int r2 = (int) color2.R;
    int g2 = (int) color2.G;
    int b2 = (int) color2.B;
    int red = (int) Convert.ToByte((float) r1 + (float) (r2 - r1) * Percentage);
    byte num1 = Convert.ToByte((float) g1 + (float) (g2 - g1) * Percentage);
    byte num2 = Convert.ToByte((float) b1 + (float) (b2 - b1) * Percentage);
    int green = (int) num1;
    int blue = (int) num2;
    return Color.FromArgb(red, green, blue);
  }

  private object GetDefalultValue(int attId)
  {
    int count = this._linkAttributes.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this._linkAttributes[index].AttributeID == attId)
      {
        object defalultValue = this._linkAttributes[index].Values[0];
        if (!(defalultValue is MeasuredValue measuredValue))
          return defalultValue;
        return measuredValue.ToString().Equals(string.Empty) ? (object) DBNull.Value : (object) measuredValue.Value;
      }
    }
    return (object) null;
  }

  internal static string GetColumnCaption(AttributeTypeProperties attProp, string units)
  {
    string str1 = string.Empty;
    string str2 = string.Empty;
    if (attProp.ShortName.Length > 0)
      str1 = $" [{attProp.ShortName}]";
    if (attProp.Alias.Length > 0)
      str2 = $" ({attProp.Alias})";
    string str3 = $"{attProp.Name}{str1}{str2}";
    if (units != null && units.Length > 0)
      str3 = $"{str3}, {units}";
    return str3.Trim();
  }

  internal static int IndexOfAttProp(int attributeId, AttributeTypeProperties[] props)
  {
    if (props != null)
    {
      int length = props.Length;
      for (int index = 0; index < length; ++index)
      {
        if (props[index].AttributeID == attributeId)
          return index;
      }
    }
    return -1;
  }

  internal static int IndexOfAttProp(string attName, AttributeTypeProperties[] props)
  {
    if (props != null)
    {
      int length = props.Length;
      for (int index = 0; index < length; ++index)
      {
        if (props[index].Name.Equals(attName))
          return index;
      }
    }
    return -1;
  }

  internal static int IndexOfAttProp(Guid attGuid, AttributeTypeProperties[] props)
  {
    if (props != null)
    {
      int length = props.Length;
      for (int index = 0; index < length; ++index)
      {
        if (props[index].AttributeGuid.Equals(attGuid))
          return index;
      }
    }
    return -1;
  }

  internal static int IndexOfAttProp(int attributeId, Attribute4ObjectTypeProperties[] props)
  {
    if (props != null)
    {
      int length = props.Length;
      for (int index = 0; index < length; ++index)
      {
        if (props[index].AttributeID == attributeId)
          return index;
      }
    }
    return -1;
  }

  private void LoadViewInfo(IUserSession session)
  {
  }

  private void CheckPortalRights(IUserSession session, long objID)
  {
    this._isPortalReadOnly = false;
    IImbaseDBObject imbaseDbObject = session.GetObject(objID, false) as IImbaseDBObject;
    try
    {
      this._isPortalReadOnly = imbaseDbObject.ReadonlyPublished;
    }
    catch
    {
    }
    if (!this._isPortalReadOnly)
      return;
    this.mnNewRecord.Enabled = this.mnReplace.Enabled = this.mnFilter.Enabled = this.mnDeleteRecord.Enabled = this.mnDeleteSelected.Enabled = false;
    this.btNewRecord.Enabled = this.btReplace.Enabled = this.btManualFilter.Enabled = this.btCut.Enabled = this.btPaste.Enabled = false;
  }

  private void CheckAccessRights(IUserSession session, long objID)
  {
    IDBObject dbObject = session.GetObject(objID, false);
    if (dbObject == null)
      this._grid.ReadOnly = true;
    if (!this._grid.ReadOnly)
    {
      long checkoutBy = dbObject.CheckoutBy;
      if (dbObject is IDBSecurity dbSecurity)
      {
        ObjectModifyModes objectModifyMode = dbObject.ObjectModifyMode;
        bool flag1 = !dbSecurity.CheckAccess(ActionType.Edit, true, false) || objectModifyMode == ObjectModifyModes.CantModify || objectModifyMode == ObjectModifyModes.Checkout && checkoutBy != 0L && checkoutBy != this._userId || objectModifyMode == ObjectModifyModes.CreateVersion && checkoutBy != 0L && checkoutBy != this._userId;
        this._grid.ReadOnly = flag1 || !dbSecurity.CheckAccess(ActionType.EditTableData, true, false);
        ButtonItem btAddRecOwner = this.btAddRecOwner;
        ButtonItem btAddRecDate = this.btAddRecDate;
        ButtonItem btEditStructure = this.btEditStructure;
        bool flag2;
        this.btProperties.Enabled = flag2 = !flag1 && dbSecurity.CheckAccess(ActionType.EditTableStructureAndProperties, true, false);
        int num1;
        bool flag3 = (num1 = flag2 ? 1 : 0) != 0;
        btEditStructure.Enabled = num1 != 0;
        int num2;
        bool flag4 = (num2 = flag3 ? 1 : 0) != 0;
        btAddRecDate.Enabled = num2 != 0;
        int num3 = flag4 ? 1 : 0;
        btAddRecOwner.Enabled = num3 != 0;
        this.mnCopyRecord.Enabled = this.mnNewRecord.Enabled = this.btNewRecord.Enabled = dbSecurity.CheckAccess(ActionType.AddNewRows, true, false);
      }
      else
        this._grid.ReadOnly = true;
      if (!this._grid.ReadOnly)
      {
        if (objID != this._linkId)
          return;
        this.CheckAccessRights(session, this._tableId);
      }
    }
    if (!this._grid.ReadOnly)
      return;
    this.mnNewRecord.Enabled = this.mnReplace.Enabled = this.mnFilter.Enabled = this.mnDeleteRecord.Enabled = this.mnDeleteSelected.Enabled = false;
    this.btNewRecord.Enabled = this.btReplace.Enabled = this.btManualFilter.Enabled = this.btCut.Enabled = this.btPaste.Enabled = false;
    this.btAddRecOwner.Enabled = false;
    this.btAddRecDate.Enabled = false;
  }

  private void GetTableAttributes(IUserSession session)
  {
    IDBObject tableObject = session.GetObject(this._tableId);
    if (this.UpdateCheckoutStatus(tableObject))
      tableObject = session.GetObject(this._tableId);
    IDBAttribute attributeById = tableObject.GetAttributeByID(session.IdentHelper.ModifyContentDateID);
    if (attributeById != null && !attributeById.IsNull)
      this._tableDate = attributeById.AsDateTime;
    this.PreAnalizeAttributes(tableObject.GetAttributesValues(TableEditor.ImbaseAttValuesModes));
  }

  private bool IsTableAttribute(AttributeValues attVal)
  {
    return attVal.AttributeType == FieldTypes.ftSystem || ImbaseHelper.IsSystemAttribute(attVal.AttributeID);
  }

  private void PreAnalizeAttributes(AttributeValues[] atts)
  {
    this._attributesViewId = -1;
    int length = atts.Length;
    for (int index = 0; index < length; ++index)
    {
      AttributeValues att = atts[index];
      if (att.AttributeID == Intermech.Imbase.Consts.ImbaseTableViewAttID)
        this._attributesViewId = att.AttributeID;
    }
  }

  internal static AttributeValues FindAttributeValue(AttributeValues[] atts, object vdata)
  {
    if (atts == null)
      return (AttributeValues) null;
    if (vdata == null)
      return (AttributeValues) null;
    string str = vdata as string;
    int length = atts.Length;
    for (int index = 0; index < length; ++index)
    {
      AttributeValues att = atts[index];
      if (vdata is short && Convert.ToInt32(vdata) == att.AttributeID || str != null && (str.Equals(att.AttributeGuid.ToString()) || str.Equals(att.AttributeName) || str.Equals(att.AttributeAlias)))
        return att;
    }
    return (AttributeValues) null;
  }

  private void LoadTableDataSet(IUserSession session)
  {
    IImbaseServer server = EditorHelper.GetServer(session);
    this._dataSet = TableLoadHelper.GetTables(session, this._tableId, false);
    this.LoadTables(session);
    this.GetCreatedObjects(session, server);
  }

  private void GetCreatedObjects(IUserSession session, IImbaseServer server)
  {
    this._createdObjects = server.GetCreatedObjects(session.SessionGUID, this._linkId);
    this._usedKeys.Clear();
    if (this._createdObjects == null)
      return;
    int columnIndex = this._createdObjects.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID.ToString());
    if (columnIndex == -1)
      return;
    DataRowCollection rows = this._createdObjects.Rows;
    int count = rows.Count;
    for (int index = 0; index < count; ++index)
    {
      object obj = rows[index][columnIndex];
      if (obj != null && !DBNull.Value.Equals(obj))
      {
        long int64 = Convert.ToInt64(obj);
        if (!this._usedKeys.Contains(int64))
          this._usedKeys.Add(int64);
      }
    }
    this._usedKeys.Sort();
  }

  private void LoadTables(IUserSession session)
  {
    this.DetachExtender();
    this._grid.Columns.Clear();
    this._dataTable = this._dataSet.Tables["IMS_DATA"];
    this._attTable = this._dataSet.Tables["IMS_ATTR_TYPES"];
    this.FillOwnerColumn(session);
    this.FillModDateColumn(session);
    this._proxyTable = this._dataTable.Copy();
    this._deletedRows.Clear();
    this._proxyDataSet = new DataSet();
    this._proxyDataSet.Tables.Add(this._proxyTable);
    this._proxyDataSet.EnforceConstraints = false;
    this._dataSet.EnforceConstraints = false;
    this._calcColumns = new List<CalculatedColumn>();
    ImbaseKeyInfo keyInfo = new ImbaseKeyInfo();
    this._proxyTable.ExtendedProperties.Add((object) "CalcContext", (object) this._calcContext);
    TableLoadHelper.AssignAttributes(session, this._linkId, this._tableId, this._proxyTable, this._attTable, out this._rowsAttProps, this._calcColumns, ref keyInfo);
    if (this._calcColumns.Count > 0)
    {
      TableLoadHelper.GetNamedValuesData(this._proxyTable, this._rowsAttProps, out this._namedValuesData, out this._namedValues);
    }
    else
    {
      this._namedValues = (NamedValue[]) null;
      this._namedValuesData = (IMSAttributeType[]) null;
    }
    this._proxyTable.AcceptChanges();
    this._proxyToDataColumns = new Hashtable(this._dataTable.Columns.Count);
    foreach (DataColumn column1 in (InternalDataCollectionBase) this._proxyTable.Columns)
    {
      DataColumn column2 = this._dataTable.Columns[column1.ColumnName];
      if (column2 != null && !column1.ExtendedProperties.ContainsKey((object) "F_VIRTUAL"))
        this._proxyToDataColumns[(object) column1] = (object) column2;
      string columnName = column1.ColumnName;
      column1.ColumnName = column1.Caption;
      column1.Caption = columnName;
    }
    this.AttachUndoLog();
    this._grid.DataSource = (object) this._proxyTable;
    this.AttachTableEvents();
    this.SetPrimaryKey(this._dataTable);
    this.MapSystemColumns();
    this.UpdateRecsIndicator();
  }

  private void AttachUndoLog()
  {
    this._undoPosition = 0;
    if (this._undoLog != null)
    {
      this._undoLog.ClearLog();
      this._undoLog.TransactionAdded -= new DataTableTransactionLog.TransactionEventHandler(this.UndoLog_TransactionAdded);
      this._undoLog.TransactionAdding -= new DataTableTransactionLog.TransactionEventHandler(this.UndoLog_TransactionAdding);
    }
    this._undoLog = new DataTableTransactionLog(this._proxyTable);
    this._undoLog.TransactionAdded += new DataTableTransactionLog.TransactionEventHandler(this.UndoLog_TransactionAdded);
    this._undoLog.TransactionAdding += new DataTableTransactionLog.TransactionEventHandler(this.UndoLog_TransactionAdding);
    this.CanUndo = false;
    this.CanRedo = false;
  }

  private void UndoLog_TransactionAdding(object sender, TransactionEventArgs e)
  {
    if (e.Record.TransactionType == DataTableTransactionRecord.RecordType.ChangeField)
    {
      string columnName = e.Record.ColumnName;
      if (this._proxyTable.Columns.Contains(columnName) && this._proxyTable.Columns[columnName].ExtendedProperties.ContainsKey((object) "F_VIRTUAL"))
      {
        e.Cancel = true;
        return;
      }
    }
    this.CanRedo = false;
    if (this._undoPosition >= this._undoLog.Log.Count - 1)
      return;
    this._undoLog.Log.RemoveRange(this._undoPosition + 1, this._undoLog.Log.Count - (this._undoPosition + 1));
  }

  private void UndoLog_TransactionAdded(object sender, TransactionEventArgs e)
  {
    this._undoPosition = this._undoLog.Log.Count - 1;
    this._undoEnabled = this._undoCommandState.Enabled = true;
  }

  private void OnUndo()
  {
    DataTableTransactionRecord tr = this._undoLog.Undo(this._undoPosition, out DataRow _);
    --this._undoPosition;
    if (this._undoPosition < 0)
      this.CanUndo = false;
    this.CanRedo = true;
    this.NavigateToRow(tr);
  }

  private void OnRedo()
  {
    ++this._undoPosition;
    DataTableTransactionRecord tr = this._undoLog.Redo(this._undoPosition, out DataRow _);
    if (this._undoPosition == this._undoLog.Log.Count - 1)
      this.CanRedo = false;
    this.CanUndo = true;
    this.NavigateToRow(tr);
  }

  private void NavigateToRow(DataTableTransactionRecord tr)
  {
    switch (tr.Row.RowState)
    {
      case DataRowState.Detached:
        break;
      case DataRowState.Deleted:
        break;
      default:
        this.SetCurrentCell(Convert.ToInt64(tr.Row[this._keyColumnIndex]));
        if (tr.TransactionType != DataTableTransactionRecord.RecordType.ChangeField)
          break;
        this.TryRevertRow(tr.Row);
        if (this._grid.CurrentRow == null)
          break;
        int count = this._grid.Columns.Count;
        for (int index = 0; index < count; ++index)
        {
          if (this._grid.Columns[index].DataPropertyName == tr.ColumnName)
          {
            this._grid.CurrentCell = this._grid.CurrentRow.Cells[index];
            break;
          }
        }
        break;
    }
  }

  private void TryRevertRow(DataRow proxyRow)
  {
    DataRow dataRow = this._dataTable.Rows.Find(proxyRow[this._keyColumnIndex]);
    if (dataRow == null || dataRow.RowState != DataRowState.Modified)
      return;
    foreach (DataColumn column in (InternalDataCollectionBase) dataRow.Table.Columns)
    {
      if (!object.Equals(dataRow[column], dataRow[column, DataRowVersion.Original]))
        return;
    }
    try
    {
      this._calculating = true;
      this._undoLog.SuspendLogging();
      dataRow.RejectChanges();
    }
    finally
    {
      this._calculating = false;
      this._undoLog.ResumeLogging();
    }
  }

  private bool CanUndo
  {
    get => this._undoEnabled;
    set
    {
      this._undoEnabled = value;
      if (this._undoCommandState.Enabled == value)
        return;
      this._undoCommandState.Enabled = value;
    }
  }

  private bool CanRedo
  {
    get => this._redoEnabled;
    set
    {
      this._redoEnabled = value;
      if (this._redoCommandState.Enabled == value)
        return;
      this._redoCommandState.Enabled = value;
    }
  }

  private void MapSystemColumns()
  {
    this._keyColumnIndex = this._proxyTable.Columns.IndexOf("-2");
    this._guidColumnIndex = this._proxyTable.Columns.IndexOf("-12");
    this._recOwnerColumnIndex = this._proxyTable.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseTableRecordOwnerAttID.ToString());
    this._recModDateColumnIndex = this._proxyTable.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseTableRecordModDateAttID.ToString());
    DataColumn column = this._proxyTable.Columns["-2"];
    if (column == null)
      return;
    this._proxyTable.PrimaryKey = new DataColumn[1]
    {
      column
    };
  }

  private void SetPrimaryKey(DataTable dataTable)
  {
    DataColumn column = dataTable.Columns["F_KEY"];
    if (column == null)
      return;
    dataTable.PrimaryKey = new DataColumn[1]{ column };
  }

  private void AttachTableEvents()
  {
    this._proxyTable.ColumnChanged += new DataColumnChangeEventHandler(this.ProxyTable_ColumnChanged);
    this._proxyTable.ColumnChanging += new DataColumnChangeEventHandler(this.ProxyTable_ColumnChanging);
    this._proxyTable.RowChanged += new DataRowChangeEventHandler(this.ProxyTable_RowChanged);
    this._proxyTable.RowChanging += new DataRowChangeEventHandler(this.ProxyTable_RowChanging);
    this._proxyTable.TableNewRow += new DataTableNewRowEventHandler(this.ProxyTable_TableNewRow);
    this._proxyTable.RowDeleted += new DataRowChangeEventHandler(this.ProxyTable_RowDeleted);
    this._proxyTable.RowDeleting += new DataRowChangeEventHandler(this.ProxyTable_RowDeleting);
    this._dataTable.RowChanging += new DataRowChangeEventHandler(this.DataTable_RowChanging);
    this._dataTable.RowChanged += new DataRowChangeEventHandler(this.DataTable_RowChanged);
    this._dataTable.RowDeleting += new DataRowChangeEventHandler(this.DataTable_RowDeleting);
    this._dataTable.RowDeleted += new DataRowChangeEventHandler(this.DataTable_RowDeleted);
    this._dataTable.TableNewRow += new DataTableNewRowEventHandler(this.DataTable_TableNewRow);
    this._attTable.RowChanged += new DataRowChangeEventHandler(this.AttTable_RowChanged);
    this._attTable.RowDeleted += new DataRowChangeEventHandler(this.AttTable_RowDeleted);
    this._attTable.TableNewRow += new DataTableNewRowEventHandler(this.AttTable_TableNewRow);
  }

  private bool UpdateCheckoutStatus(IDBObject tableObject)
  {
    bool flag = false;
    this.CheckedOut = CheckOutMode.None;
    this._checkoutNeed = tableObject.ObjectModifyMode == ObjectModifyModes.Checkout;
    long checkoutBy = tableObject.CheckoutBy;
    if (checkoutBy == this._userId)
    {
      this.CheckedOut = CheckOutMode.CheckedOut;
      flag = true;
    }
    else if (checkoutBy != 0L)
      this.CheckedOut = CheckOutMode.OtherUser;
    return flag;
  }

  internal void CheckCheckout()
  {
    if (this._checkOutMode != CheckOutMode.None || !this._checkoutNeed)
      return;
    this.CheckOut();
  }

  private void CancelCheckOut()
  {
    if (this.CheckedOut == CheckOutMode.CheckedOut && this._tableId < 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        try
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._tableId);
          if (dbObject.CheckoutBy == this._userId)
            dbObject.CancelChanges();
          this.CheckedOut = CheckOutMode.None;
        }
        catch (Exception ex)
        {
          TableEditor.ShowException(ex);
        }
        this.FireNotification("ObjectsChangesCancelled", -this._tableId);
      }
    }
    this.ReloadTables();
  }

  private bool CheckIn(IUserSession session)
  {
    bool flag = false;
    if (this.CheckedOut == CheckOutMode.CheckedOut)
    {
      try
      {
        IDBObject dbObject = session.GetObject(this._tableId);
        if (dbObject.CheckoutBy == this._userId)
        {
          dbObject.CheckIn();
          flag = true;
        }
        this.FireNotification("ObjectsCheckedIn", this._tableId);
        this.CheckedOut = CheckOutMode.None;
      }
      catch (NotUniqueIndexValueException ex)
      {
        string caption = LocalizationHolder.rm.GetString("TableEditor_UndoCheckIn");
        this.NeedShowNotUniqueIndexes(ex.Table, ex.NotUniqueIndexes, ex.RowNumbers, caption, MessageBoxIcon.Hand);
      }
      catch (Exception ex)
      {
        TableEditor.ShowException(ex);
      }
    }
    return flag;
  }

  private void CheckOut()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._tableId);
        if (dbObject1.CheckoutBy != this._userId)
        {
          IDBObject dbObject2 = dbObject1.CheckOut(true);
          this._tableId = dbObject2.ObjectID;
          IDBAttribute attributeById = dbObject2.GetAttributeByID(sessionKeeper.Session.IdentHelper.ModifyContentDateID);
          if (attributeById != null && !attributeById.IsNull && attributeById.AsDateTime > this._tableDate)
            this.ReloadTables();
        }
        this.CheckedOut = CheckOutMode.CheckedOut;
      }
      catch (Exception ex)
      {
        TableEditor.ShowException(ex);
      }
    }
  }

  private static void ShowException(Exception ex)
  {
    int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Imbase.Client_45"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  private void DataSetChanging() => this.CheckCheckout();

  private void DataSetChanged()
  {
    this.RepaintCaption();
    this.UpdateButtons();
    this.UpdateRecsIndicator();
  }

  private void AttTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
  {
    this.DataSetChanged();
  }

  private void AttTable_RowDeleted(object sender, DataRowChangeEventArgs e)
  {
    this.DataSetChanged();
  }

  private void AttTable_RowChanged(object sender, DataRowChangeEventArgs e)
  {
    this.DataSetChanged();
  }

  private void DataTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
  {
    e.Row[this._guidColumnIndex] = (object) Guid.NewGuid();
    this.DataSetChanged();
  }

  private void DataTable_RowDeleting(object sender, DataRowChangeEventArgs e)
  {
    DataRow row = e.Row;
    long int64 = Convert.ToInt64(row[this._keyColumnIndex]);
    if (DeletedRecord.FindRowRecord(int64, this._deletedRows) == null)
      this._deletedRows.Add(new DeletedRecord(row, int64));
    this.DataSetChanging();
  }

  private void DataTable_RowDeleted(object sender, DataRowChangeEventArgs e)
  {
    this.DataSetChanged();
  }

  private void DataTable_RowChanging(object sender, DataRowChangeEventArgs e)
  {
    this.DataSetChanging();
  }

  private void DataTable_RowChanged(object sender, DataRowChangeEventArgs e)
  {
    this.DataSetChanged();
  }

  private void ProxyTable_RowDeleting(object sender, DataRowChangeEventArgs e)
  {
    long int64 = Convert.ToInt64(e.Row[this._keyColumnIndex]);
    DataRow dataRow = this._dataTable.Rows.Find((object) int64);
    if (this._recOwnerColumnIndex != -1 && !this._isAdmin && e.Row[this._recOwnerColumnIndex] != null && !string.Equals(this._userGuid, e.Row[this._recOwnerColumnIndex].ToString()))
      throw new AbortException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!EditorHelper.GetServer(session).GetSecurityForRecord(session.SessionGUID, Math.Abs(this._tableId), int64).CheckAccess(ActionType.Delete, true))
        throw new AbortException();
    }
    if (this._usedKeys.Contains(int64))
    {
      DeleteRecordMode deleteRecordMode = this._imbaseParamsService.CommonParams.DeleteRecordMode;
      if (this._isAdmin && deleteRecordMode == DeleteRecordMode.Disable)
        deleteRecordMode = DeleteRecordMode.Ask;
      switch (deleteRecordMode)
      {
        case DeleteRecordMode.Disable:
          int num = (int) MessageBox.Show("Запись не может быть удалена, потому что она была использована для создания объектов IPS.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          throw new AbortException();
        case DeleteRecordMode.Ask:
          if (MessageBox.Show("Запись была использована для создания объектов IPS.\nВы действительно хотите ее удалить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            throw new AbortException();
          break;
      }
    }
    dataRow?.Delete();
    this._bookmarks.Remove(int64);
    if (!this._filter.Contains(int64))
      return;
    this._filter.Remove(int64);
    this.FilterChanged = true;
  }

  private bool FilterChanged
  {
    set
    {
      if (value == this._filterChanged)
        return;
      this._filterChanged = value;
      this.DataSetChanged();
    }
  }

  private void ProxyTable_RowDeleted(object sender, DataRowChangeEventArgs e)
  {
  }

  private void ProxyTable_RowChanging(object sender, DataRowChangeEventArgs e)
  {
  }

  private void ProxyTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
  {
  }

  private void ProxyTable_RowChanged(object sender, DataRowChangeEventArgs e)
  {
    if (e.Action == DataRowAction.Add)
    {
      DeletedRecord rowRecord = DeletedRecord.FindRowRecord(Convert.ToInt64(e.Row[this._keyColumnIndex]), this._deletedRows);
      DataRow row;
      if (rowRecord != null)
      {
        row = rowRecord._row;
        if (row.RowState == DataRowState.Deleted)
          row.RejectChanges();
      }
      else
        row = this._dataTable.NewRow();
      foreach (DataColumn key in (IEnumerable) this._proxyToDataColumns.Keys)
      {
        DataColumn proxyToDataColumn = this._proxyToDataColumns[(object) key] as DataColumn;
        if (proxyToDataColumn.Ordinal > 0 && !proxyToDataColumn.ReadOnly)
          row[proxyToDataColumn] = e.Row[key];
      }
      if (this._dataTable.Rows.IndexOf(row) == -1)
      {
        if (rowRecord != null && rowRecord._index != -1)
          this._dataTable.Rows.InsertAt(row, rowRecord._index);
        else
          this._dataTable.Rows.Add(row);
      }
      int keyColumnIndex = this._keyColumnIndex;
      object objA = (object) e.Row[keyColumnIndex].ToString();
      object objB = (object) row[keyColumnIndex].ToString();
      if (objB != null && !object.Equals(objA, objB))
        row[keyColumnIndex] = objB;
    }
    e.Row.ClearErrors();
    if (e.Row.RowState == DataRowState.Detached || this._calculating)
      return;
    this._calculating = true;
    try
    {
      this.CalculateRow(e.Row);
    }
    finally
    {
      this._calculating = false;
    }
  }

  internal void CalculateRow(DataRow dataRow)
  {
    dataRow.RowError = string.Empty;
    if (this._calcColumns == null)
      return;
    int count = this._calcColumns.Count;
    for (int index = 0; index < count; ++index)
      this._calcColumns[index].Calculate(dataRow, this._calcContext, this._namedValuesData, this._namedValues);
  }

  private void ProxyTable_ColumnChanging(object sender, DataColumnChangeEventArgs e)
  {
  }

  private void ProxyTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
  {
    if (this._proxyToDataColumns[(object) e.Column] is DataColumn proxyToDataColumn)
    {
      DataRow dataRow = this._dataTable.Rows.Find(e.Row[this._keyColumnIndex]);
      if (dataRow != null)
      {
        dataRow[proxyToDataColumn] = e.ProposedValue;
        if (this._recModDateColumnIndex != -1 && e.Column.Ordinal != this._recModDateColumnIndex)
        {
          bool flag = this._undoLog.SuspendLogging();
          try
          {
            e.Row[this._recModDateColumnIndex] = (object) DateTime.Now;
          }
          finally
          {
            if (flag)
              this._undoLog.ResumeLogging();
          }
        }
      }
    }
    if (!this._calculating)
    {
      this._calculating = true;
      try
      {
        this.CalculateRow(e.Row);
      }
      finally
      {
        this._calculating = false;
      }
    }
    e.Row.ClearErrors();
  }

  internal DataColumn ProxyToDataColumn(DataColumn col)
  {
    return this._proxyToDataColumns[(object) col] as DataColumn;
  }

  internal static GetAttributeValuesModes ImbaseAttValuesModes
  {
    get
    {
      return GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeAlias | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.IncludeCaption;
    }
  }

  public long TableId => this._tableId;

  internal long RecordId
  {
    get
    {
      return this._keyColumnIndex == -1 || this._grid.CurrentRow == null ? -1L : Convert.ToInt64(this._grid.CurrentRow.Cells[this._keyColumnIndex].Value);
    }
  }

  internal Guid RecordGuid
  {
    get
    {
      Guid result;
      return this._keyColumnIndex == -1 || this._grid.CurrentRow == null || !Guid.TryParse(this._grid.CurrentRow.Cells[this._guidColumnIndex].Value.ToString(), out result) ? Guid.Empty : result;
    }
  }

  public long LinkId
  {
    get => this._linkId;
    set
    {
      if (this._linkId == value)
        return;
      this._linkId = value;
      this.RecalcLinkValues();
    }
  }

  public override Color BackColor
  {
    get
    {
      switch (this._checkOutMode)
      {
        case CheckOutMode.CheckedOut:
          return Color.SkyBlue;
        case CheckOutMode.OtherUser:
          return Color.Orange;
        default:
          return base.BackColor;
      }
    }
    set => base.BackColor = value;
  }

  public override string Text
  {
    get
    {
      string text = base.Text;
      if (this.IsDirty())
        text += "*";
      return text;
    }
    set => base.Text = value;
  }

  public CheckOutMode CheckedOut
  {
    get => this._checkOutMode;
    set
    {
      if (this._checkOutMode == value)
        return;
      this._checkOutMode = value;
      this._tableId = this._checkOutMode != CheckOutMode.CheckedOut ? Math.Abs(this._tableId) : -Math.Abs(this._tableId);
      if (this.Parent == null)
        return;
      base.BackColor = base.BackColor;
    }
  }

  public QuickObjectInfo TableInfo => this._tableInfo;

  internal DataSet OriginalDataSet => this._dataSet;

  private void RecalcLinkValues()
  {
  }

  private void CalcRecordRefColumnsData()
  {
    if (this._recordRefColumns.Count == 0)
      return;
    this._recordRefMap.Clear();
    List<string> state = new List<string>(this._dataTable.Rows.Count * this._recordRefColumns.Count);
    foreach (DataGridViewColumn recordRefColumn in this._recordRefColumns)
    {
      int columnIndex = this._proxyTable.Columns.IndexOf(recordRefColumn.DataPropertyName);
      if (columnIndex != -1)
      {
        foreach (DataRow row in (InternalDataCollectionBase) this._proxyTable.Rows)
        {
          string str = Convert.ToString(row[columnIndex]);
          if (str.Length > 2 && str[0] == 'I' && str[1] == 'K' && !state.Contains(str))
            state.Add(str);
        }
      }
    }
    state.Sort();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.MainRecordsThreadProc), (object) state);
  }

  private void CalcObjectRefColumnsData(QuickObjectInfo curUser)
  {
    if (this._objectRefColumns.Count == 0)
      return;
    this._objectRefMap.Clear();
    this._objectRefMap.Add(curUser.VersionGuid.ToString(), curUser.Caption);
    List<string> state = new List<string>(this._dataTable.Rows.Count * this._objectRefColumns.Count);
    foreach (DataGridViewColumn objectRefColumn in this._objectRefColumns)
    {
      string dataPropertyName = objectRefColumn.DataPropertyName;
      int columnIndex = this._proxyTable.Columns.IndexOf(dataPropertyName);
      if (columnIndex != -1)
      {
        foreach (DataRow row in (InternalDataCollectionBase) this._proxyTable.Rows)
        {
          if (row[columnIndex] is ValuesArray valuesArray)
          {
            foreach (object obj in valuesArray.GetArray())
            {
              if (obj != null)
              {
                string str = obj.ToString();
                if (!string.IsNullOrWhiteSpace(str) && !state.Contains(str))
                  state.Add(str);
              }
            }
          }
          else
          {
            string str = Convert.ToString(row[columnIndex]);
            if (str.Length > 0 && !state.Contains(str))
              state.Add(str);
          }
        }
        DataColumn column = this._proxyTable.Columns[dataPropertyName];
        if (column != null && this._proxyToDataColumns.ContainsKey((object) column))
        {
          object proxyToDataColumn = this._proxyToDataColumns[(object) column];
          if (proxyToDataColumn != null)
          {
            DataRow[] dataRowArray = this._attTable.Select($"{"F_ATTRIBUTE_GUID"}='{proxyToDataColumn}'");
            if (dataRowArray.Length != 0)
            {
              object obj = dataRowArray[0]["F_DEFAULT_VALUE"];
              if (obj != null && obj != DBNull.Value)
              {
                string str = obj.ToString();
                if (!state.Contains(str))
                  state.Add(str);
              }
            }
          }
        }
      }
    }
    state.Sort();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.MainObjectsThreadProc), (object) state);
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

  internal void RecordThreadProc(object stateInfo)
  {
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

  internal void ObjectThreadProc(object stateInfo)
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
          lock (this._recordRefMap)
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
    if (!this._disposed)
      this._grid.Invalidate();
    EventHandler invalidateGrid = this.InvalidateGrid;
    if (invalidateGrid == null)
      return;
    invalidateGrid((object) this, EventArgs.Empty);
  }

  private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
  {
    if (e.ColumnIndex == -1)
      return;
    DataGridViewColumn column1 = this._grid.Columns[e.ColumnIndex];
    if (this._protectedColumns.Contains(column1))
    {
      e.Value = (object) string.Empty;
      e.FormattingApplied = true;
    }
    else
    {
      if (this._recordRefColumns.Contains(column1))
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
      else if (this._objectRefColumns.Contains(column1))
      {
        string str = (string) null;
        object empty = e.Value;
        ValuesArray valuesArray = empty as ValuesArray;
        bool flag = false;
        if (valuesArray != null)
        {
          object[] array = valuesArray.GetArray();
          StringBuilder stringBuilder = new StringBuilder(128 /*0x80*/);
          int length = array.Length;
          for (int index = 0; index < length; ++index)
          {
            object obj = array[index];
            if (obj != null)
            {
              string key = obj.ToString();
              if (!string.IsNullOrWhiteSpace(key) && this._objectRefMap.TryGetValue(key, out key))
              {
                flag = true;
                if (stringBuilder.Length > 0)
                  stringBuilder.Append("; ");
                stringBuilder.Append(key);
              }
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
      if (column1.IsDataBound)
      {
        DataColumn column2 = this._proxyTable.Columns[column1.DataPropertyName];
        if (column2 != null && column2.ExtendedProperties.ContainsKey((object) "F_DISPLAY"))
        {
          object extendedProperty = column2.ExtendedProperties[(object) "F_DISPLAY"];
          if (extendedProperty != null)
          {
            e.Value = extendedProperty;
            e.FormattingApplied = true;
          }
        }
      }
      if (e.RowIndex == -1 || e.RowIndex >= this._proxyTable.Rows.Count)
        return;
      DataRow proxyRow = this.GetProxyRow(e.RowIndex);
      if (proxyRow == null)
        return;
      DataRow dataRow = this._dataTable.Rows.Find(proxyRow[this._keyColumnIndex]);
      DataColumn column3 = this._proxyTable.Columns[this._grid.Columns[e.ColumnIndex].Name];
      DataColumn proxyToDataColumn = this._proxyToDataColumns[(object) column3] as DataColumn;
      if (dataRow == null)
        return;
      switch (dataRow.RowState)
      {
        case DataRowState.Unchanged:
          if (proxyToDataColumn == null || object.Equals(dataRow[proxyToDataColumn], proxyRow[column3]))
            break;
          e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Italic);
          break;
        case DataRowState.Modified:
          if (proxyToDataColumn == null || dataRow[proxyToDataColumn, DataRowVersion.Current].Equals(dataRow[proxyToDataColumn, DataRowVersion.Original]))
            break;
          e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
          break;
      }
    }
  }

  private void Grid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Imbase.Client_73"), LocalizationHolder.rm.GetString("Imbase.Client_74"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      e.Cancel = true;
    else
      e.Cancel = false;
  }

  private void Grid_EditingControlShowing(
    object sender,
    DataGridViewEditingControlShowingEventArgs e)
  {
    if (!(e.Control is TextBox control))
      return;
    control.AutoCompleteMode = AutoCompleteMode.Suggest;
    control.AutoCompleteCustomSource = this.GetAutocompleteData(this._grid.CurrentCell.ColumnIndex);
    control.AutoCompleteSource = AutoCompleteSource.CustomSource;
  }

  private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
  {
    if (this._grid.CurrentCell != null && this._grid.CurrentCell.IsInEditMode)
      e.ThrowException = false;
    else if (this._grid.Focused)
    {
      if (e.Exception.Message.Contains("ComboBox"))
      {
        if (sender is DataGridView dataGridView)
        {
          object obj = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? (object) "(null)";
          dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1134"), (object) obj.ToString(), (object) dataGridView.Columns[e.ColumnIndex].HeaderText);
        }
        e.ThrowException = false;
      }
      else
      {
        int num = (int) MessageBox.Show(e.Exception.Message);
      }
    }
    else
      e.ThrowException = false;
  }

  private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
  {
    if (e.RowIndex == -1)
      return;
    this._grid.InvalidateRow(e.RowIndex);
    if (this._groupChanging || this._grid.Columns[e.ColumnIndex].ReadOnly || this._bookmarks.Count <= 0 || !this._bookmarks.Contains(this.GetRowKey(e.RowIndex)))
      return;
    this.DoGroupChanging(this._grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, e.ColumnIndex);
  }

  private void DoGroupChanging(object value, int cellIndex)
  {
    if (this._groupChanging || this._bookmarks.Count == 0)
      return;
    if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1137"), (object) this._grid.Columns[cellIndex].HeaderText), LocalizationHolder.rm.GetString("Imbase.Client_1138"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    try
    {
      this._groupChanging = true;
      DataGridViewRowCollection rows = this._grid.Rows;
      List<DataRow> dataRowList = new List<DataRow>();
      int count1 = rows.Count;
      for (int index = 0; index < count1; ++index)
      {
        DataGridViewRow dataGridViewRow = rows[index];
        if (this._bookmarks.Contains(Convert.ToInt64(dataGridViewRow.Cells[this._keyColumnIndex].Value)) && !object.Equals(value, dataGridViewRow.Cells[cellIndex].Value))
        {
          DataRow proxyRow = this.GetProxyRow(dataGridViewRow.Index);
          dataRowList.Add(proxyRow);
        }
      }
      int count2 = dataRowList.Count;
      DataColumn proxyColumn = this.GetProxyColumn(cellIndex);
      for (int index = 0; index < count2; ++index)
        dataRowList[index][proxyColumn] = value;
    }
    finally
    {
      this._groupChanging = false;
    }
  }

  private void Grid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
  {
    e.PaintParts &= ~DataGridViewPaintParts.Focus;
    Color color = this._grid.DefaultCellStyle.BackColor;
    bool flag1 = false;
    if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
    {
      color = this._grid.DefaultCellStyle.SelectionBackColor;
      flag1 = true;
    }
    if (e.RowIndex != -1 && e.RowIndex < this._proxyTable.Rows.Count)
    {
      DataRow realRow = this.GetRealRow(e.RowIndex);
      if (realRow != null)
      {
        bool flag2 = this._bookmarks.Contains(Convert.ToInt64(realRow["F_KEY"]));
        switch (realRow.RowState)
        {
          case DataRowState.Unchanged:
            color = flag2 ? (flag1 ? Color.FromArgb(102, 153, 204) : Color.LightBlue) : color;
            break;
          case DataRowState.Added:
            color = flag2 ? Color.FromArgb(126, 200, 119) : Color.FromArgb(201, 236, 199);
            break;
          case DataRowState.Modified:
            color = flag2 ? Color.FromArgb(236, 146, 10) : Color.FromArgb(243, 189, 139);
            break;
        }
      }
    }
    Rectangle rect = new Rectangle(this._grid.RowHeadersWidth, e.RowBounds.Top, this._grid.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - this._grid.HorizontalScrollingOffset + 1, e.RowBounds.Height);
    using (SolidBrush solidBrush = new SolidBrush(color))
    {
      e.Graphics.FillRectangle((Brush) solidBrush, rect);
      DataGridViewCell currentCell = this._grid.CurrentCell;
      if (currentCell != null && e.RowIndex == currentCell.RowIndex)
      {
        Rectangle displayRectangle = this._grid.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, true);
        if (!displayRectangle.IsEmpty && displayRectangle.Width > 0 && displayRectangle.Height > 0)
        {
          solidBrush.Color = Color.DarkGray;
          e.Graphics.FillRectangle((Brush) solidBrush, displayRectangle);
        }
      }
      solidBrush.Color = Color.Silver;
      foreach (DataGridViewBand readOnlyColumn in this._readOnlyColumns)
      {
        Rectangle displayRectangle = this._grid.GetCellDisplayRectangle(readOnlyColumn.Index, e.RowIndex, true);
        if (!displayRectangle.IsEmpty && displayRectangle.Width > 0 && displayRectangle.Height > 0)
          e.Graphics.FillRectangle((Brush) solidBrush, displayRectangle);
      }
      foreach (DataGridViewBand protectedColumn in this._protectedColumns)
      {
        Rectangle displayRectangle = this._grid.GetCellDisplayRectangle(protectedColumn.Index, e.RowIndex, true);
        if (!displayRectangle.IsEmpty && displayRectangle.Width > 0 && displayRectangle.Height > 0)
          e.Graphics.FillRectangle((Brush) solidBrush, displayRectangle);
      }
    }
  }

  private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
  {
    if (e.ColumnIndex == -1 || e.RowIndex == -1)
      return;
    DataGridViewColumn column1 = this._grid.Columns[e.ColumnIndex];
    if (this._protectedColumns.Contains(column1) || this._readOnlyColumns.Contains(column1) || !object.Equals(column1.Tag, (object) sc_7788.ssp_imbase_7790()) || this._grid.ReadOnly)
      return;
    DataColumn column2 = this._proxyTable.Columns[column1.Name];
    int num = int.Parse(column2.ColumnName);
    int index1 = TableEditor.IndexOfAttProp(num, this._rowsAttProps);
    if (index1 == -1 || this._rowsAttProps[index1].Computed != ComputeValueModes.JITValue)
      return;
    string expression = TableEditor.RenameFormulaFields(this._rowsAttProps[index1].Formula, this._rowsAttProps, true);
    if (!ExpressionEditor.EditExpression(ref expression, this._rowsAttProps, num, new ParseEventHandler(this.ParserHandler)))
      return;
    string str = TableEditor.RenameFormulaFields(expression, this._rowsAttProps, false);
    using (Parser parser = new Parser())
    {
      parser.Context = (object) this._rowsAttProps;
      parser.AutoDetectVariables = true;
      parser.CreateVariable += new CreateVariableEventHandler(TableLoadHelper.Parser_CreateVariable);
      try
      {
        string text = str;
        if (string.IsNullOrEmpty(text))
          text = "''";
        CalculatedColumn calculatedColumn = new CalculatedColumn(parser.Parse(text), column2.ColumnName, this._proxyTable);
        int count = this._calcColumns.Count;
        for (int index2 = 0; index2 < count; ++index2)
        {
          if (this._calcColumns[index2]._columnIndex == column2.Ordinal)
          {
            int cycledColumnIndex = -1;
            this._calcColumns[index2] = calculatedColumn;
            this._calcColumns = new List<CalculatedColumn>((IEnumerable<CalculatedColumn>) CalculatedColumn.Sort(this._calcColumns.ToArray(), ref cycledColumnIndex));
            if (cycledColumnIndex != -1)
            {
              int index3 = TableEditor.IndexOfAttProp(int.Parse(this._proxyTable.Columns[cycledColumnIndex].ColumnName), this._rowsAttProps);
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_76"), (object) this._rowsAttProps[index3].Name));
            }
            this._rowsAttProps[index1].Formula = str;
            this._attTable.Rows.Find((object) this._rowsAttProps[index1].AttributeGuid)["F_FORMULA"] = (object) str;
            this.RecalcTable();
            break;
          }
        }
      }
      finally
      {
        parser.CreateVariable -= new CreateVariableEventHandler(TableLoadHelper.Parser_CreateVariable);
      }
    }
  }

  private void Grid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
  {
    DataColumn proxyColumn = this.GetProxyColumn(e.ColumnIndex);
    string str = e.Value.ToString();
    DataGridViewColumn column = this._grid.Columns[e.ColumnIndex];
    if (column is DataGridViewComboBoxColumn viewComboBoxColumn)
    {
      if (string.IsNullOrEmpty(str))
      {
        e.Value = (object) DBNull.Value;
        e.ParsingApplied = true;
        return;
      }
      if (proxyColumn.ExtendedProperties.Contains((object) "F_MEASURE") && viewComboBoxColumn.DataSource is DataTable dataSource)
      {
        DataRow[] dataRowArray = dataSource.Select($"[{viewComboBoxColumn.DisplayMember}]='{str}'");
        str = dataRowArray.Length != 0 ? Convert.ToString(dataRowArray[0][viewComboBoxColumn.ValueMember]) : string.Empty;
      }
    }
    if (proxyColumn.ExtendedProperties.Contains((object) "F_MEASURE"))
    {
      if (string.IsNullOrWhiteSpace(str) && column is DataGridViewComboBoxColumn)
      {
        e.Value = (object) DBNull.Value;
      }
      else
      {
        long int64 = Convert.ToInt64(proxyColumn.ExtendedProperties[(object) "F_MEASURE"]);
        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(int64);
        if (!string.IsNullOrEmpty(str))
        {
          MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(str, descriptor, true);
          if (measuredValue.MeasureID != int64)
            measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, int64);
          e.Value = (object) measuredValue.Value;
        }
        else
          e.Value = (object) null;
      }
      e.ParsingApplied = true;
    }
    else
    {
      if (!this._objectRefColumns.Contains(column) || string.IsNullOrEmpty(str))
        return;
      Guid guid = new Guid(str);
    }
  }

  private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
  {
    if (e.ColumnIndex == -1)
    {
      if (e.RowIndex == -1)
        this.OnInvertSelection((object) this, EventArgs.Empty);
      else if (e.RowIndex < this._proxyTable.Rows.Count)
      {
        long int64 = Convert.ToInt64(this.GetProxyRow(e.RowIndex)[this._keyColumnIndex]);
        bool flag = this._bookmarks.Contains(int64);
        Keys modifierKeys = Control.ModifierKeys;
        if ((modifierKeys & (Keys.Shift | Keys.Control)) == Keys.None)
        {
          this._bookmarks.Clear();
          if (!flag)
            this._bookmarks.Add(int64);
          this._grid.Invalidate();
        }
        else if ((modifierKeys & Keys.Control) == Keys.Control)
        {
          if (flag)
            this._bookmarks.Remove(int64);
          else
            this._bookmarks.Add(int64);
          this._grid.InvalidateRow(e.RowIndex);
        }
        else if ((modifierKeys & Keys.Shift) == Keys.Shift)
        {
          int rowIndex2 = 0;
          if (this._bookmarks.Count > 0)
            rowIndex2 = this.GetRowIndex(this._bookmarks[this._bookmarks.Count - 1]);
          this.SelectRange(e.RowIndex, rowIndex2);
          this._grid.Invalidate();
        }
      }
    }
    this.UpdateRecsIndicator();
  }

  private void Grid_CellValidated(object sender, DataGridViewCellEventArgs e)
  {
    if (!(this._grid.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn))
      return;
    this._grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
    this.CheckDepValue(e);
  }

  private void Grid_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
  {
    if (this._loading || this._isColsOrderChanged)
      return;
    this._isColsOrderChanged = true;
    this.Text = base.Text;
    this.UpdateButtons();
  }

  private void LabelWithButtonEditingControl_ButtonClick(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataGridViewTextBoxCell gridViewTextBoxCell = sender as DataGridViewTextBoxCell;
      if (sessionKeeper.Session.GetAttributeType(Convert.ToInt32(this._grid.Columns[gridViewTextBoxCell.ColumnIndex].Name)).AttributeID != Intermech.Imbase.Consts.ImbaseTemplateAttID)
        return;
      SymbolSelectChB_Ctrl symbolSelectChBCtrl = new SymbolSelectChB_Ctrl(this.GetTemplatesBody);
      symbolSelectChBCtrl.Filter = gridViewTextBoxCell.Value != null ? gridViewTextBoxCell.Value.ToString() : string.Empty;
      symbolSelectChBCtrl.Width = this._grid.CurrentCell.Size.Width + sc_7788.ssp_imbase_7791(1461699337);
      Rectangle displayRectangle = this._grid.GetCellDisplayRectangle(this._grid.CurrentCell.ColumnIndex, this._grid.CurrentCell.RowIndex, true);
      Rectangle screen = this._grid.RectangleToScreen(displayRectangle);
      int num = screen.X + displayRectangle.Width - symbolSelectChBCtrl.Width;
      int x = num > 0 ? num : 0;
      int bottom = screen.Bottom;
      int y = bottom + symbolSelectChBCtrl.Height < Screen.PrimaryScreen.WorkingArea.Height ? bottom : bottom - this._grid.CurrentCell.Size.Height - symbolSelectChBCtrl.Height;
      symbolSelectChBCtrl.Location = new Point(x, y);
      if (symbolSelectChBCtrl.ShowDialog() != DialogResult.OK)
        return;
      this._grid.CurrentCell.Value = (object) symbolSelectChBCtrl.Filter;
    }
  }

  private void ArrayColumn_ButtonClick(object sender, EventArgs e)
  {
    DataGridViewCell dataGridViewCell = sender as DataGridViewCell;
    Rectangle screen = this._grid.RectangleToScreen(this._grid.GetCellDisplayRectangle(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex, false));
    object obj = dataGridViewCell.Value;
    ArrayEditor arrayEditor = new ArrayEditor();
    arrayEditor.Left = screen.Left;
    arrayEditor.Top = screen.Top + screen.Height;
    DataColumn proxyColumn = this.GetProxyColumn(dataGridViewCell.ColumnIndex);
    DataGridViewColumn owningColumn = dataGridViewCell.OwningColumn;
    int result = -1;
    int.TryParse(owningColumn.Name, out result);
    int index = TableEditor.IndexOfAttProp(result, this._rowsAttProps);
    System.Type elementType = proxyColumn.ExtendedProperties[(object) "dataType"] as System.Type;
    if (elementType == (System.Type) null)
      elementType = AttributesTypeHelper.GetTypeOfAttributeValue(this._rowsAttProps[index].FieldType);
    if (!arrayEditor.EditArray(ref obj, elementType, this._rowsAttProps[index], proxyColumn.ExtendedProperties, proxyColumn.Caption, this))
      return;
    dataGridViewCell.Value = obj;
    dataGridViewCell.DataGridView.InvalidateCell(dataGridViewCell);
  }

  private void RecordReference_ButtonClick(object sender, EventArgs e)
  {
    DataGridViewTextBoxCell gridViewTextBoxCell = sender as DataGridViewTextBoxCell;
    if (!(ServicesManager.GetService(typeof (IImbaseSelector)) is ImbaseSelector service))
      return;
    object obj = gridViewTextBoxCell.Value;
    string empty = string.Empty;
    if (obj != null && obj != DBNull.Value)
      empty = obj.ToString();
    string key = service.SelectRecord(empty, true);
    if (string.IsNullOrEmpty(key))
      return;
    if (!this._recordRefMap.ContainsKey(key))
    {
      this.RecordThreadProc((object) new List<string>(1)
      {
        key
      });
      this._calcContext.SetRecordsMap(this._recordRefMap);
    }
    gridViewTextBoxCell.Value = (object) key;
  }

  private void ObjectReference_ButtonClick(object sender, EventArgs e)
  {
    if (!(sender is DataGridViewTextBoxCell gridViewTextBoxCell) || this._grid.Columns.Count <= gridViewTextBoxCell.ColumnIndex)
      return;
    DataGridViewColumn column = this._grid.Columns[gridViewTextBoxCell.ColumnIndex];
    long objectID;
    if (this._describers.ContainsKey(column.Name))
    {
      objectID = this.FromDescriptor(column.Name, gridViewTextBoxCell.Value);
    }
    else
    {
      if (column.Tag == null)
        return;
      int result1 = -1;
      if (!int.TryParse(column.Tag.ToString(), out result1))
        return;
      if (result1 != -1 && result1 != 0)
        Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(result1, true), true);
      ImbaseFilterEditor imbaseFilterEditor = (ImbaseFilterEditor) null;
      IImbaseFilterSelector service = ServicesManager.GetService(typeof (IImbaseFilterSelector)) as IImbaseFilterSelector;
      long num = 0;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (service != null)
        {
          string str = Convert.ToString(gridViewTextBoxCell.Value);
          if (!string.IsNullOrEmpty(str) && GuidHelper.IsGuid(str))
            num = sessionKeeper.Session.GetObjectInfo(new Guid(str)).ObjectID;
          imbaseFilterEditor = AttributeValuesEditor.ImbaseAttributesHandle(sessionKeeper.Session, Convert.ToInt32(column.Name), Intermech.Imbase.Consts.ImbaseTableRecordTypeID, num);
        }
      }
      if (imbaseFilterEditor != null)
      {
        ObjectPropertyClass objectPropertyClass = (ObjectPropertyClass) imbaseFilterEditor.EditValue(service, Convert.ToInt32(column.Name), num);
        objectID = objectPropertyClass != null ? objectPropertyClass.ObjectID : 0L;
      }
      else
      {
        int result2 = 0;
        int.TryParse(column.Name, out result2);
        ArrayList arrayList = new ArrayList();
        if (result1 != 0)
          arrayList.Add((object) result1);
        else
          arrayList = ObjectEditor.GetObjTypeListByAttrId(result2);
        if (arrayList.Count > 1 && arrayList.IndexOf((object) 0) != -1)
          arrayList.Remove((object) 0);
        IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects((int[]) arrayList.ToArray(typeof (int)), new long[1]
        {
          num
        }, false, true);
        if (dbObjectIdArray == null || dbObjectIdArray.Length == 0)
          return;
        objectID = dbObjectIdArray[0].Value;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (objectID < 0L)
      {
        int num = (int) MessageBox.Show("Нельзя использовать заготовку или взятый на редактирование объект. Сначала завершите редактирование.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID, false);
        if (objectActualCopy == null)
          return;
        string key = objectActualCopy.ObjectGUID.ToString();
        if (!this._objectRefMap.ContainsKey(key))
          this._objectRefMap.Add(key, objectActualCopy.Caption);
        gridViewTextBoxCell.Value = (object) key;
      }
    }
  }

  private long FromDescriptor(string colName, object oldValue)
  {
    long result = 0;
    int int32 = Convert.ToInt32(colName);
    IAttributePropertyDescriber describer = this._describers[colName];
    if (describer.GetPropDescriptorEditor(int32) is UITypeEditor descriptorEditor)
    {
      switch (descriptorEditor.GetEditStyle())
      {
        case UITypeEditorEditStyle.Modal:
        case UITypeEditorEditStyle.DropDown:
          string str = Convert.ToString(oldValue);
          if (GuidHelper.IsGuid(str))
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              result = sessionKeeper.Session.GetObjectInfo(new Guid(str)).ObjectID;
          }
          else if (!long.TryParse(str, out result))
            result = 0L;
          using (ServiceContainer provider = new ServiceContainer())
          {
            object propertyValue = descriptorEditor.EditValue((System.IServiceProvider) provider, (object) result);
            result = 0L;
            if (propertyValue != null)
            {
              if (propertyValue != DBNull.Value)
              {
                object attributeValue = describer.GetAttributeValue((IElementInfo) null, int32, propertyValue);
                if (attributeValue != null)
                {
                  if (long.TryParse(Convert.ToString(attributeValue), out result))
                    break;
                }
                result = 0L;
                break;
              }
              break;
            }
            break;
          }
      }
    }
    return result;
  }

  private void MemoColumn_ButtonClick(object sender, EventArgs e)
  {
    if (!(sender is DataGridViewTextBoxCell gridViewTextBoxCell) || this._grid.Columns.Count <= gridViewTextBoxCell.ColumnIndex)
      return;
    string empty = string.Empty;
    object obj = gridViewTextBoxCell.Value;
    if (obj != null)
      empty = obj.ToString();
    using (MemoForm memoForm = new MemoForm())
    {
      memoForm.Memo = empty;
      if (memoForm.ShowDialog() != DialogResult.OK)
        return;
      gridViewTextBoxCell.Value = (object) memoForm.Memo;
    }
  }

  private int GetRowIndex(long key)
  {
    DataGridViewRowCollection rows = this._grid.Rows;
    int count = rows.Count;
    for (int index = 0; index < count; ++index)
    {
      if (Convert.ToInt64(rows[index].Cells[this._keyColumnIndex].Value).Equals(key))
        return index;
    }
    return -1;
  }

  private long GetRowKey(int rowIndex)
  {
    return rowIndex < 0 || rowIndex > this._grid.Rows.Count ? -1L : Convert.ToInt64(this._grid.Rows[rowIndex].Cells[this._keyColumnIndex].Value);
  }

  private void SelectRange(int rowIndex1, int rowIndex2)
  {
    DataGridViewRowCollection rows = this._grid.Rows;
    int num1 = rowIndex1;
    int num2 = rowIndex2;
    if (num1 > num2)
    {
      num1 = rowIndex2;
      num2 = rowIndex1;
    }
    for (int index = num1; index <= num2; ++index)
      this.SafeAddKey(this._bookmarks, Convert.ToInt64(rows[index].Cells[this._keyColumnIndex].Value));
  }

  private void ParserHandler(object sender, ParseEventArgs e)
  {
    VariableValuesCollection usedVariables = e.Tree.UsedVariables;
    int count = usedVariables.Count;
    DataRow dataRow = this._proxyTable.Rows.Find(this._grid.CurrentRow.Cells[this._keyColumnIndex].Value);
    for (int index1 = 0; index1 < count; ++index1)
    {
      VariableValue variableValue = usedVariables[index1];
      int index2 = TableEditor.IndexOfAttProp(variableValue.Name, this._rowsAttProps);
      if (index2 != -1)
      {
        DataColumn column = this._proxyTable.Columns[this._rowsAttProps[index2].AttributeID.ToString()];
        if (column != null)
        {
          int index3 = this._proxyTable.Columns.IndexOf(column);
          object obj = dataRow[column];
          bool flag = false;
          if (!DBNull.Value.Equals(obj))
          {
            if (this._namedValuesData != null && this._namedValuesData[index3] != null && this._namedValues != null && this._namedValues[index3] != null && obj != null)
            {
              int index4 = this._namedValuesData[index3].PossibleValues.IndexOf(obj);
              if (index4 != -1)
              {
                string name = Convert.ToString(this._namedValuesData[index3].PossibleValuesDescriptions[index4]);
                if (!string.IsNullOrEmpty(name))
                {
                  obj = (object) this._namedValues[index3].SetData(name, obj);
                  flag = true;
                }
              }
            }
            if (!flag && this._calcContext != null && this._calcContext.IsMapped(index3))
              obj = (object) this._calcContext.GetMapValue(dataRow[index3].ToString());
          }
          variableValue.Value = obj;
        }
      }
    }
    e.Result = e.Tree.Evaluate(usedVariables);
  }

  private DataRowState GetRowState(int gridRowIndex)
  {
    int num = this._proxyTable.Rows.Count - this._proxyTable.Select(string.Empty, string.Empty, DataViewRowState.Deleted).Length;
    if (gridRowIndex != -1 && gridRowIndex < num)
    {
      DataRow dataRow = this._dataTable.Rows.Find(this.GetProxyRow(gridRowIndex)[this._keyColumnIndex]);
      if (dataRow != null)
        return dataRow.RowState;
    }
    return DataRowState.Unchanged;
  }

  private DataColumn GetProxyColumn(int gridColumnIndex)
  {
    return this._proxyTable.Columns[this._grid.Columns[gridColumnIndex].Name];
  }

  private DataRow GetRealRow(int gridRowIndex)
  {
    return this._dataTable.Rows.Find(this._grid.Rows[gridRowIndex].Cells[this._keyColumnIndex].Value);
  }

  private DataRow GetProxyRow(int gridRowIndex)
  {
    return this._keyColumnIndex == -1 ? (DataRow) null : this._proxyTable.Rows.Find(this._grid.Rows[gridRowIndex].Cells[this._keyColumnIndex].Value);
  }

  private AutoCompleteStringCollection GetAutocompleteData(int columnIndex)
  {
    AutoCompleteStringCollection autocompleteData = (AutoCompleteStringCollection) null;
    DataColumn column = this._proxyTable.Columns[this._grid.Columns[columnIndex].Name];
    if (column != null)
    {
      ArrayList arrayList = new ArrayList();
      DataRowCollection rows = this._proxyTable.Rows;
      int count = rows.Count;
      for (int index = 0; index < count; ++index)
      {
        DataRow dataRow = rows[index];
        if (dataRow.RowState != DataRowState.Detached && dataRow.RowState != DataRowState.Deleted)
        {
          string str = rows[index][column].ToString();
          if (str.Length > 0 && !arrayList.Contains((object) str))
            arrayList.Add((object) str);
        }
      }
      arrayList.Sort((IComparer) StringComparer.InvariantCulture);
      autocompleteData = new AutoCompleteStringCollection();
      autocompleteData.AddRange((string[]) arrayList.ToArray(typeof (string)));
    }
    return autocompleteData;
  }

  private void RecalcTable()
  {
    if (this._calcColumns == null)
      return;
    int count = this._calcColumns.Count;
    foreach (DataRow dataRow in this._proxyTable.Select())
      dataRow.RowError = string.Empty;
    this._calculating = true;
    try
    {
      for (int index = 0; index < count; ++index)
        this._calcColumns[index].Calculate(this._proxyTable, this._calcContext, this._namedValuesData, this._namedValues);
    }
    finally
    {
      this._calculating = false;
    }
    this._grid.Invalidate();
  }

  internal static string RenameFormulaFields(
    string formula,
    AttributeTypeProperties[] atp,
    bool toLongName)
  {
    int length = atp.Length;
    string str = formula;
    for (int index = 0; index < length; ++index)
      str = !toLongName ? str.Replace($"[{atp[index].Name}]", $"[{atp[index].AttributeGuid.ToString()}]") : str.Replace($"[{atp[index].AttributeGuid.ToString()}]", $"[{atp[index].Name}]");
    return str;
  }

  private void contextMenuBarItem1_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (!this.contextMenuBarItem1.Enabled)
      throw new AbortException();
    this.UpdateButtons();
  }

  private void UpdateButtons()
  {
    this.btCheckOut.Visible = this.mnCheckOut.Visible = this._checkOutMode == CheckOutMode.None && this._checkoutNeed;
    this.btCheckIn.Visible = this.mnCheckIn.Visible = this._checkOutMode == CheckOutMode.CheckedOut && this._checkoutNeed;
    this.btSaveChanges.Enabled = this.mnSaveChanges.Enabled = this.IsDirty();
    this.btCancelChanges.Enabled = this.mnCancelChanges.Enabled = this.mnSaveChanges.Enabled || this._checkOutMode == CheckOutMode.CheckedOut;
    this.OnClipboardContextChanged((object) this._clipboard, EventArgs.Empty);
    bool flag = this._grid.CurrentRow == null || this._grid.ReadOnly;
    this.mnDeleteRecord.Enabled = !flag;
    this.mnCopyRecord.Enabled = !flag && this.mnNewRecord.Enabled;
    this.mnObjectProps.Enabled = this._usedKeys.Contains(this.RecordId);
    this.miNormaCS.Visible = this.tbNormaCS.Visible = ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, false) != null;
    this.btAddRecOwner.Visible = this._recOwnerColumn == null;
    this.btAddRecDate.Visible = this._recModDateColumn == null;
    this.mnReplace.Enabled = this.CanChangeDataInColumn();
  }

  private bool CanChangeDataInColumn()
  {
    if (this._grid.CurrentCell == null)
      return false;
    int columnIndex = this._grid.CurrentCell.ColumnIndex;
    if (columnIndex < 0)
      return false;
    DataGridViewColumn column = this._grid.Columns[columnIndex];
    return column != null && !column.ReadOnly && !object.Equals(column.Tag, (object) "expression");
  }

  private bool IsDirty()
  {
    if (this._dataSet == null)
      return false;
    return this._dataSet.HasChanges() || this._isColsOrderChanged || this._filterChanged;
  }

  private void CancelChanges()
  {
    this.CancelCheckOut();
    this.CancelDataChanges();
    this.RepaintCaption();
    this._grid.Invalidate();
  }

  private void CancelDataChanges()
  {
    this._attTable.RejectChanges();
    this._dataSet.RejectChanges();
    this.ReloadTables();
    this.RepaintCaption();
    this._grid.Invalidate();
  }

  private void ReloadTables() => this.LoadData();

  private bool CheckUniqueIndexes()
  {
    bool flag = true;
    DataTable data = (DataTable) null;
    List<long> keys = (List<long>) null;
    List<int> uIndexes = (List<int>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseIndexingService customService = sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
      try
      {
        if (customService == null)
          throw new Exception(LocalizationHolder.rm.GetString("Imbase_ImpossibleCheckUniqueIndexes"));
        data = customService.CheckUniqueBeforeTableDataChange(sessionKeeper.Session.SessionGUID, this._tableId, this._dataSet.Tables["IMS_ATTR_TYPES"], this._dataSet.Tables["IMS_DATA"], out uIndexes, out keys);
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
        flag = false;
      }
    }
    string caption = LocalizationHolder.rm.GetString("Imbase_SaveChanges");
    return flag && !this.NeedShowNotUniqueIndexes(data, uIndexes, keys, caption, MessageBoxIcon.Question);
  }

  private bool NeedShowNotUniqueIndexes(
    DataTable data,
    List<int> indexes,
    List<long> rowNums,
    string caption,
    MessageBoxIcon icon)
  {
    bool flag = false;
    string text = string.Empty;
    if (rowNums != null && rowNums.Count > 0)
    {
      this._bookmarks = rowNums;
      this.UpdateRecsIndicator();
    }
    if (indexes != null)
    {
      if (data != null)
      {
        this._bookmarks = rowNums;
        this.UpdateRecsIndicator();
        if (indexes.Count == 1)
        {
          AttributeTypeProperties attributeTypeProperties = ((IEnumerable<AttributeTypeProperties>) this._rowsAttProps).FirstOrDefault<AttributeTypeProperties>((System.Func<AttributeTypeProperties, bool>) (prop => prop.AttributeID == indexes[0]));
          text = string.Format(LocalizationHolder.rm.GetString("Imbase_SaveNotUniqueData_ErrMsg_Single"), (object) attributeTypeProperties.Name);
        }
        else
        {
          string[] array = ((IEnumerable<AttributeTypeProperties>) this._rowsAttProps).Where<AttributeTypeProperties>((System.Func<AttributeTypeProperties, bool>) (x => indexes.Contains(x.AttributeID))).Select<AttributeTypeProperties, string>((System.Func<AttributeTypeProperties, string>) (x => x.Name)).ToArray<string>();
          text = string.Format(LocalizationHolder.rm.GetString("Imbase_SaveNotUniqueData_ErrMsg_Multi"), (object) string.Join("\n", array));
        }
      }
      else if (rowNums != null && rowNums.Count > 0)
      {
        if (indexes.Count == 1)
        {
          AttributeTypeProperties attributeTypeProperties = ((IEnumerable<AttributeTypeProperties>) this._rowsAttProps).FirstOrDefault<AttributeTypeProperties>((System.Func<AttributeTypeProperties, bool>) (prop => prop.AttributeID == indexes[0]));
          text = string.Format(LocalizationHolder.rm.GetString("Imbase_SaveNotUniqueDataInTable_ErrMsg_Single"), (object) attributeTypeProperties.Name);
        }
        else
        {
          string str = $"'{string.Join("', '", ((IEnumerable<AttributeTypeProperties>) this._rowsAttProps).Where<AttributeTypeProperties>((System.Func<AttributeTypeProperties, bool>) (x => indexes.Contains(x.AttributeID))).Select<AttributeTypeProperties, string>((System.Func<AttributeTypeProperties, string>) (x => x.Name)).ToArray<string>())}'";
          text = string.Format(LocalizationHolder.rm.GetString("Imbase_SaveNotUniqueDataInTable_ErrMsg_Multi"), (object) str);
        }
      }
    }
    this._grid.Invalidate();
    if (!string.IsNullOrEmpty(text))
    {
      if (data != null)
      {
        if (MessageBox.Show(text, caption, MessageBoxButtons.YesNo, icon) == DialogResult.Yes)
          UniqueIndexesView.Show(indexes, data);
      }
      else
      {
        int num = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("Imbase_SaveChanges"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      flag = true;
    }
    return flag;
  }

  private List<int> GetAddedAttrIDs(DataTable originalDT, DataTable changedDT)
  {
    List<object> originalList = originalDT.AsEnumerable().Select<DataRow, object>((System.Func<DataRow, object>) (x => x["F_ATTRIBUTE_GUID"])).ToList<object>();
    return changedDT.AsEnumerable().Select<DataRow, object>((System.Func<DataRow, object>) (x => x["F_ATTRIBUTE_GUID"])).ToList<object>().Where<object>((System.Func<object, bool>) (x => !originalList.Contains(x))).Select<object, int>((System.Func<object, int>) (x => MetaDataHelper.GetAttributeTypeID(Convert.ToString(x)))).ToList<int>();
  }

  private List<int> GetDeletedAttrIDs(DataTable originalDT, DataTable changedDT)
  {
    List<object> list = originalDT.AsEnumerable().Select<DataRow, object>((System.Func<DataRow, object>) (x => x["F_ATTRIBUTE_GUID"])).ToList<object>();
    List<object> changedList = changedDT.AsEnumerable().Select<DataRow, object>((System.Func<DataRow, object>) (x => x["F_ATTRIBUTE_GUID"])).ToList<object>();
    System.Func<object, bool> predicate = (System.Func<object, bool>) (x => !changedList.Contains(x));
    return list.Where<object>(predicate).Select<object, int>((System.Func<object, int>) (x => MetaDataHelper.GetAttributeTypeID(Convert.ToString(x)))).ToList<int>();
  }

  private List<long> GetDeletedRowNums(DataTable originalDT, DataTable changedDT)
  {
    List<long> list = originalDT.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["F_KEY"]))).ToList<long>();
    List<long> changedList = changedDT.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => x.RowState != DataRowState.Deleted)).Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["F_KEY"]))).ToList<long>();
    Predicate<long> match = (Predicate<long>) (x => !changedList.Contains(x));
    return list.FindAll(match);
  }

  private bool SaveChanges(bool checkIn)
  {
    bool flag1 = false;
    if (this._proxyTable.HasErrors)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase.Client_1139"), LocalizationHolder.rm.GetString("Imbase.Client_1140"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return flag1;
    }
    for (int columnIndex = 0; columnIndex < this._grid.Columns.Count; ++columnIndex)
    {
      for (int rowIndex = 0; rowIndex < this._grid.RowCount; ++rowIndex)
      {
        DataGridViewCell dataGridViewCell = this._grid[columnIndex, rowIndex];
        if (dataGridViewCell != null && !string.IsNullOrEmpty(dataGridViewCell.ErrorText))
        {
          int num = (int) MessageBox.Show("Некоторые поля содержат ошибки в данных. Сохранение невозможно.", LocalizationHolder.rm.GetString("Imbase.Client_1140"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this._grid.CurrentCell = dataGridViewCell;
          return flag1;
        }
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._tableId, false);
      if (objectActualCopy == null && this._tableId < 0L)
      {
        this._tableId = Math.Abs(this._tableId);
        if (this._checkoutNeed)
        {
          this._checkOutMode = CheckOutMode.None;
          this.CheckOut();
          objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._tableId, false);
        }
      }
      bool flag2 = objectActualCopy.ObjectModifyMode == ObjectModifyModes.InBase;
      if (!flag2 || !this.CheckUniqueIndexes())
      {
        if (flag2)
          goto label_32;
      }
      DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, this._tableId, true);
      List<long> longList = (List<long>) null;
      List<int> intList = (List<int>) null;
      if (tables != null)
      {
        longList = this.GetDeletedRowNums(tables.Tables["IMS_DATA"], this._dataTable);
        intList = this.GetDeletedAttrIDs(tables.Tables["IMS_ATTR_TYPES"], this._attTable);
      }
      flag1 = this.SaveTableData(sessionKeeper.Session);
      if (flag1)
      {
        checkIn = checkIn && this.CheckIn(sessionKeeper.Session);
        TableEditor.OnTableChanged(this, new ImbaseTableChangedEventArgs(this._linkId, this._tableId));
        if (this._needUpdateObjects.Count > 0 && this._checkOutMode == CheckOutMode.None)
          UpdateCreatedObjects.Show(this._linkId, this._needUpdateObjects);
        this._needUpdateObjects.Clear();
        if (flag2)
        {
          IndexesHelper helper = new IndexesHelper(this._tableId)
          {
            Actions = IndexesStatus.UpdateTableData,
            DeletedRowNums = longList,
            DeletedColumns = intList
          };
          ((IBackgroundTaskView) ServicesManager.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) new ImbaseIndexesBackgroundTask(helper));
        }
        this.SaveFilter(sessionKeeper.Session);
        if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
        {
          if (checkIn)
          {
            long objectID = this._linkId == -1L || this._linkId == 0L ? this._tableId : this._linkId;
            service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", objectID));
          }
          else
            service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this._tableId));
        }
        this.RepaintCaption();
      }
    }
label_32:
    this._grid.Invalidate();
    return flag1;
  }

  private void SaveFilter(IUserSession session)
  {
    if (!this._filterChanged || this._linkId == -1L)
      return;
    IDBObject dbObject = session.GetObject(this._linkId);
    IDBAttribute dbAttribute = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ManualTableFilterId);
    if (this._filter.Count > 0)
    {
      if (dbAttribute == null)
        dbAttribute = dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ManualTableFilterId, false);
      int count = this._filter.Count;
      using (MemoryStream memoryStream = new MemoryStream(count * 8))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          for (int index = 0; index < count; ++index)
            binaryWriter.Write(this._filter[index]);
          using (MemoryStream outStream = new MemoryStream(Convert.ToInt32(memoryStream.Length / 2L)))
          {
            ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) memoryStream, Convert.ToInt32((object) ZLibCompressLevels.LevelMax));
            IBlobWriter blobWriter = dbAttribute as IBlobWriter;
            blobWriter.OpenBlob(new BlobInformation(memoryStream.Length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, "filter"), false);
            blobWriter.WriteDataBlock(outStream.ToArray());
          }
        }
      }
    }
    else
      dbAttribute?.Delete(0L);
    this.FilterChanged = false;
  }

  private void LoadManualFilter(IDBAttribute att)
  {
    if (att == null || !(att is IBlobReader blobReader))
      return;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      try
      {
        if (blobInformation.RealFileSize <= 0L)
          return;
        byte[] buffer = blobReader.ReadDataBlock(0);
        if (buffer == null)
          return;
        using (MemoryStream inStream = new MemoryStream(buffer))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
            memoryStream.Position = 0L;
            using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream))
            {
              int num = (int) memoryStream.Length / 8;
              for (int index = 0; index < num; ++index)
                this._filter.Add(binaryReader.ReadInt64());
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

  private bool SaveTableData(IUserSession session)
  {
    this.UpdateDataSet();
    this._proxyTable.AcceptChanges();
    this._attTable.AcceptChanges();
    this._dataTable.AcceptChanges();
    TableLoadHelper.StoreData(session, this._tableId, this._dataSet, session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
    return true;
  }

  private void RepaintCaption() => base.Text = this.Text;

  private bool SafeAddKey(List<long> list, long value)
  {
    if (list.Contains(value))
      return false;
    list.Add(value);
    return true;
  }

  internal static bool SameRows(object[] data1, object[] data2)
  {
    int length = data1.Length;
    for (int index = 2; index < length; ++index)
    {
      if (!object.Equals(data1[index], data2[index]))
        return false;
    }
    return true;
  }

  private void OnCheckOut(object sender, EventArgs e)
  {
    this.CheckOut();
    this._notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new List<long>()
    {
      Math.Abs(this._tableId)
    }, (IList<long>) new List<long>() { this._tableId }));
    this.UpdateButtons();
  }

  private void OnCheckIn(object sender, EventArgs e)
  {
    this.SaveChanges(true);
    this.UpdateButtons();
  }

  private void OnSaveChanges(object sender, EventArgs e)
  {
    this.SaveChanges(false);
    this._isColsOrderChanged = false;
    this.UpdateButtons();
  }

  private void OnCancelChanges(object sender, EventArgs e)
  {
    this.CancelChanges();
    this._isColsOrderChanged = false;
    this.UpdateButtons();
  }

  private void OnSelectByCondition(object sender, EventArgs e)
  {
    string empty = string.Empty;
    List<int> substColumns = new List<int>(16 /*0x10*/);
    bool addSelection;
    if (!FilterBuilder.BuildConditionString(this._rowsAttProps, ref empty, out addSelection, substColumns))
      return;
    DataRow[] dataRowArray;
    if (substColumns.Count > 0)
    {
      DataTable stbstTable = TableLoadHelper.CreateStbstTable(this._proxyTable, ref empty, substColumns, this._rowsAttProps);
      dataRowArray = stbstTable.Select(empty, string.Empty, DataViewRowState.CurrentRows);
      stbstTable.Dispose();
    }
    else
      dataRowArray = this._proxyTable.Select(empty, string.Empty, DataViewRowState.CurrentRows);
    int length = dataRowArray.Length;
    if (length > 0)
    {
      if (!addSelection)
        this._bookmarks.Clear();
      for (int index = 0; index < length; ++index)
        this.SafeAddKey(this._bookmarks, Convert.ToInt64(dataRowArray[index][this._keyColumnIndex]));
      this._grid.Invalidate();
      int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1141"), (object) length));
    }
    else
    {
      int num1 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1142"), (object) length));
    }
    this.UpdateRecsIndicator();
  }

  private void OnSelectSameRecords(object sender, EventArgs e)
  {
    List<long> list = new List<long>(32 /*0x20*/);
    DataRow[] dataRowArray = this._proxyTable.Select();
    int length = dataRowArray.Length;
    for (int index1 = 0; index1 < length; ++index1)
    {
      object[] itemArray = dataRowArray[index1].ItemArray;
      for (int index2 = index1 + 1; index2 < length; ++index2)
      {
        if (TableEditor.SameRows(itemArray, dataRowArray[index2].ItemArray))
        {
          this.SafeAddKey(list, Convert.ToInt64(itemArray[this._keyColumnIndex]));
          this.SafeAddKey(list, Convert.ToInt64(dataRowArray[index2][this._keyColumnIndex]));
        }
      }
    }
    if (list.Count > 0)
    {
      this._bookmarks = list;
      this._grid.Invalidate();
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase.Client_77"), LocalizationHolder.rm.GetString("Imbase.Client_78"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase.Client_79"), LocalizationHolder.rm.GetString("Imbase.Client_80"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    this.UpdateRecsIndicator();
  }

  private void OnSelectRecord(object sender, EventArgs e)
  {
    DataGridViewRow currentRow = this._grid.CurrentRow;
    if (currentRow == null)
      return;
    DataRow dataRow = this._proxyTable.Rows.Find(currentRow.Cells[this._keyColumnIndex].Value);
    if (dataRow == null || !this.SafeAddKey(this._bookmarks, Convert.ToInt64(dataRow[this._keyColumnIndex])))
      return;
    this._grid.InvalidateRow(currentRow.Index);
  }

  private void OnClearSelection(object sender, EventArgs e)
  {
    this._bookmarks.Clear();
    this._grid.Invalidate();
    this.UpdateRecsIndicator();
  }

  private void OnInvertSelection(object sender, EventArgs e)
  {
    DataRow[] dataRowArray = this._proxyTable.Select();
    int length = dataRowArray.Length;
    List<long> longList = new List<long>(length);
    for (int index = 0; index < length; ++index)
    {
      long int64 = Convert.ToInt64(dataRowArray[index][this._keyColumnIndex]);
      if (!this._bookmarks.Contains(int64))
        longList.Add(int64);
    }
    this._bookmarks = longList;
    this._grid.Invalidate();
    this.UpdateRecsIndicator();
  }

  private void OnDeleteSelected(object sender, EventArgs e)
  {
    int count = this._bookmarks.Count;
    if (count <= 0)
      return;
    List<long> longList = new List<long>((IEnumerable<long>) this._bookmarks);
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Imbase.Client_81"), LocalizationHolder.rm.GetString("Imbase.Client_82"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    for (int index = 0; index < count; ++index)
      this._proxyTable.Rows.Find((object) longList[index])?.Delete();
  }

  private void OnCreateTable(object sender, EventArgs e)
  {
    DataSet copyDS = new DataSet("IMS_TABLE_RECORDS");
    DataTable table1 = this._attTable.Copy();
    table1.TableName = "IMS_ATTR_TYPES";
    table1.AcceptChanges();
    DataTable table2 = this._dataTable.Clone();
    table2.TableName = "IMS_DATA";
    if (this._bookmarks.Count == 0)
    {
      int num1 = (int) MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Imbase_CreateCopyTable_NotSelectedRowsMessage_Message"), LocalizationHolder.rm.GetString("Imbase_CreateCopyTable_NotSelectedRowsMessage_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      int count = this._bookmarks.Count;
      DataTable dataTable = this._proxyTable.Clone();
      for (int index = 0; index < count; ++index)
      {
        DataRow dataRow1 = this._dataTable.Rows.Find((object) this._bookmarks[index]);
        table2.Rows.Add(dataRow1.ItemArray);
        DataRow dataRow2 = this._proxyTable.Rows.Find((object) this._bookmarks[index]);
        dataTable.Rows.Add(dataRow2.ItemArray);
      }
      dataTable.AcceptChanges();
      table2.AcceptChanges();
      copyDS.Tables.Add(table1);
      copyDS.Tables.Add(table2);
      using (CreateCopyTableDialog createCopyTableDialog = new CreateCopyTableDialog(copyDS, this._linkId, this._parentID, this._relationTypeID))
      {
        int num2 = (int) createCopyTableDialog.ShowDialog();
      }
    }
  }

  private void OnCapitalize(object sender, EventArgs e)
  {
  }

  private void OnNewRecord(object sender, EventArgs e)
  {
    if (this._isPortalReadOnly)
      return;
    DataRow row = this._proxyTable.NewRow();
    this._proxyTable.Rows.Add(row);
    this.SetCurrentCell(Convert.ToInt64(row[this._keyColumnIndex]));
  }

  private void SetCurrentCell(long key)
  {
    int count = this._grid.Rows.Count;
    int currentCellIndex = this.GetCurrentCellIndex();
    for (int index = 0; index < count; ++index)
    {
      if (Convert.ToInt64(this._grid.Rows[index].Cells[this._keyColumnIndex].Value).Equals(key))
      {
        this._grid.CurrentCell = this._grid.Rows[index].Cells[currentCellIndex];
        break;
      }
    }
  }

  private int GetCurrentCellIndex()
  {
    if (this._grid.CurrentCell != null)
      return this._grid.CurrentCell.ColumnIndex;
    int count = this._grid.Columns.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this._grid.Columns[index].Visible)
        return index;
    }
    return 0;
  }

  private void SetCurrentCell(Guid key)
  {
    string str = key.ToString();
    int currentCellIndex = this.GetCurrentCellIndex();
    int count = this._grid.Rows.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this._grid.Rows[index].Cells[this._guidColumnIndex].Value.ToString().Equals(str, StringComparison.InvariantCultureIgnoreCase))
      {
        this._grid.CurrentCell = this._grid.Rows[index].Cells[currentCellIndex];
        break;
      }
    }
  }

  private void OnCopyRecord(object sender, EventArgs e)
  {
    if (this._isPortalReadOnly)
      return;
    DataRow dataRow = this._proxyTable.Rows.Find((object) Convert.ToInt64(this._grid.CurrentRow.Cells[this._keyColumnIndex].Value));
    if (dataRow == null)
      return;
    object[] itemArray = dataRow.ItemArray;
    DataRow newRow = this._proxyTable.NewRow();
    int count = this._proxyTable.Columns.Count;
    for (int index = 2; index < count; ++index)
    {
      if (index != this._recOwnerColumnIndex && index != this._recModDateColumnIndex)
      {
        DataColumn column = this._proxyTable.Columns[index];
        if (!column.ReadOnly && !column.ExtendedProperties.ContainsKey((object) "F_DONTCOPY"))
          newRow[index] = itemArray[index];
      }
    }
    this.AddNewProxyRow(newRow);
  }

  private void AddNewProxyRow(DataRow newRow)
  {
    try
    {
      this._proxyTable.Rows.Add(newRow);
      this.SetCurrentCell(Convert.ToInt64(newRow[this._keyColumnIndex]));
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Imbase.Client_1136"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void OnDeleteRecord(object sender, EventArgs e)
  {
    if (this.CheckReadOnly())
      return;
    if (this._bookmarks.Count == 0)
    {
      if (this._grid.CurrentRow == null)
        return;
      DataRow row = this._proxyTable.Rows.Find((object) Convert.ToInt64(this._grid.CurrentRow.Cells[this._keyColumnIndex].Value));
      if (row == null)
        return;
      this._proxyTable.Rows.Remove(row);
    }
    else
    {
      while (this._bookmarks.Count > 0)
      {
        DataRow row = this._proxyTable.Rows.Find((object) this._bookmarks[0]);
        if (row != null)
          this._proxyTable.Rows.Remove(row);
      }
    }
  }

  private bool CheckReadOnly()
  {
    if (!this._grid.ReadOnly)
      return this._isPortalReadOnly;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_TableEditor_ReadOnlyTable"), LocalizationHolder.rm.GetString("Imbase.Table.ChangeKeep.Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    return true;
  }

  private void OnFilterShow(object sender, EventArgs e)
  {
    this._bookmarks.Clear();
    this._bookmarks.AddRange((IEnumerable<long>) this._filter);
    this._grid.Invalidate();
    this.UpdateRecsIndicator();
  }

  private void OnFilterAddSelected(object sender, EventArgs e)
  {
    int count = this._bookmarks.Count;
    if (count <= 0)
      return;
    for (int index = 0; index < count; ++index)
    {
      long bookmark = this._bookmarks[index];
      if (!this._filter.Contains(bookmark))
      {
        this._filter.Add(bookmark);
        this.FilterChanged = true;
      }
    }
  }

  private void OnFilterRemoveSelected(object sender, EventArgs e)
  {
    int count = this._bookmarks.Count;
    if (count <= 0)
      return;
    for (int index = 0; index < count; ++index)
    {
      long bookmark = this._bookmarks[index];
      if (this._filter.Contains(bookmark))
      {
        this._filter.Remove(bookmark);
        this.FilterChanged = true;
      }
    }
  }

  private void OnFilterClear(object sender, EventArgs e)
  {
    if (this._filter.Count <= 0)
      return;
    this._filter.Clear();
    this.FilterChanged = true;
  }

  private void OnCut(object sender, EventArgs e) => this.ExecuteCutCopy(true);

  private void OnCopy(object sender, EventArgs e) => this.ExecuteCutCopy(false);

  private void OnPaste(object sender, EventArgs e)
  {
    MenuItemBase menuItemBase = sender as MenuItemBase;
    if (this.btPaste.Items.Count == 1 || menuItemBase == null)
      return;
    if (menuItemBase.Tag == null)
      menuItemBase = (MenuItemBase) this.btPaste.Items[0];
    TableData tag = menuItemBase.Tag as TableData;
    Dictionary<long, long> needUpdateObjects = new Dictionary<long, long>();
    if (!CopyRecords.CopyTableRecords(this, tag, needUpdateObjects))
      return;
    if (tag != null)
      tag.IsCut = false;
    if (needUpdateObjects.Count <= 0)
      return;
    foreach (long key in needUpdateObjects.Keys)
    {
      if (!this._needUpdateObjects.ContainsKey(key))
        this._needUpdateObjects.Add(key, needUpdateObjects[key]);
    }
  }

  private void OnClearClipboard(object sender, EventArgs e)
  {
    this._clipboard.RemoveDataObjects(typeof (IImbaseTableData));
  }

  private void OnClipboardContextChanged(object sender, EventArgs e)
  {
    this.BuildPasteMenu(this._clipboard.GetDataObjects(typeof (IImbaseTableData)));
    this.btPaste.Enabled = this.btPaste.Items.Count > 0 && !this._grid.ReadOnly && this.mnNewRecord.Enabled;
  }

  private void BuildPasteMenu(object[] tables)
  {
    MenuItemBase.MenuItemCollection items = this.btPaste.Items;
    items.Clear();
    int length = tables.Length;
    for (int index = 0; index < length; ++index)
    {
      if (tables[index] is IImbaseTableData table)
      {
        MenuButtonItem menuButtonItem = new MenuButtonItem(table.ToString(), new EventHandler(this.OnPaste));
        menuButtonItem.Tag = (object) table;
        if (table.TableId == this._tableId)
          menuButtonItem.Font = new Font(this.btPaste.Font, FontStyle.Underline);
        if (table is ICutCopy cutCopy)
          menuButtonItem.ImageIndex = cutCopy.ImageIndex;
        items.Add((ToolbarItemBase) menuButtonItem);
      }
    }
    if (items.Count <= 0)
      return;
    items.Add((ToolbarItemBase) this.mnClearClipboard);
  }

  private void ExecuteCutCopy(bool cut)
  {
    if (this._grid.CurrentRow == null && this._grid.Rows.Count > 0)
      this._grid.CurrentCell = this._grid.Rows[0].Cells[2];
    DataSet fromSelectedRows = this.CreateDataSetFromSelectedRows();
    if (fromSelectedRows.Tables["IMS_DATA"].Rows.Count == 0)
      return;
    if (cut)
      this.OnDeleteRecord((object) null, new EventArgs());
    this._clipboard.SetDataObject((object) new TableData(fromSelectedRows, Math.Abs(this._tableId), this._linkId, this._tableInfo.Caption, cut)
    {
      usedKeys = this._usedKeys,
      createdObjects = this._createdObjects
    });
  }

  private DataSet CreateDataSetFromSelectedRows()
  {
    DataSet fromSelectedRows = new DataSet("IMS_TABLE_RECORDS");
    DataTable table1 = this._attTable.Copy();
    table1.AcceptChanges();
    table1.TableName = "IMS_ATTR_TYPES";
    DataTable table2 = this._dataTable.Clone();
    int count = this._bookmarks.Count;
    if (count == 0 && this._grid.Rows.Count > 0)
    {
      DataRow dataRow = this._dataTable.Rows.Find((object) Convert.ToInt64(this._grid.CurrentRow.Cells[this._keyColumnIndex].Value));
      if (dataRow != null)
        table2.Rows.Add(dataRow.ItemArray);
    }
    else
    {
      for (int index = 0; index < count; ++index)
      {
        DataRow dataRow = this._dataTable.Rows.Find((object) this._bookmarks[index]);
        if (dataRow != null)
          table2.Rows.Add(dataRow.ItemArray);
      }
    }
    table2.TableName = "IMS_DATA";
    table2.AcceptChanges();
    fromSelectedRows.Tables.Add(table1);
    fromSelectedRows.Tables.Add(table2);
    return fromSelectedRows;
  }

  private bool ShowRestructureWarn()
  {
    return !this.CanUndo && !this.CanRedo || MessageBox.Show("После изменения структуры таблицы\n'История изменений' будет удалена.\nВы действительно хотите продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
  }

  private void OnAddRecOwner_Click(object sender, EventArgs e)
  {
    this.CreateColumn(Intermech.Imbase.Consts.ImbaseTableRecordOwnerAttGUID);
  }

  private void CreateColumn(Guid attGuid)
  {
    if (!this.ShowRestructureWarn())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBAttributeType attributeType = session.GetAttributeType(attGuid);
      this._attTable.Rows.Add((object) attGuid.ToString(), (object) 2, (object) 0, (object) "", (object) 0, (object) "", (object) 0);
      TableLoadHelper.CreateDataColumn(this._dataTable, attributeType);
      this.LoadTables(session);
      this.Text = base.Text;
      this.MapColumns(session);
      this.UpdateButtons();
    }
  }

  private void OnAddRecDate_Click(object sender, EventArgs e)
  {
    this.CreateColumn(Intermech.Imbase.Consts.ImbaseTableRecordModDateAttGUID);
  }

  private void FillOwnerColumn(IUserSession session)
  {
    DataColumn column = this._dataTable.Columns[Intermech.Imbase.Consts.ImbaseTableRecordOwnerAttGUID.ToString()];
    if (column == null)
      return;
    this._dataTable.BeginLoadData();
    try
    {
      IDBObject dbObject = session.GetObject(this._tableId);
      QuickObjectInfo objectInfo = session.GetObjectInfo(dbObject.OwnerID);
      foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
      {
        if (TableLoadHelper.IsNull(row[column]))
          row[column] = (object) objectInfo.VersionGuid;
      }
    }
    finally
    {
      this._dataTable.EndLoadData();
    }
  }

  private void FillModDateColumn(IUserSession session)
  {
    DataColumn column = this._dataTable.Columns[Intermech.Imbase.Consts.ImbaseTableRecordModDateAttGUID.ToString()];
    if (column == null)
      return;
    this._dataTable.BeginLoadData();
    try
    {
      IDBAttribute attributeById = session.GetObject(this._tableId).GetAttributeByID(session.IdentHelper.ModifyContentDateID);
      DateTime dateTime = DateTime.Now;
      if (attributeById != null)
        dateTime = attributeById.AsDateTime;
      foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
      {
        if (TableLoadHelper.IsNull(row[column]))
          row[column] = (object) dateTime;
      }
    }
    finally
    {
      this._dataTable.EndLoadData();
    }
  }

  private void OnEditStructure(object sender, EventArgs e)
  {
    if (this._grid.IsCurrentCellInEditMode)
      this._grid.CancelEdit();
    bool colsOrderChanged = this._isColsOrderChanged;
    if (!this.ShowRestructureWarn())
      return;
    this.UpdateDataSet();
    DataSet newData;
    if (StructureEditor.EditStructure(this, false, out newData, out this._isColsOrderChanged))
    {
      this._dataSet = newData;
      bool flag = !colsOrderChanged ? this._isColsOrderChanged : colsOrderChanged;
      this._isColsOrderChanged = true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.LoadTables(sessionKeeper.Session);
        this.Text = base.Text;
        this.MapColumns(sessionKeeper.Session);
        this.UpdateButtons();
      }
      this._isColsOrderChanged = flag;
    }
    else
      this._isColsOrderChanged = colsOrderChanged;
  }

  private void OnFilterBeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.mnFilterAdd.Enabled = this.mnFilterAdd2.Enabled = this.mnFilterRemove.Enabled = this.mnFilterRemove2.Enabled = this._bookmarks.Count > 0;
  }

  private void OnQuickSearchComboBox_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r' && e.KeyChar != '\n')
      return;
    int modifierKeys = (int) Control.ModifierKeys;
    bool fingOther = modifierKeys == 131072 /*0x020000*/;
    bool searchUp = modifierKeys == 262144 /*0x040000*/;
    this.FindTextInColumn(this._grid.CurrentCell.ColumnIndex, (sender as ComboBox).Text, fingOther, searchUp);
  }

  private void OnQuickSearch_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.cbQuickSearch.ComboBox.Text = this._grid.CurrentCell.FormattedValue.ToString();
  }

  private bool FindTextInColumn(int cellIndex, string text, bool fingOther, bool searchUp)
  {
    DataGridViewCell currentCell = this._grid.CurrentCell;
    bool textInColumn = false;
    int index = this._grid.CurrentRow.Index;
    int num = this._grid.Rows.Count - 1;
    if (!fingOther)
    {
      if (searchUp)
        --index;
      else
        ++index;
    }
    while (index >= 0 && index < num)
    {
      string str = this._grid.Rows[index].Cells[cellIndex].FormattedValue.ToString();
      textInColumn = !string.IsNullOrEmpty(text) ? str.IndexOf(text) != -1 : str.Length == 0;
      if (fingOther)
        textInColumn = !textInColumn;
      if (textInColumn)
      {
        if (fingOther)
        {
          this.cbQuickSearch.ComboBox.Text = str;
          break;
        }
        break;
      }
      if (searchUp)
        --index;
      else
        ++index;
    }
    if (textInColumn)
      this._grid.CurrentCell = this._grid.Rows[index].Cells[cellIndex];
    return textInColumn;
  }

  private bool Next()
  {
    int index = this._grid.CurrentRow.Index;
    int columnIndex = this._grid.CurrentCell.ColumnIndex;
    if (index == this._grid.RowCount)
      return false;
    this._grid.CurrentCell = this._grid.Rows[index + 1].Cells[columnIndex];
    return true;
  }

  private bool Prior()
  {
    int index = this._grid.CurrentRow.Index;
    int columnIndex = this._grid.CurrentCell.ColumnIndex;
    if (index == 0)
      return false;
    this._grid.CurrentCell = this._grid.Rows[index - 1].Cells[columnIndex];
    return true;
  }

  private bool Eof() => this._grid.CurrentRow.Index == this._grid.RowCount - 1;

  private bool Bof() => this._grid.CurrentRow.Index == 0;

  public bool Execute(ICommandState commandState)
  {
    switch (commandState.CommandName)
    {
      case "AVS.FindNext":
        this.OnFind((object) null, EventArgs.Empty);
        return true;
      case "AVS.SearchAndReplaceText":
        this.OnReplace((object) null, EventArgs.Empty);
        return true;
      case "Undo":
        this.OnUndo();
        return true;
      case "Redo":
        this.OnRedo();
        return true;
      default:
        return false;
    }
  }

  public bool QueryStatus(ICommandState commandState)
  {
    switch (commandState.CommandName)
    {
      case "AVS.FindNext":
      case "AVS.SearchAndReplaceText":
        commandState.Enabled = true;
        return true;
      case "Undo":
        commandState.Enabled = this.CanUndo;
        return true;
      case "Redo":
        commandState.Enabled = this.CanRedo;
        return true;
      default:
        return false;
    }
  }

  private void OnReplace(object sender, EventArgs e)
  {
    this._findPos = -1;
    FindService.ShowDialog((IFindTarget) this, true);
  }

  private void OnFind(object sender, EventArgs e)
  {
    this._findPos = -1;
    FindService.ShowDialog((IFindTarget) this, false);
  }

  public void ResetFindPos() => this._findPos = -1;

  public void Find(FindReplaceData data, bool lockScroll = false)
  {
    if (this._grid.CurrentCell == null)
      return;
    bool scroll = this._findPos != -1;
    if (lockScroll)
      scroll = false;
    else
      this._findPos = -1;
    bool searchUp = (data._options & FindReplaceOptions.SearchUp) != 0;
    bool matchCase = (data._options & FindReplaceOptions.MatchCase) != 0;
    bool wholeWord = (data._options & FindReplaceOptions.WholeWord) != 0;
    bool selected = (data._options & FindReplaceOptions.Selected) != 0;
    bool fromCurrent = (data._options & FindReplaceOptions.FromCurrent) != 0;
    if (scroll)
      fromCurrent = true;
    int columnIndex = this._grid.CurrentCell.ColumnIndex;
    if (columnIndex < 0 || this.FindTextInColumnEx(columnIndex, data._findText, searchUp, matchCase, wholeWord, selected, fromCurrent, scroll) || this._groupChanging)
      return;
    int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1143"), (object) data._findText));
  }

  public void Replace(FindReplaceData data)
  {
    if (this.CheckReadOnly() || !this.CanChangeDataInColumn())
      return;
    bool groupChanging = this._groupChanging;
    try
    {
      this._groupChanging = true;
      if (this._findPos == -1)
        this.Find(data, false);
      if (this._findPos != -1)
      {
        this._grid.CurrentCell.Value = TableEditor.Replace(this._grid.CurrentCell.Value.ToString(), this._findPos, data._findText.Length, data._replaceText);
        this._findPos += data._replaceText.Length;
        if ((data._options & FindReplaceOptions.RelaceAll) != FindReplaceOptions.None)
          data._options |= FindReplaceOptions.FromCurrent;
        this.Find(data, true);
      }
      if ((data._options & FindReplaceOptions.RelaceAll) != FindReplaceOptions.None)
      {
        data._options |= FindReplaceOptions.FromCurrent;
        while (this._findPos != -1)
          this.Replace(data);
      }
    }
    finally
    {
      this._groupChanging = groupChanging;
    }
    this._findPos = -1;
  }

  private static object Replace(string text, int startPos, int size, string newText)
  {
    int length = text.Length;
    if (startPos > length || startPos + size > length)
      return (object) text;
    string str1 = text.Substring(0, startPos);
    string str2 = text.Substring(startPos + size);
    string str3 = newText;
    string str4 = str2;
    return (object) (str1 + str3 + str4);
  }

  private bool FindTextInColumnEx(
    int cellIndex,
    string text,
    bool searchUp,
    bool matchCase,
    bool wholeWord,
    bool selected,
    bool fromCurrent,
    bool scroll)
  {
    bool textInColumnEx = false;
    int index = 0;
    int count = this._grid.Rows.Count;
    if (searchUp)
      index = count - 1;
    if (fromCurrent)
      index = this._grid.CurrentRow.Index;
    StringComparison comparsion = StringComparison.InvariantCultureIgnoreCase;
    if (matchCase)
      comparsion = StringComparison.InvariantCulture;
    if (scroll)
    {
      if (searchUp)
        --index;
      else
        ++index;
    }
    while (index >= 0 && index < count)
    {
      bool flag = true;
      if (selected && !this._bookmarks.Contains(Convert.ToInt64(this._grid.Rows[index].Cells[this._keyColumnIndex].Value)))
        flag = false;
      if (flag)
      {
        string text1 = this._grid.Rows[index].Cells[cellIndex].FormattedValue.ToString();
        if (string.IsNullOrEmpty(text))
        {
          if (string.IsNullOrEmpty(text1))
          {
            textInColumnEx = true;
            this._findPos = 0;
          }
          else
            textInColumnEx = false;
        }
        else
          textInColumnEx = this.FindSubString(text1, text, wholeWord, ref this._findPos, comparsion);
      }
      if (!textInColumnEx)
      {
        if (searchUp)
          --index;
        else
          ++index;
      }
      else
        break;
    }
    if (textInColumnEx)
      this._grid.CurrentCell = this._grid.Rows[index].Cells[cellIndex];
    return textInColumnEx;
  }

  private bool FindSubString(
    string text,
    string value,
    bool wholeWord,
    ref int pos,
    StringComparison comparsion)
  {
    if (!wholeWord)
    {
      pos = pos != -1 ? (text.Length <= pos + 1 ? -1 : text.IndexOf(value, pos + 1, comparsion)) : text.IndexOf(value, comparsion);
      return pos != -1;
    }
    if (text.Equals(value, comparsion))
    {
      pos = 0;
      return true;
    }
    int length = value.Length;
    int startIndex = pos + 1;
    if (startIndex < 0)
      startIndex = 0;
    if (startIndex >= text.Length)
    {
      pos = -1;
      return false;
    }
    for (; startIndex < text.Length; startIndex += length)
    {
      pos = text.IndexOf(value, startIndex, comparsion);
      if (pos == -1)
        return false;
      if (pos == 0 && this.IsSpaceChar(text[length]))
        return true;
      bool flag1 = true;
      if (pos > 0)
        flag1 = this.IsSpaceChar(text[pos - 1]);
      bool flag2 = true;
      if (pos + length < text.Length)
        flag2 = this.IsSpaceChar(text[pos + length]);
      if (flag1 & flag2)
        return true;
    }
    return false;
  }

  private bool IsSpaceChar(char ch) => !char.IsLetterOrDigit(ch) && ch != '_';

  private void FindService_Closed(object sender, EventArgs e) => this._findPos = -1;

  private void TableEditor_BeforeFirstShown(object sender, EventArgs e)
  {
    foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
    {
      int result;
      if (int.TryParse(column.Name, out result))
        column.DisplayIndex = this._colsOrderDict[result];
    }
    this._grid.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(this.Grid_ColumnDisplayIndexChanged);
  }

  private bool UpdateDataSet()
  {
    int result = -1;
    this._colsWidthDict.Clear();
    if (!this._isColsOrderChanged)
    {
      foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
      {
        if (int.TryParse(column.Name, out result) && result != -12 && result != -2)
          this._colsWidthDict.Add(result, column.Width);
      }
      return false;
    }
    DataTable table = this._attTable.Clone();
    Guid empty = Guid.Empty;
    int num = 0;
    foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
    {
      if (column.DisplayIndex > num)
        num = column.DisplayIndex;
    }
    DataRow[] dataRowArray1 = new DataRow[num + 1];
    foreach (DataGridViewColumn column in (BaseCollection) this._grid.Columns)
    {
      if (int.TryParse(column.Name, out result) && result != -12 && result != -2)
      {
        this._colsWidthDict.Add(result, column.Width);
        DataRow[] dataRowArray2 = this._attTable.Select($"{"F_ATTRIBUTE_GUID"}='{this.GetAttrGuidForId(result)}'");
        if (dataRowArray2 != null && dataRowArray2.Length != 0)
          dataRowArray1[column.DisplayIndex] = dataRowArray2[0];
      }
    }
    foreach (DataRow dataRow in dataRowArray1)
    {
      if (dataRow != null)
      {
        DataRow row = table.NewRow();
        for (int columnIndex = 0; columnIndex < dataRow.ItemArray.Length; ++columnIndex)
          row[columnIndex] = dataRow[columnIndex];
        table.Rows.Add(row);
      }
    }
    this._dataSet.Tables.Remove("IMS_ATTR_TYPES");
    this._dataSet.Tables.Add(table);
    this._attTable = table;
    return true;
  }

  private Guid GetAttrGuidForId(int id)
  {
    foreach (AttributeTypeProperties rowsAttProp in this._rowsAttProps)
    {
      if (rowsAttProp.AttributeID == id)
        return rowsAttProp.AttributeGuid;
    }
    return Guid.Empty;
  }

  private TreeNode Find_trvItem(TreeNodeCollection nodes, long itemsID)
  {
    foreach (TreeNode node in nodes)
    {
      if (node.Tag is NodeInfo tag && tag.IsTableReference)
      {
        if (itemsID == -1L || tag.ObjectId == itemsID)
          return node;
      }
      else if (node.Nodes.Count > 0)
      {
        TreeNode trvItem = this.Find_trvItem(node.Nodes, itemsID);
        if (trvItem != null)
          return trvItem;
      }
    }
    return (TreeNode) null;
  }

  private string GetTemplatesBody
  {
    get
    {
      if (this._templateBody == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._tableId);
          if (dbObject1 == null)
            return string.Empty;
          IDBAttribute attributeById1 = dbObject1.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTemplateRefAttID);
          if (attributeById1 == null || attributeById1.AsInteger == 0L)
            return string.Empty;
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(attributeById1.AsInteger);
          if (dbObject2 == null)
            return string.Empty;
          IDBAttribute attributeById2 = dbObject2.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTemplateDataAttID);
          if (attributeById2 == null)
            return string.Empty;
          this._templateBody = (object) attributeById2.AsString;
        }
      }
      return this._templateBody.ToString();
    }
  }

  private void On_btnSelect_Click(object sender, EventArgs e)
  {
    this._bookmarks.Clear();
    string filter = this._templatesTree.Filter;
    int ordinal = this._proxyTable.Columns[$"{Intermech.Imbase.Consts.ImbaseTemplateAttID}"].Ordinal;
    if (string.IsNullOrEmpty(filter))
    {
      foreach (DataRow row in (InternalDataCollectionBase) this._proxyTable.Rows)
      {
        long int64 = Convert.ToInt64(row[this._keyColumnIndex]);
        if (!this._bookmarks.Contains(int64))
          this._bookmarks.Add(int64);
      }
    }
    else
    {
      foreach (DataRow row in (InternalDataCollectionBase) this._proxyTable.Rows)
      {
        string str = row[ordinal].ToString();
        long int64 = Convert.ToInt64(row[this._keyColumnIndex]);
        if (!this._bookmarks.Contains(int64))
        {
          bool flag = false;
          for (int index = 0; index < filter.Length; ++index)
          {
            if (str.IndexOf(filter[index]) == -1)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            this._bookmarks.Add(int64);
        }
      }
    }
    this._grid.Invalidate();
    this.UpdateRecsIndicator();
  }

  private void On_btnSetFilter_Click(object sender, EventArgs e)
  {
    string filter = this._templatesTree.Filter;
    if (string.IsNullOrEmpty(filter))
      return;
    int ordinal = this._proxyTable.Columns[$"{Intermech.Imbase.Consts.ImbaseTemplateAttID}"].Ordinal;
    foreach (long bookmark in this._bookmarks)
    {
      DataRow[] dataRowArray = this._proxyTable.Select($"[{this._proxyTable.Columns[this._keyColumnIndex].ColumnName}]={bookmark}");
      string str = dataRowArray[0][ordinal].ToString();
      if (string.IsNullOrEmpty(str))
      {
        dataRowArray[0][ordinal] = (object) filter;
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder(str);
        for (int index = 0; index < filter.Length; ++index)
        {
          if (str.IndexOf(filter[index]) == -1)
            stringBuilder.Append(filter[index]);
        }
        char[] charArray = stringBuilder.ToString().ToCharArray();
        Array.Sort<char>(charArray);
        stringBuilder.Remove(0, stringBuilder.Length);
        stringBuilder.Append(charArray);
        dataRowArray[0][ordinal] = (object) stringBuilder.ToString();
      }
    }
    this._grid.Invalidate();
  }

  private void On_btnTree_Click(object sender, EventArgs e)
  {
    if (this._spltContainer.Panel1Collapsed)
    {
      if (this._templatesTree == null)
      {
        this._templatesTree = new SymbolSelectRB_Ctrl(this.GetTemplatesBody);
        this._templatesTree.ButtonOKText = LocalizationHolder.rm.GetString("Imbase_Save");
        this._templatesTree.Dock = DockStyle.Fill;
        this._pnlFilter.Controls.Add((Control) this._templatesTree);
        this._templatesTree.BtnClickEvent += new EventHandler(this.On_templatesTree_BtnClickEvent);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IImbaseServer server = EditorHelper.GetServer(sessionKeeper.Session);
          if (server != null)
          {
            List<long> ids = new List<long>();
            DataTable tree = new DataTable();
            server.GetFoldersForTable(sessionKeeper.Session.SessionGUID, this._tableId, string.Empty, out ids, out tree);
            this._treeBuilder.CreateTree(tree);
          }
        }
        if (this._trv.Nodes.Count > 0)
          this._trv.SelectedNode = this.Find_trvItem(this._trv.Nodes, this._linkId);
      }
      this._spltContainer.Panel1Collapsed = false;
    }
    else
      this._spltContainer.Panel1Collapsed = true;
  }

  private void On_templatesTree_BtnClickEvent(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._trv.SelectedNode;
    if (selectedNode == null || !(selectedNode.Tag is NodeInfo tag) || !tag.IsTableReference || (sender as Button).DialogResult != DialogResult.OK)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(tag.ObjectId);
      if (dbObject == null)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTemplateAttID);
      if (attributeById == null)
        return;
      attributeById.Value = (object) this._templatesTree.Filter;
      this._templatesTree.Filter = attributeById.Value.ToString();
    }
  }

  private void On_trv_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeNode selectedNode = this._trv.SelectedNode;
    if (selectedNode == null || !(selectedNode.Tag is NodeInfo tag) || !tag.IsTableReference)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(tag.ObjectId);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTemplateAttID);
        if (attributeById != null)
          this._templatesTree.Filter = attributeById.AsString;
      }
    }
    if (!this._chbAutoSelect.Checked)
      return;
    this.On_btnSelect_Click((object) null, (EventArgs) null);
  }

  private void OnbtProperties_Click(object sender, EventArgs e)
  {
    using (ImbaseTablePropertiesForm tablePropertiesForm = new ImbaseTablePropertiesForm(this._linkId, this._tableId))
    {
      int num = (int) tablePropertiesForm.ShowDialog();
    }
  }

  private void CheckAndApplyDependence(DataGridViewCellCancelEventArgs e)
  {
    if (e.ColumnIndex < 0 || !(this._grid.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn column) || !this._depMappingColumns.ContainsKey((DataGridViewColumn) column))
      return;
    MasterColDef depMappingColumn = this._depMappingColumns[(DataGridViewColumn) column];
    if (!(column.DataSource is DataView dataSource))
      return;
    DataGridViewCell cell = this._grid.Rows[e.RowIndex].Cells[depMappingColumn.ColumnIndex];
    string str = cell.Value.ToString();
    if (!string.IsNullOrWhiteSpace(str))
    {
      string format = "{0} = {1}";
      if (dataSource.Table.Columns["F_MAIN"].DataType.Equals(typeof (string)))
        format = "{0} = '{1}'";
      dataSource.RowFilter = string.Format(format, (object) "F_MAIN", (object) str);
      this._depView = dataSource;
      this._emptyFilter = "F_MAIN is null";
      cell.ErrorText = (string) null;
    }
    else
    {
      cell.ErrorText = $"Не установлено значение для задания фильтра в поле.\"{this._grid.Columns[e.ColumnIndex].HeaderText}\"";
      throw new AbortException();
    }
  }

  private void CancelDependency(DataGridViewCellEventArgs e)
  {
    if (this._depView != null)
      this._depView.RowFilter = this._emptyFilter;
    this._depView = (DataView) null;
  }

  private void CheckDepValue(DataGridViewCellEventArgs e)
  {
    if (this._depMappingColumns.Count <= 0)
      return;
    foreach (DataGridViewColumn key in this._depMappingColumns.Keys)
    {
      MasterColDef depMappingColumn = this._depMappingColumns[key];
      if (depMappingColumn.ColumnIndex == e.ColumnIndex || e.ColumnIndex == key.Index)
      {
        DataGridViewRow row1 = this._grid.Rows[e.RowIndex];
        DataGridViewCell cell = row1.Cells[key.Index];
        object obj1 = cell.Value;
        object obj2 = row1.Cells[depMappingColumn.ColumnIndex].Value;
        if (DBNull.Value.Equals(obj1) && DBNull.Value.Equals(obj2))
          break;
        if (key is DataGridViewComboBoxColumn viewComboBoxColumn && viewComboBoxColumn.DataSource is DataView dataSource)
        {
          foreach (DataRow row2 in (InternalDataCollectionBase) dataSource.Table.Rows)
          {
            if (row2[0].Equals(obj2) && row2[1].Equals(obj1))
              return;
          }
          cell.ErrorText = $"Значение в ячейке не соответствует заданному фильтру \"{this._grid.Columns[depMappingColumn.ColumnIndex].HeaderText}\"";
          break;
        }
      }
    }
  }

  private void Grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
  {
    long recordId = this.RecordId;
    if (!this._isAdmin && this._recOwnerColumn != null)
    {
      bool flag = false;
      if (e.ColumnIndex == this._recOwnerColumn.Index)
        flag = true;
      if (!this._userGuid.Equals(this._grid.Rows[e.RowIndex].Cells[this._recOwnerColumn.Name].Value.ToString(), StringComparison.InvariantCultureIgnoreCase))
      {
        if (!flag)
          throw new AbortException();
        if (!this._canChangeRecOwner)
          throw new AbortException();
      }
    }
    if (this._lastCheckedRowId != recordId)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        if (!EditorHelper.GetServer(session).GetSecurityForRecord(session.SessionGUID, Math.Abs(this._tableId), this.RecordId).CheckAccess(ActionType.Edit))
          throw new AbortException();
        this._lastCheckedRowId = recordId;
      }
    }
    this.toolBar1.Enabled = false;
    this.contextMenuBarItem1.Enabled = false;
    if (this._depMappingColumns.Count <= 0)
      return;
    this.CheckAndApplyDependence(e);
  }

  private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
  {
    this.toolBar1.Enabled = true;
    this.contextMenuBarItem1.Enabled = true;
    if (this._depMappingColumns.Count <= 0)
      return;
    this.CancelDependency(e);
  }

  private void On_btnCheckDataSet_Click(object sender, EventArgs e) => this.ValidationDataSet();

  private void ValidationDataSet()
  {
    if (this._proxyTable == null || this._proxyTable.Columns.Count == 0)
      return;
    List<string> missingAttrs = new List<string>();
    foreach (DataColumn column in (InternalDataCollectionBase) this._proxyTable.Columns)
    {
      string columnName = column.ColumnName;
      string str1 = columnName;
      int num = -12;
      string str2 = num.ToString();
      if (!(str1 == str2))
      {
        string str3 = columnName;
        num = -2;
        string str4 = num.ToString();
        if (!(str3 == str4))
        {
          int result = 0;
          if ((!int.TryParse(columnName, out result) || this._attTable.Select($"F_ATTRIBUTE_GUID = '{SQLStringHelper.QuoteLikeString(column.Caption)}'").Length == 0 && !missingAttrs.Contains(columnName)) && !missingAttrs.Contains(columnName))
            missingAttrs.Add(columnName);
        }
      }
    }
    if (missingAttrs.Count > 0)
    {
      if (!this.CorrectionDataSet(missingAttrs))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.LoadTables(sessionKeeper.Session);
        this.UpdateButtons();
        this.Text = base.Text;
        this.MapColumns(sessionKeeper.Session);
      }
    }
    int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("TableEditor_MissingAttrs_ValidatingFinish"), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private bool CorrectionDataSet(List<string> missingAttrs)
  {
    MissingAttrsForTableEditorForm forTableEditorForm = new MissingAttrsForTableEditorForm(missingAttrs, this._rowsAttProps);
    if (!forTableEditorForm.NeedShowForm || forTableEditorForm.ShowDialog() == DialogResult.Cancel)
      return false;
    bool flag = false;
    List<string> stringList = new List<string>();
    if (forTableEditorForm.AddedAttrs.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (string addedAttr in forTableEditorForm.AddedAttrs)
        {
          try
          {
            Guid anAttributeGuid = new Guid(addedAttr);
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid);
            if (attributeType == null)
            {
              if (!stringList.Contains(addedAttr))
                stringList.Add(addedAttr);
            }
            else
            {
              DataRow row = this._attTable.NewRow();
              row["F_ATTRIBUTE_GUID"] = (object) anAttributeGuid;
              row["F_REQUIRED"] = (object) 2;
              row["F_COMPUTED"] = (object) 0;
              row["F_FORMULA"] = (object) string.Empty;
              row["F_UNIQUE"] = (object) 0;
              if (!(attributeType.DefaultValue is DBNull))
              {
                DataTable possibleValues = attributeType.GetPossibleValues();
                if (possibleValues != null && possibleValues.Rows.Count > 0)
                {
                  string filterExpression = $"{possibleValues.Columns[1].ColumnName}='{attributeType.DefaultValue}'";
                  DataRow[] dataRowArray = possibleValues.Select(filterExpression);
                  if (dataRowArray.Length != 0)
                    row["F_DEFAULT_VALUE"] = (object) dataRowArray[0]["F_DESCRIPTION"].ToString();
                }
                else
                  row["F_DEFAULT_VALUE"] = (object) attributeType.DefaultValueDescription;
              }
              row["F_OPTIONS"] = (object) Convert.ToInt32((object) attributeType.Options);
              row["F_UNITS"] = (object) string.Empty;
              this._attTable.Rows.Add(row);
              flag = true;
            }
          }
          catch
          {
            if (!stringList.Contains(addedAttr))
              stringList.Add(addedAttr);
          }
        }
      }
    }
    if (forTableEditorForm.DeletedAttrs.Count > 0)
    {
      foreach (string deletedAttr in forTableEditorForm.DeletedAttrs)
      {
        if (!stringList.Contains(deletedAttr))
          stringList.Add(deletedAttr);
      }
    }
    if (stringList.Count > 0)
    {
      foreach (string name in stringList)
      {
        if (this._dataTable.Columns.Contains(name))
        {
          this._dataTable.Columns.Remove(name);
          flag = true;
        }
        DataRow[] dataRowArray = this._attTable.Select($"{"F_ATTRIBUTE_GUID"}='{name}'");
        if (dataRowArray.Length != 0)
        {
          this._attTable.Rows.Remove(dataRowArray[0]);
          flag = true;
        }
      }
    }
    if (!flag || this._attTable.Rows.Count == 0 || this._attTable.Columns.Count == 0)
      return false;
    this._attTable.Rows[0][0] = this._attTable.Rows[0][0];
    return true;
  }

  private void OnGrid_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\u0001')
      return;
    DataGridViewRowCollection rows = this._grid.Rows;
    int count = rows.Count;
    for (int index = 0; index < count; ++index)
    {
      long int64 = Convert.ToInt64(rows[index].Cells[this._keyColumnIndex].Value);
      if (!this._bookmarks.Contains(int64))
        this._bookmarks.Add(int64);
    }
    this._grid.Invalidate();
    this.UpdateRecsIndicator();
    e.Handled = true;
  }

  private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
  {
    if (e.ColumnIndex != -1 || e.RowIndex == -1 || e.RowIndex >= this._proxyTable.Rows.Count)
      return;
    long int64 = Convert.ToInt64(this.GetProxyRow(e.RowIndex)[this._keyColumnIndex]);
    e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
    if (this._usedKeys.Contains(int64) && this._nil != null && this._imgObjectIndex != -1)
    {
      Rectangle cellBounds = e.CellBounds;
      int x = cellBounds.Right - 20;
      cellBounds = e.CellBounds;
      int top = cellBounds.Top;
      cellBounds = e.CellBounds;
      int num = (cellBounds.Height - 16 /*0x10*/) / 2;
      int y = top + num;
      if (x > 12)
        this._nil.ImageList.Draw(e.Graphics, new Point(x, y), this._imgObjectIndex);
    }
    e.Handled = true;
  }

  private void Grid_CellToolTipTextNeeded(
    object sender,
    DataGridViewCellToolTipTextNeededEventArgs e)
  {
    if (e.ColumnIndex != -1 || e.RowIndex == -1 || this._keyColumnIndex == -1)
      return;
    long int64 = Convert.ToInt64(this._grid.Rows[e.RowIndex].Cells[this._keyColumnIndex].Value);
    if (!this._usedKeys.Contains(int64))
      return;
    DataRow[] dataRowArray = this._createdObjects.Select($"[{Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID}]={int64}");
    if (dataRowArray == null || dataRowArray.Length == 0)
      return;
    e.ToolTipText = $"{dataRowArray[0][2]}{Environment.NewLine}Идентификатор объекта :{dataRowArray[0][0]}";
  }

  private long GetCreatedObjectId(long recordId)
  {
    return TableEditor.GetCreatedObjectId(recordId, this._usedKeys, this._createdObjects);
  }

  internal static long GetCreatedObjectId(
    long recordId,
    List<long> usedKeys,
    DataTable createdObjects)
  {
    if (usedKeys.Contains(recordId))
    {
      DataRow[] dataRowArray = createdObjects.Select($"[{Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID}]={recordId}");
      if (dataRowArray != null && dataRowArray.Length != 0)
        return Convert.ToInt64(dataRowArray[0][0]);
    }
    return -1;
  }

  private void mnObjectProps_Click(object sender, EventArgs e)
  {
    long createdObjectId = this.GetCreatedObjectId(this.RecordId);
    if (createdObjectId == -1L)
      return;
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, createdObjectId);
  }

  private void OnLockPanelPaint(object sender, PaintEventArgs e)
  {
    if (this._nil == null || this._lockedIndex == -1 || !this._grid.ReadOnly)
      return;
    this._nil.ImageList.Draw(e.Graphics, new Point(1, 1), this._lockedIndex);
  }

  private void UpdateRecsIndicator()
  {
    this.sbRecNumPanel.Text = $"Зап {this._dataTable.Rows.Count} Выд {this._bookmarks.Count}";
    long recordId = this.RecordId;
    this.sbRecKey.Text = $"ID : {recordId}";
    this.sbRecKey.Tag = (object) recordId;
    Guid recordGuid = this.RecordGuid;
    this.sbRecGuid.Text = $"GUID : {recordGuid}";
    this.sbRecGuid.Tag = (object) recordGuid;
  }

  private void SecuritySelectedRowsEdit(object sender, EventArgs e)
  {
    if (this._bookmarks.Count == 0)
      return;
    List<object> objectList = new List<object>();
    foreach (long bookmark in this._bookmarks)
      objectList.Add((object) ImbaseHelper.CreateCategoryId(this._tableId, bookmark));
    this.EditSecurity(objectList.ToArray(), 25);
  }

  private void EditSecurity(object[] ids, int category)
  {
    this._securityCategory = category;
    this.SecurityEditorForm.Execute(ids, (ISecurityCallback) this, false);
  }

  private void SecurityCurrentRowEdit(object sender, EventArgs e)
  {
    this.EditSecurity(new List<object>()
    {
      (object) ImbaseHelper.CreateCategoryId(this._tableId, this.RecordId)
    }.ToArray(), 25);
  }

  private void SecurityCurrentAttEdit(object sender, EventArgs e)
  {
  }

  public IDBSecurity GetSecurity(IUserSession session, object id)
  {
    long categoryId = (long) id;
    long tableId = -1;
    int recordId = -1;
    ref long local1 = ref tableId;
    ref int local2 = ref recordId;
    ImbaseHelper.GetObjectAndId(categoryId, out local1, out local2);
    return EditorHelper.GetServer(session).GetSecurityForRecord(session.SessionGUID, tableId, (long) recordId);
  }

  public int MaintainedCategory => this._securityCategory;

  public Tuple<int, object> Applicability => (Tuple<int, object>) null;

  private void Grid_CellEnter(object sender, DataGridViewCellEventArgs e)
  {
    DataGridViewColumn column = this._grid.Columns[e.ColumnIndex];
    if (column == null || this.sbAttImage.Tag == column)
      return;
    this.sbAttImage.Tag = (object) column;
    int result = 0;
    int.TryParse(column.Name, out result);
    int index = TableEditor.IndexOfAttProp(result, this._rowsAttProps);
    if (index == -1)
      return;
    AttributeTypeProperties rowsAttProp = this._rowsAttProps[index];
    Icon icon = Statics.IconSrv.GetIcon(3, -1, (object) rowsAttProp.FieldType);
    Image image = (Image) null;
    if (icon != null)
      image = (Image) icon.ToBitmap();
    this.sbAttImage.Image = image;
    this.sbShortName.Text = rowsAttProp.ShortName;
    this.sbLongName.Text = rowsAttProp.Name;
  }

  private void DetachExtender() => this._extender.DataGridView = (DataGridView) null;

  private void AttachExtender()
  {
    if (!this.mnFilterOn.Checked)
      return;
    this._extender.DataGridView = (DataGridView) this._grid;
  }

  private void MnFilterOn_Click(object sender, EventArgs e)
  {
    this.splitContainer1.Panel1Collapsed = !this.mnFilterOn.Checked;
    if (this.mnFilterOn.Checked)
      this.AttachExtender();
    else
      this.DetachExtender();
  }

  private void LeftFilterFactory_GridFilterCreated(object sender, GridFilterEventArgs args)
  {
    DataGridViewColumn column1 = args.Column;
    if (string.IsNullOrEmpty(column1.DataPropertyName))
      return;
    if (!column1.Visible)
    {
      args.GridFilter = (IGridFilter) new EmptyGridFilter();
    }
    else
    {
      IGridFilter gridFilter = args.GridFilter;
      if (gridFilter == null)
        return;
      DataColumn column2 = this._proxyTable.Columns[column1.DataPropertyName];
      if (column2 != null && this.mnOnlyData.Checked && column2.ExtendedProperties.ContainsKey((object) "F_VIRTUAL"))
      {
        args.GridFilter = (IGridFilter) new EmptyGridFilter();
      }
      else
      {
        DataGridViewComboBoxColumn viewComboBoxColumn1 = column1 as DataGridViewComboBoxColumn;
        if (!this._objectRefColumns.Contains(column1) && !this._recordRefColumns.Contains(column1) && viewComboBoxColumn1 == null && !column2.DataType.Equals(typeof (ValuesArray)))
        {
          gridFilter.ApplyAutoComplete(column2);
        }
        else
        {
          ComboBox comboBox = gridFilter.ComboBox;
          System.Type dataType;
          bool isArray;
          List<string> distinctValues = GridFilterBase.GetDistinctValues(column2, out dataType, out isArray, out bool _);
          bool flag = false;
          Dictionary<string, string> dictionary = (Dictionary<string, string>) null;
          ObjectStringMapEnumerationSource enumerationSource = new ObjectStringMapEnumerationSource();
          if (typeof (string).Equals(dataType) || typeof (Guid).Equals(dataType))
            flag = true;
          DataGridViewComboBoxColumn viewComboBoxColumn2;
          if (this._objectRefColumns.Contains(column1))
          {
            viewComboBoxColumn2 = (DataGridViewComboBoxColumn) null;
            dictionary = this._objectRefMap;
          }
          else if (this._recordRefColumns.Contains(column1))
          {
            viewComboBoxColumn2 = (DataGridViewComboBoxColumn) null;
            dictionary = this._recordRefMap;
          }
          else if (viewComboBoxColumn1 != null)
          {
            if (viewComboBoxColumn1.DataSource is DataTable dataSource)
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataSource.Rows)
              {
                if (!DBNull.Value.Equals(row[viewComboBoxColumn1.ValueMember]))
                  enumerationSource.AddMapping(row[viewComboBoxColumn1.ValueMember], row[viewComboBoxColumn1.DisplayMember].ToString());
              }
            }
          }
          else if (isArray)
          {
            foreach (string name in distinctValues)
              enumerationSource.AddMapping((object) name, name);
          }
          if (dictionary != null)
          {
            foreach (string key in distinctValues)
            {
              if (!string.IsNullOrEmpty(key) && dictionary.ContainsKey(key))
                enumerationSource.AddMapping((object) key, dictionary[key]);
            }
          }
          EnumerationGridFilter enumerationGridFilter = new EnumerationGridFilter((IEnumerationSource) enumerationSource);
          enumerationGridFilter.UseQuotes = flag;
          if (isArray)
            enumerationGridFilter.UseLike = true;
          enumerationGridFilter.UseCustomFilterPlacement = true;
          args.GridFilter = (IGridFilter) enumerationGridFilter;
        }
      }
    }
  }

  private void MnExpFilter_Click(object sender, EventArgs e)
  {
    this.mnFilterOn.Checked = !this.mnFilterOn.Checked;
    this.MnFilterOn_Click((object) this.mnFilterOn, EventArgs.Empty);
  }

  private void MnFilterClean_Click(object sender, EventArgs e) => this._extender.ClearFilters();

  private void OnNormaCS_Click(object sender, EventArgs e)
  {
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.Start();
  }

  private void OnFindByNumberNCS_Click(object sender, EventArgs e)
  {
    if (this._grid.CurrentCell == null || this._grid.CurrentCell.RowIndex == -1 || this._grid.CurrentCell.ColumnIndex == -1)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByNumber(Convert.ToString(this._grid.CurrentCell.Value));
  }

  private void OnFindByNameNCS_Click(object sender, EventArgs e)
  {
    if (this._grid.CurrentCell == null || this._grid.CurrentCell.RowIndex == -1 || this._grid.CurrentCell.ColumnIndex == -1)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByName(Convert.ToString(this._grid.CurrentCell.Value));
  }

  private void OnFindByTextNCS_Click(object sender, EventArgs e)
  {
    if (this._grid.CurrentCell == null || this._grid.CurrentCell.RowIndex == -1 || this._grid.CurrentCell.ColumnIndex == -1)
      return;
    ServiceUtils.GetService<INormaCSService>((object) ServicesManager.ServiceContainer, true)?.FindByText(Convert.ToString(this._grid.CurrentCell.Value));
  }

  private void On_sbRecKey_DoubleClick(object sender, EventArgs e)
  {
    if (!(sender is ToolStripStatusLabel stripStatusLabel))
      return;
    object tag = stripStatusLabel.Tag;
    if (tag == null)
      return;
    System.Windows.Forms.Clipboard.SetText(tag.ToString());
  }

  private void On_sbFindId_Click(object sender, EventArgs e) => FindRecByID.Execute(this);

  internal void GotoId(string text)
  {
    if (string.IsNullOrWhiteSpace(text))
      return;
    long result1;
    if (long.TryParse(text, out result1))
    {
      this.SetCurrentCell(result1);
    }
    else
    {
      Guid result2;
      if (!Guid.TryParse(text, out result2))
        return;
      this.SetCurrentCell(result2);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._disposed = true;
      this.UnSubscribe();
      this._grid.ColumnDisplayIndexChanged -= new DataGridViewColumnEventHandler(this.Grid_ColumnDisplayIndexChanged);
      this.SetRenderer((IToolBarRenderer) new EmptyToolbarRenderer());
      this._depView = (DataView) null;
      this._recOwnerColumn = (DataGridViewColumn) null;
      this._recModDateColumn = (DataGridViewColumn) null;
      this._recordRefColumns.Clear();
      this._objectRefColumns.Clear();
      this._depMappingColumns.Clear();
      this._readOnlyColumns.Clear();
      this._protectedColumns.Clear();
      this._describers.Clear();
      if (this.components != null)
        this.components.Dispose();
    }
    try
    {
      if (this._grid != null)
      {
        this._grid.CancelEdit();
        this._grid.Dispose();
      }
      this._grid = (DoubleBufferedDataGridView) null;
      base.Dispose(disposing);
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableEditor));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DefaultGridFilterFactory gridFilterFactory = new DefaultGridFilterFactory();
    this._spltContainer = new SplitContainer();
    this._trv = new TreeView();
    this.menuBar1 = new MenuBar();
    this.contextMenuBarItem1 = new ContextMenuBarItem();
    this.mnCheckOut = new MenuButtonItem();
    this.mnCheckIn = new MenuButtonItem();
    this.mnSaveChanges = new MenuButtonItem();
    this.mnCancelChanges = new MenuButtonItem();
    this.mnObjectProps = new MenuButtonItem();
    this.mnSelection = new MenuButtonItem();
    this.mnSelectByCondition = new MenuButtonItem();
    this.mnSelectSameRecords = new MenuButtonItem();
    this.mnSelectRecord = new MenuButtonItem();
    this.mnClearSelection = new MenuButtonItem();
    this.mnInvertSelection = new MenuButtonItem();
    this.mnDeleteSelected = new MenuButtonItem();
    this._securityEditSelectedRows = new MenuButtonItem();
    this.mnCreateTable = new MenuButtonItem();
    this.mnFilter = new MenuButtonItem();
    this.mnFilterShow = new MenuButtonItem();
    this.mnFilterAdd = new MenuButtonItem();
    this.mnFilterRemove = new MenuButtonItem();
    this.mnFilterClear = new MenuButtonItem();
    this.mnCapitalize = new MenuButtonItem();
    this.mnNewRecord = new MenuButtonItem();
    this.mnCopyRecord = new MenuButtonItem();
    this.mnDeleteRecord = new MenuButtonItem();
    this.mnFind = new MenuButtonItem();
    this.mnReplace = new MenuButtonItem();
    this.mnRepeadFind = new MenuButtonItem();
    this._securityCurrentRow = new MenuButtonItem();
    this.menuButtonItem2 = new MenuButtonItem();
    this.miNormaCS = new MenuButtonItem();
    this.miLaunchNormaCS = new MenuButtonItem();
    this.miFindByNumberNCS = new MenuButtonItem();
    this.miFindByNameNCS = new MenuButtonItem();
    this.miFindByTextNCS = new MenuButtonItem();
    this._grid = new DoubleBufferedDataGridView();
    this._splitter = new Splitter();
    this._pnlFilter = new Panel();
    this._pnlBottom = new Panel();
    this._chbAutoSelect = new CheckBox();
    this._btnSetFilter = new Button();
    this._btnSelect = new Button();
    this.splitContainer1 = new SplitContainer();
    this.leftFilterFactory = new LayoutedGridFilterFactoryControl();
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this._btnTree = new ButtonItem();
    this.labelItem1 = new LabelItem();
    this.cbQuickSearch = new ComboBoxItem();
    this.btNewRecord = new ButtonItem();
    this.btFind = new ButtonItem();
    this.btReplace = new ButtonItem();
    this.mnExpFilter = new DropDownMenuItem();
    this.mnFilterOn = new MenuButtonItem();
    this.mnFilterClean = new MenuButtonItem();
    this.mnOnlyData = new MenuButtonItem();
    this.btManualFilter = new DropDownMenuItem();
    this.mnFilterShow2 = new MenuButtonItem();
    this.mnFilterAdd2 = new MenuButtonItem();
    this.mnFilterRemove2 = new MenuButtonItem();
    this.mnFilterClear2 = new MenuButtonItem();
    this.btCheckOut = new ButtonItem();
    this.btCheckIn = new ButtonItem();
    this.btCancelCheckOut = new ButtonItem();
    this.btCancelChanges = new ButtonItem();
    this.btSaveChanges = new ButtonItem();
    this.btCut = new ButtonItem();
    this.btCopy = new ButtonItem();
    this.btPaste = new DropDownMenuItem();
    this.mnClearClipboard = new MenuButtonItem();
    this.btAddRecOwner = new ButtonItem();
    this.btAddRecDate = new ButtonItem();
    this.btEditStructure = new ButtonItem();
    this.btProperties = new ButtonItem();
    this._btnCheckDataSet = new ButtonItem();
    this.tbNormaCS = new DropDownMenuItem();
    this.tbLaunchNormaCS = new MenuButtonItem();
    this.tbFindByNumberNCS = new MenuButtonItem();
    this.tbFindByNameNCS = new MenuButtonItem();
    this.FindByTextNCS = new MenuButtonItem();
    this.imageList1 = new ImageList(this.components);
    this.statusStrip1 = new StatusStrip();
    this.sbRecNumPanel = new ToolStripStatusLabel();
    this.sbRecKey = new ToolStripStatusLabel();
    this.sbRecGuid = new ToolStripStatusLabel();
    this.sbFindId = new ToolStripSplitButton();
    this.sbAttImage = new ToolStripStatusLabel();
    this.sbShortName = new ToolStripStatusLabel();
    this.sbLongName = new ToolStripStatusLabel();
    this._treeBuilder = new TreeBuilder(this.components);
    this._extender = new DataGridFilterExtender(this.components);
    this.menuButtonItem17 = new MenuButtonItem();
    this.menuButtonItem18 = new MenuButtonItem();
    this.menuButtonItem19 = new MenuButtonItem();
    this.menuButtonItem20 = new MenuButtonItem();
    this._spltContainer.BeginInit();
    this._spltContainer.Panel1.SuspendLayout();
    this._spltContainer.Panel2.SuspendLayout();
    this._spltContainer.SuspendLayout();
    ((ISupportInitialize) this._grid).BeginInit();
    this._pnlBottom.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.statusStrip1.SuspendLayout();
    this._extender.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._spltContainer, "_spltContainer");
    this._spltContainer.Name = "_spltContainer";
    this._spltContainer.Panel1.Controls.Add((Control) this._trv);
    this._spltContainer.Panel1.Controls.Add((Control) this.menuBar1);
    this._spltContainer.Panel1.Controls.Add((Control) this._splitter);
    this._spltContainer.Panel1.Controls.Add((Control) this._pnlFilter);
    this._spltContainer.Panel1.Controls.Add((Control) this._pnlBottom);
    componentResourceManager.ApplyResources((object) this._spltContainer.Panel1, "_spltContainer.Panel1");
    this._spltContainer.Panel2.Controls.Add((Control) this.splitContainer1);
    componentResourceManager.ApplyResources((object) this._spltContainer.Panel2, "_spltContainer.Panel2");
    this._trv.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this._trv, "_trv");
    this._trv.HideSelection = false;
    this._trv.ItemHeight = 20;
    this._trv.Name = "_trv";
    this._trv.Sorted = true;
    this._trv.AfterSelect += new TreeViewEventHandler(this.On_trv_AfterSelect);
    this.menuBar1.Guid = new Guid("aff7368a-60de-49c0-aeb6-1eed3176b5fa");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem1
    });
    componentResourceManager.ApplyResources((object) this.menuBar1, "menuBar1");
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) null;
    this.menuBar1.Stretch = false;
    componentResourceManager.ApplyResources((object) this.contextMenuBarItem1, "contextMenuBarItem1");
    this.contextMenuBarItem1.Items.AddRange(new ToolbarItemBase[17]
    {
      (ToolbarItemBase) this.mnCheckOut,
      (ToolbarItemBase) this.mnCheckIn,
      (ToolbarItemBase) this.mnSaveChanges,
      (ToolbarItemBase) this.mnCancelChanges,
      (ToolbarItemBase) this.mnObjectProps,
      (ToolbarItemBase) this.mnSelection,
      (ToolbarItemBase) this.mnFilter,
      (ToolbarItemBase) this.mnCapitalize,
      (ToolbarItemBase) this.mnNewRecord,
      (ToolbarItemBase) this.mnCopyRecord,
      (ToolbarItemBase) this.mnDeleteRecord,
      (ToolbarItemBase) this.mnFind,
      (ToolbarItemBase) this.mnReplace,
      (ToolbarItemBase) this.mnRepeadFind,
      (ToolbarItemBase) this._securityCurrentRow,
      (ToolbarItemBase) this.menuButtonItem2,
      (ToolbarItemBase) this.miNormaCS
    });
    this.contextMenuBarItem1.ShowText = true;
    this.contextMenuBarItem1.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItem1_BeforePopup);
    componentResourceManager.ApplyResources((object) this.mnCheckOut, "mnCheckOut");
    this.mnCheckOut.ShowText = true;
    this.mnCheckOut.Click += new EventHandler(this.OnCheckOut);
    componentResourceManager.ApplyResources((object) this.mnCheckIn, "mnCheckIn");
    this.mnCheckIn.ShowText = true;
    this.mnCheckIn.Click += new EventHandler(this.OnCheckIn);
    componentResourceManager.ApplyResources((object) this.mnSaveChanges, "mnSaveChanges");
    this.mnSaveChanges.ShowText = true;
    this.mnSaveChanges.Click += new EventHandler(this.OnSaveChanges);
    componentResourceManager.ApplyResources((object) this.mnCancelChanges, "mnCancelChanges");
    this.mnCancelChanges.ShowText = true;
    this.mnCancelChanges.Click += new EventHandler(this.OnCancelChanges);
    this.mnObjectProps.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnObjectProps, "mnObjectProps");
    this.mnObjectProps.ShowText = true;
    this.mnObjectProps.Click += new EventHandler(this.mnObjectProps_Click);
    this.mnSelection.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnSelection, "mnSelection");
    this.mnSelection.Items.AddRange(new ToolbarItemBase[8]
    {
      (ToolbarItemBase) this.mnSelectByCondition,
      (ToolbarItemBase) this.mnSelectSameRecords,
      (ToolbarItemBase) this.mnSelectRecord,
      (ToolbarItemBase) this.mnClearSelection,
      (ToolbarItemBase) this.mnInvertSelection,
      (ToolbarItemBase) this.mnDeleteSelected,
      (ToolbarItemBase) this._securityEditSelectedRows,
      (ToolbarItemBase) this.mnCreateTable
    });
    this.mnSelection.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnSelectByCondition, "mnSelectByCondition");
    this.mnSelectByCondition.ShowText = true;
    this.mnSelectByCondition.Click += new EventHandler(this.OnSelectByCondition);
    this.mnSelectSameRecords.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnSelectSameRecords, "mnSelectSameRecords");
    this.mnSelectSameRecords.ShowText = true;
    this.mnSelectSameRecords.Click += new EventHandler(this.OnSelectSameRecords);
    componentResourceManager.ApplyResources((object) this.mnSelectRecord, "mnSelectRecord");
    this.mnSelectRecord.ShowText = true;
    this.mnSelectRecord.Click += new EventHandler(this.OnSelectRecord);
    componentResourceManager.ApplyResources((object) this.mnClearSelection, "mnClearSelection");
    this.mnClearSelection.ShowText = true;
    this.mnClearSelection.Click += new EventHandler(this.OnClearSelection);
    componentResourceManager.ApplyResources((object) this.mnInvertSelection, "mnInvertSelection");
    this.mnInvertSelection.ShowText = true;
    this.mnInvertSelection.Click += new EventHandler(this.OnInvertSelection);
    this.mnDeleteSelected.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnDeleteSelected, "mnDeleteSelected");
    this.mnDeleteSelected.ShowText = true;
    this.mnDeleteSelected.Click += new EventHandler(this.OnDeleteSelected);
    this._securityEditSelectedRows.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._securityEditSelectedRows, "_securityEditSelectedRows");
    this._securityEditSelectedRows.ShowText = true;
    this._securityEditSelectedRows.Click += new EventHandler(this.SecuritySelectedRowsEdit);
    this.mnCreateTable.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnCreateTable, "mnCreateTable");
    this.mnCreateTable.ShowText = true;
    this.mnCreateTable.Click += new EventHandler(this.OnCreateTable);
    componentResourceManager.ApplyResources((object) this.mnFilter, "mnFilter");
    this.mnFilter.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.mnFilterShow,
      (ToolbarItemBase) this.mnFilterAdd,
      (ToolbarItemBase) this.mnFilterRemove,
      (ToolbarItemBase) this.mnFilterClear
    });
    this.mnFilter.ShowText = true;
    this.mnFilter.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.OnFilterBeforePopup);
    componentResourceManager.ApplyResources((object) this.mnFilterShow, "mnFilterShow");
    this.mnFilterShow.ShowText = true;
    this.mnFilterShow.Click += new EventHandler(this.OnFilterShow);
    componentResourceManager.ApplyResources((object) this.mnFilterAdd, "mnFilterAdd");
    this.mnFilterAdd.ShowText = true;
    this.mnFilterAdd.Click += new EventHandler(this.OnFilterAddSelected);
    componentResourceManager.ApplyResources((object) this.mnFilterRemove, "mnFilterRemove");
    this.mnFilterRemove.ShowText = true;
    this.mnFilterRemove.Click += new EventHandler(this.OnFilterRemoveSelected);
    componentResourceManager.ApplyResources((object) this.mnFilterClear, "mnFilterClear");
    this.mnFilterClear.ShowText = true;
    this.mnFilterClear.Click += new EventHandler(this.OnFilterClear);
    componentResourceManager.ApplyResources((object) this.mnCapitalize, "mnCapitalize");
    this.mnCapitalize.ShowText = true;
    this.mnCapitalize.Visible = false;
    this.mnCapitalize.Click += new EventHandler(this.OnCapitalize);
    this.mnNewRecord.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnNewRecord, "mnNewRecord");
    this.mnNewRecord.ShowText = true;
    this.mnNewRecord.Click += new EventHandler(this.OnNewRecord);
    componentResourceManager.ApplyResources((object) this.mnCopyRecord, "mnCopyRecord");
    this.mnCopyRecord.ShowText = true;
    this.mnCopyRecord.Click += new EventHandler(this.OnCopyRecord);
    componentResourceManager.ApplyResources((object) this.mnDeleteRecord, "mnDeleteRecord");
    this.mnDeleteRecord.ShowText = true;
    this.mnDeleteRecord.Click += new EventHandler(this.OnDeleteRecord);
    this.mnFind.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnFind, "mnFind");
    this.mnFind.Shortcut = Shortcut.CtrlF;
    this.mnFind.ShowText = true;
    this.mnFind.Click += new EventHandler(this.OnFind);
    componentResourceManager.ApplyResources((object) this.mnReplace, "mnReplace");
    this.mnReplace.Shortcut = Shortcut.CtrlH;
    this.mnReplace.ShowText = true;
    this.mnReplace.Click += new EventHandler(this.OnReplace);
    componentResourceManager.ApplyResources((object) this.mnRepeadFind, "mnRepeadFind");
    this.mnRepeadFind.Shortcut = Shortcut.F3;
    this.mnRepeadFind.ShowText = true;
    componentResourceManager.ApplyResources((object) this._securityCurrentRow, "_securityCurrentRow");
    this._securityCurrentRow.ShowText = true;
    this._securityCurrentRow.Click += new EventHandler(this.SecurityCurrentRowEdit);
    componentResourceManager.ApplyResources((object) this.menuButtonItem2, "menuButtonItem2");
    this.menuButtonItem2.ShowText = true;
    this.menuButtonItem2.Visible = false;
    this.menuButtonItem2.Click += new EventHandler(this.SecurityCurrentAttEdit);
    this.miNormaCS.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.miNormaCS, "miNormaCS");
    this.miNormaCS.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.miLaunchNormaCS,
      (ToolbarItemBase) this.miFindByNumberNCS,
      (ToolbarItemBase) this.miFindByNameNCS,
      (ToolbarItemBase) this.miFindByTextNCS
    });
    this.miNormaCS.ShowText = true;
    componentResourceManager.ApplyResources((object) this.miLaunchNormaCS, "miLaunchNormaCS");
    this.miLaunchNormaCS.ShowText = true;
    this.miLaunchNormaCS.Click += new EventHandler(this.OnNormaCS_Click);
    componentResourceManager.ApplyResources((object) this.miFindByNumberNCS, "miFindByNumberNCS");
    this.miFindByNumberNCS.ShowText = true;
    this.miFindByNumberNCS.Click += new EventHandler(this.OnFindByNumberNCS_Click);
    componentResourceManager.ApplyResources((object) this.miFindByNameNCS, "miFindByNameNCS");
    this.miFindByNameNCS.ShowText = true;
    this.miFindByNameNCS.Click += new EventHandler(this.OnFindByNameNCS_Click);
    componentResourceManager.ApplyResources((object) this.miFindByTextNCS, "miFindByTextNCS");
    this.miFindByTextNCS.ShowText = true;
    this.miFindByTextNCS.Click += new EventHandler(this.OnFindByTextNCS_Click);
    this._grid.AllowUserToAddRows = false;
    this._grid.AllowUserToOrderColumns = true;
    this._grid.AllowUserToResizeRows = false;
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
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
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.BackColor = SystemColors.Window;
    gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle2.ForeColor = SystemColors.ControlText;
    gridViewCellStyle2.SelectionBackColor = SystemColors.ControlLight;
    gridViewCellStyle2.SelectionForeColor = SystemColors.WindowText;
    gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
    this._grid.DefaultCellStyle = gridViewCellStyle2;
    this._grid.MultiSelect = false;
    this._grid.Name = "_grid";
    this.menuBar1.SetPopupMenu((Control) this._grid, (MenuBarItem) this.contextMenuBarItem1);
    this._grid.RowFilter = "";
    this._grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this._grid.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent;
    this._grid.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.Transparent;
    this._grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
    this._grid.SortChanged = false;
    this._grid.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.Grid_CellBeginEdit);
    this._grid.CellClick += new DataGridViewCellEventHandler(this.Grid_CellClick);
    this._grid.CellDoubleClick += new DataGridViewCellEventHandler(this.Grid_CellDoubleClick);
    this._grid.CellEndEdit += new DataGridViewCellEventHandler(this.Grid_CellEndEdit);
    this._grid.CellEnter += new DataGridViewCellEventHandler(this.Grid_CellEnter);
    this._grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
    this._grid.CellPainting += new DataGridViewCellPaintingEventHandler(this.Grid_CellPainting);
    this._grid.CellParsing += new DataGridViewCellParsingEventHandler(this.Grid_CellParsing);
    this._grid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(this.Grid_CellToolTipTextNeeded);
    this._grid.CellValidated += new DataGridViewCellEventHandler(this.Grid_CellValidated);
    this._grid.CellValueChanged += new DataGridViewCellEventHandler(this.Grid_CellValueChanged);
    this._grid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.Grid_EditingControlShowing);
    this._grid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(this.Grid_RowPrePaint);
    this._grid.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.Grid_UserDeletingRow);
    this._grid.KeyPress += new KeyPressEventHandler(this.OnGrid_KeyPress);
    componentResourceManager.ApplyResources((object) this._splitter, "_splitter");
    this._splitter.Name = "_splitter";
    this._splitter.TabStop = false;
    this._pnlFilter.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this._pnlFilter, "_pnlFilter");
    this._pnlFilter.Name = "_pnlFilter";
    this._pnlBottom.Controls.Add((Control) this._chbAutoSelect);
    this._pnlBottom.Controls.Add((Control) this._btnSetFilter);
    this._pnlBottom.Controls.Add((Control) this._btnSelect);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._chbAutoSelect, "_chbAutoSelect");
    this._chbAutoSelect.Name = "_chbAutoSelect";
    this._chbAutoSelect.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnSetFilter, "_btnSetFilter");
    this._btnSetFilter.Name = "_btnSetFilter";
    this._btnSetFilter.UseVisualStyleBackColor = true;
    this._btnSetFilter.Click += new EventHandler(this.On_btnSetFilter_Click);
    componentResourceManager.ApplyResources((object) this._btnSelect, "_btnSelect");
    this._btnSelect.Name = "_btnSelect";
    this._btnSelect.UseVisualStyleBackColor = true;
    this._btnSelect.Click += new EventHandler(this.On_btnSelect_Click);
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.leftFilterFactory);
    this.splitContainer1.Panel1Collapsed = true;
    this.splitContainer1.Panel2.Controls.Add((Control) this._grid);
    componentResourceManager.ApplyResources((object) this.leftFilterFactory, "leftFilterFactory");
    gridFilterFactory.CreateDistinctGridFilters = false;
    gridFilterFactory.DefaultGridFilterType = typeof (TextGridFilterCombo);
    gridFilterFactory.DefaultShowDateInBetweenOperator = false;
    gridFilterFactory.DefaultShowNumericInBetweenOperator = true;
    gridFilterFactory.HandleEnumerationTypes = true;
    gridFilterFactory.MaximumDistinctValues = 20;
    this.leftFilterFactory.InnerGridFilterFactory = (IGridFilterFactory) gridFilterFactory;
    this.leftFilterFactory.Name = "leftFilterFactory";
    this.leftFilterFactory.GridFilterCreated += new GridFilterEventHandler(this.LeftFilterFactory_GridFilterCreated);
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("d859f7ae-ea50-4194-9554-98a3d290e389");
    this.toolBar1.Hidden = false;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[22]
    {
      (ToolbarItemBase) this._btnTree,
      (ToolbarItemBase) this.labelItem1,
      (ToolbarItemBase) this.cbQuickSearch,
      (ToolbarItemBase) this.btNewRecord,
      (ToolbarItemBase) this.btFind,
      (ToolbarItemBase) this.btReplace,
      (ToolbarItemBase) this.mnExpFilter,
      (ToolbarItemBase) this.btManualFilter,
      (ToolbarItemBase) this.btCheckOut,
      (ToolbarItemBase) this.btCheckIn,
      (ToolbarItemBase) this.btCancelCheckOut,
      (ToolbarItemBase) this.btCancelChanges,
      (ToolbarItemBase) this.btSaveChanges,
      (ToolbarItemBase) this.btCut,
      (ToolbarItemBase) this.btCopy,
      (ToolbarItemBase) this.btPaste,
      (ToolbarItemBase) this.btAddRecOwner,
      (ToolbarItemBase) this.btAddRecDate,
      (ToolbarItemBase) this.btEditStructure,
      (ToolbarItemBase) this.btProperties,
      (ToolbarItemBase) this._btnCheckDataSet,
      (ToolbarItemBase) this.tbNormaCS
    });
    componentResourceManager.ApplyResources((object) this.toolBar1, "toolBar1");
    this.toolBar1.Name = "toolBar1";
    this._btnTree.AutoToggle = AutoToggleType.Single;
    componentResourceManager.ApplyResources((object) this._btnTree, "_btnTree");
    this._btnTree.Enabled = false;
    this._btnTree.Click += new EventHandler(this.On_btnTree_Click);
    componentResourceManager.ApplyResources((object) this.labelItem1, "labelItem1");
    componentResourceManager.ApplyResources((object) this.cbQuickSearch, "cbQuickSearch");
    this.cbQuickSearch.MinimumControlWidth = 150;
    this.cbQuickSearch.Padding.Bottom = 0;
    this.cbQuickSearch.Padding.Left = 1;
    this.cbQuickSearch.Padding.Right = 1;
    this.cbQuickSearch.Padding.Top = 0;
    this.btNewRecord.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btNewRecord, "btNewRecord");
    this.btNewRecord.Click += new EventHandler(this.OnNewRecord);
    this.btFind.BeginGroup = true;
    this.btFind.BuddyMenu = this.mnFind;
    componentResourceManager.ApplyResources((object) this.btFind, "btFind");
    this.btFind.Click += new EventHandler(this.OnFind);
    this.btReplace.BuddyMenu = this.mnReplace;
    componentResourceManager.ApplyResources((object) this.btReplace, "btReplace");
    this.btReplace.Click += new EventHandler(this.OnReplace);
    this.mnExpFilter.AutoToggle = AutoToggleType.Single;
    this.mnExpFilter.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnExpFilter, "mnExpFilter");
    this.mnExpFilter.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.mnFilterOn,
      (ToolbarItemBase) this.mnFilterClean,
      (ToolbarItemBase) this.mnOnlyData
    });
    this.mnExpFilter.ShowText = true;
    this.mnExpFilter.Click += new EventHandler(this.MnExpFilter_Click);
    this.mnFilterOn.AutoToggle = AutoToggleType.Single;
    componentResourceManager.ApplyResources((object) this.mnFilterOn, "mnFilterOn");
    this.mnFilterOn.ShowText = true;
    this.mnFilterOn.Click += new EventHandler(this.MnFilterOn_Click);
    componentResourceManager.ApplyResources((object) this.mnFilterClean, "mnFilterClean");
    this.mnFilterClean.ShowText = true;
    this.mnFilterClean.Click += new EventHandler(this.MnFilterClean_Click);
    this.mnOnlyData.AutoToggle = AutoToggleType.Single;
    this.mnOnlyData.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnOnlyData, "mnOnlyData");
    this.mnOnlyData.ShowText = true;
    this.mnOnlyData.Click += new EventHandler(this.MnFilterOn_Click);
    this.btManualFilter.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btManualFilter, "btManualFilter");
    this.btManualFilter.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.mnFilterShow2,
      (ToolbarItemBase) this.mnFilterAdd2,
      (ToolbarItemBase) this.mnFilterRemove2,
      (ToolbarItemBase) this.mnFilterClear2
    });
    this.btManualFilter.ShowText = true;
    this.btManualFilter.Visible = false;
    this.btManualFilter.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.OnFilterBeforePopup);
    this.btManualFilter.Click += new EventHandler(this.OnFilterShow);
    componentResourceManager.ApplyResources((object) this.mnFilterShow2, "mnFilterShow2");
    this.mnFilterShow2.ShowText = true;
    this.mnFilterShow2.Click += new EventHandler(this.OnFilterShow);
    componentResourceManager.ApplyResources((object) this.mnFilterAdd2, "mnFilterAdd2");
    this.mnFilterAdd2.ShowText = true;
    this.mnFilterAdd2.Click += new EventHandler(this.OnFilterAddSelected);
    componentResourceManager.ApplyResources((object) this.mnFilterRemove2, "mnFilterRemove2");
    this.mnFilterRemove2.ShowText = true;
    this.mnFilterRemove2.Click += new EventHandler(this.OnFilterRemoveSelected);
    componentResourceManager.ApplyResources((object) this.mnFilterClear2, "mnFilterClear2");
    this.mnFilterClear2.ShowText = true;
    this.mnFilterClear2.Click += new EventHandler(this.OnFilterClear);
    this.btCheckOut.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btCheckOut, "btCheckOut");
    this.btCheckOut.Click += new EventHandler(this.OnCheckOut);
    componentResourceManager.ApplyResources((object) this.btCheckIn, "btCheckIn");
    this.btCheckIn.Click += new EventHandler(this.OnCheckIn);
    componentResourceManager.ApplyResources((object) this.btCancelCheckOut, "btCancelCheckOut");
    this.btCancelCheckOut.Visible = false;
    this.btCancelCheckOut.Click += new EventHandler(this.OnCancelChanges);
    componentResourceManager.ApplyResources((object) this.btCancelChanges, "btCancelChanges");
    this.btCancelChanges.Click += new EventHandler(this.OnCancelChanges);
    componentResourceManager.ApplyResources((object) this.btSaveChanges, "btSaveChanges");
    this.btSaveChanges.Click += new EventHandler(this.OnSaveChanges);
    this.btCut.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btCut, "btCut");
    this.btCut.Click += new EventHandler(this.OnCut);
    componentResourceManager.ApplyResources((object) this.btCopy, "btCopy");
    this.btCopy.Click += new EventHandler(this.OnCopy);
    componentResourceManager.ApplyResources((object) this.btPaste, "btPaste");
    this.btPaste.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.mnClearClipboard
    });
    this.btPaste.ShowText = true;
    this.btPaste.Click += new EventHandler(this.OnPaste);
    this.mnClearClipboard.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnClearClipboard, "mnClearClipboard");
    this.mnClearClipboard.ShowText = true;
    this.mnClearClipboard.Click += new EventHandler(this.OnClearClipboard);
    this.btAddRecOwner.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btAddRecOwner, "btAddRecOwner");
    this.btAddRecOwner.Click += new EventHandler(this.OnAddRecOwner_Click);
    this.btAddRecDate.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btAddRecDate, "btAddRecDate");
    this.btAddRecDate.Click += new EventHandler(this.OnAddRecDate_Click);
    this.btEditStructure.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btEditStructure, "btEditStructure");
    this.btEditStructure.Click += new EventHandler(this.OnEditStructure);
    componentResourceManager.ApplyResources((object) this.btProperties, "btProperties");
    this.btProperties.Click += new EventHandler(this.OnbtProperties_Click);
    this._btnCheckDataSet.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._btnCheckDataSet, "_btnCheckDataSet");
    this._btnCheckDataSet.Click += new EventHandler(this.On_btnCheckDataSet_Click);
    this.tbNormaCS.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.tbNormaCS, "tbNormaCS");
    this.tbNormaCS.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.tbLaunchNormaCS,
      (ToolbarItemBase) this.tbFindByNumberNCS,
      (ToolbarItemBase) this.tbFindByNameNCS,
      (ToolbarItemBase) this.FindByTextNCS
    });
    this.tbNormaCS.ShowText = true;
    this.tbNormaCS.Click += new EventHandler(this.OnNormaCS_Click);
    componentResourceManager.ApplyResources((object) this.tbLaunchNormaCS, "tbLaunchNormaCS");
    this.tbLaunchNormaCS.ShowText = true;
    this.tbLaunchNormaCS.Click += new EventHandler(this.OnNormaCS_Click);
    componentResourceManager.ApplyResources((object) this.tbFindByNumberNCS, "tbFindByNumberNCS");
    this.tbFindByNumberNCS.ShowText = true;
    this.tbFindByNumberNCS.Click += new EventHandler(this.OnFindByNumberNCS_Click);
    componentResourceManager.ApplyResources((object) this.tbFindByNameNCS, "tbFindByNameNCS");
    this.tbFindByNameNCS.ShowText = true;
    this.tbFindByNameNCS.Click += new EventHandler(this.OnFindByNameNCS_Click);
    componentResourceManager.ApplyResources((object) this.FindByTextNCS, "FindByTextNCS");
    this.FindByTextNCS.ShowText = true;
    this.FindByTextNCS.Click += new EventHandler(this.OnFindByTextNCS_Click);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "MoveUp.bmp");
    this.imageList1.Images.SetKeyName(1, "MoveDown.bmp");
    this.imageList1.Images.SetKeyName(2, "MoveFirst.bmp");
    this.imageList1.Images.SetKeyName(3, "MoveLast.bmp");
    this.imageList1.Images.SetKeyName(4, "normacs.bmp");
    this.statusStrip1.GripMargin = new Padding(0);
    this.statusStrip1.Items.AddRange(new ToolStripItem[7]
    {
      (ToolStripItem) this.sbRecNumPanel,
      (ToolStripItem) this.sbRecKey,
      (ToolStripItem) this.sbRecGuid,
      (ToolStripItem) this.sbFindId,
      (ToolStripItem) this.sbAttImage,
      (ToolStripItem) this.sbShortName,
      (ToolStripItem) this.sbLongName
    });
    componentResourceManager.ApplyResources((object) this.statusStrip1, "statusStrip1");
    this.statusStrip1.Name = "statusStrip1";
    this.statusStrip1.ShowItemToolTips = true;
    this.statusStrip1.SizingGrip = false;
    componentResourceManager.ApplyResources((object) this.sbRecNumPanel, "sbRecNumPanel");
    this.sbRecNumPanel.AutoToolTip = true;
    this.sbRecNumPanel.BorderSides = ToolStripStatusLabelBorderSides.Right;
    this.sbRecNumPanel.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.sbRecNumPanel.Name = "sbRecNumPanel";
    this.sbRecKey.BorderSides = ToolStripStatusLabelBorderSides.Right;
    this.sbRecKey.DoubleClickEnabled = true;
    this.sbRecKey.Name = "sbRecKey";
    componentResourceManager.ApplyResources((object) this.sbRecKey, "sbRecKey");
    this.sbRecKey.Click += new EventHandler(this.On_sbRecKey_DoubleClick);
    this.sbRecGuid.BorderSides = ToolStripStatusLabelBorderSides.Right;
    this.sbRecGuid.DoubleClickEnabled = true;
    this.sbRecGuid.Name = "sbRecGuid";
    componentResourceManager.ApplyResources((object) this.sbRecGuid, "sbRecGuid");
    this.sbRecGuid.DoubleClick += new EventHandler(this.On_sbRecKey_DoubleClick);
    this.sbFindId.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.sbFindId.DropDownButtonWidth = 0;
    componentResourceManager.ApplyResources((object) this.sbFindId, "sbFindId");
    this.sbFindId.Name = "sbFindId";
    this.sbFindId.ButtonClick += new EventHandler(this.On_sbFindId_Click);
    componentResourceManager.ApplyResources((object) this.sbAttImage, "sbAttImage");
    this.sbAttImage.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
    this.sbAttImage.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.sbAttImage.Name = "sbAttImage";
    componentResourceManager.ApplyResources((object) this.sbShortName, "sbShortName");
    this.sbShortName.BorderSides = ToolStripStatusLabelBorderSides.Right;
    this.sbShortName.Name = "sbShortName";
    componentResourceManager.ApplyResources((object) this.sbLongName, "sbLongName");
    this.sbLongName.BorderSides = ToolStripStatusLabelBorderSides.Right;
    this.sbLongName.Name = "sbLongName";
    this._treeBuilder.AllowFavourites = false;
    this._treeBuilder.Catalogs = new long[0];
    this._treeBuilder.Checked = new long[0];
    this._treeBuilder.TreeView = this._trv;
    this._extender.DataGridView = (DataGridView) null;
    this._extender.FilterFactory = (IGridFilterFactory) this.leftFilterFactory;
    this._extender.FilterText = "Фильтр";
    componentResourceManager.ApplyResources((object) this.menuButtonItem17, "menuButtonItem17");
    this.menuButtonItem17.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuButtonItem18, "menuButtonItem18");
    this.menuButtonItem18.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuButtonItem19, "menuButtonItem19");
    this.menuButtonItem19.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuButtonItem20, "menuButtonItem20");
    this.menuButtonItem20.ShowText = true;
    this.Controls.Add((Control) this.statusStrip1);
    this.Controls.Add((Control) this._spltContainer);
    this.Controls.Add((Control) this.toolBar1);
    this.DoubleBuffered = true;
    this.Guid = new Guid("3c867640-5326-4b43-9479-d82a8a02f876");
    this.Name = nameof (TableEditor);
    this.ShowHint = DockState.Document;
    this.ShowImageInDocumentTab = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BeforeFirstShown += new EventHandler(this.TableEditor_BeforeFirstShown);
    this._spltContainer.Panel1.ResumeLayout(false);
    this._spltContainer.Panel2.ResumeLayout(false);
    this._spltContainer.EndInit();
    this._spltContainer.ResumeLayout(false);
    ((ISupportInitialize) this._grid).EndInit();
    this._pnlBottom.ResumeLayout(false);
    this._pnlBottom.PerformLayout();
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.statusStrip1.ResumeLayout(false);
    this.statusStrip1.PerformLayout();
    this._extender.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
