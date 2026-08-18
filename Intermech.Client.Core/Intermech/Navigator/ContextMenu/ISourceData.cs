
// Type: Intermech.Navigator.ContextMenu.ISourceData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Предоставляет доступ ко всем исходным данным, которые
/// доступны для процесса сборки команд контекстного меню.
/// </summary>
internal interface ISourceData
{
  /// <summary>
  /// Возвращает коллекцию выбранных пользователем элементов навигации.
  /// </summary>
  ISelectedItems Items { get; }

  /// <summary>Возвращает контейнер с дополнительными сервисами.</summary>
  IServiceProvider ViewServices { get; }

  /// <summary>
  /// Возврашает словарь, содержащий коллекцию выбранных пользователем
  /// элементов навигации, разбитую на кластеры. В каждом кластере находятся
  /// элементы, принадлежащие одной и той же категории. Ключем в словаре
  /// служит идентификатор категории.
  /// </summary>
  IDictionary CategoryClusters { get; }

  /// <summary>
  /// Возврашает словарь, содержащий коллекцию выбранных пользователем
  /// элементов навигации, разбитую на кластеры. В каждом кластере находятся
  /// элементы, принадлежащие одной и той же категории и типу. Ключем в словаре
  /// служит CategoryTypeKey - пара идентификатов.
  /// </summary>
  IDictionary TypeClusters { get; }
}
