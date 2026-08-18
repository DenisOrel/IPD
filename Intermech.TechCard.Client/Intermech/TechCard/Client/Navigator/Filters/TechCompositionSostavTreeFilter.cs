// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Filters.TechCompositionSostavTreeFilter
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.TechCard.Client.Navigator.Parts;
using Intermech.TechCard.Client.Navigator.Queries;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Filters;

/// <summary>
/// Интерфейс для фильтрации отображаемого в дескрипторе состава по SostavTreeItem
/// </summary>
public sealed class TechCompositionSostavTreeFilter : TechCompositionFilter
{
  /// <summary>Object's role</summary>
  private readonly RelatedObjectsRole _objectRole;
  /// <summary>Composition's list</summary>
  private readonly IList<TechCardUtils.SostavTreeItem> _sostavList;
  /// <summary>
  /// Tech objects composition's cache list
  /// where key - project version id, value - child list
  /// </summary>
  private IDictionary<long, IList<TechCardUtils.SostavTreeItem>> _techComposition;

  /// <summary>Fill tech composition cache</summary>
  /// <param name="objectRole"></param>
  /// <param name="sostavList"></param>
  private void SetCompositionList(
    RelatedObjectsRole objectRole,
    IList<TechCardUtils.SostavTreeItem> sostavList)
  {
    Dictionary<long, IList<TechCardUtils.SostavTreeItem>> dictionary = new Dictionary<long, IList<TechCardUtils.SostavTreeItem>>();
    if (sostavList != null && sostavList.Count != 0)
    {
      foreach (TechCardUtils.SostavTreeItem sostav in (IEnumerable<TechCardUtils.SostavTreeItem>) sostavList)
      {
        long key = objectRole == RelatedObjectsRole.Composition ? sostav.ProjID : sostav.PartID;
        IList<TechCardUtils.SostavTreeItem> sostavTreeItemList;
        if (!dictionary.TryGetValue(key, out sostavTreeItemList))
        {
          sostavTreeItemList = (IList<TechCardUtils.SostavTreeItem>) new List<TechCardUtils.SostavTreeItem>();
          dictionary.Add(key, sostavTreeItemList);
        }
        sostavTreeItemList.Add(sostav);
      }
    }
    this._techComposition = (IDictionary<long, IList<TechCardUtils.SostavTreeItem>>) dictionary;
  }

  /// <summary>Constructor</summary>
  /// <param name="objectRole"></param>
  /// <param name="sostavList"></param>
  public TechCompositionSostavTreeFilter(
    RelatedObjectsRole objectRole,
    [NotNull] IList<TechCardUtils.SostavTreeItem> sostavList)
  {
    this._objectRole = objectRole;
    this._sostavList = (IList<TechCardUtils.SostavTreeItem>) new List<TechCardUtils.SostavTreeItem>((IEnumerable<TechCardUtils.SostavTreeItem>) sostavList);
    this.SetCompositionList(this._objectRole, this._sostavList);
  }

  /// <summary>Constructor</summary>
  /// <param name="objectRole">Object's role</param>
  /// <param name="sostavList">Composition's list</param>
  public TechCompositionSostavTreeFilter(
    RelatedObjectsRole objectRole,
    [NotNull] IList<TechCardUtils.SostavSortedTreeItem> sostavList)
    : this(objectRole, (IList<TechCardUtils.SostavTreeItem>) new List<TechCardUtils.SostavTreeItem>((IEnumerable<TechCardUtils.SostavTreeItem>) sostavList))
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="role"></param>
  public override void UpdateRelatedObjectsRole(RelatedObjectsRole role)
  {
    this.SetCompositionList(this._objectRole, this._sostavList);
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
    if (nodePart == null)
      return (INodeQuery) null;
    if (!this._techComposition.ContainsKey(nodePart.ObjID) || this._techComposition[nodePart.ObjID].Count == 0)
      return (INodeQuery) null;
    List<long> longList = new List<long>();
    List<int> intList = new List<int>();
    foreach (TechCardUtils.SostavTreeItem sostavTreeItem in (IEnumerable<TechCardUtils.SostavTreeItem>) this._techComposition[nodePart.ObjID])
    {
      if (sostavTreeItem != null)
      {
        longList.Add(sostavTreeItem.LinkID);
        if (!intList.Contains(sostavTreeItem.LinkTypeID))
          intList.Add(sostavTreeItem.LinkTypeID);
      }
    }
    int relTypeId = intList.Count == 1 ? intList[0] : -1;
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    if (intList.Count != 1)
      conditionStructureList.Add(new ConditionStructure(-23, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.AND, 0, false));
    conditionStructureList.Add(new ConditionStructure(-20, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, false));
    conditions = ConditionStructure.Join(conditions, conditionStructureList.ToArray());
    TechCompositionQuery customQuery = new TechCompositionQuery((INodeQuerySupport) nodePart, nodePart.ObjID, nodePart.ObjTypeID, nodePart.ObjRole, relTypeId, conditions, nodePart.FiltrationOwnerID, nodePart.Contexts);
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
    if (!(obj is TechCompositionSostavTreeFilter sostavTreeFilter) || this._objectRole != sostavTreeFilter._objectRole || sostavTreeFilter._techComposition == null || this._techComposition == null || sostavTreeFilter._techComposition.Count != this._techComposition.Count)
      return false;
    List<long> longList1 = new List<long>((IEnumerable<long>) this._techComposition.Keys);
    List<long> longList2 = new List<long>((IEnumerable<long>) sostavTreeFilter._techComposition.Keys);
    for (int index = 0; index < this._techComposition.Count; ++index)
    {
      long key = longList1[index];
      bool flag = key == longList2[index];
      if (flag)
        flag = this._techComposition[key].Equals((object) sostavTreeFilter._techComposition[key]);
      if (!flag)
        return false;
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    return this._objectRole.GetHashCode() ^ this._sostavList.Count.GetHashCode();
  }
}
