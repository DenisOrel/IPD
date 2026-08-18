// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.CehRoutesContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Ceh_Route;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands.Edit;
using Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.Commands;
using Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route;

/// <summary>
/// Провайдер контекстного меню для объектов типа "Расцеховочный маршрут"
/// </summary>
internal class CehRoutesContextCommandProvider : ICommandsProvider
{
  /// <summary>Вызов команды "Редактировать"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void EditObjectCommand(
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
    SimpleEditCommand simpleEditCommand = new SimpleEditCommand();
    simpleEditCommand.Init(items, viewServices, additionalInfo);
    simpleEditCommand.Execute();
  }

  /// <summary>Реализация команды "Обновить строку маршрута"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void RouteUpdateCehRouteStringCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index1 = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index1];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service1.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index1 + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num1));
    if (viewServices == null || items == null || items.Count == 0)
      return;
    List<ObjInfoItem> source = new List<ObjInfoItem>(items.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICehRouteStringService service2 = ServiceUtils.GetService<ICehRouteStringService>((object) sessionKeeper.Session, true);
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID);
      childrenIdRecursive.Add(TechCardConsts.ObjectTypes.CehRouteID);
      GenericListHelper.MakeUnique<int>(childrenIdRecursive);
      for (int index2 = 0; index2 < items.Count; ++index2)
      {
        if (!(items.GetItemData(index2, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
          return;
        if (childrenIdRecursive.BinarySearch(itemData.ObjectType) >= 0)
        {
          service2.CreateCehRouteString(itemData.ObjectID, sessionKeeper.Session.SessionGUID, true);
          source.Add(new ObjInfoItem(itemData.ObjectID, itemData.ObjectType));
        }
      }
    }
    int num2 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_19459.ssp_techcard_19460()), (object) source.Count), LocalizationHolder.rm.GetString(sc_19459.ssp_techcard_19461()), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) source.Select<ObjInfoItem, long>((Func<ObjInfoItem, long>) (item => item.ObjectID)).ToArray<long>(), (IList<int>) source.Select<ObjInfoItem, int>((Func<ObjInfoItem, int>) (item => item.ObjTypeID)).ToArray<int>()));
  }

  private void RouteGroupCommand_DeleteObject(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    RouteElemObjectsDeleteCommand objectsDeleteCommand = new RouteElemObjectsDeleteCommand();
    objectsDeleteCommand.Init(items, viewServices, additionalInfo);
    objectsDeleteCommand.Execute();
  }

  private void RouteGroupCommand_ReplaceObject(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    RouteElemObjectsReplaceCommand objectsReplaceCommand = new RouteElemObjectsReplaceCommand();
    objectsReplaceCommand.Init(items, viewServices, additionalInfo);
    objectsReplaceCommand.Execute();
  }

  private void RouteGroupCommand_InsertLastObject(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    RouteElemObjectsInsertCommand objectsInsertCommand = new RouteElemObjectsInsertCommand(CompositionTargetMode.Add);
    objectsInsertCommand.Init(items, viewServices, additionalInfo);
    objectsInsertCommand.Execute();
  }

  private void RouteGroupCommand_InsertFirstObject(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    RouteElemObjectsInsertCommand objectsInsertCommand = new RouteElemObjectsInsertCommand(CompositionTargetMode.InsertFirst);
    objectsInsertCommand.Init(items, viewServices, additionalInfo);
    objectsInsertCommand.Execute();
  }

  private void RouteGroupCommand_ApplyToArticleCopy(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    CehRouteApplyToArticleCopyCommand articleCopyCommand = new CehRouteApplyToArticleCopyCommand();
    articleCopyCommand.Init(items, viewServices, additionalInfo);
    articleCopyCommand.Execute();
  }

  /// <summary>Конструктор</summary>
  public CehRoutesContextCommandProvider([NotNull] IFactory factory)
  {
    MenuTemplateNode node = factory.ContextMenuTemplate["TechCard.RouteGroupCommands"];
    MenuTemplate contextMenuTemplate = factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "RouteUpdateCehRouteString", LocalizationHolder.rm.GetString("TechCard.Client_449"), -1, 13, 15);
      if (node == null)
      {
        ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
        int imageIndex = -1;
        node = new MenuTemplateNode("RouteGroupCommands", LocalizationHolder.rm.GetString("TechCard.RouteGroupCommands"), imageIndex, 40, 30);
        contextMenuTemplate.Nodes.Add(node);
      }
      node.Nodes.Add(new MenuTemplateNode("RouteGroupCommand_InsertFirstObject", LocalizationHolder.rm.GetString("TechCard.RouteGroupCommand_InsertFirstObject"), -1, 10, 20));
      node.Nodes.Add(new MenuTemplateNode("RouteGroupCommand_InsertLastObject", LocalizationHolder.rm.GetString("TechCard.RouteGroupCommand_InsertLastObject"), -1, 10, 30));
      node.Nodes.Add(new MenuTemplateNode("RouteGroupCommand_ReplaceObject", LocalizationHolder.rm.GetString("TechCard.RouteGroupCommand_ReplaceObject"), -1, 10, 40));
      node.Nodes.Add(new MenuTemplateNode("RouteGroupCommand_DeleteObject", LocalizationHolder.rm.GetString("TechCard.RouteGroupCommand_DeleteObject"), -1, 10, 50));
      node.Nodes.Add(new MenuTemplateNode("RouteGroupCommand_ApplyToArticleCopy", LocalizationHolder.rm.GetString("TechCard.RouteGroupCommand_ApplyToArticleCopy"), -1, 10, 50));
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
    if (viewServices == null)
      return CommandsInfo.Empty;
    IViewState service = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    ViewStateFlags viewStateFlags = service != null ? service.ViewState : ViewStateFlags.None;
    CommandsInfo mergedCommands = new CommandsInfo();
    if (viewStateFlags.HasFlag((Enum) ViewStateFlags.ReadOnly))
      return mergedCommands;
    if (items.Count > 0)
    {
      mergedCommands.Add("RouteUpdateCehRouteString", new CommandInfo(0, new ClickEventHandler(CehRoutesContextCommandProvider.RouteUpdateCehRouteStringCommand)));
      if (!viewStateFlags.HasFlag((Enum) ViewStateFlags.InDialog) && MetaDataHelper.GetApplicability(TechCardConsts.ObjectTypes.CehRouteID, TechCardConsts.ObjectTypes.ElemRouteID, TechCardConsts.RelTypes.TechRelationID) != null)
      {
        mergedCommands.Add("RouteGroupCommand_InsertFirstObject", new CommandInfo(0, new ClickEventHandler(this.RouteGroupCommand_InsertFirstObject)));
        mergedCommands.Add("RouteGroupCommand_InsertLastObject", new CommandInfo(0, new ClickEventHandler(this.RouteGroupCommand_InsertLastObject)));
        mergedCommands.Add("RouteGroupCommand_ReplaceObject", new CommandInfo(0, new ClickEventHandler(this.RouteGroupCommand_ReplaceObject)));
        mergedCommands.Add("RouteGroupCommand_DeleteObject", new CommandInfo(0, new ClickEventHandler(this.RouteGroupCommand_DeleteObject)));
      }
    }
    if (!viewStateFlags.HasFlag((Enum) ViewStateFlags.InDialog) && items.Count == 1)
    {
      mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(CehRoutesContextCommandProvider.EditObjectCommand)));
      mergedCommands.Add("RouteGroupCommand_ApplyToArticleCopy", new CommandInfo(0, new ClickEventHandler(this.RouteGroupCommand_ApplyToArticleCopy)));
    }
    return mergedCommands;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }
}
