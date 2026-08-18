// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.CalcTestForm
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using SourceGrid3;
using SourceGrid3.Cells;
using SourceGrid3.Cells.Controllers;
using SourceGrid3.Cells.Views;
using SourceGrid3.Styles;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for CalcTest.</summary>
public class CalcTestForm : Form
{
  private TempFormula tf;
  internal int curCmd;
  internal IView selVisModel;
  internal IView defVisModel;
  internal ArrayList typeStack;
  internal ArrayList valueStack;
  internal bool OK = true;
  internal bool needClear;
  private GroupBox groupForm;
  private Splitter splitter1;
  private ImageList imageList1;
  private RichTextBox memoForm;
  private GroupBox groupLog;
  private TextBox textMsg;
  private Splitter splitter3;
  private GroupBox groupParm;
  private Grid gridParms;
  private Splitter splitter2;
  private GroupBox groupTest;
  private Grid gridStack;
  private Splitter splitter4;
  private Grid gridPostfix;
  private Panel panel1;
  private System.Windows.Forms.Button btnRefresh;
  private System.Windows.Forms.Button btnStep;
  private System.Windows.Forms.Button btnRun;
  private IContainer components;

  public CalcTestForm()
  {
    this.InitializeComponent();
    this.selVisModel = (IView) new SourceGrid3.Cells.Views.Cell();
    this.selVisModel.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
    this.defVisModel = (IView) new SourceGrid3.Cells.Views.Cell();
    this.defVisModel.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
    this.selVisModel.BackColor = Color.Red;
    this.selVisModel.ForeColor = Color.White;
    this.typeStack = new ArrayList();
    this.valueStack = new ArrayList();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public void Execute(ref TempFormula tf)
  {
    this.tf = tf;
    this.ShowFormula();
    this.CreateParmGrid();
    this.CreatePostfixGrid();
    this.InitStackGrid();
    this.curCmd = 0;
    this.RefreshCalc();
    this.MarkCurCmd(true);
    this.OK = true;
    this.needClear = false;
    int num = (int) this.ShowDialog();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CalcTestForm));
    this.groupForm = new GroupBox();
    this.memoForm = new RichTextBox();
    this.splitter1 = new Splitter();
    this.imageList1 = new ImageList(this.components);
    this.groupLog = new GroupBox();
    this.textMsg = new TextBox();
    this.splitter3 = new Splitter();
    this.groupParm = new GroupBox();
    this.gridParms = new Grid();
    this.splitter2 = new Splitter();
    this.groupTest = new GroupBox();
    this.gridStack = new Grid();
    this.splitter4 = new Splitter();
    this.gridPostfix = new Grid();
    this.panel1 = new Panel();
    this.btnRefresh = new System.Windows.Forms.Button();
    this.btnStep = new System.Windows.Forms.Button();
    this.btnRun = new System.Windows.Forms.Button();
    this.groupForm.SuspendLayout();
    this.groupLog.SuspendLayout();
    this.groupParm.SuspendLayout();
    this.groupTest.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.groupForm.AccessibleDescription = (string) null;
    this.groupForm.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.groupForm, "groupForm");
    this.groupForm.BackgroundImage = (System.Drawing.Image) null;
    this.groupForm.Controls.Add((Control) this.memoForm);
    this.groupForm.Font = (Font) null;
    this.groupForm.Name = "groupForm";
    this.groupForm.TabStop = false;
    this.memoForm.AccessibleDescription = (string) null;
    this.memoForm.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.memoForm, "memoForm");
    this.memoForm.BackgroundImage = (System.Drawing.Image) null;
    this.memoForm.Name = "memoForm";
    this.memoForm.ReadOnly = true;
    this.splitter1.AccessibleDescription = (string) null;
    this.splitter1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.BackgroundImage = (System.Drawing.Image) null;
    this.splitter1.BorderStyle = BorderStyle.Fixed3D;
    this.splitter1.Font = (Font) null;
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.White;
    this.imageList1.Images.SetKeyName(0, "");
    this.imageList1.Images.SetKeyName(1, "");
    this.imageList1.Images.SetKeyName(2, "");
    this.groupLog.AccessibleDescription = (string) null;
    this.groupLog.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.groupLog, "groupLog");
    this.groupLog.BackgroundImage = (System.Drawing.Image) null;
    this.groupLog.Controls.Add((Control) this.textMsg);
    this.groupLog.Font = (Font) null;
    this.groupLog.Name = "groupLog";
    this.groupLog.TabStop = false;
    this.textMsg.AccessibleDescription = (string) null;
    this.textMsg.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textMsg, "textMsg");
    this.textMsg.BackColor = Color.White;
    this.textMsg.BackgroundImage = (System.Drawing.Image) null;
    this.textMsg.Font = (Font) null;
    this.textMsg.Name = "textMsg";
    this.textMsg.ReadOnly = true;
    this.splitter3.AccessibleDescription = (string) null;
    this.splitter3.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.splitter3, "splitter3");
    this.splitter3.BackgroundImage = (System.Drawing.Image) null;
    this.splitter3.BorderStyle = BorderStyle.Fixed3D;
    this.splitter3.Font = (Font) null;
    this.splitter3.Name = "splitter3";
    this.splitter3.TabStop = false;
    this.groupParm.AccessibleDescription = (string) null;
    this.groupParm.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.groupParm, "groupParm");
    this.groupParm.BackgroundImage = (System.Drawing.Image) null;
    this.groupParm.Controls.Add((Control) this.gridParms);
    this.groupParm.Font = (Font) null;
    this.groupParm.Name = "groupParm";
    this.groupParm.TabStop = false;
    this.gridParms.AccessibleDescription = (string) null;
    this.gridParms.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.gridParms, "gridParms");
    this.gridParms.BackColor = Color.White;
    this.gridParms.BackgroundImage = (System.Drawing.Image) null;
    this.gridParms.BorderStyle = BorderStyle.FixedSingle;
    this.gridParms.Font = (Font) null;
    this.gridParms.GridToolTipActive = true;
    this.gridParms.Name = "gridParms";
    this.gridParms.SpecialKeys = GridSpecialKeys.Default;
    this.gridParms.StyleGrid = (StyleGrid) null;
    this.splitter2.AccessibleDescription = (string) null;
    this.splitter2.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.splitter2, "splitter2");
    this.splitter2.BackgroundImage = (System.Drawing.Image) null;
    this.splitter2.BorderStyle = BorderStyle.Fixed3D;
    this.splitter2.Font = (Font) null;
    this.splitter2.Name = "splitter2";
    this.splitter2.TabStop = false;
    this.groupTest.AccessibleDescription = (string) null;
    this.groupTest.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.groupTest, "groupTest");
    this.groupTest.BackgroundImage = (System.Drawing.Image) null;
    this.groupTest.Controls.Add((Control) this.gridStack);
    this.groupTest.Controls.Add((Control) this.splitter4);
    this.groupTest.Controls.Add((Control) this.gridPostfix);
    this.groupTest.Controls.Add((Control) this.panel1);
    this.groupTest.Font = (Font) null;
    this.groupTest.Name = "groupTest";
    this.groupTest.TabStop = false;
    this.gridStack.AccessibleDescription = (string) null;
    this.gridStack.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.gridStack, "gridStack");
    this.gridStack.BackColor = Color.White;
    this.gridStack.BackgroundImage = (System.Drawing.Image) null;
    this.gridStack.BorderStyle = BorderStyle.FixedSingle;
    this.gridStack.Font = (Font) null;
    this.gridStack.GridToolTipActive = true;
    this.gridStack.Name = "gridStack";
    this.gridStack.SpecialKeys = GridSpecialKeys.Default;
    this.gridStack.StyleGrid = (StyleGrid) null;
    this.gridStack.Resize += new EventHandler(this.gridStack_Resize);
    this.splitter4.AccessibleDescription = (string) null;
    this.splitter4.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.splitter4, "splitter4");
    this.splitter4.BackgroundImage = (System.Drawing.Image) null;
    this.splitter4.BorderStyle = BorderStyle.Fixed3D;
    this.splitter4.Font = (Font) null;
    this.splitter4.Name = "splitter4";
    this.splitter4.TabStop = false;
    this.gridPostfix.AccessibleDescription = (string) null;
    this.gridPostfix.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.gridPostfix, "gridPostfix");
    this.gridPostfix.BackColor = Color.White;
    this.gridPostfix.BackgroundImage = (System.Drawing.Image) null;
    this.gridPostfix.BorderStyle = BorderStyle.FixedSingle;
    this.gridPostfix.Font = (Font) null;
    this.gridPostfix.GridToolTipActive = true;
    this.gridPostfix.Name = "gridPostfix";
    this.gridPostfix.SpecialKeys = GridSpecialKeys.Default;
    this.gridPostfix.StyleGrid = (StyleGrid) null;
    this.gridPostfix.Resize += new EventHandler(this.gridPostfix_Resize);
    this.panel1.AccessibleDescription = (string) null;
    this.panel1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.BackgroundImage = (System.Drawing.Image) null;
    this.panel1.Controls.Add((Control) this.btnRefresh);
    this.panel1.Controls.Add((Control) this.btnStep);
    this.panel1.Controls.Add((Control) this.btnRun);
    this.panel1.Font = (Font) null;
    this.panel1.Name = "panel1";
    this.btnRefresh.AccessibleDescription = (string) null;
    this.btnRefresh.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnRefresh, "btnRefresh");
    this.btnRefresh.BackgroundImage = (System.Drawing.Image) null;
    this.btnRefresh.Font = (Font) null;
    this.btnRefresh.ImageList = this.imageList1;
    this.btnRefresh.Name = "btnRefresh";
    this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
    this.btnStep.AccessibleDescription = (string) null;
    this.btnStep.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnStep, "btnStep");
    this.btnStep.BackgroundImage = (System.Drawing.Image) null;
    this.btnStep.Font = (Font) null;
    this.btnStep.ImageList = this.imageList1;
    this.btnStep.Name = "btnStep";
    this.btnStep.Click += new EventHandler(this.btnStep_Click);
    this.btnRun.AccessibleDescription = (string) null;
    this.btnRun.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.btnRun, "btnRun");
    this.btnRun.BackgroundImage = (System.Drawing.Image) null;
    this.btnRun.Font = (Font) null;
    this.btnRun.ImageList = this.imageList1;
    this.btnRun.Name = "btnRun";
    this.btnRun.Click += new EventHandler(this.btnRun_Click);
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (System.Drawing.Image) null;
    this.Controls.Add((Control) this.groupTest);
    this.Controls.Add((Control) this.splitter2);
    this.Controls.Add((Control) this.groupParm);
    this.Controls.Add((Control) this.splitter3);
    this.Controls.Add((Control) this.groupLog);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.groupForm);
    this.Font = (Font) null;
    this.Icon = (Icon) null;
    this.Name = nameof (CalcTestForm);
    this.groupForm.ResumeLayout(false);
    this.groupLog.ResumeLayout(false);
    this.groupLog.PerformLayout();
    this.groupParm.ResumeLayout(false);
    this.groupTest.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void PaintCurToken(Token t)
  {
    if (t.type != Intermech.Expert.TokenType.FuncCall)
      this.memoForm.Select(t.StartPos, t.text.Length);
    switch (t.type)
    {
      case Intermech.Expert.TokenType.UnaryOper:
      case Intermech.Expert.TokenType.BinaryOper:
        this.memoForm.SelectionColor = Color.DarkRed;
        break;
      case Intermech.Expert.TokenType.OpeningBrace:
      case Intermech.Expert.TokenType.ClosingBrace:
        this.memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.FuncCall:
        this.memoForm.Select(t.StartPos, t.text.Length - 1);
        this.memoForm.SelectionColor = Color.Black;
        this.memoForm.Select(t.StartPos + t.text.Length - 1, 1);
        this.memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.Integer:
        this.memoForm.SelectionColor = Color.Indigo;
        break;
      case Intermech.Expert.TokenType.Float:
        this.memoForm.SelectionColor = Color.DarkOliveGreen;
        break;
      case Intermech.Expert.TokenType.String:
        this.memoForm.SelectionColor = Color.DarkMagenta;
        break;
      case Intermech.Expert.TokenType.Date:
        this.memoForm.SelectionColor = Color.DarkOrchid;
        break;
      case Intermech.Expert.TokenType.ObjectLink:
        this.memoForm.SelectionColor = Color.Red;
        break;
      default:
        this.memoForm.SelectionColor = Color.Black;
        break;
    }
  }

  public void ShowFormula()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.tf.Count; ++index)
      stringBuilder.Append(this.tf[index].text);
    this.memoForm.Text = stringBuilder.ToString();
    for (int index = 0; index < this.tf.Count; ++index)
      this.PaintCurToken(this.tf[index]);
  }

  private void CreateParmGrid()
  {
    this.gridParms.Redim(this.tf.usedAttrs.Count + 1, 2);
    this.gridParms.FixedColumns = 1;
    this.gridParms.FixedRows = 1;
    this.gridParms[0, 0] = (ICell) new SourceGrid3.Cells.Real.ColumnHeader((object) LocalizationHolder.rm.GetString("Expert.Editor_64"));
    ICell cell1 = (ICell) new SourceGrid3.Cells.Real.ColumnHeader((object) LocalizationHolder.rm.GetString("Expert.Editor_65"));
    cell1.AddController((IController) new Unselectable());
    this.gridParms[0, 1] = cell1;
    for (int index = 0; index < this.tf.pairNames.Count; ++index)
    {
      PairName pairName = this.tf.pairNames[index];
      DataType dataType;
      try
      {
        dataType = pairName.GetDataType();
      }
      catch (EInvalidAttrType ex)
      {
        this.gridParms[index + 1, 1] = (ICell) new SourceGrid3.Cells.Real.Header();
        continue;
      }
      switch (dataType)
      {
        case DataType.Integer:
        case DataType.ObjectLink:
          this.gridParms[index + 1, 1] = (ICell) new SourceGrid3.Cells.Real.Cell((object) 0, typeof (int));
          break;
        case DataType.Float:
          this.gridParms[index + 1, 1] = (ICell) new SourceGrid3.Cells.Real.Cell((object) 0.0, typeof (double));
          break;
        case DataType.String:
          this.gridParms[index + 1, 1] = (ICell) new SourceGrid3.Cells.Real.Cell((object) "", typeof (string));
          break;
        case DataType.Date:
          this.gridParms[index + 1, 1] = (ICell) new SourceGrid3.Cells.Real.Cell((object) DateTime.Today, typeof (DateTime));
          break;
        case DataType.Boolean:
          this.gridParms[index + 1, 1] = (ICell) new SourceGrid3.Cells.Real.CheckBox();
          break;
        case DataType.Packet:
          this.gridParms[index + 1, 1] = (ICell) new SourceGrid3.Cells.Real.Header();
          break;
      }
      SourceGrid3.Cells.Real.Cell cell2 = (SourceGrid3.Cells.Real.Cell) new SourceGrid3.Cells.Real.Header((object) pairName.ShortName);
      cell2.AddController((IController) Resizable.ResizeWidth);
      cell2.Controller.AddController((IController) new Unselectable());
      this.gridParms[index + 1, 0] = (ICell) cell2;
    }
    this.gridParms.Columns.AutoSize(false);
    this.gridParms.Selection.EnableMultiSelection = false;
    this.gridParms.Columns[0].Width = this.gridParms.Width - this.gridParms.Columns[1].Width - 2;
  }

  private void CreatePostfixGrid()
  {
    this.gridPostfix.Redim(this.tf.postfixForm.Count + 1, 1);
    this.gridPostfix.FixedRows = 1;
    ICell cell1 = (ICell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("Expert.Editor_66"));
    cell1.AddController((IController) new Unselectable());
    this.gridPostfix[0, 0] = cell1;
    for (int index = 0; index < this.tf.postfixForm.Count; ++index)
    {
      SourceGrid3.Cells.Real.Cell cell2 = new SourceGrid3.Cells.Real.Cell((object) this.tf.postfixForm[index].text);
      cell2.View = this.defVisModel;
      this.gridPostfix[index + 1, 0] = (ICell) cell2;
    }
    this.gridPostfix.Columns.AutoSize(false);
    this.gridPostfix.Selection.EnableMultiSelection = false;
    this.gridPostfix.Columns[0].Width = this.gridPostfix.Width - 2;
  }

  private void InitStackGrid()
  {
    this.gridStack.Redim(1, 2);
    this.gridStack.FixedRows = 1;
    SourceGrid3.Cells.Real.Cell cell1 = (SourceGrid3.Cells.Real.Cell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("Expert.Editor_67"));
    cell1.AddController((IController) new Unselectable());
    cell1.AddController((IController) Resizable.ResizeWidth);
    this.gridStack[0, 0] = (ICell) cell1;
    SourceGrid3.Cells.Real.Cell cell2 = (SourceGrid3.Cells.Real.Cell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("Expert.Editor_68"));
    cell2.AddController((IController) new Unselectable());
    this.gridStack[0, 1] = (ICell) cell2;
    this.gridStack.Columns.AutoSize(false);
    this.gridStack.Selection.EnableMultiSelection = false;
    this.gridStack.Columns[0].Width = this.gridStack.Width / 2 - 1;
    this.gridStack.Columns[1].Width = this.gridStack.Width - 2 - this.gridStack.Columns[0].Width;
    this.gridStack.Selection.SelectionMode = GridSelectionMode.Row;
    this.gridStack.Selection.FocusBackColor = this.gridStack.Selection.BackColor;
  }

  private void gridPostfix_Resize(object sender, EventArgs e)
  {
    this.gridPostfix.Columns[0].Width = this.gridPostfix.Width - 2;
  }

  private void gridStack_Resize(object sender, EventArgs e)
  {
    this.gridStack.Columns[0].Width = this.gridStack.Width / 2 - 1;
    this.gridStack.Columns[1].Width = this.gridStack.Width - 2 - this.gridStack.Columns[0].Width;
  }

  internal void MarkCurCmd(bool Mark)
  {
    if (this.curCmd + 1 >= this.gridPostfix.RowsCount)
      return;
    if (Mark)
      this.gridPostfix[this.curCmd + 1, 0].View = this.selVisModel;
    else
      this.gridPostfix[this.curCmd + 1, 0].View = this.defVisModel;
    this.gridPostfix[this.curCmd + 1, 0].Invalidate();
  }

  internal void FrameMsg(bool Start)
  {
    if (Start)
      this.textMsg.AppendText(LocalizationHolder.rm.GetString("Expert.Editor_69"));
    else
      this.textMsg.AppendText(LocalizationHolder.rm.GetString("Expert.Editor_70"));
  }

  internal void RefreshCalc()
  {
    this.textMsg.Clear();
    this.FrameMsg(true);
    if (this.curCmd != 0)
    {
      this.MarkCurCmd(false);
      this.curCmd = 0;
      this.MarkCurCmd(true);
    }
    this.gridStack.RowsCount = 1;
    this.typeStack.Clear();
    this.valueStack.Clear();
    this.OK = true;
  }

  internal bool PerformCmd(bool showProgress)
  {
    if (this.curCmd >= this.tf.postfixForm.Count)
      return false;
    Token t = this.tf.postfixForm[this.curCmd];
    int curCmd = this.curCmd;
    try
    {
      switch (t.type)
      {
        case Intermech.Expert.TokenType.UnaryOper:
          if (t.text == "-")
          {
            this.CheckStackType(t, true, DataType.Integer, DataType.Float);
            int topType = (int) this.topType;
            if (topType == 0)
              this.topValue = (object) -(int) this.topValue;
            if (topType == 1)
              this.topValue = (object) -(double) this.topValue;
          }
          if (t.text == LocalizationHolder.rm.GetString("Expert.Editor_71"))
          {
            this.CheckStackType(t, true, DataType.Boolean);
            this.topValue = (object) !(bool) this.topValue;
          }
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.BinaryOper:
          if (!this.PerformBinary(t))
            return false;
          break;
        case Intermech.Expert.TokenType.FuncCall:
          if (!this.PerformFunc(t))
            return false;
          break;
        case Intermech.Expert.TokenType.Integer:
          this.typeStack.Add((object) DataType.Integer);
          this.valueStack.Add((object) t.iValue);
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.Float:
          this.typeStack.Add((object) DataType.Float);
          this.valueStack.Add((object) t.fValue);
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.String:
          this.typeStack.Add((object) DataType.String);
          this.valueStack.Add((object) t.text.Substring(1, t.text.Length - 2));
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.Date:
          this.typeStack.Add((object) DataType.Date);
          this.valueStack.Add((object) new DateTime(t.iValue));
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.ObjectLink:
          this.typeStack.Add((object) DataType.ObjectLink);
          this.valueStack.Add((object) t.iValue);
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.Attribute:
          if (!this.PerformAttribute(t))
            return false;
          break;
        case Intermech.Expert.TokenType.Command:
          if (!this.PerformCommand(t))
            return false;
          break;
        case Intermech.Expert.TokenType.Measured:
          this.typeStack.Add((object) DataType.Measured);
          this.valueStack.Add((object) new MeasuredValue(t.fValue, t.iValue));
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.Boolean:
          this.typeStack.Add((object) DataType.Boolean);
          if (t.iValue == 0L)
            this.valueStack.Add((object) false);
          else
            this.valueStack.Add((object) true);
          ++this.curCmd;
          break;
        default:
          ++this.curCmd;
          break;
      }
      if (showProgress)
      {
        this.gridPostfix[curCmd + 1, 0].View = this.defVisModel;
        this.gridPostfix[curCmd + 1, 0].Invalidate();
        this.MarkCurCmd(true);
        this.ShowStack();
      }
      return true;
    }
    catch (CalcTestForm.EInvalidParm ex)
    {
      this.textMsg.AppendText("\r\n" + ex.Message);
      return false;
    }
  }

  internal DataType topType
  {
    get => (DataType) this.typeStack[this.typeStack.Count - 1];
    set => this.typeStack[this.typeStack.Count - 1] = (object) value;
  }

  internal object topValue
  {
    get => this.valueStack[this.valueStack.Count - 1];
    set => this.valueStack[this.valueStack.Count - 1] = value;
  }

  internal DataType firstType => (DataType) this.typeStack[this.typeStack.Count - 2];

  internal object firstValue => this.valueStack[this.valueStack.Count - 2];

  internal void Pop(ArrayList al)
  {
    if (al.Count <= 0)
      return;
    al.RemoveAt(al.Count - 1);
  }

  internal void CheckStackType(Token t, bool Top, params DataType[] types)
  {
    string str = string.Format(LocalizationHolder.rm.GetString("Expert.Editor_72"), (object) t.text.Trim());
    if (this.typeStack.Count <= 0)
      throw new CalcTestForm.EInvalidParm(str + LocalizationHolder.rm.GetString("Expert.Editor_73"));
    if (!Top && this.typeStack.Count < 2)
      throw new CalcTestForm.EInvalidParm(str + LocalizationHolder.rm.GetString("Expert.Editor_74"));
    DataType dt = !Top ? (DataType) this.typeStack[this.typeStack.Count - 2] : (DataType) this.typeStack[this.typeStack.Count - 1];
    bool flag = false;
    for (int index = 0; index < types.Length; ++index)
    {
      if (dt == types[index])
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new CalcTestForm.EInvalidParm($"{str}{LocalizationHolder.rm.GetString("Expert.Editor_75")}{DataTypeConvertor.DataTypeName(dt)})");
  }

  internal bool PerformAttribute(Token t)
  {
    PairName pairName = this.tf.pairNames[t.info];
    string str = string.Format(LocalizationHolder.rm.GetString("Expert.Editor_76"), (object) pairName.ShortName);
    DataType dataType;
    try
    {
      dataType = pairName.GetDataType();
    }
    catch (EInvalidAttrType ex)
    {
      this.textMsg.AppendText(str + LocalizationHolder.rm.GetString("Expert.Editor_77"));
      return false;
    }
    for (int row = 1; row < this.gridParms.RowsCount; ++row)
    {
      if ((string) this.gridParms[row, 0].Value == pairName.ShortName)
      {
        this.typeStack.Add((object) dataType);
        this.valueStack.Add(this.gridParms[row, 1].Value);
        ++this.curCmd;
        return true;
      }
    }
    return false;
  }

  internal bool PerformBinary(Token t)
  {
    DataType topType = this.topType;
    DataType firstType = this.firstType;
    object obj1 = (object) null;
    switch (t.text.Trim())
    {
      case "*":
      case "-":
      case "/":
      case "^":
        this.CheckStackType(t, false, DataType.Float, DataType.Integer);
        this.CheckStackType(t, true, DataType.Float, DataType.Integer);
        DataType dataType1;
        if (topType == DataType.Measured || firstType == DataType.Measured)
        {
          MeasuredValue operand1 = topType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), 0L);
          MeasuredValue operand2 = firstType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), 0L);
          switch (t.text)
          {
            case "-":
              obj1 = (object) MeasureHelper.Substract(operand1, operand2);
              break;
            case "*":
              obj1 = (object) MeasureHelper.Multiply(operand1, operand2);
              break;
            case "^":
              throw new Exception(LocalizationHolder.rm.GetString("Expert.Editor_78"));
            case "/":
              obj1 = (object) MeasureHelper.Divide(operand1, operand2);
              break;
          }
          dataType1 = DataType.Measured;
        }
        else if (topType == DataType.Float || firstType == DataType.Float)
        {
          double d = Convert.ToDouble(this.firstValue);
          double num = Convert.ToDouble(this.topValue);
          switch (t.text)
          {
            case "-":
              obj1 = (object) (d - num);
              break;
            case "*":
              obj1 = (object) (d * num);
              break;
            case "^":
              obj1 = (object) Math.Exp(num * Math.Log(d));
              break;
            case "/":
              obj1 = (object) (d / num);
              break;
          }
          dataType1 = DataType.Float;
        }
        else
        {
          long int64_1 = Convert.ToInt64(this.firstValue);
          long int64_2 = Convert.ToInt64(this.topValue);
          switch (t.text)
          {
            case "-":
              obj1 = (object) (int64_1 - int64_2);
              break;
            case "*":
              obj1 = (object) (int64_1 * int64_2);
              break;
            case "^":
              obj1 = (object) Math.Round(Math.Pow((double) int64_1, (double) int64_2));
              break;
            case "/":
              obj1 = (object) (int64_1 / int64_2);
              break;
          }
          dataType1 = DataType.Integer;
        }
        this.Pop(this.typeStack);
        this.Pop(this.typeStack);
        this.Pop(this.valueStack);
        this.Pop(this.valueStack);
        this.typeStack.Add((object) dataType1);
        this.valueStack.Add(obj1);
        ++this.curCmd;
        return true;
      case "+":
        this.CheckStackType(t, false, DataType.Float, DataType.Integer, DataType.String, DataType.Measured);
        this.CheckStackType(t, true, DataType.Float, DataType.Integer, DataType.String, DataType.Measured);
        object obj2;
        DataType dataType2;
        if (topType == DataType.String || firstType == DataType.String)
        {
          obj2 = (object) (this.firstValue.ToString() + this.topValue.ToString());
          dataType2 = DataType.String;
        }
        else if (topType == DataType.Measured || firstType == DataType.Measured)
        {
          obj2 = (object) MeasureHelper.Add(topType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), 0L), firstType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), 0L));
          dataType2 = DataType.Measured;
        }
        else if (topType == DataType.Float || firstType == DataType.Float)
        {
          obj2 = (object) (Convert.ToDouble(this.firstValue) + Convert.ToDouble(this.topValue));
          dataType2 = DataType.Float;
        }
        else
        {
          obj2 = (object) (Convert.ToInt32(this.firstValue) + Convert.ToInt32(this.topValue));
          dataType2 = DataType.Integer;
        }
        this.Pop(this.typeStack);
        this.Pop(this.typeStack);
        this.Pop(this.valueStack);
        this.Pop(this.valueStack);
        this.typeStack.Add((object) dataType2);
        this.valueStack.Add(obj2);
        ++this.curCmd;
        return true;
      case ":":
        DiapValue diapValue = new DiapValue();
        diapValue.Low = new ExpertValue(topType, this.firstValue);
        diapValue.High = new ExpertValue(topType, this.topValue);
        this.Pop(this.typeStack);
        this.Pop(this.typeStack);
        this.Pop(this.valueStack);
        this.Pop(this.valueStack);
        this.typeStack.Add((object) DataType.Diap);
        this.valueStack.Add((object) diapValue);
        ++this.curCmd;
        return true;
      case "<":
      case "<=":
      case "<>":
      case "=":
      case ">":
      case ">=":
        if (topType == firstType && (topType == DataType.ObjectLink || topType == DataType.Packet) && (t.text == "=" || t.text == "<>"))
        {
          this.textMsg.AppendText(LocalizationHolder.rm.GetString("Expert.Editor_81"));
          return false;
        }
        this.CheckStackType(t, false, DataType.Float, DataType.Integer, DataType.String);
        this.CheckStackType(t, true, DataType.Float, DataType.Integer, DataType.String);
        if (topType == DataType.String || firstType == DataType.String)
        {
          int num1 = string.Compare(Convert.ToString(this.firstValue), Convert.ToString(this.topValue));
          switch (t.text.Trim())
          {
            case "<":
              obj1 = (object) (num1 < 0);
              break;
            case "<=":
              obj1 = (object) (num1 <= 0);
              break;
            case ">":
              obj1 = (object) (num1 > 0);
              break;
            case ">=":
              obj1 = (object) (num1 >= 0);
              break;
            case "=":
              int num2;
              obj1 = (object) (num2 = 0);
              break;
            case "<>":
              obj1 = (object) (num1 != 0);
              break;
          }
        }
        else if (topType == DataType.Measured || firstType == DataType.Measured)
        {
          CompareResult compareResult = MeasureHelper.Compare(topType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), 0L), firstType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), 0L));
          switch (t.text.Trim())
          {
            case "<":
              obj1 = (object) (compareResult == CompareResult.Less);
              break;
            case "<=":
              obj1 = (object) (bool) (compareResult == CompareResult.Less ? 1 : (compareResult == CompareResult.Equal ? 1 : 0));
              break;
            case ">":
              obj1 = (object) (compareResult == CompareResult.More);
              break;
            case ">=":
              obj1 = (object) (bool) (compareResult == CompareResult.More ? 1 : (compareResult == CompareResult.Equal ? 1 : 0));
              break;
            case "=":
              obj1 = (object) (compareResult == CompareResult.Equal);
              break;
            case "<>":
              obj1 = (object) (compareResult != 0);
              break;
          }
        }
        else if (topType == DataType.Float || firstType == DataType.Float)
        {
          double num = Convert.ToDouble(this.firstValue) - Convert.ToDouble(this.topValue);
          switch (t.text.Trim())
          {
            case "<":
            case "<=":
              obj1 = (object) (num < 1E-20);
              break;
            case ">":
            case ">=":
              obj1 = (object) (num > 1E-20);
              break;
            case "=":
              obj1 = (object) (Math.Abs(num) < 1E-20);
              break;
            case "<>":
              obj1 = (object) (Math.Abs(num) > 1E-20);
              break;
          }
        }
        else
        {
          int int32_1 = Convert.ToInt32(this.firstValue);
          int int32_2 = Convert.ToInt32(this.topValue);
          switch (t.text.Trim())
          {
            case "<":
              obj1 = (object) (int32_1 < int32_2);
              break;
            case "<=":
              obj1 = (object) (int32_1 <= int32_2);
              break;
            case ">":
              obj1 = (object) (int32_1 > int32_2);
              break;
            case ">=":
              obj1 = (object) (int32_1 >= int32_2);
              break;
            case "=":
              obj1 = (object) (int32_1 == int32_2);
              break;
            case "<>":
              obj1 = (object) (int32_1 != int32_2);
              break;
          }
        }
        this.Pop(this.typeStack);
        this.Pop(this.typeStack);
        this.Pop(this.valueStack);
        this.Pop(this.valueStack);
        this.typeStack.Add((object) DataType.Boolean);
        this.valueStack.Add(obj1);
        ++this.curCmd;
        return true;
      case "?":
        object obj3 = (object) this.IsInPacket(this.firstValue, firstType, (PacketValue) this.topValue);
        this.Pop(this.typeStack);
        this.Pop(this.typeStack);
        this.Pop(this.valueStack);
        this.Pop(this.valueStack);
        this.typeStack.Add((object) DataType.Boolean);
        this.valueStack.Add(obj3);
        ++this.curCmd;
        return true;
      default:
        return true;
    }
  }

  internal bool IsInPacket(object val, DataType valType, PacketValue pv)
  {
    for (int index = 0; index < pv.Count; ++index)
    {
      switch (pv[index].ValueType)
      {
        case DataType.Integer:
          if ((valType == DataType.Integer || valType == DataType.String) && Convert.ToInt64(val) == Convert.ToInt64(pv[index].Value))
            return true;
          break;
        case DataType.Float:
          if ((valType == DataType.Float || valType == DataType.String || valType == DataType.Integer) && Math.Abs(Convert.ToDouble(val) - Convert.ToDouble(pv[index].Value)) < ExpertConsts.Epsilon)
            return true;
          break;
        case DataType.Measured:
          if (valType == DataType.Measured && MeasureHelper.Compare((MeasuredValue) pv[index].Value, (MeasuredValue) val) == CompareResult.Equal || valType == DataType.String && Convert.ToString((object) (MeasuredValue) pv[index].Value) == Convert.ToString(val))
            return true;
          break;
        case DataType.String:
          if ((valType == DataType.Integer || valType == DataType.String) && Convert.ToInt64(val) == Convert.ToInt64(pv[index].Value))
            return true;
          break;
        case DataType.Diap:
          DiapValue diapValue = (DiapValue) pv[index].Value;
          switch (diapValue.Low.ValueType)
          {
            case DataType.Integer:
              long int64 = Convert.ToInt64(val);
              if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && int64 >= Convert.ToInt64(diapValue.Low.Value) && int64 <= Convert.ToInt64(diapValue.High.Value))
                return true;
              continue;
            case DataType.Float:
              double num = Convert.ToDouble(val);
              if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && num >= Convert.ToDouble(diapValue.Low.Value) - ExpertConsts.Epsilon && num <= Convert.ToDouble(diapValue.High.Value) + ExpertConsts.Epsilon)
                return true;
              continue;
            case DataType.Measured:
              if (valType == DataType.Measured)
              {
                MeasuredValue val1_1 = (MeasuredValue) val;
                MeasuredValue val1_2 = (MeasuredValue) diapValue.Low.Value;
                MeasuredValue val2_1 = (MeasuredValue) diapValue.High.Value;
                MeasuredValue val2_2 = val1_1;
                CompareResult compareResult1 = MeasureHelper.Compare(val1_2, val2_2);
                CompareResult compareResult2 = MeasureHelper.Compare(val1_1, val2_1);
                if ((compareResult1 == CompareResult.Equal || compareResult1 == CompareResult.Less) && (compareResult2 == CompareResult.Equal || compareResult2 == CompareResult.Less))
                  return true;
                continue;
              }
              continue;
            case DataType.String:
              string strA = Convert.ToString(val);
              if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && string.Compare(strA, Convert.ToString(diapValue.Low.Value)) >= 0 && string.Compare(strA, Convert.ToString(diapValue.High.Value)) <= 0)
                return true;
              continue;
            default:
              continue;
          }
      }
    }
    return false;
  }

  internal bool PerformCommand(Token t)
  {
    switch (t.info)
    {
      case 0:
        this.CheckStackType(t, true, DataType.Boolean);
        if (Convert.ToBoolean(this.topValue))
        {
          this.curCmd = (int) t.iValue;
          break;
        }
        ++this.curCmd;
        this.Pop(this.typeStack);
        this.Pop(this.valueStack);
        break;
      case 1:
        this.CheckStackType(t, true, DataType.Boolean);
        if (!Convert.ToBoolean(this.topValue))
        {
          this.curCmd = (int) t.iValue;
          break;
        }
        ++this.curCmd;
        this.Pop(this.typeStack);
        this.Pop(this.valueStack);
        break;
      case 2:
        PacketValue packetValue = new PacketValue();
        for (int index = 0; (long) index < t.iValue; ++index)
          packetValue.Add(new ExpertValue((DataType) this.typeStack[this.typeStack.Count - index - 1], this.valueStack[this.valueStack.Count - index - 1]));
        for (int index = 0; (long) index < t.iValue; ++index)
        {
          this.Pop(this.typeStack);
          this.Pop(this.valueStack);
        }
        this.typeStack.Add((object) DataType.Packet);
        this.valueStack.Add((object) packetValue);
        ++this.curCmd;
        break;
    }
    return true;
  }

  internal bool PerformFunc(Token t)
  {
    FuncData fd = ExpertFunc.funcs(t.info);
    int length = fd.parmTypes.Length;
    if (this.typeStack.Count < length)
    {
      this.textMsg.AppendText(string.Format(LocalizationHolder.rm.GetString("Expert.Editor_84"), (object) fd.text, (object) length.ToString()));
      return false;
    }
    ArrayList parms = new ArrayList(length);
    for (int index = 0; index < fd.parmTypes.Length; ++index)
    {
      object obj = this.valueStack[this.valueStack.Count - length + index];
      try
      {
        switch (fd.parmTypes[index])
        {
          case DataType.Integer:
          case DataType.ObjectLink:
            parms.Add((object) Convert.ToInt32(obj));
            continue;
          case DataType.Float:
            parms.Add((object) Convert.ToDouble(obj));
            continue;
          case DataType.String:
            parms.Add((object) Convert.ToString(obj));
            continue;
          case DataType.Date:
            parms.Add((object) Convert.ToDateTime(obj));
            continue;
          case DataType.Boolean:
            parms.Add((object) Convert.ToBoolean(obj));
            continue;
          case DataType.Packet:
            parms.Add((object) (PacketValue) obj);
            continue;
          default:
            continue;
        }
      }
      catch (Exception ex)
      {
        this.textMsg.AppendText(LocalizationHolder.rm.GetString("Expert.Editor_85") + ex.Message);
        return false;
      }
    }
    try
    {
      if (!this.CallFunction(fd, parms))
        return false;
      ++this.curCmd;
      return true;
    }
    catch (Exception ex)
    {
      this.textMsg.AppendText($"{LocalizationHolder.rm.GetString("Expert.Editor_86")}{fd.text}: {ex.Message}");
      return false;
    }
  }

  internal bool CallFunction(FuncData fd, ArrayList parms)
  {
    int length = fd.parmTypes.Length;
    object obj = (object) null;
    DataType dataType = fd.result;
    if (fd.func > (FormulaFunc) 1000)
    {
      obj = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IExpertServer)) as IExpertServer).InvokeFunc((int) fd.func, parms);
    }
    else
    {
      string text = string.Format(LocalizationHolder.rm.GetString("Expert.Editor_87"), (object) fd.text);
      switch (fd.func)
      {
        case FormulaFunc.sin:
          obj = (object) Math.Sin((double) parms[0]);
          break;
        case FormulaFunc.cos:
          obj = (object) Math.Cos((double) parms[0]);
          break;
        case FormulaFunc.tg:
          obj = (object) Math.Tan((double) parms[0]);
          break;
        case FormulaFunc.ln:
          obj = (object) Math.Log((double) parms[0]);
          break;
        case FormulaFunc.lg:
          obj = (object) Math.Log10((double) parms[0]);
          break;
        case FormulaFunc.atg:
          obj = (object) Math.Atan((double) parms[0]);
          break;
        case FormulaFunc.exp:
          obj = (object) Math.Exp((double) parms[0]);
          break;
        case FormulaFunc.sqrt:
          obj = (object) Math.Sqrt((double) parms[0]);
          break;
        case FormulaFunc.abs:
          if (this.topType == DataType.Integer)
          {
            dataType = DataType.Integer;
            obj = (object) Math.Abs(Convert.ToInt32(this.topValue));
            break;
          }
          obj = (object) Math.Abs((double) parms[0]);
          break;
        case FormulaFunc.def:
          this.textMsg.AppendText(text);
          return false;
        case FormulaFunc.nom:
          this.textMsg.AppendText(text);
          return false;
        case FormulaFunc.kv:
          this.textMsg.AppendText(text);
          return false;
        case FormulaFunc.hi:
          this.textMsg.AppendText(text);
          return false;
        case FormulaFunc.lo:
          this.textMsg.AppendText(text);
          return false;
        case FormulaFunc.kt:
          this.textMsg.AppendText(text);
          return false;
        case FormulaFunc.st:
          this.textMsg.AppendText(text);
          return false;
        case FormulaFunc.ctn:
          this.textMsg.AppendText(text);
          return false;
        case FormulaFunc.rnd:
          obj = (object) Math.Round((double) parms[0]);
          break;
        case FormulaFunc.rnde:
          double parm1 = (double) parms[0];
          int parm2 = (int) parms[1];
          if (parm2 >= 0)
          {
            obj = (object) Math.Round(parm1, parm2, MidpointRounding.AwayFromZero);
            break;
          }
          int num1 = 1;
          for (; parm2 < 0; ++parm2)
            num1 *= 10;
          obj = (object) (Math.Round(parm1 / (double) num1, 0) * (double) num1);
          break;
        case FormulaFunc.rndg:
          double parm3 = (double) parms[0];
          string str = Convert.ToString(Math.Abs(Convert.ToInt64(Math.Truncate(parm3))));
          int parm4 = (int) parms[1];
          if (parm4 > str.Length)
          {
            obj = (object) Math.Round(parm3, parm4 - str.Length, MidpointRounding.AwayFromZero);
            break;
          }
          double num2 = 1.0;
          for (; str.Length > parm4; ++parm4)
            num2 *= 10.0;
          obj = (object) (Math.Round(parm3 / num2, 0) * num2);
          break;
        case FormulaFunc.Int:
          obj = (object) Convert.ToInt32(Math.Floor((double) parms[0]));
          if ((int) obj < 0)
          {
            obj = (object) ((int) obj + 1);
            break;
          }
          break;
        case FormulaFunc.frac:
          obj = (object) ((double) parms[0] - Math.Floor((double) parms[0]));
          break;
        case FormulaFunc.has:
          obj = (object) (((string) parms[0]).IndexOf((string) parms[1]) >= 0);
          break;
        case FormulaFunc.begs:
          obj = (object) ((string) parms[0]).StartsWith((string) parms[1]);
          break;
        case FormulaFunc.ends:
          obj = (object) ((string) parms[0]).EndsWith((string) parms[1]);
          break;
        case FormulaFunc.upp:
          obj = (object) ((string) parms[0]).ToUpper();
          break;
        case FormulaFunc.low:
          obj = (object) ((string) parms[0]).ToLower();
          break;
        case FormulaFunc.now:
          obj = (object) DateTime.Now;
          break;
        case FormulaFunc.flag:
          obj = (object) (((ulong) (1 << Convert.ToInt32(parms[0]) - 1) & (ulong) Convert.ToInt64(parms[1])) > 0UL);
          break;
        case FormulaFunc.num:
          obj = (object) ((MeasuredValue) parms[0]).Value;
          break;
      }
    }
    for (int index = 0; index < length; ++index)
    {
      this.Pop(this.typeStack);
      this.Pop(this.valueStack);
    }
    this.typeStack.Add((object) dataType);
    this.valueStack.Add(obj);
    return true;
  }

  internal void ShowStack()
  {
    this.gridStack.RowsCount = this.typeStack.Count + 1;
    for (int index = 0; index < this.typeStack.Count; ++index)
    {
      SourceGrid3.Cells.Real.Cell grid1 = (SourceGrid3.Cells.Real.Cell) this.gridStack[index + 1, 0];
      SourceGrid3.Cells.Real.Cell grid2 = (SourceGrid3.Cells.Real.Cell) this.gridStack[index + 1, 1];
      string cellValue1 = this.valueStack[index].ToString();
      DataType type = (DataType) this.typeStack[index];
      switch (type)
      {
        case DataType.String:
          cellValue1 = $"\"{cellValue1}\"";
          break;
        case DataType.Boolean:
          cellValue1 = Convert.ToBoolean(this.valueStack[index]) ? LocalizationHolder.rm.GetString("Expert.Editor_88") : LocalizationHolder.rm.GetString("Expert.Editor_89");
          break;
      }
      string cellValue2 = DataTypeConvertor.DataTypeName(type);
      if (grid1 == null || grid2 == null)
      {
        SourceGrid3.Cells.Real.Cell cell1 = new SourceGrid3.Cells.Real.Cell((object) cellValue1);
        SourceGrid3.Cells.Real.Cell cell2 = new SourceGrid3.Cells.Real.Cell((object) cellValue2);
        this.gridStack[index + 1, 0] = (ICell) cell1;
        this.gridStack[index + 1, 1] = (ICell) cell2;
      }
      else
      {
        grid1.Value = (object) cellValue1;
        grid2.Value = (object) cellValue2;
      }
    }
  }

  private void btnRefresh_Click(object sender, EventArgs e)
  {
    this.RefreshCalc();
    this.OK = true;
  }

  private void btnRun_Click(object sender, EventArgs e)
  {
    if (this.needClear)
    {
      this.RefreshCalc();
      this.OK = true;
      this.needClear = false;
    }
    while (this.curCmd < this.tf.postfixForm.Count)
    {
      this.OK = this.PerformCmd(false);
      if (!this.OK)
        break;
    }
    if (this.OK)
      this.ReportResult();
    this.needClear = true;
  }

  private void btnStep_Click(object sender, EventArgs e)
  {
    if (this.needClear)
    {
      this.RefreshCalc();
      this.OK = true;
      this.needClear = false;
    }
    else
    {
      if (!this.OK)
        return;
      if (this.curCmd < this.tf.postfixForm.Count)
        this.OK = this.PerformCmd(true);
      if (this.OK && this.curCmd >= this.tf.postfixForm.Count)
      {
        this.ReportResult();
        this.needClear = true;
      }
      if (this.OK)
        return;
      this.needClear = true;
    }
  }

  private bool CheckType(DataType dt, params DataType[] types)
  {
    bool flag = false;
    for (int index = 0; index < types.Length; ++index)
    {
      if (dt == types[index])
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      this.textMsg.AppendText($"{LocalizationHolder.rm.GetString("Expert.Editor_90")}{DataTypeConvertor.DataTypeName(dt)})");
    return flag;
  }

  private void ReportResult()
  {
    this.FrameMsg(false);
    if (this.typeStack.Count < 1)
      this.textMsg.AppendText(LocalizationHolder.rm.GetString("Expert.Editor_91"));
    else if (this.typeStack.Count > 1)
    {
      this.textMsg.AppendText(LocalizationHolder.rm.GetString("Expert.Editor_92"));
    }
    else
    {
      DataType topType = this.topType;
      string text = LocalizationHolder.rm.GetString("Expert.Editor_93");
      switch (this.tf.resType)
      {
        case DataType.Integer:
          if (!this.CheckType(topType, DataType.Integer, DataType.String))
            return;
          text += this.topValue.ToString();
          break;
        case DataType.Float:
          if (!this.CheckType(topType, DataType.Integer, DataType.Float, DataType.String))
            return;
          text += this.topValue.ToString();
          break;
        case DataType.String:
          if (!this.CheckType(topType, DataType.Date, DataType.Float, DataType.Integer, DataType.String))
            return;
          text += this.topValue.ToString();
          break;
        case DataType.Date:
          if (!this.CheckType(topType, DataType.Date, DataType.String))
            return;
          text += this.topValue.ToString();
          break;
        case DataType.Boolean:
          if (!this.CheckType(topType, DataType.Boolean))
            return;
          text += Convert.ToBoolean(this.topValue) ? LocalizationHolder.rm.GetString("Expert.Editor_94") : LocalizationHolder.rm.GetString("Expert.Editor_95");
          break;
        case DataType.ObjectLink:
          if (!this.CheckType(topType, DataType.ObjectLink))
            return;
          text += this.topValue.ToString();
          break;
        case DataType.Packet:
          if (!this.CheckType(topType, DataType.Packet))
            return;
          text += this.topValue.ToString();
          break;
      }
      this.textMsg.AppendText(text);
    }
  }

  public class EInvalidParm(string Message) : Exception(Message)
  {
  }
}
