// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_edit_pict
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_edit_pict : Form
{
  private RadioButton AlignBot;
  private RadioButton AlignMiddle;
  private RadioButton AlignOffset;
  private RadioButton AlignTop;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Label label1;
  private Label label2;
  private Label label3;
  private bool metric;
  private TextBox OffsetVal;
  private Button OK;
  private int pict;
  private TextBox PictHeight;
  private GroupBox PictSizeLbl;
  private TextBox PictWidth;

  internal terdlg_edit_pict(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void AlignClick(object sender, EventArgs e)
  {
    this.OffsetVal.Enabled = this.AlignOffset.Checked;
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
    this.label3 = new Label();
    this.OffsetVal = new TextBox();
    this.AlignOffset = new RadioButton();
    this.AlignBot = new RadioButton();
    this.AlignMiddle = new RadioButton();
    this.AlignTop = new RadioButton();
    this.PictSizeLbl = new GroupBox();
    this.PictWidth = new TextBox();
    this.PictHeight = new TextBox();
    this.label2 = new Label();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.PictSizeLbl.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(120, 144 /*0x90*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(208 /*0xD0*/, 144 /*0x90*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.groupBox2,
      (Control) this.PictSizeLbl
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(280, 136);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox2.Controls.AddRange(new Control[6]
    {
      (Control) this.label3,
      (Control) this.OffsetVal,
      (Control) this.AlignOffset,
      (Control) this.AlignBot,
      (Control) this.AlignMiddle,
      (Control) this.AlignTop
    });
    this.groupBox2.Location = new Point(144 /*0x90*/, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(128 /*0x80*/, 112 /*0x70*/);
    this.groupBox2.TabIndex = 1;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Alignment";
    this.label3.Location = new Point(80 /*0x50*/, 90);
    this.label3.Name = "label3";
    this.label3.Size = new Size(40, 16 /*0x10*/);
    this.label3.TabIndex = 5;
    this.label3.Text = "Twips";
    this.OffsetVal.Location = new Point(24, 88);
    this.OffsetVal.Name = "OffsetVal";
    this.OffsetVal.Size = new Size(48 /*0x30*/, 20);
    this.OffsetVal.TabIndex = 4;
    this.OffsetVal.Text = "";
    this.AlignOffset.Location = new Point(8, 72);
    this.AlignOffset.Name = "AlignOffset";
    this.AlignOffset.Size = new Size(104, 16 /*0x10*/);
    this.AlignOffset.TabIndex = 3;
    this.AlignOffset.Text = "Baseline Offset";
    this.AlignOffset.Click += new EventHandler(this.AlignClick);
    this.AlignBot.Location = new Point(8, 48 /*0x30*/);
    this.AlignBot.Name = "AlignBot";
    this.AlignBot.Size = new Size(88, 16 /*0x10*/);
    this.AlignBot.TabIndex = 2;
    this.AlignBot.Text = "Bottom";
    this.AlignBot.Click += new EventHandler(this.AlignClick);
    this.AlignMiddle.Location = new Point(8, 32 /*0x20*/);
    this.AlignMiddle.Name = "AlignMiddle";
    this.AlignMiddle.Size = new Size(88, 16 /*0x10*/);
    this.AlignMiddle.TabIndex = 1;
    this.AlignMiddle.Text = "Middle";
    this.AlignMiddle.Click += new EventHandler(this.AlignClick);
    this.AlignTop.Location = new Point(8, 16 /*0x10*/);
    this.AlignTop.Name = "AlignTop";
    this.AlignTop.Size = new Size(88, 16 /*0x10*/);
    this.AlignTop.TabIndex = 0;
    this.AlignTop.Text = "Top";
    this.AlignTop.Click += new EventHandler(this.AlignClick);
    this.PictSizeLbl.Controls.AddRange(new Control[4]
    {
      (Control) this.PictWidth,
      (Control) this.PictHeight,
      (Control) this.label2,
      (Control) this.label1
    });
    this.PictSizeLbl.Location = new Point(8, 16 /*0x10*/);
    this.PictSizeLbl.Name = "PictSizeLbl";
    this.PictSizeLbl.Size = new Size(128 /*0x80*/, 112 /*0x70*/);
    this.PictSizeLbl.TabIndex = 0;
    this.PictSizeLbl.TabStop = false;
    this.PictSizeLbl.Text = "Picture Size (inches)";
    this.PictWidth.Location = new Point(56, 64 /*0x40*/);
    this.PictWidth.Name = "PictWidth";
    this.PictWidth.Size = new Size(48 /*0x30*/, 20);
    this.PictWidth.TabIndex = 3;
    this.PictWidth.Text = "";
    this.PictHeight.Location = new Point(56, 30);
    this.PictHeight.Name = "PictHeight";
    this.PictHeight.Size = new Size(48 /*0x30*/, 20);
    this.PictHeight.TabIndex = 2;
    this.PictHeight.Text = "";
    this.label2.Location = new Point(8, 66);
    this.label2.Name = "label2";
    this.label2.Size = new Size(48 /*0x30*/, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "Width";
    this.label1.Location = new Point(8, 32 /*0x20*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(48 /*0x30*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Height";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(296, 173);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_edit_pict);
    this.Text = "Edit Picture Parameters";
    this.Load += new EventHandler(this.terdlg_edit_pict_Load);
    this.Activated += new EventHandler(this.terdlg_edit_pict_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.PictSizeLbl.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    int EndRange = this.metric ? 40 : 15;
    if (!this.ctl.CheckDlgValue((Form) this, 'F', this.PictWidth, 0.1, (double) EndRange) || !this.ctl.CheckDlgValue((Form) this, 'F', this.PictHeight, 0.1, (double) EndRange))
      return;
    double x1 = this.ctl.ToDouble(this.PictWidth);
    this.e.TerFont[this.pict].PictWidth = this.metric ? this.ctl.CmToTwips(x1) : (int) this.ctl.InchesToTwips(x1);
    double x2 = this.ctl.ToDouble(this.PictHeight);
    this.e.TerFont[this.pict].PictHeight = this.metric ? this.ctl.CmToTwips(x2) : (int) this.ctl.InchesToTwips(x2);
    int num = 0;
    if (this.AlignOffset.Checked)
    {
      num = 0;
      this.e.TerFont[this.pict].offset = this.ctl.ToInt(this.OffsetVal);
    }
    else
    {
      this.e.TerFont[this.pict].offset = 0;
      if (this.AlignTop.Checked)
        num = 2;
      if (this.AlignBot.Checked)
        num = 0;
      if (this.AlignMiddle.Checked)
        num = 1;
    }
    this.e.TerFont[this.pict].PictAlign = num;
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_edit_pict_Activated(object sender, EventArgs e) => this.PictHeight.Focus();

  private void terdlg_edit_pict_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.metric = this.ctl.True(this.e.TerFlags & 2);
    this.pict = this.ctl.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (this.metric)
    {
      this.PictSizeLbl.Text = this.e.MsgString[180];
      this.PictWidth.Text = $"{this.ctl.TwipsToCm(this.e.TerFont[this.pict].PictWidth):f2}";
      this.PictHeight.Text = $"{this.ctl.TwipsToCm(this.e.TerFont[this.pict].PictHeight):f2}";
    }
    else
    {
      this.PictWidth.Text = $"{this.ctl.TwipsToInches(this.e.TerFont[this.pict].PictWidth):f2}";
      this.PictHeight.Text = $"{this.ctl.TwipsToInches(this.e.TerFont[this.pict].PictHeight):f2}";
    }
    if (this.e.TerFont[this.pict].offset > 0)
    {
      this.AlignOffset.Checked = true;
      this.OffsetVal.Text = this.e.TerFont[this.pict].offset.ToString();
    }
    else
    {
      if (this.e.TerFont[this.pict].PictAlign == 2)
        this.AlignTop.Checked = true;
      if (this.e.TerFont[this.pict].PictAlign == 1)
        this.AlignMiddle.Checked = true;
      if (this.e.TerFont[this.pict].PictAlign == 0)
        this.AlignBot.Checked = true;
      this.OffsetVal.Enabled = false;
    }
  }
}
