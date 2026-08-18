// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.WeldingJoints.MenuModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Tools.Client.WeldingJoints;

internal sealed class MenuModule : InitializerModule
{
  private IFactory navigatorFactory;
  private Func<MenuCommandsProvider> commandsProviderFactory;
  private MenuTemplateNode updateWeldingSeamsCommandNode;

  public MenuModule(IFactory navigatorFactory, Func<MenuCommandsProvider> commandsProviderFactory)
  {
    this.navigatorFactory = navigatorFactory;
    this.commandsProviderFactory = commandsProviderFactory;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.AddCommandItemsToContextMenuTemplate();
    this.AddCommandsProviderToNavigator();
  }

  private void AddCommandItemsToContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode menuTemplateNode = contextMenuTemplate[Intermech.Tools.Client.IntegratorsContextMenu.MenuConsts.IntegratorsMenuName];
      if (menuTemplateNode == null)
        return;
      this.updateWeldingSeamsCommandNode = new MenuTemplateNode(MenuConsts.UpdateWeldingSeamsCommandName, MenuConsts.UpdateWeldingSeamsDisplayName, -1, 27, 30);
      menuTemplateNode.Nodes.Add(this.updateWeldingSeamsCommandNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private void AddCommandsProviderToNavigator()
  {
    this.navigatorFactory.AddCommandsProvider(1, (ICommandsProvider) this.commandsProviderFactory());
  }

  protected override void DoShutdown()
  {
    this.RemoveCommandItemsFromContextMenuTemplate();
    base.DoShutdown();
  }

  private void RemoveCommandItemsFromContextMenuTemplate()
  {
    if (this.updateWeldingSeamsCommandNode == null)
      return;
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate[Intermech.Tools.Client.IntegratorsContextMenu.MenuConsts.IntegratorsMenuName]?.Nodes.Remove(this.updateWeldingSeamsCommandNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    this.updateWeldingSeamsCommandNode = (MenuTemplateNode) null;
  }
}
