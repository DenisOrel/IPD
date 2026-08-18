// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Commands.ImbaseCopyPasteProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Imbase.BackgroundTask;
using Intermech.Imbase.Favorites;
using Intermech.Imbase.Indexes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Commands;

internal class ImbaseCopyPasteProvider : ICommandsProvider
{
  private Guid CodeImbaseAttrGuid = new Guid("cad0020f-306c-11d8-b4e9-00304f19f545");

  private ImbaseClipboard ImbaseClipboard
  {
    get
    {
      return !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service) ? (ImbaseClipboard) null : service.GetDataObject() as ImbaseClipboard;
    }
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    if (items != null && items.Count == 1)
    {
      bool flag = false;
      ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
      if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
        flag = this.ImbaseClipboard != null && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID;
      if (flag)
        groupCommands.Add("Paste", new CommandInfo(0, new ClickEventHandler(this.Paste), (object) "CreateCopy"));
      else
        groupCommands.Suppress("Paste", 0);
    }
    return groupCommands;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items != null && items.Count > 0)
    {
      IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      IDBTypedObjectID parentData = items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      ViewStateFlags stateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
      if (this.CanAddCommandCut(viewServices, stateFlags, itemData, parentData))
        mergedCommands.Add("Cut", new CommandInfo(0, new ClickEventHandler(this.CutCopy), (object) "Cut"));
      else
        mergedCommands.Suppress("Cut", 0);
      if (this.CanAddCommandCopy(viewServices, stateFlags, itemData, parentData))
        mergedCommands.Add("Copy", new CommandInfo(0, new ClickEventHandler(this.CutCopy), (object) "Copy"));
      else
        mergedCommands.Suppress("Copy", 0);
      ImbaseCopyPasteProvider.PasteFlag pasteFlag = this.CanAddCommandPaste(viewServices, stateFlags, itemData);
      if (items.Count <= 1)
      {
        switch (pasteFlag)
        {
          case ImbaseCopyPasteProvider.PasteFlag.None:
            break;
          case ImbaseCopyPasteProvider.PasteFlag.Simple:
            mergedCommands.Add("Paste", new CommandInfo(0, new ClickEventHandler(this.Paste), (object) "CreateCopy"));
            goto label_13;
          case ImbaseCopyPasteProvider.PasteFlag.Link:
            mergedCommands.Add("CreateCopy", new CommandInfo(0, new ClickEventHandler(this.Paste), (object) "CreateCopy"));
            mergedCommands.Add("CreatePrototype", new CommandInfo(0, new ClickEventHandler(this.Paste), (object) "CreatePrototype"));
            goto label_13;
          case ImbaseCopyPasteProvider.PasteFlag.Folder:
            mergedCommands.Add("CreateFolderCopy", new CommandInfo(0, new ClickEventHandler(this.Paste), (object) "CreateCopy"));
            mergedCommands.Add("CreateFolderPrototype", new CommandInfo(0, new ClickEventHandler(this.Paste), (object) "CreatePrototype"));
            goto label_13;
          default:
            goto label_13;
        }
      }
      mergedCommands.Suppress("Paste", 0);
    }
label_13:
    return mergedCommands;
  }

  private void CutCopy(ISelectedItems items, System.IServiceProvider viewServices, object additionalInfo)
  {
    if (items == null || items.Count <= 0)
      return;
    if (!(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_CannotGetCutCopyService"), LocalizationHolder.rm.GetString("Imbase.Client_45"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      bool isCut = object.Equals(additionalInfo, (object) "Cut");
      if (!isCut)
        ObjectCommands.AddToWindowsClipboard(items, viewServices, additionalInfo);
      List<ClipboardObject> collection = new List<ClipboardObject>(items.Count);
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData1 = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        IDBRelationID itemData2 = items.GetItemData(index, typeof (IDBRelationID)) as IDBRelationID;
        if (itemData1 != null)
          collection.Add(new ClipboardObject(itemData1, itemData2));
      }
      if (collection.Count <= 0)
        return;
      service.SetDataObject((object) new ImbaseClipboard(collection, isCut));
    }
  }

  private void Paste(ISelectedItems items, System.IServiceProvider viewServices, object additionalInfo)
  {
    if (items == null || items.Count <= 0)
      return;
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    ImbaseClipboard imbaseClipboard = this.ImbaseClipboard;
    if (itemData == null)
      return;
    if (imbaseClipboard == null)
      return;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long num1 = itemData.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID ? itemData.ObjectID : (itemData.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID ? this.GetCatalogIdByFavoritesId(sessionKeeper.Session, itemData.ObjectID) : TableLoadHelper.GetCatalogIDByObjectID(sessionKeeper.Session, itemData.ObjectID));
        long prevCatalogID = imbaseClipboard[0].ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID ? this.GetCatalogIdByFavoritesId(sessionKeeper.Session, imbaseClipboard[0].ObjectID) : TableLoadHelper.GetCatalogIDByObjectID(sessionKeeper.Session, imbaseClipboard[0].ObjectID);
        if (!this.CheckUniqueIndexes(sessionKeeper.Session, imbaseClipboard, num1, prevCatalogID))
          return;
        if (imbaseClipboard[0].ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID && num1 != prevCatalogID)
        {
          int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_CopyPaste_Favorites_Msg"), LocalizationHolder.rm.GetString("Imbase_CopyPaste_PasteError_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          List<long> longList = (List<long>) null;
          if (imbaseClipboard.IsCut)
          {
            this.CutPaste(sessionKeeper.Session, itemData, imbaseClipboard);
          }
          else
          {
            bool isCreateCopy = object.Equals(additionalInfo, (object) "CreateCopy");
            longList = this.CopyPaste(sessionKeeper.Session, itemData, imbaseClipboard, isCreateCopy);
          }
          if (longList == null || !(ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service))
            return;
          IndexesHelper helper = new IndexesHelper(num1, IndexesStatus.UpdateAfterCopyMove)
          {
            PrevCatalogID = prevCatalogID,
            PastedObjIDs = longList
          };
          service.AddTask((IBackgroundTask) new ImbaseIndexesBackgroundTask(helper));
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      try
      {
        if (ImbaseCopyPasteProvider.DataForEvents.CreatedRelIDs.Count > 0)
        {
          INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
          if (imbaseClipboard.IsCut)
            service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) ImbaseCopyPasteProvider.DataForEvents.RemovedRelIDs, (IList<long>) ImbaseCopyPasteProvider.DataForEvents.RemovedParentIDs, (IList<int>) null, (IList<int>) ImbaseCopyPasteProvider.DataForEvents.RemovedRelTypeIDs));
          if (ImbaseCopyPasteProvider.DataForEvents.CreatedChildIDs.Count > 0)
            service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) ImbaseCopyPasteProvider.DataForEvents.CreatedChildIDs));
          service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) ImbaseCopyPasteProvider.DataForEvents.CreatedRelIDs, (IList<long>) ImbaseCopyPasteProvider.DataForEvents.CreatedParentIDs, (IList<int>) null, (IList<int>) ImbaseCopyPasteProvider.DataForEvents.CreatedRelTypeIDs));
        }
      }
      finally
      {
        ImbaseCopyPasteProvider.DataForEvents.Clear();
      }
    }
  }

  private List<long> CutPaste(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    ImbaseClipboard imbClipboard)
  {
    List<long> longList = new List<long>(imbClipboard.Count);
    foreach (IGrouping<int, ClipboardObject> grouping in (IEnumerable<IGrouping<int, ClipboardObject>>) imbClipboard.GetObjsGroupedByType)
    {
      int defaultRelationTypeId = ImbaseHelper.GetDefaultRelationTypeID(selectedObj.ObjectType, grouping.Key);
      if (defaultRelationTypeId != -1)
      {
        if (defaultRelationTypeId != Intermech.Imbase.Consts.ImbaseFavoritesRelationID)
        {
          ISelectionsService selectionsService = this.GetSelectionsService(session);
          string objectClassifKey = this.GetSelectedObjectClassifKey(session, selectedObj.ObjectID);
          foreach (ClipboardObject clipboardObject in (IEnumerable<ClipboardObject>) grouping)
          {
            if (clipboardObject.RelationType != Intermech.Imbase.Consts.ImbaseFavoritesRelationID)
            {
              IDBObject objectActualCopy = session.GetObjectActualCopy(clipboardObject.ObjectID, false);
              if (objectActualCopy != null)
              {
                IDBAttribute classifKeyAttribute = this.GetClassifKeyAttribute(objectActualCopy);
                string asString = classifKeyAttribute.AsString;
                if (!this.CheckLoop(objectClassifKey, asString))
                {
                  string nextClassifierKey = selectionsService.GenerateNextClassifierKey((object) session.SessionGUID, selectedObj.ObjectType, objectClassifKey, objectActualCopy.ObjectType);
                  IDBRelation dbRelation = session.GetRelation(clipboardObject.Value, false);
                  if (dbRelation == null)
                  {
                    IDBRelationCollection relationCollection = session.GetRelationCollection(defaultRelationTypeId);
                    relationCollection.LocalTypesMode = true;
                    DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
                    {
                      (object) -20
                    }), clipboardObject.ObjectID);
                    dbRelation = dataTable.Rows.Count > 0 ? session.GetRelation(Convert.ToInt64(dataTable.Rows[0][0])) : relationCollection.Create(selectedObj.ObjectID, objectActualCopy.ObjectID);
                  }
                  long projId = dbRelation.ProjID;
                  if (projId != selectedObj.ObjectID)
                    dbRelation.ProjID = selectedObj.ObjectID;
                  ImbaseCopyPasteProvider.DataForEvents.AddRemovedData(projId, dbRelation.RelationID, dbRelation.RelationType);
                  this.UpdateClassifKeyAttribute(session, objectActualCopy.ObjectID, classifKeyAttribute, nextClassifierKey);
                  if (objectActualCopy.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID)
                    this.SetClassifKey4Level(session, selectionsService, objectActualCopy.ObjectID, objectActualCopy.ObjectType, nextClassifierKey);
                  ImbaseCopyPasteProvider.DataForEvents.AddCreatedData(selectedObj.ObjectID, clipboardObject.ObjectID, dbRelation.RelationID, dbRelation.RelationType);
                  longList.Add(clipboardObject.ObjectID);
                }
              }
            }
          }
        }
        else
        {
          IDBRelationCollection relationCollection = session.GetRelationCollection(defaultRelationTypeId);
          foreach (ClipboardObject clipboardObject in (IEnumerable<ClipboardObject>) grouping)
          {
            if (clipboardObject.RelationType == Intermech.Imbase.Consts.ImbaseFavoritesRelationID && clipboardObject.ProjID != selectedObj.ObjectID)
            {
              if (clipboardObject.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID && this.CheckLoopForFavorites(relationCollection, selectedObj.ObjectID, clipboardObject.ObjectID))
              {
                int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_CopyPaste_PasteInSelf_Msg"), LocalizationHolder.rm.GetString("Imbase_CopyPaste_PasteError_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
              }
              else
              {
                IDBRelation relation = session.GetRelation(clipboardObject.Value, false);
                if (relation != null)
                {
                  long projId = relation.ProjID;
                  if (projId != selectedObj.ObjectID)
                    relation.ProjID = selectedObj.ObjectID;
                  ImbaseCopyPasteProvider.DataForEvents.AddRemovedData(projId, relation.RelationID, relation.RelationType);
                  ImbaseCopyPasteProvider.DataForEvents.AddCreatedData(selectedObj.ObjectID, clipboardObject.ObjectID, relation.RelationID, relation.RelationType);
                  longList.Add(clipboardObject.ObjectID);
                }
              }
            }
          }
        }
      }
    }
    return longList.Count <= 0 ? (List<long>) null : longList;
  }

  private List<long> CopyPaste(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    ImbaseClipboard imbClipboard,
    bool isCreateCopy)
  {
    List<string> copiedFolderKeys = new List<string>();
    Dictionary<string, long> folderKeys = new Dictionary<string, long>();
    List<long> longList = (List<long>) null;
    if (selectedObj.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
    {
      Dictionary<long, long> newParentIDs = this.PasteFolders(session, selectedObj, imbClipboard, ref copiedFolderKeys, ref folderKeys);
      this.PasteRecords(session, selectedObj, imbClipboard, newParentIDs, copiedFolderKeys, folderKeys);
      longList = this.PasteLinks(session, selectedObj, imbClipboard, newParentIDs, copiedFolderKeys, folderKeys, isCreateCopy);
    }
    this.PasteToFavorites(session, selectedObj, imbClipboard);
    return longList;
  }

  private Dictionary<long, long> PasteFolders(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    ImbaseClipboard imbClipboard,
    ref List<string> copiedFolderKeys,
    ref Dictionary<string, long> folderKeys)
  {
    Dictionary<long, long> dictionary = new Dictionary<long, long>();
    List<long> copiedFolderIDs = imbClipboard.FolderIDs;
    if (copiedFolderIDs != null)
    {
      Dictionary<long, string> keys = (Dictionary<long, string>) null;
      IDBObjectCollection objectCollection1 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
      if (objectCollection1 != null)
      {
        keys = this.GetFolderKeys(objectCollection1, copiedFolderIDs);
        if (keys != null)
          this.GetInLoopID(session, selectedObj.ObjectID, keys)?.ForEach((Action<long>) (x =>
          {
            keys.Remove(x);
            copiedFolderIDs.Remove(x);
          }));
      }
      if (keys != null && keys.Count > 0)
      {
        bool flag = true;
        string caption = LocalizationHolder.rm.GetString("Imbase_Paste_Caption");
        switch (MessageBox.Show($"{LocalizationHolder.rm.GetString("Imbase_Paste")} {LocalizationHolder.rm.GetString("Imbase_CopyIncludedObject")}?", caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
        {
          case DialogResult.Cancel:
            return (Dictionary<long, long>) null;
          case DialogResult.Yes:
            flag = false;
            break;
        }
        IDBObjectCollection objectCollection2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
        if (objectCollection2 == null)
          throw new ApplicationException("Не удалось получить коллекцию объектов типа 'Папка IMBASE'.");
        Dictionary<long, List<long>> first = new Dictionary<long, List<long>>();
        copiedFolderKeys = keys.Values.ToList<string>();
        if (!flag)
        {
          foreach (KeyValuePair<long, string> keyValuePair in keys)
          {
            Dictionary<string, long> subFolderKeys = this.GetSubFolderKeys(objectCollection2, keyValuePair.Value);
            folderKeys = folderKeys.Concat<KeyValuePair<string, long>>((IEnumerable<KeyValuePair<string, long>>) subFolderKeys).ToDictionary<KeyValuePair<string, long>, string, long>((System.Func<KeyValuePair<string, long>, string>) (x => x.Key), (System.Func<KeyValuePair<string, long>, long>) (y => y.Value));
            Dictionary<long, List<long>> second = this.GroupedFolderByParentID(subFolderKeys);
            if (second != null)
              first = first.Concat<KeyValuePair<long, List<long>>>((IEnumerable<KeyValuePair<long, List<long>>>) second).ToDictionary<KeyValuePair<long, List<long>>, long, List<long>>((System.Func<KeyValuePair<long, List<long>>, long>) (x => x.Key), (System.Func<KeyValuePair<long, List<long>>, List<long>>) (y => y.Value));
          }
        }
        IDBRelationCollection relationCollection1 = this.GetRelationCollection(session, (IDBRelationCollection) null, selectedObj.ObjectType, Intermech.Imbase.Consts.ImbaseFolderTypeID);
        if (relationCollection1 != null)
        {
          if (copiedFolderIDs.Count > 0)
          {
            foreach (long num in copiedFolderIDs)
            {
              long newObject = this.CreateNewObject(objectCollection2, relationCollection1, selectedObj.ObjectID, num);
              dictionary.Add(num, newObject);
            }
          }
          if (first.Count > 0)
          {
            IDBRelationCollection relationCollection2 = this.GetRelationCollection(session, relationCollection1, Intermech.Imbase.Consts.ImbaseFolderTypeID, Intermech.Imbase.Consts.ImbaseFolderTypeID);
            if (relationCollection2 != null)
            {
              foreach (KeyValuePair<long, List<long>> keyValuePair in first)
              {
                long parentID = dictionary[keyValuePair.Key];
                foreach (long num in keyValuePair.Value)
                {
                  long newObject = this.CreateNewObject(objectCollection2, relationCollection2, parentID, num);
                  dictionary.Add(num, newObject);
                }
              }
            }
          }
        }
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<long, long>) null : dictionary;
  }

  private Dictionary<long, string> GetFolderKeys(
    IDBObjectCollection folderCollection,
    List<long> folderIDs)
  {
    Dictionary<long, string> folderKeys = (Dictionary<long, string>) null;
    long[] array = folderIDs.ToArray();
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) array, LogicalOperators.AND, 0, false),
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.NotEmpty, (object) array, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      columnDescriptor1,
      columnDescriptor2
    });
    DataTable source = folderCollection.Select(paramSet);
    if (source != null && source.Rows.Count > 0)
      folderKeys = source.AsEnumerable().ToDictionary<DataRow, long, string>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0])), (System.Func<DataRow, string>) (y => Convert.ToString(y[1])));
    return folderKeys;
  }

  private List<long> GetInLoopID(
    IUserSession session,
    long selectedObjID,
    Dictionary<long, string> keys)
  {
    List<long> longList = new List<long>(keys.Count);
    string objectClassifKey = this.GetSelectedObjectClassifKey(session, selectedObjID);
    foreach (KeyValuePair<long, string> key in keys)
    {
      if (this.CheckLoop(objectClassifKey, key.Value))
        longList.Add(key.Key);
    }
    return longList.Count <= 0 ? (List<long>) null : longList;
  }

  private Dictionary<string, long> GetSubFolderKeys(
    IDBObjectCollection folderCollection,
    string classifKey)
  {
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKey, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      columnDescriptor1,
      columnDescriptor2
    });
    return folderCollection.Select(paramSet).AsEnumerable().ToDictionary<DataRow, string, long>((System.Func<DataRow, string>) (x => Convert.ToString(x[0])), (System.Func<DataRow, long>) (y => Convert.ToInt64(y[1])));
  }

  private Dictionary<long, List<long>> GroupedFolderByParentID(Dictionary<string, long> folders)
  {
    Dictionary<long, List<long>> dictionary = new Dictionary<long, List<long>>();
    string empty = string.Empty;
    for (int index = 1; index < folders.Count; ++index)
    {
      KeyValuePair<string, long> keyValuePair = folders.ElementAt<KeyValuePair<string, long>>(index);
      string key = keyValuePair.Key.Substring(0, keyValuePair.Key.Length - 2);
      if (folders.ContainsKey(key))
      {
        long folder = folders[key];
        if (dictionary.ContainsKey(folder))
          dictionary[folder].Add(keyValuePair.Value);
        else
          dictionary.Add(folder, new List<long>()
          {
            keyValuePair.Value
          });
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<long, List<long>>) null : dictionary;
  }

  private void PasteToFavorites(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    ImbaseClipboard imbClipboard)
  {
    if (selectedObj.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
    {
      List<long> folderIds = imbClipboard.FolderIDs;
      if (folderIds != null && folderIds.Count > 0)
        folderIds.ForEach((Action<long>) (x => ImbaseFavoritesCommands.AddToFavorites(session, selectedObj.ObjectID, x)));
      List<long> recordIds = imbClipboard.RecordIDs;
      if (recordIds != null && recordIds.Count > 0)
        recordIds.ForEach((Action<long>) (x => ImbaseFavoritesCommands.AddToFavorites(session, selectedObj.ObjectID, x)));
      List<long> linkIds = imbClipboard.LinkIDs;
      if (linkIds != null && linkIds.Count > 0)
        linkIds.ForEach((Action<long>) (x => ImbaseFavoritesCommands.AddToFavorites(session, selectedObj.ObjectID, x)));
    }
    List<long> favoritesIds = imbClipboard.FavoritesIDs;
    if (favoritesIds == null || favoritesIds.Count <= 0 || selectedObj.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID && selectedObj.ObjectType != Intermech.Imbase.Consts.ImbaseCatalogTypeID)
      return;
    this.PasteFavorites(session, selectedObj, favoritesIds);
  }

  private void PasteFavorites(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    List<long> objIds)
  {
    Dictionary<long, long> dictionary = new Dictionary<long, long>();
    IDBObjectCollection objectCollection1 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFavoritesTypeID);
    IDBRelationCollection favoritesRelcoll = session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseFavoritesRelationID);
    if (objectCollection1 == null || favoritesRelcoll == null)
      return;
    objIds.Where<long>((System.Func<long, bool>) (x => this.CheckLoopForFavorites(favoritesRelcoll, selectedObj.ObjectID, x))).ToList<long>().ForEach((Action<long>) (x => objIds.Remove(x)));
    if (objIds.Count == 0)
      return;
    bool flag = true;
    string caption = LocalizationHolder.rm.GetString("Imbase_Paste_Caption");
    switch (MessageBox.Show($"{LocalizationHolder.rm.GetString("Imbase_Paste")} {LocalizationHolder.rm.GetString("Imbase_CopyIncludedObject")}?", caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
    {
      case DialogResult.Cancel:
        return;
      case DialogResult.Yes:
        flag = false;
        break;
    }
    IDBObjectCollection objectCollection2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFavoritesTypeID);
    favoritesRelcoll = session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseFavoritesRelationID);
    if (favoritesRelcoll == null || objectCollection2 == null)
      return;
    Dictionary<long, List<long>> first = new Dictionary<long, List<long>>();
    if (!flag)
    {
      foreach (long objId in objIds)
      {
        Dictionary<long, List<long>> favoritesSubfolders = this.GetFavoritesSubfolders(favoritesRelcoll, objId);
        if (favoritesSubfolders != null)
          first = first.Concat<KeyValuePair<long, List<long>>>((IEnumerable<KeyValuePair<long, List<long>>>) favoritesSubfolders).ToDictionary<KeyValuePair<long, List<long>>, long, List<long>>((System.Func<KeyValuePair<long, List<long>>, long>) (x => x.Key), (System.Func<KeyValuePair<long, List<long>>, List<long>>) (y => y.Value));
      }
    }
    foreach (long objId in objIds)
    {
      Tuple<long, long> newFavoritesObject = this.CreateNewFavoritesObject(objectCollection2, favoritesRelcoll, selectedObj.ObjectID, objId, !flag);
      dictionary.Add(objId, newFavoritesObject.Item1);
      ImbaseFavoritesCommands.SendNotification(selectedObj.ObjectID, newFavoritesObject.Item1, newFavoritesObject.Item2, MetaDataHelper.GetRelationTypeID(Intermech.Imbase.Consts.ImbaseFavoritesRelationGUID));
    }
    foreach (KeyValuePair<long, List<long>> keyValuePair in first)
    {
      long parentId;
      if (dictionary.TryGetValue(keyValuePair.Key, out parentId))
      {
        foreach (long num in keyValuePair.Value)
        {
          Tuple<long, long> newFavoritesObject = this.CreateNewFavoritesObject(objectCollection2, favoritesRelcoll, parentId, num, !flag);
          dictionary.Add(num, newFavoritesObject.Item1);
        }
      }
    }
  }

  private Dictionary<long, List<long>> GetFavoritesSubfolders(
    IDBRelationCollection relcoll,
    long folderId)
  {
    relcoll.LocalTypesMode = true;
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) Intermech.Imbase.Consts.ImbaseFavoritesTypeID, LogicalOperators.NONE, 0, false)
    }, columns);
    return relcoll.ConsistFrom(paramSet, folderId, true).AsEnumerable().Select(x => new
    {
      ParentObjId = Convert.ToInt64(x[0]),
      ChildObjId = Convert.ToInt64(x[1])
    }).GroupBy(x => x.ParentObjId, x => x.ChildObjId).ToDictionary<IGrouping<long, long>, long, List<long>>((System.Func<IGrouping<long, long>, long>) (x => x.Key), (System.Func<IGrouping<long, long>, List<long>>) (x => x.ToList<long>()));
  }

  private bool CheckLoopForFavorites(
    IDBRelationCollection relColl,
    long parentObjId,
    long childObjId)
  {
    relColl.LocalTypesMode = true;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    return relColl.ConsistFrom(paramSet, childObjId, true).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToList<long>().Contains(parentObjId);
  }

  private Tuple<long, long> CreateNewFavoritesObject(
    IDBObjectCollection objCollection,
    IDBRelationCollection relCollection,
    long parentId,
    long prototypeObjId,
    bool needCopyRelations)
  {
    IDBObject dbObject = objCollection.Create(prototypeObjId);
    dbObject.GetAttributeByGuid(this.CodeImbaseAttrGuid)?.Delete(0L);
    dbObject.GetAttributeByGuid(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"))?.Delete(0L);
    IDBRelation dbRelation = relCollection.Create(parentId, dbObject.ObjectID);
    if (needCopyRelations)
    {
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      }, 0L, (object) null, -1);
      relCollection.ChildObjectTypes = (IList<int>) new int[3]
      {
        Intermech.Imbase.Consts.ImbaseFolderTypeID,
        Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID,
        Intermech.Imbase.Consts.ImbaseTableRefTypeID
      };
      DataTable dataTable = relCollection.ConsistFrom(paramSet, prototypeObjId);
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          relCollection.Create(dbObject.ObjectID, int64);
        }
      }
    }
    dbObject.CommitCreation(true, true);
    return new Tuple<long, long>(dbObject.ObjectID, dbRelation.RelationID);
  }

  private long GetCatalogIdByFavoritesId(IUserSession session, long folderId)
  {
    DataTable parentSostavData = DataHelper.GetParentSostavData(folderId, session, (IEnumerable<int>) new int[1]
    {
      Intermech.Imbase.Consts.ImbaseFavoritesRelationID
    }, true);
    return parentSostavData == null || parentSostavData.Rows.Count == 0 ? 0L : parentSostavData.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (r => Convert.ToInt32(r["F_OBJECT_TYPE"]) == Intermech.Imbase.Consts.ImbaseCatalogTypeID)).Select<DataRow, long>((System.Func<DataRow, long>) (r => Convert.ToInt64(r["F_OBJECT_ID"]))).FirstOrDefault<long>();
  }

  private void PasteRecords(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    ImbaseClipboard imbClipboard,
    Dictionary<long, long> newParentIDs,
    List<string> copiedFolders,
    Dictionary<string, long> folderKeys)
  {
    IDBObjectCollection recordCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
    if (recordCollection == null)
      return;
    IDBRelationCollection relCollection = (IDBRelationCollection) null;
    List<long> recordIds = imbClipboard.RecordIDs;
    if (recordIds != null)
    {
      relCollection = this.GetRelationCollection(session, relCollection, selectedObj.ObjectType, Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
      if (relCollection != null)
        recordIds.ForEach((Action<long>) (x => this.CreateNewObject(recordCollection, relCollection, selectedObj.ObjectID, x)));
    }
    if (newParentIDs == null)
      return;
    relCollection = this.GetRelationCollection(session, relCollection, Intermech.Imbase.Consts.ImbaseFolderTypeID, Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
    if (relCollection == null)
      return;
    Dictionary<long, long> first = new Dictionary<long, long>();
    foreach (string copiedFolder in copiedFolders)
    {
      Dictionary<long, long> idsWithParentIds = this.GetObjectIDsWithParentIDs(recordCollection, copiedFolder, folderKeys);
      if (idsWithParentIds != null)
        first = first.Concat<KeyValuePair<long, long>>((IEnumerable<KeyValuePair<long, long>>) idsWithParentIds).ToDictionary<KeyValuePair<long, long>, long, long>((System.Func<KeyValuePair<long, long>, long>) (x => x.Key), (System.Func<KeyValuePair<long, long>, long>) (y => y.Value));
    }
    if (first.Count <= 0)
      return;
    foreach (KeyValuePair<long, long> keyValuePair in first)
      this.CreateNewObject(recordCollection, relCollection, newParentIDs[keyValuePair.Value], keyValuePair.Key);
  }

  private List<long> PasteLinks(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    ImbaseClipboard imbClipboard,
    Dictionary<long, long> newParentIDs,
    List<string> copiedFolders,
    Dictionary<string, long> folderKeys,
    bool isCreateCopy)
  {
    return !isCreateCopy ? this.CreateTableRefWithNewTable(session, selectedObj, imbClipboard, newParentIDs, copiedFolders, folderKeys) : this.CreateTableRefWithOldTable(session, selectedObj, imbClipboard, newParentIDs, copiedFolders, folderKeys);
  }

  private List<long> CreateTableRefWithOldTable(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    ImbaseClipboard imbClipboard,
    Dictionary<long, long> newParentIDs,
    List<string> copiedFolders,
    Dictionary<string, long> folderKeys)
  {
    List<long> longList = new List<long>();
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection != null)
    {
      IDBRelationCollection relCollection = (IDBRelationCollection) null;
      List<long> linkIds = imbClipboard.LinkIDs;
      if (linkIds != null)
      {
        relCollection = this.GetRelationCollection(session, relCollection, selectedObj.ObjectType, Intermech.Imbase.Consts.ImbaseTableRefTypeID);
        if (relCollection != null)
        {
          foreach (long prototypeObjID in linkIds)
          {
            long newObject = this.CreateNewObject(objectCollection, relCollection, selectedObj.ObjectID, prototypeObjID);
            longList.Add(newObject);
          }
        }
      }
      if (newParentIDs != null)
      {
        IDBRelationCollection relationCollection = this.GetRelationCollection(session, relCollection, Intermech.Imbase.Consts.ImbaseFolderTypeID, Intermech.Imbase.Consts.ImbaseTableRefTypeID);
        if (relationCollection != null)
        {
          Dictionary<long, long> first = new Dictionary<long, long>();
          foreach (string copiedFolder in copiedFolders)
          {
            Dictionary<long, long> idsWithParentIds = this.GetObjectIDsWithParentIDs(objectCollection, copiedFolder, folderKeys);
            if (idsWithParentIds != null)
              first = first.Concat<KeyValuePair<long, long>>((IEnumerable<KeyValuePair<long, long>>) idsWithParentIds).ToDictionary<KeyValuePair<long, long>, long, long>((System.Func<KeyValuePair<long, long>, long>) (x => x.Key), (System.Func<KeyValuePair<long, long>, long>) (y => y.Value));
          }
          if (first.Count > 0)
          {
            foreach (KeyValuePair<long, long> keyValuePair in first)
            {
              long newObject = this.CreateNewObject(objectCollection, relationCollection, newParentIDs[keyValuePair.Value], keyValuePair.Key);
              longList.Add(newObject);
            }
          }
        }
      }
    }
    return longList.Count <= 0 ? (List<long>) null : longList;
  }

  private List<long> CreateTableRefWithNewTable(
    IUserSession session,
    IDBTypedObjectID selectedObj,
    ImbaseClipboard imbClipboard,
    Dictionary<long, long> newParentIDs,
    List<string> copiedFolders,
    Dictionary<string, long> folderKeys)
  {
    List<long> longList = new List<long>();
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection != null)
    {
      IDBRelationCollection relCollection = (IDBRelationCollection) null;
      List<long> linkIds = imbClipboard.LinkIDs;
      if (linkIds != null)
      {
        relCollection = this.GetRelationCollection(session, relCollection, selectedObj.ObjectType, Intermech.Imbase.Consts.ImbaseTableRefTypeID);
        if (relCollection != null)
        {
          Dictionary<long, List<long>> dictionary = this.GroupByTableID(objectCollection, linkIds);
          if (dictionary != null)
          {
            int relationTypeId = relCollection.RelationTypeID;
            foreach (KeyValuePair<long, List<long>> keyValuePair in dictionary)
            {
              long tableByPrototype = keyValuePair.Key != 0L ? TableLoadHelper.CreateTableByPrototype(session, keyValuePair.Key) : 0L;
              foreach (long prototypeTableRefID in keyValuePair.Value)
              {
                Tuple<long, long> tableRefByPrototype = TableLoadHelper.CreateTableRefByPrototype(session, selectedObj.ObjectID, prototypeTableRefID, relationTypeId, tableByPrototype);
                if (tableRefByPrototype != null)
                {
                  ImbaseCopyPasteProvider.DataForEvents.AddCreatedData(selectedObj.ObjectID, tableRefByPrototype.Item1, tableRefByPrototype.Item2, relationTypeId);
                  longList.Add(tableRefByPrototype.Item1);
                }
              }
            }
          }
        }
      }
      if (newParentIDs != null)
      {
        IDBRelationCollection relationCollection = this.GetRelationCollection(session, relCollection, Intermech.Imbase.Consts.ImbaseFolderTypeID, Intermech.Imbase.Consts.ImbaseTableRefTypeID);
        if (relationCollection != null)
        {
          Dictionary<long, Dictionary<long, long>> dictionary1 = new Dictionary<long, Dictionary<long, long>>();
          foreach (string copiedFolder in copiedFolders)
          {
            Dictionary<long, Dictionary<long, long>> dictionary2 = this.GroupByTableID(objectCollection, copiedFolder, folderKeys);
            if (dictionary2 != null)
            {
              foreach (KeyValuePair<long, Dictionary<long, long>> keyValuePair in dictionary2)
              {
                if (dictionary1.ContainsKey(keyValuePair.Key))
                  dictionary1[keyValuePair.Key] = dictionary1[keyValuePair.Key].Concat<KeyValuePair<long, long>>((IEnumerable<KeyValuePair<long, long>>) keyValuePair.Value).ToDictionary<KeyValuePair<long, long>, long, long>((System.Func<KeyValuePair<long, long>, long>) (x => x.Key), (System.Func<KeyValuePair<long, long>, long>) (y => y.Value));
                else
                  dictionary1.Add(keyValuePair.Key, keyValuePair.Value);
              }
            }
          }
          if (dictionary1.Count > 0)
          {
            int relationTypeId = relationCollection.RelationTypeID;
            foreach (KeyValuePair<long, Dictionary<long, long>> keyValuePair1 in dictionary1)
            {
              long tableByPrototype = keyValuePair1.Key != 0L ? TableLoadHelper.CreateTableByPrototype(session, keyValuePair1.Key) : 0L;
              foreach (KeyValuePair<long, long> keyValuePair2 in keyValuePair1.Value)
              {
                Tuple<long, long> tableRefByPrototype = TableLoadHelper.CreateTableRefByPrototype(session, newParentIDs[keyValuePair2.Value], keyValuePair2.Key, relationTypeId, tableByPrototype);
                if (tableRefByPrototype != null)
                {
                  ImbaseCopyPasteProvider.DataForEvents.AddCreatedData(selectedObj.ObjectID, tableRefByPrototype.Item1, tableRefByPrototype.Item2, relationTypeId);
                  longList.Add(tableRefByPrototype.Item1);
                }
              }
            }
          }
        }
      }
    }
    return longList.Count <= 0 ? (List<long>) null : longList;
  }

  private Dictionary<long, List<long>> GroupByTableID(
    IDBObjectCollection linkCollection,
    List<long> linkIDs)
  {
    Dictionary<long, List<long>> dictionary = new Dictionary<long, List<long>>(linkIDs.Count);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) linkIDs.ToArray(), LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable dataTable = linkCollection.Select(paramSet);
    List<long> longList = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long result = 0;
      if (!long.TryParse(Convert.ToString(row[1]), out result))
        result = 0L;
      if (result != 0L)
      {
        if (dictionary.ContainsKey(result))
          dictionary[result].Add(Convert.ToInt64(row[0]));
        else
          dictionary.Add(result, new List<long>()
          {
            Convert.ToInt64(row[0])
          });
      }
      else
        longList.Add(Convert.ToInt64(row[0]));
    }
    if (longList.Count > 0)
      dictionary.Add(0L, longList);
    return dictionary.Count <= 0 ? (Dictionary<long, List<long>>) null : dictionary;
  }

  private Dictionary<long, Dictionary<long, long>> GroupByTableID(
    IDBObjectCollection linkCollection,
    string classifKey,
    Dictionary<string, long> folderKeys)
  {
    Dictionary<long, Dictionary<long, long>> dictionary1 = new Dictionary<long, Dictionary<long, long>>();
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor3 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKey, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[3]
    {
      columnDescriptor1,
      columnDescriptor2,
      columnDescriptor3
    });
    DataTable dataTable = linkCollection.Select(paramSet);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      Dictionary<long, long> dictionary2 = new Dictionary<long, long>();
      string empty = string.Empty;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long result = 0;
        if (!long.TryParse(Convert.ToString(row[1]), out result))
          result = 0L;
        string str = Convert.ToString(row[2]);
        string key = str.Substring(0, str.Length - 2);
        if (folderKeys.ContainsKey(key))
        {
          long folderKey = folderKeys[key];
          if (result != 0L)
          {
            if (dictionary1.ContainsKey(result))
              dictionary1[result].Add(Convert.ToInt64(row[0]), folderKey);
            else
              dictionary1.Add(result, new Dictionary<long, long>()
              {
                {
                  Convert.ToInt64(row[0]),
                  folderKey
                }
              });
          }
          else
            dictionary2.Add(Convert.ToInt64(row[0]), folderKey);
        }
      }
      if (dictionary2.Count > 0)
        dictionary1.Add(0L, dictionary2);
    }
    return dictionary1.Count <= 0 ? (Dictionary<long, Dictionary<long, long>>) null : dictionary1;
  }

  private bool CanAddCommandCut(
    System.IServiceProvider services,
    ViewStateFlags stateFlags,
    IDBTypedObjectID selectedObj,
    IDBTypedObjectID parentObj)
  {
    bool flag = false;
    if (selectedObj.ObjectType != Intermech.Imbase.Consts.ImbaseCatalogTypeID && (stateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None && (stateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
    {
      int? objectType = parentObj?.ObjectType;
      int imbaseFavoritesTypeId = Intermech.Imbase.Consts.ImbaseFavoritesTypeID;
      flag = !(objectType.GetValueOrDefault() == imbaseFavoritesTypeId & objectType.HasValue) ? this.InImbaseTree(services) || this.InImbaseTableRefObjectTypeCategory(services) : this.InImbaseTree(services);
    }
    return flag;
  }

  private bool CanAddCommandCopy(
    System.IServiceProvider services,
    ViewStateFlags stateFlags,
    IDBTypedObjectID selectedObj,
    IDBTypedObjectID parentObj)
  {
    bool flag = false;
    if ((stateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None && (stateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
      flag = ((IEnumerable<int>) Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS).Contains<int>(selectedObj.ObjectType) || selectedObj.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID;
    return flag;
  }

  private ImbaseCopyPasteProvider.PasteFlag CanAddCommandPaste(
    System.IServiceProvider services,
    ViewStateFlags stateFlags,
    IDBTypedObjectID selectedObj)
  {
    ImbaseCopyPasteProvider.PasteFlag pasteFlag = ImbaseCopyPasteProvider.PasteFlag.None;
    if ((stateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None && (stateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None && this.InImbaseTree(services) && selectedObj.ObjectType != Intermech.Imbase.Consts.ImbaseTableRefTypeID && selectedObj.ObjectType != Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
    {
      ImbaseClipboard imbaseClipboard = this.ImbaseClipboard;
      if (imbaseClipboard != null)
      {
        if (imbaseClipboard.IsCut)
        {
          pasteFlag = selectedObj.ObjectType != Intermech.Imbase.Consts.ImbaseFavoritesTypeID || !imbaseClipboard.FromFavorites ? (selectedObj.ObjectType != Intermech.Imbase.Consts.ImbaseCatalogTypeID ? (imbaseClipboard.FavoritesIDs == null || imbaseClipboard.FavoritesIDs.Count <= 0 ? ImbaseCopyPasteProvider.PasteFlag.Simple : ImbaseCopyPasteProvider.PasteFlag.None) : ImbaseCopyPasteProvider.PasteFlag.Simple) : ImbaseCopyPasteProvider.PasteFlag.Simple;
        }
        else
        {
          List<long> linkIds = imbaseClipboard.LinkIDs;
          // ISSUE: explicit non-virtual call
          int count = linkIds != null ? __nonvirtual (linkIds.Count) : 0;
          pasteFlag = imbaseClipboard.HasFolder || count > 1 ? (selectedObj.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID ? ImbaseCopyPasteProvider.PasteFlag.Simple : ImbaseCopyPasteProvider.PasteFlag.Folder) : (count <= 0 ? (selectedObj.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID || selectedObj.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID || selectedObj.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID ? ImbaseCopyPasteProvider.PasteFlag.Simple : ImbaseCopyPasteProvider.PasteFlag.None) : (selectedObj.ObjectType == Intermech.Imbase.Consts.ImbaseFavoritesTypeID ? ImbaseCopyPasteProvider.PasteFlag.Simple : ImbaseCopyPasteProvider.PasteFlag.Link));
        }
      }
    }
    return pasteFlag;
  }

  private bool InImbaseTree(System.IServiceProvider viewServices)
  {
    return viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service && service.FocusedNode != null && service.FocusedNode.NodeID is Intermech.Navigator.DBObjects.NodeID;
  }

  private bool InImbaseTableRefObjectTypeCategory(System.IServiceProvider services)
  {
    return services.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service && service.FocusedNode != null && service.FocusedNode.NodeID is Intermech.Navigator.DBObjectTypes.Implementation.NodeID && service.FocusedNode.NodeID.CategoryID == 4 && service.FocusedNode.NodeID.TypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID;
  }

  private bool CheckLoop(string parentClassifKey, string childClassifKey)
  {
    bool flag = false;
    if (!string.IsNullOrEmpty(childClassifKey))
    {
      flag = parentClassifKey.StartsWith(childClassifKey);
      if (flag)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_CopyPaste_PasteInSelf_Msg"), LocalizationHolder.rm.GetString("Imbase_CopyPaste_PasteError_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
    return flag;
  }

  private bool CheckUniqueIndexes(
    IUserSession session,
    ImbaseClipboard imbClipboard,
    long newCatalogID,
    long prevCatalogID)
  {
    bool flag = true;
    QuickObjectInfo quickObjectInfo = session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService ? session.GetObjectInfo(newCatalogID) : throw new Exception(LocalizationHolder.rm.GetString(imbClipboard.IsCut ? "Imbase_Indexing_NullService_BreakMove" : "Imbase_Indexing_NullService_BreakCopy"));
    string caption = LocalizationHolder.rm.GetString("Imbase_Paste_Caption");
    List<long> linkIds = imbClipboard.LinkIDs;
    if (linkIds != null)
    {
      if (imbClipboard.IsCut)
      {
        if (prevCatalogID != newCatalogID)
        {
          if (linkIds.Count > 1 || imbClipboard.HasFolder)
          {
            IImbaseIndexingService imbaseIndexingService = customService;
            Guid sessionGuid = session.SessionGUID;
            List<long> catalogIDs = new List<long>();
            catalogIDs.Add(newCatalogID);
            string[] colsNames = new string[2]
            {
              IndexesField.F_ATTRIBUTE_ID,
              IndexesField.F_FLAG
            };
            if (imbaseIndexingService.GetUniqueIndexes(sessionGuid, catalogIDs, colsNames) != null)
              flag = MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase_CutPaste_NotUniqueDataInCatalog"), (object) quickObjectInfo.Caption), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
          }
          else if (linkIds.Count > 0)
          {
            List<long> longList = customService.CheckUniqueBeforeCopyMove(session.SessionGUID, newCatalogID, linkIds, false);
            if (longList != null)
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Paste_TableRefAttrID_NotUnique"), (object) string.Join<long>(", ", (IEnumerable<long>) longList.ToArray())));
          }
        }
      }
      else if (linkIds.Count > 1 || imbClipboard.HasFolder)
      {
        IImbaseIndexingService imbaseIndexingService = customService;
        Guid sessionGuid = session.SessionGUID;
        List<long> catalogIDs = new List<long>();
        catalogIDs.Add(newCatalogID);
        string[] colsNames = new string[2]
        {
          IndexesField.F_ATTRIBUTE_ID,
          IndexesField.F_FLAG
        };
        if (imbaseIndexingService.GetUniqueIndexes(sessionGuid, catalogIDs, colsNames) != null)
          flag = MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase_CopyPaste_NotUniqueDataInCatalog"), (object) quickObjectInfo.Caption), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
      }
      else if (linkIds.Count > 0)
      {
        if (prevCatalogID != newCatalogID)
        {
          List<long> longList = customService.CheckUniqueBeforeCopyMove(session.SessionGUID, newCatalogID, linkIds, true);
          if (longList != null)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Paste_TableRefAttrID_NotUnique"), (object) string.Join<long>(", ", (IEnumerable<long>) longList.ToArray())));
        }
        else
        {
          IImbaseIndexingService imbaseIndexingService = customService;
          Guid sessionGuid = session.SessionGUID;
          List<long> catalogIDs = new List<long>();
          catalogIDs.Add(newCatalogID);
          string[] colsNames = new string[2]
          {
            IndexesField.F_ATTRIBUTE_ID,
            IndexesField.F_FLAG
          };
          if (imbaseIndexingService.GetUniqueIndexes(sessionGuid, catalogIDs, colsNames) != null)
          {
            int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase_CopyInSameCatalog_NotUniqueDataInCatalog"), (object) quickObjectInfo.Caption), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            flag = false;
          }
        }
      }
    }
    return flag;
  }

  private ISelectionsService GetSelectionsService(IUserSession session)
  {
    return session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService ? customService : throw new Exception(LocalizationHolder.rm.GetString("Imbase_SelectionsService_IsNull"));
  }

  private string GetSelectedObjectClassifKey(IUserSession session, long objID)
  {
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(session, objID);
    return !string.IsNullOrEmpty(classifKeyByObjId) ? classifKeyByObjId : throw new Exception(LocalizationHolder.rm.GetString("Imbase_SelectedObject_ClassifKeyAttr_Empty"));
  }

  private IDBAttribute GetClassifKeyAttribute(IDBObject obj)
  {
    IDBAttribute classifKeyAttribute = obj.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    if (classifKeyAttribute == null)
      classifKeyAttribute = obj.Attributes.AddAttribute(Intermech.Imbase.Consts.ClassifFolderKeyAttId, false, new object[1]
      {
        (object) string.Empty
      });
    return classifKeyAttribute;
  }

  private void UpdateClassifKeyAttribute(
    IUserSession session,
    long objID,
    IDBAttribute classifKeyAttr,
    string newClassifKey)
  {
    classifKeyAttr.Value = (object) newClassifKey;
    if (objID >= 0L)
      return;
    IDBObject dbObject = session.GetObject(Math.Abs(objID), false);
    if (dbObject == null)
      return;
    classifKeyAttr = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    if (classifKeyAttr == null)
      return;
    classifKeyAttr.Value = (object) newClassifKey;
  }

  private void SetClassifKey4Level(
    IUserSession session,
    ISelectionsService selectionSrv,
    long parentObjID,
    int parentTypeID,
    string parentKey)
  {
    DBRecordSetParams rParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) parentObjID, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable dataTable = ImbaseHelper.SelectObjects(session, rParams, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
    string empty = string.Empty;
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(Convert.ToInt64(row[0]), false);
      if (objectActualCopy != null)
      {
        IDBAttribute classifKeyAttribute = this.GetClassifKeyAttribute(objectActualCopy);
        string nextClassifierKey = selectionSrv.GenerateNextClassifierKey((object) session.SessionGUID, parentTypeID, parentKey, objectActualCopy.ObjectType);
        this.UpdateClassifKeyAttribute(session, objectActualCopy.ObjectID, classifKeyAttribute, nextClassifierKey);
        if (objectActualCopy.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID)
          this.SetClassifKey4Level(session, selectionSrv, objectActualCopy.ObjectID, objectActualCopy.ObjectType, nextClassifierKey);
      }
    }
  }

  private IDBRelationCollection GetRelationCollection(
    IUserSession session,
    IDBRelationCollection relCollection,
    int parentTypeID,
    int childTypeID)
  {
    IDBRelationCollection relationCollection = (IDBRelationCollection) null;
    int defaultRelationTypeId = ImbaseHelper.GetDefaultRelationTypeID(parentTypeID, childTypeID);
    if (defaultRelationTypeId != -1)
    {
      if (relCollection != null && relCollection.RelationTypeID == defaultRelationTypeId)
      {
        relationCollection = relCollection;
      }
      else
      {
        relationCollection = session.GetRelationCollection(defaultRelationTypeId);
        if (relationCollection != null)
          relationCollection.ChildObjectTypes = (IList<int>) new List<int>()
          {
            childTypeID
          };
      }
    }
    return relationCollection;
  }

  private Dictionary<long, long> GetObjectIDsWithParentIDs(
    IDBObjectCollection collection,
    string classifKey,
    Dictionary<string, long> folders)
  {
    Dictionary<long, long> dictionary = new Dictionary<long, long>();
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKey, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      columnDescriptor1,
      columnDescriptor2
    });
    DataTable dataTable = collection.Select(paramSet);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      string empty = string.Empty;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        string str = Convert.ToString(row[0]);
        string key = str.Substring(0, str.Length - 2);
        if (folders.ContainsKey(key))
          dictionary.Add(Convert.ToInt64(row[1]), folders[key]);
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<long, long>) null : dictionary;
  }

  private long CreateNewObject(
    IDBObjectCollection objCollection,
    IDBRelationCollection relCollection,
    long parentID,
    long prototypeObjID)
  {
    IDBObject dbObject = objCollection.Create(prototypeObjID);
    dbObject.GetAttributeByGuid(this.CodeImbaseAttrGuid)?.Delete(0L);
    NewRelationProperties properties = new NewRelationProperties(parentID, dbObject.ID)
    {
      PartObjectID = dbObject.ObjectID
    };
    IDBRelation dbRelation = relCollection.Create(properties);
    dbObject.CommitCreation(true, true);
    ImbaseCopyPasteProvider.DataForEvents.AddCreatedData(parentID, dbObject.ObjectID, dbRelation.RelationID, dbRelation.RelationType);
    return dbObject.ObjectID;
  }

  private enum PasteFlag
  {
    None,
    Simple,
    Link,
    Folder,
  }

  private class DataForEvents
  {
    internal static List<long> CreatedParentIDs = new List<long>();
    internal static List<long> CreatedChildIDs = new List<long>();
    internal static List<long> CreatedRelIDs = new List<long>();
    internal static List<int> CreatedRelTypeIDs = new List<int>();
    internal static List<long> RemovedParentIDs = new List<long>();
    internal static List<long> RemovedRelIDs = new List<long>();
    internal static List<int> RemovedRelTypeIDs = new List<int>();

    internal static void AddCreatedData(
      long parentObjID,
      long childObjID,
      long relID,
      int relTypeID)
    {
      ImbaseCopyPasteProvider.DataForEvents.CreatedParentIDs.Add(parentObjID);
      ImbaseCopyPasteProvider.DataForEvents.CreatedChildIDs.Add(childObjID);
      ImbaseCopyPasteProvider.DataForEvents.CreatedRelIDs.Add(relID);
      ImbaseCopyPasteProvider.DataForEvents.CreatedRelTypeIDs.Add(relTypeID);
    }

    internal static void AddRemovedData(long parentObjID, long relID, int relTypeID)
    {
      ImbaseCopyPasteProvider.DataForEvents.RemovedParentIDs.Add(parentObjID);
      ImbaseCopyPasteProvider.DataForEvents.RemovedRelIDs.Add(relID);
      ImbaseCopyPasteProvider.DataForEvents.RemovedRelTypeIDs.Add(relTypeID);
    }

    internal static void Clear()
    {
      ImbaseCopyPasteProvider.DataForEvents.CreatedParentIDs.Clear();
      ImbaseCopyPasteProvider.DataForEvents.CreatedChildIDs.Clear();
      ImbaseCopyPasteProvider.DataForEvents.CreatedRelIDs.Clear();
      ImbaseCopyPasteProvider.DataForEvents.CreatedRelTypeIDs.Clear();
      ImbaseCopyPasteProvider.DataForEvents.RemovedParentIDs.Clear();
      ImbaseCopyPasteProvider.DataForEvents.RemovedRelIDs.Clear();
      ImbaseCopyPasteProvider.DataForEvents.RemovedRelTypeIDs.Clear();
    }
  }
}
