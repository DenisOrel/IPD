// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.DateRangeCollection
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

[Serializable]
public sealed class DateRangeCollection(SeriesDatesGroup owner) : RangeCollectionBase<DateRange>(owner)
{
  public void Normalize()
  {
    this.ToList<DateRange>();
    List<DateRange> source = new List<DateRange>();
    foreach (DateRange dateRange1 in (Collection<DateRange>) this)
    {
      DateRange dateRange2 = dateRange1;
      if (!dateRange2.IsEmpty)
      {
        List<DateRange> dateRangeList = new List<DateRange>();
        dateRangeList.Add(dateRange2);
        foreach (DateRange otherDateRange in source)
        {
          DateRange unionDateRange = (DateRange) null;
          if (dateRange2.TryUnion(otherDateRange, out unionDateRange))
          {
            dateRangeList.Remove(dateRange2);
            dateRangeList.Add(unionDateRange);
            dateRange2 = unionDateRange;
          }
          else
            dateRangeList.Add(otherDateRange);
        }
        source = dateRangeList;
      }
    }
    this.Clear();
    this.AddRange(source.OrderBy<DateRange, DateTime>((Func<DateRange, DateTime>) (o => o.Start)).ToArray<DateRange>());
  }

  [System.Runtime.Serialization.OnDeserialized]
  private void OnDeserialized(StreamingContext context)
  {
    DateRange[] array = this.Items.ToArray<DateRange>();
    this.ClearItems();
    foreach (DateRange dateRange in array)
      this.Add(dateRange);
  }
}
