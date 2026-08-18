
// Type: Intermech.PropertyEditors.AttrProcessor.UserMultipleValuesTypeConverter
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
/// Пользовательский класс для конвертора многозначных атрибутов
/// </summary>
internal class UserMultipleValuesTypeConverter(
  int attributeId,
  AttributeProcessor attributeProcessor) : CommonTypeConverter(attributeId, attributeProcessor)
{
  protected TypeConverter originalMultipleValuesConverter;

  protected void InitOriginalMultipleValuesConverter()
  {
    if (this.originalMultipleValuesConverter != null)
      return;
    this.originalMultipleValuesConverter = this.attributeProcessor.GetOriginalMultipleValuesConverter(this.attributeId);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    this.InitOriginalMultipleValuesConverter();
    if (this.originalMultipleValuesConverter != null)
      return this.originalMultipleValuesConverter.CanConvertFrom(context, sourceType);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    this.InitOriginalMultipleValuesConverter();
    if (this.originalMultipleValuesConverter != null)
      return this.originalMultipleValuesConverter.CanConvertTo(context, destinationType);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    this.InitOriginalMultipleValuesConverter();
    if (this.originalMultipleValuesConverter != null)
      return this.originalMultipleValuesConverter.ConvertFrom(context, culture, value);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    this.InitOriginalMultipleValuesConverter();
    if (this.originalMultipleValuesConverter != null)
      return this.originalMultipleValuesConverter.ConvertTo(context, culture, value, destinationType);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    this.InitOriginalMultipleValuesConverter();
    if (this.originalMultipleValuesConverter != null)
      return this.originalMultipleValuesConverter.CreateInstance(context, propertyValues);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
  {
    this.InitOriginalMultipleValuesConverter();
    return this.originalMultipleValuesConverter != null ? this.originalMultipleValuesConverter.GetCreateInstanceSupported(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    this.InitOriginalMultipleValuesConverter();
    if (this.originalMultipleValuesConverter != null)
      return this.originalMultipleValuesConverter.GetProperties(context, value, attributes);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    this.InitOriginalMultipleValuesConverter();
    return this.originalMultipleValuesConverter != null ? this.originalMultipleValuesConverter.GetPropertiesSupported(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    this.InitOriginalMultipleValuesConverter();
    return this.originalMultipleValuesConverter != null ? this.originalMultipleValuesConverter.GetStandardValues(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
  {
    this.InitOriginalMultipleValuesConverter();
    return this.originalMultipleValuesConverter != null ? this.originalMultipleValuesConverter.GetStandardValuesExclusive(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
  {
    this.InitOriginalMultipleValuesConverter();
    return this.originalMultipleValuesConverter != null ? this.originalMultipleValuesConverter.GetStandardValuesSupported(context) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  /// <summary>
  /// НЕ ПЕРЕКРЫВАТЬ. для перекрытия использовать IsValidExt
  /// </summary>
  /// <param name="context"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override bool IsValid(ITypeDescriptorContext context, object value)
  {
    this.InitOriginalMultipleValuesConverter();
    if (this.originalMultipleValuesConverter != null)
      return this.originalMultipleValuesConverter.IsValid(context, value);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override bool IsValidExt(
    ITypeDescriptorContext context,
    object value,
    ref List<ValidationResult> results)
  {
    this.InitOriginalMultipleValuesConverter();
    if (this.originalMultipleValuesConverter != null && this.originalMultipleValuesConverter is CommonTypeConverter)
      return ((CommonTypeConverter) this.originalMultipleValuesConverter).IsValidExt(context, value, ref results);
    throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }

  public override IAttributeEditorControl GetEditorControl(UITypeEditorEditStyle style)
  {
    this.InitOriginalMultipleValuesConverter();
    return this.originalMultipleValuesConverter != null && this.originalMultipleValuesConverter is CommonTypeConverter ? ((CommonTypeConverter) this.originalMultipleValuesConverter).GetEditorControl(style) : throw new AttributeProcessorException(AttributeProcessorConsts.msgMissingConverter);
  }
}
