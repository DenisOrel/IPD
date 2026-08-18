// Decompiled with JetBrains decompiler
// Type: Intermech.Project.WorkTimeUnit
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class WorkTimeUnit
{
  [NotEmpty]
  public readonly long MeasureID;
  /// <summary>Количество часов в этой единице времени</summary>
  private double _hoursPerUnit = 1.0;
  [NotNull]
  public readonly string ShortName;
  [NotNull]
  [ItemNotNull]
  [NonSerialized]
  public List<string> Names = new List<string>();

  public WorkTimeUnit([NotEmpty] long measureID, [NotNull] string shortName)
  {
    this.MeasureID = measureID;
    this.ShortName = shortName;
    this.Names.Add(this.ShortName);
  }

  private void InitSchedule([CanBeNull] Schedule schedule)
  {
    if (schedule == null)
      schedule = Schedule.Standard;
    if (this.MeasureID == MeasureUnit.Minutes.ID)
      this._hoursPerUnit = 0.01666666753590107;
    else if (this.MeasureID == MeasureUnit.Hours.ID)
      this._hoursPerUnit = 1.0;
    else if (this.MeasureID == MeasureUnit.Days.ID)
      this._hoursPerUnit = schedule.DayDuration;
    else if (this.MeasureID == MeasureUnit.Weeks.ID)
    {
      this._hoursPerUnit = schedule.WeekDuration;
    }
    else
    {
      if (this.MeasureID != MeasureUnit.Months.ID)
        return;
      this._hoursPerUnit = schedule.MonthDuration;
    }
  }

  /// <summary>Сколько текущих единиц измерения в days днях</summary>
  public double Convert(double days, [NotNull] Schedule schedule)
  {
    this.InitSchedule(schedule);
    return days * schedule.DayDuration / this._hoursPerUnit;
  }

  /// <summary>Переводит количество в этой единице измерения в дни</summary>
  public double ToDays(double value, [NotNull] Schedule schedule)
  {
    this.InitSchedule(schedule);
    return value * this._hoursPerUnit / schedule.DayDuration;
  }

  public double ToHours(double value, [CanBeNull] Schedule schedule)
  {
    this.InitSchedule(schedule);
    return value * this._hoursPerUnit;
  }

  public override string ToString()
  {
    return this.Names.Count <= 1 ? this.ShortName : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Names[1]);
  }

  public override int GetHashCode() => this.MeasureID.GetHashCode();
}
