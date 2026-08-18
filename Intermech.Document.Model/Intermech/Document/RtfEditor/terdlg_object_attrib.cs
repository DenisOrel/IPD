// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_object_attrib
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_object_attrib : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private Button FillColor;
  private CheckBox FillXparent;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private GroupBox groupBox3;
  private GroupBox groupBox4;
  private GroupBox groupBox5;
  private GroupBox groupBox6;
  private Label label1;
  private Label label2;
  private Button LineColor;
  private RadioButton LineDotted;
  private CheckBox LineDraw;
  private RadioButton LineSolid;
  private TextBox LineThick;
  private RadioButton NoWrap;
  private Button OK;
  private RadioButton WrapAround;
  private RadioButton WrapThru;
  private TextBox ZOrder;

  internal terdlg_object_attrib(ImRtfEditor parent)
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

  private void FillColor_Click(object sender, EventArgs ev)
  {
    this.e.DlgColor2 = this.ctl.DlgEditColor((Control) this, this.e.DlgColor2, true);
  }

  private void FillXparent_Click(object sender, EventArgs ev)
  {
    this.FillColor.Enabled = !this.FillXparent.Checked;
  }

  private void InitializeComponent()
  {
    this.OK = new Button();
    this.Cancel = new Button();
    this.groupBox1 = new GroupBox();
    this.groupBox6 = new GroupBox();
    this.ZOrder = new TextBox();
    this.label2 = new Label();
    this.groupBox5 = new GroupBox();
    this.WrapAround = new RadioButton();
    this.NoWrap = new RadioButton();
    this.WrapThru = new RadioButton();
    this.groupBox4 = new GroupBox();
    this.FillColor = new Button();
    this.FillXparent = new CheckBox();
    this.groupBox2 = new GroupBox();
    this.groupBox3 = new GroupBox();
    this.LineDotted = new RadioButton();
    this.LineSolid = new RadioButton();
    this.LineColor = new Button();
    this.LineDraw = new CheckBox();
    this.LineThick = new TextBox();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.groupBox6.SuspendLayout();
    this.groupBox5.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(216, 216);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(304, 216);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[4]
    {
      (Control) this.groupBox6,
      (Control) this.groupBox5,
      (Control) this.groupBox4,
      (Control) this.groupBox2
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(376, 208 /*0xD0*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox6.Controls.AddRange(new Control[2]
    {
      (Control) this.ZOrder,
      (Control) this.label2
    });
    this.groupBox6.Location = new Point(184, 152);
    this.groupBox6.Name = "groupBox6";
    this.groupBox6.Size = new Size(184, 48 /*0x30*/);
    this.groupBox6.TabIndex = 3;
    this.groupBox6.TabStop = false;
    this.ZOrder.Location = new Point(80 /*0x50*/, 16 /*0x10*/);
    this.ZOrder.Name = "ZOrder";
    this.ZOrder.Size = new Size(56, 20);
    this.ZOrder.TabIndex = 1;
    this.ZOrder.Text = "";
    this.label2.Location = new Point(8, 16 /*0x10*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(56, 16 /*0x10*/);
    this.label2.TabIndex = 0;
    this.label2.Text = "Z Order";
    this.groupBox5.Controls.AddRange(new Control[3]
    {
      (Control) this.WrapAround,
      (Control) this.NoWrap,
      (Control) this.WrapThru
    });
    this.groupBox5.Location = new Point(184, 72);
    this.groupBox5.Name = "groupBox5";
    this.groupBox5.Size = new Size(184, 72);
    this.groupBox5.TabIndex = 2;
    this.groupBox5.TabStop = false;
    this.groupBox5.Text = "Text Wrapping";
    this.WrapAround.Location = new Point(8, 48 /*0x30*/);
    this.WrapAround.Name = "WrapAround";
    this.WrapAround.Size = new Size(168, 16 /*0x10*/);
    this.WrapAround.TabIndex = 2;
    this.WrapAround.Text = "Wrap Around Object";
    this.NoWrap.Location = new Point(8, 32 /*0x20*/);
    this.NoWrap.Name = "NoWrap";
    this.NoWrap.Size = new Size(168, 16 /*0x10*/);
    this.NoWrap.TabIndex = 1;
    this.NoWrap.Text = "No Wrap";
    this.WrapThru.Location = new Point(8, 16 /*0x10*/);
    this.WrapThru.Name = "WrapThru";
    this.WrapThru.Size = new Size(168, 16 /*0x10*/);
    this.WrapThru.TabIndex = 0;
    this.WrapThru.Text = "Wrap Through";
    this.groupBox4.Controls.AddRange(new Control[2]
    {
      (Control) this.FillColor,
      (Control) this.FillXparent
    });
    this.groupBox4.Location = new Point(184, 16 /*0x10*/);
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.Size = new Size(184, 48 /*0x30*/);
    this.groupBox4.TabIndex = 1;
    this.groupBox4.TabStop = false;
    this.groupBox4.Text = "Fill Attributes";
    this.FillColor.Location = new Point(112 /*0x70*/, 16 /*0x10*/);
    this.FillColor.Name = "FillColor";
    this.FillColor.Size = new Size(64 /*0x40*/, 24);
    this.FillColor.TabIndex = 1;
    this.FillColor.Text = "Color...";
    this.FillColor.Click += new EventHandler(this.FillColor_Click);
    this.FillXparent.Location = new Point(8, 16 /*0x10*/);
    this.FillXparent.Name = "FillXparent";
    this.FillXparent.Size = new Size(96 /*0x60*/, 24);
    this.FillXparent.TabIndex = 0;
    this.FillXparent.Text = "Transparent";
    this.FillXparent.Click += new EventHandler(this.FillXparent_Click);
    this.groupBox2.Controls.AddRange(new Control[5]
    {
      (Control) this.groupBox3,
      (Control) this.LineColor,
      (Control) this.LineDraw,
      (Control) this.LineThick,
      (Control) this.label1
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(168, 184);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Line Attributes";
    this.groupBox3.Controls.AddRange(new Control[2]
    {
      (Control) this.LineDotted,
      (Control) this.LineSolid
    });
    this.groupBox3.Location = new Point(8, 120);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(152, 56);
    this.groupBox3.TabIndex = 4;
    this.groupBox3.TabStop = false;
    this.groupBox3.Text = "LineType";
    this.LineDotted.Location = new Point(8, 32 /*0x20*/);
    this.LineDotted.Name = "LineDotted";
    this.LineDotted.Size = new Size(136, 16 /*0x10*/);
    this.LineDotted.TabIndex = 1;
    this.LineDotted.Text = "Dotted Line";
    this.LineSolid.Location = new Point(8, 16 /*0x10*/);
    this.LineSolid.Name = "LineSolid";
    this.LineSolid.Size = new Size(136, 16 /*0x10*/);
    this.LineSolid.TabIndex = 0;
    this.LineSolid.Text = "Solid Line";
    this.LineColor.Location = new Point(8, 88);
    this.LineColor.Name = "LineColor";
    this.LineColor.Size = new Size(152, 24);
    this.LineColor.TabIndex = 3;
    this.LineColor.Text = "Line Color...";
    this.LineColor.Click += new EventHandler(this.LineColor_Click);
    this.LineDraw.Location = new Point(8, 16 /*0x10*/);
    this.LineDraw.Name = "LineDraw";
    this.LineDraw.Size = new Size(144 /*0x90*/, 16 /*0x10*/);
    this.LineDraw.TabIndex = 2;
    this.LineDraw.Text = "Draw Line or Border";
    this.LineDraw.Click += new EventHandler(this.LineDraw_Click);
    this.LineThick.Location = new Point(112 /*0x70*/, 56);
    this.LineThick.Name = "LineThick";
    this.LineThick.Size = new Size(48 /*0x30*/, 20);
    this.LineThick.TabIndex = 1;
    this.LineThick.Text = "";
    this.label1.Location = new Point(8, 56);
    this.label1.Name = "label1";
    this.label1.Size = new Size(104, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Thickness (twips)";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(392, 245);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_object_attrib);
    this.Text = "Drawing Object Parameters";
    this.Load += new EventHandler(this.terdlg_object_attrib_Load);
    this.Activated += new EventHandler(this.terdlg_object_attrib_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox6.ResumeLayout(false);
    this.groupBox5.ResumeLayout(false);
    this.groupBox4.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void LineColor_Click(object sender, EventArgs ev)
  {
    this.e.DlgColor1 = this.ctl.DlgEditColor((Control) this, this.e.DlgColor1, true);
  }

  private void LineDraw_Click(object sender, EventArgs ev)
  {
    bool flag = this.LineDraw.Checked;
    this.LineSolid.Enabled = flag;
    this.LineDotted.Enabled = flag;
    this.LineColor.Enabled = flag;
    this.LineThick.Enabled = flag;
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgInt1 = this.ctl.ToInt(this.LineThick);
    this.e.DlgInt4 = this.ctl.ToInt(this.ZOrder);
    this.e.DlgInt2 = 0;
    if (this.LineDraw.Checked)
      this.e.DlgInt2 = !this.LineSolid.Checked ? 2 : 1;
    this.e.DlgBool1 = !this.FillXparent.Checked;
    this.e.DlgInt5 = 0;
    if (this.WrapThru.Checked)
      this.e.DlgInt5 = 16384 /*0x4000*/;
    if (!this.NoWrap.Checked)
      return;
    this.e.DlgInt5 = 8192 /*0x2000*/;
  }

  private void terdlg_object_attrib_Activated(object sender, EventArgs e) => this.LineThick.Focus();

  private void terdlg_object_attrib_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    int dlgInt1 = this.e.DlgInt1;
    int flags = this.e.ParaFrame[dlgInt1].flags;
    this.LineThick.Text = this.e.ParaFrame[dlgInt1].LineWdth.ToString();
    this.ZOrder.Text = this.e.ParaFrame[dlgInt1].ZOrder.ToString();
    if (this.ctl.True(flags & 1024 /*0x0400*/))
    {
      this.LineDraw.Checked = true;
      this.LineSolid.Checked = this.ctl.False(flags & 2048 /*0x0800*/);
      this.LineDotted.Checked = this.ctl.True(flags & 2048 /*0x0800*/);
    }
    else
    {
      this.LineSolid.Checked = true;
      this.LineSolid.Enabled = false;
      this.LineDotted.Enabled = false;
      this.LineColor.Enabled = false;
      this.LineThick.Enabled = false;
    }
    this.FillXparent.Checked = this.e.ParaFrame[dlgInt1].FillPattern == 0;
    if (this.e.ParaFrame[dlgInt1].FillPattern == 0)
      this.FillColor.Enabled = false;
    if (this.ctl.True(flags & 640))
    {
      this.WrapThru.Checked = this.ctl.True(flags & 16384 /*0x4000*/);
      this.NoWrap.Checked = this.ctl.True(flags & 8192 /*0x2000*/);
      this.WrapAround.Checked = this.ctl.False(flags & 24576 /*0x6000*/);
    }
    if (this.ctl.True(flags & 256 /*0x0100*/))
    {
      this.FillXparent.Enabled = false;
      this.FillColor.Enabled = false;
    }
    this.e.DlgColor1 = this.e.ParaFrame[dlgInt1].LineColor;
    this.e.DlgColor2 = this.e.ParaFrame[dlgInt1].BackColor;
  }
}
