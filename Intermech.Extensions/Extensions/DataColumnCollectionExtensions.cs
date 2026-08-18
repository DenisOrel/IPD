// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DataColumnCollectionExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class DataColumnCollectionExtensions
{
  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> Select<T>(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull, InstantHandle] System.Func<DataColumn, T> selector)
  {
    return dataColumnCollection.Cast<DataColumn>().Select<DataColumn, T>(selector).WrapWithCount<T>(dataColumnCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> Select<T>(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull, InstantHandle] Func<DataColumn, int, T> selector)
  {
    return dataColumnCollection.Cast<DataColumn>().Select<DataColumn, T>(selector).WrapWithCount<T>(dataColumnCollection.Count);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DataColumn> Where(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull, InstantHandle] System.Func<DataColumn, bool> predicate)
  {
    return dataColumnCollection.Cast<DataColumn>().Where<DataColumn>(predicate).WrapWithCountOrCapacity<DataColumn>((IEnumerable) dataColumnCollection, false);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DataColumn> Where(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull, InstantHandle] Func<DataColumn, int, bool> predicate)
  {
    return dataColumnCollection.Cast<DataColumn>().Where<DataColumn>(predicate).WrapWithCountOrCapacity<DataColumn>((IEnumerable) dataColumnCollection, false);
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TAccumulate Aggregate<TAccumulate>(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [CanBeNull] TAccumulate seed,
    [NotNull, InstantHandle] Func<TAccumulate, DataColumn, TAccumulate> func)
  {
    return dataColumnCollection.Cast<DataColumn>().Aggregate<DataColumn, TAccumulate>(seed, func);
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult Aggregate<TAccumulate, TResult>(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [CanBeNull] TAccumulate seed,
    [NotNull, InstantHandle] Func<TAccumulate, DataColumn, TAccumulate> func,
    [NotNull, InstantHandle] System.Func<TAccumulate, TResult> resultSelector)
  {
    return dataColumnCollection.Cast<DataColumn>().Aggregate<DataColumn, TAccumulate, TResult>(seed, func, resultSelector);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool All(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull, InstantHandle] System.Func<DataColumn, bool> predicate)
  {
    return dataColumnCollection.Cast<DataColumn>().All<DataColumn>(predicate);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Any([NotNull] this DataColumnCollection dataColumnCollection)
  {
    return dataColumnCollection.Cast<DataColumn>().Any<DataColumn>();
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Any(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull, InstantHandle] System.Func<DataColumn, bool> predicate)
  {
    return dataColumnCollection.Cast<DataColumn>().Any<DataColumn>(predicate);
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataColumn First([NotNull] this DataColumnCollection dataColumnCollection)
  {
    return dataColumnCollection.Cast<DataColumn>().First<DataColumn>();
  }

  [DebuggerHidden]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataColumn First(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull, InstantHandle] System.Func<DataColumn, bool> predicate)
  {
    return dataColumnCollection.Cast<DataColumn>().First<DataColumn>(predicate);
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataColumn FirstOrDefault([NotNull] this DataColumnCollection dataColumnCollection)
  {
    return dataColumnCollection.Cast<DataColumn>().FirstOrDefault<DataColumn>();
  }

  [DebuggerHidden]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataColumn FirstOrDefault(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull, InstantHandle] System.Func<DataColumn, bool> predicate)
  {
    return dataColumnCollection.Cast<DataColumn>().FirstOrDefault<DataColumn>(predicate);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAll(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull] Action<DataColumn> handler)
  {
    dataColumnCollection.Cast<DataColumn>().InvokeForAll<DataColumn>(handler);
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAll(
    [NotNull] this DataColumnCollection dataColumnCollection,
    [NotNull] Action<int, DataColumn> handler)
  {
    dataColumnCollection.Cast<DataColumn>().InvokeForAll<DataColumn>(handler);
  }

  [NotNull]
  public static IReadOnlyCollection<string> GetNames([NotNull] this DataColumnCollection dataColumnCollection)
  {
    return dataColumnCollection.Cast<DataColumn>().Select<DataColumn, string>((System.Func<DataColumn, string>) (column => column.ColumnName)).WrapWithCount<string>(dataColumnCollection.Count);
  }

  [NotNull]
  public static IReadOnlyCollection<string> GetCaptions(
    [NotNull] this DataColumnCollection dataColumnCollection)
  {
    return dataColumnCollection.Cast<DataColumn>().Select<DataColumn, string>((System.Func<DataColumn, string>) (column => column.Caption)).WrapWithCount<string>(dataColumnCollection.Count);
  }
}
