// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.Analyzer.TechCardCreateVersionAnalyzerCopyModeStep
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion.Analyzer;

/// <summary>Анализ режимов создания объектов</summary>
/// <remarks>
/// Для некоторых техн. типов не создаются копии объектов,
/// например оснастка, оборудование, персонал, материал, и один и тот же
/// объект может входить в сотни разных родительских типов.
/// Поэтому создание версии для таких типов возможна лишь в
/// контексте родителя.
/// </remarks>
/// &gt;
internal class TechCardCreateVersionAnalyzerCopyModeStep : TechCardCreateVersionAnalyzerStep
{
  /// <summary>
  /// Получение информации о головных узлах из применяемости для исходных объектов
  /// </summary>
  /// <param name="relObjInfo2RootObjCache"></param>
  /// <returns></returns>
  private bool LoadRootItemInfo(
    out IDictionary<ObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache)
  {
    relObjInfo2RootObjCache = (IDictionary<ObjInfoItem, ObjInfoItem>) new Dictionary<ObjInfoItem, ObjInfoItem>();
    IDictionary<ObjInfoItem, RelObjInfoItem> dictionary = (IDictionary<ObjInfoItem, RelObjInfoItem>) new Dictionary<ObjInfoItem, RelObjInfoItem>();
    foreach (RelObjInfoItem compositionItem in (IEnumerable<RelObjInfoItem>) this._stepData.CompositionItems)
    {
      if (!((TypedInfoItem) compositionItem.PartInfo == (TypedInfoItem) null))
        dictionary[compositionItem.PartInfo] = compositionItem;
    }
    foreach (RelObjInfoItem relObjInfoItem1 in (IEnumerable<RelObjInfoItem>) this._stepData.RelObjInfoItems)
    {
      ObjInfoItem projInfo = relObjInfoItem1.ProjInfo;
      if ((TypedInfoItem) projInfo == (TypedInfoItem) null)
      {
        relObjInfo2RootObjCache[relObjInfoItem1.PartInfo] = relObjInfoItem1.PartInfo;
      }
      else
      {
        RelObjInfoItem relObjInfoItem2;
        while (dictionary.TryGetValue(projInfo, out relObjInfoItem2) && !((TypedInfoItem) relObjInfoItem2.ProjInfo == (TypedInfoItem) null) && TechCardConsts.Utils.IsTechcardObjectType((object) relObjInfoItem2.ProjInfo.ObjTypeID))
          projInfo = relObjInfoItem2.ProjInfo;
        relObjInfo2RootObjCache[relObjInfoItem1.PartInfo] = projInfo;
      }
    }
    return relObjInfo2RootObjCache.Any<KeyValuePair<ObjInfoItem, ObjInfoItem>>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relObjInfo2RootObjCache"></param>
  /// <returns></returns>
  private bool CheckRootItemInfo(
    IDictionary<ObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache)
  {
    ICollection<int> signedTechCardTypeIds = TechCardCreateVersionAnalyzer.GetSignedTechCardTypeIds();
    if (!signedTechCardTypeIds.Any<int>())
    {
      this._stepData.DefaultCreateVersionHandler = true;
      return false;
    }
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
    foreach (KeyValuePair<ObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<ObjInfoItem, ObjInfoItem>>) relObjInfo2RootObjCache)
    {
      if (!((TypedInfoItem) keyValuePair.Key == (TypedInfoItem) keyValuePair.Value) && !signedTechCardTypeIds.Contains(keyValuePair.Value.ObjTypeID))
        objInfoItemList.Add(keyValuePair.Value);
    }
    if (objInfoItemList.Count == 0)
      return true;
    ICollection<ObjInfoItem> objInfoItems = (ICollection<ObjInfoItem>) new HashSet<ObjInfoItem>();
    GenericListHelper.MakeUnique<ObjInfoItem>(objInfoItemList);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Dictionary<long, ImbaseObjCreateInfo> objCreateInfo;
      if (ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, true).GetCreationMode((IDictionary<long, int>) ObjInfoHelper.GetObjectCache((IEnumerable<ObjInfoItem>) objInfoItemList), sessionKeeper.Session.SessionGUID, out objCreateInfo))
      {
        if (objCreateInfo.Count > 0)
        {
          foreach (KeyValuePair<long, ImbaseObjCreateInfo> keyValuePair in objCreateInfo)
          {
            if (keyValuePair.Value.CreateMode == ImbaseObjCreateMode.iocmUseExists)
            {
              int index = SomeTypedInfoHelper<ObjInfoItem>.BinarySearch(objInfoItemList, keyValuePair.Key);
              if (index >= 0)
                objInfoItems.Add(objInfoItemList[index]);
            }
          }
        }
      }
    }
    if (objInfoItems.Count == 0)
      return true;
    if (this._stepData.RelObjInfoItems.Count == 1 && this._stepData.ErrorDescriptors.Count == 0)
    {
      this._stepData.DefaultCreateVersionHandler = true;
      return false;
    }
    ICollection<ObjInfoItem> invalidObjItems = (ICollection<ObjInfoItem>) new HashSet<ObjInfoItem>();
    foreach (KeyValuePair<ObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<ObjInfoItem, ObjInfoItem>>) relObjInfo2RootObjCache)
    {
      if (objInfoItems.Contains(keyValuePair.Value))
        invalidObjItems.Add(keyValuePair.Key);
    }
    this._stepData.RelObjInfoItems.RemoveRange<RelObjInfoItem>(this._stepData.RelObjInfoItems.Where<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item => invalidObjItems.Contains(item.PartInfo))));
    string caption = LocalizationHolder.rm.GetString(sc_19736.ssp_techcard_19737());
    DictDescriptor dictDescriptor = (DictDescriptor) new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<int>) ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) invalidObjItems)), caption, ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) invalidObjItems));
    dictDescriptor.ExpandNodes = false;
    this._stepData.ErrorDescriptors.Add((IDescriptor) dictDescriptor);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="param"></param>
  /// <returns></returns>
  protected override bool DoExecute()
  {
    IDictionary<ObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache;
    return this._stepData.RelObjInfoItems.Count == 0 || !this.LoadRootItemInfo(out relObjInfo2RootObjCache) || this.CheckRootItemInfo(relObjInfo2RootObjCache);
  }
}
