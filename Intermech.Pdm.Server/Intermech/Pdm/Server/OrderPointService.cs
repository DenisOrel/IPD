// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.OrderPointService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

internal class OrderPointService : LongLifeObject, IOrderPointService
{
  public Dictionary<long, long> GetDeployedCompositionInfo(
    Guid sessionGuid,
    long assemblyUnitObjectID)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return (Dictionary<long, long>) null;
    if (!(ServerServices.GetService(typeof (ICompositionLoadService)) is ICompositionLoadService service))
      return (Dictionary<long, long>) null;
    Dictionary<long, long> deployedCompositionInfo = new Dictionary<long, long>();
    List<int> intList = new List<int>()
    {
      MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad00250-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad0038d-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad00252-306c-11d8-b4e9-00304f19f545")
    };
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID)
    };
    long objectId = assemblyUnitObjectID;
    int assemblyUnitTypeId = PDMPluginIDs.assemblyUnitTypeID;
    List<int> searchRelationTypes = new List<int>();
    searchRelationTypes.Add(MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545"));
    List<int> searchObjectTypes = intList;
    List<ColumnDescriptor> columns = columnDescriptorList;
    DataTable dataTable = service.LoadComposition((object) sessionById, objectId, assemblyUnitTypeId, (IEnumerable<int>) searchRelationTypes, (IEnumerable<int>) searchObjectTypes, (IEnumerable<ColumnDescriptor>) columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, "", (HybridDictionary) null, -1);
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (Dictionary<long, long>) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      deployedCompositionInfo.Add(Convert.ToInt64(row[0]), Convert.ToInt64(row[1]));
    return deployedCompositionInfo;
  }

  public List<long> GetOrderPoints(Guid sessionGuid, long assemblyUnitObjectID)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return (List<long>) null;
    List<long> orderPoints = new List<long>();
    IDBRelationCollection relationCollection = sessionById.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545"));
    relationCollection.ObjectTypeID = PDMPluginIDs.orderPointTypeID;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable = relationCollection.ConsistFrom(paramSet, assemblyUnitObjectID);
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (List<long>) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      orderPoints.Add(Convert.ToInt64(row[0]));
    return orderPoints;
  }

  public List<long> GetPointComposition(Guid sessionGuid, long pointID)
  {
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return (List<long>) null;
    List<long> pointComposition = new List<long>();
    IDBRelationCollection relationCollection = sessionById.GetRelationCollection(PDMPluginIDs.orderPointCompositionRelationTypeID);
    relationCollection.ChildObjectTypes = (IList<int>) new List<int>()
    {
      MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad00250-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad0038d-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad00252-306c-11d8-b4e9-00304f19f545")
    };
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable = relationCollection.ConsistFrom(paramSet, pointID);
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (List<long>) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (!pointComposition.Contains(Convert.ToInt64(row[0])))
        pointComposition.Add(Convert.ToInt64(row[0]));
    }
    return pointComposition;
  }
}
