// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_edit_style
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_edit_style : Form
{
  private ListBox box;
  private Label BoxLabel;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label3;
  private Label label4;
  private Label label5;
  private Label label6;
  private Label label7;
  private Label label8;
  private Button OK;
  private Panel panel1;
  private RadioButton SsChar;
  private TextBox SsName;
  private Label SsNameLabel;
  private CheckBox SsNew;
  private RadioButton SsPara;

  internal terdlg_edit_style(ImRtfEditor parent)
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
    this.SsChar = new RadioButton();
    this.SsPara = new RadioButton();
    this.SsNew = new CheckBox();
    this.SsNameLabel = new Label();
    this.SsName = new TextBox();
    this.BoxLabel = new Label();
    this.box = new ListBox();
    this.panel1 = new Panel();
    this.label8 = new Label();
    this.label7 = new Label();
    this.label6 = new Label();
    this.label5 = new Label();
    this.label4 = new Label();
    this.label3 = new Label();
    this.groupBox1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(64 /*0x40*/, 304);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(152, 304);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.SsChar,
      (Control) this.SsPara
    });
    this.groupBox1.Location = new Point(8, 16 /*0x10*/);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(128 /*0x80*/, 64 /*0x40*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Type";
    this.SsChar.Location = new Point(8, 40);
    this.SsChar.Name = "SsChar";
    this.SsChar.Size = new Size(104, 16 /*0x10*/);
    this.SsChar.TabIndex = 1;
    this.SsChar.Text = "Character Style";
    this.SsChar.CheckedChanged += new EventHandler(this.SsChar_CheckedChanged);
    this.SsPara.Location = new Point(8, 16 /*0x10*/);
    this.SsPara.Name = "SsPara";
    this.SsPara.Size = new Size(104, 16 /*0x10*/);
    this.SsPara.TabIndex = 0;
    this.SsPara.Text = "Paragraph Style";
    this.SsPara.CheckedChanged += new EventHandler(this.SsPara_CheckedChanged);
    this.SsNew.Location = new Point(8, 96 /*0x60*/);
    this.SsNew.Name = "SsNew";
    this.SsNew.Size = new Size(136, 24);
    this.SsNew.TabIndex = 7;
    this.SsNew.Text = "Create new style";
    this.SsNew.CheckedChanged += new EventHandler(this.SsNew_CheckedChanged);
    this.SsNameLabel.Location = new Point(8, 120);
    this.SsNameLabel.Name = "SsNameLabel";
    this.SsNameLabel.Size = new Size(136, 16 /*0x10*/);
    this.SsNameLabel.TabIndex = 8;
    this.SsNameLabel.Text = "Style Name:";
    this.SsName.Location = new Point(8, 136);
    this.SsName.Name = "SsName";
    this.SsName.Size = new Size(128 /*0x80*/, 20);
    this.SsName.TabIndex = 9;
    this.SsName.Text = "";
    this.BoxLabel.Location = new Point(160 /*0xA0*/, 8);
    this.BoxLabel.Name = "BoxLabel";
    this.BoxLabel.Size = new Size(152, 24);
    this.BoxLabel.TabIndex = 10;
    this.BoxLabel.Text = "Select a style to edit";
    this.box.Location = new Point(152, 24);
    this.box.Name = "box";
    this.box.Size = new Size(128 /*0x80*/, 134);
    this.box.Sorted = true;
    this.box.TabIndex = 11;
    this.box.DoubleClick += new EventHandler(this.box_DoubleClick);
    this.panel1.BorderStyle = BorderStyle.Fixed3D;
    this.panel1.Controls.AddRange(new Control[6]
    {
      (Control) this.label8,
      (Control) this.label7,
      (Control) this.label6,
      (Control) this.label5,
      (Control) this.label4,
      (Control) this.label3
    });
    this.panel1.Location = new Point(8, 176 /*0xB0*/);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(272, 112 /*0x70*/);
    this.panel1.TabIndex = 12;
    this.label8.Location = new Point(8, 88);
    this.label8.Name = "label8";
    this.label8.Size = new Size(296, 16 /*0x10*/);
    this.label8.TabIndex = 5;
    this.label8.Text = "from the edit menu or click anywhere on the text.";
    this.label7.Location = new Point(8, 72);
    this.label7.Name = "label7";
    this.label7.Size = new Size(296, 16 /*0x10*/);
    this.label7.TabIndex = 4;
    this.label7.Text = "stylesheet recroding, select the 'Edit Style' opton";
    this.label6.Location = new Point(8, 56);
    this.label6.Name = "label6";
    this.label6.Size = new Size(296, 16 /*0x10*/);
    this.label6.TabIndex = 3;
    this.label6.Text = "from the menu, toolbar or the ruler.  To end the";
    this.label5.Location = new Point(8, 40);
    this.label5.Name = "label5";
    this.label5.Size = new Size(296, 16 /*0x10*/);
    this.label5.TabIndex = 2;
    this.label5.Text = "You can select paragraph or font option selections";
    this.label4.Location = new Point(8, 24);
    this.label4.Name = "label4";
    this.label4.Size = new Size(296, 16 /*0x10*/);
    this.label4.TabIndex = 1;
    this.label4.Text = "Click OK to begin recording the style attributes.";
    this.label3.Location = new Point(8, 8);
    this.label3.Name = "label3";
    this.label3.Size = new Size(296, 16 /*0x10*/);
    this.label3.TabIndex = 0;
    this.label3.Text = "Select a style to edit or type in a new style name.";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(288, 341);
    this.Controls.AddRange(new Control[9]
    {
      (Control) this.panel1,
      (Control) this.box,
      (Control) this.BoxLabel,
      (Control) this.SsName,
      (Control) this.SsNameLabel,
      (Control) this.SsNew,
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_edit_style);
    this.Text = "Edit Stylesheet";
    this.Load += new EventHandler(this.terdlg_edit_style_Load);
    this.groupBox1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    bool flag = this.SsNew.Checked;
    this.e.DlgInt1 = flag ? 1 : 0;
    this.e.DlgInt2 = !this.SsPara.Checked ? 1 : 2;
    if (flag)
    {
      this.e.TempString = this.SsName.Text.Trim();
      if (this.e.TempString.Length == 0)
      {
        int num = (int) MessageBox.Show(this.e.MsgString[37], (string) null, MessageBoxButtons.OK);
        this.SsName.Focus();
        this.DialogResult = DialogResult.None;
      }
      else
      {
        for (int index = 0; index < this.e.TotalSID; ++index)
        {
          if (this.e.StyleId[index].InUse && this.e.StyleId[index].name == this.e.TempString)
          {
            if (this.e.StyleId[index].type == 2)
            {
              int num1 = (int) MessageBox.Show(this.e.MsgString[118], (string) null, MessageBoxButtons.OK);
            }
            else
            {
              int num2 = (int) MessageBox.Show(this.e.MsgString[7], (string) null, MessageBoxButtons.OK);
            }
            this.SsName.Focus();
            this.DialogResult = DialogResult.None;
            break;
          }
        }
      }
    }
    else
      this.e.TempString = this.e.StyleId[((tc.ClsBox) this.box.SelectedItem).value].name;
  }

  private void SsChar_CheckedChanged(object sender, EventArgs ev)
  {
    int num = 0;
    this.e.par.FillStyleBox((object) this.box, 1, false, true);
    for (int index = 0; index < this.e.TotalSID; ++index)
    {
      if (this.e.StyleId[index].InUse && this.e.StyleId[index].type == 1)
        ++num;
    }
    if (num > 1)
      return;
    this.SsNew.Checked = true;
    this.box.Enabled = false;
    this.BoxLabel.Enabled = false;
    this.SsName.Enabled = true;
    this.SsNameLabel.Enabled = true;
  }

  private void SsNew_CheckedChanged(object sender, EventArgs e)
  {
    bool flag = this.SsNew.Checked;
    this.box.Enabled = !flag;
    this.BoxLabel.Enabled = !flag;
    this.SsName.Enabled = flag;
    this.SsNameLabel.Enabled = flag;
  }

  private void SsPara_CheckedChanged(object sender, EventArgs ev)
  {
    this.e.par.FillStyleBox((object) this.box, 2, false, true);
    this.box.Enabled = true;
    this.BoxLabel.Enabled = true;
  }

  private void terdlg_edit_style_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.SsPara.Checked = true;
    this.e.par.FillStyleBox((object) this.box, 2, false, true);
    this.SsNameLabel.Enabled = false;
    this.SsName.Enabled = false;
  }
}
