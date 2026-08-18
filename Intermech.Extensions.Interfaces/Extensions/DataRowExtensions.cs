// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DataRowExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Data;
using Intermech.Diagnostics;
using System;
using System.Data;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class DataRowExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long FieldAsObjectID(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    bool throwExceptionIfUnknownObjectId = true)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        long int64 = Convert.ToInt64(obj);
        if (throwExceptionIfUnknownObjectId)
          Intermech.Check.ObjectIdNotEmpty(int64);
        return int64;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long FieldAsObjectIdOrUnknown(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    long defaultValue = 0)
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
  public static bool TryGetFieldAsObjectID(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    out long result,
    bool falseIfUnknownObjectId = true)
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
        return !falseIfUnknownObjectId || !Intermech.Check.ObjectIdIsEmpty(result);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int FieldAsObjectTypeID(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    bool throwExceptionIfUnknownObjectId = true)
  {
    object obj = dataRow[fieldIndex];
    switch (obj)
    {
      case null:
      case DBNull _:
        throw new FieldIsEmptyException(fieldIndex);
      default:
        int int32 = Convert.ToInt32(obj);
        if (throwExceptionIfUnknownObjectId)
          Intermech.Check.ObjectTypeIdNotEmpty(int32);
        return int32;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int FieldAsObjectTypeIdOrUnknown(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    int defaultValue = -1)
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
  public static bool TryGetFieldAsObjectTypeID(
    [NotNull] this DataRow dataRow,
    [ZeroOrPositiveNumber] int fieldIndex,
    out int result,
    bool falseIfUnknownObjectId = true)
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
        return !falseIfUnknownObjectId || !Intermech.Check.ObjectTypeIdIsEmpty(result);
    }
  }
}
