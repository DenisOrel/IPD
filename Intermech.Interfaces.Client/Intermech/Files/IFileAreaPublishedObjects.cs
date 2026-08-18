// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IFileAreaPublishedObjects
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Дополнительный интерфейс файловой области, позволяющий получать состояния опубликованных объектов.
/// </summary>
public interface IFileAreaPublishedObjects : IFileArea
{
  /// <summary>
  /// Возвращает список объектов, опубликованных в рабочей области.
  /// </summary>
  /// <returns>Список опубликованных версий объектов</returns>
  List<DBObjectState> GetPublishedObjects();

  /// <summary>Проверяет публикацию объекта в рабочей области.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>true, если указанная версия объекта опубликована в рабочей области; false - если не опубликована</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  bool IsObjectPublished(long objectId);

  /// <summary>
  /// Позволяет найти опубликованный объект по идентификатору версии объекта.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Состояние опубликованной версии объекта или null, если объект не был опубликован</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  DBObjectState FindPublishedObjectByVersionId(long objectId);

  /// <summary>
  /// Позволяет найти опубликованный объект по идентификатору объекта.
  /// </summary>
  /// <param name="id">Идентификатор объекта</param>
  /// <returns>Состояние опубликованной версии объекта или null, если объект не был опубликован</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор объекта</exception>
  DBObjectState FindPublishedObjectById(long id);
}
