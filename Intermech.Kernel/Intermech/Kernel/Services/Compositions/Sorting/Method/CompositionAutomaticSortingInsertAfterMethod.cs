// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Sorting.Method.CompositionAutomaticSortingInsertAfterMethod
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.Compositions.Sorting.Method;

internal class CompositionAutomaticSortingInsertAfterMethod(
  [NotNull] CompositionObjectInfoCache objectCompositionCache) : 
  CompositionAutomaticSortingInsertBeforeMethod(objectCompositionCache)
{
  protected override void DoProceedItem(
    KeyValuePair<CompositionObjectSortingNode, IList<CompositionSortingProjInfo>> compNodeItem)
  {
    CompositionObjectSortingNode key1 = compNodeItem.Key;
    IList<CompositionSortingProjInfo> compositionSortingProjInfoList = compNodeItem.Value;
    ObjInfoItem key2 = new ObjInfoItem(key1.ProjObjID, key1.ProjTypeID);
    CompositionObjectInfo compositionObjectInfo;
    if (key2.ObjectID != this._targetRelationInfo.ProjObjID || !this._objectCompositionCache.Data.TryGetValue(key2, out compositionObjectInfo))
      return;
    long num1 = 1000000;
    int num2 = this._objectCompositionCache.Сomparer.SortingRule.CompareTo(key2.ObjTypeID, key1.RelTypeID, this._targetRelationInfo.RelTypeID, key1.PartObjType, this._targetRelationInfo.PartObjType, true);
    if (num2 < 0)
      return;
    long sorting = this._targetRelationInfo.Sorting;
    if (num2 == 0)
    {
      CompositionSortingInfoItem prevObject = compositionObjectInfo.CompositionInfoCache.GetPrevObject((CompositionSortingInfoItem) this._targetRelationInfo);
      if (prevObject != null && (key1.RelTypeID != this._targetRelationInfo.RelTypeID || key1.RelTypeID == prevObject.RelTypeID))
        num1 = (prevObject.Sorting - sorting) / (long) (compositionSortingProjInfoList.Count + 1);
    }
    else
    {
      CompositionSortingInfoItem prevObject = compositionObjectInfo.CompositionInfoCache.GetPrevObject((CompositionSortingInfoItem) this._targetRelationInfo);
      CompositionSortingInfoItem compositionSortingInfoItem;
      for (compositionSortingInfoItem = prevObject; prevObject != null && this._objectCompositionCache.Сomparer.SortingRule.CompareTo(key2.ObjTypeID, compositionSortingInfoItem.RelTypeID, key1.RelTypeID, compositionSortingInfoItem.PartObjType, key1.PartObjType, true) < 0; prevObject = compositionObjectInfo.CompositionInfoCache.GetPrevObject(prevObject))
        compositionSortingInfoItem = prevObject;
      if (compositionSortingInfoItem != null)
      {
        sorting = compositionSortingInfoItem.Sorting;
        num1 = prevObject == null ? 1000000000L : (prevObject.Sorting - compositionSortingInfoItem.Sorting) / (long) (compositionSortingProjInfoList.Count + 1);
      }
      else
        num1 = 1000000000L;
    }
    foreach (CompositionSortingProjInfo compositionSortingProjInfo in (IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList)
    {
      sorting += num1;
      if (sorting == 0L)
        ++sorting;
      compositionSortingProjInfo.Sorting = sorting;
      this._session.GetRelation(compositionSortingProjInfo.PrjLinkID).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"), (object) sorting)
      });
      compositionObjectInfo.CompositionInfoCache.InsertItem((CompositionSortingInfoItem) (compositionSortingProjInfo.Clone() as CompositionSortingProjInfo), this._targetIndex);
    }
  }
}
