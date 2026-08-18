// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CreateByAnalog.CreateByAnalogCommandTask
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.MRP2;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands.CreateByAnalog;

internal class CreateByAnalogCommandTask
{
  /// <summary>Объект-описание ПВ</summary>
  private readonly ObjInfoIDItem _productionReportInfoItem;
  /// <summary>Тип создаваемого объекта</summary>
  private readonly int _createObjectTypeId;
  /// <summary>
  /// Таблица с данными ПВ для которого создаются объекты по прототипу
  /// </summary>
  private readonly DataTable _productionReportData;
  /// <summary>Таблица с данными ПВ-прототипа</summary>
  private readonly DataTable _prototypeProductionReportData;
  /// <summary>
  /// 
  /// </summary>
  private IDictionary<long, ObjInfoIDItem> _productObject2LinkItemCache;
  /// <summary>
  /// 
  /// </summary>
  private IDictionary<long, ObjInfoIDItem> _prototypeProductObject2LinkItemCache;
  /// <summary>
  /// 
  /// </summary>
  private readonly IDictionary<long, string> _productObject2UidCache = (IDictionary<long, string>) new Dictionary<long, string>();
  /// <summary>Кэш : F_PROJ_ID =&gt; строки записей ПВ</summary>
  private IDictionary<long, IEnumerable<DataRow>> _productProjObject2DataRows;

  /// <summary>
  /// Получение списка DataRow для указанных родительских объектов
  /// </summary>
  /// <param name="projObjectIds"></param>
  /// <returns></returns>
  private IEnumerable<DataRow> GetProductionDataRowsByProjId(IEnumerable<long> projObjectIds)
  {
    foreach (long projObjectId in projObjectIds)
    {
      IEnumerable<DataRow> dataRows;
      if (this._productProjObject2DataRows.TryGetValue(projObjectId, out dataRows))
      {
        foreach (DataRow dataRow in dataRows)
          yield return dataRow;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dataTable"></param>
  /// <returns></returns>
  private IDictionary<long, ObjInfoIDItem> GetObject2LinkItemCache(
    IUserSession session,
    DataTable dataTable)
  {
    IDictionary<long, ObjInfoIDItem> object2LinkItemCache = (IDictionary<long, ObjInfoIDItem>) new Dictionary<long, ObjInfoIDItem>();
    dataTable.AsEnumerable().InvokeForAll((Action<DataRow>) (row =>
    {
      long int64Value = DataSetProcessor.GetInt64Value(row, "cadd9a8c-306c-11d8-b4e9-00304f19f545", 0L);
      if (int64Value == 0L)
        return;
      object2LinkItemCache[DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L)] = new ObjInfoIDItem(int64Value);
    }));
    ObjInfoHelper.UpdateUnknownInfo((IEnumerable<ObjInfoItem>) object2LinkItemCache.Values, session);
    return object2LinkItemCache;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void PrepareData(IUserSession session)
  {
    ObjInfoHelper.UpdateUnknownInfo((IEnumerable<ObjInfoItem>) new ObjInfoIDItem[1]
    {
      this._productionReportInfoItem
    }, session);
    this._prototypeProductObject2LinkItemCache = this.GetObject2LinkItemCache(session, this._prototypeProductionReportData);
    this._productObject2LinkItemCache = this.GetObject2LinkItemCache(session, this._productionReportData);
    foreach (DataRow row in (InternalDataCollectionBase) this._productionReportData.Rows)
    {
      long int64Value = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L);
      string stringValue = DataSetProcessor.GetStringValue(row, TechCardConsts.AttributeTypes.ProductionObjectUIDAttrGuid.ToString(), string.Empty);
      if (!string.IsNullOrEmpty(stringValue))
        this._productObject2UidCache[int64Value] = stringValue;
    }
    this._productProjObject2DataRows = (IDictionary<long, IEnumerable<DataRow>>) this._productionReportData.AsEnumerable().GroupBy<DataRow, long>((System.Func<DataRow, long>) (item => DataSetProcessor.GetInt64Value(item, "F_PROJ_ID", 0L))).ToDictionary<IGrouping<long, DataRow>, long, IEnumerable<DataRow>>((System.Func<IGrouping<long, DataRow>, long>) (group => group.Key), (System.Func<IGrouping<long, DataRow>, IEnumerable<DataRow>>) (group => group.AsEnumerable<DataRow>()));
  }

  private void DoExecute(
    IUserSession session,
    CreateByAnalogObjectOptions options,
    ISelectedItems protoTypeSelectedItems)
  {
    for (int index = 0; index < protoTypeSelectedItems.Count; ++index)
    {
      NavigatorTreeNode itemData1 = protoTypeSelectedItems.GetItemData<NavigatorTreeNode>(index, false);
      if (itemData1 != null)
      {
        IDBObjectID itemData2 = protoTypeSelectedItems.GetItemData<IDBObjectID>(index, false);
        if (itemData2 != null)
        {
          NavigatorTreeNode parent1 = itemData1.Parent;
          NavigatorTreeNode parent2 = parent1?.Parent;
          NavigatorTreeNode parent3 = parent2?.Parent;
          IDBTypedObjectID dbTypedObjId1;
          IDBTypedObjectID dbTypedObjId2;
          IDBTypedObjectID dbTypedObjId3;
          if (TechcardClientControlsUtils.GetObjectInfo(parent1, out dbTypedObjId1) && TechcardClientControlsUtils.GetObjectInfo(parent2, out dbTypedObjId2) && TechcardClientControlsUtils.GetObjectInfo(parent3, out dbTypedObjId3))
          {
            IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) null;
            IDBTypedObjectID dbTypedObjId4 = dbTypedObjId2;
            NavigatorTreeNode treeNode = parent2;
            while (dbTypedObjId4 != null)
            {
              if (MetaDataHelper.IsObjectTypeChildOf(dbTypedObjId4.ObjectType, MRP2Consts.objtypeIdExitAssembly))
              {
                dbTypedObjectId = dbTypedObjId4;
                break;
              }
              treeNode = treeNode?.Parent;
              if (!TechcardClientControlsUtils.GetObjectInfo(treeNode, out dbTypedObjId4))
                break;
            }
            if (dbTypedObjectId != null)
            {
              bool flag = dbTypedObjId2.ObjectID == dbTypedObjectId.ObjectID;
              ObjInfoIDItem productAnalogLinkItem;
              ObjInfoIDItem exitAssemblyAnalogLinkItem;
              if (this._prototypeProductObject2LinkItemCache.TryGetValue(dbTypedObjId2.ObjectID, out productAnalogLinkItem) && this._prototypeProductObject2LinkItemCache.TryGetValue(dbTypedObjectId.ObjectID, out exitAssemblyAnalogLinkItem))
              {
                IEnumerable<long> productObjectIds = (IEnumerable<long>) this._productObject2LinkItemCache.Where<KeyValuePair<long, ObjInfoIDItem>>((System.Func<KeyValuePair<long, ObjInfoIDItem>, bool>) (item => item.Value.ID == productAnalogLinkItem.ID)).Select<KeyValuePair<long, ObjInfoIDItem>, long>((System.Func<KeyValuePair<long, ObjInfoIDItem>, long>) (item => item.Key)).ToHashSet<long>();
                if (productObjectIds.Any<long>())
                {
                  IEnumerable<long> longs;
                  if (flag)
                  {
                    longs = (IEnumerable<long>) new long[1]
                    {
                      this._productionReportInfoItem.ObjectID
                    };
                  }
                  else
                  {
                    ObjInfoIDItem parenProductAnalogLinkItem;
                    if (this._prototypeProductObject2LinkItemCache.TryGetValue(dbTypedObjId3.ObjectID, out parenProductAnalogLinkItem))
                    {
                      longs = (IEnumerable<long>) this._productObject2LinkItemCache.Where<KeyValuePair<long, ObjInfoIDItem>>((System.Func<KeyValuePair<long, ObjInfoIDItem>, bool>) (item => item.Value.ID == parenProductAnalogLinkItem.ID)).Select<KeyValuePair<long, ObjInfoIDItem>, long>((System.Func<KeyValuePair<long, ObjInfoIDItem>, long>) (item => item.Key)).ToHashSet<long>();
                      if (!longs.Any<long>())
                        continue;
                    }
                    else
                      continue;
                  }
                  IEnumerable<long> source = this._productObject2LinkItemCache.Where<KeyValuePair<long, ObjInfoIDItem>>((System.Func<KeyValuePair<long, ObjInfoIDItem>, bool>) (item => item.Value.ID == exitAssemblyAnalogLinkItem.ID)).Select<KeyValuePair<long, ObjInfoIDItem>, long>((System.Func<KeyValuePair<long, ObjInfoIDItem>, long>) (item => item.Key));
                  if (source.Any<long>())
                  {
                    HashSet<string> exitAssemblyUIds = new HashSet<string>();
                    foreach (long key in source)
                    {
                      string str;
                      if (this._productObject2UidCache.TryGetValue(key, out str))
                        exitAssemblyUIds.Add(str);
                    }
                    foreach (DataRow row1 in this.GetProductionDataRowsByProjId(longs).Where<DataRow>((System.Func<DataRow, bool>) (row => productObjectIds.Contains<long>(DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L)))))
                    {
                      long int64Value1 = DataSetProcessor.GetInt64Value(row1, "F_OBJECT_ID", 0L);
                      int int32Value = DataSetProcessor.GetInt32Value(row1, "F_OBJECT_TYPE", -1);
                      long int64Value2 = DataSetProcessor.GetInt64Value(row1, "F_PROJ_ID", 0L);
                      HashSet<long> hashSet1 = this.GetProductionDataRowsByProjId((IEnumerable<long>) new long[1]
                      {
                        int64Value1
                      }).Where<DataRow>((System.Func<DataRow, bool>) (row => DataSetProcessor.GetInt32Value(row, "F_OBJECT_TYPE", -1) == TechCardConsts.ObjectTypes.ProcRoutingID)).Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L))).ToHashSet<long>();
                      if (!options.IgnoreExistingProcessRoutes || !hashSet1.Contains(dbTypedObjId1.ObjectID))
                      {
                        HashSet<long> hashSet2 = this.GetProductionDataRowsByProjId((IEnumerable<long>) hashSet1).Where<DataRow>((System.Func<DataRow, bool>) (row => DataSetProcessor.GetInt32Value(row, "F_OBJECT_TYPE", -1) == this._createObjectTypeId)).Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, "F_PROJ_ID", 0L))).ToHashSet<long>();
                        long objectID = 0;
                        if (hashSet1.Count > hashSet2.Count)
                        {
                          foreach (DataRow row2 in this.GetProductionDataRowsByProjId((IEnumerable<long>) hashSet1))
                          {
                            long int64Value3 = DataSetProcessor.GetInt64Value(row2, "F_PROJ_ID", 0L);
                            DataRow row3 = row2;
                            Guid guid = TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrGUID;
                            string columnName1 = guid.ToString();
                            long int64Value4 = DataSetProcessor.GetInt64Value(row3, columnName1, 0L);
                            if (int64Value4 != 0L)
                            {
                              if (int64Value4 != this._productionReportInfoItem.ID)
                                continue;
                            }
                            else
                            {
                              DataRow row4 = row2;
                              guid = TechCardConsts.AttributeTypes.MemberOfProductionReportVersionAttrGUID;
                              string columnName2 = guid.ToString();
                              if (DataSetProcessor.GetInt64Value(row4, columnName2, 0L) != this._productionReportInfoItem.ObjectID)
                                continue;
                            }
                            DataRow row5 = row2;
                            guid = TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrGUID;
                            string columnName3 = guid.ToString();
                            string empty = string.Empty;
                            string stringValue = DataSetProcessor.GetStringValue(row5, columnName3, empty);
                            if (exitAssemblyUIds.Contains(stringValue) && !hashSet2.Contains(int64Value3))
                            {
                              objectID = int64Value3;
                              break;
                            }
                          }
                        }
                        IDBObject processRouteDbObject = objectID != 0L ? session.GetObject(objectID, true) : this.CreateProcessRouteObject(session, new ObjInfoItem(int64Value1, int32Value), !flag ? new ObjInfoItem(int64Value2) : (ObjInfoItem) null, (IEnumerable<string>) exitAssemblyUIds);
                        this.CreateObjectByPrototype(session, processRouteDbObject, itemData2.Value);
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="productObjectId"></param>
  /// <param name="productParentObjectId"></param>
  /// <param name="exitAssemblyUIds"></param>
  /// <returns></returns>
  private IDBObject CreateProcessRouteObject(
    IUserSession session,
    ObjInfoItem productObjectItem,
    ObjInfoItem productParentObjectItem,
    IEnumerable<string> exitAssemblyUIds)
  {
    IDBObject processRouteObject = session.GetObjectCollection(TechCardConsts.ObjectTypes.ProcRoutingID).Create();
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(processRouteObject.ObjectID, processRouteObject.ObjectType);
    ObjInfoItem contextObjectItem = productObjectItem;
    ObjInfoIDItem[] objInfoIdItemArray = new ObjInfoIDItem[1]
    {
      this._productionReportInfoItem
    };
    ITechCardClassifyObjectService classifyObjectService1 = service;
    IUserSession session1 = session;
    TechCardClassifyObjectAttributeParams classifyParams1 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem);
    classifyParams1.ExtraContextObjInfoItems = (IEnumerable<ObjInfoItem>) objInfoIdItemArray;
    TechCardClassifyObjectDesignationStrategy classifyStrategy1 = new TechCardClassifyObjectDesignationStrategy();
    string initValue1;
    ref string local1 = ref initValue1;
    int num1 = classifyObjectService1.ClassifyObjectAttribute(session1, classifyParams1, (ITechCardClassifyObjectStrategy) classifyStrategy1, out local1) ? 1 : 0;
    ITechCardClassifyObjectService classifyObjectService2 = service;
    IUserSession session2 = session;
    TechCardClassifyObjectAttributeParams classifyParams2 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, contextObjectItem);
    classifyParams2.ExtraContextObjInfoItems = (IEnumerable<ObjInfoItem>) objInfoIdItemArray;
    TechCardClassifyObjectNameStrategy classifyStrategy2 = new TechCardClassifyObjectNameStrategy();
    string initValue2;
    ref string local2 = ref initValue2;
    int num2 = classifyObjectService2.ClassifyObjectAttribute(session2, classifyParams2, (ITechCardClassifyObjectStrategy) classifyStrategy2, out local2) ? 1 : 0;
    if ((num1 | num2) != 0)
      processRouteObject.SetAttributesValues(new List<AttributeValues>(2)
      {
        new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) initValue2),
        new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) initValue1)
      }.ToArray());
    TechcardClientUtils.StartCreateRelations(processRouteObject.ObjectID, session);
    IDBRelationCollection relationCollection = session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
    foreach (string exitAssemblyUid in exitAssemblyUIds)
    {
      IDBObject partDbObject = session.GetObjectCollection(TechCardConsts.ObjectTypes.ProcRoutingEntryID).Create();
      IList<AttributeValues> source = (IList<AttributeValues>) new List<AttributeValues>((IEnumerable<AttributeValues>) new AttributeValues[2]
      {
        new AttributeValues(TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrID, (object) this._productionReportInfoItem.ID),
        new AttributeValues(TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID, (object) exitAssemblyUid)
      });
      if (!ObjInfoItem.IsEmpty((ITypedInfoItem) productParentObjectItem))
        source.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID, (object) this._productObject2UidCache[productParentObjectItem.ObjectID]));
      partDbObject.SetAttributesValues(source.ToArray<AttributeValues>());
      TechcardClientUtils.CreateRelation(relationCollection, processRouteObject, partDbObject);
      partDbObject.CommitCreation(true);
    }
    TechcardClientUtils.CreateRelation(relationCollection, productObjectItem, (ObjInfoItem) new ObjInfoIDItem(processRouteObject), NewRelationProperties.Empty);
    processRouteObject.CommitCreation(true, true);
    return processRouteObject;
  }

  /// <summary>Создание объекта по аналогу / прототипу</summary>
  /// <param name="session"></param>
  /// <param name="processRouteDbObject"></param>
  /// <param name="selectedDbObjectId"></param>
  private IDBObject CreateObjectByPrototype(
    IUserSession session,
    IDBObject processRouteDbObject,
    long prototypeObjectId)
  {
    IDBObject partDbObject = session.GetObjectCollection(this._createObjectTypeId).Create(prototypeObjectId);
    IMSApplicability applicability = MetaDataHelper.GetApplicability(TechCardConsts.ObjectTypes.ProcRoutingID, this._createObjectTypeId, TechCardConsts.RelTypes.TechRelationID);
    if (applicability != null && applicability.IsContent && processRouteDbObject.ObjectModifyMode == ObjectModifyModes.Checkout && processRouteDbObject.CheckoutBy == 0L)
      processRouteDbObject = processRouteDbObject.CheckOut(true);
    TechcardClientUtils.CreateRelation(session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID), processRouteDbObject, partDbObject);
    partDbObject.CommitCreation(true);
    if (TechCardParamsHelper.TechParams.ProcessRoute.AutoCheckIn && !Intermech.Consts.IsUndefinedObjectId(processRouteDbObject.CheckoutBy))
      processRouteDbObject.CheckIn();
    return partDbObject;
  }

  /// <summary>Конструктор</summary>
  /// <param name="productionReportInfoItem">Объект-описание ПВ</param>
  /// <param name="createObjectTypeId">Тип создаваемого объекта</param>
  /// <param name="productionReportData">Таблица с данными ПВ для которого создаются объекты по прототипу</param>
  /// <param name="prototypeProductionReportData">Таблица с данными ПВ-прототипа</param>
  public CreateByAnalogCommandTask(
    [NotNull] ObjInfoIDItem productionReportInfoItem,
    int createObjectTypeId,
    [NotNull] DataTable productionReportData,
    [NotNull] DataTable prototypeProductionReportData)
  {
    this._productionReportInfoItem = productionReportInfoItem;
    this._createObjectTypeId = createObjectTypeId;
    this._productionReportData = productionReportData;
    this._prototypeProductionReportData = prototypeProductionReportData;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="options"></param>
  /// <param name="protoTypeSelectedItems"></param>
  public void Execute(
    IUserSession session,
    CreateByAnalogObjectOptions options,
    ISelectedItems protoTypeSelectedItems)
  {
    this.PrepareData(session);
    try
    {
      this.DoExecute(session, options, protoTypeSelectedItems);
    }
    finally
    {
      TechcardClientUtils.StopCreateRelations(session);
    }
  }
}
