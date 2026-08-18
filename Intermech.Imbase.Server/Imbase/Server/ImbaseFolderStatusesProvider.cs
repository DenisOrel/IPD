// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseFolderStatusesProvider
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server;

public class ImbaseFolderStatusesProvider
{
  private const string Guid = "76FE1A02-6226-431C-84B6-C4D3A8C78AE3";
  private static bool _checkApplicability;

  public static void Load(System.Guid sessionGuid)
  {
    IElementStatusesService service1 = ServiceUtils.GetService<IElementStatusesService>((object) ServerServices.ServiceContainer, true);
    IEventLogHelper service2 = ServiceUtils.GetService<IEventLogHelper>((object) ServerServices.ServiceContainer, true);
    ElementStatusesPluginDescription serverPlugin = new ElementStatusesPluginDescription(32 /*0x20*/, "76FE1A02-6226-431C-84B6-C4D3A8C78AE3", (string) null, "Папки Imbase", "Статус папки Imbase");
    service1.RegisterServerPlugin(serverPlugin);
    ImbaseFolderStatusesProvider._checkApplicability = ServiceUtils.GetService<IImbaseParamsService>((object) ServerServices.ServiceContainer, true).CommonParams.CheckApplicabilityBeforeCreateComposition;
    service2.GetRecordsListEvent += new GetRecordsListHandler(ImbaseFolderStatusesProvider.EventLogHelper_GetRecordsListEvent);
  }

  public static void SetStatusValues(ImbaseCommonParams commonParams)
  {
    ImbaseFolderStatusesProvider._checkApplicability = commonParams.CheckApplicabilityBeforeCreateComposition;
    IPluginStatusesTable service = ServiceUtils.GetService<IPluginStatusesTable>((object) ServerServices.ServiceContainer, true);
    service.RemoveStatuses("76FE1A02-6226-431C-84B6-C4D3A8C78AE3");
    service.AddStatus("76FE1A02-6226-431C-84B6-C4D3A8C78AE3", 1, EnumTypeHelper.GetCaption((Enum) ApplicabilityStatusEnum.NoLimit), commonParams.FolderApplicabilityIcons.NoRestrictionImageData);
    service.AddStatus("76FE1A02-6226-431C-84B6-C4D3A8C78AE3", 2, EnumTypeHelper.GetCaption((Enum) ApplicabilityStatusEnum.ForbiddenUse), commonParams.FolderApplicabilityIcons.DenyAddRecordImageData);
    service.AddStatus("76FE1A02-6226-431C-84B6-C4D3A8C78AE3", 3, EnumTypeHelper.GetCaption((Enum) ApplicabilityStatusEnum.LimitedUse), commonParams.FolderApplicabilityIcons.DenyAddObjectImageData);
    service.AddStatus("76FE1A02-6226-431C-84B6-C4D3A8C78AE3", 4, EnumTypeHelper.GetCaption((Enum) ApplicabilityStatusEnum.TotalForbiddenUse), commonParams.FolderApplicabilityIcons.DenyAllImageData);
  }

  public static void Unload()
  {
    IPluginStatusesTable service1 = ServiceUtils.GetService<IPluginStatusesTable>((object) ServerServices.ServiceContainer, true);
    IEventLogHelper service2 = ServiceUtils.GetService<IEventLogHelper>((object) ServerServices.ServiceContainer, true);
    service1.RemoveStatuses("76FE1A02-6226-431C-84B6-C4D3A8C78AE3");
    GetRecordsListHandler recordsListHandler = new GetRecordsListHandler(ImbaseFolderStatusesProvider.EventLogHelper_GetRecordsListEvent);
    service2.GetRecordsListEvent -= recordsListHandler;
  }

  private static void EventLogHelper_GetRecordsListEvent(
    DataTable table,
    object sender,
    DBRecordSetParams parameters,
    IUserSession session)
  {
    if (!ImbaseFolderStatusesProvider._checkApplicability || table == null || parameters.ColumnsInfo == null || session == null)
      return;
    ImbaseFolderStatusesProvider.SetFolderStatus(session, table, parameters);
  }

  private static void SetFolderStatus(
    IUserSession session,
    DataTable table,
    DBRecordSetParams parameters)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    if (parameters.ColumnsInfo == null)
      throw new ArgumentException();
    ColumnInfo columnInfo = ((IEnumerable<ColumnInfo>) parameters.ColumnsInfo).FirstOrDefault<ColumnInfo>((System.Func<ColumnInfo, bool>) (x => CoreHelper.GetAttributeTypeID(x) == -77));
    int columnIndex = Array.IndexOf<ColumnInfo>(parameters.ColumnsInfo, columnInfo);
    if (columnIndex == -1)
      return;
    List<Tuple<int, AttributeSourceTypes>> list = ((IEnumerable<ColumnInfo>) parameters.ColumnsInfo).Select<ColumnInfo, Tuple<int, AttributeSourceTypes>>((System.Func<ColumnInfo, Tuple<int, AttributeSourceTypes>>) (x => new Tuple<int, AttributeSourceTypes>(CoreHelper.GetAttributeTypeID(x), CoreHelper.GetAttributeSourceType(x)))).ToList<Tuple<int, AttributeSourceTypes>>();
    Tuple<int, AttributeSourceTypes> tuple1 = list.FirstOrDefault<Tuple<int, AttributeSourceTypes>>((System.Func<Tuple<int, AttributeSourceTypes>, bool>) (x => x.Item1 == -2 && x.Item2 == AttributeSourceTypes.Object));
    if (tuple1 == null)
      return;
    int attrObjectIdIndx = list.IndexOf(tuple1);
    if (attrObjectIdIndx == -1)
      return;
    Tuple<int, AttributeSourceTypes> tuple2 = list.FirstOrDefault<Tuple<int, AttributeSourceTypes>>((System.Func<Tuple<int, AttributeSourceTypes>, bool>) (x => x.Item1 == -7 && x.Item2 == AttributeSourceTypes.Object));
    Tuple<int, AttributeSourceTypes> tuple3 = list.FirstOrDefault<Tuple<int, AttributeSourceTypes>>((System.Func<Tuple<int, AttributeSourceTypes>, bool>) (x => x.Item1 == MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseUsingAttGUID) && x.Item2 == AttributeSourceTypes.Object));
    Dictionary<long, string> applicabilityCache = new Dictionary<long, string>();
    if (tuple2 == null || tuple3 == null)
    {
      long[] array = table.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[attrObjectIdIndx]))).ToArray<long>();
      ImbaseFolderStatusesProvider.FillCache(session, array, applicabilityCache);
    }
    else
    {
      int attrApplicabilityIndx = list.IndexOf(tuple3);
      if (attrApplicabilityIndx == -1)
        return;
      int attrObjTypeInds = list.IndexOf(tuple2);
      if (attrObjTypeInds == -1)
        return;
      foreach (Tuple<long, string> tuple4 in table.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[attrObjTypeInds]) == MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID))).Select<DataRow, Tuple<long, string>>((System.Func<DataRow, Tuple<long, string>>) (x => new Tuple<long, string>(Convert.ToInt64(x[attrObjectIdIndx]), Convert.ToString(x[attrApplicabilityIndx])))).ToList<Tuple<long, string>>())
      {
        if (!applicabilityCache.ContainsKey(tuple4.Item1))
          applicabilityCache.Add(tuple4.Item1, tuple4.Item2);
      }
    }
    if (applicabilityCache.Count == 0)
      return;
    IElementStatusesService service = ServiceUtils.GetService<IElementStatusesService>((object) ServerServices.ServiceContainer, true);
    table.BeginLoadData();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      long int64 = Convert.ToInt64(row[attrObjectIdIndx]);
      string statusStr;
      if (applicabilityCache.TryGetValue(int64, out statusStr))
      {
        ApplicabilityStatusEnum status = ApplicabilityStatusHelper.GetStatus(statusStr);
        if (status != ApplicabilityStatusEnum.None)
        {
          byte[] elementStatuses = (byte[]) ((Array) row[columnIndex]).Clone();
          service.SetElementStatuses32("76FE1A02-6226-431C-84B6-C4D3A8C78AE3", elementStatuses, (int) status);
          row[columnIndex] = (object) elementStatuses;
        }
      }
    }
    table.EndLoadData();
    table.AcceptChanges();
  }

  private static void FillCache(
    IUserSession session,
    long[] objIds,
    Dictionary<long, string> applicabilityCache)
  {
    if (objIds == null || objIds.Length == 0)
      return;
    IDBObjectCollection objectCollection = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID));
    objectCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeGUID);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) objIds, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseUsingAttGUID), AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable dataTable = objectCollection.Select(paramSet);
    if (dataTable == null || dataTable.Rows.Count == 0)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      string str = Convert.ToString(row[1]);
      if (!applicabilityCache.ContainsKey(int64))
        applicabilityCache.Add(int64, str);
    }
  }
}
