// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.CommandList
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

public class CommandList : Dictionary<string, CommandList.CommandInfo>
{
  [NotNull]
  public CommandList.CommandInfo AddCommand(
    [NotNull, NotEmpty] string commandName,
    [NotNull] string text,
    int imageIndex,
    bool beginGroup)
  {
    CommandList.CommandInfo commandInfo = new CommandList.CommandInfo();
    commandInfo.Text = text;
    commandInfo.CommandName = commandName;
    commandInfo.ImageIndex = imageIndex;
    commandInfo.BeginGroup = beginGroup;
    if (commandInfo.Text == string.Empty)
    {
      ICommandState command = Intermech.Client.Services.CommandManager.FindCommand(commandInfo.CommandName);
      if (command != null)
      {
        commandInfo.Text = (command.Text != string.Empty ? command.Text : command.ToolTipText) ?? string.Empty;
        commandInfo.ImageIndex = command.ImageIndex;
      }
    }
    this.Add(commandName.ToLower(), commandInfo);
    return commandInfo;
  }

  [NotNull]
  public CommandList.CommandInfo AddCommand([NotNull, NotEmpty] string commandName, [NotNull] string text, int imageIndex)
  {
    return this.AddCommand(commandName, text, imageIndex, false);
  }

  [NotNull]
  public CommandList.CommandInfo AddCommand([NotNull, NotEmpty] string commandName, [NotNull] string text)
  {
    return this.AddCommand(commandName, text, -1);
  }

  [NotNull]
  public CommandList.CommandInfo AddCommand([NotNull, NotEmpty] string commandName, Shortcut shortcut)
  {
    CommandList.CommandInfo commandInfo = this.AddCommand(commandName, string.Empty);
    commandInfo.Shortcut = shortcut;
    return commandInfo;
  }

  [NotNull]
  public CommandList.CommandInfo AddCommand([NotNull, NotEmpty] string commandName)
  {
    return this.AddCommand(commandName, string.Empty);
  }

  internal void AddToToolbar([NotNull] Intermech.Bars.ToolBar toolBar, [NotNull, ItemNotNull, ItemNotEmpty] IReadOnlyCollection<string> items)
  {
    toolBar.ImageList = Intermech.Client.Services.NamedList.ImageList;
    foreach (string str1 in (IEnumerable<string>) items)
    {
      CommandList.CommandInfo commandInfo;
      this.TryGetValue(str1.ToLower(), out commandInfo);
      if (commandInfo != null)
      {
        ButtonItem buttonItem = new ButtonItem();
        buttonItem.Text = commandInfo.Text;
        string str2 = commandInfo.Text.Replace("&", string.Empty);
        buttonItem.ToolTipText = str2;
        buttonItem.CommandName = commandInfo.CommandName;
        buttonItem.BeginGroup = commandInfo.BeginGroup;
        buttonItem.ImageIndex = commandInfo.ImageIndex;
        toolBar.Items.Add((ToolbarItemBase) buttonItem);
        Intermech.Client.Services.CommandManager.Add((ButtonItemBase) buttonItem);
      }
    }
  }

  internal void AddToMenu([NotNull] MenuItemBase mi, [NotNull, ItemNotNull, ItemNotEmpty] IReadOnlyCollection<string> items)
  {
    foreach (string cmd in (IEnumerable<string>) items)
      this.AddToMenu(mi, cmd);
  }

  [CanBeNull]
  internal MenuButtonItem AddToMenu([NotNull] MenuItemBase mi, [NotNull, NotEmpty] string cmd, [CanBeNull, NotEmpty] string insertAfterCommandName = null)
  {
    string str = insertAfterCommandName;
    CommandList.CommandInfo commandInfo;
    this.TryGetValue(cmd.ToLower(), out commandInfo);
    if (commandInfo == null)
      return (MenuButtonItem) null;
    MenuButtonItem menu = new MenuButtonItem();
    menu.Text = commandInfo.Text;
    menu.ToolTipText = commandInfo.Text;
    menu.CommandName = commandInfo.CommandName;
    menu.BeginGroup = commandInfo.BeginGroup;
    menu.ImageIndex = commandInfo.ImageIndex;
    menu.Shortcut = commandInfo.Shortcut;
    if (insertAfterCommandName == null)
    {
      mi.Items.Add((ToolbarItemBase) menu);
    }
    else
    {
      int num = mi.Items.IndexOfFirst((Predicate<object>) (menuItem => menuItem is MenuItemBase menuItemBase && string.Equals(menuItemBase.CommandName, insertAfterCommandName, StringComparison.InvariantCultureIgnoreCase)));
      if (num < 0)
        throw new Exception($"Command \"{insertAfterCommandName}\" not found in \"{mi}\" menu");
      mi.Items.Insert(num + 1, (ToolbarItemBase) menu);
    }
    Intermech.Client.Services.CommandManager.Add((ButtonItemBase) menu);
    return menu;
  }

  public class CommandInfo
  {
    [NotNull]
    public string Text = string.Empty;
    [NotNull]
    public string CommandName = string.Empty;
    public int ImageIndex = -1;
    public bool BeginGroup;
    public Shortcut Shortcut;
  }
}
