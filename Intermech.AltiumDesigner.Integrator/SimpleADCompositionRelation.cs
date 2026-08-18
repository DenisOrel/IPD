// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SimpleADCompositionRelation
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class SimpleADCompositionRelation : SimpleECADCompositionRelation
{
  private IIntegratorOutput _outputSvc;

  public SimpleADCompositionRelation(
    ADIntegratorSettings settings,
    ComponentsGroup group,
    ElectricalArticleCache assembly,
    IIntegratorOutput outputSvc)
    : base((ECADIntegratorSettings) settings, group, assembly)
  {
    this._outputSvc = outputSvc;
  }

  protected override MeasuredValue GetQuantity()
  {
    return MaterialQuantityHelper.IsMaterial((ADIntegratorSettings) this.settings, this.group) ? MaterialQuantityHelper.SummMaterials(((ADIntegratorSettings) this.settings).QuantityParameter, this.group, this._outputSvc) : base.GetQuantity();
  }
}
