
// Type: Intermech.Search.GroupAttributesChanging.ObjectBlankListControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Search.iGrid;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class ObjectBlankListControl : UserControl, ISupportInitialize
{
  private const string IconColumnKey = "Icon";
  private const int IconColumnWidth = 50;
  private const string StatusesColumnKey = "Statuses";
  private const int StatusesColumnWidth = 200;
  private const int StatusesIconMargin = 5;
  private BindingList<ObjectBlank> _objects = new BindingList<ObjectBlank>();
  private NodeColumnCollection _supportedColumns = new NodeColumnCollection();
  private ObjectBlank[] _selectedObjects = new ObjectBlank[0];
  private NodeColumnCollection _columns = new NodeColumnCollection();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TenTec.Windows.iGridLib.iGrid _grid;
  private ImageList _imageList;
  private ToolTip _toolTip;

  public ObjectBlankListControl() => this.InitializeComponent();

  public event EventHandler SelectionChanged;

  public event EventHandler ColumnsChanged;

  public event EventHandler CurrentColumnChanged;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public BindingList<ObjectBlank> Objects
  {
    get => this._objects;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this._objects == value)
        return;
      this._objects.ListChanged -= new ListChangedEventHandler(this.Objects_ListChanged);
      this._objects = value;
      this._objects.ListChanged += new ListChangedEventHandler(this.Objects_ListChanged);
      if (this._columns.Count == 0)
        this.SetDefaultColumns();
      this.CreateSupportedColumns();
      this.UpdateGrid();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ObjectBlank SelectedObject
  {
    get => ((IEnumerable<ObjectBlank>) this.SelectedObjects).FirstOrDefault<ObjectBlank>();
    set
    {
      this.SelectedObjects = new ObjectBlank[1]{ value };
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ObjectBlank[] SelectedObjects
  {
    get => this._selectedObjects;
    set
    {
      if (this._selectedObjects == value)
        return;
      this._selectedObjects = value != null ? ((IEnumerable<ObjectBlank>) value).Where<ObjectBlank>((Func<ObjectBlank, bool>) (o => o != null)).ToArray<ObjectBlank>() : new ObjectBlank[0];
      this._grid.SelectionChanged -= new EventHandler(this.Grid_SelectionChanged);
      try
      {
        foreach (iGRow row in (IEnumerable) this._grid.Rows)
        {
          if (row.Tag is ObjectBlank && ((IEnumerable<ObjectBlank>) this._selectedObjects).Contains<ObjectBlank>((ObjectBlank) row.Tag))
            row.SetSelectedForAllCells(true);
          else
            row.SetSelectedForAllCells(false);
        }
      }
      finally
      {
        this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
      }
      this.OnSelectionChanged();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeColumn CurrentColumn { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool TrySetCommonEditableAttributesAsDefault { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeColumnCollection Columns
  {
    get => (NodeColumnCollection) this._columns.Clone();
    set
    {
      if (this._columns == value)
        return;
      this._columns = value != null ? (NodeColumnCollection) value.Clone() : new NodeColumnCollection();
      try
      {
        this._grid.Cols.Clear();
        iGCol iGcol1 = this._grid.Cols.Add();
        iGcol1.AllowGrouping = false;
        iGcol1.AllowMoving = false;
        iGcol1.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
        iGcol1.CellStyle.ReadOnly = iGBool.True;
        iGcol1.Key = "Icon";
        iGcol1.SortType = iGSortType.None;
        iGcol1.Width = 50;
        iGCol iGcol2 = this._grid.Cols.Add();
        iGcol2.AllowGrouping = false;
        iGcol2.AllowMoving = false;
        iGcol2.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Foreground | iGCustomDrawFlags.Background;
        iGcol2.CellStyle.ReadOnly = iGBool.True;
        iGcol2.Key = "Statuses";
        iGcol2.Text = (object) "Статусы";
        iGcol2.SortType = iGSortType.None;
        iGcol2.Width = 200;
        foreach (NodeColumn column in (List<NodeColumn>) this._columns)
        {
          iGCol iGcol3 = this._grid.Cols.Add();
          iGcol3.AllowGrouping = false;
          iGcol3.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
          iGcol3.Tag = (object) column;
          iGcol3.Text = (object) column.Caption;
          iGcol3.SortType = iGSortType.ByValue;
          iGcol3.Width = column.Width;
        }
        foreach (NodeColumn nodeColumn in (IEnumerable<NodeColumn>) this._columns.Where<NodeColumn>((Func<NodeColumn, bool>) (o => o.SortOrder != 0)).OrderBy<NodeColumn, int>((Func<NodeColumn, int>) (o => o.SortIndex)))
          this._grid.SortObject.Add(this.GetGridColumnForNodeColumn(nodeColumn).Index, nodeColumn.SortOrder == NodeColumnSortOrder.Ascending ? iGSortOrder.Ascending : iGSortOrder.Descending);
      }
      finally
      {
        this.UpdateGrid();
      }
      this.OnColumnsChanged();
    }
  }

  public void ChangeColumns()
  {
    INavigatorClientService navigatorClientService = ServiceLocator.Get<INavigatorClientService>();
    NodeColumnCollection defaultColumns = this.GetDefaultColumns();
    if (this.TrySetCommonEditableAttributesAsDefault)
      defaultColumns = this.GetCommonEditableColumns();
    if (defaultColumns.Count == 0)
      defaultColumns = this.GetDefaultColumns();
    this.Columns = navigatorClientService.ChangeColumns(this.Columns, this._supportedColumns, defaultColumns);
  }

  public IEnumerable<ObjectBlank> GetNextAfterLastSelectedObjectThenLastSelectObject()
  {
    int i;
    if (this._selectedObjects.Length != 0)
    {
      iGRow row = this.GetRowForObject(((IEnumerable<ObjectBlank>) this._selectedObjects).Last<ObjectBlank>());
      if (row != null)
      {
        for (i = row.Index + 1; i <= this._grid.Rows.Count - 1; ++i)
        {
          if (this._grid.Rows[i].Tag is ObjectBlank)
            yield return this._grid.Rows[i].Tag as ObjectBlank;
        }
        for (i = 0; i < row.Index; ++i)
        {
          if (this._grid.Rows[i].Tag is ObjectBlank)
            yield return this._grid.Rows[i].Tag as ObjectBlank;
        }
        if (row.Tag is ObjectBlank)
          yield return (ObjectBlank) row.Tag;
      }
      row = (iGRow) null;
    }
    else
    {
      for (i = 0; i < this._grid.Rows.Count; ++i)
      {
        if (this._grid.Rows[i].Tag is ObjectBlank)
          yield return this._grid.Rows[i].Tag as ObjectBlank;
      }
    }
  }

  public object CreateMemento()
  {
    return (object) new ObjectBlankListControl.ObjectBlankListMemento()
    {
      Columns = this.Columns
    };
  }

  public void SetMemento(object memento)
  {
    this.Columns = memento is ObjectBlankListControl.ObjectBlankListMemento ? ((ObjectBlankListControl.ObjectBlankListMemento) memento).Columns : throw new ArgumentException();
  }

  void ISupportInitialize.BeginInit()
  {
  }

  void ISupportInitialize.EndInit()
  {
  }

  private void Objects_ListChanged(object sender, ListChangedEventArgs e) => this.UpdateGrid();

  private void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
  {
    ObjectBlank objectForRow = this.GetObjectForRow(e.RowIndex);
    if (objectForRow == null)
      return;
    NodeColumn columnForGridColumn = this.GetNodeColumnForGridColumn(e.ColIndex);
    if (columnForGridColumn == null || columnForGridColumn.Attribute == null)
      return;
    objectForRow.Attributes[columnForGridColumn.Attribute.AttributeID].Value = (object) (this._grid.Cells[e.RowIndex, e.ColIndex].Value as string);
  }

  private void Grid_AfterContentsSorted(object sender, EventArgs e)
  {
    this.CreateNodeColumnsFromGridColumns();
  }

  private void Grid_CellMouseLeave(object sender, iGCellMouseEnterLeaveEventArgs e)
  {
  }

  private void Grid_CellMouseMove(object sender, iGCellMouseMoveEventArgs e)
  {
    if (this._grid.Cols[e.ColIndex].Key == "Statuses")
    {
      if (this._grid.Cells[e.RowIndex, e.ColIndex].Value is ObjectBlankListControl.ObjectBlankStatusIcon[] source)
      {
        Point cellMousePosition = new Point(e.MousePos.X - e.Bounds.X, e.MousePos.Y - e.Bounds.Y);
        ObjectBlankListControl.ObjectBlankStatusIcon objectBlankStatusIcon = ((IEnumerable<ObjectBlankListControl.ObjectBlankStatusIcon>) source).FirstOrDefault<ObjectBlankListControl.ObjectBlankStatusIcon>((Func<ObjectBlankListControl.ObjectBlankStatusIcon, bool>) (o => o.Bounds.Contains(cellMousePosition)));
        if (objectBlankStatusIcon != null)
        {
          if (!(objectBlankStatusIcon.ToolTip != this._toolTip.ToolTipTitle))
            return;
          this._toolTip.Show(objectBlankStatusIcon.ToolTip, (IWin32Window) this, e.MousePos);
        }
        else
          this._toolTip.Hide((IWin32Window) this);
      }
      else
        this._toolTip.Hide((IWin32Window) this);
    }
    else
      this._toolTip.Hide((IWin32Window) this);
  }

  private void Grid_ColHdrEndDrag(object sender, iGColHdrEndDragEventArgs e)
  {
    this.CreateNodeColumnsFromGridColumns();
  }

  private void Grid_ColWidthEndChange(object sender, iGColWidthEventArgs e)
  {
    this.CreateNodeColumnsFromGridColumns();
  }

  private void Grid_CurCellChanged(object sender, EventArgs e)
  {
    if (this._grid.CurCell != null)
      this.CurrentColumn = this.GetNodeColumnForGridColumn(this._grid.CurCell.ColIndex);
    this.OnCurrentColumnChanged();
  }

  private void Grid_CustomDrawCellBackground(object sender, iGCustomDrawCellEventArgs e)
  {
    ObjectBlank objectForRow = this.GetObjectForRow(e.RowIndex);
    if (objectForRow == null || ObjectHelper.IsUnknownObjectVersionID(objectForRow.CheckedOutBy))
      return;
    using (NavGradientBrush checkedOutBrush = ServiceLocator.Get<INavigatorClientService>().GetCheckedOutBrush(objectForRow.CheckedOutBy, e.Bounds))
    {
      if (checkedOutBrush == null)
        return;
      e.Graphics.FillRectangle(checkedOutBrush.Brush, e.Bounds);
    }
  }

  private void Grid_CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
  {
    if (!(this._grid.Cols[e.ColIndex].Key == "Statuses") || !(this._grid.Cells[e.RowIndex, e.ColIndex].Value is ObjectBlankListControl.ObjectBlankStatusIcon[] objectBlankStatusIconArray))
      return;
    for (int index = 0; index < objectBlankStatusIconArray.Length; ++index)
    {
      ObjectBlankListControl.ObjectBlankStatusIcon objectBlankStatusIcon = objectBlankStatusIconArray[index];
      using (Image image1 = this._imageList.Images[objectBlankStatusIcon.ImageName])
      {
        Graphics graphics = e.Graphics;
        Image image2 = image1;
        int x1 = e.Bounds.X;
        Rectangle bounds1 = objectBlankStatusIcon.Bounds;
        int x2 = bounds1.X;
        int x3 = x1 + x2;
        bounds1 = e.Bounds;
        int y1 = bounds1.Y;
        Rectangle bounds2 = objectBlankStatusIcon.Bounds;
        int y2 = bounds2.Y;
        int y3 = y1 + y2;
        bounds2 = objectBlankStatusIcon.Bounds;
        int width = bounds2.Width;
        bounds2 = objectBlankStatusIcon.Bounds;
        int height = bounds2.Height;
        graphics.DrawImage(image2, x3, y3, width, height);
      }
    }
  }

  private void Grid_RequestEdit(object sender, iGRequestEditEventArgs e)
  {
    ObjectBlank objectForRow = this.GetObjectForRow(e.RowIndex);
    if (objectForRow == null)
      return;
    NodeColumn columnForGridColumn = this.GetNodeColumnForGridColumn(e.ColIndex);
    if (columnForGridColumn == null || columnForGridColumn.Attribute == null)
      return;
    e.DoDefault = objectForRow.Attributes[columnForGridColumn.Attribute.AttributeID].IsEditable;
  }

  private void Grid_SelectionChanged(object sender, EventArgs e)
  {
    this._selectedObjects = this._grid.Rows.Cast<iGRow>().Where<iGRow>((Func<iGRow, bool>) (o => o.IsAnyCellSelected() && o.Tag is ObjectBlank)).Select<iGRow, ObjectBlank>((Func<iGRow, ObjectBlank>) (o => (ObjectBlank) o.Tag)).ToArray<ObjectBlank>();
    this.OnSelectionChanged();
  }

  private void UpdateGrid()
  {
    ObjectBlank[] selectedObjects = this.SelectedObjects;
    this._grid.BeginUpdate();
    try
    {
      this._grid.Rows.Clear();
      this._selectedObjects = new ObjectBlank[0];
      ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
      foreach (ObjectBlank objectBlank in (Collection<ObjectBlank>) this._objects)
      {
        iGRow iGrow = this._grid.Rows.Add();
        iGrow.Tag = (object) objectBlank;
        foreach (iGCell cell in (IEnumerable) iGrow.Cells)
        {
          if (cell.Col.Key == "Icon")
          {
            cell.ImageList = categoryTypeIconService.ImageList;
            cell.ImageIndex = categoryTypeIconService.IndexOf(4, objectBlank.ObjectTypeID);
          }
          else if (cell.Col.Key == "Statuses")
          {
            List<ObjectBlankListControl.ObjectBlankStatusIcon> objectBlankStatusIconList = new List<ObjectBlankListControl.ObjectBlankStatusIcon>();
            int width = this._imageList.ImageSize.Width;
            Size imageSize = this._imageList.ImageSize;
            int num1;
            if (imageSize.Height >= cell.Bounds.Height)
            {
              num1 = cell.Bounds.Height - 2;
            }
            else
            {
              imageSize = this._imageList.ImageSize;
              num1 = imageSize.Height;
            }
            int height = num1;
            int x = 5;
            int y = (cell.Bounds.Height - height) / 2;
            List<Image> imageList = new List<Image>();
            if (objectBlank.Statuses.HasFlag((Enum) ObjectBlankStatuses.Copy))
            {
              objectBlankStatusIconList.Add(new ObjectBlankListControl.ObjectBlankStatusIcon("Copy.png", ObjectBlankStatuses.Copy.GetDescription<ObjectBlankStatuses>(), new Rectangle(x, y, width, height)));
              int num2 = x;
              imageSize = this._imageList.ImageSize;
              int num3 = imageSize.Width + 5;
              x = num2 + num3;
            }
            if (objectBlank.Statuses.HasFlag((Enum) ObjectBlankStatuses.Error))
            {
              objectBlankStatusIconList.Add(new ObjectBlankListControl.ObjectBlankStatusIcon("Error.png", ObjectBlankStatuses.Error.GetDescription<ObjectBlankStatuses>(), new Rectangle(x, y, width, height)));
              int num4 = x;
              imageSize = this._imageList.ImageSize;
              int num5 = imageSize.Width + 5;
              x = num4 + num5;
            }
            if (objectBlank.Statuses.HasFlag((Enum) ObjectBlankStatuses.Instance))
            {
              objectBlankStatusIconList.Add(new ObjectBlankListControl.ObjectBlankStatusIcon("Instance.png", ObjectBlankStatuses.Instance.GetDescription<ObjectBlankStatuses>(), new Rectangle(x, y, width, height)));
              int num6 = x;
              imageSize = this._imageList.ImageSize;
              int num7 = imageSize.Width + 5;
              x = num6 + num7;
            }
            if (objectBlank.Statuses.HasFlag((Enum) ObjectBlankStatuses.Sussess))
              objectBlankStatusIconList.Add(new ObjectBlankListControl.ObjectBlankStatusIcon("Success.png", ObjectBlankStatuses.Sussess.GetDescription<ObjectBlankStatuses>(), new Rectangle(x, y, width, height)));
            cell.Value = (object) objectBlankStatusIconList.ToArray();
          }
          else if (cell.Col.Tag is NodeColumn)
          {
            NodeColumn tag = (NodeColumn) cell.Col.Tag;
            if (tag.Attribute != null)
            {
              if (tag.Attribute.AttributeID == -2)
              {
                cell.ForeColor = SystemColors.GrayText;
                cell.ReadOnly = iGBool.True;
                cell.Value = (object) objectBlank.ObjectVersionID;
              }
              else
              {
                AttributeBlank attribute = objectBlank.Attributes[tag.Attribute.AttributeID];
                if (attribute != null)
                {
                  if (attribute.IsReadOnly)
                    cell.ForeColor = SystemColors.GrayText;
                  if (attribute.IsChanged)
                    cell.ForeColor = Color.DarkRed;
                  cell.ReadOnly = attribute.IsReadOnly ? iGBool.True : iGBool.False;
                  INodeColumnTransform defaultTransform = Holder.ColumnSchemes.GetDefaultTransform(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) attribute.AttributeTypeID);
                  if (defaultTransform != null)
                  {
                    try
                    {
                      cell.Value = defaultTransform.Apply(attribute.Value, tag, (object) null, (object[]) null);
                    }
                    catch
                    {
                      cell.Value = attribute.Value;
                    }
                  }
                  else
                    cell.Value = attribute.Value;
                }
                else
                  cell.ReadOnly = iGBool.True;
              }
            }
            else
              cell.ReadOnly = iGBool.True;
          }
        }
      }
    }
    finally
    {
      this._grid.EndUpdate();
      this._grid.Sort();
      this.SelectedObjects = selectedObjects;
    }
  }

  private void SetDefaultColumns() => this.Columns = this.GetDefaultColumns();

  private NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = new NodeColumnCollection();
    NodeColumn versionIdNodeColummn = this.CreateObjectVersionIDNodeColummn();
    versionIdNodeColummn.SortIndex = 0;
    versionIdNodeColummn.SortOrder = NodeColumnSortOrder.Ascending;
    defaultColumns.Add(versionIdNodeColummn);
    NodeColumn nodeColumn1 = new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) -50, typeof (string), FieldTypes.ftString, ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.CAPTION));
    defaultColumns.Add(nodeColumn1);
    int[] allAttributes = this.GetAllAttributes();
    if (((IEnumerable<int>) allAttributes).Contains<int>(Intermech.Search.Constants.DesignationAttributeTypeID))
    {
      NodeColumn nodeColumn2 = new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) Intermech.Search.Constants.DesignationAttributeTypeID, typeof (string), FieldTypes.ftString, MetaDataHelper.GetAttributeTypeName(Intermech.Search.Constants.DesignationAttributeTypeID));
      defaultColumns.Add(nodeColumn2);
    }
    if (((IEnumerable<int>) allAttributes).Contains<int>(Intermech.Search.Constants.NameAttributeTypeID))
    {
      NodeColumn nodeColumn3 = new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) Intermech.Search.Constants.NameAttributeTypeID, typeof (string), FieldTypes.ftString, MetaDataHelper.GetAttributeTypeName(Intermech.Search.Constants.NameAttributeTypeID));
      defaultColumns.Add(nodeColumn3);
    }
    return defaultColumns;
  }

  private int[] GetAllAttributes()
  {
    List<int> source = new List<int>();
    foreach (ObjectBlank objectBlank in (Collection<ObjectBlank>) this._objects)
      source.AddRange(objectBlank.Attributes.Select<AttributeBlank, int>((Func<AttributeBlank, int>) (o => o.AttributeTypeID)));
    return source.Distinct<int>().ToArray<int>();
  }

  private NodeColumnCollection GetCommonEditableColumns()
  {
    NodeColumnCollection commonEditableColumns = new NodeColumnCollection();
    commonEditableColumns.Add(this.CreateObjectVersionIDNodeColummn());
    commonEditableColumns.AddRange((IEnumerable<NodeColumn>) this.CreateColumns(this.GetCommonEditableAttributes()));
    return commonEditableColumns;
  }

  private int[] GetCommonEditableAttributes()
  {
    List<AttributeBlank> source = new List<AttributeBlank>();
    foreach (ObjectBlank objectBlank in (Collection<ObjectBlank>) this._objects)
      source.AddRange((IEnumerable<AttributeBlank>) objectBlank.Attributes);
    return source.GroupBy<AttributeBlank, int>((Func<AttributeBlank, int>) (o => o.AttributeTypeID)).Where<IGrouping<int, AttributeBlank>>((Func<IGrouping<int, AttributeBlank>, bool>) (o => o.All<AttributeBlank>((Func<AttributeBlank, bool>) (oo => oo.IsEditable)))).Select<IGrouping<int, AttributeBlank>, int>((Func<IGrouping<int, AttributeBlank>, int>) (o => o.Key)).ToArray<int>();
  }

  private NodeColumn CreateObjectVersionIDNodeColummn()
  {
    return new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID, typeof (long), FieldTypes.ftInteger, ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OBJECT_ID));
  }

  private void CreateSupportedColumns()
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    columnCollection.Add(this.CreateObjectVersionIDNodeColummn());
    columnCollection.AddRange((IEnumerable<NodeColumn>) this.CreateColumns(this.GetAllAttributes()));
    this._supportedColumns = columnCollection;
  }

  private NodeColumnCollection CreateColumns(int[] attributes)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    foreach (int attribute in attributes)
    {
      FieldTypes attrType = FieldTypes.ftUnknown;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attribute);
      if (attributeType != null)
        attrType = attributeType.FieldType;
      else if (ObligatoryObjectAttributesHelper.IsObligatoryAttribute(attribute))
        attrType = FieldTypes.ftSystem;
      NodeColumn nodeColumn = new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) attribute, typeof (string), attrType, MetaDataHelper.GetAttributeTypeName(attribute));
      columns.Add(nodeColumn);
    }
    return columns;
  }

  private void OnSelectionChanged()
  {
    EventHandler selectionChanged = this.SelectionChanged;
    if (selectionChanged == null)
      return;
    selectionChanged((object) this, EventArgs.Empty);
  }

  private ObjectBlank GetObjectForRow(int rowIndex)
  {
    iGRow row = this._grid.Rows[rowIndex];
    return row == null ? (ObjectBlank) null : row.Tag as ObjectBlank;
  }

  private NodeColumn GetNodeColumnForGridColumn(int colIndex)
  {
    iGCol col = this._grid.Cols[colIndex];
    return col == null ? (NodeColumn) null : col.Tag as NodeColumn;
  }

  private iGCol GetGridColumnForNodeColumn(NodeColumn nodeColumn)
  {
    return this._grid.Cols.Cast<iGCol>().FirstOrDefault<iGCol>((Func<iGCol, bool>) (o => o.Tag == nodeColumn));
  }

  private void CreateNodeColumnsFromGridColumns()
  {
    List<NodeColumn> source = new List<NodeColumn>();
    foreach (iGCol col in (IEnumerable) this._grid.Cols)
    {
      if (col.Tag is NodeColumn)
      {
        NodeColumn tag = (NodeColumn) col.Tag;
        tag.Width = col.Width;
        tag.SortIndex = -1;
        tag.SortOrder = NodeColumnSortOrder.None;
        source.Add(tag);
      }
    }
    for (int index = 0; index < this._grid.SortObject.Count; ++index)
    {
      iGCol col = this._grid.Cols[this._grid.SortObject[index].ColIndex];
      if (col.Tag is NodeColumn)
      {
        NodeColumn tag = (NodeColumn) col.Tag;
        tag.SortIndex = this._grid.SortObject[index].Index;
        tag.SortOrder = this._grid.SortObject[index].SortOrder.ConvertToNodeColumnSortOrder();
      }
    }
    this._columns = new NodeColumnCollection();
    this._columns.AddRange(source.Select<NodeColumn, NodeColumn>((Func<NodeColumn, NodeColumn>) (o => (NodeColumn) o.Clone())));
  }

  private iGRow GetRowForObject(ObjectBlank objectBlank)
  {
    return this._grid.Rows.Cast<iGRow>().FirstOrDefault<iGRow>((Func<iGRow, bool>) (o => o.Tag == objectBlank));
  }

  private void OnColumnsChanged()
  {
    EventHandler columnsChanged = this.ColumnsChanged;
    if (columnsChanged == null)
      return;
    columnsChanged((object) this, EventArgs.Empty);
  }

  private void OnCurrentColumnChanged()
  {
    EventHandler currentColumnChanged = this.CurrentColumnChanged;
    if (currentColumnChanged == null)
      return;
    currentColumnChanged((object) this, EventArgs.Empty);
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectBlankListControl));
    this._grid = new TenTec.Windows.iGridLib.iGrid();
    this._imageList = new ImageList(this.components);
    this._toolTip = new ToolTip(this.components);
    ((ISupportInitialize) this._grid).BeginInit();
    this.SuspendLayout();
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
    this._grid.Size = new Size(488, 315);
    this._grid.TabIndex = 3;
    this._grid.CellMouseLeave += new iGCellMouseEnterLeaveEventHandler(this.Grid_CellMouseLeave);
    this._grid.CellMouseMove += new iGCellMouseMoveEventHandler(this.Grid_CellMouseMove);
    this._grid.CustomDrawCellForeground += new iGCustomDrawCellEventHandler(this.Grid_CustomDrawCellForeground);
    this._grid.CustomDrawCellBackground += new iGCustomDrawCellEventHandler(this.Grid_CustomDrawCellBackground);
    this._grid.ColWidthEndChange += new iGColWidthEventHandler(this.Grid_ColWidthEndChange);
    this._grid.ColHdrEndDrag += new iGColHdrEndDragEventHandler(this.Grid_ColHdrEndDrag);
    this._grid.CurCellChanged += new EventHandler(this.Grid_CurCellChanged);
    this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
    this._grid.AfterContentsSorted += new EventHandler(this.Grid_AfterContentsSorted);
    this._grid.RequestEdit += new iGRequestEditEventHandler(this.Grid_RequestEdit);
    this._grid.AfterCommitEdit += new iGAfterCommitEditEventHandler(this.Grid_AfterCommitEdit);
    this._imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imageList.ImageStream");
    this._imageList.TransparentColor = Color.Transparent;
    this._imageList.Images.SetKeyName(0, "Copy.png");
    this._imageList.Images.SetKeyName(1, "Error.png");
    this._imageList.Images.SetKeyName(2, "Instance.png");
    this._imageList.Images.SetKeyName(3, "Success.png");
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._grid);
    this.Name = nameof (ObjectBlankListControl);
    this.Size = new Size(488, 315);
    ((ISupportInitialize) this._grid).EndInit();
    this.ResumeLayout(false);
  }

  private sealed class ObjectBlankStatusIcon
  {
    public ObjectBlankStatusIcon(string imageName, string toolTip, Rectangle bounds)
    {
      this.ImageName = imageName;
      this.ToolTip = toolTip;
      this.Bounds = bounds;
    }

    public string ImageName { get; private set; }

    public string ToolTip { get; private set; }

    public Rectangle Bounds { get; private set; }
  }

  [Serializable]
  private sealed class ObjectBlankListMemento
  {
    public NodeColumnCollection Columns { get; set; }
  }
}
