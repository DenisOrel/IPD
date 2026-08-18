// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.ConditionClass
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>
/// 
/// </summary>
[Serializable]
public class ConditionClass
{
  public string Alias = string.Empty;
  public RelationalOperators RelOperator;
  public object Value;

  /// <summary>Конструктор.</summary>
  /// <param name="alias">Обобщенное наименование</param>
  /// <param name="relOperator">Условие</param>
  /// <param name="value">Значение</param>
  public ConditionClass(string alias, RelationalOperators relOperator, object value)
  {
    this.Alias = alias;
    this.RelOperator = relOperator;
    this.Value = value;
  }
}
