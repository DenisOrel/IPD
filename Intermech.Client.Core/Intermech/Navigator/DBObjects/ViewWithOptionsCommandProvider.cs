
// Type: Intermech.Navigator.DBObjects.ViewWithOptionsCommandProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Interfaces.StandaloneView;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер для команды "Смотреть..."</summary>
internal class ViewWithOptionsCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    if (items.Count != 1 || ((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 536870913L /*0x20000001*/) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("ViewWithOptions", new CommandInfo(0, new ClickEventHandler(this.ViewWithOptionsCommandHandler)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>
  /// Обработчик команды "Смотреть...". Позволяет перед просмотром документа сконфигурировать опции записи в файл документа
  /// сведений о подписях, контрольной сумме, атрибутах документа и др.
  /// </summary>
  /// <param name="items">Выделенные элементы</param>
  /// <param name="viewServices">Контекстные сервисы</param>
  /// <param name="additionalInfo">Дополнительный параметр команды</param>
  private void ViewWithOptionsCommandHandler(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (items.Count != 1)
      throw new ArgumentOutOfRangeException(nameof (items), "В навигаторе должен быть выделен только один документ.");
    using (new DynamicScope())
    {
      StandaloneViewVars.IsActive.Declare(true);
      StandaloneViewVars.AdjustSettingsInDialogMode.Declare(true);
      ObjectCommands.ViewCommand(items, viewServices, additionalInfo);
    }
  }
}
