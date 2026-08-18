// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes.ProcMoCehRouteListViewDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.TechCard.Client.TcObjectsTypes.Process_Route;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;

/// <summary>Диалог выбора "Маршрута обработки(МО)"</summary>
public class ProcMoCehRouteListViewDlg : ProcRouteListBaseDlg
{
  /// <summary>Constructor</summary>
  /// <param name="objArtList">Ид. версий изделия</param>
  /// <param name="objChildId">Ид. версии дочернего объекта</param>
  /// <param name="procRouteId">Selected proc routes</param>
  public ProcMoCehRouteListViewDlg(List<long> objArtList, long objChildId, long[] procRouteId)
    : base(objArtList, objChildId, procRouteId)
  {
    this.InitializeData();
  }

  /// <summary>Initialize data</summary>
  private void InitializeData()
  {
    this._objChildTypeID = MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.CehRouteGUID);
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
    return ProcRouteListBaseDlg.ShowDialog(objArtId, objTpId, typeof (ProcMoCehRouteListViewDlg), ref procRouteId);
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
    return ProcRouteListBaseDlg.ShowDialog(objArtId, objTpId, typeof (ProcMoCehRouteListViewDlg), ref procRouteId, true);
  }
}
