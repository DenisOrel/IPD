// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADVariant
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADVariant(IVariant variant) : 
  TypedParametersContainer<IVariant>(variant),
  IVariant,
  IParametrable,
  IValueBagContainer,
  IIdentification,
  IDisposable
{
  public string Description => this.Instance.Description;

  public List<IVariation> Variations
  {
    get
    {
      List<IVariation> variations1 = new List<IVariation>();
      List<IVariation> variations2 = this.Instance.Variations;
      if (variations2 != null && variations2.Count > 0)
      {
        foreach (IVariation variation in variations2)
          variations1.Add((IVariation) new ADVariation(variation));
      }
      return variations1;
    }
  }
}
