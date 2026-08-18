
// Type: Intermech.PropertyEditors.AttrProcessor.UserTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// Пользовательский конвертор для работы в связке с AttributeProcessor
/// Наследуется от базового конвертора и вызывает по умолчанию методы оригинальных конверторов
/// </summary>
public class UserTypeConverter(int aAttributeId, AttributeProcessor aAttributeProcessor) : 
  CommonTypeConverter(aAttributeId, aAttributeProcessor)
{
  protected TypeConverter originalSingleValueConverter;

  /// <summary>
  /// берем оригинальный TypeConverter для вызова его по умолчанию
  /// </summary>
  protected void InitOriginalSingleValueConverter()
  {
    if (this.originalSingleValueConverter == null)
      return;
    this.originalSingleValueConverter = this.attributeProcessor.GetOriginalSingleValueConverter(this.attributeId);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    this.InitOriginalSingleValueConverter();
    if (this.originalSingleValueConverter != null)
      return this.originalSingleValueConverter.CanConvertFrom(context, sourceType);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    this.InitOriginalSingleValueConverter();
    if (this.originalSingleValueConverter != null)
      return this.originalSingleValueConverter.CanConvertTo(context, destinationType);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    this.InitOriginalSingleValueConverter();
    if (this.originalSingleValueConverter != null)
      return this.originalSingleValueConverter.ConvertFrom(context, culture, value);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    this.InitOriginalSingleValueConverter();
    if (this.originalSingleValueConverter != null)
      return this.originalSingleValueConverter.ConvertTo(context, culture, value, destinationType);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    this.InitOriginalSingleValueConverter();
    if (this.originalSingleValueConverter != null)
      return this.originalSingleValueConverter.CreateInstance(context, propertyValues);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
  {
    this.InitOriginalSingleValueConverter();
    return this.originalSingleValueConverter != null ? this.originalSingleValueConverter.GetCreateInstanceSupported(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    this.InitOriginalSingleValueConverter();
    if (this.originalSingleValueConverter != null)
      return this.originalSingleValueConverter.GetProperties(context, value, attributes);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    this.InitOriginalSingleValueConverter();
    return this.originalSingleValueConverter != null ? this.originalSingleValueConverter.GetPropertiesSupported(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    this.InitOriginalSingleValueConverter();
    return this.originalSingleValueConverter != null ? this.originalSingleValueConverter.GetStandardValues(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
  {
    this.InitOriginalSingleValueConverter();
    return this.originalSingleValueConverter != null ? this.originalSingleValueConverter.GetStandardValuesExclusive(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
  {
    this.InitOriginalSingleValueConverter();
    return this.originalSingleValueConverter != null ? this.originalSingleValueConverter.GetStandardValuesSupported(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  /// <summary>
  /// НЕ ПЕРЕКРЫВАТЬ. для перекрытия использовать IsValidExt
  /// </summary>
  /// <param name="context"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override bool IsValid(ITypeDescriptorContext context, object value)
  {
    this.InitOriginalSingleValueConverter();
    if (this.originalSingleValueConverter != null)
      return this.originalSingleValueConverter.IsValid(context, value);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool IsValidExt(
    ITypeDescriptorContext context,
    object value,
    ref List<ValidationResult> results)
  {
    this.InitOriginalSingleValueConverter();
    if (this.originalSingleValueConverter != null && this.originalSingleValueConverter is CommonTypeConverter)
      return ((CommonTypeConverter) this.originalSingleValueConverter).IsValidExt(context, value, ref results);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override IAttributeEditorControl GetEditorControl(UITypeEditorEditStyle style)
  {
    this.InitOriginalSingleValueConverter();
    return this.originalSingleValueConverter != null ? ((CommonTypeConverter) this.originalSingleValueConverter).GetEditorControl(style) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }
}
