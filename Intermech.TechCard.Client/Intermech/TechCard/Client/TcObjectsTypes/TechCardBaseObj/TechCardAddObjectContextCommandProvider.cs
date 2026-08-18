// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardAddObjectContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>
/// Реализация провайдера для команды контекстного меню "Добавить"
/// </summary>
internal class TechCardAddObjectContextCommandProvider : TechCardBaseCompositionTypesCommandProvider
{
  /// <summary>
  /// Добавление элемента меню для добавления объекта тек. типа (расширение меню добавить)
  /// </summary>
  /// <param name="commandsInfo"></param>
  /// <param name="objectTypeId"></param>
  protected bool AddObjectTypeMenuItem(CommandsInfo commandsInfo, int objectTypeId)
  {
    if (objectTypeId == -1)
      return false;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeId);
    if (objectType == null || objectType.VersionsMode == ObjectVersionModes.Abstract)
      return false;
    commandsInfo.Add("add" + (object) objectTypeId, new CommandInfo(0, new ClickEventHandler(TechCardAddObjectContextCommandProvider.AddObjectTypeCommand), (object) objectTypeId));
    return true;
  }

  /// <summary>Конструктор</summary>
  public TechCardAddObjectContextCommandProvider()
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
      List<IMSObjectType> possibleTypes4Command = this.GetAllPossibleTypes4Command(new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, new List<int>((IEnumerable<int>) TechCardConsts.ObjectTypes.TechAllBaseObjTypes).ToArray());
      for (int index = 0; index < possibleTypes4Command.Count; ++index)
      {
        IMSObjectType imsObjectType = possibleTypes4Command[index];
        if (imsObjectType == null || imsObjectType.VersionsMode == ObjectVersionModes.Abstract)
          break;
        int num = service2 != null ? service2.IndexOf(4, imsObjectType.ObjectTypeID) : -1;
        Keys shortcut;
        TechCardConsts.Caches.TechCardHotKeys.TryGetValue(imsObjectType.Guid, out shortcut);
        MenuTemplateNode orCreate2 = TcClientUtils.FindOrCreate(orCreate1.Nodes, "add" + (object) imsObjectType.ObjectTypeID, imsObjectType.ObjectTypeName, -1, 100, index * 100, shortcut);
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
    System.IServiceProvider viewServices)
  {
    if (items == null || viewServices == null)
      return CommandsInfo.Empty;
    List<int> allowedObjectTypes = AddTechCardObjectCommand.GetAllowedObjectTypes(items, viewServices);
    CommandsInfo commandsInfo = new CommandsInfo();
    if (allowedObjectTypes.Count == 0)
      commandsInfo.Suppress("techElemAddNode", 0);
    foreach (int objectTypeId in allowedObjectTypes)
      this.AddObjectTypeMenuItem(commandsInfo, objectTypeId);
    return commandsInfo;
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

  /// <summary>команда добавления объектов в состав</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void AddObjectTypeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] first = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = first;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !((IEnumerable<byte>) first).SequenceEqual<byte>((IEnumerable<byte>) TechCardProtectionKey.Key[index + 1]))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (viewServices != null)
    {
      INavigatorTreeViewContextMenuHelper service2 = ServiceUtils.GetService<INavigatorTreeViewContextMenuHelper>((object) viewServices, false);
      if (service2 != null)
        service2.CanRestoreFocusedNode = false;
    }
    int objectTypeId = -1;
    try
    {
      objectTypeId = Convert.ToInt32(additionalInfo);
    }
    catch (Exception ex)
    {
      if (!(ex is FormatException))
        throw;
    }
    TechCardSelectedItemsCommand selectedItemsCommand = AddExistingObjectCommand.IsAllowCommand(items, viewServices, additionalInfo) ? (TechCardSelectedItemsCommand) new AddExistingObjectCommand(objectTypeId) : (TechCardSelectedItemsCommand) new AddTechCardObjectCommand(objectTypeId);
    selectedItemsCommand.Init(items, viewServices, additionalInfo);
    selectedItemsCommand.Execute();
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    TechCardAddObjectContextCommandProvider provider = new TechCardAddObjectContextCommandProvider();
    factory.AddCommandsProvider(1, (ICommandsProvider) provider);
  }
}
