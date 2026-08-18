// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_table
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_table : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label1;
  private Label label2;
  private Button OK;
  private TextBox TableCols;
  private TextBox TableRows;

  internal terdlg_table(ImRtfEditor parent)
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
    this.label2 = new Label();
    this.TableRows = new TextBox();
    this.TableCols = new TextBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(40, 80 /*0x50*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(128 /*0x80*/, 80 /*0x50*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[4]
    {
      (Control) this.TableCols,
      (Control) this.TableRows,
      (Control) this.label2,
      (Control) this.label1
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(200, 72);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(104, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Number of Rows";
    this.label2.Location = new Point(8, 40);
    this.label2.Name = "label2";
    this.label2.Size = new Size(112 /*0x70*/, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "Number of Columns";
    this.TableRows.Location = new Point(136, 14);
    this.TableRows.Name = "TableRows";
    this.TableRows.Size = new Size(56, 20);
    this.TableRows.TabIndex = 2;
    this.TableRows.Text = "";
    this.TableCols.Location = new Point(136, 40);
    this.TableCols.Name = "TableCols";
    this.TableCols.Size = new Size(56, 20);
    this.TableCols.TabIndex = 3;
    this.TableCols.Text = "";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(216, 109);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_table);
    this.Text = "New Table Parameters";
    this.Load += new EventHandler(this.terdlg_table_Load);
    this.Activated += new EventHandler(this.terdlg_table_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    if (!this.ctl.CheckDlgValue((Form) this, 'I', this.TableRows, 1.0, 999.0) || !this.ctl.CheckDlgValue((Form) this, 'I', this.TableCols, 1.0, 30.0))
      return;
    this.e.TableRows = this.ctl.ToInt(this.TableRows);
    this.e.TableCols = this.ctl.ToInt(this.TableCols);
    this.DialogResult = DialogResult.OK;
  }

  private void terdlg_table_Activated(object sender, EventArgs e) => this.TableRows.Focus();

  private void terdlg_table_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    int tableRows = this.e.TableRows;
    int tableCols = this.e.TableCols;
    this.TableRows.Text = tableRows.ToString();
    this.TableCols.Text = tableCols.ToString();
  }
}
