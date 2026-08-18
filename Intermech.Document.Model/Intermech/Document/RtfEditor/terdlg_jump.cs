// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_jump
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_jump : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private TextBox JumpTo;
  private Label label1;
  private Button OK;

  internal terdlg_jump(ImRtfEditor parent)
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
    this.label1 = new Label();
    this.JumpTo = new TextBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(24, 56);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(112 /*0x70*/, 56);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.JumpTo,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(184, 48 /*0x30*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.label1.Location = new Point(8, 18);
    this.label1.Name = "label1";
    this.label1.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Jump to Line Number";
    this.JumpTo.Location = new Point(120, 16 /*0x10*/);
    this.JumpTo.Name = "JumpTo";
    this.JumpTo.Size = new Size(48 /*0x30*/, 20);
    this.JumpTo.TabIndex = 1;
    this.JumpTo.Text = "";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(200, 85);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_jump);
    this.Text = "Jump";
    this.Load += new EventHandler(this.terdlg_jump_Load);
    this.Activated += new EventHandler(this.terdlg_jump_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    if (!this.ctl.CheckDlgValue((Form) this, 'I', this.JumpTo, 1.0, (double) this.e.TotalLines))
      return;
    this.e.DlgResult = this.ctl.ToInt(this.JumpTo);
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_jump_Activated(object sender, EventArgs e) => this.JumpTo.Focus();

  private void terdlg_jump_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
  }
}
