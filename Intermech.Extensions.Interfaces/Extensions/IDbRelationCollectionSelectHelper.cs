// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDbRelationCollectionSelectHelper
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IDbRelationCollectionSelectHelper
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ConsistOf(
    [NotNull] this IDBRelationCollection dbRelationCollection,
    [NotEmpty] long objectVersionID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return dbRelationCollection.ConsistFrom(new DBRecordSetParams(conditions, columns), objectVersionID, recursive, actualDate ?? DateTime.UtcNow + dbRelationCollection.Session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ConsistOf(
    [NotNull] this IDBRelationCollection dbRelationCollection,
    [NotEmpty] long objectVersionID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return dbRelationCollection.ConsistFrom(new DBRecordSetParams(conditions, columns), objectVersionID, recursive, actualDate ?? DateTime.UtcNow + dbRelationCollection.Session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable EntersIn(
    [NotNull] this IDBRelationCollection dbRelationCollection,
    [NotEmpty] long objectID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return dbRelationCollection.EntersIn(new DBRecordSetParams(conditions, columns), objectID, recursive, actualDate ?? DateTime.UtcNow + dbRelationCollection.Session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable EntersIn(
    [NotNull] this IDBRelationCollection dbRelationCollection,
    [NotEmpty] long objectID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return dbRelationCollection.EntersIn(new DBRecordSetParams(conditions, columns), objectID, recursive, actualDate ?? DateTime.UtcNow + dbRelationCollection.Session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable EntersInVersion(
    [NotNull] this IDBRelationCollection dbRelationCollection,
    [NotEmpty] long objectVersionID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return dbRelationCollection.EntersInVersion(new DBRecordSetParams(conditions, columns), objectVersionID, recursive, actualDate ?? DateTime.UtcNow + dbRelationCollection.Session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable EntersInVersion(
    [NotNull] this IDBRelationCollection dbRelationCollection,
    [NotEmpty] long objectVersionID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return dbRelationCollection.EntersInVersion(new DBRecordSetParams(conditions, columns), objectVersionID, recursive, actualDate ?? DateTime.UtcNow + dbRelationCollection.Session.TimeZoneOffset);
  }
}
