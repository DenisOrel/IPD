
// Type: Intermech.Navigator.Queries.ObjectApplicabilityQueryBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Базовый класс запроса на получение применяемости объекта
/// </summary>
public abstract class ObjectApplicabilityQueryBase : DBRecordsNodeQuery
{
  private INodeQuerySupport _support;

  /// <summary>Конструктор</summary>
  /// <param name="objectVersionID">Идентификатор версии объекта</param>
  /// <param name="support">Хелпер запроса</param>
  /// <exception cref="T:System.ArgumentException"></exception>
  /// <exception cref="T:System.ArgumentNullException">support</exception>
  public ObjectApplicabilityQueryBase(long objectVersionID, INodeQuerySupport support)
    : base((object) null)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    if (support == null)
      throw new ArgumentNullException(nameof (support));
    this.ObjectVersionID = objectVersionID;
    this._support = support;
  }

  /// <summary>Получить идентификатор версии объекта</summary>
  /// <value>Идентификатор версии объекта</value>
  public long ObjectVersionID { get; private set; }

  protected override DBRecordSetParams GetQueryParams(RecordMapping mapping, bool withSortInfo)
  {
    DBRecordSetParams queryParams = new DBRecordSetParams()
    {
      Columns = ((IEnumerable<object>) mapping.Fields).Select<object, object>((Func<object, object>) (o => this.ConvertColumn(o))).ToArray<object>()
    };
    queryParams.ColumnsInfo = ((IEnumerable<object>) queryParams.Columns).Select<object, Intermech.Kernel.Search.ColumnInfo>((Func<object, Intermech.Kernel.Search.ColumnInfo>) (o => new Intermech.Kernel.Search.ColumnInfo(o, AttributeSourceTypes.Object, (object) null))).ToArray<Intermech.Kernel.Search.ColumnInfo>();
    if (withSortInfo && mapping.SortFields != null && mapping.SortOrders != null)
    {
      queryParams.SortColumns = ((IEnumerable<object>) mapping.SortFields).Select<object, object>((Func<object, object>) (o => this.ConvertColumn(o))).ToArray<object>();
      queryParams.Orders = ((IEnumerable<NodeColumnSortOrder>) mapping.SortOrders).Select<NodeColumnSortOrder, SortOrders>((Func<NodeColumnSortOrder, SortOrders>) (o => this.ConvertSortOrder(o))).ToArray<SortOrders>();
    }
    return queryParams;
  }

  protected override INodeQuerySupport Support => this._support;

  private object ConvertColumn(object column)
  {
    return column is NodeColumnID ? (object) ((NodeColumnID) column).AttributeID : column;
  }

  private SortOrders ConvertSortOrder(NodeColumnSortOrder nodeColumnSortOrder)
  {
    if (nodeColumnSortOrder == NodeColumnSortOrder.Ascending)
      return SortOrders.ASC;
    return nodeColumnSortOrder == NodeColumnSortOrder.Descending ? SortOrders.DESC : SortOrders.NONE;
  }
}
