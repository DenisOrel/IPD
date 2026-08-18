// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DataTableExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Data;
using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class DataTableExtensions
{
  [NotNull]
  public static DataTable DeleteRows([NotNull] this DataTable dataTable, [NotNull, InstantHandle] System.Func<DataRow, bool> predicate)
  {
    bool flag = false;
    lock (dataTable)
    {
      for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
      {
        DataRow row = dataTable.Rows[index];
        if (predicate(row))
        {
          row.Delete();
          flag = true;
        }
      }
      if (flag)
        dataTable.AcceptChanges();
    }
    return dataTable;
  }

  [DebuggerHidden]
  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> Select<T>(
    [NotNull] this DataTable dataTable,
    [NotNull] System.Func<DataRow, T> selector)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, T>(selector).WrapWithCount<T>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> Select<T>(
    [NotNull] this DataTable dataTable,
    [NotNull] Func<DataRow, int, T> selector)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, T>(selector).WrapWithCount<T>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TResult> SelectNotNull<TResult>(
    [NotNull] this DataTable dataTable,
    [NotNull] System.Func<DataRow, TResult> selector)
    where TResult : class
  {
    return dataTable.Rows.Cast<DataRow>().SelectNotNull<DataRow, TResult>(selector);
  }

  [DebuggerHidden]
  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DataRow> Where([NotNull] this DataTable dataTable, [NotNull] System.Func<DataRow, bool> predicate)
  {
    return dataTable.Rows.Cast<DataRow>().Where<DataRow>(predicate);
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DataRow> Where(
    [NotNull] this DataTable dataTable,
    [NotNull] Func<DataRow, int, bool> predicate)
  {
    return dataTable.Rows.Cast<DataRow>().Where<DataRow>(predicate);
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TAccumulate Aggregate<TAccumulate>(
    [NotNull] this DataTable dataTable,
    [CanBeNull] TAccumulate seed,
    [NotNull, InstantHandle] Func<TAccumulate, DataRow, TAccumulate> func)
  {
    return dataTable.Rows.Cast<DataRow>().Aggregate<DataRow, TAccumulate>(seed, func);
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Aggregate<TAccumulate, TResult>(
    [NotNull] this DataTable dataTable,
    [CanBeNull] TAccumulate seed,
    [NotNull, InstantHandle] Func<TAccumulate, DataRow, TAccumulate> func,
    [NotNull, InstantHandle] System.Func<TAccumulate, TResult> resultSelector)
  {
    return dataTable.Rows.Cast<DataRow>().Aggregate<DataRow, TAccumulate, TResult>(seed, func, resultSelector);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool All([NotNull] this DataTable dataTable, [NotNull, InstantHandle] System.Func<DataRow, bool> predicate)
  {
    return dataTable.Rows.Cast<DataRow>().All<DataRow>(predicate);
  }

  public static bool Any([NotNull] this DataTable dataTable)
  {
    return dataTable.Rows.Cast<DataRow>().Any<DataRow>();
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Any([NotNull] this DataTable dataTable, [NotNull, InstantHandle] System.Func<DataRow, bool> predicate)
  {
    return dataTable.Rows.Cast<DataRow>().Any<DataRow>(predicate);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetFieldIndex([NotNull] this DataTable dataTable, [NotNull, NotWhitespace] string fieldName)
  {
    int num = dataTable.Columns.IndexOf(fieldName);
    return num != -1 ? num : throw new FieldWithNameNotFoundException(fieldName);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool TryGetFieldIndex(
    [NotNull] this DataTable dataTable,
    [NotNull, NotWhitespace] string fieldName,
    out int fieldIndex,
    bool aQuiet = false)
  {
    fieldIndex = dataTable.Columns.IndexOf(fieldName);
    if (fieldIndex != -1)
      return true;
    if (!aQuiet)
      throw new FieldWithNameNotFoundException(fieldName);
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool TryConvertFieldNameToFieldIndex(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    out int fieldIndex,
    bool aQuiet = false)
  {
    fieldIndex = dataTable.Columns.IndexOf(fieldName);
    if (fieldIndex != -1)
      return true;
    if (!aQuiet)
      throw new FieldWithNameNotFoundException(fieldName);
    return false;
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> GetValues<T>([CanBeNull] this DataTable dataTable, int columnIndex = 0)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<T>) Array.Empty<T>() : dataTable.GetValues<T>(Field.Custom<T>(columnIndex));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> GetValues<T>([CanBeNull] this DataTable dataTable, [NotNull, NotWhitespace] string columnName)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<T>) Array.Empty<T>() : dataTable.GetValues<T>(Field.Custom<T>(columnName));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> GetValues<T>([CanBeNull] this DataTable dataTable, Field<T> field)
  {
    if (dataTable == null)
      return (IReadOnlyCollection<T>) Array.Empty<T>();
    int count = dataTable.Rows.Count;
    if (count == 0)
      return (IReadOnlyCollection<T>) Array.Empty<T>();
    int fieldIndex;
    if (count > 1 && field.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field.FieldName, out fieldIndex))
      field = field.WithIndex(fieldIndex);
    return dataTable.Rows.GetValues<T>(in field);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2)> GetValues<T1, T2>(
    [CanBeNull] this DataTable dataTable,
    int startColumnIndex = 0)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<(T1, T2)>) Array.Empty<(T1, T2)>() : dataTable.GetValues<T1, T2>(Field.Custom<T1>(startColumnIndex), Field.Custom<T2>(startColumnIndex + 1));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2)> GetValues<T1, T2>(
    [CanBeNull] this DataTable dataTable,
    [NotNull, NotWhitespace] string startColumnName)
  {
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (IReadOnlyCollection<(T1, T2)>) Array.Empty<(T1, T2)>();
    int fieldIndex = dataTable.GetFieldIndex(startColumnName);
    return dataTable.GetValues<T1, T2>(Field.Custom<T1>(fieldIndex), Field.Custom<T2>(fieldIndex + 1));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2)> GetValues<T1, T2>(
    [CanBeNull] this DataTable dataTable,
    Field<T1> field1,
    Field<T2> field2)
  {
    if (dataTable == null)
      return (IReadOnlyCollection<(T1, T2)>) Array.Empty<(T1, T2)>();
    int count = dataTable.Rows.Count;
    if (count == 0)
      return (IReadOnlyCollection<(T1, T2)>) Array.Empty<(T1, T2)>();
    if (count > 1)
    {
      int fieldIndex;
      if (field1.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field1.FieldName, out fieldIndex))
        field1 = field1.WithIndex(fieldIndex);
      if (field2.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field2.FieldName, out fieldIndex))
        field2 = field2.WithIndex(fieldIndex);
    }
    return dataTable.Rows.GetValues<T1, T2>(in field1, in field2);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3)> GetValues<T1, T2, T3>(
    [CanBeNull] this DataTable dataTable,
    int startColumnIndex = 0)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<(T1, T2, T3)>) Array.Empty<(T1, T2, T3)>() : dataTable.GetValues<T1, T2, T3>(Field.Custom<T1>(startColumnIndex), Field.Custom<T2>(startColumnIndex + 1), Field.Custom<T3>(startColumnIndex + 2));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3)> GetValues<T1, T2, T3>(
    [CanBeNull] this DataTable dataTable,
    [NotNull, NotWhitespace] string startColumnName)
  {
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (IReadOnlyCollection<(T1, T2, T3)>) Array.Empty<(T1, T2, T3)>();
    int fieldIndex = dataTable.GetFieldIndex(startColumnName);
    return dataTable.GetValues<T1, T2, T3>(Field.Custom<T1>(fieldIndex), Field.Custom<T2>(fieldIndex + 1), Field.Custom<T3>(fieldIndex + 2));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3)> GetValues<T1, T2, T3>(
    [CanBeNull] this DataTable dataTable,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3)
  {
    if (dataTable == null)
      return (IReadOnlyCollection<(T1, T2, T3)>) Array.Empty<(T1, T2, T3)>();
    int count = dataTable.Rows.Count;
    if (count == 0)
      return (IReadOnlyCollection<(T1, T2, T3)>) Array.Empty<(T1, T2, T3)>();
    if (count > 1)
    {
      int fieldIndex;
      if (field1.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field1.FieldName, out fieldIndex))
        field1 = field1.WithIndex(fieldIndex);
      if (field2.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field2.FieldName, out fieldIndex))
        field2 = field2.WithIndex(fieldIndex);
      if (field3.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field3.FieldName, out fieldIndex))
        field3 = field3.WithIndex(fieldIndex);
    }
    return dataTable.Rows.GetValues<T1, T2, T3>(in field1, in field2, in field3);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4)> GetValues<T1, T2, T3, T4>(
    [CanBeNull] this DataTable dataTable,
    int startColumnIndex = 0)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<(T1, T2, T3, T4)>) Array.Empty<(T1, T2, T3, T4)>() : dataTable.GetValues<T1, T2, T3, T4>(Field.Custom<T1>(startColumnIndex), Field.Custom<T2>(startColumnIndex + 1), Field.Custom<T3>(startColumnIndex + 2), Field.Custom<T4>(startColumnIndex + 3));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4)> GetValues<T1, T2, T3, T4>(
    [CanBeNull] this DataTable dataTable,
    [NotNull, NotWhitespace] string startColumnName)
  {
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4)>) Array.Empty<(T1, T2, T3, T4)>();
    int fieldIndex = dataTable.GetFieldIndex(startColumnName);
    return dataTable.GetValues<T1, T2, T3, T4>(Field.Custom<T1>(fieldIndex), Field.Custom<T2>(fieldIndex + 1), Field.Custom<T3>(fieldIndex + 2), Field.Custom<T4>(fieldIndex + 3));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4)> GetValues<T1, T2, T3, T4>(
    [CanBeNull] this DataTable dataTable,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4)
  {
    if (dataTable == null)
      return (IReadOnlyCollection<(T1, T2, T3, T4)>) Array.Empty<(T1, T2, T3, T4)>();
    int count = dataTable.Rows.Count;
    if (count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4)>) Array.Empty<(T1, T2, T3, T4)>();
    if (count > 1)
    {
      int fieldIndex;
      if (field1.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field1.FieldName, out fieldIndex))
        field1 = field1.WithIndex(fieldIndex);
      if (field2.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field2.FieldName, out fieldIndex))
        field2 = field2.WithIndex(fieldIndex);
      if (field3.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field3.FieldName, out fieldIndex))
        field3 = field3.WithIndex(fieldIndex);
      if (field4.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field4.FieldName, out fieldIndex))
        field4 = field4.WithIndex(fieldIndex);
    }
    return dataTable.Rows.GetValues<T1, T2, T3, T4>(in field1, in field2, in field3, in field4);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5)> GetValues<T1, T2, T3, T4, T5>(
    [CanBeNull] this DataTable dataTable,
    int startColumnIndex = 0)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<(T1, T2, T3, T4, T5)>) Array.Empty<(T1, T2, T3, T4, T5)>() : dataTable.GetValues<T1, T2, T3, T4, T5>(Field.Custom<T1>(startColumnIndex), Field.Custom<T2>(startColumnIndex + 1), Field.Custom<T3>(startColumnIndex + 2), Field.Custom<T4>(startColumnIndex + 3), Field.Custom<T5>(startColumnIndex + 4));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5)> GetValues<T1, T2, T3, T4, T5>(
    [CanBeNull] this DataTable dataTable,
    [NotNull, NotWhitespace] string startColumnName)
  {
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5)>) Array.Empty<(T1, T2, T3, T4, T5)>();
    int fieldIndex = dataTable.GetFieldIndex(startColumnName);
    return dataTable.GetValues<T1, T2, T3, T4, T5>(Field.Custom<T1>(fieldIndex), Field.Custom<T2>(fieldIndex + 1), Field.Custom<T3>(fieldIndex + 2), Field.Custom<T4>(fieldIndex + 3), Field.Custom<T5>(fieldIndex + 4));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5)> GetValues<T1, T2, T3, T4, T5>(
    [CanBeNull] this DataTable dataTable,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4,
    Field<T5> field5)
  {
    if (dataTable == null)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5)>) Array.Empty<(T1, T2, T3, T4, T5)>();
    int count = dataTable.Rows.Count;
    if (count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5)>) Array.Empty<(T1, T2, T3, T4, T5)>();
    if (count > 1)
    {
      int fieldIndex;
      if (field1.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field1.FieldName, out fieldIndex))
        field1 = field1.WithIndex(fieldIndex);
      if (field2.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field2.FieldName, out fieldIndex))
        field2 = field2.WithIndex(fieldIndex);
      if (field3.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field3.FieldName, out fieldIndex))
        field3 = field3.WithIndex(fieldIndex);
      if (field4.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field4.FieldName, out fieldIndex))
        field4 = field4.WithIndex(fieldIndex);
      if (field5.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field5.FieldName, out fieldIndex))
        field5 = field5.WithIndex(fieldIndex);
    }
    return dataTable.Rows.GetValues<T1, T2, T3, T4, T5>(in field1, in field2, in field3, in field4, in field5);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6)> GetValues<T1, T2, T3, T4, T5, T6>(
    [CanBeNull] this DataTable dataTable,
    int startColumnIndex = 0)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6)>) Array.Empty<(T1, T2, T3, T4, T5, T6)>() : dataTable.GetValues<T1, T2, T3, T4, T5, T6>(Field.Custom<T1>(startColumnIndex), Field.Custom<T2>(startColumnIndex + 1), Field.Custom<T3>(startColumnIndex + 2), Field.Custom<T4>(startColumnIndex + 3), Field.Custom<T5>(startColumnIndex + 4), Field.Custom<T6>(startColumnIndex + 5));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6)> GetValues<T1, T2, T3, T4, T5, T6>(
    [CanBeNull] this DataTable dataTable,
    [NotNull, NotWhitespace] string startColumnName)
  {
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6)>) Array.Empty<(T1, T2, T3, T4, T5, T6)>();
    int fieldIndex = dataTable.GetFieldIndex(startColumnName);
    return dataTable.GetValues<T1, T2, T3, T4, T5, T6>(Field.Custom<T1>(fieldIndex), Field.Custom<T2>(fieldIndex + 1), Field.Custom<T3>(fieldIndex + 2), Field.Custom<T4>(fieldIndex + 3), Field.Custom<T5>(fieldIndex + 4), Field.Custom<T6>(fieldIndex + 5));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6)> GetValues<T1, T2, T3, T4, T5, T6>(
    [CanBeNull] this DataTable dataTable,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4,
    Field<T5> field5,
    Field<T6> field6)
  {
    if (dataTable == null)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6)>) Array.Empty<(T1, T2, T3, T4, T5, T6)>();
    int count = dataTable.Rows.Count;
    if (count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6)>) Array.Empty<(T1, T2, T3, T4, T5, T6)>();
    if (count > 1)
    {
      int fieldIndex;
      if (field1.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field1.FieldName, out fieldIndex))
        field1 = field1.WithIndex(fieldIndex);
      if (field2.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field2.FieldName, out fieldIndex))
        field2 = field2.WithIndex(fieldIndex);
      if (field3.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field3.FieldName, out fieldIndex))
        field3 = field3.WithIndex(fieldIndex);
      if (field4.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field4.FieldName, out fieldIndex))
        field4 = field4.WithIndex(fieldIndex);
      if (field5.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field5.FieldName, out fieldIndex))
        field5 = field5.WithIndex(fieldIndex);
      if (field6.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field6.FieldName, out fieldIndex))
        field6 = field6.WithIndex(fieldIndex);
    }
    return dataTable.Rows.GetValues<T1, T2, T3, T4, T5, T6>(in field1, in field2, in field3, in field4, in field5, in field6);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7)> GetValues<T1, T2, T3, T4, T5, T6, T7>(
    [CanBeNull] this DataTable dataTable,
    int startColumnIndex = 0)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7)>) Array.Empty<(T1, T2, T3, T4, T5, T6, T7)>() : dataTable.GetValues<T1, T2, T3, T4, T5, T6, T7>(Field.Custom<T1>(startColumnIndex), Field.Custom<T2>(startColumnIndex + 1), Field.Custom<T3>(startColumnIndex + 2), Field.Custom<T4>(startColumnIndex + 3), Field.Custom<T5>(startColumnIndex + 4), Field.Custom<T6>(startColumnIndex + 5), Field.Custom<T7>(startColumnIndex + 6));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7)> GetValues<T1, T2, T3, T4, T5, T6, T7>(
    [CanBeNull] this DataTable dataTable,
    [NotNull, NotWhitespace] string startColumnName)
  {
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7)>) Array.Empty<(T1, T2, T3, T4, T5, T6, T7)>();
    int fieldIndex = dataTable.GetFieldIndex(startColumnName);
    return dataTable.GetValues<T1, T2, T3, T4, T5, T6, T7>(Field.Custom<T1>(fieldIndex), Field.Custom<T2>(fieldIndex + 1), Field.Custom<T3>(fieldIndex + 2), Field.Custom<T4>(fieldIndex + 3), Field.Custom<T5>(fieldIndex + 4), Field.Custom<T6>(fieldIndex + 5), Field.Custom<T7>(fieldIndex + 6));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7)> GetValues<T1, T2, T3, T4, T5, T6, T7>(
    [CanBeNull] this DataTable dataTable,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4,
    Field<T5> field5,
    Field<T6> field6,
    Field<T7> field7)
  {
    if (dataTable == null)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7)>) Array.Empty<(T1, T2, T3, T4, T5, T6, T7)>();
    int count = dataTable.Rows.Count;
    if (count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7)>) Array.Empty<(T1, T2, T3, T4, T5, T6, T7)>();
    if (count > 1)
    {
      int fieldIndex;
      if (field1.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field1.FieldName, out fieldIndex))
        field1 = field1.WithIndex(fieldIndex);
      if (field2.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field2.FieldName, out fieldIndex))
        field2 = field2.WithIndex(fieldIndex);
      if (field3.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field3.FieldName, out fieldIndex))
        field3 = field3.WithIndex(fieldIndex);
      if (field4.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field4.FieldName, out fieldIndex))
        field4 = field4.WithIndex(fieldIndex);
      if (field5.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field5.FieldName, out fieldIndex))
        field5 = field5.WithIndex(fieldIndex);
      if (field6.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field6.FieldName, out fieldIndex))
        field6 = field6.WithIndex(fieldIndex);
      if (field7.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field7.FieldName, out fieldIndex))
        field7 = field7.WithIndex(fieldIndex);
    }
    return dataTable.Rows.GetValues<T1, T2, T3, T4, T5, T6, T7>(in field1, in field2, in field3, in field4, in field5, in field6, in field7);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7, T8)> GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(
    [CanBeNull] this DataTable dataTable,
    int startColumnIndex = 0)
  {
    return dataTable == null || dataTable.Rows.Count == 0 ? (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7, T8)>) Array.Empty<(T1, T2, T3, T4, T5, T6, T7, T8)>() : dataTable.GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(Field.Custom<T1>(startColumnIndex), Field.Custom<T2>(startColumnIndex + 1), Field.Custom<T3>(startColumnIndex + 2), Field.Custom<T4>(startColumnIndex + 3), Field.Custom<T5>(startColumnIndex + 4), Field.Custom<T6>(startColumnIndex + 5), Field.Custom<T7>(startColumnIndex + 6), Field.Custom<T8>(startColumnIndex + 7));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7, T8)> GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(
    [CanBeNull] this DataTable dataTable,
    [NotNull, NotWhitespace] string startColumnName)
  {
    if (dataTable == null || dataTable.Rows.Count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7, T8)>) Array.Empty<(T1, T2, T3, T4, T5, T6, T7, T8)>();
    int fieldIndex = dataTable.GetFieldIndex(startColumnName);
    return dataTable.GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(Field.Custom<T1>(fieldIndex), Field.Custom<T2>(fieldIndex + 1), Field.Custom<T3>(fieldIndex + 2), Field.Custom<T4>(fieldIndex + 3), Field.Custom<T5>(fieldIndex + 4), Field.Custom<T6>(fieldIndex + 5), Field.Custom<T7>(fieldIndex + 6), Field.Custom<T8>(fieldIndex + 7));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7, T8)> GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(
    [CanBeNull] this DataTable dataTable,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4,
    Field<T5> field5,
    Field<T6> field6,
    Field<T7> field7,
    Field<T8> field8)
  {
    if (dataTable == null)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7, T8)>) Array.Empty<(T1, T2, T3, T4, T5, T6, T7, T8)>();
    int count = dataTable.Rows.Count;
    if (count == 0)
      return (IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7, T8)>) Array.Empty<(T1, T2, T3, T4, T5, T6, T7, T8)>();
    if (count > 1)
    {
      int fieldIndex;
      if (field1.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field1.FieldName, out fieldIndex))
        field1 = field1.WithIndex(fieldIndex);
      if (field2.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field2.FieldName, out fieldIndex))
        field2 = field2.WithIndex(fieldIndex);
      if (field3.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field3.FieldName, out fieldIndex))
        field3 = field3.WithIndex(fieldIndex);
      if (field4.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field4.FieldName, out fieldIndex))
        field4 = field4.WithIndex(fieldIndex);
      if (field5.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field5.FieldName, out fieldIndex))
        field5 = field5.WithIndex(fieldIndex);
      if (field6.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field6.FieldName, out fieldIndex))
        field6 = field6.WithIndex(fieldIndex);
      if (field7.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field7.FieldName, out fieldIndex))
        field7 = field7.WithIndex(fieldIndex);
      if (field8.FieldName != null && dataTable.TryConvertFieldNameToFieldIndex(field8.FieldName, out fieldIndex))
        field8 = field8.WithIndex(fieldIndex);
    }
    return dataTable.Rows.GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(in field1, in field2, in field3, in field4, in field5, in field6, in field7, in field8);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldName, defaultValue, formatProvider))).WrapWithCount<string>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldName, formatProvider))).WrapWithCount<string>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<long> FieldAsLongListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    long defaultValue = 0)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (dataRow => dataRow.FieldAsLongDef(fieldName, defaultValue))).WrapWithCount<long>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<int> FieldAsIntListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    int defaultValue = 0)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, int>((System.Func<DataRow, int>) (dataRow => dataRow.FieldAsIntDef(fieldName, defaultValue))).WrapWithCount<int>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<double> FieldAsDoubleDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    double defaultValue = 0.0)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, double>((System.Func<DataRow, double>) (dataRow => dataRow.FieldAsDoubleDef(fieldName, defaultValue))).WrapWithCount<double>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<bool> FieldAsBoolListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool defaultValue = false)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, bool>((System.Func<DataRow, bool>) (dataRow => dataRow.FieldAsBoolDef(fieldName, defaultValue))).WrapWithCount<bool>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldName, defaultValue, formatProvider))).WrapWithCount<DateTime>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldName, formatProvider))).WrapWithCount<DateTime>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<Guid> FieldAsGuidListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, Guid>((System.Func<DataRow, Guid>) (dataRow => dataRow.FieldAsGuidDef(fieldName, formatProvider))).WrapWithCount<Guid>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<byte[]> FieldAsBytesListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    [CanBeNull] byte[] defaultValue = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, byte[]>((System.Func<DataRow, byte[]>) (dataRow => dataRow.FieldAsBytesDef(fieldName, defaultValue))).WrapWithCount<byte[]>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<object> FieldAsObjectListDef(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    [CanBeNull] object defaultValue = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, object>((System.Func<DataRow, object>) (dataRow => dataRow.FieldAsObjectDef(fieldName, defaultValue))).WrapWithCount<object>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldIndex, defaultValue, formatProvider))).WrapWithCount<string>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldIndex, formatProvider))).WrapWithCount<string>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<long> FieldAsLongListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    long defaultValue = 0)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (dataRow => dataRow.FieldAsLongDef(fieldIndex, defaultValue))).WrapWithCount<long>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<int> FieldAsIntListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    int defaultValue = 0)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, int>((System.Func<DataRow, int>) (dataRow => dataRow.FieldAsIntDef(fieldIndex, defaultValue))).WrapWithCount<int>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<double> FieldAsDoubleListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    double defaultValue = 0.0)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, double>((System.Func<DataRow, double>) (dataRow => dataRow.FieldAsDoubleDef(fieldIndex, defaultValue))).WrapWithCount<double>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<bool> FieldAsBoolListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool defaultValue = false)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, bool>((System.Func<DataRow, bool>) (dataRow => dataRow.FieldAsBoolDef(fieldIndex, defaultValue))).WrapWithCount<bool>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldIndex, defaultValue, formatProvider))).WrapWithCount<DateTime>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldIndex, formatProvider))).WrapWithCount<DateTime>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<Guid> FieldAsGuidListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, Guid>((System.Func<DataRow, Guid>) (dataRow => dataRow.FieldAsGuidDef(fieldIndex, formatProvider))).WrapWithCount<Guid>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<byte[]> FieldAsBytesListDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    [CanBeNull] byte[] defaultValue = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, byte[]>((System.Func<DataRow, byte[]>) (dataRow => dataRow.FieldAsBytesDef(fieldIndex, defaultValue))).WrapWithCount<byte[]>(dataTable.Rows.Count);
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [NotNull]
  public static IReadOnlyCollection<object> FieldAsObjectDef(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    [CanBeNull] object defaultValue = null)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, object>((System.Func<DataRow, object>) (dataRow => dataRow.FieldAsObjectDef(fieldIndex, defaultValue))).WrapWithCount<object>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> SelectNotNull(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, object>((System.Func<DataRow, object>) (dataRow =>
    {
      object obj = dataRow[fieldName];
      switch (obj)
      {
        case null:
        case DBNull _:
          if (failOnNull)
            throw new FieldIsEmptyException(fieldName);
          return (object) null;
        default:
          return obj;
      }
    })).NotNull<object>();
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> SelectNotNull(
    [NotNull] this DataTable dataTable,
    int fieldIndex = 0,
    bool failOnNull = false)
  {
    return dataTable.Rows.Cast<DataRow>().Select<DataRow, object>((System.Func<DataRow, object>) (dataRow =>
    {
      object obj = dataRow[fieldIndex];
      switch (obj)
      {
        case null:
        case DBNull _:
          if (failOnNull)
            throw new FieldIsEmptyException(fieldIndex);
          return (object) null;
        default:
          return obj;
      }
    })).NotNull<object>();
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).Select<object, string>((System.Func<object, string>) (fieldValue => Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture))).WrapWithCount<string>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<long> FieldAsLongList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).Select<object, long>(new System.Func<object, long>(Convert.ToInt64)).WrapWithCount<long>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<int> FieldAsIntList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).Select<object, int>(new System.Func<object, int>(Convert.ToInt32)).WrapWithCount<int>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<double> FieldAsDoubleList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).Select<object, double>(new System.Func<object, double>(Convert.ToDouble)).WrapWithCount<double>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<bool> FieldAsBoolList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).Select<object, bool>(new System.Func<object, bool>(Convert.ToBoolean)).WrapWithCount<bool>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).Select<object, DateTime>((System.Func<object, DateTime>) (value => Convert.ToDateTime(value, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture))).WrapWithCount<DateTime>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<Guid> FieldAsGuidList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).Select<object, Guid>((System.Func<object, Guid>) (value =>
    {
      string input = Convert.ToString(value, formatProvider);
      Guid result;
      return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
    })).WrapWithCount<Guid>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<byte[]> FieldAsBytesList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).Select<object, byte[]>((System.Func<object, byte[]>) (value => (byte[]) value)).WrapWithCount<byte[]>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<object> FieldAsObjectList(
    [NotNull] this DataTable dataTable,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldName, failOnNull).WrapWithCount<object>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).Select<object, string>((System.Func<object, string>) (fieldValue => Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture))).WrapWithCount<string>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<long> FieldAsLongList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).Select<object, long>(new System.Func<object, long>(Convert.ToInt64)).WrapWithCount<long>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<int> FieldAsIntList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).Select<object, int>(new System.Func<object, int>(Convert.ToInt32)).WrapWithCount<int>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<double> FieldAsDoubleList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).Select<object, double>(new System.Func<object, double>(Convert.ToDouble)).WrapWithCount<double>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<bool> FieldAsBoolList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).Select<object, bool>(new System.Func<object, bool>(Convert.ToBoolean)).WrapWithCount<bool>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).Select<object, DateTime>((System.Func<object, DateTime>) (value => Convert.ToDateTime(value, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture))).WrapWithCount<DateTime>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<Guid> FieldAsGuidList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).Select<object, Guid>((System.Func<object, Guid>) (value =>
    {
      string input = Convert.ToString(value, formatProvider);
      Guid result;
      return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
    })).WrapWithCount<Guid>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<byte[]> FieldAsBytesList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).Select<object, byte[]>((System.Func<object, byte[]>) (value => (byte[]) value)).WrapWithCount<byte[]>(dataTable.Rows.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<object> FieldAsObjectList(
    [NotNull] this DataTable dataTable,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataTable.SelectNotNull(fieldIndex, failOnNull).WrapWithCount<object>(dataTable.Rows.Count);
  }

  [NotNull]
  [ItemNotNull]
  public static IReadOnlyList<DataRow> GetRows([NotNull] this DataTable dataTable)
  {
    return (IReadOnlyList<DataRow>) new DataRowsList(dataTable.Rows);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DataRow> Rows([NotNull] this DataTable dataTable)
  {
    return dataTable.Rows.Cast<DataRow>();
  }
}
