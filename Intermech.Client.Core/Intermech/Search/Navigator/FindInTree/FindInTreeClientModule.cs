
// Type: Intermech.Search.Navigator.FindInTree.FindInTreeClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search.Navigator.FindInTree;

public sealed class FindInTreeClientModule
{
  private static readonly MenuTemplateNode FindInTreeMenuTemplateNode = new MenuTemplateNode("FindInTree", "Найти в дереве", -1, -1, -1);
  private IFactory _factory;
  private FindInTreeCommandsProvider _findInTreeCommandsProvider;

  public FindInTreeClientModule(IFactory factory)
  {
    this._factory = factory != null ? factory : throw new ArgumentNullException(nameof (factory));
  }

  public void Load()
  {
    this._factory.ContextMenuTemplate.Nodes.Add(FindInTreeClientModule.FindInTreeMenuTemplateNode);
    this._findInTreeCommandsProvider = new FindInTreeCommandsProvider();
    this._factory.AddCommandsProvider((ICommandsProvider) this._findInTreeCommandsProvider);
  }

  public void Unload()
  {
    this._factory.ContextMenuTemplate.Nodes.Remove(FindInTreeClientModule.FindInTreeMenuTemplateNode);
    this._factory.RemoveCommandsProvider((ICommandsProvider) this._findInTreeCommandsProvider);
  }
}
