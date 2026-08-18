// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.StructureEditorBooleanConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class StructureEditorBooleanConverter : BaseTypeConverter
{
  internal StructureEditorBooleanConverter()
  {
    this._hash.Add((object) true, (object) LocalizationHolder.rm.GetString("Imbase.Table.AttributeRedactor.BoolConverter.True"));
    this._hash.Add((object) false, (object) LocalizationHolder.rm.GetString("Imbase.Table.AttributeRedactor.BoolConverter.False"));
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    bool result = false;
    if (value == null || !bool.TryParse(value.ToString(), out result))
      return value;
    return !(destinationType == typeof (string)) ? base.ConvertTo(context, culture, (object) result, destinationType) : this._hash[(object) result];
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (this._values != null)
      return this._values;
    this._values = new TypeConverter.StandardValuesCollection((ICollection) new object[2]
    {
      (object) true,
      (object) false
    });
    return this._values;
  }
}
