// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_cell_rotation
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_cell_rotation : Form
{
  private RadioButton AllCells;
  private RadioButton BotToTop;
  private Button Cancel;
  private RadioButton Cols;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private int CurCell;
  private ImRtfEditor e;
  private GroupBox group;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private RadioButton Horz;
  private Button OK;
  private RadioButton Rows;
  private RadioButton SelCells;
  private RadioButton TopToBot;

  internal terdlg_cell_rotation(ImRtfEditor parent)
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
    this.BotToTop = new RadioButton();
    this.TopToBot = new RadioButton();
    this.Horz = new RadioButton();
    this.groupBox2 = new GroupBox();
    this.Rows = new RadioButton();
    this.Cols = new RadioButton();
    this.SelCells = new RadioButton();
    this.AllCells = new RadioButton();
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
    this.groupBox1.Controls.Add((Control) this.group);
    this.groupBox1.Controls.Add((Control) this.groupBox2);
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(248, 112 /*0x70*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.group.Controls.Add((Control) this.BotToTop);
    this.group.Controls.Add((Control) this.TopToBot);
    this.group.Controls.Add((Control) this.Horz);
    this.group.Location = new Point(128 /*0x80*/, 16 /*0x10*/);
    this.group.Name = "group";
    this.group.Size = new Size(112 /*0x70*/, 88);
    this.group.TabIndex = 1;
    this.group.TabStop = false;
    this.group.Text = "Text Rotation";
    this.BotToTop.Location = new Point(8, 64 /*0x40*/);
    this.BotToTop.Name = "BotToTop";
    this.BotToTop.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.BotToTop.TabIndex = 2;
    this.BotToTop.Text = "Bottom to Top";
    this.TopToBot.Location = new Point(8, 40);
    this.TopToBot.Name = "TopToBot";
    this.TopToBot.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.TopToBot.TabIndex = 1;
    this.TopToBot.Text = "Top to Bottom";
    this.Horz.Location = new Point(8, 16 /*0x10*/);
    this.Horz.Name = "Horz";
    this.Horz.Size = new Size(88, 16 /*0x10*/);
    this.Horz.TabIndex = 0;
    this.Horz.Text = "Horizontal";
    this.groupBox2.Controls.Add((Control) this.Rows);
    this.groupBox2.Controls.Add((Control) this.Cols);
    this.groupBox2.Controls.Add((Control) this.SelCells);
    this.groupBox2.Controls.Add((Control) this.AllCells);
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
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(264, 149);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.Cancel);
    this.Controls.Add((Control) this.OK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_cell_rotation);
    this.Text = "Cell Text Rotation";
    this.Load += new EventHandler(this.terdlg_cell_rotation_Load);
    this.Activated += new EventHandler(this.terdlg_cell_rotation_Activated);
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
    if (this.Horz.Checked || this.TopToBot.Checked || this.BotToTop.Checked)
      flag = true;
    if (!flag)
    {
      this.e.DlgResult = 0;
    }
    else
    {
      int num = 0;
      if (this.TopToBot.Checked)
        num = 270;
      if (this.BotToTop.Checked)
        num = 90;
      this.e.cell[this.CurCell].TextAngle = num;
      this.e.DlgResult = !this.AllCells.Checked ? (!this.SelCells.Checked ? (!this.Cols.Checked ? 888 : 887) : 889) : 942;
    }
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_cell_rotation_Activated(object sender, EventArgs e)
  {
  }

  private void terdlg_cell_rotation_Load(object sender, EventArgs ev)
  {
    bool flag1 = true;
    this.e.misc.CenterDlgBox((Form) sender);
    this.CurCell = this.e.text[this.e.CurLine].cid;
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
              num1 = this.e.cell[cid].TextAngle;
              flag2 = true;
            }
            if (this.e.cell[cid].TextAngle != num1)
            {
              flag1 = false;
              break;
            }
          }
        }
      }
    }
    else
      num1 = this.e.cell[this.CurCell].TextAngle;
    if (flag1)
    {
      this.Horz.Checked = num1 == 0;
      this.TopToBot.Checked = num1 == 270;
      this.BotToTop.Checked = num1 == 90;
    }
    this.SelCells.Checked = true;
  }
}
