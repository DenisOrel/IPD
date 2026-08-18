// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Article.RootObjectContextCommandProvider
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
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Article;

/// <summary>
/// Класс реализующий команды контекстного меню для головных объектов, на которые могут быть созданы ТП, РМ, Заготовка
/// </summary>
internal class RootObjectContextCommandProvider : ICommandsProvider
{
  /// <summary>Constructor</summary>
  public RootObjectContextCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service1))
      return;
    ICategoryTypeIconService service2 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    MenuTemplate contextMenuTemplate = service1.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode orCreate1 = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techElemAddNode", LocalizationHolder.rm.GetString("TechCard.Client_430"), -1, 13, 10);
      orCreate1.ImageListSource = ImageListSource.CategoryImageList;
      orCreate1.ImageIndex = service2 != null ? service2.IndexOf(4, TechCardConsts.ObjectTypes.TechBaseObjectID) : -1;
      Keys shortcut;
      TechCardConsts.Caches.TechCardHotKeys.TryGetValue(TechCardConsts.ObjectTypes.TechProcBaseGUID, out shortcut);
      MenuTemplateNode orCreate2 = TcClientUtils.FindOrCreate(orCreate1.Nodes, "create_TechProc", LocalizationHolder.rm.GetString(sc_19374.ssp_techcard_19375()), -1, 100, 60, shortcut);
      orCreate2.ImageListSource = ImageListSource.CategoryImageList;
      orCreate2.ImageIndex = service2 != null ? service2.IndexOf(4, TechCardConsts.ObjectTypes.TechProcBaseID) : -1;
      TechCardConsts.Caches.TechCardHotKeys.TryGetValue(TechCardConsts.ObjectTypes.ZagotGUID, out shortcut);
      MenuTemplateNode orCreate3 = TcClientUtils.FindOrCreate(orCreate1.Nodes, "add" + (object) TechCardConsts.ObjectTypes.ZagotID, MetaDataHelper.GetObjectTypeName(TechCardConsts.ObjectTypes.ZagotID), -1, 100, 70, shortcut);
      orCreate3.ImageListSource = ImageListSource.CategoryImageList;
      orCreate3.ImageIndex = service2 != null ? service2.IndexOf(4, TechCardConsts.ObjectTypes.ZagotID) : -1;
      TechCardConsts.Caches.TechCardHotKeys.TryGetValue(TechCardConsts.ObjectTypes.CehRouteGUID, out shortcut);
      MenuTemplateNode orCreate4 = TcClientUtils.FindOrCreate(orCreate1.Nodes, "add" + (object) TechCardConsts.ObjectTypes.CehRouteID, MetaDataHelper.GetObjectTypeName(TechCardConsts.ObjectTypes.CehRouteID), -1, 100, 80 /*0x50*/, shortcut);
      orCreate4.ImageListSource = ImageListSource.CategoryImageList;
      orCreate4.ImageIndex = service2 != null ? service2.IndexOf(4, TechCardConsts.ObjectTypes.CehRouteID) : -1;
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
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null || items.Count != 1 || ((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    IMSApplicability applicability = items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? MetaDataHelper.GetApplicability(itemData.ObjectType, TechCardConsts.ObjectTypes.ProcRoutingID, TechCardConsts.RelTypes.TechRelationID) : (IMSApplicability) null;
    if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled)
    {
      mergedCommands.Add("create_TechProc", new CommandInfo(0, new ClickEventHandler(RootObjectContextCommandProvider.TechProcCreateCommand)));
      mergedCommands.Add("add" + (object) TechCardConsts.ObjectTypes.ZagotID, new CommandInfo(0, new ClickEventHandler(RootObjectContextCommandProvider.TechZagotCreateCommand)));
      mergedCommands.Add("add" + (object) TechCardConsts.ObjectTypes.CehRouteID, new CommandInfo(0, new ClickEventHandler(RootObjectContextCommandProvider.TechCehRouteCreateCommand)));
    }
    return mergedCommands;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Создание объекта указанного типа</summary>
  /// <param name="objTypeId">Ид. типа создаваемого объекта</param>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CreateTechObjectCommand(
    int objTypeId,
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (objTypeId == -1 || items == null || items.Count == 0)
      return;
    IObjectCreatorService service = ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, false);
    if (service == null || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID))
      return;
    long objectByTypeDialog = service.CreateObjectByTypeDialog(objTypeId, out OpenEditorMode _, (IObjectCreatorParams) new TechObjectCreatorParams(items, viewServices)
    {
      AsyncMode = true
    });
    if (Intermech.Consts.IsUndefinedObjectId(objectByTypeDialog))
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void TechProcCreateCommand(
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
    RootObjectContextCommandProvider.CreateTechObjectCommand(TechCardConsts.ObjectTypes.TechProcEdinID, items, viewServices, additionalInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void TechZagotCreateCommand(
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
    RootObjectContextCommandProvider.CreateTechObjectCommand(TechCardConsts.ObjectTypes.ZagotID, items, viewServices, additionalInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void TechCehRouteCreateCommand(
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
    RootObjectContextCommandProvider.CreateTechObjectCommand(TechCardConsts.ObjectTypes.CehRouteID, items, viewServices, additionalInfo);
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    factory.AddCommandsProvider(1, (ICommandsProvider) new RootObjectContextCommandProvider());
  }
}
