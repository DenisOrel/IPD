// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechCardUtils
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Expert;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Summary description for TechCardUtils.</summary>
public static class TechCardUtils
{
  /// <summary>Парсим таблицу в список элементов состава</summary>
  /// <param name="sourceTable">Таблица с данными</param>
  /// <param name="columns"></param>
  /// <param name="sostavItems"></param>
  /// <returns></returns>
  public static bool ParseChildDataTable(
    DataTable sourceTable,
    Dictionary<string, ColumnDescriptor> columns,
    ref List<TechCardUtils.SostavSortedTreeItem> sostavItems)
  {
    if (sourceTable == null || sostavItems == null)
      return false;
    Dictionary<long, bool> dictionary = new Dictionary<long, bool>(sourceTable.Rows.Count);
    int columnIndex1 = -1;
    int columnIndex2 = -1;
    int columnIndex3 = -1;
    int columnIndex4 = -1;
    int columnIndex5 = -1;
    int num = -1;
    for (int index = 0; index < sourceTable.Columns.Count; ++index)
    {
      string columnName = sourceTable.Columns[index].ColumnName;
      int attributeId = DataHelper.GetAttributeID((object) columnName, false);
      if (columnIndex1 == -1 && (columnName == "F_PRJLINK_ID" || attributeId == -20))
        columnIndex1 = index;
      if (columnIndex2 == -1 && (columnName == "F_RELATION_TYPE" || attributeId == -23))
        columnIndex2 = index;
      if (columnIndex3 == -1 && (columnName == "F_PROJ_ID" || attributeId == -21))
        columnIndex3 = index;
      if (columnIndex4 == -1 && (columnName == "F_OBJECT_ID" || attributeId == -2))
        columnIndex4 = index;
      if (columnIndex5 == -1 && (columnName == "F_OBJECT_TYPE" || attributeId == -7))
        columnIndex5 = index;
      if (num == -1 && attributeId == TechCardConsts.AttributeTypes.SortAttrTypeID)
        num = index;
    }
    foreach (DataRow row in (InternalDataCollectionBase) sourceTable.Rows)
    {
      long int64_1 = Convert.ToInt64(row[columnIndex3]);
      long int64_2 = Convert.ToInt64(row[columnIndex1]);
      if (!dictionary.ContainsKey(int64_2))
      {
        dictionary.Add(int64_2, true);
        int int32_1 = Convert.ToInt32(row[columnIndex2]);
        long int64_3 = Convert.ToInt64(row[columnIndex4]);
        int int32_2 = Convert.ToInt32(row[columnIndex5]);
        long sortIdx = 0;
        if (num != -1)
        {
          try
          {
            sortIdx = row["cad00202-306c-11d8-b4e9-00304f19f545"].Equals((object) DBNull.Value) ? 0L : Convert.ToInt64(row["cad00202-306c-11d8-b4e9-00304f19f545"]);
          }
          catch (Exception ex)
          {
            if (!(ex is InvalidCastException))
              throw;
          }
        }
        TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem = new TechCardUtils.SostavSortedTreeItem(int64_1, int64_3, int32_2, int64_2, int32_1, sortIdx);
        sostavItems.Add(sostavSortedTreeItem);
        if (columns != null && columns.Count != 0)
        {
          foreach (string key in columns.Keys)
            sostavSortedTreeItem.Values.Add(key, row[key]);
        }
      }
    }
    return true;
  }

  /// <summary>Парсим таблицу в список элементов состава</summary>
  /// <param name="sourceTable">Таблица с данными</param>
  /// <param name="partInfoList">Список дочерних элементов</param>
  /// <param name="columns">Описания доп. столбцов</param>
  /// <param name="sostavItems">Список с элементами состава</param>
  /// <param name="linkIdList">Список ид. обработанных связей для обработки "зацикленных" составов</param>
  /// <returns></returns>
  public static bool ParseParentDataTable(
    DataTable sourceTable,
    List<ObjInfoItem> partInfoList,
    Dictionary<string, ColumnDescriptor> columns,
    ref List<TechCardUtils.SostavTreeItem> sostavItems,
    List<long> linkIdList)
  {
    if (sourceTable == null || partInfoList == null || partInfoList.Count == 0 || sostavItems == null)
      return false;
    List<long> objectIds = ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) partInfoList);
    objectIds.Sort();
    int columnIndex1 = -1;
    int columnIndex2 = -1;
    int num1 = -1;
    int columnIndex3 = -1;
    int columnIndex4 = -1;
    int columnIndex5 = -1;
    int num2 = -1;
    for (int index = 0; index < sourceTable.Columns.Count; ++index)
    {
      string columnName = sourceTable.Columns[index].ColumnName;
      int attributeId = DataHelper.GetAttributeID((object) columnName, false);
      if (columnIndex1 == -1 && (columnName == "F_PRJLINK_ID" || attributeId == -20))
        columnIndex1 = index;
      if (columnIndex2 == -1 && (columnName == "F_RELATION_TYPE" || attributeId == -23))
        columnIndex2 = index;
      if (num1 == -1 && (columnName == "F_PART_ID" || attributeId == -22))
        num1 = index;
      if (columnIndex3 == -1 && (columnName == "F_PROJ_ID" || attributeId == -21))
        columnIndex3 = index;
      if (columnIndex4 == -1 && columnName == DataHelper.Consts.cnt_fld_PartObjID)
        columnIndex4 = index;
      if (columnIndex5 == -1 && (columnName == "F_OBJECT_TYPE" || attributeId == -7))
        columnIndex5 = index;
      if (num2 == -1 && attributeId == TechCardConsts.AttributeTypes.SortAttrTypeID)
        num2 = index;
    }
    List<ObjInfoItem> partInfoList1 = new List<ObjInfoItem>(sourceTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) sourceTable.Rows)
    {
      long int64_1 = Convert.ToInt64(row[columnIndex4]);
      if (objectIds.BinarySearch(int64_1) >= 0)
      {
        long int64_2 = Convert.ToInt64(row[columnIndex1]);
        if (!linkIdList.Contains(int64_2))
        {
          linkIdList.Add(int64_2);
          long int64_3 = Convert.ToInt64(row[columnIndex3]);
          int int32_1 = Convert.ToInt32(row[columnIndex2]);
          int int32_2 = Convert.ToInt32(row[columnIndex5]);
          long sortIdx = 0;
          if (num2 != -1)
          {
            try
            {
              sortIdx = row["cad00202-306c-11d8-b4e9-00304f19f545"].Equals((object) DBNull.Value) ? 0L : Convert.ToInt64(row["cad00202-306c-11d8-b4e9-00304f19f545"]);
            }
            catch (Exception ex)
            {
              if (!(ex is InvalidCastException))
                throw;
            }
          }
          TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem = new TechCardUtils.SostavSortedTreeItem(int64_3, int64_1, int32_2, int64_2, int32_1, sortIdx);
          sostavItems.Add((TechCardUtils.SostavTreeItem) sostavSortedTreeItem);
          if (columns != null && columns.Count != 0)
          {
            foreach (string key in columns.Keys)
              sostavSortedTreeItem.Values.Add(key, row[key]);
          }
          partInfoList1.Add(new ObjInfoItem(int64_3, int32_2));
        }
      }
    }
    if (partInfoList1.Count < sourceTable.Rows.Count)
      TechCardUtils.ParseParentDataTable(sourceTable, partInfoList1, columns, ref sostavItems, linkIdList);
    return true;
  }

  /// <summary>
  /// Проверка на допустимость связи по направлению childObjTypeId "входит в" ownObjTypeId
  /// </summary>
  /// <param name="ownObjTypeId">Ид. типа родительского объекта</param>
  /// <param name="childObjTypeId">Ид. типа дочернего объекта</param>
  /// <param name="relTypeId">Ид. типа связи</param>
  /// <param name="session"></param>
  [Obsolete("Obsoleted. Will be removed in IPS 7.1. Use method without IUserSession", true)]
  public static bool CheckRelationApplicability(
    int ownObjTypeId,
    int childObjTypeId,
    int relTypeId,
    IUserSession session)
  {
    return TechCardUtils.CheckRelationApplicability(ownObjTypeId, childObjTypeId, relTypeId, false, true);
  }

  /// <summary>
  /// проверка на допустимость связи по направлению childObjTypeId "входит в" ownObjTypeId
  /// </summary>
  /// <param name="ownObjTypeId">Ид. типа родительского объекта</param>
  /// <param name="childObjTypeId">Ид. типа дочернего объекта</param>
  /// <param name="relTypeId">Ид. типа связи</param>
  /// <param name="session"></param>
  /// <param name="checkReverse">Проверить связь в обратном направлении</param>
  /// <param name="throwException"></param>
  [Obsolete("Obsoleted. Will be removed in IPS 7.1. Use method without IUserSession", true)]
  public static bool CheckRelationApplicability(
    int ownObjTypeId,
    int childObjTypeId,
    int relTypeId,
    IUserSession session,
    bool checkReverse,
    bool throwException)
  {
    return TechCardUtils.CheckRelationApplicability(ownObjTypeId, childObjTypeId, relTypeId, checkReverse, throwException);
  }

  /// <summary>
  /// Проверка на допустимость связи по направлению childObjTypeId "входит в" ownObjTypeId
  /// </summary>
  /// <param name="ownObjTypeId">Ид. типа родительского объекта</param>
  /// <param name="childObjTypeId">Ид. типа дочернего объекта</param>
  /// <param name="relTypeId">Ид. типа связи</param>
  public static void CheckRelationApplicability(
    int ownObjTypeId,
    int childObjTypeId,
    int relTypeId)
  {
    TechCardUtils.CheckRelationApplicability(ownObjTypeId, childObjTypeId, relTypeId, false, true);
  }

  /// <summary>
  /// проверка на допустимость связи по направлению childObjTypeId "входит в" ownObjTypeId
  /// </summary>
  /// <param name="ownObjTypeId">Ид. типа родительского объекта</param>
  /// <param name="childObjTypeId">Ид. типа дочернего объекта</param>
  /// <param name="relTypeId">Ид. типа связи</param>
  /// <param name="session"></param>
  /// <param name="checkReverse">Проверить связь в обратном направлении</param>
  /// <param name="throwException"></param>
  public static bool CheckRelationApplicability(
    int ownObjTypeId,
    int childObjTypeId,
    int relTypeId,
    bool checkReverse,
    bool throwException)
  {
    List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(ownObjTypeId, relTypeId);
    if (childObjectTypesId != null && childObjectTypesId.Contains(childObjTypeId))
      return true;
    if (childObjectTypesId != null)
    {
      foreach (int parentType in childObjectTypesId)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(childObjTypeId, parentType))
          return true;
      }
    }
    List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(ownObjTypeId);
    parentsIdReverse.Remove(ownObjTypeId);
    foreach (int ownObjTypeId1 in parentsIdReverse)
    {
      if (TechCardUtils.CheckRelationApplicability(ownObjTypeId1, childObjTypeId, relTypeId, checkReverse, throwException))
        return true;
    }
    if (checkReverse)
      return TechCardUtils.CheckRelationApplicability(childObjTypeId, ownObjTypeId, relTypeId, false, throwException);
    if (!throwException)
      return false;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(relTypeId);
    if (relationType == null)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.TechCard_3") + (object) relTypeId + LocalizationHolder.rm.GetString("Interfaces.TechCard_4"));
    IMSObjectType objectType1 = MetaDataHelper.GetObjectType(ownObjTypeId);
    if (objectType1 == null)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.TechCard_5") + (object) ownObjTypeId + LocalizationHolder.rm.GetString("Interfaces.TechCard_6"));
    IMSObjectType objectType2 = MetaDataHelper.GetObjectType(childObjTypeId);
    if (objectType2 == null)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.TechCard_7") + (object) childObjTypeId + LocalizationHolder.rm.GetString("Interfaces.TechCard_8"));
    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.TechCard_9"), (object) objectType2.ObjectName, (object) objectType1.ObjectName, (object) relationType.Description));
  }

  /// <summary>
  /// Раскрутка состава объекта вниз (Оставил в целях совместимости)
  /// </summary>
  /// <param name="projId"></param>
  /// <param name="userSession"></param>
  /// <param name="relations"></param>
  /// <returns></returns>
  public static List<TechCardUtils.SostavSortedTreeItem> GetChildSostavTree(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations)
  {
    return TechCardUtils.GetChildSostavTree(projId, userSession, relations, true);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavSortedTreeItem> GetChildSostavTree(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive)
  {
    return TechCardUtils.GetChildSostavTree(projId, userSession, relations, recursive, new ConditionStructure[0]);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Дополнительные условия на выбор объектов</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavSortedTreeItem> GetChildSostavTree(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    ConditionStructure[] conditions)
  {
    return TechCardUtils.GetChildSostavTree(projId, userSession, relations, recursive, conditions, (Dictionary<string, ColumnDescriptor>) null);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavSortedTreeItem> GetChildSostavTree(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    ConditionStructure[] conditions,
    Dictionary<string, ColumnDescriptor> columns)
  {
    return TechCardUtils.GetChildSostavTree(new ObjInfoItem(projId), userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projIdList">Идентификаторы объектов для которых надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavSortedTreeItem> GetChildSostavTree(
    IList<long> projIdList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    ConditionStructure[] conditions,
    Dictionary<string, ColumnDescriptor> columns)
  {
    return TechCardUtils.GetChildSostavTree(ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) projIdList), userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projInfo">Описание объекта (ид. версии / тип) для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavSortedTreeItem> GetChildSostavTree(
    ObjInfoItem projInfo,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    ConditionStructure[] conditions,
    Dictionary<string, ColumnDescriptor> columns)
  {
    if ((TypedInfoItem) projInfo == (TypedInfoItem) null || projInfo.ObjectID == 0L || projInfo.ObjectID == -1L)
      return new List<TechCardUtils.SostavSortedTreeItem>();
    return TechCardUtils.GetChildSostavTree(new List<ObjInfoItem>(1)
    {
      projInfo
    }, userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projInfoList">Описание объектов для которых надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <param name="tags"></param>
  /// <param name="filtrationOwnerId">Правило подбора версий</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavSortedTreeItem> GetChildSostavTree(
    List<ObjInfoItem> projInfoList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    ConditionStructure[] conditions,
    Dictionary<string, ColumnDescriptor> columns,
    HybridDictionary tags = null,
    string filtrationOwnerId = "")
  {
    List<TechCardUtils.SostavSortedTreeItem> sostavItems = new List<TechCardUtils.SostavSortedTreeItem>();
    if (projInfoList == null || projInfoList.Count == 0)
      return sostavItems;
    if (!(relations is int[] numArray1))
      numArray1 = relations != null ? relations.ToArray<int>() : (int[]) null;
    int[] numArray2 = numArray1;
    if (numArray2 == null || !((IEnumerable<int>) numArray2).Any<int>() || userSession == null)
      return sostavItems;
    projInfoList = SomeTypedInfoHelper<ObjInfoItem>.RemoveDuplicateEmpty(projInfoList);
    ColumnDescriptor[] columns1 = (ColumnDescriptor[]) null;
    if (columns != null && columns.Count != 0)
      columns1 = new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) columns.Values).ToArray();
    DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projInfoList, userSession, (IEnumerable<int>) numArray2, recursive, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns1, tags, filtrationOwnerId);
    if (childSostavData?.Rows == null)
      return sostavItems;
    TechCardUtils.ParseChildDataTable(childSostavData, columns, ref sostavItems);
    return sostavItems;
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partId">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavTreeItem> GetParentSostavTree(
    long partId,
    IUserSession userSession,
    int[] relations,
    bool recursive)
  {
    return TechCardUtils.GetParentSostavTree(partId, userSession, relations, recursive, new ConditionStructure[0]);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partId">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavTreeItem> GetParentSostavTree(
    long partId,
    IUserSession userSession,
    int[] relations,
    bool recursive,
    ConditionStructure[] conditions)
  {
    return TechCardUtils.GetParentSostavTree(partId, userSession, relations, recursive, new ConditionStructure[0], (Dictionary<string, ColumnDescriptor>) null);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partId">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavTreeItem> GetParentSostavTree(
    long partId,
    IUserSession userSession,
    int[] relations,
    bool recursive,
    ConditionStructure[] conditions,
    Dictionary<string, ColumnDescriptor> columns)
  {
    return partId == 0L ? new List<TechCardUtils.SostavTreeItem>() : TechCardUtils.GetParentSostavTree(new ObjInfoItem(partId), userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partIdList">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavTreeItem> GetParentSostavTree(
    List<long> partIdList,
    IUserSession userSession,
    int[] relations,
    bool recursive,
    ConditionStructure[] conditions,
    Dictionary<string, ColumnDescriptor> columns)
  {
    List<TechCardUtils.SostavTreeItem> sostavTreeItemList = new List<TechCardUtils.SostavTreeItem>();
    return partIdList == null || partIdList.Count == 0 || relations == null || relations.Length == 0 || userSession == null ? sostavTreeItemList : TechCardUtils.GetParentSostavTree(ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) partIdList), userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObjInfo">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavTreeItem> GetParentSostavTree(
    ObjInfoItem partObjInfo,
    IUserSession userSession,
    int[] relations,
    bool recursive,
    ConditionStructure[] conditions,
    Dictionary<string, ColumnDescriptor> columns)
  {
    if ((TypedInfoItem) partObjInfo == (TypedInfoItem) null || partObjInfo.ObjectID == 0L)
      return new List<TechCardUtils.SostavTreeItem>();
    return TechCardUtils.GetParentSostavTree(new List<ObjInfoItem>(1)
    {
      partObjInfo
    }, userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partInfoList">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>возвращается ArrayList, содержащий для каждого объекта состава
  /// структуру SostavTreeItem</returns>
  public static List<TechCardUtils.SostavTreeItem> GetParentSostavTree(
    List<ObjInfoItem> partInfoList,
    IUserSession userSession,
    int[] relations,
    bool recursive,
    ConditionStructure[] conditions,
    Dictionary<string, ColumnDescriptor> columns)
  {
    List<TechCardUtils.SostavTreeItem> sostavItems = new List<TechCardUtils.SostavTreeItem>();
    if (partInfoList == null || partInfoList.Count == 0 || relations == null || relations.Length == 0 || userSession == null)
      return sostavItems;
    List<ObjInfoItem> partObjList = SomeTypedInfoHelper<ObjInfoItem>.RemoveDuplicateEmpty(partInfoList);
    ColumnDescriptor[] columnDescriptorArray = (ColumnDescriptor[]) null;
    if (columns != null && columns.Count != 0)
      columnDescriptorArray = new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) columns.Values).ToArray();
    IUserSession userSession1 = userSession;
    int[] relations1 = relations;
    int num = recursive ? 1 : 0;
    ConditionStructure[] conditions1 = conditions;
    ColumnDescriptor[] columns1 = columnDescriptorArray;
    DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) partObjList, userSession1, (IEnumerable<int>) relations1, num != 0, (IEnumerable<ConditionStructure>) conditions1, (IEnumerable<ColumnDescriptor>) columns1);
    if (parentSostavData?.Rows == null)
      return sostavItems;
    List<long> linkIdList = new List<long>(parentSostavData.Rows.Count);
    TechCardUtils.ParseParentDataTable(parentSostavData, partInfoList, columns, ref sostavItems, linkIdList);
    return sostavItems;
  }

  /// <summary>
  /// Получение идентификатора техпроцесса в состав которого непосредственно входит указанный объект
  /// </summary>
  /// <param name="partObjectId">идентификатор объекта, для которого надо определить ТП</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns>возвращается идентификатор ТП или -1, если ТП не найден</returns>
  public static long GetParentTP(long partObjectId, IUserSession session)
  {
    long parentTp1 = 0;
    List<long> parentTp2 = TechCardUtils.GetParentTP(new List<long>()
    {
      partObjectId
    }, session, false);
    if (parentTp2.Count != 0)
      parentTp1 = parentTp2[0];
    return parentTp1;
  }

  /// <summary>
  /// Получение идентификаторов техпроцессов в состав которых непосредственно входят указанные объекты
  /// </summary>
  /// <param name="partObjectIDs">Идентификаторы объектов, для которых надо определить ТП</param>
  /// <param name="session"></param>
  /// <param name="collectAllTp">Флаг для получение всего списка ТП</param>
  /// <returns>Возвращаются идентификаторы ТП</returns>
  public static List<long> GetParentTP(
    List<long> partObjectIDs,
    IUserSession session,
    bool collectAllTp)
  {
    return TechCardUtils.GetParentObjects(partObjectIDs, session, TechCardConsts.ObjectTypes.TechProcBaseID, collectAllTp);
  }

  /// <summary>
  /// Получение идентификаторов род. объектов в состав которых входят указанные объекты
  /// </summary>
  /// <param name="partObjectIDs">Идентификаторы объектов, для которых надо определить ТП</param>
  /// <param name="session"></param>
  /// <param name="objTypeId">Ид. типа родительского объекта</param>
  /// <param name="collectAllTp">Флаг для получение всего списка ТП</param>
  /// <returns>Возвращаются идентификаторы найденных родительских объектов</returns>
  public static List<long> GetParentObjects(
    List<long> partObjectIDs,
    IUserSession session,
    int objTypeId,
    bool collectAllTp)
  {
    return TechCardUtils.GetParentObjects(partObjectIDs, session, MetaDataHelper.GetObjectTypeChildrenIDRecursive(objTypeId), collectAllTp);
  }

  /// <summary>
  /// Получение идентификаторов род. объектов в состав которых входят указанные объекты
  /// </summary>
  /// <param name="partObjectIDs">Идентификаторы объектов, для которых надо определить родителя</param>
  /// <param name="session"></param>
  /// <param name="parentObjTypeIds">Идентификаторы типов искомого родительского объекта</param>
  /// <param name="collectAllTp">Флаг для получение всего списка </param>
  /// <returns>Возвращаются идентификаторы найденных родительских объектов</returns>
  public static List<long> GetParentObjects(
    List<long> partObjectIDs,
    IUserSession session,
    List<int> parentObjTypeIds,
    bool collectAllTp)
  {
    List<long> parentObjects = new List<long>();
    if (partObjectIDs == null || partObjectIDs.Count == 0 || session == null)
      return parentObjects;
    List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(partObjectIDs, session, new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, false, (ConditionStructure[]) null, (Dictionary<string, ColumnDescriptor>) null);
    if (parentSostavTree == null || parentSostavTree.Count == 0)
      return parentObjects;
    List<long> partObjectIDs1 = new List<long>();
    foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
    {
      if (sostavTreeItem != null)
      {
        int objectTypeId = sostavTreeItem.ObjectTypeID;
        long projId = sostavTreeItem.ProjID;
        if (parentObjTypeIds.Contains(objectTypeId))
        {
          if (!parentObjects.Contains(projId))
            parentObjects.Add(projId);
        }
        else if (TechCardConsts.Utils.IsTechcardObjectType((object) objectTypeId))
          partObjectIDs1.Add(projId);
      }
    }
    if (partObjectIDs1.Count != 0 && (collectAllTp || parentObjects.Count == 0))
    {
      foreach (long parentObject in TechCardUtils.GetParentObjects(partObjectIDs1, session, parentObjTypeIds, collectAllTp))
      {
        if (parentObject != 0L && !parentObjects.Contains(parentObject))
          parentObjects.Add(parentObject);
      }
    }
    return parentObjects;
  }

  /// <summary>Get relation's guid table</summary>
  /// <param name="relInfoList">Описание связей</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns></returns>
  public static DataTable GetRelationId2GuidTable(
    List<RelInfoItem> relInfoList,
    IUserSession session)
  {
    DataTable toTable = (DataTable) null;
    if (relInfoList == null || relInfoList.Count == 0 || session == null)
      return (DataTable) null;
    GenericListHelper.MakeUnique<RelInfoItem>(relInfoList);
    Dictionary<int, List<RelInfoItem>> dictionary = new Dictionary<int, List<RelInfoItem>>();
    foreach (RelInfoItem relInfo in relInfoList)
    {
      List<RelInfoItem> relInfoItemList;
      if (!dictionary.TryGetValue(relInfo.RelTypeID, out relInfoItemList))
      {
        relInfoItemList = new List<RelInfoItem>();
        dictionary.Add(relInfo.RelTypeID, relInfoItemList);
      }
      relInfoItemList.Add(relInfo);
    }
    if (dictionary.ContainsKey(-1))
    {
      dictionary.Clear();
      dictionary.Add(-1, relInfoList);
    }
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    columnDescriptorList.Add(new ColumnDescriptor((object) -20, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -26, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -23, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    foreach (KeyValuePair<int, List<RelInfoItem>> keyValuePair in dictionary)
    {
      IDBRelationCollection relationCollection = session.GetRelationCollection(keyValuePair.Key);
      if (relationCollection != null)
      {
        List<RelInfoItem>[] relInfoItemListArray = GenericListHelper.SplitByChanks<RelInfoItem>((IList<RelInfoItem>) keyValuePair.Value, TechCardConsts.Consts.SQL_PACKET_SIZE);
        if (relInfoItemListArray != null)
        {
          foreach (List<RelInfoItem> relInfoItemList in relInfoItemListArray)
          {
            conditionStructureList.Clear();
            conditionStructureList.Add(new ConditionStructure(-20, RelationalOperators.In, (object) SomeTypedInfoHelper<TypedInfoItem>.GetItemIDs((IEnumerable<TypedInfoItem>) relInfoItemList.ToArray()).ToArray(), LogicalOperators.NONE, 0, false));
            DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray());
            relationCollection.LocalTypesMode = true;
            DataTable fromTable = relationCollection.Select(paramSet);
            if (fromTable != null)
            {
              if (toTable != null)
                DataSetProcessor.AddTable(toTable, fromTable, false);
              else
                toTable = fromTable;
            }
          }
        }
      }
    }
    toTable?.AcceptChanges();
    return toTable;
  }

  /// <summary>Get relation's guid</summary>
  /// <param name="relInfoList">Описание связей</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns></returns>
  public static Dictionary<Guid, RelInfoItem> GetRelationGuid2Id(
    List<RelInfoItem> relInfoList,
    IUserSession session)
  {
    Dictionary<Guid, RelInfoItem> relationGuid2Id = new Dictionary<Guid, RelInfoItem>();
    if (relInfoList == null || relInfoList.Count == 0 || session == null)
      return relationGuid2Id;
    DataTable relationId2GuidTable = TechCardUtils.GetRelationId2GuidTable(relInfoList, session);
    if (relationId2GuidTable != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) relationId2GuidTable.Rows)
      {
        if (row != null)
        {
          long int64 = Convert.ToInt64(row[0]);
          string str = row[1].ToString();
          int int32 = Convert.ToInt32(row[2]);
          if (GuidHelper.IsGuid(str))
          {
            Guid key = new Guid(str);
            if (!relationGuid2Id.ContainsKey(key))
              relationGuid2Id.Add(key, new RelInfoItem(int64, int32));
          }
        }
      }
    }
    return relationGuid2Id;
  }

  /// <summary>Копирование атрибутов объектов</summary>
  /// <param name="sourceDbRel">Связь - источник</param>
  /// <param name="targetDbRel">Связь - приемник</param>
  /// <param name="attrTypeIds">Список ид. копируемых атрибутов</param>
  /// <param name="throwException"></param>
  /// <returns></returns>
  public static bool CopyRelationAttributes(
    IDBRelation sourceDbRel,
    IDBRelation targetDbRel,
    int[] attrTypeIds,
    bool throwException = true)
  {
    if (sourceDbRel == null || targetDbRel == null || attrTypeIds == null || attrTypeIds.Length == 0)
      return false;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (int attrTypeId in attrTypeIds)
    {
      if (attrTypeId != 0)
      {
        IDBAttribute byId = sourceDbRel.Attributes.FindByID(attrTypeId);
        if (byId != null)
          attributeValuesList.Add(new AttributeValues(attrTypeId, byId.Value)
          {
            ThrowSetException = throwException
          });
      }
    }
    if (attributeValuesList.Count != 0)
      targetDbRel.SetAttributesValues(attributeValuesList.ToArray());
    return true;
  }

  /// <summary>Копирование атрибутов объектов</summary>
  /// <param name="sourceDbObj">Объект - источник</param>
  /// <param name="targetDbObj">Объект - приемник</param>
  /// <param name="attrTypeIds">Список ид. копируемых атрибутов</param>
  /// <param name="throwException"></param>
  /// <returns></returns>
  public static bool CopyObjectAttributes(
    IDBObject sourceDbObj,
    IDBObject targetDbObj,
    int[] attrTypeIds,
    bool throwException = true)
  {
    if (sourceDbObj == null || targetDbObj == null || attrTypeIds != null && attrTypeIds.Length == 0)
      return false;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AttributeValues attributesValue in sourceDbObj.GetAttributesValues(GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption))
    {
      if (attributesValue != null && (attrTypeIds == null || Array.IndexOf<int>(attrTypeIds, attributesValue.AttributeID) >= 0))
      {
        if (!throwException)
          attributesValue.ThrowSetException = false;
        attributeValuesList.Add(attributesValue);
      }
    }
    if (attributeValuesList.Count != 0)
      targetDbObj.SetAttributesValues(attributeValuesList.ToArray());
    return true;
  }

  /// <summary>Структура для описания элемента состава объекта</summary>
  [Serializable]
  public class SostavTreeItem
  {
    private Dictionary<string, object> _values;
    /// <summary>
    /// идентификатор версии объекта в который входит данный объект
    /// </summary>
    public long ProjID;
    /// <summary>
    /// идентификатор версии объекта который является входящим
    /// </summary>
    public long PartID;
    /// <summary>идентификатор связи, которой связаны ProjID и PartID</summary>
    public long LinkID;
    /// <summary>
    /// идентификатор типа связи, которой связаны ProjID и PartID
    /// </summary>
    public int LinkTypeID;
    /// <summary>
    /// Object type ID (Part's type ID - for child sostav tree/Proj's type ID - for parent sostav tree)
    /// </summary>
    public int ObjectTypeID;

    /// <summary>Конструктор</summary>
    /// <param name="projId">идентификатор версии объекта в который входит данный объект</param>
    /// <param name="partId">идентификатор версии объекта который является входящим</param>
    /// <param name="linkId">идентификатор связи, которой связаны ProjID и PartID</param>
    /// <param name="linkType">идентификатор типа связи, которой связаны ProjID и PartID</param>
    /// <param name="objTypeId"></param>
    public SostavTreeItem(long projId, long partId, long linkId, int linkType, int objTypeId)
    {
      this._values = new Dictionary<string, object>();
      this.ProjID = projId;
      this.PartID = partId;
      this.LinkID = linkId;
      this.LinkTypeID = linkType;
      this.ObjectTypeID = objTypeId;
    }

    /// <summary>Constructor</summary>
    /// <param name="projId"></param>
    /// <param name="partId"></param>
    /// <param name="linkId"></param>
    /// <param name="linkType"></param>
    public SostavTreeItem(long projId, long partId, long linkId, int linkType)
      : this(projId, partId, linkId, linkType, -1)
    {
    }

    /// <summary>Part/Project values (from custom columns)</summary>
    public Dictionary<string, object> Values
    {
      get => this._values;
      set => this._values = value;
    }
  }

  /// <summary>
  /// Структура для описания элемента сортированного объекта
  /// </summary>
  [Serializable]
  public class SostavSortedTreeItem : TechCardUtils.SostavTreeItem
  {
    /// <summary>Значение атрибута сортировки для связи</summary>
    public long SortIdx;

    /// <summary>Конструктор</summary>
    /// <param name="projId">идентификатор версии объекта в который входит данный объект</param>
    /// <param name="partId">идентификатор версии объекта который является входящим</param>
    /// <param name="partType">идентификатор типа объекта который является входящим</param>
    /// <param name="linkId">идентификатор связи, которой связаны ProjID и PartID</param>
    /// <param name="linkType">идентификатор типа связи, которой связаны ProjID и PartID</param>
    /// <param name="sortIdx">Значение атрибута сортировки для связи</param>
    public SostavSortedTreeItem(
      long projId,
      long partId,
      int partType,
      long linkId,
      int linkType,
      long sortIdx)
      : base(projId, partId, linkId, linkType, partType)
    {
      this.SortIdx = sortIdx;
    }

    /// <summary>
    /// идентификатор типа объекта который является входящим (for compatibility only)
    /// </summary>
    public int PartType
    {
      get => this.ObjectTypeID;
      set => this.ObjectTypeID = value;
    }
  }
}
