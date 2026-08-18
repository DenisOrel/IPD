// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_edit_input_field
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_edit_input_field : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private int FieldId;
  private TextBox FieldName;
  private Button FontInfo;
  private GroupBox groupBox1;
  private CheckBox HasBorder;
  private Label label1;
  private Label label2;
  private TextBox MaxLen;
  private Button OK;

  internal terdlg_edit_input_field(ImRtfEditor parent)
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

  private void FontInfo_Click(object sender, EventArgs e) => this.ctl.DlgEditFont();

  private void InitializeComponent()
  {
    this.OK = new Button();
    this.Cancel = new Button();
    this.groupBox1 = new GroupBox();
    this.FontInfo = new Button();
    this.HasBorder = new CheckBox();
    this.MaxLen = new TextBox();
    this.label2 = new Label();
    this.FieldName = new TextBox();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(80 /*0x50*/, 144 /*0x90*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(168, 144 /*0x90*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[6]
    {
      (Control) this.FontInfo,
      (Control) this.HasBorder,
      (Control) this.MaxLen,
      (Control) this.label2,
      (Control) this.FieldName,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(240 /*0xF0*/, 136);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.FontInfo.Location = new Point(176 /*0xB0*/, 100);
    this.FontInfo.Name = "FontInfo";
    this.FontInfo.Size = new Size(56, 24);
    this.FontInfo.TabIndex = 7;
    this.FontInfo.Text = "Font...";
    this.FontInfo.Click += new EventHandler(this.FontInfo_Click);
    this.HasBorder.Location = new Point(8, 104);
    this.HasBorder.Name = "HasBorder";
    this.HasBorder.Size = new Size(152, 16 /*0x10*/);
    this.HasBorder.TabIndex = 6;
    this.HasBorder.Text = "Border Around the Field";
    this.MaxLen.Location = new Point(136, 62);
    this.MaxLen.Name = "MaxLen";
    this.MaxLen.Size = new Size(40, 20);
    this.MaxLen.TabIndex = 3;
    this.MaxLen.Text = "";
    this.label2.Location = new Point(8, 64 /*0x40*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.label2.TabIndex = 2;
    this.label2.Text = "Maximum Characters";
    this.FieldName.Location = new Point(8, 32 /*0x20*/);
    this.FieldName.Name = "FieldName";
    this.FieldName.Size = new Size(224 /*0xE0*/, 20);
    this.FieldName.TabIndex = 1;
    this.FieldName.Text = "";
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(72, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Field Name";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(256 /*0x0100*/, 173);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_edit_input_field);
    this.Text = "Edit Input Field";
    this.Load += new EventHandler(this.terdlg_edit_input_field_Load);
    this.Activated += new EventHandler(this.terdlg_edit_input_field_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.None;
    this.e.DlgText1 = this.FieldName.Text;
    if (this.e.DlgText1.Length == 0)
      return;
    this.e.DlgInt1 = this.ctl.ToInt(this.MaxLen);
    this.e.DlgBool1 = this.HasBorder.Checked;
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_edit_input_field_Activated(object sender, EventArgs e)
  {
    this.FieldName.Focus();
  }

  private void terdlg_edit_input_field_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.FieldId = this.e.DlgInt6;
    this.FieldName.Text = this.e.DlgText1;
    this.MaxLen.Text = this.e.DlgInt1.ToString();
    this.HasBorder.Checked = this.e.DlgBool1;
    if (this.FieldId != 3)
      return;
    this.MaxLen.Enabled = false;
    this.HasBorder.Enabled = false;
    this.FontInfo.Enabled = false;
  }
}
