
// Type: Intermech.Client.Core.PropertyEditors.FlagsEditor`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.PropertyEditors;

/// <summary>Редактор опций</summary>
public class FlagsEditor<T> : UITypeEditor
{
  /// <summary>Возвращаем стиль редактора - выпадающее окно</summary>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return context == null ? base.GetEditStyle((ITypeDescriptorContext) null) : UITypeEditorEditStyle.DropDown;
  }

  /// <summary>Реализация метода редактирования</summary>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    IWindowsFormsEditorService formsEditorService = (IWindowsFormsEditorService) null;
    if (provider != null)
      formsEditorService = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    if (formsEditorService == null || !(value is Enum))
      return base.EditValue(context, provider, value);
    int int32_1 = Convert.ToInt32((object) (T) value);
    CheckedListBox checkedListBox1 = new CheckedListBox();
    checkedListBox1.BorderStyle = BorderStyle.None;
    checkedListBox1.CheckOnClick = true;
    checkedListBox1.Height = 64 /*0x40*/;
    checkedListBox1.Tag = (object) formsEditorService;
    CheckedListBox checkedListBox2 = checkedListBox1;
    Array values = Enum.GetValues(typeof (T));
    for (int index1 = 0; index1 < values.Length; ++index1)
    {
      T obj = (T) values.GetValue(index1);
      if (Convert.ToInt32((object) obj) != 0)
      {
        string enumDescription = EnumDescConverter.GetEnumDescription(typeof (T), Enum.GetName(typeof (T), (object) obj));
        if (!checkedListBox2.Items.Contains((object) enumDescription))
        {
          int index2 = checkedListBox2.Items.Add((object) enumDescription);
          checkedListBox2.SetItemChecked(index2, (int32_1 | Convert.ToInt32((object) obj)) == int32_1);
        }
      }
    }
    formsEditorService.DropDownControl((Control) checkedListBox2);
    int num = 0;
    foreach (string checkedItem in checkedListBox2.CheckedItems)
    {
      int int32_2 = Convert.ToInt32((object) EnumTypeHelper.GetEnumValue(typeof (T), checkedItem));
      num |= int32_2;
    }
    return Enum.ToObject(typeof (T), num);
  }
}
