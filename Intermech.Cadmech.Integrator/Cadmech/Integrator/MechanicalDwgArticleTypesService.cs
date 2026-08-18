// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MechanicalDwgArticleTypesService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class MechanicalDwgArticleTypesService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : ArticleTypesService(driver, driverContext)
{
  protected override List<LocalId<int>> DoGetPossibleArticleTypes(SectionEntity articleItem)
  {
    DwgArticleData dwgArticleData = articleItem.Sections.Get<DwgArticleData>();
    return dwgArticleData.PossibleObjectTypes.GetRange(0, dwgArticleData.PossibleObjectTypes.Count);
  }
}
