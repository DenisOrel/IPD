// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_list_prop
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_list_prop : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label1;
  private TextBox ListName;
  private CheckBox Nested;
  private Button OK;
  private CheckBox RestartAtSec;

  internal terdlg_list_prop(ImRtfEditor parent)
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
    this.RestartAtSec = new CheckBox();
    this.Nested = new CheckBox();
    this.ListName = new TextBox();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(56, 128 /*0x80*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(144 /*0x90*/, 128 /*0x80*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[4]
    {
      (Control) this.RestartAtSec,
      (Control) this.Nested,
      (Control) this.ListName,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 8);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(264, 112 /*0x70*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.RestartAtSec.Location = new Point(16 /*0x10*/, 88);
    this.RestartAtSec.Name = "RestartAtSec";
    this.RestartAtSec.Size = new Size(176 /*0xB0*/, 16 /*0x10*/);
    this.RestartAtSec.TabIndex = 3;
    this.RestartAtSec.Text = "Restart at section break";
    this.Nested.Location = new Point(16 /*0x10*/, 72);
    this.Nested.Name = "Nested";
    this.Nested.Size = new Size(168, 16 /*0x10*/);
    this.Nested.TabIndex = 2;
    this.Nested.Text = "Multi-level list";
    this.ListName.Location = new Point(80 /*0x50*/, 24);
    this.ListName.Name = "ListName";
    this.ListName.Size = new Size(176 /*0xB0*/, 20);
    this.ListName.TabIndex = 1;
    this.ListName.Text = "";
    this.label1.Location = new Point(16 /*0x10*/, 24);
    this.label1.Name = "label1";
    this.label1.Size = new Size(64 /*0x40*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "List Name";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(280, 157);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_list_prop);
    this.Text = "List Properties";
    this.Load += new EventHandler(this.terdlg_list_prop_Load);
    this.Activated += new EventHandler(this.terdlg_list_prop_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgText1 = this.ListName.Text;
    this.e.DlgInt1 = this.Nested.Checked ? 1 : 0;
    this.e.DlgUint = tc.ResetFlag(this.e.DlgUint, 1);
    if (!this.RestartAtSec.Checked)
      return;
    this.e.DlgUint |= 1;
  }

  private void terdlg_list_prop_Activated(object sender, EventArgs e) => this.ListName.Focus();

  private void terdlg_list_prop_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.ListName.Text = this.e.DlgText1;
    this.Nested.Checked = this.ctl.True(this.e.DlgInt1);
    this.RestartAtSec.Checked = this.ctl.True(this.e.DlgUint & 1);
    this.ListName.Focus();
  }
}
