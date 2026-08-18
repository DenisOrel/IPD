// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDBAttributeExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IDBAttributeExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<long> GetMultipleIntValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (iDbAttribute == null || iDbAttribute.IsNull || iDbAttribute.ValuesCount <= 0)
      return (IReadOnlyList<long>) Array.Empty<long>();
    object[] values = iDbAttribute.Values;
    return (values != null ? values.MapList<long>((Func<object, long>) (value => Convert.ToInt64(value, formatProvider))) : (IReadOnlyList<long>) null) ?? (IReadOnlyList<long>) Array.Empty<long>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleIntValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out IReadOnlyList<long> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleIntValues(formatProvider)).Count > 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<long> GetMultipleObjLinkValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return (IReadOnlyList<long>) Array.Empty<long>();
    object[] values = iDbAttribute.Values;
    return values == null || values.Length == 0 ? (IReadOnlyList<long>) Array.Empty<long>() : (IReadOnlyList<long>) ((IEnumerable<object>) values).Where<object>((Func<object, bool>) (value => value != null && !(value is DBNull))).Select<object, long>((Func<object, long>) (value => Convert.ToInt64(value, formatProvider))).Where<long>((Func<long, bool>) (value => value != 0L)).ToList<long>(values.Length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleObjLinkValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out IReadOnlyList<long> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleObjLinkValues(formatProvider)).Count > 0;
  }

  [NotNull]
  [ItemNotNull]
  [ItemCanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<string> GetMultipleStrValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (iDbAttribute == null || iDbAttribute.IsNull || iDbAttribute.ValuesCount <= 0)
      return (IReadOnlyList<string>) Array.Empty<string>();
    object[] values = iDbAttribute.Values;
    return (values != null ? values.MapList<string>((Func<object, string>) (value =>
    {
      switch (value)
      {
        case null:
        case DBNull _:
          return string.Empty;
        default:
          return Convert.ToString(value, formatProvider);
      }
    })) : (IReadOnlyList<string>) null) ?? (IReadOnlyList<string>) Array.Empty<string>();
  }

  [NotNull]
  [ItemNotNull]
  [ItemNotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<string> GetMultipleNotEmptyStrValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return (IReadOnlyList<string>) Array.Empty<string>();
    object[] values = iDbAttribute.Values;
    return values == null || values.Length == 0 ? (IReadOnlyList<string>) Array.Empty<string>() : (IReadOnlyList<string>) values.MapList<string>((Func<object, string>) (value =>
    {
      switch (value)
      {
        case null:
        case DBNull _:
          return (string) null;
        default:
          return Convert.ToString(value, formatProvider);
      }
    })).Where<string>((Func<string, bool>) (value => !string.IsNullOrEmpty(value))).ToList<string>(values.Length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleStrValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull, ItemNotNull, ItemCanBeEmpty] out IReadOnlyList<string> result,
    [CanBeNull] IFormatProvider formatProvider = null,
    bool onlyNonEmptyStrings = false)
  {
    return (result = onlyNonEmptyStrings ? iDbAttribute.GetMultipleNotEmptyStrValues(formatProvider) : iDbAttribute.GetMultipleStrValues(formatProvider)).Count > 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleStrValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull, ItemNotNull, ItemCanBeEmpty] out IReadOnlyList<string> result,
    bool onlyNonEmptyStrings,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = onlyNonEmptyStrings ? iDbAttribute.GetMultipleNotEmptyStrValues(formatProvider) : iDbAttribute.GetMultipleStrValues(formatProvider)).Count > 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<bool> GetMultipleBoolValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (iDbAttribute == null || iDbAttribute.IsNull || iDbAttribute.ValuesCount <= 0)
      return (IReadOnlyList<bool>) Array.Empty<bool>();
    object[] values = iDbAttribute.Values;
    return (values != null ? values.MapList<bool>((Func<object, bool>) (value => Convert.ToBoolean(value, formatProvider))) : (IReadOnlyList<bool>) null) ?? (IReadOnlyList<bool>) Array.Empty<bool>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleBoolValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out IReadOnlyList<bool> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleBoolValues(formatProvider)).Count > 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<DateTime> GetMultipleDateTimeValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (iDbAttribute == null || iDbAttribute.IsNull || iDbAttribute.ValuesCount <= 0)
      return (IReadOnlyList<DateTime>) Array.Empty<DateTime>();
    object[] values = iDbAttribute.Values;
    return (values != null ? values.MapList<DateTime>((Func<object, DateTime>) (value => Convert.ToDateTime(value, formatProvider))) : (IReadOnlyList<DateTime>) null) ?? (IReadOnlyList<DateTime>) Array.Empty<DateTime>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleDateTimeValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out IReadOnlyList<DateTime> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleDateTimeValues(formatProvider)).Count > 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<double> GetMultipleDoubleValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (iDbAttribute == null || iDbAttribute.IsNull || iDbAttribute.ValuesCount <= 0)
      return (IReadOnlyList<double>) Array.Empty<double>();
    object[] values = iDbAttribute.Values;
    return (values != null ? values.MapList<double>((Func<object, double>) (value => Convert.ToDouble(value, formatProvider))) : (IReadOnlyList<double>) null) ?? (IReadOnlyList<double>) Array.Empty<double>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleDoubleValues(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out IReadOnlyList<double> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleDoubleValues(formatProvider)).Count > 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long[] GetMultipleIntValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return Array.Empty<long>();
    object[] values = iDbAttribute.Values;
    if (values == null || values.Length == 0)
      Array.Empty<long>();
    return ((IEnumerable<object>) values).Where<object>((Func<object, bool>) (value => value != null && !(value is DBNull))).Select<object, long>((Func<object, long>) (value => Convert.ToInt64(value, formatProvider))).ToArray<long>(values.Length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleIntValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out long[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleIntValuesArray(formatProvider)).Length != 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long[] GetMultipleObjLinkValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return Array.Empty<long>();
    object[] values = iDbAttribute.Values;
    return values == null || values.Length == 0 ? Array.Empty<long>() : ((IEnumerable<object>) values).Where<object>((Func<object, bool>) (value => value != null && !(value is DBNull))).Select<object, long>((Func<object, long>) (value => Convert.ToInt64(value, formatProvider))).Where<long>((Func<long, bool>) (value => value != 0L)).ToArray<long>(values.Length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleObjLinkValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out long[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleObjLinkValuesArray(formatProvider)).Length != 0;
  }

  [NotNull]
  [ItemNotNull]
  [ItemCanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string[] GetMultipleStrValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return Array.Empty<string>();
    object[] values = iDbAttribute.Values;
    if (values == null || values.Length == 0)
      Array.Empty<string>();
    return ((IEnumerable<object>) values).Where<object>((Func<object, bool>) (value => value != null && !(value is DBNull))).Select<object, string>((Func<object, string>) (value => Convert.ToString(value, formatProvider))).ToArray<string>(values.Length);
  }

  [NotNull]
  [ItemNotNull]
  [ItemNotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string[] GetMultipleNotEmptyStrValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return Array.Empty<string>();
    object[] values = iDbAttribute.Values;
    if (values == null || values.Length == 0)
      Array.Empty<string>();
    return ((IEnumerable<object>) values).Select<object, string>((Func<object, string>) (value =>
    {
      switch (value)
      {
        case null:
        case DBNull _:
          return (string) null;
        default:
          return Convert.ToString(value, formatProvider);
      }
    })).Where<string>((Func<string, bool>) (value => !string.IsNullOrEmpty(value))).ToArray<string>(values.Length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleStrValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out string[] result,
    [CanBeNull] IFormatProvider formatProvider = null,
    bool onlyNonEmptyStrings = false)
  {
    return (result = onlyNonEmptyStrings ? iDbAttribute.GetMultipleNotEmptyStrValuesArray(formatProvider) : iDbAttribute.GetMultipleStrValuesArray(formatProvider)).Length != 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleStrValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out string[] result,
    bool onlyNonEmptyStrings,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = onlyNonEmptyStrings ? iDbAttribute.GetMultipleNotEmptyStrValuesArray(formatProvider) : iDbAttribute.GetMultipleStrValuesArray(formatProvider)).Length != 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool[] GetMultipleBoolValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return Array.Empty<bool>();
    object[] values = iDbAttribute.Values;
    if (values == null || values.Length == 0)
      Array.Empty<bool>();
    return ((IEnumerable<object>) values).Where<object>((Func<object, bool>) (value => value != null && !(value is DBNull))).Select<object, bool>((Func<object, bool>) (value => Convert.ToBoolean(value, formatProvider))).ToArray<bool>(values.Length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleBoolValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out bool[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleBoolValuesArray(formatProvider)).Length != 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime[] GetMultipleDateTimeValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return Array.Empty<DateTime>();
    object[] values = iDbAttribute.Values;
    if (values == null || values.Length == 0)
      Array.Empty<string>();
    return ((IEnumerable<object>) values).Where<object>((Func<object, bool>) (value => value != null && !(value is DBNull))).Select<object, DateTime>((Func<object, DateTime>) (value => Convert.ToDateTime(value, formatProvider))).ToArray<DateTime>(values.Length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleDateTimeValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out DateTime[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleDateTimeValuesArray(formatProvider)).Length != 0;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double[] GetMultipleDoubleValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if ((iDbAttribute != null ? (iDbAttribute.IsNull ? 1 : 0) : 1) != 0 || iDbAttribute.ValuesCount == 0)
      return Array.Empty<double>();
    object[] values = iDbAttribute.Values;
    if (values == null || values.Length == 0)
      Array.Empty<string>();
    return ((IEnumerable<object>) values).Where<object>((Func<object, bool>) (value => value != null && !(value is DBNull))).Select<object, double>((Func<object, double>) (value => Convert.ToDouble(value, formatProvider))).ToArray<double>(values.Length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAnyOfMultipleDoubleValuesArray(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] out double[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return (result = iDbAttribute.GetMultipleDoubleValuesArray(formatProvider)).Length != 0;
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static MeasuredValue GetAsMeasuredValueOrDefault(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] MeasureDescriptor defaultMeasure,
    [CanBeNull] MeasuredValue defaultValue = null)
  {
    if (iDbAttribute == null || iDbAttribute.IsNull)
      return defaultValue;
    if (iDbAttribute is IDBMeasureAttribute measureAttribute)
      return measureAttribute.Value;
    string asString = iDbAttribute.AsString;
    return string.IsNullOrWhiteSpace(asString) ? defaultValue : MeasureHelper.Instance.ConvertToMeasuredValue(asString, defaultMeasure, false) ?? defaultValue;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAsMeasuredValue(
    [CanBeNull] this IDBAttribute iDbAttribute,
    [NotNull] MeasureDescriptor defaultMeasure,
    out MeasuredValue result)
  {
    result = iDbAttribute.GetAsMeasuredValueOrDefault(defaultMeasure);
    return result != null;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void SetAsMeasuredValue(
    [NotNull] this IDBAttribute iDbAttribute,
    [CanBeNull] MeasuredValue newValue,
    bool autoDelAttrIfEmpty = false)
  {
    if (newValue == null)
    {
      if (autoDelAttrIfEmpty)
        iDbAttribute.Delete(0L);
      else
        iDbAttribute.Clear();
    }
    else if (iDbAttribute is IDBMeasureAttribute measureAttribute)
    {
      measureAttribute.Value = newValue;
    }
    else
    {
      string b = newValue.ToString();
      if (string.Equals(iDbAttribute.AsString, b, StringComparison.InvariantCulture))
        return;
      iDbAttribute.Value = (object) newValue.ToString();
    }
  }
}
