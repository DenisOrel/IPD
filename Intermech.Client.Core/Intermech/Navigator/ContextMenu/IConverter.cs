
// Type: Intermech.Navigator.ContextMenu.IConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Позволяет реализовать алгоритм преобразования таблицы команд контекстного меню в
/// компонент пользовательского интерфейса.
/// </summary>
internal interface IConverter
{
  /// <summary>
  /// Преобразует таблицу команд в компонент пользовательского интерфейса.
  /// </summary>
  /// <param name="commandsTable">Таблица команд</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  /// <returns>Компонент пользовательского интерфейса</returns>
  Component ToContextMenu(CommandsTable commandsTable, IServiceProvider viewServices);
}
