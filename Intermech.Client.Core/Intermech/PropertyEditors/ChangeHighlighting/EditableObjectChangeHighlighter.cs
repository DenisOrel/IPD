
// Type: Intermech.PropertyEditors.ChangeHighlighting.EditableObjectChangeHighlighter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors.ChangeHighlighting;

/// <summary>
/// Реализует подсветку измененных свойств объекта при его редактировании в PropertyGrid.
/// Редактируемый объект должен реализовывать интерфейс ICloneable, а его свойства должны быть либо примитивными, либо реализовывать метод Equals.
/// Составные свойства редактируемого объекта должны использовать ExpandableObjectConverter, а также обязательно реализовывать метод Equals.
/// </summary>
public sealed class EditableObjectChangeHighlighter : CustomTypeDescriptor
{
  private readonly object editableObject;
  private readonly object originalObject;

  /// <summary>Создает объект.</summary>
  /// <param name="editableObject">Редактируемый объект</param>
  /// <exception cref="T:System.ArgumentNullException">editableObject</exception>
  public EditableObjectChangeHighlighter(ICloneable editableObject)
  {
    this.editableObject = editableObject != null ? (object) editableObject : throw new ArgumentNullException(nameof (editableObject));
    this.originalObject = editableObject.Clone();
  }

  /// <summary>Возвращает необернутый редактируемый объект.</summary>
  public object EditableObject => this.editableObject;

  /// <summary>
  /// Возвращает копию исходного состояния редактируемого объекта.
  /// </summary>
  public object OriginalObject => this.originalObject;

  public override PropertyDescriptorCollection GetProperties()
  {
    return this.GetProperties((Attribute[]) null);
  }

  public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(this.editableObject, attributes);
    PropertyDescriptor[] properties2 = new PropertyDescriptor[properties1.Count];
    for (int index = 0; index < properties1.Count; ++index)
      properties2[index] = (PropertyDescriptor) new EditableObjectChangeHighlighter.PropertyDescriptorWrapper(properties1[index], properties1[index].GetValue(this.originalObject));
    return new PropertyDescriptorCollection(properties2);
  }

  public override object GetPropertyOwner(PropertyDescriptor pd) => this.editableObject;

  private sealed class PropertyDescriptorWrapper : PropertyDescriptor
  {
    private PropertyDescriptor nativeDescr;
    private object originalValue;
    private TypeConverter converter;

    public PropertyDescriptorWrapper(PropertyDescriptor nativeDescr, object originalValue)
      : base((MemberDescriptor) nativeDescr)
    {
      this.nativeDescr = nativeDescr;
      this.originalValue = originalValue;
      this.converter = (TypeConverter) new EditableObjectChangeHighlighter.TypeConverterWrapper(this.nativeDescr.Converter);
    }

    public override TypeConverter Converter => this.converter;

    public override Type ComponentType => this.nativeDescr.ComponentType;

    public override Type PropertyType => this.nativeDescr.PropertyType;

    public override bool IsReadOnly => this.nativeDescr.IsReadOnly;

    public override bool CanResetValue(object component) => this.ShouldSerializeValue(component);

    public override void ResetValue(object component)
    {
      this.nativeDescr.SetValue(component, this.originalValue);
    }

    public override object GetValue(object component) => this.nativeDescr.GetValue(component);

    public override void SetValue(object component, object value)
    {
      this.nativeDescr.SetValue(component, value);
    }

    public override bool ShouldSerializeValue(object component)
    {
      return !object.Equals(this.GetValue(component), this.originalValue);
    }
  }

  private sealed class TypeConverterWrapper : TypeConverter
  {
    private TypeConverter nativeConverter;

    public TypeConverterWrapper(TypeConverter nativeConverter)
    {
      this.nativeConverter = nativeConverter;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      return this.nativeConverter.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      return this.nativeConverter.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return this.nativeConverter.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      return this.nativeConverter.ConvertTo(context, culture, value, destinationType);
    }

    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      return this.nativeConverter.CreateInstance(context, propertyValues);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
    {
      return this.nativeConverter.GetCreateInstanceSupported(context);
    }

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      PropertyDescriptorCollection properties1 = this.nativeConverter.GetProperties(context, value, attributes);
      PropertyDescriptor[] properties2 = new PropertyDescriptor[properties1.Count];
      for (int index = 0; index < properties1.Count; ++index)
        properties2[index] = (PropertyDescriptor) new EditableObjectChangeHighlighter.PropertyDescriptorWrapper(properties1[index], properties1[index].GetValue(value));
      return new PropertyDescriptorCollection(properties2);
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return this.nativeConverter.GetPropertiesSupported(context);
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      return this.nativeConverter.GetStandardValues(context);
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return this.nativeConverter.GetStandardValuesExclusive(context);
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return this.nativeConverter.GetStandardValuesSupported(context);
    }

    public override bool IsValid(ITypeDescriptorContext context, object value)
    {
      return this.nativeConverter.IsValid(context, value);
    }
  }
}
