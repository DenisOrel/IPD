// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// Провайдер команд контекстного меню для работы с параметрами моделей (интеграция с Cadmech 3D)
/// </summary>
internal class Cadmech3DCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  private Cadmech3DCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode orCreate = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "cadmech3D_RootTemplate", LocalizationHolder.rm.GetString("TechCard.Client_482"), -1, 13, 1000);
      TcClientUtils.FindOrCreate(orCreate.Nodes, "cadmech3D_AddModelFromArticle", LocalizationHolder.rm.GetString("TechCard.Client_483"), -1, 100, 100);
      TcClientUtils.FindOrCreate(orCreate.Nodes, "cadmech3D_AddModelFromList", LocalizationHolder.rm.GetString("TechCard.Client_484"), -1, 100, 200);
      TcClientUtils.FindOrCreate(orCreate.Nodes, "cadmech3D_SelectTypeElems", LocalizationHolder.rm.GetString("TechCard.Client_485"), -1, 200, 100);
      TcClientUtils.FindOrCreate(orCreate.Nodes, "cadmech3D_AddSurfaceFromModel", LocalizationHolder.rm.GetString("TechCard.Client_486"), -1, 200, 200);
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
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L || items == null || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("cadmech3D_AddModelFromArticle", new CommandInfo(0, new ClickEventHandler(Cadmech3DCommandProvider.AddModelFromArticleCommand)));
    mergedCommands.Add("cadmech3D_AddModelFromList", new CommandInfo(0, new ClickEventHandler(Cadmech3DCommandProvider.AddModelFromListCommand)));
    mergedCommands.Add("cadmech3D_SelectTypeElems", new CommandInfo(0, new ClickEventHandler(Cadmech3DCommandProvider.SelectTypeElemsCommand)));
    mergedCommands.Add("cadmech3D_AddSurfaceFromModel", new CommandInfo(0, new ClickEventHandler(Cadmech3DCommandProvider.AddSurfaceFromModelCommand)));
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

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="factory"></param>
  /// <param name="objTypeId"></param>
  private static void RegisterCommandProvider4ObjectType(IFactory factory, int objTypeId)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    if (MetaDataHelper.HasApplicability(objTypeId, TechCardConsts.ObjectTypes.SurfaceMasterID, TechCardConsts.RelTypes.TechRelationID))
    {
      factory.AddCommandsProvider(1, objTypeId, (ICommandsProvider) new Cadmech3DCommandProvider());
    }
    else
    {
      foreach (int objTypeId1 in MetaDataHelper.GetObjectTypeChildrenID(objTypeId))
        Cadmech3DCommandProvider.RegisterCommandProvider4ObjectType(factory, objTypeId1);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void AddModelFromArticleCommand(
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
    Cadmech3DAddModelBaseCommand modelBaseCommand = new Cadmech3DAddModelBaseCommand("cadmech3D_AddModelFromArticle", new Cadmech3DCommand.CadModelLoadDelegate(Cadmech3DCommand.FindModelForObject));
    modelBaseCommand.Init(items, viewServices, (object) null);
    modelBaseCommand.Execute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void AddModelFromListCommand(
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
    Cadmech3DAddModelBaseCommand modelBaseCommand = new Cadmech3DAddModelBaseCommand("cadmech3D_AddModelFromList", new Cadmech3DCommand.CadModelLoadDelegate(Cadmech3DCommand.SelectModelFromList));
    modelBaseCommand.Init(items, viewServices, (object) null);
    modelBaseCommand.Execute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void SelectTypeElemsCommand(
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
    Cadmech3DSelectTypeElemsCommand typeElemsCommand = new Cadmech3DSelectTypeElemsCommand("cadmech3D_SelectTypeElems", new Cadmech3DCommand.CadModelLoadDelegate(Cadmech3DCommand.FindModelForObject));
    typeElemsCommand.Init(items, viewServices, (object) null);
    typeElemsCommand.Execute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void AddSurfaceFromModelCommand(
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
    Cadmech3DAddSurfaceCommand daddSurfaceCommand = new Cadmech3DAddSurfaceCommand("cadmech3D_AddSurfaceFromModel", new Cadmech3DCommand.CadModelLoadDelegate(Cadmech3DCommand.FindModelForObject));
    daddSurfaceCommand.Init(items, viewServices, (object) null);
    daddSurfaceCommand.Execute();
  }
}
