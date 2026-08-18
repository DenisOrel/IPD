// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.MechanicalArticleEntity
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Experimental.Kernel.Entities;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>Доменнный объект 'Исполнение изделия'.</summary>
[DBObjectType("CAD00268-306C-11D8-B4E9-00304F19F545")]
internal sealed class MechanicalArticleEntity
{
  /// <summary>Создает объект.</summary>
  public MechanicalArticleEntity() => this.ObjectId = 0L;

  [Key]
  [DBAttributeType("CAD00029-306C-11D8-B4E9-00304F19F545")]
  public long ObjectId { get; set; }

  [DBAttributeType("CAD0001F-306C-11D8-B4E9-00304F19F545")]
  public string Designation { get; set; }

  [DBAttributeType("CAD0038A-306C-11D8-B4E9-00304F19F545")]
  public string OKPCode { get; set; }

  [DBAttributeType("CAD00020-306C-11D8-B4E9-00304F19F545")]
  public string Name { get; set; }

  [InverseEntity(typeof (MechanicalDocumentEntity))]
  [DBRelationType("CAD00154-306C-11D8-B4E9-00304F19F545")]
  public MechanicalDocumentOccurence DocumentOccurence { get; set; }

  [InverseEntity(typeof (WeldingSeamEntity))]
  [DBRelationType("CADD98C2-306C-11D8-B4E9-00304F19F545")]
  public List<WeldingSeamOccurence> WeldingSeams { get; set; }
}
