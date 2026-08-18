// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElectricalComponentCompoundValue
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public class ElectricalComponentCompoundValue : CompoundValue<IElectricalComponent>
{
  protected override string GetPropertyValue(IElectricalComponent parameters, string parameterName)
  {
    return Convert.ToString(parameters.GetPropertyValue(parameterName));
  }

  public static string HandleValue(IElectricalComponent parameters, string parameterName)
  {
    return new ElectricalComponentCompoundValue().Handle(parameters, parameterName);
  }
}
