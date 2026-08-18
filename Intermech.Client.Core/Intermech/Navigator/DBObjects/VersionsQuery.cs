
// Type: Intermech.Navigator.DBObjects.VersionsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Navigator.DBObjects;

internal sealed class VersionsQuery : ObjectsQuery
{
  private readonly long _id;

  public VersionsQuery(
    INodeQuerySupport support,
    long id,
    ConditionStructure[] conditions,
    IServiceProvider services)
    : base(support, -1, conditions, services)
  {
    this._id = id;
  }

  protected override DataTable OnSelect(IUserSession session, DBRecordSetParams queryParams)
  {
    List<int> versionsObjectTypes = VersionsHelper.GetVersionsObjectTypes(session, this._id);
    DataTable toTable = (DataTable) null;
    int columnIndex = Array.IndexOf<object>(queryParams.Columns, (object) ObligatoryObjectAttributes.F_OBJECT_ID);
    for (int index1 = 0; index1 < versionsObjectTypes.Count; ++index1)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(versionsObjectTypes[index1]);
      this.PrepareCollection(objectCollection);
      DataTable dataTable = objectCollection.Select(queryParams);
      if (versionsObjectTypes.Count == 1)
        return dataTable;
      if (toTable == null)
        toTable = dataTable.Clone();
      for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
      {
        if (toTable.Select($"[{columnIndex}]={dataTable.Rows[index2][columnIndex]}").Length == 0)
          DataSetProcessor.AddRow(toTable, dataTable.Rows[index2], false);
      }
      toTable.AcceptChanges();
    }
    return toTable;
  }
}
