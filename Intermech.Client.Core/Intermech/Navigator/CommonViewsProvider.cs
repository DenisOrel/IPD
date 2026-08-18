
// Type: Intermech.Navigator.CommonViewsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Navigator;

/// <summary>
/// Реализует провайдер вьюшек, общих для всех категорий элементов
/// навигации.
/// </summary>
internal class CommonViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services) => ViewsInfo.Empty;
}
