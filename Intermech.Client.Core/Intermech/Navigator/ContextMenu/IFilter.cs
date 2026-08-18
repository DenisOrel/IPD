
// Type: Intermech.Navigator.ContextMenu.IFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Позволяет реализовать алгоритм фильтрации команд,
/// попадающих в таблицу при ее построении.
/// </summary>
public interface IFilter
{
  /// <summary>
  /// Возвращает true, если команда с таким именем может быть
  /// помещена в таблицу команд.
  /// </summary>
  /// <param name="commandName">Имя команды</param>
  /// <returns>Признак прохождения условий фильтра.</returns>
  bool PassCommand(string commandName);
}
