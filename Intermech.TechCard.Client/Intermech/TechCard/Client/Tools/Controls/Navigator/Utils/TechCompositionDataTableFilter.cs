// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.Utils.TechCompositionDataTableFilter
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.Navigator.Parts;
using Intermech.TechCard.Client.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator.Utils;

/// <summary>
/// Интерфейс для фильтрации отображаемого в дескрипторе состава по DataTable
/// </summary>
public sealed class TechCompositionDataTableFilter : TechCompositionFilter
{
  /// <summary>Object's role</summary>
  private RelatedObjectsRole _objectRole;
  /// <summary>Composition's info</summary>
  private readonly DataTable _compositionData;
  /// <summary>
  /// Objects composition's Cache
  /// Key - project version id, value - child data
  /// </summary>
  private IDictionary<long, IList<DataRow>> _compositionCache;
  /// <summary>
  /// 
  /// </summary>
  private int _colIdxProjObjId;
  /// <summary>
  /// 
  /// </summary>
  private int _colIdxRelId;
  /// <summary>
  /// 
  /// </summary>
  private int _colIdxRelType;

  /// <summary>Fill tech composition cache</summary>
  private void LoadCompositionCache()
  {
    this._compositionCache = (IDictionary<long, IList<DataRow>>) new Dictionary<long, IList<DataRow>>();
    if (this._compositionData == null)
      return;
    this._colIdxProjObjId = this._objectRole == RelatedObjectsRole.Composition ? this._compositionData.Columns.IndexOf("F_PROJ_ID") : this._compositionData.Columns.IndexOf(DataHelper.Consts.cnt_fld_PartObjID);
    this._colIdxRelId = this._compositionData.Columns.IndexOf("F_PRJLINK_ID");
    this._colIdxRelType = this._compositionData.Columns.IndexOf("F_RELATION_TYPE");
    if (this._colIdxProjObjId == -1 || this._colIdxRelId == -1 || this._colIdxRelType == -1)
      return;
    foreach (DataRow dataRow in this._compositionData.Select(string.Empty, "[F_PRJLINK_ID]"))
    {
      long int64 = Convert.ToInt64(dataRow[this._colIdxProjObjId]);
      IList<DataRow> dataRowList;
      if (!this._compositionCache.TryGetValue(int64, out dataRowList))
      {
        dataRowList = (IList<DataRow>) new List<DataRow>();
        this._compositionCache.Add(int64, dataRowList);
      }
      dataRowList.Add(dataRow);
    }
  }

  /// <summary>Constructor</summary>
  /// <param name="objectRole"></param>
  /// <param name="compositionData"></param>
  /// <remarks>Для compositionData обязательно наличие следующих полей F_PROJ_ID, F_PART_OBJ_ID, F_PRJLINK_ID, F_RELATION_TYPE !!</remarks>
  public TechCompositionDataTableFilter(RelatedObjectsRole objectRole, DataTable compositionData)
  {
    this._objectRole = objectRole;
    this._compositionData = compositionData;
    this.LoadCompositionCache();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="role"></param>
  public override void UpdateRelatedObjectsRole(RelatedObjectsRole role)
  {
    this._objectRole = role;
    this.LoadCompositionCache();
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
    IList<DataRow> dataRowList;
    if (!this._compositionCache.TryGetValue(nodePart.ObjID, out dataRowList))
      return (INodeQuery) null;
    List<long> longList = new List<long>();
    List<int> intList = new List<int>();
    foreach (DataRow dataRow in (IEnumerable<DataRow>) dataRowList)
    {
      int int32 = Convert.ToInt32(dataRow[this._colIdxRelType]);
      if (nodePart.RelationTypeID == -1 || nodePart.RelationTypeID == int32)
      {
        longList.Add(Convert.ToInt64(dataRow[this._colIdxRelId]));
        if (!intList.Contains(int32))
          intList.Add(int32);
      }
    }
    if (intList.Count == 0)
      return (INodeQuery) null;
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
    if (!(obj is TechCompositionDataTableFilter compositionDataTableFilter) || this._objectRole != compositionDataTableFilter._objectRole || compositionDataTableFilter._compositionData == null || this._compositionData == null || compositionDataTableFilter._compositionCache.Count != this._compositionCache.Count || compositionDataTableFilter._compositionData.Rows.Count != this._compositionData.Rows.Count)
      return false;
    List<long> longList1 = new List<long>((IEnumerable<long>) this._compositionCache.Keys);
    List<long> longList2 = new List<long>((IEnumerable<long>) compositionDataTableFilter._compositionCache.Keys);
    for (int index1 = 0; index1 < longList1.Count; ++index1)
    {
      long key = longList1[index1];
      if (key != longList2[index1])
        return false;
      IList<DataRow> dataRowList1 = this._compositionCache[key];
      IList<DataRow> dataRowList2 = compositionDataTableFilter._compositionCache[key];
      if (dataRowList1.Count != dataRowList2.Count)
        return false;
      for (int index2 = 0; index2 < dataRowList1.Count; ++index2)
      {
        if (dataRowList1[index2][this._colIdxRelId] != dataRowList2[index2][compositionDataTableFilter._colIdxRelId])
          return false;
      }
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    return this._compositionData == null ? 0 : this._compositionData.Rows.Count.GetHashCode();
  }
}
