// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADVariation
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADVariation : 
  TypedParametersContainer<IVariation>,
  IVariation,
  ISchComponent,
  IParametrable,
  IValueBagContainer,
  IIdentification,
  IDisposable
{
  public ADVariation(IVariation variation)
    : base(variation)
  {
    this.VariationKind = variation.VariationKind;
    this.AlternatePart = variation.AlternatePart;
    this.VariationCount = variation.VariationCount;
    this.DesignatorText = variation.DesignatorText;
  }

  public int VariationKind { get; }

  public string AlternatePart { get; }

  public int VariationCount { get; }

  public string DesignatorText { get; }
}
