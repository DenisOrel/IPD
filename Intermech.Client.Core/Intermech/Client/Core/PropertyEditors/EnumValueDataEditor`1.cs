
// Type: Intermech.Client.Core.PropertyEditors.EnumValueDataEditor`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.PropertyEditors;

/// <summary>Custom converter for EnumValueData</summary>
public class EnumValueDataEditor<T> : DropDownListEditor
{
  /// <summary>
  /// 
  /// </summary>
  private readonly EnumValueDataConverter<T> _typeConverter;

  /// <summary>Конструктор</summary>
  public EnumValueDataEditor()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="getListDelegate"></param>
  public EnumValueDataEditor(EventsHolder.GetListDelegate getListDelegate)
    : base(getListDelegate)
  {
    this._typeConverter = new EnumValueDataConverter<T>(getListDelegate);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return this._typeConverter.GetStandardValuesCustomList(context, args);
  }

  /// <summary>
  /// Редактирует значение заданного объекта, используя стиль редактора,
  /// предоставляемого с помощью GetEditStyle
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    IWindowsFormsEditorService formsEditorService = (IWindowsFormsEditorService) null;
    if (provider != null)
      formsEditorService = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    if (formsEditorService == null)
      return base.EditValue(context, provider, value);
    T data = (value as EnumValueData<T>).Data;
    Convert.ToInt32((object) data);
    int int32 = Convert.ToInt32((object) data);
    CheckedListBox checkedListBox = new CheckedListBox();
    checkedListBox.BorderStyle = BorderStyle.None;
    checkedListBox.CheckOnClick = true;
    checkedListBox.Height = 64 /*0x40*/;
    checkedListBox.Tag = (object) formsEditorService;
    ArrayList standardValues = this.GetStandardValues(context);
    int index1 = 0;
    for (int index2 = 0; index2 < standardValues.Count; ++index2)
    {
      EnumValueData<T> enumValueData = standardValues[index2] as EnumValueData<T>;
      checkedListBox.Items.Add((object) enumValueData);
      checkedListBox.SetItemChecked(index1, (int32 | Convert.ToInt32((object) enumValueData.Data)) == int32);
      ++index1;
    }
    formsEditorService.DropDownControl((Control) checkedListBox);
    int num = 0;
    foreach (EnumValueData<T> checkedItem in checkedListBox.CheckedItems)
      num |= Convert.ToInt32((object) checkedItem.Data);
    return (object) new EnumValueData<T>(Enum.ToObject(typeof (T), num));
  }
}
