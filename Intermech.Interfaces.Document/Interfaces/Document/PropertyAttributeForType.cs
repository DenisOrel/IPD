// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PropertyAttributeForType
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Контейнер для перекрытия атрибута у свойства</summary>
[Serializable]
public class PropertyAttributeForType
{
  /// <summary>Тип владельца свойства</summary>
  public readonly Type PropertyOwnerType;
  /// <summary>Атрибут</summary>
  public readonly Attribute Attribute;

  /// <summary>Конструктор</summary>
  /// <param name="propertyOwnerType">Тип владельца свойства</param>
  /// <param name="attribute">Атрибут</param>
  public PropertyAttributeForType(Type propertyOwnerType, Attribute attribute)
  {
    if (propertyOwnerType == (Type) null)
      throw new ArgumentNullException(nameof (propertyOwnerType));
    if (attribute == null)
      throw new ArgumentNullException(nameof (attribute));
    this.PropertyOwnerType = propertyOwnerType;
    this.Attribute = attribute;
  }
}
