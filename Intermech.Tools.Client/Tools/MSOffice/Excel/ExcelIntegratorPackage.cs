// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelIntegratorPackage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Files;
using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class ExcelIntegratorPackage : IOCBasedPackage
{
  private IIntegratorRegistry integratorRegistry;
  private IOpenFilesService openFilesService;
  private ExcelIntegrator integrator;
  private OpenFilesHandler openFilesHandler;

  public ExcelIntegratorPackage(
    IOCBasedPackageParameters createParameters,
    IIntegratorRegistry integratorRegistry,
    IOpenFilesService openFilesService)
    : base(createParameters, "Интегратор с Microsoft Excel")
  {
    if (integratorRegistry == null)
      throw new ArgumentNullException(nameof (integratorRegistry));
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    this.integratorRegistry = integratorRegistry;
    this.openFilesService = openFilesService;
  }

  protected override void DoLoad()
  {
    base.DoLoad();
    this.integrator = new ExcelIntegrator();
    this.integrator.Initialize();
    this.integratorRegistry.RegisterIntegrator((IIntegrator) this.integrator);
    this.openFilesHandler = new OpenFilesHandler((IIntegrator) this.integrator);
    this.openFilesService.RegisterExtension((IOpenFilesServiceExtension) this.openFilesHandler);
  }

  protected override void DoUnload()
  {
    if (this.openFilesHandler != null)
    {
      this.openFilesService.UnregisterExtension((IOpenFilesServiceExtension) this.openFilesHandler);
      this.openFilesHandler = (OpenFilesHandler) null;
    }
    if (this.integrator != null)
    {
      this.integratorRegistry.UnregisterIntgerator((IIntegrator) this.integrator);
      this.integrator = (ExcelIntegrator) null;
    }
    base.DoUnload();
  }
}
