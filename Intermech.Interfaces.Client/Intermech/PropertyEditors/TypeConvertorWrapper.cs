// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.TypeConvertorWrapper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.PropertyEditors;

public class TypeConvertorWrapper : TypeConverter
{
  private TypeConverter typeConverter;
  private bool disableManualEdit;

  public TypeConverter WrappedTypeConverter => this.typeConverter;

  public TypeConvertorWrapper(TypeConverter typeConverter, bool disableManualEdit)
  {
    this.typeConverter = typeConverter;
    this.disableManualEdit = disableManualEdit;
  }

  public new bool CanConvertFrom(Type sourceType)
  {
    return (!this.disableManualEdit || !sourceType.Equals(typeof (string))) && this.typeConverter.CanConvertFrom(sourceType);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return (!this.disableManualEdit || !sourceType.Equals(typeof (string))) && this.typeConverter.CanConvertFrom(context, sourceType);
  }

  public new bool CanConvertTo(Type destinationType)
  {
    return this.typeConverter.CanConvertTo(destinationType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return this.typeConverter.CanConvertTo(context, destinationType);
  }

  public new object ConvertFrom(object value) => this.typeConverter.ConvertFrom(value);

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return this.typeConverter.ConvertFrom(context, culture, value);
  }

  public new object ConvertFromInvariantString(string text)
  {
    return this.typeConverter.ConvertFromInvariantString(text);
  }

  public new object ConvertFromInvariantString(ITypeDescriptorContext context, string text)
  {
    return this.typeConverter.ConvertFromInvariantString(context, text);
  }

  public new object ConvertFromString(string text) => this.typeConverter.ConvertFromString(text);

  public new object ConvertFromString(ITypeDescriptorContext context, string text)
  {
    return this.typeConverter.ConvertFromString(context, text);
  }

  public new object ConvertFromString(
    ITypeDescriptorContext context,
    CultureInfo culture,
    string text)
  {
    return this.typeConverter.ConvertFromString(context, culture, text);
  }

  public new object ConvertTo(object value, Type destinationType)
  {
    return this.typeConverter.ConvertTo(value, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return this.typeConverter.ConvertTo(context, culture, value, destinationType);
  }

  public new string ConvertToInvariantString(object value)
  {
    return this.typeConverter.ConvertToInvariantString(value);
  }

  public new string ConvertToInvariantString(ITypeDescriptorContext context, object value)
  {
    return this.typeConverter.ConvertToInvariantString(context, value);
  }

  public new string ConvertToString(object value) => this.typeConverter.ConvertToString(value);

  public new string ConvertToString(ITypeDescriptorContext context, object value)
  {
    return this.typeConverter.ConvertToString(context, value);
  }

  public new string ConvertToString(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return this.typeConverter.ConvertToString(context, culture, value);
  }

  public new object CreateInstance(IDictionary propertyValues)
  {
    return this.typeConverter.CreateInstance(propertyValues);
  }

  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    return this.typeConverter.CreateInstance(context, propertyValues);
  }

  public new bool GetCreateInstanceSupported() => this.typeConverter.GetCreateInstanceSupported();

  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
  {
    return this.typeConverter.GetCreateInstanceSupported(context);
  }

  public new PropertyDescriptorCollection GetProperties(object value)
  {
    return this.typeConverter.GetProperties(value);
  }

  public new PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value)
  {
    return this.typeConverter.GetProperties(context, value);
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    return this.typeConverter.GetProperties(context, value, attributes);
  }

  public new bool GetPropertiesSupported() => this.typeConverter.GetPropertiesSupported();

  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    return this.typeConverter.GetPropertiesSupported(context);
  }

  public new ICollection GetStandardValues() => this.typeConverter.GetStandardValues();

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    return this.typeConverter.GetStandardValues(context);
  }

  public new bool GetStandardValuesExclusive() => this.typeConverter.GetStandardValuesExclusive();

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
  {
    return this.typeConverter.GetStandardValuesExclusive(context);
  }

  public new bool GetStandardValuesSupported() => this.typeConverter.GetStandardValuesSupported();

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
  {
    return this.typeConverter.GetStandardValuesSupported(context);
  }

  public new bool IsValid(object value) => this.typeConverter.IsValid(value);

  public override bool IsValid(ITypeDescriptorContext context, object value)
  {
    return this.typeConverter.IsValid(context, value);
  }
}
