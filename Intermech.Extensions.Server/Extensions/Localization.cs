// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Localization
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

internal static class Localization
{
  [NotNull]
  internal static readonly ResourceManager Resources = new ResourceManager("Intermech.Extensions.Server.Resources.Resources", Assembly.GetExecutingAssembly());
  [NotNull]
  internal static readonly ResourceManager AttributeResources = new ResourceManager("Intermech.Extensions.Server.Resources.Attributes", Assembly.GetExecutingAssembly());

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.Resources.GetString(stringName, args);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString([NotNull, NotWhitespace] string stringName, out string result, [NotNull] params object[] args)
  {
    return Localization.Resources.TryGetString(stringName, out result, args);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] CultureInfo cultureInfo, [NotNull] params object[] args)
  {
    return Localization.Resources.GetString(stringName, cultureInfo, args);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, cultureInfo, args);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    out string result,
    [NotNull] params object[] args)
  {
    return Localization.Resources.TryGetString(stringName, cultureInfo, out result, args);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeString([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.AttributeResources.GetString(stringName, args);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeStringOrNull([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.AttributeResources.GetStringOrNull(stringName, args);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttributeString(
    [NotNull, NotWhitespace] string stringName,
    out string result,
    [NotNull] params object[] args)
  {
    return Localization.AttributeResources.TryGetString(stringName, out result, args);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeString(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    return Localization.AttributeResources.GetString(stringName, cultureInfo, args);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeStringOrNull(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    return Localization.AttributeResources.GetStringOrNull(stringName, cultureInfo, args);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttributeString(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    out string result,
    [NotNull] params object[] args)
  {
    return Localization.AttributeResources.TryGetString(stringName, cultureInfo, out result, args);
  }
}
