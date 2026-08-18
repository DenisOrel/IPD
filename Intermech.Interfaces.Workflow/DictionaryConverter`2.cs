// Decompiled with JetBrains decompiler
// Type: Intermech.DictionaryConverter`2
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech;

/// <summary>
/// Позволяет записывать типизированные значения из словаря Dict, отображая для пользователя соответствующие Dict.Values
/// </summary>
/// <typeparam name="T1">Реальный тип значений</typeparam>
/// <typeparam name="T2">Тип отображаемых значений</typeparam>
public class DictionaryConverter<T1, T2> : TypeConverter
{
  private Dictionary<T1, T2> _dict;
  private TypeConverter.StandardValuesCollection _standardValues;

  protected virtual Dictionary<T1, T2> Dict => this._dict;

  public DictionaryConverter(Dictionary<T1, T2> values) => this._dict = values;

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (T2) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (value is T2)
    {
      foreach (KeyValuePair<T1, T2> keyValuePair in this.Dict)
      {
        if (keyValuePair.Value.Equals(value))
          return (object) keyValuePair.Key;
      }
    }
    return base.ConvertFrom(context, culture, value);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (T1) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    T2 obj;
    return value is T1 key && this.Dict.TryGetValue(key, out obj) ? (object) obj : base.ConvertTo(context, culture, value, destinationType);
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (this._standardValues == null)
      this._standardValues = new TypeConverter.StandardValuesCollection((ICollection) this.Dict.Values);
    return this._standardValues;
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
}
