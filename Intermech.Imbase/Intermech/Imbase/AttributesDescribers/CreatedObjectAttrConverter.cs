// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.CreatedObjectAttrConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

public class CreatedObjectAttrConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value == null || value == DBNull.Value)
      value = (object) new ObjectTypeAttProxy(Guid.Empty);
    return (object) value.ToString();
  }
}
