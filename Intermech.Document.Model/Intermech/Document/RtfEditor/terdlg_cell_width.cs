// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_cell_width
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_cell_width : Form
{
  private RadioButton AllCells;
  private Button Cancel;
  private TextBox CellMargin;
  private TextBox CellWidth;
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
  private Button OK;
  private RadioButton Rows;
  private RadioButton SelCells;

  internal terdlg_cell_width(ImRtfEditor parent)
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
    this.AllCells = new RadioButton();
    this.SelCells = new RadioButton();
    this.Cols = new RadioButton();
    this.Rows = new RadioButton();
    this.group = new GroupBox();
    this.label1 = new Label();
    this.label2 = new Label();
    this.CellWidth = new TextBox();
    this.CellMargin = new TextBox();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.group.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(128 /*0x80*/, 120);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(216, 120);
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
    this.groupBox1.Size = new Size(296, 112 /*0x70*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox2.Controls.AddRange(new Control[4]
    {
      (Control) this.Rows,
      (Control) this.Cols,
      (Control) this.SelCells,
      (Control) this.AllCells
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(112 /*0x70*/, 88);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Range";
    this.AllCells.Location = new Point(8, 16 /*0x10*/);
    this.AllCells.Name = "AllCells";
    this.AllCells.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.AllCells.TabIndex = 0;
    this.AllCells.Text = "All Cells";
    this.SelCells.Location = new Point(8, 32 /*0x20*/);
    this.SelCells.Name = "SelCells";
    this.SelCells.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.SelCells.TabIndex = 1;
    this.SelCells.Text = "Selected Cells";
    this.Cols.Location = new Point(8, 48 /*0x30*/);
    this.Cols.Name = "Cols";
    this.Cols.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.Cols.TabIndex = 2;
    this.Cols.Text = "Columns";
    this.Rows.Location = new Point(8, 64 /*0x40*/);
    this.Rows.Name = "Rows";
    this.Rows.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.Rows.TabIndex = 3;
    this.Rows.Text = "Rows";
    this.group.Controls.AddRange(new Control[4]
    {
      (Control) this.CellMargin,
      (Control) this.CellWidth,
      (Control) this.label2,
      (Control) this.label1
    });
    this.group.Location = new Point(128 /*0x80*/, 16 /*0x10*/);
    this.group.Name = "group";
    this.group.Size = new Size(160 /*0xA0*/, 88);
    this.group.TabIndex = 1;
    this.group.TabStop = false;
    this.label1.Location = new Point(8, 26);
    this.label1.Name = "label1";
    this.label1.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Cell Width (Twips)";
    this.label2.Location = new Point(8, 48 /*0x30*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(104, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "Cell Margin (Twips)";
    this.CellWidth.Location = new Point(112 /*0x70*/, 24);
    this.CellWidth.Name = "CellWidth";
    this.CellWidth.Size = new Size(40, 20);
    this.CellWidth.TabIndex = 2;
    this.CellWidth.Text = "";
    this.CellMargin.Location = new Point(112 /*0x70*/, 46);
    this.CellMargin.Name = "CellMargin";
    this.CellMargin.Size = new Size(40, 20);
    this.CellMargin.TabIndex = 3;
    this.CellMargin.Text = "";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(312, 149);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_cell_width);
    this.Text = "Set Cell Width";
    this.Load += new EventHandler(this.terdlg_cell_width_Load);
    this.Activated += new EventHandler(this.terdlg_cell_width_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.group.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.CurCell = this.e.text[this.e.CurLine].cid;
    this.e.DlgInt1 = this.e.DlgInt2 = -1;
    if (this.CellWidth.Text.Length > 0)
      this.e.DlgInt1 = this.ctl.ToInt(this.CellWidth);
    if (this.CellMargin.Text.Length > 0)
    {
      if (!this.ctl.CheckDlgValue((Form) this, 'I', this.CellMargin, 0.0, 720.0))
        return;
      this.e.DlgInt2 = this.ctl.ToInt(this.CellMargin);
    }
    this.e.DlgResult = !this.AllCells.Checked ? (!this.SelCells.Checked ? (!this.Cols.Checked ? 888 : 887) : 889) : 942;
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_cell_width_Activated(object sender, EventArgs e) => this.CellWidth.Focus();

  private void terdlg_cell_width_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.CurCell = this.e.HilightType != 0 ? this.e.text[this.e.HilightBegRow].cid : this.e.text[this.e.CurLine].cid;
    if (this.e.HilightType == 0)
    {
      this.CellWidth.Text = this.e.cell[this.CurCell].width.ToString();
      this.CellMargin.Text = this.e.cell[this.CurCell].margin.ToString();
    }
    this.SelCells.Checked = true;
  }
}
