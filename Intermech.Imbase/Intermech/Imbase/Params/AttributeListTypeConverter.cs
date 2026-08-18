// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.AttributeListTypeConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase.Params;

internal class AttributeListTypeConverter : TypeConverter
{
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
  {
    return destType == typeof (string);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destType)
  {
    return destType == typeof (string) && value is ICollection collection && collection.Count > 0 ? (object) " < Список... >" : (object) "< ... >";
  }
}
