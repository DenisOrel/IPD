// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_hyperlink
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_hyperlink : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label1;
  private Label label2;
  private TextBox LinkCode;
  private TextBox LinkText;
  private Button OK;

  internal terdlg_hyperlink(ImRtfEditor parent)
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
    this.LinkText = new TextBox();
    this.LinkCode = new TextBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(80 /*0x50*/, 120);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(168, 120);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[4]
    {
      (Control) this.LinkCode,
      (Control) this.LinkText,
      (Control) this.label2,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(240 /*0xF0*/, 112 /*0x70*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(104, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Link Text";
    this.label2.Location = new Point(8, 64 /*0x40*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(204, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "Link Code (or URL)";
    this.LinkText.Location = new Point(8, 32 /*0x20*/);
    this.LinkText.Name = "LinkText";
    this.LinkText.Size = new Size(224 /*0xE0*/, 20);
    this.LinkText.TabIndex = 2;
    this.LinkText.Text = "";
    this.LinkCode.Location = new Point(8, 80 /*0x50*/);
    this.LinkCode.Name = "LinkCode";
    this.LinkCode.Size = new Size(224 /*0xE0*/, 20);
    this.LinkCode.TabIndex = 3;
    this.LinkCode.Text = "";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(256 /*0x0100*/, 149);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_hyperlink);
    this.Text = "Data Field Parameters";
    this.Load += new EventHandler(this.terdlg_hyperlink_Load);
    this.Activated += new EventHandler(this.terdlg_hyperlink_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgText1 = this.LinkText.Text;
    if (this.e.DlgText1.Length == 0)
      this.DialogResult = DialogResult.None;
    else
      this.e.DlgText2 = this.LinkCode.Text;
  }

  private void terdlg_hyperlink_Activated(object sender, EventArgs e) => this.LinkText.Focus();

  private void terdlg_hyperlink_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
  }
}
