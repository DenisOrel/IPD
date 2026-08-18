
// Type: Intermech.PropertyEditors.PasswordTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for PasswordTypeConverter.</summary>
public class PasswordTypeConverter : TypeConverter
{
  internal static string _passwordString = new string(ClientConsts.PasswordChar, 8);

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return !(sourceType == typeof (string)) && base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value is string ? value : base.ConvertFrom(context, culture, value);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return typeof (string) == destinationType || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return typeof (string) == destinationType ? (object) PasswordTypeConverter._passwordString : base.ConvertTo(context, culture, value, destinationType);
  }
}
