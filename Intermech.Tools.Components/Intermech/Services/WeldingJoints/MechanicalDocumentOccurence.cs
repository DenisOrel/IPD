// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.MechanicalDocumentOccurence
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
/// Связка доменных объектов 'Исполнение изделия' и 'Конструкторский документ'.
/// </summary>
internal sealed class MechanicalDocumentOccurence
{
  public MechanicalDocumentOccurence()
  {
  }

  public MechanicalDocumentOccurence(
    MechanicalArticleEntity article,
    MechanicalDocumentEntity document)
  {
    if (article == null)
      throw new ArgumentNullException(nameof (article));
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    this.Article = article;
    this.Document = document;
  }

  [DBRelationStart]
  public MechanicalArticleEntity Article { get; private set; }

  [DBRelationEnd]
  public MechanicalDocumentEntity Document { get; private set; }

  [Key]
  [DBAttributeType("CAD00033-306C-11D8-B4E9-00304F19F545")]
  public long RelationId { get; set; }

  [DBAttributeType("CAD00344-306C-11D8-B4E9-00304F19F545")]
  public Guid RelationGuid { get; set; }

  [DBAttributeType("CADD95AF-306C-11D8-B4E9-00304F19F545")]
  public string CADConfigurationName { get; set; }
}
