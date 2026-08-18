// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.DateRange
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

[Serializable]
public sealed class DateRange : IRange, INotifyPropertyChanged
{
  public static readonly DateTime MinValue = DateTime.MinValue.Date;
  public static readonly DateTime MaxValue = DateTime.MaxValue.Date;
  private static readonly Regex SplitRangeRagex = new Regex("[^\\.0-9]", RegexOptions.Compiled);
  private DateTime _start = DateRange.MinValue;
  private DateTime _end = DateRange.MaxValue;
  private SeriesDatesGroup _group;

  public static DateRange Empty => new DateRange(DateRange.MinValue, DateRange.MaxValue);

  public static DateRange FromString(string @string)
  {
    if (string.IsNullOrEmpty(@string))
      throw new ArgumentNullException("@string");
    try
    {
      string[] strArray = DateRange.SplitRangeRagex.Split(@string);
      if (strArray.Length == 0)
        return DateRange.Empty;
      if (strArray.Length != 1)
        return new DateRange(strArray.Length == 0 || string.IsNullOrEmpty(strArray[0]) ? DateRange.MinValue : Convert.ToDateTime(strArray[0]), strArray.Length <= 1 || string.IsNullOrEmpty(strArray[1]) ? DateRange.MaxValue : Convert.ToDateTime(strArray[1]));
      DateTime dateTime = !string.IsNullOrEmpty(strArray[0]) ? Convert.ToDateTime(strArray[0]) : DateRange.MinValue;
      return new DateRange(dateTime, dateTime);
    }
    catch (FormatException ex)
    {
      throw new Exception("Не удается распознать дату, или дата не является действительной (нет в календаре)", (Exception) ex);
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  public DateRange(DateTime start, DateTime end)
  {
    DateTime date1 = start.Date;
    DateTime date2 = end.Date;
    this.Start = !(date1 > date2) ? date1 : throw new ArgumentException();
    this.End = date2;
  }

  public DateTime Start
  {
    get => this._start;
    set
    {
      DateTime date = value.Date;
      if (date > this.End)
        throw new ArgumentException();
      if (!(this._start != date))
        return;
      this._start = date;
      this.OnPropertyChanged(nameof (Start));
    }
  }

  public DateTime End
  {
    get => this._end;
    set
    {
      DateTime date = value.Date;
      if (date < this.Start)
        throw new ArgumentException();
      if (!(this._end != date))
        return;
      this._end = date;
      this.OnPropertyChanged(nameof (End));
    }
  }

  public bool TryUnion(DateRange otherDateRange, out DateRange unionDateRange)
  {
    if (otherDateRange == null)
      throw new ArgumentNullException("otherSeriesRange");
    unionDateRange = (DateRange) null;
    if ((!(this.Start <= otherDateRange.Start) || !(this.End >= otherDateRange.End)) && (!(this.Start >= otherDateRange.Start) || !(this.End <= otherDateRange.End)) && (!(this.Start <= otherDateRange.Start) || !(this.End >= otherDateRange.Start)) && (!(this.Start <= otherDateRange.End) || !(this.End >= otherDateRange.End)) && (!(otherDateRange.Start != DateRange.MinValue) || !(this.End == otherDateRange.Start.Subtract(new TimeSpan(864000000000L)).Date)) && (!(otherDateRange.End != DateRange.MinValue) || !(this.Start == otherDateRange.End.Subtract(new TimeSpan(864000000000L)).Date)) && (!(this.Start != DateRange.MinValue) || !(this.Start.Subtract(new TimeSpan(864000000000L)).Date == otherDateRange.End)) && (!(this.End != DateRange.MinValue) || !(this.End.Subtract(new TimeSpan(864000000000L)).Date == otherDateRange.Start)))
      return false;
    unionDateRange = new DateRange(this.Start < otherDateRange.Start ? this.Start : otherDateRange.Start, this.End > otherDateRange.End ? this.End : otherDateRange.End);
    return true;
  }

  public override bool Equals(object obj)
  {
    if (this == obj)
      return true;
    return obj is DateRange dateRange && object.Equals((object) this.Start, (object) dateRange.Start) && object.Equals((object) this.End, (object) dateRange.End);
  }

  public override int GetHashCode()
  {
    DateTime dateTime = this.Start;
    int num1 = dateTime.GetHashCode() << 16 /*0x10*/;
    dateTime = this.End;
    int num2 = dateTime.GetHashCode() & (int) ushort.MaxValue;
    return num1 | num2;
  }

  public override string ToString()
  {
    if (this.Start == this.End)
      return !(this.Start != DateRange.MinValue) || !(this.Start != DateRange.MaxValue) ? string.Empty : this.Start.Date.ToString("d");
    DateTime dateTime;
    string empty1;
    if (!(this.Start != DateRange.MinValue) || !(this.Start != DateRange.MaxValue))
    {
      empty1 = string.Empty;
    }
    else
    {
      dateTime = this.Start.Date;
      empty1 = dateTime.ToString("d");
    }
    string empty2;
    if (!(this.End != DateRange.MinValue) || !(this.End != DateRange.MaxValue))
    {
      empty2 = string.Empty;
    }
    else
    {
      dateTime = this.End;
      dateTime = dateTime.Date;
      empty2 = dateTime.ToString("d");
    }
    return $"{empty1}..{empty2}";
  }

  object IRange.Start
  {
    get => (object) this.Start;
    set => this.Start = value is DateTime dateTime ? dateTime : throw new ArgumentException();
  }

  public bool HasStart
  {
    get => this.Start != DateRange.MinValue;
    set
    {
      if (this.HasStart == value)
        return;
      this.Start = value ? (this.HasEnd ? this.End : DateTime.Now.Date) : DateRange.MinValue;
      this.OnPropertyChanged(nameof (HasStart));
    }
  }

  object IRange.End
  {
    get => (object) this.End;
    set => this.End = value is DateTime dateTime ? dateTime : throw new ArgumentException();
  }

  public bool HasEnd
  {
    get => this.End != DateRange.MaxValue;
    set
    {
      if (this.HasEnd == value)
        return;
      this.End = value ? (this.HasStart ? this.Start : DateTime.Now.Date) : DateRange.MaxValue;
      this.OnPropertyChanged(nameof (HasEnd));
    }
  }

  public bool IsEmpty => !this.HasStart && !this.HasEnd;

  public SeriesDatesGroup Group
  {
    get => this._group;
    set
    {
      if (this._group == value)
        return;
      SeriesDatesGroup group = this._group;
      this._group = value;
      group?.Dates.Remove(this);
      if (this._group == null)
        return;
      this._group.Dates.Add(this);
    }
  }

  public event PropertyChangedEventHandler PropertyChanged;

  private void OnPropertyChanged(string propertyName)
  {
    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
    if (propertyChanged == null)
      return;
    propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }
}
