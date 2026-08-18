// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Sorting.Method.CompositionAutomaticSortingInsertFirstMethod
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.Compositions.Sorting.Method;

internal class CompositionAutomaticSortingInsertFirstMethod(
  [NotNull] CompositionObjectInfoCache objectCompositionCache) : CompositionAutomaticSortingMethod(objectCompositionCache)
{
  protected override void DoProceedItem(
    KeyValuePair<CompositionObjectSortingNode, IList<CompositionSortingProjInfo>> compNodeItem)
  {
    CompositionObjectSortingNode key = compNodeItem.Key;
    IList<CompositionSortingProjInfo> compositionSortingProjInfoList = compNodeItem.Value;
    CompositionObjectInfo compositionObjectInfo;
    if (!this._objectCompositionCache.Data.TryGetValue(new ObjInfoItem(key.ProjObjID, key.ProjTypeID), out compositionObjectInfo))
      return;
    long initValue = 1000000;
    long num = 1000000;
    Dictionary<int, long> dictionary;
    if (!compositionObjectInfo.SortingCache.TryGetValue(key.RelTypeID, out dictionary))
    {
      dictionary = new Dictionary<int, long>();
      compositionObjectInfo.SortingCache.Add(key.RelTypeID, dictionary);
    }
    if (dictionary.ContainsKey(key.PartObjType))
    {
      initValue = dictionary[key.PartObjType];
    }
    else
    {
      CompositionSortingInfoItem closedObjectRec1 = compositionObjectInfo.CompositionInfoCache.FindClosedObjectRec(key.ProjTypeID, key.RelTypeID, key.PartObjType, CompositionSortingLookupMode.LessOnly);
      if (closedObjectRec1 != null)
      {
        initValue = closedObjectRec1.Sorting;
        if (closedObjectRec1.RelTypeID != key.RelTypeID || closedObjectRec1.PartObjType != key.PartObjType)
        {
          CompositionSortingInfoItem prevObject = compositionObjectInfo.CompositionInfoCache.GetPrevObject(closedObjectRec1);
          if (prevObject != null)
            num = (prevObject.Sorting - initValue) / (long) (compositionSortingProjInfoList.Count + 1);
          else
            initValue = (Convert.ToInt64(initValue / 1000000000L) + 1L) * 1000000000L - num;
        }
      }
      else if (compositionObjectInfo.CompositionInfoCache.InfoItems.Count != 0)
      {
        CompositionSortingInfoItem closedObjectRec2 = compositionObjectInfo.CompositionInfoCache.FindClosedObjectRec(key.ProjTypeID, key.RelTypeID, key.PartObjType, CompositionSortingLookupMode.More);
        if (closedObjectRec2 != null)
          initValue = (Convert.ToInt64(closedObjectRec2.Sorting / 1000000000L) - 1L) * 1000000000L - Convert.ToInt64(num / 2L);
      }
    }
    int index1 = 0;
    for (int index2 = 0; index2 < compositionObjectInfo.CompositionInfoCache.InfoItems.Count; ++index2)
    {
      if (compositionObjectInfo.CompositionInfoCache.InfoItems[index2].Sorting < initValue + num)
      {
        index1 = index2;
        break;
      }
    }
    foreach (CompositionSortingProjInfo compositionSortingProjInfo in (IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList)
    {
      initValue += num;
      if (initValue == 0L)
        ++initValue;
      compositionSortingProjInfo.Sorting = initValue;
      this._session.GetRelation(compositionSortingProjInfo.PrjLinkID).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"), (object) initValue)
      });
      compositionObjectInfo.CompositionInfoCache.InsertItem((CompositionSortingInfoItem) (compositionSortingProjInfo.Clone() as CompositionSortingProjInfo), index1);
    }
    dictionary[key.PartObjType] = initValue;
  }
}
