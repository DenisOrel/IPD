// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.AutoPlaceInArchiveView.AutoPlaceInArchiveProvider
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Archives.AutoPlaceInArchiveView;

/// <summary>
/// Провайдер для регистрации закладки "Автоматическое размещение"
/// </summary>
internal class AutoPlaceInArchiveProvider : IViewsProvider
{
  /// <summary>
  /// Возвращает контейнер со сведениями о закладках, которые должны быть
  /// выведены на экран в указанном контексте, а также о закладках других
  /// провайдеров, вывод которых должен быть подавлен.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="services">Контейнер сервисов, которыми может пользоваться закладка.</param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    if (items == null || items.Count != 1)
      return ViewsInfo.Empty;
    views.Add("AutoPlaceInArchiveView", new ViewInfo(0, typeof (Intermech.Archives.AutoPlaceInArchiveView.AutoPlaceInArchiveView)));
    return views;
  }
}
