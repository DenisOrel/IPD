// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechCardObjUtils
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Expert;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>
/// 
/// </summary>
public static class TechCardObjUtils
{
  /// <summary>Techcard proc route utils</summary>
  public static class ProcRoute
  {
    /// <summary>
    /// Get ceh routes list for proc route (Now can be only one)
    /// </summary>
    /// <param name="procRouteObjId"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public static List<long> GetCehRouteID(long procRouteObjId, IUserSession session)
    {
      List<long> cehRouteId = new List<long>();
      int relationTypeId = MetaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechRelationGuid);
      int objectTypeId = MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.CehRouteGUID);
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId);
      if (!childrenIdRecursive.Contains(objectTypeId))
        childrenIdRecursive.Add(objectTypeId);
      ConditionStructure[] conditionStructureArray = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false)
      };
      long projId = procRouteObjId;
      IUserSession userSession = session;
      int[] relations = new int[1]{ relationTypeId };
      ConditionStructure[] conditions = conditionStructureArray;
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in TechCardUtils.GetChildSostavTree(projId, userSession, (IEnumerable<int>) relations, false, conditions))
      {
        long partId = sostavTreeItem.PartID;
        if (objectTypeId != 0)
          cehRouteId.Add(partId);
      }
      return cehRouteId;
    }
  }

  /// <summary>Article object utilities</summary>
  public static class Article
  {
    /// <summary>
    /// Получение списка ТП для изделия (без разворота его состава)
    /// </summary>
    /// <param name="objectId">Ид. версии изделия</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetTechProcList(long objectId, IUserSession session)
    {
      return TechCardObjUtils.Article.GetTechProcList(new ObjInfoItem(objectId), session);
    }

    /// <summary>
    /// Получение списка ТП для изделия (без разворота его состава)
    /// </summary>
    /// <param name="objectIDs">Ид. версий изделий</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetTechProcList(List<long> objectIDs, IUserSession session)
    {
      return TechCardObjUtils.Article.GetTechProcList(ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) objectIDs), session);
    }

    /// <summary>
    /// Получение списка ТП для изделия (без разворота его состава)
    /// </summary>
    /// <param name="objInfo">Ид. версии изделия</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetTechProcList(ObjInfoItem objInfo, IUserSession session)
    {
      return TechCardObjUtils.Article.GetTechProcList(new List<ObjInfoItem>(1)
      {
        objInfo
      }, session);
    }

    /// <summary>
    /// Получение списка ТП для изделия (без разворота его состава)
    /// </summary>
    /// <param name="objInfoList">Ид. версий изделий</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetTechProcList(
      List<ObjInfoItem> objInfoList,
      IUserSession session)
    {
      return TechCardObjUtils.Article.GetTechProcList(objInfoList, (int[]) null, session);
    }

    /// <summary>
    /// Получение списка ТП для изделия (без разворота его состава)
    /// </summary>
    /// <param name="objInfoList">Ид. версий изделий</param>
    /// <param name="techProcTypes">Ид. типов искомых ТП, если null или пусто - возвращаются все ТП</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetTechProcList(
      List<ObjInfoItem> objInfoList,
      int[] techProcTypes,
      IUserSession session)
    {
      List<ObjInfoItem> techProcList = new List<ObjInfoItem>();
      if (objInfoList == null || objInfoList.Count == 0 || session == null)
        return techProcList;
      objInfoList = SomeTypedInfoHelper<ObjInfoItem>.RemoveDuplicateEmpty(objInfoList);
      if (objInfoList.Count == 0)
        return techProcList;
      List<int> aList = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TechProcBaseID);
      List<int> resultData;
      if (techProcTypes != null && techProcTypes.Length != 0 && GenericListHelper.GetDifference<int>((IList<int>) aList, (IList<int>) new List<int>((IEnumerable<int>) techProcTypes), GenericListHelper.SearchMode.smExistInBoth, out resultData))
        aList = resultData;
      if (aList.Count == 0)
        return techProcList;
      DBRecordSetParams dbRsp = new DBRecordSetParams((ConditionStructure[]) null, new List<ColumnDescriptor>(2)
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      }.ToArray());
      DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) objInfoList, session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, 2, dbRsp, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null);
      if (childSostavData == null || childSostavData.Rows.Count == 0)
        return techProcList;
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
      {
        if (row != null)
        {
          int int32 = Convert.ToInt32(row[1]);
          if (aList.Contains(int32))
            techProcList.Add(new ObjInfoItem(Convert.ToInt64(row[0]), int32));
        }
      }
      return techProcList;
    }

    /// <summary>
    /// Поиск ид. версий изделий (первых найденных) для списка объектов
    /// </summary>
    /// <param name="objectId">Ид. версии объекта</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetArticles4Object(long objectId, IUserSession session)
    {
      return TechCardObjUtils.Article.GetArticles4Object(new ObjInfoItem(objectId), session);
    }

    /// <summary>
    /// Поиск ид. версий изделий (первых найденных) для списка объектов
    /// </summary>
    /// <param name="objInfo">Описание объектов для которых ищем деталь</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetArticles4Object(ObjInfoItem objInfo, IUserSession session)
    {
      return TechCardObjUtils.Article.GetArticles4Objects(new List<ObjInfoItem>()
      {
        objInfo
      }, session);
    }

    /// <summary>
    /// Поиск ид. версий изделий (первых найденных) для списка объектов
    /// </summary>
    /// <param name="objectIds">Ид. версий объектов для которых ищем деталь</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetArticles4Objects(List<long> objectIds, IUserSession session)
    {
      List<ObjInfoItem> articles4Objects = new List<ObjInfoItem>();
      if (objectIds == null || objectIds.Count == 0)
        return articles4Objects;
      List<ObjInfoItem> objInfoList = new List<ObjInfoItem>(objectIds.Count);
      foreach (long objectId in objectIds)
        objInfoList.Add(new ObjInfoItem(objectId));
      return TechCardObjUtils.Article.GetArticles4Objects(objInfoList, session);
    }

    /// <summary>
    /// Поиск ид. версий изделий (первых найденных) для списка объектов
    /// </summary>
    /// <param name="objInfoList">Описание объектов для которых ищем деталь</param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    public static List<ObjInfoItem> GetArticles4Objects(
      List<ObjInfoItem> objInfoList,
      IUserSession session)
    {
      List<long> analyzedIds = new List<long>();
      return TechCardObjUtils.Article.GetArticles4Objects(objInfoList, analyzedIds, session);
    }

    /// <summary>
    /// Поиск ид. версий изделий (первых найденных) для списка объектов
    /// </summary>
    /// <param name="objInfoList">Описание объектов для которых ищем деталь</param>
    /// <param name="analyzedIds">Ид. версий обработанных объектов (для исключения зацикливания) </param>
    /// <param name="session">Пользовательская сессия</param>
    /// <returns></returns>
    internal static List<ObjInfoItem> GetArticles4Objects(
      List<ObjInfoItem> objInfoList,
      List<long> analyzedIds,
      IUserSession session)
    {
      List<ObjInfoItem> articles4Objects = new List<ObjInfoItem>();
      if (objInfoList == null || objInfoList.Count == 0 || session == null)
        return articles4Objects;
      HashSet<int> hashSet = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes).ToHashSet<int>();
      List<ObjInfoItem> objInfoList1 = new List<ObjInfoItem>();
      foreach (ObjInfoItem objInfo in objInfoList)
      {
        if (objInfo.ObjTypeID == -1)
          objInfoList1.Add(objInfo);
        else if (hashSet.Contains(objInfo.ObjTypeID))
          articles4Objects.Add(objInfo);
      }
      if (articles4Objects.Count != 0)
        return articles4Objects;
      if (objInfoList1.Count > 0)
      {
        List<long> objectIds = ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) objInfoList1);
        ColumnDescriptor[] columns = new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -7, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
        };
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) hashSet.ToArray<int>(), LogicalOperators.NONE, 0, false)
        };
        DataTable objectData = DataHelper.GetObjectData(-1, session, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<long>) objectIds);
        if (objectData != null && objectData.Rows.Count != 0)
        {
          int columnIndex1 = objectData.Columns.IndexOf("F_OBJECT_ID");
          int columnIndex2 = objectData.Columns.IndexOf("F_OBJECT_TYPE");
          foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
            articles4Objects.Add(new ObjInfoItem(long.Parse(row[columnIndex1].ToString()), int.Parse(row[columnIndex2].ToString())));
          return articles4Objects;
        }
      }
      DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) objInfoList, session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) null);
      if (parentSostavData == null || parentSostavData.Rows.Count == 0)
        return articles4Objects;
      foreach (ObjInfoItem objInfo in objInfoList)
      {
        if (!((TypedInfoItem) objInfo == (TypedInfoItem) null) && objInfo.ObjectID != 0L)
          analyzedIds.Add(objInfo.ObjectID);
      }
      analyzedIds.Sort();
      List<ObjInfoItem> objInfoList2 = new List<ObjInfoItem>();
      int columnIndex3 = parentSostavData.Columns.IndexOf("F_PROJ_ID");
      int columnIndex4 = parentSostavData.Columns.IndexOf("F_OBJECT_TYPE");
      foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
      {
        long int64 = Convert.ToInt64(row[columnIndex3]);
        if (int64 != 0L)
        {
          int int32 = Convert.ToInt32(row[columnIndex4]);
          if (hashSet.Contains(int32))
            articles4Objects.Add(new ObjInfoItem(int64, int32));
          else if (analyzedIds.BinarySearch(int64) < 0)
            objInfoList2.Add(new ObjInfoItem(int64, int32));
        }
      }
      if (articles4Objects.Count != 0 || objInfoList2.Count == 0)
        return articles4Objects;
      articles4Objects = TechCardObjUtils.Article.GetArticles4Objects(objInfoList2, analyzedIds, session);
      return articles4Objects;
    }
  }
}
