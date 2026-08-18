// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.ISeriesDatesServerService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

public interface ISeriesDatesServerService
{
  SeriesDatesPack FindSeriesDates(Guid userSessionGuid, long[] objectVersionIds);

  Dictionary<long, Dictionary<long, SeriesDatesPack>> FindSeriesDatesForOtherVersions(
    Guid userSessionGuid,
    long[] objectVersionIds);

  void SaveSeriesDates(
    Guid userSessionGuid,
    long[] objectVersionIds,
    SeriesDatesPack seriesDatesPack);
}
