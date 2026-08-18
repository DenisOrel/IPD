// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TableEditControl
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevAge;
using DevExpress.IM.XtraEditors;
using Intermech.Bars;
using Intermech.Expert.Table;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using SourceGrid3;
using SourceGrid3.Cells;
using SourceGrid3.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Вьюшка редактор таблиц эксперной системы</summary>
public class TableEditControl : UserControl
{
  private Panel panel1;
  private Button bCancel;
  private TabControl tabControl1;
  private Grid _defGrid;
  private MenuBar menuBar1;
  private MenuButtonItem menu_Setup;
  private ContextMenuBarItem menu;
  private Button bApply;
  private MenuButtonItem menu_Copy;
  private MenuButtonItem menu_Copy_Row;
  private MenuButtonItem menu_Copy_Column;
  private MenuButtonItem menu_Paste;
  private MenuButtonItem menu_Paste_Row;
  private MenuButtonItem menu_Paste_Column;
  private MenuButtonItem menu_Clear;
  private MenuButtonItem menu_Clear_Row;
  private MenuButtonItem menu_Clear_Column;
  private MenuButtonItem menu_Clear_Cell;
  private MenuButtonItem menu_Clear_Layer;
  private MenuButtonItem menu_Add;
  private MenuButtonItem menu_Add_Row;
  private MenuButtonItem menu_Add_Column;
  private MenuButtonItem menu_Delete;
  private MenuButtonItem menu_Delete_Row;
  private MenuButtonItem menu_Delete_Column;
  private ContextMenuBarItem menu_Symbol;
  private Panel panFormula;
  private TextEdit teFormula;
  private Label label1;
  private bool _modified;
  private bool _readonly;
  private string _caption = string.Empty;
  private TableEditHelper _helper = new TableEditHelper();
  private bool _formMode;
  private eTable[] _tables;
  private TempFormula _formula;
  private eTable[] _oTables;
  private Panel panel2;
  private TempFormula _oFormula;

  /// <summary>Конструктор</summary>
  /// <param name="tableCaption">Заголовок таблицы</param>
  /// <param name="tables">Таблицы</param>
  /// <param name="formula">Формула</param>
  public TableEditControl(string tableCaption, eTable[] tables, TempFormula formula)
    : this(tableCaption, tables, formula, false)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="tableCaption">Заголовок таблицы</param>
  /// <param name="tables">Таблицы</param>
  /// <param name="formula">Формула</param>
  /// <param name="formMode">Режим "Форма"</param>
  public TableEditControl(
    string tableCaption,
    eTable[] tables,
    TempFormula formula,
    bool formMode)
  {
    this.InitializeComponent();
    this._formMode = formMode;
    if (this._formMode)
      this.bApply.Text = "OK";
    this._defGrid.UserException += new SourceGrid3.ExceptionEventHandler(this._defGrid_UserException);
    this._caption = tableCaption;
    this._oTables = tables;
    this._oFormula = formula;
    this.RollbackChanges();
    List<FieldInfo> fieldInfoList = new List<FieldInfo>();
    foreach (FieldInfo field in typeof (eCellSymbol).GetFields())
    {
      if (!fieldInfoList.Contains(field))
      {
        fieldInfoList.Add(field);
        object[] customAttributes = field.GetCustomAttributes(typeof (DescriptionAttribute), true);
        if (customAttributes.Length == 1 && customAttributes[0] is DescriptionAttribute)
          this.menu_Symbol.Items.Add((ToolbarItemBase) new MenuButtonItem((customAttributes[0] as DescriptionAttribute).Description));
      }
    }
  }

  /// <summary>Не выдавать сообщений о неверном формате данных</summary>
  private void _defGrid_UserException(object sender, SourceGrid3.ExceptionEventArgs e)
  {
    if (e.Exception == null || e.Exception.InnerException == null || !(e.Exception.InnerException is ConversionErrorException) || !e.Exception.Message.Contains("<null>"))
      return;
    e.Handled = true;
  }

  /// <summary>Активация контрола</summary>
  public void Activate()
  {
    this.menuBar1.Renderer = (ServicesManager.GetService(typeof (BarManager)) as BarManager).Renderer;
    this.TableToGrid();
  }

  /// <summary>Деактивация контрола</summary>
  public void Deactivate()
  {
    if (this.menuBar1 == null)
      return;
    this.menuBar1.Renderer = (IToolBarRenderer) new Office2002Renderer();
  }

  protected override void Dispose(bool disposing)
  {
    this.Deactivate();
    base.Dispose(disposing);
  }

  /// <summary>Изменения значений</summary>
  public bool Modified
  {
    get => this._modified;
    set
    {
      this._modified = value;
      if (this._formMode)
      {
        this.bApply.Enabled = value;
        this.bCancel.Enabled = true;
      }
      else
        this.panel1.Enabled = value;
    }
  }

  /// <summary>Отображение окна редактирования формулы</summary>
  public bool ShowFormulaEditBox
  {
    set => this.panFormula.Visible = value;
  }

  /// <summary>Только для чтения</summary>
  public bool ReadOnly
  {
    get => this._readonly;
    set => this._readonly = value;
  }

  /// <summary>Таблицы</summary>
  public eTable[] Tables
  {
    get => this._tables;
    set => this._tables = value;
  }

  /// <summary>Формула</summary>
  public TempFormula Formula
  {
    get => this._formula;
    set => this._formula = value;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableEditControl));
    this.panel1 = new Panel();
    this.bCancel = new Button();
    this.bApply = new Button();
    this.tabControl1 = new TabControl();
    this._defGrid = new Grid();
    this.menuBar1 = new MenuBar();
    this.menu = new ContextMenuBarItem();
    this.menu_Setup = new MenuButtonItem();
    this.menu_Copy = new MenuButtonItem();
    this.menu_Copy_Row = new MenuButtonItem();
    this.menu_Copy_Column = new MenuButtonItem();
    this.menu_Paste = new MenuButtonItem();
    this.menu_Paste_Row = new MenuButtonItem();
    this.menu_Paste_Column = new MenuButtonItem();
    this.menu_Clear = new MenuButtonItem();
    this.menu_Clear_Cell = new MenuButtonItem();
    this.menu_Clear_Row = new MenuButtonItem();
    this.menu_Clear_Column = new MenuButtonItem();
    this.menu_Clear_Layer = new MenuButtonItem();
    this.menu_Add = new MenuButtonItem();
    this.menu_Add_Row = new MenuButtonItem();
    this.menu_Add_Column = new MenuButtonItem();
    this.menu_Delete = new MenuButtonItem();
    this.menu_Delete_Row = new MenuButtonItem();
    this.menu_Delete_Column = new MenuButtonItem();
    this.menu_Symbol = new ContextMenuBarItem();
    this.panFormula = new Panel();
    this.teFormula = new TextEdit();
    this.label1 = new Label();
    this.panel2 = new Panel();
    this.panel1.SuspendLayout();
    this.panFormula.SuspendLayout();
    this.teFormula.Properties.BeginInit();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bApply);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.Name = "bCancel";
    this.bCancel.Click += new EventHandler(this.bCancel_Click);
    componentResourceManager.ApplyResources((object) this.bApply, "bApply");
    this.bApply.Name = "bApply";
    this.bApply.Click += new EventHandler(this.bApply_Click);
    componentResourceManager.ApplyResources((object) this.tabControl1, "tabControl1");
    this.tabControl1.Multiline = true;
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_TabIndexChanged);
    this._defGrid.GridToolTipActive = true;
    componentResourceManager.ApplyResources((object) this._defGrid, "_defGrid");
    this._defGrid.Name = "_defGrid";
    this._defGrid.SpecialKeys = GridSpecialKeys.Default;
    this._defGrid.StyleGrid = (StyleGrid) null;
    this._defGrid.MouseUp += new GridMouseEventHandler(this._defGrid_MouseUp);
    this.menuBar1.Guid = new Guid("374abe1c-77d6-4d91-b1a8-c9ad25937e74");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.menu,
      (ToolbarItemBase) this.menu_Symbol
    });
    componentResourceManager.ApplyResources((object) this.menuBar1, "menuBar1");
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.menu, "menu");
    this.menu.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.menu_Setup,
      (ToolbarItemBase) this.menu_Copy,
      (ToolbarItemBase) this.menu_Paste,
      (ToolbarItemBase) this.menu_Clear,
      (ToolbarItemBase) this.menu_Add,
      (ToolbarItemBase) this.menu_Delete
    });
    this.menu.ShowText = true;
    this.menu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.menu_BeforePopup);
    componentResourceManager.ApplyResources((object) this.menu_Setup, "menu_Setup");
    this.menu_Setup.ShowText = true;
    this.menu_Setup.Click += new EventHandler(this.menu_Setup_Click);
    this.menu_Copy.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.menu_Copy, "menu_Copy");
    this.menu_Copy.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.menu_Copy_Row,
      (ToolbarItemBase) this.menu_Copy_Column
    });
    this.menu_Copy.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menu_Copy_Row, "menu_Copy_Row");
    this.menu_Copy_Row.ShowText = true;
    this.menu_Copy_Row.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Copy_Column, "menu_Copy_Column");
    this.menu_Copy_Column.ShowText = true;
    this.menu_Copy_Column.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Paste, "menu_Paste");
    this.menu_Paste.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.menu_Paste_Row,
      (ToolbarItemBase) this.menu_Paste_Column
    });
    this.menu_Paste.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menu_Paste_Row, "menu_Paste_Row");
    this.menu_Paste_Row.ShowText = true;
    this.menu_Paste_Row.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Paste_Column, "menu_Paste_Column");
    this.menu_Paste_Column.ShowText = true;
    this.menu_Paste_Column.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Clear, "menu_Clear");
    this.menu_Clear.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.menu_Clear_Cell,
      (ToolbarItemBase) this.menu_Clear_Row,
      (ToolbarItemBase) this.menu_Clear_Column,
      (ToolbarItemBase) this.menu_Clear_Layer
    });
    this.menu_Clear.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menu_Clear_Cell, "menu_Clear_Cell");
    this.menu_Clear_Cell.ShowText = true;
    this.menu_Clear_Cell.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Clear_Row, "menu_Clear_Row");
    this.menu_Clear_Row.ShowText = true;
    this.menu_Clear_Row.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Clear_Column, "menu_Clear_Column");
    this.menu_Clear_Column.ShowText = true;
    this.menu_Clear_Column.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Clear_Layer, "menu_Clear_Layer");
    this.menu_Clear_Layer.ShowText = true;
    this.menu_Clear_Layer.Click += new EventHandler(this.menu_Click);
    this.menu_Add.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.menu_Add, "menu_Add");
    this.menu_Add.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.menu_Add_Row,
      (ToolbarItemBase) this.menu_Add_Column
    });
    this.menu_Add.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menu_Add_Row, "menu_Add_Row");
    this.menu_Add_Row.ShowText = true;
    this.menu_Add_Row.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Add_Column, "menu_Add_Column");
    this.menu_Add_Column.ShowText = true;
    this.menu_Add_Column.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Delete, "menu_Delete");
    this.menu_Delete.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.menu_Delete_Row,
      (ToolbarItemBase) this.menu_Delete_Column
    });
    this.menu_Delete.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menu_Delete_Row, "menu_Delete_Row");
    this.menu_Delete_Row.ShowText = true;
    this.menu_Delete_Row.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Delete_Column, "menu_Delete_Column");
    this.menu_Delete_Column.ShowText = true;
    this.menu_Delete_Column.Click += new EventHandler(this.menu_Click);
    componentResourceManager.ApplyResources((object) this.menu_Symbol, "menu_Symbol");
    this.menu_Symbol.ShowText = true;
    this.panFormula.Controls.Add((Control) this.teFormula);
    this.panFormula.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panFormula, "panFormula");
    this.panFormula.Name = "panFormula";
    componentResourceManager.ApplyResources((object) this.teFormula, "teFormula");
    this.teFormula.Name = "teFormula";
    this.teFormula.Properties.ReadOnly = true;
    this.teFormula.DoubleClick += new EventHandler(this.teFormula_DoubleClick);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Controls.Add((Control) this._defGrid);
    this.panel2.Controls.Add((Control) this.tabControl1);
    this.panel2.Name = "panel2";
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panFormula);
    this.Controls.Add((Control) this.menuBar1);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (TableEditControl);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ClientSizeChanged += new EventHandler(this.OnActivate);
    this.MouseUp += new MouseEventHandler(this.menu_MouseUp);
    this.panel1.ResumeLayout(false);
    this.panFormula.ResumeLayout(false);
    this.teFormula.Properties.EndInit();
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void menu_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    bool flag1 = this._tables != null && this._tables.Length != 0;
    bool flag2 = false;
    if (flag1)
    {
      Grid grid = this.tabControl1.TabPages.Count <= 0 ? this._defGrid : this.tabControl1.SelectedTab.Tag as Grid;
      ICell focusCell = grid.FocusCell;
      if (focusCell == null)
      {
        focusCell = grid[this._tables[0].FixedRows.Count, this._tables[0].FixedColumns.Count];
        grid.SetFocusCell(focusCell);
      }
      if (focusCell != null)
      {
        eCell cell = this._tables[0].GetCell(focusCell.Row, focusCell.Column);
        flag2 = cell != null && cell.CellDestination.Equals((object) eCellDestination.Data);
      }
    }
    this.menu_Copy.Enabled = this.menu_Paste.Enabled = flag2;
    if (this.menu_Paste.Enabled)
    {
      object dataObject = (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).GetDataObject();
      this.menu_Paste.Enabled = dataObject != null && (dataObject.GetType().Equals(typeof (eRow)) || dataObject.GetType().Equals(typeof (eColumn)));
      if (this.menu_Paste.Enabled)
      {
        this.menu_Paste_Row.Enabled = dataObject.GetType().Equals(typeof (eRow));
        this.menu_Paste_Column.Enabled = dataObject.GetType().Equals(typeof (eColumn));
      }
    }
    this.menu_Add.Enabled = this.menu_Clear.Enabled = this.menu_Delete.Enabled = flag2;
    this.menu_Add_Row.Enabled = flag1;
    MenuButtonItem menuAddColumn = this.menu_Add_Column;
    eTableType tableType;
    int num1;
    if (flag1)
    {
      tableType = this._tables[0].TableType;
      num1 = tableType.Equals((object) eTableType.DoubleEntry) ? 1 : 0;
    }
    else
      num1 = 0;
    menuAddColumn.Enabled = num1 != 0;
    this.menu_Delete_Row.Enabled = flag1 && this._tables[0].ValuesTable.RowsCount > 1;
    MenuButtonItem menuDeleteColumn = this.menu_Delete_Column;
    int num2;
    if (flag1 && this._tables[0].ValuesTable.ColumnsCount > 1)
    {
      tableType = this._tables[0].TableType;
      num2 = tableType.Equals((object) eTableType.DoubleEntry) ? 1 : 0;
    }
    else
      num2 = 0;
    menuDeleteColumn.Enabled = num2 != 0;
  }

  private void menu_Setup_Click(object sender, EventArgs e)
  {
    if (!(sender is ButtonItemBase buttonItemBase) || !(buttonItemBase.CommandName == "menu_Setup"))
      return;
    using (TableSetup tableSetup = new TableSetup(this._tables))
    {
      if (!tableSetup.ShowDialog().Equals((object) DialogResult.OK))
        return;
      this._tables = tableSetup.Tables;
      this.TableToGrid();
      this.Modified = true;
    }
  }

  private void menu_Click(object sender, EventArgs e)
  {
    Grid grid = !this.tabControl1.Visible || this.tabControl1.TabPages.Count <= 0 ? this._defGrid : this.tabControl1.SelectedTab.Tag as Grid;
    eTable tag = grid.Tag as eTable;
    ICell focusCell = grid.FocusCell;
    int num1 = focusCell.Row - tag.FixedRows.Count;
    int num2 = focusCell.Column - tag.FixedColumns.Count;
    IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
    if (!(sender is ButtonItemBase buttonItemBase))
      return;
    string commandName = buttonItemBase.CommandName;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
    {
      case 146784109:
        if (!(commandName == "menu_Add_Row"))
          break;
        switch (tag.TableType)
        {
          case eTableType.NoEntry:
          case eTableType.SingleEntry:
            eRow fixedRow1 = tag.FixedRows[0];
            eRow row1 = new eRow();
            foreach (eCell eCell in fixedRow1)
              row1.Add(new eCell(eCellDestination.Data, eCell.CommonType));
            tag.ValuesTable.InsertRow(num1, row1);
            if (tag.TableType.Equals((object) eTableType.SingleEntry))
            {
              foreach (eColumn fixedColumn in (IEnumerable<eColumn>) tag.FixedColumns)
                fixedColumn.Insert(num1, new eCell(eCellDestination.HeaderData, fixedColumn.Header != null ? fixedColumn.Header.CommonType : (CommonTypeHolder) null));
            }
            this.UpdateGrid(grid, tag);
            this.Modified = true;
            return;
          case eTableType.DoubleEntry:
            foreach (eTable table in this._tables)
            {
              table.ValuesTable.InsertRow(num1, new eRow(table.ValuesTable.ColumnsCount, eCellDestination.Data, table.Result[0]));
              foreach (eColumn fixedColumn in (IEnumerable<eColumn>) table.FixedColumns)
                fixedColumn.Insert(num1, new eCell(eCellDestination.HeaderData, fixedColumn.Header != null ? fixedColumn.Header.CommonType : (CommonTypeHolder) null));
            }
            this.UpdateGrid(grid, tag);
            this.Modified = true;
            return;
          default:
            return;
        }
      case 333394057:
        if (!(commandName == "menu_Copy_Column"))
          break;
        service.SetDataObject((object) tag.ValuesTable.GetColumn(num2));
        break;
      case 420334413:
        if (!(commandName == "menu_Clear_Cell"))
          break;
        tag.ValuesTable[num1, num2].CellValue = (ExpertValue) null;
        this.UpdateGrid(grid, tag);
        this.Modified = true;
        break;
      case 513721471:
        if (!(commandName == "menu_Clear_Column"))
          break;
        foreach (eCell eCell in tag.ValuesTable.GetColumn(num2))
          eCell.CellValue = (ExpertValue) null;
        this.UpdateGrid(grid, tag);
        this.Modified = true;
        break;
      case 1362711763:
        if (!(commandName == "menu_Clear_Row"))
          break;
        foreach (eCell eCell in tag.ValuesTable.GetRow(num1))
          eCell.CellValue = (ExpertValue) null;
        this.UpdateGrid(grid, tag);
        this.Modified = true;
        break;
      case 1710095341:
        if (!(commandName == "menu_Add_Column"))
          break;
        foreach (eTable table in this._tables)
        {
          table.ValuesTable.InsertColumn(num2, new eColumn(table.ValuesTable.RowsCount, eCellDestination.Data, table.Result[0]));
          foreach (eRow fixedRow2 in (IEnumerable<eRow>) table.FixedRows)
            fixedRow2.Insert(num2, new eCell(eCellDestination.HeaderData, fixedRow2.Header != null ? fixedRow2.Header.CommonType : (CommonTypeHolder) null));
        }
        this.UpdateGrid(grid, tag);
        this.Modified = true;
        break;
      case 1968780521:
        if (!(commandName == "menu_Copy_Row"))
          break;
        service.SetDataObject((object) tag.ValuesTable.GetRow(num1));
        break;
      case 3200109315:
        if (!(commandName == "menu_Delete_Column"))
          break;
        foreach (eTable table in this._tables)
        {
          table.ValuesTable.RemoveColumn(num2);
          foreach (eRow fixedRow3 in (IEnumerable<eRow>) table.FixedRows)
            fixedRow3.Remove(num2);
        }
        this.UpdateGrid(grid, tag);
        this.Modified = true;
        break;
      case 3418942622:
        if (!(commandName == "menu_Clear_Layer"))
          break;
        for (int row2 = 0; row2 < tag.ValuesTable.RowsCount; ++row2)
        {
          for (int column = 0; column < tag.ValuesTable.ColumnsCount; ++column)
            tag.ValuesTable[row2, column].CellValue = (ExpertValue) null;
        }
        this.UpdateGrid(grid, tag);
        this.Modified = true;
        break;
      case 3856118935:
        if (!(commandName == "menu_Delete_Row"))
          break;
        switch (tag.TableType)
        {
          case eTableType.NoEntry:
          case eTableType.SingleEntry:
            tag.ValuesTable.RemoveRow(num1);
            if (tag.TableType.Equals((object) eTableType.SingleEntry))
            {
              foreach (eColumn fixedColumn in (IEnumerable<eColumn>) tag.FixedColumns)
                fixedColumn.Remove(num1);
            }
            this.UpdateGrid(grid, tag);
            this.Modified = true;
            return;
          case eTableType.DoubleEntry:
            foreach (eTable table in this._tables)
            {
              table.ValuesTable.RemoveRow(num1);
              foreach (eColumn fixedColumn in (IEnumerable<eColumn>) table.FixedColumns)
                fixedColumn.Remove(num1);
            }
            this.UpdateGrid(grid, tag);
            this.Modified = true;
            return;
          default:
            return;
        }
      case 3906939077:
        if (!(commandName == "menu_Paste_Column"))
          break;
        eColumn dataObject1 = service.GetDataObject() as eColumn;
        if (dataObject1.RowsCount.Equals(tag.ValuesTable.RowsCount))
        {
          for (int index = 0; index < tag.ValuesTable.RowsCount; ++index)
          {
            eCell other = dataObject1[index];
            tag.ValuesTable[index, num2].Assign(other);
          }
          this.UpdateGrid(grid, tag);
          this.Modified = true;
          break;
        }
        int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_22"), LocalizationHolder.rm.GetString("Expert.Editor_23"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        break;
      case 4096094021:
        if (!(commandName == "menu_Paste_Row"))
          break;
        eRow dataObject2 = service.GetDataObject() as eRow;
        if (dataObject2.ColumnsCount.Equals(tag.ValuesTable.ColumnsCount))
        {
          for (int index = 0; index < tag.ValuesTable.ColumnsCount; ++index)
          {
            eCell other = dataObject2[index];
            tag.ValuesTable[num1, index].Assign(other);
          }
          this.UpdateGrid(grid, tag);
          this.Modified = true;
          break;
        }
        int num4 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_20"), LocalizationHolder.rm.GetString("Expert.Editor_21"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        break;
    }
  }

  private void menu_MouseUp(object sender, MouseEventArgs e)
  {
    if (this._readonly || !e.Button.Equals((object) MouseButtons.Right) || !(sender is Control))
      return;
    this.menu.Show(sender as Control, new Point(e.X, e.Y));
  }

  private void _defGrid_MouseUp(GridVirtual sender, MouseEventArgs e)
  {
    if (this._readonly || !e.Button.Equals((object) MouseButtons.Right))
      return;
    this.menu.Show((Control) sender, new Point(e.X, e.Y));
  }

  private void tabControl1_TabIndexChanged(object sender, EventArgs e)
  {
    TabPage selectedTab = this.tabControl1.SelectedTab;
    if (selectedTab == null)
      return;
    Grid tag1 = selectedTab.Tag as Grid;
    eTable tag2 = tag1.Tag as eTable;
    this.UpdateGrid(tag1, tag2);
  }

  private void bApply_Click(object sender, EventArgs e) => this.ApplyChanges();

  private void bCancel_Click(object sender, EventArgs e)
  {
    this.RollbackChanges();
    this.TableToGrid();
  }

  /// <summary>Принятие изменений</summary>
  public void ApplyChanges()
  {
    this.Modified = false;
    EventHandler onApplyChanges = this.OnApplyChanges;
    if (onApplyChanges == null)
      return;
    onApplyChanges((object) this, EventArgs.Empty);
  }

  public void SaveOrigCopy(eTable[] _conTables)
  {
    this._oTables = new eTable[_conTables.Length];
    for (int index = 0; index < _conTables.Length; ++index)
      this._oTables[index] = (eTable) _conTables[index].Clone();
  }

  /// <summary>Откат изменений</summary>
  public void RollbackChanges()
  {
    this._tables = new eTable[this._oTables.Length];
    for (int index = 0; index < this._oTables.Length; ++index)
      this._tables[index] = this._oTables[index].Clone() as eTable;
    this._formula = this._oFormula != null ? this._oFormula.Clone() as TempFormula : (TempFormula) null;
    this.Modified = false;
    EventHandler onRollbackChanges = this.OnRollbackChanges;
    if (onRollbackChanges == null)
      return;
    onRollbackChanges((object) this, EventArgs.Empty);
  }

  private void _helper_Modified(object sender) => this.Modified = true;

  private void teFormula_DoubleClick(object sender, EventArgs e)
  {
    if (this._readonly)
      return;
    using (FormEditor formEditor = new FormEditor())
    {
      TempFormula tF = new TempFormula();
      tF.Init();
      if (this._formula != null)
        tF = this._formula.Clone() as TempFormula;
      formEditor.CanReturnEmpty = true;
      if (!formEditor.Execute(ref tF, string.Format(LocalizationHolder.rm.GetString("Expert.Editor_26"), (object) this._caption), true) || !formEditor.Changed)
        return;
      this._formula = tF;
      this.FormulaToTextEdit();
      this.Modified = true;
    }
  }

  private void UpdateGrid(Grid grid, eTable table)
  {
    this._helper.Modified -= new ModifiedHandler(this._helper_Modified);
    this._helper.Detach();
    this._helper.SetInfo(table, this._tables, this._readonly);
    this._helper.Attach(grid, this.menu_Symbol);
    this._helper.Modified += new ModifiedHandler(this._helper_Modified);
  }

  private void OnActivate(object sender, EventArgs e)
  {
    if (this.Width <= 0 || this.Height <= 0)
      return;
    this._helper.windowHei = this.Height;
    this._helper.windowWid = this.Width;
    this._helper.FixFixedColumns();
  }

  private void TableToGrid()
  {
    if (this._tables != null && this._tables.Length != 0)
    {
      if (this._tables.Length.Equals(1))
      {
        this._defGrid.Dock = DockStyle.Fill;
        this._defGrid.Tag = (object) this._tables[0];
        this._defGrid.Visible = !(this.tabControl1.Visible = false);
        this.UpdateGrid(this._defGrid, this._tables[0]);
      }
      else
      {
        this._defGrid.FixedColumns = 0;
        this._defGrid.FixedRows = 0;
        this._defGrid.Redim(0, 0);
        this._defGrid.Visible = !(this.tabControl1.Visible = true);
        this.tabControl1.Dock = DockStyle.Fill;
        this.tabControl1.SuspendLayout();
        try
        {
          this.tabControl1.TabPages.Clear();
          foreach (eTable table in this._tables)
          {
            TabPage tabPage = new TabPage(table.Result[0].ToString());
            Grid grid = new Grid();
            grid.UserException += new SourceGrid3.ExceptionEventHandler(this._defGrid_UserException);
            tabPage.SuspendLayout();
            grid.Dock = DockStyle.Fill;
            grid.Tag = (object) table;
            grid.MouseUp += new GridMouseEventHandler(this._defGrid_MouseUp);
            grid.Parent = (Control) tabPage;
            tabPage.Tag = (object) grid;
            tabPage.ResumeLayout(false);
            this.tabControl1.TabPages.Add(tabPage);
          }
          this.tabControl1.SelectedTab = this.tabControl1.TabPages[0];
        }
        finally
        {
          this.tabControl1.ResumeLayout(false);
        }
        this.UpdateGrid(this.tabControl1.SelectedTab.Tag as Grid, this._tables[0]);
      }
    }
    this.FormulaToTextEdit();
  }

  private void FormulaToTextEdit()
  {
    this.teFormula.EditValue = (object) null;
    if (this._formula == null)
      return;
    this.teFormula.EditValue = (object) this._formula.ToString();
  }

  /// <summary>Событие на сохранение изменений</summary>
  public event EventHandler OnApplyChanges;

  /// <summary>Событие на отмену изменений</summary>
  public event EventHandler OnRollbackChanges;
}
