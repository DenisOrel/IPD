// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CustomPropertyDescriptor
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Настраиваемый PropertyDescriptor,
/// который является оболочкой для PropertyDescriptor выдаваемого
/// TypeDescriptor.GetProperties</summary>
[Serializable]
public class CustomPropertyDescriptor : PropertyDescriptor
{
  private ArrayList _attributeList = new ArrayList();
  private bool? _isReadOnly;
  /// <summary>Исходный PropertyDescriptor полученный TypeDescriptor.GetProperties</summary>
  private PropertyDescriptor _PropDesc;
  private string name;
  private bool? serializeValue;

  /// <summary>Добавить атрибут свойства</summary>
  /// <param name="attr">Атрибут</param>
  public void AddAttribute(Attribute attr) => this._attributeList.Add((object) attr);

  /// <summary>Атрибуты свойства</summary>
  public override AttributeCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      Attribute[] attributeArray = new Attribute[this._attributeList.Count + this.AttributeArray.Length];
      this._attributeList.CopyTo((Array) attributeArray);
      for (int count = this._attributeList.Count; count < attributeArray.Length; ++count)
        attributeArray[count] = this.AttributeArray[count - this._attributeList.Count];
      return new AttributeCollection(attributeArray);
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="PropDesc">Исходный PropertyDescriptor полученный TypeDescriptor.GetProperties,
  /// на основе которого работает класс</param>
  public CustomPropertyDescriptor(PropertyDescriptor PropDesc)
    : base((MemberDescriptor) PropDesc)
  {
    this._PropDesc = PropDesc;
  }

  /// <summary>Конструктор</summary>
  /// <param name="displayName"></param>
  /// <param name="attrs"></param>
  public CustomPropertyDescriptor(string displayName, Attribute[] attrs)
    : base(displayName, attrs)
  {
  }

  /// <summary>Категория свойства</summary>
  public override string Category
  {
    [DebuggerStepThrough] get
    {
      string category = base.Category;
      if (category == "Misc")
        category = LocalizationHolder.rm.GetString("Interfaces.Document_6");
      return category;
    }
  }

  /// <summary>Установить имя</summary>
  /// <param name="name"></param>
  public void SetName(string name) => this.name = name;

  public override string Name => this.name != null ? this.name : base.Name;

  /// <summary>Просто обращается к исходному объекту</summary>
  public override Type ComponentType
  {
    [DebuggerStepThrough] get => this._PropDesc.ComponentType;
  }

  /// <summary>Только для чтения</summary>
  public override bool IsReadOnly
  {
    [DebuggerStepThrough] get
    {
      if (this._isReadOnly.HasValue)
        return this._isReadOnly.Value;
      return this.Attributes[typeof (ReadOnlyAttribute)] is ReadOnlyAttribute attribute ? attribute.IsReadOnly : this._PropDesc.IsReadOnly;
    }
  }

  /// <summary>Устанавливает значение IsReadOnly поверх базового. Если true, то перекрывает ReadOnly,
  /// если false, то зависит от базового</summary>
  /// <param name="value">Новое значение</param>
  public virtual void SetIsReadOnly(bool value) => this._isReadOnly = new bool?(value);

  /// <summary>Просто обращается к исходному объекту</summary>
  public override Type PropertyType
  {
    [DebuggerStepThrough] get => this._PropDesc.PropertyType;
  }

  /// <summary>Можно ли сбросить значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Можно ли сбросить значение свойства</returns>
  public override bool CanResetValue(object component) => this._PropDesc.CanResetValue(component);

  /// <summary>Получить значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Значение свойства</returns>
  public override object GetValue(object component) => this._PropDesc.GetValue(component);

  /// <summary>Сбросить значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  public override void ResetValue(object component) => this._PropDesc.ResetValue(component);

  /// <summary>Установить значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  /// <param name="value">Значение свойства</param>
  public override void SetValue(object component, object value)
  {
    if (this.IsReadOnly)
      return;
    if (value is string && this.Converter.CanConvertFrom(value.GetType()))
      this._PropDesc.SetValue(component, this.Converter.ConvertFrom(value));
    else
      this._PropDesc.SetValue(component, value);
  }

  public bool? SerializeValue
  {
    get => this.serializeValue;
    set => this.serializeValue = value;
  }

  /// <summary>Нужно ли сохранить данное значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Нужно ли сохранить данное значение свойства</returns>
  public override bool ShouldSerializeValue(object component)
  {
    if (this.SerializeValue.HasValue)
      return this.SerializeValue.Value;
    return component != null && this._PropDesc.ShouldSerializeValue(component);
  }

  public override PropertyDescriptorCollection GetChildProperties(
    object instance,
    Attribute[] filter)
  {
    PropertyDescriptorCollection childProperties = base.GetChildProperties(instance, filter);
    if (!this.IsReadOnly)
      return childProperties;
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>(childProperties.Count);
    foreach (PropertyDescriptor PropDesc in childProperties)
    {
      if (!(PropDesc is CustomPropertyDescriptor propertyDescriptor))
        propertyDescriptor = new CustomPropertyDescriptor(PropDesc);
      propertyDescriptor.SetIsReadOnly(true);
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }

  public static void SetReadOnlyProperties(PropertyDescriptorCollection properties)
  {
    for (int index = 0; index < properties.Count; ++index)
    {
      if (properties[index] is CustomPropertyDescriptor property)
        property.SetIsReadOnly(true);
    }
  }

  public static void SetReadOnlyProperties(IDictionary properties)
  {
    foreach (DictionaryEntry property in properties)
    {
      if (property.Value is CustomPropertyDescriptor propertyDescriptor)
        propertyDescriptor.SetIsReadOnly(true);
    }
  }
}
