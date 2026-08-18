// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleStructureOccurence
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Описывает вхождение изделия-компонента в состав головного изделия. Фактически, объекты этого типа описывают проектные связи состава головного изделия.
/// </summary>
public sealed class ArticleStructureOccurence
{
  private readonly Guid occurenceGuid;
  private readonly string componentKey;
  private readonly ValueBag attributes;
  private readonly SectionCollection sections;

  /// <summary>Создает описатель входжения.</summary>
  /// <param name="occurenceGuid">Идентификатор вхождения компонента в состав головного изделия</param>
  /// <param name="componentKey">Ключ изделия компонента</param>
  public ArticleStructureOccurence(Guid occurenceGuid, string componentKey)
  {
    this.occurenceGuid = occurenceGuid;
    this.componentKey = componentKey;
    this.attributes = new ValueBag();
    this.sections = new SectionCollection();
  }

  /// <summary>
  /// Возвращает идентификатор вхождения компонента в состав головного изделия.
  /// </summary>
  public Guid OccurenceGuid => this.occurenceGuid;

  /// <summary>
  /// Возвращает ключ изделия-компонента в составе головного изделия.
  /// </summary>
  public string ComponentKey => this.componentKey;

  /// <summary>
  /// Возвращает контейнер атрибутов для связи между компонентом и изделием.
  /// </summary>
  public ValueBag Attributes => this.attributes;

  /// <summary>
  /// Возвращает контейнер пользовательских секций данных для связи между компонентом и изделием.
  /// </summary>
  public SectionCollection Sections => this.sections;
}
