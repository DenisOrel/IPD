
// Type: Intermech.Navigator.Selections.Implementation.HiveViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Реализует провайдер закладок для корня дерева выборок.
/// </summary>
internal class HiveViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    IViewState service = services != null ? services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    int num = service == null ? 0 : ((service.ViewState & ViewStateFlags.InParametersCard) > ViewStateFlags.None ? 1 : 0);
    if (items.GetItemData(0, typeof (IDBObjectID)) != null)
      views.Add("ObjectProperties", new ViewInfo(4, 697, typeof (PropertiesView)));
    if (num == 0)
      views.Add("Thumbnails", new ViewInfo(0, typeof (ThumbnailView)));
    return views;
  }
}
