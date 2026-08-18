// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IExtendedSaveSupport
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать сервис расширенного сохранения изменений. Как правило, этот сервис
/// используется интеграторами с CAD-системами для сохранения изменений с обновлением структуры
/// изделия.
/// </summary>
public interface IExtendedSaveSupport
{
  /// <summary>
  /// Возвращает коллекцию типов документов, которые поддерживают расширенное сохранение.
  /// </summary>
  /// <returns>Коллекция идентификаторов типов документов</returns>
  ICollection<LocalId<int>> GetSupportedDocumentTypes();

  /// <summary>
  /// Захватывает и сохраняет в рабочую копию объекта изменения, сделанные пользователем в
  /// файловой копии объекта.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="options">Опции выполнения операции</param>
  /// <returns>Объект с данными о результате выполнения</returns>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="options" /> не должен быть равен null</exception>
  ExtendedSaveResult CaptureChanges(long objectId, ExtendedSaveOptions options);
}
