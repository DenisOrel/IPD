
// Type: Intermech.PropertyEditors.ConvertersHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

public class ConvertersHolder
{
  public static TypeConverter GetTypeConverterByType(Type type)
  {
    TypeConverter typeConverterByType = (TypeConverter) null;
    if (type == typeof (bool))
      typeConverterByType = (TypeConverter) new BooleanConverter();
    if (type == typeof (int))
      typeConverterByType = (TypeConverter) new Int32Converter();
    if (type == typeof (long))
      typeConverterByType = (TypeConverter) new Int64Converter();
    if (type == typeof (DateTime))
      typeConverterByType = (TypeConverter) new DateTimeConverter();
    if (type == typeof (double))
      typeConverterByType = (TypeConverter) new DoubleConverter();
    if (type == typeof (string))
      typeConverterByType = (TypeConverter) new StringConverter();
    return typeConverterByType;
  }
}
