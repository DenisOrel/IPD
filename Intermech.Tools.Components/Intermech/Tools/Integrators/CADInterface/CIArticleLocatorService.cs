// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleLocatorService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class CIArticleLocatorService : ArticleLocatorService
{
  private ICADInterfaceService cadService;

  public CIArticleLocatorService(
    MechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    ICADInterfaceService cadService)
    : base(driver, driverContext)
  {
    this.cadService = cadService != null ? cadService : throw new ArgumentNullException(nameof (cadService));
  }

  protected override IObjectLocator DoCreateNormalArticleLocator(SectionEntity articleItem)
  {
    return this.cadService.CreateArticleLocator(ArticleProcessingMethod.NormalObject, (ArticleLocatorDataProvider) new CaptureChangesArticleLocatorDataProvider(this.Driver, articleItem));
  }

  protected override IObjectLocator DoCreateImbaseObjectLocator(SectionEntity articleItem)
  {
    return this.cadService.CreateArticleLocator(ArticleProcessingMethod.ImbaseObject, (ArticleLocatorDataProvider) new CaptureChangesArticleLocatorDataProvider(this.Driver, articleItem));
  }

  protected override IObjectLocator DoCreateMinorMaterialLocator(SectionEntity articleItem)
  {
    return this.cadService.CreateArticleLocator(ArticleProcessingMethod.MinorMaterial, (ArticleLocatorDataProvider) new CaptureChangesArticleLocatorDataProvider(this.Driver, articleItem));
  }
}
