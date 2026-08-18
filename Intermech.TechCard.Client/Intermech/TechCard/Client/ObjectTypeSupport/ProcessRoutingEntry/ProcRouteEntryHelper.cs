// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.ProcRouteEntryHelper
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.MRP2;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;

/// <summary>
/// 
/// </summary>
public static class ProcRouteEntryHelper
{
  /// <summary>
  /// Создать объект и заполнить атрибуты по текущей входимости
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="projItem">Родительский объект для создания связи</param>
  /// <param name="createIsEmpty">Создавать объект, даже если нет атрибутов для заполнения текущей входимости</param>
  /// <param name="selectedItems">Выбранный элемент дерева навигатора. Используется для поиска родительских узлов текущего дерева и заполнения атрибутов входимости </param>
  /// <returns></returns>
  public static long CreateProcRouteEntry(
    IUserSession session,
    ObjInfoItem projItem,
    bool createIsEmpty,
    ISelectedItems selectedItems)
  {
    if (session == null || (TypedInfoItem) projItem == (TypedInfoItem) null)
      return 0;
    IDBObject projDbObject = session.GetObject(projItem.ObjectID, false);
    if (projDbObject == null)
      return 0;
    IDBObjectCollection objectCollection = session.GetObjectCollection(TechCardConsts.ObjectTypes.ProcRoutingEntryID);
    if (objectCollection == null)
      return 0;
    List<IDBRelation> source = new List<IDBRelation>();
    IList<AttributeValues> attributeValueList;
    if (!createIsEmpty & !ProcRouteEntryHelper.GetCurrentEntryAttributeValues(session, selectedItems, out attributeValueList))
      return 0;
    IDBObject partDbObject = objectCollection.Create();
    if (attributeValueList != null && attributeValueList.Count != 0)
      partDbObject.SetAttributesValues(attributeValueList.ToArray<AttributeValues>());
    IDBRelationCollection relationCollection = session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
    TechcardClientUtils.StartCreateRelations(projDbObject.ObjectID, session);
    try
    {
      source.Add(TechcardClientUtils.CreateRelation(relationCollection, projDbObject, partDbObject));
      if (partDbObject.IsCreationMode)
        partDbObject.CommitCreation(false);
    }
    finally
    {
      TechcardClientUtils.StopCreateRelations(session);
    }
    if (source.Count != 0)
    {
      NotificationQueue notificationQueue = new NotificationQueue();
      notificationQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<IDBRelation, long>((Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source.Select<IDBRelation, long>((Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source.Select<IDBRelation, int>((Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
      notificationQueue.FlushQueue();
    }
    return partDbObject.ObjectID;
  }

  /// <summary>Заполнение параметров текущей входимости</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="selectedItems">Выбранный элемент дерева навигатора. Используется для поиска родительских узлов текущего дерева и заполнения атрибутов входимости </param>
  /// <param name="attributeValueList">Коллекция значений атрибутов текущей входимости</param>
  /// <returns>True если коллекция не пустая</returns>
  public static bool GetCurrentEntryAttributeValues(
    [NotNull] IUserSession session,
    [NotNull] ISelectedItems selectedItems,
    out IList<AttributeValues> attributeValueList)
  {
    attributeValueList = (IList<AttributeValues>) new List<AttributeValues>();
    IEnumerable<RelObjInfoItem> relObjInfoItems;
    if (!TechcardClientControlsUtils.GetItemsApplicabilityInfo(selectedItems, (IServiceProvider) ApplicationServices.Container, out relObjInfoItems))
      return false;
    if (!(relObjInfoItems is List<RelObjInfoItem> source))
      source = relObjInfoItems.ToList<RelObjInfoItem>();
    List<int> articleObjectTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
    RelObjInfoItem relObjInfoItem = source.FirstOrDefault<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (a => articleObjectTypes.Contains(a.PartInfo.ObjTypeID)));
    if ((TypedInfoItem) relObjInfoItem == (TypedInfoItem) null)
      return false;
    ObjInfoItem partInfo = relObjInfoItem.PartInfo;
    if (MetaDataHelper.IsObjectTypeChildOf(partInfo.ObjTypeID, MRP2Consts.objtypeIdExitAssembly))
    {
      object attributeValueByGuid = session.GetObjectAttributeValueByGuid(partInfo.ObjectID, TechCardConsts.AttributeTypes.ProductionObjectUIDAttrGuid);
      attributeValueList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID, attributeValueByGuid));
    }
    ObjInfoItem projInfo1 = relObjInfoItem.ProjInfo;
    if (!((TypedInfoItem) projInfo1 != (TypedInfoItem) null) || projInfo1.HasEmptyInfo)
      return false;
    if (MetaDataHelper.IsObjectTypeChildOf(projInfo1.ObjTypeID, TechCardConsts.ObjectTypes.ArticleBaseID) || MetaDataHelper.IsObjectTypeChildOf(projInfo1.ObjTypeID, TechCardConsts.ObjectTypes.ZakazObjectID))
      attributeValueList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfAssemblyObjectAttrID, (object) ProcRouteEntryHelper.GetItemID(projInfo1)));
    if (MetaDataHelper.IsObjectTypeChildOf(projInfo1.ObjTypeID, MetaDataHelper.GetObjectTypeID("cadd9a5d-306c-11d8-b4e9-00304f19f545")))
    {
      object attributeValueByGuid = session.GetObjectAttributeValueByGuid(projInfo1.ObjectID, TechCardConsts.AttributeTypes.ProductionObjectUIDAttrGuid);
      attributeValueList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID, attributeValueByGuid));
    }
    for (int index = source.IndexOf(relObjInfoItem); index < source.Count; ++index)
    {
      ObjInfoItem projInfo2 = source[index].ProjInfo;
      if (!((TypedInfoItem) projInfo2 == (TypedInfoItem) null) && !projInfo2.HasEmptyInfo)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(projInfo2.ObjTypeID, MRP2Consts.objtypeIdExitAssembly))
        {
          object attributeValueByGuid = session.GetObjectAttributeValueByGuid(projInfo2.ObjectID, TechCardConsts.AttributeTypes.ProductionObjectUIDAttrGuid);
          attributeValueList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID, attributeValueByGuid));
        }
        if (MetaDataHelper.IsObjectTypeChildOf(projInfo2.ObjTypeID, TechCardConsts.ObjectTypes.ZakazObjectID))
          attributeValueList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfOrderObjectAttrID, (object) ProcRouteEntryHelper.GetItemID(projInfo2)));
        if (MetaDataHelper.IsObjectTypeChildOf(projInfo2.ObjTypeID, MRP2Consts.objtypeIdProductionLists))
          attributeValueList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrID, (object) ProcRouteEntryHelper.GetItemID(projInfo2)));
      }
      else
        break;
    }
    return attributeValueList.Any<AttributeValues>();
  }

  /// <summary>Получить идентификатор объекта (не версии)</summary>
  /// <param name="infoItem"></param>
  /// <returns></returns>
  private static long GetItemID(ObjInfoItem infoItem)
  {
    return infoItem is ObjInfoIDItem objInfoIdItem ? objInfoIdItem.ID : ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(infoItem.ObjectID).ID;
  }
}
