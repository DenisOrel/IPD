// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.PossibleValuesFromListEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Imbase;

internal class PossibleValuesFromListEditor : DropDownEditor
{
  private IWindowsFormsEditorService svc;

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    object obj = value;
    if (context != null && context.Instance is StructureEditorPropGridDescriptor instance && instance.PossibleValues != null && instance.PossibleValues.Count > 0)
    {
      this.svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      CheckedListBox checkedListBox = new CheckedListBox();
      checkedListBox.BorderStyle = BorderStyle.None;
      checkedListBox.CheckOnClick = true;
      List<string> stringList1 = (List<string>) null;
      if (value is object[] array)
      {
        string[] collection = Array.ConvertAll<object, string>(array, new Converter<object, string>(Convert.ToString));
        stringList1 = collection.Length != 0 ? new List<string>((IEnumerable<string>) collection) : new List<string>(0);
      }
      List<string> stringList2 = stringList1 ?? new List<string>(0);
      foreach (KeyValuePair<string, object> possibleValue in instance.PossibleValues)
      {
        if (!string.IsNullOrEmpty(possibleValue.Key))
          checkedListBox.Items.Add((object) new PossibleValuesFromListEditor.CheckedListBoxItem((object) possibleValue.Key, possibleValue.Value), stringList2.Contains(possibleValue.Key));
      }
      int itemHeight = checkedListBox.GetItemHeight(0);
      int num1 = checkedListBox.Items.Count <= 10 ? checkedListBox.Items.Count : 10;
      int num2;
      checkedListBox.Height = (num2 = num1 + 1) * itemHeight + itemHeight / 2;
      this.svc.DropDownControl((Control) checkedListBox);
      if (checkedListBox.CheckedItems.Count > 0)
      {
        object[] objArray = new object[checkedListBox.CheckedItems.Count];
        for (int index = 0; index < checkedListBox.CheckedItems.Count; ++index)
          objArray[index] = ((PossibleValuesFromListEditor.CheckedListBoxItem) checkedListBox.CheckedItems[index]).Value;
        obj = (object) objArray;
      }
      else
        obj = (object) null;
    }
    return obj;
  }

  private struct CheckedListBoxItem
  {
    internal object Descr;
    internal object Value;

    internal CheckedListBoxItem(object value, object descr)
    {
      this.Descr = descr;
      this.Value = value;
    }

    public override string ToString() => this.Descr.ToString();
  }
}
