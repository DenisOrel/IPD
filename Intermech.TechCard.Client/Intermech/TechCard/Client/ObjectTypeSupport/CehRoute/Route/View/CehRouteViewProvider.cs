// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.View.CehRouteViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.View;

/// <summary>
/// Провайдер закладок для объекта типа "Расцеховочный маршрут"
/// </summary>
internal class CehRouteViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null || items.Count != 1)
      return ViewsInfo.Empty;
    ServiceUtils.GetService<ISelectedItems>((object) services, false);
    ViewsInfo views = new ViewsInfo();
    IDBTypedObjectID parentData = items.GetParentData<IDBTypedObjectID>(0, false);
    if (parentData == null || MetaDataHelper.IsObjectTypeChildOf(parentData.ObjectType, TechCardConsts.ObjectTypes.ProcRoutingID))
      views.Add("TechCard.CehRouteEntryListView", new ViewInfo(0, typeof (CehRouteEntryListView)));
    return views;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="factory"></param>
  public static void RegisterViewProvider([NotNull] IFactory factory)
  {
    factory.AddViewsProvider(1, TechCardConsts.ObjectTypes.CehRouteID, (IViewsProvider) new CehRouteViewProvider());
  }
}
