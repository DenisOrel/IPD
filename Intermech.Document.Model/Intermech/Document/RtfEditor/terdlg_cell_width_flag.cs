// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_cell_width_flag
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_cell_width_flag : Form
{
  private RadioButton All;
  private RadioButton BestFit;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private RadioButton FixWidth;
  private RadioButton FixWidthPct;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private GroupBox groupBox3;
  private Button OK;
  private RadioButton SelCells;
  private RadioButton SelCols;
  private RadioButton SelRows;

  internal terdlg_cell_width_flag(ImRtfEditor parent)
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
    this.groupBox2 = new GroupBox();
    this.All = new RadioButton();
    this.SelCells = new RadioButton();
    this.SelCols = new RadioButton();
    this.SelRows = new RadioButton();
    this.groupBox3 = new GroupBox();
    this.BestFit = new RadioButton();
    this.FixWidth = new RadioButton();
    this.FixWidthPct = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(128 /*0x80*/, 216);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(216, 216);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.groupBox3,
      (Control) this.groupBox2
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(288, 208 /*0xD0*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox2.Controls.AddRange(new Control[4]
    {
      (Control) this.SelRows,
      (Control) this.SelCols,
      (Control) this.SelCells,
      (Control) this.All
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(272, 88);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Range";
    this.All.Location = new Point(8, 16 /*0x10*/);
    this.All.Name = "All";
    this.All.Size = new Size(120, 16 /*0x10*/);
    this.All.TabIndex = 0;
    this.All.Text = "All Cells";
    this.SelCells.Location = new Point(8, 32 /*0x20*/);
    this.SelCells.Name = "SelCells";
    this.SelCells.Size = new Size(120, 16 /*0x10*/);
    this.SelCells.TabIndex = 1;
    this.SelCells.Text = "Selected Cells";
    this.SelCols.Location = new Point(8, 48 /*0x30*/);
    this.SelCols.Name = "SelCols";
    this.SelCols.Size = new Size(120, 16 /*0x10*/);
    this.SelCols.TabIndex = 2;
    this.SelCols.Text = "Columns";
    this.SelRows.Location = new Point(8, 64 /*0x40*/);
    this.SelRows.Name = "SelRows";
    this.SelRows.Size = new Size(120, 16 /*0x10*/);
    this.SelRows.TabIndex = 3;
    this.SelRows.Text = "Rows";
    this.groupBox3.Controls.AddRange(new Control[3]
    {
      (Control) this.FixWidthPct,
      (Control) this.FixWidth,
      (Control) this.BestFit
    });
    this.groupBox3.Location = new Point(8, 112 /*0x70*/);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(272, 88);
    this.groupBox3.TabIndex = 1;
    this.groupBox3.TabStop = false;
    this.groupBox3.Text = "Cell Width";
    this.BestFit.Location = new Point(8, 16 /*0x10*/);
    this.BestFit.Name = "BestFit";
    this.BestFit.Size = new Size(232, 16 /*0x10*/);
    this.BestFit.TabIndex = 0;
    this.BestFit.Text = "Best fit";
    this.FixWidth.Location = new Point(8, 32 /*0x20*/);
    this.FixWidth.Name = "FixWidth";
    this.FixWidth.Size = new Size(232, 16 /*0x10*/);
    this.FixWidth.TabIndex = 1;
    this.FixWidth.Text = "Current width";
    this.FixWidth.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
    this.FixWidthPct.Location = new Point(8, 48 /*0x30*/);
    this.FixWidthPct.Name = "FixWidthPct";
    this.FixWidthPct.Size = new Size(256 /*0x0100*/, 16 /*0x10*/);
    this.FixWidthPct.TabIndex = 2;
    this.FixWidthPct.Text = "Current width as percentage of the table width";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(304, 245);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_cell_width_flag);
    this.Text = "Cell Width";
    this.Load += new EventHandler(this.terdlg_cell_width_flag_Load);
    this.Activated += new EventHandler(this.terdlg_cell_width_flag_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.e.DlgInt1 = 0;
    if (this.FixWidth.Checked)
      this.e.DlgInt1 = 256 /*0x0100*/;
    if (this.FixWidthPct.Checked)
      this.e.DlgInt1 = 512 /*0x0200*/;
    this.e.DlgResult = !this.All.Checked ? (!this.SelCells.Checked ? (!this.SelCols.Checked ? 888 : 887) : 889) : 942;
    this.DialogResult = DialogResult.OK;
  }

  private void radioButton1_CheckedChanged(object sender, EventArgs e)
  {
  }

  private void terdlg_cell_width_flag_Activated(object sender, EventArgs e)
  {
  }

  private void terdlg_cell_width_flag_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    int dlgInt1 = this.e.DlgInt1;
    if (this.ctl.True(dlgInt1 & 256 /*0x0100*/))
      this.FixWidth.Checked = true;
    else if (this.ctl.True(dlgInt1 & 512 /*0x0200*/))
      this.FixWidthPct.Checked = true;
    else
      this.BestFit.Checked = true;
    if (this.e.HilightType == 0)
      this.SelCols.Checked = true;
    else
      this.SelCells.Checked = true;
  }
}
