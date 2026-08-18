// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.StringEnumerationExtension
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Extensions;

public static class StringEnumerationExtension
{
  [NotNull]
  [ItemNotNull]
  [ItemNotEmpty]
  public static IEnumerable<string> NotNullNotEmpty([NotNull] this IEnumerable<string> strings)
  {
    return strings.Where<string>((Func<string, bool>) (str => !string.IsNullOrEmpty(str)));
  }

  [NotNull]
  [ItemNotNull]
  [ItemNotWhitespace]
  public static IEnumerable<string> NotNullNotWhitespace([NotNull] this IEnumerable<string> strings)
  {
    return strings.Where<string>((Func<string, bool>) (str => !string.IsNullOrWhiteSpace(str)));
  }

  [NotNull]
  public static string CombineNotEmpty([NotNull] this IEnumerable<string> strings, char delimiter)
  {
    bool flag = true;
    StringBuilder stringBuilder = new StringBuilder(256 /*0x0100*/);
    foreach (string str in strings.NotNullNotEmpty())
    {
      if (!flag)
        stringBuilder.Append(delimiter);
      else
        flag = false;
      stringBuilder.Append(str);
    }
    return stringBuilder.ToString();
  }

  [NotNull]
  public static string CombineNotWhitespace([NotNull] this IEnumerable<string> strings, char delimiter)
  {
    bool flag = true;
    StringBuilder stringBuilder = new StringBuilder(256 /*0x0100*/);
    foreach (string str in strings.NotNullNotWhitespace())
    {
      if (!flag)
        stringBuilder.Append(delimiter);
      else
        flag = false;
      stringBuilder.Append(str);
    }
    return stringBuilder.ToString();
  }
}
