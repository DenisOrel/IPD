// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProductionCopy.ProductionCopyCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands.CreateByAnalog;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProductionCopy;

/// <summary>
/// Реализация провайдера команд для производственных копий изделий
/// </summary>
internal class ProductionCopyCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  private ProductionCopyCommandProvider()
  {
    IFactory service1 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, false);
    if (service1 == null)
      return;
    ICategoryTypeIconService service2 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    MenuTemplate contextMenuTemplate = service1.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode orCreate1 = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "CreateByProductionAnalogObject", LocalizationHolder.rm.GetString("TechCard.Client_CreateByProductionCopyAnalog"), -1, 13, 90);
      orCreate1.ImageListSource = ImageListSource.CategoryImageList;
      orCreate1.ImageIndex = service2 != null ? service2.IndexOf(4, TechCardConsts.ObjectTypes.ArticleCopyBaseID) : -1;
      IList<int> createdObjectTypeIds = ProductionCopyCommandProvider.GetCreatedObjectTypeIds();
      for (int index = 0; index < createdObjectTypeIds.Count; ++index)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(createdObjectTypeIds[index]);
        if (objectType != null && objectType.VersionsMode != ObjectVersionModes.Abstract)
        {
          int num = service2 != null ? service2.IndexOf(4, objectType.ObjectTypeID) : -1;
          MenuTemplateNode orCreate2 = TcClientUtils.FindOrCreate(orCreate1.Nodes, "CreateByProductionAnalogObject_" + (object) objectType.ObjectTypeID, objectType.ObjectTypeName, -1, 100, index * 100);
          orCreate2.ImageListSource = ImageListSource.CategoryImageList;
          orCreate2.ImageIndex = num;
        }
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
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null || viewServices == null)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    IViewState service = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    if (((ViewStateFlags) (service != null ? (long) service.ViewState : 0L)).HasFlag((Enum) ViewStateFlags.ReadOnly) || items.Count != 1)
    {
      mergedCommands.Suppress("CreateByProductionAnalogObject", 0);
    }
    else
    {
      foreach (int createdObjectTypeId in (IEnumerable<int>) ProductionCopyCommandProvider.GetCreatedObjectTypeIds())
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(createdObjectTypeId);
        if (objectType != null && objectType.VersionsMode != ObjectVersionModes.Abstract)
          mergedCommands.Add("CreateByProductionAnalogObject_" + (object) createdObjectTypeId, new CommandInfo(0, new ClickEventHandler(ProductionCopyCommandProvider.CreateByProductionAnalogObjectCommand), (object) createdObjectTypeId));
      }
    }
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

  private static IList<int> GetCreatedObjectTypeIds()
  {
    List<int> collection = new List<int>();
    collection.AddRange<int>((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID));
    collection.AddRange<int>((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ZagotID));
    return (IList<int>) collection;
  }

  /// <summary>Команда создания объектов по ПВ-аналогу</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void CreateByProductionAnalogObjectCommand(
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
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString(sc_19595.ssp_techcard_19596()), (object) num));
    if (viewServices != null)
    {
      INavigatorTreeViewContextMenuHelper service2 = ServiceUtils.GetService<INavigatorTreeViewContextMenuHelper>((object) viewServices, false);
      if (service2 != null)
        service2.CanRestoreFocusedNode = false;
    }
    int createObjectTypeId = -1;
    try
    {
      createObjectTypeId = Convert.ToInt32(additionalInfo);
    }
    catch (Exception ex)
    {
      if (!(ex is FormatException))
        throw;
    }
    CreateByAnalogObjectCommand analogObjectCommand = new CreateByAnalogObjectCommand(createObjectTypeId);
    analogObjectCommand.Init(items, viewServices, additionalInfo);
    analogObjectCommand.Execute();
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  public static void RegisterCommandProvider([NotNull] IFactory factory)
  {
    ProductionCopyCommandProvider provider = new ProductionCopyCommandProvider();
    factory.AddCommandsProvider(1, MRP2Consts.objtypeIdProductionLists, (ICommandsProvider) provider);
    factory.AddCommandsProvider(1, TechCardConsts.ObjectTypes.ArticleCopyBaseID, (ICommandsProvider) provider);
  }
}
