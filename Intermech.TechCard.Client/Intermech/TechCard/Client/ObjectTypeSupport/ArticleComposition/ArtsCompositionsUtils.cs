// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.ArtsCompositionsUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechNumeration;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Remoting.Sponsors;
using Intermech.TechCard.Client.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition;

/// <summary>
/// 
/// </summary>
public class ArtsCompositionsUtils
{
  /// <summary>
  /// Копирование атрибутов объекта для "Единица состава изделия"
  /// </summary>
  /// <param name="dbObject">Объект - приемник</param>
  /// <param name="artItemInfo">Описание изделия</param>
  /// <param name="attrTypeIds">Перечень копируемых атрибутов</param>
  /// <param name="customValues">Кастом атрибуты</param>
  /// <param name="session">Пользов. сессия</param>
  /// <returns></returns>
  internal static bool CopyObjectAttributes(
    IDBObject dbObject,
    ArtsCompositionsUtils.ArticleItemInfo artItemInfo,
    List<int> attrTypeIds,
    List<AttributeValues> customValues,
    IUserSession session)
  {
    if (dbObject == null || session == null)
      return false;
    if (attrTypeIds != null && attrTypeIds.Count != 0 && artItemInfo != null && artItemInfo.PartArtID != 0L)
      TechCardUtils.CopyObjectAttributes(session.GetObject(artItemInfo.PartArtID, false), dbObject, attrTypeIds.ToArray());
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    if (artItemInfo != null)
    {
      attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.ObjectRefAttrID, (object) artItemInfo.PartArtID));
      if (artItemInfo.MainArtID != 0L)
        attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfMainObjectAttrID, (object) artItemInfo.MainArtID));
      if (artItemInfo.ProjArtID != 0L)
        attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID, (object) artItemInfo.ProjArtID));
    }
    if (customValues != null)
      attributeValuesList.AddRange((IEnumerable<AttributeValues>) customValues);
    dbObject.SetAttributesValues(attributeValuesList.ToArray());
    return true;
  }

  /// <summary>
  /// Копирование атрибутов связи для "Единица состава изделия"
  /// </summary>
  /// <param name="dbRelation">Связь - приемник</param>
  /// <param name="artItemInfo">Описание изделия</param>
  /// <param name="attrTypeIds">Перечень копируемых атрибутов</param>
  /// <param name="customValues">Кастом атрибуты</param>
  /// <param name="session">Пользов. сессия</param>
  /// <returns></returns>
  internal static bool CopyRelationAttributes(
    IDBRelation dbRelation,
    ArtsCompositionsUtils.ArticleItemInfo artItemInfo,
    List<int> attrTypeIds,
    List<AttributeValues> customValues,
    IUserSession session)
  {
    if (dbRelation == null || session == null)
      return false;
    if (attrTypeIds != null && attrTypeIds.Count != 0 && artItemInfo != null && artItemInfo.ProjRelID != 0L)
    {
      bool flag = false;
      try
      {
        if (attrTypeIds.Contains(TechCardConsts.AttributeTypes.CountAttrTypeID) && artItemInfo.Count != null)
        {
          attrTypeIds.Remove(TechCardConsts.AttributeTypes.CountAttrTypeID);
          flag = true;
        }
        attrTypeIds.Remove(TechCardConsts.AttributeTypes.ContextVersionID);
        TechCardUtils.CopyRelationAttributes(session.GetRelation(artItemInfo.ProjRelID, false), dbRelation, attrTypeIds.ToArray());
      }
      finally
      {
        if (flag)
          attrTypeIds.Add(TechCardConsts.AttributeTypes.CountAttrTypeID);
      }
    }
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    if (artItemInfo?.Count != null)
      attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.CountAttrTypeID, (object) artItemInfo.Count));
    if (customValues != null)
      attributeValuesList.AddRange((IEnumerable<AttributeValues>) customValues);
    if (attributeValuesList.Count != 0)
      dbRelation.SetAttributesValues(attributeValuesList.ToArray());
    return true;
  }

  /// <summary>Добавление сборочной единицы</summary>
  /// <param name="projTechTypedObjId">Ид. версии объекта ТП, в который будет производиться вставка СБ.Е.</param>
  /// <param name="artItemList">Ид. версий изделий</param>
  /// <param name="needAccessories">Признак добавления состава 1-го уровня</param>
  /// <param name="session">Пользов. сессия</param>
  /// <param name="objCreated">Список созданных объектов</param>
  /// <returns></returns>
  public static bool AddAssemblingItems(
    IDBTypedObjectID projTechTypedObjId,
    List<ArtsCompositionsUtils.ArticleItemInfo> artItemList,
    bool needAccessories,
    IUserSession session,
    out List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated)
  {
    objCreated = new List<ArtsCompositionsUtils.ArticleCreatedItem>();
    if (projTechTypedObjId == null || projTechTypedObjId.ObjectID == 0L || artItemList == null || artItemList.Count == 0 || session == null)
      return false;
    if (!(session.GetCustomService(typeof (ITechNumerationService)) is ITechNumerationService customService))
    {
      string caption = LocalizationHolder.rm.GetString("TechCard.Client_138");
      int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_370"), (object) typeof (ITechNumerationService)), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    List<long> longList = new List<long>();
    Dictionary<long, ArtsCompositionsUtils.ArticleItemInfo> dictionary1 = new Dictionary<long, ArtsCompositionsUtils.ArticleItemInfo>();
    Dictionary<long, ArtsCompositionsUtils.ArticleCreatedItem> dictionary2 = new Dictionary<long, ArtsCompositionsUtils.ArticleCreatedItem>();
    Dictionary<long, IDBTypedObjectID> dictionary3 = new Dictionary<long, IDBTypedObjectID>();
    List<int> attrIds1;
    ArtsCompositionsUtils.GetAssemblingRelAttributes(out attrIds1);
    attrIds1?.Remove(TechCardConsts.AttributeTypes.CountAttrTypeID);
    IDBObjectCollection objectCollection = session.GetObjectCollection(TechCardConsts.ObjectTypes.SobirEdinicaID);
    IDBRelationCollection relationCollection = session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
    ITechNumerationSession session1 = customService.CreateSession(session.SessionGUID);
    using (new RemoteLock((object) session1))
    {
      TechcardClientUtils.StartCreateRelations(projTechTypedObjId.ObjectID, session);
      try
      {
        foreach (ArtsCompositionsUtils.ArticleItemInfo artItem in artItemList)
        {
          if (artItem != null && !dictionary2.ContainsKey(artItem.PartArtID))
          {
            IDBObject dbObject = objectCollection.Create();
            List<int> attrIds2;
            ArtsCompositionsUtils.GetAssemblingObjAttributes(session.GetObjectInfo(artItem.PartArtID).ObjectTypeID, out attrIds2);
            ArtsCompositionsUtils.CopyObjectAttributes(dbObject, artItem, attrIds2, (List<AttributeValues>) null, session);
            session1.PartObjToSuppress.AddItem(dbObject.ObjectID);
            IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) TechcardClientUtils.GetDBTypedObjectID(dbObject);
            IDBRelation relation = TechcardClientUtils.CreateRelation(relationCollection, projTechTypedObjId, dbTypedObjectId);
            if (relation != null)
            {
              ArtsCompositionsUtils.CopyRelationAttributes(relation, artItem, attrIds1, (List<AttributeValues>) null, session);
              ArtsCompositionsUtils.ArticleCreatedItem articleCreatedItem = new ArtsCompositionsUtils.ArticleCreatedItem(artItem.PartArtID, dbObject.ObjectID, dbObject.ObjectType, relation.RelationID, artItem.Count);
              objCreated.Add(articleCreatedItem);
              dictionary2.Add(artItem.PartArtID, articleCreatedItem);
              dictionary1.Add(artItem.PartArtID, artItem);
              session1.RelationsToSuppress.AddItem(relation.RelationID);
              if (dbObject.IsCreationMode)
              {
                dbObject.CommitCreation(true);
                if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
                  dbObject = dbObject.CheckOut();
                articleCreatedItem.TechObjID = dbObject.ObjectID;
              }
              longList.Add(dbObject.ObjectID);
              dictionary3.Add(dbObject.ObjectID, (IDBTypedObjectID) TechcardClientUtils.GetDBTypedObjectID(dbObject));
            }
          }
        }
        if (objCreated.Count != 0)
        {
          session1.PartObjToSuppress.Clear();
          session1.RelationsToSuppress.Clear();
          session1.NumerateObject(objCreated[0].ProjLinkID, TechNumerationObjectModes.CurrentObj, session.SessionGUID);
        }
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(session);
        customService.DisposeSession(session.SessionGUID);
      }
    }
    if (!needAccessories)
      return true;
    List<ObjInfoItem> projObjList = new List<ObjInfoItem>(dictionary1.Count);
    foreach (long key in dictionary1.Keys)
      projObjList.Add(new ObjInfoItem(key));
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new List<int>((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes));
    conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false));
    DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, session, (IEnumerable<int>) TechCardConsts.RelTypes.ArtsCompositionRelations, false, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), (IEnumerable<ColumnDescriptor>) null);
    if (childSostavData == null || childSostavData.Rows.Count == 0)
      return true;
    Dictionary<long, long> dictionary4 = new Dictionary<long, long>();
    int artCompositionRelType = TechCardConsts.RelTypes.ProektRelationID;
    ArtsCompositionsUtils.GetAccessoryRelAttributes(artCompositionRelType, out attrIds1);
    ITechNumerationSession session2 = customService.CreateSession(session.SessionGUID);
    using (new RemoteLock((object) session2))
    {
      TechcardClientUtils.StartCreateRelations((IEnumerable<long>) longList.ToArray(), session);
      int columnIndex1 = childSostavData.Columns.IndexOf("F_PROJ_ID");
      try
      {
        int columnIndex2 = childSostavData.Columns.IndexOf("F_PRJLINK_ID");
        int columnIndex3 = childSostavData.Columns.IndexOf("F_RELATION_TYPE");
        int columnIndex4 = childSostavData.Columns.IndexOf("F_OBJECT_ID");
        int columnIndex5 = childSostavData.Columns.IndexOf("F_OBJECT_TYPE");
        foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
        {
          long int64_1 = Convert.ToInt64(row[columnIndex2]);
          long int64_2 = Convert.ToInt64(row[columnIndex1]);
          long int64_3 = Convert.ToInt64(row[columnIndex4]);
          if (int64_1 != 0L && int64_2 != 0L && int64_3 != 0L)
          {
            int int32 = Convert.ToInt32(row[columnIndex5]);
            int int32Value = DataSetProcessor.GetInt32Value(row, columnIndex3, TechCardConsts.RelTypes.ProektRelationID);
            ArtsCompositionsUtils.ArticleCreatedItem articleCreatedItem;
            IDBTypedObjectID projTypedObjId;
            ArtsCompositionsUtils.ArticleItemInfo articleItemInfo;
            if (dictionary2.TryGetValue(int64_2, out articleCreatedItem) && articleCreatedItem != null && dictionary3.TryGetValue(articleCreatedItem.TechObjID, out projTypedObjId) && dictionary1.TryGetValue(int64_2, out articleItemInfo))
            {
              ArtsCompositionsUtils.ArticleItemInfo artItemInfo = new ArtsCompositionsUtils.ArticleItemInfo(articleItemInfo.MainArtID, int64_2, int64_1, int32Value, int64_3, (MeasuredValue) null);
              if (artCompositionRelType != int32Value)
              {
                artCompositionRelType = int32Value;
                ArtsCompositionsUtils.GetAccessoryRelAttributes(artCompositionRelType, out attrIds1);
              }
              List<int> attrIds3;
              ArtsCompositionsUtils.GetAccessoryObjAttributes(int32, out attrIds3);
              ArtsCompositionsUtils.ArticleCreatedItem createdItem;
              if (ArtsCompositionsUtils.AddAccessoryItems(projTypedObjId, artItemInfo, session, out createdItem, attrIds1, attrIds3, session2))
              {
                if (!dictionary4.ContainsKey(int64_2))
                  dictionary4.Add(int64_2, createdItem.ProjLinkID);
                objCreated.Add(createdItem);
              }
            }
          }
        }
        session2.PartObjToSuppress.Clear();
        session2.RelationsToSuppress.Clear();
        foreach (KeyValuePair<long, long> keyValuePair in dictionary4)
          session2.NumerateObject(keyValuePair.Value, TechNumerationObjectModes.CurrentObj, session.SessionGUID);
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(session);
        customService.DisposeSession(session.SessionGUID);
      }
    }
    return true;
  }

  /// <summary>Добавление комплектующей единицы</summary>
  /// <param name="projTypedObjId">Описание сборочной единицы</param>
  /// <param name="artItemInfo">Описание изделия - прототипа для комплектующей единицы</param>
  /// <param name="session"></param>
  /// <param name="createdItem">Созданный объект</param>
  /// <remarks>При вызове из вне рекомендуется использовать TechcardClientUtils.StartCreateRelations/StopCreateRelations для корректной сортировки объектов</remarks>
  /// <returns></returns>
  public static bool AddAccessoryItems(
    IDBTypedObjectID projTypedObjId,
    ArtsCompositionsUtils.ArticleItemInfo artItemInfo,
    IUserSession session,
    out ArtsCompositionsUtils.ArticleCreatedItem createdItem)
  {
    List<int> attrIds1;
    ArtsCompositionsUtils.GetAccessoryObjAttributes(session.GetObjectInfo(artItemInfo.PartArtID).ObjectTypeID, out attrIds1);
    List<int> attrIds2;
    ArtsCompositionsUtils.GetAccessoryRelAttributes(artItemInfo.ProjRelTypeID, out attrIds2);
    return ArtsCompositionsUtils.AddAccessoryItems(projTypedObjId, artItemInfo, session, out createdItem, attrIds2, attrIds1, (ITechNumerationSession) null);
  }

  /// <summary>Добавление комплектующей единицы</summary>
  /// <param name="projTypedObjId">Описание сборочной единицы</param>
  /// <param name="artItemInfo">Описание изделия - прототипа для комплектующей единицы</param>
  /// <param name="session"></param>
  /// <param name="createdItem">Созданный объект</param>
  /// <param name="relAttrIds">Ид. типов копируемых атрибутов связей</param>
  /// <param name="objAttrIds">Ид. типов копируемых атрибутов объектов</param>
  /// <param name="numSession"></param>
  /// <remarks>При вызове из вне рекомендуется использовать TechcardClientUtils.StartCreateRelations/StopCreateRelations для корректной сортировки объектов</remarks>
  /// <returns></returns>
  public static bool AddAccessoryItems(
    IDBTypedObjectID projTypedObjId,
    ArtsCompositionsUtils.ArticleItemInfo artItemInfo,
    IUserSession session,
    out ArtsCompositionsUtils.ArticleCreatedItem createdItem,
    List<int> relAttrIds,
    List<int> objAttrIds,
    ITechNumerationSession numSession)
  {
    createdItem = (ArtsCompositionsUtils.ArticleCreatedItem) null;
    if (artItemInfo == null || artItemInfo.PartArtID == 0L || projTypedObjId == null || projTypedObjId.ObjectID == 0L || relAttrIds == null || objAttrIds == null)
      return false;
    IDBObject dbObject = session.GetObjectCollection(TechCardConsts.ObjectTypes.KomlEdinicaID).Create();
    if (dbObject == null)
      return false;
    ArtsCompositionsUtils.CopyObjectAttributes(dbObject, artItemInfo, objAttrIds, (List<AttributeValues>) null, session);
    numSession?.PartObjToSuppress.AddItem(dbObject.ObjectID);
    IDBRelationCollection relationCollection = session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
    IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) TechcardClientUtils.GetDBTypedObjectID(dbObject);
    IDBTypedObjectID projDbObjectId = projTypedObjId;
    IDBTypedObjectID partDbObjectId = dbTypedObjectId;
    IDBRelation relation = TechcardClientUtils.CreateRelation(relationCollection, projDbObjectId, partDbObjectId);
    if (relation == null)
      return false;
    ArtsCompositionsUtils.CopyRelationAttributes(relation, artItemInfo, relAttrIds, (List<AttributeValues>) null, session);
    numSession?.RelationsToSuppress.AddItem(relation.RelationID);
    if (dbObject.IsCreationMode)
    {
      dbObject.CommitCreation(false);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
        dbObject = dbObject.CheckOut();
    }
    MeasuredValue count = (MeasuredValue) null;
    if (artItemInfo.Count != null)
    {
      count = artItemInfo.Count;
    }
    else
    {
      IDBAttribute attributeById = relation.GetAttributeByID(TechCardConsts.AttributeTypes.CountAttrTypeID);
      if (attributeById != null)
        count = attributeById.Value as MeasuredValue;
    }
    createdItem = new ArtsCompositionsUtils.ArticleCreatedItem(artItemInfo.PartArtID, dbObject.ObjectID, dbObject.ObjectType, relation.RelationID, count);
    return true;
  }

  /// <summary>
  /// Получение списка копируемых атрибутов с изделия для собираемой единицы
  /// </summary>
  /// <param name="sourceObjTypeId">Ид. типа объекта - источника</param>
  /// <param name="attrIds"></param>
  /// <returns></returns>
  public static bool GetAssemblingObjAttributes(int sourceObjTypeId, out List<int> attrIds)
  {
    if (!TechCardConsts.Utils.GetCommonObjTypeAttrs(sourceObjTypeId, TechCardConsts.ObjectTypes.SobirEdinicaID, false, out attrIds))
      return false;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(TechCardConsts.ObjectTypes.SobirEdinicaID);
    if (objectType != null)
      objectType.FilterCopyAttributes((IList<int>) attrIds);
    return true;
  }

  /// <summary>
  /// Получение списка копируемых атрибутов с изделия для собираемой единицы
  /// </summary>
  /// <param name="attrIds"></param>
  /// <returns></returns>
  [Obsolete("Will be removed in IPS 6.0")]
  public static bool GetAssemblingObjAttributes(out List<int> attrIds)
  {
    return ArtsCompositionsUtils.GetAssemblingObjAttributes(TechCardConsts.ObjectTypes.ArticleBaseID, out attrIds);
  }

  /// <summary>
  /// Получение списка копируемых атрибутов с изделия для собираемой единицы
  /// </summary>
  /// <param name="attrIds"></param>
  /// <returns></returns>
  public static bool GetAssemblingRelAttributes(out List<int> attrIds)
  {
    attrIds = (List<int>) null;
    return true;
  }

  /// <summary>
  /// Получение списка копируемых атрибутов с изделия для компл. единицы
  /// </summary>
  /// <param name="sourceObjTypeId">Ид. типа объекта - источника</param>
  /// <param name="attrIds"></param>
  /// <returns></returns>
  public static bool GetAccessoryObjAttributes(int sourceObjTypeId, out List<int> attrIds)
  {
    if (!TechCardConsts.Utils.GetCommonObjTypeAttrs(sourceObjTypeId, TechCardConsts.ObjectTypes.KomlEdinicaID, false, out attrIds))
      return false;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(TechCardConsts.ObjectTypes.KomlEdinicaID);
    if (objectType != null)
      objectType.FilterCopyAttributes((IList<int>) attrIds);
    return true;
  }

  /// <summary>
  /// Получение списка копируемых атрибутов с изделия для компл. единицы
  /// </summary>
  /// <param name="attrIds"></param>
  /// <returns></returns>
  [Obsolete("Will be removed in IPS 6.0")]
  public static bool GetAccessoryObjAttributes(out List<int> attrIds)
  {
    attrIds = new List<int>();
    foreach (int artCompositionType in (IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes)
    {
      List<int> attrIds1;
      if (TechCardConsts.Utils.GetCommonObjTypeAttrs(artCompositionType, TechCardConsts.ObjectTypes.KomlEdinicaID, false, out attrIds1))
        attrIds.AddRange((IEnumerable<int>) attrIds1);
    }
    GenericListHelper.MakeUnique<int>(attrIds);
    return attrIds.Count > 0;
  }

  /// <summary>
  /// Получение списка копируемых атрибутов со связи состава изделия для связи комплектующего единицы с родителем
  /// </summary>
  /// <param name="attrIds"></param>
  /// <returns></returns>
  public static bool GetAccessoryRelAttributes(int artCompositionRelType, out List<int> attrIds)
  {
    if (artCompositionRelType == -1)
      artCompositionRelType = TechCardConsts.RelTypes.ProektRelationID;
    int num = TechCardConsts.Utils.GetCommonRelTypeAttrs(artCompositionRelType, TechCardConsts.RelTypes.TechRelationID, true, out attrIds) ? 1 : 0;
    if (attrIds == null)
      attrIds = new List<int>();
    IMSRelationType relationType = MetaDataHelper.GetRelationType(TechCardConsts.RelTypes.TechRelationID);
    if (relationType != null)
      relationType.FilterCopyAttributes((IList<int>) attrIds);
    if (attrIds.Contains(TechCardConsts.AttributeTypes.CountAttrTypeID))
      return num != 0;
    attrIds.Add(TechCardConsts.AttributeTypes.CountAttrTypeID);
    return num != 0;
  }

  /// <summary>Структура - описание добавляемого изделия</summary>
  public class ArticleItemInfo
  {
    /// <summary>Ид. версии головного изделия</summary>
    public readonly long MainArtID;
    /// <summary>Ид. версии родительского изделия</summary>
    public readonly long ProjArtID;
    /// <summary>Ид. связи с родительским изделием</summary>
    public readonly long ProjRelID;
    /// <summary>Ид. типа связи с родительским изделием</summary>
    public readonly int ProjRelTypeID;
    /// <summary>Ид. версии дочернего / добавляемого изделия</summary>
    public readonly long PartArtID;
    /// <summary>Кол-во изделий в составе</summary>
    public readonly MeasuredValue Count;

    /// <summary>Конструктор</summary>
    /// <param name="mainArtId">Ид. версии головного изделия</param>
    /// <param name="projArtId">Ид. версии родительского изделия</param>
    /// <param name="projRelId">Ид. связи с родительским изделием</param>
    /// <param name="projRelTypeId">Ид. типа связи с родительским изделием</param>
    /// <param name="partArtId">Ид. версии дочернего / добавляемого изделия</param>
    /// <param name="count">Кол-во изделий в составе</param>
    public ArticleItemInfo(
      long mainArtId,
      long projArtId,
      long projRelId,
      int projRelTypeId,
      long partArtId,
      MeasuredValue count)
    {
      this.MainArtID = mainArtId;
      this.ProjArtID = projArtId;
      this.ProjRelID = projRelId;
      this.ProjRelTypeID = projRelTypeId;
      this.PartArtID = partArtId;
      this.Count = count;
    }

    /// <summary>Конструктор</summary>
    /// <param name="mainArtId">Ид. версии дочернего / добавляемого изделия</param>
    /// <param name="partArtId">Ид. версии дочернего / добавляемого изделия</param>
    /// <param name="count">Кол-во изделий в составе</param>
    public ArticleItemInfo(long mainArtId, long partArtId, MeasuredValue count)
      : this(mainArtId, 0L, 0L, -1, partArtId, count)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="partArtId">Ид. версии дочернего / добавляемого изделия</param>
    /// <param name="count">Кол-во изделий в составе</param>
    public ArticleItemInfo(long partArtId, MeasuredValue count)
      : this(0L, partArtId, count)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="partArtId">Ид. версии дочернего / добавляемого изделия</param>
    public ArticleItemInfo(long partArtId)
      : this(partArtId, (MeasuredValue) null)
    {
    }
  }

  /// <summary>Структура - описание добавленного изделия</summary>
  public class ArticleCreatedItem
  {
    /// <summary>Ид. добавляемого изделия</summary>
    public readonly long ArtObjID;
    /// <summary>Ид. созданного объекта</summary>
    public long TechObjID;
    /// <summary>Ид. тип созданного объекта</summary>
    public readonly int TechObjTypeID;
    /// <summary>Ид. типа связи с род. объектом</summary>
    public readonly long ProjLinkID;
    /// <summary>Количество</summary>
    public readonly MeasuredValue Count;

    /// <summary>Конструктор</summary>
    /// <param name="artObjId">Ид. добавляемого изделия</param>
    /// <param name="techObjId">Ид. созданного объекта</param>
    /// <param name="techObjTypeId">Ид. тип созданного объекта</param>
    /// <param name="projLinkId">Ид. созданной</param>
    /// <param name="count">Количество</param>
    public ArticleCreatedItem(
      long artObjId,
      long techObjId,
      int techObjTypeId,
      long projLinkId,
      MeasuredValue count)
    {
      this.ArtObjID = artObjId;
      this.TechObjID = techObjId;
      this.TechObjTypeID = techObjTypeId;
      this.ProjLinkID = projLinkId;
      this.Count = count;
    }
  }
}
