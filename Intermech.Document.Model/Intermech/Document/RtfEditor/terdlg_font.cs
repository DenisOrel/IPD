// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_font
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_font : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private ComboBox FontPoints;
  private ComboBox FontTypes;
  private Label label1;
  private Label label2;
  private Button OK;

  internal terdlg_font(ImRtfEditor parent)
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
    this.FontTypes = new ComboBox();
    this.label1 = new Label();
    this.FontPoints = new ComboBox();
    this.label2 = new Label();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(24, 240 /*0xF0*/);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(112 /*0x70*/, 240 /*0xF0*/);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.FontTypes.DropDownStyle = ComboBoxStyle.Simple;
    this.FontTypes.Location = new Point(8, 32 /*0x20*/);
    this.FontTypes.Name = "FontTypes";
    this.FontTypes.Size = new Size(120, 200);
    this.FontTypes.Sorted = true;
    this.FontTypes.TabIndex = 6;
    this.label1.Location = new Point(40, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(64 /*0x40*/, 16 /*0x10*/);
    this.label1.TabIndex = 7;
    this.label1.Text = "Face Name";
    this.FontPoints.DropDownStyle = ComboBoxStyle.Simple;
    this.FontPoints.Location = new Point(144 /*0x90*/, 32 /*0x20*/);
    this.FontPoints.Name = "FontPoints";
    this.FontPoints.Size = new Size(64 /*0x40*/, 200);
    this.FontPoints.Sorted = true;
    this.FontPoints.TabIndex = 8;
    this.label2.Location = new Point(160 /*0xA0*/, 8);
    this.label2.Name = "label2";
    this.label2.Size = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.label2.TabIndex = 9;
    this.label2.Text = "Point";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.AcceptButton = (IButtonControl) this.OK;
    this.ClientSize = new Size(216, 269);
    this.Controls.AddRange(new Control[6]
    {
      (Control) this.label2,
      (Control) this.FontPoints,
      (Control) this.label1,
      (Control) this.FontTypes,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_font);
    this.Text = "Font Selection";
    this.Load += new EventHandler(this.terdlg_font_Load);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.ReqTypeFace = this.FontTypes.Text;
    this.e.ReqTwipsSize = (int) (this.e.ctl.ToDouble(this.FontPoints.Text) * 20.0);
  }

  private void terdlg_font_Load(object sender, EventArgs ev)
  {
    int dlgInt1 = this.e.DlgInt1;
    this.e.misc.CenterDlgBox((Form) sender);
    int num1 = this.ctl.FillFontBox(this.FontTypes);
    if (this.e.CurSID >= 0 && this.e.StyleId[this.e.CurSID].TypeFace.Length == 0)
    {
      this.e.ReqTypeFace = "";
    }
    else
    {
      int index;
      for (index = 0; index < num1; ++index)
      {
        string str = (string) this.FontTypes.Items[index];
        if (this.e.CurSID < 0 && str == this.e.TerFont[dlgInt1].TypeFace || this.e.CurSID >= 0 && str == this.e.StyleId[this.e.CurSID].TypeFace)
          break;
      }
      if (index < num1)
        this.FontTypes.SelectedIndex = index;
      this.e.ReqTypeFace = this.e.CurSID >= 0 ? this.e.StyleId[this.e.CurSID].TypeFace : this.e.TerFont[dlgInt1].TypeFace;
    }
    this.e.ReqTwipsSize = this.e.CurSID < 0 ? this.e.TerFont[dlgInt1].TwipsSize : this.e.StyleId[this.e.CurSID].TwipsSize;
    this.ctl.FillPointBox(this.FontPoints);
    this.FontTypes.Text = this.e.ReqTypeFace;
    int reqTwipsSize = this.e.ReqTwipsSize;
    int num2 = reqTwipsSize / 20;
    string str1 = num2.ToString();
    if (reqTwipsSize > num2 * 20)
      str1 += ".5";
    this.FontPoints.Text = str1;
  }
}
