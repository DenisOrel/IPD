// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IEmbedAttributesService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Необязательный сервис интегратора, отвечающий за передачу изменений из карточки объекта в его файлы.
/// </summary>
public interface IEmbedAttributesService
{
  /// <summary>
  /// Записывает в файловую копию объекта указанные значения атрибутов объекта.
  /// Как правило, этот метод вызывается из карточки документа для сохранения изменных атрибутов в файле документа.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="attributeValues">Коллекция значений атрибутов</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="attributeValues" /> не должен быть равен null</exception>
  void EmbedAttributeValues(long objectId, IList<AttributeValues> attributeValues);

  /// <summary>
  /// Записывает в файловую копию объекта указанные значения атрибутов объекта.
  /// Как правило, этот метод вызывается из карточки документа для сохранения изменных атрибутов в файле документа.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="attributeValues">Коллекция значений атрибутов</param>
  /// <param name="options">Опции выполнения операции</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="attributeValues" /> не должен быть равен null. Параметр <paramref name="options" /> не должен быть равен null.</exception>
  void EmbedAttributeValues(
    long objectId,
    IList<AttributeValues> attributeValues,
    EmbedAttributesActionOptions options);
}
