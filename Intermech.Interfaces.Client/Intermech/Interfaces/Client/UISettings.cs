// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UISettings
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Настройки пользовательского интерфейса</summary>
public static class UISettings
{
  /// <summary>
  /// Первый запуск IPS (либо запуск с очищенной конфигурацией пользователя)
  /// </summary>
  public static bool FirstTimeRunning = false;
  /// <summary>Режим восстановления сохранённых окон</summary>
  public static DocumentRestoreMode RestoreDocumentWindows;
  /// <summary>Полноэкранный режим</summary>
  public static bool FullScreenMode;
  /// <summary>Обновлять неактивные окна</summary>
  public static bool AutoupdateNonActiveWindows;
  /// <summary>
  /// Если включен этот режим, то при вставке объектов будут рассылаться управляемые события
  /// </summary>
  public static bool DragDropNotofications;
  /// <summary>Запрос на закрытие приложения</summary>
  public static bool AskOnExit;
  /// <summary>
  /// Показывать идентификаторы версий объектов в заголовках
  /// </summary>
  public static NavigatorCaptionVersionsMode ShowVersionIDs;
  /// <summary>
  /// Показывать краткие наименования атрибутов в заголовках столбцов в списках "Навигатора"
  /// </summary>
  public static bool ShowShortAttributeNames;
  /// <summary>Показывать заставку при старте приложения</summary>
  public static bool ShowSplash;
  /// <summary>Вызывать действия по умолчанию для</summary>
  public static bool RunDefaultAction;
  /// <summary>
  /// Автоматически брать на изменение вновь созданные объекты
  /// </summary>
  public static readonly bool AutoCheckOutNewObjects = true;
  /// <summary>Режим отображения заголовков окон "Навигатора"</summary>
  public static NavigatorWindowCaptionsMode NavigatorWindowCaptionsMode;
  /// <summary>
  /// Показывать ли протокол подбора версий в подсказках к значкам статусов
  /// </summary>
  public static bool ShowVersionsLog;
  /// <summary>
  /// Показывать ли в гридах с объектами дополнительную колонку с версией объекта
  /// </summary>
  public static bool ShowGridChkoutColumn;
  /// <summary>
  /// Показывать ли в деревьях с объектами дополнительную колонку с версией объекта
  /// </summary>
  public static bool ShowTreeChkoutColumn;
  /// <summary>Способ отображения информации о версиях объектов</summary>
  public static NavigatorWindowBaseVersionsMode NavigatorWindowBaseVersionsMode;
  /// <summary>Всегда переключаться на первые закладки в Навигаторе</summary>
  public static bool AlwaysShowFirstTab = false;
  /// <summary>Способ обработки гиперссылок в элементах Навигатора</summary>
  public static NavigatorLinksMode NavigatorLinksMode;
  public static bool SwitchToCard;

  public static bool SaveSelectedChildrenViewObjectFilter { get; set; }

  public static Guid? SelectedChildrenViewObjectFilter { get; set; }

  public static bool SearchInIndexSubstring { get; set; }

  public static bool ShowSelectionsTabsForObjectTypes { get; set; }

  /// <summary>Отображать объединенные выборки</summary>
  public static bool ShowUnitedSelections { get; set; }

  /// <summary>Отображать папку Избранное в Навигаторе</summary>
  public static bool ShowFavoritesFolder { get; set; }

  public static bool DisableChildrenViewGrouping { get; set; }

  public static bool ShowListObjectTypes4CreatingObject { get; set; }

  public static bool HighlightCyrillicSimilarLatinCharacters { get; set; }

  public static Color CyrillicSimilarLatinCharacterHighlightColor { get; set; } = Color.Red;

  public static bool HighlightLatinSimilarCyrillicCharacters { get; set; }

  public static Color LatinSimilarCyrillicCharacterHighlightColor { get; set; } = Color.Blue;

  public static int[] AllowableForHighlightingSimilarCharactersObjectTypes { get; set; } = new int[0];

  /// <summary>Статический конструктор</summary>
  static UISettings()
  {
    UISettings.RestoreDocumentWindows = DocumentRestoreMode.CreateProxy;
    UISettings.AutoupdateNonActiveWindows = false;
    UISettings.DragDropNotofications = false;
    UISettings.AskOnExit = true;
    UISettings.ShowVersionIDs = NavigatorCaptionVersionsMode.CaptionBracket;
    UISettings.FullScreenMode = false;
    UISettings.ShowShortAttributeNames = false;
    UISettings.ShowSplash = true;
    UISettings.NavigatorWindowCaptionsMode = NavigatorWindowCaptionsMode.Default;
    UISettings.ShowVersionsLog = false;
    UISettings.ShowGridChkoutColumn = true;
    UISettings.ShowTreeChkoutColumn = false;
    UISettings.NavigatorWindowBaseVersionsMode = NavigatorWindowBaseVersionsMode.ShowOtherVersions;
    UISettings.AlwaysShowFirstTab = false;
    UISettings.NavigatorLinksMode = NavigatorLinksMode.MiddleMouseClick;
    UISettings.ShowUnitedSelections = true;
    UISettings.ShowFavoritesFolder = true;
    UISettings.ShowListObjectTypes4CreatingObject = false;
  }

  public static void RaiseChanged()
  {
    EventHandler changed = UISettings.Changed;
    if (changed == null)
      return;
    changed((object) null, EventArgs.Empty);
  }

  public static event EventHandler Changed;
}
