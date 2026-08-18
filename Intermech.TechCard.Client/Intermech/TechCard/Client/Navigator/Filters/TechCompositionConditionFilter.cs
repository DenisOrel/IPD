// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Filters.TechCompositionConditionFilter
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.TechCard.Client.Navigator.Parts;
using Intermech.TechCard.Client.Navigator.Queries;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Filters;

/// <summary>
/// Интерфейс для фильтрации отображаемого в дескрипторе состава по Condition
/// </summary>
public sealed class TechCompositionConditionFilter : TechCompositionFilter
{
  /// <summary>Composition's info</summary>
  private readonly IList<ConditionStructure> _conditions;

  /// <summary>Конструктор</summary>
  /// <param name="conditions">Условия фильтрации</param>
  public TechCompositionConditionFilter([NotNull] IEnumerable<ConditionStructure> conditions)
  {
    this._conditions = (IList<ConditionStructure>) conditions.ToList<ConditionStructure>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="role"></param>
  public override void UpdateRelatedObjectsRole(RelatedObjectsRole role)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodePart"></param>
  /// <param name="conditions"></param>
  /// <returns></returns>
  public override bool CallBaseMethod(
    TechCompositionPart nodePart,
    ref ConditionStructure[] conditions)
  {
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodePart"></param>
  /// <param name="conditions"></param>
  /// <returns></returns>
  public override INodeQuery GetCustomQuery(
    TechCompositionPart nodePart,
    ConditionStructure[] conditions)
  {
    conditions = ConditionStructure.Join(conditions, this._conditions.ToArray<ConditionStructure>());
    TechCompositionQuery customQuery = new TechCompositionQuery((INodeQuerySupport) nodePart, nodePart.ObjID, nodePart.ObjTypeID, nodePart.ObjRole, nodePart.RelationTypeID, conditions, nodePart.FiltrationOwnerID, nodePart.Contexts);
    customQuery.Services = nodePart.Services;
    return (INodeQuery) customQuery;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (!(obj is TechCompositionConditionFilter compositionConditionFilter))
      return false;
    int num = this._conditions.Count<ConditionStructure>();
    if (num != compositionConditionFilter._conditions.Count<ConditionStructure>())
      return false;
    for (int index = 0; index < num; ++index)
    {
      if (!this._conditions[index].Equals((object) compositionConditionFilter._conditions[index]))
        return false;
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => this._conditions.Count.GetHashCode();
}
