// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.IViewsProvider
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Позволяет реализовать провайдер закладок, встраиваемых в навигатор.
/// Провайдер должен проанализировать информацию о контексте, в котором будут
/// показаны закладки, и вернуть контейнер со сведениями о допустимых закладках.
/// </summary>
public interface IViewsProvider
{
  /// <summary>
  /// Возвращает контейнер со сведениями о закладках, которые должны быть
  /// выведены на экран в указанном контексте, а также о закладках других
  /// провайдеров, вывод которых должен быть подавлен.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="services">Контейнер сервисов, которыми может пользоваться закладка.</param>
  ViewsInfo GetViews(ISelectedItems items, IServiceProvider services);
}
