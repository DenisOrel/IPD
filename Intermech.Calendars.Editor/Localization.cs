
// Type: Intermech.Calendars.Editor.Localization
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.Calendars.Editor;

internal static class Localization
{
  [NotNull]
  internal static readonly ResourceManager Resources = new ResourceManager("Intermech.Calendars.Editor.Properties.Resources", Assembly.GetExecutingAssembly());

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Calendars.Localization.GetString(stringName);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Calendars.Localization.GetStringOrNull(stringName);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString([NotNull, NotWhitespace] string stringName, out string result, [NotNull] params object[] args)
  {
    return Localization.Resources.TryGetString(stringName, out result, args) || Intermech.Calendars.Localization.TryGetString(stringName, out result, args);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] CultureInfo cultureInfo, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Calendars.Localization.GetString(stringName);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, cultureInfo, args) ?? Intermech.Calendars.Localization.GetStringOrNull(stringName, cultureInfo);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    out string result,
    [NotNull] params object[] args)
  {
    return Localization.Resources.TryGetString(stringName, cultureInfo, out result, args) || Intermech.Calendars.Localization.TryGetString(stringName, cultureInfo, out result, args);
  }
}
