// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes.RouteElemClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;

/// <summary>Выбранный РЭ</summary>
public class RouteElemClass
{
  private long _procRouteID;
  private long _cehRouteID;
  private long _linkID;
  private long _objID;
  private long _templateOrderID;
  private long _routeElemOrderID;

  private void _initData()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="procRouteID">Ид. версии МО</param>
  /// <param name="cehRouteID">Ид. версии маршрута</param>
  /// <param name="linkID">Ид. связи</param>
  /// <param name="objID">Ид. версии объекта</param>
  public RouteElemClass(long procRouteID, long cehRouteID, long linkID, long objID)
  {
    this._initData();
    this._procRouteID = procRouteID;
    this._cehRouteID = cehRouteID;
    this._linkID = linkID;
    this._objID = objID;
  }

  /// <summary>Ид. версии МО</summary>
  public long ProcRouteID
  {
    get => this._procRouteID;
    set => this._procRouteID = value;
  }

  /// <summary>Ид. версии РМ</summary>
  public long CehRouteID
  {
    get => this._cehRouteID;
    set => this._cehRouteID = value;
  }

  /// <summary>Ид. связи</summary>
  public long LinkID
  {
    get => this._linkID;
    set => this._linkID = value;
  }

  /// <summary>Ид. версии объекта</summary>
  public long ObjID
  {
    get => this._objID;
    set => this._objID = value;
  }

  /// <summary>Сортировка для ШР</summary>
  public long TemplateOrderID
  {
    get => this._templateOrderID;
    set => this._templateOrderID = value;
  }

  /// <summary>Сортировка для РЭ</summary>
  public long RouteElemOrderID
  {
    get => this._routeElemOrderID;
    set => this._routeElemOrderID = value;
  }

  /// <summary>Custom comparer for sorting items</summary>
  internal class SortComparer : IComparer<RouteElemClass>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(RouteElemClass x, RouteElemClass y)
    {
      int num = Math.Sign(x.TemplateOrderID - y.TemplateOrderID);
      return num != 0 ? num : Math.Sign(x.RouteElemOrderID - y.RouteElemOrderID);
    }
  }
}
