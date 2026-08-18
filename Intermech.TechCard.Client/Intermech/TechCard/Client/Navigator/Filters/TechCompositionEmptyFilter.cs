// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Filters.TechCompositionEmptyFilter
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Parts;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Filters;

/// <summary>
/// Интерфейс для фильтрации отображаемого в дескрипторе состава по Condition
/// </summary>
public sealed class TechCompositionEmptyFilter : TechCompositionFilter
{
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
    return true;
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
    return (INodeQuery) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj) => obj is TechCompositionEmptyFilter;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();
}
