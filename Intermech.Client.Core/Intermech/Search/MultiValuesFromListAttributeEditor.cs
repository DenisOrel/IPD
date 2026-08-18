
// Type: Intermech.Search.MultiValuesFromListAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class MultiValuesFromListAttributeEditor : AttributeEditor
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckedListBox _checkedListBox;

  public MultiValuesFromListAttributeEditor()
  {
    this.InitializeComponent();
    this._checkedListBox.DisplayMember = "Item2";
    this._checkedListBox.ValueMember = "Item1";
  }

  protected override void DoSetValue()
  {
    this._checkedListBox.ItemCheck -= new ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
    try
    {
      for (int index = 0; index < this._checkedListBox.Items.Count; ++index)
      {
        Tuple<object, string> tuple = (Tuple<object, string>) this._checkedListBox.Items[index];
        if (this.Values != null && ((IEnumerable<object>) this.Values).Contains<object>(tuple.Item1))
          this._checkedListBox.SetItemCheckState(index, CheckState.Checked);
        else
          this._checkedListBox.SetItemCheckState(index, CheckState.Unchecked);
      }
    }
    finally
    {
      this._checkedListBox.ItemCheck += new ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
    }
  }

  protected override void DoInitializeEditor()
  {
    this._checkedListBox.BeginUpdate();
    try
    {
      this._checkedListBox.Items.Clear();
      if (this.AttributeType == null || this.AttributeType.PossibleValues == null)
        return;
      for (int index = 0; index < this.AttributeType.PossibleValues.Count; ++index)
      {
        string str = this.AttributeType.PossibleValuesDescriptions[index] == null || object.Equals(this.AttributeType.PossibleValuesDescriptions[index], (object) string.Empty) ? this.AttributeType.PossibleValues[index].ToString() : this.AttributeType.PossibleValuesDescriptions[index].ToString();
        this._checkedListBox.Items.Add((object) new Tuple<object, string>(this.AttributeType.PossibleValues[index], str));
      }
    }
    finally
    {
      this._checkedListBox.EndUpdate();
    }
  }

  private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    List<object> objectList = new List<object>();
    for (int index = 0; index < this._checkedListBox.Items.Count; ++index)
    {
      if (this._checkedListBox.CheckedIndices.Contains(index) && (e.Index != index || e.NewValue == CheckState.Checked) || e.Index == index && e.NewValue == CheckState.Checked)
        objectList.Add(((Tuple<object, string>) this._checkedListBox.Items[index]).Item1);
    }
    this.SetValues(objectList.Count > 0 ? objectList.ToArray() : (object[]) null, false);
    this.OnValueChanged();
  }

  private void CheckedListBox_KeyUp(object sender, KeyEventArgs e) => this.HandleKeyUp(e.KeyCode);

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
    this._checkedListBox = new CheckedListBox();
    this.SuspendLayout();
    this._checkedListBox.CheckOnClick = true;
    this._checkedListBox.Dock = DockStyle.Fill;
    this._checkedListBox.FormattingEnabled = true;
    this._checkedListBox.Location = new Point(0, 0);
    this._checkedListBox.Name = "_checkedListBox";
    this._checkedListBox.Size = new Size(200, 150);
    this._checkedListBox.TabIndex = 0;
    this._checkedListBox.ItemCheck += new ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
    this._checkedListBox.KeyUp += new KeyEventHandler(this.CheckedListBox_KeyUp);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._checkedListBox);
    this.MinimumSize = new Size(0, 100);
    this.Name = nameof (MultiValuesFromListAttributeEditor);
    this.Size = new Size(200, 150);
    this.ResumeLayout(false);
  }
}
