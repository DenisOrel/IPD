// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ArticleLocatorBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует фабрику для алгоритмов поиска изделия в базе IPS в зависимости от разновидности изделия и способа его обработки.
/// </summary>
public class ArticleLocatorBuilder
{
  private readonly EmptyObjectLocator emptyLocator;
  private ArticleLocatorDataProvider dataProvider;

  /// <summary>Создает объект.</summary>
  public ArticleLocatorBuilder() => this.emptyLocator = new EmptyObjectLocator();

  /// <summary>
  /// Возвращает или задает провайдер данных для алгоритмов поиска изделия.
  /// Свойство автоматически очищается после выполнения метода создания алгоритма поиска.
  /// </summary>
  public ArticleLocatorDataProvider DataProvider
  {
    get => this.dataProvider;
    set => this.dataProvider = value;
  }

  /// <summary>
  /// Создает алгоритм поиска изделия в базе IPS. После выполнения метода все свойства с исходными данными будут автоматически очищены.
  /// </summary>
  /// <param name="method">Разновидность изделия и способ его обработки</param>
  /// <returns>Ссылка на алгоритм поиска изделия</returns>
  /// <exception cref="T:System.InvalidOperationException">Не все свойства с исходными данными корректно заполнены</exception>
  public IObjectLocator CreateLocator(ArticleProcessingMethod method)
  {
    if (this.dataProvider == null)
      throw new InvalidOperationException("Property 'DataProvider' is not set.");
    try
    {
      switch (method)
      {
        case ArticleProcessingMethod.NormalObject:
          List<IObjectLocator> locators = new List<IObjectLocator>();
          IExternalKeyLocatorData externalKeyDecoder = this.dataProvider.TryCreateExternalKeyDecoder();
          if (externalKeyDecoder != null)
            locators.Add((IObjectLocator) new ExternalKeyArticleLocator(externalKeyDecoder));
          IIdentityArticleLocatorData identityDecoder1 = this.dataProvider.TryCreateIdentityDecoder();
          if (identityDecoder1 != null)
            locators.Add((IObjectLocator) new IdentityArticleLocator(identityDecoder1));
          return locators.Count != 0 ? (IObjectLocator) new CompositeObjectLocator((IEnumerable<IObjectLocator>) locators) : (IObjectLocator) this.emptyLocator;
        case ArticleProcessingMethod.MinorMaterial:
          IIdentityArticleLocatorData identityDecoder2 = this.dataProvider.TryCreateIdentityDecoder();
          return identityDecoder2 != null ? (IObjectLocator) new MaterialLocator(identityDecoder2) : (IObjectLocator) this.emptyLocator;
        case ArticleProcessingMethod.ImbaseObject:
          IImbaseKeyLocatorData imbaseKeyDecoder = this.dataProvider.TryCreateImbaseKeyDecoder();
          return imbaseKeyDecoder != null ? (IObjectLocator) new ImbaseKeyArticleLocator(imbaseKeyDecoder) : (IObjectLocator) this.emptyLocator;
        default:
          throw new NotSupportedEnumException((Enum) method);
      }
    }
    finally
    {
      this.DataProvider = (ArticleLocatorDataProvider) null;
    }
  }
}
