
// Type: Intermech.Calendars.Localization
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.Calendars
{
    internal static class Localization
    {
      [NotNull]
      internal static readonly ResourceManager Resources = new ResourceManager("Intermech.Calendars.Properties.Resources", Assembly.GetExecutingAssembly());

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
    }
}
