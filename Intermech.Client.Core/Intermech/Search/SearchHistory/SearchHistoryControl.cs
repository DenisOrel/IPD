
// Type: Intermech.Search.SearchHistory.SearchHistoryControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using Intermech.Search.Data.Repositories;
using Intermech.Search.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Search.SearchHistory;

public sealed class SearchHistoryControl : UserControl, ISupportInitialize
{
  private const string SearchStringColumnKey = "SearchString";
  private const string SearchDateTimeColumnKey = "SearchDateTime";
  private const string UserColumnKey = "User";
  private const string SecurityLevelColumnKey = "SecurityLevel";
  private SearchHistoryItem[] _loadedSearchHistoryItems = new SearchHistoryItem[0];
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolStrip _toolStrip;
  private ToolStripComboBox _usersToolStripComboBox;
  private ToolStripLabel toolStripLabel1;
  private ToolStripLabel toolStripLabel2;
  private ToolStripLabel toolStripLabel3;
  private ToolStripLabel toolStripLabel4;
  private ToolStripLabel toolStripLabel5;
  private ToolStripTextBox _searchStringToolStripTextBox;
  protected internal iGrid _grid;
  private ToolStripDateTimePicker _startDateToolStripDateTimePicker;
  private ToolStripDateTimePicker _endDateToolStripDateTimePicker;

  public SearchHistoryControl()
  {
    this.InitializeComponent();
    this.IntializeToolStripDateTimePicker(this._startDateToolStripDateTimePicker);
    this.IntializeToolStripDateTimePicker(this._endDateToolStripDateTimePicker);
    this.InitializeGrid();
  }

  public void BeginInit()
  {
    if (this.DesignMode)
      return;
    this.InitializeUsersComboBox();
    this.ReloadSearchHistory();
  }

  public void EndInit()
  {
  }

  private void UsersToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.ReloadSearchHistory();
  }

  private void StartDateToolStripDateTimePicker_TextChanged(object sender, EventArgs e)
  {
    this.ReloadSearchHistory();
  }

  private void EndDateToolStripDateTimePicker_TextChanged(object sender, EventArgs e)
  {
    this.ReloadSearchHistory();
  }

  private void NameToolStripTextBox_TextChanged(object sender, EventArgs e) => this.UpdateGrid();

  private void InitializeUsersComboBox()
  {
    if (!(ServicesManager.GetService(typeof (IObjectRepository)) is IObjectRepository service))
      return;
    List<_Object> source = service.Find(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"));
    _Object @object = new _Object()
    {
      Caption = "Все",
      VersionID = -1
    };
    List<_Object> list = source.OrderBy<_Object, string>((System.Func<_Object, string>) (o => o.Caption)).ToList<_Object>();
    list.Insert(0, @object);
    this._usersToolStripComboBox.BeginUpdate();
    this._usersToolStripComboBox.ComboBox.DisplayMember = "Caption";
    try
    {
      this._usersToolStripComboBox.Items.Clear();
      this._usersToolStripComboBox.Items.AddRange((object[]) list.ToArray());
    }
    finally
    {
      this._usersToolStripComboBox.EndUpdate();
    }
    this._usersToolStripComboBox.SelectedItem = (object) @object;
  }

  private void IntializeToolStripDateTimePicker(ToolStripDateTimePicker toolStripDateTimePicker)
  {
    toolStripDateTimePicker.DateTimePicker.ShowCheckBox = true;
    toolStripDateTimePicker.DateTimePicker.Checked = false;
    toolStripDateTimePicker.DateTimePicker.CustomFormat = "dd.MM.yyyy hh:mm:ss";
    toolStripDateTimePicker.DateTimePicker.Format = DateTimePickerFormat.Custom;
  }

  private void InitializeGrid()
  {
    this._grid.BeginUpdate();
    try
    {
      this._grid.Cols.Add("SearchString", "Строка поиска", 250);
      this._grid.Cols.Add("SearchDateTime", "Дата", 250).SortType = iGSortType.ByValue;
      this._grid.Cols.Add("User", "Пользователь", 250);
      this._grid.Cols.Add("SecurityLevel", "Уровень доступа", 250);
    }
    finally
    {
      this._grid.EndUpdate();
    }
  }

  private void ReloadSearchHistory()
  {
    List<SearchHistoryItem> searchHistoryItemList = new List<SearchHistoryItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IGlobalIndexHelper)) is IGlobalIndexHelper customService)
      {
        foreach (DataRow row in (InternalDataCollectionBase) customService.GetQueriesHistory(sessionKeeper.Session.SessionGUID, this.GetSelectedUserVersionID(), this.GetStartDateTime(), this.GetEndDateTime()).Rows)
        {
          SearchHistoryItem searchHistoryItem = new SearchHistoryItem()
          {
            SearchDateTime = DataSetProcessor.GetDateTimeValue(row, "F_QUERY_DATE", DateTime.MinValue),
            SearchString = DataSetProcessor.GetStringValue(row, "F_QUERY_STR", (string) null),
            SecurityLevel = DataSetProcessor.GetInt32Value(row, "F_ACCESS", 0),
            UserVersionID = DataSetProcessor.GetInt64Value(row, "F_USER_ID", 0L)
          };
          searchHistoryItemList.Add(searchHistoryItem);
        }
      }
    }
    this._loadedSearchHistoryItems = searchHistoryItemList.ToArray();
    this.UpdateGrid();
  }

  private long GetSelectedUserVersionID()
  {
    return !(this._usersToolStripComboBox.SelectedItem is _Object selectedItem) ? -1L : selectedItem.VersionID;
  }

  private DateTime GetStartDateTime()
  {
    return !this._startDateToolStripDateTimePicker.DateTimePicker.Checked ? DateTime.MinValue : this._startDateToolStripDateTimePicker.DateTimePicker.Value;
  }

  private DateTime GetEndDateTime()
  {
    return !this._endDateToolStripDateTimePicker.DateTimePicker.Checked ? DateTime.MaxValue : this._endDateToolStripDateTimePicker.DateTimePicker.Value;
  }

  private void UpdateGrid()
  {
    SearchHistoryItem[] searchHistoryItemArray = this._loadedSearchHistoryItems;
    if (!string.IsNullOrEmpty(this._searchStringToolStripTextBox.Text))
      searchHistoryItemArray = ((IEnumerable<SearchHistoryItem>) this._loadedSearchHistoryItems).Where<SearchHistoryItem>((System.Func<SearchHistoryItem, bool>) (o => o.SearchString != null && o.SearchString.Contains(this._searchStringToolStripTextBox.Text))).ToArray<SearchHistoryItem>();
    this._grid.BeginUpdate();
    try
    {
      this._grid.Rows.Clear();
      foreach (SearchHistoryItem searchHistoryItem in searchHistoryItemArray)
      {
        iGRow iGrow = this._grid.Rows.Add();
        iGrow.Cells["SearchString"].Value = (object) searchHistoryItem.SearchString;
        iGrow.Cells["SearchDateTime"].Value = (object) searchHistoryItem.SearchDateTime;
        iGrow.Cells["User"].Value = (object) this.GetUserName(searchHistoryItem.UserVersionID);
        iGrow.Cells["SecurityLevel"].Value = (object) this.GetSecurityLevelName(searchHistoryItem.SecurityLevel);
      }
    }
    finally
    {
      this._grid.EndUpdate();
    }
  }

  private string GetUserName(long userVersionID)
  {
    return !(CacheManager.Cache("UserNamesCache") is IUserNamesCache userNamesCache) ? (string) null : userNamesCache.GetUserName(userVersionID);
  }

  private string GetSecurityLevelName(int securityLevel)
  {
    return SecurityLevelHolder.GetDescriptionBySecurityLevel(securityLevel);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._toolStrip = new ToolStrip();
    this.toolStripLabel1 = new ToolStripLabel();
    this._usersToolStripComboBox = new ToolStripComboBox();
    this.toolStripLabel2 = new ToolStripLabel();
    this.toolStripLabel3 = new ToolStripLabel();
    this._startDateToolStripDateTimePicker = new ToolStripDateTimePicker();
    this.toolStripLabel4 = new ToolStripLabel();
    this._endDateToolStripDateTimePicker = new ToolStripDateTimePicker();
    this.toolStripLabel5 = new ToolStripLabel();
    this._searchStringToolStripTextBox = new ToolStripTextBox();
    this._grid = new iGrid();
    this._toolStrip.SuspendLayout();
    ((ISupportInitialize) this._grid).BeginInit();
    this.SuspendLayout();
    this._toolStrip.Items.AddRange(new ToolStripItem[9]
    {
      (ToolStripItem) this.toolStripLabel1,
      (ToolStripItem) this._usersToolStripComboBox,
      (ToolStripItem) this.toolStripLabel2,
      (ToolStripItem) this.toolStripLabel3,
      (ToolStripItem) this._startDateToolStripDateTimePicker,
      (ToolStripItem) this.toolStripLabel4,
      (ToolStripItem) this._endDateToolStripDateTimePicker,
      (ToolStripItem) this.toolStripLabel5,
      (ToolStripItem) this._searchStringToolStripTextBox
    });
    this._toolStrip.Location = new Point(0, 0);
    this._toolStrip.Name = "_toolStrip";
    this._toolStrip.Size = new Size(1117, 26);
    this._toolStrip.TabIndex = 0;
    this._toolStrip.Text = "toolStrip1";
    this.toolStripLabel1.Name = "toolStripLabel1";
    this.toolStripLabel1.Size = new Size(90, 23);
    this.toolStripLabel1.Text = "Пользователь: ";
    this._usersToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._usersToolStripComboBox.Name = "_usersToolStripComboBox";
    this._usersToolStripComboBox.Size = new Size(200, 26);
    this._usersToolStripComboBox.SelectedIndexChanged += new EventHandler(this.UsersToolStripComboBox_SelectedIndexChanged);
    this.toolStripLabel2.Name = "toolStripLabel2";
    this.toolStripLabel2.Size = new Size(0, 23);
    this.toolStripLabel3.Name = "toolStripLabel3";
    this.toolStripLabel3.Size = new Size(24, 23);
    this.toolStripLabel3.Text = "От:";
    this._startDateToolStripDateTimePicker.Name = "_startDateToolStripDateTimePicker";
    this._startDateToolStripDateTimePicker.Size = new Size(200, 23);
    this._startDateToolStripDateTimePicker.Text = "12 октября 2016 г.";
    this._startDateToolStripDateTimePicker.TextChanged += new EventHandler(this.StartDateToolStripDateTimePicker_TextChanged);
    this.toolStripLabel4.Name = "toolStripLabel4";
    this.toolStripLabel4.Size = new Size(25, 23);
    this.toolStripLabel4.Text = "До:";
    this._endDateToolStripDateTimePicker.Name = "_endDateToolStripDateTimePicker";
    this._endDateToolStripDateTimePicker.Size = new Size(200, 23);
    this._endDateToolStripDateTimePicker.Text = "12 октября 2016 г.";
    this._endDateToolStripDateTimePicker.TextChanged += new EventHandler(this.EndDateToolStripDateTimePicker_TextChanged);
    this.toolStripLabel5.Name = "toolStripLabel5";
    this.toolStripLabel5.Size = new Size(93, 23);
    this.toolStripLabel5.Text = "Наименование:";
    this._searchStringToolStripTextBox.Name = "_searchStringToolStripTextBox";
    this._searchStringToolStripTextBox.Size = new Size(250, 26);
    this._searchStringToolStripTextBox.TextChanged += new EventHandler(this.NameToolStripTextBox_TextChanged);
    this._grid.AllowDrop = true;
    this._grid.AutoWidthColMode = iGAutoWidthColMode.Cells;
    this._grid.BackColorEvenRows = SystemColors.Window;
    this._grid.BackColorOddRows = SystemColors.Window;
    this._grid.Cursor = Cursors.Default;
    this._grid.DefaultAutoGroupRow.Height = 25;
    this._grid.DefaultCol.Width = 120;
    this._grid.DefaultRow.Height = 25;
    this._grid.DefaultRow.NormalCellHeight = 25;
    this._grid.Dock = DockStyle.Fill;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = "Перетащите заголовок колонки в эту область для группировки по значениям этой колонки";
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = 19;
    this._grid.HighlightBackColorNoFocus = SystemColors.Highlight;
    this._grid.HighlightForeColorNoFocus = SystemColors.HighlightText;
    this._grid.HotTracking = false;
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._grid.Location = new Point(0, 26);
    this._grid.Name = "_grid";
    this._grid.PageCapacity = 500;
    this._grid.PressedMouseMoveMode = iGPressedMouseMoveMode.Normal;
    this._grid.ProcessTab = false;
    this._grid.ReadOnly = true;
    this._grid.RowMode = true;
    this._grid.RowModeHasCurCell = true;
    this._grid.RowTextStartColNear = 211;
    this._grid.SelectionMode = iGSelectionMode.MultiExtended;
    this._grid.ShowControlsInAllCells = false;
    this._grid.Size = new Size(1117, 335);
    this._grid.TabIndex = 1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._grid);
    this.Controls.Add((Control) this._toolStrip);
    this.Name = nameof (SearchHistoryControl);
    this.Size = new Size(1117, 361);
    this._toolStrip.ResumeLayout(false);
    this._toolStrip.PerformLayout();
    ((ISupportInitialize) this._grid).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
