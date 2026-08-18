
// Type: Intermech.Navigator.CustomNode.MultipleObjectsViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator.CustomNode;

/// <summary>Провайдер базовых вьюшек для элементов "Тип объекта" из пространства навигации.</summary>
internal class MultipleObjectsViewsProvider : IViewsProvider
{
  /// <summary>Возвращает контейнер со сведениями о закладках, которые должны быть выведены на экран в указанном контексте, а также о
  /// закладках других провайдеров, вывод которых должен быть подавлен.</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="services">Контейнер сервисов, которыми может пользоваться закладка.</param>
  /// <returns>The views</returns>
  public ViewsInfo GetViews([NotNull] ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ChildrenView", new ViewInfo(0, typeof (MultipleObjectsView)));
    return views;
  }
}
