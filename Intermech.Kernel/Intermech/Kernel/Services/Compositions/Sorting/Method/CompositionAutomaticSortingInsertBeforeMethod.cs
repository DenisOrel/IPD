// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Sorting.Method.CompositionAutomaticSortingInsertBeforeMethod
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.Compositions.Sorting.Method;

internal class CompositionAutomaticSortingInsertBeforeMethod(
  [NotNull] CompositionObjectInfoCache objectCompositionCache) : CompositionAutomaticSortingMethod(objectCompositionCache)
{
  protected int _targetIndex = -1;
  protected CompositionSortingProjInfo _targetRelationInfo;

  protected override bool DoValidateParams(CompositionSortingParams sortingParams)
  {
    return base.DoValidateParams(sortingParams) & !Intermech.Consts.IsUndefinedRelationId(sortingParams.TargetRelationId);
  }

  protected override bool DoLoadData()
  {
    if (!base.DoLoadData())
      return false;
    foreach (KeyValuePair<ObjInfoItem, CompositionObjectInfo> keyValuePair in (IEnumerable<KeyValuePair<ObjInfoItem, CompositionObjectInfo>>) this._objectCompositionCache.Data)
    {
      CompositionObjectInfo compositionObjectInfo = keyValuePair.Value;
      foreach (CompositionSortingInfoItem infoItem in (IEnumerable<CompositionSortingInfoItem>) compositionObjectInfo.CompositionInfoCache.InfoItems)
      {
        if (infoItem.PrjLinkID == this._sortingParams.TargetRelationId)
        {
          this._targetIndex = compositionObjectInfo.CompositionInfoCache.InfoItems.IndexOf(infoItem);
          this._targetRelationInfo = new CompositionSortingProjInfo(infoItem)
          {
            ProjObjID = keyValuePair.Key.ObjectID,
            ProjTypeID = keyValuePair.Key.ObjTypeID
          };
          break;
        }
      }
    }
    return this._targetRelationInfo != null;
  }

  protected override void DoProceedItem(
    KeyValuePair<CompositionObjectSortingNode, IList<CompositionSortingProjInfo>> compNodeItem)
  {
    CompositionObjectSortingNode key1 = compNodeItem.Key;
    IList<CompositionSortingProjInfo> compositionSortingProjInfoList = compNodeItem.Value;
    if (key1.ProjObjID != this._targetRelationInfo.ProjObjID)
      return;
    ObjInfoItem key2 = new ObjInfoItem(key1.ProjObjID, key1.ProjTypeID);
    CompositionObjectInfo compositionObjectInfo;
    if (!this._objectCompositionCache.Data.TryGetValue(key2, out compositionObjectInfo))
      return;
    long initValue = 1000000;
    long val1 = 1000000;
    int num1 = this._objectCompositionCache.Сomparer.SortingRule.CompareTo(key2.ObjTypeID, key1.RelTypeID, this._targetRelationInfo.RelTypeID, key1.PartObjType, this._targetRelationInfo.PartObjType, true);
    if (num1 > 0)
      return;
    long num2;
    if (num1 == 0)
    {
      CompositionSortingInfoItem nextObject = compositionObjectInfo.CompositionInfoCache.GetNextObject((CompositionSortingInfoItem) this._targetRelationInfo);
      if (nextObject != null)
      {
        initValue = nextObject.Sorting;
        num2 = (this._targetRelationInfo.Sorting - initValue) / (long) (compositionSortingProjInfoList.Count + 1);
      }
      else
        num2 = Math.Min(val1, this._targetRelationInfo.Sorting - 1000000000L);
    }
    else
    {
      CompositionSortingInfoItem nextObject = compositionObjectInfo.CompositionInfoCache.GetNextObject((CompositionSortingInfoItem) this._targetRelationInfo);
      CompositionSortingInfoItem compositionSortingInfoItem;
      for (compositionSortingInfoItem = nextObject; nextObject != null && this._objectCompositionCache.Сomparer.SortingRule.CompareTo(key2.ObjTypeID, compositionSortingInfoItem.RelTypeID, key1.RelTypeID, compositionSortingInfoItem.PartObjType, key1.PartObjType, true) > 0; nextObject = compositionObjectInfo.CompositionInfoCache.GetNextObject(nextObject))
        compositionSortingInfoItem = nextObject;
      if (compositionSortingInfoItem != null)
      {
        initValue = compositionSortingInfoItem.Sorting;
        num2 = nextObject == null ? 1000000000L : (compositionSortingInfoItem.Sorting - nextObject.Sorting) / (long) (compositionSortingProjInfoList.Count + 1);
      }
      else
        num2 = 1000000000L;
    }
    foreach (CompositionSortingProjInfo compositionSortingProjInfo in (IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList)
    {
      initValue += num2;
      if (initValue == 0L)
        ++initValue;
      compositionSortingProjInfo.Sorting = initValue;
      this._session.GetRelation(compositionSortingProjInfo.PrjLinkID).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"), (object) initValue)
      });
      if (this._targetIndex == compositionObjectInfo.CompositionInfoCache.InfoItems.Count - 1)
        compositionObjectInfo.CompositionInfoCache.AddItem((CompositionSortingInfoItem) (compositionSortingProjInfo.Clone() as CompositionSortingProjInfo));
      else
        compositionObjectInfo.CompositionInfoCache.InsertItem((CompositionSortingInfoItem) (compositionSortingProjInfo.Clone() as CompositionSortingProjInfo), this._targetIndex + 1);
    }
  }
}
