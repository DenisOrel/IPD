
// Type: Intermech.Navigator.ListInstances.ListInstancesQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Navigator.ListInstances;

internal class ListInstancesQuery(
  INodeQuerySupport support,
  int objTypeID,
  ConditionStructure[] conditions,
  IServiceProvider services) : ObjectsQuery(support, objTypeID, conditions, services)
{
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    DataTable dataTable = base.GetDataTable(queryParams);
    ((ListInstancesPart) this.support).InstancesIDs = new List<long>(dataTable.Rows.Count);
    int columnIndex = Array.IndexOf<object>(this.mapping.Fields, (object) ObjectsPartBase.ncF_OBJECT_ID);
    if (columnIndex >= 0)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        ((ListInstancesPart) this.support).InstancesIDs.Add(Convert.ToInt64(dataTable.Rows[index][columnIndex]));
    }
    return dataTable;
  }
}
