// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SingleArticleLocatorData
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
/// Реализует декодер исходных данных для алгоритма поиска единственного изделия, связанного с заданным документом.
/// </summary>
public sealed class SingleArticleLocatorData : ISingleArticleLocatorData
{
  private readonly MechanicalDriver driver;
  private readonly SectionEntity articleItem;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Стратегия анализа изменений</param>
  /// <param name="articleItem">Сущность изделия</param>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public SingleArticleLocatorData(MechanicalDriver driver, SectionEntity articleItem)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    this.driver = driver;
    this.articleItem = articleItem;
  }

  /// <summary>
  /// Возвращает версию документа, для которого надо найти изделие.
  /// </summary>
  /// <returns>Идентификатор версии документа</returns>
  public long GetDocumentId()
  {
    SectionEntity articleMainDocument = this.driver.MechanicalOperations.Articles.TryGetArticleMainDocument(this.articleItem);
    return articleMainDocument == null ? 0L : ObjectSection.GetObjectId(articleMainDocument);
  }
}
