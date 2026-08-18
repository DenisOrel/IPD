
// Type: Intermech.Client.Core.CompositionCopying.CompositionCopyingCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.CompositionCopying;

/// <summary>Провайдер команды "Создать\Состав по прототипу".</summary>
internal sealed class CompositionCopyingCommandsProvider : ICommandsProvider
{
  private ICompositionCopyingDispatcherService _compositionCopyingDispatcherService;

  public CompositionCopyingCommandsProvider(
    ICompositionCopyingDispatcherService compositionCopyingDispatcherService)
  {
    this._compositionCopyingDispatcherService = compositionCopyingDispatcherService != null ? compositionCopyingDispatcherService : throw new ArgumentNullException(nameof (compositionCopyingDispatcherService));
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    Action handler = this._compositionCopyingDispatcherService.FindHandler(items, viewServices);
    if (handler == null)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("CreateCompositionByPrototype", new CommandInfo(-1, new ClickEventHandler(this.CreateCompositionByPrototype), (object) handler));
    return groupCommands;
  }

  private void CreateCompositionByPrototype(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    ((Action) additionalInfo)();
  }
}
