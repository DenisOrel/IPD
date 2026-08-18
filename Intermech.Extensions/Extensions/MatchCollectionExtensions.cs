// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.MatchCollectionExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Extensions;

public static class MatchCollectionExtensions
{
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TResult> Select<TResult>(
    [NotNull] this MatchCollection source,
    [NotNull] Func<Match, TResult> selector)
  {
    return source.OfType<Match>().Select<Match, TResult>(selector);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TResult> Select<TResult>(
    [NotNull] this MatchCollection source,
    [NotNull] Func<Match, int, TResult> selector)
  {
    return source.OfType<Match>().Select<Match, TResult>(selector);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<Match> Where([NotNull] this MatchCollection source, [NotNull] Func<Match, bool> predicate)
  {
    return source.OfType<Match>().Where<Match>(predicate);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<Match> Where(
    [NotNull] this MatchCollection source,
    [NotNull] Func<Match, int, bool> predicate)
  {
    return source.OfType<Match>().Where<Match>(predicate);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<string> GetFirstGroupValues([NotNull] this MatchCollection source)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<MatchCollection>(source, nameof (source));
    List<string> firstGroupValues = new List<string>(source.Count);
    firstGroupValues.AddRange(source.OfType<Match>().SelectNotNull<Match, string>((Func<Match, string>) (match => match.GetFirstGroupValue())));
    return firstGroupValues;
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<List<string>> GetGroupValuesList([NotNull] this MatchCollection source)
  {
    List<List<string>> groupValuesList = new List<List<string>>(source.Count);
    groupValuesList.AddRange(source.OfType<Match>().Select<Match, List<string>>((Func<Match, List<string>>) (match => match.GetGroupValue())));
    return groupValuesList;
  }
}
