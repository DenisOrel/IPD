// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADIntegratorModule
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADIntegratorModule : IntegratorModule<ADIntegrator>
{
  private readonly PluginContext pluginCtx;

  public ADIntegratorModule(PluginContext pluginCtx) => this.pluginCtx = pluginCtx;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.pluginCtx.IntegratorInstance = (IIntegrator) this.Integrator;
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    this.pluginCtx.IntegratorInstance = (IIntegrator) null;
  }
}
