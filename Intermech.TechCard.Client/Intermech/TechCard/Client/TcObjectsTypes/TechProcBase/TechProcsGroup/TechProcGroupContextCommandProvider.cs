// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.TechProcGroupContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcsGroup;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>
/// Класс реализующий команды контекстного меню для объектов типа "ГТП/ТТП"
/// </summary>
public class TechProcGroupContextCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  public TechProcGroupContextCommandProvider()
  {
    IFactory service = ServiceUtils.GetService<IFactory>((object) TechCardClient.ServiceProvider, false);
    if (service == null)
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "TechProcObjLinkToDCE", LocalizationHolder.rm.GetString("TechCard.Client_285"), -1, 10, 94);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "ApplyGroupAttributes", LocalizationHolder.rm.GetString("TechCard.Client_546"), -1, 10, 95);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    IViewState service = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    ViewStateFlags viewStateFlags = service != null ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.ReadOnly) != ViewStateFlags.None || items == null || items.Count != 1)
      return CommandsInfo.Empty;
    NodeIDPath nodeIdPath = (viewStateFlags & ViewStateFlags.NodeInTree) != ViewStateFlags.None ? new NodeIDPath(items.GetParentPath(0), items.GetItemID(0)) : (NodeIDPath) null;
    if (nodeIdPath == null || nodeIdPath.Length == 0)
      return CommandsInfo.Empty;
    bool flag = false;
    for (int Index = nodeIdPath.Length - 1; Index >= 0 && nodeIdPath[Index] is NodeID nodeId && (nodeId.RelationTypeID == TechCardConsts.RelTypes.TechRelationID || nodeId.RelationTypeID == -1); --Index)
    {
      int objectTypeId = nodeId.ObjectTypeID;
      if (MetaDataHelper.IsObjectTypeChildOf(objectTypeId, TechCardConsts.ObjectTypes.TechProcGroupID) || MetaDataHelper.IsObjectTypeChildOf(objectTypeId, TechCardConsts.ObjectTypes.TechProcTipovID))
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("TechProcObjLinkToDCE", new CommandInfo(0, new ClickEventHandler(TechProcGroupContextCommandProvider.TechProcObjLinkToDCE)));
    mergedCommands.Add("ApplyGroupAttributes", new CommandInfo(0, new ClickEventHandler(TechProcGroupContextCommandProvider.ApplyGroupAttributes)));
    return mergedCommands;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Реализация команды "Режим привязки"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void TechProcObjLinkToDCE(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (!(viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service2) || service2.SelectedNodes.Length != 1 || items == null || items.Count != sc_19680.ssp_techcard_19681(303958687) || !(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData))
      return;
    new TechProcGroupLinkObj2ArtDialog().ShowDialog(service2, new RelInfoItem(itemData.Value, itemData.RelationType));
  }

  private static void ApplyGroupAttributes(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (!(viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service2) || service2.SelectedNodes.Length != 1 || items == null || items.Count != sc_19680.ssp_techcard_19682(1251833874))
      return;
    ApplyGroupAttributesBaseCommand attributesBaseCommand = (items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData ? (itemData.ProjID != 0L ? 1 : 0) : 1) != 0 ? (ApplyGroupAttributesBaseCommand) new ApplyGroupAttributesTechProcCompositionCommand(nameof (ApplyGroupAttributes)) : (ApplyGroupAttributesBaseCommand) new ApplyGroupAttributesFromObjectCommand(nameof (ApplyGroupAttributes));
    attributesBaseCommand.Init(items, viewServices, additionalInfo);
    attributesBaseCommand.Execute();
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    TechProcGroupContextCommandProvider provider = new TechProcGroupContextCommandProvider();
    factory.AddCommandsProvider(1, (ICommandsProvider) provider);
  }
}
