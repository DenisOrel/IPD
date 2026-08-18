// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDataRecordExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Data;
using Intermech.Diagnostics;
using System;
using System.Data;
using System.Globalization;

#nullable disable
namespace Intermech.Extensions;

public static class IDataRecordExtensions
{
  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  public static string FieldAsStringDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRecord[fieldName].Invoke<object, string>((System.Func<object, string>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }));
  }

  [NotNull]
  public static string FieldAsStringDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRecord[fieldName].Invoke<object, string>((System.Func<object, string>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return string.Empty;
        default:
          return Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }));
  }

  public static long FieldAsLongDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    long defaultValue = 0)
  {
    return dataRecord[fieldName].Invoke<object, long>((System.Func<object, long>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToInt64(fieldValue);
      }
    }));
  }

  public static int FieldAsIntDef([NotNull] this IDataRecord dataRecord, [NotNull] string fieldName, int defaultValue = 0)
  {
    return dataRecord[fieldName].Invoke<object, int>((System.Func<object, int>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToInt32(fieldValue);
      }
    }));
  }

  public static double FieldAsDoubleDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    double defaultValue = 0.0)
  {
    return dataRecord[fieldName].Invoke<object, double>((System.Func<object, double>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToDouble(fieldValue);
      }
    }));
  }

  public static bool FieldAsBoolDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    bool defaultValue = false)
  {
    return dataRecord[fieldName].Invoke<object, bool>((System.Func<object, bool>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToBoolean(fieldValue);
      }
    }));
  }

  public static DateTime FieldAsDateTimeDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRecord[fieldName].Invoke<object, DateTime>((System.Func<object, DateTime>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToDateTime(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }));
  }

  public static DateTime FieldAsDateTimeDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRecord[fieldName].Invoke<object, DateTime>((System.Func<object, DateTime>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return DateTime.MinValue;
        default:
          return Convert.ToDateTime(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }));
  }

  public static Guid FieldAsGuidDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    string input = dataRecord[fieldName].Invoke<object, string>((System.Func<object, string>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return string.Empty;
        default:
          return Convert.ToString(fieldValue, formatProvider);
      }
    }));
    Guid result;
    return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
  }

  [ContractAnnotation("defaultValue:Null => CanBeNull; => NotNull")]
  [CanBeNull]
  public static byte[] FieldAsBytesDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] byte[] defaultValue = null)
  {
    return dataRecord[fieldName].Invoke<object, byte[]>((System.Func<object, byte[]>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return (byte[]) fieldValue;
      }
    }));
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  public static object FieldAsObjectDef(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] object defaultValue = null)
  {
    return dataRecord[fieldName].Invoke<object, object>((System.Func<object, object>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return fieldValue;
      }
    }));
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  public static string FieldAsStringDef(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] string defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRecord[fieldIndex].Invoke<object, string>((System.Func<object, string>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }));
  }

  [NotNull]
  public static string FieldAsStringDef(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRecord[fieldIndex].Invoke<object, string>((System.Func<object, string>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return string.Empty;
        default:
          return Convert.ToString(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }));
  }

  public static long FieldAsLongDef([NotNull] this IDataRecord dataRecord, int fieldIndex, long defaultValue = 0)
  {
    return dataRecord[fieldIndex].Invoke<object, long>((System.Func<object, long>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToInt64(fieldValue);
      }
    }));
  }

  public static int FieldAsIntDef([NotNull] this IDataRecord dataRecord, int fieldIndex, int defaultValue = 0)
  {
    return dataRecord[fieldIndex].Invoke<object, int>((System.Func<object, int>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToInt32(fieldValue);
      }
    }));
  }

  public static double FieldAsDoubleDef(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    double defaultValue = 0.0)
  {
    return dataRecord[fieldIndex].Invoke<object, double>((System.Func<object, double>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToDouble(fieldValue);
      }
    }));
  }

  public static bool FieldAsBoolDef([NotNull] this IDataRecord dataRecord, int fieldIndex, bool defaultValue = false)
  {
    return dataRecord[fieldIndex].Invoke<object, bool>((System.Func<object, bool>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToBoolean(fieldValue);
      }
    }));
  }

  public static DateTime FieldAsDateTimeDef(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    DateTime defaultValue,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRecord[fieldIndex].Invoke<object, DateTime>((System.Func<object, DateTime>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return Convert.ToDateTime(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }));
  }

  public static DateTime FieldAsDateTimeDef(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return dataRecord[fieldIndex].Invoke<object, DateTime>((System.Func<object, DateTime>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return DateTime.MinValue;
        default:
          return Convert.ToDateTime(fieldValue, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }));
  }

  public static Guid FieldAsGuidDef(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    string input = dataRecord[fieldIndex].Invoke<object, string>((System.Func<object, string>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return string.Empty;
        default:
          return Convert.ToString(fieldValue, formatProvider);
      }
    }));
    Guid result;
    return string.IsNullOrWhiteSpace(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
  }

  [CanBeNull]
  public static byte[] FieldAsBytesDef(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] byte[] defaultValue = null)
  {
    return dataRecord[fieldIndex].Invoke<object, byte[]>((System.Func<object, byte[]>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return (byte[]) fieldValue;
      }
    }));
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  public static object FieldAsObjectDef(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] object defaultValue = null)
  {
    return dataRecord[fieldIndex].Invoke<object, object>((System.Func<object, object>) (fieldValue =>
    {
      switch (fieldValue)
      {
        case null:
        case DBNull _:
          return defaultValue;
        default:
          return fieldValue;
      }
    }));
  }

  [NotNull]
  public static string FieldAsString(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRecord[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  public static long FieldAsLong([NotNull] this IDataRecord dataRecord, [NotNull] string fieldName)
  {
    object obj = dataRecord[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToInt64(obj);
    }
  }

  public static int FieldAsInt([NotNull] this IDataRecord dataRecord, [NotNull] string fieldName)
  {
    object obj = dataRecord[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToInt32(obj);
    }
  }

  public static double FieldAsDouble([NotNull] this IDataRecord dataRecord, [NotNull] string fieldName)
  {
    object obj = dataRecord[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToDouble(obj);
    }
  }

  public static bool FieldAsBool([NotNull] this IDataRecord dataRecord, [NotNull] string fieldName)
  {
    object obj = dataRecord[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToBoolean(obj);
    }
  }

  public static DateTime FieldAsDateTime(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRecord[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  public static Guid FieldAsGuid(
    [NotNull] this IDataRecord dataRecord,
    [NotNull] string fieldName,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRecord[fieldName];
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

  [NotNull]
  public static byte[] FieldAsBytes([NotNull] this IDataRecord dataRecord, [NotNull] string fieldName)
  {
    object obj = dataRecord[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return (byte[]) obj;
    }
  }

  [NotNull]
  public static object FieldAsObject([NotNull] this IDataRecord dataRecord, [NotNull] string fieldName)
  {
    object obj = dataRecord[fieldName];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldName);
      default:
        return obj;
    }
  }

  [NotNull]
  public static string FieldAsString(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRecord[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToString(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  public static long FieldAsLong([NotNull] this IDataRecord dataRecord, int fieldIndex)
  {
    object obj = dataRecord[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToInt64(obj);
    }
  }

  public static int FieldAsInt([NotNull] this IDataRecord dataRecord, int fieldIndex)
  {
    object obj = dataRecord[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToInt32(obj);
    }
  }

  public static double FieldAsDouble([NotNull] this IDataRecord dataRecord, int fieldIndex)
  {
    object obj = dataRecord[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToDouble(obj);
    }
  }

  public static bool FieldAsBool([NotNull] this IDataRecord dataRecord, int fieldIndex)
  {
    object obj = dataRecord[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToBoolean(obj);
    }
  }

  public static DateTime FieldAsDateTime(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRecord[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return Convert.ToDateTime(obj, formatProvider ?? (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  public static Guid FieldAsGuid(
    [NotNull] this IDataRecord dataRecord,
    int fieldIndex,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    object obj = dataRecord[fieldIndex];
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

  [NotNull]
  public static byte[] FieldAsBytes([NotNull] this IDataRecord dataRecord, int fieldIndex)
  {
    object obj = dataRecord[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return (byte[]) obj;
    }
  }

  [NotNull]
  public static object FieldAsObject([NotNull] this IDataRecord dataRecord, int fieldIndex)
  {
    object obj = dataRecord[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        return obj;
    }
  }
}
