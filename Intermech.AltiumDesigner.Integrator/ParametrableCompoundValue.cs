// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ParametrableCompoundValue
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Tools.Integrators.Electrical;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ParametrableCompoundValue : CompoundValue<Parameter[]>
{
  public static string HandleValue(Parameter[] parameters, string parameterName)
  {
    return new ParametrableCompoundValue().Handle(parameters, parameterName);
  }

  protected override string GetPropertyValue(Parameter[] parameters, string parameterName)
  {
    Parameter parameter = Array.Find<Parameter>(parameters, (Predicate<Parameter>) (element => element.Name == parameterName));
    return parameter != null ? Convert.ToString(parameter.Value) : string.Empty;
  }
}
