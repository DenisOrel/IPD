// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CaptureChangesArticleLocatorDataProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class CaptureChangesArticleLocatorDataProvider : ArticleLocatorDataProvider
{
  private SectionEntity articleItem;
  private MechanicalDriver driver;

  public CaptureChangesArticleLocatorDataProvider(
    MechanicalDriver driver,
    SectionEntity articleItem)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    this.driver = driver;
    this.articleItem = articleItem;
  }

  public override IExternalKeyLocatorData TryCreateExternalKeyDecoder()
  {
    SectionEntity articleMainDocument = this.driver.MechanicalOperations.Articles.TryGetArticleMainDocument(this.articleItem);
    if (articleMainDocument != null)
    {
      IArticleExternalKeysService externalKeysService = this.driver.TryGetArticleExternalKeysService(articleMainDocument);
      if (externalKeysService != null && externalKeysService.HasExternalKeySupport(this.articleItem, articleMainDocument))
        return (IExternalKeyLocatorData) new ExternalKeyLocatorData(this.driver, this.articleItem, externalKeysService);
    }
    return (IExternalKeyLocatorData) null;
  }

  public override IImbaseKeyLocatorData TryCreateImbaseKeyDecoder()
  {
    return (IImbaseKeyLocatorData) new ImbaseKeyLocatorData(this.articleItem);
  }

  public override IIdentityArticleLocatorData TryCreateIdentityDecoder()
  {
    return (IIdentityArticleLocatorData) new IdentityArticleLocatorData(this.articleItem);
  }
}
