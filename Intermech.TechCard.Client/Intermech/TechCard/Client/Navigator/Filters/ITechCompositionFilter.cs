// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Filters.ITechCompositionFilter
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Parts;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Filters;

/// <summary>
/// Интерфейс для фильтрации отображаемого в дескрипторе состава
/// </summary>
public interface ITechCompositionFilter
{
  /// <summary>Update filter's role if is needed</summary>
  /// <param name="role"></param>
  void UpdateRelatedObjectsRole(RelatedObjectsRole role);

  /// <summary>Return true if need to call base method to get IQuery</summary>
  /// <remarks>Внимание! Conditions can be modified !</remarks>
  /// <param name="nodePart"></param>
  /// <param name="conditions"></param>
  /// <returns></returns>
  bool CallBaseMethod(TechCompositionPart nodePart, ref ConditionStructure[] conditions);

  /// <summary>Get node's query according filtration rule</summary>
  /// <param name="nodePart"></param>
  /// <param name="conditions"></param>
  /// <returns></returns>
  INodeQuery GetCustomQuery(TechCompositionPart nodePart, ConditionStructure[] conditions);

  /// <summary>Интерфейс настроек фильтрации для RelatedObjectQuery</summary>
  IRelatedObjectQueryFilterMode QueryFilter { get; }
}
