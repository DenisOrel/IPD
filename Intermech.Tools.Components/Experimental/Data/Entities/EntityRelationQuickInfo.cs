// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityRelationQuickInfo
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>Описывает связь между двумя доменными объектами.</summary>
public sealed class EntityRelationQuickInfo : IEquatable<EntityRelationQuickInfo>
{
  /// <summary>Создает объект.</summary>
  /// <param name="parentEntity">Родительский объект, где связь начинается</param>
  /// <param name="propertyName">Имя навигационного свойства родительского объекта</param>
  /// <param name="childEntity">Дочерний объект, где связь заканчивается</param>
  /// <param name="childOccurence">Объект-связка, который может отсутствовать</param>
  /// <exception cref="T:ArgumentNullException">Параметры <paramref name="parentEntity" />, <paramref name="propertyName" />, <paramref name="childEntity" /> не должны быть равны null</exception>
  public EntityRelationQuickInfo(
    object parentEntity,
    string propertyName,
    object childEntity,
    object childOccurence = null)
  {
    if (parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (childEntity == null)
      throw new ArgumentNullException(nameof (childEntity));
    this.ParentEntity = parentEntity;
    this.PropertyName = propertyName;
    this.ChildEntity = childEntity;
    this.ChildOccurence = childOccurence;
  }

  /// <summary>Возвращает родительский объект, где связь начинается.</summary>
  public object ParentEntity { get; private set; }

  /// <summary>
  /// Возвращает имя навигационного свойства родительского объекта.
  /// </summary>
  public string PropertyName { get; private set; }

  /// <summary>Возвращает дочерний объект, где связь заканчивается.</summary>
  public object ChildEntity { get; private set; }

  /// <summary>
  /// Возвращает признак сложной связи между доменными объектами, когда они связаны с помощью вспомогательного объекта,
  /// используемого для представления свойств самой связи.
  /// </summary>
  public bool IsComplex
  {
    [DebuggerStepThrough] get => this.ChildOccurence != null;
  }

  /// <summary>
  /// Возвращает объект-связку, используемый для представления свойств самой связи.
  /// Значение свойства может быть не задано, если связь между доменными объектами простая.
  /// </summary>
  public object ChildOccurence { get; private set; }

  public bool Equals(EntityRelationQuickInfo other)
  {
    return other != null && (other == this || other.ParentEntity == this.ParentEntity && other.PropertyName == this.PropertyName && other.ChildEntity == this.ChildEntity && other.ChildOccurence == this.ChildOccurence);
  }

  public override bool Equals(object obj)
  {
    return !(obj is EntityRelationQuickInfo other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode()
  {
    return this.PropertyName.GetHashCode() ^ this.ParentEntity.GetHashCode() ^ this.ChildEntity.GetHashCode();
  }
}
