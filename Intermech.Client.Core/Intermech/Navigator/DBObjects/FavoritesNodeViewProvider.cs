
// Type: Intermech.Navigator.DBObjects.FavoritesNodeViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер для узла навигатора Избранное</summary>
public class FavoritesNodeViewProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _isViewRegistered;

  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = ViewsInfo.Empty;
    if (!FavoritesNodeViewProvider._isViewRegistered)
    {
      AdjustableViewsHelper.RegisterView("FavoritesChildrenView", LocalizationHolder.rm.GetString("Client.Core_1351"), "", "", "imgFavorites", true, 0);
      FavoritesNodeViewProvider._isViewRegistered = true;
    }
    if (items.Count == 1)
    {
      views = new ViewsInfo();
      views.Add("FavoritesChildrenView", new ViewInfo(0, typeof (FavoritesChildrenView)));
      views.Suppress("ChildrenView", 3);
    }
    return views;
  }
}
