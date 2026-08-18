
// Type: Intermech.PropertyEditors.ObjectTypeAttrConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

public class ObjectTypeAttrConverter : TypeConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return !sourceType.Equals(typeof (string)) && base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return destinationType.Equals(typeof (string)) && value is ObjectTypeAttProxy ? (object) ((ObjectTypeAttProxy) value).ToString() : base.ConvertTo(context, culture, value, destinationType);
  }
}
