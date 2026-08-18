
// Type: Intermech.Search.ContextMenus.ContextMenuClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.ContextMenus;

public static class ContextMenuClientHelper
{
  public static MenuTemplateNode GetMenuTemplateNodeForCommand(string commandName)
  {
    return !string.IsNullOrEmpty(commandName) ? ServiceLocator.Get<IFactory>().ContextMenuTemplate[commandName] : throw new ArgumentException();
  }

  public static ImageList GetImageListForImageListSource(ImageListSource imageListSource)
  {
    if (imageListSource == ImageListSource.CategoryImageList)
      return ServiceLocator.Get<ICategoryTypeIconService>().ImageList;
    return imageListSource == ImageListSource.NamedImageList ? ServiceLocator.Get<INamedImageList>().ImageList : (ImageList) null;
  }

  public static MenuTemplate CreateMenuTemplateFromContextMenu(Intermech.Search.ContextMenus.ContextMenu contextMenu)
  {
    if (contextMenu == null)
      throw new ArgumentNullException(nameof (contextMenu));
    MenuTemplate templateFromContextMenu = new MenuTemplate();
    foreach (ContextMenuItem contextMenuItem in (Collection<ContextMenuItem>) contextMenu.Items)
    {
      MenuTemplateNode fromContextMenuItem = ContextMenuClientHelper.CreateTemplateNodeFromContextMenuItem(contextMenuItem);
      if (fromContextMenuItem != null)
        templateFromContextMenu.Nodes.Add(fromContextMenuItem);
    }
    templateFromContextMenu.RebuildNameHash();
    return templateFromContextMenu;
  }

  private static MenuTemplateNode CreateTemplateNodeFromContextMenuItem(
    ContextMenuItem contextMenuItem)
  {
    int groupID = contextMenuItem.GetPreviousSiblingsAndSelf().Count<ContextMenuItem>((Func<ContextMenuItem, bool>) (o => o.BeginGroup));
    int orderID = contextMenuItem.Parent.Items.IndexOf(contextMenuItem);
    MenuTemplateNode fromContextMenuItem1 = (MenuTemplateNode) null;
    if (!string.IsNullOrEmpty(contextMenuItem.CommandName))
    {
      MenuTemplateNode templateNodeForCommand = ContextMenuClientHelper.GetMenuTemplateNodeForCommand(contextMenuItem.CommandName);
      if (templateNodeForCommand != null)
        fromContextMenuItem1 = new MenuTemplateNode(contextMenuItem.CommandName, contextMenuItem.Text, templateNodeForCommand.ImageIndex, groupID, orderID, templateNodeForCommand.Shortcut, true, templateNodeForCommand.ImageListSource);
    }
    else
      fromContextMenuItem1 = new MenuTemplateNode(contextMenuItem.Text, -1, groupID, orderID);
    if (fromContextMenuItem1 != null)
    {
      foreach (ContextMenuItem contextMenuItem1 in (Collection<ContextMenuItem>) contextMenuItem.Items)
      {
        MenuTemplateNode fromContextMenuItem2 = ContextMenuClientHelper.CreateTemplateNodeFromContextMenuItem(contextMenuItem1);
        if (fromContextMenuItem2 != null)
          fromContextMenuItem1.Nodes.Add(fromContextMenuItem2);
      }
    }
    return fromContextMenuItem1;
  }
}
