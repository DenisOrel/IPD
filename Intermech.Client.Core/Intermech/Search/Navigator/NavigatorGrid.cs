
// Type: Intermech.Search.Navigator.NavigatorGrid
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Search.iGrid;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Search.Navigator;

public sealed class NavigatorGrid : UserControl, ISupportInitialize
{
  private const string IconColumnKey = "IconColumn";
  private const int IconColumnWidth = 50;
  private object _dataSource;
  private bool _showIconColumn;
  private List<object> _selectedItems = new List<object>();
  private LazyService<ICategoryTypeIconService> _categoryTypeIconService = new LazyService<ICategoryTypeIconService>();
  private NavigatorCellFeature _navigatorCellFeature = new NavigatorCellFeature();
  private NavigatorColorSchemesFeature _navigatorColorSchemesFeature = new NavigatorColorSchemesFeature();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected internal TenTec.Windows.iGridLib.iGrid _grid;

  public NavigatorGrid() => this.InitializeComponent();

  public event EventHandler SelectionChanged;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public object DataSource
  {
    get => this._dataSource;
    set
    {
      if (this._dataSource == value)
        return;
      if (this._dataSource is IBindingList)
        ((IBindingList) this._dataSource).ListChanged -= new ListChangedEventHandler(this.DataSource_ListChanged);
      this._dataSource = value;
      if (this._dataSource is IBindingList)
        ((IBindingList) this._dataSource).ListChanged += new ListChangedEventHandler(this.DataSource_ListChanged);
      this.UpdateGrid();
    }
  }

  public bool ShowIconColumn
  {
    get => this._showIconColumn;
    set
    {
      if (this._showIconColumn == value)
        return;
      this._showIconColumn = value;
      this.UpdateGrid();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public object SelectedItem
  {
    get => ((IEnumerable<object>) this.SelectedItems).FirstOrDefault<object>();
    set
    {
      object[] objArray;
      if (value == null)
        objArray = (object[]) null;
      else
        objArray = new object[1]{ value };
      this.SelectedItems = objArray;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public object[] SelectedItems
  {
    get => this._selectedItems.ToArray();
    set
    {
      this._selectedItems.Clear();
      if (value != null)
        this._selectedItems.AddRange((IEnumerable<object>) value);
      this._grid.SelectionChanged -= new EventHandler(this.Grid_SelectionChanged);
      try
      {
        foreach (iGRow row in (IEnumerable) this._grid.Rows)
        {
          if (row.Tag != null && this._selectedItems.Contains(row.Tag))
            row.SetSelectedForAllCells(true);
        }
      }
      finally
      {
        this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
      }
      this.OnSelectionChanged();
    }
  }

  public NodeColumnCollection GetNodeColumns()
  {
    NodeColumnCollection nodeColumns = new NodeColumnCollection();
    foreach (iGCol iGcol in (IEnumerable<iGCol>) this._grid.Cols.Cast<iGCol>().OrderBy<iGCol, int>((Func<iGCol, int>) (o => o.Order)))
    {
      if (iGcol.Tag is NodeColumn)
      {
        NodeColumn tag = (NodeColumn) iGcol.Tag;
        nodeColumns.Add(tag);
      }
    }
    return nodeColumns;
  }

  public void SetNodeColumns(NodeColumnCollection nodeColumns)
  {
    this._grid.Cols.Clear();
    iGCol iGcol1 = this._grid.Cols.Add();
    iGcol1.AllowGrouping = false;
    iGcol1.AllowMoving = false;
    iGcol1.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
    iGcol1.CellStyle.ReadOnly = iGBool.True;
    iGcol1.Key = "IconColumn";
    iGcol1.SortType = iGSortType.None;
    iGcol1.Width = 50;
    foreach (NodeColumn nodeColumn in (List<NodeColumn>) nodeColumns)
    {
      iGCol iGcol2 = this._grid.Cols.Add();
      iGcol2.AllowGrouping = false;
      iGcol2.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
      iGcol2.SortOrder = nodeColumn.SortOrder == NodeColumnSortOrder.Ascending ? iGSortOrder.Ascending : (nodeColumn.SortOrder == NodeColumnSortOrder.Descending ? iGSortOrder.Descending : iGSortOrder.None);
      iGcol2.Tag = (object) nodeColumn;
      iGcol2.Text = (object) nodeColumn.Caption;
      iGcol2.SortType = iGSortType.None;
      iGcol2.Width = nodeColumn.Width;
    }
    this.UpdateGrid();
  }

  public void BeginInit()
  {
  }

  public void EndInit()
  {
  }

  private void Grid_CustomDrawCellBackground(object sender, iGCustomDrawCellEventArgs e)
  {
    object tag = this._grid.Rows[e.RowIndex].Tag;
    _Object @object = (_Object) null;
    if (tag is _Object)
      @object = (_Object) tag;
    else if (tag is CompositionPart)
      @object = ((RelationObjectBase) tag).Object;
    if (@object == null)
      return;
    NavGradientBrush navGradientBrush = this._navigatorColorSchemesFeature.GetNavGradientBrush(@object, e.Bounds);
    if (navGradientBrush == null)
      return;
    using (navGradientBrush)
      e.Graphics.FillRectangle(navGradientBrush.Brush, e.Bounds);
  }

  private void Grid_SelectionChanged(object sender, EventArgs e)
  {
    this._selectedItems.Clear();
    foreach (iGCell selectedCell in this._grid.SelectedCells)
      this._selectedItems.Add(selectedCell.Row.Tag);
    this.OnSelectionChanged();
  }

  private void DataSource_ListChanged(object sender, ListChangedEventArgs e) => this.UpdateGrid();

  private void UpdateGrid()
  {
    object[] selectedItems = this.SelectedItems;
    this._grid.BeginUpdate();
    try
    {
      this._grid.Rows.Clear();
      if (this._dataSource is IEnumerable)
      {
        foreach (object obj in (IEnumerable) this._dataSource)
        {
          iGRow iGrow = this._grid.Rows.Add();
          iGrow.Tag = obj;
          _Object @object = (_Object) null;
          Relation relation = (Relation) null;
          if (obj is _Object)
            @object = (_Object) obj;
          else if (obj is CompositionPart)
            @object = ((RelationObjectBase) obj).Object;
          if (obj is Relation)
            relation = (Relation) obj;
          else if (obj is CompositionPart)
            relation = ((RelationObjectBase) obj).Relation;
          foreach (iGCell cell in (IEnumerable) iGrow.Cells)
          {
            if (cell.Col.Key == "IconColumn")
            {
              if (@object != null)
              {
                cell.ImageList = this._categoryTypeIconService.Value.ImageList;
                cell.ImageIndex = this._categoryTypeIconService.Value.IndexOf(4, @object.TypeID);
              }
            }
            else if (cell.Col.Tag is NodeColumn)
            {
              NodeColumn tag = (NodeColumn) cell.Col.Tag;
              if (tag.Attribute != null)
              {
                AttributeSourceTypes attributeSourceTypes = tag.AttrSource;
                if (attributeSourceTypes == AttributeSourceTypes.Auto && AttributeTypeHelper.IsSystemAttributeTypeID(tag.Attribute.AttributeID))
                  attributeSourceTypes = ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) tag.Attribute.AttributeID);
                _Attribute attribute = (_Attribute) null;
                switch (attributeSourceTypes)
                {
                  case AttributeSourceTypes.Object:
                    attribute = @object.Attributes.GetAttribute(tag.Attribute.AttributeID);
                    break;
                  case AttributeSourceTypes.Relation:
                    attribute = relation.Attributes.GetAttribute(tag.Attribute.AttributeID);
                    break;
                }
                if (attribute != null)
                  cell.ReadOnly = attribute.IsReadOnly.HasValue ? (attribute.IsReadOnly.Value ? iGBool.True : iGBool.False) : iGBool.True;
                object cellValue = (object) null;
                this._navigatorCellFeature.TryGetCellValue(@object, relation, tag, out cellValue);
                cell.Value = cellValue;
              }
            }
          }
        }
      }
    }
    finally
    {
      this._grid.EndUpdate();
    }
    this.SelectedItems = selectedItems;
  }

  private void OnSelectionChanged()
  {
    EventHandler selectionChanged = this.SelectionChanged;
    if (selectionChanged == null)
      return;
    selectionChanged((object) this, EventArgs.Empty);
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
    this._grid = new TenTec.Windows.iGridLib.iGrid();
    ((ISupportInitialize) this._grid).BeginInit();
    this.SuspendLayout();
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
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = 19;
    this._grid.HighlightBackColorNoFocus = SystemColors.Highlight;
    this._grid.HighlightForeColorNoFocus = SystemColors.HighlightText;
    this._grid.HotTracking = false;
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._grid.Location = new Point(0, 0);
    this._grid.Name = "_grid";
    this._grid.PageCapacity = 500;
    this._grid.PressedMouseMoveMode = iGPressedMouseMoveMode.Normal;
    this._grid.ProcessTab = false;
    this._grid.RowMode = true;
    this._grid.RowModeHasCurCell = true;
    this._grid.RowTextStartColNear = 211;
    this._grid.SelectionMode = iGSelectionMode.MultiExtended;
    this._grid.ShowControlsInAllCells = false;
    this._grid.Size = new Size(562, 376);
    this._grid.TabIndex = 1;
    this._grid.CustomDrawCellBackground += new iGCustomDrawCellEventHandler(this.Grid_CustomDrawCellBackground);
    this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._grid);
    this.Name = nameof (NavigatorGrid);
    this.Size = new Size(562, 376);
    ((ISupportInitialize) this._grid).EndInit();
    this.ResumeLayout(false);
  }
}
