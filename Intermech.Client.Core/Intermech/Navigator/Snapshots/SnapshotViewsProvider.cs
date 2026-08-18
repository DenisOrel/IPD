
// Type: Intermech.Navigator.Snapshots.SnapshotViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.Snapshots;

/// <summary>провайдер закладок для итераций</summary>
public class SnapshotViewsProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _registeredView;

  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (!SnapshotViewsProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("SnapshotProperty", LocalizationHolder.rm.GetString("Client.Core_1408"), "", "", "imgProp", true, 0);
      AdjustableViewsHelper.RegisterView("SnapshotConsist", LocalizationHolder.rm.GetString("Client.Core_1406"), "", "", "imgContains", true, 0);
      SnapshotViewsProvider._registeredView = true;
    }
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add(LocalizationHolder.rm.GetString("Client.Core_1408"), new ViewInfo(0, typeof (SnapshotProperty)));
    views.Add(LocalizationHolder.rm.GetString("Client.Core_1406"), new ViewInfo(0, typeof (SnapshotConsist)));
    return views;
  }
}
