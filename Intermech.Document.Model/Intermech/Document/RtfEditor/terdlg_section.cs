// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_section
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_section : Form
{
  private int BinCount;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private int CustomPaperIdx;
  private ImRtfEditor e;
  private ListBox FirstBin;
  private RadioButton FlowDef;
  private RadioButton FlowLtr;
  private RadioButton FlowRtl;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private GroupBox groupBox3;
  private GroupBox groupBox4;
  private GroupBox groupBox5;
  private GroupBox groupBox6;
  private GroupBox groupBox7;
  private Label label1;
  private Label label2;
  private Label label3;
  private RadioButton Landscape;
  private bool metric;
  private ListBox NextBin;
  private Button OK;
  private TextBox PageNo;
  private ListBox Paper;
  private TextBox PaperHeight;
  private Label PaperHeightLabel;
  private TextBox PaperWidth;
  private Label PaperWidthLabel;
  private RadioButton Portrait;
  private CheckBox RestartPageNo;
  private TextBox SecColSpace;
  private TextBox SecColumns;
  private CheckBox SecNewPage;
  private int sect;
  private Label Units;

  internal terdlg_section(ImRtfEditor parent)
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
    this.groupBox7 = new GroupBox();
    this.NextBin = new ListBox();
    this.FirstBin = new ListBox();
    this.label3 = new Label();
    this.label2 = new Label();
    this.groupBox6 = new GroupBox();
    this.PaperHeight = new TextBox();
    this.PaperWidth = new TextBox();
    this.PaperHeightLabel = new Label();
    this.PaperWidthLabel = new Label();
    this.Paper = new ListBox();
    this.groupBox5 = new GroupBox();
    this.FlowLtr = new RadioButton();
    this.FlowRtl = new RadioButton();
    this.FlowDef = new RadioButton();
    this.groupBox4 = new GroupBox();
    this.PageNo = new TextBox();
    this.RestartPageNo = new CheckBox();
    this.SecNewPage = new CheckBox();
    this.groupBox3 = new GroupBox();
    this.Landscape = new RadioButton();
    this.Portrait = new RadioButton();
    this.groupBox2 = new GroupBox();
    this.SecColSpace = new TextBox();
    this.SecColumns = new TextBox();
    this.Units = new Label();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.groupBox7.SuspendLayout();
    this.groupBox6.SuspendLayout();
    this.groupBox5.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(168, 392);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(256 /*0x0100*/, 392);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[6]
    {
      (Control) this.groupBox7,
      (Control) this.groupBox6,
      (Control) this.groupBox5,
      (Control) this.groupBox4,
      (Control) this.groupBox3,
      (Control) this.groupBox2
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(336, 384);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox7.Controls.AddRange(new Control[4]
    {
      (Control) this.NextBin,
      (Control) this.FirstBin,
      (Control) this.label3,
      (Control) this.label2
    });
    this.groupBox7.Location = new Point(8, 280);
    this.groupBox7.Name = "groupBox7";
    this.groupBox7.Size = new Size(320, 96 /*0x60*/);
    this.groupBox7.TabIndex = 5;
    this.groupBox7.TabStop = false;
    this.groupBox7.Text = "Paper Source";
    this.NextBin.Location = new Point(168, 32 /*0x20*/);
    this.NextBin.Name = "NextBin";
    this.NextBin.Size = new Size(136, 56);
    this.NextBin.TabIndex = 3;
    this.FirstBin.Location = new Point(8, 32 /*0x20*/);
    this.FirstBin.Name = "FirstBin";
    this.FirstBin.Size = new Size(136, 56);
    this.FirstBin.TabIndex = 2;
    this.label3.Location = new Point(168, 16 /*0x10*/);
    this.label3.Name = "label3";
    this.label3.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.label3.TabIndex = 1;
    this.label3.Text = "Other Page Bin";
    this.label2.Location = new Point(8, 16 /*0x10*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.label2.TabIndex = 0;
    this.label2.Text = "First Page Bin";
    this.groupBox6.Controls.AddRange(new Control[5]
    {
      (Control) this.PaperHeight,
      (Control) this.PaperWidth,
      (Control) this.PaperHeightLabel,
      (Control) this.PaperWidthLabel,
      (Control) this.Paper
    });
    this.groupBox6.Location = new Point(8, 192 /*0xC0*/);
    this.groupBox6.Name = "groupBox6";
    this.groupBox6.Size = new Size(320, 80 /*0x50*/);
    this.groupBox6.TabIndex = 4;
    this.groupBox6.TabStop = false;
    this.groupBox6.Text = "Paper Size";
    this.PaperHeight.Location = new Point(248, 46);
    this.PaperHeight.Name = "PaperHeight";
    this.PaperHeight.Size = new Size(48 /*0x30*/, 20);
    this.PaperHeight.TabIndex = 4;
    this.PaperHeight.Text = "";
    this.PaperWidth.Location = new Point(248, 14);
    this.PaperWidth.Name = "PaperWidth";
    this.PaperWidth.Size = new Size(48 /*0x30*/, 20);
    this.PaperWidth.TabIndex = 3;
    this.PaperWidth.Text = "";
    this.PaperHeightLabel.Location = new Point(160 /*0xA0*/, 48 /*0x30*/);
    this.PaperHeightLabel.Name = "PaperHeightLabel";
    this.PaperHeightLabel.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.PaperHeightLabel.TabIndex = 2;
    this.PaperHeightLabel.Text = "Height (inches)";
    this.PaperWidthLabel.Location = new Point(160 /*0xA0*/, 16 /*0x10*/);
    this.PaperWidthLabel.Name = "PaperWidthLabel";
    this.PaperWidthLabel.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.PaperWidthLabel.TabIndex = 1;
    this.PaperWidthLabel.Text = "Width (inches)";
    this.Paper.Location = new Point(8, 16 /*0x10*/);
    this.Paper.Name = "Paper";
    this.Paper.Size = new Size(120, 56);
    this.Paper.TabIndex = 0;
    this.Paper.SelectedIndexChanged += new EventHandler(this.Paper_SelectedIndexChanged);
    this.groupBox5.Controls.AddRange(new Control[3]
    {
      (Control) this.FlowLtr,
      (Control) this.FlowRtl,
      (Control) this.FlowDef
    });
    this.groupBox5.Location = new Point(200, 104);
    this.groupBox5.Name = "groupBox5";
    this.groupBox5.Size = new Size(128 /*0x80*/, 80 /*0x50*/);
    this.groupBox5.TabIndex = 3;
    this.groupBox5.TabStop = false;
    this.groupBox5.Text = "Text Flow";
    this.FlowLtr.Location = new Point(8, 56);
    this.FlowLtr.Name = "FlowLtr";
    this.FlowLtr.Size = new Size(104, 16 /*0x10*/);
    this.FlowLtr.TabIndex = 2;
    this.FlowLtr.Text = "Left-to-Right";
    this.FlowRtl.Location = new Point(8, 40);
    this.FlowRtl.Name = "FlowRtl";
    this.FlowRtl.Size = new Size(104, 16 /*0x10*/);
    this.FlowRtl.TabIndex = 1;
    this.FlowRtl.Text = "Right-to-Left";
    this.FlowDef.Location = new Point(8, 24);
    this.FlowDef.Name = "FlowDef";
    this.FlowDef.Size = new Size(104, 16 /*0x10*/);
    this.FlowDef.TabIndex = 0;
    this.FlowDef.Text = "Default";
    this.groupBox4.Controls.AddRange(new Control[3]
    {
      (Control) this.PageNo,
      (Control) this.RestartPageNo,
      (Control) this.SecNewPage
    });
    this.groupBox4.Location = new Point(8, 104);
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.Size = new Size(184, 80 /*0x50*/);
    this.groupBox4.TabIndex = 2;
    this.groupBox4.TabStop = false;
    this.PageNo.Location = new Point(144 /*0x90*/, 46);
    this.PageNo.Name = "PageNo";
    this.PageNo.Size = new Size(32 /*0x20*/, 20);
    this.PageNo.TabIndex = 2;
    this.PageNo.Text = "";
    this.RestartPageNo.Location = new Point(8, 48 /*0x30*/);
    this.RestartPageNo.Name = "RestartPageNo";
    this.RestartPageNo.Size = new Size(152, 16 /*0x10*/);
    this.RestartPageNo.TabIndex = 1;
    this.RestartPageNo.Text = "Restart Page Number at";
    this.RestartPageNo.CheckStateChanged += new EventHandler(this.RestartPageNo_CheckStateChanged);
    this.SecNewPage.Location = new Point(8, 24);
    this.SecNewPage.Name = "SecNewPage";
    this.SecNewPage.Size = new Size(168, 16 /*0x10*/);
    this.SecNewPage.TabIndex = 0;
    this.SecNewPage.Text = "Start Section on New Page";
    this.groupBox3.Controls.AddRange(new Control[2]
    {
      (Control) this.Landscape,
      (Control) this.Portrait
    });
    this.groupBox3.Location = new Point(200, 16 /*0x10*/);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(128 /*0x80*/, 80 /*0x50*/);
    this.groupBox3.TabIndex = 1;
    this.groupBox3.TabStop = false;
    this.groupBox3.Text = "Orientation";
    this.Landscape.Location = new Point(8, 48 /*0x30*/);
    this.Landscape.Name = "Landscape";
    this.Landscape.Size = new Size(104, 16 /*0x10*/);
    this.Landscape.TabIndex = 1;
    this.Landscape.Text = "Landscape";
    this.Portrait.Location = new Point(8, 24);
    this.Portrait.Name = "Portrait";
    this.Portrait.Size = new Size(104, 16 /*0x10*/);
    this.Portrait.TabIndex = 0;
    this.Portrait.Text = "Portrait";
    this.groupBox2.Controls.AddRange(new Control[4]
    {
      (Control) this.SecColSpace,
      (Control) this.SecColumns,
      (Control) this.Units,
      (Control) this.label1
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(184, 80 /*0x50*/);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Column";
    this.SecColSpace.Location = new Point(128 /*0x80*/, 40);
    this.SecColSpace.Name = "SecColSpace";
    this.SecColSpace.Size = new Size(40, 20);
    this.SecColSpace.TabIndex = 4;
    this.SecColSpace.Text = "";
    this.SecColumns.Location = new Point(128 /*0x80*/, 14);
    this.SecColumns.Name = "SecColumns";
    this.SecColumns.Size = new Size(40, 20);
    this.SecColumns.TabIndex = 3;
    this.SecColumns.Text = "";
    this.Units.Location = new Point(8, 40);
    this.Units.Name = "Units";
    this.Units.Size = new Size(120, 32 /*0x20*/);
    this.Units.TabIndex = 2;
    this.Units.Text = "Space Between the Columns (inches)";
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(120, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Number of Columns";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(352, 421);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_section);
    this.Text = "title";
    this.Load += new EventHandler(this.terdlg_section_Load);
    this.Activated += new EventHandler(this.terdlg_section_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox7.ResumeLayout(false);
    this.groupBox6.ResumeLayout(false);
    this.groupBox5.ResumeLayout(false);
    this.groupBox4.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    if (this.e.TerArg.PrintView)
    {
      if (!this.ctl.CheckDlgValue((Form) this, 'I', this.SecColumns, 1.0, 4.0) || !this.ctl.CheckDlgValue((Form) this, 'F', this.SecColSpace, 0.1, 4.0))
        return;
      this.e.TerSect[this.sect].columns = this.ctl.ToInt(this.SecColumns);
      double x = this.ctl.ToDouble(this.SecColSpace);
      this.e.TerSect[this.sect].ColumnSpace = !this.metric ? (float) x : this.ctl.CmToInches((float) x);
      this.e.TerSect[this.sect].IsPortrait = this.Portrait.Checked;
      int num = 0;
      if (this.FlowRtl.Checked)
        num = 2;
      else if (this.FlowLtr.Checked)
        num = 1;
      this.e.TerSect[this.sect].flow = num;
    }
    if (this.SecNewPage.Checked)
      this.e.TerSect[this.sect].flags |= 1;
    else
      this.e.TerSect[this.sect].flags = tc.ResetUintFlag(ref this.e.TerSect[this.sect].flags, 1);
    if (this.RestartPageNo.Checked)
    {
      this.e.TerSect[this.sect].flags |= 2;
      this.e.TerSect[this.sect].FirstPageNo = (short) this.ctl.ToInt(this.PageNo);
      if (this.e.TerSect[this.sect].FirstPageNo < (short) 1)
        this.e.TerSect[this.sect].FirstPageNo = (short) 1;
    }
    else
      this.e.TerSect[this.sect].flags = tc.ResetUintFlag(ref this.e.TerSect[this.sect].flags, 2);
    if (this.Paper.SelectedIndex == this.CustomPaperIdx)
    {
      this.e.TerSect[this.sect].PprKind = PaperKind.Custom;
      double x1 = this.ctl.ToDouble(this.PaperWidth);
      this.e.TerSect[this.sect].PprWidth = !this.metric ? (float) x1 : this.ctl.CmToInches((float) x1);
      double x2 = this.ctl.ToDouble(this.PaperHeight);
      this.e.TerSect[this.sect].PprHeight = !this.metric ? (float) x2 : this.ctl.CmToInches((float) x2);
    }
    else
    {
      PaperSize objectValue = (PaperSize) ((tc.ClsBox) this.Paper.SelectedItem).ObjectValue;
      this.e.TerSect[this.sect].PprKind = objectValue.Kind;
      this.e.TerSect[this.sect].PprWidth = (float) objectValue.Width / 100f;
      this.e.TerSect[this.sect].PprHeight = (float) objectValue.Height / 100f;
    }
    if (this.FirstBin.Enabled)
    {
      this.e.TerSect[this.sect].FirstPageBin = (PaperSourceKind) ((tc.ClsBox) this.FirstBin.SelectedItem).ObjectValue;
      this.e.TerSect[this.sect].bin = (PaperSourceKind) ((tc.ClsBox) this.NextBin.SelectedItem).ObjectValue;
    }
    this.DialogResult = DialogResult.OK;
  }

  private void Paper_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.SetPaperDim(this.Paper.SelectedIndex);
  }

  private void RestartPageNo_CheckStateChanged(object sender, EventArgs e)
  {
    this.PageNo.Enabled = this.RestartPageNo.Checked;
  }

  private void SetPaperDim(int PaperSizeIdx)
  {
    float x1;
    float x2;
    if (PaperSizeIdx == this.CustomPaperIdx)
    {
      x1 = this.e.TerSect[this.sect].PprWidth;
      x2 = this.e.TerSect[this.sect].PprHeight;
    }
    else
    {
      PaperSize objectValue = (PaperSize) ((tc.ClsBox) this.Paper.SelectedItem).ObjectValue;
      x1 = (float) objectValue.Width / 100f;
      x2 = (float) objectValue.Height / 100f;
    }
    if (this.metric)
      this.PaperWidth.Text = $"{(double) this.ctl.InchesToCm(x1):f2}";
    else
      this.PaperWidth.Text = $"{(double) x1:f2}";
    if (this.metric)
      this.PaperHeight.Text = $"{(double) this.ctl.InchesToCm(x2):f2}";
    else
      this.PaperHeight.Text = $"{(double) x2:f2}";
    this.PaperWidth.Enabled = PaperSizeIdx == this.CustomPaperIdx;
    this.PaperHeight.Enabled = PaperSizeIdx == this.CustomPaperIdx;
  }

  private void terdlg_section_Activated(object sender, EventArgs e) => this.SecColumns.Focus();

  private void terdlg_section_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.metric = this.ctl.True(this.e.TerFlags & 2);
    this.sect = this.ctl.GetSection(this.e.CurLine);
    this.SecNewPage.Checked = this.ctl.True(this.e.TerSect[this.sect].flags & 1);
    if (this.ctl.True(this.e.TerSect[this.sect].flags & 2))
    {
      this.RestartPageNo.Checked = true;
      this.PageNo.Text = this.e.TerSect[this.sect].FirstPageNo.ToString();
    }
    else
      this.PageNo.Enabled = false;
    if (this.metric)
    {
      this.Units.Text = this.e.MsgString[173];
      this.PaperWidthLabel.Text = "Width (Cm)";
      this.PaperHeightLabel.Text = "Height (Cm)";
    }
    if (!this.e.TerArg.PrintView)
    {
      this.SecColumns.Enabled = false;
      this.SecColSpace.Enabled = false;
      this.Portrait.Enabled = false;
      this.Landscape.Enabled = false;
      this.FlowDef.Enabled = false;
      this.FlowRtl.Enabled = false;
      this.FlowLtr.Enabled = false;
    }
    else
    {
      this.SecColumns.Text = this.e.TerSect[this.sect].columns.ToString();
      if (this.metric)
        this.SecColSpace.Text = $"{(double) this.ctl.InchesToCm(this.e.TerSect[this.sect].ColumnSpace):f2}";
      else
        this.SecColSpace.Text = $"{(double) this.ctl.e.TerSect[this.sect].ColumnSpace:f2}";
      if (this.e.TerSect[this.sect].IsPortrait)
        this.Portrait.Checked = true;
      else
        this.Landscape.Checked = true;
    }
    PrinterSettings printerSettings = new PrinterSettings();
    PrinterSettings.PaperSizeCollection paperSizes = printerSettings.PaperSizes;
    int num1 = 0;
    int PaperSizeIdx = -1;
    this.CustomPaperIdx = -1;
    foreach (PaperSize ArgValue in paperSizes)
    {
      this.Paper.Items.Add((object) new tc.ClsBox(ArgValue.PaperName, (object) ArgValue));
      if (this.e.TerSect[this.sect].PprKind == ArgValue.Kind)
        PaperSizeIdx = num1;
      if (ArgValue.Kind == PaperKind.Custom)
        this.CustomPaperIdx = num1;
      ++num1;
    }
    if (this.CustomPaperIdx == -1)
    {
      this.Paper.Items.Add((object) new tc.ClsBox("Custom", (object) PaperKind.Custom));
      this.CustomPaperIdx = num1;
      int num2 = num1 + 1;
    }
    if (PaperSizeIdx == -1)
      PaperSizeIdx = this.CustomPaperIdx;
    this.Paper.SelectedIndex = PaperSizeIdx;
    this.SetPaperDim(PaperSizeIdx);
    PrinterSettings.PaperSourceCollection paperSources = printerSettings.PaperSources;
    this.BinCount = 0;
    int num3;
    int num4 = num3 = 0;
    foreach (PaperSource paperSource in paperSources)
    {
      this.FirstBin.Items.Add((object) new tc.ClsBox(paperSource.SourceName, (object) paperSource.Kind));
      this.NextBin.Items.Add((object) new tc.ClsBox(paperSource.SourceName, (object) paperSource.Kind));
      if (this.e.TerSect[this.sect].FirstPageBin == paperSource.Kind)
        num4 = this.BinCount;
      if (this.e.TerSect[this.sect].bin == paperSource.Kind)
        num3 = this.BinCount;
      ++this.BinCount;
    }
    this.FirstBin.SelectedIndex = num4;
    this.NextBin.SelectedIndex = num3;
    if (this.BinCount == 0)
    {
      this.FirstBin.Enabled = false;
      this.NextBin.Enabled = false;
    }
    switch (this.e.TerSect[this.sect].flow)
    {
      case 1:
        this.FlowLtr.Checked = true;
        break;
      case 2:
        this.FlowRtl.Checked = true;
        break;
      default:
        this.FlowDef.Checked = true;
        break;
    }
  }
}
