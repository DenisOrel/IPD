// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.TableEditorDialog
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>Редактор таблиц</summary>
/// <summary>Редактор таблиц</summary>
public class TableEditorDialog : Form
{
  /// <summary>Последняя координата верхнего левого угла формы</summary>
  protected static Point lastFormPosition = Point.Empty;
  /// <summary>Редактируемая в данный момент времени таблица</summary>
  protected TableElement item;
  /// <summary>Были ли изменения в редактируемой таблице</summary>
  protected bool isChanged;
  /// <summary>Выполняется ли обновление содержимого контролов - проверка для обработчиков событий</summary>
  protected bool isUpdating;
  /// <summary>Есть ли в в столбцах значения ширины, равные нулю</summary>
  protected bool hasInvalidWidth;
  /// <summary>Есть ли в строках значения высоты, равные нулю</summary>
  protected bool hasInvalidHeight;
  /// <summary>Список с кнопками, которые отвечают за рамки в редактируемой таблице</summary>
  protected List<Button> frameButtons = new List<Button>(5);
  private Decimal roundRowsInsertHeight_ValueTo;
  private bool lockSetRoundRowsInsertHeight_ValueTo;
  /// <summary>Контейнер компонентов формы</summary>
  private IContainer components;
  private Panel panelTableSize;
  private Button buttonFrameMode_1;
  private Button buttonFrameMode_2;
  private Button buttonFrameMode_4;
  private Button buttonFrameMode_3;
  private Button buttonFrameMode_5;
  private ToolTip toolTips;
  private PictureBox pictureTableDimensions;
  private CheckBox cb_tLW;
  private CheckBox cb_tRW;
  private CheckBox cb_tW;
  private CheckBox cb_tLH;
  private CheckBox cb_tH;
  private CheckBox cb_tBH;
  private CustomNumericUpDown numeric_tLW;
  private CustomNumericUpDown numeric_tW;
  private CustomNumericUpDown numeric_tRW;
  private CustomNumericUpDown numeric_tLH;
  private CustomNumericUpDown numeric_tBH;
  private CustomNumericUpDown numeric_tH;
  private Panel panelTableType;
  private RadioButton radio_StaticTable;
  private RadioButton radio_DynamicTable;
  private Panel panelTableProperties;
  private CheckBox cb_TableWrapping;
  private CheckBox cb_FullSizeGrid;
  private CustomNumericUpDown numeric_GridRowHeight;
  private Label label_GridRowHeight;
  private Panel panelTableColumns;
  private iGrid grid_Columns;
  private Label label_ColumnsInsert;
  private Label label_ColumnsInsertCount;
  private CustomNumericUpDown numeric_ColumnsInsertCount;
  private CustomNumericUpDown numeric_ColumnsInsertWidth;
  private Label label_ColumnsWidth;
  private Panel panelTableRows;
  private CustomNumericUpDown numeric_RowsInsertHeight;
  private Label label_ColumnsHeight;
  private CustomNumericUpDown numeric_RowsInsertCount;
  private Label label_RowsInsertCount;
  private Label label_RowsInsert;
  private iGrid grid_Rows;
  private CustomNumericUpDown numeric_HeaderRows;
  private Label label_HeaderRows;
  private Button btnCancel;
  private Button btnOK;
  private Bevel bevel;
  private ErrorProvider errorProvider;
  private Label labelWarning;
  private Button button_ColumnsInsertAfter;
  private Button button_ColumnsInsertBefore;
  private Button button_ColumnsRemove;
  private Label label_ColumnsCount;
  private Button button_RowsRemove;
  private Button button_RowsInsertAfter;
  private Button button_RowsInsertBefore;
  private Label label_RowsCount;
  private Label labelColumnsHeader;
  private Label labelRowsHeader;
  private ImageList imageButtons;
  private ImageList imageFrames;
  private GridView gridView1;
  private Button buttonFrameMode_6;

  /// <summary>Создать незаполненный экземпляр формы - редактора таблиц</summary>
  public TableEditorDialog()
    : this((TableElement) null)
  {
  }

  /// <summary>Создать экземпляр формы - редактора таблиц</summary>
  /// <param name="table">Экземпляр таблицы, над которым будут выполняться все изменения</param>
  public TableEditorDialog(TableElement table)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1191);
    this.InitForm(table);
  }

  /// <summary>Создать в гридах столбцы</summary>
  protected virtual void PrepareGridsColumns()
  {
    iGCellStyle iGcellStyle1 = new iGCellStyle(true);
    iGcellStyle1.TextAlign = iGContentAlignment.MiddleLeft;
    iGcellStyle1.ReadOnly = iGBool.True;
    iGCellStyle iGcellStyle2 = new iGCellStyle(true);
    iGcellStyle2.SingleClickEdit = iGBool.True;
    iGcellStyle2.TextAlign = iGContentAlignment.MiddleCenter;
    iGcellStyle2.Type = iGCellType.Check;
    iGcellStyle2.ValueType = typeof (bool);
    iGcellStyle2.SingleClickEdit = iGBool.True;
    iGcellStyle2.ReadOnly = iGBool.False;
    iGcellStyle2.EmptyStringAs = iGEmptyStringAs.EmptyString;
    iGCellStyle iGcellStyle3 = new iGCellStyle(true);
    iGcellStyle3.SingleClickEdit = iGBool.True;
    iGcellStyle3.TextAlign = iGContentAlignment.MiddleLeft;
    iGcellStyle3.ValueType = typeof (int);
    iGcellStyle3.SingleClickEdit = iGBool.True;
    iGcellStyle3.ReadOnly = iGBool.False;
    iGcellStyle3.EmptyStringAs = iGEmptyStringAs.EmptyString;
    iGCellStyle iGcellStyle4 = new iGCellStyle(true);
    iGcellStyle4.SingleClickEdit = iGBool.True;
    iGcellStyle4.TextAlign = iGContentAlignment.MiddleLeft;
    iGcellStyle4.ValueType = typeof (string);
    iGcellStyle4.SingleClickEdit = iGBool.True;
    iGcellStyle4.ReadOnly = iGBool.False;
    iGcellStyle4.EmptyStringAs = iGEmptyStringAs.EmptyString;
    iGColHdrStyle iGcolHdrStyle = new iGColHdrStyle(true);
    iGcolHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.grid_Columns.Cols.Clear();
    iGCol iGcol1 = this.grid_Columns.Cols.Add(new iGColPattern(36, true, true, 20, -1, true, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) LocalizationHolder.rm.GetString("Document.Model_524"), GridColumnKeys.column_ColumnNumber, -1, (object) string.Empty, (object) null, -1));
    iGcol1.CellStyle = iGcellStyle1;
    iGcol1.ColHdrStyle = iGcolHdrStyle;
    iGCol iGcol2 = this.grid_Columns.Cols.Add(new iGColPattern(80 /*0x50*/, true, true, 20, -1, true, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) LocalizationHolder.rm.GetString("Document.Model_525"), GridColumnKeys.column_ColumnName, -1, (object) string.Empty, (object) null, -1));
    iGcol2.CellStyle = iGcellStyle4;
    iGcol2.ColHdrStyle = iGcolHdrStyle;
    iGCol iGcol3 = this.grid_Columns.Cols.Add(new iGColPattern(20, true, true, 20, 20, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) "", GridColumnKeys.column_ColumnCheck, -1, (object) false, (object) null, -1));
    iGcol3.CellStyle = iGcellStyle2;
    iGcol3.ColHdrStyle = iGcolHdrStyle;
    iGcol3.CellStyle = iGcellStyle2;
    iGcol3.ColHdrStyle = iGcolHdrStyle;
    iGCol iGcol4 = this.grid_Columns.Cols.Add(new iGColPattern(75, true, true, 20, -1, true, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) LocalizationHolder.rm.GetString("Document.Model_526"), GridColumnKeys.column_ColumnWidth, -1, (object) 10, (object) null, -1));
    iGcol4.CellStyle = iGcellStyle3;
    iGcol4.ColHdrStyle = iGcolHdrStyle;
    this.grid_Rows.Cols.Clear();
    iGCol iGcol5 = this.grid_Rows.Cols.Add(new iGColPattern(36, true, true, 20, -1, true, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) LocalizationHolder.rm.GetString("Document.Model_524"), GridColumnKeys.column_RowNumber, -1, (object) string.Empty, (object) null, -1));
    iGcol5.CellStyle = iGcellStyle1;
    iGcol5.ColHdrStyle = iGcolHdrStyle;
    iGCol iGcol6 = this.grid_Rows.Cols.Add(new iGColPattern(80 /*0x50*/, true, true, 20, -1, true, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) LocalizationHolder.rm.GetString("Document.Model_527"), GridColumnKeys.column_RowName, -1, (object) string.Empty, (object) null, -1));
    iGcol6.CellStyle = iGcellStyle4;
    iGcol6.ColHdrStyle = iGcolHdrStyle;
    iGCol iGcol7 = this.grid_Rows.Cols.Add(new iGColPattern(20, true, true, 20, 20, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) "", GridColumnKeys.column_RowCheck, -1, (object) false, (object) null, -1));
    iGcol7.CellStyle = iGcellStyle2;
    iGcol7.ColHdrStyle = iGcolHdrStyle;
    iGCol iGcol8 = this.grid_Rows.Cols.Add(new iGColPattern(75, true, true, 20, -1, true, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) LocalizationHolder.rm.GetString("Document.Model_528"), GridColumnKeys.column_RowHeight, -1, (object) 1, (object) null, -1));
    iGcol8.CellStyle = iGcellStyle3;
    iGcol8.ColHdrStyle = iGcolHdrStyle;
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="table">Экземпляр таблицы, над которым будут выполняться все изменения</param>
  /// <returns>Результат работы формы</returns>
  public static DialogResult Execute(TableElement table, bool newTable)
  {
    if (table == null)
      return DialogResult.Abort;
    using (TableEditorDialog tableEditorDialog = new TableEditorDialog(table))
    {
      if (!newTable)
      {
        string str = LocalizationHolder.rm.GetString("Document.Model_589");
        tableEditorDialog.Text = str;
      }
      return tableEditorDialog.ShowDialog();
    }
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="table">Экземпляр таблицы, над которым будут выполняться все изменения</param>
  /// <returns>Результат работы формы</returns>
  public static DialogResult Execute(TableElement table) => TableEditorDialog.Execute(table, true);

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TableEditorDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    Point location = this.Location;
    int x = location.X;
    location = this.Location;
    int y = location.Y;
    TableEditorDialog.lastFormPosition = new Point(x, y);
  }

  /// <summary>Выполнить инициализацию формы исходными данными</summary>
  /// <param name="table">Экземпляр таблицы, над которым будут выполняться все изменения</param>
  /// <returns>true, если инициализация выполнена успешно</returns>
  protected bool InitForm(TableElement table)
  {
    this.MaximumSize = new Size(700, 1000);
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Location = TableEditorDialog.lastFormPosition.IsEmpty ? new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2) : TableEditorDialog.lastFormPosition;
    this.frameButtons.Add(this.buttonFrameMode_1);
    this.frameButtons.Add(this.buttonFrameMode_2);
    this.frameButtons.Add(this.buttonFrameMode_3);
    this.frameButtons.Add(this.buttonFrameMode_4);
    this.frameButtons.Add(this.buttonFrameMode_5);
    this.frameButtons.Add(this.buttonFrameMode_6);
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.panelTableType.BackColor = ControlColors.colorHeaderBackground;
    this.labelColumnsHeader.BackColor = ControlColors.colorHeaderBackground;
    this.labelRowsHeader.BackColor = ControlColors.colorHeaderBackground;
    this.PrepareGridsColumns();
    this.numeric_tLW.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    this.numeric_tW.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    this.numeric_tRW.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    this.numeric_tLH.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    this.numeric_tH.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    this.numeric_tBH.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    this.numeric_GridRowHeight.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    this.numeric_ColumnsInsertWidth.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    this.numeric_RowsInsertHeight.ValueChanged += new EventHandler(this.numeric_ValueChanged);
    return this.LoadFromTable(table);
  }

  private void numeric_ValueChanged(object sender, EventArgs e)
  {
    NumericUpDown numericUpDown = sender as NumericUpDown;
    double d = (double) numericUpDown.Value;
    string str = (d - Math.Floor(d)).ToString();
    int num = 0;
    if (str.Length > 1)
      num = str.Length - 2;
    numericUpDown.DecimalPlaces = num;
  }

  /// <summary>Очистить все поля в форме</summary>
  /// <param name="updateControls">Обновить статусы всех контролов</param>
  protected virtual void Clear(bool updateControls)
  {
    this.numeric_tLW.Value = 20M;
    this.cb_tLW.Checked = true;
    this.numeric_tW.Value = 180M;
    this.cb_tW.Checked = false;
    this.numeric_tRW.Value = 20M;
    this.cb_tRW.Checked = true;
    this.numeric_tLH.Value = 20M;
    this.cb_tLH.Checked = true;
    this.numeric_tH.Value = 250M;
    this.cb_tH.Checked = false;
    this.numeric_tBH.Value = 20M;
    this.cb_tBH.Checked = true;
    this.grid_Columns.Rows.Clear();
    this.grid_Rows.Rows.Clear();
    this.radio_StaticTable.Checked = true;
    this.cb_TableWrapping.Checked = false;
    this.cb_FullSizeGrid.Checked = false;
    this.numeric_GridRowHeight.Value = 10M;
    this.numeric_ColumnsInsertCount.Value = 1M;
    this.numeric_ColumnsInsertWidth.Value = 25M;
    this.numeric_RowsInsertCount.Value = 1M;
    this.numeric_RowsInsertHeight.Value = this.numeric_GridRowHeight.Value;
    this.roundRowsInsertHeight_ValueTo = this.numeric_RowsInsertHeight.Value;
    this.numeric_HeaderRows.Value = 1M;
    if (!updateControls)
      return;
    this.UpdateControls();
  }

  /// <summary>Заполнить грид, управляющий колонками, данными из указанной таблицы</summary>
  /// <param name="table">Экземпляр таблицы, над которым будут выполняться все изменения</param>
  protected virtual void LoadColumns(TableElement table)
  {
    this.grid_Columns.Rows.Clear();
    List<RowColParams> gridColumnsParams = this.item.GridColumnsParams;
    if (gridColumnsParams != null && gridColumnsParams.Count > 0)
    {
      for (int index = 0; index < gridColumnsParams.Count; ++index)
      {
        RowColParams rowColParams = gridColumnsParams[index];
        if (rowColParams != null)
        {
          iGRow iGrow = this.grid_Columns.Rows.Add();
          iGrow.Key = index.ToString();
          iGrow.Cells[GridColumnKeys.column_ColumnNumber].Style = new iGCellStyle();
          iGrow.Cells[GridColumnKeys.column_ColumnNumber].Selectable = iGBool.False;
          iGrow.Cells[GridColumnKeys.column_ColumnNumber].Style.ReadOnly = iGBool.True;
          iGrow.Cells[GridColumnKeys.column_ColumnNumber].Style.TextAlign = iGContentAlignment.MiddleRight;
          iGrow.Cells[GridColumnKeys.column_ColumnNumber].Style.BackColor = this.grid_Columns.Header.BackColor;
          iGrow.Cells[GridColumnKeys.column_ColumnNumber].Value = (object) (index + 1);
          iGrow.Cells[GridColumnKeys.column_ColumnCheck].ValueType = typeof (bool);
          iGrow.Cells[GridColumnKeys.column_ColumnCheck].Value = (object) true;
          iGrow.Cells[GridColumnKeys.column_ColumnName].ValueType = typeof (string);
          iGrow.Cells[GridColumnKeys.column_ColumnName].Value = rowColParams.ColRowName != null ? (object) rowColParams.ColRowName : (object) string.Empty;
          iGrow.Cells[GridColumnKeys.column_ColumnWidth].ValueType = typeof (float);
          iGrow.Cells[GridColumnKeys.column_ColumnWidth].Value = (object) rowColParams.Size;
        }
      }
    }
    this.label_ColumnsCount.Text = this.grid_Columns.Rows.Count.ToString();
    this.CorrectTableColumnWidths();
  }

  /// <summary>Заполнить грид, управляющий строками, данными из указанной таблицы</summary>
  /// <param name="table">Экземпляр таблицы, над которым будут выполняться все изменения</param>
  protected virtual void LoadRows(TableElement table)
  {
    this.grid_Rows.Rows.Clear();
    if (this.item.NodesCount > 0)
    {
      for (int index = 0; index < this.item.NodesCount; ++index)
      {
        if (this.item.Nodes[index] is TableElement node)
        {
          iGRow iGrow = this.grid_Rows.Rows.Add();
          iGrow.Key = index.ToString();
          iGrow.Cells[GridColumnKeys.column_RowNumber].Style = new iGCellStyle();
          iGrow.Cells[GridColumnKeys.column_RowNumber].Selectable = iGBool.False;
          iGrow.Cells[GridColumnKeys.column_RowNumber].Style.ReadOnly = iGBool.True;
          iGrow.Cells[GridColumnKeys.column_RowNumber].Style.TextAlign = iGContentAlignment.MiddleRight;
          iGrow.Cells[GridColumnKeys.column_RowNumber].Style.BackColor = this.grid_Rows.Header.BackColor;
          iGrow.Cells[GridColumnKeys.column_RowNumber].Value = (object) (index + 1);
          iGrow.Cells[GridColumnKeys.column_RowCheck].ValueType = typeof (bool);
          iGrow.Cells[GridColumnKeys.column_RowCheck].Value = (object) true;
          iGrow.Cells[GridColumnKeys.column_RowName].ValueType = typeof (string);
          iGrow.Cells[GridColumnKeys.column_RowName].Value = node.Name != null ? (object) node.Name : (object) string.Empty;
          iGrow.Cells[GridColumnKeys.column_RowHeight].ValueType = typeof (float);
          iGrow.Cells[GridColumnKeys.column_RowHeight].Value = (object) Math.Round((double) node.Size.Height, 5);
        }
      }
    }
    this.label_RowsCount.Text = this.grid_Rows.Rows.Count.ToString();
    this.CorrectTableRowHeights();
  }

  /// <summary>Заполнить поля в форме данными из указанной таблицы</summary>
  /// <param name="table">Экземпляр таблицы, над которым будут выполняться все изменения</param>
  /// <returns>true, если данные успешно загружены</returns>
  protected virtual bool LoadFromTable(TableElement table)
  {
    try
    {
      this.isUpdating = true;
      this.Clear(false);
      this.item = table;
      if (table == null)
        return false;
      this.SetFrameMode(0);
      this.numeric_HeaderRows.Value = (Decimal) table.HeadersCount;
      this.numeric_tLW.Value = Convert.ToDecimal(this.item.Bounds.Left);
      this.cb_tLW.Checked = true;
      this.numeric_tW.Value = Convert.ToDecimal(this.item.Bounds.Width);
      this.cb_tW.Checked = true;
      CustomNumericUpDown numericTRw = this.numeric_tRW;
      double width1 = (double) this.item.Page.Size.Width;
      RectangleF bounds = this.item.Bounds;
      double left = (double) bounds.Left;
      double num1 = width1 - left;
      bounds = this.item.Bounds;
      double width2 = (double) bounds.Width;
      Decimal num2 = Convert.ToDecimal((float) (num1 - width2));
      numericTRw.Value = num2;
      this.cb_tRW.Checked = false;
      CustomNumericUpDown numericTLh = this.numeric_tLH;
      bounds = this.item.Bounds;
      Decimal num3 = Convert.ToDecimal(bounds.Top);
      numericTLh.Value = num3;
      this.cb_tLH.Checked = true;
      CustomNumericUpDown numericTH = this.numeric_tH;
      bounds = this.item.Bounds;
      Decimal num4 = Convert.ToDecimal(bounds.Height);
      numericTH.Value = num4;
      this.cb_tH.Checked = true;
      CustomNumericUpDown numericTBh = this.numeric_tBH;
      double height1 = (double) this.item.Page.Size.Height;
      bounds = this.item.Bounds;
      double top = (double) bounds.Top;
      double num5 = height1 - top;
      bounds = this.item.Bounds;
      double height2 = (double) bounds.Height;
      Decimal num6 = Convert.ToDecimal((float) (num5 - height2));
      numericTBh.Value = num6;
      this.cb_tBH.Checked = false;
      this.LoadRows(this.item);
      this.LoadColumns(this.item);
      this.cb_FullSizeGrid.Checked = this.item.IsPageFlow;
      this.numeric_GridRowHeight.Value = Convert.ToDecimal(this.item.DefaultRowSize);
      if (this.item.IsPageFlow || this.item.AutoSizeHeight)
        this.radio_DynamicTable.Checked = true;
      else
        this.radio_StaticTable.Checked = true;
      this.cb_TableWrapping.Checked = this.item.IsPageFlow;
      this.cb_FullSizeGrid.Checked = this.item.DrawGridToBottom;
      return true;
    }
    finally
    {
      this.isUpdating = false;
      this.CorrectRowNums();
      this.CorrectTableRowHeights();
      this.CorrectTableColumnWidths();
      this.UpdateControls();
    }
  }

  /// <summary>Обновить размеры строк из таблицы</summary>
  private void UpdateSizesFromTable()
  {
    try
    {
      this.isUpdating = true;
      for (int index = 0; index < this.item.Nodes.Count && index < this.grid_Rows.Rows.Count; ++index)
      {
        if (this.item.Nodes[index] is TableElement node)
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Value = (object) Math.Round((double) node.Size.Height, 5);
      }
      List<RowColParams> gridColumnsParams = this.item.GridColumnsParams;
      if (gridColumnsParams != null && gridColumnsParams.Count > 0)
      {
        for (int index = 0; index < gridColumnsParams.Count && index < this.grid_Columns.Rows.Count; ++index)
        {
          RowColParams rowColParams = gridColumnsParams[index];
          if (rowColParams != null)
            this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnWidth].Value = (object) rowColParams.Size;
        }
      }
      this.numeric_tLW.Value = Convert.ToDecimal(this.item.Bounds.Left);
      CustomNumericUpDown numericTW = this.numeric_tW;
      RectangleF bounds1 = this.item.Bounds;
      Decimal num1 = Convert.ToDecimal(bounds1.Width);
      numericTW.Value = num1;
      CustomNumericUpDown numericTRw = this.numeric_tRW;
      double width = (double) this.item.Page.Size.Width;
      bounds1 = this.item.Bounds;
      double left = (double) bounds1.Left;
      Decimal num2 = Convert.ToDecimal((float) (width - left) - this.item.Bounds.Width);
      numericTRw.Value = num2;
      this.numeric_tLH.Value = Convert.ToDecimal(this.item.Bounds.Top);
      CustomNumericUpDown numericTH = this.numeric_tH;
      RectangleF bounds2 = this.item.Bounds;
      Decimal num3 = Convert.ToDecimal(bounds2.Height);
      numericTH.Value = num3;
      CustomNumericUpDown numericTBh = this.numeric_tBH;
      double height1 = (double) this.item.Page.Size.Height;
      bounds2 = this.item.Bounds;
      double top = (double) bounds2.Top;
      double num4 = height1 - top;
      bounds2 = this.item.Bounds;
      double height2 = (double) bounds2.Height;
      Decimal num5 = Convert.ToDecimal((float) (num4 - height2));
      numericTBh.Value = num5;
    }
    finally
    {
      this.isUpdating = false;
    }
  }

  /// <summary>Внести изменения в редактируемую таблицу, указать, что есть изменения, обновить статус контролов</summary>
  protected virtual void ApplyChanges()
  {
    this.isChanged = true;
    this.item.SuspendUpdateLayout();
    this.item.SuspendUpdateGeometryRefreshUI();
    try
    {
      this.UpdateControls();
      this.CorrectTableRowHeights();
      this.CorrectTableColumnWidths();
      this.CorrectRowNums();
      this.SaveToTable(this.item);
    }
    finally
    {
      this.item.ResumeUpdateRefreshUI(false, false);
      this.item.ResumeUpdateLayout(true, true);
    }
    this.UpdateSizesFromTable();
  }

  /// <summary>Задать свойства указанной таблицы из элементов управления формы</summary>
  /// <param name="table">Редактируемая таблица</param>
  /// <returns>true, если свойства успешно скопированы</returns>
  protected virtual bool SaveToTable(TableElement table)
  {
    if (table == null)
      return false;
    try
    {
      table.BeginChangingStructure();
      RectangleF rectangleF = new RectangleF(Convert.ToSingle(this.numeric_tLW.Value), Convert.ToSingle(this.numeric_tLH.Value), Convert.ToSingle(this.numeric_tW.Value), Convert.ToSingle(this.numeric_tH.Value));
      if ((double) table.MinHeight != 0.0)
        table.AssignMinHeight(rectangleF.Height, false, false, false);
      if ((double) table.MaxHeight != 0.0)
        table.AssignMaxHeight(rectangleF.Height, false, false, false);
      table.AssignBounds(rectangleF, true, false, false);
      table.AssignDrawGridToBottom(this.cb_FullSizeGrid.Checked, false);
      if (this.radio_DynamicTable.Checked)
        table.IsPageFlow = this.cb_TableWrapping.Checked;
      else
        table.IsPageFlow = false;
      float defaultRowSize = table.DefaultRowSize;
      table.SetDefaultRowSize(Convert.ToSingle(this.numeric_GridRowHeight.Value), false, false, false, false);
      for (int index = 0; index < table.NodesCount; ++index)
      {
        if (table.Nodes[index] is TableData node)
        {
          if (node.TableCellType == CellType.DataCell)
          {
            if ((double) node.DefaultRowSize == (double) defaultRowSize)
              node.SetDefaultRowSize(Convert.ToSingle(this.numeric_GridRowHeight.Value), true, true, false, false);
          }
          else
            node.SetDefaultRowSize(0.0f, true, true, false, false);
        }
      }
    }
    finally
    {
      table.EndChangingStructure(true, true, false, true);
    }
    return true;
  }

  /// <summary>Обновить статусы всех контролов</summary>
  protected virtual void UpdateControls()
  {
    bool readOnly = this.item == null;
    this.SetHorizontalSizeControlsState(readOnly);
    this.SetVerticalSizeControlsState(readOnly);
    this.SetGridColumnsControlsState(readOnly);
    this.SetGridRowsControlsState(readOnly);
    this.SetControlsState(readOnly);
  }

  /// <summary>Установить корректные статусы остальным элементам управления</summary>
  /// <param name="readOnly">Можно ли редактировать что-либо в форме</param>
  protected virtual void SetControlsState(bool readOnly)
  {
    this.buttonFrameMode_1.Enabled = !readOnly;
    this.buttonFrameMode_2.Enabled = !readOnly;
    this.buttonFrameMode_3.Enabled = !readOnly;
    this.buttonFrameMode_4.Enabled = !readOnly;
    this.buttonFrameMode_5.Enabled = !readOnly;
    this.radio_StaticTable.Enabled = !readOnly;
    this.radio_DynamicTable.Enabled = !readOnly;
    this.cb_TableWrapping.Enabled = !readOnly && this.radio_DynamicTable.Checked;
    this.cb_FullSizeGrid.Enabled = !readOnly && this.radio_DynamicTable.Checked;
    this.numeric_GridRowHeight.ReadOnly = readOnly;
    this.numeric_HeaderRows.ReadOnly = readOnly;
    if (!this.hasInvalidHeight && !this.hasInvalidWidth)
    {
      this.errorProvider.Clear();
      this.labelWarning.Visible = false;
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.hasInvalidHeight)
        stringBuilder.Append(LocalizationHolder.rm.GetString("Document.Model_625"));
      if (this.hasInvalidWidth)
        stringBuilder.Append(LocalizationHolder.rm.GetString("Document.Model_626"));
      string str = stringBuilder.ToString();
      this.labelWarning.Visible = true;
      this.errorProvider.SetError((Control) this.labelWarning, str);
    }
    this.button_ColumnsInsertAfter.Enabled = !this.hasInvalidWidth;
    this.button_ColumnsInsertBefore.Enabled = !this.hasInvalidWidth;
    this.button_RowsInsertAfter.Enabled = !this.hasInvalidHeight;
    this.button_RowsInsertBefore.Enabled = !this.hasInvalidHeight;
    this.btnOK.Enabled = !readOnly && !this.hasInvalidHeight && !this.hasInvalidWidth;
    this.btnCancel.Enabled = true;
  }

  /// <summary>Установить корректные статусы элементам управления, связанным с колонками таблицы</summary>
  /// <param name="readOnly">Можно ли редактировать что-либо в форме</param>
  protected virtual void SetGridColumnsControlsState(bool readOnly)
  {
    iGRow row = (this.grid_Columns.SelectedCells.Count > 0 ? this.grid_Columns.SelectedCells[0] : (iGCell) null)?.Row;
    this.button_ColumnsRemove.Enabled = !readOnly && row != null && this.grid_Columns.Rows.Count > 1;
    this.button_ColumnsInsertBefore.Enabled = !readOnly;
    this.button_ColumnsInsertAfter.Enabled = !readOnly;
    this.grid_Columns.Enabled = !readOnly;
    this.numeric_ColumnsInsertCount.ReadOnly = readOnly;
    this.numeric_ColumnsInsertWidth.ReadOnly = readOnly;
    this.label_ColumnsCount.Text = string.Format(LocalizationHolder.rm.GetString("Document.Model_627"), (object) this.grid_Columns.Rows.Count);
  }

  /// <summary>Установить корректные статусы элементам управления, связанным со строками таблицы</summary>
  /// <param name="readOnly">Можно ли редактировать что-либо в форме</param>
  protected virtual void SetGridRowsControlsState(bool readOnly)
  {
    iGRow row = (this.grid_Rows.SelectedCells.Count > 0 ? this.grid_Rows.SelectedCells[0] : (iGCell) null)?.Row;
    this.button_RowsRemove.Enabled = !readOnly && row != null && this.grid_Rows.Rows.Count > 1;
    this.button_RowsInsertBefore.Enabled = !readOnly;
    this.button_RowsInsertAfter.Enabled = !readOnly;
    this.grid_Rows.Enabled = !readOnly;
    this.numeric_RowsInsertCount.ReadOnly = readOnly;
    this.numeric_RowsInsertHeight.ReadOnly = readOnly;
    if (this.numeric_GridRowHeight.Value != 0M)
      this.numeric_RowsInsertHeight.Increment = this.numeric_GridRowHeight.Value;
    else
      this.numeric_RowsInsertHeight.Increment = 1M;
    this.label_RowsCount.Text = string.Format(LocalizationHolder.rm.GetString("Document.Model_628"), (object) this.grid_Rows.Rows.Count);
  }

  /// <summary>Установить корректные цвета фона и статусы контролам, отвечающим за горизонтальные размеры
  /// таблицы и отступы от краёв листа справа и слева. Фон и статусы зависят от значений чек-боксов,
  /// связанных с этими контролами</summary>
  /// <param name="readOnly">Можно ли редактировать что-либо в форме</param>
  protected virtual void SetHorizontalSizeControlsState(bool readOnly)
  {
    this.numeric_tLW.BackColor = this.cb_tLW.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tLW.ReadOnly = !this.cb_tLW.Checked | readOnly;
    this.numeric_tLW.Enabled = !this.numeric_tLW.ReadOnly;
    this.cb_tLW.Enabled = !readOnly;
    this.numeric_tW.BackColor = this.cb_tW.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tW.ReadOnly = !this.cb_tW.Checked | readOnly;
    this.numeric_tW.Enabled = !this.numeric_tW.ReadOnly;
    this.cb_tW.Enabled = !readOnly;
    this.numeric_tRW.BackColor = this.cb_tRW.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tRW.ReadOnly = !this.cb_tRW.Checked | readOnly;
    this.numeric_tRW.Enabled = !this.numeric_tRW.ReadOnly;
    this.cb_tRW.Enabled = !readOnly;
  }

  /// <summary>Установить корректные цвета фона и статусы контролам, отвечающим за вертикальные размеры
  /// таблицы и отступы от краёв листа сверху и снизу. Фон и статусы зависят от значений чек-боксов,
  /// связанных с этими контролами</summary>
  /// <param name="readOnly">Можно ли редактировать что-либо в форме</param>
  protected virtual void SetVerticalSizeControlsState(bool readOnly)
  {
    this.numeric_tLH.BackColor = this.cb_tLH.Checked ? ControlColors.colorVerticalSizeActive : ControlColors.colorVerticalSizeInactive;
    this.numeric_tLH.ReadOnly = !this.cb_tLH.Checked | readOnly;
    this.numeric_tLH.Enabled = !this.numeric_tLH.ReadOnly;
    this.cb_tLH.Enabled = !readOnly;
    this.numeric_tH.BackColor = this.cb_tH.Checked ? ControlColors.colorVerticalSizeActive : ControlColors.colorVerticalSizeInactive;
    this.numeric_tH.ReadOnly = !this.cb_tH.Checked | readOnly;
    this.numeric_tH.Enabled = !this.numeric_tH.ReadOnly;
    this.cb_tH.Enabled = !readOnly;
    this.numeric_tBH.BackColor = this.cb_tBH.Checked ? ControlColors.colorVerticalSizeActive : ControlColors.colorVerticalSizeInactive;
    this.numeric_tBH.ReadOnly = !this.cb_tBH.Checked | readOnly;
    this.numeric_tBH.Enabled = !this.numeric_tBH.ReadOnly;
    this.cb_tBH.Enabled = !readOnly;
    this.numeric_HeaderRows.BackColor = ControlColors.colorHeaderCell;
  }

  /// <summary>Установить один из режимов рамки</summary>
  /// <param name="mode">Режимы - 1 .. 5. Если указать значение 0, режим будет неопределён</param>
  protected virtual void SetFrameMode(int mode)
  {
    if (mode == 0)
    {
      for (int index = 0; index < this.frameButtons.Count; ++index)
        this.frameButtons[index].FlatStyle = FlatStyle.Standard;
    }
    else
    {
      for (int index = 0; index < this.frameButtons.Count; ++index)
        this.frameButtons[index].FlatStyle = mode == index + 1 ? FlatStyle.Flat : FlatStyle.Standard;
      float num1 = 0.5f;
      float num2 = 0.0f;
      switch (mode)
      {
        case 1:
          BorderLine bl1 = new BorderLine(BorderStyles.SolidLine);
          bl1.Width = num2;
          this.item.TopBorderLine = bl1.Clone();
          this.item.LeftBorderLine = bl1.Clone();
          this.item.RightBorderLine = bl1.Clone();
          this.item.BottomBorderLine = bl1.Clone();
          bl1.Width = num2;
          BorderLineTE borderLineTe1 = new BorderLineTE(bl1);
          this.item.InnerHorizontalLineTE = borderLineTe1.Clone();
          this.item.InnerVerticalLineTE = borderLineTe1.Clone();
          int nodesCount1 = this.item.NodesCount;
          for (int index = 0; index < nodesCount1; ++index)
          {
            if (this.item.Nodes[index] is RectangleElement)
              (this.item.Nodes[index] as RectangleElement).BottomBorderLine = bl1.Clone();
          }
          break;
        case 2:
          BorderLine bl2 = new BorderLine(BorderStyles.SolidLine);
          bl2.Width = num1;
          this.item.TopBorderLine = bl2.Clone();
          this.item.LeftBorderLine = bl2.Clone();
          this.item.RightBorderLine = bl2.Clone();
          this.item.BottomBorderLine = bl2.Clone();
          bl2.Width = num2;
          BorderLineTE borderLineTe2 = new BorderLineTE(bl2);
          this.item.InnerHorizontalLineTE = borderLineTe2.Clone();
          this.item.InnerVerticalLineTE = borderLineTe2.Clone();
          int nodesCount2 = this.item.NodesCount;
          for (int index = 0; index < nodesCount2; ++index)
          {
            if (this.item.Nodes[index] is RectangleElement)
              (this.item.Nodes[index] as RectangleElement).BottomBorderLine = bl2.Clone();
          }
          break;
        case 3:
          BorderLine bl3 = new BorderLine(BorderStyles.SolidLine);
          bl3.Width = num1;
          this.item.TopBorderLine = bl3.Clone();
          this.item.LeftBorderLine = bl3.Clone();
          this.item.RightBorderLine = bl3.Clone();
          this.item.BottomBorderLine = bl3.Clone();
          bl3.Width = num2;
          BorderLineTE borderLineTe3 = new BorderLineTE(bl3);
          this.item.InnerHorizontalLineTE = borderLineTe3.Clone();
          this.item.InnerVerticalLineTE = borderLineTe3.Clone();
          int nodesCount3 = (int) this.numeric_HeaderRows.Value;
          if (this.item.NodesCount < nodesCount3)
            nodesCount3 = this.item.NodesCount;
          bl3.Width = num1;
          for (int index = 0; index < nodesCount3; ++index)
          {
            if (this.item.Nodes[index] is RectangleElement)
              (this.item.Nodes[index] as RectangleElement).BottomBorderLine = bl3.Clone();
          }
          break;
        case 4:
          BorderLine bl4 = new BorderLine(BorderStyles.SolidLine);
          bl4.Width = num1;
          this.item.TopBorderLine = bl4.Clone();
          this.item.LeftBorderLine = bl4.Clone();
          this.item.RightBorderLine = bl4.Clone();
          this.item.BottomBorderLine = bl4.Clone();
          bl4.Width = num2;
          BorderLineTE borderLineTe4 = new BorderLineTE(bl4);
          this.item.InnerHorizontalLineTE = borderLineTe4.Clone();
          borderLineTe4.WidthTE = new float?(num1);
          this.item.InnerVerticalLineTE = borderLineTe4.Clone();
          int nodesCount4 = (int) this.numeric_HeaderRows.Value;
          if (this.item.NodesCount < nodesCount4)
            nodesCount4 = this.item.NodesCount;
          bl4.Width = num1;
          for (int index = 0; index < nodesCount4; ++index)
          {
            if (this.item.Nodes[index] is RectangleElement)
              (this.item.Nodes[index] as RectangleElement).BottomBorderLine = bl4.Clone();
          }
          break;
        case 5:
          BorderLine bl5 = new BorderLine(BorderStyles.None);
          this.item.TopBorderLine = bl5.Clone();
          this.item.LeftBorderLine = bl5.Clone();
          this.item.RightBorderLine = bl5.Clone();
          this.item.BottomBorderLine = bl5.Clone();
          BorderLineTE borderLineTe5 = new BorderLineTE(bl5);
          this.item.InnerVerticalLineTE = borderLineTe5.Clone();
          this.item.InnerHorizontalLineTE = borderLineTe5.Clone();
          int nodesCount5 = this.item.NodesCount;
          for (int index = 0; index < nodesCount5; ++index)
          {
            if (this.item.Nodes[index] is RectangleElement)
              (this.item.Nodes[index] as RectangleElement).BottomBorderLine = bl5.Clone();
          }
          break;
        case 6:
          BorderLine bl6 = new BorderLine(BorderStyles.SolidLine);
          bl6.Width = num1;
          this.item.TopBorderLine = bl6.Clone();
          this.item.LeftBorderLine = bl6.Clone();
          this.item.RightBorderLine = bl6.Clone();
          this.item.BottomBorderLine = bl6.Clone();
          bl6.Width = num1;
          BorderLineTE borderLineTe6 = new BorderLineTE(bl6);
          borderLineTe6.WidthTE = new float?(num1);
          this.item.InnerHorizontalLineTE = borderLineTe6.Clone();
          this.item.InnerVerticalLineTE = borderLineTe6.Clone();
          int nodesCount6 = this.item.NodesCount;
          bl6.Width = num1;
          for (int index = 0; index < nodesCount6; ++index)
          {
            if (this.item.Nodes[index] is RectangleElement)
              (this.item.Nodes[index] as RectangleElement).BottomBorderLine = bl6.Clone();
          }
          break;
      }
    }
  }

  /// <summary>Отыскать, какая из кнопок "нажата", вернуть её порядковый номер в списке (1 .. 5).
  /// 0 означает, что ни одна из кнопок не "нажата"</summary>
  /// <returns>Порядковый номер нажатой кнопки (1 .. 5) или 0, если нет такой кнопки</returns>
  protected virtual int GetFrameMode()
  {
    for (int index = 0; index < this.frameButtons.Count; ++index)
    {
      if (this.frameButtons[index].FlatStyle == FlatStyle.Flat)
        return index + 1;
    }
    return 0;
  }

  /// <summary>Выполнить автоматический расчёт ширины таблицы, отступов, колонок, откорректировать значения контролов</summary>
  protected virtual void AutoCalcHorizontalSizes()
  {
    if (this.item == null)
      return;
    if (this.isUpdating)
      return;
    try
    {
      this.isUpdating = true;
      Decimal num1 = Convert.ToDecimal(this.item.Page.Size.Width);
      Decimal num2 = this.cb_tLW.Checked ? this.numeric_tLW.Value : -1M;
      Decimal num3 = this.cb_tRW.Checked ? this.numeric_tRW.Value : -1M;
      Decimal num4 = this.cb_tW.Checked ? this.numeric_tW.Value : -1M;
      if (num2 == -1M)
      {
        Decimal num5 = num1 - num4 - num3;
        num2 = num5 < 0M ? 0M : num5;
      }
      else if (num3 == -1M)
      {
        Decimal num6 = num1 - num4 - num2;
        num3 = num6 < 0M ? 0M : num6;
      }
      else if (num4 == -1M)
      {
        Decimal num7 = num1 - num2 - num3;
        num4 = num7 < 0M ? 0M : num7;
      }
      this.numeric_tLW.Value = num2;
      this.numeric_tW.Value = num4;
      this.numeric_tRW.Value = num3;
    }
    finally
    {
      this.isUpdating = false;
    }
  }

  /// <summary>Выполнить автоматический расчёт высоты таблицы, отступов, строк, откорректировать значения контролов</summary>
  protected virtual void AutoCalcVerticalSizes()
  {
    if (this.item == null)
      return;
    if (this.isUpdating)
      return;
    try
    {
      this.isUpdating = true;
      Decimal num1 = Convert.ToDecimal(this.item.Page.Size.Height);
      Decimal num2 = this.cb_tLH.Checked ? this.numeric_tLH.Value : -1M;
      Decimal num3 = this.cb_tBH.Checked ? this.numeric_tBH.Value : -1M;
      Decimal num4 = this.cb_tH.Checked ? this.numeric_tH.Value : -1M;
      if (num2 == -1M)
      {
        Decimal num5 = num1 - num4 - num3;
        num2 = num5 < 0M ? 0M : num5;
      }
      else if (num3 == -1M)
      {
        Decimal num6 = num1 - num4 - num2;
        num3 = num6 < 0M ? 0M : num6;
      }
      else if (num4 == -1M)
      {
        Decimal num7 = num1 - num2 - num3;
        num4 = num7 < 0M ? 0M : num7;
      }
      this.numeric_tLH.Value = num2;
      this.numeric_tH.Value = num4;
      this.numeric_tBH.Value = num3;
    }
    finally
    {
      this.isUpdating = false;
      this.CorrectTableRowHeights();
    }
  }

  /// <summary>Проверить грид, управляющий колонкам в таблице, исправить "галочки", если есть ошибки</summary>
  protected virtual void CorrectTableColumnWidths()
  {
    if (this.item == null)
      return;
    if (this.isUpdating)
      return;
    try
    {
      this.isUpdating = true;
      List<RowColParams> gridColumnsParams = this.item.GridColumnsParams;
      int count = this.grid_Columns.Rows.Count;
      if (count <= 0)
        return;
      int num1 = 0;
      float single1 = Convert.ToSingle(this.numeric_tW.Value);
      float num2 = 0.0f;
      for (int index = 0; index < count; ++index)
      {
        if (this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnCheck].Value.Equals((object) true))
          ++num1;
      }
      if (num1 == count)
      {
        this.grid_Columns.Rows[count - 1].Cells[GridColumnKeys.column_ColumnCheck].Value = (object) false;
        --num1;
      }
      for (int index = 0; index < count; ++index)
      {
        if (this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnCheck].Value.Equals((object) true))
          num2 += Convert.ToSingle(this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnWidth].Value);
      }
      float num3 = single1 - num2;
      float num4 = (double) num3 <= 0.0 || count - num1 <= 0 ? 0.0f : num3 / (float) (count - num1);
      this.hasInvalidWidth = (double) num4 <= 0.0;
      for (int index = 0; index < count; ++index)
      {
        RowColParams rowColParams = gridColumnsParams[index];
        int num5 = this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnCheck].Value.Equals((object) true) ? 1 : 0;
        float single2 = Convert.ToSingle(this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnWidth].Value);
        float width = num5 != 0 ? single2 : num4;
        this.item.SetGridColumnWidth(index, width, true, true, false);
        this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnWidth].Value = (object) width;
        if (num5 != 0)
        {
          this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnWidth].Style.ReadOnly = iGBool.False;
          this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnWidth].Style.BackColor = this.grid_Rows.BackColor;
        }
        else
        {
          this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnWidth].Style.ReadOnly = iGBool.True;
          this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnWidth].Style.BackColor = (double) width > 0.0 ? ControlColors.colorDisabledCell : ControlColors.colorErrorCell;
        }
      }
    }
    finally
    {
      this.isUpdating = false;
    }
  }

  /// <summary>Проверить грид, управляющий строками в таблице, исправить "галочки", если есть ошибки</summary>
  protected virtual void CorrectTableRowHeights()
  {
    if (this.item == null)
      return;
    if (this.isUpdating)
      return;
    try
    {
      this.isUpdating = true;
      float single1 = Convert.ToSingle(this.numeric_GridRowHeight.Value);
      int int32 = Convert.ToInt32(this.numeric_HeaderRows.Value);
      int count = this.grid_Rows.Rows.Count;
      for (int index = 0; index < count; ++index)
      {
        TableElement node = this.item.Nodes[index] as TableElement;
        if (index < int32)
        {
          node.TableCellType = CellType.Header;
          node.CloneByTemplateWithParent = true;
          node.SetDefaultRowSize(0.0f, true, true, false, false);
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowNumber].Style.BackColor = ControlColors.colorHeaderCell;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Style.BackColor = ControlColors.colorHeaderCell;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowName].Style.BackColor = ControlColors.colorHeaderCell;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.BackColor = ControlColors.colorHeaderCell;
        }
        else
        {
          node.SetTableCellType(CellType.DataCell, false, false);
          node.CloneByTemplateWithParent = !this.radio_DynamicTable.Checked;
          if ((double) node.DefaultRowSize == 0.0 || (double) node.DefaultRowSize == (double) this.item.DefaultRowSize)
          {
            node.SetDefaultRowSize(0.0f, true, true, false, false);
            node.overrideFlags &= ~OverrideFlags.DefaultRowSize;
            node.overrideFlags2 &= ~OverrideFlags2.ParentDefaultRowSize;
          }
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowNumber].Style.BackColor = this.grid_Rows.BackColor;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Style.BackColor = this.grid_Rows.BackColor;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowName].Style.BackColor = this.grid_Rows.BackColor;
        }
      }
      if (this.radio_DynamicTable.Checked)
      {
        for (int index = 0; index < count; ++index)
        {
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Value = (object) true;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Style.ReadOnly = iGBool.True;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Style.BackColor = ControlColors.colorDisabledCell;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.ReadOnly = iGBool.False;
          if (index < int32)
            this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.BackColor = ControlColors.colorHeaderCell;
          else
            this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.BackColor = this.grid_Rows.BackColor;
          TableElement node = this.item.Nodes[index] as TableElement;
          float height = Convert.ToSingle(this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Value);
          float increment = Convert.ToSingle(this.numeric_GridRowHeight.Value);
          if (index < this.item.NodesCount && this.item.Nodes[index] is RectangleElement)
          {
            float defaultRowSize = (this.item.Nodes[index] as RectangleElement).DefaultRowSize;
            if ((double) defaultRowSize != 0.0)
              increment = defaultRowSize;
          }
          if ((double) height > 0.0 && (double) single1 != 0.0 && index >= int32)
          {
            float num = this.RoundHeight(height, increment, false);
            if ((double) num != (double) height)
            {
              this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Value = (object) Convert.ToDecimal(num);
              height = num;
            }
          }
          RectangleF newBounds = new RectangleF(node.Location, new SizeF(node.Size.Width, height));
          node.AssignMinHeight(newBounds.Height, false, false, false);
          if ((double) node.MaxHeight != 0.0 && (double) node.MaxHeight < (double) newBounds.Height)
            node.AssignMaxHeight(newBounds.Height, false, false, false);
          node.SetCellSizes(newBounds, false, true, true, true, false);
        }
      }
      else
      {
        if (count <= 0)
          return;
        int num1 = 0;
        float single2 = Convert.ToSingle(this.numeric_tH.Value);
        float num2 = 0.0f;
        for (int index = 0; index < count; ++index)
        {
          if (this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Value.Equals((object) true))
            ++num1;
        }
        if (num1 == count)
        {
          this.grid_Rows.Rows[count - 1].Cells[GridColumnKeys.column_RowCheck].Value = (object) false;
          --num1;
        }
        for (int index = 0; index < count; ++index)
        {
          float num3 = Convert.ToSingle(this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Value);
          if ((double) num3 > 0.0 && (double) single1 != 0.0 && index >= int32)
          {
            float increment = Convert.ToSingle(this.numeric_GridRowHeight.Value);
            if (index < this.item.NodesCount && this.item.Nodes[index] is RectangleElement)
            {
              float defaultRowSize = (this.item.Nodes[index] as RectangleElement).DefaultRowSize;
              if ((double) defaultRowSize != 0.0)
                increment = defaultRowSize;
            }
            float num4 = this.RoundHeight(num3, increment, false);
            if ((double) num4 != (double) num3)
            {
              this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Value = (object) Convert.ToDecimal(num4);
              num3 = num4;
            }
          }
          if (this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Value.Equals((object) true))
            num2 += num3;
        }
        float num5 = single2 - num2;
        float height = (double) num5 <= 0.0 || count - num1 <= 0 ? 0.0f : num5 / (float) (count - num1);
        this.hasInvalidHeight = (double) height <= 0.0;
        for (int index = 0; index < count; ++index)
        {
          TableElement node = this.item.Nodes[index] as TableElement;
          int num6 = this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Value.Equals((object) true) ? 1 : 0;
          float single3 = Convert.ToSingle(this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Value);
          SizeF size1;
          RectangleF rectangleF1;
          if (num6 == 0)
          {
            PointF location = node.Location;
            size1 = node.Size;
            SizeF size2 = new SizeF(size1.Width, height);
            rectangleF1 = new RectangleF(location, size2);
          }
          else
          {
            PointF location = node.Location;
            size1 = node.Size;
            SizeF size3 = new SizeF(size1.Width, single3);
            rectangleF1 = new RectangleF(location, size3);
          }
          RectangleF rectangleF2 = rectangleF1;
          node.AssignMinHeight(rectangleF2.Height, false, false, false);
          if ((double) node.MaxHeight != 0.0 && (double) node.MaxHeight < (double) rectangleF2.Height)
            node.AssignMaxHeight(rectangleF2.Height, false, false, false);
          node.SetCellSizes(rectangleF2, false, true, true, true, false);
          rectangleF2 = UnitsConverter.RoundPectangle(rectangleF2, 5);
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Value = (object) rectangleF2.Height;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Style.ReadOnly = iGBool.False;
          this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowCheck].Style.BackColor = this.grid_Rows.BackColor;
          if (num6 != 0)
          {
            this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.ReadOnly = iGBool.False;
            if (index < int32)
              this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.BackColor = ControlColors.colorHeaderCell;
            else
              this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.BackColor = this.grid_Rows.BackColor;
          }
          else
          {
            this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.ReadOnly = iGBool.True;
            this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowHeight].Style.BackColor = (double) rectangleF2.Height > 0.0 ? ControlColors.colorDisabledCell : ControlColors.colorErrorCell;
          }
        }
      }
    }
    finally
    {
      this.isUpdating = false;
    }
  }

  /// <summary>Перенумеровать строки</summary>
  protected virtual void CorrectRowNums()
  {
    if (this.item == null || this.isUpdating)
      return;
    int num;
    for (int index = 0; index < this.grid_Columns.Rows.Count; ++index)
    {
      iGCell cell = this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnNumber];
      num = index + 1;
      string str = num.ToString();
      cell.Value = (object) str;
    }
    for (int index = 0; index < this.grid_Rows.Rows.Count; ++index)
    {
      iGCell cell = this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowNumber];
      num = index + 1;
      string str = num.ToString();
      cell.Value = (object) str;
    }
  }

  /// <summary>Кликнут один из чек-боксов, отвечающих за горизонтальные размеры и отступы</summary>
  /// <param name="sender">Отправитель (чек-бокс)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeHorizontalSizeChecks(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    CheckBox checkBox = sender as CheckBox;
    try
    {
      this.isUpdating = true;
      if (checkBox == this.cb_tLW)
      {
        if (!checkBox.Checked)
        {
          this.cb_tW.Checked = true;
          this.cb_tRW.Checked = true;
        }
        else
        {
          this.cb_tW.Checked = true;
          this.cb_tRW.Checked = false;
        }
      }
      if (checkBox == this.cb_tW)
      {
        if (!checkBox.Checked)
        {
          this.cb_tLW.Checked = true;
          this.cb_tRW.Checked = true;
        }
        else
        {
          this.cb_tLW.Checked = true;
          this.cb_tRW.Checked = false;
        }
      }
      if (checkBox == this.cb_tRW)
      {
        if (!checkBox.Checked)
        {
          this.cb_tLW.Checked = true;
          this.cb_tW.Checked = true;
        }
        else
        {
          this.cb_tLW.Checked = false;
          this.cb_tW.Checked = true;
        }
      }
      this.SetHorizontalSizeControlsState(false);
    }
    finally
    {
      this.isUpdating = false;
      this.AutoCalcHorizontalSizes();
      this.ApplyChanges();
    }
  }

  /// <summary>Кликнут один из чек-боксов, отвечающих за вертикальные размеры и отступы</summary>
  /// <param name="sender">Отправитель (чек-бокс)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeVerticalSizeChecks(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    CheckBox checkBox = sender as CheckBox;
    try
    {
      this.isUpdating = true;
      if (checkBox == this.cb_tLH)
      {
        if (!checkBox.Checked)
        {
          this.cb_tH.Checked = true;
          this.cb_tBH.Checked = true;
        }
        else
        {
          this.cb_tH.Checked = true;
          this.cb_tBH.Checked = false;
        }
      }
      if (checkBox == this.cb_tH)
      {
        if (!checkBox.Checked)
        {
          this.cb_tLH.Checked = true;
          this.cb_tBH.Checked = true;
        }
        else
        {
          this.cb_tLH.Checked = true;
          this.cb_tBH.Checked = false;
        }
      }
      if (checkBox == this.cb_tBH)
      {
        if (!checkBox.Checked)
        {
          this.cb_tLH.Checked = true;
          this.cb_tH.Checked = true;
        }
        else
        {
          this.cb_tLH.Checked = false;
          this.cb_tH.Checked = true;
        }
      }
      this.SetVerticalSizeControlsState(false);
    }
    finally
    {
      this.isUpdating = false;
      this.AutoCalcVerticalSizes();
      this.ApplyChanges();
    }
  }

  /// <summary>Изменился один из горизонтальных размеров или отступов</summary>
  /// <param name="sender">Отправитель (редактор текста)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeHorizontalSizes(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    this.AutoCalcHorizontalSizes();
    this.ApplyChanges();
  }

  /// <summary>Изменился один из вертикальных размеров или отступов</summary>
  /// <param name="sender">Отправитель (редактор текста)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeVerticalSizes(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    this.AutoCalcVerticalSizes();
    this.ApplyChanges();
  }

  /// <summary>Нажата одна из кнопок, отвечающих за управление рамками в таблице</summary>
  /// <param name="sender">Отправитель (кнопка)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeFrameMode(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    Button button = sender as Button;
    try
    {
      this.isUpdating = true;
      this.SetFrameMode(this.frameButtons.IndexOf(button) + 1);
    }
    finally
    {
      this.isUpdating = false;
      this.ApplyChanges();
    }
  }

  /// <summary>Изменилось состояние одного из контролов, которое влияет на высоту строк в таблице</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoUpdateRowsAndState(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    if (sender == this.radio_DynamicTable && this.radio_DynamicTable.Checked)
    {
      this.cb_TableWrapping.Checked = true;
      if (this.item.NodesCount >= 1 && this.item.Nodes[this.item.NodesCount - 1] is TableData)
        this.grid_Rows.Rows[this.item.NodesCount - 1].Cells[GridColumnKeys.column_RowHeight].Value = (object) this.numeric_RowsInsertHeight.Value;
    }
    if (sender == this.numeric_GridRowHeight)
      this.Set_RowsInsertHeight_Value();
    this.ApplyChanges();
  }

  /// <summary>Изменилось количество строк в заголовке таблицы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoUpdateHeaderRows(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    this.CorrectTableRowHeights();
    this.SetFrameMode(this.GetFrameMode());
    this.ApplyChanges();
  }

  /// <summary>Добавить колонки в указанную позицию. Количество, ширина и прочие параметры берутся из контролов</summary>
  /// <param name="insertIndex">Позиция для вставки колонок</param>
  protected virtual void InsertColumns(int insertIndex)
  {
    int num1 = Convert.ToInt32(this.numeric_ColumnsInsertCount.Value);
    float single = Convert.ToSingle(this.numeric_ColumnsInsertWidth.Value);
    List<RowColParams> gridColumnsParams = this.item.GridColumnsParams;
    if (gridColumnsParams != null && gridColumnsParams.Count > 0)
    {
      float num2 = 0.0f;
      for (int index = 0; index < gridColumnsParams.Count; ++index)
      {
        if (this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnCheck].Value.Equals((object) false))
          num2 += gridColumnsParams[index].Size;
      }
      if ((double) single * (double) num1 > (double) num2)
        num1 = (int) Math.Floor((double) num2 / (double) single);
      if ((double) single * (double) num1 == (double) num2)
        --num1;
    }
    for (int index1 = 0; index1 < num1; ++index1)
    {
      this.item.InsertNewGridColumn(insertIndex, false, false);
      this.item.SetGridColumnWidth(insertIndex, single, true, false, false);
      for (int index2 = 0; index2 < this.item.Nodes.Count; ++index2)
      {
        TableElement node = this.item.Nodes[index2] as TableElement;
        node.SetVisible(false, false, false, false, false, false);
        node.SetVisible(true, false, false, false, true, false);
      }
      iGRow iGrow = this.grid_Columns.Rows.Insert(insertIndex);
      iGrow.Key = index1.ToString();
      iGrow.Cells[GridColumnKeys.column_ColumnNumber].Style = new iGCellStyle();
      iGrow.Cells[GridColumnKeys.column_ColumnNumber].Selectable = iGBool.False;
      iGrow.Cells[GridColumnKeys.column_ColumnNumber].Style.ReadOnly = iGBool.True;
      iGrow.Cells[GridColumnKeys.column_ColumnNumber].Style.TextAlign = iGContentAlignment.MiddleRight;
      iGrow.Cells[GridColumnKeys.column_ColumnNumber].Style.BackColor = this.grid_Columns.Header.BackColor;
      iGrow.Cells[GridColumnKeys.column_ColumnCheck].ValueType = typeof (bool);
      iGrow.Cells[GridColumnKeys.column_ColumnCheck].Value = (object) true;
      iGrow.Cells[GridColumnKeys.column_ColumnName].ValueType = typeof (string);
      iGrow.Cells[GridColumnKeys.column_ColumnName].Value = (object) string.Empty;
      iGrow.Cells[GridColumnKeys.column_ColumnWidth].ValueType = typeof (float);
      iGrow.Cells[GridColumnKeys.column_ColumnWidth].Value = (object) single;
    }
  }

  /// <summary>Добавить указанное количество колонок перед колонкой, выделенной в гриде</summary>
  /// <param name="sender">Отправитель (кнопка)</param>
  /// <param name="e">Аргументы события</param>
  private void DoInsertColsBefore(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    int index1 = -1;
    try
    {
      this.isUpdating = true;
      this.item.SuspendUpdateLayout();
      this.item.SuspendUpdateGeometryRefreshUI();
      iGRow row = this.grid_Columns.CurCell != null ? this.grid_Columns.CurCell.Row : (iGRow) null;
      int index2 = row == null || row.Index <= 0 ? 0 : row.Index;
      this.InsertColumns(index2);
      index1 = index2 + Convert.ToInt32(this.numeric_ColumnsInsertCount.Value) - 1;
    }
    finally
    {
      this.SetFrameMode(this.GetFrameMode());
      this.item.SetNeedUpdateLayoutFlag(true, true, false, false, true);
      this.item.ResumeUpdateLayout(false, true);
      this.isUpdating = false;
      this.ApplyChanges();
      this.item.ResumeUpdateRefreshUI(true, true);
      if (this.grid_Columns.Rows.Count > index1)
      {
        this.grid_Columns.Rows[index1].EnsureVisible();
        this.grid_Columns.Rows[index1].Cells[GridColumnKeys.column_ColumnName].Selected = true;
      }
    }
  }

  /// <summary>Добавить указанное количество колонок после колонки, выделенной в гриде</summary>
  /// <param name="sender">Отправитель (кнопка)</param>
  /// <param name="e">Аргументы события</param>
  private void DoInsertColsAfter(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    int index = -1;
    try
    {
      this.isUpdating = true;
      this.item.SuspendUpdateLayout();
      this.item.SuspendUpdateGeometryRefreshUI();
      iGRow row = this.grid_Columns.CurCell != null ? this.grid_Columns.CurCell.Row : (iGRow) null;
      int insertIndex = row == null || row.Index <= 0 ? this.grid_Columns.Rows.Count : row.Index + 1;
      this.InsertColumns(insertIndex);
      index = insertIndex + Convert.ToInt32(this.numeric_ColumnsInsertCount.Value) - 1;
    }
    finally
    {
      this.SetFrameMode(this.GetFrameMode());
      this.item.SetNeedUpdateLayoutFlag(true, true, false, false, true);
      this.item.ResumeUpdateLayout(false, true);
      this.isUpdating = false;
      this.ApplyChanges();
      this.item.ResumeUpdateRefreshUI(true, true);
      if (this.grid_Columns.Rows.Count > index)
      {
        this.grid_Columns.Rows[index].EnsureVisible();
        this.grid_Columns.Rows[index].Cells[GridColumnKeys.column_ColumnName].Selected = true;
      }
    }
  }

  /// <summary>Удалить указанные колонки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoColsRemove(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    if (this.grid_Columns.SelectedCells.Count == 0)
      return;
    try
    {
      this.isUpdating = true;
      this.item.SuspendUpdateLayout();
      this.item.SuspendUpdateGeometryRefreshUI();
      List<int> intList = new List<int>(this.grid_Columns.SelectedCells.Count);
      int num = -1;
      for (int index = 0; index < this.grid_Columns.SelectedCells.Count; ++index)
      {
        iGRow row = this.grid_Columns.SelectedCells[index].Row;
        if (!intList.Contains(row.Index))
        {
          intList.Add(row.Index);
          num = Math.Max(num, row.Index);
        }
      }
      intList.Sort();
      for (int index = intList.Count - 1; index >= 0 && this.grid_Columns.Rows.Count != 1; --index)
      {
        this.item.RemoveGridColumn(intList[index], false, false, false);
        this.grid_Columns.Rows.RemoveAt(intList[index]);
      }
      if (num > this.grid_Columns.Rows.Count - 1)
        num = this.grid_Columns.Rows.Count - 1;
      if (num < 0 || num > this.grid_Columns.Rows.Count - 1)
        return;
      for (int colIndex = 0; colIndex < this.grid_Columns.Cols.Count; ++colIndex)
        this.grid_Columns.Rows[num].Cells[colIndex].Selected = true;
      this.grid_Columns.SetCurRow(num);
    }
    finally
    {
      this.item.ResumeUpdateLayout(false, true);
      this.isUpdating = false;
      this.ApplyChanges();
      this.item.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Событие вызывается перед завершением редактирования значений в таблице, управляющей колонками</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_Columns_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    if (e.ColIndex == this.grid_Columns.Cols[GridColumnKeys.column_ColumnCheck].Index)
      this.ApplyChanges();
    if (e.ColIndex == this.grid_Columns.Cols[GridColumnKeys.column_ColumnName].Index)
    {
      List<RowColParams> gridColumnsParams = this.item.GridColumnsParams;
      gridColumnsParams[e.RowIndex].ColRowName = e.NewValue?.ToString();
      if (Convert.ToInt32(this.numeric_HeaderRows.Value) > 0 && this.item.Nodes.Count > 0)
      {
        RectangleElement[] cells;
        ((TableData) this.item.Nodes[0]).GetCellPositionForGridColumn(e.RowIndex, true, out cells);
        if (cells != null && cells.Length != 0 && cells[0] is TextData textData && string.IsNullOrEmpty(textData.Text))
          textData.AssignText(gridColumnsParams[e.RowIndex].ColRowName, false, true, true, false, false);
      }
      e.Result = iGEditResult.Commit;
      this.ApplyChanges();
    }
    if (e.ColIndex != this.grid_Columns.Cols[GridColumnKeys.column_ColumnWidth].Index)
      return;
    float result;
    if (e.NewValue == null || !float.TryParse(e.NewValue.ToString(), out result) || (double) result <= 0.0)
    {
      e.Result = iGEditResult.Proceed;
    }
    else
    {
      this.item.SetGridColumnWidth(e.RowIndex, result, true, false, false);
      e.Result = iGEditResult.Commit;
      this.ApplyChanges();
    }
  }

  /// <summary>Событие вызывается после завершения редактирования значений в таблице, управляющей колонками</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    this.ApplyChanges();
  }

  /// <summary>Изменились выделенные строки в гриде, управляющем колонками таблицы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_Columns_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Добавить строки в указанную позицию. Количество, высота и прочие параметры берутся из контролов</summary>
  /// <param name="insertIndex">Позиция для вставки строк</param>
  protected virtual void InsertRows(int insertIndex)
  {
    int int32_1 = Convert.ToInt32(this.numeric_RowsInsertCount.Value);
    float single = Convert.ToSingle(this.numeric_RowsInsertHeight.Value);
    RectangleF newBounds = new RectangleF(this.item.Location.X, 0.0f, this.item.Size.Width, single);
    int int32_2 = Convert.ToInt32(this.numeric_HeaderRows.Value);
    for (int index = 0; index < int32_1; ++index)
    {
      this.item.InsertNewRow(insertIndex, (RectangleElement) null, false, false);
      RectangleElement node1 = (RectangleElement) this.item.Nodes[insertIndex];
      if (insertIndex < int32_2)
      {
        node1.TableCellType = CellType.Header;
        node1.CloneByTemplateWithParent = true;
        if (insertIndex != int32_2 - 1 && this.item.NodesCount > int32_2 - 1)
        {
          RectangleElement node2 = (RectangleElement) this.item.Nodes[int32_2 - 1];
          node2.SetTableCellType(CellType.DataCell, false, false);
          node2.CloneByTemplateWithParent = !this.radio_DynamicTable.Checked;
        }
      }
      else
        node1.CloneByTemplateWithParent = !this.radio_DynamicTable.Checked;
      TableElement node3 = this.item.Nodes[insertIndex] as TableElement;
      node3.Tag = (object) false;
      node3.SetVisible(false, false, false, false, false, false);
      node3.SetVisible(true, false, false, false, true, false);
      node3.AssignMinHeight(newBounds.Height, false, false, false);
      node3.SetCellSizes(newBounds, false, true, true, true, false);
      if ((double) this.item.DefaultRowSize != 0.0 && (double) newBounds.Height % (double) this.item.DefaultRowSize != 0.0)
        node3.SetDefaultRowSize(newBounds.Height, true, true, false, false);
      iGRow iGrow = this.grid_Rows.Rows.Insert(insertIndex);
      iGrow.Key = iGrow.Index.ToString();
      iGrow.Cells[GridColumnKeys.column_RowNumber].Style = new iGCellStyle();
      iGrow.Cells[GridColumnKeys.column_RowNumber].Selectable = iGBool.False;
      iGrow.Cells[GridColumnKeys.column_RowNumber].Style.ReadOnly = iGBool.True;
      iGrow.Cells[GridColumnKeys.column_RowNumber].Style.TextAlign = iGContentAlignment.MiddleRight;
      iGrow.Cells[GridColumnKeys.column_RowNumber].Style.BackColor = this.grid_Rows.Header.BackColor;
      iGrow.Cells[GridColumnKeys.column_RowCheck].ValueType = typeof (bool);
      iGrow.Cells[GridColumnKeys.column_RowCheck].Value = (object) true;
      iGrow.Cells[GridColumnKeys.column_RowName].ValueType = typeof (string);
      iGrow.Cells[GridColumnKeys.column_RowName].Value = node3.Name != null ? (object) node3.Name : (object) string.Empty;
      iGrow.Cells[GridColumnKeys.column_RowHeight].ValueType = typeof (float);
      iGrow.Cells[GridColumnKeys.column_RowHeight].Value = (object) single;
    }
  }

  /// <summary>Добавить указанное количество строк перед строкой, выделенной в гриде</summary>
  /// <param name="sender">Отправитель (кнопка)</param>
  /// <param name="e">Аргументы события</param>
  private void DoInsertRowsBefore(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    int index1 = -1;
    try
    {
      this.isUpdating = true;
      this.item.SuspendUpdateLayout();
      this.item.SuspendUpdateGeometryRefreshUI();
      iGRow row = this.grid_Rows.CurCell != null ? this.grid_Rows.CurCell.Row : (iGRow) null;
      int index2 = row == null || row.Index <= 0 ? 0 : row.Index;
      this.InsertRows(index2);
      index1 = index2 + Convert.ToInt32(this.numeric_RowsInsertCount.Value) - 1;
    }
    finally
    {
      this.SetFrameMode(this.GetFrameMode());
      this.item.SetNeedUpdateLayoutFlag(true, true, false, false, true);
      this.item.ResumeUpdateLayout(false, true);
      this.isUpdating = false;
      this.ApplyChanges();
      this.item.ResumeUpdateRefreshUI(true, true);
      if (this.grid_Rows.Rows.Count > index1)
      {
        this.grid_Rows.Rows[index1].EnsureVisible();
        this.grid_Rows.Rows[index1].Cells[GridColumnKeys.column_RowName].Selected = true;
      }
    }
  }

  /// <summary>Добавить указанное количество строк после строки, выделенной в гриде</summary>
  /// <param name="sender">Отправитель (кнопка)</param>
  /// <param name="e">Аргументы события</param>
  private void DoInsertRowsAfter(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    int index = -1;
    try
    {
      this.isUpdating = true;
      this.item.SuspendUpdateLayout();
      this.item.SuspendUpdateGeometryRefreshUI();
      iGRow row = this.grid_Rows.CurCell != null ? this.grid_Rows.CurCell.Row : (iGRow) null;
      int insertIndex = row == null || row.Index <= 0 ? this.grid_Rows.Rows.Count : row.Index + 1;
      this.InsertRows(insertIndex);
      index = insertIndex + Convert.ToInt32(this.numeric_RowsInsertCount.Value) - 1;
    }
    finally
    {
      this.SetFrameMode(this.GetFrameMode());
      this.item.SetNeedUpdateLayoutFlag(true, true, false, false, true);
      this.item.ResumeUpdateLayout(false, true);
      this.isUpdating = false;
      this.ApplyChanges();
      this.item.ResumeUpdateRefreshUI(true, true);
      if (this.grid_Rows.Rows.Count > index)
      {
        this.grid_Rows.Rows[index].EnsureVisible();
        this.grid_Rows.Rows[index].Cells[GridColumnKeys.column_RowName].Selected = true;
      }
    }
  }

  /// <summary>Удалить из таблицы строки, выделенные в гриде</summary>
  /// <param name="sender">Отправитель (кнопка)</param>
  /// <param name="e">Аргументы события</param>
  private void DoRowsRemove(object sender, EventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    if (this.grid_Rows.SelectedCells.Count == 0)
      return;
    try
    {
      this.isUpdating = true;
      this.item.SuspendUpdateLayout();
      this.item.SuspendUpdateGeometryRefreshUI();
      List<int> intList = new List<int>(this.grid_Rows.SelectedCells.Count);
      int num = -1;
      for (int index = 0; index < this.grid_Rows.SelectedCells.Count; ++index)
      {
        iGRow row = this.grid_Rows.SelectedCells[index].Row;
        if (!intList.Contains(row.Index))
        {
          intList.Add(row.Index);
          num = Math.Max(num, row.Index);
        }
      }
      intList.Sort();
      for (int index = intList.Count - 1; index >= 0 && this.grid_Rows.Rows.Count != 1; --index)
      {
        this.item.RemoveChildNodeAt(intList[index], false, false);
        this.grid_Rows.Rows.RemoveAt(intList[index]);
      }
      if (num > this.grid_Rows.Rows.Count - 1)
        num = this.grid_Rows.Rows.Count - 1;
      if (num < 0 || num > this.grid_Rows.Rows.Count - 1)
        return;
      for (int colIndex = 0; colIndex < this.grid_Rows.Cols.Count; ++colIndex)
        this.grid_Rows.Rows[num].Cells[colIndex].Selected = true;
      this.grid_Rows.SetCurRow(num);
    }
    finally
    {
      this.item.ResumeUpdateLayout(false, true);
      this.isUpdating = false;
      this.ApplyChanges();
      this.item.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Событие вызывается перед завершением редактирования значений в таблице, управляющей строками</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_Rows_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
  {
    if (this.item == null || this.isUpdating)
      return;
    if (e.ColIndex == this.grid_Rows.Cols[GridColumnKeys.column_RowCheck].Index)
    {
      (this.item.Nodes[e.RowIndex] as TableElement).Tag = e.NewValue;
      this.grid_Rows.Rows[e.RowIndex].Cells[GridColumnKeys.column_RowCheck].Value = e.NewValue;
      this.CorrectTableRowHeights();
      e.Result = !this.grid_Rows.Rows[e.RowIndex].Cells[GridColumnKeys.column_RowCheck].Value.Equals(e.NewValue) ? iGEditResult.Cancel : iGEditResult.Commit;
      this.ApplyChanges();
    }
    if (e.ColIndex == this.grid_Rows.Cols[GridColumnKeys.column_RowName].Index)
    {
      if (this.item.Nodes[e.RowIndex] is TableElement node)
      {
        string str = e.NewValue?.ToString();
        node.Name = str;
      }
      e.Result = iGEditResult.Commit;
      this.ApplyChanges();
    }
    if (e.ColIndex != this.grid_Rows.Cols[GridColumnKeys.column_RowHeight].Index)
      return;
    TableElement node1 = this.item.Nodes[e.RowIndex] as TableElement;
    float result;
    if (e.NewValue == null || !float.TryParse(e.NewValue.ToString(), out result) || (double) result <= 0.0)
    {
      e.Result = iGEditResult.Proceed;
    }
    else
    {
      RectangleF newBounds = new RectangleF(node1.Location, new SizeF(node1.Size.Width, result));
      node1.AssignMinHeight(newBounds.Height, false, false, false);
      node1.SetCellSizes(newBounds, false, true, false, true, false);
      e.Result = iGEditResult.Commit;
      this.ApplyChanges();
    }
  }

  /// <summary>Изменились выделенные строки или ячейки в гриде, управляющем строками</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_Rows_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (keyData != Keys.Return)
      return base.ProcessCmdKey(ref msg, keyData);
    if (this.grid_Columns.IsEditing)
      this.grid_Columns.CommitEditCurCell();
    else if (this.grid_Rows.IsEditing)
    {
      this.grid_Rows.CommitEditCurCell();
    }
    else
    {
      if (this.ActiveControl is NumericUpDown && (this.ActiveControl == this.numeric_tLW || this.ActiveControl == this.numeric_tW || this.ActiveControl == this.numeric_tRW || this.ActiveControl == this.numeric_tLH || this.ActiveControl == this.numeric_tH || this.ActiveControl == this.numeric_tBH))
        this.AutoCalcVerticalSizes();
      this.ApplyChanges();
    }
    return true;
  }

  private void TableEditorDialog_MouseClick(object sender, MouseEventArgs e)
  {
    if (!(sender is Button button) || button.Focused)
      return;
    button.PerformClick();
  }

  private void grid_Columns_TextBoxKeyDown(object sender, iGTextBoxKeyDownEventArgs e)
  {
    iGrid iGrid = sender as iGrid;
    if (e.KeyValue == Keys.Up && e.RowIndex > 0)
    {
      iGrid.CommitEditCurCell();
      iGrid.CurCell = iGrid.Cells[iGrid.CurCell.RowIndex - 1, iGrid.CurCell.ColIndex];
    }
    if (e.KeyValue == Keys.Down && e.RowIndex < iGrid.Rows.Count - 1)
    {
      iGrid.CommitEditCurCell();
      iGrid.CurCell = iGrid.Cells[iGrid.CurCell.RowIndex + 1, iGrid.CurCell.ColIndex];
    }
    if (e.KeyValue == Keys.Left && iGrid.TextBox.SelectionStart == 0 && iGrid.TextBox.SelectionLength == 0 && e.ColIndex != 0)
    {
      iGrid.CommitEditCurCell();
      iGrid.CurCell = iGrid.Cells[iGrid.CurCell.RowIndex, iGrid.CurCell.ColIndex - 1];
    }
    if (e.KeyValue != Keys.Right || iGrid.TextBox.SelectionStart != iGrid.TextBox.Text.Length || iGrid.TextBox.SelectionLength != 0 || e.ColIndex >= iGrid.Cols.Count - 1)
      return;
    iGrid.CommitEditCurCell();
    iGrid.CurCell = iGrid.Cells[iGrid.CurCell.RowIndex, iGrid.CurCell.ColIndex + 1];
  }

  private float RoundHeight(float value, float increment, bool ask)
  {
    float num1 = value;
    if ((double) increment != 0.0)
    {
      float num2 = (float) Math.Round((double) value / (double) increment);
      if ((double) num2 == 0.0)
        num2 = 1f;
      num1 = num2 * increment;
    }
    if ((double) num1 != (double) value & ask && MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_605"), LocalizationHolder.rm.GetString("Document.Model_604"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      num1 = value;
    return num1;
  }

  private void Set_RowsInsertHeight_Value()
  {
    Decimal num = (Decimal) this.RoundHeight((float) this.roundRowsInsertHeight_ValueTo, Convert.ToSingle(this.numeric_GridRowHeight.Value), false);
    if (!(this.numeric_RowsInsertHeight.Value != num))
      return;
    try
    {
      this.lockSetRoundRowsInsertHeight_ValueTo = true;
      this.numeric_RowsInsertHeight.Value = num;
    }
    finally
    {
      this.lockSetRoundRowsInsertHeight_ValueTo = false;
    }
  }

  private void NumericUpDown_ValueChanged(object sender, EventArgs e)
  {
    if (sender == this.numeric_RowsInsertHeight)
    {
      Decimal num = (Decimal) this.RoundHeight((float) this.numeric_RowsInsertHeight.Value, Convert.ToSingle(this.numeric_GridRowHeight.Value), true);
      if (this.numeric_RowsInsertHeight.Value != num)
        this.numeric_RowsInsertHeight.Value = num;
      if (!this.lockSetRoundRowsInsertHeight_ValueTo)
        this.roundRowsInsertHeight_ValueTo = num;
    }
    NumericUpDown numericUpDown = sender as NumericUpDown;
    Decimal d = numericUpDown.Value;
    int num1 = (d - Decimal.Truncate(d)).ToString().Length - 2;
    if (num1 < 0)
      num1 = 0;
    numericUpDown.DecimalPlaces = num1;
  }

  private void numeric_GridRowHeight_KeyPress(object sender, KeyPressEventArgs e)
  {
    CultureInfo currentCulture = CultureInfo.CurrentCulture;
    if (e.KeyChar == '.')
      e.KeyChar = currentCulture.NumberFormat.NumberDecimalSeparator[0];
    if (e.KeyChar != ',')
      return;
    e.KeyChar = currentCulture.NumberFormat.NumberDecimalSeparator[0];
  }

  /// <summary>Очистить ресурсы, используемые формой</summary>
  /// <param name="disposing">true, если должны быть освобождены управляемые ресурсы</param>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableEditorDialog));
    this.panelTableSize = new Panel();
    this.numeric_tH = new CustomNumericUpDown();
    this.numeric_tBH = new CustomNumericUpDown();
    this.numeric_tLH = new CustomNumericUpDown();
    this.numeric_tRW = new CustomNumericUpDown();
    this.numeric_tW = new CustomNumericUpDown();
    this.numeric_tLW = new CustomNumericUpDown();
    this.cb_tBH = new CheckBox();
    this.cb_tH = new CheckBox();
    this.cb_tLH = new CheckBox();
    this.cb_tRW = new CheckBox();
    this.cb_tW = new CheckBox();
    this.cb_tLW = new CheckBox();
    this.pictureTableDimensions = new PictureBox();
    this.buttonFrameMode_5 = new Button();
    this.imageFrames = new ImageList(this.components);
    this.buttonFrameMode_6 = new Button();
    this.buttonFrameMode_4 = new Button();
    this.buttonFrameMode_3 = new Button();
    this.buttonFrameMode_2 = new Button();
    this.buttonFrameMode_1 = new Button();
    this.toolTips = new ToolTip(this.components);
    this.numeric_GridRowHeight = new CustomNumericUpDown();
    this.numeric_ColumnsInsertWidth = new CustomNumericUpDown();
    this.numeric_ColumnsInsertCount = new CustomNumericUpDown();
    this.numeric_RowsInsertHeight = new CustomNumericUpDown();
    this.numeric_RowsInsertCount = new CustomNumericUpDown();
    this.numeric_HeaderRows = new CustomNumericUpDown();
    this.button_ColumnsInsertAfter = new Button();
    this.imageButtons = new ImageList(this.components);
    this.button_ColumnsInsertBefore = new Button();
    this.button_ColumnsRemove = new Button();
    this.button_RowsRemove = new Button();
    this.button_RowsInsertAfter = new Button();
    this.button_RowsInsertBefore = new Button();
    this.panelTableType = new Panel();
    this.radio_DynamicTable = new RadioButton();
    this.radio_StaticTable = new RadioButton();
    this.panelTableProperties = new Panel();
    this.label_GridRowHeight = new Label();
    this.cb_FullSizeGrid = new CheckBox();
    this.cb_TableWrapping = new CheckBox();
    this.panelTableColumns = new Panel();
    this.labelColumnsHeader = new Label();
    this.label_ColumnsCount = new Label();
    this.label_ColumnsWidth = new Label();
    this.label_ColumnsInsertCount = new Label();
    this.label_ColumnsInsert = new Label();
    this.grid_Columns = new iGrid();
    this.panelTableRows = new Panel();
    this.labelRowsHeader = new Label();
    this.label_RowsCount = new Label();
    this.label_ColumnsHeight = new Label();
    this.label_RowsInsertCount = new Label();
    this.label_RowsInsert = new Label();
    this.grid_Rows = new iGrid();
    this.label_HeaderRows = new Label();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.bevel = new Bevel();
    this.labelWarning = new Label();
    this.errorProvider = new ErrorProvider(this.components);
    this.gridView1 = new GridView();
    this.panelTableSize.SuspendLayout();
    this.numeric_tH.BeginInit();
    this.numeric_tBH.BeginInit();
    this.numeric_tLH.BeginInit();
    this.numeric_tRW.BeginInit();
    this.numeric_tW.BeginInit();
    this.numeric_tLW.BeginInit();
    ((ISupportInitialize) this.pictureTableDimensions).BeginInit();
    this.numeric_GridRowHeight.BeginInit();
    this.numeric_ColumnsInsertWidth.BeginInit();
    this.numeric_ColumnsInsertCount.BeginInit();
    this.numeric_RowsInsertHeight.BeginInit();
    this.numeric_RowsInsertCount.BeginInit();
    this.numeric_HeaderRows.BeginInit();
    this.panelTableType.SuspendLayout();
    this.panelTableProperties.SuspendLayout();
    this.panelTableColumns.SuspendLayout();
    ((ISupportInitialize) this.grid_Columns).BeginInit();
    this.panelTableRows.SuspendLayout();
    ((ISupportInitialize) this.grid_Rows).BeginInit();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.gridView1.BeginInit();
    this.SuspendLayout();
    this.panelTableSize.Controls.Add((Control) this.numeric_tH);
    this.panelTableSize.Controls.Add((Control) this.numeric_tBH);
    this.panelTableSize.Controls.Add((Control) this.numeric_tLH);
    this.panelTableSize.Controls.Add((Control) this.numeric_tRW);
    this.panelTableSize.Controls.Add((Control) this.numeric_tW);
    this.panelTableSize.Controls.Add((Control) this.numeric_tLW);
    this.panelTableSize.Controls.Add((Control) this.cb_tBH);
    this.panelTableSize.Controls.Add((Control) this.cb_tH);
    this.panelTableSize.Controls.Add((Control) this.cb_tLH);
    this.panelTableSize.Controls.Add((Control) this.cb_tRW);
    this.panelTableSize.Controls.Add((Control) this.cb_tW);
    this.panelTableSize.Controls.Add((Control) this.cb_tLW);
    this.panelTableSize.Controls.Add((Control) this.pictureTableDimensions);
    this.panelTableSize.Controls.Add((Control) this.buttonFrameMode_5);
    this.panelTableSize.Controls.Add((Control) this.buttonFrameMode_6);
    this.panelTableSize.Controls.Add((Control) this.buttonFrameMode_4);
    this.panelTableSize.Controls.Add((Control) this.buttonFrameMode_3);
    this.panelTableSize.Controls.Add((Control) this.buttonFrameMode_2);
    this.panelTableSize.Controls.Add((Control) this.buttonFrameMode_1);
    componentResourceManager.ApplyResources((object) this.panelTableSize, "panelTableSize");
    this.panelTableSize.Name = "panelTableSize";
    componentResourceManager.ApplyResources((object) this.numeric_tH, "numeric_tH");
    this.numeric_tH.BackColor = Color.White;
    this.numeric_tH.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tH.ForeColor = Color.Black;
    this.numeric_tH.Maximum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this.numeric_tH.Name = "numeric_tH";
    this.toolTips.SetToolTip((Control) this.numeric_tH, componentResourceManager.GetString("numeric_tH.ToolTip"));
    this.numeric_tH.ValueChanged += new EventHandler(this.DoChangeVerticalSizes);
    this.numeric_tH.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    componentResourceManager.ApplyResources((object) this.numeric_tBH, "numeric_tBH");
    this.numeric_tBH.BackColor = Color.White;
    this.numeric_tBH.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tBH.ForeColor = Color.Black;
    this.numeric_tBH.Maximum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this.numeric_tBH.Minimum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      int.MinValue
    });
    this.numeric_tBH.Name = "numeric_tBH";
    this.toolTips.SetToolTip((Control) this.numeric_tBH, componentResourceManager.GetString("numeric_tBH.ToolTip"));
    this.numeric_tBH.ValueChanged += new EventHandler(this.DoChangeVerticalSizes);
    this.numeric_tBH.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    componentResourceManager.ApplyResources((object) this.numeric_tLH, "numeric_tLH");
    this.numeric_tLH.BackColor = Color.White;
    this.numeric_tLH.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tLH.ForeColor = Color.Black;
    this.numeric_tLH.Maximum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this.numeric_tLH.Minimum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      int.MinValue
    });
    this.numeric_tLH.Name = "numeric_tLH";
    this.toolTips.SetToolTip((Control) this.numeric_tLH, componentResourceManager.GetString("numeric_tLH.ToolTip"));
    this.numeric_tLH.ValueChanged += new EventHandler(this.DoChangeVerticalSizes);
    this.numeric_tLH.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    this.numeric_tRW.BackColor = Color.White;
    this.numeric_tRW.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tRW.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tRW, "numeric_tRW");
    this.numeric_tRW.Maximum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this.numeric_tRW.Minimum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      int.MinValue
    });
    this.numeric_tRW.Name = "numeric_tRW";
    this.toolTips.SetToolTip((Control) this.numeric_tRW, componentResourceManager.GetString("numeric_tRW.ToolTip"));
    this.numeric_tRW.ValueChanged += new EventHandler(this.DoChangeHorizontalSizes);
    this.numeric_tRW.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    this.numeric_tW.BackColor = Color.White;
    this.numeric_tW.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tW.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tW, "numeric_tW");
    this.numeric_tW.Maximum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this.numeric_tW.Name = "numeric_tW";
    this.toolTips.SetToolTip((Control) this.numeric_tW, componentResourceManager.GetString("numeric_tW.ToolTip"));
    this.numeric_tW.ValueChanged += new EventHandler(this.DoChangeHorizontalSizes);
    this.numeric_tW.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    this.numeric_tLW.BackColor = Color.White;
    this.numeric_tLW.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tLW.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tLW, "numeric_tLW");
    this.numeric_tLW.Maximum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this.numeric_tLW.Minimum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      int.MinValue
    });
    this.numeric_tLW.Name = "numeric_tLW";
    this.toolTips.SetToolTip((Control) this.numeric_tLW, componentResourceManager.GetString("numeric_tLW.ToolTip"));
    this.numeric_tLW.ValueChanged += new EventHandler(this.DoChangeHorizontalSizes);
    this.numeric_tLW.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    this.cb_tBH.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.cb_tBH, "cb_tBH");
    this.cb_tBH.Checked = true;
    this.cb_tBH.CheckState = CheckState.Checked;
    this.cb_tBH.Name = "cb_tBH";
    this.toolTips.SetToolTip((Control) this.cb_tBH, componentResourceManager.GetString("cb_tBH.ToolTip"));
    this.cb_tBH.UseVisualStyleBackColor = false;
    this.cb_tBH.CheckedChanged += new EventHandler(this.DoChangeVerticalSizeChecks);
    this.cb_tH.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.cb_tH, "cb_tH");
    this.cb_tH.Name = "cb_tH";
    this.toolTips.SetToolTip((Control) this.cb_tH, componentResourceManager.GetString("cb_tH.ToolTip"));
    this.cb_tH.UseVisualStyleBackColor = false;
    this.cb_tH.CheckedChanged += new EventHandler(this.DoChangeVerticalSizeChecks);
    this.cb_tLH.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.cb_tLH, "cb_tLH");
    this.cb_tLH.Checked = true;
    this.cb_tLH.CheckState = CheckState.Checked;
    this.cb_tLH.Name = "cb_tLH";
    this.toolTips.SetToolTip((Control) this.cb_tLH, componentResourceManager.GetString("cb_tLH.ToolTip"));
    this.cb_tLH.UseVisualStyleBackColor = false;
    this.cb_tLH.CheckedChanged += new EventHandler(this.DoChangeVerticalSizeChecks);
    this.cb_tRW.BackColor = Color.White;
    componentResourceManager.ApplyResources((object) this.cb_tRW, "cb_tRW");
    this.cb_tRW.Checked = true;
    this.cb_tRW.CheckState = CheckState.Checked;
    this.cb_tRW.Name = "cb_tRW";
    this.toolTips.SetToolTip((Control) this.cb_tRW, componentResourceManager.GetString("cb_tRW.ToolTip"));
    this.cb_tRW.UseVisualStyleBackColor = false;
    this.cb_tRW.CheckedChanged += new EventHandler(this.DoChangeHorizontalSizeChecks);
    this.cb_tW.BackColor = Color.White;
    componentResourceManager.ApplyResources((object) this.cb_tW, "cb_tW");
    this.cb_tW.Checked = true;
    this.cb_tW.CheckState = CheckState.Checked;
    this.cb_tW.Name = "cb_tW";
    this.toolTips.SetToolTip((Control) this.cb_tW, componentResourceManager.GetString("cb_tW.ToolTip"));
    this.cb_tW.UseVisualStyleBackColor = false;
    this.cb_tW.CheckedChanged += new EventHandler(this.DoChangeHorizontalSizeChecks);
    this.cb_tLW.BackColor = Color.White;
    componentResourceManager.ApplyResources((object) this.cb_tLW, "cb_tLW");
    this.cb_tLW.Name = "cb_tLW";
    this.toolTips.SetToolTip((Control) this.cb_tLW, componentResourceManager.GetString("cb_tLW.ToolTip"));
    this.cb_tLW.UseVisualStyleBackColor = false;
    this.cb_tLW.CheckedChanged += new EventHandler(this.DoChangeHorizontalSizeChecks);
    componentResourceManager.ApplyResources((object) this.pictureTableDimensions, "pictureTableDimensions");
    this.pictureTableDimensions.Name = "pictureTableDimensions";
    this.pictureTableDimensions.TabStop = false;
    componentResourceManager.ApplyResources((object) this.buttonFrameMode_5, "buttonFrameMode_5");
    this.buttonFrameMode_5.ImageList = this.imageFrames;
    this.buttonFrameMode_5.Name = "buttonFrameMode_5";
    this.buttonFrameMode_5.TabStop = false;
    this.toolTips.SetToolTip((Control) this.buttonFrameMode_5, componentResourceManager.GetString("buttonFrameMode_5.ToolTip"));
    this.buttonFrameMode_5.UseVisualStyleBackColor = true;
    this.buttonFrameMode_5.Click += new EventHandler(this.DoChangeFrameMode);
    this.imageFrames.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageFrames.ImageStream");
    this.imageFrames.TransparentColor = Color.Transparent;
    this.imageFrames.Images.SetKeyName(0, "t_1.ico");
    this.imageFrames.Images.SetKeyName(1, "t_2.ico");
    this.imageFrames.Images.SetKeyName(2, "t_3.ico");
    this.imageFrames.Images.SetKeyName(3, "t_4.ico");
    this.imageFrames.Images.SetKeyName(4, "t_5.ico");
    this.imageFrames.Images.SetKeyName(5, "t7.png");
    componentResourceManager.ApplyResources((object) this.buttonFrameMode_6, "buttonFrameMode_6");
    this.buttonFrameMode_6.ImageList = this.imageFrames;
    this.buttonFrameMode_6.Name = "buttonFrameMode_6";
    this.buttonFrameMode_6.TabStop = false;
    this.toolTips.SetToolTip((Control) this.buttonFrameMode_6, componentResourceManager.GetString("buttonFrameMode_6.ToolTip"));
    this.buttonFrameMode_6.UseVisualStyleBackColor = true;
    this.buttonFrameMode_6.Click += new EventHandler(this.DoChangeFrameMode);
    componentResourceManager.ApplyResources((object) this.buttonFrameMode_4, "buttonFrameMode_4");
    this.buttonFrameMode_4.ImageList = this.imageFrames;
    this.buttonFrameMode_4.Name = "buttonFrameMode_4";
    this.buttonFrameMode_4.TabStop = false;
    this.toolTips.SetToolTip((Control) this.buttonFrameMode_4, componentResourceManager.GetString("buttonFrameMode_4.ToolTip"));
    this.buttonFrameMode_4.UseVisualStyleBackColor = true;
    this.buttonFrameMode_4.Click += new EventHandler(this.DoChangeFrameMode);
    componentResourceManager.ApplyResources((object) this.buttonFrameMode_3, "buttonFrameMode_3");
    this.buttonFrameMode_3.ImageList = this.imageFrames;
    this.buttonFrameMode_3.Name = "buttonFrameMode_3";
    this.buttonFrameMode_3.TabStop = false;
    this.toolTips.SetToolTip((Control) this.buttonFrameMode_3, componentResourceManager.GetString("buttonFrameMode_3.ToolTip"));
    this.buttonFrameMode_3.UseVisualStyleBackColor = true;
    this.buttonFrameMode_3.Click += new EventHandler(this.DoChangeFrameMode);
    componentResourceManager.ApplyResources((object) this.buttonFrameMode_2, "buttonFrameMode_2");
    this.buttonFrameMode_2.ImageList = this.imageFrames;
    this.buttonFrameMode_2.Name = "buttonFrameMode_2";
    this.buttonFrameMode_2.TabStop = false;
    this.toolTips.SetToolTip((Control) this.buttonFrameMode_2, componentResourceManager.GetString("buttonFrameMode_2.ToolTip"));
    this.buttonFrameMode_2.UseVisualStyleBackColor = true;
    this.buttonFrameMode_2.Click += new EventHandler(this.DoChangeFrameMode);
    componentResourceManager.ApplyResources((object) this.buttonFrameMode_1, "buttonFrameMode_1");
    this.buttonFrameMode_1.ImageList = this.imageFrames;
    this.buttonFrameMode_1.Name = "buttonFrameMode_1";
    this.buttonFrameMode_1.TabStop = false;
    this.toolTips.SetToolTip((Control) this.buttonFrameMode_1, componentResourceManager.GetString("buttonFrameMode_1.ToolTip"));
    this.buttonFrameMode_1.UseVisualStyleBackColor = true;
    this.buttonFrameMode_1.Click += new EventHandler(this.DoChangeFrameMode);
    componentResourceManager.ApplyResources((object) this.numeric_GridRowHeight, "numeric_GridRowHeight");
    this.numeric_GridRowHeight.Name = "numeric_GridRowHeight";
    this.toolTips.SetToolTip((Control) this.numeric_GridRowHeight, componentResourceManager.GetString("numeric_GridRowHeight.ToolTip"));
    this.numeric_GridRowHeight.ValueChanged += new EventHandler(this.DoUpdateRowsAndState);
    this.numeric_GridRowHeight.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    componentResourceManager.ApplyResources((object) this.numeric_ColumnsInsertWidth, "numeric_ColumnsInsertWidth");
    this.numeric_ColumnsInsertWidth.Maximum = new Decimal(new int[4]
    {
      200000,
      0,
      0,
      0
    });
    this.numeric_ColumnsInsertWidth.Name = "numeric_ColumnsInsertWidth";
    this.toolTips.SetToolTip((Control) this.numeric_ColumnsInsertWidth, componentResourceManager.GetString("numeric_ColumnsInsertWidth.ToolTip"));
    this.numeric_ColumnsInsertWidth.ValueChanged += new EventHandler(this.NumericUpDown_ValueChanged);
    this.numeric_ColumnsInsertWidth.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    componentResourceManager.ApplyResources((object) this.numeric_ColumnsInsertCount, "numeric_ColumnsInsertCount");
    this.numeric_ColumnsInsertCount.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numeric_ColumnsInsertCount.Name = "numeric_ColumnsInsertCount";
    this.toolTips.SetToolTip((Control) this.numeric_ColumnsInsertCount, componentResourceManager.GetString("numeric_ColumnsInsertCount.ToolTip"));
    this.numeric_ColumnsInsertCount.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this.numeric_RowsInsertHeight, "numeric_RowsInsertHeight");
    this.numeric_RowsInsertHeight.Maximum = new Decimal(new int[4]
    {
      200000,
      0,
      0,
      0
    });
    this.numeric_RowsInsertHeight.Name = "numeric_RowsInsertHeight";
    this.toolTips.SetToolTip((Control) this.numeric_RowsInsertHeight, componentResourceManager.GetString("numeric_RowsInsertHeight.ToolTip"));
    this.numeric_RowsInsertHeight.ValueChanged += new EventHandler(this.NumericUpDown_ValueChanged);
    this.numeric_RowsInsertHeight.KeyPress += new KeyPressEventHandler(this.numeric_GridRowHeight_KeyPress);
    componentResourceManager.ApplyResources((object) this.numeric_RowsInsertCount, "numeric_RowsInsertCount");
    this.numeric_RowsInsertCount.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numeric_RowsInsertCount.Name = "numeric_RowsInsertCount";
    this.toolTips.SetToolTip((Control) this.numeric_RowsInsertCount, componentResourceManager.GetString("numeric_RowsInsertCount.ToolTip"));
    this.numeric_RowsInsertCount.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this.numeric_HeaderRows, "numeric_HeaderRows");
    this.numeric_HeaderRows.Name = "numeric_HeaderRows";
    this.toolTips.SetToolTip((Control) this.numeric_HeaderRows, componentResourceManager.GetString("numeric_HeaderRows.ToolTip"));
    this.numeric_HeaderRows.ValueChanged += new EventHandler(this.DoUpdateHeaderRows);
    componentResourceManager.ApplyResources((object) this.button_ColumnsInsertAfter, "button_ColumnsInsertAfter");
    this.button_ColumnsInsertAfter.ImageList = this.imageButtons;
    this.button_ColumnsInsertAfter.Name = "button_ColumnsInsertAfter";
    this.toolTips.SetToolTip((Control) this.button_ColumnsInsertAfter, componentResourceManager.GetString("button_ColumnsInsertAfter.ToolTip"));
    this.button_ColumnsInsertAfter.Click += new EventHandler(this.DoInsertColsAfter);
    this.imageButtons.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageButtons.ImageStream");
    this.imageButtons.TransparentColor = Color.Transparent;
    this.imageButtons.Images.SetKeyName(0, "col_left.png");
    this.imageButtons.Images.SetKeyName(1, "col_right.png");
    this.imageButtons.Images.SetKeyName(2, "row_up.png");
    this.imageButtons.Images.SetKeyName(3, "row_down.png");
    this.imageButtons.Images.SetKeyName(4, "delete.ico");
    componentResourceManager.ApplyResources((object) this.button_ColumnsInsertBefore, "button_ColumnsInsertBefore");
    this.button_ColumnsInsertBefore.ImageList = this.imageButtons;
    this.button_ColumnsInsertBefore.Name = "button_ColumnsInsertBefore";
    this.toolTips.SetToolTip((Control) this.button_ColumnsInsertBefore, componentResourceManager.GetString("button_ColumnsInsertBefore.ToolTip"));
    this.button_ColumnsInsertBefore.Click += new EventHandler(this.DoInsertColsBefore);
    componentResourceManager.ApplyResources((object) this.button_ColumnsRemove, "button_ColumnsRemove");
    this.button_ColumnsRemove.ImageList = this.imageButtons;
    this.button_ColumnsRemove.Name = "button_ColumnsRemove";
    this.toolTips.SetToolTip((Control) this.button_ColumnsRemove, componentResourceManager.GetString("button_ColumnsRemove.ToolTip"));
    this.button_ColumnsRemove.Click += new EventHandler(this.DoColsRemove);
    componentResourceManager.ApplyResources((object) this.button_RowsRemove, "button_RowsRemove");
    this.button_RowsRemove.ImageList = this.imageButtons;
    this.button_RowsRemove.Name = "button_RowsRemove";
    this.toolTips.SetToolTip((Control) this.button_RowsRemove, componentResourceManager.GetString("button_RowsRemove.ToolTip"));
    this.button_RowsRemove.Click += new EventHandler(this.DoRowsRemove);
    componentResourceManager.ApplyResources((object) this.button_RowsInsertAfter, "button_RowsInsertAfter");
    this.button_RowsInsertAfter.ImageList = this.imageButtons;
    this.button_RowsInsertAfter.Name = "button_RowsInsertAfter";
    this.toolTips.SetToolTip((Control) this.button_RowsInsertAfter, componentResourceManager.GetString("button_RowsInsertAfter.ToolTip"));
    this.button_RowsInsertAfter.Click += new EventHandler(this.DoInsertRowsAfter);
    componentResourceManager.ApplyResources((object) this.button_RowsInsertBefore, "button_RowsInsertBefore");
    this.button_RowsInsertBefore.ImageList = this.imageButtons;
    this.button_RowsInsertBefore.Name = "button_RowsInsertBefore";
    this.toolTips.SetToolTip((Control) this.button_RowsInsertBefore, componentResourceManager.GetString("button_RowsInsertBefore.ToolTip"));
    this.button_RowsInsertBefore.Click += new EventHandler(this.DoInsertRowsBefore);
    this.panelTableType.Controls.Add((Control) this.radio_DynamicTable);
    this.panelTableType.Controls.Add((Control) this.radio_StaticTable);
    componentResourceManager.ApplyResources((object) this.panelTableType, "panelTableType");
    this.panelTableType.Name = "panelTableType";
    componentResourceManager.ApplyResources((object) this.radio_DynamicTable, "radio_DynamicTable");
    this.radio_DynamicTable.Name = "radio_DynamicTable";
    this.radio_DynamicTable.CheckedChanged += new EventHandler(this.DoUpdateRowsAndState);
    componentResourceManager.ApplyResources((object) this.radio_StaticTable, "radio_StaticTable");
    this.radio_StaticTable.Name = "radio_StaticTable";
    this.radio_StaticTable.CheckedChanged += new EventHandler(this.DoUpdateRowsAndState);
    this.panelTableProperties.Controls.Add((Control) this.label_GridRowHeight);
    this.panelTableProperties.Controls.Add((Control) this.numeric_GridRowHeight);
    this.panelTableProperties.Controls.Add((Control) this.cb_FullSizeGrid);
    this.panelTableProperties.Controls.Add((Control) this.cb_TableWrapping);
    componentResourceManager.ApplyResources((object) this.panelTableProperties, "panelTableProperties");
    this.panelTableProperties.Name = "panelTableProperties";
    componentResourceManager.ApplyResources((object) this.label_GridRowHeight, "label_GridRowHeight");
    this.label_GridRowHeight.Name = "label_GridRowHeight";
    componentResourceManager.ApplyResources((object) this.cb_FullSizeGrid, "cb_FullSizeGrid");
    this.cb_FullSizeGrid.Name = "cb_FullSizeGrid";
    this.cb_FullSizeGrid.CheckedChanged += new EventHandler(this.DoUpdateRowsAndState);
    componentResourceManager.ApplyResources((object) this.cb_TableWrapping, "cb_TableWrapping");
    this.cb_TableWrapping.Name = "cb_TableWrapping";
    this.cb_TableWrapping.CheckedChanged += new EventHandler(this.DoUpdateRowsAndState);
    componentResourceManager.ApplyResources((object) this.panelTableColumns, "panelTableColumns");
    this.panelTableColumns.Controls.Add((Control) this.labelColumnsHeader);
    this.panelTableColumns.Controls.Add((Control) this.label_ColumnsCount);
    this.panelTableColumns.Controls.Add((Control) this.button_ColumnsRemove);
    this.panelTableColumns.Controls.Add((Control) this.button_ColumnsInsertAfter);
    this.panelTableColumns.Controls.Add((Control) this.button_ColumnsInsertBefore);
    this.panelTableColumns.Controls.Add((Control) this.numeric_ColumnsInsertWidth);
    this.panelTableColumns.Controls.Add((Control) this.label_ColumnsWidth);
    this.panelTableColumns.Controls.Add((Control) this.numeric_ColumnsInsertCount);
    this.panelTableColumns.Controls.Add((Control) this.label_ColumnsInsertCount);
    this.panelTableColumns.Controls.Add((Control) this.label_ColumnsInsert);
    this.panelTableColumns.Controls.Add((Control) this.grid_Columns);
    this.panelTableColumns.Name = "panelTableColumns";
    componentResourceManager.ApplyResources((object) this.labelColumnsHeader, "labelColumnsHeader");
    this.labelColumnsHeader.Name = "labelColumnsHeader";
    componentResourceManager.ApplyResources((object) this.label_ColumnsCount, "label_ColumnsCount");
    this.label_ColumnsCount.Name = "label_ColumnsCount";
    componentResourceManager.ApplyResources((object) this.label_ColumnsWidth, "label_ColumnsWidth");
    this.label_ColumnsWidth.Name = "label_ColumnsWidth";
    componentResourceManager.ApplyResources((object) this.label_ColumnsInsertCount, "label_ColumnsInsertCount");
    this.label_ColumnsInsertCount.Name = "label_ColumnsInsertCount";
    componentResourceManager.ApplyResources((object) this.label_ColumnsInsert, "label_ColumnsInsert");
    this.label_ColumnsInsert.Name = "label_ColumnsInsert";
    componentResourceManager.ApplyResources((object) this.grid_Columns, "grid_Columns");
    this.grid_Columns.Header.AllowPress = false;
    this.grid_Columns.Header.Height = (int) componentResourceManager.GetObject("grid_Columns.Header.Height");
    this.grid_Columns.Name = "grid_Columns";
    this.grid_Columns.SelectionChanged += new EventHandler(this.grid_Columns_SelectionChanged);
    this.grid_Columns.BeforeCommitEdit += new iGBeforeCommitEditEventHandler(this.grid_Columns_BeforeCommitEdit);
    this.grid_Columns.AfterCommitEdit += new iGAfterCommitEditEventHandler(this.grid_AfterCommitEdit);
    this.grid_Columns.TextBoxKeyDown += new iGTextBoxKeyDownEventHandler(this.grid_Columns_TextBoxKeyDown);
    componentResourceManager.ApplyResources((object) this.panelTableRows, "panelTableRows");
    this.panelTableRows.Controls.Add((Control) this.labelRowsHeader);
    this.panelTableRows.Controls.Add((Control) this.label_RowsCount);
    this.panelTableRows.Controls.Add((Control) this.button_RowsRemove);
    this.panelTableRows.Controls.Add((Control) this.button_RowsInsertAfter);
    this.panelTableRows.Controls.Add((Control) this.button_RowsInsertBefore);
    this.panelTableRows.Controls.Add((Control) this.numeric_RowsInsertHeight);
    this.panelTableRows.Controls.Add((Control) this.label_ColumnsHeight);
    this.panelTableRows.Controls.Add((Control) this.numeric_RowsInsertCount);
    this.panelTableRows.Controls.Add((Control) this.label_RowsInsertCount);
    this.panelTableRows.Controls.Add((Control) this.label_RowsInsert);
    this.panelTableRows.Controls.Add((Control) this.grid_Rows);
    this.panelTableRows.Name = "panelTableRows";
    componentResourceManager.ApplyResources((object) this.labelRowsHeader, "labelRowsHeader");
    this.labelRowsHeader.Name = "labelRowsHeader";
    componentResourceManager.ApplyResources((object) this.label_RowsCount, "label_RowsCount");
    this.label_RowsCount.Name = "label_RowsCount";
    componentResourceManager.ApplyResources((object) this.label_ColumnsHeight, "label_ColumnsHeight");
    this.label_ColumnsHeight.Name = "label_ColumnsHeight";
    componentResourceManager.ApplyResources((object) this.label_RowsInsertCount, "label_RowsInsertCount");
    this.label_RowsInsertCount.Name = "label_RowsInsertCount";
    componentResourceManager.ApplyResources((object) this.label_RowsInsert, "label_RowsInsert");
    this.label_RowsInsert.Name = "label_RowsInsert";
    componentResourceManager.ApplyResources((object) this.grid_Rows, "grid_Rows");
    this.grid_Rows.Header.AllowPress = false;
    this.grid_Rows.Header.Height = (int) componentResourceManager.GetObject("grid_Rows.Header.Height");
    this.grid_Rows.Name = "grid_Rows";
    this.grid_Rows.SelectionChanged += new EventHandler(this.grid_Rows_SelectionChanged);
    this.grid_Rows.BeforeCommitEdit += new iGBeforeCommitEditEventHandler(this.grid_Rows_BeforeCommitEdit);
    this.grid_Rows.AfterCommitEdit += new iGAfterCommitEditEventHandler(this.grid_AfterCommitEdit);
    this.grid_Rows.TextBoxKeyDown += new iGTextBoxKeyDownEventHandler(this.grid_Columns_TextBoxKeyDown);
    componentResourceManager.ApplyResources((object) this.label_HeaderRows, "label_HeaderRows");
    this.label_HeaderRows.Name = "label_HeaderRows";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.MouseClick += new MouseEventHandler(this.TableEditorDialog_MouseClick);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.MouseClick += new MouseEventHandler(this.TableEditorDialog_MouseClick);
    componentResourceManager.ApplyResources((object) this.bevel, "bevel");
    this.bevel.Name = "bevel";
    componentResourceManager.ApplyResources((object) this.labelWarning, "labelWarning");
    this.labelWarning.Name = "labelWarning";
    this.errorProvider.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this.errorProvider, "errorProvider");
    this.gridView1.GridControl = (GridControl) null;
    this.gridView1.Name = "gridView1";
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.labelWarning);
    this.Controls.Add((Control) this.bevel);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.numeric_HeaderRows);
    this.Controls.Add((Control) this.label_HeaderRows);
    this.Controls.Add((Control) this.panelTableRows);
    this.Controls.Add((Control) this.panelTableColumns);
    this.Controls.Add((Control) this.panelTableProperties);
    this.Controls.Add((Control) this.panelTableType);
    this.Controls.Add((Control) this.panelTableSize);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TableEditorDialog);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.TableEditorDialog_FormClosed);
    this.MouseClick += new MouseEventHandler(this.TableEditorDialog_MouseClick);
    this.panelTableSize.ResumeLayout(false);
    this.numeric_tH.EndInit();
    this.numeric_tBH.EndInit();
    this.numeric_tLH.EndInit();
    this.numeric_tRW.EndInit();
    this.numeric_tW.EndInit();
    this.numeric_tLW.EndInit();
    ((ISupportInitialize) this.pictureTableDimensions).EndInit();
    this.numeric_GridRowHeight.EndInit();
    this.numeric_ColumnsInsertWidth.EndInit();
    this.numeric_ColumnsInsertCount.EndInit();
    this.numeric_RowsInsertHeight.EndInit();
    this.numeric_RowsInsertCount.EndInit();
    this.numeric_HeaderRows.EndInit();
    this.panelTableType.ResumeLayout(false);
    this.panelTableType.PerformLayout();
    this.panelTableProperties.ResumeLayout(false);
    this.panelTableProperties.PerformLayout();
    this.panelTableColumns.ResumeLayout(false);
    this.panelTableColumns.PerformLayout();
    ((ISupportInitialize) this.grid_Columns).EndInit();
    this.panelTableRows.ResumeLayout(false);
    this.panelTableRows.PerformLayout();
    ((ISupportInitialize) this.grid_Rows).EndInit();
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.gridView1.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
