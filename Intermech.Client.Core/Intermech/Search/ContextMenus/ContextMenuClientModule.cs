
// Type: Intermech.Search.ContextMenus.ContextMenuClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenuClientModule
{
  private ContextMenusCommandsProvider _contextMenusCommandsProvider = new ContextMenusCommandsProvider();

  public void Load()
  {
    ServiceLocator.Register<IContextMenuClientService>((IContextMenuClientService) new ContextMenuClientService());
    IFactory factory = ServiceLocator.Get<IFactory>();
    factory.AddViewsProvider((IViewsProvider) new ContextMenuViewsProvider());
    factory.AddCommandsProvider(1, ContextMenuConstants.ContextMenuObjectTypeID, (ICommandsProvider) this._contextMenusCommandsProvider);
    ServiceLocator.Get<IStartupService>().StartupComplete += new EventHandler(this.StartupService_StartupComplete);
  }

  public void Unload()
  {
    ServiceLocator.Unregister<IContextMenuClientService>();
    ServiceLocator.Get<IStartupService>().StartupComplete -= new EventHandler(this.StartupService_StartupComplete);
  }

  private void StartupService_StartupComplete(object sender, EventArgs e)
  {
    ServiceLocator.Get<IContextMenuClientService>().ReloadCache();
  }
}
