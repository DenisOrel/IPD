
// Type: Intermech.Navigator.ContextMenu.CommandLink
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Описывает фрагмент односвязного списка, содержащего обработчики
/// одной и той же команды контекстного меню.
/// </summary>
public class CommandLink
{
  private CommandInfo _commandInfo;
  private ItemsLink _itemsLink;
  private CommandLink _next;

  public CommandLink(CommandInfo commandInfo, ISelectedItems items)
  {
    Services.Check(commandInfo);
    this._commandInfo = commandInfo;
    if (items != null)
      this._itemsLink = new ItemsLink(items);
    this._next = (CommandLink) null;
  }

  /// <summary>Возвращает контейнер с информацией о команде.</summary>
  public CommandInfo CommandInfo
  {
    get => this._commandInfo;
    set => this._commandInfo = value;
  }

  /// <summary>
  /// Возвращает список коллекций элементов навигации, представляющих
  /// область действия команды.
  /// </summary>
  public ItemsLink ItemsLink
  {
    get => this._itemsLink;
    set => this._itemsLink = value;
  }

  /// <summary>Возвращает следующий фрагмент списка.</summary>
  public CommandLink Next
  {
    get => this._next;
    set => this._next = value;
  }
}
