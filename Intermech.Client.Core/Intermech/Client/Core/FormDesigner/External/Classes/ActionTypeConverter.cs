
// Type: Intermech.Client.Core.FormDesigner.External.Classes.ActionTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Client.Core.FormDesigner.External.Classes;

/// <summary>
/// 
/// </summary>
public class ActionTypeConverter : TypeConverter
{
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
    return !(destinationType == typeof (string)) ? base.ConvertTo(context, culture, value, destinationType) : (object) LocalizationHolder.rm.GetString("Client.Core_169");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="value"></param>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    return !(value is IFormDesignerActionParams component) ? (PropertyDescriptorCollection) null : TypeDescriptor.GetProperties((object) component, attributes);
  }
}
