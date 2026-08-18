// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_cell_border
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_cell_border : Form
{
  private RadioButton AllCells;
  private TextBox BotWidth;
  private Button Cancel;
  private TextBox CellMargin;
  private RadioButton Cols;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private int CurCell;
  private ImRtfEditor e;
  private GroupBox group;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Label label1;
  private Label label2;
  private Label label3;
  private Label label4;
  private Label label5;
  private TextBox LeftWidth;
  private Button OK;
  private CheckBox Outline;
  private TextBox RightWidth;
  private RadioButton Rows;
  private RadioButton SelCells;
  private TextBox TopWidth;

  internal terdlg_cell_border(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.OK = new Button();
    this.Cancel = new Button();
    this.groupBox1 = new GroupBox();
    this.group = new GroupBox();
    this.RightWidth = new TextBox();
    this.label5 = new Label();
    this.LeftWidth = new TextBox();
    this.label4 = new Label();
    this.BotWidth = new TextBox();
    this.label3 = new Label();
    this.TopWidth = new TextBox();
    this.label1 = new Label();
    this.groupBox2 = new GroupBox();
    this.Rows = new RadioButton();
    this.Cols = new RadioButton();
    this.SelCells = new RadioButton();
    this.AllCells = new RadioButton();
    this.CellMargin = new TextBox();
    this.label2 = new Label();
    this.Outline = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.group.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(136, 208 /*0xD0*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(224 /*0xE0*/, 208 /*0xD0*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.Add((Control) this.Outline);
    this.groupBox1.Controls.Add((Control) this.group);
    this.groupBox1.Controls.Add((Control) this.groupBox2);
    this.groupBox1.Controls.Add((Control) this.CellMargin);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(296, 200);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.group.Controls.Add((Control) this.RightWidth);
    this.group.Controls.Add((Control) this.label5);
    this.group.Controls.Add((Control) this.LeftWidth);
    this.group.Controls.Add((Control) this.label4);
    this.group.Controls.Add((Control) this.BotWidth);
    this.group.Controls.Add((Control) this.label3);
    this.group.Controls.Add((Control) this.TopWidth);
    this.group.Controls.Add((Control) this.label1);
    this.group.Location = new Point(128 /*0x80*/, 16 /*0x10*/);
    this.group.Name = "group";
    this.group.Size = new Size(160 /*0xA0*/, 120);
    this.group.TabIndex = 1;
    this.group.TabStop = false;
    this.group.Text = "Border Width (Twips)";
    this.RightWidth.Location = new Point(72, 88);
    this.RightWidth.Name = "RightWidth";
    this.RightWidth.Size = new Size(40, 20);
    this.RightWidth.TabIndex = 8;
    this.RightWidth.Text = "";
    this.label5.Location = new Point(8, 88);
    this.label5.Name = "label5";
    this.label5.Size = new Size(48 /*0x30*/, 16 /*0x10*/);
    this.label5.TabIndex = 7;
    this.label5.Text = "Right";
    this.LeftWidth.Location = new Point(72, 64 /*0x40*/);
    this.LeftWidth.Name = "LeftWidth";
    this.LeftWidth.Size = new Size(40, 20);
    this.LeftWidth.TabIndex = 6;
    this.LeftWidth.Text = "";
    this.label4.Location = new Point(8, 64 /*0x40*/);
    this.label4.Name = "label4";
    this.label4.Size = new Size(48 /*0x30*/, 16 /*0x10*/);
    this.label4.TabIndex = 5;
    this.label4.Text = "Left";
    this.BotWidth.Location = new Point(72, 40);
    this.BotWidth.Name = "BotWidth";
    this.BotWidth.Size = new Size(40, 20);
    this.BotWidth.TabIndex = 4;
    this.BotWidth.Text = "";
    this.label3.Location = new Point(8, 40);
    this.label3.Name = "label3";
    this.label3.Size = new Size(48 /*0x30*/, 16 /*0x10*/);
    this.label3.TabIndex = 3;
    this.label3.Text = "Bottom";
    this.TopWidth.Location = new Point(72, 16 /*0x10*/);
    this.TopWidth.Name = "TopWidth";
    this.TopWidth.Size = new Size(40, 20);
    this.TopWidth.TabIndex = 2;
    this.TopWidth.Text = "";
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(48 /*0x30*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Top";
    this.groupBox2.Controls.Add((Control) this.Rows);
    this.groupBox2.Controls.Add((Control) this.Cols);
    this.groupBox2.Controls.Add((Control) this.SelCells);
    this.groupBox2.Controls.Add((Control) this.AllCells);
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(112 /*0x70*/, 120);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Range";
    this.Rows.Location = new Point(8, 88);
    this.Rows.Name = "Rows";
    this.Rows.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.Rows.TabIndex = 3;
    this.Rows.Text = "Rows";
    this.Rows.Click += new EventHandler(this.SelectionClick);
    this.Cols.Location = new Point(8, 64 /*0x40*/);
    this.Cols.Name = "Cols";
    this.Cols.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.Cols.TabIndex = 2;
    this.Cols.Text = "Columns";
    this.Cols.Click += new EventHandler(this.SelectionClick);
    this.SelCells.Location = new Point(8, 40);
    this.SelCells.Name = "SelCells";
    this.SelCells.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.SelCells.TabIndex = 1;
    this.SelCells.Text = "Selected Cells";
    this.AllCells.Location = new Point(8, 16 /*0x10*/);
    this.AllCells.Name = "AllCells";
    this.AllCells.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.AllCells.TabIndex = 0;
    this.AllCells.Text = "All Cells";
    this.CellMargin.Location = new Point(112 /*0x70*/, 168);
    this.CellMargin.Name = "CellMargin";
    this.CellMargin.Size = new Size(40, 20);
    this.CellMargin.TabIndex = 3;
    this.CellMargin.Text = "";
    this.label2.Location = new Point(8, 168);
    this.label2.Name = "label2";
    this.label2.Size = new Size(104, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "Cell Margin (Twips)";
    this.Outline.Location = new Point(8, 144 /*0x90*/);
    this.Outline.Name = "Outline";
    this.Outline.Size = new Size(280, 16 /*0x10*/);
    this.Outline.TabIndex = 4;
    this.Outline.Text = "Draw outline border around selected cells";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(312, 237);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.Cancel);
    this.Controls.Add((Control) this.OK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_cell_border);
    this.Text = "Set Cell Border";
    this.Load += new EventHandler(this.terdlg_cell_border_Load);
    this.Activated += new EventHandler(this.terdlg_cell_border_Activated);
    this.groupBox1.ResumeLayout(false);
    this.group.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.CurCell = this.e.text[this.e.CurLine].cid;
    this.e.cell[this.CurCell].BorderWidth[0] = -1;
    this.e.cell[this.CurCell].BorderWidth[1] = -1;
    this.e.cell[this.CurCell].BorderWidth[2] = -1;
    this.e.cell[this.CurCell].BorderWidth[3] = -1;
    if (this.CellMargin.Text.Length > 0)
    {
      if (!this.ctl.CheckDlgValue((Form) this, 'I', this.CellMargin, 0.0, 720.0))
        return;
      this.e.cell[this.CurCell].margin = this.ctl.ToInt(this.CellMargin);
    }
    int margin = this.e.cell[this.CurCell].margin;
    if (this.ctl.ToInt(this.TopWidth) > margin || this.ctl.ToInt(this.BotWidth) > margin || this.ctl.ToInt(this.LeftWidth) > margin || this.ctl.ToInt(this.RightWidth) > margin)
    {
      this.ctl.PrintError(162, nameof (terdlg_cell_border));
      this.CellMargin.Focus();
    }
    else
    {
      if (this.TopWidth.Text.Length > 0)
      {
        if (!this.ctl.CheckDlgValue((Form) this, 'I', this.TopWidth, 0.0, (double) margin))
          return;
        this.e.cell[this.CurCell].BorderWidth[0] = this.ctl.ToInt(this.TopWidth);
      }
      if (this.BotWidth.Text.Length > 0)
      {
        if (!this.ctl.CheckDlgValue((Form) this, 'I', this.BotWidth, 0.0, (double) margin))
          return;
        this.e.cell[this.CurCell].BorderWidth[1] = this.ctl.ToInt(this.BotWidth);
      }
      if (this.LeftWidth.Text.Length > 0)
      {
        if (!this.ctl.CheckDlgValue((Form) this, 'I', this.LeftWidth, 0.0, (double) margin))
          return;
        this.e.cell[this.CurCell].BorderWidth[2] = this.ctl.ToInt(this.LeftWidth);
      }
      if (this.RightWidth.Text.Length > 0)
      {
        if (!this.ctl.CheckDlgValue((Form) this, 'I', this.RightWidth, 0.0, (double) margin))
          return;
        this.e.cell[this.CurCell].BorderWidth[3] = this.ctl.ToInt(this.RightWidth);
      }
      this.e.DlgBool1 = this.Outline.Checked;
      this.e.DlgResult = !this.AllCells.Checked ? (!this.SelCells.Checked ? (!this.Cols.Checked ? 888 : 887) : 889) : 942;
      this.DialogResult = DialogResult.OK;
    }
  }

  private void SelectionClick(object sender, EventArgs e)
  {
    this.TopWidth.Text = "";
    this.BotWidth.Text = "";
    this.LeftWidth.Text = "";
    this.RightWidth.Text = "";
  }

  private void terdlg_cell_border_Activated(object sender, EventArgs e) => this.TopWidth.Focus();

  private void terdlg_cell_border_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.CurCell = this.e.HilightType != 0 ? this.e.text[this.e.HilightBegRow].cid : this.e.text[this.e.CurLine].cid;
    int num = !this.ctl.True(this.e.cell[this.CurCell].border & 1) ? 0 : this.e.cell[this.CurCell].BorderWidth[0];
    if (num > 0 || this.e.HilightType == 0)
      this.TopWidth.Text = num.ToString();
    num = !this.ctl.True(this.e.cell[this.CurCell].border & 2) ? 0 : this.e.cell[this.CurCell].BorderWidth[1];
    if (num > 0 || this.e.HilightType == 0)
      this.BotWidth.Text = num.ToString();
    num = !this.ctl.True(this.e.cell[this.CurCell].border & 4) ? 0 : this.e.cell[this.CurCell].BorderWidth[2];
    if (num > 0 || this.e.HilightType == 0)
      this.LeftWidth.Text = num.ToString();
    num = !this.ctl.True(this.e.cell[this.CurCell].border & 8) ? 0 : this.e.cell[this.CurCell].BorderWidth[3];
    if (num > 0 || this.e.HilightType == 0)
      this.RightWidth.Text = num.ToString();
    if (this.e.HilightType == 0)
      this.AllCells.Checked = true;
    else
      this.SelCells.Checked = true;
    if (this.e.HilightType != 0)
      return;
    this.CellMargin.Text = this.e.cell[this.CurCell].margin.ToString();
  }
}
