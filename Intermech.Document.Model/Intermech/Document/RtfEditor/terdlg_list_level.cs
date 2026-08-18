// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_list_level
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_list_level : Form
{
  private ComboBox box;
  private Button Cancel;
  private ComboBox CharAft;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private Button Fonts;
  private GroupBox group1;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private GroupBox groupBox3;
  private GroupBox groupBox4;
  private Label label1;
  private Label label2;
  private Label label3;
  private Label label4;
  private Label label5;
  private Label label6;
  private Label label7;
  private Label label8;
  private Label label9;
  private CheckBox Legal;
  private ComboBox LevelBox;
  private RadioButton ListItem;
  private RadioButton ListOr;
  private ComboBox ListOrBox;
  private CheckBox NoReset;
  private TextBox NumText;
  private ComboBox NumType;
  private Button OK;
  private CheckBox Reformat;
  private CheckBox RestartNum;
  private TextBox StartAt;

  internal terdlg_list_level(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void box_SelectedIndexChanged(object sender, EventArgs ev)
  {
    this.ListItem_CheckedChanged(sender, ev);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void Fonts_Click(object sender, EventArgs ev)
  {
    this.e.DlgInt1 = this.e.par.DlgEditListFont(this.e.DlgInt1);
  }

  private void InitializeComponent()
  {
    this.OK = new Button();
    this.Cancel = new Button();
    this.groupBox1 = new GroupBox();
    this.groupBox4 = new GroupBox();
    this.Fonts = new Button();
    this.NumType = new ComboBox();
    this.CharAft = new ComboBox();
    this.label9 = new Label();
    this.label8 = new Label();
    this.StartAt = new TextBox();
    this.label7 = new Label();
    this.group1 = new GroupBox();
    this.label6 = new Label();
    this.label5 = new Label();
    this.label4 = new Label();
    this.label3 = new Label();
    this.NumText = new TextBox();
    this.label2 = new Label();
    this.groupBox3 = new GroupBox();
    this.NoReset = new CheckBox();
    this.Reformat = new CheckBox();
    this.Legal = new CheckBox();
    this.RestartNum = new CheckBox();
    this.groupBox2 = new GroupBox();
    this.LevelBox = new ComboBox();
    this.label1 = new Label();
    this.ListOrBox = new ComboBox();
    this.box = new ComboBox();
    this.ListOr = new RadioButton();
    this.ListItem = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.group1.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(392, 352);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(480, 352);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[4]
    {
      (Control) this.groupBox4,
      (Control) this.group1,
      (Control) this.groupBox3,
      (Control) this.groupBox2
    });
    this.groupBox1.Location = new Point(8, 8);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(552, 336);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox4.Controls.AddRange(new Control[7]
    {
      (Control) this.Fonts,
      (Control) this.NumType,
      (Control) this.CharAft,
      (Control) this.label9,
      (Control) this.label8,
      (Control) this.StartAt,
      (Control) this.label7
    });
    this.groupBox4.Location = new Point(336, 112 /*0x70*/);
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.Size = new Size(208 /*0xD0*/, 208 /*0xD0*/);
    this.groupBox4.TabIndex = 3;
    this.groupBox4.TabStop = false;
    this.Fonts.Location = new Point(88, 168);
    this.Fonts.Name = "Fonts";
    this.Fonts.Size = new Size(104, 24);
    this.Fonts.TabIndex = 6;
    this.Fonts.Text = "Number Font...";
    this.Fonts.Click += new EventHandler(this.Fonts_Click);
    this.NumType.Location = new Point(40, 104);
    this.NumType.Name = "NumType";
    this.NumType.Size = new Size(152, 21);
    this.NumType.TabIndex = 5;
    this.NumType.SelectedIndexChanged += new EventHandler(this.NumType_SelectedIndexChanged);
    this.CharAft.Location = new Point(104, 48 /*0x30*/);
    this.CharAft.Name = "CharAft";
    this.CharAft.Size = new Size(88, 21);
    this.CharAft.TabIndex = 4;
    this.label9.Location = new Point(8, 88);
    this.label9.Name = "label9";
    this.label9.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.label9.TabIndex = 3;
    this.label9.Text = "Number Type";
    this.label8.Location = new Point(8, 56);
    this.label8.Name = "label8";
    this.label8.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.label8.TabIndex = 2;
    this.label8.Text = "Character After";
    this.StartAt.Location = new Point(104, 16 /*0x10*/);
    this.StartAt.Name = "StartAt";
    this.StartAt.Size = new Size(88, 20);
    this.StartAt.TabIndex = 1;
    this.StartAt.Text = "";
    this.label7.Location = new Point(8, 24);
    this.label7.Name = "label7";
    this.label7.Size = new Size(72, 16 /*0x10*/);
    this.label7.TabIndex = 0;
    this.label7.Text = "Start at";
    this.group1.Controls.AddRange(new Control[6]
    {
      (Control) this.label6,
      (Control) this.label5,
      (Control) this.label4,
      (Control) this.label3,
      (Control) this.NumText,
      (Control) this.label2
    });
    this.group1.Location = new Point(8, 208 /*0xD0*/);
    this.group1.Name = "group1";
    this.group1.Size = new Size(320, 112 /*0x70*/);
    this.group1.TabIndex = 2;
    this.group1.TabStop = false;
    this.label6.Location = new Point(8, 88);
    this.label6.Name = "label6";
    this.label6.Size = new Size(304, 16 /*0x10*/);
    this.label6.TabIndex = 5;
    this.label6.Text = "    (2) Second item.";
    this.label5.Location = new Point(8, 72);
    this.label5.Name = "label5";
    this.label5.Size = new Size(304, 16 /*0x10*/);
    this.label5.TabIndex = 4;
    this.label5.Text = "    (1) First item.";
    this.label4.Location = new Point(8, 40);
    this.label4.Name = "label4";
    this.label4.Size = new Size(304, 16 /*0x10*/);
    this.label4.TabIndex = 3;
    this.label4.Text = "The list level number must be surrounded by a pair of '~'";
    this.label3.Location = new Point(8, 56);
    this.label3.Name = "label3";
    this.label3.Size = new Size(304, 16 /*0x10*/);
    this.label3.TabIndex = 2;
    this.label3.Text = "Example: (~1~) might print as";
    this.NumText.Location = new Point(80 /*0x50*/, 16 /*0x10*/);
    this.NumText.Name = "NumText";
    this.NumText.Size = new Size(224 /*0xE0*/, 20);
    this.NumText.TabIndex = 1;
    this.NumText.Text = "";
    this.label2.Location = new Point(8, 16 /*0x10*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(72, 16 /*0x10*/);
    this.label2.TabIndex = 0;
    this.label2.Text = "Number text";
    this.groupBox3.Controls.AddRange(new Control[4]
    {
      (Control) this.NoReset,
      (Control) this.Reformat,
      (Control) this.Legal,
      (Control) this.RestartNum
    });
    this.groupBox3.Location = new Point(8, 112 /*0x70*/);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(320, 88);
    this.groupBox3.TabIndex = 1;
    this.groupBox3.TabStop = false;
    this.NoReset.Location = new Point(8, 64 /*0x40*/);
    this.NoReset.Name = "NoReset";
    this.NoReset.Size = new Size(304, 16 /*0x10*/);
    this.NoReset.TabIndex = 3;
    this.NoReset.Text = "Do not reset numbering when the parent level changes";
    this.Reformat.Location = new Point(8, 48 /*0x30*/);
    this.Reformat.Name = "Reformat";
    this.Reformat.Size = new Size(272, 16 /*0x10*/);
    this.Reformat.TabIndex = 2;
    this.Reformat.Text = "Override font attribute of the list level";
    this.Legal.Location = new Point(8, 32 /*0x20*/);
    this.Legal.Name = "Legal";
    this.Legal.Size = new Size(296, 16 /*0x10*/);
    this.Legal.TabIndex = 1;
    this.Legal.Text = "Change previous level number to an Arabic format";
    this.RestartNum.Location = new Point(8, 16 /*0x10*/);
    this.RestartNum.Name = "RestartNum";
    this.RestartNum.Size = new Size(296, 16 /*0x10*/);
    this.RestartNum.TabIndex = 0;
    this.RestartNum.Text = "Restart numbering when override changes for a list";
    this.groupBox2.Controls.AddRange(new Control[6]
    {
      (Control) this.LevelBox,
      (Control) this.label1,
      (Control) this.ListOrBox,
      (Control) this.box,
      (Control) this.ListOr,
      (Control) this.ListItem
    });
    this.groupBox2.Location = new Point(8, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(536, 88);
    this.groupBox2.TabIndex = 0;
    this.groupBox2.TabStop = false;
    this.LevelBox.Location = new Point(472, 56);
    this.LevelBox.Name = "LevelBox";
    this.LevelBox.Size = new Size(48 /*0x30*/, 21);
    this.LevelBox.Sorted = true;
    this.LevelBox.TabIndex = 5;
    this.LevelBox.SelectedIndexChanged += new EventHandler(this.LevelBox_SelectedIndexChanged);
    this.label1.Location = new Point(416, 56);
    this.label1.Name = "label1";
    this.label1.Size = new Size(56, 16 /*0x10*/);
    this.label1.TabIndex = 4;
    this.label1.Text = "List level";
    this.ListOrBox.Location = new Point(96 /*0x60*/, 56);
    this.ListOrBox.Name = "ListOrBox";
    this.ListOrBox.Size = new Size(200, 21);
    this.ListOrBox.Sorted = true;
    this.ListOrBox.TabIndex = 3;
    this.ListOrBox.SelectedIndexChanged += new EventHandler(this.ListOrBox_SelectedIndexChanged);
    this.box.Location = new Point(96 /*0x60*/, 16 /*0x10*/);
    this.box.Name = "box";
    this.box.Size = new Size(200, 21);
    this.box.Sorted = true;
    this.box.TabIndex = 2;
    this.box.SelectedIndexChanged += new EventHandler(this.box_SelectedIndexChanged);
    this.ListOr.Location = new Point(8, 56);
    this.ListOr.Name = "ListOr";
    this.ListOr.Size = new Size(88, 16 /*0x10*/);
    this.ListOr.TabIndex = 1;
    this.ListOr.Text = "List override";
    this.ListItem.Location = new Point(8, 16 /*0x10*/);
    this.ListItem.Name = "ListItem";
    this.ListItem.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.ListItem.TabIndex = 0;
    this.ListItem.Text = "List item";
    this.ListItem.CheckedChanged += new EventHandler(this.ListItem_CheckedChanged);
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(568, 381);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_list_level);
    this.Text = "List Level properties";
    this.Load += new EventHandler(this.terdlg_list_level_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox4.ResumeLayout(false);
    this.group1.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void LevelBox_SelectedIndexChanged(object sender, EventArgs ev)
  {
    this.e.par.SetDlgListLevelProp(this.ListItem.Checked, this.box, this.ListOrBox, this.LevelBox, this.RestartNum, this.Legal, this.Reformat, this.NoReset, this.StartAt, this.NumType, this.CharAft, this.NumText);
  }

  private void ListItem_CheckedChanged(object sender, EventArgs ev)
  {
    this.e.par.SetDlgListLevel(this.ListItem.Checked, this.box, this.ListOrBox, this.LevelBox);
    this.e.par.SetDlgListLevelProp(this.ListItem.Checked, this.box, this.ListOrBox, this.LevelBox, this.RestartNum, this.Legal, this.Reformat, this.NoReset, this.StartAt, this.NumType, this.CharAft, this.NumText);
  }

  private void ListOrBox_SelectedIndexChanged(object sender, EventArgs ev)
  {
    this.ListItem_CheckedChanged(sender, ev);
  }

  private void NumType_SelectedIndexChanged(object sender, EventArgs e)
  {
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    tc.StrListLevel pLevel;
    int id;
    int level;
    if (!this.e.par.GetDlgListLevelPtr(this.ListItem.Checked, this.box, this.ListOrBox, this.LevelBox, out pLevel, out id, out level))
    {
      this.DialogResult = DialogResult.None;
    }
    else
    {
      tc.ResetUintFlag(ref pLevel.flags, 57);
      if (this.RestartNum.Checked)
        pLevel.flags |= 1;
      if (this.Legal.Checked)
        pLevel.flags |= 8;
      if (this.Reformat.Checked)
        pLevel.flags |= 16 /*0x10*/;
      if (this.NoReset.Checked)
        pLevel.flags |= 32 /*0x20*/;
      pLevel.text = new char[100];
      this.e.par.CodeListText(this.NumText.Text, pLevel.text);
      pLevel.start = this.e.misc.ToInt(this.StartAt);
      pLevel.NumType = this.e.par.DlgListNumType(this.NumType, pLevel.NumType, false);
      pLevel.CharAft = this.e.par.DlgListCharAft(this.CharAft, pLevel.CharAft, false);
      pLevel.FontId = this.e.DlgInt1;
      if (this.ListItem.Checked)
        this.e.list[id].level[level] = pLevel;
      else
        this.e.ListOr[id].level[level] = pLevel;
    }
  }

  private void terdlg_list_level_Activated(object sender, EventArgs ev)
  {
  }

  private void terdlg_list_level_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.e.par.FillListBox((object) this.box, true, false, -1);
    int index = this.e.par.FillListOrBox((object) this.ListOrBox, true, false, -1, true);
    this.ListItem.Checked = index <= 0 || this.e.ListOr[index].LevelCount == 0;
    this.ListOr.Checked = !this.ListItem.Checked;
    this.e.par.SetDlgListLevel(this.ListItem.Checked, this.box, this.ListOrBox, this.LevelBox);
    int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
    if (bltId > 0 && this.e.TerBlt[bltId].ls > 0)
      this.LevelBox.SelectedIndex = this.e.TerBlt[bltId].lvl;
    this.e.par.SetDlgListLevelProp(this.ListItem.Checked, this.box, this.ListOrBox, this.LevelBox, this.RestartNum, this.Legal, this.Reformat, this.NoReset, this.StartAt, this.NumType, this.CharAft, this.NumText);
  }
}
