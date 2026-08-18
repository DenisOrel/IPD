// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ExternalKeyLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует декодер исходных данных для алгоритма поиска изделия в применяемости документа по внешнему ключу изделия, хранящемуся в файле документа.
/// </summary>
public sealed class ExternalKeyLocatorData : IExternalKeyLocatorData
{
  private readonly MechanicalDriver driver;
  private readonly SectionEntity articleItem;
  private readonly IArticleExternalKeysService externalKeysService;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Стратегия анализа изменений</param>
  /// <param name="articleItem">Сущность изделия</param>
  /// <param name="externalKeysService">Сервис внешних ключей. Может быть не задан</param>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public ExternalKeyLocatorData(
    MechanicalDriver driver,
    SectionEntity articleItem,
    IArticleExternalKeysService externalKeysService)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    this.driver = driver;
    this.articleItem = articleItem;
    this.externalKeysService = externalKeysService;
  }

  /// <summary>
  /// Возвращает внешний ключ изделия, хранящийся в файле документа.
  /// </summary>
  /// <returns>Значение внешнего ключа изделия, может быть равно null или пустой строке</returns>
  public string GetExternalKey()
  {
    if (this.externalKeysService == null)
      return (string) null;
    SectionEntity articleMainDocument = this.driver.MechanicalOperations.Articles.TryGetArticleMainDocument(this.articleItem);
    if (articleMainDocument == null)
      return (string) null;
    return !this.externalKeysService.HasExternalKeySupport(this.articleItem, articleMainDocument) ? (string) null : this.externalKeysService.GetExternalKey(this.articleItem, articleMainDocument);
  }

  /// <summary>Возвращает версию документа.</summary>
  /// <returns>Значение идентификатора версии документа, может быть неопределено</returns>
  public long GetDocumentId()
  {
    SectionEntity articleMainDocument = this.driver.MechanicalOperations.Articles.TryGetArticleMainDocument(this.articleItem);
    return articleMainDocument == null ? 0L : articleMainDocument.Sections.Get<ObjectSection>().ObjectId;
  }
}
