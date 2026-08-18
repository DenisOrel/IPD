
// Type: Intermech.Client.Core.Organizer.OrganizerRootViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Общий провайдер (отрабатывает для всех узлов навигатора).
/// Необходим для того, чтобы для узлов органайзера загружать первой закладку с календарем.
/// </summary>
public class OrganizerRootViewProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _registeredView;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider provider)
  {
    ViewsInfo views = ViewsInfo.Empty;
    if (!OrganizerRootViewProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("OrganizerCalendarView", LocalizationHolder.rm.GetString("Organizer.CalendarView.Caption"), "", "", "imgOrganizerTask", true, 0);
      OrganizerRootViewProvider._registeredView = true;
    }
    if (items.Count == 1 && items.GetItemData(0, typeof (IOrganizerNode)) is IOrganizerNode && items is NavigatorTreeViewSelectedItems)
    {
      views = new ViewsInfo();
      views.Add("OrganizerCalendarView", new ViewInfo(0, typeof (OrganizerCalendarView)));
    }
    return views;
  }
}
