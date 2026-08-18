// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBFileValueLoader
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Remoting.Sponsors;
using System;
using System.IO;

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>
/// Класс загрузчика значений для атрибутов типа "Файл", "Двоичные данные", "Короткие двоичные данные".
/// Загрузчик используется для ленивого чтения значений сложных типов атрибутов.
/// </summary>
/// <remarks>Реализация не является thread safe.</remarks>
public sealed class DBFileValueLoader
{
  private int attributeId;
  private int attributeValueIndex;
  private IDBEntityTypeDescriptor entityDescriptor;
  private object entity;
  private AttributableElements entityElementType;
  private long entityId;

  /// <summary>Создает объект.</summary>
  /// <param name="attributeId">Идентификатор атрибута объекта или связи IPS</param>
  /// <param name="attributeValueIndex">Индекс значения в атрибуте</param>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="attributeId" /> содержит некорректное значение; параметр <paramref name="attributeValueIndex" /> содержит отрицательное значение</exception>
  public DBFileValueLoader(int attributeId, int attributeValueIndex = 0)
  {
    if (attributeId == 0)
      throw new ArgumentException("Не задан идентификатор атрибута объекта или связи IPS.", nameof (attributeId));
    if (attributeValueIndex < 0)
      throw new ArgumentOutOfRangeException(nameof (attributeValueIndex));
    this.attributeId = attributeId;
    this.attributeValueIndex = attributeValueIndex;
    this.entityElementType = AttributableElements.None;
  }

  /// <summary>
  /// Возвращает идентификатор атрибута объекта или связи IPS.
  /// </summary>
  public int AttributeId => this.attributeId;

  /// <summary>Возвращает индекс значения в атрибуте.</summary>
  public int AttributeValueIndex => this.attributeValueIndex;

  /// <summary>
  /// Задает идентификатор объекта или связи IPS, из которых будет выполняться чтение значения атрибута.
  /// </summary>
  /// <param name="entityElementType">Объект или связь IPS</param>
  /// <param name="entityId">Идентификатор объекта или связи IPS</param>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="entityElementType" /> содержит некорректное значение; параметр <paramref name="entityId" /> содержит неопределенное значение</exception>
  public void SetEntity(AttributableElements entityElementType, long entityId)
  {
    if (entityElementType != AttributableElements.Object && entityElementType != AttributableElements.Relation)
      throw new ArgumentException("Недопустимое значение типа элемента.", nameof (entityElementType));
    if (entityId == 0L || entityId == -1L)
      throw new ArgumentException("Недопустимое значение идентификатора объекта или связи IPS.", nameof (entityId));
    this.entityElementType = entityElementType;
    this.entityId = entityId;
    this.entityDescriptor = (IDBEntityTypeDescriptor) null;
    this.entity = (object) null;
  }

  /// <summary>
  /// Задает доменный объект, из которого будет выполняться чтение значения атрибута.
  /// </summary>
  /// <param name="entityDescriptor">Дескриптор доменного объекта</param>
  /// <param name="entity">Доменный объект или объект-связка</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="entityDescriptor" /> содержит null; параметр <paramref name="entity" /> содержит null</exception>
  internal void SetEntity(IDBEntityTypeDescriptor entityDescriptor, object entity)
  {
    if (entityDescriptor == null)
      throw new ArgumentNullException(nameof (entityDescriptor));
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.entityDescriptor = entityDescriptor;
    this.entity = entity;
    this.entityElementType = AttributableElements.None;
    this.entityId = 0L;
  }

  /// <summary>Читает значение атрибута.</summary>
  /// <returns>Прочитанное значение атрибута</returns>
  /// <exception cref="T:System.InvalidOperationException">Не задан объект или связь IPS для чтения значения атрибута. Предварительно необходимо было вызвать метод <see cref="M:Experimental.Kernel.Entities.DBFileValueLoader.SetEntity(Intermech.AttributableElements,System.Int64)" />.</exception>
  /// <exception cref="T:Experimental.Data.Entities.EntityException">При чтении значения атрибута произошла ошибка</exception>
  public DBFileValue LoadValue()
  {
    this.LazyInitializeEntityElementTypeAndId();
    using (RemoteLock remoteLock = new RemoteLock())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributable objToLock = this.entityElementType == AttributableElements.Object ? (IDBAttributable) sessionKeeper.Session.GetObject(this.entityId, true) : (IDBAttributable) sessionKeeper.Session.GetRelation(this.entityId, true);
        IDBAttribute attributeById = objToLock.GetAttributeByID(this.AttributeId);
        if (attributeById == null)
          throw new InvalidOperationException($"Не удалось найти указанный атрибут с идентификатором = {this.attributeId} у объекта/связи IPS с идентификатором = {this.entityId}.");
        remoteLock.Add((object) objToLock);
        remoteLock.Add((object) attributeById);
        using (ImChunkedStream aDestStream = new ImChunkedStream())
        {
          new BlobProcReader(this.entityId, this.entityElementType, this.attributeId, this.attributeValueIndex, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
          aDestStream.Flush();
          return new DBFileValue(attributeById.AsString, aDestStream.ToArray());
        }
      }
    }
  }

  private void LazyInitializeEntityElementTypeAndId()
  {
    if (this.entityElementType == AttributableElements.None)
    {
      if (this.entityDescriptor == null || this.entity == null)
        throw this.EntityIsNotSetException();
      this.FillEntityElementTypeAndId();
    }
    else if (this.entityElementType == AttributableElements.None || this.entityId == 0L || this.entityId == -1L)
      throw this.EntityIsNotSetException();
  }

  private void FillEntityElementTypeAndId()
  {
    DBEntityKind entityKind = this.entityDescriptor.EntityKind;
    switch (entityKind)
    {
      case DBEntityKind.Object:
        this.entityId = this.entityDescriptor.AsDBObjectDescriptor().GetKey(this.entity);
        this.entityElementType = AttributableElements.Object;
        break;
      case DBEntityKind.Relation:
        this.entityId = this.entityDescriptor.AsDBRelationDescriptor().GetKey(this.entity);
        this.entityElementType = AttributableElements.Relation;
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityKind);
    }
  }

  private EntityException EntityIsNotSetException()
  {
    return new EntityException($"Доменный объект не задан. Воспользуйтесь методом '{"SetEntity"}'.");
  }

  public bool Equals(DBFileValueLoader other)
  {
    if (other != null)
    {
      if (this.entity != null && other.entity != null)
        return this.entity == other.entity;
      if (this.entityElementType != AttributableElements.None && other.entityElementType != AttributableElements.None && this.entityElementType == other.entityElementType)
        return this.entityId == other.entityId;
    }
    return false;
  }

  public override bool Equals(object obj)
  {
    return !(obj is DBFileValueLoader other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode()
  {
    return this.entity != null ? this.entity.GetHashCode() : this.entityId.GetHashCode();
  }
}
