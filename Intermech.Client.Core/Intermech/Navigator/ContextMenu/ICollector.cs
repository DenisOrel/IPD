
// Type: Intermech.Navigator.ContextMenu.ICollector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.ContextMenu;

/// <summary>Позволяет реализовать алгоритм сбора команд.</summary>
internal interface ICollector
{
  /// <summary>
  /// Собирает команды в соответствии с реализованным алгоритмом и
  /// помещает их в результирующую таблицу с помощью построителя таблиц.
  /// </summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="builder">Построитель, предоставляющий методы для операций с таблицами команд</param>
  void Execute(ISourceData sourceData, CommandsTableBuilder builder);
}
