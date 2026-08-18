// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DateSchedule
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class DateSchedule : Entity
{
  private DateTime _date;
  [CanBeNull]
  [NonSerialized]
  private object _tag;
  [NotNull]
  private DayTimeIntervalCollection _timeIntervalCollection = new DayTimeIntervalCollection();

  public DateSchedule(DateTime date)
  {
    date = date.Date;
    this._date = date;
  }

  public DateSchedule(DateTime date, [NotNull] DayTimeIntervalCollection timeIntervalCollection)
    : this(date)
  {
    this.TimeIntervalCollection = timeIntervalCollection;
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.TimeIntervalCollection.ListChanged += new ListChangedEventHandler(this.TimeIntervalCollection_ListChanged);
  }

  private void TimeIntervalCollection_ListChanged([CanBeNull] object sender, [NotNull] ListChangedEventArgs e)
  {
    if (e.ListChangedType != ListChangedType.ItemAdded && e.ListChangedType != ListChangedType.ItemChanged && e.ListChangedType != ListChangedType.ItemDeleted && e.ListChangedType != ListChangedType.ItemMoved && e.ListChangedType != ListChangedType.Reset)
      return;
    this.OnPropertyChanged("TimeIntervalCollection");
  }

  public virtual DateTime Date => this._date;

  /// <summary>Дата + время, которое берется из первого интервала
  /// Пока что везде интервалы в timeIntervalCollection идут последовательно по возрастанию</summary>
  public DateTime StartTime => this._date.AddHours(this._timeIntervalCollection.Start);

  /// <summary>Дата + время, которое берется из последнего интервала
  /// Пока что везде интервалы в timeIntervalCollection идут последовательно по возрастанию</summary>
  public DateTime FinishTime => this._date.AddHours(this._timeIntervalCollection.Finish);

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
  public virtual DayTimeIntervalCollection TimeIntervalCollection
  {
    get => this._timeIntervalCollection;
    set
    {
      if (value == this.TimeIntervalCollection)
        return;
      this.TimeIntervalCollection.ListChanged -= new ListChangedEventHandler(this.TimeIntervalCollection_ListChanged);
      this._timeIntervalCollection = value;
      this.TimeIntervalCollection.ListChanged -= new ListChangedEventHandler(this.TimeIntervalCollection_ListChanged);
      this.OnPropertyChanged(nameof (TimeIntervalCollection));
    }
  }

  public double Duration => this.TimeIntervalCollection.Duration;

  public double Work => this.TimeIntervalCollection.Work;

  public void Assign([NotNull] DateSchedule src)
  {
    this._date = src.Date;
    this.TimeIntervalCollection.Assign(src.TimeIntervalCollection);
  }

  [NotNull]
  public DateSchedule Clone()
  {
    DateSchedule dateSchedule = new DateSchedule(this.Date);
    dateSchedule.Assign(this);
    return dateSchedule;
  }

  public override string ToString() => $"{this.StartTime} - {this.FinishTime}";
}
