// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_cell_shading
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_cell_shading : Form
{
  private RadioButton AllCells;
  private Button Cancel;
  private TextBox CellShade;
  private RadioButton Cols;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private int CurCell;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Label label1;
  private Button OK;
  private RadioButton Rows;
  private RadioButton SelCells;

  internal terdlg_cell_shading(ImRtfEditor parent)
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
    this.CellShade = new TextBox();
    this.label1 = new Label();
    this.groupBox2 = new GroupBox();
    this.Rows = new RadioButton();
    this.Cols = new RadioButton();
    this.SelCells = new RadioButton();
    this.AllCells = new RadioButton();
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
    this.groupBox1.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox2,
      (Control) this.label1,
      (Control) this.CellShade
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(168, 144 /*0x90*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.CellShade.Location = new Point(120, 112 /*0x70*/);
    this.CellShade.Name = "CellShade";
    this.CellShade.Size = new Size(40, 20);
    this.CellShade.TabIndex = 2;
    this.CellShade.Text = "";
    this.label1.Location = new Point(8, 112 /*0x70*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Shading Percentage";
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
    this.Name = nameof (terdlg_cell_shading);
    this.Text = "Cell Shading Parameters";
    this.Load += new EventHandler(this.terdlg_cell_shading_Load);
    this.Activated += new EventHandler(this.terdlg_cell_shading_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.CurCell = this.e.text[this.e.CurLine].cid;
    if (this.CellShade.Text.Length == 0)
    {
      this.e.DlgResult = 0;
    }
    else
    {
      if (!this.ctl.CheckDlgValue((Form) this, 'I', this.CellShade, 0.0, 100.0))
        return;
      this.e.cell[this.CurCell].shading = this.ctl.ToInt(this.CellShade);
      this.e.DlgResult = !this.AllCells.Checked ? (!this.SelCells.Checked ? (!this.Cols.Checked ? 888 : 887) : 889) : 942;
    }
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_cell_shading_Activated(object sender, EventArgs e) => this.CellShade.Focus();

  private void terdlg_cell_shading_Load(object sender, EventArgs ev)
  {
    bool flag = true;
    this.e.misc.CenterDlgBox((Form) sender);
    this.CurCell = this.e.text[this.e.CurLine].cid;
    int num1;
    if (this.e.HilightType != 0)
    {
      num1 = -1;
      int num2 = this.e.HilightEndCol != 0 ? this.e.HilightEndRow : this.e.HilightEndRow - 1;
      for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= num2; ++hilightBegRow)
      {
        if (this.ctl.LineSelected(hilightBegRow))
        {
          int cid = this.e.text[hilightBegRow].cid;
          if (cid != 0)
          {
            if (num1 < 0)
              num1 = this.e.cell[cid].shading;
            if (this.e.cell[cid].shading != num1)
            {
              flag = false;
              break;
            }
          }
        }
      }
      if (num1 < 0)
        num1 = 0;
    }
    else
      num1 = this.e.cell[this.CurCell].shading;
    if (flag)
      this.CellShade.Text = num1.ToString();
    if (this.e.HilightType == 0)
      this.AllCells.Checked = true;
    else
      this.SelCells.Checked = true;
  }
}
