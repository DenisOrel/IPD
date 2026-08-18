// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Navigator.Commands.DocumentTechCardProvider
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Navigator.Commands;

internal class DocumentTechCardProvider : ICommandsProvider
{
  private DocumentTechCardProvider([NotNull] IFactory factory)
  {
    MenuTemplate contextMenuTemplate = factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode menuTemplateNode = factory.ContextMenuTemplate["Reports"];
      if (menuTemplateNode == null)
        return;
      INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
      menuTemplateNode.Nodes.Add(new MenuTemplateNode(DocumentTechCardCommandsEnum.MakeDocument.GetName<DocumentTechCardCommandsEnum>(), DocumentTechCardCommandsEnum.MakeDocument.GetDescription<DocumentTechCardCommandsEnum>(), service != null ? service.ImageIndex("imgReport") : -1, 20, 200));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0 || viewServices == null)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items.Count != 1)
      mergedCommands.Suppress(DocumentTechCardCommandsEnum.MakeDocument.ToString(), 4);
    else
      mergedCommands.Add(DocumentTechCardCommandsEnum.MakeDocument.ToString(), new CommandInfo(4, new ClickEventHandler(DocumentTechCardProvider.DocumentTechCardEditCommand)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  private static void DocumentTechCardEditCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    new DocumentTechCardCommands(DocumentTechCardCommandsEnum.MakeDocument).Execute(items, viewServices, additionalInfo);
  }

  public static void Register([NotNull] IFactory factory)
  {
    DocumentTechCardProvider provider = new DocumentTechCardProvider(factory);
    factory.AddCommandsProvider(1, TechCardConsts.ObjectTypes.TechProcEdinID, (ICommandsProvider) provider);
    factory.AddCommandsProvider(1, TechCardConsts.ObjectTypes.OperaciyaID, (ICommandsProvider) provider);
  }
}
