// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IAttributesLockService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса, позволяющего запретить добавление/редактирование определенных атрибутов у объектов и связей
/// средствами пользовательского интерфейса IPS.
/// </summary>
/// <remarks>
/// Необходимость в этом сервисе возникла из-за того, что ряд атрибутов может приходить извне IPS. Например,
/// атрибуты изделия, выпускаемого по 3D-модели, извлекаются из файла этой модели. Поэтому в изменении
/// атрибутов такого изделия нет никакого смысла, так как введенные пользователем значения все равно будут
/// перезаписаны значениями из файла модели при завершении редактирования 3D-модели.
/// </remarks>
public interface IAttributesLockService
{
  /// <summary>
  /// Определяет атрибуты объекта или связи, добавление/редактирование которых средствами
  /// интерфейса пользователя должно быть недоступно.
  /// </summary>
  /// <param name="elementKind">Указывает, к чему относятся атрибуты - к объекту или связи</param>
  /// <param name="elementId">Идентификатор версии объекта или связи</param>
  /// <param name="elementType">Идентификатор типа объекта или типа связи</param>
  /// <returns>Коллекция идентификаторов атрибутов</returns>
  ICollection<int> GetLockedAttributes(
    AttributableElements elementKind,
    long elementId,
    int elementType);

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
  void LockAttributeValues(
    AttributableElements elementKind,
    long elementId,
    int elementType,
    IList<AttributeValues> attributeValues);

  /// <summary>
  /// Основное событие, используемое для построения списка блокируемых атрибутов.
  /// </summary>
  event EventHandler<AttributesLockArgs> GetLockedAttributesHandler;
}
