// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_paste_spec
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_paste_spec : Form
{
  private ListBox box;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private ImRtfEditor e;
  private GroupBox groupBox1;
  private Label label1;
  private Button OK;

  internal terdlg_paste_spec(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void AddFormat(ListBox box, DataObject data, string format)
  {
    if (!data.GetDataPresent(format))
      return;
    string ArgItem = format;
    if (format == "MetaFilePict")
      ArgItem = "Picture";
    if (format == "DeviceIndependentBitmap")
      ArgItem = "Bitmap";
    if (format == "EnhancedMetafile")
      ArgItem = "Enhanced Metafile";
    box.Items.Add((object) new tc.ClsBox(ArgItem, format));
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
    this.box = new ListBox();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.OK.DialogResult = DialogResult.OK;
    this.OK.Location = new Point(32 /*0x20*/, 168);
    this.OK.Name = "OK";
    this.OK.Size = new Size(80 /*0x50*/, 24);
    this.OK.TabIndex = 4;
    this.OK.Text = "OK";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(120, 168);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(80 /*0x50*/, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.AddRange(new Control[2]
    {
      (Control) this.label1,
      (Control) this.box
    });
    this.groupBox1.Location = new Point(8, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(192 /*0xC0*/, 160 /*0xA0*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.box.Location = new Point(8, 40);
    this.box.Name = "box";
    this.box.Size = new Size(176 /*0xB0*/, 108);
    this.box.TabIndex = 0;
    this.label1.Location = new Point(8, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(136, 16 /*0x10*/);
    this.label1.TabIndex = 1;
    this.label1.Text = "Available data formats";
    this.AcceptButton = (IButtonControl) this.OK;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(208 /*0xD0*/, 197);
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.groupBox1,
      (Control) this.Cancel,
      (Control) this.OK
    });
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_paste_spec);
    this.Text = "Paste Special";
    this.Load += new EventHandler(this.terdlg_paste_spec_Load);
    this.Activated += new EventHandler(this.terdlg_paste_spec_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void OK_Click(object sender, EventArgs ev)
  {
    if (this.box.SelectedIndex < 0)
      return;
    this.e.DlgText = ((tc.ClsBox) this.box.SelectedItem).TextValue;
  }

  private void terdlg_paste_spec_Activated(object sender, EventArgs ev) => this.box.Focus();

  private void terdlg_paste_spec_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    this.e.DlgText = "";
    DataObject dataObject = (DataObject) Clipboard.GetDataObject();
    if (dataObject == null)
      return;
    string str = ",Text,UnicodeText,Rich Text Format,DeviceIndependentBitmap,MetaFilePict,EnhancedMetafile,";
    string[] formats = dataObject.GetFormats();
    for (int index = 0; index < formats.Length; ++index)
    {
      if (str.IndexOf($",{formats[index]},") >= 0)
        this.AddFormat(this.box, dataObject, formats[index]);
    }
    if (this.box.Items.Count <= 0)
      return;
    this.box.SelectedIndex = 0;
  }
}
