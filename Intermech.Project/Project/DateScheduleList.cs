// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DateScheduleList
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
public class DateScheduleList : List<DateSchedule>, ISerializable
{
  public DateScheduleList()
  {
  }

  public DateScheduleList([NotNull] IEnumerable<DateSchedule> dateSchedules)
    : base(dateSchedules)
  {
  }

  public DateScheduleList(int capacity, [CanBeNull] IEnumerable<DateSchedule> dateSchedules = null)
    : base(capacity)
  {
    if (dateSchedules == null)
      return;
    this.AddRange(dateSchedules);
  }

  protected DateScheduleList([NotNull] SerializationInfo info, StreamingContext context)
    : this()
  {
    this.AddRange((IEnumerable<DateSchedule>) info.GetValue<DateSchedule[]>("Items"));
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    DateSchedule[] dateScheduleArray = new DateSchedule[this.Count];
    for (int index = 0; index < this.Count; ++index)
      dateScheduleArray[index] = this[index];
    info.AddValue("Items", (object) dateScheduleArray);
  }

  [CanBeNull]
  public DateSchedule GetByDate(DateTime date)
  {
    return this.FirstOrDefault<DateSchedule>((Func<DateSchedule, bool>) (ds => ds.Date == date.Date));
  }

  public void Assign([NotNull, ItemNotNull] DateScheduleList src)
  {
    this.Clear();
    foreach (DateSchedule dateSchedule in (List<DateSchedule>) src)
      this.Add(dateSchedule.Clone());
  }

  /// <summary>Склеивает все интервалы за одни и те же даты, убирает пересечения</summary>
  /// <returns>A DateScheduleList</returns>
  [NotNull]
  public DateScheduleList MergedList()
  {
    DateScheduleList dateScheduleList = new DateScheduleList();
    foreach (DateSchedule dateSchedule1 in (List<DateSchedule>) this)
    {
      DateSchedule dateSchedule2 = dateScheduleList.GetByDate(dateSchedule1.Date);
      if (dateSchedule2 == null)
      {
        dateSchedule2 = new DateSchedule(dateSchedule1.Date);
        dateScheduleList.Add(dateSchedule2);
      }
      dateSchedule2.TimeIntervalCollection.Merge(dateSchedule1.TimeIntervalCollection);
    }
    return dateScheduleList;
  }

  public double Duration
  {
    get => this.Sum<DateSchedule>((Func<DateSchedule, double>) (ds => ds.Duration));
  }

  [CanBeNull]
  public DateSchedule Find(DateTime dt)
  {
    return this.FirstOrDefault<DateSchedule>((Func<DateSchedule, bool>) (ds => ds.Date == dt.Date));
  }

  public double Work => this.Sum<DateSchedule>((Func<DateSchedule, double>) (ds => ds.Work));
}
