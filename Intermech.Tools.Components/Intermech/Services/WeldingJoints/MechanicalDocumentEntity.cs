// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.MechanicalDocumentEntity
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Experimental.Kernel.Entities;
using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>Доменнный объект 'Конструкторский документ'.</summary>
[DBObjectType("CAD0057F-306C-11D8-B4E9-00304F19F545")]
internal sealed class MechanicalDocumentEntity
{
  /// <summary>Создает объект.</summary>
  public MechanicalDocumentEntity() => this.ObjectId = 0L;

  [Key]
  [DBAttributeType("CAD00029-306C-11D8-B4E9-00304F19F545")]
  public long ObjectId { get; set; }

  [DBAttributeType("CAD00800-306C-11D8-B4E9-00304F19F545")]
  public Guid GUID { get; set; }

  [DBAttributeType("CAD0001F-306C-11D8-B4E9-00304F19F545")]
  public string Designation { get; set; }

  [DBAttributeType("CAD00020-306C-11D8-B4E9-00304F19F545")]
  public string Name { get; set; }

  public string CreateWeldingSeamExternalKey(Guid weldingSeamAnchorGuid, bool isOnBackSide)
  {
    if (this.GUID == Guid.Empty)
      throw new InvalidOperationException("Невозможно создать внешний ключ для сварного шва, если глобальный идентификатор документа не задан.");
    string upperInvariant = $"{weldingSeamAnchorGuid.ToString("N")}_{this.GUID.ToString("N")}".ToUpperInvariant();
    if (isOnBackSide)
      upperInvariant += "_BACKSIDE";
    return upperInvariant;
  }
}
