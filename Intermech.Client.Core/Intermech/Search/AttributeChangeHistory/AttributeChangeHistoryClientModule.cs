
// Type: Intermech.Search.AttributeChangeHistory.AttributeChangeHistoryClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search.AttributeChangeHistory;

public sealed class AttributeChangeHistoryClientModule
{
  private AttributeChangeHistoryClientService _attributeChangeHistoryClientService = new AttributeChangeHistoryClientService();
  private AttributeChangeHistoryCommandsProvider _attributeChangeHistoryCommandsProvider = new AttributeChangeHistoryCommandsProvider();
  private MenuTemplateNode _showAttributeChangeHistoryFromMenuTemplateNode = new MenuTemplateNode("ShowAttributeChangeHistoryForm", "История изменений", -1, -1, -1);
  private MenuButtonItem _showAttributeChangeHistoryMenuButtonItem = new MenuButtonItem("История изменения атрибутов", new EventHandler(AttributeChangeHistoryClientModule.ShowAttributeChangeHistoryForm));

  public void Load()
  {
    ServiceLocator.Register<IAttributeChangeHistoryClientService>((IAttributeChangeHistoryClientService) this._attributeChangeHistoryClientService);
    IFactory factory = ServiceLocator.Get<IFactory>();
    factory.AddCommandsProvider((ICommandsProvider) this._attributeChangeHistoryCommandsProvider);
    factory.ContextMenuTemplate["Attributes"].Nodes.Add(this._showAttributeChangeHistoryFromMenuTemplateNode);
    ServiceLocator.Get<IMainMenuService>().RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Default, this._showAttributeChangeHistoryMenuButtonItem);
  }

  public void Unload()
  {
    ServiceLocator.Unregister<IAttributeChangeHistoryClientService>();
    IFactory factory = ServiceLocator.Get<IFactory>();
    factory.RemoveCommandsProvider((ICommandsProvider) this._attributeChangeHistoryCommandsProvider);
    factory.ContextMenuTemplate["Attributes"].Nodes.Remove(this._showAttributeChangeHistoryFromMenuTemplateNode);
    ServiceLocator.Get<IMainMenuService>().UnregiterMenuItems(this._showAttributeChangeHistoryMenuButtonItem);
  }

  private static void ShowAttributeChangeHistoryForm(object sender, EventArgs e)
  {
    ServiceLocator.Get<IAttributeChangeHistoryClientService>().ShowAttributeChangeHistoryForm();
  }
}
