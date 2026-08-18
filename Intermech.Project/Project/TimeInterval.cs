// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TimeInterval
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class TimeInterval : Entity
{
  private double _duration;
  private double _start;
  [CanBeNull]
  [NonSerialized]
  private object _tag;
  public double FTag;
  public double Ratio = 1.0;

  public TimeInterval(double start, double duration)
  {
    this.Start = start;
    this.Duration = duration;
  }

  public TimeInterval(int startHours, int startMinutes, int finishHours, int finishMinutes)
  {
    this.Start = (double) startHours + (double) startMinutes / 60.0;
    this.Duration = (double) finishHours + (double) finishMinutes / 60.0 - this.Start;
  }

  public virtual double Duration
  {
    get => this._duration;
    set
    {
      if (value == this.Duration)
        return;
      this._duration = value;
      this.OnPropertyChanged(nameof (Duration));
    }
  }

  public virtual double Start
  {
    get => this._start;
    set
    {
      if (value == this.Start)
        return;
      this._start = value;
      this.OnPropertyChanged(nameof (Start));
    }
  }

  public double Finish => this.Start + this.Duration;

  [CanBeNull]
  public virtual object Tag
  {
    get => this._tag;
    set
    {
      if (value == this.Tag)
        return;
      this._tag = value;
      this.OnPropertyChanged(nameof (Tag));
    }
  }

  [NotNull]
  public TimeInterval Clone()
  {
    return new TimeInterval(this._start, this._duration)
    {
      Ratio = this.Ratio
    };
  }

  /// <summary>Объединяет два временных интервала, если это возможно (между интервалами нет промежутков)</summary>
  /// <returns>Возвращает true, если объединение успешно и false, если оно невозможно</returns>
  public bool Merge([NotNull] TimeInterval ti)
  {
    if (ti.Start >= this.Start && ti.Start < this.Finish)
    {
      this.Duration = Math.Max(this.Finish, ti.Finish) - this.Start;
      return true;
    }
    if (this.Start < ti.Start || this.Start >= ti.Finish)
      return false;
    double num = Math.Max(this.Finish, ti.Finish);
    this.Start = ti.Start;
    this.Duration = num - ti.Start;
    return true;
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return obj is TimeInterval timeInterval && this.Start == timeInterval.Start && this.Duration == timeInterval.Duration && this.Ratio == timeInterval.Ratio;
  }

  public override int GetHashCode() => (this.Start, this.Duration, this.Ratio).GetHashCode();

  public double Work => this.Duration * this.Ratio;
}
