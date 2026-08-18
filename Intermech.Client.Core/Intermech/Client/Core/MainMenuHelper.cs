
// Type: Intermech.Client.Core.MainMenuHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.Client.Core;

/// <summary>
/// Предоставляет интеллектуальные методы для вставки новых элементов в главное меню клиента IPS.
/// </summary>
public static class MainMenuHelper
{
  /// <summary>
  /// Вставляет команды в главное меню ниже указанного опорного пункта меню. Если опорного пункта нет, то вставка выполняется в середину меню на границе групп.
  /// </summary>
  /// <param name="menuName">Имя меню</param>
  /// <param name="baseItemName">Имя опорного пункта. Он используется только для задания позиции вставки</param>
  /// <param name="newItems">Вставляемые пункты меню</param>
  public static void InsertAfter(
    string menuName,
    string baseItemName,
    params ToolbarItemBase[] newItems)
  {
    if (menuName == null)
      throw new ArgumentNullException(nameof (menuName));
    if (newItems == null)
      throw new ArgumentNullException(nameof (newItems));
    if (newItems.Length == 0)
      return;
    BarManager service = ServiceUtils.GetService<BarManager>((object) ServicesManager.ServiceContainer, false);
    if (service == null)
      return;
    MenuBarItem menuBar = service.MenuBar.FindMenuBar(menuName);
    if (menuBar == null)
      return;
    int index = menuBar.Items.Count >> 1;
    if (!string.IsNullOrEmpty(baseItemName))
    {
      MenuItemBase menuItemBase = menuBar.FindItem(baseItemName);
      if (menuItemBase != null)
        index = menuItemBase.Index;
    }
    if (index < menuBar.Items.Count - 1)
    {
      ++index;
      while (index < menuBar.Items.Count && !menuBar.Items[index].BeginGroup)
        ++index;
    }
    foreach (ToolbarItemBase newItem in newItems)
      menuBar.Items.Insert(index++, newItem);
  }

  /// <summary>
  /// Проверяет возможность вставки команд в главное меню ниже указанного опорного пункта меню. Если опорного пункта нет, то метод возвращает false.
  /// </summary>
  /// <param name="menuName">Имя меню</param>
  /// <param name="baseItemName">Имя опорного пункта. Он используется только для задания позиции вставки</param>
  /// <returns>Результат проверки</returns>
  public static bool CanInsertAfter(string menuName, string baseItemName)
  {
    if (menuName == null)
      throw new ArgumentNullException(nameof (menuName));
    BarManager service = ServiceUtils.GetService<BarManager>((object) ServicesManager.ServiceContainer, false);
    if (service != null)
    {
      MenuBarItem menuBar = service.MenuBar.FindMenuBar(menuName);
      if (menuBar != null && !string.IsNullOrEmpty(baseItemName) && menuBar.FindItem(baseItemName) != null)
        return true;
    }
    return false;
  }
}
