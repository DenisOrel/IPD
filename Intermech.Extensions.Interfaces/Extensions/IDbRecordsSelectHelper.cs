// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDbRecordsSelectHelper
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Data;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IDbRecordsSelectHelper
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable Select(
    [NotNull] this IDBRecords dbRecords,
    [CanBeNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    [CanBeNull, ItemNotNull] object[] sortColumns = null,
    [CanBeNull] SortOrders[] orders = null,
    long lastKeyValue = 0,
    [CanBeNull] object lastOrderValue = null,
    int recordCount = -1,
    bool failIfNotFound = true,
    [CanBeNull] string tableName = null)
  {
    return dbRecords.Select(new DBRecordSetParams(conditions, columns, lastKeyValue, lastOrderValue, recordCount)
    {
      SortColumns = sortColumns,
      Orders = orders,
      FailIfNotFound = failIfNotFound,
      TableName = tableName ?? string.Empty
    });
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable Select(
    [NotNull] this IDBRecords dbRecords,
    [CanBeNull] ConditionStructure[] conditions,
    [CanBeNull] ColumnDescriptor[] columns,
    [CanBeNull, ItemNotNull] object[] sortColumns = null,
    [CanBeNull] SortOrders[] orders = null,
    long lastKeyValue = 0,
    [CanBeNull] object lastOrderValue = null,
    int recordCount = -1,
    bool failIfNotFound = true,
    [CanBeNull] string tableName = null)
  {
    return dbRecords.Select(new DBRecordSetParams(conditions, columns, lastKeyValue, lastOrderValue, recordCount)
    {
      SortColumns = sortColumns,
      Orders = orders,
      FailIfNotFound = failIfNotFound,
      TableName = tableName ?? string.Empty
    });
  }
}
