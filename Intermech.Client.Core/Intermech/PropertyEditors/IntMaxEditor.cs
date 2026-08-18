
// Type: Intermech.PropertyEditors.IntMaxEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>Редактор с кнопкой, возвращающей Int.MaxValue</summary>
public class IntMaxEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    return (object) int.MaxValue;
  }
}
