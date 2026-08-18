// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseViewsProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Indexes;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Imbase;

internal class ImbaseViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    INodeID itemId = items.GetItemID(0);
    ViewsInfo views = this.GetViewsForSelectionWindow(services, itemId.TypeID, itemId.CategoryID);
    if (views == null)
    {
      views = new ViewsInfo();
      IViewState service = services != null ? services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
      bool flag1 = service != null && (service.ViewState & ViewStateFlags.InParametersCard) > ViewStateFlags.None;
      if (itemId.CategoryID == Consts.CatalogsNodeCategoryID)
      {
        views.Add("ChildrenView", new ViewInfo(1, 876, typeof (CatalogsView)));
        if (!flag1)
          views.Add("Thumbnails", new ViewInfo(1, 2172, typeof (ImbaseThumbnailView)));
      }
      else if (itemId.CategoryID == Consts.TablesNodeCategoryID)
      {
        views.Add("ChildrenView", new ViewInfo(1, 876, typeof (TablesView)));
        if (!flag1)
          views.Add("Thumbnails", new ViewInfo(1, 2172, typeof (ImbaseThumbnailView)));
      }
      else if (itemId.CategoryID == Consts.RootNodeCategoryID)
      {
        views.Add("ChildrenView", new ViewInfo(1, 875, typeof (ImbaseRootView)));
        if (!flag1)
          views.Add("Thumbnails", new ViewInfo(1, 2172, typeof (ImbaseRootNodeThumbnailView)));
      }
      else if (itemId.CategoryID == 1)
      {
        if (itemId.TypeID == Consts.ImbaseFolderTypeID || itemId.TypeID == Consts.ImbaseCatalogTypeID)
        {
          views.Add("ChildrenView", new ViewInfo(1, 876, typeof (CatalogsView)));
          if (!flag1)
            views.Add("Thumbnails", new ViewInfo(3, 2172, typeof (ImbaseThumbnailView)));
          if (itemId.TypeID == Consts.ImbaseCatalogTypeID)
          {
            bool flag2 = false;
            if (itemId is NodeID nodeId)
              flag2 = this.CheckIndexViewVisible(nodeId.ObjectID);
            if (ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true).IsAdmin | flag2)
              views.Add("ImbaseIndexesView", new ViewInfo(3, 1754, typeof (ImbaseIndexesView)));
            views.Suppress("PDM.ApplicabilityView", 3);
          }
        }
        else if (itemId.TypeID == Consts.ImbaseTableRefTypeID || itemId.TypeID == Consts.ImbaseTableTypeID)
        {
          views.Suppress("ChildrenView", 3);
          views.Suppress("PDM.ContainsView", 3);
          views.Add("ImbaseTableView", new ViewInfo(3, 876, typeof (ImbaseTableView)));
          if (itemId.TypeID == Consts.ImbaseTableRefTypeID)
          {
            views.Add("ImbaseTableRefView", new ViewInfo(3, typeof (TableLinkPropertiesView)));
            views.Add("ImbaseTableEventsView", new ViewInfo(3, typeof (ImbaseTableEventsView)));
            views.Add("ImbaseSecutityTableView", new ViewInfo(3, typeof (SecurityTableView)));
          }
          if (itemId.TypeID == Consts.ImbaseTableTypeID)
            views.Add("TableRefserencesView", new ViewInfo(3, typeof (TableRefsView)));
        }
        else if (itemId.TypeID == Consts.ImbaseTableMixTypeID)
          views.Add("ImbaseTableMixView", new ViewInfo(3, typeof (ImbaseTableMixView)));
        else if (itemId.TypeID == Consts.ImbaseCatalogRecordTypeID)
          views.Suppress("PDM.ContainsView", 3);
        else if (itemId.TypeID == Consts.ImbaseFavoritesTypeID)
          views.Add("ChildrenView", new ViewInfo(1, typeof (CatalogsView)));
      }
      else if (itemId.CategoryID == 4 && (itemId.TypeID == Consts.ImbaseFolderTypeID || itemId.TypeID == Consts.ImbaseCatalogTypeID || itemId.TypeID == Consts.ImbaseTableTypeID || itemId.TypeID == Consts.ImbaseTableRefTypeID) && !flag1)
        views.Add("Thumbnails", new ViewInfo(3, 2172, typeof (ImbaseThumbnailView)));
    }
    return views;
  }

  private ViewsInfo GetViewsForSelectionWindow(
    IServiceProvider services,
    int typeID,
    int categoryID)
  {
    ViewsInfo forSelectionWindow = (ViewsInfo) null;
    if (services.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service && service.Services != null && service.Services.GetService(typeof (IRegistryInImbase)) is IRegistryInImbase)
    {
      forSelectionWindow = new ViewsInfo();
      if (typeID == Consts.ImbaseFolderTypeID || typeID == Consts.ImbaseTableRefTypeID)
        forSelectionWindow.Add("RegistryInImbaseView", new ViewInfo(1, -1, typeof (RegistryInImbaseView)));
      else if (typeID == Consts.ImbaseFavoritesTypeID)
        forSelectionWindow.Add("ChildrenView", new ViewInfo(1, typeof (CatalogsView)));
      if (categoryID == Consts.CatalogsNodeCategoryID)
        forSelectionWindow.Add("ChildrenView", new ViewInfo(1, 876, typeof (CatalogsView)));
      else if (categoryID == Consts.RootNodeCategoryID)
        forSelectionWindow.Add("ChildrenView", new ViewInfo(1, 875, typeof (ImbaseRootView)));
      forSelectionWindow.Suppress("PDM.ContainsView", 3);
      forSelectionWindow.Suppress("PDM.ApplicabilityView", 3);
      forSelectionWindow.Suppress("ObjectVisualizer", 3);
      forSelectionWindow.Suppress("ObjectFiles", 3);
      forSelectionWindow.Suppress("ObjectSecurity", 3);
      forSelectionWindow.Suppress("ObjectEvents", 3);
      forSelectionWindow.Suppress("ObjectsVisibilityView", 3);
      forSelectionWindow.Suppress("RelationProperties", 3);
      forSelectionWindow.Suppress("ApplicabilityView", 3);
    }
    return forSelectionWindow;
  }

  private bool CheckIndexViewVisible(long objId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objId) is IDBSecurity dbSecurity && dbSecurity.CheckAccess(ActionType.ManageCatalogIndexes, false, false);
  }
}
