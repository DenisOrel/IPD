
// Type: Intermech.Client.Core.PropertyEditors.FlagsConverter`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Client.Core.PropertyEditors;

/// <summary>Конвертер для преобразования опций.</summary>
public class FlagsConverter<T> : TypeConverter
{
  /// <summary>Разделитель для элементов</summary>
  private const string ValueSeparator = ", ";

  /// <summary>Можно ли конвертировать в</summary>
  /// <param name="context"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }

  /// <summary>Конвертировать в</summary>
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
    if (destinationType != typeof (string) || !(value is Enum))
      return base.ConvertTo(context, culture, value, destinationType);
    int int32 = Convert.ToInt32(value);
    Array values1 = Enum.GetValues(typeof (T));
    List<string> values2 = new List<string>(values1.Length);
    for (int index = 0; index < values1.Length; ++index)
    {
      T obj = (T) values1.GetValue(index);
      if (Convert.ToInt32((object) obj) != 0 && (int32 | Convert.ToInt32((object) obj)) == int32)
      {
        string enumDescription = EnumDescConverter.GetEnumDescription(typeof (T), Enum.GetName(typeof (T), (object) obj));
        if (!values2.Contains(enumDescription))
          values2.Add(enumDescription);
      }
    }
    return values2.Count == 0 ? (object) EnumDescConverter.GetEnumDescription(typeof (T), Enum.GetName(typeof (T), (object) 0)) : (object) string.Join(", ", (IEnumerable<string>) values2);
  }
}
