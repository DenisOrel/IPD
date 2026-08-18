
// Type: Intermech.Navigator.ContextMenu.Extensions.ICommandsFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.ContextMenu.Extensions;

/// <summary>Позволяет фильтровать команды, доступные в контексте</summary>
public interface ICommandsFilter
{
  /// <summary>Осуществаляет фильтрацию видимости команд в данном контексте. Для скрытия команды надо установить параметр commanIsVisible
  /// в false. При этом надо учитывать, что фильтры в контексте могут быть вложенными, соотв. более "глубокий" фильтр (например
  /// контрол вложенный в данный контрол) может отфильтровать команду ранее. Если команда всё же нужна - можно установить
  /// видимость принудительно в true. </summary>
  /// <param name="items">Список выделенных сущностей</param>
  /// <param name="commandWithVisibleStatuses">Перечисление команд и их статусов</param>
  void FilterCommands(
    ISelectedItems items,
    IEnumerable<CommandAndVisibleStatus> commandWithVisibleStatuses);
}
