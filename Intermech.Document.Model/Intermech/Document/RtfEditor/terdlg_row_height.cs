// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_row_height
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_row_height : Form
{
  private CheckBox AllRows;
  private RadioButton AutoHt;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private RadioButton ExactHt;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Label HeightLabel;
  private RadioButton MinHt;
  private Button OK;
  private TextBox RowHeight;

  internal terdlg_row_height(ImRtfEditor parent)
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

  private void HeightSelectionClick(object sender, EventArgs e)
  {
    bool flag = this.AutoHt.Checked;
    this.HeightLabel.Enabled = !flag;
    this.RowHeight.Enabled = !flag;
  }

  private void InitializeComponent()
  {
    this.OK = new Button();
    this.Cancel = new Button();
    this.groupBox1 = new GroupBox();
    this.AllRows = new CheckBox();
    this.groupBox2 = new GroupBox();
    this.ExactHt = new RadioButton();
    this.MinHt = new RadioButton();
    this.AutoHt = new RadioButton();
    this.HeightLabel = new Label();
    this.RowHeight = new TextBox();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(64 /*0x40*/, 120);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(152, 120);
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
    this.groupBox1.Size = new Size(224 /*0xE0*/, 112 /*0x70*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.AllRows.Location = new Point(8, 88);
    this.AllRows.Name = "AllRows";
    this.AllRows.Size = new Size(208 /*0xD0*/, 16 /*0x10*/);
    this.AllRows.TabIndex = 1;
    this.AllRows.Text = "Apply to all rows in the current table";
    this.groupBox2.Controls.AddRange(new Control[5]
    {
      (Control) this.ExactHt,
      (Control) this.MinHt,
      (Control) this.AutoHt,
      (Control) this.HeightLabel,
      (Control) this.RowHeight
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(208 /*0xD0*/, 64 /*0x40*/);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Height";
    this.ExactHt.Location = new Point(144 /*0x90*/, 16 /*0x10*/);
    this.ExactHt.Name = "ExactHt";
    this.ExactHt.Size = new Size(56, 16 /*0x10*/);
    this.ExactHt.TabIndex = 2;
    this.ExactHt.Text = "Exact";
    this.ExactHt.Click += new EventHandler(this.HeightSelectionClick);
    this.MinHt.Location = new Point(64 /*0x40*/, 16 /*0x10*/);
    this.MinHt.Name = "MinHt";
    this.MinHt.Size = new Size(72, 16 /*0x10*/);
    this.MinHt.TabIndex = 1;
    this.MinHt.Text = "Minimum";
    this.MinHt.Click += new EventHandler(this.HeightSelectionClick);
    this.AutoHt.Location = new Point(8, 16 /*0x10*/);
    this.AutoHt.Name = "AutoHt";
    this.AutoHt.Size = new Size(56, 16 /*0x10*/);
    this.AutoHt.TabIndex = 0;
    this.AutoHt.Text = "Auto";
    this.AutoHt.Click += new EventHandler(this.HeightSelectionClick);
    this.HeightLabel.Location = new Point(72, 40);
    this.HeightLabel.Name = "HeightLabel";
    this.HeightLabel.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.HeightLabel.TabIndex = 1;
    this.HeightLabel.Text = "Height (twips)";
    this.RowHeight.Location = new Point(152, 38);
    this.RowHeight.Name = "RowHeight";
    this.RowHeight.Size = new Size(40, 20);
    this.RowHeight.TabIndex = 2;
    this.RowHeight.Text = "";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(240 /*0xF0*/, 149);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_row_height);
    this.Text = "Row Height";
    this.Load += new EventHandler(this.terdlg_row_height_Load);
    this.Activated += new EventHandler(this.terdlg_row_height_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.e.DlgInt1 = 0;
    if (!this.AutoHt.Checked)
    {
      if (!this.ctl.CheckDlgValue((Form) this, 'I', this.RowHeight, 0.0, (double) this.ctl.InchesToTwips((double) this.e.PageHeight)))
        return;
      this.e.DlgInt1 = this.ctl.ToInt(this.RowHeight);
      if (this.ExactHt.Checked)
        this.e.DlgInt1 = -this.e.DlgInt1;
    }
    this.e.DlgResult = !this.AllRows.Checked ? 888 : 891;
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_row_height_Activated(object sender, EventArgs e)
  {
  }

  private void terdlg_row_height_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    int minHeight = this.e.TableRow[this.e.cell[this.e.text[this.e.CurLine].cid].row].MinHeight;
    bool flag = false;
    if (minHeight == 0)
    {
      this.AutoHt.Checked = true;
      flag = true;
    }
    else if (minHeight > 0)
    {
      this.RowHeight.Text = minHeight.ToString();
      this.MinHt.Checked = true;
    }
    else
    {
      this.RowHeight.Text = (-minHeight).ToString();
      this.ExactHt.Checked = true;
    }
    this.HeightLabel.Enabled = !flag;
    this.RowHeight.Enabled = !flag;
  }
}
