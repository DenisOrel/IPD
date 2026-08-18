
// Type: Intermech.Search.Concretization.ConcretizationClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;


namespace Intermech.Search.Concretization;

public sealed class ConcretizationClientModule
{
  private ConcretizationCommandsProvider _concretizationCommandsProvider;
  private MenuTemplateNode _concretizationMenuTemplateNode = new MenuTemplateNode()
  {
    Name = "Core.Concretization",
    Text = "Конкретизация",
    Nodes = {
      new MenuTemplateNode()
      {
        Name = "Concretization.CurrentVersion",
        Text = "Текущая версия"
      },
      new MenuTemplateNode()
      {
        Name = "Concretization.CurrentVersionInComposition",
        Text = "Текущая версия в составе"
      },
      new MenuTemplateNode()
      {
        Name = "Concretization.SelectVersion",
        Text = "Выбрать версию"
      },
      new MenuTemplateNode()
      {
        Name = "Concretization.SelectVersionInComposition",
        Text = "Выбрать версию в составе"
      },
      new MenuTemplateNode()
      {
        Name = "Concretization.EntireComposition",
        Text = "Весь состав"
      }
    }
  };
  private MenuTemplateNode _abstractionMenuTemplateNode = new MenuTemplateNode()
  {
    Name = "Core.Abstraction",
    Text = "Абстрагирование",
    Nodes = {
      new MenuTemplateNode()
      {
        Name = "Abstraction.CurrentVersion",
        Text = "Текущая версия"
      },
      new MenuTemplateNode()
      {
        Name = "Abstraction.CurrentVersionInComposition",
        Text = "Текущая версия в составе"
      },
      new MenuTemplateNode()
      {
        Name = "Abstraction.EntireComposition",
        Text = "Весь состав"
      }
    }
  };
  private MenuTemplateNode _checkVersionMenuTemplateNode = new MenuTemplateNode()
  {
    Name = "Core.CheckVersion",
    Text = "Проверить версию в составе"
  };

  public void Load()
  {
    ConcretizationClientService service = new ConcretizationClientService();
    ServiceLocator.Register<IConcretizationClientService>((IConcretizationClientService) service);
    IFactory factory = ServiceLocator.Get<IFactory>();
    this._concretizationCommandsProvider = new ConcretizationCommandsProvider((IConcretizationClientService) service);
    factory.AddCommandsProvider((ICommandsProvider) this._concretizationCommandsProvider);
    factory.ContextMenuTemplate.Nodes.Add(this._concretizationMenuTemplateNode);
    factory.ContextMenuTemplate.Nodes.Add(this._abstractionMenuTemplateNode);
    factory.ContextMenuTemplate["ObjectComposition"].Nodes.Add(this._checkVersionMenuTemplateNode);
  }

  public void Unload()
  {
    ServiceLocator.Unregister<IConcretizationClientService>();
    IFactory factory = ServiceLocator.Get<IFactory>();
    factory.RemoveCommandsProvider((ICommandsProvider) this._concretizationCommandsProvider);
    factory.ContextMenuTemplate.Nodes.Remove(this._concretizationMenuTemplateNode);
    factory.ContextMenuTemplate.Nodes.Remove(this._abstractionMenuTemplateNode);
    factory.ContextMenuTemplate["ObjectComposition"].Nodes.Remove(this._checkVersionMenuTemplateNode);
  }
}
