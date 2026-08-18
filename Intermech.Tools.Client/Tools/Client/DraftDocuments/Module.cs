// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.DraftDocuments.Module
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Tools.Client.DraftDocuments;

internal sealed class Module : InitializerModule
{
  private IFactory navigatorFactory;
  private IDraftDocumentsService draftDocumentsService;
  private Func<CommandsProvider> commandsProviderFactory;

  public Module(
    IFactory navigatorFactory,
    IDraftDocumentsService draftDocumentsService,
    Func<CommandsProvider> commandsProviderFactory)
  {
    if (navigatorFactory == null)
      throw new ArgumentNullException(nameof (navigatorFactory));
    if (draftDocumentsService == null)
      throw new ArgumentNullException(nameof (draftDocumentsService));
    if (commandsProviderFactory == null)
      throw new ArgumentNullException(nameof (commandsProviderFactory));
    this.navigatorFactory = navigatorFactory;
    this.draftDocumentsService = draftDocumentsService;
    this.commandsProviderFactory = commandsProviderFactory;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.AddCommandItemsToContextMenuTemplate();
    this.AddCommandsProviderToNavigator();
  }

  protected override void DoShutdown() => base.DoShutdown();

  private void AddCommandItemsToContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ConvertToDocument", "Преобразовать в документ", -1, 15, 15));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private void AddCommandsProviderToNavigator()
  {
    this.navigatorFactory.AddCommandsProvider(1, this.draftDocumentsService.IdCache.DraftDocuments.Id, (ICommandsProvider) this.commandsProviderFactory());
  }
}
