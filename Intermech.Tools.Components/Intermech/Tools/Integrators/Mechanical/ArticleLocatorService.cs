// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleLocatorService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Сервис для поиска изделия в базе IPS по его описанию в документе приложения.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
public class ArticleLocatorService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : MechanicalDriverService(driver, driverContext), IArticleLocatorService
{
  public IObjectLocator CreateNormalArticleLocator(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoCreateNormalArticleLocator(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  protected virtual IObjectLocator DoCreateNormalArticleLocator(SectionEntity articleItem)
  {
    List<IObjectLocator> locators = new List<IObjectLocator>();
    SectionEntity articleMainDocument = this.Driver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null)
    {
      IArticleExternalKeysService externalKeysService = this.Driver.TryGetArticleExternalKeysService(articleMainDocument);
      if (externalKeysService != null && externalKeysService.HasExternalKeySupport(articleItem, articleMainDocument))
        locators.Add((IObjectLocator) new ExternalKeyArticleLocator((IExternalKeyLocatorData) new ExternalKeyLocatorData(this.Driver, articleItem, externalKeysService)));
    }
    locators.Add((IObjectLocator) new IdentityArticleLocator((IIdentityArticleLocatorData) new IdentityArticleLocatorData(articleItem)));
    return (IObjectLocator) new CompositeObjectLocator((IEnumerable<IObjectLocator>) locators);
  }

  public IObjectLocator CreateImbaseObjectLocator(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoCreateImbaseObjectLocator(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  protected virtual IObjectLocator DoCreateImbaseObjectLocator(SectionEntity articleItem)
  {
    return (IObjectLocator) new ImbaseKeyArticleLocator((IImbaseKeyLocatorData) new ImbaseKeyLocatorData(articleItem));
  }

  public IObjectLocator CreateMinorMaterialLocator(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoCreateMinorMaterialLocator(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  protected virtual IObjectLocator DoCreateMinorMaterialLocator(SectionEntity articleItem)
  {
    throw new NotSupportedException();
  }
}
