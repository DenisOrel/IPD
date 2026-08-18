// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DayTimeIntervalCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class DayTimeIntervalCollection : TimeIntervalCollection
{
  public DayTimeIntervalCollection()
  {
  }

  public DayTimeIntervalCollection([NotNull] DayTimeIntervalCollection src)
    : this()
  {
    this.Assign(src);
  }

  protected DayTimeIntervalCollection([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  protected override void OnListChanged(ListChangedEventArgs e)
  {
    base.OnListChanged(e);
    if ((e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged) && this[e.NewIndex].Start >= 24.0)
    {
      this.RemoveAt(e.NewIndex);
      throw new ArgumentOutOfRangeException("Cannot update time interval collection: start value must be restricted to one day.");
    }
  }

  public double Start
  {
    get
    {
      if (this.Count <= 0)
        return 0.0;
      return this._AllowDuplicates ? this.Select<TimeInterval, double>((Func<TimeInterval, double>) (ti => ti.Start)).Append<double>(25.0).Min() : this[0].Start;
    }
  }

  public double Finish
  {
    get
    {
      if (this.Count <= 0)
        return 0.0;
      return this._AllowDuplicates ? this.Select<TimeInterval, double>((Func<TimeInterval, double>) (ti => ti.Finish)).Append<double>(0.0).Max() : this[this.Count - 1].Finish;
    }
  }

  internal void Assign([NotNull, ItemNotNull] DayTimeIntervalCollection coll)
  {
    this.Clear();
    foreach (TimeInterval timeInterval in (System.Collections.ObjectModel.Collection<TimeInterval>) coll)
      this.Add(timeInterval.Clone());
  }

  public double Work => this.Sum<TimeInterval>((Func<TimeInterval, double>) (ti => ti.Work));

  public void Merge([NotNull, ItemNotNull] DayTimeIntervalCollection intervals)
  {
    DayTimeIntervalCollection intervalCollection = this;
    foreach (TimeInterval interval in (System.Collections.ObjectModel.Collection<TimeInterval>) intervals)
    {
      bool flag = false;
      for (int index = 0; index < intervalCollection.Count; ++index)
      {
        TimeInterval timeInterval1 = intervalCollection[index];
        TimeInterval timeInterval2 = (TimeInterval) null;
        TimeInterval timeInterval3 = (TimeInterval) null;
        if (timeInterval1.Equals((object) interval))
        {
          timeInterval1.Ratio += interval.Ratio;
          flag = true;
          break;
        }
        if (interval.Start >= timeInterval1.Start && interval.Start < timeInterval1.Finish)
        {
          timeInterval2 = timeInterval1;
          timeInterval3 = interval;
        }
        else if (timeInterval1.Start >= interval.Start && timeInterval1.Start < interval.Finish)
        {
          timeInterval2 = interval;
          timeInterval3 = timeInterval1;
        }
        if (timeInterval2 != null)
        {
          TimeInterval timeInterval4 = new TimeInterval(timeInterval2.Start, timeInterval3.Start - timeInterval2.Start);
          timeInterval4.Ratio = timeInterval2.Ratio;
          double start = Math.Min(timeInterval2.Finish, timeInterval3.Finish);
          TimeInterval timeInterval5 = new TimeInterval(timeInterval3.Start, start - timeInterval3.Start);
          timeInterval5.Ratio = timeInterval2.Ratio + timeInterval3.Ratio;
          TimeInterval timeInterval6 = new TimeInterval(start, Math.Max(timeInterval2.Finish, timeInterval3.Finish) - start);
          timeInterval6.Ratio = timeInterval3.Ratio;
          flag = true;
          intervalCollection.RemoveAt(index);
          if (timeInterval6.Duration > 0.0)
            intervalCollection.Insert(index, timeInterval6);
          if (timeInterval5.Duration > 0.0)
            intervalCollection.Insert(index, timeInterval5);
          if (timeInterval4.Duration > 0.0)
          {
            intervalCollection.Insert(index, timeInterval4);
            break;
          }
          break;
        }
      }
      if (!flag)
      {
        int index = 0;
        while (index < intervalCollection.Count && intervalCollection[index].Start <= interval.Start)
          ++index;
        intervalCollection.Insert(index, interval);
      }
    }
  }
}
