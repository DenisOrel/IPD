// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Common.TechcardClientUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.Common;

/// <summary>Создание связей с назначением сортировки</summary>
/// <remarks>Назначение сортировки производится через службу
/// ICompositionsAutomaticSortingService, класс остался для совместимости со старым кодом</remarks>
public static class TechcardClientUtils
{
  /// <summary>Получение сервиса нумерации и отображения составов</summary>
  /// <param name="session"></param>
  /// <returns></returns>
  private static ICompositionsAutomaticSortingService GetAutoSortService(IUserSession session)
  {
    return session?.GetCustomService(typeof (ICompositionsAutomaticSortingService)) as ICompositionsAutomaticSortingService;
  }

  /// <summary>Начало создание связей</summary>
  /// <remarks>Для ускорения рекомендуется передавать ObjInfoItem с типом объекта</remarks>
  /// <param name="objectId">Ид. версии родительского объекта</param>
  /// <param name="session">сессия</param>
  public static void StartCreateRelations(long objectId, IUserSession session)
  {
    if (objectId == 0L || session == null)
      return;
    TechcardClientUtils.StartCreateRelations(new ObjInfoItem(objectId), session);
  }

  /// <summary>Начало создание связей</summary>
  /// <param name="objItem">Ид. версии родительского объекта</param>
  /// <param name="session">сессия</param>
  public static void StartCreateRelations(ObjInfoItem objItem, IUserSession session)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) objItem) || session == null)
      return;
    TechcardClientUtils.StartCreateRelations((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      objItem
    }, session);
  }

  /// <summary>Начало создание связей</summary>
  /// <remarks>Для ускорения рекомендуется передавать ObjInfoItem с типом объекта</remarks>
  /// <param name="objectIDs">Ид. версии родительского объекта</param>
  /// <param name="session">сессия</param>
  public static void StartCreateRelations(IEnumerable<long> objectIDs, IUserSession session)
  {
    if (objectIDs == null || session == null)
      return;
    List<ObjInfoItem> objItems = new List<ObjInfoItem>();
    foreach (long objectId in objectIDs)
      objItems.Add(new ObjInfoItem(objectId));
    TechcardClientUtils.StartCreateRelations((IEnumerable<ObjInfoItem>) objItems, session);
  }

  /// <summary>Начало создание связей</summary>
  /// <param name="objItems">Описание версий родительских объектов</param>
  /// <param name="session">сессия</param>
  public static void StartCreateRelations(IEnumerable<ObjInfoItem> objItems, IUserSession session)
  {
    if (objItems == null || session == null)
      return;
    TechcardClientUtils.GetAutoSortService(session)?.CreateSession((object) session.SessionGUID).PrefetchObjectComposition(objItems, (object) session.SessionGUID);
  }

  /// <summary>Завершение создания связей</summary>
  /// <param name="session">Пользовательская сессия</param>
  public static void StopCreateRelations(IUserSession session)
  {
    if (session == null)
      return;
    TechcardClientUtils.GetAutoSortService(session)?.DisposeSession((object) session.SessionGUID);
  }

  /// <summary>Создание связи для указанных объектов</summary>
  /// <param name="relTypeId"></param>
  /// <param name="userSession"></param>
  /// <param name="projDbObject"></param>
  /// <param name="partDbObject"></param>
  /// <returns></returns>
  internal static IDBRelation CreateRelation(
    int relTypeId,
    IUserSession userSession,
    IDBObject projDbObject,
    IDBObject partDbObject)
  {
    return relTypeId == -1 || userSession == null || projDbObject == null || partDbObject == null ? (IDBRelation) null : TechcardClientUtils.CreateRelation(userSession.GetRelationCollection(relTypeId), projDbObject, partDbObject);
  }

  /// <summary>Создание связи для указанных объектов</summary>
  /// <param name="relColl"></param>
  /// <param name="userSession"></param>
  /// <param name="projDbObject"></param>
  /// <param name="partDbObject"></param>
  /// <returns></returns>
  internal static IDBRelation CreateRelation(
    IDBRelationCollection relColl,
    IDBObject projDbObject,
    IDBObject partDbObject)
  {
    if (relColl == null || projDbObject == null || partDbObject == null)
      return (IDBRelation) null;
    IDBTypedObjectID dbTypedObjectId1 = (IDBTypedObjectID) TechcardClientUtils.GetDBTypedObjectID(projDbObject);
    IDBTypedObjectID dbTypedObjectId2 = (IDBTypedObjectID) TechcardClientUtils.GetDBTypedObjectID(partDbObject);
    return TechcardClientUtils.CreateRelation(relColl, dbTypedObjectId1, dbTypedObjectId2);
  }

  /// <summary>Создание связи для указанных объектов</summary>
  /// <param name="relTypeId"></param>
  /// <param name="userSession"></param>
  /// <param name="projDbObjectId"></param>
  /// <param name="partDbObjectId"></param>
  /// <returns></returns>
  internal static IDBRelation CreateRelation(
    int relTypeId,
    IUserSession userSession,
    IDBTypedObjectID projDbObjectId,
    IDBTypedObjectID partDbObjectId)
  {
    return relTypeId == -1 || userSession == null || projDbObjectId == null || partDbObjectId == null ? (IDBRelation) null : TechcardClientUtils.CreateRelation(userSession.GetRelationCollection(relTypeId), projDbObjectId, partDbObjectId);
  }

  /// <summary>Создание связи для указанных объектов</summary>
  /// <param name="relColl"></param>
  /// <param name="userSession"></param>
  /// <param name="projDbObjectId"></param>
  /// <param name="partDbObjectId"></param>
  /// <returns></returns>
  internal static IDBRelation CreateRelation(
    IDBRelationCollection relColl,
    IDBTypedObjectID projDbObjectId,
    IDBTypedObjectID partDbObjectId)
  {
    return TechcardClientUtils.CreateRelation(relColl, projDbObjectId, partDbObjectId, NewRelationProperties.Empty);
  }

  /// <summary>Создание связи для указанных объектов</summary>
  /// <param name="dbRelationCollection"></param>
  /// <param name="userSession"></param>
  /// <param name="projDbObjectId"></param>
  /// <param name="partDbObjectId"></param>
  /// <param name="relProps">Параметры создаваемой связи</param>
  /// <returns></returns>
  internal static IDBRelation CreateRelation(
    IDBRelationCollection dbRelationCollection,
    IDBTypedObjectID projDbObjectId,
    IDBTypedObjectID partDbObjectId,
    NewRelationProperties relProps)
  {
    return TechcardClientUtils.CreateRelation(dbRelationCollection, (ObjInfoItem) new ObjInfoIDItem(projDbObjectId.ObjectID, projDbObjectId.ObjectType, projDbObjectId.ID), (ObjInfoItem) new ObjInfoIDItem(partDbObjectId.ObjectID, partDbObjectId.ObjectType, partDbObjectId.ID), relProps);
  }

  /// <summary>Создание связи для указанных объектов</summary>
  /// <param name="dbRelationCollection"></param>
  /// <param name="userSession"></param>
  /// <param name="projDbObjectId"></param>
  /// <param name="partDbObjectId"></param>
  /// <param name="relProps">Параметры создаваемой связи</param>
  /// <returns></returns>
  internal static IDBRelation CreateRelation(
    IDBRelationCollection dbRelationCollection,
    ObjInfoItem projDbObjectItem,
    ObjInfoItem partDbObjectItem,
    NewRelationProperties relProps)
  {
    if (dbRelationCollection == null || ObjInfoItem.IsEmpty((ITypedInfoItem) projDbObjectItem) || ObjInfoItem.IsEmpty((ITypedInfoItem) partDbObjectItem))
      return (IDBRelation) null;
    IUserSession session1 = dbRelationCollection.Session;
    if (relProps.ProjectObjectID == 0L)
      relProps.ProjectObjectID = projDbObjectItem.ObjectID;
    if (relProps.PartID == 0L)
    {
      if (partDbObjectItem is ObjInfoIDItem objInfoIdItem)
        relProps.PartID = objInfoIdItem.ID;
      relProps.PartObjectID = partDbObjectItem.ObjectID;
    }
    IDBRelation relation = dbRelationCollection.Create(relProps);
    if (relation == null)
      return (IDBRelation) null;
    if (relProps.ValuesList != null)
    {
      bool flag = false;
      foreach (AttributeValues values in relProps.ValuesList)
      {
        if (values.AttributeID == TechCardConsts.AttributeTypes.SortAttrTypeID)
        {
          flag = values.Values != null;
          break;
        }
      }
      if (flag)
        return relation;
    }
    ICompositionsAutomaticSortingService autoSortService = TechcardClientUtils.GetAutoSortService(session1);
    if (autoSortService == null)
      return (IDBRelation) null;
    if (autoSortService.IsSessionPresent((object) session1.SessionGUID) != 0)
    {
      ICompositionsAutomaticSortingSession session2 = autoSortService.CreateSession((object) session1.SessionGUID);
      try
      {
        session2.ProceedRelation(new CompositionSortingProjInfo(relation.RelationID, relation.RelationType, projDbObjectItem.ObjectID, projDbObjectItem.ObjTypeID, partDbObjectItem.ObjTypeID, 0L), (object) session1.SessionGUID);
      }
      finally
      {
        autoSortService.DisposeSession((object) session1.SessionGUID);
      }
    }
    return relation;
  }

  /// <summary>
  /// Создание для указанного объекта связей заданного типа с заданными объектами
  /// </summary>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="objectId">идентификатор объекта для которого создаются связи (Может быть заготовкой)</param>
  /// <param name="relationTypeIDs">массив идентификаторов типов связей, которые будут созданы</param>
  /// <param name="relatedObjectIDs">массив идентификаторов объектов, с которыми будут созданы связи (Не передавать сюда заготовки объектов!!)</param>
  /// <param name="startDate">дата и время с которых связь начинает действовать</param>
  /// <param name="createMode">Режим создания связей</param>
  public static List<IDBRelation> CreateRelations(
    IUserSession userSession,
    long objectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    TechCreateRelMode createMode)
  {
    List<IDBRelation> relations = new List<IDBRelation>();
    if (objectId == 0L || relationTypeIDs == null || relationTypeIDs.Length == 0 || relatedObjectIDs == null || relatedObjectIDs.Length == 0)
      return relations;
    IDBTypedObjectID dbTypedObjectId1 = (IDBTypedObjectID) null;
    Dictionary<long, IDBTypedObjectID> dictionary = new Dictionary<long, IDBTypedObjectID>();
    List<long> objIdList = new List<long>((IEnumerable<long>) relatedObjectIDs);
    objIdList.Add(objectId);
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(6);
    columnDescriptorList.Add(new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -7, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -3, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -8, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -15, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) objIdList.ToArray(), LogicalOperators.NONE, 0, false)
    };
    DataTable objectData = DataHelper.GetObjectData(-1, userSession, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<long>) objIdList);
    if (objectData == null || objectData.Rows.Count == 0)
      return relations;
    foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
    {
      if (row != null)
      {
        long result1;
        long.TryParse(row[0].ToString(), out result1);
        int result2;
        int.TryParse(row[1].ToString(), out result2);
        long result3;
        long.TryParse(row[2].ToString(), out result3);
        long result4;
        long.TryParse(row[3].ToString(), out result4);
        long result5;
        long.TryParse(row[4].ToString(), out result5);
        string caption = "";
        IDBTypedObjectID dbTypedObjectId2 = (IDBTypedObjectID) new DBTypedObjectID(result2, result1, result3, caption, result4, 0L, 0L, string.Empty, result5);
        if (objectId == result1)
          dbTypedObjectId1 = dbTypedObjectId2;
        else
          dictionary.Add(result1, dbTypedObjectId2);
      }
    }
    if (dictionary.Count == 0)
      return relations;
    if (dbTypedObjectId1 == null)
    {
      IDBObject dbObject = userSession.GetObject(objectId, false);
      if (dbObject == null)
        return relations;
      dbTypedObjectId1 = (IDBTypedObjectID) TechcardClientUtils.GetDBTypedObjectID(dbObject);
    }
    int objectType1 = dbTypedObjectId1.ObjectType;
    int num = relationTypeIDs.Length < relatedObjectIDs.Length ? relationTypeIDs.Length : relatedObjectIDs.Length;
    for (int index = 0; index < num; ++index)
    {
      if (relatedObjectIDs[index] != 0L && relationTypeIDs[index] > -1)
      {
        int relationTypeId = relationTypeIDs[index];
        long relatedObjectId = relatedObjectIDs[index];
        IDBTypedObjectID dbTypedObjectId3;
        if (dictionary.TryGetValue(relatedObjectId, out dbTypedObjectId3) && dbTypedObjectId3 != null)
        {
          int objectType2 = dbTypedObjectId3.ObjectType;
          IDBRelationCollection relationCollection = userSession.GetRelationCollection(relationTypeId);
          if (relationCollection != null)
          {
            switch (createMode)
            {
              case TechCreateRelMode.tcrmEnterIn:
                if (TechCardUtils.CheckRelationApplicability(objectType2, objectType1, relationTypeId, false, false))
                {
                  relations.Add(TechcardClientUtils.CreateRelation(relationCollection, dbTypedObjectId3, dbTypedObjectId1));
                  continue;
                }
                continue;
              case TechCreateRelMode.tcrmContains:
                if (TechCardUtils.CheckRelationApplicability(objectType1, objectType2, relationTypeId, false, false))
                {
                  relations.Add(TechcardClientUtils.CreateRelation(relationCollection, dbTypedObjectId1, dbTypedObjectId3));
                  continue;
                }
                continue;
              case TechCreateRelMode.tcrmBothEnterInFirst:
                if (TechCardUtils.CheckRelationApplicability(objectType2, objectType1, relationTypeId, false, false))
                {
                  relations.Add(TechcardClientUtils.CreateRelation(relationCollection, dbTypedObjectId3, dbTypedObjectId1));
                  continue;
                }
                if (TechCardUtils.CheckRelationApplicability(objectType1, objectType2, relationTypeId, false, false))
                {
                  relations.Add(TechcardClientUtils.CreateRelation(relationCollection, dbTypedObjectId1, dbTypedObjectId3));
                  continue;
                }
                continue;
              case TechCreateRelMode.tcrmBothContainsFirst:
                if (TechCardUtils.CheckRelationApplicability(objectType1, objectType2, relationTypeId, false, false))
                {
                  relations.Add(TechcardClientUtils.CreateRelation(relationCollection, dbTypedObjectId1, dbTypedObjectId3));
                  continue;
                }
                if (TechCardUtils.CheckRelationApplicability(objectType2, objectType1, relationTypeId, false, false))
                {
                  relations.Add(TechcardClientUtils.CreateRelation(relationCollection, dbTypedObjectId3, dbTypedObjectId1));
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
    return relations;
  }

  /// <summary>Получение информации по самому объекту</summary>
  /// <param name="dbObject"></param>
  /// <returns></returns>
  public static DBTypedObjectID GetDBTypedObjectID(IDBObject dbObject)
  {
    return dbObject == null ? (DBTypedObjectID) null : new DBTypedObjectID(dbObject.ObjectType, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, Convert.ToInt64(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
  }

  /// <summary>Утилиты для работы с атрибутами</summary>
  public sealed class Attributes
  {
    /// <summary>
    /// Редактирование /выбор значения для ссылочного атрибута
    /// </summary>
    /// <remarks>Внутри метода вызываются диалоги, поэтому не рекомендуется вызывать метод внутри UserSession</remarks>
    /// <param name="objTypeId">Ид. типа объекта</param>
    /// <param name="attrTypeId">Ид. типа атрибута</param>
    /// <param name="attrValue">Значение атрибута</param>
    /// <returns></returns>
    public static bool EditObjLinkValue(int objTypeId, int attrTypeId, ref long attrValue)
    {
      if (attrTypeId == 0)
        return false;
      IImbaseFilterSelector service = ServiceUtils.GetService<IImbaseFilterSelector>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        ImbaseExtendedItem imbaseExtendedItem = (ImbaseExtendedItem) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ExtendedServiceHelper.ObjTypeInfo objTypeData = ExtendedServiceHelper.GetObjTypeData(objTypeId, sessionKeeper.Session);
          if (objTypeData != null)
            imbaseExtendedItem = objTypeData.GetValue(attrTypeId, sessionKeeper.Session);
        }
        if (imbaseExtendedItem != null && imbaseExtendedItem.SelectMode != ImbaseCatalogSelectMode.imcmNone)
        {
          List<long> catalogIds = imbaseExtendedItem.CatalogIDs;
          ImbaseCatalogSelectMode selectMode = imbaseExtendedItem.SelectMode;
          if (catalogIds.Count > 0)
          {
            attrValue = service.SelectImbaseObject(catalogIds, (int[]) null, 0L, attrValue, selectMode, attrID: attrTypeId);
            return attrValue != 0L;
          }
        }
      }
      ArrayList arrayList = ObjectEditor.GetObjTypeListByAttrId(attrTypeId) ?? new ArrayList();
      if (arrayList.Count == 0)
        arrayList.Add((object) -1);
      if (arrayList.Count > 0)
      {
        IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects((int[]) arrayList.ToArray(typeof (int)), new long[1]
        {
          attrValue
        }, true, true);
        if (dbObjectIdArray != null)
        {
          attrValue = dbObjectIdArray[0].Value;
          return true;
        }
      }
      return false;
    }
  }

  /// <summary>Утилиты для типов объектов</summary>
  public static class ObjectTypes
  {
    /// <summary>
    ///  Список видимых типов объектов для текущего пользователя
    /// </summary>
    private static readonly List<int> VisibleObjTypes = new List<int>();
    /// <summary>
    /// Время последнего обновления списка видимых типов объектов
    /// </summary>
    private static DateTime _visibleObjTypesLastUpdateTime = DateTime.Today;

    /// <summary>
    /// Получение списка видимых типов объектов для тек. пользователя
    /// </summary>
    /// <returns>Отсортированный по ид. типа объектов список</returns>
    public static List<int> GetVisibleObjTypes()
    {
      lock (TechcardClientUtils.ObjectTypes.VisibleObjTypes)
      {
        if (TechcardClientUtils.ObjectTypes.VisibleObjTypes.Count != 0 && TechcardClientUtils.ObjectTypes._visibleObjTypesLastUpdateTime + Intermech.Consts.CacheClearPeriod >= DateTime.Now)
          return new List<int>((IEnumerable<int>) TechcardClientUtils.ObjectTypes.VisibleObjTypes);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(-2, true);
          TechcardClientUtils.ObjectTypes.VisibleObjTypes.AddRange((IEnumerable<int>) objectTypeCollection.GetVisibleList());
          GenericListHelper.MakeUnique<int>(TechcardClientUtils.ObjectTypes.VisibleObjTypes);
          TechcardClientUtils.ObjectTypes._visibleObjTypesLastUpdateTime = DateTime.Now;
        }
        return new List<int>((IEnumerable<int>) TechcardClientUtils.ObjectTypes.VisibleObjTypes);
      }
    }
  }
}
