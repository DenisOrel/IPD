// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_cell_vert_align
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_cell_vert_align : Form
{
  private RadioButton AlignBase;
  private RadioButton AlignBot;
  private RadioButton AlignCtr;
  private int AlignFlags;
  private RadioButton AlignTop;
  private RadioButton AllCells;
  private Button Cancel;
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

  internal terdlg_cell_vert_align(ImRtfEditor parent)
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
    this.groupBox2 = new GroupBox();
    this.Rows = new RadioButton();
    this.Cols = new RadioButton();
    this.SelCells = new RadioButton();
    this.AllCells = new RadioButton();
    this.AlignTop = new RadioButton();
    this.AlignCtr = new RadioButton();
    this.AlignBot = new RadioButton();
    this.AlignBase = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.group.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(88, 120);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(176 /*0xB0*/, 120);
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
    this.groupBox1.Size = new Size(248, 112 /*0x70*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.group.Controls.AddRange(new Control[4]
    {
      (Control) this.AlignBase,
      (Control) this.AlignBot,
      (Control) this.AlignCtr,
      (Control) this.AlignTop
    });
    this.group.Location = new Point(128 /*0x80*/, 16 /*0x10*/);
    this.group.Name = "group";
    this.group.Size = new Size(112 /*0x70*/, 88);
    this.group.TabIndex = 1;
    this.group.TabStop = false;
    this.group.Text = "Vertical Alignment";
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
    this.AlignTop.Location = new Point(8, 16 /*0x10*/);
    this.AlignTop.Name = "AlignTop";
    this.AlignTop.Size = new Size(88, 16 /*0x10*/);
    this.AlignTop.TabIndex = 0;
    this.AlignTop.Text = "Top";
    this.AlignCtr.Location = new Point(8, 32 /*0x20*/);
    this.AlignCtr.Name = "AlignCtr";
    this.AlignCtr.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.AlignCtr.TabIndex = 1;
    this.AlignCtr.Text = "Center";
    this.AlignBot.Location = new Point(8, 48 /*0x30*/);
    this.AlignBot.Name = "AlignBot";
    this.AlignBot.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.AlignBot.TabIndex = 2;
    this.AlignBot.Text = "Bottom";
    this.AlignBase.Location = new Point(8, 64 /*0x40*/);
    this.AlignBase.Name = "AlignBase";
    this.AlignBase.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.AlignBase.TabIndex = 3;
    this.AlignBase.Text = "Baseline";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(264, 149);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_cell_vert_align);
    this.Text = "Cell Vertical Alignment";
    this.Load += new EventHandler(this.terdlg_cell_vert_align_Load);
    this.Activated += new EventHandler(this.terdlg_cell_vert_align_Activated);
    this.groupBox1.ResumeLayout(false);
    this.group.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.CurCell = this.e.text[this.e.CurLine].cid;
    bool flag = false;
    if (this.AlignTop.Checked || this.AlignCtr.Checked || this.AlignBot.Checked || this.AlignBase.Checked)
      flag = true;
    if (!flag)
    {
      this.e.DlgResult = 0;
    }
    else
    {
      int num = 0;
      if (this.AlignCtr.Checked)
        num = 4096 /*0x1000*/;
      if (this.AlignBot.Checked)
        num = 8192 /*0x2000*/;
      if (this.AlignBase.Checked)
        num = 65536 /*0x010000*/;
      this.e.cell[this.CurCell].flags = tc.ResetUintFlag(ref this.e.cell[this.CurCell].flags, this.AlignFlags);
      this.e.cell[this.CurCell].flags |= num;
      this.e.DlgResult = !this.AllCells.Checked ? (!this.SelCells.Checked ? (!this.Cols.Checked ? 888 : 887) : 889) : 942;
    }
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_cell_vert_align_Activated(object sender, EventArgs e)
  {
  }

  private void terdlg_cell_vert_align_Load(object sender, EventArgs ev)
  {
    bool flag1 = true;
    this.e.misc.CenterDlgBox((Form) sender);
    this.CurCell = this.e.text[this.e.CurLine].cid;
    this.AlignFlags = 77824 /*0x013000*/;
    int num1;
    if (this.e.HilightType != 0)
    {
      num1 = 0;
      bool flag2 = false;
      int num2 = this.e.HilightEndCol != 0 ? this.e.HilightEndRow : this.e.HilightEndRow - 1;
      for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= num2; ++hilightBegRow)
      {
        if (this.ctl.LineSelected(hilightBegRow))
        {
          int cid = this.e.text[hilightBegRow].cid;
          if (cid != 0)
          {
            if (!flag2)
            {
              num1 = this.e.cell[cid].flags & this.AlignFlags;
              flag2 = true;
            }
            if ((this.e.cell[cid].flags & this.AlignFlags) != num1)
            {
              flag1 = false;
              break;
            }
          }
        }
      }
    }
    else
      num1 = this.e.cell[this.CurCell].flags & this.AlignFlags;
    if (flag1)
    {
      this.AlignTop.Checked = num1 == 0;
      this.AlignCtr.Checked = num1 == 4096 /*0x1000*/;
      this.AlignBot.Checked = num1 == 8192 /*0x2000*/;
      this.AlignBase.Checked = num1 == 65536 /*0x010000*/;
    }
    if (this.e.HilightType == 0)
      this.AllCells.Checked = true;
    else
      this.SelCells.Checked = true;
  }
}
