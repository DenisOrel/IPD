// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.ApplyGroupAttributesFromObjectCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>Применить атрибуты группового объекта в единичных</summary>
internal class ApplyGroupAttributesFromObjectCommand(string name) : ApplyGroupAttributesBaseCommand(name)
{
  /// <summary>
  /// Загрузить данные единичных объектов для диалога выбора
  /// </summary>
  /// <returns></returns>
  protected override bool LoadUnitItems()
  {
    if (this._groupObjId == null || this._groupObjId.ObjectID == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICompositionLoadService customService = sessionKeeper.Session.GetCustomService<ICompositionLoadService>();
      CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
      {
        new ObjInfoItem(this._groupObjId.ObjectID, this._groupObjId.ObjectType)
      }, (IEnumerable<int>) null, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechLinkGTPObjRelationID
      }, ObjInfoDbScheme.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) null, true, false, 1, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
      DataTable source = customService.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams);
      List<ObjInfoItem> objects = new List<ObjInfoItem>();
      if (source != null)
        new ObjInfoDbScheme().ParseItems((IEnumerable<DataRow>) source.AsEnumerable(), (ICollection<ObjInfoItem>) objects);
      if (objects.Count == 0)
        return false;
      this._unitInfoItems = objects;
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
    if (selectedUnitList == null || selectedUnitList.Count == 0 || selectedAttributes == null || selectedAttributes.Count == 0)
      return false;
    bool flag = false;
    List<ObjInfoItem> list = this._unitInfoItems.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (a => selectedUnitList.Contains(a.ObjectID))).ToList<ObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      try
      {
        session.StartLogHistory();
        foreach (KeyValuePair<ElementInfo, List<AttributeValues>> selectedAttribute in selectedAttributes)
        {
          if (selectedAttribute.Key.ElementKind == AttributableElements.Object)
            flag = this.SetGroupAttributesInObject(sessionKeeper.Session, list, selectedAttribute.Value);
        }
        this._modificationsList.AddRange((IEnumerable<CategoryValue>) session.GetModificationsHistoryList());
      }
      finally
      {
        session.StopLogHistory();
      }
    }
    return flag;
  }
}
