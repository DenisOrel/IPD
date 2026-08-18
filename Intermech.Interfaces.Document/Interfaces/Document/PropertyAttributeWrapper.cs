// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PropertyAttributeWrapper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс оболочка для атрибутов которые навешиваются динамически на свойства класса.
/// Помещаются в словарь возвращаемый GetPropertyAttributes().</summary>
[Serializable]
public class PropertyAttributeWrapper
{
  /// <summary>Контейнеры атрибутов для типов</summary>
  public ArrayList AttributesForTypes = new ArrayList();
  /// <summary>Имя свойства</summary>
  public string PropertyName;

  /// <summary>Индексатор для AttributesForTypes</summary>
  public PropertyAttributeForType this[int index]
  {
    [DebuggerStepThrough] get => (PropertyAttributeForType) this.AttributesForTypes[index];
    set => this.AttributesForTypes[index] = (object) value;
  }

  /// <summary>Конструктор</summary>
  /// <param name="propertyName">Имя свойства</param>
  /// <param name="propertyOwnerType">Тип владельца свойства</param>
  /// <param name="attribute">Атрибут</param>
  public PropertyAttributeWrapper(string propertyName, Type propertyOwnerType, Attribute attribute)
  {
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (propertyOwnerType == (Type) null)
      throw new ArgumentNullException(nameof (propertyOwnerType));
    if (attribute == null)
      throw new ArgumentNullException(nameof (attribute));
    this.AttributesForTypes.Add((object) new PropertyAttributeForType(propertyOwnerType, attribute));
    this.PropertyName = propertyName;
  }

  /// <summary>Конструктор</summary>
  /// <param name="propertyName">Имя свойства</param>
  /// <param name="attributesForTypes">Массив атрибутов</param>
  public PropertyAttributeWrapper(
    string propertyName,
    PropertyAttributeForType[] attributesForTypes)
  {
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (attributesForTypes == null)
      throw new ArgumentNullException(nameof (attributesForTypes));
    this.AttributesForTypes.AddRange((ICollection) attributesForTypes);
    this.PropertyName = propertyName;
  }
}
