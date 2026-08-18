
// Type: Intermech.Client.Core.ImageLibraryViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Thumbnail;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Client.Core;

/// <summary>Summary description for ImageLibraryViewProvider.</summary>
public class ImageLibraryViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    IViewState service = services != null ? services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    bool flag = service != null && (service.ViewState & ViewStateFlags.InParametersCard) > ViewStateFlags.None;
    ViewsInfo views = new ViewsInfo();
    INodeID itemId = items.GetItemID(0);
    if (itemId.CategoryID == Intermech.Navigator.Consts.ImageLibraryNodeTypeID)
    {
      if (!flag)
      {
        views.Add("Thumbnails", new ViewInfo(1, 2172, typeof (ThumbnailView)));
        views.Add("ChildrenView", new ViewInfo(1, typeof (ImageLibraryView)));
      }
    }
    else if (itemId.CategoryID == 1 && itemId.TypeID == Intermech.Client.Core.Thumbnail.Consts.ImageLibraryFolderTypeID)
    {
      if (!flag)
      {
        views.Add("Thumbnails", new ViewInfo(3, 2172, typeof (ThumbnailView)));
        views.Add("ChildrenView", new ViewInfo(0, typeof (ImageLibraryView)));
      }
    }
    else if (itemId.CategoryID == 4 && itemId.TypeID == Intermech.Client.Core.Thumbnail.Consts.ImageLibraryItemTypeID && !flag)
      views.Add("Thumbnails", new ViewInfo(3, 2172, typeof (ThumbnailView)));
    return views;
  }
}
