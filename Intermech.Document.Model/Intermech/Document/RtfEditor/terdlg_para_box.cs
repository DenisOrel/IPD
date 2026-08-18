// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_para_box
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_para_box : Form
{
  private Button BorderColor;
  private CheckBox BoxBetween;
  private CheckBox BoxBot;
  private CheckBox BoxDouble;
  private CheckBox BoxLeft;
  private CheckBox BoxRight;
  private CheckBox BoxThick;
  private CheckBox BoxTop;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private GroupBox groupBox3;
  private Label label1;
  private Button OK;
  private bool SamePfmt;
  private TextBox Shading;

  internal terdlg_para_box(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void BorderColor_Click(object sender, EventArgs ev)
  {
    this.e.DlgColor1 = this.ctl.DlgEditColor((Control) this, this.e.DlgColor1, true);
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
    this.BoxBetween = new CheckBox();
    this.BoxBot = new CheckBox();
    this.BoxTop = new CheckBox();
    this.BoxRight = new CheckBox();
    this.BoxLeft = new CheckBox();
    this.groupBox2 = new GroupBox();
    this.BoxThick = new CheckBox();
    this.BoxDouble = new CheckBox();
    this.label1 = new Label();
    this.Shading = new TextBox();
    this.groupBox3 = new GroupBox();
    this.BorderColor = new Button();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(88, 184);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(176 /*0xB0*/, 184);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.Add((Control) this.BoxBetween);
    this.groupBox1.Controls.Add((Control) this.BoxBot);
    this.groupBox1.Controls.Add((Control) this.BoxTop);
    this.groupBox1.Controls.Add((Control) this.BoxRight);
    this.groupBox1.Controls.Add((Control) this.BoxLeft);
    this.groupBox1.Location = new Point(8, 16 /*0x10*/);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(96 /*0x60*/, 136);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Sides";
    this.BoxBetween.Checked = true;
    this.BoxBetween.CheckState = CheckState.Indeterminate;
    this.BoxBetween.Location = new Point(16 /*0x10*/, 104);
    this.BoxBetween.Name = "BoxBetween";
    this.BoxBetween.Size = new Size(72, 16 /*0x10*/);
    this.BoxBetween.TabIndex = 4;
    this.BoxBetween.Text = "Between";
    this.BoxBot.Checked = true;
    this.BoxBot.CheckState = CheckState.Indeterminate;
    this.BoxBot.Location = new Point(16 /*0x10*/, 72);
    this.BoxBot.Name = "BoxBot";
    this.BoxBot.Size = new Size(64 /*0x40*/, 16 /*0x10*/);
    this.BoxBot.TabIndex = 3;
    this.BoxBot.Text = "Bottom";
    this.BoxTop.Checked = true;
    this.BoxTop.CheckState = CheckState.Indeterminate;
    this.BoxTop.Location = new Point(16 /*0x10*/, 56);
    this.BoxTop.Name = "BoxTop";
    this.BoxTop.Size = new Size(72, 16 /*0x10*/);
    this.BoxTop.TabIndex = 2;
    this.BoxTop.Text = "Top";
    this.BoxRight.Checked = true;
    this.BoxRight.CheckState = CheckState.Indeterminate;
    this.BoxRight.Location = new Point(16 /*0x10*/, 32 /*0x20*/);
    this.BoxRight.Name = "BoxRight";
    this.BoxRight.Size = new Size(72, 16 /*0x10*/);
    this.BoxRight.TabIndex = 1;
    this.BoxRight.Text = "Right";
    this.BoxLeft.Checked = true;
    this.BoxLeft.CheckState = CheckState.Indeterminate;
    this.BoxLeft.Location = new Point(16 /*0x10*/, 16 /*0x10*/);
    this.BoxLeft.Name = "BoxLeft";
    this.BoxLeft.Size = new Size(56, 16 /*0x10*/);
    this.BoxLeft.TabIndex = 0;
    this.BoxLeft.Text = "Left";
    this.groupBox2.Controls.Add((Control) this.BoxThick);
    this.groupBox2.Controls.Add((Control) this.BoxDouble);
    this.groupBox2.Location = new Point(112 /*0x70*/, 16 /*0x10*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(120, 72);
    this.groupBox2.TabIndex = 7;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Type";
    this.BoxThick.Checked = true;
    this.BoxThick.CheckState = CheckState.Indeterminate;
    this.BoxThick.Location = new Point(16 /*0x10*/, 48 /*0x30*/);
    this.BoxThick.Name = "BoxThick";
    this.BoxThick.Size = new Size(88, 16 /*0x10*/);
    this.BoxThick.TabIndex = 2;
    this.BoxThick.Text = "Thick Frame";
    this.BoxDouble.Checked = true;
    this.BoxDouble.CheckState = CheckState.Indeterminate;
    this.BoxDouble.Location = new Point(16 /*0x10*/, 24);
    this.BoxDouble.Name = "BoxDouble";
    this.BoxDouble.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.BoxDouble.TabIndex = 1;
    this.BoxDouble.Text = "Double Frame";
    this.label1.Location = new Point(112 /*0x70*/, 96 /*0x60*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(64 /*0x40*/, 16 /*0x10*/);
    this.label1.TabIndex = 8;
    this.label1.Text = "Shading %";
    this.Shading.Location = new Point(176 /*0xB0*/, 96 /*0x60*/);
    this.Shading.Name = "Shading";
    this.Shading.Size = new Size(56, 20);
    this.Shading.TabIndex = 9;
    this.Shading.Text = "0";
    this.groupBox3.Controls.Add((Control) this.BorderColor);
    this.groupBox3.Controls.Add((Control) this.groupBox1);
    this.groupBox3.Controls.Add((Control) this.groupBox2);
    this.groupBox3.Controls.Add((Control) this.Shading);
    this.groupBox3.Controls.Add((Control) this.label1);
    this.groupBox3.Location = new Point(8, 8);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(248, 168);
    this.groupBox3.TabIndex = 10;
    this.groupBox3.TabStop = false;
    this.BorderColor.Location = new Point(112 /*0x70*/, 128 /*0x80*/);
    this.BorderColor.Name = "BorderColor";
    this.BorderColor.Size = new Size(120, 24);
    this.BorderColor.TabIndex = 10;
    this.BorderColor.Text = "Border Color...";
    this.BorderColor.Click += new EventHandler(this.BorderColor_Click);
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(264, 213);
    this.Controls.Add((Control) this.groupBox3);
    this.Controls.Add((Control) this.Cancel);
    this.Controls.Add((Control) this.OK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_para_box);
    this.Text = "Paragraph Box Parameters";
    this.Load += new EventHandler(this.terdlg_para_box_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.DlgOnFlags = this.e.DlgOffFlags = 0;
    switch (this.BoxTop.CheckState)
    {
      case CheckState.Checked:
        this.e.DlgOnFlags |= 16 /*0x10*/;
        goto case CheckState.Indeterminate;
      case CheckState.Indeterminate:
        switch (this.BoxBot.CheckState)
        {
          case CheckState.Checked:
            this.e.DlgOnFlags |= 32 /*0x20*/;
            goto case CheckState.Indeterminate;
          case CheckState.Indeterminate:
            switch (this.BoxBetween.CheckState)
            {
              case CheckState.Checked:
                this.e.DlgOnFlags |= 65536 /*0x010000*/;
                goto case CheckState.Indeterminate;
              case CheckState.Indeterminate:
                switch (this.BoxLeft.CheckState)
                {
                  case CheckState.Checked:
                    this.e.DlgOnFlags |= 64 /*0x40*/;
                    goto case CheckState.Indeterminate;
                  case CheckState.Indeterminate:
                    switch (this.BoxRight.CheckState)
                    {
                      case CheckState.Checked:
                        this.e.DlgOnFlags |= 128 /*0x80*/;
                        goto case CheckState.Indeterminate;
                      case CheckState.Indeterminate:
                        switch (this.BoxDouble.CheckState)
                        {
                          case CheckState.Checked:
                            this.e.DlgOnFlags |= 256 /*0x0100*/;
                            goto case CheckState.Indeterminate;
                          case CheckState.Indeterminate:
                            switch (this.BoxThick.CheckState)
                            {
                              case CheckState.Checked:
                                this.e.DlgOnFlags |= 512 /*0x0200*/;
                                goto case CheckState.Indeterminate;
                              case CheckState.Indeterminate:
                                this.e.TempString = this.Shading.Text;
                                this.e.TempString.Trim();
                                return;
                              default:
                                this.e.DlgOffFlags |= 512 /*0x0200*/;
                                goto case CheckState.Indeterminate;
                            }
                          default:
                            this.e.DlgOffFlags |= 256 /*0x0100*/;
                            goto case CheckState.Indeterminate;
                        }
                      default:
                        this.e.DlgOffFlags |= 128 /*0x80*/;
                        goto case CheckState.Indeterminate;
                    }
                  default:
                    this.e.DlgOffFlags |= 64 /*0x40*/;
                    goto case CheckState.Indeterminate;
                }
              default:
                this.e.DlgOffFlags |= 65536 /*0x010000*/;
                goto case CheckState.Indeterminate;
            }
          default:
            this.e.DlgOffFlags |= 32 /*0x20*/;
            goto case CheckState.Indeterminate;
        }
      default:
        this.e.DlgOffFlags |= 16 /*0x10*/;
        goto case CheckState.Indeterminate;
    }
  }

  private void terdlg_para_box_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.SamePfmt = true;
    int num;
    int shading;
    if (this.e.EditingParaStyle)
    {
      num = this.e.StyleId[this.e.CurSID].ParaFlags;
      shading = this.e.StyleId[this.e.CurSID].shading;
      this.e.DlgColor1 = this.e.StyleId[this.e.CurSID].ParaBorderColor;
      this.SamePfmt = true;
    }
    else
    {
      int pfmt = this.e.text[this.e.CurLine].pfmt;
      num = this.e.PfmtId[pfmt].flags;
      shading = this.e.PfmtId[pfmt].shading;
      this.e.DlgColor1 = this.e.PfmtId[pfmt].BorderColor;
      if (this.e.par.IsLineRtl(this.e.CurLine))
      {
        CheckBox boxLeft = this.BoxLeft;
        this.BoxLeft = this.BoxRight;
        this.BoxRight = boxLeft;
      }
      if (this.e.HilightType != 0)
      {
        int LineNo = this.e.HilightBegRow + 1;
        while (LineNo <= this.e.HilightEndRow && (!this.e.ctl.LineSelected(LineNo) || (num & 1008) == (this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 1008) && shading == this.e.PfmtId[this.e.text[LineNo].pfmt].shading))
          ++LineNo;
        if (LineNo <= this.e.HilightEndRow)
          this.SamePfmt = false;
      }
    }
    if (this.SamePfmt)
    {
      this.BoxTop.CheckState = (num & 16 /*0x10*/) != 0 ? CheckState.Checked : CheckState.Unchecked;
      this.BoxBot.CheckState = (num & 32 /*0x20*/) != 0 ? CheckState.Checked : CheckState.Unchecked;
      this.BoxBetween.CheckState = (num & 65536 /*0x010000*/) != 0 ? CheckState.Checked : CheckState.Unchecked;
      this.BoxLeft.CheckState = (num & 64 /*0x40*/) != 0 ? CheckState.Checked : CheckState.Unchecked;
      this.BoxRight.CheckState = (num & 128 /*0x80*/) != 0 ? CheckState.Checked : CheckState.Unchecked;
      this.BoxDouble.CheckState = (num & 256 /*0x0100*/) != 0 ? CheckState.Checked : CheckState.Unchecked;
      this.BoxThick.CheckState = (num & 512 /*0x0200*/) != 0 ? CheckState.Checked : CheckState.Unchecked;
    }
    if (!this.SamePfmt)
      return;
    this.Shading.Text = (shading / 100).ToString();
  }
}
