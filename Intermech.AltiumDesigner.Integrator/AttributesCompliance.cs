// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.AttributesCompliance
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

[DefaultProperty("DBAttributeName")]
internal sealed class AttributesCompliance : ICloneable
{
  private string _dbAttributeName;
  private string _cadAttributeName;

  [DisplayName("Атрибут базы данных")]
  [Description("Наименование атрибута базы данных IPS")]
  public string DBAttributeName
  {
    get => this._dbAttributeName;
    set => this._dbAttributeName = value;
  }

  [DisplayName("Параметр ECAD")]
  [Description("Наименование параметра соответствующего элемента в ECAD")]
  public string CADAttributeName
  {
    get => this._cadAttributeName;
    set => this._cadAttributeName = value;
  }

  public AttributesCompliance Clone()
  {
    return new AttributesCompliance()
    {
      _dbAttributeName = this._dbAttributeName,
      _cadAttributeName = this._cadAttributeName
    };
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override string ToString() => "Соответствие атрибутов";

  public override int GetHashCode()
  {
    int hashCode = 0;
    if (this._dbAttributeName != null)
      hashCode ^= this._dbAttributeName.GetHashCode();
    if (this._cadAttributeName != null)
      hashCode ^= this._cadAttributeName.GetHashCode();
    return hashCode;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is AttributesCompliance attributesCompliance))
      return base.Equals(obj);
    return !(attributesCompliance._dbAttributeName != this._dbAttributeName) && !(attributesCompliance._cadAttributeName != this._cadAttributeName);
  }
}
