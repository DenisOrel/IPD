// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.Analyzer.TechCardCreateVersionAnalyzerSignApplicabilityStep
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Descriptors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion.Analyzer;

internal class TechCardCreateVersionAnalyzerSignApplicabilityStep : TechCardCreateVersionAnalyzerStep
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IEnumerable<int> _relationTypes = (IEnumerable<int>) new int[2]
  {
    TechCardConsts.RelTypes.TechRelationID,
    TechCardConsts.RelTypes.SortedRelationID
  };

  /// <summary>
  /// Проверка применяемости на необходимость создания версии родительского объекта
  /// </summary>
  /// <param name="relationTypeId"></param>
  /// <param name="projObjectTypeId"></param>
  /// <param name="partObjectTypeId"></param>
  /// <returns></returns>
  private bool AllowApplicability(int relationTypeId, int projObjectTypeId, int partObjectTypeId)
  {
    IMSApplicability applicability = MetaDataHelper.GetApplicability(projObjectTypeId, partObjectTypeId, relationTypeId);
    if (applicability == null)
      return false;
    if (applicability.IsContent)
      return true;
    if (applicability.RelationTypeID != TechCardConsts.RelTypes.SortedRelationID)
      return false;
    return MetaDataHelper.IsObjectTypeChildOf(partObjectTypeId, TechCardConsts.ObjectTypes.TechBaseDocID) || MetaDataHelper.IsObjectTypeChildOf(partObjectTypeId, TechCardConsts.ObjectTypes.ComlectTechDocBaseID);
  }

  /// <summary>
  /// Получение информации о головных узлах из применяемости для исходных объектов
  /// </summary>
  /// <param name="relObjInfo2RootObjCache"></param>
  /// <returns>Ищем ближайший подписываемый объект. Если его нет - возвращаем самый "верхний" родитель</returns>
  protected bool LoadRootItemInfo(
    out IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache)
  {
    relObjInfo2RootObjCache = (IDictionary<RelObjInfoItem, ObjInfoItem>) new Dictionary<RelObjInfoItem, ObjInfoItem>();
    ICollection<int> signedTechCardTypeIds = TechCardCreateVersionAnalyzer.GetSignedTechCardTypeIds();
    if (!signedTechCardTypeIds.Any<int>())
    {
      this._stepData.DefaultCreateVersionHandler = true;
      return false;
    }
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
        relObjInfo2RootObjCache[relObjInfoItem1] = relObjInfoItem1.PartInfo;
      }
      else
      {
        ObjInfoItem objInfoItem = projInfo;
        for (RelObjInfoItem relObjInfoItem2 = relObjInfoItem1; (TypedInfoItem) relObjInfoItem2 != (TypedInfoItem) null && !((TypedInfoItem) relObjInfoItem2.ProjInfo == (TypedInfoItem) null) && this.AllowApplicability(relObjInfoItem2.RelTypeID, relObjInfoItem2.ProjInfo.ObjTypeID, relObjInfoItem2.PartInfo.ObjTypeID); dictionary.TryGetValue(relObjInfoItem2.ProjInfo, out relObjInfoItem2))
        {
          if (signedTechCardTypeIds.Contains(relObjInfoItem2.ProjInfo.ObjTypeID))
          {
            objInfoItem = relObjInfoItem2.ProjInfo;
            break;
          }
          if (TechCardConsts.Utils.IsTechcardObjectType((object) relObjInfoItem2.ProjInfo.ObjTypeID))
            objInfoItem = relObjInfoItem2.ProjInfo;
        }
        relObjInfo2RootObjCache[relObjInfoItem1] = objInfoItem;
      }
    }
    return relObjInfo2RootObjCache.Any<KeyValuePair<RelObjInfoItem, ObjInfoItem>>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relObjInfo2RootObjCache"></param>
  /// <returns></returns>
  protected virtual bool CheckRootItemInfo(
    IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache)
  {
    DataTable dataTable = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable[] cacheTables = sessionKeeper.Session.GetCacheTables("IMS_TYPES_APPLICABILITY");
      if (cacheTables != null)
      {
        if (cacheTables.Length != 0)
        {
          dataTable = DataSetProcessor.CopyTable(cacheTables[0]);
          int columnIndex = dataTable.Columns.IndexOf("F_RELATION_TYPE");
          if (columnIndex == -1)
          {
            dataTable.Clear();
          }
          else
          {
            for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
            {
              if (this._relationTypes == null || !this._relationTypes.Contains<int>(Convert.ToInt32(dataTable.Rows[index][columnIndex])))
                dataTable.Rows.RemoveAt(index);
            }
          }
        }
      }
    }
    if (dataTable == null)
      return false;
    int columnIndex1 = dataTable.Columns.IndexOf("F_OBJECT_TYPE");
    int columnIndex2 = dataTable.Columns.IndexOf("F_INOBJECT_TYPE");
    List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
    ICollection<int> signedTechCardTypeIds = TechCardCreateVersionAnalyzer.GetSignedTechCardTypeIds();
    foreach (KeyValuePair<RelObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<RelObjInfoItem, ObjInfoItem>>) relObjInfo2RootObjCache)
    {
      if (!signedTechCardTypeIds.Contains(keyValuePair.Key.PartInfo.ObjTypeID))
      {
        if (signedTechCardTypeIds.Contains(keyValuePair.Value.ObjTypeID))
          relObjInfoItemList.Add(keyValuePair.Key);
        else if (dataTable.Rows.Count != 0)
        {
          HashSet<int> intSet1 = new HashSet<int>();
          HashSet<int> other = new HashSet<int>()
          {
            keyValuePair.Value.ObjTypeID
          };
          other.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeParentsID(keyValuePair.Value.ObjTypeID));
          bool flag = false;
          while (other.Count != 0)
          {
            HashSet<int> intSet2 = new HashSet<int>();
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              int int32_1 = Convert.ToInt32(row[columnIndex1]);
              int int32_2 = Convert.ToInt32(row[columnIndex2]);
              if (other.Contains(int32_1) && !intSet1.Contains(int32_2) && this.AllowApplicability(Convert.ToInt32(row["F_RELATION_TYPE"]), int32_2, int32_1))
              {
                if (!signedTechCardTypeIds.Contains(int32_2))
                {
                  intSet2.Add(int32_2);
                }
                else
                {
                  flag = true;
                  relObjInfoItemList.Add(keyValuePair.Key);
                  break;
                }
              }
            }
            if (!flag)
            {
              intSet1.UnionWith((IEnumerable<int>) other);
              other.Clear();
              foreach (int childTypeID in intSet2)
              {
                other.Add(childTypeID);
                other.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeParentsID(childTypeID));
              }
            }
            else
              break;
          }
        }
      }
    }
    if (relObjInfo2RootObjCache.Count == relObjInfoItemList.Count)
      return true;
    if (this._stepData.RelObjInfoItems.Count == 1 && this._stepData.ErrorDescriptors.Count == 0)
    {
      this._stepData.DefaultCreateVersionHandler = true;
      return false;
    }
    ICollection<ObjInfoItem> invalidObjectItems = (ICollection<ObjInfoItem>) new HashSet<ObjInfoItem>();
    foreach (KeyValuePair<RelObjInfoItem, ObjInfoItem> keyValuePair in (IEnumerable<KeyValuePair<RelObjInfoItem, ObjInfoItem>>) relObjInfo2RootObjCache)
    {
      if (!relObjInfoItemList.Contains(keyValuePair.Key))
        invalidObjectItems.Add(keyValuePair.Key.PartInfo);
    }
    this._stepData.RelObjInfoItems.RemoveRange<RelObjInfoItem>(this._stepData.RelObjInfoItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => invalidObjectItems.Contains(item.PartInfo))));
    string caption = LocalizationHolder.rm.GetString(sc_19738.ssp_techcard_19739());
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) invalidObjectItems);
    DictDescriptor dictDescriptor = (DictDescriptor) new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<int>) ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) invalidObjectItems)), caption, objectTypeCache);
    dictDescriptor.ExpandNodes = false;
    this._stepData.ErrorDescriptors.Add((IDescriptor) dictDescriptor);
    return true;
  }

  protected override bool DoExecute()
  {
    IDictionary<RelObjInfoItem, ObjInfoItem> relObjInfo2RootObjCache;
    return this._stepData.RelObjInfoItems.Count == 0 || !this.LoadRootItemInfo(out relObjInfo2RootObjCache) || this.CheckRootItemInfo(relObjInfo2RootObjCache);
  }
}
