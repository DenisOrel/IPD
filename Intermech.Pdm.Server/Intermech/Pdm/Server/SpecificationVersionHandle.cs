// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.SpecificationVersionHandle
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

internal sealed class SpecificationVersionHandle : DocumentVersionHandle
{
  protected override List<Tuple<long, int>> FindDocuments(IUserSession session, long articleID)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.DocRelationTypeID);
    relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }), articleID);
    if (dataTable.Rows.Count <= 0)
      return (List<Tuple<long, int>>) null;
    return new List<Tuple<long, int>>((IEnumerable<Tuple<long, int>>) new Tuple<long, int>[1]
    {
      new Tuple<long, int>(Convert.ToInt64(dataTable.Rows[0][0]), objectTypeId)
    });
  }
}
