
// Type: Intermech.Client.Core.FormDesigner.External.Navigator.ExternalEditorParamsViewProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Client.Core.FormDesigner.External.Navigator;

/// <summary>
/// 
/// </summary>
internal class ExternalEditorParamsViewProvider : IViewsProvider
{
  /// <summary>Зарегистрирована ли закладка.</summary>
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
    if (!ExternalEditorParamsViewProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("ExternalEditorParamsView", LocalizationHolder.rm.GetString("Client.Core_179"), "", "", "", true, 0);
      ExternalEditorParamsViewProvider._registeredView = true;
    }
    if (items.Count == 1)
    {
      views = new ViewsInfo();
      views.Add(LocalizationHolder.rm.GetString("Client.Core_179"), new ViewInfo(0, 1613, typeof (ExternalEditorParamsView)));
    }
    return views;
  }
}
