// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.MatchExtensions
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

public static class MatchExtensions
{
  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetFirstGroupValue([NotNull] this Match source, [CanBeNull] string defaultValue = null)
  {
    return !source.Success || source.Groups.Count <= 1 ? defaultValue : source.Groups.Cast<Group>().Skip<Group>(1).FirstOrDefault<Group>((Func<Group, bool>) (group => group.Success))?.Value ?? defaultValue;
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<string> GetGroupValue([NotNull] this Match source)
  {
    if (!source.Success || source.Groups.Count <= 1)
      return new List<string>(0);
    List<string> groupValue = new List<string>(source.Groups.Count - 1);
    groupValue.AddRange(source.Groups.Cast<Group>().Skip<Group>(1).Where<Group>((Func<Group, bool>) (group => group.Success)).Select<Group, string>((Func<Group, string>) (group => group.Value)));
    return groupValue;
  }
}
