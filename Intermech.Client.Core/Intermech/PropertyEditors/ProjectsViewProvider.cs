
// Type: Intermech.PropertyEditors.ProjectsViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.PropertyEditors;

/// <summary>Провайдер для узлов "Проекты".</summary>
/// <remark>
/// Необходим для того, чтобы подменить закладку "ChildrenView" на дочернюю ей "ProjectsChildrenView".
/// </remark>
public class ProjectsViewProvider : IViewsProvider
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
    if (!ProjectsViewProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("ProjectsChildrenView", LocalizationHolder.rm.GetString("Organaizer_TaskCaption"), "", "", "imgProjects", true, 0);
      ProjectsViewProvider._registeredView = true;
    }
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ProjectsChildrenView", new ViewInfo(0, typeof (ProjectsChildrenView)));
    return views;
  }
}
