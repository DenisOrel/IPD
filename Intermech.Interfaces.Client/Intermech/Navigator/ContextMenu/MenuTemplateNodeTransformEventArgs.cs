// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.MenuTemplateNodeTransformEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Аргументы события "Выполнить преобразование шаблона контекстного меню"
/// </summary>
public class MenuTemplateNodeTransformEventArgs : EventArgs
{
  /// <summary>Элемент шаблона контекстного меню</summary>
  public MenuTemplateNode MenuTemplateNode;
  /// <summary>
  /// Коллекция выделенных элементов, на основе которых строится команда контекстного меню
  /// </summary>
  private ISelectedItems _items;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;

  /// <summary>
  /// Коллекция выделенных элементов, на основе которых строится команда контекстного меню
  /// </summary>
  public ISelectedItems Items
  {
    [DebuggerStepThrough] get => this._items;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
  }

  /// <summary>
  /// Создать аргументы события "Выполнить преобразование шаблона контекстного меню"
  /// </summary>
  /// <param name="menuTemplateNode">Элемент шаблона контекстного меню</param>
  /// <param name="items">Коллекция выделенных элементов, на основе которых строится команда контекстного меню</param>
  /// <param name="services">Контейнер сервисов</param>
  public MenuTemplateNodeTransformEventArgs(
    MenuTemplateNode menuTemplateNode,
    ISelectedItems items,
    IServiceProvider services)
  {
    this.MenuTemplateNode = menuTemplateNode;
    this._items = items;
    this._services = services;
  }
}
