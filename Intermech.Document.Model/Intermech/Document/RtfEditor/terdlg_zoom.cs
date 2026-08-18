// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_zoom
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_zoom : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label1;
  private Label label2;
  private Button OK;
  private TextBox Percent;

  internal terdlg_zoom(ImRtfEditor parent)
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
    this.label2 = new Label();
    this.Percent = new TextBox();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(48 /*0x30*/, 64 /*0x40*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(136, 64 /*0x40*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[3]
    {
      (Control) this.label2,
      (Control) this.Percent,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(208 /*0xD0*/, 56);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.label2.Location = new Point(184, 24);
    this.label2.Name = "label2";
    this.label2.Size = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.label2.TabIndex = 2;
    this.label2.Text = "%";
    this.Percent.Location = new Point(152, 22);
    this.Percent.Name = "Percent";
    this.Percent.Size = new Size(32 /*0x20*/, 20);
    this.Percent.TabIndex = 1;
    this.Percent.Text = "100";
    this.label1.Location = new Point(8, 24);
    this.label1.Name = "label1";
    this.label1.Size = new Size(136, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Zoom Percent (10 to 200)";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(224 /*0xE0*/, 93);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_zoom);
    this.Text = "Zoom Parameters";
    this.Load += new EventHandler(this.terdlg_zoom_Load);
    this.Activated += new EventHandler(this.terdlg_zoom_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    int num = this.ctl.ToInt(this.Percent);
    if (num < 10 || num > 500)
    {
      this.Percent.Focus();
      this.ctl.MessageBeep(0);
      this.DialogResult = DialogResult.None;
    }
    else
      this.e.DlgInt1 = num;
  }

  private void terdlg_zoom_Activated(object sender, EventArgs e) => this.Percent.Focus();

  private void terdlg_zoom_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.Percent.Text = this.e.ZoomPercent.ToString();
  }
}
