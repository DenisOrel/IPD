// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.FieldTypeConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class FieldTypeConverter : TypeConverter
{
  public FieldTypeConverter(Type type)
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value == null)
      return (object) null;
    int result;
    return int.TryParse(value.ToString(), out result) ? (object) ((FieldTypes) result).GetAttribute<DescriptionAttribute>().Description : value;
  }
}
