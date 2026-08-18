// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElectricalArticleStructureService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Сервис для работы с составом изделия. Используется при сохранении изменений в конструкторских документах для синхронизации проектных связей между изделиями.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Объект драйвера</param>
/// <param name="driverContext">Объект рабочего контекста драйвера</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
public sealed class ElectricalArticleStructureService(
  AppMechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : ArticleStructureService((MechanicalDriver) driver, driverContext)
{
  private AppMechanicalDriver MGDriver
  {
    [DebuggerStepThrough] get => (AppMechanicalDriver) this.Driver;
  }

  /// <summary>
  /// Проверяет, является ли указанное изделие сборочной единицей, т.е. изделием с конструкторским составом.
  /// Это метод используется для определения изделий, для которых требуется выполнить синхронизацию состава.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <param name="documentItem">Сущность конструкторского документа, по которому выпускается изделие. Значение параметра может быть null, если источником изделия является не документ, а что-то другое</param>
  /// <returns>true - указанное изделие является сборочной единицей и требует синхронизации состава, false - изделие не требует синхронизации состава</returns>
  protected override bool IsProjectArticle(SectionEntity articleItem, SectionEntity documentItem)
  {
    ElectricalArticleCache electricalArticleCache = articleItem.Sections.Get<ElectricalArticleCache>();
    return electricalArticleCache.ArticleType == ArticleTypes.Assembly || electricalArticleCache.ArticleType == ArticleTypes.VirtualAssembly || base.IsProjectArticle(articleItem, documentItem);
  }

  /// <summary>
  /// Возвращает состав указанной сборочной единицы в виде коллекции вхождений изделий-компонентов.
  /// Каждое вхождение компонента соответствует одной проектной связи с компонентом в базе IPS.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <returns>Коллекция вхождений изделий-компонентов</returns>
  protected override List<ArticleStructureOccurence> DoReadArticleStructure(
    SectionEntity projectArticleItem)
  {
    List<ArticleStructureOccurence> list = base.DoReadArticleStructure(projectArticleItem);
    ElectricalArticleCache electricalArticleCache = projectArticleItem.Sections.Get<ElectricalArticleCache>();
    string name = MetaDataHelper.GetAttributeType(new Guid("cad01478-306c-11d8-b4e9-00304f19f545")).Name;
    if ((electricalArticleCache.ArticleType == ArticleTypes.Assembly || electricalArticleCache.ArticleType == ArticleTypes.VirtualAssembly) && electricalArticleCache.Composition != null && electricalArticleCache.Composition.Count > 0)
    {
      CollectionUtils.EnsureNewItemsCapacity<ArticleStructureOccurence>(list, electricalArticleCache.Composition.Count);
      foreach (CompositionItem compositionItem in electricalArticleCache.Composition)
      {
        ArticleStructureOccurence structureOccurence = new ArticleStructureOccurence(compositionItem.PosGuid, compositionItem.ID);
        if (compositionItem.AdditionalAttributes != null)
        {
          foreach (Tuple<StringKey, object> additionalAttribute in compositionItem.AdditionalAttributes)
            structureOccurence.Attributes.Add(additionalAttribute.Item1, additionalAttribute.Item2);
          structureOccurence.Attributes.SetFlagForAll(NamedFlags.ReadOnly);
        }
        list.Add(structureOccurence);
      }
    }
    return list;
  }
}
