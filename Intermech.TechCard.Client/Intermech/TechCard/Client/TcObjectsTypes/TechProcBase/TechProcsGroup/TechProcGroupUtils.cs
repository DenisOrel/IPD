// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.TechProcGroupUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Interfaces.TechCard.TechNumeration;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Remoting.Sponsors;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.Services.ClassifyObject;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>Summary description for TechProcGroupUtils.</summary>
public sealed class TechProcGroupUtils
{
  /// <summary>Abstract constructor to prevent class creation</summary>
  private TechProcGroupUtils()
  {
  }

  /// <summary>
  /// Получение режимов создания объекто ЕТП для соотв. обхектов ГТП
  /// </summary>
  /// <param name="objInfoList">Информация об объектах</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="obj2CreateInfoList">Режимы создания объектов</param>
  /// <returns>Режимы создания объектов</returns>
  public static bool GetEtpObjectCreatioModes(
    List<ObjInfoItem> objInfoList,
    IUserSession session,
    out Dictionary<long, ImbaseObjCreateInfo> obj2CreateInfoList)
  {
    obj2CreateInfoList = new Dictionary<long, ImbaseObjCreateInfo>();
    if (objInfoList == null || session == null)
      return false;
    GenericListHelper.MakeUnique<ObjInfoItem>(objInfoList);
    List<ObjInfoItem> objInfoList1 = new List<ObjInfoItem>(objInfoList.Count);
    foreach (ObjInfoItem objInfo in objInfoList)
    {
      bool flag = false;
      foreach (int compositionGtpNonCloneType in (IEnumerable<int>) TechCardConsts.ObjectTypes.TechCompositionGtpNonCloneTypes)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(objInfo.ObjTypeID, compositionGtpNonCloneType))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        obj2CreateInfoList[objInfo.ObjectID] = new ImbaseObjCreateInfo(objInfo.ObjTypeID, ImbaseObjCreateMode.iocmUseExists);
      else
        objInfoList1.Add(objInfo);
    }
    if (objInfoList1.Count == 0 || !(session.GetCustomService(typeof (IImbaseTechObjInfoService)) is IImbaseTechObjInfoService customService))
      return true;
    Dictionary<long, ImbaseObjCreateInfo> objCreateInfo;
    if (!customService.GetCreationMode((IDictionary<long, int>) ObjInfoHelper.GetObjectCache((IEnumerable<ObjInfoItem>) objInfoList1), session.SessionGUID, out objCreateInfo))
    {
      obj2CreateInfoList.Clear();
      return false;
    }
    foreach (KeyValuePair<long, ImbaseObjCreateInfo> keyValuePair in objCreateInfo)
      obj2CreateInfoList[keyValuePair.Key] = keyValuePair.Value;
    return true;
  }

  /// <summary>
  /// Получение списка ид. версий объектов ЕТП по ид. версии объекта группового ТП
  /// </summary>
  /// <param name="gtpObjInfo">Ид. версии объекта группового ТП</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Gtp2EtpRefData GetEtpObjIDList(ObjInfoItem gtpObjInfo, IUserSession session)
  {
    return TechProcGroupUtils.GetEtpObjIDList(gtpObjInfo, Guid.Empty, session);
  }

  /// <summary>
  /// Получение списка ид. версий объектов ЕТП по ид. версии объекта группового ТП
  /// </summary>
  /// <param name="gtpObjInfo">Ид. версии объекта группового ТП</param>
  /// <param name="etpObjType">Guid типа объекта единичного ТП</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Gtp2EtpRefData GetEtpObjIDList(
    ObjInfoItem gtpObjInfo,
    Guid etpObjType,
    IUserSession session)
  {
    List<ObjInfoItem> gtpObjInfoList = new List<ObjInfoItem>(1);
    gtpObjInfoList.Add(gtpObjInfo);
    Gtp2EtpRefData etpObjIdList = (Gtp2EtpRefData) null;
    if (!TechProcGroupUtils.GetEtpObjIDList(gtpObjInfoList, etpObjType, session).TryGetValue(gtpObjInfo, out etpObjIdList))
      etpObjIdList = new Gtp2EtpRefData((TypedInfoItem) gtpObjInfo, GtpRefDataType.gritGtpObject, (Dictionary<TypedInfoItem, TypedInfoItem>) null);
    return etpObjIdList;
  }

  /// <summary>
  /// Получение списка ид. версий объектов ЕТП по ид. версии объекта группового ТП
  /// </summary>
  /// <param name="gtpObjInfoList">Ид. версий объектов группового ТП</param>
  /// <param name="etpObjType">Guid типа объекта единичного ТП</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Dictionary<ObjInfoItem, Gtp2EtpRefData> GetEtpObjIDList(
    List<ObjInfoItem> gtpObjInfoList,
    Guid etpObjType,
    IUserSession session)
  {
    return TechProcGroupUtils.GetEtpObjIDList(gtpObjInfoList, new List<Guid>(1)
    {
      etpObjType
    }, session);
  }

  /// <summary>
  /// Получение списка ид. версий объектов ЕТП по ид. версии объекта группового ТП
  /// </summary>
  /// <param name="gtpObjInfoList">Ид. версий объектов группового ТП</param>
  /// <param name="etpObjTypes">Guidы типов объектов единичного ТП</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Dictionary<ObjInfoItem, Gtp2EtpRefData> GetEtpObjIDList(
    List<ObjInfoItem> gtpObjInfoList,
    List<Guid> etpObjTypes,
    IUserSession session)
  {
    Dictionary<ObjInfoItem, Gtp2EtpRefData> etpObjIdList = new Dictionary<ObjInfoItem, Gtp2EtpRefData>();
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    List<int> list = new List<int>();
    if (etpObjTypes != null)
    {
      foreach (Guid etpObjType in etpObjTypes)
      {
        if (!etpObjType.Equals(Guid.Empty))
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(etpObjType);
          list.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId));
          list.Add(objectTypeId);
        }
      }
      GenericListHelper.MakeUnique<int>(list);
      if (list.Count == 1)
        conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.Equal, (object) list[0], LogicalOperators.NONE, 0, false));
      else if (list.Count > 0)
        conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) list.ToArray(), LogicalOperators.NONE, 0, false));
    }
    List<TechCardUtils.SostavSortedTreeItem> childSostavTree = TechCardUtils.GetChildSostavTree(gtpObjInfoList, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechLinkGTPObjRelationID
    }, false, conditionStructureList.ToArray(), (Dictionary<string, ColumnDescriptor>) null);
    if (childSostavTree == null || childSostavTree.Count == 0)
      return etpObjIdList;
    Dictionary<long, ObjInfoItem> dictionary = new Dictionary<long, ObjInfoItem>(gtpObjInfoList.Count + childSostavTree.Count);
    foreach (ObjInfoItem gtpObjInfo in gtpObjInfoList)
      dictionary[gtpObjInfo.ObjectID] = gtpObjInfo;
    ObjInfoItem objInfoItem1 = (ObjInfoItem) null;
    Gtp2EtpRefData gtp2EtpRefData = (Gtp2EtpRefData) null;
    foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in childSostavTree)
    {
      if (sostavSortedTreeItem != null)
      {
        ObjInfoItem objInfoItem2 = new ObjInfoItem(sostavSortedTreeItem.PartID, sostavSortedTreeItem.ObjectTypeID);
        dictionary[objInfoItem2.ObjectID] = objInfoItem2;
        if ((list.Count == 0 || list.Contains(sostavSortedTreeItem.PartType)) && dictionary.TryGetValue(sostavSortedTreeItem.ProjID, out objInfoItem1))
        {
          if (!etpObjIdList.TryGetValue(objInfoItem1, out gtp2EtpRefData))
          {
            gtp2EtpRefData = new Gtp2EtpRefData((TypedInfoItem) objInfoItem1, GtpRefDataType.gritGtpObject, (Dictionary<TypedInfoItem, TypedInfoItem>) null);
            etpObjIdList.Add(objInfoItem1, gtp2EtpRefData);
          }
          gtp2EtpRefData.ObjRefIDs.Add((TypedInfoItem) new RelInfoItem(sostavSortedTreeItem.LinkID, sostavSortedTreeItem.LinkTypeID), (TypedInfoItem) objInfoItem2);
        }
      }
    }
    return etpObjIdList;
  }

  /// <summary>
  /// Получение списка ид. версий объектов ГТП по ид. версии объекта единичного ТП
  /// </summary>
  /// <param name="etpObjInfo">Ид. версии объекта единичного ТП</param>
  /// <param name="gtpObjType">Guid типа объекта группового ТП</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Gtp2EtpRefData GetGtpObjIDList(
    ObjInfoItem etpObjInfo,
    Guid gtpObjType,
    IUserSession session)
  {
    Dictionary<ObjInfoItem, Gtp2EtpRefData> gtpObjIdList1 = TechProcGroupUtils.GetGtpObjIDList(new List<ObjInfoItem>(1)
    {
      etpObjInfo
    }, gtpObjType, session);
    if (gtpObjIdList1 == null)
      return (Gtp2EtpRefData) null;
    Gtp2EtpRefData gtpObjIdList2 = (Gtp2EtpRefData) null;
    if (!gtpObjIdList1.TryGetValue(etpObjInfo, out gtpObjIdList2))
      gtpObjIdList2 = new Gtp2EtpRefData((TypedInfoItem) etpObjInfo, GtpRefDataType.gritEtpObject, (Dictionary<TypedInfoItem, TypedInfoItem>) null);
    return gtpObjIdList2;
  }

  /// <summary>
  /// Получение списка ид. версий объектов ГТП по ид. версий объектов единичного ТП
  /// </summary>
  /// <param name="etpObjInfoList">Ид. версий объекта единичного ТП</param>
  /// <param name="gtpObjType">Guid типа объекта группового ТП</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Dictionary<ObjInfoItem, Gtp2EtpRefData> GetGtpObjIDList(
    List<ObjInfoItem> etpObjInfoList,
    Guid gtpObjType,
    IUserSession session)
  {
    Dictionary<ObjInfoItem, Gtp2EtpRefData> gtpObjIdList = new Dictionary<ObjInfoItem, Gtp2EtpRefData>();
    List<int> conditionValue = (List<int>) null;
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    if (!gtpObjType.Equals(Guid.Empty))
    {
      int objectTypeId = MetaDataHelper.GetObjectTypeID(gtpObjType);
      conditionValue = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId);
      conditionValue.Add(objectTypeId);
      if (conditionValue.Count == 1)
        conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.Equal, (object) conditionValue[0], LogicalOperators.NONE, 0, false));
      else
        conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) conditionValue, LogicalOperators.NONE, 0, false));
    }
    List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(etpObjInfoList, session, new int[1]
    {
      TechCardConsts.RelTypes.TechLinkGTPObjRelationID
    }, false, conditionStructureList.ToArray(), (Dictionary<string, ColumnDescriptor>) null);
    if (parentSostavTree == null || parentSostavTree.Count == 0)
      return gtpObjIdList;
    Dictionary<long, ObjInfoItem> dictionary = new Dictionary<long, ObjInfoItem>(etpObjInfoList.Count + parentSostavTree.Count);
    foreach (ObjInfoItem etpObjInfo in etpObjInfoList)
      dictionary[etpObjInfo.ObjectID] = etpObjInfo;
    ObjInfoItem objInfoItem1 = (ObjInfoItem) null;
    Gtp2EtpRefData gtp2EtpRefData = (Gtp2EtpRefData) null;
    foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
    {
      if (sostavTreeItem != null)
      {
        ObjInfoItem objInfoItem2 = new ObjInfoItem(sostavTreeItem.ProjID, sostavTreeItem.ObjectTypeID);
        dictionary[objInfoItem2.ObjectID] = objInfoItem2;
        if ((gtpObjType.Equals(Guid.Empty) || conditionValue.Contains(sostavTreeItem.ObjectTypeID)) && !dictionary.TryGetValue(sostavTreeItem.PartID, out objInfoItem1))
        {
          if (!gtpObjIdList.TryGetValue(objInfoItem1, out gtp2EtpRefData))
          {
            gtp2EtpRefData = new Gtp2EtpRefData((TypedInfoItem) objInfoItem1, GtpRefDataType.gritEtpObject, (Dictionary<TypedInfoItem, TypedInfoItem>) null);
            gtpObjIdList.Add(objInfoItem1, gtp2EtpRefData);
          }
          gtp2EtpRefData.ObjRefIDs.Add((TypedInfoItem) new RelInfoItem(sostavTreeItem.LinkID, sostavTreeItem.LinkTypeID), (TypedInfoItem) objInfoItem2);
        }
      }
    }
    return gtpObjIdList;
  }

  /// <summary>Get etp relation's by according gtp relaton</summary>
  /// <param name="gtpRelInfo"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static List<Gtp2EtpRefData> GetEtpRelIDList(RelInfoItem gtpRelInfo, IUserSession session)
  {
    if ((TypedInfoItem) gtpRelInfo == (TypedInfoItem) null || session == null || gtpRelInfo.RelationID == 0L)
      return new List<Gtp2EtpRefData>();
    return TechProcGroupUtils.GetEtpRelIDList(new List<RelInfoItem>(1)
    {
      gtpRelInfo
    }, session);
  }

  /// <summary>Get etp relation's by according gtp relatons</summary>
  /// <param name="gtpRelInfoList"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static List<Gtp2EtpRefData> GetEtpRelIDList(
    List<RelInfoItem> gtpRelInfoList,
    IUserSession session)
  {
    List<Gtp2EtpRefData> etpRelIdList = new List<Gtp2EtpRefData>();
    if (gtpRelInfoList == null || gtpRelInfoList.Count == 0 || session == null)
      return etpRelIdList;
    Dictionary<Guid, RelInfoItem> relationGuid2Id = TechCardUtils.GetRelationGuid2Id(gtpRelInfoList, session);
    if (relationGuid2Id == null || relationGuid2Id.Count == 0)
      return etpRelIdList;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    columnDescriptorList.Add(new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) TechCardConsts.AttributeTypes.TechProcGroupRelAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    DataTable toTable = (DataTable) null;
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    List<Guid> guidList = new List<Guid>((IEnumerable<Guid>) relationGuid2Id.Keys);
    foreach (int compositionGtpRelation in (IEnumerable<int>) TechCardConsts.RelTypes.TechCompositionGtpRelations)
    {
      IDBRelationCollection relationCollection = session.GetRelationCollection(compositionGtpRelation);
      if (relationCollection != null)
      {
        relationCollection.LocalTypesMode = true;
        conditionStructureList.Clear();
        conditionStructureList.Add(new ConditionStructure(TechCardConsts.AttributeTypes.TechProcGroupRelAttrID, RelationalOperators.In, (object) guidList.ToArray(), LogicalOperators.NONE, 0, false));
        DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray());
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
    if (toTable == null)
      return etpRelIdList;
    int columnIndex1 = toTable.Columns.IndexOf("F_PRJLINK_ID");
    int columnIndex2 = toTable.Columns.IndexOf("F_RELATION_TYPE");
    int columnIndex3 = toTable.Columns.IndexOf("F_PROJ_ID");
    int columnIndex4 = toTable.Columns.IndexOf("F_OBJECT_ID");
    int columnIndex5 = toTable.Columns.IndexOf("F_OBJECT_TYPE");
    int columnIndex6 = toTable.Columns.IndexOf(TechCardConsts.AttributeTypes.TechProcGroupRelAttrGUID.ToString());
    foreach (DataRow row in (InternalDataCollectionBase) toTable.Rows)
    {
      if (row != null)
      {
        string str = row[columnIndex6].ToString();
        if (GuidHelper.IsGuid(str) && relationGuid2Id.ContainsKey(new Guid(str)))
        {
          long int64_1 = Convert.ToInt64(row[columnIndex1]);
          int int32_1 = Convert.ToInt32(row[columnIndex2]);
          long int64_2 = Convert.ToInt64(row[columnIndex3]);
          long int64_3 = Convert.ToInt64(row[columnIndex4]);
          int int32_2 = Convert.ToInt32(row[columnIndex5]);
          Gtp2EtpRefData gtp2EtpRefData = new Gtp2EtpRefData((TypedInfoItem) relationGuid2Id[new Guid(str)], GtpRefDataType.gritGtpRelation, (Dictionary<TypedInfoItem, TypedInfoItem>) null);
          ObjInfoItem objInfoItem1 = new ObjInfoItem(int64_2);
          ObjInfoItem objInfoItem2 = new ObjInfoItem(int64_3, int32_2);
          gtp2EtpRefData.ObjRefIDs.Add((TypedInfoItem) new RelObjInfoItem(int64_1, int32_1)
          {
            ProjInfo = objInfoItem1,
            PartInfo = objInfoItem2
          }, (TypedInfoItem) objInfoItem2);
          etpRelIdList.Add(gtp2EtpRefData);
        }
      }
    }
    return etpRelIdList;
  }

  /// <summary>
  /// Get etp relation's by according gtp relaton and root etp object Ids
  /// </summary>
  /// <remarks>Оставил для совместимости</remarks>
  /// <param name="gtpRelInfoList"></param>
  /// <param name="etpRootObjInfo"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static List<Gtp2EtpRefData> GetEtpRelIDList(
    List<RelInfoItem> gtpRelInfoList,
    ObjInfoItem etpRootObjInfo,
    IUserSession session)
  {
    List<Gtp2EtpRefData> etpRelIdList = new List<Gtp2EtpRefData>();
    if (gtpRelInfoList == null || gtpRelInfoList.Count == 0 || (TypedInfoItem) etpRootObjInfo == (TypedInfoItem) null || etpRootObjInfo.ObjectID == 0L || session == null)
      return etpRelIdList;
    Dictionary<Guid, RelInfoItem> relationGuid2Id = TechCardUtils.GetRelationGuid2Id(gtpRelInfoList, session);
    return relationGuid2Id == null || relationGuid2Id.Count == 0 ? etpRelIdList : TechProcGroupUtils.GetEtpRelIDList(relationGuid2Id, etpRootObjInfo, session);
  }

  /// <summary>
  /// Get etp relation's by according gtp relaton and root etp object Ids
  /// </summary>
  /// <param name="relGuid2InfoCache">Кеш гуид связи -&gt; ид. связи</param>
  /// <param name="etpRootObjInfo">Головной объект единичного ТП</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="filtrationOwnerId"></param>
  /// <returns></returns>
  public static List<Gtp2EtpRefData> GetEtpRelIDList(
    Dictionary<Guid, RelInfoItem> relGuid2InfoCache,
    ObjInfoItem etpRootObjInfo,
    IUserSession session,
    string filtrationOwnerId = null)
  {
    List<Gtp2EtpRefData> etpRelIdList = new List<Gtp2EtpRefData>();
    if (relGuid2InfoCache == null || relGuid2InfoCache.Count == 0 || (TypedInfoItem) etpRootObjInfo == (TypedInfoItem) null || etpRootObjInfo.ObjectID == 0L || session == null)
      return etpRelIdList;
    Dictionary<string, ColumnDescriptor> columns = new Dictionary<string, ColumnDescriptor>();
    string key1 = TechCardConsts.AttributeTypes.TechProcGroupRelAttrID.ToString();
    columns.Add(key1, new ColumnDescriptor((object) TechCardConsts.AttributeTypes.TechProcGroupRelAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    List<TechCardUtils.SostavSortedTreeItem> childSostavTree = TechCardUtils.GetChildSostavTree(((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      etpRootObjInfo
    }).ToList<ObjInfoItem>(), session, (IEnumerable<int>) TechCardConsts.RelTypes.TechCompositionGtpRelations, true, (ConditionStructure[]) null, columns, filtrationOwnerId: filtrationOwnerId);
    if (childSostavTree == null || childSostavTree.Count == 0)
      return etpRelIdList;
    RelInfoItem itemInfo = (RelInfoItem) null;
    foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in childSostavTree)
    {
      if (sostavSortedTreeItem != null)
      {
        string str = sostavSortedTreeItem.Values[key1].ToString();
        if (GuidHelper.IsGuid(str))
        {
          Guid key2 = new Guid(str);
          if (relGuid2InfoCache.TryGetValue(key2, out itemInfo))
          {
            Gtp2EtpRefData gtp2EtpRefData = new Gtp2EtpRefData((TypedInfoItem) itemInfo, GtpRefDataType.gritGtpRelation, (Dictionary<TypedInfoItem, TypedInfoItem>) null);
            ObjInfoItem objInfoItem1 = new ObjInfoItem(sostavSortedTreeItem.ProjID);
            ObjInfoItem objInfoItem2 = new ObjInfoItem(sostavSortedTreeItem.PartID, sostavSortedTreeItem.ObjectTypeID);
            gtp2EtpRefData.ObjRefIDs.Add((TypedInfoItem) new RelObjInfoItem(sostavSortedTreeItem.LinkID, sostavSortedTreeItem.LinkTypeID)
            {
              ProjInfo = objInfoItem1,
              PartInfo = objInfoItem2
            }, (TypedInfoItem) objInfoItem2);
            etpRelIdList.Add(gtp2EtpRefData);
          }
        }
      }
    }
    return etpRelIdList;
  }

  /// <summary>
  ///  Получение ид. версии объекта единичного ТП, по Ид. версии объекта группового ТП и ид. версии маршрута обработки (или другого родительского объекта)
  ///  (Рекурсивное раскрытие объектов маршрута использовать крайне осторожно)
  /// </summary>
  /// <param name="gtpObjInfo">Описание версии объекта группового ТП</param>
  /// <param name="procRouteObjInfo">ид. версии маршрута обработки (или другого родительского объекта)</param>
  /// <param name="etpObjTypeID">Ид. типа объекта ЕТП, если не задан - исполльзуем тип из gtpObjInfo</param>
  /// <param name="recursive">Разворачивать ли рекурсивно</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Gtp2EtpRefData GetEtpObjID(
    ObjInfoItem gtpObjInfo,
    ObjInfoItem procRouteObjInfo,
    int etpObjTypeID,
    bool recursive,
    IUserSession session)
  {
    Gtp2EtpRefData etpObjId = new Gtp2EtpRefData((TypedInfoItem) gtpObjInfo, GtpRefDataType.gritGtpObject, (Dictionary<TypedInfoItem, TypedInfoItem>) null);
    if ((TypedInfoItem) gtpObjInfo == (TypedInfoItem) null || gtpObjInfo.ObjectID == 0L || (TypedInfoItem) procRouteObjInfo == (TypedInfoItem) null || procRouteObjInfo.ObjectID == 0L)
      return etpObjId;
    Gtp2EtpRefData etpObjIdList = TechProcGroupUtils.GetEtpObjIDList(gtpObjInfo, session);
    if (etpObjIdList == null)
      return etpObjId;
    if (etpObjTypeID == 0)
      etpObjTypeID = gtpObjInfo.ObjTypeID;
    if (etpObjTypeID == 0)
      etpObjTypeID = session.GetObject(gtpObjInfo.ObjectID).ObjectType;
    List<int> expandObjectTypes = (List<int>) null;
    if (!recursive)
    {
      expandObjectTypes = new List<int>();
      expandObjectTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.MarshrObrabID));
      expandObjectTypes.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehBaseRouteID));
    }
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      procRouteObjInfo
    }, (IEnumerable<int>) null, (IEnumerable<int>) expandObjectTypes, (IEnumerable<int>) TechCardConsts.RelTypes.TechCompositionGtpRelations, (IEnumerable<ColumnDescriptor>) RelObjInfoDbScheme<ObjInfoItem>.GetSourceTableColumns().ToArray<ColumnDescriptor>(), (IEnumerable<ConditionStructure>) null, true, false, recursive ? -1 : 4, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
    DataTable source = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
    if (source == null)
      return etpObjId;
    List<RelObjInfoItem> objects = new List<RelObjInfoItem>();
    new RelObjInfoDbScheme<ObjInfoItem>(true).ParseInfoItems(session, source != null ? (IEnumerable<DataRow>) source.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<RelObjInfoItem>) objects);
    foreach (RelObjInfoItem relObjInfoItem in objects)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(relObjInfoItem.PartInfo.ObjTypeID, etpObjTypeID) && etpObjIdList.ObjRefIDs.ContainsValue((TypedInfoItem) relObjInfoItem.PartInfo))
        etpObjId.ObjRefIDs.Add((TypedInfoItem) new RelInfoItem(relObjInfoItem.RelationID, relObjInfoItem.RelTypeID), (TypedInfoItem) relObjInfoItem.PartInfo);
    }
    return etpObjId;
  }

  /// <summary>
  /// Получение ид. версии объекта единичного ТП, по ид. версии объекта группового ТП и ид. версии маршрута обработки
  /// </summary>
  /// <param name="gtpObjInfo">Ид. версии объекта группового ТП</param>
  /// <param name="procRouteObjInfo">Ид. версии маршрута обработки </param>
  /// <param name="recursive">Разворачивать ли рекурсивно</param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Gtp2EtpRefData GetEtpObjID(
    ObjInfoItem gtpObjInfo,
    ObjInfoItem procRouteObjInfo,
    bool recursive,
    IUserSession session)
  {
    return TechProcGroupUtils.GetEtpObjID(gtpObjInfo, procRouteObjInfo, -1, recursive, session);
  }

  /// <summary>
  ///  Получение ид. версии единичного ТП, по ид. версии группового ТП и ид. версии маршрута обработки
  /// </summary>
  /// <param name="gtpProcObjID">Ид. версии группового ТП </param>
  /// <param name="procRouteObjID">Ид. версии маршрута обработки </param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static Gtp2EtpRefData GetEtpProcObjID(
    ObjInfoItem gtpProcObjID,
    ObjInfoItem procRouteObjID,
    IUserSession session)
  {
    return TechProcGroupUtils.GetEtpObjID(gtpProcObjID, procRouteObjID, TechCardConsts.ObjectTypes.TechProcEdinID, false, session);
  }

  /// <summary>
  /// Получение списка головных объектов (в составе ЕТП), указанного типа, по связям единичных объектов
  /// </summary>
  /// <remarks>Поиск объектов происходит по иерархии структруры объектов ТП вверх</remarks>
  /// <param name="etpRelInfoList">Информация об объектах в составе ТП</param>
  /// <param name="searchObjType">Тип искомого объекта</param>
  /// <param name="session"></param>
  /// <param name="etpRel2ObjList">Результат в виде инф. по исходной связи -&gt; искомый объект вверх по составу</param>
  /// <returns></returns>
  public static bool GetEtpProcObjects(
    Dictionary<RelInfoItem, ObjInfoItem> etpRelInfoList,
    int searchObjType,
    IUserSession session,
    out Dictionary<RelInfoItem, ObjInfoItem> etpRel2ObjList)
  {
    etpRel2ObjList = (Dictionary<RelInfoItem, ObjInfoItem>) null;
    if (etpRelInfoList == null || etpRelInfoList.Count == 0 || session == null || searchObjType == -1)
      return false;
    etpRel2ObjList = new Dictionary<RelInfoItem, ObjInfoItem>(etpRelInfoList.Count);
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
    Dictionary<RelInfoItem, ObjInfoItem> dictionary = new Dictionary<RelInfoItem, ObjInfoItem>(etpRelInfoList.Count);
    foreach (KeyValuePair<RelInfoItem, ObjInfoItem> etpRelInfo in etpRelInfoList)
    {
      RelObjInfoItem key = etpRelInfo.Key as RelObjInfoItem;
      ObjInfoItem objInfoItem = !((TypedInfoItem) key != (TypedInfoItem) null) ? etpRelInfo.Value : key.ProjInfo ?? etpRelInfo.Value;
      if (!((TypedInfoItem) objInfoItem == (TypedInfoItem) null))
      {
        objInfoItemList.Add(objInfoItem);
        dictionary.Add(etpRelInfo.Key, objInfoItem);
      }
    }
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objInfoItemList, session);
    foreach (KeyValuePair<RelInfoItem, ObjInfoItem> keyValuePair in dictionary)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(keyValuePair.Value.ObjTypeID, searchObjType))
      {
        etpRel2ObjList.Add(keyValuePair.Key, keyValuePair.Value);
        objInfoItemList.Remove(keyValuePair.Value);
      }
    }
    if (objInfoItemList.Count == 0)
      return true;
    DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) objInfoItemList, session, (IEnumerable<int>) TechCardConsts.RelTypes.TechCompositionGtpRelations, true, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) null);
    if (parentSostavData == null || parentSostavData.Rows.Count == 0)
      return false;
    int columnIndex1 = parentSostavData.Columns.IndexOf("F_OBJECT_ID");
    int columnIndex2 = parentSostavData.Columns.IndexOf("F_OBJECT_TYPE");
    parentSostavData.Columns.IndexOf("F_PART_ID");
    List<long> longList = new List<long>();
    foreach (KeyValuePair<RelInfoItem, ObjInfoItem> etpRelInfo in etpRelInfoList)
    {
      if (!etpRel2ObjList.ContainsKey(etpRelInfo.Key))
      {
        RelObjInfoItem key = etpRelInfo.Key as RelObjInfoItem;
        DataRow[] dataRowArray1 = (TypedInfoItem) key != (TypedInfoItem) null ? parentSostavData.Select($"{DataHelper.Consts.cnt_fld_PartObjID}={(object) key.ProjInfo.ObjectID}") : parentSostavData.Select("F_PRJLINK_ID=" + (object) etpRelInfo.Key.RelationID);
        if (dataRowArray1 != null && dataRowArray1.Length != 0)
        {
          long int64 = Convert.ToInt64(dataRowArray1[0][columnIndex1]);
          int int32 = Convert.ToInt32(dataRowArray1[0][columnIndex2]);
          longList.Clear();
          longList.Add(int64);
          while (!MetaDataHelper.IsObjectTypeChildOf(int32, searchObjType))
          {
            DataRow[] dataRowArray2 = parentSostavData.Select($"{DataHelper.Consts.cnt_fld_PartObjID}={(object) int64}");
            if (dataRowArray2 != null && dataRowArray2.Length != 0)
            {
              int64 = Convert.ToInt64(dataRowArray2[0][columnIndex1]);
              int32 = Convert.ToInt32(dataRowArray2[0][columnIndex2]);
              if (!longList.Contains(int64))
                longList.Add(int64);
              else
                break;
            }
            else
              break;
          }
          if (MetaDataHelper.IsObjectTypeChildOf(int32, TechCardConsts.ObjectTypes.TechProcEdinID))
            etpRel2ObjList.Add(etpRelInfo.Key, new ObjInfoItem(int64, int32));
        }
      }
    }
    return etpRel2ObjList.Count > 0;
  }

  /// <summary>
  /// Диалог выбора связи объектов единичных тепроцессов / единичные техпроцессы, для соответственной связи ГТП
  /// </summary>
  /// <param name="gtpRelID">Ид. связи ГТП</param>
  /// <param name="caption"></param>
  /// <param name="dlgCaption"></param>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="etpRel2ObjList">Перечень выбранных связей</param>
  /// <returns>Кеш ид. родитльской связи =&gt; ид. родительского объекта</returns>
  public static bool GetEtpRelIDListByDialog(
    RelInfoItem gtpRelID,
    string caption,
    string dlgCaption,
    IUserSession session,
    out Dictionary<RelInfoItem, ObjInfoItem> etpRel2ObjList)
  {
    etpRel2ObjList = (Dictionary<RelInfoItem, ObjInfoItem>) null;
    if ((TypedInfoItem) gtpRelID == (TypedInfoItem) null || gtpRelID.RelationID == 0L || session == null)
      return false;
    List<Gtp2EtpRefData> etpRelIdList = TechProcGroupUtils.GetEtpRelIDList(gtpRelID, session);
    if (etpRelIdList == null || etpRelIdList.Count == 0)
      return false;
    etpRel2ObjList = new Dictionary<RelInfoItem, ObjInfoItem>();
    foreach (Gtp2EtpRefData gtp2EtpRefData in etpRelIdList)
    {
      if (gtp2EtpRefData != null)
      {
        foreach (KeyValuePair<TypedInfoItem, TypedInfoItem> objRefId in gtp2EtpRefData.ObjRefIDs)
        {
          if (!etpRel2ObjList.ContainsKey(objRefId.Key as RelInfoItem))
            etpRel2ObjList.Add(objRefId.Key as RelInfoItem, objRefId.Value as ObjInfoItem);
        }
      }
    }
    if (etpRel2ObjList.Count == 0)
      return false;
    Dictionary<RelInfoItem, ObjInfoItem> etpRel2ObjList1 = new Dictionary<RelInfoItem, ObjInfoItem>(etpRel2ObjList.Count);
    if (!TechProcGroupUtils.GetEtpProcObjects(etpRel2ObjList, TechCardConsts.ObjectTypes.TechProcEdinID, session, out etpRel2ObjList1) || etpRel2ObjList1.Count == 0)
      return false;
    List<long> longList = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.TechProcEdinID, (IDictionary<long, int>) ObjInfoHelper.GetObjectCache((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>((IEnumerable<ObjInfoItem>) etpRel2ObjList1.Values)), caption, dlgCaption);
    if (longList == null || longList.Count == 0)
      return false;
    longList.Sort();
    Dictionary<RelInfoItem, ObjInfoItem> dictionary = new Dictionary<RelInfoItem, ObjInfoItem>(longList.Count);
    foreach (KeyValuePair<RelInfoItem, ObjInfoItem> keyValuePair in etpRel2ObjList1)
    {
      if (longList.BinarySearch(keyValuePair.Value.ObjectID) >= 0)
        dictionary.Add(keyValuePair.Key, etpRel2ObjList[keyValuePair.Key]);
    }
    etpRel2ObjList = dictionary;
    return etpRel2ObjList.Count > 0;
  }

  /// <summary>Remove etp objects from techproc composition</summary>
  /// <param name="gtp2etpObjList"></param>
  /// <param name="session"></param>
  internal static void RemoveEtpObjects(
    List<Gtp2EtpRefObjData> gtp2etpObjList,
    IUserSession session)
  {
    if (gtp2etpObjList == null || gtp2etpObjList.Count == 0 || session == null)
      return;
    Dictionary<long, ImbaseObjCreateInfo> obj2CreateInfoList = (Dictionary<long, ImbaseObjCreateInfo>) null;
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
    foreach (Gtp2EtpRefObjData gtp2etpObj in gtp2etpObjList)
    {
      if (gtp2etpObj != null && gtp2etpObj.ItemType == GtpRefDataType.gritGtpRelation && gtp2etpObj.SostavItem != null)
        objInfoList.Add(new ObjInfoItem(gtp2etpObj.SostavItem.PartID, gtp2etpObj.SostavItem.ObjectTypeID));
    }
    TechProcGroupUtils.GetEtpObjectCreatioModes(objInfoList, session, out obj2CreateInfoList);
    List<ObjInfoItem> projObjList = new List<ObjInfoItem>();
    foreach (Gtp2EtpRefObjData gtp2etpObj in gtp2etpObjList)
    {
      if (gtp2etpObj != null && gtp2etpObj.ItemType == GtpRefDataType.gritGtpRelation)
      {
        ImbaseObjCreateMode imbaseObjCreateMode = ImbaseObjCreateMode.iocmCreateNew;
        long num = gtp2etpObj.SostavItem.PartID;
        if (obj2CreateInfoList != null && obj2CreateInfoList.ContainsKey(num))
          imbaseObjCreateMode = obj2CreateInfoList[num].CreateMode;
        foreach (KeyValuePair<TypedInfoItem, TypedInfoItem> objRefId in gtp2etpObj.ObjRefIDs)
        {
          RelInfoItem key = objRefId.Key as RelInfoItem;
          if (!((TypedInfoItem) key == (TypedInfoItem) null) && key.RelationID != 0L)
          {
            projObjList.Add(objRefId.Value as ObjInfoItem);
            switch (imbaseObjCreateMode)
            {
              case ImbaseObjCreateMode.iocmUnknown:
              case ImbaseObjCreateMode.iocmCreateNew:
              case ImbaseObjCreateMode.iocmUseExists:
                ObjInfoItem objInfoItem = objRefId.Value as ObjInfoItem;
                if (!((TypedInfoItem) objInfoItem == (TypedInfoItem) null) && objInfoItem.ObjectID != 0L)
                {
                  IMSApplicability applicability = MetaDataHelper.GetApplicability(gtp2etpObj.SostavItem.ObjectTypeID, objInfoItem.ObjTypeID, TechCardConsts.RelTypes.TechLinkGTPObjRelationID);
                  if (applicability != null && applicability.IsContent && num > 0L)
                  {
                    IDBObject dbObject1 = session.GetObject(num, false);
                    if (dbObject1 != null)
                    {
                      switch (dbObject1.ObjectModifyMode)
                      {
                        case ObjectModifyModes.Checkout:
                        case ObjectModifyModes.CreateVersion:
                        case ObjectModifyModes.CantModify:
                          IDBObject dbObject2 = dbObject1.CheckOut();
                          gtp2etpObj.SostavItem.PartID = dbObject2.ObjectID;
                          break;
                      }
                      num = 0L;
                      continue;
                    }
                    continue;
                  }
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
    DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, session, (IEnumerable<int>) TechCardConsts.RelTypes.TechCompositionGtpRelations, true, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.CreateNewObjectAttGUID), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    }, (HybridDictionary) null);
    List<ObjInfoItem> partObjList = new List<ObjInfoItem>();
    if (childSostavData != null)
    {
      int columnIndex1 = childSostavData.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex2 = childSostavData.Columns.IndexOf("F_OBJECT_TYPE");
      int columnIndex3 = childSostavData.Columns.IndexOf(Intermech.Imbase.Consts.CreateNewObjectAttGUID.ToString());
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
      {
        object obj = row[columnIndex3];
        int int32 = Convert.ToInt32(row[columnIndex2]);
        IMSApplicability applicability = MetaDataHelper.GetApplicability(int32, int32, TechCardConsts.RelTypes.TechLinkGTPObjRelationID);
        if (applicability != null && applicability.IsContent)
          partObjList.Add(new ObjInfoItem(Convert.ToInt64(row[columnIndex1]), int32));
      }
    }
    DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) partObjList, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechLinkGTPObjRelationID
    }, false, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) null, (HybridDictionary) null);
    if (parentSostavData != null)
    {
      int columnIndex = parentSostavData.Columns.IndexOf("F_OBJECT_ID");
      foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
      {
        long int64 = Convert.ToInt64(row[columnIndex]);
        if (int64 >= 0L)
        {
          IDBObject objectActualCopy = session.GetObjectActualCopy(int64, false);
          if (objectActualCopy != null)
          {
            switch (objectActualCopy.ObjectModifyMode)
            {
              case ObjectModifyModes.Checkout:
              case ObjectModifyModes.CreateVersion:
              case ObjectModifyModes.CantModify:
                objectActualCopy.CheckOut();
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
    List<IDBObject> dbObjectList = new List<IDBObject>();
    foreach (Gtp2EtpRefObjData gtp2etpObj in gtp2etpObjList)
    {
      if (gtp2etpObj != null && gtp2etpObj.ItemType == GtpRefDataType.gritGtpRelation)
      {
        ImbaseObjCreateMode imbaseObjCreateMode = ImbaseObjCreateMode.iocmCreateNew;
        long num = gtp2etpObj.SostavItem.PartID;
        if (obj2CreateInfoList != null && obj2CreateInfoList.ContainsKey(num))
          imbaseObjCreateMode = obj2CreateInfoList[num].CreateMode;
        foreach (KeyValuePair<TypedInfoItem, TypedInfoItem> objRefId in gtp2etpObj.ObjRefIDs)
        {
          RelInfoItem key = objRefId.Key as RelInfoItem;
          if (!((TypedInfoItem) key == (TypedInfoItem) null) && key.RelationID != 0L)
          {
            IDBRelation relation = session.GetRelation(key.RelationID, false);
            if (relation != null)
            {
              IDBObject objectActualCopy1 = session.GetObjectActualCopy(relation.ProjID, false);
              if (objectActualCopy1 != null)
              {
                IMSApplicability applicability1 = MetaDataHelper.GetApplicability(objectActualCopy1.ObjectType, objRefId.Value.ItemTypeID, key.ItemTypeID);
                if (applicability1 != null)
                {
                  if (applicability1.IsContent && objectActualCopy1.ObjectID > 0L)
                  {
                    switch (objectActualCopy1.ObjectModifyMode)
                    {
                      case ObjectModifyModes.Checkout:
                      case ObjectModifyModes.CreateVersion:
                      case ObjectModifyModes.CantModify:
                        objectActualCopy1.CheckOut();
                        break;
                    }
                  }
                  switch (imbaseObjCreateMode)
                  {
                    case ImbaseObjCreateMode.iocmUnknown:
                    case ImbaseObjCreateMode.iocmCreateNew:
                      ObjInfoItem objInfoItem = objRefId.Value as ObjInfoItem;
                      if (!((TypedInfoItem) objInfoItem == (TypedInfoItem) null) && objInfoItem.ObjectID != 0L)
                      {
                        IDBObject objectActualCopy2 = session.GetObjectActualCopy(objInfoItem.ObjectID, false);
                        if (objectActualCopy2 != null)
                        {
                          IMSApplicability applicability2 = MetaDataHelper.GetApplicability(gtp2etpObj.SostavItem.ObjectTypeID, objectActualCopy2.ObjectType, TechCardConsts.RelTypes.TechLinkGTPObjRelationID);
                          if (applicability2 != null && applicability2.IsContent && num > 0L)
                          {
                            IDBObject objectActualCopy3 = session.GetObjectActualCopy(num, false);
                            if (objectActualCopy3 != null)
                            {
                              switch (objectActualCopy3.ObjectModifyMode)
                              {
                                case ObjectModifyModes.Checkout:
                                case ObjectModifyModes.CreateVersion:
                                case ObjectModifyModes.CantModify:
                                  objectActualCopy3.CheckOut();
                                  break;
                              }
                              num = 0L;
                            }
                          }
                          dbObjectList.Add(objectActualCopy2);
                          if (objInfoItem.ObjectID < 0L)
                          {
                            IDBObject dbObject = session.GetObject(Math.Abs(objInfoItem.ObjectID), false);
                            if (dbObject != null)
                            {
                              dbObjectList.Add(dbObject);
                              continue;
                            }
                            continue;
                          }
                          continue;
                        }
                        continue;
                      }
                      continue;
                    case ImbaseObjCreateMode.iocmUseExists:
                      relation.Delete(0L);
                      continue;
                    default:
                      continue;
                  }
                }
              }
            }
          }
        }
        gtp2etpObj.ObjRefIDs.Clear();
      }
    }
    foreach (IDBObject dbObject in dbObjectList)
      session.GetObject(dbObject.ObjectID, false)?.Delete(0L);
  }

  /// <summary>Create and link etp objects</summary>
  /// <remarks>Create etp obejct, link to parent and group objects</remarks>
  /// <param name="gtpItemList">Gtp objects - part of gtp2etpObjList</param>
  /// <param name="gtp2etpObjList">Gtp object's compositions with links to etp objects (if exist) </param>
  /// <param name="session"></param>
  /// <param name="recursive">Recursive flag</param>
  /// <returns></returns>
  internal static bool CreateEtpObject(
    List<Gtp2EtpRefObjData> gtpItemList,
    List<Gtp2EtpRefObjData> gtp2etpObjList,
    IUserSession session,
    bool recursive)
  {
    if (gtpItemList == null || gtpItemList.Count == 0 || gtp2etpObjList == null || gtp2etpObjList.Count == 0 || session == null)
      return false;
    List<Gtp2EtpRefObjData> gtp2EtpRefObjDataList = new List<Gtp2EtpRefObjData>(gtpItemList.Count);
    foreach (Gtp2EtpRefObjData gtpItem in gtpItemList)
    {
      if (gtpItem != null && gtpItem.SostavItem != null && gtpItem.ItemType == GtpRefDataType.gritGtpRelation && gtpItem.ObjRefIDs.Count == 0)
        gtp2EtpRefObjDataList.Add(gtpItem);
    }
    bool etpObject = false;
    if (!recursive && gtp2EtpRefObjDataList.Count == 0)
      return etpObject;
    Dictionary<long, Gtp2EtpRefObjData> dictionary1 = new Dictionary<long, Gtp2EtpRefObjData>(gtp2etpObjList.Count);
    foreach (Gtp2EtpRefObjData gtp2etpObj in gtp2etpObjList)
    {
      if (gtp2etpObj != null)
      {
        if (gtp2etpObj.ItemType == GtpRefDataType.gritGtpRelation)
        {
          if (gtp2etpObj.SostavItem != null && !dictionary1.ContainsKey(gtp2etpObj.SostavItem.PartID))
            dictionary1.Add(gtp2etpObj.SostavItem.PartID, gtp2etpObj);
        }
        else if (gtp2etpObj.ItemType == GtpRefDataType.gritGtpObject && !dictionary1.ContainsKey(gtp2etpObj.ItemInfo.ItemID))
          dictionary1.Add(gtp2etpObj.ItemInfo.ItemID, gtp2etpObj);
      }
    }
    Dictionary<long, Gtp2EtpRefObjData> dictionary2 = new Dictionary<long, Gtp2EtpRefObjData>(gtp2EtpRefObjDataList.Count);
    Gtp2EtpRefObjData gtp2EtpRefObjData1 = (Gtp2EtpRefObjData) null;
    foreach (Gtp2EtpRefObjData gtp2EtpRefObjData2 in gtp2EtpRefObjDataList)
    {
      if (!dictionary2.ContainsKey(gtp2EtpRefObjData2.SostavItem.ProjID) && dictionary1.TryGetValue(gtp2EtpRefObjData2.SostavItem.ProjID, out gtp2EtpRefObjData1))
        dictionary2.Add(gtp2EtpRefObjData2.SostavItem.ProjID, gtp2EtpRefObjData1);
    }
    if (!recursive && dictionary2.Count == 0)
      return etpObject;
    List<Gtp2EtpRefObjData> gtpItemList1 = new List<Gtp2EtpRefObjData>(dictionary2.Count);
    foreach (Gtp2EtpRefObjData gtp2EtpRefObjData3 in dictionary2.Values)
    {
      if (gtp2EtpRefObjData3.ObjRefIDs.Count == 0)
        gtpItemList1.Add(gtp2EtpRefObjData3);
    }
    if (gtpItemList1.Count != 0)
    {
      TechProcGroupUtils.CreateEtpObject(gtpItemList1, gtp2etpObjList, session, false);
      foreach (Gtp2EtpRefObjData gtp2EtpRefObjData4 in gtpItemList1)
      {
        if (gtp2EtpRefObjData4.ObjRefIDs.Count == 0)
        {
          switch (gtp2EtpRefObjData4.ItemType)
          {
            case GtpRefDataType.gritGtpObject:
              dictionary2.Remove(gtp2EtpRefObjData4.ItemInfo.ItemID);
              continue;
            case GtpRefDataType.gritGtpRelation:
              if (gtp2EtpRefObjData4.SostavItem != null)
              {
                dictionary2.Remove(gtp2EtpRefObjData4.SostavItem.PartID);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
    if (!recursive && dictionary2.Count == 0)
      return etpObject;
    Dictionary<long, ImbaseObjCreateInfo> obj2CreateInfoList = new Dictionary<long, ImbaseObjCreateInfo>(gtp2EtpRefObjDataList.Count);
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>(gtp2EtpRefObjDataList.Count);
    foreach (Gtp2EtpRefObjData gtp2EtpRefObjData5 in gtp2EtpRefObjDataList)
      objInfoList.Add(new ObjInfoItem(gtp2EtpRefObjData5.SostavItem.PartID, gtp2EtpRefObjData5.SostavItem.ObjectTypeID));
    if (!TechProcGroupUtils.GetEtpObjectCreatioModes(objInfoList, session, out obj2CreateInfoList))
      return etpObject;
    ITechNumerationSession session1 = session.GetCustomService(typeof (ITechNumerationService)) is ITechNumerationService customService1 ? customService1.CreateSession(session.SessionGUID) : (ITechNumerationSession) null;
    using (new RemoteLock((object) session1))
    {
      try
      {
        if (!(session.GetCustomService(typeof (ITechUtilsService)) is ITechUtilsService customService2))
        {
          string caption = LocalizationHolder.rm.GetString("TechCard.Client_138");
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_370"), (object) typeof (ITechUtilsService).ToString()), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return etpObject;
        }
        foreach (Gtp2EtpRefObjData gtp2EtpRefObjData6 in gtp2EtpRefObjDataList)
        {
          ImbaseObjCreateInfo imbaseObjCreateInfo;
          if (gtp2EtpRefObjData6.ObjRefIDs.Count == 0 && obj2CreateInfoList.TryGetValue(gtp2EtpRefObjData6.SostavItem.PartID, out imbaseObjCreateInfo) && dictionary2.TryGetValue(gtp2EtpRefObjData6.SostavItem.ProjID, out gtp2EtpRefObjData1))
          {
            ObjInfoItem objInfoItem1 = new ObjInfoItem(gtp2EtpRefObjData6.SostavItem.PartID, gtp2EtpRefObjData6.SostavItem.ObjectTypeID);
            ObjInfoItem objInfoItem2 = (ObjInfoItem) null;
            IDBObject dbObject1 = (IDBObject) null;
            switch (imbaseObjCreateInfo.CreateMode)
            {
              case ImbaseObjCreateMode.iocmCreateNew:
                if (!((TypedInfoItem) objInfoItem1 == (TypedInfoItem) null) && objInfoItem1.ObjectID != 0L)
                {
                  dbObject1 = customService2.CreateObject(objInfoItem1.ObjTypeID, objInfoItem1.ObjectID, session.SessionGUID);
                  objInfoItem2 = new ObjInfoItem(dbObject1);
                  IMSApplicability applicability = MetaDataHelper.GetApplicability(objInfoItem1.ObjTypeID, objInfoItem2.ObjTypeID, TechCardConsts.RelTypes.TechLinkGTPObjRelationID);
                  if (applicability != null && applicability.IsContent && objInfoItem1.ObjectID > 0L)
                  {
                    IDBObject dbObject2 = session.GetObject(objInfoItem1.ObjectID);
                    switch (dbObject2.ObjectModifyMode)
                    {
                      case ObjectModifyModes.Checkout:
                      case ObjectModifyModes.CreateVersion:
                      case ObjectModifyModes.CantModify:
                        objInfoItem1 = new ObjInfoItem(dbObject2.CheckOut());
                        break;
                    }
                  }
                  List<IDBRelation> relations = TechcardClientUtils.CreateRelations(session, objInfoItem2.ObjectID, new int[1]
                  {
                    TechCardConsts.RelTypes.TechLinkGTPObjRelationID
                  }, new long[1]{ objInfoItem1.ObjectID }, DateTime.Now, TechCreateRelMode.tcrmEnterIn);
                  if (relations != null && relations.Count > 0)
                  {
                    gtp2EtpRefObjData6.ObjRefIDs.Add((TypedInfoItem) new RelInfoItem(relations[0].RelationID, relations[0].RelationType), (TypedInfoItem) objInfoItem2);
                    break;
                  }
                  break;
                }
                continue;
              case ImbaseObjCreateMode.iocmUseExists:
                objInfoItem2 = new ObjInfoItem(gtp2EtpRefObjData6.SostavItem.PartID, gtp2EtpRefObjData6.SostavItem.ObjectTypeID);
                gtp2EtpRefObjData6.ObjRefIDs.Add((TypedInfoItem) new RelInfoItem(0L), (TypedInfoItem) objInfoItem2);
                break;
            }
            TypedInfoItem typedInfoItem1 = new List<TypedInfoItem>((IEnumerable<TypedInfoItem>) gtp2EtpRefObjData1.ObjRefIDs.Values)[0];
            if (imbaseObjCreateInfo.CreateMode != ImbaseObjCreateMode.iocmUseExists || typedInfoItem1.ItemID != gtp2EtpRefObjData6.SostavItem.ProjID)
            {
              session1?.PartObjToSuppress.AddItem(objInfoItem2.ObjectID);
              IDBRelation relation1 = session.GetRelation(gtp2EtpRefObjData6.ItemInfo.ItemID, true);
              if (MetaDataHelper.GetApplicability(typedInfoItem1.ItemTypeID, objInfoItem2.ObjTypeID, relation1.RelationType).IsContent && typedInfoItem1.ItemID > 0L)
              {
                IDBObject dbObject3 = session.GetObject(typedInfoItem1.ItemID);
                switch (dbObject3.ObjectModifyMode)
                {
                  case ObjectModifyModes.Checkout:
                  case ObjectModifyModes.CreateVersion:
                  case ObjectModifyModes.CantModify:
                    typedInfoItem1 = (TypedInfoItem) new ObjInfoItem(dbObject3.CheckOut());
                    break;
                }
              }
              IDBRelation relation2 = customService2.CreateRelation(relation1.RelationType, typedInfoItem1.ItemID, objInfoItem2.ItemID, gtp2EtpRefObjData6.ItemInfo.ItemID, session.SessionGUID);
              if (imbaseObjCreateInfo.CreateMode == ImbaseObjCreateMode.iocmCreateNew)
              {
                IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(relation1.RelationType, TechCardConsts.AttributeTypes.ContextVersionID);
                if (attribute4RelationType != null && !attribute4RelationType.Options.HasFlag((Enum) AttributeOptions.DontCopyPrototypeValue))
                  relation2.SetAttributesValues(new AttributeValues[1]
                  {
                    new AttributeValues(TechCardConsts.AttributeTypes.ContextVersionID)
                  });
              }
              session1?.RelationsToSuppress.AddItem(relation2.RelationID);
              AttributeValues[] valuesList = new AttributeValues[1]
              {
                new AttributeValues(TechCardConsts.AttributeTypes.TechProcGroupRelAttrID, (object) (relation1 as IDBGuid).GUID)
              };
              relation2.SetAttributesValues(valuesList);
              if (dbObject1 != null && dbObject1.IsCreationMode)
              {
                dbObject1.CommitCreation(true);
                if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject1.ObjectModifyMode == ObjectModifyModes.CreateVersion)
                  dbObject1.CheckOut();
                foreach (TypedInfoItem typedInfoItem2 in gtp2EtpRefObjData6.ObjRefIDs.Values)
                {
                  ObjInfoItem objInfoItem3 = typedInfoItem2 as ObjInfoItem;
                  if (!ObjInfoItem.IsEmpty((ITypedInfoItem) objInfoItem3) && Math.Abs(objInfoItem3.ObjectID) == Math.Abs(dbObject1.ObjectID))
                    objInfoItem3.ObjectID = dbObject1.ObjectID;
                }
              }
            }
          }
        }
      }
      finally
      {
        customService1?.DisposeSession(session.SessionGUID);
      }
    }
    if (recursive)
    {
      List<Gtp2EtpRefObjData> gtpItemList2 = new List<Gtp2EtpRefObjData>(gtp2etpObjList.Count);
      List<long> list = gtpItemList.Select<Gtp2EtpRefObjData, long>((System.Func<Gtp2EtpRefObjData, long>) (item => item.SostavItem.PartID)).ToList<long>();
      foreach (Gtp2EtpRefObjData gtp2etpObj in gtp2etpObjList)
      {
        if (gtp2etpObj != null && gtp2etpObj.SostavItem != null && list.Contains(gtp2etpObj.SostavItem.ProjID))
          gtpItemList2.Add(gtp2etpObj);
      }
      if (gtpItemList2.Count != 0)
        TechProcGroupUtils.CreateEtpObject(gtpItemList2, gtp2etpObjList, session, recursive);
    }
    return true;
  }

  /// <summary>
  /// Переименовать ЕТП по ГТП согласно изделию к которому относиться
  /// </summary>
  /// <param name="etpDbObj">Объект ЕПТ</param>
  /// <param name="gtpDbObj">Объект ГТП</param>
  /// <param name="artObjInfo">Ид. версии изделия</param>
  /// <param name="procRouteInfo">Ид. версии МО</param>
  /// <param name="session"></param>
  /// <returns></returns>
  internal static bool RenameEtpProcess(
    IDBObject etpDbObj,
    IDBObject gtpDbObj,
    ObjInfoItem artObjInfo,
    ObjInfoItem procRouteInfo,
    IUserSession session)
  {
    if (etpDbObj == null || gtpDbObj == null || (TypedInfoItem) artObjInfo == (TypedInfoItem) null || artObjInfo.ObjectID == 0L)
      return false;
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(etpDbObj.ObjectType, TechCardConsts.AttributeTypes.DesignationAttrTypeID);
    if (attribute4ObjectType == null || attribute4ObjectType.Computed != ComputeValueModes.NotComputableValue || session.GetObject(artObjInfo.ObjectID, false) == null)
      return false;
    AttributeValues[] valuesList1 = new AttributeValues[2]
    {
      new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), (object) null),
      new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), (object) null)
    };
    etpDbObj.SetAttributesValues(valuesList1);
    IDBAttribute attributeByGuid = gtpDbObj.GetAttributeByGuid(TechCardConsts.AttributeTypes.ProductionAttrGUID);
    long asInteger = attributeByGuid != null ? attributeByGuid.AsInteger : 0L;
    ITechCardClassifyObjectService service1 = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(etpDbObj);
    ITechCardClassifyObjectService classifyObjectService = service1;
    IUserSession session1 = session;
    TechCardClassifyObjectAttributeParams objectAttributeParams = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, artObjInfo);
    objectAttributeParams.AttributeValues = (IEnumerable<AttributeValues>) new AttributeValues[1]
    {
      new AttributeValues(TechCardConsts.AttributeTypes.ProductionAttrID, (object) asInteger)
    };
    TechCardClassifyObjectAttributeParams classifyParams = objectAttributeParams;
    TechCardClassifyTechProcessDesignationStrategy classifyStrategy = new TechCardClassifyTechProcessDesignationStrategy();
    string initValue;
    ref string local = ref initValue;
    string attributeValue;
    if (!(classifyObjectService.ClassifyObjectAttribute(session1, classifyParams, (ITechCardClassifyObjectStrategy) classifyStrategy, out local) | service1.ClassifyObjectAttribute(session, new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, artObjInfo), (ITechCardClassifyObjectStrategy) new TechCardClassifyObjectNameStrategy(), out attributeValue)))
      return false;
    bool flag = true;
    IExpertUser service2 = ServiceUtils.GetService<IExpertUser>((object) ApplicationServices.Container, false);
    if (service2 != null)
    {
      using (IExpertTask expertTask = service2.GetExpertTask())
      {
        using (new RemoteLock((object) etpDbObj))
        {
          expertTask.SetParmValue(etpDbObj.ObjectID, TechCardConsts.AttributeTypes.NameAttrTypeID, (object) attributeValue);
          expertTask.SetParmValue(etpDbObj.ObjectID, TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) initValue);
          object obj;
          if (expertTask.Calculate(etpDbObj.ObjectType, MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.DesignationEtpObj4Gtp), etpDbObj.ObjectID, new long[3]
          {
            artObjInfo.ObjectID,
            gtpDbObj.ObjectID,
            procRouteInfo.ObjectID
          }, out obj) == ExpertResult.OK)
          {
            initValue = Convert.ToString(obj);
            flag = false;
          }
        }
      }
    }
    if (flag)
      initValue = $"{initValue} [{gtpDbObj.Caption}]";
    AttributeValues[] valuesList2 = new AttributeValues[2]
    {
      new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) attributeValue),
      new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) initValue)
    };
    etpDbObj.SetAttributesValues(valuesList2);
    return true;
  }
}
