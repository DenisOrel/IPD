// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_para_text_flow
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_para_text_flow : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private RadioButton Default;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private RadioButton Ltr;
  private Button OK;
  private RadioButton Rtl;

  internal terdlg_para_text_flow(ImRtfEditor parent)
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
    this.Ltr = new RadioButton();
    this.Rtl = new RadioButton();
    this.Default = new RadioButton();
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
      (Control) this.Default,
      (Control) this.Rtl,
      (Control) this.Ltr
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(168, 96 /*0x60*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Text flow";
    this.Ltr.Location = new Point(16 /*0x10*/, 24);
    this.Ltr.Name = "Ltr";
    this.Ltr.Size = new Size(96 /*0x60*/, 24);
    this.Ltr.TabIndex = 0;
    this.Ltr.Text = "Left-to-Right";
    this.Rtl.Location = new Point(16 /*0x10*/, 40);
    this.Rtl.Name = "Rtl";
    this.Rtl.Size = new Size(96 /*0x60*/, 24);
    this.Rtl.TabIndex = 1;
    this.Rtl.Text = "Right-to-Left";
    this.Default.Location = new Point(16 /*0x10*/, 56);
    this.Default.Name = "Default";
    this.Default.Size = new Size(96 /*0x60*/, 24);
    this.Default.TabIndex = 2;
    this.Default.Text = "Default";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(184, 133);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_para_text_flow);
    this.Text = "Paragraph Text Flow";
    this.Load += new EventHandler(this.terdlg_para_text_flow_Load);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    int num = 0;
    if (this.Rtl.Checked)
      num = 2;
    else if (this.Ltr.Checked)
      num = 1;
    this.e.DlgInt1 = num;
  }

  private void terdlg_para_text_flow_Activated(object sender, EventArgs ev)
  {
  }

  private void terdlg_para_text_flow_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.Text = this.e.DlgText1;
    switch (this.e.DlgInt1)
    {
      case 1:
        this.Ltr.Checked = true;
        break;
      case 2:
        this.Rtl.Checked = true;
        break;
      default:
        this.Default.Checked = true;
        break;
    }
  }
}
