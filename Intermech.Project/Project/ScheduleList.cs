// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ScheduleList
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Project;

public class ScheduleList
{
  [NotNull]
  private static readonly Dictionary<long, Schedule> _scheduleList = new Dictionary<long, Schedule>();

  public static void ReloadSchedule(long objectID, [NotNull] IUserSession session)
  {
    Schedule schedule;
    if (ScheduleList._scheduleList.TryGetValue(objectID, out schedule))
      schedule.Load(objectID, session);
    MergedSchedule.ClearCachesBasedOnCalendar(objectID);
  }

  [NotNull]
  public static Schedule GetSchedule([CanBeEmpty] long objectID, [CanBeNull] IUserSession session)
  {
    if (objectID == 0L)
      objectID = Calendars.StandardCalendarID;
    Schedule schedule;
    if (!ScheduleList._scheduleList.TryGetValue(objectID, out schedule))
    {
      schedule = new Schedule();
      schedule.Load(objectID, session);
      ScheduleList._scheduleList.Add(objectID, schedule);
    }
    return schedule;
  }

  [NotNull]
  public static List<KeyValuePair<long, string>> GetAllSchedules([NotNull] IUserSession session)
  {
    DataTable dataTable = session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.Calendar).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -2,
      (object) -50
    }, 0L, (object) null, -1));
    if (dataTable == null || dataTable.Rows.Count == 0)
      return new List<KeyValuePair<long, string>>(0);
    List<KeyValuePair<long, string>> allSchedules = new List<KeyValuePair<long, string>>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      allSchedules.Add(new KeyValuePair<long, string>(Convert.ToInt64(row[0]), row[1]?.ToString() ?? string.Empty));
    return allSchedules;
  }
}
