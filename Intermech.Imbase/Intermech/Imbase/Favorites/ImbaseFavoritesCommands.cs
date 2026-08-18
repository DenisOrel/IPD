// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Favorites.ImbaseFavoritesCommands
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Imbase.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Favorites;

public class ImbaseFavoritesCommands
{
  private static long _findInTreeObjId;

  public static void FindInImbaseTreeCommand(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null || items.Count == 0)
      return;
    ImbaseFavoritesCommands.RestoreFocusNode(viewservices);
    NavigatorTreeView service = viewservices.GetService<NavigatorTreeView>();
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
    {
      ImbaseFavoritesCommands._findInTreeObjId = 0L;
    }
    else
    {
      ImbaseFavoritesCommands._findInTreeObjId = itemData.ObjectID;
      NodeIDPath parentPath = items.GetParentPath(0);
      for (int Index = parentPath.Length - 1; Index >= 0; --Index)
      {
        NodeID nodeId = (NodeID) parentPath[Index];
        if (nodeId == null || nodeId.ObjectTypeID != Intermech.Imbase.Consts.ImbaseCatalogTypeID)
          parentPath.RemoveLast();
        else
          break;
      }
      if (parentPath.Length > 0)
      {
        NavigatorTreeNode lastNode = (NavigatorTreeNode) null;
        if (!service.TryFind(parentPath, out lastNode))
          lastNode = (NavigatorTreeNode) null;
        NavigatorTreeNode firstNode = service.FindFirstNode(new System.Func<NavigatorTreeNode, bool>(ImbaseFavoritesCommands.FindNode), new System.Func<NavigatorTreeNode, bool>(ImbaseFavoritesCommands.FindInChildsNode), true, lastNode);
        if (firstNode == null || firstNode.HasFocus)
          return;
        NodeIDPath nodeIdPath = service.GetNodeIDPath(firstNode);
        service.TryBrowse(nodeIdPath);
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(sessionKeeper.Session, ImbaseFavoritesCommands._findInTreeObjId);
          long classificatorKey = ImbaseFavoritesCommands.GetCatalogIdByClassificatorKey(sessionKeeper.Session, classifKeyByObjId);
          if (classificatorKey == 0L)
            return;
          NodeIDPath path = new NodeIDPath((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(classificatorKey));
          NavigatorTreeView treeView = Utils.OpenNewWindow(path.RootDescriptor, viewservices, new GetSupportedColumnsEventHandler(Utils.DefaultSupportedColumnsObjects), path).TreeView;
          NavigatorTreeNode firstNode = treeView.FindFirstNode(new System.Func<NavigatorTreeNode, bool>(ImbaseFavoritesCommands.FindNode), new System.Func<NavigatorTreeNode, bool>(ImbaseFavoritesCommands.FindInChildsNode), true);
          if (firstNode == null || firstNode.HasFocus)
            return;
          NodeIDPath nodeIdPath = treeView.GetNodeIDPath(firstNode);
          treeView.TryBrowse(nodeIdPath);
        }
      }
    }
  }

  private static bool FindNode(NavigatorTreeNode treeNode)
  {
    NodeID nodeId = (NodeID) treeNode.NodeID;
    return nodeId != null && nodeId.ObjectID == ImbaseFavoritesCommands._findInTreeObjId && nodeId.RelationTypeID == Intermech.Imbase.Consts.ImbaseDefaultLinkID;
  }

  private static bool FindInChildsNode(NavigatorTreeNode treeNode)
  {
    NodeID nodeId = (NodeID) treeNode.NodeID;
    return nodeId != null && (nodeId.RelationTypeID == Intermech.Imbase.Consts.ImbaseDefaultLinkID || nodeId.RelationTypeID == -1);
  }

  public static void RemoveFromFavoritesCommand(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null || items.Count == 0)
      return;
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBTypedObjectID parentData = items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (itemData == null || parentData == null)
      return;
    ImbaseFavoritesCommands.RemoveFromFavoritesCommand(itemData.ObjectID, parentData.ObjectID);
  }

  public static void RemoveFromFavoritesCommand(long objId, long parentId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(parentId, objId, Intermech.Imbase.Consts.ImbaseFavoritesRelationID, true);
      if (relation == null)
        return;
      relation.Delete(0L);
      if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
        return;
      service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", relation.RelationID));
    }
  }

  public static void AddToFavoritesCommand(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    ImbaseFavoritesCommands.AddToFavoritesCommand(new long[1]
    {
      itemData.ObjectID
    });
  }

  public static void AddToFavoritesCommand(long[] objectIds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectId in objectIds)
      {
        string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(sessionKeeper.Session, objectId);
        long classificatorKey = ImbaseFavoritesCommands.GetCatalogIdByClassificatorKey(sessionKeeper.Session, classifKeyByObjId);
        if (classificatorKey == 0L || !(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
          break;
        Guid sessionGuid = sessionKeeper.Session.SessionGUID;
        long[] catalogIds = new long[1]{ classificatorKey };
        DataTable foldersForCatalogs = customService.GetFavoriteFoldersForCatalogs(sessionGuid, catalogIds, false);
        switch (foldersForCatalogs != null ? foldersForCatalogs.Rows.Count : 0)
        {
          case 0:
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFavoritesTypeID);
            IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseFavoritesRelationID);
            IDBObject dbObject = objectCollection.Create();
            IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false);
            if (dbAttribute != null)
              dbAttribute.AsString = LocalizationHolder.rm.GetString("Favorites");
            IDBRelation dbRelation = relationCollection.Create(classificatorKey, dbObject.ObjectID);
            dbObject.CommitCreation(true);
            if (dbRelation != null)
              ImbaseFavoritesCommands.SendNotification(classificatorKey, dbObject.ObjectID, dbRelation.RelationID, dbRelation.RelationType);
            ImbaseFavoritesCommands.AddToFavorites(sessionKeeper.Session, dbObject.ObjectID, objectId);
            break;
          case 1:
            long int64 = Convert.ToInt64(foldersForCatalogs?.Rows[0][0]);
            ImbaseFavoritesCommands.AddToFavorites(sessionKeeper.Session, int64, objectId);
            break;
          default:
            Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new SelectedFavoritesAnalizer(), true);
            SelectionOptions options = SelectionOptions.SelectObjects | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect;
            long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects("Укажите папку избранное", "", (IDescriptor) new ImbaseCatalogFavoritesDescriptor(classificatorKey), options);
            if (numArray != null && numArray.Length == 1)
            {
              ImbaseFavoritesCommands.AddToFavorites(sessionKeeper.Session, numArray[0], objectId);
              break;
            }
            break;
        }
      }
    }
  }

  public static long AddToFavorites(IUserSession session, long favoritesObjId, long includeObjId)
  {
    if (!(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
      return 0;
    Guid sessionGuid = session.SessionGUID;
    long parentId1 = includeObjId;
    int[] addTypes = new int[3]
    {
      Intermech.Imbase.Consts.ImbaseFolderTypeID,
      Intermech.Imbase.Consts.ImbaseTableRefTypeID,
      Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID
    };
    DataTable subfolders = customService.GetSubfolders(sessionGuid, parentId1, addTypes);
    Tuple<long, long> tuple;
    if ((subfolders == null ? 0 : (subfolders.Rows.Count > 0 ? 1 : 0)) != 0)
    {
      bool flag = false;
      string caption = LocalizationHolder.rm.GetString("Favorites");
      switch (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase_Create_Favorites_By_Prototype"), (object) session.GetObject(includeObjId).Caption), caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
      {
        case DialogResult.Cancel:
          return 0;
        case DialogResult.Yes:
          flag = true;
          break;
      }
      IDBRelationCollection relationCollection1 = session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseFavoritesRelationID);
      if (flag)
      {
        IDBObjectCollection objectCollection1 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFavoritesTypeID);
        IDBObjectCollection objectCollection2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
        IDBRelationCollection relationCollection2 = session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseDefaultLinkID);
        Dictionary<long, long> dictionary1 = new Dictionary<long, long>();
        string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(session, includeObjId);
        if (classifKeyByObjId == string.Empty)
          return 0;
        Dictionary<long, List<long>> dictionary2 = ImbaseFavoritesCommands.GroupedFolderByParentId(ImbaseFavoritesCommands.GetSubFolderKeys(objectCollection2, classifKeyByObjId));
        tuple = ImbaseFavoritesCommands.CreateFavoritesObjectByPrototype(objectCollection1, relationCollection1, relationCollection2, favoritesObjId, includeObjId);
        dictionary1.Add(includeObjId, tuple.Item1);
        foreach (KeyValuePair<long, List<long>> keyValuePair in dictionary2)
        {
          long parentId2;
          if (dictionary1.TryGetValue(keyValuePair.Key, out parentId2))
          {
            foreach (long num in keyValuePair.Value)
            {
              Tuple<long, long> objectByPrototype = ImbaseFavoritesCommands.CreateFavoritesObjectByPrototype(objectCollection1, relationCollection1, relationCollection2, parentId2, num);
              dictionary1.Add(num, objectByPrototype.Item1);
            }
          }
        }
      }
      else
      {
        IDBRelation dbRelation = relationCollection1.Create(favoritesObjId, includeObjId);
        tuple = new Tuple<long, long>(includeObjId, dbRelation.RelationID);
      }
    }
    else
    {
      IDBRelation dbRelation = session.GetRelationCollection(Intermech.Imbase.Consts.ImbaseFavoritesRelationID).Create(favoritesObjId, includeObjId);
      tuple = new Tuple<long, long>(includeObjId, dbRelation.RelationID);
    }
    ImbaseFavoritesCommands.SendNotification(favoritesObjId, tuple.Item1, tuple.Item2, MetaDataHelper.GetRelationTypeID(Intermech.Imbase.Consts.ImbaseFavoritesRelationGUID));
    return tuple.Item1;
  }

  public static Dictionary<string, long> GetSubFolderKeys(
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

  private static Tuple<long, long> CreateFavoritesObjectByPrototype(
    IDBObjectCollection favoritesObjCollection,
    IDBRelationCollection favoritesRelColl,
    IDBRelationCollection simpleWithSortrelColl,
    long parentId,
    long prototypeObjId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(prototypeObjId, true);
      IDBObject dbObject2 = favoritesObjCollection.Create();
      dbObject2.Attributes.AssignPossibleAttributes(dbObject1.Attributes, 0);
      dbObject2.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"))?.Delete(0L);
      dbObject2.GetAttributeByGuid(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"))?.Delete(0L);
      IDBRelation dbRelation = favoritesRelColl.Create(parentId, dbObject2.ObjectID);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      }, 0L, (object) null, -1);
      simpleWithSortrelColl.ChildObjectTypes = (IList<int>) new int[2]
      {
        Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID,
        Intermech.Imbase.Consts.ImbaseTableRefTypeID
      };
      DataTable dataTable = simpleWithSortrelColl.ConsistFrom(paramSet, prototypeObjId);
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          favoritesRelColl.Create(dbObject2.ObjectID, int64);
        }
      }
      dbObject2.CommitCreation(true, true);
      return new Tuple<long, long>(dbObject2.ObjectID, dbRelation.RelationID);
    }
  }

  public static Dictionary<long, List<long>> GroupedFolderByParentId(
    Dictionary<string, long> folders)
  {
    Dictionary<long, List<long>> dictionary = new Dictionary<long, List<long>>();
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

  private static long GetCatalogIdByClassificatorKey(IUserSession session, string classificatorKey)
  {
    long classificatorKey1 = 0;
    if (classificatorKey == string.Empty || classificatorKey.Length < 2)
      return classificatorKey1;
    string conditionValue = classificatorKey.Substring(0, 2);
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    object[] columns = new object[1]{ (object) -2 };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0)
    }, columns);
    DataTable dataTable = objectCollection.Select(paramSet);
    return dataTable.Rows.Count != 1 ? classificatorKey1 : Convert.ToInt64(dataTable.Rows[0][0]);
  }

  public static void SendNotification(
    long parentsId,
    long objectsId,
    long relationsId,
    int relationsTypesId)
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service) || objectsId == 0L || objectsId == -1L)
      return;
    service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectsId));
    service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", relationsId, parentsId, relationsTypesId));
  }

  private static void RestoreFocusNode(System.IServiceProvider viewServices, bool canRestore = false)
  {
    if (!(viewServices?.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service))
      return;
    service.CanRestoreFocusedNode = canRestore;
  }
}
