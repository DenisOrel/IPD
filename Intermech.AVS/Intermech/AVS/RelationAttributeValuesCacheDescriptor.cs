// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RelationAttributeValuesCacheDescriptor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

/// <summary>Описатель элемента списка из List[RelationAttributeValuesCache] для преставления его в PropertyGrid</summary>
[Serializable]
public class RelationAttributeValuesCacheDescriptor : PropertyDescriptor
{
  private readonly RelationAttributeValuesCache _relation;

  /// <summary>Конструктор</summary>
  public RelationAttributeValuesCacheDescriptor(RelationAttributeValuesCache relation, string name)
    : base(name, (Attribute[]) null)
  {
    this._relation = relation;
  }

  /// <summary>Получить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Значение свойства</returns>
  public override object GetValue(object component) => (object) this._relation;

  /// <summary>Установить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <param name="value">Значение свойства</param>
  public override void SetValue(object component, object value)
  {
  }

  /// <summary>Можно ли сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Можно ли сбросить значение свойства</returns>
  public override bool CanResetValue(object component) => false;

  /// <summary>Тип владельца свойства</summary>
  public override Type ComponentType
  {
    [DebuggerStepThrough] get => typeof (List<RelationAttributeValuesCache>);
  }

  /// <summary>Тип свойства</summary>
  public override Type PropertyType
  {
    [DebuggerStepThrough] get => typeof (RelationAttributeValuesCache);
  }

  /// <summary>Сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  public override void ResetValue(object component)
  {
  }

  /// <summary>Нужно ли сохранить данное значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Нужно ли сохранить данное значение свойства</returns>
  public override bool ShouldSerializeValue(object component) => false;

  /// <summary>Только для чтения</summary>
  public override bool IsReadOnly
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>Категория свойства</summary>
  public override string Category
  {
    [DebuggerStepThrough] get => "[Debug]";
  }

  public override TypeConverter Converter
  {
    get => (TypeConverter) new RelationAttributeValuesCacheConverter();
  }

  public override PropertyDescriptorCollection GetChildProperties(
    object instance,
    Attribute[] filter)
  {
    return instance != null ? new RelationAttributeValuesCacheConverter().GetProperties((object) (RelationAttributeValuesCache) instance) : base.GetChildProperties(instance, filter);
  }
}
