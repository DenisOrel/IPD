// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRoutesNotificationHandler
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Ceh_Route;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

internal class CehRoutesNotificationHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void RelationsWasChangedHandler(object sender, NotificationEventArgs e)
  {
    if (!(e is DBRelationsEventArgs e1))
      return;
    CehRoutesNotificationHandler.CreateCehRouteStringByRelationChanged(sender, e1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void CreateCehRouteStringByRelationChanged(object sender, DBRelationsEventArgs e)
  {
    if (e == null || e.RelationCommand != NavigatorRelationCommand.Unknown || e.ItemsCount == 0)
      return;
    List<RelObjInfoItem> source1 = new List<RelObjInfoItem>();
    for (int index = 0; index < e.RelationIDs.Count; ++index)
    {
      long relationId = e.RelationIDs[index];
      int relationType = e.GetRelationType(relationId);
      if (relationType == TechCardConsts.RelTypes.TechRelationID)
      {
        ObjInfoItem objInfoItem = new ObjInfoItem(e.GetProjID(relationId), e.GetProjTypeID4Link(relationId));
        source1.Add(new RelObjInfoItem(relationId, relationType)
        {
          ProjInfo = objInfoItem
        });
      }
    }
    if (source1.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<RelObjInfoItem> list = source1.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => ObjInfoItem.IsEmpty((ITypedInfoItem) item.ProjInfo))).ToList<RelObjInfoItem>();
      if (list.Count != 0)
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
        relationCollection.LocalTypesMode = true;
        ColumnDescriptor[] columns = new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -20),
          new ColumnDescriptor((object) -21)
        };
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-20, RelationalOperators.In, (object) list.Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (item => item.RelationID)).ToArray<long>(), LogicalOperators.NONE, 0, false)
        }, columns);
        DataTable dataTable = relationCollection.Select(paramSet);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long relationID = Convert.ToInt64(row[0]);
            long int64 = Convert.ToInt64(row[1]);
            RelObjInfoItem relObjInfoItem = list.Find((Predicate<RelObjInfoItem>) (item => item.RelationID == relationID));
            if ((TypedInfoItem) relObjInfoItem != (TypedInfoItem) null)
              relObjInfoItem.ProjInfo.ObjectID = int64;
          }
        }
        if (e.ItemsCount == source1.Count)
        {
          for (int index = 0; index < e.ItemsCount; ++index)
            e.ProjIDs.Insert(index, source1[index].ProjInfo.ObjectID);
        }
      }
      foreach (ObjInfoItem updateUnknownType in ServiceUtils.GetService<ITypedInfoService>((object) sessionKeeper.Session, true).UpdateUnknownTypes((IEnumerable<ObjInfoItem>) source1.Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)).Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => item.ObjTypeID == -1)).ToArray<ObjInfoItem>(), (object) sessionKeeper.Session.SessionGUID))
      {
        ObjInfoItem objInfoWithType = updateUnknownType;
        RelObjInfoItem relObjInfoItem = source1.Find((Predicate<RelObjInfoItem>) (item => item.ProjInfo.Equals(objInfoWithType)));
        if ((TypedInfoItem) relObjInfoItem != (TypedInfoItem) null)
          relObjInfoItem.ProjInfo.ObjTypeID = objInfoWithType.ObjTypeID;
      }
    }
    List<ObjInfoItem> list1 = source1.Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)).Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => MetaDataHelper.IsObjectTypeChildOf(item.ObjTypeID, TechCardConsts.ObjectTypes.TemplRouteBaseID))).ToList<ObjInfoItem>();
    if (list1.Count == 0)
      return;
    if (e is DBRelationsExtendedEventArgs extendedEventArgs && extendedEventArgs.AttributeValuesArray != null)
    {
      bool flag = false;
      ICehRouteStringItem cehRouteStringItem;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ServiceUtils.GetService<ICehRouteStringService>((object) sessionKeeper.Session, true).LoadSettings(sessionKeeper.Session.SessionGUID, out cehRouteStringItem);
        if (cehRouteStringItem == null)
          return;
      }
      foreach (ICehRouteStringTemplItem routeStringTemplItem in (IEnumerable<ICehRouteStringTemplItem>) cehRouteStringItem.Items)
      {
        foreach (AttributeValues attributeValues in extendedEventArgs.AttributeValuesArray)
        {
          if (routeStringTemplItem.RouteTemplate.Contains(CehRouteStringTemplItem.LinkAttributePrefix + attributeValues.AttributeName))
            flag = true;
        }
        if (flag)
          break;
      }
      if (!flag)
        return;
    }
    List<ObjInfoItem> typedInfoList = new List<ObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable source2 = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true).LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, (IEnumerable<ObjInfoItem>) list1, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID), (IEnumerable<ColumnDescriptor>) new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) -2),
        new ColumnDescriptor((object) -7)
      }, false, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null, 1);
      List<ObjInfoItem> objInfoItemList = source2 != null ? source2.AsEnumerable().Select<DataRow, ObjInfoItem>((System.Func<DataRow, ObjInfoItem>) (row => new ObjInfoItem(Convert.ToInt64(row[0]), Convert.ToInt32(row[1])))).ToList<ObjInfoItem>() : new List<ObjInfoItem>();
      if (objInfoItemList.Count == 0)
        return;
      ICehRouteStringService service = ServiceUtils.GetService<ICehRouteStringService>((object) sessionKeeper.Session, true);
      foreach (ObjInfoItem objInfoItem in objInfoItemList)
      {
        service.CreateCehRouteString(objInfoItem.ObjectID, sessionKeeper.Session.SessionGUID);
        typedInfoList.Add(objInfoItem);
      }
    }
    INotificationService service1 = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    List<long> itemIdList;
    List<int> itemTypeList;
    if (service1 == null || !SomeTypedInfoHelper<ObjInfoItem>.GetItemCache((IEnumerable<ObjInfoItem>) typedInfoList, out itemIdList, out itemTypeList))
      return;
    service1.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) itemIdList, (IList<int>) itemTypeList));
  }

  /// <summary>
  /// 
  /// </summary>
  public static void Register()
  {
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.Subscribe("RelationsChanged", new NotificationEventHandler(CehRoutesNotificationHandler.RelationsWasChangedHandler));
  }
}
