// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_search
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_search : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label1;
  private Button OK;
  private RadioButton SearchBack;
  private RadioButton SearchBegin;
  private CheckBox SearchCase;
  private RadioButton SearchFor;
  private TextBox SearchString;
  private CheckBox SearchWord;

  internal terdlg_search(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
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
    this.SearchCase = new CheckBox();
    this.SearchBack = new RadioButton();
    this.SearchFor = new RadioButton();
    this.SearchBegin = new RadioButton();
    this.SearchString = new TextBox();
    this.label1 = new Label();
    this.SearchWord = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(88, 144 /*0x90*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(176 /*0xB0*/, 144 /*0x90*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[7]
    {
      (Control) this.SearchWord,
      (Control) this.SearchCase,
      (Control) this.SearchBack,
      (Control) this.SearchFor,
      (Control) this.SearchBegin,
      (Control) this.SearchString,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(272, 136);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.SearchCase.Location = new Point(152, 88);
    this.SearchCase.Name = "SearchCase";
    this.SearchCase.Size = new Size(112 /*0x70*/, 24);
    this.SearchCase.TabIndex = 5;
    this.SearchCase.Text = "Case Sensitive";
    this.SearchBack.Location = new Point(8, 104);
    this.SearchBack.Name = "SearchBack";
    this.SearchBack.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.SearchBack.TabIndex = 4;
    this.SearchBack.Text = "Backward";
    this.SearchFor.Location = new Point(8, 88);
    this.SearchFor.Name = "SearchFor";
    this.SearchFor.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.SearchFor.TabIndex = 3;
    this.SearchFor.Text = "Forward";
    this.SearchBegin.Location = new Point(8, 72);
    this.SearchBegin.Name = "SearchBegin";
    this.SearchBegin.Size = new Size(144 /*0x90*/, 16 /*0x10*/);
    this.SearchBegin.TabIndex = 2;
    this.SearchBegin.Text = "From Beginning  of File";
    this.SearchString.Location = new Point(8, 32 /*0x20*/);
    this.SearchString.Name = "SearchString";
    this.SearchString.Size = new Size(256 /*0x0100*/, 20);
    this.SearchString.TabIndex = 1;
    this.SearchString.Text = "";
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(48 /*0x30*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Locate:";
    this.SearchWord.Location = new Point(152, 104);
    this.SearchWord.Name = "SearchWord";
    this.SearchWord.Size = new Size(112 /*0x70*/, 24);
    this.SearchWord.TabIndex = 6;
    this.SearchWord.Text = "Whole Word";
    this.SearchWord.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(288, 173);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_search);
    this.Text = "Search String Parameters";
    this.Load += new EventHandler(this.terdlg_search_Load);
    this.Activated += new EventHandler(this.terdlg_search_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.SearchString = this.SearchString.Text;
    this.e.SearchFlags = tc.ResetFlag(this.e.SearchFlags, 48 /*0x30*/);
    if (this.SearchCase.Checked)
      this.e.SearchFlags |= 16 /*0x10*/;
    if (this.SearchWord.Checked)
      this.e.SearchFlags |= 32 /*0x20*/;
    if (this.SearchBegin.Checked)
      this.e.SearchDirection = 'E';
    else if (this.SearchFor.Checked)
      this.e.SearchDirection = 'F';
    else
      this.e.SearchDirection = 'B';
  }

  private void terdlg_search_Activated(object sender, EventArgs e) => this.SearchString.Focus();

  private void terdlg_search_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.SearchString.Text = this.e.SearchString;
    if (this.ctl.True(this.e.SearchFlags & 16 /*0x10*/))
      this.SearchCase.Checked = true;
    if (this.ctl.True(this.e.SearchFlags & 32 /*0x20*/))
      this.SearchWord.Checked = true;
    this.e.SearchDirection = 'E';
    this.SearchBegin.Checked = true;
  }
}
