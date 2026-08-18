// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.AppMechanicalDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public abstract class AppMechanicalDriver : MechanicalDriver
{
  private readonly IIntegrator integrator;
  private bool apiSessionIsOpen;

  public AppMechanicalDriver(IIntegrator integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  protected override void InitializeDriver()
  {
    base.InitializeDriver();
    this.OpenApplicationApiSession();
  }

  private void OpenApplicationApiSession()
  {
    IApplicationApiService service = ServiceUtils.GetService<IApplicationApiService>((object) this.integrator, false);
    if (service == null)
      return;
    service.OpenApiSession();
    this.apiSessionIsOpen = true;
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.CloseApplicationApiSession();
  }

  private void CloseApplicationApiSession()
  {
    if (!this.apiSessionIsOpen)
      return;
    this.apiSessionIsOpen = false;
    ServiceUtils.GetService<IApplicationApiService>((object) this.integrator, true).CloseApiSession();
  }

  public IIntegrator Integrator => this.integrator;
}
