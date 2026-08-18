
// Type: Intermech.Client.Core.CompositionCopying.CompositionCopyingInitializerModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.CompositionCopying;

/// <summary>
/// Модуль инициализации для провайдера команды "Создать\Состав по прототипу".
/// </summary>
internal sealed class CompositionCopyingInitializerModule : InitializerModule
{
  private readonly IFactory _navigatorFactory;
  private readonly CompositionCopyingCommandsProvider _commandsProvider;
  private MenuTemplateNode _createSubMenu;
  private MenuTemplateNode _createCompositionByPrototypeNode;

  public CompositionCopyingInitializerModule(
    IFactory navigatorFactory,
    CompositionCopyingCommandsProvider commandsProvider)
  {
    if (navigatorFactory == null)
      throw new ArgumentNullException(nameof (navigatorFactory));
    if (commandsProvider == null)
      throw new ArgumentNullException(nameof (commandsProvider));
    this._navigatorFactory = navigatorFactory;
    this._commandsProvider = commandsProvider;
  }

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.RegisterCommandsProvider();
  }

  /// <summary>
  /// Завершает работу объектов и сервисов, предоставленных модулем.
  /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
  /// </summary>
  protected override void DoShutdown()
  {
    this.UnregisterCommandsProvider();
    base.DoShutdown();
  }

  private void RegisterCommandsProvider()
  {
    this._createSubMenu = this._navigatorFactory.ContextMenuTemplate["Create"];
    if (this._createSubMenu == null)
      return;
    this._createCompositionByPrototypeNode = new MenuTemplateNode("CreateCompositionByPrototype", "Состав по прототипу", -1, 10, 150);
    this._createSubMenu.Nodes.Add(this._createCompositionByPrototypeNode);
    this._navigatorFactory.AddCommandsProvider(1, (ICommandsProvider) this._commandsProvider);
  }

  private void UnregisterCommandsProvider()
  {
    if (this._createSubMenu == null)
      return;
    this._navigatorFactory.RemoveCommandsProvider(1, (ICommandsProvider) this._commandsProvider);
    if (this._createCompositionByPrototypeNode != null)
    {
      this._createSubMenu.Nodes.Remove(this._createCompositionByPrototypeNode);
      this._createCompositionByPrototypeNode = (MenuTemplateNode) null;
    }
    this._createSubMenu = (MenuTemplateNode) null;
  }
}
