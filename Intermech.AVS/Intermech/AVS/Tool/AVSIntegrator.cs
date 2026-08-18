// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Tool.AVSIntegrator
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Tool;

public sealed class AVSIntegrator : NonConfigurableIntegrator
{
  internal static readonly string IntegratorName = "Интегратор с AVS";
  internal static readonly string ApplicationName = "AVS";
  internal static readonly Guid IntegratorId = new Guid("42E3B681-92FD-410D-8EB0-676287A3AB43");
  private static readonly Guid SpecificationDocumentType = new Guid("cad00133-306c-11d8-b4e9-00304f19f545");

  public override Guid Id => AVSIntegrator.IntegratorId;

  public override string DisplayName => AVSIntegrator.IntegratorName;

  protected override bool HasSpecialFileManagement() => true;

  protected override ICollection<Guid> GetDocumentTypes()
  {
    ICollection<Guid> documentTypes = base.GetDocumentTypes();
    documentTypes.Add(AVSIntegrator.SpecificationDocumentType);
    return documentTypes;
  }
}
