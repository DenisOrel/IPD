// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.PasswordConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase.Params;

public sealed class PasswordConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (destinationType != typeof (string))
      return base.ConvertTo(context, culture, value, destinationType);
    if (!(value is string str))
      return base.ConvertTo(context, culture, value, destinationType);
    return str.Length <= 0 ? (object) string.Empty : (object) new string('*', str.Length);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value is string str ? (object) str : base.ConvertFrom(context, culture, value);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }
}
