// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Process_Route.ProcRouteThroughObject
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.TechCard.Client.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Process_Route;

/// <summary>Реализация сквозного маршрута обработки</summary>
internal class ProcRouteThroughObject
{
  /// <summary>Описание объекта МО</summary>
  private readonly ObjInfoItem _objectInfo;
  /// <summary>Описание связей типа "Сквозной МО" с операциями</summary>
  private List<RelObjInfoItem> _rel2OperInfoList;
  /// <summary>Список все операций ТП</summary>
  private List<ObjInfoItem> _allOperInfoList;

  /// <summary>Загрузка сквозных связей МО с операциями</summary>
  /// <param name="session"></param>
  private void LoadLinkedOperInfoList(IUserSession session)
  {
    if (this._rel2OperInfoList != null)
      return;
    this._rel2OperInfoList = new List<RelObjInfoItem>();
    DataTable childSostavData = DataHelper.GetChildSostavData(this.ObjectInfo, session, (IEnumerable<int>) new int[1]
    {
      MetaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechThroughMORelationGuid)
    }, false);
    if (childSostavData == null || childSostavData.Rows.Count == 0)
      return;
    List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
    new RelObjInfoDbScheme<ObjInfoItem>(true).ParseInfoItems(session, (IEnumerable<DataRow>) childSostavData.AsEnumerable(), (ICollection<RelObjInfoItem>) relObjInfoItemList);
    this._rel2OperInfoList.AddRange(relObjInfoItemList.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => MetaDataHelper.IsObjectTypeChildOf(item.PartInfo.ItemTypeID, TechCardConsts.ObjectTypes.OperaciyaID))));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="cehRouteList"></param>
  /// <param name="operInfoList"></param>
  /// <returns></returns>
  private bool GetRouteOperObjList(
    IUserSession session,
    List<ObjInfoItem> cehRouteList,
    out List<ObjInfoItem> operInfoList)
  {
    operInfoList = (List<ObjInfoItem>) null;
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (cehRouteList == null)
      throw new ArgumentNullException(nameof (cehRouteList));
    if (cehRouteList.Count == 0)
      return false;
    DataTable childSostavData1 = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) cehRouteList, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, true, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -26, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    }.ToArray());
    List<ObjInfoItem> projObjList1 = new List<ObjInfoItem>();
    List<string> stringList = new List<string>();
    if (childSostavData1 != null)
    {
      int columnIndex1 = childSostavData1.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex2 = childSostavData1.Columns.IndexOf("F_OBJECT_TYPE");
      int columnIndex3 = childSostavData1.Columns.IndexOf("F_PRJ_GUID");
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData1.Rows)
      {
        int int32 = Convert.ToInt32(row[columnIndex2]);
        if (MetaDataHelper.IsObjectTypeChildOf(int32, TechCardConsts.ObjectTypes.ElemRouteID))
        {
          projObjList1.Add(new ObjInfoItem(Convert.ToInt64(row[columnIndex1]), int32));
          if (row[columnIndex3] != DBNull.Value)
            stringList.Add(row[columnIndex3].ToString());
        }
      }
    }
    if (projObjList1.Count == 0)
      return false;
    DataTable childSostavData2 = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList1, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRouteRelationID
    }, false, (IEnumerable<ConditionStructure>) new List<ConditionStructure>()
    {
      new ConditionStructure(TechCardConsts.AttributeTypes.ElemRouteLinkAttrID, RelationalOperators.In, (object) stringList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation)
    }.ToArray(), (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ElemRouteLinkAttrID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    }.ToArray());
    if (childSostavData2 == null)
      return false;
    List<ObjInfoItem> projObjList2 = new List<ObjInfoItem>();
    string str1 = TechCardConsts.AttributeTypes.ElemRouteLinkAttrGuid.ToString();
    int idxFldPartObjId = childSostavData2.Columns.IndexOf("F_OBJECT_ID");
    int idxFldPartObjType = childSostavData2.Columns.IndexOf("F_OBJECT_TYPE");
    foreach (string str2 in stringList)
    {
      DataRow[] source = childSostavData2.Select($"[{str1}] = '{str2}'");
      projObjList2.AddRange((IEnumerable<ObjInfoItem>) ((IEnumerable<DataRow>) source).Select<DataRow, ObjInfoItem>((System.Func<DataRow, ObjInfoItem>) (item => new ObjInfoItem(Convert.ToInt64(item[idxFldPartObjId]), Convert.ToInt32(item[idxFldPartObjType])))).ToArray<ObjInfoItem>());
    }
    DataTable childSostavData3 = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList2, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, true, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) null);
    if (childSostavData3 == null)
      return false;
    operInfoList = new List<ObjInfoItem>();
    idxFldPartObjId = childSostavData3.Columns.IndexOf("F_OBJECT_ID");
    idxFldPartObjType = childSostavData3.Columns.IndexOf("F_OBJECT_TYPE");
    foreach (ObjInfoItem objInfoItem in projObjList2)
    {
      DataRow[] source = childSostavData3.Select($"[{"F_PROJ_ID"}] = {objInfoItem.ObjectID}");
      operInfoList.AddRange((IEnumerable<ObjInfoItem>) ((IEnumerable<DataRow>) source).Where<DataRow>((System.Func<DataRow, bool>) (item => MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(item[idxFldPartObjType]), TechCardConsts.ObjectTypes.OperaciyaID))).Select<DataRow, ObjInfoItem>((System.Func<DataRow, ObjInfoItem>) (item => new ObjInfoItem(Convert.ToInt64(item[idxFldPartObjId]), Convert.ToInt32(item[idxFldPartObjType])))).ToArray<ObjInfoItem>());
    }
    return operInfoList.Count > 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="cehRouteList"></param>
  /// <param name="operInfoList"></param>
  /// <returns></returns>
  private bool GetTechProcOperObjList(
    IUserSession session,
    List<ObjInfoItem> cehRouteList,
    out List<ObjInfoItem> operInfoList)
  {
    operInfoList = (List<ObjInfoItem>) null;
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (cehRouteList == null)
      throw new ArgumentNullException(nameof (cehRouteList));
    if (cehRouteList.Count == 0)
      return false;
    DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) cehRouteList, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, -1, new DBRecordSetParams((ConditionStructure[]) null), (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) new List<int>()
    {
      TechCardConsts.ObjectTypes.OperaciyaID
    });
    if (childSostavData == null)
      return false;
    operInfoList = new List<ObjInfoItem>();
    new ObjInfoDbScheme().ParseItems(childSostavData != null ? (IEnumerable<DataRow>) childSostavData.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) operInfoList);
    return operInfoList.Count > 0;
  }

  /// <summary>Конструктор</summary>
  /// <param name="procRouteInfo"></param>
  public ProcRouteThroughObject(ObjInfoItem procRouteInfo)
  {
    if ((TypedInfoItem) procRouteInfo == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (procRouteInfo));
    this._objectInfo = !ObjInfoItem.IsEmpty((ITypedInfoItem) procRouteInfo) ? procRouteInfo : throw new ArgumentException("ObjectID must be defined", nameof (procRouteInfo));
  }

  /// <summary>
  /// Получение списка объектов (операций) доступных для привязки  к сквозному маршруту обработки
  /// </summary>
  /// <param name="session"></param>
  /// <param name="operInfoList"></param>
  /// <returns>Описание объектов (вместе со связями) объектов доступных для привязки к текущему сквозному</returns>
  public bool GetUnlinkedOperObjList(IUserSession session, out List<ObjInfoItem> operInfoList)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    operInfoList = new List<ObjInfoItem>();
    DataTable childSostavData = DataHelper.GetChildSostavData(this.ObjectInfo, session, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, false);
    if (childSostavData == null || childSostavData.Rows.Count == 0)
      return false;
    List<ObjInfoItem> objects = new List<ObjInfoItem>(childSostavData.Rows.Count);
    new ObjInfoDbScheme().ParseItems(childSostavData != null ? (IEnumerable<DataRow>) childSostavData.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) objects);
    List<ObjInfoItem> cehRouteList1 = new List<ObjInfoItem>();
    List<ObjInfoItem> cehRouteList2 = new List<ObjInfoItem>();
    foreach (ObjInfoItem objInfoItem in objects)
    {
      if (!((TypedInfoItem) objInfoItem == (TypedInfoItem) null))
      {
        if (MetaDataHelper.IsObjectTypeChildOf(objInfoItem.ObjTypeID, TechCardConsts.ObjectTypes.CehRouteID))
          cehRouteList1.Add(objInfoItem);
        else if (MetaDataHelper.IsObjectTypeChildOf(objInfoItem.ObjTypeID, TechCardConsts.ObjectTypes.TechProcBaseID))
          cehRouteList2.Add(objInfoItem);
      }
    }
    if (cehRouteList1.Count != 0)
    {
      if (!this.GetRouteOperObjList(session, cehRouteList1, out this._allOperInfoList))
        return false;
    }
    else if (!this.GetTechProcOperObjList(session, cehRouteList2, out this._allOperInfoList))
      return false;
    if (this._allOperInfoList == null || this._allOperInfoList.Count == 0)
      return false;
    this.LoadLinkedOperInfoList(session);
    List<ObjInfoItem> linkedOperInfoList = this._rel2OperInfoList.Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.PartInfo)).ToList<ObjInfoItem>();
    linkedOperInfoList.Sort();
    operInfoList = this._allOperInfoList.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => linkedOperInfoList.BinarySearch(item) < 0)).ToList<ObjInfoItem>();
    return operInfoList.Count > 0;
  }

  /// <summary>Добавление операций в сквозной ТП</summary>
  /// <param name="session"></param>
  /// <param name="operInfoList"></param>
  /// <returns></returns>
  public List<RelInfoItem> LinkOper2ThroughObject(
    IUserSession session,
    List<ObjInfoItem> operInfoList)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (operInfoList == null)
      throw new ArgumentNullException(nameof (operInfoList));
    List<RelInfoItem> relInfoItemList = new List<RelInfoItem>();
    if (operInfoList.Count == 0)
      return relInfoItemList;
    if (this._rel2OperInfoList.Count == 0)
    {
      TechcardClientUtils.StartCreateRelations(this._objectInfo.ObjectID, session);
      try
      {
        int relTypeId = MetaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.TechThroughMORelationGuid);
        List<IDBRelation> relations = TechcardClientUtils.CreateRelations(session, this._objectInfo.ObjectID, operInfoList.Select<ObjInfoItem, int>((System.Func<ObjInfoItem, int>) (item => relTypeId)).ToArray<int>(), operInfoList.Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (item => item.ObjectID)).ToArray<long>(), DateTime.Now, TechCreateRelMode.tcrmBothContainsFirst);
        if (relations == null)
          return relInfoItemList;
        relInfoItemList.AddRange((IEnumerable<RelInfoItem>) relations.Select<IDBRelation, RelInfoItem>((System.Func<IDBRelation, RelInfoItem>) (item => new RelInfoItem(item))).ToArray<RelInfoItem>());
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(session);
      }
      return relInfoItemList;
    }
    ICompositionsAutomaticSortingSession automaticSortingSession = (ICompositionsAutomaticSortingSession) null;
    ICompositionsAutomaticSortingService service = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) session, true);
    if (service != null)
      automaticSortingSession = service.CreateSession((object) session.SessionGUID);
    try
    {
      automaticSortingSession?.PrefetchObjectComposition((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
      {
        this._objectInfo
      }, (object) session.SessionGUID);
      IDBRelationCollection relationCollection = session.GetRelationCollection(TechCardConsts.RelTypes.TechThroughMORelationID);
      Dictionary<ObjInfoItem, long> dictionary = this._rel2OperInfoList.ToDictionary<RelObjInfoItem, ObjInfoItem, long>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.PartInfo), (System.Func<RelObjInfoItem, long>) (item => item.RelationID));
      foreach (ObjInfoItem operInfo in operInfoList)
      {
        IDBRelation dbRel = relationCollection.Create(this._objectInfo.ObjectID, operInfo.ObjectID);
        if (dbRel != null)
        {
          relInfoItemList.Add(new RelInfoItem(dbRel));
          if (automaticSortingSession != null)
          {
            CompositionSortingProjInfo compositionSortingProjInfo = new CompositionSortingProjInfo(dbRel.RelationID, dbRel.RelationType, this._objectInfo.ObjectID, this._objectInfo.ObjTypeID, operInfo.ObjTypeID);
            dictionary[operInfo] = dbRel.RelationID;
            long targetRelationId = 0;
            int num = this._allOperInfoList.IndexOf(operInfo);
            if (num != -1)
            {
              int index = num + 1;
              while (index < this._allOperInfoList.Count && !dictionary.TryGetValue(this._allOperInfoList[index], out targetRelationId))
                ++index;
            }
            if (targetRelationId != 0L)
              automaticSortingSession.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) new CompositionSortingProjInfo[1]
              {
                compositionSortingProjInfo
              }, CompositionTargetMode.InsertBefore, targetRelationId, (object) session.SessionGUID);
            else
              automaticSortingSession.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) new CompositionSortingProjInfo[1]
              {
                compositionSortingProjInfo
              }, (object) session.SessionGUID);
          }
        }
      }
    }
    finally
    {
      service?.DisposeSession((object) session.SessionGUID);
    }
    return relInfoItemList;
  }

  /// <summary>Описание объекта МО</summary>
  public ObjInfoItem ObjectInfo
  {
    [DebuggerStepThrough] get => this._objectInfo;
  }
}
