// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ValueFromListConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class ValueFromListConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value == null || value == DBNull.Value || context == null)
      return (object) null;
    string key = value.ToString();
    if (string.IsNullOrEmpty(key))
      return (object) null;
    return !(context.Instance is StructureEditorPropGridDescriptor instance) || instance.PossibleValues == null || !instance.PossibleValues.ContainsKey(key) ? value : instance.PossibleValues[key];
  }
}
