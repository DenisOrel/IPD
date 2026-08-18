// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.terdlg_bookmark
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class terdlg_bookmark : Form
{
  private ComboBox box;
  private Button Cancel;
  private System.ComponentModel.Container components;
  private CCtl ctl;
  private Button Delete;
  private ImRtfEditor e;
  private Button GoTo;
  private GroupBox groupBox1;
  private Button Insert;
  private Button PageRef;

  internal terdlg_bookmark(ImRtfEditor parent)
  {
    this.components = (System.ComponentModel.Container) null;
    this.e = parent;
    this.ctl = this.e.ctl;
    this.InitializeComponent();
  }

  private void box_DoubleClick(object sender, EventArgs ev)
  {
    string selectedItem = (string) this.box.SelectedItem;
    int num = this.ctl.IsValidBookmark(selectedItem, false) ? 1 : 0;
    bool flag = this.ctl.IsValidBookmark(selectedItem, true);
    if (num == 0)
      return;
    this.e.TempString = selectedItem;
    this.e.DlgInt1 = !flag ? 0 : 2;
    this.DialogResult = DialogResult.OK;
    this.Hide();
  }

  private void box_TextChanged(object sender, EventArgs ev)
  {
    string text = this.box.Text;
    bool flag1 = this.ctl.IsValidBookmark(text, false);
    bool flag2 = this.ctl.IsValidBookmark(text, true);
    this.Insert.Enabled = flag1;
    this.Delete.Enabled = flag2;
    this.GoTo.Enabled = flag2;
    this.PageRef.Enabled = flag2;
    if (flag1 && !flag2)
      this.AcceptButton = (IButtonControl) this.Insert;
    if (!flag2)
      return;
    this.AcceptButton = (IButtonControl) this.GoTo;
  }

  private void Delete_Click(object sender, EventArgs ev)
  {
    this.e.TempString = this.box.Text;
    this.e.DlgInt1 = 1;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void GoTo_Click(object sender, EventArgs ev)
  {
    this.e.TempString = this.box.Text;
    this.e.DlgInt1 = 2;
  }

  private void InitializeComponent()
  {
    this.Cancel = new Button();
    this.groupBox1 = new GroupBox();
    this.GoTo = new Button();
    this.Delete = new Button();
    this.Insert = new Button();
    this.box = new ComboBox();
    this.PageRef = new Button();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.Cancel.DialogResult = DialogResult.Cancel;
    this.Cancel.Location = new Point(176 /*0xB0*/, 264);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(152, 24);
    this.Cancel.TabIndex = 5;
    this.Cancel.Text = "Cancel";
    this.groupBox1.Controls.Add((Control) this.PageRef);
    this.groupBox1.Controls.Add((Control) this.GoTo);
    this.groupBox1.Controls.Add((Control) this.Delete);
    this.groupBox1.Controls.Add((Control) this.Insert);
    this.groupBox1.Controls.Add((Control) this.box);
    this.groupBox1.Location = new Point(16 /*0x10*/, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(312, 256 /*0x0100*/);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.GoTo.DialogResult = DialogResult.OK;
    this.GoTo.Location = new Point(160 /*0xA0*/, 192 /*0xC0*/);
    this.GoTo.Name = "GoTo";
    this.GoTo.Size = new Size(144 /*0x90*/, 24);
    this.GoTo.TabIndex = 3;
    this.GoTo.Text = "Go To";
    this.GoTo.Click += new EventHandler(this.GoTo_Click);
    this.Delete.DialogResult = DialogResult.OK;
    this.Delete.Location = new Point(8, 224 /*0xE0*/);
    this.Delete.Name = "Delete";
    this.Delete.Size = new Size(136, 24);
    this.Delete.TabIndex = 2;
    this.Delete.Text = "Delete";
    this.Delete.Click += new EventHandler(this.Delete_Click);
    this.Insert.DialogResult = DialogResult.OK;
    this.Insert.Location = new Point(8, 192 /*0xC0*/);
    this.Insert.Name = "Insert";
    this.Insert.Size = new Size(136, 24);
    this.Insert.TabIndex = 1;
    this.Insert.Text = "Insert";
    this.Insert.Click += new EventHandler(this.Insert_Click);
    this.box.DropDownStyle = ComboBoxStyle.Simple;
    this.box.Location = new Point(8, 16 /*0x10*/);
    this.box.Name = "box";
    this.box.Size = new Size(296, 168);
    this.box.Sorted = true;
    this.box.TabIndex = 0;
    this.box.DoubleClick += new EventHandler(this.box_DoubleClick);
    this.box.TextChanged += new EventHandler(this.box_TextChanged);
    this.PageRef.DialogResult = DialogResult.OK;
    this.PageRef.Location = new Point(160 /*0xA0*/, 224 /*0xE0*/);
    this.PageRef.Name = "PageRef";
    this.PageRef.Size = new Size(144 /*0x90*/, 24);
    this.PageRef.TabIndex = 4;
    this.PageRef.Text = "Insert Page Reference";
    this.PageRef.Click += new EventHandler(this.PageRef_Click);
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this.Cancel;
    this.ClientSize = new Size(336, 293);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.Cancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (terdlg_bookmark);
    this.Text = "Bookmark";
    this.Load += new EventHandler(this.terdlg_bookmark_Load);
    this.Activated += new EventHandler(this.terdlg_bookmark_Activated);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void Insert_Click(object sender, EventArgs ev)
  {
    this.e.TempString = this.box.Text;
    this.e.DlgInt1 = 0;
  }

  private void PageRef_Click(object sender, EventArgs ev)
  {
    this.e.TempString = this.box.Text;
    this.e.DlgInt1 = 956;
  }

  private void terdlg_bookmark_Activated(object sender, EventArgs e) => this.box.Focus();

  private void terdlg_bookmark_Load(object sender, EventArgs ev)
  {
    this.e.misc.CenterDlgBox((Form) sender);
    string name1 = "";
    string str1 = "";
    string str2;
    if (this.ctl.GetTag(this.e.CurLine, this.e.CurCol, 1, out name1, out str2, out int _) == 0)
      name1 = "";
    int bookmark = this.e.TerGetBookmark(-1, out str2);
    for (int index = 0; index < bookmark; ++index)
    {
      string name2;
      this.e.TerGetBookmark(index, out name2);
      this.box.Items.Add((object) name2);
      if (name2 == name1 && name1.Length > 0)
        str1 = name2;
    }
    if (str1.Length >= 0)
      this.box.SelectedItem = (object) str1;
    this.Insert.Enabled = false;
    if (bookmark != 0 && !(str1 == ""))
      return;
    this.Delete.Enabled = false;
    this.GoTo.Enabled = false;
    this.PageRef.Enabled = false;
  }
}
