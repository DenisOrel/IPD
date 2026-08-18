// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadApiSession
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.Tools.Integrators;
using System.Diagnostics;

#nullable disable
namespace Intermech.Cadmech.Integrator;

public sealed class AcadApiSession : ApplicationApiSession<ICadProxy>
{
  private AcadApiOperations apiOperations;

  public AcadApiSession(IIntegrator integrator)
    : base(integrator)
  {
  }

  public AcadApiSession(IApplicationApiService apiService)
    : base(apiService)
  {
  }

  internal AcadApiOperations ApiOperations
  {
    [DebuggerStepThrough] get
    {
      this.CheckNotDisposed();
      if (this.apiOperations == null)
        this.apiOperations = this.CreateApiOperations();
      return this.apiOperations;
    }
  }

  private AcadApiOperations CreateApiOperations()
  {
    return new AcadApiOperations(((IntegratorService) this.ApplicationApiService).Integrator, this.ApplicationApiService);
  }
}
