// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTableDestColorsConverter
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Expert.Editor.Table;

internal class ExpertTableDestColorsConverter : TypeConverter
{
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof (ExpertTableDestColors), attributes);
    return new PropertyDescriptorCollection((PropertyDescriptor[]) null)
    {
      (PropertyDescriptor) new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties.Find("Header", false), (object) null),
      (PropertyDescriptor) new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties.Find("Data", false), (object) null)
    }.Sort(new string[2]{ "Header", "Data" });
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType.Equals(typeof (string)) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return destinationType.Equals(typeof (string)) ? (object) string.Empty : base.ConvertTo(context, culture, value, destinationType);
  }
}
