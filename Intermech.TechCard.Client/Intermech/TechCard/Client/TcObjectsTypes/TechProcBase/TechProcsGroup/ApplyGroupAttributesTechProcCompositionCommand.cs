// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.ApplyGroupAttributesTechProcCompositionCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.TechCard.Client.Commands;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>
/// Передать атрибуты в единичные объекты из элемента состава группового техпроцесса
/// </summary>
internal class ApplyGroupAttributesTechProcCompositionCommand(string name) : 
  ApplyGroupAttributesBaseCommand(name)
{
  /// <summary>
  /// Словарь соответствия связи состава ЕТП (которую необходимо обновить атрибутами) и ЕТП от состава которого эта связь
  /// </summary>
  private Dictionary<RelInfoItem, ObjInfoItem> _relInfo2EtpInfoList;
  /// <summary>
  /// Словарь соответствия связи состава ЕТП и дочернего объекта, который необходимо обновить атрибутами
  /// </summary>
  private Dictionary<RelInfoItem, ObjInfoItem> _etpRel2ObjList;

  /// <summary>
  /// Загрузить данные по единичным объектам для диалога выбора
  /// </summary>
  /// <returns></returns>
  protected override bool LoadUnitItems()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<Gtp2EtpRefData> etpRelIdList = TechProcGroupUtils.GetEtpRelIDList((RelInfoItem) new RelObjInfoItem(this._relationId.Value, this._relationId.RelationType)
      {
        PartInfo = new ObjInfoItem(this._groupObjId.ObjectID, this._groupObjId.ObjectType),
        ProjInfo = (this._relationId.ProjID != 0L ? new ObjInfoItem(this._relationId.ProjID) : (ObjInfoItem) null)
      }, sessionKeeper.Session);
      if (etpRelIdList == null || etpRelIdList.Count == 0)
        return false;
      this._etpRel2ObjList = new Dictionary<RelInfoItem, ObjInfoItem>();
      foreach (Gtp2EtpRefData gtp2EtpRefData in etpRelIdList)
      {
        if (gtp2EtpRefData != null)
        {
          foreach (KeyValuePair<TypedInfoItem, TypedInfoItem> objRefId in gtp2EtpRefData.ObjRefIDs)
          {
            if (!this._etpRel2ObjList.ContainsKey(objRefId.Key as RelInfoItem))
              this._etpRel2ObjList.Add(objRefId.Key as RelInfoItem, objRefId.Value as ObjInfoItem);
          }
        }
      }
      if (this._etpRel2ObjList.Count == 0 || !TechProcGroupUtils.GetEtpProcObjects(this._etpRel2ObjList, TechCardConsts.ObjectTypes.TechProcEdinID, sessionKeeper.Session, out this._relInfo2EtpInfoList) || this._relInfo2EtpInfoList.Count == 0)
        return false;
      this._unitInfoItems = this._relInfo2EtpInfoList.Values.ToList<ObjInfoItem>();
    }
    return true;
  }

  /// <summary>Применить в отмеченных объектах отмеченные атрибуты</summary>
  /// <param name="selectedUnitList"></param>
  /// <param name="selectedAttributes"></param>
  /// <returns></returns>
  public override bool ApplyGroupAttributes(
    List<long> selectedUnitList,
    Dictionary<ElementInfo, List<AttributeValues>> selectedAttributes)
  {
    List<RelObjInfoItem> relInfoItem = new List<RelObjInfoItem>();
    foreach (long selectedUnit in selectedUnitList)
    {
      foreach (KeyValuePair<RelInfoItem, ObjInfoItem> relInfo2EtpInfo in this._relInfo2EtpInfoList)
      {
        ObjInfoItem partInfo;
        if (relInfo2EtpInfo.Value.ObjectID == selectedUnit && this._etpRel2ObjList.TryGetValue(relInfo2EtpInfo.Key, out partInfo))
        {
          if (relInfo2EtpInfo.Key is RelObjInfoItem key)
            relInfoItem.Add(key);
          else
            relInfoItem.Add(new RelObjInfoItem(relInfo2EtpInfo.Key, (ObjInfoItem) null, partInfo));
        }
      }
    }
    if (relInfoItem.Count == 0)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      try
      {
        session.StartLogHistory();
        this.SetGroupAttributes(session, relInfoItem, selectedAttributes);
        this._modificationsList.AddRange((IEnumerable<CategoryValue>) session.GetModificationsHistoryList());
      }
      finally
      {
        session.StopLogHistory();
      }
    }
    return true;
  }
}
