// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.BaseTypeConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core.FormDesigner.Controls;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class BaseTypeConverter : TypeConverter
{
  protected BidirectHashtable _hash = new BidirectHashtable();
  protected TypeConverter.StandardValuesCollection _values;

  internal BidirectHashtable Hash => this._hash;

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return !(value.GetType() == typeof (string)) ? base.ConvertFrom(context, culture, value) : this._hash[value];
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return !(destinationType == typeof (string)) ? base.ConvertTo(context, culture, value, destinationType) : this._hash[value];
  }

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
}
