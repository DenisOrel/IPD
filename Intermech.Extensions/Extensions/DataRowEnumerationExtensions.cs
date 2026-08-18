// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DataRowEnumerationExtensions
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

public static class DataRowEnumerationExtensions
{
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAll(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] Action<DataRow> handler)
  {
    foreach (DataRow dataRow in dataRowEnumeration)
      handler(dataRow);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAll(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] Action<int, DataRow> handler)
  {
    int num = 0;
    foreach (DataRow dataRow in dataRowEnumeration)
      handler(num++, dataRow);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> GetValues<T>(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    Field<T> field)
  {
    return dataRowEnumeration.Select<DataRow, T>((System.Func<DataRow, T>) (dataRow => dataRow.GetValue<T>(in field)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(T1, T2)> GetValues<T1, T2>(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    Field<T1> field1,
    Field<T2> field2)
  {
    return dataRowEnumeration.Select<DataRow, (T1, T2)>((System.Func<DataRow, (T1, T2)>) (dataRow => dataRow.GetValues<T1, T2>(in field1, in field2)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(T1, T2, T3)> GetValues<T1, T2, T3>(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3)
  {
    return dataRowEnumeration.Select<DataRow, (T1, T2, T3)>((System.Func<DataRow, (T1, T2, T3)>) (dataRow => dataRow.GetValues<T1, T2, T3>(in field1, in field2, in field3)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(T1, T2, T3, T4)> GetValues<T1, T2, T3, T4>(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4)
  {
    return dataRowEnumeration.Select<DataRow, (T1, T2, T3, T4)>((System.Func<DataRow, (T1, T2, T3, T4)>) (dataRow => dataRow.GetValues<T1, T2, T3, T4>(in field1, in field2, in field3, in field4)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(T1, T2, T3, T4, T5)> GetValues<T1, T2, T3, T4, T5>(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4,
    Field<T5> field5)
  {
    return dataRowEnumeration.Select<DataRow, (T1, T2, T3, T4, T5)>((System.Func<DataRow, (T1, T2, T3, T4, T5)>) (dataRow => dataRow.GetValues<T1, T2, T3, T4, T5>(in field1, in field2, in field3, in field4, in field5)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(T1, T2, T3, T4, T5, T6)> GetValues<T1, T2, T3, T4, T5, T6>(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4,
    Field<T5> field5,
    Field<T6> field6)
  {
    return dataRowEnumeration.Select<DataRow, (T1, T2, T3, T4, T5, T6)>((System.Func<DataRow, (T1, T2, T3, T4, T5, T6)>) (dataRow => dataRow.GetValues<T1, T2, T3, T4, T5, T6>(in field1, in field2, in field3, in field4, in field5, in field6)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> GetValues<T1, T2, T3, T4, T5, T6, T7>(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4,
    Field<T5> field5,
    Field<T6> field6,
    Field<T7> field7)
  {
    return dataRowEnumeration.Select<DataRow, (T1, T2, T3, T4, T5, T6, T7)>((System.Func<DataRow, (T1, T2, T3, T4, T5, T6, T7)>) (dataRow => dataRow.GetValues<T1, T2, T3, T4, T5, T6, T7>(in field1, in field2, in field3, in field4, in field5, in field6, in field7)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    Field<T1> field1,
    Field<T2> field2,
    Field<T3> field3,
    Field<T4> field4,
    Field<T5> field5,
    Field<T6> field6,
    Field<T7> field7,
    Field<T8> field8)
  {
    return dataRowEnumeration.Select<DataRow, (T1, T2, T3, T4, T5, T6, T7, T8)>((System.Func<DataRow, (T1, T2, T3, T4, T5, T6, T7, T8)>) (dataRow => dataRow.GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(in field1, in field2, in field3, in field4, in field5, in field6, in field7, in field8)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<string> FieldAsStringEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldName, defaultValue, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<string> FieldAsStringEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldName, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> FieldAsLongEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    long defaultValue = 0)
  {
    return dataRowEnumeration.Select<DataRow, long>((System.Func<DataRow, long>) (dataRow => dataRow.FieldAsLongDef(fieldName, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> FieldAsIntEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    int defaultValue = 0)
  {
    return dataRowEnumeration.Select<DataRow, int>((System.Func<DataRow, int>) (dataRow => dataRow.FieldAsIntDef(fieldName, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<double> FieldAsDoubleDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    double defaultValue = 0.0)
  {
    return dataRowEnumeration.Select<DataRow, double>((System.Func<DataRow, double>) (dataRow => dataRow.FieldAsDoubleDef(fieldName, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<bool> FieldAsBoolEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool defaultValue = false)
  {
    return dataRowEnumeration.Select<DataRow, bool>((System.Func<DataRow, bool>) (dataRow => dataRow.FieldAsBoolDef(fieldName, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DateTime> FieldAsDateTimeEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldName, defaultValue, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DateTime> FieldAsDateTimeEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldName, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<Guid> FieldAsGuidEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, Guid>((System.Func<DataRow, Guid>) (dataRow => dataRow.FieldAsGuidDef(fieldName, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<byte[]> FieldAsBytesEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    [CanBeNull] byte[] defaultValue = null)
  {
    return dataRowEnumeration.Select<DataRow, byte[]>((System.Func<DataRow, byte[]>) (dataRow => dataRow.FieldAsBytesDef(fieldName, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> FieldAsObjectEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    [CanBeNull] object defaultValue = null)
  {
    return dataRowEnumeration.Select<DataRow, object>((System.Func<DataRow, object>) (dataRow => dataRow.FieldAsObjectDef(fieldName, defaultValue)));
  }

  [DebuggerHidden]
  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<string> FieldAsStringEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldIndex, defaultValue, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<string> FieldAsStringEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, string>((System.Func<DataRow, string>) (dataRow => dataRow.FieldAsStringDef(fieldIndex, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> FieldAsLongEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    long defaultValue = 0)
  {
    return dataRowEnumeration.Select<DataRow, long>((System.Func<DataRow, long>) (dataRow => dataRow.FieldAsLongDef(fieldIndex, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> FieldAsIntEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    int defaultValue = 0)
  {
    return dataRowEnumeration.Select<DataRow, int>((System.Func<DataRow, int>) (dataRow => dataRow.FieldAsIntDef(fieldIndex, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<double> FieldAsDoubleEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    double defaultValue = 0.0)
  {
    return dataRowEnumeration.Select<DataRow, double>((System.Func<DataRow, double>) (dataRow => dataRow.FieldAsDoubleDef(fieldIndex, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<bool> FieldAsBoolEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool defaultValue = false)
  {
    return dataRowEnumeration.Select<DataRow, bool>((System.Func<DataRow, bool>) (dataRow => dataRow.FieldAsBoolDef(fieldIndex, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DateTime> FieldAsDateTimeEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldIndex, defaultValue, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DateTime> FieldAsDateTimeEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, DateTime>((System.Func<DataRow, DateTime>) (dataRow => dataRow.FieldAsDateTimeDef(fieldIndex, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<Guid> FieldAsGuidEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.Select<DataRow, Guid>((System.Func<DataRow, Guid>) (dataRow => dataRow.FieldAsGuidDef(fieldIndex, formatProvider)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<byte[]> FieldAsBytesEnumerationDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    [CanBeNull] byte[] defaultValue = null)
  {
    return dataRowEnumeration.Select<DataRow, byte[]>((System.Func<DataRow, byte[]>) (dataRow => dataRow.FieldAsBytesDef(fieldIndex, defaultValue)));
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [NotNull]
  public static IEnumerable<object> FieldAsObjectDef(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    [CanBeNull] object defaultValue = null)
  {
    return dataRowEnumeration.Select<DataRow, object>((System.Func<DataRow, object>) (dataRow => dataRow.FieldAsObjectDef(fieldIndex, defaultValue)));
  }

  [DebuggerHidden]
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> SelectNotNull(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowEnumeration.Select<DataRow, object>((System.Func<DataRow, object>) (dataRow =>
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
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> SelectNotNull(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex = 0,
    bool failOnNull = false)
  {
    return dataRowEnumeration.Select<DataRow, object>((System.Func<DataRow, object>) (dataRow =>
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
  public static IEnumerable<string> FieldAsStringEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull).Select<object, string>((System.Func<object, string>) (fieldValue => Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> FieldAsLongEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull).Select<object, long>(new System.Func<object, long>(Convert.ToInt64));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> FieldAsIntEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull).Select<object, int>(new System.Func<object, int>(Convert.ToInt32));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<double> FieldAsDoubleEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull).Select<object, double>(new System.Func<object, double>(Convert.ToDouble));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<bool> FieldAsBoolEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull).Select<object, bool>(new System.Func<object, bool>(Convert.ToBoolean));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DateTime> FieldAsDateTimeEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull).Select<object, DateTime>((System.Func<object, DateTime>) (value => Convert.ToDateTime(value, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<Guid> FieldAsGuidEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull).Select<object, Guid>((System.Func<object, Guid>) (value =>
    {
      string input = Convert.ToString(value, formatProvider);
      Guid result;
      return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
    }));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<byte[]> FieldAsBytesEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull).Select<object, byte[]>((System.Func<object, byte[]>) (value => (byte[]) value));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> FieldAsObjectEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    [NotNull] string fieldName,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldName, failOnNull);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<string> FieldAsStringEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull).Select<object, string>((System.Func<object, string>) (fieldValue => Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> FieldAsLongEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull).Select<object, long>(new System.Func<object, long>(Convert.ToInt64));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> FieldAsIntEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull).Select<object, int>(new System.Func<object, int>(Convert.ToInt32));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<double> FieldAsDoubleEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull).Select<object, double>(new System.Func<object, double>(Convert.ToDouble));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<bool> FieldAsBoolEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull).Select<object, bool>(new System.Func<object, bool>(Convert.ToBoolean));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DateTime> FieldAsDateTimeEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull).Select<object, DateTime>((System.Func<object, DateTime>) (value => Convert.ToDateTime(value, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture)));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<Guid> FieldAsGuidEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull).Select<object, Guid>((System.Func<object, Guid>) (value =>
    {
      string input = Convert.ToString(value, formatProvider);
      Guid result;
      return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
    }));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<byte[]> FieldAsBytesEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull).Select<object, byte[]>((System.Func<object, byte[]>) (value => (byte[]) value));
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> FieldAsObjectEnumeration(
    [NotNull] this IEnumerable<DataRow> dataRowEnumeration,
    int fieldIndex,
    bool failOnNull = false)
  {
    return dataRowEnumeration.SelectNotNull(fieldIndex, failOnNull);
  }
}
