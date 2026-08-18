// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ComponentsFilterSettings`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Настройки фильтрации состава</summary>
public class ComponentsFilterSettings<TVariants> : ICloneable where TVariants : ComponentsCompositionVariants, new()
{
  private Tuple<StringKey, string> _onlyElementListCondition;
  private TVariants _table;

  public ComponentsFilterSettings()
  {
    this._onlyElementListCondition = new Tuple<StringKey, string>((StringKey) string.Empty, string.Empty);
    this._table = new TVariants();
    this._table.Initialize(true);
  }

  public ComponentsFilterSettings(
    TVariants table,
    Tuple<StringKey, string> onlyElementListCondition)
  {
    this._onlyElementListCondition = onlyElementListCondition;
    this._table = table;
  }

  /// <summary>
  /// Таблица соответсвий типов компонентов в каких составах  они могут участвовать
  /// </summary>
  public TVariants Table
  {
    get => this._table;
    set => this._table = value;
  }

  public Tuple<StringKey, string> OnlyElementListCondition
  {
    get => this._onlyElementListCondition;
    set => this._onlyElementListCondition = value;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is ComponentsFilterSettings<TVariants> componentsFilterSettings))
      return base.Equals(obj);
    return componentsFilterSettings.Table.Equals((object) this.Table) && componentsFilterSettings.OnlyElementListCondition.Item1 == this.OnlyElementListCondition.Item1 && componentsFilterSettings.OnlyElementListCondition.Item2 == this.OnlyElementListCondition.Item2;
  }

  public override int GetHashCode()
  {
    return this._table.GetHashCode() << 16 /*0x10*/ ^ this._onlyElementListCondition.Item1.GetHashCode() << 8 ^ this._onlyElementListCondition.Item2.GetHashCode();
  }

  public override string ToString() => "(Настройки)";

  object ICloneable.Clone() => this.Clone();

  public object Clone()
  {
    return (object) new ComponentsFilterSettings<TVariants>((TVariants) ((ICloneable) this._table).Clone(), new Tuple<StringKey, string>(this._onlyElementListCondition.Item1, this._onlyElementListCondition.Item2));
  }
}
