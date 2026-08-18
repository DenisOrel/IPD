
// Type: Intermech.Search.UI.ContextMenuHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using System;
using System.Collections.Generic;


namespace Intermech.Search.UI;

public static class ContextMenuHelper
{
  public static MenuTemplateNode GetContextMenuTemplateNodeForCommand(string commandName)
  {
    if (string.IsNullOrEmpty(commandName))
      throw new ArgumentException();
    return ContextMenuHelper.GetMenuTemplateNodeForCommand((IEnumerable<MenuTemplateNode>) Holder.Factory.ContextMenuTemplate.Nodes, commandName);
  }

  private static MenuTemplateNode GetMenuTemplateNodeForCommand(
    IEnumerable<MenuTemplateNode> menuTemplateNodes,
    string commandName)
  {
    foreach (MenuTemplateNode menuTemplateNode in menuTemplateNodes)
    {
      if (menuTemplateNode.Name == commandName)
        return menuTemplateNode;
      MenuTemplateNode templateNodeForCommand = ContextMenuHelper.GetMenuTemplateNodeForCommand((IEnumerable<MenuTemplateNode>) menuTemplateNode.Nodes, commandName);
      if (templateNodeForCommand != null)
        return templateNodeForCommand;
    }
    return (MenuTemplateNode) null;
  }
}
