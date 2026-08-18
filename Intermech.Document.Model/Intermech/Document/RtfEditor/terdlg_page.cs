// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_page
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_page : Form
{
  private CheckBox AllSects;
  private TextBox BotMargin;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private TextBox FtrMargin;
  private GroupBox groupBox1;
  private TextBox HdrMargin;
  private Label label1;
  private Label label2;
  private Label label3;
  private Label label4;
  private Label label5;
  private Label label6;
  private TextBox LeftMargin;
  private bool metric;
  private Button OK;
  private TextBox RightMargin;
  private int sect;
  private TextBox TopMargin;
  private GroupBox Units;

  internal terdlg_page(ImRtfEditor parent)
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
    this.FtrMargin = new TextBox();
    this.HdrMargin = new TextBox();
    this.label6 = new Label();
    this.label5 = new Label();
    this.Units = new GroupBox();
    this.BotMargin = new TextBox();
    this.RightMargin = new TextBox();
    this.TopMargin = new TextBox();
    this.LeftMargin = new TextBox();
    this.label4 = new Label();
    this.label3 = new Label();
    this.label2 = new Label();
    this.label1 = new Label();
    this.AllSects = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.Units.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(88, 184);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(176 /*0xB0*/, 184);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[6]
    {
      (Control) this.AllSects,
      (Control) this.FtrMargin,
      (Control) this.HdrMargin,
      (Control) this.label6,
      (Control) this.label5,
      (Control) this.Units
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(248, 176 /*0xB0*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.FtrMargin.Location = new Point(200, 128 /*0x80*/);
    this.FtrMargin.Name = "FtrMargin";
    this.FtrMargin.Size = new Size(40, 20);
    this.FtrMargin.TabIndex = 4;
    this.FtrMargin.Text = "";
    this.HdrMargin.Location = new Point(200, 104);
    this.HdrMargin.Name = "HdrMargin";
    this.HdrMargin.Size = new Size(40, 20);
    this.HdrMargin.TabIndex = 3;
    this.HdrMargin.Text = "";
    this.label6.Location = new Point(8, 130);
    this.label6.Name = "label6";
    this.label6.Size = new Size(208 /*0xD0*/, 16 /*0x10*/);
    this.label6.TabIndex = 2;
    this.label6.Text = "Footer distance from the Page Bottom";
    this.label5.Location = new Point(8, 106);
    this.label5.Name = "label5";
    this.label5.Size = new Size(184, 16 /*0x10*/);
    this.label5.TabIndex = 1;
    this.label5.Text = "Header distance from the Page Top";
    this.Units.Controls.AddRange(new Control[8]
    {
      (Control) this.BotMargin,
      (Control) this.RightMargin,
      (Control) this.TopMargin,
      (Control) this.LeftMargin,
      (Control) this.label4,
      (Control) this.label3,
      (Control) this.label2,
      (Control) this.label1
    });
    this.Units.Location = new Point(8, 16 /*0x10*/);
    this.Units.Name = "Units";
    this.Units.Size = new Size(232, 80 /*0x50*/);
    this.Units.TabIndex = 0;
    this.Units.TabStop = false;
    this.Units.Text = "Margin (Inches)";
    this.BotMargin.Location = new Point(168, 48 /*0x30*/);
    this.BotMargin.Name = "BotMargin";
    this.BotMargin.Size = new Size(48 /*0x30*/, 20);
    this.BotMargin.TabIndex = 7;
    this.BotMargin.Text = "";
    this.RightMargin.Location = new Point(168, 24);
    this.RightMargin.Name = "RightMargin";
    this.RightMargin.Size = new Size(48 /*0x30*/, 20);
    this.RightMargin.TabIndex = 6;
    this.RightMargin.Text = "";
    this.TopMargin.Location = new Point(48 /*0x30*/, 48 /*0x30*/);
    this.TopMargin.Name = "TopMargin";
    this.TopMargin.Size = new Size(48 /*0x30*/, 20);
    this.TopMargin.TabIndex = 5;
    this.TopMargin.Text = "";
    this.LeftMargin.Location = new Point(48 /*0x30*/, 24);
    this.LeftMargin.Name = "LeftMargin";
    this.LeftMargin.Size = new Size(48 /*0x30*/, 20);
    this.LeftMargin.TabIndex = 4;
    this.LeftMargin.Text = "";
    this.label4.Location = new Point(120, 50);
    this.label4.Name = "label4";
    this.label4.Size = new Size(56, 16 /*0x10*/);
    this.label4.TabIndex = 3;
    this.label4.Text = "Bottom";
    this.label3.Location = new Point(120, 26);
    this.label3.Name = "label3";
    this.label3.Size = new Size(56, 16 /*0x10*/);
    this.label3.TabIndex = 2;
    this.label3.Text = "Right";
    this.label2.Location = new Point(8, 50);
    this.label2.Name = "label2";
    this.label2.Size = new Size(56, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "Top";
    this.label1.Location = new Point(8, 26);
    this.label1.Name = "label1";
    this.label1.Size = new Size(56, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Left";
    this.AllSects.Location = new Point(8, 152);
    this.AllSects.Name = "AllSects";
    this.AllSects.Size = new Size(136, 16 /*0x10*/);
    this.AllSects.TabIndex = 5;
    this.AllSects.Text = "All Sections";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(264, 213);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_page);
    this.Text = "Page Parameters";
    this.Load += new EventHandler(this.terdlg_page_Load);
    this.Activated += new EventHandler(this.terdlg_page_Activated);
    this.groupBox1.ResumeLayout(false);
    this.Units.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    bool flag = this.AllSects.Checked;
    if (!this.ctl.CheckDlgValue((Form) this, 'F', this.LeftMargin, 0.0, 9.0) || !this.ctl.CheckDlgValue((Form) this, 'F', this.RightMargin, 0.0, 9.0) || !this.ctl.CheckDlgValue((Form) this, 'F', this.TopMargin, 0.0, 9.0) || !this.ctl.CheckDlgValue((Form) this, 'F', this.BotMargin, 0.0, 9.0) || !this.ctl.CheckDlgValue((Form) this, 'F', this.HdrMargin, 0.0, 9.0) || !this.ctl.CheckDlgValue((Form) this, 'F', this.FtrMargin, 0.0, 9.0))
      return;
    double num1 = this.ctl.ToDouble(this.LeftMargin, this.metric);
    double num2 = this.ctl.ToDouble(this.RightMargin, this.metric);
    double num3 = this.ctl.ToDouble(this.TopMargin, this.metric);
    double num4 = this.ctl.ToDouble(this.BotMargin, this.metric);
    double num5 = this.ctl.ToDouble(this.HdrMargin, this.metric);
    double num6 = this.ctl.ToDouble(this.FtrMargin, this.metric);
    this.ctl.OpenCurPrinter(false);
    if ((double) this.e.PageWidth <= num1 + num2 + 0.5 || (double) this.e.PageHeight <= num3 + num4 + 0.05000000074505806)
    {
      this.ctl.PrintError(87, "PageParam");
    }
    else
    {
      for (int index = 0; index < this.e.TotalSects; ++index)
      {
        if (flag || index == this.sect)
        {
          this.e.TerSect[index].LeftMargin = (float) num1;
          this.e.TerSect[index].RightMargin = (float) num2;
          this.e.TerSect[index].TopMargin = (float) num3;
          this.e.TerSect[index].BotMargin = (float) num4;
          this.e.TerSect[index].HdrMargin = (float) num5;
          this.e.TerSect[index].FtrMargin = (float) num6;
          if (this.e.TerSect[index].FirstLine < this.e.RepageBeginLine)
            this.e.RepageBeginLine = this.e.TerSect[this.sect].FirstLine;
        }
      }
      ++this.e.TerArg.modified;
      this.DialogResult = DialogResult.OK;
    }
  }

  private void terdlg_page_Activated(object sender, EventArgs e) => this.LeftMargin.Focus();

  private void terdlg_page_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.metric = this.ctl.True(this.e.TerFlags & 2);
    if (this.metric)
      this.Units.Text = this.e.MsgString[189];
    this.sect = this.ctl.GetSection(this.e.CurLine);
    double num1 = this.metric ? (double) this.ctl.InchesToCm(this.e.TerSect[this.sect].LeftMargin) : (double) this.e.TerSect[this.sect].LeftMargin;
    double num2 = this.metric ? (double) this.ctl.InchesToCm(this.e.TerSect[this.sect].RightMargin) : (double) this.e.TerSect[this.sect].RightMargin;
    double num3 = this.metric ? (double) this.ctl.InchesToCm(this.e.TerSect[this.sect].TopMargin) : (double) this.e.TerSect[this.sect].TopMargin;
    double num4 = this.metric ? (double) this.ctl.InchesToCm(this.e.TerSect[this.sect].BotMargin) : (double) this.e.TerSect[this.sect].BotMargin;
    double num5 = this.metric ? (double) this.ctl.InchesToCm(this.e.TerSect[this.sect].HdrMargin) : (double) this.e.TerSect[this.sect].HdrMargin;
    double num6 = this.metric ? (double) this.ctl.InchesToCm(this.e.TerSect[this.sect].FtrMargin) : (double) this.e.TerSect[this.sect].FtrMargin;
    this.LeftMargin.Text = $"{num1:f2}";
    this.RightMargin.Text = $"{num2:f2}";
    this.TopMargin.Text = $"{num3:f2}";
    this.BotMargin.Text = $"{num4:f2}";
    this.HdrMargin.Text = $"{num5:f2}";
    this.FtrMargin.Text = $"{num6:f2}";
  }
}
