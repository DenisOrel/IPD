// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.NotImportedVariantSettingsSurrogate
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators.Electrical;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class NotImportedVariantSettingsSurrogate : ICloneable, IListParamValuesSettings
{
  [DisplayName("Имя параметра")]
  [Description("Имя параметра в свойствах варианта проекта")]
  public string ParameterName { get; set; }

  [DisplayName("Значение параметра")]
  [Description("Значение параметра, при котором по варианту не будет создаваться исполнение.")]
  public string ParameterValue { get; set; }

  public NotImportedVariantSettingsSurrogate Clone()
  {
    return new NotImportedVariantSettingsSurrogate()
    {
      ParameterValue = this.ParameterValue,
      ParameterName = this.ParameterName
    };
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override string ToString() => "Настройка";

  public override int GetHashCode()
  {
    int hashCode = 0;
    if (this.ParameterValue != null)
      hashCode ^= this.ParameterValue.GetHashCode();
    if (this.ParameterName != null)
      hashCode ^= this.ParameterName.GetHashCode();
    return hashCode;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is NotImportedVariantSettingsSurrogate settingsSurrogate))
      return base.Equals(obj);
    return !(settingsSurrogate.ParameterValue != this.ParameterValue) && !(settingsSurrogate.ParameterName != this.ParameterName);
  }
}
