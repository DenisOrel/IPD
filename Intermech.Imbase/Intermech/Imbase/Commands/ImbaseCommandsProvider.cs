// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Commands.ImbaseCommandsProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Editors;
using Intermech.Imbase.Favorites;
using Intermech.Imbase.ImbaseObjectsCreators;
using Intermech.Imbase.Views;
using Intermech.ImbaseExcelUnloader.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Commands;

internal class ImbaseCommandsProvider : ICommandsProvider
{
  private IDBTypedObjectID _rootSelectedItem;
  private long _newRelationId = -1;

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    INodeID itemId = items.GetItemID(0);
    if (itemId != null && (itemId.TypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID || itemId.TypeID == Intermech.Imbase.Consts.ImbaseTableTypeID))
      mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(ImbaseCommandsProvider.OnOpenTableEditor)));
    else if (itemId != null && itemId.TypeID == Intermech.Imbase.Consts.ImbaseTableMixTypeID)
      mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(this.OnOpenTableEditorMix)));
    else
      mergedCommands.Suppress("EditDocument", 0);
    mergedCommands.Suppress("Exclude", 0);
    if (itemId != null && (itemId.TypeID == Intermech.Imbase.Consts.ImbaseCatalogTypeID || itemId.TypeID == Intermech.Imbase.Consts.ImbaseFolderTypeID || itemId.TypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID))
      mergedCommands.Add("UnloadToExcel", new CommandInfo(0, new ClickEventHandler(UnloadToExcelHelper.Unload)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service1 ? service1.ViewState : ViewStateFlags.None;
    if (items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBTypedObjectID parentData = items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (itemData != null)
    {
      if (items is NavigatorTreeViewSelectedItem || items is NodeItems || (viewStateFlags & ViewStateFlags.NodeInTree) != ViewStateFlags.None)
      {
        this._rootSelectedItem = itemData;
        if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
          groupCommands.Add("CreateFavoritesNode", new CommandInfo(0, new ClickEventHandler(this.CreateFavoritesCommand)));
        if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
        {
          groupCommands.Add("CreateFoldersNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewFolderCommand)));
          if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID)
          {
            groupCommands.Add("CreateCatalogRecordsNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewCatalogRecordCommand)));
            groupCommands.Add("CreateTablesRefNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewRefCommand)));
            groupCommands.Add("CreateTablesMixNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewMixCommand)));
            groupCommands.Add("Imbase.Capitalize", new CommandInfo(0, new ClickEventHandler(this.CapitalizeCommand)));
            groupCommands.Add("CreatedObjectsFromImbase", new CommandInfo(0, new ClickEventHandler(this.ObjectsFromImbase)));
            groupCommands.Add("Imbase.TableFieldsRights", new CommandInfo(0, new ClickEventHandler(this.AssignTableFieldsRights)));
            if (parentData != null && parentData.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
              groupCommands.Add("AddToFavorites", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.AddToFavoritesCommand)));
          }
          if ((viewStateFlags & ViewStateFlags.NodeInTree) != ViewStateFlags.None)
          {
            groupCommands.Add("FindByImages", new CommandInfo(6, new ClickEventHandler(ImbaseCommandsProvider.OnFindByImages)));
            groupCommands.Add("FindInTables", new CommandInfo(6, new ClickEventHandler(ImbaseCommandsProvider.OnFindInTables)));
            groupCommands.Add("FindByName", new CommandInfo(6, new ClickEventHandler(ImbaseCommandsProvider.OnFindByName)));
            groupCommands.Add("FindByIndex", new CommandInfo(6, new ClickEventHandler(ImbaseCommandsProvider.OnFindByIndex)));
            ICurrentUserAndRole service2 = ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, false);
            if ((service2 != null ? (service2.IsAdmin ? 1 : 0) : 0) != 0)
              groupCommands.Add("ReplaceAttribute", new CommandInfo(6, new ClickEventHandler(ImbaseCommandsProvider.OnReplaceAttribute)));
          }
        }
        else if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
        {
          groupCommands.Add("CreatedObjectsFromImbase", new CommandInfo(0, new ClickEventHandler(this.ObjectsFromImbase)));
          if (parentData != null && parentData.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
            groupCommands.Add("AddToFavorites", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.AddToFavoritesCommand)));
        }
        if (itemData.ObjectType != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          groupCommands.Suppress("CreateProto", 0);
        }
        else
        {
          groupCommands.Add("CreateProto", new CommandInfo(0, new ClickEventHandler(ImbaseCommandsProvider.CreatePrototypeRefCommand)));
          groupCommands.Add("CreatedObjectsFromImbase", new CommandInfo(0, new ClickEventHandler(this.ObjectsFromImbase)));
          groupCommands.Add("Imbase.TableFieldsRights", new CommandInfo(0, new ClickEventHandler(this.AssignTableFieldsRights)));
          if (parentData != null && parentData.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
            groupCommands.Add("AddToFavorites", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.AddToFavoritesCommand)));
        }
        groupCommands.Suppress("Navigator.CreateObjectType", 2);
        if (parentData != null && parentData.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID && (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID))
        {
          groupCommands.Add("RemoveFromFavorites", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.RemoveFromFavoritesCommand)));
          groupCommands.Add("FindInImbaseTree", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.FindInImbaseTreeCommand)));
        }
      }
      else
      {
        this._rootSelectedItem = parentData;
        if (parentData == null)
        {
          if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID)
          {
            groupCommands.Add("CreateCatalogsNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewCatalogCommand)));
            groupCommands.Suppress("CreateProto", 0);
          }
          else if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
            groupCommands.Suppress("CreateProto", 0);
        }
        else if (parentData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID)
        {
          groupCommands.Add("CreateFoldersNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewFolderCommand)));
          groupCommands.Add("CreateFavoritesNode", new CommandInfo(0, new ClickEventHandler(this.CreateFavoritesCommand)));
        }
        else if (parentData.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
        {
          groupCommands.Add("CreateFavoritesNode", new CommandInfo(0, new ClickEventHandler(this.CreateFavoritesCommand)));
          if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
          {
            groupCommands.Add("RemoveFromFavorites", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.RemoveFromFavoritesCommand)));
            groupCommands.Add("FindInImbaseTree", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.FindInImbaseTreeCommand)));
          }
        }
        else if (parentData.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID)
        {
          groupCommands.Add("CreateCatalogRecordsNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewCatalogRecordCommand)));
          groupCommands.Add("CreateFoldersNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewFolderCommand)));
          groupCommands.Add("CreateTablesRefNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewRefCommand)));
          groupCommands.Add("CreateTablesMixNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewMixCommand)));
        }
        if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
        {
          groupCommands.Add("CreatedObjectsFromImbase", new CommandInfo(0, new ClickEventHandler(this.ObjectsFromImbase)));
          if (parentData != null && parentData.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
            groupCommands.Add("AddToFavorites", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.AddToFavoritesCommand)));
        }
        if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
          groupCommands.Add("CreateProto", new CommandInfo(0, new ClickEventHandler(ImbaseCommandsProvider.CreatePrototypeRefCommand)));
        else if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          groupCommands.Add("CreateProto", new CommandInfo(0, new ClickEventHandler(ImbaseCommandsProvider.CreatePrototypeRefCommand)));
          groupCommands.Add("CreatedObjectsFromImbase", new CommandInfo(0, new ClickEventHandler(this.ObjectsFromImbase)));
          if (parentData != null && parentData.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
            groupCommands.Add("AddToFavorites", new CommandInfo(0, new ClickEventHandler(ImbaseFavoritesCommands.AddToFavoritesCommand)));
          groupCommands.Suppress("Navigator.CreateObjectType", 6);
        }
        else if (itemData.ObjectType != Intermech.Imbase.Consts.ImbaseTemplateTypeID)
        {
          groupCommands.Suppress("CreateProto", 0);
          groupCommands.Suppress("Navigator.CreateObjectType", 6);
        }
      }
      if (itemData.ObjectType != Intermech.Imbase.Consts.ImbaseTemplateTypeID)
        groupCommands.Suppress("CreateNew", 0);
      if (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID || itemData.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        groupCommands.Add("ImportedTableConflictEditor", new CommandInfo(0, new ClickEventHandler(this.ImportedTableConflictEditorCommand)));
      groupCommands.Suppress("CreateInclude", 0);
      groupCommands.Suppress("CreateVersion", 6);
      groupCommands.Suppress("SeekInTree", 7);
    }
    else
    {
      INodeID itemId = items.GetItemID(0);
      if (itemId != null)
      {
        if (itemId.CategoryID == Intermech.Imbase.Consts.CatalogsNodeCategoryID)
        {
          groupCommands.Add("CreateCatalogsNode", new CommandInfo(0, new ClickEventHandler(this.CreateNewCatalogCommand)));
          groupCommands.Add("FindByIndex", new CommandInfo(6, new ClickEventHandler(ImbaseCommandsProvider.OnFindByIndex)));
        }
        else if (itemId.CategoryID == Intermech.Imbase.Consts.RootNodeCategoryID && (viewStateFlags & ViewStateFlags.NodeInTree) != ViewStateFlags.None)
        {
          groupCommands.Add("FindByIndex", new CommandInfo(6, new ClickEventHandler(ImbaseCommandsProvider.OnFindByIndex)));
          groupCommands.Add("FindByRecordKey", new CommandInfo(6, new ClickEventHandler(this.OnFindByRecordKey)));
        }
      }
    }
    return groupCommands;
  }

  private void OnOpenTableEditorMix(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null)
      return;
    INodeID lastId = items.GetParentPath(0).LastID;
    long targetId = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    long num1 = (long) -sc_7671.ssp_imbase_7672(1176128660);
    int num2 = -1;
    if (lastId is NodeID)
    {
      NodeID nodeId = lastId as NodeID;
      num1 = nodeId.ObjectID;
      num2 = nodeId.RelationTypeID;
    }
    long parentID = num1;
    int relationTypeID = num2;
    EditorMixHelper.CreateEditor(targetId, parentID, relationTypeID).Show();
  }

  private void CreateNewMixCommand(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null || items.Count == 0 || !(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(this._rootSelectedItem.ObjectType);
    Hashtable aObjectTypeIDRelationTypeIDs = new Hashtable()
    {
      {
        (object) Intermech.Imbase.Consts.ImbaseTableMixTypeID,
        (object) objectType.DefaultRelation
      }
    };
    long[] aRelatedObjectIDs = new long[1]
    {
      this._rootSelectedItem.ObjectID
    };
    long objectByTypeDialog = service.CreateObjectByTypeDialog(aObjectTypeIDRelationTypeIDs, aRelatedObjectIDs);
    if (objectByTypeDialog != -1L)
      EditorMixHelper.CreateEditor(objectByTypeDialog, this._rootSelectedItem.ObjectID, objectType.DefaultRelation).Show();
    this._rootSelectedItem = (IDBTypedObjectID) null;
  }

  private void OnFindByRecordKey(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (!(items.GetItemData(0, typeof (NavigatorTreeNode)) is NavigatorTreeNode itemData))
      return;
    IViewsManager service1 = viewservices.GetService(typeof (IViewsManager)) as IViewsManager;
    TreeViewsBridge service2 = viewservices.GetService(typeof (TreeViewsBridge)) as TreeViewsBridge;
    using (FindByRecordCodeForm byRecordCodeForm = new FindByRecordCodeForm())
    {
      if (byRecordCodeForm.ShowDialog() != DialogResult.OK)
        return;
      long linkId = byRecordCodeForm.LinkId;
      long recordId = byRecordCodeForm.RecordId;
      if (!this.CheckRecordExist(linkId, recordId) || FindHelper.SearchNodeByNodeID(itemData, linkId) == null)
      {
        int num = (int) MessageBox.Show($"Не удалось найти запись по коду: {byRecordCodeForm.RecordCodeStr}", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        ImbaseCommandsProvider.RestoreFocusNode(viewservices);
        SelectedRecords.Clear();
        SelectedRecords.Add(linkId, new long[1]{ recordId });
        if (service1 == null)
          return;
        try
        {
          if (service2 != null)
            service2.BridgeEnabled = false;
          service1.UpdateViews(Intermech.Navigator.ContextMenu.Services.GetItems(linkId));
          string str = recordId == -1L ? "ObjectProperties" : "ImbaseTableView";
          foreach (IViewPage viewPage in service1.ViewPages)
          {
            if (!(viewPage.Name != str))
            {
              service1.ActiveViewPage = viewPage;
              break;
            }
          }
        }
        finally
        {
          if (service2 != null)
            service2.BridgeEnabled = true;
        }
      }
    }
  }

  private bool CheckRecordExist(long linkId, long recordId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long linkId1 = 0;
      long tableId = 0;
      TableLoadHelper.CheckObjectId(session, linkId, ref linkId1, ref tableId);
      DataTable table = TableLoadHelper.GetTables(session, tableId, true)?.Tables["IMS_DATA"];
      return table != null && table.Columns.Contains("F_KEY") && table.AsEnumerable().Any<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_KEY"]) == recordId));
    }
  }

  private void ImportedTableConflictEditorCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectID = itemData.ObjectType != Intermech.Imbase.Consts.ImbaseTableRefTypeID ? itemData.ObjectID : sessionKeeper.Session.GetObject(itemData.ObjectID).GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID).AsInteger;
      IDBObject table = sessionKeeper.Session.GetObject(objectID);
      IDBAttribute attributeByGuid1 = table.GetAttributeByGuid(PortalConsts.attributeImportedTableData, false);
      IDBAttribute attributeByGuid2 = table.GetAttributeByGuid(PortalConsts.attributeTableAttributes, false);
      if (attributeByGuid1 != null && attributeByGuid2 != null)
      {
        using (ImportTableConflictEditor tableConflictEditor = new ImportTableConflictEditor())
        {
          tableConflictEditor.Init(table, attributeByGuid1);
          int num = (int) tableConflictEditor.ShowDialog();
        }
      }
      else if (table.GetAttributeByGuid(PortalConsts.attributeTableAttributes, false) == null && table.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false) != null)
      {
        using (ImportOldTableConflictEditor tableConflictEditor = new ImportOldTableConflictEditor())
        {
          tableConflictEditor.InitData(sessionKeeper.Session, table);
          int num = (int) tableConflictEditor.ShowDialog();
        }
      }
      else
      {
        int num1 = (int) IMMessageBox.Show("Редактировать конфликт импорта", $"Конфликт импорта у {table.NameInMessages} не обнаружен", MessageBoxButtons.OK);
      }
    }
  }

  private void OncDlg_ObjectCreatorDraftCreatedEvent(object sender, AfterDraftCreatedEventArgs e)
  {
    if (this._rootSelectedItem == null || e.ObjectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(this._rootSelectedItem.ObjectType);
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(e.ObjectID, false);
      if (!MetaDataHelper.HasApplicability(this._rootSelectedItem.ObjectType, objectActualCopy.ObjectType, objectType.DefaultRelation))
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_7671.ssp_imbase_7673()), (object) objectActualCopy.Caption, (object) this._rootSelectedItem.Caption), LocalizationHolder.rm.GetString("Imbase_CreateRelation_ErrorCaption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
        this._newRelationId = sessionKeeper.Session.GetRelationCollection(objectType.DefaultRelation).Create(this._rootSelectedItem.ObjectID, objectActualCopy.ObjectID).RelationID;
    }
  }

  private static void OnFindByImages(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ImbaseCommandsProvider.RestoreFocusNode(viewServices);
    bool modal = ImbaseCommandsProvider.IsModal(viewServices);
    if (!(items.GetItemData(0, typeof (NavigatorTreeNode)) is NavigatorTreeNode itemData))
      return;
    FindByImagesView.Show((object) itemData, modal, (LocateNodeEventHandler) null);
  }

  private static void OnFindByIndex(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ImbaseCommandsProvider.RestoreFocusNode(viewServices);
    bool modal = ImbaseCommandsProvider.IsModal(viewServices);
    IViewsManager service1 = viewServices.GetService(typeof (IViewsManager)) as IViewsManager;
    TreeViewsBridge service2 = viewServices.GetService(typeof (TreeViewsBridge)) as TreeViewsBridge;
    FindByIndexView.Show((object) (items.GetItemData(0, typeof (NavigatorTreeNode)) as NavigatorTreeNode), modal, (LocateNodeEventHandler) null, service1, service2);
  }

  private static void OnFindInTables(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ImbaseCommandsProvider.RestoreFocusNode(viewServices);
    bool modal = ImbaseCommandsProvider.IsModal(viewServices);
    if (!(items.GetItemData(0, typeof (NavigatorTreeNode)) is NavigatorTreeNode itemData))
      return;
    FindInTablesView.Show((object) itemData, modal, (LocateNodeEventHandler) null);
  }

  private static void OnFindByName(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ImbaseCommandsProvider.RestoreFocusNode(viewServices);
    bool modal = ImbaseCommandsProvider.IsModal(viewServices);
    if (!(items.GetItemData(0, typeof (NavigatorTreeNode)) is NavigatorTreeNode itemData))
      return;
    FindByNameView.Show((object) itemData, modal, (LocateNodeEventHandler) null);
  }

  private static void OnReplaceAttribute(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ImbaseCommandsProvider.RestoreFocusNode(viewServices);
    bool modal = ImbaseCommandsProvider.IsModal(viewServices);
    if (!(items.GetItemData(0, typeof (NavigatorTreeNode)) is NavigatorTreeNode itemData))
      return;
    ReplaceAttributesView.Show((object) itemData, modal);
  }

  private static void OnOpenTableEditor(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      return;
    INodeID lastId = items.GetParentPath(0).LastID;
    long targetId = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    long num1 = (long) -sc_7671.ssp_imbase_7674(1245233041);
    int num2 = -1;
    if (lastId is NodeID)
    {
      NodeID nodeId = lastId as NodeID;
      num1 = nodeId.ObjectID;
      num2 = nodeId.RelationTypeID;
    }
    long parentID = num1;
    int relationTypeID = num2;
    EditorHelper.CreateEditor(targetId, parentID, relationTypeID).Show();
  }

  private void CreateNewCatalogCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0 || !(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service1))
      return;
    if (items is NavigatorTreeViewSelectedItems)
    {
      if (items.GetItemData(0, typeof (IDescriptor)) is IDescriptor itemData)
        ImbaseCatalogCreator.CatalogTypeName = itemData.GetAddress(itemData.GetRecordNodeID());
    }
    else if (items.GetParentData(0, typeof (IDescriptor)) is IDescriptor parentData && parentData is CatalogsNodeDescriptor)
      ImbaseCatalogCreator.CatalogTypeName = parentData.GetAddress(parentData.GetRecordNodeID());
    long objectByTypeDialog = service1.CreateObjectByTypeDialog(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    ImbaseCatalogCreator.CatalogTypeName = string.Empty;
    if (objectByTypeDialog == 0L || objectByTypeDialog == -1L || !(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2))
      return;
    service2.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog));
  }

  private void CreateNewCatalogRecordCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    this.CheckAcess(items, ActionType.CreateFolderOrRecordInCatalog);
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
      return;
    this._newRelationId = -1L;
    service.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.OncDlg_ObjectCreatorDraftCreatedEvent);
    long objectsID = 0;
    try
    {
      objectsID = service.CreateObjectByTypeDialog(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
    }
    finally
    {
      service.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.OncDlg_ObjectCreatorDraftCreatedEvent);
    }
    if (objectsID != -1L && objectsID != 0L && this._newRelationId != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this._newRelationId);
        this.SendNotification(this._rootSelectedItem.ObjectID, objectsID, relation.RelationID, relation.RelationType);
      }
    }
    this._rootSelectedItem = (IDBTypedObjectID) null;
    this._newRelationId = -1L;
  }

  private void CreateNewFolderCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    this.CheckAcess(items, ActionType.CreateFolderOrRecordInCatalog);
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
      return;
    this._newRelationId = -1L;
    service.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.OncDlg_ObjectCreatorDraftCreatedEvent);
    long objectsID = 0;
    try
    {
      objectsID = service.CreateObjectByTypeDialog(Intermech.Imbase.Consts.ImbaseFolderTypeID);
    }
    finally
    {
      service.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.OncDlg_ObjectCreatorDraftCreatedEvent);
    }
    if (this._newRelationId != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this._newRelationId, false);
        if (relation != null)
          this.SendNotification(this._rootSelectedItem.ObjectID, objectsID, relation.RelationID, relation.RelationType);
      }
    }
    this._rootSelectedItem = (IDBTypedObjectID) null;
    this._newRelationId = -1L;
  }

  private void CreateNewRefCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    this.CheckAcess(items, ActionType.CreateTableLinkInCatalog);
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(this._rootSelectedItem.ObjectType);
    Hashtable aObjectTypeIDRelationTypeIDs = new Hashtable(1)
    {
      {
        (object) Intermech.Imbase.Consts.ImbaseTableRefTypeID,
        (object) objectType.DefaultRelation
      }
    };
    long[] aRelatedObjectIDs = new long[1]
    {
      this._rootSelectedItem.ObjectID
    };
    service.CreateObjectByTypeDialog(aObjectTypeIDRelationTypeIDs, aRelatedObjectIDs);
    this._rootSelectedItem = (IDBTypedObjectID) null;
  }

  private void AssignTableFieldsRights(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    INodeID itemId = items.GetItemID(0);
    if (itemId == null || !(itemId is NodeID nodeId))
      return;
    AssingTableAttRights.ShowAccessRightsDialog(nodeId.ObjectID);
  }

  private void CapitalizeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    INodeID itemId = items.GetItemID(0);
    if (itemId == null)
      return;
    using (CapitalizeFoldersDialog capitalizeFoldersDialog = new CapitalizeFoldersDialog())
    {
      if (capitalizeFoldersDialog.ShowDialog() != DialogResult.OK)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
          return;
        customService.CapitalizeFolders(sessionKeeper.Session.SessionGUID, (itemId as NodeID).ObjectID, capitalizeFoldersDialog.UpperCase);
        if (!(viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service))
          return;
        service.TreeRefreshNodeCommand(items, viewServices, additionalInfo);
      }
    }
  }

  private void ObjectsFromImbase(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      return;
    INodeID itemId = items.GetItemID(0);
    if (itemId == null || !(itemId is NodeID))
      return;
    NodeID nodeId = itemId as NodeID;
    string caption = LocalizationHolder.rm.GetString("Imbase_Message");
    string text = LocalizationHolder.rm.GetString("Imbase_CreatedObjectsFromImbase_EmptyAttr_Msg");
    if (itemId.TypeID != Intermech.Imbase.Consts.ImbaseFolderTypeID)
    {
      int createdType = this.GetCreatedType(nodeId.ObjectID);
      if (createdType != -1)
      {
        Utils.OpenNewWindow((IDescriptor) new ObjectsFromImbaseDescriptor(createdType, nodeId.ObjectID, this.GetTypeCaption(createdType)), viewServices);
      }
      else
      {
        int num = (int) MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
    else
    {
      List<long> objIDs = (List<long>) null;
      List<int> createdTypes = this.GetCreatedTypes(nodeId.ObjectID, out objIDs);
      DescriptorCollection descriptors = new DescriptorCollection();
      foreach (int typeID in createdTypes)
        descriptors.Add((IDescriptor) new ObjectsFromImbaseDescriptor(typeID, objIDs, this.GetTypeCaption(typeID)));
      if (descriptors.Count > 0)
      {
        Utils.OpenNewWindow((IDescriptor) new ObjectsFromImbaseDescriptor(descriptors), viewServices);
      }
      else
      {
        int num = (int) MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
  }

  private int GetCreatedType(long objID)
  {
    int createdType = -1;
    if (objID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
        if (objectActualCopy != null)
        {
          IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(Intermech.Imbase.Consts.CreatedObjectAttGUID);
          if (attributeByGuid != null)
          {
            object obj = attributeByGuid.Value;
            if (obj != null && GuidHelper.IsGuid(obj.ToString()))
              createdType = MetaDataHelper.GetObjectTypeID(new Guid(obj.ToString()));
          }
          if (createdType == -1)
          {
            IDBAttribute attributeById = objectActualCopy.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
            if (attributeById != null)
              createdType = this.GetParentWithCreatedTypeAttr(attributeById.AsString);
          }
        }
      }
    }
    return createdType;
  }

  private List<int> GetCreatedTypes(long objID, out List<long> objIDs)
  {
    List<int> createdTypes = new List<int>();
    objIDs = new List<long>();
    int[] numArray = new int[3]
    {
      Intermech.Imbase.Consts.ImbaseFolderTypeID,
      Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID,
      Intermech.Imbase.Consts.ImbaseTableRefTypeID
    };
    int int32 = Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID);
    DataTable dataTable = (DataTable) null;
    string str1 = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
      if (objectActualCopy != null)
      {
        IDBAttribute attributeById = objectActualCopy.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
        if (attributeById != null)
        {
          str1 = attributeById.AsString;
          if (!string.IsNullOrEmpty(str1))
            dataTable = ImbaseHelper.SelectObjects(sessionKeeper.Session, new DBRecordSetParams(new ConditionStructure[2]
            {
              new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) str1, LogicalOperators.AND, 0, true),
              new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE), RelationalOperators.In, (object) numArray, LogicalOperators.NONE, 0, false)
            }, new object[3]
            {
              (object) int32,
              (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
              (object) Intermech.Imbase.Consts.CreatedObjectAttID
            })
            {
              ColumnNames = new ColumnNameMapping[3]
              {
                ColumnNameMapping.ID,
                ColumnNameMapping.ID,
                ColumnNameMapping.ID
              }
            }, numArray);
        }
      }
    }
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      string columnName1 = int32.ToString();
      string columnName2 = Intermech.Imbase.Consts.CreatedObjectAttID.ToString();
      List<object> objectList = new List<object>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        objIDs.Add(Convert.ToInt64(row[columnName1]));
        object obj = row[columnName2];
        if (obj != null && obj != DBNull.Value && !objectList.Contains(obj))
        {
          objectList.Add(obj);
          string str2 = obj.ToString();
          if (GuidHelper.IsGuid(str2))
          {
            IMSObjectType objectType = MetaDataHelper.GetObjectType(new Guid(str2));
            if (objectType != null)
              createdTypes.Add(objectType.ObjectTypeID);
          }
        }
      }
      DataRow[] dataRowArray = dataTable.Select($"[{columnName1}]={objID}");
      if (dataRowArray[0][columnName2] == DBNull.Value || dataRowArray[0][columnName2] == null)
      {
        int withCreatedTypeAttr = this.GetParentWithCreatedTypeAttr(str1);
        if (withCreatedTypeAttr != -1 && !createdTypes.Contains(withCreatedTypeAttr))
          createdTypes.Add(withCreatedTypeAttr);
      }
    }
    return createdTypes;
  }

  private int GetParentWithCreatedTypeAttr(string classifFolderKey)
  {
    int withCreatedTypeAttr = -1;
    if (!string.IsNullOrEmpty(classifFolderKey) && classifFolderKey.Length > 2)
    {
      DataTable dataTable = (DataTable) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        int[] numArray = new int[2]
        {
          Intermech.Imbase.Consts.ImbaseFolderTypeID,
          Intermech.Imbase.Consts.ImbaseCatalogTypeID
        };
        string conditionValue = classifFolderKey.Substring(0, 2);
        dataTable = ImbaseHelper.SelectObjects(sessionKeeper.Session, new DBRecordSetParams(new ConditionStructure[3]
        {
          new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) conditionValue, LogicalOperators.AND, 0, false),
          new ConditionStructure(Intermech.Imbase.Consts.CreatedObjectAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, false),
          new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE), RelationalOperators.In, (object) numArray, LogicalOperators.NONE, 0, false)
        }, new object[2]
        {
          (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
          (object) Intermech.Imbase.Consts.CreatedObjectAttID
        })
        {
          ColumnNames = new ColumnNameMapping[2]
          {
            ColumnNameMapping.ID,
            ColumnNameMapping.ID
          }
        }, numArray);
      }
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        string columnName = Intermech.Imbase.Consts.ClassifFolderKeyAttId.ToString();
        dataTable.DefaultView.Sort = $"{columnName} DESC";
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.DefaultView.ToTable().Rows)
        {
          if (classifFolderKey.StartsWith(row[columnName].ToString()))
          {
            string str = row[Intermech.Imbase.Consts.CreatedObjectAttID.ToString()].ToString();
            if (GuidHelper.IsGuid(str))
            {
              withCreatedTypeAttr = MetaDataHelper.GetObjectTypeID(new Guid(str));
              break;
            }
            break;
          }
        }
      }
    }
    return withCreatedTypeAttr;
  }

  private string GetTypeCaption(int typeID)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(typeID);
    return objectType == null ? string.Empty : objectType.ObjectName;
  }

  private static void CreatePrototypeRefCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count <= 0)
      return;
    INodeID itemId = items.GetItemID(0);
    if (itemId == null)
      return;
    try
    {
      DialogResult dialogResult = MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_CreatePrototype_MessageBox_Message"), LocalizationHolder.rm.GetString("Imbase_CreatePrototype_MessageBox_Caption"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if (dialogResult == DialogResult.Cancel)
        return;
      bool copyData = dialogResult == DialogResult.Yes;
      if (itemId.TypeID == Intermech.Imbase.Consts.ImbaseTableTypeID)
      {
        long objectId = (itemId as NodeID).ObjectID;
        if (objectId == 0L)
          throw new Exception(LocalizationHolder.rm.GetString(sc_7671.ssp_imbase_7675()));
        DataSet copyDS = (DataSet) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          copyDS = TableLoadHelper.GetTables(sessionKeeper.Session, objectId, true);
        DataTable table = copyDS.Tables["IMS_DATA"];
        if (!copyData)
        {
          table.Clear();
          table.AcceptChanges();
        }
        using (CreateCopyTableDialog createCopyTableDialog = new CreateCopyTableDialog(copyDS, objectId))
        {
          int num = (int) createCopyTableDialog.ShowDialog();
        }
      }
      else
      {
        if (itemId.TypeID != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
          return;
        long objectId1 = (itemId as NodeID).ObjectID;
        if (objectId1 == 0L)
          throw new Exception(LocalizationHolder.rm.GetString(sc_7671.ssp_imbase_7676()));
        NodeIDPath parentPath = items.GetParentPath(0);
        if (parentPath == null || !(parentPath.LastID is NodeID lastId))
          return;
        long objectId2 = lastId.ObjectID;
        int relationTypeID = lastId.RelationTypeID;
        if (objectId2 == 0L)
          throw new Exception(LocalizationHolder.rm.GetString(sc_7671.ssp_imbase_7677()));
        if (relationTypeID == -1)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectId2);
            if (!objectInfo.Empty)
              relationTypeID = MetaDataHelper.GetDefaultRelationTypeID(objectInfo.ObjectTypeID);
          }
        }
        if (relationTypeID == -1)
          throw new Exception(LocalizationHolder.rm.GetString(sc_7671.ssp_imbase_7678()));
        ImbaseCommandsProvider.CreatePrototypeTableRef(objectId1, objectId2, relationTypeID, copyData);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  internal static void CreatePrototypeTableRef(
    long sourceTableRefID,
    long parentID,
    int relationTypeID,
    bool copyData)
  {
    DataSet copyDS = (DataSet) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(sourceTableRefID, false);
      if (objectActualCopy == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_PrototypeTableRef_NullSourceTableRef"), (object) Convert.ToString(sourceTableRefID)));
      long tableReference = TableLoadHelper.GetTableReference(sessionKeeper.Session, sourceTableRefID);
      copyDS = TableLoadHelper.GetTables(sessionKeeper.Session, tableReference, true);
      DataTable table1 = copyDS.Tables["IMS_ATTR_TYPES"];
      DataTable table2 = copyDS.Tables["IMS_DATA"];
      if (!copyData)
      {
        table2.Clear();
        table2.AcceptChanges();
      }
      else if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.InBase)
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService)
        {
          long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID(sessionKeeper.Session, sourceTableRefID);
          if (catalogIdByObjectId == 0L)
            throw new Exception(LocalizationHolder.rm.GetString("Imbase_PrototypeTableRef_NullCatalogID"));
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          List<long> catalogIDs = new List<long>();
          catalogIDs.Add(catalogIdByObjectId);
          string[] colsNames = new string[2]
          {
            IndexesField.F_ATTRIBUTE_ID,
            IndexesField.F_FLAG
          };
          DataTable uniqueIndexes1 = customService.GetUniqueIndexes(sessionGuid, catalogIDs, colsNames);
          List<int> uniqueIndexes = uniqueIndexes1 != null ? uniqueIndexes1.AsEnumerable().Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>() : (List<int>) null;
          if (uniqueIndexes != null)
          {
            if (uniqueIndexes.Count > 0)
            {
              if (table1.AsEnumerable().Count<DataRow>((System.Func<DataRow, bool>) (x => uniqueIndexes.Contains(MetaDataHelper.GetAttributeTypeID(Convert.ToString(x[0]))))) > 0)
                throw new Exception(LocalizationHolder.rm.GetString("Imbase_PrototypeTableRef_HasUniqueIndexes"));
            }
          }
        }
      }
    }
    using (CreateCopyTableDialog createCopyTableDialog = new CreateCopyTableDialog(copyDS, sourceTableRefID, parentID, relationTypeID))
    {
      int num = (int) createCopyTableDialog.ShowDialog();
    }
  }

  public void CreateFavoritesCommand(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null || items.Count == 0 || !(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
      return;
    this._newRelationId = -1L;
    service.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.OncDlg_ObjectCreatorDraftCreatedFavoritesEvent);
    long objectsID = 0;
    try
    {
      objectsID = service.CreateObjectByTypeDialog(Intermech.Imbase.Consts.ImbaseFavoritesTypeID);
    }
    finally
    {
      service.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.OncDlg_ObjectCreatorDraftCreatedFavoritesEvent);
    }
    if (this._newRelationId != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this._newRelationId, false);
        if (relation != null)
          this.SendNotification(this._rootSelectedItem.ObjectID, objectsID, relation.RelationID, relation.RelationType);
      }
    }
    this._rootSelectedItem = (IDBTypedObjectID) null;
    this._newRelationId = -1L;
  }

  private void OncDlg_ObjectCreatorDraftCreatedFavoritesEvent(
    object sender,
    AfterDraftCreatedEventArgs e)
  {
    if (this._rootSelectedItem == null || e.ObjectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      MetaDataHelper.GetObjectType(this._rootSelectedItem.ObjectType);
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(e.ObjectID, false);
      if (!MetaDataHelper.HasApplicability(this._rootSelectedItem.ObjectType, objectActualCopy.ObjectType, Intermech.Imbase.Consts.ImbaseFavoritesRelationID))
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_7671.ssp_imbase_7679()), (object) objectActualCopy.Caption, (object) this._rootSelectedItem.Caption), LocalizationHolder.rm.GetString("Imbase_CreateRelation_ErrorCaption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
        this._newRelationId = sessionKeeper.Session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseFavoritesRelationID).Create(this._rootSelectedItem.ObjectID, objectActualCopy.ObjectID).RelationID;
    }
  }

  private static bool IsModal(System.IServiceProvider viewServices)
  {
    return viewServices.GetService(typeof (IViewState)) is IViewState service && (service.ViewState & ViewStateFlags.InDialog) != 0;
  }

  private static void RestoreFocusNode(System.IServiceProvider viewServices, bool canRestore = false)
  {
    if (viewServices == null || !(viewServices.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service))
      return;
    service.CanRestoreFocusedNode = false;
  }

  private void SendNotification(
    long parentsID,
    long objectsID,
    long relationsID,
    int relationsTypesID)
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service) || objectsID == 0L || objectsID == -1L)
      return;
    service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectsID));
    service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", relationsID, parentsID, relationsTypesID));
  }

  private long GetCatalogByClassyfKey(IUserSession session, string classifKey)
  {
    string conditionValue = classifKey;
    if (conditionValue.Length >= 2)
      conditionValue = conditionValue.Substring(0, 2);
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, true)
    }, new ColumnDescriptor[1]{ columnDescriptor });
    DataTable dataTable = objectCollection.Select(paramSet);
    return dataTable == null || dataTable.Rows.Count <= 0 ? 0L : Convert.ToInt64(dataTable.Rows[0][0]);
  }

  private void CheckAcess(ISelectedItems items, ActionType actionType)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string asString = sessionKeeper.Session.GetObject(itemData.ObjectID).GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId)?.AsString;
      if (string.IsNullOrEmpty(asString) || asString.Length < 2)
        return;
      string classifKey = asString.Substring(0, 2);
      long catalogByClassyfKey = this.GetCatalogByClassyfKey(sessionKeeper.Session, classifKey);
      if (!(sessionKeeper.Session.GetObject(catalogByClassyfKey) is IDBSecurity dbSecurity))
        return;
      dbSecurity.CheckAccess(actionType, true, true);
    }
  }
}
