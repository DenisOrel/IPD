// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.Navigator.XmlExchangeCommandProvider
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.XmlExchange.Client.Navigator.Commands;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.XmlExchange.Client.Navigator;

/// <summary>
/// 
/// </summary>
internal class XmlExchangeCommandProvider : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="factory"></param>
  public XmlExchangeCommandProvider(IFactory factory)
  {
    MenuTemplate contextMenuTemplate = factory.ContextMenuTemplate;
    MenuTemplateNode menuTemplateNode = factory.ContextMenuTemplate["XmlExchange"];
    contextMenuTemplate.BeginUpdate();
    try
    {
      if (menuTemplateNode != null)
        return;
      INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
      int imageIndex1 = service != null ? service.ImageIndex("XML.imgBriefcase") : -1;
      int imageIndex2 = service != null ? service.ImageIndex("XML.imgBriefcaseExport") : -1;
      int imageIndex3 = service != null ? service.ImageIndex("XML.imgBriefcaseImport") : -1;
      MenuTemplateNode node = new MenuTemplateNode("XmlExchange", LocalizationHolder.rm.GetString("XmlExchange.Client_3"), imageIndex1, 90000, 10);
      contextMenuTemplate.Nodes.Add(node);
      node.Nodes.Add(new MenuTemplateNode("ExportData", LocalizationHolder.rm.GetString("XmlExchange.Client_8"), imageIndex2, 10, 20));
      node.Nodes.Add(new MenuTemplateNode("ImportData", LocalizationHolder.rm.GetString("XmlExchange.Client_15"), imageIndex3, 10, 20));
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
    if (items == null || viewServices == null || !(viewServices.GetService(typeof (IViewState)) is IViewState))
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("ImportData", new CommandInfo(4, new ClickEventHandler(XmlImportCommand.Execute)));
    if (items.Count == 0)
      return mergedCommands;
    bool flag = false;
    List<ObjInfoItem> objInfoList;
    if (XmlExportCommand.GetSelectedItemsInfo(items, out objInfoList, false) && objInfoList != null && objInfoList.Count != 0)
    {
      List<int> objectTypes = ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objInfoList);
      GenericListHelper.MakeUnique<int>(objectTypes);
      if (objectTypes.Count == 1)
        flag = true;
    }
    if (flag)
      mergedCommands.Add("ExportData", new CommandInfo(4, new ClickEventHandler(XmlExportCommand.Execute)));
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
}
