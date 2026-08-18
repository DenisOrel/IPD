// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ArticleObjectType.ArticleContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ArticleObjectType;

/// <summary>Article context provider</summary>
internal class ArticleContextCommandProvider : ICommandsProvider
{
  /// <summary>Constructor</summary>
  public ArticleContextCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
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
    if (items == null || items.Count == 0)
      return CommandsInfo.Empty;
    if (viewServices.GetService(typeof (IViewState)) is IViewState service)
    {
      long viewState = (long) service.ViewState;
    }
    return new CommandsInfo();
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

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void ArticleAddToDesktopCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
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
    if (items == null || items.Count == 0 || !(items.GetItemData(sc_19371.ssp_techcard_19373(1736733047), typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long parObjId = 0;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
      if (items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData2)
      {
        if (childrenIdRecursive.Contains(parentData2.ObjectType))
          parObjId = parentData2.ObjectID;
      }
      else if (items.GetParentData(0, typeof (IDBObjectID)) is IDBObjectID parentData1)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(parentData1.Value);
        if (childrenIdRecursive.Contains(objectInfo.ObjectTypeID))
          parObjId = parentData1.Value;
      }
      Intermech.TechCard.Client.NotionObject.ArticleContextCommandProvider.AddObjectToDesktop(Intermech.TechCard.Client.NotionObject.ArticleContextCommandProvider.GetCurrentUserDesktopID(sessionKeeper.Session), 0L, parObjId, itemData.ObjectID);
    }
  }
}
