// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_date
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_date : Form
{
  private ListBox box;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Button OK;

  internal terdlg_date(ImRtfEditor parent)
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
    this.box = new ListBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(8, 208 /*0xD0*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(104, 208 /*0xD0*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[1]
    {
      (Control) this.box
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(176 /*0xB0*/, 200);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.box.Location = new Point(8, 16 /*0x10*/);
    this.box.Name = "box";
    this.box.Size = new Size(160 /*0xA0*/, 173);
    this.box.TabIndex = 0;
    this.box.DoubleClick += new EventHandler(this.box_DoubleClick);
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(192 /*0xC0*/, 237);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_date);
    this.Text = "Insert Current Date/Time";
    this.Load += new EventHandler(this.terdlg_date_Load);
    this.Activated += new EventHandler(this.terdlg_date_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgText1 = tc.DateFieldText[this.box.SelectedIndex];
  }

  private void terdlg_date_Activated(object sender, EventArgs e)
  {
  }

  private void terdlg_date_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    for (int index = 0; index < tc.TotalDateFmts; ++index)
    {
      string DateString;
      this.ctl.GetDateString(tc.DateFieldText[index], out DateString, -2);
      this.box.Items.Add((object) DateString);
    }
    this.box.SelectedIndex = 0;
  }
}
