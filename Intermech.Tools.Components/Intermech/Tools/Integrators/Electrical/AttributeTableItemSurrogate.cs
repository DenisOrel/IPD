// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.AttributeTableItemSurrogate
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Таблица соотвествий параметров компонентов схемы или платы атрибутам базы данных IPS
/// </summary>
[DefaultProperty("DBAttributeName")]
public sealed class AttributeTableItemSurrogate : ICloneable
{
  private string _dbAttributeName;
  private string _cadAttributeName;
  private bool _obligatory;

  /// <summary>Атрибут базы данных</summary>
  [DisplayName("Атрибут базы данных")]
  [Description("Наименование атрибута базы данных IPS")]
  public string DBAttributeName
  {
    get => this._dbAttributeName;
    set => this._dbAttributeName = value;
  }

  /// <summary>Параметр компонента схемы</summary>
  [DisplayName("Параметр ECAD")]
  [Description("Наименование параметра соответствующего элемента в ECAD")]
  public string CADAttributeName
  {
    get => this._cadAttributeName;
    set => this._cadAttributeName = value;
  }

  /// <summary>Обязательность?</summary>
  [Browsable(false)]
  public bool Obligatory
  {
    get => this._obligatory;
    set => this._obligatory = value;
  }

  public AttributeTableItemSurrogate Clone()
  {
    return new AttributeTableItemSurrogate()
    {
      _dbAttributeName = this._dbAttributeName,
      _cadAttributeName = this._cadAttributeName,
      _obligatory = this._obligatory
    };
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override string ToString() => "Соответствия атрибутов";

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
    if (!(obj is AttributeTableItemSurrogate tableItemSurrogate))
      return base.Equals(obj);
    return !(tableItemSurrogate._dbAttributeName != this._dbAttributeName) && !(tableItemSurrogate._cadAttributeName != this._cadAttributeName);
  }
}
