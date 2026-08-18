
// Type: Intermech.Search.EditingContexts.EditingContextsClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;


namespace Intermech.Search.EditingContexts;

public sealed class EditingContextsClientModule
{
  private EditingContextsModule _editingContextsModule = new EditingContextsModule();

  public void Load()
  {
    this._editingContextsModule.Load();
    ServiceLocator.Register<IEditingContextClientService>((IEditingContextClientService) new EditingContextClientService());
    IFactory factory = ServiceLocator.Get<IFactory>();
    factory.AddViewsProvider(1, (IViewsProvider) new EditingContextsViewsProvider());
    factory.AddCommandsProvider(1, (ICommandsProvider) new EditingContextsCommandsProvider());
    AdjustableViewsHelper.RegisterView("EditingContextsView", LocalizationHolder.rm.GetString("Client.Core_1225"), LocalizationHolder.rm.GetString("Client.Core_1226"), "Intermech.Navigator", "imgObjectsFilter", true, 0);
  }
}
