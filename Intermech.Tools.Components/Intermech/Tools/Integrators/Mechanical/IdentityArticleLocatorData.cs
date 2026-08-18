// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IdentityArticleLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует декодер исходных данных для алгоритмов поиска объекта по обозначению, коду ОКП и наименованию.
/// </summary>
public sealed class IdentityArticleLocatorData : IIdentityArticleLocatorData
{
  private readonly SectionEntity articleItem;

  /// <summary>Создает объект.</summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IdentityArticleLocatorData(SectionEntity articleItem)
  {
    this.articleItem = articleItem != null ? articleItem : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>Возвращает обозначение объекта.</summary>
  /// <returns>Обозначение объекта</returns>
  public string GetDesignation()
  {
    return this.GetWorkAttributes().Read<string>((StringKey) IDCache.Default.Designation.Text, string.Empty);
  }

  /// <summary>Возвращает код ОКП объекта.</summary>
  /// <returns>Код ОКП объекта</returns>
  public string GetOKPCode()
  {
    return this.GetWorkAttributes().Read<string>((StringKey) IDCache.Default.OKPCode.Text, string.Empty);
  }

  /// <summary>Возвращает наименование объекта.</summary>
  /// <returns>Наименование объекта</returns>
  public string GetName()
  {
    return this.GetWorkAttributes().Read<string>((StringKey) IDCache.Default.Name.Text, string.Empty);
  }

  private ValueBag GetWorkAttributes()
  {
    return this.articleItem.Sections.Get<AttributesSection>().WorkingSet;
  }
}
