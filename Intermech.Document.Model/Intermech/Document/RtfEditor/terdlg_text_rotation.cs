// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_text_rotation
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_text_rotation : Form
{
  private RadioButton BotToTop;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private RadioButton Horz;
  private Button OK;
  private RadioButton TopToBot;

  internal terdlg_text_rotation(ImRtfEditor parent)
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
    this.Horz = new RadioButton();
    this.TopToBot = new RadioButton();
    this.BotToTop = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(8, 80 /*0x50*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(96 /*0x60*/, 80 /*0x50*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[3]
    {
      (Control) this.BotToTop,
      (Control) this.TopToBot,
      (Control) this.Horz
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(168, 72);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.Horz.Location = new Point(8, 16 /*0x10*/);
    this.Horz.Name = "Horz";
    this.Horz.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.Horz.TabIndex = 0;
    this.Horz.Text = "Horizontal";
    this.TopToBot.Location = new Point(8, 32 /*0x20*/);
    this.TopToBot.Name = "TopToBot";
    this.TopToBot.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.TopToBot.TabIndex = 1;
    this.TopToBot.Text = "Top to Bottom";
    this.BotToTop.Location = new Point(8, 48 /*0x30*/);
    this.BotToTop.Name = "BotToTop";
    this.BotToTop.Size = new Size(120, 16 /*0x10*/);
    this.BotToTop.TabIndex = 2;
    this.BotToTop.Text = "Bottom to Top";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(184, 109);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_text_rotation);
    this.Text = "Text Rotation";
    this.Load += new EventHandler(this.terdlg_text_rotation_Load);
    this.Activated += new EventHandler(this.terdlg_text_rotation_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgInt1 = !this.Horz.Checked ? (!this.BotToTop.Checked ? 270 : 90) : 0;
  }

  private void terdlg_text_rotation_Activated(object sender, EventArgs e)
  {
  }

  private void terdlg_text_rotation_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    switch (this.e.DlgInt1)
    {
      case 0:
        this.Horz.Checked = true;
        break;
      case 90:
        this.BotToTop.Checked = true;
        break;
      default:
        this.TopToBot.Checked = true;
        break;
    }
  }
}
