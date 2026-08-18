// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Localization
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

[LocalizationRequired]
internal class Localization
{
  [NotNull]
  internal static readonly ResourceManager Resources = new ResourceManager("Intermech.Extensions.Interfaces.Resources.Resources", Assembly.GetExecutingAssembly());
  [NotNull]
  internal static readonly ResourceManager AttributeResources = new ResourceManager("Intermech.Extensions.Interfaces.Resources.Attributes", Assembly.GetExecutingAssembly());

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
