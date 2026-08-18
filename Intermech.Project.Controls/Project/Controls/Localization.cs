// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.Localization
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project.Controls;

internal static class Localization
{
  [NotNull]
  internal static readonly ResourceManager Resources = new ResourceManager("Intermech.Project.Controls.Properties.Resources", Assembly.GetExecutingAssembly());

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Project.Localization.GetString(stringName);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Project.Localization.GetStringOrNull(stringName);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString([NotNull, NotWhitespace] string stringName, out string result, [NotNull] params object[] args)
  {
    return Localization.Resources.TryGetString(stringName, out result, args) || Intermech.Project.Localization.TryGetString(stringName, out result, args);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] CultureInfo cultureInfo, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Project.Localization.GetString(stringName);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, cultureInfo, args) ?? Intermech.Project.Localization.GetStringOrNull(stringName, cultureInfo);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    out string result,
    [NotNull] params object[] args)
  {
    return Localization.Resources.TryGetString(stringName, cultureInfo, out result, args) || Intermech.Project.Localization.TryGetString(stringName, cultureInfo, out result, args);
  }
}
