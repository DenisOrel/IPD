// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AdditionalAttributesDescriptor
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Описатель свойства типа AdditionalAttributeCollection для преставления его в PropertyGrid</summary>
public class AdditionalAttributesDescriptor : PropertyDescriptor
{
  /// <summary>Конструктор</summary>
  /// <param name="descr">Базовый дескриптор</param>
  protected AdditionalAttributesDescriptor(MemberDescriptor descr)
    : base(descr)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="descr">Базовый дескриптор</param>
  /// <param name="attrs">Атрибуты свойств</param>
  protected AdditionalAttributesDescriptor(MemberDescriptor descr, Attribute[] attrs)
    : base(descr, attrs)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="name">Имя свойства</param>
  /// <param name="attrs">Атрибуты свойства</param>
  protected AdditionalAttributesDescriptor(string name, Attribute[] attrs)
    : base(name, attrs)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="attrs">Атрибуты свойства</param>
  public AdditionalAttributesDescriptor(Attribute[] attrs)
    : base("AdditionalAttributes", attrs)
  {
  }

  /// <summary>Можно ли сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Можно ли сбросить значение свойства</returns>
  public override bool CanResetValue(object component) => false;

  /// <summary>Тип владельца свойства</summary>
  public override Type ComponentType
  {
    [DebuggerStepThrough] get => typeof (DocumentTreeNode);
  }

  /// <summary>Получить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Значение свойства</returns>
  public override object GetValue(object component)
  {
    if (component == null)
      return (object) null;
    DocumentTreeNode owner = (DocumentTreeNode) component;
    return (object) owner.AdditionalAttributes ?? (object) new AdditionalAttributeCollection(owner);
  }

  /// <summary>Установить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <param name="value">Значение свойства</param>
  public override void SetValue(object component, object value)
  {
    AdditionalAttributeCollection attributeCollection = (AdditionalAttributeCollection) value;
    if (((DocumentTreeNode) component).AdditionalAttributes == null && attributeCollection.Count <= 0)
      return;
    ((DocumentTreeNode) component).AdditionalAttributes = (AdditionalAttributeCollection) value;
  }

  /// <summary>Только для чтения</summary>
  public override bool IsReadOnly
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Тип свойства</summary>
  public override Type PropertyType
  {
    [DebuggerStepThrough] get => typeof (AdditionalAttributeCollection);
  }

  /// <summary>Сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  public override void ResetValue(object component)
  {
  }

  /// <summary>Нужно ли сохранить данное значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Нужно ли сохранить данное значение свойства</returns>
  public override bool ShouldSerializeValue(object component) => true;
}
