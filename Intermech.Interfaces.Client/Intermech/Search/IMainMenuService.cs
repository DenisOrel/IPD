// Decompiled with JetBrains decompiler
// Type: Intermech.Search.IMainMenuService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Bars;
using System;

#nullable disable
namespace Intermech.Search;

/// <summary>
/// Сервис для работы с главным меню IPS, регистрирует и автоматически размещает пункты меню в указанные позиции
/// </summary>
public interface IMainMenuService
{
  MenuBar MenuBar { get; }

  event EventHandler AfterMainMenuChanged;

  /// <summary>Зарегистрирвовать пункты меню</summary>
  /// <param name="mainMenuItemSite">Место</param>
  /// <param name="mainMenuItemPosition">Позиция</param>
  /// <param name="menuItems">Пункты меню</param>
  void RegisterMenuItems(
    MainMenuItemSite mainMenuItemSite,
    MainMenuItemPosition mainMenuItemPosition,
    params MenuButtonItem[] menuItems);

  /// <summary>
  /// Зарегистрировать группу пунктов меню (порядок пунктов в группе не меняется при обновлении меню)
  /// </summary>
  /// <param name="mainMenuItemSite">Место</param>
  /// <param name="mainMenuItemPosition">Позиция</param>
  /// <param name="disableAutoBeginGroup">Запретить автоматическую установку свойства BeginGroup для первого элемента в группе</param>
  /// <param name="menuItems">Пункты меню</param>
  void RegisterMenuItemsGroup(
    MainMenuItemSite mainMenuItemSite,
    MainMenuItemPosition mainMenuItemPosition,
    bool disableAutoBeginGroup,
    params MenuButtonItem[] menuItems);

  /// <summary>Снять пункты меню с регистрации</summary>
  /// <param name="menuItems">Пункты меню</param>
  void UnregiterMenuItems(params MenuButtonItem[] menuItems);

  void SuppressRebuildMainMenu();

  void ResumeRebuildMainMenu();
}
