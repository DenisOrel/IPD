// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ResourceManagerExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ResourceManagerExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString(
    [NotNull] this ResourceManager resources,
    [NotNull, NotWhitespace] string stringName,
    [NotNull] params object[] args)
  {
    string format = resources.GetString(stringName);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format(format, args) : format;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull(
    [NotNull] this ResourceManager resources,
    [NotNull, NotWhitespace] string stringName,
    [NotNull] params object[] args)
  {
    string format = resources.GetString(stringName);
    if (format == null)
      return (string) null;
    return args.Length != 0 ? string.Format(format, args) : format;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString(
    [NotNull] this ResourceManager resources,
    [NotNull, NotWhitespace] string stringName,
    out string result,
    [NotNull] params object[] args)
  {
    result = resources.GetString(stringName);
    if (result == null)
      return false;
    if (args.Length != 0)
      result = string.Format(result, args);
    return true;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString(
    [NotNull] this ResourceManager resources,
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    string format = resources.GetString(stringName, cultureInfo);
    if (format == null)
      return stringName;
    return args.Length != 0 ? string.Format((IFormatProvider) cultureInfo, format, args) : format;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull(
    [NotNull] this ResourceManager resources,
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    string format = resources.GetString(stringName, cultureInfo);
    if (format == null)
      return (string) null;
    return args.Length != 0 ? string.Format((IFormatProvider) cultureInfo, format, args) : format;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString(
    [NotNull] this ResourceManager resources,
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    out string result,
    [NotNull] params object[] args)
  {
    result = resources.GetString(stringName, cultureInfo);
    if (result == null)
      return false;
    if (args.Length != 0)
      result = string.Format((IFormatProvider) cultureInfo, result, args);
    return true;
  }
}
