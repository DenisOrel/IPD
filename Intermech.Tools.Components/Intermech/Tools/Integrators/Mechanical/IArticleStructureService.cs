// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticleStructureService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Сервис для работы с составом изделия. Используется при сохранении изменений в конструкторских документах для синхронизации проектных связей между изделиями.
/// </summary>
public interface IArticleStructureService
{
  /// <summary>
  /// Проверяет, является ли указанное изделие сборочной единицей, т.е. изделием с конструкторским составом.
  /// Это метод используется для определения изделий, для которых требуется выполнить синхронизацию состава.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>true - указанное изделие является сборочной единицей и требует синхронизации состава, false - изделие не требует синхронизации состава</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  bool IsProjectArticle(SectionEntity articleItem);

  /// <summary>
  /// Возвращает состав указанной сборочной единицы в виде коллекции вхождений изделий-компонентов.
  /// Каждое вхождение компонента соответствует одной проектной связи с компонентом в базе IPS.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <returns>Коллекция вхождений изделий-компонентов</returns>
  /// <exception cref="T:ArgumentNullException">projectArticleItem</exception>
  List<ArticleStructureOccurence> ReadArticleStructure(SectionEntity projectArticleItem);

  /// <summary>Реализует поиск сущности для изделия-компонента.</summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="componentOccurence">Вхождение изделия-компонента</param>
  /// <returns>Найденная сущность для изделия компонента или null</returns>
  /// <exception cref="T:ArgumentNullException">projectArticleItem or componentOccurence</exception>
  SectionEntity FindArticleComponent(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence);

  /// <summary>Возвращает путь к файлу, в котором описан компонент.</summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="componentOccurence">Вхождение изделия-компонента</param>
  /// <returns>Путь к файлу или null</returns>
  string TryGetArticleComponentFile(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence);

  /// <summary>
  /// Записывает в объект CAD-системы изменения, сделанные в процессе синхронизации состава сборочной единицы.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="stats">Статистика по изменениям в составе сборочной единицы</param>
  /// <exception cref="T:ArgumentNullException">projectArticleItem or stats</exception>
  void FlushArticleStructureChanges(SectionEntity projectArticleItem, ArticleStructureStats stats);
}
