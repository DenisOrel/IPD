// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Zagot.ProcRouteZagListViewDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.TechCard.Client.TcObjectsTypes.Process_Route;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Zagot;

/// <summary>
/// Диалог выбора маршрута обработки при создании заготовки
/// </summary>
public class ProcRouteZagListViewDlg : ProcRouteListBaseDlg
{
  /// <summary>Поиск заготовки для МО</summary>
  /// <param name="procRouteObjId">Ид. версии МО</param>
  /// <returns></returns>
  internal static long GetZagotObjId(long procRouteObjId)
  {
    long zagotObjId = 0;
    List<long> longList = ProcRouteListBaseDlg.LoadChildObjects(MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ZagotGUID), procRouteObjId);
    if (longList != null && longList.Count > 0)
      zagotObjId = longList[0];
    return zagotObjId;
  }

  /// <summary>Constructor</summary>
  /// <param name="objArtList">Ид. версий изделия</param>
  /// <param name="objChildId">Ид. версии дочернего объекта</param>
  /// <param name="procRouteId">Selected proc routes</param>
  public ProcRouteZagListViewDlg(List<long> objArtList, long objChildId, long[] procRouteId)
    : base(objArtList, objChildId, procRouteId)
  {
    this.InitializeData();
  }

  /// <summary>Initialize data</summary>
  private void InitializeData()
  {
    this._objChildTypeID = MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.ZagotGUID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objArtId"></param>
  /// <param name="objTpId"></param>
  /// <param name="procRouteId"></param>
  /// <returns></returns>
  public static bool ShowDialog(long objArtId, long objTpId, ref long procRouteId)
  {
    return ProcRouteListBaseDlg.ShowDialog(objArtId, objTpId, typeof (ProcRouteZagListViewDlg), ref procRouteId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objArtId"></param>
  /// <param name="objTpId"></param>
  /// <param name="procRouteId"></param>
  /// <returns></returns>
  public static bool ShowDialog(long objArtId, long objTpId, ref long[] procRouteId)
  {
    return ProcRouteListBaseDlg.ShowDialog(objArtId, objTpId, typeof (ProcRouteZagListViewDlg), ref procRouteId, true);
  }
}
