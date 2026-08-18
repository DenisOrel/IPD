// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Sorting.Method.CompositionAutomaticSortingMethod
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services.Compositions.Sorting.Method;

internal abstract class CompositionAutomaticSortingMethod
{
  protected readonly CompositionObjectInfoCache _objectCompositionCache;
  protected IUserSession _session;
  protected CompositionSortingParams _sortingParams;

  private bool ValidateCompositionList(
    IList<CompositionSortingProjInfo> compositionInfoList)
  {
    bool flag = false;
    if (compositionInfoList == null || compositionInfoList.Count == 0)
      return false;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545");
    List<int> intList = new List<int>();
    Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
    for (int index = 0; index < compositionInfoList.Count; ++index)
    {
      CompositionSortingProjInfo compositionInfo = compositionInfoList[index];
      if (compositionInfo == null || compositionInfo.PrjLinkID == 0L)
        intList.Add(index);
      else if (!(flag = compositionInfo.HasEmptyInfo()))
      {
        IMSRelationType relationType = MetaDataHelper.GetRelationType(compositionInfo.RelTypeID);
        if (relationType == null)
          intList.Add(index);
        else if (!relationType.AnyAttributes && MetaDataHelper.GetAttribute4RelationType(compositionInfo.RelTypeID, attributeTypeId) == null)
        {
          intList.Add(index);
        }
        else
        {
          List<int> visibleRelations;
          if (!dictionary.TryGetValue(compositionInfo.ProjTypeID, out visibleRelations))
          {
            visibleRelations = this._objectCompositionCache.Сomparer.SortingRule.GetObjectTypeVisibleRelations(compositionInfo.ProjTypeID, true);
            dictionary.Add(compositionInfo.ProjTypeID, visibleRelations);
          }
          if (!visibleRelations.Contains(compositionInfo.RelTypeID))
            intList.Add(index);
        }
      }
    }
    for (int index = intList.Count - 1; index >= 0; --index)
      compositionInfoList.RemoveAt(index);
    return flag;
  }

  private void UpdateCompositionList(
    IList<CompositionSortingProjInfo> compositionInfoList)
  {
    if (compositionInfoList == null || compositionInfoList.Count == 0)
      return;
    IDictionary<long, CompositionSortingProjInfo> dictionary1 = (IDictionary<long, CompositionSortingProjInfo>) new Dictionary<long, CompositionSortingProjInfo>(compositionInfoList.Count);
    foreach (CompositionSortingProjInfo compositionInfo in (IEnumerable<CompositionSortingProjInfo>) compositionInfoList)
    {
      if (compositionInfo != null && (compositionInfo.ProjObjID == 0L || compositionInfo.PartObjType == -1 || compositionInfo.RelTypeID == -1))
        dictionary1.Add(compositionInfo.PrjLinkID, compositionInfo);
    }
    if (dictionary1.Count != 0)
    {
      IDBRelationCollection relationCollection = this._session.GetRelationCollection(-1);
      IEnumerable<ColumnDescriptor> sourceTableColumns = RelObjInfoDbScheme<ObjInfoItem>.GetSourceTableColumns();
      DBRecordSetParams paramSet = new DBRecordSetParams(new List<ConditionStructure>()
      {
        new ConditionStructure(-20, RelationalOperators.In, (object) dictionary1.Keys.ToArray<long>(), LogicalOperators.NONE, 0, false)
      }.ToArray(), sourceTableColumns.ToArray<ColumnDescriptor>());
      relationCollection.LocalTypesMode = true;
      DataTable dataTable = relationCollection.Select(paramSet);
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          CompositionSortingProjInfo compositionSortingProjInfo;
          if (dictionary1.TryGetValue(DataSetProcessor.GetInt64Value(row, "F_PRJLINK_ID", 0L), out compositionSortingProjInfo))
          {
            compositionSortingProjInfo.RelTypeID = DataSetProcessor.GetInt32Value(row, "F_RELATION_TYPE", -1);
            compositionSortingProjInfo.ProjObjID = DataSetProcessor.GetInt64Value(row, "F_PROJ_ID", 0L);
            compositionSortingProjInfo.PartObjType = DataSetProcessor.GetInt32Value(row, "F_OBJECT_TYPE", -1);
          }
        }
      }
      foreach (KeyValuePair<long, CompositionSortingProjInfo> keyValuePair in (IEnumerable<KeyValuePair<long, CompositionSortingProjInfo>>) dictionary1)
      {
        if (keyValuePair.Value.RelTypeID == -1)
        {
          IDBRelation relation = this._session.GetRelation(keyValuePair.Value.PrjLinkID, false);
          if (relation != null)
          {
            keyValuePair.Value.RelTypeID = relation.RelationType;
            keyValuePair.Value.ProjObjID = relation.ProjID;
            IDBObject objectByVersionsRule = this._session.GetObjectByVersionsRule(relation.PartID, "cad005aa-306c-11d8-b4e9-00304f19f545", false);
            if (objectByVersionsRule != null)
              keyValuePair.Value.PartObjType = objectByVersionsRule.ObjectType;
          }
        }
      }
    }
    Dictionary<long, IList<CompositionSortingProjInfo>> dictionary2 = new Dictionary<long, IList<CompositionSortingProjInfo>>(compositionInfoList.Count);
    foreach (CompositionSortingProjInfo compositionInfo in (IEnumerable<CompositionSortingProjInfo>) compositionInfoList)
    {
      if (compositionInfo != null && compositionInfo.ProjTypeID == -1)
      {
        IList<CompositionSortingProjInfo> compositionSortingProjInfoList;
        if (!dictionary2.TryGetValue(compositionInfo.ProjObjID, out compositionSortingProjInfoList))
        {
          compositionSortingProjInfoList = (IList<CompositionSortingProjInfo>) new List<CompositionSortingProjInfo>();
          dictionary2[compositionInfo.ProjObjID] = compositionSortingProjInfoList;
        }
        compositionSortingProjInfoList.Add(compositionInfo);
      }
    }
    if (dictionary2.Count <= 0)
      return;
    List<ObjInfoItem> objectInfoList = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) new List<long>((IEnumerable<long>) dictionary2.Keys));
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objectInfoList, this._session);
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
    for (int index = objectInfoList.Count - 1; index >= 0; --index)
    {
      ObjInfoItem objInfoItem = objectInfoList[index];
      if (objInfoItem.ItemTypeID == -1 && objInfoItem.ObjectID >= 0L)
      {
        objInfoItem.ObjectID = -objInfoItem.ObjectID;
        objectInfoList.RemoveAt(index);
        objInfoList.Add(objInfoItem);
      }
    }
    if (objInfoList.Count > 0)
    {
      ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objInfoList, this._session);
      foreach (ObjInfoItem objInfoItem in objInfoList)
      {
        if (objInfoItem.ObjTypeID != -1)
        {
          objInfoItem.ObjectID = -objInfoItem.ObjectID;
          objectInfoList.Add(objInfoItem);
        }
      }
    }
    foreach (ObjInfoItem objInfoItem in objectInfoList)
    {
      IList<CompositionSortingProjInfo> compositionSortingProjInfoList;
      if (dictionary2.TryGetValue(objInfoItem.ObjectID, out compositionSortingProjInfoList))
      {
        foreach (CompositionSortingProjInfo compositionSortingProjInfo in (IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList)
          compositionSortingProjInfo.ProjTypeID = objInfoItem.ObjTypeID;
      }
    }
  }

  private CompositionSortingParams DoGetPreparedParams(CompositionSortingParams sortingParams)
  {
    List<CompositionSortingProjInfo> compositionSortingProjInfoList = new List<CompositionSortingProjInfo>(sortingParams.CompositionSortingInfo);
    bool flag = this.ValidateCompositionList((IList<CompositionSortingProjInfo>) compositionSortingProjInfoList);
    if (compositionSortingProjInfoList.Count == 0)
      return (CompositionSortingParams) null;
    if (flag)
    {
      this.UpdateCompositionList((IList<CompositionSortingProjInfo>) compositionSortingProjInfoList);
      if (this.ValidateCompositionList((IList<CompositionSortingProjInfo>) compositionSortingProjInfoList))
      {
        for (int index = compositionSortingProjInfoList.Count - 1; index >= 0; --index)
        {
          if (compositionSortingProjInfoList[index].HasEmptyInfo())
            compositionSortingProjInfoList.RemoveAt(index);
        }
      }
    }
    return compositionSortingProjInfoList.Count == 0 ? (CompositionSortingParams) null : new CompositionSortingParams((IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList, sortingParams.TargetRelationId);
  }

  private void DoProceedItems()
  {
    Dictionary<CompositionObjectSortingNode, IList<CompositionSortingProjInfo>> dictionary = new Dictionary<CompositionObjectSortingNode, IList<CompositionSortingProjInfo>>();
    foreach (CompositionSortingProjInfo source in this._sortingParams.CompositionSortingInfo)
    {
      if (source != null)
      {
        CompositionObjectSortingNode key = new CompositionObjectSortingNode(source);
        IList<CompositionSortingProjInfo> compositionSortingProjInfoList;
        if (!dictionary.TryGetValue(key, out compositionSortingProjInfoList))
        {
          compositionSortingProjInfoList = (IList<CompositionSortingProjInfo>) new List<CompositionSortingProjInfo>();
          dictionary.Add(key, compositionSortingProjInfoList);
        }
        compositionSortingProjInfoList.Add(source);
      }
    }
    foreach (KeyValuePair<CompositionObjectSortingNode, IList<CompositionSortingProjInfo>> compNodeItem in dictionary)
      this.DoProceedItem(compNodeItem);
  }

  protected abstract void DoProceedItem(
    KeyValuePair<CompositionObjectSortingNode, IList<CompositionSortingProjInfo>> compNodeItem);

  protected virtual bool DoValidateParams([CanBeNull] CompositionSortingParams sortingParams)
  {
    bool? nullable;
    if (sortingParams == null)
    {
      nullable = new bool?();
    }
    else
    {
      IEnumerable<CompositionSortingProjInfo> compositionSortingInfo = sortingParams.CompositionSortingInfo;
      nullable = compositionSortingInfo != null ? new bool?(compositionSortingInfo.Any<CompositionSortingProjInfo>()) : new bool?();
    }
    return nullable ?? false;
  }

  protected virtual bool DoLoadData()
  {
    List<ObjInfoItem> list = this._sortingParams.CompositionSortingInfo.Select<CompositionSortingProjInfo, ObjInfoItem>((System.Func<CompositionSortingProjInfo, ObjInfoItem>) (item => new ObjInfoItem(item.ProjObjID, item.ProjTypeID))).ToList<ObjInfoItem>();
    GenericListHelper.MakeUnique<ObjInfoItem>(list);
    this._objectCompositionCache.LoadData(this._session, (IEnumerable<ObjInfoItem>) list);
    return true;
  }

  protected CompositionAutomaticSortingMethod([NotNull] CompositionObjectInfoCache objectCompositionCache)
  {
    this._objectCompositionCache = objectCompositionCache;
  }

  public void Execute([NotNull] IUserSession session, [NotNull] CompositionSortingParams sortingParams)
  {
    if (!this.DoValidateParams(sortingParams))
      return;
    this._session = session;
    this._sortingParams = this.DoGetPreparedParams(sortingParams);
    if (!this.DoValidateParams(this._sortingParams) || !this.DoLoadData())
      return;
    this.DoProceedItems();
  }
}
