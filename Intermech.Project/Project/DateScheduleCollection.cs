// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DateScheduleCollection
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
public class DateScheduleCollection : Collection<DateSchedule>, ISerializable
{
  public DateScheduleCollection()
  {
  }

  protected DateScheduleCollection([NotNull] SerializationInfo info, StreamingContext context)
    : this()
  {
    this.EntityType = info.GetType("EntityType");
    this.AddRange((IEnumerable<DateSchedule>) info.GetValue<DateSchedule[]>("Items"));
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
    DateSchedule[] dateScheduleArray = new DateSchedule[this.Count];
    for (int index = 0; index < this.Count; ++index)
      dateScheduleArray[index] = this[index];
    info.AddValue("Items", (object) dateScheduleArray);
  }

  [CanBeNull]
  public virtual DayTimeIntervalCollection this[DateTime date]
  {
    get
    {
      return this.Where<DateSchedule>((Func<DateSchedule, bool>) (schedule => schedule.Date == date)).Select<DateSchedule, DayTimeIntervalCollection>((Func<DateSchedule, DayTimeIntervalCollection>) (schedule => schedule.TimeIntervalCollection)).FirstOrDefault<DayTimeIntervalCollection>();
    }
    set
    {
      date = date.Date;
      foreach (DateSchedule dateSchedule in (System.Collections.ObjectModel.Collection<DateSchedule>) this)
      {
        if (dateSchedule.Date == date)
        {
          if (value != null)
          {
            dateSchedule.TimeIntervalCollection = value;
            return;
          }
          this.Remove(dateSchedule);
          return;
        }
      }
      if (value == null)
        return;
      this.Add(new DateSchedule(date, value));
    }
  }
}
