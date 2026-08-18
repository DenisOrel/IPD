// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_input_field
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_input_field : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private CheckBox FieldBorder;
  private TextBox FieldData;
  private Button FieldFont;
  private TextBox FieldName;
  private GroupBox groupBox1;
  private Label label1;
  private Label label2;
  private Label label3;
  private TextBox MaxLen;
  private Button OK;

  internal terdlg_input_field(ImRtfEditor parent)
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

  private void FieldFont_Click(object sender, EventArgs e) => this.ctl.DlgEditFont();

  private void InitializeComponent()
  {
    this.OK = new Button();
    this.Cancel = new Button();
    this.groupBox1 = new GroupBox();
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.FieldName = new TextBox();
    this.FieldData = new TextBox();
    this.MaxLen = new TextBox();
    this.FieldFont = new Button();
    this.FieldBorder = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(112 /*0x70*/, 176 /*0xB0*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(200, 176 /*0xB0*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[8]
    {
      (Control) this.FieldBorder,
      (Control) this.FieldFont,
      (Control) this.MaxLen,
      (Control) this.FieldData,
      (Control) this.FieldName,
      (Control) this.label3,
      (Control) this.label2,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(272, 168);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(72, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Field Name";
    this.label2.Location = new Point(8, 56);
    this.label2.Name = "label2";
    this.label2.Size = new Size(72, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "Field Data";
    this.label3.Location = new Point(8, 112 /*0x70*/);
    this.label3.Name = "label3";
    this.label3.Size = new Size(120, 16 /*0x10*/);
    this.label3.TabIndex = 2;
    this.label3.Text = "Maximum Field Length";
    this.FieldName.Location = new Point(8, 32 /*0x20*/);
    this.FieldName.Name = "FieldName";
    this.FieldName.Size = new Size(248, 20);
    this.FieldName.TabIndex = 3;
    this.FieldName.Text = "";
    this.FieldData.Location = new Point(8, 72);
    this.FieldData.Name = "FieldData";
    this.FieldData.Size = new Size(248, 20);
    this.FieldData.TabIndex = 4;
    this.FieldData.Text = "";
    this.MaxLen.Location = new Point(128 /*0x80*/, 112 /*0x70*/);
    this.MaxLen.Name = "MaxLen";
    this.MaxLen.Size = new Size(56, 20);
    this.MaxLen.TabIndex = 5;
    this.MaxLen.Text = "";
    this.FieldFont.Location = new Point(192 /*0xC0*/, 136);
    this.FieldFont.Name = "FieldFont";
    this.FieldFont.Size = new Size(64 /*0x40*/, 24);
    this.FieldFont.TabIndex = 6;
    this.FieldFont.Text = "Font...";
    this.FieldFont.Click += new EventHandler(this.FieldFont_Click);
    this.FieldBorder.Location = new Point(8, 136);
    this.FieldBorder.Name = "FieldBorder";
    this.FieldBorder.Size = new Size(160 /*0xA0*/, 16 /*0x10*/);
    this.FieldBorder.TabIndex = 7;
    this.FieldBorder.Text = "Border Around the Field";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(288, 205);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_input_field);
    this.Text = "Input Field Parameters";
    this.Load += new EventHandler(this.terdlg_input_field_Load);
    this.Activated += new EventHandler(this.terdlg_input_field_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgText1 = this.FieldName.Text;
    if (this.e.DlgText1.Length == 0)
    {
      this.DialogResult = DialogResult.None;
    }
    else
    {
      this.e.DlgText2 = this.FieldData.Text;
      this.e.DlgInt1 = this.ctl.ToInt(this.MaxLen);
      this.e.DlgInt2 = this.FieldBorder.Checked ? 1 : 0;
    }
  }

  private void terdlg_input_field_Activated(object sender, EventArgs e) => this.FieldName.Focus();

  private void terdlg_input_field_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    int effectiveCfmt = this.ctl.GetEffectiveCfmt();
    this.e.DlgTypeface = this.e.TerFont[effectiveCfmt].TypeFace;
    this.e.DlgInt3 = this.e.TerFont[effectiveCfmt].TwipsSize;
    this.e.DlgInt4 = this.e.TerFont[effectiveCfmt].style;
    this.e.DlgColor1 = this.e.TerFont[effectiveCfmt].TextColor;
  }
}
