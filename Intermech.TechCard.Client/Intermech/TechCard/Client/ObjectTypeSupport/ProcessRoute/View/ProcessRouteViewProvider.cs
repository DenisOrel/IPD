// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.View.ProcessRouteViewProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.View;

/// <summary>
/// Провайдер закладок для объекта типа "Маршрут обработки"
/// </summary>
internal class ProcessRouteViewProvider : IViewsProvider
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
    ViewsInfo views = new ViewsInfo();
    views.Add("TechCard.ProcessRouteEntryListView", new ViewInfo(0, typeof (ProcessRouteEntryListView)));
    return views;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="factory"></param>
  public static void RegisterViewProvider([NotNull] IFactory factory)
  {
    factory.AddViewsProvider(1, TechCardConsts.ObjectTypes.ProcRoutingID, (IViewsProvider) new ProcessRouteViewProvider());
  }
}
