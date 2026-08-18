
// Type: Intermech.PropertyEditors.DropDownListEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

/// <summary>Редактор BorderStyles</summary>
public class DropDownListEditor : UITypeEditor, IComparer
{
  protected bool sortValues;
  private EventsHolder.GetListDelegate GetList;
  protected static CaseInsensitiveComparer _comparer = new CaseInsensitiveComparer();

  public DropDownListEditor()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public DropDownListEditor(EventsHolder.GetListDelegate getListDelegate)
  {
    this.GetList = getListDelegate;
  }

  public ArrayList GetStandardValues(ITypeDescriptorContext context)
  {
    ArrayList standardValues = (ArrayList) null;
    if (this.GetList != null)
      standardValues = this.GetList((object) this);
    if (standardValues == null)
      standardValues = this.GetStandardValuesCustomList(context);
    if (this.sortValues)
      standardValues.Sort((IComparer) this);
    return standardValues;
  }

  public virtual ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return (ArrayList) null;
  }

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
      ArrayList standardValues = this.GetStandardValues(context);
      if (standardValues != null)
        listBox.Items.AddRange(standardValues.ToArray());
      listBox.SelectedItem = value;
      formsEditorService.DropDownControl((Control) listBox);
      if (listBox.SelectedItem != null)
        value = listBox.SelectedItem;
    }
    return value;
  }

  private void listBox_Click(object sender, EventArgs e)
  {
    if (!(sender is ListBox listBox) || listBox.SelectedItem == null || !(listBox.Tag is IWindowsFormsEditorService tag))
      return;
    tag.CloseDropDown();
  }

  public virtual int Compare(object x, object y)
  {
    if (x == null && y == null)
      return 0;
    if (x == null)
      return -1;
    return y == null ? 1 : DropDownListEditor._comparer.Compare((object) x.ToString(), (object) y.ToString());
  }
}
