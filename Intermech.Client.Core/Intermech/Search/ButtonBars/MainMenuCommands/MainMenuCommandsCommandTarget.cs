
// Type: Intermech.Search.ButtonBars.MainMenuCommands.MainMenuCommandsCommandTarget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.ButtonBars.MainMenuCommands;

public sealed class MainMenuCommandsCommandTarget : ICommandTarget
{
  public bool Execute(ICommandState commandState)
  {
    MenuItemBase menuItemBase = commandState != null ? this.GetMenuItemForCommand(commandState.CommandName) : throw new ArgumentNullException(nameof (commandState));
    if (menuItemBase == null || !menuItemBase.Enabled)
      return false;
    menuItemBase.PerformClick();
    return true;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    MenuItemBase menuItemForCommand = this.GetMenuItemForCommand(commandState.CommandName);
    if (menuItemForCommand == null || !menuItemForCommand.Enabled)
      return false;
    commandState.Enabled = true;
    return true;
  }

  private MenuItemBase GetMenuItemForCommand(string commandName)
  {
    if (!string.IsNullOrEmpty(commandName) && commandName.StartsWith("MainMenu"))
    {
      string[] commandNameParts = commandName.Split('/');
      if (commandNameParts.Length > 1)
      {
        MenuItemBase menuItemForCommand = ServiceLocator.Get<IMainMenuService>().MenuBar.Items.Cast<MenuItemBase>().FirstOrDefault<MenuItemBase>((Func<MenuItemBase, bool>) (o => o.CommandName == commandNameParts[1] || o.Text == commandNameParts[1]));
        foreach (string str in ((IEnumerable<string>) commandNameParts).Skip<string>(2))
        {
          string commandNamePart = str;
          menuItemForCommand = menuItemForCommand.Items.Cast<MenuItemBase>().FirstOrDefault<MenuItemBase>((Func<MenuItemBase, bool>) (o => o.CommandName == commandNamePart || o.Text == commandNamePart));
        }
        return menuItemForCommand;
      }
    }
    return (MenuItemBase) null;
  }
}
