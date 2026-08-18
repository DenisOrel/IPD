// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TableSetup
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Expert.Table;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Форма настройки таблицы</summary>
public class TableSetup : Form
{
  private Panel panel1;
  private Button bOk;
  private Button bCancel;
  private Panel panel2;
  private Panel panel3;
  private Label label1;
  private TextBox tbName;
  private Label label2;
  private System.Windows.Forms.ComboBox cbType;
  private Panel panel4;
  private GroupBox gbVert;
  private GroupBox gbHorz;
  private Splitter splitter1;
  private Label label3;
  private Label label4;
  private Panel panel5;
  private Panel panel6;
  private Label label5;
  private Label label6;
  private Panel panel7;
  private Panel panel8;
  private Panel panel9;
  private GroupBox gbResult;
  private Panel panel10;
  private Label label7;
  private Panel panel11;
  private Label label8;
  private ListBox lbHorz;
  private ListBox lbVert;
  private ArrayList _tables = new ArrayList();
  private MenuBar menuBar1;
  private MenuButtonItem vert_Add;
  private MenuButtonItem vert_Change;
  private MenuButtonItem vert_Clear;
  private ContextMenuBarItem lbVertContext;
  private MenuButtonItem vert_Delete;
  private SpinEdit spinHorz;
  private SpinEdit spinVert;
  private SpinEdit spinResult;
  private ContextMenuBarItem lbHorzContext;
  private MenuButtonItem horz_Change;
  private MenuButtonItem horz_Delete;
  private MenuButtonItem horz_Clear;
  private ContextMenuBarItem lbResultContext;
  private MenuButtonItem result_Add;
  private MenuButtonItem result_Change;
  private MenuButtonItem result_Delete;
  private MenuButtonItem result_Clear;
  private MenuButtonItem horz_Add;
  private MenuButtonItem result_Move;
  private MenuButtonItem result_Up;
  private MenuButtonItem result_Down;
  private MenuButtonItem horz_Move;
  private MenuButtonItem horz_Up;
  private MenuButtonItem horz_Down;
  private MenuButtonItem vert_Move;
  private MenuButtonItem vert_Up;
  private MenuButtonItem vert_Down;
  private MenuButtonItem horz_Entry;
  private MenuButtonItem horz_EntryAdd;
  private MenuButtonItem horz_EntryRemove;
  private eTableType _previousType = eTableType.SingleEntry;
  private ListBox lbResult;
  /// <summary>
  /// 
  /// </summary>
  private bool _allowAnyObjectType = true;
  /// <summary>
  /// 
  /// </summary>
  private bool _allowEmptyTableName;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Конструктор</summary>
  public TableSetup()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1325);
    this.spinVert.Properties.MinValue = 1M;
    this.spinVert.Properties.MaxValue = 2147483647M;
    this.spinHorz.Properties.MinValue = 1M;
    this.spinHorz.Properties.MaxValue = 2147483647M;
    this.spinResult.Properties.MinValue = 1M;
    this.spinResult.Properties.MaxValue = 2147483647M;
    foreach (FieldInfo field in typeof (eTableType).GetFields())
    {
      eTableType eTableType = (eTableType) field.GetValue((object) eTableType.NoEntry);
      string caption = EnumTypeHelper.GetCaption((Enum) eTableType);
      if (!this.cbType.Items.Contains((object) caption))
      {
        this.cbType.Items.Add((object) caption);
        if (eTableType.Equals((object) eTableType.SingleEntry))
          this.cbType.SelectedItem = (object) caption;
      }
    }
    this.spinResult.Enabled = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="tables">список таблиц</param>
  public TableSetup(eTable[] tables)
    : this()
  {
    if (tables == null)
      return;
    this._tables.AddRange((ICollection) tables);
    this.ParseTables();
  }

  /// <summary>Dispose</summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Список настроенных таблиц (с данными или без)</summary>
  public eTable[] Tables => this._tables.ToArray(typeof (eTable)) as eTable[];

  /// <summary>Разрешает настривать атрибуты на любой тип объекта</summary>
  public bool AllowAnyObjectType
  {
    get => this._allowAnyObjectType;
    set => this._allowAnyObjectType = value;
  }

  /// <summary>Разрешает создавать таблицы с пустым наименованием</summary>
  public bool AllowEmptyTableName
  {
    get => this._allowEmptyTableName;
    set => this._allowEmptyTableName = value;
  }

  private void ParseTables()
  {
    this.lbVert.Items.Clear();
    this.lbHorz.Items.Clear();
    this.lbResult.Items.Clear();
    this.spinVert.Value = 0M;
    this.spinHorz.Value = 0M;
    this.spinResult.Value = 0M;
    if (this._tables.Count <= 0)
      return;
    eTable table1 = this._tables[0] as eTable;
    this.tbName.Text = table1.Name;
    switch (table1.TableType)
    {
      case eTableType.NoEntry:
        this.cbType.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) table1.TableType);
        IEnumerator enumerator1 = table1.FixedRows[0].GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
          {
            eCell current = (eCell) enumerator1.Current;
            eCellDestination cellDestination = current.CellDestination;
            if (cellDestination.Equals((object) eCellDestination.Header))
            {
              this.lbHorz.Items.Add((object) current.CommonType);
            }
            else
            {
              cellDestination = current.CellDestination;
              if (cellDestination.Equals((object) eCellDestination.Result))
              {
                this.lbResult.Items.Add((object) current.CommonType);
                this.lbHorz.Items.Add((object) current.CommonType);
              }
            }
          }
          break;
        }
        finally
        {
          if (enumerator1 is IDisposable disposable)
            disposable.Dispose();
        }
      case eTableType.SingleEntry:
        this.cbType.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) table1.TableType);
        foreach (eCell eCell in table1.FixedRows[0])
        {
          if (eCell.CellDestination.Equals((object) eCellDestination.Result))
            this.lbResult.Items.Add((object) eCell.CommonType);
        }
        using (IEnumerator<eColumn> enumerator2 = table1.FixedColumns.GetEnumerator())
        {
          while (enumerator2.MoveNext())
          {
            eColumn current = enumerator2.Current;
            if (current.Header != null)
            {
              this.lbVert.Items.Add((object) current.Header.CommonType);
            }
            else
            {
              foreach (eCell eCell in current)
              {
                if (eCell.CellDestination.Equals((object) eCellDestination.Header))
                  this.lbVert.Items.Add((object) eCell.CommonType);
              }
            }
          }
          break;
        }
      case eTableType.DoubleEntry:
        this.cbType.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) table1.TableType);
        foreach (eTable table2 in this._tables)
          this.lbResult.Items.Add((object) table2.Result[0]);
        foreach (eColumn fixedColumn in (IEnumerable<eColumn>) table1.FixedColumns)
        {
          if (fixedColumn.Header != null)
          {
            this.lbVert.Items.Add((object) fixedColumn.Header.CommonType);
          }
          else
          {
            foreach (eCell eCell in fixedColumn)
            {
              if (eCell.CellDestination.Equals((object) eCellDestination.Header))
                this.lbVert.Items.Add((object) eCell.CommonType);
            }
          }
        }
        using (IEnumerator<eRow> enumerator3 = table1.FixedRows.GetEnumerator())
        {
          while (enumerator3.MoveNext())
          {
            eRow current = enumerator3.Current;
            if (current.Header != null)
            {
              this.lbHorz.Items.Add((object) current.Header.CommonType);
            }
            else
            {
              foreach (eCell eCell in current)
              {
                if (eCell.CellDestination.Equals((object) eCellDestination.Header) && eCell.CommonType != null)
                  this.lbHorz.Items.Add((object) eCell.CommonType);
              }
            }
          }
          break;
        }
    }
    this.spinVert.Value = (Decimal) table1.ValuesTable.RowsCount;
    this.spinHorz.Value = (Decimal) table1.ValuesTable.ColumnsCount;
    this.spinResult.Value = (Decimal) this.lbResult.Items.Count;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableSetup));
    this.panel1 = new Panel();
    this.bCancel = new Button();
    this.bOk = new Button();
    this.panel2 = new Panel();
    this.tbName = new TextBox();
    this.label1 = new Label();
    this.panel3 = new Panel();
    this.cbType = new System.Windows.Forms.ComboBox();
    this.label2 = new Label();
    this.panel4 = new Panel();
    this.gbHorz = new GroupBox();
    this.panel8 = new Panel();
    this.lbHorz = new ListBox();
    this.panel6 = new Panel();
    this.label4 = new Label();
    this.spinHorz = new SpinEdit();
    this.label6 = new Label();
    this.splitter1 = new Splitter();
    this.gbVert = new GroupBox();
    this.panel7 = new Panel();
    this.lbVert = new ListBox();
    this.panel5 = new Panel();
    this.label3 = new Label();
    this.spinVert = new SpinEdit();
    this.label5 = new Label();
    this.panel9 = new Panel();
    this.gbResult = new GroupBox();
    this.panel11 = new Panel();
    this.lbResult = new ListBox();
    this.panel10 = new Panel();
    this.label7 = new Label();
    this.spinResult = new SpinEdit();
    this.label8 = new Label();
    this.menuBar1 = new MenuBar();
    this.lbVertContext = new ContextMenuBarItem();
    this.vert_Add = new MenuButtonItem();
    this.vert_Change = new MenuButtonItem();
    this.vert_Delete = new MenuButtonItem();
    this.vert_Clear = new MenuButtonItem();
    this.vert_Move = new MenuButtonItem();
    this.vert_Up = new MenuButtonItem();
    this.vert_Down = new MenuButtonItem();
    this.lbHorzContext = new ContextMenuBarItem();
    this.horz_Entry = new MenuButtonItem();
    this.horz_EntryAdd = new MenuButtonItem();
    this.horz_EntryRemove = new MenuButtonItem();
    this.horz_Add = new MenuButtonItem();
    this.horz_Change = new MenuButtonItem();
    this.horz_Delete = new MenuButtonItem();
    this.horz_Clear = new MenuButtonItem();
    this.horz_Move = new MenuButtonItem();
    this.horz_Up = new MenuButtonItem();
    this.horz_Down = new MenuButtonItem();
    this.lbResultContext = new ContextMenuBarItem();
    this.result_Add = new MenuButtonItem();
    this.result_Change = new MenuButtonItem();
    this.result_Delete = new MenuButtonItem();
    this.result_Clear = new MenuButtonItem();
    this.result_Move = new MenuButtonItem();
    this.result_Up = new MenuButtonItem();
    this.result_Down = new MenuButtonItem();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panel4.SuspendLayout();
    this.gbHorz.SuspendLayout();
    this.panel8.SuspendLayout();
    this.panel6.SuspendLayout();
    this.spinHorz.Properties.BeginInit();
    this.gbVert.SuspendLayout();
    this.panel7.SuspendLayout();
    this.panel5.SuspendLayout();
    this.spinVert.Properties.BeginInit();
    this.panel9.SuspendLayout();
    this.gbResult.SuspendLayout();
    this.panel11.SuspendLayout();
    this.panel10.SuspendLayout();
    this.spinResult.Properties.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOk);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    componentResourceManager.ApplyResources((object) this.bOk, "bOk");
    this.bOk.Name = "bOk";
    this.bOk.Click += new EventHandler(this.bOk_Click);
    this.panel2.Controls.Add((Control) this.tbName);
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Controls.Add((Control) this.panel3);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.tbName, "tbName");
    this.tbName.Name = "tbName";
    this.tbName.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panel3.Controls.Add((Control) this.cbType);
    this.panel3.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.cbType, "cbType");
    this.cbType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbType.Name = "cbType";
    this.cbType.SelectedIndexChanged += new EventHandler(this.cbType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.panel4.Controls.Add((Control) this.gbHorz);
    this.panel4.Controls.Add((Control) this.splitter1);
    this.panel4.Controls.Add((Control) this.gbVert);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    this.panel4.Resize += new EventHandler(this.panel4_Resize);
    this.gbHorz.Controls.Add((Control) this.panel8);
    this.gbHorz.Controls.Add((Control) this.panel6);
    componentResourceManager.ApplyResources((object) this.gbHorz, "gbHorz");
    this.gbHorz.FlatStyle = FlatStyle.System;
    this.gbHorz.Name = "gbHorz";
    this.gbHorz.TabStop = false;
    this.panel8.Controls.Add((Control) this.lbHorz);
    componentResourceManager.ApplyResources((object) this.panel8, "panel8");
    this.panel8.Name = "panel8";
    componentResourceManager.ApplyResources((object) this.lbHorz, "lbHorz");
    this.lbHorz.Name = "lbHorz";
    this.lbHorz.MouseUp += new MouseEventHandler(this.lbHorz_MouseUp);
    this.lbHorz.KeyDown += new KeyEventHandler(this.lbHorz_KeyDown);
    this.panel6.Controls.Add((Control) this.label4);
    this.panel6.Controls.Add((Control) this.spinHorz);
    this.panel6.Controls.Add((Control) this.label6);
    componentResourceManager.ApplyResources((object) this.panel6, "panel6");
    this.panel6.Name = "panel6";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.spinHorz, "spinHorz");
    this.spinHorz.Name = "spinHorz";
    this.spinHorz.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.spinHorz.Properties.UseCtrlIncrement = false;
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.gbVert.Controls.Add((Control) this.panel7);
    this.gbVert.Controls.Add((Control) this.panel5);
    componentResourceManager.ApplyResources((object) this.gbVert, "gbVert");
    this.gbVert.FlatStyle = FlatStyle.System;
    this.gbVert.Name = "gbVert";
    this.gbVert.TabStop = false;
    this.panel7.Controls.Add((Control) this.lbVert);
    componentResourceManager.ApplyResources((object) this.panel7, "panel7");
    this.panel7.Name = "panel7";
    this.lbVert.BackColor = SystemColors.Window;
    componentResourceManager.ApplyResources((object) this.lbVert, "lbVert");
    this.lbVert.Name = "lbVert";
    this.lbVert.MouseUp += new MouseEventHandler(this.lbVert_MouseUp);
    this.lbVert.KeyDown += new KeyEventHandler(this.lbVert_KeyDown);
    this.panel5.Controls.Add((Control) this.label3);
    this.panel5.Controls.Add((Control) this.spinVert);
    this.panel5.Controls.Add((Control) this.label5);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Name = "panel5";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.spinVert, "spinVert");
    this.spinVert.Name = "spinVert";
    this.spinVert.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.spinVert.Properties.UseCtrlIncrement = false;
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    this.panel9.Controls.Add((Control) this.gbResult);
    componentResourceManager.ApplyResources((object) this.panel9, "panel9");
    this.panel9.Name = "panel9";
    this.gbResult.Controls.Add((Control) this.panel11);
    this.gbResult.Controls.Add((Control) this.panel10);
    componentResourceManager.ApplyResources((object) this.gbResult, "gbResult");
    this.gbResult.FlatStyle = FlatStyle.System;
    this.gbResult.Name = "gbResult";
    this.gbResult.TabStop = false;
    this.panel11.Controls.Add((Control) this.lbResult);
    componentResourceManager.ApplyResources((object) this.panel11, "panel11");
    this.panel11.Name = "panel11";
    componentResourceManager.ApplyResources((object) this.lbResult, "lbResult");
    this.lbResult.Name = "lbResult";
    this.lbResult.MouseUp += new MouseEventHandler(this.lbResult_MouseUp);
    this.lbResult.KeyDown += new KeyEventHandler(this.lbResult_KeyDown);
    this.panel10.Controls.Add((Control) this.label7);
    this.panel10.Controls.Add((Control) this.spinResult);
    this.panel10.Controls.Add((Control) this.label8);
    componentResourceManager.ApplyResources((object) this.panel10, "panel10");
    this.panel10.Name = "panel10";
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    componentResourceManager.ApplyResources((object) this.spinResult, "spinResult");
    this.spinResult.Name = "spinResult";
    this.spinResult.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.spinResult.Properties.UseCtrlIncrement = false;
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    this.menuBar1.Guid = new Guid("a8887a34-b0c3-4c8b-98f0-72bd6f0a870f");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.lbVertContext,
      (ToolbarItemBase) this.lbHorzContext,
      (ToolbarItemBase) this.lbResultContext
    });
    componentResourceManager.ApplyResources((object) this.menuBar1, "menuBar1");
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.lbVertContext, "lbVertContext");
    this.lbVertContext.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.vert_Add,
      (ToolbarItemBase) this.vert_Change,
      (ToolbarItemBase) this.vert_Delete,
      (ToolbarItemBase) this.vert_Clear,
      (ToolbarItemBase) this.vert_Move
    });
    this.lbVertContext.ShowText = true;
    this.lbVertContext.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.lbVertContext_BeforePopup);
    componentResourceManager.ApplyResources((object) this.vert_Add, "vert_Add");
    this.vert_Add.ShowText = true;
    this.vert_Add.Click += new EventHandler(this.lbVertContext_Click);
    componentResourceManager.ApplyResources((object) this.vert_Change, "vert_Change");
    this.vert_Change.ShowText = true;
    this.vert_Change.Click += new EventHandler(this.lbVertContext_Click);
    componentResourceManager.ApplyResources((object) this.vert_Delete, "vert_Delete");
    this.vert_Delete.ShowText = true;
    this.vert_Delete.Click += new EventHandler(this.lbVertContext_Click);
    componentResourceManager.ApplyResources((object) this.vert_Clear, "vert_Clear");
    this.vert_Clear.ShowText = true;
    this.vert_Clear.Click += new EventHandler(this.lbVertContext_Click);
    this.vert_Move.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.vert_Move, "vert_Move");
    this.vert_Move.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.vert_Up,
      (ToolbarItemBase) this.vert_Down
    });
    this.vert_Move.ShowText = true;
    componentResourceManager.ApplyResources((object) this.vert_Up, "vert_Up");
    this.vert_Up.ShowText = true;
    this.vert_Up.Click += new EventHandler(this.lbVertContext_Click);
    componentResourceManager.ApplyResources((object) this.vert_Down, "vert_Down");
    this.vert_Down.ShowText = true;
    this.vert_Down.Click += new EventHandler(this.lbVertContext_Click);
    componentResourceManager.ApplyResources((object) this.lbHorzContext, "lbHorzContext");
    this.lbHorzContext.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.horz_Entry,
      (ToolbarItemBase) this.horz_Add,
      (ToolbarItemBase) this.horz_Change,
      (ToolbarItemBase) this.horz_Delete,
      (ToolbarItemBase) this.horz_Clear,
      (ToolbarItemBase) this.horz_Move
    });
    this.lbHorzContext.ShowText = true;
    this.lbHorzContext.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.lbHorzContext_BeforePopup);
    componentResourceManager.ApplyResources((object) this.horz_Entry, "horz_Entry");
    this.horz_Entry.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.horz_EntryAdd,
      (ToolbarItemBase) this.horz_EntryRemove
    });
    this.horz_Entry.ShowText = true;
    componentResourceManager.ApplyResources((object) this.horz_EntryAdd, "horz_EntryAdd");
    this.horz_EntryAdd.ShowText = true;
    this.horz_EntryAdd.Click += new EventHandler(this.lbHorzContext_Click);
    componentResourceManager.ApplyResources((object) this.horz_EntryRemove, "horz_EntryRemove");
    this.horz_EntryRemove.ShowText = true;
    this.horz_EntryRemove.Click += new EventHandler(this.lbHorzContext_Click);
    this.horz_Add.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.horz_Add, "horz_Add");
    this.horz_Add.ShowText = true;
    this.horz_Add.Click += new EventHandler(this.lbHorzContext_Click);
    componentResourceManager.ApplyResources((object) this.horz_Change, "horz_Change");
    this.horz_Change.ShowText = true;
    this.horz_Change.Click += new EventHandler(this.lbHorzContext_Click);
    componentResourceManager.ApplyResources((object) this.horz_Delete, "horz_Delete");
    this.horz_Delete.ShowText = true;
    this.horz_Delete.Click += new EventHandler(this.lbHorzContext_Click);
    componentResourceManager.ApplyResources((object) this.horz_Clear, "horz_Clear");
    this.horz_Clear.ShowText = true;
    this.horz_Clear.Click += new EventHandler(this.lbHorzContext_Click);
    this.horz_Move.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.horz_Move, "horz_Move");
    this.horz_Move.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.horz_Up,
      (ToolbarItemBase) this.horz_Down
    });
    this.horz_Move.ShowText = true;
    componentResourceManager.ApplyResources((object) this.horz_Up, "horz_Up");
    this.horz_Up.ShowText = true;
    this.horz_Up.Click += new EventHandler(this.lbHorzContext_Click);
    componentResourceManager.ApplyResources((object) this.horz_Down, "horz_Down");
    this.horz_Down.ShowText = true;
    this.horz_Down.Click += new EventHandler(this.lbHorzContext_Click);
    componentResourceManager.ApplyResources((object) this.lbResultContext, "lbResultContext");
    this.lbResultContext.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.result_Add,
      (ToolbarItemBase) this.result_Change,
      (ToolbarItemBase) this.result_Delete,
      (ToolbarItemBase) this.result_Clear,
      (ToolbarItemBase) this.result_Move
    });
    this.lbResultContext.ShowText = true;
    this.lbResultContext.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.lbResultContext_BeforePopup);
    componentResourceManager.ApplyResources((object) this.result_Add, "result_Add");
    this.result_Add.ShowText = true;
    this.result_Add.Click += new EventHandler(this.lbResultContext_Click);
    componentResourceManager.ApplyResources((object) this.result_Change, "result_Change");
    this.result_Change.ShowText = true;
    this.result_Change.Click += new EventHandler(this.lbResultContext_Click);
    componentResourceManager.ApplyResources((object) this.result_Delete, "result_Delete");
    this.result_Delete.ShowText = true;
    this.result_Delete.Click += new EventHandler(this.lbResultContext_Click);
    componentResourceManager.ApplyResources((object) this.result_Clear, "result_Clear");
    this.result_Clear.ShowText = true;
    this.result_Clear.Click += new EventHandler(this.lbResultContext_Click);
    this.result_Move.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.result_Move, "result_Move");
    this.result_Move.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.result_Up,
      (ToolbarItemBase) this.result_Down
    });
    this.result_Move.ShowText = true;
    componentResourceManager.ApplyResources((object) this.result_Up, "result_Up");
    this.result_Up.ShowText = true;
    this.result_Up.Click += new EventHandler(this.lbResultContext_Click);
    componentResourceManager.ApplyResources((object) this.result_Down, "result_Down");
    this.result_Down.ShowText = true;
    this.result_Down.Click += new EventHandler(this.lbResultContext_Click);
    this.AcceptButton = (IButtonControl) this.bOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.panel4);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel9);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.menuBar1);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TableSetup);
    this.Load += new EventHandler(this.TableSetup_Load);
    this.Closed += new EventHandler(this.TableSetup_Closed);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.gbHorz.ResumeLayout(false);
    this.panel8.ResumeLayout(false);
    this.panel6.ResumeLayout(false);
    this.spinHorz.Properties.EndInit();
    this.gbVert.ResumeLayout(false);
    this.panel7.ResumeLayout(false);
    this.panel5.ResumeLayout(false);
    this.spinVert.Properties.EndInit();
    this.panel9.ResumeLayout(false);
    this.gbResult.ResumeLayout(false);
    this.panel11.ResumeLayout(false);
    this.panel10.ResumeLayout(false);
    this.spinResult.Properties.EndInit();
    this.ResumeLayout(false);
  }

  private void TableSetup_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    if (!(ServicesManager.GetService(typeof (BarManager)) is BarManager service))
      return;
    this.menuBar1.Renderer = service.Renderer;
  }

  private void TableSetup_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
    this.menuBar1.Renderer = (IToolBarRenderer) new Office2003Renderer();
  }

  private void panel4_Resize(object sender, EventArgs e)
  {
    this.gbVert.Width = this.panel4.Width / 2;
  }

  private void ClearShortcut()
  {
    this.vert_Up.Shortcut = Shortcut.None;
    this.vert_Down.Shortcut = Shortcut.None;
    this.horz_Up.Shortcut = Shortcut.None;
    this.horz_Down.Shortcut = Shortcut.None;
    this.result_Up.Shortcut = Shortcut.None;
    this.result_Down.Shortcut = Shortcut.None;
  }

  private void lbVertContext_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.ClearShortcut();
    bool flag = this.lbVert.SelectedItem != null;
    this.vert_Add.Enabled = true;
    this.vert_Change.Enabled = flag;
    this.vert_Delete.Enabled = flag;
    this.vert_Clear.Enabled = flag;
    this.vert_Move.Enabled = flag;
    if (!flag)
      return;
    this.vert_Up.Shortcut = Shortcut.CtrlU;
    this.vert_Up.Enabled = this.lbVert.SelectedIndex > 0;
    this.vert_Down.Shortcut = Shortcut.CtrlD;
    this.vert_Down.Enabled = this.lbVert.SelectedIndex < this.lbVert.Items.Count - 1;
  }

  private void lbVertContext_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(sender is ButtonItemBase buttonItemBase))
        return;
      switch (buttonItemBase.CommandName)
      {
        case "vert_Add":
          using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object))
          {
            advSelectorForm.AttributeTypesMultiselect = true;
            if (!advSelectorForm.ShowDialog().Equals((object) DialogResult.OK) || advSelectorForm.ObjectType.Equals(-1) && !this._allowAnyObjectType)
              break;
            foreach (int attributeType in advSelectorForm.AttributeTypes)
            {
              CommonTypeHolder commonTypeHolder = new CommonTypeHolder(advSelectorForm.ObjectType, attributeType, session);
              if (!this.lbVert.Items.Contains((object) commonTypeHolder))
              {
                this.lbVert.Items.Add((object) commonTypeHolder);
              }
              else
              {
                int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_31"), LocalizationHolder.rm.GetString("Expert.Editor_32"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              }
            }
            break;
          }
        case "vert_Change":
          CommonTypeHolder selectedItem1 = this.lbVert.SelectedItem as CommonTypeHolder;
          int selectID = -1;
          if (!selectedItem1.ObjectType.Guid.Equals(Guid.Empty))
            selectID = sessionKeeper.Session.GetObjectType(selectedItem1.ObjectType.Guid).ObjectType;
          int attributeId = sessionKeeper.Session.GetAttributeType(selectedItem1.AttributeType.Guid).AttributeID;
          using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, selectID, new int[1]
          {
            attributeId
          }))
          {
            if (!advSelectorForm.ShowDialog().Equals((object) DialogResult.OK) || advSelectorForm.ObjectType.Equals(-1) && !this._allowAnyObjectType)
              break;
            CommonTypeHolder commonTypeHolder = new CommonTypeHolder(advSelectorForm.ObjectType, advSelectorForm.AttributeTypes[0], session);
            if (!this.lbVert.Items.Contains((object) commonTypeHolder))
            {
              int selectedIndex = this.lbVert.SelectedIndex;
              this.lbVert.Items.RemoveAt(selectedIndex);
              this.lbVert.Items.Insert(selectedIndex, (object) commonTypeHolder);
              this.lbVert.SelectedIndex = selectedIndex;
              break;
            }
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_33"), LocalizationHolder.rm.GetString("Expert.Editor_34"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;
          }
        case "vert_Delete":
          this.lbVert.Items.Remove(this.lbVert.SelectedItem);
          break;
        case "vert_Clear":
          this.lbVert.Items.Clear();
          break;
        case "vert_Up":
          int index1 = this.lbVert.SelectedIndex - 1;
          object selectedItem2 = this.lbVert.SelectedItem;
          this.lbVert.Items.Remove(selectedItem2);
          this.lbVert.Items.Insert(index1, selectedItem2);
          this.lbVert.SelectedItem = selectedItem2;
          break;
        case "vert_Down":
          int index2 = this.lbVert.SelectedIndex + 1;
          object selectedItem3 = this.lbVert.SelectedItem;
          this.lbVert.Items.Remove(selectedItem3);
          this.lbVert.Items.Insert(index2, selectedItem3);
          this.lbVert.SelectedItem = selectedItem3;
          break;
      }
    }
  }

  private void lbVert_MouseUp(object sender, MouseEventArgs e)
  {
    if (!e.Button.Equals((object) MouseButtons.Right))
      return;
    this.lbVert.SelectedIndex = this.lbVert.IndexFromPoint(e.X, e.Y);
    this.lbVertContext.Show((Control) this.lbVert, new Point(e.X, e.Y));
  }

  private void lbVert_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.Modifiers.Equals((object) Keys.Control) || this.lbVert.SelectedItem == null)
      return;
    switch (e.KeyCode)
    {
      case Keys.Up:
      case Keys.U:
        if (this.lbVert.SelectedIndex <= 0)
          break;
        this.lbVertContext_Click((object) this.vert_Up, (EventArgs) null);
        break;
      case Keys.Down:
      case Keys.D:
        if (this.lbVert.SelectedIndex >= this.lbVert.Items.Count - 1)
          break;
        this.lbVertContext_Click((object) this.vert_Down, (EventArgs) null);
        break;
    }
  }

  private void lbHorzContext_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.ClearShortcut();
    bool flag = this.lbHorz.SelectedItem != null;
    this.horz_Add.Enabled = true;
    this.horz_Change.Enabled = flag;
    this.horz_Delete.Enabled = flag;
    this.horz_Clear.Enabled = flag;
    this.horz_Move.Enabled = flag;
    if (flag)
    {
      this.horz_Up.Shortcut = Shortcut.CtrlU;
      this.horz_Up.Enabled = this.lbHorz.SelectedIndex > 0;
      this.horz_Down.Shortcut = Shortcut.CtrlD;
      this.horz_Down.Enabled = this.lbHorz.SelectedIndex < this.lbHorz.Items.Count - 1;
    }
    this.horz_Entry.Enabled = flag;
    this.horz_Entry.Visible = this.cbType.Text.Equals(EnumTypeHelper.GetCaption((Enum) eTableType.NoEntry));
    if (!flag)
      return;
    this.horz_EntryAdd.Enabled = !(this.horz_EntryRemove.Enabled = this.lbResult.Items.Contains(this.lbHorz.SelectedItem));
  }

  private void lbHorzContext_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(sender is ButtonItemBase buttonItemBase))
        return;
      switch (buttonItemBase.CommandName)
      {
        case "horz_Add":
          using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object))
          {
            advSelectorForm.AttributeTypesMultiselect = true;
            if (advSelectorForm.ShowDialog().Equals((object) DialogResult.OK))
            {
              if (advSelectorForm.ObjectType.Equals(-1))
              {
                if (!this._allowAnyObjectType)
                  break;
              }
              foreach (int attributeType in advSelectorForm.AttributeTypes)
              {
                CommonTypeHolder commonTypeHolder = new CommonTypeHolder(advSelectorForm.ObjectType, attributeType, session);
                if (!this.lbHorz.Items.Contains((object) commonTypeHolder))
                {
                  this.lbHorz.Items.Add((object) commonTypeHolder);
                }
                else
                {
                  int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_35"), LocalizationHolder.rm.GetString("Expert.Editor_36"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
              }
              break;
            }
            break;
          }
        case "horz_Change":
          CommonTypeHolder selectedItem1 = this.lbHorz.SelectedItem as CommonTypeHolder;
          int selectID = -1;
          if (!selectedItem1.ObjectType.Guid.Equals(Guid.Empty))
            selectID = sessionKeeper.Session.GetObjectType(selectedItem1.ObjectType.Guid).ObjectType;
          int attributeId = sessionKeeper.Session.GetAttributeType(selectedItem1.AttributeType.Guid).AttributeID;
          using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, selectID, new int[1]
          {
            attributeId
          }))
          {
            if (advSelectorForm.ShowDialog().Equals((object) DialogResult.OK))
            {
              if (advSelectorForm.ObjectType.Equals(-1))
              {
                if (!this._allowAnyObjectType)
                  break;
              }
              CommonTypeHolder commonTypeHolder = new CommonTypeHolder(advSelectorForm.ObjectType, advSelectorForm.AttributeTypes[0], session);
              if (!this.lbHorz.Items.Contains((object) commonTypeHolder))
              {
                int selectedIndex = this.lbHorz.SelectedIndex;
                this.lbHorz.Items.RemoveAt(selectedIndex);
                this.lbHorz.Items.Insert(selectedIndex, (object) commonTypeHolder);
                this.lbHorz.SelectedIndex = selectedIndex;
                break;
              }
              int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_37"), LocalizationHolder.rm.GetString("Expert.Editor_38"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              break;
            }
            break;
          }
        case "horz_Clear":
          this.lbHorz.Items.Clear();
          break;
        case "horz_Delete":
          if (this.lbResult.Items.Contains(this.lbHorz.SelectedItem))
            this.lbResult.Items.Remove(this.lbHorz.SelectedItem);
          this.lbHorz.Items.Remove(this.lbHorz.SelectedItem);
          break;
        case "horz_Down":
          int index1 = this.lbHorz.SelectedIndex + 1;
          object selectedItem2 = this.lbHorz.SelectedItem;
          this.lbHorz.Items.Remove(selectedItem2);
          this.lbHorz.Items.Insert(index1, selectedItem2);
          this.lbHorz.SelectedItem = selectedItem2;
          break;
        case "horz_EntryAdd":
          this.lbResult.Items.Add(this.lbHorz.SelectedItem);
          this.spinResult.Value = (Decimal) this.lbResult.Items.Count;
          break;
        case "horz_EntryRemove":
          this.lbResult.Items.Remove(this.lbHorz.SelectedItem);
          this.spinResult.Value = (Decimal) this.lbResult.Items.Count;
          break;
        case "horz_Up":
          int index2 = this.lbHorz.SelectedIndex - 1;
          object selectedItem3 = this.lbHorz.SelectedItem;
          this.lbHorz.Items.Remove(selectedItem3);
          this.lbHorz.Items.Insert(index2, selectedItem3);
          this.lbHorz.SelectedItem = selectedItem3;
          break;
      }
      if (!this.cbType.Text.Equals(EnumTypeHelper.GetCaption((Enum) eTableType.NoEntry)))
        return;
      this.spinHorz.Value = (Decimal) this.lbHorz.Items.Count;
    }
  }

  private void lbHorz_MouseUp(object sender, MouseEventArgs e)
  {
    if (!e.Button.Equals((object) MouseButtons.Right))
      return;
    this.lbHorz.SelectedIndex = this.lbHorz.IndexFromPoint(e.X, e.Y);
    this.lbHorzContext.Show((Control) this.lbHorz, new Point(e.X, e.Y));
  }

  private void lbHorz_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.Modifiers.Equals((object) Keys.Control) || this.lbHorz.SelectedItem == null)
      return;
    switch (e.KeyCode)
    {
      case Keys.Up:
      case Keys.U:
        if (this.lbHorz.SelectedIndex <= 0)
          break;
        this.lbHorzContext_Click((object) this.horz_Up, (EventArgs) null);
        break;
      case Keys.Down:
      case Keys.D:
        if (this.lbHorz.SelectedIndex >= this.lbHorz.Items.Count - 1)
          break;
        this.lbHorzContext_Click((object) this.horz_Down, (EventArgs) null);
        break;
    }
  }

  private void lbResultContext_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.ClearShortcut();
    bool flag = this.lbResult.SelectedItem != null;
    this.result_Add.Enabled = true;
    this.result_Change.Enabled = flag;
    this.result_Delete.Enabled = flag;
    this.result_Clear.Enabled = flag;
    this.result_Move.Enabled = flag;
    if (!flag)
      return;
    this.result_Up.Shortcut = Shortcut.CtrlU;
    this.result_Up.Enabled = this.lbResult.SelectedIndex > 0;
    this.result_Down.Shortcut = Shortcut.CtrlD;
    this.result_Down.Enabled = this.lbResult.SelectedIndex < this.lbResult.Items.Count - 1;
  }

  private void lbResultContext_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(sender is ButtonItemBase buttonItemBase))
        return;
      switch (buttonItemBase.CommandName)
      {
        case "result_Add":
          using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object))
          {
            advSelectorForm.AttributeTypesMultiselect = true;
            if (advSelectorForm.ShowDialog().Equals((object) DialogResult.OK))
            {
              if (advSelectorForm.ObjectType.Equals(-1))
              {
                if (!this._allowAnyObjectType)
                  break;
              }
              foreach (int attributeType in advSelectorForm.AttributeTypes)
              {
                CommonTypeHolder commonTypeHolder = new CommonTypeHolder(advSelectorForm.ObjectType, attributeType, session);
                if (!this.lbResult.Items.Contains((object) commonTypeHolder))
                {
                  this.lbResult.Items.Add((object) commonTypeHolder);
                }
                else
                {
                  int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_39"), LocalizationHolder.rm.GetString("Expert.Editor_40"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
              }
              break;
            }
            break;
          }
        case "result_Change":
          CommonTypeHolder selectedItem1 = this.lbResult.SelectedItem as CommonTypeHolder;
          int selectID = -1;
          if (!selectedItem1.ObjectType.Guid.Equals(Guid.Empty))
            selectID = sessionKeeper.Session.GetObjectType(selectedItem1.ObjectType.Guid).ObjectType;
          int attributeId = sessionKeeper.Session.GetAttributeType(selectedItem1.AttributeType.Guid).AttributeID;
          using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, selectID, new int[1]
          {
            attributeId
          }))
          {
            if (advSelectorForm.ShowDialog().Equals((object) DialogResult.OK))
            {
              if (advSelectorForm.ObjectType.Equals(-1))
              {
                if (!this._allowAnyObjectType)
                  break;
              }
              CommonTypeHolder commonTypeHolder = new CommonTypeHolder(advSelectorForm.ObjectType, advSelectorForm.AttributeTypes[0], session);
              if (!this.lbResult.Items.Contains((object) commonTypeHolder))
              {
                int selectedIndex = this.lbResult.SelectedIndex;
                this.lbResult.Items.RemoveAt(selectedIndex);
                this.lbResult.Items.Insert(selectedIndex, (object) commonTypeHolder);
                this.lbResult.SelectedIndex = selectedIndex;
                break;
              }
              int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_41"), LocalizationHolder.rm.GetString("Expert.Editor_42"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              break;
            }
            break;
          }
        case "result_Delete":
          this.lbResult.Items.Remove(this.lbResult.SelectedItem);
          break;
        case "result_Clear":
          this.lbResult.Items.Clear();
          break;
        case "result_Up":
          int index1 = this.lbResult.SelectedIndex - 1;
          object selectedItem2 = this.lbResult.SelectedItem;
          this.lbResult.Items.Remove(selectedItem2);
          this.lbResult.Items.Insert(index1, selectedItem2);
          this.lbResult.SelectedItem = selectedItem2;
          break;
        case "result_Down":
          int index2 = this.lbResult.SelectedIndex + 1;
          object selectedItem3 = this.lbResult.SelectedItem;
          this.lbResult.Items.Remove(selectedItem3);
          this.lbResult.Items.Insert(index2, selectedItem3);
          this.lbResult.SelectedItem = selectedItem3;
          break;
      }
      this.spinResult.Value = (Decimal) this.lbResult.Items.Count;
    }
  }

  private void lbResult_MouseUp(object sender, MouseEventArgs e)
  {
    if (!e.Button.Equals((object) MouseButtons.Right))
      return;
    this.lbResult.SelectedIndex = this.lbResult.IndexFromPoint(new Point(e.X, e.Y));
    this.lbResultContext.Show((Control) this.lbResult, new Point(e.X, e.Y));
  }

  private void lbResult_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.Modifiers.Equals((object) Keys.Control) || this.lbResult.SelectedItem == null)
      return;
    switch (e.KeyCode)
    {
      case Keys.Up:
      case Keys.U:
        if (this.lbResult.SelectedIndex <= 0)
          break;
        this.lbResultContext_Click((object) this.result_Up, (EventArgs) null);
        break;
      case Keys.Down:
      case Keys.D:
        if (this.lbResult.SelectedIndex >= this.lbResult.Items.Count - 1)
          break;
        this.lbResultContext_Click((object) this.result_Down, (EventArgs) null);
        break;
    }
  }

  private void cbType_SelectedIndexChanged(object sender, EventArgs e)
  {
    eTableType enumValue = (eTableType) EnumTypeHelper.GetEnumValue(typeof (eTableType), this.cbType.Text, (object) eTableType.SingleEntry);
    if (this._previousType.Equals((object) eTableType.NoEntry))
    {
      switch (enumValue)
      {
        case eTableType.SingleEntry:
        case eTableType.DoubleEntry:
          this.lbHorz.Items.Clear();
          this.spinHorz.Value = 0M;
          break;
      }
    }
    else if (this._previousType.Equals((object) eTableType.SingleEntry))
    {
      if (enumValue != eTableType.NoEntry)
      {
        if ((uint) (enumValue - 1) <= 1U)
          ;
      }
      else
      {
        this.lbVert.Items.Clear();
        this.spinVert.Value = 1M;
        object[] objArray = new object[this.lbResult.Items.Count];
        this.lbResult.Items.CopyTo(objArray, 0);
        this.lbHorz.Items.AddRange(objArray);
        this.spinHorz.Value = (Decimal) this.lbHorz.Items.Count;
      }
    }
    else if (this._previousType.Equals((object) eTableType.DoubleEntry))
    {
      switch (enumValue)
      {
        case eTableType.NoEntry:
          this.lbVert.Items.Clear();
          this.spinVert.Value = 0M;
          object[] objArray1 = new object[this.lbResult.Items.Count];
          this.lbResult.Items.CopyTo(objArray1, 0);
          this.lbHorz.Items.AddRange(objArray1);
          this.spinHorz.Value = (Decimal) this.lbHorz.Items.Count;
          break;
        case eTableType.SingleEntry:
          this.lbHorz.Items.Clear();
          this.spinHorz.Value = 0M;
          break;
      }
    }
    switch (enumValue)
    {
      case eTableType.NoEntry:
        this.gbResult.Enabled = this.lbVert.Enabled = this.spinHorz.Enabled = !(this.gbHorz.Enabled = true);
        this.lbResult.BackColor = this.lbVert.BackColor = SystemColors.Control;
        this.lbHorz.BackColor = SystemColors.Window;
        this.label7.Text = LocalizationHolder.rm.GetString("Expert.Editor_43");
        break;
      case eTableType.SingleEntry:
        this.gbResult.Enabled = this.lbVert.Enabled = !(this.gbHorz.Enabled = false);
        this.lbResult.BackColor = this.lbVert.BackColor = SystemColors.Window;
        this.lbHorz.BackColor = SystemColors.Control;
        this.label7.Text = LocalizationHolder.rm.GetString("Expert.Editor_44");
        break;
      case eTableType.DoubleEntry:
        this.gbResult.Enabled = this.lbVert.Enabled = this.spinHorz.Enabled = this.gbHorz.Enabled = true;
        this.lbResult.BackColor = this.lbVert.BackColor = this.lbHorz.BackColor = SystemColors.Window;
        this.label7.Text = LocalizationHolder.rm.GetString("Expert.Editor_45");
        break;
    }
    this._previousType = enumValue;
  }

  private void bOk_Click(object sender, EventArgs e)
  {
    if (!this.AllowEmptyTableName && this.tbName.Text == "")
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_589"), LocalizationHolder.rm.GetString("Expert.Editor_32"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      this.DialogResult = DialogResult.None;
    }
    else
    {
      switch ((eTableType) EnumTypeHelper.GetEnumValue(typeof (eTableType), this.cbType.Text, (object) eTableType.SingleEntry))
      {
        case eTableType.NoEntry:
          if (this.lbHorz.Items.Count.Equals(0))
          {
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_46"), LocalizationHolder.rm.GetString("Expert.Editor_47"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
          }
          eTable newTable1 = new eTable(eTableType.NoEntry);
          newTable1.Name = this.tbName.Text;
          eRow eRow1 = new eRow();
          ArrayList arrayList1 = new ArrayList();
          int num1 = Convert.ToInt32(this.spinVert.Value).Equals(0) ? 1 : Convert.ToInt32(this.spinVert.Value);
          newTable1.ValuesTable = new eValuesTable(num1, 0);
          foreach (CommonTypeHolder commonType in this.lbHorz.Items)
          {
            eCell cell = new eCell(eCellDestination.Header, commonType);
            eRow1.Add(cell);
            if (this.lbResult.Items.Contains((object) commonType))
            {
              cell.CellDestination = eCellDestination.Result;
              arrayList1.Add((object) commonType);
            }
            newTable1.ValuesTable.AddColumn(new eColumn(num1, eCellDestination.Data, commonType));
          }
          newTable1.Result = arrayList1.ToArray(typeof (CommonTypeHolder)) as CommonTypeHolder[];
          newTable1.FixedRows.Add(eRow1);
          this.ImportTable(newTable1);
          this._tables.Clear();
          this._tables.Add((object) newTable1);
          break;
        case eTableType.SingleEntry:
          if (this.lbVert.Items.Count.Equals(0))
          {
            int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_50"), LocalizationHolder.rm.GetString("Expert.Editor_51"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
          }
          if (this.lbResult.Items.Count.Equals(0))
          {
            int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_52"), LocalizationHolder.rm.GetString("Expert.Editor_53"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
          }
          eTable newTable2 = new eTable(eTableType.SingleEntry);
          newTable2.Name = this.tbName.Text;
          eRow eRow2 = new eRow();
          ArrayList arrayList2 = new ArrayList();
          int num4 = Convert.ToInt32(this.spinVert.Value).Equals(0) ? 1 : Convert.ToInt32(this.spinVert.Value);
          newTable2.ValuesTable = new eValuesTable(num4, 0);
          foreach (CommonTypeHolder commonType in this.lbResult.Items)
          {
            eCell cell = new eCell(eCellDestination.Result, commonType);
            eRow2.Add(cell);
            arrayList2.Add((object) commonType);
            newTable2.ValuesTable.AddColumn(new eColumn(num4, eCellDestination.Data, commonType));
          }
          newTable2.Result = arrayList2.ToArray(typeof (CommonTypeHolder)) as CommonTypeHolder[];
          newTable2.FixedRows.Add(eRow2);
          foreach (CommonTypeHolder commonType in this.lbVert.Items)
            newTable2.FixedColumns.Add(new eColumn(num4 + 1, eCellDestination.HeaderData, commonType)
            {
              Header = new eCell(eCellDestination.Header, commonType)
            });
          this.ImportTable(newTable2);
          this._tables.Clear();
          this._tables.Add((object) newTable2);
          break;
        case eTableType.DoubleEntry:
          if (this.lbVert.Items.Count.Equals(0))
          {
            int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_54"), LocalizationHolder.rm.GetString("Expert.Editor_55"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
          }
          if (this.lbVert.Items.Count.Equals(0))
          {
            int num6 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_56"), LocalizationHolder.rm.GetString("Expert.Editor_57"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
          }
          if (this.lbResult.Items.Count.Equals(0))
          {
            int num7 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_58"), LocalizationHolder.rm.GetString("Expert.Editor_59"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
          }
          int num8 = Convert.ToInt32(this.spinVert.Value).Equals(0) ? 1 : Convert.ToInt32(this.spinVert.Value);
          int num9 = Convert.ToInt32(this.spinHorz.Value).Equals(0) ? 1 : Convert.ToInt32(this.spinHorz.Value);
          ArrayList c = new ArrayList();
          foreach (CommonTypeHolder commonType1 in this.lbResult.Items)
          {
            eTable newTable3 = new eTable(eTableType.DoubleEntry);
            newTable3.Name = this.tbName.Text;
            newTable3.ValuesTable = new eValuesTable(num8, 0);
            newTable3.Result = new CommonTypeHolder[1]
            {
              commonType1
            };
            foreach (CommonTypeHolder commonType2 in this.lbVert.Items)
              newTable3.FixedColumns.Add(new eColumn(num8 + 1, eCellDestination.HeaderData, commonType2)
              {
                Header = new eCell(eCellDestination.Header, commonType2)
              });
            foreach (CommonTypeHolder commonTypeHolder in this.lbHorz.Items)
              newTable3.FixedRows.Add(new eRow(num9 + 1, eCellDestination.HeaderData, commonTypeHolder)
              {
                Header = new eCell(eCellDestination.Header, commonTypeHolder)
              });
            newTable3.FixedRows.Add(new eRow()
            {
              new eCell(eCellDestination.Result, eCellType.Text)
              {
                ColSpan = num9,
                CellValue = new ExpertValue($"{commonType1.ObjectType.Name} - {commonType1.AttributeType.Name}")
              }
            });
            for (int index = 0; index < num9; ++index)
              newTable3.ValuesTable.AddColumn(new eColumn(num8, eCellDestination.Data, commonType1));
            this.ImportTable(newTable3);
            c.Add((object) newTable3);
          }
          this._tables.Clear();
          this._tables.AddRange((ICollection) c);
          break;
      }
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>
  /// Импорт результатов таблицы. Только для безвходовых и одновходовых таблиц
  /// </summary>
  /// <param name="oldTable">Старая таблица (откуда импортируем)</param>
  /// <param name="newTable">Новая таблица (куда импортируем)</param>
  private void ImportResults1(eTable oldTable, eTable newTable)
  {
    eRow fixedRow = oldTable.FixedRows[0];
    for (int count = newTable.FixedColumns.Count; count < newTable.ColumnsCount; ++count)
    {
      eCell cell1 = newTable.GetCell(0, count);
      if (cell1 != null)
      {
        int num = -1;
        for (int index = 0; index < fixedRow.ColumnsCount; ++index)
        {
          eCell cell2 = fixedRow[index];
          if (cell1.EqualsStructure(cell2))
          {
            num = index + oldTable.FixedColumns.Count;
            newTable.SetColumnWidth(count, oldTable.GetColumnWidth(num));
            break;
          }
        }
        if (num >= 0)
        {
          for (int row = 0; row < newTable.RowsCount; ++row)
          {
            eCell cell3 = newTable.GetCell(row, count);
            eCell cell4 = oldTable.GetCell(row, num);
            if (cell3 != null && cell4 != null)
            {
              if (!cell3.CellDestination.Equals((object) cell4.CellDestination))
                cell4.CellDestination = cell3.CellDestination;
              newTable.SetCell(row, count, cell4);
            }
          }
        }
        else if (count < oldTable.ColumnsCount)
        {
          for (int row = 0; row < newTable.RowsCount; ++row)
          {
            eCell cell5 = newTable.GetCell(row, count);
            eCell cell6 = oldTable.GetCell(row, count);
            if (cell5 != null && cell6 != null)
              cell5.Assign(cell6);
          }
        }
      }
    }
  }

  /// <summary>Импортировать фиксированные столбцы</summary>
  /// <param name="oldTable">Старая таблица (откуда импортируем)</param>
  /// <param name="newTable">Новая таблица (куда импортируем)</param>
  private void ImportFixedCols(eTable oldTable, eTable newTable)
  {
    for (int index1 = 0; index1 < newTable.FixedColumns.Count; ++index1)
    {
      eColumn fixedColumn1 = newTable.FixedColumns[index1];
      if (fixedColumn1 != null)
      {
        eColumn eColumn = (eColumn) null;
        if (fixedColumn1.Header != null)
        {
          for (int index2 = 0; index2 < oldTable.FixedColumns.Count; ++index2)
          {
            eColumn fixedColumn2 = oldTable.FixedColumns[index2];
            if (fixedColumn2 != null && fixedColumn1.Header.EqualsStructure(fixedColumn2.Header))
            {
              eColumn = fixedColumn2;
              newTable.SetColumnWidth(index1, oldTable.GetColumnWidth(index2));
              break;
            }
          }
        }
        if (eColumn != null)
        {
          fixedColumn1.Header = eColumn.Header;
          for (int index3 = 0; index3 < fixedColumn1.RowsCount; ++index3)
          {
            eCell eCell = index3 < eColumn.RowsCount ? eColumn[index3] : (eCell) null;
            if (eCell != null)
              fixedColumn1[index3] = eCell;
          }
        }
        else if (index1 < oldTable.ColumnsCount)
        {
          for (int row = 0; row < fixedColumn1.RowsCount; ++row)
          {
            eCell cell1 = newTable.GetCell(row, index1);
            eCell cell2 = row < oldTable.RowsCount ? oldTable.GetCell(row, index1) : (eCell) null;
            if (cell1 != null && cell2 != null)
              cell1.Assign(cell2);
          }
        }
      }
    }
  }

  /// <summary>
  /// Импортировать фиксированные строки (только для двухвходовых таблиц)
  /// </summary>
  /// <param name="oldTable">Старая таблица (откуда импортируем)</param>
  /// <param name="newTable">Новая таблица (куда импортируем)</param>
  private void ImportFixedRows(eTable oldTable, eTable newTable)
  {
    for (int index1 = 0; index1 < newTable.FixedRows.Count; ++index1)
    {
      eRow fixedRow1 = newTable.FixedRows[index1];
      if (fixedRow1 != null && index1 != newTable.FixedRows.Count - 1)
      {
        eRow eRow = (eRow) null;
        if (fixedRow1.Header != null)
        {
          for (int index2 = 0; index2 < oldTable.FixedRows.Count; ++index2)
          {
            eRow fixedRow2 = oldTable.FixedRows[index2];
            if (fixedRow2 != null && fixedRow1.Header.EqualsStructure(fixedRow2.Header))
            {
              eRow = fixedRow2;
              break;
            }
          }
        }
        if (eRow != null)
        {
          fixedRow1.Header = eRow.Header;
          for (int index3 = 0; index3 < fixedRow1.ColumnsCount; ++index3)
          {
            eCell eCell = index3 < eRow.ColumnsCount ? eRow[index3] : (eCell) null;
            if (eCell != null)
              fixedRow1[index3] = eCell;
          }
        }
        else if (index1 < oldTable.RowsCount)
        {
          for (int column = 0; column < fixedRow1.ColumnsCount; ++column)
          {
            eCell cell1 = newTable.GetCell(index1, column);
            eCell cell2 = column < oldTable.ColumnsCount ? oldTable.GetCell(index1, column) : (eCell) null;
            if (cell2 != null && cell1 != null)
              cell1.Assign(cell2);
          }
        }
      }
    }
  }

  private void ImportTable(eTable newTable)
  {
    eTable table = this.FindTable((object[]) newTable.Result);
    if (table == null || !table.TableType.Equals((object) newTable.TableType))
      return;
    switch (newTable.TableType)
    {
      case eTableType.NoEntry:
        this.ImportResults1(table, newTable);
        break;
      case eTableType.SingleEntry:
        this.ImportResults1(table, newTable);
        this.ImportFixedCols(table, newTable);
        break;
      case eTableType.DoubleEntry:
        this.ImportFixedCols(table, newTable);
        this.ImportFixedRows(table, newTable);
        eValuesTable.AssignTo(table.ValuesTable.Array, newTable.ValuesTable.Array);
        break;
    }
  }

  private eTable FindTable(object[] results)
  {
    if (this._tables.Count.Equals(0))
      return (eTable) null;
    if (this._tables.Count.Equals(1))
    {
      eTable table = this._tables[0] as eTable;
      if (!table.TableType.Equals((object) eTableType.DoubleEntry))
        return table;
    }
    foreach (eTable table in this._tables)
    {
      ArrayList arrayList = new ArrayList((ICollection) table.Result);
      if (results.Length.Equals(arrayList.Count))
      {
        bool flag = true;
        foreach (CommonTypeHolder result in results)
          flag &= arrayList.Contains((object) result);
        if (flag)
          return table;
      }
    }
    return (eTable) null;
  }
}
