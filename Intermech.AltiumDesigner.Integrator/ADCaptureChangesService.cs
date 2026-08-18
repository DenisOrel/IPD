// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADCaptureChangesService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using System.Diagnostics;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADCaptureChangesService(IIntegrator owner) : CaptureChangesService(owner)
{
  private ADMechanicalDriver driver;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.driver = new ADMechanicalDriver(this.Integrator);
  }

  protected override ICaptureChangesDriver Driver
  {
    [DebuggerStepThrough] get => (ICaptureChangesDriver) this.driver;
  }

  protected override void ConfigureDriverParameters(CaptureChangesOptions options)
  {
    base.ConfigureDriverParameters(options);
  }

  protected override void ResetDriverParameters() => base.ResetDriverParameters();
}
