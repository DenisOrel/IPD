
// Type: Intermech.Navigator.DBObjects.ObjectsVisibilityViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Провайдер для узлов-объектов, в составе которых может быть
/// атрибут "Видимость объекта"
/// </summary>
internal class ObjectsVisibilityViewProvider : IViewsProvider
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
    if (ObjectsVisibilityViewProvider._userAndRole == null)
      ObjectsVisibilityViewProvider._userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    if (!this.CanShowViews(items, services))
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ObjectsVisibilityView", new ViewInfo(4, typeof (ObjectsVisibilityView)));
    return views;
  }

  /// <summary>
  /// Метод анализирует коллекцию выделенных элементов пространства навигации, и пытается определить,
  /// можно ли показывать закладки провайдера ObjectsVisibilityViewProvider для этой коллекции
  /// </summary>
  /// <param name="items">Коллекция выделенных элементов пространства навигации</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>true, если закладки можно отображать</returns>
  private bool CanShowViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null || items.Count == 0)
      return false;
    int lcLevelId = MetaDataHelper.GetLCLevelID("cad00049-306c-11d8-b4e9-00304f19f545");
    for (int index = 0; index < items.Count; ++index)
    {
      if (!(items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1) || !MetaDataHelper.HasObjectTypeVisibilityAttr(itemData1.ObjectType) || !(items.GetItemData(index, typeof (IDBLCStepID)) is IDBLCStepID itemData2))
        return false;
      IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(itemData2.LCStepID);
      if (lcStep == null || lcStep.LevelID == lcLevelId)
        return false;
    }
    return true;
  }
}
