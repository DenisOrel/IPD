// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TableEditHelper
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Bars;
using Intermech.Expert.Editor.Table;
using Intermech.Expert.Table;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using SourceGrid3;
using SourceGrid3.Cells;
using SourceGrid3.Cells.Controllers;
using SourceGrid3.Cells.Editors;
using SourceGrid3.Cells.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Класс для управления SourceGrid</summary>
public class TableEditHelper
{
  private eTable _table;
  private eTable[] _tables;
  private bool _readonly;
  public int windowWid;
  public int windowHei;
  private bool lockCellChange;
  private Grid _grid;
  private ContextMenuBarItem _menu;
  private IExpertTableColorsService _colorService;
  private IExpertTablePropertiesService _propertiesService;
  private Point _mousePoint = new Point(0, 0);

  /// <summary>Конструктор</summary>
  /// <param name="table">таблица</param>
  /// <param name="tables">список таблиц, если несколько</param>
  /// <param name="readOnly">только для чтения</param>
  public TableEditHelper(eTable table, eTable[] tables, bool readOnly)
    : this()
  {
    this.SetInfo(table, tables, readOnly);
  }

  /// <summary>Конструктор</summary>
  public TableEditHelper()
  {
    this._colorService = ServicesManager.GetService(typeof (IExpertTableColorsService)) as IExpertTableColorsService;
    if (this._colorService == null)
      throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Expert.Editor_30"), (object) typeof (IExpertTableColorsService)));
    this._propertiesService = ServicesManager.GetService(typeof (IExpertTablePropertiesService)) as IExpertTablePropertiesService;
    if (this._propertiesService == null)
      throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Expert.Editor_582"), (object) typeof (IExpertTablePropertiesService)));
  }

  /// <summary>Событие на изменение</summary>
  public event ModifiedHandler Modified;

  /// <summary>Установить параметры класса</summary>
  /// <param name="table">таблица</param>
  /// <param name="tables">список тадлиц если несколько</param>
  /// <param name="readOnly">только для чтения</param>
  public void SetInfo(eTable table, eTable[] tables, bool readOnly)
  {
    this._table = table;
    this._tables = tables;
    this._readonly = readOnly;
  }

  /// <summary>Привязвать таблицу к SourceGrid</summary>
  /// <param name="grid">SurceGrid</param>
  /// <param name="menu">контекстное меню</param>
  public void Attach(Grid grid, ContextMenuBarItem menu)
  {
    this._grid = grid;
    this._menu = menu;
    foreach (ButtonItemBase buttonItemBase in (CollectionBase) this._menu.Items)
      buttonItemBase.Click += new EventHandler(this.symbol_Click);
    this.Refresh();
  }

  /// <summary>Отвязвать SourceGrid</summary>
  public void Detach()
  {
    this._grid = (Grid) null;
    this._menu = (ContextMenuBarItem) null;
  }

  /// <summary>Обновить</summary>
  public void Refresh()
  {
    this._grid.Redim(0, 0);
    this._grid.Redim(this._table.FixedRows.Count + this._table.ValuesTable.RowsCount, this._table.FixedColumns.Count + this._table.ValuesTable.ColumnsCount);
    this._grid.FixedRows = this._table.FixedRows.Count;
    this._grid.FixedColumns = this._table.FixedColumns.Count;
    this.ShowFixedRows();
    this.ShowFixedColumns();
    this.ShowValues();
    this.UpdateSize();
    this._grid.Selection.EnableMultiSelection = false;
    this._grid.Invalidate(true);
  }

  private ICell GetCell(
    eCell cell,
    System.Type createType,
    Color foreColor,
    Color backColor,
    Position pos)
  {
    ICell cell1 = this.GetCell(cell, createType, pos);
    cell1.View.ForeColor = foreColor;
    cell1.View.BackColor = backColor;
    return cell1;
  }

  private ICell GetCell(eCell cell, System.Type createType, Position pos)
  {
    ICell instance = Activator.CreateInstance(createType) as ICell;
    this._grid[pos.Row, pos.Column] = instance;
    int num1 = this._grid.ColumnsCount - pos.Column;
    if (cell.ColSpan <= 0)
      cell.ColSpan = 1;
    instance.ColumnSpan = cell.ColSpan <= num1 ? cell.ColSpan : num1;
    int num2 = this._grid.RowsCount - pos.Row;
    instance.RowSpan = cell.RowSpan <= num2 ? cell.RowSpan : num2;
    instance.Tag = (object) cell;
    if (!cell.CellDestination.Equals((object) eCellDestination.Data))
    {
      instance.Value = (object) cell.ToString();
      instance.View = (IView) new GradientFlatHeader();
      instance.View.Font = new Font(instance.View.GetDrawingFont((GridVirtual) this._grid), FontStyle.Bold);
    }
    else
    {
      if (cell.CellValue.ValueType == DataType.Boolean)
        instance.View = (IView) new SourceGrid3.Cells.Views.CheckBox();
      else
        instance.View = (IView) new SourceGrid3.Cells.Views.Cell();
      instance.Value = !cell.isEmpty ? cell.CellValue.Value : (object) "";
    }
    if (!this._readonly)
    {
      CustomEvents model = new CustomEvents();
      eCellDestination cellDestination = cell.CellDestination;
      if (cellDestination.Equals((object) eCellDestination.Data) && cell.CommonType != null && cell.CommonType.AttributeType.MasterAttributeID == 0)
      {
        switch (cell.CellValue.ValueType)
        {
          case DataType.Integer:
          case DataType.Float:
          case DataType.String:
            instance.Editor = (EditorBase) new SourceGrid3.Cells.Editors.TextBox(DataTypeConvertor.DataType2Type(cell.CellValue.ValueType));
            break;
          case DataType.Measured:
            MeasureTextBox measureTextBox = new MeasureTextBox(typeof (MeasuredValue));
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(cell.CommonType.AttributeType.Guid);
            measureTextBox.PhysID = attributeType.SizeType;
            instance.Editor = (EditorBase) measureTextBox;
            break;
          case DataType.Date:
            instance.Editor = (EditorBase) new SourceGrid3.Cells.Editors.DateTimePicker();
            break;
        }
      }
      cellDestination = cell.CellDestination;
      if (cellDestination.Equals((object) eCellDestination.Data))
      {
        model.DoubleClick += new EventHandler(this.ce_DoubleClick);
        model.KeyDown += new KeyEventHandler(this.ce_KeyDown);
        model.ValueChanged += new EventHandler(this.ce_ValueChanged);
      }
      model.MouseUp += new MouseEventHandler(this.ce_MouseUp);
      instance.Controller.AddController((IController) model);
    }
    TableEditHelper.AddHintToCell(instance, cell);
    return instance;
  }

  /// <summary>Добавлени подсказки для ячейки</summary>
  /// <param name="gridCell">Ячейка в таблице</param>
  /// <param name="cell">Данные о ячейке</param>
  private static void AddHintToCell(ICell gridCell, eCell cell)
  {
    gridCell.ToolTipText = string.Empty;
    switch (cell.CellDestination)
    {
      case eCellDestination.Data:
      case eCellDestination.HeaderData:
        if (!cell.CellType.Equals((object) eCellType.Value) || cell.isEmpty || cell.CellValue == null || cell.CommonType == null)
          break;
        List<string> strValues = cell.CommonType.AttributeType.StrValues;
        string hintSpecial = TableEditHelper.GetHintSpecial(gridCell, strValues, cell.CellValue);
        if (hintSpecial != "")
        {
          gridCell.ToolTipText = hintSpecial;
          break;
        }
        ArrayList tips = new ArrayList();
        TableEditHelper.AddHintToValue(tips, cell.CellValue);
        gridCell.ToolTipText = string.Join("\r\n", tips.ToArray(typeof (string)) as string[]);
        break;
      case eCellDestination.Header:
      case eCellDestination.Result:
        if (cell.CommonType == null)
          break;
        string str = $"{cell.CommonType.ObjectType.Name} - {cell.CommonType.AttributeType.Name}";
        gridCell.ToolTipText = str;
        break;
    }
  }

  private static string GetIntTip(List<string> sList, ExpertValue val)
  {
    if (val.ValueType != DataType.Integer)
      return "";
    int int32 = Convert.ToInt32((object) val);
    return sList != null && int32 < sList.Count ? sList[int32] : "";
  }

  private static string GetHintSpecial(ICell gridCell, List<string> sList, ExpertValue value)
  {
    string hintSpecial1 = "";
    switch (value.ValueType)
    {
      case DataType.Integer:
        string intTip1 = TableEditHelper.GetIntTip(sList, value);
        if (intTip1 != "")
          return intTip1;
        break;
      case DataType.Packet:
        string str = "{";
        PacketValue packetValue = value.Value as PacketValue;
        for (int index = 0; index < packetValue.Count; ++index)
        {
          string hintSpecial2 = TableEditHelper.GetHintSpecial(gridCell, sList, packetValue[index]);
          if (hintSpecial2 == "")
            return "";
          str += hintSpecial2;
          if (index < packetValue.Count - 1)
            str += ", ";
        }
        hintSpecial1 = str + "}";
        break;
      case DataType.Diap:
        DiapValue diapValue = value.Value as DiapValue;
        string intTip2 = TableEditHelper.GetIntTip(sList, diapValue.Low);
        if (intTip2 != "")
        {
          string intTip3 = TableEditHelper.GetIntTip(sList, diapValue.High);
          if (intTip3 != "")
            return $"{intTip2}:{intTip3}";
          break;
        }
        break;
    }
    return hintSpecial1;
  }

  /// <summary>Дабавить подсказку по значению</summary>
  /// <param name="tips">Содержание подсказки</param>
  /// <param name="cellValue">Значение ячейки</param>
  private static void AddHintToValue(ArrayList tips, ExpertValue cellValue)
  {
    switch (cellValue.ValueType)
    {
      case DataType.ObjectLink:
        ObjectIDToCaption objectIdToCaption = new ObjectIDToCaption(Convert.ToInt64(cellValue.Value));
        tips.Add((object) $"{cellValue.Value.ToString()} - {objectIdToCaption.ToString()}");
        break;
      case DataType.Packet:
        PacketValue packetValue = cellValue.Value as PacketValue;
        for (int index = 0; index < packetValue.Count; ++index)
          TableEditHelper.AddHintToValue(tips, packetValue[index]);
        break;
      case DataType.Diap:
        DiapValue diapValue = cellValue.Value as DiapValue;
        ArrayList tips1 = new ArrayList();
        TableEditHelper.AddHintToValue(tips1, diapValue.Low);
        TableEditHelper.AddHintToValue(tips1, diapValue.High);
        tips.Add((object) string.Join(":", tips1.ToArray(typeof (string)) as string[]));
        break;
    }
  }

  private void UpdateProperties(ref eCell cell)
  {
    if (cell.CommonType == null)
      return;
    eCellDestination cellDestination = cell.CellDestination;
    if (!cellDestination.Equals((object) eCellDestination.Header))
    {
      cellDestination = cell.CellDestination;
      if (!cellDestination.Equals((object) eCellDestination.Result))
        return;
    }
    cell.OverrideByValue = this._propertiesService.Current.UseShortName4ObjectType || this._propertiesService.Current.UseShortName4AttributeType;
    if (!cell.OverrideByValue)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      string empty = string.Empty;
      string str1 = cell.CommonType.ObjectType.ToString();
      string str2 = cell.CommonType.AttributeType.ToString();
      if (this._propertiesService.Current.UseShortName4ObjectType)
      {
        if (!cell.CommonType.ObjectType.Guid.Equals(Guid.Empty))
        {
          IDBObjectType objectType = session.GetObjectType(cell.CommonType.ObjectType.Guid, false);
          if (objectType != null)
            str1 = objectType.ObjectTypeShortName.Equals(string.Empty) ? objectType.ObjectTypeName : objectType.ObjectTypeShortName;
        }
        else
          str1 = cell.CommonType.ObjectType.ToString();
      }
      if (this._propertiesService.Current.UseShortName4AttributeType)
      {
        IDBAttributeType attributeType = session.GetAttributeType(cell.CommonType.AttributeType.Guid, false);
        if (attributeType != null)
          str2 = attributeType.ShortName.Equals(string.Empty) ? attributeType.Name : attributeType.ShortName;
      }
      string str3 = $"{str1}.{str2}";
      cell.CellValue = new ExpertValue(str3);
    }
  }

  /// <summary>Отображение фиксированных рядов</summary>
  private void ShowFixedRows()
  {
    System.Type createType = typeof (SourceGrid3.Cells.Real.Header);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this._table.FixedRows.Count; ++index)
      {
        eRow fixedRow = this._table.FixedRows[index];
        if (fixedRow != null)
        {
          if (this._grid.FixedColumns > 0 && fixedRow.Header != null)
          {
            eCell header = fixedRow.Header;
            this.UpdateProperties(ref header);
            header.CommonType.AttributeType.UpdateStrValues(sessionKeeper.Session);
            this.GetCell(header, createType, this._colorService.Current.InputHorz.Header.ForeColor, this._colorService.Current.InputHorz.Header.BackColor, new Position(index, this._grid.FixedColumns - 1));
          }
          eCell cell;
          for (int fixedColumns = this._grid.FixedColumns; fixedColumns < this._grid.ColumnsCount; fixedColumns += cell.ColSpan)
          {
            Color foreColor = SystemColors.ControlText;
            Color backColor = SystemColors.Control;
            cell = fixedRow[fixedColumns - this._grid.FixedColumns];
            this.UpdateProperties(ref cell);
            if (cell.ColSpan >= this._table.ColumnsCount)
              cell.ColSpan = this._table.ColumnsCount - 1;
            switch (cell.CellDestination)
            {
              case eCellDestination.HeaderData:
                foreColor = this._colorService.Current.InputHorz.Data.ForeColor;
                backColor = this._colorService.Current.InputHorz.Data.BackColor;
                break;
              case eCellDestination.Result:
                foreColor = this._colorService.Current.Output.ForeColor;
                backColor = this._colorService.Current.Output.BackColor;
                break;
            }
            if (cell.CommonType != null)
              cell.CommonType.AttributeType.UpdateStrValues(sessionKeeper.Session);
            this.GetCell(cell, createType, foreColor, backColor, new Position(index, fixedColumns));
          }
        }
      }
    }
  }

  /// <summary>Отображение фиксированных колонок</summary>
  private void ShowFixedColumns()
  {
    System.Type createType = typeof (SourceGrid3.Cells.Real.Header);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < this._table.FixedColumns.Count; ++index)
      {
        eColumn fixedColumn = this._table.FixedColumns[index];
        if (fixedColumn != null)
        {
          if (this._grid.FixedRows > 0 && fixedColumn.Header != null)
          {
            eCell header = fixedColumn.Header;
            this.UpdateProperties(ref header);
            header.CommonType.AttributeType.UpdateStrValues(sessionKeeper.Session);
            this.GetCell(header, createType, this._colorService.Current.InputVert.Header.ForeColor, this._colorService.Current.InputVert.Header.BackColor, new Position(this._grid.FixedRows - 1, index));
          }
          eCell cell;
          for (int fixedRows = this._grid.FixedRows; fixedRows < this._grid.RowsCount; fixedRows += cell.RowSpan)
          {
            cell = fixedColumn[fixedRows - this._grid.FixedRows];
            this.UpdateProperties(ref cell);
            cell.CommonType.AttributeType.UpdateStrValues(sessionKeeper.Session);
            this.GetCell(cell, createType, this._colorService.Current.InputVert.Data.ForeColor, this._colorService.Current.InputVert.Data.BackColor, new Position(fixedRows, index));
          }
        }
      }
    }
  }

  /// <summary>Отображение в таблице значений</summary>
  private void ShowValues()
  {
    System.Type createType = typeof (SourceGrid3.Cells.Real.Cell);
    int fixedRows = this._grid.FixedRows;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (; fixedRows < this._grid.RowsCount; ++fixedRows)
      {
        for (int fixedColumns = this._grid.FixedColumns; fixedColumns < this._grid.ColumnsCount; ++fixedColumns)
        {
          eCell cell = this._table.ValuesTable[fixedRows - this._grid.FixedRows, fixedColumns - this._grid.FixedColumns];
          cell.CommonType.AttributeType.UpdateStrValues(sessionKeeper.Session);
          this.GetCell(cell, createType, this._colorService.Current.Data.ForeColor, this._colorService.Current.Data.BackColor, new Position(fixedRows, fixedColumns));
        }
      }
    }
  }

  /// <summary>Обновление ширины и высоты колонок и рядов</summary>
  private void UpdateSize()
  {
    Graphics graphics = this._grid.CreateGraphics();
    for (int index = 0; index < this._table.ColumnsCount; ++index)
    {
      int width = this._table.GetColumnWidth(index);
      if (width.Equals(0))
      {
        int p_iRow = 0;
        if (this._table.TableType == eTableType.DoubleEntry && index < this._table.FixedColumns.Count - 1)
          p_iRow = this._table.FixedRows.Count - 1;
        width = !(this._grid.GetCell(p_iRow, index) is ICell cell) || cell.Value.ToString().Equals(string.Empty) ? 50 : (int) graphics.MeasureString($"W{cell.Value.ToString()}W", cell.View.Font).Width;
        this._table.SetColumnWidth(index, width);
      }
      this._grid.Columns[index].Width = width;
    }
    for (int index = 0; index < this._table.RowsCount; ++index)
    {
      int height = this._table.GetRowHeight(index);
      if (height.Equals(0))
      {
        height = 20;
        this._table.SetRowHeight(index, height);
      }
      this._grid.Rows[index].Height = height;
    }
  }

  public void FixFixedColumns()
  {
    int num1 = 0;
    int num2 = 0;
    if (this._grid == null)
      return;
    this._grid.CreateGraphics();
    for (int index = 0; index < this._table.ColumnsCount; ++index)
    {
      int columnWidth = this._table.GetColumnWidth(index);
      if (index < this._table.FixedColumns.Count)
        num1 += columnWidth;
    }
    for (int index = 0; index < this._table.RowsCount; ++index)
    {
      int rowHeight = this._table.GetRowHeight(index);
      if (index < this._table.FixedRows.Count)
        num2 += rowHeight;
    }
    if (this.windowWid > 0 && this.windowWid < num1)
    {
      int num3 = this.windowWid * 3 / 4;
      for (int p = 0; p < this._table.FixedColumns.Count; ++p)
        this._grid.Columns[p].Width = num3 / this._table.FixedColumns.Count;
    }
    if (this.windowHei <= 0 || this.windowHei >= num2)
      return;
    int num4 = this.windowHei * 3 / 4;
    for (int p = 0; p < this._table.FixedRows.Count; ++p)
      this._grid.Rows[p].Height = num4 / this._table.FixedRows.Count;
  }

  private void ce_KeyDown(object sender, KeyEventArgs e)
  {
    ICell cell = ((CellContext) sender).Cell as ICell;
    if (e.KeyCode.Equals((object) Keys.Return) && cell.IsEditing())
    {
      cell.Editor.ApplyEdit();
    }
    else
    {
      if (e.Modifiers.Equals((object) Keys.None) && e.KeyCode.Equals((object) Keys.Return))
        this.ce_DoubleClick(sender, EventArgs.Empty);
      if (!e.Modifiers.Equals((object) Keys.None) || !e.KeyCode.Equals((object) Keys.Delete))
        return;
      eCell tag = cell.Tag as eCell;
      cell.Value = ExpertValue.Empty(tag.CellValue.ValueType).Value;
      this.ce_ValueChanged(sender, EventArgs.Empty);
    }
  }

  private void ce_DoubleClick(object sender, EventArgs e)
  {
    bool flag = false;
    CellContext cellContext = (CellContext) sender;
    ICell cell = cellContext.Cell as ICell;
    eCell tag = cell.Tag as eCell;
    cellContext.EndEdit(true);
    switch (tag.CellDestination)
    {
      case eCellDestination.Data:
        if (tag.CommonType.AttributeType != null)
        {
          DataType dataType = DataTypeConvertor.AttrType2DataType(tag.CommonType.AttributeType.FieldTypes);
          switch (dataType)
          {
            case DataType.Measured:
              using (SingleValueEditor singleValueEditor = new SingleValueEditor(tag.CommonType, dataType, (IList) new object[0], (IList) new object[0]))
              {
                if (tag.CellValue != null)
                  singleValueEditor.Value = tag.CellValue.Value == null || tag.CellValue.Value is MeasuredValue ? tag.CellValue.Value : (object) null;
                int attributeTypeId = MetaDataHelper.GetAttributeTypeID(tag.CommonType.AttributeType.Guid);
                if (attributeTypeId != 0)
                {
                  AttributeTypeHolder ath = ExpertTableCaches.GetAttrHolder((long) attributeTypeId);
                  if (ath == null)
                  {
                    ath = tag.CommonType.AttributeType;
                    if (ath != null)
                      ExpertTableCaches.AddAttrHolder((long) attributeTypeId, ath);
                  }
                  if (tag.CellValue == null)
                    singleValueEditor.AttrMeasuredEdit.DefMeasure = ath.SavedMeasure;
                  singleValueEditor.AttrMeasuredEdit.ButtonPerformClick();
                  if (singleValueEditor.AttrMeasuredEdit.Modified)
                  {
                    flag = this.UpdateCellForTables(cell, tag, new eCellSymbol?(), new ExpertValue(dataType, singleValueEditor.Value));
                    if (singleValueEditor.Value != null)
                    {
                      if (singleValueEditor.Value is MeasuredValue)
                      {
                        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor((MeasuredValue) singleValueEditor.Value);
                        if (!descriptor.Empty)
                        {
                          if (ath != null)
                          {
                            ath.SavedMeasure = descriptor;
                            break;
                          }
                          break;
                        }
                        break;
                      }
                      break;
                    }
                    break;
                  }
                  break;
                }
                break;
              }
            case DataType.ObjectLink:
              using (SingleValueEditor singleValueEditor = new SingleValueEditor(tag.CommonType, dataType, (IList) new object[0], (IList) new object[0]))
              {
                singleValueEditor.Value = tag.CellValue.Value;
                singleValueEditor.ButtonEditClick.DynamicInvoke((object) singleValueEditor.ButtonEdit, null);
                flag = this.UpdateCellForTables(cell, tag, new eCellSymbol?(), new ExpertValue(dataType, singleValueEditor.Value));
                break;
              }
            default:
              using (ChooseSymbol chooseSymbol = new ChooseSymbol(tag.CommonType, tag.CellValue))
              {
                if (chooseSymbol.ShowDialog().Equals((object) DialogResult.OK))
                {
                  flag = this.UpdateCellForTables(cell, tag, new eCellSymbol?(), chooseSymbol.ResultValue);
                  break;
                }
                break;
              }
          }
        }
        else
          break;
        break;
      case eCellDestination.Header:
      case eCellDestination.Result:
        if (tag.CellType.Equals((object) eCellType.Text) && tag.CommonType != null)
        {
          string caption = EnumTypeHelper.GetCaption((Enum) eCellSymbol.Other);
          EnumTypeHelper.GetCaption((Enum) eCellSymbol.Set);
          foreach (MenuButtonItem menuButtonItem in (CollectionBase) this._menu.Items)
          {
            menuButtonItem.Checked = menuButtonItem.Text.Equals(EnumTypeHelper.GetCaption((Enum) tag.CellSymbol));
            menuButtonItem.Visible = !menuButtonItem.Text.Equals(caption);
            menuButtonItem.Tag = (object) cell;
          }
          this._menu.Show((Control) cell.Grid, this._mousePoint);
          break;
        }
        break;
      case eCellDestination.HeaderData:
        using (ChooseSymbol chooseSymbol = new ChooseSymbol(tag.CellSymbol, tag.CommonType, tag.CellValue))
        {
          DialogResult dialogResult = chooseSymbol.ShowDialog();
          if (!dialogResult.Equals((object) DialogResult.OK))
          {
            if (!dialogResult.Equals((object) DialogResult.Yes))
              break;
          }
          flag = this.UpdateCellForTables(cell, tag, new eCellSymbol?(chooseSymbol.CellSymbol), chooseSymbol.ResultValue, dialogResult.Equals((object) DialogResult.Yes));
          break;
        }
    }
    if (!(flag | this.IsCellModified(cell, tag)))
      return;
    eCellDestination cellDestination = tag.CellDestination;
    cell.Value = !cellDestination.Equals((object) eCellDestination.Data) ? (object) tag.ToString() : tag.CellValue.Value;
    TableEditHelper.AddHintToCell(cell, tag);
    cell.Invalidate();
    ModifiedHandler modified = this.Modified;
    if (modified == null)
      return;
    modified((object) cell);
  }

  private void ce_ValueChanged(object sender, EventArgs e)
  {
    if (this.lockCellChange)
      return;
    ICell cell = ((CellContext) sender).Cell as ICell;
    eCell tag = cell.Tag as eCell;
    DataType valueType = tag.CellValue.ValueType;
    System.Type type = DataTypeConvertor.DataType2Type(valueType);
    object obj = cell.Value;
    if (tag.isEmpty)
      tag.CellValue = (ExpertValue) null;
    else if (cell.Value != null && !type.Equals(cell.Value.GetType()))
      obj = TypeDescriptor.GetConverter(type).ConvertFrom(cell.Value);
    if (tag.CellValue != null)
    {
      if (obj != null)
      {
        if (obj is MeasuredValue)
        {
          MeasuredValue measuredValue = (MeasuredValue) obj;
          if (string.IsNullOrEmpty(measuredValue.Caption) || char.IsDigit(measuredValue.Caption, measuredValue.Caption.Length - 1))
          {
            if (tag.CommonType.AttributeType.Guid != Guid.Empty)
            {
              AttributeTypeHolder attrHolder = ExpertTableCaches.GetAttrHolder((long) MetaDataHelper.GetAttributeID((object) tag.CommonType.AttributeType.Guid));
              if (attrHolder != null && attrHolder.SavedMeasure != null && measuredValue.MeasureID != attrHolder.SavedMeasure.MeasureID)
              {
                measuredValue = new MeasuredValue(measuredValue.Value, attrHolder.SavedMeasure.MeasureID);
                obj = (object) measuredValue;
              }
            }
            measuredValue.Caption = MeasureHelper.ConvertToString(measuredValue.Value, measuredValue.MeasureID, false);
            this.lockCellChange = true;
            try
            {
              cell.Value = (object) measuredValue;
            }
            finally
            {
              this.lockCellChange = false;
            }
          }
        }
        tag.CellValue = new ExpertValue(valueType, obj);
      }
      else
        tag.CellValue = (ExpertValue) null;
    }
    TableEditHelper.AddHintToCell(cell, tag);
    ModifiedHandler modified = this.Modified;
    if (modified == null)
      return;
    modified((object) cell);
  }

  private void symbol_Click(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem))
      return;
    MenuButtonItem menuButtonItem = sender as MenuButtonItem;
    eCellSymbol enumValue = (eCellSymbol) EnumTypeHelper.GetEnumValue(typeof (eCellSymbol), menuButtonItem.Text, (object) eCellSymbol.None);
    ICell tag1 = menuButtonItem.Tag as ICell;
    eCell tag2 = tag1.Tag as eCell;
    if (!this.UpdateCellForTables(tag1, tag2, new eCellSymbol?(enumValue), (ExpertValue) null))
      return;
    eCellDestination cellDestination = tag2.CellDestination;
    tag1.Value = !cellDestination.Equals((object) eCellDestination.Data) ? (object) tag2.ToString() : tag2.CellValue.Value;
    TableEditHelper.AddHintToCell(tag1, tag2);
    tag1.Invalidate();
    ModifiedHandler modified = this.Modified;
    if (modified == null)
      return;
    modified((object) tag1);
  }

  /// <summary>
  /// Обновить ячейку для всех таблиц (их несколько, только если таблица двухвходовая)
  /// </summary>
  /// <param name="gridCell">Интерфейс, указывающий строку и столбец ячейки</param>
  /// <param name="cell">Измененная ячейка</param>
  /// <param name="newCellSymbol">Новый символ для ячейки</param>
  /// <param name="newCellValue">Новое значение для ячейки</param>
  /// <param name="clear">Признак очистки ячейки</param>
  /// <returns>true, если хотя бы одна ячейка была изменена</returns>
  private bool UpdateCellForTables(
    ICell gridCell,
    eCell cell,
    eCellSymbol? newCellSymbol,
    ExpertValue newCellValue,
    bool clear = false)
  {
    bool flag = false;
    if (cell.CellDestination.Equals((object) eCellDestination.Data))
    {
      flag = this.UpdateCellForTable(cell, newCellSymbol, newCellValue, clear);
    }
    else
    {
      foreach (eTable table in this._tables)
      {
        eCell cell1 = table.GetCell(gridCell.Row, gridCell.Column);
        flag |= this.UpdateCellForTable(cell1, newCellSymbol, newCellValue, clear);
      }
    }
    return flag;
  }

  /// <summary>Обновить значение ячейки в этой таблице</summary>
  /// <param name="cell">Измененная ячейка</param>
  /// <param name="newCellSymbol">Новый символ для ячейки</param>
  /// <param name="newCellValue">Новое значение для ячейки</param>
  /// <param name="clear">Признак очистки ячейки</param>
  /// <returns>true, если ячейка была изменена</returns>
  private bool UpdateCellForTable(
    eCell cell,
    eCellSymbol? newCellSymbol,
    ExpertValue newCellValue,
    bool clear)
  {
    bool flag = false;
    if (newCellSymbol.HasValue)
    {
      int cellSymbol = (int) cell.CellSymbol;
      eCellSymbol? nullable = newCellSymbol;
      int valueOrDefault = (int) nullable.GetValueOrDefault();
      if (!(cellSymbol == valueOrDefault & nullable.HasValue))
      {
        cell.CellSymbol = newCellSymbol.Value;
        flag = true;
      }
    }
    if (newCellValue != null && !newCellValue.Equals((object) cell._RealCellValue) || clear && cell.CellValue != null)
    {
      cell.CellValue = newCellValue;
      flag = true;
    }
    return flag;
  }

  private bool IsCellModified(ICell src, eCell cell)
  {
    return Convert.ToString(src.Value) != cell.ToString();
  }

  private void ce_MouseUp(object sender, MouseEventArgs e)
  {
    CellContext cellContext = (CellContext) sender;
    ICell cell = cellContext.Cell as ICell;
    eCell tag = cell.Tag as eCell;
    int width = cellContext.Grid.Columns.GetWidth(cell.Column);
    int height = cellContext.Grid.Rows.GetHeight(cell.Row);
    int columnWidth = this._table.GetColumnWidth(cell.Column);
    int rowHeight = this._table.GetRowHeight(cell.Row);
    if (width.Equals(columnWidth) && height.Equals(rowHeight))
    {
      if (!e.Button.Equals((object) MouseButtons.Left) || tag.CellDestination.Equals((object) eCellDestination.Data) || !this._table.TableType.Equals((object) eTableType.NoEntry) && tag.CellDestination.Equals((object) eCellDestination.Result))
        return;
      this._mousePoint = new Point(e.X, e.Y);
      this.ce_DoubleClick(sender, EventArgs.Empty);
      cell.Invalidate();
    }
    else
    {
      foreach (eTable table in this._tables)
      {
        table.SetColumnWidth(cell.Column, width);
        table.SetRowHeight(cell.Row, height);
      }
      if (this.Modified == null)
        return;
      this.Modified((object) cell);
    }
  }
}
