// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ValueFromListEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Imbase;

internal class ValueFromListEditor : DropDownEditor
{
  private IWindowsFormsEditorService svc;

  private void ListBoxClick(object sender, EventArgs e) => this.svc.CloseDropDown();

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (context == null)
      return value;
    this.svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    if (!(context.Instance is StructureEditorPropGridDescriptor instance) || instance.PossibleValues == null || instance.PossibleValues.Count == 0)
      return value;
    ListBox listBox = new ListBox();
    listBox.BorderStyle = BorderStyle.None;
    string str = value != null ? value.ToString() : string.Empty;
    foreach (KeyValuePair<string, object> possibleValue in instance.PossibleValues)
    {
      int num = listBox.Items.Add((object) new ValueFromListEditor.ItemForListBox((object) possibleValue.Key, possibleValue.Value));
      if (!(possibleValue.Key != str))
        listBox.SelectedIndex = num;
    }
    listBox.Height = listBox.Items.Count <= 10 ? listBox.Items.Count * 13 + 6 : 136;
    listBox.Click += new EventHandler(this.ListBoxClick);
    this.svc.DropDownControl((Control) listBox);
    listBox.Click -= new EventHandler(this.ListBoxClick);
    AttributeTypeProperties attrTypeProps = instance.AttrTypeProps;
    if (listBox.SelectedItem != null)
    {
      attrTypeProps.DefaultValue = ((ValueFromListEditor.ItemForListBox) listBox.SelectedItem).Value;
      instance.AttrTypeProps = attrTypeProps;
    }
    return attrTypeProps.DefaultValue;
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
