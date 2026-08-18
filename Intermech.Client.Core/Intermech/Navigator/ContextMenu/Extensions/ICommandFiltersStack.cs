
// Type: Intermech.Navigator.ContextMenu.Extensions.ICommandFiltersStack
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.ContextMenu.Extensions;

/// <summary>Интерфейс стека сервисов фильтрации контекстных команд в локальном контексте.
/// Позволяет организовывать работу однотивных сервисов, находящихся во вложенных друг в
/// друга контейнерах. Например "фильтрация команд контекстного меню должна осуществляться контролом, а так же всеми
/// контролами, в которые он вложен (поддерживающих сервис фильтрации команд)"</summary>
public interface ICommandFiltersStack : IContextServicesStack<ICommandsFilter>
{
  /// <summary>Выполнение кумулятивной фильтрации используя все фильтры в стеке</summary>
  /// <param name="items">Список выделенных сущностей</param>
  /// <param name="commands">Массив команд и их статусов видимости</param>
  void FilterCommands(ISelectedItems items, CommandsTable commandsTable, bool defaultVisibleStatus);
}
