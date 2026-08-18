// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_row_position
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_row_position : Form
{
  private CheckBox AllRows;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Button OK;
  private RadioButton PosCenter;
  private RadioButton PosLeft;
  private RadioButton PosRight;

  internal terdlg_row_position(ImRtfEditor parent)
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
    this.PosLeft = new RadioButton();
    this.PosCenter = new RadioButton();
    this.PosRight = new RadioButton();
    this.AllRows = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(24, 144 /*0x90*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(112 /*0x70*/, 144 /*0x90*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.AllRows,
      (Control) this.groupBox2
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(184, 136);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox2.Controls.AddRange(new Control[3]
    {
      (Control) this.PosRight,
      (Control) this.PosCenter,
      (Control) this.PosLeft
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(168, 80 /*0x50*/);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.PosLeft.Location = new Point(8, 16 /*0x10*/);
    this.PosLeft.Name = "PosLeft";
    this.PosLeft.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.PosLeft.TabIndex = 0;
    this.PosLeft.Text = "Left Justified";
    this.PosCenter.Location = new Point(8, 32 /*0x20*/);
    this.PosCenter.Name = "PosCenter";
    this.PosCenter.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.PosCenter.TabIndex = 1;
    this.PosCenter.Text = "Centered";
    this.PosRight.Location = new Point(8, 48 /*0x30*/);
    this.PosRight.Name = "PosRight";
    this.PosRight.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.PosRight.TabIndex = 2;
    this.PosRight.Text = "Right Justified";
    this.AllRows.Location = new Point(8, 104);
    this.AllRows.Name = "AllRows";
    this.AllRows.Size = new Size(168, 16 /*0x10*/);
    this.AllRows.TabIndex = 1;
    this.AllRows.Text = "All Rows in the Table";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(200, 173);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_row_position);
    this.Text = "Table Row Alignment";
    this.Load += new EventHandler(this.terdlg_row_position_Load);
    this.Activated += new EventHandler(this.terdlg_row_position_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgInt1 = 0;
    if (this.PosCenter.Checked)
      this.e.DlgInt1 = 1;
    else if (this.PosRight.Checked)
      this.e.DlgInt1 = 2;
    if (this.AllRows.Checked)
      this.e.DlgResult = 891;
    else
      this.e.DlgResult = 888;
  }

  private void terdlg_row_position_Activated(object sender, EventArgs e)
  {
  }

  private void terdlg_row_position_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    int row = this.e.cell[this.e.text[this.e.CurLine].cid].row;
    if (this.ctl.True(this.e.TableRow[row].flags & 1))
      this.PosCenter.Checked = true;
    else if (this.ctl.True(this.e.TableRow[row].flags & 2))
      this.PosRight.Checked = true;
    else
      this.PosLeft.Checked = true;
    this.AllRows.Checked = true;
  }
}
