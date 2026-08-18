// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.RadioGroup
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class RadioGroup : GroupBox
{
  private List<RadioButton> _items = new List<RadioButton>();
  private int _updateCounter;
  public EventHandler SelectedIndexChanged;

  public RadioButton[] Items => this._items.ToArray();

  protected override void OnControlAdded(ControlEventArgs e)
  {
    if (e.Control is RadioButton)
    {
      RadioButton control = e.Control as RadioButton;
      this._items.Add(control);
      control.Font = this.Font;
      control.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
    }
    base.OnControlAdded(e);
    this.LayoutButtons();
  }

  private void RadioButton_CheckedChanged(object sender, EventArgs e)
  {
    this.OnSelectedIndexChanged();
  }

  protected override void OnControlRemoved(ControlEventArgs e)
  {
    if (e.Control is RadioButton)
      this._items.Remove(e.Control as RadioButton);
    base.OnControlRemoved(e);
    if (this._updateCounter != 0)
      return;
    this.LayoutButtons();
  }

  public void Clear()
  {
    this.BeginUpdate();
    try
    {
      for (int index = this._items.Count - 1; index >= 0; --index)
        this.Controls.Remove((Control) this._items[index]);
    }
    finally
    {
      this.EndUpdate();
    }
  }

  public void BeginUpdate() => ++this._updateCounter;

  public void EndUpdate()
  {
    --this._updateCounter;
    if (this._updateCounter != 0)
      return;
    this.LayoutButtons();
  }

  protected void LayoutButtons()
  {
    if (this._updateCounter != 0 || this._items.Count <= 0)
      return;
    Padding padding = this.Padding;
    int num1 = padding.Top + this._items[0].Height / 2;
    int num2 = this.ClientSize.Height - num1;
    padding = this.Padding;
    int bottom = padding.Bottom;
    int num3 = (num2 - bottom) / this._items.Count;
    int num4 = num1 + (num3 / 2 - this._items[0].Height / 2);
    for (int index = 0; index < this._items.Count; ++index)
    {
      RadioButton radioButton = this._items[index];
      radioButton.Dock = DockStyle.None;
      padding = this.Padding;
      radioButton.Left = padding.Left + 8;
      radioButton.Width = this.ClientSize.Width - 16 /*0x10*/;
      radioButton.Top = num4;
      num4 += num3;
    }
  }

  protected virtual void OnSelectedIndexChanged()
  {
    if (this.SelectedIndexChanged == null)
      return;
    this.SelectedIndexChanged((object) this, EventArgs.Empty);
  }

  public int SelectedIndex
  {
    get
    {
      for (int index = 0; index < this._items.Count; ++index)
      {
        if (this._items[index].Checked)
          return index;
      }
      return -1;
    }
    set
    {
      if (value == this.SelectedIndex)
        return;
      if (value == -1)
        this._items[this.SelectedIndex].Checked = false;
      else if (value >= 0 && value < this._items.Count)
        this._items[value].Checked = true;
      this.OnSelectedIndexChanged();
    }
  }
}
