
// Type: Intermech.PropertyEditors.ValidationRuleConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ValidationRuleEditor.</summary>
public class ValidationRuleConverter : TypeConverter
{
  public static readonly string ValidationRule_EnableEmptyAttr = LocalizationHolder.rm.GetString("Client.Core_988");
  public static readonly string ValidationRule_DisableObjectsDelete = LocalizationHolder.rm.GetString("Client.Core_989");

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return typeof (string) == sourceType || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (value == null || value is string && (string) value == ValidationRuleConverter.ValidationRule_EnableEmptyAttr)
      return (object) string.Empty;
    return value is string && (string) value == ValidationRuleConverter.ValidationRule_DisableObjectsDelete ? (object) "Value" : base.ConvertFrom(context, culture, value);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return typeof (string) == destinationType || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(typeof (string) == destinationType))
      return base.ConvertTo(context, culture, value, destinationType);
    return value != null && value.ToString() == "Value" ? (object) ValidationRuleConverter.ValidationRule_DisableObjectsDelete : (object) ValidationRuleConverter.ValidationRule_EnableEmptyAttr;
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    return new TypeConverter.StandardValuesCollection((ICollection) new string[2]
    {
      string.Empty,
      "Value"
    });
  }
}
