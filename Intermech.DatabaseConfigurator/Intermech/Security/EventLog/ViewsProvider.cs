// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.ViewsProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.EventLog;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search.SearchHistory;
using System;

#nullable disable
namespace Intermech.Security.EventLog;

internal class ViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("Events", new ViewInfo(0, 1109, typeof (EventsView)));
    views.Add("Config", new ViewInfo(0, 1112, typeof (EventsConfigView)));
    views.Add("Security", new ViewInfo(0, 710, typeof (EventLogSecurityView)));
    views.Add("LogStatistics", new ViewInfo(0, typeof (StatisticsView)));
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IGlobalIndexSettings)) is IGlobalIndexSettings customService && customService.IsSaveSearchQueryHistory)
      views.Add("SearchHistoryView", new ViewInfo(0, typeof (SearchHistoryView)));
    return views;
  }
}
