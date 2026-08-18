// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.TechcardClientControlsUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.HelperClasses.UIHelpers;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>Controls utils class</summary>
public static class TechcardClientControlsUtils
{
  /// <summary>Получение информации о связи из узла навигатора</summary>
  /// <param name="treeNode">Узел дерева навигатора</param>
  /// <param name="dbRelationId">Описание соотв. связи</param>
  /// <returns></returns>
  public static bool GetRelationInfo(NavigatorTreeNode treeNode, out IDBRelationID dbRelationId)
  {
    dbRelationId = (IDBRelationID) null;
    NavigatorTreeNode node;
    if ((node = treeNode) == null || node.Tree == null)
      return false;
    INode nodeHandler = node.Tree.GetNodeHandler(node);
    if (nodeHandler == null)
      return false;
    dbRelationId = nodeHandler.GetData(node.NodeID, typeof (IDBRelationID)) as IDBRelationID;
    return dbRelationId != null;
  }

  /// <summary>Получение информации об объекте из узла навигатора</summary>
  /// <param name="treeNode">Узел дерева навигатора</param>
  /// <param name="dbTypedObjId">Описание соотв. объекта</param>
  /// <returns></returns>
  public static bool GetObjectInfo(NavigatorTreeNode treeNode, out IDBTypedObjectID dbTypedObjId)
  {
    dbTypedObjId = (IDBTypedObjectID) null;
    NavigatorTreeNode node = treeNode;
    INode nodeHandler = node?.Tree?.GetNodeHandler(node);
    if (nodeHandler == null)
      return false;
    dbTypedObjId = nodeHandler.GetData(node.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    return dbTypedObjId != null;
  }

  /// <summary>Получение информации об объекте из узла навигатора</summary>
  /// <param name="treeNode">Узел дерева навигатора</param>
  /// <param name="objectId">Ид. версии объекта</param>
  /// <param name="objTypeId">Ид. типа объекта</param>
  /// <returns></returns>
  public static bool GetObjectInfo(
    NavigatorTreeNode treeNode,
    out long objectId,
    out int objTypeId)
  {
    objectId = 0L;
    objTypeId = -1;
    IDBTypedObjectID dbTypedObjId;
    if (!TechcardClientControlsUtils.GetObjectInfo(treeNode, out dbTypedObjId) || dbTypedObjId == null)
      return false;
    objectId = dbTypedObjId.ObjectID;
    objTypeId = dbTypedObjId.ObjectType;
    return true;
  }

  /// <summary>
  /// Получение информации об объекте, связи из узла навигатора
  /// </summary>
  /// <param name="treeNode">Нод дерева</param>
  /// <param name="dbTypedObjectId">Описание объекта</param>
  /// <param name="dbRelationId">Описание связи</param>
  /// <returns></returns>
  public static bool GetObjectInfo(
    NavigatorTreeNode treeNode,
    out IDBTypedObjectID dbTypedObjectId,
    out IDBRelationID dbRelationId)
  {
    return TechcardClientControlsUtils.GetObjectInfo(treeNode, out dbTypedObjectId, out dbRelationId, false);
  }

  /// <summary>Получение у узла навигатора типов связи, объекта</summary>
  /// <param name="treeNode">Нод дерева</param>
  /// <param name="dbTypedObjId">Описание объекта</param>
  /// <param name="dbRelId">Описание связи</param>
  /// <param name="needLoadFromBase">Признак загрузки информации из базы, если в дереве не найдено</param>
  /// <returns></returns>
  public static bool GetObjectInfo(
    NavigatorTreeNode treeNode,
    out IDBTypedObjectID dbTypedObjectId,
    out IDBRelationID dbRelationId,
    bool needLoadFromBase)
  {
    return TechcardClientControlsUtils.GetObjectInfo(treeNode, out dbTypedObjectId, out IDBTypedObjectID _, out dbRelationId, needLoadFromBase);
  }

  /// <summary>Получение у узла навигатора типов связи, объекта</summary>
  /// <param name="treeNode">Нод дерева</param>
  /// <param name="dbTypedObjectId">Описание объекта</param>
  /// <param name="dbRelationId">Описание связи</param>
  /// <param name="needLoadFromBase">Признак загрузки информации из базы, если в дереве не найдено</param>
  /// <returns></returns>
  public static bool GetObjectInfo(
    NavigatorTreeNode treeNode,
    out IDBTypedObjectID dbTypedObjectId,
    out IDBTypedObjectID projTypedObjectId,
    out IDBRelationID dbRelationId,
    bool needLoadFromBase)
  {
    dbRelationId = (IDBRelationID) null;
    dbTypedObjectId = (IDBTypedObjectID) null;
    projTypedObjectId = (IDBTypedObjectID) null;
    NavigatorTreeNode navigatorTreeNode;
    if (treeNode == null || (navigatorTreeNode = treeNode) == null || navigatorTreeNode.Tree == null)
      return false;
    INode nodeHandler1 = navigatorTreeNode.Tree.GetNodeHandler(treeNode);
    if (nodeHandler1 == null)
      return false;
    dbRelationId = nodeHandler1.GetData(navigatorTreeNode.NodeID, typeof (IDBRelationID)) as IDBRelationID;
    dbTypedObjectId = nodeHandler1.GetData(navigatorTreeNode.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (dbTypedObjectId == null)
    {
      if (!(nodeHandler1.GetData(navigatorTreeNode.NodeID, typeof (IDBObjectID)) is IDBObjectID data))
        return false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = needLoadFromBase ? sessionKeeper.Session.GetObject(data.Value, false) : (IDBObject) null;
        dbTypedObjectId = (IDBTypedObjectID) new DBTypedObjectID(dbObject != null ? dbObject.ObjectType : -1, data.Value, dbObject != null ? dbObject.ID : 0L, dbObject != null ? dbObject.Caption : string.Empty, dbObject != null ? dbObject.OwnerID : 0L, dbObject != null ? (long) dbObject.VersionID : 0L, dbObject != null ? Convert.ToInt64(dbObject.IsBaseVersion) : 0L, dbObject != null ? dbObject.SiteID : string.Empty, dbObject != null ? dbObject.ModificationID : 0L);
      }
    }
    INode nodeHandler2 = navigatorTreeNode.Parent != null ? navigatorTreeNode.Tree.GetNodeHandler(treeNode.Parent) : (INode) null;
    if (nodeHandler2 != null)
    {
      projTypedObjectId = nodeHandler2.GetData(navigatorTreeNode.Parent.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      if (projTypedObjectId == null && dbRelationId != null)
        projTypedObjectId = (IDBTypedObjectID) new DBTypedObjectID(-1, dbRelationId.ProjID, 0L, string.Empty, 0L, 0L, 0L, string.Empty, 0L);
    }
    return true;
  }

  /// <summary>
  /// Убедимся что выбранный (пока только один) элемент списка из дерева
  /// </summary>
  /// <param name="items"></param>
  /// <param name="treeView"></param>
  /// <returns></returns>
  public static bool IsSelectedItemsFromTree(ISelectedItems items, NavigatorTreeView treeView)
  {
    if (items == null || items.Count == 0 || treeView == null)
      return false;
    long num = 0;
    if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2)
      num = itemData2.ObjectID;
    else if (items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData1)
      num = itemData1.Value;
    if (num == 0L)
      return false;
    NavigatorTreeNode selectedNode = treeView.SelectedNodes[0];
    IDBTypedObjectID dbTypedObjectId;
    IDBRelationID dbRelationId;
    return selectedNode != null && TechcardClientControlsUtils.GetObjectInfo(selectedNode, out dbTypedObjectId, out dbRelationId) && dbRelationId != null && dbTypedObjectId != null && dbTypedObjectId.ObjectID == num;
  }

  /// <summary>
  /// Получение (поиск) информации об иерархии (применяемости) записей в текущем окне навигатора
  /// </summary>
  /// <param name="items"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="relObjInfoItems"></param>
  /// <returns></returns>
  public static bool GetItemsApplicabilityInfo(
    ISelectedItems items,
    IServiceProvider serviceProvider,
    out IEnumerable<RelObjInfoItem> relObjInfoItems)
  {
    relObjInfoItems = (IEnumerable<RelObjInfoItem>) null;
    if (!items.Any())
      return false;
    if (ServiceUtils.GetService<ICurrentNavWindow>((object) ApplicationServices.Container, false)?.TreeView is NavigatorTreeView treeView && TechcardClientControlsUtils.IsSelectedItemsFromTree(items, treeView))
    {
      TechRelObjInfoItemsFromSelectedItemApplicabilityProvider applicabilityProvider = new TechRelObjInfoItemsFromSelectedItemApplicabilityProvider(treeView.SelectedItems, treeView.Services);
      relObjInfoItems = applicabilityProvider.Execute();
      return relObjInfoItems != null && relObjInfoItems.Any<RelObjInfoItem>();
    }
    IViewState service = ServiceUtils.GetService<IViewState>((object) serviceProvider, false);
    if (service != null)
    {
      TechRelObjInfoItemsFromSelectedItemApplicabilityProvider applicabilityProvider = new TechRelObjInfoItemsFromSelectedItemApplicabilityProvider(items, serviceProvider);
      relObjInfoItems = applicabilityProvider.Execute();
      IEnumerable<RelObjInfoItem> source = relObjInfoItems;
      if ((source != null ? (source.Any<RelObjInfoItem>() ? 1 : 0) : 0) != 0 && service.ViewState.HasFlag((Enum) ViewStateFlags.NodeInViews))
      {
        IEnumerable<RelObjInfoItem> relObjInfoItems1;
        if (!TechcardClientControlsUtils.GetItemsApplicabilityInfo(ObjectExtensions.GetItems(relObjInfoItems.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (relItems => relItems.ProjInfo.ObjectID)).Distinct<long>().ToArray<long>()), (IServiceProvider) ApplicationServices.Container, out relObjInfoItems1))
          return true;
        List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>(relObjInfoItems);
        relObjInfoItemList.AddRange(relObjInfoItems1);
        relObjInfoItems = (IEnumerable<RelObjInfoItem>) relObjInfoItemList;
        return true;
      }
    }
    TechRelObjInfoItemsFromSelectedItemApplicabilityProvider applicabilityProvider1 = new TechRelObjInfoItemsFromSelectedItemApplicabilityProvider(items, serviceProvider);
    relObjInfoItems = applicabilityProvider1.Execute();
    IEnumerable<RelObjInfoItem> source1 = relObjInfoItems;
    return source1 != null && source1.Any<RelObjInfoItem>();
  }

  /// <summary>Получение "checked" элементов навигатора</summary>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  [Obsolete("Use ContextCommandHelper instead", true)]
  public static ISelectedItems GetCheckedItems(IServiceProvider viewServices)
  {
    return ContextCommandHelper.GetCheckedItems(viewServices);
  }

  /// <summary>Получение "checked" элементов навигатора</summary>
  /// <param name="viewServices"></param>
  /// <param name="defaultValue">Значение "по молчанию" для случая, когда "checked" узлов нет</param>
  /// <returns></returns>
  [Obsolete("Use ContextCommandHelper instead", true)]
  public static ISelectedItems GetCheckedItems(
    IServiceProvider viewServices,
    ISelectedItems defaultValue)
  {
    return ContextCommandHelper.GetCheckedItems(viewServices, defaultValue);
  }

  /// <summary>Получение "checked" элементов навигатора</summary>
  /// <param name="viewServices"></param>
  /// <param name="defaultValue">Значение "по молчанию" для случая, когда "checked" узлов нет</param>
  /// <param name="minObjCount">Мин. допустимое количество "checked" узлов</param>
  /// <returns></returns>
  [Obsolete("Use ContextCommandHelper instead", true)]
  public static ISelectedItems GetCheckedItems(
    IServiceProvider viewServices,
    ISelectedItems defaultValue,
    int minObjCount)
  {
    return ContextCommandHelper.GetCheckedItems(viewServices, defaultValue, minObjCount);
  }

  /// <summary>Конвертация лога сессии в cписок cобытий</summary>
  /// <param name="categoryList"></param>
  /// <returns></returns>
  public static IEnumerable<NotificationEventArgs> GetNotificationEvents(
    IList<CategoryValue> categoryList)
  {
    if (categoryList == null)
      throw new ArgumentNullException(nameof (categoryList));
    NotificationQueue notificationQueue = new NotificationQueue();
    List<ObjInfoItem> aList1 = new List<ObjInfoItem>();
    List<RelObjInfoItem> aList2 = new List<RelObjInfoItem>();
    List<ObjInfoItem> bList = new List<ObjInfoItem>();
    List<RelObjInfoItem> relObjInfoItemList1 = new List<RelObjInfoItem>();
    List<ObjInfoItem> aList3 = new List<ObjInfoItem>();
    List<ObjInfoItem> source1 = new List<ObjInfoItem>();
    List<ObjInfoItem> objInfoItemList1 = new List<ObjInfoItem>();
    List<ObjInfoItem> aList4 = new List<ObjInfoItem>();
    List<RelObjInfoItem> aList5 = new List<RelObjInfoItem>();
    int index = 0;
    while (index < categoryList.Count)
    {
      CategoryValue category1 = categoryList[index];
      ModificationEvent modificationEvent1 = category1 as ModificationEvent;
      RelationModificationEvent modificationEvent2 = category1 as RelationModificationEvent;
      ++index;
      if (category1.ActionID == ActionType.CheckIn && category1.CategoryType == 1)
        aList3.Add(new ObjInfoItem(category1.CategoryID, modificationEvent1 != null ? modificationEvent1.MetadataTypeID : -1));
      else if (category1.ActionID == ActionType.CheckOut && category1.CategoryType == 1)
      {
        CategoryValue category2 = categoryList[index];
        source1.Add(new ObjInfoItem(category1.CategoryID));
        objInfoItemList1.Add(new ObjInfoItem(category2.CategoryID));
        ++index;
      }
      else if (category1.ActionID == ActionType.Create && category1.CategoryType == 1 || category1.ActionID == ActionType.CreateChildItem && category1.CategoryType == 1)
      {
        if (category1.CategoryID > 0L)
          aList1.Add(new ObjInfoItem(Math.Abs(category1.CategoryID), modificationEvent1 != null ? modificationEvent1.MetadataTypeID : -1));
      }
      else if (category1.ActionID == ActionType.AddLink && category1.CategoryType == 1 || category1.ActionID == ActionType.Create && category1.CategoryType == 5)
        aList2.Add(new RelObjInfoItem(category1.CategoryID, modificationEvent1 != null ? modificationEvent1.MetadataTypeID : -1)
        {
          ProjInfo = modificationEvent2 != null ? new ObjInfoItem(modificationEvent2.ProjID) : (ObjInfoItem) null
        });
      else if (category1.ActionID == ActionType.Delete && category1.CategoryType == 1 || category1.ActionID == ActionType.Purge && category1.CategoryType == 1)
        bList.Add(new ObjInfoItem(category1.CategoryID, modificationEvent1 != null ? modificationEvent1.MetadataTypeID : -1));
      else if (category1.ActionID == ActionType.DeleteLink && category1.CategoryType == 1 || category1.ActionID == ActionType.DeleteLink && category1.CategoryType == 5 || category1.ActionID == ActionType.Purge && category1.CategoryType == 5 || category1.ActionID == ActionType.Delete && category1.CategoryType == 5)
        relObjInfoItemList1.Add(new RelObjInfoItem(category1.CategoryID, modificationEvent1 != null ? modificationEvent1.MetadataTypeID : -1)
        {
          ProjInfo = modificationEvent2 != null ? new ObjInfoItem(modificationEvent2.ProjID) : (ObjInfoItem) null
        });
      else if ((category1.ActionID == ActionType.Edit || category1.ActionID == ActionType.EditProperties) && category1.CategoryType == 1)
        aList4.Add(new ObjInfoItem(category1.CategoryID, modificationEvent1 != null ? modificationEvent1.MetadataTypeID : -1));
      else if (category1.ActionID == ActionType.EditLink || category1.ActionID == ActionType.EditProperties && category1.CategoryType == 5)
        aList5.Add(new RelObjInfoItem(category1.CategoryID, modificationEvent1 != null ? modificationEvent1.MetadataTypeID : -1)
        {
          ProjInfo = modificationEvent2 != null ? new ObjInfoItem(modificationEvent2.ProjID) : (ObjInfoItem) null
        });
    }
    List<ObjInfoItem> resultData1;
    GenericListHelper.GetDifference<ObjInfoItem>((IList<ObjInfoItem>) aList1, (IList<ObjInfoItem>) bList, GenericListHelper.SearchMode.smNotExistInB, out resultData1);
    List<ObjInfoItem> objInfoItemList2 = resultData1;
    List<RelObjInfoItem> resultData2;
    GenericListHelper.GetDifference<RelObjInfoItem>((IList<RelObjInfoItem>) aList2, (IList<RelObjInfoItem>) relObjInfoItemList1, GenericListHelper.SearchMode.smNotExistInB, out resultData2);
    List<RelObjInfoItem> relObjInfoItemList2 = resultData2;
    GenericListHelper.GetDifference<ObjInfoItem>((IList<ObjInfoItem>) aList3, (IList<ObjInfoItem>) bList, GenericListHelper.SearchMode.smNotExistInB, out resultData1);
    List<ObjInfoItem> objInfoItemList3 = resultData1;
    GenericListHelper.GetDifference<ObjInfoItem>((IList<ObjInfoItem>) aList4, (IList<ObjInfoItem>) objInfoItemList2, GenericListHelper.SearchMode.smNotExistInB, out resultData1);
    GenericListHelper.GetDifference<ObjInfoItem>((IList<ObjInfoItem>) resultData1, (IList<ObjInfoItem>) bList, GenericListHelper.SearchMode.smNotExistInB, out resultData1);
    GenericListHelper.GetDifference<ObjInfoItem>((IList<ObjInfoItem>) resultData1, (IList<ObjInfoItem>) objInfoItemList3, GenericListHelper.SearchMode.smNotExistInB, out resultData1);
    GenericListHelper.GetDifference<ObjInfoItem>((IList<ObjInfoItem>) resultData1, (IList<ObjInfoItem>) objInfoItemList1, GenericListHelper.SearchMode.smNotExistInB, out resultData1);
    List<ObjInfoItem> objInfoItemList4 = resultData1;
    GenericListHelper.GetDifference<RelObjInfoItem>((IList<RelObjInfoItem>) aList5, (IList<RelObjInfoItem>) relObjInfoItemList2, GenericListHelper.SearchMode.smNotExistInB, out resultData2);
    GenericListHelper.GetDifference<RelObjInfoItem>((IList<RelObjInfoItem>) resultData2, (IList<RelObjInfoItem>) relObjInfoItemList1, GenericListHelper.SearchMode.smNotExistInB, out resultData2);
    List<RelObjInfoItem> relObjInfoItemList3 = resultData2;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<RelObjInfoItem> source2 = new List<RelObjInfoItem>();
      source2.AddRange((IEnumerable<RelObjInfoItem>) relObjInfoItemList2);
      source2.AddRange((IEnumerable<RelObjInfoItem>) relObjInfoItemList1);
      source2.AddRange((IEnumerable<RelObjInfoItem>) relObjInfoItemList3);
      foreach (RelObjInfoItem relObjInfoItem in source2)
      {
        if (relObjInfoItem.RelTypeID == -1 || !((TypedInfoItem) relObjInfoItem.ProjInfo != (TypedInfoItem) null))
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(relObjInfoItem.RelationID, false);
          if (relation == null)
          {
            relObjInfoItem.ProjInfo = relObjInfoItem.ProjInfo ?? new ObjInfoItem();
          }
          else
          {
            relObjInfoItem.RelTypeID = relation.RelationType;
            relObjInfoItem.ProjInfo = relObjInfoItem.ProjInfo ?? new ObjInfoItem(relation.ProjID);
          }
        }
      }
      List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
      objInfoList.AddRange((IEnumerable<ObjInfoItem>) objInfoItemList2);
      objInfoList.AddRange((IEnumerable<ObjInfoItem>) objInfoItemList3);
      objInfoList.AddRange((IEnumerable<ObjInfoItem>) objInfoItemList4);
      objInfoList.AddRange(source2.Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)));
      ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objInfoList, sessionKeeper.Session);
    }
    if (relObjInfoItemList1.Count > 0)
      notificationQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) relObjInfoItemList1.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) relObjInfoItemList1.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToList<long>(), (IList<int>) relObjInfoItemList1.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.ProjInfo.ObjTypeID)).ToList<int>(), (IList<int>) relObjInfoItemList1.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToList<int>()));
    if (objInfoItemList2.Count > 0)
      notificationQueue.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) objInfoItemList2.Select<ObjInfoItem, long>((Func<ObjInfoItem, long>) (item => item.ObjectID)).ToList<long>(), (IList<int>) objInfoItemList2.Select<ObjInfoItem, int>((Func<ObjInfoItem, int>) (item => item.ObjTypeID)).ToList<int>()));
    if (relObjInfoItemList2.Count > 0)
      notificationQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relObjInfoItemList2.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) relObjInfoItemList2.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToList<long>(), (IList<int>) relObjInfoItemList2.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.ProjInfo.ObjTypeID)).ToList<int>(), (IList<int>) relObjInfoItemList2.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToList<int>()));
    if (source1.Count > 0)
      notificationQueue.QueueEvent((NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) source1.Select<ObjInfoItem, long>((Func<ObjInfoItem, long>) (item => item.ObjectID)).ToList<long>(), (IList<long>) objInfoItemList1.Select<ObjInfoItem, long>((Func<ObjInfoItem, long>) (item => item.ObjectID)).ToList<long>()));
    if (objInfoItemList3.Count > 0)
      notificationQueue.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objInfoItemList3.Select<ObjInfoItem, long>((Func<ObjInfoItem, long>) (item => item.ObjectID)).ToList<long>(), (IList<int>) objInfoItemList3.Select<ObjInfoItem, int>((Func<ObjInfoItem, int>) (item => item.ObjTypeID)).ToList<int>()));
    if (objInfoItemList4.Count != 0)
      notificationQueue.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objInfoItemList4.Select<ObjInfoItem, long>((Func<ObjInfoItem, long>) (item => item.ObjectID)).ToList<long>(), (IList<int>) objInfoItemList4.Select<ObjInfoItem, int>((Func<ObjInfoItem, int>) (item => item.ObjTypeID)).ToList<int>(), true));
    if (relObjInfoItemList3.Count != 0)
      notificationQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", (IList<long>) relObjInfoItemList3.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) relObjInfoItemList3.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToList<long>(), (IList<int>) relObjInfoItemList3.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.ProjInfo.ObjTypeID)).ToList<int>(), (IList<int>) relObjInfoItemList3.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToList<int>()));
    return notificationQueue.ToEnumerable();
  }

  /// <summary>
  /// Уведомление навигатора об изменения объектов / связей в рамках тек. сессии
  /// </summary>
  /// <param name="service"></param>
  /// <param name="categoryList"></param>
  /// <param name="sender"></param>
  public static void FireNotificationEvents(
    INotificationService service,
    IEnumerable<CategoryValue> categoryList,
    object sender)
  {
    if (service == null || categoryList == null)
      return;
    foreach (NotificationEventArgs notificationEvent in TechcardClientControlsUtils.GetNotificationEvents((IList<CategoryValue>) categoryList.ToList<CategoryValue>()))
      service.FireEvent(sender, notificationEvent);
  }
}
