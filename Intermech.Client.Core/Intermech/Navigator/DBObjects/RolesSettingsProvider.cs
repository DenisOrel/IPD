
// Type: Intermech.Navigator.DBObjects.RolesSettingsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Провайдер для редактора дополнительных настроек ролей пользователей
/// </summary>
public sealed class RolesSettingsProvider : IViewsProvider
{
  /// <summary>Получить список закладок</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>Список закладок</returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count <= 0)
      return ViewsInfo.Empty;
    bool flag = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
        IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
        if (dbObject != null && dbObject is IDBSecurity dbSecurity)
          flag &= dbSecurity.CheckAccess(ActionType.Edit, false, false);
        if (!flag)
          return ViewsInfo.Empty;
      }
    }
    if (!flag)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("RolesContextMenusView", new ViewInfo(0, 1066, typeof (RolesContextMenusView)));
    views.Add("RolesViewsView", new ViewInfo(0, 1066, typeof (RolesViewsView)));
    if (items.Count == 1)
      views.Add("RolesPluginsView", new ViewInfo(0, 1103, typeof (RolesPluginsView)));
    return views;
  }
}
