
// Type: Intermech.Search.ObjectGroups.ObjectGroupClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupClientModule
{
  private ObjectGroupCommandsProvider _objectGroupCommandsProvider = new ObjectGroupCommandsProvider();

  public void Load()
  {
    IFactory factory = ServiceLocator.Get<IFactory>();
    factory.AddViewsProvider((IViewsProvider) new ObjectGroupViewProvider());
    factory.AddCommandsProvider((ICommandsProvider) this._objectGroupCommandsProvider);
  }

  public void Unload()
  {
    ServiceLocator.Get<IFactory>().RemoveCommandsProvider((ICommandsProvider) this._objectGroupCommandsProvider);
  }
}
