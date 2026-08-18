// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_cell_border_color
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_cell_border_color : Form
{
  private RadioButton AllCells;
  private Button Cancel;
  private Button ColorAll;
  private Button ColorBot;
  private Button ColorLeft;
  private Button ColorRight;
  private Button ColorTop;
  private RadioButton Cols;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private int CurCell;
  private ImRtfEditor e;
  private GroupBox group;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Button OK;
  private RadioButton Rows;
  private RadioButton SelCells;

  internal terdlg_cell_border_color(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void ColorAll_Click(object sender, EventArgs ev)
  {
    this.e.DlgColor1 = this.e.DlgColor2 = this.e.DlgColor3 = this.e.DlgColor4 = this.ctl.DlgEditColor((Control) this, this.e.DlgColor1, true);
  }

  private void ColorBot_Click(object sender, EventArgs ev)
  {
    this.e.DlgColor2 = this.ctl.DlgEditColor((Control) this, this.e.DlgColor2, true);
  }

  private void ColorLeft_Click(object sender, EventArgs ev)
  {
    this.e.DlgColor3 = this.ctl.DlgEditColor((Control) this, this.e.DlgColor3, true);
  }

  private void ColorRight_Click(object sender, EventArgs ev)
  {
    this.e.DlgColor4 = this.ctl.DlgEditColor((Control) this, this.e.DlgColor4, true);
  }

  private void ColorTop_Click(object sender, EventArgs ev)
  {
    this.e.DlgColor1 = this.ctl.DlgEditColor((Control) this, this.e.DlgColor1, true);
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
    this.groupBox2 = new GroupBox();
    this.Rows = new RadioButton();
    this.Cols = new RadioButton();
    this.SelCells = new RadioButton();
    this.AllCells = new RadioButton();
    this.ColorAll = new Button();
    this.ColorTop = new Button();
    this.ColorBot = new Button();
    this.ColorLeft = new Button();
    this.ColorRight = new Button();
    this.groupBox1.SuspendLayout();
    this.group.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(64 /*0x40*/, 184);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(152, 184);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.group,
      (Control) this.groupBox2
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(224 /*0xE0*/, 176 /*0xB0*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.group.Controls.AddRange(new Control[5]
    {
      (Control) this.ColorRight,
      (Control) this.ColorLeft,
      (Control) this.ColorBot,
      (Control) this.ColorTop,
      (Control) this.ColorAll
    });
    this.group.Location = new Point(128 /*0x80*/, 16 /*0x10*/);
    this.group.Name = "group";
    this.group.Size = new Size(88, 152);
    this.group.TabIndex = 1;
    this.group.TabStop = false;
    this.group.Text = "Border Color";
    this.groupBox2.Controls.AddRange(new Control[4]
    {
      (Control) this.Rows,
      (Control) this.Cols,
      (Control) this.SelCells,
      (Control) this.AllCells
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(112 /*0x70*/, 152);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Range";
    this.Rows.Location = new Point(8, 96 /*0x60*/);
    this.Rows.Name = "Rows";
    this.Rows.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.Rows.TabIndex = 3;
    this.Rows.Text = "Rows";
    this.Cols.Location = new Point(8, 72);
    this.Cols.Name = "Cols";
    this.Cols.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.Cols.TabIndex = 2;
    this.Cols.Text = "Columns";
    this.SelCells.Location = new Point(8, 48 /*0x30*/);
    this.SelCells.Name = "SelCells";
    this.SelCells.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.SelCells.TabIndex = 1;
    this.SelCells.Text = "Selected Cells";
    this.AllCells.Location = new Point(8, 24);
    this.AllCells.Name = "AllCells";
    this.AllCells.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.AllCells.TabIndex = 0;
    this.AllCells.Text = "All Cells";
    this.ColorAll.Location = new Point(8, 16 /*0x10*/);
    this.ColorAll.Name = "ColorAll";
    this.ColorAll.Size = new Size(64 /*0x40*/, 24);
    this.ColorAll.TabIndex = 0;
    this.ColorAll.Text = "All...";
    this.ColorAll.Click += new EventHandler(this.ColorAll_Click);
    this.ColorTop.Location = new Point(8, 48 /*0x30*/);
    this.ColorTop.Name = "ColorTop";
    this.ColorTop.Size = new Size(64 /*0x40*/, 24);
    this.ColorTop.TabIndex = 1;
    this.ColorTop.Text = "Top...";
    this.ColorTop.Click += new EventHandler(this.ColorTop_Click);
    this.ColorBot.Location = new Point(8, 72);
    this.ColorBot.Name = "ColorBot";
    this.ColorBot.Size = new Size(64 /*0x40*/, 24);
    this.ColorBot.TabIndex = 2;
    this.ColorBot.Text = "Bottom...";
    this.ColorBot.Click += new EventHandler(this.ColorBot_Click);
    this.ColorLeft.Location = new Point(8, 96 /*0x60*/);
    this.ColorLeft.Name = "ColorLeft";
    this.ColorLeft.Size = new Size(64 /*0x40*/, 24);
    this.ColorLeft.TabIndex = 3;
    this.ColorLeft.Text = "Left...";
    this.ColorLeft.Click += new EventHandler(this.ColorLeft_Click);
    this.ColorRight.Location = new Point(8, 120);
    this.ColorRight.Name = "ColorRight";
    this.ColorRight.Size = new Size(64 /*0x40*/, 24);
    this.ColorRight.TabIndex = 4;
    this.ColorRight.Text = "Right...";
    this.ColorRight.Click += new EventHandler(this.ColorRight_Click);
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(240 /*0xF0*/, 213);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_cell_border_color);
    this.Text = "Set Cell Border Color";
    this.Load += new EventHandler(this.terdlg_cell_border_color_Load);
    this.Activated += new EventHandler(this.terdlg_cell_border_color_Activated);
    this.groupBox1.ResumeLayout(false);
    this.group.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.CurCell = this.e.text[this.e.CurLine].cid;
    this.e.cell[this.CurCell].BorderColor[0] = this.e.DlgColor1;
    this.e.cell[this.CurCell].BorderColor[1] = this.e.DlgColor2;
    this.e.cell[this.CurCell].BorderColor[2] = this.e.DlgColor3;
    this.e.cell[this.CurCell].BorderColor[3] = this.e.DlgColor4;
    this.e.DlgResult = !this.AllCells.Checked ? (!this.SelCells.Checked ? (!this.Cols.Checked ? 888 : 887) : 889) : 942;
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_cell_border_color_Activated(object sender, EventArgs e) => this.OK.Focus();

  private void terdlg_cell_border_color_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.CurCell = this.e.HilightType != 0 ? this.e.text[this.e.HilightBegRow].cid : this.e.text[this.e.CurLine].cid;
    this.e.DlgColor1 = this.e.cell[this.CurCell].BorderColor[0];
    this.e.DlgColor2 = this.e.cell[this.CurCell].BorderColor[1];
    this.e.DlgColor3 = this.e.cell[this.CurCell].BorderColor[2];
    this.e.DlgColor4 = this.e.cell[this.CurCell].BorderColor[3];
    if (this.e.HtmlMode)
    {
      this.ColorTop.Enabled = false;
      this.ColorBot.Enabled = false;
      this.ColorLeft.Enabled = false;
      this.ColorRight.Enabled = false;
    }
    if (this.e.HilightType == 0)
      this.AllCells.Checked = true;
    else
      this.SelCells.Checked = true;
  }
}
