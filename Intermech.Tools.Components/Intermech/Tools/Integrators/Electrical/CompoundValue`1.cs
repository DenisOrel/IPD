// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.CompoundValue`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Составное значение из нескольких параметров</summary>
public abstract class CompoundValue<TParametrable>
{
  /// <summary>Обработать значение</summary>
  /// <returns></returns>
  public string Handle(TParametrable parameters, string parameterName)
  {
    foreach (Match match in new Regex("%[^%]{1,}%").Matches(parameterName))
      parameterName = parameterName.Replace(match.Value, this.GetPropertyValue(parameters, match.Value.Replace("%", string.Empty)));
    return parameterName;
  }

  protected abstract string GetPropertyValue(TParametrable parameters, string parameterName);
}
