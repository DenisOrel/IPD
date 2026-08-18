// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ImbaseAttributeArticleLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует декодер исходных данных для алгоритмов поиска объекта по значению атрибута записи Imbase.
/// </summary>
public sealed class ImbaseAttributeArticleLocatorData : IImbaseAttributeLocatorData
{
  private readonly SectionEntity articleItem;
  private readonly int objectTypeId;
  private readonly LocalId<int> attribute;

  /// <summary>Создает объект.</summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <param name="objectTypeId">Идентификатор типа объекта, создаваемого по записи Imbase</param>
  /// <param name="attribute">Описатель атрибута</param>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public ImbaseAttributeArticleLocatorData(
    SectionEntity articleItem,
    int objectTypeId,
    LocalId<int> attribute)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (objectTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта.", nameof (objectTypeId));
    if (attribute == null)
      throw new ArgumentNullException(nameof (attribute));
    this.articleItem = articleItem;
    this.objectTypeId = objectTypeId;
    this.attribute = attribute;
  }

  /// <summary>
  /// Возвращает идентификатор типа объекта, создаваемого по записи Imbase.
  /// </summary>
  public int ObjectTypeId => this.objectTypeId;

  /// <summary>Возвращает идентификатор атрибута записи Imbase.</summary>
  public int ImbaseAttributeId => this.attribute.Id;

  /// <summary>Возвращает значение атрибута записи Imbase.</summary>
  public string ImbaseAttributeValue
  {
    get => this.GetWorkAttributes().Read<string>((StringKey) this.attribute.Name, string.Empty);
  }

  private ValueBag GetWorkAttributes()
  {
    return this.articleItem.Sections.Get<AttributesSection>().WorkingSet;
  }
}
