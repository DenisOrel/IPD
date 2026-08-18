// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.FromListEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class FromListEditor : DropDownEditor
{
  private IWindowsFormsEditorService svc;

  private void ListBoxClick(object sender, EventArgs e) => this.svc.CloseDropDown();

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    object obj = value;
    if (context != null)
    {
      this.svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      if (context.Instance is RestructuringPropGridDescriptor instance && instance.PossibleValues != null && instance.PossibleValues.Count > 0)
      {
        ListBox listBox = new ListBox();
        listBox.BorderStyle = BorderStyle.None;
        string str = Convert.ToString(value);
        foreach (KeyValuePair<string, object> possibleValue in instance.PossibleValues)
        {
          int num = listBox.Items.Add((object) new FromListEditor.ItemForListBox((object) possibleValue.Key, possibleValue.Value));
          if (!(possibleValue.Key != str))
            listBox.SelectedIndex = num;
        }
        listBox.Height = listBox.Items.Count <= 10 ? listBox.Items.Count * listBox.ItemHeight + listBox.ItemHeight / 2 : listBox.ItemHeight * 10 + listBox.ItemHeight / 2;
        listBox.Click += new EventHandler(this.ListBoxClick);
        this.svc.DropDownControl((Control) listBox);
        listBox.Click -= new EventHandler(this.ListBoxClick);
        if (listBox.SelectedItem != null)
          obj = ((FromListEditor.ItemForListBox) listBox.SelectedItem).Value;
      }
    }
    return obj;
  }

  private struct ItemForListBox
  {
    private object _descr;
    private object _value;

    internal object Description => this._descr;

    internal object Value => this._value;

    internal ItemForListBox(object value, object descr)
    {
      this._descr = descr;
      this._value = value;
    }

    public override string ToString() => this._descr.ToString();
  }
}
