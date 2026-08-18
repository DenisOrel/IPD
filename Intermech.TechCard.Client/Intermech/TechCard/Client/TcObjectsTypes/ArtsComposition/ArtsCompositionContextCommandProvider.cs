// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.ArtsComposition.ArtsCompositionContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.Extensions;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.ArtsComposition;

/// <summary>
/// Провайдер команд навигатора для работы со сборочными ТП
/// </summary>
internal class ArtsCompositionContextCommandProvider : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  private static System.IServiceProvider _services;

  /// <summary>
  /// Проверка на допустимость команд для собираемого объекта
  /// </summary>
  /// <param name="commandsInfo"></param>
  /// <param name="items"></param>
  private static void AssemblingCommandsValidate(CommandsInfo commandsInfo, ISelectedItems items)
  {
    if (commandsInfo == null)
      return;
    bool flag = false;
    try
    {
      if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
        return;
      foreach (IMSApplicability typeApplicability in MetaDataHelper.GetObjectTypeApplicabilities(itemData.ObjectType))
      {
        if (typeApplicability != null && typeApplicability.RelationTypeID == TechCardConsts.RelTypes.TechRelationID && MetaDataHelper.IsObjectTypeChildOf(typeApplicability.ChildObjectTypeID, TechCardConsts.ObjectTypes.EdinicaSostavaID))
        {
          flag = true;
          break;
        }
      }
    }
    finally
    {
      if (!flag)
      {
        commandsInfo.Suppress("techAssemblingNode", 0);
        commandsInfo.Suppress("techAssemblingRootNode", 0);
        commandsInfo.Suppress("techAssemblingRootFullNode", 0);
        commandsInfo.Suppress("techAssemblingRootCompNode", 0);
        commandsInfo.Suppress("techAssemblingRootCompFullNode", 0);
        commandsInfo.Suppress("techAssemblingFromListNode", 0);
        commandsInfo.Suppress("techAssemblingFromListFullNode", 0);
      }
      else
      {
        commandsInfo.Add("techAssemblingRootNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AssemblingRootNode)));
        commandsInfo.Add("techAssemblingRootFullNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AssemblingRootFullNode)));
        commandsInfo.Add("techAssemblingRootCompNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AssemblingRootCompNode)));
        commandsInfo.Add("techAssemblingRootCompFullNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AssemblingRootCompFullNode)));
        commandsInfo.Add("techAssemblingFromListNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AssemblingFromListNode)));
        commandsInfo.Add("techAssemblingFromListFullNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AssemblingFromListFullNode)));
      }
    }
  }

  /// <summary>
  /// Проверка на допустимость команд для собираемого объекта
  /// </summary>
  /// <param name="commandsInfo"></param>
  /// <param name="items"></param>
  private static void AccessoryCommandsValidate(CommandsInfo commandsInfo, ISelectedItems items)
  {
    if (commandsInfo == null)
      return;
    bool flag = false;
    try
    {
      if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, TechCardConsts.ObjectTypes.SobirEdinicaID))
        return;
      foreach (IMSApplicability typeApplicability in MetaDataHelper.GetObjectTypeApplicabilities(itemData.ObjectType))
      {
        if (typeApplicability != null && typeApplicability.RelationTypeID == TechCardConsts.RelTypes.TechRelationID && MetaDataHelper.IsObjectTypeChildOf(typeApplicability.ChildObjectTypeID, TechCardConsts.ObjectTypes.KomlEdinicaID))
        {
          flag = true;
          break;
        }
      }
    }
    finally
    {
      if (!flag)
      {
        commandsInfo.Suppress("techAccessoryNode", 0);
        commandsInfo.Suppress("techAccessoryRootNode", 0);
        commandsInfo.Suppress("techAccessorySelectedNode", 0);
        commandsInfo.Suppress("techAccessoryFromListNode", 0);
        commandsInfo.Suppress("techAccessoryFromListOnly", 0);
      }
      else
      {
        commandsInfo.Add("techAccessoryRootNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AccessoryRootNode)));
        commandsInfo.Add("techAccessorySelectedNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AccessorySelectedNode)));
        commandsInfo.Add("techAccessoryFromListNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AccessoryFromListNode)));
        commandsInfo.Add("techAccessoryFromListOnly", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.Add_AccessoryFromListOnly)));
      }
    }
  }

  /// <summary>Проверка на допустимость команды изменения объекта</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  private static bool CanEditObject(ISelectedItems items, System.IServiceProvider viewServices)
  {
    IViewState service = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    return ((service != null ? (long) service.ViewState : 0L) & 2L) == 0L && items.Count == 1 && items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData && itemData.Value != 0L;
  }

  /// <summary>Реализация команды добавить головное</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AssemblingRootNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAssemblingRootNode), false);
  }

  /// <summary>Реализация команды добавить головное с составом</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AssemblingRootFullNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAssemblingRootNode), true);
  }

  /// <summary>Реализация команды добавить из состава головного</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AssemblingRootCompNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAssemblingRootCompNode), false);
  }

  /// <summary>
  /// Реализация команды добавить из состава головного c составом
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AssemblingRootCompFullNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAssemblingRootCompNode), true);
  }

  /// <summary>Реализация команды добавить из списка</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AssemblingFromListNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAssemblingFromListNode), false);
  }

  /// <summary>Реализация команды добавить из списка с составом</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AssemblingFromListFullNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAssemblingFromListNode), true);
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из состава головного
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AccessoryRootNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAccessoryRootNode), false);
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из состава выделенного
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AccessorySelectedNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAccessorySelectedNode), false);
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из состава произвольного объекта
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AccessoryFromListNode(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAccessoryFromListNode), false);
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из списка
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void Add_AccessoryFromListOnly(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAccessoryFromListOnly), false);
  }

  /// <summary>
  /// Реализация базовой команды добавления изделий в сборочный ТП
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  /// <param name="commandsMethod">Делегат команды</param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  private static void Command_AddBase(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo,
    ArtsCompositionContextCommandProvider.ContextCommandsHandler commandsMethod,
    bool needCompItems)
  {
    ArtsCompositionContextCommandProvider._services = viewServices;
    if (items == null || items.Count == 0 || viewServices == null || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    ArtsCompositionContextCommandProvider.Command_AddBase(itemData, viewServices, commandsMethod, needCompItems);
  }

  /// <summary>
  /// Реализация базовой команды добавления изделий в сборочный ТП
  /// </summary>
  /// <param name="selectedTypedObject">Объект ТП в который будет произведено добавление / изменение</param>
  /// <param name="viewServices"></param>
  /// <param name="commandsMethod">Делегат команды</param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  internal static void Command_AddBase(
    IDBTypedObjectID selectedTypedObject,
    System.IServiceProvider viewServices,
    ArtsCompositionContextCommandProvider.ContextCommandsHandler commandsMethod,
    bool needCompItems)
  {
    ArtsCompositionContextCommandProvider._services = viewServices;
    if (selectedTypedObject == null || viewServices == null)
      return;
    List<long> relationsCreated = new List<long>();
    List<long> objectsCreated = new List<long>();
    long assembTpObjId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!ArtsCompositionContextCommandProvider.IsObjectInAssemblingTp(selectedTypedObject.ObjectID, sessionKeeper.Session, out assembTpObjId))
        return;
    }
    if (commandsMethod != null && !commandsMethod(selectedTypedObject, assembTpObjId, needCompItems, ref relationsCreated, ref objectsCreated))
      return;
    ArtsCompositionContextCommandProvider.Command_Notify(objectsCreated, relationsCreated);
  }

  /// <summary>Уведомление навигатора о создании объектов / связей</summary>
  /// <param name="objCreated"></param>
  /// <param name="relCreated"></param>
  private static void Command_Notify(List<long> objCreated, List<long> relCreated)
  {
    ArtsCompositionContextCommandProvider.Command_Notify(objCreated, (List<long>) null, relCreated, (List<long>) null);
  }

  /// <summary>
  /// Уведомление навигатора о создании / удалении объектов / связей
  /// </summary>
  /// <param name="objCreated"></param>
  /// <param name="objRemoved"></param>
  /// <param name="relCreated"></param>
  /// <param name="relRemoved"></param>
  private static void Command_Notify(
    List<long> objCreated,
    List<long> objRemoved,
    List<long> relCreated,
    List<long> relRemoved)
  {
    bool flag1 = relCreated != null && relCreated.Count != 0;
    bool flag2 = relRemoved != null && relRemoved.Count != 0;
    bool flag3 = objCreated != null && objCreated.Count != 0;
    bool flag4 = objRemoved != null && objRemoved.Count != 0;
    if (!(flag1 | flag2 | flag3 | flag4))
      return;
    INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    if (flag2)
      service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relCreated));
    if (flag1)
      service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relCreated));
    if (flag4)
      service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objCreated));
    if (!flag3)
      return;
    service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) objCreated));
  }

  /// <summary>
  /// Реализация команды добавить головное / головное с составом
  /// </summary>
  /// <param name="selectedTypedObject"></param>
  /// <param name="techProccessObjectId"></param>
  /// <param name="needComposiontionItems">Признак добавления изделия с составом</param>
  /// <param name="relationsCreated"></param>
  /// <param name="objectsCreated"></param>
  /// <returns></returns>
  private static bool Command_AddAssemblingRootNode(
    IDBTypedObjectID selectedTypedObject,
    long techProccessObjectId,
    bool needComposiontionItems,
    ref List<long> relationsCreated,
    ref List<long> objectsCreated)
  {
    if (selectedTypedObject == null || techProccessObjectId == 0L || relationsCreated == null || objectsCreated == null)
      return false;
    List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      objectsCreated.Clear();
      relationsCreated.Clear();
      long artObjId;
      if (!ArtsCompositionContextCommandProvider.GetArticle4Tp(selectedTypedObject, techProccessObjectId, session, out artObjId))
        return false;
      List<ArtsCompositionsUtils.ArticleItemInfo> artItemList = new List<ArtsCompositionsUtils.ArticleItemInfo>()
      {
        new ArtsCompositionsUtils.ArticleItemInfo(artObjId)
      };
      if (!ArtsCompositionsUtils.AddAssemblingItems(selectedTypedObject, artItemList, needComposiontionItems, session, out objCreated))
        return false;
    }
    if (objCreated != null)
    {
      foreach (ArtsCompositionsUtils.ArticleCreatedItem articleCreatedItem in objCreated)
      {
        if (articleCreatedItem != null)
        {
          objectsCreated.Add(articleCreatedItem.TechObjID);
          relationsCreated.Add(articleCreatedItem.ProjLinkID);
        }
      }
    }
    return true;
  }

  /// <summary>Реализация команды добавить из состава головного</summary>
  /// <param name="selectedTypedObject"></param>
  /// <param name="techProcObjId"></param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  /// <param name="relCreated"></param>
  /// <param name="objCreated"></param>
  /// <returns></returns>
  public static bool Command_AddAssemblingRootCompNode(
    IDBTypedObjectID selectedTypedObject,
    long techProcObjId,
    bool needCompItems,
    ref List<long> relCreated,
    ref List<long> objCreated)
  {
    if (selectedTypedObject == null || techProcObjId == 0L || relCreated == null || objCreated == null)
      return false;
    objCreated.Clear();
    relCreated.Clear();
    long artObjId;
    string objectString;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!ArtsCompositionContextCommandProvider.GetArticle4Tp(selectedTypedObject, techProcObjId, session, out artObjId))
        return false;
      objectString = TechCardConsts.Utils.GetObjectString(artObjId, session);
    }
    ArtsCompositionForm.AddArticleMethod addCallBack = needCompItems ? new ArtsCompositionForm.AddArticleMethod(ArtsCompositionContextCommandProvider.Method_AddAssemblingFullNode) : new ArtsCompositionForm.AddArticleMethod(ArtsCompositionContextCommandProvider.Method_AddAssemblingNode);
    ArtsCompositionForm.ObjCreateParams objCreateParams = new ArtsCompositionForm.ObjCreateParams(selectedTypedObject, false, addCallBack);
    return ArtsCompositionForm.Execute(objectString, artObjId, techProcObjId, objCreateParams, ArtsCompositionContextCommandProvider._services) == DialogResult.OK;
  }

  /// <summary>
  /// Реализация команды добавить из списка / из списка с составом
  /// </summary>
  /// <param name="selectedTypedObject"></param>
  /// <param name="techProcObjId"></param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  /// <param name="relCreated"></param>
  /// <param name="objCreated"></param>
  /// <returns></returns>
  public static bool Command_AddAssemblingFromListNode(
    IDBTypedObjectID selectedTypedObject,
    long techProcObjId,
    bool needCompItems,
    ref List<long> relCreated,
    ref List<long> objCreated)
  {
    if (selectedTypedObject == null || techProcObjId == 0L || relCreated == null || objCreated == null)
      return false;
    List<long> longList = TechCardClientConst.SelectObjectsDlg(TechCardConsts.ObjectTypes.ArticleBaseGUID, "");
    if (longList == null || longList.Count == 0)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ArtsCompositionsUtils.ArticleItemInfo> artItemList = new List<ArtsCompositionsUtils.ArticleItemInfo>();
      foreach (long partArtId in longList)
        artItemList.Add(new ArtsCompositionsUtils.ArticleItemInfo(partArtId));
      List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated1;
      if (!ArtsCompositionsUtils.AddAssemblingItems(selectedTypedObject, artItemList, needCompItems, sessionKeeper.Session, out objCreated1))
        return false;
      if (objCreated1 != null)
      {
        foreach (ArtsCompositionsUtils.ArticleCreatedItem articleCreatedItem in objCreated1)
        {
          if (articleCreatedItem != null)
          {
            objCreated.Add(articleCreatedItem.TechObjID);
            relCreated.Add(articleCreatedItem.ProjLinkID);
          }
        }
      }
    }
    return true;
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из состава головного
  /// </summary>
  /// <param name="selectedTypedObject"></param>
  /// <param name="techProcObjId"></param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  /// <param name="relCreated"></param>
  /// <param name="objCreated"></param>
  /// <returns></returns>
  public static bool Command_AddAccessoryRootNode(
    IDBTypedObjectID selectedTypedObject,
    long techProcObjId,
    bool needCompItems,
    ref List<long> relCreated,
    ref List<long> objCreated)
  {
    if (selectedTypedObject == null || techProcObjId == 0L || relCreated == null || objCreated == null)
      return false;
    objCreated.Clear();
    relCreated.Clear();
    long artObjId;
    string objectString;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!ArtsCompositionContextCommandProvider.GetArticle4Tp(selectedTypedObject, techProcObjId, session, out artObjId))
        return false;
      objectString = TechCardConsts.Utils.GetObjectString(artObjId, session);
    }
    ArtsCompositionForm.AddArticleMethod addCallBack = new ArtsCompositionForm.AddArticleMethod(ArtsCompositionContextCommandProvider.Method_AddAccessoryNode);
    ArtsCompositionForm.ObjCreateParams objCreateParams = new ArtsCompositionForm.ObjCreateParams(selectedTypedObject, true, addCallBack);
    return ArtsCompositionForm.Execute(objectString, artObjId, techProcObjId, objCreateParams, ArtsCompositionContextCommandProvider._services) == DialogResult.OK;
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из состава выделенного
  /// </summary>
  /// <param name="selectedTypedObject"></param>
  /// <param name="techProcObjId"></param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  /// <param name="relCreated"></param>
  /// <param name="objCreated"></param>
  /// <returns></returns>
  public static bool Command_AddAccessorySelectedNode(
    IDBTypedObjectID selectedTypedObject,
    long techProcObjId,
    bool needCompItems,
    ref List<long> relCreated,
    ref List<long> objCreated)
  {
    if (selectedTypedObject == null || techProcObjId == 0L || relCreated == null || objCreated == null)
      return false;
    string frmCaption = string.Empty;
    long objectId1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      objCreated.Clear();
      relCreated.Clear();
      long objectId2 = selectedTypedObject.ObjectID;
      IDBObject dbObject = session.GetObject(objectId2, true);
      IDBAttribute attributeById = dbObject.GetAttributeByID(TechCardConsts.AttributeTypes.ObjectRefAttrID);
      if (attributeById == null || attributeById.Value == DBNull.Value || Convert.ToInt64(attributeById.Value) == 0L)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString(sc_19376.ssp_techcard_19377()), (object) dbObject.Caption, (object) selectedTypedObject.ObjectID));
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(Convert.ToInt64(attributeById.Value), false);
      if (objectActualCopy == null)
        return false;
      objectId1 = objectActualCopy.ObjectID;
      frmCaption = dbObject.Caption;
    }
    ArtsCompositionForm.AddArticleMethod addCallBack = new ArtsCompositionForm.AddArticleMethod(ArtsCompositionContextCommandProvider.Method_AddAccessoryNode);
    ArtsCompositionForm.ObjCreateParams objCreateParams = new ArtsCompositionForm.ObjCreateParams(selectedTypedObject, true, addCallBack);
    return ArtsCompositionForm.Execute(frmCaption, objectId1, techProcObjId, objCreateParams, ArtsCompositionContextCommandProvider._services) == DialogResult.OK;
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из состава произвольного объекта
  /// </summary>
  /// <param name="selectedTypedObject"></param>
  /// <param name="techProcObjId"></param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  /// <param name="relCreated"></param>
  /// <param name="objCreated"></param>
  /// <returns></returns>
  public static bool Command_AddAccessoryFromListNode(
    IDBTypedObjectID selectedTypedObject,
    long techProcObjId,
    bool needCompItems,
    ref List<long> relCreated,
    ref List<long> objCreated)
  {
    if (selectedTypedObject == null || techProcObjId == 0L || relCreated == null || objCreated == null)
      return false;
    objCreated.Clear();
    relCreated.Clear();
    List<long> longList = TechCardClientConst.SelectObjectsDlg(TechCardConsts.ObjectTypes.ArticleBaseGUID, "");
    if (longList == null || longList.Count == 0)
      return false;
    long num = longList[0];
    string objectString;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectString = TechCardConsts.Utils.GetObjectString(num, sessionKeeper.Session);
    ArtsCompositionForm.AddArticleMethod addCallBack = new ArtsCompositionForm.AddArticleMethod(ArtsCompositionContextCommandProvider.Method_AddAccessoryNode);
    ArtsCompositionForm.ObjCreateParams objCreateParams = new ArtsCompositionForm.ObjCreateParams(selectedTypedObject, true, addCallBack);
    return ArtsCompositionForm.Execute(objectString, num, techProcObjId, objCreateParams, ArtsCompositionContextCommandProvider._services) == DialogResult.OK;
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из списка
  /// </summary>
  /// <param name="selectedTypedObject"></param>
  /// <param name="techProcObjId"></param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  /// <param name="relCreated"></param>
  /// <param name="objCreated"></param>
  /// <returns></returns>
  public static bool Command_AddAccessoryFromListOnly(
    IDBTypedObjectID selectedTypedObject,
    long techProcObjId,
    bool needCompItems,
    ref List<long> relCreated,
    ref List<long> objCreated)
  {
    if (selectedTypedObject == null || techProcObjId == 0L || relCreated == null || objCreated == null)
      return false;
    string frmCaption = LocalizationHolder.rm.GetString("TechCard.Client_95");
    ArtsCompositionForm.AddArticleMethod addCallBack = new ArtsCompositionForm.AddArticleMethod(ArtsCompositionContextCommandProvider.Method_AddAccessoryNode);
    ArtsCompositionForm.ObjCreateParams objCreateParams1 = new ArtsCompositionForm.ObjCreateParams(selectedTypedObject, true, addCallBack);
    long techDbObjId = techProcObjId;
    ArtsCompositionForm.ObjCreateParams objCreateParams2 = objCreateParams1;
    System.IServiceProvider services = ArtsCompositionContextCommandProvider._services;
    return ArtsCompositionListForm.Execute(frmCaption, techDbObjId, objCreateParams2, services) == DialogResult.OK;
  }

  /// <summary>Реализация команды "Изменить объект"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void EditObjectCommand(
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
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num1));
    ArtsCompositionContextCommandProvider._services = viewServices;
    if (items == null || items.Count == 0 || viewServices == null || !ArtsCompositionContextCommandProvider.CanEditObject(items, viewServices) || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1) || !MetaDataHelper.IsObjectTypeChildOf(itemData1.ObjectType, TechCardConsts.ObjectTypes.KomlEdinicaID) || !(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData2))
      return;
    long assembTpObjId;
    long artObjId;
    string objectString;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!ArtsCompositionContextCommandProvider.IsObjectInAssemblingTp(itemData1.ObjectID, session, out assembTpObjId) || !ArtsCompositionContextCommandProvider.GetArticle4Tp(itemData1, assembTpObjId, session, out artObjId))
        return;
      objectString = TechCardConsts.Utils.GetObjectString(artObjId, session);
    }
    ArtsCompositionForm.AddArticleMethod addCallBack = new ArtsCompositionForm.AddArticleMethod(ArtsCompositionContextCommandProvider.Method_EditObject);
    ArtsCompositionForm.ObjCreateParams objCreateParams = new ArtsCompositionForm.ObjCreateParams((IDBTypedObjectID) new DBTypedObjectID(itemData1.ObjectType, itemData1.ObjectID, itemData1.ID, itemData1.Caption, itemData2.Value, itemData1.Version, itemData1.BaseVersion, itemData1.SiteID, itemData1.ModificationID), false, ArtsCompositionForm.ObjCreateParams.ObjCreateMode.Replace, addCallBack);
    int num2 = (int) ArtsCompositionForm.Execute(objectString, artObjId, assembTpObjId, objCreateParams, ArtsCompositionContextCommandProvider._services);
  }

  /// <summary>Реализация команды "Отчет по составу"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CompItemReportCommand(
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
    ArtsCompositionContextCommandProvider.Command_AddBase(items, viewServices, additionalInfo, new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_CompItemReport), false);
  }

  /// <summary>
  /// Реализация команды вставить в собираемый узел из состава произвольного объекта
  /// </summary>
  /// <param name="selectedTypedObject"></param>
  /// <param name="techProcObjId"></param>
  /// <param name="needCompItems">Признак добавления изделия с составом</param>
  /// <param name="relCreated"></param>
  /// <param name="objCreated"></param>
  /// <returns></returns>
  public static bool Command_CompItemReport(
    IDBTypedObjectID selectedTypedObject,
    long techProcObjId,
    bool needCompItems,
    ref List<long> relCreated,
    ref List<long> objCreated)
  {
    if (selectedTypedObject == null || techProcObjId == 0L || relCreated == null || objCreated == null)
      return false;
    objCreated.Clear();
    relCreated.Clear();
    long artObjId;
    string objectString;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!ArtsCompositionContextCommandProvider.GetArticle4Tp(selectedTypedObject, techProcObjId, session, out artObjId))
        return false;
      objectString = TechCardConsts.Utils.GetObjectString(artObjId, session);
    }
    return ArtsCompositionReportForm.Execute(objectString, artObjId, techProcObjId, ArtsCompositionContextCommandProvider._services) == DialogResult.OK;
  }

  /// <summary>Добавить собираемый узел</summary>
  /// <param name="projTechObj">Описание род. объекта ТП</param>
  /// <param name="artInfo">Описание изделия</param>
  /// <param name="objCreated">Список созданных объектов</param>
  /// <returns></returns>
  public static bool Method_AddAssemblingNode(
    IDBTypedObjectID projTechObj,
    ArtsCompositionsUtils.ArticleItemInfo artInfo,
    out List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated)
  {
    objCreated = (List<ArtsCompositionsUtils.ArticleCreatedItem>) null;
    if (artInfo == null || artInfo.PartArtID == 0L || projTechObj == null || projTechObj.ObjectID == 0L)
      return false;
    List<ArtsCompositionsUtils.ArticleItemInfo> artItemList = new List<ArtsCompositionsUtils.ArticleItemInfo>()
    {
      artInfo
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!ArtsCompositionsUtils.AddAssemblingItems(projTechObj, artItemList, false, sessionKeeper.Session, out objCreated))
        return false;
      if (objCreated != null)
      {
        List<long> objCreated1 = new List<long>(objCreated.Count);
        List<long> relCreated = new List<long>(objCreated.Count);
        foreach (ArtsCompositionsUtils.ArticleCreatedItem articleCreatedItem in objCreated)
        {
          objCreated1.Add(articleCreatedItem.TechObjID);
          relCreated.Add(articleCreatedItem.ProjLinkID);
        }
        ArtsCompositionContextCommandProvider.Command_Notify(objCreated1, relCreated);
      }
    }
    return true;
  }

  /// <summary>Добавить собираемый узел с составом</summary>
  /// <param name="projTechObj">Описание род. объекта ТП</param>
  /// <param name="artInfo">Описание изделия</param>
  /// <param name="objCreated">Список созданных объектов</param>
  /// <returns></returns>
  public static bool Method_AddAssemblingFullNode(
    IDBTypedObjectID projTechObj,
    ArtsCompositionsUtils.ArticleItemInfo artInfo,
    out List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated)
  {
    objCreated = (List<ArtsCompositionsUtils.ArticleCreatedItem>) null;
    if (artInfo == null || artInfo.PartArtID == 0L || projTechObj == null || projTechObj.ObjectID == 0L)
      return false;
    List<ArtsCompositionsUtils.ArticleItemInfo> artItemList = new List<ArtsCompositionsUtils.ArticleItemInfo>()
    {
      artInfo
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!ArtsCompositionsUtils.AddAssemblingItems(projTechObj, artItemList, true, sessionKeeper.Session, out objCreated))
        return false;
      if (objCreated != null)
      {
        List<long> objCreated1 = new List<long>(objCreated.Count);
        List<long> relCreated = new List<long>(objCreated.Count);
        foreach (ArtsCompositionsUtils.ArticleCreatedItem articleCreatedItem in objCreated)
        {
          objCreated1.Add(articleCreatedItem.TechObjID);
          relCreated.Add(articleCreatedItem.ProjLinkID);
        }
        ArtsCompositionContextCommandProvider.Command_Notify(objCreated1, relCreated);
      }
    }
    return true;
  }

  /// <summary>Добавить комплектующий узел</summary>
  /// <param name="projTechObj">Описание род. объекта ТП</param>
  /// <param name="artInfo">Описание изделия</param>
  /// <param name="objCreated">Список созданных объектов</param>
  /// <returns></returns>
  public static bool Method_AddAccessoryNode(
    IDBTypedObjectID projTechObj,
    ArtsCompositionsUtils.ArticleItemInfo artInfo,
    out List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated)
  {
    objCreated = new List<ArtsCompositionsUtils.ArticleCreatedItem>();
    if (artInfo == null || artInfo.PartArtID == 0L || projTechObj == null || projTechObj.ObjectID == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      TechcardClientUtils.StartCreateRelations(projTechObj.ObjectID, sessionKeeper.Session);
      ArtsCompositionsUtils.ArticleCreatedItem createdItem;
      try
      {
        if (!ArtsCompositionsUtils.AddAccessoryItems(projTechObj, artInfo, sessionKeeper.Session, out createdItem))
          return false;
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
      }
      if (createdItem != null)
      {
        objCreated.Add(createdItem);
        List<long> objCreated1 = new List<long>();
        List<long> relCreated = new List<long>();
        objCreated1.Add(createdItem.TechObjID);
        relCreated.Add(createdItem.ProjLinkID);
        ArtsCompositionContextCommandProvider.Command_Notify(objCreated1, relCreated);
      }
    }
    return true;
  }

  /// <summary>Добавить комплектующий узел</summary>
  /// <param name="projTechObj">Описание род. объекта ТП</param>
  /// <param name="artInfo">Описание изделия</param>
  /// <param name="objCreated">Список созданных объектов</param>
  /// <returns></returns>
  public static bool Method_EditObject(
    IDBTypedObjectID projTechObj,
    ArtsCompositionsUtils.ArticleItemInfo artInfo,
    out List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated)
  {
    objCreated = new List<ArtsCompositionsUtils.ArticleCreatedItem>();
    if (artInfo == null || artInfo.PartArtID == 0L || projTechObj == null || projTechObj.ObjectID == 0L || projTechObj.Owner == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      QuickObjectInfo objectInfo = session.GetObjectInfo(artInfo.PartArtID);
      List<int> attrIds;
      if (MetaDataHelper.IsObjectTypeChildOf(projTechObj.ObjectType, TechCardConsts.ObjectTypes.SobirEdinicaID))
      {
        ArtsCompositionsUtils.GetAssemblingObjAttributes(objectInfo.ObjectTypeID, out attrIds);
      }
      else
      {
        if (!MetaDataHelper.IsObjectTypeChildOf(projTechObj.ObjectType, TechCardConsts.ObjectTypes.KomlEdinicaID))
          return false;
        ArtsCompositionsUtils.GetAccessoryObjAttributes(objectInfo.ObjectTypeID, out attrIds);
      }
      IDBObject dbObject = session.GetObject(projTechObj.ObjectID, false);
      if (dbObject == null)
        return false;
      ArtsCompositionsUtils.CopyObjectAttributes(dbObject, artInfo, attrIds, (List<AttributeValues>) null, session);
      ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("ObjectsChanged", projTechObj.ObjectID));
    }
    return true;
  }

  /// <summary>Проверка - находиться ли объект в сборочном ТП</summary>
  /// <param name="objectId">Ид. версии объекта ТП</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="assembTpObjId">Ид. версии сборочного ТП</param>
  /// <returns></returns>
  private static bool IsObjectInAssemblingTp(
    long objectId,
    IUserSession session,
    out long assembTpObjId)
  {
    assembTpObjId = 0L;
    if (objectId == 0L || session == null)
      return false;
    List<long> longList = new List<long>();
    IDBObject dbObject = session.GetObject(objectId, false);
    if (dbObject != null)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, TechCardConsts.ObjectTypes.TechProcBaseID))
        longList.Add(objectId);
      else
        longList = TechCardUtils.GetParentTP(new List<long>()
        {
          objectId
        }, session, true);
    }
    switch (longList.Count)
    {
      case 0:
        string caption1 = LocalizationHolder.rm.GetString("TechCard.Client_138");
        int num1 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_19376.ssp_techcard_19378()), (object) TechCardConsts.Utils.GetObjectString(objectId, session), (object) objectId), caption1, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return false;
      case 1:
        assembTpObjId = longList[0];
        return true;
      default:
        string caption2 = LocalizationHolder.rm.GetString(sc_19376.ssp_techcard_19379());
        int num2 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_386"), (object) TechCardConsts.Utils.GetObjectString(objectId, session), (object) objectId), caption2, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return false;
    }
  }

  /// <summary>Получение изделия на техпроцесс</summary>
  /// <param name="selectedTypedObject">Описание текущего объекта</param>
  /// <param name="techProcObjId">Ид. версии техпроцесса</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="artObjId">Ид. версии изделия</param>
  /// <returns></returns>
  private static bool GetArticle4Tp(
    IDBTypedObjectID selectedTypedObject,
    long techProcObjId,
    IUserSession session,
    out long artObjId)
  {
    artObjId = 0L;
    HashSet<int> articleObjectTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes).ToHashSet<int>();
    IEnumerable<RelObjInfoItem> relObjInfoItems;
    if (TechcardClientControlsUtils.GetItemsApplicabilityInfo(ObjectExtensions.GetItems(selectedTypedObject.ObjectID), (System.IServiceProvider) ApplicationServices.Container, out relObjInfoItems))
    {
      RelObjInfoItem relObjInfoItem = relObjInfoItems.FirstOrDefault<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.PartInfo != (TypedInfoItem) null && articleObjectTypes.Contains(item.PartInfo.ObjTypeID)));
      if ((TypedInfoItem) relObjInfoItem != (TypedInfoItem) null)
      {
        artObjId = relObjInfoItem.PartInfo.ObjectID;
        return true;
      }
    }
    List<long> parentObjects = TechCardUtils.GetParentObjects(new List<long>()
    {
      techProcObjId
    }, session, articleObjectTypes.ToList<int>(), true);
    if (parentObjects == null || parentObjects.Count == 0)
    {
      string caption = LocalizationHolder.rm.GetString(sc_19376.ssp_techcard_19380());
      int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_389"), (object) TechCardConsts.Utils.GetObjectString(techProcObjId, session), (object) techProcObjId), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    artObjId = parentObjects[0];
    return true;
  }

  /// <summary>Конструктор</summary>
  public ArtsCompositionContextCommandProvider()
  {
    IFactory service = ServiceUtils.GetService<IFactory>((object) TechCardClient.ServiceProvider, false);
    if (service == null)
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode orCreate1 = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techAssemblingNode", LocalizationHolder.rm.GetString("TechCard.Client_375"), -1, 13, 110);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "techAssemblingRootNode", LocalizationHolder.rm.GetString("TechCard.Client_376"), -1, 100, 100);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "techAssemblingRootFullNode", LocalizationHolder.rm.GetString("TechCard.Client_377"), -1, 100, 200);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "techAssemblingRootCompNode", LocalizationHolder.rm.GetString("TechCard.Client_378"), -1, 100, 300);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "techAssemblingRootCompFullNode", LocalizationHolder.rm.GetString("TechCard.techAssemblingRootCompFullNode"), -1, 100, 400);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "techAssemblingFromListNode", LocalizationHolder.rm.GetString("TechCard.Client_379"), -1, 100, 500);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "techAssemblingFromListFullNode", LocalizationHolder.rm.GetString("TechCard.Client_380"), -1, 100, 600);
      MenuTemplateNode orCreate2 = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techAccessoryNode", LocalizationHolder.rm.GetString("TechCard.Client_381"), -1, 13, 120);
      TcClientUtils.FindOrCreate(orCreate2.Nodes, "techAccessoryRootNode", LocalizationHolder.rm.GetString("TechCard.Client_382"), -1, 100, 100);
      TcClientUtils.FindOrCreate(orCreate2.Nodes, "techAccessorySelectedNode", LocalizationHolder.rm.GetString("TechCard.Client_383"), -1, 100, 200);
      TcClientUtils.FindOrCreate(orCreate2.Nodes, "techAccessoryFromListNode", LocalizationHolder.rm.GetString("TechCard.Client_384"), -1, 100, 300);
      TcClientUtils.FindOrCreate(orCreate2.Nodes, "techAccessoryFromListOnly", LocalizationHolder.rm.GetString("TechCard.Client_379"), -1, 100, 400);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techComposItemReportNode", LocalizationHolder.rm.GetString("TechCard.Client_397"), -1, 13, 130);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "editObjectNode", LocalizationHolder.rm.GetString("TechCard.Client_239"), -1, 13, 30);
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
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo commandsInfo = new CommandsInfo();
    ArtsCompositionContextCommandProvider.AssemblingCommandsValidate(commandsInfo, items);
    ArtsCompositionContextCommandProvider.AccessoryCommandsValidate(commandsInfo, items);
    IDBTypedObjectID itemData = items.GetItemData<IDBTypedObjectID>(0, false);
    if (itemData != null && MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, TechCardConsts.ObjectTypes.EdinicaSostavaID))
    {
      commandsInfo.Add("editObjectNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.EditObjectCommand)));
      commandsInfo.Add("techComposItemReportNode", new CommandInfo(0, new ClickEventHandler(ArtsCompositionContextCommandProvider.CompItemReportCommand)));
    }
    return commandsInfo;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    new ArtsCompositionContextCommandProvider().RegisterForAllBaseTypes(factory);
  }

  /// <summary>Делегат для команд контекстных меню</summary>
  /// <param name="selectedTypedObject">Описание текущего объекта</param>
  /// <param name="techProcessObjectId">Ид. версии техпроцесса</param>
  /// <param name="needCompositionItems">Признак добавления изделия с составом</param>
  /// <param name="relationsCreated">Список созданных связей</param>
  /// <param name="objectsCreated">Список созданных объектов</param>
  /// <returns></returns>
  internal delegate bool ContextCommandsHandler(
    IDBTypedObjectID selectedTypedObject,
    long techProcessObjectId,
    bool needCompositionItems,
    ref List<long> relationsCreated,
    ref List<long> objectsCreated);
}
