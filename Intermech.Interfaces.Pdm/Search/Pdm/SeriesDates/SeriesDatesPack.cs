// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.SeriesDatesPack
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

[Serializable]
public sealed class SeriesDatesPack
{
  public SeriesDatesPack()
  {
    this.Groups = new SeriesDatesGroupCollection(this);
    this.ObjectVersionID = 0L;
  }

  public long ObjectVersionID { get; set; }

  public SeriesDatesGroupCollection Groups { get; private set; }

  public SeriesDatesPack Intersect(SeriesDatesPack otherSeriesDatesPack)
  {
    if (otherSeriesDatesPack == null)
      throw new ArgumentNullException(nameof (otherSeriesDatesPack));
    SeriesDatesPack seriesDatesPack = new SeriesDatesPack();
    foreach (long num in this.Groups.Select<SeriesDatesGroup, long>((Func<SeriesDatesGroup, long>) (o => o.HeadProductVersionID)).Intersect<long>(otherSeriesDatesPack.Groups.Select<SeriesDatesGroup, long>((Func<SeriesDatesGroup, long>) (o => o.HeadProductVersionID))))
    {
      long headProductVersionID = num;
      SeriesDatesGroup seriesDatesGroup = this.Groups.Single<SeriesDatesGroup>((Func<SeriesDatesGroup, bool>) (o => o.HeadProductVersionID == headProductVersionID)).Intersect(this.Groups.Single<SeriesDatesGroup>((Func<SeriesDatesGroup, bool>) (o => o.HeadProductVersionID == headProductVersionID)));
      if (seriesDatesGroup.Dates.Count > 0 || seriesDatesGroup.Series.Count > 0)
        seriesDatesPack.Groups.Add(seriesDatesGroup);
    }
    return seriesDatesPack;
  }
}
