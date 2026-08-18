
// Type: Intermech.Navigator.ContextMenu.Extensions.CommandFiltersStack
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;


namespace Intermech.Navigator.ContextMenu.Extensions;

/// <summary>Стека сервисов фильтрации контекстных команд в локальном контексте.
/// Позволяет организовывать работу однотивных сервисов, находящихся во вложенных друг в
/// друга контейнерах. Например "фильтрация команд контекстного меню должна осуществляться контролом, а так же всеми
/// контролами, в которые он вложен (поддерживающих сервис фильтрации команд)"</summary>
/// <summary>Конструктор</summary>
/// <param name="localContext"></param>
/// <param name="localCommandsFilter"></param>
public class CommandFiltersStack(
  IServiceContainer localContext,
  ICommandsFilter localCommandsFilter) : 
  ContextServicesStack<ICommandsFilter>(localContext, localCommandsFilter),
  ICommandFiltersStack,
  IContextServicesStack<ICommandsFilter>
{
  /// <summary>Выполнение кумулятивной фильтрации используя все фильтры в стеке</summary>
  /// <param name="items">Список выделенных сущностей</param>
  /// <param name="commands">Список команд и их статусов видимости</param>
  public void FilterCommands(
    ISelectedItems items,
    CommandsTable commandsTable,
    bool defaultVisibleStatus)
  {
    if (!((IEnumerable<string>) commandsTable.CommandNames).Any<string>())
      return;
    CommandAndVisibleStatus[] array = ((IEnumerable<string>) commandsTable.CommandNames).Select<string, CommandAndVisibleStatus>((Func<string, CommandAndVisibleStatus>) (commandName => new CommandAndVisibleStatus(commandName, defaultVisibleStatus))).ToArray<CommandAndVisibleStatus>(commandsTable.CommandNames.Length);
    foreach (ICommandsFilter commandsFilter in this.Enumeration.Reverse<ICommandsFilter>())
      commandsFilter.FilterCommands(items, (IEnumerable<CommandAndVisibleStatus>) array);
    foreach (string commandName in ((IEnumerable<CommandAndVisibleStatus>) array).Where<CommandAndVisibleStatus>((Func<CommandAndVisibleStatus, bool>) (command => !command.IsVisible)).Select<CommandAndVisibleStatus, string>((Func<CommandAndVisibleStatus, string>) (command => command.Name)))
      commandsTable.Remove(commandName);
  }
}
