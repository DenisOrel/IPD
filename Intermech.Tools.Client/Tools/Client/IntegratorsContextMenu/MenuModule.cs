// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IntegratorsContextMenu.MenuModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Tools.Client.IntegratorsContextMenu;

internal sealed class MenuModule : InitializerModule
{
  private IFactory navigatorFactory;
  private MenuTemplateNode menuNode;

  public MenuModule(IFactory navigatorFactory) => this.navigatorFactory = navigatorFactory;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.AddMenuToContextMenuTemplate();
  }

  private void AddMenuToContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      this.menuNode = new MenuTemplateNode(MenuConsts.IntegratorsMenuName, MenuConsts.IntegratorsMenuDisplayName, -1, 24, 30);
      contextMenuTemplate.Nodes.Add(this.menuNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  protected override void DoShutdown()
  {
    this.RemoveMenuFromContextMenuTemplate();
    base.DoShutdown();
  }

  private void RemoveMenuFromContextMenuTemplate()
  {
    if (this.menuNode == null)
      return;
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Remove(this.menuNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    this.menuNode = (MenuTemplateNode) null;
  }
}
