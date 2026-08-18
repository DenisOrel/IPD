
// Type: Intermech.Navigator.Selections.Implementation.HiveCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Реализует провайдер команд контекстного меню для
/// корня дерева выборок.
/// </summary>
internal sealed class HiveCommandsProvider : ICommandsProvider
{
  private static ITopBinding _currentBinding;

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    IViewState service1;
    if ((service1 = viewServices.GetService<IViewState>()) != null)
    {
      long viewState = (long) service1.ViewState;
    }
    ITopBinding itemData = items.GetItemData(0, typeof (ITopBinding)) as ITopBinding;
    if (items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    if (itemData != null)
    {
      IClipboard service2 = ServicesManager.GetService<IClipboard>();
      if (service2.GetDataObject() is IDBObjectTypedIDCollection dataObject1 && dataObject1.Count > 0)
      {
        bool flag = true;
        for (int index = 0; index < dataObject1.Count; ++index)
        {
          if (!(dataObject1[index] is IDBTypedObjectID dbTypedObjectId))
          {
            flag = false;
            break;
          }
          if ((!SelectionCommands.IsSelection(dbTypedObjectId.ObjectType) || itemData.BindingType == BindingType.Classificators) && (!SelectionCommands.IsClassifierExcludeFolder(dbTypedObjectId.ObjectType) || itemData.BindingType != BindingType.Classificators))
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          groupCommands.Add("Paste", new CommandInfo(0, new ClickEventHandler(HiveCommandsProvider.PasteCommand)));
          object dataObject = service2.GetDataObject();
          if ((!(dataObject is ICutCopy) ? 0 : ((dataObject as ICutCopy).IsCut ? 1 : 0)) == 0)
            groupCommands.Add("PasteAsLink", new CommandInfo(0, new ClickEventHandler(HiveCommandsProvider.PasteAsLinkCommand)));
        }
      }
    }
    groupCommands.Add("Create", new CommandInfo(0, new ClickEventHandler(HiveCommandsProvider.CreateSelection)));
    return groupCommands;
  }

  private static void PasteAsLinkCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    HiveCommandsProvider.PasteCommandMethod(items, (HiveCommandsProvider.PasteCommandHandler) ((session, binding, otc, selSvc, nService, isCut) =>
    {
      for (int index = 0; index < otc.Count; ++index)
      {
        IDBTypedObjectID dbTypedObjectId = otc[index] as IDBTypedObjectID;
        binding.BindSelection(dbTypedObjectId.ObjectID);
        nService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", dbTypedObjectId.ObjectID));
      }
    }));
  }

  private static void PasteCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    HiveCommandsProvider.PasteCommandMethod(items, (HiveCommandsProvider.PasteCommandHandler) ((session, binding, otc, selSvc, nService, isCut) =>
    {
      IDBObjectCollection objectCollection = (IDBObjectCollection) null;
      for (int index = 0; index < otc.Count; ++index)
      {
        IDBTypedObjectID dbTypedObjectId = otc[index] as IDBTypedObjectID;
        if (!isCut)
        {
          if (objectCollection == null || objectCollection.ObjectTypeID != dbTypedObjectId.ObjectType)
            objectCollection = session.GetObjectCollection(dbTypedObjectId.ObjectType);
          IDBObject dbObject = objectCollection.Create(dbTypedObjectId.ObjectID);
          dbObject.GetAttributeByID(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID)?.ClearValues();
          binding.BindSelection(dbObject.ObjectID);
          dbObject.CommitCreation(false);
          nService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", dbObject.ObjectID));
          CompositionCopierTask.BeginCreate(dbObject.ObjectID, dbTypedObjectId.ObjectID);
        }
        else
        {
          IDBObject dbObject = session.GetObject(dbTypedObjectId.ObjectID);
          dbObject.GetAttributeByID(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID)?.ClearValues();
          binding.BindSelection(dbObject.ObjectID);
          nService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", dbTypedObjectId.ObjectID));
        }
      }
    }));
  }

  private static void PasteCommandMethod(
    ISelectedItems items,
    HiveCommandsProvider.PasteCommandHandler method)
  {
    if (method == null)
      throw new ArgumentNullException();
    ITopBinding itemData = items.GetItemData(0, typeof (ITopBinding)) as ITopBinding;
    object dataObject = ((IClipboard) ServicesManager.GetService(typeof (IClipboard))).GetDataObject();
    bool isCut = dataObject is ICutCopy && (dataObject as ICutCopy).IsCut;
    if (!(dataObject is IDBObjectTypedIDCollection otc) || otc.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ISelectionsService customService = sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
      INotificationService service = ServicesManager.GetService<INotificationService>();
      method(sessionKeeper.Session, itemData, otc, customService, service, isCut);
    }
  }

  private static void CreateSelection(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    INavigatorTreeViewContextMenuHelper service1;
    if (viewServices != null && (service1 = viewServices.GetService<INavigatorTreeViewContextMenuHelper>()) != null)
      service1.CanRestoreFocusedNode = false;
    if (!(items.GetItemData(0, typeof (ITopBinding)) is ITopBinding itemData))
      return;
    HiveCommandsProvider._currentBinding = itemData;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(items.GetItemID(0).TypeID);
    IObjectCreatorService service2 = ServicesManager.GetService<IObjectCreatorService>();
    service2.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(HiveCommandsProvider.CDlg_ObjectCreatorDraftCreatedEvent);
    try
    {
      int objectTypeID;
      long objectByTypeDialog = service2.CreateObjectByTypeDialog(childrenIdRecursive.ToArray(), out objectTypeID);
      if (objectByTypeDialog == -1L)
        return;
      DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog, objectTypeID, true);
      Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
    }
    finally
    {
      service2.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(HiveCommandsProvider.CDlg_ObjectCreatorDraftCreatedEvent);
    }
  }

  private static void CDlg_ObjectCreatorDraftCreatedEvent(
    object sender,
    AfterDraftCreatedEventArgs e)
  {
    if (HiveCommandsProvider._currentBinding == null)
      return;
    HiveCommandsProvider._currentBinding.BindSelection(e.ObjectID);
    HiveCommandsProvider._currentBinding = (ITopBinding) null;
  }

  private delegate void PasteCommandHandler(
    IUserSession session,
    ITopBinding binding,
    IDBObjectTypedIDCollection otc,
    ISelectionsService selSvc,
    INotificationService nService,
    bool isCut);
}
