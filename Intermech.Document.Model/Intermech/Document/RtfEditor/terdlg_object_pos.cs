// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_object_pos
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_object_pos : Form
{
  private bool BaseMarg;
  private bool BasePage;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private int flags;
  private int FrameId;
  private GroupBox groupBox1;
  private Button OK;
  private RadioButton VertMarg;
  private RadioButton VertPage;
  private RadioButton VertPara;

  internal terdlg_object_pos(ImRtfEditor parent)
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
    this.VertPage = new RadioButton();
    this.VertMarg = new RadioButton();
    this.VertPara = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(8, 104);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(96 /*0x60*/, 104);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[3]
    {
      (Control) this.VertPara,
      (Control) this.VertMarg,
      (Control) this.VertPage
    });
    this.groupBox1.Location = new Point(8, 8);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(168, 88);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Vertical Position Relative to";
    this.VertPage.Location = new Point(16 /*0x10*/, 24);
    this.VertPage.Name = "VertPage";
    this.VertPage.Size = new Size(104, 16 /*0x10*/);
    this.VertPage.TabIndex = 0;
    this.VertPage.Text = "Top of the Page";
    this.VertMarg.Location = new Point(16 /*0x10*/, 44);
    this.VertMarg.Name = "VertMarg";
    this.VertMarg.Size = new Size(104, 16 /*0x10*/);
    this.VertMarg.TabIndex = 1;
    this.VertMarg.Text = "Top Margin";
    this.VertPara.Location = new Point(16 /*0x10*/, 64 /*0x40*/);
    this.VertPara.Name = "VertPara";
    this.VertPara.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.VertPara.TabIndex = 2;
    this.VertPara.Text = "Current Paragraph";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(184, 141);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_object_pos);
    this.Text = "Object Position";
    this.Load += new EventHandler(this.terdlg_object_pos_Load);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.BasePage = this.VertPage.Checked;
    this.BaseMarg = this.VertMarg.Checked;
    this.e.DlgInt2 = 2;
    if (this.BasePage)
      this.e.DlgInt2 = 0;
    if (!this.BaseMarg)
      return;
    this.e.DlgInt2 = 1;
  }

  private void terdlg_object_pos_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.FrameId = this.e.DlgInt1;
    this.flags = this.e.ParaFrame[this.FrameId].flags;
    this.VertPage.Checked = this.ctl.True(this.flags & 32 /*0x20*/);
    this.VertMarg.Checked = this.ctl.True(this.flags & 64 /*0x40*/);
    this.VertPara.Checked = this.ctl.False(this.flags & 96 /*0x60*/);
  }
}
