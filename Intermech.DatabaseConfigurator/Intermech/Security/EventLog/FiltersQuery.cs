// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FiltersQuery
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Security.EventLog;

internal class FiltersQuery : BaseNodeQuery
{
  private INodeQuerySupport support;
  private List<FiltersQuery.ResultRow> rows;
  public const string F_GUID = "F_GUID";
  public const string F_CAPTION = "F_CAPTION";

  public FiltersQuery(INodeQuerySupport support)
  {
    this.support = support;
    this.rows = new List<FiltersQuery.ResultRow>();
  }

  protected override NodeQueryResult Execute(object bookmark, int count, RecordMapping mapping)
  {
    int position1 = bookmark != null ? ((PositionBookmark) bookmark).Position : 0;
    if (position1 + count > FiltersManager.Filters.Count)
      count = FiltersManager.Filters.Count - position1;
    if (count <= 0)
      return NodeQueryResult.Empty;
    this.rows.Clear();
    for (int index = 0; index < count; ++index)
    {
      Filter filter = FiltersManager.Filters[position1 + index];
      this.rows.Add(new FiltersQuery.ResultRow(filter.Guid, filter.Name));
    }
    int position2 = position1 + count;
    return new NodeQueryResult(position2 < FiltersManager.Filters.Count ? (object) new PositionBookmark(position2) : (object) (PositionBookmark) null, count, this.TotalRecordCount, FiltersQuery.ResultRow.FieldsOrder);
  }

  protected override NodeQueryResult Execute(object[] recordIds, RecordMapping mapping)
  {
    this.rows.Clear();
    for (int index = 0; index < recordIds.Length; ++index)
    {
      Filter filter = FiltersManager.Filters.FindFilter((Guid) recordIds[index]);
      if (filter != null)
        this.rows.Add(new FiltersQuery.ResultRow(filter.Guid, filter.Name));
    }
    return new NodeQueryResult(this.rows.Count, this.TotalRecordCount, FiltersQuery.ResultRow.FieldsOrder);
  }

  protected override object[] GetFieldValues(int index) => this.rows[index].ItemArray;

  protected override INodeQuerySupport Support => this.support;

  private class ResultRow
  {
    private object[] itemArray;
    public static readonly object[] FieldsOrder = new object[2]
    {
      (object) "F_GUID",
      (object) "F_CAPTION"
    };

    public ResultRow(Guid filterGuid, string caption)
    {
      this.itemArray = new object[2];
      this.itemArray[0] = (object) filterGuid;
      this.itemArray[1] = (object) caption;
    }

    public object[] ItemArray => this.itemArray;
  }
}
