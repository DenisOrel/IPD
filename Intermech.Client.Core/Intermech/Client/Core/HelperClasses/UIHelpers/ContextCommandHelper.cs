
// Type: Intermech.Client.Core.HelperClasses.UIHelpers.ContextCommandHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.HelperClasses.UIHelpers;

/// <summary>
/// 
/// </summary>
public static class ContextCommandHelper
{
  /// <summary>
  /// 
  /// </summary>
  private static int Def_Min_Checked_Nodes = 1;

  /// <summary>
  /// Получение ISelectedItems для "checked" элементов навигатора
  /// </summary>
  /// <param name="viewServices"></param>
  /// <param name="defaultValue">Значение "по молчанию" для случая, когда "checked" узлов нет</param>
  /// <returns></returns>
  public static ISelectedItems GetCheckedItems(
    IServiceProvider viewServices,
    ISelectedItems defaultValue = null)
  {
    return ContextCommandHelper.GetCheckedItems(viewServices, defaultValue, ContextCommandHelper.Def_Min_Checked_Nodes);
  }

  /// <summary>
  /// Получение ISelectedItems для "checked" элементов навигатора
  /// </summary>
  /// <param name="viewServices"></param>
  /// <param name="defaultValue">Значение "по молчанию" для случая, когда "checked" узлов нет</param>
  /// <param name="minObjCount">Мин. допустимое количество "checked" узлов</param>
  /// <returns></returns>
  public static ISelectedItems GetCheckedItems(
    IServiceProvider viewServices,
    ISelectedItems defaultValue,
    int minObjCount)
  {
    if (viewServices == null)
      return defaultValue;
    IViewState service1 = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    if (((service1 != null ? (long) service1.ViewState : 0L) & 128L /*0x80*/) == 0L)
      return defaultValue;
    NavigatorTreeView service2 = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    return service2 == null || service2.CheckedItems.Count < minObjCount ? defaultValue : service2.CheckedItems;
  }
}
