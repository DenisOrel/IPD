// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_set_tab
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_set_tab : Form
{
  private ComboBox box;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private GroupBox groupBox3;
  private bool metric;
  private Button OK;
  private RadioButton TabDot;
  private RadioButton TabHyph;
  private RadioButton TabNone;
  private RadioButton TabTypeCenter;
  private RadioButton TabTypeDecimal;
  private RadioButton TabTypeLeft;
  private RadioButton TabTypeRight;
  private RadioButton TabUline;
  private Label Units;

  internal terdlg_set_tab(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void box_SelectedIndexChanged(object sender, EventArgs ev)
  {
    int selectedIndex = this.box.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this.DlgSetTabAttrib(this.e.DlgInt1, selectedIndex);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private bool DlgSetTabAttrib(int TabId, int TabNo)
  {
    if (TabNo < 0 || TabNo >= this.e.TerTab[TabId].count)
    {
      this.TabTypeLeft.Checked = true;
      this.TabNone.Checked = true;
      return true;
    }
    int num = this.e.TerTab[TabId].type[TabNo];
    byte flag = this.e.TerTab[TabId].flags[TabNo];
    this.TabTypeLeft.Checked = num == 0;
    this.TabTypeRight.Checked = num == 1;
    this.TabTypeCenter.Checked = num == 2;
    this.TabTypeDecimal.Checked = num == 3;
    this.TabNone.Checked = flag == (byte) 0;
    this.TabDot.Checked = flag == (byte) 1;
    this.TabHyph.Checked = flag == (byte) 2;
    this.TabUline.Checked = flag == (byte) 4;
    return true;
  }

  private void InitializeComponent()
  {
    this.OK = new Button();
    this.Cancel = new Button();
    this.Units = new Label();
    this.box = new ComboBox();
    this.groupBox1 = new GroupBox();
    this.TabTypeLeft = new RadioButton();
    this.TabTypeRight = new RadioButton();
    this.TabTypeCenter = new RadioButton();
    this.TabTypeDecimal = new RadioButton();
    this.groupBox2 = new GroupBox();
    this.TabNone = new RadioButton();
    this.TabDot = new RadioButton();
    this.TabHyph = new RadioButton();
    this.TabUline = new RadioButton();
    this.groupBox3 = new GroupBox();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(64 /*0x40*/, 168);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(152, 168);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.Units.Location = new Point(8, 16 /*0x10*/);
    this.Units.Name = "Units";
    this.Units.Size = new Size(120, 16 /*0x10*/);
    this.Units.TabIndex = 6;
    this.Units.Text = "Tab position in Inches";
    this.box.Location = new Point(144 /*0x90*/, 16 /*0x10*/);
    this.box.Name = "box";
    this.box.Size = new Size(72, 21);
    this.box.TabIndex = 7;
    this.box.SelectedIndexChanged += new EventHandler(this.box_SelectedIndexChanged);
    this.groupBox1.Controls.AddRange(new Control[4]
    {
      (Control) this.TabTypeLeft,
      (Control) this.TabTypeRight,
      (Control) this.TabTypeCenter,
      (Control) this.TabTypeDecimal
    });
    this.groupBox1.Location = new Point(16 /*0x10*/, 48 /*0x30*/);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(96 /*0x60*/, 96 /*0x60*/);
    this.groupBox1.TabIndex = 8;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Tab Type";
    this.TabTypeLeft.Location = new Point(8, 24);
    this.TabTypeLeft.Name = "TabTypeLeft";
    this.TabTypeLeft.Size = new Size(72, 16 /*0x10*/);
    this.TabTypeLeft.TabIndex = 0;
    this.TabTypeLeft.Text = "Left";
    this.TabTypeRight.Location = new Point(8, 40);
    this.TabTypeRight.Name = "TabTypeRight";
    this.TabTypeRight.Size = new Size(72, 16 /*0x10*/);
    this.TabTypeRight.TabIndex = 1;
    this.TabTypeRight.Text = "Right";
    this.TabTypeCenter.Location = new Point(8, 56);
    this.TabTypeCenter.Name = "TabTypeCenter";
    this.TabTypeCenter.Size = new Size(72, 16 /*0x10*/);
    this.TabTypeCenter.TabIndex = 2;
    this.TabTypeCenter.Text = "Center";
    this.TabTypeDecimal.Location = new Point(8, 72);
    this.TabTypeDecimal.Name = "TabTypeDecimal";
    this.TabTypeDecimal.Size = new Size(72, 16 /*0x10*/);
    this.TabTypeDecimal.TabIndex = 3;
    this.TabTypeDecimal.Text = "Decimal";
    this.groupBox2.Controls.AddRange(new Control[4]
    {
      (Control) this.TabNone,
      (Control) this.TabDot,
      (Control) this.TabHyph,
      (Control) this.TabUline
    });
    this.groupBox2.Location = new Point(120, 48 /*0x30*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(88, 96 /*0x60*/);
    this.groupBox2.TabIndex = 9;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Tab Leader";
    this.TabNone.Location = new Point(8, 24);
    this.TabNone.Name = "TabNone";
    this.TabNone.Size = new Size(72, 16 /*0x10*/);
    this.TabNone.TabIndex = 0;
    this.TabNone.Text = "None";
    this.TabDot.Location = new Point(8, 40);
    this.TabDot.Name = "TabDot";
    this.TabDot.Size = new Size(72, 16 /*0x10*/);
    this.TabDot.TabIndex = 1;
    this.TabDot.Text = "Dots";
    this.TabHyph.Location = new Point(8, 56);
    this.TabHyph.Name = "TabHyph";
    this.TabHyph.Size = new Size(72, 16 /*0x10*/);
    this.TabHyph.TabIndex = 2;
    this.TabHyph.Text = "Hyphens";
    this.TabUline.Location = new Point(8, 72);
    this.TabUline.Name = "TabUline";
    this.TabUline.Size = new Size(72, 16 /*0x10*/);
    this.TabUline.TabIndex = 3;
    this.TabUline.Text = "Underline";
    this.groupBox3.Controls.AddRange(new Control[4]
    {
      (Control) this.Units,
      (Control) this.box,
      (Control) this.groupBox1,
      (Control) this.groupBox2
    });
    this.groupBox3.Location = new Point(8, 8);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(224 /*0xE0*/, 152);
    this.groupBox3.TabIndex = 10;
    this.groupBox3.TabStop = false;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(240 /*0xF0*/, 197);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox3,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_set_tab);
    this.Text = "Set a Tab Position";
    this.Load += new EventHandler(this.terdlg_set_tab_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    double inches = this.ctl.ToDouble(this.box.Text);
    if (this.metric)
      inches = (double) this.ctl.CmToInches((float) inches);
    int twips = (int) this.ctl.InchesToTwips(inches);
    if (twips <= 0)
    {
      this.ctl.MessageBeep(0);
      this.DialogResult = DialogResult.None;
      this.box.Focus();
    }
    else
    {
      this.e.DlgInt2 = 0;
      if (this.TabTypeRight.Checked)
        this.e.DlgInt2 = 1;
      else if (this.TabTypeCenter.Checked)
        this.e.DlgInt2 = 2;
      else if (this.TabTypeDecimal.Checked)
        this.e.DlgInt2 = 3;
      this.e.DlgInt3 = 0;
      if (this.TabDot.Checked)
        this.e.DlgInt3 = 1;
      else if (this.TabHyph.Checked)
        this.e.DlgInt3 = 2;
      else if (this.TabUline.Checked)
        this.e.DlgInt3 = 4;
      this.e.DlgInt1 = twips;
    }
  }

  private void terdlg_set_tab_Activated(object sender, EventArgs ev) => this.box.Focus();

  private void terdlg_set_tab_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.metric = this.ctl.True(this.e.TerFlags & 2);
    if (this.metric)
      this.Units.Text = this.e.MsgString[188];
    int TabId = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].TabId : this.e.StyleId[this.e.CurSID].TabId;
    this.e.DlgInt1 = TabId;
    for (int index = 0; index < this.e.TerTab[TabId].count; ++index)
    {
      double x = (double) this.e.TerTab[TabId].pos[index] / 1440.0;
      if (this.metric)
        x = (double) this.ctl.InchesToCm((float) x);
      this.box.Items.Add((object) $"{x:f2}");
    }
    if (this.e.TerTab[TabId].count > 0)
      this.box.SelectedIndex = 0;
    if (this.ctl.IsLineRtl(this.e.CurLine))
    {
      this.TabTypeLeft.Text = "Right";
      this.TabTypeRight.Text = "Left";
    }
    this.DlgSetTabAttrib(TabId, 0);
  }
}
