// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_list_select
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_list_select : Form
{
  private ListBox box;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private Button OK;

  internal terdlg_list_select(ImRtfEditor parent)
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
    this.box = new ListBox();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(8, 168);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(96 /*0x60*/, 168);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.box.Location = new Point(8, 8);
    this.box.Name = "box";
    this.box.Size = new Size(168, 147);
    this.box.TabIndex = 6;
    this.box.DoubleClick += new EventHandler(this.box_DoubleClick);
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(184, 197);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.box,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_list_select);
    this.Text = "Select a List";
    this.Load += new EventHandler(this.terdlg_list_select_Load);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgInt1 = ((tc.ClsBox) this.box.SelectedItem).value;
  }

  private void terdlg_list_select_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.e.par.FillListBox((object) this.box, true, true, -1);
  }
}
