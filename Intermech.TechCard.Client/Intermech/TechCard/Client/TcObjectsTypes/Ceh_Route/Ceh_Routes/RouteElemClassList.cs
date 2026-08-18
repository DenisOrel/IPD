// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes.RouteElemClassList
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;

/// <summary>Список выбранных РЭ</summary>
public class RouteElemClassList : List<RouteElemClass>
{
  /// <summary>Поиск элемента по параметрам</summary>
  /// <param name="procRouteID">Ид. версии МО</param>
  /// <param name="cehRouteID">Ид. версии маршрута</param>
  /// <param name="linkID">Ид. связи</param>
  /// <param name="objID">Ид. версии РЭ</param>
  /// <returns></returns>
  public int IndexOf(long procRouteID, long cehRouteID, long linkID, long objID)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      RouteElemClass routeElemClass = this[index];
      if (routeElemClass.ProcRouteID == procRouteID && routeElemClass.CehRouteID == cehRouteID && routeElemClass.LinkID == linkID && routeElemClass.ObjID == objID)
        return index;
    }
    return -1;
  }
}
