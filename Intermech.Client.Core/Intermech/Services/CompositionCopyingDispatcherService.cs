
// Type: Intermech.Services.CompositionCopyingDispatcherService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Services;

/// <summary>
/// Класс сервиса диспетчера для механизмов клонирования структуры объектов.
/// Диспетчер позволяет выбрать подходящий метод клонирования в зависимости от головного объекта.
/// </summary>
/// <remarks>Реализация является thread safe.</remarks>
internal sealed class CompositionCopyingDispatcherService : ICompositionCopyingDispatcherService
{
  private readonly object syncRoot;
  private EventHandler<FindCompositionCopyingHandlerEventArgs> findBySelectedItems;

  /// <summary>Создает объект.</summary>
  public CompositionCopyingDispatcherService() => this.syncRoot = new object();

  /// <summary>
  /// Выполняет поиск подходящего метода клонирования структуры объекта.
  /// </summary>
  /// <param name="items">Выбранные элементы интерфейса пользователя</param>
  /// <param name="viewServices">Контекстные сервисы интерфейса пользователя. Параметр может быть не задан</param>
  /// <returns>Найденный метод или null</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="items" /> содержит null</exception>
  public Action FindHandler(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    lock (this.syncRoot)
    {
      if (this.findBySelectedItems != null)
      {
        FindCompositionCopyingHandlerEventArgs e = new FindCompositionCopyingHandlerEventArgs(items, viewServices);
        this.findBySelectedItems((object) this, e);
        if (e.Handler != null)
          return e.Handler;
      }
    }
    return (Action) null;
  }

  /// <summary>
  /// Событие для выбора метода клонирования структуры объекта,
  /// используя выбранные элементы интерфейса пользователя.
  /// </summary>
  public event EventHandler<FindCompositionCopyingHandlerEventArgs> FindBySelectedItems
  {
    add
    {
      lock (this.syncRoot)
        this.findBySelectedItems += value;
    }
    remove
    {
      lock (this.syncRoot)
        this.findBySelectedItems -= value;
    }
  }
}
