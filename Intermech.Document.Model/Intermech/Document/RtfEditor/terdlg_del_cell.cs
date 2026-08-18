// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_del_cell
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_del_cell : Form
{
  private RadioButton BtnCell;
  private RadioButton BtnColumn;
  private RadioButton BtnRow;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private ImRtfEditor e;
  private Button OK;
  private Panel panel1;

  internal terdlg_del_cell(ImRtfEditor parent)
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

  private void DlgDeleteCell_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.BtnCell.Checked = true;
    this.BtnCell.Focus();
  }

  private void InitializeComponent()
  {
    this.BtnCell = new RadioButton();
    this.BtnColumn = new RadioButton();
    this.BtnRow = new RadioButton();
    this.panel1 = new Panel();
    this.Cancel = new Button();
    this.OK = new Button();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.BtnCell.Location = new Point(8, 8);
    this.BtnCell.Name = "BtnCell";
    this.BtnCell.Size = new Size(104, 16 /*0x10*/);
    this.BtnCell.TabIndex = 0;
    this.BtnCell.Text = "Delete Cells";
    this.BtnColumn.Location = new Point(8, 24);
    this.BtnColumn.Name = "BtnColumn";
    this.BtnColumn.Size = new Size(104, 16 /*0x10*/);
    this.BtnColumn.TabIndex = 1;
    this.BtnColumn.Text = "Delete Columns";
    this.BtnRow.Location = new Point(8, 40);
    this.BtnRow.Name = "BtnRow";
    this.BtnRow.Size = new Size(104, 16 /*0x10*/);
    this.BtnRow.TabIndex = 2;
    this.BtnRow.Text = "Delete Rows";
    this.panel1.BorderStyle = BorderStyle.Fixed3D;
    this.panel1.Controls.AddRange(new Control[3]
    {
      (Control) this.BtnCell,
      (Control) this.BtnColumn,
      (Control) this.BtnRow
    });
    this.panel1.Location = new Point(8, 16 /*0x10*/);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(152, 64 /*0x40*/);
    this.panel1.TabIndex = 3;
    this.Cancel.Location = new Point(88, 88);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(72, 23);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(8, 88);
    this.OK.Name = "OK";
    this.OK.Size = new Size(72, 23);
    this.OK.TabIndex = 6;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(176 /*0xB0*/, 125);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.OK,
      (Control) this.Cancel,
      (Control) this.panel1
    });
    this.Name = "DlgDeleteCell";
    this.Text = "Delete Table Cells";
    this.Load += new EventHandler(this.DlgDeleteCell_Load);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    if (this.BtnCell.Checked)
      this.e.DlgResult = 889;
    else if (this.BtnColumn.Checked)
      this.e.DlgResult = 887;
    else
      this.e.DlgResult = 888;
  }
}
