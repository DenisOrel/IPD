// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamComponentOccurence
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Experimental.Kernel.Entities;
using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Связка для доменных объектов 'Сварной шов' и 'Исполнение изделия' по связям типа "Свариваемые компоненты".
/// </summary>
internal sealed class WeldingSeamComponentOccurence
{
  public WeldingSeamComponentOccurence()
  {
  }

  public WeldingSeamComponentOccurence(
    WeldingSeamEntity weldingSeam,
    MechanicalArticleEntity article)
  {
    if (weldingSeam == null)
      throw new ArgumentNullException(nameof (weldingSeam));
    if (article == null)
      throw new ArgumentNullException(nameof (article));
    this.WeldingSeam = weldingSeam;
    this.Article = article;
  }

  /// <summary>Возвращает сварной шов.</summary>
  [DBRelationStart]
  public WeldingSeamEntity WeldingSeam { get; private set; }

  /// <summary>Возвращает компонент сварного шва.</summary>
  [DBRelationEnd]
  public MechanicalArticleEntity Article { get; private set; }

  /// <summary>Возвращает или задает 'Идентификатор связи'</summary>
  [Key]
  [DBAttributeType("CAD00033-306C-11D8-B4E9-00304F19F545")]
  public long RelationId { get; set; }

  /// <summary>
  /// Возвращает или задает 'Глобальный идентификатор связи'
  /// </summary>
  [DBAttributeType("CAD00344-306C-11D8-B4E9-00304F19F545")]
  public Guid RelationGuid { get; set; }

  /// <summary>Возвращает или задает 'Тип связи'</summary>
  [DBAttributeType("CAD00036-306C-11D8-B4E9-00304F19F545")]
  public int RelationType { get; set; }

  /// <summary>
  /// Возвращает или задает 'Группа свариваемых компонентов'
  /// </summary>
  [DBAttributeType("CADD9A0F-306C-11D8-B4E9-00304F19F545")]
  public long GroupId { get; set; }
}
