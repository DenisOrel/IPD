
// Type: Intermech.Client.Core.PropertyEditors.AttrProcessor.Editors.StandartValuesEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.PropertyEditors.AttrProcessor.Editors;

public class StandartValuesEditor : UITypeEditor
{
  protected bool sortValues;
  private TypeConverter converter;

  public StandartValuesEditor()
    : this((TypeConverter) null)
  {
  }

  public StandartValuesEditor(TypeConverter converter) => this.converter = converter;

  /// <summary>Поддерживает ли отрисовка значения</summary>
  /// <param name="context">Контекст</param>
  /// <returns>Поддерживает ли отрисовка значения</returns>
  public override bool GetPaintValueSupported(ITypeDescriptorContext context) => false;

  /// <summary>Получает стиль редактирования, используемый методом EditValue</summary>
  /// <param name="context">ITypeDescriptorContext, используемый для получения
  /// дополнительных сведений о контексте</param>
  /// <returns>Значение UITypeEditorEditStyle, которое указывает на стиль редактирования,
  /// используемый EditValue. Если UITypeEditor не поддерживает данный метод,
  /// то GetEditStyle возвращает None</returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  /// <summary>Можно ли изменять размер выпадающего списка</summary>
  public override bool IsDropDownResizable => true;

  /// <summary>Редактирует значение заданного объекта, используя стиль редактора,
  /// предоставляемого с помощью GetEditStyle</summary>
  /// <param name="context">ITypeDescriptorContext, используемый для получения
  /// дополнительных сведений о контексте</param>
  /// <param name="provider">Поставщик IServiceProvider, который использует этот редактор
  /// для обслуживания</param>
  /// <param name="value">Объект редактирования</param>
  /// <returns>Новое значение объекта</returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    IWindowsFormsEditorService formsEditorService = (IWindowsFormsEditorService) null;
    if (provider != null)
      formsEditorService = provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;
    if (formsEditorService != null)
    {
      ListBox listBox = new ListBox();
      listBox.BorderStyle = BorderStyle.None;
      listBox.Click += new EventHandler(this.listBox_Click);
      listBox.Tag = (object) formsEditorService;
      TypeConverter.StandardValuesCollection standardValues = this.converter.GetStandardValues(context);
      object obj = (object) null;
      foreach (object val in standardValues)
      {
        string caption = this.converter.ConvertToString(context, val);
        if (val == value)
          obj = val;
        listBox.Items.Add((object) new StandartValuesEditor.ListBoxItem(val, caption));
      }
      listBox.SelectedItem = obj;
      formsEditorService.DropDownControl((Control) listBox);
      if (listBox.SelectedItem != null)
        value = (listBox.SelectedItem as StandartValuesEditor.ListBoxItem).Value;
    }
    return value;
  }

  private void listBox_Click(object sender, EventArgs e)
  {
    if (!(sender is ListBox listBox) || listBox.SelectedItem == null || !(listBox.Tag is IWindowsFormsEditorService tag))
      return;
    tag.CloseDropDown();
  }

  private class ListBoxItem
  {
    private object value;
    private string caption;

    public ListBoxItem(object val, string caption)
    {
      this.Value = val;
      this.Caption = caption;
    }

    public object Value
    {
      get => this.value;
      set => this.value = value;
    }

    public string Caption
    {
      get => this.caption;
      set => this.caption = value;
    }

    public override string ToString() => this.Caption;
  }
}
