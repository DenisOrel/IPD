// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_footnote
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_footnote : Form
{
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private TextBox FNoteChar;
  private Label FNoteMarkerLbl;
  private CheckBox FNoteSupscr;
  private TextBox FNoteText;
  private Label FNoteTextLbl;
  private GroupBox groupBox1;
  private Button OK;

  internal terdlg_footnote(ImRtfEditor parent)
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
    this.FNoteMarkerLbl = new Label();
    this.FNoteTextLbl = new Label();
    this.FNoteChar = new TextBox();
    this.FNoteText = new TextBox();
    this.FNoteSupscr = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(208 /*0xD0*/, 120);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(296, 120);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[5]
    {
      (Control) this.FNoteSupscr,
      (Control) this.FNoteText,
      (Control) this.FNoteChar,
      (Control) this.FNoteTextLbl,
      (Control) this.FNoteMarkerLbl
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(368, 112 /*0x70*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.FNoteMarkerLbl.Location = new Point(8, 16 /*0x10*/);
    this.FNoteMarkerLbl.Name = "FNoteMarkerLbl";
    this.FNoteMarkerLbl.Size = new Size(88, 16 /*0x10*/);
    this.FNoteMarkerLbl.TabIndex = 0;
    this.FNoteMarkerLbl.Text = "Footnote Marker";
    this.FNoteTextLbl.Location = new Point(8, 40);
    this.FNoteTextLbl.Name = "FNoteTextLbl";
    this.FNoteTextLbl.Size = new Size(88, 16 /*0x10*/);
    this.FNoteTextLbl.TabIndex = 1;
    this.FNoteTextLbl.Text = "Footnote Text";
    this.FNoteChar.Location = new Point(96 /*0x60*/, 14);
    this.FNoteChar.Name = "FNoteChar";
    this.FNoteChar.Size = new Size(56, 20);
    this.FNoteChar.TabIndex = 2;
    this.FNoteChar.Text = "";
    this.FNoteText.Location = new Point(8, 56);
    this.FNoteText.Name = "FNoteText";
    this.FNoteText.Size = new Size(352, 20);
    this.FNoteText.TabIndex = 3;
    this.FNoteText.Text = "";
    this.FNoteSupscr.Location = new Point(8, 88);
    this.FNoteSupscr.Name = "FNoteSupscr";
    this.FNoteSupscr.Size = new Size(176 /*0xB0*/, 16 /*0x10*/);
    this.FNoteSupscr.TabIndex = 4;
    this.FNoteSupscr.Text = "Superscript Footnote Marker";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(384, 149);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_footnote);
    this.Text = "Footnote Marker";
    this.Load += new EventHandler(this.terdlg_footnote_Load);
    this.Activated += new EventHandler(this.terdlg_footnote_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    this.e.TempString = this.FNoteChar.Text;
    if (this.e.TempString.Length == 0)
    {
      this.DialogResult = DialogResult.Cancel;
    }
    else
    {
      for (int index = 0; index < this.e.TempString.Length; ++index)
      {
        if (this.e.TempString[index] == ' ' || this.ctl.IsBreakChar(this.e.TempString[index]))
        {
          int num = (int) this.ctl.ShowMessage(this.e.MsgString[2], (string) null, MessageBoxButtons.OK);
          this.FNoteChar.Focus();
          return;
        }
      }
      this.e.TempString1 = this.FNoteText.Text;
      if (this.e.TempString1.Length == 0)
      {
        this.DialogResult = DialogResult.Cancel;
      }
      else
      {
        for (int index = 0; index < this.e.TempString1.Length; ++index)
        {
          if (this.ctl.IsBreakChar(this.e.TempString1[index]))
          {
            int num = (int) this.ctl.ShowMessage(this.e.MsgString[2], (string) null, MessageBoxButtons.OK);
            this.FNoteText.Focus();
            return;
          }
        }
        this.e.DlgInt1 = 0;
        if (!this.FNoteSupscr.Checked)
          return;
        this.e.DlgInt1 = 16 /*0x10*/;
      }
    }
  }

  private void terdlg_footnote_Activated(object sender, EventArgs e) => this.FNoteChar.Focus();

  private void terdlg_footnote_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    if (!this.e.DlgBool1)
    {
      this.FNoteMarkerLbl.Text = "Endnote Marker";
      this.FNoteTextLbl.Text = "Endnote Text";
    }
    this.FNoteChar.MaxLength = 10;
    this.FNoteText.MaxLength = 900;
    this.FNoteSupscr.Checked = true;
  }
}
