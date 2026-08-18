// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DayOfWeekScheduleCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class DayOfWeekScheduleCollection : Collection<DayOfWeekSchedule>, ISerializable
{
  public DayOfWeekScheduleCollection()
  {
  }

  protected DayOfWeekScheduleCollection([NotNull] SerializationInfo info, StreamingContext context)
    : this()
  {
    this.EntityType = info.GetType("EntityType");
    this.AddRange((IEnumerable<DayOfWeekSchedule>) info.GetValue<DayOfWeekSchedule[]>("Items"));
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    string str = this.EntityType.Assembly.FullName;
    char ch = ',';
    int length = str.IndexOf(ch);
    if (length >= 0)
      str = str.Substring(0, length);
    info.AddValue("EntityType", (object) $"{this.EntityType.FullName}, {str}");
    DayOfWeekSchedule[] dayOfWeekScheduleArray = new DayOfWeekSchedule[this.Count];
    for (int index = 0; index < this.Count; ++index)
      dayOfWeekScheduleArray[index] = this[index];
    info.AddValue("Items", (object) dayOfWeekScheduleArray);
  }

  [CanBeNull]
  public virtual DayTimeIntervalCollection this[DayOfWeek dayOfWeek]
  {
    get
    {
      return this.Where<DayOfWeekSchedule>((Func<DayOfWeekSchedule, bool>) (schedule => schedule.DayOfWeek == dayOfWeek)).Select<DayOfWeekSchedule, DayTimeIntervalCollection>((Func<DayOfWeekSchedule, DayTimeIntervalCollection>) (schedule => schedule.TimeIntervalCollection)).FirstOrDefault<DayTimeIntervalCollection>();
    }
    set
    {
      foreach (DayOfWeekSchedule dayOfWeekSchedule in (System.Collections.ObjectModel.Collection<DayOfWeekSchedule>) this)
      {
        if (dayOfWeekSchedule.DayOfWeek == dayOfWeek)
        {
          if (value != null)
          {
            dayOfWeekSchedule.TimeIntervalCollection = value;
            return;
          }
          this.Remove(dayOfWeekSchedule);
          return;
        }
      }
      if (value == null)
        return;
      this.Add(new DayOfWeekSchedule(dayOfWeek, value));
    }
  }
}
