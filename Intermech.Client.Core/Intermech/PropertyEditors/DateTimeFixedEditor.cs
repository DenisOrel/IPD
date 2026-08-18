
// Type: Intermech.PropertyEditors.DateTimeFixedEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace Intermech.PropertyEditors;

/// <summary>
/// Пофикшенный стандартный редактор: если несколько раз выбирать текущую дату, то повторно выдает дату + текущее время, а не дату+0:00:00, как обычно
/// </summary>
public class DateTimeFixedEditor : DateTimeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    object obj = base.EditValue(context, sp, value);
    if (obj is DateTime dateTime)
      obj = (object) dateTime.Date;
    return obj;
  }
}
