// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_draw_object
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_draw_object : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private RadioButton Line;
  private Button OK;
  private RadioButton Rectangle;
  private RadioButton TextBox;

  internal terdlg_draw_object(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
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
    this.TextBox = new RadioButton();
    this.Rectangle = new RadioButton();
    this.Line = new RadioButton();
    this.groupBox1 = new GroupBox();
    this.OK = new Button();
    this.Cancel = new Button();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.TextBox.Location = new Point(16 /*0x10*/, 24);
    this.TextBox.Name = "TextBox";
    this.TextBox.Size = new Size(88, 16 /*0x10*/);
    this.TextBox.TabIndex = 0;
    this.TextBox.Text = "Text Box";
    this.Rectangle.Location = new Point(16 /*0x10*/, 40);
    this.Rectangle.Name = "Rectangle";
    this.Rectangle.Size = new Size(88, 16 /*0x10*/);
    this.Rectangle.TabIndex = 1;
    this.Rectangle.Text = "Rectangle";
    this.Line.Location = new Point(16 /*0x10*/, 56);
    this.Line.Name = "Line";
    this.Line.Size = new Size(88, 16 /*0x10*/);
    this.Line.TabIndex = 2;
    this.Line.Text = "Line";
    this.groupBox1.Controls.AddRange(new Control[3]
    {
      (Control) this.TextBox,
      (Control) this.Rectangle,
      (Control) this.Line
    });
    this.groupBox1.Location = new Point(8, 8);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(168, 88);
    this.groupBox1.TabIndex = 3;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Object Type";
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
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(184, 141);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.Cancel,
      (Control) this.OK,
      (Control) this.groupBox1
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_draw_object);
    this.Text = "Insert Drawing Object";
    this.Load += new EventHandler(this.terdlg_draw_object_Load);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    if (this.TextBox.Checked)
      this.e.DlgResult = 1;
    else if (this.Rectangle.Checked)
      this.e.DlgResult = 2;
    else
      this.e.DlgResult = 3;
  }

  private void terdlg_draw_object_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.TextBox.Checked = true;
  }
}
