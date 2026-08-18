// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FindCompositionCopyingHandlerEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы события для выбора метод клонирования структуры объекта.
/// </summary>
/// <remarks>
/// Обработчик события должен заполнить свойство <see cref="P:Intermech.Interfaces.Client.FindCompositionCopyingHandlerEventArgs.Handler" />,
/// если может клонировать структуру выбранного объекта, и если
/// свойство еще не было заполнено другим обработчиком.
/// </remarks>
public class FindCompositionCopyingHandlerEventArgs : EventArgs
{
  /// <summary>Создает объект.</summary>
  /// <param name="items">Выбранные элементы интерфейса пользователя</param>
  /// <param name="viewServices">Контекстные сервисы интерфейса пользователя. Параметр может быть не задан</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="items" /> содержит null</exception>
  public FindCompositionCopyingHandlerEventArgs(ISelectedItems items, IServiceProvider viewServices)
  {
    this.Items = items != null ? items : throw new ArgumentNullException(nameof (items));
    this.ViewServices = viewServices;
  }

  /// <summary>
  /// Возвращает выбранные элементы интерфейса пользователя.
  /// </summary>
  public ISelectedItems Items { get; }

  /// <summary>
  /// Возвращает контекстные сервисы интерфейса пользователя.
  /// Значение свойства может быть не задано и равно null.
  /// </summary>
  public IServiceProvider ViewServices { get; }

  /// <summary>
  /// Возвращает или задает метод клонирования структуры объекта.
  /// </summary>
  public Action Handler { get; set; }
}
