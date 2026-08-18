// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadLaunchActionService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadLaunchActionService(IIntegrator owner) : Intermech.Tools.Integrators.LaunchActionService(owner)
{
  protected override void OpenDocumentFileFromDisk(LaunchParams launchParams)
  {
    using (AcadApiSession acadApiSession = new AcadApiSession(this.Integrator))
    {
      ICadProxy application = acadApiSession.Application;
      application.SwitchToApp();
      application.OpenDocument(launchParams.ResultFilePath).Activate();
    }
  }
}
