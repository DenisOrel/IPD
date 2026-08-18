// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.Integrator.CadTechRequirementsService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator.Integrator;

internal class CadTechRequirementsService(IIntegrator owner) : TechRequirementsService(owner)
{
  public override IDisposable CreateApiSession()
  {
    return (IDisposable) new AcadApiSession(this.Integrator);
  }

  public override IIMTextDocumentProvider GetIMTextDocumentProvider(
    long documentId,
    string documentFilePath,
    IDisposable apiSession)
  {
    if (string.IsNullOrEmpty(documentFilePath))
      throw new ArgumentNullException(nameof (documentFilePath));
    if (apiSession == null)
      throw new ArgumentNullException(nameof (apiSession));
    CadmechRootProxy cadmechRootProxy = CadmechRootProxy.Create(true);
    return (IIMTextDocumentProvider) new CadIMTextDocumentProvider(documentId, documentFilePath, cadmechRootProxy);
  }

  public override bool CanGetTechRequirements(int documentTypeID)
  {
    AcadIntegratorSettingsService service;
    if (this.Integrator.TryGetService<AcadIntegratorSettingsService>(out service))
    {
      AcadIntegratorSettings settings = service.GetSettings();
      if (settings.MechanicalSettings.IsEnabled && settings.MechanicalSettings.AssemblyDrawings.FindIndex((Predicate<DrawingTypeSettings>) (x => x.DocumentType.Id == documentTypeID)) != -1)
        return true;
    }
    return false;
  }
}
