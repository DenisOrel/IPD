// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Filters.ObjectFilterService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Filters;

internal class ObjectFilterService : LongLifeObject, IObjectFilterService, ICommonFilterService
{
  private readonly ObjFilterCacheService _objCacheService;

  private void InitializeData()
  {
  }

  public ObjectFilterService()
  {
    this._objCacheService = new ObjFilterCacheService();
    this.InitializeData();
  }

  public List<ImbaseObjFilterInfo> GetFilterList(Guid sessionGuid, int refObjTypeId)
  {
    if (sessionGuid == Guid.Empty)
      return (List<ImbaseObjFilterInfo>) null;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    return session == null ? (List<ImbaseObjFilterInfo>) null : this._objCacheService.GetFilterList(session, refObjTypeId);
  }

  public bool GetFilterData(Guid sessionGuid, long filterObjId, out ImbaseObjFilterData filterData)
  {
    filterData = (ImbaseObjFilterData) null;
    if (sessionGuid == Guid.Empty || filterObjId == 0L)
      return false;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    return session != null && this._objCacheService.GetFilterData(session, filterObjId, out filterData);
  }

  public bool SetFilterData(Guid sessionGuid, long filterObjId, ImbaseObjFilterData filterData)
  {
    if (sessionGuid == Guid.Empty || filterObjId == 0L)
      return false;
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    return session != null && this._objCacheService.SetFilterData(session, filterObjId, filterData);
  }

  public DataTable ApplyFilter(
    Guid sessionGuid,
    long filterObjId,
    string ownerGuid,
    DataTable dataTable,
    HybridDictionary extArgs)
  {
    if (sessionGuid == Guid.Empty)
      return (DataTable) null;
    if (filterObjId == 0L || dataTable == null)
      return (DataTable) null;
    objId = 0L;
    Dictionary<CalcAttrPair, CalculatedAttr> parms = (Dictionary<CalcAttrPair, CalculatedAttr>) null;
    if (extArgs != null)
    {
      if (!extArgs.Contains((object) ObjectFilterConsts.args_ObjectID) || !(extArgs[(object) ObjectFilterConsts.args_ObjectID] is long objId))
        ;
      if (extArgs.Contains((object) ObjectFilterConsts.args_ExtraAttrs) && extArgs[(object) ObjectFilterConsts.args_ExtraAttrs] is Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> extArg)
      {
        parms = new Dictionary<CalcAttrPair, CalculatedAttr>();
        foreach (KeyValuePair<TypedInfoItem, IEnumerable<AttributeValues>> keyValuePair in extArg)
        {
          if (keyValuePair.Value != null)
          {
            foreach (AttributeValues attributeValues in keyValuePair.Value)
            {
              CalcAttrPair calcAttrPair = new CalcAttrPair(keyValuePair.Key.ItemID, attributeValues.AttributeID);
              if (attributeValues.Values == null)
                parms.Add(calcAttrPair, new CalculatedAttr(calcAttrPair, (object) attributeValues.Values));
              else if (attributeValues.Values.Length == 1)
              {
                parms.Add(calcAttrPair, new CalculatedAttr(calcAttrPair, attributeValues.Values[0]));
              }
              else
              {
                PacketValue packetValue = new PacketValue();
                foreach (object O in attributeValues.Values)
                  packetValue.Add(new ExpertValue(DataTypeConvertor.GetDataType(O), O));
                parms.Add(calcAttrPair, new CalculatedAttr(calcAttrPair, attributeValues.Values[0]));
              }
            }
          }
        }
      }
    }
    IExpertServer service = ServiceUtils.GetService<IExpertServer>((object) ApplicationServices.Container, false);
    if (service == null)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_26"), (object) typeof (IExpertServer)));
    IUserSession session = ImbaseServer.GetSession(sessionGuid);
    if (session == null)
      return (DataTable) null;
    DataTable dataTable1 = dataTable.Copy();
    dataTable1.RemotingFormat = SerializationFormat.Binary;
    ImbaseObjFilterData filterData;
    if (!this._objCacheService.GetFilterData(session, filterObjId, out filterData) || filterData == null || filterData.Items.Count == 0)
      return dataTable1;
    DataTable dataTable2 = (DataTable) null;
    ImbaseObjFilterItemList objFilterItemList = new ImbaseObjFilterItemList(filterData.Items);
    objFilterItemList.Sort();
    int taskId = service.StartTask(sessionGuid);
    try
    {
      if (parms != null)
        service.SetCalcParms(taskId, parms);
      foreach (ImbaseObjFilterItem filterItem in (List<ImbaseObjFilterItem>) objFilterItemList)
      {
        if (filterItem != null)
        {
          if (filterItem.Condition == null)
          {
            dataTable2 = ImbaseObjFilterDataHelper.GetFilterItemData(filterItem);
            break;
          }
          object obj;
          int num = (int) service.CalcFormula(taskId, (object) filterItem.Condition, objId, out obj);
          bool boolean = Convert.ToBoolean(obj);
          if (num == 1 && boolean)
          {
            dataTable2 = ImbaseObjFilterDataHelper.GetFilterItemData(filterItem);
            break;
          }
        }
      }
    }
    finally
    {
      service.EndTask(taskId);
    }
    if (dataTable2 == null || dataTable2.Rows.Count == 0)
      return dataTable1;
    string ownerSqlCond = FolderFilterService.GetOwnerSQLCond(ownerGuid);
    DataRow[] filterRows = dataTable2.Select(ownerSqlCond);
    return !FolderFilterService.IsFoldersContainsFilter(dataTable, filterRows, sessionGuid) ? dataTable1 : FolderFilterService.ApplyFilter(sessionGuid, filterRows, dataTable1);
  }

  DataTable IObjectFilterService.LoadCatalogTable(Guid sessionGuid, long catalogId)
  {
    return FolderFilterService.LoadCatalogTable(sessionGuid, catalogId, false, Intermech.Imbase.Consts.ImbaseFolderTypeID);
  }

  DataTable IObjectFilterService.RemoveWithMissingParents(
    DataTable dt,
    int recObjTypeId,
    int classifKeyColumnIndex)
  {
    return FolderFilterService.RemoveWithMissingParents(dt, recObjTypeId, classifKeyColumnIndex);
  }

  public void SubscribeOnSystemlEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    this._objCacheService.SubscribeOnSystemEvents(eventHelper);
  }

  public void UnSubscribeOnSystemEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    this._objCacheService.UnsubscribeOnSystemEvents(eventHelper);
  }
}
