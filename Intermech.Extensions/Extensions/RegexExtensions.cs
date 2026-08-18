// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.RegexExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Extensions;

public static class RegexExtensions
{
  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetFirstMatch([NotNull] this Regex source, [NotNull] string text, [CanBeNull] string defaultValue = null)
  {
    return source.Match(text).GetFirstGroupValue(defaultValue);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<string> GetFirstMatchGroups([NotNull] this Regex source, [NotNull] string text)
  {
    return source.Match(text).GetGroupValue();
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<string> GetMatches([NotNull] this Regex source, [NotNull] string text)
  {
    return source.Matches(text).GetFirstGroupValues();
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<List<string>> GetMatchesGroups([NotNull] this Regex source, [NotNull] string text)
  {
    return source.Matches(text).GetGroupValuesList();
  }
}
