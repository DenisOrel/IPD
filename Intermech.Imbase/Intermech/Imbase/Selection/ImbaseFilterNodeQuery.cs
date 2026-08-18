// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFilterNodeQuery
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Selection;

internal class ImbaseFilterNodeQuery : RelatedObjectsQuery
{
  private DataTable _dtFilter;

  public ImbaseFilterNodeQuery(
    INodeQuerySupport support,
    long objId,
    int objTypeId,
    RelatedObjectsRole role,
    int relTypeId,
    ConditionStructure[] conditions)
    : base(support, objId, objTypeId, role, relTypeId, conditions)
  {
    this._parentObjTypeID = Intermech.Imbase.Consts.ImbaseRootObjectTypeID;
  }

  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    DataTable dataTable = base.GetDataTable(queryParams);
    if (this._dtFilter != null && this._dtFilter.Rows.Count > 0 && queryParams.Columns != null && queryParams.Columns.Length != 0)
    {
      int index1 = ((IEnumerable<object>) queryParams.Columns).ToList<object>().FindIndex((Predicate<object>) (x => Convert.ToString(x) == "F_OBJECT_ID"));
      int index2 = 0;
      while (index2 < dataTable.Rows.Count)
      {
        long id = Convert.ToInt64(dataTable.Rows[index2][index1 != -1 ? index1 : 1]);
        if (this._dtFilter.AsEnumerable().Count<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_OBJECT_ID"]) == id)) > 0)
          ++index2;
        else
          dataTable.Rows.Remove(dataTable.Rows[index2]);
      }
    }
    return dataTable;
  }

  public void SetFilter(DataTable dt) => this._dtFilter = dt;
}
