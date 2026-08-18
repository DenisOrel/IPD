// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamOccurence
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Experimental.Kernel.Entities;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Связка доменных объектов 'Исполнение изделия' и 'Сварной шов'.
/// </summary>
internal sealed class WeldingSeamOccurence
{
  public WeldingSeamOccurence()
  {
  }

  public WeldingSeamOccurence(MechanicalArticleEntity article, WeldingSeamEntity weldingSeam)
  {
    if (article == null)
      throw new ArgumentNullException(nameof (article));
    if (weldingSeam == null)
      throw new ArgumentNullException(nameof (weldingSeam));
    this.Article = article;
    this.WeldingSeam = weldingSeam;
  }

  [DBRelationStart]
  public MechanicalArticleEntity Article { get; private set; }

  [DBRelationEnd]
  public WeldingSeamEntity WeldingSeam { get; private set; }

  [Key]
  [DBAttributeType("CAD00033-306C-11D8-B4E9-00304F19F545")]
  public long RelationId { get; set; }

  [DBAttributeType("CAD00344-306C-11D8-B4E9-00304F19F545")]
  public Guid RelationGuid { get; set; }

  [DBAttributeType("CAD00036-306C-11D8-B4E9-00304F19F545")]
  public int RelationType { get; set; }

  [DBAttributeType("CAD00267-306C-11D8-B4E9-00304F19F545")]
  public MeasuredValue Count { get; set; }
}
