
// Type: Intermech.Navigator.ContextMenu.MergedCommandsCollector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu.Extensions;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует алгоритм сбора команд, которые могут содержать несколько
/// обработчиков, каждый их которых обрабатывает свое подмножество
/// элементов навигации.
/// </summary>
internal class MergedCommandsCollector : BaseCommandsCollector, ICollector
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
    bool flag1 = service1 != null && (service1.ViewState & ViewStateFlags.DisableGlobalCommandProviders) != 0;
    IContextServicesStack<ILocalCommandsProvider> service2 = sourceData.ViewServices.GetService<IContextServicesStack<ILocalCommandsProvider>>(false);
    ICommandFiltersStack service3 = sourceData.ViewServices.GetService<ICommandFiltersStack>(false);
    bool flag2 = true;
    foreach (DictionaryEntry typeCluster in sourceData.TypeClusters)
    {
      CategoryTypeKey key = (CategoryTypeKey) typeCluster.Key;
      ISelectedItems items = (ISelectedItems) typeCluster.Value;
      tableBuilder.Reset();
      CommandsTable viewCommands = this.GetViewCommands(sourceData, items);
      if (viewCommands != null)
      {
        service3?.FilterCommands(sourceData.Items, viewCommands, true);
        tableBuilder.Combine(viewCommands);
      }
      if (service2 != null)
        this.CombineWithContextCommands(sourceData, service3, (IEnumerable<ICommandsProvider>) service2.Enumeration, tableBuilder, items);
      if (!flag1 || service3 != null)
      {
        CommandsTable commands1 = this.GetCommands(key.CategoryID, key.TypeID, sourceData, items);
        if (commands1 != null)
        {
          service3?.FilterCommands(sourceData.Items, commands1, !flag1);
          tableBuilder.Combine(commands1);
        }
        CommandsTable commands2 = this.GetCommands(key.CategoryID, sourceData, items);
        if (commands2 != null)
        {
          service3?.FilterCommands(sourceData.Items, commands2, !flag1);
          tableBuilder.Combine(commands2);
        }
        CommandsTable commands3 = this.GetCommands(sourceData, items);
        if (commands3 != null)
        {
          service3?.FilterCommands(sourceData.Items, commands3, !flag1);
          tableBuilder.Combine(commands3);
        }
      }
      CommandsTable commandsTable = tableBuilder.ToCommandsTable();
      if (flag2)
      {
        builder.Combine(commandsTable);
        flag2 = false;
      }
      else
        builder.Merge(commandsTable);
    }
  }

  /// <summary>
  /// Возвращает таблицу команд, предоставленных визуальным элементом,
  /// который инициировал создание контекстного меню.
  /// </summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="items">Коллекция элементов навигации, представляющая область действия для команд</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetViewCommands(ISourceData sourceData, ISelectedItems items)
  {
    ICommandsProvider service = (ICommandsProvider) sourceData.ViewServices.GetService(typeof (ICommandsProvider));
    if (service == null)
      return (CommandsTable) null;
    return this.GetCommands(sourceData, items, service);
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
    CommandsTableBuilder tableBuilder,
    ISelectedItems items)
  {
    if (commandsProvidersStack == null)
      return;
    foreach (CommandsTable commandsTable in commandsProvidersStack.Where<ICommandsProvider>((Func<ICommandsProvider, bool>) (provider => provider != null)).SelectNotNull<ICommandsProvider, CommandsTable>((Func<ICommandsProvider, CommandsTable>) (provider => this.GetCommands(sourceData, items, provider))))
    {
      commandFiltersStack?.FilterCommands(sourceData.Items, commandsTable, true);
      tableBuilder.Combine(commandsTable);
    }
  }

  /// <summary>
  /// Возвращает таблицу команд от провайдеров, зарегистрированных на конкретные категорию и
  /// тип элементов навигации.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элементов навигации</param>
  /// <param name="typeID">Идентификатор типа элементов навигации</param>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="items">Коллекция элементов навигации, представляющая область действия для команд</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetCommands(
    int categoryID,
    int typeID,
    ISourceData sourceData,
    ISelectedItems items)
  {
    ICommandsProvider[] commandsProviders = Holder.Factory.GetCommandsProviders(categoryID, typeID);
    return this.GetCommands(sourceData, items, commandsProviders);
  }

  /// <summary>
  /// Возвращает таблицу команд от провайдеров, зарегистрированных на конкретную категорию и
  /// любой тип элементов навигации.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элементов навигации</param>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="items">Коллекция элементов навигации, представляющая область действия для команд</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetCommands(int categoryID, ISourceData sourceData, ISelectedItems items)
  {
    ICommandsProvider[] commandsProviders = Holder.Factory.GetCommandsProviders(categoryID);
    return this.GetCommands(sourceData, items, commandsProviders);
  }

  /// <summary>
  /// Возвращает таблицу команд от провайдеров, зарегистрированных на любую категорию и
  /// тип элементов навигации.
  /// </summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="items">Коллекция элементов навигации, представляющая область действия для команд</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetCommands(ISourceData sourceData, ISelectedItems items)
  {
    ICommandsProvider[] commandsProviders = Holder.Factory.GetCommandsProviders();
    return this.GetCommands(sourceData, items, commandsProviders);
  }

  /// <summary>Возвращает таблицу команд от провайдеров.</summary>
  /// <param name="sourceData">Исходные данные для поиска подходящих команд</param>
  /// <param name="items">Коллекция элементов навигации, представляющая область действия для команд</param>
  /// <param name="providers">Массив провайдеров</param>
  /// <returns>Таблица команд</returns>
  private CommandsTable GetCommands(
    ISourceData sourceData,
    ISelectedItems items,
    params ICommandsProvider[] providers)
  {
    if (providers == null)
      return (CommandsTable) null;
    CommandsTableBuilder commandsTableBuilder = new CommandsTableBuilder(true);
    for (int index = 0; index < providers.Length; ++index)
    {
      try
      {
        CommandsInfo mergedCommands = providers[index].GetMergedCommands(items, sourceData.ViewServices);
        commandsTableBuilder.Insert(mergedCommands, items);
      }
      catch (Exception ex)
      {
        this.ShowError(ex);
      }
    }
    return commandsTableBuilder.ToCommandsTable();
  }
}
