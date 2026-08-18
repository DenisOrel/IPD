
// Type: Intermech.PropertyEditors.Int64CustomConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class Int64CustomConverter : Int64Converter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override bool IsValid(ITypeDescriptorContext context, object value)
  {
    return value.ToString() == string.Empty || base.IsValid(context, value);
  }

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
    return !(value.ToString() == string.Empty) ? base.ConvertFrom(context, culture, value) : (object) null;
  }
}
