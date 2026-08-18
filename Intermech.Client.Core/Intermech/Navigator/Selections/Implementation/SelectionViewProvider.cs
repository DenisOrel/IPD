
// Type: Intermech.Navigator.Selections.Implementation.SelectionViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Провайдер закладок навигатора для элементов типа "Выборка", "Классификатор" и "Папка классификатора".
/// </summary>
internal sealed class SelectionViewProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    IViewState service = services != null ? services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    bool flag = service != null && (service.ViewState & ViewStateFlags.InParametersCard) > ViewStateFlags.None;
    int num = (items.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID).Value;
    if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00156-306c-11d8-b4e9-00304f19f545")).Contains(num))
      views.Add("SelectionPropertiesView", new ViewInfo(0, 783, typeof (SelectionPropertiesView)));
    if (num == MetaDataHelper.GetObjectTypeID("cad0014e-306c-11d8-b4e9-00304f19f545") || num == MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545"))
      views.Add("ClassificatorPropertiesView", new ViewInfo(0, 797, typeof (SelectionPropertiesView)));
    if (!flag)
      views.Add("Thumbnails", new ViewInfo(0, typeof (ThumbnailView)));
    if (num != MetaDataHelper.GetObjectTypeID(PortalConsts.objtypePortalSelections))
    {
      views.Add("SelectionViewObject", new ViewInfo(0, 785, typeof (Intermech.Navigator.SelectionView.SelectionView)));
      if (service != null && (service.ViewState & ViewStateFlags.NoCompositionView) != ViewStateFlags.NoCompositionView)
      {
        if (items.GetItemData(0, typeof (IBinding)) != null && !flag)
        {
          views.Add("ChildrenView", new ViewInfo(4, 779, typeof (ObjectsView)));
          if (items.GetItemData(0, typeof (IDBObjectTypeSelectionID)) is IDBObjectTypeSelectionID itemData2 && MetaDataHelper.IsObjectTypeChildOf(itemData2.BindedObjectTypeID, MetaDataHelper.GetObjectTypeID(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"))))
            views.Add("DocumentsThumbnailView", new ViewInfo(0, typeof (ThumbnailDocs)));
          else if (items.GetItemData(0, typeof (INodeID)) as INodeID is SelectionNodeID itemData1 && (itemData1.SelectionType == SelectionType.Archiv || itemData1.SelectionType == SelectionType.Archives))
            views.Add("DocumentsThumbnailView", new ViewInfo(0, typeof (ThumbnailDocs)));
          else if (items is NavigatorTreeViewSelectedItems viewSelectedItems && viewSelectedItems.Nodes[0].Handler is SelectionNode && MetaDataHelper.IsObjectTypeChildOf((viewSelectedItems.Nodes[0].Handler as SelectionNode).FilterObjectType, MetaDataHelper.GetObjectTypeID(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"))))
            views.Add("DocumentsThumbnailView", new ViewInfo(0, typeof (ThumbnailDocs)));
        }
        else
          views.Suppress("ChildrenView", 4);
      }
    }
    return views;
  }
}
