// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardBaseNumerateCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Extensions;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>
/// Реализация провайдера для команды контекстного меню "Перенумеровать"
/// </summary>
internal class TechCardBaseNumerateCommandProvider : TechCardBaseCompositionTypesCommandProvider
{
  /// <summary>Конструктор</summary>
  public TechCardBaseNumerateCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service1))
      return;
    MenuTemplate contextMenuTemplate = service1.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      INamedImageList service2 = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
      MenuTemplateNode orCreate1 = TcClientUtils.FindOrCreate(TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "numTemplateNode", LocalizationHolder.rm.GetString("TechCard.Client_240"), service2 != null ? service2.ImageIndex("imgNumerate") : -1, 13, 50).Nodes, "numObjectInCompositionTemplateNode", LocalizationHolder.rm.GetString("TechCard.Client_242"), service2 != null ? service2.ImageIndex("imgNumerateComposition") : -1, 100, 200);
      ICategoryTypeIconService service3 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
      List<IMSObjectType> possibleTypes4Command = this.GetAllPossibleTypes4Command(new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      });
      for (int index = 0; index < possibleTypes4Command.Count; ++index)
      {
        IMSObjectType imsObjectType = possibleTypes4Command[index];
        if (imsObjectType == null || imsObjectType.VersionsMode == ObjectVersionModes.Abstract)
          break;
        int num = service3 != null ? service3.IndexOf(4, imsObjectType.ObjectTypeID) : -1;
        MenuTemplateNode orCreate2 = TcClientUtils.FindOrCreate(orCreate1.Nodes, "num" + (object) imsObjectType.ObjectTypeID, imsObjectType.ObjectTypeName, -1, 100, index * 100);
        orCreate2.ImageListSource = ImageListSource.CategoryImageList;
        orCreate2.ImageIndex = num;
      }
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
  public override CommandsInfo GetMergedCommands(
    ISelectedItems items,
    IServiceProvider viewServices)
  {
    ViewStateFlags viewStateFlags = !(viewServices.GetService(typeof (IViewState)) is IViewState service) ? ViewStateFlags.None : service.ViewState;
    if ((viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.None || items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID))
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    NavigatorTreeNode itemData = items.GetItemData<NavigatorTreeNode>(0, false);
    if (itemData == null)
      return CommandsInfo.Empty;
    List<int> intList = new List<int>();
    if (itemData.Full)
    {
      List<int> list = new List<int>(itemData.Children.Count);
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) itemData.Children)
      {
        if (child.NodeID is NodeID nodeId && nodeId.RelationTypeID == TechCardConsts.RelTypes.TechRelationID)
          list.Add(nodeId.ObjectTypeID);
      }
      GenericListHelper.MakeUnique<int>(list);
      intList = list;
    }
    else
      intList.Clear();
    foreach (int additionalInfo in intList)
      mergedCommands.Add("num" + (object) additionalInfo, new CommandInfo(0, new ClickEventHandler(TechCardBaseNumerateCommandProvider.NumerateObjectInCompositionCommand), (object) additionalInfo));
    return mergedCommands;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public override CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    new TechCardBaseNumerateCommandProvider().RegisterForAllBaseTypes(factory);
  }

  /// <summary>Реализация команды Перенумеровать/Объекты в составе</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void NumerateObjectInCompositionCommand(
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
    int result;
    if (items == null || items.Count != 1 || viewServices == null || additionalInfo == null || !int.TryParse(additionalInfo.ToString(), out result) || result == -1 || !(viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service2) || service2.SelectedItems.Count != 1)
      return;
    NavigatorTreeNode selectedNode = service2.SelectedNodes[0];
    if (selectedNode == null || !selectedNode.InTree || selectedNode.Children.Count == 0 || !TechcardClientControlsUtils.IsSelectedItemsFromTree(items, service2))
      return;
    NavigatorTreeNode objectNode = (NavigatorTreeNode) null;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) selectedNode.Children)
    {
      IDBTypedObjectID dbTypedObjectId;
      if (child != null && TechcardClientControlsUtils.GetObjectInfo(child, out dbTypedObjectId, out IDBRelationID _, false) && dbTypedObjectId != null && dbTypedObjectId.ObjectType == result)
      {
        objectNode = child;
        break;
      }
    }
    if (objectNode == null)
      return;
    TechcardBaseObjectCommandUtils.NumerateCommand(objectNode, true, service2);
  }
}
