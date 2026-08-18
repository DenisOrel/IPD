// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_list_para
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_list_para : Form
{
  private ListBox box;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupbox;
  private Label label1;
  private Label label2;
  private ComboBox Level;
  private Button OK;

  internal terdlg_list_para(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void box_DoubleClick(object sender, EventArgs ev)
  {
    this.DialogResult = DialogResult.OK;
    this.OK_Click(sender, ev);
    if (this.DialogResult != DialogResult.OK)
      return;
    this.Hide();
  }

  private void box_SelectedIndexChanged(object sender, EventArgs ev)
  {
    this.e.par.SetDlgListParaLevel(this.box, this.Level, this.e.DlgInt2);
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
    this.groupbox = new GroupBox();
    this.box = new ListBox();
    this.Level = new ComboBox();
    this.label1 = new Label();
    this.label2 = new Label();
    this.groupbox.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(128 /*0x80*/, 232);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(216, 232);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupbox.Controls.AddRange(new Control[4]
    {
      (Control) this.label2,
      (Control) this.label1,
      (Control) this.Level,
      (Control) this.box
    });
    this.groupbox.Location = new Point(8, 8);
    this.groupbox.Name = "groupbox";
    this.groupbox.Size = new Size(288, 216);
    this.groupbox.TabIndex = 6;
    this.groupbox.TabStop = false;
    this.box.Location = new Point(8, 40);
    this.box.Name = "box";
    this.box.Size = new Size(168, 160 /*0xA0*/);
    this.box.TabIndex = 0;
    this.box.DoubleClick += new EventHandler(this.box_DoubleClick);
    this.box.SelectedIndexChanged += new EventHandler(this.box_SelectedIndexChanged);
    this.Level.Location = new Point(192 /*0xC0*/, 40);
    this.Level.Name = "Level";
    this.Level.Size = new Size(80 /*0x50*/, 21);
    this.Level.TabIndex = 1;
    this.Level.Text = "1";
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(72, 16 /*0x10*/);
    this.label1.TabIndex = 2;
    this.label1.Text = "List";
    this.label2.Location = new Point(192 /*0xC0*/, 16 /*0x10*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(72, 16 /*0x10*/);
    this.label2.TabIndex = 3;
    this.label2.Text = "Level";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(304, 261);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupbox,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_list_para);
    this.Text = "Apply paragraph numbering using Lists";
    this.Load += new EventHandler(this.terdlg_list_para_Load);
    this.groupbox.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgInt1 = ((tc.ClsBox) this.box.SelectedItem).value;
    this.e.DlgInt2 = ((tc.ClsBox) this.Level.SelectedItem).value;
  }

  private void terdlg_list_para_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.e.par.FillListOrBox((object) this.box, true, true, this.e.DlgInt1, false);
    this.e.par.SetDlgListParaLevel(this.box, this.Level, this.e.DlgInt2);
  }
}
