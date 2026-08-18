// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADComponentProperty
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Tools.Integrators.Electrical;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADComponentProperty : IComponentProperty
{
  private Parameter _adParameter;

  public ADComponentProperty(Parameter adParameter) => this._adParameter = adParameter;

  public object Value
  {
    get => this._adParameter.Value;
    set => this._adParameter.Value = value;
  }
}
