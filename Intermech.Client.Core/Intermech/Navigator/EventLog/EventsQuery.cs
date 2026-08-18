
// Type: Intermech.Navigator.EventLog.EventsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Search.EventLog;
using Intermech.Search.EventLogFilters;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Navigator.EventLog;

/// <summary>
/// Реализует запрос к журналу событий, который возвращает значения требуемых
/// колонок. Используется для порционного чтения данных.
/// </summary>
public class EventsQuery : DBRecordsNodeQuery, IContextAware
{
  private INodeQuerySupport support;
  private ConditionStructure[] conditions;
  private HybridDictionary tags;

  public EventsQuery(
    INodeQuerySupport support,
    ConditionStructure[] conditions,
    HybridDictionary tags)
    : base((object) ObligatoryObjectAttributes.F_EVENT_ID)
  {
    this.support = support;
    this.conditions = conditions;
    this.tags = tags;
  }

  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping mapping)
  {
    DBRecordSetParams queryParams = base.GetQueryParams(bookmark, count, mapping) with
    {
      Conditions = this.conditions
    };
    if (this.tags != null && this.tags.Count > 0)
    {
      if (queryParams.Tags == null)
        queryParams.Tags = new HybridDictionary();
      IDictionaryEnumerator enumerator = this.tags.GetEnumerator();
      while (enumerator.MoveNext())
        queryParams.Tags[enumerator.Key] = enumerator.Value;
    }
    return queryParams;
  }

  protected override DBRecordSetParams GetQueryParams(object[] recordIds, RecordMapping mapping)
  {
    ConditionStructure joinedCondition = new ConditionStructure(-30, RelationalOperators.In, (object) recordIds, LogicalOperators.NONE, 0, false);
    DBRecordSetParams queryParams = base.GetQueryParams(recordIds, mapping) with
    {
      Conditions = ConditionStructure.Join(joinedCondition, this.conditions)
    };
    if (this.tags != null && this.tags.Count > 0)
    {
      if (queryParams.Tags == null)
        queryParams.Tags = new HybridDictionary();
      IDictionaryEnumerator enumerator = this.tags.GetEnumerator();
      while (enumerator.MoveNext())
        queryParams.Tags[enumerator.Key] = enumerator.Value;
    }
    return queryParams;
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
    else
    {
      queryParams.SortColumns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_BEGIN_DATE
      };
      queryParams.Orders = new SortOrders[1]
      {
        SortOrders.DESC
      };
      mapping.SortFields = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_BEGIN_DATE
      };
      mapping.SortOrders = new NodeColumnSortOrder[1]
      {
        NodeColumnSortOrder.Descending
      };
    }
    if (this.tags != null && this.tags.Count > 0)
    {
      if (queryParams.Tags == null)
        queryParams.Tags = new HybridDictionary();
      IDictionaryEnumerator enumerator = this.tags.GetEnumerator();
      while (enumerator.MoveNext())
        queryParams.Tags[enumerator.Key] = enumerator.Value;
    }
    return queryParams;
  }

  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    this.ApplyEventLogFilter(ref queryParams);
    IEventLogProvider service = this.Services != null ? this.Services.GetService(typeof (IEventLogProvider)) as IEventLogProvider : (IEventLogProvider) null;
    bool archiveMode = service != null && service.EventLog == EventLogs.Archival;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (queryParams.Tags != null && queryParams.Tags.Contains((object) Consts.ObjectVersionID))
      {
        long tag = (long) queryParams.Tags[(object) Consts.ObjectVersionID];
        if (ObjectHelper.IsUnknownObjectVersionID(tag))
          return (DataTable) null;
        return sessionKeeper.Session.GetObject(tag, false)?.GetEventsList(queryParams, true, archiveMode);
      }
      IEventLog eventLog = archiveMode ? sessionKeeper.Session.EventLogArchive : sessionKeeper.Session.EventLog;
      BeforeClientRecordsSelectEventArgs args = new BeforeClientRecordsSelectEventArgs(queryParams, sessionKeeper.Session, (IServiceProvider) null);
      QueryEvents.FireBeforeClientRecordsSelect((object) this, args);
      DBRecordSetParams paramSet = args.NewParameters.HasValue ? args.NewParameters.Value : queryParams;
      return eventLog.Select(paramSet, true);
    }
  }

  protected override INodeQuerySupport Support => this.support;

  public IServiceProvider Services { get; set; }

  private void ApplyEventLogFilter(ref DBRecordSetParams recordSetParams)
  {
    IEventLogFilterProvider service = this.Services != null ? this.Services.GetService(typeof (IEventLogFilterProvider)) as IEventLogFilterProvider : (IEventLogFilterProvider) null;
    if (service == null || service.Filter == null)
      return;
    recordSetParams.Conditions = ConditionStructure.Join(EventLogFiltersHelper.CreateConditionsFromFilter(service.Filter), recordSetParams.Conditions ?? new ConditionStructure[0]);
  }
}
