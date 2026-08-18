// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.Navigator.XmlViewsProvider
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.XmlExchange.Client.Navigator;

/// <summary>Провайдер закладок</summary>
internal class XmlViewsProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static volatile bool _registeredView;

  /// <summary>Создать провайдер</summary>
  public XmlViewsProvider()
  {
    if (XmlViewsProvider._registeredView)
      return;
    AdjustableViewsHelper.RegisterView("XmlImportSettingsView", "Настройка импорта из XML", "Закладка для настройки параметров импорта информации из пакетов XML", "Intermech.XmlExchange.Client", "XML.imgBriefcaseImport", true, 0);
    AdjustableViewsHelper.RegisterView("XmlExportSettingsView", "Настройка экспорта в XML", "Закладка для настройки параметров экспорта информации в пакеты XML", "Intermech.XmlExchange.Client", "XML.imgBriefcaseExport", true, 0);
    XmlViewsProvider._registeredView = true;
  }

  /// <summary>
  /// Возвращает контейнер со сведениями о закладках, которые должны быть
  /// выведены на экран в указанном контексте, а также о закладках других
  /// провайдеров, вывод которых должен быть подавлен.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="services">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1 || services == null || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID(XmlExchangeConsts.Common.ImportSettObjTypeGuid);
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID(XmlExchangeConsts.Common.ExportSettObjTypeGuid);
    if (itemData.ObjectType == objectTypeId1)
      views.Add("XmlImportSettingsView", new ViewInfo(3, -1, typeof (XmlImportSettingsView)));
    if (itemData.ObjectType == objectTypeId2)
      views.Add("XmlExportSettingsView", new ViewInfo(3, -1, typeof (XmlExportSettingsView)));
    return views;
  }
}
