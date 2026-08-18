
// Type: Intermech.Navigator.DBObjects.ProjectTeamsViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Projects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер для узла с объектами типа "Проекты"</summary>
internal class ProjectTeamsViewProvider : IViewsProvider
{
  /// <summary>Ссылка на текущие настройки пользователя</summary>
  private static ICurrentUserAndRole _userAndRole;

  /// <summary>
  /// Возвращает контейнер со сведениями о закладках, которые должны быть
  /// выведены на экран в указанном контексте, а также о закладках других
  /// провайдеров, вывод которых должен быть подавлен.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="services">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    if (ProjectTeamsViewProvider._userAndRole == null)
      ProjectTeamsViewProvider._userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
      IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
      if (dbObject == null || !(dbObject is IDBProjectObject dbProjectObject))
        return ViewsInfo.Empty;
      if (!dbProjectObject.IsProjectParticipant(ProjectTeamsViewProvider._userAndRole.UserID))
        return ViewsInfo.Empty;
    }
    ViewsInfo views = new ViewsInfo();
    views.Add("ProjectTeamsView", new ViewInfo(4, 870, typeof (ProjectTeamsView)));
    return views;
  }
}
