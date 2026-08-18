// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadCaptureChangesService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using System.Diagnostics;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadCaptureChangesService : CaptureChangesService
{
  private readonly ChangesDriver driver;

  public AcadCaptureChangesService(IIntegrator owner)
    : base(owner)
  {
    this.driver = new ChangesDriver(owner);
  }

  protected override ICaptureChangesDriver Driver
  {
    [DebuggerStepThrough] get => (ICaptureChangesDriver) this.driver;
  }

  protected override void ConfigureDriverParameters(CaptureChangesOptions options)
  {
    base.ConfigureDriverParameters(options);
    this.driver.ApplyTypicalSettings();
  }

  protected override void ResetDriverParameters()
  {
    base.ResetDriverParameters();
    this.driver.ProcessingSchemas = (DwgDriverProcessingSchemas) 0;
    this.driver.MechanicalDocuments.ArticleEmitter = (IDwgArticleEmitter) null;
  }
}
