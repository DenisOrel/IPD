// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_row_text_flow
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_row_text_flow : Form
{
  private CheckBox AllRows;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private RadioButton FlowDef;
  private RadioButton FlowLtr;
  private RadioButton FlowRtl;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Button OK;

  internal terdlg_row_text_flow(ImRtfEditor parent)
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
    this.groupBox2 = new GroupBox();
    this.FlowLtr = new RadioButton();
    this.FlowRtl = new RadioButton();
    this.FlowDef = new RadioButton();
    this.AllRows = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(64 /*0x40*/, 136);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(152, 136);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.AllRows,
      (Control) this.groupBox2
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(224 /*0xE0*/, 128 /*0x80*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox2.Controls.AddRange(new Control[3]
    {
      (Control) this.FlowDef,
      (Control) this.FlowRtl,
      (Control) this.FlowLtr
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(208 /*0xD0*/, 80 /*0x50*/);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Text Flow";
    this.FlowLtr.Location = new Point(8, 16 /*0x10*/);
    this.FlowLtr.Name = "FlowLtr";
    this.FlowLtr.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.FlowLtr.TabIndex = 0;
    this.FlowLtr.Text = "Left-to-right";
    this.FlowRtl.Location = new Point(8, 32 /*0x20*/);
    this.FlowRtl.Name = "FlowRtl";
    this.FlowRtl.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.FlowRtl.TabIndex = 1;
    this.FlowRtl.Text = "Right-to-left";
    this.FlowDef.Location = new Point(8, 48 /*0x30*/);
    this.FlowDef.Name = "FlowDef";
    this.FlowDef.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.FlowDef.TabIndex = 2;
    this.FlowDef.Text = "Default text flow";
    this.AllRows.Location = new Point(8, 104);
    this.AllRows.Name = "AllRows";
    this.AllRows.Size = new Size(208 /*0xD0*/, 16 /*0x10*/);
    this.AllRows.TabIndex = 1;
    this.AllRows.Text = "Apply to all rows in the current table";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(240 /*0xF0*/, 165);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_row_text_flow);
    this.Text = "Table Text Flow";
    this.Load += new EventHandler(this.terdlg_row_text_flow_Load);
    this.Activated += new EventHandler(this.terdlg_row_text_flow_Activated);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    int num = 0;
    if (this.FlowRtl.Checked)
      num = 2;
    else if (this.FlowLtr.Checked)
      num = 1;
    this.e.DlgInt1 = num;
    if (this.AllRows.Checked)
      this.e.DlgResult = 891;
    else
      this.e.DlgResult = 888;
  }

  private void terdlg_row_text_flow_Activated(object sender, EventArgs e)
  {
  }

  private void terdlg_row_text_flow_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    switch (this.e.TableRow[this.e.cell[this.e.text[this.e.CurLine].cid].row].flow)
    {
      case 1:
        this.FlowLtr.Checked = true;
        break;
      case 2:
        this.FlowRtl.Checked = true;
        break;
      default:
        this.FlowDef.Checked = true;
        break;
    }
  }
}
