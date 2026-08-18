// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDbObjectCollectionSelectHelper
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

public static class IDbObjectCollectionSelectHelper
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable SelectWithLocalObjects(
    [NotNull] this IDBObjectCollection objectCollection,
    [CanBeNull] ConditionStructure[] conditions,
    [CanBeNull] ColumnDescriptor[] columns,
    long lastKeyValue = 0,
    [CanBeNull] object lastOrderValue = null,
    int recordCount = -1,
    bool failIfNotFound = true,
    [CanBeNull] string tableName = null)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns, lastKeyValue, lastOrderValue, recordCount)
    {
      FailIfNotFound = failIfNotFound,
      TableName = tableName ?? string.Empty
    };
    return objectCollection.SelectWithLocalObjects(paramSet);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable SelectWithLocalObjects(
    [NotNull] this IDBObjectCollection objectCollection,
    [CanBeNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    long lastKeyValue = 0,
    [CanBeNull] object lastOrderValue = null,
    int recordCount = -1,
    bool failIfNotFound = true,
    [CanBeNull] string tableName = null)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns, lastKeyValue, lastOrderValue, recordCount)
    {
      FailIfNotFound = failIfNotFound,
      TableName = tableName ?? string.Empty
    };
    return objectCollection.SelectWithLocalObjects(paramSet);
  }
}
