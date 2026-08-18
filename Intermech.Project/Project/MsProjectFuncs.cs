// Decompiled with JetBrains decompiler
// Type: Intermech.Project.MsProjectFuncs
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Project;

internal class MsProjectFuncs
{
  [NotNull]
  private static readonly Regex _durationRegex = new Regex("P((?<y>\\d+)Y(?<m>\\d+)M(?<d>\\d+)D)?T(?<h>\\d+)H(?<mm>\\d+)M(?<s>\\d+)S", RegexOptions.Compiled);
  [NotNull]
  public static readonly string ProcessNamePrefix = "<>\\";

  [NotNull]
  public static string DateTimeToStr(DateTime dt) => dt.ToString("yyyy-MM-ddTHH:mm:ss");

  public static DateTime StrToDateTime([NotNull, NotEmpty] string s)
  {
    return DateTime.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static double StringToHours([NotNull] string duration)
  {
    Match match = MsProjectFuncs._durationRegex.Match(duration);
    if (!match.Success)
      return 0.0;
    int result1;
    int.TryParse(Intermech.Diagnostics.Check.Optional.NotNull<Group>(match.Groups["d"]).Value, out result1);
    int result2;
    int.TryParse(Intermech.Diagnostics.Check.Optional.NotNull<Group>(match.Groups["h"]).Value, out result2);
    int result3;
    int.TryParse(Intermech.Diagnostics.Check.Optional.NotNull<Group>(match.Groups["mm"]).Value, out result3);
    int result4;
    int.TryParse(Intermech.Diagnostics.Check.Optional.NotNull<Group>(match.Groups["s"]).Value, out result4);
    return new TimeSpan(result1, result2, result3, result4).TotalHours;
  }

  [NotNull]
  public static string HoursToString(double hours)
  {
    int num1 = (int) hours;
    int num2 = (int) ((hours - (double) num1) * 60.0);
    int num3 = (int) ((hours - (double) num1) * 3600.0 - (double) (num2 * 60));
    return $"PT{num1}H{num2}M{num3}S";
  }

  [NotNull]
  public static string ProjectNameToString([NotNull] string name)
  {
    return MsProjectFuncs.ProcessNamePrefix + name;
  }

  [NotNull]
  public static string StringToProjectName([NotNull] string s)
  {
    return s.Replace(MsProjectFuncs.ProcessNamePrefix, string.Empty);
  }
}
