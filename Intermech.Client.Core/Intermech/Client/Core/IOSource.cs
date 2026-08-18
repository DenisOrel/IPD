
// Type: Intermech.Client.Core.IOSource
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core;

/// <summary>Класс источника событий</summary>
public class IOSource : IIOSource
{
  /// <summary>
  /// Элемент управления, который является источником событий
  /// </summary>
  private object FControl;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider FServices;
  /// <summary>Коллекция выделенных в элементе управления элементов</summary>
  private ISelectedItems FSelectedItems;

  /// <summary>Создать заполненный экземпляр класса IOSource</summary>
  /// <param name="AControl">Элемент управления, который является источником событий</param>
  /// <param name="AServices">Контейнер сервисов</param>
  /// <param name="ASelectedItems">Коллекция выделенных в элементе управления элементов</param>
  public IOSource(object AControl, IServiceProvider AServices, ISelectedItems ASelectedItems)
  {
    this.Control = AControl;
    this.FServices = AServices;
    this.SelectedItems = ASelectedItems;
  }

  /// <summary>
  /// Элемент управления, который является источником событий
  /// </summary>
  public object Control
  {
    get => this.FControl;
    set => this.FControl = value;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    get => this.FServices;
    set => this.FServices = value;
  }

  /// <summary>Коллекция выделенных в элементе управления элементов</summary>
  public ISelectedItems SelectedItems
  {
    get => this.FSelectedItems;
    set => this.FSelectedItems = value;
  }
}
