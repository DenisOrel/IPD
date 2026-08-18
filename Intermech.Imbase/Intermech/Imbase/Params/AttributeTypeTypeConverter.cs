// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.AttributeTypeTypeConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase.Params;

internal class AttributeTypeTypeConverter : TypeConverter
{
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(destinationType == typeof (string)) || !(value is int attrTypeID))
      return base.ConvertTo(context, culture, value, destinationType);
    return attrTypeID != 0 ? (object) MetaDataHelper.GetAttributeTypeName(attrTypeID) : (object) "Атрибут не указан";
  }
}
