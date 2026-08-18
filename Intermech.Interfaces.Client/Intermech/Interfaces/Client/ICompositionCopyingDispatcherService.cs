// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICompositionCopyingDispatcherService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса диспетчера для механизмов клонирования структуры объектов.
/// Диспетчер позволяет выбрать подходящий метод клонирования в зависимости от головного объекта.
/// </summary>
/// <remarks>Реализация сервиса должна быть thread safe.</remarks>
public interface ICompositionCopyingDispatcherService
{
  /// <summary>
  /// Выполняет поиск подходящего метода клонирования структуры объекта.
  /// </summary>
  /// <param name="items">Выбранные элементы интерфейса пользователя</param>
  /// <param name="viewServices">Контекстные сервисы интерфейса пользователя. Параметр может быть не задан</param>
  /// <returns>Найденный метод или null</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="items" /> содержит null</exception>
  Action FindHandler(ISelectedItems items, IServiceProvider viewServices);

  /// <summary>
  /// Событие для выбора метода клонирования структуры объекта,
  /// используя выбранные элементы интерфейса пользователя.
  /// </summary>
  event EventHandler<FindCompositionCopyingHandlerEventArgs> FindBySelectedItems;
}
