// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MechanicalDwgArticleLocatorService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class MechanicalDwgArticleLocatorService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : ArticleLocatorService(driver, driverContext)
{
  protected override IObjectLocator DoCreateNormalArticleLocator(SectionEntity articleItem)
  {
    List<IObjectLocator> locators = new List<IObjectLocator>();
    if (articleItem.Sections.Contains<PartData>())
      locators.Add((IObjectLocator) new PartGuidArticleLocator((IPartGuidArticleLocatorData) new PartGuidArticleLocatorDataFromArticleEntity(articleItem)));
    locators.Add((IObjectLocator) new IdentityArticleLocator((IIdentityArticleLocatorData) new IdentityArticleLocatorData(articleItem)));
    return locators.Count <= 1 ? locators[0] : (IObjectLocator) new CompositeObjectLocator((IEnumerable<IObjectLocator>) locators);
  }

  protected override IObjectLocator DoCreateMinorMaterialLocator(SectionEntity articleItem)
  {
    return (IObjectLocator) new MaterialLocator((IIdentityArticleLocatorData) new IdentityArticleLocatorData(articleItem));
  }
}
