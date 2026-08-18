// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_checkbox_field
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_checkbox_field : Form
{
  private TextBox BoxSize;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private TextBox FieldName;
  private GroupBox groupBox1;
  private CheckBox IsChecked;
  private Label label1;
  private Label label2;
  private Button OK;

  internal terdlg_checkbox_field(ImRtfEditor parent)
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
    this.BoxSize = new TextBox();
    this.label2 = new Label();
    this.IsChecked = new CheckBox();
    this.FieldName = new TextBox();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(104, 112 /*0x70*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(192 /*0xC0*/, 112 /*0x70*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[5]
    {
      (Control) this.BoxSize,
      (Control) this.label2,
      (Control) this.IsChecked,
      (Control) this.FieldName,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(264, 104);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.BoxSize.Location = new Point(200, 64 /*0x40*/);
    this.BoxSize.Name = "BoxSize";
    this.BoxSize.Size = new Size(48 /*0x30*/, 20);
    this.BoxSize.TabIndex = 4;
    this.BoxSize.Text = "";
    this.label2.Location = new Point(128 /*0x80*/, 64 /*0x40*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(64 /*0x40*/, 16 /*0x10*/);
    this.label2.TabIndex = 3;
    this.label2.Text = "Size (twips)";
    this.IsChecked.Location = new Point(16 /*0x10*/, 64 /*0x40*/);
    this.IsChecked.Name = "IsChecked";
    this.IsChecked.Size = new Size(88, 16 /*0x10*/);
    this.IsChecked.TabIndex = 2;
    this.IsChecked.Text = "Checked";
    this.FieldName.Location = new Point(16 /*0x10*/, 32 /*0x20*/);
    this.FieldName.Name = "FieldName";
    this.FieldName.Size = new Size(232, 20);
    this.FieldName.TabIndex = 1;
    this.FieldName.Text = "";
    this.label1.Location = new Point(16 /*0x10*/, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(88, 24);
    this.label1.TabIndex = 0;
    this.label1.Text = "Field Name";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(280, 141);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_checkbox_field);
    this.Text = "Checkbox Field Parameters";
    this.Load += new EventHandler(this.terdlg_checkbox_field_Load);
    this.Activated += new EventHandler(this.terdlg_checkbox_field_Activated);
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
      this.e.DlgInt1 = this.ctl.ToInt(this.BoxSize);
      this.e.DlgInt2 = this.IsChecked.Checked ? 1 : 0;
    }
  }

  private void terdlg_checkbox_field_Activated(object sender, EventArgs e)
  {
    this.FieldName.Focus();
  }

  private void terdlg_checkbox_field_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.BoxSize.Text = 200.ToString();
  }
}
