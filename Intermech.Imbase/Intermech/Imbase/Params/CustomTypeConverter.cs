// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.CustomTypeConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Params;

public class CustomTypeConverter : TypeConverter
{
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    return !(context.PropertyDescriptor is CustomPropertyDescriptor propertyDescriptor) ? (PropertyDescriptorCollection) null : propertyDescriptor.ChildProperties;
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    return !(context.PropertyDescriptor is CustomPropertyDescriptor propertyDescriptor) ? base.GetPropertiesSupported(context) : propertyDescriptor.PropertiesSupported;
  }
}
