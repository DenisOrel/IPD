// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.AttributeSettingsConverter
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Expressions;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

public class AttributeSettingsConverter : TypeConverter
{
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (Variable) || destinationType.IsSubclassOf(typeof (Variable)) || base.CanConvertTo(context, destinationType);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (TemplateAttribute) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value is TemplateAttribute templateAttribute ? (object) templateAttribute.Attribute : base.ConvertFrom(context, culture, value);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (destinationType == typeof (string))
      return (value is AttributeSettings attributeSettings ? (object) attributeSettings.GetText() : (object) (string) null) ?? (object) string.Empty;
    if (!(destinationType == typeof (Variable)))
      return base.ConvertTo(context, culture, value, destinationType);
    return !(value is AttributeSettings attribute) ? (object) null : (object) new TemplateAttribute(attribute);
  }
}
