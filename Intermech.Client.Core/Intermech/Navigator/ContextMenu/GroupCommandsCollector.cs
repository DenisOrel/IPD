
// Type: Intermech.Navigator.ContextMenu.GroupCommandsCollector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu.Extensions;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует алгоритм сбора групповых команд, т.е. обрабатывающих все выделенные
/// пользователем элементы навигации.
/// </summary>
internal class GroupCommandsCollector : BaseCommandsCollector, ICollector
{
  /// <summary>
  /// Собирает команды в соответствии с реализованным алгоритмом и
  /// помещает их в результирующую таблицу с помощью построителя таблиц.
  /// </summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="builder">Построитель, предоставляющий методы для операций с таблицами команд</param>
  void ICollector.Execute(ISourceData sourceData, CommandsTableBuilder builder)
  {
    Services.Check(sourceData);
    Services.Check(builder);
    CommandsTableBuilder tableBuilder = new CommandsTableBuilder(true);
    IViewState service1 = sourceData.ViewServices.GetService<IViewState>(false);
    bool flag = service1 != null && (service1.ViewState & ViewStateFlags.DisableGlobalCommandProviders) != 0;
    IContextServicesStack<ILocalCommandsProvider> service2 = sourceData.ViewServices.GetService<IContextServicesStack<ILocalCommandsProvider>>(false);
    ICommandFiltersStack service3 = sourceData.ViewServices.GetService<ICommandFiltersStack>(false);
    CommandsTable viewCommands = this.GetViewCommands(sourceData);
    if (viewCommands != null)
    {
      service3?.FilterCommands(sourceData.Items, viewCommands, true);
      tableBuilder.Combine(viewCommands);
    }
    if (service2 != null)
      this.CombineWithContextCommands(sourceData, service3, (IEnumerable<ICommandsProvider>) service2.Enumeration, tableBuilder);
    if (!flag || service3 != null)
    {
      CommandsTable typeCommands = this.GetTypeCommands(sourceData);
      if (typeCommands != null)
      {
        service3?.FilterCommands(sourceData.Items, typeCommands, !flag);
        tableBuilder.Combine(typeCommands);
      }
      CommandsTable categoryCommands = this.GetCategoryCommands(sourceData);
      if (categoryCommands != null)
      {
        service3?.FilterCommands(sourceData.Items, categoryCommands, !flag);
        tableBuilder.Combine(categoryCommands);
      }
      CommandsTable commonCommands = this.GetCommonCommands(sourceData);
      if (commonCommands != null)
      {
        service3?.FilterCommands(sourceData.Items, commonCommands, !flag);
        tableBuilder.Combine(commonCommands);
      }
    }
    CommandsTable commandsTable = tableBuilder.ToCommandsTable();
    builder.Combine(commandsTable);
  }

  /// <summary>
  /// Возвращает таблицу команд, предоставленных визуальным элементом,
  /// который инициировал создание контекстного меню.
  /// </summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetViewCommands(ISourceData sourceData)
  {
    ICommandsProvider service = (ICommandsProvider) sourceData.ViewServices.GetService(typeof (ICommandsProvider));
    if (service == null)
      return (CommandsTable) null;
    return this.GetCommands(sourceData, service);
  }

  /// <summary>Если контекст поддерживает дополнение списка команд собственными, то собираю дополнительные команды контекста</summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="commandFiltersStack">Стек фильтров комманд</param>
  /// <param name="commandsProvidersStack">Стек провайдеров команд в данном контексте</param>
  /// <param name="tableBuilder"></param>
  private void CombineWithContextCommands(
    ISourceData sourceData,
    ICommandFiltersStack commandFiltersStack,
    IEnumerable<ICommandsProvider> commandsProvidersStack,
    CommandsTableBuilder tableBuilder)
  {
    if (commandsProvidersStack == null)
      return;
    foreach (CommandsTable commandsTable in commandsProvidersStack.Where<ICommandsProvider>((Func<ICommandsProvider, bool>) (provider => provider != null)).SelectNotNull<ICommandsProvider, CommandsTable>((Func<ICommandsProvider, CommandsTable>) (provider => this.GetCommands(sourceData, provider))))
    {
      commandFiltersStack?.FilterCommands(sourceData.Items, commandsTable, true);
      tableBuilder.Combine(commandsTable);
    }
  }

  /// <summary>
  /// Возвращает таблицу команд от провайдеров, зарегистрированных на конкретную категорию и
  /// любой тип элементов навигации.
  /// </summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetCategoryCommands(ISourceData sourceData)
  {
    if (sourceData.CategoryClusters.Count != 1)
      return (CommandsTable) null;
    INodeID itemId = sourceData.Items.GetItemID(0);
    ICommandsProvider[] commandsProviders = Holder.Factory.GetCommandsProviders(itemId.CategoryID);
    return this.GetCommands(sourceData, commandsProviders);
  }

  /// <summary>
  /// Возвращает таблицу команд от провайдеров, зарегистрированных на конкретные категорию и
  /// тип элементов навигации.
  /// </summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetTypeCommands(ISourceData sourceData)
  {
    if (sourceData.TypeClusters.Count != 1)
      return (CommandsTable) null;
    INodeID itemId = sourceData.Items.GetItemID(0);
    ICommandsProvider[] commandsProviders = Holder.Factory.GetCommandsProviders(itemId.CategoryID, itemId.TypeID);
    return this.GetCommands(sourceData, commandsProviders);
  }

  /// <summary>
  /// Возвращает таблицу команд от провайдеров, зарегистрированных на любую категорию и
  /// тип элементов навигации.
  /// </summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetCommonCommands(ISourceData sourceData)
  {
    ICommandsProvider[] commandsProviders = Holder.Factory.GetCommandsProviders();
    return this.GetCommands(sourceData, commandsProviders);
  }

  /// <summary>Возвращает таблицу команд от провайдеров.</summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="providers">Массив провайдеров</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetCommands(ISourceData sourceData, params ICommandsProvider[] providers)
  {
    if (providers == null)
      return (CommandsTable) null;
    CommandsTableBuilder commandsTableBuilder = new CommandsTableBuilder(true);
    for (int index = 0; index < providers.Length; ++index)
    {
      try
      {
        CommandsInfo groupCommands = providers[index].GetGroupCommands(sourceData.Items, sourceData.ViewServices);
        commandsTableBuilder.Insert(groupCommands, sourceData.Items);
      }
      catch (Exception ex)
      {
        this.ShowError(ex);
      }
    }
    return commandsTableBuilder.ToCommandsTable();
  }
}
