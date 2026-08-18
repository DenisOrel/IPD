// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.BaseDropDownEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class BaseDropDownEditor : UITypeEditor
{
  /// <summary>
  /// 
  /// </summary>
  private IWindowsFormsEditorService _svc;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void ListBoxClick(object sender, EventArgs e)
  {
    if (this._svc == null)
      return;
    this._svc.CloseDropDown();
  }

  /// <summary>Создание эдитора.</summary>
  /// <param name="provider">Provider</param>
  /// <param name="height">Высота элемента</param>
  /// <param name="values">Значения</param>
  /// <param name="selValue">Выбранное значение (при открытии)</param>
  /// <returns>Выбранное значение (при закрытии)</returns>
  public object SetEditor(
    System.IServiceProvider provider,
    int height,
    ICollection values,
    object selValue)
  {
    this._svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    ListBox listBox = new ListBox();
    listBox.BorderStyle = BorderStyle.None;
    listBox.Height = height;
    listBox.Items.AddRange(values.OfType<object>().ToArray<object>());
    listBox.Sorted = true;
    listBox.SelectedItem = selValue;
    listBox.Click += new EventHandler(this.ListBoxClick);
    this._svc.DropDownControl((Control) listBox);
    listBox.Click -= new EventHandler(this.ListBoxClick);
    return listBox.SelectedItem;
  }
}
