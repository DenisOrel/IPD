// Decompiled with JetBrains decompiler
// Type: Intermech.Project.RatioSchedule
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project;

public class RatioSchedule : Schedule, IEquatable<RatioSchedule>
{
  [NotNull]
  public Schedule BaseSchedule { get; private set; }

  public double Ratio { get; private set; }

  public RatioSchedule([NotNull] Schedule baseSchedule, double ratio)
  {
    this.BaseSchedule = baseSchedule;
    this.Ratio = ratio;
    this.Assign(baseSchedule);
  }

  protected internal override DayTimeIntervalCollection GetIntervals(DateTime date)
  {
    DayTimeIntervalCollection intervals1 = this.BaseSchedule.GetIntervals(date);
    DayTimeIntervalCollection intervals2 = new DayTimeIntervalCollection();
    foreach (TimeInterval timeInterval1 in (System.Collections.ObjectModel.Collection<TimeInterval>) intervals1)
    {
      TimeInterval timeInterval2 = timeInterval1.Clone();
      timeInterval2.Ratio *= this.Ratio;
      intervals2.Add(timeInterval2);
    }
    return intervals2;
  }

  public override void Assign(Schedule src)
  {
    base.Assign(src);
    if (!(src is RatioSchedule ratioSchedule))
      return;
    this.BaseSchedule = ratioSchedule.BaseSchedule;
    this.Ratio = ratioSchedule.Ratio;
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return obj is RatioSchedule other && this.Equals(other);
  }

  public bool Equals([CanBeNull] RatioSchedule other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    return object.Equals((object) this.BaseSchedule, (object) other.BaseSchedule) && Math.Abs(this.Ratio - other.Ratio) < 1E-09;
  }

  public override int GetHashCode()
  {
    return (base.GetHashCode(), this.BaseSchedule, this.Ratio).GetHashCode();
  }
}
