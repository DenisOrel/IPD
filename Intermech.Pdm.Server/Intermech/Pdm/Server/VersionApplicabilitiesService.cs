// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.VersionApplicabilitiesService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System;

#nullable disable
namespace Intermech.Pdm.Server;

internal class VersionApplicabilitiesService : LongLifeObject, IVersionApplicabilitiesService
{
  ObjectFiltrationState IVersionApplicabilitiesService.CheckApplicabilities(
    IUserSession session,
    string applicabilities,
    long objectID,
    long masterArticle,
    DateTime date,
    int series)
  {
    if (session == null || objectID == 0L)
      return ObjectFiltrationState.fsFiltrationStopped;
    return string.IsNullOrEmpty(applicabilities) || !session.EnabledSeriesDates || date == DateTime.MinValue && series == int.MinValue ? ObjectFiltrationState.fsNotRequired : (applicabilities.IndexOf("1|") != 0 ? new SeriesDatesApplicabilityCollection((object) applicabilities) : new SeriesDatesApplicabilityCollection((object) session.GetObject(objectID, false))).CheckApplicabilities(session, objectID, masterArticle, date, series);
  }
}
