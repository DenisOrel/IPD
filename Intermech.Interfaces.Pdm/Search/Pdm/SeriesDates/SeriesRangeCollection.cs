// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.SeriesRangeCollection
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
public sealed class SeriesRangeCollection(SeriesDatesGroup owner) : RangeCollectionBase<SeriesRange>(owner)
{
  public void Normalize()
  {
    this.ToList<SeriesRange>();
    List<SeriesRange> source = new List<SeriesRange>();
    foreach (SeriesRange seriesRange1 in (Collection<SeriesRange>) this)
    {
      SeriesRange seriesRange2 = seriesRange1;
      if (!seriesRange2.IsEmpty)
      {
        List<SeriesRange> seriesRangeList = new List<SeriesRange>();
        seriesRangeList.Add(seriesRange2);
        foreach (SeriesRange otherSeriesRange in source)
        {
          SeriesRange unionSeriesRange = (SeriesRange) null;
          if (seriesRange2.TryUnion(otherSeriesRange, out unionSeriesRange))
          {
            seriesRangeList.Remove(seriesRange2);
            seriesRangeList.Add(unionSeriesRange);
            seriesRange2 = unionSeriesRange;
          }
          else
            seriesRangeList.Add(otherSeriesRange);
        }
        source = seriesRangeList;
      }
    }
    this.Clear();
    this.AddRange(source.OrderBy<SeriesRange, int>((Func<SeriesRange, int>) (o => o.Start)).ToArray<SeriesRange>());
  }

  [System.Runtime.Serialization.OnDeserialized]
  private void OnDeserialized(StreamingContext context)
  {
    SeriesRange[] array = this.Items.ToArray<SeriesRange>();
    this.ClearItems();
    foreach (SeriesRange seriesRange in array)
      this.Add(seriesRange);
  }
}
