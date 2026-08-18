// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.SeriesDatesGroup
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.Utilities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

[Serializable]
public sealed class SeriesDatesGroup : INotifyPropertyChanged
{
  private SeriesDatesPack _pack;

  public SeriesDatesGroup(long headProductVersionID)
  {
    this.HeadProductVersionID = !ObjectHelper.IsUnknownObjectVersionID(headProductVersionID) ? headProductVersionID : throw new ArgumentException();
    this.Series = new SeriesRangeCollection(this);
    this.Series.ListChanged += new ListChangedEventHandler(this.Series_ListChanged);
    this.Dates = new DateRangeCollection(this);
    this.Dates.ListChanged += new ListChangedEventHandler(this.Dates_ListChanged);
  }

  public long HeadProductVersionID { get; private set; }

  public SeriesRangeCollection Series { get; private set; }

  public DateRangeCollection Dates { get; private set; }

  public SeriesDatesPack Pack
  {
    get => this._pack;
    set
    {
      if (this._pack == value)
        return;
      SeriesDatesPack pack = this._pack;
      this._pack = value;
      pack?.Groups.Remove(this);
      if (this._pack == null)
        return;
      this._pack.Groups.Add(this);
    }
  }

  public SeriesDatesGroup Intersect(SeriesDatesGroup otherSeriesDatesGroup)
  {
    if (otherSeriesDatesGroup == null)
      throw new ArgumentNullException(nameof (otherSeriesDatesGroup));
    SeriesDatesGroup seriesDatesGroup = this.HeadProductVersionID == otherSeriesDatesGroup.HeadProductVersionID ? new SeriesDatesGroup(this.HeadProductVersionID) : throw new ArgumentException();
    foreach (SeriesRange seriesRange1 in this.Series.ToList<SeriesRange>())
    {
      SeriesRange seriesRange = seriesRange1;
      SeriesRange seriesRange2 = otherSeriesDatesGroup.Series.SingleOrDefault<SeriesRange>((Func<SeriesRange, bool>) (o => object.Equals((object) o, (object) seriesRange)));
      if (seriesRange2 != null)
        seriesDatesGroup.Series.Add(seriesRange2);
    }
    foreach (DateRange dateRange1 in this.Dates.ToList<DateRange>())
    {
      DateRange dateRange = dateRange1;
      DateRange dateRange2 = otherSeriesDatesGroup.Dates.SingleOrDefault<DateRange>((Func<DateRange, bool>) (o => object.Equals((object) o, (object) dateRange)));
      if (dateRange2 != null)
        seriesDatesGroup.Dates.Add(dateRange2);
    }
    return seriesDatesGroup;
  }

  public event PropertyChangedEventHandler PropertyChanged;

  private void Dates_ListChanged(object sender, ListChangedEventArgs e)
  {
    this.OnPropertyChanged("DatesAsString");
  }

  private void Series_ListChanged(object sender, ListChangedEventArgs e)
  {
    this.OnPropertyChanged("SeriesAsString");
  }

  [System.Runtime.Serialization.OnDeserialized]
  private void OnDeserialized(StreamingContext context)
  {
    this.Series.ListChanged += new ListChangedEventHandler(this.Series_ListChanged);
    this.Dates.ListChanged += new ListChangedEventHandler(this.Dates_ListChanged);
  }

  private void OnPropertyChanged(string propertyName)
  {
    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
    if (propertyChanged == null)
      return;
    propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }
}
