// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.FormDesignerEventsConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Конвертер для свойства ...</summary>
public class FormDesignerEventsConverter : TypeConverter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return base.ConvertFrom(context, culture, value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return value != null && value != DBNull.Value ? (object) LocalizationHolder.rm.GetString("FormDesigner_FormProp_Events_ConverterText") : (object) string.Empty;
  }
}
