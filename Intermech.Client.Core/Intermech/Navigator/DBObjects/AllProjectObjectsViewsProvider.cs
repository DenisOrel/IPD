
// Type: Intermech.Navigator.DBObjects.AllProjectObjectsViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Провайдер закладок для узла "Все объекты проекта"</summary>
public class AllProjectObjectsViewsProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _registeredView;

  /// <summary>Получить список закладок</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ChildrenView", new ViewInfo(0, 866, typeof (AllProjectObjectsObjectsView)));
    if (AllProjectObjectsViewsProvider._registeredView)
      return views;
    AdjustableViewsHelper.RegisterView("AllProjectObjectsObjectsView", AllProjectObjectsNode.AllProjectObjectsNodeName, "", "", "imgAllProjectObjects", true, 0);
    AllProjectObjectsViewsProvider._registeredView = true;
    return views;
  }
}
