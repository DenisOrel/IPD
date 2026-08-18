// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.TypedParametersContainer`1
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class TypedParametersContainer<TType>(TType instance) : ParametersContainer((IParametrable) instance)
  where TType : IParametrable
{
  public TType Instance => (TType) this.Parametrable;

  public void AddNewParameter(Parameter parameter) => this.Parametrable.AddNewParameter(parameter);

  public void SetParameterValue(string name, Type type, object parameterValue)
  {
    this.Parametrable.SetParameterValue(name, type, parameterValue);
  }
}
