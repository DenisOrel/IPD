
// Type: Intermech.Search.AutoConcretization.AutoConcretizationClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Concretization;
using System;


namespace Intermech.Search.AutoConcretization;

public sealed class AutoConcretizationClientModule
{
  private static readonly MenuTemplateNode EnableAutoConcretizationMenuTemplateNode = new MenuTemplateNode("EnableAutoConcretization", "Включить автоконкретизацию состава", -1, -1, -1);
  private static readonly MenuTemplateNode DisableAutoConcretizationMenuTemplateNode = new MenuTemplateNode("DisableAutoConcretization", "Выключить автоконкретизацию состава", -1, -1, -1);
  private IFactory _factory;
  private IConcretizationClientService _concretizationClientService;
  private AutoConcretizationCommandsProvider _autoConcretizationCommandsProvider;

  public AutoConcretizationClientModule(
    IFactory factory,
    IConcretizationClientService concretizationClientService)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    if (concretizationClientService == null)
      throw new ArgumentNullException(nameof (concretizationClientService));
    this._factory = factory;
    this._concretizationClientService = concretizationClientService;
  }

  public void Load()
  {
    this._factory.ContextMenuTemplate["Core.Concretization"].Nodes.Add(AutoConcretizationClientModule.EnableAutoConcretizationMenuTemplateNode);
    this._factory.ContextMenuTemplate["Core.Abstraction"].Nodes.Add(AutoConcretizationClientModule.DisableAutoConcretizationMenuTemplateNode);
    this._autoConcretizationCommandsProvider = new AutoConcretizationCommandsProvider((IAutoConcretizationClientService) new AutoConcretizationClientService(this._concretizationClientService));
    this._factory.AddCommandsProvider((ICommandsProvider) this._autoConcretizationCommandsProvider);
  }

  public void Unload()
  {
    this._factory.ContextMenuTemplate["Core.Concretization"].Nodes.Remove(AutoConcretizationClientModule.EnableAutoConcretizationMenuTemplateNode);
    this._factory.ContextMenuTemplate["Core.Abstraction"].Nodes.Remove(AutoConcretizationClientModule.DisableAutoConcretizationMenuTemplateNode);
    this._factory.RemoveCommandsProvider((ICommandsProvider) this._autoConcretizationCommandsProvider);
  }
}
