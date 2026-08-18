
// Type: Intermech.Search.UI.NotificationHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.UI;

public static class NotificationHelper
{
  /// <summary>
  /// Получение очереди сообщений для списка категорий (событий)
  /// </summary>
  /// <param name="categoryValues"></param>
  /// <returns></returns>
  public static INotificationQueue GetQueue(
    IList<CategoryValue> categoryValues,
    NavigatorRelationCommand navigatorRelationCommand = NavigatorRelationCommand.Unknown)
  {
    if (categoryValues == null)
      throw new ArgumentNullException(nameof (categoryValues));
    NotificationQueue queue = new NotificationQueue();
    List<long> longList1 = new List<long>();
    List<long> longList2 = new List<long>();
    List<long> longList3 = new List<long>();
    List<RelObjInfoItem> relObjInfoItemList1 = new List<RelObjInfoItem>();
    List<RelObjInfoItem> relObjInfoItemList2 = new List<RelObjInfoItem>();
    List<RelObjInfoItem> source = new List<RelObjInfoItem>();
    for (int index1 = 0; index1 < categoryValues.Count; ++index1)
    {
      ModificationEvent categoryValue1 = categoryValues[index1] as ModificationEvent;
      RelationModificationEvent categoryValue2 = categoryValues[index1] as RelationModificationEvent;
      if (index1 + 1 < categoryValues.Count && categoryValues[index1].ActionID == ActionType.CheckOut && categoryValues[index1].CategoryType == 1 && categoryValues[index1 + 1].ActionID == ActionType.CheckOut && categoryValues[index1 + 1].CategoryType == 1)
      {
        long categoryId1 = categoryValues[index1].CategoryID;
        long categoryId2 = categoryValues[index1 + 1].CategoryID;
        if (longList3.Contains(categoryId1))
          longList3.Remove(categoryId1);
        if (!longList1.Contains(categoryId1))
          longList1.Add(categoryId1);
        if (!longList2.Contains(categoryId2))
          longList2.Add(categoryId2);
        DBObjectsCheckOutEventArgs args = new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new List<long>()
        {
          categoryId1
        }, (IList<long>) new List<long>() { categoryId2 });
        queue.QueueEvent((NotificationEventArgs) args);
        ++index1;
      }
      if (categoryValues[index1].CategoryType == 1 && categoryValues[index1].ActionID == ActionType.Create)
      {
        int objectTypeID = -1;
        if (categoryValue1 != null)
          objectTypeID = categoryValue1.MetadataTypeID;
        if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(categoryValues[index1].CategoryID, false);
            if (dbObject != null)
              objectTypeID = dbObject.TypeID;
          }
        }
        queue.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", categoryValues[index1].CategoryID, objectTypeID));
      }
      if (categoryValues[index1].CategoryType == 1 && categoryValues[index1].ActionID == ActionType.Edit)
      {
        if (categoryValues[index1] is ModificationEvent)
          queue.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", categoryValues[index1].CategoryID, ((ModificationEvent) categoryValues[index1]).MetadataTypeID));
        else
          queue.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", categoryValues[index1].CategoryID));
      }
      if (categoryValues[index1].CategoryType == 1 && (categoryValues[index1].ActionID == ActionType.Delete || categoryValues[index1].ActionID == ActionType.Purge))
        queue.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", categoryValues[index1].CategoryID));
      if (categoryValues[index1].ActionID == ActionType.CheckIn && categoryValues[index1].CategoryType == 1)
      {
        long categoryId = categoryValues[index1].CategoryID;
        if (longList2.Contains(categoryId))
        {
          int index2 = longList2.IndexOf(categoryId);
          longList1.RemoveAt(index2);
          longList2.Remove(categoryId);
        }
        else
          longList3.Add(categoryId);
        DBObjectsEventArgs args = new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) new List<long>()
        {
          categoryId
        });
        queue.QueueEvent((NotificationEventArgs) args);
        ++index1;
      }
      if (categoryValues[index1].CategoryType == 5 && (categoryValues[index1].ActionID == ActionType.AddLink || categoryValues[index1].ActionID == ActionType.Create))
      {
        RelObjInfoItem relObjInfoItem = new RelObjInfoItem(categoryValues[index1].CategoryID, categoryValue1 != null ? categoryValue1.MetadataTypeID : -1)
        {
          ProjInfo = categoryValue2 != null ? new ObjInfoItem(categoryValue2.ProjID) : (ObjInfoItem) null
        };
        if (!relObjInfoItemList1.Contains(relObjInfoItem))
          relObjInfoItemList1.Add(relObjInfoItem);
      }
      if (categoryValues[index1].CategoryType == 5 && (categoryValues[index1].ActionID == ActionType.EditLink || categoryValues[index1].ActionID == ActionType.Edit))
      {
        RelObjInfoItem relObjInfoItem = new RelObjInfoItem(categoryValues[index1].CategoryID, categoryValue1 != null ? categoryValue1.MetadataTypeID : -1)
        {
          ProjInfo = categoryValue2 != null ? new ObjInfoItem(categoryValue2.ProjID) : (ObjInfoItem) null
        };
        if (!relObjInfoItemList2.Contains(relObjInfoItem) && !relObjInfoItemList1.Contains(relObjInfoItem))
          relObjInfoItemList2.Add(relObjInfoItem);
      }
      if (categoryValues[index1].CategoryType == 5 && (categoryValues[index1].ActionID == ActionType.DeleteLink || categoryValues[index1].ActionID == ActionType.Delete || categoryValues[index1].ActionID == ActionType.Purge))
      {
        RelObjInfoItem relObjInfoItem = new RelObjInfoItem(categoryValues[index1].CategoryID, categoryValue1 != null ? categoryValue1.MetadataTypeID : -1)
        {
          ProjInfo = categoryValue2 != null ? new ObjInfoItem(categoryValue2.ProjID) : (ObjInfoItem) null
        };
        if (!source.Contains(relObjInfoItem))
        {
          source.Add(relObjInfoItem);
          if (relObjInfoItemList1.Contains(relObjInfoItem))
            relObjInfoItemList1.Remove(relObjInfoItem);
          if (relObjInfoItemList2.Contains(relObjInfoItem))
            relObjInfoItemList2.Remove(relObjInfoItem);
        }
      }
    }
    if (source.Count > 0)
    {
      List<long> list1 = source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (o => o.RelationID)).ToList<long>();
      List<long> list2 = source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (o => !((TypedInfoItem) o.ProjInfo != (TypedInfoItem) null) ? 0L : o.ProjInfo.ObjectID)).ToList<long>();
      List<int> list3 = source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (o => !((TypedInfoItem) o.ProjInfo != (TypedInfoItem) null) ? -1 : o.ProjInfo.ObjTypeID)).ToList<int>();
      List<int> list4 = source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (o => o.RelTypeID)).ToList<int>();
      queue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) list1, (IList<long>) list2, (IList<int>) list3, (IList<int>) list4, navigatorRelationCommand));
      queue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) list1.Select<long, long>((Func<long, long>) (o => -o)).ToList<long>(), (IList<long>) list2, (IList<int>) list3, (IList<int>) list4, navigatorRelationCommand));
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (RelObjInfoItem relObjInfoItem in relObjInfoItemList1)
      {
        int num = -1;
        if ((TypedInfoItem) relObjInfoItem.ProjInfo != (TypedInfoItem) null)
          num = relObjInfoItem.ProjInfo.ObjTypeID;
        if (relObjInfoItem.RelTypeID == -1 || (TypedInfoItem) relObjInfoItem.ProjInfo == (TypedInfoItem) null)
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(relObjInfoItem.RelationID, false);
          if (relation == null && longList2.Count > 0)
            relation = sessionKeeper.Session.GetRelation(-relObjInfoItem.RelationID, false);
          if (relation != null)
          {
            relObjInfoItem.RelTypeID = relation.RelationType;
            relObjInfoItem.ProjInfo = (ObjInfoItem) new ObjInfoIDItem(relation.ProjID);
            IDBObject dbObject = sessionKeeper.Session.GetObject(relation.ProjID, false);
            if (dbObject != null)
              num = dbObject.ObjectType;
          }
          else
            continue;
        }
        if (ObjectTypeHelper.IsUnknownObjectTypeID(num) && (TypedInfoItem) relObjInfoItem.ProjInfo != (TypedInfoItem) null && !ObjectHelper.IsUnknownObjectID(relObjInfoItem.ProjInfo.ObjectID))
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(relObjInfoItem.ProjInfo.ObjectID, false);
          if (dbObject != null)
            num = dbObject.ObjectType;
        }
        DBRelationsEventArgs args = new DBRelationsEventArgs("RelationsCreated", relObjInfoItem.RelationID, relObjInfoItem.ProjInfo.ObjectID, num, relObjInfoItem.RelTypeID)
        {
          RelationCommand = navigatorRelationCommand
        };
        queue.QueueEvent((NotificationEventArgs) args);
      }
      foreach (RelObjInfoItem relObjInfoItem in relObjInfoItemList2)
      {
        if (relObjInfoItem.RelTypeID == -1 || (TypedInfoItem) relObjInfoItem.ProjInfo == (TypedInfoItem) null)
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(relObjInfoItem.RelationID, false);
          if (relation == null && longList2.Count > 0)
            relation = sessionKeeper.Session.GetRelation(-relObjInfoItem.RelationID, false);
          if (relation != null)
          {
            relObjInfoItem.RelTypeID = relation.RelationType;
            relObjInfoItem.ProjInfo = (ObjInfoItem) new ObjInfoIDItem(relation.ProjID);
          }
          else
            continue;
        }
        DBRelationsEventArgs args = new DBRelationsEventArgs("RelationsChanged", relObjInfoItem.RelationID, relObjInfoItem.ProjInfo.ObjectID, relObjInfoItem.RelTypeID)
        {
          RelationCommand = navigatorRelationCommand
        };
        queue.QueueEvent((NotificationEventArgs) args);
      }
    }
    return (INotificationQueue) queue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="categoryValues"></param>
  public static void Notify(
    object sender,
    List<CategoryValue> categoryValues,
    NavigatorRelationCommand navigatorRelationCommand = NavigatorRelationCommand.Unknown)
  {
    NotificationHelper.GetQueue((IList<CategoryValue>) categoryValues, navigatorRelationCommand)?.FlushQueue();
  }
}
