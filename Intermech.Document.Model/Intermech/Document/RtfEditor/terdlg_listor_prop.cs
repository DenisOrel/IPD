// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_listor_prop
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_listor_prop : Form
{
  private ComboBox box;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label1;
  private Button OK;
  private CheckBox OverrideLevel;

  internal terdlg_listor_prop(ImRtfEditor parent)
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
    this.label1 = new Label();
    this.box = new ComboBox();
    this.OverrideLevel = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(40, 120);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(128 /*0x80*/, 120);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[3]
    {
      (Control) this.OverrideLevel,
      (Control) this.box,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 8);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(240 /*0xF0*/, 96 /*0x60*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.label1.Location = new Point(16 /*0x10*/, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(128 /*0x80*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "List to Override";
    this.box.Location = new Point(16 /*0x10*/, 32 /*0x20*/);
    this.box.Name = "box";
    this.box.Size = new Size(208 /*0xD0*/, 21);
    this.box.TabIndex = 1;
    this.OverrideLevel.Location = new Point(16 /*0x10*/, 72);
    this.OverrideLevel.Name = "OverrideLevel";
    this.OverrideLevel.Size = new Size(152, 16 /*0x10*/);
    this.OverrideLevel.TabIndex = 2;
    this.OverrideLevel.Text = "Override Levels";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(256 /*0x0100*/, 149);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_listor_prop);
    this.Text = "List Override Properties";
    this.Load += new EventHandler(this.terdlg_listor_prop_Load);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    tc.ClsBox selectedItem = (tc.ClsBox) this.box.SelectedItem;
    if (selectedItem == null)
    {
      this.ctl.MessageBeep(0);
      this.DialogResult = DialogResult.None;
    }
    else
    {
      this.e.DlgInt1 = selectedItem.value;
      this.e.DlgInt2 = this.OverrideLevel.Checked ? 1 : 0;
    }
  }

  private void terdlg_listor_prop_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.e.par.FillListBox((object) this.box, true, false, this.e.DlgInt1);
    this.OverrideLevel.Checked = this.ctl.True(this.e.DlgInt2);
  }
}
