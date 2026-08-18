// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadApplicationLauncherService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.ControlFlow;
using Intermech.Interfaces;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadApplicationLauncherService(IIntegrator owner) : 
  ApplicationLauncherService(owner),
  IApplicationLauncherService
{
  protected override void DoLaunchApplication()
  {
    AcadSetupSettings appSetupSettings = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this.Integrator, true).GetAppSetupSettings();
    using (new DynamicScope())
    {
      IntegratorVars.NakedApiSessions.Declare(true);
      using (AcadApiSession acadApiSession = new AcadApiSession(this.Integrator))
      {
        ICadProxy application = acadApiSession.Application;
        acadApiSession.ApiOperations.ReconfigureApplication(application, appSetupSettings);
        application.SwitchToApp();
      }
    }
  }
}
