
// Type: Intermech.Client.Core.Redline.UserRankGraphsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Redline;

public class UserRankGraphsForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckedListBox checkedListBox1;
  private Button _bCancel;
  private Button _bApply;

  public UserRankGraphsForm(List<string> collection)
  {
    this.InitializeComponent();
    foreach (object obj in collection)
      this.checkedListBox1.Items.Add(obj, CheckState.Unchecked);
    Size size1 = TextRenderer.MeasureText(this.Text, SystemFonts.CaptionFont);
    this.Width = size1.Width + 16 /*0x10*/;
    Size size2 = this.Size;
    ref Size local = ref size2;
    size1 = this.Size;
    int num = Math.Max(size1.Width, this.ChangeDropDownWidth((ListBox) this.checkedListBox1) + 13);
    local.Width = num;
    this.Size = size2;
    this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
  }

  private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    CheckedListBox checkedListBox = sender as CheckedListBox;
    for (int index = 0; index < checkedListBox.Items.Count; ++index)
    {
      if (index != e.Index)
        checkedListBox.SetItemChecked(index, false);
    }
  }

  /// <summary>найти максимальную длинну для набора Items</summary>
  /// <param name="cb">страницы</param>
  /// <returns>максимальная длинна для набора Items</returns>
  private int ChangeDropDownWidth(ListBox cb)
  {
    float val1 = 0.0f;
    using (Graphics graphics = this.CreateGraphics())
    {
      IEnumerator enumerator = cb.Items.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
        val1 = Math.Max(val1, graphics.MeasureString(enumerator.Current.ToString(), cb.Font).Width);
    }
    return (int) val1 + 1;
  }

  public string SelectedItem
  {
    get
    {
      return this.checkedListBox1.SelectedItems.Count > 0 && this.checkedListBox1.GetItemChecked(this.checkedListBox1.SelectedIndex) ? this.checkedListBox1.SelectedItem as string : string.Empty;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.checkedListBox1 = new CheckedListBox();
    this._bCancel = new Button();
    this._bApply = new Button();
    this.SuspendLayout();
    this.checkedListBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.checkedListBox1.CheckOnClick = true;
    this.checkedListBox1.FormattingEnabled = true;
    this.checkedListBox1.Location = new Point(0, 0);
    this.checkedListBox1.Name = "checkedListBox1";
    this.checkedListBox1.Size = new Size(252, 184);
    this.checkedListBox1.TabIndex = 0;
    this._bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._bCancel.DialogResult = DialogResult.Cancel;
    this._bCancel.FlatStyle = FlatStyle.System;
    this._bCancel.ImeMode = ImeMode.NoControl;
    this._bCancel.Location = new Point(133, 197);
    this._bCancel.Name = "_bCancel";
    this._bCancel.Size = new Size(121, 27);
    this._bCancel.TabIndex = 3;
    this._bCancel.Text = "Отмена";
    this._bApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._bApply.DialogResult = DialogResult.OK;
    this._bApply.FlatStyle = FlatStyle.System;
    this._bApply.ImeMode = ImeMode.NoControl;
    this._bApply.Location = new Point(6, 197);
    this._bApply.Name = "_bApply";
    this._bApply.Size = new Size(121, 27);
    this._bApply.TabIndex = 2;
    this._bApply.Text = "OK";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(256 /*0x0100*/, 228);
    this.Controls.Add((Control) this._bCancel);
    this.Controls.Add((Control) this._bApply);
    this.Controls.Add((Control) this.checkedListBox1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (UserRankGraphsForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Text = "Выберите должность и графу для создания замечания";
    this.ResumeLayout(false);
  }
}
