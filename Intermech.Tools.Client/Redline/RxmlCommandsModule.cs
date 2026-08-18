// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.RxmlCommandsModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Redline;

internal sealed class RxmlCommandsModule : InitializerModule
{
  private static readonly string ViewCommandName = "ViewDocument";
  private IFactory navigatorFactory;
  private Lazy<RxmlCommandsProvider> commandsProvider;

  public RxmlCommandsModule(IFactory navigatorFactory, Lazy<RxmlCommandsProvider> commandsProvider)
  {
    this.navigatorFactory = navigatorFactory;
    this.commandsProvider = commandsProvider;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    MenuTemplateNode menuTemplateNode = this.navigatorFactory.ContextMenuTemplate[RxmlCommandsModule.ViewCommandName];
    if (menuTemplateNode == null)
      return;
    this.navigatorFactory.ContextMenuTemplate.BeginUpdate();
    try
    {
      this.navigatorFactory.ContextMenuTemplate.Nodes.Add(new MenuTemplateNode(RxmlCommandsProvider.ViewRxmlCommandName, "Смотреть файл замечаний", menuTemplateNode.ImageIndex, menuTemplateNode.GroupID, menuTemplateNode.OrderID + 1));
    }
    finally
    {
      this.navigatorFactory.ContextMenuTemplate.EndUpdate();
    }
    this.navigatorFactory.AddCommandsProvider(1, (ICommandsProvider) this.commandsProvider.Value);
  }

  protected override void DoShutdown()
  {
    if (this.commandsProvider.IsValueCreated)
      this.navigatorFactory.RemoveCommandsProvider(1, (ICommandsProvider) this.commandsProvider.Value);
    MenuTemplateNode node = this.navigatorFactory.ContextMenuTemplate[RxmlCommandsProvider.ViewRxmlCommandName];
    if (node != null)
      this.navigatorFactory.ContextMenuTemplate.Nodes.Remove(node);
    base.DoShutdown();
  }
}
