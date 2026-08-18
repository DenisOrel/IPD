// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_cell_color
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_cell_color : Form
{
  private RadioButton AllCells;
  private Button Cancel;
  private Button CellColor;
  private RadioButton Cols;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private int CurCell;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Button OK;
  private RadioButton Rows;
  private RadioButton SelCells;

  internal terdlg_cell_color(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void CellColor_Click(object sender, EventArgs ev)
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
    this.groupBox2 = new GroupBox();
    this.Rows = new RadioButton();
    this.Cols = new RadioButton();
    this.SelCells = new RadioButton();
    this.AllCells = new RadioButton();
    this.CellColor = new Button();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(8, 152);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(96 /*0x60*/, 152);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.CellColor,
      (Control) this.groupBox2
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(168, 144 /*0x90*/);
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
    this.groupBox2.Size = new Size(152, 88);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Range";
    this.Rows.Location = new Point(8, 64 /*0x40*/);
    this.Rows.Name = "Rows";
    this.Rows.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.Rows.TabIndex = 3;
    this.Rows.Text = "Rows";
    this.Cols.Location = new Point(8, 48 /*0x30*/);
    this.Cols.Name = "Cols";
    this.Cols.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.Cols.TabIndex = 2;
    this.Cols.Text = "Columns";
    this.SelCells.Location = new Point(8, 32 /*0x20*/);
    this.SelCells.Name = "SelCells";
    this.SelCells.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.SelCells.TabIndex = 1;
    this.SelCells.Text = "Selected Cells";
    this.AllCells.Location = new Point(8, 16 /*0x10*/);
    this.AllCells.Name = "AllCells";
    this.AllCells.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.AllCells.TabIndex = 0;
    this.AllCells.Text = "All Cells";
    this.CellColor.Location = new Point(8, 112 /*0x70*/);
    this.CellColor.Name = "CellColor";
    this.CellColor.Size = new Size(152, 24);
    this.CellColor.TabIndex = 1;
    this.CellColor.Text = "Set Color...";
    this.CellColor.Click += new EventHandler(this.CellColor_Click);
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(184, 181);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_cell_color);
    this.Text = "Cell Color Parameters";
    this.Load += new EventHandler(this.terdlg_cell_color_Load);
    this.Activated += new EventHandler(this.terdlg_cell_color_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.CurCell = this.e.text[this.e.CurLine].cid;
    this.e.cell[this.CurCell].BackColor = this.e.DlgColor1;
    this.e.DlgResult = !this.AllCells.Checked ? (!this.SelCells.Checked ? (!this.Cols.Checked ? 888 : 887) : 889) : 942;
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_cell_color_Activated(object sender, EventArgs e) => this.CellColor.Focus();

  private void terdlg_cell_color_Load(object sender, EventArgs ev)
  {
    bool flag = true;
    this.e.misc.CenterDlgBox((Form) sender);
    this.CurCell = this.e.text[this.e.CurLine].cid;
    Color color;
    if (this.e.HilightType != 0)
    {
      color = Color.White;
      int num = this.e.HilightEndCol != 0 ? this.e.HilightEndRow : this.e.HilightEndRow - 1;
      for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= num; ++hilightBegRow)
      {
        if (this.ctl.LineSelected(hilightBegRow))
        {
          int cid = this.e.text[hilightBegRow].cid;
          if (cid != 0)
          {
            if (color == Color.White)
              color = this.e.cell[cid].BackColor;
            if (this.e.cell[cid].BackColor != color)
            {
              flag = false;
              break;
            }
          }
        }
      }
    }
    else
      color = this.e.cell[this.CurCell].BackColor;
    this.e.DlgColor1 = !flag ? tc.CLR_WHITE : color;
    if (this.e.HilightType == 0)
      this.AllCells.Checked = true;
    else
      this.SelCells.Checked = true;
  }
}
