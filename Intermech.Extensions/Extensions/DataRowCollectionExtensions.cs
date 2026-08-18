// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DataRowCollectionExtensions
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

public static class DataRowCollectionExtensions
{
  [DebuggerHidden]
  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> Select<T>(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] System.Func<DataRow, T> selector)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, T>(selector).WrapWithCount<T>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> Select<T>(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] Func<DataRow, int, T> selector)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, T>(selector).WrapWithCount<T>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DataRow> Where(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] System.Func<DataRow, bool> predicate)
  {
    return dataRowCollection.Cast<DataRow>().Where<DataRow>(predicate).WrapWithCapacity<DataRow>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DataRow> Where(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] Func<DataRow, int, bool> predicate)
  {
    return dataRowCollection.Cast<DataRow>().Where<DataRow>(predicate).WrapWithCapacity<DataRow>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TAccumulate Aggregate<TAccumulate>(
    [NotNull] this DataRowCollection dataRowCollection,
    [CanBeNull] TAccumulate seed,
    [NotNull] Func<TAccumulate, DataRow, TAccumulate> func)
  {
    return dataRowCollection.Cast<DataRow>().Aggregate<DataRow, TAccumulate>(seed, func);
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Aggregate<TAccumulate, TResult>(
    [NotNull] this DataRowCollection dataRowCollection,
    [CanBeNull] TAccumulate seed,
    [NotNull] Func<TAccumulate, DataRow, TAccumulate> func,
    [NotNull] System.Func<TAccumulate, TResult> resultSelector)
  {
    return dataRowCollection.Cast<DataRow>().Aggregate<DataRow, TAccumulate, TResult>(seed, func, resultSelector);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool All([NotNull] this DataRowCollection dataRowCollection, [NotNull] System.Func<DataRow, bool> predicate)
  {
    return dataRowCollection.Cast<DataRow>().All<DataRow>(predicate);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Any([NotNull] this DataRowCollection dataRowCollection)
  {
    return dataRowCollection.Cast<DataRow>().Any<DataRow>();
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Any([NotNull] this DataRowCollection dataRowCollection, [NotNull] System.Func<DataRow, bool> predicate)
  {
    return dataRowCollection.Cast<DataRow>().Any<DataRow>(predicate);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataRow First([NotNull] this DataRowCollection dataRowCollection)
  {
    return dataRowCollection.Cast<DataRow>().First<DataRow>();
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataRow First(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] System.Func<DataRow, bool> predicate)
  {
    return dataRowCollection.Cast<DataRow>().First<DataRow>(predicate);
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataRow FirstOrDefault([NotNull] this DataRowCollection dataRowCollection)
  {
    return dataRowCollection.Cast<DataRow>().FirstOrDefault<DataRow>();
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataRow FirstOrDefault(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] System.Func<DataRow, bool> predicate)
  {
    return dataRowCollection.Cast<DataRow>().FirstOrDefault<DataRow>(predicate);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAll([NotNull] this DataRowCollection dataRowCollection, [NotNull] Action<DataRow> handler)
  {
    dataRowCollection.Cast<DataRow>().InvokeForAll(handler);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAll(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] Action<int, DataRow> handler)
  {
    dataRowCollection.Cast<DataRow>().InvokeForAll(handler);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> GetValues<T>(
    [NotNull] this DataRowCollection dataRowCollection,
    in Field<T> field)
  {
    return dataRowCollection.Cast<DataRow>().GetValues<T>(field).WrapWithCount<T>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2)> GetValues<T1, T2>(
    [NotNull] this DataRowCollection dataRowCollection,
    in Field<T1> field1,
    in Field<T2> field2)
  {
    return dataRowCollection.Cast<DataRow>().GetValues<T1, T2>(field1, field2).WrapWithCount<(T1, T2)>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3)> GetValues<T1, T2, T3>(
    [NotNull] this DataRowCollection dataRowCollection,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3)
  {
    return dataRowCollection.Cast<DataRow>().GetValues<T1, T2, T3>(field1, field2, field3).WrapWithCount<(T1, T2, T3)>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4)> GetValues<T1, T2, T3, T4>(
    [NotNull] this DataRowCollection dataRowCollection,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4)
  {
    return dataRowCollection.Cast<DataRow>().GetValues<T1, T2, T3, T4>(field1, field2, field3, field4).WrapWithCount<(T1, T2, T3, T4)>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5)> GetValues<T1, T2, T3, T4, T5>(
    [NotNull] this DataRowCollection dataRowCollection,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4,
    in Field<T5> field5)
  {
    return dataRowCollection.Cast<DataRow>().GetValues<T1, T2, T3, T4, T5>(field1, field2, field3, field4, field5).WrapWithCount<(T1, T2, T3, T4, T5)>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6)> GetValues<T1, T2, T3, T4, T5, T6>(
    [NotNull] this DataRowCollection dataRowCollection,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4,
    in Field<T5> field5,
    in Field<T6> field6)
  {
    return dataRowCollection.Cast<DataRow>().GetValues<T1, T2, T3, T4, T5, T6>(field1, field2, field3, field4, field5, field6).WrapWithCount<(T1, T2, T3, T4, T5, T6)>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7)> GetValues<T1, T2, T3, T4, T5, T6, T7>(
    [NotNull] this DataRowCollection dataRowCollection,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4,
    in Field<T5> field5,
    in Field<T6> field6,
    in Field<T7> field7)
  {
    return dataRowCollection.Cast<DataRow>().GetValues<T1, T2, T3, T4, T5, T6, T7>(field1, field2, field3, field4, field5, field6, field7).WrapWithCount<(T1, T2, T3, T4, T5, T6, T7)>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<(T1, T2, T3, T4, T5, T6, T7, T8)> GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(
    [NotNull] this DataRowCollection dataRowCollection,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4,
    in Field<T5> field5,
    in Field<T6> field6,
    in Field<T7> field7,
    in Field<T8> field8)
  {
    return dataRowCollection.Cast<DataRow>().GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(field1, field2, field3, field4, field5, field6, field7, field8).WrapWithCount<(T1, T2, T3, T4, T5, T6, T7, T8)>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldName, defaultValue, formatProvider))).WrapWithCount<string>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldName, formatProvider))).WrapWithCount<string>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<long> FieldAsLongListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    long defaultValue = 0)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (dataRow => dataRow.FieldAsLongDef(fieldName, defaultValue))).WrapWithCount<long>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<int> FieldAsIntListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    int defaultValue = 0)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, int>((System.Func<DataRow, int>) (dataRow => dataRow.FieldAsIntDef(fieldName, defaultValue))).WrapWithCount<int>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<double> FieldAsDoubleDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    double defaultValue = 0.0)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, double>((System.Func<DataRow, double>) (dataRow => dataRow.FieldAsDoubleDef(fieldName, defaultValue))).WrapWithCount<double>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<bool> FieldAsBoolListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool defaultValue = false)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, bool>((System.Func<DataRow, bool>) (dataRow => dataRow.FieldAsBoolDef(fieldName, defaultValue))).WrapWithCount<bool>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldName, defaultValue, formatProvider))).WrapWithCount<DateTime>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldName, formatProvider))).WrapWithCount<DateTime>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<Guid> FieldAsGuidListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, Guid>((System.Func<DataRow, Guid>) (dataRow => dataRow.FieldAsGuidDef(fieldName, formatProvider))).WrapWithCount<Guid>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<byte[]> FieldAsBytesListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    [CanBeNull] byte[] defaultValue = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, byte[]>((System.Func<DataRow, byte[]>) (dataRow => dataRow.FieldAsBytesDef(fieldName, defaultValue))).WrapWithCount<byte[]>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<object> FieldAsObjectListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    [CanBeNull] object defaultValue = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, object>((System.Func<DataRow, object>) (dataRow => dataRow.FieldAsObjectDef(fieldName, defaultValue))).WrapWithCount<object>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldIndex, defaultValue, formatProvider))).WrapWithCount<string>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldIndex, formatProvider))).WrapWithCount<string>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<long> FieldAsLongListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    long defaultValue = 0)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (dataRow => dataRow.FieldAsLongDef(fieldIndex, defaultValue))).WrapWithCount<long>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<int> FieldAsIntListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    int defaultValue = 0)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, int>((System.Func<DataRow, int>) (dataRow => dataRow.FieldAsIntDef(fieldIndex, defaultValue))).WrapWithCount<int>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<double> FieldAsDoubleListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    double defaultValue = 0.0)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, double>((System.Func<DataRow, double>) (dataRow => dataRow.FieldAsDoubleDef(fieldIndex, defaultValue))).WrapWithCount<double>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<bool> FieldAsBoolListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool defaultValue = false)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, bool>((System.Func<DataRow, bool>) (dataRow => dataRow.FieldAsBoolDef(fieldIndex, defaultValue))).WrapWithCount<bool>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldIndex, defaultValue, formatProvider))).WrapWithCount<DateTime>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldIndex, formatProvider))).WrapWithCount<DateTime>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<Guid> FieldAsGuidListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, Guid>((System.Func<DataRow, Guid>) (dataRow => dataRow.FieldAsGuidDef(fieldIndex, formatProvider))).WrapWithCount<Guid>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<byte[]> FieldAsBytesListDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    [CanBeNull] byte[] defaultValue = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, byte[]>((System.Func<DataRow, byte[]>) (dataRow => dataRow.FieldAsBytesDef(fieldIndex, defaultValue))).WrapWithCount<byte[]>(dataRowCollection.Count);
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [NotNull]
  public static IReadOnlyCollection<object> FieldAsObjectDef(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    [CanBeNull] object defaultValue = null)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, object>((System.Func<DataRow, object>) (dataRow => dataRow.FieldAsObjectDef(fieldIndex, defaultValue))).WrapWithCount<object>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> SelectNotNull(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, object>((System.Func<DataRow, object>) (dataRow =>
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
    })).NotNull<object>().WrapWithCapacity<object>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> SelectNotNull(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex = 0,
    bool failOnNull = false)
  {
    return dataRowCollection.Cast<DataRow>().Select<DataRow, object>((System.Func<DataRow, object>) (dataRow =>
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
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).Select<object, string>((System.Func<object, string>) (fieldValue => Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture))).WrapWithCount<string>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<long> FieldAsLongList(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).Select<object, long>(new System.Func<object, long>(Convert.ToInt64)).WrapWithCount<long>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<int> FieldAsIntList(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).Select<object, int>(new System.Func<object, int>(Convert.ToInt32)).WrapWithCount<int>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<double> FieldAsDoubleList(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).Select<object, double>(new System.Func<object, double>(Convert.ToDouble)).WrapWithCount<double>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<bool> FieldAsBoolList(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).Select<object, bool>(new System.Func<object, bool>(Convert.ToBoolean)).WrapWithCount<bool>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeList(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).Select<object, DateTime>((System.Func<object, DateTime>) (value => Convert.ToDateTime(value, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture))).WrapWithCount<DateTime>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<Guid> FieldAsGuidList(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).Select<object, Guid>((System.Func<object, Guid>) (value =>
    {
      string input = Convert.ToString(value, formatProvider);
      Guid result;
      return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
    })).WrapWithCount<Guid>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<byte[]> FieldAsBytesList(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).Select<object, byte[]>((System.Func<object, byte[]>) (value => (byte[]) value)).WrapWithCount<byte[]>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<object> FieldAsObjectList(
    [NotNull] this DataRowCollection dataRowCollection,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldName, failOnNull).WrapWithCount<object>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<string> FieldAsStringList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).Select<object, string>((System.Func<object, string>) (fieldValue => Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture))).WrapWithCount<string>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<long> FieldAsLongList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).Select<object, long>(new System.Func<object, long>(Convert.ToInt64)).WrapWithCount<long>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<int> FieldAsIntList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).Select<object, int>(new System.Func<object, int>(Convert.ToInt32)).WrapWithCount<int>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<double> FieldAsDoubleList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).Select<object, double>(new System.Func<object, double>(Convert.ToDouble)).WrapWithCount<double>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<bool> FieldAsBoolList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).Select<object, bool>(new System.Func<object, bool>(Convert.ToBoolean)).WrapWithCount<bool>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<DateTime> FieldAsDateTimeList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).Select<object, DateTime>((System.Func<object, DateTime>) (value => Convert.ToDateTime(value, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture))).WrapWithCount<DateTime>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<Guid> FieldAsGuidList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).Select<object, Guid>((System.Func<object, Guid>) (value =>
    {
      string input = Convert.ToString(value, formatProvider);
      Guid result;
      return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
    })).WrapWithCount<Guid>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<byte[]> FieldAsBytesList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).Select<object, byte[]>((System.Func<object, byte[]>) (value => (byte[]) value)).WrapWithCount<byte[]>(dataRowCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<object> FieldAsObjectList(
    [NotNull] this DataRowCollection dataRowCollection,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowCollection.SelectNotNull(fieldIndex, failOnNull).WrapWithCount<object>(dataRowCollection.Count);
  }
}
