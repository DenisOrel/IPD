
// Type: Intermech.Client.Core.Organizer.OrganizerTaskObjectsViewProvider
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
/// Провайдер для объектов типа "Задачи органайзера".
/// Необходим для того, чтобы отобразить закладку с формой объекта.
/// Если в дальнейшем форма будет создана средствами IPS, то провайдер можно будет убрать
/// (закладка с формой будет добавляться автоматически).
/// </summary>
public class OrganizerTaskObjectsViewProvider : IViewsProvider
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
    if (!OrganizerTaskObjectsViewProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("OrganizerTaskView", LocalizationHolder.rm.GetString("Client.Core.ObjectsForm"), "", "", "", true, 0);
      AdjustableViewsHelper.RegisterView("OrganizerPropertiesView", LocalizationHolder.rm.GetString("Client.Core_146"), "", "", "", true, 10);
      OrganizerTaskObjectsViewProvider._registeredView = true;
    }
    if (items.Count == 1)
    {
      views = new ViewsInfo();
      views.Add("OrganizerTaskView", new ViewInfo(0, typeof (OrganizerTaskView)));
      views.Add("OrganizerPropertiesView", new ViewInfo(0, typeof (OrganizerPropertiesView)));
      views.Suppress("ObjectProperties", 3);
    }
    return views;
  }
}
