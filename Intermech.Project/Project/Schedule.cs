// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Schedule
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class Schedule : Entity, ISerializable
{
  [NotNull]
  private DateScheduleCollection _holidaySchedules = new DateScheduleCollection();
  [CanBeNull]
  internal static Schedule _Standard = (Schedule) null;
  [CanBeNull]
  [NonSerialized]
  private object _tag;
  [NotNull]
  private DayOfWeekScheduleCollection _weekdaySchedules = new DayOfWeekScheduleCollection();
  protected static bool _EnableCache = true;
  [NotNull]
  private readonly Dictionary<DateTime, DayTimeIntervalCollection> _intervalsCache = new Dictionary<DateTime, DayTimeIntervalCollection>();
  [CanBeNull]
  private ICalendar _calendar;
  internal const int MaxWorkTimeYears = 30;
  private const double WorkTimeToDateMode = 99999.0;

  public Schedule()
  {
  }

  [CanBeNull]
  public DayTimeIntervalCollection GetDayTimeIntervals(DateTime date)
  {
    date = date.Date;
    DayTimeIntervalCollection dayTimeIntervals;
    if (Schedule._EnableCache && this._intervalsCache.TryGetValue(date, out dayTimeIntervals))
      return dayTimeIntervals;
    DayTimeIntervalCollection intervals = this.GetIntervals(date);
    if (Schedule._EnableCache)
      this._intervalsCache.Add(date, intervals);
    return intervals;
  }

  public void ClearCache() => this._intervalsCache.Clear();

  [NotNull]
  protected internal virtual DayTimeIntervalCollection GetIntervals(DateTime date)
  {
    ICalendarDay dayByDate = this._calendar.GetDayByDate(date);
    DayTimeIntervalCollection intervals = new DayTimeIntervalCollection();
    if (dayByDate.DayType != DayType.Holiday)
    {
      foreach (IWorkTimePeriod workTimePeriod in (IEnumerable<IWorkTimePeriod>) dayByDate.WorkTimePeriods)
        intervals.Add(new TimeInterval(workTimePeriod.StartHours, workTimePeriod.StartMinutes, workTimePeriod.FinishHours, workTimePeriod.FinishMinutes));
    }
    return intervals;
  }

  private void HolidaySchedules_ListChanged([CanBeNull] object sender, [NotNull] ListChangedEventArgs e)
  {
    if (e.ListChangedType != ListChangedType.ItemAdded && e.ListChangedType != ListChangedType.ItemChanged && e.ListChangedType != ListChangedType.ItemDeleted && e.ListChangedType != ListChangedType.ItemMoved && e.ListChangedType != ListChangedType.Reset)
      return;
    this.OnPropertyChanged("HolidaySchedules");
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.WeekdaySchedules.ListChanged += new ListChangedEventHandler(this.WeekdaySchedules_ListChanged);
    this.HolidaySchedules.ListChanged += new ListChangedEventHandler(this.HolidaySchedules_ListChanged);
  }

  private void WeekdaySchedules_ListChanged([CanBeNull] object sender, [NotNull] ListChangedEventArgs e)
  {
    if (e.ListChangedType != ListChangedType.ItemAdded && e.ListChangedType != ListChangedType.ItemChanged && e.ListChangedType != ListChangedType.ItemDeleted && e.ListChangedType != ListChangedType.ItemMoved && e.ListChangedType != ListChangedType.Reset)
      return;
    this.OnPropertyChanged("WeekdaySchedules");
  }

  [NotNull]
  protected virtual DateScheduleCollection HolidaySchedules => this._holidaySchedules;

  [CanBeNull]
  public static Schedule Standard => Schedule._Standard;

  [CanBeNull]
  public virtual object Tag
  {
    get => this._tag;
    set
    {
      if (value == this.Tag)
        return;
      this.OnPropertyChanging(nameof (Tag));
      this._tag = value;
      this.OnPropertyChanged(nameof (Tag));
      this.OnPropertyChangeCompleted(nameof (Tag));
    }
  }

  [NotNull]
  protected virtual DayOfWeekScheduleCollection WeekdaySchedules => this._weekdaySchedules;

  public void Load([NotEmpty] long objectID, [CanBeNull] IUserSession session)
  {
    this._weekdaySchedules.Clear();
    this._holidaySchedules.Clear();
    this.ClearCache();
    this._calendar = Intermech.Extensions.Calendars.Get(session, objectID);
    if (this._calendar == null)
      return;
    this.DayDuration = this._calendar.HoursInDay;
    this.WeekDuration = this._calendar.HoursInWeek;
    this.MonthDuration = (double) this._calendar.DaysInMonth * this.DayDuration;
    foreach (IWeekDayInfo weekDay in (IEnumerable<IWeekDayInfo>) this._calendar.StandardWeek.WeekDays)
    {
      DayTimeIntervalCollection intervalCollection = new DayTimeIntervalCollection();
      if (weekDay.DayType != DayType.Holiday)
      {
        foreach (IWorkTimePeriod workTimePeriod in (IEnumerable<IWorkTimePeriod>) weekDay.WorkTimePeriods)
          intervalCollection.Add(new TimeInterval(workTimePeriod.StartHours, workTimePeriod.StartMinutes, workTimePeriod.FinishHours, workTimePeriod.FinishMinutes));
      }
      this._weekdaySchedules[weekDay.DayOfWeek] = intervalCollection;
    }
  }

  public virtual void Assign([NotNull] Schedule src)
  {
    this._calendar = src._calendar;
    this.DayDuration = src.DayDuration;
    this.WeekDuration = src.WeekDuration;
    this.MonthDuration = src.MonthDuration;
    this._weekdaySchedules = src.WeekdaySchedules;
    this._holidaySchedules = src.HolidaySchedules;
  }

  public bool IsNonWorkingTime(DateTime time)
  {
    return this._calendar.GetDayByDate(time).DayType == DayType.Holiday;
  }

  public long ObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (this._calendar ?? throw new NullReferenceException("_calendar")).CalendarID;
    }
  }

  public double DayDuration { get; set; }

  public double WeekDuration { get; set; }

  public double MonthDuration { get; set; }

  protected Schedule([NotNull] SerializationInfo info, StreamingContext context)
    : this()
  {
    Schedule schedule = ScheduleList.GetSchedule(info.GetInt64("cid"), (IUserSession) null);
    if (schedule == null)
      return;
    this.Assign(schedule);
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ICalendar calendar = this._calendar;
    long calendarId = calendar != null ? calendar.CalendarID : 0L;
    info.AddValue("cid", calendarId);
  }

  public override int GetHashCode() => this.ObjectID.GetHashCode();

  [NotNull]
  public virtual DateScheduleList GetWorkTime(DateTime start, DateTime finish)
  {
    DateScheduleList workTime = new DateScheduleList();
    if (start.Year < 30 || finish.Year - start.Year > 30)
      return workTime;
    double val2_1 = (double) start.Hour + (double) start.Minute / 60.0 + (double) start.Second / 3600.0;
    double val2_2 = 999999.0;
    for (DateTime date = start; date.Date <= finish.Date; date = date.AddDays(1.0))
    {
      DayTimeIntervalCollection dayTimeIntervals = this.GetDayTimeIntervals(date);
      if (dayTimeIntervals != null)
      {
        DateSchedule dateSchedule = (DateSchedule) null;
        if (finish.Date == date.Date)
        {
          val2_2 = (double) finish.Hour + (double) finish.Minute / 60.0 + (double) finish.Second / 3600.0;
          if (val2_2 > dayTimeIntervals.Finish)
            val2_2 = dayTimeIntervals.Finish;
        }
        foreach (TimeInterval timeInterval in (System.Collections.ObjectModel.Collection<TimeInterval>) dayTimeIntervals)
        {
          if (start.Date != date.Date)
            val2_1 = -1.0;
          if (val2_1 != -1.0 && start.Date == date.Date && val2_1 < timeInterval.Start)
            val2_1 = timeInterval.Start;
          double start1 = Math.Max(timeInterval.Start, val2_1);
          double num = Math.Min(timeInterval.Finish, val2_2);
          if (num - start1 > 0.0)
          {
            if (dateSchedule == null)
              dateSchedule = new DateSchedule(date);
            dateSchedule.TimeIntervalCollection.Add(new TimeInterval(start1, num - start1)
            {
              Ratio = timeInterval.Ratio
            });
          }
        }
        if (dateSchedule != null)
          workTime.Add(dateSchedule);
        if (finish.Date == date.Date)
          break;
      }
    }
    return workTime;
  }

  [NotNull]
  public virtual DateScheduleList GetWorkTime(DateTime start, double work)
  {
    return work <= 0.0 ? this._getBackwardWorkTimeList(start, -work) : this._getWorkTimeList(start, work);
  }

  [NotNull]
  private DateScheduleList _getWorkTimeList(DateTime start, double work)
  {
    DateTime maxDate = start.AddYears(30);
    return this._getWorkTimeList(start, maxDate, work);
  }

  [NotNull]
  private DateScheduleList _getWorkTimeList(DateTime start, DateTime maxDate, double work)
  {
    DateScheduleList workTimeList = new DateScheduleList();
    double num1 = (double) start.Hour + (double) start.Minute / 60.0 + (double) start.Second / 3600.0;
    for (DateTime date = start; date < maxDate; date = date.AddDays(1.0))
    {
      DayTimeIntervalCollection dayTimeIntervals = this.GetDayTimeIntervals(date);
      if (dayTimeIntervals != null)
      {
        DateSchedule dateSchedule = (DateSchedule) null;
        foreach (TimeInterval timeInterval in (System.Collections.ObjectModel.Collection<TimeInterval>) dayTimeIntervals)
        {
          double start1 = timeInterval.Start;
          if (start.Date == date.Date && num1 > start1)
            start1 = num1;
          double val2 = timeInterval.Finish - start1;
          if (val2 > 0.0)
          {
            if (dateSchedule == null)
              dateSchedule = new DateSchedule(date);
            double duration = Math.Min(work / timeInterval.Ratio, val2);
            if (work == 99999.0)
            {
              double num2 = (double) maxDate.Hour + (double) maxDate.Minute / 60.0 + (double) maxDate.Second / 3600.0;
              if (date.Date == maxDate.Date && num2 >= start1 && num2 < timeInterval.Finish)
              {
                work = 0.0;
                duration = timeInterval.Finish - num2;
              }
            }
            dateSchedule.TimeIntervalCollection.Add(new TimeInterval(start1, duration)
            {
              Ratio = timeInterval.Ratio
            });
            if (work != 99999.0)
              work -= duration * timeInterval.Ratio;
          }
          if (work <= 0.0)
            break;
        }
        if (dateSchedule != null)
          workTimeList.Add(dateSchedule);
      }
      if (work <= 0.0)
        break;
    }
    return workTimeList;
  }

  [NotNull]
  private DateScheduleList _getBackwardWorkTimeList(DateTime start, double work)
  {
    DateScheduleList backwardWorkTimeList = new DateScheduleList();
    if (start.Year < 30)
      return backwardWorkTimeList;
    DateTime dateTime = start.AddYears(-30);
    double val2_1 = (double) start.Hour + (double) start.Minute / 60.0 + (double) start.Second / 3600.0;
    for (DateTime date = start; date > dateTime; date = date.AddDays(-1.0))
    {
      DayTimeIntervalCollection dayTimeIntervals = this.GetDayTimeIntervals(date);
      if (dayTimeIntervals != null)
      {
        DateSchedule dateSchedule = (DateSchedule) null;
        for (int index = dayTimeIntervals.Count - 1; index >= 0; --index)
        {
          TimeInterval timeInterval = dayTimeIntervals[index];
          double start1 = timeInterval.Start;
          double finish = timeInterval.Finish;
          if (!(start.Date == date.Date) || val2_1 > start1)
          {
            double num1 = Math.Min(finish, val2_1) - start1;
            if (num1 > 0.0)
            {
              double val2_2 = num1;
              double num2 = val2_2 - Math.Abs(work) / timeInterval.Ratio;
              if (num2 > 0.0)
                start1 += num2;
              double duration = Math.Min(Math.Abs(work) / timeInterval.Ratio, val2_2);
              if (dateSchedule == null)
                dateSchedule = new DateSchedule(date);
              dateSchedule.TimeIntervalCollection.Insert(0, new TimeInterval(start1, duration)
              {
                Ratio = timeInterval.Ratio
              });
              work -= timeInterval.Ratio * duration;
            }
            if (work <= 0.0)
              break;
          }
        }
        val2_1 = 99999.0;
        if (dateSchedule != null)
          backwardWorkTimeList.Add(dateSchedule);
      }
      if (work <= 0.0)
        break;
    }
    return backwardWorkTimeList;
  }
}
