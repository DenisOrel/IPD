// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFIntegratorPackage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFIntegratorPackage : IOCBasedPackage
{
  private IIntegratorRegistry integratorRegistry;
  private PDFIntegrator integrator;

  public PDFIntegratorPackage(
    IOCBasedPackageParameters createParameters,
    IIntegratorRegistry integratorRegistry)
    : base(createParameters, PDFConsts.IntegratorName)
  {
    this.integratorRegistry = integratorRegistry != null ? integratorRegistry : throw new ArgumentNullException(nameof (integratorRegistry));
  }

  protected override void DoLoad()
  {
    base.DoLoad();
    this.integrator = new PDFIntegrator();
    this.integrator.Initialize();
    this.integratorRegistry.RegisterIntegrator((IIntegrator) this.integrator);
  }

  protected override void DoUnload()
  {
    if (this.integrator != null)
    {
      this.integratorRegistry.UnregisterIntgerator((IIntegrator) this.integrator);
      this.integrator = (PDFIntegrator) null;
    }
    base.DoUnload();
  }
}
