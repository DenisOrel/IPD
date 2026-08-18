
// Type: Intermech.Navigator.DBObjects.EditingContextsViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер для узла с объектами типа "Проекты"</summary>
internal class EditingContextsViewProvider : IViewsProvider
{
  /// <summary>Ссылка на текущие настройки пользователя</summary>
  private static ICurrentUserAndRole _userAndRole;
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _registered;

  /// <summary>
  /// Возвращает контейнер со сведениями о закладках, которые должны быть
  /// выведены на экран в указанном контексте, а также о закладках других
  /// провайдеров, вывод которых должен быть подавлен.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="services">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !MetaDataHelper.IsObjectTypeEditingContext(itemData.ObjectType))
      return ViewsInfo.Empty;
    IViewState service = services != null ? services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    int num = service == null ? 0 : ((service.ViewState & ViewStateFlags.InParametersCard) > ViewStateFlags.None ? 1 : 0);
    if (EditingContextsViewProvider._userAndRole == null)
      EditingContextsViewProvider._userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    ViewsInfo views = new ViewsInfo();
    views.Add("EditingContextsView", new ViewInfo(4, 765, typeof (EditingContextsView)));
    if (EditingContextsViewProvider._registered)
      return views;
    AdjustableViewsHelper.RegisterView("EditingContextsView", LocalizationHolder.rm.GetString("Client.Core_1225"), LocalizationHolder.rm.GetString("Client.Core_1226"), "Intermech.Navigator", "imgObjectsFilter", true, 15);
    EditingContextsViewProvider._registered = true;
    return views;
  }
}
