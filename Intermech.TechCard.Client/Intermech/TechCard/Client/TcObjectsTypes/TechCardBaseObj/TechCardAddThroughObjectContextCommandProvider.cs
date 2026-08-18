// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardAddThroughObjectContextCommandProvider
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
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>
/// Реализация провайдера для команды контекстного меню "Добавить объекты сквозного ТП"
/// </summary>
internal class TechCardAddThroughObjectContextCommandProvider : 
  TechCardBaseCompositionTypesCommandProvider
{
  /// <summary>Конструктор</summary>
  public TechCardAddThroughObjectContextCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service1))
      return;
    ICategoryTypeIconService service2 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    MenuTemplate contextMenuTemplate = service1.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      Keys shortcut = Keys.None;
      MenuTemplateNode orCreate = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "AddTechThroughObject", LocalizationHolder.rm.GetString("TechCard.Client_520"), -1, 13, 11, shortcut);
      orCreate.ImageListSource = ImageListSource.CategoryImageList;
      orCreate.ImageIndex = service2 != null ? service2.IndexOf(4, TechCardConsts.ObjectTypes.TechBaseObjectID) : -1;
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
    System.IServiceProvider viewServices)
  {
    ViewStateFlags viewStateFlags = !(viewServices.GetService(typeof (IViewState)) is IViewState service) ? ViewStateFlags.None : service.ViewState;
    if ((viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.None || !(viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView) || items == null || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    if (!(items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData))
      return CommandsInfo.Empty;
    int parObjTypeID = itemData.Value;
    List<int> visibleObjTypes = TechcardClientUtils.ObjectTypes.GetVisibleObjTypes();
    int throughtTpRelationId = TechCardConsts.RelTypes.TechThroughtTPRelationID;
    List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(parObjTypeID, throughtTpRelationId);
    bool flag = false;
    foreach (int parentTypeID in childObjectTypesId)
    {
      if (visibleObjTypes.BinarySearch(parentTypeID) >= 0)
      {
        flag = true;
        break;
      }
      if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID).Any<int>((Func<int, bool>) (subTypeId => visibleObjTypes.BinarySearch(subTypeId) >= 0)))
      {
        flag = true;
        break;
      }
    }
    if (flag)
      mergedCommands.Add("AddTechThroughObject", new CommandInfo(0, new ClickEventHandler(this.AddTechThroughObjectCommand)));
    else
      mergedCommands.Suppress("AddTechThroughObject", 0);
    return mergedCommands;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public override CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>команда добавления объктов в состав</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void AddTechThroughObjectCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    AddThroughObjectCommand throughObjectCommand = new AddThroughObjectCommand(true);
    throughObjectCommand.Init(items, viewServices, additionalInfo);
    throughObjectCommand.Execute();
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    TechCardAddThroughObjectContextCommandProvider provider = new TechCardAddThroughObjectContextCommandProvider();
    factory.AddCommandsProvider(1, (ICommandsProvider) provider);
  }
}
