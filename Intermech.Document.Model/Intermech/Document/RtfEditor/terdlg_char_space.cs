// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_char_space
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_char_space : Form
{
  private TextBox Adj;
  private Label AdjTitle;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private RadioButton Expand;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Button OK;
  private RadioButton Reduce;
  private RadioButton Reset;

  internal terdlg_char_space(ImRtfEditor parent)
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
    this.Expand = new RadioButton();
    this.Reduce = new RadioButton();
    this.Reset = new RadioButton();
    this.groupBox2 = new GroupBox();
    this.AdjTitle = new Label();
    this.Adj = new TextBox();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(48 /*0x30*/, 80 /*0x50*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(136, 80 /*0x50*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[3]
    {
      (Control) this.Reset,
      (Control) this.Reduce,
      (Control) this.Expand
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(104, 72);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.Expand.Location = new Point(8, 16 /*0x10*/);
    this.Expand.Name = "Expand";
    this.Expand.Size = new Size(72, 16 /*0x10*/);
    this.Expand.TabIndex = 0;
    this.Expand.Text = "Expand";
    this.Reduce.Location = new Point(8, 32 /*0x20*/);
    this.Reduce.Name = "Reduce";
    this.Reduce.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.Reduce.TabIndex = 1;
    this.Reduce.Text = "Compress";
    this.Reset.Location = new Point(8, 48 /*0x30*/);
    this.Reset.Name = "Reset";
    this.Reset.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.Reset.TabIndex = 2;
    this.Reset.Text = "Normal";
    this.Reset.CheckedChanged += new EventHandler(this.Reset_CheckedChanged);
    this.groupBox2.Controls.AddRange(new Control[2]
    {
      (Control) this.Adj,
      (Control) this.AdjTitle
    });
    this.groupBox2.Location = new Point(120, 0);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(144 /*0x90*/, 72);
    this.groupBox2.TabIndex = 7;
    this.groupBox2.TabStop = false;
    this.AdjTitle.Location = new Point(8, 24);
    this.AdjTitle.Name = "AdjTitle";
    this.AdjTitle.Size = new Size(64 /*0x40*/, 32 /*0x20*/);
    this.AdjTitle.TabIndex = 0;
    this.AdjTitle.Text = "Adjustment (Twips)";
    this.Adj.Location = new Point(80 /*0x50*/, 24);
    this.Adj.Name = "Adj";
    this.Adj.Size = new Size(56, 20);
    this.Adj.TabIndex = 1;
    this.Adj.Text = "";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(272, 109);
    this.Controls.AddRange(new Control[4]
    {
      (Control) this.groupBox2,
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_char_space);
    this.Text = "Character Spacing";
    this.Load += new EventHandler(this.terdlg_char_space_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgInt1 = 0;
    if (this.Reset.Checked)
      return;
    int num = this.ctl.ToInt(this.Adj);
    if (num < 1 || num > 5000)
    {
      this.Adj.Focus();
      this.DialogResult = DialogResult.None;
      this.e.ctl.MessageBeep(0);
    }
    else if (this.Expand.Checked)
      this.e.DlgInt1 = num;
    else
      this.e.DlgInt1 = -num;
  }

  private void Reset_CheckedChanged(object sender, EventArgs e)
  {
    bool flag = this.Reset.Checked;
    this.Adj.Enabled = !flag;
    this.AdjTitle.Enabled = !flag;
  }

  private void terdlg_char_space_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    int dlgInt1 = this.e.DlgInt1;
    this.Expand.Checked = dlgInt1 > 0;
    this.Reduce.Checked = dlgInt1 < 0;
    this.Reset.Checked = dlgInt1 == 0;
    if (dlgInt1 == 0)
    {
      this.Adj.Text = 20.ToString();
      this.Adj.Enabled = false;
      this.AdjTitle.Enabled = false;
    }
    else
      this.Adj.Text = Math.Abs(dlgInt1).ToString();
  }
}
