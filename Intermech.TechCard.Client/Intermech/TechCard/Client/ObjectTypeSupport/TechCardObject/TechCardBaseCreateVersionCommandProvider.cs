// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.TechCardBaseCreateVersionCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.HelperClasses.UIHelpers;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject;

/// <summary>
/// Провайдер команды "Создать версию" контекстного меню навигатора для технологических объектов
/// </summary>
internal class TechCardBaseCreateVersionCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  public TechCardBaseCreateVersionCommandProvider()
  {
    IFactory service = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false) == null)
      return CommandsInfo.Empty;
    IViewState service = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    ViewStateFlags viewStateFlags = service == null ? ViewStateFlags.None : service.ViewState;
    if ((viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.None)
      return CommandsInfo.Empty;
    items = ContextCommandHelper.GetCheckedItems(viewServices, items);
    if (items == null || items.Count == 0)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("CreateVersion", new CommandInfo(0, new ClickEventHandler(TechCardBaseCreateVersionCommandProvider.CreateVersionCommand)));
    return mergedCommands;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>команда создания версии объекта</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CreateVersionCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (viewServices != null)
    {
      INavigatorTreeViewContextMenuHelper service = ServiceUtils.GetService<INavigatorTreeViewContextMenuHelper>((object) viewServices, false);
      if (service != null)
        service.CanRestoreFocusedNode = false;
    }
    Intermech.TechCard.Client.Commands.CreateVersion.CreateVersionCommand createVersionCommand = new Intermech.TechCard.Client.Commands.CreateVersion.CreateVersionCommand();
    createVersionCommand.Init(items, viewServices, additionalInfo);
    createVersionCommand.Execute();
  }
}
