
// Type: Intermech.PropertyEditors.AttrProcessor.MultipleValuesTypeConverter
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
/// TODO: Конвертор для атрибутов со множеством значений.
/// </summary>
internal class MultipleValuesTypeConverter(int attributeId, AttributeProcessor attributeProcessor) : 
  CommonTypeConverter(attributeId, attributeProcessor)
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return base.CanConvertFrom(context, sourceType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return base.CanConvertTo(context, destinationType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return base.ConvertFrom(context, culture, value);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return base.ConvertTo(context, culture, value, destinationType);
  }

  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    return base.CreateInstance(context, propertyValues);
  }

  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
  {
    return base.GetCreateInstanceSupported(context);
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    Type propertyType = this.attributeProcessor.GetPropertyType(this.attributeId);
    TypeConverter singleValueConverter = this.attributeProcessor.GetSingleValueConverter(this.attributeId);
    bool flag = this.attributeProcessor.GetReadOnly(this.attributeId);
    bool canReset = this.attributeProcessor.GetCanReset(this.attributeId);
    object[] values = this.attributeProcessor.GetValues(this.attributeId);
    List<CommonPropertyDescriptor> propertyDescriptorList = new List<CommonPropertyDescriptor>();
    for (int index = 0; index < values.Length; ++index)
    {
      SinglePropertyDescriptor propertyDescriptor = new SinglePropertyDescriptor(this.attributeId, this.attributeProcessor, $"[{index.ToString()}]", attributes, index, propertyType, singleValueConverter, new bool?(flag), new bool?(canReset));
      propertyDescriptorList.Add((CommonPropertyDescriptor) propertyDescriptor);
    }
    return new PropertyDescriptorCollection((PropertyDescriptor[]) propertyDescriptorList.ToArray());
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    return (TypeConverter.StandardValuesCollection) null;
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => false;

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => false;

  /// <summary>
  /// НЕ ПЕРЕКРЫВАТЬ. для перекрытия использовать IsValidExt
  /// </summary>
  /// <param name="context"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override bool IsValid(ITypeDescriptorContext context, object value)
  {
    return base.IsValid(context, value);
  }

  public override bool IsValidExt(
    ITypeDescriptorContext context,
    object value,
    ref List<ValidationResult> results)
  {
    if (!(value is object[]))
      return base.IsValidExt(context, value, ref results);
    bool flag = true;
    results = (List<ValidationResult>) null;
    object[] objArray = (object[]) value;
    TypeConverter singleValueConverter = this.attributeProcessor.GetSingleValueConverter(this.attributeId);
    for (int index = 0; index < objArray.Length; ++index)
    {
      List<ValidationResult> results1 = (List<ValidationResult>) null;
      if (!((CommonTypeConverter) singleValueConverter).IsValidExt(context, objArray[index], ref results1))
      {
        flag = false;
        if (results == null)
          results = new List<ValidationResult>();
        results.Add(new ValidationResult(results1[0].AttributeId, index, results1[0].Reason));
      }
    }
    return flag;
  }

  public override List<UITypeEditorEditStyle> GetPossibleEditorControlStyle()
  {
    if (!AttributeProcessorProcs.IsMultipleValued(this.attributeId))
      return new List<UITypeEditorEditStyle>();
    return new List<UITypeEditorEditStyle>((IEnumerable<UITypeEditorEditStyle>) new UITypeEditorEditStyle[1]
    {
      UITypeEditorEditStyle.Modal
    });
  }

  public override IAttributeEditorControl GetEditorControl(UITypeEditorEditStyle style)
  {
    if (style == UITypeEditorEditStyle.None)
      return (IAttributeEditorControl) null;
    if (this.styles == null)
      this.styles = this.GetPossibleEditorControlStyle();
    if (this.styles.IndexOf(style) == -1)
      return (IAttributeEditorControl) null;
    IAttributeEditorControl iAttributeEditorControl = (IAttributeEditorControl) new PropertyGridEditorControl();
    if (style == UITypeEditorEditStyle.Modal)
    {
      EditorControlForm editorControlForm = new EditorControlForm();
      editorControlForm.AssignControl(iAttributeEditorControl);
      iAttributeEditorControl = (IAttributeEditorControl) editorControlForm;
    }
    return iAttributeEditorControl;
  }
}
