// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_replace
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_replace : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label1;
  private Label label2;
  private Button OK;
  private RadioButton ReplaceBlock;
  private RadioButton ReplaceFile;
  private TextBox ReplaceString;
  private CheckBox ReplaceVerify;
  private TextBox ReplaceWith;

  internal terdlg_replace(ImRtfEditor parent)
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
    this.label2 = new Label();
    this.ReplaceString = new TextBox();
    this.ReplaceWith = new TextBox();
    this.ReplaceFile = new RadioButton();
    this.ReplaceBlock = new RadioButton();
    this.ReplaceVerify = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(112 /*0x70*/, 168);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(200, 168);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[7]
    {
      (Control) this.ReplaceVerify,
      (Control) this.ReplaceBlock,
      (Control) this.ReplaceFile,
      (Control) this.ReplaceWith,
      (Control) this.ReplaceString,
      (Control) this.label2,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(272, 160 /*0xA0*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Replace:";
    this.label2.Location = new Point(8, 64 /*0x40*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "With";
    this.ReplaceString.Location = new Point(8, 32 /*0x20*/);
    this.ReplaceString.Name = "ReplaceString";
    this.ReplaceString.Size = new Size(256 /*0x0100*/, 20);
    this.ReplaceString.TabIndex = 2;
    this.ReplaceString.Text = "";
    this.ReplaceWith.Location = new Point(8, 80 /*0x50*/);
    this.ReplaceWith.Name = "ReplaceWith";
    this.ReplaceWith.Size = new Size(256 /*0x0100*/, 20);
    this.ReplaceWith.TabIndex = 3;
    this.ReplaceWith.Text = "";
    this.ReplaceFile.Location = new Point(8, 120);
    this.ReplaceFile.Name = "ReplaceFile";
    this.ReplaceFile.Size = new Size(104, 16 /*0x10*/);
    this.ReplaceFile.TabIndex = 4;
    this.ReplaceFile.Text = "All";
    this.ReplaceBlock.Location = new Point(8, 136);
    this.ReplaceBlock.Name = "ReplaceBlock";
    this.ReplaceBlock.Size = new Size(104, 16 /*0x10*/);
    this.ReplaceBlock.TabIndex = 5;
    this.ReplaceBlock.Text = "Selected Text";
    this.ReplaceVerify.Location = new Point(136, 136);
    this.ReplaceVerify.Name = "ReplaceVerify";
    this.ReplaceVerify.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.ReplaceVerify.TabIndex = 6;
    this.ReplaceVerify.Text = "Verify Each Replace";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(288, 197);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_replace);
    this.Text = "Replace String Parameters";
    this.Load += new EventHandler(this.terdlg_replace_Load);
    this.Activated += new EventHandler(this.terdlg_replace_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.ReplaceString = this.ReplaceString.Text;
    this.e.ReplaceWith = this.ReplaceWith.Text;
    this.e.ReplaceVerify = this.ReplaceVerify.Checked;
    this.e.ReplaceBlock = this.ReplaceBlock.Checked;
    if (!this.e.ReplaceBlock || this.e.HilightType == 1 || this.e.HilightType == 2)
      return;
    int num = (int) this.ctl.ShowMessage(this.e.MsgString[83], "", MessageBoxButtons.OK);
    this.ReplaceBlock.Focus();
  }

  private void terdlg_replace_Activated(object sender, EventArgs e) => this.ReplaceString.Focus();

  private void terdlg_replace_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.ReplaceString.Text = this.e.ReplaceString;
    this.ReplaceWith.Text = this.e.ReplaceWith;
    this.e.ReplaceVerify = false;
    this.ReplaceVerify.Checked = this.e.ReplaceVerify;
    this.e.ReplaceBlock = this.e.HilightType == 1 || this.e.HilightType == 2;
    this.ReplaceFile.Checked = !this.e.ReplaceBlock;
    this.ReplaceBlock.Checked = this.e.ReplaceBlock;
  }
}
