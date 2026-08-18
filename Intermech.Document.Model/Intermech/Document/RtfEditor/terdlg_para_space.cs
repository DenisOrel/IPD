// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_para_space
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_para_space : Form
{
  internal ComboBox box;
  internal Button Cancel;
  internal System.ComponentModel.Container components;
  internal CCtl ctl;
  internal int CurIdx;
  internal ImRtfEditor e;
  internal GroupBox groupBox1;
  internal GroupBox groupBox2;
  internal int l;
  internal Label label1;
  internal Label label2;
  internal int LineSpacing;
  internal float multiple;
  internal Button OK;
  internal int ParaId;
  internal TextBox ParaSpace;
  internal TextBox ParaSpaceAft;
  internal TextBox ParaSpaceBef;
  internal Label ParaSpaceLbl;
  internal bool SamePfmt;
  internal int SpaceBetween;

  internal terdlg_para_space(ImRtfEditor parent)
  {
    this.SamePfmt = true;
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  internal void box_SelectedIndexChanged(object sender, EventArgs ev)
  {
    this.CurIdx = this.box.SelectedIndex;
    this.e.par.SetParaSpaceDlg(this, this.CurIdx, this.e.DlgInt3, this.e.DlgInt4);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  internal void InitializeComponent()
  {
    this.OK = new Button();
    this.Cancel = new Button();
    this.groupBox1 = new GroupBox();
    this.ParaSpace = new TextBox();
    this.box = new ComboBox();
    this.groupBox2 = new GroupBox();
    this.ParaSpaceAft = new TextBox();
    this.ParaSpaceBef = new TextBox();
    this.label2 = new Label();
    this.label1 = new Label();
    this.ParaSpaceLbl = new Label();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(40, 144 /*0x90*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(128 /*0x80*/, 144 /*0x90*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[3]
    {
      (Control) this.ParaSpaceLbl,
      (Control) this.ParaSpace,
      (Control) this.box
    });
    this.groupBox1.Location = new Point(8, 8);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(240 /*0xF0*/, 56);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Line Spacing";
    this.ParaSpace.Location = new Point(104, 24);
    this.ParaSpace.Name = "ParaSpace";
    this.ParaSpace.Size = new Size(56, 20);
    this.ParaSpace.TabIndex = 1;
    this.ParaSpace.Text = "";
    this.box.Location = new Point(8, 24);
    this.box.Name = "box";
    this.box.Size = new Size(80 /*0x50*/, 21);
    this.box.TabIndex = 0;
    this.box.SelectedIndexChanged += new EventHandler(this.box_SelectedIndexChanged);
    this.groupBox2.Controls.AddRange(new Control[4]
    {
      (Control) this.ParaSpaceAft,
      (Control) this.ParaSpaceBef,
      (Control) this.label2,
      (Control) this.label1
    });
    this.groupBox2.Location = new Point(8, 64 /*0x40*/);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(240 /*0xF0*/, 72);
    this.groupBox2.TabIndex = 7;
    this.groupBox2.TabStop = false;
    this.ParaSpaceAft.Location = new Point(176 /*0xB0*/, 40);
    this.ParaSpaceAft.Name = "ParaSpaceAft";
    this.ParaSpaceAft.Size = new Size(48 /*0x30*/, 20);
    this.ParaSpaceAft.TabIndex = 3;
    this.ParaSpaceAft.Text = "";
    this.ParaSpaceBef.Location = new Point(176 /*0xB0*/, 16 /*0x10*/);
    this.ParaSpaceBef.Name = "ParaSpaceBef";
    this.ParaSpaceBef.Size = new Size(48 /*0x30*/, 20);
    this.ParaSpaceBef.TabIndex = 2;
    this.ParaSpaceBef.Text = "";
    this.label2.Location = new Point(8, 40);
    this.label2.Name = "label2";
    this.label2.Size = new Size(168, 16 /*0x10*/);
    this.label2.TabIndex = 1;
    this.label2.Text = "Space After Paragraph (points)";
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(176 /*0xB0*/, 16 /*0x10*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Space Before Paragraph (points)";
    this.ParaSpaceLbl.Location = new Point(168, 24);
    this.ParaSpaceLbl.Name = "ParaSpaceLbl";
    this.ParaSpaceLbl.Size = new Size(40, 16 /*0x10*/);
    this.ParaSpaceLbl.TabIndex = 2;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(256 /*0x0100*/, 173);
    this.Controls.AddRange(new Control[4]
    {
      (Control) this.groupBox2,
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_para_space);
    this.Text = "Paragraph Spacing Parameters";
    this.Load += new EventHandler(this.terdlg_para_space_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  internal void OK_Click(object sender, EventArgs ev)
  {
    if (!this.e.ctl.ToInt((Form) this, this.ParaSpaceBef, 1) || !this.e.ctl.ToInt((Form) this, this.ParaSpaceAft, 2))
      return;
    this.CurIdx = this.box.SelectedIndex;
    this.SpaceBetween = this.LineSpacing = 0;
    if (this.CurIdx == 1)
      this.LineSpacing = 50;
    if (this.CurIdx == 2)
      this.LineSpacing = 100;
    if (this.CurIdx == 5)
    {
      this.e.TempString = this.ParaSpace.Text;
      this.multiple = (float) this.e.ctl.ToDouble(this.e.TempString);
      if ((double) this.multiple < 0.5)
        this.multiple = 0.5f;
      if ((double) this.multiple > 9.0)
        this.multiple = 9f;
      this.LineSpacing = (int) ((double) this.multiple * 100.0 - 100.0);
    }
    if (this.CurIdx == 3 && !this.ctl.ToInt((Form) this, this.ParaSpace, out this.SpaceBetween))
      return;
    if (this.CurIdx == 4)
    {
      if (!this.ctl.ToInt((Form) this, this.ParaSpace, out this.SpaceBetween))
        return;
      this.SpaceBetween = -this.SpaceBetween;
    }
    this.e.DlgInt3 = this.SpaceBetween;
    this.e.DlgInt4 = this.LineSpacing;
  }

  internal void terdlg_para_space_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.box.Items.Add((object) new tc.ClsBox(this.e.MsgString[181], 0));
    this.box.Items.Add((object) new tc.ClsBox(this.e.MsgString[182], 1));
    this.box.Items.Add((object) new tc.ClsBox(this.e.MsgString[183], 2));
    this.box.Items.Add((object) new tc.ClsBox(this.e.MsgString[197], 3));
    this.box.Items.Add((object) new tc.ClsBox(this.e.MsgString[184], 4));
    this.box.Items.Add((object) new tc.ClsBox(this.e.MsgString[185], 5));
    this.box.SelectedIndex = 0;
    this.ParaSpace.Enabled = false;
    this.ParaSpaceLbl.Text = "";
    if (this.e.EditingParaStyle)
    {
      this.SamePfmt = true;
    }
    else
    {
      if (this.e.HilightType != 0)
      {
        this.l = this.e.HilightBegRow + 1;
        while (this.l <= this.e.HilightEndRow && (!this.e.ctl.LineSelected(this.l) || this.e.text[this.l].pfmt == this.e.text[this.e.HilightBegRow].pfmt))
          ++this.l;
        if (this.l <= this.e.HilightEndRow)
          this.SamePfmt = false;
      }
      this.ParaId = this.e.text[this.e.CurLine].pfmt;
    }
    this.SpaceBetween = 12;
    this.LineSpacing = 0;
    if (this.SamePfmt)
    {
      if (this.e.EditingParaStyle)
      {
        this.ParaSpaceBef.Text = this.e.ctl.TwipsToPoints(this.e.StyleId[this.e.CurSID].SpaceBefore).ToString();
        this.ParaSpaceAft.Text = this.e.ctl.TwipsToPoints(this.e.StyleId[this.e.CurSID].SpaceAfter).ToString();
        this.SpaceBetween = (int) this.e.ctl.TwipsToPoints(this.e.StyleId[this.e.CurSID].SpaceBetween);
        this.LineSpacing = this.e.StyleId[this.e.CurSID].LineSpacing;
        if ((this.e.StyleId[this.e.CurSID].ParaFlags & 4) != 0)
          this.LineSpacing = 100;
      }
      else
      {
        this.ParaSpaceBef.Text = this.e.ctl.TwipsToPoints(this.e.PfmtId[this.ParaId].SpaceBefore).ToString();
        this.ParaSpaceAft.Text = this.e.ctl.TwipsToPoints(this.e.PfmtId[this.ParaId].SpaceAfter).ToString();
        this.SpaceBetween = (int) this.e.ctl.TwipsToPoints(this.e.PfmtId[this.ParaId].SpaceBetween);
        this.LineSpacing = this.e.PfmtId[this.ParaId].LineSpacing;
        if ((this.e.PfmtId[this.ParaId].flags & 4) != 0)
          this.LineSpacing = 100;
      }
      this.CurIdx = this.LineSpacing != 0 ? (this.LineSpacing != 50 ? (this.LineSpacing != 100 ? 5 : 2) : 1) : (this.SpaceBetween != 0 ? (this.SpaceBetween <= 0 ? 4 : 3) : 0);
      this.e.par.SetParaSpaceDlg(this, this.CurIdx, this.SpaceBetween, this.LineSpacing);
    }
    this.e.DlgInt3 = this.SpaceBetween;
    this.e.DlgInt4 = this.LineSpacing;
  }
}
