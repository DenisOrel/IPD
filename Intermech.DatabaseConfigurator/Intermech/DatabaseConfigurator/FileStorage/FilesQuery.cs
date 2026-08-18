// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FilesQuery
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Objects;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Data;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FilesQuery : DBRecordsNodeQuery
{
  private INodeQuerySupport support;
  private ConditionStructure[] conditions;
  private long _storageID;

  public FilesQuery(INodeQuerySupport support, ConditionStructure[] conditions, long storageID)
    : base((object) ObligatoryObjectAttributes.F_FILE_ID)
  {
    this.support = support;
    this.conditions = conditions;
    this._storageID = storageID;
  }

  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping mapping)
  {
    return base.GetQueryParams(bookmark, count, mapping) with
    {
      Conditions = this.conditions
    };
  }

  protected override DBRecordSetParams GetQueryParams(object[] recordIds, RecordMapping mapping)
  {
    ConditionStructure joinedCondition = new ConditionStructure(-70, RelationalOperators.In, (object) recordIds, LogicalOperators.NONE, 0, false);
    return base.GetQueryParams(recordIds, mapping) with
    {
      Conditions = ConditionStructure.Join(joinedCondition, this.conditions)
    };
  }

  protected override DBRecordSetParams GetQueryParams(RecordMapping mapping, bool withSortInfo)
  {
    DBRecordSetParams queryParams = new DBRecordSetParams((ConditionStructure[]) null);
    queryParams.Columns = new object[mapping.Fields.Length];
    for (int index = 0; index < queryParams.Columns.Length; ++index)
      queryParams.Columns[index] = mapping.Fields[index];
    if (withSortInfo && mapping.SortFields != null)
    {
      queryParams.SortColumns = new object[mapping.SortFields.Length];
      queryParams.Orders = new SortOrders[mapping.SortFields.Length];
      for (int index = 0; index < mapping.SortFields.Length; ++index)
      {
        queryParams.SortColumns[index] = mapping.SortFields[index];
        queryParams.Orders[index] = mapping.SortOrders[index] == NodeColumnSortOrder.Ascending ? SortOrders.ASC : SortOrders.DESC;
      }
    }
    return queryParams;
  }

  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObject(this._storageID, false) is IBlobStorageObject blobStorageObject))
        return (DataTable) null;
      BeforeClientRecordsSelectEventArgs args = new BeforeClientRecordsSelectEventArgs(queryParams, sessionKeeper.Session, (IServiceProvider) null);
      QueryEvents.FireBeforeClientRecordsSelect((object) this, args);
      DBRecordSetParams paramSet = args.NewParameters.HasValue ? args.NewParameters.Value : queryParams;
      return blobStorageObject.Select(paramSet);
    }
  }

  protected override INodeQuerySupport Support => this.support;
}
