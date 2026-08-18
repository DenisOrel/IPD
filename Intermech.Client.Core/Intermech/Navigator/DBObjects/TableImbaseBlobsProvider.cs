
// Type: Intermech.Navigator.DBObjects.TableImbaseBlobsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

internal class TableImbaseBlobsProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _registeredView;

  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (!TableImbaseBlobsProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("TableImbaseBlobsView", LocalizationHolder.rm.GetString("Client.Core_322"), "", "", "", true, 0);
      TableImbaseBlobsProvider._registeredView = true;
    }
    ViewsInfo views = new ViewsInfo();
    views.Add("TableImbaseBlobsView", new ViewInfo(0, typeof (TableImbaseBlobsView)));
    return views;
  }
}
