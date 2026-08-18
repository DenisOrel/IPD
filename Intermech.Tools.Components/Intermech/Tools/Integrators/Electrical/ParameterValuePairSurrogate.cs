// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ParameterValuePairSurrogate
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public sealed class ParameterValuePairSurrogate : ICloneable
{
  private string _parameterName;
  private string _parameterValue;

  /// <summary>Параметр компонента схемы</summary>
  [DisplayName("Параметр компонента схемы")]
  [Description("Наименование параметра компонента схемы в редакторе")]
  public string ParameterName
  {
    get => this._parameterName;
    set => this._parameterName = value;
  }

  /// <summary>Значение параметра</summary>
  [DisplayName("Значение параметра")]
  [Description("Значение параметра компонента схемы в редакторе")]
  public string ParameterValue
  {
    get => this._parameterValue;
    set => this._parameterValue = value;
  }

  public ParameterValuePairSurrogate Clone()
  {
    return new ParameterValuePairSurrogate()
    {
      ParameterName = this.ParameterName,
      ParameterValue = this.ParameterValue
    };
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override string ToString() => "Параметр и его значение";

  public override int GetHashCode()
  {
    int hashCode = 0;
    if (this._parameterName != null)
      hashCode ^= this._parameterName.GetHashCode();
    if (this._parameterValue != null)
      hashCode ^= this._parameterValue.GetHashCode();
    return hashCode;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is ParameterValuePairSurrogate valuePairSurrogate))
      return base.Equals(obj);
    return !(valuePairSurrogate.ParameterName != this.ParameterName) && !(valuePairSurrogate.ParameterValue != this.ParameterValue);
  }
}
