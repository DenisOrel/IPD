// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADEmbedAttributesService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using System.Diagnostics;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADEmbedAttributesService(IIntegrator owner) : EmbedAttributesService(owner)
{
  private DocumentEmbedAttributesDriver driver;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.driver = new DocumentEmbedAttributesDriver(this.Integrator);
  }

  protected override IEmbedAttributesDriver Driver
  {
    [DebuggerStepThrough] get => (IEmbedAttributesDriver) this.driver;
  }

  protected override void ConfigureDriverParameters() => base.ConfigureDriverParameters();

  protected override void ResetDriverParameters() => base.ResetDriverParameters();
}
