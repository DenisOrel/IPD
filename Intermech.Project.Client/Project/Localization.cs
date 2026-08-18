// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Localization
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

internal class Localization
{
  [NotNull]
  internal static readonly ResourceManager Resources = new ResourceManager("Intermech.Project.Client.Properties.Resources", Assembly.GetExecutingAssembly());

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Project.Controls.Localization.GetString(stringName);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull([NotNull, NotWhitespace] string stringName, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Project.Controls.Localization.GetStringOrNull(stringName);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString([NotNull, NotWhitespace] string stringName, out string result, [NotNull] params object[] args)
  {
    return Localization.Resources.TryGetString(stringName, out result, args) || Intermech.Project.Controls.Localization.TryGetString(stringName, out result, args);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetString([NotNull, NotWhitespace] string stringName, [NotNull] CultureInfo cultureInfo, [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, args) ?? Intermech.Project.Controls.Localization.GetString(stringName);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStringOrNull(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    [NotNull] params object[] args)
  {
    return Localization.Resources.GetStringOrNull(stringName, cultureInfo, args) ?? Intermech.Project.Controls.Localization.GetStringOrNull(stringName, cultureInfo);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetString(
    [NotNull, NotWhitespace] string stringName,
    [NotNull] CultureInfo cultureInfo,
    out string result,
    [NotNull] params object[] args)
  {
    return Localization.Resources.TryGetString(stringName, cultureInfo, out result, args) || Intermech.Project.Controls.Localization.TryGetString(stringName, cultureInfo, out result, args);
  }
}
