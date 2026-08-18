
// Type: Intermech.Navigator.ContextMenu.CommandsTableBuilder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Предназначен для построения таблицы команд на основе данных,
/// полученных от провайдеров команд или других источников.
/// </summary>
internal class CommandsTableBuilder
{
  /// <summary>Таблица команд контекстного меню</summary>
  private CommandsTable _commandsTable;
  /// <summary>
  /// Возвращает или устанавливает значение признака, который определяет,
  /// будет ли оставаться в построенной таблице информация о подавленных
  /// командах.
  /// </summary>
  private bool _keepSuppressed;
  /// <summary>Исключать из списка команд те команды, которые скрыты</summary>
  private bool _excludeInvisible;

  /// <summary>Создает новый построитель таблицы команд.</summary>
  public CommandsTableBuilder()
    : this(false)
  {
  }

  /// <summary>
  /// Создает новый построитель таблицы команд, позволяя указать, должен
  /// ли он оставлять в построенной таблице информацию о подавленных
  /// командах.
  /// </summary>
  /// <param name="keepSuppressed">Признак сохранения информации о подавленных командах</param>
  public CommandsTableBuilder(bool keepSuppressed)
  {
    this._commandsTable = new CommandsTable();
    this._keepSuppressed = keepSuppressed;
  }

  /// <summary>
  /// Возвращает или устанавливает значение признака, который определяет,
  /// будет ли оставаться в построенной таблице информация о подавленных
  /// командах.
  /// </summary>
  public bool KeepSuppressed
  {
    get => this._keepSuppressed;
    set => this._keepSuppressed = value;
  }

  /// <summary>Исключать из списка команд те команды, которые скрыты</summary>
  public bool ExcludeInvisible
  {
    get => this._excludeInvisible;
    set => this._excludeInvisible = value;
  }

  /// <summary>
  /// Приводит построитель в исходное состояние для построения новой таблицы.
  /// </summary>
  public void Reset() => this._commandsTable.Clear();

  /// <summary>
  /// Вставляет новую команду в таблицу. Если при вставке окажется, что в таблице
  /// уже есть команда с такми именем, то она будет замещена новой только в том случае,
  /// если ее приоритет ниже приоритена вставляемой команды.
  /// </summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandInfo">Информация о команде</param>
  /// <param name="items">Коллекция элементов навигации, которые обрабатывает команда</param>
  public void Insert(string commandName, CommandInfo commandInfo, ISelectedItems items)
  {
    Services.Check(commandName);
    Services.Check(commandInfo);
    Services.Check(items);
    CommandLink commandLink = this._commandsTable[commandName];
    if (commandLink == null)
    {
      this._commandsTable.Add(commandName, new CommandLink(commandInfo, items));
    }
    else
    {
      if (commandLink.CommandInfo.Priority >= commandInfo.Priority)
        return;
      if (commandLink.Next != null)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3787.ssp_imclient_3788()));
      if (commandLink.ItemsLink.Next != null)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3787.ssp_imclient_3789()));
      if (commandLink.ItemsLink.Items != items)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3787.ssp_imclient_3790()));
      commandLink.CommandInfo = commandInfo;
    }
  }

  /// <summary>
  /// Вставляет новые команды в таблицу. Если при вставке окажется, что в таблице
  /// уже есть команда с такми именем, то она будет замещена новой только в том случае,
  /// если ее приоритет ниже приоритена вставляемой команды.
  /// </summary>
  /// <param name="info">Контейнер с информацией о командах</param>
  /// <param name="items">Коллекция элементов навигации, которые обрабатывают команды</param>
  public void Insert(CommandsInfo info, ISelectedItems items)
  {
    Services.Check(info);
    Services.Check(items);
    if (info.CommandNames == null)
      return;
    for (int index = 0; index < info.CommandNames.Length; ++index)
    {
      string commandName = info.CommandNames[index];
      this.Insert(commandName, info.GetInfo(commandName), items);
    }
  }

  /// <summary>
  /// Вставляет в таблицу команды из указанной таблицы, выбирая только те,
  /// которые отсутствуют в создаваемой. Вставляемые команды удаляются из
  /// исходной таблицы.
  /// </summary>
  /// <param name="table">Таблица команд</param>
  public void Combine(CommandsTable table)
  {
    Services.Check(table);
    foreach (string commandName in table.CommandNames)
    {
      if (!this._commandsTable.Contains(commandName))
      {
        this._commandsTable.Add(commandName, table[commandName]);
        table.Remove(commandName);
      }
    }
  }

  /// <summary>
  /// Строит пересечение создаваемой таблицы команд и новой таблицы команд.
  /// В результате этой операции в создаваемой таблице останутся только те
  /// команды, которые присутствуют в обеих таблицах. Те команды, которые
  /// не попадут в пересечение, будут удалены из обеих таблиц.
  /// </summary>
  /// <param name="table">Таблица команд, с которой строится пересечение.</param>
  public void Merge(CommandsTable table)
  {
    Services.Check(table);
    string[] commandNames1 = this._commandsTable.CommandNames;
    for (int index = 0; index < commandNames1.Length; ++index)
    {
      if (!table.Contains(commandNames1[index]))
        this._commandsTable.Remove(commandNames1[index]);
    }
    string[] commandNames2 = table.CommandNames;
    for (int index = 0; index < commandNames2.Length; ++index)
    {
      CommandLink existingLink = this._commandsTable[commandNames2[index]];
      if (existingLink != null)
      {
        CommandLink newLink = table[commandNames2[index]];
        this.MergeCommands(existingLink, newLink);
        table.Remove(commandNames2[index]);
      }
    }
  }

  /// <summary>Возвращает построенную таблицу команд.</summary>
  /// <returns>Таблица команд</returns>
  public CommandsTable ToCommandsTable()
  {
    if (!this._keepSuppressed)
      this.RemoveSuppressedCommands();
    if (this._excludeInvisible)
      this.RemoveHiddenCommands();
    return this._commandsTable;
  }

  /// <summary>
  /// Выполняет объединение списков обработчиков или областей действия команды.
  /// </summary>
  private void MergeCommands(CommandLink existingLink, CommandLink newLink)
  {
    CommandLink next;
    for (; newLink != null; newLink = next)
    {
      next = newLink.Next;
      for (CommandLink commandLink = existingLink; commandLink != null; commandLink = commandLink.Next)
      {
        if (this.SameClickHandlers(commandLink.CommandInfo, newLink.CommandInfo))
        {
          ItemsLink itemsLink = existingLink.ItemsLink;
          while (itemsLink.Next != null)
            itemsLink = itemsLink.Next;
          itemsLink.Next = newLink.ItemsLink;
          newLink.ItemsLink = (ItemsLink) null;
          break;
        }
        if (commandLink.Next == null)
        {
          existingLink.Next = newLink;
          newLink.Next = (CommandLink) null;
          break;
        }
      }
    }
  }

  /// <summary>
  /// Возвращает признак, что две команды имеют одинаковые обработчики.
  /// </summary>
  /// <param name="infoA">Информация о первой команде</param>
  /// <param name="infoB">Информация о второй команде</param>
  /// <returns>Признак равенства обработчиков</returns>
  private bool SameClickHandlers(CommandInfo infoA, CommandInfo infoB)
  {
    return ((infoA.ClickHandler == null || infoB.ClickHandler == null ? 0 : (infoA.ClickHandler.Equals((object) infoB.ClickHandler) ? 1 : 0)) & (infoA.AdditionalInfo == null ? (infoB.AdditionalInfo == null ? 1 : 0) : (infoA.AdditionalInfo.Equals(infoB.AdditionalInfo) ? 1 : 0))) != 0;
  }

  /// <summary>
  /// Удаляет из создаваемой таблицы информацию о подавленных командах.
  /// Такие команды характеризуются отсутствием делегата метода,
  /// который выполняет команду.
  /// </summary>
  private void RemoveSuppressedCommands()
  {
    string[] commandNames = this._commandsTable.CommandNames;
    for (int index = 0; index < commandNames.Length; ++index)
    {
      for (CommandLink next = this._commandsTable[commandNames[index]]; next != null; next = next.Next)
      {
        if (next.CommandInfo.ClickHandler == null || next.CommandInfo.ClickHandler != null && next.CommandInfo.ClickHandler.GetInvocationList().Length == 0)
        {
          this._commandsTable.Remove(commandNames[index]);
          break;
        }
      }
    }
  }

  /// <summary>
  /// Удаляет из создаваемой таблицы информацию о скрытых командах
  /// </summary>
  private void RemoveHiddenCommands()
  {
    AdjustableMenuCommands service = ServicesManager.GetService(typeof (AdjustableMenuCommands)) as AdjustableMenuCommands;
    string[] commandNames = this._commandsTable.CommandNames;
    for (int index = 0; index < commandNames.Length; ++index)
    {
      string command = commandNames[index];
      AdjustableMenuCommand commandFromRoot = service.FindCommandFromRoot(command);
      if (commandFromRoot != null && !commandFromRoot.Visible && this._excludeInvisible)
        this._commandsTable.Remove(commandNames[index]);
    }
  }
}
