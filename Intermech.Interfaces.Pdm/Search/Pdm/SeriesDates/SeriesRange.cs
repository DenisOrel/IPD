// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.SeriesRange
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
public sealed class SeriesRange : IRange, INotifyPropertyChanged
{
  public const int MinValue = -2147483648 /*0x80000000*/;
  public const int MaxValue = 2147483647 /*0x7FFFFFFF*/;
  private static readonly Regex SplitRangeRegex = new Regex("[^\\-0-9]+", RegexOptions.Compiled);
  private int _start = int.MinValue;
  private int _end = int.MaxValue;
  private SeriesDatesGroup _group;

  public static SeriesRange Empty => new SeriesRange(int.MinValue, int.MaxValue);

  public static SeriesRange FromString(string @string)
  {
    string[] strArray = !string.IsNullOrEmpty(@string) ? SeriesRange.SplitRangeRegex.Split(@string) : throw new ArgumentException();
    if (strArray.Length == 0)
      return SeriesRange.Empty;
    if (strArray.Length == 1)
    {
      int num = !string.IsNullOrEmpty(strArray[0]) ? Convert.ToInt32(strArray[0]) : int.MinValue;
      if (num < 0)
        num = 0;
      return new SeriesRange(num, num);
    }
    int start = strArray.Length == 0 || string.IsNullOrEmpty(strArray[0]) ? int.MinValue : Convert.ToInt32(strArray[0]);
    int end = strArray.Length <= 1 || string.IsNullOrEmpty(strArray[1]) ? int.MaxValue : Convert.ToInt32(strArray[1]);
    if (start < 0)
      start = 0;
    if (end < 0)
      end = 0;
    return new SeriesRange(start, end);
  }

  public SeriesRange(int start, int end)
  {
    this._start = start <= end ? start : throw new ArgumentException();
    this._end = end;
  }

  public int Start
  {
    get => this._start;
    set
    {
      if (value > this.End)
        throw new ArgumentException();
      if (this._start == value)
        return;
      this._start = value;
      this.OnPropertyChanged(nameof (Start));
    }
  }

  public int End
  {
    get => this._end;
    set
    {
      if (value < this.Start)
        throw new ArgumentException();
      if (this._end == value)
        return;
      this._end = value;
      this.OnPropertyChanged(nameof (End));
    }
  }

  public bool TryUnion(SeriesRange otherSeriesRange, out SeriesRange unionSeriesRange)
  {
    if (otherSeriesRange == null)
      throw new ArgumentNullException(nameof (otherSeriesRange));
    unionSeriesRange = (SeriesRange) null;
    if ((this.Start > otherSeriesRange.Start || this.End < otherSeriesRange.End) && (this.Start < otherSeriesRange.Start || this.End > otherSeriesRange.End) && (this.Start > otherSeriesRange.Start || this.End < otherSeriesRange.Start) && (this.Start > otherSeriesRange.End || this.End < otherSeriesRange.End) && (otherSeriesRange.Start == int.MinValue || this.End != otherSeriesRange.Start - 1) && (otherSeriesRange.End == int.MinValue || this.Start != otherSeriesRange.End - 1) && (this.Start == int.MinValue || this.Start - 1 != otherSeriesRange.End) && (this.End == int.MinValue || this.End - 1 != otherSeriesRange.Start))
      return false;
    unionSeriesRange = new SeriesRange(this.Start < otherSeriesRange.Start ? this.Start : otherSeriesRange.Start, this.End > otherSeriesRange.End ? this.End : otherSeriesRange.End);
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
    int num1 = this.Start;
    int num2 = num1.GetHashCode() << 16 /*0x10*/;
    num1 = this.End;
    int num3 = num1.GetHashCode() & (int) ushort.MaxValue;
    return num2 | num3;
  }

  public override string ToString()
  {
    if (this.Start != this.End)
      return $"{(this.Start == int.MinValue || this.Start == int.MaxValue ? (object) string.Empty : (object) this.Start.ToString())}..{(this.End == int.MinValue || this.End == int.MaxValue ? (object) string.Empty : (object) this.End.ToString())}";
    return this.Start == int.MinValue || this.Start == int.MaxValue ? string.Empty : this.Start.ToString();
  }

  object IRange.Start
  {
    get => (object) this.Start;
    set => this.Start = value is int num ? num : throw new ArgumentException();
  }

  public bool HasStart
  {
    get => this.Start != int.MinValue;
    set
    {
      if (this.HasStart == value)
        return;
      this.Start = value ? (this.HasEnd ? this.End : 0) : int.MinValue;
      this.OnPropertyChanged(nameof (HasStart));
    }
  }

  object IRange.End
  {
    get => (object) this.End;
    set => this.End = value is int num ? num : throw new ArgumentException();
  }

  public bool HasEnd
  {
    get => this.End != int.MaxValue;
    set
    {
      if (this.HasEnd == value)
        return;
      this.End = value ? (this.Start > 0 ? this.Start : 0) : int.MaxValue;
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
      group?.Series.Remove(this);
      if (this._group == null)
        return;
      this._group.Series.Add(this);
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
