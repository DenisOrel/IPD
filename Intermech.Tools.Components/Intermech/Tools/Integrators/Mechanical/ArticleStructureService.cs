// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleStructureService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует базовый класс для сервиса работы с составом изделия. Сервис используется при сохранении изменений в конструкторских документах для синхронизации проектных связей между изделиями.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
public class ArticleStructureService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : MechanicalDriverService(driver, driverContext), IArticleStructureService
{
  /// <summary>
  /// Проверяет, является ли указанное изделие сборочной единицей, т.е. изделием с конструкторским составом.
  /// Это метод используется для определения изделий, для которых требуется выполнить синхронизацию состава.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>true - указанное изделие является сборочной единицей и требует синхронизации состава, false - изделие не требует синхронизации состава</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public bool IsProjectArticle(SectionEntity articleItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    SectionEntity articleMainDocument = this.Driver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem);
    return this.IsProjectArticle(articleItem, articleMainDocument);
  }

  /// <summary>
  /// Проверяет, является ли указанное изделие сборочной единицей, т.е. изделием с конструкторским составом.
  /// Это метод используется для определения изделий, для которых требуется выполнить синхронизацию состава.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <param name="documentItem">Сущность конструкторского документа, по которому выпускается изделие. Значение параметра может быть null, если источником изделия является не документ, а что-то другое</param>
  /// <returns>true - указанное изделие является сборочной единицей и требует синхронизации состава, false - изделие не требует синхронизации состава</returns>
  protected virtual bool IsProjectArticle(SectionEntity articleItem, SectionEntity documentItem)
  {
    return false;
  }

  [Conditional("DEBUG")]
  private void CheckIsProjectArticle(SectionEntity articleItem)
  {
    if (!this.IsProjectArticle(articleItem))
      throw new InvalidOperationException($"Указанное изделие '{DisplaySection.GetQualifiedName(articleItem)}' не является сборочной единицей.");
  }

  /// <summary>
  /// Возвращает состав указанной сборочной единицы в виде коллекции вхождений изделий-компонентов.
  /// Каждое вхождение компонента соответствует одной проектной связи с компонентом в базе IPS.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <returns>Коллекция вхождений изделий-компонентов</returns>
  /// <exception cref="T:ArgumentNullException">projectArticleItem</exception>
  public List<ArticleStructureOccurence> ReadArticleStructure(SectionEntity projectArticleItem)
  {
    return projectArticleItem != null ? this.DoReadArticleStructure(projectArticleItem) : throw new ArgumentNullException(nameof (projectArticleItem));
  }

  /// <summary>
  /// Возвращает состав указанной сборочной единицы в виде коллекции вхождений изделий-компонентов.
  /// Каждое вхождение компонента соответствует одной проектной связи с компонентом в базе IPS.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <returns>Коллекция вхождений изделий-компонентов</returns>
  protected virtual List<ArticleStructureOccurence> DoReadArticleStructure(
    SectionEntity projectArticleItem)
  {
    return new List<ArticleStructureOccurence>(32 /*0x20*/);
  }

  /// <summary>
  /// Реализует поиск сущности для изделия-компонента. Реализация по умолчанию использует поиск по уникальному ключу изделия-компонента.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="componentOccurence">Вхождение изделия-компонента</param>
  /// <returns>Найденная сущность для изделия компонента или null</returns>
  /// <exception cref="T:ArgumentNullException">projectArticleItem or componentOccurence</exception>
  public SectionEntity FindArticleComponent(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence)
  {
    if (projectArticleItem == null)
      throw new ArgumentNullException(nameof (projectArticleItem));
    return componentOccurence != null ? this.DoFindArticleComponent(projectArticleItem, componentOccurence) : throw new ArgumentNullException(nameof (componentOccurence));
  }

  /// <summary>
  /// Реализует поиск сущности для изделия-компонента. Реализация по умолчанию использует поиск по уникальному ключу изделия-компонента.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="componentOccurence">Вхождение изделия-компонента</param>
  /// <returns>Найденная сущность для изделия компонента или null</returns>
  protected virtual SectionEntity DoFindArticleComponent(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence)
  {
    return projectArticleItem.Database is CaptureChangesDatabase database ? ArticleSection.FindArticleByKey(database, componentOccurence.ComponentKey) : (SectionEntity) null;
  }

  /// <summary>
  /// Возвращает путь к файлу документа, в котором описан компонент.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="componentOccurence">Вхождение изделия-компонента</param>
  /// <returns>Путь к файлу документа или null</returns>
  public string TryGetArticleComponentFile(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence)
  {
    if (projectArticleItem == null)
      throw new ArgumentNullException(nameof (projectArticleItem));
    return componentOccurence != null ? this.DoTryGetArticleComponentFile(projectArticleItem, componentOccurence) : throw new ArgumentNullException(nameof (componentOccurence));
  }

  /// <summary>
  /// Возвращает путь к файлу документа, в котором описан компонент.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="componentOccurence">Вхождение изделия-компонента</param>
  /// <returns>Путь к файлу документа или null</returns>
  protected virtual string DoTryGetArticleComponentFile(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence)
  {
    return (string) null;
  }

  /// <summary>
  /// Записывает в объект CAD-системы изменения, сделанные в процессе синхронизации состава сборочной единицы.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="stats">Статистика по изменениям в составе сборочной единицы</param>
  /// <exception cref="T:ArgumentNullException">projectArticleItem or stats</exception>
  public void FlushArticleStructureChanges(
    SectionEntity projectArticleItem,
    ArticleStructureStats stats)
  {
    if (projectArticleItem == null)
      throw new ArgumentNullException(nameof (projectArticleItem));
    if (stats == null)
      throw new ArgumentNullException(nameof (stats));
    this.DoFlushArticleStructureChanges(projectArticleItem, stats);
  }

  /// <summary>
  /// Записывает в объект CAD-системы изменения, сделанные в процессе синхронизации состава сборочной единицы.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="stats">Статистика по изменениям в составе сборочной единицы</param>
  protected virtual void DoFlushArticleStructureChanges(
    SectionEntity projectArticleItem,
    ArticleStructureStats stats)
  {
  }
}
