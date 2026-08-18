// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DayOfWeekSchedule
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
public class DayOfWeekSchedule : Entity
{
  [CanBeNull]
  [NonSerialized]
  private object _tag;
  [NotNull]
  private DayTimeIntervalCollection _timeIntervalCollection = new DayTimeIntervalCollection();

  public DayOfWeekSchedule(DayOfWeek dayOfWeek) => this.DayOfWeek = dayOfWeek;

  public DayOfWeekSchedule(DayOfWeek dayOfWeek, [NotNull] DayTimeIntervalCollection timeIntervalCollection)
    : this(dayOfWeek)
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

  public virtual DayOfWeek DayOfWeek { get; }

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
}
