
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class GroupAttributesChangingClientModule
{
  private static readonly MenuTemplateNode GroupAttributeChangingMenuTemplateNode = new MenuTemplateNode("GroupAttributesChanging", "Изменить по маске", -1, 10, 70);
  private IFactory _factory;
  private GroupAttributesChangingCommandsProvider _groupAttributesChangingCommandsProvider;

  public GroupAttributesChangingClientModule(IFactory factory)
  {
    this._factory = factory != null ? factory : throw new ArgumentNullException(nameof (factory));
  }

  public void Load()
  {
    this._factory.ContextMenuTemplate["Attributes"].Nodes.Add(GroupAttributesChangingClientModule.GroupAttributeChangingMenuTemplateNode);
    this._groupAttributesChangingCommandsProvider = new GroupAttributesChangingCommandsProvider((IGroupAttributesChangingClientService) new GroupAttributesChangingClientService());
    this._factory.AddCommandsProvider((ICommandsProvider) this._groupAttributesChangingCommandsProvider);
  }

  public void Unload()
  {
    this._factory.ContextMenuTemplate["Attributes"].Nodes.Remove(GroupAttributesChangingClientModule.GroupAttributeChangingMenuTemplateNode);
    this._factory.RemoveCommandsProvider((ICommandsProvider) this._groupAttributesChangingCommandsProvider);
  }
}
