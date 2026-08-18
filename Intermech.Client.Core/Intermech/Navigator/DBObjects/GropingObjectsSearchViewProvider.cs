
// Type: Intermech.Navigator.DBObjects.GropingObjectsSearchViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Провайдер для узлов, которые могут входить в состав группирующих объектов
/// или содержат атрибут "Номер группы изменений"
/// </summary>
internal class GropingObjectsSearchViewProvider : IViewsProvider
{
  /// <summary>Получить список закладок</summary>
  /// <param name="items">Выделенные элементы</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>Список закладок</returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count == 0 || services == null || ((services.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 536870913L /*0x20000001*/) != 0L || services.GetService(typeof (ContextsSearchView)) is ContextsSearchView)
      return ViewsInfo.Empty;
    List<int> intList = new List<int>();
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData && MetaDataHelper.HasObjectTypeGrouppedRelTypes(itemData.Value))
        intList.Add(itemData.Value);
      if (intList.Count > 0)
        break;
    }
    if (intList.Count == 0)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ContextsSearchView", new ViewInfo(4, 809, typeof (ContextsSearchView)));
    return views;
  }
}
