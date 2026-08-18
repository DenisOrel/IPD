// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ParametersContainer
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class ParametersContainer : IValueBagContainer, IDisposable
{
  private readonly ADClientSponsor _sponsor;

  public ParametersContainer(IParametrable parametrable)
  {
    this.Parametrable = parametrable;
    this.InternalId = this.GetInternalId();
    this._sponsor = new ADClientSponsor();
    this._sponsor.Register((object) this.Parametrable);
  }

  protected virtual string GetInternalId() => this.Parametrable.InternalId;

  public string InternalId { get; }

  public virtual Parameter[] Parameters
  {
    get
    {
      return ((IEnumerable<Parameter>) this.Parametrable.Parameters).Where<Parameter>((Func<Parameter, bool>) (x => !string.IsNullOrEmpty(x.Name))).ToArray<Parameter>();
    }
    set => this.Parametrable.Parameters = value;
  }

  public IParametrable Parametrable { get; }

  public void Dispose() => this._sponsor.Dispose();
}
