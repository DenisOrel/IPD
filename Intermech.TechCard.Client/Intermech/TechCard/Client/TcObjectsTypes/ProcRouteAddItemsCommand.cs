// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.ProcRouteAddItemsCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Settings.TechCardParams;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>
/// Реализация команды "Добавить в состав" для маршрута обработки
/// </summary>
internal class ProcRouteAddItemsCommand : AddObjectCommand
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <returns></returns>
  protected override bool CheckTargetObjectAllowModification(ObjInfoItem targetObjInfo)
  {
    ProcRouteAddItemsCommand.CheckProcRouteObjectAllowCompositionModification(targetObjInfo, (IEnumerable<IDBTypedObjectID>) this._selectedObjInfoItems);
    return base.CheckTargetObjectAllowModification(targetObjInfo);
  }

  /// <summary>
  /// Проверка допустимости добавления в состав заготовок, РМ, ТП в соотв. с текущими настройками
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <param name="childObjectItems"></param>
  /// <returns></returns>
  internal static void CheckProcRouteObjectAllowCompositionModification(
    ObjInfoItem targetObjInfo,
    IEnumerable<IDBTypedObjectID> childObjectItems)
  {
    if (!(childObjectItems is IDBTypedObjectID[] source))
      source = childObjectItems.ToArray<IDBTypedObjectID>();
    bool flag1 = ((IEnumerable<IDBTypedObjectID>) source).Any<IDBTypedObjectID>((System.Func<IDBTypedObjectID, bool>) (item => MetaDataHelper.IsObjectTypeChildOf(item.ObjectType, TechCardConsts.ObjectTypes.ZagotID)));
    bool flag2 = ((IEnumerable<IDBTypedObjectID>) source).Any<IDBTypedObjectID>((System.Func<IDBTypedObjectID, bool>) (item => MetaDataHelper.IsObjectTypeChildOf(item.ObjectType, TechCardConsts.ObjectTypes.CehRouteID)));
    bool flag3 = ((IEnumerable<IDBTypedObjectID>) source).Any<IDBTypedObjectID>((System.Func<IDBTypedObjectID, bool>) (item => MetaDataHelper.IsObjectTypeChildOf(item.ObjectType, TechCardConsts.ObjectTypes.TechProcBaseID)));
    if ((!flag1 || !TechCardParamsHelper.TechParams.ProcessRoute.UniqueBillet) && (!flag2 || !TechCardParamsHelper.TechParams.ProcessRoute.UniqueCehRoute) && !flag3)
      return;
    string caption;
    DataTable childSostavData;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      caption = sessionKeeper.Session.GetObjectInfo(targetObjInfo.ObjectID).Caption;
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      };
      childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
      {
        targetObjInfo
      }, sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) columns);
    }
    List<string> values = new List<string>();
    if (childSostavData == null)
      return;
    if (flag1 && TechCardParamsHelper.TechParams.ProcessRoute.UniqueBillet && childSostavData.AsEnumerable().Any<DataRow>((System.Func<DataRow, bool>) (row => MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(row[0]), TechCardConsts.ObjectTypes.ZagotID))))
    {
      string format = LocalizationHolder.rm.GetString(sc_19546.ssp_techcard_19547());
      values.Add(string.Format(format, (object) caption, (object) targetObjInfo.ObjectID));
    }
    if (flag2 && TechCardParamsHelper.TechParams.ProcessRoute.UniqueCehRoute && childSostavData.AsEnumerable().Any<DataRow>((System.Func<DataRow, bool>) (row => MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(row[0]), TechCardConsts.ObjectTypes.CehRouteID))))
    {
      string format = LocalizationHolder.rm.GetString(sc_19546.ssp_techcard_19548());
      values.Add(string.Format(format, (object) caption, (object) targetObjInfo.ObjectID));
    }
    if (values.Count != 0)
      throw new Exception(string.Join(Environment.NewLine, (IEnumerable<string>) values));
  }

  public ProcRouteAddItemsCommand()
    : base()
  {
  }
}
