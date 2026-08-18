// Decompiled with JetBrains decompiler
// Type: Intermech.Project.WorkTimeUnits
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Project;

public static class WorkTimeUnits
{
  [NotNull]
  public static readonly Dictionary<string, WorkTimeUnit> Units = new Dictionary<string, WorkTimeUnit>();
  [NotNull]
  private static readonly Regex _parseRegex = new Regex("^\\s*(\\+?[\\d\\.,\\s]+)(.*)", RegexOptions.Compiled | RegexOptions.Singleline);
  [NotNull]
  private static readonly Regex _parseRegexWithMinus = new Regex("^\\s*([+-]?[\\d\\.,\\s]+)(.*)", RegexOptions.Compiled | RegexOptions.Singleline);

  [CanBeNull]
  private static WorkTimeUnit AddMU([NotNull] IUserSession session, long mid, [NotNull, NotWhitespace] string synKey)
  {
    IDBObject iDbAttributable = session.GetObject(mid, false);
    if (iDbAttributable != null)
    {
      string lower = (iDbAttributable.AttributeByID(Intermech.Metadata.Attributes.ShortName.ID).AsString ?? string.Empty).ToLower();
      if (!WorkTimeUnits.Units.ContainsKey(lower))
      {
        WorkTimeUnit workTimeUnit = new WorkTimeUnit(mid, lower);
        string str1 = Localization.GetString(synKey, (object) false);
        char[] chArray = new char[1]{ ',' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (!workTimeUnit.Names.Contains(str2))
            workTimeUnit.Names.Add(str2);
        }
        WorkTimeUnits.Units.Add(lower, workTimeUnit);
        return workTimeUnit;
      }
    }
    return (WorkTimeUnit) null;
  }

  [CanBeNull]
  public static WorkTimeUnit Hours { get; private set; }

  [CanBeNull]
  public static WorkTimeUnit Days { get; private set; }

  public static void Init([NotNull] IUserSession session)
  {
    WorkTimeUnits.Units.Clear();
    WorkTimeUnits.AddMU(session, MeasureUnit.Minutes.ID, "TimeUnitM");
    WorkTimeUnits.Hours = WorkTimeUnits.AddMU(session, MeasureUnit.Hours.ID, "TimeUnitH");
    WorkTimeUnits.Days = WorkTimeUnits.AddMU(session, MeasureUnit.Days.ID, "TimeUnitD");
    WorkTimeUnits.AddMU(session, MeasureUnit.Weeks.ID, "TimeUnitW");
    WorkTimeUnits.AddMU(session, MeasureUnit.Months.ID, "TimeUnitMon");
  }

  /// <summary>Преобразует строковую длительность в WorkTimeValue</summary>
  /// <param name="s">Исходная строка</param>
  /// <param name="defaultUnit">Единица измерения по умолчанию</param>
  /// <param name="defaultValue">(Optional)</param>
  /// <param name="lagMode">(Optional) Режим запаздывания дополнительно разрешает отрицательные значения</param>
  [CanBeNull]
  public static WorkTimeValue Parse(
    [NotNull, CanBeEmpty] string s,
    [CanBeNull] WorkTimeUnit defaultUnit,
    double defaultValue = 1.0,
    bool lagMode = false)
  {
    s = s.Trim();
    bool estimation = s.EndsWith(IMProject.EstimationSymbol);
    if (estimation)
      s = s.Substring(0, s.Length - IMProject.EstimationSymbol.Length);
    if (s == string.Empty)
      return new WorkTimeValue(defaultValue, defaultUnit, estimation);
    Regex regex = WorkTimeUnits._parseRegex;
    if (lagMode)
      regex = WorkTimeUnits._parseRegexWithMinus;
    Match match = regex.Match(s);
    if (match.Success)
    {
      double num = double.Parse(match.Groups[1].Value.ToLower().Replace(" ", string.Empty).Replace(",", "."), (IFormatProvider) CultureInfo.InvariantCulture);
      string lower = match.Groups[2].Value.ToLower();
      foreach (WorkTimeUnit unit in WorkTimeUnits.Units.Values)
      {
        if (unit.Names.Contains(lower))
          return new WorkTimeValue(num, unit, estimation);
      }
      if (lower == string.Empty)
        return new WorkTimeValue(num, defaultUnit, estimation);
    }
    return (WorkTimeValue) null;
  }

  [CanBeNull]
  public static WorkTimeUnit GetByMeasureID(long measureID)
  {
    return WorkTimeUnits.Units.Values.FirstOrDefault<WorkTimeUnit>((Func<WorkTimeUnit, bool>) (workTimeUnit => workTimeUnit.MeasureID == measureID));
  }
}
