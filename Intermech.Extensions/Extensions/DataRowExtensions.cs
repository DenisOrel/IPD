// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DataRowExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Data;
using Intermech.Diagnostics;
using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class DataRowExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetValue<T>([NotNull] this DataRow dataRow, in Field<T> field)
  {
    return field.GetValue(dataRow);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetNotNullValue<T>([NotNull] this DataRow dataRow, in Field<T> field) where T : class
  {
    return field.GetValue(dataRow);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2) GetValues<T1, T2>(
    [NotNull] this DataRow dataRow,
    in Field<T1> field1,
    in Field<T2> field2)
  {
    return (field1.GetValue(dataRow), field2.GetValue(dataRow));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3) GetValues<T1, T2, T3>(
    [NotNull] this DataRow dataRow,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3)
  {
    return (field1.GetValue(dataRow), field2.GetValue(dataRow), field3.GetValue(dataRow));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4) GetValues<T1, T2, T3, T4>(
    [NotNull] this DataRow dataRow,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4)
  {
    return (field1.GetValue(dataRow), field2.GetValue(dataRow), field3.GetValue(dataRow), field4.GetValue(dataRow));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4, T5) GetValues<T1, T2, T3, T4, T5>(
    [NotNull] this DataRow dataRow,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4,
    in Field<T5> field5)
  {
    return (field1.GetValue(dataRow), field2.GetValue(dataRow), field3.GetValue(dataRow), field4.GetValue(dataRow), field5.GetValue(dataRow));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4, T5, T6) GetValues<T1, T2, T3, T4, T5, T6>(
    [NotNull] this DataRow dataRow,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4,
    in Field<T5> field5,
    in Field<T6> field6)
  {
    return (field1.GetValue(dataRow), field2.GetValue(dataRow), field3.GetValue(dataRow), field4.GetValue(dataRow), field5.GetValue(dataRow), field6.GetValue(dataRow));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4, T5, T6, T7) GetValues<T1, T2, T3, T4, T5, T6, T7>(
    [NotNull] this DataRow dataRow,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4,
    in Field<T5> field5,
    in Field<T6> field6,
    in Field<T7> field7)
  {
    return (field1.GetValue(dataRow), field2.GetValue(dataRow), field3.GetValue(dataRow), field4.GetValue(dataRow), field5.GetValue(dataRow), field6.GetValue(dataRow), field7.GetValue(dataRow));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static (T1, T2, T3, T4, T5, T6, T7, T8) GetValues<T1, T2, T3, T4, T5, T6, T7, T8>(
    [NotNull] this DataRow dataRow,
    in Field<T1> field1,
    in Field<T2> field2,
    in Field<T3> field3,
    in Field<T4> field4,
    in Field<T5> field5,
    in Field<T6> field6,
    in Field<T7> field7,
    in Field<T8> field8)
  {
    return (field1.GetValue(dataRow), field2.GetValue(dataRow), field3.GetValue(dataRow), field4.GetValue(dataRow), field5.GetValue(dataRow), field6.GetValue(dataRow), field7.GetValue(dataRow), field8.GetValue(dataRow));
  }

  [ContractAnnotation("value:null => null")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static T GetVal<T>([NotNull] object value)
  {
    return value is T obj ? obj : (T) Convert.ChangeType(value, typeof (T));
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string FieldAsStringDef(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string FieldAsStringDef(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return string.Empty;
      default:
        return Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long FieldAsLongDef([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, long defaultValue = 0)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToInt64(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int FieldAsIntDef([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, int defaultValue = 0)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToInt32(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TEnum FieldAsEnumDef<TEnum>(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    TEnum defaultValue = default (TEnum))
    where TEnum : struct, Enum
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        Type underlyingType = Enum.GetUnderlyingType(typeof (TEnum));
        if (underlyingType == typeof (int))
          return (TEnum) (System.ValueType) Convert.ToInt32(obj);
        if (underlyingType == typeof (long))
          return (TEnum) (System.ValueType) Convert.ToInt64(obj);
        throw new Exception($"Unsupported Enum underlying type {underlyingType}");
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double FieldAsDoubleDef(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    double defaultValue = 0.0)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToDouble(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool FieldAsBoolDef([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, bool defaultValue = false)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToBoolean(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime FieldAsDateTimeDef(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime FieldAsDateTimeDef(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        return DateTime.MinValue;
      default:
        return Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Guid FieldAsGuidDef(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldName];
    string empty;
    switch (obj)
    {
      case null:
      case DBNull _:
        empty = string.Empty;
        break;
      default:
        empty = Convert.ToString(obj, formatProvider);
        break;
    }
    string input = empty;
    Guid result;
    return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
  }

  [ContractAnnotation("defaultValue:Null => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static byte[] FieldAsBytesDef([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, [CanBeNull] byte[] defaultValue = null)
  {
    object obj = dataRow[fieldName];
    return obj is DBNull ? defaultValue : (byte[]) obj;
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object FieldAsObjectDef(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    [CanBeNull] object defaultValue = null)
  {
    object obj = dataRow[fieldName];
    return obj is DBNull ? defaultValue : obj;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string FieldAsNotNullStringDef(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [NotNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    return obj is DBNull ? defaultValue : Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture) ?? string.Empty;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string FieldAsNotNullString(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return string.Empty;
      default:
        return Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string FieldAsStringDef(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    return obj is DBNull ? defaultValue : Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string FieldAsStringDef(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return string.Empty;
      default:
        return Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture) ?? string.Empty;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long FieldAsLongDef([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, long defaultValue = 0)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToInt64(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int FieldAsIntDef([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, int defaultValue = 0)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToInt32(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TEnum FieldAsEnumDef<TEnum>(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    TEnum defaultValue)
    where TEnum : struct, Enum
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        Type underlyingType = Enum.GetUnderlyingType(typeof (TEnum));
        if (underlyingType == typeof (int))
          return (TEnum) (System.ValueType) Convert.ToInt32(obj);
        if (underlyingType == typeof (long))
          return (TEnum) (System.ValueType) Convert.ToInt64(obj);
        throw new Exception($"Unsupported Enum underlying type {underlyingType}");
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double FieldAsDoubleDef([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, double defaultValue = 0.0)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToDouble(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool FieldAsBoolDef([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, bool defaultValue = false)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToBoolean(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime FieldAsDateTimeDef(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime FieldAsDateTimeDef(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return DateTime.MinValue;
      default:
        return Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Guid FieldAsGuidDef(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    string empty;
    switch (obj)
    {
      case null:
      case DBNull _:
        empty = string.Empty;
        break;
      default:
        empty = Convert.ToString(obj, formatProvider);
        break;
    }
    string input = empty;
    Guid result;
    return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static byte[] FieldAsBytesDef([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, [CanBeNull] byte[] defaultValue = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return (byte[]) obj;
    }
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  public static object FieldAsObjectDef([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, [CanBeNull] object defaultValue = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      default:
        return obj;
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string FieldAsString(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsString(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    out string result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = (string) null;
      return false;
    }
    result = Convert.ToString(result1, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long FieldAsLong([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToInt64(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsLong([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, out long result)
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = 0L;
      return false;
    }
    result = Convert.ToInt64(result1);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int FieldAsInt([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToInt32(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsInt([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, out int result)
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = 0;
      return false;
    }
    result = Convert.ToInt32(result1);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TEnum FieldAsEnum<TEnum>([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName) where TEnum : struct, Enum
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        Type underlyingType = Enum.GetUnderlyingType(typeof (TEnum));
        if (underlyingType == typeof (int))
          return (TEnum) (System.ValueType) Convert.ToInt32(obj);
        if (underlyingType == typeof (long))
          return (TEnum) (System.ValueType) Convert.ToInt64(obj);
        throw new Exception($"Unsupported Enum underlying type {underlyingType}");
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsEnum<TEnum>(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    out TEnum result)
    where TEnum : struct, Enum
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = default (TEnum);
      return false;
    }
    Type underlyingType = Enum.GetUnderlyingType(typeof (TEnum));
    if (underlyingType == typeof (int))
    {
      int int32 = Convert.ToInt32(result1);
      result = (TEnum) (System.ValueType) int32;
      return true;
    }
    if (!(underlyingType == typeof (long)))
      throw new Exception($"Unsupported Enum underlying type {underlyingType}");
    long int64 = Convert.ToInt64(result1);
    result = (TEnum) (System.ValueType) int64;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double FieldAsDouble([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToDouble(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsDouble([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, out double result)
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = 0.0;
      return false;
    }
    result = Convert.ToDouble(result1);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool FieldAsBool([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToBoolean(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsBool([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, out bool result)
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = false;
      return false;
    }
    result = Convert.ToBoolean(result1);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime FieldAsDateTime(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsDateTime(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    out DateTime result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = new DateTime();
      return false;
    }
    result = Convert.ToDateTime(result1, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Guid FieldAsGuid(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        string input = Convert.ToString(obj, formatProvider);
        Guid result;
        return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsGuid(
    [NotNull] this DataRow dataRow,
    [NotNull, NotWhitespace] string fieldName,
    out Guid result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = Guid.Empty;
      return false;
    }
    string input = Convert.ToString(result1, formatProvider);
    if (!string.IsNullOrWhiteSpace(input))
      return Guid.TryParse(input, out result);
    result = Guid.Empty;
    return false;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static byte[] FieldAsBytes([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName)
  {
    object obj = dataRow[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return (byte[]) obj;
    }
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsBytes([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, out byte[] result)
  {
    object result1;
    if (!dataRow.TryGetFieldValue(fieldName, out result1))
    {
      result = (byte[]) null;
      return false;
    }
    result = (byte[]) result1;
    return true;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object GetFieldValue([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName)
  {
    object fieldValue = dataRow[fieldName];
    switch (fieldValue)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return fieldValue;
    }
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldValue([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, out object result)
  {
    result = dataRow[fieldIndex];
    if (result != null && !(result is DBNull))
      return true;
    result = (object) null;
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldValueAcceptNulls(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] out object result)
  {
    result = dataRow[fieldIndex];
    if (!(result is DBNull))
      return true;
    result = (object) null;
    return false;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldValue([NotNull] this DataRow dataRow, [NotNull, NotWhitespace] string fieldName, out object result)
  {
    int columnIndex = dataRow.Table.Columns.IndexOf(fieldName);
    if (columnIndex == -1)
    {
      result = (object) null;
      return false;
    }
    result = dataRow[columnIndex];
    if (result != null && !(result is DBNull))
      return true;
    result = (object) null;
    return false;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string FieldAsString(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsString(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    out string result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = (string) null;
        return false;
      default:
        result = Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture) ?? string.Empty;
        return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsNotNullString(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [NotNull] out string result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = string.Empty;
        return false;
      default:
        result = Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture) ?? string.Empty;
        return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long FieldAsLong([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToInt64(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsLong([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, out long result)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = 0L;
        return false;
      default:
        result = Convert.ToInt64(obj);
        return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int FieldAsInt([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToInt32(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsInt([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, out int result)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = 0;
        return false;
      default:
        result = Convert.ToInt32(obj);
        return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TEnum FieldAsEnum<TEnum>([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex) where TEnum : struct, Enum
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        Type underlyingType = Enum.GetUnderlyingType(typeof (TEnum));
        if (underlyingType == typeof (int))
          return (TEnum) (System.ValueType) Convert.ToInt32(obj);
        if (underlyingType == typeof (long))
          return (TEnum) (System.ValueType) Convert.ToInt64(obj);
        throw new Exception($"Unsupported Enum underlying type {underlyingType}");
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsEnum<TEnum>(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    out TEnum result)
    where TEnum : struct, Enum
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = default (TEnum);
        return false;
      default:
        Type underlyingType = Enum.GetUnderlyingType(typeof (TEnum));
        if (underlyingType == typeof (int))
        {
          int int32 = Convert.ToInt32(obj);
          result = (TEnum) (System.ValueType) int32;
          return true;
        }
        if (!(underlyingType == typeof (long)))
          throw new Exception($"Unsupported Enum underlying type {underlyingType}");
        long int64 = Convert.ToInt64(obj);
        result = (TEnum) (System.ValueType) int64;
        return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double FieldAsDouble([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToDouble(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsDouble([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, out double result)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = 0.0;
        return false;
      default:
        result = Convert.ToDouble(obj);
        return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool FieldAsBool([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToBoolean(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsBool([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex, out bool result)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = false;
        return false;
      default:
        result = Convert.ToBoolean(obj);
        return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime FieldAsDateTime(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsDateTime(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    out DateTime result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = DateTime.MinValue;
        return false;
      default:
        result = Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Guid FieldAsGuid(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        string input = Convert.ToString(obj, formatProvider);
        Guid result;
        return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsGuid(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    out Guid result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = Guid.Empty;
        return false;
      default:
        string input = Convert.ToString(obj, formatProvider);
        if (!string.IsNullOrWhiteSpace(input))
          return Guid.TryParse(input, out result);
        result = Guid.Empty;
        return false;
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static byte[] FieldAsBytes([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return (byte[]) obj;
    }
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFieldAsBytes(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    out byte[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        result = (byte[]) null;
        return false;
      default:
        result = (byte[]) obj;
        return true;
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object GetFieldValue([NotNull] this DataRow dataRow, [ZeroOrPositiveNumber] int fieldIndex)
  {
    object fieldValue = dataRow[fieldIndex];
    switch (fieldValue)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return fieldValue;
    }
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetGetFieldValue(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    out object result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    result = dataRow[fieldIndex];
    if (result != null && !(result is DBNull))
      return true;
    result = (object) null;
    return false;
  }
}
