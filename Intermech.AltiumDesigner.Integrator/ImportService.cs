// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ImportService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ImportService(IIntegrator owner) : FileImportService(owner)
{
  private ADMechanicalDriver captureDriver;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.captureDriver = new ADMechanicalDriver(this.Integrator);
  }

  protected override ICaptureChangesDriver GetCaptureChangesDriver()
  {
    return (ICaptureChangesDriver) this.captureDriver;
  }

  protected override void SetCaptureChangesParameters(bool extendedImport)
  {
    base.SetCaptureChangesParameters(extendedImport);
    if (!extendedImport)
      return;
    this.captureDriver.UpdateArticles = true;
    this.captureDriver.RecalculateMass = false;
  }

  protected override void ResetCaptureChangesParameters()
  {
    base.ResetCaptureChangesParameters();
    this.captureDriver.UpdateArticles = false;
    this.captureDriver.RecalculateMass = false;
  }
}
