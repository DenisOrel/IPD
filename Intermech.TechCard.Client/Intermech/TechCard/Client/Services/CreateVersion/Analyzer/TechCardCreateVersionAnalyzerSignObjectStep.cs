// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.Analyzer.TechCardCreateVersionAnalyzerSignObjectStep
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.TechCard.Client.Services.DataProviders;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion.Analyzer;

/// <summary>Пророка наличия подписываемых объектов</summary>
internal class TechCardCreateVersionAnalyzerSignObjectStep : 
  TechCardCreateVersionAnalyzerSignApplicabilityStep
{
  /// <summary>
  /// 
  /// </summary>
  private readonly bool _singleSignedObjectLimit;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relObjInfo2SignedObjCache"></param>
  /// <returns></returns>
  private bool GetSignedObjectCache(
    IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache,
    out IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2SignedObjCache)
  {
    relObjInfo2SignedObjCache = (IDictionary<RelObjInfoItem, ObjInfoItem>) new Dictionary<RelObjInfoItem, ObjInfoItem>();
    ICollection<int> signedTechCardTypeIds = TechCardCreateVersionAnalyzer.GetSignedTechCardTypeIds();
    foreach (KeyValuePair<RelObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<RelObjInfoItem, ObjInfoItem>>) relObjInfo2RootObjCache)
    {
      if (signedTechCardTypeIds.Contains(keyValuePair.Value.ObjTypeID))
        relObjInfo2SignedObjCache[keyValuePair.Key] = keyValuePair.Value;
    }
    return relObjInfo2SignedObjCache.Any<KeyValuePair<RelObjInfoItem, ObjInfoItem>>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relObjInfo2SignedObjCache"></param>
  /// <returns></returns>
  private bool CheckSignedObjectLimit(
    IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2SignedObjCache)
  {
    if (!this._singleSignedObjectLimit)
      return true;
    IDictionary<long, ObjInfoItem> dictionary = (IDictionary<long, ObjInfoItem>) new Dictionary<long, ObjInfoItem>();
    foreach (ObjInfoIDItem objInfoIdItem in (IEnumerable<ObjInfoItem>) relObjInfo2SignedObjCache.Values)
      dictionary[objInfoIdItem.ID] = (ObjInfoItem) objInfoIdItem;
    if (dictionary.Count <= 1)
      return true;
    string format = LocalizationHolder.rm.GetString(sc_19740.ssp_techcard_19741()) + LocalizationHolder.rm.GetString("TechCard.Client_468");
    string caption = LocalizationHolder.rm.GetString("TechCard.Client_138");
    List<string> values = new List<string>();
    using (SessionKeeper keeper = new SessionKeeper())
      values.AddRange(dictionary.Values.Select<ObjInfoItem, string>((System.Func<ObjInfoItem, string>) (item => TechCardConsts.Utils.GetObjectString(item.ObjectID, keeper.Session))));
    int num = (int) MessageBox.Show(string.Format(format, (object) string.Join(", ", (IEnumerable<string>) values)), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="unsignedRootObjItems"></param>
  /// <param name="relObjInfo2SignedObjDbCache"></param>
  /// <returns></returns>
  private bool LoadSignedObjectsFromDb(
    ICollection<ObjInfoItem> unsignedRootObjItems,
    out IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2SignedObjDbCache)
  {
    relObjInfo2SignedObjDbCache = (IDictionary<RelObjInfoItem, ObjInfoItem>) new Dictionary<RelObjInfoItem, ObjInfoItem>();
    DataTable parentSostavData;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      parentSostavData = DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) unsignedRootObjItems, sessionKeeper.Session, TechCardConsts.RelTypes.TechAllRelationTypes.Append<int>(TechCardConsts.RelTypes.SortedRelationID), true, new DBRecordSetParams(-1));
    if (parentSostavData == null)
      return true;
    TechCardCreateVersionAnalyzerStepData stepData = this._stepData;
    try
    {
      RelObjInfoItem[] array = new TechCardRelObjInfoItemsTypeUpdater<RelObjInfoItem>((ITechCardDataEnumerableProvider<RelObjInfoItem>) new TechRelObjInfoItemsFromDataTableProvider<RelObjInfoItem>(parentSostavData, false)).Execute().ToArray<RelObjInfoItem>();
      stepData.CompositionItems.AddRange<RelObjInfoItem>((IEnumerable<RelObjInfoItem>) array);
      this._stepData = new TechCardCreateVersionAnalyzerStepData((IEnumerable<RelObjInfoItem>) ((IEnumerable<RelObjInfoItem>) array).Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => unsignedRootObjItems.Contains(item.PartInfo))).ToList<RelObjInfoItem>(), (IEnumerable<RelObjInfoItem>) array);
      IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache;
      this.LoadRootItemInfo(out relObjInfo2RootObjCache);
      this.GetSignedObjectCache(relObjInfo2RootObjCache, out relObjInfo2SignedObjDbCache);
    }
    finally
    {
      this._stepData = stepData;
    }
    return true;
  }

  public TechCardCreateVersionAnalyzerSignObjectStep(bool singleSignedObjectLimit = true)
  {
    this._singleSignedObjectLimit = singleSignedObjectLimit;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relObjInfo2RootObjCache"></param>
  /// <returns></returns>
  protected override bool CheckRootItemInfo(
    IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache)
  {
    IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2SignedObjCache;
    this.GetSignedObjectCache(relObjInfo2RootObjCache, out relObjInfo2SignedObjCache);
    this._stepData.RelObjInfo2SignedObjCache = relObjInfo2SignedObjCache;
    if (!this.CheckSignedObjectLimit(relObjInfo2SignedObjCache))
      return false;
    ICollection<ObjInfoItem> unsignedRootObjItems = (ICollection<ObjInfoItem>) new HashSet<ObjInfoItem>();
    foreach (KeyValuePair<RelObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<RelObjInfoItem, ObjInfoItem>>) relObjInfo2RootObjCache)
    {
      if (!relObjInfo2SignedObjCache.ContainsKey(keyValuePair.Key))
        unsignedRootObjItems.Add(keyValuePair.Value);
    }
    if (unsignedRootObjItems.Count == 0)
      return relObjInfo2RootObjCache.Any<KeyValuePair<RelObjInfoItem, ObjInfoItem>>();
    IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2SignedObjDbCache;
    if (!this.LoadSignedObjectsFromDb(unsignedRootObjItems, out relObjInfo2SignedObjDbCache))
      return false;
    foreach (KeyValuePair<RelObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<RelObjInfoItem, ObjInfoItem>>) relObjInfo2RootObjCache)
    {
      KeyValuePair<RelObjInfoItem, ObjInfoItem> relObjInfo2RootObjItem = keyValuePair;
      if (!relObjInfo2SignedObjCache.ContainsKey(relObjInfo2RootObjItem.Key))
      {
        IEnumerable<ObjInfoItem> source = relObjInfo2SignedObjDbCache.Where<KeyValuePair<RelObjInfoItem, ObjInfoItem>>((System.Func<KeyValuePair<RelObjInfoItem, ObjInfoItem>, bool>) (item => (TypedInfoItem) item.Key.PartInfo == (TypedInfoItem) relObjInfo2RootObjItem.Value)).Select<KeyValuePair<RelObjInfoItem, ObjInfoItem>, ObjInfoItem>((System.Func<KeyValuePair<RelObjInfoItem, ObjInfoItem>, ObjInfoItem>) (item => item.Value));
        if (source.Any<ObjInfoItem>())
          relObjInfo2SignedObjCache[relObjInfo2RootObjItem.Key] = source.FirstOrDefault<ObjInfoItem>();
      }
    }
    if (relObjInfo2SignedObjCache.Count == 0)
    {
      this._stepData.DefaultCreateVersionHandler = true;
      return false;
    }
    return this.CheckSignedObjectLimit(relObjInfo2SignedObjCache);
  }
}
