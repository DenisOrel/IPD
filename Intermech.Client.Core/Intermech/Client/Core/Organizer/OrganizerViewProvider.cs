
// Type: Intermech.Client.Core.Organizer.OrganizerViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Провайдер для узлов "Органайзер" и "Задачи органайзера".
/// Необходим для того, чтобы подменить закладку "ChildrenView" на дочернюю ей "OrganizerChildrenView".
/// </summary>
public class OrganizerViewProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка</summary>
  private static bool _registeredView;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = ViewsInfo.Empty;
    if (!OrganizerViewProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("OrganizerChildrenView", LocalizationHolder.rm.GetString("Client_Core_ObjectsType_Projects"), "", "", "imgOrganizerTask", true, 0);
      OrganizerViewProvider._registeredView = true;
    }
    if (items.Count == 1)
    {
      views = new ViewsInfo();
      views.Add("OrganizerChildrenView", new ViewInfo(0, typeof (OrganizerChildrenView)));
      views.Suppress("ChildrenView", 3);
    }
    return views;
  }
}
