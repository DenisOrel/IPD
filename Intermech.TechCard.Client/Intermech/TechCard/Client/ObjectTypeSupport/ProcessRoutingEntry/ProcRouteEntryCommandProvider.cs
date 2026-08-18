// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.ProcRouteEntryCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;

internal class ProcRouteEntryCommandProvider : ICommandsProvider
{
  private const string addCommandName = "AddCurrentAssemblyEntry";
  private const string removeCommandName = "RemoveCurrentAssemblyEntry";

  /// <summary>Конструктор</summary>
  public ProcRouteEntryCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "AddCurrentAssemblyEntry", LocalizationHolder.rm.GetString("TechCard.AddCurrentAssemblyEntry"), -1, 13, 87);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "RemoveCurrentAssemblyEntry", LocalizationHolder.rm.GetString("TechCard.RemoveCurrentAssemblyEntry"), -1, 13, 88);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items.Count != 1 || ((viewServices.GetService(typeof (IViewState)) is IViewState service1 ? (long) service1.ViewState : 0L) & 2L) != 0L || !(viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service2))
      return mergedCommands;
    IEnumerable<RelObjInfoItem> source = new TechRelObjInfoItemsFromSelectedItemApplicabilityProvider(items, service2.Services).Execute();
    if (source == null || !source.Any<RelObjInfoItem>())
      return mergedCommands;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new int[2]
    {
      MRP2Consts.objtypeIdProductionObjects,
      MetaDataHelper.GetObjectTypeID("cad00583-306c-11d8-b4e9-00304f19f545")
    });
    ObjInfoItem childArticle = source.FirstOrDefault<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (a => a.PartInfo.ObjTypeID == TechCardConsts.ObjectTypes.ProcRoutingID))?.ProjInfo;
    if ((TypedInfoItem) childArticle == (TypedInfoItem) null || childArticle.ObjectID == 0L || childrenIdRecursive.Contains(childArticle.ObjTypeID))
      return mergedCommands;
    ObjInfoItem projInfo = source.FirstOrDefault<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (a => (TypedInfoItem) a.PartInfo == (TypedInfoItem) childArticle))?.ProjInfo;
    if ((TypedInfoItem) projInfo == (TypedInfoItem) null || projInfo.ObjectID == 0L || childrenIdRecursive.Contains(projInfo.ObjTypeID))
      return mergedCommands;
    mergedCommands.Add("AddCurrentAssemblyEntry", new CommandInfo(0, new ClickEventHandler(this.AddCurrentAssemblyEntry)));
    mergedCommands.Add("RemoveCurrentAssemblyEntry", new CommandInfo(0, new ClickEventHandler(this.RemoveCurrentAssemblyEntry)));
    return mergedCommands;
  }

  /// <summary>Добавить текущую входимость</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void AddCurrentAssemblyEntry(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    EditCurrentAssemblyEntryCommand assemblyEntryCommand = new EditCurrentAssemblyEntryCommand(EditCurrentAssemblyEntryMode.Add, nameof (AddCurrentAssemblyEntry));
    assemblyEntryCommand.Init(items, viewServices, additionalInfo);
    assemblyEntryCommand.Execute();
  }

  /// <summary>Исключить текущую входимость</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void RemoveCurrentAssemblyEntry(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    EditCurrentAssemblyEntryCommand assemblyEntryCommand = new EditCurrentAssemblyEntryCommand(EditCurrentAssemblyEntryMode.Remove, nameof (RemoveCurrentAssemblyEntry));
    assemblyEntryCommand.Init(items, viewServices, additionalInfo);
    assemblyEntryCommand.Execute();
  }
}
