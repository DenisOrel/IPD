
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemesProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Провайдер закладок для узла "Схемы жизненных циклов"</summary>
public class LifeCycleSchemesProvider : IViewsProvider
{
  /// <summary>Получить список закладок</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    views.Add("ChildrenView", new ViewInfo(0, typeof (LifeCycleSchemesView)));
    return views;
  }
}
