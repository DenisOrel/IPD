
// Type: Intermech.Services.AttributesLockService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Intermech.Services;

/// <summary>
/// Реализует сервис, позволяющих запретить добавление/редактирование определенных атрибутов у объектов и связей
/// средствами пользовательского интерфейса IPS.
/// </summary>
/// <remarks>
/// Необходимость в этом сервисе возникла из-за того, что ряд атрибутов может приходить извне IPS. Например,
/// атрибуты изделия, выпускаемого по 3D-модели, извлекаются из файла этой модели. Поэтому в изменении
/// атрибутов такого изделия нет никакого смысла, так как введенные пользователем значения все равно будут
/// перезаписаны значениями из файла модели при завершении редактирования 3D-модели.
/// </remarks>
internal sealed class AttributesLockService : IAttributesLockService
{
  private readonly object syncRoot;
  private readonly ReadOnlyCollection<int> emptyAttributesList;
  private EventHandler<AttributesLockArgs> getLockedAttributesMainHandler;
  private AttributesLockService.GetLockedAttributesCacheItem getLockedAttributesCache;

  /// <summary>Создает объект.</summary>
  public AttributesLockService()
  {
    this.syncRoot = new object();
    this.emptyAttributesList = new ReadOnlyCollection<int>((IList<int>) new int[0]);
  }

  /// <summary>
  /// Определяет атрибуты объекта или связи, добавление/редактирование которых средствами
  /// интерфейса пользователя должно быть недоступно.
  /// </summary>
  /// <param name="elementKind">Указывает, к чему относятся атрибуты - к объекту или связи</param>
  /// <param name="elementId">Идентификатор версии объекта или связи</param>
  /// <param name="elementType">Идентификатор типа объекта или типа связи</param>
  /// <returns>Коллекция идентификаторов атрибутов</returns>
  public ICollection<int> GetLockedAttributes(
    AttributableElements elementKind,
    long elementId,
    int elementType)
  {
    lock (this.syncRoot)
    {
      AttributesLockService.GetLockedAttributesCacheKey attributesCacheKey = new AttributesLockService.GetLockedAttributesCacheKey(elementKind, elementId, elementType);
      if (this.getLockedAttributesCache != null && this.getLockedAttributesCache.Key.Equals(attributesCacheKey))
        return this.getLockedAttributesCache.Value;
      ICollection<int> lockedAttributesSlow = this.GetLockedAttributesSlow(elementKind, elementId, elementType);
      this.getLockedAttributesCache = new AttributesLockService.GetLockedAttributesCacheItem(attributesCacheKey, lockedAttributesSlow);
      return lockedAttributesSlow;
    }
  }

  private ICollection<int> GetLockedAttributesSlow(
    AttributableElements elementKind,
    long elementId,
    int elementType)
  {
    if (this.getLockedAttributesMainHandler != null)
    {
      AttributesLockArgs e = new AttributesLockArgs(elementKind, elementId, elementType);
      if (this.getLockedAttributesMainHandler != null)
        this.getLockedAttributesMainHandler((object) null, e);
      if (e.LockedAttributes.Count != 0)
      {
        HashSet<int> lockedAttributes = e.LockedAttributes;
        if (e.UnlockedAttributes.Count != 0)
          lockedAttributes.ExceptWith((IEnumerable<int>) e.UnlockedAttributes);
        return (ICollection<int>) new ReadOnlyCollectionWrapper<int>((ICollection<int>) lockedAttributes);
      }
    }
    return (ICollection<int>) this.emptyAttributesList;
  }

  /// <summary>
  /// Помечает как read-only значения атрибутов объекта или связи, редактирование которых средствами
  /// интерфейса пользователя должно быть недоступно. Этот метод является вспомогательным,
  /// он основан на методе GetLockedAttributes.
  /// </summary>
  /// <param name="elementKind">Указывает, к чему относятся атрибуты - к объекту или связи</param>
  /// <param name="elementId">Идентификатор версии объекта или связи</param>
  /// <param name="elementType">Идентификатор типа объекта или типа связи</param>
  /// <param name="attributeValues">Cписок значений атрибутов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список значений атрибутов не может быть null</exception>
  public void LockAttributeValues(
    AttributableElements elementKind,
    long elementId,
    int elementType,
    IList<AttributeValues> attributeValues)
  {
    if (attributeValues == null)
      throw new ArgumentNullException("values");
    if (attributeValues.Count == 0)
      return;
    ICollection<int> lockedAttributes = this.GetLockedAttributes(elementKind, elementId, elementType);
    if (lockedAttributes.Count == 0)
      return;
    foreach (AttributeValues attributeValue in (IEnumerable<AttributeValues>) attributeValues)
    {
      if (lockedAttributes.Contains(attributeValue.AttributeID))
      {
        AttributeValues attributeValues1 = attributeValue;
        attributeValues1.ReadOnly = ((attributeValues1.ReadOnly ? 1 : 0) | 1) != 0;
      }
    }
  }

  /// <summary>
  /// Основное событие, используемое для построения списка блокируемых атрибутов.
  /// </summary>
  public event EventHandler<AttributesLockArgs> GetLockedAttributesHandler
  {
    add
    {
      lock (this.syncRoot)
        this.getLockedAttributesMainHandler += value;
    }
    remove
    {
      lock (this.syncRoot)
        this.getLockedAttributesMainHandler -= value;
    }
  }

  private sealed class GetLockedAttributesCacheKey : 
    IEquatable<AttributesLockService.GetLockedAttributesCacheKey>
  {
    public GetLockedAttributesCacheKey(
      AttributableElements elementKind,
      long elementId,
      int elementType)
    {
      this.ElementKind = elementKind;
      this.ElementId = elementId;
      this.ElementType = elementType;
    }

    public AttributableElements ElementKind { get; }

    public long ElementId { get; }

    public int ElementType { get; }

    public bool Equals(
      AttributesLockService.GetLockedAttributesCacheKey other)
    {
      return other != null && other.ElementKind == this.ElementKind && other.ElementId == this.ElementId && other.ElementType == this.ElementType;
    }

    public override bool Equals(object obj)
    {
      return !(obj is AttributesLockService.GetLockedAttributesCacheKey other) ? base.Equals(obj) : this.Equals(other);
    }

    public override int GetHashCode() => this.ElementId.GetHashCode();
  }

  private sealed class GetLockedAttributesCacheItem
  {
    public GetLockedAttributesCacheItem(
      AttributesLockService.GetLockedAttributesCacheKey key,
      ICollection<int> value)
    {
      this.Key = key;
      this.Value = value;
    }

    public AttributesLockService.GetLockedAttributesCacheKey Key { get; }

    public ICollection<int> Value { get; }
  }
}
