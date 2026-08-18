// Decompiled with JetBrains decompiler
// Type: Intermech.Project.MergedSchedule
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Project;

internal class MergedSchedule : Schedule
{
  [CanBeNull]
  private readonly Schedule _projectSchedule;
  [NotNull]
  private readonly List<Schedule> _schedules;
  [NotNull]
  private static readonly Dictionary<int, MergedSchedule> _scheduleCache = new Dictionary<int, MergedSchedule>();

  public MergedSchedule([CanBeNull] Schedule projectSchedule, [NotNull] List<Schedule> schedules)
  {
    if (projectSchedule != null)
    {
      this._projectSchedule = projectSchedule;
      this.DayDuration = projectSchedule.DayDuration;
      this.WeekDuration = projectSchedule.WeekDuration;
      this.MonthDuration = projectSchedule.MonthDuration;
    }
    this._schedules = schedules;
  }

  protected internal override DayTimeIntervalCollection GetIntervals(DateTime date)
  {
    DayTimeIntervalCollection src = (DayTimeIntervalCollection) null;
    bool flag = false;
    foreach (Schedule schedule in this._schedules)
    {
      DayTimeIntervalCollection dayTimeIntervals = schedule.GetDayTimeIntervals(date);
      if (src == null)
      {
        src = dayTimeIntervals;
      }
      else
      {
        if (Schedule._EnableCache && !flag)
        {
          src = new DayTimeIntervalCollection(src);
          flag = true;
        }
        src.Merge(dayTimeIntervals);
      }
    }
    return Intermech.Diagnostics.Check.Result.NotNull<DayTimeIntervalCollection>(src);
  }

  public override int GetHashCode()
  {
    int hashCode = this._projectSchedule != null ? this._projectSchedule.GetHashCode() : 0;
    foreach (Schedule schedule in this._schedules)
    {
      hashCode *= 17;
      if (schedule != null)
        hashCode += schedule.GetHashCode();
    }
    return hashCode;
  }

  public bool IsBasedOnCalendar(long calendarID)
  {
    return this._projectSchedule != null && this._projectSchedule.ObjectID == calendarID || this._schedules.Any<Schedule>((Func<Schedule, bool>) (s => s.ObjectID == calendarID));
  }

  [NotNull]
  internal static MergedSchedule Get([CanBeNull] Schedule projectSchedule, [NotNull] List<Schedule> schedules)
  {
    MergedSchedule mergedSchedule1 = new MergedSchedule(projectSchedule, schedules);
    if (Schedule._EnableCache)
    {
      MergedSchedule mergedSchedule2;
      if (!MergedSchedule._scheduleCache.TryGetValue(mergedSchedule1.GetHashCode(), out mergedSchedule2))
        MergedSchedule._scheduleCache.Add(mergedSchedule1.GetHashCode(), mergedSchedule1);
      else
        mergedSchedule1 = mergedSchedule2;
    }
    return mergedSchedule1;
  }

  internal static void ClearCachesBasedOnCalendar(long calendarID)
  {
    foreach (MergedSchedule mergedSchedule in MergedSchedule._scheduleCache.Values)
    {
      if (mergedSchedule.IsBasedOnCalendar(calendarID))
        mergedSchedule.ClearCache();
    }
  }

  public override DateScheduleList GetWorkTime(DateTime start, double work)
  {
    DateScheduleList workTime = new DateScheduleList();
    work /= (double) this._schedules.Count;
    workTime.AddRange(this._schedules.SelectMany<Schedule, DateSchedule>((Func<Schedule, IEnumerable<DateSchedule>>) (s => (IEnumerable<DateSchedule>) s.GetWorkTime(start, work))));
    return workTime;
  }
}
