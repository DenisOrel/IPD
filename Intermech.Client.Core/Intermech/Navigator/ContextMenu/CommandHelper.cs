
// Type: Intermech.Navigator.ContextMenu.CommandHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.Search.UI.Commands;
using Intermech.UI;
using System;
using System.Collections;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует вспомогательный класс, с помощью которого можно запустить команду на выполнение
/// </summary>
public class CommandHelper
{
  /// <summary>Список обработчиков команды контекстного меню</summary>
  private CommandLink _commandLink;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _viewServices;
  private string _commandName;

  /// <summary>
  /// Создает объект, с помощью которого можно запустить команду на выполнение.
  /// </summary>
  /// <param name="commandLink">Односвязный список обработчиков команды</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  public CommandHelper(string commandName, CommandLink commandLink, IServiceProvider viewServices)
  {
    if (commandName == null)
      throw new ArgumentNullException(nameof (commandName));
    Services.Check(commandLink);
    Services.Check(viewServices);
    this._commandName = commandName;
    this._commandLink = commandLink;
    this._viewServices = viewServices;
  }

  /// <summary>Выполняет команду контекстного меню.</summary>
  public void Execute()
  {
    using (new DynamicScope())
    {
      UIVars.UICommand.Declare(new UICommandInfo("Dynamic command"));
      for (CommandLink commandLink = this._commandLink; commandLink != null; commandLink = commandLink.Next)
      {
        ISelectedItems handlerItems = this.GetHandlerItems(commandLink);
        commandLink.CommandInfo.ClickHandler(handlerItems, this._viewServices, commandLink.CommandInfo.AdditionalInfo);
      }
    }
  }

  /// <summary>Выполняет команду контекстного меню.</summary>
  /// <param name="caption">
  /// Название команды контектного меню, которое будет использовано
  /// для показа диалога об ошибке в случае возникновения
  /// исключительной ситуации
  /// </param>
  public void Execute(string caption)
  {
    using (new DynamicScope())
    {
      UIVars.UICommand.Declare(new UICommandInfo(caption));
      for (CommandLink commandLink = this._commandLink; commandLink != null; commandLink = commandLink.Next)
      {
        try
        {
          this.CollectStatistics(this._commandName);
          ISelectedItems handlerItems = this.GetHandlerItems(commandLink);
          commandLink.CommandInfo.ClickHandler(handlerItems, this._viewServices, commandLink.CommandInfo.AdditionalInfo);
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
      }
    }
  }

  /// <summary>
  /// Возвращает коллекцию элементов навигации, которая будет передана обработчику
  /// команды контекстного меню.
  /// </summary>
  /// <param name="commandLink">Список обработчиков команды контекстного меню</param>
  /// <returns></returns>
  public ISelectedItems GetHandlerItems(CommandLink commandLink)
  {
    if (commandLink.ItemsLink.Next == null)
      return commandLink.ItemsLink.Items;
    ArrayList arrayList = new ArrayList();
    for (ItemsLink itemsLink = commandLink.ItemsLink; itemsLink != null; itemsLink = itemsLink.Next)
      arrayList.Add((object) itemsLink.Items);
    return (ISelectedItems) new UnitedItems((ISelectedItems[]) arrayList.ToArray(typeof (ISelectedItems)));
  }

  /// <summary>Список обработчиков команды контекстного меню</summary>
  public CommandLink CommandLink => this._commandLink;

  private void CollectStatistics(string commandName)
  {
    ICommandStatisticsRepository statisticsRepository = ServiceLocator.Get<ICommandStatisticsRepository>();
    CommandStatistics statistics = statisticsRepository.Find(commandName) ?? new CommandStatistics();
    ++statistics.CurrentSessionUsesCount;
    ++statistics.TotalUsesCount;
    statisticsRepository.AddOrUpdate(commandName, statistics);
  }
}
